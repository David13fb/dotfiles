#include <stdlib.h>
#include <stdio.h>
#include <unistd.h>



int main(int argc, char *argv[]) {
    
     pid_t pid = fork();
     if(pid == 0){
        printf("Soy el hijo");
     }


    return 0;
}