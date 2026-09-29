.class Lcom/helpshift/websockets/WritingThread;
.super Lcom/helpshift/websockets/WebSocketThread;
.source "WritingThread.java"


# static fields
.field private static final FLUSH_THRESHOLD:I = 0x3e8

.field private static final SHOULD_CONTINUE:I = 0x2

.field private static final SHOULD_FLUSH:I = 0x3

.field private static final SHOULD_SEND:I = 0x0

.field private static final SHOULD_STOP:I = 0x1


# instance fields
.field private mCloseFrame:Lcom/helpshift/websockets/WebSocketFrame;

.field private mFlushNeeded:Z

.field private final mFrames:Ljava/util/LinkedList;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/LinkedList<",
            "Lcom/helpshift/websockets/WebSocketFrame;",
            ">;"
        }
    .end annotation
.end field

.field private final mPMCE:Lcom/helpshift/websockets/PerMessageCompressionExtension;

.field private mStopRequested:Z

.field private mStopped:Z


# direct methods
.method public constructor <init>(Lcom/helpshift/websockets/WebSocket;)V
    .locals 2

    const-string v0, "WritingThread"

    .line 49
    sget-object v1, Lcom/helpshift/websockets/ThreadType;->WRITING_THREAD:Lcom/helpshift/websockets/ThreadType;

    invoke-direct {p0, v0, p1, v1}, Lcom/helpshift/websockets/WebSocketThread;-><init>(Ljava/lang/String;Lcom/helpshift/websockets/WebSocket;Lcom/helpshift/websockets/ThreadType;)V

    .line 51
    new-instance v0, Ljava/util/LinkedList;

    invoke-direct {v0}, Ljava/util/LinkedList;-><init>()V

    iput-object v0, p0, Lcom/helpshift/websockets/WritingThread;->mFrames:Ljava/util/LinkedList;

    .line 52
    invoke-virtual {p1}, Lcom/helpshift/websockets/WebSocket;->getPerMessageCompressionExtension()Lcom/helpshift/websockets/PerMessageCompressionExtension;

    move-result-object p1

    iput-object p1, p0, Lcom/helpshift/websockets/WritingThread;->mPMCE:Lcom/helpshift/websockets/PerMessageCompressionExtension;

    return-void
.end method

.method private addHighPriorityFrame(Lcom/helpshift/websockets/WebSocketFrame;)V
    .locals 3

    .line 201
    iget-object v0, p0, Lcom/helpshift/websockets/WritingThread;->mFrames:Ljava/util/LinkedList;

    invoke-virtual {v0}, Ljava/util/LinkedList;->iterator()Ljava/util/Iterator;

    move-result-object v0

    const/4 v1, 0x0

    :goto_0
    invoke-interface {v0}, Ljava/util/Iterator;->hasNext()Z

    move-result v2

    if-eqz v2, :cond_1

    invoke-interface {v0}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Lcom/helpshift/websockets/WebSocketFrame;

    .line 203
    invoke-static {v2}, Lcom/helpshift/websockets/WritingThread;->isHighPriorityFrame(Lcom/helpshift/websockets/WebSocketFrame;)Z

    move-result v2

    if-nez v2, :cond_0

    goto :goto_1

    :cond_0
    add-int/lit8 v1, v1, 0x1

    goto :goto_0

    .line 210
    :cond_1
    :goto_1
    iget-object v0, p0, Lcom/helpshift/websockets/WritingThread;->mFrames:Ljava/util/LinkedList;

    invoke-virtual {v0, v1, p1}, Ljava/util/LinkedList;->add(ILjava/lang/Object;)V

    return-void
.end method

