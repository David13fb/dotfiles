#include <filesystem>
#include <stdio.h>
#include <stdlib.h>
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
        char x = ' ';
        if(aux->d_type == DT_REG) x = '*';
        printf("%s %s \n", aux->d_name,x);
    }
    closedir(dir);
    return 0;
}
