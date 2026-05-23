
#include <fcntl.h>
#include <linux/limits.h>
#include <pthread.h>
#include <stdio.h>
#include <stdlib.h>
#include <sys/types.h>
#include <sys/stat.h>
#include <sys/sysmacros.h>
#include <sys/types.h>
#include <dirent.h>
#include <unistd.h>
#include <sys/wait.h>


int global;

void main() {

    int   local=3;

    pid_t pid;

  

    global=10;

    pid = fork();

    if (pid == 0 ) {

        global = global + 5;

        local  = local + 5;

    }

    else {

        wait(NULL);

        global += 10;

        local  += 10;

    }

    printf("global:%d local:%d\n", global, local);

}