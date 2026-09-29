.class public final Lcom/google/firebase/messaging/cpp/DataPair;
.super Lcom/google/flatbuffers/Table;
.source "DataPair.java"


# direct methods
.method public constructor <init>()V
    .locals 0

    .line 11
    invoke-direct {p0}, Lcom/google/flatbuffers/Table;-><init>()V

    return-void
.end method

.method public static addKey(Lcom/google/flatbuffers/FlatBufferBuilder;I)V
    .locals 1

    const/4 v0, 0x0

    .line 34
    invoke-virtual {p0, v0, p1, v0}, Lcom/google/flatbuffers/FlatBufferBuilder;->addOffset(III)V

    return-void
.end method

.method public static addValue(Lcom/google/flatbuffers/FlatBufferBuilder;I)V
    .locals 2

    const/4 v0, 0x1

    const/4 v1, 0x0

    .line 35
    invoke-virtual {p0, v0, p1, v1}, Lcom/google/flatbuffers/FlatBufferBuilder;->addOffset(III)V

    return-void
.end method

.method public static createDataPair(Lcom/google/flatbuffers/FlatBufferBuilder;II)I
    .locals 1

    const/4 v0, 0x2

    .line 27
    invoke-virtual {p0, v0}, Lcom/google/flatbuffers/FlatBufferBuilder;->startObject(I)V

    .line 28
    invoke-static {p0, p2}, Lcom/google/firebase/messaging/cpp/DataPair;->addValue(Lcom/google/flatbuffers/FlatBufferBuilder;I)V

    .line 29
    invoke-static {p0, p1}, Lcom/google/firebase/messaging/cpp/DataPair;->addKey(Lcom/google/flatbuffers/FlatBufferBuilder;I)V

    .line 30
    invoke-static {p0}, Lcom/google/firebase/messaging/cpp/DataPair;->endDataPair(Lcom/google/flatbuffers/FlatBufferBuilder;)I

    move-result p0

    return p0
.end method

.method public static endDataPair(Lcom/google/flatbuffers/FlatBufferBuilder;)I
    .locals 0

    .line 37
    invoke-virtual {p0}, Lcom/google/flatbuffers/FlatBufferBuilder;->endObject()I

    move-result p0

    return p0
.end method

.method public static getRootAsDataPair(Ljava/nio/ByteBuffer;)Lcom/google/firebase/messaging/cpp/DataPair;
    .locals 1

    .line 12
    new-instance v0, Lcom/google/firebase/messaging/cpp/DataPair;

    invoke-direct {v0}, Lcom/google/firebase/messaging/cpp/DataPair;-><init>()V

    invoke-static {p0, v0}, Lcom/google/firebase/messaging/cpp/DataPair;->getRootAsDataPair(Ljava/nio/ByteBuffer;Lcom/google/firebase/messaging/cpp/DataPair;)Lcom/google/firebase/messaging/cpp/DataPair;

    move-result-object p0

    return-object p0
.end method

.method public static getRootAsDataPair(Ljava/nio/ByteBuffer;Lcom/google/firebase/messaging/cpp/DataPair;)Lcom/google/firebase/messaging/cpp/DataPair;
    .locals 2

    .line 13
    sget-object v0, Ljava/nio/ByteOrder;->LITTLE_ENDIAN:Ljava/nio/ByteOrder;

    invoke-virtual {p0, v0}, Ljava/nio/ByteBuffer;->order(Ljava/nio/ByteOrder;)Ljava/nio/ByteBuffer;

    invoke-virtual {p0}, Ljava/nio/ByteBuffer;->position()I

    move-result v0

    invoke-virtual {p0, v0}, Ljava/nio/ByteBuffer;->getInt(I)I

    move-result v0

    invoke-virtual {p0}, Ljava/nio/ByteBuffer;->position()I

    move-result v1

    add-int/2addr v0, v1

    invoke-virtual {p1, v0, p0}, Lcom/google/firebase/messaging/cpp/DataPair;->__assign(ILjava/nio/ByteBuffer;)Lcom/google/firebase/messaging/cpp/DataPair;

    move-result-object p0

    return-object p0
.end method

.method public static startDataPair(Lcom/google/flatbuffers/FlatBufferBuilder;)V
    .locals 1

    const/4 v0, 0x2

    .line 33
    invoke-virtual {p0, v0}, Lcom/google/flatbuffers/FlatBufferBuilder;->startObject(I)V

    return-void
.end method


# virtual methods
.method public __assign(ILjava/nio/ByteBuffer;)Lcom/google/firebase/messaging/cpp/DataPair;
    .locals 0

    .line 15
    invoke-virtual {p0, p1, p2}, Lcom/google/firebase/messaging/cpp/DataPair;->__init(ILjava/nio/ByteBuffer;)V

    return-object p0
