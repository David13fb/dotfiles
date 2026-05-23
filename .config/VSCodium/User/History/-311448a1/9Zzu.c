#include <fcntl.h>
#include <stdio.h>
#include <sys/mman.h>
#include <sys/stat.h>
#include <sys/types.h>
#include <unistd.h>
#include <string.h>
int main(int argc, char *argv[]) {
    void* addr;
    printf("MISHEVOS");
    addr = mmap(NULL, 1024, PROT_READ| PROT_WRITE,MAP_PRIVATE,2,0);
    memset(addr, '\0', 1);
    printf("PID: %d  Direcion del segmento %p \n",getpid(),addr);
    sleep(600);
    return 0;
}
