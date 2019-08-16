package com.appsflyer;

import android.content.Context;
import android.content.Intent;
import android.os.Looper;
import java.io.IOException;
import java.lang.ref.WeakReference;
import java.net.HttpURLConnection;
import java.util.HashMap;
import java.util.Map;
import java.util.concurrent.Executors;
import java.util.concurrent.ScheduledExecutorService;
import java.util.concurrent.TimeUnit;
import org.json.JSONObject;

/* renamed from: com.appsflyer.i */
final class C0442i implements Runnable {

    /* renamed from: ʼ */
    private static String f279 = new StringBuilder("https://validate.%s/api/v").append(AppsFlyerLib.f150).append("/androidevent?buildnumber=4.8.11&app_id=").toString();

    /* renamed from: ʻ */
    private ScheduledExecutorService f280;
    /* access modifiers changed from: private */

    /* renamed from: ʽ */
    public Map<String, String> f281;

    /* renamed from: ˊ */
    private String f282;

    /* renamed from: ˊॱ */
    private final Intent f283;

    /* renamed from: ˋ */
    private String f284;

    /* renamed from: ˎ */
    private String f285;

    /* renamed from: ˏ */
    private String f286;
    /* access modifiers changed from: private */

    /* renamed from: ॱ */
    public WeakReference<Context> f287 = null;

    /* renamed from: ॱॱ */
    private String f288;

    /* renamed from: ᐝ */
    private String f289;

    C0442i(Context context, String str, String str2, String str3, String str4, String str5, String str6, Map<String, String> map, ScheduledExecutorService scheduledExecutorService, Intent intent) {
        this.f287 = new WeakReference<>(context);
        this.f282 = str;
        this.f284 = str2;
        this.f286 = str4;
        this.f288 = str5;
        this.f289 = str6;
        this.f281 = map;
        this.f285 = str3;
        this.f280 = scheduledExecutorService;
        this.f283 = intent;
    }

    public final void run() {
        boolean z;
        if (this.f282 != null && this.f282.length() != 0 && !AppsFlyerLib.getInstance().isTrackingStopped()) {
            HttpURLConnection httpURLConnection = null;
            try {
                Context context = (Context) this.f287.get();
                if (context != null) {
                    HashMap hashMap = new HashMap();
                    hashMap.put("public-key", this.f284);
                    hashMap.put("sig-data", this.f286);
                    hashMap.put("signature", this.f285);
                    final HashMap hashMap2 = new HashMap();
                    hashMap2.putAll(hashMap);
                    Executors.newSingleThreadScheduledExecutor().schedule(new Runnable() {
                        public final void run() {
                            C0442i.m307(C0442i.this, hashMap2, C0442i.this.f281, C0442i.this.f287);
                        }
                    }, 5, TimeUnit.MILLISECONDS);
                    hashMap.put("dev_key", this.f282);
                    hashMap.put("app_id", context.getPackageName());
                    hashMap.put("uid", AppsFlyerLib.getInstance().getAppsFlyerUID(context));
                    hashMap.put(ServerParameters.ADVERTISING_ID_PARAM, AppsFlyerProperties.getInstance().getString(ServerParameters.ADVERTISING_ID_PARAM));
                    String jSONObject = new JSONObject(hashMap).toString();
                    String url = ServerConfigHandler.getUrl("https://sdk-services.%s/validate-android-signature");
                    C0469y.m373().mo6645(url, jSONObject);
                    httpURLConnection = m306(jSONObject, url);
                    int i = -1;
                    if (httpURLConnection != null) {
                        i = httpURLConnection.getResponseCode();
                    }
                    AppsFlyerLib.getInstance();
                    String r4 = AppsFlyerLib.m211(httpURLConnection);
                    C0469y.m373().mo6644(url, i, r4);
                    JSONObject jSONObject2 = new JSONObject(r4);
                    jSONObject2.put("code", i);
                    if (i == 200) {
                        AFLogger.afInfoLog(new StringBuilder("Validate response 200 ok: ").append(jSONObject2.toString()).toString());
                        if (jSONObject2.optBoolean("result")) {
                            z = jSONObject2.getBoolean("result");
                        } else {
                            z = false;
                        }
                        m308(z, this.f286, this.f288, this.f289, jSONObject2.toString());
                    } else {
                        AFLogger.afInfoLog("Failed Validate request");
                        m308(false, this.f286, this.f288, this.f289, jSONObject2.toString());
                    }
                    if (httpURLConnection != null) {
                        httpURLConnection.disconnect();
                    }
                }
            } catch (Throwable th) {
                if (httpURLConnection != null) {
                    httpURLConnection.disconnect();
                }
                throw th;
            }
        }
    }

