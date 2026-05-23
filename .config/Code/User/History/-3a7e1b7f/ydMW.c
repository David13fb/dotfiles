#include <stdio.h>
#include <stdlib.h>

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
        //mayot y menor number se sacan desde la id del dispositivo dev_t
        dev_t dev_id = st.st_rdev;
        unsigned int major_num = major(dev_id);
        unsigned int minor_num = minor(dev_id);
        
    } 
    return 0;
}

