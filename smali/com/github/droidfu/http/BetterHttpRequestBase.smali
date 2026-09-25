.class public abstract Lcom/github/droidfu/http/BetterHttpRequestBase;
.super Ljava/lang/Object;
.source "BetterHttpRequestBase.java"

# interfaces
.implements Lcom/github/droidfu/http/BetterHttpRequest;
.implements Lorg/apache/http/client/ResponseHandler;


# annotations
.annotation system Ldalvik/annotation/Signature;
    value = {
        "Ljava/lang/Object;",
        "Lcom/github/droidfu/http/BetterHttpRequest;",
        "Lorg/apache/http/client/ResponseHandler<",
        "Lcom/github/droidfu/http/BetterHttpResponse;",
        ">;"
    }
.end annotation


# static fields
.field protected static final HTTP_CONTENT_TYPE_HEADER:Ljava/lang/String; = "Content-Type"

.field private static final MAX_RETRIES:I = 0x5


# instance fields
.field private executionCount:I

.field protected expectedStatusCodes:Ljava/util/List;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/List<",
            "Ljava/lang/Integer;",
            ">;"
        }
    .end annotation
.end field

.field protected httpClient:Lorg/apache/http/impl/client/AbstractHttpClient;

.field protected maxRetries:I

.field private oldTimeout:I

.field protected request:Lorg/apache/http/client/methods/HttpUriRequest;


# direct methods
.method constructor <init>(Lorg/apache/http/impl/client/AbstractHttpClient;)V
    .locals 1

    .line 57
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 45
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    iput-object v0, p0, Lcom/github/droidfu/http/BetterHttpRequestBase;->expectedStatusCodes:Ljava/util/List;

    const/4 v0, 0x5

    .line 51
    iput v0, p0, Lcom/github/droidfu/http/BetterHttpRequestBase;->maxRetries:I

    .line 58
    iput-object p1, p0, Lcom/github/droidfu/http/BetterHttpRequestBase;->httpClient:Lorg/apache/http/impl/client/AbstractHttpClient;

    return-void
.end method

.method private retryRequest(Lcom/github/droidfu/http/BetterHttpRequestRetryHandler;Ljava/io/IOException;Lorg/apache/http/protocol/HttpContext;)Z
    .locals 2

    const-string v0, "BetterHttp"

    const-string v1, "Intercepting exception that wasn\'t handled by HttpClient"

    .line 137
    invoke-static {v0, v1}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;)I

    .line 138
    iget v0, p0, Lcom/github/droidfu/http/BetterHttpRequestBase;->executionCount:I

    invoke-virtual {p1}, Lcom/github/droidfu/http/BetterHttpRequestRetryHandler;->getTimesRetried()I

    move-result v1

    invoke-static {v0, v1}, Ljava/lang/Math;->max(II)I

    move-result v0

    iput v0, p0, Lcom/github/droidfu/http/BetterHttpRequestBase;->executionCount:I

    .line 139
    iget v0, p0, Lcom/github/droidfu/http/BetterHttpRequestBase;->executionCount:I

    add-int/lit8 v0, v0, 0x1

    iput v0, p0, Lcom/github/droidfu/http/BetterHttpRequestBase;->executionCount:I

    invoke-virtual {p1, p2, v0, p3}, Lcom/github/droidfu/http/BetterHttpRequestRetryHandler;->retryRequest(Ljava/io/IOException;ILorg/apache/http/protocol/HttpContext;)Z

    move-result p1

    return p1
.end method


