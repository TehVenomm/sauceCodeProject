.class public Lcom/helpshift/analytics/domainmodel/AnalyticsEventDM;
.super Ljava/lang/Object;
.source "AnalyticsEventDM.java"

# interfaces
.implements Lcom/helpshift/common/AutoRetriableDM;


# static fields
.field private static final tsSecFormatter:Ljava/text/DecimalFormat;


# instance fields
.field private final analyticsEventDAO:Lcom/helpshift/analytics/AnalyticsEventDAO;

.field private final domain:Lcom/helpshift/common/domain/Domain;

.field private eventModelList:Ljava/util/List;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/List<",
            "Lcom/helpshift/analytics/dto/AnalyticsEventDTO;",
            ">;"
        }
    .end annotation
.end field

.field private final jsonifier:Lcom/helpshift/common/platform/Jsonifier;

.field private final platform:Lcom/helpshift/common/platform/Platform;

.field private sdkConfigurationDM:Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;


# direct methods
.method static constructor <clinit>()V
    .locals 4

    .line 45
    new-instance v0, Ljava/text/DecimalFormat;

    const-string v1, "0.000"

    new-instance v2, Ljava/text/DecimalFormatSymbols;

    sget-object v3, Ljava/util/Locale;->US:Ljava/util/Locale;

    invoke-direct {v2, v3}, Ljava/text/DecimalFormatSymbols;-><init>(Ljava/util/Locale;)V

    invoke-direct {v0, v1, v2}, Ljava/text/DecimalFormat;-><init>(Ljava/lang/String;Ljava/text/DecimalFormatSymbols;)V

    sput-object v0, Lcom/helpshift/analytics/domainmodel/AnalyticsEventDM;->tsSecFormatter:Ljava/text/DecimalFormat;

    return-void
.end method

.method public constructor <init>(Lcom/helpshift/common/domain/Domain;Lcom/helpshift/common/platform/Platform;)V
    .locals 1

    .line 53
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 54
    iput-object p1, p0, Lcom/helpshift/analytics/domainmodel/AnalyticsEventDM;->domain:Lcom/helpshift/common/domain/Domain;

    .line 55
    iput-object p2, p0, Lcom/helpshift/analytics/domainmodel/AnalyticsEventDM;->platform:Lcom/helpshift/common/platform/Platform;

    .line 56
    invoke-interface {p2}, Lcom/helpshift/common/platform/Platform;->getJsonifier()Lcom/helpshift/common/platform/Jsonifier;

    move-result-object v0

    iput-object v0, p0, Lcom/helpshift/analytics/domainmodel/AnalyticsEventDM;->jsonifier:Lcom/helpshift/common/platform/Jsonifier;

    .line 57
    invoke-interface {p2}, Lcom/helpshift/common/platform/Platform;->getAnalyticsEventDAO()Lcom/helpshift/analytics/AnalyticsEventDAO;

    move-result-object p2

    iput-object p2, p0, Lcom/helpshift/analytics/domainmodel/AnalyticsEventDM;->analyticsEventDAO:Lcom/helpshift/analytics/AnalyticsEventDAO;

    .line 58
    invoke-virtual {p1}, Lcom/helpshift/common/domain/Domain;->getSDKConfigurationDM()Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;

    move-result-object p1

    iput-object p1, p0, Lcom/helpshift/analytics/domainmodel/AnalyticsEventDM;->sdkConfigurationDM:Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;

    .line 59
    iget-object p1, p0, Lcom/helpshift/analytics/domainmodel/AnalyticsEventDM;->domain:Lcom/helpshift/common/domain/Domain;

    invoke-virtual {p1}, Lcom/helpshift/common/domain/Domain;->getAutoRetryFailedEventDM()Lcom/helpshift/common/AutoRetryFailedEventDM;

    move-result-object p1

    sget-object p2, Lcom/helpshift/common/AutoRetryFailedEventDM$EventType;->ANALYTICS:Lcom/helpshift/common/AutoRetryFailedEventDM$EventType;

    invoke-virtual {p1, p2, p0}, Lcom/helpshift/common/AutoRetryFailedEventDM;->register(Lcom/helpshift/common/AutoRetryFailedEventDM$EventType;Lcom/helpshift/common/AutoRetriableDM;)V

    return-void
