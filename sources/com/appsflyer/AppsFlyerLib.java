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
import android.os.Build.VERSION;
import android.os.Bundle;
import android.os.Looper;
import android.os.Process;
import android.support.annotation.NonNull;
import android.support.annotation.Nullable;
import android.text.TextUtils;
import com.appsflyer.AFLogger.LogLevel;
import com.appsflyer.AppsFlyerProperties.EmailsCryptType;
import com.appsflyer.OneLinkHttpTask.HttpsUrlConnectionProvider;
import com.appsflyer.cache.CacheManager;
import com.appsflyer.cache.RequestCacheData;
import com.appsflyer.share.Constants;
import com.facebook.internal.ServerProtocol;
import com.google.android.gms.common.GoogleApiAvailability;
import com.google.firebase.analytics.FirebaseAnalytics.Param;
import java.io.File;
import java.io.IOException;
import java.lang.ref.WeakReference;
import java.net.URL;
import java.text.SimpleDateFormat;
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
import java.util.TimeZone;
import java.util.concurrent.ExecutorService;
import java.util.concurrent.RejectedExecutionException;
import java.util.concurrent.ScheduledExecutorService;
import java.util.concurrent.ScheduledThreadPoolExecutor;
import java.util.concurrent.TimeUnit;
import java.util.concurrent.atomic.AtomicInteger;
import net.gogame.gowrap.integrations.AbstractIntegrationSupport;
import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

public class AppsFlyerLib implements C0428b {
    public static final String AF_PRE_INSTALL_PATH = "AF_PRE_INSTALL_PATH";
    public static final String ATTRIBUTION_ID_COLUMN_NAME = "aid";
    public static final String ATTRIBUTION_ID_CONTENT_URI = "content://com.facebook.katana.provider.AttributionIdProvider";
    public static final String IS_STOP_TRACKING_USED = "is_stop_tracking_used";
    public static final String LOG_TAG = "AppsFlyer_4.8.11";
    public static final String PRE_INSTALL_SYSTEM_DEFAULT = "/data/local/tmp/pre_install.appsflyer";
    public static final String PRE_INSTALL_SYSTEM_DEFAULT_ETC = "/etc/pre_install.appsflyer";
    public static final String PRE_INSTALL_SYSTEM_RO_PROP = "ro.appsflyer.preinstall.path";

    /* renamed from: ʼ */
    private static String f142 = new StringBuilder("https://t.%s/api/v").append(f143).toString();

    /* renamed from: ʽ */
    private static final String f143 = new StringBuilder().append(f150).append("/androidevent?buildnumber=4.8.11&app_id=").toString();

    /* renamed from: ˈ */
    private static AppsFlyerLib f144 = new AppsFlyerLib();

    /* renamed from: ˊॱ */
    private static final List<String> f145 = Arrays.asList(new String[]{"is_cache"});
    /* access modifiers changed from: private */

    /* renamed from: ˋॱ */
    public static final List<String> f146 = Arrays.asList(new String[]{"googleplay", "playstore", "googleplaystore"});

    /* renamed from: ˎ */
    static final String f147 = new StringBuilder("https://register.%s/api/v").append(f143).toString();

    /* renamed from: ˏ */
    static AppsFlyerInAppPurchaseValidatorListener f148 = null;
    /* access modifiers changed from: private */

    /* renamed from: ˏॱ */
    public static AppsFlyerConversionListener f149 = null;

    /* renamed from: ॱ */
    static final String f150 = BuildConfig.AF_SDK_VERSION.substring(0, BuildConfig.AF_SDK_VERSION.indexOf(AbstractIntegrationSupport.DEFAULT_EVENT_NAME_DELIMITER));

    /* renamed from: ॱॱ */
    private static String f151 = new StringBuilder("https://attr.%s/api/v").append(f143).toString();

    /* renamed from: ᐝ */
    private static String f152 = new StringBuilder("https://events.%s/api/v").append(f143).toString();

    /* renamed from: ʻ */
    private long f153 = -1;
    /* access modifiers changed from: private */

    /* renamed from: ʻॱ */
    public Map<String, String> f154;

    /* renamed from: ʼॱ */
    private C0457a f155;

    /* renamed from: ʽॱ */
    private long f156;

    /* renamed from: ʾ */
    private long f157;

    /* renamed from: ʿ */
    private Uri f158 = null;

    /* renamed from: ˉ */
    private boolean f159 = false;

    /* renamed from: ˊ */
    String f160;

    /* renamed from: ˊˊ */
    private long f161;

    /* renamed from: ˊˋ */
    private Map<Long, String> f162;

    /* renamed from: ˊᐝ */
    private boolean f163 = false;

    /* renamed from: ˋ */
    String f164;

    /* renamed from: ˋˊ */
    private String f165;

    /* renamed from: ˋˋ */
    private boolean f166 = false;

    /* renamed from: ˋᐝ */
    private boolean f167 = false;

    /* renamed from: ˌ */
    private C0464t f168 = new C0464t();

    /* renamed from: ˍ */
    private boolean f169;

    /* renamed from: ˎˎ */
    private boolean f170;

    /* renamed from: ͺ */
    private long f171 = -1;

    /* renamed from: ॱˊ */
    private long f172 = TimeUnit.SECONDS.toMillis(5);
    /* access modifiers changed from: private */

    /* renamed from: ॱˋ */
    public ScheduledExecutorService f173 = null;

    /* renamed from: ॱˎ */
    private C0433e f174 = null;
    /* access modifiers changed from: private */

    /* renamed from: ॱᐝ */
    public boolean f175 = false;
    /* access modifiers changed from: private */

    /* renamed from: ᐝॱ */
    public long f176;

    /* renamed from: com.appsflyer.AppsFlyerLib$4 */
    static /* synthetic */ class C04164 {

        /* renamed from: ॱ */
        static final /* synthetic */ int[] f183 = new int[EmailsCryptType.values().length];

        static {
            try {
                f183[EmailsCryptType.SHA1.ordinal()] = 1;
            } catch (NoSuchFieldError e) {
            }
            try {
                f183[EmailsCryptType.MD5.ordinal()] = 2;
            } catch (NoSuchFieldError e2) {
            }
            try {
                f183[EmailsCryptType.SHA256.ordinal()] = 3;
            } catch (NoSuchFieldError e3) {
            }
            try {
                f183[EmailsCryptType.NONE.ordinal()] = 4;
            } catch (NoSuchFieldError e4) {
            }
        }
    }

    /* renamed from: com.appsflyer.AppsFlyerLib$a */
    class C0417a extends C0420d {
        public C0417a(Context context, String str, ScheduledExecutorService scheduledExecutorService) {
            super(context, str, scheduledExecutorService);
        }

        /* renamed from: ॱ */
        public final String mo6494() {
            return ServerConfigHandler.getUrl("https://api.%s/install_data/v3/");
        }

        /* access modifiers changed from: protected */
        /* renamed from: ˊ */
        public final void mo6493(Map<String, String> map) {
            map.put("is_first_launch", Boolean.toString(true));
            AppsFlyerLib.f149.onInstallConversionDataLoaded(map);
            AppsFlyerLib.m214((Context) this.f198.get(), "appsflyerConversionDataRequestRetries", 0);
        }

        /* access modifiers changed from: protected */
        /* renamed from: ˊ */
        public final void mo6492(String str, int i) {
            AppsFlyerLib.f149.onInstallConversionFailure(str);
            if (i >= 400 && i < 500) {
                AppsFlyerLib.m214((Context) this.f198.get(), "appsflyerConversionDataRequestRetries", AppsFlyerLib.m220((Context) this.f198.get()).getInt("appsflyerConversionDataRequestRetries", 0) + 1);
            }
        }
    }

    /* renamed from: com.appsflyer.AppsFlyerLib$b */
    class C0418b implements Runnable {

        /* renamed from: ʻ */
        private boolean f185;

        /* renamed from: ʼ */
        private ExecutorService f186;

        /* renamed from: ʽ */
        private String f187;

        /* renamed from: ˊ */
        private String f188;

        /* renamed from: ˋ */
        private String f189;

        /* renamed from: ˎ */
        private String f190;

        /* renamed from: ˏ */
        private WeakReference<Context> f191;

        /* renamed from: ॱ */
        private final Intent f192;

        /* renamed from: ॱॱ */
        private boolean f193;

        /* synthetic */ C0418b(AppsFlyerLib appsFlyerLib, WeakReference weakReference, String str, String str2, String str3, String str4, ExecutorService executorService, boolean z, Intent intent, byte b) {
            this(weakReference, str, str2, str3, str4, executorService, z, intent);
        }

        /* JADX WARNING: type inference failed for: r8v0, types: [boolean, java.util.concurrent.ExecutorService] */
        /* JADX WARNING: Incorrect type for immutable var: ssa=boolean, code=null, for r8v0, types: [boolean, java.util.concurrent.ExecutorService] */
        /* JADX WARNING: Unknown variable types count: 1 */
        /* Code decompiled incorrectly, please refer to instructions dump. */
        private C0418b(java.lang.ref.WeakReference<android.content.Context> r3, java.lang.String r4, java.lang.String r5, java.lang.String r6, java.lang.String r7, boolean r8, boolean r9, android.content.Intent r10) {
            /*
                r1 = this;
                com.appsflyer.AppsFlyerLib.this = r2
                r1.<init>()
                r1.f191 = r3
                r1.f188 = r4
                r1.f189 = r5
                r1.f190 = r6
                r1.f187 = r7
                r0 = 1
                r1.f193 = r0
                r1.f186 = r8
                r1.f185 = r9
                r1.f192 = r10
                return
            */
            throw new UnsupportedOperationException("Method not decompiled: com.appsflyer.AppsFlyerLib.C0418b.<init>(com.appsflyer.AppsFlyerLib, java.lang.ref.WeakReference, java.lang.String, java.lang.String, java.lang.String, java.lang.String, java.util.concurrent.ExecutorService, boolean, android.content.Intent):void");
        }

        public final void run() {
            AppsFlyerLib.m215(AppsFlyerLib.this, (Context) this.f191.get(), this.f188, this.f189, this.f190, this.f187, this.f193, this.f185, this.f192);
        }
    }

    /* renamed from: com.appsflyer.AppsFlyerLib$c */
    class C0419c implements Runnable {

        /* renamed from: ˏ */
        private WeakReference<Context> f196 = null;

        public C0419c(Context context) {
            this.f196 = new WeakReference<>(context);
        }

        public final void run() {
            if (!AppsFlyerLib.this.f175) {
                AppsFlyerLib.this.f176 = System.currentTimeMillis();
                if (this.f196 != null) {
                    AppsFlyerLib.this.f175 = true;
                    try {
                        String r5 = AppsFlyerProperties.getInstance().getString(AppsFlyerProperties.AF_KEY);
                        synchronized (this.f196) {
                            for (RequestCacheData requestCacheData : CacheManager.getInstance().getCachedRequests((Context) this.f196.get())) {
                                AFLogger.afInfoLog(new StringBuilder("resending request: ").append(requestCacheData.getRequestURL()).toString());
                                try {
                                    AppsFlyerLib.m216(AppsFlyerLib.this, new StringBuilder().append(requestCacheData.getRequestURL()).append("&isCachedRequest=true&timeincache=").append(Long.toString((System.currentTimeMillis() - Long.parseLong(requestCacheData.getCacheKey(), 10)) / 1000)).toString(), requestCacheData.getPostData(), r5, this.f196, requestCacheData.getCacheKey(), false);
                                } catch (Exception e) {
                                    AFLogger.afErrorLog("Failed to resend cached request", e);
                                }
                            }
                        }
                        AppsFlyerLib.this.f175 = false;
                    } catch (Exception e2) {
                        try {
                            AFLogger.afErrorLog("failed to check cache. ", e2);
                        } finally {
                            AppsFlyerLib.this.f175 = false;
                        }
                    }
                    AppsFlyerLib.this.f173.shutdown();
                    AppsFlyerLib.this.f173 = null;
                }
            }
        }
    }

    /* renamed from: com.appsflyer.AppsFlyerLib$d */
    abstract class C0420d implements Runnable {

        /* renamed from: ˊ */
        private String f197;

        /* renamed from: ˋ */
        WeakReference<Context> f198 = null;

        /* renamed from: ˎ */
        private ScheduledExecutorService f199;

        /* renamed from: ˏ */
        private AtomicInteger f200 = new AtomicInteger(0);

        /* access modifiers changed from: protected */
        /* renamed from: ˊ */
        public abstract void mo6492(String str, int i);

        /* access modifiers changed from: protected */
        /* renamed from: ˊ */
        public abstract void mo6493(Map<String, String> map);

        /* renamed from: ॱ */
        public abstract String mo6494();

        C0420d(Context context, String str, ScheduledExecutorService scheduledExecutorService) {
            this.f198 = new WeakReference<>(context);
            this.f197 = str;
            if (scheduledExecutorService == null) {
                this.f199 = AFExecutor.getInstance().mo6409();
            } else {
                this.f199 = scheduledExecutorService;
            }
        }

        /* JADX WARNING: Removed duplicated region for block: B:75:0x0231  */
        /* Code decompiled incorrectly, please refer to instructions dump. */
        public void run() {
            /*
                r11 = this;
                r10 = 1
                java.lang.String r0 = r11.f197
                if (r0 == 0) goto L_0x000d
                java.lang.String r0 = r11.f197
                int r0 = r0.length()
                if (r0 != 0) goto L_0x000e
            L_0x000d:
                return
            L_0x000e:
                com.appsflyer.AppsFlyerLib r0 = com.appsflyer.AppsFlyerLib.this
                boolean r0 = r0.isTrackingStopped()
                if (r0 != 0) goto L_0x000d
                java.util.concurrent.atomic.AtomicInteger r0 = r11.f200
                r0.incrementAndGet()
                r2 = 0
                java.lang.ref.WeakReference<android.content.Context> r0 = r11.f198     // Catch:{ Throwable -> 0x01e7, all -> 0x026e }
                java.lang.Object r0 = r0.get()     // Catch:{ Throwable -> 0x01e7, all -> 0x026e }
                android.content.Context r0 = (android.content.Context) r0     // Catch:{ Throwable -> 0x01e7, all -> 0x026e }
                if (r0 != 0) goto L_0x002c
                java.util.concurrent.atomic.AtomicInteger r0 = r11.f200
                r0.decrementAndGet()
                goto L_0x000d
            L_0x002c:
                long r4 = java.lang.System.currentTimeMillis()     // Catch:{ Throwable -> 0x01e7, all -> 0x026e }
                java.lang.ref.WeakReference r1 = new java.lang.ref.WeakReference     // Catch:{ Throwable -> 0x01e7, all -> 0x026e }
                r1.<init>(r0)     // Catch:{ Throwable -> 0x01e7, all -> 0x026e }
                java.lang.String r1 = com.appsflyer.AppsFlyerLib.m203(r1)     // Catch:{ Throwable -> 0x01e7, all -> 0x026e }
                java.lang.String r3 = com.appsflyer.AppsFlyerLib.m189(r0, r1)     // Catch:{ Throwable -> 0x01e7, all -> 0x026e }
                java.lang.String r1 = ""
                if (r3 == 0) goto L_0x0059
                java.util.List r6 = com.appsflyer.AppsFlyerLib.f146     // Catch:{ Throwable -> 0x01e7, all -> 0x026e }
                java.lang.String r7 = r3.toLowerCase()     // Catch:{ Throwable -> 0x01e7, all -> 0x026e }
                boolean r6 = r6.contains(r7)     // Catch:{ Throwable -> 0x01e7, all -> 0x026e }
                if (r6 != 0) goto L_0x01d6
                java.lang.String r1 = "-"
                java.lang.String r3 = java.lang.String.valueOf(r3)     // Catch:{ Throwable -> 0x01e7, all -> 0x026e }
                java.lang.String r1 = r1.concat(r3)     // Catch:{ Throwable -> 0x01e7, all -> 0x026e }
            L_0x0059:
                java.lang.StringBuilder r3 = new java.lang.StringBuilder     // Catch:{ Throwable -> 0x01e7, all -> 0x026e }
                r3.<init>()     // Catch:{ Throwable -> 0x01e7, all -> 0x026e }
                java.lang.String r6 = r11.mo6494()     // Catch:{ Throwable -> 0x01e7, all -> 0x026e }
                java.lang.StringBuilder r3 = r3.append(r6)     // Catch:{ Throwable -> 0x01e7, all -> 0x026e }
                java.lang.String r6 = r0.getPackageName()     // Catch:{ Throwable -> 0x01e7, all -> 0x026e }
                java.lang.StringBuilder r3 = r3.append(r6)     // Catch:{ Throwable -> 0x01e7, all -> 0x026e }
                java.lang.StringBuilder r1 = r3.append(r1)     // Catch:{ Throwable -> 0x01e7, all -> 0x026e }
                java.lang.String r3 = "?devkey="
                java.lang.StringBuilder r1 = r1.append(r3)     // Catch:{ Throwable -> 0x01e7, all -> 0x026e }
                java.lang.String r3 = r11.f197     // Catch:{ Throwable -> 0x01e7, all -> 0x026e }
                java.lang.StringBuilder r1 = r1.append(r3)     // Catch:{ Throwable -> 0x01e7, all -> 0x026e }
                java.lang.String r3 = "&device_id="
                java.lang.StringBuilder r1 = r1.append(r3)     // Catch:{ Throwable -> 0x01e7, all -> 0x026e }
                java.lang.ref.WeakReference r3 = new java.lang.ref.WeakReference     // Catch:{ Throwable -> 0x01e7, all -> 0x026e }
                r3.<init>(r0)     // Catch:{ Throwable -> 0x01e7, all -> 0x026e }
                java.lang.String r3 = com.appsflyer.C0455p.m326(r3)     // Catch:{ Throwable -> 0x01e7, all -> 0x026e }
                java.lang.StringBuilder r3 = r1.append(r3)     // Catch:{ Throwable -> 0x01e7, all -> 0x026e }
                com.appsflyer.y r1 = com.appsflyer.C0469y.m373()     // Catch:{ Throwable -> 0x01e7, all -> 0x026e }
                java.lang.String r6 = r3.toString()     // Catch:{ Throwable -> 0x01e7, all -> 0x026e }
                java.lang.String r7 = ""
                r1.mo6645(r6, r7)     // Catch:{ Throwable -> 0x01e7, all -> 0x026e }
                java.lang.StringBuilder r1 = new java.lang.StringBuilder     // Catch:{ Throwable -> 0x01e7, all -> 0x026e }
                java.lang.String r6 = "Calling server for attribution url: "
                r1.<init>(r6)     // Catch:{ Throwable -> 0x01e7, all -> 0x026e }
                java.lang.String r6 = r3.toString()     // Catch:{ Throwable -> 0x01e7, all -> 0x026e }
                java.lang.StringBuilder r1 = r1.append(r6)     // Catch:{ Throwable -> 0x01e7, all -> 0x026e }
                java.lang.String r1 = r1.toString()     // Catch:{ Throwable -> 0x01e7, all -> 0x026e }
                com.appsflyer.C0434f.C04375.m289(r1)     // Catch:{ Throwable -> 0x01e7, all -> 0x026e }
                java.net.URL r1 = new java.net.URL     // Catch:{ Throwable -> 0x01e7, all -> 0x026e }
                java.lang.String r6 = r3.toString()     // Catch:{ Throwable -> 0x01e7, all -> 0x026e }
                r1.<init>(r6)     // Catch:{ Throwable -> 0x01e7, all -> 0x026e }
                java.net.URLConnection r1 = r1.openConnection()     // Catch:{ Throwable -> 0x01e7, all -> 0x026e }
                java.net.HttpURLConnection r1 = (java.net.HttpURLConnection) r1     // Catch:{ Throwable -> 0x01e7, all -> 0x026e }
                java.lang.String r2 = "GET"
                r1.setRequestMethod(r2)     // Catch:{ Throwable -> 0x0220 }
                r2 = 10000(0x2710, float:1.4013E-41)
                r1.setConnectTimeout(r2)     // Catch:{ Throwable -> 0x0220 }
                java.lang.String r2 = "Connection"
                java.lang.String r6 = "close"
                r1.setRequestProperty(r2, r6)     // Catch:{ Throwable -> 0x0220 }
                r1.connect()     // Catch:{ Throwable -> 0x0220 }
                int r2 = r1.getResponseCode()     // Catch:{ Throwable -> 0x0220 }
                java.lang.String r6 = com.appsflyer.AppsFlyerLib.m211(r1)     // Catch:{ Throwable -> 0x0220 }
                com.appsflyer.y r7 = com.appsflyer.C0469y.m373()     // Catch:{ Throwable -> 0x0220 }
                java.lang.String r8 = r3.toString()     // Catch:{ Throwable -> 0x0220 }
                r7.mo6644(r8, r2, r6)     // Catch:{ Throwable -> 0x0220 }
                r7 = 200(0xc8, float:2.8E-43)
                if (r2 != r7) goto L_0x023d
                long r2 = java.lang.System.currentTimeMillis()     // Catch:{ Throwable -> 0x0220 }
                java.lang.String r7 = "appsflyerGetConversionDataTiming"
                long r2 = r2 - r4
                r4 = 1000(0x3e8, double:4.94E-321)
                long r2 = r2 / r4
                com.appsflyer.AppsFlyerLib.m196(r0, r7, r2)     // Catch:{ Throwable -> 0x0220 }
                java.lang.String r2 = "Attribution data: "
                java.lang.String r3 = java.lang.String.valueOf(r6)     // Catch:{ Throwable -> 0x0220 }
                java.lang.String r2 = r2.concat(r3)     // Catch:{ Throwable -> 0x0220 }
                com.appsflyer.C0434f.C04375.m289(r2)     // Catch:{ Throwable -> 0x0220 }
                int r2 = r6.length()     // Catch:{ Throwable -> 0x0220 }
                if (r2 <= 0) goto L_0x01c5
                if (r0 == 0) goto L_0x01c5
                java.util.Map r4 = com.appsflyer.AppsFlyerLib.m230(r6)     // Catch:{ Throwable -> 0x0220 }
                java.lang.String r2 = "iscache"
                java.lang.Object r2 = r4.get(r2)     // Catch:{ Throwable -> 0x0220 }
                java.lang.String r2 = (java.lang.String) r2     // Catch:{ Throwable -> 0x0220 }
                if (r2 == 0) goto L_0x0132
                r3 = 0
                java.lang.String r3 = java.lang.Boolean.toString(r3)     // Catch:{ Throwable -> 0x0220 }
                boolean r3 = r3.equals(r2)     // Catch:{ Throwable -> 0x0220 }
                if (r3 == 0) goto L_0x0132
                java.lang.String r3 = "appsflyerConversionDataCacheExpiration"
                long r8 = java.lang.System.currentTimeMillis()     // Catch:{ Throwable -> 0x0220 }
                com.appsflyer.AppsFlyerLib.m196(r0, r3, r8)     // Catch:{ Throwable -> 0x0220 }
            L_0x0132:
                java.lang.String r3 = "af_siteid"
                boolean r3 = r4.containsKey(r3)     // Catch:{ Throwable -> 0x0220 }
                if (r3 == 0) goto L_0x015c
                java.lang.String r3 = "af_channel"
                boolean r3 = r4.containsKey(r3)     // Catch:{ Throwable -> 0x0220 }
                if (r3 == 0) goto L_0x0209
                java.lang.StringBuilder r5 = new java.lang.StringBuilder     // Catch:{ Throwable -> 0x0220 }
                java.lang.String r3 = "[Invite] Detected App-Invite via channel: "
                r5.<init>(r3)     // Catch:{ Throwable -> 0x0220 }
                java.lang.String r3 = "af_channel"
                java.lang.Object r3 = r4.get(r3)     // Catch:{ Throwable -> 0x0220 }
                java.lang.String r3 = (java.lang.String) r3     // Catch:{ Throwable -> 0x0220 }
                java.lang.StringBuilder r3 = r5.append(r3)     // Catch:{ Throwable -> 0x0220 }
                java.lang.String r3 = r3.toString()     // Catch:{ Throwable -> 0x0220 }
                com.appsflyer.AFLogger.afDebugLog(r3)     // Catch:{ Throwable -> 0x0220 }
            L_0x015c:
                java.lang.String r3 = "af_siteid"
                boolean r3 = r4.containsKey(r3)     // Catch:{ Throwable -> 0x0220 }
                if (r3 == 0) goto L_0x017e
                java.lang.StringBuilder r5 = new java.lang.StringBuilder     // Catch:{ Throwable -> 0x0220 }
                java.lang.String r3 = "[Invite] Detected App-Invite via channel: "
                r5.<init>(r3)     // Catch:{ Throwable -> 0x0220 }
                java.lang.String r3 = "af_channel"
                java.lang.Object r3 = r4.get(r3)     // Catch:{ Throwable -> 0x0220 }
                java.lang.String r3 = (java.lang.String) r3     // Catch:{ Throwable -> 0x0220 }
                java.lang.StringBuilder r3 = r5.append(r3)     // Catch:{ Throwable -> 0x0220 }
                java.lang.String r3 = r3.toString()     // Catch:{ Throwable -> 0x0220 }
                com.appsflyer.AFLogger.afDebugLog(r3)     // Catch:{ Throwable -> 0x0220 }
            L_0x017e:
                java.lang.String r3 = "is_first_launch"
                r5 = 0
                java.lang.String r5 = java.lang.Boolean.toString(r5)     // Catch:{ Throwable -> 0x0220 }
                r4.put(r3, r5)     // Catch:{ Throwable -> 0x0220 }
                org.json.JSONObject r3 = new org.json.JSONObject     // Catch:{ Throwable -> 0x0220 }
                r3.<init>(r4)     // Catch:{ Throwable -> 0x0220 }
                java.lang.String r3 = r3.toString()     // Catch:{ Throwable -> 0x0220 }
                if (r3 == 0) goto L_0x0222
                java.lang.String r5 = "attributionId"
                com.appsflyer.AppsFlyerLib.m232(r0, r5, r3)     // Catch:{ Throwable -> 0x0220 }
            L_0x0198:
                java.lang.StringBuilder r3 = new java.lang.StringBuilder     // Catch:{ Throwable -> 0x0220 }
                java.lang.String r5 = "iscache="
                r3.<init>(r5)     // Catch:{ Throwable -> 0x0220 }
                java.lang.StringBuilder r2 = r3.append(r2)     // Catch:{ Throwable -> 0x0220 }
                java.lang.String r3 = " caching conversion data"
                java.lang.StringBuilder r2 = r2.append(r3)     // Catch:{ Throwable -> 0x0220 }
                java.lang.String r2 = r2.toString()     // Catch:{ Throwable -> 0x0220 }
                com.appsflyer.AFLogger.afDebugLog(r2)     // Catch:{ Throwable -> 0x0220 }
                com.appsflyer.AppsFlyerConversionListener r2 = com.appsflyer.AppsFlyerLib.f149     // Catch:{ Throwable -> 0x0220 }
                if (r2 == 0) goto L_0x01c5
                java.util.concurrent.atomic.AtomicInteger r2 = r11.f200     // Catch:{ Throwable -> 0x0220 }
                int r2 = r2.intValue()     // Catch:{ Throwable -> 0x0220 }
                if (r2 > r10) goto L_0x01c5
                java.util.Map r0 = com.appsflyer.AppsFlyerLib.m192(r0)     // Catch:{ k -> 0x0235 }
            L_0x01c2:
                r11.mo6493(r0)     // Catch:{ Throwable -> 0x0220 }
            L_0x01c5:
                java.util.concurrent.atomic.AtomicInteger r0 = r11.f200
                r0.decrementAndGet()
                if (r1 == 0) goto L_0x01cf
                r1.disconnect()
            L_0x01cf:
                java.util.concurrent.ScheduledExecutorService r0 = r11.f199
                r0.shutdown()
                goto L_0x000d
            L_0x01d6:
                java.lang.String r6 = "AF detected using redundant Google-Play channel for attribution - %s. Using without channel postfix."
                r7 = 1
                java.lang.Object[] r7 = new java.lang.Object[r7]     // Catch:{ Throwable -> 0x01e7, all -> 0x026e }
                r8 = 0
                r7[r8] = r3     // Catch:{ Throwable -> 0x01e7, all -> 0x026e }
                java.lang.String r3 = java.lang.String.format(r6, r7)     // Catch:{ Throwable -> 0x01e7, all -> 0x026e }
                com.appsflyer.AFLogger.afWarnLog(r3)     // Catch:{ Throwable -> 0x01e7, all -> 0x026e }
                goto L_0x0059
            L_0x01e7:
                r0 = move-exception
                r1 = r2
            L_0x01e9:
                com.appsflyer.AppsFlyerConversionListener r2 = com.appsflyer.AppsFlyerLib.f149     // Catch:{ all -> 0x0229 }
                if (r2 == 0) goto L_0x01f7
                java.lang.String r2 = r0.getMessage()     // Catch:{ all -> 0x0229 }
                r3 = 0
                r11.mo6492(r2, r3)     // Catch:{ all -> 0x0229 }
            L_0x01f7:
                java.lang.String r2 = r0.getMessage()     // Catch:{ all -> 0x0229 }
                com.appsflyer.AFLogger.afErrorLog(r2, r0)     // Catch:{ all -> 0x0229 }
                java.util.concurrent.atomic.AtomicInteger r0 = r11.f200
                r0.decrementAndGet()
                if (r1 == 0) goto L_0x01cf
                r1.disconnect()
                goto L_0x01cf
            L_0x0209:
                java.lang.String r3 = "[CrossPromotion] App was installed via %s's Cross Promotion"
                r5 = 1
                java.lang.Object[] r5 = new java.lang.Object[r5]     // Catch:{ Throwable -> 0x0220 }
                r7 = 0
                java.lang.String r8 = "af_siteid"
                java.lang.Object r8 = r4.get(r8)     // Catch:{ Throwable -> 0x0220 }
                r5[r7] = r8     // Catch:{ Throwable -> 0x0220 }
                java.lang.String r3 = java.lang.String.format(r3, r5)     // Catch:{ Throwable -> 0x0220 }
                com.appsflyer.AFLogger.afDebugLog(r3)     // Catch:{ Throwable -> 0x0220 }
                goto L_0x015c
            L_0x0220:
                r0 = move-exception
                goto L_0x01e9
            L_0x0222:
                java.lang.String r3 = "attributionId"
                com.appsflyer.AppsFlyerLib.m232(r0, r3, r6)     // Catch:{ Throwable -> 0x0220 }
                goto L_0x0198
            L_0x0229:
                r0 = move-exception
            L_0x022a:
                java.util.concurrent.atomic.AtomicInteger r2 = r11.f200
                r2.decrementAndGet()
                if (r1 == 0) goto L_0x0234
                r1.disconnect()
            L_0x0234:
                throw r0
            L_0x0235:
                r0 = move-exception
                java.lang.String r2 = "Exception while trying to fetch attribution data. "
                com.appsflyer.AFLogger.afErrorLog(r2, r0)     // Catch:{ Throwable -> 0x0220 }
                r0 = r4
                goto L_0x01c2
            L_0x023d:
                com.appsflyer.AppsFlyerConversionListener r0 = com.appsflyer.AppsFlyerLib.f149     // Catch:{ Throwable -> 0x0220 }
                if (r0 == 0) goto L_0x0250
                java.lang.String r0 = "Error connection to server: "
                java.lang.String r4 = java.lang.String.valueOf(r2)     // Catch:{ Throwable -> 0x0220 }
                java.lang.String r0 = r0.concat(r4)     // Catch:{ Throwable -> 0x0220 }
                r11.mo6492(r0, r2)     // Catch:{ Throwable -> 0x0220 }
            L_0x0250:
                java.lang.StringBuilder r0 = new java.lang.StringBuilder     // Catch:{ Throwable -> 0x0220 }
                java.lang.String r4 = "AttributionIdFetcher response code: "
                r0.<init>(r4)     // Catch:{ Throwable -> 0x0220 }
                java.lang.StringBuilder r0 = r0.append(r2)     // Catch:{ Throwable -> 0x0220 }
                java.lang.String r2 = "  url: "
                java.lang.StringBuilder r0 = r0.append(r2)     // Catch:{ Throwable -> 0x0220 }
                java.lang.StringBuilder r0 = r0.append(r3)     // Catch:{ Throwable -> 0x0220 }
                java.lang.String r0 = r0.toString()     // Catch:{ Throwable -> 0x0220 }
                com.appsflyer.C0434f.C04375.m289(r0)     // Catch:{ Throwable -> 0x0220 }
                goto L_0x01c5
            L_0x026e:
                r0 = move-exception
                r1 = r2
                goto L_0x022a
            */
            throw new UnsupportedOperationException("Method not decompiled: com.appsflyer.AppsFlyerLib.C0420d.run():void");
        }
    }

