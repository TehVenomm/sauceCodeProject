package com.crashlytics.android.answers;

import android.content.Context;
import io.fabric.sdk.android.services.common.ApiKey;
import io.fabric.sdk.android.services.common.CommonUtils;
import io.fabric.sdk.android.services.events.EnabledEventsStrategy;
import io.fabric.sdk.android.services.events.FilesSender;
import io.fabric.sdk.android.services.network.HttpRequestFactory;
import io.fabric.sdk.android.services.settings.AnalyticsSettingsData;
import java.util.concurrent.ScheduledExecutorService;

class EnabledSessionAnalyticsManagerStrategy extends EnabledEventsStrategy<SessionEvent> implements SessionAnalyticsManagerStrategy<SessionEvent> {
    EventFilter eventFilter = new KeepAllEventFilter();
    FilesSender filesSender;
    private final HttpRequestFactory httpRequestFactory;

    public EnabledSessionAnalyticsManagerStrategy(Context context, ScheduledExecutorService scheduledExecutorService, SessionAnalyticsFilesManager sessionAnalyticsFilesManager, HttpRequestFactory httpRequestFactory) {
        super(context, scheduledExecutorService, sessionAnalyticsFilesManager);
        this.httpRequestFactory = httpRequestFactory;
    }

    public FilesSender getFilesSender() {
        return this.filesSender;
    }

    public void recordEvent(SessionEvent sessionEvent) {
        if (this.eventFilter.skipEvent(sessionEvent)) {
            CommonUtils.logControlled(Answers.getInstance().getContext(), "skipping filtered event " + sessionEvent);
        } else {
            super.recordEvent(sessionEvent);
        }
    }

    public void setAnalyticsSettingsData(AnalyticsSettingsData analyticsSettingsData, String str) {
        this.filesSender = AnswersRetryFilesSender.build(new SessionAnalyticsFilesSender(Answers.getInstance(), str, analyticsSettingsData.analyticsURL, this.httpRequestFactory, new ApiKey().getValue(this.context)));
        ((SessionAnalyticsFilesManager) this.filesManager).setAnalyticsSettingsData(analyticsSettingsData);
        configureRollover(analyticsSettingsData.flushIntervalSeconds);
        if (analyticsSettingsData.samplingRate > 1) {
            this.eventFilter = new SamplingEventFilter(analyticsSettingsData.samplingRate);
        }
    }
}
