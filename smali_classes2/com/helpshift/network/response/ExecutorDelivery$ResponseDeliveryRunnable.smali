.class Lcom/helpshift/network/response/ExecutorDelivery$ResponseDeliveryRunnable;
.super Ljava/lang/Object;
.source "ExecutorDelivery.java"

# interfaces
.implements Ljava/lang/Runnable;


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lcom/helpshift/network/response/ExecutorDelivery;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x2
    name = "ResponseDeliveryRunnable"
.end annotation


# instance fields
.field private final request:Lcom/helpshift/network/request/Request;

.field private final response:Lcom/helpshift/network/response/Response;

.field private final runnable:Ljava/lang/Runnable;

.field final synthetic this$0:Lcom/helpshift/network/response/ExecutorDelivery;


# direct methods
.method public constructor <init>(Lcom/helpshift/network/response/ExecutorDelivery;Lcom/helpshift/network/request/Request;Lcom/helpshift/network/response/Response;Ljava/lang/Runnable;)V
    .locals 0

    .line 64
    iput-object p1, p0, Lcom/helpshift/network/response/ExecutorDelivery$ResponseDeliveryRunnable;->this$0:Lcom/helpshift/network/response/ExecutorDelivery;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 65
    iput-object p2, p0, Lcom/helpshift/network/response/ExecutorDelivery$ResponseDeliveryRunnable;->request:Lcom/helpshift/network/request/Request;

    .line 66
    iput-object p3, p0, Lcom/helpshift/network/response/ExecutorDelivery$ResponseDeliveryRunnable;->response:Lcom/helpshift/network/response/Response;

    .line 67
    iput-object p4, p0, Lcom/helpshift/network/response/ExecutorDelivery$ResponseDeliveryRunnable;->runnable:Ljava/lang/Runnable;

    return-void
.end method


# virtual methods
.method public run()V
    .locals 2

    .line 73
    :try_start_0
    iget-object v0, p0, Lcom/helpshift/network/response/ExecutorDelivery$ResponseDeliveryRunnable;->response:Lcom/helpshift/network/response/Response;

    invoke-virtual {v0}, Lcom/helpshift/network/response/Response;->isSuccess()Z

    move-result v0

    if-eqz v0, :cond_0

    .line 74
    iget-object v0, p0, Lcom/helpshift/network/response/ExecutorDelivery$ResponseDeliveryRunnable;->request:Lcom/helpshift/network/request/Request;

    iget-object v1, p0, Lcom/helpshift/network/response/ExecutorDelivery$ResponseDeliveryRunnable;->response:Lcom/helpshift/network/response/Response;

    iget-object v1, v1, Lcom/helpshift/network/response/Response;->result:Ljava/lang/Object;

    invoke-virtual {v0, v1}, Lcom/helpshift/network/request/Request;->deliverResponse(Ljava/lang/Object;)V

    goto :goto_0

    .line 77
    :cond_0
    iget-object v0, p0, Lcom/helpshift/network/response/ExecutorDelivery$ResponseDeliveryRunnable;->request:Lcom/helpshift/network/request/Request;

    iget-object v1, p0, Lcom/helpshift/network/response/ExecutorDelivery$ResponseDeliveryRunnable;->response:Lcom/helpshift/network/response/Response;

    iget-object v1, v1, Lcom/helpshift/network/response/Response;->error:Lcom/helpshift/network/errors/NetworkError;

    invoke-virtual {v0, v1}, Lcom/helpshift/network/request/Request;->deliverError(Lcom/helpshift/network/errors/NetworkError;)V
    :try_end_0
    .catch Ljava/lang/Throwable; {:try_start_0 .. :try_end_0} :catch_0

    .line 83
    :catch_0
    :goto_0
    iget-object v0, p0, Lcom/helpshift/network/response/ExecutorDelivery$ResponseDeliveryRunnable;->request:Lcom/helpshift/network/request/Request;

    invoke-virtual {v0}, Lcom/helpshift/network/request/Request;->markDelivered()V

    .line 84
    iget-object v0, p0, Lcom/helpshift/network/response/ExecutorDelivery$ResponseDeliveryRunnable;->runnable:Ljava/lang/Runnable;

    if-eqz v0, :cond_1

    .line 85
    iget-object v0, p0, Lcom/helpshift/network/response/ExecutorDelivery$ResponseDeliveryRunnable;->runnable:Ljava/lang/Runnable;

    invoke-interface {v0}, Ljava/lang/Runnable;->run()V

    :cond_1
    return-void
.end method
