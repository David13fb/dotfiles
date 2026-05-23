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
        //parece que no existe otra manera de sacar el inodo si no es con stat 
       struct stat _stat;
        lstat(argv[1], &_stat);
        unsigned int _mayor = major(_stat.st_rdev);
         unsigned int _minor= minor(_stat.st_rdev);
       
        //Tipo del fichero
        __MODE_T_TYPE _fileType = _stat.st_mode;
        unsigned int _inode = _stat.st_ino;
        //el ultimo acceso al fichero tambien se encuentra en el struct
        __TIME_T_TYPE _atime = _stat.st_atime;

        //salida 
        printf("inodo: %d \n", _inode );
        printf("numero del dispositivo [%d,%d]\n",_mayor,_minor);
        printf("tipo de archivo: %d \n",_fileType);
        printf("ultimo acceso: %d \n",_atime);

    } 
    return 0;
}

