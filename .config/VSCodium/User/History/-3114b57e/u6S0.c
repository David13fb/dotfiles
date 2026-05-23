#include <fcntl.h>
#include <linux/limits.h>
#include <stdio.h>
#include <stdlib.h>
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
    printf("[HIJO] PID=%d, PPID=%d, PGID=%d, SID=%d. Durmiendo %ss \n",getpid(),getppid(),getpgid(getpid()),getsid(getpid()),argv[2]);
    sleep(atoi(argv[2]));
    printf("Hijo termina \n");
    case 1:
    //padre
    printf("[Padre] PID=%d, PPID=%d, PGID=%d, SID=%d. Durmiendo %ss \n ",getpid(),getppid(),getpgid(getpid()),getsid(getpid()),argv[1]);
    sleep(atoi(argv[1]));
    printf("Padre Termina \n");
    }
    return 0;
}
