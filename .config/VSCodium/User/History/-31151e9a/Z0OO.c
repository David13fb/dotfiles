#include <stdio.h>
#include <stdlib.h>
#include <fcntl.h>
#include <unistd.h>
#include <string.h>


     
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
    
    if (argv[2][0] != '-') {
        outputFile = open(argv[2],0664);
        if (fd_out < 0) {
            perror("Error abriendo salida");
            exit(EXIT_FAILURE);
        }
    for(int i = 0; i< blockCount;i++){
       


    }


     return 0;
}