package com.google.android.gms.measurement.internal;

import com.google.android.gms.common.internal.Preconditions;
import java.util.concurrent.BlockingQueue;

final class zzfg extends Thread {
    private final /* synthetic */ zzfc zznt;
    private final Object zznu = new Object();
    private final BlockingQueue<zzfh<?>> zznv;

    public zzfg(zzfc zzfc, String str, BlockingQueue<zzfh<?>> blockingQueue) {
        this.zznt = zzfc;
        Preconditions.checkNotNull(str);
        Preconditions.checkNotNull(blockingQueue);
        this.zznv = blockingQueue;
        setName(str);
    }

    private final void zza(InterruptedException interruptedException) {
        this.zznt.zzab().zzgn().zza(String.valueOf(getName()).concat(" was interrupted"), interruptedException);
    }

    /* JADX WARNING: Code restructure failed: missing block: B:44:0x008b, code lost:
        r1 = r6.zznt.zzng;
     */
    /* JADX WARNING: Code restructure failed: missing block: B:45:0x0091, code lost:
        monitor-enter(r1);
     */
    /* JADX WARNING: Code restructure failed: missing block: B:47:?, code lost:
        r6.zznt.zznh.release();
        r6.zznt.zzng.notifyAll();
     */
    /* JADX WARNING: Code restructure failed: missing block: B:48:0x00aa, code lost:
        if (r6 != r6.zznt.zzna) goto L_0x00bc;
     */
    /* JADX WARNING: Code restructure failed: missing block: B:49:0x00ac, code lost:
        r6.zznt.zzna = null;
     */
    /* JADX WARNING: Code restructure failed: missing block: B:50:0x00b2, code lost:
        monitor-exit(r1);
     */
    /* JADX WARNING: Code restructure failed: missing block: B:51:0x00b3, code lost:
        return;
     */
    /* JADX WARNING: Code restructure failed: missing block: B:61:0x00c2, code lost:
        if (r6 != r6.zznt.zznb) goto L_0x00ce;
     */
    /* JADX WARNING: Code restructure failed: missing block: B:62:0x00c4, code lost:
        r6.zznt.zznb = null;
     */
    /* JADX WARNING: Code restructure failed: missing block: B:67:?, code lost:
        r6.zznt.zzab().zzgk().zzao("Current scheduler thread is neither worker nor network");
     */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    public final void run() {
        /*
            r6 = this;
            r0 = 0
            r1 = r0
        L_0x0002:
            if (r1 != 0) goto L_0x0015
            com.google.android.gms.measurement.internal.zzfc r0 = r6.zznt     // Catch:{ InterruptedException -> 0x0010 }
            java.util.concurrent.Semaphore r0 = r0.zznh     // Catch:{ InterruptedException -> 0x0010 }
            r0.acquire()     // Catch:{ InterruptedException -> 0x0010 }
            r0 = 1
            r1 = r0
            goto L_0x0002
        L_0x0010:
            r0 = move-exception
            r6.zza(r0)
            goto L_0x0002
        L_0x0015:
            int r0 = android.os.Process.myTid()     // Catch:{ all -> 0x0033 }
            int r2 = android.os.Process.getThreadPriority(r0)     // Catch:{ all -> 0x0033 }
        L_0x001d:
            java.util.concurrent.BlockingQueue<com.google.android.gms.measurement.internal.zzfh<?>> r0 = r6.zznv     // Catch:{ all -> 0x0033 }
            java.lang.Object r0 = r0.poll()     // Catch:{ all -> 0x0033 }
            com.google.android.gms.measurement.internal.zzfh r0 = (com.google.android.gms.measurement.internal.zzfh) r0     // Catch:{ all -> 0x0033 }
            if (r0 == 0) goto L_0x0060
            boolean r1 = r0.zznx     // Catch:{ all -> 0x0033 }
            if (r1 == 0) goto L_0x005d
            r1 = r2
        L_0x002c:
            android.os.Process.setThreadPriority(r1)     // Catch:{ all -> 0x0033 }
            r0.run()     // Catch:{ all -> 0x0033 }
            goto L_0x001d
        L_0x0033:
            r0 = move-exception
            com.google.android.gms.measurement.internal.zzfc r1 = r6.zznt
            java.lang.Object r1 = r1.zzng
            monitor-enter(r1)
            com.google.android.gms.measurement.internal.zzfc r2 = r6.zznt     // Catch:{ all -> 0x00f4 }
            java.util.concurrent.Semaphore r2 = r2.zznh     // Catch:{ all -> 0x00f4 }
            r2.release()     // Catch:{ all -> 0x00f4 }
            com.google.android.gms.measurement.internal.zzfc r2 = r6.zznt     // Catch:{ all -> 0x00f4 }
            java.lang.Object r2 = r2.zzng     // Catch:{ all -> 0x00f4 }
            r2.notifyAll()     // Catch:{ all -> 0x00f4 }
            com.google.android.gms.measurement.internal.zzfc r2 = r6.zznt     // Catch:{ all -> 0x00f4 }
            com.google.android.gms.measurement.internal.zzfg r2 = r2.zzna     // Catch:{ all -> 0x00f4 }
            if (r6 != r2) goto L_0x00e4
            com.google.android.gms.measurement.internal.zzfc r2 = r6.zznt     // Catch:{ all -> 0x00f4 }
            r3 = 0
            r2.zzna = null     // Catch:{ all -> 0x00f4 }
        L_0x005b:
            monitor-exit(r1)     // Catch:{ all -> 0x00f4 }
            throw r0
        L_0x005d:
            r1 = 10
            goto L_0x002c
        L_0x0060:
            java.lang.Object r1 = r6.zznu     // Catch:{ all -> 0x0033 }
            monitor-enter(r1)     // Catch:{ all -> 0x0033 }
            java.util.concurrent.BlockingQueue<com.google.android.gms.measurement.internal.zzfh<?>> r0 = r6.zznv     // Catch:{ all -> 0x00b9 }
            java.lang.Object r0 = r0.peek()     // Catch:{ all -> 0x00b9 }
            if (r0 != 0) goto L_0x007a
            com.google.android.gms.measurement.internal.zzfc r0 = r6.zznt     // Catch:{ all -> 0x00b9 }
            boolean r0 = r0.zzni     // Catch:{ all -> 0x00b9 }
            if (r0 != 0) goto L_0x007a
            java.lang.Object r0 = r6.zznu     // Catch:{ InterruptedException -> 0x00b4 }
            r4 = 30000(0x7530, double:1.4822E-319)
            r0.wait(r4)     // Catch:{ InterruptedException -> 0x00b4 }
        L_0x007a:
            monitor-exit(r1)     // Catch:{ all -> 0x00b9 }
            com.google.android.gms.measurement.internal.zzfc r0 = r6.zznt     // Catch:{ all -> 0x0033 }
            java.lang.Object r1 = r0.zzng     // Catch:{ all -> 0x0033 }
            monitor-enter(r1)     // Catch:{ all -> 0x0033 }
            java.util.concurrent.BlockingQueue<com.google.android.gms.measurement.internal.zzfh<?>> r0 = r6.zznv     // Catch:{ all -> 0x00e1 }
            java.lang.Object r0 = r0.peek()     // Catch:{ all -> 0x00e1 }
            if (r0 != 0) goto L_0x00de
            monitor-exit(r1)     // Catch:{ all -> 0x00e1 }
            com.google.android.gms.measurement.internal.zzfc r0 = r6.zznt
            java.lang.Object r1 = r0.zzng
            monitor-enter(r1)
            com.google.android.gms.measurement.internal.zzfc r0 = r6.zznt     // Catch:{ all -> 0x00cb }
            java.util.concurrent.Semaphore r0 = r0.zznh     // Catch:{ all -> 0x00cb }
            r0.release()     // Catch:{ all -> 0x00cb }
            com.google.android.gms.measurement.internal.zzfc r0 = r6.zznt     // Catch:{ all -> 0x00cb }
            java.lang.Object r0 = r0.zzng     // Catch:{ all -> 0x00cb }
            r0.notifyAll()     // Catch:{ all -> 0x00cb }
            com.google.android.gms.measurement.internal.zzfc r0 = r6.zznt     // Catch:{ all -> 0x00cb }
            com.google.android.gms.measurement.internal.zzfg r0 = r0.zzna     // Catch:{ all -> 0x00cb }
            if (r6 != r0) goto L_0x00bc
            com.google.android.gms.measurement.internal.zzfc r0 = r6.zznt     // Catch:{ all -> 0x00cb }
            r2 = 0
            r0.zzna = null     // Catch:{ all -> 0x00cb }
        L_0x00b2:
            monitor-exit(r1)     // Catch:{ all -> 0x00cb }
            return
        L_0x00b4:
            r0 = move-exception
            r6.zza(r0)     // Catch:{ all -> 0x00b9 }
            goto L_0x007a
        L_0x00b9:
            r0 = move-exception
            monitor-exit(r1)     // Catch:{ all -> 0x00b9 }
            throw r0     // Catch:{ all -> 0x0033 }
        L_0x00bc:
            com.google.android.gms.measurement.internal.zzfc r0 = r6.zznt     // Catch:{ all -> 0x00cb }
            com.google.android.gms.measurement.internal.zzfg r0 = r0.zznb     // Catch:{ all -> 0x00cb }
            if (r6 != r0) goto L_0x00ce
            com.google.android.gms.measurement.internal.zzfc r0 = r6.zznt     // Catch:{ all -> 0x00cb }
            r2 = 0
            r0.zznb = null     // Catch:{ all -> 0x00cb }
            goto L_0x00b2
        L_0x00cb:
            r0 = move-exception
            monitor-exit(r1)     // Catch:{ all -> 0x00cb }
            throw r0
        L_0x00ce:
            com.google.android.gms.measurement.internal.zzfc r0 = r6.zznt     // Catch:{ all -> 0x00cb }
            com.google.android.gms.measurement.internal.zzef r0 = r0.zzab()     // Catch:{ all -> 0x00cb }
            com.google.android.gms.measurement.internal.zzeh r0 = r0.zzgk()     // Catch:{ all -> 0x00cb }
            java.lang.String r2 = "Current scheduler thread is neither worker nor network"
            r0.zzao(r2)     // Catch:{ all -> 0x00cb }
            goto L_0x00b2
        L_0x00de:
            monitor-exit(r1)     // Catch:{ all -> 0x00e1 }
            goto L_0x001d
        L_0x00e1:
            r0 = move-exception
            monitor-exit(r1)     // Catch:{ all -> 0x00e1 }
            throw r0     // Catch:{ all -> 0x0033 }
        L_0x00e4:
            com.google.android.gms.measurement.internal.zzfc r2 = r6.zznt     // Catch:{ all -> 0x00f4 }
            com.google.android.gms.measurement.internal.zzfg r2 = r2.zznb     // Catch:{ all -> 0x00f4 }
            if (r6 != r2) goto L_0x00f7
            com.google.android.gms.measurement.internal.zzfc r2 = r6.zznt     // Catch:{ all -> 0x00f4 }
            r3 = 0
            r2.zznb = null     // Catch:{ all -> 0x00f4 }
            goto L_0x005b
        L_0x00f4:
            r0 = move-exception
            monitor-exit(r1)     // Catch:{ all -> 0x00f4 }
            throw r0
        L_0x00f7:
            com.google.android.gms.measurement.internal.zzfc r2 = r6.zznt     // Catch:{ all -> 0x00f4 }
            com.google.android.gms.measurement.internal.zzef r2 = r2.zzab()     // Catch:{ all -> 0x00f4 }
            com.google.android.gms.measurement.internal.zzeh r2 = r2.zzgk()     // Catch:{ all -> 0x00f4 }
            java.lang.String r3 = "Current scheduler thread is neither worker nor network"
            r2.zzao(r3)     // Catch:{ all -> 0x00f4 }
            goto L_0x005b
        */
        throw new UnsupportedOperationException("Method not decompiled: com.google.android.gms.measurement.internal.zzfg.run():void");
    }

    public final void zzhr() {
        synchronized (this.zznu) {
            this.zznu.notifyAll();
        }
    }
}
