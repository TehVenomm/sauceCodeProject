.class public Lcom/crashlytics/android/core/CrashlyticsCore;
.super Lio/fabric/sdk/android/Kit;
.source "CrashlyticsCore.java"


# annotations
.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Lcom/crashlytics/android/core/CrashlyticsCore$OptInLatch;,
        Lcom/crashlytics/android/core/CrashlyticsCore$Builder;
    }
.end annotation

.annotation system Ldalvik/annotation/Signature;
    value = {
        "Lio/fabric/sdk/android/Kit<",
        "Ljava/lang/Void;",
        ">;"
    }
.end annotation

.annotation runtime Lio/fabric/sdk/android/services/concurrency/DependsOn;
    value = {
        Lcom/crashlytics/android/core/internal/CrashEventDataProvider;
    }
.end annotation


# static fields
.field static final CLS_DEFAULT_PROCESS_DELAY:F = 1.0f

.field static final COLLECT_CUSTOM_KEYS:Ljava/lang/String; = "com.crashlytics.CollectCustomKeys"

.field static final COLLECT_CUSTOM_LOGS:Ljava/lang/String; = "com.crashlytics.CollectCustomLogs"

.field static final CRASHLYTICS_API_ENDPOINT:Ljava/lang/String; = "com.crashlytics.ApiEndpoint"

.field static final CRASHLYTICS_REQUIRE_BUILD_ID:Ljava/lang/String; = "com.crashlytics.RequireBuildId"

.field static final CRASHLYTICS_REQUIRE_BUILD_ID_DEFAULT:Z = true

.field static final DEFAULT_MAIN_HANDLER_TIMEOUT_SEC:I = 0x4

.field private static final INITIALIZATION_MARKER_FILE_NAME:Ljava/lang/String; = "initialization_marker"

.field static final MAX_ATTRIBUTES:I = 0x40

.field static final MAX_ATTRIBUTE_SIZE:I = 0x400

.field private static final PREF_ALWAYS_SEND_REPORTS_KEY:Ljava/lang/String; = "always_send_reports_opt_in"

.field private static final SHOULD_PROMPT_BEFORE_SENDING_REPORTS_DEFAULT:Z = false

.field public static final TAG:Ljava/lang/String; = "Fabric"


# instance fields
.field private final attributes:Ljava/util/concurrent/ConcurrentHashMap;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/concurrent/ConcurrentHashMap<",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            ">;"
        }
    .end annotation
.end field

.field private buildId:Ljava/lang/String;

.field private delay:F

.field private disabled:Z

.field private executorServiceWrapper:Lcom/crashlytics/android/core/CrashlyticsExecutorServiceWrapper;

.field private externalCrashEventDataProvider:Lcom/crashlytics/android/core/internal/CrashEventDataProvider;

.field private handler:Lcom/crashlytics/android/core/CrashlyticsUncaughtExceptionHandler;

.field private httpRequestFactory:Lio/fabric/sdk/android/services/network/HttpRequestFactory;

.field private initializationMarkerFile:Ljava/io/File;

.field private installerPackageName:Ljava/lang/String;

.field private listener:Lcom/crashlytics/android/core/CrashlyticsListener;

.field private packageName:Ljava/lang/String;

.field private final pinningInfo:Lcom/crashlytics/android/core/PinningInfoProvider;

.field private final startTime:J

.field private userEmail:Ljava/lang/String;

.field private userId:Ljava/lang/String;

.field private userName:Ljava/lang/String;

.field private versionCode:Ljava/lang/String;

.field private versionName:Ljava/lang/String;


# direct methods
.method public constructor <init>()V
    .locals 3

    const/4 v0, 0x0

    const/high16 v1, 0x3f800000    # 1.0f

    const/4 v2, 0x0

    .line 197
    invoke-direct {p0, v1, v0, v0, v2}, Lcom/crashlytics/android/core/CrashlyticsCore;-><init>(FLcom/crashlytics/android/core/CrashlyticsListener;Lcom/crashlytics/android/core/PinningInfoProvider;Z)V

    return-void
.end method

.method constructor <init>(FLcom/crashlytics/android/core/CrashlyticsListener;Lcom/crashlytics/android/core/PinningInfoProvider;Z)V
    .locals 7

    const-string v0, "Crashlytics Exception Handler"

    .line 202
    invoke-static {v0}, Lio/fabric/sdk/android/services/common/ExecutorUtils;->buildSingleThreadExecutorService(Ljava/lang/String;)Ljava/util/concurrent/ExecutorService;

    move-result-object v6

    move-object v1, p0

    move v2, p1

    move-object v3, p2

    move-object v4, p3

    move v5, p4

    invoke-direct/range {v1 .. v6}, Lcom/crashlytics/android/core/CrashlyticsCore;-><init>(FLcom/crashlytics/android/core/CrashlyticsListener;Lcom/crashlytics/android/core/PinningInfoProvider;ZLjava/util/concurrent/ExecutorService;)V

    return-void
.end method

.method constructor <init>(FLcom/crashlytics/android/core/CrashlyticsListener;Lcom/crashlytics/android/core/PinningInfoProvider;ZLjava/util/concurrent/ExecutorService;)V
    .locals 2

    .line 211
    invoke-direct {p0}, Lio/fabric/sdk/android/Kit;-><init>()V

    const/4 v0, 0x0

    .line 94
    iput-object v0, p0, Lcom/crashlytics/android/core/CrashlyticsCore;->userId:Ljava/lang/String;

    .line 95
    iput-object v0, p0, Lcom/crashlytics/android/core/CrashlyticsCore;->userEmail:Ljava/lang/String;

    .line 96
    iput-object v0, p0, Lcom/crashlytics/android/core/CrashlyticsCore;->userName:Ljava/lang/String;

    .line 212
    new-instance v0, Ljava/util/concurrent/ConcurrentHashMap;

    invoke-direct {v0}, Ljava/util/concurrent/ConcurrentHashMap;-><init>()V

    iput-object v0, p0, Lcom/crashlytics/android/core/CrashlyticsCore;->attributes:Ljava/util/concurrent/ConcurrentHashMap;

    .line 213
    invoke-static {}, Ljava/lang/System;->currentTimeMillis()J

    move-result-wide v0

    iput-wide v0, p0, Lcom/crashlytics/android/core/CrashlyticsCore;->startTime:J

    .line 214
    iput p1, p0, Lcom/crashlytics/android/core/CrashlyticsCore;->delay:F

    .line 215
    iput-object p2, p0, Lcom/crashlytics/android/core/CrashlyticsCore;->listener:Lcom/crashlytics/android/core/CrashlyticsListener;

    .line 216
    iput-object p3, p0, Lcom/crashlytics/android/core/CrashlyticsCore;->pinningInfo:Lcom/crashlytics/android/core/PinningInfoProvider;

    .line 217
    iput-boolean p4, p0, Lcom/crashlytics/android/core/CrashlyticsCore;->disabled:Z

    .line 218
    new-instance p1, Lcom/crashlytics/android/core/CrashlyticsExecutorServiceWrapper;

    invoke-direct {p1, p5}, Lcom/crashlytics/android/core/CrashlyticsExecutorServiceWrapper;-><init>(Ljava/util/concurrent/ExecutorService;)V

    iput-object p1, p0, Lcom/crashlytics/android/core/CrashlyticsCore;->executorServiceWrapper:Lcom/crashlytics/android/core/CrashlyticsExecutorServiceWrapper;

    return-void
.end method

.method static synthetic access$000(Lcom/crashlytics/android/core/CrashlyticsCore;)Ljava/io/File;
    .locals 0

    .line 61
    iget-object p0, p0, Lcom/crashlytics/android/core/CrashlyticsCore;->initializationMarkerFile:Ljava/io/File;

    return-object p0
.end method

.method static synthetic access$100(Lcom/crashlytics/android/core/CrashlyticsCore;Landroid/app/Activity;Lio/fabric/sdk/android/services/settings/PromptSettingsData;)Z
    .locals 0

    .line 61
    invoke-direct {p0, p1, p2}, Lcom/crashlytics/android/core/CrashlyticsCore;->getSendDecisionFromUser(Landroid/app/Activity;Lio/fabric/sdk/android/services/settings/PromptSettingsData;)Z

    move-result p0

    return p0
.end method

.method static synthetic access$300(Lcom/crashlytics/android/core/CrashlyticsCore;FI)I
    .locals 0

    .line 61
    invoke-direct {p0, p1, p2}, Lcom/crashlytics/android/core/CrashlyticsCore;->dipsToPixels(FI)I

    move-result p0

    return p0
.end method

.method private dipsToPixels(FI)I
    .locals 0

    int-to-float p2, p2

    mul-float p1, p1, p2

    float-to-int p1, p1

    return p1
.end method

