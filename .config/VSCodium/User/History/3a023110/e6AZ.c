#include <stdlib.h>
#include <stdio.h>
#include <unistd.h>



int main(int argc, char *argv[]) {
    if (access(argv[1], F_OK) == 0) {
        printf("El archivo existe.\n");
        return 1;
}