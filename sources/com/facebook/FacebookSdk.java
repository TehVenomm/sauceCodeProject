package com.facebook;

import android.app.Application;
import android.content.Context;
import android.content.pm.ApplicationInfo;
import android.content.pm.PackageInfo;
import android.content.pm.PackageManager;
import android.content.pm.PackageManager.NameNotFoundException;
import android.content.pm.Signature;
import android.os.AsyncTask;
import android.support.p000v4.media.session.PlaybackStateCompat;
import android.util.Base64;
import android.util.Log;
import com.facebook.appevents.AppEventsLogger;
import com.facebook.appevents.internal.ActivityLifecycleTracker;
import com.facebook.internal.BoltsMeasurementEventListener;
import com.facebook.internal.FetchedAppSettingsManager;
import com.facebook.internal.LockOnGetVariable;
import com.facebook.internal.NativeProtocol;
import com.facebook.internal.ServerProtocol;
import com.facebook.internal.Utility;
import com.facebook.internal.Validate;
import java.io.File;
import java.security.MessageDigest;
import java.security.NoSuchAlgorithmException;
import java.util.Arrays;
import java.util.Collections;
import java.util.HashSet;
import java.util.Locale;
import java.util.Set;
import java.util.concurrent.BlockingQueue;
import java.util.concurrent.Callable;
import java.util.concurrent.Executor;
import java.util.concurrent.FutureTask;
import java.util.concurrent.LinkedBlockingQueue;
import java.util.concurrent.ThreadFactory;
import java.util.concurrent.atomic.AtomicInteger;
import java.util.concurrent.atomic.AtomicLong;
import p017io.fabric.sdk.android.services.common.CommonUtils;

public final class FacebookSdk {
    public static final String ADVERTISER_ID_COLLECTION_ENABLED_PROPERTY = "com.facebook.sdk.AdvertiserIDCollectionEnabled";
    public static final String APPLICATION_ID_PROPERTY = "com.facebook.sdk.ApplicationId";
    public static final String APPLICATION_NAME_PROPERTY = "com.facebook.sdk.ApplicationName";
    private static final String ATTRIBUTION_PREFERENCES = "com.facebook.sdk.attributionTracking";
    public static final String AUTO_LOG_APP_EVENTS_ENABLED_PROPERTY = "com.facebook.sdk.AutoLogAppEventsEnabled";
    static final String CALLBACK_OFFSET_CHANGED_AFTER_INIT = "The callback request code offset can't be updated once the SDK is initialized. Call FacebookSdk.setCallbackRequestCodeOffset inside your Application.onCreate method";
    static final String CALLBACK_OFFSET_NEGATIVE = "The callback request code offset can't be negative.";
    public static final String CALLBACK_OFFSET_PROPERTY = "com.facebook.sdk.CallbackOffset";
    public static final String CLIENT_TOKEN_PROPERTY = "com.facebook.sdk.ClientToken";
    public static final String CODELESS_DEBUG_LOG_ENABLED_PROPERTY = "com.facebook.sdk.CodelessDebugLogEnabled";
    private static final int DEFAULT_CALLBACK_REQUEST_CODE_OFFSET = 64206;
    private static final int DEFAULT_CORE_POOL_SIZE = 5;
    private static final int DEFAULT_KEEP_ALIVE = 1;
    private static final int DEFAULT_MAXIMUM_POOL_SIZE = 128;
    private static final ThreadFactory DEFAULT_THREAD_FACTORY = new ThreadFactory() {
        private final AtomicInteger counter = new AtomicInteger(0);

        public Thread newThread(Runnable runnable) {
            return new Thread(runnable, "FacebookSdk #" + this.counter.incrementAndGet());
        }
    };
    private static final BlockingQueue<Runnable> DEFAULT_WORK_QUEUE = new LinkedBlockingQueue(10);
    private static final String FACEBOOK_COM = "facebook.com";
    private static final Object LOCK = new Object();
    private static final int MAX_REQUEST_CODE_RANGE = 100;
    private static final String PUBLISH_ACTIVITY_PATH = "%s/activities";
    private static final String TAG = FacebookSdk.class.getCanonicalName();
    public static final String WEB_DIALOG_THEME = "com.facebook.sdk.WebDialogTheme";
    private static volatile Boolean advertiserIDCollectionEnabled;
    private static volatile String appClientToken;
    /* access modifiers changed from: private */
    public static Context applicationContext;
    /* access modifiers changed from: private */
    public static volatile String applicationId;
    private static volatile String applicationName;
    private static volatile Boolean autoLogAppEventsEnabled;
    private static LockOnGetVariable<File> cacheDir;
    private static int callbackRequestCodeOffset = DEFAULT_CALLBACK_REQUEST_CODE_OFFSET;
    private static volatile Boolean codelessDebugLogEnabled;
    private static Executor executor;
    private static volatile String facebookDomain = FACEBOOK_COM;
    private static String graphApiVersion = ServerProtocol.getDefaultAPIVersion();
    private static volatile boolean isDebugEnabled = false;
    private static boolean isLegacyTokenUpgradeSupported = false;
    private static final HashSet<LoggingBehavior> loggingBehaviors = new HashSet<>(Arrays.asList(new LoggingBehavior[]{LoggingBehavior.DEVELOPER_ERRORS}));
    private static AtomicLong onProgressThreshold = new AtomicLong(PlaybackStateCompat.ACTION_PREPARE_FROM_SEARCH);
    private static Boolean sdkInitialized = Boolean.valueOf(false);

