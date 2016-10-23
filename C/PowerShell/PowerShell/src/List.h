#ifndef THREADS_H_
#define THREADS_H_
typedef struct list list;
struct list;
list* createList(pthread_t t, char* val);
void setNext(list* this, list* target);
void setPrev(list*this, list* target);
void freeList(list* file);
list* getHead(list* file);
list* getTail(list* file);
void addToEnd(list* newfile, list* target);
list*  delete(char* name, list* head);
int isPresent(char* name, list* head);
void printAll(list* file);
#endif /*MP3_H_*/
