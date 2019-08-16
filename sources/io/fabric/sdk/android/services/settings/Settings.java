package p017io.fabric.sdk.android.services.settings;

import android.content.Context;
import java.util.Locale;
import java.util.concurrent.CountDownLatch;
import java.util.concurrent.atomic.AtomicReference;
import p017io.fabric.sdk.android.Fabric;
import p017io.fabric.sdk.android.Kit;
import p017io.fabric.sdk.android.services.common.ApiKey;
import p017io.fabric.sdk.android.services.common.CommonUtils;
import p017io.fabric.sdk.android.services.common.DataCollectionArbiter;
import p017io.fabric.sdk.android.services.common.DeliveryMechanism;
import p017io.fabric.sdk.android.services.common.IdManager;
import p017io.fabric.sdk.android.services.common.SystemCurrentTimeProvider;
import p017io.fabric.sdk.android.services.network.HttpRequestFactory;

/* renamed from: io.fabric.sdk.android.services.settings.Settings */
public class Settings {
    public static final String SETTINGS_CACHE_FILENAME = "com.crashlytics.settings.json";
    private static final String SETTINGS_URL_FORMAT = "https://settings.crashlytics.com/spi/v2/platforms/android/apps/%s/settings";
    private boolean initialized;
    private SettingsController settingsController;
    private final AtomicReference<SettingsData> settingsData;
    private final CountDownLatch settingsDataLatch;

    /* renamed from: io.fabric.sdk.android.services.settings.Settings$LazyHolder */
    static class LazyHolder {
        /* access modifiers changed from: private */
        public static final Settings INSTANCE = new Settings();

        LazyHolder() {
        }
    }

    /* renamed from: io.fabric.sdk.android.services.settings.Settings$SettingsAccess */
    public interface SettingsAccess<T> {
        T usingSettings(SettingsData settingsData);
    }

    private Settings() {
        this.settingsData = new AtomicReference<>();
        this.settingsDataLatch = new CountDownLatch(1);
        this.initialized = false;
    }

    public static Settings getInstance() {
        return LazyHolder.INSTANCE;
    }

    private void setSettingsData(SettingsData settingsData2) {
        this.settingsData.set(settingsData2);
        this.settingsDataLatch.countDown();
    }

    public SettingsData awaitSettingsData() {
        try {
            this.settingsDataLatch.await();
            return (SettingsData) this.settingsData.get();
        } catch (InterruptedException e) {
            Fabric.getLogger().mo20971e(Fabric.TAG, "Interrupted while waiting for settings data.");
            return null;
        }
    }

    public void clearSettings() {
        this.settingsData.set(null);
    }

    public Settings initialize(Kit kit, IdManager idManager, HttpRequestFactory httpRequestFactory, String str, String str2, String str3, DataCollectionArbiter dataCollectionArbiter) {
        synchronized (this) {
            if (!this.initialized) {
                if (this.settingsController == null) {
                    Context context = kit.getContext();
                    String appIdentifier = idManager.getAppIdentifier();
                    String value = new ApiKey().getValue(context);
                    String installerPackageName = idManager.getInstallerPackageName();
                    SystemCurrentTimeProvider systemCurrentTimeProvider = new SystemCurrentTimeProvider();
                    DefaultSettingsJsonTransform defaultSettingsJsonTransform = new DefaultSettingsJsonTransform();
                    DefaultCachedSettingsIo defaultCachedSettingsIo = new DefaultCachedSettingsIo(kit);
                    String appIconHashOrNull = CommonUtils.getAppIconHashOrNull(context);
                    Kit kit2 = kit;
                    String str4 = str3;
                    DefaultSettingsSpiCall defaultSettingsSpiCall = new DefaultSettingsSpiCall(kit2, str4, String.format(Locale.US, SETTINGS_URL_FORMAT, new Object[]{appIdentifier}), httpRequestFactory);
                    String str5 = str2;
                    String str6 = str;
                    this.settingsController = new DefaultSettingsController(kit, new SettingsRequest(value, idManager.getModelName(), idManager.getOsBuildVersionString(), idManager.getOsDisplayVersionString(), idManager.getAppInstallIdentifier(), CommonUtils.createInstanceIdFrom(CommonUtils.resolveBuildId(context)), str5, str6, DeliveryMechanism.determineFrom(installerPackageName).getId(), appIconHashOrNull), systemCurrentTimeProvider, defaultSettingsJsonTransform, defaultCachedSettingsIo, defaultSettingsSpiCall, dataCollectionArbiter);
                }
                this.initialized = true;
            }
        }
        return this;
    }

    public boolean loadSettingsData() {
        boolean z;
        synchronized (this) {
            SettingsData loadSettingsData = this.settingsController.loadSettingsData();
            setSettingsData(loadSettingsData);
            z = loadSettingsData != null;
        }
        return z;
    }

    public boolean loadSettingsSkippingCache() {
        boolean z;
        synchronized (this) {
            SettingsData loadSettingsData = this.settingsController.loadSettingsData(SettingsCacheBehavior.SKIP_CACHE_LOOKUP);
            setSettingsData(loadSettingsData);
            if (loadSettingsData == null) {
                Fabric.getLogger().mo20972e(Fabric.TAG, "Failed to force reload of settings from Crashlytics.", null);
            }
            z = loadSettingsData != null;
        }
        return z;
    }

    public void setSettingsController(SettingsController settingsController2) {
        this.settingsController = settingsController2;
    }

    public <T> T withSettings(SettingsAccess<T> settingsAccess, T t) {
        SettingsData settingsData2 = (SettingsData) this.settingsData.get();
        return settingsData2 == null ? t : settingsAccess.usingSettings(settingsData2);
    }
}
