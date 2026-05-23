#include <fcntl.h>
#include <linux/limits.h>
#include <stdio.h>
#include <stdlib.h>
#include <sys/types.h>
#include <sys/stat.h>
#include <sys/sysmacros.h>
 #include <sys/types.h>
#include <dirent.h>
#include <unistd.h>
#include <string.h>
int main(int argc, char *argv[]) {
   
    if(argc < 2){ 
        perror("falta programa a ejecutar como hijo");
        exit(-1);
    }

    return 0;
}
