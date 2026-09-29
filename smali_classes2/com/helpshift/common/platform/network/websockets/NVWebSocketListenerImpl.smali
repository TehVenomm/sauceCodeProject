.class Lcom/helpshift/common/platform/network/websockets/NVWebSocketListenerImpl;
.super Ljava/lang/Object;
.source "NVWebSocketListenerImpl.java"

# interfaces
.implements Lcom/helpshift/websockets/WebSocketListener;


# instance fields
.field private final delegateWebSocketListener:Lcom/helpshift/common/platform/network/websockets/IHSWebSocketListener;

.field private final hsWebSocket:Lcom/helpshift/common/platform/network/websockets/HSWebSocket;


# direct methods
.method constructor <init>(Lcom/helpshift/common/platform/network/websockets/HSWebSocket;Lcom/helpshift/common/platform/network/websockets/IHSWebSocketListener;)V
    .locals 0

    .line 23
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 24
    iput-object p2, p0, Lcom/helpshift/common/platform/network/websockets/NVWebSocketListenerImpl;->delegateWebSocketListener:Lcom/helpshift/common/platform/network/websockets/IHSWebSocketListener;

    .line 25
    iput-object p1, p0, Lcom/helpshift/common/platform/network/websockets/NVWebSocketListenerImpl;->hsWebSocket:Lcom/helpshift/common/platform/network/websockets/HSWebSocket;

    return-void
.end method


# virtual methods
.method public handleCallbackError(Lcom/helpshift/websockets/WebSocket;Ljava/lang/Throwable;)V
    .locals 0
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/lang/Exception;
        }
    .end annotation

    return-void
.end method

.method public onBinaryFrame(Lcom/helpshift/websockets/WebSocket;Lcom/helpshift/websockets/WebSocketFrame;)V
    .locals 0
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/lang/Exception;
        }
    .end annotation

    return-void
.end method

.method public onBinaryMessage(Lcom/helpshift/websockets/WebSocket;[B)V
    .locals 0
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/lang/Exception;
        }
    .end annotation

    return-void
.end method

.method public onCloseFrame(Lcom/helpshift/websockets/WebSocket;Lcom/helpshift/websockets/WebSocketFrame;)V
    .locals 0
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/lang/Exception;
        }
    .end annotation

    return-void
.end method

.method public onConnectError(Lcom/helpshift/websockets/WebSocket;Lcom/helpshift/websockets/WebSocketException;)V
    .locals 0
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/lang/Exception;
        }
    .end annotation

    return-void
.end method

.method public onConnected(Lcom/helpshift/websockets/WebSocket;Ljava/util/Map;)V
    .locals 0
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Lcom/helpshift/websockets/WebSocket;",
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Ljava/util/List<",
            "Ljava/lang/String;",
            ">;>;)V"
        }
    .end annotation

    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/lang/Exception;
        }
    .end annotation

    .line 35
    iget-object p1, p0, Lcom/helpshift/common/platform/network/websockets/NVWebSocketListenerImpl;->delegateWebSocketListener:Lcom/helpshift/common/platform/network/websockets/IHSWebSocketListener;

    iget-object p2, p0, Lcom/helpshift/common/platform/network/websockets/NVWebSocketListenerImpl;->hsWebSocket:Lcom/helpshift/common/platform/network/websockets/HSWebSocket;

    invoke-interface {p1, p2}, Lcom/helpshift/common/platform/network/websockets/IHSWebSocketListener;->onConnected(Lcom/helpshift/common/platform/network/websockets/HSWebSocket;)V

    return-void
.end method

.method public onContinuationFrame(Lcom/helpshift/websockets/WebSocket;Lcom/helpshift/websockets/WebSocketFrame;)V
    .locals 0
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/lang/Exception;
        }
    .end annotation

    return-void
.end method

.method public onDisconnected(Lcom/helpshift/websockets/WebSocket;Lcom/helpshift/websockets/WebSocketFrame;Lcom/helpshift/websockets/WebSocketFrame;Z)V
    .locals 0
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/lang/Exception;
        }
    .end annotation

    .line 46
    iget-object p1, p0, Lcom/helpshift/common/platform/network/websockets/NVWebSocketListenerImpl;->delegateWebSocketListener:Lcom/helpshift/common/platform/network/websockets/IHSWebSocketListener;

    invoke-interface {p1}, Lcom/helpshift/common/platform/network/websockets/IHSWebSocketListener;->onDisconnected()V

    return-void
.end method

