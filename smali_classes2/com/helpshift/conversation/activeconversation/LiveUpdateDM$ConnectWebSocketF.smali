.class Lcom/helpshift/conversation/activeconversation/LiveUpdateDM$ConnectWebSocketF;
.super Lcom/helpshift/common/domain/F;
.source "LiveUpdateDM.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x2
    name = "ConnectWebSocketF"
.end annotation


# instance fields
.field private final id:I

.field final synthetic this$0:Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;


# direct methods
.method constructor <init>(Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;I)V
    .locals 0

    .line 261
    iput-object p1, p0, Lcom/helpshift/conversation/activeconversation/LiveUpdateDM$ConnectWebSocketF;->this$0:Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;

    invoke-direct {p0}, Lcom/helpshift/common/domain/F;-><init>()V

    .line 262
    iput p2, p0, Lcom/helpshift/conversation/activeconversation/LiveUpdateDM$ConnectWebSocketF;->id:I

    return-void
.end method


# virtual methods
.method public f()V
    .locals 5

    .line 267
    iget-object v0, p0, Lcom/helpshift/conversation/activeconversation/LiveUpdateDM$ConnectWebSocketF;->this$0:Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;

    iget-object v0, v0, Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;->listener:Lcom/helpshift/conversation/activeconversation/LiveUpdateDM$TypingIndicatorListener;

    if-eqz v0, :cond_3

    iget v0, p0, Lcom/helpshift/conversation/activeconversation/LiveUpdateDM$ConnectWebSocketF;->id:I

    iget-object v1, p0, Lcom/helpshift/conversation/activeconversation/LiveUpdateDM$ConnectWebSocketF;->this$0:Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;

    iget-object v1, v1, Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;->pingCount:Ljava/util/concurrent/atomic/AtomicInteger;

    invoke-virtual {v1}, Ljava/util/concurrent/atomic/AtomicInteger;->get()I

    move-result v1

    if-ne v0, v1, :cond_3

    .line 268
    iget-object v0, p0, Lcom/helpshift/conversation/activeconversation/LiveUpdateDM$ConnectWebSocketF;->this$0:Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;

    iget-boolean v0, v0, Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;->socketConnected:Z

    if-nez v0, :cond_2

    iget-object v0, p0, Lcom/helpshift/conversation/activeconversation/LiveUpdateDM$ConnectWebSocketF;->this$0:Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;

    iget-boolean v0, v0, Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;->isConnecting:Z

    if-eqz v0, :cond_0

    goto/16 :goto_0

    .line 271
    :cond_0
    iget-object v0, p0, Lcom/helpshift/conversation/activeconversation/LiveUpdateDM$ConnectWebSocketF;->this$0:Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;

    iget-object v0, v0, Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;->domain:Lcom/helpshift/common/domain/Domain;

    invoke-virtual {v0}, Lcom/helpshift/common/domain/Domain;->getWebSocketAuthDM()Lcom/helpshift/auth/domainmodel/WebSocketAuthDM;

    move-result-object v0

    invoke-virtual {v0}, Lcom/helpshift/auth/domainmodel/WebSocketAuthDM;->getAuthToken()Lcom/helpshift/auth/dto/WebSocketAuthData;

    move-result-object v0

    if-nez v0, :cond_1

    .line 273
    iget-object v0, p0, Lcom/helpshift/conversation/activeconversation/LiveUpdateDM$ConnectWebSocketF;->this$0:Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;

    invoke-virtual {v0}, Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;->scheduleConnectionRetry()V

    return-void

    :cond_1
    const-string v1, "Helpshift_LiveUpdateDM"

    const-string v2, "Connecting web-socket"

    .line 277
    invoke-static {v1, v2}, Lcom/helpshift/util/HSLogger;->d(Ljava/lang/String;Ljava/lang/String;)V

    .line 279
    :try_start_0
    iget-object v1, p0, Lcom/helpshift/conversation/activeconversation/LiveUpdateDM$ConnectWebSocketF;->this$0:Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;

    new-instance v2, Lcom/helpshift/common/platform/network/websockets/HSWebSocket$Builder;

    iget-object v3, p0, Lcom/helpshift/conversation/activeconversation/LiveUpdateDM$ConnectWebSocketF;->this$0:Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;

    invoke-virtual {v3, v0}, Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;->getWebSocketPath(Lcom/helpshift/auth/dto/WebSocketAuthData;)Ljava/lang/String;

    move-result-object v0

    invoke-direct {v2, v0}, Lcom/helpshift/common/platform/network/websockets/HSWebSocket$Builder;-><init>(Ljava/lang/String;)V

    sget-object v0, Ljava/util/concurrent/TimeUnit;->SECONDS:Ljava/util/concurrent/TimeUnit;

    const-wide/16 v3, 0x3c

    .line 280
    invoke-virtual {v0, v3, v4}, Ljava/util/concurrent/TimeUnit;->toMillis(J)J

    move-result-wide v3

    long-to-int v0, v3

    invoke-virtual {v2, v0}, Lcom/helpshift/common/platform/network/websockets/HSWebSocket$Builder;->setConnectionTimeout(I)Lcom/helpshift/common/platform/network/websockets/HSWebSocket$Builder;

    move-result-object v0

    const-string v2, "permessage-deflate"

    .line 281
    invoke-virtual {v0, v2}, Lcom/helpshift/common/platform/network/websockets/HSWebSocket$Builder;->addExtension(Ljava/lang/String;)Lcom/helpshift/common/platform/network/websockets/HSWebSocket$Builder;

    move-result-object v0

    const-string v2, "client_no_context_takeover"

    .line 282
    invoke-virtual {v0, v2}, Lcom/helpshift/common/platform/network/websockets/HSWebSocket$Builder;->addExtension(Ljava/lang/String;)Lcom/helpshift/common/platform/network/websockets/HSWebSocket$Builder;

    move-result-object v0

    const-string v2, "server_no_context_takeover"

    .line 283
    invoke-virtual {v0, v2}, Lcom/helpshift/common/platform/network/websockets/HSWebSocket$Builder;->addExtension(Ljava/lang/String;)Lcom/helpshift/common/platform/network/websockets/HSWebSocket$Builder;

    move-result-object v0

    const-string v2, "dirigent-pubsub-v1"

    .line 284
    invoke-virtual {v0, v2}, Lcom/helpshift/common/platform/network/websockets/HSWebSocket$Builder;->addProtocol(Ljava/lang/String;)Lcom/helpshift/common/platform/network/websockets/HSWebSocket$Builder;

    move-result-object v0

    const-string v2, "hs-sdk-ver"

    iget-object v3, p0, Lcom/helpshift/conversation/activeconversation/LiveUpdateDM$ConnectWebSocketF;->this$0:Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;

    iget-object v3, v3, Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;->sdkVersionHeaderValue:Ljava/lang/String;

    .line 285
    invoke-virtual {v0, v2, v3}, Lcom/helpshift/common/platform/network/websockets/HSWebSocket$Builder;->addHeader(Ljava/lang/String;Ljava/lang/String;)Lcom/helpshift/common/platform/network/websockets/HSWebSocket$Builder;

    move-result-object v0

    iget-object v2, p0, Lcom/helpshift/conversation/activeconversation/LiveUpdateDM$ConnectWebSocketF;->this$0:Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;

    .line 286
    invoke-virtual {v0, v2}, Lcom/helpshift/common/platform/network/websockets/HSWebSocket$Builder;->setWebSocketListener(Lcom/helpshift/common/platform/network/websockets/IHSWebSocketListener;)Lcom/helpshift/common/platform/network/websockets/HSWebSocket$Builder;

    move-result-object v0

    .line 287
    invoke-virtual {v0}, Lcom/helpshift/common/platform/network/websockets/HSWebSocket$Builder;->build()Lcom/helpshift/common/platform/network/websockets/HSWebSocket;

    move-result-object v0

    iput-object v0, v1, Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;->webSocket:Lcom/helpshift/common/platform/network/websockets/HSWebSocket;

    .line 289
    iget-object v0, p0, Lcom/helpshift/conversation/activeconversation/LiveUpdateDM$ConnectWebSocketF;->this$0:Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;

    const/4 v1, 0x1

    iput-boolean v1, v0, Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;->isConnecting:Z

    .line 290
    iget-object v0, p0, Lcom/helpshift/conversation/activeconversation/LiveUpdateDM$ConnectWebSocketF;->this$0:Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;

    iget-object v0, v0, Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;->webSocket:Lcom/helpshift/common/platform/network/websockets/HSWebSocket;

    invoke-virtual {v0}, Lcom/helpshift/common/platform/network/websockets/HSWebSocket;->connect()V
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_1

    :catch_0
    move-exception v0

    const-string v1, "Helpshift_LiveUpdateDM"

    const-string v2, "Exception in connecting web-socket"

    .line 293
    invoke-static {v1, v2, v0}, Lcom/helpshift/util/HSLogger;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)V

    .line 294
    iget-object v0, p0, Lcom/helpshift/conversation/activeconversation/LiveUpdateDM$ConnectWebSocketF;->this$0:Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;

    invoke-virtual {v0}, Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;->scheduleConnectionRetry()V

    goto :goto_1

    :cond_2
    :goto_0
    return-void

    :cond_3
    :goto_1
    return-void
.end method
