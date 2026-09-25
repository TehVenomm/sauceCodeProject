.class Lcom/helpshift/websockets/WebSocketOutputStream;
.super Ljava/io/FilterOutputStream;
.source "WebSocketOutputStream.java"


# direct methods
.method public constructor <init>(Ljava/io/OutputStream;)V
    .locals 0

    .line 32
    invoke-direct {p0, p1}, Ljava/io/FilterOutputStream;-><init>(Ljava/io/OutputStream;)V

    return-void
.end method

.method private writeFrame0(Lcom/helpshift/websockets/WebSocketFrame;)V
    .locals 3
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    .line 61
    invoke-virtual {p1}, Lcom/helpshift/websockets/WebSocketFrame;->getFin()Z

    move-result v0

    const/4 v1, 0x0

    if-eqz v0, :cond_0

    const/16 v0, 0x80

    goto :goto_0

    :cond_0
    const/4 v0, 0x0

    .line 62
    :goto_0
    invoke-virtual {p1}, Lcom/helpshift/websockets/WebSocketFrame;->getRsv1()Z

    move-result v2

    if-eqz v2, :cond_1

    const/16 v2, 0x40

    goto :goto_1

    :cond_1
    const/4 v2, 0x0

    :goto_1
    or-int/2addr v0, v2

    .line 63
    invoke-virtual {p1}, Lcom/helpshift/websockets/WebSocketFrame;->getRsv2()Z

    move-result v2

    if-eqz v2, :cond_2

    const/16 v2, 0x20

    goto :goto_2

    :cond_2
    const/4 v2, 0x0

    :goto_2
    or-int/2addr v0, v2

    .line 64
    invoke-virtual {p1}, Lcom/helpshift/websockets/WebSocketFrame;->getRsv3()Z

    move-result v2

    if-eqz v2, :cond_3

    const/16 v1, 0x10

    :cond_3
    or-int/2addr v0, v1

    .line 65
    invoke-virtual {p1}, Lcom/helpshift/websockets/WebSocketFrame;->getOpcode()I

    move-result p1

    and-int/lit8 p1, p1, 0xf

    or-int/2addr p1, v0

    .line 67
    invoke-virtual {p0, p1}, Lcom/helpshift/websockets/WebSocketOutputStream;->write(I)V

    return-void
.end method

.method private writeFrame1(Lcom/helpshift/websockets/WebSocketFrame;)V
    .locals 1
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    .line 75
    invoke-virtual {p1}, Lcom/helpshift/websockets/WebSocketFrame;->getPayloadLength()I

    move-result p1

    const/16 v0, 0x7d

    if-gt p1, v0, :cond_0

    or-int/lit16 p1, p1, 0x80

    goto :goto_0

    :cond_0
    const v0, 0xffff

    if-gt p1, v0, :cond_1

    const/16 p1, 0xfe

    goto :goto_0

    :cond_1
    const/16 p1, 0xff

    .line 87
    :goto_0
    invoke-virtual {p0, p1}, Lcom/helpshift/websockets/WebSocketOutputStream;->write(I)V

    return-void
.end method

