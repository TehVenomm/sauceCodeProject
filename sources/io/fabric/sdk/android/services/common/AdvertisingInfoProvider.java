package p017io.fabric.sdk.android.services.common;

import android.annotation.SuppressLint;
import android.content.Context;
import android.text.TextUtils;
import p017io.fabric.sdk.android.Fabric;
import p017io.fabric.sdk.android.services.persistence.PreferenceStore;
import p017io.fabric.sdk.android.services.persistence.PreferenceStoreImpl;

/* renamed from: io.fabric.sdk.android.services.common.AdvertisingInfoProvider */
class AdvertisingInfoProvider {
    private static final String ADVERTISING_INFO_PREFERENCES = "TwitterAdvertisingInfoPreferences";
    private static final String PREFKEY_ADVERTISING_ID = "advertising_id";
    private static final String PREFKEY_LIMIT_AD_TRACKING = "limit_ad_tracking_enabled";
    private final Context context;
    private final PreferenceStore preferenceStore;

    public AdvertisingInfoProvider(Context context2) {
        this.context = context2.getApplicationContext();
        this.preferenceStore = new PreferenceStoreImpl(context2, ADVERTISING_INFO_PREFERENCES);
    }

    /* access modifiers changed from: private */
    public AdvertisingInfo getAdvertisingInfoFromStrategies() {
        AdvertisingInfo advertisingInfo = getReflectionStrategy().getAdvertisingInfo();
        if (!isInfoValid(advertisingInfo)) {
            advertisingInfo = getServiceStrategy().getAdvertisingInfo();
            if (!isInfoValid(advertisingInfo)) {
                Fabric.getLogger().mo20969d(Fabric.TAG, "AdvertisingInfo not present");
            } else {
                Fabric.getLogger().mo20969d(Fabric.TAG, "Using AdvertisingInfo from Service Provider");
            }
        } else {
            Fabric.getLogger().mo20969d(Fabric.TAG, "Using AdvertisingInfo from Reflection Provider");
        }
        return advertisingInfo;
    }

    private boolean isInfoValid(AdvertisingInfo advertisingInfo) {
        return advertisingInfo != null && !TextUtils.isEmpty(advertisingInfo.advertisingId);
    }

    private void refreshInfoIfNeededAsync(final AdvertisingInfo advertisingInfo) {
        new Thread(new BackgroundPriorityRunnable() {
            public void onRun() {
                AdvertisingInfo access$000 = AdvertisingInfoProvider.this.getAdvertisingInfoFromStrategies();
                if (!advertisingInfo.equals(access$000)) {
                    Fabric.getLogger().mo20969d(Fabric.TAG, "Asychronously getting Advertising Info and storing it to preferences");
                    AdvertisingInfoProvider.this.storeInfoToPreferences(access$000);
                }
            }
        }).start();
    }

    /* access modifiers changed from: private */
    @SuppressLint({"CommitPrefEdits"})
    public void storeInfoToPreferences(AdvertisingInfo advertisingInfo) {
        if (isInfoValid(advertisingInfo)) {
            this.preferenceStore.save(this.preferenceStore.edit().putString("advertising_id", advertisingInfo.advertisingId).putBoolean(PREFKEY_LIMIT_AD_TRACKING, advertisingInfo.limitAdTrackingEnabled));
        } else {
            this.preferenceStore.save(this.preferenceStore.edit().remove("advertising_id").remove(PREFKEY_LIMIT_AD_TRACKING));
        }
    }

    public AdvertisingInfo getAdvertisingInfo() {
        AdvertisingInfo infoFromPreferences = getInfoFromPreferences();
        if (isInfoValid(infoFromPreferences)) {
            Fabric.getLogger().mo20969d(Fabric.TAG, "Using AdvertisingInfo from Preference Store");
            refreshInfoIfNeededAsync(infoFromPreferences);
            return infoFromPreferences;
        }
        AdvertisingInfo advertisingInfoFromStrategies = getAdvertisingInfoFromStrategies();
        storeInfoToPreferences(advertisingInfoFromStrategies);
        return advertisingInfoFromStrategies;
    }

    /* access modifiers changed from: protected */
    public AdvertisingInfo getInfoFromPreferences() {
        return new AdvertisingInfo(this.preferenceStore.get().getString("advertising_id", ""), this.preferenceStore.get().getBoolean(PREFKEY_LIMIT_AD_TRACKING, false));
    }

    public AdvertisingInfoStrategy getReflectionStrategy() {
        return new AdvertisingInfoReflectionStrategy(this.context);
    }

    public AdvertisingInfoStrategy getServiceStrategy() {
        return new AdvertisingInfoServiceStrategy(this.context);
    }
}
