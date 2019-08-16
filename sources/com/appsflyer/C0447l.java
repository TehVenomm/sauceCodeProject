package com.appsflyer;

import android.content.Context;
import android.content.SharedPreferences.Editor;
import android.os.AsyncTask;
import java.io.BufferedWriter;
import java.io.OutputStream;
import java.io.OutputStreamWriter;
import java.lang.ref.WeakReference;
import java.net.HttpURLConnection;
import java.net.URL;
import java.util.Map;
import org.json.JSONObject;
import p017io.fabric.sdk.android.services.network.HttpRequest;

/* renamed from: com.appsflyer.l */
final class C0447l extends AsyncTask<String, Void, String> {

    /* renamed from: ʻ */
    private boolean f293;

    /* renamed from: ʼ */
    private WeakReference<Context> f294;

    /* renamed from: ʽ */
    private HttpURLConnection f295;

    /* renamed from: ˊ */
    String f296;

    /* renamed from: ˋ */
    private boolean f297 = false;

    /* renamed from: ˎ */
    Map<String, String> f298;

    /* renamed from: ˏ */
    private String f299 = "";

    /* renamed from: ॱ */
    private boolean f300 = false;

    /* renamed from: ॱॱ */
    private boolean f301;

    /* renamed from: ᐝ */
    private URL f302;

    C0447l(Context context, boolean z) {
        this.f294 = new WeakReference<>(context);
        this.f301 = true;
        this.f293 = true;
        this.f300 = z;
    }

    /* access modifiers changed from: protected */
    public final void onPreExecute() {
        if (this.f296 == null) {
            this.f296 = new JSONObject(this.f298).toString();
        }
    }

    /* access modifiers changed from: protected */
    /* renamed from: ˋ */
    public final String doInBackground(String... strArr) {
        if (this.f300) {
            return null;
        }
        try {
            this.f302 = new URL(strArr[0]);
            if (this.f301) {
                C0469y.m373().mo6645(this.f302.toString(), this.f296);
                int length = this.f296.getBytes("UTF-8").length;
                C04375.m289(new StringBuilder("call = ").append(this.f302).append("; size = ").append(length).append(" byte").append(length > 1 ? "s" : "").append("; body = ").append(this.f296).toString());
            }
            this.f295 = (HttpURLConnection) this.f302.openConnection();
            this.f295.setReadTimeout(30000);
            this.f295.setConnectTimeout(30000);
            this.f295.setRequestMethod(HttpRequest.METHOD_POST);
            this.f295.setDoInput(true);
            this.f295.setDoOutput(true);
            this.f295.setRequestProperty(HttpRequest.HEADER_CONTENT_TYPE, "application/json");
            OutputStream outputStream = this.f295.getOutputStream();
            BufferedWriter bufferedWriter = new BufferedWriter(new OutputStreamWriter(outputStream, "UTF-8"));
            bufferedWriter.write(this.f296);
            bufferedWriter.flush();
            bufferedWriter.close();
            outputStream.close();
            this.f295.connect();
            int responseCode = this.f295.getResponseCode();
            if (this.f293) {
                AppsFlyerLib.getInstance();
                this.f299 = AppsFlyerLib.m211(this.f295);
            }
            if (this.f301) {
                C0469y.m373().mo6644(this.f302.toString(), responseCode, this.f299);
            }
            if (responseCode == 200) {
                AFLogger.afInfoLog("Status 200 ok");
                Context context = (Context) this.f294.get();
                if (this.f302.toString().startsWith(ServerConfigHandler.getUrl(AppsFlyerLib.f147)) && context != null) {
                    Editor edit = context.getSharedPreferences("appsflyer-data", 0).edit();
                    edit.putBoolean("sentRegisterRequestToAF", true);
                    edit.apply();
                    AFLogger.afDebugLog("Successfully registered for Uninstall Tracking");
                }
            } else {
                this.f297 = true;
            }
        } catch (Throwable th) {
            AFLogger.afErrorLog(new StringBuilder("Error while calling ").append(this.f302.toString()).toString(), th);
            this.f297 = true;
        }
        return this.f299;
    }

    /* access modifiers changed from: protected */
    public final void onCancelled() {
    }

    /* access modifiers changed from: protected */
    /* renamed from: ॱ */
    public final void onPostExecute(String str) {
        if (this.f297) {
            AFLogger.afInfoLog("Connection error: ".concat(String.valueOf(str)));
        } else {
            AFLogger.afInfoLog("Connection call succeeded: ".concat(String.valueOf(str)));
        }
    }

    /* access modifiers changed from: 0000 */
    /* renamed from: ˊ */
    public final void mo6583() {
        this.f301 = false;
    }

    /* access modifiers changed from: 0000 */
    /* renamed from: ˏ */
    public final HttpURLConnection mo6585() {
        return this.f295;
    }

    /* access modifiers changed from: 0000 */
    /* renamed from: ॱ */
    public final void mo6586() {
        this.f293 = false;
    }
}
