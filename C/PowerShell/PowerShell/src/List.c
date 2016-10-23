/*
 ============================================================================
 Name        : mp3.c
 Author      : Abhishek Nigam
 Version     : V43.2
 Copyright   : Don't copy.
 Description : CISC361 Assignment 1 Part a
 ============================================================================
 */

#include <stdio.h>
#include <stdlib.h>
#include<string.h>
#include <pthread.h>
#include "List.h"
struct list{
	pthread_t thread;
	char* data;
	list* next;
	list* prev;
};
//constructor
list* createList(pthread_t t, char* val){
	list* re = malloc(sizeof *re);
	re->thread = t;
	re->data = val;
	re->next = 0;
	re->prev = 0;
	return re;
}
//sets next node
void setNext(list* this, list* target){
	this->next = target;
}
//sets prev node
void setPrev(list*this, list* target){
	this->prev = target;
}
//frees mp3* pointers and its contents
void freeList(list* t){
	if(t->thread != NULL)pthread_cancel(t->thread);
	free(t->data);
	free(t);
}

//gets the first node in the list
list* getHead(list* file){
	if(!file)return file;
	else{
	if(file->prev)return getHead(file->prev);
	else return file;
	}
}
//gets the last node in the list
list* getTail(list* file){
	if(file->next)return getTail(file->next);
	else return file;
}
//adds a new node to the tail of the list
void addToEnd(list* head, list* new){
	head = getTail(head);
	setNext(head, new);
	setPrev(new, head);
}
//delets a node by checking artist name with 
//name user provides
list*  delete(char* name, list* head){
	list* temp = head;
	list* temp2 = temp;
	int flag = 0;
	while(temp){
		if(!strcmp(name,temp->data))
			{
			 printf("Node with %s found and deleted.\n", name);
			 if(!temp->prev && !temp->next){
				freeList(head);
				temp2 = 0;
				temp = 0;
				flag = 1;
				break;
			 }
			 if(temp->prev)temp->prev->next = temp->next;
			 if(temp->next){
				 temp->next->prev = temp->prev;
				 temp2 = temp->next;
				 freeList(temp);
				 temp = temp2;
			 }
			 else{
				 freeList(temp);
				 temp = 0;
			 }
			flag = 1;
			}
		else{
		temp2 = temp;
		temp = temp->next;
		}
	}
	if(flag == 0)fprintf(stderr,"Node with %s not found.\n", name);
	return getHead(temp2);
}
//prints all nodes in a given list
//from any node, starting from
//beginning to end
void printAll(list* file){
	if(file){
	file = getHead(file);
	while(file){
		printf("Node data:%s\n",file->data);
		file = file->next;
	}
	}
	else printf("No Data.\n");
}

int isPresent(char* name, list* head){
	list* temp = getHead(head);
	while(temp){
		if(!strcmp(temp->data, name))return 0;
		temp = temp->next;
	}
	return 1;
}

