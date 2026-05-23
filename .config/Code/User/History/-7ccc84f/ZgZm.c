#include <stdio.h>
#include <stdlib.h>
#include <unistd.h> //cara conseguir los ids
int main() {
    printf("pid: %d ",getpid());
    printf("ppid: %d ",getppid());
    printf("pgid: %d ",getpgid());
    printf("sid: %d ",getsid());
}
