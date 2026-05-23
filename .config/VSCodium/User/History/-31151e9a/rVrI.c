#include <stdio.h>
#include <stdlib.h>
#include <fcntl.h>
#include <unistd.h>
#include <string.h>

int main(int argc, char *argv[]) {
    if (argc < 6) return 1;

    // CORRECCIÓN 1: Abrir el archivo correctamente con open()
    int inputfile = STDIN_FILENO;
    if (strcmp(argv[1], "-") != 0) {
        inputfile = open(argv[1], O_RDONLY);
        if (inputfile < 0) {
            perror("Error abriendo entrada");
            exit(1);
        }
    }
    //Nos guardamos los sizes y numero de bloques
    int blockSize = atoi(argv[3]);
    if(blockSize > 8192) blockSize = 8192;
    static char buffer[8192];   
    int blockCount = atoi(argv[4]);
    int seek = atoi(argv[5]);

    //Preparamos el output
    int outputFile = STDOUT_FILENO;
    if (strcmp(argv[2], "-") != 0) {
        outputFile = open(argv[2], O_WRONLY |O_CREAT | O_TRUNC, 0664);
        if (outputFile < 0) {
            perror("Error abriendo salida");
            exit(1);
        }
        // Aplicar el lseek solo si no es salida estándar
        if (seek > 0) {
            lseek(outputFile, (off_t)seek * blockSize, SEEK_SET);
        }
    }

    int blocks = 0;
    for(int i = 0; i < blockCount; i++) {
        int bytes_leidos_en_este_bloque = 0;
        
        // Leer un bloque completo
        while(bytes_leidos_en_este_bloque < blockSize) {
            int n_read = read(inputfile, buffer + bytes_leidos_en_este_bloque, blockSize - bytes_leidos_en_este_bloque);
            
            if (n_read == 0) bytes_leidos_en_este_bloque = blockSize; 
            if (n_read < 0) { perror("read"); exit(1); }
            
            bytes_leidos_en_este_bloque += n_read;
        }

        // CORRECCIÓN 2: El write DEBE estar dentro del bucle de bloques
        int total_written = 0;
        while (total_written < bytes_leidos_en_este_bloque) {
            int n_write = write(outputFile, buffer + total_written, bytes_leidos_en_este_bloque - total_written);
            if (n_write < 0) { perror("write"); exit(1); }
            total_written += n_write;
        }
        blocks++;
    }

    printf("BLOQUES COPIADOS: %d\n", blocks);
    if (inputfile != STDIN_FILENO) close(inputfile);
    if (outputFile != STDOUT_FILENO) close(outputFile);
    return 0;
}