.class Lcom/crashlytics/android/answers/SessionAnalyticsManager;
.super Ljava/lang/Object;
.source "SessionAnalyticsManager.java"


# static fields
.field private static final EXECUTOR_SERVICE:Ljava/lang/String; = "Crashlytics SAM"

.field static final ON_CRASH_ERROR_MSG:Ljava/lang/String; = "onCrash called from main thread!!!"


# instance fields
.field customEventsEnabled:Z

.field final eventsHandler:Lcom/crashlytics/android/answers/SessionEventsHandler;

.field final metadata:Lcom/crashlytics/android/answers/SessionEventMetadata;


# direct methods
.method constructor <init>(Lcom/crashlytics/android/answers/SessionEventMetadata;Lcom/crashlytics/android/answers/SessionEventsHandler;)V
    .locals 1

    .line 41
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    const/4 v0, 0x1

    .line 28
    iput-boolean v0, p0, Lcom/crashlytics/android/answers/SessionAnalyticsManager;->customEventsEnabled:Z

    .line 42
    iput-object p1, p0, Lcom/crashlytics/android/answers/SessionAnalyticsManager;->metadata:Lcom/crashlytics/android/answers/SessionEventMetadata;

    .line 43
    iput-object p2, p0, Lcom/crashlytics/android/answers/SessionAnalyticsManager;->eventsHandler:Lcom/crashlytics/android/answers/SessionEventsHandler;

    return-void
.end method

.method public static build(Landroid/content/Context;Lcom/crashlytics/android/answers/SessionEventMetadata;Lcom/crashlytics/android/answers/SessionAnalyticsFilesManager;Lio/fabric/sdk/android/services/network/HttpRequestFactory;)Lcom/crashlytics/android/answers/SessionAnalyticsManager;
    .locals 2

    const-string v0, "Crashlytics SAM"

    .line 32
    invoke-static {v0}, Lio/fabric/sdk/android/services/common/ExecutorUtils;->buildSingleThreadScheduledExecutorService(Ljava/lang/String;)Ljava/util/concurrent/ScheduledExecutorService;

    move-result-object v0

    .line 34
    new-instance v1, Lcom/crashlytics/android/answers/EnabledSessionAnalyticsManagerStrategy;

    invoke-direct {v1, p0, v0, p2, p3}, Lcom/crashlytics/android/answers/EnabledSessionAnalyticsManagerStrategy;-><init>(Landroid/content/Context;Ljava/util/concurrent/ScheduledExecutorService;Lcom/crashlytics/android/answers/SessionAnalyticsFilesManager;Lio/fabric/sdk/android/services/network/HttpRequestFactory;)V

    .line 36
    new-instance p3, Lcom/crashlytics/android/answers/SessionEventsHandler;

    invoke-direct {p3, p0, v1, p2, v0}, Lcom/crashlytics/android/answers/SessionEventsHandler;-><init>(Landroid/content/Context;Lio/fabric/sdk/android/services/events/EventsStrategy;Lcom/crashlytics/android/answers/SessionAnalyticsFilesManager;Ljava/util/concurrent/ScheduledExecutorService;)V

    .line 38
    new-instance p0, Lcom/crashlytics/android/answers/SessionAnalyticsManager;

    invoke-direct {p0, p1, p3}, Lcom/crashlytics/android/answers/SessionAnalyticsManager;-><init>(Lcom/crashlytics/android/answers/SessionEventMetadata;Lcom/crashlytics/android/answers/SessionEventsHandler;)V

    return-object p0
.end method


# virtual methods
.method public disable()V
    .locals 1

    .line 120
    iget-object v0, p0, Lcom/crashlytics/android/answers/SessionAnalyticsManager;->eventsHandler:Lcom/crashlytics/android/answers/SessionEventsHandler;

    invoke-virtual {v0}, Lcom/crashlytics/android/answers/SessionEventsHandler;->disable()V

    return-void
.end method

