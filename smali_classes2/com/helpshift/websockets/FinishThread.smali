.class Lcom/helpshift/websockets/FinishThread;
.super Lcom/helpshift/websockets/WebSocketThread;
.source "FinishThread.java"


# direct methods
.method public constructor <init>(Lcom/helpshift/websockets/WebSocket;)V
    .locals 2

    const-string v0, "FinishThread"

    .line 27
    sget-object v1, Lcom/helpshift/websockets/ThreadType;->FINISH_THREAD:Lcom/helpshift/websockets/ThreadType;

    invoke-direct {p0, v0, p1, v1}, Lcom/helpshift/websockets/WebSocketThread;-><init>(Ljava/lang/String;Lcom/helpshift/websockets/WebSocket;Lcom/helpshift/websockets/ThreadType;)V

    return-void
.end method


# virtual methods
.method public runMain()V
    .locals 1

    .line 33
    iget-object v0, p0, Lcom/helpshift/websockets/FinishThread;->mWebSocket:Lcom/helpshift/websockets/WebSocket;

    invoke-virtual {v0}, Lcom/helpshift/websockets/WebSocket;->finish()V

    return-void
.end method
