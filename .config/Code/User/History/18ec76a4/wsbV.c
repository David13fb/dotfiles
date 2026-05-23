#include <stdio.h>
#include <stdlib.h>
#include <unistd.h> //cara conseguir los ids
int main(int argc, char *argv[]) {

    int sleepParent = argv[1];
int sleepChild = argv[2];
    //prints  
    printf("pid: %d ",getpid());
    printf("ppid: %d ",getppid());
    printf("pgid: %d ",getpgid(getpid()));
    printf("sid: %d ",getsid(getpid()));
    print("durmiendo %d s",sleepParent);


    
}