    public interface InitializeCallback {
        void onInitialized();
    }

    public static void addLoggingBehavior(LoggingBehavior loggingBehavior) {
        synchronized (loggingBehaviors) {
            loggingBehaviors.add(loggingBehavior);
            updateGraphDebugBehavior();
        }
    }

    public static void clearLoggingBehaviors() {
        synchronized (loggingBehaviors) {
            loggingBehaviors.clear();
        }
    }

    public static boolean getAdvertiserIDCollectionEnabled() {
        Validate.sdkInitialized();
        return advertiserIDCollectionEnabled.booleanValue();
    }

    public static Context getApplicationContext() {
        Validate.sdkInitialized();
        return applicationContext;
    }

    public static String getApplicationId() {
        Validate.sdkInitialized();
        return applicationId;
    }

    public static String getApplicationName() {
        Validate.sdkInitialized();
        return applicationName;
    }

    public static String getApplicationSignature(Context context) {
        String str = null;
        Validate.sdkInitialized();
        if (context == null) {
            return str;
        }
        PackageManager packageManager = context.getPackageManager();
        if (packageManager == null) {
            return str;
        }
        try {
            PackageInfo packageInfo = packageManager.getPackageInfo(context.getPackageName(), 64);
            Signature[] signatureArr = packageInfo.signatures;
            if (signatureArr == null || signatureArr.length == 0) {
                return str;
            }
            try {
                MessageDigest instance = MessageDigest.getInstance(CommonUtils.SHA1_INSTANCE);
                instance.update(packageInfo.signatures[0].toByteArray());
                return Base64.encodeToString(instance.digest(), 9);
            } catch (NoSuchAlgorithmException e) {
                return str;
            }
        } catch (NameNotFoundException e2) {
            return str;
        }
    }

    public static boolean getAutoLogAppEventsEnabled() {
        Validate.sdkInitialized();
        return autoLogAppEventsEnabled.booleanValue();
    }

    public static File getCacheDir() {
        Validate.sdkInitialized();
        return (File) cacheDir.getValue();
    }

    public static int getCallbackRequestCodeOffset() {
        Validate.sdkInitialized();
        return callbackRequestCodeOffset;
    }

    public static String getClientToken() {
        Validate.sdkInitialized();
        return appClientToken;
    }

    public static boolean getCodelessDebugLogEnabled() {
        Validate.sdkInitialized();
        return codelessDebugLogEnabled.booleanValue();
    }

    public static Executor getExecutor() {
        synchronized (LOCK) {
            if (executor == null) {
                executor = AsyncTask.THREAD_POOL_EXECUTOR;
            }
        }
        return executor;
    }

