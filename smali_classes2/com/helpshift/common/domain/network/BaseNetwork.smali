.class abstract Lcom/helpshift/common/domain/network/BaseNetwork;
.super Ljava/lang/Object;
.source "BaseNetwork.java"

# interfaces
.implements Lcom/helpshift/common/domain/network/Network;


# instance fields
.field private final apiKey:Ljava/lang/String;

.field private final appId:Ljava/lang/String;

.field private final cryptoDM:Lcom/helpshift/crypto/CryptoDM;

.field private final device:Lcom/helpshift/common/platform/Device;

.field private final domain:Lcom/helpshift/common/domain/Domain;

.field private final domainKey:Ljava/lang/String;

.field private final jsonifier:Lcom/helpshift/common/platform/Jsonifier;

.field private final localeProviderDM:Lcom/helpshift/localeprovider/domainmodel/LocaleProviderDM;

.field final networkRequestDAO:Lcom/helpshift/common/platform/network/NetworkRequestDAO;

.field private final platform:Lcom/helpshift/common/platform/Platform;

.field final route:Ljava/lang/String;

.field private final transport:Lcom/helpshift/common/platform/network/HTTPTransport;


# direct methods
.method constructor <init>(Ljava/lang/String;Lcom/helpshift/common/domain/Domain;Lcom/helpshift/common/platform/Platform;)V
    .locals 0

    .line 45
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 46
    iput-object p1, p0, Lcom/helpshift/common/domain/network/BaseNetwork;->route:Ljava/lang/String;

    .line 47
    iput-object p3, p0, Lcom/helpshift/common/domain/network/BaseNetwork;->platform:Lcom/helpshift/common/platform/Platform;

    .line 48
    iput-object p2, p0, Lcom/helpshift/common/domain/network/BaseNetwork;->domain:Lcom/helpshift/common/domain/Domain;

    .line 49
    invoke-virtual {p2}, Lcom/helpshift/common/domain/Domain;->getLocaleProviderDM()Lcom/helpshift/localeprovider/domainmodel/LocaleProviderDM;

    move-result-object p1

    iput-object p1, p0, Lcom/helpshift/common/domain/network/BaseNetwork;->localeProviderDM:Lcom/helpshift/localeprovider/domainmodel/LocaleProviderDM;

    .line 50
    invoke-virtual {p2}, Lcom/helpshift/common/domain/Domain;->getCryptoDM()Lcom/helpshift/crypto/CryptoDM;

    move-result-object p1

    iput-object p1, p0, Lcom/helpshift/common/domain/network/BaseNetwork;->cryptoDM:Lcom/helpshift/crypto/CryptoDM;

    .line 51
    invoke-interface {p3}, Lcom/helpshift/common/platform/Platform;->getNetworkRequestDAO()Lcom/helpshift/common/platform/network/NetworkRequestDAO;

    move-result-object p1

    iput-object p1, p0, Lcom/helpshift/common/domain/network/BaseNetwork;->networkRequestDAO:Lcom/helpshift/common/platform/network/NetworkRequestDAO;

    .line 52
    invoke-interface {p3}, Lcom/helpshift/common/platform/Platform;->getHTTPTransport()Lcom/helpshift/common/platform/network/HTTPTransport;

    move-result-object p1

    iput-object p1, p0, Lcom/helpshift/common/domain/network/BaseNetwork;->transport:Lcom/helpshift/common/platform/network/HTTPTransport;

    .line 53
    invoke-interface {p3}, Lcom/helpshift/common/platform/Platform;->getAPIKey()Ljava/lang/String;

    move-result-object p1

    iput-object p1, p0, Lcom/helpshift/common/domain/network/BaseNetwork;->apiKey:Ljava/lang/String;

    .line 54
    invoke-interface {p3}, Lcom/helpshift/common/platform/Platform;->getDomain()Ljava/lang/String;

    move-result-object p1

    iput-object p1, p0, Lcom/helpshift/common/domain/network/BaseNetwork;->domainKey:Ljava/lang/String;

    .line 55
    invoke-interface {p3}, Lcom/helpshift/common/platform/Platform;->getAppId()Ljava/lang/String;

    move-result-object p1

    iput-object p1, p0, Lcom/helpshift/common/domain/network/BaseNetwork;->appId:Ljava/lang/String;

    .line 56
    invoke-interface {p3}, Lcom/helpshift/common/platform/Platform;->getDevice()Lcom/helpshift/common/platform/Device;

    move-result-object p1

    iput-object p1, p0, Lcom/helpshift/common/domain/network/BaseNetwork;->device:Lcom/helpshift/common/platform/Device;

    .line 57
    invoke-interface {p3}, Lcom/helpshift/common/platform/Platform;->getJsonifier()Lcom/helpshift/common/platform/Jsonifier;

    move-result-object p1

    iput-object p1, p0, Lcom/helpshift/common/domain/network/BaseNetwork;->jsonifier:Lcom/helpshift/common/platform/Jsonifier;

    return-void
