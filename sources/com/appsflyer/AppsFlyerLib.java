package com.appsflyer;

import android.app.Activity;
import android.app.Application;
import android.content.ContentResolver;
import android.content.Context;
import android.content.Intent;
import android.content.IntentFilter;
import android.content.SharedPreferences;
import android.content.SharedPreferences.Editor;
import android.content.pm.PackageInfo;
import android.content.pm.PackageManager;
import android.content.pm.PackageManager.NameNotFoundException;
import android.database.Cursor;
import android.net.Uri;
import android.os.AsyncTask;
import android.os.Build;
import android.os.Build.VERSION;
import android.os.Bundle;
import android.os.Looper;
import android.os.Process;
import android.support.annotation.NonNull;
import android.support.annotation.Nullable;
import android.text.TextUtils;
import com.appsflyer.AFLogger.LogLevel;
import com.appsflyer.AppsFlyerProperties.EmailsCryptType;
import com.appsflyer.C0270f.C02695;
import com.appsflyer.C0273g.C0271a;
import com.appsflyer.C0273g.C0272c;
import com.appsflyer.C0290q.C0251a;
import com.appsflyer.C0292s.C0248b;
import com.appsflyer.C0299u.C0298c;
import com.appsflyer.OneLinkHttpTask.HttpsUrlConnectionProvider;
import com.appsflyer.cache.CacheManager;
import com.appsflyer.cache.RequestCacheData;
import com.appsflyer.share.Constants;
import com.facebook.internal.NativeProtocol;
import com.facebook.internal.ServerProtocol;
import com.google.android.gms.common.GoogleApiAvailability;
import com.google.firebase.analytics.FirebaseAnalytics.Param;
import io.fabric.sdk.android.services.common.IdManager;
import io.fabric.sdk.android.services.network.HttpRequest;
import java.io.BufferedReader;
import java.io.File;
import java.io.FileNotFoundException;
import java.io.FileReader;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.io.OutputStreamWriter;
import java.io.Reader;
import java.io.Writer;
import java.lang.ref.WeakReference;
import java.net.HttpURLConnection;
import java.net.URL;
import java.text.DateFormat;
import java.text.SimpleDateFormat;
import java.util.AbstractCollection;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.Collections;
import java.util.Date;
import java.util.HashMap;
import java.util.Iterator;
import java.util.LinkedHashMap;
import java.util.List;
import java.util.Locale;
import java.util.Map;
import java.util.Properties;
import java.util.TimeZone;
import java.util.concurrent.ConcurrentHashMap;
import java.util.concurrent.ExecutorService;
import java.util.concurrent.ScheduledExecutorService;
import java.util.concurrent.TimeUnit;
import java.util.concurrent.atomic.AtomicInteger;
import net.gogame.gowrap.integrations.AbstractIntegrationSupport;
import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

public class AppsFlyerLib implements C0259b {
    public static final String AF_PRE_INSTALL_PATH = "AF_PRE_INSTALL_PATH";
    public static final String ATTRIBUTION_ID_COLUMN_NAME = "aid";
    public static final String ATTRIBUTION_ID_CONTENT_URI = "content://com.facebook.katana.provider.AttributionIdProvider";
    public static final String IS_STOP_TRACKING_USED = "is_stop_tracking_used";
    public static final String LOG_TAG = "AppsFlyer_4.8.11";
    public static final String PRE_INSTALL_SYSTEM_DEFAULT = "/data/local/tmp/pre_install.appsflyer";
    public static final String PRE_INSTALL_SYSTEM_DEFAULT_ETC = "/etc/pre_install.appsflyer";
    public static final String PRE_INSTALL_SYSTEM_RO_PROP = "ro.appsflyer.preinstall.path";
    /* renamed from: ʼ */
    private static String f154 = new StringBuilder("https://t.%s/api/v").append(f155).toString();
    /* renamed from: ʽ */
    private static final String f155 = new StringBuilder().append(f162).append("/androidevent?buildnumber=4.8.11&app_id=").toString();
    /* renamed from: ˈ */
    private static AppsFlyerLib f156 = new AppsFlyerLib();
    /* renamed from: ˊॱ */
    private static final List<String> f157 = Arrays.asList(new String[]{"is_cache"});
    /* renamed from: ˋॱ */
    private static final List<String> f158 = Arrays.asList(new String[]{"googleplay", "playstore", "googleplaystore"});
    /* renamed from: ˎ */
    static final String f159 = new StringBuilder("https://register.%s/api/v").append(f155).toString();
    /* renamed from: ˏ */
    static AppsFlyerInAppPurchaseValidatorListener f160 = null;
    /* renamed from: ˏॱ */
    private static AppsFlyerConversionListener f161 = null;
    /* renamed from: ॱ */
    static final String f162 = BuildConfig.AF_SDK_VERSION.substring(0, BuildConfig.AF_SDK_VERSION.indexOf(AbstractIntegrationSupport.DEFAULT_EVENT_NAME_DELIMITER));
    /* renamed from: ॱॱ */
    private static String f163 = new StringBuilder("https://attr.%s/api/v").append(f155).toString();
    /* renamed from: ᐝ */
    private static String f164 = new StringBuilder("https://events.%s/api/v").append(f155).toString();
    /* renamed from: ʻ */
    private long f165 = -1;
    /* renamed from: ʻॱ */
    private Map<String, String> f166;
    /* renamed from: ʼॱ */
    private C0251a f167;
    /* renamed from: ʽॱ */
    private long f168;
    /* renamed from: ʾ */
    private long f169;
    /* renamed from: ʿ */
    private Uri f170 = null;
    /* renamed from: ˉ */
    private boolean f171 = false;
    /* renamed from: ˊ */
    String f172;
    /* renamed from: ˊˊ */
    private long f173;
    /* renamed from: ˊˋ */
    private Map<Long, String> f174;
    /* renamed from: ˊᐝ */
    private boolean f175 = false;
    /* renamed from: ˋ */
    String f176;
    /* renamed from: ˋˊ */
    private String f177;
    /* renamed from: ˋˋ */
    private boolean f178 = false;
    /* renamed from: ˋᐝ */
    private boolean f179 = false;
    /* renamed from: ˌ */
    private C0297t f180 = new C0297t();
    /* renamed from: ˍ */
    private boolean f181;
    /* renamed from: ˎˎ */
    private boolean f182;
    /* renamed from: ͺ */
    private long f183 = -1;
    /* renamed from: ॱˊ */
    private long f184 = TimeUnit.SECONDS.toMillis(5);
    /* renamed from: ॱˋ */
    private ScheduledExecutorService f185 = null;
    /* renamed from: ॱˎ */
    private C0266e f186 = null;
    /* renamed from: ॱᐝ */
    private boolean f187 = false;
    /* renamed from: ᐝॱ */
    private long f188;

    /* renamed from: com.appsflyer.AppsFlyerLib$3 */
    class C02523 implements C0251a {
        /* renamed from: ˊ */
        private /* synthetic */ AppsFlyerLib f128;

        C02523(AppsFlyerLib appsFlyerLib) {
            this.f128 = appsFlyerLib;
        }

        /* renamed from: ॱ */
        public final void mo1206(Activity activity) {
            if (2 > AppsFlyerLib.m239(AppsFlyerLib.m240((Context) activity))) {
                C0270f ˏ = C0270f.m295(activity);
                ˏ.f239.post(ˏ.f236);
                ˏ.f239.post(ˏ.f237);
            }
            AFLogger.afInfoLog("onBecameForeground");
            AppsFlyerLib.getInstance().m263();
            AppsFlyerLib.getInstance().m262((Context) activity, null, null);
            AFLogger.resetDeltaTime();
        }

        /* renamed from: ॱ */
        public final void mo1207(WeakReference<Context> weakReference) {
            C0265d.m287((Context) weakReference.get());
            C0270f ˏ = C0270f.m295((Context) weakReference.get());
            ˏ.f239.post(ˏ.f236);
        }
    }

    /* renamed from: com.appsflyer.AppsFlyerLib$4 */
    static /* synthetic */ class C02534 {
        /* renamed from: ॱ */
        static final /* synthetic */ int[] f129 = new int[EmailsCryptType.values().length];

        static {
            try {
                f129[EmailsCryptType.SHA1.ordinal()] = 1;
            } catch (NoSuchFieldError e) {
            }
            try {
                f129[EmailsCryptType.MD5.ordinal()] = 2;
            } catch (NoSuchFieldError e2) {
            }
            try {
                f129[EmailsCryptType.SHA256.ordinal()] = 3;
            } catch (NoSuchFieldError e3) {
            }
            try {
                f129[EmailsCryptType.NONE.ordinal()] = 4;
            } catch (NoSuchFieldError e4) {
            }
        }
    }

    /* renamed from: com.appsflyer.AppsFlyerLib$d */
    abstract class C0254d implements Runnable {
        /* renamed from: ˊ */
        private String f130;
        /* renamed from: ˋ */
        WeakReference<Context> f131 = null;
        /* renamed from: ˎ */
        private ScheduledExecutorService f132;
        /* renamed from: ˏ */
        private AtomicInteger f133 = new AtomicInteger(0);
        /* renamed from: ॱ */
        private /* synthetic */ AppsFlyerLib f134;

        /* renamed from: ˊ */
        protected abstract void mo1208(String str, int i);

        /* renamed from: ˊ */
        protected abstract void mo1209(Map<String, String> map);

        /* renamed from: ॱ */
        public abstract String mo1210();

        C0254d(AppsFlyerLib appsFlyerLib, Context context, String str, ScheduledExecutorService scheduledExecutorService) {
            this.f134 = appsFlyerLib;
            this.f131 = new WeakReference(context);
            this.f130 = str;
            if (scheduledExecutorService == null) {
                this.f132 = AFExecutor.getInstance().m179();
            } else {
                this.f132 = scheduledExecutorService;
            }
        }

        public void run() {
            HttpURLConnection httpURLConnection;
            Map ˋ;
            Throwable e;
            if (this.f130 != null && this.f130.length() != 0 && !this.f134.isTrackingStopped()) {
                this.f133.incrementAndGet();
                try {
                    Context context = (Context) this.f131.get();
                    if (context == null) {
                        this.f133.decrementAndGet();
                        return;
                    }
                    long currentTimeMillis = System.currentTimeMillis();
                    String ॱ = AppsFlyerLib.m209(context, AppsFlyerLib.m223(new WeakReference(context)));
                    String str = "";
                    if (ॱ != null) {
                        if (AppsFlyerLib.f158.contains(ॱ.toLowerCase())) {
                            AFLogger.afWarnLog(String.format("AF detected using redundant Google-Play channel for attribution - %s. Using without channel postfix.", new Object[]{ॱ}));
                        } else {
                            str = "-".concat(String.valueOf(ॱ));
                        }
                    }
                    StringBuilder append = new StringBuilder().append(mo1210()).append(context.getPackageName()).append(str).append("?devkey=").append(this.f130).append("&device_id=").append(C0288p.m335(new WeakReference(context)));
                    C0300y.m378().m391(append.toString(), "");
                    C02695.m293(new StringBuilder("Calling server for attribution url: ").append(append.toString()).toString());
                    httpURLConnection = (HttpURLConnection) new URL(append.toString()).openConnection();
                    Map ˎ;
                    try {
                        httpURLConnection.setRequestMethod(HttpRequest.METHOD_GET);
                        httpURLConnection.setConnectTimeout(10000);
                        httpURLConnection.setRequestProperty("Connection", "close");
                        httpURLConnection.connect();
                        int responseCode = httpURLConnection.getResponseCode();
                        String ˎ2 = AppsFlyerLib.m231(httpURLConnection);
                        C0300y.m378().m390(append.toString(), responseCode, ˎ2);
                        if (responseCode == 200) {
                            AppsFlyerLib.m216(context, "appsflyerGetConversionDataTiming", (System.currentTimeMillis() - currentTimeMillis) / 1000);
                            C02695.m293("Attribution data: ".concat(String.valueOf(ˎ2)));
                            if (ˎ2.length() > 0 && context != null) {
                                ˋ = AppsFlyerLib.m250(ˎ2);
                                String str2 = (String) ˋ.get("iscache");
                                if (str2 != null && Boolean.toString(false).equals(str2)) {
                                    AppsFlyerLib.m216(context, "appsflyerConversionDataCacheExpiration", System.currentTimeMillis());
                                }
                                if (ˋ.containsKey(Constants.URL_SITE_ID)) {
                                    if (ˋ.containsKey("af_channel")) {
                                        AFLogger.afDebugLog(new StringBuilder(Constants.LOG_INVITE_DETECTED_APP_INVITE_VIA_CHANNEL).append((String) ˋ.get("af_channel")).toString());
                                    } else {
                                        AFLogger.afDebugLog(String.format(Constants.LOG_CROSS_PROMOTION_APP_INSTALLED_FROM_CROSS_PROMOTION, new Object[]{ˋ.get(Constants.URL_SITE_ID)}));
                                    }
                                }
                                if (ˋ.containsKey(Constants.URL_SITE_ID)) {
                                    AFLogger.afDebugLog(new StringBuilder(Constants.LOG_INVITE_DETECTED_APP_INVITE_VIA_CHANNEL).append((String) ˋ.get("af_channel")).toString());
                                }
                                ˋ.put("is_first_launch", Boolean.toString(false));
                                ॱ = new JSONObject(ˋ).toString();
                                if (ॱ != null) {
                                    AppsFlyerLib.m252(context, "attributionId", ॱ);
                                } else {
                                    AppsFlyerLib.m252(context, "attributionId", ˎ2);
                                }
                                AFLogger.afDebugLog(new StringBuilder("iscache=").append(str2).append(" caching conversion data").toString());
                                if (AppsFlyerLib.f161 != null && this.f133.intValue() <= 1) {
                                    ˎ = AppsFlyerLib.m212(context);
                                    mo1209(ˎ);
                                }
                            }
                        } else {
                            if (AppsFlyerLib.f161 != null) {
                                mo1208("Error connection to server: ".concat(String.valueOf(responseCode)), responseCode);
                            }
                            C02695.m293(new StringBuilder("AttributionIdFetcher response code: ").append(responseCode).append("  url: ").append(append).toString());
                        }
                    } catch (Throwable e2) {
                        AFLogger.afErrorLog("Exception while trying to fetch attribution data. ", e2);
                        ˎ = ˋ;
                    } catch (Throwable th) {
                        e2 = th;
                        try {
                            if (AppsFlyerLib.f161 != null) {
                                mo1208(e2.getMessage(), 0);
                            }
                            AFLogger.afErrorLog(e2.getMessage(), e2);
                            this.f133.decrementAndGet();
                            if (httpURLConnection != null) {
                                httpURLConnection.disconnect();
                            }
                            this.f132.shutdown();
                        } catch (Throwable th2) {
                            e2 = th2;
                            this.f133.decrementAndGet();
                            if (httpURLConnection != null) {
                                httpURLConnection.disconnect();
                            }
                            throw e2;
                        }
                    }
                    this.f133.decrementAndGet();
                    if (httpURLConnection != null) {
                        httpURLConnection.disconnect();
                    }
                    this.f132.shutdown();
                } catch (Throwable th3) {
                    e2 = th3;
                    httpURLConnection = null;
                    this.f133.decrementAndGet();
                    if (httpURLConnection != null) {
                        httpURLConnection.disconnect();
                    }
                    throw e2;
                }
            }
        }
    }

    /* renamed from: com.appsflyer.AppsFlyerLib$a */
    class C0255a extends C0254d {
        /* renamed from: ˎ */
        private /* synthetic */ AppsFlyerLib f135;

        public C0255a(AppsFlyerLib appsFlyerLib, Context context, String str, ScheduledExecutorService scheduledExecutorService) {
            this.f135 = appsFlyerLib;
            super(appsFlyerLib, context, str, scheduledExecutorService);
        }

        /* renamed from: ॱ */
        public final String mo1210() {
            return ServerConfigHandler.getUrl("https://api.%s/install_data/v3/");
        }

        /* renamed from: ˊ */
        protected final void mo1209(Map<String, String> map) {
            map.put("is_first_launch", Boolean.toString(true));
            AppsFlyerLib.f161.onInstallConversionDataLoaded(map);
            AppsFlyerLib.m234((Context) this.f131.get(), "appsflyerConversionDataRequestRetries", 0);
        }

        /* renamed from: ˊ */
        protected final void mo1208(String str, int i) {
            AppsFlyerLib.f161.onInstallConversionFailure(str);
            if (i >= 400 && i < 500) {
                AppsFlyerLib.m234((Context) this.f131.get(), "appsflyerConversionDataRequestRetries", AppsFlyerLib.m240((Context) this.f131.get()).getInt("appsflyerConversionDataRequestRetries", 0) + 1);
            }
        }
    }

    /* renamed from: com.appsflyer.AppsFlyerLib$b */
    class C0256b implements Runnable {
        /* renamed from: ʻ */
        private boolean f136;
        /* renamed from: ʼ */
        private ExecutorService f137;
        /* renamed from: ʽ */
        private String f138;
        /* renamed from: ˊ */
        private String f139;
        /* renamed from: ˋ */
        private String f140;
        /* renamed from: ˎ */
        private String f141;
        /* renamed from: ˏ */
        private WeakReference<Context> f142;
        /* renamed from: ॱ */
        private final Intent f143;
        /* renamed from: ॱॱ */
        private boolean f144;
        /* renamed from: ᐝ */
        private /* synthetic */ AppsFlyerLib f145;

        private C0256b(AppsFlyerLib appsFlyerLib, WeakReference<Context> weakReference, String str, String str2, String str3, String str4, boolean z, boolean z2, Intent intent) {
            this.f145 = appsFlyerLib;
            this.f142 = weakReference;
            this.f139 = str;
            this.f140 = str2;
            this.f141 = str3;
            this.f138 = str4;
            this.f144 = true;
            this.f137 = z;
            this.f136 = z2;
            this.f143 = intent;
        }

        public final void run() {
            AppsFlyerLib.m235(this.f145, (Context) this.f142.get(), this.f139, this.f140, this.f141, this.f138, this.f144, this.f136, this.f143);
        }
    }

    /* renamed from: com.appsflyer.AppsFlyerLib$c */
    class C0257c implements Runnable {
        /* renamed from: ˎ */
        private /* synthetic */ AppsFlyerLib f146;
        /* renamed from: ˏ */
        private WeakReference<Context> f147 = null;

        public C0257c(AppsFlyerLib appsFlyerLib, Context context) {
            this.f146 = appsFlyerLib;
            this.f147 = new WeakReference(context);
        }

        public final void run() {
            if (!this.f146.f187) {
                this.f146.f188 = System.currentTimeMillis();
                if (this.f147 != null) {
                    this.f146.f187 = true;
                    try {
                        String ˎ = AppsFlyerProperties.getInstance().getString(AppsFlyerProperties.AF_KEY);
                        synchronized (this.f147) {
                            for (RequestCacheData requestCacheData : CacheManager.getInstance().getCachedRequests((Context) this.f147.get())) {
                                AFLogger.afInfoLog(new StringBuilder("resending request: ").append(requestCacheData.getRequestURL()).toString());
                                try {
                                    AppsFlyerLib.m236(this.f146, new StringBuilder().append(requestCacheData.getRequestURL()).append("&isCachedRequest=true&timeincache=").append(Long.toString((System.currentTimeMillis() - Long.parseLong(requestCacheData.getCacheKey(), 10)) / 1000)).toString(), requestCacheData.getPostData(), ˎ, this.f147, requestCacheData.getCacheKey(), false);
                                } catch (Throwable e) {
                                    AFLogger.afErrorLog("Failed to resend cached request", e);
                                }
                            }
                        }
                        this.f146.f187 = false;
                    } catch (Throwable e2) {
                        try {
                            AFLogger.afErrorLog("failed to check cache. ", e2);
                        } finally {
                            this.f146.f187 = false;
                        }
                    }
                    this.f146.f185.shutdown();
                    this.f146.f185 = null;
                }
            }
        }
    }

    /* renamed from: com.appsflyer.AppsFlyerLib$e */
    class C0258e implements Runnable {
        /* renamed from: ˊ */
        private int f148;
        /* renamed from: ˋ */
        private boolean f149;
        /* renamed from: ˎ */
        private String f150;
        /* renamed from: ˏ */
        private WeakReference<Context> f151;
        /* renamed from: ॱ */
        private Map<String, Object> f152;
        /* renamed from: ᐝ */
        private /* synthetic */ AppsFlyerLib f153;

        private C0258e(AppsFlyerLib appsFlyerLib, String str, Map<String, Object> map, Context context, boolean z, int i) {
            this.f153 = appsFlyerLib;
            this.f151 = null;
            this.f150 = str;
            this.f152 = map;
            this.f151 = new WeakReference(context);
            this.f149 = z;
            this.f148 = i;
        }

        public final void run() {
            String str = null;
            if (!this.f153.isTrackingStopped()) {
                if (this.f149 && this.f148 <= 2 && AppsFlyerLib.m247(this.f153)) {
                    this.f152.put("rfr", this.f153.f166);
                }
                try {
                    String str2 = (String) this.f152.get("appsflyerKey");
                    str = AFHelper.convertToJsonObject(this.f152).toString();
                    AppsFlyerLib.m236(this.f153, this.f150, str, str2, this.f151, null, this.f149);
                } catch (Throwable e) {
                    Throwable th = e;
                    AFLogger.afErrorLog("Exception while sending request to server. ", th);
                    if (str != null && this.f151 != null && !this.f150.contains("&isCachedRequest=true&timeincache=")) {
                        CacheManager.getInstance().cacheRequest(new RequestCacheData(this.f150, str, BuildConfig.AF_SDK_VERSION), (Context) this.f151.get());
                        AFLogger.afErrorLog(th.getMessage(), th);
                    }
                } catch (Throwable e2) {
                    AFLogger.afErrorLog(e2.getMessage(), e2);
                }
            }
        }
    }

    /* renamed from: ˊ */
    private static String m209(Context context, String str) throws NameNotFoundException {
        SharedPreferences sharedPreferences = context.getSharedPreferences("appsflyer-data", 0);
        if (sharedPreferences.contains("CACHED_CHANNEL")) {
            return sharedPreferences.getString("CACHED_CHANNEL", null);
        }
        m252(context, "CACHED_CHANNEL", str);
        return str;
    }

    /* renamed from: ˋ */
    static /* synthetic */ String m223(WeakReference weakReference) {
        String string = AppsFlyerProperties.getInstance().getString(AppsFlyerProperties.CHANNEL);
        return string == null ? m224(weakReference, "CHANNEL") : string;
    }