    public static String getFacebookDomain() {
        return facebookDomain;
    }

    public static String getGraphApiVersion() {
        Utility.logd(TAG, String.format("getGraphApiVersion: %s", new Object[]{graphApiVersion}));
        return graphApiVersion;
    }

    public static boolean getLimitEventAndDataUsage(Context context) {
        Validate.sdkInitialized();
        return context.getSharedPreferences(AppEventsLogger.APP_EVENT_PREFERENCES, 0).getBoolean("limitEventUsage", false);
    }

    public static Set<LoggingBehavior> getLoggingBehaviors() {
        Set<LoggingBehavior> unmodifiableSet;
        synchronized (loggingBehaviors) {
            unmodifiableSet = Collections.unmodifiableSet(new HashSet(loggingBehaviors));
        }
        return unmodifiableSet;
    }

    public static long getOnProgressThreshold() {
        Validate.sdkInitialized();
        return onProgressThreshold.get();
    }

    public static String getSdkVersion() {
        return FacebookSdkVersion.BUILD;
    }

    public static boolean isDebugEnabled() {
        return isDebugEnabled;
    }

    public static boolean isFacebookRequestCode(int i) {
        return i >= callbackRequestCodeOffset && i < callbackRequestCodeOffset + 100;
    }

    public static boolean isInitialized() {
        boolean booleanValue;
        synchronized (FacebookSdk.class) {
            try {
                booleanValue = sdkInitialized.booleanValue();
            } finally {
                Class<FacebookSdk> cls = FacebookSdk.class;
            }
        }
        return booleanValue;
    }

    public static boolean isLegacyTokenUpgradeSupported() {
        return isLegacyTokenUpgradeSupported;
    }

    public static boolean isLoggingBehaviorEnabled(LoggingBehavior loggingBehavior) {
        boolean z;
        synchronized (loggingBehaviors) {
            z = isDebugEnabled() && loggingBehaviors.contains(loggingBehavior);
        }
        return z;
    }

    static void loadDefaultsFromMetadata(Context context) {
        if (context != null) {
            try {
                ApplicationInfo applicationInfo = context.getPackageManager().getApplicationInfo(context.getPackageName(), 128);
                if (applicationInfo != null && applicationInfo.metaData != null) {
                    if (applicationId == null) {
                        Object obj = applicationInfo.metaData.get(APPLICATION_ID_PROPERTY);
                        if (obj instanceof String) {
                            String str = (String) obj;
                            if (str.toLowerCase(Locale.ROOT).startsWith("fb")) {
                                applicationId = str.substring(2);
                            } else {
                                applicationId = str;
                            }
                        } else if (obj instanceof Integer) {
                            throw new FacebookException("App Ids cannot be directly placed in the manifest.They must be prefixed by 'fb' or be placed in the string resource file.");
                        }
                    }
                    if (applicationName == null) {
                        applicationName = applicationInfo.metaData.getString(APPLICATION_NAME_PROPERTY);
                    }
                    if (appClientToken == null) {
                        appClientToken = applicationInfo.metaData.getString(CLIENT_TOKEN_PROPERTY);
                    }
                    if (callbackRequestCodeOffset == DEFAULT_CALLBACK_REQUEST_CODE_OFFSET) {
                        callbackRequestCodeOffset = applicationInfo.metaData.getInt(CALLBACK_OFFSET_PROPERTY, DEFAULT_CALLBACK_REQUEST_CODE_OFFSET);
                    }
                    if (autoLogAppEventsEnabled == null) {
                        autoLogAppEventsEnabled = Boolean.valueOf(applicationInfo.metaData.getBoolean(AUTO_LOG_APP_EVENTS_ENABLED_PROPERTY, true));
                    }
                    if (codelessDebugLogEnabled == null) {
                        codelessDebugLogEnabled = Boolean.valueOf(applicationInfo.metaData.getBoolean(CODELESS_DEBUG_LOG_ENABLED_PROPERTY, false));
                    }
                    if (advertiserIDCollectionEnabled == null) {
                        advertiserIDCollectionEnabled = Boolean.valueOf(applicationInfo.metaData.getBoolean(ADVERTISER_ID_COLLECTION_ENABLED_PROPERTY, true));
                    }
                }
            } catch (NameNotFoundException e) {
            }
        }
    }

