.class Lcom/helpshift/websockets/FixedDistanceHuffman;
.super Lcom/helpshift/websockets/Huffman;
.source "FixedDistanceHuffman.java"


# static fields
.field private static final INSTANCE:Lcom/helpshift/websockets/FixedDistanceHuffman;


# direct methods
.method static constructor <clinit>()V
    .locals 1

    .line 26
    new-instance v0, Lcom/helpshift/websockets/FixedDistanceHuffman;

    invoke-direct {v0}, Lcom/helpshift/websockets/FixedDistanceHuffman;-><init>()V

    sput-object v0, Lcom/helpshift/websockets/FixedDistanceHuffman;->INSTANCE:Lcom/helpshift/websockets/FixedDistanceHuffman;

    return-void
.end method

.method private constructor <init>()V
    .locals 1

    .line 30
    invoke-static {}, Lcom/helpshift/websockets/FixedDistanceHuffman;->buildCodeLensFromSym()[I

    move-result-object v0

    invoke-direct {p0, v0}, Lcom/helpshift/websockets/Huffman;-><init>([I)V

    return-void
.end method

.method private static buildCodeLensFromSym()[I
    .locals 4

    const/16 v0, 0x20

    .line 40
    new-array v1, v0, [I

    const/4 v2, 0x0

    :goto_0
    if-ge v2, v0, :cond_0

    const/4 v3, 0x5

    .line 43
    aput v3, v1, v2

    add-int/lit8 v2, v2, 0x1

    goto :goto_0

    :cond_0
    return-object v1
.end method

.method public static getInstance()Lcom/helpshift/websockets/FixedDistanceHuffman;
    .locals 1

    .line 54
    sget-object v0, Lcom/helpshift/websockets/FixedDistanceHuffman;->INSTANCE:Lcom/helpshift/websockets/FixedDistanceHuffman;

    return-object v0
.end method
