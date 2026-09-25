.class public Lcom/github/droidfu/http/BetterHttpResponseImpl;
.super Ljava/lang/Object;
.source "BetterHttpResponseImpl.java"

# interfaces
.implements Lcom/github/droidfu/http/BetterHttpResponse;


# instance fields
.field private entity:Lorg/apache/http/HttpEntity;

.field private response:Lorg/apache/http/HttpResponse;


# direct methods
.method public constructor <init>(Lorg/apache/http/HttpResponse;)V
    .locals 1
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    .line 31
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 32
    iput-object p1, p0, Lcom/github/droidfu/http/BetterHttpResponseImpl;->response:Lorg/apache/http/HttpResponse;

    .line 33
    invoke-interface {p1}, Lorg/apache/http/HttpResponse;->getEntity()Lorg/apache/http/HttpEntity;

    move-result-object p1

    if-eqz p1, :cond_0

    .line 35
    new-instance v0, Lorg/apache/http/entity/BufferedHttpEntity;

    invoke-direct {v0, p1}, Lorg/apache/http/entity/BufferedHttpEntity;-><init>(Lorg/apache/http/HttpEntity;)V

    iput-object v0, p0, Lcom/github/droidfu/http/BetterHttpResponseImpl;->entity:Lorg/apache/http/HttpEntity;

    :cond_0
    return-void
.end method


# virtual methods
.method public getHeader(Ljava/lang/String;)Ljava/lang/String;
    .locals 1

    .line 60
    iget-object v0, p0, Lcom/github/droidfu/http/BetterHttpResponseImpl;->response:Lorg/apache/http/HttpResponse;

    invoke-interface {v0, p1}, Lorg/apache/http/HttpResponse;->containsHeader(Ljava/lang/String;)Z

    move-result v0

    if-nez v0, :cond_0

    const/4 p1, 0x0

    return-object p1

    .line 63
    :cond_0
    iget-object v0, p0, Lcom/github/droidfu/http/BetterHttpResponseImpl;->response:Lorg/apache/http/HttpResponse;

    invoke-interface {v0, p1}, Lorg/apache/http/HttpResponse;->getFirstHeader(Ljava/lang/String;)Lorg/apache/http/Header;

    move-result-object p1

    invoke-interface {p1}, Lorg/apache/http/Header;->getValue()Ljava/lang/String;

    move-result-object p1

    return-object p1
.end method

.method public getResponseBody()Ljava/io/InputStream;
    .locals 1
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    .line 44
    iget-object v0, p0, Lcom/github/droidfu/http/BetterHttpResponseImpl;->entity:Lorg/apache/http/HttpEntity;

    invoke-interface {v0}, Lorg/apache/http/HttpEntity;->getContent()Ljava/io/InputStream;

    move-result-object v0

    return-object v0
.end method

.method public getResponseBodyAsBytes()[B
    .locals 1
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    .line 48
    iget-object v0, p0, Lcom/github/droidfu/http/BetterHttpResponseImpl;->entity:Lorg/apache/http/HttpEntity;

    invoke-static {v0}, Lorg/apache/http/util/EntityUtils;->toByteArray(Lorg/apache/http/HttpEntity;)[B

    move-result-object v0

    return-object v0
.end method

.method public getResponseBodyAsString()Ljava/lang/String;
    .locals 1
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    .line 52
    iget-object v0, p0, Lcom/github/droidfu/http/BetterHttpResponseImpl;->entity:Lorg/apache/http/HttpEntity;

    invoke-static {v0}, Lorg/apache/http/util/EntityUtils;->toString(Lorg/apache/http/HttpEntity;)Ljava/lang/String;

    move-result-object v0

    return-object v0
.end method

.method public getStatusCode()I
    .locals 1

    .line 56
    iget-object v0, p0, Lcom/github/droidfu/http/BetterHttpResponseImpl;->response:Lorg/apache/http/HttpResponse;

    invoke-interface {v0}, Lorg/apache/http/HttpResponse;->getStatusLine()Lorg/apache/http/StatusLine;

    move-result-object v0

    invoke-interface {v0}, Lorg/apache/http/StatusLine;->getStatusCode()I

    move-result v0

    return v0
.end method

.method public unwrap()Lorg/apache/http/HttpResponse;
    .locals 1

    .line 40
    iget-object v0, p0, Lcom/github/droidfu/http/BetterHttpResponseImpl;->response:Lorg/apache/http/HttpResponse;

    return-object v0
.end method
