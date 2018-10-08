package com.google.android.gms.internal;

import android.content.Context;
import android.os.Looper;
import com.google.android.gms.common.internal.zzbp;
import com.google.android.gms.common.util.zzd;
import java.lang.Thread.UncaughtExceptionHandler;
import java.util.concurrent.ArrayBlockingQueue;
import java.util.concurrent.BlockingQueue;
import java.util.concurrent.Callable;
import java.util.concurrent.ExecutorService;
import java.util.concurrent.Future;
import java.util.concurrent.FutureTask;
import java.util.concurrent.LinkedBlockingQueue;
import java.util.concurrent.PriorityBlockingQueue;
import java.util.concurrent.Semaphore;
import java.util.concurrent.ThreadPoolExecutor;
import java.util.concurrent.TimeUnit;
import java.util.concurrent.atomic.AtomicLong;

public final class zzccj extends zzcdm {
    private static final AtomicLong zzisb = new AtomicLong(Long.MIN_VALUE);
    private ExecutorService zzirr;
    private zzccn zzirs;
    private zzccn zzirt;
    private final PriorityBlockingQueue<FutureTask<?>> zziru = new PriorityBlockingQueue();
    private final BlockingQueue<FutureTask<?>> zzirv = new LinkedBlockingQueue();
    private final UncaughtExceptionHandler zzirw = new zzccl(this, "Thread death: Uncaught exception on worker thread");
    private final UncaughtExceptionHandler zzirx = new zzccl(this, "Thread death: Uncaught exception on network thread");
    private final Object zziry = new Object();
    private final Semaphore zzirz = new Semaphore(2);
    private volatile boolean zzisa;

    zzccj(zzcco zzcco) {
        super(zzcco);
    }

    private final void zza(zzccm<?> zzccm) {
        synchronized (this.zziry) {
            this.zziru.add(zzccm);
            if (this.zzirs == null) {
                this.zzirs = new zzccn(this, "Measurement Worker", this.zziru);
                this.zzirs.setUncaughtExceptionHandler(this.zzirw);
                this.zzirs.start();
            } else {
                this.zzirs.zzmi();
            }
        }
    }

    public static boolean zzaq() {
        return Looper.myLooper() == Looper.getMainLooper();
    }

    public final /* bridge */ /* synthetic */ Context getContext() {
        return super.getContext();
    }

    public final /* bridge */ /* synthetic */ void zzatt() {
        super.zzatt();
    }

    public final /* bridge */ /* synthetic */ void zzatu() {
        super.zzatu();
    }

    public final void zzatv() {
        if (Thread.currentThread() != this.zzirt) {
            throw new IllegalStateException("Call expected from network thread");
        }
    }

    public final /* bridge */ /* synthetic */ zzcaf zzatw() {
        return super.zzatw();
    }

    public final /* bridge */ /* synthetic */ zzcam zzatx() {
        return super.zzatx();
    }

    public final /* bridge */ /* synthetic */ zzcdo zzaty() {
        return super.zzaty();
    }

    public final /* bridge */ /* synthetic */ zzcbj zzatz() {
        return super.zzatz();
    }

    public final /* bridge */ /* synthetic */ zzcaw zzaua() {
        return super.zzaua();
    }

    public final /* bridge */ /* synthetic */ zzceg zzaub() {
        return super.zzaub();
    }

    public final /* bridge */ /* synthetic */ zzcec zzauc() {
        return super.zzauc();
    }

    public final /* bridge */ /* synthetic */ zzcbk zzaud() {
        return super.zzaud();
    }

    public final /* bridge */ /* synthetic */ zzcaq zzaue() {
        return super.zzaue();
    }

    public final /* bridge */ /* synthetic */ zzcbm zzauf() {
        return super.zzauf();
    }

    public final /* bridge */ /* synthetic */ zzcfo zzaug() {
        return super.zzaug();
    }

    public final /* bridge */ /* synthetic */ zzcci zzauh() {
        return super.zzauh();
    }

    public final /* bridge */ /* synthetic */ zzcfd zzaui() {
        return super.zzaui();
    }

    public final /* bridge */ /* synthetic */ zzccj zzauj() {
        return super.zzauj();
    }

    public final /* bridge */ /* synthetic */ zzcbo zzauk() {
        return super.zzauk();
    }

    public final /* bridge */ /* synthetic */ zzcbz zzaul() {
        return super.zzaul();
    }

    public final /* bridge */ /* synthetic */ zzcap zzaum() {
        return super.zzaum();
    }

    public final boolean zzayr() {
        return Thread.currentThread() == this.zzirs;
    }

    final ExecutorService zzays() {
        ExecutorService executorService;
        synchronized (this.zziry) {
            if (this.zzirr == null) {
                this.zzirr = new ThreadPoolExecutor(0, 1, 30, TimeUnit.SECONDS, new ArrayBlockingQueue(100));
            }
            executorService = this.zzirr;
        }
        return executorService;
    }

    public final <V> Future<V> zzd(Callable<V> callable) throws IllegalStateException {
        zzwh();
        zzbp.zzu(callable);
        zzccm zzccm = new zzccm(this, (Callable) callable, false, "Task exception on worker thread");
        if (Thread.currentThread() == this.zzirs) {
            if (!this.zziru.isEmpty()) {
                zzauk().zzaye().log("Callable skipped the worker queue.");
            }
            zzccm.run();
        } else {
            zza(zzccm);
        }
        return zzccm;
    }

    public final <V> Future<V> zze(Callable<V> callable) throws IllegalStateException {
        zzwh();
        zzbp.zzu(callable);
        zzccm zzccm = new zzccm(this, (Callable) callable, true, "Task exception on worker thread");
        if (Thread.currentThread() == this.zzirs) {
            zzccm.run();
        } else {
            zza(zzccm);
        }
        return zzccm;
    }

    public final void zzg(Runnable runnable) throws IllegalStateException {
        zzwh();
        zzbp.zzu(runnable);
        zza(new zzccm(this, runnable, false, "Task exception on worker thread"));
    }

    public final void zzh(Runnable runnable) throws IllegalStateException {
        zzwh();
        zzbp.zzu(runnable);
        zzccm zzccm = new zzccm(this, runnable, false, "Task exception on network thread");
        synchronized (this.zziry) {
            this.zzirv.add(zzccm);
            if (this.zzirt == null) {
                this.zzirt = new zzccn(this, "Measurement Network", this.zzirv);
                this.zzirt.setUncaughtExceptionHandler(this.zzirx);
                this.zzirt.start();
            } else {
                this.zzirt.zzmi();
            }
        }
    }

    public final void zzug() {
        if (Thread.currentThread() != this.zzirs) {
            throw new IllegalStateException("Call expected from worker thread");
        }
    }

    protected final void zzuh() {
    }

    public final /* bridge */ /* synthetic */ zzd zzvu() {
        return super.zzvu();
    }
}
