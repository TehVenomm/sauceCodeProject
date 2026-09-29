.class public Lcom/github/droidfu/http/BetterHttp;
.super Ljava/lang/Object;
.source "BetterHttp.java"


# static fields
.field public static final DEFAULT_HTTP_USER_AGENT:Ljava/lang/String; = "Android/DroidFu"

.field public static final DEFAULT_MAX_CONNECTIONS:I = 0x4

.field public static final DEFAULT_SOCKET_TIMEOUT:I = 0x7530

.field static final LOG_TAG:Ljava/lang/String; = "BetterHttp"

.field private static appContext:Landroid/content/Context; = null

.field private static defaultHeaders:Ljava/util/HashMap; = null
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/HashMap<",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            ">;"
        }
    .end annotation
.end field

.field private static httpClient:Lorg/apache/http/impl/client/AbstractHttpClient; = null

.field private static maxConnections:I = 0x4

.field private static responseCache:Lcom/github/droidfu/cachefu/HttpResponseCache; = null

.field private static socketTimeout:I = 0x7530


# direct methods
.method static constructor <clinit>()V
    .locals 1

    .line 46
    new-instance v0, Ljava/util/HashMap;

    invoke-direct {v0}, Ljava/util/HashMap;-><init>()V

    sput-object v0, Lcom/github/droidfu/http/BetterHttp;->defaultHeaders:Ljava/util/HashMap;

    return-void
.end method

.method public constructor <init>()V
    .locals 0

    .line 35
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method

.method public static delete(Ljava/lang/String;)Lcom/github/droidfu/http/BetterHttpRequest;
    .locals 3

    .line 200
    new-instance v0, Lcom/github/droidfu/http/HttpDelete;

    sget-object v1, Lcom/github/droidfu/http/BetterHttp;->httpClient:Lorg/apache/http/impl/client/AbstractHttpClient;

    sget-object v2, Lcom/github/droidfu/http/BetterHttp;->defaultHeaders:Ljava/util/HashMap;

    invoke-direct {v0, v1, p0, v2}, Lcom/github/droidfu/http/HttpDelete;-><init>(Lorg/apache/http/impl/client/AbstractHttpClient;Ljava/lang/String;Ljava/util/HashMap;)V

    return-object v0
.end method

.method public static enableResponseCache(IJI)V
    .locals 1

    .line 95
    new-instance v0, Lcom/github/droidfu/cachefu/HttpResponseCache;

    invoke-direct {v0, p0, p1, p2, p3}, Lcom/github/droidfu/cachefu/HttpResponseCache;-><init>(IJI)V

    sput-object v0, Lcom/github/droidfu/http/BetterHttp;->responseCache:Lcom/github/droidfu/cachefu/HttpResponseCache;

    return-void
.end method

.method public static enableResponseCache(Landroid/content/Context;IJII)V
    .locals 0

    .line 121
    invoke-static {p1, p2, p3, p4}, Lcom/github/droidfu/http/BetterHttp;->enableResponseCache(IJI)V

    .line 122
    sget-object p1, Lcom/github/droidfu/http/BetterHttp;->responseCache:Lcom/github/droidfu/cachefu/HttpResponseCache;

    invoke-virtual {p1, p0, p5}, Lcom/github/droidfu/cachefu/HttpResponseCache;->enableDiskCache(Landroid/content/Context;I)Z

    return-void
.end method

.method public static get(Ljava/lang/String;)Lcom/github/droidfu/http/BetterHttpRequest;
    .locals 1

    const/4 v0, 0x0

    .line 173
    invoke-static {p0, v0}, Lcom/github/droidfu/http/BetterHttp;->get(Ljava/lang/String;Z)Lcom/github/droidfu/http/BetterHttpRequest;

    move-result-object p0

    return-object p0
.end method

