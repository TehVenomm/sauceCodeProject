package com.crashlytics.android.answers;

import android.content.Context;
import java.io.IOException;
import java.util.List;
import java.util.Locale;
import java.util.concurrent.RejectedExecutionException;
import java.util.concurrent.ScheduledExecutorService;
import java.util.concurrent.ScheduledFuture;
import java.util.concurrent.TimeUnit;
import java.util.concurrent.atomic.AtomicReference;
import net.gogame.gowrap.integrations.AbstractIntegrationSupport;
import p017io.fabric.sdk.android.Fabric;
import p017io.fabric.sdk.android.Kit;
import p017io.fabric.sdk.android.services.common.ApiKey;
import p017io.fabric.sdk.android.services.common.CommonUtils;
import p017io.fabric.sdk.android.services.events.FilesSender;
import p017io.fabric.sdk.android.services.events.TimeBasedFileRollOverRunnable;
import p017io.fabric.sdk.android.services.network.HttpRequestFactory;
import p017io.fabric.sdk.android.services.settings.AnalyticsSettingsData;

class EnabledSessionAnalyticsManagerStrategy implements SessionAnalyticsManagerStrategy {
    static final int UNDEFINED_ROLLOVER_INTERVAL_SECONDS = -1;
    ApiKey apiKey = new ApiKey();
    private final Context context;
    boolean customEventsEnabled = true;
    EventFilter eventFilter = new KeepAllEventFilter();
    private final ScheduledExecutorService executorService;
    private final SessionAnalyticsFilesManager filesManager;
    FilesSender filesSender;
    private final FirebaseAnalyticsApiAdapter firebaseAnalyticsApiAdapter;
    boolean forwardToFirebaseAnalyticsEnabled = false;
    private final HttpRequestFactory httpRequestFactory;
    boolean includePurchaseEventsInForwardedEvents = false;
    private final Kit kit;
    final SessionEventMetadata metadata;
    boolean predefinedEventsEnabled = true;
    private final AtomicReference<ScheduledFuture<?>> rolloverFutureRef = new AtomicReference<>();
    volatile int rolloverIntervalSeconds = -1;

    public EnabledSessionAnalyticsManagerStrategy(Kit kit2, Context context2, ScheduledExecutorService scheduledExecutorService, SessionAnalyticsFilesManager sessionAnalyticsFilesManager, HttpRequestFactory httpRequestFactory2, SessionEventMetadata sessionEventMetadata, FirebaseAnalyticsApiAdapter firebaseAnalyticsApiAdapter2) {
        this.kit = kit2;
        this.context = context2;
        this.executorService = scheduledExecutorService;
        this.filesManager = sessionAnalyticsFilesManager;
        this.httpRequestFactory = httpRequestFactory2;
        this.metadata = sessionEventMetadata;
        this.firebaseAnalyticsApiAdapter = firebaseAnalyticsApiAdapter2;
    }

    public void cancelTimeBasedFileRollOver() {
        if (this.rolloverFutureRef.get() != null) {
            CommonUtils.logControlled(this.context, "Cancelling time-based rollover because no events are currently being generated.");
            ((ScheduledFuture) this.rolloverFutureRef.get()).cancel(false);
            this.rolloverFutureRef.set(null);
        }
    }

    public void deleteAllEvents() {
        this.filesManager.deleteAllEventsFiles();
    }

    public void processEvent(Builder builder) {
        SessionEvent build = builder.build(this.metadata);
        if (!this.customEventsEnabled && Type.CUSTOM.equals(build.type)) {
            Fabric.getLogger().mo20969d(Answers.TAG, "Custom events tracking disabled - skipping event: " + build);
        } else if (!this.predefinedEventsEnabled && Type.PREDEFINED.equals(build.type)) {
            Fabric.getLogger().mo20969d(Answers.TAG, "Predefined events tracking disabled - skipping event: " + build);
        } else if (this.eventFilter.skipEvent(build)) {
            Fabric.getLogger().mo20969d(Answers.TAG, "Skipping filtered event: " + build);
        } else {
            try {
                this.filesManager.writeEvent(build);
            } catch (IOException e) {
                Fabric.getLogger().mo20972e(Answers.TAG, "Failed to write event: " + build, e);
            }
            scheduleTimeBasedRollOverIfNeeded();
            boolean z = Type.CUSTOM.equals(build.type) || Type.PREDEFINED.equals(build.type);
            boolean equals = AbstractIntegrationSupport.DEFAULT_PURCHASE_CATEGORY.equals(build.predefinedType);
            if (this.forwardToFirebaseAnalyticsEnabled && z) {
                if (!equals || this.includePurchaseEventsInForwardedEvents) {
                    try {
                        this.firebaseAnalyticsApiAdapter.processEvent(build);
                    } catch (Exception e2) {
                        Fabric.getLogger().mo20972e(Answers.TAG, "Failed to map event to Firebase: " + build, e2);
                    }
                }
            }
        }
    }

