.class public Lcom/google/flatbuffers/FlatBufferBuilder;
.super Ljava/lang/Object;
.source "FlatBufferBuilder.java"


# annotations
.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Lcom/google/flatbuffers/FlatBufferBuilder$ByteBufferBackedInputStream;,
        Lcom/google/flatbuffers/FlatBufferBuilder$HeapByteBufferFactory;,
        Lcom/google/flatbuffers/FlatBufferBuilder$ByteBufferFactory;
    }
.end annotation


# static fields
.field static final synthetic $assertionsDisabled:Z


# instance fields
.field bb:Ljava/nio/ByteBuffer;

.field bb_factory:Lcom/google/flatbuffers/FlatBufferBuilder$ByteBufferFactory;

.field finished:Z

.field force_defaults:Z

.field minalign:I

.field nested:Z

.field num_vtables:I

.field object_start:I

.field space:I

.field final utf8:Lcom/google/flatbuffers/Utf8;

.field vector_num_elems:I

.field vtable:[I

.field vtable_in_use:I

.field vtables:[I


# direct methods
.method static constructor <clinit>()V
    .locals 0

    return-void
.end method

.method public constructor <init>()V
    .locals 1

    const/16 v0, 0x400

    .line 100
    invoke-direct {p0, v0}, Lcom/google/flatbuffers/FlatBufferBuilder;-><init>(I)V

    return-void
.end method

.method public constructor <init>(I)V
    .locals 3

    .line 93
    sget-object v0, Lcom/google/flatbuffers/FlatBufferBuilder$HeapByteBufferFactory;->INSTANCE:Lcom/google/flatbuffers/FlatBufferBuilder$HeapByteBufferFactory;

    invoke-static {}, Lcom/google/flatbuffers/Utf8;->getDefault()Lcom/google/flatbuffers/Utf8;

    move-result-object v1

    const/4 v2, 0x0

    invoke-direct {p0, p1, v0, v2, v1}, Lcom/google/flatbuffers/FlatBufferBuilder;-><init>(ILcom/google/flatbuffers/FlatBufferBuilder$ByteBufferFactory;Ljava/nio/ByteBuffer;Lcom/google/flatbuffers/Utf8;)V

    return-void
.end method

.method public constructor <init>(ILcom/google/flatbuffers/FlatBufferBuilder$ByteBufferFactory;)V
    .locals 2

    .line 59
    invoke-static {}, Lcom/google/flatbuffers/Utf8;->getDefault()Lcom/google/flatbuffers/Utf8;

    move-result-object v0

    const/4 v1, 0x0

    invoke-direct {p0, p1, p2, v1, v0}, Lcom/google/flatbuffers/FlatBufferBuilder;-><init>(ILcom/google/flatbuffers/FlatBufferBuilder$ByteBufferFactory;Ljava/nio/ByteBuffer;Lcom/google/flatbuffers/Utf8;)V

    return-void
.end method

.method public constructor <init>(ILcom/google/flatbuffers/FlatBufferBuilder$ByteBufferFactory;Ljava/nio/ByteBuffer;Lcom/google/flatbuffers/Utf8;)V
    .locals 3

    .line 71
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    const/4 v0, 0x1

    .line 38
    iput v0, p0, Lcom/google/flatbuffers/FlatBufferBuilder;->minalign:I

    const/4 v1, 0x0

    .line 39
    iput-object v1, p0, Lcom/google/flatbuffers/FlatBufferBuilder;->vtable:[I

    const/4 v1, 0x0

    .line 40
    iput v1, p0, Lcom/google/flatbuffers/FlatBufferBuilder;->vtable_in_use:I

    .line 41
    iput-boolean v1, p0, Lcom/google/flatbuffers/FlatBufferBuilder;->nested:Z

    .line 42
    iput-boolean v1, p0, Lcom/google/flatbuffers/FlatBufferBuilder;->finished:Z

    const/16 v2, 0x10

    .line 44
    new-array v2, v2, [I

    iput-object v2, p0, Lcom/google/flatbuffers/FlatBufferBuilder;->vtables:[I

    .line 45
    iput v1, p0, Lcom/google/flatbuffers/FlatBufferBuilder;->num_vtables:I

    .line 46
    iput v1, p0, Lcom/google/flatbuffers/FlatBufferBuilder;->vector_num_elems:I

    .line 47
    iput-boolean v1, p0, Lcom/google/flatbuffers/FlatBufferBuilder;->force_defaults:Z

    if-gtz p1, :cond_0

    const/4 p1, 0x1

    .line 75
    :cond_0
    iput p1, p0, Lcom/google/flatbuffers/FlatBufferBuilder;->space:I

    .line 76
    iput-object p2, p0, Lcom/google/flatbuffers/FlatBufferBuilder;->bb_factory:Lcom/google/flatbuffers/FlatBufferBuilder$ByteBufferFactory;

    if-eqz p3, :cond_1

    .line 78
    iput-object p3, p0, Lcom/google/flatbuffers/FlatBufferBuilder;->bb:Ljava/nio/ByteBuffer;

    .line 79
    iget-object p1, p0, Lcom/google/flatbuffers/FlatBufferBuilder;->bb:Ljava/nio/ByteBuffer;

    invoke-virtual {p1}, Ljava/nio/ByteBuffer;->clear()Ljava/nio/Buffer;

    .line 80
    iget-object p1, p0, Lcom/google/flatbuffers/FlatBufferBuilder;->bb:Ljava/nio/ByteBuffer;

    sget-object p2, Ljava/nio/ByteOrder;->LITTLE_ENDIAN:Ljava/nio/ByteOrder;

    invoke-virtual {p1, p2}, Ljava/nio/ByteBuffer;->order(Ljava/nio/ByteOrder;)Ljava/nio/ByteBuffer;

    goto :goto_0

    .line 82
    :cond_1
    invoke-virtual {p2, p1}, Lcom/google/flatbuffers/FlatBufferBuilder$ByteBufferFactory;->newByteBuffer(I)Ljava/nio/ByteBuffer;

    move-result-object p1

    iput-object p1, p0, Lcom/google/flatbuffers/FlatBufferBuilder;->bb:Ljava/nio/ByteBuffer;

    .line 84
    :goto_0
    iput-object p4, p0, Lcom/google/flatbuffers/FlatBufferBuilder;->utf8:Lcom/google/flatbuffers/Utf8;

    return-void
.end method

.method public constructor <init>(Ljava/nio/ByteBuffer;)V
    .locals 1

    .line 124
    new-instance v0, Lcom/google/flatbuffers/FlatBufferBuilder$HeapByteBufferFactory;

    invoke-direct {v0}, Lcom/google/flatbuffers/FlatBufferBuilder$HeapByteBufferFactory;-><init>()V

    invoke-direct {p0, p1, v0}, Lcom/google/flatbuffers/FlatBufferBuilder;-><init>(Ljava/nio/ByteBuffer;Lcom/google/flatbuffers/FlatBufferBuilder$ByteBufferFactory;)V

    return-void
.end method

.method public constructor <init>(Ljava/nio/ByteBuffer;Lcom/google/flatbuffers/FlatBufferBuilder$ByteBufferFactory;)V
    .locals 2

    .line 113
    invoke-virtual {p1}, Ljava/nio/ByteBuffer;->capacity()I

    move-result v0

    invoke-static {}, Lcom/google/flatbuffers/Utf8;->getDefault()Lcom/google/flatbuffers/Utf8;

    move-result-object v1

    invoke-direct {p0, v0, p2, p1, v1}, Lcom/google/flatbuffers/FlatBufferBuilder;-><init>(ILcom/google/flatbuffers/FlatBufferBuilder$ByteBufferFactory;Ljava/nio/ByteBuffer;Lcom/google/flatbuffers/Utf8;)V

    return-void
.end method

.method private dataStart()I
    .locals 1
    .annotation runtime Ljava/lang/Deprecated;
    .end annotation

    .line 946
    invoke-virtual {p0}, Lcom/google/flatbuffers/FlatBufferBuilder;->finished()V

    .line 947
    iget v0, p0, Lcom/google/flatbuffers/FlatBufferBuilder;->space:I

    return v0
.end method

.method static growByteBuffer(Ljava/nio/ByteBuffer;Lcom/google/flatbuffers/FlatBufferBuilder$ByteBufferFactory;)Ljava/nio/ByteBuffer;
    .locals 3

    .line 228
    invoke-virtual {p0}, Ljava/nio/ByteBuffer;->capacity()I

    move-result v0

    const/high16 v1, -0x40000000    # -2.0f

    and-int/2addr v1, v0

    if-nez v1, :cond_1

    if-nez v0, :cond_0

    const/4 v1, 0x1

    goto :goto_0

    :cond_0
    shl-int/lit8 v1, v0, 0x1

    :goto_0
    const/4 v2, 0x0

    .line 232
    invoke-virtual {p0, v2}, Ljava/nio/ByteBuffer;->position(I)Ljava/nio/Buffer;

    .line 233
    invoke-virtual {p1, v1}, Lcom/google/flatbuffers/FlatBufferBuilder$ByteBufferFactory;->newByteBuffer(I)Ljava/nio/ByteBuffer;

    move-result-object p1

    sub-int/2addr v1, v0

    .line 234
    invoke-virtual {p1, v1}, Ljava/nio/ByteBuffer;->position(I)Ljava/nio/Buffer;

    .line 235
    invoke-virtual {p1, p0}, Ljava/nio/ByteBuffer;->put(Ljava/nio/ByteBuffer;)Ljava/nio/ByteBuffer;

    return-object p1

    .line 230
    :cond_1
    new-instance p0, Ljava/lang/AssertionError;

    const-string p1, "FlatBuffers: cannot grow buffer beyond 2 gigabytes."

    invoke-direct {p0, p1}, Ljava/lang/AssertionError;-><init>(Ljava/lang/Object;)V

    throw p0
.end method


# virtual methods
.method public Nested(I)V
    .locals 1

    .line 591
    invoke-virtual {p0}, Lcom/google/flatbuffers/FlatBufferBuilder;->offset()I

    move-result v0

    if-ne p1, v0, :cond_0

    return-void

    .line 592
    :cond_0
    new-instance p1, Ljava/lang/AssertionError;

    const-string v0, "FlatBuffers: struct must be serialized inline."

    invoke-direct {p1, v0}, Ljava/lang/AssertionError;-><init>(Ljava/lang/Object;)V

    throw p1
.end method

.method public addBoolean(IZZ)V
    .locals 1

    .line 653
    iget-boolean v0, p0, Lcom/google/flatbuffers/FlatBufferBuilder;->force_defaults:Z

    if-nez v0, :cond_0

    if-eq p2, p3, :cond_1

    :cond_0
    invoke-virtual {p0, p2}, Lcom/google/flatbuffers/FlatBufferBuilder;->addBoolean(Z)V

    invoke-virtual {p0, p1}, Lcom/google/flatbuffers/FlatBufferBuilder;->slot(I)V

    :cond_1
    return-void
.end method

.method public addBoolean(Z)V
    .locals 2

    const/4 v0, 0x1

    const/4 v1, 0x0

    .line 348
    invoke-virtual {p0, v0, v1}, Lcom/google/flatbuffers/FlatBufferBuilder;->prep(II)V

    invoke-virtual {p0, p1}, Lcom/google/flatbuffers/FlatBufferBuilder;->putBoolean(Z)V

    return-void
.end method

.method public addByte(B)V
    .locals 2

    const/4 v0, 0x1

    const/4 v1, 0x0

    .line 355
    invoke-virtual {p0, v0, v1}, Lcom/google/flatbuffers/FlatBufferBuilder;->prep(II)V

    invoke-virtual {p0, p1}, Lcom/google/flatbuffers/FlatBufferBuilder;->putByte(B)V

    return-void
.end method

.method public addByte(IBI)V
    .locals 1

    .line 664
    iget-boolean v0, p0, Lcom/google/flatbuffers/FlatBufferBuilder;->force_defaults:Z

    if-nez v0, :cond_0

    if-eq p2, p3, :cond_1

    :cond_0
    invoke-virtual {p0, p2}, Lcom/google/flatbuffers/FlatBufferBuilder;->addByte(B)V

    invoke-virtual {p0, p1}, Lcom/google/flatbuffers/FlatBufferBuilder;->slot(I)V

    :cond_1
    return-void
.end method

.method public addDouble(D)V
    .locals 2

    const/16 v0, 0x8

    const/4 v1, 0x0

    .line 390
    invoke-virtual {p0, v0, v1}, Lcom/google/flatbuffers/FlatBufferBuilder;->prep(II)V

    invoke-virtual {p0, p1, p2}, Lcom/google/flatbuffers/FlatBufferBuilder;->putDouble(D)V

    return-void
.end method

.method public addDouble(IDD)V
    .locals 1

    .line 719
    iget-boolean v0, p0, Lcom/google/flatbuffers/FlatBufferBuilder;->force_defaults:Z

    if-nez v0, :cond_0

    cmpl-double v0, p2, p4

    if-eqz v0, :cond_1

    :cond_0
    invoke-virtual {p0, p2, p3}, Lcom/google/flatbuffers/FlatBufferBuilder;->addDouble(D)V

    invoke-virtual {p0, p1}, Lcom/google/flatbuffers/FlatBufferBuilder;->slot(I)V

    :cond_1
    return-void
.end method

.method public addFloat(F)V
    .locals 2

    const/4 v0, 0x4

    const/4 v1, 0x0

    .line 383
    invoke-virtual {p0, v0, v1}, Lcom/google/flatbuffers/FlatBufferBuilder;->prep(II)V

    invoke-virtual {p0, p1}, Lcom/google/flatbuffers/FlatBufferBuilder;->putFloat(F)V

    return-void
.end method

.method public addFloat(IFD)V
    .locals 3

    .line 708
    iget-boolean v0, p0, Lcom/google/flatbuffers/FlatBufferBuilder;->force_defaults:Z

    if-nez v0, :cond_0

    float-to-double v0, p2

    cmpl-double v2, v0, p3

    if-eqz v2, :cond_1

    :cond_0
    invoke-virtual {p0, p2}, Lcom/google/flatbuffers/FlatBufferBuilder;->addFloat(F)V

    invoke-virtual {p0, p1}, Lcom/google/flatbuffers/FlatBufferBuilder;->slot(I)V

    :cond_1
    return-void
.end method

.method public addInt(I)V
    .locals 2

    const/4 v0, 0x4

    const/4 v1, 0x0

    .line 369
    invoke-virtual {p0, v0, v1}, Lcom/google/flatbuffers/FlatBufferBuilder;->prep(II)V

    invoke-virtual {p0, p1}, Lcom/google/flatbuffers/FlatBufferBuilder;->putInt(I)V

    return-void
.end method

.method public addInt(III)V
    .locals 1

    .line 686
    iget-boolean v0, p0, Lcom/google/flatbuffers/FlatBufferBuilder;->force_defaults:Z

    if-nez v0, :cond_0

    if-eq p2, p3, :cond_1

    :cond_0
    invoke-virtual {p0, p2}, Lcom/google/flatbuffers/FlatBufferBuilder;->addInt(I)V

    invoke-virtual {p0, p1}, Lcom/google/flatbuffers/FlatBufferBuilder;->slot(I)V

    :cond_1
    return-void
.end method

.method public addLong(IJJ)V
    .locals 1

    .line 697
    iget-boolean v0, p0, Lcom/google/flatbuffers/FlatBufferBuilder;->force_defaults:Z

    if-nez v0, :cond_0

    cmp-long v0, p2, p4

    if-eqz v0, :cond_1

    :cond_0
    invoke-virtual {p0, p2, p3}, Lcom/google/flatbuffers/FlatBufferBuilder;->addLong(J)V

    invoke-virtual {p0, p1}, Lcom/google/flatbuffers/FlatBufferBuilder;->slot(I)V

    :cond_1
    return-void
.end method

.method public addLong(J)V
    .locals 2

    const/16 v0, 0x8

    const/4 v1, 0x0

    .line 376
    invoke-virtual {p0, v0, v1}, Lcom/google/flatbuffers/FlatBufferBuilder;->prep(II)V

    invoke-virtual {p0, p1, p2}, Lcom/google/flatbuffers/FlatBufferBuilder;->putLong(J)V

    return-void
.end method

.method public addOffset(I)V
    .locals 2

    const/4 v0, 0x4

    const/4 v1, 0x0

    .line 398
    invoke-virtual {p0, v0, v1}, Lcom/google/flatbuffers/FlatBufferBuilder;->prep(II)V

    .line 400
    invoke-virtual {p0}, Lcom/google/flatbuffers/FlatBufferBuilder;->offset()I

    move-result v1

    sub-int/2addr v1, p1

    add-int/2addr v1, v0

    .line 401
    invoke-virtual {p0, v1}, Lcom/google/flatbuffers/FlatBufferBuilder;->putInt(I)V

    return-void
.end method

.method public addOffset(III)V
    .locals 1

    .line 730
    iget-boolean v0, p0, Lcom/google/flatbuffers/FlatBufferBuilder;->force_defaults:Z

    if-nez v0, :cond_0

    if-eq p2, p3, :cond_1

    :cond_0
    invoke-virtual {p0, p2}, Lcom/google/flatbuffers/FlatBufferBuilder;->addOffset(I)V

    invoke-virtual {p0, p1}, Lcom/google/flatbuffers/FlatBufferBuilder;->slot(I)V

    :cond_1
    return-void
.end method

.method public addShort(ISI)V
    .locals 1

    .line 675
    iget-boolean v0, p0, Lcom/google/flatbuffers/FlatBufferBuilder;->force_defaults:Z

    if-nez v0, :cond_0

    if-eq p2, p3, :cond_1

    :cond_0
    invoke-virtual {p0, p2}, Lcom/google/flatbuffers/FlatBufferBuilder;->addShort(S)V

    invoke-virtual {p0, p1}, Lcom/google/flatbuffers/FlatBufferBuilder;->slot(I)V

    :cond_1
    return-void
.end method

.method public addShort(S)V
    .locals 2

    const/4 v0, 0x2

    const/4 v1, 0x0

    .line 362
    invoke-virtual {p0, v0, v1}, Lcom/google/flatbuffers/FlatBufferBuilder;->prep(II)V

    invoke-virtual {p0, p1}, Lcom/google/flatbuffers/FlatBufferBuilder;->putShort(S)V

    return-void
.end method

.method public addStruct(III)V
    .locals 0

    if-eq p2, p3, :cond_0

    .line 741
    invoke-virtual {p0, p2}, Lcom/google/flatbuffers/FlatBufferBuilder;->Nested(I)V

    .line 742
    invoke-virtual {p0, p1}, Lcom/google/flatbuffers/FlatBufferBuilder;->slot(I)V

    :cond_0
    return-void
.end method

.method public clear()V
    .locals 4

    .line 206
    iget-object v0, p0, Lcom/google/flatbuffers/FlatBufferBuilder;->bb:Ljava/nio/ByteBuffer;

    invoke-virtual {v0}, Ljava/nio/ByteBuffer;->capacity()I

    move-result v0

    iput v0, p0, Lcom/google/flatbuffers/FlatBufferBuilder;->space:I

    .line 207
    iget-object v0, p0, Lcom/google/flatbuffers/FlatBufferBuilder;->bb:Ljava/nio/ByteBuffer;

    invoke-virtual {v0}, Ljava/nio/ByteBuffer;->clear()Ljava/nio/Buffer;

    const/4 v0, 0x1

    .line 208
    iput v0, p0, Lcom/google/flatbuffers/FlatBufferBuilder;->minalign:I

    .line 209
    :goto_0
    iget v1, p0, Lcom/google/flatbuffers/FlatBufferBuilder;->vtable_in_use:I

    const/4 v2, 0x0

    if-lez v1, :cond_0

    iget-object v1, p0, Lcom/google/flatbuffers/FlatBufferBuilder;->vtable:[I

    iget v3, p0, Lcom/google/flatbuffers/FlatBufferBuilder;->vtable_in_use:I

    sub-int/2addr v3, v0

    iput v3, p0, Lcom/google/flatbuffers/FlatBufferBuilder;->vtable_in_use:I

    aput v2, v1, v3

    goto :goto_0

    .line 210
    :cond_0
    iput v2, p0, Lcom/google/flatbuffers/FlatBufferBuilder;->vtable_in_use:I

    .line 211
    iput-boolean v2, p0, Lcom/google/flatbuffers/FlatBufferBuilder;->nested:Z

    .line 212
    iput-boolean v2, p0, Lcom/google/flatbuffers/FlatBufferBuilder;->finished:Z

    .line 213
    iput v2, p0, Lcom/google/flatbuffers/FlatBufferBuilder;->object_start:I

    .line 214
    iput v2, p0, Lcom/google/flatbuffers/FlatBufferBuilder;->num_vtables:I

    .line 215
    iput v2, p0, Lcom/google/flatbuffers/FlatBufferBuilder;->vector_num_elems:I

    return-void
.end method

.method public createByteVector([B)I
    .locals 3

    .line 556
    array-length v0, p1

    const/4 v1, 0x1

    .line 557
    invoke-virtual {p0, v1, v0, v1}, Lcom/google/flatbuffers/FlatBufferBuilder;->startVector(III)V

    .line 558
    iget-object v1, p0, Lcom/google/flatbuffers/FlatBufferBuilder;->bb:Ljava/nio/ByteBuffer;

    iget v2, p0, Lcom/google/flatbuffers/FlatBufferBuilder;->space:I

    sub-int/2addr v2, v0

    iput v2, p0, Lcom/google/flatbuffers/FlatBufferBuilder;->space:I

    invoke-virtual {v1, v2}, Ljava/nio/ByteBuffer;->position(I)Ljava/nio/Buffer;

    .line 559
    iget-object v0, p0, Lcom/google/flatbuffers/FlatBufferBuilder;->bb:Ljava/nio/ByteBuffer;

    invoke-virtual {v0, p1}, Ljava/nio/ByteBuffer;->put([B)Ljava/nio/ByteBuffer;

    .line 560
    invoke-virtual {p0}, Lcom/google/flatbuffers/FlatBufferBuilder;->endVector()I

    move-result p1

    return p1
.end method

.method public createSortedVectorOfTables(Lcom/google/flatbuffers/Table;[I)I
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "<T:",
            "Lcom/google/flatbuffers/Table;",
            ">(TT;[I)I"
        }
    .end annotation

    .line 514
    iget-object v0, p0, Lcom/google/flatbuffers/FlatBufferBuilder;->bb:Ljava/nio/ByteBuffer;

    invoke-virtual {p1, p2, v0}, Lcom/google/flatbuffers/Table;->sortTables([ILjava/nio/ByteBuffer;)V

    .line 515
    invoke-virtual {p0, p2}, Lcom/google/flatbuffers/FlatBufferBuilder;->createVectorOfTables([I)I

    move-result p1

    return p1
.end method

.method public createString(Ljava/lang/CharSequence;)I
    .locals 3

    .line 526
    iget-object v0, p0, Lcom/google/flatbuffers/FlatBufferBuilder;->utf8:Lcom/google/flatbuffers/Utf8;

    invoke-virtual {v0, p1}, Lcom/google/flatbuffers/Utf8;->encodedLength(Ljava/lang/CharSequence;)I

    move-result v0

    const/4 v1, 0x0

    .line 527
    invoke-virtual {p0, v1}, Lcom/google/flatbuffers/FlatBufferBuilder;->addByte(B)V

    const/4 v1, 0x1

    .line 528
    invoke-virtual {p0, v1, v0, v1}, Lcom/google/flatbuffers/FlatBufferBuilder;->startVector(III)V

    .line 529
    iget-object v1, p0, Lcom/google/flatbuffers/FlatBufferBuilder;->bb:Ljava/nio/ByteBuffer;

    iget v2, p0, Lcom/google/flatbuffers/FlatBufferBuilder;->space:I

    sub-int/2addr v2, v0

    iput v2, p0, Lcom/google/flatbuffers/FlatBufferBuilder;->space:I

    invoke-virtual {v1, v2}, Ljava/nio/ByteBuffer;->position(I)Ljava/nio/Buffer;

    .line 530
    iget-object v0, p0, Lcom/google/flatbuffers/FlatBufferBuilder;->utf8:Lcom/google/flatbuffers/Utf8;

    iget-object v1, p0, Lcom/google/flatbuffers/FlatBufferBuilder;->bb:Ljava/nio/ByteBuffer;

    invoke-virtual {v0, p1, v1}, Lcom/google/flatbuffers/Utf8;->encodeUtf8(Ljava/lang/CharSequence;Ljava/nio/ByteBuffer;)V

    .line 531
    invoke-virtual {p0}, Lcom/google/flatbuffers/FlatBufferBuilder;->endVector()I

    move-result p1

    return p1
.end method

.method public createString(Ljava/nio/ByteBuffer;)I
    .locals 3

    .line 541
    invoke-virtual {p1}, Ljava/nio/ByteBuffer;->remaining()I

    move-result v0

    const/4 v1, 0x0

    .line 542
    invoke-virtual {p0, v1}, Lcom/google/flatbuffers/FlatBufferBuilder;->addByte(B)V

    const/4 v1, 0x1

    .line 543
    invoke-virtual {p0, v1, v0, v1}, Lcom/google/flatbuffers/FlatBufferBuilder;->startVector(III)V

    .line 544
    iget-object v1, p0, Lcom/google/flatbuffers/FlatBufferBuilder;->bb:Ljava/nio/ByteBuffer;

    iget v2, p0, Lcom/google/flatbuffers/FlatBufferBuilder;->space:I

    sub-int/2addr v2, v0

    iput v2, p0, Lcom/google/flatbuffers/FlatBufferBuilder;->space:I

    invoke-virtual {v1, v2}, Ljava/nio/ByteBuffer;->position(I)Ljava/nio/Buffer;

    .line 545
    iget-object v0, p0, Lcom/google/flatbuffers/FlatBufferBuilder;->bb:Ljava/nio/ByteBuffer;

    invoke-virtual {v0, p1}, Ljava/nio/ByteBuffer;->put(Ljava/nio/ByteBuffer;)Ljava/nio/ByteBuffer;

    .line 546
    invoke-virtual {p0}, Lcom/google/flatbuffers/FlatBufferBuilder;->endVector()I

    move-result p1

    return p1
.end method

.method public createUnintializedVector(III)Ljava/nio/ByteBuffer;
    .locals 1

    mul-int v0, p1, p2

    .line 483
    invoke-virtual {p0, p1, p2, p3}, Lcom/google/flatbuffers/FlatBufferBuilder;->startVector(III)V

    .line 485
    iget-object p1, p0, Lcom/google/flatbuffers/FlatBufferBuilder;->bb:Ljava/nio/ByteBuffer;

    iget p2, p0, Lcom/google/flatbuffers/FlatBufferBuilder;->space:I

    sub-int/2addr p2, v0

    iput p2, p0, Lcom/google/flatbuffers/FlatBufferBuilder;->space:I

    invoke-virtual {p1, p2}, Ljava/nio/ByteBuffer;->position(I)Ljava/nio/Buffer;

    .line 488
    iget-object p1, p0, Lcom/google/flatbuffers/FlatBufferBuilder;->bb:Ljava/nio/ByteBuffer;

    invoke-virtual {p1}, Ljava/nio/ByteBuffer;->slice()Ljava/nio/ByteBuffer;

    move-result-object p1

    sget-object p2, Ljava/nio/ByteOrder;->LITTLE_ENDIAN:Ljava/nio/ByteOrder;

    invoke-virtual {p1, p2}, Ljava/nio/ByteBuffer;->order(Ljava/nio/ByteOrder;)Ljava/nio/ByteBuffer;

    move-result-object p1

    .line 489
    invoke-virtual {p1, v0}, Ljava/nio/ByteBuffer;->limit(I)Ljava/nio/Buffer;

    return-object p1
.end method

.method public createVectorOfTables([I)I
    .locals 2

    .line 500
    invoke-virtual {p0}, Lcom/google/flatbuffers/FlatBufferBuilder;->notNested()V

    .line 501
    array-length v0, p1

    const/4 v1, 0x4

    invoke-virtual {p0, v1, v0, v1}, Lcom/google/flatbuffers/FlatBufferBuilder;->startVector(III)V

    .line 502
    array-length v0, p1

    add-int/lit8 v0, v0, -0x1

    :goto_0
    if-ltz v0, :cond_0

    aget v1, p1, v0

    invoke-virtual {p0, v1}, Lcom/google/flatbuffers/FlatBufferBuilder;->addOffset(I)V

    add-int/lit8 v0, v0, -0x1

    goto :goto_0

    .line 503
    :cond_0
    invoke-virtual {p0}, Lcom/google/flatbuffers/FlatBufferBuilder;->endVector()I

    move-result p1

    return p1
.end method

.method public dataBuffer()Ljava/nio/ByteBuffer;
    .locals 1

    .line 932
    invoke-virtual {p0}, Lcom/google/flatbuffers/FlatBufferBuilder;->finished()V

    .line 933
    iget-object v0, p0, Lcom/google/flatbuffers/FlatBufferBuilder;->bb:Ljava/nio/ByteBuffer;

    return-object v0
.end method

.method public endObject()I
    .locals 11

    .line 763
    iget-object v0, p0, Lcom/google/flatbuffers/FlatBufferBuilder;->vtable:[I

    if-eqz v0, :cond_9

    iget-boolean v0, p0, Lcom/google/flatbuffers/FlatBufferBuilder;->nested:Z

    if-eqz v0, :cond_9

    const/4 v0, 0x0

    .line 765
    invoke-virtual {p0, v0}, Lcom/google/flatbuffers/FlatBufferBuilder;->addInt(I)V

    .line 766
    invoke-virtual {p0}, Lcom/google/flatbuffers/FlatBufferBuilder;->offset()I

    move-result v1

    .line 768
    iget v2, p0, Lcom/google/flatbuffers/FlatBufferBuilder;->vtable_in_use:I

    add-int/lit8 v2, v2, -0x1

    :goto_0
    if-ltz v2, :cond_0

    .line 770
    iget-object v3, p0, Lcom/google/flatbuffers/FlatBufferBuilder;->vtable:[I

    aget v3, v3, v2

    if-nez v3, :cond_0

    add-int/lit8 v2, v2, -0x1

    goto :goto_0

    :cond_0
    add-int/lit8 v3, v2, 0x1

    :goto_1
    if-ltz v2, :cond_2

    .line 774
    iget-object v4, p0, Lcom/google/flatbuffers/FlatBufferBuilder;->vtable:[I

    aget v4, v4, v2

    if-eqz v4, :cond_1

    iget-object v4, p0, Lcom/google/flatbuffers/FlatBufferBuilder;->vtable:[I

    aget v4, v4, v2

    sub-int v4, v1, v4

    goto :goto_2

    :cond_1
    const/4 v4, 0x0

    :goto_2
    int-to-short v4, v4

    .line 775
    invoke-virtual {p0, v4}, Lcom/google/flatbuffers/FlatBufferBuilder;->addShort(S)V

    add-int/lit8 v2, v2, -0x1

    goto :goto_1

    .line 779
    :cond_2
    iget v2, p0, Lcom/google/flatbuffers/FlatBufferBuilder;->object_start:I

    sub-int v2, v1, v2

    int-to-short v2, v2

    invoke-virtual {p0, v2}, Lcom/google/flatbuffers/FlatBufferBuilder;->addShort(S)V

    const/4 v2, 0x2

    add-int/2addr v3, v2

    mul-int/lit8 v3, v3, 0x2

    int-to-short v3, v3

    .line 780
    invoke-virtual {p0, v3}, Lcom/google/flatbuffers/FlatBufferBuilder;->addShort(S)V

    const/4 v3, 0x0

    .line 785
    :goto_3
    iget v4, p0, Lcom/google/flatbuffers/FlatBufferBuilder;->num_vtables:I

    if-ge v3, v4, :cond_6

    .line 786
    iget-object v4, p0, Lcom/google/flatbuffers/FlatBufferBuilder;->bb:Ljava/nio/ByteBuffer;

    invoke-virtual {v4}, Ljava/nio/ByteBuffer;->capacity()I

    move-result v4

    iget-object v5, p0, Lcom/google/flatbuffers/FlatBufferBuilder;->vtables:[I

    aget v5, v5, v3

    sub-int/2addr v4, v5

    .line 787
    iget v5, p0, Lcom/google/flatbuffers/FlatBufferBuilder;->space:I

    .line 788
    iget-object v6, p0, Lcom/google/flatbuffers/FlatBufferBuilder;->bb:Ljava/nio/ByteBuffer;

    invoke-virtual {v6, v4}, Ljava/nio/ByteBuffer;->getShort(I)S

    move-result v6

    .line 789
    iget-object v7, p0, Lcom/google/flatbuffers/FlatBufferBuilder;->bb:Ljava/nio/ByteBuffer;

    invoke-virtual {v7, v5}, Ljava/nio/ByteBuffer;->getShort(I)S

    move-result v7

    if-ne v6, v7, :cond_5

    const/4 v7, 0x2

    :goto_4
    if-ge v7, v6, :cond_4

    .line 791
    iget-object v8, p0, Lcom/google/flatbuffers/FlatBufferBuilder;->bb:Ljava/nio/ByteBuffer;

    add-int v9, v4, v7

    invoke-virtual {v8, v9}, Ljava/nio/ByteBuffer;->getShort(I)S

    move-result v8

    iget-object v9, p0, Lcom/google/flatbuffers/FlatBufferBuilder;->bb:Ljava/nio/ByteBuffer;

    add-int v10, v5, v7

    invoke-virtual {v9, v10}, Ljava/nio/ByteBuffer;->getShort(I)S

    move-result v9

    if-eq v8, v9, :cond_3

    goto :goto_5

    :cond_3
    add-int/lit8 v7, v7, 0x2

    goto :goto_4

    .line 795
    :cond_4
    iget-object v4, p0, Lcom/google/flatbuffers/FlatBufferBuilder;->vtables:[I

    aget v3, v4, v3

    goto :goto_6

    :cond_5
    :goto_5
    add-int/lit8 v3, v3, 0x1

    goto :goto_3

    :cond_6
    const/4 v3, 0x0

    :goto_6
    if-eqz v3, :cond_7

    .line 803
    iget-object v2, p0, Lcom/google/flatbuffers/FlatBufferBuilder;->bb:Ljava/nio/ByteBuffer;

    invoke-virtual {v2}, Ljava/nio/ByteBuffer;->capacity()I

    move-result v2

    sub-int/2addr v2, v1

    iput v2, p0, Lcom/google/flatbuffers/FlatBufferBuilder;->space:I

    .line 805
    iget-object v2, p0, Lcom/google/flatbuffers/FlatBufferBuilder;->bb:Ljava/nio/ByteBuffer;

    iget v4, p0, Lcom/google/flatbuffers/FlatBufferBuilder;->space:I

    sub-int/2addr v3, v1

    invoke-virtual {v2, v4, v3}, Ljava/nio/ByteBuffer;->putInt(II)Ljava/nio/ByteBuffer;

    goto :goto_7

    .line 809
    :cond_7
    iget v3, p0, Lcom/google/flatbuffers/FlatBufferBuilder;->num_vtables:I

    iget-object v4, p0, Lcom/google/flatbuffers/FlatBufferBuilder;->vtables:[I

    array-length v4, v4

    if-ne v3, v4, :cond_8

    iget-object v3, p0, Lcom/google/flatbuffers/FlatBufferBuilder;->vtables:[I

    iget v4, p0, Lcom/google/flatbuffers/FlatBufferBuilder;->num_vtables:I

    mul-int/lit8 v4, v4, 0x2

    invoke-static {v3, v4}, Ljava/util/Arrays;->copyOf([II)[I

    move-result-object v2

    iput-object v2, p0, Lcom/google/flatbuffers/FlatBufferBuilder;->vtables:[I

    .line 810
    :cond_8
    iget-object v2, p0, Lcom/google/flatbuffers/FlatBufferBuilder;->vtables:[I

    iget v3, p0, Lcom/google/flatbuffers/FlatBufferBuilder;->num_vtables:I

    add-int/lit8 v4, v3, 0x1

    iput v4, p0, Lcom/google/flatbuffers/FlatBufferBuilder;->num_vtables:I

    invoke-virtual {p0}, Lcom/google/flatbuffers/FlatBufferBuilder;->offset()I

    move-result v4

    aput v4, v2, v3

    .line 812
    iget-object v2, p0, Lcom/google/flatbuffers/FlatBufferBuilder;->bb:Ljava/nio/ByteBuffer;

    iget-object v3, p0, Lcom/google/flatbuffers/FlatBufferBuilder;->bb:Ljava/nio/ByteBuffer;

    invoke-virtual {v3}, Ljava/nio/ByteBuffer;->capacity()I

    move-result v3

    sub-int/2addr v3, v1

    invoke-virtual {p0}, Lcom/google/flatbuffers/FlatBufferBuilder;->offset()I

    move-result v4

    sub-int/2addr v4, v1

    invoke-virtual {v2, v3, v4}, Ljava/nio/ByteBuffer;->putInt(II)Ljava/nio/ByteBuffer;

    .line 815
    :goto_7
    iput-boolean v0, p0, Lcom/google/flatbuffers/FlatBufferBuilder;->nested:Z

    return v1

    .line 764
    :cond_9
    new-instance v0, Ljava/lang/AssertionError;

    const-string v1, "FlatBuffers: endObject called without startObject"

    invoke-direct {v0, v1}, Ljava/lang/AssertionError;-><init>(Ljava/lang/Object;)V

    throw v0
.end method

.method public endVector()I
    .locals 2

    .line 463
    iget-boolean v0, p0, Lcom/google/flatbuffers/FlatBufferBuilder;->nested:Z

    if-eqz v0, :cond_0

    const/4 v0, 0x0

    .line 465
    iput-boolean v0, p0, Lcom/google/flatbuffers/FlatBufferBuilder;->nested:Z

    .line 466
    iget v0, p0, Lcom/google/flatbuffers/FlatBufferBuilder;->vector_num_elems:I

    invoke-virtual {p0, v0}, Lcom/google/flatbuffers/FlatBufferBuilder;->putInt(I)V

    .line 467
    invoke-virtual {p0}, Lcom/google/flatbuffers/FlatBufferBuilder;->offset()I

    move-result v0

    return v0

    .line 464
    :cond_0
    new-instance v0, Ljava/lang/AssertionError;

    const-string v1, "FlatBuffers: endVector called without startVector"

    invoke-direct {v0, v1}, Ljava/lang/AssertionError;-><init>(Ljava/lang/Object;)V

    throw v0
.end method

.method public finish(I)V
    .locals 1

    const/4 v0, 0x0

    .line 858
    invoke-virtual {p0, p1, v0}, Lcom/google/flatbuffers/FlatBufferBuilder;->finish(IZ)V

    return-void
.end method

.method public finish(ILjava/lang/String;)V
    .locals 1

    const/4 v0, 0x0

    .line 897
    invoke-virtual {p0, p1, p2, v0}, Lcom/google/flatbuffers/FlatBufferBuilder;->finish(ILjava/lang/String;Z)V

    return-void
.end method

.method protected finish(ILjava/lang/String;Z)V
    .locals 3

    .line 879
    iget v0, p0, Lcom/google/flatbuffers/FlatBufferBuilder;->minalign:I

    const/4 v1, 0x4

    if-eqz p3, :cond_0

    const/4 v2, 0x4

    goto :goto_0

    :cond_0
    const/4 v2, 0x0

    :goto_0
    add-int/lit8 v2, v2, 0x8

    invoke-virtual {p0, v0, v2}, Lcom/google/flatbuffers/FlatBufferBuilder;->prep(II)V

    .line 880
    invoke-virtual {p2}, Ljava/lang/String;->length()I

    move-result v0

    if-ne v0, v1, :cond_2

    const/4 v0, 0x3

    :goto_1
    if-ltz v0, :cond_1

    .line 884
    invoke-virtual {p2, v0}, Ljava/lang/String;->charAt(I)C

    move-result v1

    int-to-byte v1, v1

    invoke-virtual {p0, v1}, Lcom/google/flatbuffers/FlatBufferBuilder;->addByte(B)V

    add-int/lit8 v0, v0, -0x1

    goto :goto_1

    .line 886
    :cond_1
    invoke-virtual {p0, p1, p3}, Lcom/google/flatbuffers/FlatBufferBuilder;->finish(IZ)V

    return-void

    .line 881
    :cond_2
    new-instance p1, Ljava/lang/AssertionError;

    const-string p2, "FlatBuffers: file identifier must be length 4"

    invoke-direct {p1, p2}, Ljava/lang/AssertionError;-><init>(Ljava/lang/Object;)V

    throw p1
.end method

.method protected finish(IZ)V
    .locals 3

    .line 843
    iget v0, p0, Lcom/google/flatbuffers/FlatBufferBuilder;->minalign:I

    const/4 v1, 0x4

    if-eqz p2, :cond_0

    const/4 v2, 0x4

    goto :goto_0

    :cond_0
    const/4 v2, 0x0

    :goto_0
    add-int/2addr v2, v1

    invoke-virtual {p0, v0, v2}, Lcom/google/flatbuffers/FlatBufferBuilder;->prep(II)V

    .line 844
    invoke-virtual {p0, p1}, Lcom/google/flatbuffers/FlatBufferBuilder;->addOffset(I)V

    if-eqz p2, :cond_1

    .line 846
    iget-object p1, p0, Lcom/google/flatbuffers/FlatBufferBuilder;->bb:Ljava/nio/ByteBuffer;

    invoke-virtual {p1}, Ljava/nio/ByteBuffer;->capacity()I

    move-result p1

    iget p2, p0, Lcom/google/flatbuffers/FlatBufferBuilder;->space:I

    sub-int/2addr p1, p2

    invoke-virtual {p0, p1}, Lcom/google/flatbuffers/FlatBufferBuilder;->addInt(I)V

    .line 848
    :cond_1
    iget-object p1, p0, Lcom/google/flatbuffers/FlatBufferBuilder;->bb:Ljava/nio/ByteBuffer;

    iget p2, p0, Lcom/google/flatbuffers/FlatBufferBuilder;->space:I

    invoke-virtual {p1, p2}, Ljava/nio/ByteBuffer;->position(I)Ljava/nio/Buffer;

    const/4 p1, 0x1

    .line 849
    iput-boolean p1, p0, Lcom/google/flatbuffers/FlatBufferBuilder;->finished:Z

    return-void
.end method

.method public finishSizePrefixed(I)V
    .locals 1

    const/4 v0, 0x1

    .line 867
    invoke-virtual {p0, p1, v0}, Lcom/google/flatbuffers/FlatBufferBuilder;->finish(IZ)V

    return-void
.end method

.method public finishSizePrefixed(ILjava/lang/String;)V
    .locals 1

    const/4 v0, 0x1

    .line 908
    invoke-virtual {p0, p1, p2, v0}, Lcom/google/flatbuffers/FlatBufferBuilder;->finish(ILjava/lang/String;Z)V

    return-void
.end method

.method public finished()V
    .locals 2

    .line 568
    iget-boolean v0, p0, Lcom/google/flatbuffers/FlatBufferBuilder;->finished:Z

    if-eqz v0, :cond_0

    return-void

    .line 569
    :cond_0
    new-instance v0, Ljava/lang/AssertionError;

    const-string v1, "FlatBuffers: you can only access the serialized buffer after it has been finished by FlatBufferBuilder.finish()."

    invoke-direct {v0, v1}, Ljava/lang/AssertionError;-><init>(Ljava/lang/Object;)V

    throw v0
.end method

.method public forceDefaults(Z)Lcom/google/flatbuffers/FlatBufferBuilder;
    .locals 0

    .line 920
    iput-boolean p1, p0, Lcom/google/flatbuffers/FlatBufferBuilder;->force_defaults:Z

    return-object p0
.end method

.method public init(Ljava/nio/ByteBuffer;Lcom/google/flatbuffers/FlatBufferBuilder$ByteBufferFactory;)Lcom/google/flatbuffers/FlatBufferBuilder;
    .locals 0

    .line 138
    iput-object p2, p0, Lcom/google/flatbuffers/FlatBufferBuilder;->bb_factory:Lcom/google/flatbuffers/FlatBufferBuilder$ByteBufferFactory;

    .line 139
    iput-object p1, p0, Lcom/google/flatbuffers/FlatBufferBuilder;->bb:Ljava/nio/ByteBuffer;

    .line 140
    iget-object p1, p0, Lcom/google/flatbuffers/FlatBufferBuilder;->bb:Ljava/nio/ByteBuffer;

    invoke-virtual {p1}, Ljava/nio/ByteBuffer;->clear()Ljava/nio/Buffer;

    .line 141
    iget-object p1, p0, Lcom/google/flatbuffers/FlatBufferBuilder;->bb:Ljava/nio/ByteBuffer;

    sget-object p2, Ljava/nio/ByteOrder;->LITTLE_ENDIAN:Ljava/nio/ByteOrder;

    invoke-virtual {p1, p2}, Ljava/nio/ByteBuffer;->order(Ljava/nio/ByteOrder;)Ljava/nio/ByteBuffer;

    const/4 p1, 0x1

    .line 142
    iput p1, p0, Lcom/google/flatbuffers/FlatBufferBuilder;->minalign:I

    .line 143
    iget-object p1, p0, Lcom/google/flatbuffers/FlatBufferBuilder;->bb:Ljava/nio/ByteBuffer;

    invoke-virtual {p1}, Ljava/nio/ByteBuffer;->capacity()I

    move-result p1

    iput p1, p0, Lcom/google/flatbuffers/FlatBufferBuilder;->space:I

    const/4 p1, 0x0

    .line 144
    iput p1, p0, Lcom/google/flatbuffers/FlatBufferBuilder;->vtable_in_use:I

    .line 145
    iput-boolean p1, p0, Lcom/google/flatbuffers/FlatBufferBuilder;->nested:Z

    .line 146
    iput-boolean p1, p0, Lcom/google/flatbuffers/FlatBufferBuilder;->finished:Z

    .line 147
    iput p1, p0, Lcom/google/flatbuffers/FlatBufferBuilder;->object_start:I

    .line 148
    iput p1, p0, Lcom/google/flatbuffers/FlatBufferBuilder;->num_vtables:I

    .line 149
    iput p1, p0, Lcom/google/flatbuffers/FlatBufferBuilder;->vector_num_elems:I

    return-object p0
.end method

.method public notNested()V
    .locals 2

    .line 579
    iget-boolean v0, p0, Lcom/google/flatbuffers/FlatBufferBuilder;->nested:Z

    if-nez v0, :cond_0

    return-void

    .line 580
    :cond_0
    new-instance v0, Ljava/lang/AssertionError;

    const-string v1, "FlatBuffers: object serialization must not be nested."

    invoke-direct {v0, v1}, Ljava/lang/AssertionError;-><init>(Ljava/lang/Object;)V

    throw v0
.end method

.method public offset()I
    .locals 2

    .line 245
    iget-object v0, p0, Lcom/google/flatbuffers/FlatBufferBuilder;->bb:Ljava/nio/ByteBuffer;

    invoke-virtual {v0}, Ljava/nio/ByteBuffer;->capacity()I

    move-result v0

    iget v1, p0, Lcom/google/flatbuffers/FlatBufferBuilder;->space:I

    sub-int/2addr v0, v1

    return v0
.end method

.method public pad(I)V
    .locals 4

    const/4 v0, 0x0

    const/4 v1, 0x0

    :goto_0
    if-ge v1, p1, :cond_0

    .line 254
    iget-object v2, p0, Lcom/google/flatbuffers/FlatBufferBuilder;->bb:Ljava/nio/ByteBuffer;

    iget v3, p0, Lcom/google/flatbuffers/FlatBufferBuilder;->space:I

    add-int/lit8 v3, v3, -0x1

    iput v3, p0, Lcom/google/flatbuffers/FlatBufferBuilder;->space:I

    invoke-virtual {v2, v3, v0}, Ljava/nio/ByteBuffer;->put(IB)Ljava/nio/ByteBuffer;

    add-int/lit8 v1, v1, 0x1

    goto :goto_0

    :cond_0
    return-void
.end method

.method public prep(II)V
    .locals 4

    .line 269
    iget v0, p0, Lcom/google/flatbuffers/FlatBufferBuilder;->minalign:I

    if-le p1, v0, :cond_0

    iput p1, p0, Lcom/google/flatbuffers/FlatBufferBuilder;->minalign:I

    .line 272
    :cond_0
    iget-object v0, p0, Lcom/google/flatbuffers/FlatBufferBuilder;->bb:Ljava/nio/ByteBuffer;

    invoke-virtual {v0}, Ljava/nio/ByteBuffer;->capacity()I

    move-result v0

    iget v1, p0, Lcom/google/flatbuffers/FlatBufferBuilder;->space:I

    sub-int/2addr v0, v1

    add-int/2addr v0, p2

    xor-int/lit8 v0, v0, -0x1

    add-int/lit8 v0, v0, 0x1

    add-int/lit8 v1, p1, -0x1

    and-int/2addr v0, v1

    .line 274
    :goto_0
    iget v1, p0, Lcom/google/flatbuffers/FlatBufferBuilder;->space:I

    add-int v2, v0, p1

    add-int/2addr v2, p2

    if-ge v1, v2, :cond_2

    .line 275
    iget-object v1, p0, Lcom/google/flatbuffers/FlatBufferBuilder;->bb:Ljava/nio/ByteBuffer;

    invoke-virtual {v1}, Ljava/nio/ByteBuffer;->capacity()I

    move-result v1

    .line 276
    iget-object v2, p0, Lcom/google/flatbuffers/FlatBufferBuilder;->bb:Ljava/nio/ByteBuffer;

    .line 277
    iget-object v3, p0, Lcom/google/flatbuffers/FlatBufferBuilder;->bb_factory:Lcom/google/flatbuffers/FlatBufferBuilder$ByteBufferFactory;

    invoke-static {v2, v3}, Lcom/google/flatbuffers/FlatBufferBuilder;->growByteBuffer(Ljava/nio/ByteBuffer;Lcom/google/flatbuffers/FlatBufferBuilder$ByteBufferFactory;)Ljava/nio/ByteBuffer;

    move-result-object v3

    iput-object v3, p0, Lcom/google/flatbuffers/FlatBufferBuilder;->bb:Ljava/nio/ByteBuffer;

    .line 278
    iget-object v3, p0, Lcom/google/flatbuffers/FlatBufferBuilder;->bb:Ljava/nio/ByteBuffer;

    if-eq v2, v3, :cond_1

    .line 279
    iget-object v3, p0, Lcom/google/flatbuffers/FlatBufferBuilder;->bb_factory:Lcom/google/flatbuffers/FlatBufferBuilder$ByteBufferFactory;

    invoke-virtual {v3, v2}, Lcom/google/flatbuffers/FlatBufferBuilder$ByteBufferFactory;->releaseByteBuffer(Ljava/nio/ByteBuffer;)V

    .line 281
    :cond_1
    iget v2, p0, Lcom/google/flatbuffers/FlatBufferBuilder;->space:I

    iget-object v3, p0, Lcom/google/flatbuffers/FlatBufferBuilder;->bb:Ljava/nio/ByteBuffer;

    invoke-virtual {v3}, Ljava/nio/ByteBuffer;->capacity()I

    move-result v3

    sub-int/2addr v3, v1

    add-int/2addr v2, v3

    iput v2, p0, Lcom/google/flatbuffers/FlatBufferBuilder;->space:I

    goto :goto_0

    .line 283
    :cond_2
    invoke-virtual {p0, v0}, Lcom/google/flatbuffers/FlatBufferBuilder;->pad(I)V

    return-void
.end method

.method public putBoolean(Z)V
    .locals 2

    .line 292
    iget-object v0, p0, Lcom/google/flatbuffers/FlatBufferBuilder;->bb:Ljava/nio/ByteBuffer;

    iget v1, p0, Lcom/google/flatbuffers/FlatBufferBuilder;->space:I

    add-int/lit8 v1, v1, -0x1

    iput v1, p0, Lcom/google/flatbuffers/FlatBufferBuilder;->space:I

    int-to-byte p1, p1

    invoke-virtual {v0, v1, p1}, Ljava/nio/ByteBuffer;->put(IB)Ljava/nio/ByteBuffer;

    return-void
.end method

.method public putByte(B)V
    .locals 2

    .line 300
    iget-object v0, p0, Lcom/google/flatbuffers/FlatBufferBuilder;->bb:Ljava/nio/ByteBuffer;

    iget v1, p0, Lcom/google/flatbuffers/FlatBufferBuilder;->space:I

    add-int/lit8 v1, v1, -0x1

    iput v1, p0, Lcom/google/flatbuffers/FlatBufferBuilder;->space:I

    invoke-virtual {v0, v1, p1}, Ljava/nio/ByteBuffer;->put(IB)Ljava/nio/ByteBuffer;

    return-void
.end method

.method public putDouble(D)V
    .locals 2

    .line 340
    iget-object v0, p0, Lcom/google/flatbuffers/FlatBufferBuilder;->bb:Ljava/nio/ByteBuffer;

    iget v1, p0, Lcom/google/flatbuffers/FlatBufferBuilder;->space:I

    add-int/lit8 v1, v1, -0x8

    iput v1, p0, Lcom/google/flatbuffers/FlatBufferBuilder;->space:I

    invoke-virtual {v0, v1, p1, p2}, Ljava/nio/ByteBuffer;->putDouble(ID)Ljava/nio/ByteBuffer;

    return-void
.end method

.method public putFloat(F)V
    .locals 2

    .line 332
    iget-object v0, p0, Lcom/google/flatbuffers/FlatBufferBuilder;->bb:Ljava/nio/ByteBuffer;

    iget v1, p0, Lcom/google/flatbuffers/FlatBufferBuilder;->space:I

    add-int/lit8 v1, v1, -0x4

    iput v1, p0, Lcom/google/flatbuffers/FlatBufferBuilder;->space:I

    invoke-virtual {v0, v1, p1}, Ljava/nio/ByteBuffer;->putFloat(IF)Ljava/nio/ByteBuffer;

    return-void
.end method

.method public putInt(I)V
    .locals 2

    .line 316
    iget-object v0, p0, Lcom/google/flatbuffers/FlatBufferBuilder;->bb:Ljava/nio/ByteBuffer;

    iget v1, p0, Lcom/google/flatbuffers/FlatBufferBuilder;->space:I

    add-int/lit8 v1, v1, -0x4

    iput v1, p0, Lcom/google/flatbuffers/FlatBufferBuilder;->space:I

    invoke-virtual {v0, v1, p1}, Ljava/nio/ByteBuffer;->putInt(II)Ljava/nio/ByteBuffer;

    return-void
.end method

.method public putLong(J)V
    .locals 2

    .line 324
    iget-object v0, p0, Lcom/google/flatbuffers/FlatBufferBuilder;->bb:Ljava/nio/ByteBuffer;

    iget v1, p0, Lcom/google/flatbuffers/FlatBufferBuilder;->space:I

    add-int/lit8 v1, v1, -0x8

    iput v1, p0, Lcom/google/flatbuffers/FlatBufferBuilder;->space:I

    invoke-virtual {v0, v1, p1, p2}, Ljava/nio/ByteBuffer;->putLong(IJ)Ljava/nio/ByteBuffer;

    return-void
.end method

.method public putShort(S)V
    .locals 2

    .line 308
    iget-object v0, p0, Lcom/google/flatbuffers/FlatBufferBuilder;->bb:Ljava/nio/ByteBuffer;

    iget v1, p0, Lcom/google/flatbuffers/FlatBufferBuilder;->space:I

    add-int/lit8 v1, v1, -0x2

    iput v1, p0, Lcom/google/flatbuffers/FlatBufferBuilder;->space:I

    invoke-virtual {v0, v1, p1}, Ljava/nio/ByteBuffer;->putShort(IS)Ljava/nio/ByteBuffer;

    return-void
.end method

.method public required(II)V
    .locals 2

    .line 827
    iget-object v0, p0, Lcom/google/flatbuffers/FlatBufferBuilder;->bb:Ljava/nio/ByteBuffer;

    invoke-virtual {v0}, Ljava/nio/ByteBuffer;->capacity()I

    move-result v0

    sub-int/2addr v0, p1

    .line 828
    iget-object p1, p0, Lcom/google/flatbuffers/FlatBufferBuilder;->bb:Ljava/nio/ByteBuffer;

    invoke-virtual {p1, v0}, Ljava/nio/ByteBuffer;->getInt(I)I

    move-result p1

    sub-int/2addr v0, p1

    .line 829
    iget-object p1, p0, Lcom/google/flatbuffers/FlatBufferBuilder;->bb:Ljava/nio/ByteBuffer;

    add-int/2addr v0, p2

    invoke-virtual {p1, v0}, Ljava/nio/ByteBuffer;->getShort(I)S

    move-result p1

    if-eqz p1, :cond_0

    const/4 p1, 0x1

    goto :goto_0

    :cond_0
    const/4 p1, 0x0

    :goto_0
    if-eqz p1, :cond_1

    return-void

    .line 832
    :cond_1
    new-instance p1, Ljava/lang/AssertionError;

    const/16 v0, 0x2a

    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1, v0}, Ljava/lang/StringBuilder;-><init>(I)V

    const-string v0, "FlatBuffers: field "

    invoke-virtual {v1, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1, p2}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    const-string p2, " must be set"

    invoke-virtual {v1, p2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p2

    invoke-direct {p1, p2}, Ljava/lang/AssertionError;-><init>(Ljava/lang/Object;)V

    throw p1
.end method

.method public sizedByteArray()[B
    .locals 3

    .line 973
    iget v0, p0, Lcom/google/flatbuffers/FlatBufferBuilder;->space:I

    iget-object v1, p0, Lcom/google/flatbuffers/FlatBufferBuilder;->bb:Ljava/nio/ByteBuffer;

    invoke-virtual {v1}, Ljava/nio/ByteBuffer;->capacity()I

    move-result v1

    iget v2, p0, Lcom/google/flatbuffers/FlatBufferBuilder;->space:I

    sub-int/2addr v1, v2

    invoke-virtual {p0, v0, v1}, Lcom/google/flatbuffers/FlatBufferBuilder;->sizedByteArray(II)[B

    move-result-object v0

    return-object v0
.end method

.method public sizedByteArray(II)[B
    .locals 1

    .line 960
    invoke-virtual {p0}, Lcom/google/flatbuffers/FlatBufferBuilder;->finished()V

    .line 961
    new-array p2, p2, [B

    .line 962
    iget-object v0, p0, Lcom/google/flatbuffers/FlatBufferBuilder;->bb:Ljava/nio/ByteBuffer;

    invoke-virtual {v0, p1}, Ljava/nio/ByteBuffer;->position(I)Ljava/nio/Buffer;

    .line 963
    iget-object p1, p0, Lcom/google/flatbuffers/FlatBufferBuilder;->bb:Ljava/nio/ByteBuffer;

    invoke-virtual {p1, p2}, Ljava/nio/ByteBuffer;->get([B)Ljava/nio/ByteBuffer;

    return-object p2
.end method

.method public sizedInputStream()Ljava/io/InputStream;
    .locals 2

    .line 983
    invoke-virtual {p0}, Lcom/google/flatbuffers/FlatBufferBuilder;->finished()V

    .line 984
    iget-object v0, p0, Lcom/google/flatbuffers/FlatBufferBuilder;->bb:Ljava/nio/ByteBuffer;

    invoke-virtual {v0}, Ljava/nio/ByteBuffer;->duplicate()Ljava/nio/ByteBuffer;

    move-result-object v0

    .line 985
    iget v1, p0, Lcom/google/flatbuffers/FlatBufferBuilder;->space:I

    invoke-virtual {v0, v1}, Ljava/nio/ByteBuffer;->position(I)Ljava/nio/Buffer;

    .line 986
    iget-object v1, p0, Lcom/google/flatbuffers/FlatBufferBuilder;->bb:Ljava/nio/ByteBuffer;

    invoke-virtual {v1}, Ljava/nio/ByteBuffer;->capacity()I

    move-result v1

    invoke-virtual {v0, v1}, Ljava/nio/ByteBuffer;->limit(I)Ljava/nio/Buffer;

    .line 987
    new-instance v1, Lcom/google/flatbuffers/FlatBufferBuilder$ByteBufferBackedInputStream;

    invoke-direct {v1, v0}, Lcom/google/flatbuffers/FlatBufferBuilder$ByteBufferBackedInputStream;-><init>(Ljava/nio/ByteBuffer;)V

    return-object v1
.end method

.method public slot(I)V
    .locals 2

    .line 753
    iget-object v0, p0, Lcom/google/flatbuffers/FlatBufferBuilder;->vtable:[I

    invoke-virtual {p0}, Lcom/google/flatbuffers/FlatBufferBuilder;->offset()I

    move-result v1

    aput v1, v0, p1

    return-void
.end method

.method public startObject(I)V
    .locals 2

    .line 636
    invoke-virtual {p0}, Lcom/google/flatbuffers/FlatBufferBuilder;->notNested()V

    .line 637
    iget-object v0, p0, Lcom/google/flatbuffers/FlatBufferBuilder;->vtable:[I

    if-eqz v0, :cond_0

    iget-object v0, p0, Lcom/google/flatbuffers/FlatBufferBuilder;->vtable:[I

    array-length v0, v0

    if-ge v0, p1, :cond_1

    :cond_0
    new-array v0, p1, [I

    iput-object v0, p0, Lcom/google/flatbuffers/FlatBufferBuilder;->vtable:[I

    .line 638
    :cond_1
    iput p1, p0, Lcom/google/flatbuffers/FlatBufferBuilder;->vtable_in_use:I

    .line 639
    iget-object p1, p0, Lcom/google/flatbuffers/FlatBufferBuilder;->vtable:[I

    iget v0, p0, Lcom/google/flatbuffers/FlatBufferBuilder;->vtable_in_use:I

    const/4 v1, 0x0

    invoke-static {p1, v1, v0, v1}, Ljava/util/Arrays;->fill([IIII)V

    const/4 p1, 0x1

    .line 640
    iput-boolean p1, p0, Lcom/google/flatbuffers/FlatBufferBuilder;->nested:Z

    .line 641
    invoke-virtual {p0}, Lcom/google/flatbuffers/FlatBufferBuilder;->offset()I

    move-result p1

    iput p1, p0, Lcom/google/flatbuffers/FlatBufferBuilder;->object_start:I

    return-void
.end method

.method public startVector(III)V
    .locals 0

    .line 448
    invoke-virtual {p0}, Lcom/google/flatbuffers/FlatBufferBuilder;->notNested()V

    .line 449
    iput p2, p0, Lcom/google/flatbuffers/FlatBufferBuilder;->vector_num_elems:I

    mul-int p1, p1, p2

    const/4 p2, 0x4

    .line 450
    invoke-virtual {p0, p2, p1}, Lcom/google/flatbuffers/FlatBufferBuilder;->prep(II)V

    .line 451
    invoke-virtual {p0, p3, p1}, Lcom/google/flatbuffers/FlatBufferBuilder;->prep(II)V

    const/4 p1, 0x1

    .line 452
    iput-boolean p1, p0, Lcom/google/flatbuffers/FlatBufferBuilder;->nested:Z

    return-void
.end method
