#include <stdio.h>
#include <stdlib.h>
#include <libc.h>
#include <sys/sysmacros.h>


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
    }
    else {

        dev_t makedev(unsigned int maj, unsigned int min);
       unsigned int major(dev_t dev);
       unsigned int minor(dev_t dev);
    } 
    return 0;
}

