.class public Lcom/helpshift/common/domain/network/IdempotentNetwork;
.super Ljava/lang/Object;
.source "IdempotentNetwork.java"

# interfaces
.implements Lcom/helpshift/common/domain/network/Network;


# instance fields
.field private idempotentPolicy:Lcom/helpshift/common/domain/idempotent/IdempotentPolicy;

.field private network:Lcom/helpshift/common/domain/network/Network;

.field private networkRequestDAO:Lcom/helpshift/common/platform/network/NetworkRequestDAO;

.field private route:Ljava/lang/String;

.field private uniqueMappingKey:Ljava/lang/String;


# direct methods
.method public constructor <init>(Lcom/helpshift/common/domain/network/Network;Lcom/helpshift/common/platform/Platform;Lcom/helpshift/common/domain/idempotent/IdempotentPolicy;Ljava/lang/String;Ljava/lang/String;)V
    .locals 0

    .line 29
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 30
    iput-object p1, p0, Lcom/helpshift/common/domain/network/IdempotentNetwork;->network:Lcom/helpshift/common/domain/network/Network;

    .line 31
    iput-object p3, p0, Lcom/helpshift/common/domain/network/IdempotentNetwork;->idempotentPolicy:Lcom/helpshift/common/domain/idempotent/IdempotentPolicy;

    .line 32
    invoke-interface {p2}, Lcom/helpshift/common/platform/Platform;->getNetworkRequestDAO()Lcom/helpshift/common/platform/network/NetworkRequestDAO;

    move-result-object p1

    iput-object p1, p0, Lcom/helpshift/common/domain/network/IdempotentNetwork;->networkRequestDAO:Lcom/helpshift/common/platform/network/NetworkRequestDAO;

    .line 33
    iput-object p4, p0, Lcom/helpshift/common/domain/network/IdempotentNetwork;->route:Ljava/lang/String;

    .line 34
    iput-object p5, p0, Lcom/helpshift/common/domain/network/IdempotentNetwork;->uniqueMappingKey:Ljava/lang/String;

    return-void
.end method


# virtual methods
.method public makeRequest(Lcom/helpshift/common/platform/network/RequestData;)Lcom/helpshift/common/platform/network/Response;
    .locals 4

    .line 40
    iget-object v0, p0, Lcom/helpshift/common/domain/network/IdempotentNetwork;->networkRequestDAO:Lcom/helpshift/common/platform/network/NetworkRequestDAO;

    iget-object v1, p0, Lcom/helpshift/common/domain/network/IdempotentNetwork;->route:Ljava/lang/String;

    iget-object v2, p0, Lcom/helpshift/common/domain/network/IdempotentNetwork;->uniqueMappingKey:Ljava/lang/String;

    invoke-interface {v0, v1, v2}, Lcom/helpshift/common/platform/network/NetworkRequestDAO;->getPendingRequestId(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v0

    .line 42
    invoke-static {v0}, Lcom/helpshift/common/StringUtils;->isEmpty(Ljava/lang/String;)Z

    move-result v1

    if-eqz v1, :cond_0

    .line 43
    iget-object v0, p0, Lcom/helpshift/common/domain/network/IdempotentNetwork;->networkRequestDAO:Lcom/helpshift/common/platform/network/NetworkRequestDAO;

    iget-object v1, p0, Lcom/helpshift/common/domain/network/IdempotentNetwork;->route:Ljava/lang/String;

    iget-object v2, p0, Lcom/helpshift/common/domain/network/IdempotentNetwork;->uniqueMappingKey:Ljava/lang/String;

    .line 44
    invoke-virtual {p1}, Lcom/helpshift/common/platform/network/RequestData;->getRequestId()Ljava/lang/String;

    move-result-object v3

    .line 43
    invoke-interface {v0, v1, v2, v3}, Lcom/helpshift/common/platform/network/NetworkRequestDAO;->storePendingRequestId(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V

    goto :goto_0

    .line 47
    :cond_0
    invoke-virtual {p1, v0}, Lcom/helpshift/common/platform/network/RequestData;->overrideRequestId(Ljava/lang/String;)V

    .line 51
    :goto_0
    iget-object v0, p0, Lcom/helpshift/common/domain/network/IdempotentNetwork;->network:Lcom/helpshift/common/domain/network/Network;

    invoke-interface {v0, p1}, Lcom/helpshift/common/domain/network/Network;->makeRequest(Lcom/helpshift/common/platform/network/RequestData;)Lcom/helpshift/common/platform/network/Response;

    move-result-object v0

    if-eqz v0, :cond_1

    .line 52
    iget-object v1, p0, Lcom/helpshift/common/domain/network/IdempotentNetwork;->idempotentPolicy:Lcom/helpshift/common/domain/idempotent/IdempotentPolicy;

    iget v2, v0, Lcom/helpshift/common/platform/network/Response;->status:I

    invoke-interface {v1, v2}, Lcom/helpshift/common/domain/idempotent/IdempotentPolicy;->isRequestCompleted(I)Z

    move-result v1

    if-eqz v1, :cond_2

    .line 54
    :cond_1
    iget-object v1, p0, Lcom/helpshift/common/domain/network/IdempotentNetwork;->networkRequestDAO:Lcom/helpshift/common/platform/network/NetworkRequestDAO;

    iget-object v2, p0, Lcom/helpshift/common/domain/network/IdempotentNetwork;->route:Ljava/lang/String;

    iget-object v3, p0, Lcom/helpshift/common/domain/network/IdempotentNetwork;->uniqueMappingKey:Ljava/lang/String;

    invoke-interface {v1, v2, v3}, Lcom/helpshift/common/platform/network/NetworkRequestDAO;->deletePendingRequestId(Ljava/lang/String;Ljava/lang/String;)V

    .line 55
    iget-object v1, p0, Lcom/helpshift/common/domain/network/IdempotentNetwork;->networkRequestDAO:Lcom/helpshift/common/platform/network/NetworkRequestDAO;

    invoke-virtual {p1}, Lcom/helpshift/common/platform/network/RequestData;->getRequestId()Ljava/lang/String;

    move-result-object p1

    invoke-interface {v1, p1}, Lcom/helpshift/common/platform/network/NetworkRequestDAO;->storeSuccessfulRequestId(Ljava/lang/String;)V

    :cond_2
    return-object v0
.end method
