.class Lcom/helpshift/websockets/ListenerManager;
.super Ljava/lang/Object;
.source "ListenerManager.java"


# instance fields
.field private mCopiedListeners:Ljava/util/List;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/List<",
            "Lcom/helpshift/websockets/WebSocketListener;",
            ">;"
        }
    .end annotation
.end field

.field private final mListeners:Ljava/util/List;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/List<",
            "Lcom/helpshift/websockets/WebSocketListener;",
            ">;"
        }
    .end annotation
.end field

.field private mSyncNeeded:Z

.field private final mWebSocket:Lcom/helpshift/websockets/WebSocket;


# direct methods
.method public constructor <init>(Lcom/helpshift/websockets/WebSocket;)V
    .locals 1

    .line 38
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 32
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    iput-object v0, p0, Lcom/helpshift/websockets/ListenerManager;->mListeners:Ljava/util/List;

    const/4 v0, 0x1

    .line 34
    iput-boolean v0, p0, Lcom/helpshift/websockets/ListenerManager;->mSyncNeeded:Z

    .line 39
    iput-object p1, p0, Lcom/helpshift/websockets/ListenerManager;->mWebSocket:Lcom/helpshift/websockets/WebSocket;

    return-void
.end method

.method private callHandleCallbackError(Lcom/helpshift/websockets/WebSocketListener;Ljava/lang/Throwable;)V
    .locals 1

    .line 462
    :try_start_0
    iget-object v0, p0, Lcom/helpshift/websockets/ListenerManager;->mWebSocket:Lcom/helpshift/websockets/WebSocket;

    invoke-interface {p1, v0, p2}, Lcom/helpshift/websockets/WebSocketListener;->handleCallbackError(Lcom/helpshift/websockets/WebSocket;Ljava/lang/Throwable;)V
    :try_end_0
    .catch Ljava/lang/Throwable; {:try_start_0 .. :try_end_0} :catch_0

    :catch_0
    return-void
.end method

.method private getSynchronizedListeners()Ljava/util/List;
    .locals 4
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()",
            "Ljava/util/List<",
            "Lcom/helpshift/websockets/WebSocketListener;",
            ">;"
        }
    .end annotation

    .line 123
    iget-object v0, p0, Lcom/helpshift/websockets/ListenerManager;->mListeners:Ljava/util/List;

    monitor-enter v0

    .line 124
    :try_start_0
    iget-boolean v1, p0, Lcom/helpshift/websockets/ListenerManager;->mSyncNeeded:Z

    if-nez v1, :cond_0

    .line 125
    iget-object v1, p0, Lcom/helpshift/websockets/ListenerManager;->mCopiedListeners:Ljava/util/List;

    monitor-exit v0

    return-object v1

    .line 129
    :cond_0
    new-instance v1, Ljava/util/ArrayList;

    iget-object v2, p0, Lcom/helpshift/websockets/ListenerManager;->mListeners:Ljava/util/List;

    .line 130
    invoke-interface {v2}, Ljava/util/List;->size()I

    move-result v2

    invoke-direct {v1, v2}, Ljava/util/ArrayList;-><init>(I)V

    .line 132
    iget-object v2, p0, Lcom/helpshift/websockets/ListenerManager;->mListeners:Ljava/util/List;

    invoke-interface {v2}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v2

    :goto_0
    invoke-interface {v2}, Ljava/util/Iterator;->hasNext()Z

    move-result v3

    if-eqz v3, :cond_1

    invoke-interface {v2}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v3

    check-cast v3, Lcom/helpshift/websockets/WebSocketListener;

    .line 133
    invoke-interface {v1, v3}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    goto :goto_0

    .line 137
    :cond_1
    iput-object v1, p0, Lcom/helpshift/websockets/ListenerManager;->mCopiedListeners:Ljava/util/List;

    const/4 v2, 0x0

    .line 138
    iput-boolean v2, p0, Lcom/helpshift/websockets/ListenerManager;->mSyncNeeded:Z

    .line 140
    monitor-exit v0

    return-object v1

    :catchall_0
    move-exception v1

    .line 141
    monitor-exit v0
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    throw v1
.end method


# virtual methods
.method public addListener(Lcom/helpshift/websockets/WebSocketListener;)V
    .locals 2

    if-nez p1, :cond_0

    return-void

    .line 53
    :cond_0
    iget-object v0, p0, Lcom/helpshift/websockets/ListenerManager;->mListeners:Ljava/util/List;

    monitor-enter v0

    .line 54
    :try_start_0
    iget-object v1, p0, Lcom/helpshift/websockets/ListenerManager;->mListeners:Ljava/util/List;

    invoke-interface {v1, p1}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    const/4 p1, 0x1

    .line 55
    iput-boolean p1, p0, Lcom/helpshift/websockets/ListenerManager;->mSyncNeeded:Z

    .line 56
    monitor-exit v0

    return-void

    :catchall_0
    move-exception p1

    monitor-exit v0
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    throw p1
.end method

.method public addListeners(Ljava/util/List;)V
    .locals 3
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/List<",
            "Lcom/helpshift/websockets/WebSocketListener;",
            ">;)V"
        }
    .end annotation

    if-nez p1, :cond_0

    return-void

    .line 65
    :cond_0
    iget-object v0, p0, Lcom/helpshift/websockets/ListenerManager;->mListeners:Ljava/util/List;

    monitor-enter v0

    .line 66
    :try_start_0
    invoke-interface {p1}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object p1

    :goto_0
    invoke-interface {p1}, Ljava/util/Iterator;->hasNext()Z

    move-result v1

    if-eqz v1, :cond_2

    invoke-interface {p1}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Lcom/helpshift/websockets/WebSocketListener;

    if-nez v1, :cond_1

    goto :goto_0

    .line 71
    :cond_1
    iget-object v2, p0, Lcom/helpshift/websockets/ListenerManager;->mListeners:Ljava/util/List;

    invoke-interface {v2, v1}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    const/4 v1, 0x1

    .line 72
    iput-boolean v1, p0, Lcom/helpshift/websockets/ListenerManager;->mSyncNeeded:Z

    goto :goto_0

    .line 74
    :cond_2
    monitor-exit v0

    return-void

    :catchall_0
    move-exception p1

    monitor-exit v0
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    throw p1
.end method

