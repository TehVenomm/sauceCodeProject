.class Lcom/helpshift/websockets/SocketConnector;
.super Ljava/lang/Object;
.source "SocketConnector.java"


# instance fields
.field private final mAddress:Lcom/helpshift/websockets/Address;

.field private final mConnectionTimeout:I

.field private final mHost:Ljava/lang/String;

.field private final mPort:I

.field private final mProxyHandshaker:Lcom/helpshift/websockets/ProxyHandshaker;

.field private final mSSLSocketFactory:Ljavax/net/ssl/SSLSocketFactory;

.field private mSocket:Ljava/net/Socket;


# direct methods
.method constructor <init>(Ljava/net/Socket;Lcom/helpshift/websockets/Address;I)V
    .locals 8

    const/4 v4, 0x0

    const/4 v5, 0x0

    const/4 v6, 0x0

    const/4 v7, 0x0

    move-object v0, p0

    move-object v1, p1

    move-object v2, p2

    move v3, p3

    .line 49
    invoke-direct/range {v0 .. v7}, Lcom/helpshift/websockets/SocketConnector;-><init>(Ljava/net/Socket;Lcom/helpshift/websockets/Address;ILcom/helpshift/websockets/ProxyHandshaker;Ljavax/net/ssl/SSLSocketFactory;Ljava/lang/String;I)V

    return-void
.end method

.method constructor <init>(Ljava/net/Socket;Lcom/helpshift/websockets/Address;ILcom/helpshift/websockets/ProxyHandshaker;Ljavax/net/ssl/SSLSocketFactory;Ljava/lang/String;I)V
    .locals 0

    .line 56
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 57
    iput-object p1, p0, Lcom/helpshift/websockets/SocketConnector;->mSocket:Ljava/net/Socket;

    .line 58
    iput-object p2, p0, Lcom/helpshift/websockets/SocketConnector;->mAddress:Lcom/helpshift/websockets/Address;

    .line 59
    iput p3, p0, Lcom/helpshift/websockets/SocketConnector;->mConnectionTimeout:I

    .line 60
    iput-object p4, p0, Lcom/helpshift/websockets/SocketConnector;->mProxyHandshaker:Lcom/helpshift/websockets/ProxyHandshaker;

    .line 61
    iput-object p5, p0, Lcom/helpshift/websockets/SocketConnector;->mSSLSocketFactory:Ljavax/net/ssl/SSLSocketFactory;

    .line 62
    iput-object p6, p0, Lcom/helpshift/websockets/SocketConnector;->mHost:Ljava/lang/String;

    .line 63
    iput p7, p0, Lcom/helpshift/websockets/SocketConnector;->mPort:I

    return-void
.end method

