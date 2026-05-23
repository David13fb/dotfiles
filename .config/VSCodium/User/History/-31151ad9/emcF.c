#include <fcntl.h>
#include <stdio.h>
#include <sys/types.h>
#include <sys/stat.h>
#include <sys/sysmacros.h>
 #include <sys/types.h>
#include <dirent.h>
#include <unistd.h>

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
        lstat(aux->d_name,&mstat);
          mode_t permisos = mstat.st_mode;
          
        //Regular file con permiso de ejecucion
        if((permisos == S_IXGRP||permisos == S_IXUSR ||permisos == S_IXOTH))printf("%s * \n", aux->d_name);
        else printf("%s  \n", aux->d_name);
    }
    closedir(dir);
    return 0;
}