.method public onCrash(Ljava/lang/String;)V
    .locals 2

    .line 60
    invoke-static {}, Landroid/os/Looper;->myLooper()Landroid/os/Looper;

    move-result-object v0

    invoke-static {}, Landroid/os/Looper;->getMainLooper()Landroid/os/Looper;

    move-result-object v1

    if-eq v0, v1, :cond_0

    .line 67
    iget-object v0, p0, Lcom/crashlytics/android/answers/SessionAnalyticsManager;->eventsHandler:Lcom/crashlytics/android/answers/SessionEventsHandler;

    iget-object v1, p0, Lcom/crashlytics/android/answers/SessionAnalyticsManager;->metadata:Lcom/crashlytics/android/answers/SessionEventMetadata;

    invoke-static {v1, p1}, Lcom/crashlytics/android/answers/SessionEvent;->buildCrashEvent(Lcom/crashlytics/android/answers/SessionEventMetadata;Ljava/lang/String;)Lcom/crashlytics/android/answers/SessionEvent;

    move-result-object p1

    invoke-virtual {v0, p1}, Lcom/crashlytics/android/answers/SessionEventsHandler;->recordEventSync(Ljava/lang/Object;)V

    return-void

    .line 61
    :cond_0
    new-instance p1, Ljava/lang/IllegalStateException;

    const-string v0, "onCrash called from main thread!!!"

    invoke-direct {p1, v0}, Ljava/lang/IllegalStateException;-><init>(Ljava/lang/String;)V

    throw p1
.end method

.method public onCreate(Landroid/app/Activity;)V
    .locals 3

    .line 79
    iget-object v0, p0, Lcom/crashlytics/android/answers/SessionAnalyticsManager;->eventsHandler:Lcom/crashlytics/android/answers/SessionEventsHandler;

    iget-object v1, p0, Lcom/crashlytics/android/answers/SessionAnalyticsManager;->metadata:Lcom/crashlytics/android/answers/SessionEventMetadata;

    sget-object v2, Lcom/crashlytics/android/answers/SessionEvent$Type;->CREATE:Lcom/crashlytics/android/answers/SessionEvent$Type;

    invoke-static {v1, v2, p1}, Lcom/crashlytics/android/answers/SessionEvent;->buildActivityLifecycleEvent(Lcom/crashlytics/android/answers/SessionEventMetadata;Lcom/crashlytics/android/answers/SessionEvent$Type;Landroid/app/Activity;)Lcom/crashlytics/android/answers/SessionEvent;

    move-result-object p1

    const/4 v1, 0x0

    invoke-virtual {v0, p1, v1}, Lcom/crashlytics/android/answers/SessionEventsHandler;->recordEventAsync(Ljava/lang/Object;Z)V

    return-void
.end method

.method public onCustom(Ljava/lang/String;Ljava/util/Map;)V
    .locals 2
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/lang/String;",
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Ljava/lang/Object;",
            ">;)V"
        }
    .end annotation

    .line 47
    iget-boolean v0, p0, Lcom/crashlytics/android/answers/SessionAnalyticsManager;->customEventsEnabled:Z

    if-eqz v0, :cond_0

    .line 48
    iget-object v0, p0, Lcom/crashlytics/android/answers/SessionAnalyticsManager;->eventsHandler:Lcom/crashlytics/android/answers/SessionEventsHandler;

    iget-object v1, p0, Lcom/crashlytics/android/answers/SessionAnalyticsManager;->metadata:Lcom/crashlytics/android/answers/SessionEventMetadata;

    invoke-static {v1, p1, p2}, Lcom/crashlytics/android/answers/SessionEvent;->buildCustomEvent(Lcom/crashlytics/android/answers/SessionEventMetadata;Ljava/lang/String;Ljava/util/Map;)Lcom/crashlytics/android/answers/SessionEvent;

    move-result-object p1

    const/4 p2, 0x0

    invoke-virtual {v0, p1, p2}, Lcom/crashlytics/android/answers/SessionEventsHandler;->recordEventAsync(Ljava/lang/Object;Z)V

    :cond_0
    return-void
.end method

.method public onDestroy(Landroid/app/Activity;)V
    .locals 3

    .line 84
    iget-object v0, p0, Lcom/crashlytics/android/answers/SessionAnalyticsManager;->eventsHandler:Lcom/crashlytics/android/answers/SessionEventsHandler;

    iget-object v1, p0, Lcom/crashlytics/android/answers/SessionAnalyticsManager;->metadata:Lcom/crashlytics/android/answers/SessionEventMetadata;

    sget-object v2, Lcom/crashlytics/android/answers/SessionEvent$Type;->DESTROY:Lcom/crashlytics/android/answers/SessionEvent$Type;

    invoke-static {v1, v2, p1}, Lcom/crashlytics/android/answers/SessionEvent;->buildActivityLifecycleEvent(Lcom/crashlytics/android/answers/SessionEventMetadata;Lcom/crashlytics/android/answers/SessionEvent$Type;Landroid/app/Activity;)Lcom/crashlytics/android/answers/SessionEvent;

    move-result-object p1

    const/4 v1, 0x0

    invoke-virtual {v0, p1, v1}, Lcom/crashlytics/android/answers/SessionEventsHandler;->recordEventAsync(Ljava/lang/Object;Z)V

    return-void
