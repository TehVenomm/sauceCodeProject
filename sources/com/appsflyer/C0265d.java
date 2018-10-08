package com.appsflyer;

import android.content.Context;
import android.support.annotation.NonNull;
import java.lang.ref.WeakReference;

/* renamed from: com.appsflyer.d */
final class C0265d {
    /* renamed from: ˎ */
    private long f223;
    /* renamed from: ˏ */
    private String f224;
    /* renamed from: ॱ */
    private final Object f225;

    C0265d() {
    }

    /* renamed from: ˋ */
    static void m287(Context context) {
        Context applicationContext = context.getApplicationContext();
        AFLogger.afInfoLog("onBecameBackground");
        AppsFlyerLib.getInstance().m260();
        AFLogger.afInfoLog("callStatsBackground background call");
        AppsFlyerLib.getInstance().m261(new WeakReference(applicationContext));
        C0300y ˋ = C0300y.m378();
        if (ˋ.m385()) {
            ˋ.m394();
            if (applicationContext != null) {
                C0300y.m377(applicationContext.getPackageName(), applicationContext.getPackageManager());
            }
            ˋ.m386();
        } else {
            AFLogger.afDebugLog("RD status is OFF");
        }
        AFExecutor.getInstance().m178();
    }

    C0265d(long j, String str) {
        this.f225 = new Object();
        this.f223 = 0;
        this.f224 = "";
        this.f223 = j;
        this.f224 = str;
    }

    C0265d(String str) {
        this(System.currentTimeMillis(), str);
    }

    @NonNull
    /* renamed from: ˊ */
    static C0265d m286(String str) {
        if (str == null) {
            return new C0265d(0, "");
        }
        String[] split = str.split(",");
        if (split.length < 2) {
            return new C0265d(0, "");
        }
        return new C0265d(Long.parseLong(split[0]), split[1]);
    }

    /* renamed from: ˊ */
    final boolean m290(C0265d c0265d) {
        return m288(c0265d.f223, c0265d.f224);
    }

    /* JADX WARNING: inconsistent code. */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    /* renamed from: ˋ */
    private boolean m288(long r10, java.lang.String r12) {
        /*
        r9 = this;
        r0 = 1;
        r1 = 0;
        r3 = r9.f225;
        monitor-enter(r3);
        if (r12 == 0) goto L_0x0024;
    L_0x0007:
        r2 = r9.f224;	 Catch:{ all -> 0x0027 }
        r2 = r12.equals(r2);	 Catch:{ all -> 0x0027 }
        if (r2 != 0) goto L_0x0024;
    L_0x000f:
        r4 = r9.f223;	 Catch:{ all -> 0x0027 }
        r4 = r10 - r4;
        r6 = 2000; // 0x7d0 float:2.803E-42 double:9.88E-321;
        r2 = (r4 > r6 ? 1 : (r4 == r6 ? 0 : -1));
        if (r2 <= 0) goto L_0x0022;
    L_0x0019:
        r2 = r0;
    L_0x001a:
        if (r2 == 0) goto L_0x0024;
    L_0x001c:
        r9.f223 = r10;	 Catch:{ all -> 0x0027 }
        r9.f224 = r12;	 Catch:{ all -> 0x0027 }
        monitor-exit(r3);	 Catch:{ all -> 0x0027 }
    L_0x0021:
        return r0;
    L_0x0022:
        r2 = r1;
        goto L_0x001a;
    L_0x0024:
        monitor-exit(r3);
        r0 = r1;
        goto L_0x0021;
    L_0x0027:
        r0 = move-exception;
        monitor-exit(r3);
        throw r0;
        */
        throw new UnsupportedOperationException("Method not decompiled: com.appsflyer.d.ˋ(long, java.lang.String):boolean");
    }

    public final String toString() {
        return new StringBuilder().append(this.f223).append(",").append(this.f224).toString();
    }

    /* renamed from: ˊ */
    final String m289() {
        return this.f224;
    }
}
