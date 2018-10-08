package com.crashlytics.android.beta;

import android.annotation.TargetApi;
import android.app.Application;
import android.content.Context;
import android.os.Build.VERSION;
import android.text.TextUtils;
import io.fabric.sdk.android.Fabric;
import io.fabric.sdk.android.Kit;
import io.fabric.sdk.android.services.cache.MemoryValueCache;
import io.fabric.sdk.android.services.common.CommonUtils;
import io.fabric.sdk.android.services.common.DeliveryMechanism;
import io.fabric.sdk.android.services.common.DeviceIdentifierProvider;
import io.fabric.sdk.android.services.common.IdManager;
import io.fabric.sdk.android.services.common.IdManager.DeviceIdentifierType;
import io.fabric.sdk.android.services.common.SystemCurrentTimeProvider;
import io.fabric.sdk.android.services.network.DefaultHttpRequestFactory;
import io.fabric.sdk.android.services.persistence.PreferenceStoreImpl;
import io.fabric.sdk.android.services.settings.BetaSettingsData;
import io.fabric.sdk.android.services.settings.Settings;
import io.fabric.sdk.android.services.settings.SettingsData;
import java.util.HashMap;
import java.util.Map;

public class Beta extends Kit<Boolean> implements DeviceIdentifierProvider {
    private static final String CRASHLYTICS_API_ENDPOINT = "com.crashlytics.ApiEndpoint";
    private static final String CRASHLYTICS_BUILD_PROPERTIES = "crashlytics-build.properties";
    static final String NO_DEVICE_TOKEN = "";
    public static final String TAG = "Beta";
    private final MemoryValueCache<String> deviceTokenCache = new MemoryValueCache();
    private final DeviceTokenLoader deviceTokenLoader = new DeviceTokenLoader();
    private UpdatesController updatesController;

    private String getBetaDeviceToken(Context context, String str) {
        if (isAppPossiblyInstalledByBeta(str, VERSION.SDK_INT)) {
            Fabric.getLogger().mo4289d(TAG, "App was possibly installed by Beta. Getting device token");
            try {
                String str2 = (String) this.deviceTokenCache.get(context, this.deviceTokenLoader);
                return "".equals(str2) ? null : str2;
            } catch (Throwable e) {
                Fabric.getLogger().mo4292e(TAG, "Failed to load the Beta device token", e);
                return null;
            }
        }
        Fabric.getLogger().mo4289d(TAG, "App was not installed by Beta. Skipping device token");
        return null;
    }

    private BetaSettingsData getBetaSettingsData() {
        SettingsData awaitSettingsData = Settings.getInstance().awaitSettingsData();
        return awaitSettingsData != null ? awaitSettingsData.betaSettingsData : null;
    }

    public static Beta getInstance() {
        return (Beta) Fabric.getKit(Beta.class);
    }

    /* JADX WARNING: inconsistent code. */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    private com.crashlytics.android.beta.BuildProperties loadBuildProperties(android.content.Context r8) {
        /*
        r7 = this;
        r2 = 0;
        r0 = r8.getAssets();	 Catch:{ Exception -> 0x0067, all -> 0x0088 }
        r1 = "crashlytics-build.properties";
        r1 = r0.open(r1);	 Catch:{ Exception -> 0x0067, all -> 0x0088 }
        if (r1 == 0) goto L_0x00ae;
    L_0x000d:
        r2 = com.crashlytics.android.beta.BuildProperties.fromPropertiesStream(r1);	 Catch:{ Exception -> 0x00a2, all -> 0x009d }
        r0 = io.fabric.sdk.android.Fabric.getLogger();	 Catch:{ Exception -> 0x00a8, all -> 0x009d }
        r3 = new java.lang.StringBuilder;	 Catch:{ Exception -> 0x00a8, all -> 0x009d }
        r3.<init>();	 Catch:{ Exception -> 0x00a8, all -> 0x009d }
        r4 = "Beta";
        r5 = r2.packageName;	 Catch:{ Exception -> 0x00a8, all -> 0x009d }
        r3 = r3.append(r5);	 Catch:{ Exception -> 0x00a8, all -> 0x009d }
        r5 = " build properties: ";
        r3 = r3.append(r5);	 Catch:{ Exception -> 0x00a8, all -> 0x009d }
        r5 = r2.versionName;	 Catch:{ Exception -> 0x00a8, all -> 0x009d }
        r3 = r3.append(r5);	 Catch:{ Exception -> 0x00a8, all -> 0x009d }
        r5 = " (";
        r3 = r3.append(r5);	 Catch:{ Exception -> 0x00a8, all -> 0x009d }
        r5 = r2.versionCode;	 Catch:{ Exception -> 0x00a8, all -> 0x009d }
        r3 = r3.append(r5);	 Catch:{ Exception -> 0x00a8, all -> 0x009d }
        r5 = ")";
        r3 = r3.append(r5);	 Catch:{ Exception -> 0x00a8, all -> 0x009d }
        r5 = " - ";
        r3 = r3.append(r5);	 Catch:{ Exception -> 0x00a8, all -> 0x009d }
        r5 = r2.buildId;	 Catch:{ Exception -> 0x00a8, all -> 0x009d }
        r3 = r3.append(r5);	 Catch:{ Exception -> 0x00a8, all -> 0x009d }
        r3 = r3.toString();	 Catch:{ Exception -> 0x00a8, all -> 0x009d }
        r0.mo4289d(r4, r3);	 Catch:{ Exception -> 0x00a8, all -> 0x009d }
        r0 = r2;
    L_0x0054:
        if (r1 == 0) goto L_0x0059;
    L_0x0056:
        r1.close();	 Catch:{ IOException -> 0x005a }
    L_0x0059:
        return r0;
    L_0x005a:
        r1 = move-exception;
        r2 = io.fabric.sdk.android.Fabric.getLogger();
        r3 = "Beta";
        r4 = "Error closing Beta build properties asset";
        r2.mo4292e(r3, r4, r1);
        goto L_0x0059;
    L_0x0067:
        r0 = move-exception;
        r1 = r0;
        r0 = r2;
    L_0x006a:
        r3 = io.fabric.sdk.android.Fabric.getLogger();	 Catch:{ all -> 0x009f }
        r4 = "Beta";
        r5 = "Error reading Beta build properties";
        r3.mo4292e(r4, r5, r1);	 Catch:{ all -> 0x009f }
        if (r2 == 0) goto L_0x0059;
    L_0x0077:
        r2.close();	 Catch:{ IOException -> 0x007b }
        goto L_0x0059;
    L_0x007b:
        r1 = move-exception;
        r2 = io.fabric.sdk.android.Fabric.getLogger();
        r3 = "Beta";
        r4 = "Error closing Beta build properties asset";
        r2.mo4292e(r3, r4, r1);
        goto L_0x0059;
    L_0x0088:
        r0 = move-exception;
        r1 = r2;
    L_0x008a:
        if (r1 == 0) goto L_0x008f;
    L_0x008c:
        r1.close();	 Catch:{ IOException -> 0x0090 }
    L_0x008f:
        throw r0;
    L_0x0090:
        r1 = move-exception;
        r2 = io.fabric.sdk.android.Fabric.getLogger();
        r3 = "Beta";
        r4 = "Error closing Beta build properties asset";
        r2.mo4292e(r3, r4, r1);
        goto L_0x008f;
    L_0x009d:
        r0 = move-exception;
        goto L_0x008a;
    L_0x009f:
        r0 = move-exception;
        r1 = r2;
        goto L_0x008a;
    L_0x00a2:
        r0 = move-exception;
        r6 = r0;
        r0 = r2;
        r2 = r1;
        r1 = r6;
        goto L_0x006a;
    L_0x00a8:
        r0 = move-exception;
        r6 = r0;
        r0 = r2;
        r2 = r1;
        r1 = r6;
        goto L_0x006a;
    L_0x00ae:
        r0 = r2;
        goto L_0x0054;
        */
        throw new UnsupportedOperationException("Method not decompiled: com.crashlytics.android.beta.Beta.loadBuildProperties(android.content.Context):com.crashlytics.android.beta.BuildProperties");
    }