.method private changeToClosing()V
    .locals 3

    .line 439
    iget-object v0, p0, Lcom/helpshift/websockets/WritingThread;->mWebSocket:Lcom/helpshift/websockets/WebSocket;

    invoke-virtual {v0}, Lcom/helpshift/websockets/WebSocket;->getStateManager()Lcom/helpshift/websockets/StateManager;

    move-result-object v0

    .line 443
    monitor-enter v0

    .line 445
    :try_start_0
    invoke-virtual {v0}, Lcom/helpshift/websockets/StateManager;->getState()Lcom/helpshift/websockets/WebSocketState;

    move-result-object v1

    .line 448
    sget-object v2, Lcom/helpshift/websockets/WebSocketState;->CLOSING:Lcom/helpshift/websockets/WebSocketState;

    if-eq v1, v2, :cond_0

    sget-object v2, Lcom/helpshift/websockets/WebSocketState;->CLOSED:Lcom/helpshift/websockets/WebSocketState;

    if-eq v1, v2, :cond_0

    .line 450
    sget-object v1, Lcom/helpshift/websockets/StateManager$CloseInitiator;->CLIENT:Lcom/helpshift/websockets/StateManager$CloseInitiator;

    invoke-virtual {v0, v1}, Lcom/helpshift/websockets/StateManager;->changeToClosing(Lcom/helpshift/websockets/StateManager$CloseInitiator;)V

    const/4 v1, 0x1

    goto :goto_0

    :cond_0
    const/4 v1, 0x0

    .line 454
    :goto_0
    monitor-exit v0
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    if-eqz v1, :cond_1

    .line 458
    iget-object v0, p0, Lcom/helpshift/websockets/WritingThread;->mWebSocket:Lcom/helpshift/websockets/WebSocket;

    invoke-virtual {v0}, Lcom/helpshift/websockets/WebSocket;->getListenerManager()Lcom/helpshift/websockets/ListenerManager;

    move-result-object v0

    sget-object v1, Lcom/helpshift/websockets/WebSocketState;->CLOSING:Lcom/helpshift/websockets/WebSocketState;

    invoke-virtual {v0, v1}, Lcom/helpshift/websockets/ListenerManager;->callOnStateChanged(Lcom/helpshift/websockets/WebSocketState;)V

    :cond_1
    return-void

    :catchall_0
    move-exception v1

    .line 454
    :try_start_1
    monitor-exit v0
    :try_end_1
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    throw v1
.end method

.method private doFlush()V
    .locals 5
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Lcom/helpshift/websockets/WebSocketException;
        }
    .end annotation

    .line 361
    :try_start_0
    invoke-direct {p0}, Lcom/helpshift/websockets/WritingThread;->flush()V

    .line 363
    monitor-enter p0
    :try_end_0
    .catch Ljava/io/IOException; {:try_start_0 .. :try_end_0} :catch_0

    const/4 v0, 0x0

    .line 364
    :try_start_1
    iput-boolean v0, p0, Lcom/helpshift/websockets/WritingThread;->mFlushNeeded:Z

    .line 365
    monitor-exit p0

    return-void

    :catchall_0
    move-exception v0

    monitor-exit p0
    :try_end_1
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    :try_start_2
    throw v0
    :try_end_2
    .catch Ljava/io/IOException; {:try_start_2 .. :try_end_2} :catch_0

    :catch_0
    move-exception v0

    .line 369
    new-instance v1, Lcom/helpshift/websockets/WebSocketException;

    sget-object v2, Lcom/helpshift/websockets/WebSocketError;->FLUSH_ERROR:Lcom/helpshift/websockets/WebSocketError;

    new-instance v3, Ljava/lang/StringBuilder;

    invoke-direct {v3}, Ljava/lang/StringBuilder;-><init>()V

    const-string v4, "Flushing frames to the server failed: "

    invoke-virtual {v3, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    .line 371
    invoke-virtual {v0}, Ljava/io/IOException;->getMessage()Ljava/lang/String;

    move-result-object v4

    invoke-virtual {v3, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v3}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v3

    invoke-direct {v1, v2, v3, v0}, Lcom/helpshift/websockets/WebSocketException;-><init>(Lcom/helpshift/websockets/WebSocketError;Ljava/lang/String;Ljava/lang/Throwable;)V

    .line 374
    iget-object v0, p0, Lcom/helpshift/websockets/WritingThread;->mWebSocket:Lcom/helpshift/websockets/WebSocket;

    invoke-virtual {v0}, Lcom/helpshift/websockets/WebSocket;->getListenerManager()Lcom/helpshift/websockets/ListenerManager;

    move-result-object v0

    .line 375
    invoke-virtual {v0, v1}, Lcom/helpshift/websockets/ListenerManager;->callOnError(Lcom/helpshift/websockets/WebSocketException;)V

    const/4 v2, 0x0

    .line 376
    invoke-virtual {v0, v1, v2}, Lcom/helpshift/websockets/ListenerManager;->callOnSendError(Lcom/helpshift/websockets/WebSocketException;Lcom/helpshift/websockets/WebSocketFrame;)V

    .line 378
    throw v1
.end method

.method private flush()V
    .locals 1
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    .line 234
    iget-object v0, p0, Lcom/helpshift/websockets/WritingThread;->mWebSocket:Lcom/helpshift/websockets/WebSocket;

    invoke-virtual {v0}, Lcom/helpshift/websockets/WebSocket;->getOutput()Lcom/helpshift/websockets/WebSocketOutputStream;

    move-result-object v0

    invoke-virtual {v0}, Lcom/helpshift/websockets/WebSocketOutputStream;->flush()V

    return-void
.end method

.method private flushIfLongInterval(J)J
    .locals 7
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Lcom/helpshift/websockets/WebSocketException;
        }
    .end annotation

    .line 341
    invoke-static {}, Ljava/lang/System;->currentTimeMillis()J

    move-result-wide v0

    sub-long v2, v0, p1

    const-wide/16 v4, 0x3e8

    cmp-long v6, v4, v2

    if-gez v6, :cond_0

    .line 346
    invoke-direct {p0}, Lcom/helpshift/websockets/WritingThread;->doFlush()V

    return-wide v0

    :cond_0
    return-wide p1
