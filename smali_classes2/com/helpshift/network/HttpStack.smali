.class public interface abstract Lcom/helpshift/network/HttpStack;
.super Ljava/lang/Object;
.source "HttpStack.java"


# virtual methods
.method public abstract performRequest(Lcom/helpshift/network/request/Request;)Lcom/helpshift/network/HttpResponse;
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;,
            Lcom/helpshift/exceptions/InstallException;
        }
    .end annotation
.end method
