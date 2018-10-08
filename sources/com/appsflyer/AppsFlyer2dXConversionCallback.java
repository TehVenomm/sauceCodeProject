package com.appsflyer;

import com.facebook.share.internal.ShareConstants;
import java.util.Map;
import org.json.JSONObject;

public class AppsFlyer2dXConversionCallback implements AppsFlyerConversionListener {
    public native void onAppOpenAttributionNative(Object obj);

    public native void onAttributionFailureNative(Object obj);

    public native void onInstallConversionDataLoadedNative(Object obj);

    public native void onInstallConversionFailureNative(Object obj);

    public void onInstallConversionDataLoaded(Map<String, String> map) {
        onInstallConversionDataLoadedNative(map);
    }

    public void onInstallConversionFailure(String str) {
        m192("onAttributionFailure", str);
    }

    public void onAppOpenAttribution(Map<String, String> map) {
        onAppOpenAttributionNative(map);
    }

    public void onAttributionFailure(String str) {
        m192("onInstallConversionFailure", str);
    }

    /* renamed from: ˋ */
    private void m192(String str, String str2) {
        try {
            JSONObject jSONObject = new JSONObject();
            jSONObject.put("status", "failure");
            jSONObject.put(ShareConstants.WEB_DIALOG_PARAM_DATA, str2);
            Object obj = -1;
            switch (str.hashCode()) {
                case -1390007222:
                    if (str.equals("onAttributionFailure")) {
                        obj = 1;
                        break;
                    }
                    break;
                case 1050716216:
                    if (str.equals("onInstallConversionFailure")) {
                        obj = null;
                        break;
                    }
                    break;
            }
            switch (obj) {
                case null:
                    onInstallConversionFailureNative(jSONObject);
                    return;
                case 1:
                    onAttributionFailureNative(jSONObject);
                    return;
                default:
                    return;
            }
        } catch (Throwable e) {
            e.printStackTrace();
        }
        e.printStackTrace();
    }
}
