package com.crashlytics.android.core;

import android.content.Context;
import android.util.Log;
import com.appsflyer.share.Constants;
import com.crashlytics.android.answers.AppMeasurementEventLogger;
import com.crashlytics.android.answers.EventLogger;
import java.net.URL;
import java.util.Collections;
import java.util.Map;
import java.util.concurrent.Callable;
import java.util.concurrent.ConcurrentHashMap;
import java.util.concurrent.ExecutionException;
import java.util.concurrent.ExecutorService;
import java.util.concurrent.Future;
import java.util.concurrent.TimeUnit;
import java.util.concurrent.TimeoutException;
import javax.net.ssl.HttpsURLConnection;
import net.gogame.gowrap.integrations.AbstractIntegrationSupport;
import p017io.fabric.sdk.android.Fabric;
import p017io.fabric.sdk.android.Kit;
import p017io.fabric.sdk.android.services.common.ApiKey;
import p017io.fabric.sdk.android.services.common.CommonUtils;
import p017io.fabric.sdk.android.services.common.DataCollectionArbiter;
import p017io.fabric.sdk.android.services.common.ExecutorUtils;
import p017io.fabric.sdk.android.services.common.FirebaseInfo;
import p017io.fabric.sdk.android.services.common.IdManager;
import p017io.fabric.sdk.android.services.concurrency.DependsOn;
import p017io.fabric.sdk.android.services.concurrency.Priority;
import p017io.fabric.sdk.android.services.concurrency.PriorityCallable;
import p017io.fabric.sdk.android.services.concurrency.Task;
import p017io.fabric.sdk.android.services.concurrency.UnmetDependencyException;
import p017io.fabric.sdk.android.services.network.DefaultHttpRequestFactory;
import p017io.fabric.sdk.android.services.network.HttpMethod;
import p017io.fabric.sdk.android.services.network.HttpRequest;
import p017io.fabric.sdk.android.services.network.HttpRequestFactory;
import p017io.fabric.sdk.android.services.persistence.FileStoreImpl;
import p017io.fabric.sdk.android.services.persistence.PreferenceStoreImpl;
import p017io.fabric.sdk.android.services.settings.Settings;
import p017io.fabric.sdk.android.services.settings.SettingsData;

@DependsOn({CrashlyticsNdkDataProvider.class})
public class CrashlyticsCore extends Kit<Void> {
    static final float CLS_DEFAULT_PROCESS_DELAY = 1.0f;
    static final String CRASHLYTICS_REQUIRE_BUILD_ID = "com.crashlytics.RequireBuildId";
    static final boolean CRASHLYTICS_REQUIRE_BUILD_ID_DEFAULT = true;
    static final String CRASH_MARKER_FILE_NAME = "crash_marker";
    static final int DEFAULT_MAIN_HANDLER_TIMEOUT_SEC = 4;
    private static final String INITIALIZATION_MARKER_FILE_NAME = "initialization_marker";
    static final int MAX_ATTRIBUTES = 64;
    static final int MAX_ATTRIBUTE_SIZE = 1024;
    private static final String MISSING_BUILD_ID_MSG = "The Crashlytics build ID is missing. This occurs when Crashlytics tooling is absent from your app's build configuration. Please review Crashlytics onboarding instructions and ensure you have a valid Crashlytics account.";
    private static final String PREFERENCE_STORE_NAME = "com.crashlytics.android.core.CrashlyticsCore";
    public static final String TAG = "CrashlyticsCore";
    private final ConcurrentHashMap<String, String> attributes;
    private CrashlyticsBackgroundWorker backgroundWorker;
    private CrashlyticsController controller;
    private CrashlyticsFileMarker crashMarker;
    private CrashlyticsNdkDataProvider crashlyticsNdkDataProvider;
    private float delay;
    private boolean disabled;
    private HttpRequestFactory httpRequestFactory;
    /* access modifiers changed from: private */
    public CrashlyticsFileMarker initializationMarker;
    private CrashlyticsListener listener;
    private final PinningInfoProvider pinningInfo;
    private final long startTime;
    private String userEmail;
    private String userId;
    private String userName;

