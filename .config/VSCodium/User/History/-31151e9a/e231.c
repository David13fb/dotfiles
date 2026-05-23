#include <stdlib.h>
#include <stdio.h>
#include <sys/stat.h>
#include <sys/sysmacros.h>
#include <stdbool.h>
#include <unistd.h>



     
     int buffer[8192];     

int main(int argc, char *argv[]) {
    int inputfile = STDIN_FILENO;
    if((fopen(argv[1],"r")== NULL )&& (argv[1][0] != '-')){ 
        perror("Archivo de entrada invalido"); 
    }
    int blockSize = atoi(argv[3]);
    if(blockSize > 8192) blockSize = 8192;
    int blockCount = atoi(argv[4]);
    int seek = atoi(argv[5]);

    int outputFile = STDOUT_FILENO;
    
    for(int i = 0; i< blockCount;i++){
       


    }


     return 0;
}