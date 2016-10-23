#include "t_lib.h"

int interval = 1000;//Time interval
int caught = 0;	//Used to tell if yield has been caught by or called
//before termination.

//Sig handler for SIG ALARM
void sig_handler(int sig){
	caught = 1;
	printf("\n**\a %d captured...Swapping Context**\n\n", sig);
	signal(sig, sig_handler);//Signal reset.
	t_yield();
}

struct sem_t{
    int count;
    struct tcb* queue;
};

struct mnode{
    char* message;     // copy of the message 
    int  len;          // length of the message 
    int  sender;       // TID of sender thread 
    int  receiver;     // TID of receiver thread 
    struct mnode* next; // pointer to next node 
};

struct mbox{
	struct mnode* msg;
	struct sem_t* msem;
};


struct tcb{
    int thread_id;
    int thread_priority;
    ucontext_t* thread_context;
    struct tcb *next;
    struct tcb* prev;
    struct mnode* buffer;
    sem_t *tSem;
};


tcb *running;
tcb *ready0;
tcb *ready1;
sem_t *mSem;
sem_t *mSem2;

//Changed for LQ.
void t_init()
{
  tcb *tmp = createTCB();
  sem_init(&mSem,0);
  sem_init(&mSem2,0);
  tmp->thread_id = 0;
  tmp->thread_priority = 1;//Main has lower priority = 1.
  getcontext(tmp->thread_context);    /* let tmp be the context of main() */
  running = tmp;
  ready0 = 0;
  ready1 = 0;
}

//Gets head of file.
//Does NOT check for null, check before passing.
tcb* getHead(tcb* file){
	if(file->prev)return getHead(file->prev);
	else return file;
}
//Gets tail of file.
//Does NOT check for null, check before passing.
tcb* getTail(tcb* blah){
	if(blah->next)return getTail(blah->next);
	else return blah;
}

void setNext(tcb* this, tcb* target){
	this->next = target;
}
//sets prev node
void setPrev(tcb* this, tcb* target){
	this->prev = target;
}

void addToEnd(tcb* head, tcb* new){
	if(head && new){
	head = getTail(head);
	setNext(head, new);
	setPrev(new, head);
	head = getHead(head);
	}
}
//Check if thread is present
int isPresent(int id, tcb* head){
	tcb* temp = getHead(head);
	while(temp){
		if(temp->thread_id == id)return 0;
		temp = temp->next;
	}
	return 1;
}

//end of linked list functions

tcb* createTCB(){
	tcb* ret = (tcb*)malloc(sizeof (tcb));
	ret->next = 0;
	ret->prev = 0;
	ret->thread_context = (ucontext_t *) malloc(sizeof(ucontext_t));
	ret->thread_id = NULL;
	ret->thread_priority = NULL;
	ret->buffer = 0;
	sem_init(&(ret->tSem),1);
	return ret;
}
/*
basically, put the running thread at the end
of ready and the head of ready goes to running

Adapted for LQ.
*/
void t_yield()
{
	if(caught == 0)ualarm(0,0);//In case terminates early.

	if(ready0)ready0 = getHead(ready0);
	//Checks if there are high priority threads left.
	//This occurs no matter what priority the
	//current thread has.
	if(length(ready0)!=0){
	  tcb* temp = running;
	  running = getHead(ready0);
	  ready0 = ready0->next;
	  running->next = 0;
	  running->prev = 0;
	  //Have to check the old thread's priority
	  //before deciding where to place it.
	  if(temp->thread_priority==0){
		  //if its priority is 0 it belongs in the ready0 queue.
		  if(ready0){
			  ready0->prev = 0;
			  addToEnd(ready0, temp);
		  }
		  else ready0 = temp;
	  }
	  else{
		  //otherwise it belongs in the ready1 queue.
		  if(ready1){
				  ready1->prev = 0;
				  addToEnd(ready1, temp);
			  }
		  else ready1 = temp;
	  }
	  //the one that is running, with the one you want to run.

	  //Reset alarm and caught flag.
	  sighold(SIGALRM);
	  caught = 0;
	  ualarm(interval,0);
	  sigrelse(SIGALRM);
	  //
	  swapcontext(temp->thread_context, running->thread_context);
	}
	//There are no other high priority threads.
	else{
		//If the current thread itself is
		//a higher priority no need to swap.
		if(running->thread_priority == 0)printf("Current thread higher priority. Continuing...\n");
		else{
			//The running thread priority is 1. Checks ready1.
			if(ready1)ready1 = getHead(ready1);
			if(length(ready1)!=0){
			  tcb* temp = running;
			  running = getHead(ready1);
			  ready1 = ready1->next;
			  running->next = 0;
			  running->prev = 0;
			  //Old thread's priority has to be 1.
			  if(ready1){
				  ready1->prev = 0;
				  addToEnd(ready1, temp);
			  }
			  else ready1 = temp;
			  //the one that is running, with the one you want to run.

			  //Reset alarm and caught flag.
			  sighold(SIGALRM);
			  caught = 0;
			  ualarm(interval,0);
			  sigrelse(SIGALRM);
			  //
			  swapcontext(temp->thread_context, running->thread_context);
			}
			//There are no threads in both ready queues. Continuing current thread.
			else printf("There are no other threads in the ready queues...\n");
		}
	}
}