# virtual methods
.method public bridge varargs synthetic expecting([Ljava/lang/Integer;)Lcom/github/droidfu/http/BetterHttpRequest;
    .locals 0

    .line 1
    invoke-virtual {p0, p1}, Lcom/github/droidfu/http/BetterHttpRequestBase;->expecting([Ljava/lang/Integer;)Lcom/github/droidfu/http/BetterHttpRequestBase;

    move-result-object p1

    return-object p1
.end method

.method public varargs expecting([Ljava/lang/Integer;)Lcom/github/droidfu/http/BetterHttpRequestBase;
    .locals 0

    .line 70
    invoke-static {p1}, Ljava/util/Arrays;->asList([Ljava/lang/Object;)Ljava/util/List;

    move-result-object p1

    iput-object p1, p0, Lcom/github/droidfu/http/BetterHttpRequestBase;->expectedStatusCodes:Ljava/util/List;

    return-object p0
.end method

.method public getRequestUrl()Ljava/lang/String;
    .locals 1

    .line 66
    iget-object v0, p0, Lcom/github/droidfu/http/BetterHttpRequestBase;->request:Lorg/apache/http/client/methods/HttpUriRequest;

    invoke-interface {v0}, Lorg/apache/http/client/methods/HttpUriRequest;->getURI()Ljava/net/URI;

    move-result-object v0

    invoke-virtual {v0}, Ljava/net/URI;->toString()Ljava/lang/String;

    move-result-object v0

    return-object v0
.end method

.method public handleResponse(Lorg/apache/http/HttpResponse;)Lcom/github/droidfu/http/BetterHttpResponse;
    .locals 4
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    .line 143
    invoke-interface {p1}, Lorg/apache/http/HttpResponse;->getStatusLine()Lorg/apache/http/StatusLine;

    move-result-object v0

    invoke-interface {v0}, Lorg/apache/http/StatusLine;->getStatusCode()I

    move-result v0

    .line 144
    iget-object v1, p0, Lcom/github/droidfu/http/BetterHttpRequestBase;->expectedStatusCodes:Ljava/util/List;

    if-eqz v1, :cond_1

    iget-object v1, p0, Lcom/github/droidfu/http/BetterHttpRequestBase;->expectedStatusCodes:Ljava/util/List;

    invoke-interface {v1}, Ljava/util/List;->isEmpty()Z

    move-result v1

    if-nez v1, :cond_1

    .line 145
    iget-object v1, p0, Lcom/github/droidfu/http/BetterHttpRequestBase;->expectedStatusCodes:Ljava/util/List;

    invoke-static {v0}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v2

    invoke-interface {v1, v2}, Ljava/util/List;->contains(Ljava/lang/Object;)Z

    move-result v1

    if-eqz v1, :cond_0

    goto :goto_0

    .line 146
    :cond_0
    new-instance p1, Lorg/apache/http/client/HttpResponseException;

    new-instance v1, Ljava/lang/StringBuilder;

    const-string v2, "Unexpected status code: "

    invoke-direct {v1, v2}, Ljava/lang/StringBuilder;-><init>(Ljava/lang/String;)V

    invoke-virtual {v1, v0}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-direct {p1, v0, v1}, Lorg/apache/http/client/HttpResponseException;-><init>(ILjava/lang/String;)V

    throw p1

    .line 149
    :cond_1
    :goto_0
    new-instance v1, Lcom/github/droidfu/http/BetterHttpResponseImpl;

    invoke-direct {v1, p1}, Lcom/github/droidfu/http/BetterHttpResponseImpl;-><init>(Lorg/apache/http/HttpResponse;)V

    .line 150
    invoke-static {}, Lcom/github/droidfu/http/BetterHttp;->getResponseCache()Lcom/github/droidfu/cachefu/HttpResponseCache;

    move-result-object p1

    if-eqz p1, :cond_2

    .line 152
    new-instance v2, Lcom/github/droidfu/http/CachedHttpResponse$ResponseData;

    invoke-interface {v1}, Lcom/github/droidfu/http/BetterHttpResponse;->getResponseBodyAsBytes()[B

    move-result-object v3

    invoke-direct {v2, v0, v3}, Lcom/github/droidfu/http/CachedHttpResponse$ResponseData;-><init>(I[B)V

    .line 153
    invoke-virtual {p0}, Lcom/github/droidfu/http/BetterHttpRequestBase;->getRequestUrl()Ljava/lang/String;

    move-result-object v0

    invoke-virtual {p1, v0, v2}, Lcom/github/droidfu/cachefu/HttpResponseCache;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    :cond_2
    return-object v1
.end method

.method public bridge synthetic handleResponse(Lorg/apache/http/HttpResponse;)Ljava/lang/Object;
    .locals 0
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Lorg/apache/http/client/ClientProtocolException;,
            Ljava/io/IOException;
        }
    .end annotation

    .line 1
    invoke-virtual {p0, p1}, Lcom/github/droidfu/http/BetterHttpRequestBase;->handleResponse(Lorg/apache/http/HttpResponse;)Lcom/github/droidfu/http/BetterHttpResponse;

    move-result-object p1

    return-object p1
.end method

.method public bridge synthetic retries(I)Lcom/github/droidfu/http/BetterHttpRequest;
    .locals 0

    .line 1
    invoke-virtual {p0, p1}, Lcom/github/droidfu/http/BetterHttpRequestBase;->retries(I)Lcom/github/droidfu/http/BetterHttpRequestBase;

    move-result-object p1

    return-object p1
.end method

.method public retries(I)Lcom/github/droidfu/http/BetterHttpRequestBase;
    .locals 1

    if-gez p1, :cond_0

    const/4 p1, 0x0

    .line 76
    iput p1, p0, Lcom/github/droidfu/http/BetterHttpRequestBase;->maxRetries:I

    goto :goto_0

    :cond_0
    const/4 v0, 0x5

    if-le p1, v0, :cond_1

    .line 78
    iput v0, p0, Lcom/github/droidfu/http/BetterHttpRequestBase;->maxRetries:I

    goto :goto_0

    .line 80
    :cond_1
    iput p1, p0, Lcom/github/droidfu/http/BetterHttpRequestBase;->maxRetries:I

    :goto_0
    return-object p0
.end method

.method public send()Lcom/github/droidfu/http/BetterHttpResponse;
    .locals 6
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/net/ConnectException;
        }
    .end annotation

    .line 94
    new-instance v0, Lcom/github/droidfu/http/BetterHttpRequestRetryHandler;

    iget v1, p0, Lcom/github/droidfu/http/BetterHttpRequestBase;->maxRetries:I

    invoke-direct {v0, v1}, Lcom/github/droidfu/http/BetterHttpRequestRetryHandler;-><init>(I)V

    .line 97
    iget-object v1, p0, Lcom/github/droidfu/http/BetterHttpRequestBase;->httpClient:Lorg/apache/http/impl/client/AbstractHttpClient;

    invoke-virtual {v1, v0}, Lorg/apache/http/impl/client/AbstractHttpClient;->setHttpRequestRetryHandler(Lorg/apache/http/client/HttpRequestRetryHandler;)V

    .line 99
    new-instance v1, Lorg/apache/http/protocol/BasicHttpContext;

    invoke-direct {v1}, Lorg/apache/http/protocol/BasicHttpContext;-><init>()V

    const/4 v2, 0x1

    const/4 v3, 0x0

    :cond_0
    :goto_0
    if-eqz v2, :cond_3

    .line 111
    :try_start_0
    iget-object v2, p0, Lcom/github/droidfu/http/BetterHttpRequestBase;->httpClient:Lorg/apache/http/impl/client/AbstractHttpClient;

    iget-object v3, p0, Lcom/github/droidfu/http/BetterHttpRequestBase;->request:Lorg/apache/http/client/methods/HttpUriRequest;

    invoke-virtual {v2, v3, p0, v1}, Lorg/apache/http/impl/client/AbstractHttpClient;->execute(Lorg/apache/http/client/methods/HttpUriRequest;Lorg/apache/http/client/ResponseHandler;Lorg/apache/http/protocol/HttpContext;)Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Lcom/github/droidfu/http/BetterHttpResponse;
    :try_end_0
    .catch Ljava/io/IOException; {:try_start_0 .. :try_end_0} :catch_1
    .catch Ljava/lang/NullPointerException; {:try_start_0 .. :try_end_0} :catch_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    .line 123
    iget v0, p0, Lcom/github/droidfu/http/BetterHttpRequestBase;->oldTimeout:I

    invoke-static {}, Lcom/github/droidfu/http/BetterHttp;->getSocketTimeout()I

    move-result v1

    if-eq v0, v1, :cond_1

    .line 124
    iget v0, p0, Lcom/github/droidfu/http/BetterHttpRequestBase;->oldTimeout:I

    invoke-static {v0}, Lcom/github/droidfu/http/BetterHttp;->setSocketTimeout(I)V

    :cond_1
    return-object v2

    :catchall_0
    move-exception v0

    goto :goto_2

    :catch_0
    move-exception v2

    .line 119
    :try_start_1
    new-instance v3, Ljava/io/IOException;

    new-instance v4, Ljava/lang/StringBuilder;

    const-string v5, "NPE in HttpClient"

    invoke-direct {v4, v5}, Ljava/lang/StringBuilder;-><init>(Ljava/lang/String;)V

    invoke-virtual {v2}, Ljava/lang/NullPointerException;->getMessage()Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v4, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v4}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v2

    invoke-direct {v3, v2}, Ljava/io/IOException;-><init>(Ljava/lang/String;)V

    .line 120
    invoke-direct {p0, v0, v3, v1}, Lcom/github/droidfu/http/BetterHttpRequestBase;->retryRequest(Lcom/github/droidfu/http/BetterHttpRequestRetryHandler;Ljava/io/IOException;Lorg/apache/http/protocol/HttpContext;)Z

    move-result v2
    :try_end_1
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    .line 123
    iget v4, p0, Lcom/github/droidfu/http/BetterHttpRequestBase;->oldTimeout:I

    invoke-static {}, Lcom/github/droidfu/http/BetterHttp;->getSocketTimeout()I

    move-result v5

    if-eq v4, v5, :cond_0

    .line 124
    :goto_1
    iget v4, p0, Lcom/github/droidfu/http/BetterHttpRequestBase;->oldTimeout:I

    invoke-static {v4}, Lcom/github/droidfu/http/BetterHttp;->setSocketTimeout(I)V

    goto :goto_0

    :catch_1
    move-exception v2

    move-object v3, v2

    .line 114
    :try_start_2
    invoke-direct {p0, v0, v3, v1}, Lcom/github/droidfu/http/BetterHttpRequestBase;->retryRequest(Lcom/github/droidfu/http/BetterHttpRequestRetryHandler;Ljava/io/IOException;Lorg/apache/http/protocol/HttpContext;)Z

    move-result v2
    :try_end_2
    .catchall {:try_start_2 .. :try_end_2} :catchall_0

    .line 123
    iget v4, p0, Lcom/github/droidfu/http/BetterHttpRequestBase;->oldTimeout:I

    invoke-static {}, Lcom/github/droidfu/http/BetterHttp;->getSocketTimeout()I

    move-result v5

    if-eq v4, v5, :cond_0

    goto :goto_1

    :goto_2
    iget v1, p0, Lcom/github/droidfu/http/BetterHttpRequestBase;->oldTimeout:I

    invoke-static {}, Lcom/github/droidfu/http/BetterHttp;->getSocketTimeout()I

    move-result v2

    if-eq v1, v2, :cond_2

    .line 124
    iget v1, p0, Lcom/github/droidfu/http/BetterHttpRequestBase;->oldTimeout:I

    invoke-static {v1}, Lcom/github/droidfu/http/BetterHttp;->setSocketTimeout(I)V

    .line 126
    :cond_2
    throw v0

    .line 130
    :cond_3
    new-instance v0, Ljava/net/ConnectException;

    invoke-direct {v0}, Ljava/net/ConnectException;-><init>()V

    .line 131
    invoke-virtual {v0, v3}, Ljava/net/ConnectException;->initCause(Ljava/lang/Throwable;)Ljava/lang/Throwable;

    .line 132
    throw v0
.end method

.method public unwrap()Lorg/apache/http/client/methods/HttpUriRequest;
    .locals 1

    .line 62
    iget-object v0, p0, Lcom/github/droidfu/http/BetterHttpRequestBase;->request:Lorg/apache/http/client/methods/HttpUriRequest;

    return-object v0
.end method

.method public withTimeout(I)Lcom/github/droidfu/http/BetterHttpRequest;
    .locals 3

    .line 86
    iget-object v0, p0, Lcom/github/droidfu/http/BetterHttpRequestBase;->httpClient:Lorg/apache/http/impl/client/AbstractHttpClient;

    invoke-virtual {v0}, Lorg/apache/http/impl/client/AbstractHttpClient;->getParams()Lorg/apache/http/params/HttpParams;

    move-result-object v0

    const-string v1, "http.socket.timeout"

    const/16 v2, 0x7530

    invoke-interface {v0, v1, v2}, Lorg/apache/http/params/HttpParams;->getIntParameter(Ljava/lang/String;I)I

    move-result v0

    iput v0, p0, Lcom/github/droidfu/http/BetterHttpRequestBase;->oldTimeout:I

    .line 88
    invoke-static {p1}, Lcom/github/droidfu/http/BetterHttp;->setSocketTimeout(I)V

    return-object p0
.end method