    /* renamed from: com.appsflyer.AppsFlyerLib$e */
    class C0421e implements Runnable {

        /* renamed from: ˊ */
        private int f202;

        /* renamed from: ˋ */
        private boolean f203;

        /* renamed from: ˎ */
        private String f204;

        /* renamed from: ˏ */
        private WeakReference<Context> f205;

        /* renamed from: ॱ */
        private Map<String, Object> f206;

        /* synthetic */ C0421e(AppsFlyerLib appsFlyerLib, String str, Map map, Context context, boolean z, int i, byte b) {
            this(str, map, context, z, i);
        }

        private C0421e(String str, Map<String, Object> map, Context context, boolean z, int i) {
            this.f205 = null;
            this.f204 = str;
            this.f206 = map;
            this.f205 = new WeakReference<>(context);
            this.f203 = z;
            this.f202 = i;
        }

        public final void run() {
            String str = null;
            if (!AppsFlyerLib.this.isTrackingStopped()) {
                if (this.f203 && this.f202 <= 2 && AppsFlyerLib.m227(AppsFlyerLib.this)) {
                    this.f206.put("rfr", AppsFlyerLib.this.f154);
                }
                try {
                    String str2 = (String) this.f206.get("appsflyerKey");
                    str = AFHelper.convertToJsonObject(this.f206).toString();
                    AppsFlyerLib.m216(AppsFlyerLib.this, this.f204, str, str2, this.f205, null, this.f203);
                } catch (IOException e) {
                    IOException iOException = e;
                    AFLogger.afErrorLog("Exception while sending request to server. ", iOException);
                    if (str != null && this.f205 != null && !this.f204.contains("&isCachedRequest=true&timeincache=")) {
                        CacheManager.getInstance().cacheRequest(new RequestCacheData(this.f204, str, BuildConfig.AF_SDK_VERSION), (Context) this.f205.get());
                        AFLogger.afErrorLog(iOException.getMessage(), iOException);
                    }
                } catch (Throwable th) {
                    AFLogger.afErrorLog(th.getMessage(), th);
                }
            }
        }
    }

    /* access modifiers changed from: private */
    /* renamed from: ˊ */
    public static String m189(Context context, String str) throws NameNotFoundException {
        SharedPreferences sharedPreferences = context.getSharedPreferences("appsflyer-data", 0);
        if (sharedPreferences.contains("CACHED_CHANNEL")) {
            return sharedPreferences.getString("CACHED_CHANNEL", null);
        }
        m232(context, "CACHED_CHANNEL", str);
        return str;
    }

    /* renamed from: ˋ */
    static /* synthetic */ String m203(WeakReference weakReference) {
        String string = AppsFlyerProperties.getInstance().getString(AppsFlyerProperties.CHANNEL);
        return string == null ? m204(weakReference, "CHANNEL") : string;
    }

    /* renamed from: ˎ */
    static /* synthetic */ void m215(AppsFlyerLib appsFlyerLib, Context context, String str, String str2, String str3, String str4, boolean z, boolean z2, Intent intent) {
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
        Map r6 = appsFlyerLib.mo6483(context, str, str2, str3, str4, z, sharedPreferences, z3, intent);
        String str5 = (String) r6.get("appsflyerKey");
        if (str5 == null || str5.length() == 0) {
            AFLogger.afDebugLog("Not sending data yet, waiting for dev key");
            return;
        }
        if (!appsFlyerLib.isTrackingStopped()) {
            AFLogger.afInfoLog("AppsFlyerLib.sendTrackingWithEvent");
        }
        String url = z3 ? z2 ? ServerConfigHandler.getUrl(f151) : ServerConfigHandler.getUrl(f142) : ServerConfigHandler.getUrl(f152);
        C0421e eVar = new C0421e(appsFlyerLib, new StringBuilder().append(url).append(context.getPackageName()).toString(), r6, context.getApplicationContext(), z3, m188(sharedPreferences, "appsFlyerCount", false), 0);
        if (z3 && m235(context)) {
            if (!(appsFlyerLib.f154 != null && appsFlyerLib.f154.size() > 0)) {
                AFLogger.afDebugLog("Failed to get new referrer, wait ...");
                m217(AFExecutor.getInstance().mo6409(), eVar, 500, TimeUnit.MILLISECONDS);
                return;
            }
        }
        eVar.run();
    }

    /* renamed from: ˎ */
    static /* synthetic */ void m216(AppsFlyerLib appsFlyerLib, String str, String str2, String str3, WeakReference weakReference, String str4, boolean z) throws IOException {
        URL url = new URL(str);
        AFLogger.afInfoLog(new StringBuilder("url: ").append(url.toString()).toString());
        C04375.m289("data: ".concat(String.valueOf(str2)));
        m207((Context) weakReference.get(), LOG_TAG, "EVENT_DATA", str2);
        try {
            appsFlyerLib.m198(url, str2, str3, weakReference, str4, z);
        } catch (IOException e) {
            AFLogger.afErrorLog("Exception in sendRequestToServer. ", e);
            if (AppsFlyerProperties.getInstance().getBoolean(AppsFlyerProperties.USE_HTTP_FALLBACK, false)) {
                appsFlyerLib.m198(new URL(str.replace("https:", "http:")), str2, str3, weakReference, str4, z);
                return;
            }
            AFLogger.afInfoLog(new StringBuilder("failed to send requeset to server. ").append(e.getLocalizedMessage()).toString());
            m207((Context) weakReference.get(), LOG_TAG, "ERROR", e.getLocalizedMessage());
            throw e;
        }
    }

    /* renamed from: ˏ */
    static /* synthetic */ boolean m227(AppsFlyerLib appsFlyerLib) {
        return appsFlyerLib.f154 != null && appsFlyerLib.f154.size() > 0;
    }

    /* renamed from: ॱ */
    private static void m233(Context context, Map<String, ? super String> map) {
        C0438g gVar = C0440c.f270;
        C0439a r0 = C0438g.m290(context);
        map.put("network", r0.mo6570());
        if (r0.mo6569() != null) {
            map.put("operator", r0.mo6569());
        }
        if (r0.mo6568() != null) {
            map.put("carrier", r0.mo6568());
        }
    }

    /* renamed from: ॱ */
    static /* synthetic */ void m234(Map map) {
        if (f149 != null) {
            try {
                f149.onAppOpenAttribution(map);
            } catch (Throwable th) {
                AFLogger.afErrorLog(th.getLocalizedMessage(), th);
            }
        }
    }

