
#include "getPath.h"
#include "List.h"

int pid;
int sh( int argc, char **argv, char **envp);
char *which(char *command, struct pathelement *pathlist);
char *where(char *command, struct pathelement *pathlist);
void listFiles ( char *dir );
void printenv(char **envp);
void chomp(char* c);
char* copyString(char* target);
void signal_handler(int signal);

extern list* users;
extern float warnload;

#define PROMPTMAX 32
#define MAXARGS 10