/*
free the running thread
and make the head of ready running

Adapted for LQ.
*/
void t_terminate()
{
	free(running->thread_context);
	//Check higher priority threads first.
	if(length(ready0)!= 0){
		ready0 = getHead(ready0);
		running = createTCB();
		running->thread_context = ready0->thread_context;
		running->thread_id = ready0->thread_id;
		running->thread_priority = ready0->thread_priority;
		running->buffer = ready0->buffer;
		ready0 = ready0->next;
		if(ready0)ready0->prev = 0;
		swapcontext(getCurrentContext(),running->thread_context);
	}
	//then check lower priority threads.
	else{
		ready1 = getHead(ready1);
		running = createTCB();
		running->thread_context = ready1->thread_context;
		running->thread_id = ready1->thread_id;
		running->thread_priority = ready1->thread_priority;
		running->buffer = ready1->buffer;
		ready1 = ready1->next;
		if(ready1)ready1->prev = 0;
		swapcontext(getCurrentContext(),running->thread_context);
	}
}
//DEBUG
void printQueues(){
	  printf("Ready0\n");
	  printAll(ready0);
	  printf("Ready1\n");
	  printAll(ready1);
	  printf("Running\n");
	  printAll(running);
}

void printAll(tcb* head){
	int i = 0;
	while(head){
		printf("Thread %d: ID%d\n", head->thread_id);
		head = head->next;
	}
	if(head)head = getHead(head);
}
/*
free running and ready.
Changed for LQ.
*/
void t_shutdown()
{
	if(running)emptyQueue(getHead(running));
	if(ready0)emptyQueue(getHead(ready0));
	if(ready1)emptyQueue(getHead(ready1));
	free(running);
	free(ready0);
	free(ready1);
	printf("All queues cleared. Exiting..\n");

}
//Empties queue. Requires head to be the head of the function.
//Does NOT check for null. Do before passing.
void emptyQueue(tcb* head){
	if(head){
		tcb* temp = head->next;
		while(head){
			free(head->thread_context);
			head = temp;
			if(head)temp = head->next;
		}
	}
}

ucontext_t* getCurrentContext(){
	ucontext_t* ret = (ucontext_t*) malloc(sizeof (ucontext_t));
	getcontext(ret);
	return ret;
}

int length(tcb* head){
	int c = 0;
	while(head){
		c++;
		head = head->next;
	}
	return c;
}

/*
add to the end of ready unless running is empty
otherwise make it running

Changed for LQ.
*/
void t_create(void (*fct)(int), int id, int pri)
{
  signal(SIGALRM, sig_handler);
  size_t sz = 0x10000;
  tcb* newTCB = createTCB();
  newTCB->thread_id = id;
  newTCB->thread_priority = pri;
  getcontext(newTCB->thread_context);
  newTCB->thread_context->uc_stack.ss_sp = malloc(sz);
  newTCB->thread_context->uc_stack.ss_size = sz;
  newTCB->thread_context->uc_stack.ss_flags = 0;
  makecontext(newTCB->thread_context, fct, 1, id);
  if(pri == 0){
	  if(!ready0){
		  ready0 = (tcb*)malloc(sizeof(tcb));
		  ready0 = newTCB;
	  }
	  else addToEnd(ready0, newTCB);
  }
  else if(pri == 1){
	  if(!ready1){
		  ready1 = (tcb*)malloc(sizeof(tcb));
		  ready1 = newTCB;
	  }
	  else addToEnd(ready1, newTCB);
  }
  else fprintf(stderr, "Create error: Thread priority %d is not 0 or 1\n", pri);
  ualarm(1, 0);
}

void sem_init(sem_t **sp, int sem_count)
{
    (*sp) = malloc(sizeof(sem_t));
    (*sp)->count = sem_count;
    (*sp)->queue = 0;
}

