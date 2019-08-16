package com.google.android.gms.measurement.internal;

import android.support.annotation.GuardedBy;
import android.support.annotation.NonNull;
import android.support.annotation.Nullable;
import com.google.android.gms.common.util.VisibleForTesting;

@VisibleForTesting
public final class zzdu<V> {
    private static final Object zzjo = new Object();
    private final String zzjj;
    private final zzdv<V> zzjk;
    private final V zzjl;
    private final V zzjm;
    private final Object zzjn;
    @GuardedBy("overrideLock")
    private volatile V zzjp;
    @GuardedBy("cachingLock")
    private volatile V zzjq;

    private zzdu(@NonNull String str, @NonNull V v, @NonNull V v2, @Nullable zzdv<V> zzdv) {
        this.zzjn = new Object();
        this.zzjp = null;
        this.zzjq = null;
        this.zzjj = str;
        this.zzjl = v;
        this.zzjm = v2;
        this.zzjk = zzdv;
    }

    /* JADX WARNING: Code restructure failed: missing block: B:70:?, code lost:
        return r6;
     */
    /* JADX WARNING: No exception handlers in catch block: Catch:{  } */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    public final V get(@android.support.annotation.Nullable V r6) {
        /*
            r5 = this;
            java.lang.Object r1 = r5.zzjn
            monitor-enter(r1)
            V r0 = r5.zzjp     // Catch:{ all -> 0x0009 }
            monitor-exit(r1)     // Catch:{ all -> 0x0009 }
            if (r6 == 0) goto L_0x000c
        L_0x0008:
            return r6
        L_0x0009:
            r0 = move-exception
            monitor-exit(r1)     // Catch:{ all -> 0x0009 }
            throw r0
        L_0x000c:
            com.google.android.gms.measurement.internal.zzr r0 = com.google.android.gms.measurement.internal.zzak.zzfv
            if (r0 != 0) goto L_0x0013
            V r6 = r5.zzjl
            goto L_0x0008
        L_0x0013:
            com.google.android.gms.measurement.internal.zzr r0 = com.google.android.gms.measurement.internal.zzak.zzfv
            java.lang.Object r2 = zzjo
            monitor-enter(r2)
            boolean r0 = com.google.android.gms.measurement.internal.zzr.isMainThread()     // Catch:{ all -> 0x0026 }
            if (r0 == 0) goto L_0x002c
            V r0 = r5.zzjq     // Catch:{ all -> 0x0026 }
            if (r0 != 0) goto L_0x0029
            V r6 = r5.zzjl     // Catch:{ all -> 0x0026 }
        L_0x0024:
            monitor-exit(r2)     // Catch:{ all -> 0x0026 }
            goto L_0x0008
        L_0x0026:
            r0 = move-exception
            monitor-exit(r2)     // Catch:{ all -> 0x0026 }
            throw r0
        L_0x0029:
            V r6 = r5.zzjq     // Catch:{ all -> 0x0026 }
            goto L_0x0024
        L_0x002c:
            boolean r0 = com.google.android.gms.measurement.internal.zzr.isMainThread()     // Catch:{ all -> 0x0026 }
            if (r0 != 0) goto L_0x006a
            com.google.android.gms.measurement.internal.zzr r0 = com.google.android.gms.measurement.internal.zzak.zzfv     // Catch:{ all -> 0x0026 }
            java.util.List r0 = com.google.android.gms.measurement.internal.zzak.zzfw     // Catch:{ SecurityException -> 0x005c }
            java.util.Iterator r3 = r0.iterator()     // Catch:{ SecurityException -> 0x005c }
        L_0x003c:
            boolean r0 = r3.hasNext()     // Catch:{ SecurityException -> 0x005c }
            if (r0 == 0) goto L_0x0060
            java.lang.Object r0 = r3.next()     // Catch:{ SecurityException -> 0x005c }
            com.google.android.gms.measurement.internal.zzdu r0 = (com.google.android.gms.measurement.internal.zzdu) r0     // Catch:{ SecurityException -> 0x005c }
            java.lang.Object r4 = zzjo     // Catch:{ SecurityException -> 0x005c }
            monitor-enter(r4)     // Catch:{ SecurityException -> 0x005c }
            boolean r1 = com.google.android.gms.measurement.internal.zzr.isMainThread()     // Catch:{ all -> 0x0059 }
            if (r1 == 0) goto L_0x0072
            java.lang.IllegalStateException r0 = new java.lang.IllegalStateException     // Catch:{ all -> 0x0059 }
            java.lang.String r1 = "Refreshing flag cache must be done on a worker thread."
            r0.<init>(r1)     // Catch:{ all -> 0x0059 }
            throw r0     // Catch:{ all -> 0x0059 }
        L_0x0059:
            r0 = move-exception
            monitor-exit(r4)     // Catch:{ all -> 0x0059 }
            throw r0     // Catch:{ SecurityException -> 0x005c }
        L_0x005c:
            r0 = move-exception
            com.google.android.gms.measurement.internal.zzak.zza(r0)     // Catch:{ all -> 0x0026 }
        L_0x0060:
            monitor-exit(r2)     // Catch:{ all -> 0x0026 }
            com.google.android.gms.measurement.internal.zzdv<V> r0 = r5.zzjk
            if (r0 != 0) goto L_0x0082
            com.google.android.gms.measurement.internal.zzr r0 = com.google.android.gms.measurement.internal.zzak.zzfv
            V r6 = r5.zzjl
            goto L_0x0008
        L_0x006a:
            java.lang.IllegalStateException r0 = new java.lang.IllegalStateException     // Catch:{ all -> 0x0026 }
            java.lang.String r1 = "Tried to refresh flag cache on main thread or on package side."
            r0.<init>(r1)     // Catch:{ all -> 0x0026 }
            throw r0     // Catch:{ all -> 0x0026 }
        L_0x0072:
            com.google.android.gms.measurement.internal.zzdv<V> r1 = r0.zzjk     // Catch:{ all -> 0x0059 }
            if (r1 == 0) goto L_0x0080
            com.google.android.gms.measurement.internal.zzdv<V> r1 = r0.zzjk     // Catch:{ all -> 0x0059 }
            java.lang.Object r1 = r1.get()     // Catch:{ all -> 0x0059 }
        L_0x007c:
            r0.zzjq = r1     // Catch:{ all -> 0x0059 }
            monitor-exit(r4)     // Catch:{ all -> 0x0059 }
            goto L_0x003c
        L_0x0080:
            r1 = 0
            goto L_0x007c
        L_0x0082:
            com.google.android.gms.measurement.internal.zzdv<V> r0 = r5.zzjk     // Catch:{ SecurityException -> 0x0089 }
            java.lang.Object r6 = r0.get()     // Catch:{ SecurityException -> 0x0089 }
            goto L_0x0008
        L_0x0089:
            r0 = move-exception
            com.google.android.gms.measurement.internal.zzak.zza(r0)
            com.google.android.gms.measurement.internal.zzr r0 = com.google.android.gms.measurement.internal.zzak.zzfv
            V r6 = r5.zzjl
            goto L_0x0008
        */
        throw new UnsupportedOperationException("Method not decompiled: com.google.android.gms.measurement.internal.zzdu.get(java.lang.Object):java.lang.Object");
    }

    public final String getKey() {
        return this.zzjj;
    }
}
