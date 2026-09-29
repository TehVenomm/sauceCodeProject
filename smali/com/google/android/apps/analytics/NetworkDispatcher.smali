.class Lcom/google/android/apps/analytics/NetworkDispatcher;
.super Ljava/lang/Object;

# interfaces
.implements Lcom/google/android/apps/analytics/Dispatcher;


# annotations
.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Lcom/google/android/apps/analytics/NetworkDispatcher$DispatcherThread;
    }
.end annotation


# static fields
.field private static final GOOGLE_ANALYTICS_HOST_NAME:Ljava/lang/String; = "www.google-analytics.com"

.field private static final GOOGLE_ANALYTICS_HOST_PORT:I = 0x50

.field private static final MAX_EVENTS_PER_PIPELINE:I = 0x1e

.field private static final MAX_GET_LENGTH:I = 0x7f4

.field private static final MAX_POST_LENGTH:I = 0x2000

.field private static final MAX_SEQUENTIAL_REQUESTS:I = 0x5

.field private static final MIN_RETRY_INTERVAL:J = 0x2L

.field private static final USER_AGENT_TEMPLATE:Ljava/lang/String; = "%s/%s (Linux; U; Android %s; %s-%s; %s Build/%s)"


# instance fields
.field private dispatcherThread:Lcom/google/android/apps/analytics/NetworkDispatcher$DispatcherThread;

.field private dryRun:Z

.field private final googleAnalyticsHost:Lorg/apache/http/HttpHost;

.field private final userAgent:Ljava/lang/String;


# direct methods
.method public constructor <init>()V
    .locals 2

    const-string v0, "GoogleAnalytics"

    const-string v1, "1.4.2"

    invoke-direct {p0, v0, v1}, Lcom/google/android/apps/analytics/NetworkDispatcher;-><init>(Ljava/lang/String;Ljava/lang/String;)V

    return-void
.end method

.method public constructor <init>(Ljava/lang/String;Ljava/lang/String;)V
    .locals 2

    const-string v0, "www.google-analytics.com"

    const/16 v1, 0x50

    invoke-direct {p0, p1, p2, v0, v1}, Lcom/google/android/apps/analytics/NetworkDispatcher;-><init>(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;I)V

    return-void
.end method

