.class public Lcom/helpshift/common/platform/network/websockets/HSWebSocket$Builder;
.super Ljava/lang/Object;
.source "HSWebSocket.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lcom/helpshift/common/platform/network/websockets/HSWebSocket;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x9
    name = "Builder"
.end annotation


# instance fields
.field private connectionTimeout:I

.field private extensions:Ljava/util/List;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/List<",
            "Ljava/lang/String;",
            ">;"
        }
    .end annotation
.end field

.field private headers:Ljava/util/Map;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            ">;"
        }
    .end annotation
.end field

.field private listener:Lcom/helpshift/common/platform/network/websockets/IHSWebSocketListener;

.field private protocols:Ljava/util/List;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/List<",
            "Ljava/lang/String;",
            ">;"
        }
    .end annotation
.end field

.field private socketTimeout:I

.field private url:Ljava/lang/String;


# direct methods
.method public constructor <init>(Ljava/lang/String;)V
    .locals 1

    .line 78
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 73
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    iput-object v0, p0, Lcom/helpshift/common/platform/network/websockets/HSWebSocket$Builder;->extensions:Ljava/util/List;

    .line 74
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    iput-object v0, p0, Lcom/helpshift/common/platform/network/websockets/HSWebSocket$Builder;->protocols:Ljava/util/List;

    .line 75
    new-instance v0, Ljava/util/HashMap;

    invoke-direct {v0}, Ljava/util/HashMap;-><init>()V

    iput-object v0, p0, Lcom/helpshift/common/platform/network/websockets/HSWebSocket$Builder;->headers:Ljava/util/Map;

    .line 79
    iput-object p1, p0, Lcom/helpshift/common/platform/network/websockets/HSWebSocket$Builder;->url:Ljava/lang/String;

    return-void
.end method


# virtual methods
.method public addExtension(Ljava/lang/String;)Lcom/helpshift/common/platform/network/websockets/HSWebSocket$Builder;
    .locals 1

    .line 93
    iget-object v0, p0, Lcom/helpshift/common/platform/network/websockets/HSWebSocket$Builder;->extensions:Ljava/util/List;

    invoke-interface {v0, p1}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    return-object p0
.end method

