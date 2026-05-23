#include <fcntl.h>
#include <linux/limits.h>
#include <stdio.h>
#include <sys/types.h>
#include <sys/stat.h>
#include <sys/sysmacros.h>
 #include <sys/types.h>
#include <dirent.h>
#include <unistd.h>

int main(int argc, char *argv[]) {
   pid_t pid = fork(); 
   switch (pid) { 
    case -1:
     perror("fork failed"); 
     break;
    case 0:
    //hijo
    printf("[HIJO]");
    default:
    //padre
    printf("[Padre]");
    return 0;
}
