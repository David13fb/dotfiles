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
        pthread_exit(&tinfo->thread_num);
       }
int main(int argc, char *argv[]) {
   int numThreads = atoi(argv[1]);
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
                void* retorno;
                pthread_join(tinfo[i].thread_id, &retorno);
                printf("Thread %d \n", (int*)retorno);
           }
           //sleep(5*numThreads);

           printf("ACABE \n");
    return 0;
}