    /* renamed from: ॱ */
    private static boolean m235(@NonNull Context context) {
        if (m188(context.getSharedPreferences("appsflyer-data", 0), "appsFlyerCount", false) > 2) {
            AFLogger.afRDLog("Install referrer will not load, the counter > 2, ");
            return false;
        }
        try {
            Class.forName("com.android.installreferrer.api.InstallReferrerClient");
            if (C0439a.m291(context, "com.google.android.finsky.permission.BIND_GET_INSTALL_REFERRER_SERVICE")) {
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

    /* access modifiers changed from: 0000 */
    /* JADX WARNING: Removed duplicated region for block: B:6:0x001d  */
    /* JADX WARNING: Removed duplicated region for block: B:8:0x0025  */
    /* renamed from: ˋ */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    public final void mo6482(android.content.Context r10, java.lang.String r11) {
        /*
            r9 = this;
            r1 = 1
            r2 = 0
            java.lang.String r0 = "waitForCustomerId"
            com.appsflyer.AppsFlyerProperties r3 = com.appsflyer.AppsFlyerProperties.getInstance()
            boolean r0 = r3.getBoolean(r0, r2)
            if (r0 == 0) goto L_0x0023
            java.lang.String r0 = "AppUserId"
            com.appsflyer.AppsFlyerProperties r3 = com.appsflyer.AppsFlyerProperties.getInstance()
            java.lang.String r0 = r3.getString(r0)
            if (r0 != 0) goto L_0x0023
            r0 = r1
        L_0x001b:
            if (r0 == 0) goto L_0x0025
            java.lang.String r0 = "CustomerUserId not set, Tracking is disabled"
            com.appsflyer.AFLogger.afInfoLog(r0, r1)
        L_0x0022:
            return
        L_0x0023:
            r0 = r2
            goto L_0x001b
        L_0x0025:
            java.util.HashMap r1 = new java.util.HashMap
            r1.<init>()
            java.lang.String r0 = "AppsFlyerKey"
            com.appsflyer.AppsFlyerProperties r3 = com.appsflyer.AppsFlyerProperties.getInstance()
            java.lang.String r3 = r3.getString(r0)
            if (r3 != 0) goto L_0x003c
            java.lang.String r0 = "[registerUninstall] AppsFlyer's SDK cannot send any event without providing DevKey."
            com.appsflyer.AFLogger.afWarnLog(r0)
            goto L_0x0022
        L_0x003c:
            android.content.pm.PackageManager r0 = r10.getPackageManager()
            java.lang.String r4 = r10.getPackageName()
            r5 = 0
            android.content.pm.PackageInfo r5 = r0.getPackageInfo(r4, r5)     // Catch:{ Throwable -> 0x017f }
            java.lang.String r6 = "app_version_code"
            int r7 = r5.versionCode     // Catch:{ Throwable -> 0x017f }
            java.lang.String r7 = java.lang.Integer.toString(r7)     // Catch:{ Throwable -> 0x017f }
            r1.put(r6, r7)     // Catch:{ Throwable -> 0x017f }
            java.lang.String r6 = "app_version_name"
            java.lang.String r7 = r5.versionName     // Catch:{ Throwable -> 0x017f }
            r1.put(r6, r7)     // Catch:{ Throwable -> 0x017f }
            android.content.pm.ApplicationInfo r6 = r5.applicationInfo     // Catch:{ Throwable -> 0x017f }
            java.lang.CharSequence r0 = r0.getApplicationLabel(r6)     // Catch:{ Throwable -> 0x017f }
            java.lang.String r0 = r0.toString()     // Catch:{ Throwable -> 0x017f }
            java.lang.String r6 = "app_name"
            r1.put(r6, r0)     // Catch:{ Throwable -> 0x017f }
            long r6 = r5.firstInstallTime     // Catch:{ Throwable -> 0x017f }
            java.lang.String r0 = "yyyy-MM-dd_HHmmssZ"
            java.text.SimpleDateFormat r5 = new java.text.SimpleDateFormat     // Catch:{ Throwable -> 0x017f }
            java.util.Locale r8 = java.util.Locale.US     // Catch:{ Throwable -> 0x017f }
            r5.<init>(r0, r8)     // Catch:{ Throwable -> 0x017f }
            java.lang.String r0 = "installDate"
            java.lang.String r8 = "UTC"
            java.util.TimeZone r8 = java.util.TimeZone.getTimeZone(r8)     // Catch:{ Throwable -> 0x017f }
            r5.setTimeZone(r8)     // Catch:{ Throwable -> 0x017f }
            java.util.Date r8 = new java.util.Date     // Catch:{ Throwable -> 0x017f }
            r8.<init>(r6)     // Catch:{ Throwable -> 0x017f }
            java.lang.String r5 = r5.format(r8)     // Catch:{ Throwable -> 0x017f }
            r1.put(r0, r5)     // Catch:{ Throwable -> 0x017f }
        L_0x008c:
            m233(r10, r1)
            java.lang.String r0 = "AppUserId"
            com.appsflyer.AppsFlyerProperties r5 = com.appsflyer.AppsFlyerProperties.getInstance()
            java.lang.String r0 = r5.getString(r0)
            if (r0 == 0) goto L_0x00a0
            java.lang.String r5 = "appUserId"
            r1.put(r5, r0)
        L_0x00a0:
            java.lang.String r0 = "model"
            java.lang.String r5 = android.os.Build.MODEL     // Catch:{ Throwable -> 0x0187 }
            r1.put(r0, r5)     // Catch:{ Throwable -> 0x0187 }
            java.lang.String r0 = "brand"
            java.lang.String r5 = android.os.Build.BRAND     // Catch:{ Throwable -> 0x0187 }
            r1.put(r0, r5)     // Catch:{ Throwable -> 0x0187 }
        L_0x00ae:
            com.appsflyer.AppsFlyerProperties r0 = com.appsflyer.AppsFlyerProperties.getInstance()
            java.lang.String r5 = "deviceTrackingDisabled"
            boolean r0 = r0.getBoolean(r5, r2)
            if (r0 == 0) goto L_0x00c1
            java.lang.String r0 = "deviceTrackingDisabled"
            java.lang.String r5 = "true"
            r1.put(r0, r5)
        L_0x00c1:
            android.content.ContentResolver r0 = r10.getContentResolver()
            com.appsflyer.n r0 = com.appsflyer.C0454o.m324(r0)
            if (r0 == 0) goto L_0x00e1
            java.lang.String r5 = "amazon_aid"
            java.lang.String r6 = r0.mo6598()
            r1.put(r5, r6)
            java.lang.String r5 = "amazon_aid_limit"
            boolean r0 = r0.mo6597()
            java.lang.String r0 = java.lang.String.valueOf(r0)
            r1.put(r5, r0)
        L_0x00e1:
            com.appsflyer.AppsFlyerProperties r0 = com.appsflyer.AppsFlyerProperties.getInstance()
            java.lang.String r5 = "advertiserId"
            java.lang.String r0 = r0.getString(r5)
            if (r0 == 0) goto L_0x00f2
            java.lang.String r5 = "advertiserId"
            r1.put(r5, r0)
        L_0x00f2:
            java.lang.String r0 = "devkey"
            r1.put(r0, r3)
            java.lang.String r0 = "uid"
            java.lang.ref.WeakReference r3 = new java.lang.ref.WeakReference
            r3.<init>(r10)
            java.lang.String r3 = com.appsflyer.C0455p.m326(r3)
            r1.put(r0, r3)
            java.lang.String r0 = "af_gcm_token"
            r1.put(r0, r11)
            java.lang.String r0 = "appsflyer-data"
            android.content.SharedPreferences r0 = r10.getSharedPreferences(r0, r2)
            java.lang.String r3 = "appsFlyerCount"
            int r0 = m188(r0, r3, r2)
            java.lang.String r2 = "launch_counter"
            java.lang.String r0 = java.lang.Integer.toString(r0)
            r1.put(r2, r0)
            java.lang.String r0 = "sdk"
            int r2 = android.os.Build.VERSION.SDK_INT
            java.lang.String r2 = java.lang.Integer.toString(r2)
            r1.put(r0, r2)
            java.lang.ref.WeakReference r2 = new java.lang.ref.WeakReference
            r2.<init>(r10)
            com.appsflyer.AppsFlyerProperties r0 = com.appsflyer.AppsFlyerProperties.getInstance()
            java.lang.String r3 = "channel"
            java.lang.String r0 = r0.getString(r3)
            if (r0 != 0) goto L_0x0141
            java.lang.String r0 = "CHANNEL"
            java.lang.String r0 = m204(r2, r0)
        L_0x0141:
            if (r0 == 0) goto L_0x0148
            java.lang.String r2 = "channel"
            r1.put(r2, r0)
        L_0x0148:
            com.appsflyer.l r0 = new com.appsflyer.l     // Catch:{ Throwable -> 0x0175 }
            boolean r2 = r9.isTrackingStopped()     // Catch:{ Throwable -> 0x0175 }
            r0.<init>(r10, r2)     // Catch:{ Throwable -> 0x0175 }
            r0.f298 = r1     // Catch:{ Throwable -> 0x0175 }
            java.lang.StringBuilder r1 = new java.lang.StringBuilder     // Catch:{ Throwable -> 0x0175 }
            r1.<init>()     // Catch:{ Throwable -> 0x0175 }
            java.lang.String r2 = f147     // Catch:{ Throwable -> 0x0175 }
            java.lang.String r2 = com.appsflyer.ServerConfigHandler.getUrl(r2)     // Catch:{ Throwable -> 0x0175 }
            java.lang.StringBuilder r1 = r1.append(r2)     // Catch:{ Throwable -> 0x0175 }
            java.lang.StringBuilder r1 = r1.append(r4)     // Catch:{ Throwable -> 0x0175 }
            java.lang.String r1 = r1.toString()     // Catch:{ Throwable -> 0x0175 }
            r2 = 1
            java.lang.String[] r2 = new java.lang.String[r2]     // Catch:{ Throwable -> 0x0175 }
            r3 = 0
            r2[r3] = r1     // Catch:{ Throwable -> 0x0175 }
            r0.execute(r2)     // Catch:{ Throwable -> 0x0175 }
            goto L_0x0022
        L_0x0175:
            r0 = move-exception
            java.lang.String r1 = r0.getMessage()
            com.appsflyer.AFLogger.afErrorLog(r1, r0)
            goto L_0x0022
        L_0x017f:
            r0 = move-exception
            java.lang.String r5 = "Exception while collecting application version info."
            com.appsflyer.AFLogger.afErrorLog(r5, r0)
            goto L_0x008c
        L_0x0187:
            r0 = move-exception
            java.lang.String r5 = "Exception while collecting device brand and model."
            com.appsflyer.AFLogger.afErrorLog(r5, r0)
            goto L_0x00ae
        */
        throw new UnsupportedOperationException("Method not decompiled: com.appsflyer.AppsFlyerLib.mo6482(android.content.Context, java.lang.String):void");
    }

    /* access modifiers changed from: 0000 */
    /* renamed from: ॱ */
    public final void mo6487() {
        this.f156 = System.currentTimeMillis();
    }

    /* access modifiers changed from: 0000 */
    /* renamed from: ˎ */
    public final void mo6484() {
        this.f157 = System.currentTimeMillis();
    }

    /* access modifiers changed from: 0000 */
    /* renamed from: ˋ */
    public final void mo6481(Context context, Intent intent) {
        String stringExtra = intent.getStringExtra(AppsFlyerProperties.IS_MONITOR);
        if (stringExtra != null) {
            AFLogger.afInfoLog("Turning on monitoring.");
            AppsFlyerProperties.getInstance().set(AppsFlyerProperties.IS_MONITOR, stringExtra.equals(ServerProtocol.DIALOG_RETURN_SCOPES_TRUE));
            m207(context, null, "START_TRACKING", context.getPackageName());
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
                this.f161 = System.currentTimeMillis();
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
                    ScheduledThreadPoolExecutor r7 = AFExecutor.getInstance().mo6409();
                    m217(r7, new C0418b(this, new WeakReference(context.getApplicationContext()), null, null, null, stringExtra2, r7, true, intent, 0), 5, TimeUnit.MILLISECONDS);
                }
            }
        }
    }

    /* renamed from: ˊ */
    private static void m199(JSONObject jSONObject) {
        ArrayList arrayList = new ArrayList();
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
                while (true) {
                    if (i2 >= jSONArray2.length()) {
                        break;
                    } else if (jSONArray2.getLong(i2) == ((Long) arrayList.get(0)).longValue() || jSONArray2.getLong(i2) == ((Long) arrayList.get(1)).longValue() || jSONArray2.getLong(i2) == ((Long) arrayList.get(arrayList.size() - 1)).longValue()) {
                        str = null;
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
    static void m223(Context context, String str) {
        JSONObject jSONObject;
        JSONArray jSONArray;
        AFLogger.afDebugLog("received a new (extra) referrer: ".concat(String.valueOf(str)));
        try {
            long currentTimeMillis = System.currentTimeMillis();
            String string = context.getSharedPreferences("appsflyer-data", 0).getString("extraReferrers", null);
            if (string == null) {
                JSONObject jSONObject2 = new JSONObject();
                jSONArray = new JSONArray();
                jSONObject = jSONObject2;
            } else {
                jSONObject = new JSONObject(string);
                if (jSONObject.has(str)) {
                    jSONArray = new JSONArray((String) jSONObject.get(str));
                } else {
                    jSONArray = new JSONArray();
                }
            }
            if (((long) jSONArray.length()) < 5) {
                jSONArray.put(currentTimeMillis);
            }
            if (((long) jSONObject.length()) >= 4) {
                m199(jSONObject);
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
        return f144;
    }

    public void stopTracking(boolean z, Context context) {
        this.f166 = z;
        CacheManager.getInstance().clearCache(context);
        if (this.f166) {
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
        C0469y.m373().mo6647("getSdkVersion", new String[0]);
        return "version: 4.8.11 (build 383)";
    }

    public void onPause(Context context) {
        C0432d.m278(context);
        C0434f r0 = C0434f.m284(context);
        r0.f255.post(r0.f252);
    }

    /* renamed from: ˊ */
    private void m194(Application application) {
        AppsFlyerProperties.getInstance().loadProperties(application.getApplicationContext());
        if (VERSION.SDK_INT < 14) {
            AFLogger.afInfoLog("SDK<14 call trackEvent manually");
            AFLogger.afInfoLog("onBecameForeground");
            getInstance().f156 = System.currentTimeMillis();
            getInstance().mo6486((Context) application, (String) null, null);
            AFLogger.resetDeltaTime();
        } else if (VERSION.SDK_INT >= 14 && this.f155 == null) {
            C0456q.m329();
            this.f155 = new C0457a() {
                /* renamed from: ॱ */
                public final void mo6490(Activity activity) {
                    if (2 > AppsFlyerLib.m219(AppsFlyerLib.m220((Context) activity))) {
                        C0434f r0 = C0434f.m284(activity);
                        r0.f255.post(r0.f252);
                        r0.f255.post(r0.f253);
                    }
                    AFLogger.afInfoLog("onBecameForeground");
                    AppsFlyerLib.getInstance().mo6487();
                    AppsFlyerLib.getInstance().mo6486((Context) activity, (String) null, null);
                    AFLogger.resetDeltaTime();
                }

                /* renamed from: ॱ */
                public final void mo6491(WeakReference<Context> weakReference) {
                    C0432d.m278((Context) weakReference.get());
                    C0434f r0 = C0434f.m284((Context) weakReference.get());
                    r0.f255.post(r0.f252);
                }
            };
            C0456q.m332().mo6607(application, this.f155);
        }
    }

    @Deprecated
    public void setGCMProjectID(String str) {
        C0469y.m373().mo6647("setGCMProjectID", str);
        AFLogger.afWarnLog("Method 'setGCMProjectNumber' is deprecated. Please follow the documentation.");
        enableUninstallTracking(str);
    }

    @Deprecated
    public void setGCMProjectNumber(String str) {
        C0469y.m373().mo6647("setGCMProjectNumber", str);
        AFLogger.afWarnLog("Method 'setGCMProjectNumber' is deprecated. Please follow the documentation.");
        enableUninstallTracking(str);
    }

    @Deprecated
    public void setGCMProjectNumber(Context context, String str) {
        C0469y.m373().mo6647("setGCMProjectNumber", str);
        AFLogger.afWarnLog("Method 'setGCMProjectNumber' is deprecated. Please use 'enableUninstallTracking'.");
        enableUninstallTracking(str);
    }

    public void enableUninstallTracking(String str) {
        C0469y.m373().mo6647("enableUninstallTracking", str);
        AppsFlyerProperties.getInstance().set("gcmProjectNumber", str);
    }

    public void updateServerUninstallToken(Context context, String str) {
        if (str != null) {
            C0467u.m369(context, new C0432d(str));
        }
    }

    public void setDebugLog(boolean z) {
        LogLevel logLevel;
        C0469y.m373().mo6647("setDebugLog", String.valueOf(z));
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
        C0469y.m373().mo6647("setImeiData", str);
        this.f160 = str;
    }

    public void setAndroidIdData(String str) {
        C0469y.m373().mo6647("setAndroidIdData", str);
        this.f164 = str;
    }

    public AppsFlyerLib enableLocationCollection(boolean z) {
        this.f159 = z;
        return this;
    }

    @Deprecated
    public void setAppUserId(String str) {
        C0469y.m373().mo6647("setAppUserId", str);
        setCustomerUserId(str);
    }

    public void setCustomerUserId(String str) {
        C0469y.m373().mo6647("setCustomerUserId", str);
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
                m225(context, string, null, null, referrer, context instanceof Activity ? ((Activity) context).getIntent() : null);
                if (AppsFlyerProperties.getInstance().getString("afUninstallToken") != null) {
                    mo6482(context, AppsFlyerProperties.getInstance().getString("afUninstallToken"));
                    return;
                }
                return;
            }
            setCustomerUserId(str);
            AFLogger.afInfoLog("waitForCustomerUserId is false; setting CustomerUserID: ".concat(String.valueOf(str)), true);
        }
    }

    public void setAppInviteOneLink(String str) {
        C0469y.m373().mo6647("setAppInviteOneLink", str);
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
            C0469y.m373().mo6647("setAdditionalData", hashMap.toString());
            AppsFlyerProperties.getInstance().setCustomData(new JSONObject(hashMap).toString());
        }
    }

    public void sendDeepLinkData(Activity activity) {
        if (activity != null && activity.getIntent() != null) {
            C0469y.m373().mo6647("sendDeepLinkData", activity.getLocalClassName(), new StringBuilder("activity_intent_").append(activity.getIntent().toString()).toString());
        } else if (activity != null) {
            C0469y.m373().mo6647("sendDeepLinkData", activity.getLocalClassName(), "activity_intent_null");
        } else {
            C0469y.m373().mo6647("sendDeepLinkData", "activity_null");
        }
        AFLogger.afInfoLog(new StringBuilder("getDeepLinkData with activity ").append(activity.getIntent().getDataString()).toString());
        m194(activity.getApplication());
    }

    /* JADX WARNING: Removed duplicated region for block: B:17:0x0074  */
    /* JADX WARNING: Removed duplicated region for block: B:22:0x009b  */
    /* JADX WARNING: Removed duplicated region for block: B:55:? A[RETURN, SYNTHETIC] */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    public void sendPushNotificationData(android.app.Activity r14) {
        /*
            r13 = this;
            r1 = 0
            r12 = 2
            r6 = 1
            r5 = 0
            if (r14 == 0) goto L_0x00cf
            android.content.Intent r0 = r14.getIntent()
            if (r0 == 0) goto L_0x00cf
            com.appsflyer.y r0 = com.appsflyer.C0469y.m373()
            java.lang.String r2 = "sendPushNotificationData"
            java.lang.String[] r3 = new java.lang.String[r12]
            java.lang.String r4 = r14.getLocalClassName()
            r3[r5] = r4
            java.lang.StringBuilder r4 = new java.lang.StringBuilder
            java.lang.String r5 = "activity_intent_"
            r4.<init>(r5)
            android.content.Intent r5 = r14.getIntent()
            java.lang.String r5 = r5.toString()
            java.lang.StringBuilder r4 = r4.append(r5)
            java.lang.String r4 = r4.toString()
            r3[r6] = r4
            r0.mo6647(r2, r3)
        L_0x0036:
            boolean r0 = r14 instanceof android.app.Activity
            if (r0 == 0) goto L_0x01aa
            r0 = r14
            android.app.Activity r0 = (android.app.Activity) r0
            android.content.Intent r2 = r0.getIntent()
            if (r2 == 0) goto L_0x01aa
            android.os.Bundle r3 = r2.getExtras()
            if (r3 == 0) goto L_0x01aa
            java.lang.String r0 = "af"
            java.lang.String r1 = r3.getString(r0)
            if (r1 == 0) goto L_0x006d
            java.lang.String r0 = "Push Notification received af payload = "
            java.lang.String r4 = java.lang.String.valueOf(r1)
            java.lang.String r0 = r0.concat(r4)
            com.appsflyer.AFLogger.afInfoLog(r0)
            java.lang.String r0 = "af"
            r3.remove(r0)
            r0 = r14
            android.app.Activity r0 = (android.app.Activity) r0
            android.content.Intent r2 = r2.putExtras(r3)
            r0.setIntent(r2)
        L_0x006d:
            r0 = r1
        L_0x006e:
            r13.f165 = r0
            java.lang.String r0 = r13.f165
            if (r0 == 0) goto L_0x00ce
            long r4 = java.lang.System.currentTimeMillis()
            java.util.Map<java.lang.Long, java.lang.String> r0 = r13.f162
            if (r0 != 0) goto L_0x00f9
            java.lang.String r0 = "pushes: initializing pushes history.."
            com.appsflyer.AFLogger.afInfoLog(r0)
            java.util.concurrent.ConcurrentHashMap r0 = new java.util.concurrent.ConcurrentHashMap
            r0.<init>()
            r13.f162 = r0
            r2 = r4
        L_0x0089:
            com.appsflyer.AppsFlyerProperties r0 = com.appsflyer.AppsFlyerProperties.getInstance()
            java.lang.String r1 = "pushPayloadHistorySize"
            int r0 = r0.getInt(r1, r12)
            java.util.Map<java.lang.Long, java.lang.String> r1 = r13.f162
            int r1 = r1.size()
            if (r1 != r0) goto L_0x00bc
            java.lang.StringBuilder r0 = new java.lang.StringBuilder
            java.lang.String r1 = "pushes: removing oldest overflowing push (oldest push:"
            r0.<init>(r1)
            java.lang.StringBuilder r0 = r0.append(r2)
            java.lang.String r1 = ")"
            java.lang.StringBuilder r0 = r0.append(r1)
            java.lang.String r0 = r0.toString()
            com.appsflyer.AFLogger.afInfoLog(r0)
            java.util.Map<java.lang.Long, java.lang.String> r0 = r13.f162
            java.lang.Long r1 = java.lang.Long.valueOf(r2)
            r0.remove(r1)
        L_0x00bc:
            java.util.Map<java.lang.Long, java.lang.String> r0 = r13.f162
            java.lang.Long r1 = java.lang.Long.valueOf(r4)
            java.lang.String r2 = r13.f165
            r0.put(r1, r2)
            android.app.Application r0 = r14.getApplication()
            r13.m194(r0)
        L_0x00ce:
            return
        L_0x00cf:
            if (r14 == 0) goto L_0x00e8
            com.appsflyer.y r0 = com.appsflyer.C0469y.m373()
            java.lang.String r2 = "sendPushNotificationData"
            java.lang.String[] r3 = new java.lang.String[r12]
            java.lang.String r4 = r14.getLocalClassName()
            r3[r5] = r4
            java.lang.String r4 = "activity_intent_null"
            r3[r6] = r4
            r0.mo6647(r2, r3)
            goto L_0x0036
        L_0x00e8:
            com.appsflyer.y r0 = com.appsflyer.C0469y.m373()
            java.lang.String r2 = "sendPushNotificationData"
            java.lang.String[] r3 = new java.lang.String[r6]
            java.lang.String r4 = "activity_null"
            r3[r5] = r4
            r0.mo6647(r2, r3)
            goto L_0x0036
        L_0x00f9:
            com.appsflyer.AppsFlyerProperties r0 = com.appsflyer.AppsFlyerProperties.getInstance()     // Catch:{ Throwable -> 0x01a5 }
            java.lang.String r1 = "pushPayloadMaxAging"
            r2 = 1800000(0x1b7740, double:8.89318E-318)
            long r6 = r0.getLong(r1, r2)     // Catch:{ Throwable -> 0x01a5 }
            java.util.Map<java.lang.Long, java.lang.String> r0 = r13.f162     // Catch:{ Throwable -> 0x01a5 }
            java.util.Set r0 = r0.keySet()     // Catch:{ Throwable -> 0x01a5 }
            java.util.Iterator r8 = r0.iterator()     // Catch:{ Throwable -> 0x01a5 }
            r2 = r4
        L_0x0111:
            boolean r0 = r8.hasNext()     // Catch:{ Throwable -> 0x016a }
            if (r0 == 0) goto L_0x0089
            java.lang.Object r0 = r8.next()     // Catch:{ Throwable -> 0x016a }
            java.lang.Long r0 = (java.lang.Long) r0     // Catch:{ Throwable -> 0x016a }
            org.json.JSONObject r9 = new org.json.JSONObject     // Catch:{ Throwable -> 0x016a }
            java.lang.String r1 = r13.f165     // Catch:{ Throwable -> 0x016a }
            r9.<init>(r1)     // Catch:{ Throwable -> 0x016a }
            org.json.JSONObject r10 = new org.json.JSONObject     // Catch:{ Throwable -> 0x016a }
            java.util.Map<java.lang.Long, java.lang.String> r1 = r13.f162     // Catch:{ Throwable -> 0x016a }
            java.lang.Object r1 = r1.get(r0)     // Catch:{ Throwable -> 0x016a }
            java.lang.String r1 = (java.lang.String) r1     // Catch:{ Throwable -> 0x016a }
            r10.<init>(r1)     // Catch:{ Throwable -> 0x016a }
            java.lang.String r1 = "pid"
            java.lang.Object r1 = r9.get(r1)     // Catch:{ Throwable -> 0x016a }
            java.lang.String r11 = "pid"
            java.lang.Object r11 = r10.get(r11)     // Catch:{ Throwable -> 0x016a }
            boolean r1 = r1.equals(r11)     // Catch:{ Throwable -> 0x016a }
            if (r1 == 0) goto L_0x0187
            java.lang.StringBuilder r0 = new java.lang.StringBuilder     // Catch:{ Throwable -> 0x016a }
            java.lang.String r1 = "PushNotificationMeasurement: A previous payload with same PID was already acknowledged! (old: "
            r0.<init>(r1)     // Catch:{ Throwable -> 0x016a }
            java.lang.StringBuilder r0 = r0.append(r10)     // Catch:{ Throwable -> 0x016a }
            java.lang.String r1 = ", new: "
            java.lang.StringBuilder r0 = r0.append(r1)     // Catch:{ Throwable -> 0x016a }
            java.lang.StringBuilder r0 = r0.append(r9)     // Catch:{ Throwable -> 0x016a }
            java.lang.String r1 = ")"
            java.lang.StringBuilder r0 = r0.append(r1)     // Catch:{ Throwable -> 0x016a }
            java.lang.String r0 = r0.toString()     // Catch:{ Throwable -> 0x016a }
            com.appsflyer.AFLogger.afInfoLog(r0)     // Catch:{ Throwable -> 0x016a }
            r0 = 0
            r13.f165 = r0     // Catch:{ Throwable -> 0x016a }
            goto L_0x00ce
        L_0x016a:
            r0 = move-exception
        L_0x016b:
            java.lang.StringBuilder r1 = new java.lang.StringBuilder
            java.lang.String r6 = "Error while handling push notification measurement: "
            r1.<init>(r6)
            java.lang.Class r6 = r0.getClass()
            java.lang.String r6 = r6.getSimpleName()
            java.lang.StringBuilder r1 = r1.append(r6)
            java.lang.String r1 = r1.toString()
            com.appsflyer.AFLogger.afErrorLog(r1, r0)
            goto L_0x0089
        L_0x0187:
            long r10 = r0.longValue()     // Catch:{ Throwable -> 0x016a }
            long r10 = r4 - r10
            int r1 = (r10 > r6 ? 1 : (r10 == r6 ? 0 : -1))
            if (r1 <= 0) goto L_0x0196
            java.util.Map<java.lang.Long, java.lang.String> r1 = r13.f162     // Catch:{ Throwable -> 0x016a }
            r1.remove(r0)     // Catch:{ Throwable -> 0x016a }
        L_0x0196:
            long r10 = r0.longValue()     // Catch:{ Throwable -> 0x016a }
            int r1 = (r10 > r2 ? 1 : (r10 == r2 ? 0 : -1))
            if (r1 > 0) goto L_0x01a8
            long r0 = r0.longValue()     // Catch:{ Throwable -> 0x016a }
        L_0x01a2:
            r2 = r0
            goto L_0x0111
        L_0x01a5:
            r0 = move-exception
            r2 = r4
            goto L_0x016b
        L_0x01a8:
            r0 = r2
            goto L_0x01a2
        L_0x01aa:
            r0 = r1
            goto L_0x006e
        */
        throw new UnsupportedOperationException("Method not decompiled: com.appsflyer.AppsFlyerLib.sendPushNotificationData(android.app.Activity):void");
    }

    @Deprecated
    public void setUserEmail(String str) {
        C0469y.m373().mo6647("setUserEmail", str);
        AppsFlyerProperties.getInstance().set(AppsFlyerProperties.USER_EMAIL, str);
    }

    public void setUserEmails(String... strArr) {
        C0469y.m373().mo6647("setUserEmails", strArr);
        setUserEmails(EmailsCryptType.NONE, strArr);
    }

    public void setUserEmails(EmailsCryptType emailsCryptType, String... strArr) {
        String str;
        ArrayList arrayList = new ArrayList(strArr.length + 1);
        arrayList.add(emailsCryptType.toString());
        arrayList.addAll(Arrays.asList(strArr));
        C0469y.m373().mo6647("setUserEmails", (String[]) arrayList.toArray(new String[(strArr.length + 1)]));
        AppsFlyerProperties.getInstance().set(AppsFlyerProperties.EMAIL_CRYPT_TYPE, emailsCryptType.getValue());
        HashMap hashMap = new HashMap();
        Object obj = null;
        ArrayList arrayList2 = new ArrayList();
        int length = strArr.length;
        int i = 0;
        while (i < length) {
            String str2 = strArr[i];
            switch (C04164.f183[emailsCryptType.ordinal()]) {
                case 2:
                    str = "md5_el_arr";
                    arrayList2.add(C0459r.m340(str2));
                    break;
                case 3:
                    str = "sha256_el_arr";
                    arrayList2.add(C0459r.m338(str2));
                    break;
                case 4:
                    str = "plain_el_arr";
                    arrayList2.add(str2);
                    break;
                default:
                    str = "sha1_el_arr";
                    arrayList2.add(C0459r.m341(str2));
                    break;
            }
            i++;
            obj = str;
        }
        hashMap.put(obj, arrayList2);
        AppsFlyerProperties.getInstance().setUserEmails(new JSONObject(hashMap).toString());
    }

    public void setCollectAndroidID(boolean z) {
        C0469y.m373().mo6647("setCollectAndroidID", String.valueOf(z));
        AppsFlyerProperties.getInstance().set(AppsFlyerProperties.COLLECT_ANDROID_ID, Boolean.toString(z));
    }

    public void setCollectIMEI(boolean z) {
        C0469y.m373().mo6647("setCollectIMEI", String.valueOf(z));
        AppsFlyerProperties.getInstance().set(AppsFlyerProperties.COLLECT_IMEI, Boolean.toString(z));
    }

    @Deprecated
    public void setCollectFingerPrint(boolean z) {
        C0469y.m373().mo6647("setCollectFingerPrint", String.valueOf(z));
        AppsFlyerProperties.getInstance().set(AppsFlyerProperties.COLLECT_FINGER_PRINT, Boolean.toString(z));
    }

    public AppsFlyerLib init(String str, AppsFlyerConversionListener appsFlyerConversionListener) {
        C0469y r1 = C0469y.m373();
        String str2 = "init";
        String[] strArr = new String[2];
        strArr[0] = str;
        strArr[1] = appsFlyerConversionListener == null ? "null" : "conversionDataListener";
        r1.mo6647(str2, strArr);
        AFLogger.m185(String.format("Initializing AppsFlyer SDK: (v%s.%s)", new Object[]{BuildConfig.AF_SDK_VERSION, "383"}));
        this.f170 = true;
        AppsFlyerProperties.getInstance().set(AppsFlyerProperties.AF_KEY, str);
        C04375.m288(str);
        f149 = appsFlyerConversionListener;
        return this;
    }

    public AppsFlyerLib init(String str, AppsFlyerConversionListener appsFlyerConversionListener, Context context) {
        if (context != null && m235(context)) {
            if (this.f174 == null) {
                this.f174 = new C0433e();
                this.f174.mo6561(context, this);
            } else {
                AFLogger.afWarnLog("AFInstallReferrer instance already created");
            }
        }
        return init(str, appsFlyerConversionListener);
    }

    public void startTracking(Application application) {
        if (!this.f170) {
            AFLogger.afWarnLog("ERROR: AppsFlyer SDK is not initialized! The API call 'startTracking(Application)' must be called after the 'init(String, AppsFlyerConversionListener)' API method, which should be called on the Application's onCreate.");
        } else {
            startTracking(application, null);
        }
    }

    public void startTracking(Application application, String str) {
        C0469y.m373().mo6647("startTracking", str);
        AFLogger.afInfoLog(String.format("Starting AppsFlyer Tracking: (v%s.%s)", new Object[]{BuildConfig.AF_SDK_VERSION, "383"}));
        AFLogger.afInfoLog("Build Number: 383");
        AppsFlyerProperties.getInstance().loadProperties(application.getApplicationContext());
        if (!TextUtils.isEmpty(str)) {
            AppsFlyerProperties.getInstance().set(AppsFlyerProperties.AF_KEY, str);
            C04375.m288(str);
        } else {
            if (TextUtils.isEmpty(AppsFlyerProperties.getInstance().getString(AppsFlyerProperties.AF_KEY))) {
                AFLogger.afWarnLog("ERROR: AppsFlyer SDK is not initialized! You must provide AppsFlyer Dev-Key either in the 'init' API method (should be called on Application's onCreate),or in the startTracking API method (should be called on Activity's onCreate).");
                return;
            }
        }
        m194(application);
    }

    public void setAppId(String str) {
        C0469y.m373().mo6647("setAppId", str);
        AppsFlyerProperties.getInstance().set(AppsFlyerProperties.APP_ID, str);
    }

    public void setExtension(String str) {
        C0469y.m373().mo6647("setExtension", str);
        AppsFlyerProperties.getInstance().set(AppsFlyerProperties.EXTENSION, str);
    }

    public void setIsUpdate(boolean z) {
        C0469y.m373().mo6647("setIsUpdate", String.valueOf(z));
        AppsFlyerProperties.getInstance().set(AppsFlyerProperties.IS_UPDATE, z);
    }

    public void setCurrencyCode(String str) {
        C0469y.m373().mo6647("setCurrencyCode", str);
        AppsFlyerProperties.getInstance().set(AppsFlyerProperties.CURRENCY_CODE, str);
    }

    public void trackLocation(Context context, double d, double d2) {
        C0469y.m373().mo6647("trackLocation", String.valueOf(d), String.valueOf(d2));
        HashMap hashMap = new HashMap();
        hashMap.put(AFInAppEventParameterName.LONGTITUDE, Double.toString(d2));
        hashMap.put(AFInAppEventParameterName.LATITUDE, Double.toString(d));
        mo6486(context, AFInAppEventType.LOCATION_COORDINATES, (Map<String, Object>) hashMap);
    }

    /* access modifiers changed from: 0000 */
    /* renamed from: ˎ */
    public final void mo6485(WeakReference<Context> weakReference) {
        if (weakReference.get() != null) {
            AFLogger.afInfoLog("app went to background");
            SharedPreferences sharedPreferences = ((Context) weakReference.get()).getSharedPreferences("appsflyer-data", 0);
            AppsFlyerProperties.getInstance().saveProperties(sharedPreferences);
            long j = this.f157 - this.f156;
            HashMap hashMap = new HashMap();
            String string = AppsFlyerProperties.getInstance().getString(AppsFlyerProperties.AF_KEY);
            if (string == null) {
                AFLogger.afWarnLog("[callStats] AppsFlyer's SDK cannot send any event without providing DevKey.");
                return;
            }
            String string2 = AppsFlyerProperties.getInstance().getString("KSAppsFlyerId");
            if (AppsFlyerProperties.getInstance().getBoolean(AppsFlyerProperties.DEVICE_TRACKING_DISABLED, false)) {
                hashMap.put(AppsFlyerProperties.DEVICE_TRACKING_DISABLED, ServerProtocol.DIALOG_RETURN_SCOPES_TRUE);
            }
            C0452n r0 = C0454o.m324(((Context) weakReference.get()).getContentResolver());
            if (r0 != null) {
                hashMap.put("amazon_aid", r0.mo6598());
                hashMap.put("amazon_aid_limit", String.valueOf(r0.mo6597()));
            }
            String string3 = AppsFlyerProperties.getInstance().getString(ServerParameters.ADVERTISING_ID_PARAM);
            if (string3 != null) {
                hashMap.put(ServerParameters.ADVERTISING_ID_PARAM, string3);
            }
            hashMap.put("app_id", ((Context) weakReference.get()).getPackageName());
            hashMap.put("devkey", string);
            hashMap.put("uid", C0455p.m326(weakReference));
            hashMap.put("time_in_app", String.valueOf(j / 1000));
            hashMap.put("statType", "user_closed_app");
            hashMap.put("platform", "Android");
            hashMap.put("launch_counter", Integer.toString(m188(sharedPreferences, "appsFlyerCount", false)));
            hashMap.put("gcd_conversion_data_timing", Long.toString(sharedPreferences.getLong("appsflyerGetConversionDataTiming", 0)));
            String str = AppsFlyerProperties.CHANNEL;
            String string4 = AppsFlyerProperties.getInstance().getString(AppsFlyerProperties.CHANNEL);
            if (string4 == null) {
                string4 = m204(weakReference, "CHANNEL");
            }
            hashMap.put(str, string4);
            hashMap.put("originalAppsflyerId", string2 != null ? string2 : "");
            if (this.f167) {
                try {
                    C0447l lVar = new C0447l(null, isTrackingStopped());
                    lVar.f298 = hashMap;
                    if (Thread.currentThread() == Looper.getMainLooper().getThread()) {
                        AFLogger.afDebugLog("Main thread detected. Running callStats task in a new thread.");
                        lVar.execute(new String[]{ServerConfigHandler.getUrl("https://stats.%s/stats")});
                        return;
                    }
                    AFLogger.afDebugLog(new StringBuilder("Running callStats task (on current thread: ").append(Thread.currentThread().toString()).append(" )").toString());
                    lVar.onPreExecute();
                    lVar.onPostExecute(lVar.doInBackground(ServerConfigHandler.getUrl("https://stats.%s/stats")));
                } catch (Throwable th) {
                    AFLogger.afErrorLog("Could not send callStats request", th);
                }
            } else {
                AFLogger.afDebugLog("Stats call is disabled, ignore ...");
            }
        }
    }

    public void trackAppLaunch(Context context, String str) {
        if (m235(context)) {
            if (this.f174 == null) {
                this.f174 = new C0433e();
                this.f174.mo6561(context, this);
            } else {
                AFLogger.afWarnLog("AFInstallReferrer instance already created");
            }
        }
        m225(context, str, null, null, "", null);
    }

    /* access modifiers changed from: protected */
    public void setDeepLinkData(Intent intent) {
        if (intent != null) {
            try {
                if ("android.intent.action.VIEW".equals(intent.getAction())) {
                    this.f158 = intent.getData();
                    AFLogger.afDebugLog(new StringBuilder("Unity setDeepLinkData = ").append(this.f158).toString());
                }
            } catch (Throwable th) {
                AFLogger.afErrorLog("Exception while setting deeplink data (unity). ", th);
            }
        }
    }

    public void reportTrackSession(Context context) {
        C0469y.m373().mo6647("reportTrackSession", new String[0]);
        C0469y.m373().mo6650();
        mo6486(context, (String) null, null);
    }

    public void trackEvent(Context context, String str, Map<String, Object> map) {
        Map<String, Object> map2;
        if (map == null) {
            map2 = new HashMap<>();
        } else {
            map2 = map;
        }
        C0469y.m373().mo6647("trackEvent", str, new JSONObject(map2).toString());
        mo6486(context, str, map);
    }

    /* access modifiers changed from: 0000 */
    /* renamed from: ˏ */
    public final void mo6486(Context context, String str, Map<String, Object> map) {
        Intent intent = context instanceof Activity ? ((Activity) context).getIntent() : null;
        if (AppsFlyerProperties.getInstance().getString(AppsFlyerProperties.AF_KEY) == null) {
            AFLogger.afWarnLog("[TrackEvent/Launch] AppsFlyer's SDK cannot send any event without providing DevKey.");
            return;
        }
        if (map == null) {
            map = new HashMap<>();
        }
        JSONObject jSONObject = new JSONObject(map);
        String referrer = AppsFlyerProperties.getInstance().getReferrer(context);
        String jSONObject2 = jSONObject.toString();
        if (referrer == null) {
            referrer = "";
        }
        m225(context, null, str, jSONObject2, referrer, intent);
    }

    /* renamed from: ˋ */
    private static void m207(Context context, String str, String str2, String str3) {
        if (AppsFlyerProperties.getInstance().getBoolean(AppsFlyerProperties.IS_MONITOR, false)) {
            Intent intent = new Intent("com.appsflyer.MonitorBroadcast");
            intent.setPackage("com.appsflyer.nightvision");
            intent.putExtra("message", str2);
            intent.putExtra("value", str3);
            intent.putExtra("packageName", ServerProtocol.DIALOG_RETURN_SCOPES_TRUE);
            intent.putExtra(Constants.URL_MEDIA_SOURCE, new Integer(Process.myPid()));
            intent.putExtra("eventIdentifier", str);
            intent.putExtra("sdk", BuildConfig.AF_SDK_VERSION);
            context.sendBroadcast(intent);
        }
    }

    public void setDeviceTrackingDisabled(boolean z) {
        C0469y.m373().mo6647("setDeviceTrackingDisabled", String.valueOf(z));
        AppsFlyerProperties.getInstance().set(AppsFlyerProperties.DEVICE_TRACKING_DISABLED, z);
    }

    /* access modifiers changed from: private */
    /* renamed from: ˊ */
    public static Map<String, String> m192(Context context) throws C0446k {
        String string = context.getSharedPreferences("appsflyer-data", 0).getString("attributionId", null);
        if (string != null && string.length() > 0) {
            return m230(string);
        }
        throw new C0446k();
    }

    public void registerConversionListener(Context context, AppsFlyerConversionListener appsFlyerConversionListener) {
        C0469y.m373().mo6647("registerConversionListener", new String[0]);
        if (appsFlyerConversionListener != null) {
            f149 = appsFlyerConversionListener;
        }
    }

    public void unregisterConversionListener() {
        C0469y.m373().mo6647("unregisterConversionListener", new String[0]);
        f149 = null;
    }

    public void registerValidatorListener(Context context, AppsFlyerInAppPurchaseValidatorListener appsFlyerInAppPurchaseValidatorListener) {
        C0469y.m373().mo6647("registerValidatorListener", new String[0]);
        AFLogger.afDebugLog("registerValidatorListener called");
        if (appsFlyerInAppPurchaseValidatorListener == null) {
            AFLogger.afDebugLog("registerValidatorListener null listener");
        } else {
            f148 = appsFlyerInAppPurchaseValidatorListener;
        }
    }

    /* access modifiers changed from: protected */
    public void getConversionData(Context context, final ConversionDataListener conversionDataListener) {
        f149 = new AppsFlyerConversionListener() {
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
    private static Map<String, String> m213(Context context, String str) {
        String str2;
        LinkedHashMap linkedHashMap = new LinkedHashMap();
        String[] split = str.split("&");
        int length = split.length;
        boolean z = false;
        for (int i = 0; i < length; i++) {
            String str3 = split[i];
            int indexOf = str3.indexOf("=");
            if (indexOf > 0) {
                str2 = str3.substring(0, indexOf);
            } else {
                str2 = str3;
            }
            if (!linkedHashMap.containsKey(str2)) {
                if (str2.equals(Constants.URL_CAMPAIGN)) {
                    str2 = Param.CAMPAIGN;
                } else if (str2.equals(Constants.URL_MEDIA_SOURCE)) {
                    str2 = "media_source";
                } else if (str2.equals("af_prt")) {
                    z = true;
                    str2 = "agency";
                }
                linkedHashMap.put(str2, "");
            }
            linkedHashMap.put(str2, (indexOf <= 0 || str3.length() <= indexOf + 1) ? null : str3.substring(indexOf + 1));
        }
        try {
            if (!linkedHashMap.containsKey("install_time")) {
                PackageInfo packageInfo = context.getPackageManager().getPackageInfo(context.getPackageName(), 0);
                SimpleDateFormat simpleDateFormat = new SimpleDateFormat("yyyy-MM-dd HH:mm:ss", Locale.US);
                long j = packageInfo.firstInstallTime;
                simpleDateFormat.setTimeZone(TimeZone.getTimeZone("UTC"));
                linkedHashMap.put("install_time", simpleDateFormat.format(new Date(j)));
            }
        } catch (Exception e) {
            AFLogger.afErrorLog("Could not fetch install time. ", e);
        }
        if (!linkedHashMap.containsKey("af_status")) {
            linkedHashMap.put("af_status", "Non-organic");
        }
        if (z) {
            linkedHashMap.remove("media_source");
        }
        return linkedHashMap;
    }

    /* access modifiers changed from: private */
    /* renamed from: ॱ */
    public static Map<String, String> m230(String str) {
        HashMap hashMap = new HashMap();
        try {
            JSONObject jSONObject = new JSONObject(str);
            Iterator keys = jSONObject.keys();
            while (keys.hasNext()) {
                String str2 = (String) keys.next();
                if (!f145.contains(str2)) {
                    String string = jSONObject.getString(str2);
                    if (!TextUtils.isEmpty(string) && !"null".equals(string)) {
                        hashMap.put(str2, string);
                    }
                }
            }
            return hashMap;
        } catch (JSONException e) {
            AFLogger.afErrorLog(e.getMessage(), e);
            return null;
        }
    }

    /* JADX WARNING: Removed duplicated region for block: B:12:0x002e  */
    /* JADX WARNING: Removed duplicated region for block: B:9:0x0023  */
    /* renamed from: ˏ */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    private void m225(android.content.Context r12, java.lang.String r13, java.lang.String r14, java.lang.String r15, java.lang.String r16, android.content.Intent r17) {
        /*
            r11 = this;
            android.content.Context r3 = r12.getApplicationContext()
            if (r14 != 0) goto L_0x002a
            r0 = 1
        L_0x0007:
            java.lang.String r1 = "waitForCustomerId"
            com.appsflyer.AppsFlyerProperties r2 = com.appsflyer.AppsFlyerProperties.getInstance()
            r4 = 0
            boolean r1 = r2.getBoolean(r1, r4)
            if (r1 == 0) goto L_0x002c
            java.lang.String r1 = "AppUserId"
            com.appsflyer.AppsFlyerProperties r2 = com.appsflyer.AppsFlyerProperties.getInstance()
            java.lang.String r1 = r2.getString(r1)
            if (r1 != 0) goto L_0x002c
            r1 = 1
        L_0x0021:
            if (r1 == 0) goto L_0x002e
            java.lang.String r0 = "CustomerUserId not set, Tracking is disabled"
            r1 = 1
            com.appsflyer.AFLogger.afInfoLog(r0, r1)
        L_0x0029:
            return
        L_0x002a:
            r0 = 0
            goto L_0x0007
        L_0x002c:
            r1 = 0
            goto L_0x0021
        L_0x002e:
            if (r0 == 0) goto L_0x0049
            com.appsflyer.AppsFlyerProperties r0 = com.appsflyer.AppsFlyerProperties.getInstance()
            java.lang.String r1 = "launchProtectEnabled"
            r2 = 1
            boolean r0 = r0.getBoolean(r1, r2)
            if (r0 == 0) goto L_0x006d
            boolean r0 = r11.m208()
            if (r0 != 0) goto L_0x0029
        L_0x0043:
            long r0 = java.lang.System.currentTimeMillis()
            r11.f153 = r0
        L_0x0049:
            com.appsflyer.AFExecutor r0 = com.appsflyer.AFExecutor.getInstance()
            java.util.concurrent.ScheduledThreadPoolExecutor r7 = r0.mo6409()
            com.appsflyer.AppsFlyerLib$b r0 = new com.appsflyer.AppsFlyerLib$b
            java.lang.ref.WeakReference r2 = new java.lang.ref.WeakReference
            r2.<init>(r3)
            r8 = 0
            r10 = 0
            r1 = r11
            r3 = r13
            r4 = r14
            r5 = r15
            r6 = r16
            r9 = r17
            r0.<init>(r1, r2, r3, r4, r5, r6, r7, r8, r9, r10)
            r2 = 150(0x96, double:7.4E-322)
            java.util.concurrent.TimeUnit r1 = java.util.concurrent.TimeUnit.MILLISECONDS
            m217(r7, r0, r2, r1)
            goto L_0x0029
        L_0x006d:
            java.lang.String r0 = "Allowing multiple launches within a 5 second time window."
            com.appsflyer.AFLogger.afInfoLog(r0)
            goto L_0x0043
        */
        throw new UnsupportedOperationException("Method not decompiled: com.appsflyer.AppsFlyerLib.m225(android.content.Context, java.lang.String, java.lang.String, java.lang.String, java.lang.String, android.content.Intent):void");
    }

    /* renamed from: ˋ */
    private boolean m208() {
        if (this.f153 > 0) {
            long currentTimeMillis = System.currentTimeMillis() - this.f153;
            SimpleDateFormat simpleDateFormat = new SimpleDateFormat("yyyy/MM/dd HH:mm:ss.SSS Z", Locale.US);
            long j = this.f153;
            simpleDateFormat.setTimeZone(TimeZone.getTimeZone("UTC"));
            String format = simpleDateFormat.format(new Date(j));
            long j2 = this.f171;
            simpleDateFormat.setTimeZone(TimeZone.getTimeZone("UTC"));
            String format2 = simpleDateFormat.format(new Date(j2));
            if (currentTimeMillis < this.f172 && !isTrackingStopped()) {
                AFLogger.afInfoLog(String.format(Locale.US, "Last Launch attempt: %s;\nLast successful Launch event: %s;\nThis launch is blocked: %s ms < %s ms", new Object[]{format, format2, Long.valueOf(currentTimeMillis), Long.valueOf(this.f172)}));
                return true;
            } else if (!isTrackingStopped()) {
                AFLogger.afInfoLog(String.format(Locale.US, "Last Launch attempt: %s;\nLast successful Launch event: %s;\nSending launch (+%s ms)", new Object[]{format, format2, Long.valueOf(currentTimeMillis)}));
            }
        } else if (!isTrackingStopped()) {
            AFLogger.afInfoLog("Sending first launch for this session!");
        }
        return false;
    }

    /* access modifiers changed from: 0000 */
    /* JADX WARNING: Code restructure failed: missing block: B:370:0x0aa7, code lost:
        if (r2 != null) goto L_0x0aa9;
     */
    /* JADX WARNING: No exception handlers in catch block: Catch:{  } */
    /* renamed from: ˎ */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    public final java.util.Map<java.lang.String, java.lang.Object> mo6483(android.content.Context r13, java.lang.String r14, java.lang.String r15, java.lang.String r16, java.lang.String r17, boolean r18, android.content.SharedPreferences r19, boolean r20, android.content.Intent r21) {
        /*
            r12 = this;
            java.util.HashMap r5 = new java.util.HashMap
            r5.<init>()
            com.appsflyer.C0454o.m325(r13, r5)
            java.util.Date r2 = new java.util.Date
            r2.<init>()
            long r2 = r2.getTime()
            java.lang.String r4 = "af_timestamp"
            java.lang.String r6 = java.lang.Long.toString(r2)
            r5.put(r4, r6)
            java.lang.String r2 = com.appsflyer.C0427a.m268(r13, r2)
            if (r2 == 0) goto L_0x0025
            java.lang.String r3 = "cksm_v1"
            r5.put(r3, r2)
        L_0x0025:
            boolean r2 = r12.isTrackingStopped()     // Catch:{ Throwable -> 0x0913 }
            if (r2 != 0) goto L_0x090c
            java.lang.StringBuilder r3 = new java.lang.StringBuilder     // Catch:{ Throwable -> 0x0913 }
            java.lang.String r2 = "******* sendTrackingWithEvent: "
            r3.<init>(r2)     // Catch:{ Throwable -> 0x0913 }
            if (r20 == 0) goto L_0x0909
            java.lang.String r2 = "Launch"
        L_0x0036:
            java.lang.StringBuilder r2 = r3.append(r2)     // Catch:{ Throwable -> 0x0913 }
            java.lang.String r2 = r2.toString()     // Catch:{ Throwable -> 0x0913 }
            com.appsflyer.AFLogger.afInfoLog(r2)     // Catch:{ Throwable -> 0x0913 }
        L_0x0041:
            java.lang.String r3 = "AppsFlyer_4.8.11"
            java.lang.String r4 = "EVENT_CREATED_WITH_NAME"
            if (r20 == 0) goto L_0x091c
            java.lang.String r2 = "Launch"
        L_0x0049:
            m207(r13, r3, r4, r2)     // Catch:{ Throwable -> 0x0913 }
            com.appsflyer.cache.CacheManager r2 = com.appsflyer.cache.CacheManager.getInstance()     // Catch:{ Throwable -> 0x0913 }
            r2.init(r13)     // Catch:{ Throwable -> 0x0913 }
            android.content.pm.PackageManager r2 = r13.getPackageManager()     // Catch:{ Exception -> 0x091f }
            java.lang.String r3 = r13.getPackageName()     // Catch:{ Exception -> 0x091f }
            r4 = 4096(0x1000, float:5.74E-42)
            android.content.pm.PackageInfo r2 = r2.getPackageInfo(r3, r4)     // Catch:{ Exception -> 0x091f }
            java.lang.String[] r2 = r2.requestedPermissions     // Catch:{ Exception -> 0x091f }
            java.util.List r2 = java.util.Arrays.asList(r2)     // Catch:{ Exception -> 0x091f }
            java.lang.String r3 = "android.permission.INTERNET"
            boolean r3 = r2.contains(r3)     // Catch:{ Exception -> 0x091f }
            if (r3 != 0) goto L_0x007b
            java.lang.String r3 = "Permission android.permission.INTERNET is missing in the AndroidManifest.xml"
            com.appsflyer.AFLogger.afWarnLog(r3)     // Catch:{ Exception -> 0x091f }
            r3 = 0
            java.lang.String r4 = "PERMISSION_INTERNET_MISSING"
            r6 = 0
            m207(r13, r3, r4, r6)     // Catch:{ Exception -> 0x091f }
        L_0x007b:
            java.lang.String r3 = "android.permission.ACCESS_NETWORK_STATE"
            boolean r3 = r2.contains(r3)     // Catch:{ Exception -> 0x091f }
            if (r3 != 0) goto L_0x0088
            java.lang.String r3 = "Permission android.permission.ACCESS_NETWORK_STATE is missing in the AndroidManifest.xml"
            com.appsflyer.AFLogger.afWarnLog(r3)     // Catch:{ Exception -> 0x091f }
        L_0x0088:
            java.lang.String r3 = "android.permission.ACCESS_WIFI_STATE"
            boolean r2 = r2.contains(r3)     // Catch:{ Exception -> 0x091f }
            if (r2 != 0) goto L_0x0095
            java.lang.String r2 = "Permission android.permission.ACCESS_WIFI_STATE is missing in the AndroidManifest.xml"
            com.appsflyer.AFLogger.afWarnLog(r2)     // Catch:{ Exception -> 0x091f }
        L_0x0095:
            if (r18 == 0) goto L_0x009e
            java.lang.String r2 = "af_events_api"
            java.lang.String r3 = "1"
            r5.put(r2, r3)     // Catch:{ Throwable -> 0x0913 }
        L_0x009e:
            java.lang.String r2 = "brand"
            java.lang.String r3 = android.os.Build.BRAND     // Catch:{ Throwable -> 0x0913 }
            r5.put(r2, r3)     // Catch:{ Throwable -> 0x0913 }
            java.lang.String r2 = "device"
            java.lang.String r3 = android.os.Build.DEVICE     // Catch:{ Throwable -> 0x0913 }
            r5.put(r2, r3)     // Catch:{ Throwable -> 0x0913 }
            java.lang.String r2 = "product"
            java.lang.String r3 = android.os.Build.PRODUCT     // Catch:{ Throwable -> 0x0913 }
            r5.put(r2, r3)     // Catch:{ Throwable -> 0x0913 }
            java.lang.String r2 = "sdk"
            int r3 = android.os.Build.VERSION.SDK_INT     // Catch:{ Throwable -> 0x0913 }
            java.lang.String r3 = java.lang.Integer.toString(r3)     // Catch:{ Throwable -> 0x0913 }
            r5.put(r2, r3)     // Catch:{ Throwable -> 0x0913 }
            java.lang.String r2 = "model"
            java.lang.String r3 = android.os.Build.MODEL     // Catch:{ Throwable -> 0x0913 }
            r5.put(r2, r3)     // Catch:{ Throwable -> 0x0913 }
            java.lang.String r2 = "deviceType"
            java.lang.String r3 = android.os.Build.TYPE     // Catch:{ Throwable -> 0x0913 }
            r5.put(r2, r3)     // Catch:{ Throwable -> 0x0913 }
            if (r20 == 0) goto L_0x098e
            java.lang.String r2 = "appsflyer-data"
            r3 = 0
            android.content.SharedPreferences r2 = r13.getSharedPreferences(r2, r3)     // Catch:{ Throwable -> 0x0913 }
            java.lang.String r3 = "appsFlyerCount"
            boolean r2 = r2.contains(r3)     // Catch:{ Throwable -> 0x0913 }
            if (r2 != 0) goto L_0x0927
            r2 = 1
        L_0x00de:
            if (r2 == 0) goto L_0x0204
            com.appsflyer.AppsFlyerProperties r2 = com.appsflyer.AppsFlyerProperties.getInstance()     // Catch:{ Throwable -> 0x0913 }
            boolean r2 = r2.isOtherSdkStringDisabled()     // Catch:{ Throwable -> 0x0913 }
            if (r2 != 0) goto L_0x019b
            java.lang.String r3 = "af_sdks"
            java.lang.StringBuilder r4 = new java.lang.StringBuilder     // Catch:{ Throwable -> 0x0913 }
            r4.<init>()     // Catch:{ Throwable -> 0x0913 }
            java.lang.String r2 = "com.tune.Tune"
            com.appsflyer.t r6 = r12.f168     // Catch:{ Throwable -> 0x0913 }
            boolean r2 = r6.mo6635(r2)     // Catch:{ Throwable -> 0x0913 }
            if (r2 == 0) goto L_0x092a
            r2 = 1
        L_0x00fc:
            java.lang.StringBuilder r4 = r4.append(r2)     // Catch:{ Throwable -> 0x0913 }
            java.lang.String r2 = "com.adjust.sdk.Adjust"
            com.appsflyer.t r6 = r12.f168     // Catch:{ Throwable -> 0x0913 }
            boolean r2 = r6.mo6635(r2)     // Catch:{ Throwable -> 0x0913 }
            if (r2 == 0) goto L_0x092d
            r2 = 1
        L_0x010b:
            java.lang.StringBuilder r4 = r4.append(r2)     // Catch:{ Throwable -> 0x0913 }
            java.lang.String r2 = "com.kochava.android.tracker.Feature"
            com.appsflyer.t r6 = r12.f168     // Catch:{ Throwable -> 0x0913 }
            boolean r2 = r6.mo6635(r2)     // Catch:{ Throwable -> 0x0913 }
            if (r2 == 0) goto L_0x0930
            r2 = 1
        L_0x011a:
            java.lang.StringBuilder r4 = r4.append(r2)     // Catch:{ Throwable -> 0x0913 }
            java.lang.String r2 = "io.branch.referral.Branch"
            com.appsflyer.t r6 = r12.f168     // Catch:{ Throwable -> 0x0913 }
            boolean r2 = r6.mo6635(r2)     // Catch:{ Throwable -> 0x0913 }
            if (r2 == 0) goto L_0x0933
            r2 = 1
        L_0x0129:
            java.lang.StringBuilder r4 = r4.append(r2)     // Catch:{ Throwable -> 0x0913 }
            java.lang.String r2 = "com.apsalar.sdk.Apsalar"
            com.appsflyer.t r6 = r12.f168     // Catch:{ Throwable -> 0x0913 }
            boolean r2 = r6.mo6635(r2)     // Catch:{ Throwable -> 0x0913 }
            if (r2 == 0) goto L_0x0936
            r2 = 1
        L_0x0138:
            java.lang.StringBuilder r4 = r4.append(r2)     // Catch:{ Throwable -> 0x0913 }
            java.lang.String r2 = "com.localytics.android.Localytics"
            com.appsflyer.t r6 = r12.f168     // Catch:{ Throwable -> 0x0913 }
            boolean r2 = r6.mo6635(r2)     // Catch:{ Throwable -> 0x0913 }
            if (r2 == 0) goto L_0x0939
            r2 = 1
        L_0x0147:
            java.lang.StringBuilder r4 = r4.append(r2)     // Catch:{ Throwable -> 0x0913 }
            java.lang.String r2 = "com.tenjin.android.TenjinSDK"
            com.appsflyer.t r6 = r12.f168     // Catch:{ Throwable -> 0x0913 }
            boolean r2 = r6.mo6635(r2)     // Catch:{ Throwable -> 0x0913 }
            if (r2 == 0) goto L_0x093c
            r2 = 1
        L_0x0156:
            java.lang.StringBuilder r4 = r4.append(r2)     // Catch:{ Throwable -> 0x0913 }
            java.lang.String r2 = "place holder for TD"
            com.appsflyer.t r6 = r12.f168     // Catch:{ Throwable -> 0x0913 }
            boolean r2 = r6.mo6635(r2)     // Catch:{ Throwable -> 0x0913 }
            if (r2 == 0) goto L_0x093f
            r2 = 1
        L_0x0165:
            java.lang.StringBuilder r4 = r4.append(r2)     // Catch:{ Throwable -> 0x0913 }
            java.lang.String r2 = "it.partytrack.sdk.Track"
            com.appsflyer.t r6 = r12.f168     // Catch:{ Throwable -> 0x0913 }
            boolean r2 = r6.mo6635(r2)     // Catch:{ Throwable -> 0x0913 }
            if (r2 == 0) goto L_0x0942
            r2 = 1
        L_0x0174:
            java.lang.StringBuilder r4 = r4.append(r2)     // Catch:{ Throwable -> 0x0913 }
            java.lang.String r2 = "jp.appAdForce.android.LtvManager"
            com.appsflyer.t r6 = r12.f168     // Catch:{ Throwable -> 0x0913 }
            boolean r2 = r6.mo6635(r2)     // Catch:{ Throwable -> 0x0913 }
            if (r2 == 0) goto L_0x0945
            r2 = 1
        L_0x0183:
            java.lang.StringBuilder r2 = r4.append(r2)     // Catch:{ Throwable -> 0x0913 }
            java.lang.String r2 = r2.toString()     // Catch:{ Throwable -> 0x0913 }
            r5.put(r3, r2)     // Catch:{ Throwable -> 0x0913 }
            float r2 = m236(r13)     // Catch:{ Throwable -> 0x0913 }
            java.lang.String r3 = "batteryLevel"
            java.lang.String r2 = java.lang.String.valueOf(r2)     // Catch:{ Throwable -> 0x0913 }
            r5.put(r3, r2)     // Catch:{ Throwable -> 0x0913 }
        L_0x019b:
            r2 = 18
            java.lang.String r3 = "OPPO"
            java.lang.String r4 = android.os.Build.BRAND     // Catch:{ Throwable -> 0x0913 }
            boolean r3 = r3.equals(r4)     // Catch:{ Throwable -> 0x0913 }
            if (r3 == 0) goto L_0x0948
            r3 = 1
        L_0x01a8:
            if (r3 == 0) goto L_0x01b1
            r2 = 23
            java.lang.String r3 = "OPPO device found"
            com.appsflyer.AFLogger.afRDLog(r3)     // Catch:{ Throwable -> 0x0913 }
        L_0x01b1:
            int r3 = android.os.Build.VERSION.SDK_INT     // Catch:{ Throwable -> 0x0913 }
            if (r3 < r2) goto L_0x096e
            java.lang.StringBuilder r2 = new java.lang.StringBuilder     // Catch:{ Throwable -> 0x0913 }
            java.lang.String r3 = "OS SDK is="
            r2.<init>(r3)     // Catch:{ Throwable -> 0x0913 }
            int r3 = android.os.Build.VERSION.SDK_INT     // Catch:{ Throwable -> 0x0913 }
            java.lang.StringBuilder r2 = r2.append(r3)     // Catch:{ Throwable -> 0x0913 }
            java.lang.String r3 = "; use KeyStore"
            java.lang.StringBuilder r2 = r2.append(r3)     // Catch:{ Throwable -> 0x0913 }
            java.lang.String r2 = r2.toString()     // Catch:{ Throwable -> 0x0913 }
            com.appsflyer.AFLogger.afRDLog(r2)     // Catch:{ Throwable -> 0x0913 }
            com.appsflyer.AFKeystoreWrapper r2 = new com.appsflyer.AFKeystoreWrapper     // Catch:{ Throwable -> 0x0913 }
            r2.<init>(r13)     // Catch:{ Throwable -> 0x0913 }
            boolean r3 = r2.mo6411()     // Catch:{ Throwable -> 0x0913 }
            if (r3 != 0) goto L_0x094b
            java.lang.ref.WeakReference r3 = new java.lang.ref.WeakReference     // Catch:{ Throwable -> 0x0913 }
            r3.<init>(r13)     // Catch:{ Throwable -> 0x0913 }
            java.lang.String r3 = com.appsflyer.C0455p.m326(r3)     // Catch:{ Throwable -> 0x0913 }
            r2.mo6413(r3)     // Catch:{ Throwable -> 0x0913 }
            java.lang.String r3 = "KSAppsFlyerId"
            java.lang.String r4 = r2.mo6410()     // Catch:{ Throwable -> 0x0913 }
            com.appsflyer.AppsFlyerProperties r6 = com.appsflyer.AppsFlyerProperties.getInstance()     // Catch:{ Throwable -> 0x0913 }
            r6.set(r3, r4)     // Catch:{ Throwable -> 0x0913 }
            java.lang.String r3 = "KSAppsFlyerRICounter"
            int r2 = r2.mo6414()     // Catch:{ Throwable -> 0x0913 }
            java.lang.String r2 = java.lang.String.valueOf(r2)     // Catch:{ Throwable -> 0x0913 }
            com.appsflyer.AppsFlyerProperties r4 = com.appsflyer.AppsFlyerProperties.getInstance()     // Catch:{ Throwable -> 0x0913 }
            r4.set(r3, r2)     // Catch:{ Throwable -> 0x0913 }
        L_0x0204:
            java.lang.String r4 = "timepassedsincelastlaunch"
            java.lang.String r2 = "appsflyer-data"
            r3 = 0
            android.content.SharedPreferences r2 = r13.getSharedPreferences(r2, r3)     // Catch:{ Throwable -> 0x0913 }
            java.lang.String r3 = "AppsFlyerTimePassedSincePrevLaunch"
            r6 = 0
            long r2 = r2.getLong(r3, r6)     // Catch:{ Throwable -> 0x0913 }
            long r6 = java.lang.System.currentTimeMillis()     // Catch:{ Throwable -> 0x0913 }
            java.lang.String r8 = "AppsFlyerTimePassedSincePrevLaunch"
            m196(r13, r8, r6)     // Catch:{ Throwable -> 0x0913 }
            r8 = 0
            int r8 = (r2 > r8 ? 1 : (r2 == r8 ? 0 : -1))
            if (r8 <= 0) goto L_0x098a
            long r2 = r6 - r2
            r6 = 1000(0x3e8, double:4.94E-321)
            long r2 = r2 / r6
        L_0x0229:
            java.lang.String r2 = java.lang.Long.toString(r2)     // Catch:{ Throwable -> 0x0913 }
            r5.put(r4, r2)     // Catch:{ Throwable -> 0x0913 }
            com.appsflyer.AppsFlyerProperties r2 = com.appsflyer.AppsFlyerProperties.getInstance()     // Catch:{ Throwable -> 0x0913 }
            java.lang.String r3 = "oneLinkSlug"
            java.lang.String r2 = r2.getString(r3)     // Catch:{ Throwable -> 0x0913 }
            if (r2 == 0) goto L_0x0250
            java.lang.String r3 = "onelink_id"
            r5.put(r3, r2)     // Catch:{ Throwable -> 0x0913 }
            java.lang.String r2 = "ol_ver"
            com.appsflyer.AppsFlyerProperties r3 = com.appsflyer.AppsFlyerProperties.getInstance()     // Catch:{ Throwable -> 0x0913 }
            java.lang.String r4 = "onelinkVersion"
            java.lang.String r3 = r3.getString(r4)     // Catch:{ Throwable -> 0x0913 }
            r5.put(r2, r3)     // Catch:{ Throwable -> 0x0913 }
        L_0x0250:
            java.lang.String r2 = "KSAppsFlyerId"
            com.appsflyer.AppsFlyerProperties r3 = com.appsflyer.AppsFlyerProperties.getInstance()     // Catch:{ Throwable -> 0x0913 }
            java.lang.String r2 = r3.getString(r2)     // Catch:{ Throwable -> 0x0913 }
            java.lang.String r3 = "KSAppsFlyerRICounter"
            com.appsflyer.AppsFlyerProperties r4 = com.appsflyer.AppsFlyerProperties.getInstance()     // Catch:{ Throwable -> 0x0913 }
            java.lang.String r3 = r4.getString(r3)     // Catch:{ Throwable -> 0x0913 }
            if (r2 == 0) goto L_0x027c
            if (r3 == 0) goto L_0x027c
            java.lang.Integer r4 = java.lang.Integer.valueOf(r3)     // Catch:{ Throwable -> 0x0913 }
            int r4 = r4.intValue()     // Catch:{ Throwable -> 0x0913 }
            if (r4 <= 0) goto L_0x027c
            java.lang.String r4 = "reinstallCounter"
            r5.put(r4, r3)     // Catch:{ Throwable -> 0x0913 }
            java.lang.String r3 = "originalAppsflyerId"
            r5.put(r3, r2)     // Catch:{ Throwable -> 0x0913 }
        L_0x027c:
            java.lang.String r2 = "additionalCustomData"
            com.appsflyer.AppsFlyerProperties r3 = com.appsflyer.AppsFlyerProperties.getInstance()     // Catch:{ Throwable -> 0x0913 }
            java.lang.String r2 = r3.getString(r2)     // Catch:{ Throwable -> 0x0913 }
            if (r2 == 0) goto L_0x028d
            java.lang.String r3 = "customData"
            r5.put(r3, r2)     // Catch:{ Throwable -> 0x0913 }
        L_0x028d:
            android.content.pm.PackageManager r2 = r13.getPackageManager()     // Catch:{ Exception -> 0x0a08 }
            java.lang.String r3 = r13.getPackageName()     // Catch:{ Exception -> 0x0a08 }
            java.lang.String r2 = r2.getInstallerPackageName(r3)     // Catch:{ Exception -> 0x0a08 }
            if (r2 == 0) goto L_0x02a0
            java.lang.String r3 = "installer_package"
            r5.put(r3, r2)     // Catch:{ Exception -> 0x0a08 }
        L_0x02a0:
            com.appsflyer.AppsFlyerProperties r2 = com.appsflyer.AppsFlyerProperties.getInstance()     // Catch:{ Throwable -> 0x0913 }
            java.lang.String r3 = "sdkExtension"
            java.lang.String r2 = r2.getString(r3)     // Catch:{ Throwable -> 0x0913 }
            if (r2 == 0) goto L_0x02b7
            int r3 = r2.length()     // Catch:{ Throwable -> 0x0913 }
            if (r3 <= 0) goto L_0x02b7
            java.lang.String r3 = "sdkExtension"
            r5.put(r3, r2)     // Catch:{ Throwable -> 0x0913 }
        L_0x02b7:
            java.lang.ref.WeakReference r3 = new java.lang.ref.WeakReference     // Catch:{ Throwable -> 0x0913 }
            r3.<init>(r13)     // Catch:{ Throwable -> 0x0913 }
            com.appsflyer.AppsFlyerProperties r2 = com.appsflyer.AppsFlyerProperties.getInstance()     // Catch:{ Throwable -> 0x0913 }
            java.lang.String r4 = "channel"
            java.lang.String r2 = r2.getString(r4)     // Catch:{ Throwable -> 0x0913 }
            if (r2 != 0) goto L_0x02ce
            java.lang.String r2 = "CHANNEL"
            java.lang.String r2 = m204(r3, r2)     // Catch:{ Throwable -> 0x0913 }
        L_0x02ce:
            java.lang.String r3 = m189(r13, r2)     // Catch:{ Throwable -> 0x0913 }
            if (r3 == 0) goto L_0x02d9
            java.lang.String r4 = "channel"
            r5.put(r4, r3)     // Catch:{ Throwable -> 0x0913 }
        L_0x02d9:
            if (r3 == 0) goto L_0x02e1
            boolean r4 = r3.equals(r2)     // Catch:{ Throwable -> 0x0913 }
            if (r4 == 0) goto L_0x02e5
        L_0x02e1:
            if (r3 != 0) goto L_0x02ea
            if (r2 == 0) goto L_0x02ea
        L_0x02e5:
            java.lang.String r3 = "af_latestchannel"
            r5.put(r3, r2)     // Catch:{ Throwable -> 0x0913 }
        L_0x02ea:
            java.lang.String r2 = "appsflyer-data"
            r3 = 0
            android.content.SharedPreferences r2 = r13.getSharedPreferences(r2, r3)     // Catch:{ Throwable -> 0x0913 }
            java.lang.String r3 = "INSTALL_STORE"
            boolean r3 = r2.contains(r3)     // Catch:{ Throwable -> 0x0913 }
            if (r3 == 0) goto L_0x0a10
            java.lang.String r3 = "INSTALL_STORE"
            r4 = 0
            java.lang.String r2 = r2.getString(r3, r4)     // Catch:{ Throwable -> 0x0913 }
        L_0x0300:
            if (r2 == 0) goto L_0x030b
            java.lang.String r3 = "af_installstore"
            java.lang.String r2 = r2.toLowerCase()     // Catch:{ Throwable -> 0x0913 }
            r5.put(r3, r2)     // Catch:{ Throwable -> 0x0913 }
        L_0x030b:
            java.lang.String r2 = "appsflyer-data"
            r3 = 0
            android.content.SharedPreferences r3 = r13.getSharedPreferences(r2, r3)     // Catch:{ Throwable -> 0x0913 }
            java.lang.String r2 = "preInstallName"
            com.appsflyer.AppsFlyerProperties r4 = com.appsflyer.AppsFlyerProperties.getInstance()     // Catch:{ Throwable -> 0x0913 }
            java.lang.String r2 = r4.getString(r2)     // Catch:{ Throwable -> 0x0913 }
            if (r2 != 0) goto L_0x0338
            java.lang.String r4 = "preInstallName"
            boolean r4 = r3.contains(r4)     // Catch:{ Throwable -> 0x0913 }
            if (r4 == 0) goto L_0x0a38
            java.lang.String r2 = "preInstallName"
            r4 = 0
            java.lang.String r2 = r3.getString(r2, r4)     // Catch:{ Throwable -> 0x0913 }
        L_0x032d:
            if (r2 == 0) goto L_0x0338
            java.lang.String r3 = "preInstallName"
            com.appsflyer.AppsFlyerProperties r4 = com.appsflyer.AppsFlyerProperties.getInstance()     // Catch:{ Throwable -> 0x0913 }
            r4.set(r3, r2)     // Catch:{ Throwable -> 0x0913 }
        L_0x0338:
            if (r2 == 0) goto L_0x0343
            java.lang.String r3 = "af_preinstall_name"
            java.lang.String r2 = r2.toLowerCase()     // Catch:{ Throwable -> 0x0913 }
            r5.put(r3, r2)     // Catch:{ Throwable -> 0x0913 }
        L_0x0343:
            java.lang.ref.WeakReference r2 = new java.lang.ref.WeakReference     // Catch:{ Throwable -> 0x0913 }
            r2.<init>(r13)     // Catch:{ Throwable -> 0x0913 }
            java.lang.String r3 = "AF_STORE"
            java.lang.String r2 = m204(r2, r3)     // Catch:{ Throwable -> 0x0913 }
            if (r2 == 0) goto L_0x0359
            java.lang.String r3 = "af_currentstore"
            java.lang.String r2 = r2.toLowerCase()     // Catch:{ Throwable -> 0x0913 }
            r5.put(r3, r2)     // Catch:{ Throwable -> 0x0913 }
        L_0x0359:
            if (r14 == 0) goto L_0x0acc
            int r2 = r14.length()     // Catch:{ Throwable -> 0x0913 }
            if (r2 < 0) goto L_0x0acc
            java.lang.String r2 = "appsflyerKey"
            r5.put(r2, r14)     // Catch:{ Throwable -> 0x0913 }
        L_0x0366:
            java.lang.String r2 = "AppUserId"
            com.appsflyer.AppsFlyerProperties r3 = com.appsflyer.AppsFlyerProperties.getInstance()     // Catch:{ Throwable -> 0x0913 }
            java.lang.String r2 = r3.getString(r2)     // Catch:{ Throwable -> 0x0913 }
            if (r2 == 0) goto L_0x0377
            java.lang.String r3 = "appUserId"
            r5.put(r3, r2)     // Catch:{ Throwable -> 0x0913 }
        L_0x0377:
            com.appsflyer.AppsFlyerProperties r2 = com.appsflyer.AppsFlyerProperties.getInstance()     // Catch:{ Throwable -> 0x0913 }
            java.lang.String r3 = "userEmails"
            java.lang.String r2 = r2.getString(r3)     // Catch:{ Throwable -> 0x0913 }
            if (r2 == 0) goto L_0x0afa
            java.lang.String r3 = "user_emails"
            r5.put(r3, r2)     // Catch:{ Throwable -> 0x0913 }
        L_0x0388:
            if (r15 == 0) goto L_0x0398
            java.lang.String r2 = "eventName"
            r5.put(r2, r15)     // Catch:{ Throwable -> 0x0913 }
            if (r16 == 0) goto L_0x0398
            java.lang.String r2 = "eventValue"
            r0 = r16
            r5.put(r2, r0)     // Catch:{ Throwable -> 0x0913 }
        L_0x0398:
            java.lang.String r2 = "appid"
            com.appsflyer.AppsFlyerProperties r3 = com.appsflyer.AppsFlyerProperties.getInstance()     // Catch:{ Throwable -> 0x0913 }
            java.lang.String r2 = r3.getString(r2)     // Catch:{ Throwable -> 0x0913 }
            if (r2 == 0) goto L_0x03b3
            java.lang.String r2 = "appid"
            java.lang.String r3 = "appid"
            com.appsflyer.AppsFlyerProperties r4 = com.appsflyer.AppsFlyerProperties.getInstance()     // Catch:{ Throwable -> 0x0913 }
            java.lang.String r3 = r4.getString(r3)     // Catch:{ Throwable -> 0x0913 }
            r5.put(r2, r3)     // Catch:{ Throwable -> 0x0913 }
        L_0x03b3:
            java.lang.String r2 = "currencyCode"
            com.appsflyer.AppsFlyerProperties r3 = com.appsflyer.AppsFlyerProperties.getInstance()     // Catch:{ Throwable -> 0x0913 }
            java.lang.String r2 = r3.getString(r2)     // Catch:{ Throwable -> 0x0913 }
            if (r2 == 0) goto L_0x03e3
            int r3 = r2.length()     // Catch:{ Throwable -> 0x0913 }
            r4 = 3
            if (r3 == r4) goto L_0x03de
            java.lang.StringBuilder r3 = new java.lang.StringBuilder     // Catch:{ Throwable -> 0x0913 }
            java.lang.String r4 = "WARNING: currency code should be 3 characters!!! '"
            r3.<init>(r4)     // Catch:{ Throwable -> 0x0913 }
            java.lang.StringBuilder r3 = r3.append(r2)     // Catch:{ Throwable -> 0x0913 }
            java.lang.String r4 = "' is not a legal value."
            java.lang.StringBuilder r3 = r3.append(r4)     // Catch:{ Throwable -> 0x0913 }
            java.lang.String r3 = r3.toString()     // Catch:{ Throwable -> 0x0913 }
            com.appsflyer.AFLogger.afWarnLog(r3)     // Catch:{ Throwable -> 0x0913 }
        L_0x03de:
            java.lang.String r3 = "currency"
            r5.put(r3, r2)     // Catch:{ Throwable -> 0x0913 }
        L_0x03e3:
            java.lang.String r2 = "IS_UPDATE"
            com.appsflyer.AppsFlyerProperties r3 = com.appsflyer.AppsFlyerProperties.getInstance()     // Catch:{ Throwable -> 0x0913 }
            java.lang.String r2 = r3.getString(r2)     // Catch:{ Throwable -> 0x0913 }
            if (r2 == 0) goto L_0x03f4
            java.lang.String r3 = "isUpdate"
            r5.put(r3, r2)     // Catch:{ Throwable -> 0x0913 }
        L_0x03f4:
            boolean r2 = r12.isPreInstalledApp(r13)     // Catch:{ Throwable -> 0x0913 }
            java.lang.String r3 = "af_preinstalled"
            java.lang.String r2 = java.lang.Boolean.toString(r2)     // Catch:{ Throwable -> 0x0913 }
            r5.put(r3, r2)     // Catch:{ Throwable -> 0x0913 }
            com.appsflyer.AppsFlyerProperties r2 = com.appsflyer.AppsFlyerProperties.getInstance()     // Catch:{ Throwable -> 0x0913 }
            java.lang.String r3 = "collectFacebookAttrId"
            r4 = 1
            boolean r2 = r2.getBoolean(r3, r4)     // Catch:{ Throwable -> 0x0913 }
            if (r2 == 0) goto L_0x0427
            android.content.pm.PackageManager r2 = r13.getPackageManager()     // Catch:{ NameNotFoundException -> 0x0b11, Throwable -> 0x0b1a }
            java.lang.String r3 = "com.facebook.katana"
            r4 = 0
            r2.getApplicationInfo(r3, r4)     // Catch:{ NameNotFoundException -> 0x0b11, Throwable -> 0x0b1a }
            android.content.ContentResolver r2 = r13.getContentResolver()     // Catch:{ NameNotFoundException -> 0x0b11, Throwable -> 0x0b1a }
            java.lang.String r2 = r12.getAttributionId(r2)     // Catch:{ NameNotFoundException -> 0x0b11, Throwable -> 0x0b1a }
        L_0x0420:
            if (r2 == 0) goto L_0x0427
            java.lang.String r3 = "fb"
            r5.put(r3, r2)     // Catch:{ Throwable -> 0x0913 }
        L_0x0427:
            com.appsflyer.AppsFlyerProperties r2 = com.appsflyer.AppsFlyerProperties.getInstance()     // Catch:{ Throwable -> 0x0913 }
            java.lang.String r3 = "deviceTrackingDisabled"
            r4 = 0
            boolean r2 = r2.getBoolean(r3, r4)     // Catch:{ Throwable -> 0x0913 }
            if (r2 == 0) goto L_0x0b24
            java.lang.String r2 = "deviceTrackingDisabled"
            java.lang.String r3 = "true"
            r5.put(r2, r3)     // Catch:{ Throwable -> 0x0913 }
        L_0x043b:
            java.lang.ref.WeakReference r2 = new java.lang.ref.WeakReference     // Catch:{ Exception -> 0x0c16 }
            r2.<init>(r13)     // Catch:{ Exception -> 0x0c16 }
            java.lang.String r2 = com.appsflyer.C0455p.m326(r2)     // Catch:{ Exception -> 0x0c16 }
            if (r2 == 0) goto L_0x044b
            java.lang.String r3 = "uid"
            r5.put(r3, r2)     // Catch:{ Exception -> 0x0c16 }
        L_0x044b:
            java.lang.String r2 = "lang"
            java.util.Locale r3 = java.util.Locale.getDefault()     // Catch:{ Exception -> 0x0c2f }
            java.lang.String r3 = r3.getDisplayLanguage()     // Catch:{ Exception -> 0x0c2f }
            r5.put(r2, r3)     // Catch:{ Exception -> 0x0c2f }
        L_0x0458:
            java.lang.String r2 = "lang_code"
            java.util.Locale r3 = java.util.Locale.getDefault()     // Catch:{ Exception -> 0x0c37 }
            java.lang.String r3 = r3.getLanguage()     // Catch:{ Exception -> 0x0c37 }
            r5.put(r2, r3)     // Catch:{ Exception -> 0x0c37 }
        L_0x0465:
            java.lang.String r2 = "country"
            java.util.Locale r3 = java.util.Locale.getDefault()     // Catch:{ Exception -> 0x0c3f }
            java.lang.String r3 = r3.getCountry()     // Catch:{ Exception -> 0x0c3f }
            r5.put(r2, r3)     // Catch:{ Exception -> 0x0c3f }
        L_0x0472:
            java.lang.String r2 = "platformextension"
            com.appsflyer.t r3 = r12.f168     // Catch:{ Throwable -> 0x0913 }
            java.lang.String r3 = r3.mo6634()     // Catch:{ Throwable -> 0x0913 }
            r5.put(r2, r3)     // Catch:{ Throwable -> 0x0913 }
            m233(r13, r5)     // Catch:{ Throwable -> 0x0913 }
            java.lang.String r2 = "yyyy-MM-dd_HHmmssZ"
            java.text.SimpleDateFormat r3 = new java.text.SimpleDateFormat     // Catch:{ Throwable -> 0x0913 }
            java.util.Locale r4 = java.util.Locale.US     // Catch:{ Throwable -> 0x0913 }
            r3.<init>(r2, r4)     // Catch:{ Throwable -> 0x0913 }
            int r2 = android.os.Build.VERSION.SDK_INT     // Catch:{ Throwable -> 0x0913 }
            r4 = 9
            if (r2 < r4) goto L_0x04b5
            android.content.pm.PackageManager r2 = r13.getPackageManager()     // Catch:{ Exception -> 0x0c47 }
            java.lang.String r4 = r13.getPackageName()     // Catch:{ Exception -> 0x0c47 }
            r6 = 0
            android.content.pm.PackageInfo r2 = r2.getPackageInfo(r4, r6)     // Catch:{ Exception -> 0x0c47 }
            long r6 = r2.firstInstallTime     // Catch:{ Exception -> 0x0c47 }
            java.lang.String r2 = "installDate"
            java.lang.String r4 = "UTC"
            java.util.TimeZone r4 = java.util.TimeZone.getTimeZone(r4)     // Catch:{ Exception -> 0x0c47 }
            r3.setTimeZone(r4)     // Catch:{ Exception -> 0x0c47 }
            java.util.Date r4 = new java.util.Date     // Catch:{ Exception -> 0x0c47 }
            r4.<init>(r6)     // Catch:{ Exception -> 0x0c47 }
            java.lang.String r4 = r3.format(r4)     // Catch:{ Exception -> 0x0c47 }
            r5.put(r2, r4)     // Catch:{ Exception -> 0x0c47 }
        L_0x04b5:
            android.content.pm.PackageManager r2 = r13.getPackageManager()     // Catch:{ Throwable -> 0x0c56 }
            java.lang.String r4 = r13.getPackageName()     // Catch:{ Throwable -> 0x0c56 }
            r6 = 0
            android.content.pm.PackageInfo r2 = r2.getPackageInfo(r4, r6)     // Catch:{ Throwable -> 0x0c56 }
            java.lang.String r4 = "versionCode"
            r6 = 0
            r0 = r19
            int r4 = r0.getInt(r4, r6)     // Catch:{ Throwable -> 0x0c56 }
            int r6 = r2.versionCode     // Catch:{ Throwable -> 0x0c56 }
            if (r6 <= r4) goto L_0x04dc
            java.lang.String r4 = "appsflyerConversionDataRequestRetries"
            r6 = 0
            m214(r13, r4, r6)     // Catch:{ Throwable -> 0x0c56 }
            java.lang.String r4 = "versionCode"
            int r6 = r2.versionCode     // Catch:{ Throwable -> 0x0c56 }
            m214(r13, r4, r6)     // Catch:{ Throwable -> 0x0c56 }
        L_0x04dc:
            java.lang.String r4 = "app_version_code"
            int r6 = r2.versionCode     // Catch:{ Throwable -> 0x0c56 }
            java.lang.String r6 = java.lang.Integer.toString(r6)     // Catch:{ Throwable -> 0x0c56 }
            r5.put(r4, r6)     // Catch:{ Throwable -> 0x0c56 }
            java.lang.String r4 = "app_version_name"
            java.lang.String r6 = r2.versionName     // Catch:{ Throwable -> 0x0c56 }
            r5.put(r4, r6)     // Catch:{ Throwable -> 0x0c56 }
            int r4 = android.os.Build.VERSION.SDK_INT     // Catch:{ Throwable -> 0x0c56 }
            r6 = 9
            if (r4 < r6) goto L_0x056d
            long r6 = r2.firstInstallTime     // Catch:{ Throwable -> 0x0c56 }
            long r8 = r2.lastUpdateTime     // Catch:{ Throwable -> 0x0c56 }
            java.lang.String r2 = "date1"
            java.lang.String r4 = "UTC"
            java.util.TimeZone r4 = java.util.TimeZone.getTimeZone(r4)     // Catch:{ Throwable -> 0x0c56 }
            r3.setTimeZone(r4)     // Catch:{ Throwable -> 0x0c56 }
            java.util.Date r4 = new java.util.Date     // Catch:{ Throwable -> 0x0c56 }
            r4.<init>(r6)     // Catch:{ Throwable -> 0x0c56 }
            java.lang.String r4 = r3.format(r4)     // Catch:{ Throwable -> 0x0c56 }
            r5.put(r2, r4)     // Catch:{ Throwable -> 0x0c56 }
            java.lang.String r2 = "date2"
            java.lang.String r4 = "UTC"
            java.util.TimeZone r4 = java.util.TimeZone.getTimeZone(r4)     // Catch:{ Throwable -> 0x0c56 }
            r3.setTimeZone(r4)     // Catch:{ Throwable -> 0x0c56 }
            java.util.Date r4 = new java.util.Date     // Catch:{ Throwable -> 0x0c56 }
            r4.<init>(r8)     // Catch:{ Throwable -> 0x0c56 }
            java.lang.String r4 = r3.format(r4)     // Catch:{ Throwable -> 0x0c56 }
            r5.put(r2, r4)     // Catch:{ Throwable -> 0x0c56 }
            java.lang.String r2 = "appsflyer-data"
            r4 = 0
            android.content.SharedPreferences r2 = r13.getSharedPreferences(r2, r4)     // Catch:{ Throwable -> 0x0c56 }
            java.lang.String r4 = "appsFlyerFirstInstall"
            r6 = 0
            java.lang.String r2 = r2.getString(r4, r6)     // Catch:{ Throwable -> 0x0c56 }
            if (r2 != 0) goto L_0x055b
            java.lang.String r2 = "appsflyer-data"
            r4 = 0
            android.content.SharedPreferences r2 = r13.getSharedPreferences(r2, r4)     // Catch:{ Throwable -> 0x0c56 }
            java.lang.String r4 = "appsFlyerCount"
            boolean r2 = r2.contains(r4)     // Catch:{ Throwable -> 0x0c56 }
            if (r2 != 0) goto L_0x0c4f
            r2 = 1
        L_0x0546:
            if (r2 == 0) goto L_0x0c52
            java.lang.String r2 = "AppsFlyer: first launch detected"
            com.appsflyer.AFLogger.afDebugLog(r2)     // Catch:{ Throwable -> 0x0c56 }
            java.util.Date r2 = new java.util.Date     // Catch:{ Throwable -> 0x0c56 }
            r2.<init>()     // Catch:{ Throwable -> 0x0c56 }
            java.lang.String r2 = r3.format(r2)     // Catch:{ Throwable -> 0x0c56 }
        L_0x0556:
            java.lang.String r3 = "appsFlyerFirstInstall"
            m232(r13, r3, r2)     // Catch:{ Throwable -> 0x0c56 }
        L_0x055b:
            java.lang.String r3 = "AppsFlyer: first launch date: "
            java.lang.String r4 = java.lang.String.valueOf(r2)     // Catch:{ Throwable -> 0x0c56 }
            java.lang.String r3 = r3.concat(r4)     // Catch:{ Throwable -> 0x0c56 }
            com.appsflyer.AFLogger.afInfoLog(r3)     // Catch:{ Throwable -> 0x0c56 }
            java.lang.String r3 = "firstLaunchDate"
            r5.put(r3, r2)     // Catch:{ Throwable -> 0x0c56 }
        L_0x056d:
            int r2 = r17.length()     // Catch:{ Throwable -> 0x0913 }
            if (r2 <= 0) goto L_0x057a
            java.lang.String r2 = "referrer"
            r0 = r17
            r5.put(r2, r0)     // Catch:{ Throwable -> 0x0913 }
        L_0x057a:
            java.lang.String r2 = "extraReferrers"
            r3 = 0
            r0 = r19
            java.lang.String r2 = r0.getString(r2, r3)     // Catch:{ Throwable -> 0x0913 }
            if (r2 == 0) goto L_0x058a
            java.lang.String r3 = "extraReferrers"
            r5.put(r3, r2)     // Catch:{ Throwable -> 0x0913 }
        L_0x058a:
            java.lang.String r2 = "afUninstallToken"
            com.appsflyer.AppsFlyerProperties r3 = com.appsflyer.AppsFlyerProperties.getInstance()     // Catch:{ Throwable -> 0x0913 }
            java.lang.String r2 = r3.getString(r2)     // Catch:{ Throwable -> 0x0913 }
            if (r2 == 0) goto L_0x05a3
            com.appsflyer.d r2 = com.appsflyer.C0432d.m277(r2)     // Catch:{ Throwable -> 0x0913 }
            java.lang.String r3 = "af_gcm_token"
            java.lang.String r2 = r2.mo6557()     // Catch:{ Throwable -> 0x0913 }
            r5.put(r3, r2)     // Catch:{ Throwable -> 0x0913 }
        L_0x05a3:
            boolean r2 = com.appsflyer.C0467u.m366(r13)     // Catch:{ Throwable -> 0x0913 }
            r12.f169 = r2     // Catch:{ Throwable -> 0x0913 }
            java.lang.StringBuilder r2 = new java.lang.StringBuilder     // Catch:{ Throwable -> 0x0913 }
            java.lang.String r3 = "didConfigureTokenRefreshService="
            r2.<init>(r3)     // Catch:{ Throwable -> 0x0913 }
            boolean r3 = r12.f169     // Catch:{ Throwable -> 0x0913 }
            java.lang.StringBuilder r2 = r2.append(r3)     // Catch:{ Throwable -> 0x0913 }
            java.lang.String r2 = r2.toString()     // Catch:{ Throwable -> 0x0913 }
            com.appsflyer.AFLogger.afDebugLog(r2)     // Catch:{ Throwable -> 0x0913 }
            boolean r2 = r12.f169     // Catch:{ Throwable -> 0x0913 }
            if (r2 != 0) goto L_0x05c8
            java.lang.String r2 = "tokenRefreshConfigured"
            java.lang.Boolean r3 = java.lang.Boolean.FALSE     // Catch:{ Throwable -> 0x0913 }
            r5.put(r2, r3)     // Catch:{ Throwable -> 0x0913 }
        L_0x05c8:
            if (r20 == 0) goto L_0x05e8
            java.lang.String r2 = r12.f165     // Catch:{ Throwable -> 0x0913 }
            if (r2 == 0) goto L_0x05e5
            org.json.JSONObject r2 = new org.json.JSONObject     // Catch:{ Throwable -> 0x0913 }
            java.lang.String r3 = r12.f165     // Catch:{ Throwable -> 0x0913 }
            r2.<init>(r3)     // Catch:{ Throwable -> 0x0913 }
            java.lang.String r3 = "isPush"
            java.lang.String r4 = "true"
            r2.put(r3, r4)     // Catch:{ Throwable -> 0x0913 }
            java.lang.String r3 = "af_deeplink"
            java.lang.String r2 = r2.toString()     // Catch:{ Throwable -> 0x0913 }
            r5.put(r3, r2)     // Catch:{ Throwable -> 0x0913 }
        L_0x05e5:
            r2 = 0
            r12.f165 = r2     // Catch:{ Throwable -> 0x0913 }
        L_0x05e8:
            if (r20 == 0) goto L_0x0602
            r2 = 0
            if (r21 == 0) goto L_0x05fd
            java.lang.String r3 = "android.intent.action.VIEW"
            java.lang.String r4 = r21.getAction()     // Catch:{ Throwable -> 0x0913 }
            boolean r3 = r3.equals(r4)     // Catch:{ Throwable -> 0x0913 }
            if (r3 == 0) goto L_0x05fd
            android.net.Uri r2 = r21.getData()     // Catch:{ Throwable -> 0x0913 }
        L_0x05fd:
            if (r2 == 0) goto L_0x0c5e
            r12.m226(r13, r5, r2)     // Catch:{ Throwable -> 0x0913 }
        L_0x0602:
            boolean r2 = r12.f163     // Catch:{ Throwable -> 0x0913 }
            if (r2 == 0) goto L_0x062a
            java.lang.String r2 = "testAppMode_retargeting"
            java.lang.String r3 = "true"
            r5.put(r2, r3)     // Catch:{ Throwable -> 0x0913 }
            org.json.JSONObject r2 = new org.json.JSONObject     // Catch:{ Throwable -> 0x0913 }
            r2.<init>(r5)     // Catch:{ Throwable -> 0x0913 }
            java.lang.String r2 = r2.toString()     // Catch:{ Throwable -> 0x0913 }
            android.content.Intent r3 = new android.content.Intent     // Catch:{ Throwable -> 0x0913 }
            java.lang.String r4 = "com.appsflyer.testIntgrationBroadcast"
            r3.<init>(r4)     // Catch:{ Throwable -> 0x0913 }
            java.lang.String r4 = "params"
            r3.putExtra(r4, r2)     // Catch:{ Throwable -> 0x0913 }
            r13.sendBroadcast(r3)     // Catch:{ Throwable -> 0x0913 }
            java.lang.String r2 = "Sent retargeting params to test app"
            com.appsflyer.AFLogger.afInfoLog(r2)     // Catch:{ Throwable -> 0x0913 }
        L_0x062a:
            long r2 = java.lang.System.currentTimeMillis()     // Catch:{ Throwable -> 0x0913 }
            long r6 = r12.f161     // Catch:{ Throwable -> 0x0913 }
            long r2 = r2 - r6
            com.appsflyer.AppsFlyerProperties r4 = com.appsflyer.AppsFlyerProperties.getInstance()     // Catch:{ Throwable -> 0x0913 }
            java.lang.String r4 = r4.getReferrer(r13)     // Catch:{ Throwable -> 0x0913 }
            r6 = 30000(0x7530, double:1.4822E-319)
            int r2 = (r2 > r6 ? 1 : (r2 == r6 ? 0 : -1))
            if (r2 > 0) goto L_0x0c69
            if (r4 == 0) goto L_0x0c69
            java.lang.String r2 = "AppsFlyer_Test"
            boolean r2 = r4.contains(r2)     // Catch:{ Throwable -> 0x0913 }
            if (r2 == 0) goto L_0x0c69
            r2 = 1
        L_0x064a:
            if (r2 == 0) goto L_0x0679
            java.lang.String r2 = "testAppMode"
            java.lang.String r3 = "true"
            r5.put(r2, r3)     // Catch:{ Throwable -> 0x0913 }
            org.json.JSONObject r2 = new org.json.JSONObject     // Catch:{ Throwable -> 0x0913 }
            r2.<init>(r5)     // Catch:{ Throwable -> 0x0913 }
            java.lang.String r2 = r2.toString()     // Catch:{ Throwable -> 0x0913 }
            android.content.Intent r3 = new android.content.Intent     // Catch:{ Throwable -> 0x0913 }
            java.lang.String r4 = "com.appsflyer.testIntgrationBroadcast"
            r3.<init>(r4)     // Catch:{ Throwable -> 0x0913 }
            java.lang.String r4 = "params"
            r3.putExtra(r4, r2)     // Catch:{ Throwable -> 0x0913 }
            r13.sendBroadcast(r3)     // Catch:{ Throwable -> 0x0913 }
            java.lang.String r2 = "Sent params to test app"
            com.appsflyer.AFLogger.afInfoLog(r2)     // Catch:{ Throwable -> 0x0913 }
            java.lang.String r2 = "Test mode ended!"
            com.appsflyer.AFLogger.afInfoLog(r2)     // Catch:{ Throwable -> 0x0913 }
            r2 = 0
            r12.f161 = r2     // Catch:{ Throwable -> 0x0913 }
        L_0x0679:
            java.lang.String r2 = "advertiserId"
            com.appsflyer.AppsFlyerProperties r3 = com.appsflyer.AppsFlyerProperties.getInstance()     // Catch:{ Throwable -> 0x0913 }
            java.lang.String r2 = r3.getString(r2)     // Catch:{ Throwable -> 0x0913 }
            if (r2 != 0) goto L_0x069b
            com.appsflyer.C0454o.m325(r13, r5)     // Catch:{ Throwable -> 0x0913 }
            java.lang.String r2 = "advertiserId"
            com.appsflyer.AppsFlyerProperties r3 = com.appsflyer.AppsFlyerProperties.getInstance()     // Catch:{ Throwable -> 0x0913 }
            java.lang.String r2 = r3.getString(r2)     // Catch:{ Throwable -> 0x0913 }
            if (r2 == 0) goto L_0x0c6c
            java.lang.String r2 = "GAID_retry"
            java.lang.String r3 = "true"
            r5.put(r2, r3)     // Catch:{ Throwable -> 0x0913 }
        L_0x069b:
            android.content.ContentResolver r2 = r13.getContentResolver()     // Catch:{ Throwable -> 0x0913 }
            com.appsflyer.n r2 = com.appsflyer.C0454o.m324(r2)     // Catch:{ Throwable -> 0x0913 }
            if (r2 == 0) goto L_0x06bb
            java.lang.String r3 = "amazon_aid"
            java.lang.String r4 = r2.mo6598()     // Catch:{ Throwable -> 0x0913 }
            r5.put(r3, r4)     // Catch:{ Throwable -> 0x0913 }
            java.lang.String r3 = "amazon_aid_limit"
            boolean r2 = r2.mo6597()     // Catch:{ Throwable -> 0x0913 }
            java.lang.String r2 = java.lang.String.valueOf(r2)     // Catch:{ Throwable -> 0x0913 }
            r5.put(r3, r2)     // Catch:{ Throwable -> 0x0913 }
        L_0x06bb:
            com.appsflyer.AppsFlyerProperties r2 = com.appsflyer.AppsFlyerProperties.getInstance()     // Catch:{ Throwable -> 0x0913 }
            java.lang.String r2 = r2.getReferrer(r13)     // Catch:{ Throwable -> 0x0913 }
            if (r2 == 0) goto L_0x06d8
            int r3 = r2.length()     // Catch:{ Throwable -> 0x0913 }
            if (r3 <= 0) goto L_0x06d8
            java.lang.String r3 = "referrer"
            java.lang.Object r3 = r5.get(r3)     // Catch:{ Throwable -> 0x0913 }
            if (r3 != 0) goto L_0x06d8
            java.lang.String r3 = "referrer"
            r5.put(r3, r2)     // Catch:{ Throwable -> 0x0913 }
        L_0x06d8:
            java.lang.String r2 = "true"
            java.lang.String r3 = "sentSuccessfully"
            java.lang.String r4 = ""
            r0 = r19
            java.lang.String r3 = r0.getString(r3, r4)     // Catch:{ Throwable -> 0x0913 }
            boolean r3 = r2.equals(r3)     // Catch:{ Throwable -> 0x0913 }
            java.lang.String r2 = "sentRegisterRequestToAF"
            r4 = 0
            r0 = r19
            boolean r2 = r0.getBoolean(r2, r4)     // Catch:{ Throwable -> 0x0913 }
            java.lang.String r4 = "registeredUninstall"
            java.lang.Boolean r2 = java.lang.Boolean.valueOf(r2)     // Catch:{ Throwable -> 0x0913 }
            r5.put(r4, r2)     // Catch:{ Throwable -> 0x0913 }
            java.lang.String r2 = "appsFlyerCount"
            r0 = r19
            r1 = r20
            int r4 = m188(r0, r2, r1)     // Catch:{ Throwable -> 0x0913 }
            java.lang.String r2 = "counter"
            java.lang.String r6 = java.lang.Integer.toString(r4)     // Catch:{ Throwable -> 0x0913 }
            r5.put(r2, r6)     // Catch:{ Throwable -> 0x0913 }
            java.lang.String r6 = "iaecounter"
            if (r15 == 0) goto L_0x0c75
            r2 = 1
        L_0x0712:
            java.lang.String r7 = "appsFlyerInAppEventCount"
            r0 = r19
            int r2 = m188(r0, r7, r2)     // Catch:{ Throwable -> 0x0913 }
            java.lang.String r2 = java.lang.Integer.toString(r2)     // Catch:{ Throwable -> 0x0913 }
            r5.put(r6, r2)     // Catch:{ Throwable -> 0x0913 }
            if (r20 == 0) goto L_0x0744
            r2 = 1
            if (r4 != r2) goto L_0x0744
            com.appsflyer.AppsFlyerProperties r2 = com.appsflyer.AppsFlyerProperties.getInstance()     // Catch:{ Throwable -> 0x0913 }
            r2.setFirstLaunchCalled()     // Catch:{ Throwable -> 0x0913 }
            java.lang.String r2 = "waitForCustomerId"
            com.appsflyer.AppsFlyerProperties r6 = com.appsflyer.AppsFlyerProperties.getInstance()     // Catch:{ Throwable -> 0x0913 }
            r7 = 0
            boolean r2 = r6.getBoolean(r2, r7)     // Catch:{ Throwable -> 0x0913 }
            if (r2 == 0) goto L_0x0744
            java.lang.String r2 = "wait_cid"
            r6 = 1
            java.lang.String r6 = java.lang.Boolean.toString(r6)     // Catch:{ Throwable -> 0x0913 }
            r5.put(r2, r6)     // Catch:{ Throwable -> 0x0913 }
        L_0x0744:
            java.lang.String r6 = "isFirstCall"
            if (r3 != 0) goto L_0x0c78
            r2 = 1
        L_0x0749:
            java.lang.String r2 = java.lang.Boolean.toString(r2)     // Catch:{ Throwable -> 0x0913 }
            r5.put(r6, r2)     // Catch:{ Throwable -> 0x0913 }
            java.util.HashMap r2 = new java.util.HashMap     // Catch:{ Throwable -> 0x0913 }
            r2.<init>()     // Catch:{ Throwable -> 0x0913 }
            java.lang.String r3 = "cpu_abi"
            java.lang.String r6 = "ro.product.cpu.abi"
            java.lang.String r6 = m190(r6)     // Catch:{ Throwable -> 0x0913 }
            r2.put(r3, r6)     // Catch:{ Throwable -> 0x0913 }
            java.lang.String r3 = "cpu_abi2"
            java.lang.String r6 = "ro.product.cpu.abi2"
            java.lang.String r6 = m190(r6)     // Catch:{ Throwable -> 0x0913 }
            r2.put(r3, r6)     // Catch:{ Throwable -> 0x0913 }
            java.lang.String r3 = "arch"
            java.lang.String r6 = "os.arch"
            java.lang.String r6 = m190(r6)     // Catch:{ Throwable -> 0x0913 }
            r2.put(r3, r6)     // Catch:{ Throwable -> 0x0913 }
            java.lang.String r3 = "build_display_id"
            java.lang.String r6 = "ro.build.display.id"
            java.lang.String r6 = m190(r6)     // Catch:{ Throwable -> 0x0913 }
            r2.put(r3, r6)     // Catch:{ Throwable -> 0x0913 }
            if (r20 == 0) goto L_0x07ff
            boolean r3 = r12.f159     // Catch:{ Throwable -> 0x0913 }
            if (r3 == 0) goto L_0x07c7
            com.appsflyer.j r3 = com.appsflyer.C0444j.C0445d.f292     // Catch:{ Throwable -> 0x0913 }
            android.location.Location r3 = com.appsflyer.C0444j.m310(r13)     // Catch:{ Throwable -> 0x0913 }
            java.util.HashMap r6 = new java.util.HashMap     // Catch:{ Throwable -> 0x0913 }
            r7 = 3
            r6.<init>(r7)     // Catch:{ Throwable -> 0x0913 }
            if (r3 == 0) goto L_0x07bc
            java.lang.String r7 = "lat"
            double r8 = r3.getLatitude()     // Catch:{ Throwable -> 0x0913 }
            java.lang.String r8 = java.lang.String.valueOf(r8)     // Catch:{ Throwable -> 0x0913 }
            r6.put(r7, r8)     // Catch:{ Throwable -> 0x0913 }
            java.lang.String r7 = "lon"
            double r8 = r3.getLongitude()     // Catch:{ Throwable -> 0x0913 }
            java.lang.String r8 = java.lang.String.valueOf(r8)     // Catch:{ Throwable -> 0x0913 }
            r6.put(r7, r8)     // Catch:{ Throwable -> 0x0913 }
            java.lang.String r7 = "ts"
            long r8 = r3.getTime()     // Catch:{ Throwable -> 0x0913 }
            java.lang.String r3 = java.lang.String.valueOf(r8)     // Catch:{ Throwable -> 0x0913 }
            r6.put(r7, r3)     // Catch:{ Throwable -> 0x0913 }
        L_0x07bc:
            boolean r3 = r6.isEmpty()     // Catch:{ Throwable -> 0x0913 }
            if (r3 != 0) goto L_0x07c7
            java.lang.String r3 = "loc"
            r2.put(r3, r6)     // Catch:{ Throwable -> 0x0913 }
        L_0x07c7:
            com.appsflyer.c r3 = com.appsflyer.C0429c.C0430c.f236     // Catch:{ Throwable -> 0x0913 }
            com.appsflyer.c$e r3 = r3.mo6540(r13)     // Catch:{ Throwable -> 0x0913 }
            java.lang.String r6 = "btl"
            float r7 = r3.mo6541()     // Catch:{ Throwable -> 0x0913 }
            java.lang.String r7 = java.lang.Float.toString(r7)     // Catch:{ Throwable -> 0x0913 }
            r2.put(r6, r7)     // Catch:{ Throwable -> 0x0913 }
            java.lang.String r6 = r3.mo6542()     // Catch:{ Throwable -> 0x0913 }
            if (r6 == 0) goto L_0x07e9
            java.lang.String r6 = "btch"
            java.lang.String r3 = r3.mo6542()     // Catch:{ Throwable -> 0x0913 }
            r2.put(r6, r3)     // Catch:{ Throwable -> 0x0913 }
        L_0x07e9:
            r3 = 2
            if (r3 < r4) goto L_0x07ff
            com.appsflyer.f r3 = com.appsflyer.C0434f.m284(r13)     // Catch:{ Throwable -> 0x0913 }
            java.util.List r3 = r3.mo6563()     // Catch:{ Throwable -> 0x0913 }
            boolean r4 = r3.isEmpty()     // Catch:{ Throwable -> 0x0913 }
            if (r4 != 0) goto L_0x07ff
            java.lang.String r4 = "sensors"
            r2.put(r4, r3)     // Catch:{ Throwable -> 0x0913 }
        L_0x07ff:
            java.util.Map r3 = com.appsflyer.AFScreenManager.getScreenMetrics(r13)     // Catch:{ Throwable -> 0x0913 }
            java.lang.String r4 = "dim"
            r2.put(r4, r3)     // Catch:{ Throwable -> 0x0913 }
            java.lang.String r3 = "deviceData"
            r5.put(r3, r2)     // Catch:{ Throwable -> 0x0913 }
            com.appsflyer.r r2 = new com.appsflyer.r     // Catch:{ Throwable -> 0x0913 }
            r2.<init>()     // Catch:{ Throwable -> 0x0913 }
            java.lang.String r2 = "appsflyerKey"
            java.lang.Object r2 = r5.get(r2)     // Catch:{ Throwable -> 0x0913 }
            java.lang.String r2 = (java.lang.String) r2     // Catch:{ Throwable -> 0x0913 }
            java.lang.String r3 = "af_timestamp"
            java.lang.Object r3 = r5.get(r3)     // Catch:{ Throwable -> 0x0913 }
            java.lang.String r3 = (java.lang.String) r3     // Catch:{ Throwable -> 0x0913 }
            java.lang.String r4 = "uid"
            java.lang.Object r4 = r5.get(r4)     // Catch:{ Throwable -> 0x0913 }
            java.lang.String r4 = (java.lang.String) r4     // Catch:{ Throwable -> 0x0913 }
            java.lang.StringBuilder r6 = new java.lang.StringBuilder     // Catch:{ Throwable -> 0x0913 }
            r6.<init>()     // Catch:{ Throwable -> 0x0913 }
            r7 = 0
            r8 = 7
            java.lang.String r2 = r2.substring(r7, r8)     // Catch:{ Throwable -> 0x0913 }
            java.lang.StringBuilder r2 = r6.append(r2)     // Catch:{ Throwable -> 0x0913 }
            r6 = 0
            r7 = 7
            java.lang.String r4 = r4.substring(r6, r7)     // Catch:{ Throwable -> 0x0913 }
            java.lang.StringBuilder r2 = r2.append(r4)     // Catch:{ Throwable -> 0x0913 }
            int r4 = r3.length()     // Catch:{ Throwable -> 0x0913 }
            int r4 = r4 + -7
            java.lang.String r3 = r3.substring(r4)     // Catch:{ Throwable -> 0x0913 }
            java.lang.StringBuilder r2 = r2.append(r3)     // Catch:{ Throwable -> 0x0913 }
            java.lang.String r2 = r2.toString()     // Catch:{ Throwable -> 0x0913 }
            java.lang.String r2 = com.appsflyer.C0459r.m341(r2)     // Catch:{ Throwable -> 0x0913 }
            java.lang.String r3 = "af_v"
            r5.put(r3, r2)     // Catch:{ Throwable -> 0x0913 }
            com.appsflyer.r r2 = new com.appsflyer.r     // Catch:{ Throwable -> 0x0913 }
            r2.<init>()     // Catch:{ Throwable -> 0x0913 }
            java.lang.String r2 = "appsflyerKey"
            java.lang.Object r2 = r5.get(r2)     // Catch:{ Throwable -> 0x0913 }
            java.lang.String r2 = (java.lang.String) r2     // Catch:{ Throwable -> 0x0913 }
            java.lang.StringBuilder r3 = new java.lang.StringBuilder     // Catch:{ Throwable -> 0x0913 }
            r3.<init>()     // Catch:{ Throwable -> 0x0913 }
            java.lang.StringBuilder r2 = r3.append(r2)     // Catch:{ Throwable -> 0x0913 }
            java.lang.String r3 = "af_timestamp"
            java.lang.Object r3 = r5.get(r3)     // Catch:{ Throwable -> 0x0913 }
            java.lang.StringBuilder r2 = r2.append(r3)     // Catch:{ Throwable -> 0x0913 }
            java.lang.String r2 = r2.toString()     // Catch:{ Throwable -> 0x0913 }
            java.lang.StringBuilder r3 = new java.lang.StringBuilder     // Catch:{ Throwable -> 0x0913 }
            r3.<init>()     // Catch:{ Throwable -> 0x0913 }
            java.lang.StringBuilder r2 = r3.append(r2)     // Catch:{ Throwable -> 0x0913 }
            java.lang.String r3 = "uid"
            java.lang.Object r3 = r5.get(r3)     // Catch:{ Throwable -> 0x0913 }
            java.lang.StringBuilder r2 = r2.append(r3)     // Catch:{ Throwable -> 0x0913 }
            java.lang.String r2 = r2.toString()     // Catch:{ Throwable -> 0x0913 }
            java.lang.StringBuilder r3 = new java.lang.StringBuilder     // Catch:{ Throwable -> 0x0913 }
            r3.<init>()     // Catch:{ Throwable -> 0x0913 }
            java.lang.StringBuilder r2 = r3.append(r2)     // Catch:{ Throwable -> 0x0913 }
            java.lang.String r3 = "installDate"
            java.lang.Object r3 = r5.get(r3)     // Catch:{ Throwable -> 0x0913 }
            java.lang.StringBuilder r2 = r2.append(r3)     // Catch:{ Throwable -> 0x0913 }
            java.lang.String r2 = r2.toString()     // Catch:{ Throwable -> 0x0913 }
            java.lang.StringBuilder r3 = new java.lang.StringBuilder     // Catch:{ Throwable -> 0x0913 }
            r3.<init>()     // Catch:{ Throwable -> 0x0913 }
            java.lang.StringBuilder r2 = r3.append(r2)     // Catch:{ Throwable -> 0x0913 }
            java.lang.String r3 = "counter"
            java.lang.Object r3 = r5.get(r3)     // Catch:{ Throwable -> 0x0913 }
            java.lang.StringBuilder r2 = r2.append(r3)     // Catch:{ Throwable -> 0x0913 }
            java.lang.String r2 = r2.toString()     // Catch:{ Throwable -> 0x0913 }
            java.lang.StringBuilder r3 = new java.lang.StringBuilder     // Catch:{ Throwable -> 0x0913 }
            r3.<init>()     // Catch:{ Throwable -> 0x0913 }
            java.lang.StringBuilder r2 = r3.append(r2)     // Catch:{ Throwable -> 0x0913 }
            java.lang.String r3 = "iaecounter"
            java.lang.Object r3 = r5.get(r3)     // Catch:{ Throwable -> 0x0913 }
            java.lang.StringBuilder r2 = r2.append(r3)     // Catch:{ Throwable -> 0x0913 }
            java.lang.String r2 = r2.toString()     // Catch:{ Throwable -> 0x0913 }
            java.lang.String r2 = com.appsflyer.C0459r.m340(r2)     // Catch:{ Throwable -> 0x0913 }
            java.lang.String r2 = com.appsflyer.C0459r.m341(r2)     // Catch:{ Throwable -> 0x0913 }
            java.lang.String r3 = "af_v2"
            r5.put(r3, r2)     // Catch:{ Throwable -> 0x0913 }
            java.lang.String r2 = "is_stop_tracking_used"
            r0 = r19
            boolean r2 = r0.contains(r2)     // Catch:{ Throwable -> 0x0913 }
            if (r2 == 0) goto L_0x0907
            java.lang.String r2 = "istu"
            java.lang.String r3 = "is_stop_tracking_used"
            r4 = 0
            r0 = r19
            boolean r3 = r0.getBoolean(r3, r4)     // Catch:{ Throwable -> 0x0913 }
            java.lang.String r3 = java.lang.String.valueOf(r3)     // Catch:{ Throwable -> 0x0913 }
            r5.put(r2, r3)     // Catch:{ Throwable -> 0x0913 }
        L_0x0907:
            r2 = r5
        L_0x0908:
            return r2
        L_0x0909:
            r2 = r15
            goto L_0x0036
        L_0x090c:
            java.lang.String r2 = "SDK tracking has been stopped"
            com.appsflyer.AFLogger.afInfoLog(r2)     // Catch:{ Throwable -> 0x0913 }
            goto L_0x0041
        L_0x0913:
            r2 = move-exception
            java.lang.String r3 = r2.getLocalizedMessage()
            com.appsflyer.AFLogger.afErrorLog(r3, r2)
            goto L_0x0907
        L_0x091c:
            r2 = r15
            goto L_0x0049
        L_0x091f:
            r2 = move-exception
            java.lang.String r3 = "Exception while validation permissions. "
            com.appsflyer.AFLogger.afErrorLog(r3, r2)     // Catch:{ Throwable -> 0x0913 }
            goto L_0x0095
        L_0x0927:
            r2 = 0
            goto L_0x00de
        L_0x092a:
            r2 = 0
            goto L_0x00fc
        L_0x092d:
            r2 = 0
            goto L_0x010b
        L_0x0930:
            r2 = 0
            goto L_0x011a
        L_0x0933:
            r2 = 0
            goto L_0x0129
        L_0x0936:
            r2 = 0
            goto L_0x0138
        L_0x0939:
            r2 = 0
            goto L_0x0147
        L_0x093c:
            r2 = 0
            goto L_0x0156
        L_0x093f:
            r2 = 0
            goto L_0x0165
        L_0x0942:
            r2 = 0
            goto L_0x0174
        L_0x0945:
            r2 = 0
            goto L_0x0183
        L_0x0948:
            r3 = 0
            goto L_0x01a8
        L_0x094b:
            r2.mo6412()     // Catch:{ Throwable -> 0x0913 }
            java.lang.String r3 = "KSAppsFlyerId"
            java.lang.String r4 = r2.mo6410()     // Catch:{ Throwable -> 0x0913 }
            com.appsflyer.AppsFlyerProperties r6 = com.appsflyer.AppsFlyerProperties.getInstance()     // Catch:{ Throwable -> 0x0913 }
            r6.set(r3, r4)     // Catch:{ Throwable -> 0x0913 }
            java.lang.String r3 = "KSAppsFlyerRICounter"
            int r2 = r2.mo6414()     // Catch:{ Throwable -> 0x0913 }
            java.lang.String r2 = java.lang.String.valueOf(r2)     // Catch:{ Throwable -> 0x0913 }
            com.appsflyer.AppsFlyerProperties r4 = com.appsflyer.AppsFlyerProperties.getInstance()     // Catch:{ Throwable -> 0x0913 }
            r4.set(r3, r2)     // Catch:{ Throwable -> 0x0913 }
            goto L_0x0204
        L_0x096e:
            java.lang.StringBuilder r2 = new java.lang.StringBuilder     // Catch:{ Throwable -> 0x0913 }
            java.lang.String r3 = "OS SDK is="
            r2.<init>(r3)     // Catch:{ Throwable -> 0x0913 }
            int r3 = android.os.Build.VERSION.SDK_INT     // Catch:{ Throwable -> 0x0913 }
            java.lang.StringBuilder r2 = r2.append(r3)     // Catch:{ Throwable -> 0x0913 }
            java.lang.String r3 = "; no KeyStore usage"
            java.lang.StringBuilder r2 = r2.append(r3)     // Catch:{ Throwable -> 0x0913 }
            java.lang.String r2 = r2.toString()     // Catch:{ Throwable -> 0x0913 }
            com.appsflyer.AFLogger.afRDLog(r2)     // Catch:{ Throwable -> 0x0913 }
            goto L_0x0204
        L_0x098a:
            r2 = -1
            goto L_0x0229
        L_0x098e:
            java.lang.String r2 = "appsflyer-data"
            r3 = 0
            android.content.SharedPreferences r2 = r13.getSharedPreferences(r2, r3)     // Catch:{ Throwable -> 0x0913 }
            android.content.SharedPreferences$Editor r3 = r2.edit()     // Catch:{ Throwable -> 0x0913 }
            java.lang.String r4 = "prev_event_name"
            r6 = 0
            java.lang.String r4 = r2.getString(r4, r6)     // Catch:{ Exception -> 0x09fb }
            if (r4 == 0) goto L_0x09db
            org.json.JSONObject r6 = new org.json.JSONObject     // Catch:{ Exception -> 0x09fb }
            r6.<init>()     // Catch:{ Exception -> 0x09fb }
            java.lang.String r7 = "prev_event_timestamp"
            java.lang.StringBuilder r8 = new java.lang.StringBuilder     // Catch:{ Exception -> 0x09fb }
            r8.<init>()     // Catch:{ Exception -> 0x09fb }
            java.lang.String r9 = "prev_event_timestamp"
            r10 = -1
            long r10 = r2.getLong(r9, r10)     // Catch:{ Exception -> 0x09fb }
            java.lang.StringBuilder r8 = r8.append(r10)     // Catch:{ Exception -> 0x09fb }
            java.lang.String r8 = r8.toString()     // Catch:{ Exception -> 0x09fb }
            r6.put(r7, r8)     // Catch:{ Exception -> 0x09fb }
            java.lang.String r7 = "prev_event_value"
            java.lang.String r8 = "prev_event_value"
            r9 = 0
            java.lang.String r2 = r2.getString(r8, r9)     // Catch:{ Exception -> 0x09fb }
            r6.put(r7, r2)     // Catch:{ Exception -> 0x09fb }
            java.lang.String r2 = "prev_event_name"
            r6.put(r2, r4)     // Catch:{ Exception -> 0x09fb }
            java.lang.String r2 = "prev_event"
            java.lang.String r4 = r6.toString()     // Catch:{ Exception -> 0x09fb }
            r5.put(r2, r4)     // Catch:{ Exception -> 0x09fb }
        L_0x09db:
            java.lang.String r2 = "prev_event_name"
            r3.putString(r2, r15)     // Catch:{ Exception -> 0x09fb }
            java.lang.String r2 = "prev_event_value"
            r0 = r16
            r3.putString(r2, r0)     // Catch:{ Exception -> 0x09fb }
            java.lang.String r2 = "prev_event_timestamp"
            long r6 = java.lang.System.currentTimeMillis()     // Catch:{ Exception -> 0x09fb }
            r3.putLong(r2, r6)     // Catch:{ Exception -> 0x09fb }
            int r2 = android.os.Build.VERSION.SDK_INT     // Catch:{ Exception -> 0x09fb }
            r4 = 9
            if (r2 < r4) goto L_0x0a03
            r3.apply()     // Catch:{ Exception -> 0x09fb }
            goto L_0x0250
        L_0x09fb:
            r2 = move-exception
            java.lang.String r3 = "Error while processing previous event."
            com.appsflyer.AFLogger.afErrorLog(r3, r2)     // Catch:{ Throwable -> 0x0913 }
            goto L_0x0250
        L_0x0a03:
            r3.commit()     // Catch:{ Exception -> 0x09fb }
            goto L_0x0250
        L_0x0a08:
            r2 = move-exception
            java.lang.String r3 = "Exception while getting the app's installer package. "
            com.appsflyer.AFLogger.afErrorLog(r3, r2)     // Catch:{ Throwable -> 0x0913 }
            goto L_0x02a0
        L_0x0a10:
            java.lang.String r2 = "appsflyer-data"
            r3 = 0
            android.content.SharedPreferences r2 = r13.getSharedPreferences(r2, r3)     // Catch:{ Throwable -> 0x0913 }
            java.lang.String r3 = "appsFlyerCount"
            boolean r2 = r2.contains(r3)     // Catch:{ Throwable -> 0x0913 }
            if (r2 != 0) goto L_0x0a34
            r2 = 1
        L_0x0a20:
            if (r2 == 0) goto L_0x0a36
            java.lang.ref.WeakReference r2 = new java.lang.ref.WeakReference     // Catch:{ Throwable -> 0x0913 }
            r2.<init>(r13)     // Catch:{ Throwable -> 0x0913 }
            java.lang.String r3 = "AF_STORE"
            java.lang.String r2 = m204(r2, r3)     // Catch:{ Throwable -> 0x0913 }
        L_0x0a2d:
            java.lang.String r3 = "INSTALL_STORE"
            m232(r13, r3, r2)     // Catch:{ Throwable -> 0x0913 }
            goto L_0x0300
        L_0x0a34:
            r2 = 0
            goto L_0x0a20
        L_0x0a36:
            r2 = 0
            goto L_0x0a2d
        L_0x0a38:
            java.lang.String r3 = "appsflyer-data"
            r4 = 0
            android.content.SharedPreferences r3 = r13.getSharedPreferences(r3, r4)     // Catch:{ Throwable -> 0x0913 }
            java.lang.String r4 = "appsFlyerCount"
            boolean r3 = r3.contains(r4)     // Catch:{ Throwable -> 0x0913 }
            if (r3 != 0) goto L_0x0ab4
            r3 = 1
        L_0x0a48:
            if (r3 == 0) goto L_0x0aab
            java.lang.String r2 = "ro.appsflyer.preinstall.path"
            java.lang.String r2 = m190(r2)     // Catch:{ Throwable -> 0x0913 }
            java.io.File r2 = m222(r2)     // Catch:{ Throwable -> 0x0913 }
            if (r2 == 0) goto L_0x0a5c
            boolean r3 = r2.exists()     // Catch:{ Throwable -> 0x0913 }
            if (r3 != 0) goto L_0x0ab6
        L_0x0a5c:
            r3 = 1
        L_0x0a5d:
            if (r3 == 0) goto L_0x0a71
            java.lang.String r2 = "AF_PRE_INSTALL_PATH"
            android.content.pm.PackageManager r3 = r13.getPackageManager()     // Catch:{ Throwable -> 0x0913 }
            java.lang.String r4 = r13.getPackageName()     // Catch:{ Throwable -> 0x0913 }
            java.lang.String r2 = m229(r2, r3, r4)     // Catch:{ Throwable -> 0x0913 }
            java.io.File r2 = m222(r2)     // Catch:{ Throwable -> 0x0913 }
        L_0x0a71:
            if (r2 == 0) goto L_0x0a79
            boolean r3 = r2.exists()     // Catch:{ Throwable -> 0x0913 }
            if (r3 != 0) goto L_0x0ab8
        L_0x0a79:
            r3 = 1
        L_0x0a7a:
            if (r3 == 0) goto L_0x0a82
            java.lang.String r2 = "/data/local/tmp/pre_install.appsflyer"
            java.io.File r2 = m222(r2)     // Catch:{ Throwable -> 0x0913 }
        L_0x0a82:
            if (r2 == 0) goto L_0x0a8a
            boolean r3 = r2.exists()     // Catch:{ Throwable -> 0x0913 }
            if (r3 != 0) goto L_0x0aba
        L_0x0a8a:
            r3 = 1
        L_0x0a8b:
            if (r3 == 0) goto L_0x0c81
            java.lang.String r2 = "/etc/pre_install.appsflyer"
            java.io.File r2 = m222(r2)     // Catch:{ Throwable -> 0x0913 }
            r3 = r2
        L_0x0a94:
            if (r3 == 0) goto L_0x0a9c
            boolean r2 = r3.exists()     // Catch:{ Throwable -> 0x0913 }
            if (r2 != 0) goto L_0x0abc
        L_0x0a9c:
            r2 = 1
        L_0x0a9d:
            if (r2 != 0) goto L_0x0abe
            java.lang.String r2 = r13.getPackageName()     // Catch:{ Throwable -> 0x0913 }
            java.lang.String r2 = m202(r3, r2)     // Catch:{ Throwable -> 0x0913 }
            if (r2 == 0) goto L_0x0abe
        L_0x0aa9:
            if (r2 == 0) goto L_0x0ac0
        L_0x0aab:
            if (r2 == 0) goto L_0x032d
            java.lang.String r3 = "preInstallName"
            m232(r13, r3, r2)     // Catch:{ Throwable -> 0x0913 }
            goto L_0x032d
        L_0x0ab4:
            r3 = 0
            goto L_0x0a48
        L_0x0ab6:
            r3 = 0
            goto L_0x0a5d
        L_0x0ab8:
            r3 = 0
            goto L_0x0a7a
        L_0x0aba:
            r3 = 0
            goto L_0x0a8b
        L_0x0abc:
            r2 = 0
            goto L_0x0a9d
        L_0x0abe:
            r2 = 0
            goto L_0x0aa9
        L_0x0ac0:
            java.lang.ref.WeakReference r2 = new java.lang.ref.WeakReference     // Catch:{ Throwable -> 0x0913 }
            r2.<init>(r13)     // Catch:{ Throwable -> 0x0913 }
            java.lang.String r3 = "AF_PRE_INSTALL_NAME"
            java.lang.String r2 = m204(r2, r3)     // Catch:{ Throwable -> 0x0913 }
            goto L_0x0aab
        L_0x0acc:
            java.lang.String r2 = "AppsFlyerKey"
            com.appsflyer.AppsFlyerProperties r3 = com.appsflyer.AppsFlyerProperties.getInstance()     // Catch:{ Throwable -> 0x0913 }
            java.lang.String r2 = r3.getString(r2)     // Catch:{ Throwable -> 0x0913 }
            if (r2 == 0) goto L_0x0ae5
            int r3 = r2.length()     // Catch:{ Throwable -> 0x0913 }
            if (r3 < 0) goto L_0x0ae5
            java.lang.String r3 = "appsflyerKey"
            r5.put(r3, r2)     // Catch:{ Throwable -> 0x0913 }
            goto L_0x0366
        L_0x0ae5:
            java.lang.String r2 = "AppsFlyer dev key is missing!!! Please use  AppsFlyerLib.getInstance().setAppsFlyerKey(...) to set it. "
            com.appsflyer.AFLogger.afInfoLog(r2)     // Catch:{ Throwable -> 0x0913 }
            java.lang.String r2 = "AppsFlyer_4.8.11"
            java.lang.String r3 = "DEV_KEY_MISSING"
            r4 = 0
            m207(r13, r2, r3, r4)     // Catch:{ Throwable -> 0x0913 }
            java.lang.String r2 = "AppsFlyer will not track this event."
            com.appsflyer.AFLogger.afInfoLog(r2)     // Catch:{ Throwable -> 0x0913 }
            r2 = 0
            goto L_0x0908
        L_0x0afa:
            java.lang.String r2 = "userEmail"
            com.appsflyer.AppsFlyerProperties r3 = com.appsflyer.AppsFlyerProperties.getInstance()     // Catch:{ Throwable -> 0x0913 }
            java.lang.String r2 = r3.getString(r2)     // Catch:{ Throwable -> 0x0913 }
            if (r2 == 0) goto L_0x0388
            java.lang.String r3 = "sha1_el"
            java.lang.String r2 = com.appsflyer.C0459r.m341(r2)     // Catch:{ Throwable -> 0x0913 }
            r5.put(r3, r2)     // Catch:{ Throwable -> 0x0913 }
            goto L_0x0388
        L_0x0b11:
            r2 = move-exception
            r2 = 0
            java.lang.String r3 = "Exception while collecting facebook's attribution ID. "
            com.appsflyer.AFLogger.afWarnLog(r3)     // Catch:{ Throwable -> 0x0913 }
            goto L_0x0420
        L_0x0b1a:
            r2 = move-exception
            r3 = 0
            java.lang.String r4 = "Exception while collecting facebook's attribution ID. "
            com.appsflyer.AFLogger.afErrorLog(r4, r2)     // Catch:{ Throwable -> 0x0913 }
            r2 = r3
            goto L_0x0420
        L_0x0b24:
            java.lang.String r2 = "appsflyer-data"
            r3 = 0
            android.content.SharedPreferences r6 = r13.getSharedPreferences(r2, r3)     // Catch:{ Throwable -> 0x0913 }
            com.appsflyer.AppsFlyerProperties r2 = com.appsflyer.AppsFlyerProperties.getInstance()     // Catch:{ Throwable -> 0x0913 }
            java.lang.String r3 = "collectIMEI"
            r4 = 1
            boolean r2 = r2.getBoolean(r3, r4)     // Catch:{ Throwable -> 0x0913 }
            java.lang.String r3 = "imeiCached"
            r4 = 0
            java.lang.String r3 = r6.getString(r3, r4)     // Catch:{ Throwable -> 0x0913 }
            r4 = 0
            if (r2 == 0) goto L_0x0bde
            int r2 = android.os.Build.VERSION.SDK_INT     // Catch:{ Throwable -> 0x0913 }
            r7 = 19
            if (r2 < r7) goto L_0x0b4c
            boolean r2 = m209(r13)     // Catch:{ Throwable -> 0x0913 }
            if (r2 != 0) goto L_0x0bbb
        L_0x0b4c:
            r2 = 1
        L_0x0b4d:
            if (r2 == 0) goto L_0x0bd7
            java.lang.String r2 = "phone"
            java.lang.Object r2 = r13.getSystemService(r2)     // Catch:{ InvocationTargetException -> 0x0bc9, Exception -> 0x0bd0 }
            android.telephony.TelephonyManager r2 = (android.telephony.TelephonyManager) r2     // Catch:{ InvocationTargetException -> 0x0bc9, Exception -> 0x0bd0 }
            java.lang.Class r7 = r2.getClass()     // Catch:{ InvocationTargetException -> 0x0bc9, Exception -> 0x0bd0 }
            java.lang.String r8 = "getDeviceId"
            r9 = 0
            java.lang.Class[] r9 = new java.lang.Class[r9]     // Catch:{ InvocationTargetException -> 0x0bc9, Exception -> 0x0bd0 }
            java.lang.reflect.Method r7 = r7.getMethod(r8, r9)     // Catch:{ InvocationTargetException -> 0x0bc9, Exception -> 0x0bd0 }
            r8 = 0
            java.lang.Object[] r8 = new java.lang.Object[r8]     // Catch:{ InvocationTargetException -> 0x0bc9, Exception -> 0x0bd0 }
            java.lang.Object r2 = r7.invoke(r2, r8)     // Catch:{ InvocationTargetException -> 0x0bc9, Exception -> 0x0bd0 }
            java.lang.String r2 = (java.lang.String) r2     // Catch:{ InvocationTargetException -> 0x0bc9, Exception -> 0x0bd0 }
            if (r2 == 0) goto L_0x0bbd
            r4 = r2
        L_0x0b70:
            if (r4 == 0) goto L_0x0be5
            java.lang.String r2 = "imeiCached"
            m232(r13, r2, r4)     // Catch:{ Throwable -> 0x0913 }
            java.lang.String r2 = "imei"
            r5.put(r2, r4)     // Catch:{ Throwable -> 0x0913 }
        L_0x0b7c:
            com.appsflyer.AppsFlyerProperties r2 = com.appsflyer.AppsFlyerProperties.getInstance()     // Catch:{ Throwable -> 0x0913 }
            java.lang.String r3 = "collectAndroidId"
            r4 = 1
            boolean r4 = r2.getBoolean(r3, r4)     // Catch:{ Throwable -> 0x0913 }
            java.lang.String r2 = "androidIdCached"
            r3 = 0
            java.lang.String r2 = r6.getString(r2, r3)     // Catch:{ Throwable -> 0x0913 }
            r3 = 0
            if (r4 == 0) goto L_0x0c08
            int r4 = android.os.Build.VERSION.SDK_INT     // Catch:{ Throwable -> 0x0913 }
            r6 = 19
            if (r4 < r6) goto L_0x0b9d
            boolean r4 = m209(r13)     // Catch:{ Throwable -> 0x0913 }
            if (r4 != 0) goto L_0x0beb
        L_0x0b9d:
            r4 = 1
        L_0x0b9e:
            if (r4 == 0) goto L_0x0c01
            android.content.ContentResolver r4 = r13.getContentResolver()     // Catch:{ Exception -> 0x0bf8 }
            java.lang.String r6 = "android_id"
            java.lang.String r4 = android.provider.Settings.Secure.getString(r4, r6)     // Catch:{ Exception -> 0x0bf8 }
            if (r4 == 0) goto L_0x0bed
            r3 = r4
        L_0x0bad:
            if (r3 == 0) goto L_0x0c0f
            java.lang.String r2 = "androidIdCached"
            m232(r13, r2, r3)     // Catch:{ Throwable -> 0x0913 }
            java.lang.String r2 = "android_id"
            r5.put(r2, r3)     // Catch:{ Throwable -> 0x0913 }
            goto L_0x043b
        L_0x0bbb:
            r2 = 0
            goto L_0x0b4d
        L_0x0bbd:
            java.lang.String r2 = r12.f160     // Catch:{ InvocationTargetException -> 0x0bc9, Exception -> 0x0bd0 }
            if (r2 == 0) goto L_0x0bc4
            java.lang.String r4 = r12.f160     // Catch:{ InvocationTargetException -> 0x0bc9, Exception -> 0x0bd0 }
            goto L_0x0b70
        L_0x0bc4:
            if (r3 == 0) goto L_0x0c7e
            r2 = r3
        L_0x0bc7:
            r4 = r2
            goto L_0x0b70
        L_0x0bc9:
            r2 = move-exception
            java.lang.String r2 = "WARNING: READ_PHONE_STATE is missing."
            com.appsflyer.AFLogger.afWarnLog(r2)     // Catch:{ Throwable -> 0x0913 }
            goto L_0x0b70
        L_0x0bd0:
            r2 = move-exception
            java.lang.String r3 = "WARNING: READ_PHONE_STATE is missing. "
            com.appsflyer.AFLogger.afErrorLog(r3, r2)     // Catch:{ Throwable -> 0x0913 }
            goto L_0x0b70
        L_0x0bd7:
            java.lang.String r2 = r12.f160     // Catch:{ Throwable -> 0x0913 }
            if (r2 == 0) goto L_0x0b70
            java.lang.String r4 = r12.f160     // Catch:{ Throwable -> 0x0913 }
            goto L_0x0b70
        L_0x0bde:
            java.lang.String r2 = r12.f160     // Catch:{ Throwable -> 0x0913 }
            if (r2 == 0) goto L_0x0b70
            java.lang.String r4 = r12.f160     // Catch:{ Throwable -> 0x0913 }
            goto L_0x0b70
        L_0x0be5:
            java.lang.String r2 = "IMEI was not collected."
            com.appsflyer.AFLogger.afInfoLog(r2)     // Catch:{ Throwable -> 0x0913 }
            goto L_0x0b7c
        L_0x0beb:
            r4 = 0
            goto L_0x0b9e
        L_0x0bed:
            java.lang.String r4 = r12.f164     // Catch:{ Exception -> 0x0bf8 }
            if (r4 == 0) goto L_0x0bf4
            java.lang.String r3 = r12.f164     // Catch:{ Exception -> 0x0bf8 }
            goto L_0x0bad
        L_0x0bf4:
            if (r2 == 0) goto L_0x0c7b
        L_0x0bf6:
            r3 = r2
            goto L_0x0bad
        L_0x0bf8:
            r2 = move-exception
            java.lang.String r4 = r2.getMessage()     // Catch:{ Throwable -> 0x0913 }
            com.appsflyer.AFLogger.afErrorLog(r4, r2)     // Catch:{ Throwable -> 0x0913 }
            goto L_0x0bad
        L_0x0c01:
            java.lang.String r2 = r12.f164     // Catch:{ Throwable -> 0x0913 }
            if (r2 == 0) goto L_0x0bad
            java.lang.String r3 = r12.f164     // Catch:{ Throwable -> 0x0913 }
            goto L_0x0bad
        L_0x0c08:
            java.lang.String r2 = r12.f164     // Catch:{ Throwable -> 0x0913 }
            if (r2 == 0) goto L_0x0bad
            java.lang.String r3 = r12.f164     // Catch:{ Throwable -> 0x0913 }
            goto L_0x0bad
        L_0x0c0f:
            java.lang.String r2 = "Android ID was not collected."
            com.appsflyer.AFLogger.afInfoLog(r2)     // Catch:{ Throwable -> 0x0913 }
            goto L_0x043b
        L_0x0c16:
            r2 = move-exception
            java.lang.StringBuilder r3 = new java.lang.StringBuilder     // Catch:{ Throwable -> 0x0913 }
            java.lang.String r4 = "ERROR: could not get uid "
            r3.<init>(r4)     // Catch:{ Throwable -> 0x0913 }
            java.lang.String r4 = r2.getMessage()     // Catch:{ Throwable -> 0x0913 }
            java.lang.StringBuilder r3 = r3.append(r4)     // Catch:{ Throwable -> 0x0913 }
            java.lang.String r3 = r3.toString()     // Catch:{ Throwable -> 0x0913 }
            com.appsflyer.AFLogger.afErrorLog(r3, r2)     // Catch:{ Throwable -> 0x0913 }
            goto L_0x044b
        L_0x0c2f:
            r2 = move-exception
            java.lang.String r3 = "Exception while collecting display language name. "
            com.appsflyer.AFLogger.afErrorLog(r3, r2)     // Catch:{ Throwable -> 0x0913 }
            goto L_0x0458
        L_0x0c37:
            r2 = move-exception
            java.lang.String r3 = "Exception while collecting display language code. "
            com.appsflyer.AFLogger.afErrorLog(r3, r2)     // Catch:{ Throwable -> 0x0913 }
            goto L_0x0465
        L_0x0c3f:
            r2 = move-exception
            java.lang.String r3 = "Exception while collecting country name. "
            com.appsflyer.AFLogger.afErrorLog(r3, r2)     // Catch:{ Throwable -> 0x0913 }
            goto L_0x0472
        L_0x0c47:
            r2 = move-exception
            java.lang.String r4 = "Exception while collecting install date. "
            com.appsflyer.AFLogger.afErrorLog(r4, r2)     // Catch:{ Throwable -> 0x0913 }
            goto L_0x04b5
        L_0x0c4f:
            r2 = 0
            goto L_0x0546
        L_0x0c52:
            java.lang.String r2 = ""
            goto L_0x0556
        L_0x0c56:
            r2 = move-exception
            java.lang.String r3 = "Exception while collecting app version data "
            com.appsflyer.AFLogger.afErrorLog(r3, r2)     // Catch:{ Throwable -> 0x0913 }
            goto L_0x056d
        L_0x0c5e:
            android.net.Uri r2 = r12.f158     // Catch:{ Throwable -> 0x0913 }
            if (r2 == 0) goto L_0x0602
            android.net.Uri r2 = r12.f158     // Catch:{ Throwable -> 0x0913 }
            r12.m226(r13, r5, r2)     // Catch:{ Throwable -> 0x0913 }
            goto L_0x0602
        L_0x0c69:
            r2 = 0
            goto L_0x064a
        L_0x0c6c:
            java.lang.String r2 = "GAID_retry"
            java.lang.String r3 = "false"
            r5.put(r2, r3)     // Catch:{ Throwable -> 0x0913 }
            goto L_0x069b
        L_0x0c75:
            r2 = 0
            goto L_0x0712
        L_0x0c78:
            r2 = 0
            goto L_0x0749
        L_0x0c7b:
            r2 = r3
            goto L_0x0bf6
        L_0x0c7e:
            r2 = r4
            goto L_0x0bc7
        L_0x0c81:
            r3 = r2
            goto L_0x0a94
        */
        throw new UnsupportedOperationException("Method not decompiled: com.appsflyer.AppsFlyerLib.mo6483(android.content.Context, java.lang.String, java.lang.String, java.lang.String, java.lang.String, boolean, android.content.SharedPreferences, boolean, android.content.Intent):java.util.Map");
    }

    /* renamed from: ˏ */
    private void m226(Context context, Map<String, Object> map, Uri uri) {
        final Map hashMap;
        map.put("af_deeplink", uri.toString());
        if (uri.getQueryParameter("af_deeplink") != null) {
            this.f163 = "AppsFlyer_Test".equals(uri.getQueryParameter("media_source")) && Boolean.parseBoolean(uri.getQueryParameter("is_retargeting"));
            hashMap = m213(context, uri.getQuery());
            String str = "path";
            String path = uri.getPath();
            if (path != null) {
                hashMap.put(str, path);
            }
            String str2 = "scheme";
            String scheme = uri.getScheme();
            if (scheme != null) {
                hashMap.put(str2, scheme);
            }
            String str3 = "host";
            String host = uri.getHost();
            if (host != null) {
                hashMap.put(str3, host);
            }
        } else {
            hashMap = new HashMap();
            hashMap.put("link", uri.toString());
        }
        final WeakReference weakReference = new WeakReference(context);
        C0460s sVar = new C0460s(uri, this);
        sVar.setConnProvider(new HttpsUrlConnectionProvider());
        if (sVar.mo6610()) {
            sVar.mo6609(new C0461b() {
                /* renamed from: ॱ */
                public final void mo6488(String str) {
                    if (AppsFlyerLib.f149 != null) {
                        m244(hashMap);
                        AppsFlyerLib.f149.onAttributionFailure(str);
                    }
                }

                /* renamed from: ˋ */
                private void m244(Map<String, String> map) {
                    if (weakReference.get() != null) {
                        AppsFlyerLib.m232((Context) weakReference.get(), "deeplinkAttribution", new JSONObject(map).toString());
                    }
                }

                /* renamed from: ॱ */
                public final void mo6489(Map<String, String> map) {
                    for (String str : map.keySet()) {
                        hashMap.put(str, map.get(str));
                    }
                    m244(hashMap);
                    AppsFlyerLib.m234(hashMap);
                }
            });
            AFExecutor.getInstance().getThreadPoolExecutor().execute(sVar);
        } else if (f149 != null) {
            try {
                f149.onAppOpenAttribution(hashMap);
            } catch (Throwable th) {
                AFLogger.afErrorLog(th.getLocalizedMessage(), th);
            }
        }
    }

    /* renamed from: ˋ */
    private static boolean m209(Context context) {
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
        } catch (NameNotFoundException e) {
            AFLogger.afErrorLog("WARNING:  Google Play Services is unavailable. ", e);
            return false;
        }
    }

    /* renamed from: ˊ */
    private static String m190(String str) {
        try {
            return (String) Class.forName("android.os.SystemProperties").getMethod("get", new Class[]{String.class}).invoke(null, new Object[]{str});
        } catch (Throwable th) {
            AFLogger.afErrorLog(th.getMessage(), th);
            return null;
        }
    }

    @Nullable
    /* renamed from: ˋ */
    private static String m204(WeakReference<Context> weakReference, String str) {
        if (weakReference.get() == null) {
            return null;
        }
        return m229(str, ((Context) weakReference.get()).getPackageManager(), ((Context) weakReference.get()).getPackageName());
    }

    @Nullable
    /* renamed from: ॱ */
    private static String m229(String str, PackageManager packageManager, String str2) {
        try {
            Bundle bundle = packageManager.getApplicationInfo(str2, 128).metaData;
            if (bundle == null) {
                return null;
            }
            Object obj = bundle.get(str);
            if (obj != null) {
                return obj.toString();
            }
            return null;
        } catch (Throwable th) {
            AFLogger.afErrorLog(new StringBuilder("Could not find ").append(str).append(" value in the manifest").toString(), th);
            return null;
        }
    }

    public void setPreinstallAttribution(String str, String str2, String str3) {
        AFLogger.afDebugLog("setPreinstallAttribution API called");
        JSONObject jSONObject = new JSONObject();
        if (str != null) {
            try {
                jSONObject.put(Constants.URL_MEDIA_SOURCE, str);
            } catch (JSONException e) {
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

    /* JADX WARNING: type inference failed for: r3v0, types: [java.io.Reader] */
    /* JADX WARNING: type inference failed for: r3v1 */
    /* JADX WARNING: type inference failed for: r3v2, types: [java.io.Reader] */
    /* JADX WARNING: type inference failed for: r3v3 */
    /* JADX WARNING: type inference failed for: r1v5, types: [java.io.Reader] */
    /* JADX WARNING: type inference failed for: r3v4 */
    /* JADX WARNING: type inference failed for: r1v8 */
    /* JADX WARNING: type inference failed for: r1v9, types: [java.io.FileReader, java.io.Reader] */
    /* JADX WARNING: type inference failed for: r3v7 */
    /* JADX WARNING: type inference failed for: r3v9 */
    /* JADX WARNING: type inference failed for: r1v11 */
    /* JADX WARNING: Multi-variable type inference failed */
    /* JADX WARNING: Removed duplicated region for block: B:24:0x0057 A[SYNTHETIC, Splitter:B:24:0x0057] */
    /* JADX WARNING: Removed duplicated region for block: B:31:0x0069 A[SYNTHETIC, Splitter:B:31:0x0069] */
    /* JADX WARNING: Unknown variable types count: 4 */
    /* renamed from: ˋ */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    private static java.lang.String m202(java.io.File r4, java.lang.String r5) {
        /*
            r0 = 0
            java.util.Properties r2 = new java.util.Properties     // Catch:{ FileNotFoundException -> 0x0024, Throwable -> 0x004b, all -> 0x0064 }
            r2.<init>()     // Catch:{ FileNotFoundException -> 0x0024, Throwable -> 0x004b, all -> 0x0064 }
            java.io.FileReader r1 = new java.io.FileReader     // Catch:{ FileNotFoundException -> 0x0024, Throwable -> 0x004b, all -> 0x0064 }
            r1.<init>(r4)     // Catch:{ FileNotFoundException -> 0x0024, Throwable -> 0x004b, all -> 0x0064 }
            r2.load(r1)     // Catch:{ FileNotFoundException -> 0x0080, Throwable -> 0x007d }
            java.lang.String r3 = "Found PreInstall property!"
            com.appsflyer.AFLogger.afInfoLog(r3)     // Catch:{ FileNotFoundException -> 0x0080, Throwable -> 0x007d }
            java.lang.String r0 = r2.getProperty(r5)     // Catch:{ FileNotFoundException -> 0x0080, Throwable -> 0x007d }
            r1.close()     // Catch:{ Throwable -> 0x001b }
        L_0x001a:
            return r0
        L_0x001b:
            r1 = move-exception
            java.lang.String r2 = r1.getMessage()
            com.appsflyer.AFLogger.afErrorLog(r2, r1)
            goto L_0x001a
        L_0x0024:
            r1 = move-exception
            r1 = r0
        L_0x0026:
            java.lang.StringBuilder r2 = new java.lang.StringBuilder     // Catch:{ all -> 0x0076 }
            java.lang.String r3 = "PreInstall file wasn't found: "
            r2.<init>(r3)     // Catch:{ all -> 0x0076 }
            java.lang.String r3 = r4.getAbsolutePath()     // Catch:{ all -> 0x0076 }
            java.lang.StringBuilder r2 = r2.append(r3)     // Catch:{ all -> 0x0076 }
            java.lang.String r2 = r2.toString()     // Catch:{ all -> 0x0076 }
            com.appsflyer.AFLogger.afDebugLog(r2)     // Catch:{ all -> 0x0076 }
            if (r1 == 0) goto L_0x001a
            r1.close()     // Catch:{ Throwable -> 0x0042 }
            goto L_0x001a
        L_0x0042:
            r1 = move-exception
            java.lang.String r2 = r1.getMessage()
            com.appsflyer.AFLogger.afErrorLog(r2, r1)
            goto L_0x001a
        L_0x004b:
            r1 = move-exception
            r2 = r1
            r3 = r0
        L_0x004e:
            java.lang.String r1 = r2.getMessage()     // Catch:{ all -> 0x007a }
            com.appsflyer.AFLogger.afErrorLog(r1, r2)     // Catch:{ all -> 0x007a }
            if (r3 == 0) goto L_0x001a
            r3.close()     // Catch:{ Throwable -> 0x005b }
            goto L_0x001a
        L_0x005b:
            r1 = move-exception
            java.lang.String r2 = r1.getMessage()
            com.appsflyer.AFLogger.afErrorLog(r2, r1)
            goto L_0x001a
        L_0x0064:
            r1 = move-exception
            r2 = r1
            r3 = r0
        L_0x0067:
            if (r3 == 0) goto L_0x006c
            r3.close()     // Catch:{ Throwable -> 0x006d }
        L_0x006c:
            throw r2
        L_0x006d:
            r0 = move-exception
            java.lang.String r1 = r0.getMessage()
            com.appsflyer.AFLogger.afErrorLog(r1, r0)
            goto L_0x006c
        L_0x0076:
            r0 = move-exception
            r2 = r0
            r3 = r1
            goto L_0x0067
        L_0x007a:
            r0 = move-exception
            r2 = r0
            goto L_0x0067
        L_0x007d:
            r2 = move-exception
            r3 = r1
            goto L_0x004e
        L_0x0080:
            r2 = move-exception
            goto L_0x0026
        */
        throw new UnsupportedOperationException("Method not decompiled: com.appsflyer.AppsFlyerLib.m202(java.io.File, java.lang.String):java.lang.String");
    }

    /* renamed from: ˏ */
    private static File m222(String str) {
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
        } catch (NameNotFoundException e) {
            AFLogger.afErrorLog("Could not check if app is pre installed", e);
            return false;
        }
    }

    public String getAttributionId(ContentResolver contentResolver) {
        String str = 0;
        String[] strArr = {ATTRIBUTION_ID_COLUMN_NAME};
        Cursor query = contentResolver.query(Uri.parse(ATTRIBUTION_ID_CONTENT_URI), strArr, str, str, str);
        if (query != null) {
            try {
                if (query.moveToFirst()) {
                    str = query.getString(query.getColumnIndex(ATTRIBUTION_ID_COLUMN_NAME));
                    if (query != null) {
                        try {
                            query.close();
                        } catch (Exception e) {
                            AFLogger.afErrorLog(e.getMessage(), e);
                        }
                    }
                    return str;
                }
            } catch (Exception e2) {
                AFLogger.afErrorLog("Could not collect cursor attribution. ", e2);
                if (query != null) {
                    try {
                        query.close();
                    } catch (Exception e3) {
                        AFLogger.afErrorLog(e3.getMessage(), e3);
                    }
                }
            } finally {
                if (query != null) {
                    try {
                        query.close();
                    } catch (Exception e4) {
                        AFLogger.afErrorLog(e4.getMessage(), e4);
                    }
                }
            }
        }
        if (query != null) {
            try {
                query.close();
            } catch (Exception e5) {
                AFLogger.afErrorLog(e5.getMessage(), e5);
            }
        }
        return str;
    }

    /* renamed from: ˏ */
    static SharedPreferences m220(Context context) {
        return context.getSharedPreferences("appsflyer-data", 0);
    }

    /* renamed from: ˏ */
    static int m219(SharedPreferences sharedPreferences) {
        return m188(sharedPreferences, "appsFlyerCount", false);
    }

    /* renamed from: ˊ */
    private static int m188(SharedPreferences sharedPreferences, String str, boolean z) {
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
        if (C0469y.m373().mo6639()) {
            C0469y.m373().mo6641(String.valueOf(i));
        }
        return i;
    }

    public String getAppsFlyerUID(Context context) {
        C0469y.m373().mo6647("getAppsFlyerUID", new String[0]);
        return C0455p.m326(new WeakReference(context));
    }

    /* JADX WARNING: Removed duplicated region for block: B:61:0x0178 A[SYNTHETIC, Splitter:B:61:0x0178] */
    /* renamed from: ˊ */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    private void m198(java.net.URL r11, java.lang.String r12, java.lang.String r13, java.lang.ref.WeakReference<android.content.Context> r14, java.lang.String r15, boolean r16) throws java.io.IOException {
        /*
            r10 = this;
            java.lang.Object r0 = r14.get()
            android.content.Context r0 = (android.content.Context) r0
            if (r16 == 0) goto L_0x0170
            com.appsflyer.AppsFlyerConversionListener r1 = f149
            if (r1 == 0) goto L_0x0170
            r1 = 1
            r2 = r1
        L_0x000e:
            r3 = 0
            com.appsflyer.y r1 = com.appsflyer.C0469y.m373()     // Catch:{ all -> 0x0218 }
            java.lang.String r4 = r11.toString()     // Catch:{ all -> 0x0218 }
            r1.mo6645(r4, r12)     // Catch:{ all -> 0x0218 }
            java.net.URLConnection r1 = r11.openConnection()     // Catch:{ all -> 0x0218 }
            java.net.HttpURLConnection r1 = (java.net.HttpURLConnection) r1     // Catch:{ all -> 0x0218 }
            java.lang.String r3 = "POST"
            r1.setRequestMethod(r3)     // Catch:{ all -> 0x017c }
            byte[] r3 = r12.getBytes()     // Catch:{ all -> 0x017c }
            int r3 = r3.length     // Catch:{ all -> 0x017c }
            java.lang.String r4 = "Content-Length"
            java.lang.String r3 = java.lang.String.valueOf(r3)     // Catch:{ all -> 0x017c }
            r1.setRequestProperty(r4, r3)     // Catch:{ all -> 0x017c }
            java.lang.String r3 = "Content-Type"
            java.lang.String r4 = "application/json"
            r1.setRequestProperty(r3, r4)     // Catch:{ all -> 0x017c }
            r3 = 10000(0x2710, float:1.4013E-41)
            r1.setConnectTimeout(r3)     // Catch:{ all -> 0x017c }
            r3 = 1
            r1.setDoOutput(r3)     // Catch:{ all -> 0x017c }
            r4 = 0
            java.io.OutputStreamWriter r3 = new java.io.OutputStreamWriter     // Catch:{ all -> 0x0174 }
            java.io.OutputStream r5 = r1.getOutputStream()     // Catch:{ all -> 0x0174 }
            java.lang.String r6 = "UTF-8"
            r3.<init>(r5, r6)     // Catch:{ all -> 0x0174 }
            r3.write(r12)     // Catch:{ all -> 0x021c }
            r3.close()     // Catch:{ all -> 0x017c }
            int r3 = r1.getResponseCode()     // Catch:{ all -> 0x017c }
            java.lang.String r4 = m211(r1)     // Catch:{ all -> 0x017c }
            com.appsflyer.y r5 = com.appsflyer.C0469y.m373()     // Catch:{ all -> 0x017c }
            java.lang.String r6 = r11.toString()     // Catch:{ all -> 0x017c }
            r5.mo6644(r6, r3, r4)     // Catch:{ all -> 0x017c }
            java.lang.String r5 = "response code: "
            java.lang.String r6 = java.lang.String.valueOf(r3)     // Catch:{ all -> 0x017c }
            java.lang.String r5 = r5.concat(r6)     // Catch:{ all -> 0x017c }
            com.appsflyer.AFLogger.afInfoLog(r5)     // Catch:{ all -> 0x017c }
            java.lang.String r5 = "AppsFlyer_4.8.11"
            java.lang.String r6 = "SERVER_RESPONSE_CODE"
            java.lang.String r7 = java.lang.Integer.toString(r3)     // Catch:{ all -> 0x017c }
            m207(r0, r5, r6, r7)     // Catch:{ all -> 0x017c }
            java.lang.String r5 = "appsflyer-data"
            r6 = 0
            android.content.SharedPreferences r5 = r0.getSharedPreferences(r5, r6)     // Catch:{ all -> 0x017c }
            r6 = 200(0xc8, float:2.8E-43)
            if (r3 != r6) goto L_0x010d
            java.lang.Object r3 = r14.get()     // Catch:{ all -> 0x017c }
            if (r3 == 0) goto L_0x0099
            if (r16 == 0) goto L_0x0099
            long r6 = java.lang.System.currentTimeMillis()     // Catch:{ all -> 0x017c }
            r10.f171 = r6     // Catch:{ all -> 0x017c }
        L_0x0099:
            java.lang.String r3 = "afUninstallToken"
            com.appsflyer.AppsFlyerProperties r6 = com.appsflyer.AppsFlyerProperties.getInstance()     // Catch:{ all -> 0x017c }
            java.lang.String r3 = r6.getString(r3)     // Catch:{ all -> 0x017c }
            if (r3 == 0) goto L_0x0183
            java.lang.String r6 = "Uninstall Token exists: "
            java.lang.String r7 = java.lang.String.valueOf(r3)     // Catch:{ all -> 0x017c }
            java.lang.String r6 = r6.concat(r7)     // Catch:{ all -> 0x017c }
            com.appsflyer.AFLogger.afDebugLog(r6)     // Catch:{ all -> 0x017c }
            java.lang.String r6 = "sentRegisterRequestToAF"
            r7 = 0
            boolean r6 = r5.getBoolean(r6, r7)     // Catch:{ all -> 0x017c }
            if (r6 != 0) goto L_0x00d0
            java.lang.String r6 = "Resending Uninstall token to AF servers: "
            java.lang.String r7 = java.lang.String.valueOf(r3)     // Catch:{ all -> 0x017c }
            java.lang.String r6 = r6.concat(r7)     // Catch:{ all -> 0x017c }
            com.appsflyer.AFLogger.afDebugLog(r6)     // Catch:{ all -> 0x017c }
            com.appsflyer.d r6 = new com.appsflyer.d     // Catch:{ all -> 0x017c }
            r6.<init>(r3)     // Catch:{ all -> 0x017c }
            com.appsflyer.C0467u.m369(r0, r6)     // Catch:{ all -> 0x017c }
        L_0x00d0:
            android.net.Uri r3 = r10.f158     // Catch:{ all -> 0x017c }
            if (r3 == 0) goto L_0x00d7
            r3 = 0
            r10.f158 = r3     // Catch:{ all -> 0x017c }
        L_0x00d7:
            if (r15 == 0) goto L_0x00e0
            com.appsflyer.cache.CacheManager r3 = com.appsflyer.cache.CacheManager.getInstance()     // Catch:{ all -> 0x017c }
            r3.deleteRequest(r15, r0)     // Catch:{ all -> 0x017c }
        L_0x00e0:
            java.lang.Object r3 = r14.get()     // Catch:{ all -> 0x017c }
            if (r3 == 0) goto L_0x0100
            if (r15 != 0) goto L_0x0100
            java.lang.String r3 = "sentSuccessfully"
            java.lang.String r6 = "true"
            m232(r0, r3, r6)     // Catch:{ all -> 0x017c }
            boolean r3 = r10.f175     // Catch:{ all -> 0x017c }
            if (r3 != 0) goto L_0x0100
            long r6 = java.lang.System.currentTimeMillis()     // Catch:{ all -> 0x017c }
            long r8 = r10.f176     // Catch:{ all -> 0x017c }
            long r6 = r6 - r8
            r8 = 15000(0x3a98, double:7.411E-320)
            int r3 = (r6 > r8 ? 1 : (r6 == r8 ? 0 : -1))
            if (r3 >= 0) goto L_0x01a6
        L_0x0100:
            org.json.JSONObject r3 = com.appsflyer.ServerConfigHandler.m264(r4)     // Catch:{ all -> 0x017c }
            java.lang.String r4 = "send_background"
            r6 = 0
            boolean r3 = r3.optBoolean(r4, r6)     // Catch:{ all -> 0x017c }
            r10.f167 = r3     // Catch:{ all -> 0x017c }
        L_0x010d:
            java.lang.String r3 = "appsflyerConversionDataRequestRetries"
            r4 = 0
            int r3 = r5.getInt(r3, r4)     // Catch:{ all -> 0x017c }
            java.lang.String r4 = "appsflyerConversionDataCacheExpiration"
            r6 = 0
            long r6 = r5.getLong(r4, r6)     // Catch:{ all -> 0x017c }
            r8 = 0
            int r4 = (r6 > r8 ? 1 : (r6 == r8 ? 0 : -1))
            if (r4 == 0) goto L_0x013e
            long r8 = java.lang.System.currentTimeMillis()     // Catch:{ all -> 0x017c }
            long r6 = r8 - r6
            r8 = 5184000000(0x134fd9000, double:2.561236308E-314)
            int r4 = (r6 > r8 ? 1 : (r6 == r8 ? 0 : -1))
            if (r4 <= 0) goto L_0x013e
            java.lang.String r4 = "attributionId"
            r6 = 0
            m232(r0, r4, r6)     // Catch:{ all -> 0x017c }
            java.lang.String r4 = "appsflyerConversionDataCacheExpiration"
            r6 = 0
            m196(r0, r4, r6)     // Catch:{ all -> 0x017c }
        L_0x013e:
            java.lang.String r4 = "attributionId"
            r6 = 0
            java.lang.String r4 = r5.getString(r4, r6)     // Catch:{ all -> 0x017c }
            if (r4 != 0) goto L_0x01c4
            if (r13 == 0) goto L_0x01c4
            if (r2 == 0) goto L_0x01c4
            com.appsflyer.AppsFlyerConversionListener r4 = f149     // Catch:{ all -> 0x017c }
            if (r4 == 0) goto L_0x01c4
            r4 = 5
            if (r3 > r4) goto L_0x01c4
            com.appsflyer.AFExecutor r2 = com.appsflyer.AFExecutor.getInstance()     // Catch:{ all -> 0x017c }
            java.util.concurrent.ScheduledThreadPoolExecutor r2 = r2.mo6409()     // Catch:{ all -> 0x017c }
            com.appsflyer.AppsFlyerLib$a r3 = new com.appsflyer.AppsFlyerLib$a     // Catch:{ all -> 0x017c }
            android.content.Context r0 = r0.getApplicationContext()     // Catch:{ all -> 0x017c }
            r3.<init>(r0, r13, r2)     // Catch:{ all -> 0x017c }
            r4 = 10
            java.util.concurrent.TimeUnit r0 = java.util.concurrent.TimeUnit.MILLISECONDS     // Catch:{ all -> 0x017c }
            m217(r2, r3, r4, r0)     // Catch:{ all -> 0x017c }
        L_0x016a:
            if (r1 == 0) goto L_0x016f
            r1.disconnect()
        L_0x016f:
            return
        L_0x0170:
            r1 = 0
            r2 = r1
            goto L_0x000e
        L_0x0174:
            r0 = move-exception
            r2 = r4
        L_0x0176:
            if (r2 == 0) goto L_0x017b
            r2.close()     // Catch:{ all -> 0x017c }
        L_0x017b:
            throw r0     // Catch:{ all -> 0x017c }
        L_0x017c:
            r0 = move-exception
        L_0x017d:
            if (r1 == 0) goto L_0x0182
            r1.disconnect()
        L_0x0182:
            throw r0
        L_0x0183:
            java.lang.String r3 = "gcmProjectNumber"
            com.appsflyer.AppsFlyerProperties r6 = com.appsflyer.AppsFlyerProperties.getInstance()     // Catch:{ all -> 0x017c }
            java.lang.String r3 = r6.getString(r3)     // Catch:{ all -> 0x017c }
            if (r3 == 0) goto L_0x00d0
            java.lang.String r3 = "GCM Project number exists. Fetching token and sending to AF servers"
            com.appsflyer.AFLogger.afDebugLog(r3)     // Catch:{ all -> 0x017c }
            java.lang.ref.WeakReference r3 = new java.lang.ref.WeakReference     // Catch:{ all -> 0x017c }
            r3.<init>(r0)     // Catch:{ all -> 0x017c }
            com.appsflyer.u$c r6 = new com.appsflyer.u$c     // Catch:{ all -> 0x017c }
            r6.<init>(r3)     // Catch:{ all -> 0x017c }
            r3 = 0
            java.lang.Void[] r3 = new java.lang.Void[r3]     // Catch:{ all -> 0x017c }
            r6.execute(r3)     // Catch:{ all -> 0x017c }
            goto L_0x00d0
        L_0x01a6:
            java.util.concurrent.ScheduledExecutorService r3 = r10.f173     // Catch:{ all -> 0x017c }
            if (r3 != 0) goto L_0x0100
            com.appsflyer.AFExecutor r3 = com.appsflyer.AFExecutor.getInstance()     // Catch:{ all -> 0x017c }
            java.util.concurrent.ScheduledThreadPoolExecutor r3 = r3.mo6409()     // Catch:{ all -> 0x017c }
            r10.f173 = r3     // Catch:{ all -> 0x017c }
            com.appsflyer.AppsFlyerLib$c r3 = new com.appsflyer.AppsFlyerLib$c     // Catch:{ all -> 0x017c }
            r3.<init>(r0)     // Catch:{ all -> 0x017c }
            java.util.concurrent.ScheduledExecutorService r6 = r10.f173     // Catch:{ all -> 0x017c }
            r8 = 1
            java.util.concurrent.TimeUnit r7 = java.util.concurrent.TimeUnit.SECONDS     // Catch:{ all -> 0x017c }
            m217(r6, r3, r8, r7)     // Catch:{ all -> 0x017c }
            goto L_0x0100
        L_0x01c4:
            if (r13 != 0) goto L_0x01cc
            java.lang.String r0 = "AppsFlyer dev key is missing."
            com.appsflyer.AFLogger.afWarnLog(r0)     // Catch:{ all -> 0x017c }
            goto L_0x016a
        L_0x01cc:
            if (r2 == 0) goto L_0x016a
            com.appsflyer.AppsFlyerConversionListener r2 = f149     // Catch:{ all -> 0x017c }
            if (r2 == 0) goto L_0x016a
            java.lang.String r2 = "attributionId"
            r3 = 0
            java.lang.String r2 = r5.getString(r2, r3)     // Catch:{ all -> 0x017c }
            if (r2 == 0) goto L_0x016a
            java.lang.String r2 = "appsFlyerCount"
            r3 = 0
            int r2 = m188(r5, r2, r3)     // Catch:{ all -> 0x017c }
            r3 = 1
            if (r2 <= r3) goto L_0x016a
            java.util.Map r0 = m192(r0)     // Catch:{ k -> 0x020e }
            if (r0 == 0) goto L_0x016a
            java.lang.String r2 = "is_first_launch"
            boolean r2 = r0.containsKey(r2)     // Catch:{ Throwable -> 0x0204 }
            if (r2 != 0) goto L_0x01fd
            java.lang.String r2 = "is_first_launch"
            r3 = 0
            java.lang.String r3 = java.lang.Boolean.toString(r3)     // Catch:{ Throwable -> 0x0204 }
            r0.put(r2, r3)     // Catch:{ Throwable -> 0x0204 }
        L_0x01fd:
            com.appsflyer.AppsFlyerConversionListener r2 = f149     // Catch:{ Throwable -> 0x0204 }
            r2.onInstallConversionDataLoaded(r0)     // Catch:{ Throwable -> 0x0204 }
            goto L_0x016a
        L_0x0204:
            r0 = move-exception
            java.lang.String r2 = r0.getLocalizedMessage()     // Catch:{ k -> 0x020e }
            com.appsflyer.AFLogger.afErrorLog(r2, r0)     // Catch:{ k -> 0x020e }
            goto L_0x016a
        L_0x020e:
            r0 = move-exception
            java.lang.String r2 = r0.getMessage()     // Catch:{ all -> 0x017c }
            com.appsflyer.AFLogger.afErrorLog(r2, r0)     // Catch:{ all -> 0x017c }
            goto L_0x016a
        L_0x0218:
            r0 = move-exception
            r1 = r3
            goto L_0x017d
        L_0x021c:
            r0 = move-exception
            r2 = r3
            goto L_0x0176
        */
        throw new UnsupportedOperationException("Method not decompiled: com.appsflyer.AppsFlyerLib.m198(java.net.URL, java.lang.String, java.lang.String, java.lang.ref.WeakReference, java.lang.String, boolean):void");
    }

    public void validateAndTrackInAppPurchase(Context context, String str, String str2, String str3, String str4, String str5, Map<String, String> map) {
        C0469y r3 = C0469y.m373();
        String str6 = "validateAndTrackInAppPurchase";
        String[] strArr = new String[6];
        strArr[0] = str;
        strArr[1] = str2;
        strArr[2] = str3;
        strArr[3] = str4;
        strArr[4] = str5;
        strArr[5] = map == null ? "" : map.toString();
        r3.mo6647(str6, strArr);
        if (!isTrackingStopped()) {
            AFLogger.afInfoLog(new StringBuilder("Validate in app called with parameters: ").append(str3).append(" ").append(str4).append(" ").append(str5).toString());
        }
        if (str != null && str4 != null && str2 != null && str5 != null && str3 != null) {
            ScheduledThreadPoolExecutor r11 = AFExecutor.getInstance().mo6409();
            m217(r11, new C0442i(context.getApplicationContext(), AppsFlyerProperties.getInstance().getString(AppsFlyerProperties.AF_KEY), str, str2, str3, str4, str5, map, r11, context instanceof Activity ? ((Activity) context).getIntent() : null), 10, TimeUnit.MILLISECONDS);
        } else if (f148 != null) {
            f148.onValidateInAppFailure("Please provide purchase parameters");
        }
    }

    /* renamed from: ˎ */
    private static void m217(ScheduledExecutorService scheduledExecutorService, Runnable runnable, long j, TimeUnit timeUnit) {
        if (scheduledExecutorService != null) {
            try {
                if (!scheduledExecutorService.isShutdown() && !scheduledExecutorService.isTerminated()) {
                    scheduledExecutorService.schedule(runnable, j, timeUnit);
                    return;
                }
            } catch (RejectedExecutionException e) {
                AFLogger.afErrorLog("scheduleJob failed with RejectedExecutionException Exception", e);
                return;
            } catch (Throwable th) {
                AFLogger.afErrorLog("scheduleJob failed with Exception", th);
                return;
            }
        }
        AFLogger.afWarnLog("scheduler is null, shut downed or terminated");
    }

    public void onHandleReferrer(Map<String, String> map) {
        this.f154 = map;
    }

    public boolean isTrackingStopped() {
        return this.f166;
    }

    /* JADX WARNING: Removed duplicated region for block: B:16:0x0047 A[SYNTHETIC, Splitter:B:16:0x0047] */
    /* JADX WARNING: Removed duplicated region for block: B:19:0x004c A[Catch:{ Throwable -> 0x0092 }] */
    /* JADX WARNING: Removed duplicated region for block: B:29:0x0067 A[SYNTHETIC, Splitter:B:29:0x0067] */
    /* JADX WARNING: Removed duplicated region for block: B:32:0x006c A[Catch:{ Throwable -> 0x008b }] */
    @android.support.annotation.NonNull
    /* renamed from: ˎ */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    static java.lang.String m211(java.net.HttpURLConnection r6) {
        /*
            r2 = 0
            java.lang.StringBuilder r4 = new java.lang.StringBuilder
            r4.<init>()
            java.io.InputStream r0 = r6.getErrorStream()     // Catch:{ Throwable -> 0x0094, all -> 0x0062 }
            if (r0 != 0) goto L_0x0010
            java.io.InputStream r0 = r6.getInputStream()     // Catch:{ Throwable -> 0x0094, all -> 0x0062 }
        L_0x0010:
            java.io.InputStreamReader r1 = new java.io.InputStreamReader     // Catch:{ Throwable -> 0x0094, all -> 0x0062 }
            r1.<init>(r0)     // Catch:{ Throwable -> 0x0094, all -> 0x0062 }
            java.io.BufferedReader r3 = new java.io.BufferedReader     // Catch:{ Throwable -> 0x0098, all -> 0x008d }
            r3.<init>(r1)     // Catch:{ Throwable -> 0x0098, all -> 0x008d }
        L_0x001a:
            java.lang.String r0 = r3.readLine()     // Catch:{ Throwable -> 0x002a }
            if (r0 == 0) goto L_0x0059
            java.lang.StringBuilder r0 = r4.append(r0)     // Catch:{ Throwable -> 0x002a }
            r2 = 10
            r0.append(r2)     // Catch:{ Throwable -> 0x002a }
            goto L_0x001a
        L_0x002a:
            r0 = move-exception
        L_0x002b:
            java.lang.StringBuilder r2 = new java.lang.StringBuilder     // Catch:{ all -> 0x0090 }
            java.lang.String r5 = "Could not read connection response from: "
            r2.<init>(r5)     // Catch:{ all -> 0x0090 }
            java.net.URL r5 = r6.getURL()     // Catch:{ all -> 0x0090 }
            java.lang.String r5 = r5.toString()     // Catch:{ all -> 0x0090 }
            java.lang.StringBuilder r2 = r2.append(r5)     // Catch:{ all -> 0x0090 }
            java.lang.String r2 = r2.toString()     // Catch:{ all -> 0x0090 }
            com.appsflyer.AFLogger.afErrorLog(r2, r0)     // Catch:{ all -> 0x0090 }
            if (r3 == 0) goto L_0x004a
            r3.close()     // Catch:{ Throwable -> 0x0092 }
        L_0x004a:
            if (r1 == 0) goto L_0x004f
            r1.close()     // Catch:{ Throwable -> 0x0092 }
        L_0x004f:
            java.lang.String r0 = r4.toString()
            org.json.JSONObject r1 = new org.json.JSONObject     // Catch:{ JSONException -> 0x0070 }
            r1.<init>(r0)     // Catch:{ JSONException -> 0x0070 }
        L_0x0058:
            return r0
        L_0x0059:
            r3.close()     // Catch:{ Throwable -> 0x0060 }
            r1.close()     // Catch:{ Throwable -> 0x0060 }
            goto L_0x004f
        L_0x0060:
            r0 = move-exception
            goto L_0x004f
        L_0x0062:
            r0 = move-exception
            r1 = r2
            r3 = r2
        L_0x0065:
            if (r3 == 0) goto L_0x006a
            r3.close()     // Catch:{ Throwable -> 0x008b }
        L_0x006a:
            if (r1 == 0) goto L_0x006f
            r1.close()     // Catch:{ Throwable -> 0x008b }
        L_0x006f:
            throw r0
        L_0x0070:
            r1 = move-exception
            org.json.JSONObject r1 = new org.json.JSONObject
            r1.<init>()
            java.lang.String r2 = "string_response"
            r1.put(r2, r0)     // Catch:{ JSONException -> 0x0080 }
            java.lang.String r0 = r1.toString()     // Catch:{ JSONException -> 0x0080 }
            goto L_0x0058
        L_0x0080:
            r0 = move-exception
            org.json.JSONObject r0 = new org.json.JSONObject
            r0.<init>()
            java.lang.String r0 = r0.toString()
            goto L_0x0058
        L_0x008b:
            r1 = move-exception
            goto L_0x006f
        L_0x008d:
            r0 = move-exception
            r3 = r2
            goto L_0x0065
        L_0x0090:
            r0 = move-exception
            goto L_0x0065
        L_0x0092:
            r0 = move-exception
            goto L_0x004f
        L_0x0094:
            r0 = move-exception
            r1 = r2
            r3 = r2
            goto L_0x002b
        L_0x0098:
            r0 = move-exception
            r3 = r2
            goto L_0x002b
        */
        throw new UnsupportedOperationException("Method not decompiled: com.appsflyer.AppsFlyerLib.m211(java.net.HttpURLConnection):java.lang.String");
    }

    /* renamed from: ॱॱ */
    private static float m236(Context context) {
        char c = 0;
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
            return c;
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
        this.f172 = TimeUnit.SECONDS.toMillis((long) i);
    }

    /* access modifiers changed from: private */
    /* renamed from: ॱ */
    public static void m232(Context context, String str, String str2) {
        Editor edit = context.getSharedPreferences("appsflyer-data", 0).edit();
        edit.putString(str, str2);
        if (VERSION.SDK_INT >= 9) {
            edit.apply();
        } else {
            edit.commit();
        }
    }

    /* access modifiers changed from: private */
    /* renamed from: ˎ */
    public static void m214(Context context, String str, int i) {
        Editor edit = context.getSharedPreferences("appsflyer-data", 0).edit();
        edit.putInt(str, i);
        if (VERSION.SDK_INT >= 9) {
            edit.apply();
        } else {
            edit.commit();
        }
    }

    /* access modifiers changed from: private */
    /* renamed from: ˊ */
    public static void m196(Context context, String str, long j) {
        Editor edit = context.getSharedPreferences("appsflyer-data", 0).edit();
        edit.putLong(str, j);
        if (VERSION.SDK_INT >= 9) {
            edit.apply();
        } else {
            edit.commit();
        }
    }
}