.method private doLog(ILjava/lang/String;Ljava/lang/String;)V
    .locals 4

    .line 452
    iget-boolean v0, p0, Lcom/crashlytics/android/core/CrashlyticsCore;->disabled:Z

    if-eqz v0, :cond_0

    return-void

    :cond_0
    const-string v0, "prior to logging messages."

    .line 456
    invoke-static {v0}, Lcom/crashlytics/android/core/CrashlyticsCore;->ensureFabricWithCalled(Ljava/lang/String;)Z

    move-result v0

    if-eqz v0, :cond_1

    .line 457
    invoke-static {}, Ljava/lang/System;->currentTimeMillis()J

    move-result-wide v0

    iget-wide v2, p0, Lcom/crashlytics/android/core/CrashlyticsCore;->startTime:J

    sub-long/2addr v0, v2

    .line 458
    iget-object v2, p0, Lcom/crashlytics/android/core/CrashlyticsCore;->handler:Lcom/crashlytics/android/core/CrashlyticsUncaughtExceptionHandler;

    invoke-static {p1, p2, p3}, Lcom/crashlytics/android/core/CrashlyticsCore;->formatLogMessage(ILjava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object p1

    invoke-virtual {v2, v0, v1, p1}, Lcom/crashlytics/android/core/CrashlyticsUncaughtExceptionHandler;->writeToLog(JLjava/lang/String;)V

    :cond_1
    return-void
.end method

.method private static ensureFabricWithCalled(Ljava/lang/String;)Z
    .locals 4

    .line 1034
    invoke-static {}, Lcom/crashlytics/android/core/CrashlyticsCore;->getInstance()Lcom/crashlytics/android/core/CrashlyticsCore;

    move-result-object v0

    if-eqz v0, :cond_1

    .line 1035
    iget-object v0, v0, Lcom/crashlytics/android/core/CrashlyticsCore;->handler:Lcom/crashlytics/android/core/CrashlyticsUncaughtExceptionHandler;

    if-nez v0, :cond_0

    goto :goto_0

    :cond_0
    const/4 p0, 0x1

    return p0

    .line 1036
    :cond_1
    :goto_0
    invoke-static {}, Lio/fabric/sdk/android/Fabric;->getLogger()Lio/fabric/sdk/android/Logger;

    move-result-object v0

    const-string v1, "Fabric"

    new-instance v2, Ljava/lang/StringBuilder;

    invoke-direct {v2}, Ljava/lang/StringBuilder;-><init>()V

    const-string v3, "Crashlytics must be initialized by calling Fabric.with(Context) "

    invoke-virtual {v2, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v2, p0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v2}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p0

    const/4 v2, 0x0

    invoke-interface {v0, v1, p0, v2}, Lio/fabric/sdk/android/Logger;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)V

    const/4 p0, 0x0

    return p0
.end method

.method private finishInitSynchronously()V
    .locals 4

    .line 656
    new-instance v0, Lcom/crashlytics/android/core/CrashlyticsCore$1;

    invoke-direct {v0, p0}, Lcom/crashlytics/android/core/CrashlyticsCore$1;-><init>(Lcom/crashlytics/android/core/CrashlyticsCore;)V

    .line 668
    invoke-virtual {p0}, Lcom/crashlytics/android/core/CrashlyticsCore;->getDependencies()Ljava/util/Collection;

    move-result-object v1

    invoke-interface {v1}, Ljava/util/Collection;->iterator()Ljava/util/Iterator;

    move-result-object v1

    :goto_0
    invoke-interface {v1}, Ljava/util/Iterator;->hasNext()Z

    move-result v2

    if-eqz v2, :cond_0

    invoke-interface {v1}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Lio/fabric/sdk/android/services/concurrency/Task;

    .line 669
    invoke-virtual {v0, v2}, Lio/fabric/sdk/android/services/concurrency/PriorityCallable;->addDependency(Lio/fabric/sdk/android/services/concurrency/Task;)V

    goto :goto_0

    .line 672
    :cond_0
    invoke-virtual {p0}, Lcom/crashlytics/android/core/CrashlyticsCore;->getFabric()Lio/fabric/sdk/android/Fabric;

    move-result-object v1

    invoke-virtual {v1}, Lio/fabric/sdk/android/Fabric;->getExecutorService()Ljava/util/concurrent/ExecutorService;

    move-result-object v1

    invoke-interface {v1, v0}, Ljava/util/concurrent/ExecutorService;->submit(Ljava/util/concurrent/Callable;)Ljava/util/concurrent/Future;

    move-result-object v0

    .line 674
    invoke-static {}, Lio/fabric/sdk/android/Fabric;->getLogger()Lio/fabric/sdk/android/Logger;

    move-result-object v1

    const-string v2, "Fabric"

    const-string v3, "Crashlytics detected incomplete initialization on previous app launch. Will initialize synchronously."

    invoke-interface {v1, v2, v3}, Lio/fabric/sdk/android/Logger;->d(Ljava/lang/String;Ljava/lang/String;)V

    const-wide/16 v1, 0x4

    .line 679
    :try_start_0
    sget-object v3, Ljava/util/concurrent/TimeUnit;->SECONDS:Ljava/util/concurrent/TimeUnit;

    invoke-interface {v0, v1, v2, v3}, Ljava/util/concurrent/Future;->get(JLjava/util/concurrent/TimeUnit;)Ljava/lang/Object;
    :try_end_0
    .catch Ljava/lang/InterruptedException; {:try_start_0 .. :try_end_0} :catch_2
    .catch Ljava/util/concurrent/ExecutionException; {:try_start_0 .. :try_end_0} :catch_1
    .catch Ljava/util/concurrent/TimeoutException; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_1

    :catch_0
    move-exception v0

    .line 685
    invoke-static {}, Lio/fabric/sdk/android/Fabric;->getLogger()Lio/fabric/sdk/android/Logger;

    move-result-object v1

    const-string v2, "Fabric"

    const-string v3, "Crashlytics timed out during initialization."

    invoke-interface {v1, v2, v3, v0}, Lio/fabric/sdk/android/Logger;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)V

    goto :goto_1

    :catch_1
    move-exception v0

    .line 683
    invoke-static {}, Lio/fabric/sdk/android/Fabric;->getLogger()Lio/fabric/sdk/android/Logger;

    move-result-object v1

    const-string v2, "Fabric"

    const-string v3, "Problem encountered during Crashlytics initialization."

    invoke-interface {v1, v2, v3, v0}, Lio/fabric/sdk/android/Logger;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)V

    goto :goto_1

    :catch_2
    move-exception v0

    .line 681
    invoke-static {}, Lio/fabric/sdk/android/Fabric;->getLogger()Lio/fabric/sdk/android/Logger;

    move-result-object v1

    const-string v2, "Fabric"

    const-string v3, "Crashlytics was interrupted during initialization."

    invoke-interface {v1, v2, v3, v0}, Lio/fabric/sdk/android/Logger;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)V

    :goto_1
    return-void
.end method

