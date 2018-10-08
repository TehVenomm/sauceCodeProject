package jp.colopl.drapro;

import android.accounts.Account;
import android.accounts.AccountManager;
import android.app.Activity;
import android.content.Intent;
import android.os.Build;
import android.os.Build.VERSION;
import android.provider.Settings.Secure;
import android.text.TextUtils;
import android.util.Log;
import com.facebook.appevents.AppEventsConstants;
import com.google.android.zippy.SharedPreferencesCompat;
import com.unity3d.player.UnityPlayer;
import jp.colopl.config.Config;
import jp.colopl.config.Session;
import jp.colopl.libs.AnalyticsService;
import jp.colopl.libs.AssetService;
import jp.colopl.util.Crypto;
import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

public class NetworkHelper {
    public static final String KEY = "e87e03526ab";
    private static final String TAG = "NetworkHelper";
    private static Activity activity;
    private static String host;

    public static int getAnalytics() {
        AppConsts.appContext.startService(new Intent(AppConsts.appContext, AnalyticsService.class));
        return 0;
    }

    public static int getAsset(String str) {
        Intent intent = new Intent(AppConsts.appContext, AssetService.class);
        intent.putExtra("asset", str);
        AppConsts.appContext.startService(intent);
        return 0;
    }

    public static String getDefaultUserAgent() {
        StringBuilder stringBuilder = new StringBuilder(64);
        stringBuilder.append("Dalvik/");
        stringBuilder.append(System.getProperty("java.vm.version"));
        stringBuilder.append(" (Linux; U; Android ");
        String str = VERSION.RELEASE;
        if (str.length() <= 0) {
            str = "1.0";
        }
        stringBuilder.append(str);
        if ("REL".equals(VERSION.CODENAME)) {
            str = Build.MODEL;
            if (str.length() > 0) {
                stringBuilder.append("; ");
                stringBuilder.append(str);
            }
        }
        str = Build.ID;
        if (str.length() > 0) {
            stringBuilder.append(" Build/");
            stringBuilder.append(str);
        }
        stringBuilder.append(")");
        return stringBuilder.toString();
    }

    public static String getGoogleAccounts(String str) {
        JSONArray jSONArray = new JSONArray();
        for (Account account : AccountManager.get(activity).getAccountsByType("com.google")) {
            JSONObject jSONObject = new JSONObject();
            String str2 = "";
            try {
                Object encrypt = Crypto.encrypt(AppEventsConstants.EVENT_PARAM_VALUE_YES + ":" + String.valueOf(System.currentTimeMillis()) + ":" + account.name);
            } catch (Exception e) {
                e.printStackTrace();
                String str3 = str2;
            }
            try {
                jSONObject.put("name", account.name);
                jSONObject.put("key", encrypt);
            } catch (JSONException e2) {
                e2.printStackTrace();
            }
            jSONArray.put(jSONObject);
        }
        JSONObject jSONObject2 = new JSONObject();
        try {
            jSONObject2.put("deviceId", Crypto.encrypt(AppEventsConstants.EVENT_PARAM_VALUE_YES + ":" + str));
        } catch (Exception e3) {
            e3.printStackTrace();
        }
        try {
            jSONObject2.put("googleAccounts", jSONArray);
        } catch (JSONException e22) {
            e22.printStackTrace();
        }
        return jSONObject2.toString();
    }

    public static String getHost() {
        if (host == null) {
            host = activity.getSharedPreferences(Session.PREFERENCE_NAME, 0).getString("host", "");
        }
        return host;
    }

    public static String getItemShopDepositUrl() {
        return getHost() + "/ajax/payments/inappbilling/deposit";
    }

    public static String getItemShopRequestUrl() {
        return getHost() + "/ajax/payments/inappbilling/request";
    }

    public static String getSharedString(String str) {
        return activity.getSharedPreferences(Session.PREFERENCE_NAME, 0).getString(str, "");
    }

    public static String getUniqueId(String str) {
        if (TextUtils.isEmpty(str) || !str.equals(KEY)) {
            return "";
        }
        String string = Secure.getString(activity.getApplicationContext().getContentResolver(), "android_id");
        if (string == null) {
            string = "ffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffff";
        }
        Log.d("dragon", "device id : " + string);
        try {
            string = Crypto.encrypt(AppEventsConstants.EVENT_PARAM_VALUE_YES + ":" + Crypto.encryptMD5(string + ":" + KEY));
            Log.d("dragon", "encrypted : " + string);
            return string;
        } catch (Exception e) {
            Exception exception = e;
            string = "";
            exception.printStackTrace();
            Log.d("dragon", "encrypted error : " + "");
            return string;
        }
    }

    public static int getVersionCode() {
        return Config.getVersionCode(activity);
    }

    public static String getVersionName() {
        return Config.getVersionName(activity);
    }

    public static void init(Activity activity) {
        activity = activity;
    }

    public static void setAnalytics(String str) {
        UnityPlayer.UnitySendMessage("NativeReceiver", "notifyAnalytics", str);
    }

    public static void setHost(String str) {
        host = str;
        SharedPreferencesCompat.apply(activity.getSharedPreferences(Session.PREFERENCE_NAME, 0).edit().putString("host", host));
    }

    public static void setSharedString(String str, String str2) {
        SharedPreferencesCompat.apply(activity.getSharedPreferences(Session.PREFERENCE_NAME, 0).edit().putString(str, str2));
    }

    public static void setSidToken(String str) {
        ((StartActivity) activity).getConfig().getSession().setSid(str);
    }

    public static void startAUSmartPassIncentiveProcess(String str) {
    }
}
