package com.google.android.gms.measurement.internal;

import android.content.Context;
import com.google.android.gms.common.internal.Preconditions;
import com.google.android.gms.common.util.Clock;
import java.lang.Thread.UncaughtExceptionHandler;
import java.util.concurrent.BlockingQueue;
import java.util.concurrent.Callable;
import java.util.concurrent.Future;
import java.util.concurrent.LinkedBlockingQueue;
import java.util.concurrent.PriorityBlockingQueue;
import java.util.concurrent.Semaphore;
import java.util.concurrent.atomic.AtomicLong;
import java.util.concurrent.atomic.AtomicReference;

public final class zzfc extends zzge {
    /* access modifiers changed from: private */
    public static final AtomicLong zznj = new AtomicLong(Long.MIN_VALUE);
    /* access modifiers changed from: private */
    public zzfg zzna;
    /* access modifiers changed from: private */
    public zzfg zznb;
    private final PriorityBlockingQueue<zzfh<?>> zznc = new PriorityBlockingQueue<>();
    private final BlockingQueue<zzfh<?>> zznd = new LinkedBlockingQueue();
    private final UncaughtExceptionHandler zzne = new zzfe(this, "Thread death: Uncaught exception on worker thread");
    private final UncaughtExceptionHandler zznf = new zzfe(this, "Thread death: Uncaught exception on network thread");
    /* access modifiers changed from: private */
    public final Object zzng = new Object();
    /* access modifiers changed from: private */
    public final Semaphore zznh = new Semaphore(2);
    /* access modifiers changed from: private */
    public volatile boolean zzni;

    zzfc(zzfj zzfj) {
        super(zzfj);
    }

    private final void zza(zzfh<?> zzfh) {
        synchronized (this.zzng) {
            this.zznc.add(zzfh);
            if (this.zzna == null) {
                this.zzna = new zzfg(this, "Measurement Worker", this.zznc);
                this.zzna.setUncaughtExceptionHandler(this.zzne);
                this.zzna.start();
            } else {
                this.zzna.zzhr();
            }
        }
    }

    public final /* bridge */ /* synthetic */ Context getContext() {
        return super.getContext();
    }

    /* access modifiers changed from: 0000 */
    public final <T> T zza(AtomicReference<T> atomicReference, long j, String str, Runnable runnable) {
        synchronized (atomicReference) {
            zzaa().zza(runnable);
            try {
                atomicReference.wait(15000);
            } catch (InterruptedException e) {
                zzeh zzgn = zzab().zzgn();
                String valueOf = String.valueOf(str);
                zzgn.zzao(valueOf.length() != 0 ? "Interrupted waiting for ".concat(valueOf) : new String("Interrupted waiting for "));
                return null;
            }
        }
        Object obj = atomicReference.get();
        if (obj == null) {
            zzeh zzgn2 = zzab().zzgn();
            String valueOf2 = String.valueOf(str);
            zzgn2.zzao(valueOf2.length() != 0 ? "Timed out waiting for ".concat(valueOf2) : new String("Timed out waiting for "));
        }
        return obj;
    }

    public final <V> Future<V> zza(Callable<V> callable) throws IllegalStateException {
        zzbi();
        Preconditions.checkNotNull(callable);
        zzfh zzfh = new zzfh(this, callable, false, "Task exception on worker thread");
        if (Thread.currentThread() == this.zzna) {
            if (!this.zznc.isEmpty()) {
                zzab().zzgn().zzao("Callable skipped the worker queue.");
            }
            zzfh.run();
        } else {
            zza(zzfh);
        }
        return zzfh;
    }

    public final void zza(Runnable runnable) throws IllegalStateException {
        zzbi();
        Preconditions.checkNotNull(runnable);
        zza(new zzfh<>(this, runnable, false, "Task exception on worker thread"));
    }

    public final /* bridge */ /* synthetic */ zzfc zzaa() {
        return super.zzaa();
    }

    public final /* bridge */ /* synthetic */ zzef zzab() {
        return super.zzab();
    }

    public final /* bridge */ /* synthetic */ zzeo zzac() {
        return super.zzac();
    }

    public final /* bridge */ /* synthetic */ zzs zzad() {
        return super.zzad();
    }

    public final /* bridge */ /* synthetic */ zzr zzae() {
        return super.zzae();
    }

    public final <V> Future<V> zzb(Callable<V> callable) throws IllegalStateException {
        zzbi();
        Preconditions.checkNotNull(callable);
        zzfh zzfh = new zzfh(this, callable, true, "Task exception on worker thread");
        if (Thread.currentThread() == this.zzna) {
            zzfh.run();
        } else {
            zza(zzfh);
        }
        return zzfh;
    }

    public final void zzb(Runnable runnable) throws IllegalStateException {
        zzbi();
        Preconditions.checkNotNull(runnable);
        zzfh zzfh = new zzfh(this, runnable, false, "Task exception on network thread");
        synchronized (this.zzng) {
            this.zznd.add(zzfh);
            if (this.zznb == null) {
                this.zznb = new zzfg(this, "Measurement Network", this.zznd);
                this.zznb.setUncaughtExceptionHandler(this.zznf);
                this.zznb.start();
            } else {
                this.zznb.zzhr();
            }
        }
    }

    /* access modifiers changed from: protected */
    public final boolean zzbk() {
        return false;
    }

    public final boolean zzhp() {
        return Thread.currentThread() == this.zzna;
    }

    public final /* bridge */ /* synthetic */ void zzl() {
        super.zzl();
    }

    public final /* bridge */ /* synthetic */ void zzm() {
        super.zzm();
    }

    public final void zzn() {
        if (Thread.currentThread() != this.zznb) {
            throw new IllegalStateException("Call expected from network thread");
        }
    }

    public final void zzo() {
        if (Thread.currentThread() != this.zzna) {
            throw new IllegalStateException("Call expected from worker thread");
        }
    }

    public final /* bridge */ /* synthetic */ zzac zzw() {
        return super.zzw();
    }

    public final /* bridge */ /* synthetic */ Clock zzx() {
        return super.zzx();
    }

    public final /* bridge */ /* synthetic */ zzed zzy() {
        return super.zzy();
    }

    public final /* bridge */ /* synthetic */ zzjs zzz() {
        return super.zzz();
    }
}
