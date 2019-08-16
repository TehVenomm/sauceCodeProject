package com.appsflyer;

import android.content.Context;
import android.support.annotation.NonNull;
import android.support.annotation.WorkerThread;
import com.appsflyer.share.Constants;
import com.appsflyer.share.LinkGenerator;
import com.facebook.share.internal.ShareConstants;
import java.io.DataOutputStream;
import java.io.IOException;
import java.util.Iterator;
import java.util.Map;
import javax.net.ssl.HttpsURLConnection;
import org.json.JSONException;
import org.json.JSONObject;
import p017io.fabric.sdk.android.services.network.HttpRequest;

public class CreateOneLinkHttpTask extends OneLinkHttpTask {

    /* renamed from: ʼ */
    private Context f216;

    /* renamed from: ʽ */
    private boolean f217 = false;

    /* renamed from: ˋ */
    private String f218 = "";

    /* renamed from: ˎ */
    private ResponseListener f219;

    /* renamed from: ˏ */
    private String f220;

    /* renamed from: ॱ */
    private Map<String, String> f221;

    public interface ResponseListener {
        @WorkerThread
        void onResponse(String str);

        @WorkerThread
        void onResponseError(String str);
    }

    public CreateOneLinkHttpTask(@NonNull String str, @NonNull Map<String, String> map, AppsFlyerLib appsFlyerLib, @NonNull Context context, boolean z) {
        super(appsFlyerLib);
        this.f217 = z;
        this.f216 = context;
        if (this.f216 != null) {
            this.f218 = context.getPackageName();
        } else {
            AFLogger.afWarnLog("CreateOneLinkHttpTask: context can't be null");
        }
        this.f222 = str;
        this.f220 = "-1";
        this.f221 = map;
    }

    public void setListener(@NonNull ResponseListener responseListener) {
        this.f219 = responseListener;
    }

    /* access modifiers changed from: 0000 */
    /* renamed from: ॱ */
    public final void mo6531(HttpsURLConnection httpsURLConnection) throws JSONException, IOException {
        if (!this.f217) {
            httpsURLConnection.setRequestMethod(HttpRequest.METHOD_POST);
            httpsURLConnection.setDoInput(true);
            httpsURLConnection.setDoOutput(true);
            httpsURLConnection.setUseCaches(false);
            JSONObject jSONObject = new JSONObject();
            JSONObject jSONObject2 = new JSONObject(this.f221);
            jSONObject.put("ttl", this.f220);
            jSONObject.put(ShareConstants.WEB_DIALOG_PARAM_DATA, jSONObject2);
            httpsURLConnection.connect();
            DataOutputStream dataOutputStream = new DataOutputStream(httpsURLConnection.getOutputStream());
            dataOutputStream.writeBytes(jSONObject.toString());
            dataOutputStream.flush();
            dataOutputStream.close();
        }
    }

    /* access modifiers changed from: 0000 */
    /* renamed from: ˊ */
    public final String mo6528() {
        return new StringBuilder().append(ServerConfigHandler.getUrl("https://onelink.%s/shortlink-sdk/v1")).append(Constants.URL_PATH_DELIMITER).append(this.f222).toString();
    }

    /* access modifiers changed from: 0000 */
    /* renamed from: ˊ */
    public final void mo6529(String str) {
        try {
            JSONObject jSONObject = new JSONObject(str);
            Iterator keys = jSONObject.keys();
            while (keys.hasNext()) {
                this.f219.onResponse(jSONObject.optString((String) keys.next()));
            }
        } catch (JSONException e) {
            this.f219.onResponseError("Can't parse one link data");
            AFLogger.afErrorLog("Error while parsing to json ".concat(String.valueOf(str)), e);
        }
    }

    /* access modifiers changed from: 0000 */
    /* renamed from: ˎ */
    public final void mo6530() {
        LinkGenerator addParameters = new LinkGenerator(Constants.USER_INVITE_LINK_TYPE).setBaseURL(this.f222, AppsFlyerProperties.getInstance().getString(AppsFlyerProperties.ONELINK_DOMAIN), this.f218).addParameter(Constants.URL_SITE_ID, this.f218).addParameters(this.f221);
        String string = AppsFlyerProperties.getInstance().getString(AppsFlyerProperties.APP_USER_ID);
        if (string != null) {
            addParameters.setReferrerCustomerId(string);
        }
        this.f219.onResponse(addParameters.generateLink());
    }
}
