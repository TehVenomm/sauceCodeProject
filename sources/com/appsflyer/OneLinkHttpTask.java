package com.appsflyer;

import android.text.TextUtils;
import java.io.IOException;
import java.net.HttpURLConnection;
import java.net.URL;
import javax.net.ssl.HttpsURLConnection;
import org.json.JSONException;

public abstract class OneLinkHttpTask implements Runnable {
    /* renamed from: ˊ */
    String f197;
    /* renamed from: ˎ */
    private HttpsUrlConnectionProvider f198;
    /* renamed from: ॱ */
    private AppsFlyerLib f199;

    public static class HttpsUrlConnectionProvider {
        /* renamed from: ˋ */
        static HttpsURLConnection m272(String str) throws IOException {
            return (HttpsURLConnection) new URL(str).openConnection();
        }
    }

    /* renamed from: ˊ */
    abstract String mo1212();

    /* renamed from: ˊ */
    abstract void mo1213(String str);

    /* renamed from: ˎ */
    abstract void mo1214();

    /* renamed from: ॱ */
    abstract void mo1215(HttpsURLConnection httpsURLConnection) throws JSONException, IOException;

    OneLinkHttpTask(AppsFlyerLib appsFlyerLib) {
        this.f199 = appsFlyerLib;
    }

    public void setConnProvider(HttpsUrlConnectionProvider httpsUrlConnectionProvider) {
        this.f198 = httpsUrlConnectionProvider;
    }

    public void run() {
        long currentTimeMillis = System.currentTimeMillis() / 1000;
        String str = "";
        CharSequence charSequence = "";
        String ˊ = mo1212();
        AFLogger.afRDLog("oneLinkUrl: ".concat(String.valueOf(ˊ)));
        try {
            HttpURLConnection ˋ = HttpsUrlConnectionProvider.m272(ˊ);
            ˋ.addRequestProperty("content-type", "application/json");
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.append(AppsFlyerProperties.getInstance().getString(AppsFlyerProperties.AF_KEY)).append(currentTimeMillis);
            ˋ.addRequestProperty("authorization", C0291r.m348(stringBuilder.toString()));
            ˋ.addRequestProperty("af-timestamp", String.valueOf(currentTimeMillis));
            ˋ.setReadTimeout(3000);
            ˋ.setConnectTimeout(3000);
            mo1215(ˋ);
            int responseCode = ˋ.getResponseCode();
            str = AppsFlyerLib.m231(ˋ);
            if (responseCode == 200) {
                AFLogger.afInfoLog("Status 200 ok");
            } else {
                charSequence = new StringBuilder("Response code = ").append(responseCode).append(" content = ").append(str).toString();
            }
        } catch (Throwable th) {
            AFLogger.afErrorLog("Error while calling ".concat(String.valueOf(ˊ)), th);
            charSequence = new StringBuilder("Error while calling ").append(ˊ).append(" stacktrace: ").append(th.toString()).toString();
        }
        if (TextUtils.isEmpty(charSequence)) {
            AFLogger.afInfoLog("Connection call succeeded: ".concat(String.valueOf(str)));
            mo1213(str);
            return;
        }
        AFLogger.afWarnLog("Connection error: ".concat(String.valueOf(charSequence)));
        mo1214();
    }
}
