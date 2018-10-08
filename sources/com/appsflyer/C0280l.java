package com.appsflyer;

import android.content.Context;
import android.content.SharedPreferences.Editor;
import android.os.AsyncTask;
import com.appsflyer.C0270f.C02695;
import io.fabric.sdk.android.services.network.HttpRequest;
import java.io.BufferedWriter;
import java.io.OutputStream;
import java.io.OutputStreamWriter;
import java.io.Writer;
import java.lang.ref.WeakReference;
import java.net.HttpURLConnection;
import java.net.URL;
import java.util.Map;
import org.json.JSONObject;

/* renamed from: com.appsflyer.l */
final class C0280l extends AsyncTask<String, Void, String> {
    /* renamed from: ʻ */
    private boolean f272;
    /* renamed from: ʼ */
    private WeakReference<Context> f273;
    /* renamed from: ʽ */
    private HttpURLConnection f274;
    /* renamed from: ˊ */
    String f275;
    /* renamed from: ˋ */
    private boolean f276 = false;
    /* renamed from: ˎ */
    Map<String, String> f277;
    /* renamed from: ˏ */
    private String f278 = "";
    /* renamed from: ॱ */
    private boolean f279 = false;
    /* renamed from: ॱॱ */
    private boolean f280;
    /* renamed from: ᐝ */
    private URL f281;

    protected final /* synthetic */ Object doInBackground(Object[] objArr) {
        return m321((String[]) objArr);
    }

    protected final /* synthetic */ void onPostExecute(Object obj) {
        m324((String) obj);
    }

    C0280l(Context context, boolean z) {
        this.f273 = new WeakReference(context);
        this.f280 = true;
        this.f272 = true;
        this.f279 = z;
    }

    protected final void onPreExecute() {
        if (this.f275 == null) {
            this.f275 = new JSONObject(this.f277).toString();
        }
    }

    /* renamed from: ˋ */
    protected final String m321(String... strArr) {
        if (this.f279) {
            return null;
        }
        try {
            int length;
            this.f281 = new URL(strArr[0]);
            if (this.f280) {
                C0300y.m378().m391(this.f281.toString(), this.f275);
                length = this.f275.getBytes("UTF-8").length;
                C02695.m293(new StringBuilder("call = ").append(this.f281).append("; size = ").append(length).append(" byte").append(length > 1 ? "s" : "").append("; body = ").append(this.f275).toString());
            }
            this.f274 = (HttpURLConnection) this.f281.openConnection();
            this.f274.setReadTimeout(30000);
            this.f274.setConnectTimeout(30000);
            this.f274.setRequestMethod(HttpRequest.METHOD_POST);
            this.f274.setDoInput(true);
            this.f274.setDoOutput(true);
            this.f274.setRequestProperty(HttpRequest.HEADER_CONTENT_TYPE, "application/json");
            OutputStream outputStream = this.f274.getOutputStream();
            Writer bufferedWriter = new BufferedWriter(new OutputStreamWriter(outputStream, "UTF-8"));
            bufferedWriter.write(this.f275);
            bufferedWriter.flush();
            bufferedWriter.close();
            outputStream.close();
            this.f274.connect();
            length = this.f274.getResponseCode();
            if (this.f272) {
                AppsFlyerLib.getInstance();
                this.f278 = AppsFlyerLib.m231(this.f274);
            }
            if (this.f280) {
                C0300y.m378().m390(this.f281.toString(), length, this.f278);
            }
            if (length == 200) {
                AFLogger.afInfoLog("Status 200 ok");
                Context context = (Context) this.f273.get();
                if (this.f281.toString().startsWith(ServerConfigHandler.getUrl(AppsFlyerLib.f159)) && context != null) {
                    Editor edit = context.getSharedPreferences("appsflyer-data", 0).edit();
                    edit.putBoolean("sentRegisterRequestToAF", true);
                    edit.apply();
                    AFLogger.afDebugLog("Successfully registered for Uninstall Tracking");
                }
            } else {
                this.f276 = true;
            }
        } catch (Throwable th) {
            AFLogger.afErrorLog(new StringBuilder("Error while calling ").append(this.f281.toString()).toString(), th);
            this.f276 = true;
        }
        return this.f278;
    }

    protected final void onCancelled() {
    }

    /* renamed from: ॱ */
    protected final void m324(String str) {
        if (this.f276) {
            AFLogger.afInfoLog("Connection error: ".concat(String.valueOf(str)));
        } else {
            AFLogger.afInfoLog("Connection call succeeded: ".concat(String.valueOf(str)));
        }
    }

    /* renamed from: ˊ */
    final void m320() {
        this.f280 = false;
    }

    /* renamed from: ˏ */
    final HttpURLConnection m322() {
        return this.f274;
    }

    /* renamed from: ॱ */
    final void m323() {
        this.f272 = false;
    }
}
