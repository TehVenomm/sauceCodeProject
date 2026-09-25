.class public Lcom/google/flatbuffers/Table;
.super Ljava/lang/Object;
.source "Table.java"


# static fields
.field public static final UTF8_CHARSET:Ljava/lang/ThreadLocal;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/lang/ThreadLocal<",
            "Ljava/nio/charset/Charset;",
            ">;"
        }
    .end annotation
.end field


# instance fields
.field protected bb:Ljava/nio/ByteBuffer;

.field protected bb_pos:I

.field utf8:Lcom/google/flatbuffers/Utf8;

.field protected vtable_size:I

.field protected vtable_start:I


# direct methods
.method static constructor <clinit>()V
    .locals 1

    .line 30
    new-instance v0, Lcom/google/flatbuffers/Table$1;

    invoke-direct {v0}, Lcom/google/flatbuffers/Table$1;-><init>()V

    sput-object v0, Lcom/google/flatbuffers/Table;->UTF8_CHARSET:Ljava/lang/ThreadLocal;

    return-void
.end method

.method public constructor <init>()V
    .locals 1

    .line 29
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 44
    invoke-static {}, Lcom/google/flatbuffers/Utf8;->getDefault()Lcom/google/flatbuffers/Utf8;

    move-result-object v0

    iput-object v0, p0, Lcom/google/flatbuffers/Table;->utf8:Lcom/google/flatbuffers/Utf8;

    return-void
.end method

.method protected static __has_identifier(Ljava/nio/ByteBuffer;Ljava/lang/String;)Z
    .locals 5

    .line 189
    invoke-virtual {p1}, Ljava/lang/String;->length()I

    move-result v0

    const/4 v1, 0x4

    if-ne v0, v1, :cond_2

    const/4 v0, 0x0

    const/4 v2, 0x0

    :goto_0
    if-ge v2, v1, :cond_1

    .line 193
    invoke-virtual {p1, v2}, Ljava/lang/String;->charAt(I)C

    move-result v3

    invoke-virtual {p0}, Ljava/nio/ByteBuffer;->position()I

    move-result v4

    add-int/2addr v4, v1

    add-int/2addr v4, v2

    invoke-virtual {p0, v4}, Ljava/nio/ByteBuffer;->get(I)B

    move-result v4

    int-to-char v4, v4

    if-eq v3, v4, :cond_0

    return v0

    :cond_0
    add-int/lit8 v2, v2, 0x1

    goto :goto_0

    :cond_1
    const/4 p0, 0x1

    return p0

    .line 190
    :cond_2
    new-instance p0, Ljava/lang/AssertionError;

    const-string p1, "FlatBuffers: file identifier must be length 4"

    invoke-direct {p0, p1}, Ljava/lang/AssertionError;-><init>(Ljava/lang/Object;)V

    throw p0
.end method

.method protected static __indirect(ILjava/nio/ByteBuffer;)I
    .locals 0

    .line 79
    invoke-virtual {p1, p0}, Ljava/nio/ByteBuffer;->getInt(I)I

    move-result p1

    add-int/2addr p0, p1

    return p0
.end method

.method protected static __offset(IILjava/nio/ByteBuffer;)I
    .locals 1

    .line 64
    invoke-virtual {p2}, Ljava/nio/ByteBuffer;->capacity()I

    move-result v0

    sub-int/2addr v0, p1

    add-int/2addr p0, v0

    .line 65
    invoke-virtual {p2, v0}, Ljava/nio/ByteBuffer;->getInt(I)I

    move-result p1

    sub-int/2addr p0, p1

    invoke-virtual {p2, p0}, Ljava/nio/ByteBuffer;->getShort(I)S

    move-result p0

    add-int/2addr p0, v0

    return p0
.end method

