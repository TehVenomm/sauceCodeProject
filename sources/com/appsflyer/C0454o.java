package com.appsflyer;

import android.content.ContentResolver;
import android.os.Build;
import android.provider.Settings.Secure;
import com.appsflyer.share.Constants;

/* renamed from: com.appsflyer.o */
final class C0454o {
    C0454o() {
    }

    /* renamed from: ˊ */
    static C0452n m324(ContentResolver contentResolver) {
        if (contentResolver == null || AppsFlyerProperties.getInstance().getString("amazon_aid") != null || !"Amazon".equals(Build.MANUFACTURER)) {
            return null;
        }
        int i = Secure.getInt(contentResolver, "limit_ad_tracking", 2);
        if (i == 0) {
            return new C0452n(C0453b.AMAZON, Secure.getString(contentResolver, Constants.URL_ADVERTISING_ID), false);
        } else if (i == 2) {
            return null;
        } else {
            String str = "";
            try {
                str = Secure.getString(contentResolver, Constants.URL_ADVERTISING_ID);
            } catch (Throwable th) {
                AFLogger.afErrorLog("Couldn't fetch Amazon Advertising ID (Ad-Tracking is limited!)", th);
            }
            return new C0452n(C0453b.AMAZON, str, true);
        }
    }

    /* JADX WARNING: Removed duplicated region for block: B:17:0x003d  */
    /* JADX WARNING: Removed duplicated region for block: B:19:0x0055  */
    /* JADX WARNING: Removed duplicated region for block: B:21:0x0073 A[ADDED_TO_REGION] */
    /* JADX WARNING: Removed duplicated region for block: B:33:0x00d1 A[SYNTHETIC, Splitter:B:33:0x00d1] */
    /* JADX WARNING: Removed duplicated region for block: B:57:? A[ADDED_TO_REGION, RETURN, SYNTHETIC] */
    /* renamed from: ˊ */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    static void m325(android.content.Context r9, java.util.Map<java.lang.String, java.lang.Object> r10) {
        /*
            r1 = 0
            r3 = 0
            r2 = 1
            java.lang.String r0 = "Trying to fetch GAID.."
            com.appsflyer.AFLogger.afInfoLog(r0)
            r6 = -1
            java.lang.String r0 = "com.google.android.gms.ads.identifier.AdvertisingIdClient"
            java.lang.Class.forName(r0)     // Catch:{ Throwable -> 0x00a4 }
            com.google.android.gms.ads.identifier.AdvertisingIdClient$Info r0 = com.google.android.gms.ads.identifier.AdvertisingIdClient.getAdvertisingIdInfo(r9)     // Catch:{ Throwable -> 0x00a4 }
            if (r0 == 0) goto L_0x009d
            java.lang.String r5 = r0.getId()     // Catch:{ Throwable -> 0x00a4 }
            boolean r0 = r0.isLimitAdTrackingEnabled()     // Catch:{ Throwable -> 0x0154 }
            if (r0 != 0) goto L_0x009b
            r0 = r2
        L_0x001f:
            java.lang.String r4 = java.lang.Boolean.toString(r0)     // Catch:{ Throwable -> 0x0154 }
            if (r5 == 0) goto L_0x002b
            int r0 = r5.length()     // Catch:{ Throwable -> 0x015a }
            if (r0 != 0) goto L_0x015f
        L_0x002b:
            java.lang.String r1 = "emptyOrNull"
        L_0x002d:
            java.lang.Class r0 = r9.getClass()
            java.lang.String r0 = r0.getName()
            java.lang.String r3 = "android.app.ReceiverRestrictedContext"
            boolean r0 = r0.equals(r3)
            if (r0 == 0) goto L_0x0053
            com.appsflyer.AppsFlyerProperties r0 = com.appsflyer.AppsFlyerProperties.getInstance()
            java.lang.String r1 = "advertiserId"
            java.lang.String r5 = r0.getString(r1)
            com.appsflyer.AppsFlyerProperties r0 = com.appsflyer.AppsFlyerProperties.getInstance()
            java.lang.String r1 = "advertiserIdEnabled"
            java.lang.String r4 = r0.getString(r1)
            java.lang.String r1 = "context = android.app.ReceiverRestrictedContext"
        L_0x0053:
            if (r1 == 0) goto L_0x0071
            java.lang.String r0 = "gaidError"
            java.lang.StringBuilder r3 = new java.lang.StringBuilder
            r3.<init>()
            java.lang.StringBuilder r3 = r3.append(r6)
            java.lang.String r6 = ": "
            java.lang.StringBuilder r3 = r3.append(r6)
            java.lang.StringBuilder r1 = r3.append(r1)
            java.lang.String r1 = r1.toString()
            r10.put(r0, r1)
        L_0x0071:
            if (r5 == 0) goto L_0x009a
            if (r4 == 0) goto L_0x009a
            java.lang.String r0 = "advertiserId"
            r10.put(r0, r5)
            java.lang.String r0 = "advertiserIdEnabled"
            r10.put(r0, r4)
            com.appsflyer.AppsFlyerProperties r0 = com.appsflyer.AppsFlyerProperties.getInstance()
            java.lang.String r1 = "advertiserId"
            r0.set(r1, r5)
            com.appsflyer.AppsFlyerProperties r0 = com.appsflyer.AppsFlyerProperties.getInstance()
            java.lang.String r1 = "advertiserIdEnabled"
            r0.set(r1, r4)
            java.lang.String r0 = "isGaidWithGps"
            java.lang.String r1 = java.lang.String.valueOf(r2)
            r10.put(r0, r1)
        L_0x009a:
            return
        L_0x009b:
            r0 = r3
            goto L_0x001f
        L_0x009d:
            java.lang.String r0 = "gpsAdInfo-null"
            r2 = r3
            r4 = r1
            r5 = r1
        L_0x00a2:
            r1 = r0
            goto L_0x002d
        L_0x00a4:
            r0 = move-exception
            r8 = r0
            r7 = r3
            r4 = r1
            r5 = r1
        L_0x00a9:
            java.lang.String r0 = r8.getMessage()
            com.appsflyer.AFLogger.afErrorLog(r0, r8)
            com.google.android.gms.common.GoogleApiAvailability r0 = com.google.android.gms.common.GoogleApiAvailability.getInstance()     // Catch:{ Throwable -> 0x00f1 }
            int r0 = r0.isGooglePlayServicesAvailable(r9)     // Catch:{ Throwable -> 0x00f1 }
        L_0x00b8:
            java.lang.Class r1 = r8.getClass()
            java.lang.String r1 = r1.getSimpleName()
            java.lang.String r6 = "WARNING: Google Play Services is missing."
            com.appsflyer.AFLogger.afInfoLog(r6)
            com.appsflyer.AppsFlyerProperties r6 = com.appsflyer.AppsFlyerProperties.getInstance()
            java.lang.String r8 = "enableGpsFallback"
            boolean r6 = r6.getBoolean(r8, r2)
            if (r6 == 0) goto L_0x0150
            com.appsflyer.m$a r4 = com.appsflyer.C0448m.m316(r9)     // Catch:{ Throwable -> 0x00fd }
            java.lang.String r5 = r4.mo6589()     // Catch:{ Throwable -> 0x00fd }
            boolean r4 = r4.mo6588()     // Catch:{ Throwable -> 0x00fd }
            if (r4 != 0) goto L_0x00fb
        L_0x00df:
            java.lang.String r4 = java.lang.Boolean.toString(r2)     // Catch:{ Throwable -> 0x00fd }
            if (r5 == 0) goto L_0x00eb
            int r2 = r5.length()     // Catch:{ Throwable -> 0x00fd }
            if (r2 != 0) goto L_0x00ed
        L_0x00eb:
            java.lang.String r1 = "emptyOrNull (bypass)"
        L_0x00ed:
            r6 = r0
            r2 = r7
            goto L_0x002d
        L_0x00f1:
            r0 = move-exception
            java.lang.String r1 = r0.getMessage()
            com.appsflyer.AFLogger.afErrorLog(r1, r0)
            r0 = r6
            goto L_0x00b8
        L_0x00fb:
            r2 = r3
            goto L_0x00df
        L_0x00fd:
            r2 = move-exception
            java.lang.String r3 = r2.getMessage()
            com.appsflyer.AFLogger.afErrorLog(r3, r2)
            java.lang.StringBuilder r3 = new java.lang.StringBuilder
            r3.<init>()
            java.lang.StringBuilder r1 = r3.append(r1)
            java.lang.String r3 = "/"
            java.lang.StringBuilder r1 = r1.append(r3)
            java.lang.Class r3 = r2.getClass()
            java.lang.String r3 = r3.getSimpleName()
            java.lang.StringBuilder r1 = r1.append(r3)
            java.lang.String r1 = r1.toString()
            com.appsflyer.AppsFlyerProperties r3 = com.appsflyer.AppsFlyerProperties.getInstance()
            java.lang.String r4 = "advertiserId"
            java.lang.String r5 = r3.getString(r4)
            com.appsflyer.AppsFlyerProperties r3 = com.appsflyer.AppsFlyerProperties.getInstance()
            java.lang.String r4 = "advertiserIdEnabled"
            java.lang.String r4 = r3.getString(r4)
            java.lang.String r3 = r2.getLocalizedMessage()
            if (r3 == 0) goto L_0x0149
            java.lang.String r2 = r2.getLocalizedMessage()
            com.appsflyer.AFLogger.afInfoLog(r2)
            r6 = r0
            r2 = r7
            goto L_0x002d
        L_0x0149:
            java.lang.String r2 = r2.toString()
            com.appsflyer.AFLogger.afInfoLog(r2)
        L_0x0150:
            r6 = r0
            r2 = r7
            goto L_0x002d
        L_0x0154:
            r0 = move-exception
            r8 = r0
            r7 = r3
            r4 = r1
            goto L_0x00a9
        L_0x015a:
            r0 = move-exception
            r8 = r0
            r7 = r2
            goto L_0x00a9
        L_0x015f:
            r0 = r1
            goto L_0x00a2
        */
        throw new UnsupportedOperationException("Method not decompiled: com.appsflyer.C0454o.m325(android.content.Context, java.util.Map):void");
    }
}
