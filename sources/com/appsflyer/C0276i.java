package com.appsflyer;

import android.content.Context;
import android.content.Intent;
import android.content.SharedPreferences;
import android.os.AsyncTask;
import android.os.Looper;
import com.google.firebase.analytics.FirebaseAnalytics.Param;
import java.io.IOException;
import java.lang.ref.WeakReference;
import java.net.HttpURLConnection;
import java.util.Map;
import java.util.Map.Entry;
import java.util.concurrent.ScheduledExecutorService;
import org.json.JSONObject;

/* renamed from: com.appsflyer.i */
final class C0276i implements Runnable {
    /* renamed from: ʼ */
    private static String f260 = new StringBuilder("https://validate.%s/api/v").append(AppsFlyerLib.f162).append("/androidevent?buildnumber=4.8.11&app_id=").toString();
    /* renamed from: ʻ */
    private ScheduledExecutorService f261;
    /* renamed from: ʽ */
    private Map<String, String> f262;
    /* renamed from: ˊ */
    private String f263;
    /* renamed from: ˊॱ */
    private final Intent f264;
    /* renamed from: ˋ */
    private String f265;
    /* renamed from: ˎ */
    private String f266;
    /* renamed from: ˏ */
    private String f267;
    /* renamed from: ॱ */
    private WeakReference<Context> f268 = null;
    /* renamed from: ॱॱ */
    private String f269;
    /* renamed from: ᐝ */
    private String f270;

    /* renamed from: com.appsflyer.i$1 */
    class C02751 implements Runnable {
        /* renamed from: ˊ */
        private /* synthetic */ C0276i f258;
        /* renamed from: ॱ */
        private /* synthetic */ Map f259;

        C02751(C0276i c0276i, Map map) {
            this.f258 = c0276i;
            this.f259 = map;
        }

        public final void run() {
            C0276i.m316(this.f258, this.f259, this.f258.f262, this.f258.f268);
        }
    }