.method private static formatLogMessage(ILjava/lang/String;Ljava/lang/String;)Ljava/lang/String;
    .locals 1

    .line 1030
    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    invoke-static {p0}, Lio/fabric/sdk/android/services/common/CommonUtils;->logPriorityToString(I)Ljava/lang/String;

    move-result-object p0

    invoke-virtual {v0, p0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string p0, "/"

    invoke-virtual {v0, p0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string p0, " "

    invoke-virtual {v0, p0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0, p2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p0

    return-object p0
.end method

.method public static getInstance()Lcom/crashlytics/android/core/CrashlyticsCore;
    .locals 1

    .line 404
    const-class v0, Lcom/crashlytics/android/core/CrashlyticsCore;

    invoke-static {v0}, Lio/fabric/sdk/android/Fabric;->getKit(Ljava/lang/Class;)Lio/fabric/sdk/android/Kit;

    move-result-object v0

    check-cast v0, Lcom/crashlytics/android/core/CrashlyticsCore;

    return-object v0
.end method

.method private getSendDecisionFromUser(Landroid/app/Activity;Lio/fabric/sdk/android/services/settings/PromptSettingsData;)Z
    .locals 8

    .line 934
    new-instance v4, Lcom/crashlytics/android/core/DialogStringResolver;

    invoke-direct {v4, p1, p2}, Lcom/crashlytics/android/core/DialogStringResolver;-><init>(Landroid/content/Context;Lio/fabric/sdk/android/services/settings/PromptSettingsData;)V

    .line 938
    new-instance v6, Lcom/crashlytics/android/core/CrashlyticsCore$OptInLatch;

    const/4 v0, 0x0

    invoke-direct {v6, p0, v0}, Lcom/crashlytics/android/core/CrashlyticsCore$OptInLatch;-><init>(Lcom/crashlytics/android/core/CrashlyticsCore;Lcom/crashlytics/android/core/CrashlyticsCore$1;)V

    .line 942
    new-instance v7, Lcom/crashlytics/android/core/CrashlyticsCore$7;

    move-object v0, v7

    move-object v1, p0

    move-object v2, p1

    move-object v3, v6

    move-object v5, p2

    invoke-direct/range {v0 .. v5}, Lcom/crashlytics/android/core/CrashlyticsCore$7;-><init>(Lcom/crashlytics/android/core/CrashlyticsCore;Landroid/app/Activity;Lcom/crashlytics/android/core/CrashlyticsCore$OptInLatch;Lcom/crashlytics/android/core/DialogStringResolver;Lio/fabric/sdk/android/services/settings/PromptSettingsData;)V

    invoke-virtual {p1, v7}, Landroid/app/Activity;->runOnUiThread(Ljava/lang/Runnable;)V

    .line 1007
    invoke-static {}, Lio/fabric/sdk/android/Fabric;->getLogger()Lio/fabric/sdk/android/Logger;

    move-result-object p1

    const-string p2, "Fabric"

    const-string v0, "Waiting for user opt-in."

    invoke-interface {p1, p2, v0}, Lio/fabric/sdk/android/Logger;->d(Ljava/lang/String;Ljava/lang/String;)V

    .line 1008
    invoke-virtual {v6}, Lcom/crashlytics/android/core/CrashlyticsCore$OptInLatch;->await()V

    .line 1009
    invoke-virtual {v6}, Lcom/crashlytics/android/core/CrashlyticsCore$OptInLatch;->getOptIn()Z

    move-result p1

    return p1
.end method

.method private isRequiringBuildId(Landroid/content/Context;)Z
    .locals 2

    const-string v0, "com.crashlytics.RequireBuildId"

    const/4 v1, 0x1

    .line 1021
    invoke-static {p1, v0, v1}, Lio/fabric/sdk/android/services/common/CommonUtils;->getBooleanResourceValue(Landroid/content/Context;Ljava/lang/String;Z)Z

    move-result p1

    return p1
.end method

.method static recordFatalExceptionEvent(Ljava/lang/String;)V
    .locals 2

    .line 720
    const-class v0, Lcom/crashlytics/android/answers/Answers;

    invoke-static {v0}, Lio/fabric/sdk/android/Fabric;->getKit(Ljava/lang/Class;)Lio/fabric/sdk/android/Kit;

    move-result-object v0

    check-cast v0, Lcom/crashlytics/android/answers/Answers;

    if-eqz v0, :cond_0

    .line 722
    new-instance v1, Lio/fabric/sdk/android/services/common/Crash$FatalException;

    invoke-direct {v1, p0}, Lio/fabric/sdk/android/services/common/Crash$FatalException;-><init>(Ljava/lang/String;)V

    invoke-virtual {v0, v1}, Lcom/crashlytics/android/answers/Answers;->onException(Lio/fabric/sdk/android/services/common/Crash$FatalException;)V

    :cond_0
    return-void
.end method

.method static recordLoggedExceptionEvent(Ljava/lang/String;)V
    .locals 2

    .line 709
    const-class v0, Lcom/crashlytics/android/answers/Answers;

    invoke-static {v0}, Lio/fabric/sdk/android/Fabric;->getKit(Ljava/lang/Class;)Lio/fabric/sdk/android/Kit;

    move-result-object v0

    check-cast v0, Lcom/crashlytics/android/answers/Answers;

    if-eqz v0, :cond_0

    .line 711
    new-instance v1, Lio/fabric/sdk/android/services/common/Crash$LoggedException;

    invoke-direct {v1, p0}, Lio/fabric/sdk/android/services/common/Crash$LoggedException;-><init>(Ljava/lang/String;)V

    invoke-virtual {v0, v1}, Lcom/crashlytics/android/answers/Answers;->onException(Lio/fabric/sdk/android/services/common/Crash$LoggedException;)V

    :cond_0
    return-void
.end method

.method private static sanitizeAttribute(Ljava/lang/String;)Ljava/lang/String;
    .locals 2

    if-eqz p0, :cond_0

    .line 1047
    invoke-virtual {p0}, Ljava/lang/String;->trim()Ljava/lang/String;

    move-result-object p0

    .line 1048
    invoke-virtual {p0}, Ljava/lang/String;->length()I

    move-result v0

    const/16 v1, 0x400

    if-le v0, v1, :cond_0

    const/4 v0, 0x0

    .line 1049
    invoke-virtual {p0, v0, v1}, Ljava/lang/String;->substring(II)Ljava/lang/String;

    move-result-object p0

    :cond_0
    return-object p0
.end method

.method private setAndValidateKitProperties(Landroid/content/Context;Ljava/lang/String;)V
    .locals 4

    .line 303
    iget-object v0, p0, Lcom/crashlytics/android/core/CrashlyticsCore;->pinningInfo:Lcom/crashlytics/android/core/PinningInfoProvider;

    if-eqz v0, :cond_0

    new-instance v0, Lcom/crashlytics/android/core/CrashlyticsPinningInfoProvider;

    iget-object v1, p0, Lcom/crashlytics/android/core/CrashlyticsCore;->pinningInfo:Lcom/crashlytics/android/core/PinningInfoProvider;

    invoke-direct {v0, v1}, Lcom/crashlytics/android/core/CrashlyticsPinningInfoProvider;-><init>(Lcom/crashlytics/android/core/PinningInfoProvider;)V

    goto :goto_0

    :cond_0
    const/4 v0, 0x0

    .line 305
    :goto_0
    new-instance v1, Lio/fabric/sdk/android/services/network/DefaultHttpRequestFactory;

    invoke-static {}, Lio/fabric/sdk/android/Fabric;->getLogger()Lio/fabric/sdk/android/Logger;

    move-result-object v2

    invoke-direct {v1, v2}, Lio/fabric/sdk/android/services/network/DefaultHttpRequestFactory;-><init>(Lio/fabric/sdk/android/Logger;)V

    iput-object v1, p0, Lcom/crashlytics/android/core/CrashlyticsCore;->httpRequestFactory:Lio/fabric/sdk/android/services/network/HttpRequestFactory;

    .line 306
    iget-object v1, p0, Lcom/crashlytics/android/core/CrashlyticsCore;->httpRequestFactory:Lio/fabric/sdk/android/services/network/HttpRequestFactory;

    invoke-interface {v1, v0}, Lio/fabric/sdk/android/services/network/HttpRequestFactory;->setPinningInfoProvider(Lio/fabric/sdk/android/services/network/PinningInfoProvider;)V

    .line 309
    :try_start_0
    invoke-virtual {p1}, Landroid/content/Context;->getPackageName()Ljava/lang/String;

    move-result-object v0

    iput-object v0, p0, Lcom/crashlytics/android/core/CrashlyticsCore;->packageName:Ljava/lang/String;

    .line 310
    invoke-virtual {p0}, Lcom/crashlytics/android/core/CrashlyticsCore;->getIdManager()Lio/fabric/sdk/android/services/common/IdManager;

    move-result-object v0

    invoke-virtual {v0}, Lio/fabric/sdk/android/services/common/IdManager;->getInstallerPackageName()Ljava/lang/String;

    move-result-object v0

    iput-object v0, p0, Lcom/crashlytics/android/core/CrashlyticsCore;->installerPackageName:Ljava/lang/String;

    .line 311
    invoke-static {}, Lio/fabric/sdk/android/Fabric;->getLogger()Lio/fabric/sdk/android/Logger;

    move-result-object v0

    const-string v1, "Fabric"

    new-instance v2, Ljava/lang/StringBuilder;

    invoke-direct {v2}, Ljava/lang/StringBuilder;-><init>()V

    const-string v3, "Installer package name is: "

    invoke-virtual {v2, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v3, p0, Lcom/crashlytics/android/core/CrashlyticsCore;->installerPackageName:Ljava/lang/String;

    invoke-virtual {v2, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v2}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v2

    invoke-interface {v0, v1, v2}, Lio/fabric/sdk/android/Logger;->d(Ljava/lang/String;Ljava/lang/String;)V

    .line 314
    invoke-virtual {p1}, Landroid/content/Context;->getPackageManager()Landroid/content/pm/PackageManager;

    move-result-object v0

    .line 315
    iget-object v1, p0, Lcom/crashlytics/android/core/CrashlyticsCore;->packageName:Ljava/lang/String;

    const/4 v2, 0x0

    invoke-virtual {v0, v1, v2}, Landroid/content/pm/PackageManager;->getPackageInfo(Ljava/lang/String;I)Landroid/content/pm/PackageInfo;

    move-result-object v0

    .line 316
    iget v1, v0, Landroid/content/pm/PackageInfo;->versionCode:I

    invoke-static {v1}, Ljava/lang/Integer;->toString(I)Ljava/lang/String;

    move-result-object v1

    iput-object v1, p0, Lcom/crashlytics/android/core/CrashlyticsCore;->versionCode:Ljava/lang/String;

    .line 317
    iget-object v1, v0, Landroid/content/pm/PackageInfo;->versionName:Ljava/lang/String;

    if-nez v1, :cond_1

    const-string v0, "0.0"

    goto :goto_1

    :cond_1
    iget-object v0, v0, Landroid/content/pm/PackageInfo;->versionName:Ljava/lang/String;

    :goto_1
    iput-object v0, p0, Lcom/crashlytics/android/core/CrashlyticsCore;->versionName:Ljava/lang/String;

    .line 322
    invoke-static {p1}, Lio/fabric/sdk/android/services/common/CommonUtils;->resolveBuildId(Landroid/content/Context;)Ljava/lang/String;

    move-result-object v0

    iput-object v0, p0, Lcom/crashlytics/android/core/CrashlyticsCore;->buildId:Ljava/lang/String;
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_2

    :catch_0
    move-exception v0

    .line 325
    invoke-static {}, Lio/fabric/sdk/android/Fabric;->getLogger()Lio/fabric/sdk/android/Logger;

    move-result-object v1

    const-string v2, "Fabric"

    const-string v3, "Error setting up app properties"

    invoke-interface {v1, v2, v3, v0}, Lio/fabric/sdk/android/Logger;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)V

    .line 330
    :goto_2
    invoke-virtual {p0}, Lcom/crashlytics/android/core/CrashlyticsCore;->getIdManager()Lio/fabric/sdk/android/services/common/IdManager;

    move-result-object v0

    invoke-virtual {v0}, Lio/fabric/sdk/android/services/common/IdManager;->getBluetoothMacAddress()Ljava/lang/String;

    .line 334
    iget-object v0, p0, Lcom/crashlytics/android/core/CrashlyticsCore;->buildId:Ljava/lang/String;

    invoke-direct {p0, p1}, Lcom/crashlytics/android/core/CrashlyticsCore;->isRequiringBuildId(Landroid/content/Context;)Z

    move-result p1

    invoke-virtual {p0, v0, p1}, Lcom/crashlytics/android/core/CrashlyticsCore;->getBuildIdValidator(Ljava/lang/String;Z)Lcom/crashlytics/android/core/BuildIdValidator;

    move-result-object p1

    iget-object v0, p0, Lcom/crashlytics/android/core/CrashlyticsCore;->packageName:Ljava/lang/String;

    invoke-virtual {p1, p2, v0}, Lcom/crashlytics/android/core/BuildIdValidator;->validate(Ljava/lang/String;Ljava/lang/String;)V

    return-void
.end method


# virtual methods
.method canSendWithUserApproval()Z
    .locals 3

    .line 903
    invoke-static {}, Lio/fabric/sdk/android/services/settings/Settings;->getInstance()Lio/fabric/sdk/android/services/settings/Settings;

    move-result-object v0

    new-instance v1, Lcom/crashlytics/android/core/CrashlyticsCore$6;

    invoke-direct {v1, p0}, Lcom/crashlytics/android/core/CrashlyticsCore$6;-><init>(Lcom/crashlytics/android/core/CrashlyticsCore;)V

    const/4 v2, 0x1

    invoke-static {v2}, Ljava/lang/Boolean;->valueOf(Z)Ljava/lang/Boolean;

    move-result-object v2

    invoke-virtual {v0, v1, v2}, Lio/fabric/sdk/android/services/settings/Settings;->withSettings(Lio/fabric/sdk/android/services/settings/Settings$SettingsAccess;Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Ljava/lang/Boolean;

    invoke-virtual {v0}, Ljava/lang/Boolean;->booleanValue()Z

    move-result v0

    return v0
.end method

.method public crash()V
    .locals 1

    .line 628
    new-instance v0, Lcom/crashlytics/android/core/CrashTest;

    invoke-direct {v0}, Lcom/crashlytics/android/core/CrashTest;-><init>()V

    invoke-virtual {v0}, Lcom/crashlytics/android/core/CrashTest;->indexOutOfBounds()V

    return-void
.end method

.method didPreviousInitializationComplete()Z
    .locals 2

    .line 828
    iget-object v0, p0, Lcom/crashlytics/android/core/CrashlyticsCore;->executorServiceWrapper:Lcom/crashlytics/android/core/CrashlyticsExecutorServiceWrapper;

    new-instance v1, Lcom/crashlytics/android/core/CrashlyticsCore$4;

    invoke-direct {v1, p0}, Lcom/crashlytics/android/core/CrashlyticsCore$4;-><init>(Lcom/crashlytics/android/core/CrashlyticsCore;)V

    invoke-virtual {v0, v1}, Lcom/crashlytics/android/core/CrashlyticsExecutorServiceWrapper;->executeSyncLoggingException(Ljava/util/concurrent/Callable;)Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Ljava/lang/Boolean;

    invoke-virtual {v0}, Ljava/lang/Boolean;->booleanValue()Z

    move-result v0

    return v0
.end method

.method protected bridge synthetic doInBackground()Ljava/lang/Object;
    .locals 1

    .line 59
    invoke-virtual {p0}, Lcom/crashlytics/android/core/CrashlyticsCore;->doInBackground()Ljava/lang/Void;

    move-result-object v0

    return-object v0
.end method

.method protected doInBackground()Ljava/lang/Void;
    .locals 6

    .line 340
    invoke-virtual {p0}, Lcom/crashlytics/android/core/CrashlyticsCore;->markInitializationStarted()V

    .line 341
    iget-object v0, p0, Lcom/crashlytics/android/core/CrashlyticsCore;->handler:Lcom/crashlytics/android/core/CrashlyticsUncaughtExceptionHandler;

    invoke-virtual {v0}, Lcom/crashlytics/android/core/CrashlyticsUncaughtExceptionHandler;->cleanInvalidTempFiles()V

    const/4 v0, 0x1

    const/4 v1, 0x0

    .line 347
    :try_start_0
    invoke-static {}, Lio/fabric/sdk/android/services/settings/Settings;->getInstance()Lio/fabric/sdk/android/services/settings/Settings;

    move-result-object v2

    invoke-virtual {v2}, Lio/fabric/sdk/android/services/settings/Settings;->awaitSettingsData()Lio/fabric/sdk/android/services/settings/SettingsData;

    move-result-object v2

    if-nez v2, :cond_0

    .line 350
    invoke-static {}, Lio/fabric/sdk/android/Fabric;->getLogger()Lio/fabric/sdk/android/Logger;

    move-result-object v2

    const-string v3, "Fabric"

    const-string v4, "Received null settings, skipping initialization!"

    invoke-interface {v2, v3, v4}, Lio/fabric/sdk/android/Logger;->w(Ljava/lang/String;Ljava/lang/String;)V
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    .line 384
    invoke-virtual {p0}, Lcom/crashlytics/android/core/CrashlyticsCore;->markInitializationComplete()V

    return-object v1

    .line 354
    :cond_0
    :try_start_1
    iget-object v3, v2, Lio/fabric/sdk/android/services/settings/SettingsData;->featuresData:Lio/fabric/sdk/android/services/settings/FeaturesSettingsData;

    iget-boolean v3, v3, Lio/fabric/sdk/android/services/settings/FeaturesSettingsData;->collectReports:Z

    if-eqz v3, :cond_2

    const/4 v0, 0x0

    .line 356
    iget-object v3, p0, Lcom/crashlytics/android/core/CrashlyticsCore;->handler:Lcom/crashlytics/android/core/CrashlyticsUncaughtExceptionHandler;

    invoke-virtual {v3}, Lcom/crashlytics/android/core/CrashlyticsUncaughtExceptionHandler;->finalizeSessions()Z

    .line 358
    invoke-virtual {p0, v2}, Lcom/crashlytics/android/core/CrashlyticsCore;->getCreateReportSpiCall(Lio/fabric/sdk/android/services/settings/SettingsData;)Lcom/crashlytics/android/core/CreateReportSpiCall;

    move-result-object v2

    if-eqz v2, :cond_1

    .line 360
    new-instance v3, Lcom/crashlytics/android/core/ReportUploader;

    invoke-direct {v3, v2}, Lcom/crashlytics/android/core/ReportUploader;-><init>(Lcom/crashlytics/android/core/CreateReportSpiCall;)V

    iget v2, p0, Lcom/crashlytics/android/core/CrashlyticsCore;->delay:F

    invoke-virtual {v3, v2}, Lcom/crashlytics/android/core/ReportUploader;->uploadReports(F)V

    goto :goto_0

    .line 362
    :cond_1
    invoke-static {}, Lio/fabric/sdk/android/Fabric;->getLogger()Lio/fabric/sdk/android/Logger;

    move-result-object v2

    const-string v3, "Fabric"

    const-string v4, "Unable to create a call to upload reports."

    invoke-interface {v2, v3, v4}, Lio/fabric/sdk/android/Logger;->w(Ljava/lang/String;Ljava/lang/String;)V
    :try_end_1
    .catch Ljava/lang/Exception; {:try_start_1 .. :try_end_1} :catch_0
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    goto :goto_0

    :catchall_0
    move-exception v0

    goto :goto_3

    :catch_0
    move-exception v2

    .line 366
    :try_start_2
    invoke-static {}, Lio/fabric/sdk/android/Fabric;->getLogger()Lio/fabric/sdk/android/Logger;

    move-result-object v3

    const-string v4, "Fabric"

    const-string v5, "Error dealing with settings"

    invoke-interface {v3, v4, v5, v2}, Lio/fabric/sdk/android/Logger;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)V

    :cond_2
    :goto_0
    if-eqz v0, :cond_3

    .line 370
    invoke-static {}, Lio/fabric/sdk/android/Fabric;->getLogger()Lio/fabric/sdk/android/Logger;

    move-result-object v0

    const-string v2, "Fabric"

    const-string v3, "Crash reporting disabled."

    invoke-interface {v0, v2, v3}, Lio/fabric/sdk/android/Logger;->d(Ljava/lang/String;Ljava/lang/String;)V
    :try_end_2
    .catch Ljava/lang/Exception; {:try_start_2 .. :try_end_2} :catch_1
    .catchall {:try_start_2 .. :try_end_2} :catchall_0

    .line 384
    :cond_3
    :goto_1
    invoke-virtual {p0}, Lcom/crashlytics/android/core/CrashlyticsCore;->markInitializationComplete()V

    goto :goto_2

    :catch_1
    move-exception v0

    .line 373
    :try_start_3
    invoke-static {}, Lio/fabric/sdk/android/Fabric;->getLogger()Lio/fabric/sdk/android/Logger;

    move-result-object v2

    const-string v3, "Fabric"

    const-string v4, "Problem encountered during Crashlytics initialization."

    invoke-interface {v2, v3, v4, v0}, Lio/fabric/sdk/android/Logger;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)V
    :try_end_3
    .catchall {:try_start_3 .. :try_end_3} :catchall_0

    goto :goto_1

    :goto_2
    return-object v1

    .line 384
    :goto_3
    invoke-virtual {p0}, Lcom/crashlytics/android/core/CrashlyticsCore;->markInitializationComplete()V

    throw v0
.end method

.method getAttributes()Ljava/util/Map;
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()",
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            ">;"
        }
    .end annotation

    .line 733
    iget-object v0, p0, Lcom/crashlytics/android/core/CrashlyticsCore;->attributes:Ljava/util/concurrent/ConcurrentHashMap;

    invoke-static {v0}, Ljava/util/Collections;->unmodifiableMap(Ljava/util/Map;)Ljava/util/Map;

    move-result-object v0

    return-object v0
.end method

.method getBuildId()Ljava/lang/String;
    .locals 1

    .line 767
    iget-object v0, p0, Lcom/crashlytics/android/core/CrashlyticsCore;->buildId:Ljava/lang/String;

    return-object v0
.end method

.method getBuildIdValidator(Ljava/lang/String;Z)Lcom/crashlytics/android/core/BuildIdValidator;
    .locals 1

    .line 737
    new-instance v0, Lcom/crashlytics/android/core/BuildIdValidator;

    invoke-direct {v0, p1, p2}, Lcom/crashlytics/android/core/BuildIdValidator;-><init>(Ljava/lang/String;Z)V

    return-object v0
.end method

.method getCreateReportSpiCall(Lio/fabric/sdk/android/services/settings/SettingsData;)Lcom/crashlytics/android/core/CreateReportSpiCall;
    .locals 3

    if-eqz p1, :cond_0

    .line 925
    new-instance v0, Lcom/crashlytics/android/core/DefaultCreateReportSpiCall;

    invoke-virtual {p0}, Lcom/crashlytics/android/core/CrashlyticsCore;->getOverridenSpiEndpoint()Ljava/lang/String;

    move-result-object v1

    iget-object p1, p1, Lio/fabric/sdk/android/services/settings/SettingsData;->appData:Lio/fabric/sdk/android/services/settings/AppSettingsData;

    iget-object p1, p1, Lio/fabric/sdk/android/services/settings/AppSettingsData;->reportsUrl:Ljava/lang/String;

    iget-object v2, p0, Lcom/crashlytics/android/core/CrashlyticsCore;->httpRequestFactory:Lio/fabric/sdk/android/services/network/HttpRequestFactory;

    invoke-direct {v0, p0, v1, p1, v2}, Lcom/crashlytics/android/core/DefaultCreateReportSpiCall;-><init>(Lio/fabric/sdk/android/Kit;Ljava/lang/String;Ljava/lang/String;Lio/fabric/sdk/android/services/network/HttpRequestFactory;)V

    return-object v0

    :cond_0
    const/4 p1, 0x0

    return-object p1
.end method

.method getExternalCrashEventData()Lcom/crashlytics/android/core/internal/models/SessionEventData;
    .locals 1

    .line 850
    iget-object v0, p0, Lcom/crashlytics/android/core/CrashlyticsCore;->externalCrashEventDataProvider:Lcom/crashlytics/android/core/internal/CrashEventDataProvider;

    if-eqz v0, :cond_0

    .line 851
    iget-object v0, p0, Lcom/crashlytics/android/core/CrashlyticsCore;->externalCrashEventDataProvider:Lcom/crashlytics/android/core/internal/CrashEventDataProvider;

    invoke-interface {v0}, Lcom/crashlytics/android/core/internal/CrashEventDataProvider;->getCrashEventData()Lcom/crashlytics/android/core/internal/models/SessionEventData;

    move-result-object v0

    goto :goto_0

    :cond_0
    const/4 v0, 0x0

    :goto_0
    return-object v0
.end method

.method getHandler()Lcom/crashlytics/android/core/CrashlyticsUncaughtExceptionHandler;
    .locals 1

    .line 771
    iget-object v0, p0, Lcom/crashlytics/android/core/CrashlyticsCore;->handler:Lcom/crashlytics/android/core/CrashlyticsUncaughtExceptionHandler;

    return-object v0
.end method

.method public getIdentifier()Ljava/lang/String;
    .locals 1

    const-string v0, "com.crashlytics.sdk.android.crashlytics-core"

    return-object v0
.end method

.method getInstallerPackageName()Ljava/lang/String;
    .locals 1

    .line 751
    iget-object v0, p0, Lcom/crashlytics/android/core/CrashlyticsCore;->installerPackageName:Ljava/lang/String;

    return-object v0
.end method

.method getOverridenSpiEndpoint()Ljava/lang/String;
    .locals 2

    .line 763
    invoke-virtual {p0}, Lcom/crashlytics/android/core/CrashlyticsCore;->getContext()Landroid/content/Context;

    move-result-object v0

    const-string v1, "com.crashlytics.ApiEndpoint"

    invoke-static {v0, v1}, Lio/fabric/sdk/android/services/common/CommonUtils;->getStringsFileValue(Landroid/content/Context;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v0

    return-object v0
.end method

.method getPackageName()Ljava/lang/String;
    .locals 1

    .line 741
    iget-object v0, p0, Lcom/crashlytics/android/core/CrashlyticsCore;->packageName:Ljava/lang/String;

    return-object v0
.end method

.method public getPinningInfoProvider()Lcom/crashlytics/android/core/PinningInfoProvider;
    .locals 1

    .line 410
    iget-boolean v0, p0, Lcom/crashlytics/android/core/CrashlyticsCore;->disabled:Z

    if-nez v0, :cond_0

    iget-object v0, p0, Lcom/crashlytics/android/core/CrashlyticsCore;->pinningInfo:Lcom/crashlytics/android/core/PinningInfoProvider;

    goto :goto_0

    :cond_0
    const/4 v0, 0x0

    :goto_0
    return-object v0
.end method

.method getSdkDirectory()Ljava/io/File;
    .locals 1

    .line 875
    new-instance v0, Lio/fabric/sdk/android/services/persistence/FileStoreImpl;

    invoke-direct {v0, p0}, Lio/fabric/sdk/android/services/persistence/FileStoreImpl;-><init>(Lio/fabric/sdk/android/Kit;)V

    invoke-virtual {v0}, Lio/fabric/sdk/android/services/persistence/FileStoreImpl;->getFilesDir()Ljava/io/File;

    move-result-object v0

    return-object v0
.end method

.method getSessionSettingsData()Lio/fabric/sdk/android/services/settings/SessionSettingsData;
    .locals 1

    .line 1016
    invoke-static {}, Lio/fabric/sdk/android/services/settings/Settings;->getInstance()Lio/fabric/sdk/android/services/settings/Settings;

    move-result-object v0

    invoke-virtual {v0}, Lio/fabric/sdk/android/services/settings/Settings;->awaitSettingsData()Lio/fabric/sdk/android/services/settings/SettingsData;

    move-result-object v0

    if-nez v0, :cond_0

    const/4 v0, 0x0

    goto :goto_0

    .line 1017
    :cond_0
    iget-object v0, v0, Lio/fabric/sdk/android/services/settings/SettingsData;->sessionData:Lio/fabric/sdk/android/services/settings/SessionSettingsData;

    :goto_0
    return-object v0
.end method

.method getUserEmail()Ljava/lang/String;
    .locals 1

    .line 779
    invoke-virtual {p0}, Lcom/crashlytics/android/core/CrashlyticsCore;->getIdManager()Lio/fabric/sdk/android/services/common/IdManager;

    move-result-object v0

    invoke-virtual {v0}, Lio/fabric/sdk/android/services/common/IdManager;->canCollectUserIds()Z

    move-result v0

    if-eqz v0, :cond_0

    iget-object v0, p0, Lcom/crashlytics/android/core/CrashlyticsCore;->userEmail:Ljava/lang/String;

    goto :goto_0

    :cond_0
    const/4 v0, 0x0

    :goto_0
    return-object v0
.end method

.method getUserIdentifier()Ljava/lang/String;
    .locals 1

    .line 775
    invoke-virtual {p0}, Lcom/crashlytics/android/core/CrashlyticsCore;->getIdManager()Lio/fabric/sdk/android/services/common/IdManager;

    move-result-object v0

    invoke-virtual {v0}, Lio/fabric/sdk/android/services/common/IdManager;->canCollectUserIds()Z

    move-result v0

    if-eqz v0, :cond_0

    iget-object v0, p0, Lcom/crashlytics/android/core/CrashlyticsCore;->userId:Ljava/lang/String;

    goto :goto_0

    :cond_0
    const/4 v0, 0x0

    :goto_0
    return-object v0
.end method

.method getUserName()Ljava/lang/String;
    .locals 1

    .line 783
    invoke-virtual {p0}, Lcom/crashlytics/android/core/CrashlyticsCore;->getIdManager()Lio/fabric/sdk/android/services/common/IdManager;

    move-result-object v0

    invoke-virtual {v0}, Lio/fabric/sdk/android/services/common/IdManager;->canCollectUserIds()Z

    move-result v0

    if-eqz v0, :cond_0

    iget-object v0, p0, Lcom/crashlytics/android/core/CrashlyticsCore;->userName:Ljava/lang/String;

    goto :goto_0

    :cond_0
    const/4 v0, 0x0

    :goto_0
    return-object v0
.end method

.method public getVersion()Ljava/lang/String;
    .locals 1

    const-string v0, "2.3.3.61"

    return-object v0
.end method

.method getVersionCode()Ljava/lang/String;
    .locals 1

    .line 759
    iget-object v0, p0, Lcom/crashlytics/android/core/CrashlyticsCore;->versionCode:Ljava/lang/String;

    return-object v0
.end method

.method getVersionName()Ljava/lang/String;
    .locals 1

    .line 755
    iget-object v0, p0, Lcom/crashlytics/android/core/CrashlyticsCore;->versionName:Ljava/lang/String;

    return-object v0
.end method

.method internalVerifyPinning(Ljava/net/URL;)Z
    .locals 3

    .line 861
    invoke-virtual {p0}, Lcom/crashlytics/android/core/CrashlyticsCore;->getPinningInfoProvider()Lcom/crashlytics/android/core/PinningInfoProvider;

    move-result-object v0

    const/4 v1, 0x0

    if-eqz v0, :cond_0

    .line 862
    iget-object v0, p0, Lcom/crashlytics/android/core/CrashlyticsCore;->httpRequestFactory:Lio/fabric/sdk/android/services/network/HttpRequestFactory;

    sget-object v2, Lio/fabric/sdk/android/services/network/HttpMethod;->GET:Lio/fabric/sdk/android/services/network/HttpMethod;

    invoke-virtual {p1}, Ljava/net/URL;->toString()Ljava/lang/String;

    move-result-object p1

    invoke-interface {v0, v2, p1}, Lio/fabric/sdk/android/services/network/HttpRequestFactory;->buildHttpRequest(Lio/fabric/sdk/android/services/network/HttpMethod;Ljava/lang/String;)Lio/fabric/sdk/android/services/network/HttpRequest;

    move-result-object p1

    .line 864
    invoke-virtual {p1}, Lio/fabric/sdk/android/services/network/HttpRequest;->getConnection()Ljava/net/HttpURLConnection;

    move-result-object v0

    check-cast v0, Ljavax/net/ssl/HttpsURLConnection;

    .line 865
    invoke-virtual {v0, v1}, Ljavax/net/ssl/HttpsURLConnection;->setInstanceFollowRedirects(Z)V

    .line 868
    invoke-virtual {p1}, Lio/fabric/sdk/android/services/network/HttpRequest;->code()I

    const/4 p1, 0x1

    return p1

    :cond_0
    return v1
.end method

.method public log(ILjava/lang/String;Ljava/lang/String;)V
    .locals 3

    .line 476
    invoke-direct {p0, p1, p2, p3}, Lcom/crashlytics/android/core/CrashlyticsCore;->doLog(ILjava/lang/String;Ljava/lang/String;)V

    .line 479
    invoke-static {}, Lio/fabric/sdk/android/Fabric;->getLogger()Lio/fabric/sdk/android/Logger;

    move-result-object v0

    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, ""

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1, p2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p2

    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, ""

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1, p3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p3

    const/4 v1, 0x1

    invoke-interface {v0, p1, p2, p3, v1}, Lio/fabric/sdk/android/Logger;->log(ILjava/lang/String;Ljava/lang/String;Z)V

    return-void
.end method

.method public log(Ljava/lang/String;)V
    .locals 2

    const-string v0, "Fabric"

    const/4 v1, 0x3

    .line 448
    invoke-direct {p0, v1, v0, p1}, Lcom/crashlytics/android/core/CrashlyticsCore;->doLog(ILjava/lang/String;Ljava/lang/String;)V

    return-void
.end method

.method public logException(Ljava/lang/Throwable;)V
    .locals 3

    .line 422
    iget-boolean v0, p0, Lcom/crashlytics/android/core/CrashlyticsCore;->disabled:Z

    if-eqz v0, :cond_0

    return-void

    :cond_0
    const-string v0, "prior to logging exceptions."

    .line 426
    invoke-static {v0}, Lcom/crashlytics/android/core/CrashlyticsCore;->ensureFabricWithCalled(Ljava/lang/String;)Z

    move-result v0

    if-eqz v0, :cond_2

    if-nez p1, :cond_1

    .line 428
    invoke-static {}, Lio/fabric/sdk/android/Fabric;->getLogger()Lio/fabric/sdk/android/Logger;

    move-result-object p1

    const/4 v0, 0x5

    const-string v1, "Fabric"

    const-string v2, "Crashlytics is ignoring a request to log a null exception."

    invoke-interface {p1, v0, v1, v2}, Lio/fabric/sdk/android/Logger;->log(ILjava/lang/String;Ljava/lang/String;)V

    return-void

    .line 433
    :cond_1
    iget-object v0, p0, Lcom/crashlytics/android/core/CrashlyticsCore;->handler:Lcom/crashlytics/android/core/CrashlyticsUncaughtExceptionHandler;

    invoke-static {}, Ljava/lang/Thread;->currentThread()Ljava/lang/Thread;

    move-result-object v1

    invoke-virtual {v0, v1, p1}, Lcom/crashlytics/android/core/CrashlyticsUncaughtExceptionHandler;->writeNonFatalException(Ljava/lang/Thread;Ljava/lang/Throwable;)V

    :cond_2
    return-void
.end method

.method markInitializationComplete()V
    .locals 2

    .line 810
    iget-object v0, p0, Lcom/crashlytics/android/core/CrashlyticsCore;->executorServiceWrapper:Lcom/crashlytics/android/core/CrashlyticsExecutorServiceWrapper;

    new-instance v1, Lcom/crashlytics/android/core/CrashlyticsCore$3;

    invoke-direct {v1, p0}, Lcom/crashlytics/android/core/CrashlyticsCore$3;-><init>(Lcom/crashlytics/android/core/CrashlyticsCore;)V

    invoke-virtual {v0, v1}, Lcom/crashlytics/android/core/CrashlyticsExecutorServiceWrapper;->executeAsync(Ljava/util/concurrent/Callable;)Ljava/util/concurrent/Future;

    return-void
.end method

.method markInitializationStarted()V
    .locals 2

    .line 794
    iget-object v0, p0, Lcom/crashlytics/android/core/CrashlyticsCore;->executorServiceWrapper:Lcom/crashlytics/android/core/CrashlyticsExecutorServiceWrapper;

    new-instance v1, Lcom/crashlytics/android/core/CrashlyticsCore$2;

    invoke-direct {v1, p0}, Lcom/crashlytics/android/core/CrashlyticsCore$2;-><init>(Lcom/crashlytics/android/core/CrashlyticsCore;)V

    invoke-virtual {v0, v1}, Lcom/crashlytics/android/core/CrashlyticsExecutorServiceWrapper;->executeSyncLoggingException(Ljava/util/concurrent/Callable;)Ljava/lang/Object;

    return-void
.end method

.method protected onPreExecute()Z
    .locals 1

    .line 227
    invoke-super {p0}, Lio/fabric/sdk/android/Kit;->getContext()Landroid/content/Context;

    move-result-object v0

    .line 228
    invoke-virtual {p0, v0}, Lcom/crashlytics/android/core/CrashlyticsCore;->onPreExecute(Landroid/content/Context;)Z

    move-result v0

    return v0
.end method

.method onPreExecute(Landroid/content/Context;)Z
    .locals 12

    .line 235
    iget-boolean v0, p0, Lcom/crashlytics/android/core/CrashlyticsCore;->disabled:Z

    const/4 v1, 0x0

    if-eqz v0, :cond_0

    return v1

    .line 240
    :cond_0
    new-instance v0, Lio/fabric/sdk/android/services/common/ApiKey;

    invoke-direct {v0}, Lio/fabric/sdk/android/services/common/ApiKey;-><init>()V

    invoke-virtual {v0, p1}, Lio/fabric/sdk/android/services/common/ApiKey;->getValue(Landroid/content/Context;)Ljava/lang/String;

    move-result-object v0

    if-nez v0, :cond_1

    return v1

    .line 246
    :cond_1
    invoke-static {}, Lio/fabric/sdk/android/Fabric;->getLogger()Lio/fabric/sdk/android/Logger;

    move-result-object v2

    const-string v3, "Fabric"

    new-instance v4, Ljava/lang/StringBuilder;

    invoke-direct {v4}, Ljava/lang/StringBuilder;-><init>()V

    const-string v5, "Initializing Crashlytics "

    invoke-virtual {v4, v5}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p0}, Lcom/crashlytics/android/core/CrashlyticsCore;->getVersion()Ljava/lang/String;

    move-result-object v5

    invoke-virtual {v4, v5}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v4}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v4

    invoke-interface {v2, v3, v4}, Lio/fabric/sdk/android/Logger;->i(Ljava/lang/String;Ljava/lang/String;)V

    .line 247
    new-instance v2, Ljava/io/File;

    invoke-virtual {p0}, Lcom/crashlytics/android/core/CrashlyticsCore;->getSdkDirectory()Ljava/io/File;

    move-result-object v3

    const-string v4, "initialization_marker"

    invoke-direct {v2, v3, v4}, Ljava/io/File;-><init>(Ljava/io/File;Ljava/lang/String;)V

    iput-object v2, p0, Lcom/crashlytics/android/core/CrashlyticsCore;->initializationMarkerFile:Ljava/io/File;

    .line 252
    :try_start_0
    invoke-direct {p0, p1, v0}, Lcom/crashlytics/android/core/CrashlyticsCore;->setAndValidateKitProperties(Landroid/content/Context;Ljava/lang/String;)V
    :try_end_0
    .catch Lcom/crashlytics/android/core/CrashlyticsMissingDependencyException; {:try_start_0 .. :try_end_0} :catch_3
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_2

    .line 255
    :try_start_1
    new-instance v10, Lcom/crashlytics/android/core/SessionDataWriter;

    invoke-virtual {p0}, Lcom/crashlytics/android/core/CrashlyticsCore;->getContext()Landroid/content/Context;

    move-result-object v0

    iget-object v2, p0, Lcom/crashlytics/android/core/CrashlyticsCore;->buildId:Ljava/lang/String;

    invoke-virtual {p0}, Lcom/crashlytics/android/core/CrashlyticsCore;->getPackageName()Ljava/lang/String;

    move-result-object v3

    invoke-direct {v10, v0, v2, v3}, Lcom/crashlytics/android/core/SessionDataWriter;-><init>(Landroid/content/Context;Ljava/lang/String;Ljava/lang/String;)V

    .line 257
    invoke-static {}, Lio/fabric/sdk/android/Fabric;->getLogger()Lio/fabric/sdk/android/Logger;

    move-result-object v0

    const-string v2, "Fabric"

    const-string v3, "Installing exception handler..."

    invoke-interface {v0, v2, v3}, Lio/fabric/sdk/android/Logger;->d(Ljava/lang/String;Ljava/lang/String;)V

    .line 258
    new-instance v0, Lcom/crashlytics/android/core/CrashlyticsUncaughtExceptionHandler;

    invoke-static {}, Ljava/lang/Thread;->getDefaultUncaughtExceptionHandler()Ljava/lang/Thread$UncaughtExceptionHandler;

    move-result-object v6

    iget-object v7, p0, Lcom/crashlytics/android/core/CrashlyticsCore;->listener:Lcom/crashlytics/android/core/CrashlyticsListener;

    iget-object v8, p0, Lcom/crashlytics/android/core/CrashlyticsCore;->executorServiceWrapper:Lcom/crashlytics/android/core/CrashlyticsExecutorServiceWrapper;

    invoke-virtual {p0}, Lcom/crashlytics/android/core/CrashlyticsCore;->getIdManager()Lio/fabric/sdk/android/services/common/IdManager;

    move-result-object v9

    move-object v5, v0

    move-object v11, p0

    invoke-direct/range {v5 .. v11}, Lcom/crashlytics/android/core/CrashlyticsUncaughtExceptionHandler;-><init>(Ljava/lang/Thread$UncaughtExceptionHandler;Lcom/crashlytics/android/core/CrashlyticsListener;Lcom/crashlytics/android/core/CrashlyticsExecutorServiceWrapper;Lio/fabric/sdk/android/services/common/IdManager;Lcom/crashlytics/android/core/SessionDataWriter;Lcom/crashlytics/android/core/CrashlyticsCore;)V

    iput-object v0, p0, Lcom/crashlytics/android/core/CrashlyticsCore;->handler:Lcom/crashlytics/android/core/CrashlyticsUncaughtExceptionHandler;

    .line 269
    invoke-virtual {p0}, Lcom/crashlytics/android/core/CrashlyticsCore;->didPreviousInitializationComplete()Z

    move-result v0
    :try_end_1
    .catch Ljava/lang/Exception; {:try_start_1 .. :try_end_1} :catch_1
    .catch Lcom/crashlytics/android/core/CrashlyticsMissingDependencyException; {:try_start_1 .. :try_end_1} :catch_3

    .line 274
    :try_start_2
    iget-object v2, p0, Lcom/crashlytics/android/core/CrashlyticsCore;->handler:Lcom/crashlytics/android/core/CrashlyticsUncaughtExceptionHandler;

    invoke-virtual {v2}, Lcom/crashlytics/android/core/CrashlyticsUncaughtExceptionHandler;->ensureOpenSessionExists()V

    .line 276
    iget-object v2, p0, Lcom/crashlytics/android/core/CrashlyticsCore;->handler:Lcom/crashlytics/android/core/CrashlyticsUncaughtExceptionHandler;

    invoke-static {v2}, Ljava/lang/Thread;->setDefaultUncaughtExceptionHandler(Ljava/lang/Thread$UncaughtExceptionHandler;)V

    .line 277
    invoke-static {}, Lio/fabric/sdk/android/Fabric;->getLogger()Lio/fabric/sdk/android/Logger;

    move-result-object v2

    const-string v3, "Fabric"

    const-string v4, "Successfully installed exception handler."

    invoke-interface {v2, v3, v4}, Lio/fabric/sdk/android/Logger;->d(Ljava/lang/String;Ljava/lang/String;)V
    :try_end_2
    .catch Ljava/lang/Exception; {:try_start_2 .. :try_end_2} :catch_0
    .catch Lcom/crashlytics/android/core/CrashlyticsMissingDependencyException; {:try_start_2 .. :try_end_2} :catch_3

    goto :goto_1

    :catch_0
    move-exception v2

    goto :goto_0

    :catch_1
    move-exception v2

    const/4 v0, 0x0

    .line 280
    :goto_0
    :try_start_3
    invoke-static {}, Lio/fabric/sdk/android/Fabric;->getLogger()Lio/fabric/sdk/android/Logger;

    move-result-object v3

    const-string v4, "Fabric"

    const-string v5, "There was a problem installing the exception handler."

    invoke-interface {v3, v4, v5, v2}, Lio/fabric/sdk/android/Logger;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)V

    :goto_1
    if-eqz v0, :cond_2

    .line 284
    invoke-static {p1}, Lio/fabric/sdk/android/services/common/CommonUtils;->canTryConnection(Landroid/content/Context;)Z

    move-result p1

    if-eqz p1, :cond_2

    .line 287
    invoke-direct {p0}, Lcom/crashlytics/android/core/CrashlyticsCore;->finishInitSynchronously()V
    :try_end_3
    .catch Lcom/crashlytics/android/core/CrashlyticsMissingDependencyException; {:try_start_3 .. :try_end_3} :catch_3
    .catch Ljava/lang/Exception; {:try_start_3 .. :try_end_3} :catch_2

    return v1

    :cond_2
    const/4 p1, 0x1

    return p1

    :catch_2
    move-exception p1

    .line 296
    invoke-static {}, Lio/fabric/sdk/android/Fabric;->getLogger()Lio/fabric/sdk/android/Logger;

    move-result-object v0

    const-string v2, "Fabric"

    const-string v3, "Crashlytics was not started due to an exception during initialization"

    invoke-interface {v0, v2, v3, p1}, Lio/fabric/sdk/android/Logger;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)V

    return v1

    :catch_3
    move-exception p1

    .line 294
    new-instance v0, Lio/fabric/sdk/android/services/concurrency/UnmetDependencyException;

    invoke-direct {v0, p1}, Lio/fabric/sdk/android/services/concurrency/UnmetDependencyException;-><init>(Ljava/lang/Throwable;)V

    throw v0
