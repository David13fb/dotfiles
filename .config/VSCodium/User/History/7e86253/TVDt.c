#include <stdlib.h>
#include <stdio.h>
#include <unistd.h>



int main(int argc, char *argv[]) {
    
     pid_t pid = fork();
     if(pid == 0){
        printf("[HIJO]\n");
        printf("pid: %d \n",getpid());
        printf("ppid: %d \n",getppid());
        printf("pgid: %d \n",getpgid(getpid()));
        printf("sid: %d \n",getsid(getpid()));
        printf("user ID: %d \n",getuid());
        printf("group ID: %d \n",getgid());
        printf("actual directory: %s \n",getcwd(NULL,0));
        sleep((int)argv[2]);
         printf("Hijo terminado\n");
     }
     if(pid>0){
        printf("[PADRE]\n");
        printf("pid: %d \n",getpid());
        printf("ppid: %d \n",getppid());
        printf("pgid: %d \n",getpgid(getpid()));
        printf("sid: %d \n",getsid(getpid()));
        printf("user ID: %d \n",getuid());
        printf("group ID: %d \n",getgid());
        printf("actual directory: %s \n",getcwd(NULL,0));
        sleep((int)argv[1]);
        printf("Padre terminado\n");
     }

   
    return 0;
}