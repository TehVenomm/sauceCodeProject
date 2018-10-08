package com.crashlytics.android.core;

import android.annotation.SuppressLint;
import android.app.Activity;
import android.content.Context;
import android.content.DialogInterface;
import android.content.DialogInterface.OnClickListener;
import android.content.pm.PackageInfo;
import android.view.View;
import android.widget.ScrollView;
import android.widget.TextView;
import com.appsflyer.share.Constants;
import com.crashlytics.android.answers.Answers;
import com.crashlytics.android.core.internal.CrashEventDataProvider;
import com.crashlytics.android.core.internal.models.SessionEventData;
import io.fabric.sdk.android.Fabric;
import io.fabric.sdk.android.Kit;
import io.fabric.sdk.android.services.common.CommonUtils;
import io.fabric.sdk.android.services.common.Crash.FatalException;
import io.fabric.sdk.android.services.common.Crash.LoggedException;
import io.fabric.sdk.android.services.common.ExecutorUtils;
import io.fabric.sdk.android.services.common.IdManager;
import io.fabric.sdk.android.services.concurrency.DependsOn;
import io.fabric.sdk.android.services.concurrency.Priority;
import io.fabric.sdk.android.services.concurrency.PriorityCallable;
import io.fabric.sdk.android.services.concurrency.Task;
import io.fabric.sdk.android.services.network.DefaultHttpRequestFactory;
import io.fabric.sdk.android.services.network.HttpMethod;
import io.fabric.sdk.android.services.network.HttpRequest;
import io.fabric.sdk.android.services.network.HttpRequestFactory;
import io.fabric.sdk.android.services.network.PinningInfoProvider;
import io.fabric.sdk.android.services.persistence.FileStoreImpl;
import io.fabric.sdk.android.services.persistence.PreferenceStore;
import io.fabric.sdk.android.services.persistence.PreferenceStoreImpl;
import io.fabric.sdk.android.services.settings.PromptSettingsData;
import io.fabric.sdk.android.services.settings.SessionSettingsData;
import io.fabric.sdk.android.services.settings.Settings;
import io.fabric.sdk.android.services.settings.Settings.SettingsAccess;
import io.fabric.sdk.android.services.settings.SettingsData;
import java.io.File;
import java.net.URL;
import java.util.Collections;
import java.util.Map;
import java.util.concurrent.Callable;
import java.util.concurrent.ConcurrentHashMap;
import java.util.concurrent.CountDownLatch;
import java.util.concurrent.ExecutorService;
import java.util.concurrent.Future;
import java.util.concurrent.TimeUnit;
import javax.net.ssl.HttpsURLConnection;

@DependsOn({CrashEventDataProvider.class})
public class CrashlyticsCore extends Kit<Void> {
    static final float CLS_DEFAULT_PROCESS_DELAY = 1.0f;
    static final String COLLECT_CUSTOM_KEYS = "com.crashlytics.CollectCustomKeys";
    static final String COLLECT_CUSTOM_LOGS = "com.crashlytics.CollectCustomLogs";
    static final String CRASHLYTICS_API_ENDPOINT = "com.crashlytics.ApiEndpoint";
    static final String CRASHLYTICS_REQUIRE_BUILD_ID = "com.crashlytics.RequireBuildId";
    static final boolean CRASHLYTICS_REQUIRE_BUILD_ID_DEFAULT = true;
    static final int DEFAULT_MAIN_HANDLER_TIMEOUT_SEC = 4;
    private static final String INITIALIZATION_MARKER_FILE_NAME = "initialization_marker";
    static final int MAX_ATTRIBUTES = 64;
    static final int MAX_ATTRIBUTE_SIZE = 1024;
    private static final String PREF_ALWAYS_SEND_REPORTS_KEY = "always_send_reports_opt_in";
    private static final boolean SHOULD_PROMPT_BEFORE_SENDING_REPORTS_DEFAULT = false;
    public static final String TAG = "Fabric";
    private final ConcurrentHashMap<String, String> attributes;
    private String buildId;
    private float delay;
    private boolean disabled;
    private CrashlyticsExecutorServiceWrapper executorServiceWrapper;
    private CrashEventDataProvider externalCrashEventDataProvider;
    private CrashlyticsUncaughtExceptionHandler handler;
    private HttpRequestFactory httpRequestFactory;
    private File initializationMarkerFile;
    private String installerPackageName;
    private CrashlyticsListener listener;
    private String packageName;
    private final PinningInfoProvider pinningInfo;
    private final long startTime;
    private String userEmail;
    private String userId;
    private String userName;
    private String versionCode;
    private String versionName;