.end method

.method public setBool(Ljava/lang/String;Z)V
    .locals 0

    .line 582
    invoke-static {p2}, Ljava/lang/Boolean;->toString(Z)Ljava/lang/String;

    move-result-object p2

    invoke-virtual {p0, p1, p2}, Lcom/crashlytics/android/core/CrashlyticsCore;->setString(Ljava/lang/String;Ljava/lang/String;)V

    return-void
.end method

.method public setDouble(Ljava/lang/String;D)V
    .locals 0

    .line 591
    invoke-static {p2, p3}, Ljava/lang/Double;->toString(D)Ljava/lang/String;

    move-result-object p2

    invoke-virtual {p0, p1, p2}, Lcom/crashlytics/android/core/CrashlyticsCore;->setString(Ljava/lang/String;Ljava/lang/String;)V

    return-void
.end method

.method setExternalCrashEventDataProvider(Lcom/crashlytics/android/core/internal/CrashEventDataProvider;)V
    .locals 0

    .line 841
    iput-object p1, p0, Lcom/crashlytics/android/core/CrashlyticsCore;->externalCrashEventDataProvider:Lcom/crashlytics/android/core/internal/CrashEventDataProvider;

    return-void
.end method

.method public setFloat(Ljava/lang/String;F)V
    .locals 0

    .line 600
    invoke-static {p2}, Ljava/lang/Float;->toString(F)Ljava/lang/String;

    move-result-object p2

    invoke-virtual {p0, p1, p2}, Lcom/crashlytics/android/core/CrashlyticsCore;->setString(Ljava/lang/String;Ljava/lang/String;)V

    return-void
