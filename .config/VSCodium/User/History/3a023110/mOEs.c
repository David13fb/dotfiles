#include <stdlib.h>
#include <stdio.h>
#include <sys/stat.h>
#include <sys/sysmacros.h>
#include <unistd.h>



int main(int argc, char *argv[]) {
    //COMPROBAMOS SI SE HA DADO UNA RUTA
    //COMPROBAMOS SI EL ARCHIVO EXISTE
    if (access(argv[1], F_OK) == -1) {
        perror("El archivo existe.\n");
        return 1;
    }
    else{
        struct stat mstat;
        lstat(argv[1],&mstat);

        printf("inodo : $d \n", mstat.st_ino);
        printf("numero de dispositivo [$d] [$d] \n", major(mstat.st_dev));

    }
}