.method public static get(Ljava/lang/String;Z)Lcom/github/droidfu/http/BetterHttpRequest;
    .locals 2

    if-eqz p1, :cond_0

    .line 177
    sget-object p1, Lcom/github/droidfu/http/BetterHttp;->responseCache:Lcom/github/droidfu/cachefu/HttpResponseCache;

    if-eqz p1, :cond_0

    sget-object p1, Lcom/github/droidfu/http/BetterHttp;->responseCache:Lcom/github/droidfu/cachefu/HttpResponseCache;

    invoke-virtual {p1, p0}, Lcom/github/droidfu/cachefu/HttpResponseCache;->containsKey(Ljava/lang/Object;)Z

    move-result p1

    if-eqz p1, :cond_0

    .line 178
    new-instance p1, Lcom/github/droidfu/http/CachedHttpRequest;

    invoke-direct {p1, p0}, Lcom/github/droidfu/http/CachedHttpRequest;-><init>(Ljava/lang/String;)V

    return-object p1

    .line 180
    :cond_0
    new-instance p1, Lcom/github/droidfu/http/HttpGet;

    sget-object v0, Lcom/github/droidfu/http/BetterHttp;->httpClient:Lorg/apache/http/impl/client/AbstractHttpClient;

    sget-object v1, Lcom/github/droidfu/http/BetterHttp;->defaultHeaders:Ljava/util/HashMap;

    invoke-direct {p1, v0, p0, v1}, Lcom/github/droidfu/http/HttpGet;-><init>(Lorg/apache/http/impl/client/AbstractHttpClient;Ljava/lang/String;Ljava/util/HashMap;)V

    return-object p1
.end method

.method public static getDefaultHeaders()Ljava/util/HashMap;
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()",
            "Ljava/util/HashMap<",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            ">;"
        }
    .end annotation

    .line 228
    sget-object v0, Lcom/github/droidfu/http/BetterHttp;->defaultHeaders:Ljava/util/HashMap;

    return-object v0
.end method

.method public static getHttpClient()Lorg/apache/http/impl/client/AbstractHttpClient;
    .locals 1

    .line 137
    sget-object v0, Lcom/github/droidfu/http/BetterHttp;->httpClient:Lorg/apache/http/impl/client/AbstractHttpClient;

    return-object v0
.end method

.method public static getResponseCache()Lcom/github/droidfu/cachefu/HttpResponseCache;
    .locals 1

    .line 129
    sget-object v0, Lcom/github/droidfu/http/BetterHttp;->responseCache:Lcom/github/droidfu/cachefu/HttpResponseCache;

    return-object v0
.end method

.method public static getSocketTimeout()I
    .locals 1

    .line 220
    sget v0, Lcom/github/droidfu/http/BetterHttp;->socketTimeout:I

    return v0
.end method

.method public static post(Ljava/lang/String;)Lcom/github/droidfu/http/BetterHttpRequest;
    .locals 3

    .line 184
    new-instance v0, Lcom/github/droidfu/http/HttpPost;

    sget-object v1, Lcom/github/droidfu/http/BetterHttp;->httpClient:Lorg/apache/http/impl/client/AbstractHttpClient;

    sget-object v2, Lcom/github/droidfu/http/BetterHttp;->defaultHeaders:Ljava/util/HashMap;

    invoke-direct {v0, v1, p0, v2}, Lcom/github/droidfu/http/HttpPost;-><init>(Lorg/apache/http/impl/client/AbstractHttpClient;Ljava/lang/String;Ljava/util/HashMap;)V

    return-object v0
.end method

.method public static post(Ljava/lang/String;Lorg/apache/http/HttpEntity;)Lcom/github/droidfu/http/BetterHttpRequest;
    .locals 3

    .line 188
    new-instance v0, Lcom/github/droidfu/http/HttpPost;

    sget-object v1, Lcom/github/droidfu/http/BetterHttp;->httpClient:Lorg/apache/http/impl/client/AbstractHttpClient;

    sget-object v2, Lcom/github/droidfu/http/BetterHttp;->defaultHeaders:Ljava/util/HashMap;

    invoke-direct {v0, v1, p0, p1, v2}, Lcom/github/droidfu/http/HttpPost;-><init>(Lorg/apache/http/impl/client/AbstractHttpClient;Ljava/lang/String;Lorg/apache/http/HttpEntity;Ljava/util/HashMap;)V

    return-object v0
