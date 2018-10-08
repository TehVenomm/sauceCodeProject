package com.google.android.gms.internal;

import java.util.concurrent.atomic.AtomicReference;

final class zzcep implements Runnable {
    private /* synthetic */ String zziab;
    private /* synthetic */ String zziud;
    private /* synthetic */ String zziue;
    private /* synthetic */ zzceg zzivw;
    private /* synthetic */ AtomicReference zzivx;

    zzcep(zzceg zzceg, AtomicReference atomicReference, String str, String str2, String str3) {
        this.zzivw = zzceg;
        this.zzivx = atomicReference;
        this.zziab = str;
        this.zziud = str2;
        this.zziue = str3;
    }

    /* JADX WARNING: inconsistent code. */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    public final void run() {
        /*
        r7 = this;
        r1 = r7.zzivx;
        monitor-enter(r1);
        r0 = r7.zzivw;	 Catch:{ RemoteException -> 0x007c }
        r0 = r0.zzivq;	 Catch:{ RemoteException -> 0x007c }
        if (r0 != 0) goto L_0x0034;
    L_0x000b:
        r0 = r7.zzivw;	 Catch:{ RemoteException -> 0x007c }
        r0 = r0.zzauk();	 Catch:{ RemoteException -> 0x007c }
        r0 = r0.zzayc();	 Catch:{ RemoteException -> 0x007c }
        r2 = "Failed to get conditional properties";
        r3 = r7.zziab;	 Catch:{ RemoteException -> 0x007c }
        r3 = com.google.android.gms.internal.zzcbo.zzjf(r3);	 Catch:{ RemoteException -> 0x007c }
        r4 = r7.zziud;	 Catch:{ RemoteException -> 0x007c }
        r5 = r7.zziue;	 Catch:{ RemoteException -> 0x007c }
        r0.zzd(r2, r3, r4, r5);	 Catch:{ RemoteException -> 0x007c }
        r0 = r7.zzivx;	 Catch:{ RemoteException -> 0x007c }
        r2 = java.util.Collections.emptyList();	 Catch:{ RemoteException -> 0x007c }
        r0.set(r2);	 Catch:{ RemoteException -> 0x007c }
        r0 = r7.zzivx;	 Catch:{ all -> 0x0069 }
        r0.notify();	 Catch:{ all -> 0x0069 }
        monitor-exit(r1);	 Catch:{ all -> 0x0069 }
    L_0x0033:
        return;
    L_0x0034:
        r2 = r7.zziab;	 Catch:{ RemoteException -> 0x007c }
        r2 = android.text.TextUtils.isEmpty(r2);	 Catch:{ RemoteException -> 0x007c }
        if (r2 == 0) goto L_0x006c;
    L_0x003c:
        r2 = r7.zzivx;	 Catch:{ RemoteException -> 0x007c }
        r3 = r7.zziud;	 Catch:{ RemoteException -> 0x007c }
        r4 = r7.zziue;	 Catch:{ RemoteException -> 0x007c }
        r5 = r7.zzivw;	 Catch:{ RemoteException -> 0x007c }
        r5 = r5.zzatz();	 Catch:{ RemoteException -> 0x007c }
        r6 = r7.zzivw;	 Catch:{ RemoteException -> 0x007c }
        r6 = r6.zzauk();	 Catch:{ RemoteException -> 0x007c }
        r6 = r6.zzayj();	 Catch:{ RemoteException -> 0x007c }
        r5 = r5.zzjb(r6);	 Catch:{ RemoteException -> 0x007c }
        r0 = r0.zza(r3, r4, r5);	 Catch:{ RemoteException -> 0x007c }
        r2.set(r0);	 Catch:{ RemoteException -> 0x007c }
    L_0x005d:
        r0 = r7.zzivw;	 Catch:{ RemoteException -> 0x007c }
        r0.zzwt();	 Catch:{ RemoteException -> 0x007c }
        r0 = r7.zzivx;	 Catch:{ all -> 0x0069 }
        r0.notify();	 Catch:{ all -> 0x0069 }
    L_0x0067:
        monitor-exit(r1);	 Catch:{ all -> 0x0069 }
        goto L_0x0033;
    L_0x0069:
        r0 = move-exception;
        monitor-exit(r1);	 Catch:{ all -> 0x0069 }
        throw r0;
    L_0x006c:
        r2 = r7.zzivx;	 Catch:{ RemoteException -> 0x007c }
        r3 = r7.zziab;	 Catch:{ RemoteException -> 0x007c }
        r4 = r7.zziud;	 Catch:{ RemoteException -> 0x007c }
        r5 = r7.zziue;	 Catch:{ RemoteException -> 0x007c }
        r0 = r0.zzj(r3, r4, r5);	 Catch:{ RemoteException -> 0x007c }
        r2.set(r0);	 Catch:{ RemoteException -> 0x007c }
        goto L_0x005d;
    L_0x007c:
        r0 = move-exception;
        r2 = r7.zzivw;	 Catch:{ all -> 0x00a3 }
        r2 = r2.zzauk();	 Catch:{ all -> 0x00a3 }
        r2 = r2.zzayc();	 Catch:{ all -> 0x00a3 }
        r3 = "Failed to get conditional properties";
        r4 = r7.zziab;	 Catch:{ all -> 0x00a3 }
        r4 = com.google.android.gms.internal.zzcbo.zzjf(r4);	 Catch:{ all -> 0x00a3 }
        r5 = r7.zziud;	 Catch:{ all -> 0x00a3 }
        r2.zzd(r3, r4, r5, r0);	 Catch:{ all -> 0x00a3 }
        r0 = r7.zzivx;	 Catch:{ all -> 0x00a3 }
        r2 = java.util.Collections.emptyList();	 Catch:{ all -> 0x00a3 }
        r0.set(r2);	 Catch:{ all -> 0x00a3 }
        r0 = r7.zzivx;	 Catch:{ all -> 0x0069 }
        r0.notify();	 Catch:{ all -> 0x0069 }
        goto L_0x0067;
    L_0x00a3:
        r0 = move-exception;
        r2 = r7.zzivx;	 Catch:{ all -> 0x0069 }
        r2.notify();	 Catch:{ all -> 0x0069 }
        throw r0;	 Catch:{ all -> 0x0069 }
        */
        throw new UnsupportedOperationException("Method not decompiled: com.google.android.gms.internal.zzcep.run():void");
    }
}
