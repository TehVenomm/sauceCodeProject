.class Lcom/helpshift/conversation/activeconversation/LiveUpdateDM$HandleWebSocketMessageF;
.super Lcom/helpshift/common/domain/F;
.source "LiveUpdateDM.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x2
    name = "HandleWebSocketMessageF"
.end annotation


# instance fields
.field private final message:Ljava/lang/String;

.field final synthetic this$0:Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;


# direct methods
.method constructor <init>(Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;Ljava/lang/String;)V
    .locals 0

    .line 360
    iput-object p1, p0, Lcom/helpshift/conversation/activeconversation/LiveUpdateDM$HandleWebSocketMessageF;->this$0:Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;

    invoke-direct {p0}, Lcom/helpshift/common/domain/F;-><init>()V

    .line 361
    iput-object p2, p0, Lcom/helpshift/conversation/activeconversation/LiveUpdateDM$HandleWebSocketMessageF;->message:Ljava/lang/String;

    return-void
.end method


# virtual methods
.method public f()V
    .locals 6

    .line 367
    iget-object v0, p0, Lcom/helpshift/conversation/activeconversation/LiveUpdateDM$HandleWebSocketMessageF;->this$0:Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;

    iget-object v0, v0, Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;->platform:Lcom/helpshift/common/platform/Platform;

    invoke-interface {v0}, Lcom/helpshift/common/platform/Platform;->getResponseParser()Lcom/helpshift/common/platform/network/ResponseParser;

    move-result-object v0

    iget-object v1, p0, Lcom/helpshift/conversation/activeconversation/LiveUpdateDM$HandleWebSocketMessageF;->message:Ljava/lang/String;

    invoke-interface {v0, v1}, Lcom/helpshift/common/platform/network/ResponseParser;->parseWebSocketMessage(Ljava/lang/String;)Lcom/helpshift/conversation/dto/WebSocketMessage;

    move-result-object v0

    .line 369
    instance-of v1, v0, Lcom/helpshift/conversation/dto/WSPingMessage;

    if-eqz v1, :cond_0

    .line 370
    check-cast v0, Lcom/helpshift/conversation/dto/WSPingMessage;

    iget-wide v0, v0, Lcom/helpshift/conversation/dto/WSPingMessage;->pingWaitTimeMillis:J

    iget-object v2, p0, Lcom/helpshift/conversation/activeconversation/LiveUpdateDM$HandleWebSocketMessageF;->this$0:Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;

    iget-wide v2, v2, Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;->NETWORK_PROPOGATION_DELAY:J

    add-long/2addr v0, v2

    .line 371
    iget-object v2, p0, Lcom/helpshift/conversation/activeconversation/LiveUpdateDM$HandleWebSocketMessageF;->this$0:Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;

    iget-object v2, v2, Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;->domain:Lcom/helpshift/common/domain/Domain;

    new-instance v3, Lcom/helpshift/conversation/activeconversation/LiveUpdateDM$PingTimeoutF;

    iget-object v4, p0, Lcom/helpshift/conversation/activeconversation/LiveUpdateDM$HandleWebSocketMessageF;->this$0:Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;

    iget-object v5, p0, Lcom/helpshift/conversation/activeconversation/LiveUpdateDM$HandleWebSocketMessageF;->this$0:Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;

    iget-object v5, v5, Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;->pingCount:Ljava/util/concurrent/atomic/AtomicInteger;

    invoke-virtual {v5}, Ljava/util/concurrent/atomic/AtomicInteger;->incrementAndGet()I

    move-result v5

    invoke-direct {v3, v4, v5}, Lcom/helpshift/conversation/activeconversation/LiveUpdateDM$PingTimeoutF;-><init>(Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;I)V

    invoke-virtual {v2, v3, v0, v1}, Lcom/helpshift/common/domain/Domain;->runDelayedInParallel(Lcom/helpshift/common/domain/F;J)V

    .line 375
    iget-object v0, p0, Lcom/helpshift/conversation/activeconversation/LiveUpdateDM$HandleWebSocketMessageF;->this$0:Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;

    iget-object v0, v0, Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;->webSocket:Lcom/helpshift/common/platform/network/websockets/HSWebSocket;

    if-eqz v0, :cond_2

    .line 376
    iget-object v0, p0, Lcom/helpshift/conversation/activeconversation/LiveUpdateDM$HandleWebSocketMessageF;->this$0:Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;

    iget-object v0, v0, Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;->webSocket:Lcom/helpshift/common/platform/network/websockets/HSWebSocket;

    const-string v1, "[110]"

    invoke-virtual {v0, v1}, Lcom/helpshift/common/platform/network/websockets/HSWebSocket;->sendMessage(Ljava/lang/String;)V

    goto :goto_1

    .line 380
    :cond_0
    iget-object v1, p0, Lcom/helpshift/conversation/activeconversation/LiveUpdateDM$HandleWebSocketMessageF;->this$0:Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;

    iget-object v1, v1, Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;->listener:Lcom/helpshift/conversation/activeconversation/LiveUpdateDM$TypingIndicatorListener;

    if-eqz v1, :cond_2

    instance-of v1, v0, Lcom/helpshift/conversation/dto/WSTypingActionMessage;

    if-eqz v1, :cond_2

    .line 381
    check-cast v0, Lcom/helpshift/conversation/dto/WSTypingActionMessage;

    .line 383
    iget-boolean v1, v0, Lcom/helpshift/conversation/dto/WSTypingActionMessage;->isAgentTyping:Z

    if-eqz v1, :cond_1

    .line 384
    iget-object v1, p0, Lcom/helpshift/conversation/activeconversation/LiveUpdateDM$HandleWebSocketMessageF;->this$0:Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;

    const/4 v2, 0x1

    iput-boolean v2, v1, Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;->isAgentTyping:Z

    .line 388
    iget-wide v0, v0, Lcom/helpshift/conversation/dto/WSTypingActionMessage;->typingActionTimeoutMillis:J

    iget-object v2, p0, Lcom/helpshift/conversation/activeconversation/LiveUpdateDM$HandleWebSocketMessageF;->this$0:Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;

    iget-wide v2, v2, Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;->NETWORK_PROPOGATION_DELAY:J

    add-long/2addr v0, v2

    .line 389
    iget-object v2, p0, Lcom/helpshift/conversation/activeconversation/LiveUpdateDM$HandleWebSocketMessageF;->this$0:Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;

    iget-object v2, v2, Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;->domain:Lcom/helpshift/common/domain/Domain;

    new-instance v3, Lcom/helpshift/conversation/activeconversation/LiveUpdateDM$StartTypingTimeoutF;

    iget-object v4, p0, Lcom/helpshift/conversation/activeconversation/LiveUpdateDM$HandleWebSocketMessageF;->this$0:Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;

    iget-object v5, p0, Lcom/helpshift/conversation/activeconversation/LiveUpdateDM$HandleWebSocketMessageF;->this$0:Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;

    iget-object v5, v5, Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;->startTypingId:Ljava/util/concurrent/atomic/AtomicInteger;

    invoke-virtual {v5}, Ljava/util/concurrent/atomic/AtomicInteger;->incrementAndGet()I

    move-result v5

    invoke-direct {v3, v4, v5}, Lcom/helpshift/conversation/activeconversation/LiveUpdateDM$StartTypingTimeoutF;-><init>(Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;I)V

    invoke-virtual {v2, v3, v0, v1}, Lcom/helpshift/common/domain/Domain;->runDelayedInParallel(Lcom/helpshift/common/domain/F;J)V

    goto :goto_0

    .line 392
    :cond_1
    iget-object v0, p0, Lcom/helpshift/conversation/activeconversation/LiveUpdateDM$HandleWebSocketMessageF;->this$0:Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;

    const/4 v1, 0x0

    iput-boolean v1, v0, Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;->isAgentTyping:Z

    .line 395
    :goto_0
    iget-object v0, p0, Lcom/helpshift/conversation/activeconversation/LiveUpdateDM$HandleWebSocketMessageF;->this$0:Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;

    invoke-virtual {v0}, Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;->notifyListener()V

    :cond_2
    :goto_1
    return-void
.end method