    /* renamed from: ˎ */
    static /* synthetic */ void m235(AppsFlyerLib appsFlyerLib, Context context, String str, String str2, String str3, String str4, boolean z, boolean z2, Intent intent) {
        if (context == null) {
            AFLogger.afDebugLog("sendTrackingWithEvent - got null context. skipping event/launch.");
            return;
        }
        SharedPreferences sharedPreferences = context.getSharedPreferences("appsflyer-data", 0);
        AppsFlyerProperties.getInstance().saveProperties(sharedPreferences);
        if (!appsFlyerLib.isTrackingStopped()) {
            AFLogger.afInfoLog(new StringBuilder("sendTrackingWithEvent from activity: ").append(context.getClass().getName()).toString());
        }
        boolean z3 = str2 == null;
        Map ˎ = appsFlyerLib.m259(context, str, str2, str3, str4, z, sharedPreferences, z3, intent);
        String str5 = (String) ˎ.get("appsflyerKey");
        if (str5 == null || str5.length() == 0) {
            AFLogger.afDebugLog("Not sending data yet, waiting for dev key");
            return;
        }
        if (!appsFlyerLib.isTrackingStopped()) {
            AFLogger.afInfoLog("AppsFlyerLib.sendTrackingWithEvent");
        }
        str5 = z3 ? z2 ? ServerConfigHandler.getUrl(f163) : ServerConfigHandler.getUrl(f154) : ServerConfigHandler.getUrl(f164);
        Runnable c0258e = new C0258e(new StringBuilder().append(str5).append(context.getPackageName()).toString(), ˎ, context.getApplicationContext(), z3, m208(sharedPreferences, "appsFlyerCount", false));
        if (z3 && m255(context)) {
            Object obj = (appsFlyerLib.f166 == null || appsFlyerLib.f166.size() <= 0) ? null : 1;
            if (obj == null) {
                AFLogger.afDebugLog("Failed to get new referrer, wait ...");
                m237(AFExecutor.getInstance().m179(), c0258e, 500, TimeUnit.MILLISECONDS);
                return;
            }
        }
        c0258e.run();
    }

    /* renamed from: ˎ */
    static /* synthetic */ void m236(AppsFlyerLib appsFlyerLib, String str, String str2, String str3, WeakReference weakReference, String str4, boolean z) throws IOException {
        URL url = new URL(str);
        AFLogger.afInfoLog(new StringBuilder("url: ").append(url.toString()).toString());
        C02695.m293("data: ".concat(String.valueOf(str2)));
        m227((Context) weakReference.get(), LOG_TAG, "EVENT_DATA", str2);
        try {
            appsFlyerLib.m218(url, str2, str3, weakReference, str4, z);
        } catch (Throwable e) {
            AFLogger.afErrorLog("Exception in sendRequestToServer. ", e);
            if (AppsFlyerProperties.getInstance().getBoolean(AppsFlyerProperties.USE_HTTP_FALLBACK, false)) {
                appsFlyerLib.m218(new URL(str.replace("https:", "http:")), str2, str3, weakReference, str4, z);
                return;
            }
            AFLogger.afInfoLog(new StringBuilder("failed to send requeset to server. ").append(e.getLocalizedMessage()).toString());
            m227((Context) weakReference.get(), LOG_TAG, "ERROR", e.getLocalizedMessage());
            throw e;
        }
    }

    /* renamed from: ˏ */
    static /* synthetic */ boolean m247(AppsFlyerLib appsFlyerLib) {
        return appsFlyerLib.f166 != null && appsFlyerLib.f166.size() > 0;
    }

    /* renamed from: ॱ */
    private static void m253(Context context, Map<String, ? super String> map) {
        C0273g c0273g = C0272c.f249;
        C0271a ˊ = C0273g.m303(context);
        map.put("network", ˊ.m302());
        if (ˊ.m301() != null) {
            map.put("operator", ˊ.m301());
        }
        if (ˊ.m300() != null) {
            map.put("carrier", ˊ.m300());
        }
    }

    /* renamed from: ॱ */
    static /* synthetic */ void m254(Map map) {
        if (f161 != null) {
            try {
                f161.onAppOpenAttribution(map);
            } catch (Throwable th) {
                AFLogger.afErrorLog(th.getLocalizedMessage(), th);
            }
        }
    }

    /* renamed from: ॱ */
    private static boolean m255(@NonNull Context context) {
        if (m208(context.getSharedPreferences("appsflyer-data", 0), "appsFlyerCount", false) > 2) {
            AFLogger.afRDLog("Install referrer will not load, the counter > 2, ");
            return false;
        }
        try {
            Class.forName("com.android.installreferrer.api.InstallReferrerClient");
            if (C0271a.m299(context, "com.google.android.finsky.permission.BIND_GET_INSTALL_REFERRER_SERVICE")) {
                AFLogger.afDebugLog("Install referrer is allowed");
                return true;
            }
            AFLogger.afDebugLog("Install referrer is not allowed");
            return false;
        } catch (ClassNotFoundException e) {
            AFLogger.afRDLog("Class com.android.installreferrer.api.InstallReferrerClient not found");
            return false;
        } catch (Throwable th) {
            AFLogger.afErrorLog("An error occurred while trying to verify manifest : com.android.installreferrer.api.InstallReferrerClient", th);
            return false;
        }
    }

    /* renamed from: ˋ */
    final void m258(Context context, String str) {
        boolean z;
        Map hashMap;
        String string;
        PackageManager packageManager;
        String string2;
        C0286n ˊ;
        WeakReference weakReference;
        Object string3;
        if (AppsFlyerProperties.getInstance().getBoolean(AppsFlyerProperties.AF_WAITFOR_CUSTOMERID, false)) {
            if (AppsFlyerProperties.getInstance().getString(AppsFlyerProperties.APP_USER_ID) == null) {
                z = true;
                if (z) {
                    hashMap = new HashMap();
                    string = AppsFlyerProperties.getInstance().getString(AppsFlyerProperties.AF_KEY);
                    if (string != null) {
                        AFLogger.afWarnLog("[registerUninstall] AppsFlyer's SDK cannot send any event without providing DevKey.");
                        return;
                    }
                    packageManager = context.getPackageManager();
                    try {
                        PackageInfo packageInfo = packageManager.getPackageInfo(context.getPackageName(), 0);
                        hashMap.put("app_version_code", Integer.toString(packageInfo.versionCode));
                        hashMap.put("app_version_name", packageInfo.versionName);
                        hashMap.put(NativeProtocol.BRIDGE_ARG_APP_NAME_STRING, packageManager.getApplicationLabel(packageInfo.applicationInfo).toString());
                        long j = packageInfo.firstInstallTime;
                        DateFormat simpleDateFormat = new SimpleDateFormat("yyyy-MM-dd_HHmmssZ", Locale.US);
                        simpleDateFormat.setTimeZone(TimeZone.getTimeZone("UTC"));
                        hashMap.put("installDate", simpleDateFormat.format(new Date(j)));
                    } catch (Throwable th) {
                        AFLogger.afErrorLog("Exception while collecting application version info.", th);
                    }
                    m253(context, hashMap);
                    string2 = AppsFlyerProperties.getInstance().getString(AppsFlyerProperties.APP_USER_ID);
                    if (string2 != null) {
                        hashMap.put("appUserId", string2);
                    }
                    try {
                        hashMap.put(IdManager.MODEL_FIELD, Build.MODEL);
                        hashMap.put("brand", Build.BRAND);
                    } catch (Throwable th2) {
                        AFLogger.afErrorLog("Exception while collecting device brand and model.", th2);
                    }
                    if (AppsFlyerProperties.getInstance().getBoolean(AppsFlyerProperties.DEVICE_TRACKING_DISABLED, false)) {
                        hashMap.put(AppsFlyerProperties.DEVICE_TRACKING_DISABLED, ServerProtocol.DIALOG_RETURN_SCOPES_TRUE);
                    }
                    ˊ = C0287o.m333(context.getContentResolver());
                    if (ˊ != null) {
                        hashMap.put("amazon_aid", ˊ.m332());
                        hashMap.put("amazon_aid_limit", String.valueOf(ˊ.m331()));
                    }
                    string2 = AppsFlyerProperties.getInstance().getString(ServerParameters.ADVERTISING_ID_PARAM);
                    if (string2 != null) {
                        hashMap.put(ServerParameters.ADVERTISING_ID_PARAM, string2);
                    }
                    hashMap.put("devkey", string);
                    hashMap.put("uid", C0288p.m335(new WeakReference(context)));
                    hashMap.put("af_gcm_token", str);
                    hashMap.put("launch_counter", Integer.toString(m208(context.getSharedPreferences("appsflyer-data", 0), "appsFlyerCount", false)));
                    hashMap.put("sdk", Integer.toString(VERSION.SDK_INT));
                    weakReference = new WeakReference(context);
                    string3 = AppsFlyerProperties.getInstance().getString(AppsFlyerProperties.CHANNEL);
                    if (string3 == null) {
                        string3 = m224(weakReference, "CHANNEL");
                    }
                    if (string3 != null) {
                        hashMap.put(AppsFlyerProperties.CHANNEL, string3);
                    }
                    try {
                        AsyncTask c0280l = new C0280l(context, isTrackingStopped());
                        c0280l.f277 = hashMap;
                        c0280l.execute(new String[]{new StringBuilder().append(ServerConfigHandler.getUrl(f159)).append(r4).toString()});
                        return;
                    } catch (Throwable th22) {
                        AFLogger.afErrorLog(th22.getMessage(), th22);
                        return;
                    }
                }
                AFLogger.afInfoLog("CustomerUserId not set, Tracking is disabled", true);
            }
        }
        z = false;
        if (z) {
            hashMap = new HashMap();
            string = AppsFlyerProperties.getInstance().getString(AppsFlyerProperties.AF_KEY);
            if (string != null) {
                packageManager = context.getPackageManager();
                PackageInfo packageInfo2 = packageManager.getPackageInfo(context.getPackageName(), 0);
                hashMap.put("app_version_code", Integer.toString(packageInfo2.versionCode));
                hashMap.put("app_version_name", packageInfo2.versionName);
                hashMap.put(NativeProtocol.BRIDGE_ARG_APP_NAME_STRING, packageManager.getApplicationLabel(packageInfo2.applicationInfo).toString());
                long j2 = packageInfo2.firstInstallTime;
                DateFormat simpleDateFormat2 = new SimpleDateFormat("yyyy-MM-dd_HHmmssZ", Locale.US);
                simpleDateFormat2.setTimeZone(TimeZone.getTimeZone("UTC"));
                hashMap.put("installDate", simpleDateFormat2.format(new Date(j2)));
                m253(context, hashMap);
                string2 = AppsFlyerProperties.getInstance().getString(AppsFlyerProperties.APP_USER_ID);
                if (string2 != null) {
                    hashMap.put("appUserId", string2);
                }
                hashMap.put(IdManager.MODEL_FIELD, Build.MODEL);
                hashMap.put("brand", Build.BRAND);
                if (AppsFlyerProperties.getInstance().getBoolean(AppsFlyerProperties.DEVICE_TRACKING_DISABLED, false)) {
                    hashMap.put(AppsFlyerProperties.DEVICE_TRACKING_DISABLED, ServerProtocol.DIALOG_RETURN_SCOPES_TRUE);
                }
                ˊ = C0287o.m333(context.getContentResolver());
                if (ˊ != null) {
                    hashMap.put("amazon_aid", ˊ.m332());
                    hashMap.put("amazon_aid_limit", String.valueOf(ˊ.m331()));
                }
                string2 = AppsFlyerProperties.getInstance().getString(ServerParameters.ADVERTISING_ID_PARAM);
                if (string2 != null) {
                    hashMap.put(ServerParameters.ADVERTISING_ID_PARAM, string2);
                }
                hashMap.put("devkey", string);
                hashMap.put("uid", C0288p.m335(new WeakReference(context)));
                hashMap.put("af_gcm_token", str);
                hashMap.put("launch_counter", Integer.toString(m208(context.getSharedPreferences("appsflyer-data", 0), "appsFlyerCount", false)));
                hashMap.put("sdk", Integer.toString(VERSION.SDK_INT));
                weakReference = new WeakReference(context);
                string3 = AppsFlyerProperties.getInstance().getString(AppsFlyerProperties.CHANNEL);
                if (string3 == null) {
                    string3 = m224(weakReference, "CHANNEL");
                }
                if (string3 != null) {
                    hashMap.put(AppsFlyerProperties.CHANNEL, string3);
                }
                AsyncTask c0280l2 = new C0280l(context, isTrackingStopped());
                c0280l2.f277 = hashMap;
                c0280l2.execute(new String[]{new StringBuilder().append(ServerConfigHandler.getUrl(f159)).append(r4).toString()});
                return;
            }
            AFLogger.afWarnLog("[registerUninstall] AppsFlyer's SDK cannot send any event without providing DevKey.");
            return;
        }
        AFLogger.afInfoLog("CustomerUserId not set, Tracking is disabled", true);
    }

    /* renamed from: ॱ */
    final void m263() {
        this.f168 = System.currentTimeMillis();
    }

    /* renamed from: ˎ */
    final void m260() {
        this.f169 = System.currentTimeMillis();
    }

    /* renamed from: ˋ */
    final void m257(Context context, Intent intent) {
        String stringExtra = intent.getStringExtra(AppsFlyerProperties.IS_MONITOR);
        if (stringExtra != null) {
            AFLogger.afInfoLog("Turning on monitoring.");
            AppsFlyerProperties.getInstance().set(AppsFlyerProperties.IS_MONITOR, stringExtra.equals(ServerProtocol.DIALOG_RETURN_SCOPES_TRUE));
            m227(context, null, "START_TRACKING", context.getPackageName());
            return;
        }
        AFLogger.afInfoLog("****** onReceive called *******");
        AppsFlyerProperties.getInstance().setOnReceiveCalled();
        String stringExtra2 = intent.getStringExtra("referrer");
        AFLogger.afInfoLog("Play store referrer: ".concat(String.valueOf(stringExtra2)));
        if (stringExtra2 != null) {
            if ("AppsFlyer_Test".equals(intent.getStringExtra("TestIntegrationMode"))) {
                Editor edit = context.getSharedPreferences("appsflyer-data", 0).edit();
                edit.clear();
                if (VERSION.SDK_INT >= 9) {
                    edit.apply();
                } else {
                    edit.commit();
                }
                AppsFlyerProperties.getInstance().setFirstLaunchCalled(false);
                AFLogger.afInfoLog("Test mode started..");
                this.f173 = System.currentTimeMillis();
            }
            Editor edit2 = context.getSharedPreferences("appsflyer-data", 0).edit();
            edit2.putString("referrer", stringExtra2);
            if (VERSION.SDK_INT >= 9) {
                edit2.apply();
            } else {
                edit2.commit();
            }
            AppsFlyerProperties.getInstance().setReferrer(stringExtra2);
            if (AppsFlyerProperties.getInstance().isFirstLaunchCalled()) {
                AFLogger.afInfoLog("onReceive: isLaunchCalled");
                if (stringExtra2 != null && stringExtra2.length() > 5) {
                    Object ॱ = AFExecutor.getInstance().m179();
                    m237(ॱ, new C0256b(new WeakReference(context.getApplicationContext()), null, null, null, stringExtra2, ॱ, true, intent), 5, TimeUnit.MILLISECONDS);
                }
            }
        }
    }

    /* renamed from: ˊ */
    private static void m219(JSONObject jSONObject) {
        List arrayList = new ArrayList();
        Iterator keys = jSONObject.keys();
        while (keys.hasNext()) {
            try {
                JSONArray jSONArray = new JSONArray((String) jSONObject.get((String) keys.next()));
                for (int i = 0; i < jSONArray.length(); i++) {
                    arrayList.add(Long.valueOf(jSONArray.getLong(i)));
                }
            } catch (JSONException e) {
            }
        }
        Collections.sort(arrayList);
        Iterator keys2 = jSONObject.keys();
        String str = null;
        while (keys2.hasNext() && str == null) {
            String str2 = (String) keys2.next();
            try {
                JSONArray jSONArray2 = new JSONArray((String) jSONObject.get(str2));
                int i2 = 0;
                while (i2 < jSONArray2.length()) {
                    if (jSONArray2.getLong(i2) == ((Long) arrayList.get(0)).longValue() || jSONArray2.getLong(i2) == ((Long) arrayList.get(1)).longValue() || jSONArray2.getLong(i2) == ((Long) arrayList.get(arrayList.size() - 1)).longValue()) {
                        str = null;
                        break;
                    } else {
                        i2++;
                        str = str2;
                    }
                }
            } catch (JSONException e2) {
                str = str;
            }
        }
        if (str != null) {
            jSONObject.remove(str);
        }
    }

    /* renamed from: ˏ */
    static void m243(Context context, String str) {
        AFLogger.afDebugLog("received a new (extra) referrer: ".concat(String.valueOf(str)));
        try {
            JSONObject jSONObject;
            JSONArray jSONArray;
            long currentTimeMillis = System.currentTimeMillis();
            String string = context.getSharedPreferences("appsflyer-data", 0).getString("extraReferrers", null);
            if (string == null) {
                jSONObject = new JSONObject();
                jSONArray = new JSONArray();
            } else {
                JSONObject jSONObject2 = new JSONObject(string);
                if (jSONObject2.has(str)) {
                    jSONArray = new JSONArray((String) jSONObject2.get(str));
                    jSONObject = jSONObject2;
                } else {
                    jSONArray = new JSONArray();
                    jSONObject = jSONObject2;
                }
            }
            if (((long) jSONArray.length()) < 5) {
                jSONArray.put(currentTimeMillis);
            }
            if (((long) jSONObject.length()) >= 4) {
                m219(jSONObject);
            }
            jSONObject.put(str, jSONArray.toString());
            String jSONObject3 = jSONObject.toString();
            Editor edit = context.getSharedPreferences("appsflyer-data", 0).edit();
            edit.putString("extraReferrers", jSONObject3);
            if (VERSION.SDK_INT >= 9) {
                edit.apply();
            } else {
                edit.commit();
            }
        } catch (JSONException e) {
        } catch (Throwable th) {
            AFLogger.afErrorLog(new StringBuilder("Couldn't save referrer - ").append(str).append(": ").toString(), th);
        }
    }

    private AppsFlyerLib() {
        AFVersionDeclaration.init();
    }

    public static AppsFlyerLib getInstance() {
        return f156;
    }

    public void stopTracking(boolean z, Context context) {
        this.f178 = z;
        CacheManager.getInstance().clearCache(context);
        if (this.f178) {
            String str = IS_STOP_TRACKING_USED;
            Editor edit = context.getSharedPreferences("appsflyer-data", 0).edit();
            edit.putBoolean(str, true);
            if (VERSION.SDK_INT >= 9) {
                edit.apply();
            } else {
                edit.commit();
            }
        }
    }

    public String getSdkVersion() {
        C0300y.m378().m393("getSdkVersion", new String[0]);
        return "version: 4.8.11 (build 383)";
    }

    public void onPause(Context context) {
        C0265d.m287(context);
        C0270f ˏ = C0270f.m295(context);
        ˏ.f239.post(ˏ.f236);
    }

    /* renamed from: ˊ */
    private void m214(Application application) {
        AppsFlyerProperties.getInstance().loadProperties(application.getApplicationContext());
        if (VERSION.SDK_INT < 14) {
            AFLogger.afInfoLog("SDK<14 call trackEvent manually");
            AFLogger.afInfoLog("onBecameForeground");
            getInstance().f168 = System.currentTimeMillis();
            getInstance().m262((Context) application, null, null);
            AFLogger.resetDeltaTime();
        } else if (VERSION.SDK_INT >= 14 && this.f167 == null) {
            C0290q.m339();
            this.f167 = new C02523(this);
            C0290q.m342().m344(application, this.f167);
        }
    }

    @Deprecated
    public void setGCMProjectID(String str) {
        C0300y.m378().m393("setGCMProjectID", str);
        AFLogger.afWarnLog("Method 'setGCMProjectNumber' is deprecated. Please follow the documentation.");
        enableUninstallTracking(str);
    }

    @Deprecated
    public void setGCMProjectNumber(String str) {
        C0300y.m378().m393("setGCMProjectNumber", str);
        AFLogger.afWarnLog("Method 'setGCMProjectNumber' is deprecated. Please follow the documentation.");
        enableUninstallTracking(str);
    }

    @Deprecated
    public void setGCMProjectNumber(Context context, String str) {
        C0300y.m378().m393("setGCMProjectNumber", str);
        AFLogger.afWarnLog("Method 'setGCMProjectNumber' is deprecated. Please use 'enableUninstallTracking'.");
        enableUninstallTracking(str);
    }

    public void enableUninstallTracking(String str) {
        C0300y.m378().m393("enableUninstallTracking", str);
        AppsFlyerProperties.getInstance().set("gcmProjectNumber", str);
    }

    public void updateServerUninstallToken(Context context, String str) {
        if (str != null) {
            C0299u.m375(context, new C0265d(str));
        }
    }

    public void setDebugLog(boolean z) {
        LogLevel logLevel;
        C0300y.m378().m393("setDebugLog", String.valueOf(z));
        AppsFlyerProperties.getInstance().set("shouldLog", z);
        AppsFlyerProperties instance = AppsFlyerProperties.getInstance();
        if (z) {
            logLevel = LogLevel.DEBUG;
        } else {
            logLevel = LogLevel.NONE;
        }
        instance.set("logLevel", logLevel.getLevel());
    }

    public void setImeiData(String str) {
        C0300y.m378().m393("setImeiData", str);
        this.f172 = str;
    }

    public void setAndroidIdData(String str) {
        C0300y.m378().m393("setAndroidIdData", str);
        this.f176 = str;
    }

    public AppsFlyerLib enableLocationCollection(boolean z) {
        this.f171 = z;
        return this;
    }

    @Deprecated
    public void setAppUserId(String str) {
        C0300y.m378().m393("setAppUserId", str);
        setCustomerUserId(str);
    }

    public void setCustomerUserId(String str) {
        C0300y.m378().m393("setCustomerUserId", str);
        AFLogger.afInfoLog("setCustomerUserId = ".concat(String.valueOf(str)));
        AppsFlyerProperties.getInstance().set(AppsFlyerProperties.APP_USER_ID, str);
    }

    public void waitForCustomerUserId(boolean z) {
        AFLogger.afInfoLog("initAfterCustomerUserID: ".concat(String.valueOf(z)), true);
        AppsFlyerProperties.getInstance().set(AppsFlyerProperties.AF_WAITFOR_CUSTOMERID, z);
    }

    public void setCustomerIdAndTrack(String str, @NonNull Context context) {
        boolean z = false;
        if (context != null) {
            if (AppsFlyerProperties.getInstance().getBoolean(AppsFlyerProperties.AF_WAITFOR_CUSTOMERID, false)) {
                if (AppsFlyerProperties.getInstance().getString(AppsFlyerProperties.APP_USER_ID) == null) {
                    z = true;
                }
            }
            if (z) {
                setCustomerUserId(str);
                AFLogger.afInfoLog(new StringBuilder("CustomerUserId set: ").append(str).append(" - Initializing AppsFlyer Tacking").toString(), true);
                String referrer = AppsFlyerProperties.getInstance().getReferrer(context);
                String string = AppsFlyerProperties.getInstance().getString(AppsFlyerProperties.AF_KEY);
                if (referrer == null) {
                    referrer = "";
                }
                m245(context, string, null, null, referrer, context instanceof Activity ? ((Activity) context).getIntent() : null);
                if (AppsFlyerProperties.getInstance().getString("afUninstallToken") != null) {
                    m258(context, AppsFlyerProperties.getInstance().getString("afUninstallToken"));
                    return;
                }
                return;
            }
            setCustomerUserId(str);
            AFLogger.afInfoLog("waitForCustomerUserId is false; setting CustomerUserID: ".concat(String.valueOf(str)), true);
        }
    }

