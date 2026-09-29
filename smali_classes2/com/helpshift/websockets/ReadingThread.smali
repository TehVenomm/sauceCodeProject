.class Lcom/helpshift/websockets/ReadingThread;
.super Lcom/helpshift/websockets/WebSocketThread;
.source "ReadingThread.java"


# annotations
.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Lcom/helpshift/websockets/ReadingThread$CloseTask;
    }
.end annotation


# instance fields
.field private mCloseDelay:J

.field private mCloseFrame:Lcom/helpshift/websockets/WebSocketFrame;

.field private mCloseLock:Ljava/lang/Object;

.field private mCloseTask:Lcom/helpshift/websockets/ReadingThread$CloseTask;

.field private mCloseTimer:Ljava/util/Timer;

.field private mContinuation:Ljava/util/List;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/List<",
            "Lcom/helpshift/websockets/WebSocketFrame;",
            ">;"
        }
    .end annotation
.end field

.field private mNotWaitForCloseFrame:Z

.field private final mPMCE:Lcom/helpshift/websockets/PerMessageCompressionExtension;

.field private mStopRequested:Z


# direct methods
.method public constructor <init>(Lcom/helpshift/websockets/WebSocket;)V
    .locals 2

    const-string v0, "ReadingThread"

    .line 59
    sget-object v1, Lcom/helpshift/websockets/ThreadType;->READING_THREAD:Lcom/helpshift/websockets/ThreadType;

    invoke-direct {p0, v0, p1, v1}, Lcom/helpshift/websockets/WebSocketThread;-><init>(Ljava/lang/String;Lcom/helpshift/websockets/WebSocket;Lcom/helpshift/websockets/ThreadType;)V

    .line 50
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    iput-object v0, p0, Lcom/helpshift/websockets/ReadingThread;->mContinuation:Ljava/util/List;

    .line 51
    new-instance v0, Ljava/lang/Object;

    invoke-direct {v0}, Ljava/lang/Object;-><init>()V

    iput-object v0, p0, Lcom/helpshift/websockets/ReadingThread;->mCloseLock:Ljava/lang/Object;

    .line 61
    invoke-virtual {p1}, Lcom/helpshift/websockets/WebSocket;->getPerMessageCompressionExtension()Lcom/helpshift/websockets/PerMessageCompressionExtension;

    move-result-object p1

    iput-object p1, p0, Lcom/helpshift/websockets/ReadingThread;->mPMCE:Lcom/helpshift/websockets/PerMessageCompressionExtension;

    return-void
.end method

