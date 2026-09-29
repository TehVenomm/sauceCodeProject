.class public Lcom/helpshift/websockets/WebSocket;
.super Ljava/lang/Object;
.source "WebSocket.java"


# static fields
.field private static final DEFAULT_CLOSE_DELAY:J = 0x2710L


# instance fields
.field private mAgreedExtensions:Ljava/util/List;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/List<",
            "Lcom/helpshift/websockets/WebSocketExtension;",
            ">;"
        }
    .end annotation
.end field

.field private mAgreedProtocol:Ljava/lang/String;

.field private mAutoFlush:Z

.field private mClientCloseFrame:Lcom/helpshift/websockets/WebSocketFrame;

.field private mExtended:Z

.field private mFrameQueueSize:I

.field private mHandshakeBuilder:Lcom/helpshift/websockets/HandshakeBuilder;

.field private mInput:Lcom/helpshift/websockets/WebSocketInputStream;

.field private final mListenerManager:Lcom/helpshift/websockets/ListenerManager;

.field private mMaxPayloadSize:I

.field private mMissingCloseFrameAllowed:Z

.field private mOnConnectedCalled:Z

.field private mOnConnectedCalledLock:Ljava/lang/Object;

.field private mOutput:Lcom/helpshift/websockets/WebSocketOutputStream;

.field private mPerMessageCompressionExtension:Lcom/helpshift/websockets/PerMessageCompressionExtension;

.field private final mPingSender:Lcom/helpshift/websockets/PingSender;

.field private final mPongSender:Lcom/helpshift/websockets/PongSender;

.field private mReadingThread:Lcom/helpshift/websockets/ReadingThread;

.field private mReadingThreadFinished:Z

.field private mReadingThreadStarted:Z

.field private mServerCloseFrame:Lcom/helpshift/websockets/WebSocketFrame;

.field private mServerHeaders:Ljava/util/Map;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Ljava/util/List<",
            "Ljava/lang/String;",
            ">;>;"
        }
    .end annotation
.end field

.field private final mSocketConnector:Lcom/helpshift/websockets/SocketConnector;

.field private final mStateManager:Lcom/helpshift/websockets/StateManager;

.field private final mThreadsLock:Ljava/lang/Object;

.field private final mWebSocketFactory:Lcom/helpshift/websockets/WebSocketFactory;

.field private mWritingThread:Lcom/helpshift/websockets/WritingThread;

.field private mWritingThreadFinished:Z

.field private mWritingThreadStarted:Z


# direct methods
.method constructor <init>(Lcom/helpshift/websockets/WebSocketFactory;ZLjava/lang/String;Ljava/lang/String;Ljava/lang/String;Lcom/helpshift/websockets/SocketConnector;)V
    .locals 1

    .line 1084
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 1058
    new-instance v0, Ljava/lang/Object;

    invoke-direct {v0}, Ljava/lang/Object;-><init>()V

    iput-object v0, p0, Lcom/helpshift/websockets/WebSocket;->mThreadsLock:Ljava/lang/Object;

    const/4 v0, 0x1

    .line 1068
    iput-boolean v0, p0, Lcom/helpshift/websockets/WebSocket;->mAutoFlush:Z

    .line 1069
    iput-boolean v0, p0, Lcom/helpshift/websockets/WebSocket;->mMissingCloseFrameAllowed:Z

    .line 1073
    new-instance v0, Ljava/lang/Object;

    invoke-direct {v0}, Ljava/lang/Object;-><init>()V

    iput-object v0, p0, Lcom/helpshift/websockets/WebSocket;->mOnConnectedCalledLock:Ljava/lang/Object;

    .line 1085
    iput-object p1, p0, Lcom/helpshift/websockets/WebSocket;->mWebSocketFactory:Lcom/helpshift/websockets/WebSocketFactory;

    .line 1086
    iput-object p6, p0, Lcom/helpshift/websockets/WebSocket;->mSocketConnector:Lcom/helpshift/websockets/SocketConnector;

    .line 1087
    new-instance p1, Lcom/helpshift/websockets/StateManager;

    invoke-direct {p1}, Lcom/helpshift/websockets/StateManager;-><init>()V

    iput-object p1, p0, Lcom/helpshift/websockets/WebSocket;->mStateManager:Lcom/helpshift/websockets/StateManager;

    .line 1088
    new-instance p1, Lcom/helpshift/websockets/HandshakeBuilder;

    invoke-direct {p1, p2, p3, p4, p5}, Lcom/helpshift/websockets/HandshakeBuilder;-><init>(ZLjava/lang/String;Ljava/lang/String;Ljava/lang/String;)V

    iput-object p1, p0, Lcom/helpshift/websockets/WebSocket;->mHandshakeBuilder:Lcom/helpshift/websockets/HandshakeBuilder;

    .line 1089
    new-instance p1, Lcom/helpshift/websockets/ListenerManager;

    invoke-direct {p1, p0}, Lcom/helpshift/websockets/ListenerManager;-><init>(Lcom/helpshift/websockets/WebSocket;)V

    iput-object p1, p0, Lcom/helpshift/websockets/WebSocket;->mListenerManager:Lcom/helpshift/websockets/ListenerManager;

    .line 1090
    new-instance p1, Lcom/helpshift/websockets/PingSender;

    new-instance p2, Lcom/helpshift/websockets/CounterPayloadGenerator;

    invoke-direct {p2}, Lcom/helpshift/websockets/CounterPayloadGenerator;-><init>()V

    invoke-direct {p1, p0, p2}, Lcom/helpshift/websockets/PingSender;-><init>(Lcom/helpshift/websockets/WebSocket;Lcom/helpshift/websockets/PayloadGenerator;)V

    iput-object p1, p0, Lcom/helpshift/websockets/WebSocket;->mPingSender:Lcom/helpshift/websockets/PingSender;

    .line 1091
    new-instance p1, Lcom/helpshift/websockets/PongSender;

    new-instance p2, Lcom/helpshift/websockets/CounterPayloadGenerator;

    invoke-direct {p2}, Lcom/helpshift/websockets/CounterPayloadGenerator;-><init>()V

    invoke-direct {p1, p0, p2}, Lcom/helpshift/websockets/PongSender;-><init>(Lcom/helpshift/websockets/WebSocket;Lcom/helpshift/websockets/PayloadGenerator;)V

    iput-object p1, p0, Lcom/helpshift/websockets/WebSocket;->mPongSender:Lcom/helpshift/websockets/PongSender;

    return-void
.end method

.method private callOnConnectedIfNotYet()V
    .locals 2

    .line 2826
    iget-object v0, p0, Lcom/helpshift/websockets/WebSocket;->mOnConnectedCalledLock:Ljava/lang/Object;

    monitor-enter v0

    .line 2828
    :try_start_0
    iget-boolean v1, p0, Lcom/helpshift/websockets/WebSocket;->mOnConnectedCalled:Z

    if-eqz v1, :cond_0

    .line 2830
    monitor-exit v0

    return-void

    :cond_0
    const/4 v1, 0x1

    .line 2833
    iput-boolean v1, p0, Lcom/helpshift/websockets/WebSocket;->mOnConnectedCalled:Z

    .line 2834
    monitor-exit v0
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    .line 2837
    iget-object v0, p0, Lcom/helpshift/websockets/WebSocket;->mListenerManager:Lcom/helpshift/websockets/ListenerManager;

    iget-object v1, p0, Lcom/helpshift/websockets/WebSocket;->mServerHeaders:Ljava/util/Map;

    invoke-virtual {v0, v1}, Lcom/helpshift/websockets/ListenerManager;->callOnConnected(Ljava/util/Map;)V

    return-void

    :catchall_0
    move-exception v1

    .line 2834
    :try_start_1
    monitor-exit v0
    :try_end_1
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    throw v1
.end method

.method private changeStateOnConnect()V
    .locals 4
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Lcom/helpshift/websockets/WebSocketException;
        }
    .end annotation

    .line 2549
    iget-object v0, p0, Lcom/helpshift/websockets/WebSocket;->mStateManager:Lcom/helpshift/websockets/StateManager;

    monitor-enter v0

    .line 2551
    :try_start_0
    iget-object v1, p0, Lcom/helpshift/websockets/WebSocket;->mStateManager:Lcom/helpshift/websockets/StateManager;

    invoke-virtual {v1}, Lcom/helpshift/websockets/StateManager;->getState()Lcom/helpshift/websockets/WebSocketState;

    move-result-object v1

    sget-object v2, Lcom/helpshift/websockets/WebSocketState;->CREATED:Lcom/helpshift/websockets/WebSocketState;

    if-ne v1, v2, :cond_0

    .line 2558
    iget-object v1, p0, Lcom/helpshift/websockets/WebSocket;->mStateManager:Lcom/helpshift/websockets/StateManager;

    sget-object v2, Lcom/helpshift/websockets/WebSocketState;->CONNECTING:Lcom/helpshift/websockets/WebSocketState;

    invoke-virtual {v1, v2}, Lcom/helpshift/websockets/StateManager;->setState(Lcom/helpshift/websockets/WebSocketState;)V

    .line 2559
    monitor-exit v0
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    .line 2562
    iget-object v0, p0, Lcom/helpshift/websockets/WebSocket;->mListenerManager:Lcom/helpshift/websockets/ListenerManager;

    sget-object v1, Lcom/helpshift/websockets/WebSocketState;->CONNECTING:Lcom/helpshift/websockets/WebSocketState;

    invoke-virtual {v0, v1}, Lcom/helpshift/websockets/ListenerManager;->callOnStateChanged(Lcom/helpshift/websockets/WebSocketState;)V

    return-void

    .line 2552
    :cond_0
    :try_start_1
    new-instance v1, Lcom/helpshift/websockets/WebSocketException;

    sget-object v2, Lcom/helpshift/websockets/WebSocketError;->NOT_IN_CREATED_STATE:Lcom/helpshift/websockets/WebSocketError;

    const-string v3, "The current state of the WebSocket is not CREATED."

    invoke-direct {v1, v2, v3}, Lcom/helpshift/websockets/WebSocketException;-><init>(Lcom/helpshift/websockets/WebSocketError;Ljava/lang/String;)V

    throw v1

    :catchall_0
    move-exception v1

    .line 2559
    monitor-exit v0
    :try_end_1
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    throw v1
.end method

