#include <stdlib.h>
#include <stdio.h>
#include <unistd.h>



int main(int argc, char *argv[]) {
    //COMPROBAMOS SI EL ARCHIVO EXISTE
    if (access(argv[1], F_OK) == -1) {
        printf("El archivo existe.\n");
        return 1;
    }
    else{

    }
}