    /* renamed from: ॱ */
    private static HttpURLConnection m306(String str, String str2) throws IOException {
        try {
            C0447l lVar = new C0447l(null, AppsFlyerLib.getInstance().isTrackingStopped());
            lVar.f296 = str;
            lVar.mo6586();
            if (Thread.currentThread() == Looper.getMainLooper().getThread()) {
                AFLogger.afDebugLog(new StringBuilder("Main thread detected. Calling ").append(str2).append(" in a new thread.").toString());
                lVar.execute(new String[]{str2});
            } else {
                AFLogger.afDebugLog(new StringBuilder("Calling ").append(str2).append(" (on current thread: ").append(Thread.currentThread().toString()).append(" )").toString());
                lVar.onPreExecute();
                lVar.onPostExecute(lVar.doInBackground(str2));
            }
            return lVar.mo6585();
        } catch (Throwable th) {
            AFLogger.afErrorLog("Could not send callStats request", th);
            return null;
        }
    }

    /* renamed from: ॱ */
    private static void m308(boolean z, String str, String str2, String str3, String str4) {
        if (AppsFlyerLib.f148 != null) {
            AFLogger.afDebugLog(new StringBuilder("Validate callback parameters: ").append(str).append(" ").append(str2).append(" ").append(str3).toString());
            if (z) {
                AFLogger.afDebugLog("Validate in app purchase success: ".concat(String.valueOf(str4)));
                AppsFlyerLib.f148.onValidateInApp();
                return;
            }
            AFLogger.afDebugLog("Validate in app purchase failed: ".concat(String.valueOf(str4)));
            AppsFlyerInAppPurchaseValidatorListener appsFlyerInAppPurchaseValidatorListener = AppsFlyerLib.f148;
            if (str4 == null) {
                str4 = "Failed validating";
            }
            appsFlyerInAppPurchaseValidatorListener.onValidateInAppFailure(str4);
        }
    }

