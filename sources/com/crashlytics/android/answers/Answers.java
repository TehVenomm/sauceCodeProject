package com.crashlytics.android.answers;

import android.annotation.SuppressLint;
import android.annotation.TargetApi;
import android.app.Application;
import android.content.Context;
import android.content.pm.PackageInfo;
import android.os.Build.VERSION;
import io.fabric.sdk.android.Fabric;
import io.fabric.sdk.android.Kit;
import io.fabric.sdk.android.services.common.CommonUtils;
import io.fabric.sdk.android.services.common.Crash.FatalException;
import io.fabric.sdk.android.services.common.Crash.LoggedException;
import io.fabric.sdk.android.services.common.IdManager;
import io.fabric.sdk.android.services.common.IdManager.DeviceIdentifierType;
import io.fabric.sdk.android.services.common.SystemCurrentTimeProvider;
import io.fabric.sdk.android.services.events.GZIPQueueFileEventStorage;
import io.fabric.sdk.android.services.network.DefaultHttpRequestFactory;
import io.fabric.sdk.android.services.persistence.FileStoreImpl;
import io.fabric.sdk.android.services.persistence.PreferenceStore;
import io.fabric.sdk.android.services.persistence.PreferenceStoreImpl;
import io.fabric.sdk.android.services.settings.Settings;
import io.fabric.sdk.android.services.settings.SettingsData;
import java.io.File;
import java.util.Map;
import java.util.UUID;

public class Answers extends Kit<Boolean> {
    static final String CRASHLYTICS_API_ENDPOINT = "com.crashlytics.ApiEndpoint";
    static final long FIRST_LAUNCH_INTERVAL_IN_MS = 3600000;
    static final String PREFKEY_ANALYTICS_LAUNCHED = "analytics_launched";
    static final String SESSION_ANALYTICS_FILE_EXTENSION = ".tap";
    static final String SESSION_ANALYTICS_FILE_NAME = "session_analytics.tap";
    private static final String SESSION_ANALYTICS_TO_SEND_DIR = "session_analytics_to_send";
    public static final String TAG = "Answers";
    private long installedAt;
    private PreferenceStore preferenceStore;
    SessionAnalyticsManager sessionAnalyticsManager;
    private String versionCode;
    private String versionName;

    public static Answers getInstance() {
        return (Answers) Fabric.getKit(Answers.class);
    }

    @SuppressLint({"CommitPrefEdits"})
    @TargetApi(14)
    private void initializeSessionAnalytics(Context context) {
        try {
            SessionAnalyticsFilesManager sessionAnalyticsFilesManager = new SessionAnalyticsFilesManager(context, new SessionEventTransform(), new SystemCurrentTimeProvider(), new GZIPQueueFileEventStorage(getContext(), getSdkDirectory(), SESSION_ANALYTICS_FILE_NAME, SESSION_ANALYTICS_TO_SEND_DIR));
            IdManager idManager = getIdManager();
            Map deviceIdentifiers = idManager.getDeviceIdentifiers();
            SessionEventMetadata sessionEventMetadata = new SessionEventMetadata(context.getPackageName(), UUID.randomUUID().toString(), idManager.getAppInstallIdentifier(), (String) deviceIdentifiers.get(DeviceIdentifierType.ANDROID_ID), (String) deviceIdentifiers.get(DeviceIdentifierType.ANDROID_ADVERTISING_ID), (String) deviceIdentifiers.get(DeviceIdentifierType.FONT_TOKEN), CommonUtils.resolveBuildId(context), idManager.getOsVersionString(), idManager.getModelName(), this.versionCode, this.versionName);
            Application application = (Application) getContext().getApplicationContext();
            if (application == null || VERSION.SDK_INT < 14) {
                this.sessionAnalyticsManager = SessionAnalyticsManager.build(context, sessionEventMetadata, sessionAnalyticsFilesManager, new DefaultHttpRequestFactory(Fabric.getLogger()));
            } else {
                this.sessionAnalyticsManager = AutoSessionAnalyticsManager.build(application, sessionEventMetadata, sessionAnalyticsFilesManager, new DefaultHttpRequestFactory(Fabric.getLogger()));
            }
            if (isFirstLaunch(this.installedAt)) {
                Fabric.getLogger().mo4753d(TAG, "First launch");
                this.sessionAnalyticsManager.onInstall();
                this.preferenceStore.save(this.preferenceStore.edit().putBoolean(PREFKEY_ANALYTICS_LAUNCHED, true));
            }
        } catch (Throwable e) {
            CommonUtils.logControlledError(context, "Crashlytics failed to initialize session analytics.", e);
        }
    }