    public void setAppInviteOneLink(String str) {
        C0300y.m378().m393("setAppInviteOneLink", str);
        AFLogger.afInfoLog("setAppInviteOneLink = ".concat(String.valueOf(str)));
        if (str == null || !str.equals(AppsFlyerProperties.getInstance().getString(AppsFlyerProperties.ONELINK_ID))) {
            AppsFlyerProperties.getInstance().remove(AppsFlyerProperties.ONELINK_DOMAIN);
            AppsFlyerProperties.getInstance().remove("onelinkVersion");
            AppsFlyerProperties.getInstance().remove(AppsFlyerProperties.ONELINK_SCHEME);
        }
        AppsFlyerProperties.getInstance().set(AppsFlyerProperties.ONELINK_ID, str);
    }

    public void setAdditionalData(HashMap<String, Object> hashMap) {
        if (hashMap != null) {
            C0300y.m378().m393("setAdditionalData", hashMap.toString());
            AppsFlyerProperties.getInstance().setCustomData(new JSONObject(hashMap).toString());
        }
    }

    public void sendDeepLinkData(Activity activity) {
        if (activity != null && activity.getIntent() != null) {
            C0300y.m378().m393("sendDeepLinkData", activity.getLocalClassName(), new StringBuilder("activity_intent_").append(activity.getIntent().toString()).toString());
        } else if (activity != null) {
            C0300y.m378().m393("sendDeepLinkData", activity.getLocalClassName(), "activity_intent_null");
        } else {
            C0300y.m378().m393("sendDeepLinkData", "activity_null");
        }
        AFLogger.afInfoLog(new StringBuilder("getDeepLinkData with activity ").append(activity.getIntent().getDataString()).toString());
        m214(activity.getApplication());
    }

    public void sendPushNotificationData(Activity activity) {
        String str;
        long currentTimeMillis;
        long j;
        long j2;
        JSONObject jSONObject;
        JSONObject jSONObject2;
        long longValue;
        Throwable th;
        if (activity != null && activity.getIntent() != null) {
            C0300y.m378().m393("sendPushNotificationData", activity.getLocalClassName(), new StringBuilder("activity_intent_").append(activity.getIntent().toString()).toString());
        } else if (activity != null) {
            C0300y.m378().m393("sendPushNotificationData", activity.getLocalClassName(), "activity_intent_null");
        } else {
            C0300y.m378().m393("sendPushNotificationData", "activity_null");
        }
        if (activity instanceof Activity) {
            Intent intent = activity.getIntent();
            if (intent != null) {
                Bundle extras = intent.getExtras();
                if (extras != null) {
                    String string = extras.getString("af");
                    if (string != null) {
                        AFLogger.afInfoLog("Push Notification received af payload = ".concat(String.valueOf(string)));
                        extras.remove("af");
                        activity.setIntent(intent.putExtras(extras));
                    }
                    str = string;
                    this.f177 = str;
                    if (this.f177 != null) {
                        currentTimeMillis = System.currentTimeMillis();
                        if (this.f174 != null) {
                            AFLogger.afInfoLog("pushes: initializing pushes history..");
                            this.f174 = new ConcurrentHashMap();
                            j = currentTimeMillis;
                        } else {
                            try {
                                j2 = AppsFlyerProperties.getInstance().getLong("pushPayloadMaxAging", 1800000);
                                j = currentTimeMillis;
                                for (Long l : this.f174.keySet()) {
                                    try {
                                        jSONObject = new JSONObject(this.f177);
                                        jSONObject2 = new JSONObject((String) this.f174.get(l));
                                        if (jSONObject.get(Constants.URL_MEDIA_SOURCE).equals(jSONObject2.get(Constants.URL_MEDIA_SOURCE))) {
                                            if (currentTimeMillis - l.longValue() > j2) {
                                                this.f174.remove(l);
                                            }
                                            if (l.longValue() > j) {
                                                longValue = l.longValue();
                                            } else {
                                                longValue = j;
                                            }
                                            j = longValue;
                                        } else {
                                            AFLogger.afInfoLog(new StringBuilder("PushNotificationMeasurement: A previous payload with same PID was already acknowledged! (old: ").append(jSONObject2).append(", new: ").append(jSONObject).append(")").toString());
                                            this.f177 = null;
                                            return;
                                        }
                                    } catch (Throwable th2) {
                                        th = th2;
                                    }
                                }
                            } catch (Throwable th3) {
                                th = th3;
                                j = currentTimeMillis;
                                AFLogger.afErrorLog(new StringBuilder("Error while handling push notification measurement: ").append(th.getClass().getSimpleName()).toString(), th);
                                if (this.f174.size() == AppsFlyerProperties.getInstance().getInt("pushPayloadHistorySize", 2)) {
                                    AFLogger.afInfoLog(new StringBuilder("pushes: removing oldest overflowing push (oldest push:").append(j).append(")").toString());
                                    this.f174.remove(Long.valueOf(j));
                                }
                                this.f174.put(Long.valueOf(currentTimeMillis), this.f177);
                                m214(activity.getApplication());
                            }
                        }
                        if (this.f174.size() == AppsFlyerProperties.getInstance().getInt("pushPayloadHistorySize", 2)) {
                            AFLogger.afInfoLog(new StringBuilder("pushes: removing oldest overflowing push (oldest push:").append(j).append(")").toString());
                            this.f174.remove(Long.valueOf(j));
                        }
                        this.f174.put(Long.valueOf(currentTimeMillis), this.f177);
                        m214(activity.getApplication());
                    }
                }
            }
        }
        str = null;
        this.f177 = str;
        if (this.f177 != null) {
            currentTimeMillis = System.currentTimeMillis();
            if (this.f174 != null) {
                j2 = AppsFlyerProperties.getInstance().getLong("pushPayloadMaxAging", 1800000);
                j = currentTimeMillis;
                for (Long l2 : this.f174.keySet()) {
                    jSONObject = new JSONObject(this.f177);
                    jSONObject2 = new JSONObject((String) this.f174.get(l2));
                    if (jSONObject.get(Constants.URL_MEDIA_SOURCE).equals(jSONObject2.get(Constants.URL_MEDIA_SOURCE))) {
                        if (currentTimeMillis - l2.longValue() > j2) {
                            this.f174.remove(l2);
                        }
                        if (l2.longValue() > j) {
                            longValue = j;
                        } else {
                            longValue = l2.longValue();
                        }
                        j = longValue;
                    } else {
                        AFLogger.afInfoLog(new StringBuilder("PushNotificationMeasurement: A previous payload with same PID was already acknowledged! (old: ").append(jSONObject2).append(", new: ").append(jSONObject).append(")").toString());
                        this.f177 = null;
                        return;
                    }
                }
            }
            AFLogger.afInfoLog("pushes: initializing pushes history..");
            this.f174 = new ConcurrentHashMap();
            j = currentTimeMillis;
            if (this.f174.size() == AppsFlyerProperties.getInstance().getInt("pushPayloadHistorySize", 2)) {
                AFLogger.afInfoLog(new StringBuilder("pushes: removing oldest overflowing push (oldest push:").append(j).append(")").toString());
                this.f174.remove(Long.valueOf(j));
            }
            this.f174.put(Long.valueOf(currentTimeMillis), this.f177);
            m214(activity.getApplication());
        }
    }

    @Deprecated
    public void setUserEmail(String str) {
        C0300y.m378().m393("setUserEmail", str);
        AppsFlyerProperties.getInstance().set(AppsFlyerProperties.USER_EMAIL, str);
    }

    public void setUserEmails(String... strArr) {
        C0300y.m378().m393("setUserEmails", strArr);
        setUserEmails(EmailsCryptType.NONE, strArr);
    }

    public void setUserEmails(EmailsCryptType emailsCryptType, String... strArr) {
        List arrayList = new ArrayList(strArr.length + 1);
        arrayList.add(emailsCryptType.toString());
        arrayList.addAll(Arrays.asList(strArr));
        C0300y.m378().m393("setUserEmails", (String[]) arrayList.toArray(new String[(strArr.length + 1)]));
        AppsFlyerProperties.getInstance().set(AppsFlyerProperties.EMAIL_CRYPT_TYPE, emailsCryptType.getValue());
        Map hashMap = new HashMap();
        AbstractCollection arrayList2 = new ArrayList();
        Object obj = null;
        for (String str : strArr) {
            switch (C02534.f129[emailsCryptType.ordinal()]) {
                case 2:
                    obj = "md5_el_arr";
                    arrayList2.add(C0291r.m347(str));
                    break;
                case 3:
                    obj = "sha256_el_arr";
                    arrayList2.add(C0291r.m345(str));
                    break;
                case 4:
                    obj = "plain_el_arr";
                    arrayList2.add(str);
                    break;
                default:
                    obj = "sha1_el_arr";
                    arrayList2.add(C0291r.m348(str));
                    break;
            }
        }
        hashMap.put(obj, arrayList2);
        AppsFlyerProperties.getInstance().setUserEmails(new JSONObject(hashMap).toString());
    }

    public void setCollectAndroidID(boolean z) {
        C0300y.m378().m393("setCollectAndroidID", String.valueOf(z));
        AppsFlyerProperties.getInstance().set(AppsFlyerProperties.COLLECT_ANDROID_ID, Boolean.toString(z));
    }

    public void setCollectIMEI(boolean z) {
        C0300y.m378().m393("setCollectIMEI", String.valueOf(z));
        AppsFlyerProperties.getInstance().set(AppsFlyerProperties.COLLECT_IMEI, Boolean.toString(z));
    }

    @Deprecated
    public void setCollectFingerPrint(boolean z) {
        C0300y.m378().m393("setCollectFingerPrint", String.valueOf(z));
        AppsFlyerProperties.getInstance().set(AppsFlyerProperties.COLLECT_FINGER_PRINT, Boolean.toString(z));
    }

    public AppsFlyerLib init(String str, AppsFlyerConversionListener appsFlyerConversionListener) {
        C0300y ˋ = C0300y.m378();
        String str2 = "init";
        String[] strArr = new String[2];
        strArr[0] = str;
        strArr[1] = appsFlyerConversionListener == null ? "null" : "conversionDataListener";
        ˋ.m393(str2, strArr);
        AFLogger.m190(String.format("Initializing AppsFlyer SDK: (v%s.%s)", new Object[]{BuildConfig.AF_SDK_VERSION, "383"}));
        this.f182 = true;
        AppsFlyerProperties.getInstance().set(AppsFlyerProperties.AF_KEY, str);
        C02695.m292(str);
        f161 = appsFlyerConversionListener;
        return this;
    }

    public AppsFlyerLib init(String str, AppsFlyerConversionListener appsFlyerConversionListener, Context context) {
        if (context != null && m255(context)) {
            if (this.f186 == null) {
                this.f186 = new C0266e();
                this.f186.m291(context, this);
            } else {
                AFLogger.afWarnLog("AFInstallReferrer instance already created");
            }
        }
        return init(str, appsFlyerConversionListener);
    }

    public void startTracking(Application application) {
        if (this.f182) {
            startTracking(application, null);
        } else {
            AFLogger.afWarnLog("ERROR: AppsFlyer SDK is not initialized! The API call 'startTracking(Application)' must be called after the 'init(String, AppsFlyerConversionListener)' API method, which should be called on the Application's onCreate.");
        }
    }

    public void startTracking(Application application, String str) {
        C0300y.m378().m393("startTracking", str);
        AFLogger.afInfoLog(String.format("Starting AppsFlyer Tracking: (v%s.%s)", new Object[]{BuildConfig.AF_SDK_VERSION, "383"}));
        AFLogger.afInfoLog("Build Number: 383");
        AppsFlyerProperties.getInstance().loadProperties(application.getApplicationContext());
        if (TextUtils.isEmpty(str)) {
            if (TextUtils.isEmpty(AppsFlyerProperties.getInstance().getString(AppsFlyerProperties.AF_KEY))) {
                AFLogger.afWarnLog("ERROR: AppsFlyer SDK is not initialized! You must provide AppsFlyer Dev-Key either in the 'init' API method (should be called on Application's onCreate),or in the startTracking API method (should be called on Activity's onCreate).");
                return;
            }
        }
        AppsFlyerProperties.getInstance().set(AppsFlyerProperties.AF_KEY, str);
        C02695.m292(str);
        m214(application);
    }

    public void setAppId(String str) {
        C0300y.m378().m393("setAppId", str);
        AppsFlyerProperties.getInstance().set(AppsFlyerProperties.APP_ID, str);
    }

    public void setExtension(String str) {
        C0300y.m378().m393("setExtension", str);
        AppsFlyerProperties.getInstance().set(AppsFlyerProperties.EXTENSION, str);
    }

    public void setIsUpdate(boolean z) {
        C0300y.m378().m393("setIsUpdate", String.valueOf(z));
        AppsFlyerProperties.getInstance().set(AppsFlyerProperties.IS_UPDATE, z);
    }

    public void setCurrencyCode(String str) {
        C0300y.m378().m393("setCurrencyCode", str);
        AppsFlyerProperties.getInstance().set(AppsFlyerProperties.CURRENCY_CODE, str);
    }

    public void trackLocation(Context context, double d, double d2) {
        C0300y.m378().m393("trackLocation", String.valueOf(d), String.valueOf(d2));
        Map hashMap = new HashMap();
        hashMap.put(AFInAppEventParameterName.LONGTITUDE, Double.toString(d2));
        hashMap.put(AFInAppEventParameterName.LATITUDE, Double.toString(d));
        m262(context, AFInAppEventType.LOCATION_COORDINATES, hashMap);
    }

    /* renamed from: ˎ */
    final void m261(WeakReference<Context> weakReference) {
        if (weakReference.get() != null) {
            AFLogger.afInfoLog("app went to background");
            SharedPreferences sharedPreferences = ((Context) weakReference.get()).getSharedPreferences("appsflyer-data", 0);
            AppsFlyerProperties.getInstance().saveProperties(sharedPreferences);
            long j = this.f169 - this.f168;
            Map hashMap = new HashMap();
            String string = AppsFlyerProperties.getInstance().getString(AppsFlyerProperties.AF_KEY);
            if (string == null) {
                AFLogger.afWarnLog("[callStats] AppsFlyer's SDK cannot send any event without providing DevKey.");
                return;
            }
            String string2 = AppsFlyerProperties.getInstance().getString("KSAppsFlyerId");
            if (AppsFlyerProperties.getInstance().getBoolean(AppsFlyerProperties.DEVICE_TRACKING_DISABLED, false)) {
                hashMap.put(AppsFlyerProperties.DEVICE_TRACKING_DISABLED, ServerProtocol.DIALOG_RETURN_SCOPES_TRUE);
            }
            C0286n ˊ = C0287o.m333(((Context) weakReference.get()).getContentResolver());
            if (ˊ != null) {
                hashMap.put("amazon_aid", ˊ.m332());
                hashMap.put("amazon_aid_limit", String.valueOf(ˊ.m331()));
            }
            String string3 = AppsFlyerProperties.getInstance().getString(ServerParameters.ADVERTISING_ID_PARAM);
            if (string3 != null) {
                hashMap.put(ServerParameters.ADVERTISING_ID_PARAM, string3);
            }
            hashMap.put("app_id", ((Context) weakReference.get()).getPackageName());
            hashMap.put("devkey", string);
            hashMap.put("uid", C0288p.m335(weakReference));
            hashMap.put("time_in_app", String.valueOf(j / 1000));
            hashMap.put("statType", "user_closed_app");
            hashMap.put("platform", "Android");
            hashMap.put("launch_counter", Integer.toString(m208(sharedPreferences, "appsFlyerCount", false)));
            hashMap.put("gcd_conversion_data_timing", Long.toString(sharedPreferences.getLong("appsflyerGetConversionDataTiming", 0)));
            String str = AppsFlyerProperties.CHANNEL;
            Object string4 = AppsFlyerProperties.getInstance().getString(AppsFlyerProperties.CHANNEL);
            if (string4 == null) {
                string4 = m224((WeakReference) weakReference, "CHANNEL");
            }
            hashMap.put(str, string4);
            hashMap.put("originalAppsflyerId", string2 != null ? string2 : "");
            if (this.f179) {
                try {
                    AsyncTask c0280l = new C0280l(null, isTrackingStopped());
                    c0280l.f277 = hashMap;
                    if (Thread.currentThread() == Looper.getMainLooper().getThread()) {
                        AFLogger.afDebugLog("Main thread detected. Running callStats task in a new thread.");
                        c0280l.execute(new String[]{ServerConfigHandler.getUrl("https://stats.%s/stats")});
                        return;
                    }
                    AFLogger.afDebugLog(new StringBuilder("Running callStats task (on current thread: ").append(Thread.currentThread().toString()).append(" )").toString());
                    c0280l.onPreExecute();
                    c0280l.m324(c0280l.m321(ServerConfigHandler.getUrl("https://stats.%s/stats")));
                    return;
                } catch (Throwable th) {
                    AFLogger.afErrorLog("Could not send callStats request", th);
                    return;
                }
            }
            AFLogger.afDebugLog("Stats call is disabled, ignore ...");
        }
    }

    public void trackAppLaunch(Context context, String str) {
        if (m255(context)) {
            if (this.f186 == null) {
                this.f186 = new C0266e();
                this.f186.m291(context, this);
            } else {
                AFLogger.afWarnLog("AFInstallReferrer instance already created");
            }
        }
        m245(context, str, null, null, "", null);
    }

    protected void setDeepLinkData(Intent intent) {
        if (intent != null) {
            try {
                if ("android.intent.action.VIEW".equals(intent.getAction())) {
                    this.f170 = intent.getData();
                    AFLogger.afDebugLog(new StringBuilder("Unity setDeepLinkData = ").append(this.f170).toString());
                }
            } catch (Throwable th) {
                AFLogger.afErrorLog("Exception while setting deeplink data (unity). ", th);
            }
        }
    }

    public void reportTrackSession(Context context) {
        C0300y.m378().m393("reportTrackSession", new String[0]);
        C0300y.m378().m396();
        m262(context, null, null);
    }

    public void trackEvent(Context context, String str, Map<String, Object> map) {
        Map hashMap;
        if (map == null) {
            hashMap = new HashMap();
        } else {
            Map<String, Object> map2 = map;
        }
        JSONObject jSONObject = new JSONObject(hashMap);
        C0300y.m378().m393("trackEvent", str, jSONObject.toString());
        m262(context, str, (Map) map);
    }

    /* renamed from: ˏ */
    final void m262(Context context, String str, Map<String, Object> map) {
        Intent intent = context instanceof Activity ? ((Activity) context).getIntent() : null;
        if (AppsFlyerProperties.getInstance().getString(AppsFlyerProperties.AF_KEY) == null) {
            AFLogger.afWarnLog("[TrackEvent/Launch] AppsFlyer's SDK cannot send any event without providing DevKey.");
            return;
        }
        if (map == null) {
            map = new HashMap();
        }
        JSONObject jSONObject = new JSONObject(map);
        String referrer = AppsFlyerProperties.getInstance().getReferrer(context);
        String jSONObject2 = jSONObject.toString();
        if (referrer == null) {
            referrer = "";
        }
        m245(context, null, str, jSONObject2, referrer, intent);
    }

    /* renamed from: ˋ */
    private static void m227(Context context, String str, String str2, String str3) {
        if (AppsFlyerProperties.getInstance().getBoolean(AppsFlyerProperties.IS_MONITOR, false)) {
            Intent intent = new Intent("com.appsflyer.MonitorBroadcast");
            intent.setPackage("com.appsflyer.nightvision");
            intent.putExtra("message", str2);
            intent.putExtra(Param.VALUE, str3);
            intent.putExtra("packageName", ServerProtocol.DIALOG_RETURN_SCOPES_TRUE);
            intent.putExtra(Constants.URL_MEDIA_SOURCE, new Integer(Process.myPid()));
            intent.putExtra("eventIdentifier", str);
            intent.putExtra("sdk", BuildConfig.AF_SDK_VERSION);
            context.sendBroadcast(intent);
        }
    }

    public void setDeviceTrackingDisabled(boolean z) {
        C0300y.m378().m393("setDeviceTrackingDisabled", String.valueOf(z));
        AppsFlyerProperties.getInstance().set(AppsFlyerProperties.DEVICE_TRACKING_DISABLED, z);
    }

    /* renamed from: ˊ */
    private static Map<String, String> m212(Context context) throws C0279k {
        String string = context.getSharedPreferences("appsflyer-data", 0).getString("attributionId", null);
        if (string != null && string.length() > 0) {
            return m250(string);
        }
        throw new C0279k();
    }

    public void registerConversionListener(Context context, AppsFlyerConversionListener appsFlyerConversionListener) {
        C0300y.m378().m393("registerConversionListener", new String[0]);
        if (appsFlyerConversionListener != null) {
            f161 = appsFlyerConversionListener;
        }
    }

    public void unregisterConversionListener() {
        C0300y.m378().m393("unregisterConversionListener", new String[0]);
        f161 = null;
    }

    public void registerValidatorListener(Context context, AppsFlyerInAppPurchaseValidatorListener appsFlyerInAppPurchaseValidatorListener) {
        C0300y.m378().m393("registerValidatorListener", new String[0]);
        AFLogger.afDebugLog("registerValidatorListener called");
        if (appsFlyerInAppPurchaseValidatorListener == null) {
            AFLogger.afDebugLog("registerValidatorListener null listener");
        } else {
            f160 = appsFlyerInAppPurchaseValidatorListener;
        }
    }

    protected void getConversionData(Context context, final ConversionDataListener conversionDataListener) {
        f161 = new AppsFlyerConversionListener(this) {
            /* renamed from: ˏ */
            private /* synthetic */ AppsFlyerLib f127;

            public final void onInstallConversionDataLoaded(Map<String, String> map) {
                conversionDataListener.onConversionDataLoaded(map);
            }

            public final void onInstallConversionFailure(String str) {
                conversionDataListener.onConversionFailure(str);
            }

            public final void onAppOpenAttribution(Map<String, String> map) {
            }

            public final void onAttributionFailure(String str) {
            }
        };
    }

    /* renamed from: ˎ */
    private static Map<String, String> m233(Context context, String str) {
        Map<String, String> linkedHashMap = new LinkedHashMap();
        String[] split = str.split("&");
        int length = split.length;
        int i = 0;
        int i2 = 0;
        while (i < length) {
            Object substring;
            String str2 = split[i];
            int indexOf = str2.indexOf("=");
            if (indexOf > 0) {
                substring = str2.substring(0, indexOf);
            } else {
                String str3 = str2;
            }
            if (!linkedHashMap.containsKey(substring)) {
                if (substring.equals(Constants.URL_CAMPAIGN)) {
                    substring = Param.CAMPAIGN;
                } else if (substring.equals(Constants.URL_MEDIA_SOURCE)) {
                    substring = "media_source";
                } else if (substring.equals("af_prt")) {
                    i2 = 1;
                    substring = "agency";
                }
                linkedHashMap.put(substring, "");
            }
            int i3 = i2;
            Object obj = substring;
            substring = (indexOf <= 0 || str2.length() <= indexOf + 1) ? null : str2.substring(indexOf + 1);
            linkedHashMap.put(obj, substring);
            i++;
            i2 = i3;
        }
        try {
            if (!linkedHashMap.containsKey("install_time")) {
                PackageInfo packageInfo = context.getPackageManager().getPackageInfo(context.getPackageName(), 0);
                DateFormat simpleDateFormat = new SimpleDateFormat("yyyy-MM-dd HH:mm:ss", Locale.US);
                long j = packageInfo.firstInstallTime;
                simpleDateFormat.setTimeZone(TimeZone.getTimeZone("UTC"));
                linkedHashMap.put("install_time", simpleDateFormat.format(new Date(j)));
            }
        } catch (Throwable e) {
            AFLogger.afErrorLog("Could not fetch install time. ", e);
        }
        if (!linkedHashMap.containsKey("af_status")) {
            linkedHashMap.put("af_status", "Non-organic");
        }
        if (i2 != 0) {
            linkedHashMap.remove("media_source");
        }
        return linkedHashMap;
    }

