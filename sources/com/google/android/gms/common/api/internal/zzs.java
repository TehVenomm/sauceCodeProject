package com.google.android.gms.common.api.internal;

import android.os.Looper;
import android.support.annotation.NonNull;
import android.util.Log;
import com.google.android.gms.common.api.GoogleApiClient;
import com.google.android.gms.common.api.PendingResult;
import com.google.android.gms.common.api.PendingResult.zza;
import com.google.android.gms.common.api.Releasable;
import com.google.android.gms.common.api.Result;
import com.google.android.gms.common.api.ResultCallback;
import com.google.android.gms.common.api.ResultTransform;
import com.google.android.gms.common.api.Status;
import com.google.android.gms.common.api.TransformedResult;
import com.google.android.gms.common.internal.zzap;
import com.google.android.gms.common.internal.zzbp;
import java.lang.ref.WeakReference;
import java.util.ArrayList;
import java.util.concurrent.CountDownLatch;
import java.util.concurrent.TimeUnit;
import java.util.concurrent.atomic.AtomicReference;

public abstract class zzs<R extends Result> extends PendingResult<R> {
    static final ThreadLocal<Boolean> zzfiy = new zzt();
    private Status mStatus;
    private boolean zzaj;
    private final CountDownLatch zzaop;
    private R zzfhl;
    private final Object zzfiz;
    private zzu<R> zzfja;
    private WeakReference<GoogleApiClient> zzfjb;
    private final ArrayList<zza> zzfjc;
    private ResultCallback<? super R> zzfjd;
    private final AtomicReference<zzdl> zzfje;
    private zzv zzfjf;
    private volatile boolean zzfjg;
    private boolean zzfjh;
    private zzap zzfji;
    private volatile zzdf<R> zzfjj;
    private boolean zzfjk;

    @Deprecated
    zzs() {
        this.zzfiz = new Object();
        this.zzaop = new CountDownLatch(1);
        this.zzfjc = new ArrayList();
        this.zzfje = new AtomicReference();
        this.zzfjk = false;
        this.zzfja = new zzu(Looper.getMainLooper());
        this.zzfjb = new WeakReference(null);
    }

    @Deprecated
    protected zzs(Looper looper) {
        this.zzfiz = new Object();
        this.zzaop = new CountDownLatch(1);
        this.zzfjc = new ArrayList();
        this.zzfje = new AtomicReference();
        this.zzfjk = false;
        this.zzfja = new zzu(looper);
        this.zzfjb = new WeakReference(null);
    }

    protected zzs(GoogleApiClient googleApiClient) {
        this.zzfiz = new Object();
        this.zzaop = new CountDownLatch(1);
        this.zzfjc = new ArrayList();
        this.zzfje = new AtomicReference();
        this.zzfjk = false;
        this.zzfja = new zzu(googleApiClient != null ? googleApiClient.getLooper() : Looper.getMainLooper());
        this.zzfjb = new WeakReference(googleApiClient);
    }

    private final R get() {
        R r;
        boolean z = true;
        synchronized (this.zzfiz) {
            if (this.zzfjg) {
                z = false;
            }
            zzbp.zza(z, (Object) "Result has already been consumed.");
            zzbp.zza(isReady(), (Object) "Result is not ready.");
            r = this.zzfhl;
            this.zzfhl = null;
            this.zzfjd = null;
            this.zzfjg = true;
        }
        zzdl zzdl = (zzdl) this.zzfje.getAndSet(null);
        if (zzdl != null) {
            zzdl.zzc(this);
        }
        return r;
    }

    private final void zzc(R r) {
        this.zzfhl = r;
        this.zzfji = null;
        this.zzaop.countDown();
        this.mStatus = this.zzfhl.getStatus();
        if (this.zzaj) {
            this.zzfjd = null;
        } else if (this.zzfjd != null) {
            this.zzfja.removeMessages(2);
            this.zzfja.zza(this.zzfjd, get());
        } else if (this.zzfhl instanceof Releasable) {
            this.zzfjf = new zzv();
        }
        ArrayList arrayList = this.zzfjc;
        int size = arrayList.size();
        int i = 0;
        while (i < size) {
            Object obj = arrayList.get(i);
            i++;
            ((zza) obj).zzp(this.mStatus);
        }
        this.zzfjc.clear();
    }

