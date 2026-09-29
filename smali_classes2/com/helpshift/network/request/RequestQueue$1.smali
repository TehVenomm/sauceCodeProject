.class Lcom/helpshift/network/request/RequestQueue$1;
.super Ljava/lang/Object;
.source "RequestQueue.java"

# interfaces
.implements Ljava/util/concurrent/Callable;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lcom/helpshift/network/request/RequestQueue;->add(Lcom/helpshift/network/request/Request;)Ljava/util/concurrent/Future;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lcom/helpshift/network/request/RequestQueue;

.field final synthetic val$request:Lcom/helpshift/network/request/Request;


# direct methods
.method constructor <init>(Lcom/helpshift/network/request/RequestQueue;Lcom/helpshift/network/request/Request;)V
    .locals 0

    .line 55
    iput-object p1, p0, Lcom/helpshift/network/request/RequestQueue$1;->this$0:Lcom/helpshift/network/request/RequestQueue;

    iput-object p2, p0, Lcom/helpshift/network/request/RequestQueue$1;->val$request:Lcom/helpshift/network/request/Request;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public call()Ljava/lang/Object;
    .locals 9
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/lang/Exception;
        }
    .end annotation

    .line 60
    :try_start_0
    iget-object v0, p0, Lcom/helpshift/network/request/RequestQueue$1;->this$0:Lcom/helpshift/network/request/RequestQueue;

    iget-object v0, v0, Lcom/helpshift/network/request/RequestQueue;->network:Lcom/helpshift/network/Network;

    iget-object v1, p0, Lcom/helpshift/network/request/RequestQueue$1;->val$request:Lcom/helpshift/network/request/Request;

    invoke-interface {v0, v1}, Lcom/helpshift/network/Network;->performRequest(Lcom/helpshift/network/request/Request;)Lcom/helpshift/network/response/NetworkResponse;

    move-result-object v0

    .line 62
    iget v1, v0, Lcom/helpshift/network/response/NetworkResponse;->statusCode:I

    const/16 v2, 0x12c

    if-lt v1, v2, :cond_0

    const-string v1, "HS_RequestQueue"

    .line 63
    new-instance v2, Ljava/lang/StringBuilder;

    invoke-direct {v2}, Ljava/lang/StringBuilder;-><init>()V

    const-string v3, "Api result : "

    invoke-virtual {v2, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v3, p0, Lcom/helpshift/network/request/RequestQueue$1;->val$request:Lcom/helpshift/network/request/Request;

    iget-object v3, v3, Lcom/helpshift/network/request/Request;->url:Ljava/lang/String;

    invoke-virtual {v2, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v3, ", Status : "

    invoke-virtual {v2, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget v3, v0, Lcom/helpshift/network/response/NetworkResponse;->statusCode:I

    invoke-virtual {v2, v3}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    invoke-virtual {v2}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v2

    invoke-static {v1, v2}, Lcom/helpshift/util/HSLogger;->d(Ljava/lang/String;Ljava/lang/String;)V

    .line 68
    :cond_0
    iget-boolean v1, v0, Lcom/helpshift/network/response/NetworkResponse;->notModified:Z

    if-eqz v1, :cond_2

    .line 69
    iget-object v0, p0, Lcom/helpshift/network/request/RequestQueue$1;->val$request:Lcom/helpshift/network/request/Request;

    invoke-virtual {v0}, Lcom/helpshift/network/request/Request;->hasHadResponseDelivered()Z

    move-result v0

    if-eqz v0, :cond_1

    const/4 v0, 0x0

    return-object v0

    .line 73
    :cond_1
    new-instance v0, Lcom/helpshift/network/errors/NetworkError;

    sget-object v1, Lcom/helpshift/common/domain/network/NetworkErrorCodes;->CONTENT_UNCHANGED:Ljava/lang/Integer;

    invoke-direct {v0, v1}, Lcom/helpshift/network/errors/NetworkError;-><init>(Ljava/lang/Integer;)V

    throw v0

    .line 76
    :cond_2
    iget-object v1, p0, Lcom/helpshift/network/request/RequestQueue$1;->val$request:Lcom/helpshift/network/request/Request;

    invoke-virtual {v1, v0}, Lcom/helpshift/network/request/Request;->parseNetworkResponse(Lcom/helpshift/network/response/NetworkResponse;)Lcom/helpshift/network/response/Response;

    move-result-object v0

    .line 77
    iget-object v1, p0, Lcom/helpshift/network/request/RequestQueue$1;->this$0:Lcom/helpshift/network/request/RequestQueue;

    iget-object v1, v1, Lcom/helpshift/network/request/RequestQueue;->delivery:Lcom/helpshift/network/response/ResponseDelivery;

    iget-object v2, p0, Lcom/helpshift/network/request/RequestQueue$1;->val$request:Lcom/helpshift/network/request/Request;

    invoke-interface {v1, v2, v0}, Lcom/helpshift/network/response/ResponseDelivery;->postResponse(Lcom/helpshift/network/request/Request;Lcom/helpshift/network/response/Response;)V
    :try_end_0
    .catch Lcom/helpshift/network/errors/NetworkError; {:try_start_0 .. :try_end_0} :catch_0

    return-object v0

    :catch_0
    move-exception v0

    const-string v1, "HS_RequestQueue"

    const-string v2, "Network error"

    const/4 v3, 0x1

    .line 81
    new-array v4, v3, [Ljava/lang/Throwable;

    const/4 v5, 0x0

    aput-object v0, v4, v5

    const/4 v6, 0x2

    new-array v6, v6, [Lcom/helpshift/logger/logmodels/ILogExtrasModel;

    const-string v7, "route"

    iget-object v8, p0, Lcom/helpshift/network/request/RequestQueue$1;->val$request:Lcom/helpshift/network/request/Request;

    iget-object v8, v8, Lcom/helpshift/network/request/Request;->url:Ljava/lang/String;

    .line 82
    invoke-static {v7, v8}, Lcom/helpshift/logger/logmodels/LogExtrasModelProvider;->fromString(Ljava/lang/String;Ljava/lang/String;)Lcom/helpshift/logger/logmodels/ILogExtrasModel;

    move-result-object v7

    aput-object v7, v6, v5

    const-string v5, "reason"

    new-instance v7, Ljava/lang/StringBuilder;

    invoke-direct {v7}, Ljava/lang/StringBuilder;-><init>()V

    .line 83
    invoke-virtual {v0}, Lcom/helpshift/network/errors/NetworkError;->getReason()Ljava/lang/Integer;

    move-result-object v8

    invoke-virtual {v7, v8}, Ljava/lang/StringBuilder;->append(Ljava/lang/Object;)Ljava/lang/StringBuilder;

    const-string v8, ""

    invoke-virtual {v7, v8}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v7}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v7

    invoke-static {v5, v7}, Lcom/helpshift/logger/logmodels/LogExtrasModelProvider;->fromString(Ljava/lang/String;Ljava/lang/String;)Lcom/helpshift/logger/logmodels/ILogExtrasModel;

    move-result-object v5

    aput-object v5, v6, v3

    .line 81
    invoke-static {v1, v2, v4, v6}, Lcom/helpshift/util/HSLogger;->e(Ljava/lang/String;Ljava/lang/String;[Ljava/lang/Throwable;[Lcom/helpshift/logger/logmodels/ILogExtrasModel;)V

    .line 84
    iget-object v1, p0, Lcom/helpshift/network/request/RequestQueue$1;->this$0:Lcom/helpshift/network/request/RequestQueue;

    iget-object v2, p0, Lcom/helpshift/network/request/RequestQueue$1;->val$request:Lcom/helpshift/network/request/Request;

    invoke-virtual {v1, v2, v0}, Lcom/helpshift/network/request/RequestQueue;->parseAndDeliverNetworkError(Lcom/helpshift/network/request/Request;Lcom/helpshift/network/errors/NetworkError;)V

    return-object v0
.end method
