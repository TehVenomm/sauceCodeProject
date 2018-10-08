package com.appsflyer;

import android.support.annotation.Nullable;
import org.json.JSONException;
import org.json.JSONObject;

public class ServerConfigHandler {
    @Nullable
    /* renamed from: ˋ */
    static JSONObject m273(String str) {
        JSONObject jSONObject;
        try {
            jSONObject = new JSONObject(str);
            try {
                if (jSONObject.optBoolean("monitor", false)) {
                    C0300y.m378().m389();
                } else {
                    C0300y.m378().m392();
                    C0300y.m378().m394();
                }
            } catch (JSONException e) {
                C0300y.m378().m392();
                C0300y.m378().m394();
                return jSONObject;
            } catch (Throwable th) {
                th = th;
                AFLogger.afErrorLog(th.getMessage(), th);
                C0300y.m378().m392();
                C0300y.m378().m394();
                return jSONObject;
            }
        } catch (JSONException e2) {
            jSONObject = null;
            C0300y.m378().m392();
            C0300y.m378().m394();
            return jSONObject;
        } catch (Throwable th2) {
            Throwable th3;
            Throwable th4 = th2;
            jSONObject = null;
            th3 = th4;
            AFLogger.afErrorLog(th3.getMessage(), th3);
            C0300y.m378().m392();
            C0300y.m378().m394();
            return jSONObject;
        }
        return jSONObject;
    }

    public static String getUrl(String str) {
        return String.format(str, new Object[]{AppsFlyerLib.getInstance().getHost()});
    }
}
