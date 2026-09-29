.class public Lcom/github/droidfu/http/CachedHttpResponse;
.super Ljava/lang/Object;
.source "CachedHttpResponse.java"

# interfaces
.implements Lcom/github/droidfu/http/BetterHttpResponse;


# annotations
.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Lcom/github/droidfu/http/CachedHttpResponse$ResponseData;
    }
.end annotation


# instance fields
.field private cachedData:Lcom/github/droidfu/http/CachedHttpResponse$ResponseData;

.field private responseCache:Lcom/github/droidfu/cachefu/HttpResponseCache;


# direct methods
.method public constructor <init>(Ljava/lang/String;)V
    .locals 1

    .line 40
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 41
    invoke-static {}, Lcom/github/droidfu/http/BetterHttp;->getResponseCache()Lcom/github/droidfu/cachefu/HttpResponseCache;

    move-result-object v0

    iput-object v0, p0, Lcom/github/droidfu/http/CachedHttpResponse;->responseCache:Lcom/github/droidfu/cachefu/HttpResponseCache;

    .line 42
    iget-object v0, p0, Lcom/github/droidfu/http/CachedHttpResponse;->responseCache:Lcom/github/droidfu/cachefu/HttpResponseCache;

    invoke-virtual {v0, p1}, Lcom/github/droidfu/cachefu/HttpResponseCache;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object p1

    check-cast p1, Lcom/github/droidfu/http/CachedHttpResponse$ResponseData;

    iput-object p1, p0, Lcom/github/droidfu/http/CachedHttpResponse;->cachedData:Lcom/github/droidfu/http/CachedHttpResponse$ResponseData;

    return-void
.end method


# virtual methods
.method public getHeader(Ljava/lang/String;)Ljava/lang/String;
    .locals 0

    const/4 p1, 0x0

    return-object p1
.end method

.method public getResponseBody()Ljava/io/InputStream;
    .locals 2
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    .line 50
    new-instance v0, Ljava/io/ByteArrayInputStream;

    iget-object v1, p0, Lcom/github/droidfu/http/CachedHttpResponse;->cachedData:Lcom/github/droidfu/http/CachedHttpResponse$ResponseData;

    invoke-static {v1}, Lcom/github/droidfu/http/CachedHttpResponse$ResponseData;->access$0(Lcom/github/droidfu/http/CachedHttpResponse$ResponseData;)[B

    move-result-object v1

    invoke-direct {v0, v1}, Ljava/io/ByteArrayInputStream;-><init>([B)V

    return-object v0
.end method

.method public getResponseBodyAsBytes()[B
    .locals 1
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    .line 54
    iget-object v0, p0, Lcom/github/droidfu/http/CachedHttpResponse;->cachedData:Lcom/github/droidfu/http/CachedHttpResponse$ResponseData;

    invoke-static {v0}, Lcom/github/droidfu/http/CachedHttpResponse$ResponseData;->access$0(Lcom/github/droidfu/http/CachedHttpResponse$ResponseData;)[B

    move-result-object v0

    return-object v0
.end method

.method public getResponseBodyAsString()Ljava/lang/String;
    .locals 2
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    .line 58
    new-instance v0, Ljava/lang/String;

    iget-object v1, p0, Lcom/github/droidfu/http/CachedHttpResponse;->cachedData:Lcom/github/droidfu/http/CachedHttpResponse$ResponseData;

    invoke-static {v1}, Lcom/github/droidfu/http/CachedHttpResponse$ResponseData;->access$0(Lcom/github/droidfu/http/CachedHttpResponse$ResponseData;)[B

    move-result-object v1

    invoke-direct {v0, v1}, Ljava/lang/String;-><init>([B)V

    return-object v0
.end method

.method public getStatusCode()I
    .locals 1

    .line 62
    iget-object v0, p0, Lcom/github/droidfu/http/CachedHttpResponse;->cachedData:Lcom/github/droidfu/http/CachedHttpResponse$ResponseData;

    invoke-static {v0}, Lcom/github/droidfu/http/CachedHttpResponse$ResponseData;->access$1(Lcom/github/droidfu/http/CachedHttpResponse$ResponseData;)I

    move-result v0

    return v0
.end method

.method public unwrap()Lorg/apache/http/HttpResponse;
    .locals 1

    const/4 v0, 0x0

    return-object v0
.end method
