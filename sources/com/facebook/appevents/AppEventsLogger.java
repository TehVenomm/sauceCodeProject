package com.facebook.appevents;

import android.app.Activity;
import android.app.Application;
import android.content.ComponentName;
import android.content.Context;
import android.content.Intent;
import android.os.Build.VERSION;
import android.os.Bundle;
import android.support.annotation.Nullable;
import android.util.Log;
import android.webkit.WebView;
import bolts.AppLinks;
import com.facebook.AccessToken;
import com.facebook.FacebookException;
import com.facebook.FacebookSdk;
import com.facebook.GraphRequest;
import com.facebook.GraphRequest.Callback;
import com.facebook.HttpMethod;
import com.facebook.LoggingBehavior;
import com.facebook.appevents.internal.ActivityLifecycleTracker;
import com.facebook.appevents.internal.AutomaticAnalyticsLogger;
import com.facebook.appevents.internal.Constants;
import com.facebook.internal.AnalyticsEvents;
import com.facebook.internal.AttributionIdentifiers;
import com.facebook.internal.BundleJSONConverter;
import com.facebook.internal.FetchedAppSettingsManager;
import com.facebook.internal.Logger;
import com.facebook.internal.Utility;
import com.facebook.internal.Validate;
import com.facebook.share.internal.ShareConstants;
import java.math.BigDecimal;
import java.util.Currency;
import java.util.HashSet;
import java.util.Locale;
import java.util.Map;
import java.util.UUID;
import java.util.concurrent.Executor;
import java.util.concurrent.ScheduledThreadPoolExecutor;
import java.util.concurrent.TimeUnit;
import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

public class AppEventsLogger {
    public static final String ACTION_APP_EVENTS_FLUSHED = "com.facebook.sdk.APP_EVENTS_FLUSHED";
    public static final String APP_EVENTS_EXTRA_FLUSH_RESULT = "com.facebook.sdk.APP_EVENTS_FLUSH_RESULT";
    public static final String APP_EVENTS_EXTRA_NUM_EVENTS_FLUSHED = "com.facebook.sdk.APP_EVENTS_NUM_EVENTS_FLUSHED";
    private static final String APP_EVENT_NAME_PUSH_OPENED = "fb_mobile_push_opened";
    public static final String APP_EVENT_PREFERENCES = "com.facebook.sdk.appEventPreferences";
    private static final String APP_EVENT_PUSH_PARAMETER_ACTION = "fb_push_action";
    private static final String APP_EVENT_PUSH_PARAMETER_CAMPAIGN = "fb_push_campaign";
    private static final int APP_SUPPORTS_ATTRIBUTION_ID_RECHECK_PERIOD_IN_SECONDS = 86400;
    private static final int FLUSH_APP_SESSION_INFO_IN_SECONDS = 30;
    private static final String PUSH_PAYLOAD_CAMPAIGN_KEY = "campaign";
    private static final String PUSH_PAYLOAD_KEY = "fb_push_payload";
    private static final String SOURCE_APPLICATION_HAS_BEEN_SET_BY_THIS_INTENT = "_fbSourceApplicationHasBeenSet";
    /* access modifiers changed from: private */
    public static final String TAG = AppEventsLogger.class.getCanonicalName();
    private static String anonymousAppDeviceGUID;
    /* access modifiers changed from: private */
    public static ScheduledThreadPoolExecutor backgroundExecutor;
    private static FlushBehavior flushBehavior = FlushBehavior.AUTO;
    private static boolean isActivateAppEventRequested;
    private static boolean isOpenedByAppLink;
    private static String pushNotificationsRegistrationId;
    private static String sourceApplication;
    private static Object staticLock = new Object();
    private final AccessTokenAppIdPair accessTokenAppId;
    private final String contextName;

    public enum FlushBehavior {
        AUTO,
        EXPLICIT_ONLY
    }

    static class PersistedAppSessionInfo {
        private static final String PERSISTED_SESSION_INFO_FILENAME = "AppEventsLogger.persistedsessioninfo";
        private static final Runnable appSessionInfoFlushRunnable = new Runnable() {
            public void run() {
                PersistedAppSessionInfo.saveAppSessionInformation(FacebookSdk.getApplicationContext());
            }
        };
        private static Map<AccessTokenAppIdPair, FacebookTimeSpentData> appSessionInfoMap;
        private static boolean hasChanges = false;
        private static boolean isLoaded = false;
        private static final Object staticLock = new Object();

        PersistedAppSessionInfo() {
        }

        private static FacebookTimeSpentData getTimeSpentData(Context context, AccessTokenAppIdPair accessTokenAppIdPair) {
            restoreAppSessionInformation(context);
            FacebookTimeSpentData facebookTimeSpentData = (FacebookTimeSpentData) appSessionInfoMap.get(accessTokenAppIdPair);
            if (facebookTimeSpentData != null) {
                return facebookTimeSpentData;
            }
            FacebookTimeSpentData facebookTimeSpentData2 = new FacebookTimeSpentData();
            appSessionInfoMap.put(accessTokenAppIdPair, facebookTimeSpentData2);
            return facebookTimeSpentData2;
        }

        static void onResume(Context context, AccessTokenAppIdPair accessTokenAppIdPair, AppEventsLogger appEventsLogger, long j, String str) {
            synchronized (staticLock) {
                getTimeSpentData(context, accessTokenAppIdPair).onResume(appEventsLogger, j, str);
                onTimeSpentDataUpdate();
            }
        }