.end method

.method private addEventToStorage(Lcom/helpshift/analytics/dto/AnalyticsEventDTO;)V
    .locals 1

    .line 63
    iget-object v0, p0, Lcom/helpshift/analytics/domainmodel/AnalyticsEventDM;->eventModelList:Ljava/util/List;

    if-nez v0, :cond_0

    .line 64
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    iput-object v0, p0, Lcom/helpshift/analytics/domainmodel/AnalyticsEventDM;->eventModelList:Ljava/util/List;

    .line 66
    :cond_0
    iget-object v0, p0, Lcom/helpshift/analytics/domainmodel/AnalyticsEventDM;->eventModelList:Ljava/util/List;

    invoke-interface {v0, p1}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    return-void
.end method

.method private buildEventRequestMap(Ljava/lang/String;Lcom/helpshift/account/domainmodel/UserDM;)Ljava/util/HashMap;
    .locals 3
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/lang/String;",
            "Lcom/helpshift/account/domainmodel/UserDM;",
            ")",
            "Ljava/util/HashMap<",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            ">;"
        }
    .end annotation

    .line 144
    invoke-static {p2}, Lcom/helpshift/common/domain/network/NetworkDataRequestUtil;->getUserRequestData(Lcom/helpshift/account/domainmodel/UserDM;)Ljava/util/HashMap;

    move-result-object v0

    const-string v1, "id"

    .line 146
    invoke-direct {p0, p2}, Lcom/helpshift/analytics/domainmodel/AnalyticsEventDM;->getAnalyticsEventId(Lcom/helpshift/account/domainmodel/UserDM;)Ljava/lang/String;

    move-result-object p2

    invoke-virtual {v0, v1, p2}, Ljava/util/HashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string p2, "e"

    .line 149
    invoke-virtual {v0, p2, p1}, Ljava/util/HashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 152
    iget-object p1, p0, Lcom/helpshift/analytics/domainmodel/AnalyticsEventDM;->platform:Lcom/helpshift/common/platform/Platform;

    invoke-interface {p1}, Lcom/helpshift/common/platform/Platform;->getDevice()Lcom/helpshift/common/platform/Device;

    move-result-object p1

    const-string p2, "v"

    .line 153
    invoke-interface {p1}, Lcom/helpshift/common/platform/Device;->getSDKVersion()Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, p2, v1}, Ljava/util/HashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string p2, "os"

    .line 154
    invoke-interface {p1}, Lcom/helpshift/common/platform/Device;->getOSVersion()Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, p2, v1}, Ljava/util/HashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string p2, "av"

    .line 155
    invoke-interface {p1}, Lcom/helpshift/common/platform/Device;->getAppVersion()Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, p2, v1}, Ljava/util/HashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string p2, "dm"

    .line 156
    invoke-interface {p1}, Lcom/helpshift/common/platform/Device;->getDeviceModel()Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, p2, v1}, Ljava/util/HashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string p2, "s"

    .line 157
    iget-object v1, p0, Lcom/helpshift/analytics/domainmodel/AnalyticsEventDM;->sdkConfigurationDM:Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;

    const-string v2, "sdkType"

    invoke-virtual {v1, v2}, Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, p2, v1}, Ljava/util/HashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 158
    iget-object p2, p0, Lcom/helpshift/analytics/domainmodel/AnalyticsEventDM;->sdkConfigurationDM:Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;

    const-string v1, "pluginVersion"

    invoke-virtual {p2, v1}, Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object p2

    .line 159
    iget-object v1, p0, Lcom/helpshift/analytics/domainmodel/AnalyticsEventDM;->sdkConfigurationDM:Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;

    const-string v2, "runtimeVersion"

    invoke-virtual {v1, v2}, Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v1

    .line 160
    invoke-static {p2}, Lcom/helpshift/common/StringUtils;->isEmpty(Ljava/lang/String;)Z

    move-result v2

    if-nez v2, :cond_0

    const-string v2, "pv"

    .line 161
    invoke-virtual {v0, v2, p2}, Ljava/util/HashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 163
    :cond_0
    invoke-static {v1}, Lcom/helpshift/common/StringUtils;->isEmpty(Ljava/lang/String;)Z

    move-result p2

    if-nez p2, :cond_1

    const-string p2, "rv"

    .line 164
    invoke-virtual {v0, p2, v1}, Ljava/util/HashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    :cond_1
    const-string p2, "rs"

    .line 166
    invoke-interface {p1}, Lcom/helpshift/common/platform/Device;->getRom()Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, p2, v1}, Ljava/util/HashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 167
    invoke-interface {p1}, Lcom/helpshift/common/platform/Device;->getSimCountryIso()Ljava/lang/String;

    move-result-object p2

    .line 168
    invoke-static {p2}, Lcom/helpshift/common/StringUtils;->isEmpty(Ljava/lang/String;)Z

    move-result v1

    if-nez v1, :cond_2

    const-string v1, "cc"

    .line 169
    invoke-virtual {v0, v1, p2}, Ljava/util/HashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    :cond_2
    const-string p2, "ln"

    .line 171
    invoke-interface {p1}, Lcom/helpshift/common/platform/Device;->getLanguage()Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, p2, v1}, Ljava/util/HashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 172
    iget-object p2, p0, Lcom/helpshift/analytics/domainmodel/AnalyticsEventDM;->domain:Lcom/helpshift/common/domain/Domain;

    invoke-virtual {p2}, Lcom/helpshift/common/domain/Domain;->getLocaleProviderDM()Lcom/helpshift/localeprovider/domainmodel/LocaleProviderDM;

    move-result-object p2

    invoke-virtual {p2}, Lcom/helpshift/localeprovider/domainmodel/LocaleProviderDM;->getSDKLanguage()Ljava/lang/String;

    move-result-object p2

    .line 173
    invoke-static {p2}, Lcom/helpshift/common/StringUtils;->isEmpty(Ljava/lang/String;)Z

    move-result v1

    if-nez v1, :cond_3

    const-string v1, "dln"

    .line 174
    invoke-virtual {v0, v1, p2}, Ljava/util/HashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    :cond_3
    const-string p2, "and_id"

    .line 177
    invoke-interface {p1}, Lcom/helpshift/common/platform/Device;->getAndroidId()Ljava/lang/String;

    move-result-object p1

    invoke-virtual {v0, p2, p1}, Ljava/util/HashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    return-object v0
