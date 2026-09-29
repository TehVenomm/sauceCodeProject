.class public Lcom/helpshift/common/domain/network/FailedAPICallNetworkDecorator;
.super Ljava/lang/Object;
.source "FailedAPICallNetworkDecorator.java"

# interfaces
.implements Lcom/helpshift/common/domain/network/Network;


# instance fields
.field private final network:Lcom/helpshift/common/domain/network/Network;


# direct methods
.method public constructor <init>(Lcom/helpshift/common/domain/network/Network;)V
    .locals 0

    .line 12
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 13
    iput-object p1, p0, Lcom/helpshift/common/domain/network/FailedAPICallNetworkDecorator;->network:Lcom/helpshift/common/domain/network/Network;

    return-void
.end method

.method private isStatusCodeNonRetriable(I)Z
    .locals 1

    .line 30
    sget-object v0, Lcom/helpshift/common/domain/network/NetworkErrorCodes;->NOT_RETRIABLE_STATUS_CODES:Ljava/util/Set;

    invoke-static {p1}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object p1

    invoke-interface {v0, p1}, Ljava/util/Set;->contains(Ljava/lang/Object;)Z

    move-result p1

    return p1
.end method


# virtual methods
.method public makeRequest(Lcom/helpshift/common/platform/network/RequestData;)Lcom/helpshift/common/platform/network/Response;
    .locals 2

    .line 18
    iget-object v0, p0, Lcom/helpshift/common/domain/network/FailedAPICallNetworkDecorator;->network:Lcom/helpshift/common/domain/network/Network;

    invoke-interface {v0, p1}, Lcom/helpshift/common/domain/network/Network;->makeRequest(Lcom/helpshift/common/platform/network/RequestData;)Lcom/helpshift/common/platform/network/Response;

    move-result-object p1

    .line 19
    iget v0, p1, Lcom/helpshift/common/platform/network/Response;->status:I

    .line 21
    invoke-direct {p0, v0}, Lcom/helpshift/common/domain/network/FailedAPICallNetworkDecorator;->isStatusCodeNonRetriable(I)Z

    move-result v1

    if-nez v1, :cond_0

    return-object p1

    .line 22
    :cond_0
    sget-object p1, Lcom/helpshift/common/exception/NetworkException;->NON_RETRIABLE:Lcom/helpshift/common/exception/NetworkException;

    .line 23
    iput v0, p1, Lcom/helpshift/common/exception/NetworkException;->serverStatusCode:I

    const/4 v0, 0x0

    .line 24
    invoke-static {v0, p1}, Lcom/helpshift/common/exception/RootAPIException;->wrap(Ljava/lang/Exception;Lcom/helpshift/common/exception/ExceptionType;)Lcom/helpshift/common/exception/RootAPIException;

    move-result-object p1

    throw p1
.end method