.end method

.method public setInt(Ljava/lang/String;I)V
    .locals 0

    .line 609
    invoke-static {p2}, Ljava/lang/Integer;->toString(I)Ljava/lang/String;

    move-result-object p2

    invoke-virtual {p0, p1, p2}, Lcom/crashlytics/android/core/CrashlyticsCore;->setString(Ljava/lang/String;Ljava/lang/String;)V

    return-void
.end method

.method public declared-synchronized setListener(Lcom/crashlytics/android/core/CrashlyticsListener;)V
    .locals 3
    .annotation runtime Ljava/lang/Deprecated;
    .end annotation

    monitor-enter p0

    .line 694
    :try_start_0
    invoke-static {}, Lio/fabric/sdk/android/Fabric;->getLogger()Lio/fabric/sdk/android/Logger;

    move-result-object v0

    const-string v1, "Fabric"

    const-string v2, "Use of setListener is deprecated."

    invoke-interface {v0, v1, v2}, Lio/fabric/sdk/android/Logger;->w(Ljava/lang/String;Ljava/lang/String;)V

    if-eqz p1, :cond_0

    .line 698
    iput-object p1, p0, Lcom/crashlytics/android/core/CrashlyticsCore;->listener:Lcom/crashlytics/android/core/CrashlyticsListener;
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    .line 699
    monitor-exit p0

    return-void

    .line 696
    :cond_0
    :try_start_1
    new-instance p1, Ljava/lang/IllegalArgumentException;

    const-string v0, "listener must not be null."

    invoke-direct {p1, v0}, Ljava/lang/IllegalArgumentException;-><init>(Ljava/lang/String;)V

    throw p1
    :try_end_1
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    :catchall_0
    move-exception p1

    .line 693
    monitor-exit p0

    throw p1
