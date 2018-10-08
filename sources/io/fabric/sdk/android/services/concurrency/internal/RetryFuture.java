package io.fabric.sdk.android.services.concurrency.internal;

import java.util.concurrent.Callable;
import java.util.concurrent.atomic.AtomicReference;

class RetryFuture<T> extends AbstractFuture<T> implements Runnable {
    private final RetryThreadPoolExecutor executor;
    RetryState retryState;
    private final AtomicReference<Thread> runner = new AtomicReference();
    private final Callable<T> task;

    RetryFuture(Callable<T> callable, RetryState retryState, RetryThreadPoolExecutor retryThreadPoolExecutor) {
        this.task = callable;
        this.retryState = retryState;
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

    protected void interruptTask() {
        Thread thread = (Thread) this.runner.getAndSet(null);
        if (thread != null) {
            thread.interrupt();
        }
    }

    public void run() {
        if (!isDone() && this.runner.compareAndSet(null, Thread.currentThread())) {
            try {
                set(this.task.call());
                this.runner.getAndSet(null);
            } catch (Throwable th) {
                this.runner.getAndSet(null);
            }
        }
    }
}