    public static void zzd(Result result) {
        if (result instanceof Releasable) {
            try {
                ((Releasable) result).release();
            } catch (Throwable e) {
                String valueOf = String.valueOf(result);
                Log.w("BasePendingResult", new StringBuilder(String.valueOf(valueOf).length() + 18).append("Unable to release ").append(valueOf).toString(), e);
            }
        }
    }

    public final R await() {
        boolean z = true;
        zzbp.zza(Looper.myLooper() != Looper.getMainLooper(), (Object) "await must not be called on the UI thread");
        zzbp.zza(!this.zzfjg, (Object) "Result has already been consumed");
        if (this.zzfjj != null) {
            z = false;
        }
        zzbp.zza(z, (Object) "Cannot await if then() has been called.");
        try {
            this.zzaop.await();
        } catch (InterruptedException e) {
            zzt(Status.zzfhq);
        }
        zzbp.zza(isReady(), (Object) "Result is not ready.");
        return get();
    }

    public final R await(long j, TimeUnit timeUnit) {
        boolean z = true;
        boolean z2 = j <= 0 || Looper.myLooper() != Looper.getMainLooper();
        zzbp.zza(z2, (Object) "await must not be called on the UI thread when time is greater than zero.");
        zzbp.zza(!this.zzfjg, (Object) "Result has already been consumed.");
        if (this.zzfjj != null) {
            z = false;
        }
        zzbp.zza(z, (Object) "Cannot await if then() has been called.");
        try {
            if (!this.zzaop.await(j, timeUnit)) {
                zzt(Status.zzfhs);
            }
        } catch (InterruptedException e) {
            zzt(Status.zzfhq);
        }
        zzbp.zza(isReady(), (Object) "Result is not ready.");
        return get();
    }

    /* JADX WARNING: inconsistent code. */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    public void cancel() {
        /*
        r2 = this;
        r1 = r2.zzfiz;
        monitor-enter(r1);
        r0 = r2.zzaj;	 Catch:{ all -> 0x0029 }
        if (r0 != 0) goto L_0x000b;
    L_0x0007:
        r0 = r2.zzfjg;	 Catch:{ all -> 0x0029 }
        if (r0 == 0) goto L_0x000d;
    L_0x000b:
        monitor-exit(r1);	 Catch:{ all -> 0x0029 }
    L_0x000c:
        return;
    L_0x000d:
        r0 = r2.zzfji;	 Catch:{ all -> 0x0029 }
        if (r0 == 0) goto L_0x0016;
    L_0x0011:
        r0 = r2.zzfji;	 Catch:{ RemoteException -> 0x002c }
        r0.cancel();	 Catch:{ RemoteException -> 0x002c }
    L_0x0016:
        r0 = r2.zzfhl;	 Catch:{ all -> 0x0029 }
        zzd(r0);	 Catch:{ all -> 0x0029 }
        r0 = 1;
        r2.zzaj = r0;	 Catch:{ all -> 0x0029 }
        r0 = com.google.android.gms.common.api.Status.zzfht;	 Catch:{ all -> 0x0029 }
        r0 = r2.zzb(r0);	 Catch:{ all -> 0x0029 }
        r2.zzc(r0);	 Catch:{ all -> 0x0029 }
        monitor-exit(r1);	 Catch:{ all -> 0x0029 }
        goto L_0x000c;
    L_0x0029:
        r0 = move-exception;
        monitor-exit(r1);	 Catch:{ all -> 0x0029 }
        throw r0;
    L_0x002c:
        r0 = move-exception;
        goto L_0x0016;
        */
        throw new UnsupportedOperationException("Method not decompiled: com.google.android.gms.common.api.internal.zzs.cancel():void");
    }

    public boolean isCanceled() {
        boolean z;
        synchronized (this.zzfiz) {
            z = this.zzaj;
        }
        return z;
    }

    public final boolean isReady() {
        return this.zzaop.getCount() == 0;
    }