    public static class Builder {
        private float delay = -1.0f;
        private boolean disabled = false;
        private CrashlyticsListener listener;
        private PinningInfoProvider pinningInfoProvider;

        public CrashlyticsCore build() {
            if (this.delay < 0.0f) {
                this.delay = CrashlyticsCore.CLS_DEFAULT_PROCESS_DELAY;
            }
            return new CrashlyticsCore(this.delay, this.listener, this.pinningInfoProvider, this.disabled);
        }

        public Builder delay(float f) {
            if (f <= 0.0f) {
                throw new IllegalArgumentException("delay must be greater than 0");
            } else if (this.delay > 0.0f) {
                throw new IllegalStateException("delay already set.");
            } else {
                this.delay = f;
                return this;
            }
        }

        public Builder disabled(boolean z) {
            this.disabled = z;
            return this;
        }

        public Builder listener(CrashlyticsListener crashlyticsListener) {
            if (crashlyticsListener == null) {
                throw new IllegalArgumentException("listener must not be null.");
            } else if (this.listener != null) {
                throw new IllegalStateException("listener already set.");
            } else {
                this.listener = crashlyticsListener;
                return this;
            }
        }

        @Deprecated
        public Builder pinningInfo(PinningInfoProvider pinningInfoProvider2) {
            if (pinningInfoProvider2 == null) {
                throw new IllegalArgumentException("pinningInfoProvider must not be null.");
            } else if (this.pinningInfoProvider != null) {
                throw new IllegalStateException("pinningInfoProvider already set.");
            } else {
                this.pinningInfoProvider = pinningInfoProvider2;
                return this;
            }
        }
    }

    private static final class CrashMarkerCheck implements Callable<Boolean> {
        private final CrashlyticsFileMarker crashMarker;

        public CrashMarkerCheck(CrashlyticsFileMarker crashlyticsFileMarker) {
            this.crashMarker = crashlyticsFileMarker;
        }

        public Boolean call() throws Exception {
            if (!this.crashMarker.isPresent()) {
                return Boolean.FALSE;
            }
            Fabric.getLogger().mo20969d(CrashlyticsCore.TAG, "Found previous crash marker.");
            this.crashMarker.remove();
            return Boolean.TRUE;
        }
    }

    private static final class NoOpListener implements CrashlyticsListener {
        private NoOpListener() {
        }

        public void crashlyticsDidDetectCrashDuringPreviousExecution() {
        }
    }

    public CrashlyticsCore() {
        this(CLS_DEFAULT_PROCESS_DELAY, null, null, false);
    }

    CrashlyticsCore(float f, CrashlyticsListener crashlyticsListener, PinningInfoProvider pinningInfoProvider, boolean z) {
        this(f, crashlyticsListener, pinningInfoProvider, z, ExecutorUtils.buildSingleThreadExecutorService("Crashlytics Exception Handler"));
    }

    CrashlyticsCore(float f, CrashlyticsListener crashlyticsListener, PinningInfoProvider pinningInfoProvider, boolean z, ExecutorService executorService) {
        this.userId = null;
        this.userEmail = null;
        this.userName = null;
        this.delay = f;
        if (crashlyticsListener == null) {
            crashlyticsListener = new NoOpListener();
        }
        this.listener = crashlyticsListener;
        this.pinningInfo = pinningInfoProvider;
        this.disabled = z;
        this.backgroundWorker = new CrashlyticsBackgroundWorker(executorService);
        this.attributes = new ConcurrentHashMap<>();
        this.startTime = System.currentTimeMillis();
    }

    private void checkForPreviousCrash() {
        if (Boolean.TRUE.equals((Boolean) this.backgroundWorker.submitAndWait(new CrashMarkerCheck(this.crashMarker)))) {
            try {
                this.listener.crashlyticsDidDetectCrashDuringPreviousExecution();
            } catch (Exception e) {
                Fabric.getLogger().mo20972e(TAG, "Exception thrown by CrashlyticsListener while notifying of previous crash.", e);
            }
        }
    }

