package com.appsflyer;

import android.content.Context;
import android.support.annotation.NonNull;
import java.lang.ref.WeakReference;

/* renamed from: com.appsflyer.d */
final class C0432d {

    /* renamed from: ˎ */
    private long f244;

    /* renamed from: ˏ */
    private String f245;

    /* renamed from: ॱ */
    private final Object f246;

    C0432d() {
    }

    /* renamed from: ˋ */
    static void m278(Context context) {
        Context applicationContext = context.getApplicationContext();
        AFLogger.afInfoLog("onBecameBackground");
        AppsFlyerLib.getInstance().mo6484();
        AFLogger.afInfoLog("callStatsBackground background call");
        AppsFlyerLib.getInstance().mo6485(new WeakReference<>(applicationContext));
        C0469y r1 = C0469y.m373();
        if (r1.mo6639()) {
            r1.mo6648();
            if (applicationContext != null) {
                C0469y.m372(applicationContext.getPackageName(), applicationContext.getPackageManager());
            }
            r1.mo6640();
        } else {
            AFLogger.afDebugLog("RD status is OFF");
        }
        AFExecutor.getInstance().mo6408();
    }

    C0432d(long j, String str) {
        this.f246 = new Object();
        this.f244 = 0;
        this.f245 = "";
        this.f244 = j;
        this.f245 = str;
    }

    C0432d(String str) {
        this(System.currentTimeMillis(), str);
    }

    @NonNull
    /* renamed from: ˊ */
    static C0432d m277(String str) {
        if (str == null) {
            return new C0432d(0, "");
        }
        String[] split = str.split(",");
        if (split.length < 2) {
            return new C0432d(0, "");
        }
        return new C0432d(Long.parseLong(split[0]), split[1]);
    }

    /* access modifiers changed from: 0000 */
    /* renamed from: ˊ */
    public final boolean mo6558(C0432d dVar) {
        return m279(dVar.f244, dVar.f245);
    }

    /* JADX WARNING: Code restructure failed: missing block: B:19:?, code lost:
        return false;
     */
    /* renamed from: ˋ */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    private boolean m279(long r10, java.lang.String r12) {
        /*
            r9 = this;
            r0 = 1
            r1 = 0
            java.lang.Object r3 = r9.f246
            monitor-enter(r3)
            if (r12 == 0) goto L_0x0024
            java.lang.String r2 = r9.f245     // Catch:{ all -> 0x0027 }
            boolean r2 = r12.equals(r2)     // Catch:{ all -> 0x0027 }
            if (r2 != 0) goto L_0x0024
            long r4 = r9.f244     // Catch:{ all -> 0x0027 }
            long r4 = r10 - r4
            r6 = 2000(0x7d0, double:9.88E-321)
            int r2 = (r4 > r6 ? 1 : (r4 == r6 ? 0 : -1))
            if (r2 <= 0) goto L_0x0022
            r2 = r0
        L_0x001a:
            if (r2 == 0) goto L_0x0024
            r9.f244 = r10     // Catch:{ all -> 0x0027 }
            r9.f245 = r12     // Catch:{ all -> 0x0027 }
            monitor-exit(r3)     // Catch:{ all -> 0x0027 }
        L_0x0021:
            return r0
        L_0x0022:
            r2 = r1
            goto L_0x001a
        L_0x0024:
            monitor-exit(r3)
            r0 = r1
            goto L_0x0021
        L_0x0027:
            r0 = move-exception
            monitor-exit(r3)
            throw r0
        */
        throw new UnsupportedOperationException("Method not decompiled: com.appsflyer.C0432d.m279(long, java.lang.String):boolean");
    }

    public final String toString() {
        return new StringBuilder().append(this.f244).append(",").append(this.f245).toString();
    }

    /* access modifiers changed from: 0000 */
    /* renamed from: ˊ */
    public final String mo6557() {
        return this.f245;
    }
}