    public final void setResult(R r) {
        boolean z = true;
        synchronized (this.zzfiz) {
            if (this.zzfjh || this.zzaj) {
                zzd(r);
                return;
            }
            if (isReady()) {
            }
            zzbp.zza(!isReady(), (Object) "Results have already been set");
            if (this.zzfjg) {
                z = false;
            }
            zzbp.zza(z, (Object) "Result has already been consumed");
            zzc(r);
        }
    }

    /* JADX WARNING: inconsistent code. */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    public final void setResultCallback(com.google.android.gms.common.api.ResultCallback<? super R> r6) {
        /*
        r5 = this;
        r0 = 1;
        r1 = 0;
        r3 = r5.zzfiz;
        monitor-enter(r3);
        if (r6 != 0) goto L_0x000c;
    L_0x0007:
        r0 = 0;
        r5.zzfjd = r0;	 Catch:{ all -> 0x0027 }
        monitor-exit(r3);	 Catch:{ all -> 0x0027 }
    L_0x000b:
        return;
    L_0x000c:
        r2 = r5.zzfjg;	 Catch:{ all -> 0x0027 }
        if (r2 != 0) goto L_0x002a;
    L_0x0010:
        r2 = r0;
    L_0x0011:
        r4 = "Result has already been consumed.";
        com.google.android.gms.common.internal.zzbp.zza(r2, r4);	 Catch:{ all -> 0x0027 }
        r2 = r5.zzfjj;	 Catch:{ all -> 0x0027 }
        if (r2 != 0) goto L_0x002c;
    L_0x001a:
        r1 = "Cannot set callbacks if then() has been called.";
        com.google.android.gms.common.internal.zzbp.zza(r0, r1);	 Catch:{ all -> 0x0027 }
        r0 = r5.isCanceled();	 Catch:{ all -> 0x0027 }
        if (r0 == 0) goto L_0x002e;
    L_0x0025:
        monitor-exit(r3);	 Catch:{ all -> 0x0027 }
        goto L_0x000b;
    L_0x0027:
        r0 = move-exception;
        monitor-exit(r3);	 Catch:{ all -> 0x0027 }
        throw r0;
    L_0x002a:
        r2 = r1;
        goto L_0x0011;
    L_0x002c:
        r0 = r1;
        goto L_0x001a;
    L_0x002e:
        r0 = r5.isReady();	 Catch:{ all -> 0x0027 }
        if (r0 == 0) goto L_0x003f;
    L_0x0034:
        r0 = r5.zzfja;	 Catch:{ all -> 0x0027 }
        r1 = r5.get();	 Catch:{ all -> 0x0027 }
        r0.zza(r6, r1);	 Catch:{ all -> 0x0027 }
    L_0x003d:
        monitor-exit(r3);	 Catch:{ all -> 0x0027 }
        goto L_0x000b;
    L_0x003f:
        r5.zzfjd = r6;	 Catch:{ all -> 0x0027 }
        goto L_0x003d;
        */
        throw new UnsupportedOperationException("Method not decompiled: com.google.android.gms.common.api.internal.zzs.setResultCallback(com.google.android.gms.common.api.ResultCallback):void");
    }

