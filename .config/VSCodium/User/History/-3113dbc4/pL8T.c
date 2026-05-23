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

static void *
       thread_start(void *arg)
       {
           struct m_ThreadInfo *tinfo = arg;
           char *uargv;

           printf("Thread %d: top of stack near %p; argv_string=%s\n",
                  tinfo->thread_num, (void *) &tinfo, tinfo->argv_string);
           uargv = strdup(tinfo->argv_string);
           return uargv;
       }

int main(int argc, char *argv[]) {
    
    return 0;
}