.method public callOnBinaryFrame(Lcom/helpshift/websockets/WebSocketFrame;)V
    .locals 3

    .line 233
    invoke-direct {p0}, Lcom/helpshift/websockets/ListenerManager;->getSynchronizedListeners()Ljava/util/List;

    move-result-object v0

    invoke-interface {v0}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v0

    :goto_0
    invoke-interface {v0}, Ljava/util/Iterator;->hasNext()Z

    move-result v1

    if-eqz v1, :cond_0

    invoke-interface {v0}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Lcom/helpshift/websockets/WebSocketListener;

    .line 235
    :try_start_0
    iget-object v2, p0, Lcom/helpshift/websockets/ListenerManager;->mWebSocket:Lcom/helpshift/websockets/WebSocket;

    invoke-interface {v1, v2, p1}, Lcom/helpshift/websockets/WebSocketListener;->onBinaryFrame(Lcom/helpshift/websockets/WebSocket;Lcom/helpshift/websockets/WebSocketFrame;)V
    :try_end_0
    .catch Ljava/lang/Throwable; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception v2

    .line 238
    invoke-direct {p0, v1, v2}, Lcom/helpshift/websockets/ListenerManager;->callHandleCallbackError(Lcom/helpshift/websockets/WebSocketListener;Ljava/lang/Throwable;)V

    goto :goto_0

    :cond_0
    return-void
.end method

