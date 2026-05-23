#include <sched.h>
#include <stdlib.h>
#include <stdio.h>
#include <time.h>
#include <unistd.h>
#include <pthread.h>

struct thread_info {                   /* Used as argument to thread_start() */
           pthread_t thread_id;        /* ID returned by pthread_create() */
           int       thread_num;       /* Application-defined thread # */
       };

static void * thread_exec(void *arg)
       {
        struct thread_info *tinfo = arg;
         printf("Soy thread: %d  comienzo  \n",tinfo->thread_num);
         sleep(tinfo->thread_num);
       
        //   printf("Thread %d: top of stack near %p; argv_string=%s\n",
        //         tinfo->thread_num, (void *) &tinfo, tinfo->argv_string);
        
           return &tinfo->thread_num;
       }
int main(int argc, char *argv[]) {
   int numThreads = ;
   struct thread_info* tinfo;
    tinfo = calloc(numThreads, sizeof(*tinfo));
     /* Create one thread for each command-line argument. */
            int s;       
           // s = pthread_attr_init(PTHREAD_CREATE_JOINABLE);
           for (size_t tnum = 0; tnum < numThreads; tnum++) {
               tinfo[tnum].thread_num = tnum + 1;
               /* The pthread_create() call stores the thread ID into
                  corresponding element of tinfo[]. */

               pthread_create(&tinfo[tnum].thread_id, PTHREAD_CREATE_JOINABLE,
                                  &thread_exec,&tinfo[tnum]);
               }
            for(int i = 0; i< numThreads;i++){

                pthread_join(tinfo[i].thread_id, NULL);
           }
           //sleep(5*numThreads);

           printf("ACABE \n");
    return 0;
}