.method protected static compareStrings(IILjava/nio/ByteBuffer;)I
    .locals 8

    .line 232
    invoke-virtual {p2, p0}, Ljava/nio/ByteBuffer;->getInt(I)I

    move-result v0

    add-int/2addr p0, v0

    .line 233
    invoke-virtual {p2, p1}, Ljava/nio/ByteBuffer;->getInt(I)I

    move-result v0

    add-int/2addr p1, v0

    .line 234
    invoke-virtual {p2, p0}, Ljava/nio/ByteBuffer;->getInt(I)I

    move-result v0

    .line 235
    invoke-virtual {p2, p1}, Ljava/nio/ByteBuffer;->getInt(I)I

    move-result v1

    add-int/lit8 p0, p0, 0x4

    add-int/lit8 p1, p1, 0x4

    .line 238
    invoke-static {v0, v1}, Ljava/lang/Math;->min(II)I

    move-result v2

    const/4 v3, 0x0

    :goto_0
    if-ge v3, v2, :cond_1

    add-int v4, v3, p0

    .line 240
    invoke-virtual {p2, v4}, Ljava/nio/ByteBuffer;->get(I)B

    move-result v5

    add-int v6, v3, p1

    invoke-virtual {p2, v6}, Ljava/nio/ByteBuffer;->get(I)B

    move-result v7

    if-eq v5, v7, :cond_0

    .line 241
    invoke-virtual {p2, v4}, Ljava/nio/ByteBuffer;->get(I)B

    move-result p0

    invoke-virtual {p2, v6}, Ljava/nio/ByteBuffer;->get(I)B

    move-result p1

    sub-int/2addr p0, p1

    return p0

    :cond_0
    add-int/lit8 v3, v3, 0x1

    goto :goto_0

    :cond_1
    sub-int/2addr v0, v1

    return v0
.end method