.method private findAgreedPerMessageCompressionExtension()Lcom/helpshift/websockets/PerMessageCompressionExtension;
    .locals 4

    .line 2948
    iget-object v0, p0, Lcom/helpshift/websockets/WebSocket;->mAgreedExtensions:Ljava/util/List;

    const/4 v1, 0x0

    if-nez v0, :cond_0

    return-object v1

    .line 2952
    :cond_0
    iget-object v0, p0, Lcom/helpshift/websockets/WebSocket;->mAgreedExtensions:Ljava/util/List;

    invoke-interface {v0}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v0

    :cond_1
    invoke-interface {v0}, Ljava/util/Iterator;->hasNext()Z

    move-result v2

    if-eqz v2, :cond_2

    invoke-interface {v0}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Lcom/helpshift/websockets/WebSocketExtension;

    .line 2953
    instance-of v3, v2, Lcom/helpshift/websockets/PerMessageCompressionExtension;

    if-eqz v3, :cond_1

    .line 2954
    check-cast v2, Lcom/helpshift/websockets/PerMessageCompressionExtension;

    return-object v2

    :cond_2
    return-object v1
.end method

.method private finishAsynchronously()V
    .locals 1

    .line 2935
    new-instance v0, Lcom/helpshift/websockets/FinishThread;

    invoke-direct {v0, p0}, Lcom/helpshift/websockets/FinishThread;-><init>(Lcom/helpshift/websockets/WebSocket;)V

    .line 2938
    invoke-virtual {v0}, Lcom/helpshift/websockets/WebSocketThread;->callOnThreadCreated()V

    .line 2940
    invoke-virtual {v0}, Lcom/helpshift/websockets/WebSocketThread;->start()V

    return-void
.end method

.method private isInState(Lcom/helpshift/websockets/WebSocketState;)Z
    .locals 2

    .line 1150
    iget-object v0, p0, Lcom/helpshift/websockets/WebSocket;->mStateManager:Lcom/helpshift/websockets/StateManager;

    monitor-enter v0

    .line 1151
    :try_start_0
    iget-object v1, p0, Lcom/helpshift/websockets/WebSocket;->mStateManager:Lcom/helpshift/websockets/StateManager;

    invoke-virtual {v1}, Lcom/helpshift/websockets/StateManager;->getState()Lcom/helpshift/websockets/WebSocketState;

    move-result-object v1

    if-ne v1, p1, :cond_0

    const/4 p1, 0x1

    goto :goto_0

    :cond_0
    const/4 p1, 0x0

    :goto_0
    monitor-exit v0

    return p1

    :catchall_0
    move-exception p1

    .line 1152
    monitor-exit v0
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    throw p1
.end method

.method private onThreadsFinished()V
    .locals 0

    .line 2900
    invoke-virtual {p0}, Lcom/helpshift/websockets/WebSocket;->finish()V

    return-void
.end method

.method private onThreadsStarted()V
    .locals 1

    .line 2849
    iget-object v0, p0, Lcom/helpshift/websockets/WebSocket;->mPingSender:Lcom/helpshift/websockets/PingSender;

    invoke-virtual {v0}, Lcom/helpshift/websockets/PingSender;->start()V

    .line 2852
    iget-object v0, p0, Lcom/helpshift/websockets/WebSocket;->mPongSender:Lcom/helpshift/websockets/PongSender;

    invoke-virtual {v0}, Lcom/helpshift/websockets/PongSender;->start()V

    return-void
.end method

.method private openInputStream(Ljava/net/Socket;)Lcom/helpshift/websockets/WebSocketInputStream;
    .locals 4
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Lcom/helpshift/websockets/WebSocketException;
        }
    .end annotation

    .line 2612
    :try_start_0
    new-instance v0, Lcom/helpshift/websockets/WebSocketInputStream;

    new-instance v1, Ljava/io/BufferedInputStream;

    .line 2613
    invoke-virtual {p1}, Ljava/net/Socket;->getInputStream()Ljava/io/InputStream;

    move-result-object p1

    invoke-direct {v1, p1}, Ljava/io/BufferedInputStream;-><init>(Ljava/io/InputStream;)V

    invoke-direct {v0, v1}, Lcom/helpshift/websockets/WebSocketInputStream;-><init>(Ljava/io/InputStream;)V
    :try_end_0
    .catch Ljava/io/IOException; {:try_start_0 .. :try_end_0} :catch_0

    return-object v0

    :catch_0
    move-exception p1

    .line 2617
    new-instance v0, Lcom/helpshift/websockets/WebSocketException;

    sget-object v1, Lcom/helpshift/websockets/WebSocketError;->SOCKET_INPUT_STREAM_FAILURE:Lcom/helpshift/websockets/WebSocketError;

    new-instance v2, Ljava/lang/StringBuilder;

    invoke-direct {v2}, Ljava/lang/StringBuilder;-><init>()V

    const-string v3, "Failed to get the input stream of the raw socket: "

    invoke-virtual {v2, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    .line 2619
    invoke-virtual {p1}, Ljava/io/IOException;->getMessage()Ljava/lang/String;

    move-result-object v3

    invoke-virtual {v2, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v2}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v2

    invoke-direct {v0, v1, v2, p1}, Lcom/helpshift/websockets/WebSocketException;-><init>(Lcom/helpshift/websockets/WebSocketError;Ljava/lang/String;Ljava/lang/Throwable;)V

    throw v0
.end method

.method private openOutputStream(Ljava/net/Socket;)Lcom/helpshift/websockets/WebSocketOutputStream;
    .locals 4
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Lcom/helpshift/websockets/WebSocketException;
        }
    .end annotation

    .line 2631
    :try_start_0
    new-instance v0, Lcom/helpshift/websockets/WebSocketOutputStream;

    new-instance v1, Ljava/io/BufferedOutputStream;

    .line 2632
    invoke-virtual {p1}, Ljava/net/Socket;->getOutputStream()Ljava/io/OutputStream;

    move-result-object p1

    invoke-direct {v1, p1}, Ljava/io/BufferedOutputStream;-><init>(Ljava/io/OutputStream;)V

    invoke-direct {v0, v1}, Lcom/helpshift/websockets/WebSocketOutputStream;-><init>(Ljava/io/OutputStream;)V
    :try_end_0
    .catch Ljava/io/IOException; {:try_start_0 .. :try_end_0} :catch_0

    return-object v0

    :catch_0
    move-exception p1

    .line 2636
    new-instance v0, Lcom/helpshift/websockets/WebSocketException;

    sget-object v1, Lcom/helpshift/websockets/WebSocketError;->SOCKET_OUTPUT_STREAM_FAILURE:Lcom/helpshift/websockets/WebSocketError;

    new-instance v2, Ljava/lang/StringBuilder;

    invoke-direct {v2}, Ljava/lang/StringBuilder;-><init>()V

    const-string v3, "Failed to get the output stream from the raw socket: "

    invoke-virtual {v2, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    .line 2638
    invoke-virtual {p1}, Ljava/io/IOException;->getMessage()Ljava/lang/String;

    move-result-object v3

    invoke-virtual {v2, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v2}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v2

    invoke-direct {v0, v1, v2, p1}, Lcom/helpshift/websockets/WebSocketException;-><init>(Lcom/helpshift/websockets/WebSocketError;Ljava/lang/String;Ljava/lang/Throwable;)V

    throw v0
.end method

.method private readHandshake(Lcom/helpshift/websockets/WebSocketInputStream;Ljava/lang/String;)Ljava/util/Map;
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Lcom/helpshift/websockets/WebSocketInputStream;",
            "Ljava/lang/String;",
            ")",
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Ljava/util/List<",
            "Ljava/lang/String;",
            ">;>;"
        }
    .end annotation

    .annotation system Ldalvik/annotation/Throws;
        value = {
            Lcom/helpshift/websockets/WebSocketException;
        }
    .end annotation

    .line 2672
    new-instance v0, Lcom/helpshift/websockets/HandshakeReader;

    invoke-direct {v0, p0}, Lcom/helpshift/websockets/HandshakeReader;-><init>(Lcom/helpshift/websockets/WebSocket;)V

    invoke-virtual {v0, p1, p2}, Lcom/helpshift/websockets/HandshakeReader;->readHandshake(Lcom/helpshift/websockets/WebSocketInputStream;Ljava/lang/String;)Ljava/util/Map;

    move-result-object p1

    return-object p1
.end method

