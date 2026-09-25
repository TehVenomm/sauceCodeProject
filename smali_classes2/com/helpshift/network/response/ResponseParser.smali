.class public interface abstract Lcom/helpshift/network/response/ResponseParser;
.super Ljava/lang/Object;
.source "ResponseParser.java"


# annotations
.annotation system Ldalvik/annotation/Signature;
    value = {
        "<T:",
        "Ljava/lang/Object;",
        ">",
        "Ljava/lang/Object;"
    }
.end annotation


# virtual methods
.method public abstract parseResponse(Lcom/helpshift/network/response/NetworkResponse;)Lcom/helpshift/network/response/Response;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Lcom/helpshift/network/response/NetworkResponse;",
            ")",
            "Lcom/helpshift/network/response/Response<",
            "TT;>;"
        }
    .end annotation
.end method
