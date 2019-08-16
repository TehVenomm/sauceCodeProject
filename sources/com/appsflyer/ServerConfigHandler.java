package com.appsflyer;

import android.support.annotation.Nullable;
import org.json.JSONException;
import org.json.JSONObject;

public class ServerConfigHandler {
    @Nullable
    /* renamed from: ˋ */
    static JSONObject m264(String str) {
        JSONObject jSONObject;
        try {
            jSONObject = new JSONObject(str);
            try {
                if (jSONObject.optBoolean("monitor", false)) {
                    C0469y.m373().mo6643();
                } else {
                    C0469y.m373().mo6646();
                    C0469y.m373().mo6648();
                }
            } catch (JSONException e) {
                C0469y.m373().mo6646();
                C0469y.m373().mo6648();
                return jSONObject;
            } catch (Throwable th) {
                th = th;
                AFLogger.afErrorLog(th.getMessage(), th);
                C0469y.m373().mo6646();
                C0469y.m373().mo6648();
                return jSONObject;
            }
        } catch (JSONException e2) {
            jSONObject = null;
            C0469y.m373().mo6646();
            C0469y.m373().mo6648();
            return jSONObject;
        } catch (Throwable th2) {
            th = th2;
            jSONObject = null;
            AFLogger.afErrorLog(th.getMessage(), th);
            C0469y.m373().mo6646();
            C0469y.m373().mo6648();
            return jSONObject;
        }
        return jSONObject;
    }

    public static String getUrl(String str) {
        return String.format(str, new Object[]{AppsFlyerLib.getInstance().getHost()});
    }
}
