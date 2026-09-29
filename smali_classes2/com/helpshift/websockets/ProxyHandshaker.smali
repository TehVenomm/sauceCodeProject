.class Lcom/helpshift/websockets/ProxyHandshaker;
.super Ljava/lang/Object;
.source "ProxyHandshaker.java"


# static fields
.field private static final RN:Ljava/lang/String; = "\r\n"


# instance fields
.field private final mHost:Ljava/lang/String;

.field private final mPort:I

.field private final mSettings:Lcom/helpshift/websockets/ProxySettings;

.field private final mSocket:Ljava/net/Socket;


# direct methods
.method public constructor <init>(Ljava/net/Socket;Ljava/lang/String;ILcom/helpshift/websockets/ProxySettings;)V
    .locals 0

    .line 42
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 43
    iput-object p1, p0, Lcom/helpshift/websockets/ProxyHandshaker;->mSocket:Ljava/net/Socket;

    .line 44
    iput-object p2, p0, Lcom/helpshift/websockets/ProxyHandshaker;->mHost:Ljava/lang/String;

    .line 45
    iput p3, p0, Lcom/helpshift/websockets/ProxyHandshaker;->mPort:I

    .line 46
    iput-object p4, p0, Lcom/helpshift/websockets/ProxyHandshaker;->mSettings:Lcom/helpshift/websockets/ProxySettings;

    return-void
.end method

