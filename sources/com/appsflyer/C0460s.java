package com.appsflyer;

import android.net.Uri;
import android.support.annotation.NonNull;
import android.text.TextUtils;
import com.appsflyer.share.Constants;
import java.io.IOException;
import java.util.HashMap;
import java.util.Iterator;
import java.util.Map;
import javax.net.ssl.HttpsURLConnection;
import org.json.JSONException;
import org.json.JSONObject;
import p017io.fabric.sdk.android.services.network.HttpRequest;

/* renamed from: com.appsflyer.s */
final class C0460s extends OneLinkHttpTask {

    /* renamed from: ˋ */
    private C0461b f322;

    /* renamed from: ॱ */
    private String f323;

    /* renamed from: com.appsflyer.s$b */
    interface C0461b {
        /* renamed from: ॱ */
        void mo6488(String str);

        /* renamed from: ॱ */
        void mo6489(Map<String, String> map);
    }

    C0460s(Uri uri, AppsFlyerLib appsFlyerLib) {
        super(appsFlyerLib);
        if (!TextUtils.isEmpty(uri.getHost()) && !TextUtils.isEmpty(uri.getPath())) {
            String[] split = uri.getPath().split(Constants.URL_PATH_DELIMITER);
            if (uri.getHost().contains("onelink.me") && split.length == 3) {
                this.f222 = split[1];
                this.f323 = split[2];
            }
        }
    }

    /* access modifiers changed from: 0000 */
    /* renamed from: ˏ */
    public final void mo6609(@NonNull C0461b bVar) {
        this.f322 = bVar;
    }

    /* access modifiers changed from: 0000 */
    /* renamed from: ˏ */
    public final boolean mo6610() {
        return !TextUtils.isEmpty(this.f222) && !TextUtils.isEmpty(this.f323);
    }

    /* access modifiers changed from: 0000 */
    /* renamed from: ॱ */
    public final void mo6531(HttpsURLConnection httpsURLConnection) throws JSONException, IOException {
        httpsURLConnection.setRequestMethod(HttpRequest.METHOD_GET);
    }

    /* access modifiers changed from: 0000 */
    /* renamed from: ˊ */
    public final String mo6528() {
        return new StringBuilder().append(ServerConfigHandler.getUrl("https://onelink.%s/shortlink-sdk/v1")).append(Constants.URL_PATH_DELIMITER).append(this.f222).append("?id=").append(this.f323).toString();
    }

    /* access modifiers changed from: 0000 */
    /* renamed from: ˊ */
    public final void mo6529(String str) {
        try {
            HashMap hashMap = new HashMap();
            JSONObject jSONObject = new JSONObject(str);
            Iterator keys = jSONObject.keys();
            while (keys.hasNext()) {
                String str2 = (String) keys.next();
                hashMap.put(str2, jSONObject.optString(str2));
            }
            this.f322.mo6489((Map<String, String>) hashMap);
        } catch (JSONException e) {
            this.f322.mo6488("Can't parse one link data");
            AFLogger.afErrorLog("Error while parsing to json ".concat(String.valueOf(str)), e);
        }
    }

    /* access modifiers changed from: 0000 */
    /* renamed from: ˎ */
    public final void mo6530() {
        this.f322.mo6488("Can't get one link data");
    }
}