.method public onError(Lcom/helpshift/websockets/WebSocket;Lcom/helpshift/websockets/WebSocketException;)V
    .locals 1
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/lang/Exception;
        }
    .end annotation

    .line 126
    iget-object p1, p0, Lcom/helpshift/common/platform/network/websockets/NVWebSocketListenerImpl;->delegateWebSocketListener:Lcom/helpshift/common/platform/network/websockets/IHSWebSocketListener;

    iget-object v0, p0, Lcom/helpshift/common/platform/network/websockets/NVWebSocketListenerImpl;->hsWebSocket:Lcom/helpshift/common/platform/network/websockets/HSWebSocket;

    invoke-virtual {p2}, Lcom/helpshift/websockets/WebSocketException;->getMessage()Ljava/lang/String;

    move-result-object p2

    invoke-interface {p1, v0, p2}, Lcom/helpshift/common/platform/network/websockets/IHSWebSocketListener;->onError(Lcom/helpshift/common/platform/network/websockets/HSWebSocket;Ljava/lang/String;)V

    return-void
.end method

.method public onFrame(Lcom/helpshift/websockets/WebSocket;Lcom/helpshift/websockets/WebSocketFrame;)V
    .locals 0
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/lang/Exception;
        }
    .end annotation

    return-void
.end method

.method public onFrameError(Lcom/helpshift/websockets/WebSocket;Lcom/helpshift/websockets/WebSocketException;Lcom/helpshift/websockets/WebSocketFrame;)V
    .locals 0
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/lang/Exception;
        }
    .end annotation

    return-void
.end method

.method public onFrameSent(Lcom/helpshift/websockets/WebSocket;Lcom/helpshift/websockets/WebSocketFrame;)V
    .locals 0
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/lang/Exception;
        }
    .end annotation

    return-void
.end method

.method public onFrameUnsent(Lcom/helpshift/websockets/WebSocket;Lcom/helpshift/websockets/WebSocketFrame;)V
    .locals 0
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/lang/Exception;
        }
    .end annotation

    return-void
.end method

.method public onMessageDecompressionError(Lcom/helpshift/websockets/WebSocket;Lcom/helpshift/websockets/WebSocketException;[B)V
    .locals 0
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/lang/Exception;
        }
    .end annotation

    .line 143
    iget-object p1, p0, Lcom/helpshift/common/platform/network/websockets/NVWebSocketListenerImpl;->delegateWebSocketListener:Lcom/helpshift/common/platform/network/websockets/IHSWebSocketListener;

    iget-object p3, p0, Lcom/helpshift/common/platform/network/websockets/NVWebSocketListenerImpl;->hsWebSocket:Lcom/helpshift/common/platform/network/websockets/HSWebSocket;

    invoke-virtual {p2}, Lcom/helpshift/websockets/WebSocketException;->getMessage()Ljava/lang/String;

    move-result-object p2

    invoke-interface {p1, p3, p2}, Lcom/helpshift/common/platform/network/websockets/IHSWebSocketListener;->onError(Lcom/helpshift/common/platform/network/websockets/HSWebSocket;Ljava/lang/String;)V

    return-void
.end method

.method public onMessageError(Lcom/helpshift/websockets/WebSocket;Lcom/helpshift/websockets/WebSocketException;Ljava/util/List;)V
    .locals 0
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Lcom/helpshift/websockets/WebSocket;",
            "Lcom/helpshift/websockets/WebSocketException;",
            "Ljava/util/List<",
            "Lcom/helpshift/websockets/WebSocketFrame;",
            ">;)V"
        }
    .end annotation

    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/lang/Exception;
        }
    .end annotation

    return-void
.end method

.method public onPingFrame(Lcom/helpshift/websockets/WebSocket;Lcom/helpshift/websockets/WebSocketFrame;)V
    .locals 0
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/lang/Exception;
        }
    .end annotation

    return-void
.end method

.method public onPongFrame(Lcom/helpshift/websockets/WebSocket;Lcom/helpshift/websockets/WebSocketFrame;)V
    .locals 0
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/lang/Exception;
        }
    .end annotation

    return-void
.end method

.method public onSendError(Lcom/helpshift/websockets/WebSocket;Lcom/helpshift/websockets/WebSocketException;Lcom/helpshift/websockets/WebSocketFrame;)V
    .locals 0
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/lang/Exception;
        }
    .end annotation

    .line 153
    iget-object p1, p0, Lcom/helpshift/common/platform/network/websockets/NVWebSocketListenerImpl;->delegateWebSocketListener:Lcom/helpshift/common/platform/network/websockets/IHSWebSocketListener;

    iget-object p3, p0, Lcom/helpshift/common/platform/network/websockets/NVWebSocketListenerImpl;->hsWebSocket:Lcom/helpshift/common/platform/network/websockets/HSWebSocket;

    invoke-virtual {p2}, Lcom/helpshift/websockets/WebSocketException;->getMessage()Ljava/lang/String;

    move-result-object p2

    invoke-interface {p1, p3, p2}, Lcom/helpshift/common/platform/network/websockets/IHSWebSocketListener;->onError(Lcom/helpshift/common/platform/network/websockets/HSWebSocket;Ljava/lang/String;)V

    return-void
.end method

