.class Lcom/crashlytics/android/answers/EnabledSessionAnalyticsManagerStrategy;
.super Lio/fabric/sdk/android/services/events/EnabledEventsStrategy;
.source "EnabledSessionAnalyticsManagerStrategy.java"

# interfaces
.implements Lcom/crashlytics/android/answers/SessionAnalyticsManagerStrategy;


# annotations
.annotation system Ldalvik/annotation/Signature;
    value = {
        "Lio/fabric/sdk/android/services/events/EnabledEventsStrategy<",
        "Lcom/crashlytics/android/answers/SessionEvent;",
        ">;",
        "Lcom/crashlytics/android/answers/SessionAnalyticsManagerStrategy<",
        "Lcom/crashlytics/android/answers/SessionEvent;",
        ">;"
    }
.end annotation


# instance fields
.field eventFilter:Lcom/crashlytics/android/answers/EventFilter;

.field filesSender:Lio/fabric/sdk/android/services/events/FilesSender;

.field private final httpRequestFactory:Lio/fabric/sdk/android/services/network/HttpRequestFactory;


# direct methods
.method public constructor <init>(Landroid/content/Context;Ljava/util/concurrent/ScheduledExecutorService;Lcom/crashlytics/android/answers/SessionAnalyticsFilesManager;Lio/fabric/sdk/android/services/network/HttpRequestFactory;)V
    .locals 0

    .line 30
    invoke-direct {p0, p1, p2, p3}, Lio/fabric/sdk/android/services/events/EnabledEventsStrategy;-><init>(Landroid/content/Context;Ljava/util/concurrent/ScheduledExecutorService;Lio/fabric/sdk/android/services/events/EventsFilesManager;)V

    .line 25
    new-instance p1, Lcom/crashlytics/android/answers/KeepAllEventFilter;

    invoke-direct {p1}, Lcom/crashlytics/android/answers/KeepAllEventFilter;-><init>()V

    iput-object p1, p0, Lcom/crashlytics/android/answers/EnabledSessionAnalyticsManagerStrategy;->eventFilter:Lcom/crashlytics/android/answers/EventFilter;

    .line 31
    iput-object p4, p0, Lcom/crashlytics/android/answers/EnabledSessionAnalyticsManagerStrategy;->httpRequestFactory:Lio/fabric/sdk/android/services/network/HttpRequestFactory;

    return-void
.end method


# virtual methods
.method public getFilesSender()Lio/fabric/sdk/android/services/events/FilesSender;
    .locals 1

    .line 36
    iget-object v0, p0, Lcom/crashlytics/android/answers/EnabledSessionAnalyticsManagerStrategy;->filesSender:Lio/fabric/sdk/android/services/events/FilesSender;

    return-object v0
.end method

.method public recordEvent(Lcom/crashlytics/android/answers/SessionEvent;)V
    .locals 3

    .line 57
    iget-object v0, p0, Lcom/crashlytics/android/answers/EnabledSessionAnalyticsManagerStrategy;->eventFilter:Lcom/crashlytics/android/answers/EventFilter;

    invoke-interface {v0, p1}, Lcom/crashlytics/android/answers/EventFilter;->skipEvent(Lcom/crashlytics/android/answers/SessionEvent;)Z

    move-result v0

    if-eqz v0, :cond_0

    .line 58
    invoke-static {}, Lcom/crashlytics/android/answers/Answers;->getInstance()Lcom/crashlytics/android/answers/Answers;

    move-result-object v0

    invoke-virtual {v0}, Lcom/crashlytics/android/answers/Answers;->getContext()Landroid/content/Context;

    move-result-object v0

    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "skipping filtered event "

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/Object;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p1

    invoke-static {v0, p1}, Lio/fabric/sdk/android/services/common/CommonUtils;->logControlled(Landroid/content/Context;Ljava/lang/String;)V

    return-void

    .line 63
    :cond_0
    invoke-super {p0, p1}, Lio/fabric/sdk/android/services/events/EnabledEventsStrategy;->recordEvent(Ljava/lang/Object;)V

    return-void
.end method