.end method

.method private flushIgnoreError()V
    .locals 0

    .line 226
    :try_start_0
    invoke-direct {p0}, Lcom/helpshift/websockets/WritingThread;->flush()V
    :try_end_0
    .catch Ljava/io/IOException; {:try_start_0 .. :try_end_0} :catch_0

    :catch_0
    return-void
.end method

.method private isFlushNeeded(Z)Z
    .locals 0

    if-nez p1, :cond_1

    .line 335
    iget-object p1, p0, Lcom/helpshift/websockets/WritingThread;->mWebSocket:Lcom/helpshift/websockets/WebSocket;

    invoke-virtual {p1}, Lcom/helpshift/websockets/WebSocket;->isAutoFlush()Z

    move-result p1

    if-nez p1, :cond_1

    iget-boolean p1, p0, Lcom/helpshift/websockets/WritingThread;->mFlushNeeded:Z

    if-nez p1, :cond_1

    iget-object p1, p0, Lcom/helpshift/websockets/WritingThread;->mCloseFrame:Lcom/helpshift/websockets/WebSocketFrame;

    if-eqz p1, :cond_0

    goto :goto_0

    :cond_0
    const/4 p1, 0x0

    goto :goto_1

    :cond_1
    :goto_0
    const/4 p1, 0x1

    :goto_1
    return p1
.end method

.method private static isHighPriorityFrame(Lcom/helpshift/websockets/WebSocketFrame;)Z
    .locals 1

    .line 56
    invoke-virtual {p0}, Lcom/helpshift/websockets/WebSocketFrame;->isPingFrame()Z

    move-result v0

    if-nez v0, :cond_1

    invoke-virtual {p0}, Lcom/helpshift/websockets/WebSocketFrame;->isPongFrame()Z

    move-result p0

    if-eqz p0, :cond_0

    goto :goto_0

    :cond_0
    const/4 p0, 0x0

    goto :goto_1

    :cond_1
    :goto_0
    const/4 p0, 0x1

    :goto_1
    return p0
.end method

