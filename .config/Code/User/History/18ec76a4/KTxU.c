#include <stdio.h>
#include <stdlib.h>
#include <unistd.h> //cara conseguir los ids
int main(int argc, char *argv[]) {

    //prints  
    printf("pid: %d ",getpid());
    printf("ppid: %d ",getppid());
    printf("pgid: %d ",getpgid(getpid()));
    printf("sid: %d ",getsid(getpid()));
    
int sleepParent = argv[1];
int sleepChild = argv[2];

    
}