        static void onSuspend(Context context, AccessTokenAppIdPair accessTokenAppIdPair, AppEventsLogger appEventsLogger, long j) {
            synchronized (staticLock) {
                getTimeSpentData(context, accessTokenAppIdPair).onSuspend(appEventsLogger, j);
                onTimeSpentDataUpdate();
            }
        }

        private static void onTimeSpentDataUpdate() {
            if (!hasChanges) {
                hasChanges = true;
                AppEventsLogger.backgroundExecutor.schedule(appSessionInfoFlushRunnable, 30, TimeUnit.SECONDS);
            }
        }

        /* JADX WARNING: Removed duplicated region for block: B:21:0x004d A[Catch:{ FileNotFoundException -> 0x003f, Exception -> 0x005e, all -> 0x00b9, all -> 0x005b }] */
        /* JADX WARNING: Removed duplicated region for block: B:33:0x008a A[Catch:{ FileNotFoundException -> 0x003f, Exception -> 0x005e, all -> 0x00b9, all -> 0x005b }] */
        /* JADX WARNING: Removed duplicated region for block: B:38:0x00a5 A[Catch:{ FileNotFoundException -> 0x003f, Exception -> 0x005e, all -> 0x00b9, all -> 0x005b }] */
        /* Code decompiled incorrectly, please refer to instructions dump. */
        private static void restoreAppSessionInformation(android.content.Context r6) {
            /*
                r2 = 0
                java.lang.Object r3 = staticLock
                monitor-enter(r3)
                boolean r0 = isLoaded     // Catch:{ all -> 0x005b }
                if (r0 != 0) goto L_0x003d
                java.io.ObjectInputStream r1 = new java.io.ObjectInputStream     // Catch:{ FileNotFoundException -> 0x003f, Exception -> 0x005e, all -> 0x00b9 }
                java.lang.String r0 = "AppEventsLogger.persistedsessioninfo"
                java.io.FileInputStream r0 = r6.openFileInput(r0)     // Catch:{ FileNotFoundException -> 0x003f, Exception -> 0x005e, all -> 0x00b9 }
                r1.<init>(r0)     // Catch:{ FileNotFoundException -> 0x003f, Exception -> 0x005e, all -> 0x00b9 }
                java.lang.Object r0 = r1.readObject()     // Catch:{ FileNotFoundException -> 0x00b3, Exception -> 0x00b5, all -> 0x0098 }
                java.util.HashMap r0 = (java.util.HashMap) r0     // Catch:{ FileNotFoundException -> 0x00b3, Exception -> 0x00b5, all -> 0x0098 }
                appSessionInfoMap = r0     // Catch:{ FileNotFoundException -> 0x00b3, Exception -> 0x00b5, all -> 0x0098 }
                com.facebook.LoggingBehavior r0 = com.facebook.LoggingBehavior.APP_EVENTS     // Catch:{ FileNotFoundException -> 0x00b3, Exception -> 0x00b5, all -> 0x0098 }
                java.lang.String r2 = "AppEvents"
                java.lang.String r4 = "App session info loaded"
                com.facebook.internal.Logger.log(r0, r2, r4)     // Catch:{ FileNotFoundException -> 0x00b3, Exception -> 0x00b5, all -> 0x0098 }
                com.facebook.internal.Utility.closeQuietly(r1)     // Catch:{ all -> 0x00b7 }
                java.lang.String r0 = "AppEventsLogger.persistedsessioninfo"
                r6.deleteFile(r0)     // Catch:{ all -> 0x00b7 }
                java.util.Map<com.facebook.appevents.AccessTokenAppIdPair, com.facebook.appevents.FacebookTimeSpentData> r0 = appSessionInfoMap     // Catch:{ all -> 0x00b7 }
                if (r0 != 0) goto L_0x0037
                java.util.HashMap r0 = new java.util.HashMap     // Catch:{ all -> 0x00b7 }
                r0.<init>()     // Catch:{ all -> 0x00b7 }
                appSessionInfoMap = r0     // Catch:{ all -> 0x00b7 }
            L_0x0037:
                r0 = 1
                isLoaded = r0     // Catch:{ all -> 0x00b7 }
                r0 = 0
                hasChanges = r0     // Catch:{ all -> 0x00b7 }
            L_0x003d:
                monitor-exit(r3)     // Catch:{ all -> 0x005b }
                return
            L_0x003f:
                r0 = move-exception
                r1 = r2
            L_0x0041:
                com.facebook.internal.Utility.closeQuietly(r1)     // Catch:{ all -> 0x005b }
                java.lang.String r0 = "AppEventsLogger.persistedsessioninfo"
                r6.deleteFile(r0)     // Catch:{ all -> 0x005b }
                java.util.Map<com.facebook.appevents.AccessTokenAppIdPair, com.facebook.appevents.FacebookTimeSpentData> r0 = appSessionInfoMap     // Catch:{ all -> 0x005b }
                if (r0 != 0) goto L_0x0054
                java.util.HashMap r0 = new java.util.HashMap     // Catch:{ all -> 0x005b }
                r0.<init>()     // Catch:{ all -> 0x005b }
                appSessionInfoMap = r0     // Catch:{ all -> 0x005b }
            L_0x0054:
                r0 = 1
                isLoaded = r0     // Catch:{ all -> 0x005b }
                r0 = 0
                hasChanges = r0     // Catch:{ all -> 0x005b }
                goto L_0x003d
            L_0x005b:
                r0 = move-exception
            L_0x005c:
                monitor-exit(r3)     // Catch:{ all -> 0x005b }
                throw r0
            L_0x005e:
                r0 = move-exception
                r1 = r2
            L_0x0060:
                java.lang.String r2 = com.facebook.appevents.AppEventsLogger.TAG     // Catch:{ all -> 0x00bc }
                java.lang.StringBuilder r4 = new java.lang.StringBuilder     // Catch:{ all -> 0x00bc }
                r4.<init>()     // Catch:{ all -> 0x00bc }
                java.lang.String r5 = "Got unexpected exception restoring app session info: "
                java.lang.StringBuilder r4 = r4.append(r5)     // Catch:{ all -> 0x00bc }
                java.lang.String r0 = r0.toString()     // Catch:{ all -> 0x00bc }
                java.lang.StringBuilder r0 = r4.append(r0)     // Catch:{ all -> 0x00bc }
                java.lang.String r0 = r0.toString()     // Catch:{ all -> 0x00bc }
                android.util.Log.w(r2, r0)     // Catch:{ all -> 0x00bc }
                com.facebook.internal.Utility.closeQuietly(r1)     // Catch:{ all -> 0x005b }
                java.lang.String r0 = "AppEventsLogger.persistedsessioninfo"
                r6.deleteFile(r0)     // Catch:{ all -> 0x005b }
                java.util.Map<com.facebook.appevents.AccessTokenAppIdPair, com.facebook.appevents.FacebookTimeSpentData> r0 = appSessionInfoMap     // Catch:{ all -> 0x005b }
                if (r0 != 0) goto L_0x0091
                java.util.HashMap r0 = new java.util.HashMap     // Catch:{ all -> 0x005b }
                r0.<init>()     // Catch:{ all -> 0x005b }
                appSessionInfoMap = r0     // Catch:{ all -> 0x005b }
            L_0x0091:
                r0 = 1
                isLoaded = r0     // Catch:{ all -> 0x005b }
                r0 = 0
                hasChanges = r0     // Catch:{ all -> 0x005b }
                goto L_0x003d
            L_0x0098:
                r0 = move-exception
            L_0x0099:
                com.facebook.internal.Utility.closeQuietly(r1)     // Catch:{ all -> 0x005b }
                java.lang.String r1 = "AppEventsLogger.persistedsessioninfo"
                r6.deleteFile(r1)     // Catch:{ all -> 0x005b }
                java.util.Map<com.facebook.appevents.AccessTokenAppIdPair, com.facebook.appevents.FacebookTimeSpentData> r1 = appSessionInfoMap     // Catch:{ all -> 0x005b }
                if (r1 != 0) goto L_0x00ac
                java.util.HashMap r1 = new java.util.HashMap     // Catch:{ all -> 0x005b }
                r1.<init>()     // Catch:{ all -> 0x005b }
                appSessionInfoMap = r1     // Catch:{ all -> 0x005b }
            L_0x00ac:
                r1 = 1
                isLoaded = r1     // Catch:{ all -> 0x005b }
                r1 = 0
                hasChanges = r1     // Catch:{ all -> 0x005b }
                throw r0     // Catch:{ all -> 0x005b }
            L_0x00b3:
                r0 = move-exception
                goto L_0x0041
            L_0x00b5:
                r0 = move-exception
                goto L_0x0060
            L_0x00b7:
                r0 = move-exception
                goto L_0x005c
            L_0x00b9:
                r0 = move-exception
                r1 = r2
                goto L_0x0099
            L_0x00bc:
                r0 = move-exception
                goto L_0x0099
            */
            throw new UnsupportedOperationException("Method not decompiled: com.facebook.appevents.AppEventsLogger.PersistedAppSessionInfo.restoreAppSessionInformation(android.content.Context):void");
        }

