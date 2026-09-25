.class public interface abstract Lcom/helpshift/common/platform/network/websockets/IHSWebSocketListener;
.super Ljava/lang/Object;
.source "IHSWebSocketListener.java"


# virtual methods
.method public abstract onConnected(Lcom/helpshift/common/platform/network/websockets/HSWebSocket;)V
.end method

.method public abstract onDisconnected()V
.end method

.method public abstract onError(Lcom/helpshift/common/platform/network/websockets/HSWebSocket;Ljava/lang/String;)V
.end method

.method public abstract onMessage(Lcom/helpshift/common/platform/network/websockets/HSWebSocket;Ljava/lang/String;)V
.end method