.end method

.method private getAnalyticsEventId(Lcom/helpshift/account/domainmodel/UserDM;)Ljava/lang/String;
    .locals 2

    .line 183
    new-instance v0, Lcom/helpshift/analytics/domainmodel/LegacyAnalyticsEventDM;

    iget-object v1, p0, Lcom/helpshift/analytics/domainmodel/AnalyticsEventDM;->platform:Lcom/helpshift/common/platform/Platform;

    invoke-direct {v0, v1}, Lcom/helpshift/analytics/domainmodel/LegacyAnalyticsEventDM;-><init>(Lcom/helpshift/common/platform/Platform;)V

    .line 184
    invoke-virtual {v0, p1}, Lcom/helpshift/analytics/domainmodel/LegacyAnalyticsEventDM;->getLegacyAnalyticsEventId(Lcom/helpshift/account/domainmodel/UserDM;)Ljava/lang/String;

    move-result-object v0

    .line 187
    invoke-static {v0}, Lcom/helpshift/common/StringUtils;->isEmpty(Ljava/lang/String;)Z

    move-result v1

    if-eqz v1, :cond_0

    .line 188
    invoke-virtual {p1}, Lcom/helpshift/account/domainmodel/UserDM;->getDeviceId()Ljava/lang/String;

    move-result-object v0

    :cond_0
    return-object v0
.end method

.method private getAnalyticsNetwork()Lcom/helpshift/common/domain/network/Network;
    .locals 4

    .line 138
    new-instance v0, Lcom/helpshift/common/domain/network/POSTNetwork;

    const-string v1, "/events/"

    iget-object v2, p0, Lcom/helpshift/analytics/domainmodel/AnalyticsEventDM;->domain:Lcom/helpshift/common/domain/Domain;

    iget-object v3, p0, Lcom/helpshift/analytics/domainmodel/AnalyticsEventDM;->platform:Lcom/helpshift/common/platform/Platform;

    invoke-direct {v0, v1, v2, v3}, Lcom/helpshift/common/domain/network/POSTNetwork;-><init>(Ljava/lang/String;Lcom/helpshift/common/domain/Domain;Lcom/helpshift/common/platform/Platform;)V

    .line 139
    new-instance v1, Lcom/helpshift/common/domain/network/FailedAPICallNetworkDecorator;

    invoke-direct {v1, v0}, Lcom/helpshift/common/domain/network/FailedAPICallNetworkDecorator;-><init>(Lcom/helpshift/common/domain/network/Network;)V

    .line 140
    new-instance v0, Lcom/helpshift/common/domain/network/GuardOKNetwork;

    invoke-direct {v0, v1}, Lcom/helpshift/common/domain/network/GuardOKNetwork;-><init>(Lcom/helpshift/common/domain/network/Network;)V

    return-object v0