        /* JADX WARNING: Unknown top exception splitter block from list: {B:24:0x0057=Splitter:B:24:0x0057, B:11:0x002d=Splitter:B:11:0x002d} */
        /* Code decompiled incorrectly, please refer to instructions dump. */
        static void saveAppSessionInformation(android.content.Context r6) {
            /*
                r2 = 0
                java.lang.Object r3 = staticLock
                monitor-enter(r3)
                boolean r0 = hasChanges     // Catch:{ all -> 0x0053 }
                if (r0 == 0) goto L_0x002d
                java.io.ObjectOutputStream r1 = new java.io.ObjectOutputStream     // Catch:{ Exception -> 0x002f, all -> 0x005f }
                java.io.BufferedOutputStream r0 = new java.io.BufferedOutputStream     // Catch:{ Exception -> 0x002f, all -> 0x005f }
                java.lang.String r4 = "AppEventsLogger.persistedsessioninfo"
                r5 = 0
                java.io.FileOutputStream r4 = r6.openFileOutput(r4, r5)     // Catch:{ Exception -> 0x002f, all -> 0x005f }
                r0.<init>(r4)     // Catch:{ Exception -> 0x002f, all -> 0x005f }
                r1.<init>(r0)     // Catch:{ Exception -> 0x002f, all -> 0x005f }
                java.util.Map<com.facebook.appevents.AccessTokenAppIdPair, com.facebook.appevents.FacebookTimeSpentData> r0 = appSessionInfoMap     // Catch:{ Exception -> 0x005d, all -> 0x0056 }
                r1.writeObject(r0)     // Catch:{ Exception -> 0x005d, all -> 0x0056 }
                r0 = 0
                hasChanges = r0     // Catch:{ Exception -> 0x005d, all -> 0x0056 }
                com.facebook.LoggingBehavior r0 = com.facebook.LoggingBehavior.APP_EVENTS     // Catch:{ Exception -> 0x005d, all -> 0x0056 }
                java.lang.String r2 = "AppEvents"
                java.lang.String r4 = "App session info saved"
                com.facebook.internal.Logger.log(r0, r2, r4)     // Catch:{ Exception -> 0x005d, all -> 0x0056 }
                com.facebook.internal.Utility.closeQuietly(r1)     // Catch:{ all -> 0x005b }
            L_0x002d:
                monitor-exit(r3)     // Catch:{ all -> 0x0053 }
                return
            L_0x002f:
                r0 = move-exception
                r1 = r2
            L_0x0031:
                java.lang.String r2 = com.facebook.appevents.AppEventsLogger.TAG     // Catch:{ all -> 0x0062 }
                java.lang.StringBuilder r4 = new java.lang.StringBuilder     // Catch:{ all -> 0x0062 }
                r4.<init>()     // Catch:{ all -> 0x0062 }
                java.lang.String r5 = "Got unexpected exception while writing app session info: "
                java.lang.StringBuilder r4 = r4.append(r5)     // Catch:{ all -> 0x0062 }
                java.lang.String r0 = r0.toString()     // Catch:{ all -> 0x0062 }
                java.lang.StringBuilder r0 = r4.append(r0)     // Catch:{ all -> 0x0062 }
                java.lang.String r0 = r0.toString()     // Catch:{ all -> 0x0062 }
                android.util.Log.w(r2, r0)     // Catch:{ all -> 0x0062 }
                com.facebook.internal.Utility.closeQuietly(r1)     // Catch:{ all -> 0x0053 }
                goto L_0x002d
            L_0x0053:
                r0 = move-exception
            L_0x0054:
                monitor-exit(r3)     // Catch:{ all -> 0x0053 }
                throw r0
            L_0x0056:
                r0 = move-exception
            L_0x0057:
                com.facebook.internal.Utility.closeQuietly(r1)     // Catch:{ all -> 0x0053 }
                throw r0     // Catch:{ all -> 0x0053 }
            L_0x005b:
                r0 = move-exception
                goto L_0x0054
            L_0x005d:
                r0 = move-exception
                goto L_0x0031
            L_0x005f:
                r0 = move-exception
                r1 = r2
                goto L_0x0057
            L_0x0062:
                r0 = move-exception
                goto L_0x0057
            */
            throw new UnsupportedOperationException("Method not decompiled: com.facebook.appevents.AppEventsLogger.PersistedAppSessionInfo.saveAppSessionInformation(android.content.Context):void");
        }
    }

