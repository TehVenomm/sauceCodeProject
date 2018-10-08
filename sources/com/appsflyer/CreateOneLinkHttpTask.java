package com.appsflyer;

import android.content.Context;
import android.support.annotation.NonNull;
import android.support.annotation.WorkerThread;
import com.appsflyer.share.Constants;
import com.appsflyer.share.LinkGenerator;
import com.facebook.share.internal.ShareConstants;
import io.fabric.sdk.android.services.network.HttpRequest;
import java.io.DataOutputStream;
import java.io.IOException;
import java.io.OutputStream;
import java.util.Iterator;
import java.util.Map;
import javax.net.ssl.HttpsURLConnection;
import org.json.JSONException;
import org.json.JSONObject;

public class CreateOneLinkHttpTask extends OneLinkHttpTask {
    /* renamed from: ʼ */
    private Context f200;
    /* renamed from: ʽ */
    private boolean f201 = false;
    /* renamed from: ˋ */
    private String f202 = "";
    /* renamed from: ˎ */
    private ResponseListener f203;
    /* renamed from: ˏ */
    private String f204;
    /* renamed from: ॱ */
    private Map<String, String> f205;

    public interface ResponseListener {
        @WorkerThread
        void onResponse(String str);

        @WorkerThread
        void onResponseError(String str);
    }

    public CreateOneLinkHttpTask(@NonNull String str, @NonNull Map<String, String> map, AppsFlyerLib appsFlyerLib, @NonNull Context context, boolean z) {
        super(appsFlyerLib);
        this.f201 = z;
        this.f200 = context;
        if (this.f200 != null) {
            this.f202 = context.getPackageName();
        } else {
            AFLogger.afWarnLog("CreateOneLinkHttpTask: context can't be null");
        }
        this.f197 = str;
        this.f204 = "-1";
        this.f205 = map;
    }

    public void setListener(@NonNull ResponseListener responseListener) {
        this.f203 = responseListener;
    }

    /* renamed from: ॱ */
    final void mo1215(HttpsURLConnection httpsURLConnection) throws JSONException, IOException {
        if (!this.f201) {
            httpsURLConnection.setRequestMethod(HttpRequest.METHOD_POST);
            httpsURLConnection.setDoInput(true);
            httpsURLConnection.setDoOutput(true);
            httpsURLConnection.setUseCaches(false);
            JSONObject jSONObject = new JSONObject();
            JSONObject jSONObject2 = new JSONObject(this.f205);
            jSONObject.put("ttl", this.f204);
            jSONObject.put(ShareConstants.WEB_DIALOG_PARAM_DATA, jSONObject2);
            httpsURLConnection.connect();
            OutputStream dataOutputStream = new DataOutputStream(httpsURLConnection.getOutputStream());
            dataOutputStream.writeBytes(jSONObject.toString());
            dataOutputStream.flush();
            dataOutputStream.close();
        }
    }

    /* renamed from: ˊ */
    final String mo1212() {
        return new StringBuilder().append(ServerConfigHandler.getUrl("https://onelink.%s/shortlink-sdk/v1")).append(Constants.URL_PATH_DELIMITER).append(this.f197).toString();
    }

    /* renamed from: ˊ */
    final void mo1213(String str) {
        try {
            JSONObject jSONObject = new JSONObject(str);
            Iterator keys = jSONObject.keys();
            while (keys.hasNext()) {
                this.f203.onResponse(jSONObject.optString((String) keys.next()));
            }
        } catch (Throwable e) {
            this.f203.onResponseError("Can't parse one link data");
            AFLogger.afErrorLog("Error while parsing to json ".concat(String.valueOf(str)), e);
        }
    }

    /* renamed from: ˎ */
    final void mo1214() {
        LinkGenerator addParameters = new LinkGenerator(Constants.USER_INVITE_LINK_TYPE).setBaseURL(this.f197, AppsFlyerProperties.getInstance().getString(AppsFlyerProperties.ONELINK_DOMAIN), this.f202).addParameter(Constants.URL_SITE_ID, this.f202).addParameters(this.f205);
        String string = AppsFlyerProperties.getInstance().getString(AppsFlyerProperties.APP_USER_ID);
        if (string != null) {
            addParameters.setReferrerCustomerId(string);
        }
        this.f203.onResponse(addParameters.generateLink());
    }
}
