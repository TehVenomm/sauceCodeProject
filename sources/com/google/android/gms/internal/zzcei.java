package com.google.android.gms.internal;

import android.os.RemoteException;
import java.util.concurrent.atomic.AtomicReference;

final class zzcei implements Runnable {
    private /* synthetic */ zzceg zzivw;
    private /* synthetic */ AtomicReference zzivx;

    zzcei(zzceg zzceg, AtomicReference atomicReference) {
        this.zzivw = zzceg;
        this.zzivx = atomicReference;
    }

    public final void run() {
        synchronized (this.zzivx) {
            try {
                zzcbg zzd = this.zzivw.zzivq;
                if (zzd == null) {
                    this.zzivw.zzauk().zzayc().log("Failed to get app instance id");
                    this.zzivx.notify();
                    return;
                }
                this.zzivx.set(zzd.zzc(this.zzivw.zzatz().zzjb(null)));
                String str = (String) this.zzivx.get();
                if (str != null) {
                    this.zzivw.zzaty().zzjk(str);
                    this.zzivw.zzaul().zziqm.zzjl(str);
                }
                this.zzivw.zzwt();
                this.zzivx.notify();
            } catch (RemoteException e) {
                this.zzivw.zzauk().zzayc().zzj("Failed to get app instance id", e);
                this.zzivx.notify();
            } catch (Throwable th) {
                this.zzivx.notify();
            }
        }
    }
}