.method private shakeHands()Ljava/util/Map;
    .locals 3
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()",
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Ljava/util/List<",
            "Ljava/lang/String;",
            ">;>;"
        }
    .end annotation

    .annotation system Ldalvik/annotation/Throws;
        value = {
            Lcom/helpshift/websockets/WebSocketException;
        }
    .end annotation

    .line 2570
    iget-object v0, p0, Lcom/helpshift/websockets/WebSocket;->mSocketConnector:Lcom/helpshift/websockets/SocketConnector;

    invoke-virtual {v0}, Lcom/helpshift/websockets/SocketConnector;->getSocket()Ljava/net/Socket;

    move-result-object v0

    .line 2573
    invoke-direct {p0, v0}, Lcom/helpshift/websockets/WebSocket;->openInputStream(Ljava/net/Socket;)Lcom/helpshift/websockets/WebSocketInputStream;

    move-result-object v1

    .line 2576
    invoke-direct {p0, v0}, Lcom/helpshift/websockets/WebSocket;->openOutputStream(Ljava/net/Socket;)Lcom/helpshift/websockets/WebSocketOutputStream;

    move-result-object v0

    const/16 v2, 0x10

    .line 2581
    new-array v2, v2, [B

    .line 2584
    invoke-static {v2}, Lcom/helpshift/websockets/Misc;->nextBytes([B)[B

    .line 2587
    invoke-static {v2}, Lcom/helpshift/websockets/Base64;->encode([B)Ljava/lang/String;

    move-result-object v2

    .line 2590
    invoke-direct {p0, v0, v2}, Lcom/helpshift/websockets/WebSocket;->writeHandshake(Lcom/helpshift/websockets/WebSocketOutputStream;Ljava/lang/String;)V

    .line 2593
    invoke-direct {p0, v1, v2}, Lcom/helpshift/websockets/WebSocket;->readHandshake(Lcom/helpshift/websockets/WebSocketInputStream;Ljava/lang/String;)Ljava/util/Map;

    move-result-object v2

    .line 2597
    iput-object v1, p0, Lcom/helpshift/websockets/WebSocket;->mInput:Lcom/helpshift/websockets/WebSocketInputStream;

    .line 2598
    iput-object v0, p0, Lcom/helpshift/websockets/WebSocket;->mOutput:Lcom/helpshift/websockets/WebSocketOutputStream;

    return-object v2
.end method

.method private splitIfNecessary(Lcom/helpshift/websockets/WebSocketFrame;)Ljava/util/List;
    .locals 2
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Lcom/helpshift/websockets/WebSocketFrame;",
            ")",
            "Ljava/util/List<",
            "Lcom/helpshift/websockets/WebSocketFrame;",
            ">;"
        }
    .end annotation

    .line 2170
    iget v0, p0, Lcom/helpshift/websockets/WebSocket;->mMaxPayloadSize:I

    iget-object v1, p0, Lcom/helpshift/websockets/WebSocket;->mPerMessageCompressionExtension:Lcom/helpshift/websockets/PerMessageCompressionExtension;

    invoke-static {p1, v0, v1}, Lcom/helpshift/websockets/WebSocketFrame;->splitIfNecessary(Lcom/helpshift/websockets/WebSocketFrame;ILcom/helpshift/websockets/PerMessageCompressionExtension;)Ljava/util/List;

    move-result-object p1

    return-object p1
.end method

.method private startThreads()V
    .locals 3

    .line 2687
    new-instance v0, Lcom/helpshift/websockets/ReadingThread;

    invoke-direct {v0, p0}, Lcom/helpshift/websockets/ReadingThread;-><init>(Lcom/helpshift/websockets/WebSocket;)V

    .line 2688
    new-instance v1, Lcom/helpshift/websockets/WritingThread;

    invoke-direct {v1, p0}, Lcom/helpshift/websockets/WritingThread;-><init>(Lcom/helpshift/websockets/WebSocket;)V

    .line 2690
    iget-object v2, p0, Lcom/helpshift/websockets/WebSocket;->mThreadsLock:Ljava/lang/Object;

    monitor-enter v2

    .line 2691
    :try_start_0
    iput-object v0, p0, Lcom/helpshift/websockets/WebSocket;->mReadingThread:Lcom/helpshift/websockets/ReadingThread;

    .line 2692
    iput-object v1, p0, Lcom/helpshift/websockets/WebSocket;->mWritingThread:Lcom/helpshift/websockets/WritingThread;

    .line 2693
    monitor-exit v2
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    .line 2696
    invoke-virtual {v0}, Lcom/helpshift/websockets/ReadingThread;->callOnThreadCreated()V

    .line 2697
    invoke-virtual {v1}, Lcom/helpshift/websockets/WritingThread;->callOnThreadCreated()V

    .line 2699
    invoke-virtual {v0}, Lcom/helpshift/websockets/ReadingThread;->start()V

    .line 2700
    invoke-virtual {v1}, Lcom/helpshift/websockets/WritingThread;->start()V

    return-void

    :catchall_0
    move-exception v0

    .line 2693
    :try_start_1
    monitor-exit v2
    :try_end_1
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    throw v0
.end method

.method private stopThreads(J)V
    .locals 4

    .line 2718
    iget-object v0, p0, Lcom/helpshift/websockets/WebSocket;->mThreadsLock:Ljava/lang/Object;

    monitor-enter v0

    .line 2719
    :try_start_0
    iget-object v1, p0, Lcom/helpshift/websockets/WebSocket;->mReadingThread:Lcom/helpshift/websockets/ReadingThread;

    .line 2720
    iget-object v2, p0, Lcom/helpshift/websockets/WebSocket;->mWritingThread:Lcom/helpshift/websockets/WritingThread;

    const/4 v3, 0x0

    .line 2722
    iput-object v3, p0, Lcom/helpshift/websockets/WebSocket;->mReadingThread:Lcom/helpshift/websockets/ReadingThread;

    .line 2723
    iput-object v3, p0, Lcom/helpshift/websockets/WebSocket;->mWritingThread:Lcom/helpshift/websockets/WritingThread;

    .line 2724
    monitor-exit v0
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    if-eqz v1, :cond_0

    .line 2727
    invoke-virtual {v1, p1, p2}, Lcom/helpshift/websockets/ReadingThread;->requestStop(J)V

    :cond_0
    if-eqz v2, :cond_1

    .line 2731
    invoke-virtual {v2}, Lcom/helpshift/websockets/WritingThread;->requestStop()V

    :cond_1
    return-void

    :catchall_0
    move-exception p1

    .line 2724
    :try_start_1
    monitor-exit v0
    :try_end_1
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    throw p1
.end method

.method private writeHandshake(Lcom/helpshift/websockets/WebSocketOutputStream;Ljava/lang/String;)V
    .locals 3
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Lcom/helpshift/websockets/WebSocketException;
        }
    .end annotation

    .line 2647
    iget-object v0, p0, Lcom/helpshift/websockets/WebSocket;->mHandshakeBuilder:Lcom/helpshift/websockets/HandshakeBuilder;

    invoke-virtual {v0, p2}, Lcom/helpshift/websockets/HandshakeBuilder;->setKey(Ljava/lang/String;)V

    .line 2648
    iget-object p2, p0, Lcom/helpshift/websockets/WebSocket;->mHandshakeBuilder:Lcom/helpshift/websockets/HandshakeBuilder;

    invoke-virtual {p2}, Lcom/helpshift/websockets/HandshakeBuilder;->buildRequestLine()Ljava/lang/String;

    move-result-object p2

    .line 2649
    iget-object v0, p0, Lcom/helpshift/websockets/WebSocket;->mHandshakeBuilder:Lcom/helpshift/websockets/HandshakeBuilder;

    invoke-virtual {v0}, Lcom/helpshift/websockets/HandshakeBuilder;->buildHeaders()Ljava/util/List;

    move-result-object v0

    .line 2650
    invoke-static {p2, v0}, Lcom/helpshift/websockets/HandshakeBuilder;->build(Ljava/lang/String;Ljava/util/List;)Ljava/lang/String;

    move-result-object v1

    .line 2653
    iget-object v2, p0, Lcom/helpshift/websockets/WebSocket;->mListenerManager:Lcom/helpshift/websockets/ListenerManager;

    invoke-virtual {v2, p2, v0}, Lcom/helpshift/websockets/ListenerManager;->callOnSendingHandshake(Ljava/lang/String;Ljava/util/List;)V

    .line 2657
    :try_start_0
    invoke-virtual {p1, v1}, Lcom/helpshift/websockets/WebSocketOutputStream;->write(Ljava/lang/String;)V

    .line 2658
    invoke-virtual {p1}, Lcom/helpshift/websockets/WebSocketOutputStream;->flush()V
    :try_end_0
    .catch Ljava/io/IOException; {:try_start_0 .. :try_end_0} :catch_0

    return-void

    :catch_0
    move-exception p1

    .line 2662
    new-instance p2, Lcom/helpshift/websockets/WebSocketException;

    sget-object v0, Lcom/helpshift/websockets/WebSocketError;->OPENING_HAHDSHAKE_REQUEST_FAILURE:Lcom/helpshift/websockets/WebSocketError;

    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "Failed to send an opening handshake request to the server: "

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    .line 2664
    invoke-virtual {p1}, Ljava/io/IOException;->getMessage()Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-direct {p2, v0, v1, p1}, Lcom/helpshift/websockets/WebSocketException;-><init>(Lcom/helpshift/websockets/WebSocketError;Ljava/lang/String;Ljava/lang/Throwable;)V

    throw p2
.end method


# virtual methods
.method public addExtension(Ljava/lang/String;)Lcom/helpshift/websockets/WebSocket;
    .locals 1

    .line 1213
    iget-object v0, p0, Lcom/helpshift/websockets/WebSocket;->mHandshakeBuilder:Lcom/helpshift/websockets/HandshakeBuilder;

    invoke-virtual {v0, p1}, Lcom/helpshift/websockets/HandshakeBuilder;->addExtension(Ljava/lang/String;)V

    return-object p0
.end method

.method public addHeader(Ljava/lang/String;Ljava/lang/String;)Lcom/helpshift/websockets/WebSocket;
    .locals 1

    .line 1255
    iget-object v0, p0, Lcom/helpshift/websockets/WebSocket;->mHandshakeBuilder:Lcom/helpshift/websockets/HandshakeBuilder;

    invoke-virtual {v0, p1, p2}, Lcom/helpshift/websockets/HandshakeBuilder;->addHeader(Ljava/lang/String;Ljava/lang/String;)V

    return-object p0
.end method

.method public addListener(Lcom/helpshift/websockets/WebSocketListener;)Lcom/helpshift/websockets/WebSocket;
    .locals 1

    .line 1693
    iget-object v0, p0, Lcom/helpshift/websockets/WebSocket;->mListenerManager:Lcom/helpshift/websockets/ListenerManager;

    invoke-virtual {v0, p1}, Lcom/helpshift/websockets/ListenerManager;->addListener(Lcom/helpshift/websockets/WebSocketListener;)V

    return-object p0
.end method

.method public addListeners(Ljava/util/List;)Lcom/helpshift/websockets/WebSocket;
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/List<",
            "Lcom/helpshift/websockets/WebSocketListener;",
            ">;)",
            "Lcom/helpshift/websockets/WebSocket;"
        }
    .end annotation

    .line 1708
    iget-object v0, p0, Lcom/helpshift/websockets/WebSocket;->mListenerManager:Lcom/helpshift/websockets/ListenerManager;

    invoke-virtual {v0, p1}, Lcom/helpshift/websockets/ListenerManager;->addListeners(Ljava/util/List;)V

    return-object p0
.end method

.method public addProtocol(Ljava/lang/String;)Lcom/helpshift/websockets/WebSocket;
    .locals 1

    .line 1166
    iget-object v0, p0, Lcom/helpshift/websockets/WebSocket;->mHandshakeBuilder:Lcom/helpshift/websockets/HandshakeBuilder;

    invoke-virtual {v0, p1}, Lcom/helpshift/websockets/HandshakeBuilder;->addProtocol(Ljava/lang/String;)V

    return-object p0
.end method

.method public clearExtensions()Lcom/helpshift/websockets/WebSocket;
    .locals 1

    .line 1240
    iget-object v0, p0, Lcom/helpshift/websockets/WebSocket;->mHandshakeBuilder:Lcom/helpshift/websockets/HandshakeBuilder;

    invoke-virtual {v0}, Lcom/helpshift/websockets/HandshakeBuilder;->clearExtensions()V

    return-object p0
.end method

.method public clearHeaders()Lcom/helpshift/websockets/WebSocket;
    .locals 1

    .line 1282
    iget-object v0, p0, Lcom/helpshift/websockets/WebSocket;->mHandshakeBuilder:Lcom/helpshift/websockets/HandshakeBuilder;

    invoke-virtual {v0}, Lcom/helpshift/websockets/HandshakeBuilder;->clearHeaders()V

    return-object p0
.end method

.method public clearListeners()Lcom/helpshift/websockets/WebSocket;
    .locals 1

    .line 1750
    iget-object v0, p0, Lcom/helpshift/websockets/WebSocket;->mListenerManager:Lcom/helpshift/websockets/ListenerManager;

    invoke-virtual {v0}, Lcom/helpshift/websockets/ListenerManager;->clearListeners()V

    return-object p0
.end method

.method public clearProtocols()Lcom/helpshift/websockets/WebSocket;
    .locals 1

    .line 1193
    iget-object v0, p0, Lcom/helpshift/websockets/WebSocket;->mHandshakeBuilder:Lcom/helpshift/websockets/HandshakeBuilder;

    invoke-virtual {v0}, Lcom/helpshift/websockets/HandshakeBuilder;->clearProtocols()V

    return-object p0
.end method

.method public clearUserInfo()Lcom/helpshift/websockets/WebSocket;
    .locals 1

    .line 1323
    iget-object v0, p0, Lcom/helpshift/websockets/WebSocket;->mHandshakeBuilder:Lcom/helpshift/websockets/HandshakeBuilder;

    invoke-virtual {v0}, Lcom/helpshift/websockets/HandshakeBuilder;->clearUserInfo()V

    return-object p0
.end method

.method public connect()Lcom/helpshift/websockets/WebSocket;
    .locals 3
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Lcom/helpshift/websockets/WebSocketException;
        }
    .end annotation

    .line 1834
    invoke-direct {p0}, Lcom/helpshift/websockets/WebSocket;->changeStateOnConnect()V

    .line 1841
    :try_start_0
    iget-object v0, p0, Lcom/helpshift/websockets/WebSocket;->mSocketConnector:Lcom/helpshift/websockets/SocketConnector;

    invoke-virtual {v0}, Lcom/helpshift/websockets/SocketConnector;->connect()V

    .line 1844
    invoke-direct {p0}, Lcom/helpshift/websockets/WebSocket;->shakeHands()Ljava/util/Map;

    move-result-object v0
    :try_end_0
    .catch Lcom/helpshift/websockets/WebSocketException; {:try_start_0 .. :try_end_0} :catch_0

    .line 1861
    iput-object v0, p0, Lcom/helpshift/websockets/WebSocket;->mServerHeaders:Ljava/util/Map;

    .line 1864
    invoke-direct {p0}, Lcom/helpshift/websockets/WebSocket;->findAgreedPerMessageCompressionExtension()Lcom/helpshift/websockets/PerMessageCompressionExtension;

    move-result-object v0

    iput-object v0, p0, Lcom/helpshift/websockets/WebSocket;->mPerMessageCompressionExtension:Lcom/helpshift/websockets/PerMessageCompressionExtension;

    .line 1867
    iget-object v0, p0, Lcom/helpshift/websockets/WebSocket;->mStateManager:Lcom/helpshift/websockets/StateManager;

    sget-object v1, Lcom/helpshift/websockets/WebSocketState;->OPEN:Lcom/helpshift/websockets/WebSocketState;

    invoke-virtual {v0, v1}, Lcom/helpshift/websockets/StateManager;->setState(Lcom/helpshift/websockets/WebSocketState;)V

    .line 1870
    iget-object v0, p0, Lcom/helpshift/websockets/WebSocket;->mListenerManager:Lcom/helpshift/websockets/ListenerManager;

    sget-object v1, Lcom/helpshift/websockets/WebSocketState;->OPEN:Lcom/helpshift/websockets/WebSocketState;

    invoke-virtual {v0, v1}, Lcom/helpshift/websockets/ListenerManager;->callOnStateChanged(Lcom/helpshift/websockets/WebSocketState;)V

    .line 1873
    invoke-direct {p0}, Lcom/helpshift/websockets/WebSocket;->startThreads()V

    return-object p0

    :catch_0
    move-exception v0

    .line 1848
    iget-object v1, p0, Lcom/helpshift/websockets/WebSocket;->mSocketConnector:Lcom/helpshift/websockets/SocketConnector;

    invoke-virtual {v1}, Lcom/helpshift/websockets/SocketConnector;->closeSilently()V

    .line 1851
    iget-object v1, p0, Lcom/helpshift/websockets/WebSocket;->mStateManager:Lcom/helpshift/websockets/StateManager;

    sget-object v2, Lcom/helpshift/websockets/WebSocketState;->CLOSED:Lcom/helpshift/websockets/WebSocketState;

    invoke-virtual {v1, v2}, Lcom/helpshift/websockets/StateManager;->setState(Lcom/helpshift/websockets/WebSocketState;)V

    .line 1854
    iget-object v1, p0, Lcom/helpshift/websockets/WebSocket;->mListenerManager:Lcom/helpshift/websockets/ListenerManager;

    sget-object v2, Lcom/helpshift/websockets/WebSocketState;->CLOSED:Lcom/helpshift/websockets/WebSocketState;

    invoke-virtual {v1, v2}, Lcom/helpshift/websockets/ListenerManager;->callOnStateChanged(Lcom/helpshift/websockets/WebSocketState;)V

    .line 1857
    throw v0