.end method

.method public static put(Ljava/lang/String;)Lcom/github/droidfu/http/BetterHttpRequest;
    .locals 3

    .line 192
    new-instance v0, Lcom/github/droidfu/http/HttpPut;

    sget-object v1, Lcom/github/droidfu/http/BetterHttp;->httpClient:Lorg/apache/http/impl/client/AbstractHttpClient;

    sget-object v2, Lcom/github/droidfu/http/BetterHttp;->defaultHeaders:Ljava/util/HashMap;

    invoke-direct {v0, v1, p0, v2}, Lcom/github/droidfu/http/HttpPut;-><init>(Lorg/apache/http/impl/client/AbstractHttpClient;Ljava/lang/String;Ljava/util/HashMap;)V

    return-object v0
.end method

.method public static put(Ljava/lang/String;Lorg/apache/http/HttpEntity;)Lcom/github/droidfu/http/BetterHttpRequest;
    .locals 3

    .line 196
    new-instance v0, Lcom/github/droidfu/http/HttpPut;

    sget-object v1, Lcom/github/droidfu/http/BetterHttp;->httpClient:Lorg/apache/http/impl/client/AbstractHttpClient;

    sget-object v2, Lcom/github/droidfu/http/BetterHttp;->defaultHeaders:Ljava/util/HashMap;

    invoke-direct {v0, v1, p0, p1, v2}, Lcom/github/droidfu/http/HttpPut;-><init>(Lorg/apache/http/impl/client/AbstractHttpClient;Ljava/lang/String;Lorg/apache/http/HttpEntity;Ljava/util/HashMap;)V

    return-object v0
.end method

.method public static setContext(Landroid/content/Context;)V
    .locals 3

    .line 232
    sget-object v0, Lcom/github/droidfu/http/BetterHttp;->appContext:Landroid/content/Context;

    if-eqz v0, :cond_0

    return-void

    .line 235
    :cond_0
    invoke-virtual {p0}, Landroid/content/Context;->getApplicationContext()Landroid/content/Context;

    move-result-object p0

    sput-object p0, Lcom/github/droidfu/http/BetterHttp;->appContext:Landroid/content/Context;

    .line 236
    sget-object p0, Lcom/github/droidfu/http/BetterHttp;->appContext:Landroid/content/Context;

    new-instance v0, Lcom/github/droidfu/http/ConnectionChangedBroadcastReceiver;

    invoke-direct {v0}, Lcom/github/droidfu/http/ConnectionChangedBroadcastReceiver;-><init>()V

    new-instance v1, Landroid/content/IntentFilter;

    const-string v2, "android.net.conn.CONNECTIVITY_CHANGE"

    invoke-direct {v1, v2}, Landroid/content/IntentFilter;-><init>(Ljava/lang/String;)V

    invoke-virtual {p0, v0, v1}, Landroid/content/Context;->registerReceiver(Landroid/content/BroadcastReceiver;Landroid/content/IntentFilter;)Landroid/content/Intent;

    return-void
.end method

.method public static setDefaultHeader(Ljava/lang/String;Ljava/lang/String;)V
    .locals 1

    .line 224
    sget-object v0, Lcom/github/droidfu/http/BetterHttp;->defaultHeaders:Ljava/util/HashMap;

    invoke-virtual {v0, p0, p1}, Ljava/util/HashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    return-void
.end method

.method public static setHttpClient(Lorg/apache/http/impl/client/AbstractHttpClient;)V
    .locals 0

    .line 133
    sput-object p0, Lcom/github/droidfu/http/BetterHttp;->httpClient:Lorg/apache/http/impl/client/AbstractHttpClient;

    return-void
.end method

.method public static setMaximumConnections(I)V
    .locals 0

    .line 204
    sput p0, Lcom/github/droidfu/http/BetterHttp;->maxConnections:I

    return-void
.end method

