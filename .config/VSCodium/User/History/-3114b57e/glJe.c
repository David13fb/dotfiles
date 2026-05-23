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
     perror("fork"); 
     exit(1);
     break;
    case 0:
    //hijo
    printf([hijo])
    default:
    //padre
    return 0;
}