.end method

.method public setLong(Ljava/lang/String;J)V
    .locals 0

    .line 618
    invoke-static {p2, p3}, Ljava/lang/Long;->toString(J)Ljava/lang/String;

    move-result-object p2

    invoke-virtual {p0, p1, p2}, Lcom/crashlytics/android/core/CrashlyticsCore;->setString(Ljava/lang/String;Ljava/lang/String;)V

    return-void
.end method

.method setShouldSendUserReportsWithoutPrompting(Z)V
    .locals 3
    .annotation build Landroid/annotation/SuppressLint;
        value = {
            "CommitPrefEdits"
        }
    .end annotation

    .line 898
    new-instance v0, Lio/fabric/sdk/android/services/persistence/PreferenceStoreImpl;

    invoke-direct {v0, p0}, Lio/fabric/sdk/android/services/persistence/PreferenceStoreImpl;-><init>(Lio/fabric/sdk/android/Kit;)V

    .line 899
    invoke-interface {v0}, Lio/fabric/sdk/android/services/persistence/PreferenceStore;->edit()Landroid/content/SharedPreferences$Editor;

    move-result-object v1

    const-string v2, "always_send_reports_opt_in"

    invoke-interface {v1, v2, p1}, Landroid/content/SharedPreferences$Editor;->putBoolean(Ljava/lang/String;Z)Landroid/content/SharedPreferences$Editor;

    move-result-object p1

    invoke-interface {v0, p1}, Lio/fabric/sdk/android/services/persistence/PreferenceStore;->save(Landroid/content/SharedPreferences$Editor;)Z

    return-void