.method private main()V
    .locals 3

    .line 87
    iget-object v0, p0, Lcom/helpshift/websockets/WritingThread;->mWebSocket:Lcom/helpshift/websockets/WebSocket;

    invoke-virtual {v0}, Lcom/helpshift/websockets/WebSocket;->onWritingThreadStarted()V

    .line 91
    :goto_0
    invoke-direct {p0}, Lcom/helpshift/websockets/WritingThread;->waitForFrames()I

    move-result v0

    const/4 v1, 0x1

    if-ne v0, v1, :cond_0

    goto :goto_1

    :cond_0
    const/4 v2, 0x3

    if-ne v0, v2, :cond_1

    .line 97
    invoke-direct {p0}, Lcom/helpshift/websockets/WritingThread;->flushIgnoreError()V

    goto :goto_0

    :cond_1
    const/4 v2, 0x2

    if-ne v0, v2, :cond_2

    goto :goto_0

    :cond_2
    const/4 v0, 0x0

    .line 106
    :try_start_0
    invoke-direct {p0, v0}, Lcom/helpshift/websockets/WritingThread;->sendFrames(Z)V
    :try_end_0
    .catch Lcom/helpshift/websockets/WebSocketException; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    .line 116
    :catch_0
    :goto_1
    :try_start_1
    invoke-direct {p0, v1}, Lcom/helpshift/websockets/WritingThread;->sendFrames(Z)V
    :try_end_1
    .catch Lcom/helpshift/websockets/WebSocketException; {:try_start_1 .. :try_end_1} :catch_1

    :catch_1
    return-void
.end method

.method private notifyFinished()V
    .locals 2

    .line 464
    iget-object v0, p0, Lcom/helpshift/websockets/WritingThread;->mWebSocket:Lcom/helpshift/websockets/WebSocket;

    iget-object v1, p0, Lcom/helpshift/websockets/WritingThread;->mCloseFrame:Lcom/helpshift/websockets/WebSocketFrame;

    invoke-virtual {v0, v1}, Lcom/helpshift/websockets/WebSocket;->onWritingThreadFinished(Lcom/helpshift/websockets/WebSocketFrame;)V

    return-void
.end method