.end method

.method private getCustomHeaders(Lcom/helpshift/common/platform/network/RequestData;)Ljava/util/List;
    .locals 4
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Lcom/helpshift/common/platform/network/RequestData;",
            ")",
            "Ljava/util/List<",
            "Lcom/helpshift/common/platform/network/KeyValuePair;",
            ">;"
        }
    .end annotation

    .line 130
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    .line 131
    invoke-virtual {p1}, Lcom/helpshift/common/platform/network/RequestData;->getCustomHeaders()Ljava/util/Map;

    move-result-object p1

    if-eqz p1, :cond_0

    .line 133
    invoke-interface {p1}, Ljava/util/Map;->entrySet()Ljava/util/Set;

    move-result-object p1

    invoke-interface {p1}, Ljava/util/Set;->iterator()Ljava/util/Iterator;

    move-result-object p1

    :goto_0
    invoke-interface {p1}, Ljava/util/Iterator;->hasNext()Z

    move-result v1

    if-eqz v1, :cond_0

    invoke-interface {p1}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Ljava/util/Map$Entry;

    .line 134
    new-instance v2, Lcom/helpshift/common/platform/network/KeyValuePair;

    invoke-interface {v1}, Ljava/util/Map$Entry;->getKey()Ljava/lang/Object;

    move-result-object v3

    check-cast v3, Ljava/lang/String;

    invoke-interface {v1}, Ljava/util/Map$Entry;->getValue()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Ljava/lang/String;

    invoke-direct {v2, v3, v1}, Lcom/helpshift/common/platform/network/KeyValuePair;-><init>(Ljava/lang/String;Ljava/lang/String;)V

    invoke-virtual {v0, v2}, Ljava/util/ArrayList;->add(Ljava/lang/Object;)Z

    goto :goto_0

    :cond_0
    return-object v0
.end method

.method private getURI()Ljava/lang/String;
    .locals 2

    .line 67
    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    const-string v1, "/api/lib/3"

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v1, p0, Lcom/helpshift/common/domain/network/BaseNetwork;->route:Ljava/lang/String;

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v0

    return-object v0
.end method


