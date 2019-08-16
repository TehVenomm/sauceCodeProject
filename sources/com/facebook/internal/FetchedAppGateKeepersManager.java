package com.facebook.internal;

import android.content.Context;
import android.content.SharedPreferences;
import android.os.Bundle;
import android.support.annotation.Nullable;
import com.facebook.FacebookSdk;
import com.facebook.GraphRequest;
import java.util.Map;
import java.util.concurrent.ConcurrentHashMap;
import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

public class FetchedAppGateKeepersManager {
    private static final String APPLICATION_DEVICE_ID = "device_id";
    private static final String APPLICATION_FIELDS = "fields";
    private static final long APPLICATION_GATEKEEPER_CACHE_TIMEOUT = 3600000;
    private static final String APPLICATION_GATEKEEPER_EDGE = "mobile_sdk_gk";
    private static final String APPLICATION_GATEKEEPER_FIELD = "gatekeepers";
    private static final String APPLICATION_GRAPH_DATA = "data";
    private static final String APPLICATION_PLATFORM = "platform";
    private static final String APPLICATION_SDK_VERSION = "sdk_version";
    public static final String APP_EVENTS_IF_AUTO_LOG_SUBS = "app_events_if_auto_log_subs";
    private static final String APP_GATEKEEPERS_PREFS_KEY_FORMAT = "com.facebook.internal.APP_GATEKEEPERS.%s";
    private static final String APP_GATEKEEPERS_PREFS_STORE = "com.facebook.internal.preferences.APP_GATEKEEPERS";
    private static final String APP_PLATFORM = "android";
    private static final String TAG = FetchedAppGateKeepersManager.class.getCanonicalName();
    private static final Map<String, JSONObject> fetchedAppGateKeepers = new ConcurrentHashMap();
    /* access modifiers changed from: private */
    @Nullable
    public static Long timestamp = null;

    /* access modifiers changed from: private */
    public static JSONObject getAppGateKeepersQueryResponse(String str) {
        Bundle bundle = new Bundle();
        AttributionIdentifiers cachedIdentifiers = AttributionIdentifiers.getCachedIdentifiers();
        String str2 = "";
        if (!(cachedIdentifiers == null || cachedIdentifiers.getAndroidAdvertiserId() == null)) {
            str2 = cachedIdentifiers.getAndroidAdvertiserId();
        }
        String sdkVersion = FacebookSdk.getSdkVersion();
        bundle.putString(APPLICATION_PLATFORM, "android");
        bundle.putString(APPLICATION_DEVICE_ID, str2);
        bundle.putString(APPLICATION_SDK_VERSION, sdkVersion);
        bundle.putString("fields", APPLICATION_GATEKEEPER_FIELD);
        GraphRequest newGraphPathRequest = GraphRequest.newGraphPathRequest(null, String.format("%s/%s", new Object[]{str, APPLICATION_GATEKEEPER_EDGE}), null);
        newGraphPathRequest.setSkipClientToken(true);
        newGraphPathRequest.setParameters(bundle);
        return newGraphPathRequest.executeAndWait().getJSONObject();
    }

    public static boolean getGateKeeperForKey(String str, String str2, boolean z) {
        loadAppGateKeepersAsync();
        return (str2 == null || !fetchedAppGateKeepers.containsKey(str2)) ? z : ((JSONObject) fetchedAppGateKeepers.get(str2)).optBoolean(str, z);
    }

    private static boolean isTimestampValid(Long l) {
        return l != null && System.currentTimeMillis() - l.longValue() < 3600000;
    }

