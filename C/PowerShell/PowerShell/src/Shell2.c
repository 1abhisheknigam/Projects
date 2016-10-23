/*
 ============================================================================
 Name        : Shell2.c
 Author      : Abhishek Nigam
 Version     : 2.5
 Copyright   : No copying!
 Description : Shell Program in C, ANSI-style. With Threads.
 ============================================================================
 */
#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <strings.h>
#include <limits.h>
#include <unistd.h>

#include <time.h>
#include <pthread.h>
#include <float.h>

#include <glob.h>
#include <dirent.h>
#include <sys/types.h>
#include <sys/wait.h>
#include <sys/stat.h>
#include <signal.h>
#include "Shell2.h"
#include "List.h"
#include <pwd.h>
#include <utmpx.h>

/*Global Variables for Project 3*/
float load = FLT_MAX;								/*Global load variable. Initially set to max so that any load is less than it.*/
list* users;										/*Global list of users that watchusers shall use*/
pthread_mutex_t lock = PTHREAD_MUTEX_INITIALIZER;	/*Mutex lock for the users list.*/

/*Thread handlers*/
static void *watchusers(void *arg){
	struct utmpx *up;
	
	while(1){
		pthread_mutex_lock(&lock);
		 setutxent();			/* start at beginning */
		 if(users){
			  while (up = getutxent() )	/* get an entry */
			  	{
				  		users = getHead(users);
					    if ( up->ut_type == USER_PROCESS )	/* only care about users */
					    {
					    	  if(isPresent(up->ut_user, users)== 0)
					    	  	printf("%s has logged on %s from %s\n", up->ut_user, up->ut_line, up ->ut_host);		  
					    }
/*Extra Credit!!*/					    
					    else if(up->ut_type = DEAD_PROCESS){/*checks dead processes of users*/
					    	if(isPresent(up->ut_user, users)==0)
					    		if(up->ut_host)//users have a host, dead processes do not.
					    			printf("%s has logged out of %s from %s\n", up->ut_user, up->ut_line, up ->ut_host);	
					    }//If the dead process has a host then there is a user who logged out.
				  }//Check watchuserslogout.txt script.
			  }
		 else printf("watchuser Thread: No users to watch!\n");	
		pthread_mutex_unlock(&lock);
		sleep(20);
		printf("watchuser Thread: Slept for 20s\n");
	}
	return arg;
}


static void *watchmail(char* file){
	/* Checks the file "file" and 
	 * gives an alert if the size
	 * has changed since the last time
	 * the thread checked. */
	struct stat* buf = (struct stat*) malloc(sizeof(struct stat));
	stat(file, buf);
	int s = buf->st_size;
	void *r;
	while(1){
		stat(file, buf);
		if(buf->st_size != s){
			struct timeval tp;
			if(gettimeofday(&tp, NULL) == 0){
				printf("\aYou've got mail in %s at %s", file, ctime(&(tp.tv_sec)));
				s = buf->st_size;
			}
			else{
				//Professor specified to exit upon error in this thread.
				perror("timeval Error");
				pthread_exit(r);
			}
		}
		sleep(10);
		printf("watchmail Thread: Watched %s for 10s\n", file);
	}
	free(buf);
	return r;
}

static void *warnLoad(void* arg){
	/* Thread that checks if the
	 * one minute load average is
	 * greater than the global 
	 * load variable*/
	while(1){
		if(load == 0.0){
			load = FLT_MAX;
			printf("Load = 0.0, Ending Thread..\n");
			pthread_exit(arg);
		}
		double l[1];
		if(getloadavg(l, 1)!= -1)
			{float f = l[0];
		 	 if(f > load)
		 		 printf("Warning: Load Level:%.2f Threshold:%.1f\n",f, load);
			}
		else perror("warnload error");
		//Professor specified for thread to keep running despite error in this thread.
		sleep(30);
		printf("Warnload Thread: Slept for 30.\n");
	}
}
/* The sighandlers for the Shell.*/
void sig_handler(int sig)
{
	if(sig != 18)printf("Caught signal: %d\n", sig);
	signal(sig, sig_handler);//Signal Reset.
}

void sig_child(int sig){
	/* New signal handler for Project 2
	 * which allows background processes 
	 * to be reaped.*/
	int status;
	waitpid(-1,&status,WNOHANG | WUNTRACED); 
	signal(sig, sig_handler);//Signal Reset
}
/* Main Shell Function, as defined by Skeleton Code.
 * All variables not used from the Skeleton Code have
 * been commented. Code segments may have been
 * modified.
 */
