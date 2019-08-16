package p017io.fabric.sdk.android.services.concurrency.internal;

import java.util.concurrent.Callable;
import java.util.concurrent.TimeUnit;
import java.util.concurrent.atomic.AtomicReference;

/* renamed from: io.fabric.sdk.android.services.concurrency.internal.RetryFuture */
class RetryFuture<T> extends AbstractFuture<T> implements Runnable {
    private final RetryThreadPoolExecutor executor;
    RetryState retryState;
    private final AtomicReference<Thread> runner = new AtomicReference<>();
    private final Callable<T> task;

    RetryFuture(Callable<T> callable, RetryState retryState2, RetryThreadPoolExecutor retryThreadPoolExecutor) {
        this.task = callable;
        this.retryState = retryState2;
        this.executor = retryThreadPoolExecutor;
    }

    private Backoff getBackoff() {
        return this.retryState.getBackoff();
    }

    private int getRetryCount() {
        return this.retryState.getRetryCount();
    }

    private RetryPolicy getRetryPolicy() {
        return this.retryState.getRetryPolicy();
    }

    /* access modifiers changed from: protected */
    public void interruptTask() {
        Thread thread = (Thread) this.runner.getAndSet(null);
        if (thread != null) {
            thread.interrupt();
        }
    }

    public void run() {
        if (!isDone() && this.runner.compareAndSet(null, Thread.currentThread())) {
            try {
                set(this.task.call());
            } catch (Throwable th) {
                if (getRetryPolicy().shouldRetry(getRetryCount(), th)) {
                    long delayMillis = getBackoff().getDelayMillis(getRetryCount());
                    this.retryState = this.retryState.nextRetryState();
                    this.executor.schedule(this, delayMillis, TimeUnit.MILLISECONDS);
                } else {
                    setException(th);
                }
            } finally {
                this.runner.getAndSet(null);
            }
        }
    }
}
