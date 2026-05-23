#include <sched.h>
#include <stdlib.h>
#include <stdio.h>
#include <time.h>
#include <unistd.h>
#include <pthread.h>

const int BUFFER_SIZE = 10;



struct thread_info {                   /* Used as argument to thread_start() */
           pthread_t thread_id;        /* ID returned by pthread_create() */
           int       thread_num;       /* Application-defined thread # */
       };



typedef struct _buffer { 
    int count; 
    int in; 
    int out; 
    int data[BUFFER_SIZE]; 
} buffer_t;

pthread_mutex_t mutex = PTHREAD_MUTEX_INITIALIZER;
pthread_cond_t cond_lleno = PTHREAD_COND_INITIALIZER;
pthread_cond_t cond_vacio = PTHREAD_COND_INITIALIZER;
buffer_t buffer;
static void * productor(void *arg)
       {

        struct thread_info *tinfo = arg;
        if(buffer.count<BUFFER_SIZE){
             pthread_mutex_lock(&mutex);
            while (buffer.count<BUFFER_SIZE) {
                buffer.data[buffer.out] = tinfo->thread_num;
                buffer.out++;
                buffer.count++;
                if(buffer.count<BUFFER_SIZE){
                    sleep(2);
                }else {
                    pthread_cond_signal(&cond_lleno);
                    pthread_cond_wait(&cond_vacio, &mutex);
                }
            }
        }
         printf("Soy thread: %d  comienzo  \n",tinfo->thread_num);
         sleep(tinfo->thread_num*300);
        pthread_exit(&tinfo->thread_num);
       }



static void * consumidor(void *arg)
       {
        struct thread_info *tinfo = arg;
         printf("Soy thread: %d  comienzo  \n",tinfo->thread_num);
         sleep(tinfo->thread_num*300);
        pthread_exit(&tinfo->thread_num);
       }




int main(int argc, char *argv[]) {
   int numThreadsCondimidor = atoi(argv[1]);
   int numThreadsProductor = atoi(argv[2]);
   struct thread_info* tinfo;
    tinfo = calloc(numThreadsCondimidor +numThreadsProductor, sizeof(*tinfo));
     /* Create one thread for each command-line argument. */
            int s;       
            //Productores
           for (size_t tnum = 0; tnum < numThreadsProductor; tnum++) {
               tinfo[tnum].thread_num = tnum + 1;
               /* The pthread_create() call stores the thread ID intoº
                  corresponding element of tinfo[]. */
               pthread_create(&tinfo[tnum].thread_id, PTHREAD_CREATE_JOINABLE,
                                  &productor,&tinfo[tnum]);
               }
            //Consumidores

            for (size_t tnum = numThreadsProductor; tnum < numThreadsProductor+numThreadsCondimidor; tnum++) {
               tinfo[tnum].thread_num = tnum + 1;
               /* The pthread_create() call stores the thread ID intoº
                  corresponding element of tinfo[]. */
               pthread_create(&tinfo[tnum].thread_id, PTHREAD_CREATE_JOINABLE,
                                  &consumidor,&tinfo[tnum]);
               }
               for (size_t tnum = 0; tnum < numThreadsCondimidor+numThreadsProductor; tnum++) {
                       void* retorno;
                pthread_join(tinfo[tnum].thread_id, &retorno);
                printf("Thread termino %d \n",(int)*(int*)retorno);//(int)*(int*)retorno
               }
                
             
            
           //sleep(5*numThreads);

           printf("ACABE \n");
    return 0;
}