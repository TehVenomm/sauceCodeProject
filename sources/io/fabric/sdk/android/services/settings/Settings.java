package io.fabric.sdk.android.services.settings;

import android.content.Context;
import io.fabric.sdk.android.Fabric;
import io.fabric.sdk.android.Kit;
import io.fabric.sdk.android.services.common.ApiKey;
import io.fabric.sdk.android.services.common.CommonUtils;
import io.fabric.sdk.android.services.common.DeliveryMechanism;
import io.fabric.sdk.android.services.common.IdManager;
import io.fabric.sdk.android.services.common.SystemCurrentTimeProvider;
import io.fabric.sdk.android.services.network.HttpRequestFactory;
import java.util.Locale;
import java.util.concurrent.CountDownLatch;
import java.util.concurrent.atomic.AtomicReference;

public class Settings {
    public static final String SETTINGS_CACHE_FILENAME = "com.crashlytics.settings.json";
    private static final String SETTINGS_URL_FORMAT = "https://settings.crashlytics.com/spi/v2/platforms/android/apps/%s/settings";
    private boolean initialized;
    private SettingsController settingsController;
    private final AtomicReference<SettingsData> settingsData;
    private final CountDownLatch settingsDataLatch;

    public interface SettingsAccess<T> {
        T usingSettings(SettingsData settingsData);
    }

    static class LazyHolder {
        private static final Settings INSTANCE = new Settings();

        LazyHolder() {
        }
    }

    private Settings() {
        this.settingsData = new AtomicReference();
        this.settingsDataLatch = new CountDownLatch(1);
        this.initialized = false;
    }

    public static Settings getInstance() {
        return LazyHolder.INSTANCE;
    }

    private void setSettingsData(SettingsData settingsData) {
        this.settingsData.set(settingsData);
        this.settingsDataLatch.countDown();
    }

    public SettingsData awaitSettingsData() {
        try {
            this.settingsDataLatch.await();
            return (SettingsData) this.settingsData.get();
        } catch (InterruptedException e) {
            Fabric.getLogger().mo4291e("Fabric", "Interrupted while waiting for settings data.");
            return null;
        }
    }

    public void clearSettings() {
        this.settingsData.set(null);
    }

    public Settings initialize(Kit kit, IdManager idManager, HttpRequestFactory httpRequestFactory, String str, String str2, String str3) {
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
                    String str4 = str3;
                    DefaultSettingsSpiCall defaultSettingsSpiCall = new DefaultSettingsSpiCall(kit, str4, String.format(Locale.US, SETTINGS_URL_FORMAT, new Object[]{appIdentifier}), httpRequestFactory);
                    installerPackageName = str2;
                    String str5 = str;
                    this.settingsController = new DefaultSettingsController(kit, new SettingsRequest(value, idManager.createIdHeaderValue(value, appIdentifier), CommonUtils.createInstanceIdFrom(CommonUtils.resolveBuildId(context)), installerPackageName, str5, DeliveryMechanism.determineFrom(installerPackageName).getId(), appIconHashOrNull), systemCurrentTimeProvider, defaultSettingsJsonTransform, defaultCachedSettingsIo, defaultSettingsSpiCall);
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
                Fabric.getLogger().mo4292e("Fabric", "Failed to force reload of settings from Crashlytics.", null);
            }
            z = loadSettingsData != null;
        }
        return z;
    }

    public void setSettingsController(SettingsController settingsController) {
        this.settingsController = settingsController;
    }

    public <T> T withSettings(SettingsAccess<T> settingsAccess, T t) {
        SettingsData settingsData = (SettingsData) this.settingsData.get();
        return settingsData == null ? t : settingsAccess.usingSettings(settingsData);
    }
}
