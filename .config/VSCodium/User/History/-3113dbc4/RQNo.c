#include <ctype.h>
       #include <errno.h>
       #include <pthread.h>
       #include <stdio.h>
       #include <stdlib.h>
       #include <string.h>
       #include <sys/types.h>
       #include <unistd.h>

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
            printf("soy el hilo %d \n",tinfo->thread_num);
          sleep(tinfo->thread_num);
           return uargv;
       }

int main(int argc, char *argv[]) {
            
    int                 s, opt;
    void                *res;
    int              num_threads = 5;
    struct m_ThreadInfo  *tinfo;
    tinfo = calloc(num_threads, sizeof(*tinfo));
        /* Create one thread for each command-line argument. */

           for (size_t tnum = 0; tnum < num_threads; tnum++) {
               tinfo[tnum].thread_num = tnum + 1;
               s = pthread_create(&tinfo[tnum].thread_id, PTHREAD_CREATE_JOINABLE,
                                  &thread_start, &tinfo[tnum]);
               if (s != 0)perror("PROBLEMA AL CREAR");
                  // handle_error_en(s, "pthread_create");
           }
            
           for (size_t tnum = 0; tnum < num_threads; tnum++) {
               s = pthread_join(tinfo[tnum].thread_id, &res);
               if (s != 0)perror("PROBLEMA AL REALIZAR EL JOIN");
                 //  handle_error_en(s, "pthread_join");

               printf("Joined with thread %d; returned value was %s\n",
                      tinfo[tnum].thread_num, (char *) res);
           }
    return 0;
}