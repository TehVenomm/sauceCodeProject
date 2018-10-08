package com.crashlytics.android.beta;

import android.annotation.SuppressLint;
import android.content.Context;
import io.fabric.sdk.android.Fabric;
import io.fabric.sdk.android.services.common.ApiKey;
import io.fabric.sdk.android.services.common.CurrentTimeProvider;
import io.fabric.sdk.android.services.common.IdManager;
import io.fabric.sdk.android.services.network.HttpRequestFactory;
import io.fabric.sdk.android.services.persistence.PreferenceStore;
import io.fabric.sdk.android.services.settings.BetaSettingsData;
import java.util.concurrent.atomic.AtomicBoolean;

abstract class AbstractCheckForUpdatesController implements UpdatesController {
    static final long LAST_UPDATE_CHECK_DEFAULT = 0;
    static final String LAST_UPDATE_CHECK_KEY = "last_update_check";
    private static final long MILLIS_PER_SECOND = 1000;
    private Beta beta;
    private BetaSettingsData betaSettings;
    private BuildProperties buildProps;
    private Context context;
    private CurrentTimeProvider currentTimeProvider;
    private final AtomicBoolean externallyReady;
    private HttpRequestFactory httpRequestFactory;
    private IdManager idManager;
    private final AtomicBoolean initialized;
    private long lastCheckTimeMillis;
    private PreferenceStore preferenceStore;

    public AbstractCheckForUpdatesController() {
        this(false);
    }

    public AbstractCheckForUpdatesController(boolean z) {
        this.initialized = new AtomicBoolean();
        this.lastCheckTimeMillis = 0;
        this.externallyReady = new AtomicBoolean(z);
    }

    private void performUpdateCheck() {
        Fabric.getLogger().mo4289d(Beta.TAG, "Performing update check");
        String value = new ApiKey().getValue(this.context);
        new CheckForUpdatesRequest(this.beta, this.beta.getOverridenSpiEndpoint(), this.betaSettings.updateUrl, this.httpRequestFactory, new CheckForUpdatesResponseTransform()).invoke(value, this.idManager.createIdHeaderValue(value, this.buildProps.packageName), this.buildProps);
    }

    @SuppressLint({"CommitPrefEdits"})
    protected void checkForUpdates() {
        synchronized (this.preferenceStore) {
            if (this.preferenceStore.get().contains(LAST_UPDATE_CHECK_KEY)) {
                this.preferenceStore.save(this.preferenceStore.edit().remove(LAST_UPDATE_CHECK_KEY));
            }
        }
        long currentTimeMillis = this.currentTimeProvider.getCurrentTimeMillis();
        long j = ((long) this.betaSettings.updateSuspendDurationSeconds) * 1000;
        Fabric.getLogger().mo4289d(Beta.TAG, "Check for updates delay: " + j);
        Fabric.getLogger().mo4289d(Beta.TAG, "Check for updates last check time: " + getLastCheckTimeMillis());
        j += getLastCheckTimeMillis();
        Fabric.getLogger().mo4289d(Beta.TAG, "Check for updates current time: " + currentTimeMillis + ", next check time: " + j);
        if (currentTimeMillis >= j) {
            try {
                performUpdateCheck();
            } finally {
                setLastCheckTimeMillis(currentTimeMillis);
            }
        } else {
            Fabric.getLogger().mo4289d(Beta.TAG, "Check for updates next check time was not passed");
        }
    }

    long getLastCheckTimeMillis() {
        return this.lastCheckTimeMillis;
    }

    public void initialize(Context context, Beta beta, IdManager idManager, BetaSettingsData betaSettingsData, BuildProperties buildProperties, PreferenceStore preferenceStore, CurrentTimeProvider currentTimeProvider, HttpRequestFactory httpRequestFactory) {
        this.context = context;
        this.beta = beta;
        this.idManager = idManager;
        this.betaSettings = betaSettingsData;
        this.buildProps = buildProperties;
        this.preferenceStore = preferenceStore;
        this.currentTimeProvider = currentTimeProvider;
        this.httpRequestFactory = httpRequestFactory;
        if (signalInitialized()) {
            checkForUpdates();
        }
    }

    void setLastCheckTimeMillis(long j) {
        this.lastCheckTimeMillis = j;
    }

    protected boolean signalExternallyReady() {
        this.externallyReady.set(true);
        return this.initialized.get();
    }

    boolean signalInitialized() {
        this.initialized.set(true);
        return this.externallyReady.get();
    }
}