void sem_wait(sem_t *sp)
{
	sighold(SIGALRM);
    sp->count--;
    if (sp->count < 0){
    	tcb* temp = running;
    	if(sp->queue)addToEnd(sp->queue, temp);
    	else sp->queue = temp;
    	
    	if(ready0){
    		running = getHead(ready0);
    		ready0 = ready0->next;
    		if(ready0)ready0->prev = 0;
    	}
    	else if(ready1){
    		running = getHead(ready1);
    		ready1 = ready1->next;
    		if(ready1)ready1->prev = 0;   		
    	}
    	
		running->next = 0;
		running->prev = 0;
		swapcontext(temp->thread_context, running->thread_context);
		sigrelse(SIGALRM);
    }
    sigrelse(SIGALRM);
}

void sem_signal(sem_t *sp)
{
	sighold(SIGALRM);
    sp->count++;
    if (sp->count <= 0){
    	tcb* temp = getHead(sp->queue);
    	sp->queue = sp->queue->next;
    	if(sp->queue)sp->queue->prev = 0;
    	temp->next = 0;
    	temp->prev = 0;
    	if(temp->thread_priority == 0)addToEnd(ready0, temp);
    	else addToEnd(ready1, temp);
    }
    sigrelse(SIGALRM);
}

void sem_destroy(sem_t **sp)
{
    if((*sp)->queue)emptyQueue(getHead((*sp)->queue));
    free((*sp)->queue);
    printf("Cleared semaphore...\n");
}

mnode* createNode(){
	mnode* node = (mnode*)malloc(sizeof(mnode));
	node->message = malloc(256);
	node->next = 0;
	node->len = 0;
	node->sender = 0;
	node->receiver = 0;
	return node;
}
void emptyNodes(mnode* node){
	if(node){
		mnode* temp = node->next;
		while(node){
			free(node->message);
			node = temp;
			if(node)temp = node->next;
		}
	}
}
/* Destroy any state related to the mailbox pointed to by mb. */
void mbox_destroy(mbox **mb){
	sem_destroy(&((*mb)->msem));
	emptyNodes((*mb)->msg);
	free((*mb)->msg);
	free((*mb));
	free(mb);
	printf("Emptied mailbox...\n");
}
/* Create a mailbox pointed to by mb. */
void mbox_create(mbox **mb){
	*mb = malloc(sizeof(mbox));
	sem_init(&((*mb)->msem), 1);
	(*mb)->msg = 0;//initialize to null.
}

/* Deposit message msg of length 
 * len into the mailbox pointed to by mb. */
void mbox_deposit(mbox* mb, char* msg, int len){
	sem_wait(mb->msem);
	if(mb->msg){
		mnode* head = mb->msg;//save the head.
		while(head->next){
			head = head->next;
		}
		head->next = createNode();
		head->next->message = copyString(msg);
		head->next->len = len;
	}
	else{
		mb->msg = createNode();
		mb->msg->len = len;
		mb->msg->message = copyString(msg);
	}
//Debug to check contents of mailbox.
//	mnode* temp = mb->msg;
//	printf("---------------\n");
//	printf("Current mailBox:\n");
//	int c = 0;
//	while(temp){
//		printf("%d:%s of length %d\n", c, temp->message, temp->len);
//		temp =temp->next;
//		c++;
//	}
//	printf("---------------\n");
//End debug.
	sem_signal(mb->msem);
}

/* Withdraw the first message from the mailbox pointed to by mb into msg and set the 
 * message's length in len accordingly. Similar to receive(). */
void mbox_withdraw(mbox *mb, char *msg, int *len){
	sem_wait(mb->msem);
	if(mb->msg){
		*len = mb->msg->len;
		strcpy(msg, mb->msg->message);
		if(mb->msg->next){
			mnode* temp = mb->msg;
			mb->msg = mb->msg->next;
			free(temp->message);
			free(temp);
		}
		else{
			free(mb->msg->message);
			free(mb->msg);
			mb->msg = 0;
		}
	}
	else printf("There are no messages...\n");
	sem_signal(mb->msem);
}

char* copyString(char* target){
	/*Creates a new String in memory. Deep copying.*/
	char* returnS = malloc(1024);
	return strcpy(returnS,target);
}

