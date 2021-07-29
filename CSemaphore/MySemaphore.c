#include "MySemaphore.h"

int InitMySemaphore(MySemaphore *sema, int32_t initCnt, int32_t maxCnt)
{
    int ret = 0;

    if (initCnt < 0 || maxCnt <= 0 || initCnt > maxCnt)
    {
        return -1;
    }

    sema->cnt = initCnt;
    sema->max = maxCnt;
    ret |= pthread_mutex_init(&sema->mtx, NULL);
    ret |= pthread_cond_init(&sema->cv, NULL);

    return ret;
}

int DestroyMySemaphore(MySemaphore *sema)
{
    int ret = 0;
    ret |= pthread_cond_destroy(&sema->cv);
    ret |= pthread_mutex_destroy(&sema->mtx);
    return ret;
}

int MySemaphoreDown(MySemaphore *sema)
{
    int ret = 0;
    ret = pthread_mutex_lock(&sema->mtx);
    if (ret != 0) return ret;

    while (sema->cnt == 0)
    {
        pthread_cond_wait(&sema->cv, &sema->mtx);
    }
    --sema->cnt;

    return pthread_mutex_unlock(&sema->mtx);
}

int MySemaphoreUp(MySemaphore *sema)
{
    int ret = 0;
    ret = pthread_mutex_lock(&sema->mtx);

    if (sema->cnt == sema->max)
    {
        ret = -1;
        goto exit;
    }
    ++sema->cnt;
    pthread_cond_signal(&sema->cv);

exit:
    ret |= pthread_mutex_unlock(&sema->mtx);
    return ret;
}