.method private doConnect()V
    .locals 6
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Lcom/helpshift/websockets/WebSocketException;
        }
    .end annotation

    .line 100
    iget-object v0, p0, Lcom/helpshift/websockets/SocketConnector;->mProxyHandshaker:Lcom/helpshift/websockets/ProxyHandshaker;

    const/4 v1, 0x0

    const/4 v2, 0x1

    if-eqz v0, :cond_0

    const/4 v0, 0x1

    goto :goto_0

    :cond_0
    const/4 v0, 0x0

    .line 104
    :goto_0
    :try_start_0
    iget-object v3, p0, Lcom/helpshift/websockets/SocketConnector;->mSocket:Ljava/net/Socket;

    iget-object v4, p0, Lcom/helpshift/websockets/SocketConnector;->mAddress:Lcom/helpshift/websockets/Address;

    invoke-virtual {v4}, Lcom/helpshift/websockets/Address;->toInetSocketAddress()Ljava/net/InetSocketAddress;

    move-result-object v4

    iget v5, p0, Lcom/helpshift/websockets/SocketConnector;->mConnectionTimeout:I

    invoke-virtual {v3, v4, v5}, Ljava/net/Socket;->connect(Ljava/net/SocketAddress;I)V

    .line 106
    iget-object v3, p0, Lcom/helpshift/websockets/SocketConnector;->mSocket:Ljava/net/Socket;

    instance-of v3, v3, Ljavax/net/ssl/SSLSocket;

    if-eqz v3, :cond_1

    .line 109
    iget-object v3, p0, Lcom/helpshift/websockets/SocketConnector;->mSocket:Ljava/net/Socket;

    check-cast v3, Ljavax/net/ssl/SSLSocket;

    iget-object v4, p0, Lcom/helpshift/websockets/SocketConnector;->mAddress:Lcom/helpshift/websockets/Address;

    invoke-virtual {v4}, Lcom/helpshift/websockets/Address;->getHostname()Ljava/lang/String;

    move-result-object v4

    invoke-direct {p0, v3, v4}, Lcom/helpshift/websockets/SocketConnector;->verifyHostname(Ljavax/net/ssl/SSLSocket;Ljava/lang/String;)V
    :try_end_0
    .catch Ljava/io/IOException; {:try_start_0 .. :try_end_0} :catch_0

    :cond_1
    if-eqz v0, :cond_2

    .line 125
    invoke-direct {p0}, Lcom/helpshift/websockets/SocketConnector;->handshake()V

    :cond_2
    return-void

    :catch_0
    move-exception v3

    const/4 v4, 0x3

    .line 114
    new-array v4, v4, [Ljava/lang/Object;

    if-eqz v0, :cond_3

    const-string v0, "the proxy "

    goto :goto_1

    :cond_3
    const-string v0, ""

    :goto_1
    aput-object v0, v4, v1

    iget-object v0, p0, Lcom/helpshift/websockets/SocketConnector;->mAddress:Lcom/helpshift/websockets/Address;

    aput-object v0, v4, v2

    const/4 v0, 0x2

    .line 115
    invoke-virtual {v3}, Ljava/io/IOException;->getMessage()Ljava/lang/String;

    move-result-object v1

    aput-object v1, v4, v0

    const-string v0, "Failed to connect to %s\'%s\': %s"

    .line 114
    invoke-static {v0, v4}, Ljava/lang/String;->format(Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v0

    .line 118
    new-instance v1, Lcom/helpshift/websockets/WebSocketException;

    sget-object v2, Lcom/helpshift/websockets/WebSocketError;->SOCKET_CONNECT_ERROR:Lcom/helpshift/websockets/WebSocketError;

    invoke-direct {v1, v2, v0, v3}, Lcom/helpshift/websockets/WebSocketException;-><init>(Lcom/helpshift/websockets/WebSocketError;Ljava/lang/String;Ljava/lang/Throwable;)V

    throw v1
.end method

.method private handshake()V
    .locals 7
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Lcom/helpshift/websockets/WebSocketException;
        }
    .end annotation

    const/4 v0, 0x0

    const/4 v1, 0x2

    const/4 v2, 0x1

    .line 154
    :try_start_0
    iget-object v3, p0, Lcom/helpshift/websockets/SocketConnector;->mProxyHandshaker:Lcom/helpshift/websockets/ProxyHandshaker;

    invoke-virtual {v3}, Lcom/helpshift/websockets/ProxyHandshaker;->perform()V
    :try_end_0
    .catch Ljava/io/IOException; {:try_start_0 .. :try_end_0} :catch_2

    .line 165
    iget-object v3, p0, Lcom/helpshift/websockets/SocketConnector;->mSSLSocketFactory:Ljavax/net/ssl/SSLSocketFactory;

    if-nez v3, :cond_0

    return-void

    .line 172
    :cond_0
    :try_start_1
    iget-object v3, p0, Lcom/helpshift/websockets/SocketConnector;->mSSLSocketFactory:Ljavax/net/ssl/SSLSocketFactory;

    iget-object v4, p0, Lcom/helpshift/websockets/SocketConnector;->mSocket:Ljava/net/Socket;

    iget-object v5, p0, Lcom/helpshift/websockets/SocketConnector;->mHost:Ljava/lang/String;

    iget v6, p0, Lcom/helpshift/websockets/SocketConnector;->mPort:I

    invoke-virtual {v3, v4, v5, v6, v2}, Ljavax/net/ssl/SSLSocketFactory;->createSocket(Ljava/net/Socket;Ljava/lang/String;IZ)Ljava/net/Socket;

    move-result-object v3

    iput-object v3, p0, Lcom/helpshift/websockets/SocketConnector;->mSocket:Ljava/net/Socket;
    :try_end_1
    .catch Ljava/io/IOException; {:try_start_1 .. :try_end_1} :catch_1

    .line 185
    :try_start_2
    iget-object v3, p0, Lcom/helpshift/websockets/SocketConnector;->mSocket:Ljava/net/Socket;

    check-cast v3, Ljavax/net/ssl/SSLSocket;

    invoke-virtual {v3}, Ljavax/net/ssl/SSLSocket;->startHandshake()V

    .line 187
    iget-object v3, p0, Lcom/helpshift/websockets/SocketConnector;->mSocket:Ljava/net/Socket;

    instance-of v3, v3, Ljavax/net/ssl/SSLSocket;

    if-eqz v3, :cond_1

    .line 190
    iget-object v3, p0, Lcom/helpshift/websockets/SocketConnector;->mSocket:Ljava/net/Socket;

    check-cast v3, Ljavax/net/ssl/SSLSocket;

    iget-object v4, p0, Lcom/helpshift/websockets/SocketConnector;->mProxyHandshaker:Lcom/helpshift/websockets/ProxyHandshaker;

    invoke-virtual {v4}, Lcom/helpshift/websockets/ProxyHandshaker;->getProxiedHostname()Ljava/lang/String;

    move-result-object v4

    invoke-direct {p0, v3, v4}, Lcom/helpshift/websockets/SocketConnector;->verifyHostname(Ljavax/net/ssl/SSLSocket;Ljava/lang/String;)V
    :try_end_2
    .catch Ljava/io/IOException; {:try_start_2 .. :try_end_2} :catch_0

    :cond_1
    return-void

    :catch_0
    move-exception v3

    .line 195
    new-array v1, v1, [Ljava/lang/Object;

    iget-object v4, p0, Lcom/helpshift/websockets/SocketConnector;->mAddress:Lcom/helpshift/websockets/Address;

    aput-object v4, v1, v0

    .line 196
    invoke-virtual {v3}, Ljava/io/IOException;->getMessage()Ljava/lang/String;

    move-result-object v0

    aput-object v0, v1, v2

    const-string v0, "SSL handshake with the WebSocket endpoint (%s) failed: %s"

    .line 195
    invoke-static {v0, v1}, Ljava/lang/String;->format(Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v0

    .line 199
    new-instance v1, Lcom/helpshift/websockets/WebSocketException;

    sget-object v2, Lcom/helpshift/websockets/WebSocketError;->SSL_HANDSHAKE_ERROR:Lcom/helpshift/websockets/WebSocketError;

    invoke-direct {v1, v2, v0, v3}, Lcom/helpshift/websockets/WebSocketException;-><init>(Lcom/helpshift/websockets/WebSocketError;Ljava/lang/String;Ljava/lang/Throwable;)V

    throw v1

    :catch_1
    move-exception v0

    .line 176
    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "Failed to overlay an existing socket: "

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/io/IOException;->getMessage()Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    .line 179
    new-instance v2, Lcom/helpshift/websockets/WebSocketException;

    sget-object v3, Lcom/helpshift/websockets/WebSocketError;->SOCKET_OVERLAY_ERROR:Lcom/helpshift/websockets/WebSocketError;

    invoke-direct {v2, v3, v1, v0}, Lcom/helpshift/websockets/WebSocketException;-><init>(Lcom/helpshift/websockets/WebSocketError;Ljava/lang/String;Ljava/lang/Throwable;)V

    throw v2

    :catch_2
    move-exception v3

    .line 158
    new-array v1, v1, [Ljava/lang/Object;

    iget-object v4, p0, Lcom/helpshift/websockets/SocketConnector;->mAddress:Lcom/helpshift/websockets/Address;

    aput-object v4, v1, v0

    .line 159
    invoke-virtual {v3}, Ljava/io/IOException;->getMessage()Ljava/lang/String;

    move-result-object v0

    aput-object v0, v1, v2

    const-string v0, "Handshake with the proxy server (%s) failed: %s"

    .line 158
    invoke-static {v0, v1}, Ljava/lang/String;->format(Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v0

    .line 162
    new-instance v1, Lcom/helpshift/websockets/WebSocketException;

    sget-object v2, Lcom/helpshift/websockets/WebSocketError;->PROXY_HANDSHAKE_ERROR:Lcom/helpshift/websockets/WebSocketError;

    invoke-direct {v1, v2, v0, v3}, Lcom/helpshift/websockets/WebSocketException;-><init>(Lcom/helpshift/websockets/WebSocketError;Ljava/lang/String;Ljava/lang/Throwable;)V

    throw v1
.end method

.method private verifyHostname(Ljavax/net/ssl/SSLSocket;Ljava/lang/String;)V
    .locals 2
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Lcom/helpshift/websockets/HostnameUnverifiedException;
        }
    .end annotation

    .line 132
    sget-object v0, Lcom/helpshift/websockets/OkHostnameVerifier;->INSTANCE:Lcom/helpshift/websockets/OkHostnameVerifier;

    .line 135
    invoke-virtual {p1}, Ljavax/net/ssl/SSLSocket;->getSession()Ljavax/net/ssl/SSLSession;

    move-result-object v1

    .line 138
    invoke-virtual {v0, p2, v1}, Lcom/helpshift/websockets/OkHostnameVerifier;->verify(Ljava/lang/String;Ljavax/net/ssl/SSLSession;)Z

    move-result v0

    if-eqz v0, :cond_0

    return-void

    .line 144
    :cond_0
    new-instance v0, Lcom/helpshift/websockets/HostnameUnverifiedException;

    invoke-direct {v0, p1, p2}, Lcom/helpshift/websockets/HostnameUnverifiedException;-><init>(Ljavax/net/ssl/SSLSocket;Ljava/lang/String;)V

    throw v0
.end method


# virtual methods
.method closeSilently()V
    .locals 1

    .line 206
    :try_start_0
    iget-object v0, p0, Lcom/helpshift/websockets/SocketConnector;->mSocket:Ljava/net/Socket;

    invoke-virtual {v0}, Ljava/net/Socket;->close()V
    :try_end_0
    .catch Ljava/lang/Throwable; {:try_start_0 .. :try_end_0} :catch_0

    :catch_0
    return-void
.end method

.method public connect()V
    .locals 2
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Lcom/helpshift/websockets/WebSocketException;
        }
    .end annotation

    .line 80
    :try_start_0
    invoke-direct {p0}, Lcom/helpshift/websockets/SocketConnector;->doConnect()V
    :try_end_0
    .catch Lcom/helpshift/websockets/WebSocketException; {:try_start_0 .. :try_end_0} :catch_0

    return-void

    :catch_0
    move-exception v0

    .line 87
    :try_start_1
    iget-object v1, p0, Lcom/helpshift/websockets/SocketConnector;->mSocket:Ljava/net/Socket;

    invoke-virtual {v1}, Ljava/net/Socket;->close()V
    :try_end_1
    .catch Ljava/io/IOException; {:try_start_1 .. :try_end_1} :catch_1

    .line 93
    :catch_1
    throw v0
.end method

.method public getConnectionTimeout()I
    .locals 1

    .line 73
    iget v0, p0, Lcom/helpshift/websockets/SocketConnector;->mConnectionTimeout:I

    return v0
.end method

.method public getSocket()Ljava/net/Socket;
    .locals 1

    .line 68
    iget-object v0, p0, Lcom/helpshift/websockets/SocketConnector;->mSocket:Ljava/net/Socket;

    return-object v0
.end method
