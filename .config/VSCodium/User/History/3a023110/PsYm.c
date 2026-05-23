#include <stdlib.h>
#include <stdio.h>
#include <sys/stat.h>
#include <sys/sysmacros.h>
#include <time.h>
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

        printf("inodo : %d \n", mstat.st_ino);
        printf("numero de dispositivo [%d] [%d] \n", major(mstat.st_dev),minor(mstat.st_dev));//le pasamos el id del dispositivo
        if (S_ISDIR(mstat.st_mode)) printf("tipo de fichero: Directorio \n");
        else if (S_ISREG(mstat.st_mode)) printf("tipo de fichero: Regular file \n");
        else if (S_ISLNK(mstat.st_mode)) printf("tipo de fichero: Es un enlace \n");
        printf("Ultimo cambio de estado : %s ", ctime(&mstat.st_mtim.tv_sec));
        printf("creacion del archivo : %s ", ctime(&mstat.st_ctim.tv_sec));
       
    }
     return 0;
}