.method public static setPortForScheme(Ljava/lang/String;I)V
    .locals 2

    .line 241
    new-instance v0, Lorg/apache/http/conn/scheme/Scheme;

    invoke-static {}, Lorg/apache/http/conn/scheme/PlainSocketFactory;->getSocketFactory()Lorg/apache/http/conn/scheme/PlainSocketFactory;

    move-result-object v1

    invoke-direct {v0, p0, v1, p1}, Lorg/apache/http/conn/scheme/Scheme;-><init>(Ljava/lang/String;Lorg/apache/http/conn/scheme/SocketFactory;I)V

    .line 242
    sget-object p0, Lcom/github/droidfu/http/BetterHttp;->httpClient:Lorg/apache/http/impl/client/AbstractHttpClient;

    invoke-virtual {p0}, Lorg/apache/http/impl/client/AbstractHttpClient;->getConnectionManager()Lorg/apache/http/conn/ClientConnectionManager;

    move-result-object p0

    invoke-interface {p0}, Lorg/apache/http/conn/ClientConnectionManager;->getSchemeRegistry()Lorg/apache/http/conn/scheme/SchemeRegistry;

    move-result-object p0

    invoke-virtual {p0, v0}, Lorg/apache/http/conn/scheme/SchemeRegistry;->register(Lorg/apache/http/conn/scheme/Scheme;)Lorg/apache/http/conn/scheme/Scheme;

    return-void
.end method

.method public static setSocketTimeout(I)V
    .locals 1

    .line 215
    sput p0, Lcom/github/droidfu/http/BetterHttp;->socketTimeout:I

    .line 216
    sget-object v0, Lcom/github/droidfu/http/BetterHttp;->httpClient:Lorg/apache/http/impl/client/AbstractHttpClient;

    invoke-virtual {v0}, Lorg/apache/http/impl/client/AbstractHttpClient;->getParams()Lorg/apache/http/params/HttpParams;

    move-result-object v0

    invoke-static {v0, p0}, Lorg/apache/http/params/HttpConnectionParams;->setSoTimeout(Lorg/apache/http/params/HttpParams;I)V

    return-void
.end method

