.class public Lcom/helpshift/websockets/WebSocketFrame;
.super Ljava/lang/Object;
.source "WebSocketFrame.java"


# instance fields
.field private mFin:Z

.field private mMask:Z

.field private mOpcode:I

.field private mPayload:[B

.field private mRsv1:Z

.field private mRsv2:Z

.field private mRsv3:Z


# direct methods
.method public constructor <init>()V
    .locals 0

    .line 43
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method

.method private appendPayloadBinary(Ljava/lang/StringBuilder;)V
    .locals 5

    .line 983
    invoke-direct {p0, p1}, Lcom/helpshift/websockets/WebSocketFrame;->appendPayloadCommon(Ljava/lang/StringBuilder;)Z

    move-result v0

    if-eqz v0, :cond_0

    return-void

    :cond_0
    const/4 v0, 0x0

    const/4 v1, 0x0

    .line 988
    :goto_0
    iget-object v2, p0, Lcom/helpshift/websockets/WebSocketFrame;->mPayload:[B

    array-length v2, v2

    const/4 v3, 0x1

    if-ge v1, v2, :cond_1

    const-string v2, "%02X "

    .line 989
    new-array v3, v3, [Ljava/lang/Object;

    iget-object v4, p0, Lcom/helpshift/websockets/WebSocketFrame;->mPayload:[B

    aget-byte v4, v4, v1

    and-int/lit16 v4, v4, 0xff

    invoke-static {v4}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v4

    aput-object v4, v3, v0

    invoke-static {v2, v3}, Ljava/lang/String;->format(Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v2

    invoke-virtual {p1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    add-int/lit8 v1, v1, 0x1

    goto :goto_0

    .line 992
    :cond_1
    iget-object v0, p0, Lcom/helpshift/websockets/WebSocketFrame;->mPayload:[B

    array-length v0, v0

    if-eqz v0, :cond_2

    .line 994
    invoke-virtual {p1}, Ljava/lang/StringBuilder;->length()I

    move-result v0

    sub-int/2addr v0, v3

    invoke-virtual {p1, v0}, Ljava/lang/StringBuilder;->setLength(I)V

    :cond_2
    return-void
.end method

.method private appendPayloadClose(Ljava/lang/StringBuilder;)V
    .locals 2

    const-string v0, ",CloseCode="

    .line 969
    invoke-virtual {p1, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p0}, Lcom/helpshift/websockets/WebSocketFrame;->getCloseCode()I

    move-result v0

    invoke-virtual {p1, v0}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    const-string v0, ",Reason="

    .line 970
    invoke-virtual {p1, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    .line 972
    invoke-virtual {p0}, Lcom/helpshift/websockets/WebSocketFrame;->getCloseReason()Ljava/lang/String;

    move-result-object v0

    if-nez v0, :cond_0

    const-string v0, "null"

    .line 975
    invoke-virtual {p1, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    goto :goto_0

    :cond_0
    const-string v1, "\""

    .line 978
    invoke-virtual {p1, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p1, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v0, "\""

    invoke-virtual {p1, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    :goto_0
    return-void
.end method

.method private appendPayloadCommon(Ljava/lang/StringBuilder;)Z
    .locals 2

    const-string v0, ",Payload="

    .line 934
    invoke-virtual {p1, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    .line 936
    iget-object v0, p0, Lcom/helpshift/websockets/WebSocketFrame;->mPayload:[B

    const/4 v1, 0x1

    if-nez v0, :cond_0

    const-string v0, "null"

    .line 937
    invoke-virtual {p1, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    return v1

    .line 943
    :cond_0
    iget-boolean v0, p0, Lcom/helpshift/websockets/WebSocketFrame;->mRsv1:Z

    if-eqz v0, :cond_1

    const-string v0, "compressed"

    .line 946
    invoke-virtual {p1, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    return v1

    :cond_1
    const/4 p1, 0x0

    return p1
.end method

.method private appendPayloadText(Ljava/lang/StringBuilder;)V
    .locals 1

    .line 957
    invoke-direct {p0, p1}, Lcom/helpshift/websockets/WebSocketFrame;->appendPayloadCommon(Ljava/lang/StringBuilder;)Z

    move-result v0

    if-eqz v0, :cond_0

    return-void

    :cond_0
    const-string v0, "\""

    .line 962
    invoke-virtual {p1, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    .line 963
    invoke-virtual {p0}, Lcom/helpshift/websockets/WebSocketFrame;->getPayloadText()Ljava/lang/String;

    move-result-object v0

    invoke-virtual {p1, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v0, "\""

    .line 964
    invoke-virtual {p1, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    return-void
.end method

.method private static compress([BLcom/helpshift/websockets/PerMessageCompressionExtension;)[B
    .locals 0

    .line 343
    :try_start_0
    invoke-virtual {p1, p0}, Lcom/helpshift/websockets/PerMessageCompressionExtension;->compress([B)[B

    move-result-object p1
    :try_end_0
    .catch Lcom/helpshift/websockets/WebSocketException; {:try_start_0 .. :try_end_0} :catch_0

    return-object p1

    :catch_0
    return-object p0
.end method

.method static compressFrame(Lcom/helpshift/websockets/WebSocketFrame;Lcom/helpshift/websockets/PerMessageCompressionExtension;)Lcom/helpshift/websockets/WebSocketFrame;
    .locals 2

    if-nez p1, :cond_0

    return-object p0

    .line 285
    :cond_0
    invoke-virtual {p0}, Lcom/helpshift/websockets/WebSocketFrame;->isTextFrame()Z

    move-result v0

    if-nez v0, :cond_1

    .line 286
    invoke-virtual {p0}, Lcom/helpshift/websockets/WebSocketFrame;->isBinaryFrame()Z

    move-result v0

    if-nez v0, :cond_1

    return-object p0

    .line 292
    :cond_1
    invoke-virtual {p0}, Lcom/helpshift/websockets/WebSocketFrame;->getFin()Z

    move-result v0

    if-nez v0, :cond_2

    return-object p0

    .line 300
    :cond_2
    invoke-virtual {p0}, Lcom/helpshift/websockets/WebSocketFrame;->getRsv1()Z

    move-result v0

    if-eqz v0, :cond_3

    return-object p0

    .line 309
    :cond_3
    invoke-virtual {p0}, Lcom/helpshift/websockets/WebSocketFrame;->getPayload()[B

    move-result-object v0

    if-eqz v0, :cond_5

    .line 312
    array-length v1, v0

    if-nez v1, :cond_4

    goto :goto_0

    .line 318
    :cond_4
    invoke-static {v0, p1}, Lcom/helpshift/websockets/WebSocketFrame;->compress([BLcom/helpshift/websockets/PerMessageCompressionExtension;)[B

    move-result-object p1

    .line 332
    invoke-virtual {p0, p1}, Lcom/helpshift/websockets/WebSocketFrame;->setPayload([B)Lcom/helpshift/websockets/WebSocketFrame;

    const/4 p1, 0x1

    .line 335
    invoke-virtual {p0, p1}, Lcom/helpshift/websockets/WebSocketFrame;->setRsv1(Z)Lcom/helpshift/websockets/WebSocketFrame;

    return-object p0

    :cond_5
    :goto_0
    return-object p0
.end method

.method public static createBinaryFrame([B)Lcom/helpshift/websockets/WebSocketFrame;
    .locals 2

    .line 115
    new-instance v0, Lcom/helpshift/websockets/WebSocketFrame;

    invoke-direct {v0}, Lcom/helpshift/websockets/WebSocketFrame;-><init>()V

    const/4 v1, 0x1

    .line 116
    invoke-virtual {v0, v1}, Lcom/helpshift/websockets/WebSocketFrame;->setFin(Z)Lcom/helpshift/websockets/WebSocketFrame;

    move-result-object v0

    const/4 v1, 0x2

    .line 117
    invoke-virtual {v0, v1}, Lcom/helpshift/websockets/WebSocketFrame;->setOpcode(I)Lcom/helpshift/websockets/WebSocketFrame;

    move-result-object v0

    .line 118
    invoke-virtual {v0, p0}, Lcom/helpshift/websockets/WebSocketFrame;->setPayload([B)Lcom/helpshift/websockets/WebSocketFrame;

    move-result-object p0

    return-object p0
.end method

.method public static createCloseFrame()Lcom/helpshift/websockets/WebSocketFrame;
    .locals 2

    .line 129
    new-instance v0, Lcom/helpshift/websockets/WebSocketFrame;

    invoke-direct {v0}, Lcom/helpshift/websockets/WebSocketFrame;-><init>()V

    const/4 v1, 0x1

    .line 130
    invoke-virtual {v0, v1}, Lcom/helpshift/websockets/WebSocketFrame;->setFin(Z)Lcom/helpshift/websockets/WebSocketFrame;

    move-result-object v0

    const/16 v1, 0x8

    .line 131
    invoke-virtual {v0, v1}, Lcom/helpshift/websockets/WebSocketFrame;->setOpcode(I)Lcom/helpshift/websockets/WebSocketFrame;

    move-result-object v0

    return-object v0
.end method

.method public static createCloseFrame(I)Lcom/helpshift/websockets/WebSocketFrame;
    .locals 2

    .line 144
    invoke-static {}, Lcom/helpshift/websockets/WebSocketFrame;->createCloseFrame()Lcom/helpshift/websockets/WebSocketFrame;

    move-result-object v0

    const/4 v1, 0x0

    invoke-virtual {v0, p0, v1}, Lcom/helpshift/websockets/WebSocketFrame;->setCloseFramePayload(ILjava/lang/String;)Lcom/helpshift/websockets/WebSocketFrame;

    move-result-object p0

    return-object p0
.end method

.method public static createCloseFrame(ILjava/lang/String;)Lcom/helpshift/websockets/WebSocketFrame;
    .locals 1

    .line 161
    invoke-static {}, Lcom/helpshift/websockets/WebSocketFrame;->createCloseFrame()Lcom/helpshift/websockets/WebSocketFrame;

    move-result-object v0

    invoke-virtual {v0, p0, p1}, Lcom/helpshift/websockets/WebSocketFrame;->setCloseFramePayload(ILjava/lang/String;)Lcom/helpshift/websockets/WebSocketFrame;

    move-result-object p0

    return-object p0
.end method

.method public static createContinuationFrame()Lcom/helpshift/websockets/WebSocketFrame;
    .locals 2

    .line 61
    new-instance v0, Lcom/helpshift/websockets/WebSocketFrame;

    invoke-direct {v0}, Lcom/helpshift/websockets/WebSocketFrame;-><init>()V

    const/4 v1, 0x0

    .line 62
    invoke-virtual {v0, v1}, Lcom/helpshift/websockets/WebSocketFrame;->setOpcode(I)Lcom/helpshift/websockets/WebSocketFrame;

    move-result-object v0

    return-object v0
.end method

.method public static createContinuationFrame(Ljava/lang/String;)Lcom/helpshift/websockets/WebSocketFrame;
    .locals 1

    .line 88
    invoke-static {}, Lcom/helpshift/websockets/WebSocketFrame;->createContinuationFrame()Lcom/helpshift/websockets/WebSocketFrame;

    move-result-object v0

    invoke-virtual {v0, p0}, Lcom/helpshift/websockets/WebSocketFrame;->setPayload(Ljava/lang/String;)Lcom/helpshift/websockets/WebSocketFrame;

    move-result-object p0

    return-object p0
.end method

.method public static createContinuationFrame([B)Lcom/helpshift/websockets/WebSocketFrame;
    .locals 1

    .line 75
    invoke-static {}, Lcom/helpshift/websockets/WebSocketFrame;->createContinuationFrame()Lcom/helpshift/websockets/WebSocketFrame;

    move-result-object v0

    invoke-virtual {v0, p0}, Lcom/helpshift/websockets/WebSocketFrame;->setPayload([B)Lcom/helpshift/websockets/WebSocketFrame;

    move-result-object p0

    return-object p0
.end method

.method public static createPingFrame()Lcom/helpshift/websockets/WebSocketFrame;
    .locals 2

    .line 172
    new-instance v0, Lcom/helpshift/websockets/WebSocketFrame;

    invoke-direct {v0}, Lcom/helpshift/websockets/WebSocketFrame;-><init>()V

    const/4 v1, 0x1

    .line 173
    invoke-virtual {v0, v1}, Lcom/helpshift/websockets/WebSocketFrame;->setFin(Z)Lcom/helpshift/websockets/WebSocketFrame;

    move-result-object v0

    const/16 v1, 0x9

    .line 174
    invoke-virtual {v0, v1}, Lcom/helpshift/websockets/WebSocketFrame;->setOpcode(I)Lcom/helpshift/websockets/WebSocketFrame;

    move-result-object v0

    return-object v0
.end method

.method public static createPingFrame(Ljava/lang/String;)Lcom/helpshift/websockets/WebSocketFrame;
    .locals 1

    .line 204
    invoke-static {}, Lcom/helpshift/websockets/WebSocketFrame;->createPingFrame()Lcom/helpshift/websockets/WebSocketFrame;

    move-result-object v0

    invoke-virtual {v0, p0}, Lcom/helpshift/websockets/WebSocketFrame;->setPayload(Ljava/lang/String;)Lcom/helpshift/websockets/WebSocketFrame;

    move-result-object p0

    return-object p0
.end method

.method public static createPingFrame([B)Lcom/helpshift/websockets/WebSocketFrame;
    .locals 1

    .line 189
    invoke-static {}, Lcom/helpshift/websockets/WebSocketFrame;->createPingFrame()Lcom/helpshift/websockets/WebSocketFrame;

    move-result-object v0

    invoke-virtual {v0, p0}, Lcom/helpshift/websockets/WebSocketFrame;->setPayload([B)Lcom/helpshift/websockets/WebSocketFrame;

    move-result-object p0

    return-object p0
.end method

.method public static createPongFrame()Lcom/helpshift/websockets/WebSocketFrame;
    .locals 2

    .line 215
    new-instance v0, Lcom/helpshift/websockets/WebSocketFrame;

    invoke-direct {v0}, Lcom/helpshift/websockets/WebSocketFrame;-><init>()V

    const/4 v1, 0x1

    .line 216
    invoke-virtual {v0, v1}, Lcom/helpshift/websockets/WebSocketFrame;->setFin(Z)Lcom/helpshift/websockets/WebSocketFrame;

    move-result-object v0

    const/16 v1, 0xa

    .line 217
    invoke-virtual {v0, v1}, Lcom/helpshift/websockets/WebSocketFrame;->setOpcode(I)Lcom/helpshift/websockets/WebSocketFrame;

    move-result-object v0

    return-object v0
.end method

.method public static createPongFrame(Ljava/lang/String;)Lcom/helpshift/websockets/WebSocketFrame;
    .locals 1

    .line 247
    invoke-static {}, Lcom/helpshift/websockets/WebSocketFrame;->createPongFrame()Lcom/helpshift/websockets/WebSocketFrame;

    move-result-object v0

    invoke-virtual {v0, p0}, Lcom/helpshift/websockets/WebSocketFrame;->setPayload(Ljava/lang/String;)Lcom/helpshift/websockets/WebSocketFrame;

    move-result-object p0

    return-object p0
.end method

.method public static createPongFrame([B)Lcom/helpshift/websockets/WebSocketFrame;
    .locals 1

    .line 232
    invoke-static {}, Lcom/helpshift/websockets/WebSocketFrame;->createPongFrame()Lcom/helpshift/websockets/WebSocketFrame;

    move-result-object v0

    invoke-virtual {v0, p0}, Lcom/helpshift/websockets/WebSocketFrame;->setPayload([B)Lcom/helpshift/websockets/WebSocketFrame;

    move-result-object p0

    return-object p0
.end method

.method public static createTextFrame(Ljava/lang/String;)Lcom/helpshift/websockets/WebSocketFrame;
    .locals 2

    .line 100
    new-instance v0, Lcom/helpshift/websockets/WebSocketFrame;

    invoke-direct {v0}, Lcom/helpshift/websockets/WebSocketFrame;-><init>()V

    const/4 v1, 0x1

    .line 101
    invoke-virtual {v0, v1}, Lcom/helpshift/websockets/WebSocketFrame;->setFin(Z)Lcom/helpshift/websockets/WebSocketFrame;

    move-result-object v0

    .line 102
    invoke-virtual {v0, v1}, Lcom/helpshift/websockets/WebSocketFrame;->setOpcode(I)Lcom/helpshift/websockets/WebSocketFrame;

    move-result-object v0

    .line 103
    invoke-virtual {v0, p0}, Lcom/helpshift/websockets/WebSocketFrame;->setPayload(Ljava/lang/String;)Lcom/helpshift/websockets/WebSocketFrame;

    move-result-object p0

    return-object p0
.end method

.method static mask([B[B)[B
    .locals 3

    if-eqz p0, :cond_2

    .line 266
    array-length v0, p0

    const/4 v1, 0x4

    if-lt v0, v1, :cond_2

    if-nez p1, :cond_0

    goto :goto_1

    :cond_0
    const/4 v0, 0x0

    .line 270
    :goto_0
    array-length v1, p1

    if-ge v0, v1, :cond_1

    .line 271
    aget-byte v1, p1, v0

    rem-int/lit8 v2, v0, 0x4

    aget-byte v2, p0, v2

    xor-int/2addr v1, v2

    int-to-byte v1, v1

    aput-byte v1, p1, v0

    add-int/lit8 v0, v0, 0x1

    goto :goto_0

    :cond_1
    return-object p1

    :cond_2
    :goto_1
    return-object p1
.end method

.method private static split(Lcom/helpshift/websockets/WebSocketFrame;I)Ljava/util/List;
    .locals 5
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Lcom/helpshift/websockets/WebSocketFrame;",
            "I)",
            "Ljava/util/List<",
            "Lcom/helpshift/websockets/WebSocketFrame;",
            ">;"
        }
    .end annotation

    .line 394
    invoke-virtual {p0}, Lcom/helpshift/websockets/WebSocketFrame;->getPayload()[B

    move-result-object v0

    .line 395
    invoke-virtual {p0}, Lcom/helpshift/websockets/WebSocketFrame;->getFin()Z

    move-result v1

    .line 397
    new-instance v2, Ljava/util/ArrayList;

    invoke-direct {v2}, Ljava/util/ArrayList;-><init>()V

    .line 401
    invoke-static {v0, p1}, Ljava/util/Arrays;->copyOf([BI)[B

    move-result-object v3

    const/4 v4, 0x0

    .line 402
    invoke-virtual {p0, v4}, Lcom/helpshift/websockets/WebSocketFrame;->setFin(Z)Lcom/helpshift/websockets/WebSocketFrame;

    move-result-object v4

    invoke-virtual {v4, v3}, Lcom/helpshift/websockets/WebSocketFrame;->setPayload([B)Lcom/helpshift/websockets/WebSocketFrame;

    .line 403
    invoke-interface {v2, p0}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    move p0, p1

    .line 405
    :goto_0
    array-length v3, v0

    if-ge p0, v3, :cond_0

    add-int v3, p0, p1

    .line 407
    array-length v4, v0

    invoke-static {v3, v4}, Ljava/lang/Math;->min(II)I

    move-result v4

    .line 408
    invoke-static {v0, p0, v4}, Ljava/util/Arrays;->copyOfRange([BII)[B

    move-result-object p0

    .line 411
    invoke-static {p0}, Lcom/helpshift/websockets/WebSocketFrame;->createContinuationFrame([B)Lcom/helpshift/websockets/WebSocketFrame;

    move-result-object p0

    .line 412
    invoke-interface {v2, p0}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    move p0, v3

    goto :goto_0

    :cond_0
    if-eqz v1, :cond_1

    .line 417
    invoke-interface {v2}, Ljava/util/List;->size()I

    move-result p0

    const/4 p1, 0x1

    sub-int/2addr p0, p1

    invoke-interface {v2, p0}, Ljava/util/List;->get(I)Ljava/lang/Object;

    move-result-object p0

    check-cast p0, Lcom/helpshift/websockets/WebSocketFrame;

    invoke-virtual {p0, p1}, Lcom/helpshift/websockets/WebSocketFrame;->setFin(Z)Lcom/helpshift/websockets/WebSocketFrame;

    :cond_1
    return-object v2
.end method

.method static splitIfNecessary(Lcom/helpshift/websockets/WebSocketFrame;ILcom/helpshift/websockets/PerMessageCompressionExtension;)Ljava/util/List;
    .locals 2
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Lcom/helpshift/websockets/WebSocketFrame;",
            "I",
            "Lcom/helpshift/websockets/PerMessageCompressionExtension;",
            ")",
            "Ljava/util/List<",
            "Lcom/helpshift/websockets/WebSocketFrame;",
            ">;"
        }
    .end annotation

    const/4 v0, 0x0

    if-nez p1, :cond_0

    return-object v0

    .line 363
    :cond_0
    invoke-virtual {p0}, Lcom/helpshift/websockets/WebSocketFrame;->getPayloadLength()I

    move-result v1

    if-gt v1, p1, :cond_1

    return-object v0

    .line 369
    :cond_1
    invoke-virtual {p0}, Lcom/helpshift/websockets/WebSocketFrame;->isBinaryFrame()Z

    move-result v1

    if-nez v1, :cond_3

    invoke-virtual {p0}, Lcom/helpshift/websockets/WebSocketFrame;->isTextFrame()Z

    move-result v1

    if-eqz v1, :cond_2

    goto :goto_0

    .line 383
    :cond_2
    invoke-virtual {p0}, Lcom/helpshift/websockets/WebSocketFrame;->isContinuationFrame()Z

    move-result p2

    if-nez p2, :cond_4

    return-object v0

    .line 374
    :cond_3
    :goto_0
    invoke-static {p0, p2}, Lcom/helpshift/websockets/WebSocketFrame;->compressFrame(Lcom/helpshift/websockets/WebSocketFrame;Lcom/helpshift/websockets/PerMessageCompressionExtension;)Lcom/helpshift/websockets/WebSocketFrame;

    move-result-object p0

    .line 378
    invoke-virtual {p0}, Lcom/helpshift/websockets/WebSocketFrame;->getPayloadLength()I

    move-result p2

    if-gt p2, p1, :cond_4

    return-object v0

    .line 389
    :cond_4
    invoke-static {p0, p1}, Lcom/helpshift/websockets/WebSocketFrame;->split(Lcom/helpshift/websockets/WebSocketFrame;I)Ljava/util/List;

    move-result-object p0

    return-object p0
.end method


# virtual methods
.method public getCloseCode()I
    .locals 3

    .line 873
    iget-object v0, p0, Lcom/helpshift/websockets/WebSocketFrame;->mPayload:[B

    if-eqz v0, :cond_1

    iget-object v0, p0, Lcom/helpshift/websockets/WebSocketFrame;->mPayload:[B

    array-length v0, v0

    const/4 v1, 0x2

    if-ge v0, v1, :cond_0

    goto :goto_0

    .line 878
    :cond_0
    iget-object v0, p0, Lcom/helpshift/websockets/WebSocketFrame;->mPayload:[B

    const/4 v1, 0x0

    aget-byte v0, v0, v1

    and-int/lit16 v0, v0, 0xff

    shl-int/lit8 v0, v0, 0x8

    iget-object v1, p0, Lcom/helpshift/websockets/WebSocketFrame;->mPayload:[B

    const/4 v2, 0x1

    aget-byte v1, v1, v2

    and-int/lit16 v1, v1, 0xff

    or-int/2addr v0, v1

    return v0

    :cond_1
    :goto_0
    const/16 v0, 0x3ed

    return v0
.end method

.method public getCloseReason()Ljava/lang/String;
    .locals 3

    .line 899
    iget-object v0, p0, Lcom/helpshift/websockets/WebSocketFrame;->mPayload:[B

    if-eqz v0, :cond_1

    iget-object v0, p0, Lcom/helpshift/websockets/WebSocketFrame;->mPayload:[B

    array-length v0, v0

    const/4 v1, 0x3

    if-ge v0, v1, :cond_0

    goto :goto_0

    .line 903
    :cond_0
    iget-object v0, p0, Lcom/helpshift/websockets/WebSocketFrame;->mPayload:[B

    iget-object v1, p0, Lcom/helpshift/websockets/WebSocketFrame;->mPayload:[B

    array-length v1, v1

    const/4 v2, 0x2

    sub-int/2addr v1, v2

    invoke-static {v0, v2, v1}, Lcom/helpshift/websockets/Misc;->toStringUTF8([BII)Ljava/lang/String;

    move-result-object v0

    return-object v0

    :cond_1
    :goto_0
    const/4 v0, 0x0

    return-object v0
.end method

.method public getFin()Z
    .locals 1

    .line 429
    iget-boolean v0, p0, Lcom/helpshift/websockets/WebSocketFrame;->mFin:Z

    return v0
.end method

.method getMask()Z
    .locals 1

    .line 700
    iget-boolean v0, p0, Lcom/helpshift/websockets/WebSocketFrame;->mMask:Z

    return v0
.end method

.method public getOpcode()I
    .locals 1

    .line 558
    iget v0, p0, Lcom/helpshift/websockets/WebSocketFrame;->mOpcode:I

    return v0
.end method

.method public getPayload()[B
    .locals 1

    .line 743
    iget-object v0, p0, Lcom/helpshift/websockets/WebSocketFrame;->mPayload:[B

    return-object v0
.end method

.method public getPayloadLength()I
    .locals 1

    .line 730
    iget-object v0, p0, Lcom/helpshift/websockets/WebSocketFrame;->mPayload:[B

    if-nez v0, :cond_0

    const/4 v0, 0x0

    return v0

    .line 734
    :cond_0
    iget-object v0, p0, Lcom/helpshift/websockets/WebSocketFrame;->mPayload:[B

    array-length v0, v0

    return v0
.end method

.method public getPayloadText()Ljava/lang/String;
    .locals 1

    .line 775
    iget-object v0, p0, Lcom/helpshift/websockets/WebSocketFrame;->mPayload:[B

    if-nez v0, :cond_0

    const/4 v0, 0x0

    return-object v0

    .line 779
    :cond_0
    iget-object v0, p0, Lcom/helpshift/websockets/WebSocketFrame;->mPayload:[B

    invoke-static {v0}, Lcom/helpshift/websockets/Misc;->toStringUTF8([B)Ljava/lang/String;

    move-result-object v0

    return-object v0
.end method

.method public getRsv1()Z
    .locals 1

    .line 450
    iget-boolean v0, p0, Lcom/helpshift/websockets/WebSocketFrame;->mRsv1:Z

    return v0
.end method

.method public getRsv2()Z
    .locals 1

    .line 471
    iget-boolean v0, p0, Lcom/helpshift/websockets/WebSocketFrame;->mRsv2:Z

    return v0
.end method

.method public getRsv3()Z
    .locals 1

    .line 492
    iget-boolean v0, p0, Lcom/helpshift/websockets/WebSocketFrame;->mRsv3:Z

    return v0
.end method

.method public hasPayload()Z
    .locals 1

    .line 721
    iget-object v0, p0, Lcom/helpshift/websockets/WebSocketFrame;->mPayload:[B

    if-eqz v0, :cond_0

    const/4 v0, 0x1

    goto :goto_0

    :cond_0
    const/4 v0, 0x0

    :goto_0
    return v0
.end method

.method public isBinaryFrame()Z
    .locals 2

    .line 616
    iget v0, p0, Lcom/helpshift/websockets/WebSocketFrame;->mOpcode:I

    const/4 v1, 0x2

    if-ne v0, v1, :cond_0

    const/4 v0, 0x1

    goto :goto_0

    :cond_0
    const/4 v0, 0x0

    :goto_0
    return v0
.end method

.method public isCloseFrame()Z
    .locals 2

    .line 631
    iget v0, p0, Lcom/helpshift/websockets/WebSocketFrame;->mOpcode:I

    const/16 v1, 0x8

    if-ne v0, v1, :cond_0

    const/4 v0, 0x1

    goto :goto_0

    :cond_0
    const/4 v0, 0x0

    :goto_0
    return v0
.end method

.method public isContinuationFrame()Z
    .locals 1

    .line 586
    iget v0, p0, Lcom/helpshift/websockets/WebSocketFrame;->mOpcode:I

    if-nez v0, :cond_0

    const/4 v0, 0x1

    goto :goto_0

    :cond_0
    const/4 v0, 0x0

    :goto_0
    return v0
.end method

.method public isControlFrame()Z
    .locals 2

    .line 691
    iget v0, p0, Lcom/helpshift/websockets/WebSocketFrame;->mOpcode:I

    const/16 v1, 0x8

    if-gt v1, v0, :cond_0

    iget v0, p0, Lcom/helpshift/websockets/WebSocketFrame;->mOpcode:I

    const/16 v1, 0xf

    if-gt v0, v1, :cond_0

    const/4 v0, 0x1

    goto :goto_0

    :cond_0
    const/4 v0, 0x0

    :goto_0
    return v0
.end method

.method public isDataFrame()Z
    .locals 3

    .line 676
    iget v0, p0, Lcom/helpshift/websockets/WebSocketFrame;->mOpcode:I

    const/4 v1, 0x1

    if-gt v1, v0, :cond_0

    iget v0, p0, Lcom/helpshift/websockets/WebSocketFrame;->mOpcode:I

    const/4 v2, 0x7

    if-gt v0, v2, :cond_0

    goto :goto_0

    :cond_0
    const/4 v1, 0x0

    :goto_0
    return v1
.end method

.method public isPingFrame()Z
    .locals 2

    .line 646
    iget v0, p0, Lcom/helpshift/websockets/WebSocketFrame;->mOpcode:I

    const/16 v1, 0x9

    if-ne v0, v1, :cond_0

    const/4 v0, 0x1

    goto :goto_0

    :cond_0
    const/4 v0, 0x0

    :goto_0
    return v0
.end method

.method public isPongFrame()Z
    .locals 2

    .line 661
    iget v0, p0, Lcom/helpshift/websockets/WebSocketFrame;->mOpcode:I

    const/16 v1, 0xa

    if-ne v0, v1, :cond_0

    const/4 v0, 0x1

    goto :goto_0

    :cond_0
    const/4 v0, 0x0

    :goto_0
    return v0
.end method

.method public isTextFrame()Z
    .locals 2

    .line 601
    iget v0, p0, Lcom/helpshift/websockets/WebSocketFrame;->mOpcode:I

    const/4 v1, 0x1

    if-ne v0, v1, :cond_0

    goto :goto_0

    :cond_0
    const/4 v1, 0x0

    :goto_0
    return v1
.end method

.method public setCloseFramePayload(ILjava/lang/String;)Lcom/helpshift/websockets/WebSocketFrame;
    .locals 4

    const/4 v0, 0x2

    .line 831
    new-array v1, v0, [B

    shr-int/lit8 v2, p1, 0x8

    and-int/lit16 v2, v2, 0xff

    int-to-byte v2, v2

    const/4 v3, 0x0

    aput-byte v2, v1, v3

    and-int/lit16 p1, p1, 0xff

    int-to-byte p1, p1

    const/4 v2, 0x1

    aput-byte p1, v1, v2

    if-eqz p2, :cond_1

    .line 837
    invoke-virtual {p2}, Ljava/lang/String;->length()I

    move-result p1

    if-nez p1, :cond_0

    goto :goto_0

    .line 843
    :cond_0
    invoke-static {p2}, Lcom/helpshift/websockets/Misc;->getBytesUTF8(Ljava/lang/String;)[B

    move-result-object p1

    .line 846
    array-length p2, p1

    add-int/2addr p2, v0

    new-array p2, p2, [B

    .line 847
    invoke-static {v1, v3, p2, v3, v0}, Ljava/lang/System;->arraycopy(Ljava/lang/Object;ILjava/lang/Object;II)V

    .line 848
    array-length v1, p1

    invoke-static {p1, v3, p2, v0, v1}, Ljava/lang/System;->arraycopy(Ljava/lang/Object;ILjava/lang/Object;II)V

    .line 851
    invoke-virtual {p0, p2}, Lcom/helpshift/websockets/WebSocketFrame;->setPayload([B)Lcom/helpshift/websockets/WebSocketFrame;

    move-result-object p1

    return-object p1

    .line 839
    :cond_1
    :goto_0
    invoke-virtual {p0, v1}, Lcom/helpshift/websockets/WebSocketFrame;->setPayload([B)Lcom/helpshift/websockets/WebSocketFrame;

    move-result-object p1

    return-object p1
.end method

.method public setFin(Z)Lcom/helpshift/websockets/WebSocketFrame;
    .locals 0

    .line 439
    iput-boolean p1, p0, Lcom/helpshift/websockets/WebSocketFrame;->mFin:Z

    return-object p0
.end method

.method setMask(Z)Lcom/helpshift/websockets/WebSocketFrame;
    .locals 0

    .line 710
    iput-boolean p1, p0, Lcom/helpshift/websockets/WebSocketFrame;->mMask:Z

    return-object p0
.end method

.method public setOpcode(I)Lcom/helpshift/websockets/WebSocketFrame;
    .locals 0

    .line 569
    iput p1, p0, Lcom/helpshift/websockets/WebSocketFrame;->mOpcode:I

    return-object p0
.end method

.method public setPayload(Ljava/lang/String;)Lcom/helpshift/websockets/WebSocketFrame;
    .locals 1

    if-eqz p1, :cond_1

    .line 761
    invoke-virtual {p1}, Ljava/lang/String;->length()I

    move-result v0

    if-nez v0, :cond_0

    goto :goto_0

    .line 765
    :cond_0
    invoke-static {p1}, Lcom/helpshift/websockets/Misc;->getBytesUTF8(Ljava/lang/String;)[B

    move-result-object p1

    invoke-virtual {p0, p1}, Lcom/helpshift/websockets/WebSocketFrame;->setPayload([B)Lcom/helpshift/websockets/WebSocketFrame;

    move-result-object p1

    return-object p1

    :cond_1
    :goto_0
    const/4 p1, 0x0

    .line 762
    check-cast p1, [B

    invoke-virtual {p0, p1}, Lcom/helpshift/websockets/WebSocketFrame;->setPayload([B)Lcom/helpshift/websockets/WebSocketFrame;

    move-result-object p1

    return-object p1
.end method

.method public setPayload([B)Lcom/helpshift/websockets/WebSocketFrame;
    .locals 1

    if-eqz p1, :cond_0

    .line 796
    array-length v0, p1

    if-nez v0, :cond_0

    const/4 p1, 0x0

    .line 800
    :cond_0
    iput-object p1, p0, Lcom/helpshift/websockets/WebSocketFrame;->mPayload:[B

    return-object p0
.end method

.method public setRsv1(Z)Lcom/helpshift/websockets/WebSocketFrame;
    .locals 0

    .line 460
    iput-boolean p1, p0, Lcom/helpshift/websockets/WebSocketFrame;->mRsv1:Z

    return-object p0
.end method

.method public setRsv2(Z)Lcom/helpshift/websockets/WebSocketFrame;
    .locals 0

    .line 481
    iput-boolean p1, p0, Lcom/helpshift/websockets/WebSocketFrame;->mRsv2:Z

    return-object p0
.end method

.method public setRsv3(Z)Lcom/helpshift/websockets/WebSocketFrame;
    .locals 0

    .line 502
    iput-boolean p1, p0, Lcom/helpshift/websockets/WebSocketFrame;->mRsv3:Z

    return-object p0
.end method

.method public toString()Ljava/lang/String;
    .locals 3

    .line 908
    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    const-string v1, "WebSocketFrame(FIN="

    .line 909
    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-boolean v1, p0, Lcom/helpshift/websockets/WebSocketFrame;->mFin:Z

    if-eqz v1, :cond_0

    const-string v1, "1"

    goto :goto_0

    :cond_0
    const-string v1, "0"

    :goto_0
    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, ",RSV1="

    .line 910
    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-boolean v1, p0, Lcom/helpshift/websockets/WebSocketFrame;->mRsv1:Z

    if-eqz v1, :cond_1

    const-string v1, "1"

    goto :goto_1

    :cond_1
    const-string v1, "0"

    :goto_1
    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, ",RSV2="

    .line 911
    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-boolean v1, p0, Lcom/helpshift/websockets/WebSocketFrame;->mRsv2:Z

    if-eqz v1, :cond_2

    const-string v1, "1"

    goto :goto_2

    :cond_2
    const-string v1, "0"

    :goto_2
    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, ",RSV3="

    .line 912
    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-boolean v1, p0, Lcom/helpshift/websockets/WebSocketFrame;->mRsv3:Z

    if-eqz v1, :cond_3

    const-string v1, "1"

    goto :goto_3

    :cond_3
    const-string v1, "0"

    :goto_3
    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, ",Opcode="

    .line 913
    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget v1, p0, Lcom/helpshift/websockets/WebSocketFrame;->mOpcode:I

    invoke-static {v1}, Lcom/helpshift/websockets/Misc;->toOpcodeName(I)Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, ",Length="

    .line 914
    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p0}, Lcom/helpshift/websockets/WebSocketFrame;->getPayloadLength()I

    move-result v1

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    .line 916
    iget v1, p0, Lcom/helpshift/websockets/WebSocketFrame;->mOpcode:I

    const/16 v2, 0x8

    if-eq v1, v2, :cond_4

    packed-switch v1, :pswitch_data_0

    goto :goto_4

    .line 922
    :pswitch_0
    invoke-direct {p0, v0}, Lcom/helpshift/websockets/WebSocketFrame;->appendPayloadBinary(Ljava/lang/StringBuilder;)V

    goto :goto_4

    .line 918
    :pswitch_1
    invoke-direct {p0, v0}, Lcom/helpshift/websockets/WebSocketFrame;->appendPayloadText(Ljava/lang/StringBuilder;)V

    goto :goto_4

    .line 926
    :cond_4
    invoke-direct {p0, v0}, Lcom/helpshift/websockets/WebSocketFrame;->appendPayloadClose(Ljava/lang/StringBuilder;)V

    :goto_4
    const-string v1, ")"

    .line 930
    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v0

    return-object v0

    :pswitch_data_0
    .packed-switch 0x1
        :pswitch_1
        :pswitch_0
    .end packed-switch
.end method