.end method

.method private sendEvents(Ljava/util/List;Lcom/helpshift/account/domainmodel/UserDM;)V
    .locals 2
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/List<",
            "Lcom/helpshift/analytics/dto/AnalyticsEventDTO;",
            ">;",
            "Lcom/helpshift/account/domainmodel/UserDM;",
            ")V"
        }
    .end annotation

    .line 119
    invoke-static {p1}, Lcom/helpshift/common/ListUtils;->isEmpty(Ljava/util/List;)Z

    move-result v0

    if-nez v0, :cond_1

    .line 120
    iget-object v0, p0, Lcom/helpshift/analytics/domainmodel/AnalyticsEventDM;->jsonifier:Lcom/helpshift/common/platform/Jsonifier;

    invoke-interface {v0, p1}, Lcom/helpshift/common/platform/Jsonifier;->jsonifyAnalyticsDTOList(Ljava/util/List;)Ljava/lang/String;

    move-result-object p1

    .line 121
    invoke-direct {p0, p1, p2}, Lcom/helpshift/analytics/domainmodel/AnalyticsEventDM;->buildEventRequestMap(Ljava/lang/String;Lcom/helpshift/account/domainmodel/UserDM;)Ljava/util/HashMap;

    move-result-object p1

    .line 123
    :try_start_0
    new-instance p2, Lcom/helpshift/common/platform/network/RequestData;

    invoke-direct {p2, p1}, Lcom/helpshift/common/platform/network/RequestData;-><init>(Ljava/util/Map;)V

    .line 124
    invoke-direct {p0}, Lcom/helpshift/analytics/domainmodel/AnalyticsEventDM;->getAnalyticsNetwork()Lcom/helpshift/common/domain/network/Network;

    move-result-object v0

    invoke-interface {v0, p2}, Lcom/helpshift/common/domain/network/Network;->makeRequest(Lcom/helpshift/common/platform/network/RequestData;)Lcom/helpshift/common/platform/network/Response;
    :try_end_0
    .catch Lcom/helpshift/common/exception/RootAPIException; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception p2

    .line 127
    iget-object v0, p2, Lcom/helpshift/common/exception/RootAPIException;->exceptionType:Lcom/helpshift/common/exception/ExceptionType;

    sget-object v1, Lcom/helpshift/common/exception/NetworkException;->NON_RETRIABLE:Lcom/helpshift/common/exception/NetworkException;

    if-ne v0, v1, :cond_0

    goto :goto_0

    .line 128
    :cond_0
    invoke-static {}, Ljava/util/UUID;->randomUUID()Ljava/util/UUID;

    move-result-object v0

    invoke-virtual {v0}, Ljava/util/UUID;->toString()Ljava/lang/String;

    move-result-object v0

    .line 129
    iget-object v1, p0, Lcom/helpshift/analytics/domainmodel/AnalyticsEventDM;->analyticsEventDAO:Lcom/helpshift/analytics/AnalyticsEventDAO;

    invoke-interface {v1, v0, p1}, Lcom/helpshift/analytics/AnalyticsEventDAO;->saveUnsentAnalyticsData(Ljava/lang/String;Ljava/util/HashMap;)V

    .line 130
    iget-object p1, p0, Lcom/helpshift/analytics/domainmodel/AnalyticsEventDM;->domain:Lcom/helpshift/common/domain/Domain;

    invoke-virtual {p1}, Lcom/helpshift/common/domain/Domain;->getAutoRetryFailedEventDM()Lcom/helpshift/common/AutoRetryFailedEventDM;

    move-result-object p1

    sget-object v0, Lcom/helpshift/common/AutoRetryFailedEventDM$EventType;->ANALYTICS:Lcom/helpshift/common/AutoRetryFailedEventDM$EventType;

    invoke-virtual {p2}, Lcom/helpshift/common/exception/RootAPIException;->getServerStatusCode()I

    move-result v1

    invoke-virtual {p1, v0, v1}, Lcom/helpshift/common/AutoRetryFailedEventDM;->scheduleRetryTaskForEventType(Lcom/helpshift/common/AutoRetryFailedEventDM$EventType;I)V

    .line 131
    throw p2

    :cond_1
    :goto_0
    return-void
