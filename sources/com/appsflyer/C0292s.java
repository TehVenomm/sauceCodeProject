package com.appsflyer;

import android.net.Uri;
import android.support.annotation.NonNull;
import android.text.TextUtils;
import com.appsflyer.share.Constants;
import io.fabric.sdk.android.services.network.HttpRequest;
import java.io.IOException;
import java.util.HashMap;
import java.util.Iterator;
import java.util.Map;
import javax.net.ssl.HttpsURLConnection;
import org.json.JSONException;
import org.json.JSONObject;

/* renamed from: com.appsflyer.s */
final class C0292s extends OneLinkHttpTask {
    /* renamed from: ˋ */
    private C0248b f301;
    /* renamed from: ॱ */
    private String f302;

    /* renamed from: com.appsflyer.s$b */
    interface C0248b {
        /* renamed from: ॱ */
        void mo1204(String str);

        /* renamed from: ॱ */
        void mo1205(Map<String, String> map);
    }

    C0292s(Uri uri, AppsFlyerLib appsFlyerLib) {
        super(appsFlyerLib);
        if (!TextUtils.isEmpty(uri.getHost()) && !TextUtils.isEmpty(uri.getPath())) {
            String[] split = uri.getPath().split(Constants.URL_PATH_DELIMITER);
            if (uri.getHost().contains("onelink.me") && split.length == 3) {
                this.f197 = split[1];
                this.f302 = split[2];
            }
        }
    }

    /* renamed from: ˏ */
    final void m352(@NonNull C0248b c0248b) {
        this.f301 = c0248b;
    }

    /* renamed from: ˏ */
    final boolean m353() {
        return (TextUtils.isEmpty(this.f197) || TextUtils.isEmpty(this.f302)) ? false : true;
    }

    /* renamed from: ॱ */
    final void mo1215(HttpsURLConnection httpsURLConnection) throws JSONException, IOException {
        httpsURLConnection.setRequestMethod(HttpRequest.METHOD_GET);
    }

    /* renamed from: ˊ */
    final String mo1212() {
        return new StringBuilder().append(ServerConfigHandler.getUrl("https://onelink.%s/shortlink-sdk/v1")).append(Constants.URL_PATH_DELIMITER).append(this.f197).append("?id=").append(this.f302).toString();
    }

    /* renamed from: ˊ */
    final void mo1213(String str) {
        try {
            Map hashMap = new HashMap();
            JSONObject jSONObject = new JSONObject(str);
            Iterator keys = jSONObject.keys();
            while (keys.hasNext()) {
                String str2 = (String) keys.next();
                hashMap.put(str2, jSONObject.optString(str2));
            }
            this.f301.mo1205(hashMap);
        } catch (Throwable e) {
            this.f301.mo1204("Can't parse one link data");
            AFLogger.afErrorLog("Error while parsing to json ".concat(String.valueOf(str)), e);
        }
    }

    /* renamed from: ˎ */
    final void mo1214() {
        this.f301.mo1204("Can't get one link data");
    }
}
