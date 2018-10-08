package com.appsflyer;

import android.content.pm.PackageManager;
import android.os.AsyncTask;
import android.os.Build;
import android.os.Build.VERSION;
import com.facebook.share.internal.ShareConstants;
import io.fabric.sdk.android.services.common.IdManager;
import java.text.SimpleDateFormat;
import java.util.Locale;
import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

/* renamed from: com.appsflyer.y */
final class C0300y {
    /* renamed from: ˊ */
    private static C0300y f332;
    /* renamed from: ʻ */
    private final String f333;
    /* renamed from: ʻॱ */
    private final String f334;
    /* renamed from: ʼ */
    private final String f335;
    /* renamed from: ʼॱ */
    private final String f336;
    /* renamed from: ʽ */
    private final String f337;
    /* renamed from: ʽॱ */
    private final String f338;
    /* renamed from: ʾ */
    private final String f339;
    /* renamed from: ʿ */
    private final String f340;
    /* renamed from: ˈ */
    private final String f341;
    /* renamed from: ˉ */
    private final String f342;
    /* renamed from: ˊˊ */
    private final String f343;
    /* renamed from: ˊˋ */
    private JSONArray f344;
    /* renamed from: ˊॱ */
    private final String f345;
    /* renamed from: ˊᐝ */
    private JSONObject f346;
    /* renamed from: ˋ */
    private boolean f347;
    /* renamed from: ˋˊ */
    private int f348;
    /* renamed from: ˋॱ */
    private final String f349;
    /* renamed from: ˌ */
    private String f350;
    /* renamed from: ˍ */
    private boolean f351;
    /* renamed from: ˎ */
    private boolean f352;
    /* renamed from: ˏ */
    private final String f353;
    /* renamed from: ˏॱ */
    private final String f354;
    /* renamed from: ͺ */
    private final String f355;
    /* renamed from: ॱ */
    private final String f356;
    /* renamed from: ॱˊ */
    private final String f357;
    /* renamed from: ॱˋ */
    private final String f358;
    /* renamed from: ॱˎ */
    private final String f359;
    /* renamed from: ॱॱ */
    private final String f360;
    /* renamed from: ॱᐝ */
    private final String f361;
    /* renamed from: ᐝ */
    private final String f362;
    /* renamed from: ᐝॱ */
    private final String f363;

    private C0300y() {
        this.f347 = true;
        this.f352 = true;
        this.f356 = "brand";
        this.f353 = IdManager.MODEL_FIELD;
        this.f360 = "platform";
        this.f337 = "platform_version";
        this.f362 = ServerParameters.ADVERTISING_ID_PARAM;
        this.f333 = "imei";
        this.f335 = "android_id";
        this.f357 = "sdk_version";
        this.f354 = "devkey";
        this.f345 = "originalAppsFlyerId";
        this.f349 = "uid";
        this.f355 = "app_id";
        this.f363 = "app_version";
        this.f361 = AppsFlyerProperties.CHANNEL;
        this.f358 = "preInstall";
        this.f334 = ShareConstants.WEB_DIALOG_PARAM_DATA;
        this.f359 = "r_debugging_off";
        this.f336 = "r_debugging_on";
        this.f341 = "public_api_call";
        this.f340 = "exception";
        this.f339 = "server_request";
        this.f338 = "server_response";
        this.f342 = "yyyy-MM-dd HH:mm:ssZ";
        this.f343 = "MM-dd HH:mm:ss.SSS";
        this.f348 = 0;
        this.f350 = "-1";
        this.f344 = new JSONArray();
        this.f348 = 0;
        this.f351 = false;
    }

    /* renamed from: ˋ */
    static C0300y m378() {
        if (f332 == null) {
            f332 = new C0300y();
        }
        return f332;
    }

    /* renamed from: ˊ */
    final synchronized void m387(String str) {
        this.f350 = str;
    }

    /* renamed from: ˎ */
    final synchronized void m389() {
        this.f351 = true;
        m380("r_debugging_on", new SimpleDateFormat("yyyy-MM-dd HH:mm:ssZ", Locale.ENGLISH).format(Long.valueOf(System.currentTimeMillis())), new String[0]);
    }

