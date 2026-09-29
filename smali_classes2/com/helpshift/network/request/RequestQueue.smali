.class public Lcom/helpshift/network/request/RequestQueue;
.super Ljava/lang/Object;
.source "RequestQueue.java"


# annotations
.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Lcom/helpshift/network/request/RequestQueue$DeliveryType;
    }
.end annotation


# static fields
.field private static final TAG:Ljava/lang/String; = "HS_RequestQueue"


# instance fields
.field final delivery:Lcom/helpshift/network/response/ResponseDelivery;

.field private dispatcherPool:Ljava/util/concurrent/ExecutorService;

.field final network:Lcom/helpshift/network/Network;


# direct methods
.method protected constructor <init>(Lcom/helpshift/network/Network;Lcom/helpshift/network/response/ResponseDelivery;Ljava/util/concurrent/ExecutorService;)V
    .locals 0

    .line 26
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 27
    iput-object p1, p0, Lcom/helpshift/network/request/RequestQueue;->network:Lcom/helpshift/network/Network;

    .line 28
    iput-object p3, p0, Lcom/helpshift/network/request/RequestQueue;->dispatcherPool:Ljava/util/concurrent/ExecutorService;

    .line 29
    iput-object p2, p0, Lcom/helpshift/network/request/RequestQueue;->delivery:Lcom/helpshift/network/response/ResponseDelivery;

    return-void
.end method

.method public static getRequestQueue(Lcom/helpshift/network/Network;Ljava/lang/Integer;Ljava/util/concurrent/ExecutorService;)Lcom/helpshift/network/request/RequestQueue;
    .locals 2

    .line 34
    sget-object v0, Lcom/helpshift/network/request/RequestQueue$DeliveryType;->ON_NEW_THREAD:Ljava/lang/Integer;

    invoke-virtual {v0, p1}, Ljava/lang/Integer;->equals(Ljava/lang/Object;)Z

    move-result p1

    if-eqz p1, :cond_0

    .line 35
    new-instance p1, Landroid/os/HandlerThread;

    const-string v0, "HS-cmnet-rspns"

    invoke-direct {p1, v0}, Landroid/os/HandlerThread;-><init>(Ljava/lang/String;)V

    .line 36
    invoke-virtual {p1}, Landroid/os/HandlerThread;->start()V

    .line 37
    invoke-virtual {p1}, Landroid/os/HandlerThread;->getLooper()Landroid/os/Looper;

    move-result-object p1

    .line 38
    new-instance v0, Landroid/os/Handler;

    invoke-direct {v0, p1}, Landroid/os/Handler;-><init>(Landroid/os/Looper;)V

    .line 39
    new-instance p1, Lcom/helpshift/network/response/ExecutorDelivery;

    invoke-direct {p1, v0}, Lcom/helpshift/network/response/ExecutorDelivery;-><init>(Landroid/os/Handler;)V

    goto :goto_0

    .line 42
    :cond_0
    new-instance p1, Lcom/helpshift/network/response/ExecutorDelivery;

    new-instance v0, Landroid/os/Handler;

    invoke-static {}, Landroid/os/Looper;->getMainLooper()Landroid/os/Looper;

    move-result-object v1

    invoke-direct {v0, v1}, Landroid/os/Handler;-><init>(Landroid/os/Looper;)V

    invoke-direct {p1, v0}, Lcom/helpshift/network/response/ExecutorDelivery;-><init>(Landroid/os/Handler;)V

    .line 44
    :goto_0
    new-instance v0, Lcom/helpshift/network/request/RequestQueue;

    invoke-direct {v0, p0, p1, p2}, Lcom/helpshift/network/request/RequestQueue;-><init>(Lcom/helpshift/network/Network;Lcom/helpshift/network/response/ResponseDelivery;Ljava/util/concurrent/ExecutorService;)V

    return-object v0
.end method


# virtual methods
.method public add(Lcom/helpshift/network/request/Request;)Ljava/util/concurrent/Future;
    .locals 2

    .line 55
    iget-object v0, p0, Lcom/helpshift/network/request/RequestQueue;->dispatcherPool:Ljava/util/concurrent/ExecutorService;

    new-instance v1, Lcom/helpshift/network/request/RequestQueue$1;

    invoke-direct {v1, p0, p1}, Lcom/helpshift/network/request/RequestQueue$1;-><init>(Lcom/helpshift/network/request/RequestQueue;Lcom/helpshift/network/request/Request;)V

    invoke-interface {v0, v1}, Ljava/util/concurrent/ExecutorService;->submit(Ljava/util/concurrent/Callable;)Ljava/util/concurrent/Future;

    move-result-object p1

    return-object p1
.end method

.method protected parseAndDeliverNetworkError(Lcom/helpshift/network/request/Request;Lcom/helpshift/network/errors/NetworkError;)V
    .locals 1

    .line 92
    invoke-virtual {p1, p2}, Lcom/helpshift/network/request/Request;->parseNetworkError(Lcom/helpshift/network/errors/NetworkError;)Lcom/helpshift/network/errors/NetworkError;

    move-result-object p2

    .line 93
    iget-object v0, p0, Lcom/helpshift/network/request/RequestQueue;->delivery:Lcom/helpshift/network/response/ResponseDelivery;

    invoke-interface {v0, p1, p2}, Lcom/helpshift/network/response/ResponseDelivery;->postError(Lcom/helpshift/network/request/Request;Lcom/helpshift/network/errors/NetworkError;)V

    return-void
.end method
