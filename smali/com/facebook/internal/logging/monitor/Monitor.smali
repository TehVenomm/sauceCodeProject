.class public Lcom/facebook/internal/logging/monitor/Monitor;
.super Ljava/lang/Object;
.source "Monitor.java"


# static fields
.field private static defaultSamplingRate:Ljava/lang/Integer;

.field private static isEnabled:Z

.field private static final monitorLoggingManager:Lcom/facebook/internal/logging/LoggingManager;

.field private static final random:Ljava/util/Random;

.field private static final samplingRatesMap:Ljava/util/Map;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Ljava/lang/Integer;",
            ">;"
        }
    .end annotation
.end field


# direct methods
.method static constructor <clinit>()V
    .locals 2

    .line 47
    new-instance v0, Ljava/util/Random;

    invoke-direct {v0}, Ljava/util/Random;-><init>()V

    sput-object v0, Lcom/facebook/internal/logging/monitor/Monitor;->random:Ljava/util/Random;

    const/16 v0, 0x3e8

    .line 50
    invoke-static {v0}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v0

    sput-object v0, Lcom/facebook/internal/logging/monitor/Monitor;->defaultSamplingRate:Ljava/lang/Integer;

    .line 54
    invoke-static {}, Lcom/facebook/internal/logging/monitor/MonitorLoggingQueue;->getInstance()Lcom/facebook/internal/logging/monitor/MonitorLoggingQueue;

    move-result-object v0

    invoke-static {}, Lcom/facebook/internal/logging/monitor/MonitorLoggingStore;->getInstance()Lcom/facebook/internal/logging/monitor/MonitorLoggingStore;

    move-result-object v1

    .line 53
    invoke-static {v0, v1}, Lcom/facebook/internal/logging/monitor/MonitorLoggingManager;->getInstance(Lcom/facebook/internal/logging/LoggingCache;Lcom/facebook/internal/logging/LoggingStore;)Lcom/facebook/internal/logging/monitor/MonitorLoggingManager;

    move-result-object v0

    sput-object v0, Lcom/facebook/internal/logging/monitor/Monitor;->monitorLoggingManager:Lcom/facebook/internal/logging/LoggingManager;

    .line 55
    new-instance v0, Ljava/util/HashMap;

    invoke-direct {v0}, Ljava/util/HashMap;-><init>()V

    sput-object v0, Lcom/facebook/internal/logging/monitor/Monitor;->samplingRatesMap:Ljava/util/Map;

    return-void
.end method

.method private constructor <init>()V
    .locals 0

    .line 57
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method

.method public static addLog(Lcom/facebook/internal/logging/ExternalLog;)V
    .locals 1

    .line 138
    sget-boolean v0, Lcom/facebook/internal/logging/monitor/Monitor;->isEnabled:Z

    if-eqz v0, :cond_0

    invoke-static {p0}, Lcom/facebook/internal/logging/monitor/Monitor;->isSampled(Lcom/facebook/internal/logging/ExternalLog;)Z

    move-result v0

    if-eqz v0, :cond_0

    .line 139
    sget-object v0, Lcom/facebook/internal/logging/monitor/Monitor;->monitorLoggingManager:Lcom/facebook/internal/logging/LoggingManager;

    invoke-interface {v0, p0}, Lcom/facebook/internal/logging/LoggingManager;->addLog(Lcom/facebook/internal/logging/ExternalLog;)V

    :cond_0
    return-void
.end method

.method public static enable()V
    .locals 1

    .line 65
    sget-boolean v0, Lcom/facebook/internal/logging/monitor/Monitor;->isEnabled:Z

    if-eqz v0, :cond_0

    return-void

    :cond_0
    const/4 v0, 0x1

    .line 68
    sput-boolean v0, Lcom/facebook/internal/logging/monitor/Monitor;->isEnabled:Z

    .line 71
    invoke-static {}, Lcom/facebook/internal/logging/monitor/Monitor;->loadSamplingRatesMapAsync()V

    .line 74
    sget-object v0, Lcom/facebook/internal/logging/monitor/Monitor;->monitorLoggingManager:Lcom/facebook/internal/logging/LoggingManager;

    invoke-interface {v0}, Lcom/facebook/internal/logging/LoggingManager;->flushLoggingStore()V

    return-void
.end method

.method static fetchSamplingRate()Lorg/json/JSONObject;
    .locals 3

    .line 98
    new-instance v0, Landroid/os/Bundle;

    invoke-direct {v0}, Landroid/os/Bundle;-><init>()V

    const-string v1, "fields"

    const-string v2, "monitoring_config"

    .line 99
    invoke-virtual {v0, v1, v2}, Landroid/os/Bundle;->putString(Ljava/lang/String;Ljava/lang/String;)V

    .line 101
    invoke-static {}, Lcom/facebook/FacebookSdk;->getApplicationId()Ljava/lang/String;

    move-result-object v1

    const/4 v2, 0x0

    .line 100
    invoke-static {v2, v1, v2}, Lcom/facebook/GraphRequest;->newGraphPathRequest(Lcom/facebook/AccessToken;Ljava/lang/String;Lcom/facebook/GraphRequest$Callback;)Lcom/facebook/GraphRequest;

    move-result-object v1

    const/4 v2, 0x1

    .line 102
    invoke-virtual {v1, v2}, Lcom/facebook/GraphRequest;->setSkipClientToken(Z)V

    .line 103
    invoke-virtual {v1, v0}, Lcom/facebook/GraphRequest;->setParameters(Landroid/os/Bundle;)V

    .line 104
    invoke-virtual {v1}, Lcom/facebook/GraphRequest;->executeAndWait()Lcom/facebook/GraphResponse;

    move-result-object v0

    invoke-virtual {v0}, Lcom/facebook/GraphResponse;->getJSONObject()Lorg/json/JSONObject;

    move-result-object v0

    return-object v0