.end method


# virtual methods
.method public declared-synchronized clearAnalyticsEvent()V
    .locals 1

    monitor-enter p0

    .line 197
    :try_start_0
    iget-object v0, p0, Lcom/helpshift/analytics/domainmodel/AnalyticsEventDM;->eventModelList:Ljava/util/List;

    if-eqz v0, :cond_0

    .line 198
    iget-object v0, p0, Lcom/helpshift/analytics/domainmodel/AnalyticsEventDM;->eventModelList:Ljava/util/List;

    invoke-interface {v0}, Ljava/util/List;->clear()V
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    .line 200
    :cond_0
    monitor-exit p0

    return-void

    :catchall_0
    move-exception v0

    .line 196
    monitor-exit p0

    throw v0
.end method

.method public declared-synchronized getCurrentSessionEventsCopy()Ljava/util/List;
    .locals 2
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()",
            "Ljava/util/List<",
            "Lcom/helpshift/analytics/dto/AnalyticsEventDTO;",
            ">;"
        }
    .end annotation

    monitor-enter p0

    .line 203
    :try_start_0
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    .line 204
    iget-object v1, p0, Lcom/helpshift/analytics/domainmodel/AnalyticsEventDM;->eventModelList:Ljava/util/List;

    if-eqz v1, :cond_0

    .line 205
    iget-object v1, p0, Lcom/helpshift/analytics/domainmodel/AnalyticsEventDM;->eventModelList:Ljava/util/List;

    invoke-interface {v0, v1}, Ljava/util/List;->addAll(Ljava/util/Collection;)Z
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    .line 207
    :cond_0
    monitor-exit p0

    return-object v0

    :catchall_0
    move-exception v0

    .line 202
    monitor-exit p0

    throw v0
.end method

.method public declared-synchronized pushEvent(Lcom/helpshift/analytics/AnalyticsEventType;)V
    .locals 1

    monitor-enter p0

    const/4 v0, 0x0

    .line 82
    :try_start_0
    check-cast v0, Ljava/util/Map;

    invoke-virtual {p0, p1, v0}, Lcom/helpshift/analytics/domainmodel/AnalyticsEventDM;->pushEvent(Lcom/helpshift/analytics/AnalyticsEventType;Ljava/util/Map;)V
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    .line 83
    monitor-exit p0

    return-void

    :catchall_0
    move-exception p1

    .line 81
    monitor-exit p0

    throw p1
.end method