.end method

.method public disconnect()Lcom/helpshift/websockets/WebSocket;
    .locals 2

    const/16 v0, 0x3e8

    const/4 v1, 0x0

    .line 1889
    invoke-virtual {p0, v0, v1}, Lcom/helpshift/websockets/WebSocket;->disconnect(ILjava/lang/String;)Lcom/helpshift/websockets/WebSocket;

    move-result-object v0

    return-object v0
.end method

.method public disconnect(I)Lcom/helpshift/websockets/WebSocket;
    .locals 1

    const/4 v0, 0x0

    .line 1908
    invoke-virtual {p0, p1, v0}, Lcom/helpshift/websockets/WebSocket;->disconnect(ILjava/lang/String;)Lcom/helpshift/websockets/WebSocket;

    move-result-object p1

    return-object p1
.end method

.method public disconnect(ILjava/lang/String;)Lcom/helpshift/websockets/WebSocket;
    .locals 2

    const-wide/16 v0, 0x2710

    .line 1959
    invoke-virtual {p0, p1, p2, v0, v1}, Lcom/helpshift/websockets/WebSocket;->disconnect(ILjava/lang/String;J)Lcom/helpshift/websockets/WebSocket;

    move-result-object p1

    return-object p1
.end method

.method public disconnect(ILjava/lang/String;J)Lcom/helpshift/websockets/WebSocket;
    .locals 3

    .line 1996
    iget-object v0, p0, Lcom/helpshift/websockets/WebSocket;->mStateManager:Lcom/helpshift/websockets/StateManager;

    monitor-enter v0

    .line 1997
    :try_start_0
    sget-object v1, Lcom/helpshift/websockets/WebSocket$1;->$SwitchMap$com$helpshift$websockets$WebSocketState:[I

    iget-object v2, p0, Lcom/helpshift/websockets/WebSocket;->mStateManager:Lcom/helpshift/websockets/StateManager;

    invoke-virtual {v2}, Lcom/helpshift/websockets/StateManager;->getState()Lcom/helpshift/websockets/WebSocketState;

    move-result-object v2

    invoke-virtual {v2}, Lcom/helpshift/websockets/WebSocketState;->ordinal()I

    move-result v2

    aget v1, v1, v2

    packed-switch v1, :pswitch_data_0

    .line 2015
    monitor-exit v0

    goto :goto_0

    .line 2019
    :pswitch_0
    iget-object v1, p0, Lcom/helpshift/websockets/WebSocket;->mStateManager:Lcom/helpshift/websockets/StateManager;

    sget-object v2, Lcom/helpshift/websockets/StateManager$CloseInitiator;->CLIENT:Lcom/helpshift/websockets/StateManager$CloseInitiator;

    invoke-virtual {v1, v2}, Lcom/helpshift/websockets/StateManager;->changeToClosing(Lcom/helpshift/websockets/StateManager$CloseInitiator;)V

    .line 2022
    invoke-static {p1, p2}, Lcom/helpshift/websockets/WebSocketFrame;->createCloseFrame(ILjava/lang/String;)Lcom/helpshift/websockets/WebSocketFrame;

    move-result-object p1

    .line 2025
    invoke-virtual {p0, p1}, Lcom/helpshift/websockets/WebSocket;->sendFrame(Lcom/helpshift/websockets/WebSocketFrame;)Lcom/helpshift/websockets/WebSocket;

    .line 2026
    monitor-exit v0
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    .line 2029
    iget-object p1, p0, Lcom/helpshift/websockets/WebSocket;->mListenerManager:Lcom/helpshift/websockets/ListenerManager;

    sget-object p2, Lcom/helpshift/websockets/WebSocketState;->CLOSING:Lcom/helpshift/websockets/WebSocketState;

    invoke-virtual {p1, p2}, Lcom/helpshift/websockets/ListenerManager;->callOnStateChanged(Lcom/helpshift/websockets/WebSocketState;)V

    const-wide/16 p1, 0x0

    cmp-long v0, p3, p1

    if-gez v0, :cond_0

    const-wide/16 p3, 0x2710

    .line 2038
    :cond_0
    invoke-direct {p0, p3, p4}, Lcom/helpshift/websockets/WebSocket;->stopThreads(J)V

    return-object p0

    .line 1999
    :pswitch_1
    :try_start_1
    invoke-direct {p0}, Lcom/helpshift/websockets/WebSocket;->finishAsynchronously()V

    .line 2000
    monitor-exit v0

    return-object p0

    :goto_0
    return-object p0

    :catchall_0
    move-exception p1

    .line 2026
    monitor-exit v0
    :try_end_1
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    throw p1

    nop

    :pswitch_data_0
    .packed-switch 0x1
        :pswitch_1
        :pswitch_0
    .end packed-switch
.end method

.method public disconnect(Ljava/lang/String;)Lcom/helpshift/websockets/WebSocket;
    .locals 1

    const/16 v0, 0x3e8

    .line 1931
    invoke-virtual {p0, v0, p1}, Lcom/helpshift/websockets/WebSocket;->disconnect(ILjava/lang/String;)Lcom/helpshift/websockets/WebSocket;

    move-result-object p1

    return-object p1
.end method

.method protected finalize()V
    .locals 1
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/lang/Throwable;
        }
    .end annotation

    .line 1097
    sget-object v0, Lcom/helpshift/websockets/WebSocketState;->CREATED:Lcom/helpshift/websockets/WebSocketState;

    invoke-direct {p0, v0}, Lcom/helpshift/websockets/WebSocket;->isInState(Lcom/helpshift/websockets/WebSocketState;)Z

    move-result v0

    if-eqz v0, :cond_0

    .line 1099
    invoke-virtual {p0}, Lcom/helpshift/websockets/WebSocket;->finish()V

    .line 1102
    :cond_0
    invoke-super {p0}, Ljava/lang/Object;->finalize()V

    return-void