.end method

.method static getDefaultSamplingRate()Ljava/lang/Integer;
    .locals 1

    .line 161
    sget-object v0, Lcom/facebook/internal/logging/monitor/Monitor;->defaultSamplingRate:Ljava/lang/Integer;

    return-object v0
.end method

.method public static isEnabled()Z
    .locals 1

    .line 144
    sget-boolean v0, Lcom/facebook/internal/logging/monitor/Monitor;->isEnabled:Z

    return v0
.end method

.method static isSampled(Lcom/facebook/internal/logging/ExternalLog;)Z
    .locals 2

    .line 151
    invoke-interface {p0}, Lcom/facebook/internal/logging/ExternalLog;->getEventName()Ljava/lang/String;

    move-result-object p0

    .line 152
    sget-object v0, Lcom/facebook/internal/logging/monitor/Monitor;->defaultSamplingRate:Ljava/lang/Integer;

    invoke-virtual {v0}, Ljava/lang/Integer;->intValue()I

    move-result v0

    .line 153
    sget-object v1, Lcom/facebook/internal/logging/monitor/Monitor;->samplingRatesMap:Ljava/util/Map;

    invoke-interface {v1, p0}, Ljava/util/Map;->containsKey(Ljava/lang/Object;)Z

    move-result v1

    if-eqz v1, :cond_0

    .line 154
    sget-object v0, Lcom/facebook/internal/logging/monitor/Monitor;->samplingRatesMap:Ljava/util/Map;

    invoke-interface {v0, p0}, Ljava/util/Map;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object p0

    check-cast p0, Ljava/lang/Integer;

    invoke-virtual {p0}, Ljava/lang/Integer;->intValue()I

    move-result v0

    :cond_0
    if-lez v0, :cond_1

    .line 156
    sget-object p0, Lcom/facebook/internal/logging/monitor/Monitor;->random:Ljava/util/Random;

    invoke-virtual {p0, v0}, Ljava/util/Random;->nextInt(I)I

    move-result p0

    if-nez p0, :cond_1

    const/4 p0, 0x1

    goto :goto_0

    :cond_1
    const/4 p0, 0x0

    :goto_0
    return p0
.end method

.method public static loadSamplingRatesMapAsync()V
    .locals 2

    .line 81
    invoke-static {}, Lcom/facebook/FacebookSdk;->getExecutor()Ljava/util/concurrent/Executor;

    move-result-object v0

    new-instance v1, Lcom/facebook/internal/logging/monitor/Monitor$1;

    invoke-direct {v1}, Lcom/facebook/internal/logging/monitor/Monitor$1;-><init>()V

    invoke-interface {v0, v1}, Ljava/util/concurrent/Executor;->execute(Ljava/lang/Runnable;)V

    return-void
.end method

.method static updateSamplingRateMap(Lorg/json/JSONObject;)V
    .locals 4

    :try_start_0
    const-string v0, "monitoring_config"

    .line 115
    invoke-virtual {p0, v0}, Lorg/json/JSONObject;->getJSONObject(Ljava/lang/String;)Lorg/json/JSONObject;

    move-result-object p0

    const-string v0, "sample_rates"

    .line 116
    invoke-virtual {p0, v0}, Lorg/json/JSONObject;->getJSONArray(Ljava/lang/String;)Lorg/json/JSONArray;

    move-result-object p0

    const/4 v0, 0x0

    .line 117
    :goto_0
    invoke-virtual {p0}, Lorg/json/JSONArray;->length()I

    move-result v1

    if-ge v0, v1, :cond_1

    .line 118
    invoke-virtual {p0, v0}, Lorg/json/JSONArray;->getJSONObject(I)Lorg/json/JSONObject;

    move-result-object v1

    const-string v2, "key"

    .line 119
    invoke-virtual {v1, v2}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v2

    const-string v3, "value"

    .line 120
    invoke-virtual {v1, v3}, Lorg/json/JSONObject;->getInt(Ljava/lang/String;)I

    move-result v1

    const-string v3, "default"

    .line 121
    invoke-virtual {v3, v2}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v3

    if-eqz v3, :cond_0

    .line 122
    invoke-static {v1}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v1

    sput-object v1, Lcom/facebook/internal/logging/monitor/Monitor;->defaultSamplingRate:Ljava/lang/Integer;

    goto :goto_1

    .line 124
    :cond_0
    sget-object v3, Lcom/facebook/internal/logging/monitor/Monitor;->samplingRatesMap:Ljava/util/Map;

    invoke-static {v1}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v1

    invoke-interface {v3, v2, v1}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;
    :try_end_0
    .catch Lorg/json/JSONException; {:try_start_0 .. :try_end_0} :catch_0

    :goto_1
    add-int/lit8 v0, v0, 0x1

    goto :goto_0

    :catch_0
    :cond_1
    return-void
.end method
