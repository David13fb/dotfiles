#include <stdio.h>
#include <stdlib.h>
#include <fcntl.h>
#include <unistd.h>
#include <string.h>
#include <threads.h>

struct m_ThreadInfo{
     pthread_t thread_id;        /* ID returned by pthread_create() */
     int       thread_num;       /* Application-defined thread # */
     char     *argv_string;      /* From command-line argument */
};

int main(int argc, char *argv[]) {
    
    return 0;
}