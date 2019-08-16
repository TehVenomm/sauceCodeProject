package com.appsflyer;

import android.text.TextUtils;
import java.io.IOException;
import java.net.HttpURLConnection;
import java.net.URL;
import javax.net.ssl.HttpsURLConnection;
import org.json.JSONException;

public abstract class OneLinkHttpTask implements Runnable {

    /* renamed from: ˊ */
    String f222;

    /* renamed from: ˎ */
    private HttpsUrlConnectionProvider f223;

    /* renamed from: ॱ */
    private AppsFlyerLib f224;

    public static class HttpsUrlConnectionProvider {
        /* renamed from: ˋ */
        static HttpsURLConnection m263(String str) throws IOException {
            return (HttpsURLConnection) new URL(str).openConnection();
        }
    }

    /* access modifiers changed from: 0000 */
    /* renamed from: ˊ */
    public abstract String mo6528();

    /* access modifiers changed from: 0000 */
    /* renamed from: ˊ */
    public abstract void mo6529(String str);

    /* access modifiers changed from: 0000 */
    /* renamed from: ˎ */
    public abstract void mo6530();

    /* access modifiers changed from: 0000 */
    /* renamed from: ॱ */
    public abstract void mo6531(HttpsURLConnection httpsURLConnection) throws JSONException, IOException;

    OneLinkHttpTask(AppsFlyerLib appsFlyerLib) {
        this.f224 = appsFlyerLib;
    }

    public void setConnProvider(HttpsUrlConnectionProvider httpsUrlConnectionProvider) {
        this.f223 = httpsUrlConnectionProvider;
    }

    public void run() {
        long currentTimeMillis = System.currentTimeMillis() / 1000;
        String str = "";
        String str2 = "";
        String r4 = mo6528();
        AFLogger.afRDLog("oneLinkUrl: ".concat(String.valueOf(r4)));
        try {
            HttpsURLConnection r5 = HttpsUrlConnectionProvider.m263(r4);
            r5.addRequestProperty("content-type", "application/json");
            StringBuilder sb = new StringBuilder();
            sb.append(AppsFlyerProperties.getInstance().getString(AppsFlyerProperties.AF_KEY)).append(currentTimeMillis);
            r5.addRequestProperty("authorization", C0459r.m341(sb.toString()));
            r5.addRequestProperty("af-timestamp", String.valueOf(currentTimeMillis));
            r5.setReadTimeout(3000);
            r5.setConnectTimeout(3000);
            mo6531(r5);
            int responseCode = r5.getResponseCode();
            str = AppsFlyerLib.m211((HttpURLConnection) r5);
            if (responseCode == 200) {
                AFLogger.afInfoLog("Status 200 ok");
            } else {
                str2 = new StringBuilder("Response code = ").append(responseCode).append(" content = ").append(str).toString();
            }
        } catch (Throwable th) {
            AFLogger.afErrorLog("Error while calling ".concat(String.valueOf(r4)), th);
            str2 = new StringBuilder("Error while calling ").append(r4).append(" stacktrace: ").append(th.toString()).toString();
        }
        if (TextUtils.isEmpty(str2)) {
            AFLogger.afInfoLog("Connection call succeeded: ".concat(String.valueOf(str)));
            mo6529(str);
            return;
        }
        AFLogger.afWarnLog("Connection error: ".concat(String.valueOf(str2)));
        mo6530();
    }
}
