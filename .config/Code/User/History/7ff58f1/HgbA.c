#include <stdio.h>
#include <stdlib.h>

int main(int argc, char *argv[]) {
   
    FILE* intputdata = fopen(argv[0],"r");   
    printf(intputdata); 
    return 0;
}
