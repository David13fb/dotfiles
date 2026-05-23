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
   

    //primero creamos un proceso hijo para sustituir por el que nos dicen
    pid_t pid = fork();

    if(pid<0){
        perror("El fork ha fallado");
        return  -1;
    }
    else if(pid == 0){
        //En el hijo
        if(argc < 2){ 
        perror("falta programa a ejecutar como hijo");
        return -1;
    }
    //execvp coge el proceso actual y lo sustituye por el indicado
    pid_t aux = execvp(argv[1],&argv[1]);
    if(aux == -1){
         perror("error al crear el programa hijo");
        return -1;
    }
    }
    else {
        //proceso padre
    int status;
    //Esperamos a que el proceso termine
    if (waitpid(pid, &status, 0) == -1) {
            perror("Error en waitpid");
            return -1;
        }
    printf("El hijo temino con codigo de salida %d \n", WEXITSTATUS(status));
    }
    return 0;
}