.end method

.method public onError(Ljava/lang/String;)V
    .locals 2

    .line 71
    iget-object v0, p0, Lcom/crashlytics/android/answers/SessionAnalyticsManager;->eventsHandler:Lcom/crashlytics/android/answers/SessionEventsHandler;

    iget-object v1, p0, Lcom/crashlytics/android/answers/SessionAnalyticsManager;->metadata:Lcom/crashlytics/android/answers/SessionEventMetadata;

    invoke-static {v1, p1}, Lcom/crashlytics/android/answers/SessionEvent;->buildErrorEvent(Lcom/crashlytics/android/answers/SessionEventMetadata;Ljava/lang/String;)Lcom/crashlytics/android/answers/SessionEvent;

    move-result-object p1

    const/4 v1, 0x0

    invoke-virtual {v0, p1, v1}, Lcom/crashlytics/android/answers/SessionEventsHandler;->recordEventAsync(Ljava/lang/Object;Z)V

    return-void
.end method

.method public onInstall()V
    .locals 3

    .line 75
    iget-object v0, p0, Lcom/crashlytics/android/answers/SessionAnalyticsManager;->eventsHandler:Lcom/crashlytics/android/answers/SessionEventsHandler;

    iget-object v1, p0, Lcom/crashlytics/android/answers/SessionAnalyticsManager;->metadata:Lcom/crashlytics/android/answers/SessionEventMetadata;

    invoke-static {v1}, Lcom/crashlytics/android/answers/SessionEvent;->buildInstallEvent(Lcom/crashlytics/android/answers/SessionEventMetadata;)Lcom/crashlytics/android/answers/SessionEvent;

    move-result-object v1

    const/4 v2, 0x1

    invoke-virtual {v0, v1, v2}, Lcom/crashlytics/android/answers/SessionEventsHandler;->recordEventAsync(Ljava/lang/Object;Z)V

    return-void
.end method

.method public onPause(Landroid/app/Activity;)V
    .locals 3

    .line 89
    iget-object v0, p0, Lcom/crashlytics/android/answers/SessionAnalyticsManager;->eventsHandler:Lcom/crashlytics/android/answers/SessionEventsHandler;

    iget-object v1, p0, Lcom/crashlytics/android/answers/SessionAnalyticsManager;->metadata:Lcom/crashlytics/android/answers/SessionEventMetadata;

    sget-object v2, Lcom/crashlytics/android/answers/SessionEvent$Type;->PAUSE:Lcom/crashlytics/android/answers/SessionEvent$Type;

    invoke-static {v1, v2, p1}, Lcom/crashlytics/android/answers/SessionEvent;->buildActivityLifecycleEvent(Lcom/crashlytics/android/answers/SessionEventMetadata;Lcom/crashlytics/android/answers/SessionEvent$Type;Landroid/app/Activity;)Lcom/crashlytics/android/answers/SessionEvent;

    move-result-object p1

    const/4 v1, 0x0

    invoke-virtual {v0, p1, v1}, Lcom/crashlytics/android/answers/SessionEventsHandler;->recordEventAsync(Ljava/lang/Object;Z)V

    return-void
.end method

.method public onResume(Landroid/app/Activity;)V
    .locals 3

    .line 94
    iget-object v0, p0, Lcom/crashlytics/android/answers/SessionAnalyticsManager;->eventsHandler:Lcom/crashlytics/android/answers/SessionEventsHandler;

    iget-object v1, p0, Lcom/crashlytics/android/answers/SessionAnalyticsManager;->metadata:Lcom/crashlytics/android/answers/SessionEventMetadata;

    sget-object v2, Lcom/crashlytics/android/answers/SessionEvent$Type;->RESUME:Lcom/crashlytics/android/answers/SessionEvent$Type;

    invoke-static {v1, v2, p1}, Lcom/crashlytics/android/answers/SessionEvent;->buildActivityLifecycleEvent(Lcom/crashlytics/android/answers/SessionEventMetadata;Lcom/crashlytics/android/answers/SessionEvent$Type;Landroid/app/Activity;)Lcom/crashlytics/android/answers/SessionEvent;

    move-result-object p1

    const/4 v1, 0x0

    invoke-virtual {v0, p1, v1}, Lcom/crashlytics/android/answers/SessionEventsHandler;->recordEventAsync(Ljava/lang/Object;Z)V

    return-void
.end method

