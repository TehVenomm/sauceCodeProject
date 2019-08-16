package com.appsflyer;

import android.annotation.SuppressLint;
import android.content.Context;
import android.content.SharedPreferences;
import android.content.SharedPreferences.Editor;
import android.os.Build.VERSION;
import java.util.HashMap;
import java.util.Iterator;
import java.util.Map;
import org.json.JSONException;
import org.json.JSONObject;

public class AppsFlyerProperties {
    public static final String ADDITIONAL_CUSTOM_DATA = "additionalCustomData";
    public static final String AF_KEY = "AppsFlyerKey";
    public static final String AF_WAITFOR_CUSTOMERID = "waitForCustomerId";
    public static final String APP_ID = "appid";
    public static final String APP_USER_ID = "AppUserId";
    public static final String CHANNEL = "channel";
    public static final String COLLECT_ANDROID_ID = "collectAndroidId";
    public static final String COLLECT_FACEBOOK_ATTR_ID = "collectFacebookAttrId";
    public static final String COLLECT_FINGER_PRINT = "collectFingerPrint";
    public static final String COLLECT_IMEI = "collectIMEI";
    public static final String COLLECT_MAC = "collectMAC";
    public static final String CURRENCY_CODE = "currencyCode";
    public static final String DEVICE_TRACKING_DISABLED = "deviceTrackingDisabled";
    public static final String DISABLE_LOGS_COMPLETELY = "disableLogs";
    public static final String DISABLE_OTHER_SDK = "disableOtherSdk";
    public static final String EMAIL_CRYPT_TYPE = "userEmailsCryptType";
    public static final String ENABLE_GPS_FALLBACK = "enableGpsFallback";
    public static final String EXTENSION = "sdkExtension";
    public static final String IS_MONITOR = "shouldMonitor";
    public static final String IS_UPDATE = "IS_UPDATE";
    public static final String LAUNCH_PROTECT_ENABLED = "launchProtectEnabled";
    public static final String ONELINK_DOMAIN = "onelinkDomain";
    public static final String ONELINK_ID = "oneLinkSlug";
    public static final String ONELINK_SCHEME = "onelinkScheme";
    public static final String USER_EMAIL = "userEmail";
    public static final String USER_EMAILS = "userEmails";
    public static final String USE_HTTP_FALLBACK = "useHttpFallback";

    /* renamed from: ˏ */
    private static AppsFlyerProperties f208 = new AppsFlyerProperties();

    /* renamed from: ʽ */
    private boolean f209 = false;

    /* renamed from: ˊ */
    private String f210;

    /* renamed from: ˋ */
    private Map<String, Object> f211 = new HashMap();

    /* renamed from: ˎ */
    private boolean f212;

    /* renamed from: ॱ */
    private boolean f213;

    public enum EmailsCryptType {
        NONE(0),
        SHA1(1),
        MD5(2),
        SHA256(3);
        

        /* renamed from: ˎ */
        private final int f215;

        private EmailsCryptType(int i) {
            this.f215 = i;
        }

        public final int getValue() {
            return this.f215;
        }
    }

    public void remove(String str) {
        this.f211.remove(str);
    }

    private AppsFlyerProperties() {
    }

    public static AppsFlyerProperties getInstance() {
        return f208;
    }

    public void set(String str, String str2) {
        this.f211.put(str, str2);
    }

    public void set(String str, String[] strArr) {
        this.f211.put(str, strArr);
    }

    public void set(String str, int i) {
        this.f211.put(str, Integer.toString(i));
    }

    public void set(String str, long j) {
        this.f211.put(str, Long.toString(j));
    }

    public void set(String str, boolean z) {
        this.f211.put(str, Boolean.toString(z));
    }

    public void setCustomData(String str) {
        this.f211.put(ADDITIONAL_CUSTOM_DATA, str);
    }

    public void setUserEmails(String str) {
        this.f211.put(USER_EMAILS, str);
    }

    public String getString(String str) {
        return (String) this.f211.get(str);
    }

    public boolean getBoolean(String str, boolean z) {
        String string = getString(str);
        return string == null ? z : Boolean.valueOf(string).booleanValue();
    }

    public int getInt(String str, int i) {
        String string = getString(str);
        return string == null ? i : Integer.valueOf(string).intValue();
    }

    public long getLong(String str, long j) {
        String string = getString(str);
        return string == null ? j : Long.valueOf(string).longValue();
    }

    public Object getObject(String str) {
        return this.f211.get(str);
    }

    /* access modifiers changed from: protected */
    public boolean isOnReceiveCalled() {
        return this.f213;
    }

    /* access modifiers changed from: protected */
    public void setOnReceiveCalled() {
        this.f213 = true;
    }

    /* access modifiers changed from: protected */
    public boolean isFirstLaunchCalled() {
        return this.f212;
    }

    /* access modifiers changed from: protected */
    public void setFirstLaunchCalled(boolean z) {
        this.f212 = z;
    }

    /* access modifiers changed from: protected */
    public void setFirstLaunchCalled() {
        this.f212 = true;
    }

    /* access modifiers changed from: protected */
    public void setReferrer(String str) {
        set("AF_REFERRER", str);
        this.f210 = str;
    }

    public String getReferrer(Context context) {
        if (this.f210 != null) {
            return this.f210;
        }
        if (getString("AF_REFERRER") != null) {
            return getString("AF_REFERRER");
        }
        if (context != null) {
            return context.getSharedPreferences("appsflyer-data", 0).getString("referrer", null);
        }
        return null;
    }

    public boolean isEnableLog() {
        return getBoolean("shouldLog", true);
    }

    public boolean isLogsDisabledCompletely() {
        return getBoolean(DISABLE_LOGS_COMPLETELY, false);
    }

    public boolean isOtherSdkStringDisabled() {
        return getBoolean(DISABLE_OTHER_SDK, false);
    }

    @SuppressLint({"CommitPrefEdits"})
    public void saveProperties(SharedPreferences sharedPreferences) {
        String jSONObject = new JSONObject(this.f211).toString();
        Editor edit = sharedPreferences.edit();
        edit.putString("savedProperties", jSONObject);
        if (VERSION.SDK_INT >= 9) {
            edit.apply();
        } else {
            edit.commit();
        }
    }

    public void loadProperties(Context context) {
        if (!this.f209) {
            String string = context.getSharedPreferences("appsflyer-data", 0).getString("savedProperties", null);
            if (string != null) {
                AFLogger.afDebugLog("Loading properties..");
                try {
                    JSONObject jSONObject = new JSONObject(string);
                    Iterator keys = jSONObject.keys();
                    while (keys.hasNext()) {
                        String str = (String) keys.next();
                        if (this.f211.get(str) == null) {
                            this.f211.put(str, jSONObject.getString(str));
                        }
                    }
                    this.f209 = true;
                } catch (JSONException e) {
                    AFLogger.afErrorLog("Failed loading properties", e);
                }
                AFLogger.afDebugLog(new StringBuilder("Done loading properties: ").append(this.f209).toString());
            }
        }
    }
}