    /* JADX INFO: used method not loaded: com.facebook.internal.Utility.logd(java.lang.String, java.lang.Exception):null, types can be incorrect */
    /* JADX WARNING: Code restructure failed: missing block: B:17:0x006f, code lost:
        r0 = move-exception;
     */
    /* JADX WARNING: Code restructure failed: missing block: B:19:0x0077, code lost:
        throw new com.facebook.FacebookException("An error occurred while publishing install.", (java.lang.Throwable) r0);
     */
    /* JADX WARNING: Code restructure failed: missing block: B:20:?, code lost:
        return;
     */
    /* JADX WARNING: Code restructure failed: missing block: B:6:0x000e, code lost:
        r0 = move-exception;
     */
    /* JADX WARNING: Code restructure failed: missing block: B:7:0x000f, code lost:
        com.facebook.internal.Utility.logd("Facebook-publish", r0);
     */
    /* JADX WARNING: Exception block dominator not found, dom blocks: [B:3:0x0006, B:10:0x0039] */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    static void publishInstallAndWaitForResponse(android.content.Context r12, java.lang.String r13) {
        /*
            r10 = 0
            if (r12 == 0) goto L_0x0006
            if (r13 != 0) goto L_0x0015
        L_0x0006:
            java.lang.IllegalArgumentException r0 = new java.lang.IllegalArgumentException     // Catch:{ Exception -> 0x000e }
            java.lang.String r1 = "Both context and applicationId must be non-null"
            r0.<init>(r1)     // Catch:{ Exception -> 0x000e }
            throw r0     // Catch:{ Exception -> 0x000e }
        L_0x000e:
            r0 = move-exception
            java.lang.String r1 = "Facebook-publish"
            com.facebook.internal.Utility.logd(r1, r0)
        L_0x0014:
            return
        L_0x0015:
            com.facebook.internal.AttributionIdentifiers r0 = com.facebook.internal.AttributionIdentifiers.getAttributionIdentifiers(r12)     // Catch:{ Exception -> 0x000e }
            java.lang.String r1 = "com.facebook.sdk.attributionTracking"
            r2 = 0
            android.content.SharedPreferences r1 = r12.getSharedPreferences(r1, r2)     // Catch:{ Exception -> 0x000e }
            java.lang.StringBuilder r2 = new java.lang.StringBuilder     // Catch:{ Exception -> 0x000e }
            r2.<init>()     // Catch:{ Exception -> 0x000e }
            java.lang.StringBuilder r2 = r2.append(r13)     // Catch:{ Exception -> 0x000e }
            java.lang.String r3 = "ping"
            java.lang.StringBuilder r2 = r2.append(r3)     // Catch:{ Exception -> 0x000e }
            java.lang.String r2 = r2.toString()     // Catch:{ Exception -> 0x000e }
            r4 = 0
            long r4 = r1.getLong(r2, r4)     // Catch:{ Exception -> 0x000e }
            com.facebook.appevents.internal.AppEventsLoggerUtility$GraphAPIActivityType r3 = com.facebook.appevents.internal.AppEventsLoggerUtility.GraphAPIActivityType.MOBILE_INSTALL_EVENT     // Catch:{ JSONException -> 0x006f }
            java.lang.String r6 = com.facebook.appevents.AppEventsLogger.getAnonymousAppDeviceGUID(r12)     // Catch:{ JSONException -> 0x006f }
            boolean r7 = getLimitEventAndDataUsage(r12)     // Catch:{ JSONException -> 0x006f }
            org.json.JSONObject r0 = com.facebook.appevents.internal.AppEventsLoggerUtility.getJSONObjectForGraphAPICall(r3, r0, r6, r7, r12)     // Catch:{ JSONException -> 0x006f }
            r3 = 0
            java.lang.String r6 = "%s/activities"
            r7 = 1
            java.lang.Object[] r7 = new java.lang.Object[r7]     // Catch:{ Exception -> 0x000e }
            r8 = 0
            r7[r8] = r13     // Catch:{ Exception -> 0x000e }
            java.lang.String r6 = java.lang.String.format(r6, r7)     // Catch:{ Exception -> 0x000e }
            r7 = 0
            com.facebook.GraphRequest r0 = com.facebook.GraphRequest.newPostRequest(r3, r6, r0, r7)     // Catch:{ Exception -> 0x000e }
            int r3 = (r4 > r10 ? 1 : (r4 == r10 ? 0 : -1))
            if (r3 != 0) goto L_0x0014
            r0.executeAndWait()     // Catch:{ Exception -> 0x000e }
            android.content.SharedPreferences$Editor r0 = r1.edit()     // Catch:{ Exception -> 0x000e }
            long r4 = java.lang.System.currentTimeMillis()     // Catch:{ Exception -> 0x000e }
            r0.putLong(r2, r4)     // Catch:{ Exception -> 0x000e }
            r0.apply()     // Catch:{ Exception -> 0x000e }
            goto L_0x0014
        L_0x006f:
            r0 = move-exception
            com.facebook.FacebookException r1 = new com.facebook.FacebookException     // Catch:{ Exception -> 0x000e }
            java.lang.String r2 = "An error occurred while publishing install."
            r1.<init>(r2, r0)     // Catch:{ Exception -> 0x000e }
            throw r1     // Catch:{ Exception -> 0x000e }
        */
        throw new UnsupportedOperationException("Method not decompiled: com.facebook.FacebookSdk.publishInstallAndWaitForResponse(android.content.Context, java.lang.String):void");
    }