    /* JADX WARNING: Removed duplicated region for block: B:50:0x0151  */
    /* renamed from: ॱ */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    static /* synthetic */ void m307(com.appsflyer.C0442i r13, java.util.Map r14, java.util.Map r15, java.lang.ref.WeakReference r16) {
        /*
            r11 = 0
            r9 = 0
            java.lang.Object r1 = r16.get()
            if (r1 == 0) goto L_0x0111
            java.lang.StringBuilder r1 = new java.lang.StringBuilder
            r1.<init>()
            java.lang.String r2 = f279
            java.lang.String r2 = com.appsflyer.ServerConfigHandler.getUrl(r2)
            java.lang.StringBuilder r2 = r1.append(r2)
            java.lang.Object r1 = r16.get()
            android.content.Context r1 = (android.content.Context) r1
            java.lang.String r1 = r1.getPackageName()
            java.lang.StringBuilder r1 = r2.append(r1)
            java.lang.String r12 = r1.toString()
            java.lang.Object r1 = r16.get()
            android.content.Context r1 = (android.content.Context) r1
            java.lang.String r2 = "appsflyer-data"
            android.content.SharedPreferences r8 = r1.getSharedPreferences(r2, r9)
            java.lang.String r1 = "referrer"
            java.lang.String r6 = r8.getString(r1, r11)
            if (r6 != 0) goto L_0x003f
            java.lang.String r6 = ""
        L_0x003f:
            com.appsflyer.AppsFlyerLib r1 = com.appsflyer.AppsFlyerLib.getInstance()
            java.lang.Object r2 = r16.get()
            android.content.Context r2 = (android.content.Context) r2
            java.lang.String r3 = r13.f282
            java.lang.String r4 = "af_purchase"
            java.lang.String r5 = ""
            r7 = 1
            android.content.Intent r10 = r13.f283
            java.util.Map r1 = r1.mo6483(r2, r3, r4, r5, r6, r7, r8, r9, r10)
            java.lang.String r2 = "price"
            java.lang.String r3 = r13.f288
            r1.put(r2, r3)
            java.lang.String r2 = "currency"
            java.lang.String r3 = r13.f289
            r1.put(r2, r3)
            org.json.JSONObject r3 = new org.json.JSONObject
            r3.<init>(r1)
            org.json.JSONObject r4 = new org.json.JSONObject
            r4.<init>()
            java.util.Set r1 = r14.entrySet()     // Catch:{ JSONException -> 0x0092 }
            java.util.Iterator r5 = r1.iterator()     // Catch:{ JSONException -> 0x0092 }
        L_0x0076:
            boolean r1 = r5.hasNext()     // Catch:{ JSONException -> 0x0092 }
            if (r1 == 0) goto L_0x0112
            java.lang.Object r1 = r5.next()     // Catch:{ JSONException -> 0x0092 }
            r0 = r1
            java.util.Map$Entry r0 = (java.util.Map.Entry) r0     // Catch:{ JSONException -> 0x0092 }
            r2 = r0
            java.lang.Object r1 = r2.getKey()     // Catch:{ JSONException -> 0x0092 }
            java.lang.String r1 = (java.lang.String) r1     // Catch:{ JSONException -> 0x0092 }
            java.lang.Object r2 = r2.getValue()     // Catch:{ JSONException -> 0x0092 }
            r4.put(r1, r2)     // Catch:{ JSONException -> 0x0092 }
            goto L_0x0076
        L_0x0092:
            r1 = move-exception
            java.lang.String r2 = "Failed to build 'receipt_data'"
            com.appsflyer.AFLogger.afErrorLog(r2, r1)
        L_0x0098:
            if (r15 == 0) goto L_0x00c9
            org.json.JSONObject r4 = new org.json.JSONObject
            r4.<init>()
            java.util.Set r1 = r15.entrySet()     // Catch:{ JSONException -> 0x00c3 }
            java.util.Iterator r5 = r1.iterator()     // Catch:{ JSONException -> 0x00c3 }
        L_0x00a7:
            boolean r1 = r5.hasNext()     // Catch:{ JSONException -> 0x00c3 }
            if (r1 == 0) goto L_0x0118
            java.lang.Object r1 = r5.next()     // Catch:{ JSONException -> 0x00c3 }
            r0 = r1
            java.util.Map$Entry r0 = (java.util.Map.Entry) r0     // Catch:{ JSONException -> 0x00c3 }
            r2 = r0
            java.lang.Object r1 = r2.getKey()     // Catch:{ JSONException -> 0x00c3 }
            java.lang.String r1 = (java.lang.String) r1     // Catch:{ JSONException -> 0x00c3 }
            java.lang.Object r2 = r2.getValue()     // Catch:{ JSONException -> 0x00c3 }
            r4.put(r1, r2)     // Catch:{ JSONException -> 0x00c3 }
            goto L_0x00a7
        L_0x00c3:
            r1 = move-exception
            java.lang.String r2 = "Failed to build 'extra_prms'"
            com.appsflyer.AFLogger.afErrorLog(r2, r1)
        L_0x00c9:
            java.lang.String r1 = r3.toString()
            com.appsflyer.y r2 = com.appsflyer.C0469y.m373()
            r2.mo6645(r12, r1)
            java.net.HttpURLConnection r2 = m306(r1, r12)     // Catch:{ Throwable -> 0x0157, all -> 0x014d }
            r1 = -1
            if (r2 == 0) goto L_0x00df
            int r1 = r2.getResponseCode()     // Catch:{ Throwable -> 0x013f }
        L_0x00df:
            com.appsflyer.AppsFlyerLib.getInstance()     // Catch:{ Throwable -> 0x013f }
            java.lang.String r3 = com.appsflyer.AppsFlyerLib.m211(r2)     // Catch:{ Throwable -> 0x013f }
            com.appsflyer.y r4 = com.appsflyer.C0469y.m373()     // Catch:{ Throwable -> 0x013f }
            r4.mo6644(r12, r1, r3)     // Catch:{ Throwable -> 0x013f }
            org.json.JSONObject r4 = new org.json.JSONObject     // Catch:{ Throwable -> 0x013f }
            r4.<init>(r3)     // Catch:{ Throwable -> 0x013f }
            r3 = 200(0xc8, float:2.8E-43)
            if (r1 != r3) goto L_0x011e
            java.lang.StringBuilder r1 = new java.lang.StringBuilder     // Catch:{ Throwable -> 0x013f }
            java.lang.String r3 = "Validate-WH response - 200: "
            r1.<init>(r3)     // Catch:{ Throwable -> 0x013f }
            java.lang.String r3 = r4.toString()     // Catch:{ Throwable -> 0x013f }
            java.lang.StringBuilder r1 = r1.append(r3)     // Catch:{ Throwable -> 0x013f }
            java.lang.String r1 = r1.toString()     // Catch:{ Throwable -> 0x013f }
            com.appsflyer.AFLogger.afInfoLog(r1)     // Catch:{ Throwable -> 0x013f }
        L_0x010c:
            if (r2 == 0) goto L_0x0111
            r2.disconnect()
        L_0x0111:
            return
        L_0x0112:
            java.lang.String r1 = "receipt_data"
            r3.put(r1, r4)     // Catch:{ JSONException -> 0x0092 }
            goto L_0x0098
        L_0x0118:
            java.lang.String r1 = "extra_prms"
            r3.put(r1, r4)     // Catch:{ JSONException -> 0x00c3 }
            goto L_0x00c9
        L_0x011e:
            java.lang.StringBuilder r3 = new java.lang.StringBuilder     // Catch:{ Throwable -> 0x013f }
            java.lang.String r5 = "Validate-WH response failed - "
            r3.<init>(r5)     // Catch:{ Throwable -> 0x013f }
            java.lang.StringBuilder r1 = r3.append(r1)     // Catch:{ Throwable -> 0x013f }
            java.lang.String r3 = ": "
            java.lang.StringBuilder r1 = r1.append(r3)     // Catch:{ Throwable -> 0x013f }
            java.lang.String r3 = r4.toString()     // Catch:{ Throwable -> 0x013f }
            java.lang.StringBuilder r1 = r1.append(r3)     // Catch:{ Throwable -> 0x013f }
            java.lang.String r1 = r1.toString()     // Catch:{ Throwable -> 0x013f }
            com.appsflyer.AFLogger.afWarnLog(r1)     // Catch:{ Throwable -> 0x013f }
            goto L_0x010c
        L_0x013f:
            r1 = move-exception
        L_0x0140:
            java.lang.String r3 = r1.getMessage()     // Catch:{ all -> 0x0155 }
            com.appsflyer.AFLogger.afErrorLog(r3, r1)     // Catch:{ all -> 0x0155 }
            if (r2 == 0) goto L_0x0111
            r2.disconnect()
            goto L_0x0111
        L_0x014d:
            r1 = move-exception
            r2 = r11
        L_0x014f:
            if (r2 == 0) goto L_0x0154
            r2.disconnect()
        L_0x0154:
            throw r1
        L_0x0155:
            r1 = move-exception
            goto L_0x014f
        L_0x0157:
            r1 = move-exception
            r2 = r11
            goto L_0x0140
        */
        throw new UnsupportedOperationException("Method not decompiled: com.appsflyer.C0442i.m307(com.appsflyer.i, java.util.Map, java.util.Map, java.lang.ref.WeakReference):void");
    }
}
