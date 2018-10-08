package com.crashlytics.android.answers;

import android.annotation.TargetApi;
import android.app.Activity;
import android.app.Application;
import android.app.Application.ActivityLifecycleCallbacks;
import android.os.Bundle;
import io.fabric.sdk.android.services.common.CommonUtils;
import io.fabric.sdk.android.services.common.ExecutorUtils;
import io.fabric.sdk.android.services.network.HttpRequestFactory;
import java.util.concurrent.ScheduledExecutorService;

@TargetApi(14)
class AutoSessionAnalyticsManager extends SessionAnalyticsManager {
    private static final String EXECUTOR_SERVICE = "Crashlytics Trace Manager";
    private final ActivityLifecycleCallbacks activityLifecycleCallbacks = new C03011();
    private final Application application;

    /* renamed from: com.crashlytics.android.answers.AutoSessionAnalyticsManager$1 */
    class C03011 implements ActivityLifecycleCallbacks {
        C03011() {
        }

        public void onActivityCreated(Activity activity, Bundle bundle) {
            AutoSessionAnalyticsManager.this.onCreate(activity);
        }

        public void onActivityDestroyed(Activity activity) {
            AutoSessionAnalyticsManager.this.onDestroy(activity);
        }

        public void onActivityPaused(Activity activity) {
            AutoSessionAnalyticsManager.this.onPause(activity);
        }

        public void onActivityResumed(Activity activity) {
            AutoSessionAnalyticsManager.this.onResume(activity);
        }

        public void onActivitySaveInstanceState(Activity activity, Bundle bundle) {
            AutoSessionAnalyticsManager.this.onSaveInstanceState(activity);
        }

        public void onActivityStarted(Activity activity) {
            AutoSessionAnalyticsManager.this.onStart(activity);
        }

        public void onActivityStopped(Activity activity) {
            AutoSessionAnalyticsManager.this.onStop(activity);
        }
    }

    AutoSessionAnalyticsManager(SessionEventMetadata sessionEventMetadata, SessionEventsHandler sessionEventsHandler, Application application) {
        super(sessionEventMetadata, sessionEventsHandler);
        this.application = application;
        CommonUtils.logControlled(Answers.getInstance().getContext(), "Registering activity lifecycle callbacks for session analytics.");
        application.registerActivityLifecycleCallbacks(this.activityLifecycleCallbacks);
    }

    public static AutoSessionAnalyticsManager build(Application application, SessionEventMetadata sessionEventMetadata, SessionAnalyticsFilesManager sessionAnalyticsFilesManager, HttpRequestFactory httpRequestFactory) {
        ScheduledExecutorService buildSingleThreadScheduledExecutorService = ExecutorUtils.buildSingleThreadScheduledExecutorService(EXECUTOR_SERVICE);
        return new AutoSessionAnalyticsManager(sessionEventMetadata, new SessionEventsHandler(application, new EnabledSessionAnalyticsManagerStrategy(application, buildSingleThreadScheduledExecutorService, sessionAnalyticsFilesManager, httpRequestFactory), sessionAnalyticsFilesManager, buildSingleThreadScheduledExecutorService), application);
    }

    public void disable() {
        CommonUtils.logControlled(Answers.getInstance().getContext(), "Unregistering activity lifecycle callbacks for session analytics");
        this.application.unregisterActivityLifecycleCallbacks(this.activityLifecycleCallbacks);
        super.disable();
    }
}
