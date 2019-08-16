package p017io.fabric.sdk.android.services.concurrency.internal;

import java.util.concurrent.Callable;
import java.util.concurrent.Executors;
import java.util.concurrent.Future;
import java.util.concurrent.ScheduledThreadPoolExecutor;
import java.util.concurrent.ThreadFactory;

/* renamed from: io.fabric.sdk.android.services.concurrency.internal.RetryThreadPoolExecutor */
public class RetryThreadPoolExecutor extends ScheduledThreadPoolExecutor {
    private final Backoff backoff;
    private final RetryPolicy retryPolicy;

    public RetryThreadPoolExecutor(int i, RetryPolicy retryPolicy2, Backoff backoff2) {
        this(i, Executors.defaultThreadFactory(), retryPolicy2, backoff2);
    }

    public RetryThreadPoolExecutor(int i, ThreadFactory threadFactory, RetryPolicy retryPolicy2, Backoff backoff2) {
        super(i, threadFactory);
        if (retryPolicy2 == null) {
            throw new NullPointerException("retry policy must not be null");
        } else if (backoff2 == null) {
            throw new NullPointerException("backoff must not be null");
        } else {
            this.retryPolicy = retryPolicy2;
            this.backoff = backoff2;
        }
    }

    private <T> Future<T> scheduleWithRetryInternal(Callable<T> callable) {
        if (callable == null) {
            throw new NullPointerException();
        }
        RetryFuture retryFuture = new RetryFuture(callable, new RetryState(this.backoff, this.retryPolicy), this);
        execute(retryFuture);
        return retryFuture;
    }

    public Backoff getBackoff() {
        return this.backoff;
    }

    public RetryPolicy getRetryPolicy() {
        return this.retryPolicy;
    }

    public Future<?> scheduleWithRetry(Runnable runnable) {
        return scheduleWithRetryInternal(Executors.callable(runnable));
    }

    public <T> Future<T> scheduleWithRetry(Runnable runnable, T t) {
        return scheduleWithRetryInternal(Executors.callable(runnable, t));
    }

    public <T> Future<T> scheduleWithRetry(Callable<T> callable) {
        return scheduleWithRetryInternal(callable);
    }
}
