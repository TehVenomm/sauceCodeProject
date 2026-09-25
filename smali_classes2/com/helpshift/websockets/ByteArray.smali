.class Lcom/helpshift/websockets/ByteArray;
.super Ljava/lang/Object;
.source "ByteArray.java"


# static fields
.field private static final ADDITIONAL_BUFFER_SIZE:I = 0x400


# instance fields
.field private mBuffer:Ljava/nio/ByteBuffer;

.field private mLength:I


# direct methods
.method public constructor <init>(I)V
    .locals 0

    .line 46
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 47
    invoke-static {p1}, Ljava/nio/ByteBuffer;->allocate(I)Ljava/nio/ByteBuffer;

    move-result-object p1

    iput-object p1, p0, Lcom/helpshift/websockets/ByteArray;->mBuffer:Ljava/nio/ByteBuffer;

    const/4 p1, 0x0

    .line 48
    iput p1, p0, Lcom/helpshift/websockets/ByteArray;->mLength:I

    return-void
.end method

.method private expandBuffer(I)V
    .locals 3

    .line 79
    invoke-static {p1}, Ljava/nio/ByteBuffer;->allocate(I)Ljava/nio/ByteBuffer;

    move-result-object p1

    .line 82
    iget-object v0, p0, Lcom/helpshift/websockets/ByteArray;->mBuffer:Ljava/nio/ByteBuffer;

    invoke-virtual {v0}, Ljava/nio/ByteBuffer;->position()I

    move-result v0

    .line 83
    iget-object v1, p0, Lcom/helpshift/websockets/ByteArray;->mBuffer:Ljava/nio/ByteBuffer;

    const/4 v2, 0x0

    invoke-virtual {v1, v2}, Ljava/nio/ByteBuffer;->position(I)Ljava/nio/Buffer;

    .line 84
    iget-object v1, p0, Lcom/helpshift/websockets/ByteArray;->mBuffer:Ljava/nio/ByteBuffer;

    invoke-virtual {p1, v1}, Ljava/nio/ByteBuffer;->put(Ljava/nio/ByteBuffer;)Ljava/nio/ByteBuffer;

    .line 85
    invoke-virtual {p1, v0}, Ljava/nio/ByteBuffer;->position(I)Ljava/nio/Buffer;

    .line 88
    iput-object p1, p0, Lcom/helpshift/websockets/ByteArray;->mBuffer:Ljava/nio/ByteBuffer;

    return-void
.end method


# virtual methods
.method public clear()V
    .locals 2

    .line 185
    iget-object v0, p0, Lcom/helpshift/websockets/ByteArray;->mBuffer:Ljava/nio/ByteBuffer;

    invoke-virtual {v0}, Ljava/nio/ByteBuffer;->clear()Ljava/nio/Buffer;

    .line 186
    iget-object v0, p0, Lcom/helpshift/websockets/ByteArray;->mBuffer:Ljava/nio/ByteBuffer;

    const/4 v1, 0x0

    invoke-virtual {v0, v1}, Ljava/nio/ByteBuffer;->position(I)Ljava/nio/Buffer;

    .line 187
    iput v1, p0, Lcom/helpshift/websockets/ByteArray;->mLength:I

    return-void
.end method

.method public clearBit(I)V
    .locals 1

    const/4 v0, 0x0

    .line 292
    invoke-virtual {p0, p1, v0}, Lcom/helpshift/websockets/ByteArray;->setBit(IZ)V

    return-void
.end method

