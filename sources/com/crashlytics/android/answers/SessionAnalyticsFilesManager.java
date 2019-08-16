package com.crashlytics.android.answers;

import android.content.Context;
import java.io.IOException;
import java.util.UUID;
import p017io.fabric.sdk.android.services.common.CurrentTimeProvider;
import p017io.fabric.sdk.android.services.events.EventsFilesManager;
import p017io.fabric.sdk.android.services.events.EventsStorage;
import p017io.fabric.sdk.android.services.settings.AnalyticsSettingsData;

class SessionAnalyticsFilesManager extends EventsFilesManager<SessionEvent> {
    private static final String SESSION_ANALYTICS_TO_SEND_FILE_EXTENSION = ".tap";
    private static final String SESSION_ANALYTICS_TO_SEND_FILE_PREFIX = "sa";
    private AnalyticsSettingsData analyticsSettingsData;

    SessionAnalyticsFilesManager(Context context, SessionEventTransform sessionEventTransform, CurrentTimeProvider currentTimeProvider, EventsStorage eventsStorage) throws IOException {
        super(context, sessionEventTransform, currentTimeProvider, eventsStorage, 100);
    }

    /* access modifiers changed from: protected */
    public String generateUniqueRollOverFileName() {
        return SESSION_ANALYTICS_TO_SEND_FILE_PREFIX + EventsFilesManager.ROLL_OVER_FILE_NAME_SEPARATOR + UUID.randomUUID().toString() + EventsFilesManager.ROLL_OVER_FILE_NAME_SEPARATOR + this.currentTimeProvider.getCurrentTimeMillis() + SESSION_ANALYTICS_TO_SEND_FILE_EXTENSION;
    }

    /* access modifiers changed from: protected */
    public int getMaxByteSizePerFile() {
        return this.analyticsSettingsData == null ? super.getMaxByteSizePerFile() : this.analyticsSettingsData.maxByteSizePerFile;
    }

    /* access modifiers changed from: protected */
    public int getMaxFilesToKeep() {
        return this.analyticsSettingsData == null ? super.getMaxFilesToKeep() : this.analyticsSettingsData.maxPendingSendFileCount;
    }

    /* access modifiers changed from: 0000 */
    public void setAnalyticsSettingsData(AnalyticsSettingsData analyticsSettingsData2) {
        this.analyticsSettingsData = analyticsSettingsData2;
    }
}