.method public declared-synchronized pushEvent(Lcom/helpshift/analytics/AnalyticsEventType;Ljava/lang/String;)V
    .locals 2

    monitor-enter p0

    .line 76
    :try_start_0
    new-instance v0, Ljava/util/HashMap;

    invoke-direct {v0}, Ljava/util/HashMap;-><init>()V

    const-string v1, "id"

    .line 77
    invoke-interface {v0, v1, p2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 78
    invoke-virtual {p0, p1, v0}, Lcom/helpshift/analytics/domainmodel/AnalyticsEventDM;->pushEvent(Lcom/helpshift/analytics/AnalyticsEventType;Ljava/util/Map;)V
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    .line 79
    monitor-exit p0

    return-void

    :catchall_0
    move-exception p1

    .line 75
    monitor-exit p0

    throw p1
.end method

.method public declared-synchronized pushEvent(Lcom/helpshift/analytics/AnalyticsEventType;Ljava/util/Map;)V
    .locals 5
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Lcom/helpshift/analytics/AnalyticsEventType;",
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Ljava/lang/Object;",
            ">;)V"
        }
    .end annotation

    monitor-enter p0

    .line 70
    :try_start_0
    sget-object v0, Lcom/helpshift/analytics/domainmodel/AnalyticsEventDM;->tsSecFormatter:Ljava/text/DecimalFormat;

    invoke-static {}, Ljava/lang/System;->currentTimeMillis()J

    move-result-wide v1
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    long-to-double v1, v1

    const-wide v3, 0x408f400000000000L    # 1000.0

    invoke-static {v1, v2}, Ljava/lang/Double;->isNaN(D)Z

    div-double/2addr v1, v3

    :try_start_1
    invoke-virtual {v0, v1, v2}, Ljava/text/DecimalFormat;->format(D)Ljava/lang/String;

    move-result-object v0

    .line 71
    invoke-static {}, Ljava/util/UUID;->randomUUID()Ljava/util/UUID;

    move-result-object v1

    invoke-virtual {v1}, Ljava/util/UUID;->toString()Ljava/lang/String;

    move-result-object v1

    .line 72
    new-instance v2, Lcom/helpshift/analytics/dto/AnalyticsEventDTO;

    invoke-direct {v2, v1, p1, p2, v0}, Lcom/helpshift/analytics/dto/AnalyticsEventDTO;-><init>(Ljava/lang/String;Lcom/helpshift/analytics/AnalyticsEventType;Ljava/util/Map;Ljava/lang/String;)V

    invoke-direct {p0, v2}, Lcom/helpshift/analytics/domainmodel/AnalyticsEventDM;->addEventToStorage(Lcom/helpshift/analytics/dto/AnalyticsEventDTO;)V
    :try_end_1
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    .line 73
    monitor-exit p0

    return-void

    :catchall_0
    move-exception p1

    .line 69
    monitor-exit p0

    throw p1
.end method

.method public sendAppStartEvent(Lcom/helpshift/account/domainmodel/UserDM;)V
    .locals 6

    .line 212
    iget-object v0, p0, Lcom/helpshift/analytics/domainmodel/AnalyticsEventDM;->sdkConfigurationDM:Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;

    const-string v1, "disableAppLaunchEvent"

    invoke-virtual {v0, v1}, Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;->getBoolean(Ljava/lang/String;)Z

    move-result v0

    if-nez v0, :cond_0

    .line 213
    invoke-static {}, Ljava/util/UUID;->randomUUID()Ljava/util/UUID;

    move-result-object v0

    invoke-virtual {v0}, Ljava/util/UUID;->toString()Ljava/lang/String;

    move-result-object v0

    .line 214
    sget-object v1, Lcom/helpshift/analytics/domainmodel/AnalyticsEventDM;->tsSecFormatter:Ljava/text/DecimalFormat;

    invoke-static {}, Ljava/lang/System;->currentTimeMillis()J

    move-result-wide v2

    long-to-double v2, v2

    const-wide v4, 0x408f400000000000L    # 1000.0

    invoke-static {v2, v3}, Ljava/lang/Double;->isNaN(D)Z

    div-double/2addr v2, v4

    invoke-virtual {v1, v2, v3}, Ljava/text/DecimalFormat;->format(D)Ljava/lang/String;

    move-result-object v1

    .line 215
    new-instance v2, Lcom/helpshift/analytics/dto/AnalyticsEventDTO;

    sget-object v3, Lcom/helpshift/analytics/AnalyticsEventType;->APP_START:Lcom/helpshift/analytics/AnalyticsEventType;

    const/4 v4, 0x0

    invoke-direct {v2, v0, v3, v4, v1}, Lcom/helpshift/analytics/dto/AnalyticsEventDTO;-><init>(Ljava/lang/String;Lcom/helpshift/analytics/AnalyticsEventType;Ljava/util/Map;Ljava/lang/String;)V

    .line 219
    invoke-static {v2}, Ljava/util/Collections;->singletonList(Ljava/lang/Object;)Ljava/util/List;

    move-result-object v0

    invoke-direct {p0, v0, p1}, Lcom/helpshift/analytics/domainmodel/AnalyticsEventDM;->sendEvents(Ljava/util/List;Lcom/helpshift/account/domainmodel/UserDM;)V

    :cond_0
    return-void
.end method

