package com.crashlytics.android.core;

import android.os.Looper;
import io.fabric.sdk.android.Fabric;
import java.util.concurrent.Callable;
import java.util.concurrent.ExecutorService;
import java.util.concurrent.Future;
import java.util.concurrent.RejectedExecutionException;
import java.util.concurrent.TimeUnit;

class CrashlyticsExecutorServiceWrapper {
    private final ExecutorService executorService;

    public CrashlyticsExecutorServiceWrapper(ExecutorService executorService) {
        this.executorService = executorService;
    }

    Future<?> executeAsync(final Runnable runnable) {
        try {
            return this.executorService.submit(new Runnable() {
                public void run() {
                    try {
                        runnable.run();
                    } catch (Throwable e) {
                        Fabric.getLogger().mo4292e("Fabric", "Failed to execute task.", e);
                    }
                }
            });
        } catch (RejectedExecutionException e) {
            Fabric.getLogger().mo4289d("Fabric", "Executor is shut down because we're handling a fatal crash.");
            return null;
        }
    }

    <T> Future<T> executeAsync(final Callable<T> callable) {
        try {
            return this.executorService.submit(new Callable<T>() {
                public T call() throws Exception {
                    try {
                        return callable.call();
                    } catch (Throwable e) {
                        Fabric.getLogger().mo4292e("Fabric", "Failed to execute task.", e);
                        return null;
                    }
                }
            });
        } catch (RejectedExecutionException e) {
            Fabric.getLogger().mo4289d("Fabric", "Executor is shut down because we're handling a fatal crash.");
            return null;
        }
    }

    <T> T executeSyncLoggingException(Callable<T> callable) {
        try {
            return Looper.getMainLooper() == Looper.myLooper() ? this.executorService.submit(callable).get(4, TimeUnit.SECONDS) : this.executorService.submit(callable).get();
        } catch (RejectedExecutionException e) {
            Fabric.getLogger().mo4289d("Fabric", "Executor is shut down because we're handling a fatal crash.");
            return null;
        } catch (Throwable e2) {
            Fabric.getLogger().mo4292e("Fabric", "Failed to execute task.", e2);
            return null;
        }
    }
}
