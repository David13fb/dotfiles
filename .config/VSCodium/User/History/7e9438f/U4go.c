#include <sched.h>
#include <stdlib.h>
#include <stdio.h>
#include <time.h>
#include <unistd.h>
#include <pthread.h>



struct thread_info {                   /* Used as argument to thread_start() */
           pthread_t thread_id;        /* ID returned by pthread_create() */
           int       thread_num;       /* Application-defined thread # */
           int*      total;
       };

       pthread_mutex_t mutex;

static void * thread_exec(void *arg)
       {
        struct thread_info *tinfo = arg;
        pthread_mutex_lock(&mutex);
         printf("Soy thread: %d  comienzo  \n",tinfo->thread_num);
         *tinfo->total +=1;
            pthread_mutex_unlock(&mutex);
            printf("Sueto el mutex valor de total %d", &tinfo->total);
        pthread_exit(&tinfo->thread_num);
       }


int main(int argc, char *argv[]) {
   int numThreads = atoi(argv[1]);
   struct thread_info* tinfo;
    pthread_mutex_init(&mutex,NULL);
        int total = 0;


    tinfo = calloc(numThreads, sizeof(*tinfo));
     /* Create one thread for each command-line argument. */
            int s;       
           // s = pthread_attr_init(PTHREAD_CREATE_JOINABLE);
           for (size_t tnum = 0; tnum < numThreads; tnum++) {
               tinfo[tnum].thread_num = tnum + 1;
               tinfo[tnum].total = &total;
               /* The pthread_create() call stores the thread ID intoº
                  corresponding element of tinfo[]. */

               pthread_create(&tinfo[tnum].thread_id, PTHREAD_CREATE_JOINABLE,
                                  &thread_exec,&tinfo[tnum]);
               }
               for (size_t tnum = 0; tnum < numThreads; tnum++) {
                       void* retorno;
                pthread_join(tinfo[tnum].thread_id, &retorno);
                //printf("Thread termino %d \n",(int)*(int*)retorno);//(int)*(int*)retorno
               }
                
                pthread_mutex_destroy(&mutex);
            printf("TOTAL FINAL %d",total);
           //sleep(5*numThreads);

           printf("ACABE \n");
    return 0;
}