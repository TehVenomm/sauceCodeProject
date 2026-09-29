.class public Lcom/helpshift/conversation/dto/WSPingMessage;
.super Ljava/lang/Object;
.source "WSPingMessage.java"

# interfaces
.implements Lcom/helpshift/conversation/dto/WebSocketMessage;


# instance fields
.field public final pingWaitTimeMillis:J


# direct methods
.method public constructor <init>(J)V
    .locals 0

    .line 7
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 8
    iput-wide p1, p0, Lcom/helpshift/conversation/dto/WSPingMessage;->pingWaitTimeMillis:J

    return-void
.end method
