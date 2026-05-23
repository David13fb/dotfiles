#include <stdio.h>
#include <stdlib.h>
#include <unistd.h> //cara conseguir los ids
int main(int argc, char *argv[]) {

    //prints  
    printf("pid: %d \n",getpid());
    printf("ppid: %d \n",getppid());
    printf("pgid: %d \n",getpgid(getpid()));
    printf("sid: %d \n",getsid(getpid()));
    printf("user ID: %d \n",getuid());
    printf("group ID: %d \n",getgid());
int sleepParent = argv[1];
int sleepChild = argv[2];

    
}