    /* JADX WARNING: inconsistent code. */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    public final void run() {
        /* JADX: method processing error */
/*
Error: jadx.core.utils.exceptions.JadxRuntimeException: Can't find block by offset: 0x000d in list [B:20:0x00f1]
	at jadx.core.utils.BlockUtils.getBlockByOffset(BlockUtils.java:43)
	at jadx.core.dex.instructions.IfNode.initBlocks(IfNode.java:60)
	at jadx.core.dex.visitors.blocksmaker.BlockFinish.initBlocksInIfNodes(BlockFinish.java:48)
	at jadx.core.dex.visitors.blocksmaker.BlockFinish.visit(BlockFinish.java:33)
	at jadx.core.dex.visitors.DepthTraversal.visit(DepthTraversal.java:31)
	at jadx.core.dex.visitors.DepthTraversal.visit(DepthTraversal.java:17)
	at jadx.core.ProcessClass.process(ProcessClass.java:34)
	at jadx.api.JadxDecompiler.processClass(JadxDecompiler.java:282)
	at jadx.api.JavaClass.decompile(JavaClass.java:62)
	at jadx.api.JadxDecompiler.lambda$appendSourcesSave$0(JadxDecompiler.java:200)
	at jadx.api.JadxDecompiler$$Lambda$8/1115381650.run(Unknown Source)
*/
        /*
        r10 = this;
        r2 = 0;
        r0 = r10.f263;
        if (r0 == 0) goto L_0x000d;
    L_0x0005:
        r0 = r10.f263;
        r0 = r0.length();
        if (r0 != 0) goto L_0x000e;
    L_0x000d:
        return;
    L_0x000e:
        r0 = com.appsflyer.AppsFlyerLib.getInstance();
        r0 = r0.isTrackingStopped();
        if (r0 != 0) goto L_0x000d;
    L_0x0018:
        r1 = 0;
        r0 = r10.f268;	 Catch:{ Throwable -> 0x010a, all -> 0x0130 }
        r0 = r0.get();	 Catch:{ Throwable -> 0x010a, all -> 0x0130 }
        r0 = (android.content.Context) r0;	 Catch:{ Throwable -> 0x010a, all -> 0x0130 }
        if (r0 == 0) goto L_0x000d;	 Catch:{ Throwable -> 0x010a, all -> 0x0130 }
    L_0x0023:
        r3 = new java.util.HashMap;	 Catch:{ Throwable -> 0x010a, all -> 0x0130 }
        r3.<init>();	 Catch:{ Throwable -> 0x010a, all -> 0x0130 }
        r4 = "public-key";	 Catch:{ Throwable -> 0x010a, all -> 0x0130 }
        r5 = r10.f265;	 Catch:{ Throwable -> 0x010a, all -> 0x0130 }
        r3.put(r4, r5);	 Catch:{ Throwable -> 0x010a, all -> 0x0130 }
        r4 = "sig-data";	 Catch:{ Throwable -> 0x010a, all -> 0x0130 }
        r5 = r10.f267;	 Catch:{ Throwable -> 0x010a, all -> 0x0130 }
        r3.put(r4, r5);	 Catch:{ Throwable -> 0x010a, all -> 0x0130 }
        r4 = "signature";	 Catch:{ Throwable -> 0x010a, all -> 0x0130 }
        r5 = r10.f266;	 Catch:{ Throwable -> 0x010a, all -> 0x0130 }
        r3.put(r4, r5);	 Catch:{ Throwable -> 0x010a, all -> 0x0130 }
        r4 = new java.util.HashMap;	 Catch:{ Throwable -> 0x010a, all -> 0x0130 }
        r4.<init>();	 Catch:{ Throwable -> 0x010a, all -> 0x0130 }
        r4.putAll(r3);	 Catch:{ Throwable -> 0x010a, all -> 0x0130 }
        r5 = java.util.concurrent.Executors.newSingleThreadScheduledExecutor();	 Catch:{ Throwable -> 0x010a, all -> 0x0130 }
        r6 = new com.appsflyer.i$1;	 Catch:{ Throwable -> 0x010a, all -> 0x0130 }
        r6.<init>(r10, r4);	 Catch:{ Throwable -> 0x010a, all -> 0x0130 }
        r8 = 5;	 Catch:{ Throwable -> 0x010a, all -> 0x0130 }
        r4 = java.util.concurrent.TimeUnit.MILLISECONDS;	 Catch:{ Throwable -> 0x010a, all -> 0x0130 }
        r5.schedule(r6, r8, r4);	 Catch:{ Throwable -> 0x010a, all -> 0x0130 }
        r4 = "dev_key";	 Catch:{ Throwable -> 0x010a, all -> 0x0130 }
        r5 = r10.f263;	 Catch:{ Throwable -> 0x010a, all -> 0x0130 }
        r3.put(r4, r5);	 Catch:{ Throwable -> 0x010a, all -> 0x0130 }
        r4 = "app_id";	 Catch:{ Throwable -> 0x010a, all -> 0x0130 }
        r5 = r0.getPackageName();	 Catch:{ Throwable -> 0x010a, all -> 0x0130 }
        r3.put(r4, r5);	 Catch:{ Throwable -> 0x010a, all -> 0x0130 }
        r4 = "uid";	 Catch:{ Throwable -> 0x010a, all -> 0x0130 }
        r5 = com.appsflyer.AppsFlyerLib.getInstance();	 Catch:{ Throwable -> 0x010a, all -> 0x0130 }
        r0 = r5.getAppsFlyerUID(r0);	 Catch:{ Throwable -> 0x010a, all -> 0x0130 }
        r3.put(r4, r0);	 Catch:{ Throwable -> 0x010a, all -> 0x0130 }
        r0 = "advertiserId";	 Catch:{ Throwable -> 0x010a, all -> 0x0130 }
        r4 = com.appsflyer.AppsFlyerProperties.getInstance();	 Catch:{ Throwable -> 0x010a, all -> 0x0130 }
        r5 = "advertiserId";	 Catch:{ Throwable -> 0x010a, all -> 0x0130 }
        r4 = r4.getString(r5);	 Catch:{ Throwable -> 0x010a, all -> 0x0130 }
        r3.put(r0, r4);	 Catch:{ Throwable -> 0x010a, all -> 0x0130 }
        r0 = new org.json.JSONObject;	 Catch:{ Throwable -> 0x010a, all -> 0x0130 }
        r0.<init>(r3);	 Catch:{ Throwable -> 0x010a, all -> 0x0130 }
        r0 = r0.toString();	 Catch:{ Throwable -> 0x010a, all -> 0x0130 }
        r3 = "https://sdk-services.%s/validate-android-signature";	 Catch:{ Throwable -> 0x010a, all -> 0x0130 }
        r3 = com.appsflyer.ServerConfigHandler.getUrl(r3);	 Catch:{ Throwable -> 0x010a, all -> 0x0130 }
        r4 = com.appsflyer.C0300y.m378();	 Catch:{ Throwable -> 0x010a, all -> 0x0130 }
        r4.m391(r3, r0);	 Catch:{ Throwable -> 0x010a, all -> 0x0130 }
        r1 = com.appsflyer.C0276i.m315(r0, r3);	 Catch:{ Throwable -> 0x010a, all -> 0x0130 }
        r0 = -1;	 Catch:{ Throwable -> 0x010a, all -> 0x0130 }
        if (r1 == 0) goto L_0x00a2;	 Catch:{ Throwable -> 0x010a, all -> 0x0130 }
    L_0x009e:
        r0 = r1.getResponseCode();	 Catch:{ Throwable -> 0x010a, all -> 0x0130 }
    L_0x00a2:
        com.appsflyer.AppsFlyerLib.getInstance();	 Catch:{ Throwable -> 0x010a, all -> 0x0130 }
        r4 = com.appsflyer.AppsFlyerLib.m231(r1);	 Catch:{ Throwable -> 0x010a, all -> 0x0130 }
        r5 = com.appsflyer.C0300y.m378();	 Catch:{ Throwable -> 0x010a, all -> 0x0130 }
        r5.m390(r3, r0, r4);	 Catch:{ Throwable -> 0x010a, all -> 0x0130 }
        r3 = new org.json.JSONObject;	 Catch:{ Throwable -> 0x010a, all -> 0x0130 }
        r3.<init>(r4);	 Catch:{ Throwable -> 0x010a, all -> 0x0130 }
        r4 = "code";	 Catch:{ Throwable -> 0x010a, all -> 0x0130 }
        r3.put(r4, r0);	 Catch:{ Throwable -> 0x010a, all -> 0x0130 }
        r4 = 200; // 0xc8 float:2.8E-43 double:9.9E-322;	 Catch:{ Throwable -> 0x010a, all -> 0x0130 }
        if (r0 != r4) goto L_0x00f6;	 Catch:{ Throwable -> 0x010a, all -> 0x0130 }
    L_0x00be:
        r0 = new java.lang.StringBuilder;	 Catch:{ Throwable -> 0x010a, all -> 0x0130 }
        r4 = "Validate response 200 ok: ";	 Catch:{ Throwable -> 0x010a, all -> 0x0130 }
        r0.<init>(r4);	 Catch:{ Throwable -> 0x010a, all -> 0x0130 }
        r4 = r3.toString();	 Catch:{ Throwable -> 0x010a, all -> 0x0130 }
        r0 = r0.append(r4);	 Catch:{ Throwable -> 0x010a, all -> 0x0130 }
        r0 = r0.toString();	 Catch:{ Throwable -> 0x010a, all -> 0x0130 }
        com.appsflyer.AFLogger.afInfoLog(r0);	 Catch:{ Throwable -> 0x010a, all -> 0x0130 }
        r0 = "result";	 Catch:{ Throwable -> 0x010a, all -> 0x0130 }
        r0 = r3.optBoolean(r0);	 Catch:{ Throwable -> 0x010a, all -> 0x0130 }
        if (r0 == 0) goto L_0x0137;	 Catch:{ Throwable -> 0x010a, all -> 0x0130 }
    L_0x00dc:
        r0 = "result";	 Catch:{ Throwable -> 0x010a, all -> 0x0130 }
        r0 = r3.getBoolean(r0);	 Catch:{ Throwable -> 0x010a, all -> 0x0130 }
    L_0x00e2:
        r2 = r10.f267;	 Catch:{ Throwable -> 0x010a, all -> 0x0130 }
        r4 = r10.f269;	 Catch:{ Throwable -> 0x010a, all -> 0x0130 }
        r5 = r10.f270;	 Catch:{ Throwable -> 0x010a, all -> 0x0130 }
        r3 = r3.toString();	 Catch:{ Throwable -> 0x010a, all -> 0x0130 }
        com.appsflyer.C0276i.m317(r0, r2, r4, r5, r3);	 Catch:{ Throwable -> 0x010a, all -> 0x0130 }
    L_0x00ef:
        if (r1 == 0) goto L_0x000d;
    L_0x00f1:
        r1.disconnect();
        goto L_0x000d;
    L_0x00f6:
        r0 = "Failed Validate request";	 Catch:{ Throwable -> 0x010a, all -> 0x0130 }
        com.appsflyer.AFLogger.afInfoLog(r0);	 Catch:{ Throwable -> 0x010a, all -> 0x0130 }
        r0 = 0;	 Catch:{ Throwable -> 0x010a, all -> 0x0130 }
        r2 = r10.f267;	 Catch:{ Throwable -> 0x010a, all -> 0x0130 }
        r4 = r10.f269;	 Catch:{ Throwable -> 0x010a, all -> 0x0130 }
        r5 = r10.f270;	 Catch:{ Throwable -> 0x010a, all -> 0x0130 }
        r3 = r3.toString();	 Catch:{ Throwable -> 0x010a, all -> 0x0130 }
        com.appsflyer.C0276i.m317(r0, r2, r4, r5, r3);	 Catch:{ Throwable -> 0x010a, all -> 0x0130 }
        goto L_0x00ef;
    L_0x010a:
        r0 = move-exception;
        r2 = com.appsflyer.AppsFlyerLib.f160;	 Catch:{ Throwable -> 0x010a, all -> 0x0130 }
        if (r2 == 0) goto L_0x0122;	 Catch:{ Throwable -> 0x010a, all -> 0x0130 }
    L_0x010f:
        r2 = "Failed Validate request + ex";	 Catch:{ Throwable -> 0x010a, all -> 0x0130 }
        com.appsflyer.AFLogger.afErrorLog(r2, r0);	 Catch:{ Throwable -> 0x010a, all -> 0x0130 }
        r2 = 0;	 Catch:{ Throwable -> 0x010a, all -> 0x0130 }
        r3 = r10.f267;	 Catch:{ Throwable -> 0x010a, all -> 0x0130 }
        r4 = r10.f269;	 Catch:{ Throwable -> 0x010a, all -> 0x0130 }
        r5 = r10.f270;	 Catch:{ Throwable -> 0x010a, all -> 0x0130 }
        r6 = r0.getMessage();	 Catch:{ Throwable -> 0x010a, all -> 0x0130 }
        com.appsflyer.C0276i.m317(r2, r3, r4, r5, r6);	 Catch:{ Throwable -> 0x010a, all -> 0x0130 }
    L_0x0122:
        r2 = r0.getMessage();	 Catch:{ Throwable -> 0x010a, all -> 0x0130 }
        com.appsflyer.AFLogger.afErrorLog(r2, r0);	 Catch:{ Throwable -> 0x010a, all -> 0x0130 }
        if (r1 == 0) goto L_0x000d;
    L_0x012b:
        r1.disconnect();
        goto L_0x000d;
    L_0x0130:
        r0 = move-exception;
        if (r1 == 0) goto L_0x0136;
    L_0x0133:
        r1.disconnect();
    L_0x0136:
        throw r0;
    L_0x0137:
        r0 = r2;
        goto L_0x00e2;
        */
        throw new UnsupportedOperationException("Method not decompiled: com.appsflyer.i.run():void");
    }