.method public addHeader(Ljava/lang/String;Ljava/lang/String;)Lcom/helpshift/common/platform/network/websockets/HSWebSocket$Builder;
    .locals 1

    if-eqz p2, :cond_0

    .line 103
    invoke-static {p1}, Lcom/helpshift/common/StringUtils;->isEmpty(Ljava/lang/String;)Z

    move-result v0

    if-nez v0, :cond_0

    .line 104
    iget-object v0, p0, Lcom/helpshift/common/platform/network/websockets/HSWebSocket$Builder;->headers:Ljava/util/Map;

    invoke-interface {v0, p1, p2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    :cond_0
    return-object p0
.end method

.method public addProtocol(Ljava/lang/String;)Lcom/helpshift/common/platform/network/websockets/HSWebSocket$Builder;
    .locals 1

    .line 98
    iget-object v0, p0, Lcom/helpshift/common/platform/network/websockets/HSWebSocket$Builder;->protocols:Ljava/util/List;

    invoke-interface {v0, p1}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    return-object p0
.end method

.method public build()Lcom/helpshift/common/platform/network/websockets/HSWebSocket;
    .locals 4
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    .line 116
    new-instance v0, Lcom/helpshift/websockets/WebSocketFactory;

    invoke-direct {v0}, Lcom/helpshift/websockets/WebSocketFactory;-><init>()V

    .line 117
    iget v1, p0, Lcom/helpshift/common/platform/network/websockets/HSWebSocket$Builder;->connectionTimeout:I

    invoke-virtual {v0, v1}, Lcom/helpshift/websockets/WebSocketFactory;->setConnectionTimeout(I)Lcom/helpshift/websockets/WebSocketFactory;

    move-result-object v0

    iget-object v1, p0, Lcom/helpshift/common/platform/network/websockets/HSWebSocket$Builder;->url:Ljava/lang/String;

    .line 118
    invoke-virtual {v0, v1}, Lcom/helpshift/websockets/WebSocketFactory;->createSocket(Ljava/lang/String;)Lcom/helpshift/websockets/WebSocket;

    move-result-object v0

    .line 120
    invoke-virtual {v0}, Lcom/helpshift/websockets/WebSocket;->getSocket()Ljava/net/Socket;

    move-result-object v1

    iget v2, p0, Lcom/helpshift/common/platform/network/websockets/HSWebSocket$Builder;->socketTimeout:I

    invoke-virtual {v1, v2}, Ljava/net/Socket;->setSoTimeout(I)V

    .line 122
    iget-object v1, p0, Lcom/helpshift/common/platform/network/websockets/HSWebSocket$Builder;->extensions:Ljava/util/List;

    invoke-interface {v1}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v1

    :goto_0
    invoke-interface {v1}, Ljava/util/Iterator;->hasNext()Z

    move-result v2

    if-eqz v2, :cond_0

    invoke-interface {v1}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Ljava/lang/String;

    .line 123
    invoke-virtual {v0, v2}, Lcom/helpshift/websockets/WebSocket;->addExtension(Ljava/lang/String;)Lcom/helpshift/websockets/WebSocket;

    goto :goto_0

    .line 126
    :cond_0
    iget-object v1, p0, Lcom/helpshift/common/platform/network/websockets/HSWebSocket$Builder;->protocols:Ljava/util/List;

    invoke-interface {v1}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v1

    :goto_1
    invoke-interface {v1}, Ljava/util/Iterator;->hasNext()Z

    move-result v2

    if-eqz v2, :cond_1

    invoke-interface {v1}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Ljava/lang/String;

    .line 127
    invoke-virtual {v0, v2}, Lcom/helpshift/websockets/WebSocket;->addProtocol(Ljava/lang/String;)Lcom/helpshift/websockets/WebSocket;

    goto :goto_1

    .line 130
    :cond_1
    iget-object v1, p0, Lcom/helpshift/common/platform/network/websockets/HSWebSocket$Builder;->headers:Ljava/util/Map;

    invoke-interface {v1}, Ljava/util/Map;->keySet()Ljava/util/Set;

    move-result-object v1

    invoke-interface {v1}, Ljava/util/Set;->iterator()Ljava/util/Iterator;

    move-result-object v1

    :goto_2
    invoke-interface {v1}, Ljava/util/Iterator;->hasNext()Z

    move-result v2

    if-eqz v2, :cond_2

    invoke-interface {v1}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Ljava/lang/String;

    .line 131
    iget-object v3, p0, Lcom/helpshift/common/platform/network/websockets/HSWebSocket$Builder;->headers:Ljava/util/Map;

    invoke-interface {v3, v2}, Ljava/util/Map;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v3

    check-cast v3, Ljava/lang/String;

    invoke-virtual {v0, v2, v3}, Lcom/helpshift/websockets/WebSocket;->addHeader(Ljava/lang/String;Ljava/lang/String;)Lcom/helpshift/websockets/WebSocket;

    goto :goto_2

    .line 134
    :cond_2
    new-instance v1, Lcom/helpshift/common/platform/network/websockets/HSWebSocket;

    iget-object v2, p0, Lcom/helpshift/common/platform/network/websockets/HSWebSocket$Builder;->listener:Lcom/helpshift/common/platform/network/websockets/IHSWebSocketListener;

    invoke-direct {v1, v0, v2}, Lcom/helpshift/common/platform/network/websockets/HSWebSocket;-><init>(Lcom/helpshift/websockets/WebSocket;Lcom/helpshift/common/platform/network/websockets/IHSWebSocketListener;)V

    return-object v1
.end method

.method public setConnectionTimeout(I)Lcom/helpshift/common/platform/network/websockets/HSWebSocket$Builder;
    .locals 0

    .line 83
    iput p1, p0, Lcom/helpshift/common/platform/network/websockets/HSWebSocket$Builder;->connectionTimeout:I

    return-object p0
.end method

.method public setSocketTimeout(I)Lcom/helpshift/common/platform/network/websockets/HSWebSocket$Builder;
    .locals 0

    .line 88
    iput p1, p0, Lcom/helpshift/common/platform/network/websockets/HSWebSocket$Builder;->socketTimeout:I

    return-object p0
.end method

.method public setWebSocketListener(Lcom/helpshift/common/platform/network/websockets/IHSWebSocketListener;)Lcom/helpshift/common/platform/network/websockets/HSWebSocket$Builder;
    .locals 0

    .line 110
    iput-object p1, p0, Lcom/helpshift/common/platform/network/websockets/HSWebSocket$Builder;->listener:Lcom/helpshift/common/platform/network/websockets/IHSWebSocketListener;

    return-object p0
.end method
