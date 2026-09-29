.class Lcom/helpshift/websockets/InsufficientDataException;
.super Lcom/helpshift/websockets/WebSocketException;
.source "InsufficientDataException.java"


# static fields
.field private static final serialVersionUID:J = 0x1L


# instance fields
.field private final mReadByteCount:I

.field private final mRequestedByteCount:I


# direct methods
.method public constructor <init>(II)V
    .locals 2

    .line 36
    sget-object v0, Lcom/helpshift/websockets/WebSocketError;->INSUFFICENT_DATA:Lcom/helpshift/websockets/WebSocketError;

    const-string v1, "The end of the stream has been reached unexpectedly."

    invoke-direct {p0, v0, v1}, Lcom/helpshift/websockets/WebSocketException;-><init>(Lcom/helpshift/websockets/WebSocketError;Ljava/lang/String;)V

    .line 38
    iput p1, p0, Lcom/helpshift/websockets/InsufficientDataException;->mRequestedByteCount:I

    .line 39
    iput p2, p0, Lcom/helpshift/websockets/InsufficientDataException;->mReadByteCount:I

    return-void
.end method


# virtual methods
.method public getReadByteCount()I
    .locals 1

    .line 49
    iget v0, p0, Lcom/helpshift/websockets/InsufficientDataException;->mReadByteCount:I

    return v0
.end method

.method public getRequestedByteCount()I
    .locals 1

    .line 44
    iget v0, p0, Lcom/helpshift/websockets/InsufficientDataException;->mRequestedByteCount:I

    return v0
.end method