    public enum ProductAvailability {
        IN_STOCK,
        OUT_OF_STOCK,
        PREORDER,
        AVALIABLE_FOR_ORDER,
        DISCONTINUED
    }

    public enum ProductCondition {
        NEW,
        REFURBISHED,
        USED
    }

    private AppEventsLogger(Context context, String str, AccessToken accessToken) {
        this(Utility.getActivityName(context), str, accessToken);
    }

    protected AppEventsLogger(String str, String str2, AccessToken accessToken) {
        Validate.sdkInitialized();
        this.contextName = str;
        if (accessToken == null) {
            accessToken = AccessToken.getCurrentAccessToken();
        }
        if (!AccessToken.isCurrentAccessTokenActive() || (str2 != null && !str2.equals(accessToken.getApplicationId()))) {
            if (str2 == null) {
                str2 = Utility.getMetadataApplicationId(FacebookSdk.getApplicationContext());
            }
            this.accessTokenAppId = new AccessTokenAppIdPair(null, str2);
        } else {
            this.accessTokenAppId = new AccessTokenAppIdPair(accessToken);
        }
        initializeTimersIfNeeded();
    }

    public static void activateApp(Application application) {
        activateApp(application, (String) null);
    }

    public static void activateApp(Application application, String str) {
        if (!FacebookSdk.isInitialized()) {
            throw new FacebookException("The Facebook sdk must be initialized before calling activateApp");
        }
        AnalyticsUserIDStore.initStore();
        UserDataStore.initStore();
        if (str == null) {
            str = FacebookSdk.getApplicationId();
        }
        FacebookSdk.publishInstallAsync(application, str);
        ActivityLifecycleTracker.startTracking(application, str);
    }

    @Deprecated
    public static void activateApp(Context context) {
        if (ActivityLifecycleTracker.isTracking()) {
            Log.w(TAG, "activateApp events are being logged automatically. There's no need to call activateApp explicitly, this is safe to remove.");
            return;
        }
        FacebookSdk.sdkInitialize(context);
        activateApp(context, Utility.getMetadataApplicationId(context));
    }

    @Deprecated
    public static void activateApp(Context context, String str) {
        if (ActivityLifecycleTracker.isTracking()) {
            Log.w(TAG, "activateApp events are being logged automatically. There's no need to call activateApp explicitly, this is safe to remove.");
        } else if (context == null || str == null) {
            throw new IllegalArgumentException("Both context and applicationId must be non-null");
        } else {
            AnalyticsUserIDStore.initStore();
            UserDataStore.initStore();
            if (context instanceof Activity) {
                setSourceApplication((Activity) context);
            } else {
                resetSourceApplication();
                Utility.logd(AppEventsLogger.class.getName(), "To set source application the context of activateApp must be an instance of Activity");
            }
            FacebookSdk.publishInstallAsync(context, str);
            AppEventsLogger appEventsLogger = new AppEventsLogger(context, str, (AccessToken) null);
            final long currentTimeMillis = System.currentTimeMillis();
            final String sourceApplication2 = getSourceApplication();
            backgroundExecutor.execute(new Runnable(appEventsLogger) {
                final /* synthetic */ AppEventsLogger val$logger;

                {
                    this.val$logger = r1;
                }

                public void run() {
                    this.val$logger.logAppSessionResumeEvent(currentTimeMillis, sourceApplication2);
                }
            });
        }
    }

