.class public Lcom/helpshift/common/domain/network/POSTNetwork;
.super Lcom/helpshift/common/domain/network/BaseNetwork;
.source "POSTNetwork.java"

# interfaces
.implements Lcom/helpshift/common/domain/network/Network;


# direct methods
.method public constructor <init>(Ljava/lang/String;Lcom/helpshift/common/domain/Domain;Lcom/helpshift/common/platform/Platform;)V
    .locals 0

    .line 25
    invoke-direct {p0, p1, p2, p3}, Lcom/helpshift/common/domain/network/BaseNetwork;-><init>(Ljava/lang/String;Lcom/helpshift/common/domain/Domain;Lcom/helpshift/common/platform/Platform;)V

    return-void
.end method


# virtual methods
.method protected getHeaders(Ljava/lang/String;Lcom/helpshift/common/platform/network/RequestData;)Ljava/util/List;
    .locals 2
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

    .line 36
    invoke-super {p0, p1, p2}, Lcom/helpshift/common/domain/network/BaseNetwork;->getHeaders(Ljava/lang/String;Lcom/helpshift/common/platform/network/RequestData;)Ljava/util/List;

    move-result-object p1

    .line 37
    new-instance p2, Lcom/helpshift/common/platform/network/KeyValuePair;

    const-string v0, "Content-type"

    const-string v1, "application/x-www-form-urlencoded"

    invoke-direct {p2, v0, v1}, Lcom/helpshift/common/platform/network/KeyValuePair;-><init>(Ljava/lang/String;Ljava/lang/String;)V

    invoke-interface {p1, p2}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    return-object p1
.end method

.method protected getQuery(Ljava/util/Map;)Ljava/lang/String;
    .locals 5
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            ">;)",
            "Ljava/lang/String;"
        }
    .end annotation

    .line 42
    sget-object v0, Lcom/helpshift/common/platform/network/Method;->POST:Lcom/helpshift/common/platform/network/Method;

    invoke-virtual {p0, v0, p1}, Lcom/helpshift/common/domain/network/POSTNetwork;->getAuthData(Lcom/helpshift/common/platform/network/Method;Ljava/util/Map;)Ljava/util/Map;

    move-result-object p1

    .line 43
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    .line 44
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

    .line 46
    :try_start_0
    new-instance v2, Ljava/lang/StringBuilder;

    invoke-direct {v2}, Ljava/lang/StringBuilder;-><init>()V

    invoke-interface {v1}, Ljava/util/Map$Entry;->getKey()Ljava/lang/Object;

    move-result-object v3

    check-cast v3, Ljava/lang/String;

    const-string v4, "UTF-8"

    invoke-static {v3, v4}, Ljava/net/URLEncoder;->encode(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v3

    invoke-virtual {v2, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v3, "="

    invoke-virtual {v2, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-interface {v1}, Ljava/util/Map$Entry;->getValue()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Ljava/lang/String;

    const-string v3, "UTF-8"

    invoke-static {v1, v3}, Ljava/net/URLEncoder;->encode(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v2, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v2}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    .line 47
    invoke-interface {v0, v1}, Ljava/util/List;->add(Ljava/lang/Object;)Z
    :try_end_0
    .catch Ljava/io/UnsupportedEncodingException; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception p1

    .line 50
    sget-object v0, Lcom/helpshift/common/exception/NetworkException;->UNSUPPORTED_ENCODING_EXCEPTION:Lcom/helpshift/common/exception/NetworkException;

    invoke-static {p1, v0}, Lcom/helpshift/common/exception/RootAPIException;->wrap(Ljava/lang/Exception;Lcom/helpshift/common/exception/ExceptionType;)Lcom/helpshift/common/exception/RootAPIException;

    move-result-object p1

    throw p1

    :cond_0
    const-string p1, "&"

    .line 54
    invoke-static {p1, v0}, Lcom/helpshift/common/StringUtils;->join(Ljava/lang/CharSequence;Ljava/lang/Iterable;)Ljava/lang/String;

    move-result-object p1

    return-object p1
.end method

.method getRequest(Lcom/helpshift/common/platform/network/RequestData;)Lcom/helpshift/common/platform/network/Request;
    .locals 4

    .line 30
    new-instance v0, Lcom/helpshift/common/platform/network/POSTRequest;

    invoke-virtual {p0}, Lcom/helpshift/common/domain/network/POSTNetwork;->getURL()Ljava/lang/String;

    move-result-object v1

    iget-object v2, p1, Lcom/helpshift/common/platform/network/RequestData;->body:Ljava/util/Map;

    invoke-static {v2}, Lcom/helpshift/common/domain/network/NetworkDataRequestUtil;->cleanData(Ljava/util/Map;)Ljava/util/Map;

    move-result-object v2

    invoke-virtual {p0, v2}, Lcom/helpshift/common/domain/network/POSTNetwork;->getQuery(Ljava/util/Map;)Ljava/lang/String;

    move-result-object v2

    .line 31
    invoke-virtual {p1}, Lcom/helpshift/common/platform/network/RequestData;->getRequestId()Ljava/lang/String;

    move-result-object v3

    invoke-virtual {p0, v3, p1}, Lcom/helpshift/common/domain/network/POSTNetwork;->getHeaders(Ljava/lang/String;Lcom/helpshift/common/platform/network/RequestData;)Ljava/util/List;

    move-result-object p1

    const/16 v3, 0x1388

    invoke-direct {v0, v1, v2, p1, v3}, Lcom/helpshift/common/platform/network/POSTRequest;-><init>(Ljava/lang/String;Ljava/lang/String;Ljava/util/List;I)V

    return-object v0
.end method

.method public bridge synthetic makeRequest(Lcom/helpshift/common/platform/network/RequestData;)Lcom/helpshift/common/platform/network/Response;
    .locals 0

    .line 23
    invoke-super {p0, p1}, Lcom/helpshift/common/domain/network/BaseNetwork;->makeRequest(Lcom/helpshift/common/platform/network/RequestData;)Lcom/helpshift/common/platform/network/Response;

    move-result-object p1

    return-object p1
.end method
