package com.facebook.marketing.internal;

import android.content.Context;
import android.os.Bundle;
import android.util.Log;
import com.facebook.FacebookSdk;
import com.facebook.GraphRequest;
import com.facebook.HttpMethod;
import com.facebook.internal.AttributionIdentifiers;
import com.facebook.share.internal.ShareConstants;
import java.util.Locale;
import java.util.Map;
import java.util.concurrent.ConcurrentHashMap;
import org.json.JSONArray;
import org.json.JSONObject;

public class RemoteConfigManager {
    private static final String SAMPLING_ENDPOINT_PATH = "%s/button_auto_detection_device_selection";
    private static final String SAMPLING_RESULT_FIELD = "is_selected";
    private static final String TAG = RemoteConfigManager.class.getCanonicalName();
    private static final Map<String, RemoteConfig> remoteConfigs = new ConcurrentHashMap();

    /* access modifiers changed from: private */
    public static JSONObject getRemoteConfigQueryResponse(String str, String str2) {
        try {
            String format = String.format(Locale.US, SAMPLING_ENDPOINT_PATH, new Object[]{str});
            Bundle bundle = new Bundle();
            bundle.putString("device_id", str2);
            bundle.putString(GraphRequest.FIELDS_PARAM, SAMPLING_RESULT_FIELD);
            GraphRequest graphRequest = new GraphRequest(null, format, bundle, HttpMethod.GET, null);
            graphRequest.setSkipClientToken(true);
            return graphRequest.executeAndWait().getJSONObject();
        } catch (Exception e) {
            Log.e(TAG, "fail to request button sampling api", e);
            return new JSONObject();
        }
    }

    public static RemoteConfig getRemoteConfigWithoutQuery(String str) {
        if (str != null) {
            return (RemoteConfig) remoteConfigs.get(str);
        }
        return null;
    }

    public static void loadRemoteConfig() {
        FacebookSdk.getExecutor().execute(new Runnable() {
            public void run() {
                Context applicationContext = FacebookSdk.getApplicationContext();
                String applicationId = FacebookSdk.getApplicationId();
                AttributionIdentifiers attributionIdentifiers = AttributionIdentifiers.getAttributionIdentifiers(applicationContext);
                if (attributionIdentifiers != null && attributionIdentifiers.getAndroidAdvertiserId() != null) {
                    JSONObject access$000 = RemoteConfigManager.getRemoteConfigQueryResponse(applicationId, attributionIdentifiers.getAndroidAdvertiserId());
                    if (access$000 != null) {
                        RemoteConfigManager.parseRemoteConfigFromJSON(applicationId, access$000);
                    }
                }
            }
        });
    }

    /* access modifiers changed from: private */
    public static void parseRemoteConfigFromJSON(String str, JSONObject jSONObject) {
        JSONArray optJSONArray = jSONObject.optJSONArray(ShareConstants.WEB_DIALOG_PARAM_DATA);
        if (optJSONArray != null && optJSONArray.length() > 0) {
            JSONObject optJSONObject = optJSONArray.optJSONObject(0);
            if (optJSONObject != null) {
                remoteConfigs.put(str, new RemoteConfig(optJSONObject.optBoolean(SAMPLING_RESULT_FIELD, false)));
            }
        }
    }
}
