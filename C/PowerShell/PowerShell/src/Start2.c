/*
 ============================================================================
 Name        : Start.c
 Author      : Abhishek Nigam
 Version     : 2.1
 Copyright   : No copying!
 Description : Shell Program in C, ANSI-style
 ============================================================================
 */

#include <signal.h>
#include <stdio.h>
void sig_handler(int signal); 

int main( int argc, char **argv, char **envp )
{ 
  printf("Start\n");
  return sh(argc, argv, envp);//Start the shell.
}
#include "Shell2.h"