.method protected static compareStrings(I[BLjava/nio/ByteBuffer;)I
    .locals 7

    .line 254
    invoke-virtual {p2, p0}, Ljava/nio/ByteBuffer;->getInt(I)I

    move-result v0

    add-int/2addr p0, v0

    .line 255
    invoke-virtual {p2, p0}, Ljava/nio/ByteBuffer;->getInt(I)I

    move-result v0

    .line 256
    array-length v1, p1

    add-int/lit8 p0, p0, 0x4

    .line 258
    invoke-static {v0, v1}, Ljava/lang/Math;->min(II)I

    move-result v2

    const/4 v3, 0x0

    :goto_0
    if-ge v3, v2, :cond_1

    add-int v4, v3, p0

    .line 260
    invoke-virtual {p2, v4}, Ljava/nio/ByteBuffer;->get(I)B

    move-result v5

    aget-byte v6, p1, v3

    if-eq v5, v6, :cond_0

    .line 261
    invoke-virtual {p2, v4}, Ljava/nio/ByteBuffer;->get(I)B

    move-result p0

    aget-byte p1, p1, v3

    sub-int/2addr p0, p1

    return p0

    :cond_0
    add-int/lit8 v3, v3, 0x1

    goto :goto_0

    :cond_1
    sub-int/2addr v0, v1

    return v0
.end method


# virtual methods
.method protected __indirect(I)I
    .locals 1

    .line 75
    iget-object v0, p0, Lcom/google/flatbuffers/Table;->bb:Ljava/nio/ByteBuffer;

    invoke-virtual {v0, p1}, Ljava/nio/ByteBuffer;->getInt(I)I

    move-result v0

    add-int/2addr p1, v0

    return p1
.end method

.method protected __offset(I)I
    .locals 2

    .line 60
    iget v0, p0, Lcom/google/flatbuffers/Table;->vtable_size:I

    if-ge p1, v0, :cond_0

    iget-object v0, p0, Lcom/google/flatbuffers/Table;->bb:Ljava/nio/ByteBuffer;

    iget v1, p0, Lcom/google/flatbuffers/Table;->vtable_start:I

    add-int/2addr v1, p1

    invoke-virtual {v0, v1}, Ljava/nio/ByteBuffer;->getShort(I)S

    move-result p1

    goto :goto_0

    :cond_0
    const/4 p1, 0x0

    :goto_0
    return p1
.end method

.method public __reset()V
    .locals 1

    const/4 v0, 0x0

    .line 274
    iput-object v0, p0, Lcom/google/flatbuffers/Table;->bb:Ljava/nio/ByteBuffer;

    const/4 v0, 0x0

    .line 275
    iput v0, p0, Lcom/google/flatbuffers/Table;->bb_pos:I

    .line 276
    iput v0, p0, Lcom/google/flatbuffers/Table;->vtable_start:I

    .line 277
    iput v0, p0, Lcom/google/flatbuffers/Table;->vtable_size:I

    return-void
.end method

.method protected __string(I)Ljava/lang/String;
    .locals 3

    .line 94
    iget-object v0, p0, Lcom/google/flatbuffers/Table;->bb:Ljava/nio/ByteBuffer;

    invoke-virtual {v0, p1}, Ljava/nio/ByteBuffer;->getInt(I)I

    move-result v0

    add-int/2addr p1, v0

    .line 95
    iget-object v0, p0, Lcom/google/flatbuffers/Table;->bb:Ljava/nio/ByteBuffer;

    invoke-virtual {v0, p1}, Ljava/nio/ByteBuffer;->getInt(I)I

    move-result v0

    .line 96
    iget-object v1, p0, Lcom/google/flatbuffers/Table;->utf8:Lcom/google/flatbuffers/Utf8;

    iget-object v2, p0, Lcom/google/flatbuffers/Table;->bb:Ljava/nio/ByteBuffer;

    add-int/lit8 p1, p1, 0x4

    invoke-virtual {v1, v2, p1, v0}, Lcom/google/flatbuffers/Utf8;->decodeUtf8(Ljava/nio/ByteBuffer;II)Ljava/lang/String;

    move-result-object p1

    return-object p1
.end method

.method protected __union(Lcom/google/flatbuffers/Table;I)Lcom/google/flatbuffers/Table;
    .locals 2

    .line 172
    iget v0, p0, Lcom/google/flatbuffers/Table;->bb_pos:I

    add-int/2addr p2, v0

    .line 173
    iget-object v0, p0, Lcom/google/flatbuffers/Table;->bb:Ljava/nio/ByteBuffer;

    invoke-virtual {v0, p2}, Ljava/nio/ByteBuffer;->getInt(I)I

    move-result v0

    add-int/2addr p2, v0

    iput p2, p1, Lcom/google/flatbuffers/Table;->bb_pos:I

    .line 174
    iget-object p2, p0, Lcom/google/flatbuffers/Table;->bb:Ljava/nio/ByteBuffer;

    iput-object p2, p1, Lcom/google/flatbuffers/Table;->bb:Ljava/nio/ByteBuffer;

    .line 175
    iget p2, p1, Lcom/google/flatbuffers/Table;->bb_pos:I

    iget-object v0, p0, Lcom/google/flatbuffers/Table;->bb:Ljava/nio/ByteBuffer;

    iget v1, p1, Lcom/google/flatbuffers/Table;->bb_pos:I

    invoke-virtual {v0, v1}, Ljava/nio/ByteBuffer;->getInt(I)I

    move-result v0

    sub-int/2addr p2, v0

    iput p2, p1, Lcom/google/flatbuffers/Table;->vtable_start:I

    .line 176
    iget-object p2, p0, Lcom/google/flatbuffers/Table;->bb:Ljava/nio/ByteBuffer;

    iget v0, p1, Lcom/google/flatbuffers/Table;->vtable_start:I

    invoke-virtual {p2, v0}, Ljava/nio/ByteBuffer;->getShort(I)S

    move-result p2

    iput p2, p1, Lcom/google/flatbuffers/Table;->vtable_size:I

    return-object p1
.end method

.method protected __vector(I)I
    .locals 1

    .line 118
    iget v0, p0, Lcom/google/flatbuffers/Table;->bb_pos:I

    add-int/2addr p1, v0

    .line 119
    iget-object v0, p0, Lcom/google/flatbuffers/Table;->bb:Ljava/nio/ByteBuffer;

    invoke-virtual {v0, p1}, Ljava/nio/ByteBuffer;->getInt(I)I

    move-result v0

    add-int/2addr p1, v0

    add-int/lit8 p1, p1, 0x4

    return p1
.end method

.method protected __vector_as_bytebuffer(II)Ljava/nio/ByteBuffer;
    .locals 2

    .line 134
    invoke-virtual {p0, p1}, Lcom/google/flatbuffers/Table;->__offset(I)I

    move-result p1

    if-nez p1, :cond_0

    const/4 p1, 0x0

    return-object p1

    .line 136
    :cond_0
    iget-object v0, p0, Lcom/google/flatbuffers/Table;->bb:Ljava/nio/ByteBuffer;

    invoke-virtual {v0}, Ljava/nio/ByteBuffer;->duplicate()Ljava/nio/ByteBuffer;

    move-result-object v0

    sget-object v1, Ljava/nio/ByteOrder;->LITTLE_ENDIAN:Ljava/nio/ByteOrder;

    invoke-virtual {v0, v1}, Ljava/nio/ByteBuffer;->order(Ljava/nio/ByteOrder;)Ljava/nio/ByteBuffer;

    move-result-object v0

    .line 137
    invoke-virtual {p0, p1}, Lcom/google/flatbuffers/Table;->__vector(I)I

    move-result v1

    .line 138
    invoke-virtual {v0, v1}, Ljava/nio/ByteBuffer;->position(I)Ljava/nio/Buffer;

    .line 139
    invoke-virtual {p0, p1}, Lcom/google/flatbuffers/Table;->__vector_len(I)I

    move-result p1

    mul-int p1, p1, p2

    add-int/2addr v1, p1

    invoke-virtual {v0, v1}, Ljava/nio/ByteBuffer;->limit(I)Ljava/nio/Buffer;

    return-object v0
.end method

.method protected __vector_in_bytebuffer(Ljava/nio/ByteBuffer;II)Ljava/nio/ByteBuffer;
    .locals 1

    .line 155
    invoke-virtual {p0, p2}, Lcom/google/flatbuffers/Table;->__offset(I)I

    move-result p2

    if-nez p2, :cond_0

    const/4 p1, 0x0

    return-object p1

    .line 157
    :cond_0
    invoke-virtual {p0, p2}, Lcom/google/flatbuffers/Table;->__vector(I)I

    move-result v0

    .line 158
    invoke-virtual {p1}, Ljava/nio/ByteBuffer;->rewind()Ljava/nio/Buffer;

    .line 159
    invoke-virtual {p0, p2}, Lcom/google/flatbuffers/Table;->__vector_len(I)I

    move-result p2

    mul-int p2, p2, p3

    add-int/2addr p2, v0

    invoke-virtual {p1, p2}, Ljava/nio/ByteBuffer;->limit(I)Ljava/nio/Buffer;

    .line 160
    invoke-virtual {p1, v0}, Ljava/nio/ByteBuffer;->position(I)Ljava/nio/Buffer;

    return-object p1
.end method

.method protected __vector_len(I)I
    .locals 1

    .line 106
    iget v0, p0, Lcom/google/flatbuffers/Table;->bb_pos:I

    add-int/2addr p1, v0

    .line 107
    iget-object v0, p0, Lcom/google/flatbuffers/Table;->bb:Ljava/nio/ByteBuffer;

    invoke-virtual {v0, p1}, Ljava/nio/ByteBuffer;->getInt(I)I

    move-result v0

    add-int/2addr p1, v0

    .line 108
    iget-object v0, p0, Lcom/google/flatbuffers/Table;->bb:Ljava/nio/ByteBuffer;

    invoke-virtual {v0, p1}, Ljava/nio/ByteBuffer;->getInt(I)I

    move-result p1

    return p1
.end method

.method public getByteBuffer()Ljava/nio/ByteBuffer;
    .locals 1

    .line 51
    iget-object v0, p0, Lcom/google/flatbuffers/Table;->bb:Ljava/nio/ByteBuffer;

    return-object v0
.end method

.method protected keysCompare(Ljava/lang/Integer;Ljava/lang/Integer;Ljava/nio/ByteBuffer;)I
    .locals 0

    const/4 p1, 0x0

    return p1
.end method

.method protected sortTables([ILjava/nio/ByteBuffer;)V
    .locals 4

    .line 205
    array-length v0, p1

    new-array v0, v0, [Ljava/lang/Integer;

    const/4 v1, 0x0

    const/4 v2, 0x0

    .line 206
    :goto_0
    array-length v3, p1

    if-ge v2, v3, :cond_0

    aget v3, p1, v2

    invoke-static {v3}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v3

    aput-object v3, v0, v2

    add-int/lit8 v2, v2, 0x1

    goto :goto_0

    .line 207
    :cond_0
    new-instance v2, Lcom/google/flatbuffers/Table$2;

    invoke-direct {v2, p0, p2}, Lcom/google/flatbuffers/Table$2;-><init>(Lcom/google/flatbuffers/Table;Ljava/nio/ByteBuffer;)V

    invoke-static {v0, v2}, Ljava/util/Arrays;->sort([Ljava/lang/Object;Ljava/util/Comparator;)V

    .line 212
    :goto_1
    array-length p2, p1

    if-ge v1, p2, :cond_1

    aget-object p2, v0, v1

    invoke-virtual {p2}, Ljava/lang/Integer;->intValue()I

    move-result p2

    aput p2, p1, v1

    add-int/lit8 v1, v1, 0x1

    goto :goto_1

    :cond_1
    return-void
.end method
