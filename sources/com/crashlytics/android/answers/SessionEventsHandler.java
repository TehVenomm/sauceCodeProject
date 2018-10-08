package com.crashlytics.android.answers;

import android.content.Context;
import io.fabric.sdk.android.services.common.CommonUtils;
import io.fabric.sdk.android.services.events.EventsHandler;
import io.fabric.sdk.android.services.events.EventsStrategy;
import io.fabric.sdk.android.services.settings.AnalyticsSettingsData;
import java.util.concurrent.ScheduledExecutorService;

class SessionEventsHandler extends EventsHandler<SessionEvent> {
    SessionEventsHandler(Context context, EventsStrategy<SessionEvent> eventsStrategy, SessionAnalyticsFilesManager sessionAnalyticsFilesManager, ScheduledExecutorService scheduledExecutorService) {
        super(context, eventsStrategy, sessionAnalyticsFilesManager, scheduledExecutorService);
    }

    protected EventsStrategy<SessionEvent> getDisabledEventsStrategy() {
        return new DisabledSessionAnalyticsManagerStrategy();
    }

    protected void setAnalyticsSettingsData(final AnalyticsSettingsData analyticsSettingsData, final String str) {
        super.executeAsync(new Runnable() {
            public void run() {
                try {
                    ((SessionAnalyticsManagerStrategy) SessionEventsHandler.this.strategy).setAnalyticsSettingsData(analyticsSettingsData, str);
                } catch (Throwable e) {
                    CommonUtils.logControlledError(Answers.getInstance().getContext(), "Crashlytics failed to set analytics settings data.", e);
                }
            }
        });
    }
}