.end method

.method public setString(Ljava/lang/String;Ljava/lang/String;)V
    .locals 2

    .line 549
    iget-boolean v0, p0, Lcom/crashlytics/android/core/CrashlyticsCore;->disabled:Z

    if-eqz v0, :cond_0

    return-void

    :cond_0
    if-nez p1, :cond_3

    .line 554
    invoke-virtual {p0}, Lcom/crashlytics/android/core/CrashlyticsCore;->getContext()Landroid/content/Context;

    move-result-object p1

    if-eqz p1, :cond_2

    invoke-virtual {p0}, Lcom/crashlytics/android/core/CrashlyticsCore;->getContext()Landroid/content/Context;

    move-result-object p1

    invoke-static {p1}, Lio/fabric/sdk/android/services/common/CommonUtils;->isAppDebuggable(Landroid/content/Context;)Z

    move-result p1

    if-nez p1, :cond_1

    goto :goto_0

    .line 556
    :cond_1
    new-instance p1, Ljava/lang/IllegalArgumentException;

    const-string p2, "Custom attribute key must not be null."

    invoke-direct {p1, p2}, Ljava/lang/IllegalArgumentException;-><init>(Ljava/lang/String;)V

    throw p1

    .line 558
    :cond_2
    :goto_0
    invoke-static {}, Lio/fabric/sdk/android/Fabric;->getLogger()Lio/fabric/sdk/android/Logger;

    move-result-object p1

    const-string p2, "Fabric"

    const-string v0, "Attempting to set custom attribute with null key, ignoring."

    const/4 v1, 0x0

    invoke-interface {p1, p2, v0, v1}, Lio/fabric/sdk/android/Logger;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)V

    return-void

    .line 563
    :cond_3
    invoke-static {p1}, Lcom/crashlytics/android/core/CrashlyticsCore;->sanitizeAttribute(Ljava/lang/String;)Ljava/lang/String;

    move-result-object p1

    .line 564
    iget-object v0, p0, Lcom/crashlytics/android/core/CrashlyticsCore;->attributes:Ljava/util/concurrent/ConcurrentHashMap;

    invoke-virtual {v0}, Ljava/util/concurrent/ConcurrentHashMap;->size()I

    move-result v0

    const/16 v1, 0x40

    if-lt v0, v1, :cond_5

    iget-object v0, p0, Lcom/crashlytics/android/core/CrashlyticsCore;->attributes:Ljava/util/concurrent/ConcurrentHashMap;

    invoke-virtual {v0, p1}, Ljava/util/concurrent/ConcurrentHashMap;->containsKey(Ljava/lang/Object;)Z

    move-result v0

    if-eqz v0, :cond_4

    goto :goto_1

    .line 571
    :cond_4
    invoke-static {}, Lio/fabric/sdk/android/Fabric;->getLogger()Lio/fabric/sdk/android/Logger;

    move-result-object p1

    const-string p2, "Fabric"

    const-string v0, "Exceeded maximum number of custom attributes (64)"

    invoke-interface {p1, p2, v0}, Lio/fabric/sdk/android/Logger;->d(Ljava/lang/String;Ljava/lang/String;)V

    goto :goto_3

    :cond_5
    :goto_1
    if-nez p2, :cond_6

    const-string p2, ""

    goto :goto_2

    .line 567
    :cond_6
    invoke-static {p2}, Lcom/crashlytics/android/core/CrashlyticsCore;->sanitizeAttribute(Ljava/lang/String;)Ljava/lang/String;

    move-result-object p2

    .line 568
    :goto_2
    iget-object v0, p0, Lcom/crashlytics/android/core/CrashlyticsCore;->attributes:Ljava/util/concurrent/ConcurrentHashMap;

    invoke-virtual {v0, p1, p2}, Ljava/util/concurrent/ConcurrentHashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 569
    iget-object p1, p0, Lcom/crashlytics/android/core/CrashlyticsCore;->handler:Lcom/crashlytics/android/core/CrashlyticsUncaughtExceptionHandler;

    iget-object p2, p0, Lcom/crashlytics/android/core/CrashlyticsCore;->attributes:Ljava/util/concurrent/ConcurrentHashMap;

    invoke-virtual {p1, p2}, Lcom/crashlytics/android/core/CrashlyticsUncaughtExceptionHandler;->cacheKeyData(Ljava/util/Map;)V

    :goto_3
    return-void