    /* renamed from: ॱ */
    final synchronized void m394() {
        m380("r_debugging_off", new SimpleDateFormat("yyyy-MM-dd HH:mm:ssZ", Locale.ENGLISH).format(Long.valueOf(System.currentTimeMillis())), new String[0]);
        this.f351 = false;
        this.f347 = false;
    }

    /* renamed from: ˊ */
    final synchronized void m386() {
        this.f346 = null;
        this.f344 = null;
        f332 = null;
    }

    /* renamed from: ˏ */
    private synchronized void m383(String str, String str2, String str3, String str4, String str5, String str6) {
        try {
            this.f346.put("brand", str);
            this.f346.put(IdManager.MODEL_FIELD, str2);
            this.f346.put("platform", "Android");
            this.f346.put("platform_version", str3);
            if (str4 != null && str4.length() > 0) {
                this.f346.put(ServerParameters.ADVERTISING_ID_PARAM, str4);
            }
            if (str5 != null && str5.length() > 0) {
                this.f346.put("imei", str5);
            }
            if (str6 != null && str6.length() > 0) {
                this.f346.put("android_id", str6);
            }
        } catch (Throwable th) {
        }
    }

    /* renamed from: ˏ */
    private synchronized void m382(String str, String str2, String str3, String str4) {
        try {
            this.f346.put("sdk_version", str);
            if (str2 != null && str2.length() > 0) {
                this.f346.put("devkey", str2);
            }
            if (str3 != null && str3.length() > 0) {
                this.f346.put("originalAppsFlyerId", str3);
            }
            if (str4 != null && str4.length() > 0) {
                this.f346.put("uid", str4);
            }
        } catch (Throwable th) {
        }
    }

    /* renamed from: ˋ */
    private synchronized void m379(String str, String str2, String str3, String str4) {
        if (str != null) {
            try {
                if (str.length() > 0) {
                    this.f346.put("app_id", str);
                }
            } catch (Throwable th) {
            }
        }
        if (str2 != null && str2.length() > 0) {
            this.f346.put("app_version", str2);
        }
        if (str3 != null && str3.length() > 0) {
            this.f346.put(AppsFlyerProperties.CHANNEL, str3);
        }
        if (str4 != null && str4.length() > 0) {
            this.f346.put("preInstall", str4);
        }
    }

    /* renamed from: ˏ */
    final void m393(String str, String... strArr) {
        m380("public_api_call", str, strArr);
    }

    /* renamed from: ॱ */
    final void m395(Throwable th) {
        String message;
        StackTraceElement[] stackTrace;
        String[] strArr;
        int i = 1;
        Throwable cause = th.getCause();
        String str = "exception";
        String simpleName = th.getClass().getSimpleName();
        if (cause == null) {
            message = th.getMessage();
        } else {
            message = cause.getMessage();
        }
        if (cause == null) {
            stackTrace = th.getStackTrace();
        } else {
            stackTrace = cause.getStackTrace();
        }
        if (stackTrace == null) {
            strArr = new String[]{message};
        } else {
            String[] strArr2 = new String[(stackTrace.length + 1)];
            strArr2[0] = message;
            while (i < stackTrace.length) {
                strArr2[i] = stackTrace[i].toString();
                i++;
            }
            strArr = strArr2;
        }
        m380(str, simpleName, strArr);
    }

    /* renamed from: ˎ */
    final void m391(String str, String str2) {
        m380("server_request", str, str2);
    }

    /* renamed from: ˎ */
    final void m390(String str, int i, String str2) {
        m380("server_response", str, String.valueOf(i), str2);
    }

    /* renamed from: ˋ */
    final void m388(String str, String str2) {
        m380(null, str, str2);
    }

