.class public Lcom/helpshift/conversation/dto/WSTypingActionMessage;
.super Ljava/lang/Object;
.source "WSTypingActionMessage.java"

# interfaces
.implements Lcom/helpshift/conversation/dto/WebSocketMessage;


# instance fields
.field public final isAgentTyping:Z

.field public final typingActionTimeoutMillis:J


# direct methods
.method public constructor <init>(ZJ)V
    .locals 0

    .line 7
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 8
    iput-boolean p1, p0, Lcom/helpshift/conversation/dto/WSTypingActionMessage;->isAgentTyping:Z

    .line 9
    iput-wide p2, p0, Lcom/helpshift/conversation/dto/WSTypingActionMessage;->typingActionTimeoutMillis:J

    return-void
.end method
