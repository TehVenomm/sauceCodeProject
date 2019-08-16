package p017io.fabric.sdk.android.services.settings;

import android.annotation.SuppressLint;
import android.content.SharedPreferences.Editor;
import org.json.JSONException;
import org.json.JSONObject;
import p017io.fabric.sdk.android.Fabric;
import p017io.fabric.sdk.android.Kit;
import p017io.fabric.sdk.android.services.common.CommonUtils;
import p017io.fabric.sdk.android.services.common.CurrentTimeProvider;
import p017io.fabric.sdk.android.services.common.DataCollectionArbiter;
import p017io.fabric.sdk.android.services.persistence.PreferenceStore;
import p017io.fabric.sdk.android.services.persistence.PreferenceStoreImpl;

/* renamed from: io.fabric.sdk.android.services.settings.DefaultSettingsController */
class DefaultSettingsController implements SettingsController {
    private static final String LOAD_ERROR_MESSAGE = "Unknown error while loading Crashlytics settings. Crashes will be cached until settings can be retrieved.";
    private static final String PREFS_BUILD_INSTANCE_IDENTIFIER = "existing_instance_identifier";
    private final CachedSettingsIo cachedSettingsIo;
    private final CurrentTimeProvider currentTimeProvider;
    private final DataCollectionArbiter dataCollectionArbiter;
    private final Kit kit;
    private final PreferenceStore preferenceStore = new PreferenceStoreImpl(this.kit);
    private final SettingsJsonTransform settingsJsonTransform;
    private final SettingsRequest settingsRequest;
    private final SettingsSpiCall settingsSpiCall;

    public DefaultSettingsController(Kit kit2, SettingsRequest settingsRequest2, CurrentTimeProvider currentTimeProvider2, SettingsJsonTransform settingsJsonTransform2, CachedSettingsIo cachedSettingsIo2, SettingsSpiCall settingsSpiCall2, DataCollectionArbiter dataCollectionArbiter2) {
        this.kit = kit2;
        this.settingsRequest = settingsRequest2;
        this.currentTimeProvider = currentTimeProvider2;
        this.settingsJsonTransform = settingsJsonTransform2;
        this.cachedSettingsIo = cachedSettingsIo2;
        this.settingsSpiCall = settingsSpiCall2;
        this.dataCollectionArbiter = dataCollectionArbiter2;
    }

    private SettingsData getCachedSettingsData(SettingsCacheBehavior settingsCacheBehavior) {
        Exception e;
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
                            Fabric.getLogger().mo20969d(Fabric.TAG, "Returning cached settings.");
                            return buildFromJson;
                        } catch (Exception e2) {
                            e = e2;
                            settingsData = buildFromJson;
                            Fabric.getLogger().mo20972e(Fabric.TAG, "Failed to get cached settings", e);
                            return settingsData;
                        }
                    } else {
                        Fabric.getLogger().mo20969d(Fabric.TAG, "Cached settings have expired.");
                        return null;
                    }
                } else {
                    Fabric.getLogger().mo20972e(Fabric.TAG, "Failed to transform cached settings data.", null);
                    return null;
                }
            } else {
                Fabric.getLogger().mo20969d(Fabric.TAG, "No cached settings data found.");
                return null;
            }
        } catch (Exception e3) {
            e = e3;
        }
    }

    private void logSettings(JSONObject jSONObject, String str) throws JSONException {
        Fabric.getLogger().mo20969d(Fabric.TAG, str + jSONObject.toString());
    }

    /* access modifiers changed from: 0000 */
    public boolean buildInstanceIdentifierChanged() {
        return !getStoredBuildInstanceIdentifier().equals(getBuildInstanceIdentifierFromContext());
    }

    /* access modifiers changed from: 0000 */
    public String getBuildInstanceIdentifierFromContext() {
        return CommonUtils.createInstanceIdFrom(CommonUtils.resolveBuildId(this.kit.getContext()));
    }

    /* access modifiers changed from: 0000 */
    public String getStoredBuildInstanceIdentifier() {
        return this.preferenceStore.get().getString(PREFS_BUILD_INSTANCE_IDENTIFIER, "");
    }

    public SettingsData loadSettingsData() {
        return loadSettingsData(SettingsCacheBehavior.USE_CACHE);
    }

    public SettingsData loadSettingsData(SettingsCacheBehavior settingsCacheBehavior) {
        SettingsData settingsData = null;
        if (!this.dataCollectionArbiter.isDataCollectionEnabled()) {
            Fabric.getLogger().mo20969d(Fabric.TAG, "Not fetching settings, because data collection is disabled by Firebase.");
            return null;
        }
        try {
            if (!Fabric.isDebuggable() && !buildInstanceIdentifierChanged()) {
                settingsData = getCachedSettingsData(settingsCacheBehavior);
            }
            if (settingsData == null) {
                JSONObject invoke = this.settingsSpiCall.invoke(this.settingsRequest);
                if (invoke != null) {
                    settingsData = this.settingsJsonTransform.buildFromJson(this.currentTimeProvider, invoke);
                    this.cachedSettingsIo.writeCachedSettings(settingsData.expiresAtMillis, invoke);
                    logSettings(invoke, "Loaded settings: ");
                    setStoredBuildInstanceIdentifier(getBuildInstanceIdentifierFromContext());
                }
            }
            return settingsData == null ? getCachedSettingsData(SettingsCacheBehavior.IGNORE_CACHE_EXPIRATION) : settingsData;
        } catch (Exception e) {
            Fabric.getLogger().mo20972e(Fabric.TAG, LOAD_ERROR_MESSAGE, e);
            return null;
        }
    }

    /* access modifiers changed from: 0000 */
    @SuppressLint({"CommitPrefEdits"})
    public boolean setStoredBuildInstanceIdentifier(String str) {
        Editor edit = this.preferenceStore.edit();
        edit.putString(PREFS_BUILD_INSTANCE_IDENTIFIER, str);
        return this.preferenceStore.save(edit);
    }
}