.method private sendFrame(Lcom/helpshift/websockets/WebSocketFrame;)V
    .locals 5
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Lcom/helpshift/websockets/WebSocketException;
        }
    .end annotation

    .line 385
    iget-object v0, p0, Lcom/helpshift/websockets/WritingThread;->mPMCE:Lcom/helpshift/websockets/PerMessageCompressionExtension;

    invoke-static {p1, v0}, Lcom/helpshift/websockets/WebSocketFrame;->compressFrame(Lcom/helpshift/websockets/WebSocketFrame;Lcom/helpshift/websockets/PerMessageCompressionExtension;)Lcom/helpshift/websockets/WebSocketFrame;

    move-result-object p1

    .line 388
    iget-object v0, p0, Lcom/helpshift/websockets/WritingThread;->mWebSocket:Lcom/helpshift/websockets/WebSocket;

    invoke-virtual {v0}, Lcom/helpshift/websockets/WebSocket;->getListenerManager()Lcom/helpshift/websockets/ListenerManager;

    move-result-object v0

    invoke-virtual {v0, p1}, Lcom/helpshift/websockets/ListenerManager;->callOnSendingFrame(Lcom/helpshift/websockets/WebSocketFrame;)V

    .line 393
    iget-object v0, p0, Lcom/helpshift/websockets/WritingThread;->mCloseFrame:Lcom/helpshift/websockets/WebSocketFrame;

    if-eqz v0, :cond_0

    const/4 v0, 0x1

    goto :goto_0

    .line 398
    :cond_0
    invoke-virtual {p1}, Lcom/helpshift/websockets/WebSocketFrame;->isCloseFrame()Z

    move-result v0

    if-eqz v0, :cond_1

    .line 399
    iput-object p1, p0, Lcom/helpshift/websockets/WritingThread;->mCloseFrame:Lcom/helpshift/websockets/WebSocketFrame;

    :cond_1
    const/4 v0, 0x0

    :goto_0
    if-eqz v0, :cond_2

    .line 404
    iget-object v0, p0, Lcom/helpshift/websockets/WritingThread;->mWebSocket:Lcom/helpshift/websockets/WebSocket;

    invoke-virtual {v0}, Lcom/helpshift/websockets/WebSocket;->getListenerManager()Lcom/helpshift/websockets/ListenerManager;

    move-result-object v0

    invoke-virtual {v0, p1}, Lcom/helpshift/websockets/ListenerManager;->callOnFrameUnsent(Lcom/helpshift/websockets/WebSocketFrame;)V

    return-void

    .line 409
    :cond_2
    invoke-virtual {p1}, Lcom/helpshift/websockets/WebSocketFrame;->isCloseFrame()Z

    move-result v0

    if-eqz v0, :cond_3

    .line 412
    invoke-direct {p0}, Lcom/helpshift/websockets/WritingThread;->changeToClosing()V

    .line 417
    :cond_3
    :try_start_0
    iget-object v0, p0, Lcom/helpshift/websockets/WritingThread;->mWebSocket:Lcom/helpshift/websockets/WebSocket;

    invoke-virtual {v0}, Lcom/helpshift/websockets/WebSocket;->getOutput()Lcom/helpshift/websockets/WebSocketOutputStream;

    move-result-object v0

    invoke-virtual {v0, p1}, Lcom/helpshift/websockets/WebSocketOutputStream;->write(Lcom/helpshift/websockets/WebSocketFrame;)V
    :try_end_0
    .catch Ljava/io/IOException; {:try_start_0 .. :try_end_0} :catch_0

    .line 434
    iget-object v0, p0, Lcom/helpshift/websockets/WritingThread;->mWebSocket:Lcom/helpshift/websockets/WebSocket;

    invoke-virtual {v0}, Lcom/helpshift/websockets/WebSocket;->getListenerManager()Lcom/helpshift/websockets/ListenerManager;

    move-result-object v0

    invoke-virtual {v0, p1}, Lcom/helpshift/websockets/ListenerManager;->callOnFrameSent(Lcom/helpshift/websockets/WebSocketFrame;)V

    return-void

    :catch_0
    move-exception v0

    .line 421
    new-instance v1, Lcom/helpshift/websockets/WebSocketException;

    sget-object v2, Lcom/helpshift/websockets/WebSocketError;->IO_ERROR_IN_WRITING:Lcom/helpshift/websockets/WebSocketError;

    new-instance v3, Ljava/lang/StringBuilder;

    invoke-direct {v3}, Ljava/lang/StringBuilder;-><init>()V

    const-string v4, "An I/O error occurred when a frame was tried to be sent: "

    invoke-virtual {v3, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    .line 423
    invoke-virtual {v0}, Ljava/io/IOException;->getMessage()Ljava/lang/String;

    move-result-object v4

    invoke-virtual {v3, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v3}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v3

    invoke-direct {v1, v2, v3, v0}, Lcom/helpshift/websockets/WebSocketException;-><init>(Lcom/helpshift/websockets/WebSocketError;Ljava/lang/String;Ljava/lang/Throwable;)V

    .line 426
    iget-object v0, p0, Lcom/helpshift/websockets/WritingThread;->mWebSocket:Lcom/helpshift/websockets/WebSocket;

    invoke-virtual {v0}, Lcom/helpshift/websockets/WebSocket;->getListenerManager()Lcom/helpshift/websockets/ListenerManager;

    move-result-object v0

    .line 427
    invoke-virtual {v0, v1}, Lcom/helpshift/websockets/ListenerManager;->callOnError(Lcom/helpshift/websockets/WebSocketException;)V

    .line 428
    invoke-virtual {v0, v1, p1}, Lcom/helpshift/websockets/ListenerManager;->callOnSendError(Lcom/helpshift/websockets/WebSocketException;Lcom/helpshift/websockets/WebSocketFrame;)V

    .line 430
    throw v1
.end method

.method private sendFrames(Z)V
    .locals 4
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Lcom/helpshift/websockets/WebSocketException;
        }
    .end annotation

    .line 288
    invoke-static {}, Ljava/lang/System;->currentTimeMillis()J

    move-result-wide v0

    .line 293
    :goto_0
    monitor-enter p0

    .line 295
    :try_start_0
    iget-object v2, p0, Lcom/helpshift/websockets/WritingThread;->mFrames:Ljava/util/LinkedList;

    invoke-virtual {v2}, Ljava/util/LinkedList;->poll()Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Lcom/helpshift/websockets/WebSocketFrame;

    .line 298
    invoke-virtual {p0}, Ljava/lang/Object;->notifyAll()V

    if-nez v2, :cond_1

    .line 303
    monitor-exit p0
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    .line 328
    invoke-direct {p0, p1}, Lcom/helpshift/websockets/WritingThread;->isFlushNeeded(Z)Z

    move-result p1

    if-eqz p1, :cond_0

    .line 329
    invoke-direct {p0}, Lcom/helpshift/websockets/WritingThread;->doFlush()V

    :cond_0
    return-void

    .line 305
    :cond_1
    :try_start_1
    monitor-exit p0
    :try_end_1
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    .line 308
    invoke-direct {p0, v2}, Lcom/helpshift/websockets/WritingThread;->sendFrame(Lcom/helpshift/websockets/WebSocketFrame;)V

    .line 311
    invoke-virtual {v2}, Lcom/helpshift/websockets/WebSocketFrame;->isPingFrame()Z

    move-result v3

    if-nez v3, :cond_4

    invoke-virtual {v2}, Lcom/helpshift/websockets/WebSocketFrame;->isPongFrame()Z

    move-result v2

    if-eqz v2, :cond_2

    goto :goto_1

    .line 319
    :cond_2
    invoke-direct {p0, p1}, Lcom/helpshift/websockets/WritingThread;->isFlushNeeded(Z)Z

    move-result v2

    if-nez v2, :cond_3

    goto :goto_0

    .line 325
    :cond_3
    invoke-direct {p0, v0, v1}, Lcom/helpshift/websockets/WritingThread;->flushIfLongInterval(J)J

    move-result-wide v0

    goto :goto_0

    .line 313
    :cond_4
    :goto_1
    invoke-direct {p0}, Lcom/helpshift/websockets/WritingThread;->doFlush()V

    .line 314
    invoke-static {}, Ljava/lang/System;->currentTimeMillis()J

    move-result-wide v0

    goto :goto_0

    :catchall_0
    move-exception p1

    .line 305
    :try_start_2
    monitor-exit p0
    :try_end_2
    .catchall {:try_start_2 .. :try_end_2} :catchall_0

    throw p1
