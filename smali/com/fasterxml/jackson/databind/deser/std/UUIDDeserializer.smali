.class public Lcom/fasterxml/jackson/databind/deser/std/UUIDDeserializer;
.super Lcom/fasterxml/jackson/databind/deser/std/FromStringDeserializer;
.source "UUIDDeserializer.java"


# annotations
.annotation system Ldalvik/annotation/Signature;
    value = {
        "Lcom/fasterxml/jackson/databind/deser/std/FromStringDeserializer<",
        "Ljava/util/UUID;",
        ">;"
    }
.end annotation


# static fields
.field static final HEX_DIGITS:[I

.field private static final serialVersionUID:J = 0x1L


# direct methods
.method static constructor <clinit>()V
    .locals 4

    const/16 v0, 0x7f

    .line 15
    new-array v0, v0, [I

    sput-object v0, Lcom/fasterxml/jackson/databind/deser/std/UUIDDeserializer;->HEX_DIGITS:[I

    .line 17
    sget-object v0, Lcom/fasterxml/jackson/databind/deser/std/UUIDDeserializer;->HEX_DIGITS:[I

    const/4 v1, -0x1

    invoke-static {v0, v1}, Ljava/util/Arrays;->fill([II)V

    const/4 v0, 0x0

    const/4 v1, 0x0

    :goto_0
    const/16 v2, 0xa

    if-ge v1, v2, :cond_0

    .line 18
    sget-object v2, Lcom/fasterxml/jackson/databind/deser/std/UUIDDeserializer;->HEX_DIGITS:[I

    add-int/lit8 v3, v1, 0x30

    aput v1, v2, v3

    add-int/lit8 v1, v1, 0x1

    goto :goto_0

    :cond_0
    :goto_1
    const/4 v1, 0x6

    if-ge v0, v1, :cond_1

    .line 20
    sget-object v1, Lcom/fasterxml/jackson/databind/deser/std/UUIDDeserializer;->HEX_DIGITS:[I

    add-int/lit8 v2, v0, 0x61

    add-int/lit8 v3, v0, 0xa

    aput v3, v1, v2

    .line 21
    sget-object v1, Lcom/fasterxml/jackson/databind/deser/std/UUIDDeserializer;->HEX_DIGITS:[I

    add-int/lit8 v2, v0, 0x41

    aput v3, v1, v2

    add-int/lit8 v0, v0, 0x1

    goto :goto_1

    :cond_1
    return-void
.end method

.method public constructor <init>()V
    .locals 1

    .line 25
    const-class v0, Ljava/util/UUID;

    invoke-direct {p0, v0}, Lcom/fasterxml/jackson/databind/deser/std/FromStringDeserializer;-><init>(Ljava/lang/Class;)V

    return-void
.end method

.method static _badChar(Ljava/lang/String;IC)I
    .locals 2

    .line 104
    new-instance p1, Ljava/lang/NumberFormatException;

    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    const-string v1, "Non-hex character \'"

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0, p2}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    const-string v1, "\', not valid character for a UUID String"

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, "\' (value 0x"

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-static {p2}, Ljava/lang/Integer;->toHexString(I)Ljava/lang/String;

    move-result-object p2

    invoke-virtual {v0, p2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string p2, ") for UUID String \""

    invoke-virtual {v0, p2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0, p0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string p0, "\""

    invoke-virtual {v0, p0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p0

    invoke-direct {p1, p0}, Ljava/lang/NumberFormatException;-><init>(Ljava/lang/String;)V

    throw p1
.end method

.method private _badFormat(Ljava/lang/String;)V
    .locals 1

    .line 75
    new-instance p1, Ljava/lang/NumberFormatException;

    const-string v0, "UUID has to be represented by the standard 36-char representation"

    invoke-direct {p1, v0}, Ljava/lang/NumberFormatException;-><init>(Ljava/lang/String;)V

    throw p1
.end method

.method private _fromBytes([BLcom/fasterxml/jackson/databind/DeserializationContext;)Ljava/util/UUID;
    .locals 4
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    .line 109
    array-length v0, p1

    const/16 v1, 0x10

    if-eq v0, v1, :cond_0

    .line 110
    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    const-string v1, "Can only construct UUIDs from byte[16]; got "

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    array-length v1, p1

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    const-string v1, " bytes"

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v0

    invoke-virtual {p2, v0}, Lcom/fasterxml/jackson/databind/DeserializationContext;->mappingException(Ljava/lang/String;)Lcom/fasterxml/jackson/databind/JsonMappingException;

    .line 112
    :cond_0
    new-instance p2, Ljava/util/UUID;

    const/4 v0, 0x0

    invoke-static {p1, v0}, Lcom/fasterxml/jackson/databind/deser/std/UUIDDeserializer;->_long([BI)J

    move-result-wide v0

    const/16 v2, 0x8

    invoke-static {p1, v2}, Lcom/fasterxml/jackson/databind/deser/std/UUIDDeserializer;->_long([BI)J

    move-result-wide v2

    invoke-direct {p2, v0, v1, v2, v3}, Ljava/util/UUID;-><init>(JJ)V

    return-object p2
.end method

.method private static _int([BI)I
    .locals 2

    .line 124
    aget-byte v0, p0, p1

    shl-int/lit8 v0, v0, 0x18

    add-int/lit8 v1, p1, 0x1

    aget-byte v1, p0, v1

    and-int/lit16 v1, v1, 0xff

    shl-int/lit8 v1, v1, 0x10

    or-int/2addr v0, v1

    add-int/lit8 v1, p1, 0x2

    aget-byte v1, p0, v1

    and-int/lit16 v1, v1, 0xff

    shl-int/lit8 v1, v1, 0x8

    or-int/2addr v0, v1

    add-int/lit8 p1, p1, 0x3

    aget-byte p0, p0, p1

    and-int/lit16 p0, p0, 0xff

    or-int/2addr p0, v0

    return p0
.end method

.method private static _long([BI)J
    .locals 3

    .line 116
    invoke-static {p0, p1}, Lcom/fasterxml/jackson/databind/deser/std/UUIDDeserializer;->_int([BI)I

    move-result v0

    int-to-long v0, v0

    const/16 v2, 0x20

    shl-long/2addr v0, v2

    add-int/lit8 p1, p1, 0x4

    .line 117
    invoke-static {p0, p1}, Lcom/fasterxml/jackson/databind/deser/std/UUIDDeserializer;->_int([BI)I

    move-result p0

    int-to-long p0, p0

    shl-long/2addr p0, v2

    ushr-long/2addr p0, v2

    or-long/2addr p0, v0

    return-wide p0
.end method

.method static byteFromChars(Ljava/lang/String;I)I
    .locals 6

    .line 88
    invoke-virtual {p0, p1}, Ljava/lang/String;->charAt(I)C

    move-result v0

    add-int/lit8 v1, p1, 0x1

    .line 89
    invoke-virtual {p0, v1}, Ljava/lang/String;->charAt(I)C

    move-result v2

    const/16 v3, 0x7f

    if-gt v0, v3, :cond_0

    if-gt v2, v3, :cond_0

    .line 92
    sget-object v4, Lcom/fasterxml/jackson/databind/deser/std/UUIDDeserializer;->HEX_DIGITS:[I

    aget v4, v4, v0

    shl-int/lit8 v4, v4, 0x4

    sget-object v5, Lcom/fasterxml/jackson/databind/deser/std/UUIDDeserializer;->HEX_DIGITS:[I

    aget v5, v5, v2

    or-int/2addr v4, v5

    if-ltz v4, :cond_0

    return v4

    :cond_0
    if-gt v0, v3, :cond_2

    .line 97
    sget-object v3, Lcom/fasterxml/jackson/databind/deser/std/UUIDDeserializer;->HEX_DIGITS:[I

    aget v3, v3, v0

    if-gez v3, :cond_1

    goto :goto_0

    .line 100
    :cond_1
    invoke-static {p0, v1, v2}, Lcom/fasterxml/jackson/databind/deser/std/UUIDDeserializer;->_badChar(Ljava/lang/String;IC)I

    move-result p0

    return p0

    .line 98
    :cond_2
    :goto_0
    invoke-static {p0, p1, v0}, Lcom/fasterxml/jackson/databind/deser/std/UUIDDeserializer;->_badChar(Ljava/lang/String;IC)I

    move-result p0

    return p0
.end method

.method static intFromChars(Ljava/lang/String;I)I
    .locals 2

    .line 79
    invoke-static {p0, p1}, Lcom/fasterxml/jackson/databind/deser/std/UUIDDeserializer;->byteFromChars(Ljava/lang/String;I)I

    move-result v0

    shl-int/lit8 v0, v0, 0x18

    add-int/lit8 v1, p1, 0x2

    invoke-static {p0, v1}, Lcom/fasterxml/jackson/databind/deser/std/UUIDDeserializer;->byteFromChars(Ljava/lang/String;I)I

    move-result v1

    shl-int/lit8 v1, v1, 0x10

    add-int/2addr v0, v1

    add-int/lit8 v1, p1, 0x4

    invoke-static {p0, v1}, Lcom/fasterxml/jackson/databind/deser/std/UUIDDeserializer;->byteFromChars(Ljava/lang/String;I)I

    move-result v1

    shl-int/lit8 v1, v1, 0x8

    add-int/2addr v0, v1

    add-int/lit8 p1, p1, 0x6

    invoke-static {p0, p1}, Lcom/fasterxml/jackson/databind/deser/std/UUIDDeserializer;->byteFromChars(Ljava/lang/String;I)I

    move-result p0

    add-int/2addr v0, p0

    return v0
.end method

.method static shortFromChars(Ljava/lang/String;I)I
    .locals 1

    .line 83
    invoke-static {p0, p1}, Lcom/fasterxml/jackson/databind/deser/std/UUIDDeserializer;->byteFromChars(Ljava/lang/String;I)I

    move-result v0

    shl-int/lit8 v0, v0, 0x8

    add-int/lit8 p1, p1, 0x2

    invoke-static {p0, p1}, Lcom/fasterxml/jackson/databind/deser/std/UUIDDeserializer;->byteFromChars(Ljava/lang/String;I)I

    move-result p0

    add-int/2addr v0, p0

    return v0
.end method


# virtual methods
.method protected bridge synthetic _deserialize(Ljava/lang/String;Lcom/fasterxml/jackson/databind/DeserializationContext;)Ljava/lang/Object;
    .locals 0
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    .line 11
    invoke-virtual {p0, p1, p2}, Lcom/fasterxml/jackson/databind/deser/std/UUIDDeserializer;->_deserialize(Ljava/lang/String;Lcom/fasterxml/jackson/databind/DeserializationContext;)Ljava/util/UUID;

    move-result-object p1

    return-object p1
.end method

.method protected _deserialize(Ljava/lang/String;Lcom/fasterxml/jackson/databind/DeserializationContext;)Ljava/util/UUID;
    .locals 8
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    .line 32
    invoke-virtual {p1}, Ljava/lang/String;->length()I

    move-result v0

    const/16 v1, 0x18

    const/16 v2, 0x24

    if-eq v0, v2, :cond_1

    .line 36
    invoke-virtual {p1}, Ljava/lang/String;->length()I

    move-result v0

    if-ne v0, v1, :cond_0

    .line 37
    invoke-static {}, Lcom/fasterxml/jackson/core/Base64Variants;->getDefaultVariant()Lcom/fasterxml/jackson/core/Base64Variant;

    move-result-object v0

    invoke-virtual {v0, p1}, Lcom/fasterxml/jackson/core/Base64Variant;->decode(Ljava/lang/String;)[B

    move-result-object p1

    .line 38
    invoke-direct {p0, p1, p2}, Lcom/fasterxml/jackson/databind/deser/std/UUIDDeserializer;->_fromBytes([BLcom/fasterxml/jackson/databind/DeserializationContext;)Ljava/util/UUID;

    move-result-object p1

    return-object p1

    .line 40
    :cond_0
    invoke-direct {p0, p1}, Lcom/fasterxml/jackson/databind/deser/std/UUIDDeserializer;->_badFormat(Ljava/lang/String;)V

    :cond_1
    const/16 p2, 0x8

    .line 44
    invoke-virtual {p1, p2}, Ljava/lang/String;->charAt(I)C

    move-result p2

    const/16 v0, 0x2d

    if-ne p2, v0, :cond_2

    const/16 p2, 0xd

    invoke-virtual {p1, p2}, Ljava/lang/String;->charAt(I)C

    move-result p2

    if-ne p2, v0, :cond_2

    const/16 p2, 0x12

    invoke-virtual {p1, p2}, Ljava/lang/String;->charAt(I)C

    move-result p2

    if-ne p2, v0, :cond_2

    const/16 p2, 0x17

    invoke-virtual {p1, p2}, Ljava/lang/String;->charAt(I)C

    move-result p2

    if-eq p2, v0, :cond_3

    .line 46
    :cond_2
    invoke-direct {p0, p1}, Lcom/fasterxml/jackson/databind/deser/std/UUIDDeserializer;->_badFormat(Ljava/lang/String;)V

    :cond_3
    const/4 p2, 0x0

    .line 48
    invoke-static {p1, p2}, Lcom/fasterxml/jackson/databind/deser/std/UUIDDeserializer;->intFromChars(Ljava/lang/String;I)I

    move-result p2

    int-to-long v2, p2

    const/16 p2, 0x20

    shl-long/2addr v2, p2

    const/16 v0, 0x9

    .line 50
    invoke-static {p1, v0}, Lcom/fasterxml/jackson/databind/deser/std/UUIDDeserializer;->shortFromChars(Ljava/lang/String;I)I

    move-result v0

    int-to-long v4, v0

    const/16 v0, 0x10

    shl-long/2addr v4, v0

    const/16 v6, 0xe

    .line 51
    invoke-static {p1, v6}, Lcom/fasterxml/jackson/databind/deser/std/UUIDDeserializer;->shortFromChars(Ljava/lang/String;I)I

    move-result v6

    int-to-long v6, v6

    or-long/2addr v4, v6

    add-long/2addr v2, v4

    const/16 v4, 0x13

    .line 54
    invoke-static {p1, v4}, Lcom/fasterxml/jackson/databind/deser/std/UUIDDeserializer;->shortFromChars(Ljava/lang/String;I)I

    move-result v4

    shl-int/lit8 v0, v4, 0x10

    invoke-static {p1, v1}, Lcom/fasterxml/jackson/databind/deser/std/UUIDDeserializer;->shortFromChars(Ljava/lang/String;I)I

    move-result v1

    or-int/2addr v0, v1

    int-to-long v0, v0

    shl-long/2addr v0, p2

    const/16 v4, 0x1c

    .line 57
    invoke-static {p1, v4}, Lcom/fasterxml/jackson/databind/deser/std/UUIDDeserializer;->intFromChars(Ljava/lang/String;I)I

    move-result p1

    int-to-long v4, p1

    shl-long/2addr v4, p2

    ushr-long p1, v4, p2

    or-long/2addr p1, v0

    .line 61
    new-instance v0, Ljava/util/UUID;

    invoke-direct {v0, v2, v3, p1, p2}, Ljava/util/UUID;-><init>(JJ)V

    return-object v0
.end method

.method protected bridge synthetic _deserializeEmbedded(Ljava/lang/Object;Lcom/fasterxml/jackson/databind/DeserializationContext;)Ljava/lang/Object;
    .locals 0
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    .line 11
    invoke-virtual {p0, p1, p2}, Lcom/fasterxml/jackson/databind/deser/std/UUIDDeserializer;->_deserializeEmbedded(Ljava/lang/Object;Lcom/fasterxml/jackson/databind/DeserializationContext;)Ljava/util/UUID;

    move-result-object p1

    return-object p1
.end method

.method protected _deserializeEmbedded(Ljava/lang/Object;Lcom/fasterxml/jackson/databind/DeserializationContext;)Ljava/util/UUID;
    .locals 1
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    .line 67
    instance-of v0, p1, [B

    if-eqz v0, :cond_0

    .line 68
    check-cast p1, [B

    invoke-direct {p0, p1, p2}, Lcom/fasterxml/jackson/databind/deser/std/UUIDDeserializer;->_fromBytes([BLcom/fasterxml/jackson/databind/DeserializationContext;)Ljava/util/UUID;

    move-result-object p1

    return-object p1

    .line 70
    :cond_0
    invoke-super {p0, p1, p2}, Lcom/fasterxml/jackson/databind/deser/std/FromStringDeserializer;->_deserializeEmbedded(Ljava/lang/Object;Lcom/fasterxml/jackson/databind/DeserializationContext;)Ljava/lang/Object;

    const/4 p1, 0x0

    return-object p1
.end method
