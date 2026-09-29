.class public interface abstract Lcom/helpshift/websockets/WebSocketListener;
.super Ljava/lang/Object;
.source "WebSocketListener.java"


# virtual methods
.method public abstract handleCallbackError(Lcom/helpshift/websockets/WebSocket;Ljava/lang/Throwable;)V
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/lang/Exception;
        }
    .end annotation
.end method

.method public abstract onBinaryFrame(Lcom/helpshift/websockets/WebSocket;Lcom/helpshift/websockets/WebSocketFrame;)V
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/lang/Exception;
        }
    .end annotation
.end method

.method public abstract onBinaryMessage(Lcom/helpshift/websockets/WebSocket;[B)V
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/lang/Exception;
        }
    .end annotation
.end method

.method public abstract onCloseFrame(Lcom/helpshift/websockets/WebSocket;Lcom/helpshift/websockets/WebSocketFrame;)V
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/lang/Exception;
        }
    .end annotation
.end method

.method public abstract onConnectError(Lcom/helpshift/websockets/WebSocket;Lcom/helpshift/websockets/WebSocketException;)V
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/lang/Exception;
        }
    .end annotation
.end method

.method public abstract onConnected(Lcom/helpshift/websockets/WebSocket;Ljava/util/Map;)V
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
.end method

.method public abstract onContinuationFrame(Lcom/helpshift/websockets/WebSocket;Lcom/helpshift/websockets/WebSocketFrame;)V
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/lang/Exception;
        }
    .end annotation
.end method

.method public abstract onDisconnected(Lcom/helpshift/websockets/WebSocket;Lcom/helpshift/websockets/WebSocketFrame;Lcom/helpshift/websockets/WebSocketFrame;Z)V
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/lang/Exception;
        }
    .end annotation
.end method

.method public abstract onError(Lcom/helpshift/websockets/WebSocket;Lcom/helpshift/websockets/WebSocketException;)V
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/lang/Exception;
        }
    .end annotation
.end method

.method public abstract onFrame(Lcom/helpshift/websockets/WebSocket;Lcom/helpshift/websockets/WebSocketFrame;)V
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/lang/Exception;
        }
    .end annotation
.end method

.method public abstract onFrameError(Lcom/helpshift/websockets/WebSocket;Lcom/helpshift/websockets/WebSocketException;Lcom/helpshift/websockets/WebSocketFrame;)V
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/lang/Exception;
        }
    .end annotation
.end method

.method public abstract onFrameSent(Lcom/helpshift/websockets/WebSocket;Lcom/helpshift/websockets/WebSocketFrame;)V
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/lang/Exception;
        }
    .end annotation
.end method

.method public abstract onFrameUnsent(Lcom/helpshift/websockets/WebSocket;Lcom/helpshift/websockets/WebSocketFrame;)V
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/lang/Exception;
        }
    .end annotation
.end method

.method public abstract onMessageDecompressionError(Lcom/helpshift/websockets/WebSocket;Lcom/helpshift/websockets/WebSocketException;[B)V
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/lang/Exception;
        }
    .end annotation
.end method

.method public abstract onMessageError(Lcom/helpshift/websockets/WebSocket;Lcom/helpshift/websockets/WebSocketException;Ljava/util/List;)V
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
.end method

.method public abstract onPingFrame(Lcom/helpshift/websockets/WebSocket;Lcom/helpshift/websockets/WebSocketFrame;)V
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/lang/Exception;
        }
    .end annotation
.end method

.method public abstract onPongFrame(Lcom/helpshift/websockets/WebSocket;Lcom/helpshift/websockets/WebSocketFrame;)V
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/lang/Exception;
        }
    .end annotation
.end method

.method public abstract onSendError(Lcom/helpshift/websockets/WebSocket;Lcom/helpshift/websockets/WebSocketException;Lcom/helpshift/websockets/WebSocketFrame;)V
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/lang/Exception;
        }
    .end annotation
.end method

.method public abstract onSendingFrame(Lcom/helpshift/websockets/WebSocket;Lcom/helpshift/websockets/WebSocketFrame;)V
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/lang/Exception;
        }
    .end annotation
.end method

.method public abstract onSendingHandshake(Lcom/helpshift/websockets/WebSocket;Ljava/lang/String;Ljava/util/List;)V
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
.end method

.method public abstract onStateChanged(Lcom/helpshift/websockets/WebSocket;Lcom/helpshift/websockets/WebSocketState;)V
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/lang/Exception;
        }
    .end annotation
.end method

.method public abstract onTextFrame(Lcom/helpshift/websockets/WebSocket;Lcom/helpshift/websockets/WebSocketFrame;)V
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/lang/Exception;
        }
    .end annotation
.end method

.method public abstract onTextMessage(Lcom/helpshift/websockets/WebSocket;Ljava/lang/String;)V
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/lang/Exception;
        }
    .end annotation
.end method

.method public abstract onTextMessageError(Lcom/helpshift/websockets/WebSocket;Lcom/helpshift/websockets/WebSocketException;[B)V
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/lang/Exception;
        }
    .end annotation
.end method

.method public abstract onThreadCreated(Lcom/helpshift/websockets/WebSocket;Lcom/helpshift/websockets/ThreadType;Ljava/lang/Thread;)V
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/lang/Exception;
        }
    .end annotation
.end method

.method public abstract onThreadStarted(Lcom/helpshift/websockets/WebSocket;Lcom/helpshift/websockets/ThreadType;Ljava/lang/Thread;)V
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/lang/Exception;
        }
    .end annotation
.end method

.method public abstract onThreadStopping(Lcom/helpshift/websockets/WebSocket;Lcom/helpshift/websockets/ThreadType;Ljava/lang/Thread;)V
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/lang/Exception;
        }
    .end annotation
.end method

.method public abstract onUnexpectedError(Lcom/helpshift/websockets/WebSocket;Lcom/helpshift/websockets/WebSocketException;)V
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/lang/Exception;
        }
    .end annotation
.end method
