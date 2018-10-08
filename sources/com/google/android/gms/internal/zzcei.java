package com.google.android.gms.internal;

import java.util.concurrent.atomic.AtomicReference;

final class zzcei implements Runnable {
    private /* synthetic */ zzceg zzivw;
    private /* synthetic */ AtomicReference zzivx;

    zzcei(zzceg zzceg, AtomicReference atomicReference) {
        this.zzivw = zzceg;
        this.zzivx = atomicReference;
    }

    /* JADX WARNING: inconsistent code. */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    public final void run() {
        /*
        r5 = this;
        r1 = r5.zzivx;
        monitor-enter(r1);
        r0 = r5.zzivw;	 Catch:{ RemoteException -> 0x0062 }
        r0 = r0.zzivq;	 Catch:{ RemoteException -> 0x0062 }
        if (r0 != 0) goto L_0x0021;
    L_0x000b:
        r0 = r5.zzivw;	 Catch:{ RemoteException -> 0x0062 }
        r0 = r0.zzauk();	 Catch:{ RemoteException -> 0x0062 }
        r0 = r0.zzayc();	 Catch:{ RemoteException -> 0x0062 }
        r2 = "Failed to get app instance id";
        r0.log(r2);	 Catch:{ RemoteException -> 0x0062 }
        r0 = r5.zzivx;	 Catch:{ all -> 0x005f }
        r0.notify();	 Catch:{ all -> 0x005f }
        monitor-exit(r1);	 Catch:{ all -> 0x005f }
    L_0x0020:
        return;
    L_0x0021:
        r2 = r5.zzivx;	 Catch:{ RemoteException -> 0x0062 }
        r3 = r5.zzivw;	 Catch:{ RemoteException -> 0x0062 }
        r3 = r3.zzatz();	 Catch:{ RemoteException -> 0x0062 }
        r4 = 0;
        r3 = r3.zzjb(r4);	 Catch:{ RemoteException -> 0x0062 }
        r0 = r0.zzc(r3);	 Catch:{ RemoteException -> 0x0062 }
        r2.set(r0);	 Catch:{ RemoteException -> 0x0062 }
        r0 = r5.zzivx;	 Catch:{ RemoteException -> 0x0062 }
        r0 = r0.get();	 Catch:{ RemoteException -> 0x0062 }
        r0 = (java.lang.String) r0;	 Catch:{ RemoteException -> 0x0062 }
        if (r0 == 0) goto L_0x0053;
    L_0x003f:
        r2 = r5.zzivw;	 Catch:{ RemoteException -> 0x0062 }
        r2 = r2.zzaty();	 Catch:{ RemoteException -> 0x0062 }
        r2.zzjk(r0);	 Catch:{ RemoteException -> 0x0062 }
        r2 = r5.zzivw;	 Catch:{ RemoteException -> 0x0062 }
        r2 = r2.zzaul();	 Catch:{ RemoteException -> 0x0062 }
        r2 = r2.zziqm;	 Catch:{ RemoteException -> 0x0062 }
        r2.zzjl(r0);	 Catch:{ RemoteException -> 0x0062 }
    L_0x0053:
        r0 = r5.zzivw;	 Catch:{ RemoteException -> 0x0062 }
        r0.zzwt();	 Catch:{ RemoteException -> 0x0062 }
        r0 = r5.zzivx;	 Catch:{ all -> 0x005f }
        r0.notify();	 Catch:{ all -> 0x005f }
    L_0x005d:
        monitor-exit(r1);	 Catch:{ all -> 0x005f }
        goto L_0x0020;
    L_0x005f:
        r0 = move-exception;
        monitor-exit(r1);	 Catch:{ all -> 0x005f }
        throw r0;
    L_0x0062:
        r0 = move-exception;
        r2 = r5.zzivw;	 Catch:{ all -> 0x0078 }
        r2 = r2.zzauk();	 Catch:{ all -> 0x0078 }
        r2 = r2.zzayc();	 Catch:{ all -> 0x0078 }
        r3 = "Failed to get app instance id";
        r2.zzj(r3, r0);	 Catch:{ all -> 0x0078 }
        r0 = r5.zzivx;	 Catch:{ all -> 0x005f }
        r0.notify();	 Catch:{ all -> 0x005f }
        goto L_0x005d;
    L_0x0078:
        r0 = move-exception;
        r2 = r5.zzivx;	 Catch:{ all -> 0x005f }
        r2.notify();	 Catch:{ all -> 0x005f }
        throw r0;	 Catch:{ all -> 0x005f }
        */
        throw new UnsupportedOperationException("Method not decompiled: com.google.android.gms.internal.zzcei.run():void");
    }
}