    public static void augmentWebView(WebView webView, Context context) {
        int i = 0;
        String[] split = VERSION.RELEASE.split("\\.");
        int i2 = split.length > 0 ? Integer.parseInt(split[0]) : 0;
        if (split.length > 1) {
            i = Integer.parseInt(split[1]);
        }
        if (VERSION.SDK_INT < 17 || i2 < 4 || (i2 == 4 && i <= 1)) {
            Logger.log(LoggingBehavior.DEVELOPER_ERRORS, TAG, "augmentWebView is only available for Android SDK version >= 17 on devices running Android >= 4.2");
        } else {
            webView.addJavascriptInterface(new FacebookSDKJSInterface(context), "fbmq_" + FacebookSdk.getApplicationId());
        }
    }

    public static void clearUserData() {
        UserDataStore.setUserDataAndHash(null);
    }

    public static void clearUserID() {
        AnalyticsUserIDStore.setUserID(null);
    }

    @Deprecated
    public static void deactivateApp(Context context) {
        if (ActivityLifecycleTracker.isTracking()) {
            Log.w(TAG, "deactivateApp events are being logged automatically. There's no need to call deactivateApp, this is safe to remove.");
        } else {
            deactivateApp(context, Utility.getMetadataApplicationId(context));
        }
    }

    @Deprecated
    public static void deactivateApp(Context context, String str) {
        if (ActivityLifecycleTracker.isTracking()) {
            Log.w(TAG, "deactivateApp events are being logged automatically. There's no need to call deactivateApp, this is safe to remove.");
        } else if (context == null || str == null) {
            throw new IllegalArgumentException("Both context and applicationId must be non-null");
        } else {
            resetSourceApplication();
            AppEventsLogger appEventsLogger = new AppEventsLogger(context, str, (AccessToken) null);
            final long currentTimeMillis = System.currentTimeMillis();
            backgroundExecutor.execute(new Runnable(appEventsLogger) {
                final /* synthetic */ AppEventsLogger val$logger;

                {
                    this.val$logger = r1;
                }

                public void run() {
                    this.val$logger.logAppSessionSuspendEvent(currentTimeMillis);
                }
            });
        }
    }

    static void eagerFlush() {
        if (getFlushBehavior() != FlushBehavior.EXPLICIT_ONLY) {
            AppEventQueue.flush(FlushReason.EAGER_FLUSHING_EVENT);
        }
    }

    static Executor getAnalyticsExecutor() {
        if (backgroundExecutor == null) {
            initializeTimersIfNeeded();
        }
        return backgroundExecutor;
    }

    public static String getAnonymousAppDeviceGUID(Context context) {
        if (anonymousAppDeviceGUID == null) {
            synchronized (staticLock) {
                if (anonymousAppDeviceGUID == null) {
                    anonymousAppDeviceGUID = context.getSharedPreferences(APP_EVENT_PREFERENCES, 0).getString("anonymousAppDeviceGUID", null);
                    if (anonymousAppDeviceGUID == null) {
                        anonymousAppDeviceGUID = "XZ" + UUID.randomUUID().toString();
                        context.getSharedPreferences(APP_EVENT_PREFERENCES, 0).edit().putString("anonymousAppDeviceGUID", anonymousAppDeviceGUID).apply();
                    }
                }
            }
        }
        return anonymousAppDeviceGUID;
    }

    public static FlushBehavior getFlushBehavior() {
        FlushBehavior flushBehavior2;
        synchronized (staticLock) {
            flushBehavior2 = flushBehavior;
        }
        return flushBehavior2;
    }

    static String getPushNotificationsRegistrationId() {
        String str;
        synchronized (staticLock) {
            str = pushNotificationsRegistrationId;
        }
        return str;
    }

    static String getSourceApplication() {
        String str = "Unclassified";
        if (isOpenedByAppLink) {
            str = "Applink";
        }
        return sourceApplication != null ? str + "(" + sourceApplication + ")" : str;
    }

    public static String getUserData() {
        return UserDataStore.getHashedUserData();
    }

    public static String getUserID() {
        return AnalyticsUserIDStore.getUserID();
    }

