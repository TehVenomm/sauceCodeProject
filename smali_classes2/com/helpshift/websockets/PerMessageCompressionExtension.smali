.class abstract Lcom/helpshift/websockets/PerMessageCompressionExtension;
.super Lcom/helpshift/websockets/WebSocketExtension;
.source "PerMessageCompressionExtension.java"


# direct methods
.method public constructor <init>(Ljava/lang/String;)V
    .locals 0

    .line 32
    invoke-direct {p0, p1}, Lcom/helpshift/websockets/WebSocketExtension;-><init>(Ljava/lang/String;)V

    return-void
.end method


# virtual methods
.method protected abstract compress([B)[B
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Lcom/helpshift/websockets/WebSocketException;
        }
    .end annotation
.end method

.method protected abstract decompress([B)[B
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Lcom/helpshift/websockets/WebSocketException;
        }
    .end annotation
.end method