    public static void loadAppGateKeepersAsync() {
        synchronized (FetchedAppGateKeepersManager.class) {
            try {
                if (!isTimestampValid(timestamp)) {
                    final Context applicationContext = FacebookSdk.getApplicationContext();
                    final String applicationId = FacebookSdk.getApplicationId();
                    final String format = String.format(APP_GATEKEEPERS_PREFS_KEY_FORMAT, new Object[]{applicationId});
                    FacebookSdk.getExecutor().execute(new Runnable() {
                        public void run() {
                            JSONObject jSONObject;
                            SharedPreferences sharedPreferences = applicationContext.getSharedPreferences(FetchedAppGateKeepersManager.APP_GATEKEEPERS_PREFS_STORE, 0);
                            String string = sharedPreferences.getString(format, null);
                            if (!Utility.isNullOrEmpty(string)) {
                                try {
                                    jSONObject = new JSONObject(string);
                                } catch (JSONException e) {
                                    Utility.logd("FacebookSDK", (Exception) e);
                                    jSONObject = null;
                                }
                                if (jSONObject != null) {
                                    FetchedAppGateKeepersManager.parseAppGateKeepersFromJSON(applicationId, jSONObject);
                                }
                            }
                            JSONObject access$100 = FetchedAppGateKeepersManager.getAppGateKeepersQueryResponse(applicationId);
                            if (access$100 != null) {
                                FetchedAppGateKeepersManager.timestamp = Long.valueOf(System.currentTimeMillis());
                                FetchedAppGateKeepersManager.parseAppGateKeepersFromJSON(applicationId, access$100);
                                sharedPreferences.edit().putString(format, access$100.toString()).apply();
                            }
                        }
                    });
                }
            } finally {
                Class<FetchedAppGateKeepersManager> cls = FetchedAppGateKeepersManager.class;
            }
        }
    }

    /* access modifiers changed from: private */
    public static JSONObject parseAppGateKeepersFromJSON(String str, JSONObject jSONObject) {
        JSONObject jSONObject2;
        synchronized (FetchedAppGateKeepersManager.class) {
            try {
                jSONObject2 = fetchedAppGateKeepers.containsKey(str) ? (JSONObject) fetchedAppGateKeepers.get(str) : new JSONObject();
                JSONArray optJSONArray = jSONObject.optJSONArray("data");
                JSONObject jSONObject3 = null;
                if (optJSONArray != null) {
                    jSONObject3 = optJSONArray.optJSONObject(0);
                }
                if (!(jSONObject3 == null || jSONObject3.optJSONArray(APPLICATION_GATEKEEPER_FIELD) == null)) {
                    JSONArray optJSONArray2 = jSONObject3.optJSONArray(APPLICATION_GATEKEEPER_FIELD);
                    for (int i = 0; i < optJSONArray2.length(); i++) {
                        JSONObject jSONObject4 = optJSONArray2.getJSONObject(i);
                        jSONObject2.put(jSONObject4.getString("key"), jSONObject4.getBoolean("value"));
                    }
                }
                fetchedAppGateKeepers.put(str, jSONObject2);
            } catch (JSONException e) {
                Utility.logd("FacebookSDK", (Exception) e);
            } catch (Throwable th) {
                Class<FetchedAppGateKeepersManager> cls = FetchedAppGateKeepersManager.class;
                throw th;
            }
        }
        return jSONObject2;
    }

    @Nullable
    public static JSONObject queryAppGateKeepers(String str, boolean z) {
        if (!z && fetchedAppGateKeepers.containsKey(str)) {
            return (JSONObject) fetchedAppGateKeepers.get(str);
        }
        JSONObject appGateKeepersQueryResponse = getAppGateKeepersQueryResponse(str);
        if (appGateKeepersQueryResponse == null) {
            return null;
        }
        Context applicationContext = FacebookSdk.getApplicationContext();
        applicationContext.getSharedPreferences(APP_GATEKEEPERS_PREFS_STORE, 0).edit().putString(String.format(APP_GATEKEEPERS_PREFS_KEY_FORMAT, new Object[]{str}), appGateKeepersQueryResponse.toString()).apply();
        return parseAppGateKeepersFromJSON(str, appGateKeepersQueryResponse);
    }
}