.method public callOnBinaryMessage([B)V
    .locals 3

    .line 293
    invoke-direct {p0}, Lcom/helpshift/websockets/ListenerManager;->getSynchronizedListeners()Ljava/util/List;

    move-result-object v0

    invoke-interface {v0}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v0

    :goto_0
    invoke-interface {v0}, Ljava/util/Iterator;->hasNext()Z

    move-result v1

    if-eqz v1, :cond_0

    invoke-interface {v0}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Lcom/helpshift/websockets/WebSocketListener;

    .line 295
    :try_start_0
    iget-object v2, p0, Lcom/helpshift/websockets/ListenerManager;->mWebSocket:Lcom/helpshift/websockets/WebSocket;

    invoke-interface {v1, v2, p1}, Lcom/helpshift/websockets/WebSocketListener;->onBinaryMessage(Lcom/helpshift/websockets/WebSocket;[B)V
    :try_end_0
    .catch Ljava/lang/Throwable; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception v2

    .line 298
    invoke-direct {p0, v1, v2}, Lcom/helpshift/websockets/ListenerManager;->callHandleCallbackError(Lcom/helpshift/websockets/WebSocketListener;Ljava/lang/Throwable;)V

    goto :goto_0

    :cond_0
    return-void
.end method

.method public callOnCloseFrame(Lcom/helpshift/websockets/WebSocketFrame;)V
    .locals 3

    .line 245
    invoke-direct {p0}, Lcom/helpshift/websockets/ListenerManager;->getSynchronizedListeners()Ljava/util/List;

    move-result-object v0

    invoke-interface {v0}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v0

    :goto_0
    invoke-interface {v0}, Ljava/util/Iterator;->hasNext()Z

    move-result v1

    if-eqz v1, :cond_0

    invoke-interface {v0}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Lcom/helpshift/websockets/WebSocketListener;

    .line 247
    :try_start_0
    iget-object v2, p0, Lcom/helpshift/websockets/ListenerManager;->mWebSocket:Lcom/helpshift/websockets/WebSocket;

    invoke-interface {v1, v2, p1}, Lcom/helpshift/websockets/WebSocketListener;->onCloseFrame(Lcom/helpshift/websockets/WebSocket;Lcom/helpshift/websockets/WebSocketFrame;)V
    :try_end_0
    .catch Ljava/lang/Throwable; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception v2

    .line 250
    invoke-direct {p0, v1, v2}, Lcom/helpshift/websockets/ListenerManager;->callHandleCallbackError(Lcom/helpshift/websockets/WebSocketListener;Ljava/lang/Throwable;)V

    goto :goto_0

    :cond_0
    return-void
.end method

.method public callOnConnectError(Lcom/helpshift/websockets/WebSocketException;)V
    .locals 3

    .line 170
    invoke-direct {p0}, Lcom/helpshift/websockets/ListenerManager;->getSynchronizedListeners()Ljava/util/List;

    move-result-object v0

    invoke-interface {v0}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v0

    :goto_0
    invoke-interface {v0}, Ljava/util/Iterator;->hasNext()Z

    move-result v1

    if-eqz v1, :cond_0

    invoke-interface {v0}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Lcom/helpshift/websockets/WebSocketListener;

    .line 172
    :try_start_0
    iget-object v2, p0, Lcom/helpshift/websockets/ListenerManager;->mWebSocket:Lcom/helpshift/websockets/WebSocket;

    invoke-interface {v1, v2, p1}, Lcom/helpshift/websockets/WebSocketListener;->onConnectError(Lcom/helpshift/websockets/WebSocket;Lcom/helpshift/websockets/WebSocketException;)V
    :try_end_0
    .catch Ljava/lang/Throwable; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception v2

    .line 175
    invoke-direct {p0, v1, v2}, Lcom/helpshift/websockets/ListenerManager;->callHandleCallbackError(Lcom/helpshift/websockets/WebSocketListener;Ljava/lang/Throwable;)V

    goto :goto_0

    :cond_0
    return-void
.end method

.method public callOnConnected(Ljava/util/Map;)V
    .locals 3
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Ljava/util/List<",
            "Ljava/lang/String;",
            ">;>;)V"
        }
    .end annotation

    .line 158
    invoke-direct {p0}, Lcom/helpshift/websockets/ListenerManager;->getSynchronizedListeners()Ljava/util/List;

    move-result-object v0

    invoke-interface {v0}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v0

    :goto_0
    invoke-interface {v0}, Ljava/util/Iterator;->hasNext()Z

    move-result v1

    if-eqz v1, :cond_0

    invoke-interface {v0}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Lcom/helpshift/websockets/WebSocketListener;

    .line 160
    :try_start_0
    iget-object v2, p0, Lcom/helpshift/websockets/ListenerManager;->mWebSocket:Lcom/helpshift/websockets/WebSocket;

    invoke-interface {v1, v2, p1}, Lcom/helpshift/websockets/WebSocketListener;->onConnected(Lcom/helpshift/websockets/WebSocket;Ljava/util/Map;)V
    :try_end_0
    .catch Ljava/lang/Throwable; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception v2

    .line 163
    invoke-direct {p0, v1, v2}, Lcom/helpshift/websockets/ListenerManager;->callHandleCallbackError(Lcom/helpshift/websockets/WebSocketListener;Ljava/lang/Throwable;)V

    goto :goto_0

    :cond_0
    return-void
.end method

.method public callOnContinuationFrame(Lcom/helpshift/websockets/WebSocketFrame;)V
    .locals 3

    .line 209
    invoke-direct {p0}, Lcom/helpshift/websockets/ListenerManager;->getSynchronizedListeners()Ljava/util/List;

    move-result-object v0

    invoke-interface {v0}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v0

    :goto_0
    invoke-interface {v0}, Ljava/util/Iterator;->hasNext()Z

    move-result v1

    if-eqz v1, :cond_0

    invoke-interface {v0}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Lcom/helpshift/websockets/WebSocketListener;

    .line 211
    :try_start_0
    iget-object v2, p0, Lcom/helpshift/websockets/ListenerManager;->mWebSocket:Lcom/helpshift/websockets/WebSocket;

    invoke-interface {v1, v2, p1}, Lcom/helpshift/websockets/WebSocketListener;->onContinuationFrame(Lcom/helpshift/websockets/WebSocket;Lcom/helpshift/websockets/WebSocketFrame;)V
    :try_end_0
    .catch Ljava/lang/Throwable; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception v2

    .line 214
    invoke-direct {p0, v1, v2}, Lcom/helpshift/websockets/ListenerManager;->callHandleCallbackError(Lcom/helpshift/websockets/WebSocketListener;Ljava/lang/Throwable;)V

    goto :goto_0

    :cond_0
    return-void
.end method

.method public callOnDisconnected(Lcom/helpshift/websockets/WebSocketFrame;Lcom/helpshift/websockets/WebSocketFrame;Z)V
    .locals 3

    .line 184
    invoke-direct {p0}, Lcom/helpshift/websockets/ListenerManager;->getSynchronizedListeners()Ljava/util/List;

    move-result-object v0

    invoke-interface {v0}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v0

    :goto_0
    invoke-interface {v0}, Ljava/util/Iterator;->hasNext()Z

    move-result v1

    if-eqz v1, :cond_0

    invoke-interface {v0}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Lcom/helpshift/websockets/WebSocketListener;

    .line 186
    :try_start_0
    iget-object v2, p0, Lcom/helpshift/websockets/ListenerManager;->mWebSocket:Lcom/helpshift/websockets/WebSocket;

    invoke-interface {v1, v2, p1, p2, p3}, Lcom/helpshift/websockets/WebSocketListener;->onDisconnected(Lcom/helpshift/websockets/WebSocket;Lcom/helpshift/websockets/WebSocketFrame;Lcom/helpshift/websockets/WebSocketFrame;Z)V
    :try_end_0
    .catch Ljava/lang/Throwable; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception v2

    .line 190
    invoke-direct {p0, v1, v2}, Lcom/helpshift/websockets/ListenerManager;->callHandleCallbackError(Lcom/helpshift/websockets/WebSocketListener;Ljava/lang/Throwable;)V

    goto :goto_0

    :cond_0
    return-void
.end method

.method public callOnError(Lcom/helpshift/websockets/WebSocketException;)V
    .locals 3

    .line 377
    invoke-direct {p0}, Lcom/helpshift/websockets/ListenerManager;->getSynchronizedListeners()Ljava/util/List;

    move-result-object v0

    invoke-interface {v0}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v0

    :goto_0
    invoke-interface {v0}, Ljava/util/Iterator;->hasNext()Z

    move-result v1

    if-eqz v1, :cond_0

    invoke-interface {v0}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Lcom/helpshift/websockets/WebSocketListener;

    .line 379
    :try_start_0
    iget-object v2, p0, Lcom/helpshift/websockets/ListenerManager;->mWebSocket:Lcom/helpshift/websockets/WebSocket;

    invoke-interface {v1, v2, p1}, Lcom/helpshift/websockets/WebSocketListener;->onError(Lcom/helpshift/websockets/WebSocket;Lcom/helpshift/websockets/WebSocketException;)V
    :try_end_0
    .catch Ljava/lang/Throwable; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception v2

    .line 382
    invoke-direct {p0, v1, v2}, Lcom/helpshift/websockets/ListenerManager;->callHandleCallbackError(Lcom/helpshift/websockets/WebSocketListener;Ljava/lang/Throwable;)V

    goto :goto_0

    :cond_0
    return-void
.end method

.method public callOnFrame(Lcom/helpshift/websockets/WebSocketFrame;)V
    .locals 3

    .line 197
    invoke-direct {p0}, Lcom/helpshift/websockets/ListenerManager;->getSynchronizedListeners()Ljava/util/List;

    move-result-object v0

    invoke-interface {v0}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v0

    :goto_0
    invoke-interface {v0}, Ljava/util/Iterator;->hasNext()Z

    move-result v1

    if-eqz v1, :cond_0

    invoke-interface {v0}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Lcom/helpshift/websockets/WebSocketListener;

    .line 199
    :try_start_0
    iget-object v2, p0, Lcom/helpshift/websockets/ListenerManager;->mWebSocket:Lcom/helpshift/websockets/WebSocket;

    invoke-interface {v1, v2, p1}, Lcom/helpshift/websockets/WebSocketListener;->onFrame(Lcom/helpshift/websockets/WebSocket;Lcom/helpshift/websockets/WebSocketFrame;)V
    :try_end_0
    .catch Ljava/lang/Throwable; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception v2

    .line 202
    invoke-direct {p0, v1, v2}, Lcom/helpshift/websockets/ListenerManager;->callHandleCallbackError(Lcom/helpshift/websockets/WebSocketListener;Ljava/lang/Throwable;)V

    goto :goto_0

    :cond_0
    return-void
.end method

.method public callOnFrameError(Lcom/helpshift/websockets/WebSocketException;Lcom/helpshift/websockets/WebSocketFrame;)V
    .locals 3

    .line 389
    invoke-direct {p0}, Lcom/helpshift/websockets/ListenerManager;->getSynchronizedListeners()Ljava/util/List;

    move-result-object v0

    invoke-interface {v0}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v0

    :goto_0
    invoke-interface {v0}, Ljava/util/Iterator;->hasNext()Z

    move-result v1

    if-eqz v1, :cond_0

    invoke-interface {v0}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Lcom/helpshift/websockets/WebSocketListener;

    .line 391
    :try_start_0
    iget-object v2, p0, Lcom/helpshift/websockets/ListenerManager;->mWebSocket:Lcom/helpshift/websockets/WebSocket;

    invoke-interface {v1, v2, p1, p2}, Lcom/helpshift/websockets/WebSocketListener;->onFrameError(Lcom/helpshift/websockets/WebSocket;Lcom/helpshift/websockets/WebSocketException;Lcom/helpshift/websockets/WebSocketFrame;)V
    :try_end_0
    .catch Ljava/lang/Throwable; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception v2

    .line 394
    invoke-direct {p0, v1, v2}, Lcom/helpshift/websockets/ListenerManager;->callHandleCallbackError(Lcom/helpshift/websockets/WebSocketListener;Ljava/lang/Throwable;)V

    goto :goto_0

    :cond_0
    return-void
.end method

.method public callOnFrameSent(Lcom/helpshift/websockets/WebSocketFrame;)V
    .locals 3

    .line 317
    invoke-direct {p0}, Lcom/helpshift/websockets/ListenerManager;->getSynchronizedListeners()Ljava/util/List;

    move-result-object v0

    invoke-interface {v0}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v0

    :goto_0
    invoke-interface {v0}, Ljava/util/Iterator;->hasNext()Z

    move-result v1

    if-eqz v1, :cond_0

    invoke-interface {v0}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Lcom/helpshift/websockets/WebSocketListener;

    .line 319
    :try_start_0
    iget-object v2, p0, Lcom/helpshift/websockets/ListenerManager;->mWebSocket:Lcom/helpshift/websockets/WebSocket;

    invoke-interface {v1, v2, p1}, Lcom/helpshift/websockets/WebSocketListener;->onFrameSent(Lcom/helpshift/websockets/WebSocket;Lcom/helpshift/websockets/WebSocketFrame;)V
    :try_end_0
    .catch Ljava/lang/Throwable; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception v2

    .line 322
    invoke-direct {p0, v1, v2}, Lcom/helpshift/websockets/ListenerManager;->callHandleCallbackError(Lcom/helpshift/websockets/WebSocketListener;Ljava/lang/Throwable;)V

    goto :goto_0

    :cond_0
    return-void
.end method

.method public callOnFrameUnsent(Lcom/helpshift/websockets/WebSocketFrame;)V
    .locals 3

    .line 329
    invoke-direct {p0}, Lcom/helpshift/websockets/ListenerManager;->getSynchronizedListeners()Ljava/util/List;

    move-result-object v0

    invoke-interface {v0}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v0

    :goto_0
    invoke-interface {v0}, Ljava/util/Iterator;->hasNext()Z

    move-result v1

    if-eqz v1, :cond_0

    invoke-interface {v0}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Lcom/helpshift/websockets/WebSocketListener;

    .line 331
    :try_start_0
    iget-object v2, p0, Lcom/helpshift/websockets/ListenerManager;->mWebSocket:Lcom/helpshift/websockets/WebSocket;

    invoke-interface {v1, v2, p1}, Lcom/helpshift/websockets/WebSocketListener;->onFrameUnsent(Lcom/helpshift/websockets/WebSocket;Lcom/helpshift/websockets/WebSocketFrame;)V
    :try_end_0
    .catch Ljava/lang/Throwable; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception v2

    .line 334
    invoke-direct {p0, v1, v2}, Lcom/helpshift/websockets/ListenerManager;->callHandleCallbackError(Lcom/helpshift/websockets/WebSocketListener;Ljava/lang/Throwable;)V

    goto :goto_0

    :cond_0
    return-void
.end method

.method public callOnMessageDecompressionError(Lcom/helpshift/websockets/WebSocketException;[B)V
    .locals 3

    .line 413
    invoke-direct {p0}, Lcom/helpshift/websockets/ListenerManager;->getSynchronizedListeners()Ljava/util/List;

    move-result-object v0

    invoke-interface {v0}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v0

    :goto_0
    invoke-interface {v0}, Ljava/util/Iterator;->hasNext()Z

    move-result v1

    if-eqz v1, :cond_0

    invoke-interface {v0}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Lcom/helpshift/websockets/WebSocketListener;

    .line 415
    :try_start_0
    iget-object v2, p0, Lcom/helpshift/websockets/ListenerManager;->mWebSocket:Lcom/helpshift/websockets/WebSocket;

    invoke-interface {v1, v2, p1, p2}, Lcom/helpshift/websockets/WebSocketListener;->onMessageDecompressionError(Lcom/helpshift/websockets/WebSocket;Lcom/helpshift/websockets/WebSocketException;[B)V
    :try_end_0
    .catch Ljava/lang/Throwable; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception v2

    .line 418
    invoke-direct {p0, v1, v2}, Lcom/helpshift/websockets/ListenerManager;->callHandleCallbackError(Lcom/helpshift/websockets/WebSocketListener;Ljava/lang/Throwable;)V

    goto :goto_0

    :cond_0
    return-void
.end method

.method public callOnMessageError(Lcom/helpshift/websockets/WebSocketException;Ljava/util/List;)V
    .locals 3
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Lcom/helpshift/websockets/WebSocketException;",
            "Ljava/util/List<",
            "Lcom/helpshift/websockets/WebSocketFrame;",
            ">;)V"
        }
    .end annotation

    .line 401
    invoke-direct {p0}, Lcom/helpshift/websockets/ListenerManager;->getSynchronizedListeners()Ljava/util/List;

    move-result-object v0

    invoke-interface {v0}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v0

    :goto_0
    invoke-interface {v0}, Ljava/util/Iterator;->hasNext()Z

    move-result v1

    if-eqz v1, :cond_0

    invoke-interface {v0}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Lcom/helpshift/websockets/WebSocketListener;

    .line 403
    :try_start_0
    iget-object v2, p0, Lcom/helpshift/websockets/ListenerManager;->mWebSocket:Lcom/helpshift/websockets/WebSocket;

    invoke-interface {v1, v2, p1, p2}, Lcom/helpshift/websockets/WebSocketListener;->onMessageError(Lcom/helpshift/websockets/WebSocket;Lcom/helpshift/websockets/WebSocketException;Ljava/util/List;)V
    :try_end_0
    .catch Ljava/lang/Throwable; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception v2

    .line 406
    invoke-direct {p0, v1, v2}, Lcom/helpshift/websockets/ListenerManager;->callHandleCallbackError(Lcom/helpshift/websockets/WebSocketListener;Ljava/lang/Throwable;)V

    goto :goto_0

    :cond_0
    return-void
.end method

.method public callOnPingFrame(Lcom/helpshift/websockets/WebSocketFrame;)V
    .locals 3

    .line 257
    invoke-direct {p0}, Lcom/helpshift/websockets/ListenerManager;->getSynchronizedListeners()Ljava/util/List;

    move-result-object v0

    invoke-interface {v0}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v0

    :goto_0
    invoke-interface {v0}, Ljava/util/Iterator;->hasNext()Z

    move-result v1

    if-eqz v1, :cond_0

    invoke-interface {v0}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Lcom/helpshift/websockets/WebSocketListener;

    .line 259
    :try_start_0
    iget-object v2, p0, Lcom/helpshift/websockets/ListenerManager;->mWebSocket:Lcom/helpshift/websockets/WebSocket;

    invoke-interface {v1, v2, p1}, Lcom/helpshift/websockets/WebSocketListener;->onPingFrame(Lcom/helpshift/websockets/WebSocket;Lcom/helpshift/websockets/WebSocketFrame;)V
    :try_end_0
    .catch Ljava/lang/Throwable; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception v2

    .line 262
    invoke-direct {p0, v1, v2}, Lcom/helpshift/websockets/ListenerManager;->callHandleCallbackError(Lcom/helpshift/websockets/WebSocketListener;Ljava/lang/Throwable;)V

    goto :goto_0

    :cond_0
    return-void
.end method

.method public callOnPongFrame(Lcom/helpshift/websockets/WebSocketFrame;)V
    .locals 3

    .line 269
    invoke-direct {p0}, Lcom/helpshift/websockets/ListenerManager;->getSynchronizedListeners()Ljava/util/List;

    move-result-object v0

    invoke-interface {v0}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v0

    :goto_0
    invoke-interface {v0}, Ljava/util/Iterator;->hasNext()Z

    move-result v1

    if-eqz v1, :cond_0

    invoke-interface {v0}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Lcom/helpshift/websockets/WebSocketListener;

    .line 271
    :try_start_0
    iget-object v2, p0, Lcom/helpshift/websockets/ListenerManager;->mWebSocket:Lcom/helpshift/websockets/WebSocket;

    invoke-interface {v1, v2, p1}, Lcom/helpshift/websockets/WebSocketListener;->onPongFrame(Lcom/helpshift/websockets/WebSocket;Lcom/helpshift/websockets/WebSocketFrame;)V
    :try_end_0
    .catch Ljava/lang/Throwable; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception v2

    .line 274
    invoke-direct {p0, v1, v2}, Lcom/helpshift/websockets/ListenerManager;->callHandleCallbackError(Lcom/helpshift/websockets/WebSocketListener;Ljava/lang/Throwable;)V

    goto :goto_0

    :cond_0
    return-void
.end method

.method public callOnSendError(Lcom/helpshift/websockets/WebSocketException;Lcom/helpshift/websockets/WebSocketFrame;)V
    .locals 3

    .line 437
    invoke-direct {p0}, Lcom/helpshift/websockets/ListenerManager;->getSynchronizedListeners()Ljava/util/List;

    move-result-object v0

    invoke-interface {v0}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v0

    :goto_0
    invoke-interface {v0}, Ljava/util/Iterator;->hasNext()Z

    move-result v1

    if-eqz v1, :cond_0

    invoke-interface {v0}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Lcom/helpshift/websockets/WebSocketListener;

    .line 439
    :try_start_0
    iget-object v2, p0, Lcom/helpshift/websockets/ListenerManager;->mWebSocket:Lcom/helpshift/websockets/WebSocket;

    invoke-interface {v1, v2, p1, p2}, Lcom/helpshift/websockets/WebSocketListener;->onSendError(Lcom/helpshift/websockets/WebSocket;Lcom/helpshift/websockets/WebSocketException;Lcom/helpshift/websockets/WebSocketFrame;)V
    :try_end_0
    .catch Ljava/lang/Throwable; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception v2

    .line 442
    invoke-direct {p0, v1, v2}, Lcom/helpshift/websockets/ListenerManager;->callHandleCallbackError(Lcom/helpshift/websockets/WebSocketListener;Ljava/lang/Throwable;)V

    goto :goto_0

    :cond_0
    return-void
.end method

.method public callOnSendingFrame(Lcom/helpshift/websockets/WebSocketFrame;)V
    .locals 3

    .line 305
    invoke-direct {p0}, Lcom/helpshift/websockets/ListenerManager;->getSynchronizedListeners()Ljava/util/List;

    move-result-object v0

    invoke-interface {v0}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v0

    :goto_0
    invoke-interface {v0}, Ljava/util/Iterator;->hasNext()Z

    move-result v1

    if-eqz v1, :cond_0

    invoke-interface {v0}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Lcom/helpshift/websockets/WebSocketListener;

    .line 307
    :try_start_0
    iget-object v2, p0, Lcom/helpshift/websockets/ListenerManager;->mWebSocket:Lcom/helpshift/websockets/WebSocket;

    invoke-interface {v1, v2, p1}, Lcom/helpshift/websockets/WebSocketListener;->onSendingFrame(Lcom/helpshift/websockets/WebSocket;Lcom/helpshift/websockets/WebSocketFrame;)V
    :try_end_0
    .catch Ljava/lang/Throwable; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception v2

    .line 310
    invoke-direct {p0, v1, v2}, Lcom/helpshift/websockets/ListenerManager;->callHandleCallbackError(Lcom/helpshift/websockets/WebSocketListener;Ljava/lang/Throwable;)V

    goto :goto_0

    :cond_0
    return-void
.end method

.method public callOnSendingHandshake(Ljava/lang/String;Ljava/util/List;)V
    .locals 3
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/lang/String;",
            "Ljava/util/List<",
            "[",
            "Ljava/lang/String;",
            ">;)V"
        }
    .end annotation

    .line 470
    invoke-direct {p0}, Lcom/helpshift/websockets/ListenerManager;->getSynchronizedListeners()Ljava/util/List;

    move-result-object v0

    invoke-interface {v0}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v0

    :goto_0
    invoke-interface {v0}, Ljava/util/Iterator;->hasNext()Z

    move-result v1

    if-eqz v1, :cond_0

    invoke-interface {v0}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Lcom/helpshift/websockets/WebSocketListener;

    .line 472
    :try_start_0
    iget-object v2, p0, Lcom/helpshift/websockets/ListenerManager;->mWebSocket:Lcom/helpshift/websockets/WebSocket;

    invoke-interface {v1, v2, p1, p2}, Lcom/helpshift/websockets/WebSocketListener;->onSendingHandshake(Lcom/helpshift/websockets/WebSocket;Ljava/lang/String;Ljava/util/List;)V
    :try_end_0
    .catch Ljava/lang/Throwable; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception v2

    .line 475
    invoke-direct {p0, v1, v2}, Lcom/helpshift/websockets/ListenerManager;->callHandleCallbackError(Lcom/helpshift/websockets/WebSocketListener;Ljava/lang/Throwable;)V

    goto :goto_0

    :cond_0
    return-void
.end method

.method public callOnStateChanged(Lcom/helpshift/websockets/WebSocketState;)V
    .locals 3

    .line 146
    invoke-direct {p0}, Lcom/helpshift/websockets/ListenerManager;->getSynchronizedListeners()Ljava/util/List;

    move-result-object v0

    invoke-interface {v0}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v0

    :goto_0
    invoke-interface {v0}, Ljava/util/Iterator;->hasNext()Z

    move-result v1

    if-eqz v1, :cond_0

    invoke-interface {v0}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Lcom/helpshift/websockets/WebSocketListener;

    .line 148
    :try_start_0
    iget-object v2, p0, Lcom/helpshift/websockets/ListenerManager;->mWebSocket:Lcom/helpshift/websockets/WebSocket;

    invoke-interface {v1, v2, p1}, Lcom/helpshift/websockets/WebSocketListener;->onStateChanged(Lcom/helpshift/websockets/WebSocket;Lcom/helpshift/websockets/WebSocketState;)V
    :try_end_0
    .catch Ljava/lang/Throwable; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception v2

    .line 151
    invoke-direct {p0, v1, v2}, Lcom/helpshift/websockets/ListenerManager;->callHandleCallbackError(Lcom/helpshift/websockets/WebSocketListener;Ljava/lang/Throwable;)V

    goto :goto_0

    :cond_0
    return-void
.end method

.method public callOnTextFrame(Lcom/helpshift/websockets/WebSocketFrame;)V
    .locals 3

    .line 221
    invoke-direct {p0}, Lcom/helpshift/websockets/ListenerManager;->getSynchronizedListeners()Ljava/util/List;

    move-result-object v0

    invoke-interface {v0}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v0

    :goto_0
    invoke-interface {v0}, Ljava/util/Iterator;->hasNext()Z

    move-result v1

    if-eqz v1, :cond_0

    invoke-interface {v0}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Lcom/helpshift/websockets/WebSocketListener;

    .line 223
    :try_start_0
    iget-object v2, p0, Lcom/helpshift/websockets/ListenerManager;->mWebSocket:Lcom/helpshift/websockets/WebSocket;

    invoke-interface {v1, v2, p1}, Lcom/helpshift/websockets/WebSocketListener;->onTextFrame(Lcom/helpshift/websockets/WebSocket;Lcom/helpshift/websockets/WebSocketFrame;)V
    :try_end_0
    .catch Ljava/lang/Throwable; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception v2

    .line 226
    invoke-direct {p0, v1, v2}, Lcom/helpshift/websockets/ListenerManager;->callHandleCallbackError(Lcom/helpshift/websockets/WebSocketListener;Ljava/lang/Throwable;)V

    goto :goto_0

    :cond_0
    return-void
.end method

.method public callOnTextMessage(Ljava/lang/String;)V
    .locals 3

    .line 281
    invoke-direct {p0}, Lcom/helpshift/websockets/ListenerManager;->getSynchronizedListeners()Ljava/util/List;

    move-result-object v0

    invoke-interface {v0}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v0

    :goto_0
    invoke-interface {v0}, Ljava/util/Iterator;->hasNext()Z

    move-result v1

    if-eqz v1, :cond_0

    invoke-interface {v0}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Lcom/helpshift/websockets/WebSocketListener;

    .line 283
    :try_start_0
    iget-object v2, p0, Lcom/helpshift/websockets/ListenerManager;->mWebSocket:Lcom/helpshift/websockets/WebSocket;

    invoke-interface {v1, v2, p1}, Lcom/helpshift/websockets/WebSocketListener;->onTextMessage(Lcom/helpshift/websockets/WebSocket;Ljava/lang/String;)V
    :try_end_0
    .catch Ljava/lang/Throwable; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception v2

    .line 286
    invoke-direct {p0, v1, v2}, Lcom/helpshift/websockets/ListenerManager;->callHandleCallbackError(Lcom/helpshift/websockets/WebSocketListener;Ljava/lang/Throwable;)V

    goto :goto_0

    :cond_0
    return-void
.end method

.method public callOnTextMessageError(Lcom/helpshift/websockets/WebSocketException;[B)V
    .locals 3

    .line 425
    invoke-direct {p0}, Lcom/helpshift/websockets/ListenerManager;->getSynchronizedListeners()Ljava/util/List;

    move-result-object v0

    invoke-interface {v0}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v0

    :goto_0
    invoke-interface {v0}, Ljava/util/Iterator;->hasNext()Z

    move-result v1

    if-eqz v1, :cond_0

    invoke-interface {v0}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Lcom/helpshift/websockets/WebSocketListener;

    .line 427
    :try_start_0
    iget-object v2, p0, Lcom/helpshift/websockets/ListenerManager;->mWebSocket:Lcom/helpshift/websockets/WebSocket;

    invoke-interface {v1, v2, p1, p2}, Lcom/helpshift/websockets/WebSocketListener;->onTextMessageError(Lcom/helpshift/websockets/WebSocket;Lcom/helpshift/websockets/WebSocketException;[B)V
    :try_end_0
    .catch Ljava/lang/Throwable; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception v2

    .line 430
    invoke-direct {p0, v1, v2}, Lcom/helpshift/websockets/ListenerManager;->callHandleCallbackError(Lcom/helpshift/websockets/WebSocketListener;Ljava/lang/Throwable;)V

    goto :goto_0

    :cond_0
    return-void
.end method

.method public callOnThreadCreated(Lcom/helpshift/websockets/ThreadType;Ljava/lang/Thread;)V
    .locals 3

    .line 341
    invoke-direct {p0}, Lcom/helpshift/websockets/ListenerManager;->getSynchronizedListeners()Ljava/util/List;

    move-result-object v0

    invoke-interface {v0}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v0

    :goto_0
    invoke-interface {v0}, Ljava/util/Iterator;->hasNext()Z

    move-result v1

    if-eqz v1, :cond_0

    invoke-interface {v0}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Lcom/helpshift/websockets/WebSocketListener;

    .line 343
    :try_start_0
    iget-object v2, p0, Lcom/helpshift/websockets/ListenerManager;->mWebSocket:Lcom/helpshift/websockets/WebSocket;

    invoke-interface {v1, v2, p1, p2}, Lcom/helpshift/websockets/WebSocketListener;->onThreadCreated(Lcom/helpshift/websockets/WebSocket;Lcom/helpshift/websockets/ThreadType;Ljava/lang/Thread;)V
    :try_end_0
    .catch Ljava/lang/Throwable; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception v2

    .line 346
    invoke-direct {p0, v1, v2}, Lcom/helpshift/websockets/ListenerManager;->callHandleCallbackError(Lcom/helpshift/websockets/WebSocketListener;Ljava/lang/Throwable;)V

    goto :goto_0

    :cond_0
    return-void
.end method

.method public callOnThreadStarted(Lcom/helpshift/websockets/ThreadType;Ljava/lang/Thread;)V
    .locals 3

    .line 353
    invoke-direct {p0}, Lcom/helpshift/websockets/ListenerManager;->getSynchronizedListeners()Ljava/util/List;

    move-result-object v0

    invoke-interface {v0}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v0

    :goto_0
    invoke-interface {v0}, Ljava/util/Iterator;->hasNext()Z

    move-result v1

    if-eqz v1, :cond_0

    invoke-interface {v0}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Lcom/helpshift/websockets/WebSocketListener;

    .line 355
    :try_start_0
    iget-object v2, p0, Lcom/helpshift/websockets/ListenerManager;->mWebSocket:Lcom/helpshift/websockets/WebSocket;

    invoke-interface {v1, v2, p1, p2}, Lcom/helpshift/websockets/WebSocketListener;->onThreadStarted(Lcom/helpshift/websockets/WebSocket;Lcom/helpshift/websockets/ThreadType;Ljava/lang/Thread;)V
    :try_end_0
    .catch Ljava/lang/Throwable; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception v2

    .line 358
    invoke-direct {p0, v1, v2}, Lcom/helpshift/websockets/ListenerManager;->callHandleCallbackError(Lcom/helpshift/websockets/WebSocketListener;Ljava/lang/Throwable;)V

    goto :goto_0

    :cond_0
    return-void
.end method

.method public callOnThreadStopping(Lcom/helpshift/websockets/ThreadType;Ljava/lang/Thread;)V
    .locals 3

    .line 365
    invoke-direct {p0}, Lcom/helpshift/websockets/ListenerManager;->getSynchronizedListeners()Ljava/util/List;

    move-result-object v0

    invoke-interface {v0}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v0

    :goto_0
    invoke-interface {v0}, Ljava/util/Iterator;->hasNext()Z

    move-result v1

    if-eqz v1, :cond_0

    invoke-interface {v0}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Lcom/helpshift/websockets/WebSocketListener;

    .line 367
    :try_start_0
    iget-object v2, p0, Lcom/helpshift/websockets/ListenerManager;->mWebSocket:Lcom/helpshift/websockets/WebSocket;

    invoke-interface {v1, v2, p1, p2}, Lcom/helpshift/websockets/WebSocketListener;->onThreadStopping(Lcom/helpshift/websockets/WebSocket;Lcom/helpshift/websockets/ThreadType;Ljava/lang/Thread;)V
    :try_end_0
    .catch Ljava/lang/Throwable; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception v2

    .line 370
    invoke-direct {p0, v1, v2}, Lcom/helpshift/websockets/ListenerManager;->callHandleCallbackError(Lcom/helpshift/websockets/WebSocketListener;Ljava/lang/Throwable;)V

    goto :goto_0

    :cond_0
    return-void
.end method

.method public callOnUnexpectedError(Lcom/helpshift/websockets/WebSocketException;)V
    .locals 3

    .line 449
    invoke-direct {p0}, Lcom/helpshift/websockets/ListenerManager;->getSynchronizedListeners()Ljava/util/List;

    move-result-object v0

    invoke-interface {v0}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v0

    :goto_0
    invoke-interface {v0}, Ljava/util/Iterator;->hasNext()Z

    move-result v1

    if-eqz v1, :cond_0

    invoke-interface {v0}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Lcom/helpshift/websockets/WebSocketListener;

    .line 451
    :try_start_0
    iget-object v2, p0, Lcom/helpshift/websockets/ListenerManager;->mWebSocket:Lcom/helpshift/websockets/WebSocket;

    invoke-interface {v1, v2, p1}, Lcom/helpshift/websockets/WebSocketListener;->onUnexpectedError(Lcom/helpshift/websockets/WebSocket;Lcom/helpshift/websockets/WebSocketException;)V
    :try_end_0
    .catch Ljava/lang/Throwable; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception v2

    .line 454
    invoke-direct {p0, v1, v2}, Lcom/helpshift/websockets/ListenerManager;->callHandleCallbackError(Lcom/helpshift/websockets/WebSocketListener;Ljava/lang/Throwable;)V

    goto :goto_0

    :cond_0
    return-void
.end method

.method public clearListeners()V
    .locals 2

    .line 111
    iget-object v0, p0, Lcom/helpshift/websockets/ListenerManager;->mListeners:Ljava/util/List;

    monitor-enter v0

    .line 112
    :try_start_0
    iget-object v1, p0, Lcom/helpshift/websockets/ListenerManager;->mListeners:Ljava/util/List;

    invoke-interface {v1}, Ljava/util/List;->size()I

    move-result v1

    if-nez v1, :cond_0

    .line 113
    monitor-exit v0

    return-void

    .line 116
    :cond_0
    iget-object v1, p0, Lcom/helpshift/websockets/ListenerManager;->mListeners:Ljava/util/List;

    invoke-interface {v1}, Ljava/util/List;->clear()V

    const/4 v1, 0x1

    .line 117
    iput-boolean v1, p0, Lcom/helpshift/websockets/ListenerManager;->mSyncNeeded:Z

    .line 118
    monitor-exit v0

    return-void

    :catchall_0
    move-exception v1

    monitor-exit v0
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    throw v1
.end method

.method public getListeners()Ljava/util/List;
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()",
            "Ljava/util/List<",
            "Lcom/helpshift/websockets/WebSocketListener;",
            ">;"
        }
    .end annotation

    .line 44
    iget-object v0, p0, Lcom/helpshift/websockets/ListenerManager;->mListeners:Ljava/util/List;

    return-object v0
.end method

.method public removeListener(Lcom/helpshift/websockets/WebSocketListener;)V
    .locals 2

    if-nez p1, :cond_0

    return-void

    .line 83
    :cond_0
    iget-object v0, p0, Lcom/helpshift/websockets/ListenerManager;->mListeners:Ljava/util/List;

    monitor-enter v0

    .line 84
    :try_start_0
    iget-object v1, p0, Lcom/helpshift/websockets/ListenerManager;->mListeners:Ljava/util/List;

    invoke-interface {v1, p1}, Ljava/util/List;->remove(Ljava/lang/Object;)Z

    move-result p1

    if-eqz p1, :cond_1

    const/4 p1, 0x1

    .line 85
    iput-boolean p1, p0, Lcom/helpshift/websockets/ListenerManager;->mSyncNeeded:Z

    .line 87
    :cond_1
    monitor-exit v0

    return-void

    :catchall_0
    move-exception p1

    monitor-exit v0
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    throw p1
.end method

.method public removeListeners(Ljava/util/List;)V
    .locals 3
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/List<",
            "Lcom/helpshift/websockets/WebSocketListener;",
            ">;)V"
        }
    .end annotation

    if-nez p1, :cond_0

    return-void

    .line 96
    :cond_0
    iget-object v0, p0, Lcom/helpshift/websockets/ListenerManager;->mListeners:Ljava/util/List;

    monitor-enter v0

    .line 97
    :try_start_0
    invoke-interface {p1}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object p1

    :cond_1
    :goto_0
    invoke-interface {p1}, Ljava/util/Iterator;->hasNext()Z

    move-result v1

    if-eqz v1, :cond_3

    invoke-interface {p1}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Lcom/helpshift/websockets/WebSocketListener;

    if-nez v1, :cond_2

    goto :goto_0

    .line 102
    :cond_2
    iget-object v2, p0, Lcom/helpshift/websockets/ListenerManager;->mListeners:Ljava/util/List;

    invoke-interface {v2, v1}, Ljava/util/List;->remove(Ljava/lang/Object;)Z

    move-result v1

    if-eqz v1, :cond_1

    const/4 v1, 0x1

    .line 103
    iput-boolean v1, p0, Lcom/helpshift/websockets/ListenerManager;->mSyncNeeded:Z

    goto :goto_0

    .line 106
    :cond_3
    monitor-exit v0

    return-void

    :catchall_0
    move-exception p1

    monitor-exit v0
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    throw p1
.end method