    protected Boolean doInBackground() {
        Context context = getContext();
        initializeSessionAnalytics(context);
        try {
            SettingsData awaitSettingsData = Settings.getInstance().awaitSettingsData();
            if (awaitSettingsData == null) {
                return Boolean.valueOf(false);
            }
            if (awaitSettingsData.featuresData.collectAnalytics) {
                this.sessionAnalyticsManager.setAnalyticsSettingsData(awaitSettingsData.analyticsSettingsData, getOverridenSpiEndpoint());
                return Boolean.valueOf(true);
            }
            CommonUtils.logControlled(context, "Disabling analytics collection based on settings flag value.");
            this.sessionAnalyticsManager.disable();
            return Boolean.valueOf(false);
        } catch (Throwable e) {
            Fabric.getLogger().mo4756e(TAG, "Error dealing with settings", e);
            return Boolean.valueOf(false);
        }
    }

    boolean getAnalyticsLaunched() {
        return this.preferenceStore.get().getBoolean(PREFKEY_ANALYTICS_LAUNCHED, false);
    }

    public String getIdentifier() {
        return "com.crashlytics.sdk.android:answers";
    }

    String getOverridenSpiEndpoint() {
        return CommonUtils.getStringsFileValue(getContext(), CRASHLYTICS_API_ENDPOINT);
    }

    File getSdkDirectory() {
        return new FileStoreImpl(this).getFilesDir();
    }

    public String getVersion() {
        return "1.2.2.56";
    }

    boolean installedRecently(long j) {
        return System.currentTimeMillis() - j < 3600000;
    }

    boolean isFirstLaunch(long j) {
        return !getAnalyticsLaunched() && installedRecently(j);
    }

    public void logEvent(String str) {
        logEvent(str, new EventAttributes());
    }

    public void logEvent(String str, EventAttributes eventAttributes) {
        if (str == null) {
            throw new NullPointerException("eventName must not be null");
        } else if (eventAttributes == null) {
            throw new NullPointerException("attributes must not be null");
        } else if (this.sessionAnalyticsManager != null) {
            this.sessionAnalyticsManager.onCustom(str, eventAttributes.attributes);
        }
    }

    public void onException(FatalException fatalException) {
        if (this.sessionAnalyticsManager != null) {
            this.sessionAnalyticsManager.onCrash(fatalException.getSessionId());
        }
    }

    public void onException(LoggedException loggedException) {
        if (this.sessionAnalyticsManager != null) {
            this.sessionAnalyticsManager.onError(loggedException.getSessionId());
        }
    }

    @SuppressLint({"NewApi"})
    protected boolean onPreExecute() {
        try {
            this.preferenceStore = new PreferenceStoreImpl(this);
            Context context = getContext();
            PackageInfo packageInfo = context.getPackageManager().getPackageInfo(context.getPackageName(), 0);
            this.versionCode = Integer.toString(packageInfo.versionCode);
            this.versionName = packageInfo.versionName == null ? IdManager.DEFAULT_VERSION_NAME : packageInfo.versionName;
            if (VERSION.SDK_INT >= 9) {
                this.installedAt = packageInfo.firstInstallTime;
            } else {
                this.installedAt = new File(context.getPackageManager().getApplicationInfo(context.getPackageName(), 0).sourceDir).lastModified();
            }
            return true;
        } catch (Throwable e) {
            Fabric.getLogger().mo4756e(TAG, "Error setting up app properties", e);
            return false;
        }
    }
}