.method constructor <init>(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;I)V
    .locals 2

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    const/4 v0, 0x0

    iput-boolean v0, p0, Lcom/google/android/apps/analytics/NetworkDispatcher;->dryRun:Z

    new-instance v1, Lorg/apache/http/HttpHost;

    invoke-direct {v1, p3, p4}, Lorg/apache/http/HttpHost;-><init>(Ljava/lang/String;I)V

    iput-object v1, p0, Lcom/google/android/apps/analytics/NetworkDispatcher;->googleAnalyticsHost:Lorg/apache/http/HttpHost;

    invoke-static {}, Ljava/util/Locale;->getDefault()Ljava/util/Locale;

    move-result-object p3

    const-string p4, "%s/%s (Linux; U; Android %s; %s-%s; %s Build/%s)"

    const/4 v1, 0x7

    new-array v1, v1, [Ljava/lang/Object;

    aput-object p1, v1, v0

    const/4 p1, 0x1

    aput-object p2, v1, p1

    sget-object p1, Landroid/os/Build$VERSION;->RELEASE:Ljava/lang/String;

    const/4 p2, 0x2

    aput-object p1, v1, p2

    invoke-virtual {p3}, Ljava/util/Locale;->getLanguage()Ljava/lang/String;

    move-result-object p1

    if-eqz p1, :cond_0

    invoke-virtual {p3}, Ljava/util/Locale;->getLanguage()Ljava/lang/String;

    move-result-object p1

    invoke-virtual {p1}, Ljava/lang/String;->toLowerCase()Ljava/lang/String;

    move-result-object p1

    goto :goto_0

    :cond_0
    const-string p1, "en"

    :goto_0
    const/4 p2, 0x3

    aput-object p1, v1, p2

    const/4 p1, 0x4

    invoke-virtual {p3}, Ljava/util/Locale;->getCountry()Ljava/lang/String;

    move-result-object p2

    if-eqz p2, :cond_1

    invoke-virtual {p3}, Ljava/util/Locale;->getCountry()Ljava/lang/String;

    move-result-object p2

    invoke-virtual {p2}, Ljava/lang/String;->toLowerCase()Ljava/lang/String;

    move-result-object p2

    goto :goto_1

    :cond_1
    const-string p2, ""

    :goto_1
    aput-object p2, v1, p1

    const/4 p1, 0x5

    sget-object p2, Landroid/os/Build;->MODEL:Ljava/lang/String;

    aput-object p2, v1, p1

    const/4 p1, 0x6

    sget-object p2, Landroid/os/Build;->ID:Ljava/lang/String;

    aput-object p2, v1, p1

    invoke-static {p4, v1}, Ljava/lang/String;->format(Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object p1

    iput-object p1, p0, Lcom/google/android/apps/analytics/NetworkDispatcher;->userAgent:Ljava/lang/String;

    return-void
.end method

.method static synthetic access$200(Lcom/google/android/apps/analytics/NetworkDispatcher;)Lorg/apache/http/HttpHost;
    .locals 0

    iget-object p0, p0, Lcom/google/android/apps/analytics/NetworkDispatcher;->googleAnalyticsHost:Lorg/apache/http/HttpHost;

    return-object p0
.end method


# virtual methods
.method public dispatchHits([Lcom/google/android/apps/analytics/Hit;)V
    .locals 1

    iget-object v0, p0, Lcom/google/android/apps/analytics/NetworkDispatcher;->dispatcherThread:Lcom/google/android/apps/analytics/NetworkDispatcher$DispatcherThread;

    if-nez v0, :cond_0

    return-void

    :cond_0
    iget-object v0, p0, Lcom/google/android/apps/analytics/NetworkDispatcher;->dispatcherThread:Lcom/google/android/apps/analytics/NetworkDispatcher$DispatcherThread;

    invoke-virtual {v0, p1}, Lcom/google/android/apps/analytics/NetworkDispatcher$DispatcherThread;->dispatchHits([Lcom/google/android/apps/analytics/Hit;)V

    return-void
.end method

.method getUserAgent()Ljava/lang/String;
    .locals 1

    iget-object v0, p0, Lcom/google/android/apps/analytics/NetworkDispatcher;->userAgent:Ljava/lang/String;

    return-object v0
.end method

.method public init(Lcom/google/android/apps/analytics/Dispatcher$Callbacks;)V
    .locals 3

    invoke-virtual {p0}, Lcom/google/android/apps/analytics/NetworkDispatcher;->stop()V

    new-instance v0, Lcom/google/android/apps/analytics/NetworkDispatcher$DispatcherThread;

    iget-object v1, p0, Lcom/google/android/apps/analytics/NetworkDispatcher;->userAgent:Ljava/lang/String;

    const/4 v2, 0x0

    invoke-direct {v0, p1, v1, p0, v2}, Lcom/google/android/apps/analytics/NetworkDispatcher$DispatcherThread;-><init>(Lcom/google/android/apps/analytics/Dispatcher$Callbacks;Ljava/lang/String;Lcom/google/android/apps/analytics/NetworkDispatcher;Lcom/google/android/apps/analytics/NetworkDispatcher$1;)V

    iput-object v0, p0, Lcom/google/android/apps/analytics/NetworkDispatcher;->dispatcherThread:Lcom/google/android/apps/analytics/NetworkDispatcher$DispatcherThread;

    iget-object p1, p0, Lcom/google/android/apps/analytics/NetworkDispatcher;->dispatcherThread:Lcom/google/android/apps/analytics/NetworkDispatcher$DispatcherThread;

    invoke-virtual {p1}, Lcom/google/android/apps/analytics/NetworkDispatcher$DispatcherThread;->start()V

    return-void
.end method

.method public init(Lcom/google/android/apps/analytics/Dispatcher$Callbacks;Lcom/google/android/apps/analytics/PipelinedRequester;Lcom/google/android/apps/analytics/HitStore;)V
    .locals 6

    invoke-virtual {p0}, Lcom/google/android/apps/analytics/NetworkDispatcher;->stop()V

    new-instance p3, Lcom/google/android/apps/analytics/NetworkDispatcher$DispatcherThread;

    iget-object v3, p0, Lcom/google/android/apps/analytics/NetworkDispatcher;->userAgent:Ljava/lang/String;

    const/4 v5, 0x0

    move-object v0, p3

    move-object v1, p1

    move-object v2, p2

    move-object v4, p0

    invoke-direct/range {v0 .. v5}, Lcom/google/android/apps/analytics/NetworkDispatcher$DispatcherThread;-><init>(Lcom/google/android/apps/analytics/Dispatcher$Callbacks;Lcom/google/android/apps/analytics/PipelinedRequester;Ljava/lang/String;Lcom/google/android/apps/analytics/NetworkDispatcher;Lcom/google/android/apps/analytics/NetworkDispatcher$1;)V

    iput-object p3, p0, Lcom/google/android/apps/analytics/NetworkDispatcher;->dispatcherThread:Lcom/google/android/apps/analytics/NetworkDispatcher$DispatcherThread;

    iget-object p1, p0, Lcom/google/android/apps/analytics/NetworkDispatcher;->dispatcherThread:Lcom/google/android/apps/analytics/NetworkDispatcher$DispatcherThread;

    invoke-virtual {p1}, Lcom/google/android/apps/analytics/NetworkDispatcher$DispatcherThread;->start()V

    return-void
.end method

.method public isDryRun()Z
    .locals 1

    iget-boolean v0, p0, Lcom/google/android/apps/analytics/NetworkDispatcher;->dryRun:Z

    return v0
.end method

.method public setDryRun(Z)V
    .locals 0

    iput-boolean p1, p0, Lcom/google/android/apps/analytics/NetworkDispatcher;->dryRun:Z

    return-void
.end method

.method public stop()V
    .locals 1

    iget-object v0, p0, Lcom/google/android/apps/analytics/NetworkDispatcher;->dispatcherThread:Lcom/google/android/apps/analytics/NetworkDispatcher$DispatcherThread;

    if-eqz v0, :cond_0

    iget-object v0, p0, Lcom/google/android/apps/analytics/NetworkDispatcher;->dispatcherThread:Lcom/google/android/apps/analytics/NetworkDispatcher$DispatcherThread;

    invoke-virtual {v0}, Lcom/google/android/apps/analytics/NetworkDispatcher$DispatcherThread;->getLooper()Landroid/os/Looper;

    move-result-object v0

    if-eqz v0, :cond_0

    iget-object v0, p0, Lcom/google/android/apps/analytics/NetworkDispatcher;->dispatcherThread:Lcom/google/android/apps/analytics/NetworkDispatcher$DispatcherThread;

    invoke-virtual {v0}, Lcom/google/android/apps/analytics/NetworkDispatcher$DispatcherThread;->getLooper()Landroid/os/Looper;

    move-result-object v0

    invoke-virtual {v0}, Landroid/os/Looper;->quit()V

    const/4 v0, 0x0

    iput-object v0, p0, Lcom/google/android/apps/analytics/NetworkDispatcher;->dispatcherThread:Lcom/google/android/apps/analytics/NetworkDispatcher$DispatcherThread;

    :cond_0
    return-void
.end method

.method waitForThreadLooper()V
    .locals 1

    iget-object v0, p0, Lcom/google/android/apps/analytics/NetworkDispatcher;->dispatcherThread:Lcom/google/android/apps/analytics/NetworkDispatcher$DispatcherThread;

    invoke-virtual {v0}, Lcom/google/android/apps/analytics/NetworkDispatcher$DispatcherThread;->getLooper()Landroid/os/Looper;

    :goto_0
    iget-object v0, p0, Lcom/google/android/apps/analytics/NetworkDispatcher;->dispatcherThread:Lcom/google/android/apps/analytics/NetworkDispatcher$DispatcherThread;

    iget-object v0, v0, Lcom/google/android/apps/analytics/NetworkDispatcher$DispatcherThread;->handlerExecuteOnDispatcherThread:Landroid/os/Handler;

    if-nez v0, :cond_0

    invoke-static {}, Ljava/lang/Thread;->yield()V

    goto :goto_0

    :cond_0
    return-void
.end method
