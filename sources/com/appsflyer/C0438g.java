package com.appsflyer;

import android.content.Context;
import android.support.annotation.NonNull;
import android.support.annotation.Nullable;
import android.support.p000v4.content.ContextCompat;

/* renamed from: com.appsflyer.g */
final class C0438g {

    /* renamed from: com.appsflyer.g$a */
    static final class C0439a {

        /* renamed from: ˋ */
        private final String f267;

        /* renamed from: ˏ */
        private final String f268;

        /* renamed from: ॱ */
        private final String f269;

        C0439a(@NonNull String str, @Nullable String str2, @Nullable String str3) {
            this.f267 = str;
            this.f269 = str2;
            this.f268 = str3;
        }

        /* access modifiers changed from: 0000 */
        /* renamed from: ॱ */
        public final String mo6570() {
            return this.f267;
        }

        /* access modifiers changed from: 0000 */
        @Nullable
        /* renamed from: ˊ */
        public final String mo6568() {
            return this.f269;
        }

        /* access modifiers changed from: 0000 */
        @Nullable
        /* renamed from: ˎ */
        public final String mo6569() {
            return this.f268;
        }

        C0439a() {
        }

        /* renamed from: ˎ */
        static boolean m291(Context context, String str) {
            int checkSelfPermission = ContextCompat.checkSelfPermission(context, str);
            AFLogger.afRDLog(new StringBuilder("is Permission Available: ").append(str).append("; res: ").append(checkSelfPermission).toString());
            return checkSelfPermission == 0;
        }
    }

    /* renamed from: com.appsflyer.g$c */
    static final class C0440c {

        /* renamed from: ˏ */
        static final C0438g f270 = new C0438g();
    }

    C0438g() {
    }