.method public static setupHttpClient()V
    .locals 6

    .line 53
    new-instance v0, Lorg/apache/http/params/BasicHttpParams;

    invoke-direct {v0}, Lorg/apache/http/params/BasicHttpParams;-><init>()V

    .line 55
    sget v1, Lcom/github/droidfu/http/BetterHttp;->socketTimeout:I

    int-to-long v1, v1

    invoke-static {v0, v1, v2}, Lorg/apache/http/conn/params/ConnManagerParams;->setTimeout(Lorg/apache/http/params/HttpParams;J)V

    .line 56
    new-instance v1, Lorg/apache/http/conn/params/ConnPerRouteBean;

    sget v2, Lcom/github/droidfu/http/BetterHttp;->maxConnections:I

    invoke-direct {v1, v2}, Lorg/apache/http/conn/params/ConnPerRouteBean;-><init>(I)V

    invoke-static {v0, v1}, Lorg/apache/http/conn/params/ConnManagerParams;->setMaxConnectionsPerRoute(Lorg/apache/http/params/HttpParams;Lorg/apache/http/conn/params/ConnPerRoute;)V

    const/4 v1, 0x4

    .line 58
    invoke-static {v0, v1}, Lorg/apache/http/conn/params/ConnManagerParams;->setMaxTotalConnections(Lorg/apache/http/params/HttpParams;I)V

    .line 59
    sget v1, Lcom/github/droidfu/http/BetterHttp;->socketTimeout:I

    invoke-static {v0, v1}, Lorg/apache/http/params/HttpConnectionParams;->setSoTimeout(Lorg/apache/http/params/HttpParams;I)V

    const/4 v1, 0x1

    .line 60
    invoke-static {v0, v1}, Lorg/apache/http/params/HttpConnectionParams;->setTcpNoDelay(Lorg/apache/http/params/HttpParams;Z)V

    .line 61
    sget-object v1, Lorg/apache/http/HttpVersion;->HTTP_1_1:Lorg/apache/http/HttpVersion;

    invoke-static {v0, v1}, Lorg/apache/http/params/HttpProtocolParams;->setVersion(Lorg/apache/http/params/HttpParams;Lorg/apache/http/ProtocolVersion;)V

    const-string v1, "Android/DroidFu"

    .line 62
    invoke-static {v0, v1}, Lorg/apache/http/params/HttpProtocolParams;->setUserAgent(Lorg/apache/http/params/HttpParams;Ljava/lang/String;)V

    .line 64
    new-instance v1, Lorg/apache/http/conn/scheme/SchemeRegistry;

    invoke-direct {v1}, Lorg/apache/http/conn/scheme/SchemeRegistry;-><init>()V

    .line 65
    new-instance v2, Lorg/apache/http/conn/scheme/Scheme;

    const-string v3, "http"

    invoke-static {}, Lorg/apache/http/conn/scheme/PlainSocketFactory;->getSocketFactory()Lorg/apache/http/conn/scheme/PlainSocketFactory;

    move-result-object v4

    const/16 v5, 0x50

    invoke-direct {v2, v3, v4, v5}, Lorg/apache/http/conn/scheme/Scheme;-><init>(Ljava/lang/String;Lorg/apache/http/conn/scheme/SocketFactory;I)V

    invoke-virtual {v1, v2}, Lorg/apache/http/conn/scheme/SchemeRegistry;->register(Lorg/apache/http/conn/scheme/Scheme;)Lorg/apache/http/conn/scheme/Scheme;

    .line 66
    sget v2, Lcom/github/droidfu/support/DiagnosticSupport;->ANDROID_API_LEVEL:I

    const/16 v3, 0x1bb

    const/4 v4, 0x7

    if-lt v2, v4, :cond_0

    .line 67
    new-instance v2, Lorg/apache/http/conn/scheme/Scheme;

    const-string v4, "https"

    invoke-static {}, Lorg/apache/http/conn/ssl/SSLSocketFactory;->getSocketFactory()Lorg/apache/http/conn/ssl/SSLSocketFactory;

    move-result-object v5

    invoke-direct {v2, v4, v5, v3}, Lorg/apache/http/conn/scheme/Scheme;-><init>(Ljava/lang/String;Lorg/apache/http/conn/scheme/SocketFactory;I)V

    invoke-virtual {v1, v2}, Lorg/apache/http/conn/scheme/SchemeRegistry;->register(Lorg/apache/http/conn/scheme/Scheme;)Lorg/apache/http/conn/scheme/Scheme;

    goto :goto_0

    .line 72
    :cond_0
    new-instance v2, Lorg/apache/http/conn/scheme/Scheme;

    const-string v4, "https"

    new-instance v5, Lcom/github/droidfu/http/ssl/EasySSLSocketFactory;

    invoke-direct {v5}, Lcom/github/droidfu/http/ssl/EasySSLSocketFactory;-><init>()V

    invoke-direct {v2, v4, v5, v3}, Lorg/apache/http/conn/scheme/Scheme;-><init>(Ljava/lang/String;Lorg/apache/http/conn/scheme/SocketFactory;I)V

    invoke-virtual {v1, v2}, Lorg/apache/http/conn/scheme/SchemeRegistry;->register(Lorg/apache/http/conn/scheme/Scheme;)Lorg/apache/http/conn/scheme/Scheme;

    .line 75
    :goto_0
    new-instance v2, Lorg/apache/http/impl/conn/tsccm/ThreadSafeClientConnManager;

    invoke-direct {v2, v0, v1}, Lorg/apache/http/impl/conn/tsccm/ThreadSafeClientConnManager;-><init>(Lorg/apache/http/params/HttpParams;Lorg/apache/http/conn/scheme/SchemeRegistry;)V

    .line 76
    new-instance v1, Lorg/apache/http/impl/client/DefaultHttpClient;

    invoke-direct {v1, v2, v0}, Lorg/apache/http/impl/client/DefaultHttpClient;-><init>(Lorg/apache/http/conn/ClientConnectionManager;Lorg/apache/http/params/HttpParams;)V

    sput-object v1, Lcom/github/droidfu/http/BetterHttp;->httpClient:Lorg/apache/http/impl/client/AbstractHttpClient;

    return-void