.end method

.method finish()V
    .locals 4

    .line 2906
    iget-object v0, p0, Lcom/helpshift/websockets/WebSocket;->mPingSender:Lcom/helpshift/websockets/PingSender;

    invoke-virtual {v0}, Lcom/helpshift/websockets/PingSender;->stop()V

    .line 2907
    iget-object v0, p0, Lcom/helpshift/websockets/WebSocket;->mPongSender:Lcom/helpshift/websockets/PongSender;

    invoke-virtual {v0}, Lcom/helpshift/websockets/PongSender;->stop()V

    .line 2911
    :try_start_0
    iget-object v0, p0, Lcom/helpshift/websockets/WebSocket;->mSocketConnector:Lcom/helpshift/websockets/SocketConnector;

    invoke-virtual {v0}, Lcom/helpshift/websockets/SocketConnector;->getSocket()Ljava/net/Socket;

    move-result-object v0

    invoke-virtual {v0}, Ljava/net/Socket;->close()V
    :try_end_0
    .catch Ljava/lang/Throwable; {:try_start_0 .. :try_end_0} :catch_0

    .line 2917
    :catch_0
    iget-object v0, p0, Lcom/helpshift/websockets/WebSocket;->mStateManager:Lcom/helpshift/websockets/StateManager;

    monitor-enter v0

    .line 2919
    :try_start_1
    iget-object v1, p0, Lcom/helpshift/websockets/WebSocket;->mStateManager:Lcom/helpshift/websockets/StateManager;

    sget-object v2, Lcom/helpshift/websockets/WebSocketState;->CLOSED:Lcom/helpshift/websockets/WebSocketState;

    invoke-virtual {v1, v2}, Lcom/helpshift/websockets/StateManager;->setState(Lcom/helpshift/websockets/WebSocketState;)V

    .line 2920
    monitor-exit v0
    :try_end_1
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    .line 2923
    iget-object v0, p0, Lcom/helpshift/websockets/WebSocket;->mListenerManager:Lcom/helpshift/websockets/ListenerManager;

    sget-object v1, Lcom/helpshift/websockets/WebSocketState;->CLOSED:Lcom/helpshift/websockets/WebSocketState;

    invoke-virtual {v0, v1}, Lcom/helpshift/websockets/ListenerManager;->callOnStateChanged(Lcom/helpshift/websockets/WebSocketState;)V

    .line 2926
    iget-object v0, p0, Lcom/helpshift/websockets/WebSocket;->mListenerManager:Lcom/helpshift/websockets/ListenerManager;

    iget-object v1, p0, Lcom/helpshift/websockets/WebSocket;->mServerCloseFrame:Lcom/helpshift/websockets/WebSocketFrame;

    iget-object v2, p0, Lcom/helpshift/websockets/WebSocket;->mClientCloseFrame:Lcom/helpshift/websockets/WebSocketFrame;

    iget-object v3, p0, Lcom/helpshift/websockets/WebSocket;->mStateManager:Lcom/helpshift/websockets/StateManager;

    .line 2927
    invoke-virtual {v3}, Lcom/helpshift/websockets/StateManager;->getClosedByServer()Z

    move-result v3

    .line 2926
    invoke-virtual {v0, v1, v2, v3}, Lcom/helpshift/websockets/ListenerManager;->callOnDisconnected(Lcom/helpshift/websockets/WebSocketFrame;Lcom/helpshift/websockets/WebSocketFrame;Z)V

    return-void

    :catchall_0
    move-exception v1

    .line 2920
    :try_start_2
    monitor-exit v0
    :try_end_2
    .catchall {:try_start_2 .. :try_end_2} :catchall_0

    throw v1
.end method

.method public flush()Lcom/helpshift/websockets/WebSocket;
    .locals 3

    .line 1440
    iget-object v0, p0, Lcom/helpshift/websockets/WebSocket;->mStateManager:Lcom/helpshift/websockets/StateManager;

    monitor-enter v0

    .line 1441
    :try_start_0
    iget-object v1, p0, Lcom/helpshift/websockets/WebSocket;->mStateManager:Lcom/helpshift/websockets/StateManager;

    invoke-virtual {v1}, Lcom/helpshift/websockets/StateManager;->getState()Lcom/helpshift/websockets/WebSocketState;

    move-result-object v1

    .line 1443
    sget-object v2, Lcom/helpshift/websockets/WebSocketState;->OPEN:Lcom/helpshift/websockets/WebSocketState;

    if-eq v1, v2, :cond_0

    sget-object v2, Lcom/helpshift/websockets/WebSocketState;->CLOSING:Lcom/helpshift/websockets/WebSocketState;

    if-eq v1, v2, :cond_0

    .line 1444
    monitor-exit v0

    return-object p0

    .line 1446
    :cond_0
    monitor-exit v0
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    .line 1449
    iget-object v0, p0, Lcom/helpshift/websockets/WebSocket;->mWritingThread:Lcom/helpshift/websockets/WritingThread;

    if-eqz v0, :cond_1

    .line 1454
    invoke-virtual {v0}, Lcom/helpshift/websockets/WritingThread;->queueFlush()V

    :cond_1
    return-object p0

    :catchall_0
    move-exception v1

    .line 1446
    :try_start_1
    monitor-exit v0
    :try_end_1
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    throw v1
.end method

.method public getAgreedExtensions()Ljava/util/List;
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()",
            "Ljava/util/List<",
            "Lcom/helpshift/websockets/WebSocketExtension;",
            ">;"
        }
    .end annotation

    .line 2055
    iget-object v0, p0, Lcom/helpshift/websockets/WebSocket;->mAgreedExtensions:Ljava/util/List;

    return-object v0
.end method

.method public getAgreedProtocol()Ljava/lang/String;
    .locals 1

    .line 2076
    iget-object v0, p0, Lcom/helpshift/websockets/WebSocket;->mAgreedProtocol:Ljava/lang/String;

    return-object v0
.end method

.method public getFrameQueueSize()I
    .locals 1

    .line 1469
    iget v0, p0, Lcom/helpshift/websockets/WebSocket;->mFrameQueueSize:I

    return v0
.end method

.method getHandshakeBuilder()Lcom/helpshift/websockets/HandshakeBuilder;
    .locals 1

    .line 2767
    iget-object v0, p0, Lcom/helpshift/websockets/WebSocket;->mHandshakeBuilder:Lcom/helpshift/websockets/HandshakeBuilder;

    return-object v0
.end method

.method getInput()Lcom/helpshift/websockets/WebSocketInputStream;
    .locals 1

    .line 2739
    iget-object v0, p0, Lcom/helpshift/websockets/WebSocket;->mInput:Lcom/helpshift/websockets/WebSocketInputStream;

    return-object v0
.end method

.method getListenerManager()Lcom/helpshift/websockets/ListenerManager;
    .locals 1

    .line 2760
    iget-object v0, p0, Lcom/helpshift/websockets/WebSocket;->mListenerManager:Lcom/helpshift/websockets/ListenerManager;

    return-object v0
.end method

.method public getMaxPayloadSize()I
    .locals 1

    .line 1519
    iget v0, p0, Lcom/helpshift/websockets/WebSocket;->mMaxPayloadSize:I

    return v0
.end method

.method getOutput()Lcom/helpshift/websockets/WebSocketOutputStream;
    .locals 1

    .line 2746
    iget-object v0, p0, Lcom/helpshift/websockets/WebSocket;->mOutput:Lcom/helpshift/websockets/WebSocketOutputStream;

    return-object v0
.end method

.method getPerMessageCompressionExtension()Lcom/helpshift/websockets/PerMessageCompressionExtension;
    .locals 1

    .line 2968
    iget-object v0, p0, Lcom/helpshift/websockets/WebSocket;->mPerMessageCompressionExtension:Lcom/helpshift/websockets/PerMessageCompressionExtension;

    return-object v0
.end method

.method public getPingInterval()J
    .locals 2

    .line 1558
    iget-object v0, p0, Lcom/helpshift/websockets/WebSocket;->mPingSender:Lcom/helpshift/websockets/PingSender;

    invoke-virtual {v0}, Lcom/helpshift/websockets/PingSender;->getInterval()J

    move-result-wide v0

    return-wide v0
.end method

.method public getPingPayloadGenerator()Lcom/helpshift/websockets/PayloadGenerator;
    .locals 1

    .line 1645
    iget-object v0, p0, Lcom/helpshift/websockets/WebSocket;->mPingSender:Lcom/helpshift/websockets/PingSender;

    invoke-virtual {v0}, Lcom/helpshift/websockets/PingSender;->getPayloadGenerator()Lcom/helpshift/websockets/PayloadGenerator;

    move-result-object v0

    return-object v0
.end method

.method public getPongInterval()J
    .locals 2

    .line 1594
    iget-object v0, p0, Lcom/helpshift/websockets/WebSocket;->mPongSender:Lcom/helpshift/websockets/PongSender;

    invoke-virtual {v0}, Lcom/helpshift/websockets/PongSender;->getInterval()J

    move-result-wide v0

    return-wide v0
.end method