    /* renamed from: ˋ */
    private synchronized void m380(String str, String str2, String... strArr) {
        Object obj = 1;
        synchronized (this) {
            if (!(this.f352 && (this.f347 || this.f351))) {
                obj = null;
            }
            if (obj != null && this.f348 < 98304) {
                try {
                    long currentTimeMillis = System.currentTimeMillis();
                    String str3 = "";
                    if (strArr.length > 0) {
                        StringBuilder stringBuilder = new StringBuilder();
                        for (int length = strArr.length - 1; length > 0; length--) {
                            stringBuilder.append(strArr[length]).append(", ");
                        }
                        stringBuilder.append(strArr[0]);
                        str3 = stringBuilder.toString();
                    }
                    String format = new SimpleDateFormat("MM-dd HH:mm:ss.SSS", Locale.ENGLISH).format(Long.valueOf(currentTimeMillis));
                    if (str != null) {
                        str3 = String.format("%18s %5s _/%s [%s] %s %s", new Object[]{format, Long.valueOf(Thread.currentThread().getId()), AppsFlyerLib.LOG_TAG, str, str2, str3});
                    } else {
                        str3 = String.format("%18s %5s %s/%s %s", new Object[]{format, Long.valueOf(Thread.currentThread().getId()), str2, AppsFlyerLib.LOG_TAG, str3});
                    }
                    this.f344.put(str3);
                    this.f348 = str3.getBytes().length + this.f348;
                } catch (Throwable th) {
                }
            }
        }
    }

    /* renamed from: ॱॱ */
    private synchronized String m384() {
        String str;
        str = null;
        try {
            this.f346.put(ShareConstants.WEB_DIALOG_PARAM_DATA, this.f344);
            str = this.f346.toString();
            m376();
        } catch (JSONException e) {
        }
        return str;
    }

    /* renamed from: ˏ */
    private synchronized void m381(String str, PackageManager packageManager) {
        AppsFlyerProperties instance = AppsFlyerProperties.getInstance();
        AppsFlyerLib instance2 = AppsFlyerLib.getInstance();
        String string = instance.getString("remote_debug_static_data");
        if (string != null) {
            try {
                this.f346 = new JSONObject(string);
            } catch (Throwable th) {
            }
        } else {
            this.f346 = new JSONObject();
            m383(Build.BRAND, Build.MODEL, VERSION.RELEASE, instance.getString(ServerParameters.ADVERTISING_ID_PARAM), instance2.f172, instance2.f176);
            m382("4.8.11.383", instance.getString(AppsFlyerProperties.AF_KEY), instance.getString("KSAppsFlyerId"), instance.getString("uid"));
            try {
                int i = packageManager.getPackageInfo(str, 0).versionCode;
                m379(str, String.valueOf(i), instance.getString(AppsFlyerProperties.CHANNEL), instance.getString("preInstallName"));
            } catch (Throwable th2) {
            }
            instance.set("remote_debug_static_data", this.f346.toString());
        }
        try {
            this.f346.put("launch_counter", this.f350);
        } catch (Throwable e) {
            e.printStackTrace();
        }
    }

    /* renamed from: ʻ */
    private synchronized void m376() {
        this.f344 = null;
        this.f344 = new JSONArray();
        this.f348 = 0;
    }

    /* renamed from: ˏ */
    final synchronized void m392() {
        this.f347 = false;
        m376();
    }

    /* renamed from: ᐝ */
    final void m396() {
        this.f352 = false;
    }

    /* renamed from: ʽ */
    final boolean m385() {
        return this.f351;
    }

    /* renamed from: ˊ */
    static void m377(String str, PackageManager packageManager) {
        try {
            if (f332 == null) {
                f332 = new C0300y();
            }
            f332.m381(str, packageManager);
            if (f332 == null) {
                f332 = new C0300y();
            }
            String ॱॱ = f332.m384();
            AsyncTask c0280l = new C0280l(null, AppsFlyerLib.getInstance().isTrackingStopped());
            c0280l.f275 = ॱॱ;
            c0280l.m320();
            c0280l.execute(new String[]{new StringBuilder().append(ServerConfigHandler.getUrl("https://monitorsdk.%s/remote-debug?app_id=")).append(str).toString()});
        } catch (Throwable th) {
        }
    }
}
