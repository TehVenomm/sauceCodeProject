package com.appsflyer;

import android.content.Context;
import android.support.annotation.NonNull;
import android.support.annotation.Nullable;
import android.support.v4.content.ContextCompat;

/* renamed from: com.appsflyer.g */
final class C0273g {

    /* renamed from: com.appsflyer.g$a */
    static final class C0271a {
        /* renamed from: ˋ */
        private final String f246;
        /* renamed from: ˏ */
        private final String f247;
        /* renamed from: ॱ */
        private final String f248;

        C0271a(@NonNull String str, @Nullable String str2, @Nullable String str3) {
            this.f246 = str;
            this.f248 = str2;
            this.f247 = str3;
        }

        /* renamed from: ॱ */
        final String m302() {
            return this.f246;
        }

        @Nullable
        /* renamed from: ˊ */
        final String m300() {
            return this.f248;
        }

        @Nullable
        /* renamed from: ˎ */
        final String m301() {
            return this.f247;
        }

        C0271a() {
        }

        /* renamed from: ˎ */
        static boolean m299(Context context, String str) {
            int checkSelfPermission = ContextCompat.checkSelfPermission(context, str);
            AFLogger.afRDLog(new StringBuilder("is Permission Available: ").append(str).append("; res: ").append(checkSelfPermission).toString());
            return checkSelfPermission == 0;
        }
    }

    /* renamed from: com.appsflyer.g$c */
    static final class C0272c {
        /* renamed from: ˏ */
        static final C0273g f249 = new C0273g();
    }

    C0273g() {
    }