    public static void publishInstallAsync(Context context, final String str) {
        final Context applicationContext2 = context.getApplicationContext();
        getExecutor().execute(new Runnable() {
            public void run() {
                FacebookSdk.publishInstallAndWaitForResponse(applicationContext2, str);
            }
        });
    }

    public static void removeLoggingBehavior(LoggingBehavior loggingBehavior) {
        synchronized (loggingBehaviors) {
            loggingBehaviors.remove(loggingBehavior);
        }
    }

    @Deprecated
    public static void sdkInitialize(Context context) {
        synchronized (FacebookSdk.class) {
            try {
                sdkInitialize(context, (InitializeCallback) null);
            } finally {
                Class<FacebookSdk> cls = FacebookSdk.class;
            }
        }
    }

    @Deprecated
    public static void sdkInitialize(Context context, int i) {
        synchronized (FacebookSdk.class) {
            try {
                sdkInitialize(context, i, null);
            } finally {
                Class<FacebookSdk> cls = FacebookSdk.class;
            }
        }
    }

    @Deprecated
    public static void sdkInitialize(Context context, int i, InitializeCallback initializeCallback) {
        synchronized (FacebookSdk.class) {
            try {
                if (sdkInitialized.booleanValue() && i != callbackRequestCodeOffset) {
                    throw new FacebookException(CALLBACK_OFFSET_CHANGED_AFTER_INIT);
                } else if (i < 0) {
                    throw new FacebookException(CALLBACK_OFFSET_NEGATIVE);
                } else {
                    callbackRequestCodeOffset = i;
                    sdkInitialize(context, initializeCallback);
                }
            } finally {
                Class<FacebookSdk> cls = FacebookSdk.class;
            }
        }
    }

