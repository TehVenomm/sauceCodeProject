.class final Lcom/helpshift/websockets/DistinguishedNameParser;
.super Ljava/lang/Object;
.source "DistinguishedNameParser.java"


# instance fields
.field private beg:I

.field private chars:[C

.field private cur:I

.field private final dn:Ljava/lang/String;

.field private end:I

.field private final length:I

.field private pos:I


# direct methods
.method public constructor <init>(Ljavax/security/auth/x500/X500Principal;)V
    .locals 1

    .line 48
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    const-string v0, "RFC2253"

    .line 52
    invoke-virtual {p1, v0}, Ljavax/security/auth/x500/X500Principal;->getName(Ljava/lang/String;)Ljava/lang/String;

    move-result-object p1

    iput-object p1, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->dn:Ljava/lang/String;

    .line 53
    iget-object p1, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->dn:Ljava/lang/String;

    invoke-virtual {p1}, Ljava/lang/String;->length()I

    move-result p1

    iput p1, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->length:I

    return-void
.end method

.method private escapedAV()Ljava/lang/String;
    .locals 5

    .line 200
    iget v0, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->pos:I

    iput v0, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->beg:I

    .line 201
    iget v0, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->pos:I

    iput v0, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->end:I

    .line 203
    :cond_0
    :goto_0
    iget v0, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->pos:I

    iget v1, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->length:I

    if-lt v0, v1, :cond_1

    .line 205
    new-instance v0, Ljava/lang/String;

    iget-object v1, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->chars:[C

    iget v2, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->beg:I

    iget v3, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->end:I

    iget v4, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->beg:I

    sub-int/2addr v3, v4

    invoke-direct {v0, v1, v2, v3}, Ljava/lang/String;-><init>([CII)V

    return-object v0

    .line 208
    :cond_1
    iget-object v0, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->chars:[C

    iget v1, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->pos:I

    aget-char v0, v0, v1

    const/16 v1, 0x3b

    const/16 v2, 0x20

    if-eq v0, v2, :cond_4

    if-eq v0, v1, :cond_3

    const/16 v1, 0x5c

    if-eq v0, v1, :cond_2

    packed-switch v0, :pswitch_data_0

    .line 237
    iget-object v0, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->chars:[C

    iget v1, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->end:I

    add-int/lit8 v2, v1, 0x1

    iput v2, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->end:I

    iget-object v2, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->chars:[C

    iget v3, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->pos:I

    aget-char v2, v2, v3

    aput-char v2, v0, v1

    .line 238
    iget v0, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->pos:I

    add-int/lit8 v0, v0, 0x1

    iput v0, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->pos:I

    goto :goto_0

    .line 216
    :cond_2
    iget-object v0, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->chars:[C

    iget v1, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->end:I

    add-int/lit8 v2, v1, 0x1

    iput v2, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->end:I

    invoke-direct {p0}, Lcom/helpshift/websockets/DistinguishedNameParser;->getEscaped()C

    move-result v2

    aput-char v2, v0, v1

    .line 217
    iget v0, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->pos:I

    add-int/lit8 v0, v0, 0x1

    iput v0, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->pos:I

    goto :goto_0

    .line 213
    :cond_3
    :pswitch_0
    new-instance v0, Ljava/lang/String;

    iget-object v1, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->chars:[C

    iget v2, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->beg:I

    iget v3, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->end:I

    iget v4, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->beg:I

    sub-int/2addr v3, v4

    invoke-direct {v0, v1, v2, v3}, Ljava/lang/String;-><init>([CII)V

    return-object v0

    .line 222
    :cond_4
    iget v0, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->end:I

    iput v0, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->cur:I

    .line 224
    iget v0, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->pos:I

    add-int/lit8 v0, v0, 0x1

    iput v0, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->pos:I

    .line 225
    iget-object v0, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->chars:[C

    iget v3, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->end:I

    add-int/lit8 v4, v3, 0x1

    iput v4, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->end:I

    aput-char v2, v0, v3

    .line 227
    :goto_1
    iget v0, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->pos:I

    iget v3, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->length:I

    if-ge v0, v3, :cond_5

    iget-object v0, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->chars:[C

    iget v3, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->pos:I

    aget-char v0, v0, v3

    if-ne v0, v2, :cond_5

    .line 228
    iget-object v0, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->chars:[C

    iget v3, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->end:I

    add-int/lit8 v4, v3, 0x1

    iput v4, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->end:I

    aput-char v2, v0, v3

    .line 227
    iget v0, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->pos:I

    add-int/lit8 v0, v0, 0x1

    iput v0, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->pos:I

    goto :goto_1

    .line 230
    :cond_5
    iget v0, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->pos:I

    iget v2, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->length:I

    if-eq v0, v2, :cond_6

    iget-object v0, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->chars:[C

    iget v2, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->pos:I

    aget-char v0, v0, v2

    const/16 v2, 0x2c

    if-eq v0, v2, :cond_6

    iget-object v0, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->chars:[C

    iget v2, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->pos:I

    aget-char v0, v0, v2

    const/16 v2, 0x2b

    if-eq v0, v2, :cond_6

    iget-object v0, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->chars:[C

    iget v2, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->pos:I

    aget-char v0, v0, v2

    if-ne v0, v1, :cond_0

    .line 233
    :cond_6
    new-instance v0, Ljava/lang/String;

    iget-object v1, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->chars:[C

    iget v2, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->beg:I

    iget v3, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->cur:I

    iget v4, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->beg:I

    sub-int/2addr v3, v4

    invoke-direct {v0, v1, v2, v3}, Ljava/lang/String;-><init>([CII)V

    return-object v0

    nop

    :pswitch_data_0
    .packed-switch 0x2b
        :pswitch_0
        :pswitch_0
    .end packed-switch
.end method

.method private getByte(I)I
    .locals 8

    add-int/lit8 v0, p1, 0x1

    .line 328
    iget v1, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->length:I

    if-ge v0, v1, :cond_6

    .line 334
    iget-object v1, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->chars:[C

    aget-char p1, v1, p1

    const/16 v1, 0x46

    const/16 v2, 0x41

    const/16 v3, 0x66

    const/16 v4, 0x61

    const/16 v5, 0x39

    const/16 v6, 0x30

    if-lt p1, v6, :cond_0

    if-gt p1, v5, :cond_0

    sub-int/2addr p1, v6

    goto :goto_0

    :cond_0
    if-lt p1, v4, :cond_1

    if-gt p1, v3, :cond_1

    add-int/lit8 p1, p1, -0x57

    goto :goto_0

    :cond_1
    if-lt p1, v2, :cond_5

    if-gt p1, v1, :cond_5

    add-int/lit8 p1, p1, -0x37

    .line 348
    :goto_0
    iget-object v7, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->chars:[C

    aget-char v0, v7, v0

    if-lt v0, v6, :cond_2

    if-gt v0, v5, :cond_2

    sub-int/2addr v0, v6

    goto :goto_1

    :cond_2
    if-lt v0, v4, :cond_3

    if-gt v0, v3, :cond_3

    add-int/lit8 v0, v0, -0x57

    goto :goto_1

    :cond_3
    if-lt v0, v2, :cond_4

    if-gt v0, v1, :cond_4

    add-int/lit8 v0, v0, -0x37

    :goto_1
    shl-int/lit8 p1, p1, 0x4

    add-int/2addr p1, v0

    return p1

    .line 359
    :cond_4
    new-instance p1, Ljava/lang/IllegalStateException;

    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    const-string v1, "Malformed DN: "

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v1, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->dn:Ljava/lang/String;

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v0

    invoke-direct {p1, v0}, Ljava/lang/IllegalStateException;-><init>(Ljava/lang/String;)V

    throw p1

    .line 345
    :cond_5
    new-instance p1, Ljava/lang/IllegalStateException;

    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    const-string v1, "Malformed DN: "

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v1, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->dn:Ljava/lang/String;

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v0

    invoke-direct {p1, v0}, Ljava/lang/IllegalStateException;-><init>(Ljava/lang/String;)V

    throw p1

    .line 329
    :cond_6
    new-instance p1, Ljava/lang/IllegalStateException;

    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    const-string v1, "Malformed DN: "

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v1, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->dn:Ljava/lang/String;

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v0

    invoke-direct {p1, v0}, Ljava/lang/IllegalStateException;-><init>(Ljava/lang/String;)V

    throw p1
.end method

.method private getEscaped()C
    .locals 3

    .line 245
    iget v0, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->pos:I

    add-int/lit8 v0, v0, 0x1

    iput v0, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->pos:I

    .line 246
    iget v0, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->pos:I

    iget v1, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->length:I

    if-eq v0, v1, :cond_1

    .line 250
    iget-object v0, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->chars:[C

    iget v1, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->pos:I

    aget-char v0, v0, v1

    const/16 v1, 0x20

    if-eq v0, v1, :cond_0

    const/16 v1, 0x25

    if-eq v0, v1, :cond_0

    const/16 v1, 0x5c

    if-eq v0, v1, :cond_0

    const/16 v1, 0x5f

    if-eq v0, v1, :cond_0

    packed-switch v0, :pswitch_data_0

    packed-switch v0, :pswitch_data_1

    packed-switch v0, :pswitch_data_2

    .line 269
    invoke-direct {p0}, Lcom/helpshift/websockets/DistinguishedNameParser;->getUTF8()C

    move-result v0

    return v0

    .line 265
    :cond_0
    :pswitch_0
    iget-object v0, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->chars:[C

    iget v1, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->pos:I

    aget-char v0, v0, v1

    return v0

    .line 247
    :cond_1
    new-instance v0, Ljava/lang/IllegalStateException;

    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "Unexpected end of DN: "

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v2, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->dn:Ljava/lang/String;

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-direct {v0, v1}, Ljava/lang/IllegalStateException;-><init>(Ljava/lang/String;)V

    throw v0

    :pswitch_data_0
    .packed-switch 0x22
        :pswitch_0
        :pswitch_0
    .end packed-switch

    :pswitch_data_1
    .packed-switch 0x2a
        :pswitch_0
        :pswitch_0
        :pswitch_0
    .end packed-switch

    :pswitch_data_2
    .packed-switch 0x3b
        :pswitch_0
        :pswitch_0
        :pswitch_0
        :pswitch_0
    .end packed-switch
.end method

.method private getUTF8()C
    .locals 8

    .line 276
    iget v0, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->pos:I

    invoke-direct {p0, v0}, Lcom/helpshift/websockets/DistinguishedNameParser;->getByte(I)I

    move-result v0

    .line 277
    iget v1, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->pos:I

    const/4 v2, 0x1

    add-int/2addr v1, v2

    iput v1, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->pos:I

    const/16 v1, 0x80

    if-ge v0, v1, :cond_0

    int-to-char v0, v0

    return v0

    :cond_0
    const/16 v3, 0xc0

    const/16 v4, 0x3f

    if-lt v0, v3, :cond_7

    const/16 v3, 0xf7

    if-gt v0, v3, :cond_7

    const/16 v3, 0xdf

    if-gt v0, v3, :cond_1

    and-int/lit8 v0, v0, 0x1f

    const/4 v3, 0x1

    goto :goto_0

    :cond_1
    const/16 v3, 0xef

    if-gt v0, v3, :cond_2

    const/4 v3, 0x2

    and-int/lit8 v0, v0, 0xf

    goto :goto_0

    :cond_2
    const/4 v3, 0x3

    and-int/lit8 v0, v0, 0x7

    :goto_0
    const/4 v5, 0x0

    :goto_1
    if-ge v5, v3, :cond_6

    .line 300
    iget v6, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->pos:I

    add-int/2addr v6, v2

    iput v6, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->pos:I

    .line 301
    iget v6, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->pos:I

    iget v7, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->length:I

    if-eq v6, v7, :cond_5

    iget-object v6, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->chars:[C

    iget v7, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->pos:I

    aget-char v6, v6, v7

    const/16 v7, 0x5c

    if-eq v6, v7, :cond_3

    goto :goto_2

    .line 304
    :cond_3
    iget v6, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->pos:I

    add-int/2addr v6, v2

    iput v6, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->pos:I

    .line 306
    iget v6, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->pos:I

    invoke-direct {p0, v6}, Lcom/helpshift/websockets/DistinguishedNameParser;->getByte(I)I

    move-result v6

    .line 307
    iget v7, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->pos:I

    add-int/2addr v7, v2

    iput v7, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->pos:I

    and-int/lit16 v7, v6, 0xc0

    if-eq v7, v1, :cond_4

    return v4

    :cond_4
    shl-int/lit8 v0, v0, 0x6

    and-int/lit8 v6, v6, 0x3f

    add-int/2addr v0, v6

    add-int/lit8 v5, v5, 0x1

    goto :goto_1

    :cond_5
    :goto_2
    return v4

    :cond_6
    int-to-char v0, v0

    return v0

    :cond_7
    return v4
.end method

.method private hexAV()Ljava/lang/String;
    .locals 5

    .line 149
    iget v0, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->pos:I

    add-int/lit8 v0, v0, 0x4

    iget v1, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->length:I

    if-ge v0, v1, :cond_7

    .line 154
    iget v0, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->pos:I

    iput v0, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->beg:I

    .line 155
    iget v0, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->pos:I

    add-int/lit8 v0, v0, 0x1

    iput v0, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->pos:I

    .line 160
    :goto_0
    iget v0, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->pos:I

    iget v1, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->length:I

    if-eq v0, v1, :cond_3

    iget-object v0, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->chars:[C

    iget v1, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->pos:I

    aget-char v0, v0, v1

    const/16 v1, 0x2b

    if-eq v0, v1, :cond_3

    iget-object v0, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->chars:[C

    iget v1, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->pos:I

    aget-char v0, v0, v1

    const/16 v1, 0x2c

    if-eq v0, v1, :cond_3

    iget-object v0, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->chars:[C

    iget v1, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->pos:I

    aget-char v0, v0, v1

    const/16 v1, 0x3b

    if-ne v0, v1, :cond_0

    goto :goto_2

    .line 166
    :cond_0
    iget-object v0, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->chars:[C

    iget v1, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->pos:I

    aget-char v0, v0, v1

    const/16 v1, 0x20

    if-ne v0, v1, :cond_1

    .line 167
    iget v0, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->pos:I

    iput v0, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->end:I

    .line 168
    iget v0, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->pos:I

    add-int/lit8 v0, v0, 0x1

    iput v0, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->pos:I

    .line 171
    :goto_1
    iget v0, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->pos:I

    iget v2, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->length:I

    if-ge v0, v2, :cond_4

    iget-object v0, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->chars:[C

    iget v2, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->pos:I

    aget-char v0, v0, v2

    if-ne v0, v1, :cond_4

    iget v0, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->pos:I

    add-int/lit8 v0, v0, 0x1

    iput v0, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->pos:I

    goto :goto_1

    .line 175
    :cond_1
    iget-object v0, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->chars:[C

    iget v2, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->pos:I

    aget-char v0, v0, v2

    const/16 v2, 0x41

    if-lt v0, v2, :cond_2

    iget-object v0, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->chars:[C

    iget v2, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->pos:I

    aget-char v0, v0, v2

    const/16 v2, 0x46

    if-gt v0, v2, :cond_2

    .line 176
    iget-object v0, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->chars:[C

    iget v2, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->pos:I

    aget-char v3, v0, v2

    add-int/2addr v3, v1

    int-to-char v1, v3

    aput-char v1, v0, v2

    .line 179
    :cond_2
    iget v0, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->pos:I

    add-int/lit8 v0, v0, 0x1

    iput v0, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->pos:I

    goto :goto_0

    .line 162
    :cond_3
    :goto_2
    iget v0, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->pos:I

    iput v0, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->end:I

    .line 184
    :cond_4
    iget v0, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->end:I

    iget v1, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->beg:I

    sub-int/2addr v0, v1

    const/4 v1, 0x5

    if-lt v0, v1, :cond_6

    and-int/lit8 v1, v0, 0x1

    if-eqz v1, :cond_6

    .line 190
    div-int/lit8 v1, v0, 0x2

    new-array v1, v1, [B

    const/4 v2, 0x0

    .line 191
    iget v3, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->beg:I

    add-int/lit8 v3, v3, 0x1

    :goto_3
    array-length v4, v1

    if-ge v2, v4, :cond_5

    .line 192
    invoke-direct {p0, v3}, Lcom/helpshift/websockets/DistinguishedNameParser;->getByte(I)I

    move-result v4

    int-to-byte v4, v4

    aput-byte v4, v1, v2

    add-int/lit8 v3, v3, 0x2

    add-int/lit8 v2, v2, 0x1

    goto :goto_3

    .line 195
    :cond_5
    new-instance v1, Ljava/lang/String;

    iget-object v2, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->chars:[C

    iget v3, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->beg:I

    invoke-direct {v1, v2, v3, v0}, Ljava/lang/String;-><init>([CII)V

    return-object v1

    .line 186
    :cond_6
    new-instance v0, Ljava/lang/IllegalStateException;

    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "Unexpected end of DN: "

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v2, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->dn:Ljava/lang/String;

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-direct {v0, v1}, Ljava/lang/IllegalStateException;-><init>(Ljava/lang/String;)V

    throw v0

    .line 151
    :cond_7
    new-instance v0, Ljava/lang/IllegalStateException;

    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "Unexpected end of DN: "

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v2, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->dn:Ljava/lang/String;

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-direct {v0, v1}, Ljava/lang/IllegalStateException;-><init>(Ljava/lang/String;)V

    throw v0
.end method

.method private nextAT()Ljava/lang/String;
    .locals 5

    .line 60
    :goto_0
    iget v0, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->pos:I

    iget v1, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->length:I

    const/16 v2, 0x20

    if-ge v0, v1, :cond_0

    iget-object v0, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->chars:[C

    iget v1, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->pos:I

    aget-char v0, v0, v1

    if-ne v0, v2, :cond_0

    iget v0, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->pos:I

    add-int/lit8 v0, v0, 0x1

    iput v0, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->pos:I

    goto :goto_0

    .line 62
    :cond_0
    iget v0, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->pos:I

    iget v1, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->length:I

    if-ne v0, v1, :cond_1

    const/4 v0, 0x0

    return-object v0

    .line 67
    :cond_1
    iget v0, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->pos:I

    iput v0, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->beg:I

    .line 70
    iget v0, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->pos:I

    add-int/lit8 v0, v0, 0x1

    iput v0, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->pos:I

    .line 71
    :goto_1
    iget v0, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->pos:I

    iget v1, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->length:I

    const/16 v3, 0x3d

    if-ge v0, v1, :cond_2

    iget-object v0, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->chars:[C

    iget v1, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->pos:I

    aget-char v0, v0, v1

    if-eq v0, v3, :cond_2

    iget-object v0, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->chars:[C

    iget v1, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->pos:I

    aget-char v0, v0, v1

    if-eq v0, v2, :cond_2

    iget v0, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->pos:I

    add-int/lit8 v0, v0, 0x1

    iput v0, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->pos:I

    goto :goto_1

    .line 75
    :cond_2
    iget v0, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->pos:I

    iget v1, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->length:I

    if-ge v0, v1, :cond_b

    .line 80
    iget v0, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->pos:I

    iput v0, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->end:I

    .line 84
    iget-object v0, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->chars:[C

    iget v1, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->pos:I

    aget-char v0, v0, v1

    if-ne v0, v2, :cond_5

    .line 85
    :goto_2
    iget v0, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->pos:I

    iget v1, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->length:I

    if-ge v0, v1, :cond_3

    iget-object v0, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->chars:[C

    iget v1, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->pos:I

    aget-char v0, v0, v1

    if-eq v0, v3, :cond_3

    iget-object v0, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->chars:[C

    iget v1, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->pos:I

    aget-char v0, v0, v1

    if-ne v0, v2, :cond_3

    iget v0, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->pos:I

    add-int/lit8 v0, v0, 0x1

    iput v0, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->pos:I

    goto :goto_2

    .line 88
    :cond_3
    iget-object v0, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->chars:[C

    iget v1, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->pos:I

    aget-char v0, v0, v1

    if-ne v0, v3, :cond_4

    iget v0, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->pos:I

    iget v1, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->length:I

    if-eq v0, v1, :cond_4

    goto :goto_3

    .line 89
    :cond_4
    new-instance v0, Ljava/lang/IllegalStateException;

    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "Unexpected end of DN: "

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v2, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->dn:Ljava/lang/String;

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-direct {v0, v1}, Ljava/lang/IllegalStateException;-><init>(Ljava/lang/String;)V

    throw v0

    .line 93
    :cond_5
    :goto_3
    iget v0, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->pos:I

    add-int/lit8 v0, v0, 0x1

    iput v0, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->pos:I

    .line 97
    :goto_4
    iget v0, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->pos:I

    iget v1, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->length:I

    if-ge v0, v1, :cond_6

    iget-object v0, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->chars:[C

    iget v1, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->pos:I

    aget-char v0, v0, v1

    if-ne v0, v2, :cond_6

    iget v0, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->pos:I

    add-int/lit8 v0, v0, 0x1

    iput v0, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->pos:I

    goto :goto_4

    .line 102
    :cond_6
    iget v0, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->end:I

    iget v1, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->beg:I

    sub-int/2addr v0, v1

    const/4 v1, 0x4

    if-le v0, v1, :cond_a

    iget-object v0, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->chars:[C

    iget v2, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->beg:I

    add-int/lit8 v2, v2, 0x3

    aget-char v0, v0, v2

    const/16 v2, 0x2e

    if-ne v0, v2, :cond_a

    iget-object v0, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->chars:[C

    iget v2, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->beg:I

    aget-char v0, v0, v2

    const/16 v2, 0x4f

    if-eq v0, v2, :cond_7

    iget-object v0, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->chars:[C

    iget v2, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->beg:I

    aget-char v0, v0, v2

    const/16 v2, 0x6f

    if-ne v0, v2, :cond_a

    :cond_7
    iget-object v0, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->chars:[C

    iget v2, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->beg:I

    add-int/lit8 v2, v2, 0x1

    aget-char v0, v0, v2

    const/16 v2, 0x49

    if-eq v0, v2, :cond_8

    iget-object v0, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->chars:[C

    iget v2, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->beg:I

    add-int/lit8 v2, v2, 0x1

    aget-char v0, v0, v2

    const/16 v2, 0x69

    if-ne v0, v2, :cond_a

    :cond_8
    iget-object v0, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->chars:[C

    iget v2, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->beg:I

    add-int/lit8 v2, v2, 0x2

    aget-char v0, v0, v2

    const/16 v2, 0x44

    if-eq v0, v2, :cond_9

    iget-object v0, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->chars:[C

    iget v2, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->beg:I

    add-int/lit8 v2, v2, 0x2

    aget-char v0, v0, v2

    const/16 v2, 0x64

    if-ne v0, v2, :cond_a

    .line 106
    :cond_9
    iget v0, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->beg:I

    add-int/2addr v0, v1

    iput v0, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->beg:I

    .line 109
    :cond_a
    new-instance v0, Ljava/lang/String;

    iget-object v1, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->chars:[C

    iget v2, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->beg:I

    iget v3, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->end:I

    iget v4, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->beg:I

    sub-int/2addr v3, v4

    invoke-direct {v0, v1, v2, v3}, Ljava/lang/String;-><init>([CII)V

    return-object v0

    .line 76
    :cond_b
    new-instance v0, Ljava/lang/IllegalStateException;

    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "Unexpected end of DN: "

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v2, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->dn:Ljava/lang/String;

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-direct {v0, v1}, Ljava/lang/IllegalStateException;-><init>(Ljava/lang/String;)V

    throw v0
.end method

.method private quotedAV()Ljava/lang/String;
    .locals 5

    .line 114
    iget v0, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->pos:I

    add-int/lit8 v0, v0, 0x1

    iput v0, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->pos:I

    .line 115
    iget v0, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->pos:I

    iput v0, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->beg:I

    .line 116
    iget v0, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->beg:I

    iput v0, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->end:I

    .line 119
    :goto_0
    iget v0, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->pos:I

    iget v1, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->length:I

    if-eq v0, v1, :cond_3

    .line 123
    iget-object v0, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->chars:[C

    iget v1, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->pos:I

    aget-char v0, v0, v1

    const/16 v1, 0x22

    if-ne v0, v1, :cond_1

    .line 125
    iget v0, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->pos:I

    add-int/lit8 v0, v0, 0x1

    iput v0, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->pos:I

    .line 141
    :goto_1
    iget v0, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->pos:I

    iget v1, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->length:I

    if-ge v0, v1, :cond_0

    iget-object v0, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->chars:[C

    iget v1, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->pos:I

    aget-char v0, v0, v1

    const/16 v1, 0x20

    if-ne v0, v1, :cond_0

    iget v0, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->pos:I

    add-int/lit8 v0, v0, 0x1

    iput v0, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->pos:I

    goto :goto_1

    .line 144
    :cond_0
    new-instance v0, Ljava/lang/String;

    iget-object v1, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->chars:[C

    iget v2, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->beg:I

    iget v3, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->end:I

    iget v4, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->beg:I

    sub-int/2addr v3, v4

    invoke-direct {v0, v1, v2, v3}, Ljava/lang/String;-><init>([CII)V

    return-object v0

    .line 128
    :cond_1
    iget-object v0, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->chars:[C

    iget v1, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->pos:I

    aget-char v0, v0, v1

    const/16 v1, 0x5c

    if-ne v0, v1, :cond_2

    .line 129
    iget-object v0, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->chars:[C

    iget v1, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->end:I

    invoke-direct {p0}, Lcom/helpshift/websockets/DistinguishedNameParser;->getEscaped()C

    move-result v2

    aput-char v2, v0, v1

    goto :goto_2

    .line 133
    :cond_2
    iget-object v0, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->chars:[C

    iget v1, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->end:I

    iget-object v2, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->chars:[C

    iget v3, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->pos:I

    aget-char v2, v2, v3

    aput-char v2, v0, v1

    .line 135
    :goto_2
    iget v0, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->pos:I

    add-int/lit8 v0, v0, 0x1

    iput v0, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->pos:I

    .line 136
    iget v0, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->end:I

    add-int/lit8 v0, v0, 0x1

    iput v0, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->end:I

    goto :goto_0

    .line 120
    :cond_3
    new-instance v0, Ljava/lang/IllegalStateException;

    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "Unexpected end of DN: "

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v2, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->dn:Ljava/lang/String;

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-direct {v0, v1}, Ljava/lang/IllegalStateException;-><init>(Ljava/lang/String;)V

    throw v0
.end method


# virtual methods
.method public findMostSpecific(Ljava/lang/String;)Ljava/lang/String;
    .locals 5

    const/4 v0, 0x0

    .line 373
    iput v0, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->pos:I

    .line 374
    iput v0, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->beg:I

    .line 375
    iput v0, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->end:I

    .line 376
    iput v0, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->cur:I

    .line 377
    iget-object v0, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->dn:Ljava/lang/String;

    invoke-virtual {v0}, Ljava/lang/String;->toCharArray()[C

    move-result-object v0

    iput-object v0, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->chars:[C

    .line 379
    invoke-direct {p0}, Lcom/helpshift/websockets/DistinguishedNameParser;->nextAT()Ljava/lang/String;

    move-result-object v0

    const/4 v1, 0x0

    if-nez v0, :cond_0

    return-object v1

    :cond_0
    :goto_0
    const-string v2, ""

    .line 386
    iget v3, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->pos:I

    iget v4, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->length:I

    if-ne v3, v4, :cond_1

    return-object v1

    .line 390
    :cond_1
    iget-object v3, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->chars:[C

    iget v4, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->pos:I

    aget-char v3, v3, v4

    sparse-switch v3, :sswitch_data_0

    .line 403
    invoke-direct {p0}, Lcom/helpshift/websockets/DistinguishedNameParser;->escapedAV()Ljava/lang/String;

    move-result-object v2

    goto :goto_1

    .line 395
    :sswitch_0
    invoke-direct {p0}, Lcom/helpshift/websockets/DistinguishedNameParser;->hexAV()Ljava/lang/String;

    move-result-object v2

    goto :goto_1

    .line 392
    :sswitch_1
    invoke-direct {p0}, Lcom/helpshift/websockets/DistinguishedNameParser;->quotedAV()Ljava/lang/String;

    move-result-object v2

    .line 409
    :goto_1
    :sswitch_2
    invoke-virtual {p1, v0}, Ljava/lang/String;->equalsIgnoreCase(Ljava/lang/String;)Z

    move-result v0

    if-eqz v0, :cond_2

    return-object v2

    .line 413
    :cond_2
    iget v0, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->pos:I

    iget v2, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->length:I

    if-lt v0, v2, :cond_3

    return-object v1

    .line 417
    :cond_3
    iget-object v0, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->chars:[C

    iget v2, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->pos:I

    aget-char v0, v0, v2

    const/16 v2, 0x2c

    if-eq v0, v2, :cond_6

    iget-object v0, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->chars:[C

    iget v2, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->pos:I

    aget-char v0, v0, v2

    const/16 v2, 0x3b

    if-ne v0, v2, :cond_4

    goto :goto_2

    .line 419
    :cond_4
    iget-object v0, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->chars:[C

    iget v2, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->pos:I

    aget-char v0, v0, v2

    const/16 v2, 0x2b

    if-ne v0, v2, :cond_5

    goto :goto_2

    .line 420
    :cond_5
    new-instance p1, Ljava/lang/IllegalStateException;

    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    const-string v1, "Malformed DN: "

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v1, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->dn:Ljava/lang/String;

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v0

    invoke-direct {p1, v0}, Ljava/lang/IllegalStateException;-><init>(Ljava/lang/String;)V

    throw p1

    .line 423
    :cond_6
    :goto_2
    iget v0, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->pos:I

    add-int/lit8 v0, v0, 0x1

    iput v0, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->pos:I

    .line 424
    invoke-direct {p0}, Lcom/helpshift/websockets/DistinguishedNameParser;->nextAT()Ljava/lang/String;

    move-result-object v0

    if-eqz v0, :cond_7

    goto :goto_0

    .line 426
    :cond_7
    new-instance p1, Ljava/lang/IllegalStateException;

    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    const-string v1, "Malformed DN: "

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v1, p0, Lcom/helpshift/websockets/DistinguishedNameParser;->dn:Ljava/lang/String;

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v0

    invoke-direct {p1, v0}, Ljava/lang/IllegalStateException;-><init>(Ljava/lang/String;)V

    throw p1

    :sswitch_data_0
    .sparse-switch
        0x22 -> :sswitch_1
        0x23 -> :sswitch_0
        0x2b -> :sswitch_2
        0x2c -> :sswitch_2
        0x3b -> :sswitch_2
    .end sparse-switch
.end method
