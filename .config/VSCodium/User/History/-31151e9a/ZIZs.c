#include <stdio.h>
#include <stdlib.h>
#include <fcntl.h>
#include <unistd.h>
#include <string.h>

     
int MAX_BUFFER = 8192;

int main(int argc, char *argv[]) 
{
   char *input_path = argv[1];
    char *output_path = argv[2];
    int block_size = atoi(argv[3]);
    int block_count = atoi(argv[4]);
    int seek_blocks = atoi(argv[5]);

    // Ajustar tamaño de bloque al buffer estático
    if (block_size > MAX_BUFFER) block_size = MAX_BUFFER;
    static char buffer[MAX_BUFFER];

    // 1. Abrir archivo de entrada
    int fd_in = STDIN_FILENO;
    if (strcmp(input_path, "-") != 0) {
        if ((fd_in = open(input_path, O_RDONLY)) < 0) {
            perror("Error abriendo entrada");
            exit(EXIT_FAILURE);
        }
    }

    // 2. Abrir archivo de salida (rw-rw-r-- = 0664)
    int fd_out = STDOUT_FILENO;
    if (strcmp(output_path, "-") != 0) {
        fd_out = open(output_path, O_WRONLY | O_CREAT | O_TRUNC, 0664);
        if (fd_out < 0) {
            perror("Error abriendo salida");
            exit(EXIT_FAILURE);
        }
    }

    // 3. Aplicar seek en la salida
    if (seek_blocks > 0) {
        if (lseek(fd_out, (off_t)seek_blocks * block_size, SEEK_SET) < 0) {
            perror("Error en lseek");
            // Nota: lseek falla en pipes/stdout, dd normalmente maneja esto 
            // escribiendo bloques vacíos, pero aquí simplificamos con lseek.
        }
    }

    // 4. Bucle principal de copia
    int blocks_copied = 0;
    while (blocks_copied < block_count) {
        ssize_t bytes_to_read = block_size;
        ssize_t total_read = 0;
        
        // Leer un bloque completo (manejando lecturas parciales)
        while (total_read < bytes_to_read) {
            ssize_t n_read = read(fd_in, buffer + total_read, bytes_to_read - total_read);
            if (n_read == 0) goto end_copy; // Fin de archivo (EOF)
            if (n_read < 0) {
                perror("Error en read");
                exit(EXIT_FAILURE);
            }
            total_read += n_read;
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
        blocks_copied++;
    }

end_copy:
    if (fd_in != STDIN_FILENO) close(fd_in);
    if (fd_out != STDOUT_FILENO) close(fd_out);

    return 0;
}