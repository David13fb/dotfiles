#include <stdio.h>
#include <stdlib.h>
#include <unistd.h> //cara conseguir los ids
int main() {
   printf("gid: %d ",getgid());
   printf("pid: %d ",getpid() );
}
