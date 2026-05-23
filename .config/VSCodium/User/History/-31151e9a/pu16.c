#include <stdio.h>
#include <stdlib.h>
#include <fcntl.h>
#include <unistd.h>
#include <string.h>

     
       

int main(int argc, char *argv[]) 
{
    int inputfile = STDIN_FILENO;
    if((fopen(argv[1],"r")== NULL )&& (argv[1][0] != '-'))
    { 
        perror("Archivo de entrada invalido"); 
    }
    int blockSize = atoi(argv[3]);
    if(blockSize > 8192) blockSize = 8192;
    static char buffer[8192];   
    int blockCount = atoi(argv[4]);
    int seek = atoi(argv[5]);

    int outputFile = STDOUT_FILENO;
    
    if (argv[2][0] != '-') 
    {
        outputFile = open(argv[2], O_WRONLY | O_CREAT | O_TRUNC, 0664);
        if (outputFile < 0) {
            perror("Error abriendo salida");
        }

     if (seek > 0) {
        if (lseek(outputFile, (off_t)seek * blockSize, SEEK_SET) < 0) {
            perror("Error en lseek");
        }
    }
    

    ssize_t total_read = 0;
        for(int i = 0; i< blockCount;i++)
        {
        
            int j = 0;
            while( j< blockSize)
            {
                int n_read = read(inputfile, buffer + j, blockSize - j);
                if (n_read == 0) break;
                if (n_read < 0) 
                {
                    perror("Error en read");
                }
                j += n_read;
                total_read = j;
            }
        }
        // Escribir el bloque completo (manejando escrituras parciales)
        ssize_t total_written = 0;
        while (total_written < total_read) {
            ssize_t n_write = write(fd_out, buffer + total_written, total_read - total_written);
            if (n_write < 0) {
                perror("Error en write");
                exit(EXIT_FAILURE);
            }
            total_written += n_write;
        }
        blocks++;
    }

     return 0;
    }
     
