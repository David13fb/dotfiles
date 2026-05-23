#include <stdlib.h>
#include <stdio.h>
#include <sys/stat.h>
#include <unistd.h>



int main(int argc, char *argv[]) {
    //COMPROBAMOS SI EL ARCHIVO EXISTE
    if (access(argv[1], F_OK) == -1) {
        perror("El archivo existe.\n");
        return 1;
    }
    else{
        struct stat mstat;
        lstat(argv[1],&mstat);

        printf("inodo : $d \n", mstat.st_ino);
        printf("inodo : $d \n", mstat.st_ino);

    }
}