    /* JADX WARNING: inconsistent code. */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    /* renamed from: ˊ */
    static com.appsflyer.C0273g.C0271a m303(@android.support.annotation.NonNull android.content.Context r11) {
        /*
        r1 = 0;
        r4 = 0;
        r2 = 1;
        r3 = "unknown";
        r0 = "connectivity";
        r0 = r11.getSystemService(r0);	 Catch:{ Throwable -> 0x00c6 }
        r0 = (android.net.ConnectivityManager) r0;	 Catch:{ Throwable -> 0x00c6 }
        if (r0 == 0) goto L_0x00c2;
    L_0x000f:
        r5 = 21;
        r6 = android.os.Build.VERSION.SDK_INT;	 Catch:{ Throwable -> 0x00c6 }
        if (r5 > r6) goto L_0x0074;
    L_0x0015:
        r7 = r0.getAllNetworks();	 Catch:{ Throwable -> 0x00c6 }
        r8 = r7.length;	 Catch:{ Throwable -> 0x00c6 }
        r6 = r4;
    L_0x001b:
        if (r6 >= r8) goto L_0x0071;
    L_0x001d:
        r5 = r7[r6];	 Catch:{ Throwable -> 0x00c6 }
        r9 = r0.getNetworkInfo(r5);	 Catch:{ Throwable -> 0x00c6 }
        if (r9 == 0) goto L_0x005f;
    L_0x0025:
        r5 = r9.isConnectedOrConnecting();	 Catch:{ Throwable -> 0x00c6 }
        if (r5 == 0) goto L_0x005f;
    L_0x002b:
        r5 = r2;
    L_0x002c:
        if (r5 == 0) goto L_0x006d;
    L_0x002e:
        r0 = r9.getType();	 Catch:{ Throwable -> 0x00c6 }
        if (r2 != r0) goto L_0x0061;
    L_0x0034:
        r3 = "WIFI";
    L_0x0036:
        r0 = "phone";
        r0 = r11.getSystemService(r0);	 Catch:{ Throwable -> 0x00c6 }
        r0 = (android.telephony.TelephonyManager) r0;	 Catch:{ Throwable -> 0x00c6 }
        r2 = r0.getSimOperatorName();	 Catch:{ Throwable -> 0x00c6 }
        r1 = r0.getNetworkOperatorName();	 Catch:{ Throwable -> 0x00d0 }
        if (r1 == 0) goto L_0x004e;
    L_0x0048:
        r4 = r1.isEmpty();	 Catch:{ Throwable -> 0x00d7 }
        if (r4 == 0) goto L_0x00de;
    L_0x004e:
        r4 = 2;
        r0 = r0.getPhoneType();	 Catch:{ Throwable -> 0x00d7 }
        if (r4 != r0) goto L_0x00de;
    L_0x0055:
        r0 = "CDMA";
    L_0x0057:
        r1 = r2;
        r2 = r3;
    L_0x0059:
        r3 = new com.appsflyer.g$a;
        r3.<init>(r2, r0, r1);
        return r3;
    L_0x005f:
        r5 = r4;
        goto L_0x002c;
    L_0x0061:
        r0 = r9.getType();	 Catch:{ Throwable -> 0x00c6 }
        if (r0 != 0) goto L_0x006a;
    L_0x0067:
        r3 = "MOBILE";
        goto L_0x0036;
    L_0x006a:
        r3 = "unknown";
        goto L_0x0036;
    L_0x006d:
        r5 = r6 + 1;
        r6 = r5;
        goto L_0x001b;
    L_0x0071:
        r3 = "unknown";
        goto L_0x0036;
    L_0x0074:
        r5 = 1;
        r5 = r0.getNetworkInfo(r5);	 Catch:{ Throwable -> 0x00c6 }
        if (r5 == 0) goto L_0x0087;
    L_0x007b:
        r5 = r5.isConnectedOrConnecting();	 Catch:{ Throwable -> 0x00c6 }
        if (r5 == 0) goto L_0x0087;
    L_0x0081:
        r5 = r2;
    L_0x0082:
        if (r5 == 0) goto L_0x0089;
    L_0x0084:
        r3 = "WIFI";
        goto L_0x0036;
    L_0x0087:
        r5 = r4;
        goto L_0x0082;
    L_0x0089:
        r5 = 0;
        r5 = r0.getNetworkInfo(r5);	 Catch:{ Throwable -> 0x00c6 }
        if (r5 == 0) goto L_0x009c;
    L_0x0090:
        r5 = r5.isConnectedOrConnecting();	 Catch:{ Throwable -> 0x00c6 }
        if (r5 == 0) goto L_0x009c;
    L_0x0096:
        r5 = r2;
    L_0x0097:
        if (r5 == 0) goto L_0x009e;
    L_0x0099:
        r3 = "MOBILE";
        goto L_0x0036;
    L_0x009c:
        r5 = r4;
        goto L_0x0097;
    L_0x009e:
        r5 = r0.getActiveNetworkInfo();	 Catch:{ Throwable -> 0x00c6 }
        if (r5 == 0) goto L_0x00b6;
    L_0x00a4:
        r0 = r5.isConnectedOrConnecting();	 Catch:{ Throwable -> 0x00c6 }
        if (r0 == 0) goto L_0x00b6;
    L_0x00aa:
        r0 = r2;
    L_0x00ab:
        if (r0 == 0) goto L_0x00c2;
    L_0x00ad:
        r0 = r5.getType();	 Catch:{ Throwable -> 0x00c6 }
        if (r2 != r0) goto L_0x00b8;
    L_0x00b3:
        r3 = "WIFI";
        goto L_0x0036;
    L_0x00b6:
        r0 = r4;
        goto L_0x00ab;
    L_0x00b8:
        r0 = r5.getType();	 Catch:{ Throwable -> 0x00c6 }
        if (r0 != 0) goto L_0x00c2;
    L_0x00be:
        r3 = "MOBILE";
        goto L_0x0036;
    L_0x00c2:
        r3 = "unknown";
        goto L_0x0036;
    L_0x00c6:
        r0 = move-exception;
        r2 = r3;
        r3 = r0;
        r0 = r1;
    L_0x00ca:
        r4 = "Exception while collecting network info. ";
        com.appsflyer.AFLogger.afErrorLog(r4, r3);
        goto L_0x0059;
    L_0x00d0:
        r0 = move-exception;
        r10 = r0;
        r0 = r1;
        r1 = r2;
        r2 = r3;
        r3 = r10;
        goto L_0x00ca;
    L_0x00d7:
        r0 = move-exception;
        r10 = r0;
        r0 = r1;
        r1 = r2;
        r2 = r3;
        r3 = r10;
        goto L_0x00ca;
    L_0x00de:
        r0 = r1;
        goto L_0x0057;
        */
        throw new UnsupportedOperationException("Method not decompiled: com.appsflyer.g.ˊ(android.content.Context):com.appsflyer.g$a");
    }
}