void send(int tid, char *msg, int len){
	/* Send a message to the thread whose tid is tid. 
	 * msg is the pointer to the start of the message, 
	 * and len specifies the length of the message in bytes.
	 * In your implementation, all messages are character strings. */
	
	tcb* temp = 0;
	if(running){
		if(running->thread_id == tid)temp = running;
	}
	if(ready0 && !temp){
		tcb* t = getHead(ready0);
		while(t){
			if(t->thread_id == tid){
				temp = t;
				break;
			}
			t = t->next;
		}
	}
	if(ready1 && !temp){
		tcb* t = getHead(ready1);
		while(t){
			if(t->thread_id == tid){
				temp = t;
				break;
			}
			t = t->next;
		}
	}
	if(!temp){
		printf("Thread [%d] does not exist. send() cannot be completed..\n", tid);
		return;
	}
	
	sem_wait(temp->tSem);
	if(temp->buffer){
		mnode* t = temp->buffer;
		while(t->next){
			t = t->next;
		}
		t->next = createNode();
		t = t->next;
		t->next = 0;
		t->len = len;
		t->receiver = tid;
		t->sender = running->thread_id;
		strcpy(t->message, msg);
	}
	else{
		temp->buffer = createNode();
		temp->buffer->next = 0;
		temp->buffer->len = len;
		temp->buffer->receiver = tid;
		temp->buffer->sender = running->thread_id;
		strcpy(temp->buffer->message, msg);
	}
	sem_signal(temp->tSem);
//	
//	mnode* t1 = temp->buffer;
//	int count = 1;
//	while(t1){
//		printf("Message%d:%s in %d from %d\n", count, t1->message, temp->thread_id, t1->sender);
//		t1 = t1->next;
//		count++;
//	}
}

void receive(int *tid, char *msg, int *len){
	/* Receive a message from another thread. 
	 * The caller has to specify the sender's 
	 * tid in tid, or sets tid to 0 if intends
	 *  to receive from any thread. Upon returning,
	 *  the message is stored starting at msg. The 
	 * tid of the thread that sent the message is stored
	 *  in tid, and the length of the message is stored in 
	 * len. The caller of receive() is responsible for 
	 * allocating the space in which the message is stored.
	 *  If there is no message for the caller, then both 
	 * tid and len are set to 0. Even if more than one message 
	 * awaits the caller, only one message is returned per 
	 * call to receive(). Messages are received in the order
	 *  in which they were sent. */
	mnode* buff = running->buffer;
	if(!buff){
		*tid = 0;
		*len = 0;
		msg = 0;
		printf("Thread[%d] has no messages...\n", running->thread_id);
		sem_signal(running->tSem);
		return;
	}
	if(*tid == 0){
		//Withdraw the first item.
		*len = buff->len;
		*tid = buff->sender;
		strcpy(msg, buff->message);
		if(running->buffer->next){
			running->buffer = running->buffer->next;
		}
		else running->buffer = 0;
		sem_signal(running->tSem);
		return;
	}
	else{
		//Search for the tid.
		mnode* prev = createNode();
		while(buff){
			if(buff->sender == *tid){
				//Found it.
				*len = buff->len;
				*tid = buff->sender;
				strcpy(msg, buff->message);
				if(prev != buff){
					prev->next = buff->next;
					free(buff->message);
					free(buff);
				}
				else running->buffer = 0;//There is only one node and it was the match.
				sem_signal(running->tSem);
				return;
			}
			if(!prev)prev = createNode();
			prev = buff;
			buff = buff->next;
		}
	}
}

void block_send(int tid, char *msg, int length){ 
/* Send a message and wait for reception. 
 * The same as send(), except that the caller
 * does not return until the destination 
 * thread has received the message. */
	
	tcb* temp = 0;
	if(running){
		if(running->thread_id == tid)temp = running;
	}
	if(ready0 && !temp){
		tcb* t = getHead(ready0);
		while(t){
			if(t->thread_id == tid){
				temp = t;
				break;
			}
			t = t->next;
		}
	}
	if(ready1 && !temp){
		tcb* t = getHead(ready1);
		while(t){
			if(t->thread_id == tid){
				temp = t;
				break;
			}
			t = t->next;
		}
	}
	if(mSem->queue && !temp){
		tcb* t = getHead(mSem->queue);
		while(t){
			if(t->thread_id == tid){
				temp = t;
				break;
			}
			t = t->next;
		}
	}
	
	if(!temp){
		printf("Thread [%d] does not exist. send() cannot be completed..\n", tid);
		return;
	}
	
	if(temp->buffer){
		mnode* t = temp->buffer;
		while(t->next){
			t = t->next;
		}
		t->next = createNode();
		t = t->next;
		t->next = 0;
		t->len = length;
		t->receiver = tid;
		t->sender = running->thread_id;
		strcpy(t->message, msg);
	}
	else{
		temp->buffer = createNode();
		temp->buffer->next = 0;
		temp->buffer->len = length;
		temp->buffer->receiver = tid;
		temp->buffer->sender = running->thread_id;
		strcpy(temp->buffer->message, msg);
	}
	sem_signal(mSem);///Works with test9
	sem_wait(mSem2);///Works with test11
}

void block_receive(int *tid, char *msg, int *length){
	/* Wait for and receive a message.  Similar
	 *  to receive() except that the caller will
	 *  not resume execution until it has received
	 *  a message. */
	sem_wait(mSem);///Works with test9
	receive(tid, msg, length);
	sem_signal(mSem2);///Works with test11.

}
