#include <fcntl.h>
#include <linux/limits.h>
#include <pthread.h>
#include <stdio.h>
#include <sys/types.h>
#include <sys/stat.h>
#include <sys/sysmacros.h>
 #include <sys/types.h>
#include <dirent.h>
#include <unistd.h>
#include <sys/wait.h>

int main(int argc, char *argv[]) {
   
    if(argc < 2){ 
        perror("falta programa a ejecutar como hijo");
        return -1;
    }
    pid_t aux = execvp(argv[1],&argv[1]);
    if(aux == -1){
         perror("error al crear el programa hijo");
        return -1;
    }
    int status;
    waitpid(-aux,&status,0);
    printf("El hijo temino con codigo de salida %d \n",status);
    return 0;
}
