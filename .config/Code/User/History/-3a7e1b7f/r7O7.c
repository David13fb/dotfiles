#include <stdio.h>
#include <stdlib.h>
#include <sys/types.h>
#include <sys/stat.h>
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
        //Codigo sacado de man 3 major
        dev_t makedev(unsigned int maj, unsigned int min);
       unsigned int major(dev_t dev);
       unsigned int minor(dev_t dev);
        struct stat _stat;
        stat(argv[1], &_stat);
        unsigned int _inode

    } 
    return 0;
}