.method private callOnBinaryMessage([B)V
    .locals 1

    .line 189
    iget-object v0, p0, Lcom/helpshift/websockets/ReadingThread;->mWebSocket:Lcom/helpshift/websockets/WebSocket;

    invoke-virtual {v0}, Lcom/helpshift/websockets/WebSocket;->getListenerManager()Lcom/helpshift/websockets/ListenerManager;

    move-result-object v0

    invoke-virtual {v0, p1}, Lcom/helpshift/websockets/ListenerManager;->callOnBinaryMessage([B)V

    return-void
.end method

.method private callOnError(Lcom/helpshift/websockets/WebSocketException;)V
    .locals 1

    .line 198
    iget-object v0, p0, Lcom/helpshift/websockets/ReadingThread;->mWebSocket:Lcom/helpshift/websockets/WebSocket;

    invoke-virtual {v0}, Lcom/helpshift/websockets/WebSocket;->getListenerManager()Lcom/helpshift/websockets/ListenerManager;

    move-result-object v0

    invoke-virtual {v0, p1}, Lcom/helpshift/websockets/ListenerManager;->callOnError(Lcom/helpshift/websockets/WebSocketException;)V

    return-void
.end method

.method private callOnTextMessage(Ljava/lang/String;)V
    .locals 1

    .line 180
    iget-object v0, p0, Lcom/helpshift/websockets/ReadingThread;->mWebSocket:Lcom/helpshift/websockets/WebSocket;

    invoke-virtual {v0}, Lcom/helpshift/websockets/WebSocket;->getListenerManager()Lcom/helpshift/websockets/ListenerManager;

    move-result-object v0

    invoke-virtual {v0, p1}, Lcom/helpshift/websockets/ListenerManager;->callOnTextMessage(Ljava/lang/String;)V

    return-void
.end method

.method private callOnTextMessage([B)V
    .locals 5

    .line 157
    :try_start_0
    invoke-static {p1}, Lcom/helpshift/websockets/Misc;->toStringUTF8([B)Ljava/lang/String;

    move-result-object v0

    .line 160
    invoke-direct {p0, v0}, Lcom/helpshift/websockets/ReadingThread;->callOnTextMessage(Ljava/lang/String;)V
    :try_end_0
    .catch Ljava/lang/Throwable; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception v0

    .line 164
    new-instance v1, Lcom/helpshift/websockets/WebSocketException;

    sget-object v2, Lcom/helpshift/websockets/WebSocketError;->TEXT_MESSAGE_CONSTRUCTION_ERROR:Lcom/helpshift/websockets/WebSocketError;

    new-instance v3, Ljava/lang/StringBuilder;

    invoke-direct {v3}, Ljava/lang/StringBuilder;-><init>()V

    const-string v4, "Failed to convert payload data into a string: "

    invoke-virtual {v3, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    .line 166
    invoke-virtual {v0}, Ljava/lang/Throwable;->getMessage()Ljava/lang/String;

    move-result-object v4

    invoke-virtual {v3, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v3}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v3

    invoke-direct {v1, v2, v3, v0}, Lcom/helpshift/websockets/WebSocketException;-><init>(Lcom/helpshift/websockets/WebSocketError;Ljava/lang/String;Ljava/lang/Throwable;)V

    .line 169
    invoke-direct {p0, v1}, Lcom/helpshift/websockets/ReadingThread;->callOnError(Lcom/helpshift/websockets/WebSocketException;)V

    .line 170
    iget-object v0, p0, Lcom/helpshift/websockets/ReadingThread;->mWebSocket:Lcom/helpshift/websockets/WebSocket;

    invoke-virtual {v0}, Lcom/helpshift/websockets/WebSocket;->getListenerManager()Lcom/helpshift/websockets/ListenerManager;

    move-result-object v0

    invoke-virtual {v0, v1, p1}, Lcom/helpshift/websockets/ListenerManager;->callOnTextMessageError(Lcom/helpshift/websockets/WebSocketException;[B)V

    :goto_0
    return-void
.end method

.method private cancelClose()V
    .locals 2

    .line 943
    iget-object v0, p0, Lcom/helpshift/websockets/ReadingThread;->mCloseLock:Ljava/lang/Object;

    monitor-enter v0

    .line 944
    :try_start_0
    invoke-direct {p0}, Lcom/helpshift/websockets/ReadingThread;->cancelCloseTask()V

    .line 945
    monitor-exit v0

    return-void

    :catchall_0
    move-exception v1

    monitor-exit v0
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    throw v1
.end method

.method private cancelCloseTask()V
    .locals 2

    .line 950
    iget-object v0, p0, Lcom/helpshift/websockets/ReadingThread;->mCloseTimer:Ljava/util/Timer;

    const/4 v1, 0x0

    if-eqz v0, :cond_0

    .line 951
    iget-object v0, p0, Lcom/helpshift/websockets/ReadingThread;->mCloseTimer:Ljava/util/Timer;

    invoke-virtual {v0}, Ljava/util/Timer;->cancel()V

    .line 952
    iput-object v1, p0, Lcom/helpshift/websockets/ReadingThread;->mCloseTimer:Ljava/util/Timer;

    .line 955
    :cond_0
    iget-object v0, p0, Lcom/helpshift/websockets/ReadingThread;->mCloseTask:Lcom/helpshift/websockets/ReadingThread$CloseTask;

    if-eqz v0, :cond_1

    .line 956
    iget-object v0, p0, Lcom/helpshift/websockets/ReadingThread;->mCloseTask:Lcom/helpshift/websockets/ReadingThread$CloseTask;

    invoke-virtual {v0}, Lcom/helpshift/websockets/ReadingThread$CloseTask;->cancel()Z

    .line 957
    iput-object v1, p0, Lcom/helpshift/websockets/ReadingThread;->mCloseTask:Lcom/helpshift/websockets/ReadingThread$CloseTask;

    :cond_1
    return-void
.end method

.method private concatenatePayloads(Ljava/util/List;)[B
    .locals 5
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/List<",
            "Lcom/helpshift/websockets/WebSocketFrame;",
            ">;)[B"
        }
    .end annotation

    .line 667
    :try_start_0
    new-instance v0, Ljava/io/ByteArrayOutputStream;

    invoke-direct {v0}, Ljava/io/ByteArrayOutputStream;-><init>()V

    .line 670
    invoke-interface {p1}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v1

    :cond_0
    :goto_0
    invoke-interface {v1}, Ljava/util/Iterator;->hasNext()Z

    move-result v2

    if-eqz v2, :cond_2

    invoke-interface {v1}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Lcom/helpshift/websockets/WebSocketFrame;

    .line 672
    invoke-virtual {v2}, Lcom/helpshift/websockets/WebSocketFrame;->getPayload()[B

    move-result-object v2

    if-eqz v2, :cond_0

    .line 675
    array-length v3, v2

    if-nez v3, :cond_1

    goto :goto_0

    .line 680
    :cond_1
    invoke-virtual {v0, v2}, Ljava/io/ByteArrayOutputStream;->write([B)V

    goto :goto_0

    .line 684
    :cond_2
    invoke-virtual {v0}, Ljava/io/ByteArrayOutputStream;->toByteArray()[B

    move-result-object v0
    :try_end_0
    .catch Ljava/io/IOException; {:try_start_0 .. :try_end_0} :catch_1
    .catch Ljava/lang/OutOfMemoryError; {:try_start_0 .. :try_end_0} :catch_0

    return-object v0

    :catch_0
    move-exception v0

    goto :goto_1

    :catch_1
    move-exception v0

    .line 694
    :goto_1
    new-instance v1, Lcom/helpshift/websockets/WebSocketException;

    sget-object v2, Lcom/helpshift/websockets/WebSocketError;->MESSAGE_CONSTRUCTION_ERROR:Lcom/helpshift/websockets/WebSocketError;

    new-instance v3, Ljava/lang/StringBuilder;

    invoke-direct {v3}, Ljava/lang/StringBuilder;-><init>()V

    const-string v4, "Failed to concatenate payloads of multiple frames to construct a message: "

    invoke-virtual {v3, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    .line 696
    invoke-virtual {v0}, Ljava/lang/Throwable;->getMessage()Ljava/lang/String;

    move-result-object v4

    invoke-virtual {v3, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v3}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v3

    invoke-direct {v1, v2, v3, v0}, Lcom/helpshift/websockets/WebSocketException;-><init>(Lcom/helpshift/websockets/WebSocketError;Ljava/lang/String;Ljava/lang/Throwable;)V

    .line 699
    invoke-direct {p0, v1}, Lcom/helpshift/websockets/ReadingThread;->callOnError(Lcom/helpshift/websockets/WebSocketException;)V

    .line 700
    iget-object v0, p0, Lcom/helpshift/websockets/ReadingThread;->mWebSocket:Lcom/helpshift/websockets/WebSocket;

    invoke-virtual {v0}, Lcom/helpshift/websockets/WebSocket;->getListenerManager()Lcom/helpshift/websockets/ListenerManager;

    move-result-object v0

    invoke-virtual {v0, v1, p1}, Lcom/helpshift/websockets/ListenerManager;->callOnMessageError(Lcom/helpshift/websockets/WebSocketException;Ljava/util/List;)V

    const/16 p1, 0x3f1

    .line 705
    invoke-virtual {v1}, Lcom/helpshift/websockets/WebSocketException;->getMessage()Ljava/lang/String;

    move-result-object v0

    invoke-static {p1, v0}, Lcom/helpshift/websockets/WebSocketFrame;->createCloseFrame(ILjava/lang/String;)Lcom/helpshift/websockets/WebSocketFrame;

    move-result-object p1

    .line 708
    iget-object v0, p0, Lcom/helpshift/websockets/ReadingThread;->mWebSocket:Lcom/helpshift/websockets/WebSocket;

    invoke-virtual {v0, p1}, Lcom/helpshift/websockets/WebSocket;->sendFrame(Lcom/helpshift/websockets/WebSocketFrame;)Lcom/helpshift/websockets/WebSocket;

    const/4 p1, 0x0

    return-object p1
.end method

.method private createCloseFrame(Lcom/helpshift/websockets/WebSocketException;)Lcom/helpshift/websockets/WebSocketFrame;
    .locals 3

    .line 525
    sget-object v0, Lcom/helpshift/websockets/ReadingThread$1;->$SwitchMap$com$helpshift$websockets$WebSocketError:[I

    invoke-virtual {p1}, Lcom/helpshift/websockets/WebSocketException;->getError()Lcom/helpshift/websockets/WebSocketError;

    move-result-object v1

    invoke-virtual {v1}, Lcom/helpshift/websockets/WebSocketError;->ordinal()I

    move-result v1

    aget v0, v0, v1

    const/16 v1, 0x3ea

    const/16 v2, 0x3f0

    packed-switch v0, :pswitch_data_0

    :pswitch_0
    const/16 v1, 0x3f0

    goto :goto_0

    :pswitch_1
    const/16 v1, 0x3f1

    .line 566
    :goto_0
    :pswitch_2
    invoke-virtual {p1}, Lcom/helpshift/websockets/WebSocketException;->getMessage()Ljava/lang/String;

    move-result-object p1

    invoke-static {v1, p1}, Lcom/helpshift/websockets/WebSocketFrame;->createCloseFrame(ILjava/lang/String;)Lcom/helpshift/websockets/WebSocketFrame;

    move-result-object p1

    return-object p1

    nop

    :pswitch_data_0
    .packed-switch 0x1
        :pswitch_2
        :pswitch_2
        :pswitch_2
        :pswitch_1
        :pswitch_1
        :pswitch_2
        :pswitch_2
        :pswitch_2
        :pswitch_2
        :pswitch_2
        :pswitch_2
        :pswitch_2
        :pswitch_2
        :pswitch_0
        :pswitch_0
    .end packed-switch
.end method

.method private decompress([B)[B
    .locals 2

    .line 735
    :try_start_0
    iget-object v0, p0, Lcom/helpshift/websockets/ReadingThread;->mPMCE:Lcom/helpshift/websockets/PerMessageCompressionExtension;

    invoke-virtual {v0, p1}, Lcom/helpshift/websockets/PerMessageCompressionExtension;->decompress([B)[B

    move-result-object v0
    :try_end_0
    .catch Lcom/helpshift/websockets/WebSocketException; {:try_start_0 .. :try_end_0} :catch_0

    return-object v0

    :catch_0
    move-exception v0

    .line 742
    invoke-direct {p0, v0}, Lcom/helpshift/websockets/ReadingThread;->callOnError(Lcom/helpshift/websockets/WebSocketException;)V

    .line 743
    iget-object v1, p0, Lcom/helpshift/websockets/ReadingThread;->mWebSocket:Lcom/helpshift/websockets/WebSocket;

    invoke-virtual {v1}, Lcom/helpshift/websockets/WebSocket;->getListenerManager()Lcom/helpshift/websockets/ListenerManager;

    move-result-object v1

    invoke-virtual {v1, v0, p1}, Lcom/helpshift/websockets/ListenerManager;->callOnMessageDecompressionError(Lcom/helpshift/websockets/WebSocketException;[B)V

    const/16 p1, 0x3eb

    .line 748
    invoke-virtual {v0}, Lcom/helpshift/websockets/WebSocketException;->getMessage()Ljava/lang/String;

    move-result-object v0

    invoke-static {p1, v0}, Lcom/helpshift/websockets/WebSocketFrame;->createCloseFrame(ILjava/lang/String;)Lcom/helpshift/websockets/WebSocketFrame;

    move-result-object p1

    .line 751
    iget-object v0, p0, Lcom/helpshift/websockets/ReadingThread;->mWebSocket:Lcom/helpshift/websockets/WebSocket;

    invoke-virtual {v0, p1}, Lcom/helpshift/websockets/WebSocket;->sendFrame(Lcom/helpshift/websockets/WebSocketFrame;)Lcom/helpshift/websockets/WebSocket;

    const/4 p1, 0x0

    return-object p1
.end method

.method private getMessage(Lcom/helpshift/websockets/WebSocketFrame;)[B
    .locals 2

    .line 717
    invoke-virtual {p1}, Lcom/helpshift/websockets/WebSocketFrame;->getPayload()[B

    move-result-object v0

    .line 721
    iget-object v1, p0, Lcom/helpshift/websockets/ReadingThread;->mPMCE:Lcom/helpshift/websockets/PerMessageCompressionExtension;

    if-eqz v1, :cond_0

    invoke-virtual {p1}, Lcom/helpshift/websockets/WebSocketFrame;->getRsv1()Z

    move-result p1

    if-eqz p1, :cond_0

    .line 723
    invoke-direct {p0, v0}, Lcom/helpshift/websockets/ReadingThread;->decompress([B)[B

    move-result-object v0

    :cond_0
    return-object v0
.end method

.method private getMessage(Ljava/util/List;)[B
    .locals 2
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/List<",
            "Lcom/helpshift/websockets/WebSocketFrame;",
            ">;)[B"
        }
    .end annotation

    .line 644
    iget-object v0, p0, Lcom/helpshift/websockets/ReadingThread;->mContinuation:Ljava/util/List;

    invoke-direct {p0, v0}, Lcom/helpshift/websockets/ReadingThread;->concatenatePayloads(Ljava/util/List;)[B

    move-result-object v0

    if-nez v0, :cond_0

    const/4 p1, 0x0

    return-object p1

    .line 654
    :cond_0
    iget-object v1, p0, Lcom/helpshift/websockets/ReadingThread;->mPMCE:Lcom/helpshift/websockets/PerMessageCompressionExtension;

    if-eqz v1, :cond_1

    const/4 v1, 0x0

    invoke-interface {p1, v1}, Ljava/util/List;->get(I)Ljava/lang/Object;

    move-result-object p1

    check-cast p1, Lcom/helpshift/websockets/WebSocketFrame;

    invoke-virtual {p1}, Lcom/helpshift/websockets/WebSocketFrame;->getRsv1()Z

    move-result p1

    if-eqz p1, :cond_1

    .line 656
    invoke-direct {p0, v0}, Lcom/helpshift/websockets/ReadingThread;->decompress([B)[B

    move-result-object v0

    :cond_1
    return-object v0
.end method

.method private handleBinaryFrame(Lcom/helpshift/websockets/WebSocketFrame;)Z
    .locals 2

    .line 785
    iget-object v0, p0, Lcom/helpshift/websockets/ReadingThread;->mWebSocket:Lcom/helpshift/websockets/WebSocket;

    invoke-virtual {v0}, Lcom/helpshift/websockets/WebSocket;->getListenerManager()Lcom/helpshift/websockets/ListenerManager;

    move-result-object v0

    invoke-virtual {v0, p1}, Lcom/helpshift/websockets/ListenerManager;->callOnBinaryFrame(Lcom/helpshift/websockets/WebSocketFrame;)V

    .line 788
    invoke-virtual {p1}, Lcom/helpshift/websockets/WebSocketFrame;->getFin()Z

    move-result v0

    const/4 v1, 0x1

    if-nez v0, :cond_0

    .line 790
    iget-object v0, p0, Lcom/helpshift/websockets/ReadingThread;->mContinuation:Ljava/util/List;

    invoke-interface {v0, p1}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    return v1

    .line 798
    :cond_0
    invoke-direct {p0, p1}, Lcom/helpshift/websockets/ReadingThread;->getMessage(Lcom/helpshift/websockets/WebSocketFrame;)[B

    move-result-object p1

    .line 801
    invoke-direct {p0, p1}, Lcom/helpshift/websockets/ReadingThread;->callOnBinaryMessage([B)V

    return v1
.end method

.method private handleCloseFrame(Lcom/helpshift/websockets/WebSocketFrame;)Z
    .locals 4

    .line 810
    iget-object v0, p0, Lcom/helpshift/websockets/ReadingThread;->mWebSocket:Lcom/helpshift/websockets/WebSocket;

    invoke-virtual {v0}, Lcom/helpshift/websockets/WebSocket;->getStateManager()Lcom/helpshift/websockets/StateManager;

    move-result-object v0

    .line 813
    iput-object p1, p0, Lcom/helpshift/websockets/ReadingThread;->mCloseFrame:Lcom/helpshift/websockets/WebSocketFrame;

    .line 817
    monitor-enter v0

    .line 819
    :try_start_0
    invoke-virtual {v0}, Lcom/helpshift/websockets/StateManager;->getState()Lcom/helpshift/websockets/WebSocketState;

    move-result-object v1

    .line 822
    sget-object v2, Lcom/helpshift/websockets/WebSocketState;->CLOSING:Lcom/helpshift/websockets/WebSocketState;

    const/4 v3, 0x0

    if-eq v1, v2, :cond_0

    sget-object v2, Lcom/helpshift/websockets/WebSocketState;->CLOSED:Lcom/helpshift/websockets/WebSocketState;

    if-eq v1, v2, :cond_0

    .line 824
    sget-object v1, Lcom/helpshift/websockets/StateManager$CloseInitiator;->SERVER:Lcom/helpshift/websockets/StateManager$CloseInitiator;

    invoke-virtual {v0, v1}, Lcom/helpshift/websockets/StateManager;->changeToClosing(Lcom/helpshift/websockets/StateManager$CloseInitiator;)V

    .line 836
    iget-object v1, p0, Lcom/helpshift/websockets/ReadingThread;->mWebSocket:Lcom/helpshift/websockets/WebSocket;

    invoke-virtual {v1, p1}, Lcom/helpshift/websockets/WebSocket;->sendFrame(Lcom/helpshift/websockets/WebSocketFrame;)Lcom/helpshift/websockets/WebSocket;

    const/4 v1, 0x1

    goto :goto_0

    :cond_0
    const/4 v1, 0x0

    .line 840
    :goto_0
    monitor-exit v0
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    if-eqz v1, :cond_1

    .line 844
    iget-object v0, p0, Lcom/helpshift/websockets/ReadingThread;->mWebSocket:Lcom/helpshift/websockets/WebSocket;

    invoke-virtual {v0}, Lcom/helpshift/websockets/WebSocket;->getListenerManager()Lcom/helpshift/websockets/ListenerManager;

    move-result-object v0

    sget-object v1, Lcom/helpshift/websockets/WebSocketState;->CLOSING:Lcom/helpshift/websockets/WebSocketState;

    invoke-virtual {v0, v1}, Lcom/helpshift/websockets/ListenerManager;->callOnStateChanged(Lcom/helpshift/websockets/WebSocketState;)V

    .line 848
    :cond_1
    iget-object v0, p0, Lcom/helpshift/websockets/ReadingThread;->mWebSocket:Lcom/helpshift/websockets/WebSocket;

    invoke-virtual {v0}, Lcom/helpshift/websockets/WebSocket;->getListenerManager()Lcom/helpshift/websockets/ListenerManager;

    move-result-object v0

    invoke-virtual {v0, p1}, Lcom/helpshift/websockets/ListenerManager;->callOnCloseFrame(Lcom/helpshift/websockets/WebSocketFrame;)V

    return v3

    :catchall_0
    move-exception p1

    .line 840
    :try_start_1
    monitor-exit v0
    :try_end_1
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    throw p1
.end method

.method private handleContinuationFrame(Lcom/helpshift/websockets/WebSocketFrame;)Z
    .locals 3

    .line 603
    iget-object v0, p0, Lcom/helpshift/websockets/ReadingThread;->mWebSocket:Lcom/helpshift/websockets/WebSocket;

    invoke-virtual {v0}, Lcom/helpshift/websockets/WebSocket;->getListenerManager()Lcom/helpshift/websockets/ListenerManager;

    move-result-object v0

    invoke-virtual {v0, p1}, Lcom/helpshift/websockets/ListenerManager;->callOnContinuationFrame(Lcom/helpshift/websockets/WebSocketFrame;)V

    .line 606
    iget-object v0, p0, Lcom/helpshift/websockets/ReadingThread;->mContinuation:Ljava/util/List;

    invoke-interface {v0, p1}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    .line 609
    invoke-virtual {p1}, Lcom/helpshift/websockets/WebSocketFrame;->getFin()Z

    move-result p1

    const/4 v0, 0x1

    if-nez p1, :cond_0

    return v0

    .line 616
    :cond_0
    iget-object p1, p0, Lcom/helpshift/websockets/ReadingThread;->mContinuation:Ljava/util/List;

    invoke-direct {p0, p1}, Lcom/helpshift/websockets/ReadingThread;->getMessage(Ljava/util/List;)[B

    move-result-object p1

    const/4 v1, 0x0

    if-nez p1, :cond_1

    return v1

    .line 625
    :cond_1
    iget-object v2, p0, Lcom/helpshift/websockets/ReadingThread;->mContinuation:Ljava/util/List;

    invoke-interface {v2, v1}, Ljava/util/List;->get(I)Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Lcom/helpshift/websockets/WebSocketFrame;

    invoke-virtual {v1}, Lcom/helpshift/websockets/WebSocketFrame;->isTextFrame()Z

    move-result v1

    if-eqz v1, :cond_2

    .line 627
    invoke-direct {p0, p1}, Lcom/helpshift/websockets/ReadingThread;->callOnTextMessage([B)V

    goto :goto_0

    .line 631
    :cond_2
    invoke-direct {p0, p1}, Lcom/helpshift/websockets/ReadingThread;->callOnBinaryMessage([B)V

    .line 635
    :goto_0
    iget-object p1, p0, Lcom/helpshift/websockets/ReadingThread;->mContinuation:Ljava/util/List;

    invoke-interface {p1}, Ljava/util/List;->clear()V

    return v0
.end method

.method private handleFrame(Lcom/helpshift/websockets/WebSocketFrame;)Z
    .locals 1

    .line 572
    iget-object v0, p0, Lcom/helpshift/websockets/ReadingThread;->mWebSocket:Lcom/helpshift/websockets/WebSocket;

    invoke-virtual {v0}, Lcom/helpshift/websockets/WebSocket;->getListenerManager()Lcom/helpshift/websockets/ListenerManager;

    move-result-object v0

    invoke-virtual {v0, p1}, Lcom/helpshift/websockets/ListenerManager;->callOnFrame(Lcom/helpshift/websockets/WebSocketFrame;)V

    .line 575
    invoke-virtual {p1}, Lcom/helpshift/websockets/WebSocketFrame;->getOpcode()I

    move-result v0

    packed-switch v0, :pswitch_data_0

    packed-switch v0, :pswitch_data_1

    const/4 p1, 0x1

    return p1

    .line 592
    :pswitch_0
    invoke-direct {p0, p1}, Lcom/helpshift/websockets/ReadingThread;->handlePongFrame(Lcom/helpshift/websockets/WebSocketFrame;)Z

    move-result p1

    return p1

    .line 589
    :pswitch_1
    invoke-direct {p0, p1}, Lcom/helpshift/websockets/ReadingThread;->handlePingFrame(Lcom/helpshift/websockets/WebSocketFrame;)Z

    move-result p1

    return p1

    .line 586
    :pswitch_2
    invoke-direct {p0, p1}, Lcom/helpshift/websockets/ReadingThread;->handleCloseFrame(Lcom/helpshift/websockets/WebSocketFrame;)Z

    move-result p1

    return p1

    .line 583
    :pswitch_3
    invoke-direct {p0, p1}, Lcom/helpshift/websockets/ReadingThread;->handleBinaryFrame(Lcom/helpshift/websockets/WebSocketFrame;)Z

    move-result p1

    return p1

    .line 580
    :pswitch_4
    invoke-direct {p0, p1}, Lcom/helpshift/websockets/ReadingThread;->handleTextFrame(Lcom/helpshift/websockets/WebSocketFrame;)Z

    move-result p1

    return p1

    .line 577
    :pswitch_5
    invoke-direct {p0, p1}, Lcom/helpshift/websockets/ReadingThread;->handleContinuationFrame(Lcom/helpshift/websockets/WebSocketFrame;)Z

    move-result p1

    return p1

    nop

    :pswitch_data_0
    .packed-switch 0x0
        :pswitch_5
        :pswitch_4
        :pswitch_3
    .end packed-switch

    :pswitch_data_1
    .packed-switch 0x8
        :pswitch_2
        :pswitch_1
        :pswitch_0
    .end packed-switch
.end method

.method private handlePingFrame(Lcom/helpshift/websockets/WebSocketFrame;)Z
    .locals 1

    .line 857
    iget-object v0, p0, Lcom/helpshift/websockets/ReadingThread;->mWebSocket:Lcom/helpshift/websockets/WebSocket;

    invoke-virtual {v0}, Lcom/helpshift/websockets/WebSocket;->getListenerManager()Lcom/helpshift/websockets/ListenerManager;

    move-result-object v0

    invoke-virtual {v0, p1}, Lcom/helpshift/websockets/ListenerManager;->callOnPingFrame(Lcom/helpshift/websockets/WebSocketFrame;)V

    .line 868
    invoke-virtual {p1}, Lcom/helpshift/websockets/WebSocketFrame;->getPayload()[B

    move-result-object p1

    invoke-static {p1}, Lcom/helpshift/websockets/WebSocketFrame;->createPongFrame([B)Lcom/helpshift/websockets/WebSocketFrame;

    move-result-object p1

    .line 871
    iget-object v0, p0, Lcom/helpshift/websockets/ReadingThread;->mWebSocket:Lcom/helpshift/websockets/WebSocket;

    invoke-virtual {v0, p1}, Lcom/helpshift/websockets/WebSocket;->sendFrame(Lcom/helpshift/websockets/WebSocketFrame;)Lcom/helpshift/websockets/WebSocket;

    const/4 p1, 0x1

    return p1
.end method

.method private handlePongFrame(Lcom/helpshift/websockets/WebSocketFrame;)Z
    .locals 1

    .line 880
    iget-object v0, p0, Lcom/helpshift/websockets/ReadingThread;->mWebSocket:Lcom/helpshift/websockets/WebSocket;

    invoke-virtual {v0}, Lcom/helpshift/websockets/WebSocket;->getListenerManager()Lcom/helpshift/websockets/ListenerManager;

    move-result-object v0

    invoke-virtual {v0, p1}, Lcom/helpshift/websockets/ListenerManager;->callOnPongFrame(Lcom/helpshift/websockets/WebSocketFrame;)V

    const/4 p1, 0x1

    return p1
.end method

.method private handleTextFrame(Lcom/helpshift/websockets/WebSocketFrame;)Z
    .locals 2

    .line 760
    iget-object v0, p0, Lcom/helpshift/websockets/ReadingThread;->mWebSocket:Lcom/helpshift/websockets/WebSocket;

    invoke-virtual {v0}, Lcom/helpshift/websockets/WebSocket;->getListenerManager()Lcom/helpshift/websockets/ListenerManager;

    move-result-object v0

    invoke-virtual {v0, p1}, Lcom/helpshift/websockets/ListenerManager;->callOnTextFrame(Lcom/helpshift/websockets/WebSocketFrame;)V

    .line 763
    invoke-virtual {p1}, Lcom/helpshift/websockets/WebSocketFrame;->getFin()Z

    move-result v0

    const/4 v1, 0x1

    if-nez v0, :cond_0

    .line 765
    iget-object v0, p0, Lcom/helpshift/websockets/ReadingThread;->mContinuation:Ljava/util/List;

    invoke-interface {v0, p1}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    return v1

    .line 773
    :cond_0
    invoke-direct {p0, p1}, Lcom/helpshift/websockets/ReadingThread;->getMessage(Lcom/helpshift/websockets/WebSocketFrame;)[B

    move-result-object p1

    .line 776
    invoke-direct {p0, p1}, Lcom/helpshift/websockets/ReadingThread;->callOnTextMessage([B)V

    return v1
.end method

.method private main()V
    .locals 1

    .line 88
    iget-object v0, p0, Lcom/helpshift/websockets/ReadingThread;->mWebSocket:Lcom/helpshift/websockets/WebSocket;

    invoke-virtual {v0}, Lcom/helpshift/websockets/WebSocket;->onReadingThreadStarted()V

    .line 91
    :cond_0
    monitor-enter p0

    .line 92
    :try_start_0
    iget-boolean v0, p0, Lcom/helpshift/websockets/ReadingThread;->mStopRequested:Z

    if-eqz v0, :cond_1

    .line 93
    monitor-exit p0

    goto :goto_0

    .line 95
    :cond_1
    monitor-exit p0
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    .line 98
    invoke-direct {p0}, Lcom/helpshift/websockets/ReadingThread;->readFrame()Lcom/helpshift/websockets/WebSocketFrame;

    move-result-object v0

    if-nez v0, :cond_2

    goto :goto_0

    .line 106
    :cond_2
    invoke-direct {p0, v0}, Lcom/helpshift/websockets/ReadingThread;->handleFrame(Lcom/helpshift/websockets/WebSocketFrame;)Z

    move-result v0

    if-nez v0, :cond_0

    .line 114
    :goto_0
    invoke-direct {p0}, Lcom/helpshift/websockets/ReadingThread;->waitForCloseFrame()V

    .line 117
    invoke-direct {p0}, Lcom/helpshift/websockets/ReadingThread;->cancelClose()V

    return-void

    :catchall_0
    move-exception v0

    .line 95
    :try_start_1
    monitor-exit p0
    :try_end_1
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    throw v0
.end method

.method private readFrame()Lcom/helpshift/websockets/WebSocketFrame;
    .locals 7

    const/4 v0, 0x0

    .line 208
    :try_start_0
    iget-object v1, p0, Lcom/helpshift/websockets/ReadingThread;->mWebSocket:Lcom/helpshift/websockets/WebSocket;

    invoke-virtual {v1}, Lcom/helpshift/websockets/WebSocket;->getInput()Lcom/helpshift/websockets/WebSocketInputStream;

    move-result-object v1

    invoke-virtual {v1}, Lcom/helpshift/websockets/WebSocketInputStream;->readFrame()Lcom/helpshift/websockets/WebSocketFrame;

    move-result-object v1
    :try_end_0
    .catch Ljava/io/InterruptedIOException; {:try_start_0 .. :try_end_0} :catch_5
    .catch Ljava/io/IOException; {:try_start_0 .. :try_end_0} :catch_4
    .catch Lcom/helpshift/websockets/WebSocketException; {:try_start_0 .. :try_end_0} :catch_3

    .line 211
    :try_start_1
    invoke-direct {p0, v1}, Lcom/helpshift/websockets/ReadingThread;->verifyFrame(Lcom/helpshift/websockets/WebSocketFrame;)V
    :try_end_1
    .catch Ljava/io/InterruptedIOException; {:try_start_1 .. :try_end_1} :catch_2
    .catch Ljava/io/IOException; {:try_start_1 .. :try_end_1} :catch_1
    .catch Lcom/helpshift/websockets/WebSocketException; {:try_start_1 .. :try_end_1} :catch_0

    return-object v1

    :catch_0
    move-exception v2

    goto :goto_0

    :catch_1
    move-exception v2

    goto :goto_1

    :catch_2
    move-exception v2

    goto :goto_2

    :catch_3
    move-exception v2

    move-object v1, v0

    :goto_0
    move-object v3, v2

    goto :goto_3

    :catch_4
    move-exception v2

    move-object v1, v0

    .line 230
    :goto_1
    iget-boolean v3, p0, Lcom/helpshift/websockets/ReadingThread;->mStopRequested:Z

    if-eqz v3, :cond_0

    invoke-virtual {p0}, Lcom/helpshift/websockets/ReadingThread;->isInterrupted()Z

    move-result v3

    if-eqz v3, :cond_0

    return-object v0

    .line 237
    :cond_0
    new-instance v3, Lcom/helpshift/websockets/WebSocketException;

    sget-object v4, Lcom/helpshift/websockets/WebSocketError;->IO_ERROR_IN_READING:Lcom/helpshift/websockets/WebSocketError;

    new-instance v5, Ljava/lang/StringBuilder;

    invoke-direct {v5}, Ljava/lang/StringBuilder;-><init>()V

    const-string v6, "An I/O error occurred while a frame was being read from the web socket: "

    invoke-virtual {v5, v6}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    .line 239
    invoke-virtual {v2}, Ljava/io/IOException;->getMessage()Ljava/lang/String;

    move-result-object v6

    invoke-virtual {v5, v6}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v5}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v5

    invoke-direct {v3, v4, v5, v2}, Lcom/helpshift/websockets/WebSocketException;-><init>(Lcom/helpshift/websockets/WebSocketError;Ljava/lang/String;Ljava/lang/Throwable;)V

    goto :goto_3

    :catch_5
    move-exception v2

    move-object v1, v0

    .line 217
    :goto_2
    iget-boolean v3, p0, Lcom/helpshift/websockets/ReadingThread;->mStopRequested:Z

    if-eqz v3, :cond_1

    return-object v0

    .line 224
    :cond_1
    new-instance v3, Lcom/helpshift/websockets/WebSocketException;

    sget-object v4, Lcom/helpshift/websockets/WebSocketError;->INTERRUPTED_IN_READING:Lcom/helpshift/websockets/WebSocketError;

    new-instance v5, Ljava/lang/StringBuilder;

    invoke-direct {v5}, Ljava/lang/StringBuilder;-><init>()V

    const-string v6, "Interruption occurred while a frame was being read from the web socket: "

    invoke-virtual {v5, v6}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    .line 226
    invoke-virtual {v2}, Ljava/io/InterruptedIOException;->getMessage()Ljava/lang/String;

    move-result-object v6

    invoke-virtual {v5, v6}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v5}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v5

    invoke-direct {v3, v4, v5, v2}, Lcom/helpshift/websockets/WebSocketException;-><init>(Lcom/helpshift/websockets/WebSocketError;Ljava/lang/String;Ljava/lang/Throwable;)V

    .line 251
    :goto_3
    instance-of v2, v3, Lcom/helpshift/websockets/NoMoreFrameException;

    const/4 v4, 0x1

    if-eqz v2, :cond_2

    .line 253
    iput-boolean v4, p0, Lcom/helpshift/websockets/ReadingThread;->mNotWaitForCloseFrame:Z

    .line 256
    iget-object v2, p0, Lcom/helpshift/websockets/ReadingThread;->mWebSocket:Lcom/helpshift/websockets/WebSocket;

    invoke-virtual {v2}, Lcom/helpshift/websockets/WebSocket;->isMissingCloseFrameAllowed()Z

    move-result v2

    if-eqz v2, :cond_2

    const/4 v4, 0x0

    :cond_2
    if-eqz v4, :cond_3

    .line 263
    invoke-direct {p0, v3}, Lcom/helpshift/websockets/ReadingThread;->callOnError(Lcom/helpshift/websockets/WebSocketException;)V

    .line 264
    iget-object v2, p0, Lcom/helpshift/websockets/ReadingThread;->mWebSocket:Lcom/helpshift/websockets/WebSocket;

    invoke-virtual {v2}, Lcom/helpshift/websockets/WebSocket;->getListenerManager()Lcom/helpshift/websockets/ListenerManager;

    move-result-object v2

    invoke-virtual {v2, v3, v1}, Lcom/helpshift/websockets/ListenerManager;->callOnFrameError(Lcom/helpshift/websockets/WebSocketException;Lcom/helpshift/websockets/WebSocketFrame;)V

    .line 268
    :cond_3
    invoke-direct {p0, v3}, Lcom/helpshift/websockets/ReadingThread;->createCloseFrame(Lcom/helpshift/websockets/WebSocketException;)Lcom/helpshift/websockets/WebSocketFrame;

    move-result-object v1

    .line 271
    iget-object v2, p0, Lcom/helpshift/websockets/ReadingThread;->mWebSocket:Lcom/helpshift/websockets/WebSocket;

    invoke-virtual {v2, v1}, Lcom/helpshift/websockets/WebSocket;->sendFrame(Lcom/helpshift/websockets/WebSocketFrame;)Lcom/helpshift/websockets/WebSocket;

    return-object v0
.end method

.method private scheduleClose()V
    .locals 2

    .line 928
    iget-object v0, p0, Lcom/helpshift/websockets/ReadingThread;->mCloseLock:Ljava/lang/Object;

    monitor-enter v0

    .line 929
    :try_start_0
    invoke-direct {p0}, Lcom/helpshift/websockets/ReadingThread;->cancelCloseTask()V

    .line 930
    invoke-direct {p0}, Lcom/helpshift/websockets/ReadingThread;->scheduleCloseTask()V

    .line 931
    monitor-exit v0

    return-void

    :catchall_0
    move-exception v1

    monitor-exit v0
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    throw v1
.end method

.method private scheduleCloseTask()V
    .locals 4

    .line 936
    new-instance v0, Lcom/helpshift/websockets/ReadingThread$CloseTask;

    invoke-direct {v0, p0}, Lcom/helpshift/websockets/ReadingThread$CloseTask;-><init>(Lcom/helpshift/websockets/ReadingThread;)V

    iput-object v0, p0, Lcom/helpshift/websockets/ReadingThread;->mCloseTask:Lcom/helpshift/websockets/ReadingThread$CloseTask;

    .line 937
    new-instance v0, Ljava/util/Timer;

    const-string v1, "ReadingThreadCloseTimer"

    invoke-direct {v0, v1}, Ljava/util/Timer;-><init>(Ljava/lang/String;)V

    iput-object v0, p0, Lcom/helpshift/websockets/ReadingThread;->mCloseTimer:Ljava/util/Timer;

    .line 938
    iget-object v0, p0, Lcom/helpshift/websockets/ReadingThread;->mCloseTimer:Ljava/util/Timer;

    iget-object v1, p0, Lcom/helpshift/websockets/ReadingThread;->mCloseTask:Lcom/helpshift/websockets/ReadingThread$CloseTask;

    iget-wide v2, p0, Lcom/helpshift/websockets/ReadingThread;->mCloseDelay:J

    invoke-virtual {v0, v1, v2, v3}, Ljava/util/Timer;->schedule(Ljava/util/TimerTask;J)V

    return-void
.end method

.method private verifyFrame(Lcom/helpshift/websockets/WebSocketFrame;)V
    .locals 0
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Lcom/helpshift/websockets/WebSocketException;
        }
    .end annotation

    .line 280
    invoke-direct {p0, p1}, Lcom/helpshift/websockets/ReadingThread;->verifyReservedBits(Lcom/helpshift/websockets/WebSocketFrame;)V

    .line 283
    invoke-direct {p0, p1}, Lcom/helpshift/websockets/ReadingThread;->verifyFrameOpcode(Lcom/helpshift/websockets/WebSocketFrame;)V

    .line 286
    invoke-direct {p0, p1}, Lcom/helpshift/websockets/ReadingThread;->verifyFrameMask(Lcom/helpshift/websockets/WebSocketFrame;)V

    .line 289
    invoke-direct {p0, p1}, Lcom/helpshift/websockets/ReadingThread;->verifyFrameFragmentation(Lcom/helpshift/websockets/WebSocketFrame;)V

    .line 292
    invoke-direct {p0, p1}, Lcom/helpshift/websockets/ReadingThread;->verifyFrameSize(Lcom/helpshift/websockets/WebSocketFrame;)V

    return-void
.end method

.method private verifyFrameFragmentation(Lcom/helpshift/websockets/WebSocketFrame;)V
    .locals 2
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Lcom/helpshift/websockets/WebSocketException;
        }
    .end annotation

    .line 452
    invoke-virtual {p1}, Lcom/helpshift/websockets/WebSocketFrame;->isControlFrame()Z

    move-result v0

    if-eqz v0, :cond_1

    .line 454
    invoke-virtual {p1}, Lcom/helpshift/websockets/WebSocketFrame;->getFin()Z

    move-result p1

    if-eqz p1, :cond_0

    return-void

    .line 456
    :cond_0
    new-instance p1, Lcom/helpshift/websockets/WebSocketException;

    sget-object v0, Lcom/helpshift/websockets/WebSocketError;->FRAGMENTED_CONTROL_FRAME:Lcom/helpshift/websockets/WebSocketError;

    const-string v1, "A control frame is fragmented."

    invoke-direct {p1, v0, v1}, Lcom/helpshift/websockets/WebSocketException;-><init>(Lcom/helpshift/websockets/WebSocketError;Ljava/lang/String;)V

    throw p1

    .line 466
    :cond_1
    iget-object v0, p0, Lcom/helpshift/websockets/ReadingThread;->mContinuation:Ljava/util/List;

    invoke-interface {v0}, Ljava/util/List;->size()I

    move-result v0

    if-eqz v0, :cond_2

    const/4 v0, 0x1

    goto :goto_0

    :cond_2
    const/4 v0, 0x0

    .line 469
    :goto_0
    invoke-virtual {p1}, Lcom/helpshift/websockets/WebSocketFrame;->isContinuationFrame()Z

    move-result p1

    if-eqz p1, :cond_4

    if-eqz v0, :cond_3

    return-void

    .line 473
    :cond_3
    new-instance p1, Lcom/helpshift/websockets/WebSocketException;

    sget-object v0, Lcom/helpshift/websockets/WebSocketError;->UNEXPECTED_CONTINUATION_FRAME:Lcom/helpshift/websockets/WebSocketError;

    const-string v1, "A continuation frame was detected although a continuation had not started."

    invoke-direct {p1, v0, v1}, Lcom/helpshift/websockets/WebSocketException;-><init>(Lcom/helpshift/websockets/WebSocketError;Ljava/lang/String;)V

    throw p1

    :cond_4
    if-nez v0, :cond_5

    return-void

    .line 486
    :cond_5
    new-instance p1, Lcom/helpshift/websockets/WebSocketException;

    sget-object v0, Lcom/helpshift/websockets/WebSocketError;->CONTINUATION_NOT_CLOSED:Lcom/helpshift/websockets/WebSocketError;

    const-string v1, "A non-control frame was detected although the existing continuation had not been closed."

    invoke-direct {p1, v0, v1}, Lcom/helpshift/websockets/WebSocketException;-><init>(Lcom/helpshift/websockets/WebSocketError;Ljava/lang/String;)V

    throw p1
.end method

.method private verifyFrameMask(Lcom/helpshift/websockets/WebSocketFrame;)V
    .locals 2
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Lcom/helpshift/websockets/WebSocketException;
        }
    .end annotation

    .line 439
    invoke-virtual {p1}, Lcom/helpshift/websockets/WebSocketFrame;->getMask()Z

    move-result p1

    if-nez p1, :cond_0

    return-void

    .line 441
    :cond_0
    new-instance p1, Lcom/helpshift/websockets/WebSocketException;

    sget-object v0, Lcom/helpshift/websockets/WebSocketError;->FRAME_MASKED:Lcom/helpshift/websockets/WebSocketError;

    const-string v1, "A frame from the server is masked."

    invoke-direct {p1, v0, v1}, Lcom/helpshift/websockets/WebSocketException;-><init>(Lcom/helpshift/websockets/WebSocketError;Ljava/lang/String;)V

    throw p1
.end method

.method private verifyFrameOpcode(Lcom/helpshift/websockets/WebSocketFrame;)V
    .locals 4
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Lcom/helpshift/websockets/WebSocketException;
        }
    .end annotation

    .line 399
    invoke-virtual {p1}, Lcom/helpshift/websockets/WebSocketFrame;->getOpcode()I

    move-result v0

    packed-switch v0, :pswitch_data_0

    packed-switch v0, :pswitch_data_1

    .line 414
    iget-object v0, p0, Lcom/helpshift/websockets/ReadingThread;->mWebSocket:Lcom/helpshift/websockets/WebSocket;

    invoke-virtual {v0}, Lcom/helpshift/websockets/WebSocket;->isExtended()Z

    move-result v0

    if-eqz v0, :cond_0

    return-void

    .line 420
    :cond_0
    new-instance v0, Lcom/helpshift/websockets/WebSocketException;

    sget-object v1, Lcom/helpshift/websockets/WebSocketError;->UNKNOWN_OPCODE:Lcom/helpshift/websockets/WebSocketError;

    new-instance v2, Ljava/lang/StringBuilder;

    invoke-direct {v2}, Ljava/lang/StringBuilder;-><init>()V

    const-string v3, "A frame has an unknown opcode: 0x"

    invoke-virtual {v2, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    .line 422
    invoke-virtual {p1}, Lcom/helpshift/websockets/WebSocketFrame;->getOpcode()I

    move-result p1

    invoke-static {p1}, Ljava/lang/Integer;->toHexString(I)Ljava/lang/String;

    move-result-object p1

    invoke-virtual {v2, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v2}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p1

    invoke-direct {v0, v1, p1}, Lcom/helpshift/websockets/WebSocketException;-><init>(Lcom/helpshift/websockets/WebSocketError;Ljava/lang/String;)V

    throw v0

    :pswitch_0
    return-void

    nop

    :pswitch_data_0
    .packed-switch 0x0
        :pswitch_0
        :pswitch_0
        :pswitch_0
    .end packed-switch

    :pswitch_data_1
    .packed-switch 0x8
        :pswitch_0
        :pswitch_0
        :pswitch_0
    .end packed-switch
.end method

.method private verifyFrameSize(Lcom/helpshift/websockets/WebSocketFrame;)V
    .locals 4
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Lcom/helpshift/websockets/WebSocketException;
        }
    .end annotation

    .line 495
    invoke-virtual {p1}, Lcom/helpshift/websockets/WebSocketFrame;->isControlFrame()Z

    move-result v0

    if-nez v0, :cond_0

    return-void

    .line 506
    :cond_0
    invoke-virtual {p1}, Lcom/helpshift/websockets/WebSocketFrame;->getPayload()[B

    move-result-object p1

    if-nez p1, :cond_1

    return-void

    :cond_1
    const/16 v0, 0x7d

    .line 513
    array-length v1, p1

    if-lt v0, v1, :cond_2

    return-void

    .line 515
    :cond_2
    new-instance v0, Lcom/helpshift/websockets/WebSocketException;

    sget-object v1, Lcom/helpshift/websockets/WebSocketError;->TOO_LONG_CONTROL_FRAME_PAYLOAD:Lcom/helpshift/websockets/WebSocketError;

    new-instance v2, Ljava/lang/StringBuilder;

    invoke-direct {v2}, Ljava/lang/StringBuilder;-><init>()V

    const-string v3, "The payload size of a control frame exceeds the maximum size (125 bytes): "

    invoke-virtual {v2, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    array-length p1, p1

    invoke-virtual {v2, p1}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    invoke-virtual {v2}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p1

    invoke-direct {v0, v1, p1}, Lcom/helpshift/websockets/WebSocketException;-><init>(Lcom/helpshift/websockets/WebSocketError;Ljava/lang/String;)V

    throw v0
.end method

.method private verifyReservedBit1(Lcom/helpshift/websockets/WebSocketFrame;)V
    .locals 2
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Lcom/helpshift/websockets/WebSocketException;
        }
    .end annotation

    .line 319
    iget-object v0, p0, Lcom/helpshift/websockets/ReadingThread;->mPMCE:Lcom/helpshift/websockets/PerMessageCompressionExtension;

    if-eqz v0, :cond_0

    .line 321
    invoke-direct {p0, p1}, Lcom/helpshift/websockets/ReadingThread;->verifyReservedBit1ForPMCE(Lcom/helpshift/websockets/WebSocketFrame;)Z

    move-result v0

    if-eqz v0, :cond_0

    return-void

    .line 328
    :cond_0
    invoke-virtual {p1}, Lcom/helpshift/websockets/WebSocketFrame;->getRsv1()Z

    move-result p1

    if-nez p1, :cond_1

    return-void

    .line 334
    :cond_1
    new-instance p1, Lcom/helpshift/websockets/WebSocketException;

    sget-object v0, Lcom/helpshift/websockets/WebSocketError;->UNEXPECTED_RESERVED_BIT:Lcom/helpshift/websockets/WebSocketError;

    const-string v1, "The RSV1 bit of a frame is set unexpectedly."

    invoke-direct {p1, v0, v1}, Lcom/helpshift/websockets/WebSocketException;-><init>(Lcom/helpshift/websockets/WebSocketError;Ljava/lang/String;)V

    throw p1
.end method

.method private verifyReservedBit1ForPMCE(Lcom/helpshift/websockets/WebSocketFrame;)Z
    .locals 1
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Lcom/helpshift/websockets/WebSocketException;
        }
    .end annotation

    .line 345
    invoke-virtual {p1}, Lcom/helpshift/websockets/WebSocketFrame;->isTextFrame()Z

    move-result v0

    if-nez v0, :cond_1

    invoke-virtual {p1}, Lcom/helpshift/websockets/WebSocketFrame;->isBinaryFrame()Z

    move-result p1

    if-eqz p1, :cond_0

    goto :goto_0

    :cond_0
    const/4 p1, 0x0

    return p1

    :cond_1
    :goto_0
    const/4 p1, 0x1

    return p1
.end method

.method private verifyReservedBit2(Lcom/helpshift/websockets/WebSocketFrame;)V
    .locals 2
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Lcom/helpshift/websockets/WebSocketException;
        }
    .end annotation

    .line 361
    invoke-virtual {p1}, Lcom/helpshift/websockets/WebSocketFrame;->getRsv2()Z

    move-result p1

    if-nez p1, :cond_0

    return-void

    .line 367
    :cond_0
    new-instance p1, Lcom/helpshift/websockets/WebSocketException;

    sget-object v0, Lcom/helpshift/websockets/WebSocketError;->UNEXPECTED_RESERVED_BIT:Lcom/helpshift/websockets/WebSocketError;

    const-string v1, "The RSV2 bit of a frame is set unexpectedly."

    invoke-direct {p1, v0, v1}, Lcom/helpshift/websockets/WebSocketException;-><init>(Lcom/helpshift/websockets/WebSocketError;Ljava/lang/String;)V

    throw p1
.end method

.method private verifyReservedBit3(Lcom/helpshift/websockets/WebSocketFrame;)V
    .locals 2
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Lcom/helpshift/websockets/WebSocketException;
        }
    .end annotation

    .line 376
    invoke-virtual {p1}, Lcom/helpshift/websockets/WebSocketFrame;->getRsv3()Z

    move-result p1

    if-nez p1, :cond_0

    return-void

    .line 382
    :cond_0
    new-instance p1, Lcom/helpshift/websockets/WebSocketException;

    sget-object v0, Lcom/helpshift/websockets/WebSocketError;->UNEXPECTED_RESERVED_BIT:Lcom/helpshift/websockets/WebSocketError;

    const-string v1, "The RSV3 bit of a frame is set unexpectedly."

    invoke-direct {p1, v0, v1}, Lcom/helpshift/websockets/WebSocketException;-><init>(Lcom/helpshift/websockets/WebSocketError;Ljava/lang/String;)V

    throw p1
.end method

.method private verifyReservedBits(Lcom/helpshift/websockets/WebSocketFrame;)V
    .locals 1
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Lcom/helpshift/websockets/WebSocketException;
        }
    .end annotation

    .line 298
    iget-object v0, p0, Lcom/helpshift/websockets/ReadingThread;->mWebSocket:Lcom/helpshift/websockets/WebSocket;

    invoke-virtual {v0}, Lcom/helpshift/websockets/WebSocket;->isExtended()Z

    move-result v0

    if-eqz v0, :cond_0

    return-void

    .line 308
    :cond_0
    invoke-direct {p0, p1}, Lcom/helpshift/websockets/ReadingThread;->verifyReservedBit1(Lcom/helpshift/websockets/WebSocketFrame;)V

    .line 309
    invoke-direct {p0, p1}, Lcom/helpshift/websockets/ReadingThread;->verifyReservedBit2(Lcom/helpshift/websockets/WebSocketFrame;)V

    .line 310
    invoke-direct {p0, p1}, Lcom/helpshift/websockets/ReadingThread;->verifyReservedBit3(Lcom/helpshift/websockets/WebSocketFrame;)V

    return-void
.end method

.method private waitForCloseFrame()V
    .locals 2

    .line 888
    iget-boolean v0, p0, Lcom/helpshift/websockets/ReadingThread;->mNotWaitForCloseFrame:Z

    if-eqz v0, :cond_0

    return-void

    .line 893
    :cond_0
    iget-object v0, p0, Lcom/helpshift/websockets/ReadingThread;->mCloseFrame:Lcom/helpshift/websockets/WebSocketFrame;

    if-eqz v0, :cond_1

    return-void

    .line 901
    :cond_1
    invoke-direct {p0}, Lcom/helpshift/websockets/ReadingThread;->scheduleClose()V

    .line 906
    :cond_2
    :try_start_0
    iget-object v0, p0, Lcom/helpshift/websockets/ReadingThread;->mWebSocket:Lcom/helpshift/websockets/WebSocket;

    invoke-virtual {v0}, Lcom/helpshift/websockets/WebSocket;->getInput()Lcom/helpshift/websockets/WebSocketInputStream;

    move-result-object v0

    invoke-virtual {v0}, Lcom/helpshift/websockets/WebSocketInputStream;->readFrame()Lcom/helpshift/websockets/WebSocketFrame;

    move-result-object v0
    :try_end_0
    .catch Ljava/lang/Throwable; {:try_start_0 .. :try_end_0} :catch_0

    .line 914
    invoke-virtual {v0}, Lcom/helpshift/websockets/WebSocketFrame;->isCloseFrame()Z

    move-result v1

    if-eqz v1, :cond_3

    .line 916
    iput-object v0, p0, Lcom/helpshift/websockets/ReadingThread;->mCloseFrame:Lcom/helpshift/websockets/WebSocketFrame;

    goto :goto_0

    .line 920
    :cond_3
    invoke-virtual {p0}, Lcom/helpshift/websockets/ReadingThread;->isInterrupted()Z

    move-result v0

    if-eqz v0, :cond_2

    :catch_0
    :goto_0
    return-void
.end method


# virtual methods
.method requestStop(J)V
    .locals 1

    .line 122
    monitor-enter p0

    .line 123
    :try_start_0
    iget-boolean v0, p0, Lcom/helpshift/websockets/ReadingThread;->mStopRequested:Z

    if-eqz v0, :cond_0

    .line 124
    monitor-exit p0

    return-void

    :cond_0
    const/4 v0, 0x1

    .line 127
    iput-boolean v0, p0, Lcom/helpshift/websockets/ReadingThread;->mStopRequested:Z

    .line 128
    monitor-exit p0
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    .line 134
    invoke-virtual {p0}, Lcom/helpshift/websockets/ReadingThread;->interrupt()V

    .line 144
    iput-wide p1, p0, Lcom/helpshift/websockets/ReadingThread;->mCloseDelay:J

    .line 145
    invoke-direct {p0}, Lcom/helpshift/websockets/ReadingThread;->scheduleClose()V

    return-void

    :catchall_0
    move-exception p1

    .line 128
    :try_start_1
    monitor-exit p0
    :try_end_1
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    throw p1
.end method

.method public runMain()V
    .locals 5

    .line 68
    :try_start_0
    invoke-direct {p0}, Lcom/helpshift/websockets/ReadingThread;->main()V
    :try_end_0
    .catch Ljava/lang/Throwable; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception v0

    .line 72
    new-instance v1, Lcom/helpshift/websockets/WebSocketException;

    sget-object v2, Lcom/helpshift/websockets/WebSocketError;->UNEXPECTED_ERROR_IN_READING_THREAD:Lcom/helpshift/websockets/WebSocketError;

    new-instance v3, Ljava/lang/StringBuilder;

    invoke-direct {v3}, Ljava/lang/StringBuilder;-><init>()V

    const-string v4, "An uncaught throwable was detected in the reading thread: "

    invoke-virtual {v3, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    .line 74
    invoke-virtual {v0}, Ljava/lang/Throwable;->getMessage()Ljava/lang/String;

    move-result-object v4

    invoke-virtual {v3, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v3}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v3

    invoke-direct {v1, v2, v3, v0}, Lcom/helpshift/websockets/WebSocketException;-><init>(Lcom/helpshift/websockets/WebSocketError;Ljava/lang/String;Ljava/lang/Throwable;)V

    .line 77
    iget-object v0, p0, Lcom/helpshift/websockets/ReadingThread;->mWebSocket:Lcom/helpshift/websockets/WebSocket;

    invoke-virtual {v0}, Lcom/helpshift/websockets/WebSocket;->getListenerManager()Lcom/helpshift/websockets/ListenerManager;

    move-result-object v0

    .line 78
    invoke-virtual {v0, v1}, Lcom/helpshift/websockets/ListenerManager;->callOnError(Lcom/helpshift/websockets/WebSocketException;)V

    .line 79
    invoke-virtual {v0, v1}, Lcom/helpshift/websockets/ListenerManager;->callOnUnexpectedError(Lcom/helpshift/websockets/WebSocketException;)V

    .line 83
    :goto_0
    iget-object v0, p0, Lcom/helpshift/websockets/ReadingThread;->mWebSocket:Lcom/helpshift/websockets/WebSocket;

    iget-object v1, p0, Lcom/helpshift/websockets/ReadingThread;->mCloseFrame:Lcom/helpshift/websockets/WebSocketFrame;

    invoke-virtual {v0, v1}, Lcom/helpshift/websockets/WebSocket;->onReadingThreadFinished(Lcom/helpshift/websockets/WebSocketFrame;)V

    return-void
.end method
