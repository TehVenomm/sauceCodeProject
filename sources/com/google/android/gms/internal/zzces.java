package com.google.android.gms.internal;

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

    /* JADX WARNING: inconsistent code. */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    public final void run() {
        /*
        r5 = this;
        r1 = r5.zzivx;
        monitor-enter(r1);
        r0 = r5.zzivw;	 Catch:{ RemoteException -> 0x0046 }
        r0 = r0.zzivq;	 Catch:{ RemoteException -> 0x0046 }
        if (r0 != 0) goto L_0x0021;
    L_0x000b:
        r0 = r5.zzivw;	 Catch:{ RemoteException -> 0x0046 }
        r0 = r0.zzauk();	 Catch:{ RemoteException -> 0x0046 }
        r0 = r0.zzayc();	 Catch:{ RemoteException -> 0x0046 }
        r2 = "Failed to get user properties";
        r0.log(r2);	 Catch:{ RemoteException -> 0x0046 }
        r0 = r5.zzivx;	 Catch:{ all -> 0x0043 }
        r0.notify();	 Catch:{ all -> 0x0043 }
        monitor-exit(r1);	 Catch:{ all -> 0x0043 }
    L_0x0020:
        return;
    L_0x0021:
        r2 = r5.zzivx;	 Catch:{ RemoteException -> 0x0046 }
        r3 = r5.zzivw;	 Catch:{ RemoteException -> 0x0046 }
        r3 = r3.zzatz();	 Catch:{ RemoteException -> 0x0046 }
        r4 = 0;
        r3 = r3.zzjb(r4);	 Catch:{ RemoteException -> 0x0046 }
        r4 = r5.zzius;	 Catch:{ RemoteException -> 0x0046 }
        r0 = r0.zza(r3, r4);	 Catch:{ RemoteException -> 0x0046 }
        r2.set(r0);	 Catch:{ RemoteException -> 0x0046 }
        r0 = r5.zzivw;	 Catch:{ RemoteException -> 0x0046 }
        r0.zzwt();	 Catch:{ RemoteException -> 0x0046 }
        r0 = r5.zzivx;	 Catch:{ all -> 0x0043 }
        r0.notify();	 Catch:{ all -> 0x0043 }
    L_0x0041:
        monitor-exit(r1);	 Catch:{ all -> 0x0043 }
        goto L_0x0020;
    L_0x0043:
        r0 = move-exception;
        monitor-exit(r1);	 Catch:{ all -> 0x0043 }
        throw r0;
    L_0x0046:
        r0 = move-exception;
        r2 = r5.zzivw;	 Catch:{ all -> 0x005c }
        r2 = r2.zzauk();	 Catch:{ all -> 0x005c }
        r2 = r2.zzayc();	 Catch:{ all -> 0x005c }
        r3 = "Failed to get user properties";
        r2.zzj(r3, r0);	 Catch:{ all -> 0x005c }
        r0 = r5.zzivx;	 Catch:{ all -> 0x0043 }
        r0.notify();	 Catch:{ all -> 0x0043 }
        goto L_0x0041;
    L_0x005c:
        r0 = move-exception;
        r2 = r5.zzivx;	 Catch:{ all -> 0x0043 }
        r2.notify();	 Catch:{ all -> 0x0043 }
        throw r0;	 Catch:{ all -> 0x0043 }
        */
        throw new UnsupportedOperationException("Method not decompiled: com.google.android.gms.internal.zzces.run():void");
    }
}