    /* renamed from: ॱ */
    private static Map<String, String> m250(String str) {
        Map<String, String> hashMap = new HashMap();
        try {
            JSONObject jSONObject = new JSONObject(str);
            Iterator keys = jSONObject.keys();
            while (keys.hasNext()) {
                String str2 = (String) keys.next();
                if (!f157.contains(str2)) {
                    CharSequence string = jSONObject.getString(str2);
                    if (!(TextUtils.isEmpty(string) || "null".equals(string))) {
                        hashMap.put(str2, string);
                    }
                }
            }
            return hashMap;
        } catch (Throwable e) {
            AFLogger.afErrorLog(e.getMessage(), e);
            return null;
        }
    }

    /* renamed from: ˏ */
    private void m245(Context context, String str, String str2, String str3, String str4, Intent intent) {
        Object obj;
        Context applicationContext = context.getApplicationContext();
        Object obj2 = str2 == null ? 1 : null;
        if (AppsFlyerProperties.getInstance().getBoolean(AppsFlyerProperties.AF_WAITFOR_CUSTOMERID, false)) {
            if (AppsFlyerProperties.getInstance().getString(AppsFlyerProperties.APP_USER_ID) == null) {
                obj = 1;
                if (obj == null) {
                    AFLogger.afInfoLog("CustomerUserId not set, Tracking is disabled", true);
                }
                if (obj2 != null) {
                    if (AppsFlyerProperties.getInstance().getBoolean(AppsFlyerProperties.LAUNCH_PROTECT_ENABLED, true)) {
                        AFLogger.afInfoLog("Allowing multiple launches within a 5 second time window.");
                    } else if (m228()) {
                        return;
                    }
                    this.f165 = System.currentTimeMillis();
                }
                Object ॱ = AFExecutor.getInstance().m179();
                m237(ॱ, new C0256b(new WeakReference(applicationContext), str, str2, str3, str4, ॱ, false, intent), 150, TimeUnit.MILLISECONDS);
                return;
            }
        }
        obj = null;
        if (obj == null) {
            if (obj2 != null) {
                if (AppsFlyerProperties.getInstance().getBoolean(AppsFlyerProperties.LAUNCH_PROTECT_ENABLED, true)) {
                    AFLogger.afInfoLog("Allowing multiple launches within a 5 second time window.");
                } else if (m228()) {
                    return;
                }
                this.f165 = System.currentTimeMillis();
            }
            Object ॱ2 = AFExecutor.getInstance().m179();
            m237(ॱ2, new C0256b(new WeakReference(applicationContext), str, str2, str3, str4, ॱ2, false, intent), 150, TimeUnit.MILLISECONDS);
            return;
        }
        AFLogger.afInfoLog("CustomerUserId not set, Tracking is disabled", true);
    }

    /* renamed from: ˋ */
    private boolean m228() {
        if (this.f165 > 0) {
            long currentTimeMillis = System.currentTimeMillis() - this.f165;
            DateFormat simpleDateFormat = new SimpleDateFormat("yyyy/MM/dd HH:mm:ss.SSS Z", Locale.US);
            long j = this.f165;
            simpleDateFormat.setTimeZone(TimeZone.getTimeZone("UTC"));
            String format = simpleDateFormat.format(new Date(j));
            j = this.f183;
            simpleDateFormat.setTimeZone(TimeZone.getTimeZone("UTC"));
            String format2 = simpleDateFormat.format(new Date(j));
            if (currentTimeMillis < this.f184 && !isTrackingStopped()) {
                AFLogger.afInfoLog(String.format(Locale.US, "Last Launch attempt: %s;\nLast successful Launch event: %s;\nThis launch is blocked: %s ms < %s ms", new Object[]{format, format2, Long.valueOf(currentTimeMillis), Long.valueOf(this.f184)}));
                return true;
            } else if (!isTrackingStopped()) {
                AFLogger.afInfoLog(String.format(Locale.US, "Last Launch attempt: %s;\nLast successful Launch event: %s;\nSending launch (+%s ms)", new Object[]{format, format2, Long.valueOf(currentTimeMillis)}));
            }
        } else if (!isTrackingStopped()) {
            AFLogger.afInfoLog("Sending first launch for this session!");
        }
        return false;
    }

