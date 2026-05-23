#include <stdio.h>
#include <stdlib.h>
#include <unistd.h> //cara conseguir los ids
int main() {

    printf("pid: %d \n",getpid());
    printf("ppid: %d \n",getppid());
    printf("pgid: %d \n",getpgid(getpid()));
    printf("sid: %d \n",getsid(getpid()));
    printf("user ID: %d \n",getsid(geteuid()));
    char* aux;
    getcwd(aux,0);
    printf("actual directory: %d \n",aux);
    
}
