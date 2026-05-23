#include <stdlib.h>
#include <stdio.h>
#include <unistd.h>



int main(int argc, char *argv[]) {
    if (access(argv[1], F_OK) == 0) {
        printf("El archivo '%s' existe.\n", ruta_archivo);
}