    /* renamed from: com.crashlytics.android.core.CrashlyticsCore$1 */
    class C03091 extends PriorityCallable<Void> {
        C03091() {
        }

        public Void call() throws Exception {
            return CrashlyticsCore.this.doInBackground();
        }

        public Priority getPriority() {
            return Priority.IMMEDIATE;
        }
    }

    /* renamed from: com.crashlytics.android.core.CrashlyticsCore$2 */
    class C03102 implements Callable<Void> {
        C03102() {
        }

        public Void call() throws Exception {
            CrashlyticsCore.this.initializationMarkerFile.createNewFile();
            Fabric.getLogger().mo4753d("Fabric", "Initialization marker file created.");
            return null;
        }
    }

    /* renamed from: com.crashlytics.android.core.CrashlyticsCore$3 */
    class C03113 implements Callable<Boolean> {
        C03113() {
        }

        public Boolean call() throws Exception {
            try {
                boolean delete = CrashlyticsCore.this.initializationMarkerFile.delete();
                Fabric.getLogger().mo4753d("Fabric", "Initialization marker file removed: " + delete);
                return Boolean.valueOf(delete);
            } catch (Throwable e) {
                Fabric.getLogger().mo4756e("Fabric", "Problem encountered deleting Crashlytics initialization marker.", e);
                return Boolean.valueOf(false);
            }
        }
    }

    /* renamed from: com.crashlytics.android.core.CrashlyticsCore$4 */
    class C03124 implements Callable<Boolean> {
        C03124() {
        }

        public Boolean call() throws Exception {
            return Boolean.valueOf(CrashlyticsCore.this.initializationMarkerFile.exists());
        }
    }

    /* renamed from: com.crashlytics.android.core.CrashlyticsCore$5 */
    class C03135 implements SettingsAccess<Boolean> {
        C03135() {
        }

        public Boolean usingSettings(SettingsData settingsData) {
            boolean z = false;
            if (!settingsData.featuresData.promptEnabled) {
                return Boolean.valueOf(false);
            }
            if (!CrashlyticsCore.this.shouldSendReportsWithoutPrompting()) {
                z = true;
            }
            return Boolean.valueOf(z);
        }
    }

    /* renamed from: com.crashlytics.android.core.CrashlyticsCore$6 */
    class C03146 implements SettingsAccess<Boolean> {
        C03146() {
        }