.method public getPongPayloadGenerator()Lcom/helpshift/websockets/PayloadGenerator;
    .locals 1

    .line 1669
    iget-object v0, p0, Lcom/helpshift/websockets/WebSocket;->mPongSender:Lcom/helpshift/websockets/PongSender;

    invoke-virtual {v0}, Lcom/helpshift/websockets/PongSender;->getPayloadGenerator()Lcom/helpshift/websockets/PayloadGenerator;

    move-result-object v0

    return-object v0
.end method

.method public getSocket()Ljava/net/Socket;
    .locals 1

    .line 1762
    iget-object v0, p0, Lcom/helpshift/websockets/WebSocket;->mSocketConnector:Lcom/helpshift/websockets/SocketConnector;

    invoke-virtual {v0}, Lcom/helpshift/websockets/SocketConnector;->getSocket()Ljava/net/Socket;

    move-result-object v0

    return-object v0
.end method

.method public getState()Lcom/helpshift/websockets/WebSocketState;
    .locals 2

    .line 1128
    iget-object v0, p0, Lcom/helpshift/websockets/WebSocket;->mStateManager:Lcom/helpshift/websockets/StateManager;

    monitor-enter v0

    .line 1129
    :try_start_0
    iget-object v1, p0, Lcom/helpshift/websockets/WebSocket;->mStateManager:Lcom/helpshift/websockets/StateManager;

    invoke-virtual {v1}, Lcom/helpshift/websockets/StateManager;->getState()Lcom/helpshift/websockets/WebSocketState;

    move-result-object v1

    monitor-exit v0

    return-object v1

    :catchall_0
    move-exception v1

    .line 1130
    monitor-exit v0
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    throw v1
.end method

.method getStateManager()Lcom/helpshift/websockets/StateManager;
    .locals 1

    .line 2753
    iget-object v0, p0, Lcom/helpshift/websockets/WebSocket;->mStateManager:Lcom/helpshift/websockets/StateManager;

    return-object v0
.end method

.method public getURI()Ljava/net/URI;
    .locals 1

    .line 1774
    iget-object v0, p0, Lcom/helpshift/websockets/WebSocket;->mHandshakeBuilder:Lcom/helpshift/websockets/HandshakeBuilder;

    invoke-virtual {v0}, Lcom/helpshift/websockets/HandshakeBuilder;->getURI()Ljava/net/URI;

    move-result-object v0

    return-object v0
.end method

.method public isAutoFlush()Z
    .locals 1

    .line 1372
    iget-boolean v0, p0, Lcom/helpshift/websockets/WebSocket;->mAutoFlush:Z

    return v0
.end method

.method public isExtended()Z
    .locals 1

    .line 1346
    iget-boolean v0, p0, Lcom/helpshift/websockets/WebSocket;->mExtended:Z

    return v0
.end method

.method public isMissingCloseFrameAllowed()Z
    .locals 1

    .line 1408
    iget-boolean v0, p0, Lcom/helpshift/websockets/WebSocket;->mMissingCloseFrameAllowed:Z

    return v0
.end method

.method public isOpen()Z
    .locals 1

    .line 1142
    sget-object v0, Lcom/helpshift/websockets/WebSocketState;->OPEN:Lcom/helpshift/websockets/WebSocketState;

    invoke-direct {p0, v0}, Lcom/helpshift/websockets/WebSocket;->isInState(Lcom/helpshift/websockets/WebSocketState;)Z

    move-result v0

    return v0
.end method

.method onReadingThreadFinished(Lcom/helpshift/websockets/WebSocketFrame;)V
    .locals 2

    .line 2860
    iget-object v0, p0, Lcom/helpshift/websockets/WebSocket;->mThreadsLock:Ljava/lang/Object;

    monitor-enter v0

    const/4 v1, 0x1

    .line 2861
    :try_start_0
    iput-boolean v1, p0, Lcom/helpshift/websockets/WebSocket;->mReadingThreadFinished:Z

    .line 2862
    iput-object p1, p0, Lcom/helpshift/websockets/WebSocket;->mServerCloseFrame:Lcom/helpshift/websockets/WebSocketFrame;

    .line 2864
    iget-boolean p1, p0, Lcom/helpshift/websockets/WebSocket;->mWritingThreadFinished:Z

    if-nez p1, :cond_0

    .line 2866
    monitor-exit v0

    return-void

    .line 2868
    :cond_0
    monitor-exit v0
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    .line 2871
    invoke-direct {p0}, Lcom/helpshift/websockets/WebSocket;->onThreadsFinished()V

    return-void

    :catchall_0
    move-exception p1

    .line 2868
    :try_start_1
    monitor-exit v0
    :try_end_1
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    throw p1
.end method

.method onReadingThreadStarted()V
    .locals 2

    .line 2776
    iget-object v0, p0, Lcom/helpshift/websockets/WebSocket;->mThreadsLock:Ljava/lang/Object;

    monitor-enter v0

    const/4 v1, 0x1

    .line 2777
    :try_start_0
    iput-boolean v1, p0, Lcom/helpshift/websockets/WebSocket;->mReadingThreadStarted:Z

    .line 2779
    iget-boolean v1, p0, Lcom/helpshift/websockets/WebSocket;->mWritingThreadStarted:Z

    .line 2783
    monitor-exit v0
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    .line 2786
    invoke-direct {p0}, Lcom/helpshift/websockets/WebSocket;->callOnConnectedIfNotYet()V

    if-eqz v1, :cond_0

    .line 2790
    invoke-direct {p0}, Lcom/helpshift/websockets/WebSocket;->onThreadsStarted()V

    :cond_0
    return-void

    :catchall_0
    move-exception v1

    .line 2783
    :try_start_1
    monitor-exit v0
    :try_end_1
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    throw v1
.end method

.method onWritingThreadFinished(Lcom/helpshift/websockets/WebSocketFrame;)V
    .locals 2

    .line 2879
    iget-object v0, p0, Lcom/helpshift/websockets/WebSocket;->mThreadsLock:Ljava/lang/Object;

    monitor-enter v0

    const/4 v1, 0x1

    .line 2880
    :try_start_0
    iput-boolean v1, p0, Lcom/helpshift/websockets/WebSocket;->mWritingThreadFinished:Z

    .line 2881
    iput-object p1, p0, Lcom/helpshift/websockets/WebSocket;->mClientCloseFrame:Lcom/helpshift/websockets/WebSocketFrame;

    .line 2883
    iget-boolean p1, p0, Lcom/helpshift/websockets/WebSocket;->mReadingThreadFinished:Z

    if-nez p1, :cond_0

    .line 2885
    monitor-exit v0

    return-void

    .line 2887
    :cond_0
    monitor-exit v0
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    .line 2890
    invoke-direct {p0}, Lcom/helpshift/websockets/WebSocket;->onThreadsFinished()V

    return-void

    :catchall_0
    move-exception p1

    .line 2887
    :try_start_1
    monitor-exit v0
    :try_end_1
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    throw p1
.end method

.method onWritingThreadStarted()V
    .locals 2

    .line 2801
    iget-object v0, p0, Lcom/helpshift/websockets/WebSocket;->mThreadsLock:Ljava/lang/Object;

    monitor-enter v0

    const/4 v1, 0x1

    .line 2802
    :try_start_0
    iput-boolean v1, p0, Lcom/helpshift/websockets/WebSocket;->mWritingThreadStarted:Z

    .line 2804
    iget-boolean v1, p0, Lcom/helpshift/websockets/WebSocket;->mReadingThreadStarted:Z

    .line 2808
    monitor-exit v0
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    .line 2811
    invoke-direct {p0}, Lcom/helpshift/websockets/WebSocket;->callOnConnectedIfNotYet()V

    if-eqz v1, :cond_0

    .line 2815
    invoke-direct {p0}, Lcom/helpshift/websockets/WebSocket;->onThreadsStarted()V

    :cond_0
    return-void

    :catchall_0
    move-exception v1

    .line 2808
    :try_start_1
    monitor-exit v0
    :try_end_1
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    throw v1
.end method

.method public removeExtension(Lcom/helpshift/websockets/WebSocketExtension;)Lcom/helpshift/websockets/WebSocket;
    .locals 1

    .line 1227
    iget-object v0, p0, Lcom/helpshift/websockets/WebSocket;->mHandshakeBuilder:Lcom/helpshift/websockets/HandshakeBuilder;

    invoke-virtual {v0, p1}, Lcom/helpshift/websockets/HandshakeBuilder;->removeExtension(Lcom/helpshift/websockets/WebSocketExtension;)V

    return-object p0
.end method

.method public removeHeaders(Ljava/lang/String;)Lcom/helpshift/websockets/WebSocket;
    .locals 1

    .line 1269
    iget-object v0, p0, Lcom/helpshift/websockets/WebSocket;->mHandshakeBuilder:Lcom/helpshift/websockets/HandshakeBuilder;

    invoke-virtual {v0, p1}, Lcom/helpshift/websockets/HandshakeBuilder;->removeHeaders(Ljava/lang/String;)V

    return-object p0
.end method

.method public removeListener(Lcom/helpshift/websockets/WebSocketListener;)Lcom/helpshift/websockets/WebSocket;
    .locals 1

    .line 1722
    iget-object v0, p0, Lcom/helpshift/websockets/WebSocket;->mListenerManager:Lcom/helpshift/websockets/ListenerManager;

    invoke-virtual {v0, p1}, Lcom/helpshift/websockets/ListenerManager;->removeListener(Lcom/helpshift/websockets/WebSocketListener;)V

    return-object p0
.end method

.method public removeListeners(Ljava/util/List;)Lcom/helpshift/websockets/WebSocket;
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/List<",
            "Lcom/helpshift/websockets/WebSocketListener;",
            ">;)",
            "Lcom/helpshift/websockets/WebSocket;"
        }
    .end annotation

    .line 1737
    iget-object v0, p0, Lcom/helpshift/websockets/WebSocket;->mListenerManager:Lcom/helpshift/websockets/ListenerManager;

    invoke-virtual {v0, p1}, Lcom/helpshift/websockets/ListenerManager;->removeListeners(Ljava/util/List;)V

    return-object p0
.end method

.method public removeProtocol(Ljava/lang/String;)Lcom/helpshift/websockets/WebSocket;
    .locals 1

    .line 1180
    iget-object v0, p0, Lcom/helpshift/websockets/WebSocket;->mHandshakeBuilder:Lcom/helpshift/websockets/HandshakeBuilder;

    invoke-virtual {v0, p1}, Lcom/helpshift/websockets/HandshakeBuilder;->removeProtocol(Ljava/lang/String;)V

    return-object p0
