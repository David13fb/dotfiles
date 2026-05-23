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

int main(int argc, char *argv[]) {
   
    pid_t pid;
    FILE *outputfile;
    int status;
    for(int i = 0;i< 5; i++)
    {
        pid = fork();
        if(pid == 0){
            //hijos
           int outputfile = open("./output.txt",O_WRONLY); //abrimos el archivo
            char cadena[5];
            sprintf(cadena, "%d%d%d%d%d",i,i,i,i,i);
            lseek(outputfile, 5*i, SEEK_SET);
            write(outputfile, cadena, 5);
            
            exit(i+1);
        }
        else if(i== 0)
            {
            //padre
            int outputfile = open("./output.txt",O_CREAT | O_TRUNC | O_WRONLY,0666); //creamos el archivo
            ftruncate(outputfile, 5*5);
            char cadena[5];
            sprintf(cadena, "00000");
            write(outputfile, cadena, 5);
            close(outputfile); 
             
        }
        //Esperamos a que el hijo termine y vamos al sigueinte
         waitpid(pid,&status,0);
         printf("hijo con pid : %d e id %d ha terminado \n",pid,WEXITSTATUS(status));
    }
        
     
    return 0;
}