int sh( int argc, char **argv, char **envp )
{
  char *prompt = calloc(PROMPTMAX, sizeof(char));			/*Prompt String*/	
  char *commandline = calloc(MAX_CANON, sizeof(char));		/*User Input String*/			
  char* command = malloc(MAX_CANON);						/*Will get Command String from Input*/	
  char* arg  = malloc(MAX_CANON);							/*Will get args from Input String*/	
  char **args = calloc(MAXARGS, MAX_CANON);					/*Will store all the each arg*/	  
  char *pwd = calloc(MAX_CANON,sizeof(char));				/*Stores current directory*/	
  char *owd = calloc(MAX_CANON,sizeof(char));				/*Stores old directory*/	

  int uid ,go = 1;
  int pid = getpid();
  struct passwd *password_entry;
  char *homedir;
  setbuf(stdout, NULL);//Instant printf

  
  uid = getuid();
  password_entry = getpwuid(uid);              				 /* get passwd info */
  homedir = password_entry->pw_dir;							/* Home directory to start out with*/
     
  
  if ( (pwd = getcwd(NULL, PATH_MAX+1)) == NULL )
  {
    perror("getcwd");
    exit(2);
  }
  owd = copyString(pwd);									/*pwd and owd have been initialized.*/
  prompt[0] = '\0';											/*Prompt initialized to Nothing.*/

	//SIGNALS
	  signal(SIGINT,  sig_handler);							/*Defines handler for all the necessary signals*/
	  signal(SIGTSTP, sig_handler);
	  signal(SIGTERM, sig_handler);
	  
	  signal(SIGCHLD, sig_child);
	//SIGNALS
  
//Project 2 Initializers//
  list* mailFiles = 0;										/*List of files and threads for watchmail threads.*/				
  users = 0;												/*Initialize Global List users to nothing*/
  int lastAmp = 0;											/*Flag to check if ampersand is last in command.*/
  int loadThread = 1, userThread = 1;						/*Flags to check if warnload and watchuser threads are running yet.
   															  to makee sure there is only one thread for them.*/
	  
	  
  while(go)													/*Starts the loop*/
  {
	  
/* print your prompt */
	printf("%s [%s]>",prompt,pwd);
/* get command line and process. Removes newline. */
	if(fgets(commandline, MAX_CANON, stdin)==NULL){printf("\n");continue;}///Check for EOF
	chomp(commandline);
	if(commandline == NULL)continue;
	if(strlen(commandline)==0)continue;//Checks for empty command.
	lastAmp = checkAmp(commandline);
	//printf("%s = %d\n", commandline, lastAmp);
	char* line = copyString(commandline);
	char* tempLine = copyString(line);//Saves line in case Needed Later.
	tempLine = tempLine;
/*The commandline has been readied for checking now. 
 * Prechecking process:
 * Storing the actual command. 
 * Storing the args. 
 * Storing the commandline without the command in a string.
 * Storing the commandline without the first two words for possible aliasing.
 * */
	command = strtok(line," ");
	arg = strtok(NULL," ");
	int size = 0;
	while(arg){
		args[size] = copyString(arg);
		arg = strtok(NULL," ");
		size++;
	}
	char* restLine = malloc(MAX_CANON);//for list
	int c;
	for(c = 0; c < size; c++){
		strcat(restLine, args[c]);
		if(c != (size -1))strcat(restLine," ");
	}
	
/* End of getting command Line and Processing.*/
/* Checking command for built in commands begins.*/	
	if(!strcmp(command,"exit"))
	{
		printf("Executing built-in Command:%s\n",command);
		if(size ==0){
			printf("Exiting..");
			go = 0;
		}
		else fprintf(stderr,"Exit has Unnecessary Arguments.\n");
	}
	else if(!strcmp(command,"cd")){
		printf("Executing built-in Command:%s\n",command);
		int c = 0;
		char* tempDir = copyString(owd);
		if(size !=0){
			if(size>1)fprintf(stderr,"%s: Error: Too many arguments\n",command);
			else{
				if(!strcmp(args[0], "-")){
					owd = copyString(pwd);
					pwd = copyString(tempDir);
					chdir(pwd);
					free(tempDir);
				}
				else{
				owd = copyString(pwd);
				char* dir = copyString(restLine);
				c = chdir(dir);
				 if ( (pwd = getcwd(NULL, PATH_MAX+1)) == NULL )
				  {
				    perror("getcwd Error");
				    exit(2);
				  }
				}
			}
		}
		else{
			owd = copyString(pwd);
			pwd = homedir;
			c = chdir(pwd);
		}
		if(c!=0){
			perror("cd Error");
			owd = copyString(tempDir);
			free(tempDir);
		}
	}
	else if(!strcmp(command,"pwd")){
		if(size == 0){
			printf("Executing built-in Command:%s\n",command);
			char cwd[1024];
			if (getcwd(cwd, sizeof(cwd)) != NULL)printf("Current Working Directory: %s\n", cwd);
			else printf("getcwd() error");
		}
		else fprintf(stderr,"%s: Error: No arguments required.\n", command);
	}
	else if(!strcmp(command,"pid")){
		if(size == 0){
			printf("Executing built-in Command:%s\n",command);
			printf("SHELL VERSION:3.4\nTYPE:NIGAM MACHINE\nPID:%d\n", getpid());
		}
		else fprintf(stderr,"%s: Error: No arguments required.\n", command);
	}
	else if(!strcmp(command,"kill")){
		printf("Executing built-in Command:%s\n",command);
		int result;
		if(size == 0)printf("Kill needs at least 1 Argument.\n");
		else{
			if(size ==1){
				result = kill(atoi(args[0]),SIGTERM);
			}
			else {
				int t = atoi(args[0]);
				t*=-1;
				result = kill(atoi(args[1]),t);
			}
			if(result == -1)perror("Kill Error");
		}
	}
	else if(!strcmp(command,"list")){
			printf("Executing built-in Command:%s\n",command);
			if(size !=0){
				int i;
				for(i=0;i<size;i++){
					printf("%s:\n",args[i]);
					listFiles(args[i]);
				}
			}
			else listFiles(pwd);
		}
/*end of built in command checking for Project 2 */
	
	else if(!strcmp(command, "warnload")){
		/* Warnload
		 * 
		 * Starts the thread for checking 
		 * the one minute load average relative
		 * to user defined load.
		 * 
		 * One argument specifies load.*/
		if(size == 0)fprintf(stderr, "Not Enough Arguments.\n");
		else if(size > 1)fprintf(stderr, "warnload: Too many Arguments.\n");
		else{
			load = atof(args[0]);
			if(load == 0.0)loadThread = 1;
			if(loadThread != 0){
				pthread_t tid1;
				pthread_create(&tid1, NULL, warnLoad, "warnload1");
				loadThread = 0;
			}
		}
	}
	else if(!strcmp(command, "load")){
		/* Load
		 * 
		 * Debug command to see the last user defined load.*/
		if(load == FLT_MAX)printf("Load Threshold not specified.\n");
		else printf("Load Threshold:%.1f\n", load);
	}
	else if(!strcmp(command, "watchuser")){
		/* watchuser
		 * 
		 * Command to start a single thread to keep track of 
		 * users logged on and off.
		 * 
		 * One argument starts the thread off if not started yet.
		 * Subsequent calls simply add more users to the global
		 * list of users.
		 * 
		 * Two arguments with the arg "off" tells the thread to 
		 * stop looking for a previously specificed user by 
		 * deleting it from the global linked list.*/
		if(size == 0)fprintf(stderr, "watchuser: Not enough arguments");
		else if(size ==1){
			pthread_mutex_lock(&lock);
			if(users)addToEnd(users, createList(NULL, copyString(args[0])));
			else users = createList(NULL, copyString(args[0]));
			pthread_mutex_unlock(&lock);
			if(userThread != 0){
				pthread_t tid1;
				pthread_create(&tid1,NULL, watchusers, "watchusers1");
				userThread = 0;
			}
		}
		else if (size == 2){
			if(!strcmp(args[1], "off")){
				pthread_mutex_lock(&lock);
				users = delete(copyString(args[0]), users);
				pthread_mutex_unlock(&lock);
			}
			else fprintf(stderr, "watchuser: Wrong parameter. User 'off' or none.\n");
		}
		else fprintf(stderr, "watchuser: Too many Arguments.\n");
	}
	else if(!strcmp(command, "userList")){
		/* userList
		 * 
		 * Debug function that prints out the global
		 * linked list of users*/
		printAll(users);
	}
	else if(!strcmp(command, "users")){
		/* users
		 * 
		 * Debug function that prints out all logged on 
		 * user activity.*/
		 struct utmpx *up;
		  setutxent();			/* start at beginning */
		  while (up = getutxent() )	/* get an entry */
		  {
		    if ( up->ut_type == USER_PROCESS )	/* only care about users */
		    {
		      printf("%s has logged on %s from %s\n", up->ut_user, up->ut_line, up ->ut_host);
		    }
		  }
	}
	else if(!strcmp(command, "dead")){
		/* dead
		 * 
		 * Debug function that prints out all dead 
		 * processes.*/
		 struct utmpx *up;
				  setutxent();			/* start at beginning */
				  while (up = getutxent() )	/* get an entry */
				  {
				    if ( up->ut_type == DEAD_PROCESS )	
				    {
				      printf("NAME:%s LINE:%s HOST:%s\n", up->ut_user, up->ut_line, up ->ut_host);
				    }
				  }
	}
	else if(!strcmp(command, "pmt")){
		/* pmt (print mail threads)
		 *
		 * Debug function that prints out all the 
		 * files that the thread is watching in the
		 * linked list of files and threads.*/
		printAll(mailFiles);
	}
	else if(!strcmp(command, "watchmail")){
		/* watchmail
		 * 
		 * Command that starts a new thread EACH time it
		 * is called. The thread as well as the file 
		 * specified in the first argument is stored
		 * in a linked list.
		 * 
		 * When specified with a second "off" parameter
		 * the thread watching the specified file is 
		 * canceled and removed from the list.
		 * */
		if(size == 0)fprintf(stderr, "watchmail: Not Enough Arguments.\n");
		else{
			if(size == 1){
				if(access(args[0],F_OK) != -1){
					printf("%s Exists...\n", args[0]);
					pthread_t tid1;
					pthread_create(&tid1,NULL,watchmail,(void*)copyString(args[0]));	
					if(mailFiles){
						addToEnd(mailFiles, createList(tid1, copyString(args[0])));
					}
					else{
						mailFiles = createList(tid1, args[0]);
					}
				}
				else perror("watchmail Error");
			}
			else if(size ==2){
				if(!strcmp(args[1], "off")){
					mailFiles = delete(copyString(args[0]), mailFiles);
				}
				else fprintf(stderr, "watchmail :%s not a valid parameter. Use \'off\' or none\n", args[1]);
			}
			else fprintf(stderr, "watchmail: Too many Arguments.\n");
		}
	}
/* end of builg in commands */
	
/* If control has reached to this point, 
 * that means the user may have entered
 * a shell command. Now to find it do 
 * fork(), execve() and waitpid() */
      else{
    	/* Uses which to check if it exists. This is a shortcut. The user may 
    	 * have entered an absolute path; thus the second half of the condition 
    	 * in the 'if' below. However, if the command is present in neither, it
    	 * does not exist as a shell command or directory. An error message is 
    	 * shown, and the loop continues. */
    	char* path = which(command, getPath()); 	
	        if(path == NULL && access(command, X_OK) != 0)fprintf(stderr, "%s: The system cannot find the path specified\n",command);
		    else {
		    /* The existence of the command or directory is shown*/
		    /* Building the process's necessary argv. By convention,
		     * the first value is the command itself, while the last
		     * is NULL. The first value is either the command or the
		     * path returned by which() depending on the command 
		     * entered. Thus this will be added later, since for 
		     * either case, the args will be the same. */
	    	    	char** argp = calloc(MAX_CANON, MAX_CANON);
	    	    	int i;
	    			int j = 1;
	    			for(i=0;i<size;i++){
	    				/* Wild Card Expanding. 
	    				 * The Execve function does not expand wildcards
	    				 * so it must be done before the arguments are sent.
	    				 * If a wildcard is detected in the argument, instead 
	    				 * of storing that argument, all the corresponding paths
	    				 * of the wild card are stored. For this reason, glob()
	    				 * is used. The glob_t variable returned by this 
	    				 * function returns a vector of strings containing all
	    				 * path values corresponding to the wild card. These values
	    				 * are stored in the argument. If the wildcard has no
	    				 * corresponding match, an error is shown and loop continues
	    				 * to the beginning.
	    				 * 
	    				 * If the argument does NOT contain a wildcard, it is 
	    				 * added regularly to the argv of the process.
	    				 */
	    				if(index(args[i],'*') != NULL || index(args[i],'?')!=NULL){
	    					glob_t paths;
	    					char** p;
	    					if(glob(args[i],0,NULL,&paths)==0){
	    						for (p = paths.gl_pathv; *p != NULL; ++p){
	    							argp[j] = copyString(*p);
	    							j++;
	    						}
	    					}
	    					/*Unorthodox check for any error(usually, no match).*/
	    					else {fprintf(stderr, "Wildcard Error: %s: No match.\n", args[i]);i = 299;}
	    				}
	    				else {argp[j] = copyString(args[i]);
	    				j++;
	    				}
	    			}
	    			/* The WildCard check is done, and loop restarts since wildcard is not valid*/
	    			if(i == 300)continue;
	    			argp[j+1] = NULL;
	    			/* End of Building argp (the argv of the process).*/
	    			
	    			/*Determining the argp[0].*/
	            	if(access(path,X_OK)==0)argp[0] = copyString(path);
	            	else if(access(command, X_OK)==0)argp[0] = copyString(command);
	            	/* The argp[0] is the command that the execve needs*/
	            	if(argp[0]!= NULL){
	    	        		/* A new process is created. User is notified that
	    	        		 * the command has been recognized and a new process
	    	        		 * has begun.*/
	            		if(lastAmp == 0)printf("Executing new background process:%s\n", command);
	            		else printf("Executing new process:%s\n", command);
    	        		pid = fork();
    	        		/* Child Process does the execution*/
    	        		if(pid == 0){
    	        			i = execve(argp[0], argp, envp);
    	        			if(i== -1){
    	        				/* If any error in the execution, error is shown
    	        				 * and the process is terminated with a bad 
    	        				 * return status.*/
    	        				perror("Execution Error");
    	        				exit(-1);
    	        			}
    	        		}
    	        		/*Checking for bad return status*/
    	        		else if(pid < 0)perror("Execution Error");
    	        		/*Parent Process waits for child's completion*/
    	        		else{
    	        			if(lastAmp ==1){/* If there was a ampersand given 
    	        			in the command as the last, the process is run in the
    	        			background instead. The program does not wait for it.
    	        			
    	        			If there is no ampersand, the process is run in 
    	        			the foreground.*/
    	        			waitpid(pid, NULL, 0);
    	        			while(pid == 0){
    	        				printf("Executing...\n");
    	        				sleep(1);
    	        				}	
	    	        		}
    	        		}
	            	}
	            	/* If argp[0] is NULL, then the path is not executable. This
	            	 * may be because of permissions, or the path not being an 
	            	 * executable file (e.g. a directory).*/
	            	else printf("%s: The Path is not Executable", command);
		    	}
      	}
    }
  /*The function for the shell ends when 0 is returned*/
  return 0;
} /* sh() */
/*HELPER FUNCTIONS*/
char *which(char *command, struct pathelement *pathlist )
{
   /* loop through pathlist until finding command and return it.  Return
   NULL when not found. */
	char* temp = malloc(MAX_CANON);
	while(pathlist){
		temp = copyString(pathlist->element);
		strcat(temp, "/");
		strcat(temp, command);
		if(access(temp, F_OK) == 0)return temp;
		pathlist = pathlist->next;
	}
	return NULL;
} /* which() */
void listFiles(char *dir)
{
	if(index(dir,'*') != NULL || index(dir,'?')!=NULL){
		glob_t paths;
		char** p;
		if(glob(dir,0,NULL,&paths)==0){
			for (p = paths.gl_pathv; *p != NULL; ++p){
				printf("%s\n",*p);
			}
		}
	}
	else{
		DIR *d = opendir(dir);
		struct dirent *ent;
		if (d!= NULL) {
		  /* print all the files and directories within directory */
		  while ((ent = readdir (d)) != NULL) {
		    printf ("%s\n", ent->d_name);
		  }
		  closedir(d);
		} else {
		  perror("List error");
		}
	}
} /* list() */
void chomp(char *s) {
	/*Gets rid of trailing \n in the string.*/
	   while(*s && *s != '\n' && *s != '\r') s++;
	    *s = '\0';
}
/* chomp() */
int checkAmp(char *s){
	/*Checks and gets rid of trailing ampersand*/
	while(*s){
		if(*s == '&'){
			*s--;
			if(*s != ' ')*s++;/*Gets rid of space before ampersand if any*/
			*s = '\0';
			return 0;
		}
		s++;
	}
	return 1;
}

char* copyString(char* target){
	/*Creates a new String in memory. Deep copying.*/
	char* returnS = malloc(MAX_CANON);
	return strcpy(returnS,target);
}
/* copyString() */

/* Shell.c */

