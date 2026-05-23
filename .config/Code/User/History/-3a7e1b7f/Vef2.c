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
        
        // 1. Creamos un número de dispositivo combinado (dev_t)
        dev_t dev_combinado = makedev(unsigned int maj,unsigned int min);

        // 2. Declaramos variables para guardar los números mayor y menor extraídos
        unsigned int mayor_extraido;
        unsigned int menor_extraido;

         // 3. Extraemos los números mayor y menor
        mayor_extraido = major(dev_combinado);
        menor_extraido = minor(dev_combinado);
       //parece que no existe otra manera de sacar el inodo si no es con stat 
       struct stat _stat;
        lstat(argv[1], &_stat);
        //Tipo del fichero
        char _fileType = _stat.st_mode;
        unsigned int _inode = _stat.st_ino;
        //el ultimo acceso al fichero tambien se encuentra en el struct
        unsigned int _atime = _stat.st_atime;

        //salida 
        printf("inodo: " + _inode );
        printf("numero del dispositivo ["++);

    } 
    return 0;
}

