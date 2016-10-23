# Makefile for UD CISC361 user-level thread library
#Abhishek Nigam & Patrick Whetham
CC = gcc
CFLAGS = -g

LIBOBJS = t_lib.o 

TSTOBJS = test.o

# specify the executable 

EXECS = test

# specify the source files

LIBSRCS = t_lib.c

TSTSRCS = test.c

# ar creates the static thread library

t_lib.a: ${LIBOBJS} Makefile
	ar rcs t_lib.a ${LIBOBJS}

# here, we specify how each file should be compiled, what
# files they depend on, etc.

t_lib.o: t_lib.c t_lib.h Makefile
	${CC} ${CFLAGS} -c t_lib.c

test.o: test00.c 361_thread.h Makefile
	${CC} ${CFLAGS} -c test.c

test: test00.o t_lib.a Makefile
	${CC} ${CFLAGS} test.o t_lib.a -o test

clean:
	rm -f t_lib.a ${EXECS} ${LIBOBJS} ${TSTOBJS} 