.method private writeFrameExtendedPayloadLength(Lcom/helpshift/websockets/WebSocketFrame;)V
    .locals 1
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    .line 92
    invoke-virtual {p1}, Lcom/helpshift/websockets/WebSocketFrame;->getPayloadLength()I

    move-result p1

    const/16 v0, 0x7d

    if-gt p1, v0, :cond_0

    return-void

    :cond_0
    const v0, 0xffff

    if-gt p1, v0, :cond_1

    shr-int/lit8 v0, p1, 0x8

    and-int/lit16 v0, v0, 0xff

    .line 100
    invoke-virtual {p0, v0}, Lcom/helpshift/websockets/WebSocketOutputStream;->write(I)V

    and-int/lit16 p1, p1, 0xff

    .line 101
    invoke-virtual {p0, p1}, Lcom/helpshift/websockets/WebSocketOutputStream;->write(I)V

    return-void

    :cond_1
    const/4 v0, 0x0

    .line 107
    invoke-virtual {p0, v0}, Lcom/helpshift/websockets/WebSocketOutputStream;->write(I)V

    .line 108
    invoke-virtual {p0, v0}, Lcom/helpshift/websockets/WebSocketOutputStream;->write(I)V

    .line 109
    invoke-virtual {p0, v0}, Lcom/helpshift/websockets/WebSocketOutputStream;->write(I)V

    .line 110
    invoke-virtual {p0, v0}, Lcom/helpshift/websockets/WebSocketOutputStream;->write(I)V

    shr-int/lit8 v0, p1, 0x18

    and-int/lit16 v0, v0, 0xff

    .line 111
    invoke-virtual {p0, v0}, Lcom/helpshift/websockets/WebSocketOutputStream;->write(I)V

    shr-int/lit8 v0, p1, 0x10

    and-int/lit16 v0, v0, 0xff

    .line 112
    invoke-virtual {p0, v0}, Lcom/helpshift/websockets/WebSocketOutputStream;->write(I)V

    shr-int/lit8 v0, p1, 0x8

    and-int/lit16 v0, v0, 0xff

    .line 113
    invoke-virtual {p0, v0}, Lcom/helpshift/websockets/WebSocketOutputStream;->write(I)V

    and-int/lit16 p1, p1, 0xff

    .line 114
    invoke-virtual {p0, p1}, Lcom/helpshift/websockets/WebSocketOutputStream;->write(I)V

    return-void
.end method

.method private writeFramePayload(Lcom/helpshift/websockets/WebSocketFrame;[B)V
    .locals 3
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    .line 119
    invoke-virtual {p1}, Lcom/helpshift/websockets/WebSocketFrame;->getPayload()[B

    move-result-object p1

    if-nez p1, :cond_0

    return-void

    :cond_0
    const/4 v0, 0x0

    .line 125
    :goto_0
    array-length v1, p1

    if-ge v0, v1, :cond_1

    .line 127
    aget-byte v1, p1, v0

    rem-int/lit8 v2, v0, 0x4

    aget-byte v2, p2, v2

    xor-int/2addr v1, v2

    and-int/lit16 v1, v1, 0xff

    .line 130
    invoke-virtual {p0, v1}, Lcom/helpshift/websockets/WebSocketOutputStream;->write(I)V

    add-int/lit8 v0, v0, 0x1

    goto :goto_0

    :cond_1
    return-void
.end method


# virtual methods
.method public write(Lcom/helpshift/websockets/WebSocketFrame;)V
    .locals 1
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    .line 45
    invoke-direct {p0, p1}, Lcom/helpshift/websockets/WebSocketOutputStream;->writeFrame0(Lcom/helpshift/websockets/WebSocketFrame;)V

    .line 46
    invoke-direct {p0, p1}, Lcom/helpshift/websockets/WebSocketOutputStream;->writeFrame1(Lcom/helpshift/websockets/WebSocketFrame;)V

    .line 47
    invoke-direct {p0, p1}, Lcom/helpshift/websockets/WebSocketOutputStream;->writeFrameExtendedPayloadLength(Lcom/helpshift/websockets/WebSocketFrame;)V

    const/4 v0, 0x4

    .line 50
    invoke-static {v0}, Lcom/helpshift/websockets/Misc;->nextBytes(I)[B

    move-result-object v0

    .line 53
    invoke-virtual {p0, v0}, Lcom/helpshift/websockets/WebSocketOutputStream;->write([B)V

    .line 56
    invoke-direct {p0, p1, v0}, Lcom/helpshift/websockets/WebSocketOutputStream;->writeFramePayload(Lcom/helpshift/websockets/WebSocketFrame;[B)V

    return-void
.end method

.method public write(Ljava/lang/String;)V
    .locals 0
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    .line 38
    invoke-static {p1}, Lcom/helpshift/websockets/Misc;->getBytesUTF8(Ljava/lang/String;)[B

    move-result-object p1

    .line 40
    invoke-virtual {p0, p1}, Lcom/helpshift/websockets/WebSocketOutputStream;->write([B)V

    return-void
.end method