    @Deprecated
    public static void sdkInitialize(final Context context, final InitializeCallback initializeCallback) {
        synchronized (FacebookSdk.class) {
            try {
                if (!sdkInitialized.booleanValue()) {
                    Validate.notNull(context, "applicationContext");
                    Validate.hasFacebookActivity(context, false);
                    Validate.hasInternetPermissions(context, false);
                    applicationContext = context.getApplicationContext();
                    loadDefaultsFromMetadata(applicationContext);
                    if (Utility.isNullOrEmpty(applicationId)) {
                        throw new FacebookException("A valid Facebook app id must be set in the AndroidManifest.xml or set by calling FacebookSdk.setApplicationId before initializing the sdk.");
                    }
                    if ((applicationContext instanceof Application) && autoLogAppEventsEnabled.booleanValue()) {
                        ActivityLifecycleTracker.startTracking((Application) applicationContext, applicationId);
                    }
                    sdkInitialized = Boolean.valueOf(true);
                    FetchedAppSettingsManager.loadAppSettingsAsync();
                    NativeProtocol.updateAllAvailableProtocolVersionsAsync();
                    BoltsMeasurementEventListener.getInstance(applicationContext);
                    cacheDir = new LockOnGetVariable<>((Callable<T>) new Callable<File>() {
                        public File call() throws Exception {
                            return FacebookSdk.applicationContext.getCacheDir();
                        }
                    });
                    getExecutor().execute(new FutureTask(new Callable<Void>() {
                        public Void call() throws Exception {
                            AccessTokenManager.getInstance().loadCurrentAccessToken();
                            ProfileManager.getInstance().loadCurrentProfile();
                            if (AccessToken.isCurrentAccessTokenActive() && Profile.getCurrentProfile() == null) {
                                Profile.fetchProfileForCurrentAccessToken();
                            }
                            if (initializeCallback != null) {
                                initializeCallback.onInitialized();
                            }
                            AppEventsLogger.initializeLib(FacebookSdk.applicationContext, FacebookSdk.applicationId);
                            AppEventsLogger.newLogger(context.getApplicationContext()).flush();
                            return null;
                        }
                    }));
                } else if (initializeCallback != null) {
                    initializeCallback.onInitialized();
                }
            } finally {
                Class<FacebookSdk> cls = FacebookSdk.class;
            }
        }
    }

    public static void setAdvertiserIDCollectionEnabled(boolean z) {
        advertiserIDCollectionEnabled = Boolean.valueOf(z);
    }

    public static void setApplicationId(String str) {
        applicationId = str;
    }

    public static void setApplicationName(String str) {
        applicationName = str;
    }

    public static void setAutoLogAppEventsEnabled(boolean z) {
        autoLogAppEventsEnabled = Boolean.valueOf(z);
    }

    public static void setCacheDir(File file) {
        cacheDir = new LockOnGetVariable<>(file);
    }

    public static void setClientToken(String str) {
        appClientToken = str;
    }

    public static void setCodelessDebugLogEnabled(boolean z) {
        codelessDebugLogEnabled = Boolean.valueOf(z);
    }

    public static void setExecutor(Executor executor2) {
        Validate.notNull(executor2, "executor");
        synchronized (LOCK) {
            executor = executor2;
        }
    }

    public static void setFacebookDomain(String str) {
        Log.w(TAG, "WARNING: Calling setFacebookDomain from non-DEBUG code.");
        facebookDomain = str;
    }

    public static void setGraphApiVersion(String str) {
        Log.w(TAG, "WARNING: Calling setGraphApiVersion from non-DEBUG code.");
        if (!Utility.isNullOrEmpty(str) && !graphApiVersion.equals(str)) {
            graphApiVersion = str;
        }
    }

    public static void setIsDebugEnabled(boolean z) {
        isDebugEnabled = z;
    }

    public static void setLegacyTokenUpgradeSupported(boolean z) {
        isLegacyTokenUpgradeSupported = z;
    }

    public static void setLimitEventAndDataUsage(Context context, boolean z) {
        context.getSharedPreferences(AppEventsLogger.APP_EVENT_PREFERENCES, 0).edit().putBoolean("limitEventUsage", z).apply();
    }

    public static void setOnProgressThreshold(long j) {
        onProgressThreshold.set(j);
    }

    private static void updateGraphDebugBehavior() {
        if (loggingBehaviors.contains(LoggingBehavior.GRAPH_API_DEBUG_INFO) && !loggingBehaviors.contains(LoggingBehavior.GRAPH_API_DEBUG_WARNING)) {
            loggingBehaviors.add(LoggingBehavior.GRAPH_API_DEBUG_WARNING);
        }
    }
}