    /* JADX WARNING: inconsistent code. */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    /* renamed from: ˎ */
    final java.util.Map<java.lang.String, java.lang.Object> m259(android.content.Context r13, java.lang.String r14, java.lang.String r15, java.lang.String r16, java.lang.String r17, boolean r18, android.content.SharedPreferences r19, boolean r20, android.content.Intent r21) {
        /*
        r12 = this;
        r5 = new java.util.HashMap;
        r5.<init>();
        com.appsflyer.C0287o.m334(r13, r5);
        r2 = new java.util.Date;
        r2.<init>();
        r2 = r2.getTime();
        r4 = "af_timestamp";
        r6 = java.lang.Long.toString(r2);
        r5.put(r4, r6);
        r2 = com.appsflyer.C0261a.m277(r13, r2);
        if (r2 == 0) goto L_0x0025;
    L_0x0020:
        r3 = "cksm_v1";
        r5.put(r3, r2);
    L_0x0025:
        r2 = r12.isTrackingStopped();	 Catch:{ Throwable -> 0x0913 }
        if (r2 != 0) goto L_0x090c;
    L_0x002b:
        r3 = new java.lang.StringBuilder;	 Catch:{ Throwable -> 0x0913 }
        r2 = "******* sendTrackingWithEvent: ";
        r3.<init>(r2);	 Catch:{ Throwable -> 0x0913 }
        if (r20 == 0) goto L_0x0909;
    L_0x0034:
        r2 = "Launch";
    L_0x0036:
        r2 = r3.append(r2);	 Catch:{ Throwable -> 0x0913 }
        r2 = r2.toString();	 Catch:{ Throwable -> 0x0913 }
        com.appsflyer.AFLogger.afInfoLog(r2);	 Catch:{ Throwable -> 0x0913 }
    L_0x0041:
        r3 = "AppsFlyer_4.8.11";
        r4 = "EVENT_CREATED_WITH_NAME";
        if (r20 == 0) goto L_0x091c;
    L_0x0047:
        r2 = "Launch";
    L_0x0049:
        m227(r13, r3, r4, r2);	 Catch:{ Throwable -> 0x0913 }
        r2 = com.appsflyer.cache.CacheManager.getInstance();	 Catch:{ Throwable -> 0x0913 }
        r2.init(r13);	 Catch:{ Throwable -> 0x0913 }
        r2 = r13.getPackageManager();	 Catch:{ Exception -> 0x091f }
        r3 = r13.getPackageName();	 Catch:{ Exception -> 0x091f }
        r4 = 4096; // 0x1000 float:5.74E-42 double:2.0237E-320;
        r2 = r2.getPackageInfo(r3, r4);	 Catch:{ Exception -> 0x091f }
        r2 = r2.requestedPermissions;	 Catch:{ Exception -> 0x091f }
        r2 = java.util.Arrays.asList(r2);	 Catch:{ Exception -> 0x091f }
        r3 = "android.permission.INTERNET";
        r3 = r2.contains(r3);	 Catch:{ Exception -> 0x091f }
        if (r3 != 0) goto L_0x007b;
    L_0x006f:
        r3 = "Permission android.permission.INTERNET is missing in the AndroidManifest.xml";
        com.appsflyer.AFLogger.afWarnLog(r3);	 Catch:{ Exception -> 0x091f }
        r3 = 0;
        r4 = "PERMISSION_INTERNET_MISSING";
        r6 = 0;
        m227(r13, r3, r4, r6);	 Catch:{ Exception -> 0x091f }
    L_0x007b:
        r3 = "android.permission.ACCESS_NETWORK_STATE";
        r3 = r2.contains(r3);	 Catch:{ Exception -> 0x091f }
        if (r3 != 0) goto L_0x0088;
    L_0x0083:
        r3 = "Permission android.permission.ACCESS_NETWORK_STATE is missing in the AndroidManifest.xml";
        com.appsflyer.AFLogger.afWarnLog(r3);	 Catch:{ Exception -> 0x091f }
    L_0x0088:
        r3 = "android.permission.ACCESS_WIFI_STATE";
        r2 = r2.contains(r3);	 Catch:{ Exception -> 0x091f }
        if (r2 != 0) goto L_0x0095;
    L_0x0090:
        r2 = "Permission android.permission.ACCESS_WIFI_STATE is missing in the AndroidManifest.xml";
        com.appsflyer.AFLogger.afWarnLog(r2);	 Catch:{ Exception -> 0x091f }
    L_0x0095:
        if (r18 == 0) goto L_0x009e;
    L_0x0097:
        r2 = "af_events_api";
        r3 = "1";
        r5.put(r2, r3);	 Catch:{ Throwable -> 0x0913 }
    L_0x009e:
        r2 = "brand";
        r3 = android.os.Build.BRAND;	 Catch:{ Throwable -> 0x0913 }
        r5.put(r2, r3);	 Catch:{ Throwable -> 0x0913 }
        r2 = "device";
        r3 = android.os.Build.DEVICE;	 Catch:{ Throwable -> 0x0913 }
        r5.put(r2, r3);	 Catch:{ Throwable -> 0x0913 }
        r2 = "product";
        r3 = android.os.Build.PRODUCT;	 Catch:{ Throwable -> 0x0913 }
        r5.put(r2, r3);	 Catch:{ Throwable -> 0x0913 }
        r2 = "sdk";
        r3 = android.os.Build.VERSION.SDK_INT;	 Catch:{ Throwable -> 0x0913 }
        r3 = java.lang.Integer.toString(r3);	 Catch:{ Throwable -> 0x0913 }
        r5.put(r2, r3);	 Catch:{ Throwable -> 0x0913 }
        r2 = "model";
        r3 = android.os.Build.MODEL;	 Catch:{ Throwable -> 0x0913 }
        r5.put(r2, r3);	 Catch:{ Throwable -> 0x0913 }
        r2 = "deviceType";
        r3 = android.os.Build.TYPE;	 Catch:{ Throwable -> 0x0913 }
        r5.put(r2, r3);	 Catch:{ Throwable -> 0x0913 }
        if (r20 == 0) goto L_0x098e;
    L_0x00ce:
        r2 = "appsflyer-data";
        r3 = 0;
        r2 = r13.getSharedPreferences(r2, r3);	 Catch:{ Throwable -> 0x0913 }
        r3 = "appsFlyerCount";
        r2 = r2.contains(r3);	 Catch:{ Throwable -> 0x0913 }
        if (r2 != 0) goto L_0x0927;
    L_0x00dd:
        r2 = 1;
    L_0x00de:
        if (r2 == 0) goto L_0x0204;
    L_0x00e0:
        r2 = com.appsflyer.AppsFlyerProperties.getInstance();	 Catch:{ Throwable -> 0x0913 }
        r2 = r2.isOtherSdkStringDisabled();	 Catch:{ Throwable -> 0x0913 }
        if (r2 != 0) goto L_0x019b;
    L_0x00ea:
        r3 = "af_sdks";
        r4 = new java.lang.StringBuilder;	 Catch:{ Throwable -> 0x0913 }
        r4.<init>();	 Catch:{ Throwable -> 0x0913 }
        r2 = "com.tune.Tune";
        r6 = r12.f180;	 Catch:{ Throwable -> 0x0913 }
        r2 = r6.m368(r2);	 Catch:{ Throwable -> 0x0913 }
        if (r2 == 0) goto L_0x092a;
    L_0x00fb:
        r2 = 1;
    L_0x00fc:
        r4 = r4.append(r2);	 Catch:{ Throwable -> 0x0913 }
        r2 = "com.adjust.sdk.Adjust";
        r6 = r12.f180;	 Catch:{ Throwable -> 0x0913 }
        r2 = r6.m368(r2);	 Catch:{ Throwable -> 0x0913 }
        if (r2 == 0) goto L_0x092d;
    L_0x010a:
        r2 = 1;
    L_0x010b:
        r4 = r4.append(r2);	 Catch:{ Throwable -> 0x0913 }
        r2 = "com.kochava.android.tracker.Feature";
        r6 = r12.f180;	 Catch:{ Throwable -> 0x0913 }
        r2 = r6.m368(r2);	 Catch:{ Throwable -> 0x0913 }
        if (r2 == 0) goto L_0x0930;
    L_0x0119:
        r2 = 1;
    L_0x011a:
        r4 = r4.append(r2);	 Catch:{ Throwable -> 0x0913 }
        r2 = "io.branch.referral.Branch";
        r6 = r12.f180;	 Catch:{ Throwable -> 0x0913 }
        r2 = r6.m368(r2);	 Catch:{ Throwable -> 0x0913 }
        if (r2 == 0) goto L_0x0933;
    L_0x0128:
        r2 = 1;
    L_0x0129:
        r4 = r4.append(r2);	 Catch:{ Throwable -> 0x0913 }
        r2 = "com.apsalar.sdk.Apsalar";
        r6 = r12.f180;	 Catch:{ Throwable -> 0x0913 }
        r2 = r6.m368(r2);	 Catch:{ Throwable -> 0x0913 }
        if (r2 == 0) goto L_0x0936;
    L_0x0137:
        r2 = 1;
    L_0x0138:
        r4 = r4.append(r2);	 Catch:{ Throwable -> 0x0913 }
        r2 = "com.localytics.android.Localytics";
        r6 = r12.f180;	 Catch:{ Throwable -> 0x0913 }
        r2 = r6.m368(r2);	 Catch:{ Throwable -> 0x0913 }
        if (r2 == 0) goto L_0x0939;
    L_0x0146:
        r2 = 1;
    L_0x0147:
        r4 = r4.append(r2);	 Catch:{ Throwable -> 0x0913 }
        r2 = "com.tenjin.android.TenjinSDK";
        r6 = r12.f180;	 Catch:{ Throwable -> 0x0913 }
        r2 = r6.m368(r2);	 Catch:{ Throwable -> 0x0913 }
        if (r2 == 0) goto L_0x093c;
    L_0x0155:
        r2 = 1;
    L_0x0156:
        r4 = r4.append(r2);	 Catch:{ Throwable -> 0x0913 }
        r2 = "place holder for TD";
        r6 = r12.f180;	 Catch:{ Throwable -> 0x0913 }
        r2 = r6.m368(r2);	 Catch:{ Throwable -> 0x0913 }
        if (r2 == 0) goto L_0x093f;
    L_0x0164:
        r2 = 1;
    L_0x0165:
        r4 = r4.append(r2);	 Catch:{ Throwable -> 0x0913 }
        r2 = "it.partytrack.sdk.Track";
        r6 = r12.f180;	 Catch:{ Throwable -> 0x0913 }
        r2 = r6.m368(r2);	 Catch:{ Throwable -> 0x0913 }
        if (r2 == 0) goto L_0x0942;
    L_0x0173:
        r2 = 1;
    L_0x0174:
        r4 = r4.append(r2);	 Catch:{ Throwable -> 0x0913 }
        r2 = "jp.appAdForce.android.LtvManager";
        r6 = r12.f180;	 Catch:{ Throwable -> 0x0913 }
        r2 = r6.m368(r2);	 Catch:{ Throwable -> 0x0913 }
        if (r2 == 0) goto L_0x0945;
    L_0x0182:
        r2 = 1;
    L_0x0183:
        r2 = r4.append(r2);	 Catch:{ Throwable -> 0x0913 }
        r2 = r2.toString();	 Catch:{ Throwable -> 0x0913 }
        r5.put(r3, r2);	 Catch:{ Throwable -> 0x0913 }
        r2 = m256(r13);	 Catch:{ Throwable -> 0x0913 }
        r3 = "batteryLevel";
        r2 = java.lang.String.valueOf(r2);	 Catch:{ Throwable -> 0x0913 }
        r5.put(r3, r2);	 Catch:{ Throwable -> 0x0913 }
    L_0x019b:
        r2 = 18;
        r3 = "OPPO";
        r4 = android.os.Build.BRAND;	 Catch:{ Throwable -> 0x0913 }
        r3 = r3.equals(r4);	 Catch:{ Throwable -> 0x0913 }
        if (r3 == 0) goto L_0x0948;
    L_0x01a7:
        r3 = 1;
    L_0x01a8:
        if (r3 == 0) goto L_0x01b1;
    L_0x01aa:
        r2 = 23;
        r3 = "OPPO device found";
        com.appsflyer.AFLogger.afRDLog(r3);	 Catch:{ Throwable -> 0x0913 }
    L_0x01b1:
        r3 = android.os.Build.VERSION.SDK_INT;	 Catch:{ Throwable -> 0x0913 }
        if (r3 < r2) goto L_0x096e;
    L_0x01b5:
        r2 = new java.lang.StringBuilder;	 Catch:{ Throwable -> 0x0913 }
        r3 = "OS SDK is=";
        r2.<init>(r3);	 Catch:{ Throwable -> 0x0913 }
        r3 = android.os.Build.VERSION.SDK_INT;	 Catch:{ Throwable -> 0x0913 }
        r2 = r2.append(r3);	 Catch:{ Throwable -> 0x0913 }
        r3 = "; use KeyStore";
        r2 = r2.append(r3);	 Catch:{ Throwable -> 0x0913 }
        r2 = r2.toString();	 Catch:{ Throwable -> 0x0913 }
        com.appsflyer.AFLogger.afRDLog(r2);	 Catch:{ Throwable -> 0x0913 }
        r2 = new com.appsflyer.AFKeystoreWrapper;	 Catch:{ Throwable -> 0x0913 }
        r2.<init>(r13);	 Catch:{ Throwable -> 0x0913 }
        r3 = r2.m184();	 Catch:{ Throwable -> 0x0913 }
        if (r3 != 0) goto L_0x094b;
    L_0x01da:
        r3 = new java.lang.ref.WeakReference;	 Catch:{ Throwable -> 0x0913 }
        r3.<init>(r13);	 Catch:{ Throwable -> 0x0913 }
        r3 = com.appsflyer.C0288p.m335(r3);	 Catch:{ Throwable -> 0x0913 }
        r2.m186(r3);	 Catch:{ Throwable -> 0x0913 }
        r3 = "KSAppsFlyerId";
        r4 = r2.m183();	 Catch:{ Throwable -> 0x0913 }
        r6 = com.appsflyer.AppsFlyerProperties.getInstance();	 Catch:{ Throwable -> 0x0913 }
        r6.set(r3, r4);	 Catch:{ Throwable -> 0x0913 }
        r3 = "KSAppsFlyerRICounter";
        r2 = r2.m187();	 Catch:{ Throwable -> 0x0913 }
        r2 = java.lang.String.valueOf(r2);	 Catch:{ Throwable -> 0x0913 }
        r4 = com.appsflyer.AppsFlyerProperties.getInstance();	 Catch:{ Throwable -> 0x0913 }
        r4.set(r3, r2);	 Catch:{ Throwable -> 0x0913 }
    L_0x0204:
        r4 = "timepassedsincelastlaunch";
        r2 = "appsflyer-data";
        r3 = 0;
        r2 = r13.getSharedPreferences(r2, r3);	 Catch:{ Throwable -> 0x0913 }
        r3 = "AppsFlyerTimePassedSincePrevLaunch";
        r6 = 0;
        r2 = r2.getLong(r3, r6);	 Catch:{ Throwable -> 0x0913 }
        r6 = java.lang.System.currentTimeMillis();	 Catch:{ Throwable -> 0x0913 }
        r8 = "AppsFlyerTimePassedSincePrevLaunch";
        m216(r13, r8, r6);	 Catch:{ Throwable -> 0x0913 }
        r8 = 0;
        r8 = (r2 > r8 ? 1 : (r2 == r8 ? 0 : -1));
        if (r8 <= 0) goto L_0x098a;
    L_0x0224:
        r2 = r6 - r2;
        r6 = 1000; // 0x3e8 float:1.401E-42 double:4.94E-321;
        r2 = r2 / r6;
    L_0x0229:
        r2 = java.lang.Long.toString(r2);	 Catch:{ Throwable -> 0x0913 }
        r5.put(r4, r2);	 Catch:{ Throwable -> 0x0913 }
        r2 = com.appsflyer.AppsFlyerProperties.getInstance();	 Catch:{ Throwable -> 0x0913 }
        r3 = "oneLinkSlug";
        r2 = r2.getString(r3);	 Catch:{ Throwable -> 0x0913 }
        if (r2 == 0) goto L_0x0250;
    L_0x023c:
        r3 = "onelink_id";
        r5.put(r3, r2);	 Catch:{ Throwable -> 0x0913 }
        r2 = "ol_ver";
        r3 = com.appsflyer.AppsFlyerProperties.getInstance();	 Catch:{ Throwable -> 0x0913 }
        r4 = "onelinkVersion";
        r3 = r3.getString(r4);	 Catch:{ Throwable -> 0x0913 }
        r5.put(r2, r3);	 Catch:{ Throwable -> 0x0913 }
    L_0x0250:
        r2 = "KSAppsFlyerId";
        r3 = com.appsflyer.AppsFlyerProperties.getInstance();	 Catch:{ Throwable -> 0x0913 }
        r2 = r3.getString(r2);	 Catch:{ Throwable -> 0x0913 }
        r3 = "KSAppsFlyerRICounter";
        r4 = com.appsflyer.AppsFlyerProperties.getInstance();	 Catch:{ Throwable -> 0x0913 }
        r3 = r4.getString(r3);	 Catch:{ Throwable -> 0x0913 }
        if (r2 == 0) goto L_0x027c;
    L_0x0266:
        if (r3 == 0) goto L_0x027c;
    L_0x0268:
        r4 = java.lang.Integer.valueOf(r3);	 Catch:{ Throwable -> 0x0913 }
        r4 = r4.intValue();	 Catch:{ Throwable -> 0x0913 }
        if (r4 <= 0) goto L_0x027c;
    L_0x0272:
        r4 = "reinstallCounter";
        r5.put(r4, r3);	 Catch:{ Throwable -> 0x0913 }
        r3 = "originalAppsflyerId";
        r5.put(r3, r2);	 Catch:{ Throwable -> 0x0913 }
    L_0x027c:
        r2 = "additionalCustomData";
        r3 = com.appsflyer.AppsFlyerProperties.getInstance();	 Catch:{ Throwable -> 0x0913 }
        r2 = r3.getString(r2);	 Catch:{ Throwable -> 0x0913 }
        if (r2 == 0) goto L_0x028d;
    L_0x0288:
        r3 = "customData";
        r5.put(r3, r2);	 Catch:{ Throwable -> 0x0913 }
    L_0x028d:
        r2 = r13.getPackageManager();	 Catch:{ Exception -> 0x0a08 }
        r3 = r13.getPackageName();	 Catch:{ Exception -> 0x0a08 }
        r2 = r2.getInstallerPackageName(r3);	 Catch:{ Exception -> 0x0a08 }
        if (r2 == 0) goto L_0x02a0;
    L_0x029b:
        r3 = "installer_package";
        r5.put(r3, r2);	 Catch:{ Exception -> 0x0a08 }
    L_0x02a0:
        r2 = com.appsflyer.AppsFlyerProperties.getInstance();	 Catch:{ Throwable -> 0x0913 }
        r3 = "sdkExtension";
        r2 = r2.getString(r3);	 Catch:{ Throwable -> 0x0913 }
        if (r2 == 0) goto L_0x02b7;
    L_0x02ac:
        r3 = r2.length();	 Catch:{ Throwable -> 0x0913 }
        if (r3 <= 0) goto L_0x02b7;
    L_0x02b2:
        r3 = "sdkExtension";
        r5.put(r3, r2);	 Catch:{ Throwable -> 0x0913 }
    L_0x02b7:
        r3 = new java.lang.ref.WeakReference;	 Catch:{ Throwable -> 0x0913 }
        r3.<init>(r13);	 Catch:{ Throwable -> 0x0913 }
        r2 = com.appsflyer.AppsFlyerProperties.getInstance();	 Catch:{ Throwable -> 0x0913 }
        r4 = "channel";
        r2 = r2.getString(r4);	 Catch:{ Throwable -> 0x0913 }
        if (r2 != 0) goto L_0x02ce;
    L_0x02c8:
        r2 = "CHANNEL";
        r2 = m224(r3, r2);	 Catch:{ Throwable -> 0x0913 }
    L_0x02ce:
        r3 = m209(r13, r2);	 Catch:{ Throwable -> 0x0913 }
        if (r3 == 0) goto L_0x02d9;
    L_0x02d4:
        r4 = "channel";
        r5.put(r4, r3);	 Catch:{ Throwable -> 0x0913 }
    L_0x02d9:
        if (r3 == 0) goto L_0x02e1;
    L_0x02db:
        r4 = r3.equals(r2);	 Catch:{ Throwable -> 0x0913 }
        if (r4 == 0) goto L_0x02e5;
    L_0x02e1:
        if (r3 != 0) goto L_0x02ea;
    L_0x02e3:
        if (r2 == 0) goto L_0x02ea;
    L_0x02e5:
        r3 = "af_latestchannel";
        r5.put(r3, r2);	 Catch:{ Throwable -> 0x0913 }
    L_0x02ea:
        r2 = "appsflyer-data";
        r3 = 0;
        r2 = r13.getSharedPreferences(r2, r3);	 Catch:{ Throwable -> 0x0913 }
        r3 = "INSTALL_STORE";
        r3 = r2.contains(r3);	 Catch:{ Throwable -> 0x0913 }
        if (r3 == 0) goto L_0x0a10;
    L_0x02f9:
        r3 = "INSTALL_STORE";
        r4 = 0;
        r2 = r2.getString(r3, r4);	 Catch:{ Throwable -> 0x0913 }
    L_0x0300:
        if (r2 == 0) goto L_0x030b;
    L_0x0302:
        r3 = "af_installstore";
        r2 = r2.toLowerCase();	 Catch:{ Throwable -> 0x0913 }
        r5.put(r3, r2);	 Catch:{ Throwable -> 0x0913 }
    L_0x030b:
        r2 = "appsflyer-data";
        r3 = 0;
        r3 = r13.getSharedPreferences(r2, r3);	 Catch:{ Throwable -> 0x0913 }
        r2 = "preInstallName";
        r4 = com.appsflyer.AppsFlyerProperties.getInstance();	 Catch:{ Throwable -> 0x0913 }
        r2 = r4.getString(r2);	 Catch:{ Throwable -> 0x0913 }
        if (r2 != 0) goto L_0x0338;
    L_0x031e:
        r4 = "preInstallName";
        r4 = r3.contains(r4);	 Catch:{ Throwable -> 0x0913 }
        if (r4 == 0) goto L_0x0a38;
    L_0x0326:
        r2 = "preInstallName";
        r4 = 0;
        r2 = r3.getString(r2, r4);	 Catch:{ Throwable -> 0x0913 }
    L_0x032d:
        if (r2 == 0) goto L_0x0338;
    L_0x032f:
        r3 = "preInstallName";
        r4 = com.appsflyer.AppsFlyerProperties.getInstance();	 Catch:{ Throwable -> 0x0913 }
        r4.set(r3, r2);	 Catch:{ Throwable -> 0x0913 }
    L_0x0338:
        if (r2 == 0) goto L_0x0343;
    L_0x033a:
        r3 = "af_preinstall_name";
        r2 = r2.toLowerCase();	 Catch:{ Throwable -> 0x0913 }
        r5.put(r3, r2);	 Catch:{ Throwable -> 0x0913 }
    L_0x0343:
        r2 = new java.lang.ref.WeakReference;	 Catch:{ Throwable -> 0x0913 }
        r2.<init>(r13);	 Catch:{ Throwable -> 0x0913 }
        r3 = "AF_STORE";
        r2 = m224(r2, r3);	 Catch:{ Throwable -> 0x0913 }
        if (r2 == 0) goto L_0x0359;
    L_0x0350:
        r3 = "af_currentstore";
        r2 = r2.toLowerCase();	 Catch:{ Throwable -> 0x0913 }
        r5.put(r3, r2);	 Catch:{ Throwable -> 0x0913 }
    L_0x0359:
        if (r14 == 0) goto L_0x0acc;
    L_0x035b:
        r2 = r14.length();	 Catch:{ Throwable -> 0x0913 }
        if (r2 < 0) goto L_0x0acc;
    L_0x0361:
        r2 = "appsflyerKey";
        r5.put(r2, r14);	 Catch:{ Throwable -> 0x0913 }
    L_0x0366:
        r2 = "AppUserId";
        r3 = com.appsflyer.AppsFlyerProperties.getInstance();	 Catch:{ Throwable -> 0x0913 }
        r2 = r3.getString(r2);	 Catch:{ Throwable -> 0x0913 }
        if (r2 == 0) goto L_0x0377;
    L_0x0372:
        r3 = "appUserId";
        r5.put(r3, r2);	 Catch:{ Throwable -> 0x0913 }
    L_0x0377:
        r2 = com.appsflyer.AppsFlyerProperties.getInstance();	 Catch:{ Throwable -> 0x0913 }
        r3 = "userEmails";
        r2 = r2.getString(r3);	 Catch:{ Throwable -> 0x0913 }
        if (r2 == 0) goto L_0x0afa;
    L_0x0383:
        r3 = "user_emails";
        r5.put(r3, r2);	 Catch:{ Throwable -> 0x0913 }
    L_0x0388:
        if (r15 == 0) goto L_0x0398;
    L_0x038a:
        r2 = "eventName";
        r5.put(r2, r15);	 Catch:{ Throwable -> 0x0913 }
        if (r16 == 0) goto L_0x0398;
    L_0x0391:
        r2 = "eventValue";
        r0 = r16;
        r5.put(r2, r0);	 Catch:{ Throwable -> 0x0913 }
    L_0x0398:
        r2 = "appid";
        r3 = com.appsflyer.AppsFlyerProperties.getInstance();	 Catch:{ Throwable -> 0x0913 }
        r2 = r3.getString(r2);	 Catch:{ Throwable -> 0x0913 }
        if (r2 == 0) goto L_0x03b3;
    L_0x03a4:
        r2 = "appid";
        r3 = "appid";
        r4 = com.appsflyer.AppsFlyerProperties.getInstance();	 Catch:{ Throwable -> 0x0913 }
        r3 = r4.getString(r3);	 Catch:{ Throwable -> 0x0913 }
        r5.put(r2, r3);	 Catch:{ Throwable -> 0x0913 }
    L_0x03b3:
        r2 = "currencyCode";
        r3 = com.appsflyer.AppsFlyerProperties.getInstance();	 Catch:{ Throwable -> 0x0913 }
        r2 = r3.getString(r2);	 Catch:{ Throwable -> 0x0913 }
        if (r2 == 0) goto L_0x03e3;
    L_0x03bf:
        r3 = r2.length();	 Catch:{ Throwable -> 0x0913 }
        r4 = 3;
        if (r3 == r4) goto L_0x03de;
    L_0x03c6:
        r3 = new java.lang.StringBuilder;	 Catch:{ Throwable -> 0x0913 }
        r4 = "WARNING: currency code should be 3 characters!!! '";
        r3.<init>(r4);	 Catch:{ Throwable -> 0x0913 }
        r3 = r3.append(r2);	 Catch:{ Throwable -> 0x0913 }
        r4 = "' is not a legal value.";
        r3 = r3.append(r4);	 Catch:{ Throwable -> 0x0913 }
        r3 = r3.toString();	 Catch:{ Throwable -> 0x0913 }
        com.appsflyer.AFLogger.afWarnLog(r3);	 Catch:{ Throwable -> 0x0913 }
    L_0x03de:
        r3 = "currency";
        r5.put(r3, r2);	 Catch:{ Throwable -> 0x0913 }
    L_0x03e3:
        r2 = "IS_UPDATE";
        r3 = com.appsflyer.AppsFlyerProperties.getInstance();	 Catch:{ Throwable -> 0x0913 }
        r2 = r3.getString(r2);	 Catch:{ Throwable -> 0x0913 }
        if (r2 == 0) goto L_0x03f4;
    L_0x03ef:
        r3 = "isUpdate";
        r5.put(r3, r2);	 Catch:{ Throwable -> 0x0913 }
    L_0x03f4:
        r2 = r12.isPreInstalledApp(r13);	 Catch:{ Throwable -> 0x0913 }
        r3 = "af_preinstalled";
        r2 = java.lang.Boolean.toString(r2);	 Catch:{ Throwable -> 0x0913 }
        r5.put(r3, r2);	 Catch:{ Throwable -> 0x0913 }
        r2 = com.appsflyer.AppsFlyerProperties.getInstance();	 Catch:{ Throwable -> 0x0913 }
        r3 = "collectFacebookAttrId";
        r4 = 1;
        r2 = r2.getBoolean(r3, r4);	 Catch:{ Throwable -> 0x0913 }
        if (r2 == 0) goto L_0x0427;
    L_0x040e:
        r2 = r13.getPackageManager();	 Catch:{ NameNotFoundException -> 0x0b11, Throwable -> 0x0b1a }
        r3 = "com.facebook.katana";
        r4 = 0;
        r2.getApplicationInfo(r3, r4);	 Catch:{ NameNotFoundException -> 0x0b11, Throwable -> 0x0b1a }
        r2 = r13.getContentResolver();	 Catch:{ NameNotFoundException -> 0x0b11, Throwable -> 0x0b1a }
        r2 = r12.getAttributionId(r2);	 Catch:{ NameNotFoundException -> 0x0b11, Throwable -> 0x0b1a }
    L_0x0420:
        if (r2 == 0) goto L_0x0427;
    L_0x0422:
        r3 = "fb";
        r5.put(r3, r2);	 Catch:{ Throwable -> 0x0913 }
    L_0x0427:
        r2 = com.appsflyer.AppsFlyerProperties.getInstance();	 Catch:{ Throwable -> 0x0913 }
        r3 = "deviceTrackingDisabled";
        r4 = 0;
        r2 = r2.getBoolean(r3, r4);	 Catch:{ Throwable -> 0x0913 }
        if (r2 == 0) goto L_0x0b24;
    L_0x0434:
        r2 = "deviceTrackingDisabled";
        r3 = "true";
        r5.put(r2, r3);	 Catch:{ Throwable -> 0x0913 }
    L_0x043b:
        r2 = new java.lang.ref.WeakReference;	 Catch:{ Exception -> 0x0c16 }
        r2.<init>(r13);	 Catch:{ Exception -> 0x0c16 }
        r2 = com.appsflyer.C0288p.m335(r2);	 Catch:{ Exception -> 0x0c16 }
        if (r2 == 0) goto L_0x044b;
    L_0x0446:
        r3 = "uid";
        r5.put(r3, r2);	 Catch:{ Exception -> 0x0c16 }
    L_0x044b:
        r2 = "lang";
        r3 = java.util.Locale.getDefault();	 Catch:{ Exception -> 0x0c2f }
        r3 = r3.getDisplayLanguage();	 Catch:{ Exception -> 0x0c2f }
        r5.put(r2, r3);	 Catch:{ Exception -> 0x0c2f }
    L_0x0458:
        r2 = "lang_code";
        r3 = java.util.Locale.getDefault();	 Catch:{ Exception -> 0x0c37 }
        r3 = r3.getLanguage();	 Catch:{ Exception -> 0x0c37 }
        r5.put(r2, r3);	 Catch:{ Exception -> 0x0c37 }
    L_0x0465:
        r2 = "country";
        r3 = java.util.Locale.getDefault();	 Catch:{ Exception -> 0x0c3f }
        r3 = r3.getCountry();	 Catch:{ Exception -> 0x0c3f }
        r5.put(r2, r3);	 Catch:{ Exception -> 0x0c3f }
    L_0x0472:
        r2 = "platformextension";
        r3 = r12.f180;	 Catch:{ Throwable -> 0x0913 }
        r3 = r3.m367();	 Catch:{ Throwable -> 0x0913 }
        r5.put(r2, r3);	 Catch:{ Throwable -> 0x0913 }
        m253(r13, r5);	 Catch:{ Throwable -> 0x0913 }
        r2 = "yyyy-MM-dd_HHmmssZ";
        r3 = new java.text.SimpleDateFormat;	 Catch:{ Throwable -> 0x0913 }
        r4 = java.util.Locale.US;	 Catch:{ Throwable -> 0x0913 }
        r3.<init>(r2, r4);	 Catch:{ Throwable -> 0x0913 }
        r2 = android.os.Build.VERSION.SDK_INT;	 Catch:{ Throwable -> 0x0913 }
        r4 = 9;
        if (r2 < r4) goto L_0x04b5;
    L_0x048f:
        r2 = r13.getPackageManager();	 Catch:{ Exception -> 0x0c47 }
        r4 = r13.getPackageName();	 Catch:{ Exception -> 0x0c47 }
        r6 = 0;
        r2 = r2.getPackageInfo(r4, r6);	 Catch:{ Exception -> 0x0c47 }
        r6 = r2.firstInstallTime;	 Catch:{ Exception -> 0x0c47 }
        r2 = "installDate";
        r4 = "UTC";
        r4 = java.util.TimeZone.getTimeZone(r4);	 Catch:{ Exception -> 0x0c47 }
        r3.setTimeZone(r4);	 Catch:{ Exception -> 0x0c47 }
        r4 = new java.util.Date;	 Catch:{ Exception -> 0x0c47 }
        r4.<init>(r6);	 Catch:{ Exception -> 0x0c47 }
        r4 = r3.format(r4);	 Catch:{ Exception -> 0x0c47 }
        r5.put(r2, r4);	 Catch:{ Exception -> 0x0c47 }
    L_0x04b5:
        r2 = r13.getPackageManager();	 Catch:{ Throwable -> 0x0c56 }
        r4 = r13.getPackageName();	 Catch:{ Throwable -> 0x0c56 }
        r6 = 0;
        r2 = r2.getPackageInfo(r4, r6);	 Catch:{ Throwable -> 0x0c56 }
        r4 = "versionCode";
        r6 = 0;
        r0 = r19;
        r4 = r0.getInt(r4, r6);	 Catch:{ Throwable -> 0x0c56 }
        r6 = r2.versionCode;	 Catch:{ Throwable -> 0x0c56 }
        if (r6 <= r4) goto L_0x04dc;
    L_0x04cf:
        r4 = "appsflyerConversionDataRequestRetries";
        r6 = 0;
        m234(r13, r4, r6);	 Catch:{ Throwable -> 0x0c56 }
        r4 = "versionCode";
        r6 = r2.versionCode;	 Catch:{ Throwable -> 0x0c56 }
        m234(r13, r4, r6);	 Catch:{ Throwable -> 0x0c56 }
    L_0x04dc:
        r4 = "app_version_code";
        r6 = r2.versionCode;	 Catch:{ Throwable -> 0x0c56 }
        r6 = java.lang.Integer.toString(r6);	 Catch:{ Throwable -> 0x0c56 }
        r5.put(r4, r6);	 Catch:{ Throwable -> 0x0c56 }
        r4 = "app_version_name";
        r6 = r2.versionName;	 Catch:{ Throwable -> 0x0c56 }
        r5.put(r4, r6);	 Catch:{ Throwable -> 0x0c56 }
        r4 = android.os.Build.VERSION.SDK_INT;	 Catch:{ Throwable -> 0x0c56 }
        r6 = 9;
        if (r4 < r6) goto L_0x056d;
    L_0x04f4:
        r6 = r2.firstInstallTime;	 Catch:{ Throwable -> 0x0c56 }
        r8 = r2.lastUpdateTime;	 Catch:{ Throwable -> 0x0c56 }
        r2 = "date1";
        r4 = "UTC";
        r4 = java.util.TimeZone.getTimeZone(r4);	 Catch:{ Throwable -> 0x0c56 }
        r3.setTimeZone(r4);	 Catch:{ Throwable -> 0x0c56 }
        r4 = new java.util.Date;	 Catch:{ Throwable -> 0x0c56 }
        r4.<init>(r6);	 Catch:{ Throwable -> 0x0c56 }
        r4 = r3.format(r4);	 Catch:{ Throwable -> 0x0c56 }
        r5.put(r2, r4);	 Catch:{ Throwable -> 0x0c56 }
        r2 = "date2";
        r4 = "UTC";
        r4 = java.util.TimeZone.getTimeZone(r4);	 Catch:{ Throwable -> 0x0c56 }
        r3.setTimeZone(r4);	 Catch:{ Throwable -> 0x0c56 }
        r4 = new java.util.Date;	 Catch:{ Throwable -> 0x0c56 }
        r4.<init>(r8);	 Catch:{ Throwable -> 0x0c56 }
        r4 = r3.format(r4);	 Catch:{ Throwable -> 0x0c56 }
        r5.put(r2, r4);	 Catch:{ Throwable -> 0x0c56 }
        r2 = "appsflyer-data";
        r4 = 0;
        r2 = r13.getSharedPreferences(r2, r4);	 Catch:{ Throwable -> 0x0c56 }
        r4 = "appsFlyerFirstInstall";
        r6 = 0;
        r2 = r2.getString(r4, r6);	 Catch:{ Throwable -> 0x0c56 }
        if (r2 != 0) goto L_0x055b;
    L_0x0536:
        r2 = "appsflyer-data";
        r4 = 0;
        r2 = r13.getSharedPreferences(r2, r4);	 Catch:{ Throwable -> 0x0c56 }
        r4 = "appsFlyerCount";
        r2 = r2.contains(r4);	 Catch:{ Throwable -> 0x0c56 }
        if (r2 != 0) goto L_0x0c4f;
    L_0x0545:
        r2 = 1;
    L_0x0546:
        if (r2 == 0) goto L_0x0c52;
    L_0x0548:
        r2 = "AppsFlyer: first launch detected";
        com.appsflyer.AFLogger.afDebugLog(r2);	 Catch:{ Throwable -> 0x0c56 }
        r2 = new java.util.Date;	 Catch:{ Throwable -> 0x0c56 }
        r2.<init>();	 Catch:{ Throwable -> 0x0c56 }
        r2 = r3.format(r2);	 Catch:{ Throwable -> 0x0c56 }
    L_0x0556:
        r3 = "appsFlyerFirstInstall";
        m252(r13, r3, r2);	 Catch:{ Throwable -> 0x0c56 }
    L_0x055b:
        r3 = "AppsFlyer: first launch date: ";
        r4 = java.lang.String.valueOf(r2);	 Catch:{ Throwable -> 0x0c56 }
        r3 = r3.concat(r4);	 Catch:{ Throwable -> 0x0c56 }
        com.appsflyer.AFLogger.afInfoLog(r3);	 Catch:{ Throwable -> 0x0c56 }
        r3 = "firstLaunchDate";
        r5.put(r3, r2);	 Catch:{ Throwable -> 0x0c56 }
    L_0x056d:
        r2 = r17.length();	 Catch:{ Throwable -> 0x0913 }
        if (r2 <= 0) goto L_0x057a;
    L_0x0573:
        r2 = "referrer";
        r0 = r17;
        r5.put(r2, r0);	 Catch:{ Throwable -> 0x0913 }
    L_0x057a:
        r2 = "extraReferrers";
        r3 = 0;
        r0 = r19;
        r2 = r0.getString(r2, r3);	 Catch:{ Throwable -> 0x0913 }
        if (r2 == 0) goto L_0x058a;
    L_0x0585:
        r3 = "extraReferrers";
        r5.put(r3, r2);	 Catch:{ Throwable -> 0x0913 }
    L_0x058a:
        r2 = "afUninstallToken";
        r3 = com.appsflyer.AppsFlyerProperties.getInstance();	 Catch:{ Throwable -> 0x0913 }
        r2 = r3.getString(r2);	 Catch:{ Throwable -> 0x0913 }
        if (r2 == 0) goto L_0x05a3;
    L_0x0596:
        r2 = com.appsflyer.C0265d.m286(r2);	 Catch:{ Throwable -> 0x0913 }
        r3 = "af_gcm_token";
        r2 = r2.m289();	 Catch:{ Throwable -> 0x0913 }
        r5.put(r3, r2);	 Catch:{ Throwable -> 0x0913 }
    L_0x05a3:
        r2 = com.appsflyer.C0299u.m372(r13);	 Catch:{ Throwable -> 0x0913 }
        r12.f181 = r2;	 Catch:{ Throwable -> 0x0913 }
        r2 = new java.lang.StringBuilder;	 Catch:{ Throwable -> 0x0913 }
        r3 = "didConfigureTokenRefreshService=";
        r2.<init>(r3);	 Catch:{ Throwable -> 0x0913 }
        r3 = r12.f181;	 Catch:{ Throwable -> 0x0913 }
        r2 = r2.append(r3);	 Catch:{ Throwable -> 0x0913 }
        r2 = r2.toString();	 Catch:{ Throwable -> 0x0913 }
        com.appsflyer.AFLogger.afDebugLog(r2);	 Catch:{ Throwable -> 0x0913 }
        r2 = r12.f181;	 Catch:{ Throwable -> 0x0913 }
        if (r2 != 0) goto L_0x05c8;
    L_0x05c1:
        r2 = "tokenRefreshConfigured";
        r3 = java.lang.Boolean.FALSE;	 Catch:{ Throwable -> 0x0913 }
        r5.put(r2, r3);	 Catch:{ Throwable -> 0x0913 }
    L_0x05c8:
        if (r20 == 0) goto L_0x05e8;
    L_0x05ca:
        r2 = r12.f177;	 Catch:{ Throwable -> 0x0913 }
        if (r2 == 0) goto L_0x05e5;
    L_0x05ce:
        r2 = new org.json.JSONObject;	 Catch:{ Throwable -> 0x0913 }
        r3 = r12.f177;	 Catch:{ Throwable -> 0x0913 }
        r2.<init>(r3);	 Catch:{ Throwable -> 0x0913 }
        r3 = "isPush";
        r4 = "true";
        r2.put(r3, r4);	 Catch:{ Throwable -> 0x0913 }
        r3 = "af_deeplink";
        r2 = r2.toString();	 Catch:{ Throwable -> 0x0913 }
        r5.put(r3, r2);	 Catch:{ Throwable -> 0x0913 }
    L_0x05e5:
        r2 = 0;
        r12.f177 = r2;	 Catch:{ Throwable -> 0x0913 }
    L_0x05e8:
        if (r20 == 0) goto L_0x0602;
    L_0x05ea:
        r2 = 0;
        if (r21 == 0) goto L_0x05fd;
    L_0x05ed:
        r3 = "android.intent.action.VIEW";
        r4 = r21.getAction();	 Catch:{ Throwable -> 0x0913 }
        r3 = r3.equals(r4);	 Catch:{ Throwable -> 0x0913 }
        if (r3 == 0) goto L_0x05fd;
    L_0x05f9:
        r2 = r21.getData();	 Catch:{ Throwable -> 0x0913 }
    L_0x05fd:
        if (r2 == 0) goto L_0x0c5e;
    L_0x05ff:
        r12.m246(r13, r5, r2);	 Catch:{ Throwable -> 0x0913 }
    L_0x0602:
        r2 = r12.f175;	 Catch:{ Throwable -> 0x0913 }
        if (r2 == 0) goto L_0x062a;
    L_0x0606:
        r2 = "testAppMode_retargeting";
        r3 = "true";
        r5.put(r2, r3);	 Catch:{ Throwable -> 0x0913 }
        r2 = new org.json.JSONObject;	 Catch:{ Throwable -> 0x0913 }
        r2.<init>(r5);	 Catch:{ Throwable -> 0x0913 }
        r2 = r2.toString();	 Catch:{ Throwable -> 0x0913 }
        r3 = new android.content.Intent;	 Catch:{ Throwable -> 0x0913 }
        r4 = "com.appsflyer.testIntgrationBroadcast";
        r3.<init>(r4);	 Catch:{ Throwable -> 0x0913 }
        r4 = "params";
        r3.putExtra(r4, r2);	 Catch:{ Throwable -> 0x0913 }
        r13.sendBroadcast(r3);	 Catch:{ Throwable -> 0x0913 }
        r2 = "Sent retargeting params to test app";
        com.appsflyer.AFLogger.afInfoLog(r2);	 Catch:{ Throwable -> 0x0913 }
    L_0x062a:
        r2 = java.lang.System.currentTimeMillis();	 Catch:{ Throwable -> 0x0913 }
        r6 = r12.f173;	 Catch:{ Throwable -> 0x0913 }
        r2 = r2 - r6;
        r4 = com.appsflyer.AppsFlyerProperties.getInstance();	 Catch:{ Throwable -> 0x0913 }
        r4 = r4.getReferrer(r13);	 Catch:{ Throwable -> 0x0913 }
        r6 = 30000; // 0x7530 float:4.2039E-41 double:1.4822E-319;
        r2 = (r2 > r6 ? 1 : (r2 == r6 ? 0 : -1));
        if (r2 > 0) goto L_0x0c69;
    L_0x063f:
        if (r4 == 0) goto L_0x0c69;
    L_0x0641:
        r2 = "AppsFlyer_Test";
        r2 = r4.contains(r2);	 Catch:{ Throwable -> 0x0913 }
        if (r2 == 0) goto L_0x0c69;
    L_0x0649:
        r2 = 1;
    L_0x064a:
        if (r2 == 0) goto L_0x0679;
    L_0x064c:
        r2 = "testAppMode";
        r3 = "true";
        r5.put(r2, r3);	 Catch:{ Throwable -> 0x0913 }
        r2 = new org.json.JSONObject;	 Catch:{ Throwable -> 0x0913 }
        r2.<init>(r5);	 Catch:{ Throwable -> 0x0913 }
        r2 = r2.toString();	 Catch:{ Throwable -> 0x0913 }
        r3 = new android.content.Intent;	 Catch:{ Throwable -> 0x0913 }
        r4 = "com.appsflyer.testIntgrationBroadcast";
        r3.<init>(r4);	 Catch:{ Throwable -> 0x0913 }
        r4 = "params";
        r3.putExtra(r4, r2);	 Catch:{ Throwable -> 0x0913 }
        r13.sendBroadcast(r3);	 Catch:{ Throwable -> 0x0913 }
        r2 = "Sent params to test app";
        com.appsflyer.AFLogger.afInfoLog(r2);	 Catch:{ Throwable -> 0x0913 }
        r2 = "Test mode ended!";
        com.appsflyer.AFLogger.afInfoLog(r2);	 Catch:{ Throwable -> 0x0913 }
        r2 = 0;
        r12.f173 = r2;	 Catch:{ Throwable -> 0x0913 }
    L_0x0679:
        r2 = "advertiserId";
        r3 = com.appsflyer.AppsFlyerProperties.getInstance();	 Catch:{ Throwable -> 0x0913 }
        r2 = r3.getString(r2);	 Catch:{ Throwable -> 0x0913 }
        if (r2 != 0) goto L_0x069b;
    L_0x0685:
        com.appsflyer.C0287o.m334(r13, r5);	 Catch:{ Throwable -> 0x0913 }
        r2 = "advertiserId";
        r3 = com.appsflyer.AppsFlyerProperties.getInstance();	 Catch:{ Throwable -> 0x0913 }
        r2 = r3.getString(r2);	 Catch:{ Throwable -> 0x0913 }
        if (r2 == 0) goto L_0x0c6c;
    L_0x0694:
        r2 = "GAID_retry";
        r3 = "true";
        r5.put(r2, r3);	 Catch:{ Throwable -> 0x0913 }
    L_0x069b:
        r2 = r13.getContentResolver();	 Catch:{ Throwable -> 0x0913 }
        r2 = com.appsflyer.C0287o.m333(r2);	 Catch:{ Throwable -> 0x0913 }
        if (r2 == 0) goto L_0x06bb;
    L_0x06a5:
        r3 = "amazon_aid";
        r4 = r2.m332();	 Catch:{ Throwable -> 0x0913 }
        r5.put(r3, r4);	 Catch:{ Throwable -> 0x0913 }
        r3 = "amazon_aid_limit";
        r2 = r2.m331();	 Catch:{ Throwable -> 0x0913 }
        r2 = java.lang.String.valueOf(r2);	 Catch:{ Throwable -> 0x0913 }
        r5.put(r3, r2);	 Catch:{ Throwable -> 0x0913 }
    L_0x06bb:
        r2 = com.appsflyer.AppsFlyerProperties.getInstance();	 Catch:{ Throwable -> 0x0913 }
        r2 = r2.getReferrer(r13);	 Catch:{ Throwable -> 0x0913 }
        if (r2 == 0) goto L_0x06d8;
    L_0x06c5:
        r3 = r2.length();	 Catch:{ Throwable -> 0x0913 }
        if (r3 <= 0) goto L_0x06d8;
    L_0x06cb:
        r3 = "referrer";
        r3 = r5.get(r3);	 Catch:{ Throwable -> 0x0913 }
        if (r3 != 0) goto L_0x06d8;
    L_0x06d3:
        r3 = "referrer";
        r5.put(r3, r2);	 Catch:{ Throwable -> 0x0913 }
    L_0x06d8:
        r2 = "true";
        r3 = "sentSuccessfully";
        r4 = "";
        r0 = r19;
        r3 = r0.getString(r3, r4);	 Catch:{ Throwable -> 0x0913 }
        r3 = r2.equals(r3);	 Catch:{ Throwable -> 0x0913 }
        r2 = "sentRegisterRequestToAF";
        r4 = 0;
        r0 = r19;
        r2 = r0.getBoolean(r2, r4);	 Catch:{ Throwable -> 0x0913 }
        r4 = "registeredUninstall";
        r2 = java.lang.Boolean.valueOf(r2);	 Catch:{ Throwable -> 0x0913 }
        r5.put(r4, r2);	 Catch:{ Throwable -> 0x0913 }
        r2 = "appsFlyerCount";
        r0 = r19;
        r1 = r20;
        r4 = m208(r0, r2, r1);	 Catch:{ Throwable -> 0x0913 }
        r2 = "counter";
        r6 = java.lang.Integer.toString(r4);	 Catch:{ Throwable -> 0x0913 }
        r5.put(r2, r6);	 Catch:{ Throwable -> 0x0913 }
        r6 = "iaecounter";
        if (r15 == 0) goto L_0x0c75;
    L_0x0711:
        r2 = 1;
    L_0x0712:
        r7 = "appsFlyerInAppEventCount";
        r0 = r19;
        r2 = m208(r0, r7, r2);	 Catch:{ Throwable -> 0x0913 }
        r2 = java.lang.Integer.toString(r2);	 Catch:{ Throwable -> 0x0913 }
        r5.put(r6, r2);	 Catch:{ Throwable -> 0x0913 }
        if (r20 == 0) goto L_0x0744;
    L_0x0723:
        r2 = 1;
        if (r4 != r2) goto L_0x0744;
    L_0x0726:
        r2 = com.appsflyer.AppsFlyerProperties.getInstance();	 Catch:{ Throwable -> 0x0913 }
        r2.setFirstLaunchCalled();	 Catch:{ Throwable -> 0x0913 }
        r2 = "waitForCustomerId";
        r6 = com.appsflyer.AppsFlyerProperties.getInstance();	 Catch:{ Throwable -> 0x0913 }
        r7 = 0;
        r2 = r6.getBoolean(r2, r7);	 Catch:{ Throwable -> 0x0913 }
        if (r2 == 0) goto L_0x0744;
    L_0x073a:
        r2 = "wait_cid";
        r6 = 1;
        r6 = java.lang.Boolean.toString(r6);	 Catch:{ Throwable -> 0x0913 }
        r5.put(r2, r6);	 Catch:{ Throwable -> 0x0913 }
    L_0x0744:
        r6 = "isFirstCall";
        if (r3 != 0) goto L_0x0c78;
    L_0x0748:
        r2 = 1;
    L_0x0749:
        r2 = java.lang.Boolean.toString(r2);	 Catch:{ Throwable -> 0x0913 }
        r5.put(r6, r2);	 Catch:{ Throwable -> 0x0913 }
        r2 = new java.util.HashMap;	 Catch:{ Throwable -> 0x0913 }
        r2.<init>();	 Catch:{ Throwable -> 0x0913 }
        r3 = "cpu_abi";
        r6 = "ro.product.cpu.abi";
        r6 = m210(r6);	 Catch:{ Throwable -> 0x0913 }
        r2.put(r3, r6);	 Catch:{ Throwable -> 0x0913 }
        r3 = "cpu_abi2";
        r6 = "ro.product.cpu.abi2";
        r6 = m210(r6);	 Catch:{ Throwable -> 0x0913 }
        r2.put(r3, r6);	 Catch:{ Throwable -> 0x0913 }
        r3 = "arch";
        r6 = "os.arch";
        r6 = m210(r6);	 Catch:{ Throwable -> 0x0913 }
        r2.put(r3, r6);	 Catch:{ Throwable -> 0x0913 }
        r3 = "build_display_id";
        r6 = "ro.build.display.id";
        r6 = m210(r6);	 Catch:{ Throwable -> 0x0913 }
        r2.put(r3, r6);	 Catch:{ Throwable -> 0x0913 }
        if (r20 == 0) goto L_0x07ff;
    L_0x0783:
        r3 = r12.f171;	 Catch:{ Throwable -> 0x0913 }
        if (r3 == 0) goto L_0x07c7;
    L_0x0787:
        r3 = com.appsflyer.C0278j.C0277d.f271;	 Catch:{ Throwable -> 0x0913 }
        r3 = com.appsflyer.C0278j.m319(r13);	 Catch:{ Throwable -> 0x0913 }
        r6 = new java.util.HashMap;	 Catch:{ Throwable -> 0x0913 }
        r7 = 3;
        r6.<init>(r7);	 Catch:{ Throwable -> 0x0913 }
        if (r3 == 0) goto L_0x07bc;
    L_0x0795:
        r7 = "lat";
        r8 = r3.getLatitude();	 Catch:{ Throwable -> 0x0913 }
        r8 = java.lang.String.valueOf(r8);	 Catch:{ Throwable -> 0x0913 }
        r6.put(r7, r8);	 Catch:{ Throwable -> 0x0913 }
        r7 = "lon";
        r8 = r3.getLongitude();	 Catch:{ Throwable -> 0x0913 }
        r8 = java.lang.String.valueOf(r8);	 Catch:{ Throwable -> 0x0913 }
        r6.put(r7, r8);	 Catch:{ Throwable -> 0x0913 }
        r7 = "ts";
        r8 = r3.getTime();	 Catch:{ Throwable -> 0x0913 }
        r3 = java.lang.String.valueOf(r8);	 Catch:{ Throwable -> 0x0913 }
        r6.put(r7, r3);	 Catch:{ Throwable -> 0x0913 }
    L_0x07bc:
        r3 = r6.isEmpty();	 Catch:{ Throwable -> 0x0913 }
        if (r3 != 0) goto L_0x07c7;
    L_0x07c2:
        r3 = "loc";
        r2.put(r3, r6);	 Catch:{ Throwable -> 0x0913 }
    L_0x07c7:
        r3 = com.appsflyer.C0264c.C0262c.f214;	 Catch:{ Throwable -> 0x0913 }
        r3 = r3.m284(r13);	 Catch:{ Throwable -> 0x0913 }
        r6 = "btl";
        r7 = r3.m282();	 Catch:{ Throwable -> 0x0913 }
        r7 = java.lang.Float.toString(r7);	 Catch:{ Throwable -> 0x0913 }
        r2.put(r6, r7);	 Catch:{ Throwable -> 0x0913 }
        r6 = r3.m283();	 Catch:{ Throwable -> 0x0913 }
        if (r6 == 0) goto L_0x07e9;
    L_0x07e0:
        r6 = "btch";
        r3 = r3.m283();	 Catch:{ Throwable -> 0x0913 }
        r2.put(r6, r3);	 Catch:{ Throwable -> 0x0913 }
    L_0x07e9:
        r3 = 2;
        if (r3 < r4) goto L_0x07ff;
    L_0x07ec:
        r3 = com.appsflyer.C0270f.m295(r13);	 Catch:{ Throwable -> 0x0913 }
        r3 = r3.m297();	 Catch:{ Throwable -> 0x0913 }
        r4 = r3.isEmpty();	 Catch:{ Throwable -> 0x0913 }
        if (r4 != 0) goto L_0x07ff;
    L_0x07fa:
        r4 = "sensors";
        r2.put(r4, r3);	 Catch:{ Throwable -> 0x0913 }
    L_0x07ff:
        r3 = com.appsflyer.AFScreenManager.getScreenMetrics(r13);	 Catch:{ Throwable -> 0x0913 }
        r4 = "dim";
        r2.put(r4, r3);	 Catch:{ Throwable -> 0x0913 }
        r3 = "deviceData";
        r5.put(r3, r2);	 Catch:{ Throwable -> 0x0913 }
        r2 = new com.appsflyer.r;	 Catch:{ Throwable -> 0x0913 }
        r2.<init>();	 Catch:{ Throwable -> 0x0913 }
        r2 = "appsflyerKey";
        r2 = r5.get(r2);	 Catch:{ Throwable -> 0x0913 }
        r2 = (java.lang.String) r2;	 Catch:{ Throwable -> 0x0913 }
        r3 = "af_timestamp";
        r3 = r5.get(r3);	 Catch:{ Throwable -> 0x0913 }
        r3 = (java.lang.String) r3;	 Catch:{ Throwable -> 0x0913 }
        r4 = "uid";
        r4 = r5.get(r4);	 Catch:{ Throwable -> 0x0913 }
        r4 = (java.lang.String) r4;	 Catch:{ Throwable -> 0x0913 }
        r6 = new java.lang.StringBuilder;	 Catch:{ Throwable -> 0x0913 }
        r6.<init>();	 Catch:{ Throwable -> 0x0913 }
        r7 = 0;
        r8 = 7;
        r2 = r2.substring(r7, r8);	 Catch:{ Throwable -> 0x0913 }
        r2 = r6.append(r2);	 Catch:{ Throwable -> 0x0913 }
        r6 = 0;
        r7 = 7;
        r4 = r4.substring(r6, r7);	 Catch:{ Throwable -> 0x0913 }
        r2 = r2.append(r4);	 Catch:{ Throwable -> 0x0913 }
        r4 = r3.length();	 Catch:{ Throwable -> 0x0913 }
        r4 = r4 + -7;
        r3 = r3.substring(r4);	 Catch:{ Throwable -> 0x0913 }
        r2 = r2.append(r3);	 Catch:{ Throwable -> 0x0913 }
        r2 = r2.toString();	 Catch:{ Throwable -> 0x0913 }
        r2 = com.appsflyer.C0291r.m348(r2);	 Catch:{ Throwable -> 0x0913 }
        r3 = "af_v";
        r5.put(r3, r2);	 Catch:{ Throwable -> 0x0913 }
        r2 = new com.appsflyer.r;	 Catch:{ Throwable -> 0x0913 }
        r2.<init>();	 Catch:{ Throwable -> 0x0913 }
        r2 = "appsflyerKey";
        r2 = r5.get(r2);	 Catch:{ Throwable -> 0x0913 }
        r2 = (java.lang.String) r2;	 Catch:{ Throwable -> 0x0913 }
        r3 = new java.lang.StringBuilder;	 Catch:{ Throwable -> 0x0913 }
        r3.<init>();	 Catch:{ Throwable -> 0x0913 }
        r2 = r3.append(r2);	 Catch:{ Throwable -> 0x0913 }
        r3 = "af_timestamp";
        r3 = r5.get(r3);	 Catch:{ Throwable -> 0x0913 }
        r2 = r2.append(r3);	 Catch:{ Throwable -> 0x0913 }
        r2 = r2.toString();	 Catch:{ Throwable -> 0x0913 }
        r3 = new java.lang.StringBuilder;	 Catch:{ Throwable -> 0x0913 }
        r3.<init>();	 Catch:{ Throwable -> 0x0913 }
        r2 = r3.append(r2);	 Catch:{ Throwable -> 0x0913 }
        r3 = "uid";
        r3 = r5.get(r3);	 Catch:{ Throwable -> 0x0913 }
        r2 = r2.append(r3);	 Catch:{ Throwable -> 0x0913 }
        r2 = r2.toString();	 Catch:{ Throwable -> 0x0913 }
        r3 = new java.lang.StringBuilder;	 Catch:{ Throwable -> 0x0913 }
        r3.<init>();	 Catch:{ Throwable -> 0x0913 }
        r2 = r3.append(r2);	 Catch:{ Throwable -> 0x0913 }
        r3 = "installDate";
        r3 = r5.get(r3);	 Catch:{ Throwable -> 0x0913 }
        r2 = r2.append(r3);	 Catch:{ Throwable -> 0x0913 }
        r2 = r2.toString();	 Catch:{ Throwable -> 0x0913 }
        r3 = new java.lang.StringBuilder;	 Catch:{ Throwable -> 0x0913 }
        r3.<init>();	 Catch:{ Throwable -> 0x0913 }
        r2 = r3.append(r2);	 Catch:{ Throwable -> 0x0913 }
        r3 = "counter";
        r3 = r5.get(r3);	 Catch:{ Throwable -> 0x0913 }
        r2 = r2.append(r3);	 Catch:{ Throwable -> 0x0913 }
        r2 = r2.toString();	 Catch:{ Throwable -> 0x0913 }
        r3 = new java.lang.StringBuilder;	 Catch:{ Throwable -> 0x0913 }
        r3.<init>();	 Catch:{ Throwable -> 0x0913 }
        r2 = r3.append(r2);	 Catch:{ Throwable -> 0x0913 }
        r3 = "iaecounter";
        r3 = r5.get(r3);	 Catch:{ Throwable -> 0x0913 }
        r2 = r2.append(r3);	 Catch:{ Throwable -> 0x0913 }
        r2 = r2.toString();	 Catch:{ Throwable -> 0x0913 }
        r2 = com.appsflyer.C0291r.m347(r2);	 Catch:{ Throwable -> 0x0913 }
        r2 = com.appsflyer.C0291r.m348(r2);	 Catch:{ Throwable -> 0x0913 }
        r3 = "af_v2";
        r5.put(r3, r2);	 Catch:{ Throwable -> 0x0913 }
        r2 = "is_stop_tracking_used";
        r0 = r19;
        r2 = r0.contains(r2);	 Catch:{ Throwable -> 0x0913 }
        if (r2 == 0) goto L_0x0907;
    L_0x08f5:
        r2 = "istu";
        r3 = "is_stop_tracking_used";
        r4 = 0;
        r0 = r19;
        r3 = r0.getBoolean(r3, r4);	 Catch:{ Throwable -> 0x0913 }
        r3 = java.lang.String.valueOf(r3);	 Catch:{ Throwable -> 0x0913 }
        r5.put(r2, r3);	 Catch:{ Throwable -> 0x0913 }
    L_0x0907:
        r2 = r5;
    L_0x0908:
        return r2;
    L_0x0909:
        r2 = r15;
        goto L_0x0036;
    L_0x090c:
        r2 = "SDK tracking has been stopped";
        com.appsflyer.AFLogger.afInfoLog(r2);	 Catch:{ Throwable -> 0x0913 }
        goto L_0x0041;
    L_0x0913:
        r2 = move-exception;
        r3 = r2.getLocalizedMessage();
        com.appsflyer.AFLogger.afErrorLog(r3, r2);
        goto L_0x0907;
    L_0x091c:
        r2 = r15;
        goto L_0x0049;
    L_0x091f:
        r2 = move-exception;
        r3 = "Exception while validation permissions. ";
        com.appsflyer.AFLogger.afErrorLog(r3, r2);	 Catch:{ Throwable -> 0x0913 }
        goto L_0x0095;
    L_0x0927:
        r2 = 0;
        goto L_0x00de;
    L_0x092a:
        r2 = 0;
        goto L_0x00fc;
    L_0x092d:
        r2 = 0;
        goto L_0x010b;
    L_0x0930:
        r2 = 0;
        goto L_0x011a;
    L_0x0933:
        r2 = 0;
        goto L_0x0129;
    L_0x0936:
        r2 = 0;
        goto L_0x0138;
    L_0x0939:
        r2 = 0;
        goto L_0x0147;
    L_0x093c:
        r2 = 0;
        goto L_0x0156;
    L_0x093f:
        r2 = 0;
        goto L_0x0165;
    L_0x0942:
        r2 = 0;
        goto L_0x0174;
    L_0x0945:
        r2 = 0;
        goto L_0x0183;
    L_0x0948:
        r3 = 0;
        goto L_0x01a8;
    L_0x094b:
        r2.m185();	 Catch:{ Throwable -> 0x0913 }
        r3 = "KSAppsFlyerId";
        r4 = r2.m183();	 Catch:{ Throwable -> 0x0913 }
        r6 = com.appsflyer.AppsFlyerProperties.getInstance();	 Catch:{ Throwable -> 0x0913 }
        r6.set(r3, r4);	 Catch:{ Throwable -> 0x0913 }
        r3 = "KSAppsFlyerRICounter";
        r2 = r2.m187();	 Catch:{ Throwable -> 0x0913 }
        r2 = java.lang.String.valueOf(r2);	 Catch:{ Throwable -> 0x0913 }
        r4 = com.appsflyer.AppsFlyerProperties.getInstance();	 Catch:{ Throwable -> 0x0913 }
        r4.set(r3, r2);	 Catch:{ Throwable -> 0x0913 }
        goto L_0x0204;
    L_0x096e:
        r2 = new java.lang.StringBuilder;	 Catch:{ Throwable -> 0x0913 }
        r3 = "OS SDK is=";
        r2.<init>(r3);	 Catch:{ Throwable -> 0x0913 }
        r3 = android.os.Build.VERSION.SDK_INT;	 Catch:{ Throwable -> 0x0913 }
        r2 = r2.append(r3);	 Catch:{ Throwable -> 0x0913 }
        r3 = "; no KeyStore usage";
        r2 = r2.append(r3);	 Catch:{ Throwable -> 0x0913 }
        r2 = r2.toString();	 Catch:{ Throwable -> 0x0913 }
        com.appsflyer.AFLogger.afRDLog(r2);	 Catch:{ Throwable -> 0x0913 }
        goto L_0x0204;
    L_0x098a:
        r2 = -1;
        goto L_0x0229;
    L_0x098e:
        r2 = "appsflyer-data";
        r3 = 0;
        r2 = r13.getSharedPreferences(r2, r3);	 Catch:{ Throwable -> 0x0913 }
        r3 = r2.edit();	 Catch:{ Throwable -> 0x0913 }
        r4 = "prev_event_name";
        r6 = 0;
        r4 = r2.getString(r4, r6);	 Catch:{ Exception -> 0x09fb }
        if (r4 == 0) goto L_0x09db;
    L_0x09a2:
        r6 = new org.json.JSONObject;	 Catch:{ Exception -> 0x09fb }
        r6.<init>();	 Catch:{ Exception -> 0x09fb }
        r7 = "prev_event_timestamp";
        r8 = new java.lang.StringBuilder;	 Catch:{ Exception -> 0x09fb }
        r8.<init>();	 Catch:{ Exception -> 0x09fb }
        r9 = "prev_event_timestamp";
        r10 = -1;
        r10 = r2.getLong(r9, r10);	 Catch:{ Exception -> 0x09fb }
        r8 = r8.append(r10);	 Catch:{ Exception -> 0x09fb }
        r8 = r8.toString();	 Catch:{ Exception -> 0x09fb }
        r6.put(r7, r8);	 Catch:{ Exception -> 0x09fb }
        r7 = "prev_event_value";
        r8 = "prev_event_value";
        r9 = 0;
        r2 = r2.getString(r8, r9);	 Catch:{ Exception -> 0x09fb }
        r6.put(r7, r2);	 Catch:{ Exception -> 0x09fb }
        r2 = "prev_event_name";
        r6.put(r2, r4);	 Catch:{ Exception -> 0x09fb }
        r2 = "prev_event";
        r4 = r6.toString();	 Catch:{ Exception -> 0x09fb }
        r5.put(r2, r4);	 Catch:{ Exception -> 0x09fb }
    L_0x09db:
        r2 = "prev_event_name";
        r3.putString(r2, r15);	 Catch:{ Exception -> 0x09fb }
        r2 = "prev_event_value";
        r0 = r16;
        r3.putString(r2, r0);	 Catch:{ Exception -> 0x09fb }
        r2 = "prev_event_timestamp";
        r6 = java.lang.System.currentTimeMillis();	 Catch:{ Exception -> 0x09fb }
        r3.putLong(r2, r6);	 Catch:{ Exception -> 0x09fb }
        r2 = android.os.Build.VERSION.SDK_INT;	 Catch:{ Exception -> 0x09fb }
        r4 = 9;
        if (r2 < r4) goto L_0x0a03;
    L_0x09f6:
        r3.apply();	 Catch:{ Exception -> 0x09fb }
        goto L_0x0250;
    L_0x09fb:
        r2 = move-exception;
        r3 = "Error while processing previous event.";
        com.appsflyer.AFLogger.afErrorLog(r3, r2);	 Catch:{ Throwable -> 0x0913 }
        goto L_0x0250;
    L_0x0a03:
        r3.commit();	 Catch:{ Exception -> 0x09fb }
        goto L_0x0250;
    L_0x0a08:
        r2 = move-exception;
        r3 = "Exception while getting the app's installer package. ";
        com.appsflyer.AFLogger.afErrorLog(r3, r2);	 Catch:{ Throwable -> 0x0913 }
        goto L_0x02a0;
    L_0x0a10:
        r2 = "appsflyer-data";
        r3 = 0;
        r2 = r13.getSharedPreferences(r2, r3);	 Catch:{ Throwable -> 0x0913 }
        r3 = "appsFlyerCount";
        r2 = r2.contains(r3);	 Catch:{ Throwable -> 0x0913 }
        if (r2 != 0) goto L_0x0a34;
    L_0x0a1f:
        r2 = 1;
    L_0x0a20:
        if (r2 == 0) goto L_0x0a36;
    L_0x0a22:
        r2 = new java.lang.ref.WeakReference;	 Catch:{ Throwable -> 0x0913 }
        r2.<init>(r13);	 Catch:{ Throwable -> 0x0913 }
        r3 = "AF_STORE";
        r2 = m224(r2, r3);	 Catch:{ Throwable -> 0x0913 }
    L_0x0a2d:
        r3 = "INSTALL_STORE";
        m252(r13, r3, r2);	 Catch:{ Throwable -> 0x0913 }
        goto L_0x0300;
    L_0x0a34:
        r2 = 0;
        goto L_0x0a20;
    L_0x0a36:
        r2 = 0;
        goto L_0x0a2d;
    L_0x0a38:
        r3 = "appsflyer-data";
        r4 = 0;
        r3 = r13.getSharedPreferences(r3, r4);	 Catch:{ Throwable -> 0x0913 }
        r4 = "appsFlyerCount";
        r3 = r3.contains(r4);	 Catch:{ Throwable -> 0x0913 }
        if (r3 != 0) goto L_0x0ab4;
    L_0x0a47:
        r3 = 1;
    L_0x0a48:
        if (r3 == 0) goto L_0x0aab;
    L_0x0a4a:
        r2 = "ro.appsflyer.preinstall.path";
        r2 = m210(r2);	 Catch:{ Throwable -> 0x0913 }
        r2 = m242(r2);	 Catch:{ Throwable -> 0x0913 }
        if (r2 == 0) goto L_0x0a5c;
    L_0x0a56:
        r3 = r2.exists();	 Catch:{ Throwable -> 0x0913 }
        if (r3 != 0) goto L_0x0ab6;
    L_0x0a5c:
        r3 = 1;
    L_0x0a5d:
        if (r3 == 0) goto L_0x0a71;
    L_0x0a5f:
        r2 = "AF_PRE_INSTALL_PATH";
        r3 = r13.getPackageManager();	 Catch:{ Throwable -> 0x0913 }
        r4 = r13.getPackageName();	 Catch:{ Throwable -> 0x0913 }
        r2 = m249(r2, r3, r4);	 Catch:{ Throwable -> 0x0913 }
        r2 = m242(r2);	 Catch:{ Throwable -> 0x0913 }
    L_0x0a71:
        if (r2 == 0) goto L_0x0a79;
    L_0x0a73:
        r3 = r2.exists();	 Catch:{ Throwable -> 0x0913 }
        if (r3 != 0) goto L_0x0ab8;
    L_0x0a79:
        r3 = 1;
    L_0x0a7a:
        if (r3 == 0) goto L_0x0a82;
    L_0x0a7c:
        r2 = "/data/local/tmp/pre_install.appsflyer";
        r2 = m242(r2);	 Catch:{ Throwable -> 0x0913 }
    L_0x0a82:
        if (r2 == 0) goto L_0x0a8a;
    L_0x0a84:
        r3 = r2.exists();	 Catch:{ Throwable -> 0x0913 }
        if (r3 != 0) goto L_0x0aba;
    L_0x0a8a:
        r3 = 1;
    L_0x0a8b:
        if (r3 == 0) goto L_0x0c81;
    L_0x0a8d:
        r2 = "/etc/pre_install.appsflyer";
        r2 = m242(r2);	 Catch:{ Throwable -> 0x0913 }
        r3 = r2;
    L_0x0a94:
        if (r3 == 0) goto L_0x0a9c;
    L_0x0a96:
        r2 = r3.exists();	 Catch:{ Throwable -> 0x0913 }
        if (r2 != 0) goto L_0x0abc;
    L_0x0a9c:
        r2 = 1;
    L_0x0a9d:
        if (r2 != 0) goto L_0x0abe;
    L_0x0a9f:
        r2 = r13.getPackageName();	 Catch:{ Throwable -> 0x0913 }
        r2 = m222(r3, r2);	 Catch:{ Throwable -> 0x0913 }
        if (r2 == 0) goto L_0x0abe;
    L_0x0aa9:
        if (r2 == 0) goto L_0x0ac0;
    L_0x0aab:
        if (r2 == 0) goto L_0x032d;
    L_0x0aad:
        r3 = "preInstallName";
        m252(r13, r3, r2);	 Catch:{ Throwable -> 0x0913 }
        goto L_0x032d;
    L_0x0ab4:
        r3 = 0;
        goto L_0x0a48;
    L_0x0ab6:
        r3 = 0;
        goto L_0x0a5d;
    L_0x0ab8:
        r3 = 0;
        goto L_0x0a7a;
    L_0x0aba:
        r3 = 0;
        goto L_0x0a8b;
    L_0x0abc:
        r2 = 0;
        goto L_0x0a9d;
    L_0x0abe:
        r2 = 0;
        goto L_0x0aa9;
    L_0x0ac0:
        r2 = new java.lang.ref.WeakReference;	 Catch:{ Throwable -> 0x0913 }
        r2.<init>(r13);	 Catch:{ Throwable -> 0x0913 }
        r3 = "AF_PRE_INSTALL_NAME";
        r2 = m224(r2, r3);	 Catch:{ Throwable -> 0x0913 }
        goto L_0x0aab;
    L_0x0acc:
        r2 = "AppsFlyerKey";
        r3 = com.appsflyer.AppsFlyerProperties.getInstance();	 Catch:{ Throwable -> 0x0913 }
        r2 = r3.getString(r2);	 Catch:{ Throwable -> 0x0913 }
        if (r2 == 0) goto L_0x0ae5;
    L_0x0ad8:
        r3 = r2.length();	 Catch:{ Throwable -> 0x0913 }
        if (r3 < 0) goto L_0x0ae5;
    L_0x0ade:
        r3 = "appsflyerKey";
        r5.put(r3, r2);	 Catch:{ Throwable -> 0x0913 }
        goto L_0x0366;
    L_0x0ae5:
        r2 = "AppsFlyer dev key is missing!!! Please use  AppsFlyerLib.getInstance().setAppsFlyerKey(...) to set it. ";
        com.appsflyer.AFLogger.afInfoLog(r2);	 Catch:{ Throwable -> 0x0913 }
        r2 = "AppsFlyer_4.8.11";
        r3 = "DEV_KEY_MISSING";
        r4 = 0;
        m227(r13, r2, r3, r4);	 Catch:{ Throwable -> 0x0913 }
        r2 = "AppsFlyer will not track this event.";
        com.appsflyer.AFLogger.afInfoLog(r2);	 Catch:{ Throwable -> 0x0913 }
        r2 = 0;
        goto L_0x0908;
    L_0x0afa:
        r2 = "userEmail";
        r3 = com.appsflyer.AppsFlyerProperties.getInstance();	 Catch:{ Throwable -> 0x0913 }
        r2 = r3.getString(r2);	 Catch:{ Throwable -> 0x0913 }
        if (r2 == 0) goto L_0x0388;
    L_0x0b06:
        r3 = "sha1_el";
        r2 = com.appsflyer.C0291r.m348(r2);	 Catch:{ Throwable -> 0x0913 }
        r5.put(r3, r2);	 Catch:{ Throwable -> 0x0913 }
        goto L_0x0388;
    L_0x0b11:
        r2 = move-exception;
        r2 = 0;
        r3 = "Exception while collecting facebook's attribution ID. ";
        com.appsflyer.AFLogger.afWarnLog(r3);	 Catch:{ Throwable -> 0x0913 }
        goto L_0x0420;
    L_0x0b1a:
        r2 = move-exception;
        r3 = 0;
        r4 = "Exception while collecting facebook's attribution ID. ";
        com.appsflyer.AFLogger.afErrorLog(r4, r2);	 Catch:{ Throwable -> 0x0913 }
        r2 = r3;
        goto L_0x0420;
    L_0x0b24:
        r2 = "appsflyer-data";
        r3 = 0;
        r6 = r13.getSharedPreferences(r2, r3);	 Catch:{ Throwable -> 0x0913 }
        r2 = com.appsflyer.AppsFlyerProperties.getInstance();	 Catch:{ Throwable -> 0x0913 }
        r3 = "collectIMEI";
        r4 = 1;
        r2 = r2.getBoolean(r3, r4);	 Catch:{ Throwable -> 0x0913 }
        r3 = "imeiCached";
        r4 = 0;
        r3 = r6.getString(r3, r4);	 Catch:{ Throwable -> 0x0913 }
        r4 = 0;
        if (r2 == 0) goto L_0x0bde;
    L_0x0b40:
        r2 = android.os.Build.VERSION.SDK_INT;	 Catch:{ Throwable -> 0x0913 }
        r7 = 19;
        if (r2 < r7) goto L_0x0b4c;
    L_0x0b46:
        r2 = m229(r13);	 Catch:{ Throwable -> 0x0913 }
        if (r2 != 0) goto L_0x0bbb;
    L_0x0b4c:
        r2 = 1;
    L_0x0b4d:
        if (r2 == 0) goto L_0x0bd7;
    L_0x0b4f:
        r2 = "phone";
        r2 = r13.getSystemService(r2);	 Catch:{ InvocationTargetException -> 0x0bc9, Exception -> 0x0bd0 }
        r2 = (android.telephony.TelephonyManager) r2;	 Catch:{ InvocationTargetException -> 0x0bc9, Exception -> 0x0bd0 }
        r7 = r2.getClass();	 Catch:{ InvocationTargetException -> 0x0bc9, Exception -> 0x0bd0 }
        r8 = "getDeviceId";
        r9 = 0;
        r9 = new java.lang.Class[r9];	 Catch:{ InvocationTargetException -> 0x0bc9, Exception -> 0x0bd0 }
        r7 = r7.getMethod(r8, r9);	 Catch:{ InvocationTargetException -> 0x0bc9, Exception -> 0x0bd0 }
        r8 = 0;
        r8 = new java.lang.Object[r8];	 Catch:{ InvocationTargetException -> 0x0bc9, Exception -> 0x0bd0 }
        r2 = r7.invoke(r2, r8);	 Catch:{ InvocationTargetException -> 0x0bc9, Exception -> 0x0bd0 }
        r2 = (java.lang.String) r2;	 Catch:{ InvocationTargetException -> 0x0bc9, Exception -> 0x0bd0 }
        if (r2 == 0) goto L_0x0bbd;
    L_0x0b6f:
        r4 = r2;
    L_0x0b70:
        if (r4 == 0) goto L_0x0be5;
    L_0x0b72:
        r2 = "imeiCached";
        m252(r13, r2, r4);	 Catch:{ Throwable -> 0x0913 }
        r2 = "imei";
        r5.put(r2, r4);	 Catch:{ Throwable -> 0x0913 }
    L_0x0b7c:
        r2 = com.appsflyer.AppsFlyerProperties.getInstance();	 Catch:{ Throwable -> 0x0913 }
        r3 = "collectAndroidId";
        r4 = 1;
        r4 = r2.getBoolean(r3, r4);	 Catch:{ Throwable -> 0x0913 }
        r2 = "androidIdCached";
        r3 = 0;
        r2 = r6.getString(r2, r3);	 Catch:{ Throwable -> 0x0913 }
        r3 = 0;
        if (r4 == 0) goto L_0x0c08;
    L_0x0b91:
        r4 = android.os.Build.VERSION.SDK_INT;	 Catch:{ Throwable -> 0x0913 }
        r6 = 19;
        if (r4 < r6) goto L_0x0b9d;
    L_0x0b97:
        r4 = m229(r13);	 Catch:{ Throwable -> 0x0913 }
        if (r4 != 0) goto L_0x0beb;
    L_0x0b9d:
        r4 = 1;
    L_0x0b9e:
        if (r4 == 0) goto L_0x0c01;
    L_0x0ba0:
        r4 = r13.getContentResolver();	 Catch:{ Exception -> 0x0bf8 }
        r6 = "android_id";
        r4 = android.provider.Settings.Secure.getString(r4, r6);	 Catch:{ Exception -> 0x0bf8 }
        if (r4 == 0) goto L_0x0bed;
    L_0x0bac:
        r3 = r4;
    L_0x0bad:
        if (r3 == 0) goto L_0x0c0f;
    L_0x0baf:
        r2 = "androidIdCached";
        m252(r13, r2, r3);	 Catch:{ Throwable -> 0x0913 }
        r2 = "android_id";
        r5.put(r2, r3);	 Catch:{ Throwable -> 0x0913 }
        goto L_0x043b;
    L_0x0bbb:
        r2 = 0;
        goto L_0x0b4d;
    L_0x0bbd:
        r2 = r12.f172;	 Catch:{ InvocationTargetException -> 0x0bc9, Exception -> 0x0bd0 }
        if (r2 == 0) goto L_0x0bc4;
    L_0x0bc1:
        r4 = r12.f172;	 Catch:{ InvocationTargetException -> 0x0bc9, Exception -> 0x0bd0 }
        goto L_0x0b70;
    L_0x0bc4:
        if (r3 == 0) goto L_0x0c7e;
    L_0x0bc6:
        r2 = r3;
    L_0x0bc7:
        r4 = r2;
        goto L_0x0b70;
    L_0x0bc9:
        r2 = move-exception;
        r2 = "WARNING: READ_PHONE_STATE is missing.";
        com.appsflyer.AFLogger.afWarnLog(r2);	 Catch:{ Throwable -> 0x0913 }
        goto L_0x0b70;
    L_0x0bd0:
        r2 = move-exception;
        r3 = "WARNING: READ_PHONE_STATE is missing. ";
        com.appsflyer.AFLogger.afErrorLog(r3, r2);	 Catch:{ Throwable -> 0x0913 }
        goto L_0x0b70;
    L_0x0bd7:
        r2 = r12.f172;	 Catch:{ Throwable -> 0x0913 }
        if (r2 == 0) goto L_0x0b70;
    L_0x0bdb:
        r4 = r12.f172;	 Catch:{ Throwable -> 0x0913 }
        goto L_0x0b70;
    L_0x0bde:
        r2 = r12.f172;	 Catch:{ Throwable -> 0x0913 }
        if (r2 == 0) goto L_0x0b70;
    L_0x0be2:
        r4 = r12.f172;	 Catch:{ Throwable -> 0x0913 }
        goto L_0x0b70;
    L_0x0be5:
        r2 = "IMEI was not collected.";
        com.appsflyer.AFLogger.afInfoLog(r2);	 Catch:{ Throwable -> 0x0913 }
        goto L_0x0b7c;
    L_0x0beb:
        r4 = 0;
        goto L_0x0b9e;
    L_0x0bed:
        r4 = r12.f176;	 Catch:{ Exception -> 0x0bf8 }
        if (r4 == 0) goto L_0x0bf4;
    L_0x0bf1:
        r3 = r12.f176;	 Catch:{ Exception -> 0x0bf8 }
        goto L_0x0bad;
    L_0x0bf4:
        if (r2 == 0) goto L_0x0c7b;
    L_0x0bf6:
        r3 = r2;
        goto L_0x0bad;
    L_0x0bf8:
        r2 = move-exception;
        r4 = r2.getMessage();	 Catch:{ Throwable -> 0x0913 }
        com.appsflyer.AFLogger.afErrorLog(r4, r2);	 Catch:{ Throwable -> 0x0913 }
        goto L_0x0bad;
    L_0x0c01:
        r2 = r12.f176;	 Catch:{ Throwable -> 0x0913 }
        if (r2 == 0) goto L_0x0bad;
    L_0x0c05:
        r3 = r12.f176;	 Catch:{ Throwable -> 0x0913 }
        goto L_0x0bad;
    L_0x0c08:
        r2 = r12.f176;	 Catch:{ Throwable -> 0x0913 }
        if (r2 == 0) goto L_0x0bad;
    L_0x0c0c:
        r3 = r12.f176;	 Catch:{ Throwable -> 0x0913 }
        goto L_0x0bad;
    L_0x0c0f:
        r2 = "Android ID was not collected.";
        com.appsflyer.AFLogger.afInfoLog(r2);	 Catch:{ Throwable -> 0x0913 }
        goto L_0x043b;
    L_0x0c16:
        r2 = move-exception;
        r3 = new java.lang.StringBuilder;	 Catch:{ Throwable -> 0x0913 }
        r4 = "ERROR: could not get uid ";
        r3.<init>(r4);	 Catch:{ Throwable -> 0x0913 }
        r4 = r2.getMessage();	 Catch:{ Throwable -> 0x0913 }
        r3 = r3.append(r4);	 Catch:{ Throwable -> 0x0913 }
        r3 = r3.toString();	 Catch:{ Throwable -> 0x0913 }
        com.appsflyer.AFLogger.afErrorLog(r3, r2);	 Catch:{ Throwable -> 0x0913 }
        goto L_0x044b;
    L_0x0c2f:
        r2 = move-exception;
        r3 = "Exception while collecting display language name. ";
        com.appsflyer.AFLogger.afErrorLog(r3, r2);	 Catch:{ Throwable -> 0x0913 }
        goto L_0x0458;
    L_0x0c37:
        r2 = move-exception;
        r3 = "Exception while collecting display language code. ";
        com.appsflyer.AFLogger.afErrorLog(r3, r2);	 Catch:{ Throwable -> 0x0913 }
        goto L_0x0465;
    L_0x0c3f:
        r2 = move-exception;
        r3 = "Exception while collecting country name. ";
        com.appsflyer.AFLogger.afErrorLog(r3, r2);	 Catch:{ Throwable -> 0x0913 }
        goto L_0x0472;
    L_0x0c47:
        r2 = move-exception;
        r4 = "Exception while collecting install date. ";
        com.appsflyer.AFLogger.afErrorLog(r4, r2);	 Catch:{ Throwable -> 0x0913 }
        goto L_0x04b5;
    L_0x0c4f:
        r2 = 0;
        goto L_0x0546;
    L_0x0c52:
        r2 = "";
        goto L_0x0556;
    L_0x0c56:
        r2 = move-exception;
        r3 = "Exception while collecting app version data ";
        com.appsflyer.AFLogger.afErrorLog(r3, r2);	 Catch:{ Throwable -> 0x0913 }
        goto L_0x056d;
    L_0x0c5e:
        r2 = r12.f170;	 Catch:{ Throwable -> 0x0913 }
        if (r2 == 0) goto L_0x0602;
    L_0x0c62:
        r2 = r12.f170;	 Catch:{ Throwable -> 0x0913 }
        r12.m246(r13, r5, r2);	 Catch:{ Throwable -> 0x0913 }
        goto L_0x0602;
    L_0x0c69:
        r2 = 0;
        goto L_0x064a;
    L_0x0c6c:
        r2 = "GAID_retry";
        r3 = "false";
        r5.put(r2, r3);	 Catch:{ Throwable -> 0x0913 }
        goto L_0x069b;
    L_0x0c75:
        r2 = 0;
        goto L_0x0712;
    L_0x0c78:
        r2 = 0;
        goto L_0x0749;
    L_0x0c7b:
        r2 = r3;
        goto L_0x0bf6;
    L_0x0c7e:
        r2 = r4;
        goto L_0x0bc7;
    L_0x0c81:
        r3 = r2;
        goto L_0x0a94;
        */
        throw new UnsupportedOperationException("Method not decompiled: com.appsflyer.AppsFlyerLib.ˎ(android.content.Context, java.lang.String, java.lang.String, java.lang.String, java.lang.String, boolean, android.content.SharedPreferences, boolean, android.content.Intent):java.util.Map<java.lang.String, java.lang.Object>");
    }

