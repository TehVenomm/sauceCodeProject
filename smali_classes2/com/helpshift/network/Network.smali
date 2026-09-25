.class public interface abstract Lcom/helpshift/network/Network;
.super Ljava/lang/Object;
.source "Network.java"


# virtual methods
.method public abstract performRequest(Lcom/helpshift/network/request/Request;)Lcom/helpshift/network/response/NetworkResponse;
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Lcom/helpshift/network/errors/NetworkError;
        }
    .end annotation
.end method
