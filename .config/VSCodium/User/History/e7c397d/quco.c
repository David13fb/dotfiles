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
            //printf("soy el hijo %d  y escribo \n",i+1);
            for(int j= 0;j< 5; j++){
            fprintf(outputfile,"%d",i+1);
            }
            fprintf(outputfile,"\n");
            // no hace falta cerrar el archivo ya que exit() se ocupa de ello
            exit(i+1);
        }
        else if(i== 0)
            {
            //padre
            FILE *outputfile = fopen("./output.txt","w"); //creamos el archivo
            for(int j= 0;j< 5; j++){
            fprintf(outputfile,"%d",0);
            }
            fprintf(outputfile,"\n");
            fclose(outputfile); 
             
        }
        //Esperamos a que el hijo termine y vamos al sigueinte
         waitpid(pid,&status,0);
         printf("hijo con pid : %d e id %d ha terminado \n",pid,WEXITSTATUS(status));
    }
        
     
    return 0;
}