    C0276i(Context context, String str, String str2, String str3, String str4, String str5, String str6, Map<String, String> map, ScheduledExecutorService scheduledExecutorService, Intent intent) {
        this.f268 = new WeakReference(context);
        this.f263 = str;
        this.f265 = str2;
        this.f267 = str4;
        this.f269 = str5;
        this.f270 = str6;
        this.f262 = map;
        this.f266 = str3;
        this.f261 = scheduledExecutorService;
        this.f264 = intent;
    }

    /* renamed from: ॱ */
    private static HttpURLConnection m315(String str, String str2) throws IOException {
        try {
            AsyncTask c0280l = new C0280l(null, AppsFlyerLib.getInstance().isTrackingStopped());
            c0280l.f275 = str;
            c0280l.m323();
            if (Thread.currentThread() == Looper.getMainLooper().getThread()) {
                AFLogger.afDebugLog(new StringBuilder("Main thread detected. Calling ").append(str2).append(" in a new thread.").toString());
                c0280l.execute(new String[]{str2});
            } else {
                AFLogger.afDebugLog(new StringBuilder("Calling ").append(str2).append(" (on current thread: ").append(Thread.currentThread().toString()).append(" )").toString());
                c0280l.onPreExecute();
                c0280l.m324(c0280l.m321(str2));
            }
            return c0280l.m322();
        } catch (Throwable th) {
            AFLogger.afErrorLog("Could not send callStats request", th);
            return null;
        }
    }

