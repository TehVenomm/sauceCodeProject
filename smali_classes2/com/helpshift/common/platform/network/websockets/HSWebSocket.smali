.class public Lcom/helpshift/common/platform/network/websockets/HSWebSocket;
.super Ljava/lang/Object;
.source "HSWebSocket.java"


# annotations
.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Lcom/helpshift/common/platform/network/websockets/HSWebSocket$Builder;
    }
.end annotation


# instance fields
.field private final listener:Lcom/helpshift/common/platform/network/websockets/IHSWebSocketListener;

.field private final mWebSocket:Lcom/helpshift/websockets/WebSocket;


# direct methods
.method constructor <init>(Lcom/helpshift/websockets/WebSocket;Lcom/helpshift/common/platform/network/websockets/IHSWebSocketListener;)V
    .locals 1

    .line 24
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 25
    iput-object p1, p0, Lcom/helpshift/common/platform/network/websockets/HSWebSocket;->mWebSocket:Lcom/helpshift/websockets/WebSocket;

    .line 26
    iput-object p2, p0, Lcom/helpshift/common/platform/network/websockets/HSWebSocket;->listener:Lcom/helpshift/common/platform/network/websockets/IHSWebSocketListener;

    .line 27
    new-instance v0, Lcom/helpshift/common/platform/network/websockets/NVWebSocketListenerImpl;

    invoke-direct {v0, p0, p2}, Lcom/helpshift/common/platform/network/websockets/NVWebSocketListenerImpl;-><init>(Lcom/helpshift/common/platform/network/websockets/HSWebSocket;Lcom/helpshift/common/platform/network/websockets/IHSWebSocketListener;)V

    .line 28
    invoke-virtual {p1, v0}, Lcom/helpshift/websockets/WebSocket;->addListener(Lcom/helpshift/websockets/WebSocketListener;)Lcom/helpshift/websockets/WebSocket;

    return-void
.end method


# virtual methods
.method public connect()V
    .locals 2

    .line 36
    :try_start_0
    iget-object v0, p0, Lcom/helpshift/common/platform/network/websockets/HSWebSocket;->mWebSocket:Lcom/helpshift/websockets/WebSocket;

    invoke-virtual {v0}, Lcom/helpshift/websockets/WebSocket;->connect()Lcom/helpshift/websockets/WebSocket;
    :try_end_0
    .catch Lcom/helpshift/websockets/WebSocketException; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception v0

    .line 40
    iget-object v1, p0, Lcom/helpshift/common/platform/network/websockets/HSWebSocket;->listener:Lcom/helpshift/common/platform/network/websockets/IHSWebSocketListener;

    invoke-virtual {v0}, Lcom/helpshift/websockets/WebSocketException;->getMessage()Ljava/lang/String;

    move-result-object v0

    invoke-interface {v1, p0, v0}, Lcom/helpshift/common/platform/network/websockets/IHSWebSocketListener;->onError(Lcom/helpshift/common/platform/network/websockets/HSWebSocket;Ljava/lang/String;)V

    :goto_0
    return-void
.end method

.method public disconnect()V
    .locals 1

    .line 48
    iget-object v0, p0, Lcom/helpshift/common/platform/network/websockets/HSWebSocket;->mWebSocket:Lcom/helpshift/websockets/WebSocket;

    invoke-virtual {v0}, Lcom/helpshift/websockets/WebSocket;->disconnect()Lcom/helpshift/websockets/WebSocket;

    return-void
.end method

.method public sendMessage(Ljava/lang/String;)V
    .locals 1

    .line 58
    :try_start_0
    iget-object v0, p0, Lcom/helpshift/common/platform/network/websockets/HSWebSocket;->mWebSocket:Lcom/helpshift/websockets/WebSocket;

    invoke-virtual {v0, p1}, Lcom/helpshift/websockets/WebSocket;->sendText(Ljava/lang/String;)Lcom/helpshift/websockets/WebSocket;
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception p1

    .line 61
    iget-object v0, p0, Lcom/helpshift/common/platform/network/websockets/HSWebSocket;->listener:Lcom/helpshift/common/platform/network/websockets/IHSWebSocketListener;

    invoke-virtual {p1}, Ljava/lang/Exception;->getMessage()Ljava/lang/String;

    move-result-object p1

    invoke-interface {v0, p0, p1}, Lcom/helpshift/common/platform/network/websockets/IHSWebSocketListener;->onError(Lcom/helpshift/common/platform/network/websockets/HSWebSocket;Ljava/lang/String;)V

    :goto_0
    return-void
.end method