    /* renamed from: ˏ */
    private void m246(Context context, Map<String, Object> map, Uri uri) {
        Map ˎ;
        map.put("af_deeplink", uri.toString());
        if (uri.getQueryParameter("af_deeplink") != null) {
            boolean z = "AppsFlyer_Test".equals(uri.getQueryParameter("media_source")) && Boolean.parseBoolean(uri.getQueryParameter("is_retargeting"));
            this.f175 = z;
            ˎ = m233(context, uri.getQuery());
            String str = "path";
            String path = uri.getPath();
            if (path != null) {
                ˎ.put(str, path);
            }
            str = "scheme";
            path = uri.getScheme();
            if (path != null) {
                ˎ.put(str, path);
            }
            str = "host";
            path = uri.getHost();
            if (path != null) {
                ˎ.put(str, path);
            }
        } else {
            ˎ = new HashMap();
            ˎ.put("link", uri.toString());
        }
        final WeakReference weakReference = new WeakReference(context);
        Runnable c0292s = new C0292s(uri, this);
        c0292s.setConnProvider(new HttpsUrlConnectionProvider());
        if (c0292s.m353()) {
            c0292s.m352(new C0248b(this) {
                /* renamed from: ॱ */
                private /* synthetic */ AppsFlyerLib f125;

                /* renamed from: ॱ */
                public final void mo1204(String str) {
                    if (AppsFlyerLib.f161 != null) {
                        m195(ˎ);
                        AppsFlyerLib.f161.onAttributionFailure(str);
                    }
                }

                /* renamed from: ˋ */
                private void m195(Map<String, String> map) {
                    if (weakReference.get() != null) {
                        AppsFlyerLib.m252((Context) weakReference.get(), "deeplinkAttribution", new JSONObject(map).toString());
                    }
                }

                /* renamed from: ॱ */
                public final void mo1205(Map<String, String> map) {
                    for (String str : map.keySet()) {
                        ˎ.put(str, map.get(str));
                    }
                    m195(ˎ);
                    AppsFlyerLib.m254(ˎ);
                }
            });
            AFExecutor.getInstance().getThreadPoolExecutor().execute(c0292s);
        } else if (f161 != null) {
            try {
                f161.onAppOpenAttribution(ˎ);
            } catch (Throwable th) {
                AFLogger.afErrorLog(th.getLocalizedMessage(), th);
            }
        }
    }

