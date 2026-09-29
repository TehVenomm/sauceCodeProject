.class public interface abstract Lcom/helpshift/network/response/ResponseDelivery;
.super Ljava/lang/Object;
.source "ResponseDelivery.java"


# virtual methods
.method public abstract postError(Lcom/helpshift/network/request/Request;Lcom/helpshift/network/errors/NetworkError;)V
.end method

.method public abstract postResponse(Lcom/helpshift/network/request/Request;Lcom/helpshift/network/response/Response;)V
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Lcom/helpshift/network/request/Request;",
            "Lcom/helpshift/network/response/Response<",
            "*>;)V"
        }
    .end annotation
.end method