.end method

.method private waitForFrames()I
    .locals 4

    .line 239
    monitor-enter p0

    .line 241
    :try_start_0
    iget-boolean v0, p0, Lcom/helpshift/websockets/WritingThread;->mStopRequested:Z

    const/4 v1, 0x1

    if-eqz v0, :cond_0

    .line 242
    monitor-exit p0

    return v1

    .line 246
    :cond_0
    iget-object v0, p0, Lcom/helpshift/websockets/WritingThread;->mCloseFrame:Lcom/helpshift/websockets/WebSocketFrame;

    if-eqz v0, :cond_1

    .line 247
    monitor-exit p0

    return v1

    .line 251
    :cond_1
    iget-object v0, p0, Lcom/helpshift/websockets/WritingThread;->mFrames:Ljava/util/LinkedList;

    invoke-virtual {v0}, Ljava/util/LinkedList;->size()I

    move-result v0

    const/4 v2, 0x3

    const/4 v3, 0x0

    if-nez v0, :cond_3

    .line 253
    iget-boolean v0, p0, Lcom/helpshift/websockets/WritingThread;->mFlushNeeded:Z

    if-eqz v0, :cond_2

    .line 254
    iput-boolean v3, p0, Lcom/helpshift/websockets/WritingThread;->mFlushNeeded:Z

    .line 255
    monitor-exit p0
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    return v2

    .line 261
    :cond_2
    :try_start_1
    invoke-virtual {p0}, Ljava/lang/Object;->wait()V
    :try_end_1
    .catch Ljava/lang/InterruptedException; {:try_start_1 .. :try_end_1} :catch_0
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    .line 267
    :catch_0
    :cond_3
    :try_start_2
    iget-boolean v0, p0, Lcom/helpshift/websockets/WritingThread;->mStopRequested:Z

    if-eqz v0, :cond_4

    .line 268
    monitor-exit p0

    return v1

    .line 271
    :cond_4
    iget-object v0, p0, Lcom/helpshift/websockets/WritingThread;->mFrames:Ljava/util/LinkedList;

    invoke-virtual {v0}, Ljava/util/LinkedList;->size()I

    move-result v0

    if-nez v0, :cond_6

    .line 272
    iget-boolean v0, p0, Lcom/helpshift/websockets/WritingThread;->mFlushNeeded:Z

    if-eqz v0, :cond_5

    .line 273
    iput-boolean v3, p0, Lcom/helpshift/websockets/WritingThread;->mFlushNeeded:Z

    .line 274
    monitor-exit p0

    return v2

    :cond_5
    const/4 v0, 0x2

    .line 278
    monitor-exit p0

    return v0

    .line 280
    :cond_6
    monitor-exit p0

    return v3

    :catchall_0
    move-exception v0

    monitor-exit p0
    :try_end_2
    .catchall {:try_start_2 .. :try_end_2} :catchall_0

    throw v0
