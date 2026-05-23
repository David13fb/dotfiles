#include <fcntl.h>
#include <linux/limits.h>
#include <stdio.h>
#include <sys/types.h>
#include <sys/stat.h>
#include <sys/sysmacros.h>
 #include <sys/types.h>
#include <dirent.h>
#include <unistd.h>
#include <string.h>
int main(int argc, char *argv[]) {
   
    //Revisamos que la ruta es un directorio
     struct stat mstat;
        lstat(argv[1],&mstat);
    if (!S_ISDIR(mstat.st_mode)) {
        perror("La ruta no es un directorio valido");
    }
    DIR* dir = opendir(argv[1]);
    struct dirent* aux;
    while ((aux = readdir(dir)) != NULL) {
        //Procesamos caso a caso
        char ruta[PATH_MAX];
        snprintf(ruta,PATH_MAX, "%s%s", argv[1], aux->d_name);
        lstat(ruta,&mstat);
          mode_t permisos = mstat.st_mode;
        //Regular file 
        if(S_ISREG( mstat.st_mode)){
            //Con permisos no fuca porque patata
            if((permisos == S_IXGRP||permisos == S_IXUSR ||permisos == S_IXOTH))printf("%s * \n", aux->d_name);
            //Sin permisos
            else printf("%s  \n", aux->d_name);
        }
        else if(S_ISDIR( mstat.st_mode)){
        printf("%s/  \n", aux->d_name);
        }
        else if(S_ISLNK(mstat.st_mode)){
            //Creamos una variable para guardarnos la path
            char direccion[PATH_MAX];
            ssize_t len = readlink(ruta, direccion, sizeof(direccion));
            printf("%s -> %s  \n", aux->d_name, direccion);
        }
    }
    closedir(dir);
    return 0;
}