# virtual methods
.method getAuthData(Lcom/helpshift/common/platform/network/Method;Ljava/util/Map;)Ljava/util/Map;
    .locals 4
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Lcom/helpshift/common/platform/network/Method;",
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            ">;)",
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            ">;"
        }
    .end annotation

    .line 72
    new-instance v0, Lcom/helpshift/common/domain/network/AuthDataProvider;

    iget-object v1, p0, Lcom/helpshift/common/domain/network/BaseNetwork;->domain:Lcom/helpshift/common/domain/Domain;

    iget-object v2, p0, Lcom/helpshift/common/domain/network/BaseNetwork;->platform:Lcom/helpshift/common/platform/Platform;

    iget-object v3, p0, Lcom/helpshift/common/domain/network/BaseNetwork;->route:Ljava/lang/String;

    invoke-direct {v0, v1, v2, v3}, Lcom/helpshift/common/domain/network/AuthDataProvider;-><init>(Lcom/helpshift/common/domain/Domain;Lcom/helpshift/common/platform/Platform;Ljava/lang/String;)V

    const-string v1, "uri"

    .line 73
    invoke-direct {p0}, Lcom/helpshift/common/domain/network/BaseNetwork;->getURI()Ljava/lang/String;

    move-result-object v2

    invoke-interface {p2, v1, v2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 75
    :try_start_0
    invoke-virtual {v0, p1, p2}, Lcom/helpshift/common/domain/network/AuthDataProvider;->getAuthData(Lcom/helpshift/common/platform/network/Method;Ljava/util/Map;)Ljava/util/Map;

    move-result-object p1
    :try_end_0
    .catch Ljava/security/GeneralSecurityException; {:try_start_0 .. :try_end_0} :catch_0

    return-object p1

    :catch_0
    move-exception p1

    .line 78
    sget-object p2, Lcom/helpshift/common/exception/NetworkException;->UNABLE_TO_GENERATE_SIGNATURE:Lcom/helpshift/common/exception/NetworkException;

    .line 79
    iget-object v0, p0, Lcom/helpshift/common/domain/network/BaseNetwork;->route:Ljava/lang/String;

    iput-object v0, p2, Lcom/helpshift/common/exception/NetworkException;->route:Ljava/lang/String;

    const-string v0, "Network error"

    .line 80
    invoke-static {p1, p2, v0}, Lcom/helpshift/common/exception/RootAPIException;->wrap(Ljava/lang/Exception;Lcom/helpshift/common/exception/ExceptionType;Ljava/lang/String;)Lcom/helpshift/common/exception/RootAPIException;

    move-result-object p1

    throw p1
.end method

.method getCommonHeaders(Ljava/lang/String;)Ljava/util/List;
    .locals 9
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/lang/String;",
            ")",
            "Ljava/util/List<",
            "Lcom/helpshift/common/platform/network/KeyValuePair;",
            ">;"
        }
    .end annotation

    .line 94
    sget-object v0, Ljava/util/Locale;->ENGLISH:Ljava/util/Locale;

    const-string v1, "Helpshift-%s/%s/%s"

    const/4 v2, 0x3

    new-array v2, v2, [Ljava/lang/Object;

    iget-object v3, p0, Lcom/helpshift/common/domain/network/BaseNetwork;->device:Lcom/helpshift/common/platform/Device;

    .line 96
    invoke-interface {v3}, Lcom/helpshift/common/platform/Device;->getPlatformName()Ljava/lang/String;

    move-result-object v3

    const/4 v4, 0x0

    aput-object v3, v2, v4

    iget-object v3, p0, Lcom/helpshift/common/domain/network/BaseNetwork;->device:Lcom/helpshift/common/platform/Device;

    .line 97
    invoke-interface {v3}, Lcom/helpshift/common/platform/Device;->getSDKVersion()Ljava/lang/String;

    move-result-object v3

    const/4 v5, 0x1

    aput-object v3, v2, v5

    iget-object v3, p0, Lcom/helpshift/common/domain/network/BaseNetwork;->device:Lcom/helpshift/common/platform/Device;

    .line 98
    invoke-interface {v3}, Lcom/helpshift/common/platform/Device;->getOSVersion()Ljava/lang/String;

    move-result-object v3

    const/4 v6, 0x2

    aput-object v3, v2, v6

    .line 94
    invoke-static {v0, v1, v2}, Ljava/lang/String;->format(Ljava/util/Locale;Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v0

    .line 100
    iget-object v1, p0, Lcom/helpshift/common/domain/network/BaseNetwork;->localeProviderDM:Lcom/helpshift/localeprovider/domainmodel/LocaleProviderDM;

    invoke-virtual {v1}, Lcom/helpshift/localeprovider/domainmodel/LocaleProviderDM;->getSDKLanguage()Ljava/lang/String;

    move-result-object v1

    .line 101
    iget-object v2, p0, Lcom/helpshift/common/domain/network/BaseNetwork;->localeProviderDM:Lcom/helpshift/localeprovider/domainmodel/LocaleProviderDM;

    invoke-virtual {v2}, Lcom/helpshift/localeprovider/domainmodel/LocaleProviderDM;->getDefaultLanguage()Ljava/lang/String;

    move-result-object v2

    .line 107
    invoke-static {v1}, Lcom/helpshift/common/StringUtils;->isEmpty(Ljava/lang/String;)Z

    move-result v3

    if-nez v3, :cond_0

    .line 108
    sget-object v3, Ljava/util/Locale;->ENGLISH:Ljava/util/Locale;

    const-string v7, "%s;q=1.0, %s;q=0.5"

    new-array v8, v6, [Ljava/lang/Object;

    aput-object v1, v8, v4

    aput-object v2, v8, v5

    invoke-static {v3, v7, v8}, Ljava/lang/String;->format(Ljava/util/Locale;Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v1

    goto :goto_0

    .line 111
    :cond_0
    sget-object v1, Ljava/util/Locale;->ENGLISH:Ljava/util/Locale;

    const-string v3, "%s;q=1.0"

    new-array v7, v5, [Ljava/lang/Object;

    aput-object v2, v7, v4

    invoke-static {v1, v3, v7}, Ljava/lang/String;->format(Ljava/util/Locale;Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v1

    .line 114
    :goto_0
    sget-object v2, Ljava/util/Locale;->ENGLISH:Ljava/util/Locale;

    const-string v3, "Helpshift-%s/%s"

    new-array v6, v6, [Ljava/lang/Object;

    iget-object v7, p0, Lcom/helpshift/common/domain/network/BaseNetwork;->device:Lcom/helpshift/common/platform/Device;

    .line 116
    invoke-interface {v7}, Lcom/helpshift/common/platform/Device;->getPlatformName()Ljava/lang/String;

    move-result-object v7

    aput-object v7, v6, v4

    iget-object v4, p0, Lcom/helpshift/common/domain/network/BaseNetwork;->device:Lcom/helpshift/common/platform/Device;

    .line 117
    invoke-interface {v4}, Lcom/helpshift/common/platform/Device;->getSDKVersion()Ljava/lang/String;

    move-result-object v4

    aput-object v4, v6, v5

    .line 114
    invoke-static {v2, v3, v6}, Ljava/lang/String;->format(Ljava/util/Locale;Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v2

    .line 119
    new-instance v3, Ljava/util/ArrayList;

    invoke-direct {v3}, Ljava/util/ArrayList;-><init>()V

    .line 120
    new-instance v4, Lcom/helpshift/common/platform/network/KeyValuePair;

    const-string v5, "User-Agent"

    invoke-direct {v4, v5, v0}, Lcom/helpshift/common/platform/network/KeyValuePair;-><init>(Ljava/lang/String;Ljava/lang/String;)V

    invoke-virtual {v3, v4}, Ljava/util/ArrayList;->add(Ljava/lang/Object;)Z

    .line 121
    new-instance v0, Lcom/helpshift/common/platform/network/KeyValuePair;

    const-string v4, "Accept-Language"

    invoke-direct {v0, v4, v1}, Lcom/helpshift/common/platform/network/KeyValuePair;-><init>(Ljava/lang/String;Ljava/lang/String;)V

    invoke-virtual {v3, v0}, Ljava/util/ArrayList;->add(Ljava/lang/Object;)Z

    .line 122
    new-instance v0, Lcom/helpshift/common/platform/network/KeyValuePair;

    const-string v1, "Accept-Encoding"

    const-string v4, "gzip"

    invoke-direct {v0, v1, v4}, Lcom/helpshift/common/platform/network/KeyValuePair;-><init>(Ljava/lang/String;Ljava/lang/String;)V

    invoke-virtual {v3, v0}, Ljava/util/ArrayList;->add(Ljava/lang/Object;)Z

    .line 123
    new-instance v0, Lcom/helpshift/common/platform/network/KeyValuePair;

    const-string v1, "X-HS-V"

    invoke-direct {v0, v1, v2}, Lcom/helpshift/common/platform/network/KeyValuePair;-><init>(Ljava/lang/String;Ljava/lang/String;)V

    invoke-virtual {v3, v0}, Ljava/util/ArrayList;->add(Ljava/lang/Object;)Z

    .line 124
    new-instance v0, Lcom/helpshift/common/platform/network/KeyValuePair;

    const-string v1, "X-HS-Request-ID"

    invoke-direct {v0, v1, p1}, Lcom/helpshift/common/platform/network/KeyValuePair;-><init>(Ljava/lang/String;Ljava/lang/String;)V

    invoke-virtual {v3, v0}, Ljava/util/ArrayList;->add(Ljava/lang/Object;)Z

    return-object v3
.end method

.method getHeaders(Ljava/lang/String;Lcom/helpshift/common/platform/network/RequestData;)Ljava/util/List;
    .locals 0
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/lang/String;",
            "Lcom/helpshift/common/platform/network/RequestData;",
            ")",
            "Ljava/util/List<",
            "Lcom/helpshift/common/platform/network/KeyValuePair;",
            ">;"
        }
    .end annotation

    .line 88
    invoke-virtual {p0, p1}, Lcom/helpshift/common/domain/network/BaseNetwork;->getCommonHeaders(Ljava/lang/String;)Ljava/util/List;

    move-result-object p1

    .line 89
    invoke-direct {p0, p2}, Lcom/helpshift/common/domain/network/BaseNetwork;->getCustomHeaders(Lcom/helpshift/common/platform/network/RequestData;)Ljava/util/List;

    move-result-object p2

    invoke-interface {p1, p2}, Ljava/util/List;->addAll(Ljava/util/Collection;)Z

    return-object p1
.end method

.method abstract getRequest(Lcom/helpshift/common/platform/network/RequestData;)Lcom/helpshift/common/platform/network/Request;
.end method

.method protected getURL()Ljava/lang/String;
    .locals 2

    .line 63
    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    sget-object v1, Lcom/helpshift/common/domain/network/NetworkConstants;->scheme:Ljava/lang/String;

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v1, p0, Lcom/helpshift/common/domain/network/BaseNetwork;->domainKey:Ljava/lang/String;

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-direct {p0}, Lcom/helpshift/common/domain/network/BaseNetwork;->getURI()Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v0

    return-object v0
.end method

.method public makeRequest(Lcom/helpshift/common/platform/network/RequestData;)Lcom/helpshift/common/platform/network/Response;
    .locals 1

    .line 142
    invoke-virtual {p0, p1}, Lcom/helpshift/common/domain/network/BaseNetwork;->getRequest(Lcom/helpshift/common/platform/network/RequestData;)Lcom/helpshift/common/platform/network/Request;

    move-result-object p1

    .line 143
    iget-object v0, p0, Lcom/helpshift/common/domain/network/BaseNetwork;->transport:Lcom/helpshift/common/platform/network/HTTPTransport;

    invoke-interface {v0, p1}, Lcom/helpshift/common/platform/network/HTTPTransport;->makeRequest(Lcom/helpshift/common/platform/network/Request;)Lcom/helpshift/common/platform/network/Response;

    move-result-object p1

    return-object p1
.end method
