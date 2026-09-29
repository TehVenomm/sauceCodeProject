.class Lcom/helpshift/JavaCore$5;
.super Lcom/helpshift/common/domain/F;
.source "JavaCore.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lcom/helpshift/JavaCore;->sendRequestIdsForSuccessfulApiCalls()V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lcom/helpshift/JavaCore;


# direct methods
.method constructor <init>(Lcom/helpshift/JavaCore;)V
    .locals 0

    .line 372
    iput-object p1, p0, Lcom/helpshift/JavaCore$5;->this$0:Lcom/helpshift/JavaCore;

    invoke-direct {p0}, Lcom/helpshift/common/domain/F;-><init>()V

    return-void
.end method


# virtual methods
.method public f()V
    .locals 4

    const-string v0, "/clear-idempotent-cache/"

    .line 376
    new-instance v1, Lcom/helpshift/common/domain/network/POSTNetwork;

    iget-object v2, p0, Lcom/helpshift/JavaCore$5;->this$0:Lcom/helpshift/JavaCore;

    invoke-static {v2}, Lcom/helpshift/JavaCore;->access$100(Lcom/helpshift/JavaCore;)Lcom/helpshift/common/domain/Domain;

    move-result-object v2

    iget-object v3, p0, Lcom/helpshift/JavaCore$5;->this$0:Lcom/helpshift/JavaCore;

    iget-object v3, v3, Lcom/helpshift/JavaCore;->platform:Lcom/helpshift/common/platform/Platform;

    invoke-direct {v1, v0, v2, v3}, Lcom/helpshift/common/domain/network/POSTNetwork;-><init>(Ljava/lang/String;Lcom/helpshift/common/domain/Domain;Lcom/helpshift/common/platform/Platform;)V

    .line 377
    new-instance v0, Lcom/helpshift/common/domain/network/TSCorrectedNetwork;

    iget-object v2, p0, Lcom/helpshift/JavaCore$5;->this$0:Lcom/helpshift/JavaCore;

    iget-object v2, v2, Lcom/helpshift/JavaCore;->platform:Lcom/helpshift/common/platform/Platform;

    invoke-direct {v0, v1, v2}, Lcom/helpshift/common/domain/network/TSCorrectedNetwork;-><init>(Lcom/helpshift/common/domain/network/Network;Lcom/helpshift/common/platform/Platform;)V

    .line 379
    iget-object v1, p0, Lcom/helpshift/JavaCore$5;->this$0:Lcom/helpshift/JavaCore;

    iget-object v1, v1, Lcom/helpshift/JavaCore;->platform:Lcom/helpshift/common/platform/Platform;

    invoke-interface {v1}, Lcom/helpshift/common/platform/Platform;->getNetworkRequestDAO()Lcom/helpshift/common/platform/network/NetworkRequestDAO;

    move-result-object v1

    .line 380
    invoke-interface {v1}, Lcom/helpshift/common/platform/network/NetworkRequestDAO;->getAllSuccessfulRequestIds()Ljava/util/Set;

    move-result-object v1

    .line 381
    invoke-interface {v1}, Ljava/util/Set;->isEmpty()Z

    move-result v2

    if-nez v2, :cond_0

    .line 382
    iget-object v2, p0, Lcom/helpshift/JavaCore$5;->this$0:Lcom/helpshift/JavaCore;

    iget-object v2, v2, Lcom/helpshift/JavaCore;->platform:Lcom/helpshift/common/platform/Platform;

    invoke-interface {v2}, Lcom/helpshift/common/platform/Platform;->getJsonifier()Lcom/helpshift/common/platform/Jsonifier;

    move-result-object v2

    invoke-interface {v2, v1}, Lcom/helpshift/common/platform/Jsonifier;->jsonify(Ljava/util/Collection;)Ljava/lang/String;

    move-result-object v1

    .line 384
    new-instance v2, Ljava/util/HashMap;

    invoke-direct {v2}, Ljava/util/HashMap;-><init>()V

    const-string v3, "request_ids"

    .line 385
    invoke-interface {v2, v3, v1}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 386
    new-instance v1, Lcom/helpshift/common/platform/network/RequestData;

    invoke-direct {v1, v2}, Lcom/helpshift/common/platform/network/RequestData;-><init>(Ljava/util/Map;)V

    invoke-interface {v0, v1}, Lcom/helpshift/common/domain/network/Network;->makeRequest(Lcom/helpshift/common/platform/network/RequestData;)Lcom/helpshift/common/platform/network/Response;

    .line 387
    iget-object v0, p0, Lcom/helpshift/JavaCore$5;->this$0:Lcom/helpshift/JavaCore;

    iget-object v0, v0, Lcom/helpshift/JavaCore;->platform:Lcom/helpshift/common/platform/Platform;

    invoke-interface {v0}, Lcom/helpshift/common/platform/Platform;->getNetworkRequestDAO()Lcom/helpshift/common/platform/network/NetworkRequestDAO;

    move-result-object v0

    invoke-interface {v0}, Lcom/helpshift/common/platform/network/NetworkRequestDAO;->clearSuccessfulRequestIds()V

    :cond_0
    return-void
.end method
