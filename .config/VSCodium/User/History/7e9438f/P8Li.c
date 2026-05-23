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

static void * thread_exec(void *arg)
       {
        struct thread_info *tinfo = arg;

         printf("Soy thread: %d  comienzo  \n",tinfo->thread_num);
         sleep(tinfo->thread_num*300);
        pthread_exit(&tinfo->thread_num);
       }

pthread_mutex_t mutex = PTHREAD_MUTEX_INITIALIZER;

int main(int argc, char *argv[]) {
   int numThreads = atoi(argv[1]);
   struct thread_info* tinfo;
    int pthread_mutex_init(pthread_mutex_t *mutex,
                       const pthread_mutexattr_t *attr);



    tinfo = calloc(numThreads, sizeof(*tinfo));
     /* Create one thread for each command-line argument. */
            int s;       
           // s = pthread_attr_init(PTHREAD_CREATE_JOINABLE);
           for (size_t tnum = 0; tnum < numThreads; tnum++) {
               tinfo[tnum].thread_num = tnum + 1;
               /* The pthread_create() call stores the thread ID intoº
                  corresponding element of tinfo[]. */

               pthread_create(&tinfo[tnum].thread_id, PTHREAD_CREATE_JOINABLE,
                                  &thread_exec,&tinfo[tnum]);
               }
               for (size_t tnum = 0; tnum < numThreads; tnum++) {
                       void* retorno;
                pthread_join(tinfo[tnum].thread_id, &retorno);
                printf("Thread termino %d \n",(int)*(int*)retorno);//(int)*(int*)retorno
               }
                
                pthread_mutex_destroy(&mutex);
            
           //sleep(5*numThreads);

           printf("ACABE \n");
    return 0;
}