    public static void initializeLib(Context context, String str) {
        if (FacebookSdk.getAutoLogAppEventsEnabled()) {
            backgroundExecutor.execute(new Runnable(new AppEventsLogger(context, str, (AccessToken) null)) {
                final /* synthetic */ AppEventsLogger val$logger;

                {
                    this.val$logger = r1;
                }

                public void run() {
                    Bundle bundle = new Bundle();
                    try {
                        Class.forName("com.facebook.core.Core");
                        bundle.putInt("core_lib_included", 1);
                    } catch (ClassNotFoundException e) {
                    }
                    try {
                        Class.forName("com.facebook.login.Login");
                        bundle.putInt("login_lib_included", 1);
                    } catch (ClassNotFoundException e2) {
                    }
                    try {
                        Class.forName("com.facebook.share.Share");
                        bundle.putInt("share_lib_included", 1);
                    } catch (ClassNotFoundException e3) {
                    }
                    try {
                        Class.forName("com.facebook.places.Places");
                        bundle.putInt("places_lib_included", 1);
                    } catch (ClassNotFoundException e4) {
                    }
                    try {
                        Class.forName("com.facebook.messenger.Messenger");
                        bundle.putInt("messenger_lib_included", 1);
                    } catch (ClassNotFoundException e5) {
                    }
                    try {
                        Class.forName("com.facebook.applinks.AppLinks");
                        bundle.putInt("applinks_lib_included", 1);
                    } catch (ClassNotFoundException e6) {
                    }
                    try {
                        Class.forName("com.facebook.marketing.Marketing");
                        bundle.putInt("marketing_lib_included", 1);
                    } catch (ClassNotFoundException e7) {
                    }
                    try {
                        Class.forName("com.facebook.all.All");
                        bundle.putInt("all_lib_included", 1);
                    } catch (ClassNotFoundException e8) {
                    }
                    try {
                        Class.forName("com.android.billingclient.api.BillingClient");
                        bundle.putInt("billing_client_lib_included", 1);
                    } catch (ClassNotFoundException e9) {
                    }
                    try {
                        Class.forName("com.android.vending.billing.IInAppBillingService");
                        bundle.putInt("billing_service_lib_included", 1);
                    } catch (ClassNotFoundException e10) {
                    }
                    this.val$logger.logSdkEvent(AnalyticsEvents.EVENT_SDK_INITIALIZE, null, bundle);
                }
            });
        }
    }

    private static void initializeTimersIfNeeded() {
        synchronized (staticLock) {
            if (backgroundExecutor == null) {
                backgroundExecutor = new ScheduledThreadPoolExecutor(1);
                backgroundExecutor.scheduleAtFixedRate(new Runnable() {
                    public void run() {
                        HashSet<String> hashSet = new HashSet<>();
                        for (AccessTokenAppIdPair applicationId : AppEventQueue.getKeySet()) {
                            hashSet.add(applicationId.getApplicationId());
                        }
                        for (String queryAppSettings : hashSet) {
                            FetchedAppSettingsManager.queryAppSettings(queryAppSettings, true);
                        }
                    }
                }, 0, 86400, TimeUnit.SECONDS);
            }
        }
    }

    /* access modifiers changed from: private */
    public void logAppSessionResumeEvent(long j, String str) {
        PersistedAppSessionInfo.onResume(FacebookSdk.getApplicationContext(), this.accessTokenAppId, this, j, str);
    }

    /* access modifiers changed from: private */
    public void logAppSessionSuspendEvent(long j) {
        PersistedAppSessionInfo.onSuspend(FacebookSdk.getApplicationContext(), this.accessTokenAppId, this, j);
    }

    private static void logEvent(AppEvent appEvent, AccessTokenAppIdPair accessTokenAppIdPair) {
        AppEventQueue.add(accessTokenAppIdPair, appEvent);
        if (!appEvent.getIsImplicit() && !isActivateAppEventRequested) {
            if (appEvent.getName().equals(AppEventsConstants.EVENT_NAME_ACTIVATED_APP)) {
                isActivateAppEventRequested = true;
            } else {
                Logger.log(LoggingBehavior.APP_EVENTS, "AppEvents", "Warning: Please call AppEventsLogger.activateApp(...)from the long-lived activity's onResume() methodbefore logging other app events.");
            }
        }
    }

    private void logEvent(String str, Double d, Bundle bundle, boolean z, @Nullable UUID uuid) {
        try {
            logEvent(new AppEvent(this.contextName, str, d, bundle, z, uuid), this.accessTokenAppId);
        } catch (JSONException e) {
            Logger.log(LoggingBehavior.APP_EVENTS, "AppEvents", "JSON encoding for app event failed: '%s'", e.toString());
        } catch (FacebookException e2) {
            Logger.log(LoggingBehavior.APP_EVENTS, "AppEvents", "Invalid app event: %s", e2.toString());
        }
    }

    private void logPurchase(BigDecimal bigDecimal, Currency currency, Bundle bundle, boolean z) {
        if (bigDecimal == null) {
            notifyDeveloperError("purchaseAmount cannot be null");
        } else if (currency == null) {
            notifyDeveloperError("currency cannot be null");
        } else {
            Bundle bundle2 = bundle == null ? new Bundle() : bundle;
            bundle2.putString(AppEventsConstants.EVENT_PARAM_CURRENCY, currency.getCurrencyCode());
            logEvent(AppEventsConstants.EVENT_NAME_PURCHASED, Double.valueOf(bigDecimal.doubleValue()), bundle2, z, ActivityLifecycleTracker.getCurrentSessionGuid());
            eagerFlush();
        }
    }

    public static AppEventsLogger newLogger(Context context) {
        return new AppEventsLogger(context, (String) null, (AccessToken) null);
    }

    public static AppEventsLogger newLogger(Context context, AccessToken accessToken) {
        return new AppEventsLogger(context, (String) null, accessToken);
    }

    public static AppEventsLogger newLogger(Context context, String str) {
        return new AppEventsLogger(context, str, (AccessToken) null);
    }

    public static AppEventsLogger newLogger(Context context, String str, AccessToken accessToken) {
        return new AppEventsLogger(context, str, accessToken);
    }

    private static void notifyDeveloperError(String str) {
        Logger.log(LoggingBehavior.DEVELOPER_ERRORS, "AppEvents", str);
    }

    public static void onContextStop() {
        AppEventQueue.persistToDisk();
    }

    static void resetSourceApplication() {
        sourceApplication = null;
        isOpenedByAppLink = false;
    }

    public static void setFlushBehavior(FlushBehavior flushBehavior2) {
        synchronized (staticLock) {
            flushBehavior = flushBehavior2;
        }
    }

