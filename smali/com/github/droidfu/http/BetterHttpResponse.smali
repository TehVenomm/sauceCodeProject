.class public interface abstract Lcom/github/droidfu/http/BetterHttpResponse;
.super Ljava/lang/Object;
.source "BetterHttpResponse.java"


# virtual methods
.method public abstract getHeader(Ljava/lang/String;)Ljava/lang/String;
.end method

.method public abstract getResponseBody()Ljava/io/InputStream;
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation
.end method

.method public abstract getResponseBodyAsBytes()[B
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation
.end method

.method public abstract getResponseBodyAsString()Ljava/lang/String;
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation
.end method

.method public abstract getStatusCode()I
.end method

.method public abstract unwrap()Lorg/apache/http/HttpResponse;
.end method