    private void doLog(int i, String str, String str2) {
        if (!this.disabled && ensureFabricWithCalled("prior to logging messages.")) {
            this.controller.writeToLog(System.currentTimeMillis() - this.startTime, formatLogMessage(i, str, str2));
        }
    }

    private static boolean ensureFabricWithCalled(String str) {
        CrashlyticsCore instance = getInstance();
        if (instance != null && instance.controller != null) {
            return true;
        }
        Fabric.getLogger().mo20972e(TAG, "Crashlytics must be initialized by calling Fabric.with(Context) " + str, null);
        return false;
    }

    private void finishInitSynchronously() {
        C05191 r1 = new PriorityCallable<Void>() {
            public Void call() throws Exception {
                return CrashlyticsCore.this.doInBackground();
            }

            public Priority getPriority() {
                return Priority.IMMEDIATE;
            }
        };
        for (Task addDependency : getDependencies()) {
            r1.addDependency(addDependency);
        }
        Future submit = getFabric().getExecutorService().submit(r1);
        Fabric.getLogger().mo20969d(TAG, "Crashlytics detected incomplete initialization on previous app launch. Will initialize synchronously.");
        try {
            submit.get(4, TimeUnit.SECONDS);
        } catch (InterruptedException e) {
            Fabric.getLogger().mo20972e(TAG, "Crashlytics was interrupted during initialization.", e);
        } catch (ExecutionException e2) {
            Fabric.getLogger().mo20972e(TAG, "Problem encountered during Crashlytics initialization.", e2);
        } catch (TimeoutException e3) {
            Fabric.getLogger().mo20972e(TAG, "Crashlytics timed out during initialization.", e3);
        }
    }

    private static String formatLogMessage(int i, String str, String str2) {
        return CommonUtils.logPriorityToString(i) + Constants.URL_PATH_DELIMITER + str + " " + str2;
    }

    public static CrashlyticsCore getInstance() {
        return (CrashlyticsCore) Fabric.getKit(CrashlyticsCore.class);
    }

    static boolean isBuildIdValid(String str, boolean z) {
        if (!z) {
            Fabric.getLogger().mo20969d(TAG, "Configured not to require a build ID.");
            return true;
        } else if (!CommonUtils.isNullOrEmpty(str)) {
            return true;
        } else {
            Log.e(TAG, AbstractIntegrationSupport.DEFAULT_EVENT_NAME_DELIMITER);
            Log.e(TAG, ".     |  | ");
            Log.e(TAG, ".     |  |");
            Log.e(TAG, ".     |  |");
            Log.e(TAG, ".   \\ |  | /");
            Log.e(TAG, ".    \\    /");
            Log.e(TAG, ".     \\  /");
            Log.e(TAG, ".      \\/");
            Log.e(TAG, AbstractIntegrationSupport.DEFAULT_EVENT_NAME_DELIMITER);
            Log.e(TAG, MISSING_BUILD_ID_MSG);
            Log.e(TAG, AbstractIntegrationSupport.DEFAULT_EVENT_NAME_DELIMITER);
            Log.e(TAG, ".      /\\");
            Log.e(TAG, ".     /  \\");
            Log.e(TAG, ".    /    \\");
            Log.e(TAG, ".   / |  | \\");
            Log.e(TAG, ".     |  |");
            Log.e(TAG, ".     |  |");
            Log.e(TAG, ".     |  |");
            Log.e(TAG, AbstractIntegrationSupport.DEFAULT_EVENT_NAME_DELIMITER);
            return false;
        }
    }

    private static String sanitizeAttribute(String str) {
        if (str == null) {
            return str;
        }
        String trim = str.trim();
        return trim.length() > 1024 ? trim.substring(0, 1024) : trim;
    }

    public void crash() {
        new CrashTest().indexOutOfBounds();
    }

    /* access modifiers changed from: 0000 */
    public void createCrashMarker() {
        this.crashMarker.create();
    }

    /* access modifiers changed from: 0000 */
    public boolean didPreviousInitializationFail() {
        return this.initializationMarker.isPresent();
    }