    public static void setPushNotificationsRegistrationId(String str) {
        synchronized (staticLock) {
            if (!Utility.stringsEqualOrEmpty(pushNotificationsRegistrationId, str)) {
                pushNotificationsRegistrationId = str;
                AppEventsLogger newLogger = newLogger(FacebookSdk.getApplicationContext());
                newLogger.logEvent(AppEventsConstants.EVENT_NAME_PUSH_TOKEN_OBTAINED);
                if (getFlushBehavior() != FlushBehavior.EXPLICIT_ONLY) {
                    newLogger.flush();
                }
            }
        }
    }

    private static void setSourceApplication(Activity activity) {
        ComponentName callingActivity = activity.getCallingActivity();
        if (callingActivity != null) {
            String packageName = callingActivity.getPackageName();
            if (packageName.equals(activity.getPackageName())) {
                resetSourceApplication();
                return;
            }
            sourceApplication = packageName;
        }
        Intent intent = activity.getIntent();
        if (intent == null || intent.getBooleanExtra(SOURCE_APPLICATION_HAS_BEEN_SET_BY_THIS_INTENT, false)) {
            resetSourceApplication();
            return;
        }
        Bundle appLinkData = AppLinks.getAppLinkData(intent);
        if (appLinkData == null) {
            resetSourceApplication();
            return;
        }
        isOpenedByAppLink = true;
        Bundle bundle = appLinkData.getBundle("referer_app_link");
        if (bundle == null) {
            sourceApplication = null;
            return;
        }
        sourceApplication = bundle.getString("package");
        intent.putExtra(SOURCE_APPLICATION_HAS_BEEN_SET_BY_THIS_INTENT, true);
    }

    static void setSourceApplication(String str, boolean z) {
        sourceApplication = str;
        isOpenedByAppLink = z;
    }

    @Deprecated
    public static void setUserData(Bundle bundle) {
        UserDataStore.setUserDataAndHash(bundle);
    }

    public static void setUserData(@Nullable String str, @Nullable String str2, @Nullable String str3, @Nullable String str4, @Nullable String str5, @Nullable String str6, @Nullable String str7, @Nullable String str8, @Nullable String str9, @Nullable String str10) {
        UserDataStore.setUserDataAndHash(str, str2, str3, str4, str5, str6, str7, str8, str9, str10);
    }

    public static void setUserID(String str) {
        AnalyticsUserIDStore.setUserID(str);
    }

    public static void updateUserProperties(Bundle bundle, Callback callback) {
        updateUserProperties(bundle, FacebookSdk.getApplicationId(), callback);
    }

    public static void updateUserProperties(final Bundle bundle, final String str, final Callback callback) {
        getAnalyticsExecutor().execute(new Runnable() {
            public void run() {
                String userID = AppEventsLogger.getUserID();
                if (userID == null || userID.isEmpty()) {
                    Logger.log(LoggingBehavior.APP_EVENTS, AppEventsLogger.TAG, "AppEventsLogger userID cannot be null or empty");
                    return;
                }
                Bundle bundle = new Bundle();
                bundle.putString("user_unique_id", userID);
                bundle.putBundle("custom_data", bundle);
                AttributionIdentifiers attributionIdentifiers = AttributionIdentifiers.getAttributionIdentifiers(FacebookSdk.getApplicationContext());
                if (!(attributionIdentifiers == null || attributionIdentifiers.getAndroidAdvertiserId() == null)) {
                    bundle.putString("advertiser_id", attributionIdentifiers.getAndroidAdvertiserId());
                }
                Bundle bundle2 = new Bundle();
                try {
                    JSONObject convertToJSON = BundleJSONConverter.convertToJSON(bundle);
                    JSONArray jSONArray = new JSONArray();
                    jSONArray.put(convertToJSON);
                    bundle2.putString(ShareConstants.WEB_DIALOG_PARAM_DATA, jSONArray.toString());
                    GraphRequest graphRequest = new GraphRequest(AccessToken.getCurrentAccessToken(), String.format(Locale.US, "%s/user_properties", new Object[]{str}), bundle2, HttpMethod.POST, callback);
                    graphRequest.setSkipClientToken(true);
                    graphRequest.executeAsync();
                } catch (JSONException e) {
                    throw new FacebookException("Failed to construct request", (Throwable) e);
                }
            }
        });
    }

    public void flush() {
        AppEventQueue.flush(FlushReason.EXPLICIT);
    }

    public String getApplicationId() {
        return this.accessTokenAppId.getApplicationId();
    }

    public boolean isValidForAccessToken(AccessToken accessToken) {
        return this.accessTokenAppId.equals(new AccessTokenAppIdPair(accessToken));
    }

    public void logEvent(String str) {
        logEvent(str, (Bundle) null);
    }

    public void logEvent(String str, double d) {
        logEvent(str, d, null);
    }

    public void logEvent(String str, double d, Bundle bundle) {
        logEvent(str, Double.valueOf(d), bundle, false, ActivityLifecycleTracker.getCurrentSessionGuid());
    }

    public void logEvent(String str, Bundle bundle) {
        logEvent(str, null, bundle, false, ActivityLifecycleTracker.getCurrentSessionGuid());
    }

    /* access modifiers changed from: protected */
    public void logEventImplicitly(String str, BigDecimal bigDecimal, Currency currency, Bundle bundle) {
        logEvent(str, Double.valueOf(bigDecimal.doubleValue()), bundle, true, ActivityLifecycleTracker.getCurrentSessionGuid());
    }