    /* renamed from: ˋ */
    private static boolean m229(Context context) {
        try {
            if (GoogleApiAvailability.getInstance().isGooglePlayServicesAvailable(context) == 0) {
                return true;
            }
        } catch (Throwable th) {
            AFLogger.afErrorLog("WARNING:  Google play services is unavailable. ", th);
        }
        try {
            context.getPackageManager().getPackageInfo("com.google.android.gms", 0);
            return true;
        } catch (Throwable e) {
            AFLogger.afErrorLog("WARNING:  Google Play Services is unavailable. ", e);
            return false;
        }
    }

    /* renamed from: ˊ */
    private static String m210(String str) {
        try {
            return (String) Class.forName("android.os.SystemProperties").getMethod("get", new Class[]{String.class}).invoke(null, new Object[]{str});
        } catch (Throwable th) {
            AFLogger.afErrorLog(th.getMessage(), th);
            return null;
        }
    }

    @Nullable
    /* renamed from: ˋ */
    private static String m224(WeakReference<Context> weakReference, String str) {
        if (weakReference.get() == null) {
            return null;
        }
        return m249(str, ((Context) weakReference.get()).getPackageManager(), ((Context) weakReference.get()).getPackageName());
    }

    @Nullable
    /* renamed from: ॱ */
    private static String m249(String str, PackageManager packageManager, String str2) {
        String str3 = null;
        try {
            Bundle bundle = packageManager.getApplicationInfo(str2, 128).metaData;
            if (bundle != null) {
                Object obj = bundle.get(str);
                if (obj != null) {
                    str3 = obj.toString();
                }
            }
        } catch (Throwable th) {
            AFLogger.afErrorLog(new StringBuilder("Could not find ").append(str).append(" value in the manifest").toString(), th);
        }
        return str3;
    }

