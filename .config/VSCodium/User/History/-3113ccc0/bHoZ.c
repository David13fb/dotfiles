#include <sched.h>
#include <stdlib.h>
#include <stdio.h>
#include <time.h>
#include <unistd.h>
#include <pthread.h>
#include <stdbool.h>
const int BUFFER_SIZE = 10;



struct thread_info {                   /* Used as argument to thread_start() */
           pthread_t thread_id;        /* ID returned by pthread_create() */
           int       thread_num;       /* Application-defined thread # */
       };



typedef struct _buffer { 
    int count; 
    int in; 
    int out; 
    int data[10]; 
} buffer_t;

pthread_mutex_t mutex = PTHREAD_MUTEX_INITIALIZER;
pthread_cond_t cond_lleno = PTHREAD_COND_INITIALIZER;
pthread_cond_t cond_vacio = PTHREAD_COND_INITIALIZER;
buffer_t buffer;
static void * productor(void *arg)
       {

        struct thread_info *tinfo = arg;
        int prod = 10;
     
             pthread_mutex_lock(&mutex);
            while (prod>0) {
                if(buffer.in >= BUFFER_SIZE) buffer.in =0;
                buffer.data[buffer.in] = tinfo->thread_num;
                buffer.in++;

                buffer.count++;
                prod--;
                if(buffer.count<BUFFER_SIZE){
                    sleep(2);
                }else if (prod>0) 
                {
                    pthread_cond_signal(&cond_lleno);
                    pthread_cond_wait(&cond_vacio, &mutex);
                }
            
            pthread_mutex_unlock(&mutex);
        }
         
        pthread_exit(&tinfo->thread_num);
       }



static void * consumidor(void *arg)
       {
       
        struct thread_info *tinfo = arg;
        bool pildora = false;
       
             pthread_mutex_lock(&mutex);
            while (!pildora) {
                if(buffer.count>0){
                    sleep(1);
                }else if(!pildora) {
                    pthread_cond_signal(&cond_vacio);
                    pthread_cond_wait(&cond_lleno, &mutex);
                }

                if(buffer.out == BUFFER_SIZE) buffer.out =0;
                if(buffer.data[buffer.out]) pildora = true;
                printf("consumo valor %d, in = %d out = %d \n",buffer.data[buffer.out],buffer.in,buffer.out); 
                buffer.out++;
                buffer.count--;
                
                
        
            pthread_mutex_unlock(&mutex);
        }
         
        pthread_exit(&tinfo->thread_num);
       }




int main(int argc, char *argv[]) {
   int numThreadsCondimidor = atoi(argv[1]);
   int numThreadsProductor = atoi(argv[2]);
   struct thread_info* tinfoCon;
   struct thread_info* tinfoPro;
    tinfoPro = calloc(numThreadsProductor, sizeof(*tinfoPro));
    tinfoCon = calloc(numThreadsCondimidor , sizeof(*tinfoCon));
     /* Create one thread for each command-line argument. */
            int s;       
            //Productores
           for (size_t tnum = 0; tnum < numThreadsProductor; tnum++) {
               tinfoPro[tnum].thread_num = tnum + 1;
               /* The pthread_create() call stores the thread ID intoº
                  corresponding element of tinfo[]. */
               pthread_create(&tinfoPro[tnum].thread_id, PTHREAD_CREATE_JOINABLE,
                                  &productor,&tinfoPro[tnum]);
               }
            //Consumidores

            for (size_t tnum = 0; tnum < numThreadsCondimidor; tnum++) {
               tinfoCon[tnum].thread_num = tnum + 1;
               /* The pthread_create() call stores the thread ID intoº
                  corresponding element of tinfo[]. */
               pthread_create(&tinfo[tnum].thread_id, PTHREAD_CREATE_JOINABLE,
                                  &consumidor,&tinfoCon[tnum]);
               }
               //productores
               for (size_t tnum = 0; tnum < numThreadsProductor; tnum++) {
                       void* retorno;
                pthread_join(tinfoPro[tnum].thread_id, &retorno);
                printf("Thread termino %d \n",(int)*(int*)retorno);//(int)*(int*)retorno
               }
               for(int i = 0; i<numThreadsCondimidor;i++){
                if(buffer.in >= BUFFER_SIZE) buffer.in =0;
                buffer.data[buffer.in] = -1;
                buffer.in++;
               }

               for (size_t tnum = 0; tnum < numThreadsCondimidor+numThreadsProductor; tnum++) {
                       void* retorno;
                pthread_join(tinfoCon[tnum].thread_id, &retorno);
                printf("Thread termino %d \n",(int)*(int*)retorno);//(int)*(int*)retorno
               }
                
             
            
           //sleep(5*numThreads);

           printf("ACABE \n");
    return 0;
}