.class public Lcom/helpshift/websockets/WebSocketException;
.super Ljava/lang/Exception;
.source "WebSocketException.java"


# static fields
.field private static final serialVersionUID:J = 0x1L


# instance fields
.field private final mError:Lcom/helpshift/websockets/WebSocketError;


# direct methods
.method public constructor <init>(Lcom/helpshift/websockets/WebSocketError;)V
    .locals 0

    .line 33
    invoke-direct {p0}, Ljava/lang/Exception;-><init>()V

    .line 34
    iput-object p1, p0, Lcom/helpshift/websockets/WebSocketException;->mError:Lcom/helpshift/websockets/WebSocketError;

    return-void
.end method

.method public constructor <init>(Lcom/helpshift/websockets/WebSocketError;Ljava/lang/String;)V
    .locals 0

    .line 39
    invoke-direct {p0, p2}, Ljava/lang/Exception;-><init>(Ljava/lang/String;)V

    .line 41
    iput-object p1, p0, Lcom/helpshift/websockets/WebSocketException;->mError:Lcom/helpshift/websockets/WebSocketError;

    return-void
.end method

.method public constructor <init>(Lcom/helpshift/websockets/WebSocketError;Ljava/lang/String;Ljava/lang/Throwable;)V
    .locals 0

    .line 53
    invoke-direct {p0, p2, p3}, Ljava/lang/Exception;-><init>(Ljava/lang/String;Ljava/lang/Throwable;)V

    .line 55
    iput-object p1, p0, Lcom/helpshift/websockets/WebSocketException;->mError:Lcom/helpshift/websockets/WebSocketError;

    return-void
.end method

.method public constructor <init>(Lcom/helpshift/websockets/WebSocketError;Ljava/lang/Throwable;)V
    .locals 0

    .line 46
    invoke-direct {p0, p2}, Ljava/lang/Exception;-><init>(Ljava/lang/Throwable;)V

    .line 48
    iput-object p1, p0, Lcom/helpshift/websockets/WebSocketException;->mError:Lcom/helpshift/websockets/WebSocketError;

    return-void
.end method


# virtual methods
.method public getError()Lcom/helpshift/websockets/WebSocketError;
    .locals 1

    .line 60
    iget-object v0, p0, Lcom/helpshift/websockets/WebSocketException;->mError:Lcom/helpshift/websockets/WebSocketError;

    return-object v0
.end method
