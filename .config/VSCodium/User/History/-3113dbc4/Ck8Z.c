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
           struct thread_info *tinfo = arg;
           char *uargv;

           printf("Thread %d: top of stack near %p; argv_string=%s\n",
                  tinfo->thread_num, (void *) &tinfo, tinfo->argv_string);

           uargv = strdup(tinfo->argv_string);
           if (uargv == NULL)
               handle_error("strdup");

           for (char *p = uargv; *p != '\0'; p++)
               *p = toupper(*p);

           return uargv;
       }

int main(int argc, char *argv[]) {
    
    return 0;
}