.method public onSaveInstanceState(Landroid/app/Activity;)V
    .locals 3

    .line 99
    iget-object v0, p0, Lcom/crashlytics/android/answers/SessionAnalyticsManager;->eventsHandler:Lcom/crashlytics/android/answers/SessionEventsHandler;

    iget-object v1, p0, Lcom/crashlytics/android/answers/SessionAnalyticsManager;->metadata:Lcom/crashlytics/android/answers/SessionEventMetadata;

    sget-object v2, Lcom/crashlytics/android/answers/SessionEvent$Type;->SAVE_INSTANCE_STATE:Lcom/crashlytics/android/answers/SessionEvent$Type;

    invoke-static {v1, v2, p1}, Lcom/crashlytics/android/answers/SessionEvent;->buildActivityLifecycleEvent(Lcom/crashlytics/android/answers/SessionEventMetadata;Lcom/crashlytics/android/answers/SessionEvent$Type;Landroid/app/Activity;)Lcom/crashlytics/android/answers/SessionEvent;

    move-result-object p1

    const/4 v1, 0x0

    invoke-virtual {v0, p1, v1}, Lcom/crashlytics/android/answers/SessionEventsHandler;->recordEventAsync(Ljava/lang/Object;Z)V

    return-void
.end method

.method public onStart(Landroid/app/Activity;)V
    .locals 3

    .line 104
    iget-object v0, p0, Lcom/crashlytics/android/answers/SessionAnalyticsManager;->eventsHandler:Lcom/crashlytics/android/answers/SessionEventsHandler;

    iget-object v1, p0, Lcom/crashlytics/android/answers/SessionAnalyticsManager;->metadata:Lcom/crashlytics/android/answers/SessionEventMetadata;

    sget-object v2, Lcom/crashlytics/android/answers/SessionEvent$Type;->START:Lcom/crashlytics/android/answers/SessionEvent$Type;

    invoke-static {v1, v2, p1}, Lcom/crashlytics/android/answers/SessionEvent;->buildActivityLifecycleEvent(Lcom/crashlytics/android/answers/SessionEventMetadata;Lcom/crashlytics/android/answers/SessionEvent$Type;Landroid/app/Activity;)Lcom/crashlytics/android/answers/SessionEvent;

    move-result-object p1

    const/4 v1, 0x0

    invoke-virtual {v0, p1, v1}, Lcom/crashlytics/android/answers/SessionEventsHandler;->recordEventAsync(Ljava/lang/Object;Z)V

    return-void
.end method

.method public onStop(Landroid/app/Activity;)V
    .locals 3

    .line 109
    iget-object v0, p0, Lcom/crashlytics/android/answers/SessionAnalyticsManager;->eventsHandler:Lcom/crashlytics/android/answers/SessionEventsHandler;

    iget-object v1, p0, Lcom/crashlytics/android/answers/SessionAnalyticsManager;->metadata:Lcom/crashlytics/android/answers/SessionEventMetadata;

    sget-object v2, Lcom/crashlytics/android/answers/SessionEvent$Type;->STOP:Lcom/crashlytics/android/answers/SessionEvent$Type;

    invoke-static {v1, v2, p1}, Lcom/crashlytics/android/answers/SessionEvent;->buildActivityLifecycleEvent(Lcom/crashlytics/android/answers/SessionEventMetadata;Lcom/crashlytics/android/answers/SessionEvent$Type;Landroid/app/Activity;)Lcom/crashlytics/android/answers/SessionEvent;

    move-result-object p1

    const/4 v1, 0x0

    invoke-virtual {v0, p1, v1}, Lcom/crashlytics/android/answers/SessionEventsHandler;->recordEventAsync(Ljava/lang/Object;Z)V

    return-void
.end method

.method public setAnalyticsSettingsData(Lio/fabric/sdk/android/services/settings/AnalyticsSettingsData;Ljava/lang/String;)V
    .locals 1

    .line 115
    iget-boolean v0, p1, Lio/fabric/sdk/android/services/settings/AnalyticsSettingsData;->trackCustomEvents:Z

    iput-boolean v0, p0, Lcom/crashlytics/android/answers/SessionAnalyticsManager;->customEventsEnabled:Z

    .line 116
    iget-object v0, p0, Lcom/crashlytics/android/answers/SessionAnalyticsManager;->eventsHandler:Lcom/crashlytics/android/answers/SessionEventsHandler;

    invoke-virtual {v0, p1, p2}, Lcom/crashlytics/android/answers/SessionEventsHandler;->setAnalyticsSettingsData(Lio/fabric/sdk/android/services/settings/AnalyticsSettingsData;Ljava/lang/String;)V

    return-void
.end method
