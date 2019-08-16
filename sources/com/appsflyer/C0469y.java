package com.appsflyer;

import android.content.pm.PackageManager;
import android.os.Build;
import android.os.Build.VERSION;
import com.facebook.share.internal.ShareConstants;
import java.text.SimpleDateFormat;
import java.util.Locale;
import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

/* renamed from: com.appsflyer.y */
final class C0469y {

    /* renamed from: ˊ */
    private static C0469y f353;

    /* renamed from: ʻ */
    private final String f354;

    /* renamed from: ʻॱ */
    private final String f355;

    /* renamed from: ʼ */
    private final String f356;

    /* renamed from: ʼॱ */
    private final String f357;

    /* renamed from: ʽ */
    private final String f358;

    /* renamed from: ʽॱ */
    private final String f359;

    /* renamed from: ʾ */
    private final String f360;

    /* renamed from: ʿ */
    private final String f361;

    /* renamed from: ˈ */
    private final String f362;

    /* renamed from: ˉ */
    private final String f363;

    /* renamed from: ˊˊ */
    private final String f364;

    /* renamed from: ˊˋ */
    private JSONArray f365;

    /* renamed from: ˊॱ */
    private final String f366;

    /* renamed from: ˊᐝ */
    private JSONObject f367;

    /* renamed from: ˋ */
    private boolean f368;

    /* renamed from: ˋˊ */
    private int f369;

    /* renamed from: ˋॱ */
    private final String f370;

    /* renamed from: ˌ */
    private String f371;

    /* renamed from: ˍ */
    private boolean f372;

    /* renamed from: ˎ */
    private boolean f373;

    /* renamed from: ˏ */
    private final String f374;

    /* renamed from: ˏॱ */
    private final String f375;

    /* renamed from: ͺ */
    private final String f376;

    /* renamed from: ॱ */
    private final String f377;

    /* renamed from: ॱˊ */
    private final String f378;

    /* renamed from: ॱˋ */
    private final String f379;

    /* renamed from: ॱˎ */
    private final String f380;

    /* renamed from: ॱॱ */
    private final String f381;

    /* renamed from: ॱᐝ */
    private final String f382;

    /* renamed from: ᐝ */
    private final String f383;

    /* renamed from: ᐝॱ */
    private final String f384;

    private C0469y() {
        this.f368 = true;
        this.f373 = true;
        this.f377 = "brand";
        this.f374 = "model";
        this.f381 = "platform";
        this.f358 = "platform_version";
        this.f383 = ServerParameters.ADVERTISING_ID_PARAM;
        this.f354 = "imei";
        this.f356 = "android_id";
        this.f378 = "sdk_version";
        this.f375 = "devkey";
        this.f366 = "originalAppsFlyerId";
        this.f370 = "uid";
        this.f376 = "app_id";
        this.f384 = "app_version";
        this.f382 = AppsFlyerProperties.CHANNEL;
        this.f379 = "preInstall";
        this.f355 = ShareConstants.WEB_DIALOG_PARAM_DATA;
        this.f380 = "r_debugging_off";
        this.f357 = "r_debugging_on";
        this.f362 = "public_api_call";
        this.f361 = "exception";
        this.f360 = "server_request";
        this.f359 = "server_response";
        this.f363 = "yyyy-MM-dd HH:mm:ssZ";
        this.f364 = "MM-dd HH:mm:ss.SSS";
        this.f369 = 0;
        this.f371 = "-1";
        this.f365 = new JSONArray();
        this.f369 = 0;
        this.f372 = false;
    }

    /* renamed from: ˋ */
    static C0469y m373() {
        if (f353 == null) {
            f353 = new C0469y();
        }
        return f353;
    }

    /* access modifiers changed from: 0000 */
    /* renamed from: ˊ */
    public final synchronized void mo6641(String str) {
        this.f371 = str;
    }

    /* access modifiers changed from: 0000 */
    /* renamed from: ˎ */
    public final synchronized void mo6643() {
        this.f372 = true;
        m375("r_debugging_on", new SimpleDateFormat("yyyy-MM-dd HH:mm:ssZ", Locale.ENGLISH).format(Long.valueOf(System.currentTimeMillis())), new String[0]);
    }

