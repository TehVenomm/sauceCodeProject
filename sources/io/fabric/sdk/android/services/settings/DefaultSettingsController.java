package io.fabric.sdk.android.services.settings;

import android.annotation.SuppressLint;
import android.content.SharedPreferences.Editor;
import io.fabric.sdk.android.Fabric;
import io.fabric.sdk.android.Kit;
import io.fabric.sdk.android.services.common.CommonUtils;
import io.fabric.sdk.android.services.common.CurrentTimeProvider;
import io.fabric.sdk.android.services.persistence.PreferenceStore;
import io.fabric.sdk.android.services.persistence.PreferenceStoreImpl;
import org.json.JSONException;
import org.json.JSONObject;

class DefaultSettingsController implements SettingsController {
    private static final String LOAD_ERROR_MESSAGE = "Unknown error while loading Crashlytics settings. Crashes will be cached until settings can be retrieved.";
    private static final String PREFS_BUILD_INSTANCE_IDENTIFIER = "existing_instance_identifier";
    private final CachedSettingsIo cachedSettingsIo;
    private final CurrentTimeProvider currentTimeProvider;
    private final Kit kit;
    private final PreferenceStore preferenceStore = new PreferenceStoreImpl(this.kit);
    private final SettingsJsonTransform settingsJsonTransform;
    private final SettingsRequest settingsRequest;
    private final SettingsSpiCall settingsSpiCall;

    public DefaultSettingsController(Kit kit, SettingsRequest settingsRequest, CurrentTimeProvider currentTimeProvider, SettingsJsonTransform settingsJsonTransform, CachedSettingsIo cachedSettingsIo, SettingsSpiCall settingsSpiCall) {
        this.kit = kit;
        this.settingsRequest = settingsRequest;
        this.currentTimeProvider = currentTimeProvider;
        this.settingsJsonTransform = settingsJsonTransform;
        this.cachedSettingsIo = cachedSettingsIo;
        this.settingsSpiCall = settingsSpiCall;
    }

    private SettingsData getCachedSettingsData(SettingsCacheBehavior settingsCacheBehavior) {
        Throwable th;
        SettingsData settingsData = null;
        try {
            if (SettingsCacheBehavior.SKIP_CACHE_LOOKUP.equals(settingsCacheBehavior)) {
                return null;
            }
            JSONObject readCachedSettings = this.cachedSettingsIo.readCachedSettings();
            if (readCachedSettings != null) {
                SettingsData buildFromJson = this.settingsJsonTransform.buildFromJson(this.currentTimeProvider, readCachedSettings);
                if (buildFromJson != null) {
                    logSettings(readCachedSettings, "Loaded cached settings: ");
                    long currentTimeMillis = this.currentTimeProvider.getCurrentTimeMillis();
                    if (SettingsCacheBehavior.IGNORE_CACHE_EXPIRATION.equals(settingsCacheBehavior) || !buildFromJson.isExpired(currentTimeMillis)) {
                        try {
                            Fabric.getLogger().mo4289d("Fabric", "Returning cached settings.");
                            return buildFromJson;
                        } catch (Throwable e) {
                            Throwable th2 = e;
                            settingsData = buildFromJson;
                            th = th2;
                            Fabric.getLogger().mo4292e("Fabric", "Failed to get cached settings", th);
                            return settingsData;
                        }
                    }
                    Fabric.getLogger().mo4289d("Fabric", "Cached settings have expired.");
                    return null;
                }
                Fabric.getLogger().mo4292e("Fabric", "Failed to transform cached settings data.", null);
                return null;
            }
            Fabric.getLogger().mo4289d("Fabric", "No cached settings data found.");
            return null;
        } catch (Exception e2) {
            th = e2;
            Fabric.getLogger().mo4292e("Fabric", "Failed to get cached settings", th);
            return settingsData;
        }
    }

    private void logSettings(JSONObject jSONObject, String str) throws JSONException {
        if (!CommonUtils.isClsTrace(this.kit.getContext())) {
            jSONObject = this.settingsJsonTransform.sanitizeTraceInfo(jSONObject);
        }
        Fabric.getLogger().mo4289d("Fabric", str + jSONObject.toString());
    }

    boolean buildInstanceIdentifierChanged() {
        return !getStoredBuildInstanceIdentifier().equals(getBuildInstanceIdentifierFromContext());
    }

    String getBuildInstanceIdentifierFromContext() {
        return CommonUtils.createInstanceIdFrom(CommonUtils.resolveBuildId(this.kit.getContext()));
    }

    String getStoredBuildInstanceIdentifier() {
        return this.preferenceStore.get().getString(PREFS_BUILD_INSTANCE_IDENTIFIER, "");
    }

    public SettingsData loadSettingsData() {
        return loadSettingsData(SettingsCacheBehavior.USE_CACHE);
    }

    public SettingsData loadSettingsData(SettingsCacheBehavior settingsCacheBehavior) {
        SettingsData settingsData;
        Throwable e;
        Throwable th;
        SettingsData settingsData2 = null;
        try {
            if (!(Fabric.isDebuggable() || buildInstanceIdentifierChanged())) {
                settingsData2 = getCachedSettingsData(settingsCacheBehavior);
            }
            if (settingsData2 == null) {
                try {
                    JSONObject invoke = this.settingsSpiCall.invoke(this.settingsRequest);
                    if (invoke != null) {
                        settingsData2 = this.settingsJsonTransform.buildFromJson(this.currentTimeProvider, invoke);
                        this.cachedSettingsIo.writeCachedSettings(settingsData2.expiresAtMillis, invoke);
                        logSettings(invoke, "Loaded settings: ");
                        setStoredBuildInstanceIdentifier(getBuildInstanceIdentifierFromContext());
                        settingsData = settingsData2;
                        if (settingsData == null) {
                            try {
                                settingsData = getCachedSettingsData(SettingsCacheBehavior.IGNORE_CACHE_EXPIRATION);
                            } catch (Exception e2) {
                                e = e2;
                                Fabric.getLogger().mo4292e("Fabric", LOAD_ERROR_MESSAGE, e);
                                return settingsData;
                            }
                        }
                        return settingsData;
                    }
                } catch (Throwable e3) {
                    th = e3;
                    settingsData = settingsData2;
                    e = th;
                    Fabric.getLogger().mo4292e("Fabric", LOAD_ERROR_MESSAGE, e);
                    return settingsData;
                }
            }
            settingsData = settingsData2;
            if (settingsData == null) {
                settingsData = getCachedSettingsData(SettingsCacheBehavior.IGNORE_CACHE_EXPIRATION);
            }
        } catch (Throwable e32) {
            th = e32;
            settingsData = null;
            e = th;
            Fabric.getLogger().mo4292e("Fabric", LOAD_ERROR_MESSAGE, e);
            return settingsData;
        }
        return settingsData;
    }

    @SuppressLint({"CommitPrefEdits"})
    boolean setStoredBuildInstanceIdentifier(String str) {
        Editor edit = this.preferenceStore.edit();
        edit.putString(PREFS_BUILD_INSTANCE_IDENTIFIER, str);
        return this.preferenceStore.save(edit);
    }
}
