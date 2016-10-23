/*
 * types used by thread library
 */
#include <stdio.h>
#include <stdlib.h>
#include <ucontext.h>
#include <sys/mman.h>
#include <signal.h>
#include <unistd.h>
#include <string.h>
typedef struct sem_t sem_t;
typedef struct tcb tcb;
typedef struct mnode mnode;
typedef struct mbox mbox;
void sig_handler(int sig);
void t_create(void(*function)(int), int thread_id, int priority);
void t_yield(void);
void t_init(void);
void t_terminate();
void t_shutdown();
int length(tcb* head);
void emptyQueue(tcb* head);
void printAll(tcb* head);
void printQueues();
ucontext_t* getCurrentContext();
tcb* createTCB();
tcb* getHead(tcb* file);
tcb* getTail(tcb* file);
void setNext(tcb* this, tcb* next);
void setPrev(tcb* this, tcb* prev);
void addToEnd(tcb* this, tcb* next);
int isPresent(int id, tcb* head);
void sem_destroy(sem_t **sp);
void sem_signal(sem_t *sp);
void sem_wait(sem_t *sp);
void sem_init(sem_t **sp, int sem_count);
mnode* createNode();
void mbox_create(mbox **mb);
void mbox_destroy(mbox **mb);
void mbox_deposit(mbox *mb, char *msg, int len);
void mbox_withdraw(mbox *mb, char *msg, int *len);
char* copyString(char* target);
void send(int tid, char *msg, int len);
void receive(int *tid, char *msg, int *len);
void block_send(int tid, char *msg, int length);
void block_receive(int *tid, char *msg, int *length);

