package com.crashlytics.android.answers;

import android.app.Activity;
import android.content.Context;
import android.os.Looper;
import com.crashlytics.android.answers.BackgroundManager.Listener;
import java.util.concurrent.ScheduledExecutorService;
import p017io.fabric.sdk.android.ActivityLifecycleManager;
import p017io.fabric.sdk.android.Fabric;
import p017io.fabric.sdk.android.Kit;
import p017io.fabric.sdk.android.services.common.ExecutorUtils;
import p017io.fabric.sdk.android.services.common.IdManager;
import p017io.fabric.sdk.android.services.network.DefaultHttpRequestFactory;
import p017io.fabric.sdk.android.services.persistence.FileStoreImpl;
import p017io.fabric.sdk.android.services.settings.AnalyticsSettingsData;

class SessionAnalyticsManager implements Listener {
    static final String EXECUTOR_SERVICE = "Answers Events Handler";
    static final String ON_CRASH_ERROR_MSG = "onCrash called from main thread!!!";
    final BackgroundManager backgroundManager;
    final AnswersEventsHandler eventsHandler;
    private final long installedAt;
    final ActivityLifecycleManager lifecycleManager;
    final AnswersPreferenceManager preferenceManager;

    SessionAnalyticsManager(AnswersEventsHandler answersEventsHandler, ActivityLifecycleManager activityLifecycleManager, BackgroundManager backgroundManager2, AnswersPreferenceManager answersPreferenceManager, long j) {
        this.eventsHandler = answersEventsHandler;
        this.lifecycleManager = activityLifecycleManager;
        this.backgroundManager = backgroundManager2;
        this.preferenceManager = answersPreferenceManager;
        this.installedAt = j;
    }

    public static SessionAnalyticsManager build(Kit kit, Context context, IdManager idManager, String str, String str2, long j) {
        SessionMetadataCollector sessionMetadataCollector = new SessionMetadataCollector(context, idManager, str, str2);
        AnswersFilesManagerProvider answersFilesManagerProvider = new AnswersFilesManagerProvider(context, new FileStoreImpl(kit));
        DefaultHttpRequestFactory defaultHttpRequestFactory = new DefaultHttpRequestFactory(Fabric.getLogger());
        ActivityLifecycleManager activityLifecycleManager = new ActivityLifecycleManager(context);
        ScheduledExecutorService buildSingleThreadScheduledExecutorService = ExecutorUtils.buildSingleThreadScheduledExecutorService(EXECUTOR_SERVICE);
        BackgroundManager backgroundManager2 = new BackgroundManager(buildSingleThreadScheduledExecutorService);
        SessionAnalyticsManager sessionAnalyticsManager = new SessionAnalyticsManager(new AnswersEventsHandler(kit, context, answersFilesManagerProvider, sessionMetadataCollector, defaultHttpRequestFactory, buildSingleThreadScheduledExecutorService, new FirebaseAnalyticsApiAdapter(context)), activityLifecycleManager, backgroundManager2, AnswersPreferenceManager.build(context), j);
        return sessionAnalyticsManager;
    }

    public void disable() {
        this.lifecycleManager.resetCallbacks();
        this.eventsHandler.disable();
    }

    public void enable() {
        this.eventsHandler.enable();
        this.lifecycleManager.registerCallbacks(new AnswersLifecycleCallbacks(this, this.backgroundManager));
        this.backgroundManager.registerListener(this);
        if (isFirstLaunch()) {
            onInstall(this.installedAt);
            this.preferenceManager.setAnalyticsLaunched();
        }
    }

    /* access modifiers changed from: 0000 */
    public boolean isFirstLaunch() {
        return !this.preferenceManager.hasAnalyticsLaunched();
    }

    public void onBackground() {
        Fabric.getLogger().mo20969d(Answers.TAG, "Flush events when app is backgrounded");
        this.eventsHandler.flushEvents();
    }

    public void onCrash(String str, String str2) {
        if (Looper.myLooper() == Looper.getMainLooper()) {
            throw new IllegalStateException(ON_CRASH_ERROR_MSG);
        }
        Fabric.getLogger().mo20969d(Answers.TAG, "Logged crash");
        this.eventsHandler.processEventSync(SessionEvent.crashEventBuilder(str, str2));
    }

    public void onCustom(CustomEvent customEvent) {
        Fabric.getLogger().mo20969d(Answers.TAG, "Logged custom event: " + customEvent);
        this.eventsHandler.processEventAsync(SessionEvent.customEventBuilder(customEvent));
    }

    public void onError(String str) {
    }

    public void onInstall(long j) {
        Fabric.getLogger().mo20969d(Answers.TAG, "Logged install");
        this.eventsHandler.processEventAsyncAndFlush(SessionEvent.installEventBuilder(j));
    }

    public void onLifecycle(Activity activity, Type type) {
        Fabric.getLogger().mo20969d(Answers.TAG, "Logged lifecycle event: " + type.name());
        this.eventsHandler.processEventAsync(SessionEvent.lifecycleEventBuilder(type, activity));
    }

    public void onPredefined(PredefinedEvent predefinedEvent) {
        Fabric.getLogger().mo20969d(Answers.TAG, "Logged predefined event: " + predefinedEvent);
        this.eventsHandler.processEventAsync(SessionEvent.predefinedEventBuilder(predefinedEvent));
    }

    public void setAnalyticsSettingsData(AnalyticsSettingsData analyticsSettingsData, String str) {
        this.backgroundManager.setFlushOnBackground(analyticsSettingsData.flushOnBackground);
        this.eventsHandler.setAnalyticsSettingsData(analyticsSettingsData, str);
    }
}