.method private addHeaders(Ljava/lang/StringBuilder;)V
    .locals 5

    .line 97
    iget-object v0, p0, Lcom/helpshift/websockets/ProxyHandshaker;->mSettings:Lcom/helpshift/websockets/ProxySettings;

    invoke-virtual {v0}, Lcom/helpshift/websockets/ProxySettings;->getHeaders()Ljava/util/Map;

    move-result-object v0

    invoke-interface {v0}, Ljava/util/Map;->entrySet()Ljava/util/Set;

    move-result-object v0

    invoke-interface {v0}, Ljava/util/Set;->iterator()Ljava/util/Iterator;

    move-result-object v0

    :cond_0
    invoke-interface {v0}, Ljava/util/Iterator;->hasNext()Z

    move-result v1

    if-eqz v1, :cond_2

    invoke-interface {v0}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Ljava/util/Map$Entry;

    .line 99
    invoke-interface {v1}, Ljava/util/Map$Entry;->getKey()Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Ljava/lang/String;

    .line 102
    invoke-interface {v1}, Ljava/util/Map$Entry;->getValue()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Ljava/util/List;

    invoke-interface {v1}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v1

    :goto_0
    invoke-interface {v1}, Ljava/util/Iterator;->hasNext()Z

    move-result v3

    if-eqz v3, :cond_0

    invoke-interface {v1}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v3

    check-cast v3, Ljava/lang/String;

    if-nez v3, :cond_1

    const-string v3, ""

    .line 107
    :cond_1
    invoke-virtual {p1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v4, ": "

    invoke-virtual {p1, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p1, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v3, "\r\n"

    invoke-virtual {p1, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    goto :goto_0

    :cond_2
    return-void
.end method

.method private addProxyAuthorization(Ljava/lang/StringBuilder;)V
    .locals 5

    .line 114
    iget-object v0, p0, Lcom/helpshift/websockets/ProxyHandshaker;->mSettings:Lcom/helpshift/websockets/ProxySettings;

    invoke-virtual {v0}, Lcom/helpshift/websockets/ProxySettings;->getId()Ljava/lang/String;

    move-result-object v0

    if-eqz v0, :cond_2

    .line 116
    invoke-virtual {v0}, Ljava/lang/String;->length()I

    move-result v1

    if-nez v1, :cond_0

    goto :goto_0

    .line 120
    :cond_0
    iget-object v1, p0, Lcom/helpshift/websockets/ProxyHandshaker;->mSettings:Lcom/helpshift/websockets/ProxySettings;

    invoke-virtual {v1}, Lcom/helpshift/websockets/ProxySettings;->getPassword()Ljava/lang/String;

    move-result-object v1

    if-nez v1, :cond_1

    const-string v1, ""

    :cond_1
    const-string v2, "%s:%s"

    const/4 v3, 0x2

    .line 127
    new-array v3, v3, [Ljava/lang/Object;

    const/4 v4, 0x0

    aput-object v0, v3, v4

    const/4 v0, 0x1

    aput-object v1, v3, v0

    invoke-static {v2, v3}, Ljava/lang/String;->format(Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v0

    const-string v1, "Proxy-Authorization: Basic "

    .line 131
    invoke-virtual {p1, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    .line 132
    invoke-static {v0}, Lcom/helpshift/websockets/Base64;->encode(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v0

    invoke-virtual {p1, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v0, "\r\n"

    .line 133
    invoke-virtual {p1, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    return-void

    :cond_2
    :goto_0
    return-void
.end method

.method private buildRequest()Ljava/lang/String;
    .locals 4

    const-string v0, "%s:%d"

    const/4 v1, 0x2

    .line 76
    new-array v1, v1, [Ljava/lang/Object;

    iget-object v2, p0, Lcom/helpshift/websockets/ProxyHandshaker;->mHost:Ljava/lang/String;

    const/4 v3, 0x0

    aput-object v2, v1, v3

    iget v2, p0, Lcom/helpshift/websockets/ProxyHandshaker;->mPort:I

    invoke-static {v2}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v2

    const/4 v3, 0x1

    aput-object v2, v1, v3

    invoke-static {v0, v1}, Ljava/lang/String;->format(Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v0

    .line 79
    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "CONNECT "

    .line 80
    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v2, " HTTP/1.1"

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v2, "\r\n"

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v2, "Host: "

    .line 81
    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v0, "\r\n"

    invoke-virtual {v1, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    .line 85
    invoke-direct {p0, v1}, Lcom/helpshift/websockets/ProxyHandshaker;->addHeaders(Ljava/lang/StringBuilder;)V

    .line 88
    invoke-direct {p0, v1}, Lcom/helpshift/websockets/ProxyHandshaker;->addProxyAuthorization(Ljava/lang/StringBuilder;)V

    const-string v0, "\r\n"

    .line 91
    invoke-virtual {v1, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v0

    return-object v0
.end method

.method private readStatusLine(Ljava/io/InputStream;)V
    .locals 3
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    const-string v0, "UTF-8"

    .line 152
    invoke-static {p1, v0}, Lcom/helpshift/websockets/Misc;->readLine(Ljava/io/InputStream;Ljava/lang/String;)Ljava/lang/String;

    move-result-object p1

    if-eqz p1, :cond_2

    .line 155
    invoke-virtual {p1}, Ljava/lang/String;->length()I

    move-result v0

    if-eqz v0, :cond_2

    const-string v0, " +"

    const/4 v1, 0x3

    .line 160
    invoke-virtual {p1, v0, v1}, Ljava/lang/String;->split(Ljava/lang/String;I)[Ljava/lang/String;

    move-result-object v0

    .line 162
    array-length v1, v0

    const/4 v2, 0x2

    if-lt v1, v2, :cond_1

    const-string v1, "200"

    const/4 v2, 0x1

    .line 169
    aget-object v0, v0, v2

    invoke-virtual {v1, v0}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v0

    if-eqz v0, :cond_0

    return-void

    .line 170
    :cond_0
    new-instance v0, Ljava/io/IOException;

    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "The status code in the response from the proxy server is not \'200 Connection established\'. The status line is: "

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p1

    invoke-direct {v0, p1}, Ljava/io/IOException;-><init>(Ljava/lang/String;)V

    throw v0

    .line 163
    :cond_1
    new-instance v0, Ljava/io/IOException;

    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "The status line in the response from the proxy server is badly formatted. The status line is: "

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p1

    invoke-direct {v0, p1}, Ljava/io/IOException;-><init>(Ljava/lang/String;)V

    throw v0

    .line 156
    :cond_2
    new-instance p1, Ljava/io/IOException;

    const-string v0, "The response from the proxy server does not contain a status line."

    invoke-direct {p1, v0}, Ljava/io/IOException;-><init>(Ljava/lang/String;)V

    throw p1
.end method

.method private receiveResponse()V
    .locals 1
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    .line 139
    iget-object v0, p0, Lcom/helpshift/websockets/ProxyHandshaker;->mSocket:Ljava/net/Socket;

    invoke-virtual {v0}, Ljava/net/Socket;->getInputStream()Ljava/io/InputStream;

    move-result-object v0

    .line 142
    invoke-direct {p0, v0}, Lcom/helpshift/websockets/ProxyHandshaker;->readStatusLine(Ljava/io/InputStream;)V

    .line 146
    invoke-direct {p0, v0}, Lcom/helpshift/websockets/ProxyHandshaker;->skipHeaders(Ljava/io/InputStream;)V

    return-void
.end method

.method private sendRequest()V
    .locals 2
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    .line 61
    invoke-direct {p0}, Lcom/helpshift/websockets/ProxyHandshaker;->buildRequest()Ljava/lang/String;

    move-result-object v0

    .line 64
    invoke-static {v0}, Lcom/helpshift/websockets/Misc;->getBytesUTF8(Ljava/lang/String;)[B

    move-result-object v0

    .line 67
    iget-object v1, p0, Lcom/helpshift/websockets/ProxyHandshaker;->mSocket:Ljava/net/Socket;

    invoke-virtual {v1}, Ljava/net/Socket;->getOutputStream()Ljava/io/OutputStream;

    move-result-object v1

    .line 70
    invoke-virtual {v1, v0}, Ljava/io/OutputStream;->write([B)V

    .line 71
    invoke-virtual {v1}, Ljava/io/OutputStream;->flush()V

    return-void
.end method

.method private skipHeaders(Ljava/io/InputStream;)V
    .locals 6
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    const/4 v0, 0x0

    :cond_0
    const/4 v1, 0x0

    .line 185
    :goto_0
    invoke-virtual {p1}, Ljava/io/InputStream;->read()I

    move-result v2

    const/4 v3, -0x1

    if-eq v2, v3, :cond_5

    const/16 v4, 0xa

    if-ne v2, v4, :cond_1

    if-nez v1, :cond_0

    return-void

    :cond_1
    const/16 v5, 0xd

    if-eq v2, v5, :cond_2

    add-int/lit8 v1, v1, 0x1

    goto :goto_0

    .line 214
    :cond_2
    invoke-virtual {p1}, Ljava/io/InputStream;->read()I

    move-result v2

    if-eq v2, v3, :cond_4

    if-eq v2, v4, :cond_3

    add-int/lit8 v1, v1, 0x2

    goto :goto_0

    :cond_3
    if-nez v1, :cond_0

    return-void

    .line 219
    :cond_4
    new-instance p1, Ljava/io/EOFException;

    const-string v0, "The end of the stream from the proxy server was reached unexpectedly after a carriage return."

    invoke-direct {p1, v0}, Ljava/io/EOFException;-><init>(Ljava/lang/String;)V

    throw p1

    .line 190
    :cond_5
    new-instance p1, Ljava/io/EOFException;

    const-string v0, "The end of the stream from the proxy server was reached unexpectedly."

    invoke-direct {p1, v0}, Ljava/io/EOFException;-><init>(Ljava/lang/String;)V

    throw p1
.end method


# virtual methods
.method getProxiedHostname()Ljava/lang/String;
    .locals 1

    .line 251
    iget-object v0, p0, Lcom/helpshift/websockets/ProxyHandshaker;->mHost:Ljava/lang/String;

    return-object v0
.end method

.method public perform()V
    .locals 0
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    .line 52
    invoke-direct {p0}, Lcom/helpshift/websockets/ProxyHandshaker;->sendRequest()V

    .line 55
    invoke-direct {p0}, Lcom/helpshift/websockets/ProxyHandshaker;->receiveResponse()V

    return-void
.end method
