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
    private static AFExecutor f130;

    /* renamed from: ˊ */
    private ScheduledExecutorService f131;

    /* renamed from: ˎ */
    private Executor f132;

    /* renamed from: ॱ */
    private Executor f133;

    private AFExecutor() {
    }

    public static AFExecutor getInstance() {
        if (f130 == null) {
            f130 = new AFExecutor();
        }
        return f130;
    }

    public Executor getSerialExecutor() {
        if (this.f132 == null) {
            if (VERSION.SDK_INT < 11) {
                return Executors.newSingleThreadExecutor();
            }
            this.f132 = AsyncTask.SERIAL_EXECUTOR;
        }
        return this.f132;
    }

    public Executor getThreadPoolExecutor() {
        if (this.f133 == null || ((this.f133 instanceof ThreadPoolExecutor) && (((ThreadPoolExecutor) this.f133).isShutdown() || ((ThreadPoolExecutor) this.f133).isTerminated() || ((ThreadPoolExecutor) this.f133).isTerminating()))) {
            if (VERSION.SDK_INT < 11) {
                return Executors.newSingleThreadExecutor();
            }
            this.f133 = Executors.newFixedThreadPool(2);
        }
        return this.f133;
    }

    /* access modifiers changed from: 0000 */
    /* renamed from: ॱ */
    public final ScheduledThreadPoolExecutor mo6409() {
        if (this.f131 == null || this.f131.isShutdown() || this.f131.isTerminated()) {
            this.f131 = Executors.newScheduledThreadPool(2);
        }
        return (ScheduledThreadPoolExecutor) this.f131;
    }

    /* access modifiers changed from: 0000 */
    /* renamed from: ˋ */
    public final void mo6408() {
        try {
            m172(this.f131);
            if (this.f133 instanceof ThreadPoolExecutor) {
                m172((ThreadPoolExecutor) this.f133);
            }
        } catch (Throwable th) {
            AFLogger.afErrorLog("failed to stop Executors", th);
        }
    }

    /* renamed from: ˊ */
    private static void m172(ExecutorService executorService) {
        try {
            AFLogger.afRDLog("shut downing executor ...");
            executorService.shutdown();
            executorService.awaitTermination(10, TimeUnit.SECONDS);
            if (!executorService.isTerminated()) {
                AFLogger.afRDLog("killing non-finished tasks");
            }
            executorService.shutdownNow();
        } catch (InterruptedException e) {
            AFLogger.afRDLog("InterruptedException!!!");
            if (!executorService.isTerminated()) {
                AFLogger.afRDLog("killing non-finished tasks");
            }
            executorService.shutdownNow();
        } catch (Throwable th) {
            if (!executorService.isTerminated()) {
                AFLogger.afRDLog("killing non-finished tasks");
            }
            executorService.shutdownNow();
            throw th;
        }
    }
}
