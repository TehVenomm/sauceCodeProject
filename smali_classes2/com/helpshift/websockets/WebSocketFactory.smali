.class public Lcom/helpshift/websockets/WebSocketFactory;
.super Ljava/lang/Object;
.source "WebSocketFactory.java"


# instance fields
.field private mConnectionTimeout:I

.field private final mProxySettings:Lcom/helpshift/websockets/ProxySettings;

.field private final mSocketFactorySettings:Lcom/helpshift/websockets/SocketFactorySettings;


# direct methods
.method public constructor <init>()V
    .locals 1

    .line 44
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 45
    new-instance v0, Lcom/helpshift/websockets/SocketFactorySettings;

    invoke-direct {v0}, Lcom/helpshift/websockets/SocketFactorySettings;-><init>()V

    iput-object v0, p0, Lcom/helpshift/websockets/WebSocketFactory;->mSocketFactorySettings:Lcom/helpshift/websockets/SocketFactorySettings;

    .line 46
    new-instance v0, Lcom/helpshift/websockets/ProxySettings;

    invoke-direct {v0, p0}, Lcom/helpshift/websockets/ProxySettings;-><init>(Lcom/helpshift/websockets/WebSocketFactory;)V

    iput-object v0, p0, Lcom/helpshift/websockets/WebSocketFactory;->mProxySettings:Lcom/helpshift/websockets/ProxySettings;

    return-void
.end method

.method private createDirectRawSocket(Ljava/lang/String;IZI)Lcom/helpshift/websockets/SocketConnector;
    .locals 1
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    .line 495
    iget-object v0, p0, Lcom/helpshift/websockets/WebSocketFactory;->mSocketFactorySettings:Lcom/helpshift/websockets/SocketFactorySettings;

    invoke-virtual {v0, p3}, Lcom/helpshift/websockets/SocketFactorySettings;->selectSocketFactory(Z)Ljavax/net/SocketFactory;

    move-result-object p3

    .line 498
    invoke-virtual {p3}, Ljavax/net/SocketFactory;->createSocket()Ljava/net/Socket;

    move-result-object p3

    .line 501
    new-instance v0, Lcom/helpshift/websockets/Address;

    invoke-direct {v0, p1, p2}, Lcom/helpshift/websockets/Address;-><init>(Ljava/lang/String;I)V

    .line 504
    new-instance p1, Lcom/helpshift/websockets/SocketConnector;

    invoke-direct {p1, p3, v0, p4}, Lcom/helpshift/websockets/SocketConnector;-><init>(Ljava/net/Socket;Lcom/helpshift/websockets/Address;I)V

    return-object p1
.end method

