.class public Lcom/helpshift/common/domain/network/TSCorrectedNetwork;
.super Ljava/lang/Object;
.source "TSCorrectedNetwork.java"

# interfaces
.implements Lcom/helpshift/common/domain/network/Network;


# instance fields
.field private final network:Lcom/helpshift/common/domain/network/Network;

.field private final networkRequestDAO:Lcom/helpshift/common/platform/network/NetworkRequestDAO;


# direct methods
.method public constructor <init>(Lcom/helpshift/common/domain/network/Network;Lcom/helpshift/common/platform/Platform;)V
    .locals 0

    .line 20
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 21
    iput-object p1, p0, Lcom/helpshift/common/domain/network/TSCorrectedNetwork;->network:Lcom/helpshift/common/domain/network/Network;

    .line 22
    invoke-interface {p2}, Lcom/helpshift/common/platform/Platform;->getNetworkRequestDAO()Lcom/helpshift/common/platform/network/NetworkRequestDAO;

    move-result-object p1

    iput-object p1, p0, Lcom/helpshift/common/domain/network/TSCorrectedNetwork;->networkRequestDAO:Lcom/helpshift/common/platform/network/NetworkRequestDAO;

    return-void
.end method

.method private getHeaderValue(Ljava/util/List;Ljava/lang/String;)Ljava/lang/String;
    .locals 2
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/List<",
            "Lcom/helpshift/common/platform/network/KeyValuePair;",
            ">;",
            "Ljava/lang/String;",
            ")",
            "Ljava/lang/String;"
        }
    .end annotation

    .line 52
    invoke-interface {p1}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object p1

    :cond_0
    invoke-interface {p1}, Ljava/util/Iterator;->hasNext()Z

    move-result v0

    if-eqz v0, :cond_1

    invoke-interface {p1}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Lcom/helpshift/common/platform/network/KeyValuePair;

    .line 53
    iget-object v1, v0, Lcom/helpshift/common/platform/network/KeyValuePair;->key:Ljava/lang/String;

    if-eqz v1, :cond_0

    iget-object v1, v0, Lcom/helpshift/common/platform/network/KeyValuePair;->key:Ljava/lang/String;

    invoke-virtual {v1, p2}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v1

    if-eqz v1, :cond_0

    .line 54
    iget-object p1, v0, Lcom/helpshift/common/platform/network/KeyValuePair;->value:Ljava/lang/String;

    goto :goto_0

    :cond_1
    const/4 p1, 0x0

    :goto_0
    return-object p1
.end method

.method private makeRequest(Lcom/helpshift/common/platform/network/RequestData;I)Lcom/helpshift/common/platform/network/Response;
    .locals 3

    .line 31
    iget-object v0, p0, Lcom/helpshift/common/domain/network/TSCorrectedNetwork;->network:Lcom/helpshift/common/domain/network/Network;

    invoke-interface {v0, p1}, Lcom/helpshift/common/domain/network/Network;->makeRequest(Lcom/helpshift/common/platform/network/RequestData;)Lcom/helpshift/common/platform/network/Response;

    move-result-object v0

    .line 32
    iget v1, v0, Lcom/helpshift/common/platform/network/Response;->status:I

    const/16 v2, 0x1a6

    if-ne v1, v2, :cond_2

    if-eqz p2, :cond_1

    add-int/lit8 p2, p2, -0x1

    .line 35
    iget-object v0, v0, Lcom/helpshift/common/platform/network/Response;->headers:Ljava/util/List;

    const-string v1, "HS-UEpoch"

    invoke-direct {p0, v0, v1}, Lcom/helpshift/common/domain/network/TSCorrectedNetwork;->getHeaderValue(Ljava/util/List;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v0

    if-eqz v0, :cond_0

    .line 37
    iget-object v1, p0, Lcom/helpshift/common/domain/network/TSCorrectedNetwork;->networkRequestDAO:Lcom/helpshift/common/platform/network/NetworkRequestDAO;

    invoke-static {v0}, Lcom/helpshift/common/util/HSDateFormatSpec;->calculateTimeDelta(Ljava/lang/String;)F

    move-result v0

    invoke-interface {v1, v0}, Lcom/helpshift/common/platform/network/NetworkRequestDAO;->storeServerTimeDelta(F)V

    .line 39
    :cond_0
    new-instance v0, Lcom/helpshift/common/platform/network/RequestData;

    invoke-direct {v0, p1}, Lcom/helpshift/common/platform/network/RequestData;-><init>(Lcom/helpshift/common/platform/network/RequestData;)V

    invoke-direct {p0, v0, p2}, Lcom/helpshift/common/domain/network/TSCorrectedNetwork;->makeRequest(Lcom/helpshift/common/platform/network/RequestData;I)Lcom/helpshift/common/platform/network/Response;

    move-result-object p1

    return-object p1

    :cond_1
    const/4 p1, 0x0

    .line 42
    sget-object p2, Lcom/helpshift/common/exception/NetworkException;->TIMESTAMP_CORRECTION_RETRIES_EXHAUSTED:Lcom/helpshift/common/exception/NetworkException;

    invoke-static {p1, p2}, Lcom/helpshift/common/exception/RootAPIException;->wrap(Ljava/lang/Exception;Lcom/helpshift/common/exception/ExceptionType;)Lcom/helpshift/common/exception/RootAPIException;

    move-result-object p1

    throw p1

    :cond_2
    return-object v0
.end method


# virtual methods
.method public makeRequest(Lcom/helpshift/common/platform/network/RequestData;)Lcom/helpshift/common/platform/network/Response;
    .locals 1

    const/4 v0, 0x3

    .line 27
    invoke-direct {p0, p1, v0}, Lcom/helpshift/common/domain/network/TSCorrectedNetwork;->makeRequest(Lcom/helpshift/common/platform/network/RequestData;I)Lcom/helpshift/common/platform/network/Response;

    move-result-object p1

    return-object p1
.end method
