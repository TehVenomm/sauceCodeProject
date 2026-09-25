.class Lcom/helpshift/conversation/activeconversation/LiveUpdateDM$2;
.super Lcom/helpshift/common/domain/F;
.source "LiveUpdateDM.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;


# direct methods
.method constructor <init>(Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;)V
    .locals 0

    .line 71
    iput-object p1, p0, Lcom/helpshift/conversation/activeconversation/LiveUpdateDM$2;->this$0:Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;

    invoke-direct {p0}, Lcom/helpshift/common/domain/F;-><init>()V

    return-void
.end method


# virtual methods
.method public f()V
    .locals 3

    .line 74
    iget-object v0, p0, Lcom/helpshift/conversation/activeconversation/LiveUpdateDM$2;->this$0:Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;

    iget-object v0, v0, Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;->listener:Lcom/helpshift/conversation/activeconversation/LiveUpdateDM$TypingIndicatorListener;

    if-eqz v0, :cond_0

    .line 75
    iget-object v0, p0, Lcom/helpshift/conversation/activeconversation/LiveUpdateDM$2;->this$0:Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;

    iget-object v0, v0, Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;->domain:Lcom/helpshift/common/domain/Domain;

    invoke-virtual {v0}, Lcom/helpshift/common/domain/Domain;->getWebSocketAuthDM()Lcom/helpshift/auth/domainmodel/WebSocketAuthDM;

    move-result-object v0

    invoke-virtual {v0}, Lcom/helpshift/auth/domainmodel/WebSocketAuthDM;->refreshAuthToken()Lcom/helpshift/auth/dto/WebSocketAuthData;

    .line 76
    iget-object v0, p0, Lcom/helpshift/conversation/activeconversation/LiveUpdateDM$2;->this$0:Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;

    const/4 v1, 0x1

    iput-boolean v1, v0, Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;->isTokenRefreshed:Z

    .line 77
    new-instance v0, Lcom/helpshift/conversation/activeconversation/LiveUpdateDM$ConnectWebSocketF;

    iget-object v1, p0, Lcom/helpshift/conversation/activeconversation/LiveUpdateDM$2;->this$0:Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;

    iget-object v2, p0, Lcom/helpshift/conversation/activeconversation/LiveUpdateDM$2;->this$0:Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;

    iget-object v2, v2, Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;->pingCount:Ljava/util/concurrent/atomic/AtomicInteger;

    invoke-virtual {v2}, Ljava/util/concurrent/atomic/AtomicInteger;->incrementAndGet()I

    move-result v2

    invoke-direct {v0, v1, v2}, Lcom/helpshift/conversation/activeconversation/LiveUpdateDM$ConnectWebSocketF;-><init>(Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;I)V

    invoke-virtual {v0}, Lcom/helpshift/conversation/activeconversation/LiveUpdateDM$ConnectWebSocketF;->f()V

    :cond_0
    return-void
.end method