    /* access modifiers changed from: protected */
    public Void doInBackground() {
        markInitializationStarted();
        this.controller.cleanInvalidTempFiles();
        try {
            this.controller.registerDevicePowerStateListener();
            SettingsData awaitSettingsData = Settings.getInstance().awaitSettingsData();
            if (awaitSettingsData == null) {
                Fabric.getLogger().mo20982w(TAG, "Received null settings, skipping report submission!");
            } else {
                this.controller.registerAnalyticsEventListener(awaitSettingsData);
                if (!awaitSettingsData.featuresData.collectReports) {
                    Fabric.getLogger().mo20969d(TAG, "Collection of crash reports disabled in Crashlytics settings.");
                    markInitializationComplete();
                } else if (!DataCollectionArbiter.getInstance(getContext()).isDataCollectionEnabled()) {
                    Fabric.getLogger().mo20969d(TAG, "Automatic collection of crash reports disabled by Firebase settings.");
                    markInitializationComplete();
                } else {
                    CrashlyticsNdkData nativeCrashData = getNativeCrashData();
                    if (nativeCrashData != null && !this.controller.finalizeNativeReport(nativeCrashData)) {
                        Fabric.getLogger().mo20969d(TAG, "Could not finalize previous NDK sessions.");
                    }
                    if (!this.controller.finalizeSessions(awaitSettingsData.sessionData)) {
                        Fabric.getLogger().mo20969d(TAG, "Could not finalize previous sessions.");
                    }
                    this.controller.submitAllReports(this.delay, awaitSettingsData);
                    markInitializationComplete();
                }
            }
        } catch (Exception e) {
            Fabric.getLogger().mo20972e(TAG, "Crashlytics encountered a problem during asynchronous initialization.", e);
        } finally {
            markInitializationComplete();
        }
        return null;
    }

    /* access modifiers changed from: 0000 */
    public Map<String, String> getAttributes() {
        return Collections.unmodifiableMap(this.attributes);
    }

    /* access modifiers changed from: 0000 */
    public CrashlyticsController getController() {
        return this.controller;
    }

    public String getIdentifier() {
        return "com.crashlytics.sdk.android.crashlytics-core";
    }

    /* access modifiers changed from: 0000 */
    public CrashlyticsNdkData getNativeCrashData() {
        if (this.crashlyticsNdkDataProvider != null) {
            return this.crashlyticsNdkDataProvider.getCrashlyticsNdkData();
        }
        return null;
    }

    public PinningInfoProvider getPinningInfoProvider() {
        if (!this.disabled) {
            return this.pinningInfo;
        }
        return null;
    }

    /* access modifiers changed from: 0000 */
    public String getUserEmail() {
        if (getIdManager().canCollectUserIds()) {
            return this.userEmail;
        }
        return null;
    }

    /* access modifiers changed from: 0000 */
    public String getUserIdentifier() {
        if (getIdManager().canCollectUserIds()) {
            return this.userId;
        }
        return null;
    }

    /* access modifiers changed from: 0000 */
    public String getUserName() {
        if (getIdManager().canCollectUserIds()) {
            return this.userName;
        }
        return null;
    }

    public String getVersion() {
        return "2.6.8.32";
    }

    /* access modifiers changed from: 0000 */
    public boolean internalVerifyPinning(URL url) {
        if (getPinningInfoProvider() == null) {
            return false;
        }
        HttpRequest buildHttpRequest = this.httpRequestFactory.buildHttpRequest(HttpMethod.GET, url.toString());
        ((HttpsURLConnection) buildHttpRequest.getConnection()).setInstanceFollowRedirects(false);
        buildHttpRequest.code();
        return true;
    }

    public void log(int i, String str, String str2) {
        doLog(i, str, str2);
        Fabric.getLogger().log(i, "" + str, "" + str2, true);
    }

    public void log(String str) {
        doLog(3, TAG, str);
    }