.method public onSendingFrame(Lcom/helpshift/websockets/WebSocket;Lcom/helpshift/websockets/WebSocketFrame;)V
    .locals 0
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/lang/Exception;
        }
    .end annotation

    return-void
.end method

.method public onSendingHandshake(Lcom/helpshift/websockets/WebSocket;Ljava/lang/String;Ljava/util/List;)V
    .locals 0
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Lcom/helpshift/websockets/WebSocket;",
            "Ljava/lang/String;",
            "Ljava/util/List<",
            "[",
            "Ljava/lang/String;",
            ">;)V"
        }
    .end annotation

    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/lang/Exception;
        }
    .end annotation

    return-void
.end method

.method public onStateChanged(Lcom/helpshift/websockets/WebSocket;Lcom/helpshift/websockets/WebSocketState;)V
    .locals 0
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/lang/Exception;
        }
    .end annotation

    return-void
.end method

.method public onTextFrame(Lcom/helpshift/websockets/WebSocket;Lcom/helpshift/websockets/WebSocketFrame;)V
    .locals 0
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/lang/Exception;
        }
    .end annotation

    return-void
.end method

.method public onTextMessage(Lcom/helpshift/websockets/WebSocket;Ljava/lang/String;)V
    .locals 1
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/lang/Exception;
        }
    .end annotation

    .line 86
    iget-object p1, p0, Lcom/helpshift/common/platform/network/websockets/NVWebSocketListenerImpl;->delegateWebSocketListener:Lcom/helpshift/common/platform/network/websockets/IHSWebSocketListener;

    iget-object v0, p0, Lcom/helpshift/common/platform/network/websockets/NVWebSocketListenerImpl;->hsWebSocket:Lcom/helpshift/common/platform/network/websockets/HSWebSocket;

    invoke-interface {p1, v0, p2}, Lcom/helpshift/common/platform/network/websockets/IHSWebSocketListener;->onMessage(Lcom/helpshift/common/platform/network/websockets/HSWebSocket;Ljava/lang/String;)V

    return-void
.end method

.method public onTextMessageError(Lcom/helpshift/websockets/WebSocket;Lcom/helpshift/websockets/WebSocketException;[B)V
    .locals 0
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/lang/Exception;
        }
    .end annotation

    .line 148
    iget-object p1, p0, Lcom/helpshift/common/platform/network/websockets/NVWebSocketListenerImpl;->delegateWebSocketListener:Lcom/helpshift/common/platform/network/websockets/IHSWebSocketListener;

    iget-object p3, p0, Lcom/helpshift/common/platform/network/websockets/NVWebSocketListenerImpl;->hsWebSocket:Lcom/helpshift/common/platform/network/websockets/HSWebSocket;

    invoke-virtual {p2}, Lcom/helpshift/websockets/WebSocketException;->getMessage()Ljava/lang/String;

    move-result-object p2

    invoke-interface {p1, p3, p2}, Lcom/helpshift/common/platform/network/websockets/IHSWebSocketListener;->onError(Lcom/helpshift/common/platform/network/websockets/HSWebSocket;Ljava/lang/String;)V

    return-void
.end method

.method public onThreadCreated(Lcom/helpshift/websockets/WebSocket;Lcom/helpshift/websockets/ThreadType;Ljava/lang/Thread;)V
    .locals 0
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/lang/Exception;
        }
    .end annotation

    return-void
.end method

.method public onThreadStarted(Lcom/helpshift/websockets/WebSocket;Lcom/helpshift/websockets/ThreadType;Ljava/lang/Thread;)V
    .locals 0
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/lang/Exception;
        }
    .end annotation

    return-void
.end method

.method public onThreadStopping(Lcom/helpshift/websockets/WebSocket;Lcom/helpshift/websockets/ThreadType;Ljava/lang/Thread;)V
    .locals 0
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/lang/Exception;
        }
    .end annotation

    return-void
.end method

.method public onUnexpectedError(Lcom/helpshift/websockets/WebSocket;Lcom/helpshift/websockets/WebSocketException;)V
    .locals 1
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/lang/Exception;
        }
    .end annotation

    .line 158
    iget-object p1, p0, Lcom/helpshift/common/platform/network/websockets/NVWebSocketListenerImpl;->delegateWebSocketListener:Lcom/helpshift/common/platform/network/websockets/IHSWebSocketListener;

    iget-object v0, p0, Lcom/helpshift/common/platform/network/websockets/NVWebSocketListenerImpl;->hsWebSocket:Lcom/helpshift/common/platform/network/websockets/HSWebSocket;

    invoke-virtual {p2}, Lcom/helpshift/websockets/WebSocketException;->getMessage()Ljava/lang/String;

    move-result-object p2

    invoke-interface {p1, v0, p2}, Lcom/helpshift/common/platform/network/websockets/IHSWebSocketListener;->onError(Lcom/helpshift/common/platform/network/websockets/HSWebSocket;Ljava/lang/String;)V

    return-void
.end method
