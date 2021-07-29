#pragma once

#ifndef MY_SEMAPHORE_H

#define MY_SEMAPHORE_H

#include <pthread.h>
#include <stdint.h>

typedef struct tagMySemaphore
{
    int32_t cnt;
    int32_t max;
    pthread_mutex_t mtx;
    pthread_cond_t cv;
} MySemaphore;

#ifdef __cplusplus
extern "C" {
#endif

#ifdef MY_SEMAPHORE_ONLY

#define Down(pSema) MySemaphoreDown(pSema)
#define Up(pSema) MySemaphoreUp(pSema)

#endif

int InitMySemaphore(MySemaphore *sema, int32_t initCnt, int32_t maxCnt);

int DestroyMySemaphore(MySemaphore *sema);

int MySemaphoreDown(MySemaphore *sema);

int MySemaphoreUp(MySemaphore *sema);

#ifdef __cplusplus
}
#endif

#endif /* !MY_SEMAPHORE_H */