.method public sendEventsToServer(Lcom/helpshift/account/domainmodel/UserDM;)V
    .locals 1

    .line 86
    invoke-virtual {p0}, Lcom/helpshift/analytics/domainmodel/AnalyticsEventDM;->getCurrentSessionEventsCopy()Ljava/util/List;

    move-result-object v0

    .line 87
    invoke-virtual {p0}, Lcom/helpshift/analytics/domainmodel/AnalyticsEventDM;->clearAnalyticsEvent()V

    .line 88
    invoke-direct {p0, v0, p1}, Lcom/helpshift/analytics/domainmodel/AnalyticsEventDM;->sendEvents(Ljava/util/List;Lcom/helpshift/account/domainmodel/UserDM;)V

    return-void
.end method

.method public sendFailedApiCalls(Lcom/helpshift/common/AutoRetryFailedEventDM$EventType;)V
    .locals 6

    .line 94
    sget-object v0, Lcom/helpshift/common/AutoRetryFailedEventDM$EventType;->ANALYTICS:Lcom/helpshift/common/AutoRetryFailedEventDM$EventType;

    if-eq p1, v0, :cond_0

    return-void

    .line 97
    :cond_0
    iget-object p1, p0, Lcom/helpshift/analytics/domainmodel/AnalyticsEventDM;->analyticsEventDAO:Lcom/helpshift/analytics/AnalyticsEventDAO;

    invoke-interface {p1}, Lcom/helpshift/analytics/AnalyticsEventDAO;->getUnsentAnalytics()Ljava/util/Map;

    move-result-object p1

    if-eqz p1, :cond_2

    .line 98
    invoke-interface {p1}, Ljava/util/Map;->size()I

    move-result v0

    if-lez v0, :cond_2

    .line 99
    invoke-direct {p0}, Lcom/helpshift/analytics/domainmodel/AnalyticsEventDM;->getAnalyticsNetwork()Lcom/helpshift/common/domain/network/Network;

    move-result-object v0

    .line 100
    invoke-interface {p1}, Ljava/util/Map;->keySet()Ljava/util/Set;

    move-result-object v1

    invoke-interface {v1}, Ljava/util/Set;->iterator()Ljava/util/Iterator;

    move-result-object v1

    :goto_0
    invoke-interface {v1}, Ljava/util/Iterator;->hasNext()Z

    move-result v2

    if-eqz v2, :cond_2

    invoke-interface {v1}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Ljava/lang/String;

    .line 102
    :try_start_0
    new-instance v3, Lcom/helpshift/common/platform/network/RequestData;

    invoke-interface {p1, v2}, Ljava/util/Map;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v4

    check-cast v4, Ljava/util/Map;

    invoke-direct {v3, v4}, Lcom/helpshift/common/platform/network/RequestData;-><init>(Ljava/util/Map;)V

    .line 103
    invoke-interface {v0, v3}, Lcom/helpshift/common/domain/network/Network;->makeRequest(Lcom/helpshift/common/platform/network/RequestData;)Lcom/helpshift/common/platform/network/Response;

    .line 104
    iget-object v3, p0, Lcom/helpshift/analytics/domainmodel/AnalyticsEventDM;->analyticsEventDAO:Lcom/helpshift/analytics/AnalyticsEventDAO;

    invoke-interface {v3, v2}, Lcom/helpshift/analytics/AnalyticsEventDAO;->removeAnalyticsData(Ljava/lang/String;)V
    :try_end_0
    .catch Lcom/helpshift/common/exception/RootAPIException; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception v3

    .line 107
    iget-object v4, v3, Lcom/helpshift/common/exception/RootAPIException;->exceptionType:Lcom/helpshift/common/exception/ExceptionType;

    sget-object v5, Lcom/helpshift/common/exception/NetworkException;->NON_RETRIABLE:Lcom/helpshift/common/exception/NetworkException;

    if-ne v4, v5, :cond_1

    .line 108
    iget-object v3, p0, Lcom/helpshift/analytics/domainmodel/AnalyticsEventDM;->analyticsEventDAO:Lcom/helpshift/analytics/AnalyticsEventDAO;

    invoke-interface {v3, v2}, Lcom/helpshift/analytics/AnalyticsEventDAO;->removeAnalyticsData(Ljava/lang/String;)V

    goto :goto_0

    .line 111
    :cond_1
    throw v3

    :cond_2
    return-void
.end method