    /* access modifiers changed from: 0000 */
    /* renamed from: ॱ */
    public final synchronized void mo6648() {
        m375("r_debugging_off", new SimpleDateFormat("yyyy-MM-dd HH:mm:ssZ", Locale.ENGLISH).format(Long.valueOf(System.currentTimeMillis())), new String[0]);
        this.f372 = false;
        this.f368 = false;
    }

    /* access modifiers changed from: 0000 */
    /* renamed from: ˊ */
    public final synchronized void mo6640() {
        this.f367 = null;
        this.f365 = null;
        f353 = null;
    }

    /* renamed from: ˏ */
    private synchronized void m378(String str, String str2, String str3, String str4, String str5, String str6) {
        try {
            this.f367.put("brand", str);
            this.f367.put("model", str2);
            this.f367.put("platform", "Android");
            this.f367.put("platform_version", str3);
            if (str4 != null && str4.length() > 0) {
                this.f367.put(ServerParameters.ADVERTISING_ID_PARAM, str4);
            }
            if (str5 != null && str5.length() > 0) {
                this.f367.put("imei", str5);
            }
            if (str6 != null && str6.length() > 0) {
                this.f367.put("android_id", str6);
            }
        } catch (Throwable th) {
        }
    }

    /* renamed from: ˏ */
    private synchronized void m377(String str, String str2, String str3, String str4) {
        try {
            this.f367.put("sdk_version", str);
            if (str2 != null && str2.length() > 0) {
                this.f367.put("devkey", str2);
            }
            if (str3 != null && str3.length() > 0) {
                this.f367.put("originalAppsFlyerId", str3);
            }
            if (str4 != null && str4.length() > 0) {
                this.f367.put("uid", str4);
            }
        } catch (Throwable th) {
        }
    }

    /* renamed from: ˋ */
    private synchronized void m374(String str, String str2, String str3, String str4) {
        if (str != null) {
            try {
                if (str.length() > 0) {
                    this.f367.put("app_id", str);
                }
            } catch (Throwable th) {
            }
        }
        if (str2 != null && str2.length() > 0) {
            this.f367.put("app_version", str2);
        }
        if (str3 != null && str3.length() > 0) {
            this.f367.put(AppsFlyerProperties.CHANNEL, str3);
        }
        if (str4 != null && str4.length() > 0) {
            this.f367.put("preInstall", str4);
        }
    }

    /* access modifiers changed from: 0000 */
    /* renamed from: ˏ */
    public final void mo6647(String str, String... strArr) {
        m375("public_api_call", str, strArr);
    }

