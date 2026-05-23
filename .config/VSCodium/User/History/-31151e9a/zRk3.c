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
        outputFile = open(argv[2], O_WRONLY | O_CREAT | O_TRUNC, 0664);
        if (outputFile < 0) {
            perror("Error abriendo salida");
        }

     if (seek > 0) {
        if (lseek(outputFile, (off_t)seek * blockSize, SEEK_SET) < 0) {
            perror("Error en lseek");
        }
    }
    for(int i = 0; i< blockCount;i++){
       


    }


     return 0;
}