        public Boolean usingSettings(SettingsData settingsData) {
            boolean z = true;
            Activity currentActivity = CrashlyticsCore.this.getFabric().getCurrentActivity();
            if (!(currentActivity == null || currentActivity.isFinishing() || !CrashlyticsCore.this.shouldPromptUserBeforeSendingCrashReports())) {
                z = CrashlyticsCore.this.getSendDecisionFromUser(currentActivity, settingsData.promptData);
            }
            return Boolean.valueOf(z);
        }
    }

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
        public Builder pinningInfo(PinningInfoProvider pinningInfoProvider) {
            if (pinningInfoProvider == null) {
                throw new IllegalArgumentException("pinningInfoProvider must not be null.");
            } else if (this.pinningInfoProvider != null) {
                throw new IllegalStateException("pinningInfoProvider already set.");
            } else {
                this.pinningInfoProvider = pinningInfoProvider;
                return this;
            }
        }
    }

    private class OptInLatch {
        private final CountDownLatch latch;
        private boolean send;

        private OptInLatch() {
            this.send = false;
            this.latch = new CountDownLatch(1);
        }

        void await() {
            try {
                this.latch.await();
            } catch (InterruptedException e) {
            }
        }

        boolean getOptIn() {
            return this.send;
        }

        void setOptIn(boolean z) {
            this.send = z;
            this.latch.countDown();
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
        this.attributes = new ConcurrentHashMap();
        this.startTime = System.currentTimeMillis();
        this.delay = f;
        this.listener = crashlyticsListener;
        this.pinningInfo = pinningInfoProvider;
        this.disabled = z;
        this.executorServiceWrapper = new CrashlyticsExecutorServiceWrapper(executorService);
    }

    private int dipsToPixels(float f, int i) {
        return (int) (((float) i) * f);
    }

    private void doLog(int i, String str, String str2) {
        if (!this.disabled && ensureFabricWithCalled("prior to logging messages.")) {
            this.handler.writeToLog(System.currentTimeMillis() - this.startTime, formatLogMessage(i, str, str2));
        }
    }

    private static boolean ensureFabricWithCalled(String str) {
        CrashlyticsCore instance = getInstance();
        if (instance != null && instance.handler != null) {
            return true;
        }
        Fabric.getLogger().mo4756e("Fabric", "Crashlytics must be initialized by calling Fabric.with(Context) " + str, null);
        return false;
    }

    private void finishInitSynchronously() {
        Callable c03091 = new C03091();
        for (Task addDependency : getDependencies()) {
            c03091.addDependency(addDependency);
        }
        Future submit = getFabric().getExecutorService().submit(c03091);
        Fabric.getLogger().mo4753d("Fabric", "Crashlytics detected incomplete initialization on previous app launch. Will initialize synchronously.");
        try {
            submit.get(4, TimeUnit.SECONDS);
        } catch (Throwable e) {
            Fabric.getLogger().mo4756e("Fabric", "Crashlytics was interrupted during initialization.", e);
        } catch (Throwable e2) {
            Fabric.getLogger().mo4756e("Fabric", "Problem encountered during Crashlytics initialization.", e2);
        } catch (Throwable e22) {
            Fabric.getLogger().mo4756e("Fabric", "Crashlytics timed out during initialization.", e22);
        }
    }

    private static String formatLogMessage(int i, String str, String str2) {
        return CommonUtils.logPriorityToString(i) + Constants.URL_PATH_DELIMITER + str + " " + str2;
    }

    public static CrashlyticsCore getInstance() {
        return (CrashlyticsCore) Fabric.getKit(CrashlyticsCore.class);
    }

    private boolean getSendDecisionFromUser(Activity activity, PromptSettingsData promptSettingsData) {
        final DialogStringResolver dialogStringResolver = new DialogStringResolver(activity, promptSettingsData);
        final OptInLatch optInLatch = new OptInLatch();
        final Activity activity2 = activity;
        final PromptSettingsData promptSettingsData2 = promptSettingsData;
        activity.runOnUiThread(new Runnable() {

            /* renamed from: com.crashlytics.android.core.CrashlyticsCore$7$1 */
            class C03151 implements OnClickListener {
                C03151() {
                }

                public void onClick(DialogInterface dialogInterface, int i) {
                    optInLatch.setOptIn(true);
                    dialogInterface.dismiss();
                }
            }

            /* renamed from: com.crashlytics.android.core.CrashlyticsCore$7$2 */
            class C03162 implements OnClickListener {
                C03162() {
                }

                public void onClick(DialogInterface dialogInterface, int i) {
                    optInLatch.setOptIn(false);
                    dialogInterface.dismiss();
                }
            }

            /* renamed from: com.crashlytics.android.core.CrashlyticsCore$7$3 */
            class C03173 implements OnClickListener {
                C03173() {
                }

                public void onClick(DialogInterface dialogInterface, int i) {
                    CrashlyticsCore.this.setShouldSendUserReportsWithoutPrompting(true);
                    optInLatch.setOptIn(true);
                    dialogInterface.dismiss();
                }
            }

            public void run() {
                android.app.AlertDialog.Builder builder = new android.app.AlertDialog.Builder(activity2);
                OnClickListener c03151 = new C03151();
                float f = activity2.getResources().getDisplayMetrics().density;
                int access$300 = CrashlyticsCore.this.dipsToPixels(f, 5);
                View textView = new TextView(activity2);
                textView.setAutoLinkMask(15);
                textView.setText(dialogStringResolver.getMessage());
                textView.setTextAppearance(activity2, 16973892);
                textView.setPadding(access$300, access$300, access$300, access$300);
                textView.setFocusable(false);
                View scrollView = new ScrollView(activity2);
                scrollView.setPadding(CrashlyticsCore.this.dipsToPixels(f, 14), CrashlyticsCore.this.dipsToPixels(f, 2), CrashlyticsCore.this.dipsToPixels(f, 10), CrashlyticsCore.this.dipsToPixels(f, 12));
                scrollView.addView(textView);
                builder.setView(scrollView).setTitle(dialogStringResolver.getTitle()).setCancelable(false).setNeutralButton(dialogStringResolver.getSendButtonTitle(), c03151);
                if (promptSettingsData2.showCancelButton) {
                    builder.setNegativeButton(dialogStringResolver.getCancelButtonTitle(), new C03162());
                }
                if (promptSettingsData2.showAlwaysSendButton) {
                    builder.setPositiveButton(dialogStringResolver.getAlwaysSendButtonTitle(), new C03173());
                }
                builder.show();
            }
        });
        Fabric.getLogger().mo4753d("Fabric", "Waiting for user opt-in.");
        optInLatch.await();
        return optInLatch.getOptIn();
    }

    private boolean isRequiringBuildId(Context context) {
        return CommonUtils.getBooleanResourceValue(context, CRASHLYTICS_REQUIRE_BUILD_ID, true);
    }

    static void recordFatalExceptionEvent(String str) {
        Answers answers = (Answers) Fabric.getKit(Answers.class);
        if (answers != null) {
            answers.onException(new FatalException(str));
        }
    }

    static void recordLoggedExceptionEvent(String str) {
        Answers answers = (Answers) Fabric.getKit(Answers.class);
        if (answers != null) {
            answers.onException(new LoggedException(str));
        }
    }

    private static String sanitizeAttribute(String str) {
        if (str == null) {
            return str;
        }
        str = str.trim();
        return str.length() > 1024 ? str.substring(0, 1024) : str;
    }

    private void setAndValidateKitProperties(Context context, String str) {
        PinningInfoProvider crashlyticsPinningInfoProvider = this.pinningInfo != null ? new CrashlyticsPinningInfoProvider(this.pinningInfo) : null;
        this.httpRequestFactory = new DefaultHttpRequestFactory(Fabric.getLogger());
        this.httpRequestFactory.setPinningInfoProvider(crashlyticsPinningInfoProvider);
        try {
            this.packageName = context.getPackageName();
            this.installerPackageName = getIdManager().getInstallerPackageName();
            Fabric.getLogger().mo4753d("Fabric", "Installer package name is: " + this.installerPackageName);
            PackageInfo packageInfo = context.getPackageManager().getPackageInfo(this.packageName, 0);
            this.versionCode = Integer.toString(packageInfo.versionCode);
            this.versionName = packageInfo.versionName == null ? IdManager.DEFAULT_VERSION_NAME : packageInfo.versionName;
            this.buildId = CommonUtils.resolveBuildId(context);
        } catch (Throwable e) {
            Fabric.getLogger().mo4756e("Fabric", "Error setting up app properties", e);
        }
        getIdManager().getBluetoothMacAddress();
        getBuildIdValidator(this.buildId, isRequiringBuildId(context)).validate(str, this.packageName);
    }

    boolean canSendWithUserApproval() {
        return ((Boolean) Settings.getInstance().withSettings(new C03146(), Boolean.valueOf(true))).booleanValue();
    }

    public void crash() {
        new CrashTest().indexOutOfBounds();
    }

    boolean didPreviousInitializationComplete() {
        return ((Boolean) this.executorServiceWrapper.executeSyncLoggingException(new C03124())).booleanValue();
    }

    protected Void doInBackground() {
        Throwable e;
        Object obj = 1;
        Object obj2 = null;
        markInitializationStarted();
        this.handler.cleanInvalidTempFiles();
        try {
            SettingsData awaitSettingsData = Settings.getInstance().awaitSettingsData();
            if (awaitSettingsData == null) {
                Fabric.getLogger().mo4766w("Fabric", "Received null settings, skipping initialization!");
                markInitializationComplete();
                return null;
            }
            if (awaitSettingsData.featuresData.collectReports) {
                try {
                    this.handler.finalizeSessions();
                    CreateReportSpiCall createReportSpiCall = getCreateReportSpiCall(awaitSettingsData);
                    if (createReportSpiCall != null) {
                        new ReportUploader(createReportSpiCall).uploadReports(this.delay);
                    } else {
                        Fabric.getLogger().mo4766w("Fabric", "Unable to create a call to upload reports.");
                    }
                } catch (Exception e2) {
                    e = e2;
                    obj = null;
                    Fabric.getLogger().mo4756e("Fabric", "Error dealing with settings", e);
                    obj2 = obj;
                    if (obj2 != null) {
                        try {
                            Fabric.getLogger().mo4753d("Fabric", "Crash reporting disabled.");
                        } catch (Throwable e3) {
                            Fabric.getLogger().mo4756e("Fabric", "Problem encountered during Crashlytics initialization.", e3);
                        } finally {
                            markInitializationComplete();
                        }
                    }
                    markInitializationComplete();
                    return null;
                }
            }
            int i = 1;
            if (obj2 != null) {
                Fabric.getLogger().mo4753d("Fabric", "Crash reporting disabled.");
            }
            markInitializationComplete();
            return null;
        } catch (Exception e4) {
            e3 = e4;
            Fabric.getLogger().mo4756e("Fabric", "Error dealing with settings", e3);
            obj2 = obj;
            if (obj2 != null) {
                Fabric.getLogger().mo4753d("Fabric", "Crash reporting disabled.");
            }
            markInitializationComplete();
            return null;
        }
    }

    Map<String, String> getAttributes() {
        return Collections.unmodifiableMap(this.attributes);
    }

    String getBuildId() {
        return this.buildId;
    }

    BuildIdValidator getBuildIdValidator(String str, boolean z) {
        return new BuildIdValidator(str, z);
    }

    CreateReportSpiCall getCreateReportSpiCall(SettingsData settingsData) {
        return settingsData != null ? new DefaultCreateReportSpiCall(this, getOverridenSpiEndpoint(), settingsData.appData.reportsUrl, this.httpRequestFactory) : null;
    }

    SessionEventData getExternalCrashEventData() {
        return this.externalCrashEventDataProvider != null ? this.externalCrashEventDataProvider.getCrashEventData() : null;
    }

    CrashlyticsUncaughtExceptionHandler getHandler() {
        return this.handler;
    }

    public String getIdentifier() {
        return "com.crashlytics.sdk.android.crashlytics-core";
    }

    String getInstallerPackageName() {
        return this.installerPackageName;
    }

    String getOverridenSpiEndpoint() {
        return CommonUtils.getStringsFileValue(getContext(), CRASHLYTICS_API_ENDPOINT);
    }

    String getPackageName() {
        return this.packageName;
    }

    public PinningInfoProvider getPinningInfoProvider() {
        return !this.disabled ? this.pinningInfo : null;
    }

    File getSdkDirectory() {
        return new FileStoreImpl(this).getFilesDir();
    }

    SessionSettingsData getSessionSettingsData() {
        SettingsData awaitSettingsData = Settings.getInstance().awaitSettingsData();
        return awaitSettingsData == null ? null : awaitSettingsData.sessionData;
    }

    String getUserEmail() {
        return getIdManager().canCollectUserIds() ? this.userEmail : null;
    }

    String getUserIdentifier() {
        return getIdManager().canCollectUserIds() ? this.userId : null;
    }

    String getUserName() {
        return getIdManager().canCollectUserIds() ? this.userName : null;
    }

    public String getVersion() {
        return "2.3.3.61";
    }

    String getVersionCode() {
        return this.versionCode;
    }

    String getVersionName() {
        return this.versionName;
    }

    boolean internalVerifyPinning(URL url) {
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
        doLog(3, "Fabric", str);
    }

    public void logException(Throwable th) {
        if (this.disabled || !ensureFabricWithCalled("prior to logging exceptions.")) {
            return;
        }
        if (th == null) {
            Fabric.getLogger().log(5, "Fabric", "Crashlytics is ignoring a request to log a null exception.");
        } else {
            this.handler.writeNonFatalException(Thread.currentThread(), th);
        }
    }

    void markInitializationComplete() {
        this.executorServiceWrapper.executeAsync(new C03113());
    }

    void markInitializationStarted() {
        this.executorServiceWrapper.executeSyncLoggingException(new C03102());
    }

    protected boolean onPreExecute() {
        return onPreExecute(super.getContext());
    }

    /* JADX WARNING: inconsistent code. */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    boolean onPreExecute(android.content.Context r9) {
        /*
        r8 = this;
        r7 = 0;
        r0 = r8.disabled;
        if (r0 == 0) goto L_0x0007;
    L_0x0005:
        r0 = r7;
    L_0x0006:
        return r0;
    L_0x0007:
        r0 = new io.fabric.sdk.android.services.common.ApiKey;
        r0.<init>();
        r0 = r0.getValue(r9);
        if (r0 != 0) goto L_0x0014;
    L_0x0012:
        r0 = r7;
        goto L_0x0006;
    L_0x0014:
        r1 = io.fabric.sdk.android.Fabric.getLogger();
        r2 = "Fabric";
        r3 = new java.lang.StringBuilder;
        r3.<init>();
        r4 = "Initializing Crashlytics ";
        r3 = r3.append(r4);
        r4 = r8.getVersion();
        r3 = r3.append(r4);
        r3 = r3.toString();
        r1.mo4758i(r2, r3);
        r1 = new java.io.File;
        r2 = r8.getSdkDirectory();
        r3 = "initialization_marker";
        r1.<init>(r2, r3);
        r8.initializationMarkerFile = r1;
        r8.setAndValidateKitProperties(r9, r0);	 Catch:{ CrashlyticsMissingDependencyException -> 0x00a7, Exception -> 0x00b1 }
        r5 = new com.crashlytics.android.core.SessionDataWriter;	 Catch:{ Exception -> 0x0099, CrashlyticsMissingDependencyException -> 0x00a7 }
        r0 = r8.getContext();	 Catch:{ Exception -> 0x0099, CrashlyticsMissingDependencyException -> 0x00a7 }
        r1 = r8.buildId;	 Catch:{ Exception -> 0x0099, CrashlyticsMissingDependencyException -> 0x00a7 }
        r2 = r8.getPackageName();	 Catch:{ Exception -> 0x0099, CrashlyticsMissingDependencyException -> 0x00a7 }
        r5.<init>(r0, r1, r2);	 Catch:{ Exception -> 0x0099, CrashlyticsMissingDependencyException -> 0x00a7 }
        r0 = io.fabric.sdk.android.Fabric.getLogger();	 Catch:{ Exception -> 0x0099, CrashlyticsMissingDependencyException -> 0x00a7 }
        r1 = "Fabric";
        r2 = "Installing exception handler...";
        r0.mo4753d(r1, r2);	 Catch:{ Exception -> 0x0099, CrashlyticsMissingDependencyException -> 0x00a7 }
        r0 = new com.crashlytics.android.core.CrashlyticsUncaughtExceptionHandler;	 Catch:{ Exception -> 0x0099, CrashlyticsMissingDependencyException -> 0x00a7 }
        r1 = java.lang.Thread.getDefaultUncaughtExceptionHandler();	 Catch:{ Exception -> 0x0099, CrashlyticsMissingDependencyException -> 0x00a7 }
        r2 = r8.listener;	 Catch:{ Exception -> 0x0099, CrashlyticsMissingDependencyException -> 0x00a7 }
        r3 = r8.executorServiceWrapper;	 Catch:{ Exception -> 0x0099, CrashlyticsMissingDependencyException -> 0x00a7 }
        r4 = r8.getIdManager();	 Catch:{ Exception -> 0x0099, CrashlyticsMissingDependencyException -> 0x00a7 }
        r6 = r8;
        r0.<init>(r1, r2, r3, r4, r5, r6);	 Catch:{ Exception -> 0x0099, CrashlyticsMissingDependencyException -> 0x00a7 }
        r8.handler = r0;	 Catch:{ Exception -> 0x0099, CrashlyticsMissingDependencyException -> 0x00a7 }
        r1 = r8.didPreviousInitializationComplete();	 Catch:{ Exception -> 0x0099, CrashlyticsMissingDependencyException -> 0x00a7 }
        r0 = r8.handler;	 Catch:{ Exception -> 0x00c0, CrashlyticsMissingDependencyException -> 0x00a7 }
        r0.ensureOpenSessionExists();	 Catch:{ Exception -> 0x00c0, CrashlyticsMissingDependencyException -> 0x00a7 }
        r0 = r8.handler;	 Catch:{ Exception -> 0x00c0, CrashlyticsMissingDependencyException -> 0x00a7 }
        java.lang.Thread.setDefaultUncaughtExceptionHandler(r0);	 Catch:{ Exception -> 0x00c0, CrashlyticsMissingDependencyException -> 0x00a7 }
        r0 = io.fabric.sdk.android.Fabric.getLogger();	 Catch:{ Exception -> 0x00c0, CrashlyticsMissingDependencyException -> 0x00a7 }
        r2 = "Fabric";
        r3 = "Successfully installed exception handler.";
        r0.mo4753d(r2, r3);	 Catch:{ Exception -> 0x00c0, CrashlyticsMissingDependencyException -> 0x00a7 }
    L_0x008b:
        if (r1 == 0) goto L_0x00ae;
    L_0x008d:
        r0 = io.fabric.sdk.android.services.common.CommonUtils.canTryConnection(r9);	 Catch:{ CrashlyticsMissingDependencyException -> 0x00a7, Exception -> 0x00b1 }
        if (r0 == 0) goto L_0x00ae;
    L_0x0093:
        r8.finishInitSynchronously();	 Catch:{ CrashlyticsMissingDependencyException -> 0x00a7, Exception -> 0x00b1 }
        r0 = r7;
        goto L_0x0006;
    L_0x0099:
        r0 = move-exception;
        r1 = r7;
    L_0x009b:
        r2 = io.fabric.sdk.android.Fabric.getLogger();	 Catch:{ CrashlyticsMissingDependencyException -> 0x00a7, Exception -> 0x00b1 }
        r3 = "Fabric";
        r4 = "There was a problem installing the exception handler.";
        r2.mo4756e(r3, r4, r0);	 Catch:{ CrashlyticsMissingDependencyException -> 0x00a7, Exception -> 0x00b1 }
        goto L_0x008b;
    L_0x00a7:
        r0 = move-exception;
        r1 = new io.fabric.sdk.android.services.concurrency.UnmetDependencyException;
        r1.<init>(r0);
        throw r1;
    L_0x00ae:
        r0 = 1;
        goto L_0x0006;
    L_0x00b1:
        r0 = move-exception;
        r1 = io.fabric.sdk.android.Fabric.getLogger();
        r2 = "Fabric";
        r3 = "Crashlytics was not started due to an exception during initialization";
        r1.mo4756e(r2, r3, r0);
        r0 = r7;
        goto L_0x0006;
    L_0x00c0:
        r0 = move-exception;
        goto L_0x009b;
        */
        throw new UnsupportedOperationException("Method not decompiled: com.crashlytics.android.core.CrashlyticsCore.onPreExecute(android.content.Context):boolean");
    }

    public void setBool(String str, boolean z) {
        setString(str, Boolean.toString(z));
    }

    public void setDouble(String str, double d) {
        setString(str, Double.toString(d));
    }

    void setExternalCrashEventDataProvider(CrashEventDataProvider crashEventDataProvider) {
        this.externalCrashEventDataProvider = crashEventDataProvider;
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
            Fabric.getLogger().mo4766w("Fabric", "Use of setListener is deprecated.");
            if (crashlyticsListener == null) {
                throw new IllegalArgumentException("listener must not be null.");
            }
            this.listener = crashlyticsListener;
        }
    }

    public void setLong(String str, long j) {
        setString(str, Long.toString(j));
    }

    @SuppressLint({"CommitPrefEdits"})
    void setShouldSendUserReportsWithoutPrompting(boolean z) {
        PreferenceStore preferenceStoreImpl = new PreferenceStoreImpl(this);
        preferenceStoreImpl.save(preferenceStoreImpl.edit().putBoolean(PREF_ALWAYS_SEND_REPORTS_KEY, z));
    }

    public void setString(String str, String str2) {
        if (!this.disabled) {
            if (str != null) {
                String sanitizeAttribute = sanitizeAttribute(str);
                if (this.attributes.size() < 64 || this.attributes.containsKey(sanitizeAttribute)) {
                    this.attributes.put(sanitizeAttribute, str2 == null ? "" : sanitizeAttribute(str2));
                    this.handler.cacheKeyData(this.attributes);
                    return;
                }
                Fabric.getLogger().mo4753d("Fabric", "Exceeded maximum number of custom attributes (64)");
            } else if (getContext() == null || !CommonUtils.isAppDebuggable(getContext())) {
                Fabric.getLogger().mo4756e("Fabric", "Attempting to set custom attribute with null key, ignoring.", null);
            } else {
                throw new IllegalArgumentException("Custom attribute key must not be null.");
            }
        }
    }

    public void setUserEmail(String str) {
        if (!this.disabled) {
            this.userEmail = sanitizeAttribute(str);
            this.handler.cacheUserData(this.userId, this.userName, this.userEmail);
        }
    }

    public void setUserIdentifier(String str) {
        if (!this.disabled) {
            this.userId = sanitizeAttribute(str);
            this.handler.cacheUserData(this.userId, this.userName, this.userEmail);
        }
    }

    public void setUserName(String str) {
        if (!this.disabled) {
            this.userName = sanitizeAttribute(str);
            this.handler.cacheUserData(this.userId, this.userName, this.userEmail);
        }
    }

    boolean shouldPromptUserBeforeSendingCrashReports() {
        return ((Boolean) Settings.getInstance().withSettings(new C03135(), Boolean.valueOf(false))).booleanValue();
    }

    boolean shouldSendReportsWithoutPrompting() {
        return new PreferenceStoreImpl(this).get().getBoolean(PREF_ALWAYS_SEND_REPORTS_KEY, false);
    }

    public boolean verifyPinning(URL url) {
        try {
            return internalVerifyPinning(url);
        } catch (Throwable e) {
            Fabric.getLogger().mo4756e("Fabric", "Could not verify SSL pinning", e);
            return false;
        }
    }
}