    public void logException(Throwable th) {
        if (this.disabled || !ensureFabricWithCalled("prior to logging exceptions.")) {
            return;
        }
        if (th == null) {
            Fabric.getLogger().log(5, TAG, "Crashlytics is ignoring a request to log a null exception.");
        } else {
            this.controller.writeNonFatalException(Thread.currentThread(), th);
        }
    }

    /* access modifiers changed from: 0000 */
    public void markInitializationComplete() {
        this.backgroundWorker.submit((Callable<T>) new Callable<Boolean>() {
            public Boolean call() throws Exception {
                try {
                    boolean remove = CrashlyticsCore.this.initializationMarker.remove();
                    Fabric.getLogger().mo20969d(CrashlyticsCore.TAG, "Initialization marker file removed: " + remove);
                    return Boolean.valueOf(remove);
                } catch (Exception e) {
                    Fabric.getLogger().mo20972e(CrashlyticsCore.TAG, "Problem encountered deleting Crashlytics initialization marker.", e);
                    return Boolean.valueOf(false);
                }
            }
        });
    }

    /* access modifiers changed from: 0000 */
    public void markInitializationStarted() {
        this.backgroundWorker.submitAndWait(new Callable<Void>() {
            public Void call() throws Exception {
                CrashlyticsCore.this.initializationMarker.create();
                Fabric.getLogger().mo20969d(CrashlyticsCore.TAG, "Initialization marker file created.");
                return null;
            }
        });
    }

    /* access modifiers changed from: protected */
    public boolean onPreExecute() {
        return onPreExecute(super.getContext());
    }

    /* access modifiers changed from: 0000 */
    public boolean onPreExecute(Context context) {
        if (!DataCollectionArbiter.getInstance(context).isDataCollectionEnabled()) {
            Fabric.getLogger().mo20969d(TAG, "Crashlytics is disabled, because data collection is disabled by Firebase.");
            this.disabled = true;
        }
        if (this.disabled) {
            return false;
        }
        String value = new ApiKey().getValue(context);
        if (value == null) {
            return false;
        }
        String resolveBuildId = CommonUtils.resolveBuildId(context);
        if (!isBuildIdValid(resolveBuildId, CommonUtils.getBooleanResourceValue(context, CRASHLYTICS_REQUIRE_BUILD_ID, true))) {
            throw new UnmetDependencyException(MISSING_BUILD_ID_MSG);
        }
        try {
            Fabric.getLogger().mo20974i(TAG, "Initializing Crashlytics " + getVersion());
            FileStoreImpl fileStoreImpl = new FileStoreImpl(this);
            this.crashMarker = new CrashlyticsFileMarker(CRASH_MARKER_FILE_NAME, fileStoreImpl);
            this.initializationMarker = new CrashlyticsFileMarker(INITIALIZATION_MARKER_FILE_NAME, fileStoreImpl);
            PreferenceManager create = PreferenceManager.create(new PreferenceStoreImpl(getContext(), PREFERENCE_STORE_NAME), this);
            CrashlyticsPinningInfoProvider crashlyticsPinningInfoProvider = this.pinningInfo != null ? new CrashlyticsPinningInfoProvider(this.pinningInfo) : null;
            this.httpRequestFactory = new DefaultHttpRequestFactory(Fabric.getLogger());
            this.httpRequestFactory.setPinningInfoProvider(crashlyticsPinningInfoProvider);
            IdManager idManager = getIdManager();
            AppData create2 = AppData.create(context, idManager, value, resolveBuildId);
            ResourceUnityVersionProvider resourceUnityVersionProvider = new ResourceUnityVersionProvider(context, new ManifestUnityVersionProvider(context, create2.packageName));
            DefaultAppMeasurementEventListenerRegistrar defaultAppMeasurementEventListenerRegistrar = new DefaultAppMeasurementEventListenerRegistrar(this);
            EventLogger eventLogger = AppMeasurementEventLogger.getEventLogger(context);
            Fabric.getLogger().mo20969d(TAG, "Installer package name is: " + create2.installerPackageName);
            this.controller = new CrashlyticsController(this, this.backgroundWorker, this.httpRequestFactory, idManager, create, fileStoreImpl, create2, resourceUnityVersionProvider, defaultAppMeasurementEventListenerRegistrar, eventLogger);
            boolean didPreviousInitializationFail = didPreviousInitializationFail();
            checkForPreviousCrash();
            this.controller.enableExceptionHandling(Thread.getDefaultUncaughtExceptionHandler(), new FirebaseInfo().isFirebaseCrashlyticsEnabled(context));
            if (!didPreviousInitializationFail || !CommonUtils.canTryConnection(context)) {
                Fabric.getLogger().mo20969d(TAG, "Exception handling initialization successful");
                return true;
            }
            Fabric.getLogger().mo20969d(TAG, "Crashlytics did not finish previous background initialization. Initializing synchronously.");
            finishInitSynchronously();
            return false;
        } catch (Exception e) {
            Fabric.getLogger().mo20972e(TAG, "Crashlytics was not started due to an exception during initialization", e);
            this.controller = null;
            return false;
        }
    }