.method private createProxiedRawSocket(Ljava/lang/String;IZI)Lcom/helpshift/websockets/SocketConnector;
    .locals 10
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    .line 470
    iget-object v0, p0, Lcom/helpshift/websockets/WebSocketFactory;->mProxySettings:Lcom/helpshift/websockets/ProxySettings;

    invoke-virtual {v0}, Lcom/helpshift/websockets/ProxySettings;->getPort()I

    move-result v0

    iget-object v1, p0, Lcom/helpshift/websockets/WebSocketFactory;->mProxySettings:Lcom/helpshift/websockets/ProxySettings;

    invoke-virtual {v1}, Lcom/helpshift/websockets/ProxySettings;->isSecure()Z

    move-result v1

    invoke-static {v0, v1}, Lcom/helpshift/websockets/WebSocketFactory;->determinePort(IZ)I

    move-result v0

    .line 473
    iget-object v1, p0, Lcom/helpshift/websockets/WebSocketFactory;->mProxySettings:Lcom/helpshift/websockets/ProxySettings;

    invoke-virtual {v1}, Lcom/helpshift/websockets/ProxySettings;->selectSocketFactory()Ljavax/net/SocketFactory;

    move-result-object v1

    .line 476
    invoke-virtual {v1}, Ljavax/net/SocketFactory;->createSocket()Ljava/net/Socket;

    move-result-object v3

    .line 479
    new-instance v4, Lcom/helpshift/websockets/Address;

    iget-object v1, p0, Lcom/helpshift/websockets/WebSocketFactory;->mProxySettings:Lcom/helpshift/websockets/ProxySettings;

    invoke-virtual {v1}, Lcom/helpshift/websockets/ProxySettings;->getHost()Ljava/lang/String;

    move-result-object v1

    invoke-direct {v4, v1, v0}, Lcom/helpshift/websockets/Address;-><init>(Ljava/lang/String;I)V

    .line 482
    new-instance v6, Lcom/helpshift/websockets/ProxyHandshaker;

    iget-object v0, p0, Lcom/helpshift/websockets/WebSocketFactory;->mProxySettings:Lcom/helpshift/websockets/ProxySettings;

    invoke-direct {v6, v3, p1, p2, v0}, Lcom/helpshift/websockets/ProxyHandshaker;-><init>(Ljava/net/Socket;Ljava/lang/String;ILcom/helpshift/websockets/ProxySettings;)V

    if-eqz p3, :cond_0

    .line 485
    iget-object v0, p0, Lcom/helpshift/websockets/WebSocketFactory;->mSocketFactorySettings:Lcom/helpshift/websockets/SocketFactorySettings;

    .line 486
    invoke-virtual {v0, p3}, Lcom/helpshift/websockets/SocketFactorySettings;->selectSocketFactory(Z)Ljavax/net/SocketFactory;

    move-result-object p3

    check-cast p3, Ljavax/net/ssl/SSLSocketFactory;

    :goto_0
    move-object v7, p3

    goto :goto_1

    :cond_0
    const/4 p3, 0x0

    goto :goto_0

    .line 489
    :goto_1
    new-instance p3, Lcom/helpshift/websockets/SocketConnector;

    move-object v2, p3

    move v5, p4

    move-object v8, p1

    move v9, p2

    invoke-direct/range {v2 .. v9}, Lcom/helpshift/websockets/SocketConnector;-><init>(Ljava/net/Socket;Lcom/helpshift/websockets/Address;ILcom/helpshift/websockets/ProxyHandshaker;Ljavax/net/ssl/SSLSocketFactory;Ljava/lang/String;I)V

    return-object p3
.end method

.method private createRawSocket(Ljava/lang/String;IZI)Lcom/helpshift/websockets/SocketConnector;
    .locals 1
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    .line 447
    invoke-static {p2, p3}, Lcom/helpshift/websockets/WebSocketFactory;->determinePort(IZ)I

    move-result p2

    .line 450
    iget-object v0, p0, Lcom/helpshift/websockets/WebSocketFactory;->mProxySettings:Lcom/helpshift/websockets/ProxySettings;

    invoke-virtual {v0}, Lcom/helpshift/websockets/ProxySettings;->getHost()Ljava/lang/String;

    move-result-object v0

    if-eqz v0, :cond_0

    const/4 v0, 0x1

    goto :goto_0

    :cond_0
    const/4 v0, 0x0

    :goto_0
    if-eqz v0, :cond_1

    .line 457
    invoke-direct {p0, p1, p2, p3, p4}, Lcom/helpshift/websockets/WebSocketFactory;->createProxiedRawSocket(Ljava/lang/String;IZI)Lcom/helpshift/websockets/SocketConnector;

    move-result-object p1

    return-object p1

    .line 461
    :cond_1
    invoke-direct {p0, p1, p2, p3, p4}, Lcom/helpshift/websockets/WebSocketFactory;->createDirectRawSocket(Ljava/lang/String;IZI)Lcom/helpshift/websockets/SocketConnector;

    move-result-object p1

    return-object p1
.end method