.end method


# virtual methods
.method public queueFlush()V
    .locals 1

    .line 215
    monitor-enter p0

    const/4 v0, 0x1

    .line 216
    :try_start_0
    iput-boolean v0, p0, Lcom/helpshift/websockets/WritingThread;->mFlushNeeded:Z

    .line 219
    invoke-virtual {p0}, Ljava/lang/Object;->notifyAll()V

    .line 220
    monitor-exit p0

    return-void

    :catchall_0
    move-exception v0

    monitor-exit p0
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    throw v0
.end method

.method public queueFrame(Lcom/helpshift/websockets/WebSocketFrame;)Z
    .locals 2

    .line 134
    monitor-enter p0

    .line 137
    :catch_0
    :goto_0
    :try_start_0
    iget-boolean v0, p0, Lcom/helpshift/websockets/WritingThread;->mStopped:Z

    if-eqz v0, :cond_0

    const/4 p1, 0x0

    .line 139
    monitor-exit p0

    return p1

    .line 144
    :cond_0
    iget-boolean v0, p0, Lcom/helpshift/websockets/WritingThread;->mStopRequested:Z

    if-nez v0, :cond_5

    iget-object v0, p0, Lcom/helpshift/websockets/WritingThread;->mCloseFrame:Lcom/helpshift/websockets/WebSocketFrame;

    if-eqz v0, :cond_1

    goto :goto_1

    .line 150
    :cond_1
    invoke-virtual {p1}, Lcom/helpshift/websockets/WebSocketFrame;->isControlFrame()Z

    move-result v0

    if-eqz v0, :cond_2

    goto :goto_1

    .line 156
    :cond_2
    iget-object v0, p0, Lcom/helpshift/websockets/WritingThread;->mWebSocket:Lcom/helpshift/websockets/WebSocket;

    invoke-virtual {v0}, Lcom/helpshift/websockets/WebSocket;->getFrameQueueSize()I

    move-result v0

    if-nez v0, :cond_3

    goto :goto_1

    .line 165
    :cond_3
    iget-object v1, p0, Lcom/helpshift/websockets/WritingThread;->mFrames:Ljava/util/LinkedList;

    invoke-virtual {v1}, Ljava/util/LinkedList;->size()I

    move-result v1
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    if-ge v1, v0, :cond_4

    goto :goto_1

    .line 172
    :cond_4
    :try_start_1
    invoke-virtual {p0}, Ljava/lang/Object;->wait()V
    :try_end_1
    .catch Ljava/lang/InterruptedException; {:try_start_1 .. :try_end_1} :catch_0
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    goto :goto_0

    .line 179
    :cond_5
    :goto_1
    :try_start_2
    invoke-static {p1}, Lcom/helpshift/websockets/WritingThread;->isHighPriorityFrame(Lcom/helpshift/websockets/WebSocketFrame;)Z

    move-result v0

    if-eqz v0, :cond_6

    .line 181
    invoke-direct {p0, p1}, Lcom/helpshift/websockets/WritingThread;->addHighPriorityFrame(Lcom/helpshift/websockets/WebSocketFrame;)V

    goto :goto_2

    .line 185
    :cond_6
    iget-object v0, p0, Lcom/helpshift/websockets/WritingThread;->mFrames:Ljava/util/LinkedList;

    invoke-virtual {v0, p1}, Ljava/util/LinkedList;->addLast(Ljava/lang/Object;)V

    .line 189
    :goto_2
    invoke-virtual {p0}, Ljava/lang/Object;->notifyAll()V

    .line 190
    monitor-exit p0

    const/4 p1, 0x1

    return p1

    :catchall_0
    move-exception p1

    monitor-exit p0
    :try_end_2
    .catchall {:try_start_2 .. :try_end_2} :catchall_0

    throw p1