.method public bridge synthetic recordEvent(Ljava/lang/Object;)V
    .locals 0

    .line 19
    check-cast p1, Lcom/crashlytics/android/answers/SessionEvent;

    invoke-virtual {p0, p1}, Lcom/crashlytics/android/answers/EnabledSessionAnalyticsManagerStrategy;->recordEvent(Lcom/crashlytics/android/answers/SessionEvent;)V

    return-void
.end method

.method public setAnalyticsSettingsData(Lio/fabric/sdk/android/services/settings/AnalyticsSettingsData;Ljava/lang/String;)V
    .locals 7

    .line 42
    new-instance v6, Lcom/crashlytics/android/answers/SessionAnalyticsFilesSender;

    invoke-static {}, Lcom/crashlytics/android/answers/Answers;->getInstance()Lcom/crashlytics/android/answers/Answers;

    move-result-object v1

    iget-object v3, p1, Lio/fabric/sdk/android/services/settings/AnalyticsSettingsData;->analyticsURL:Ljava/lang/String;

    iget-object v4, p0, Lcom/crashlytics/android/answers/EnabledSessionAnalyticsManagerStrategy;->httpRequestFactory:Lio/fabric/sdk/android/services/network/HttpRequestFactory;

    new-instance v0, Lio/fabric/sdk/android/services/common/ApiKey;

    invoke-direct {v0}, Lio/fabric/sdk/android/services/common/ApiKey;-><init>()V

    iget-object v2, p0, Lcom/crashlytics/android/answers/EnabledSessionAnalyticsManagerStrategy;->context:Landroid/content/Context;

    invoke-virtual {v0, v2}, Lio/fabric/sdk/android/services/common/ApiKey;->getValue(Landroid/content/Context;)Ljava/lang/String;

    move-result-object v5

    move-object v0, v6

    move-object v2, p2

    invoke-direct/range {v0 .. v5}, Lcom/crashlytics/android/answers/SessionAnalyticsFilesSender;-><init>(Lio/fabric/sdk/android/Kit;Ljava/lang/String;Ljava/lang/String;Lio/fabric/sdk/android/services/network/HttpRequestFactory;Ljava/lang/String;)V

    invoke-static {v6}, Lcom/crashlytics/android/answers/AnswersRetryFilesSender;->build(Lcom/crashlytics/android/answers/SessionAnalyticsFilesSender;)Lcom/crashlytics/android/answers/AnswersRetryFilesSender;

    move-result-object p2

    iput-object p2, p0, Lcom/crashlytics/android/answers/EnabledSessionAnalyticsManagerStrategy;->filesSender:Lio/fabric/sdk/android/services/events/FilesSender;

    .line 45
    iget-object p2, p0, Lcom/crashlytics/android/answers/EnabledSessionAnalyticsManagerStrategy;->filesManager:Lio/fabric/sdk/android/services/events/EventsFilesManager;

    check-cast p2, Lcom/crashlytics/android/answers/SessionAnalyticsFilesManager;

    invoke-virtual {p2, p1}, Lcom/crashlytics/android/answers/SessionAnalyticsFilesManager;->setAnalyticsSettingsData(Lio/fabric/sdk/android/services/settings/AnalyticsSettingsData;)V

    .line 47
    iget p2, p1, Lio/fabric/sdk/android/services/settings/AnalyticsSettingsData;->flushIntervalSeconds:I

    invoke-virtual {p0, p2}, Lcom/crashlytics/android/answers/EnabledSessionAnalyticsManagerStrategy;->configureRollover(I)V

    .line 49
    iget p2, p1, Lio/fabric/sdk/android/services/settings/AnalyticsSettingsData;->samplingRate:I

    const/4 v0, 0x1

    if-le p2, v0, :cond_0

    .line 50
    new-instance p2, Lcom/crashlytics/android/answers/SamplingEventFilter;

    iget p1, p1, Lio/fabric/sdk/android/services/settings/AnalyticsSettingsData;->samplingRate:I

    invoke-direct {p2, p1}, Lcom/crashlytics/android/answers/SamplingEventFilter;-><init>(I)V

    iput-object p2, p0, Lcom/crashlytics/android/answers/EnabledSessionAnalyticsManagerStrategy;->eventFilter:Lcom/crashlytics/android/answers/EventFilter;

    :cond_0
    return-void
.end method