    public void logProductItem(String str, ProductAvailability productAvailability, ProductCondition productCondition, String str2, String str3, String str4, String str5, BigDecimal bigDecimal, Currency currency, String str6, String str7, String str8, Bundle bundle) {
        if (str == null) {
            notifyDeveloperError("itemID cannot be null");
        } else if (productAvailability == null) {
            notifyDeveloperError("availability cannot be null");
        } else if (productCondition == null) {
            notifyDeveloperError("condition cannot be null");
        } else if (str2 == null) {
            notifyDeveloperError("description cannot be null");
        } else if (str3 == null) {
            notifyDeveloperError("imageLink cannot be null");
        } else if (str4 == null) {
            notifyDeveloperError("link cannot be null");
        } else if (str5 == null) {
            notifyDeveloperError("title cannot be null");
        } else if (bigDecimal == null) {
            notifyDeveloperError("priceAmount cannot be null");
        } else if (currency == null) {
            notifyDeveloperError("currency cannot be null");
        } else if (str6 == null && str7 == null && str8 == null) {
            notifyDeveloperError("Either gtin, mpn or brand is required");
        } else {
            if (bundle == null) {
                bundle = new Bundle();
            }
            bundle.putString(Constants.EVENT_PARAM_PRODUCT_ITEM_ID, str);
            bundle.putString(Constants.EVENT_PARAM_PRODUCT_AVAILABILITY, productAvailability.name());
            bundle.putString(Constants.EVENT_PARAM_PRODUCT_CONDITION, productCondition.name());
            bundle.putString(Constants.EVENT_PARAM_PRODUCT_DESCRIPTION, str2);
            bundle.putString(Constants.EVENT_PARAM_PRODUCT_IMAGE_LINK, str3);
            bundle.putString(Constants.EVENT_PARAM_PRODUCT_LINK, str4);
            bundle.putString(Constants.EVENT_PARAM_PRODUCT_TITLE, str5);
            bundle.putString(Constants.EVENT_PARAM_PRODUCT_PRICE_AMOUNT, bigDecimal.setScale(3, 4).toString());
            bundle.putString(Constants.EVENT_PARAM_PRODUCT_PRICE_CURRENCY, currency.getCurrencyCode());
            if (str6 != null) {
                bundle.putString(Constants.EVENT_PARAM_PRODUCT_GTIN, str6);
            }
            if (str7 != null) {
                bundle.putString(Constants.EVENT_PARAM_PRODUCT_MPN, str7);
            }
            if (str8 != null) {
                bundle.putString(Constants.EVENT_PARAM_PRODUCT_BRAND, str8);
            }
            logEvent(AppEventsConstants.EVENT_NAME_PRODUCT_CATALOG_UPDATE, bundle);
            eagerFlush();
        }
    }

    public void logPurchase(BigDecimal bigDecimal, Currency currency) {
        if (AutomaticAnalyticsLogger.isImplicitPurchaseLoggingEnabled()) {
            Log.w(TAG, "You are logging purchase events while auto-logging of in-app purchase is enabled in the SDK. Make sure you don't log duplicate events");
        }
        logPurchase(bigDecimal, currency, null, false);
    }

    public void logPurchase(BigDecimal bigDecimal, Currency currency, Bundle bundle) {
        if (AutomaticAnalyticsLogger.isImplicitPurchaseLoggingEnabled()) {
            Log.w(TAG, "You are logging purchase events while auto-logging of in-app purchase is enabled in the SDK. Make sure you don't log duplicate events");
        }
        logPurchase(bigDecimal, currency, bundle, false);
    }

    @Deprecated
    public void logPurchaseImplicitly(BigDecimal bigDecimal, Currency currency, Bundle bundle) {
        Log.e(TAG, AutomaticAnalyticsLogger.isImplicitPurchaseLoggingEnabled() ? "Function logPurchaseImplicitly() is deprecated and your purchase events cannot be logged with this function. " + "Auto-logging of in-app purchase has been enabled in the SDK, so you don't have to manually log purchases" : "Function logPurchaseImplicitly() is deprecated and your purchase events cannot be logged with this function. " + "Please use logPurchase() function instead.");
    }

    /* access modifiers changed from: protected */
    public void logPurchaseImplicitlyInternal(BigDecimal bigDecimal, Currency currency, Bundle bundle) {
        logPurchase(bigDecimal, currency, bundle, true);
    }

    public void logPushNotificationOpen(Bundle bundle) {
        logPushNotificationOpen(bundle, null);
    }

    public void logPushNotificationOpen(Bundle bundle, String str) {
        String str2 = null;
        try {
            String string = bundle.getString(PUSH_PAYLOAD_KEY);
            if (!Utility.isNullOrEmpty(string)) {
                str2 = new JSONObject(string).getString("campaign");
                if (str2 == null) {
                    Logger.log(LoggingBehavior.DEVELOPER_ERRORS, TAG, "Malformed payload specified for logging a push notification open.");
                    return;
                }
                Bundle bundle2 = new Bundle();
                bundle2.putString(APP_EVENT_PUSH_PARAMETER_CAMPAIGN, str2);
                if (str != null) {
                    bundle2.putString(APP_EVENT_PUSH_PARAMETER_ACTION, str);
                }
                logEvent(APP_EVENT_NAME_PUSH_OPENED, bundle2);
            }
        } catch (JSONException e) {
        }
    }

    public void logSdkEvent(String str, Double d, Bundle bundle) {
        logEvent(str, d, bundle, true, ActivityLifecycleTracker.getCurrentSessionGuid());
    }
}
