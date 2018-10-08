package com.appsflyer;

import android.os.AsyncTask;
import android.os.Build.VERSION;
import java.util.concurrent.Executor;
import java.util.concurrent.ExecutorService;
import java.util.concurrent.Executors;
import java.util.concurrent.ScheduledExecutorService;
import java.util.concurrent.ScheduledThreadPoolExecutor;
import java.util.concurrent.ThreadPoolExecutor;
import java.util.concurrent.TimeUnit;

public class AFExecutor {
    /* renamed from: ˏ */
    private static AFExecutor f111;
    /* renamed from: ˊ */
    private ScheduledExecutorService f112;
    /* renamed from: ˎ */
    private Executor f113;
    /* renamed from: ॱ */
    private Executor f114;

    private AFExecutor() {
    }

    public static AFExecutor getInstance() {
        if (f111 == null) {
            f111 = new AFExecutor();
        }
        return f111;
    }

    public Executor getSerialExecutor() {
        if (this.f113 == null) {
            if (VERSION.SDK_INT < 11) {
                return Executors.newSingleThreadExecutor();
            }
            this.f113 = AsyncTask.SERIAL_EXECUTOR;
        }
        return this.f113;
    }

    public Executor getThreadPoolExecutor() {
        Object obj = (this.f114 == null || ((this.f114 instanceof ThreadPoolExecutor) && (((ThreadPoolExecutor) this.f114).isShutdown() || ((ThreadPoolExecutor) this.f114).isTerminated() || ((ThreadPoolExecutor) this.f114).isTerminating()))) ? 1 : null;
        if (obj != null) {
            if (VERSION.SDK_INT < 11) {
                return Executors.newSingleThreadExecutor();
            }
            this.f114 = Executors.newFixedThreadPool(2);
        }
        return this.f114;
    }

    /* renamed from: ॱ */
    final ScheduledThreadPoolExecutor m179() {
        Object obj = (this.f112 == null || this.f112.isShutdown() || this.f112.isTerminated()) ? 1 : null;
        if (obj != null) {
            this.f112 = Executors.newScheduledThreadPool(2);
        }
        return (ScheduledThreadPoolExecutor) this.f112;
    }

    /* renamed from: ˋ */
    final void m178() {
        try {
            m177(this.f112);
            if (this.f114 instanceof ThreadPoolExecutor) {
                m177((ThreadPoolExecutor) this.f114);
            }
        } catch (Throwable th) {
            AFLogger.afErrorLog("failed to stop Executors", th);
        }
    }

    /* renamed from: ˊ */
    private static void m177(ExecutorService executorService) {
        try {
            AFLogger.afRDLog("shut downing executor ...");
            executorService.shutdown();
            executorService.awaitTermination(10, TimeUnit.SECONDS);
        } catch (InterruptedException e) {
            AFLogger.afRDLog("InterruptedException!!!");
        } finally {
            if (!executorService.isTerminated()) {
                AFLogger.afRDLog("killing non-finished tasks");
            }
            executorService.shutdownNow();
        }
    }
}
