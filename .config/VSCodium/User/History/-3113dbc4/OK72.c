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
            
            int                 s, opt;
           void                *res;
           size_t              num_threads;
           ssize_t             stack_size;
           pthread_attr_t      attr;
           struct thread_info  *tinfo;

        /* Create one thread for each command-line argument. */

           for (size_t tnum = 0; tnum < num_threads; tnum++) {
               tinfo[tnum].thread_num = tnum + 1;
               tinfo[tnum].argv_string = argv[optind + tnum];

               /* The pthread_create() call stores the thread ID into
                  corresponding element of tinfo[]. */

               s = pthread_create(&tinfo[tnum].thread_id, &attr,
                                  &thread_start, &tinfo[tnum]);
               if (s != 0)
                   handle_error_en(s, "pthread_create");
           }
            /* Destroy the thread attributes object, since it is no
              longer needed. */

           s = pthread_attr_destroy(&attr);
           if (s != 0)
               handle_error_en(s, "pthread_attr_destroy");

           /* Now join with each thread, and display its returned value. */

           for (size_t tnum = 0; tnum < num_threads; tnum++) {
               s = pthread_join(tinfo[tnum].thread_id, &res);
               if (s != 0)
                   handle_error_en(s, "pthread_join");

               printf("Joined with thread %d; returned value was %s\n",
                      tinfo[tnum].thread_num, (char *) res);
               free(res);      /* Free memory allocated by thread */
           }

           free(tinfo);
    return 0;
}