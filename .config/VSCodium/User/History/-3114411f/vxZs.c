#include <fcntl.h>
#include <linux/limits.h>
#include <pthread.h>
#include <stdio.h>
#include <stdlib.h>
#include <sys/mman.h>
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
    char *addr;
    for(int i = 0;i< 5; i++)
    {
        pid = fork();
        if(pid == 0){
            //hijos
           addr[i] = '0'+(char)i;
            exit(i+1);
        }
        else if(i== 0)
            {
            //padre
            int outputfile = open("./output.txt",O_CREAT | O_TRUNC | O_RDWR,0666); //creamos el archivo
            ftruncate(outputfile, 1024);
            addr =mmap(NULL, 1024, PROT_READ| PROT_WRITE,
                       MAP_SHARED, outputfile, 0);
                       addr[0] = '0';
            close(outputfile); 
             
        }
        //Esperamos a que el hijo termine y vamos al sigueinte
         waitpid(pid,&status,0);
         printf("hijo con pid : %d e id %d ha terminado \n",pid,WEXITSTATUS(status));
    }
     if(pid!= 0)
            {
            msync(addr, 1024, MS_SYNC);
            int error = munmap(addr, 1024);
            if(error == -1) perror("munmap faild");
        }
     
    return 0;
}