    public void setPreinstallAttribution(String str, String str2, String str3) {
        AFLogger.afDebugLog("setPreinstallAttribution API called");
        JSONObject jSONObject = new JSONObject();
        if (str != null) {
            try {
                jSONObject.put(Constants.URL_MEDIA_SOURCE, str);
            } catch (Throwable e) {
                AFLogger.afErrorLog(e.getMessage(), e);
            }
        }
        if (str2 != null) {
            jSONObject.put(Constants.URL_CAMPAIGN, str2);
        }
        if (str3 != null) {
            jSONObject.put(Constants.URL_SITE_ID, str3);
        }
        if (jSONObject.has(Constants.URL_MEDIA_SOURCE)) {
            String jSONObject2 = jSONObject.toString();
            AppsFlyerProperties.getInstance().set("preInstallName", jSONObject2);
            return;
        }
        AFLogger.afWarnLog("Cannot set preinstall attribution data without a media source");
    }

    /* renamed from: ˋ */
    private static String m222(File file, String str) {
        Reader fileReader;
        Throwable th;
        Throwable th2;
        Throwable th3;
        Reader reader;
        String str2 = null;
        try {
            Properties properties = new Properties();
            fileReader = new FileReader(file);
            try {
                properties.load(fileReader);
                AFLogger.afInfoLog("Found PreInstall property!");
                str2 = properties.getProperty(str);
                try {
                    fileReader.close();
                } catch (Throwable th4) {
                    AFLogger.afErrorLog(th4.getMessage(), th4);
                }
            } catch (FileNotFoundException e) {
                try {
                    AFLogger.afDebugLog(new StringBuilder("PreInstall file wasn't found: ").append(file.getAbsolutePath()).toString());
                    if (fileReader != null) {
                        try {
                            fileReader.close();
                        } catch (Throwable th42) {
                            AFLogger.afErrorLog(th42.getMessage(), th42);
                        }
                    }
                    return str2;
                } catch (Throwable th5) {
                    th2 = th5;
                    if (fileReader != null) {
                        try {
                            fileReader.close();
                        } catch (Throwable th422) {
                            AFLogger.afErrorLog(th422.getMessage(), th422);
                        }
                    }
                    throw th2;
                }
            } catch (Throwable th6) {
                th3 = th6;
                reader = fileReader;
                th422 = th3;
                try {
                    AFLogger.afErrorLog(th422.getMessage(), th422);
                    if (reader != null) {
                        try {
                            reader.close();
                        } catch (Throwable th4222) {
                            AFLogger.afErrorLog(th4222.getMessage(), th4222);
                        }
                    }
                    return str2;
                } catch (Throwable th7) {
                    th2 = th7;
                    fileReader = reader;
                    if (fileReader != null) {
                        fileReader.close();
                    }
                    throw th2;
                }
            }
        } catch (FileNotFoundException e2) {
            fileReader = str2;
            AFLogger.afDebugLog(new StringBuilder("PreInstall file wasn't found: ").append(file.getAbsolutePath()).toString());
            if (fileReader != null) {
                fileReader.close();
            }
            return str2;
        } catch (Throwable th42222) {
            th3 = th42222;
            fileReader = str2;
            th2 = th3;
            if (fileReader != null) {
                fileReader.close();
            }
            throw th2;
        }
        return str2;
    }

    /* renamed from: ˏ */
    private static File m242(String str) {
        if (str != null) {
            try {
                if (str.trim().length() > 0) {
                    return new File(str.trim());
                }
            } catch (Throwable th) {
                AFLogger.afErrorLog(th.getMessage(), th);
            }
        }
        return null;
    }

    public boolean isPreInstalledApp(Context context) {
        try {
            if ((context.getPackageManager().getApplicationInfo(context.getPackageName(), 0).flags & 1) != 0) {
                return true;
            }
            return false;
        } catch (Throwable e) {
            AFLogger.afErrorLog("Could not check if app is pre installed", e);
            return false;
        }
    }

    public String getAttributionId(ContentResolver contentResolver) {
        String str = null;
        String[] strArr = new String[]{ATTRIBUTION_ID_COLUMN_NAME};
        Cursor query = contentResolver.query(Uri.parse(ATTRIBUTION_ID_CONTENT_URI), strArr, str, str, str);
        if (query != null) {
            try {
                if (query.moveToFirst()) {
                    str = query.getString(query.getColumnIndex(ATTRIBUTION_ID_COLUMN_NAME));
                    if (query != null) {
                        try {
                            query.close();
                        } catch (Throwable e) {
                            AFLogger.afErrorLog(e.getMessage(), e);
                        }
                    }
                    return str;
                }
            } catch (Throwable e2) {
                AFLogger.afErrorLog("Could not collect cursor attribution. ", e2);
                if (query != null) {
                    try {
                        query.close();
                    } catch (Throwable e22) {
                        AFLogger.afErrorLog(e22.getMessage(), e22);
                    }
                }
            } catch (Throwable th) {
                if (query != null) {
                    try {
                        query.close();
                    } catch (Throwable e3) {
                        AFLogger.afErrorLog(e3.getMessage(), e3);
                    }
                }
            }
        }
        if (query != null) {
            try {
                query.close();
            } catch (Throwable e222) {
                AFLogger.afErrorLog(e222.getMessage(), e222);
            }
        }
        return str;
    }

    /* renamed from: ˏ */
    static SharedPreferences m240(Context context) {
        return context.getSharedPreferences("appsflyer-data", 0);
    }

    /* renamed from: ˏ */
    static int m239(SharedPreferences sharedPreferences) {
        return m208(sharedPreferences, "appsFlyerCount", false);
    }

    /* renamed from: ˊ */
    private static int m208(SharedPreferences sharedPreferences, String str, boolean z) {
        int i = sharedPreferences.getInt(str, 0);
        if (z) {
            i++;
            Editor edit = sharedPreferences.edit();
            edit.putInt(str, i);
            if (VERSION.SDK_INT >= 9) {
                edit.apply();
            } else {
                edit.commit();
            }
        }
        if (C0300y.m378().m385()) {
            C0300y.m378().m387(String.valueOf(i));
        }
        return i;
    }

    public String getAppsFlyerUID(Context context) {
        C0300y.m378().m393("getAppsFlyerUID", new String[0]);
        return C0288p.m335(new WeakReference(context));
    }

    /* renamed from: ˊ */
    private void m218(URL url, String str, String str2, WeakReference<Context> weakReference, String str3, boolean z) throws IOException {
        Object obj;
        Throwable th;
        Writer writer;
        Context context = (Context) weakReference.get();
        if (!z || f161 == null) {
            obj = null;
        } else {
            obj = 1;
        }
        HttpURLConnection httpURLConnection;
        try {
            C0300y.m378().m391(url.toString(), str);
            httpURLConnection = (HttpURLConnection) url.openConnection();
            try {
                httpURLConnection.setRequestMethod(HttpRequest.METHOD_POST);
                httpURLConnection.setRequestProperty(HttpRequest.HEADER_CONTENT_LENGTH, String.valueOf(str.getBytes().length));
                httpURLConnection.setRequestProperty(HttpRequest.HEADER_CONTENT_TYPE, "application/json");
                httpURLConnection.setConnectTimeout(10000);
                httpURLConnection.setDoOutput(true);
                try {
                    Writer outputStreamWriter = new OutputStreamWriter(httpURLConnection.getOutputStream(), "UTF-8");
                    try {
                        outputStreamWriter.write(str);
                        outputStreamWriter.close();
                        int responseCode = httpURLConnection.getResponseCode();
                        String ˎ = m231(httpURLConnection);
                        C0300y.m378().m390(url.toString(), responseCode, ˎ);
                        AFLogger.afInfoLog("response code: ".concat(String.valueOf(responseCode)));
                        m227(context, LOG_TAG, "SERVER_RESPONSE_CODE", Integer.toString(responseCode));
                        SharedPreferences sharedPreferences = context.getSharedPreferences("appsflyer-data", 0);
                        if (responseCode == 200) {
                            if (weakReference.get() != null && z) {
                                this.f183 = System.currentTimeMillis();
                            }
                            String string = AppsFlyerProperties.getInstance().getString("afUninstallToken");
                            if (string != null) {
                                AFLogger.afDebugLog("Uninstall Token exists: ".concat(String.valueOf(string)));
                                if (!sharedPreferences.getBoolean("sentRegisterRequestToAF", false)) {
                                    AFLogger.afDebugLog("Resending Uninstall token to AF servers: ".concat(String.valueOf(string)));
                                    C0299u.m375(context, new C0265d(string));
                                }
                            } else {
                                if (AppsFlyerProperties.getInstance().getString("gcmProjectNumber") != null) {
                                    AFLogger.afDebugLog("GCM Project number exists. Fetching token and sending to AF servers");
                                    new C0298c(new WeakReference(context)).execute(new Void[0]);
                                }
                            }
                            if (this.f170 != null) {
                                this.f170 = null;
                            }
                            if (str3 != null) {
                                CacheManager.getInstance().deleteRequest(str3, context);
                            }
                            if (weakReference.get() != null && str3 == null) {
                                m252(context, "sentSuccessfully", ServerProtocol.DIALOG_RETURN_SCOPES_TRUE);
                                if (!this.f187 && System.currentTimeMillis() - this.f188 >= 15000 && this.f185 == null) {
                                    this.f185 = AFExecutor.getInstance().m179();
                                    m237(this.f185, new C0257c(this, context), 1, TimeUnit.SECONDS);
                                }
                            }
                            this.f179 = ServerConfigHandler.m273(ˎ).optBoolean("send_background", false);
                        }
                        responseCode = sharedPreferences.getInt("appsflyerConversionDataRequestRetries", 0);
                        long j = sharedPreferences.getLong("appsflyerConversionDataCacheExpiration", 0);
                        if (j != 0 && System.currentTimeMillis() - j > 5184000000L) {
                            m252(context, "attributionId", null);
                            m216(context, "appsflyerConversionDataCacheExpiration", 0);
                        }
                        if (sharedPreferences.getString("attributionId", null) == null && str2 != null && obj != null && f161 != null && responseCode <= 5) {
                            ScheduledExecutorService ॱ = AFExecutor.getInstance().m179();
                            m237(ॱ, new C0255a(this, context.getApplicationContext(), str2, ॱ), 10, TimeUnit.MILLISECONDS);
                        } else if (str2 == null) {
                            AFLogger.afWarnLog("AppsFlyer dev key is missing.");
                        } else if (!(obj == null || f161 == null || sharedPreferences.getString("attributionId", null) == null || m208(sharedPreferences, "appsFlyerCount", false) <= 1)) {
                            Map ˊ = m212(context);
                            if (ˊ != null) {
                                try {
                                    if (!ˊ.containsKey("is_first_launch")) {
                                        ˊ.put("is_first_launch", Boolean.toString(false));
                                    }
                                    f161.onInstallConversionDataLoaded(ˊ);
                                } catch (Throwable th2) {
                                    AFLogger.afErrorLog(th2.getLocalizedMessage(), th2);
                                }
                            }
                        }
                    } catch (Throwable th3) {
                        th2 = th3;
                        writer = outputStreamWriter;
                        if (writer != null) {
                            writer.close();
                        }
                        throw th2;
                    }
                } catch (Throwable th4) {
                    th2 = th4;
                    writer = null;
                    if (writer != null) {
                        writer.close();
                    }
                    throw th2;
                }
            } catch (Throwable th22) {
                AFLogger.afErrorLog(th22.getMessage(), th22);
            } catch (Throwable th5) {
                th22 = th5;
                if (httpURLConnection != null) {
                    httpURLConnection.disconnect();
                }
                throw th22;
            }
            if (httpURLConnection != null) {
                httpURLConnection.disconnect();
            }
        } catch (Throwable th6) {
            th22 = th6;
            httpURLConnection = null;
            if (httpURLConnection != null) {
                httpURLConnection.disconnect();
            }
            throw th22;
        }
    }

    public void validateAndTrackInAppPurchase(Context context, String str, String str2, String str3, String str4, String str5, Map<String, String> map) {
        C0300y ˋ = C0300y.m378();
        String str6 = "validateAndTrackInAppPurchase";
        String[] strArr = new String[6];
        strArr[0] = str;
        strArr[1] = str2;
        strArr[2] = str3;
        strArr[3] = str4;
        strArr[4] = str5;
        strArr[5] = map == null ? "" : map.toString();
        ˋ.m393(str6, strArr);
        if (!isTrackingStopped()) {
            AFLogger.afInfoLog(new StringBuilder("Validate in app called with parameters: ").append(str3).append(" ").append(str4).append(" ").append(str5).toString());
        }
        if (str != null && str4 != null && str2 != null && str5 != null && str3 != null) {
            ScheduledExecutorService ॱ = AFExecutor.getInstance().m179();
            m237(ॱ, new C0276i(context.getApplicationContext(), AppsFlyerProperties.getInstance().getString(AppsFlyerProperties.AF_KEY), str, str2, str3, str4, str5, map, ॱ, context instanceof Activity ? ((Activity) context).getIntent() : null), 10, TimeUnit.MILLISECONDS);
        } else if (f160 != null) {
            f160.onValidateInAppFailure("Please provide purchase parameters");
        }
    }

    /* renamed from: ˎ */
    private static void m237(ScheduledExecutorService scheduledExecutorService, Runnable runnable, long j, TimeUnit timeUnit) {
        if (scheduledExecutorService != null) {
            try {
                if (!(scheduledExecutorService.isShutdown() || scheduledExecutorService.isTerminated())) {
                    scheduledExecutorService.schedule(runnable, j, timeUnit);
                    return;
                }
            } catch (Throwable e) {
                AFLogger.afErrorLog("scheduleJob failed with RejectedExecutionException Exception", e);
                return;
            } catch (Throwable e2) {
                AFLogger.afErrorLog("scheduleJob failed with Exception", e2);
                return;
            }
        }
        AFLogger.afWarnLog("scheduler is null, shut downed or terminated");
    }

    public void onHandleReferrer(Map<String, String> map) {
        this.f166 = map;
    }

    public boolean isTrackingStopped() {
        return this.f178;
    }

    @NonNull
    /* renamed from: ˎ */
    static String m231(HttpURLConnection httpURLConnection) {
        String readLine;
        Throwable th;
        Reader reader;
        JSONObject jSONObject;
        Reader reader2 = null;
        StringBuilder stringBuilder = new StringBuilder();
        Reader inputStreamReader;
        try {
            InputStream errorStream = httpURLConnection.getErrorStream();
            if (errorStream == null) {
                errorStream = httpURLConnection.getInputStream();
            }
            inputStreamReader = new InputStreamReader(errorStream);
            try {
                Reader bufferedReader = new BufferedReader(inputStreamReader);
                while (true) {
                    try {
                        readLine = bufferedReader.readLine();
                        if (readLine != null) {
                            stringBuilder.append(readLine).append('\n');
                        } else {
                            try {
                                break;
                            } catch (Throwable th2) {
                            }
                        }
                    } catch (Throwable th3) {
                        th = th3;
                        reader2 = inputStreamReader;
                        inputStreamReader = bufferedReader;
                    }
                }
                bufferedReader.close();
                inputStreamReader.close();
            } catch (Throwable th4) {
                th = th4;
                reader = inputStreamReader;
                inputStreamReader = null;
                reader2 = reader;
                if (inputStreamReader != null) {
                    inputStreamReader.close();
                }
                if (reader2 != null) {
                    reader2.close();
                }
                throw th;
            }
        } catch (Throwable th5) {
            th = th5;
            inputStreamReader = null;
            if (inputStreamReader != null) {
                inputStreamReader.close();
            }
            if (reader2 != null) {
                reader2.close();
            }
            throw th;
        }
        readLine = stringBuilder.toString();
        try {
            jSONObject = new JSONObject(readLine);
            return readLine;
        } catch (JSONException e) {
            jSONObject = new JSONObject();
            try {
                jSONObject.put("string_response", readLine);
                return jSONObject.toString();
            } catch (JSONException e2) {
                return new JSONObject().toString();
            }
        }
    }

    /* renamed from: ॱॱ */
    private static float m256(Context context) {
        float f = 1.0f;
        try {
            Intent registerReceiver = context.getApplicationContext().registerReceiver(null, new IntentFilter("android.intent.action.BATTERY_CHANGED"));
            int intExtra = registerReceiver.getIntExtra(Param.LEVEL, -1);
            int intExtra2 = registerReceiver.getIntExtra("scale", -1);
            if (intExtra == -1 || intExtra2 == -1) {
                return 50.0f;
            }
            return (((float) intExtra) / ((float) intExtra2)) * 100.0f;
        } catch (Throwable th) {
            AFLogger.afErrorLog(th.getMessage(), th);
            return f;
        }
    }

    public void setLogLevel(LogLevel logLevel) {
        AppsFlyerProperties.getInstance().set("logLevel", logLevel.getLevel());
    }

    public void setHostName(String str) {
        AppsFlyerProperties.getInstance().set("custom_host", str);
    }

    public String getHost() {
        String string = AppsFlyerProperties.getInstance().getString("custom_host");
        return string != null ? string : ServerParameters.DEFAULT_HOST;
    }

    public void setMinTimeBetweenSessions(int i) {
        this.f184 = TimeUnit.SECONDS.toMillis((long) i);
    }

    /* renamed from: ॱ */
    private static void m252(Context context, String str, String str2) {
        Editor edit = context.getSharedPreferences("appsflyer-data", 0).edit();
        edit.putString(str, str2);
        if (VERSION.SDK_INT >= 9) {
            edit.apply();
        } else {
            edit.commit();
        }
    }

    /* renamed from: ˎ */
    private static void m234(Context context, String str, int i) {
        Editor edit = context.getSharedPreferences("appsflyer-data", 0).edit();
        edit.putInt(str, i);
        if (VERSION.SDK_INT >= 9) {
            edit.apply();
        } else {
            edit.commit();
        }
    }

    /* renamed from: ˊ */
    private static void m216(Context context, String str, long j) {
        Editor edit = context.getSharedPreferences("appsflyer-data", 0).edit();
        edit.putLong(str, j);
        if (VERSION.SDK_INT >= 9) {
            edit.apply();
        } else {
            edit.commit();
        }
    }
}