    /* renamed from: ॱ */
    private static void m317(boolean z, String str, String str2, String str3, String str4) {
        if (AppsFlyerLib.f160 != null) {
            AFLogger.afDebugLog(new StringBuilder("Validate callback parameters: ").append(str).append(" ").append(str2).append(" ").append(str3).toString());
            if (z) {
                AFLogger.afDebugLog("Validate in app purchase success: ".concat(String.valueOf(str4)));
                AppsFlyerLib.f160.onValidateInApp();
                return;
            }
            AFLogger.afDebugLog("Validate in app purchase failed: ".concat(String.valueOf(str4)));
            AppsFlyerInAppPurchaseValidatorListener appsFlyerInAppPurchaseValidatorListener = AppsFlyerLib.f160;
            if (str4 == null) {
                str4 = "Failed validating";
            }
            appsFlyerInAppPurchaseValidatorListener.onValidateInAppFailure(str4);
        }
    }

    /* renamed from: ॱ */
    static /* synthetic */ void m316(C0276i c0276i, Map map, Map map2, WeakReference weakReference) {
        Throwable e;
        if (weakReference.get() != null) {
            String obj = new StringBuilder().append(ServerConfigHandler.getUrl(f260)).append(((Context) weakReference.get()).getPackageName()).toString();
            SharedPreferences sharedPreferences = ((Context) weakReference.get()).getSharedPreferences("appsflyer-data", 0);
            String string = sharedPreferences.getString("referrer", null);
            if (string == null) {
                string = "";
            }
            Map ˎ = AppsFlyerLib.getInstance().m259((Context) weakReference.get(), c0276i.f263, AFInAppEventType.PURCHASE, "", string, true, sharedPreferences, false, c0276i.f264);
            ˎ.put(Param.PRICE, c0276i.f269);
            ˎ.put(Param.CURRENCY, c0276i.f270);
            JSONObject jSONObject = new JSONObject(ˎ);
            JSONObject jSONObject2 = new JSONObject();
            try {
                for (Entry entry : map.entrySet()) {
                    jSONObject2.put((String) entry.getKey(), entry.getValue());
                }
                jSONObject.put("receipt_data", jSONObject2);
            } catch (Throwable e2) {
                AFLogger.afErrorLog("Failed to build 'receipt_data'", e2);
            }
            if (map2 != null) {
                jSONObject2 = new JSONObject();
                try {
                    for (Entry entry2 : map2.entrySet()) {
                        jSONObject2.put((String) entry2.getKey(), entry2.getValue());
                    }
                    jSONObject.put("extra_prms", jSONObject2);
                } catch (Throwable e22) {
                    AFLogger.afErrorLog("Failed to build 'extra_prms'", e22);
                }
            }
            String jSONObject3 = jSONObject.toString();
            C0300y.m378().m391(obj, jSONObject3);
            HttpURLConnection ॱ;
            try {
                ॱ = C0276i.m315(jSONObject3, obj);
                int i = -1;
                if (ॱ != null) {
                    try {
                        i = ॱ.getResponseCode();
                    } catch (Throwable th) {
                        e22 = th;
                        try {
                            AFLogger.afErrorLog(e22.getMessage(), e22);
                            if (ॱ != null) {
                                ॱ.disconnect();
                            }
                        } catch (Throwable th2) {
                            e22 = th2;
                            if (ॱ != null) {
                                ॱ.disconnect();
                            }
                            throw e22;
                        }
                    }
                }
                AppsFlyerLib.getInstance();
                String ˎ2 = AppsFlyerLib.m231(ॱ);
                C0300y.m378().m390(obj, i, ˎ2);
                jSONObject2 = new JSONObject(ˎ2);
                if (i == 200) {
                    AFLogger.afInfoLog(new StringBuilder("Validate-WH response - 200: ").append(jSONObject2.toString()).toString());
                } else {
                    AFLogger.afWarnLog(new StringBuilder("Validate-WH response failed - ").append(i).append(": ").append(jSONObject2.toString()).toString());
                }
                if (ॱ != null) {
                    ॱ.disconnect();
                }
            } catch (Throwable th3) {
                e22 = th3;
                ॱ = null;
                if (ॱ != null) {
                    ॱ.disconnect();
                }
                throw e22;
            }
        }
    }
}