.end method

.method public sendBinary([B)Lcom/helpshift/websockets/WebSocket;
    .locals 0

    .line 2361
    invoke-static {p1}, Lcom/helpshift/websockets/WebSocketFrame;->createBinaryFrame([B)Lcom/helpshift/websockets/WebSocketFrame;

    move-result-object p1

    invoke-virtual {p0, p1}, Lcom/helpshift/websockets/WebSocket;->sendFrame(Lcom/helpshift/websockets/WebSocketFrame;)Lcom/helpshift/websockets/WebSocket;

    move-result-object p1

    return-object p1
.end method

.method public sendBinary([BZ)Lcom/helpshift/websockets/WebSocket;
    .locals 0

    .line 2380
    invoke-static {p1}, Lcom/helpshift/websockets/WebSocketFrame;->createBinaryFrame([B)Lcom/helpshift/websockets/WebSocketFrame;

    move-result-object p1

    invoke-virtual {p1, p2}, Lcom/helpshift/websockets/WebSocketFrame;->setFin(Z)Lcom/helpshift/websockets/WebSocketFrame;

    move-result-object p1

    invoke-virtual {p0, p1}, Lcom/helpshift/websockets/WebSocket;->sendFrame(Lcom/helpshift/websockets/WebSocketFrame;)Lcom/helpshift/websockets/WebSocket;

    move-result-object p1

    return-object p1
.end method

.method public sendClose()Lcom/helpshift/websockets/WebSocket;
    .locals 1

    .line 2395
    invoke-static {}, Lcom/helpshift/websockets/WebSocketFrame;->createCloseFrame()Lcom/helpshift/websockets/WebSocketFrame;

    move-result-object v0

    invoke-virtual {p0, v0}, Lcom/helpshift/websockets/WebSocket;->sendFrame(Lcom/helpshift/websockets/WebSocketFrame;)Lcom/helpshift/websockets/WebSocket;

    move-result-object v0

    return-object v0
.end method

.method public sendClose(I)Lcom/helpshift/websockets/WebSocket;
    .locals 0

    .line 2413
    invoke-static {p1}, Lcom/helpshift/websockets/WebSocketFrame;->createCloseFrame(I)Lcom/helpshift/websockets/WebSocketFrame;

    move-result-object p1

    invoke-virtual {p0, p1}, Lcom/helpshift/websockets/WebSocket;->sendFrame(Lcom/helpshift/websockets/WebSocketFrame;)Lcom/helpshift/websockets/WebSocket;

    move-result-object p1

    return-object p1
.end method

.method public sendClose(ILjava/lang/String;)Lcom/helpshift/websockets/WebSocket;
    .locals 0

    .line 2435
    invoke-static {p1, p2}, Lcom/helpshift/websockets/WebSocketFrame;->createCloseFrame(ILjava/lang/String;)Lcom/helpshift/websockets/WebSocketFrame;

    move-result-object p1

    invoke-virtual {p0, p1}, Lcom/helpshift/websockets/WebSocket;->sendFrame(Lcom/helpshift/websockets/WebSocketFrame;)Lcom/helpshift/websockets/WebSocket;

    move-result-object p1

    return-object p1
.end method

.method public sendContinuation()Lcom/helpshift/websockets/WebSocket;
    .locals 1

    .line 2192
    invoke-static {}, Lcom/helpshift/websockets/WebSocketFrame;->createContinuationFrame()Lcom/helpshift/websockets/WebSocketFrame;

    move-result-object v0

    invoke-virtual {p0, v0}, Lcom/helpshift/websockets/WebSocket;->sendFrame(Lcom/helpshift/websockets/WebSocketFrame;)Lcom/helpshift/websockets/WebSocket;

    move-result-object v0

    return-object v0
.end method

.method public sendContinuation(Ljava/lang/String;)Lcom/helpshift/websockets/WebSocket;
    .locals 0

    .line 2234
    invoke-static {p1}, Lcom/helpshift/websockets/WebSocketFrame;->createContinuationFrame(Ljava/lang/String;)Lcom/helpshift/websockets/WebSocketFrame;

    move-result-object p1

    invoke-virtual {p0, p1}, Lcom/helpshift/websockets/WebSocket;->sendFrame(Lcom/helpshift/websockets/WebSocketFrame;)Lcom/helpshift/websockets/WebSocket;

    move-result-object p1

    return-object p1
.end method

.method public sendContinuation(Ljava/lang/String;Z)Lcom/helpshift/websockets/WebSocket;
    .locals 0

    .line 2253
    invoke-static {p1}, Lcom/helpshift/websockets/WebSocketFrame;->createContinuationFrame(Ljava/lang/String;)Lcom/helpshift/websockets/WebSocketFrame;

    move-result-object p1

    invoke-virtual {p1, p2}, Lcom/helpshift/websockets/WebSocketFrame;->setFin(Z)Lcom/helpshift/websockets/WebSocketFrame;

    move-result-object p1

    invoke-virtual {p0, p1}, Lcom/helpshift/websockets/WebSocket;->sendFrame(Lcom/helpshift/websockets/WebSocketFrame;)Lcom/helpshift/websockets/WebSocket;

    move-result-object p1

    return-object p1
.end method

.method public sendContinuation(Z)Lcom/helpshift/websockets/WebSocket;
    .locals 1

    .line 2210
    invoke-static {}, Lcom/helpshift/websockets/WebSocketFrame;->createContinuationFrame()Lcom/helpshift/websockets/WebSocketFrame;

    move-result-object v0

    invoke-virtual {v0, p1}, Lcom/helpshift/websockets/WebSocketFrame;->setFin(Z)Lcom/helpshift/websockets/WebSocketFrame;

    move-result-object p1

    invoke-virtual {p0, p1}, Lcom/helpshift/websockets/WebSocket;->sendFrame(Lcom/helpshift/websockets/WebSocketFrame;)Lcom/helpshift/websockets/WebSocket;

    move-result-object p1

    return-object p1
.end method

.method public sendContinuation([B)Lcom/helpshift/websockets/WebSocket;
    .locals 0

    .line 2277
    invoke-static {p1}, Lcom/helpshift/websockets/WebSocketFrame;->createContinuationFrame([B)Lcom/helpshift/websockets/WebSocketFrame;

    move-result-object p1

    invoke-virtual {p0, p1}, Lcom/helpshift/websockets/WebSocket;->sendFrame(Lcom/helpshift/websockets/WebSocketFrame;)Lcom/helpshift/websockets/WebSocket;

    move-result-object p1

    return-object p1
.end method

.method public sendContinuation([BZ)Lcom/helpshift/websockets/WebSocket;
    .locals 0

    .line 2296
    invoke-static {p1}, Lcom/helpshift/websockets/WebSocketFrame;->createContinuationFrame([B)Lcom/helpshift/websockets/WebSocketFrame;

    move-result-object p1

    invoke-virtual {p1, p2}, Lcom/helpshift/websockets/WebSocketFrame;->setFin(Z)Lcom/helpshift/websockets/WebSocketFrame;

    move-result-object p1

    invoke-virtual {p0, p1}, Lcom/helpshift/websockets/WebSocket;->sendFrame(Lcom/helpshift/websockets/WebSocketFrame;)Lcom/helpshift/websockets/WebSocket;

    move-result-object p1

    return-object p1
.end method

.method public sendFrame(Lcom/helpshift/websockets/WebSocketFrame;)Lcom/helpshift/websockets/WebSocket;
    .locals 3

    if-nez p1, :cond_0

    return-object p0

    .line 2124
    :cond_0
    iget-object v0, p0, Lcom/helpshift/websockets/WebSocket;->mStateManager:Lcom/helpshift/websockets/StateManager;

    monitor-enter v0

    .line 2125
    :try_start_0
    iget-object v1, p0, Lcom/helpshift/websockets/WebSocket;->mStateManager:Lcom/helpshift/websockets/StateManager;

    invoke-virtual {v1}, Lcom/helpshift/websockets/StateManager;->getState()Lcom/helpshift/websockets/WebSocketState;

    move-result-object v1

    .line 2127
    sget-object v2, Lcom/helpshift/websockets/WebSocketState;->OPEN:Lcom/helpshift/websockets/WebSocketState;

    if-eq v1, v2, :cond_1

    sget-object v2, Lcom/helpshift/websockets/WebSocketState;->CLOSING:Lcom/helpshift/websockets/WebSocketState;

    if-eq v1, v2, :cond_1

    .line 2128
    monitor-exit v0

    return-object p0

    .line 2130
    :cond_1
    monitor-exit v0
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    .line 2135
    iget-object v0, p0, Lcom/helpshift/websockets/WebSocket;->mWritingThread:Lcom/helpshift/websockets/WritingThread;

    if-nez v0, :cond_2

    return-object p0

    .line 2149
    :cond_2
    invoke-direct {p0, p1}, Lcom/helpshift/websockets/WebSocket;->splitIfNecessary(Lcom/helpshift/websockets/WebSocketFrame;)Ljava/util/List;

    move-result-object v1

    if-nez v1, :cond_3

    .line 2157
    invoke-virtual {v0, p1}, Lcom/helpshift/websockets/WritingThread;->queueFrame(Lcom/helpshift/websockets/WebSocketFrame;)Z

    goto :goto_1

    .line 2160
    :cond_3
    invoke-interface {v1}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object p1

    :goto_0
    invoke-interface {p1}, Ljava/util/Iterator;->hasNext()Z

    move-result v1

    if-eqz v1, :cond_4

    invoke-interface {p1}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Lcom/helpshift/websockets/WebSocketFrame;

    .line 2162
    invoke-virtual {v0, v1}, Lcom/helpshift/websockets/WritingThread;->queueFrame(Lcom/helpshift/websockets/WebSocketFrame;)Z

    goto :goto_0

    :cond_4
    :goto_1
    return-object p0

    :catchall_0
    move-exception p1

    .line 2130
    :try_start_1
    monitor-exit v0
    :try_end_1
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    throw p1
.end method

.method public sendPing()Lcom/helpshift/websockets/WebSocket;
    .locals 1

    .line 2450
    invoke-static {}, Lcom/helpshift/websockets/WebSocketFrame;->createPingFrame()Lcom/helpshift/websockets/WebSocketFrame;

    move-result-object v0

    invoke-virtual {p0, v0}, Lcom/helpshift/websockets/WebSocket;->sendFrame(Lcom/helpshift/websockets/WebSocketFrame;)Lcom/helpshift/websockets/WebSocket;

    move-result-object v0

    return-object v0
.end method

.method public sendPing(Ljava/lang/String;)Lcom/helpshift/websockets/WebSocket;
    .locals 0

    .line 2490
    invoke-static {p1}, Lcom/helpshift/websockets/WebSocketFrame;->createPingFrame(Ljava/lang/String;)Lcom/helpshift/websockets/WebSocketFrame;

    move-result-object p1

    invoke-virtual {p0, p1}, Lcom/helpshift/websockets/WebSocket;->sendFrame(Lcom/helpshift/websockets/WebSocketFrame;)Lcom/helpshift/websockets/WebSocket;

    move-result-object p1

    return-object p1
.end method

.method public sendPing([B)Lcom/helpshift/websockets/WebSocket;
    .locals 0

    .line 2470
    invoke-static {p1}, Lcom/helpshift/websockets/WebSocketFrame;->createPingFrame([B)Lcom/helpshift/websockets/WebSocketFrame;

    move-result-object p1

    invoke-virtual {p0, p1}, Lcom/helpshift/websockets/WebSocket;->sendFrame(Lcom/helpshift/websockets/WebSocketFrame;)Lcom/helpshift/websockets/WebSocket;

    move-result-object p1

    return-object p1
.end method

.method public sendPong()Lcom/helpshift/websockets/WebSocket;
    .locals 1

    .line 2505
    invoke-static {}, Lcom/helpshift/websockets/WebSocketFrame;->createPongFrame()Lcom/helpshift/websockets/WebSocketFrame;

    move-result-object v0

    invoke-virtual {p0, v0}, Lcom/helpshift/websockets/WebSocket;->sendFrame(Lcom/helpshift/websockets/WebSocketFrame;)Lcom/helpshift/websockets/WebSocket;

    move-result-object v0

    return-object v0
.end method

.method public sendPong(Ljava/lang/String;)Lcom/helpshift/websockets/WebSocket;
    .locals 0

    .line 2545
    invoke-static {p1}, Lcom/helpshift/websockets/WebSocketFrame;->createPongFrame(Ljava/lang/String;)Lcom/helpshift/websockets/WebSocketFrame;

    move-result-object p1

    invoke-virtual {p0, p1}, Lcom/helpshift/websockets/WebSocket;->sendFrame(Lcom/helpshift/websockets/WebSocketFrame;)Lcom/helpshift/websockets/WebSocket;

    move-result-object p1

    return-object p1
.end method

.method public sendPong([B)Lcom/helpshift/websockets/WebSocket;
    .locals 0

    .line 2525
    invoke-static {p1}, Lcom/helpshift/websockets/WebSocketFrame;->createPongFrame([B)Lcom/helpshift/websockets/WebSocketFrame;

    move-result-object p1

    invoke-virtual {p0, p1}, Lcom/helpshift/websockets/WebSocket;->sendFrame(Lcom/helpshift/websockets/WebSocketFrame;)Lcom/helpshift/websockets/WebSocket;

    move-result-object p1

    return-object p1
.end method

.method public sendText(Ljava/lang/String;)Lcom/helpshift/websockets/WebSocket;
    .locals 0

    .line 2319
    invoke-static {p1}, Lcom/helpshift/websockets/WebSocketFrame;->createTextFrame(Ljava/lang/String;)Lcom/helpshift/websockets/WebSocketFrame;

    move-result-object p1

    invoke-virtual {p0, p1}, Lcom/helpshift/websockets/WebSocket;->sendFrame(Lcom/helpshift/websockets/WebSocketFrame;)Lcom/helpshift/websockets/WebSocket;

    move-result-object p1

    return-object p1
.end method

.method public sendText(Ljava/lang/String;Z)Lcom/helpshift/websockets/WebSocket;
    .locals 0

    .line 2338
    invoke-static {p1}, Lcom/helpshift/websockets/WebSocketFrame;->createTextFrame(Ljava/lang/String;)Lcom/helpshift/websockets/WebSocketFrame;

    move-result-object p1

    invoke-virtual {p1, p2}, Lcom/helpshift/websockets/WebSocketFrame;->setFin(Z)Lcom/helpshift/websockets/WebSocketFrame;

    move-result-object p1

    invoke-virtual {p0, p1}, Lcom/helpshift/websockets/WebSocket;->sendFrame(Lcom/helpshift/websockets/WebSocketFrame;)Lcom/helpshift/websockets/WebSocket;

    move-result-object p1

    return-object p1
.end method

.method setAgreedExtensions(Ljava/util/List;)V
    .locals 0
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/List<",
            "Lcom/helpshift/websockets/WebSocketExtension;",
            ">;)V"
        }
    .end annotation

    .line 2062
    iput-object p1, p0, Lcom/helpshift/websockets/WebSocket;->mAgreedExtensions:Ljava/util/List;

    return-void
.end method

.method setAgreedProtocol(Ljava/lang/String;)V
    .locals 0

    .line 2083
    iput-object p1, p0, Lcom/helpshift/websockets/WebSocket;->mAgreedProtocol:Ljava/lang/String;

    return-void
.end method

.method public setAutoFlush(Z)Lcom/helpshift/websockets/WebSocket;
    .locals 0

    .line 1385
    iput-boolean p1, p0, Lcom/helpshift/websockets/WebSocket;->mAutoFlush:Z

    return-object p0
.end method

.method public setExtended(Z)Lcom/helpshift/websockets/WebSocket;
    .locals 0

    .line 1357
    iput-boolean p1, p0, Lcom/helpshift/websockets/WebSocket;->mExtended:Z

    return-object p0
.end method

.method public setFrameQueueSize(I)Lcom/helpshift/websockets/WebSocket;
    .locals 1
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/lang/IllegalArgumentException;
        }
    .end annotation

    if-ltz p1, :cond_0

    .line 1504
    iput p1, p0, Lcom/helpshift/websockets/WebSocket;->mFrameQueueSize:I

    return-object p0

    .line 1501
    :cond_0
    new-instance p1, Ljava/lang/IllegalArgumentException;

    const-string v0, "size must not be negative."

    invoke-direct {p1, v0}, Ljava/lang/IllegalArgumentException;-><init>(Ljava/lang/String;)V

    throw p1
.end method

.method public setMaxPayloadSize(I)Lcom/helpshift/websockets/WebSocket;
    .locals 1
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/lang/IllegalArgumentException;
        }
    .end annotation

    if-ltz p1, :cond_0

    .line 1543
    iput p1, p0, Lcom/helpshift/websockets/WebSocket;->mMaxPayloadSize:I

    return-object p0

    .line 1540
    :cond_0
    new-instance p1, Ljava/lang/IllegalArgumentException;

    const-string v0, "size must not be negative."

    invoke-direct {p1, v0}, Ljava/lang/IllegalArgumentException;-><init>(Ljava/lang/String;)V

    throw p1
.end method

.method public setMissingCloseFrameAllowed(Z)Lcom/helpshift/websockets/WebSocket;
    .locals 0

    .line 1427
    iput-boolean p1, p0, Lcom/helpshift/websockets/WebSocket;->mMissingCloseFrameAllowed:Z

    return-object p0
.end method

.method public setPingInterval(J)Lcom/helpshift/websockets/WebSocket;
    .locals 1

    .line 1579
    iget-object v0, p0, Lcom/helpshift/websockets/WebSocket;->mPingSender:Lcom/helpshift/websockets/PingSender;

    invoke-virtual {v0, p1, p2}, Lcom/helpshift/websockets/PingSender;->setInterval(J)V

    return-object p0
.end method

.method public setPingPayloadGenerator(Lcom/helpshift/websockets/PayloadGenerator;)Lcom/helpshift/websockets/WebSocket;
    .locals 1

    .line 1656
    iget-object v0, p0, Lcom/helpshift/websockets/WebSocket;->mPingSender:Lcom/helpshift/websockets/PingSender;

    invoke-virtual {v0, p1}, Lcom/helpshift/websockets/PingSender;->setPayloadGenerator(Lcom/helpshift/websockets/PayloadGenerator;)V

    return-object p0
.end method

.method public setPongInterval(J)Lcom/helpshift/websockets/WebSocket;
    .locals 1

    .line 1632
    iget-object v0, p0, Lcom/helpshift/websockets/WebSocket;->mPongSender:Lcom/helpshift/websockets/PongSender;

    invoke-virtual {v0, p1, p2}, Lcom/helpshift/websockets/PongSender;->setInterval(J)V

    return-object p0
.end method

.method public setPongPayloadGenerator(Lcom/helpshift/websockets/PayloadGenerator;)Lcom/helpshift/websockets/WebSocket;
    .locals 1

    .line 1680
    iget-object v0, p0, Lcom/helpshift/websockets/WebSocket;->mPongSender:Lcom/helpshift/websockets/PongSender;

    invoke-virtual {v0, p1}, Lcom/helpshift/websockets/PongSender;->setPayloadGenerator(Lcom/helpshift/websockets/PayloadGenerator;)V

    return-object p0
.end method

.method public setUserInfo(Ljava/lang/String;)Lcom/helpshift/websockets/WebSocket;
    .locals 1

    .line 1296
    iget-object v0, p0, Lcom/helpshift/websockets/WebSocket;->mHandshakeBuilder:Lcom/helpshift/websockets/HandshakeBuilder;

    invoke-virtual {v0, p1}, Lcom/helpshift/websockets/HandshakeBuilder;->setUserInfo(Ljava/lang/String;)V

    return-object p0
.end method

.method public setUserInfo(Ljava/lang/String;Ljava/lang/String;)Lcom/helpshift/websockets/WebSocket;
    .locals 1

    .line 1310
    iget-object v0, p0, Lcom/helpshift/websockets/WebSocket;->mHandshakeBuilder:Lcom/helpshift/websockets/HandshakeBuilder;

    invoke-virtual {v0, p1, p2}, Lcom/helpshift/websockets/HandshakeBuilder;->setUserInfo(Ljava/lang/String;Ljava/lang/String;)V

    return-object p0
.end method
