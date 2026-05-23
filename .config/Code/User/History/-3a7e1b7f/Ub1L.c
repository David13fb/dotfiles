#include <stdio.h>
#include <stdlib.h>

int main(int argc, char *argv[]) {
   
    //Hay ruta 
    if (argc < 2) {
        printf("Error: no se ha dado una ruta.\n");
        return 1;
    }

    
    FILE *archivo = fopen(argv[1], "r");

    // En caso de que el archivo no exista
    if (archivo == NULL) {
        printf("no existe el archivo dado \n");
        return 1;
    return 0;
}

