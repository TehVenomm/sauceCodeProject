package com.crashlytics.android.core;

import android.annotation.SuppressLint;
import p017io.fabric.sdk.android.services.persistence.PreferenceStore;
import p017io.fabric.sdk.android.services.persistence.PreferenceStoreImpl;

@SuppressLint({"CommitPrefEdits"})
class PreferenceManager {
    static final String PREF_ALWAYS_SEND_REPORTS_KEY = "always_send_reports_opt_in";
    private static final String PREF_MIGRATION_COMPLETE = "preferences_migration_complete";
    private static final boolean SHOULD_ALWAYS_SEND_REPORTS_DEFAULT = false;
    private final CrashlyticsCore kit;
    private final PreferenceStore preferenceStore;

    public PreferenceManager(PreferenceStore preferenceStore2, CrashlyticsCore crashlyticsCore) {
        this.preferenceStore = preferenceStore2;
        this.kit = crashlyticsCore;
    }

    public static PreferenceManager create(PreferenceStore preferenceStore2, CrashlyticsCore crashlyticsCore) {
        return new PreferenceManager(preferenceStore2, crashlyticsCore);
    }

    /* access modifiers changed from: 0000 */
    public void setShouldAlwaysSendReports(boolean z) {
        this.preferenceStore.save(this.preferenceStore.edit().putBoolean(PREF_ALWAYS_SEND_REPORTS_KEY, z));
    }

    /* access modifiers changed from: 0000 */
    public boolean shouldAlwaysSendReports() {
        if (!this.preferenceStore.get().contains(PREF_MIGRATION_COMPLETE)) {
            PreferenceStoreImpl preferenceStoreImpl = new PreferenceStoreImpl(this.kit);
            if (!this.preferenceStore.get().contains(PREF_ALWAYS_SEND_REPORTS_KEY) && preferenceStoreImpl.get().contains(PREF_ALWAYS_SEND_REPORTS_KEY)) {
                this.preferenceStore.save(this.preferenceStore.edit().putBoolean(PREF_ALWAYS_SEND_REPORTS_KEY, preferenceStoreImpl.get().getBoolean(PREF_ALWAYS_SEND_REPORTS_KEY, false)));
            }
            this.preferenceStore.save(this.preferenceStore.edit().putBoolean(PREF_MIGRATION_COMPLETE, true));
        }
        return this.preferenceStore.get().getBoolean(PREF_ALWAYS_SEND_REPORTS_KEY, false);
    }
}