    boolean canCheckForUpdates(BetaSettingsData betaSettingsData, BuildProperties buildProperties) {
        return (betaSettingsData == null || TextUtils.isEmpty(betaSettingsData.updateUrl) || buildProperties == null) ? false : true;
    }

    @TargetApi(14)
    UpdatesController createUpdatesController(int i, Application application) {
        return i >= 14 ? new ActivityLifecycleCheckForUpdatesController(getFabric().getActivityLifecycleManager(), getFabric().getExecutorService()) : new ImmediateCheckForUpdatesController();
    }

    protected Boolean doInBackground() {
        Fabric.getLogger().mo4289d(TAG, "Beta kit initializing...");
        Context context = getContext();
        IdManager idManager = getIdManager();
        if (TextUtils.isEmpty(getBetaDeviceToken(context, idManager.getInstallerPackageName()))) {
            Fabric.getLogger().mo4289d(TAG, "A Beta device token was not found for this app");
            return Boolean.valueOf(false);
        }
        Fabric.getLogger().mo4289d(TAG, "Beta device token is present, checking for app updates.");
        BetaSettingsData betaSettingsData = getBetaSettingsData();
        BuildProperties loadBuildProperties = loadBuildProperties(context);
        if (canCheckForUpdates(betaSettingsData, loadBuildProperties)) {
            this.updatesController.initialize(context, this, idManager, betaSettingsData, loadBuildProperties, new PreferenceStoreImpl(this), new SystemCurrentTimeProvider(), new DefaultHttpRequestFactory(Fabric.getLogger()));
        }
        return Boolean.valueOf(true);
    }

    public Map<DeviceIdentifierType, String> getDeviceIdentifiers() {
        CharSequence betaDeviceToken = getBetaDeviceToken(getContext(), getIdManager().getInstallerPackageName());
        Map<DeviceIdentifierType, String> hashMap = new HashMap();
        if (!TextUtils.isEmpty(betaDeviceToken)) {
            hashMap.put(DeviceIdentifierType.FONT_TOKEN, betaDeviceToken);
        }
        return hashMap;
    }

    public String getIdentifier() {
        return "com.crashlytics.sdk.android:beta";
    }

    String getOverridenSpiEndpoint() {
        return CommonUtils.getStringsFileValue(getContext(), CRASHLYTICS_API_ENDPOINT);
    }

    public String getVersion() {
        return "1.1.3.61";
    }

    @TargetApi(11)
    boolean isAppPossiblyInstalledByBeta(String str, int i) {
        return i < 11 ? str == null : DeliveryMechanism.BETA_APP_PACKAGE_NAME.equals(str);
    }

    @TargetApi(14)
    protected boolean onPreExecute() {
        this.updatesController = createUpdatesController(VERSION.SDK_INT, (Application) getContext().getApplicationContext());
        return true;
    }
}
