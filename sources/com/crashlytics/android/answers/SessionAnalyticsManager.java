package com.crashlytics.android.answers;

import android.app.Activity;
import android.content.Context;
import android.os.Looper;
import io.fabric.sdk.android.services.common.ExecutorUtils;
import io.fabric.sdk.android.services.network.HttpRequestFactory;
import io.fabric.sdk.android.services.settings.AnalyticsSettingsData;
import java.util.Map;
import java.util.concurrent.ScheduledExecutorService;

class SessionAnalyticsManager {
    private static final String EXECUTOR_SERVICE = "Crashlytics SAM";
    static final String ON_CRASH_ERROR_MSG = "onCrash called from main thread!!!";
    boolean customEventsEnabled = true;
    final SessionEventsHandler eventsHandler;
    final SessionEventMetadata metadata;

    SessionAnalyticsManager(SessionEventMetadata sessionEventMetadata, SessionEventsHandler sessionEventsHandler) {
        this.metadata = sessionEventMetadata;
        this.eventsHandler = sessionEventsHandler;
    }

    public static SessionAnalyticsManager build(Context context, SessionEventMetadata sessionEventMetadata, SessionAnalyticsFilesManager sessionAnalyticsFilesManager, HttpRequestFactory httpRequestFactory) {
        ScheduledExecutorService buildSingleThreadScheduledExecutorService = ExecutorUtils.buildSingleThreadScheduledExecutorService(EXECUTOR_SERVICE);
        return new SessionAnalyticsManager(sessionEventMetadata, new SessionEventsHandler(context, new EnabledSessionAnalyticsManagerStrategy(context, buildSingleThreadScheduledExecutorService, sessionAnalyticsFilesManager, httpRequestFactory), sessionAnalyticsFilesManager, buildSingleThreadScheduledExecutorService));
    }

    public void disable() {
        this.eventsHandler.disable();
    }

    public void onCrash(String str) {
        if (Looper.myLooper() == Looper.getMainLooper()) {
            throw new IllegalStateException(ON_CRASH_ERROR_MSG);
        }
        this.eventsHandler.recordEventSync(SessionEvent.buildCrashEvent(this.metadata, str));
    }

    public void onCreate(Activity activity) {
        this.eventsHandler.recordEventAsync(SessionEvent.buildActivityLifecycleEvent(this.metadata, Type.CREATE, activity), false);
    }

    public void onCustom(String str, Map<String, Object> map) {
        if (this.customEventsEnabled) {
            this.eventsHandler.recordEventAsync(SessionEvent.buildCustomEvent(this.metadata, str, map), false);
        }
    }

    public void onDestroy(Activity activity) {
        this.eventsHandler.recordEventAsync(SessionEvent.buildActivityLifecycleEvent(this.metadata, Type.DESTROY, activity), false);
    }

    public void onError(String str) {
        this.eventsHandler.recordEventAsync(SessionEvent.buildErrorEvent(this.metadata, str), false);
    }

    public void onInstall() {
        this.eventsHandler.recordEventAsync(SessionEvent.buildInstallEvent(this.metadata), true);
    }

    public void onPause(Activity activity) {
        this.eventsHandler.recordEventAsync(SessionEvent.buildActivityLifecycleEvent(this.metadata, Type.PAUSE, activity), false);
    }

    public void onResume(Activity activity) {
        this.eventsHandler.recordEventAsync(SessionEvent.buildActivityLifecycleEvent(this.metadata, Type.RESUME, activity), false);
    }

    public void onSaveInstanceState(Activity activity) {
        this.eventsHandler.recordEventAsync(SessionEvent.buildActivityLifecycleEvent(this.metadata, Type.SAVE_INSTANCE_STATE, activity), false);
    }

    public void onStart(Activity activity) {
        this.eventsHandler.recordEventAsync(SessionEvent.buildActivityLifecycleEvent(this.metadata, Type.START, activity), false);
    }

    public void onStop(Activity activity) {
        this.eventsHandler.recordEventAsync(SessionEvent.buildActivityLifecycleEvent(this.metadata, Type.STOP, activity), false);
    }

    public void setAnalyticsSettingsData(AnalyticsSettingsData analyticsSettingsData, String str) {
        this.customEventsEnabled = analyticsSettingsData.trackCustomEvents;
        this.eventsHandler.setAnalyticsSettingsData(analyticsSettingsData, str);
    }
}
