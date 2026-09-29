.class public Lcom/helpshift/network/response/ExecutorDelivery;
.super Ljava/lang/Object;
.source "ExecutorDelivery.java"

# interfaces
.implements Lcom/helpshift/network/response/ResponseDelivery;


# annotations
.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Lcom/helpshift/network/response/ExecutorDelivery$ResponseDeliveryRunnable;
    }
.end annotation


# instance fields
.field private final responsePoster:Ljava/util/concurrent/Executor;


# direct methods
.method public constructor <init>(Landroid/os/Handler;)V
    .locals 1

    .line 39
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 40
    new-instance v0, Lcom/helpshift/network/response/ExecutorDelivery$1;

    invoke-direct {v0, p0, p1}, Lcom/helpshift/network/response/ExecutorDelivery$1;-><init>(Lcom/helpshift/network/response/ExecutorDelivery;Landroid/os/Handler;)V

    iput-object v0, p0, Lcom/helpshift/network/response/ExecutorDelivery;->responsePoster:Ljava/util/concurrent/Executor;

    return-void
.end method


# virtual methods
.method public postError(Lcom/helpshift/network/request/Request;Lcom/helpshift/network/errors/NetworkError;)V
    .locals 3

    .line 55
    invoke-virtual {p1}, Lcom/helpshift/network/request/Request;->getSequence()I

    move-result v0

    invoke-static {v0}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v0

    invoke-static {p2, v0}, Lcom/helpshift/network/response/Response;->error(Lcom/helpshift/network/errors/NetworkError;Ljava/lang/Integer;)Lcom/helpshift/network/response/Response;

    move-result-object p2

    .line 56
    iget-object v0, p0, Lcom/helpshift/network/response/ExecutorDelivery;->responsePoster:Ljava/util/concurrent/Executor;

    new-instance v1, Lcom/helpshift/network/response/ExecutorDelivery$ResponseDeliveryRunnable;

    const/4 v2, 0x0

    invoke-direct {v1, p0, p1, p2, v2}, Lcom/helpshift/network/response/ExecutorDelivery$ResponseDeliveryRunnable;-><init>(Lcom/helpshift/network/response/ExecutorDelivery;Lcom/helpshift/network/request/Request;Lcom/helpshift/network/response/Response;Ljava/lang/Runnable;)V

    invoke-interface {v0, v1}, Ljava/util/concurrent/Executor;->execute(Ljava/lang/Runnable;)V

    return-void
.end method

.method public postResponse(Lcom/helpshift/network/request/Request;Lcom/helpshift/network/response/Response;)V
    .locals 3
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Lcom/helpshift/network/request/Request;",
            "Lcom/helpshift/network/response/Response<",
            "*>;)V"
        }
    .end annotation

    .line 50
    iget-object v0, p0, Lcom/helpshift/network/response/ExecutorDelivery;->responsePoster:Ljava/util/concurrent/Executor;

    new-instance v1, Lcom/helpshift/network/response/ExecutorDelivery$ResponseDeliveryRunnable;

    const/4 v2, 0x0

    invoke-direct {v1, p0, p1, p2, v2}, Lcom/helpshift/network/response/ExecutorDelivery$ResponseDeliveryRunnable;-><init>(Lcom/helpshift/network/response/ExecutorDelivery;Lcom/helpshift/network/request/Request;Lcom/helpshift/network/response/Response;Ljava/lang/Runnable;)V

    invoke-interface {v0, v1}, Ljava/util/concurrent/Executor;->execute(Ljava/lang/Runnable;)V

    return-void
.end method