.end method

.method public requestStop()V
    .locals 1

    .line 124
    monitor-enter p0

    const/4 v0, 0x1

    .line 126
    :try_start_0
    iput-boolean v0, p0, Lcom/helpshift/websockets/WritingThread;->mStopRequested:Z

    .line 129
    invoke-virtual {p0}, Ljava/lang/Object;->notifyAll()V

    .line 130
    monitor-exit p0

    return-void

    :catchall_0
    move-exception v0

    monitor-exit p0
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    throw v0
.end method

.method public runMain()V
    .locals 5

    .line 62
    :try_start_0
    invoke-direct {p0}, Lcom/helpshift/websockets/WritingThread;->main()V
    :try_end_0
    .catch Ljava/lang/Throwable; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception v0

    .line 66
    new-instance v1, Lcom/helpshift/websockets/WebSocketException;

    sget-object v2, Lcom/helpshift/websockets/WebSocketError;->UNEXPECTED_ERROR_IN_WRITING_THREAD:Lcom/helpshift/websockets/WebSocketError;

    new-instance v3, Ljava/lang/StringBuilder;

    invoke-direct {v3}, Ljava/lang/StringBuilder;-><init>()V

    const-string v4, "An uncaught throwable was detected in the writing thread: "

    invoke-virtual {v3, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    .line 68
    invoke-virtual {v0}, Ljava/lang/Throwable;->getMessage()Ljava/lang/String;

    move-result-object v4

    invoke-virtual {v3, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v3}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v3

    invoke-direct {v1, v2, v3, v0}, Lcom/helpshift/websockets/WebSocketException;-><init>(Lcom/helpshift/websockets/WebSocketError;Ljava/lang/String;Ljava/lang/Throwable;)V

    .line 71
    iget-object v0, p0, Lcom/helpshift/websockets/WritingThread;->mWebSocket:Lcom/helpshift/websockets/WebSocket;

    invoke-virtual {v0}, Lcom/helpshift/websockets/WebSocket;->getListenerManager()Lcom/helpshift/websockets/ListenerManager;

    move-result-object v0

    .line 72
    invoke-virtual {v0, v1}, Lcom/helpshift/websockets/ListenerManager;->callOnError(Lcom/helpshift/websockets/WebSocketException;)V

    .line 73
    invoke-virtual {v0, v1}, Lcom/helpshift/websockets/ListenerManager;->callOnUnexpectedError(Lcom/helpshift/websockets/WebSocketException;)V

    .line 76
    :goto_0
    monitor-enter p0

    const/4 v0, 0x1

    .line 78
    :try_start_1
    iput-boolean v0, p0, Lcom/helpshift/websockets/WritingThread;->mStopped:Z

    .line 79
    invoke-virtual {p0}, Ljava/lang/Object;->notifyAll()V

    .line 80
    monitor-exit p0
    :try_end_1
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    .line 83
    invoke-direct {p0}, Lcom/helpshift/websockets/WritingThread;->notifyFinished()V

    return-void

    :catchall_0
    move-exception v0

    .line 80
    :try_start_2
    monitor-exit p0
    :try_end_2
    .catchall {:try_start_2 .. :try_end_2} :catchall_0

    throw v0
.end method
