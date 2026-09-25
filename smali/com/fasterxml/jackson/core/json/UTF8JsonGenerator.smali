.class public Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;
.super Lcom/fasterxml/jackson/core/json/JsonGeneratorImpl;
.source "UTF8JsonGenerator.java"


# static fields
.field private static final BYTE_0:B = 0x30t

.field private static final BYTE_BACKSLASH:B = 0x5ct

.field private static final BYTE_COLON:B = 0x3at

.field private static final BYTE_COMMA:B = 0x2ct

.field private static final BYTE_LBRACKET:B = 0x5bt

.field private static final BYTE_LCURLY:B = 0x7bt

.field private static final BYTE_QUOTE:B = 0x22t

.field private static final BYTE_RBRACKET:B = 0x5dt

.field private static final BYTE_RCURLY:B = 0x7dt

.field private static final BYTE_u:B = 0x75t

.field private static final FALSE_BYTES:[B

.field static final HEX_CHARS:[B

.field private static final MAX_BYTES_TO_BUFFER:I = 0x200

.field private static final NULL_BYTES:[B

.field private static final TRUE_BYTES:[B


# instance fields
.field protected _bufferRecyclable:Z

.field protected _cfgUnqNames:Z

.field protected _charBuffer:[C

.field protected final _charBufferLength:I

.field protected _entityBuffer:[B

.field protected _outputBuffer:[B

.field protected final _outputEnd:I

.field protected final _outputMaxContiguous:I

.field protected final _outputStream:Ljava/io/OutputStream;

.field protected _outputTail:I


# direct methods
.method static constructor <clinit>()V
    .locals 2

    .line 30
    invoke-static {}, Lcom/fasterxml/jackson/core/io/CharTypes;->copyHexBytes()[B

    move-result-object v0

    sput-object v0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->HEX_CHARS:[B

    const/4 v0, 0x4

    .line 32
    new-array v1, v0, [B

    fill-array-data v1, :array_0

    sput-object v1, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->NULL_BYTES:[B

    .line 33
    new-array v0, v0, [B

    fill-array-data v0, :array_1

    sput-object v0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->TRUE_BYTES:[B

    const/4 v0, 0x5

    .line 34
    new-array v0, v0, [B

    fill-array-data v0, :array_2

    sput-object v0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->FALSE_BYTES:[B

    return-void

    :array_0
    .array-data 1
        0x6et
        0x75t
        0x6ct
        0x6ct
    .end array-data

    :array_1
    .array-data 1
        0x74t
        0x72t
        0x75t
        0x65t
    .end array-data

    :array_2
    .array-data 1
        0x66t
        0x61t
        0x6ct
        0x73t
        0x65t
    .end array-data
.end method

.method public constructor <init>(Lcom/fasterxml/jackson/core/io/IOContext;ILcom/fasterxml/jackson/core/ObjectCodec;Ljava/io/OutputStream;)V
    .locals 0

    .line 115
    invoke-direct {p0, p1, p2, p3}, Lcom/fasterxml/jackson/core/json/JsonGeneratorImpl;-><init>(Lcom/fasterxml/jackson/core/io/IOContext;ILcom/fasterxml/jackson/core/ObjectCodec;)V

    const/4 p3, 0x0

    .line 57
    iput p3, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    .line 116
    iput-object p4, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputStream:Ljava/io/OutputStream;

    const/4 p3, 0x1

    .line 117
    iput-boolean p3, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_bufferRecyclable:Z

    .line 118
    invoke-virtual {p1}, Lcom/fasterxml/jackson/core/io/IOContext;->allocWriteEncodingBuffer()[B

    move-result-object p4

    iput-object p4, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputBuffer:[B

    .line 119
    iget-object p4, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputBuffer:[B

    array-length p4, p4

    iput p4, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputEnd:I

    .line 125
    iget p4, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputEnd:I

    shr-int/lit8 p4, p4, 0x3

    iput p4, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputMaxContiguous:I

    .line 126
    invoke-virtual {p1}, Lcom/fasterxml/jackson/core/io/IOContext;->allocConcatBuffer()[C

    move-result-object p1

    iput-object p1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_charBuffer:[C

    .line 127
    iget-object p1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_charBuffer:[C

    array-length p1, p1

    iput p1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_charBufferLength:I

    .line 130
    sget-object p1, Lcom/fasterxml/jackson/core/JsonGenerator$Feature;->ESCAPE_NON_ASCII:Lcom/fasterxml/jackson/core/JsonGenerator$Feature;

    invoke-virtual {p0, p1}, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->isEnabled(Lcom/fasterxml/jackson/core/JsonGenerator$Feature;)Z

    move-result p1

    if-eqz p1, :cond_0

    const/16 p1, 0x7f

    .line 131
    invoke-virtual {p0, p1}, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->setHighestNonEscapedChar(I)Lcom/fasterxml/jackson/core/JsonGenerator;

    .line 133
    :cond_0
    sget-object p1, Lcom/fasterxml/jackson/core/JsonGenerator$Feature;->QUOTE_FIELD_NAMES:Lcom/fasterxml/jackson/core/JsonGenerator$Feature;

    invoke-virtual {p1, p2}, Lcom/fasterxml/jackson/core/JsonGenerator$Feature;->enabledIn(I)Z

    move-result p1

    xor-int/2addr p1, p3

    iput-boolean p1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_cfgUnqNames:Z

    return-void
.end method

.method public constructor <init>(Lcom/fasterxml/jackson/core/io/IOContext;ILcom/fasterxml/jackson/core/ObjectCodec;Ljava/io/OutputStream;[BIZ)V
    .locals 0

    .line 141
    invoke-direct {p0, p1, p2, p3}, Lcom/fasterxml/jackson/core/json/JsonGeneratorImpl;-><init>(Lcom/fasterxml/jackson/core/io/IOContext;ILcom/fasterxml/jackson/core/ObjectCodec;)V

    const/4 p3, 0x0

    .line 57
    iput p3, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    .line 142
    iput-object p4, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputStream:Ljava/io/OutputStream;

    .line 143
    iput-boolean p7, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_bufferRecyclable:Z

    .line 144
    iput p6, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    .line 145
    iput-object p5, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputBuffer:[B

    .line 146
    iget-object p3, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputBuffer:[B

    array-length p3, p3

    iput p3, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputEnd:I

    .line 148
    iget p3, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputEnd:I

    shr-int/lit8 p3, p3, 0x3

    iput p3, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputMaxContiguous:I

    .line 149
    invoke-virtual {p1}, Lcom/fasterxml/jackson/core/io/IOContext;->allocConcatBuffer()[C

    move-result-object p1

    iput-object p1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_charBuffer:[C

    .line 150
    iget-object p1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_charBuffer:[C

    array-length p1, p1

    iput p1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_charBufferLength:I

    .line 151
    sget-object p1, Lcom/fasterxml/jackson/core/JsonGenerator$Feature;->QUOTE_FIELD_NAMES:Lcom/fasterxml/jackson/core/JsonGenerator$Feature;

    invoke-virtual {p1, p2}, Lcom/fasterxml/jackson/core/JsonGenerator$Feature;->enabledIn(I)Z

    move-result p1

    xor-int/lit8 p1, p1, 0x1

    iput-boolean p1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_cfgUnqNames:Z

    return-void
.end method

.method private final _handleLongCustomEscape([BII[BI)I
    .locals 3
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;,
            Lcom/fasterxml/jackson/core/JsonGenerationException;
        }
    .end annotation

    .line 1441
    array-length v0, p4

    add-int v1, p2, v0

    if-le v1, p3, :cond_1

    .line 1443
    iput p2, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    .line 1444
    invoke-virtual {p0}, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_flushBuffer()V

    .line 1445
    iget p2, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    .line 1446
    array-length v1, p1

    const/4 v2, 0x0

    if-le v0, v1, :cond_0

    .line 1447
    iget-object p1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputStream:Ljava/io/OutputStream;

    invoke-virtual {p1, p4, v2, v0}, Ljava/io/OutputStream;->write([BII)V

    return p2

    .line 1450
    :cond_0
    invoke-static {p4, v2, p1, p2, v0}, Ljava/lang/System;->arraycopy(Ljava/lang/Object;ILjava/lang/Object;II)V

    add-int/2addr p2, v0

    :cond_1
    mul-int/lit8 p5, p5, 0x6

    add-int/2addr p5, p2

    if-le p5, p3, :cond_2

    .line 1455
    invoke-virtual {p0}, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_flushBuffer()V

    .line 1456
    iget p1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    return p1

    :cond_2
    return p2
.end method

.method private final _outputMultiByteChar(II)I
    .locals 4
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    .line 1789
    iget-object v0, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputBuffer:[B

    const v1, 0xd800

    if-lt p1, v1, :cond_0

    const v1, 0xdfff

    if-gt p1, v1, :cond_0

    add-int/lit8 v1, p2, 0x1

    const/16 v2, 0x5c

    .line 1791
    aput-byte v2, v0, p2

    add-int/lit8 p2, v1, 0x1

    const/16 v2, 0x75

    .line 1792
    aput-byte v2, v0, v1

    add-int/lit8 v1, p2, 0x1

    .line 1794
    sget-object v2, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->HEX_CHARS:[B

    shr-int/lit8 v3, p1, 0xc

    and-int/lit8 v3, v3, 0xf

    aget-byte v2, v2, v3

    aput-byte v2, v0, p2

    add-int/lit8 p2, v1, 0x1

    .line 1795
    sget-object v2, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->HEX_CHARS:[B

    shr-int/lit8 v3, p1, 0x8

    and-int/lit8 v3, v3, 0xf

    aget-byte v2, v2, v3

    aput-byte v2, v0, v1

    add-int/lit8 v1, p2, 0x1

    .line 1796
    sget-object v2, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->HEX_CHARS:[B

    shr-int/lit8 v3, p1, 0x4

    and-int/lit8 v3, v3, 0xf

    aget-byte v2, v2, v3

    aput-byte v2, v0, p2

    add-int/lit8 p2, v1, 0x1

    .line 1797
    sget-object v2, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->HEX_CHARS:[B

    and-int/lit8 p1, p1, 0xf

    aget-byte p1, v2, p1

    aput-byte p1, v0, v1

    goto :goto_0

    :cond_0
    add-int/lit8 v1, p2, 0x1

    shr-int/lit8 v2, p1, 0xc

    or-int/lit16 v2, v2, 0xe0

    int-to-byte v2, v2

    .line 1799
    aput-byte v2, v0, p2

    add-int/lit8 p2, v1, 0x1

    shr-int/lit8 v2, p1, 0x6

    and-int/lit8 v2, v2, 0x3f

    or-int/lit16 v2, v2, 0x80

    int-to-byte v2, v2

    .line 1800
    aput-byte v2, v0, v1

    add-int/lit8 v1, p2, 0x1

    and-int/lit8 p1, p1, 0x3f

    or-int/lit16 p1, p1, 0x80

    int-to-byte p1, p1

    .line 1801
    aput-byte p1, v0, p2

    move p2, v1

    :goto_0
    return p2
.end method

.method private final _outputRawMultiByteChar(I[CII)I
    .locals 1
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    const v0, 0xd800

    if-lt p1, v0, :cond_2

    const v0, 0xdfff

    if-gt p1, v0, :cond_2

    if-ge p3, p4, :cond_0

    if-nez p2, :cond_1

    :cond_0
    const-string p4, "Split surrogate on writeRaw() input (last character)"

    .line 1751
    invoke-virtual {p0, p4}, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_reportError(Ljava/lang/String;)V

    .line 1753
    :cond_1
    aget-char p2, p2, p3

    invoke-virtual {p0, p1, p2}, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputSurrogates(II)V

    add-int/lit8 p3, p3, 0x1

    return p3

    .line 1757
    :cond_2
    iget-object p2, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputBuffer:[B

    .line 1758
    iget p4, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    add-int/lit8 v0, p4, 0x1

    iput v0, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    shr-int/lit8 v0, p1, 0xc

    or-int/lit16 v0, v0, 0xe0

    int-to-byte v0, v0

    aput-byte v0, p2, p4

    .line 1759
    iget p4, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    add-int/lit8 v0, p4, 0x1

    iput v0, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    shr-int/lit8 v0, p1, 0x6

    and-int/lit8 v0, v0, 0x3f

    or-int/lit16 v0, v0, 0x80

    int-to-byte v0, v0

    aput-byte v0, p2, p4

    .line 1760
    iget p4, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    add-int/lit8 v0, p4, 0x1

    iput v0, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    and-int/lit8 p1, p1, 0x3f

    or-int/lit16 p1, p1, 0x80

    int-to-byte p1, p1

    aput-byte p1, p2, p4

    return p3
.end method

.method private final _readMore(Ljava/io/InputStream;[BIII)I
    .locals 3
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    const/4 v0, 0x0

    :goto_0
    if-ge p3, p4, :cond_0

    add-int/lit8 v1, v0, 0x1

    add-int/lit8 v2, p3, 0x1

    .line 1712
    aget-byte p3, p2, p3

    aput-byte p3, p2, v0

    move v0, v1

    move p3, v2

    goto :goto_0

    .line 1716
    :cond_0
    array-length p3, p2

    invoke-static {p5, p3}, Ljava/lang/Math;->min(II)I

    move-result p3

    :cond_1
    sub-int p4, p3, v0

    if-nez p4, :cond_2

    goto :goto_1

    .line 1723
    :cond_2
    invoke-virtual {p1, p2, v0, p4}, Ljava/io/InputStream;->read([BII)I

    move-result p4

    if-gez p4, :cond_3

    return v0

    :cond_3
    add-int/2addr v0, p4

    const/4 p4, 0x3

    if-lt v0, p4, :cond_1

    :goto_1
    return v0
.end method

.method private final _writeBytes([B)V
    .locals 4
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    .line 1113
    array-length v0, p1

    .line 1114
    iget v1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    add-int/2addr v1, v0

    iget v2, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputEnd:I

    const/4 v3, 0x0

    if-le v1, v2, :cond_0

    .line 1115
    invoke-virtual {p0}, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_flushBuffer()V

    const/16 v1, 0x200

    if-le v0, v1, :cond_0

    .line 1118
    iget-object v1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputStream:Ljava/io/OutputStream;

    invoke-virtual {v1, p1, v3, v0}, Ljava/io/OutputStream;->write([BII)V

    return-void

    .line 1122
    :cond_0
    iget-object v1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputBuffer:[B

    iget v2, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    invoke-static {p1, v3, v1, v2, v0}, Ljava/lang/System;->arraycopy(Ljava/lang/Object;ILjava/lang/Object;II)V

    .line 1123
    iget p1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    add-int/2addr p1, v0

    iput p1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    return-void
.end method

.method private final _writeBytes([BII)V
    .locals 2
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    .line 1128
    iget v0, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    add-int/2addr v0, p3

    iget v1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputEnd:I

    if-le v0, v1, :cond_0

    .line 1129
    invoke-virtual {p0}, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_flushBuffer()V

    const/16 v0, 0x200

    if-le p3, v0, :cond_0

    .line 1132
    iget-object v0, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputStream:Ljava/io/OutputStream;

    invoke-virtual {v0, p1, p2, p3}, Ljava/io/OutputStream;->write([BII)V

    return-void

    .line 1136
    :cond_0
    iget-object v0, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputBuffer:[B

    iget v1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    invoke-static {p1, p2, v0, v1, p3}, Ljava/lang/System;->arraycopy(Ljava/lang/Object;ILjava/lang/Object;II)V

    .line 1137
    iget p1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    add-int/2addr p1, p3

    iput p1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    return-void
.end method

.method private final _writeCustomEscape([BILcom/fasterxml/jackson/core/SerializableString;I)I
    .locals 6
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;,
            Lcom/fasterxml/jackson/core/JsonGenerationException;
        }
    .end annotation

    .line 1427
    invoke-interface {p3}, Lcom/fasterxml/jackson/core/SerializableString;->asUnquotedUTF8()[B

    move-result-object v4

    .line 1428
    array-length p3, v4

    const/4 v0, 0x6

    if-le p3, v0, :cond_0

    .line 1430
    iget v3, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputEnd:I

    move-object v0, p0

    move-object v1, p1

    move v2, p2

    move v5, p4

    invoke-direct/range {v0 .. v5}, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_handleLongCustomEscape([BII[BI)I

    move-result p1

    return p1

    :cond_0
    const/4 p4, 0x0

    .line 1433
    invoke-static {v4, p4, p1, p2, p3}, Ljava/lang/System;->arraycopy(Ljava/lang/Object;ILjava/lang/Object;II)V

    add-int/2addr p2, p3

    return p2
.end method

.method private final _writeCustomStringSegment2([CII)V
    .locals 9
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;,
            Lcom/fasterxml/jackson/core/JsonGenerationException;
        }
    .end annotation

    .line 1370
    iget v0, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    sub-int v1, p3, p2

    mul-int/lit8 v1, v1, 0x6

    add-int/2addr v0, v1

    iget v1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputEnd:I

    if-le v0, v1, :cond_0

    .line 1371
    invoke-virtual {p0}, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_flushBuffer()V

    .line 1373
    :cond_0
    iget v0, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    .line 1375
    iget-object v1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputBuffer:[B

    .line 1376
    iget-object v2, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputEscapes:[I

    .line 1378
    iget v3, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_maximumNonEscapedChar:I

    if-gtz v3, :cond_1

    const v3, 0xffff

    goto :goto_0

    :cond_1
    iget v3, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_maximumNonEscapedChar:I

    .line 1379
    :goto_0
    iget-object v4, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_characterEscapes:Lcom/fasterxml/jackson/core/io/CharacterEscapes;

    :goto_1
    if-ge p2, p3, :cond_a

    add-int/lit8 v5, p2, 0x1

    .line 1382
    aget-char p2, p1, p2

    const/16 v6, 0x7f

    if-gt p2, v6, :cond_6

    .line 1384
    aget v6, v2, p2

    if-nez v6, :cond_2

    add-int/lit8 v6, v0, 0x1

    int-to-byte p2, p2

    .line 1385
    aput-byte p2, v1, v0

    move p2, v5

    move v0, v6

    goto :goto_1

    .line 1388
    :cond_2
    aget v6, v2, p2

    if-lez v6, :cond_3

    add-int/lit8 p2, v0, 0x1

    const/16 v7, 0x5c

    .line 1390
    aput-byte v7, v1, v0

    add-int/lit8 v0, p2, 0x1

    int-to-byte v6, v6

    .line 1391
    aput-byte v6, v1, p2

    goto :goto_2

    :cond_3
    const/4 v7, -0x2

    if-ne v6, v7, :cond_5

    .line 1393
    invoke-virtual {v4, p2}, Lcom/fasterxml/jackson/core/io/CharacterEscapes;->getEscapeSequence(I)Lcom/fasterxml/jackson/core/SerializableString;

    move-result-object v6

    if-nez v6, :cond_4

    .line 1395
    new-instance v7, Ljava/lang/StringBuilder;

    invoke-direct {v7}, Ljava/lang/StringBuilder;-><init>()V

    const-string v8, "Invalid custom escape definitions; custom escape not found for character code 0x"

    invoke-virtual {v7, v8}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-static {p2}, Ljava/lang/Integer;->toHexString(I)Ljava/lang/String;

    move-result-object p2

    invoke-virtual {v7, p2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string p2, ", although was supposed to have one"

    invoke-virtual {v7, p2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v7}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p2

    invoke-virtual {p0, p2}, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_reportError(Ljava/lang/String;)V

    :cond_4
    sub-int p2, p3, v5

    .line 1398
    invoke-direct {p0, v1, v0, v6, p2}, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_writeCustomEscape([BILcom/fasterxml/jackson/core/SerializableString;I)I

    move-result v0

    goto :goto_2

    .line 1401
    :cond_5
    invoke-direct {p0, p2, v0}, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_writeGenericEscape(II)I

    move-result v0

    goto :goto_2

    :cond_6
    if-le p2, v3, :cond_7

    .line 1406
    invoke-direct {p0, p2, v0}, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_writeGenericEscape(II)I

    move-result v0

    :goto_2
    move p2, v5

    goto :goto_1

    .line 1409
    :cond_7
    invoke-virtual {v4, p2}, Lcom/fasterxml/jackson/core/io/CharacterEscapes;->getEscapeSequence(I)Lcom/fasterxml/jackson/core/SerializableString;

    move-result-object v6

    if-eqz v6, :cond_8

    sub-int p2, p3, v5

    .line 1411
    invoke-direct {p0, v1, v0, v6, p2}, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_writeCustomEscape([BILcom/fasterxml/jackson/core/SerializableString;I)I

    move-result v0

    goto :goto_2

    :cond_8
    const/16 v6, 0x7ff

    if-gt p2, v6, :cond_9

    add-int/lit8 v6, v0, 0x1

    shr-int/lit8 v7, p2, 0x6

    or-int/lit16 v7, v7, 0xc0

    int-to-byte v7, v7

    .line 1415
    aput-byte v7, v1, v0

    add-int/lit8 v0, v6, 0x1

    and-int/lit8 p2, p2, 0x3f

    or-int/lit16 p2, p2, 0x80

    int-to-byte p2, p2

    .line 1416
    aput-byte p2, v1, v6

    goto :goto_2

    .line 1418
    :cond_9
    invoke-direct {p0, p2, v0}, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputMultiByteChar(II)I

    move-result p2

    move v0, p2

    goto :goto_2

    .line 1421
    :cond_a
    iput v0, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    return-void
.end method

.method private _writeGenericEscape(II)I
    .locals 5
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    .line 1823
    iget-object v0, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputBuffer:[B

    add-int/lit8 v1, p2, 0x1

    const/16 v2, 0x5c

    .line 1824
    aput-byte v2, v0, p2

    add-int/lit8 p2, v1, 0x1

    const/16 v2, 0x75

    .line 1825
    aput-byte v2, v0, v1

    const/16 v1, 0xff

    if-le p1, v1, :cond_0

    shr-int/lit8 v2, p1, 0x8

    and-int/2addr v1, v2

    add-int/lit8 v2, p2, 0x1

    .line 1828
    sget-object v3, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->HEX_CHARS:[B

    shr-int/lit8 v4, v1, 0x4

    aget-byte v3, v3, v4

    aput-byte v3, v0, p2

    add-int/lit8 p2, v2, 0x1

    .line 1829
    sget-object v3, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->HEX_CHARS:[B

    and-int/lit8 v1, v1, 0xf

    aget-byte v1, v3, v1

    aput-byte v1, v0, v2

    and-int/lit16 p1, p1, 0xff

    goto :goto_0

    :cond_0
    add-int/lit8 v1, p2, 0x1

    const/16 v2, 0x30

    .line 1832
    aput-byte v2, v0, p2

    add-int/lit8 p2, v1, 0x1

    .line 1833
    aput-byte v2, v0, v1

    :goto_0
    add-int/lit8 v1, p2, 0x1

    .line 1836
    sget-object v2, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->HEX_CHARS:[B

    shr-int/lit8 v3, p1, 0x4

    aget-byte v2, v2, v3

    aput-byte v2, v0, p2

    add-int/lit8 p2, v1, 0x1

    .line 1837
    sget-object v2, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->HEX_CHARS:[B

    and-int/lit8 p1, p1, 0xf

    aget-byte p1, v2, p1

    aput-byte p1, v0, v1

    return p2
.end method

.method private _writeLongString([CII)V
    .locals 1
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    .line 453
    iget p1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    iget p2, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputEnd:I

    if-lt p1, p2, :cond_0

    .line 454
    invoke-virtual {p0}, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_flushBuffer()V

    .line 456
    :cond_0
    iget-object p1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputBuffer:[B

    iget p2, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    add-int/lit8 v0, p2, 0x1

    iput v0, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    const/16 v0, 0x22

    aput-byte v0, p1, p2

    .line 457
    iget-object p1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_charBuffer:[C

    const/4 p2, 0x0

    invoke-direct {p0, p1, p2, p3}, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_writeStringSegments([CII)V

    .line 458
    iget p1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    iget p2, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputEnd:I

    if-lt p1, p2, :cond_1

    .line 459
    invoke-virtual {p0}, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_flushBuffer()V

    .line 461
    :cond_1
    iget-object p1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputBuffer:[B

    iget p2, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    add-int/lit8 p3, p2, 0x1

    iput p3, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    aput-byte v0, p1, p2

    return-void
.end method

.method private final _writeNull()V
    .locals 5
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    .line 1808
    iget v0, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    const/4 v1, 0x4

    add-int/2addr v0, v1

    iget v2, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputEnd:I

    if-lt v0, v2, :cond_0

    .line 1809
    invoke-virtual {p0}, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_flushBuffer()V

    .line 1811
    :cond_0
    sget-object v0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->NULL_BYTES:[B

    const/4 v2, 0x0

    iget-object v3, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputBuffer:[B

    iget v4, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    invoke-static {v0, v2, v3, v4, v1}, Ljava/lang/System;->arraycopy(Ljava/lang/Object;ILjava/lang/Object;II)V

    .line 1812
    iget v0, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    add-int/2addr v0, v1

    iput v0, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    return-void
.end method

.method private final _writeQuotedInt(I)V
    .locals 3
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    .line 810
    iget v0, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    add-int/lit8 v0, v0, 0xd

    iget v1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputEnd:I

    if-lt v0, v1, :cond_0

    .line 811
    invoke-virtual {p0}, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_flushBuffer()V

    .line 813
    :cond_0
    iget-object v0, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputBuffer:[B

    iget v1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    add-int/lit8 v2, v1, 0x1

    iput v2, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    const/16 v2, 0x22

    aput-byte v2, v0, v1

    .line 814
    iget-object v0, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputBuffer:[B

    iget v1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    invoke-static {p1, v0, v1}, Lcom/fasterxml/jackson/core/io/NumberOutput;->outputInt(I[BI)I

    move-result p1

    iput p1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    .line 815
    iget-object p1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputBuffer:[B

    iget v0, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    add-int/lit8 v1, v0, 0x1

    iput v1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    aput-byte v2, p1, v0

    return-void
.end method

.method private final _writeQuotedLong(J)V
    .locals 3
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    .line 836
    iget v0, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    add-int/lit8 v0, v0, 0x17

    iget v1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputEnd:I

    if-lt v0, v1, :cond_0

    .line 837
    invoke-virtual {p0}, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_flushBuffer()V

    .line 839
    :cond_0
    iget-object v0, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputBuffer:[B

    iget v1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    add-int/lit8 v2, v1, 0x1

    iput v2, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    const/16 v2, 0x22

    aput-byte v2, v0, v1

    .line 840
    iget-object v0, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputBuffer:[B

    iget v1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    invoke-static {p1, p2, v0, v1}, Lcom/fasterxml/jackson/core/io/NumberOutput;->outputLong(J[BI)I

    move-result p1

    iput p1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    .line 841
    iget-object p1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputBuffer:[B

    iget p2, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    add-int/lit8 v0, p2, 0x1

    iput v0, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    aput-byte v2, p1, p2

    return-void
.end method

.method private final _writeQuotedRaw(Ljava/lang/Object;)V
    .locals 3
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    .line 922
    iget v0, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    iget v1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputEnd:I

    if-lt v0, v1, :cond_0

    .line 923
    invoke-virtual {p0}, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_flushBuffer()V

    .line 925
    :cond_0
    iget-object v0, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputBuffer:[B

    iget v1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    add-int/lit8 v2, v1, 0x1

    iput v2, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    const/16 v2, 0x22

    aput-byte v2, v0, v1

    .line 926
    invoke-virtual {p1}, Ljava/lang/Object;->toString()Ljava/lang/String;

    move-result-object p1

    invoke-virtual {p0, p1}, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->writeRaw(Ljava/lang/String;)V

    .line 927
    iget p1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    iget v0, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputEnd:I

    if-lt p1, v0, :cond_1

    .line 928
    invoke-virtual {p0}, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_flushBuffer()V

    .line 930
    :cond_1
    iget-object p1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputBuffer:[B

    iget v0, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    add-int/lit8 v1, v0, 0x1

    iput v1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    aput-byte v2, p1, v0

    return-void
.end method

.method private final _writeQuotedShort(S)V
    .locals 3
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    .line 784
    iget v0, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    add-int/lit8 v0, v0, 0x8

    iget v1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputEnd:I

    if-lt v0, v1, :cond_0

    .line 785
    invoke-virtual {p0}, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_flushBuffer()V

    .line 787
    :cond_0
    iget-object v0, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputBuffer:[B

    iget v1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    add-int/lit8 v2, v1, 0x1

    iput v2, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    const/16 v2, 0x22

    aput-byte v2, v0, v1

    .line 788
    iget-object v0, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputBuffer:[B

    iget v1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    invoke-static {p1, v0, v1}, Lcom/fasterxml/jackson/core/io/NumberOutput;->outputInt(I[BI)I

    move-result p1

    iput p1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    .line 789
    iget-object p1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputBuffer:[B

    iget v0, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    add-int/lit8 v1, v0, 0x1

    iput v1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    aput-byte v2, p1, v0

    return-void
.end method

.method private final _writeSegmentedRaw([CII)V
    .locals 6
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;,
            Lcom/fasterxml/jackson/core/JsonGenerationException;
        }
    .end annotation

    .line 669
    iget v0, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputEnd:I

    .line 670
    iget-object v1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputBuffer:[B

    :goto_0
    if-ge p2, p3, :cond_5

    .line 676
    :cond_0
    aget-char v2, p1, p2

    const/16 v3, 0x80

    if-lt v2, v3, :cond_3

    .line 689
    iget v2, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    add-int/lit8 v2, v2, 0x3

    iget v4, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputEnd:I

    if-lt v2, v4, :cond_1

    .line 690
    invoke-virtual {p0}, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_flushBuffer()V

    :cond_1
    add-int/lit8 v2, p2, 0x1

    .line 692
    aget-char p2, p1, p2

    const/16 v4, 0x800

    if-ge p2, v4, :cond_2

    .line 694
    iget v4, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    add-int/lit8 v5, v4, 0x1

    iput v5, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    shr-int/lit8 v5, p2, 0x6

    or-int/lit16 v5, v5, 0xc0

    int-to-byte v5, v5

    aput-byte v5, v1, v4

    .line 695
    iget v4, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    add-int/lit8 v5, v4, 0x1

    iput v5, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    and-int/lit8 p2, p2, 0x3f

    or-int/2addr p2, v3

    int-to-byte p2, p2

    aput-byte p2, v1, v4

    move p2, v2

    goto :goto_0

    .line 697
    :cond_2
    invoke-direct {p0, p2, p1, v2, p3}, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputRawMultiByteChar(I[CII)I

    move-result p2

    goto :goto_0

    .line 681
    :cond_3
    iget v3, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    if-lt v3, v0, :cond_4

    .line 682
    invoke-virtual {p0}, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_flushBuffer()V

    .line 684
    :cond_4
    iget v3, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    add-int/lit8 v4, v3, 0x1

    iput v4, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    int-to-byte v2, v2

    aput-byte v2, v1, v3

    add-int/lit8 p2, p2, 0x1

    if-lt p2, p3, :cond_0

    :cond_5
    return-void
.end method

.method private final _writeStringSegment([CII)V
    .locals 5
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;,
            Lcom/fasterxml/jackson/core/JsonGenerationException;
        }
    .end annotation

    add-int/2addr p3, p2

    .line 1227
    iget v0, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    .line 1228
    iget-object v1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputBuffer:[B

    .line 1229
    iget-object v2, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputEscapes:[I

    :goto_0
    if-ge p2, p3, :cond_1

    .line 1232
    aget-char v3, p1, p2

    const/16 v4, 0x7f

    if-gt v3, v4, :cond_1

    .line 1234
    aget v4, v2, v3

    if-eqz v4, :cond_0

    goto :goto_1

    :cond_0
    add-int/lit8 v4, v0, 0x1

    int-to-byte v3, v3

    .line 1237
    aput-byte v3, v1, v0

    add-int/lit8 p2, p2, 0x1

    move v0, v4

    goto :goto_0

    .line 1240
    :cond_1
    :goto_1
    iput v0, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    if-ge p2, p3, :cond_4

    .line 1243
    iget-object v0, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_characterEscapes:Lcom/fasterxml/jackson/core/io/CharacterEscapes;

    if-eqz v0, :cond_2

    .line 1244
    invoke-direct {p0, p1, p2, p3}, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_writeCustomStringSegment2([CII)V

    goto :goto_2

    .line 1246
    :cond_2
    iget v0, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_maximumNonEscapedChar:I

    if-nez v0, :cond_3

    .line 1247
    invoke-direct {p0, p1, p2, p3}, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_writeStringSegment2([CII)V

    goto :goto_2

    .line 1249
    :cond_3
    invoke-direct {p0, p1, p2, p3}, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_writeStringSegmentASCII2([CII)V

    :cond_4
    :goto_2
    return-void
.end method

.method private final _writeStringSegment2([CII)V
    .locals 6
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;,
            Lcom/fasterxml/jackson/core/JsonGenerationException;
        }
    .end annotation

    .line 1263
    iget v0, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    sub-int v1, p3, p2

    mul-int/lit8 v1, v1, 0x6

    add-int/2addr v0, v1

    iget v1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputEnd:I

    if-le v0, v1, :cond_0

    .line 1264
    invoke-virtual {p0}, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_flushBuffer()V

    .line 1267
    :cond_0
    iget v0, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    .line 1269
    iget-object v1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputBuffer:[B

    .line 1270
    iget-object v2, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputEscapes:[I

    :goto_0
    if-ge p2, p3, :cond_5

    add-int/lit8 v3, p2, 0x1

    .line 1273
    aget-char p2, p1, p2

    const/16 v4, 0x7f

    if-gt p2, v4, :cond_3

    .line 1275
    aget v4, v2, p2

    if-nez v4, :cond_1

    add-int/lit8 v4, v0, 0x1

    int-to-byte p2, p2

    .line 1276
    aput-byte p2, v1, v0

    move p2, v3

    move v0, v4

    goto :goto_0

    .line 1279
    :cond_1
    aget v4, v2, p2

    if-lez v4, :cond_2

    add-int/lit8 p2, v0, 0x1

    const/16 v5, 0x5c

    .line 1281
    aput-byte v5, v1, v0

    add-int/lit8 v0, p2, 0x1

    int-to-byte v4, v4

    .line 1282
    aput-byte v4, v1, p2

    :goto_1
    move p2, v3

    goto :goto_0

    .line 1285
    :cond_2
    invoke-direct {p0, p2, v0}, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_writeGenericEscape(II)I

    move-result v0

    goto :goto_1

    :cond_3
    const/16 v4, 0x7ff

    if-gt p2, v4, :cond_4

    add-int/lit8 v4, v0, 0x1

    shr-int/lit8 v5, p2, 0x6

    or-int/lit16 v5, v5, 0xc0

    int-to-byte v5, v5

    .line 1290
    aput-byte v5, v1, v0

    add-int/lit8 v0, v4, 0x1

    and-int/lit8 p2, p2, 0x3f

    or-int/lit16 p2, p2, 0x80

    int-to-byte p2, p2

    .line 1291
    aput-byte p2, v1, v4

    goto :goto_1

    .line 1293
    :cond_4
    invoke-direct {p0, p2, v0}, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputMultiByteChar(II)I

    move-result p2

    move v0, p2

    goto :goto_1

    .line 1296
    :cond_5
    iput v0, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    return-void
.end method

.method private final _writeStringSegmentASCII2([CII)V
    .locals 7
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;,
            Lcom/fasterxml/jackson/core/JsonGenerationException;
        }
    .end annotation

    .line 1314
    iget v0, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    sub-int v1, p3, p2

    mul-int/lit8 v1, v1, 0x6

    add-int/2addr v0, v1

    iget v1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputEnd:I

    if-le v0, v1, :cond_0

    .line 1315
    invoke-virtual {p0}, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_flushBuffer()V

    .line 1318
    :cond_0
    iget v0, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    .line 1320
    iget-object v1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputBuffer:[B

    .line 1321
    iget-object v2, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputEscapes:[I

    .line 1322
    iget v3, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_maximumNonEscapedChar:I

    :goto_0
    if-ge p2, p3, :cond_6

    add-int/lit8 v4, p2, 0x1

    .line 1325
    aget-char p2, p1, p2

    const/16 v5, 0x7f

    if-gt p2, v5, :cond_3

    .line 1327
    aget v5, v2, p2

    if-nez v5, :cond_1

    add-int/lit8 v5, v0, 0x1

    int-to-byte p2, p2

    .line 1328
    aput-byte p2, v1, v0

    move p2, v4

    move v0, v5

    goto :goto_0

    .line 1331
    :cond_1
    aget v5, v2, p2

    if-lez v5, :cond_2

    add-int/lit8 p2, v0, 0x1

    const/16 v6, 0x5c

    .line 1333
    aput-byte v6, v1, v0

    add-int/lit8 v0, p2, 0x1

    int-to-byte v5, v5

    .line 1334
    aput-byte v5, v1, p2

    goto :goto_1

    .line 1337
    :cond_2
    invoke-direct {p0, p2, v0}, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_writeGenericEscape(II)I

    move-result v0

    goto :goto_1

    :cond_3
    if-le p2, v3, :cond_4

    .line 1342
    invoke-direct {p0, p2, v0}, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_writeGenericEscape(II)I

    move-result v0

    :goto_1
    move p2, v4

    goto :goto_0

    :cond_4
    const/16 v5, 0x7ff

    if-gt p2, v5, :cond_5

    add-int/lit8 v5, v0, 0x1

    shr-int/lit8 v6, p2, 0x6

    or-int/lit16 v6, v6, 0xc0

    int-to-byte v6, v6

    .line 1346
    aput-byte v6, v1, v0

    add-int/lit8 v0, v5, 0x1

    and-int/lit8 p2, p2, 0x3f

    or-int/lit16 p2, p2, 0x80

    int-to-byte p2, p2

    .line 1347
    aput-byte p2, v1, v5

    goto :goto_1

    .line 1349
    :cond_5
    invoke-direct {p0, p2, v0}, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputMultiByteChar(II)I

    move-result p2

    move v0, p2

    goto :goto_1

    .line 1352
    :cond_6
    iput v0, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    return-void
.end method

.method private final _writeStringSegments(Ljava/lang/String;Z)V
    .locals 8
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    const/16 v0, 0x22

    if-eqz p2, :cond_1

    .line 1156
    iget v1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    iget v2, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputEnd:I

    if-lt v1, v2, :cond_0

    .line 1157
    invoke-virtual {p0}, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_flushBuffer()V

    .line 1159
    :cond_0
    iget-object v1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputBuffer:[B

    iget v2, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    add-int/lit8 v3, v2, 0x1

    iput v3, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    aput-byte v0, v1, v2

    .line 1162
    :cond_1
    invoke-virtual {p1}, Ljava/lang/String;->length()I

    move-result v1

    .line 1164
    iget-object v2, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_charBuffer:[C

    const/4 v3, 0x0

    const/4 v4, 0x0

    :goto_0
    if-lez v1, :cond_3

    .line 1167
    iget v5, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputMaxContiguous:I

    invoke-static {v5, v1}, Ljava/lang/Math;->min(II)I

    move-result v5

    add-int v6, v4, v5

    .line 1168
    invoke-virtual {p1, v4, v6, v2, v3}, Ljava/lang/String;->getChars(II[CI)V

    .line 1169
    iget v4, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    add-int/2addr v4, v5

    iget v7, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputEnd:I

    if-le v4, v7, :cond_2

    .line 1170
    invoke-virtual {p0}, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_flushBuffer()V

    .line 1172
    :cond_2
    invoke-direct {p0, v2, v3, v5}, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_writeStringSegment([CII)V

    sub-int/2addr v1, v5

    move v4, v6

    goto :goto_0

    :cond_3
    if-eqz p2, :cond_5

    .line 1178
    iget p1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    iget p2, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputEnd:I

    if-lt p1, p2, :cond_4

    .line 1179
    invoke-virtual {p0}, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_flushBuffer()V

    .line 1181
    :cond_4
    iget-object p1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputBuffer:[B

    iget p2, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    add-int/lit8 v1, p2, 0x1

    iput v1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    aput-byte v0, p1, p2

    :cond_5
    return-void
.end method

.method private final _writeStringSegments([CII)V
    .locals 3
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;,
            Lcom/fasterxml/jackson/core/JsonGenerationException;
        }
    .end annotation

    .line 1195
    :cond_0
    iget v0, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputMaxContiguous:I

    invoke-static {v0, p3}, Ljava/lang/Math;->min(II)I

    move-result v0

    .line 1196
    iget v1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    add-int/2addr v1, v0

    iget v2, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputEnd:I

    if-le v1, v2, :cond_1

    .line 1197
    invoke-virtual {p0}, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_flushBuffer()V

    .line 1199
    :cond_1
    invoke-direct {p0, p1, p2, v0}, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_writeStringSegment([CII)V

    add-int/2addr p2, v0

    sub-int/2addr p3, v0

    if-gtz p3, :cond_0

    return-void
.end method

.method private final _writeUTF8Segment([BII)V
    .locals 4
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;,
            Lcom/fasterxml/jackson/core/JsonGenerationException;
        }
    .end annotation

    .line 1487
    iget-object v0, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputEscapes:[I

    add-int v1, p2, p3

    move v2, p2

    :goto_0
    if-ge v2, v1, :cond_1

    add-int/lit8 v3, v2, 0x1

    .line 1491
    aget-byte v2, p1, v2

    if-ltz v2, :cond_0

    .line 1492
    aget v2, v0, v2

    if-eqz v2, :cond_0

    .line 1493
    invoke-direct {p0, p1, p2, p3}, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_writeUTF8Segment2([BII)V

    return-void

    :cond_0
    move v2, v3

    goto :goto_0

    .line 1499
    :cond_1
    iget v0, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    add-int/2addr v0, p3

    iget v1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputEnd:I

    if-le v0, v1, :cond_2

    .line 1500
    invoke-virtual {p0}, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_flushBuffer()V

    .line 1502
    :cond_2
    iget-object v0, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputBuffer:[B

    iget v1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    invoke-static {p1, p2, v0, v1, p3}, Ljava/lang/System;->arraycopy(Ljava/lang/Object;ILjava/lang/Object;II)V

    .line 1503
    iget p1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    add-int/2addr p1, p3

    iput p1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    return-void
.end method

.method private final _writeUTF8Segment2([BII)V
    .locals 6
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;,
            Lcom/fasterxml/jackson/core/JsonGenerationException;
        }
    .end annotation

    .line 1509
    iget v0, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    mul-int/lit8 v1, p3, 0x6

    add-int/2addr v1, v0

    .line 1512
    iget v2, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputEnd:I

    if-le v1, v2, :cond_0

    .line 1513
    invoke-virtual {p0}, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_flushBuffer()V

    .line 1514
    iget v0, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    .line 1517
    :cond_0
    iget-object v1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputBuffer:[B

    .line 1518
    iget-object v2, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputEscapes:[I

    add-int/2addr p3, p2

    :goto_0
    if-ge p2, p3, :cond_4

    add-int/lit8 v3, p2, 0x1

    .line 1522
    aget-byte p2, p1, p2

    if-ltz p2, :cond_3

    .line 1524
    aget v4, v2, p2

    if-nez v4, :cond_1

    goto :goto_2

    .line 1528
    :cond_1
    aget v4, v2, p2

    if-lez v4, :cond_2

    add-int/lit8 p2, v0, 0x1

    const/16 v5, 0x5c

    .line 1530
    aput-byte v5, v1, v0

    add-int/lit8 v0, p2, 0x1

    int-to-byte v4, v4

    .line 1531
    aput-byte v4, v1, p2

    goto :goto_1

    .line 1534
    :cond_2
    invoke-direct {p0, p2, v0}, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_writeGenericEscape(II)I

    move-result p2

    move v0, p2

    :goto_1
    move p2, v3

    goto :goto_0

    :cond_3
    :goto_2
    add-int/lit8 v4, v0, 0x1

    .line 1525
    aput-byte p2, v1, v0

    move p2, v3

    move v0, v4

    goto :goto_0

    .line 1537
    :cond_4
    iput v0, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    return-void
.end method

.method private final _writeUTF8Segments([BII)V
    .locals 1
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;,
            Lcom/fasterxml/jackson/core/JsonGenerationException;
        }
    .end annotation

    .line 1476
    :cond_0
    iget v0, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputMaxContiguous:I

    invoke-static {v0, p3}, Ljava/lang/Math;->min(II)I

    move-result v0

    .line 1477
    invoke-direct {p0, p1, p2, v0}, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_writeUTF8Segment([BII)V

    add-int/2addr p2, v0

    sub-int/2addr p3, v0

    if-gtz p3, :cond_0

    return-void
.end method

.method private final _writeUnq(Lcom/fasterxml/jackson/core/SerializableString;)V
    .locals 2
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    .line 260
    iget-object v0, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputBuffer:[B

    iget v1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    invoke-interface {p1, v0, v1}, Lcom/fasterxml/jackson/core/SerializableString;->appendQuotedUTF8([BI)I

    move-result v0

    if-gez v0, :cond_0

    .line 262
    invoke-interface {p1}, Lcom/fasterxml/jackson/core/SerializableString;->asQuotedUTF8()[B

    move-result-object p1

    invoke-direct {p0, p1}, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_writeBytes([B)V

    goto :goto_0

    .line 264
    :cond_0
    iget p1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    add-int/2addr p1, v0

    iput p1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    :goto_0
    return-void
.end method


# virtual methods
.method protected final _flushBuffer()V
    .locals 4
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    .line 1843
    iget v0, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    if-lez v0, :cond_0

    const/4 v1, 0x0

    .line 1845
    iput v1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    .line 1846
    iget-object v2, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputStream:Ljava/io/OutputStream;

    iget-object v3, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputBuffer:[B

    invoke-virtual {v2, v3, v1, v0}, Ljava/io/OutputStream;->write([BII)V

    :cond_0
    return-void
.end method

.method protected final _outputSurrogates(II)V
    .locals 2
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    .line 1767
    invoke-virtual {p0, p1, p2}, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_decodeSurrogate(II)I

    move-result p1

    .line 1768
    iget p2, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    add-int/lit8 p2, p2, 0x4

    iget v0, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputEnd:I

    if-le p2, v0, :cond_0

    .line 1769
    invoke-virtual {p0}, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_flushBuffer()V

    .line 1771
    :cond_0
    iget-object p2, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputBuffer:[B

    .line 1772
    iget v0, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    add-int/lit8 v1, v0, 0x1

    iput v1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    shr-int/lit8 v1, p1, 0x12

    or-int/lit16 v1, v1, 0xf0

    int-to-byte v1, v1

    aput-byte v1, p2, v0

    .line 1773
    iget v0, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    add-int/lit8 v1, v0, 0x1

    iput v1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    shr-int/lit8 v1, p1, 0xc

    and-int/lit8 v1, v1, 0x3f

    or-int/lit16 v1, v1, 0x80

    int-to-byte v1, v1

    aput-byte v1, p2, v0

    .line 1774
    iget v0, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    add-int/lit8 v1, v0, 0x1

    iput v1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    shr-int/lit8 v1, p1, 0x6

    and-int/lit8 v1, v1, 0x3f

    or-int/lit16 v1, v1, 0x80

    int-to-byte v1, v1

    aput-byte v1, p2, v0

    .line 1775
    iget v0, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    add-int/lit8 v1, v0, 0x1

    iput v1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    and-int/lit8 p1, p1, 0x3f

    or-int/lit16 p1, p1, 0x80

    int-to-byte p1, p1

    aput-byte p1, p2, v0

    return-void
.end method

.method protected _releaseBuffers()V
    .locals 3

    .line 1093
    iget-object v0, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputBuffer:[B

    const/4 v1, 0x0

    if-eqz v0, :cond_0

    .line 1094
    iget-boolean v2, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_bufferRecyclable:Z

    if-eqz v2, :cond_0

    .line 1095
    iput-object v1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputBuffer:[B

    .line 1096
    iget-object v2, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_ioContext:Lcom/fasterxml/jackson/core/io/IOContext;

    invoke-virtual {v2, v0}, Lcom/fasterxml/jackson/core/io/IOContext;->releaseWriteEncodingBuffer([B)V

    .line 1098
    :cond_0
    iget-object v0, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_charBuffer:[C

    if-eqz v0, :cond_1

    .line 1100
    iput-object v1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_charBuffer:[C

    .line 1101
    iget-object v1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_ioContext:Lcom/fasterxml/jackson/core/io/IOContext;

    invoke-virtual {v1, v0}, Lcom/fasterxml/jackson/core/io/IOContext;->releaseConcatBuffer([C)V

    :cond_1
    return-void
.end method

.method protected final _verifyPrettyValueWrite(Ljava/lang/String;I)V
    .locals 0
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;,
            Lcom/fasterxml/jackson/core/JsonGenerationException;
        }
    .end annotation

    packed-switch p2, :pswitch_data_0

    .line 1024
    invoke-virtual {p0}, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_throwInternal()V

    goto :goto_0

    .line 1013
    :pswitch_0
    iget-object p1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_cfgPrettyPrinter:Lcom/fasterxml/jackson/core/PrettyPrinter;

    invoke-interface {p1, p0}, Lcom/fasterxml/jackson/core/PrettyPrinter;->writeRootValueSeparator(Lcom/fasterxml/jackson/core/JsonGenerator;)V

    goto :goto_0

    .line 1010
    :pswitch_1
    iget-object p1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_cfgPrettyPrinter:Lcom/fasterxml/jackson/core/PrettyPrinter;

    invoke-interface {p1, p0}, Lcom/fasterxml/jackson/core/PrettyPrinter;->writeObjectFieldValueSeparator(Lcom/fasterxml/jackson/core/JsonGenerator;)V

    goto :goto_0

    .line 1007
    :pswitch_2
    iget-object p1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_cfgPrettyPrinter:Lcom/fasterxml/jackson/core/PrettyPrinter;

    invoke-interface {p1, p0}, Lcom/fasterxml/jackson/core/PrettyPrinter;->writeArrayValueSeparator(Lcom/fasterxml/jackson/core/JsonGenerator;)V

    goto :goto_0

    .line 1017
    :pswitch_3
    iget-object p1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_writeContext:Lcom/fasterxml/jackson/core/json/JsonWriteContext;

    invoke-virtual {p1}, Lcom/fasterxml/jackson/core/json/JsonWriteContext;->inArray()Z

    move-result p1

    if-eqz p1, :cond_0

    .line 1018
    iget-object p1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_cfgPrettyPrinter:Lcom/fasterxml/jackson/core/PrettyPrinter;

    invoke-interface {p1, p0}, Lcom/fasterxml/jackson/core/PrettyPrinter;->beforeArrayValues(Lcom/fasterxml/jackson/core/JsonGenerator;)V

    goto :goto_0

    .line 1019
    :cond_0
    iget-object p1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_writeContext:Lcom/fasterxml/jackson/core/json/JsonWriteContext;

    invoke-virtual {p1}, Lcom/fasterxml/jackson/core/json/JsonWriteContext;->inObject()Z

    move-result p1

    if-eqz p1, :cond_1

    .line 1020
    iget-object p1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_cfgPrettyPrinter:Lcom/fasterxml/jackson/core/PrettyPrinter;

    invoke-interface {p1, p0}, Lcom/fasterxml/jackson/core/PrettyPrinter;->beforeObjectEntries(Lcom/fasterxml/jackson/core/JsonGenerator;)V

    :cond_1
    :goto_0
    return-void

    nop

    :pswitch_data_0
    .packed-switch 0x0
        :pswitch_3
        :pswitch_2
        :pswitch_1
        :pswitch_0
    .end packed-switch
.end method

.method protected final _verifyValueWrite(Ljava/lang/String;)V
    .locals 3
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;,
            Lcom/fasterxml/jackson/core/JsonGenerationException;
        }
    .end annotation

    .line 965
    iget-object v0, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_writeContext:Lcom/fasterxml/jackson/core/json/JsonWriteContext;

    invoke-virtual {v0}, Lcom/fasterxml/jackson/core/json/JsonWriteContext;->writeValue()I

    move-result v0

    const/4 v1, 0x5

    if-ne v0, v1, :cond_0

    .line 967
    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "Can not "

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v2, ", expecting field name"

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-virtual {p0, v1}, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_reportError(Ljava/lang/String;)V

    .line 969
    :cond_0
    iget-object v1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_cfgPrettyPrinter:Lcom/fasterxml/jackson/core/PrettyPrinter;

    if-nez v1, :cond_3

    packed-switch v0, :pswitch_data_0

    return-void

    .line 979
    :pswitch_0
    iget-object p1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_rootValueSeparator:Lcom/fasterxml/jackson/core/SerializableString;

    if-eqz p1, :cond_1

    .line 980
    iget-object p1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_rootValueSeparator:Lcom/fasterxml/jackson/core/SerializableString;

    invoke-interface {p1}, Lcom/fasterxml/jackson/core/SerializableString;->asUnquotedUTF8()[B

    move-result-object p1

    .line 981
    array-length v0, p1

    if-lez v0, :cond_1

    .line 982
    invoke-direct {p0, p1}, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_writeBytes([B)V

    :cond_1
    return-void

    :pswitch_1
    const/16 p1, 0x3a

    goto :goto_0

    :pswitch_2
    const/16 p1, 0x2c

    .line 990
    :goto_0
    iget v0, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    iget v1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputEnd:I

    if-lt v0, v1, :cond_2

    .line 991
    invoke-virtual {p0}, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_flushBuffer()V

    .line 993
    :cond_2
    iget-object v0, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputBuffer:[B

    iget v1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    aput-byte p1, v0, v1

    .line 994
    iget p1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    add-int/lit8 p1, p1, 0x1

    iput p1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    return-void

    .line 998
    :cond_3
    invoke-virtual {p0, p1, v0}, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_verifyPrettyValueWrite(Ljava/lang/String;I)V

    return-void

    :pswitch_data_0
    .packed-switch 0x1
        :pswitch_2
        :pswitch_1
        :pswitch_0
    .end packed-switch
.end method

.method protected final _writeBinary(Lcom/fasterxml/jackson/core/Base64Variant;Ljava/io/InputStream;[B)I
    .locals 12
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;,
            Lcom/fasterxml/jackson/core/JsonGenerationException;
        }
    .end annotation

    .line 1659
    iget v0, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputEnd:I

    add-int/lit8 v0, v0, -0x6

    .line 1660
    invoke-virtual {p1}, Lcom/fasterxml/jackson/core/Base64Variant;->getMaxLineLength()I

    move-result v1

    const/4 v2, 0x2

    shr-int/2addr v1, v2

    const/4 v3, 0x0

    const/4 v4, -0x3

    move v11, v1

    const/4 v1, 0x0

    const/4 v8, 0x0

    const/4 v9, 0x0

    :cond_0
    :goto_0
    if-le v8, v4, :cond_5

    .line 1665
    array-length v10, p3

    move-object v5, p0

    move-object v6, p2

    move-object v7, p3

    invoke-direct/range {v5 .. v10}, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_readMore(Ljava/io/InputStream;[BIII)I

    move-result v4

    const/4 v5, 0x3

    if-ge v4, v5, :cond_4

    if-lez v4, :cond_3

    .line 1690
    iget p2, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    if-le p2, v0, :cond_1

    .line 1691
    invoke-virtual {p0}, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_flushBuffer()V

    .line 1693
    :cond_1
    aget-byte p2, p3, v3

    shl-int/lit8 p2, p2, 0x10

    const/4 v0, 0x1

    if-ge v0, v4, :cond_2

    .line 1696
    aget-byte p3, p3, v0

    and-int/lit16 p3, p3, 0xff

    shl-int/lit8 p3, p3, 0x8

    or-int/2addr p2, p3

    const/4 v0, 0x2

    :cond_2
    add-int/2addr v1, v0

    .line 1700
    iget-object p3, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputBuffer:[B

    iget v2, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    invoke-virtual {p1, p2, v0, p3, v2}, Lcom/fasterxml/jackson/core/Base64Variant;->encodeBase64Partial(II[BI)I

    move-result p1

    iput p1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    :cond_3
    return v1

    :cond_4
    add-int/lit8 v5, v4, -0x3

    move v9, v4

    move v4, v5

    const/4 v8, 0x0

    .line 1672
    :cond_5
    iget v5, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    if-le v5, v0, :cond_6

    .line 1673
    invoke-virtual {p0}, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_flushBuffer()V

    :cond_6
    add-int/lit8 v5, v8, 0x1

    .line 1676
    aget-byte v6, p3, v8

    shl-int/lit8 v6, v6, 0x8

    add-int/lit8 v7, v5, 0x1

    .line 1677
    aget-byte v5, p3, v5

    and-int/lit16 v5, v5, 0xff

    or-int/2addr v5, v6

    shl-int/lit8 v5, v5, 0x8

    add-int/lit8 v8, v7, 0x1

    .line 1678
    aget-byte v6, p3, v7

    and-int/lit16 v6, v6, 0xff

    or-int/2addr v5, v6

    add-int/lit8 v1, v1, 0x3

    .line 1680
    iget-object v6, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputBuffer:[B

    iget v7, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    invoke-virtual {p1, v5, v6, v7}, Lcom/fasterxml/jackson/core/Base64Variant;->encodeBase64Chunk(I[BI)I

    move-result v5

    iput v5, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    add-int/lit8 v11, v11, -0x1

    if-gtz v11, :cond_0

    .line 1682
    iget-object v5, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputBuffer:[B

    iget v6, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    add-int/lit8 v7, v6, 0x1

    iput v7, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    const/16 v7, 0x5c

    aput-byte v7, v5, v6

    .line 1683
    iget-object v5, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputBuffer:[B

    iget v6, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    add-int/lit8 v7, v6, 0x1

    iput v7, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    const/16 v7, 0x6e

    aput-byte v7, v5, v6

    .line 1684
    invoke-virtual {p1}, Lcom/fasterxml/jackson/core/Base64Variant;->getMaxLineLength()I

    move-result v5

    shr-int/2addr v5, v2

    move v11, v5

    goto/16 :goto_0
.end method

.method protected final _writeBinary(Lcom/fasterxml/jackson/core/Base64Variant;Ljava/io/InputStream;[BI)I
    .locals 11
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;,
            Lcom/fasterxml/jackson/core/JsonGenerationException;
        }
    .end annotation

    .line 1598
    iget v0, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputEnd:I

    add-int/lit8 v0, v0, -0x6

    .line 1599
    invoke-virtual {p1}, Lcom/fasterxml/jackson/core/Base64Variant;->getMaxLineLength()I

    move-result v1

    const/4 v2, 0x2

    shr-int/2addr v1, v2

    const/4 v3, -0x3

    const/4 v4, 0x0

    const/4 v8, 0x0

    const/4 v9, 0x0

    :cond_0
    :goto_0
    if-le p4, v2, :cond_4

    if-le v8, v3, :cond_2

    move-object v5, p0

    move-object v6, p2

    move-object v7, p3

    move v10, p4

    .line 1603
    invoke-direct/range {v5 .. v10}, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_readMore(Ljava/io/InputStream;[BIII)I

    move-result v3

    const/4 v5, 0x3

    if-ge v3, v5, :cond_1

    move v9, v3

    const/4 v8, 0x0

    goto :goto_1

    :cond_1
    add-int/lit8 v5, v3, -0x3

    move v9, v3

    move v3, v5

    const/4 v8, 0x0

    .line 1610
    :cond_2
    iget v5, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    if-le v5, v0, :cond_3

    .line 1611
    invoke-virtual {p0}, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_flushBuffer()V

    :cond_3
    add-int/lit8 v5, v8, 0x1

    .line 1613
    aget-byte v6, p3, v8

    shl-int/lit8 v6, v6, 0x8

    add-int/lit8 v7, v5, 0x1

    .line 1614
    aget-byte v5, p3, v5

    and-int/lit16 v5, v5, 0xff

    or-int/2addr v5, v6

    shl-int/lit8 v5, v5, 0x8

    add-int/lit8 v8, v7, 0x1

    .line 1615
    aget-byte v6, p3, v7

    and-int/lit16 v6, v6, 0xff

    or-int/2addr v5, v6

    add-int/lit8 p4, p4, -0x3

    .line 1617
    iget-object v6, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputBuffer:[B

    iget v7, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    invoke-virtual {p1, v5, v6, v7}, Lcom/fasterxml/jackson/core/Base64Variant;->encodeBase64Chunk(I[BI)I

    move-result v5

    iput v5, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    add-int/lit8 v1, v1, -0x1

    if-gtz v1, :cond_0

    .line 1619
    iget-object v1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputBuffer:[B

    iget v5, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    add-int/lit8 v6, v5, 0x1

    iput v6, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    const/16 v6, 0x5c

    aput-byte v6, v1, v5

    .line 1620
    iget-object v1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputBuffer:[B

    iget v5, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    add-int/lit8 v6, v5, 0x1

    iput v6, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    const/16 v6, 0x6e

    aput-byte v6, v1, v5

    .line 1621
    invoke-virtual {p1}, Lcom/fasterxml/jackson/core/Base64Variant;->getMaxLineLength()I

    move-result v1

    shr-int/2addr v1, v2

    goto :goto_0

    :cond_4
    :goto_1
    if-lez p4, :cond_7

    move-object v5, p0

    move-object v6, p2

    move-object v7, p3

    move v10, p4

    .line 1627
    invoke-direct/range {v5 .. v10}, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_readMore(Ljava/io/InputStream;[BIII)I

    move-result p2

    if-lez p2, :cond_7

    .line 1630
    iget v1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    if-le v1, v0, :cond_5

    .line 1631
    invoke-virtual {p0}, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_flushBuffer()V

    .line 1633
    :cond_5
    aget-byte v0, p3, v4

    shl-int/lit8 v0, v0, 0x10

    const/4 v1, 0x1

    if-ge v1, p2, :cond_6

    .line 1636
    aget-byte p2, p3, v1

    and-int/lit16 p2, p2, 0xff

    shl-int/lit8 p2, p2, 0x8

    or-int/2addr v0, p2

    const/4 v1, 0x2

    .line 1641
    :cond_6
    iget-object p2, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputBuffer:[B

    iget p3, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    invoke-virtual {p1, v0, v1, p2, p3}, Lcom/fasterxml/jackson/core/Base64Variant;->encodeBase64Partial(II[BI)I

    move-result p1

    iput p1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    sub-int/2addr p4, v1

    :cond_7
    return p4
.end method

.method protected final _writeBinary(Lcom/fasterxml/jackson/core/Base64Variant;[BII)V
    .locals 7
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;,
            Lcom/fasterxml/jackson/core/JsonGenerationException;
        }
    .end annotation

    add-int/lit8 v0, p4, -0x3

    .line 1553
    iget v1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputEnd:I

    add-int/lit8 v1, v1, -0x6

    .line 1554
    invoke-virtual {p1}, Lcom/fasterxml/jackson/core/Base64Variant;->getMaxLineLength()I

    move-result v2

    const/4 v3, 0x2

    shr-int/2addr v2, v3

    :goto_0
    if-gt p3, v0, :cond_2

    .line 1558
    iget v4, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    if-le v4, v1, :cond_0

    .line 1559
    invoke-virtual {p0}, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_flushBuffer()V

    :cond_0
    add-int/lit8 v4, p3, 0x1

    .line 1562
    aget-byte p3, p2, p3

    shl-int/lit8 p3, p3, 0x8

    add-int/lit8 v5, v4, 0x1

    .line 1563
    aget-byte v4, p2, v4

    and-int/lit16 v4, v4, 0xff

    or-int/2addr p3, v4

    shl-int/lit8 p3, p3, 0x8

    add-int/lit8 v4, v5, 0x1

    .line 1564
    aget-byte v5, p2, v5

    and-int/lit16 v5, v5, 0xff

    or-int/2addr p3, v5

    .line 1565
    iget-object v5, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputBuffer:[B

    iget v6, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    invoke-virtual {p1, p3, v5, v6}, Lcom/fasterxml/jackson/core/Base64Variant;->encodeBase64Chunk(I[BI)I

    move-result p3

    iput p3, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    add-int/lit8 v2, v2, -0x1

    if-gtz v2, :cond_1

    .line 1568
    iget-object p3, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputBuffer:[B

    iget v2, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    add-int/lit8 v5, v2, 0x1

    iput v5, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    const/16 v5, 0x5c

    aput-byte v5, p3, v2

    .line 1569
    iget-object p3, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputBuffer:[B

    iget v2, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    add-int/lit8 v5, v2, 0x1

    iput v5, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    const/16 v5, 0x6e

    aput-byte v5, p3, v2

    .line 1570
    invoke-virtual {p1}, Lcom/fasterxml/jackson/core/Base64Variant;->getMaxLineLength()I

    move-result p3

    shr-int/2addr p3, v3

    move v2, p3

    :cond_1
    move p3, v4

    goto :goto_0

    :cond_2
    sub-int/2addr p4, p3

    if-lez p4, :cond_5

    .line 1577
    iget v0, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    if-le v0, v1, :cond_3

    .line 1578
    invoke-virtual {p0}, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_flushBuffer()V

    :cond_3
    add-int/lit8 v0, p3, 0x1

    .line 1580
    aget-byte p3, p2, p3

    shl-int/lit8 p3, p3, 0x10

    if-ne p4, v3, :cond_4

    .line 1582
    aget-byte p2, p2, v0

    and-int/lit16 p2, p2, 0xff

    shl-int/lit8 p2, p2, 0x8

    or-int/2addr p3, p2

    .line 1584
    :cond_4
    iget-object p2, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputBuffer:[B

    iget v0, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    invoke-virtual {p1, p3, p4, p2, v0}, Lcom/fasterxml/jackson/core/Base64Variant;->encodeBase64Partial(II[BI)I

    move-result p1

    iput p1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    :cond_5
    return-void
.end method

.method protected final _writePPFieldName(Lcom/fasterxml/jackson/core/SerializableString;)V
    .locals 5
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    .line 384
    iget-object v0, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_writeContext:Lcom/fasterxml/jackson/core/json/JsonWriteContext;

    invoke-interface {p1}, Lcom/fasterxml/jackson/core/SerializableString;->getValue()Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Lcom/fasterxml/jackson/core/json/JsonWriteContext;->writeFieldName(Ljava/lang/String;)I

    move-result v0

    const/4 v1, 0x4

    if-ne v0, v1, :cond_0

    const-string v1, "Can not write a field name, expecting a value"

    .line 386
    invoke-virtual {p0, v1}, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_reportError(Ljava/lang/String;)V

    :cond_0
    const/4 v1, 0x1

    if-ne v0, v1, :cond_1

    .line 389
    iget-object v0, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_cfgPrettyPrinter:Lcom/fasterxml/jackson/core/PrettyPrinter;

    invoke-interface {v0, p0}, Lcom/fasterxml/jackson/core/PrettyPrinter;->writeObjectEntrySeparator(Lcom/fasterxml/jackson/core/JsonGenerator;)V

    goto :goto_0

    .line 391
    :cond_1
    iget-object v0, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_cfgPrettyPrinter:Lcom/fasterxml/jackson/core/PrettyPrinter;

    invoke-interface {v0, p0}, Lcom/fasterxml/jackson/core/PrettyPrinter;->beforeObjectEntries(Lcom/fasterxml/jackson/core/JsonGenerator;)V

    .line 394
    :goto_0
    iget-boolean v0, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_cfgUnqNames:Z

    xor-int/2addr v0, v1

    const/16 v1, 0x22

    if-eqz v0, :cond_3

    .line 396
    iget v2, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    iget v3, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputEnd:I

    if-lt v2, v3, :cond_2

    .line 397
    invoke-virtual {p0}, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_flushBuffer()V

    .line 399
    :cond_2
    iget-object v2, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputBuffer:[B

    iget v3, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    add-int/lit8 v4, v3, 0x1

    iput v4, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    aput-byte v1, v2, v3

    .line 401
    :cond_3
    invoke-interface {p1}, Lcom/fasterxml/jackson/core/SerializableString;->asQuotedUTF8()[B

    move-result-object p1

    invoke-direct {p0, p1}, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_writeBytes([B)V

    if-eqz v0, :cond_5

    .line 403
    iget p1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    iget v0, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputEnd:I

    if-lt p1, v0, :cond_4

    .line 404
    invoke-virtual {p0}, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_flushBuffer()V

    .line 406
    :cond_4
    iget-object p1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputBuffer:[B

    iget v0, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    add-int/lit8 v2, v0, 0x1

    iput v2, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    aput-byte v1, p1, v0

    :cond_5
    return-void
.end method

.method protected final _writePPFieldName(Ljava/lang/String;)V
    .locals 5
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    .line 344
    iget-object v0, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_writeContext:Lcom/fasterxml/jackson/core/json/JsonWriteContext;

    invoke-virtual {v0, p1}, Lcom/fasterxml/jackson/core/json/JsonWriteContext;->writeFieldName(Ljava/lang/String;)I

    move-result v0

    const/4 v1, 0x4

    if-ne v0, v1, :cond_0

    const-string v1, "Can not write a field name, expecting a value"

    .line 346
    invoke-virtual {p0, v1}, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_reportError(Ljava/lang/String;)V

    :cond_0
    const/4 v1, 0x1

    if-ne v0, v1, :cond_1

    .line 349
    iget-object v0, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_cfgPrettyPrinter:Lcom/fasterxml/jackson/core/PrettyPrinter;

    invoke-interface {v0, p0}, Lcom/fasterxml/jackson/core/PrettyPrinter;->writeObjectEntrySeparator(Lcom/fasterxml/jackson/core/JsonGenerator;)V

    goto :goto_0

    .line 351
    :cond_1
    iget-object v0, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_cfgPrettyPrinter:Lcom/fasterxml/jackson/core/PrettyPrinter;

    invoke-interface {v0, p0}, Lcom/fasterxml/jackson/core/PrettyPrinter;->beforeObjectEntries(Lcom/fasterxml/jackson/core/JsonGenerator;)V

    .line 353
    :goto_0
    iget-boolean v0, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_cfgUnqNames:Z

    const/4 v2, 0x0

    if-eqz v0, :cond_2

    .line 354
    invoke-direct {p0, p1, v2}, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_writeStringSegments(Ljava/lang/String;Z)V

    return-void

    .line 357
    :cond_2
    invoke-virtual {p1}, Ljava/lang/String;->length()I

    move-result v0

    .line 358
    iget v3, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_charBufferLength:I

    if-le v0, v3, :cond_3

    .line 359
    invoke-direct {p0, p1, v1}, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_writeStringSegments(Ljava/lang/String;Z)V

    return-void

    .line 362
    :cond_3
    iget v1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    iget v3, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputEnd:I

    if-lt v1, v3, :cond_4

    .line 363
    invoke-virtual {p0}, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_flushBuffer()V

    .line 365
    :cond_4
    iget-object v1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputBuffer:[B

    iget v3, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    add-int/lit8 v4, v3, 0x1

    iput v4, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    const/16 v4, 0x22

    aput-byte v4, v1, v3

    .line 366
    iget-object v1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_charBuffer:[C

    invoke-virtual {p1, v2, v0, v1, v2}, Ljava/lang/String;->getChars(II[CI)V

    .line 368
    iget p1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputMaxContiguous:I

    if-gt v0, p1, :cond_6

    .line 369
    iget p1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    add-int/2addr p1, v0

    iget v1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputEnd:I

    if-le p1, v1, :cond_5

    .line 370
    invoke-virtual {p0}, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_flushBuffer()V

    .line 372
    :cond_5
    iget-object p1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_charBuffer:[C

    invoke-direct {p0, p1, v2, v0}, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_writeStringSegment([CII)V

    goto :goto_1

    .line 374
    :cond_6
    iget-object p1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_charBuffer:[C

    invoke-direct {p0, p1, v2, v0}, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_writeStringSegments([CII)V

    .line 376
    :goto_1
    iget p1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    iget v0, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputEnd:I

    if-lt p1, v0, :cond_7

    .line 377
    invoke-virtual {p0}, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_flushBuffer()V

    .line 379
    :cond_7
    iget-object p1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputBuffer:[B

    iget v0, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    add-int/lit8 v1, v0, 0x1

    iput v1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    aput-byte v4, p1, v0

    return-void
.end method

.method public close()V
    .locals 2
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    .line 1051
    invoke-super {p0}, Lcom/fasterxml/jackson/core/json/JsonGeneratorImpl;->close()V

    .line 1057
    iget-object v0, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputBuffer:[B

    if-eqz v0, :cond_1

    sget-object v0, Lcom/fasterxml/jackson/core/JsonGenerator$Feature;->AUTO_CLOSE_JSON_CONTENT:Lcom/fasterxml/jackson/core/JsonGenerator$Feature;

    invoke-virtual {p0, v0}, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->isEnabled(Lcom/fasterxml/jackson/core/JsonGenerator$Feature;)Z

    move-result v0

    if-eqz v0, :cond_1

    .line 1060
    :goto_0
    invoke-virtual {p0}, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->getOutputContext()Lcom/fasterxml/jackson/core/json/JsonWriteContext;

    move-result-object v0

    .line 1061
    invoke-virtual {v0}, Lcom/fasterxml/jackson/core/JsonStreamContext;->inArray()Z

    move-result v1

    if-eqz v1, :cond_0

    .line 1062
    invoke-virtual {p0}, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->writeEndArray()V

    goto :goto_0

    .line 1063
    :cond_0
    invoke-virtual {v0}, Lcom/fasterxml/jackson/core/JsonStreamContext;->inObject()Z

    move-result v0

    if-eqz v0, :cond_1

    .line 1064
    invoke-virtual {p0}, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->writeEndObject()V

    goto :goto_0

    .line 1070
    :cond_1
    invoke-virtual {p0}, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_flushBuffer()V

    .line 1078
    iget-object v0, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputStream:Ljava/io/OutputStream;

    if-eqz v0, :cond_4

    .line 1079
    iget-object v0, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_ioContext:Lcom/fasterxml/jackson/core/io/IOContext;

    invoke-virtual {v0}, Lcom/fasterxml/jackson/core/io/IOContext;->isResourceManaged()Z

    move-result v0

    if-nez v0, :cond_3

    sget-object v0, Lcom/fasterxml/jackson/core/JsonGenerator$Feature;->AUTO_CLOSE_TARGET:Lcom/fasterxml/jackson/core/JsonGenerator$Feature;

    invoke-virtual {p0, v0}, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->isEnabled(Lcom/fasterxml/jackson/core/JsonGenerator$Feature;)Z

    move-result v0

    if-eqz v0, :cond_2

    goto :goto_1

    .line 1081
    :cond_2
    sget-object v0, Lcom/fasterxml/jackson/core/JsonGenerator$Feature;->FLUSH_PASSED_TO_STREAM:Lcom/fasterxml/jackson/core/JsonGenerator$Feature;

    invoke-virtual {p0, v0}, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->isEnabled(Lcom/fasterxml/jackson/core/JsonGenerator$Feature;)Z

    move-result v0

    if-eqz v0, :cond_4

    .line 1083
    iget-object v0, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputStream:Ljava/io/OutputStream;

    invoke-virtual {v0}, Ljava/io/OutputStream;->flush()V

    goto :goto_2

    .line 1080
    :cond_3
    :goto_1
    iget-object v0, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputStream:Ljava/io/OutputStream;

    invoke-virtual {v0}, Ljava/io/OutputStream;->close()V

    .line 1087
    :cond_4
    :goto_2
    invoke-virtual {p0}, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_releaseBuffers()V

    return-void
.end method

.method public flush()V
    .locals 1
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    .line 1039
    invoke-virtual {p0}, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_flushBuffer()V

    .line 1040
    iget-object v0, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputStream:Ljava/io/OutputStream;

    if-eqz v0, :cond_0

    .line 1041
    sget-object v0, Lcom/fasterxml/jackson/core/JsonGenerator$Feature;->FLUSH_PASSED_TO_STREAM:Lcom/fasterxml/jackson/core/JsonGenerator$Feature;

    invoke-virtual {p0, v0}, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->isEnabled(Lcom/fasterxml/jackson/core/JsonGenerator$Feature;)Z

    move-result v0

    if-eqz v0, :cond_0

    .line 1042
    iget-object v0, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputStream:Ljava/io/OutputStream;

    invoke-virtual {v0}, Ljava/io/OutputStream;->flush()V

    :cond_0
    return-void
.end method

.method public getOutputTarget()Ljava/lang/Object;
    .locals 1

    .line 162
    iget-object v0, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputStream:Ljava/io/OutputStream;

    return-object v0
.end method

.method public writeBinary(Lcom/fasterxml/jackson/core/Base64Variant;Ljava/io/InputStream;I)I
    .locals 3
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;,
            Lcom/fasterxml/jackson/core/JsonGenerationException;
        }
    .end annotation

    const-string v0, "write binary value"

    .line 732
    invoke-virtual {p0, v0}, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_verifyValueWrite(Ljava/lang/String;)V

    .line 734
    iget v0, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    iget v1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputEnd:I

    if-lt v0, v1, :cond_0

    .line 735
    invoke-virtual {p0}, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_flushBuffer()V

    .line 737
    :cond_0
    iget-object v0, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputBuffer:[B

    iget v1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    add-int/lit8 v2, v1, 0x1

    iput v2, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    const/16 v2, 0x22

    aput-byte v2, v0, v1

    .line 738
    iget-object v0, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_ioContext:Lcom/fasterxml/jackson/core/io/IOContext;

    invoke-virtual {v0}, Lcom/fasterxml/jackson/core/io/IOContext;->allocBase64Buffer()[B

    move-result-object v0

    if-gez p3, :cond_1

    .line 742
    :try_start_0
    invoke-virtual {p0, p1, p2, v0}, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_writeBinary(Lcom/fasterxml/jackson/core/Base64Variant;Ljava/io/InputStream;[B)I

    move-result p3

    goto :goto_0

    :catchall_0
    move-exception p1

    goto :goto_1

    .line 744
    :cond_1
    invoke-virtual {p0, p1, p2, v0, p3}, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_writeBinary(Lcom/fasterxml/jackson/core/Base64Variant;Ljava/io/InputStream;[BI)I

    move-result p1

    if-lez p1, :cond_2

    .line 746
    new-instance p2, Ljava/lang/StringBuilder;

    invoke-direct {p2}, Ljava/lang/StringBuilder;-><init>()V

    const-string v1, "Too few bytes available: missing "

    invoke-virtual {p2, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p2, p1}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    const-string p1, " bytes (out of "

    invoke-virtual {p2, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p2, p3}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    const-string p1, ")"

    invoke-virtual {p2, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p2}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p1

    invoke-virtual {p0, p1}, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_reportError(Ljava/lang/String;)V
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    .line 751
    :cond_2
    :goto_0
    iget-object p1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_ioContext:Lcom/fasterxml/jackson/core/io/IOContext;

    invoke-virtual {p1, v0}, Lcom/fasterxml/jackson/core/io/IOContext;->releaseBase64Buffer([B)V

    .line 754
    iget p1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    iget p2, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputEnd:I

    if-lt p1, p2, :cond_3

    .line 755
    invoke-virtual {p0}, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_flushBuffer()V

    .line 757
    :cond_3
    iget-object p1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputBuffer:[B

    iget p2, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    add-int/lit8 v0, p2, 0x1

    iput v0, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    aput-byte v2, p1, p2

    return p3

    .line 751
    :goto_1
    iget-object p2, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_ioContext:Lcom/fasterxml/jackson/core/io/IOContext;

    invoke-virtual {p2, v0}, Lcom/fasterxml/jackson/core/io/IOContext;->releaseBase64Buffer([B)V

    throw p1
.end method

.method public writeBinary(Lcom/fasterxml/jackson/core/Base64Variant;[BII)V
    .locals 3
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;,
            Lcom/fasterxml/jackson/core/JsonGenerationException;
        }
    .end annotation

    const-string v0, "write binary value"

    .line 713
    invoke-virtual {p0, v0}, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_verifyValueWrite(Ljava/lang/String;)V

    .line 715
    iget v0, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    iget v1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputEnd:I

    if-lt v0, v1, :cond_0

    .line 716
    invoke-virtual {p0}, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_flushBuffer()V

    .line 718
    :cond_0
    iget-object v0, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputBuffer:[B

    iget v1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    add-int/lit8 v2, v1, 0x1

    iput v2, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    const/16 v2, 0x22

    aput-byte v2, v0, v1

    add-int/2addr p4, p3

    .line 719
    invoke-virtual {p0, p1, p2, p3, p4}, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_writeBinary(Lcom/fasterxml/jackson/core/Base64Variant;[BII)V

    .line 721
    iget p1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    iget p2, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputEnd:I

    if-lt p1, p2, :cond_1

    .line 722
    invoke-virtual {p0}, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_flushBuffer()V

    .line 724
    :cond_1
    iget-object p1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputBuffer:[B

    iget p2, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    add-int/lit8 p3, p2, 0x1

    iput p3, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    aput-byte v2, p1, p2

    return-void
.end method

.method public writeBoolean(Z)V
    .locals 4
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;,
            Lcom/fasterxml/jackson/core/JsonGenerationException;
        }
    .end annotation

    const-string v0, "write boolean value"

    .line 937
    invoke-virtual {p0, v0}, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_verifyValueWrite(Ljava/lang/String;)V

    .line 938
    iget v0, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    add-int/lit8 v0, v0, 0x5

    iget v1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputEnd:I

    if-lt v0, v1, :cond_0

    .line 939
    invoke-virtual {p0}, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_flushBuffer()V

    :cond_0
    if-eqz p1, :cond_1

    .line 941
    sget-object p1, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->TRUE_BYTES:[B

    goto :goto_0

    :cond_1
    sget-object p1, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->FALSE_BYTES:[B

    .line 942
    :goto_0
    array-length v0, p1

    const/4 v1, 0x0

    .line 943
    iget-object v2, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputBuffer:[B

    iget v3, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    invoke-static {p1, v1, v2, v3, v0}, Ljava/lang/System;->arraycopy(Ljava/lang/Object;ILjava/lang/Object;II)V

    .line 944
    iget p1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    add-int/2addr p1, v0

    iput p1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    return-void
.end method

.method public final writeEndArray()V
    .locals 3
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    .line 292
    iget-object v0, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_writeContext:Lcom/fasterxml/jackson/core/json/JsonWriteContext;

    invoke-virtual {v0}, Lcom/fasterxml/jackson/core/json/JsonWriteContext;->inArray()Z

    move-result v0

    if-nez v0, :cond_0

    .line 293
    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    const-string v1, "Current context not an ARRAY but "

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_writeContext:Lcom/fasterxml/jackson/core/json/JsonWriteContext;

    invoke-virtual {v1}, Lcom/fasterxml/jackson/core/json/JsonWriteContext;->getTypeDesc()Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v0

    invoke-virtual {p0, v0}, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_reportError(Ljava/lang/String;)V

    .line 295
    :cond_0
    iget-object v0, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_cfgPrettyPrinter:Lcom/fasterxml/jackson/core/PrettyPrinter;

    if-eqz v0, :cond_1

    .line 296
    iget-object v0, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_cfgPrettyPrinter:Lcom/fasterxml/jackson/core/PrettyPrinter;

    iget-object v1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_writeContext:Lcom/fasterxml/jackson/core/json/JsonWriteContext;

    invoke-virtual {v1}, Lcom/fasterxml/jackson/core/json/JsonWriteContext;->getEntryCount()I

    move-result v1

    invoke-interface {v0, p0, v1}, Lcom/fasterxml/jackson/core/PrettyPrinter;->writeEndArray(Lcom/fasterxml/jackson/core/JsonGenerator;I)V

    goto :goto_0

    .line 298
    :cond_1
    iget v0, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    iget v1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputEnd:I

    if-lt v0, v1, :cond_2

    .line 299
    invoke-virtual {p0}, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_flushBuffer()V

    .line 301
    :cond_2
    iget-object v0, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputBuffer:[B

    iget v1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    add-int/lit8 v2, v1, 0x1

    iput v2, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    const/16 v2, 0x5d

    aput-byte v2, v0, v1

    .line 303
    :goto_0
    iget-object v0, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_writeContext:Lcom/fasterxml/jackson/core/json/JsonWriteContext;

    invoke-virtual {v0}, Lcom/fasterxml/jackson/core/json/JsonWriteContext;->getParent()Lcom/fasterxml/jackson/core/json/JsonWriteContext;

    move-result-object v0

    iput-object v0, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_writeContext:Lcom/fasterxml/jackson/core/json/JsonWriteContext;

    return-void
.end method

.method public final writeEndObject()V
    .locals 3
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    .line 324
    iget-object v0, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_writeContext:Lcom/fasterxml/jackson/core/json/JsonWriteContext;

    invoke-virtual {v0}, Lcom/fasterxml/jackson/core/json/JsonWriteContext;->inObject()Z

    move-result v0

    if-nez v0, :cond_0

    .line 325
    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    const-string v1, "Current context not an object but "

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_writeContext:Lcom/fasterxml/jackson/core/json/JsonWriteContext;

    invoke-virtual {v1}, Lcom/fasterxml/jackson/core/json/JsonWriteContext;->getTypeDesc()Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v0

    invoke-virtual {p0, v0}, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_reportError(Ljava/lang/String;)V

    .line 327
    :cond_0
    iget-object v0, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_cfgPrettyPrinter:Lcom/fasterxml/jackson/core/PrettyPrinter;

    if-eqz v0, :cond_1

    .line 328
    iget-object v0, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_cfgPrettyPrinter:Lcom/fasterxml/jackson/core/PrettyPrinter;

    iget-object v1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_writeContext:Lcom/fasterxml/jackson/core/json/JsonWriteContext;

    invoke-virtual {v1}, Lcom/fasterxml/jackson/core/json/JsonWriteContext;->getEntryCount()I

    move-result v1

    invoke-interface {v0, p0, v1}, Lcom/fasterxml/jackson/core/PrettyPrinter;->writeEndObject(Lcom/fasterxml/jackson/core/JsonGenerator;I)V

    goto :goto_0

    .line 330
    :cond_1
    iget v0, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    iget v1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputEnd:I

    if-lt v0, v1, :cond_2

    .line 331
    invoke-virtual {p0}, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_flushBuffer()V

    .line 333
    :cond_2
    iget-object v0, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputBuffer:[B

    iget v1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    add-int/lit8 v2, v1, 0x1

    iput v2, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    const/16 v2, 0x7d

    aput-byte v2, v0, v1

    .line 335
    :goto_0
    iget-object v0, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_writeContext:Lcom/fasterxml/jackson/core/json/JsonWriteContext;

    invoke-virtual {v0}, Lcom/fasterxml/jackson/core/json/JsonWriteContext;->getParent()Lcom/fasterxml/jackson/core/json/JsonWriteContext;

    move-result-object v0

    iput-object v0, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_writeContext:Lcom/fasterxml/jackson/core/json/JsonWriteContext;

    return-void
.end method

.method public writeFieldName(Lcom/fasterxml/jackson/core/SerializableString;)V
    .locals 3
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    .line 225
    iget-object v0, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_cfgPrettyPrinter:Lcom/fasterxml/jackson/core/PrettyPrinter;

    if-eqz v0, :cond_0

    .line 226
    invoke-virtual {p0, p1}, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_writePPFieldName(Lcom/fasterxml/jackson/core/SerializableString;)V

    return-void

    .line 229
    :cond_0
    iget-object v0, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_writeContext:Lcom/fasterxml/jackson/core/json/JsonWriteContext;

    invoke-interface {p1}, Lcom/fasterxml/jackson/core/SerializableString;->getValue()Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Lcom/fasterxml/jackson/core/json/JsonWriteContext;->writeFieldName(Ljava/lang/String;)I

    move-result v0

    const/4 v1, 0x4

    if-ne v0, v1, :cond_1

    const-string v1, "Can not write a field name, expecting a value"

    .line 231
    invoke-virtual {p0, v1}, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_reportError(Ljava/lang/String;)V

    :cond_1
    const/4 v1, 0x1

    if-ne v0, v1, :cond_3

    .line 234
    iget v0, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    iget v1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputEnd:I

    if-lt v0, v1, :cond_2

    .line 235
    invoke-virtual {p0}, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_flushBuffer()V

    .line 237
    :cond_2
    iget-object v0, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputBuffer:[B

    iget v1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    add-int/lit8 v2, v1, 0x1

    iput v2, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    const/16 v2, 0x2c

    aput-byte v2, v0, v1

    .line 239
    :cond_3
    iget-boolean v0, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_cfgUnqNames:Z

    if-eqz v0, :cond_4

    .line 240
    invoke-direct {p0, p1}, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_writeUnq(Lcom/fasterxml/jackson/core/SerializableString;)V

    return-void

    .line 243
    :cond_4
    iget v0, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    iget v1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputEnd:I

    if-lt v0, v1, :cond_5

    .line 244
    invoke-virtual {p0}, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_flushBuffer()V

    .line 246
    :cond_5
    iget-object v0, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputBuffer:[B

    iget v1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    add-int/lit8 v2, v1, 0x1

    iput v2, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    const/16 v2, 0x22

    aput-byte v2, v0, v1

    .line 247
    iget-object v0, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputBuffer:[B

    iget v1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    invoke-interface {p1, v0, v1}, Lcom/fasterxml/jackson/core/SerializableString;->appendQuotedUTF8([BI)I

    move-result v0

    if-gez v0, :cond_6

    .line 249
    invoke-interface {p1}, Lcom/fasterxml/jackson/core/SerializableString;->asQuotedUTF8()[B

    move-result-object p1

    invoke-direct {p0, p1}, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_writeBytes([B)V

    goto :goto_0

    .line 251
    :cond_6
    iget p1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    add-int/2addr p1, v0

    iput p1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    .line 253
    :goto_0
    iget p1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    iget v0, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputEnd:I

    if-lt p1, v0, :cond_7

    .line 254
    invoke-virtual {p0}, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_flushBuffer()V

    .line 256
    :cond_7
    iget-object p1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputBuffer:[B

    iget v0, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    add-int/lit8 v1, v0, 0x1

    iput v1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    aput-byte v2, p1, v0

    return-void
.end method

.method public writeFieldName(Ljava/lang/String;)V
    .locals 5
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    .line 174
    iget-object v0, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_cfgPrettyPrinter:Lcom/fasterxml/jackson/core/PrettyPrinter;

    if-eqz v0, :cond_0

    .line 175
    invoke-virtual {p0, p1}, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_writePPFieldName(Ljava/lang/String;)V

    return-void

    .line 178
    :cond_0
    iget-object v0, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_writeContext:Lcom/fasterxml/jackson/core/json/JsonWriteContext;

    invoke-virtual {v0, p1}, Lcom/fasterxml/jackson/core/json/JsonWriteContext;->writeFieldName(Ljava/lang/String;)I

    move-result v0

    const/4 v1, 0x4

    if-ne v0, v1, :cond_1

    const-string v1, "Can not write a field name, expecting a value"

    .line 180
    invoke-virtual {p0, v1}, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_reportError(Ljava/lang/String;)V

    :cond_1
    const/4 v1, 0x1

    if-ne v0, v1, :cond_3

    .line 183
    iget v0, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    iget v2, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputEnd:I

    if-lt v0, v2, :cond_2

    .line 184
    invoke-virtual {p0}, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_flushBuffer()V

    .line 186
    :cond_2
    iget-object v0, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputBuffer:[B

    iget v2, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    add-int/lit8 v3, v2, 0x1

    iput v3, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    const/16 v3, 0x2c

    aput-byte v3, v0, v2

    .line 191
    :cond_3
    iget-boolean v0, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_cfgUnqNames:Z

    const/4 v2, 0x0

    if-eqz v0, :cond_4

    .line 192
    invoke-direct {p0, p1, v2}, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_writeStringSegments(Ljava/lang/String;Z)V

    return-void

    .line 195
    :cond_4
    invoke-virtual {p1}, Ljava/lang/String;->length()I

    move-result v0

    .line 197
    iget v3, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_charBufferLength:I

    if-le v0, v3, :cond_5

    .line 198
    invoke-direct {p0, p1, v1}, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_writeStringSegments(Ljava/lang/String;Z)V

    return-void

    .line 201
    :cond_5
    iget v1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    iget v3, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputEnd:I

    if-lt v1, v3, :cond_6

    .line 202
    invoke-virtual {p0}, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_flushBuffer()V

    .line 204
    :cond_6
    iget-object v1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputBuffer:[B

    iget v3, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    add-int/lit8 v4, v3, 0x1

    iput v4, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    const/16 v4, 0x22

    aput-byte v4, v1, v3

    .line 205
    iget-object v1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_charBuffer:[C

    invoke-virtual {p1, v2, v0, v1, v2}, Ljava/lang/String;->getChars(II[CI)V

    .line 207
    iget p1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputMaxContiguous:I

    if-gt v0, p1, :cond_8

    .line 208
    iget p1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    add-int/2addr p1, v0

    iget v1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputEnd:I

    if-le p1, v1, :cond_7

    .line 209
    invoke-virtual {p0}, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_flushBuffer()V

    .line 211
    :cond_7
    iget-object p1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_charBuffer:[C

    invoke-direct {p0, p1, v2, v0}, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_writeStringSegment([CII)V

    goto :goto_0

    .line 213
    :cond_8
    iget-object p1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_charBuffer:[C

    invoke-direct {p0, p1, v2, v0}, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_writeStringSegments([CII)V

    .line 216
    :goto_0
    iget p1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    iget v0, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputEnd:I

    if-lt p1, v0, :cond_9

    .line 217
    invoke-virtual {p0}, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_flushBuffer()V

    .line 219
    :cond_9
    iget-object p1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputBuffer:[B

    iget v0, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    add-int/lit8 v1, v0, 0x1

    iput v1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    aput-byte v4, p1, v0

    return-void
.end method

.method public writeNull()V
    .locals 1
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;,
            Lcom/fasterxml/jackson/core/JsonGenerationException;
        }
    .end annotation

    const-string v0, "write null value"

    .line 951
    invoke-virtual {p0, v0}, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_verifyValueWrite(Ljava/lang/String;)V

    .line 952
    invoke-direct {p0}, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_writeNull()V

    return-void
.end method

.method public writeNumber(D)V
    .locals 1
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;,
            Lcom/fasterxml/jackson/core/JsonGenerationException;
        }
    .end annotation

    .line 863
    iget-boolean v0, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_cfgNumbersAsStrings:Z

    if-nez v0, :cond_2

    invoke-static {p1, p2}, Ljava/lang/Double;->isNaN(D)Z

    move-result v0

    if-nez v0, :cond_0

    invoke-static {p1, p2}, Ljava/lang/Double;->isInfinite(D)Z

    move-result v0

    if-eqz v0, :cond_1

    :cond_0
    sget-object v0, Lcom/fasterxml/jackson/core/JsonGenerator$Feature;->QUOTE_NON_NUMERIC_NUMBERS:Lcom/fasterxml/jackson/core/JsonGenerator$Feature;

    invoke-virtual {p0, v0}, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->isEnabled(Lcom/fasterxml/jackson/core/JsonGenerator$Feature;)Z

    move-result v0

    if-eqz v0, :cond_1

    goto :goto_0

    :cond_1
    const-string v0, "write number"

    .line 871
    invoke-virtual {p0, v0}, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_verifyValueWrite(Ljava/lang/String;)V

    .line 872
    invoke-static {p1, p2}, Ljava/lang/String;->valueOf(D)Ljava/lang/String;

    move-result-object p1

    invoke-virtual {p0, p1}, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->writeRaw(Ljava/lang/String;)V

    return-void

    .line 867
    :cond_2
    :goto_0
    invoke-static {p1, p2}, Ljava/lang/String;->valueOf(D)Ljava/lang/String;

    move-result-object p1

    invoke-virtual {p0, p1}, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->writeString(Ljava/lang/String;)V

    return-void
.end method

.method public writeNumber(F)V
    .locals 1
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;,
            Lcom/fasterxml/jackson/core/JsonGenerationException;
        }
    .end annotation

    .line 879
    iget-boolean v0, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_cfgNumbersAsStrings:Z

    if-nez v0, :cond_2

    invoke-static {p1}, Ljava/lang/Float;->isNaN(F)Z

    move-result v0

    if-nez v0, :cond_0

    invoke-static {p1}, Ljava/lang/Float;->isInfinite(F)Z

    move-result v0

    if-eqz v0, :cond_1

    :cond_0
    sget-object v0, Lcom/fasterxml/jackson/core/JsonGenerator$Feature;->QUOTE_NON_NUMERIC_NUMBERS:Lcom/fasterxml/jackson/core/JsonGenerator$Feature;

    invoke-virtual {p0, v0}, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->isEnabled(Lcom/fasterxml/jackson/core/JsonGenerator$Feature;)Z

    move-result v0

    if-eqz v0, :cond_1

    goto :goto_0

    :cond_1
    const-string v0, "write number"

    .line 887
    invoke-virtual {p0, v0}, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_verifyValueWrite(Ljava/lang/String;)V

    .line 888
    invoke-static {p1}, Ljava/lang/String;->valueOf(F)Ljava/lang/String;

    move-result-object p1

    invoke-virtual {p0, p1}, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->writeRaw(Ljava/lang/String;)V

    return-void

    .line 883
    :cond_2
    :goto_0
    invoke-static {p1}, Ljava/lang/String;->valueOf(F)Ljava/lang/String;

    move-result-object p1

    invoke-virtual {p0, p1}, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->writeString(Ljava/lang/String;)V

    return-void
.end method

.method public writeNumber(I)V
    .locals 2
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;,
            Lcom/fasterxml/jackson/core/JsonGenerationException;
        }
    .end annotation

    const-string v0, "write number"

    .line 796
    invoke-virtual {p0, v0}, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_verifyValueWrite(Ljava/lang/String;)V

    .line 798
    iget v0, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    add-int/lit8 v0, v0, 0xb

    iget v1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputEnd:I

    if-lt v0, v1, :cond_0

    .line 799
    invoke-virtual {p0}, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_flushBuffer()V

    .line 801
    :cond_0
    iget-boolean v0, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_cfgNumbersAsStrings:Z

    if-eqz v0, :cond_1

    .line 802
    invoke-direct {p0, p1}, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_writeQuotedInt(I)V

    return-void

    .line 805
    :cond_1
    iget-object v0, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputBuffer:[B

    iget v1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    invoke-static {p1, v0, v1}, Lcom/fasterxml/jackson/core/io/NumberOutput;->outputInt(I[BI)I

    move-result p1

    iput p1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    return-void
.end method

.method public writeNumber(J)V
    .locals 2
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;,
            Lcom/fasterxml/jackson/core/JsonGenerationException;
        }
    .end annotation

    const-string v0, "write number"

    .line 822
    invoke-virtual {p0, v0}, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_verifyValueWrite(Ljava/lang/String;)V

    .line 823
    iget-boolean v0, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_cfgNumbersAsStrings:Z

    if-eqz v0, :cond_0

    .line 824
    invoke-direct {p0, p1, p2}, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_writeQuotedLong(J)V

    return-void

    .line 827
    :cond_0
    iget v0, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    add-int/lit8 v0, v0, 0x15

    iget v1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputEnd:I

    if-lt v0, v1, :cond_1

    .line 829
    invoke-virtual {p0}, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_flushBuffer()V

    .line 831
    :cond_1
    iget-object v0, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputBuffer:[B

    iget v1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    invoke-static {p1, p2, v0, v1}, Lcom/fasterxml/jackson/core/io/NumberOutput;->outputLong(J[BI)I

    move-result p1

    iput p1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    return-void
.end method

.method public writeNumber(Ljava/lang/String;)V
    .locals 1
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;,
            Lcom/fasterxml/jackson/core/JsonGenerationException;
        }
    .end annotation

    const-string v0, "write number"

    .line 912
    invoke-virtual {p0, v0}, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_verifyValueWrite(Ljava/lang/String;)V

    .line 913
    iget-boolean v0, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_cfgNumbersAsStrings:Z

    if-eqz v0, :cond_0

    .line 914
    invoke-direct {p0, p1}, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_writeQuotedRaw(Ljava/lang/Object;)V

    goto :goto_0

    .line 916
    :cond_0
    invoke-virtual {p0, p1}, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->writeRaw(Ljava/lang/String;)V

    :goto_0
    return-void
.end method

.method public writeNumber(Ljava/math/BigDecimal;)V
    .locals 1
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;,
            Lcom/fasterxml/jackson/core/JsonGenerationException;
        }
    .end annotation

    const-string v0, "write number"

    .line 896
    invoke-virtual {p0, v0}, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_verifyValueWrite(Ljava/lang/String;)V

    if-nez p1, :cond_0

    .line 898
    invoke-direct {p0}, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_writeNull()V

    goto :goto_0

    .line 899
    :cond_0
    iget-boolean v0, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_cfgNumbersAsStrings:Z

    if-eqz v0, :cond_1

    .line 900
    invoke-direct {p0, p1}, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_writeQuotedRaw(Ljava/lang/Object;)V

    goto :goto_0

    .line 901
    :cond_1
    sget-object v0, Lcom/fasterxml/jackson/core/JsonGenerator$Feature;->WRITE_BIGDECIMAL_AS_PLAIN:Lcom/fasterxml/jackson/core/JsonGenerator$Feature;

    invoke-virtual {p0, v0}, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->isEnabled(Lcom/fasterxml/jackson/core/JsonGenerator$Feature;)Z

    move-result v0

    if-eqz v0, :cond_2

    .line 902
    invoke-virtual {p1}, Ljava/math/BigDecimal;->toPlainString()Ljava/lang/String;

    move-result-object p1

    invoke-virtual {p0, p1}, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->writeRaw(Ljava/lang/String;)V

    goto :goto_0

    .line 904
    :cond_2
    invoke-virtual {p1}, Ljava/math/BigDecimal;->toString()Ljava/lang/String;

    move-result-object p1

    invoke-virtual {p0, p1}, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->writeRaw(Ljava/lang/String;)V

    :goto_0
    return-void
.end method

.method public writeNumber(Ljava/math/BigInteger;)V
    .locals 1
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;,
            Lcom/fasterxml/jackson/core/JsonGenerationException;
        }
    .end annotation

    const-string v0, "write number"

    .line 848
    invoke-virtual {p0, v0}, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_verifyValueWrite(Ljava/lang/String;)V

    if-nez p1, :cond_0

    .line 850
    invoke-direct {p0}, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_writeNull()V

    goto :goto_0

    .line 851
    :cond_0
    iget-boolean v0, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_cfgNumbersAsStrings:Z

    if-eqz v0, :cond_1

    .line 852
    invoke-direct {p0, p1}, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_writeQuotedRaw(Ljava/lang/Object;)V

    goto :goto_0

    .line 854
    :cond_1
    invoke-virtual {p1}, Ljava/math/BigInteger;->toString()Ljava/lang/String;

    move-result-object p1

    invoke-virtual {p0, p1}, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->writeRaw(Ljava/lang/String;)V

    :goto_0
    return-void
.end method

.method public writeNumber(S)V
    .locals 2
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;,
            Lcom/fasterxml/jackson/core/JsonGenerationException;
        }
    .end annotation

    const-string v0, "write number"

    .line 771
    invoke-virtual {p0, v0}, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_verifyValueWrite(Ljava/lang/String;)V

    .line 773
    iget v0, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    add-int/lit8 v0, v0, 0x6

    iget v1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputEnd:I

    if-lt v0, v1, :cond_0

    .line 774
    invoke-virtual {p0}, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_flushBuffer()V

    .line 776
    :cond_0
    iget-boolean v0, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_cfgNumbersAsStrings:Z

    if-eqz v0, :cond_1

    .line 777
    invoke-direct {p0, p1}, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_writeQuotedShort(S)V

    return-void

    .line 780
    :cond_1
    iget-object v0, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputBuffer:[B

    iget v1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    invoke-static {p1, v0, v1}, Lcom/fasterxml/jackson/core/io/NumberOutput;->outputInt(I[BI)I

    move-result p1

    iput p1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    return-void
.end method

.method public writeRaw(C)V
    .locals 3
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;,
            Lcom/fasterxml/jackson/core/JsonGenerationException;
        }
    .end annotation

    .line 648
    iget v0, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    add-int/lit8 v0, v0, 0x3

    iget v1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputEnd:I

    if-lt v0, v1, :cond_0

    .line 649
    invoke-virtual {p0}, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_flushBuffer()V

    .line 651
    :cond_0
    iget-object v0, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputBuffer:[B

    const/16 v1, 0x7f

    if-gt p1, v1, :cond_1

    .line 653
    iget v1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    add-int/lit8 v2, v1, 0x1

    iput v2, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    int-to-byte p1, p1

    aput-byte p1, v0, v1

    goto :goto_0

    :cond_1
    const/16 v1, 0x800

    if-ge p1, v1, :cond_2

    .line 655
    iget v1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    add-int/lit8 v2, v1, 0x1

    iput v2, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    shr-int/lit8 v2, p1, 0x6

    or-int/lit16 v2, v2, 0xc0

    int-to-byte v2, v2

    aput-byte v2, v0, v1

    .line 656
    iget v1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    add-int/lit8 v2, v1, 0x1

    iput v2, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    and-int/lit8 p1, p1, 0x3f

    or-int/lit16 p1, p1, 0x80

    int-to-byte p1, p1

    aput-byte p1, v0, v1

    goto :goto_0

    :cond_2
    const/4 v0, 0x0

    const/4 v1, 0x0

    .line 658
    invoke-direct {p0, p1, v0, v1, v1}, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputRawMultiByteChar(I[CII)I

    :goto_0
    return-void
.end method

.method public writeRaw(Lcom/fasterxml/jackson/core/SerializableString;)V
    .locals 1
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;,
            Lcom/fasterxml/jackson/core/JsonGenerationException;
        }
    .end annotation

    .line 584
    invoke-interface {p1}, Lcom/fasterxml/jackson/core/SerializableString;->asUnquotedUTF8()[B

    move-result-object p1

    .line 585
    array-length v0, p1

    if-lez v0, :cond_0

    .line 586
    invoke-direct {p0, p1}, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_writeBytes([B)V

    :cond_0
    return-void
.end method

.method public writeRaw(Ljava/lang/String;)V
    .locals 6
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;,
            Lcom/fasterxml/jackson/core/JsonGenerationException;
        }
    .end annotation

    .line 554
    invoke-virtual {p1}, Ljava/lang/String;->length()I

    move-result v0

    const/4 v1, 0x0

    const/4 v2, 0x0

    :goto_0
    if-lez v0, :cond_1

    .line 556
    iget-object v3, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_charBuffer:[C

    .line 557
    array-length v4, v3

    if-ge v0, v4, :cond_0

    move v4, v0

    :cond_0
    add-int v5, v2, v4

    .line 559
    invoke-virtual {p1, v2, v5, v3, v1}, Ljava/lang/String;->getChars(II[CI)V

    .line 560
    invoke-virtual {p0, v3, v1, v4}, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->writeRaw([CII)V

    sub-int/2addr v0, v4

    move v2, v5

    goto :goto_0

    :cond_1
    return-void
.end method

.method public writeRaw(Ljava/lang/String;II)V
    .locals 4
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;,
            Lcom/fasterxml/jackson/core/JsonGenerationException;
        }
    .end annotation

    :goto_0
    if-lez p3, :cond_1

    .line 571
    iget-object v0, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_charBuffer:[C

    .line 572
    array-length v1, v0

    if-ge p3, v1, :cond_0

    move v1, p3

    :cond_0
    add-int v2, p2, v1

    const/4 v3, 0x0

    .line 574
    invoke-virtual {p1, p2, v2, v0, v3}, Ljava/lang/String;->getChars(II[CI)V

    .line 575
    invoke-virtual {p0, v0, v3, v1}, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->writeRaw([CII)V

    sub-int/2addr p3, v1

    move p2, v2

    goto :goto_0

    :cond_1
    return-void
.end method

.method public final writeRaw([CII)V
    .locals 4
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;,
            Lcom/fasterxml/jackson/core/JsonGenerationException;
        }
    .end annotation

    add-int v0, p3, p3

    add-int/2addr v0, p3

    .line 608
    iget v1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    add-int/2addr v1, v0

    iget v2, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputEnd:I

    if-le v1, v2, :cond_1

    .line 610
    iget v1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputEnd:I

    if-ge v1, v0, :cond_0

    .line 611
    invoke-direct {p0, p1, p2, p3}, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_writeSegmentedRaw([CII)V

    return-void

    .line 615
    :cond_0
    invoke-virtual {p0}, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_flushBuffer()V

    :cond_1
    add-int/2addr p3, p2

    :goto_0
    if-ge p2, p3, :cond_5

    .line 625
    :cond_2
    aget-char v0, p1, p2

    const/16 v1, 0x7f

    if-le v0, v1, :cond_4

    add-int/lit8 v0, p2, 0x1

    .line 634
    aget-char p2, p1, p2

    const/16 v1, 0x800

    if-ge p2, v1, :cond_3

    .line 636
    iget-object v1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputBuffer:[B

    iget v2, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    add-int/lit8 v3, v2, 0x1

    iput v3, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    shr-int/lit8 v3, p2, 0x6

    or-int/lit16 v3, v3, 0xc0

    int-to-byte v3, v3

    aput-byte v3, v1, v2

    .line 637
    iget-object v1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputBuffer:[B

    iget v2, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    add-int/lit8 v3, v2, 0x1

    iput v3, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    and-int/lit8 p2, p2, 0x3f

    or-int/lit16 p2, p2, 0x80

    int-to-byte p2, p2

    aput-byte p2, v1, v2

    move p2, v0

    goto :goto_0

    .line 639
    :cond_3
    invoke-direct {p0, p2, p1, v0, p3}, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputRawMultiByteChar(I[CII)I

    move-result p2

    goto :goto_0

    .line 629
    :cond_4
    iget-object v1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputBuffer:[B

    iget v2, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    add-int/lit8 v3, v2, 0x1

    iput v3, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    int-to-byte v0, v0

    aput-byte v0, v1, v2

    add-int/lit8 p2, p2, 0x1

    if-lt p2, p3, :cond_2

    :cond_5
    return-void
.end method

.method public writeRawUTF8String([BII)V
    .locals 3
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    const-string v0, "write text value"

    .line 511
    invoke-virtual {p0, v0}, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_verifyValueWrite(Ljava/lang/String;)V

    .line 512
    iget v0, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    iget v1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputEnd:I

    if-lt v0, v1, :cond_0

    .line 513
    invoke-virtual {p0}, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_flushBuffer()V

    .line 515
    :cond_0
    iget-object v0, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputBuffer:[B

    iget v1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    add-int/lit8 v2, v1, 0x1

    iput v2, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    const/16 v2, 0x22

    aput-byte v2, v0, v1

    .line 516
    invoke-direct {p0, p1, p2, p3}, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_writeBytes([BII)V

    .line 517
    iget p1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    iget p2, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputEnd:I

    if-lt p1, p2, :cond_1

    .line 518
    invoke-virtual {p0}, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_flushBuffer()V

    .line 520
    :cond_1
    iget-object p1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputBuffer:[B

    iget p2, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    add-int/lit8 p3, p2, 0x1

    iput p3, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    aput-byte v2, p1, p2

    return-void
.end method

.method public writeRawValue(Lcom/fasterxml/jackson/core/SerializableString;)V
    .locals 1
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    const-string v0, "write raw value"

    .line 593
    invoke-virtual {p0, v0}, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_verifyValueWrite(Ljava/lang/String;)V

    .line 594
    invoke-interface {p1}, Lcom/fasterxml/jackson/core/SerializableString;->asUnquotedUTF8()[B

    move-result-object p1

    .line 595
    array-length v0, p1

    if-lez v0, :cond_0

    .line 596
    invoke-direct {p0, p1}, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_writeBytes([B)V

    :cond_0
    return-void
.end method

.method public final writeStartArray()V
    .locals 3
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    const-string v0, "start an array"

    .line 277
    invoke-virtual {p0, v0}, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_verifyValueWrite(Ljava/lang/String;)V

    .line 278
    iget-object v0, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_writeContext:Lcom/fasterxml/jackson/core/json/JsonWriteContext;

    invoke-virtual {v0}, Lcom/fasterxml/jackson/core/json/JsonWriteContext;->createChildArrayContext()Lcom/fasterxml/jackson/core/json/JsonWriteContext;

    move-result-object v0

    iput-object v0, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_writeContext:Lcom/fasterxml/jackson/core/json/JsonWriteContext;

    .line 279
    iget-object v0, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_cfgPrettyPrinter:Lcom/fasterxml/jackson/core/PrettyPrinter;

    if-eqz v0, :cond_0

    .line 280
    iget-object v0, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_cfgPrettyPrinter:Lcom/fasterxml/jackson/core/PrettyPrinter;

    invoke-interface {v0, p0}, Lcom/fasterxml/jackson/core/PrettyPrinter;->writeStartArray(Lcom/fasterxml/jackson/core/JsonGenerator;)V

    goto :goto_0

    .line 282
    :cond_0
    iget v0, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    iget v1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputEnd:I

    if-lt v0, v1, :cond_1

    .line 283
    invoke-virtual {p0}, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_flushBuffer()V

    .line 285
    :cond_1
    iget-object v0, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputBuffer:[B

    iget v1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    add-int/lit8 v2, v1, 0x1

    iput v2, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    const/16 v2, 0x5b

    aput-byte v2, v0, v1

    :goto_0
    return-void
.end method

.method public final writeStartObject()V
    .locals 3
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    const-string v0, "start an object"

    .line 309
    invoke-virtual {p0, v0}, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_verifyValueWrite(Ljava/lang/String;)V

    .line 310
    iget-object v0, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_writeContext:Lcom/fasterxml/jackson/core/json/JsonWriteContext;

    invoke-virtual {v0}, Lcom/fasterxml/jackson/core/json/JsonWriteContext;->createChildObjectContext()Lcom/fasterxml/jackson/core/json/JsonWriteContext;

    move-result-object v0

    iput-object v0, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_writeContext:Lcom/fasterxml/jackson/core/json/JsonWriteContext;

    .line 311
    iget-object v0, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_cfgPrettyPrinter:Lcom/fasterxml/jackson/core/PrettyPrinter;

    if-eqz v0, :cond_0

    .line 312
    iget-object v0, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_cfgPrettyPrinter:Lcom/fasterxml/jackson/core/PrettyPrinter;

    invoke-interface {v0, p0}, Lcom/fasterxml/jackson/core/PrettyPrinter;->writeStartObject(Lcom/fasterxml/jackson/core/JsonGenerator;)V

    goto :goto_0

    .line 314
    :cond_0
    iget v0, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    iget v1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputEnd:I

    if-lt v0, v1, :cond_1

    .line 315
    invoke-virtual {p0}, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_flushBuffer()V

    .line 317
    :cond_1
    iget-object v0, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputBuffer:[B

    iget v1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    add-int/lit8 v2, v1, 0x1

    iput v2, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    const/16 v2, 0x7b

    aput-byte v2, v0, v1

    :goto_0
    return-void
.end method

.method public final writeString(Lcom/fasterxml/jackson/core/SerializableString;)V
    .locals 3
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    const-string v0, "write text value"

    .line 491
    invoke-virtual {p0, v0}, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_verifyValueWrite(Ljava/lang/String;)V

    .line 492
    iget v0, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    iget v1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputEnd:I

    if-lt v0, v1, :cond_0

    .line 493
    invoke-virtual {p0}, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_flushBuffer()V

    .line 495
    :cond_0
    iget-object v0, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputBuffer:[B

    iget v1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    add-int/lit8 v2, v1, 0x1

    iput v2, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    const/16 v2, 0x22

    aput-byte v2, v0, v1

    .line 496
    iget-object v0, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputBuffer:[B

    iget v1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    invoke-interface {p1, v0, v1}, Lcom/fasterxml/jackson/core/SerializableString;->appendQuotedUTF8([BI)I

    move-result v0

    if-gez v0, :cond_1

    .line 498
    invoke-interface {p1}, Lcom/fasterxml/jackson/core/SerializableString;->asQuotedUTF8()[B

    move-result-object p1

    invoke-direct {p0, p1}, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_writeBytes([B)V

    goto :goto_0

    .line 500
    :cond_1
    iget p1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    add-int/2addr p1, v0

    iput p1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    .line 502
    :goto_0
    iget p1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    iget v0, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputEnd:I

    if-lt p1, v0, :cond_2

    .line 503
    invoke-virtual {p0}, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_flushBuffer()V

    .line 505
    :cond_2
    iget-object p1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputBuffer:[B

    iget v0, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    add-int/lit8 v1, v0, 0x1

    iput v1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    aput-byte v2, p1, v0

    return-void
.end method

.method public writeString(Ljava/lang/String;)V
    .locals 4
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    const-string v0, "write text value"

    .line 419
    invoke-virtual {p0, v0}, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_verifyValueWrite(Ljava/lang/String;)V

    if-nez p1, :cond_0

    .line 421
    invoke-direct {p0}, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_writeNull()V

    return-void

    .line 425
    :cond_0
    invoke-virtual {p1}, Ljava/lang/String;->length()I

    move-result v0

    .line 426
    iget v1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_charBufferLength:I

    if-le v0, v1, :cond_1

    const/4 v0, 0x1

    .line 427
    invoke-direct {p0, p1, v0}, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_writeStringSegments(Ljava/lang/String;Z)V

    return-void

    .line 431
    :cond_1
    iget-object v1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_charBuffer:[C

    const/4 v2, 0x0

    invoke-virtual {p1, v2, v0, v1, v2}, Ljava/lang/String;->getChars(II[CI)V

    .line 433
    iget p1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputMaxContiguous:I

    if-le v0, p1, :cond_2

    .line 434
    iget-object p1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_charBuffer:[C

    invoke-direct {p0, p1, v2, v0}, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_writeLongString([CII)V

    return-void

    .line 437
    :cond_2
    iget p1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    add-int/2addr p1, v0

    iget v1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputEnd:I

    if-lt p1, v1, :cond_3

    .line 438
    invoke-virtual {p0}, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_flushBuffer()V

    .line 440
    :cond_3
    iget-object p1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputBuffer:[B

    iget v1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    add-int/lit8 v3, v1, 0x1

    iput v3, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    const/16 v3, 0x22

    aput-byte v3, p1, v1

    .line 441
    iget-object p1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_charBuffer:[C

    invoke-direct {p0, p1, v2, v0}, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_writeStringSegment([CII)V

    .line 445
    iget p1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    iget v0, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputEnd:I

    if-lt p1, v0, :cond_4

    .line 446
    invoke-virtual {p0}, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_flushBuffer()V

    .line 448
    :cond_4
    iget-object p1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputBuffer:[B

    iget v0, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    add-int/lit8 v1, v0, 0x1

    iput v1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    aput-byte v3, p1, v0

    return-void
.end method

.method public writeString([CII)V
    .locals 3
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    const-string v0, "write text value"

    .line 467
    invoke-virtual {p0, v0}, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_verifyValueWrite(Ljava/lang/String;)V

    .line 468
    iget v0, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    iget v1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputEnd:I

    if-lt v0, v1, :cond_0

    .line 469
    invoke-virtual {p0}, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_flushBuffer()V

    .line 471
    :cond_0
    iget-object v0, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputBuffer:[B

    iget v1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    add-int/lit8 v2, v1, 0x1

    iput v2, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    const/16 v2, 0x22

    aput-byte v2, v0, v1

    .line 473
    iget v0, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputMaxContiguous:I

    if-gt p3, v0, :cond_2

    .line 474
    iget v0, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    add-int/2addr v0, p3

    iget v1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputEnd:I

    if-le v0, v1, :cond_1

    .line 475
    invoke-virtual {p0}, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_flushBuffer()V

    .line 477
    :cond_1
    invoke-direct {p0, p1, p2, p3}, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_writeStringSegment([CII)V

    goto :goto_0

    .line 479
    :cond_2
    invoke-direct {p0, p1, p2, p3}, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_writeStringSegments([CII)V

    .line 482
    :goto_0
    iget p1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    iget p2, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputEnd:I

    if-lt p1, p2, :cond_3

    .line 483
    invoke-virtual {p0}, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_flushBuffer()V

    .line 485
    :cond_3
    iget-object p1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputBuffer:[B

    iget p2, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    add-int/lit8 p3, p2, 0x1

    iput p3, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    aput-byte v2, p1, p2

    return-void
.end method

.method public writeUTF8String([BII)V
    .locals 3
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    const-string v0, "write text value"

    .line 526
    invoke-virtual {p0, v0}, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_verifyValueWrite(Ljava/lang/String;)V

    .line 527
    iget v0, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    iget v1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputEnd:I

    if-lt v0, v1, :cond_0

    .line 528
    invoke-virtual {p0}, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_flushBuffer()V

    .line 530
    :cond_0
    iget-object v0, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputBuffer:[B

    iget v1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    add-int/lit8 v2, v1, 0x1

    iput v2, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    const/16 v2, 0x22

    aput-byte v2, v0, v1

    .line 532
    iget v0, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputMaxContiguous:I

    if-gt p3, v0, :cond_1

    .line 533
    invoke-direct {p0, p1, p2, p3}, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_writeUTF8Segment([BII)V

    goto :goto_0

    .line 535
    :cond_1
    invoke-direct {p0, p1, p2, p3}, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_writeUTF8Segments([BII)V

    .line 537
    :goto_0
    iget p1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    iget p2, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputEnd:I

    if-lt p1, p2, :cond_2

    .line 538
    invoke-virtual {p0}, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_flushBuffer()V

    .line 540
    :cond_2
    iget-object p1, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputBuffer:[B

    iget p2, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    add-int/lit8 p3, p2, 0x1

    iput p3, p0, Lcom/fasterxml/jackson/core/json/UTF8JsonGenerator;->_outputTail:I

    aput-byte v2, p1, p2

    return-void
.end method