.method public get(I)B
    .locals 3
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/lang/IndexOutOfBoundsException;
        }
    .end annotation

    if-ltz p1, :cond_0

    .line 64
    iget v0, p0, Lcom/helpshift/websockets/ByteArray;->mLength:I

    if-le v0, p1, :cond_0

    .line 70
    iget-object v0, p0, Lcom/helpshift/websockets/ByteArray;->mBuffer:Ljava/nio/ByteBuffer;

    invoke-virtual {v0, p1}, Ljava/nio/ByteBuffer;->get(I)B

    move-result p1

    return p1

    .line 66
    :cond_0
    new-instance v0, Ljava/lang/IndexOutOfBoundsException;

    const/4 v1, 0x2

    new-array v1, v1, [Ljava/lang/Object;

    const/4 v2, 0x0

    .line 67
    invoke-static {p1}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object p1

    aput-object p1, v1, v2

    const/4 p1, 0x1

    iget v2, p0, Lcom/helpshift/websockets/ByteArray;->mLength:I

    invoke-static {v2}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v2

    aput-object v2, v1, p1

    const-string p1, "Bad index: index=%d, length=%d"

    invoke-static {p1, v1}, Ljava/lang/String;->format(Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object p1

    invoke-direct {v0, p1}, Ljava/lang/IndexOutOfBoundsException;-><init>(Ljava/lang/String;)V

    throw v0
.end method

.method public getBit(I)Z
    .locals 2

    .line 208
    div-int/lit8 v0, p1, 0x8

    .line 209
    rem-int/lit8 p1, p1, 0x8

    .line 210
    invoke-virtual {p0, v0}, Lcom/helpshift/websockets/ByteArray;->get(I)B

    move-result v0

    const/4 v1, 0x1

    shl-int p1, v1, p1

    and-int/2addr p1, v0

    if-eqz p1, :cond_0

    goto :goto_0

    :cond_0
    const/4 v1, 0x0

    :goto_0
    return v1
.end method

.method public getBits(II)I
    .locals 4

    const/4 v0, 0x1

    const/4 v1, 0x0

    const/4 v0, 0x0

    const/4 v2, 0x1

    :goto_0
    if-ge v1, p2, :cond_1

    add-int v3, p1, v1

    .line 224
    invoke-virtual {p0, v3}, Lcom/helpshift/websockets/ByteArray;->getBit(I)Z

    move-result v3

    if-eqz v3, :cond_0

    add-int/2addr v0, v2

    :cond_0
    add-int/lit8 v1, v1, 0x1

    mul-int/lit8 v2, v2, 0x2

    goto :goto_0

    :cond_1
    return v0
.end method

.method public getHuffmanBits(II)I
    .locals 3

    const/4 v0, 0x1

    sub-int/2addr p2, v0

    const/4 v1, 0x0

    :goto_0
    if-ltz p2, :cond_1

    add-int v2, p1, p2

    .line 248
    invoke-virtual {p0, v2}, Lcom/helpshift/websockets/ByteArray;->getBit(I)Z

    move-result v2

    if-eqz v2, :cond_0

    add-int/2addr v1, v0

    :cond_0
    add-int/lit8 p2, p2, -0x1

    mul-int/lit8 v0, v0, 0x2

    goto :goto_0

    :cond_1
    return v1
.end method

.method public length()I
    .locals 1

    .line 56
    iget v0, p0, Lcom/helpshift/websockets/ByteArray;->mLength:I

    return v0
.end method

.method public put(I)V
    .locals 2

    .line 97
    iget-object v0, p0, Lcom/helpshift/websockets/ByteArray;->mBuffer:Ljava/nio/ByteBuffer;

    invoke-virtual {v0}, Ljava/nio/ByteBuffer;->capacity()I

    move-result v0

    iget v1, p0, Lcom/helpshift/websockets/ByteArray;->mLength:I

    add-int/lit8 v1, v1, 0x1

    if-ge v0, v1, :cond_0

    .line 98
    iget v0, p0, Lcom/helpshift/websockets/ByteArray;->mLength:I

    add-int/lit16 v0, v0, 0x400

    invoke-direct {p0, v0}, Lcom/helpshift/websockets/ByteArray;->expandBuffer(I)V

    .line 101
    :cond_0
    iget-object v0, p0, Lcom/helpshift/websockets/ByteArray;->mBuffer:Ljava/nio/ByteBuffer;

    int-to-byte p1, p1

    invoke-virtual {v0, p1}, Ljava/nio/ByteBuffer;->put(B)Ljava/nio/ByteBuffer;

    .line 102
    iget p1, p0, Lcom/helpshift/websockets/ByteArray;->mLength:I

    add-int/lit8 p1, p1, 0x1

    iput p1, p0, Lcom/helpshift/websockets/ByteArray;->mLength:I

    return-void
.end method

.method public put(Lcom/helpshift/websockets/ByteArray;II)V
    .locals 0

    .line 148
    iget-object p1, p1, Lcom/helpshift/websockets/ByteArray;->mBuffer:Ljava/nio/ByteBuffer;

    invoke-virtual {p1}, Ljava/nio/ByteBuffer;->array()[B

    move-result-object p1

    invoke-virtual {p0, p1, p2, p3}, Lcom/helpshift/websockets/ByteArray;->put([BII)V

    return-void
.end method

.method public put([B)V
    .locals 3

    .line 113
    iget-object v0, p0, Lcom/helpshift/websockets/ByteArray;->mBuffer:Ljava/nio/ByteBuffer;

    invoke-virtual {v0}, Ljava/nio/ByteBuffer;->capacity()I

    move-result v0

    iget v1, p0, Lcom/helpshift/websockets/ByteArray;->mLength:I

    array-length v2, p1

    add-int/2addr v1, v2

    if-ge v0, v1, :cond_0

    .line 114
    iget v0, p0, Lcom/helpshift/websockets/ByteArray;->mLength:I

    array-length v1, p1

    add-int/2addr v0, v1

    add-int/lit16 v0, v0, 0x400

    invoke-direct {p0, v0}, Lcom/helpshift/websockets/ByteArray;->expandBuffer(I)V

    .line 117
    :cond_0
    iget-object v0, p0, Lcom/helpshift/websockets/ByteArray;->mBuffer:Ljava/nio/ByteBuffer;

    invoke-virtual {v0, p1}, Ljava/nio/ByteBuffer;->put([B)Ljava/nio/ByteBuffer;

    .line 118
    iget v0, p0, Lcom/helpshift/websockets/ByteArray;->mLength:I

    array-length p1, p1

    add-int/2addr v0, p1

    iput v0, p0, Lcom/helpshift/websockets/ByteArray;->mLength:I

    return-void
.end method

.method public put([BII)V
    .locals 2

    .line 131
    iget-object v0, p0, Lcom/helpshift/websockets/ByteArray;->mBuffer:Ljava/nio/ByteBuffer;

    invoke-virtual {v0}, Ljava/nio/ByteBuffer;->capacity()I

    move-result v0

    iget v1, p0, Lcom/helpshift/websockets/ByteArray;->mLength:I

    add-int/2addr v1, p3

    if-ge v0, v1, :cond_0

    .line 132
    iget v0, p0, Lcom/helpshift/websockets/ByteArray;->mLength:I

    add-int/2addr v0, p3

    add-int/lit16 v0, v0, 0x400

    invoke-direct {p0, v0}, Lcom/helpshift/websockets/ByteArray;->expandBuffer(I)V

    .line 135
    :cond_0
    iget-object v0, p0, Lcom/helpshift/websockets/ByteArray;->mBuffer:Ljava/nio/ByteBuffer;

    invoke-virtual {v0, p1, p2, p3}, Ljava/nio/ByteBuffer;->put([BII)Ljava/nio/ByteBuffer;

    .line 136
    iget p1, p0, Lcom/helpshift/websockets/ByteArray;->mLength:I

    add-int/2addr p1, p3

    iput p1, p0, Lcom/helpshift/websockets/ByteArray;->mLength:I

    return-void
.end method

.method public readBit([I)Z
    .locals 3

    const/4 v0, 0x0

    .line 258
    aget v1, p1, v0

    invoke-virtual {p0, v1}, Lcom/helpshift/websockets/ByteArray;->getBit(I)Z

    move-result v1

    .line 260
    aget v2, p1, v0

    add-int/lit8 v2, v2, 0x1

    aput v2, p1, v0

    return v1
.end method

.method public readBits([II)I
    .locals 3

    const/4 v0, 0x0

    .line 267
    aget v1, p1, v0

    invoke-virtual {p0, v1, p2}, Lcom/helpshift/websockets/ByteArray;->getBits(II)I

    move-result v1

    .line 269
    aget v2, p1, v0

    add-int/2addr v2, p2

    aput v2, p1, v0

    return v1
.end method

.method public setBit(IZ)V
    .locals 3

    .line 276
    div-int/lit8 v0, p1, 0x8

    .line 277
    rem-int/lit8 p1, p1, 0x8

    .line 278
    invoke-virtual {p0, v0}, Lcom/helpshift/websockets/ByteArray;->get(I)B

    move-result v1

    const/4 v2, 0x1

    if-eqz p2, :cond_0

    shl-int p1, v2, p1

    or-int/2addr p1, v1

    goto :goto_0

    :cond_0
    shl-int p1, v2, p1

    xor-int/lit8 p1, p1, -0x1

    and-int/2addr p1, v1

    .line 287
    :goto_0
    iget-object p2, p0, Lcom/helpshift/websockets/ByteArray;->mBuffer:Ljava/nio/ByteBuffer;

    int-to-byte p1, p1

    invoke-virtual {p2, v0, p1}, Ljava/nio/ByteBuffer;->put(IB)Ljava/nio/ByteBuffer;

    return-void
.end method

.method public shrink(I)V
    .locals 2

    .line 192
    iget-object v0, p0, Lcom/helpshift/websockets/ByteArray;->mBuffer:Ljava/nio/ByteBuffer;

    invoke-virtual {v0}, Ljava/nio/ByteBuffer;->capacity()I

    move-result v0

    if-gt v0, p1, :cond_0

    return-void

    .line 196
    :cond_0
    iget v0, p0, Lcom/helpshift/websockets/ByteArray;->mLength:I

    .line 197
    iget v1, p0, Lcom/helpshift/websockets/ByteArray;->mLength:I

    sub-int/2addr v1, p1

    .line 199
    invoke-virtual {p0, v1, v0}, Lcom/helpshift/websockets/ByteArray;->toBytes(II)[B

    move-result-object p1

    .line 201
    invoke-static {p1}, Ljava/nio/ByteBuffer;->wrap([B)Ljava/nio/ByteBuffer;

    move-result-object v0

    iput-object v0, p0, Lcom/helpshift/websockets/ByteArray;->mBuffer:Ljava/nio/ByteBuffer;

    .line 202
    iget-object v0, p0, Lcom/helpshift/websockets/ByteArray;->mBuffer:Ljava/nio/ByteBuffer;

    array-length v1, p1

    invoke-virtual {v0, v1}, Ljava/nio/ByteBuffer;->position(I)Ljava/nio/Buffer;

    .line 203
    array-length p1, p1

    iput p1, p0, Lcom/helpshift/websockets/ByteArray;->mLength:I

    return-void
.end method

.method public toBytes()[B
    .locals 1

    const/4 v0, 0x0

    .line 156
    invoke-virtual {p0, v0}, Lcom/helpshift/websockets/ByteArray;->toBytes(I)[B

    move-result-object v0

    return-object v0
.end method

.method public toBytes(I)[B
    .locals 1

    .line 161
    invoke-virtual {p0}, Lcom/helpshift/websockets/ByteArray;->length()I

    move-result v0

    invoke-virtual {p0, p1, v0}, Lcom/helpshift/websockets/ByteArray;->toBytes(II)[B

    move-result-object p1

    return-object p1
.end method

.method public toBytes(II)[B
    .locals 3

    sub-int v0, p2, p1

    const/4 v1, 0x0

    if-ltz v0, :cond_1

    if-ltz p1, :cond_1

    .line 168
    iget v2, p0, Lcom/helpshift/websockets/ByteArray;->mLength:I

    if-lt v2, p2, :cond_1

    .line 174
    new-array p2, v0, [B

    if-eqz v0, :cond_0

    .line 177
    iget-object v2, p0, Lcom/helpshift/websockets/ByteArray;->mBuffer:Ljava/nio/ByteBuffer;

    invoke-virtual {v2}, Ljava/nio/ByteBuffer;->array()[B

    move-result-object v2

    invoke-static {v2, p1, p2, v1, v0}, Ljava/lang/System;->arraycopy(Ljava/lang/Object;ILjava/lang/Object;II)V

    :cond_0
    return-object p2

    .line 169
    :cond_1
    new-instance v0, Ljava/lang/IllegalArgumentException;

    const/4 v2, 0x3

    new-array v2, v2, [Ljava/lang/Object;

    .line 171
    invoke-static {p1}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object p1

    aput-object p1, v2, v1

    const/4 p1, 0x1

    invoke-static {p2}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object p2

    aput-object p2, v2, p1

    const/4 p1, 0x2

    iget p2, p0, Lcom/helpshift/websockets/ByteArray;->mLength:I

    invoke-static {p2}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object p2

    aput-object p2, v2, p1

    const-string p1, "Bad range: beginIndex=%d, endIndex=%d, length=%d"

    .line 170
    invoke-static {p1, v2}, Ljava/lang/String;->format(Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object p1

    invoke-direct {v0, p1}, Ljava/lang/IllegalArgumentException;-><init>(Ljava/lang/String;)V

    throw v0
.end method