.method private createSocket(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;ILjava/lang/String;Ljava/lang/String;I)Lcom/helpshift/websockets/WebSocket;
    .locals 8
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    .line 426
    invoke-static {p1}, Lcom/helpshift/websockets/WebSocketFactory;->isSecureConnectionRequired(Ljava/lang/String;)Z

    move-result v1

    if-eqz p3, :cond_0

    .line 429
    invoke-virtual {p3}, Ljava/lang/String;->length()I

    move-result p1

    if-eqz p1, :cond_0

    .line 434
    invoke-static {p5}, Lcom/helpshift/websockets/WebSocketFactory;->determinePath(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v5

    .line 437
    invoke-direct {p0, p3, p4, v1, p7}, Lcom/helpshift/websockets/WebSocketFactory;->createRawSocket(Ljava/lang/String;IZI)Lcom/helpshift/websockets/SocketConnector;

    move-result-object v7

    move-object v0, p0

    move-object v2, p2

    move-object v3, p3

    move v4, p4

    move-object v6, p6

    .line 440
    invoke-direct/range {v0 .. v7}, Lcom/helpshift/websockets/WebSocketFactory;->createWebSocket(ZLjava/lang/String;Ljava/lang/String;ILjava/lang/String;Ljava/lang/String;Lcom/helpshift/websockets/SocketConnector;)Lcom/helpshift/websockets/WebSocket;

    move-result-object p1

    return-object p1

    .line 430
    :cond_0
    new-instance p1, Ljava/lang/IllegalArgumentException;

    const-string p2, "The host part is empty."

    invoke-direct {p1, p2}, Ljava/lang/IllegalArgumentException;-><init>(Ljava/lang/String;)V

    throw p1
.end method

.method private createWebSocket(ZLjava/lang/String;Ljava/lang/String;ILjava/lang/String;Ljava/lang/String;Lcom/helpshift/websockets/SocketConnector;)Lcom/helpshift/websockets/WebSocket;
    .locals 7

    if-ltz p4, :cond_0

    .line 512
    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    invoke-virtual {v0, p3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string p3, ":"

    invoke-virtual {v0, p3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0, p4}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p3

    :cond_0
    move-object v4, p3

    if-eqz p6, :cond_1

    .line 517
    new-instance p3, Ljava/lang/StringBuilder;

    invoke-direct {p3}, Ljava/lang/StringBuilder;-><init>()V

    invoke-virtual {p3, p5}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string p4, "?"

    invoke-virtual {p3, p4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p3, p6}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p3}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p5

    :cond_1
    move-object v5, p5

    .line 520
    new-instance p3, Lcom/helpshift/websockets/WebSocket;

    move-object v0, p3

    move-object v1, p0

    move v2, p1

    move-object v3, p2

    move-object v6, p7

    invoke-direct/range {v0 .. v6}, Lcom/helpshift/websockets/WebSocket;-><init>(Lcom/helpshift/websockets/WebSocketFactory;ZLjava/lang/String;Ljava/lang/String;Ljava/lang/String;Lcom/helpshift/websockets/SocketConnector;)V

    return-object p3
.end method

.method private static determinePath(Ljava/lang/String;)Ljava/lang/String;
    .locals 2

    if-eqz p0, :cond_2

    .line 66
    invoke-virtual {p0}, Ljava/lang/String;->length()I

    move-result v0

    if-nez v0, :cond_0

    goto :goto_0

    :cond_0
    const-string v0, "/"

    .line 70
    invoke-virtual {p0, v0}, Ljava/lang/String;->startsWith(Ljava/lang/String;)Z

    move-result v0

    if-eqz v0, :cond_1

    return-object p0

    .line 74
    :cond_1
    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    const-string v1, "/"

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0, p0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p0

    return-object p0

    :cond_2
    :goto_0
    const-string p0, "/"

    return-object p0
.end method

.method private static determinePort(IZ)I
    .locals 0

    if-ltz p0, :cond_0

    return p0

    :cond_0
    if-eqz p1, :cond_1

    const/16 p0, 0x1bb

    return p0

    :cond_1
    const/16 p0, 0x50

    return p0
.end method

.method private static isSecureConnectionRequired(Ljava/lang/String;)Z
    .locals 3

    if-eqz p0, :cond_4

    .line 50
    invoke-virtual {p0}, Ljava/lang/String;->length()I

    move-result v0

    if-eqz v0, :cond_4

    const-string v0, "wss"

    .line 54
    invoke-virtual {v0, p0}, Ljava/lang/String;->equalsIgnoreCase(Ljava/lang/String;)Z

    move-result v0

    if-nez v0, :cond_3

    const-string v0, "https"

    invoke-virtual {v0, p0}, Ljava/lang/String;->equalsIgnoreCase(Ljava/lang/String;)Z

    move-result v0

    if-eqz v0, :cond_0

    goto :goto_1

    :cond_0
    const-string v0, "ws"

    .line 58
    invoke-virtual {v0, p0}, Ljava/lang/String;->equalsIgnoreCase(Ljava/lang/String;)Z

    move-result v0

    if-nez v0, :cond_2

    const-string v0, "http"

    invoke-virtual {v0, p0}, Ljava/lang/String;->equalsIgnoreCase(Ljava/lang/String;)Z

    move-result v0

    if-eqz v0, :cond_1

    goto :goto_0

    .line 62
    :cond_1
    new-instance v0, Ljava/lang/IllegalArgumentException;

    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "Bad scheme: "

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1, p0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p0

    invoke-direct {v0, p0}, Ljava/lang/IllegalArgumentException;-><init>(Ljava/lang/String;)V

    throw v0

    :cond_2
    :goto_0
    const/4 p0, 0x0

    return p0

    :cond_3
    :goto_1
    const/4 p0, 0x1

    return p0

    .line 51
    :cond_4
    new-instance p0, Ljava/lang/IllegalArgumentException;

    const-string v0, "The scheme part is empty."

    invoke-direct {p0, v0}, Ljava/lang/IllegalArgumentException;-><init>(Ljava/lang/String;)V

    throw p0
.end method


# virtual methods
.method public createSocket(Ljava/lang/String;)Lcom/helpshift/websockets/WebSocket;
    .locals 1
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    .line 221
    invoke-virtual {p0}, Lcom/helpshift/websockets/WebSocketFactory;->getConnectionTimeout()I

    move-result v0

    invoke-virtual {p0, p1, v0}, Lcom/helpshift/websockets/WebSocketFactory;->createSocket(Ljava/lang/String;I)Lcom/helpshift/websockets/WebSocket;

    move-result-object p1

    return-object p1
.end method

.method public createSocket(Ljava/lang/String;I)Lcom/helpshift/websockets/WebSocket;
    .locals 0
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    if-eqz p1, :cond_1

    if-ltz p2, :cond_0

    .line 251
    invoke-static {p1}, Ljava/net/URI;->create(Ljava/lang/String;)Ljava/net/URI;

    move-result-object p1

    invoke-virtual {p0, p1, p2}, Lcom/helpshift/websockets/WebSocketFactory;->createSocket(Ljava/net/URI;I)Lcom/helpshift/websockets/WebSocket;

    move-result-object p1

    return-object p1

    .line 248
    :cond_0
    new-instance p1, Ljava/lang/IllegalArgumentException;

    const-string p2, "The given timeout value is negative."

    invoke-direct {p1, p2}, Ljava/lang/IllegalArgumentException;-><init>(Ljava/lang/String;)V

    throw p1

    .line 244
    :cond_1
    new-instance p1, Ljava/lang/IllegalArgumentException;

    const-string p2, "The given URI is null."

    invoke-direct {p1, p2}, Ljava/lang/IllegalArgumentException;-><init>(Ljava/lang/String;)V

    throw p1
.end method

.method public createSocket(Ljava/net/URI;)Lcom/helpshift/websockets/WebSocket;
    .locals 1
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    .line 352
    invoke-virtual {p0}, Lcom/helpshift/websockets/WebSocketFactory;->getConnectionTimeout()I

    move-result v0

    invoke-virtual {p0, p1, v0}, Lcom/helpshift/websockets/WebSocketFactory;->createSocket(Ljava/net/URI;I)Lcom/helpshift/websockets/WebSocket;

    move-result-object p1

    return-object p1
.end method

.method public createSocket(Ljava/net/URI;I)Lcom/helpshift/websockets/WebSocket;
    .locals 8
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    if-eqz p1, :cond_1

    if-ltz p2, :cond_0

    .line 412
    invoke-virtual {p1}, Ljava/net/URI;->getScheme()Ljava/lang/String;

    move-result-object v1

    .line 413
    invoke-virtual {p1}, Ljava/net/URI;->getUserInfo()Ljava/lang/String;

    move-result-object v2

    .line 414
    invoke-static {p1}, Lcom/helpshift/websockets/Misc;->extractHost(Ljava/net/URI;)Ljava/lang/String;

    move-result-object v3

    .line 415
    invoke-virtual {p1}, Ljava/net/URI;->getPort()I

    move-result v4

    .line 416
    invoke-virtual {p1}, Ljava/net/URI;->getRawPath()Ljava/lang/String;

    move-result-object v5

    .line 417
    invoke-virtual {p1}, Ljava/net/URI;->getRawQuery()Ljava/lang/String;

    move-result-object v6

    move-object v0, p0

    move v7, p2

    .line 419
    invoke-direct/range {v0 .. v7}, Lcom/helpshift/websockets/WebSocketFactory;->createSocket(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;ILjava/lang/String;Ljava/lang/String;I)Lcom/helpshift/websockets/WebSocket;

    move-result-object p1

    return-object p1

    .line 408
    :cond_0
    new-instance p1, Ljava/lang/IllegalArgumentException;

    const-string p2, "The given timeout value is negative."

    invoke-direct {p1, p2}, Ljava/lang/IllegalArgumentException;-><init>(Ljava/lang/String;)V

    throw p1

    .line 404
    :cond_1
    new-instance p1, Ljava/lang/IllegalArgumentException;

    const-string p2, "The given URI is null."

    invoke-direct {p1, p2}, Ljava/lang/IllegalArgumentException;-><init>(Ljava/lang/String;)V

    throw p1
.end method

.method public createSocket(Ljava/net/URL;)Lcom/helpshift/websockets/WebSocket;
    .locals 1
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    .line 269
    invoke-virtual {p0}, Lcom/helpshift/websockets/WebSocketFactory;->getConnectionTimeout()I

    move-result v0

    invoke-virtual {p0, p1, v0}, Lcom/helpshift/websockets/WebSocketFactory;->createSocket(Ljava/net/URL;I)Lcom/helpshift/websockets/WebSocket;

    move-result-object p1

    return-object p1
.end method

.method public createSocket(Ljava/net/URL;I)Lcom/helpshift/websockets/WebSocket;
    .locals 0
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    if-eqz p1, :cond_1

    if-ltz p2, :cond_0

    .line 299
    :try_start_0
    invoke-virtual {p1}, Ljava/net/URL;->toURI()Ljava/net/URI;

    move-result-object p1

    invoke-virtual {p0, p1, p2}, Lcom/helpshift/websockets/WebSocketFactory;->createSocket(Ljava/net/URI;I)Lcom/helpshift/websockets/WebSocket;

    move-result-object p1
    :try_end_0
    .catch Ljava/net/URISyntaxException; {:try_start_0 .. :try_end_0} :catch_0

    return-object p1

    .line 302
    :catch_0
    new-instance p1, Ljava/lang/IllegalArgumentException;

    const-string p2, "Failed to convert the given URL into a URI."

    invoke-direct {p1, p2}, Ljava/lang/IllegalArgumentException;-><init>(Ljava/lang/String;)V

    throw p1

    .line 295
    :cond_0
    new-instance p1, Ljava/lang/IllegalArgumentException;

    const-string p2, "The given timeout value is negative."

    invoke-direct {p1, p2}, Ljava/lang/IllegalArgumentException;-><init>(Ljava/lang/String;)V

    throw p1

    .line 291
    :cond_1
    new-instance p1, Ljava/lang/IllegalArgumentException;

    const-string p2, "The given URL is null."

    invoke-direct {p1, p2}, Ljava/lang/IllegalArgumentException;-><init>(Ljava/lang/String;)V

    throw p1
.end method

.method public getConnectionTimeout()I
    .locals 1

    .line 184
    iget v0, p0, Lcom/helpshift/websockets/WebSocketFactory;->mConnectionTimeout:I

    return v0
.end method

.method public getProxySettings()Lcom/helpshift/websockets/ProxySettings;
    .locals 1

    .line 167
    iget-object v0, p0, Lcom/helpshift/websockets/WebSocketFactory;->mProxySettings:Lcom/helpshift/websockets/ProxySettings;

    return-object v0
.end method

.method public getSSLContext()Ljavax/net/ssl/SSLContext;
    .locals 1

    .line 143
    iget-object v0, p0, Lcom/helpshift/websockets/WebSocketFactory;->mSocketFactorySettings:Lcom/helpshift/websockets/SocketFactorySettings;

    invoke-virtual {v0}, Lcom/helpshift/websockets/SocketFactorySettings;->getSSLContext()Ljavax/net/ssl/SSLContext;

    move-result-object v0

    return-object v0
.end method

.method public getSSLSocketFactory()Ljavax/net/ssl/SSLSocketFactory;
    .locals 1

    .line 121
    iget-object v0, p0, Lcom/helpshift/websockets/WebSocketFactory;->mSocketFactorySettings:Lcom/helpshift/websockets/SocketFactorySettings;

    invoke-virtual {v0}, Lcom/helpshift/websockets/SocketFactorySettings;->getSSLSocketFactory()Ljavax/net/ssl/SSLSocketFactory;

    move-result-object v0

    return-object v0
.end method

.method public getSocketFactory()Ljavax/net/SocketFactory;
    .locals 1

    .line 98
    iget-object v0, p0, Lcom/helpshift/websockets/WebSocketFactory;->mSocketFactorySettings:Lcom/helpshift/websockets/SocketFactorySettings;

    invoke-virtual {v0}, Lcom/helpshift/websockets/SocketFactorySettings;->getSocketFactory()Ljavax/net/SocketFactory;

    move-result-object v0

    return-object v0
.end method

.method public setConnectionTimeout(I)Lcom/helpshift/websockets/WebSocketFactory;
    .locals 1

    if-ltz p1, :cond_0

    .line 201
    iput p1, p0, Lcom/helpshift/websockets/WebSocketFactory;->mConnectionTimeout:I

    return-object p0

    .line 198
    :cond_0
    new-instance p1, Ljava/lang/IllegalArgumentException;

    const-string v0, "timeout value cannot be negative."

    invoke-direct {p1, v0}, Ljava/lang/IllegalArgumentException;-><init>(Ljava/lang/String;)V

    throw p1
.end method

.method public setSSLContext(Ljavax/net/ssl/SSLContext;)Lcom/helpshift/websockets/WebSocketFactory;
    .locals 1

    .line 154
    iget-object v0, p0, Lcom/helpshift/websockets/WebSocketFactory;->mSocketFactorySettings:Lcom/helpshift/websockets/SocketFactorySettings;

    invoke-virtual {v0, p1}, Lcom/helpshift/websockets/SocketFactorySettings;->setSSLContext(Ljavax/net/ssl/SSLContext;)V

    return-object p0
.end method

.method public setSSLSocketFactory(Ljavax/net/ssl/SSLSocketFactory;)Lcom/helpshift/websockets/WebSocketFactory;
    .locals 1

    .line 132
    iget-object v0, p0, Lcom/helpshift/websockets/WebSocketFactory;->mSocketFactorySettings:Lcom/helpshift/websockets/SocketFactorySettings;

    invoke-virtual {v0, p1}, Lcom/helpshift/websockets/SocketFactorySettings;->setSSLSocketFactory(Ljavax/net/ssl/SSLSocketFactory;)V

    return-object p0
.end method

.method public setSocketFactory(Ljavax/net/SocketFactory;)Lcom/helpshift/websockets/WebSocketFactory;
    .locals 1

    .line 109
    iget-object v0, p0, Lcom/helpshift/websockets/WebSocketFactory;->mSocketFactorySettings:Lcom/helpshift/websockets/SocketFactorySettings;

    invoke-virtual {v0, p1}, Lcom/helpshift/websockets/SocketFactorySettings;->setSocketFactory(Ljavax/net/SocketFactory;)V

    return-object p0
.end method
