.class abstract Lcom/helpshift/websockets/WebSocketThread;
.super Ljava/lang/Thread;
.source "WebSocketThread.java"


# instance fields
.field private final mThreadType:Lcom/helpshift/websockets/ThreadType;

.field protected final mWebSocket:Lcom/helpshift/websockets/WebSocket;


# direct methods
.method constructor <init>(Ljava/lang/String;Lcom/helpshift/websockets/WebSocket;Lcom/helpshift/websockets/ThreadType;)V
    .locals 0

    .line 31
    invoke-direct {p0, p1}, Ljava/lang/Thread;-><init>(Ljava/lang/String;)V

    .line 33
    iput-object p2, p0, Lcom/helpshift/websockets/WebSocketThread;->mWebSocket:Lcom/helpshift/websockets/WebSocket;

    .line 34
    iput-object p3, p0, Lcom/helpshift/websockets/WebSocketThread;->mThreadType:Lcom/helpshift/websockets/ThreadType;

    return-void
.end method


# virtual methods
.method public callOnThreadCreated()V
    .locals 2

    .line 57
    iget-object v0, p0, Lcom/helpshift/websockets/WebSocketThread;->mWebSocket:Lcom/helpshift/websockets/WebSocket;

    invoke-virtual {v0}, Lcom/helpshift/websockets/WebSocket;->getListenerManager()Lcom/helpshift/websockets/ListenerManager;

    move-result-object v0

    if-eqz v0, :cond_0

    .line 60
    iget-object v1, p0, Lcom/helpshift/websockets/WebSocketThread;->mThreadType:Lcom/helpshift/websockets/ThreadType;

    invoke-virtual {v0, v1, p0}, Lcom/helpshift/websockets/ListenerManager;->callOnThreadCreated(Lcom/helpshift/websockets/ThreadType;Ljava/lang/Thread;)V

    :cond_0
    return-void
.end method

.method public run()V
    .locals 2

    .line 40
    iget-object v0, p0, Lcom/helpshift/websockets/WebSocketThread;->mWebSocket:Lcom/helpshift/websockets/WebSocket;

    invoke-virtual {v0}, Lcom/helpshift/websockets/WebSocket;->getListenerManager()Lcom/helpshift/websockets/ListenerManager;

    move-result-object v0

    if-eqz v0, :cond_0

    .line 44
    iget-object v1, p0, Lcom/helpshift/websockets/WebSocketThread;->mThreadType:Lcom/helpshift/websockets/ThreadType;

    invoke-virtual {v0, v1, p0}, Lcom/helpshift/websockets/ListenerManager;->callOnThreadStarted(Lcom/helpshift/websockets/ThreadType;Ljava/lang/Thread;)V

    .line 47
    :cond_0
    invoke-virtual {p0}, Lcom/helpshift/websockets/WebSocketThread;->runMain()V

    if-eqz v0, :cond_1

    .line 51
    iget-object v1, p0, Lcom/helpshift/websockets/WebSocketThread;->mThreadType:Lcom/helpshift/websockets/ThreadType;

    invoke-virtual {v0, v1, p0}, Lcom/helpshift/websockets/ListenerManager;->callOnThreadStopping(Lcom/helpshift/websockets/ThreadType;Ljava/lang/Thread;)V

    :cond_1
    return-void
.end method

.method protected abstract runMain()V
.end method
