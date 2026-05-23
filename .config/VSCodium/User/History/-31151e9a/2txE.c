#include <stdlib.h>
#include <stdio.h>
#include <sys/stat.h>
#include <sys/sysmacros.h>
#include <stdbool.h>
#include <unistd.h>



     
     int buffer[8192];     

int main(int argc, char *argv[]) {
    FILE* inputfile= fopen(argv[1],"r");
    if((inputfile == NULL )&& (argv[1][0] != '-')){ 
        perror("Archivo de entrada invalido"); 
         return 1;
    }
    int blockSize = atoi(argv[3]);
    if(blockSize > 8192) blockSize = 8192;
    int blockCount = atoi(argv[4]);
    int seek = atoi(argv[5]);
    bool inputTerminal = inputfile == NULL;
    bool outputTerminal = argv[2][0] != '-';
    FILE* outputFile = fopen(argv[2],"w+");
    if(outputFile == NULL){ 
        perror("Archivo de entrada invalido"); 
         return 2;
    }
    for(int i = 0; i< blockCount || inputfile->_IO_buf_end;i++){
        char* input;
        if(inputTerminal)
        {
            //Leemos de terminal
            scanf("%s",input);
        }
        else
        {
            read(inputfile, input,blockSize);
        }
        if(outputTerminal)
        {
            //escribimos en teminal
            for(int j = 0; j < blockSize,j++){
                if(input[j] == '\0') break;
              //  buffer[j] = atoi(input[j])

            }

        }
        else
        {

        }


    }


     return 0;
}