#include <stdlib.h>
#include <stdio.h>
#include <unistd.h>



int main(int argc, char *argv[]) {
    
    int i = execvp(argv[1],argv[2]);

   printf("Hijo acabo con codigo de error $i \n",i);
    return 0;
}