    /* JADX WARNING: inconsistent code. */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    public final void setResultCallback(com.google.android.gms.common.api.ResultCallback<? super R> r7, long r8, java.util.concurrent.TimeUnit r10) {
        /*
        r6 = this;
        r0 = 1;
        r1 = 0;
        r3 = r6.zzfiz;
        monitor-enter(r3);
        if (r7 != 0) goto L_0x000c;
    L_0x0007:
        r0 = 0;
        r6.zzfjd = r0;	 Catch:{ all -> 0x0027 }
        monitor-exit(r3);	 Catch:{ all -> 0x0027 }
    L_0x000b:
        return;
    L_0x000c:
        r2 = r6.zzfjg;	 Catch:{ all -> 0x0027 }
        if (r2 != 0) goto L_0x002a;
    L_0x0010:
        r2 = r0;
    L_0x0011:
        r4 = "Result has already been consumed.";
        com.google.android.gms.common.internal.zzbp.zza(r2, r4);	 Catch:{ all -> 0x0027 }
        r2 = r6.zzfjj;	 Catch:{ all -> 0x0027 }
        if (r2 != 0) goto L_0x002c;
    L_0x001a:
        r1 = "Cannot set callbacks if then() has been called.";
        com.google.android.gms.common.internal.zzbp.zza(r0, r1);	 Catch:{ all -> 0x0027 }
        r0 = r6.isCanceled();	 Catch:{ all -> 0x0027 }
        if (r0 == 0) goto L_0x002e;
    L_0x0025:
        monitor-exit(r3);	 Catch:{ all -> 0x0027 }
        goto L_0x000b;
    L_0x0027:
        r0 = move-exception;
        monitor-exit(r3);	 Catch:{ all -> 0x0027 }
        throw r0;
    L_0x002a:
        r2 = r1;
        goto L_0x0011;
    L_0x002c:
        r0 = r1;
        goto L_0x001a;
    L_0x002e:
        r0 = r6.isReady();	 Catch:{ all -> 0x0027 }
        if (r0 == 0) goto L_0x003f;
    L_0x0034:
        r0 = r6.zzfja;	 Catch:{ all -> 0x0027 }
        r1 = r6.get();	 Catch:{ all -> 0x0027 }
        r0.zza(r7, r1);	 Catch:{ all -> 0x0027 }
    L_0x003d:
        monitor-exit(r3);	 Catch:{ all -> 0x0027 }
        goto L_0x000b;
    L_0x003f:
        r6.zzfjd = r7;	 Catch:{ all -> 0x0027 }
        r0 = r6.zzfja;	 Catch:{ all -> 0x0027 }
        r4 = r10.toMillis(r8);	 Catch:{ all -> 0x0027 }
        r1 = 2;
        r1 = r0.obtainMessage(r1, r6);	 Catch:{ all -> 0x0027 }
        r0.sendMessageDelayed(r1, r4);	 Catch:{ all -> 0x0027 }
        goto L_0x003d;
        */
        throw new UnsupportedOperationException("Method not decompiled: com.google.android.gms.common.api.internal.zzs.setResultCallback(com.google.android.gms.common.api.ResultCallback, long, java.util.concurrent.TimeUnit):void");
    }

    public <S extends Result> TransformedResult<S> then(ResultTransform<? super R, ? extends S> resultTransform) {
        TransformedResult<S> then;
        boolean z = true;
        zzbp.zza(!this.zzfjg, (Object) "Result has already been consumed.");
        synchronized (this.zzfiz) {
            zzbp.zza(this.zzfjj == null, (Object) "Cannot call then() twice.");
            zzbp.zza(this.zzfjd == null, (Object) "Cannot call then() if callbacks are set.");
            if (this.zzaj) {
                z = false;
            }
            zzbp.zza(z, (Object) "Cannot call then() if result was canceled.");
            this.zzfjk = true;
            this.zzfjj = new zzdf(this.zzfjb);
            then = this.zzfjj.then(resultTransform);
            if (isReady()) {
                this.zzfja.zza(this.zzfjj, get());
            } else {
                this.zzfjd = this.zzfjj;
            }
        }
        return then;
    }

    public final void zza(zza zza) {
        zzbp.zzb(zza != null, (Object) "Callback cannot be null.");
        synchronized (this.zzfiz) {
            if (isReady()) {
                zza.zzp(this.mStatus);
            } else {
                this.zzfjc.add(zza);
            }
        }
    }

    public final void zza(zzdl zzdl) {
        this.zzfje.set(zzdl);
    }

    protected final void zza(zzap zzap) {
        synchronized (this.zzfiz) {
            this.zzfji = zzap;
        }
    }

    public final Integer zzafr() {
        return null;
    }

    public final boolean zzage() {
        boolean isCanceled;
        synchronized (this.zzfiz) {
            if (((GoogleApiClient) this.zzfjb.get()) == null || !this.zzfjk) {
                cancel();
            }
            isCanceled = isCanceled();
        }
        return isCanceled;
    }

    public final void zzagf() {
        boolean z = this.zzfjk || ((Boolean) zzfiy.get()).booleanValue();
        this.zzfjk = z;
    }

    @NonNull
    protected abstract R zzb(Status status);

    public final void zzt(Status status) {
        synchronized (this.zzfiz) {
            if (!isReady()) {
                setResult(zzb(status));
                this.zzfjh = true;
            }
        }
    }
}
