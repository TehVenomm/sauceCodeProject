.class public interface abstract Lcom/github/droidfu/http/BetterHttpRequest;
.super Ljava/lang/Object;
.source "BetterHttpRequest.java"


# virtual methods
.method public varargs abstract expecting([Ljava/lang/Integer;)Lcom/github/droidfu/http/BetterHttpRequest;
.end method

.method public abstract getRequestUrl()Ljava/lang/String;
.end method

.method public abstract retries(I)Lcom/github/droidfu/http/BetterHttpRequest;
.end method

.method public abstract send()Lcom/github/droidfu/http/BetterHttpResponse;
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/net/ConnectException;
        }
    .end annotation
.end method

.method public abstract unwrap()Lorg/apache/http/client/methods/HttpUriRequest;
.end method

.method public abstract withTimeout(I)Lcom/github/droidfu/http/BetterHttpRequest;
.end method
