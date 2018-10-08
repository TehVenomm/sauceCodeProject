package com.google.android.gms.internal;

import android.os.RemoteException;
import java.util.concurrent.atomic.AtomicReference;

final class zzces implements Runnable {
    private /* synthetic */ boolean zzius;
    private /* synthetic */ zzceg zzivw;
    private /* synthetic */ AtomicReference zzivx;

    zzces(zzceg zzceg, AtomicReference atomicReference, boolean z) {
        this.zzivw = zzceg;
        this.zzivx = atomicReference;
        this.zzius = z;
    }

    public final void run() {
        synchronized (this.zzivx) {
            try {
                zzcbg zzd = this.zzivw.zzivq;
                if (zzd == null) {
                    this.zzivw.zzauk().zzayc().log("Failed to get user properties");
                    this.zzivx.notify();
                    return;
                }
                this.zzivx.set(zzd.zza(this.zzivw.zzatz().zzjb(null), this.zzius));
                this.zzivw.zzwt();
                this.zzivx.notify();
            } catch (RemoteException e) {
                this.zzivw.zzauk().zzayc().zzj("Failed to get user properties", e);
                this.zzivx.notify();
            } catch (Throwable th) {
                this.zzivx.notify();
            }
        }
    }
}