    /* JADX WARNING: Code restructure failed: missing block: B:15:0x0032, code lost:
        if (1 != r9.getType()) goto L_0x0060;
     */
    /* JADX WARNING: Code restructure failed: missing block: B:16:0x0034, code lost:
        r3 = "WIFI";
     */
    /* JADX WARNING: Code restructure failed: missing block: B:23:0x004c, code lost:
        if (r1.isEmpty() != false) goto L_0x004e;
     */
    /* JADX WARNING: Code restructure failed: missing block: B:33:0x0064, code lost:
        if (r9.getType() != 0) goto L_0x0069;
     */
    /* JADX WARNING: Code restructure failed: missing block: B:34:0x0066, code lost:
        r3 = "MOBILE";
     */
    /* JADX WARNING: Code restructure failed: missing block: B:35:0x0069, code lost:
        r3 = "unknown";
     */
    /* JADX WARNING: Removed duplicated region for block: B:21:0x0048 A[SYNTHETIC, Splitter:B:21:0x0048] */
    /* JADX WARNING: Removed duplicated region for block: B:26:0x0055 A[Catch:{ Throwable -> 0x00d4 }] */
    /* renamed from: ˊ */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    static com.appsflyer.C0438g.C0439a m290(@android.support.annotation.NonNull android.content.Context r10) {
        /*
            r5 = 0
            r2 = 0
            r1 = 1
            java.lang.String r3 = "unknown"
            java.lang.String r0 = "connectivity"
            java.lang.Object r0 = r10.getSystemService(r0)     // Catch:{ Throwable -> 0x00c5 }
            android.net.ConnectivityManager r0 = (android.net.ConnectivityManager) r0     // Catch:{ Throwable -> 0x00c5 }
            if (r0 == 0) goto L_0x00c1
            r4 = 21
            int r6 = android.os.Build.VERSION.SDK_INT     // Catch:{ Throwable -> 0x00c5 }
            if (r4 > r6) goto L_0x0073
            android.net.Network[] r7 = r0.getAllNetworks()     // Catch:{ Throwable -> 0x00c5 }
            int r8 = r7.length     // Catch:{ Throwable -> 0x00c5 }
            r6 = r2
        L_0x001b:
            if (r6 >= r8) goto L_0x0070
            r4 = r7[r6]     // Catch:{ Throwable -> 0x00c5 }
            android.net.NetworkInfo r9 = r0.getNetworkInfo(r4)     // Catch:{ Throwable -> 0x00c5 }
            if (r9 == 0) goto L_0x005e
            boolean r4 = r9.isConnectedOrConnecting()     // Catch:{ Throwable -> 0x00c5 }
            if (r4 == 0) goto L_0x005e
            r4 = r1
        L_0x002c:
            if (r4 == 0) goto L_0x006c
            int r0 = r9.getType()     // Catch:{ Throwable -> 0x00c5 }
            if (r1 != r0) goto L_0x0060
            java.lang.String r3 = "WIFI"
        L_0x0036:
            java.lang.String r0 = "phone"
            java.lang.Object r0 = r10.getSystemService(r0)     // Catch:{ Throwable -> 0x00c5 }
            android.telephony.TelephonyManager r0 = (android.telephony.TelephonyManager) r0     // Catch:{ Throwable -> 0x00c5 }
            java.lang.String r2 = r0.getSimOperatorName()     // Catch:{ Throwable -> 0x00c5 }
            java.lang.String r1 = r0.getNetworkOperatorName()     // Catch:{ Throwable -> 0x00d0 }
            if (r1 == 0) goto L_0x004e
            boolean r4 = r1.isEmpty()     // Catch:{ Throwable -> 0x00d4 }
            if (r4 == 0) goto L_0x00d7
        L_0x004e:
            r4 = 2
            int r0 = r0.getPhoneType()     // Catch:{ Throwable -> 0x00d4 }
            if (r4 != r0) goto L_0x00d7
            java.lang.String r0 = "CDMA"
        L_0x0057:
            r1 = r2
        L_0x0058:
            com.appsflyer.g$a r2 = new com.appsflyer.g$a
            r2.<init>(r3, r0, r1)
            return r2
        L_0x005e:
            r4 = r2
            goto L_0x002c
        L_0x0060:
            int r0 = r9.getType()     // Catch:{ Throwable -> 0x00c5 }
            if (r0 != 0) goto L_0x0069
            java.lang.String r3 = "MOBILE"
            goto L_0x0036
        L_0x0069:
            java.lang.String r3 = "unknown"
            goto L_0x0036
        L_0x006c:
            int r4 = r6 + 1
            r6 = r4
            goto L_0x001b
        L_0x0070:
            java.lang.String r3 = "unknown"
            goto L_0x0036
        L_0x0073:
            r4 = 1
            android.net.NetworkInfo r4 = r0.getNetworkInfo(r4)     // Catch:{ Throwable -> 0x00c5 }
            if (r4 == 0) goto L_0x0086
            boolean r4 = r4.isConnectedOrConnecting()     // Catch:{ Throwable -> 0x00c5 }
            if (r4 == 0) goto L_0x0086
            r4 = r1
        L_0x0081:
            if (r4 == 0) goto L_0x0088
            java.lang.String r3 = "WIFI"
            goto L_0x0036
        L_0x0086:
            r4 = r2
            goto L_0x0081
        L_0x0088:
            r4 = 0
            android.net.NetworkInfo r4 = r0.getNetworkInfo(r4)     // Catch:{ Throwable -> 0x00c5 }
            if (r4 == 0) goto L_0x009b
            boolean r4 = r4.isConnectedOrConnecting()     // Catch:{ Throwable -> 0x00c5 }
            if (r4 == 0) goto L_0x009b
            r4 = r1
        L_0x0096:
            if (r4 == 0) goto L_0x009d
            java.lang.String r3 = "MOBILE"
            goto L_0x0036
        L_0x009b:
            r4 = r2
            goto L_0x0096
        L_0x009d:
            android.net.NetworkInfo r4 = r0.getActiveNetworkInfo()     // Catch:{ Throwable -> 0x00c5 }
            if (r4 == 0) goto L_0x00b5
            boolean r0 = r4.isConnectedOrConnecting()     // Catch:{ Throwable -> 0x00c5 }
            if (r0 == 0) goto L_0x00b5
            r0 = r1
        L_0x00aa:
            if (r0 == 0) goto L_0x00c1
            int r0 = r4.getType()     // Catch:{ Throwable -> 0x00c5 }
            if (r1 != r0) goto L_0x00b7
            java.lang.String r3 = "WIFI"
            goto L_0x0036
        L_0x00b5:
            r0 = r2
            goto L_0x00aa
        L_0x00b7:
            int r0 = r4.getType()     // Catch:{ Throwable -> 0x00c5 }
            if (r0 != 0) goto L_0x00c1
            java.lang.String r3 = "MOBILE"
            goto L_0x0036
        L_0x00c1:
            java.lang.String r3 = "unknown"
            goto L_0x0036
        L_0x00c5:
            r1 = move-exception
            r4 = r1
            r0 = r5
            r2 = r5
        L_0x00c9:
            java.lang.String r1 = "Exception while collecting network info. "
            com.appsflyer.AFLogger.afErrorLog(r1, r4)
            r1 = r2
            goto L_0x0058
        L_0x00d0:
            r1 = move-exception
            r4 = r1
            r0 = r5
            goto L_0x00c9
        L_0x00d4:
            r4 = move-exception
            r0 = r1
            goto L_0x00c9
        L_0x00d7:
            r0 = r1
            goto L_0x0057
        */
        throw new UnsupportedOperationException("Method not decompiled: com.appsflyer.C0438g.m290(android.content.Context):com.appsflyer.g$a");
    }
}