.end method

.method public static updateProxySettings()V
    .locals 5

    .line 141
    sget-object v0, Lcom/github/droidfu/http/BetterHttp;->appContext:Landroid/content/Context;

    if-nez v0, :cond_0

    return-void

    .line 144
    :cond_0
    sget-object v0, Lcom/github/droidfu/http/BetterHttp;->httpClient:Lorg/apache/http/impl/client/AbstractHttpClient;

    invoke-virtual {v0}, Lorg/apache/http/impl/client/AbstractHttpClient;->getParams()Lorg/apache/http/params/HttpParams;

    move-result-object v0

    .line 145
    sget-object v1, Lcom/github/droidfu/http/BetterHttp;->appContext:Landroid/content/Context;

    const-string v2, "connectivity"

    invoke-virtual {v1, v2}, Landroid/content/Context;->getSystemService(Ljava/lang/String;)Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Landroid/net/ConnectivityManager;

    .line 147
    invoke-virtual {v1}, Landroid/net/ConnectivityManager;->getActiveNetworkInfo()Landroid/net/NetworkInfo;

    move-result-object v1

    if-nez v1, :cond_1

    return-void

    :cond_1
    const-string v2, "BetterHttp"

    .line 151
    invoke-virtual {v1}, Landroid/net/NetworkInfo;->toString()Ljava/lang/String;

    move-result-object v3

    invoke-static {v2, v3}, Landroid/util/Log;->i(Ljava/lang/String;Ljava/lang/String;)I

    .line 152
    invoke-virtual {v1}, Landroid/net/NetworkInfo;->getType()I

    move-result v1

    const/4 v2, 0x0

    if-nez v1, :cond_5

    .line 153
    sget-object v1, Lcom/github/droidfu/http/BetterHttp;->appContext:Landroid/content/Context;

    invoke-static {v1}, Landroid/net/Proxy;->getHost(Landroid/content/Context;)Ljava/lang/String;

    move-result-object v1

    if-nez v1, :cond_2

    .line 155
    invoke-static {}, Landroid/net/Proxy;->getDefaultHost()Ljava/lang/String;

    move-result-object v1

    .line 157
    :cond_2
    sget-object v3, Lcom/github/droidfu/http/BetterHttp;->appContext:Landroid/content/Context;

    invoke-static {v3}, Landroid/net/Proxy;->getPort(Landroid/content/Context;)I

    move-result v3

    const/4 v4, -0x1

    if-ne v3, v4, :cond_3

    .line 159
    invoke-static {}, Landroid/net/Proxy;->getDefaultPort()I

    move-result v3

    :cond_3
    if-eqz v1, :cond_4

    if-le v3, v4, :cond_4

    .line 162
    new-instance v2, Lorg/apache/http/HttpHost;

    invoke-direct {v2, v1, v3}, Lorg/apache/http/HttpHost;-><init>(Ljava/lang/String;I)V

    const-string v1, "http.route.default-proxy"

    .line 163
    invoke-interface {v0, v1, v2}, Lorg/apache/http/params/HttpParams;->setParameter(Ljava/lang/String;Ljava/lang/Object;)Lorg/apache/http/params/HttpParams;

    goto :goto_0

    :cond_4
    const-string v1, "http.route.default-proxy"

    .line 165
    invoke-interface {v0, v1, v2}, Lorg/apache/http/params/HttpParams;->setParameter(Ljava/lang/String;Ljava/lang/Object;)Lorg/apache/http/params/HttpParams;

    goto :goto_0

    :cond_5
    const-string v1, "http.route.default-proxy"

    .line 168
    invoke-interface {v0, v1, v2}, Lorg/apache/http/params/HttpParams;->setParameter(Ljava/lang/String;Ljava/lang/Object;)Lorg/apache/http/params/HttpParams;

    :goto_0
    return-void
.end method