    public void setBool(String str, boolean z) {
        setString(str, Boolean.toString(z));
    }

    /* access modifiers changed from: 0000 */
    public void setCrashlyticsNdkDataProvider(CrashlyticsNdkDataProvider crashlyticsNdkDataProvider2) {
        this.crashlyticsNdkDataProvider = crashlyticsNdkDataProvider2;
    }

    public void setDouble(String str, double d) {
        setString(str, Double.toString(d));
    }

    public void setFloat(String str, float f) {
        setString(str, Float.toString(f));
    }

    public void setInt(String str, int i) {
        setString(str, Integer.toString(i));
    }

    @Deprecated
    public void setListener(CrashlyticsListener crashlyticsListener) {
        synchronized (this) {
            Fabric.getLogger().mo20982w(TAG, "Use of setListener is deprecated.");
            if (crashlyticsListener == null) {
                throw new IllegalArgumentException("listener must not be null.");
            }
            this.listener = crashlyticsListener;
        }
    }

    public void setLong(String str, long j) {
        setString(str, Long.toString(j));
    }

    public void setString(String str, String str2) {
        if (this.disabled || !ensureFabricWithCalled("prior to setting keys.")) {
            return;
        }
        if (str == null) {
            Context context = getContext();
            if (context == null || !CommonUtils.isAppDebuggable(context)) {
                Fabric.getLogger().mo20972e(TAG, "Attempting to set custom attribute with null key, ignoring.", null);
                return;
            }
            throw new IllegalArgumentException("Custom attribute key must not be null.");
        }
        String sanitizeAttribute = sanitizeAttribute(str);
        if (this.attributes.size() < 64 || this.attributes.containsKey(sanitizeAttribute)) {
            this.attributes.put(sanitizeAttribute, str2 == null ? "" : sanitizeAttribute(str2));
            this.controller.cacheKeyData(this.attributes);
            return;
        }
        Fabric.getLogger().mo20969d(TAG, "Exceeded maximum number of custom attributes (64)");
    }

    public void setUserEmail(String str) {
        if (!this.disabled && ensureFabricWithCalled("prior to setting user data.")) {
            this.userEmail = sanitizeAttribute(str);
            this.controller.cacheUserData(this.userId, this.userName, this.userEmail);
        }
    }

    public void setUserIdentifier(String str) {
        if (!this.disabled && ensureFabricWithCalled("prior to setting user data.")) {
            this.userId = sanitizeAttribute(str);
            this.controller.cacheUserData(this.userId, this.userName, this.userEmail);
        }
    }

    public void setUserName(String str) {
        if (!this.disabled && ensureFabricWithCalled("prior to setting user data.")) {
            this.userName = sanitizeAttribute(str);
            this.controller.cacheUserData(this.userId, this.userName, this.userEmail);
        }
    }

    public boolean verifyPinning(URL url) {
        try {
            return internalVerifyPinning(url);
        } catch (Exception e) {
            Fabric.getLogger().mo20972e(TAG, "Could not verify SSL pinning", e);
            return false;
        }
    }
}
