.class Lcom/helpshift/websockets/WebSocketInputStream;
.super Ljava/io/FilterInputStream;
.source "WebSocketInputStream.java"


# direct methods
.method public constructor <init>(Ljava/io/InputStream;)V
    .locals 0

    .line 32
    invoke-direct {p0, p1}, Ljava/io/FilterInputStream;-><init>(Ljava/io/InputStream;)V

    return-void
.end method

.method private readPayload(JZ[B)[B
    .locals 3
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;,
            Lcom/helpshift/websockets/WebSocketException;
        }
    .end annotation

    const-wide/16 v0, 0x0

    cmp-long v2, p1, v0

    if-nez v2, :cond_0

    const/4 p1, 0x0

    return-object p1

    :cond_0
    long-to-int v0, p1

    .line 187
    :try_start_0
    new-array v0, v0, [B
    :try_end_0
    .catch Ljava/lang/OutOfMemoryError; {:try_start_0 .. :try_end_0} :catch_0

    .line 200
    array-length p1, v0

    invoke-virtual {p0, v0, p1}, Lcom/helpshift/websockets/WebSocketInputStream;->readBytes([BI)V

    if-eqz p3, :cond_1

    .line 205
    invoke-static {p4, v0}, Lcom/helpshift/websockets/WebSocketFrame;->mask([B[B)[B

    :cond_1
    return-object v0

    :catch_0
    move-exception p3

    .line 192
    invoke-direct {p0, p1, p2}, Lcom/helpshift/websockets/WebSocketInputStream;->skipQuietly(J)V

    .line 193
    new-instance p1, Lcom/helpshift/websockets/WebSocketException;

    sget-object p2, Lcom/helpshift/websockets/WebSocketError;->INSUFFICIENT_MEMORY_FOR_PAYLOAD:Lcom/helpshift/websockets/WebSocketError;

    new-instance p4, Ljava/lang/StringBuilder;

    invoke-direct {p4}, Ljava/lang/StringBuilder;-><init>()V

    const-string v0, "OutOfMemoryError occurred during a trial to allocate a memory area for a frame\'s payload: "

    invoke-virtual {p4, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    .line 195
    invoke-virtual {p3}, Ljava/lang/OutOfMemoryError;->getMessage()Ljava/lang/String;

    move-result-object v0

    invoke-virtual {p4, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p4}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p4

    invoke-direct {p1, p2, p4, p3}, Lcom/helpshift/websockets/WebSocketException;-><init>(Lcom/helpshift/websockets/WebSocketError;Ljava/lang/String;Ljava/lang/Throwable;)V

    throw p1
.end method

.method private skipQuietly(J)V
    .locals 0

    .line 170
    :try_start_0
    invoke-virtual {p0, p1, p2}, Lcom/helpshift/websockets/WebSocketInputStream;->skip(J)J
    :try_end_0
    .catch Ljava/io/IOException; {:try_start_0 .. :try_end_0} :catch_0

    :catch_0
    return-void
.end method


# virtual methods
.method readBytes([BI)V
    .locals 2
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;,
            Lcom/helpshift/websockets/WebSocketException;
        }
    .end annotation

    const/4 v0, 0x0

    :goto_0
    if-ge v0, p2, :cond_1

    sub-int v1, p2, v0

    .line 156
    invoke-virtual {p0, p1, v0, v1}, Lcom/helpshift/websockets/WebSocketInputStream;->read([BII)I

    move-result v1

    if-lez v1, :cond_0

    add-int/2addr v0, v1

    goto :goto_0

    .line 160
    :cond_0
    new-instance p1, Lcom/helpshift/websockets/InsufficientDataException;

    invoke-direct {p1, p2, v0}, Lcom/helpshift/websockets/InsufficientDataException;-><init>(II)V

    throw p1

    :cond_1
    return-void
.end method

.method public readFrame()Lcom/helpshift/websockets/WebSocketFrame;
    .locals 18
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;,
            Lcom/helpshift/websockets/WebSocketException;
        }
    .end annotation

    move-object/from16 v1, p0

    const/16 v0, 0x8

    .line 43
    new-array v2, v0, [B

    const/4 v3, 0x2

    .line 47
    :try_start_0
    invoke-virtual {v1, v2, v3}, Lcom/helpshift/websockets/WebSocketInputStream;->readBytes([BI)V
    :try_end_0
    .catch Lcom/helpshift/websockets/InsufficientDataException; {:try_start_0 .. :try_end_0} :catch_0

    const/4 v4, 0x0

    .line 62
    aget-byte v5, v2, v4

    and-int/lit16 v5, v5, 0x80

    const/4 v6, 0x1

    if-eqz v5, :cond_0

    const/4 v5, 0x1

    goto :goto_0

    :cond_0
    const/4 v5, 0x0

    .line 65
    :goto_0
    aget-byte v7, v2, v4

    and-int/lit8 v7, v7, 0x40

    if-eqz v7, :cond_1

    const/4 v7, 0x1

    goto :goto_1

    :cond_1
    const/4 v7, 0x0

    .line 66
    :goto_1
    aget-byte v8, v2, v4

    and-int/lit8 v8, v8, 0x20

    if-eqz v8, :cond_2

    const/4 v8, 0x1

    goto :goto_2

    :cond_2
    const/4 v8, 0x0

    .line 67
    :goto_2
    aget-byte v9, v2, v4

    and-int/lit8 v9, v9, 0x10

    if-eqz v9, :cond_3

    const/4 v9, 0x1

    goto :goto_3

    :cond_3
    const/4 v9, 0x0

    .line 70
    :goto_3
    aget-byte v10, v2, v4

    and-int/lit8 v10, v10, 0xf

    .line 77
    aget-byte v11, v2, v6

    and-int/lit16 v11, v11, 0x80

    if-eqz v11, :cond_4

    const/4 v11, 0x1

    goto :goto_4

    :cond_4
    const/4 v11, 0x0

    .line 80
    :goto_4
    aget-byte v12, v2, v6

    and-int/lit8 v12, v12, 0x7f

    int-to-long v12, v12

    const-wide/16 v14, 0x7e

    cmp-long v16, v12, v14

    const/4 v14, 0x4

    if-nez v16, :cond_5

    .line 85
    invoke-virtual {v1, v2, v3}, Lcom/helpshift/websockets/WebSocketInputStream;->readBytes([BI)V

    .line 88
    aget-byte v3, v2, v4

    and-int/lit16 v3, v3, 0xff

    shl-int/lit8 v0, v3, 0x8

    aget-byte v2, v2, v6

    and-int/lit16 v2, v2, 0xff

    or-int/2addr v0, v2

    int-to-long v12, v0

    goto :goto_5

    :cond_5
    const-wide/16 v15, 0x7f

    cmp-long v17, v12, v15

    if-nez v17, :cond_7

    .line 94
    invoke-virtual {v1, v2, v0}, Lcom/helpshift/websockets/WebSocketInputStream;->readBytes([BI)V

    .line 100
    aget-byte v12, v2, v4

    and-int/lit16 v12, v12, 0x80

    if-nez v12, :cond_6

    .line 108
    aget-byte v4, v2, v4

    and-int/lit16 v4, v4, 0xff

    shl-int/lit8 v4, v4, 0x38

    aget-byte v6, v2, v6

    and-int/lit16 v6, v6, 0xff

    shl-int/lit8 v6, v6, 0x30

    or-int/2addr v4, v6

    aget-byte v3, v2, v3

    and-int/lit16 v3, v3, 0xff

    shl-int/lit8 v3, v3, 0x28

    or-int/2addr v3, v4

    const/4 v4, 0x3

    aget-byte v4, v2, v4

    and-int/lit16 v4, v4, 0xff

    shl-int/lit8 v4, v4, 0x20

    or-int/2addr v3, v4

    aget-byte v4, v2, v14

    and-int/lit16 v4, v4, 0xff

    shl-int/lit8 v4, v4, 0x18

    or-int/2addr v3, v4

    const/4 v4, 0x5

    aget-byte v4, v2, v4

    and-int/lit16 v4, v4, 0xff

    shl-int/lit8 v4, v4, 0x10

    or-int/2addr v3, v4

    const/4 v4, 0x6

    aget-byte v4, v2, v4

    and-int/lit16 v4, v4, 0xff

    shl-int/lit8 v0, v4, 0x8

    or-int/2addr v0, v3

    const/4 v3, 0x7

    aget-byte v2, v2, v3

    and-int/lit16 v2, v2, 0xff

    or-int/2addr v0, v2

    int-to-long v12, v0

    goto :goto_5

    .line 102
    :cond_6
    new-instance v0, Lcom/helpshift/websockets/WebSocketException;

    sget-object v2, Lcom/helpshift/websockets/WebSocketError;->INVALID_PAYLOAD_LENGTH:Lcom/helpshift/websockets/WebSocketError;

    const-string v3, "The payload length of a frame is invalid."

    invoke-direct {v0, v2, v3}, Lcom/helpshift/websockets/WebSocketException;-><init>(Lcom/helpshift/websockets/WebSocketError;Ljava/lang/String;)V

    throw v0

    :cond_7
    :goto_5
    const/4 v0, 0x0

    if-eqz v11, :cond_8

    .line 123
    new-array v0, v14, [B

    .line 124
    invoke-virtual {v1, v0, v14}, Lcom/helpshift/websockets/WebSocketInputStream;->readBytes([BI)V

    :cond_8
    const-wide/32 v2, 0x7fffffff

    cmp-long v4, v2, v12

    if-ltz v4, :cond_9

    .line 137
    invoke-direct {v1, v12, v13, v11, v0}, Lcom/helpshift/websockets/WebSocketInputStream;->readPayload(JZ[B)[B

    move-result-object v0

    .line 140
    new-instance v2, Lcom/helpshift/websockets/WebSocketFrame;

    invoke-direct {v2}, Lcom/helpshift/websockets/WebSocketFrame;-><init>()V

    .line 141
    invoke-virtual {v2, v5}, Lcom/helpshift/websockets/WebSocketFrame;->setFin(Z)Lcom/helpshift/websockets/WebSocketFrame;

    move-result-object v2

    .line 142
    invoke-virtual {v2, v7}, Lcom/helpshift/websockets/WebSocketFrame;->setRsv1(Z)Lcom/helpshift/websockets/WebSocketFrame;

    move-result-object v2

    .line 143
    invoke-virtual {v2, v8}, Lcom/helpshift/websockets/WebSocketFrame;->setRsv2(Z)Lcom/helpshift/websockets/WebSocketFrame;

    move-result-object v2

    .line 144
    invoke-virtual {v2, v9}, Lcom/helpshift/websockets/WebSocketFrame;->setRsv3(Z)Lcom/helpshift/websockets/WebSocketFrame;

    move-result-object v2

    .line 145
    invoke-virtual {v2, v10}, Lcom/helpshift/websockets/WebSocketFrame;->setOpcode(I)Lcom/helpshift/websockets/WebSocketFrame;

    move-result-object v2

    .line 146
    invoke-virtual {v2, v11}, Lcom/helpshift/websockets/WebSocketFrame;->setMask(Z)Lcom/helpshift/websockets/WebSocketFrame;

    move-result-object v2

    .line 147
    invoke-virtual {v2, v0}, Lcom/helpshift/websockets/WebSocketFrame;->setPayload([B)Lcom/helpshift/websockets/WebSocketFrame;

    move-result-object v0

    return-object v0

    .line 130
    :cond_9
    invoke-direct {v1, v12, v13}, Lcom/helpshift/websockets/WebSocketInputStream;->skipQuietly(J)V

    .line 131
    new-instance v0, Lcom/helpshift/websockets/WebSocketException;

    sget-object v2, Lcom/helpshift/websockets/WebSocketError;->TOO_LONG_PAYLOAD:Lcom/helpshift/websockets/WebSocketError;

    const-string v3, "The payload length of a frame exceeds the maximum array size in Java."

    invoke-direct {v0, v2, v3}, Lcom/helpshift/websockets/WebSocketException;-><init>(Lcom/helpshift/websockets/WebSocketError;Ljava/lang/String;)V

    throw v0

    :catch_0
    move-exception v0

    move-object v2, v0

    .line 50
    invoke-virtual {v2}, Lcom/helpshift/websockets/InsufficientDataException;->getReadByteCount()I

    move-result v0

    if-nez v0, :cond_a

    .line 53
    new-instance v0, Lcom/helpshift/websockets/NoMoreFrameException;

    invoke-direct {v0}, Lcom/helpshift/websockets/NoMoreFrameException;-><init>()V

    throw v0

    .line 57
    :cond_a
    throw v2
.end method

.method public readLine()Ljava/lang/String;
    .locals 1
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    const-string v0, "UTF-8"

    .line 37
    invoke-static {p0, v0}, Lcom/helpshift/websockets/Misc;->readLine(Ljava/io/InputStream;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v0

    return-object v0
.end method
