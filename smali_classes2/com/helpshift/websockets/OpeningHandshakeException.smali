.class public Lcom/helpshift/websockets/OpeningHandshakeException;
.super Lcom/helpshift/websockets/WebSocketException;
.source "OpeningHandshakeException.java"


# static fields
.field private static final serialVersionUID:J = 0x1L


# instance fields
.field private final mBody:[B

.field private final mHeaders:Ljava/util/Map;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Ljava/util/List<",
            "Ljava/lang/String;",
            ">;>;"
        }
    .end annotation
.end field

.field private final mStatusLine:Lcom/helpshift/websockets/StatusLine;


# direct methods
.method constructor <init>(Lcom/helpshift/websockets/WebSocketError;Ljava/lang/String;Lcom/helpshift/websockets/StatusLine;Ljava/util/Map;)V
    .locals 6
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Lcom/helpshift/websockets/WebSocketError;",
            "Ljava/lang/String;",
            "Lcom/helpshift/websockets/StatusLine;",
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Ljava/util/List<",
            "Ljava/lang/String;",
            ">;>;)V"
        }
    .end annotation

    const/4 v5, 0x0

    move-object v0, p0

    move-object v1, p1

    move-object v2, p2

    move-object v3, p3

    move-object v4, p4

    .line 47
    invoke-direct/range {v0 .. v5}, Lcom/helpshift/websockets/OpeningHandshakeException;-><init>(Lcom/helpshift/websockets/WebSocketError;Ljava/lang/String;Lcom/helpshift/websockets/StatusLine;Ljava/util/Map;[B)V

    return-void
.end method

.method constructor <init>(Lcom/helpshift/websockets/WebSocketError;Ljava/lang/String;Lcom/helpshift/websockets/StatusLine;Ljava/util/Map;[B)V
    .locals 0
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Lcom/helpshift/websockets/WebSocketError;",
            "Ljava/lang/String;",
            "Lcom/helpshift/websockets/StatusLine;",
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Ljava/util/List<",
            "Ljava/lang/String;",
            ">;>;[B)V"
        }
    .end annotation

    .line 54
    invoke-direct {p0, p1, p2}, Lcom/helpshift/websockets/WebSocketException;-><init>(Lcom/helpshift/websockets/WebSocketError;Ljava/lang/String;)V

    .line 56
    iput-object p3, p0, Lcom/helpshift/websockets/OpeningHandshakeException;->mStatusLine:Lcom/helpshift/websockets/StatusLine;

    .line 57
    iput-object p4, p0, Lcom/helpshift/websockets/OpeningHandshakeException;->mHeaders:Ljava/util/Map;

    .line 58
    iput-object p5, p0, Lcom/helpshift/websockets/OpeningHandshakeException;->mBody:[B

    return-void
.end method


# virtual methods
.method public getBody()[B
    .locals 1

    .line 101
    iget-object v0, p0, Lcom/helpshift/websockets/OpeningHandshakeException;->mBody:[B

    return-object v0
.end method

.method public getHeaders()Ljava/util/Map;
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()",
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Ljava/util/List<",
            "Ljava/lang/String;",
            ">;>;"
        }
    .end annotation

    .line 82
    iget-object v0, p0, Lcom/helpshift/websockets/OpeningHandshakeException;->mHeaders:Ljava/util/Map;

    return-object v0
.end method

.method public getStatusLine()Lcom/helpshift/websockets/StatusLine;
    .locals 1

    .line 69
    iget-object v0, p0, Lcom/helpshift/websockets/OpeningHandshakeException;->mStatusLine:Lcom/helpshift/websockets/StatusLine;

    return-object v0
.end method