.end method

.method public setUserEmail(Ljava/lang/String;)V
    .locals 3

    .line 532
    iget-boolean v0, p0, Lcom/crashlytics/android/core/CrashlyticsCore;->disabled:Z

    if-eqz v0, :cond_0

    return-void

    .line 536
    :cond_0
    invoke-static {p1}, Lcom/crashlytics/android/core/CrashlyticsCore;->sanitizeAttribute(Ljava/lang/String;)Ljava/lang/String;

    move-result-object p1

    iput-object p1, p0, Lcom/crashlytics/android/core/CrashlyticsCore;->userEmail:Ljava/lang/String;

    .line 537
    iget-object p1, p0, Lcom/crashlytics/android/core/CrashlyticsCore;->handler:Lcom/crashlytics/android/core/CrashlyticsUncaughtExceptionHandler;

    iget-object v0, p0, Lcom/crashlytics/android/core/CrashlyticsCore;->userId:Ljava/lang/String;

    iget-object v1, p0, Lcom/crashlytics/android/core/CrashlyticsCore;->userName:Ljava/lang/String;

    iget-object v2, p0, Lcom/crashlytics/android/core/CrashlyticsCore;->userEmail:Ljava/lang/String;

    invoke-virtual {p1, v0, v1, v2}, Lcom/crashlytics/android/core/CrashlyticsUncaughtExceptionHandler;->cacheUserData(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V

    return-void
.end method

.method public setUserIdentifier(Ljava/lang/String;)V
    .locals 3

    .line 504
    iget-boolean v0, p0, Lcom/crashlytics/android/core/CrashlyticsCore;->disabled:Z

    if-eqz v0, :cond_0

    return-void

    .line 508
    :cond_0
    invoke-static {p1}, Lcom/crashlytics/android/core/CrashlyticsCore;->sanitizeAttribute(Ljava/lang/String;)Ljava/lang/String;

    move-result-object p1

    iput-object p1, p0, Lcom/crashlytics/android/core/CrashlyticsCore;->userId:Ljava/lang/String;

    .line 509
    iget-object p1, p0, Lcom/crashlytics/android/core/CrashlyticsCore;->handler:Lcom/crashlytics/android/core/CrashlyticsUncaughtExceptionHandler;

    iget-object v0, p0, Lcom/crashlytics/android/core/CrashlyticsCore;->userId:Ljava/lang/String;

    iget-object v1, p0, Lcom/crashlytics/android/core/CrashlyticsCore;->userName:Ljava/lang/String;

    iget-object v2, p0, Lcom/crashlytics/android/core/CrashlyticsCore;->userEmail:Ljava/lang/String;

    invoke-virtual {p1, v0, v1, v2}, Lcom/crashlytics/android/core/CrashlyticsUncaughtExceptionHandler;->cacheUserData(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V

    return-void
.end method

.method public setUserName(Ljava/lang/String;)V
    .locals 3

    .line 518
    iget-boolean v0, p0, Lcom/crashlytics/android/core/CrashlyticsCore;->disabled:Z

    if-eqz v0, :cond_0

    return-void

    .line 522
    :cond_0
    invoke-static {p1}, Lcom/crashlytics/android/core/CrashlyticsCore;->sanitizeAttribute(Ljava/lang/String;)Ljava/lang/String;

    move-result-object p1

    iput-object p1, p0, Lcom/crashlytics/android/core/CrashlyticsCore;->userName:Ljava/lang/String;

    .line 523
    iget-object p1, p0, Lcom/crashlytics/android/core/CrashlyticsCore;->handler:Lcom/crashlytics/android/core/CrashlyticsUncaughtExceptionHandler;

    iget-object v0, p0, Lcom/crashlytics/android/core/CrashlyticsCore;->userId:Ljava/lang/String;

    iget-object v1, p0, Lcom/crashlytics/android/core/CrashlyticsCore;->userName:Ljava/lang/String;

    iget-object v2, p0, Lcom/crashlytics/android/core/CrashlyticsCore;->userEmail:Ljava/lang/String;

    invoke-virtual {p1, v0, v1, v2}, Lcom/crashlytics/android/core/CrashlyticsUncaughtExceptionHandler;->cacheUserData(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V

    return-void
.end method

.method shouldPromptUserBeforeSendingCrashReports()Z
    .locals 3

    .line 879
    invoke-static {}, Lio/fabric/sdk/android/services/settings/Settings;->getInstance()Lio/fabric/sdk/android/services/settings/Settings;

    move-result-object v0

    new-instance v1, Lcom/crashlytics/android/core/CrashlyticsCore$5;

    invoke-direct {v1, p0}, Lcom/crashlytics/android/core/CrashlyticsCore$5;-><init>(Lcom/crashlytics/android/core/CrashlyticsCore;)V

    const/4 v2, 0x0

    invoke-static {v2}, Ljava/lang/Boolean;->valueOf(Z)Ljava/lang/Boolean;

    move-result-object v2

    invoke-virtual {v0, v1, v2}, Lio/fabric/sdk/android/services/settings/Settings;->withSettings(Lio/fabric/sdk/android/services/settings/Settings$SettingsAccess;Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Ljava/lang/Boolean;

    invoke-virtual {v0}, Ljava/lang/Boolean;->booleanValue()Z

    move-result v0

    return v0
.end method

.method shouldSendReportsWithoutPrompting()Z
    .locals 3

    .line 892
    new-instance v0, Lio/fabric/sdk/android/services/persistence/PreferenceStoreImpl;

    invoke-direct {v0, p0}, Lio/fabric/sdk/android/services/persistence/PreferenceStoreImpl;-><init>(Lio/fabric/sdk/android/Kit;)V

    .line 893
    invoke-interface {v0}, Lio/fabric/sdk/android/services/persistence/PreferenceStore;->get()Landroid/content/SharedPreferences;

    move-result-object v0

    const-string v1, "always_send_reports_opt_in"

    const/4 v2, 0x0

    invoke-interface {v0, v1, v2}, Landroid/content/SharedPreferences;->getBoolean(Ljava/lang/String;Z)Z

    move-result v0

    return v0
.end method

.method public verifyPinning(Ljava/net/URL;)Z
    .locals 3

    .line 641
    :try_start_0
    invoke-virtual {p0, p1}, Lcom/crashlytics/android/core/CrashlyticsCore;->internalVerifyPinning(Ljava/net/URL;)Z

    move-result p1
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    return p1

    :catch_0
    move-exception p1

    .line 643
    invoke-static {}, Lio/fabric/sdk/android/Fabric;->getLogger()Lio/fabric/sdk/android/Logger;

    move-result-object v0

    const-string v1, "Fabric"

    const-string v2, "Could not verify SSL pinning"

    invoke-interface {v0, v1, v2, p1}, Lio/fabric/sdk/android/Logger;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)V

    const/4 p1, 0x0

    return p1
.end method