    /* access modifiers changed from: 0000 */
    /* renamed from: ॱ */
    public final void mo6649(Throwable th) {
        String message;
        StackTraceElement[] stackTrace;
        String[] strArr;
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
            for (int i = 1; i < stackTrace.length; i++) {
                strArr2[i] = stackTrace[i].toString();
            }
            strArr = strArr2;
        }
        m375(str, simpleName, strArr);
    }

    /* access modifiers changed from: 0000 */
    /* renamed from: ˎ */
    public final void mo6645(String str, String str2) {
        m375("server_request", str, str2);
    }

    /* access modifiers changed from: 0000 */
    /* renamed from: ˎ */
    public final void mo6644(String str, int i, String str2) {
        m375("server_response", str, String.valueOf(i), str2);
    }

    /* access modifiers changed from: 0000 */
    /* renamed from: ˋ */
    public final void mo6642(String str, String str2) {
        m375(null, str, str2);
    }

    /* renamed from: ˋ */
    private synchronized void m375(String str, String str2, String... strArr) {
        String format;
        boolean z = true;
        synchronized (this) {
            if (!this.f373 || (!this.f368 && !this.f372)) {
                z = false;
            }
            if (z && this.f369 < 98304) {
                try {
                    long currentTimeMillis = System.currentTimeMillis();
                    String str3 = "";
                    if (strArr.length > 0) {
                        StringBuilder sb = new StringBuilder();
                        for (int length = strArr.length - 1; length > 0; length--) {
                            sb.append(strArr[length]).append(", ");
                        }
                        sb.append(strArr[0]);
                        str3 = sb.toString();
                    }
                    String format2 = new SimpleDateFormat("MM-dd HH:mm:ss.SSS", Locale.ENGLISH).format(Long.valueOf(currentTimeMillis));
                    if (str != null) {
                        format = String.format("%18s %5s _/%s [%s] %s %s", new Object[]{format2, Long.valueOf(Thread.currentThread().getId()), AppsFlyerLib.LOG_TAG, str, str2, str3});
                    } else {
                        format = String.format("%18s %5s %s/%s %s", new Object[]{format2, Long.valueOf(Thread.currentThread().getId()), str2, AppsFlyerLib.LOG_TAG, str3});
                    }
                    this.f365.put(format);
                    this.f369 = format.getBytes().length + this.f369;
                } catch (Throwable th) {
                }
            }
        }
    }

    /* renamed from: ॱॱ */
    private synchronized String m379() {
        String str;
        str = null;
        try {
            this.f367.put(ShareConstants.WEB_DIALOG_PARAM_DATA, this.f365);
            str = this.f367.toString();
            m371();
        } catch (JSONException e) {
        }
        return str;
    }

    /* renamed from: ˏ */
    private synchronized void m376(String str, PackageManager packageManager) {
        AppsFlyerProperties instance = AppsFlyerProperties.getInstance();
        AppsFlyerLib instance2 = AppsFlyerLib.getInstance();
        String string = instance.getString("remote_debug_static_data");
        if (string != null) {
            try {
                this.f367 = new JSONObject(string);
            } catch (Throwable th) {
            }
        } else {
            this.f367 = new JSONObject();
            m378(Build.BRAND, Build.MODEL, VERSION.RELEASE, instance.getString(ServerParameters.ADVERTISING_ID_PARAM), instance2.f160, instance2.f164);
            m377("4.8.11.383", instance.getString(AppsFlyerProperties.AF_KEY), instance.getString("KSAppsFlyerId"), instance.getString("uid"));
            try {
                int i = packageManager.getPackageInfo(str, 0).versionCode;
                m374(str, String.valueOf(i), instance.getString(AppsFlyerProperties.CHANNEL), instance.getString("preInstallName"));
            } catch (Throwable th2) {
            }
            instance.set("remote_debug_static_data", this.f367.toString());
        }
        try {
            this.f367.put("launch_counter", this.f371);
        } catch (JSONException e) {
            e.printStackTrace();
        }
        return;
    }

    /* renamed from: ʻ */
    private synchronized void m371() {
        this.f365 = null;
        this.f365 = new JSONArray();
        this.f369 = 0;
    }

    /* access modifiers changed from: 0000 */
    /* renamed from: ˏ */
    public final synchronized void mo6646() {
        this.f368 = false;
        m371();
    }

    /* access modifiers changed from: 0000 */
    /* renamed from: ᐝ */
    public final void mo6650() {
        this.f373 = false;
    }

    /* access modifiers changed from: 0000 */
    /* renamed from: ʽ */
    public final boolean mo6639() {
        return this.f372;
    }

    /* renamed from: ˊ */
    static void m372(String str, PackageManager packageManager) {
        try {
            if (f353 == null) {
                f353 = new C0469y();
            }
            f353.m376(str, packageManager);
            if (f353 == null) {
                f353 = new C0469y();
            }
            String r0 = f353.m379();
            C0447l lVar = new C0447l(null, AppsFlyerLib.getInstance().isTrackingStopped());
            lVar.f296 = r0;
            lVar.mo6583();
            lVar.execute(new String[]{new StringBuilder().append(ServerConfigHandler.getUrl("https://monitorsdk.%s/remote-debug?app_id=")).append(str).toString()});
        } catch (Throwable th) {
        }
    }
}