.end method

.method public __init(ILjava/nio/ByteBuffer;)V
    .locals 1

    .line 14
    iput p1, p0, Lcom/google/firebase/messaging/cpp/DataPair;->bb_pos:I

    iput-object p2, p0, Lcom/google/firebase/messaging/cpp/DataPair;->bb:Ljava/nio/ByteBuffer;

    iget p1, p0, Lcom/google/firebase/messaging/cpp/DataPair;->bb_pos:I

    iget-object p2, p0, Lcom/google/firebase/messaging/cpp/DataPair;->bb:Ljava/nio/ByteBuffer;

    iget v0, p0, Lcom/google/firebase/messaging/cpp/DataPair;->bb_pos:I

    invoke-virtual {p2, v0}, Ljava/nio/ByteBuffer;->getInt(I)I

    move-result p2

    sub-int/2addr p1, p2

    iput p1, p0, Lcom/google/firebase/messaging/cpp/DataPair;->vtable_start:I

    iget-object p1, p0, Lcom/google/firebase/messaging/cpp/DataPair;->bb:Ljava/nio/ByteBuffer;

    iget p2, p0, Lcom/google/firebase/messaging/cpp/DataPair;->vtable_start:I

    invoke-virtual {p1, p2}, Ljava/nio/ByteBuffer;->getShort(I)S

    move-result p1

    iput p1, p0, Lcom/google/firebase/messaging/cpp/DataPair;->vtable_size:I

    return-void
.end method

.method public key()Ljava/lang/String;
    .locals 2

    const/4 v0, 0x4

    .line 17
    invoke-virtual {p0, v0}, Lcom/google/firebase/messaging/cpp/DataPair;->__offset(I)I

    move-result v0

    if-eqz v0, :cond_0

    iget v1, p0, Lcom/google/firebase/messaging/cpp/DataPair;->bb_pos:I

    add-int/2addr v0, v1

    invoke-virtual {p0, v0}, Lcom/google/firebase/messaging/cpp/DataPair;->__string(I)Ljava/lang/String;

    move-result-object v0

    goto :goto_0

    :cond_0
    const/4 v0, 0x0

    :goto_0
    return-object v0
.end method

.method public keyAsByteBuffer()Ljava/nio/ByteBuffer;
    .locals 2

    const/4 v0, 0x4

    const/4 v1, 0x1

    .line 18
    invoke-virtual {p0, v0, v1}, Lcom/google/firebase/messaging/cpp/DataPair;->__vector_as_bytebuffer(II)Ljava/nio/ByteBuffer;

    move-result-object v0

    return-object v0
.end method

.method public keyInByteBuffer(Ljava/nio/ByteBuffer;)Ljava/nio/ByteBuffer;
    .locals 2

    const/4 v0, 0x4

    const/4 v1, 0x1

    .line 19
    invoke-virtual {p0, p1, v0, v1}, Lcom/google/firebase/messaging/cpp/DataPair;->__vector_in_bytebuffer(Ljava/nio/ByteBuffer;II)Ljava/nio/ByteBuffer;

    move-result-object p1

    return-object p1
.end method

.method public value()Ljava/lang/String;
    .locals 2

    const/4 v0, 0x6

    .line 20
    invoke-virtual {p0, v0}, Lcom/google/firebase/messaging/cpp/DataPair;->__offset(I)I

    move-result v0

    if-eqz v0, :cond_0

    iget v1, p0, Lcom/google/firebase/messaging/cpp/DataPair;->bb_pos:I

    add-int/2addr v0, v1

    invoke-virtual {p0, v0}, Lcom/google/firebase/messaging/cpp/DataPair;->__string(I)Ljava/lang/String;

    move-result-object v0

    goto :goto_0

    :cond_0
    const/4 v0, 0x0

    :goto_0
    return-object v0
.end method

.method public valueAsByteBuffer()Ljava/nio/ByteBuffer;
    .locals 2

    const/4 v0, 0x6

    const/4 v1, 0x1

    .line 21
    invoke-virtual {p0, v0, v1}, Lcom/google/firebase/messaging/cpp/DataPair;->__vector_as_bytebuffer(II)Ljava/nio/ByteBuffer;

    move-result-object v0

    return-object v0
.end method

.method public valueInByteBuffer(Ljava/nio/ByteBuffer;)Ljava/nio/ByteBuffer;
    .locals 2

    const/4 v0, 0x6

    const/4 v1, 0x1

    .line 22
    invoke-virtual {p0, p1, v0, v1}, Lcom/google/firebase/messaging/cpp/DataPair;->__vector_in_bytebuffer(Ljava/nio/ByteBuffer;II)Ljava/nio/ByteBuffer;

    move-result-object p1

    return-object p1
.end method