    public boolean rollFileOver() {
        try {
            return this.filesManager.rollFileOver();
        } catch (IOException e) {
            CommonUtils.logControlledError(this.context, "Failed to roll file over.", e);
            return false;
        }
    }

    /* access modifiers changed from: 0000 */
    public void scheduleTimeBasedFileRollOver(long j, long j2) {
        if (this.rolloverFutureRef.get() == null) {
            TimeBasedFileRollOverRunnable timeBasedFileRollOverRunnable = new TimeBasedFileRollOverRunnable(this.context, this);
            CommonUtils.logControlled(this.context, "Scheduling time based file roll over every " + j2 + " seconds");
            try {
                this.rolloverFutureRef.set(this.executorService.scheduleAtFixedRate(timeBasedFileRollOverRunnable, j, j2, TimeUnit.SECONDS));
            } catch (RejectedExecutionException e) {
                CommonUtils.logControlledError(this.context, "Failed to schedule time based file roll over", e);
            }
        }
    }

    public void scheduleTimeBasedRollOverIfNeeded() {
        if (this.rolloverIntervalSeconds != -1) {
            scheduleTimeBasedFileRollOver((long) this.rolloverIntervalSeconds, (long) this.rolloverIntervalSeconds);
        }
    }

    public void sendEvents() {
        int i = 0;
        if (this.filesSender == null) {
            CommonUtils.logControlled(this.context, "skipping files send because we don't yet know the target endpoint");
            return;
        }
        CommonUtils.logControlled(this.context, "Sending all files");
        List batchOfFilesToSend = this.filesManager.getBatchOfFilesToSend();
        while (batchOfFilesToSend.size() > 0) {
            try {
                CommonUtils.logControlled(this.context, String.format(Locale.US, "attempt to send batch of %d files", new Object[]{Integer.valueOf(batchOfFilesToSend.size())}));
                boolean send = this.filesSender.send(batchOfFilesToSend);
                if (send) {
                    i += batchOfFilesToSend.size();
                    this.filesManager.deleteSentFiles(batchOfFilesToSend);
                }
                if (!send) {
                    break;
                }
                batchOfFilesToSend = this.filesManager.getBatchOfFilesToSend();
            } catch (Exception e) {
                CommonUtils.logControlledError(this.context, "Failed to send batch of analytics files to server: " + e.getMessage(), e);
            }
        }
        if (i == 0) {
            this.filesManager.deleteOldestInRollOverIfOverMax();
        }
    }

    public void setAnalyticsSettingsData(AnalyticsSettingsData analyticsSettingsData, String str) {
        this.filesSender = AnswersRetryFilesSender.build(new SessionAnalyticsFilesSender(this.kit, str, analyticsSettingsData.analyticsURL, this.httpRequestFactory, this.apiKey.getValue(this.context)));
        this.filesManager.setAnalyticsSettingsData(analyticsSettingsData);
        this.forwardToFirebaseAnalyticsEnabled = analyticsSettingsData.forwardToFirebaseAnalytics;
        this.includePurchaseEventsInForwardedEvents = analyticsSettingsData.includePurchaseEventsInForwardedEvents;
        Fabric.getLogger().mo20969d(Answers.TAG, "Firebase analytics forwarding " + (this.forwardToFirebaseAnalyticsEnabled ? "enabled" : "disabled"));
        Fabric.getLogger().mo20969d(Answers.TAG, "Firebase analytics including purchase events " + (this.includePurchaseEventsInForwardedEvents ? "enabled" : "disabled"));
        this.customEventsEnabled = analyticsSettingsData.trackCustomEvents;
        Fabric.getLogger().mo20969d(Answers.TAG, "Custom event tracking " + (this.customEventsEnabled ? "enabled" : "disabled"));
        this.predefinedEventsEnabled = analyticsSettingsData.trackPredefinedEvents;
        Fabric.getLogger().mo20969d(Answers.TAG, "Predefined event tracking " + (this.predefinedEventsEnabled ? "enabled" : "disabled"));
        if (analyticsSettingsData.samplingRate > 1) {
            Fabric.getLogger().mo20969d(Answers.TAG, "Event sampling enabled");
            this.eventFilter = new SamplingEventFilter(analyticsSettingsData.samplingRate);
        }
        this.rolloverIntervalSeconds = analyticsSettingsData.flushIntervalSeconds;
        scheduleTimeBasedFileRollOver(0, (long) this.rolloverIntervalSeconds);
    }
}
