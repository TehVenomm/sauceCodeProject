.class Lcom/helpshift/conversation/activeconversation/LiveUpdateDM$PingTimeoutF;
.super Lcom/helpshift/common/domain/F;
.source "LiveUpdateDM.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x2
    name = "PingTimeoutF"
.end annotation


# instance fields
.field pingId:I

.field final synthetic this$0:Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;


# direct methods
.method constructor <init>(Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;I)V
    .locals 0

    .line 337
    iput-object p1, p0, Lcom/helpshift/conversation/activeconversation/LiveUpdateDM$PingTimeoutF;->this$0:Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;

    invoke-direct {p0}, Lcom/helpshift/common/domain/F;-><init>()V

    .line 338
    iput p2, p0, Lcom/helpshift/conversation/activeconversation/LiveUpdateDM$PingTimeoutF;->pingId:I

    return-void
.end method


# virtual methods
.method public f()V
    .locals 3

    .line 343
    iget v0, p0, Lcom/helpshift/conversation/activeconversation/LiveUpdateDM$PingTimeoutF;->pingId:I

    iget-object v1, p0, Lcom/helpshift/conversation/activeconversation/LiveUpdateDM$PingTimeoutF;->this$0:Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;

    iget-object v1, v1, Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;->pingCount:Ljava/util/concurrent/atomic/AtomicInteger;

    invoke-virtual {v1}, Ljava/util/concurrent/atomic/AtomicInteger;->get()I

    move-result v1

    if-ne v0, v1, :cond_0

    iget-object v0, p0, Lcom/helpshift/conversation/activeconversation/LiveUpdateDM$PingTimeoutF;->this$0:Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;

    iget-object v0, v0, Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;->listener:Lcom/helpshift/conversation/activeconversation/LiveUpdateDM$TypingIndicatorListener;

    if-eqz v0, :cond_0

    const-string v0, "Helpshift_LiveUpdateDM"

    const-string v1, "Ping timed out, resetting connection"

    .line 344
    invoke-static {v0, v1}, Lcom/helpshift/util/HSLogger;->d(Ljava/lang/String;Ljava/lang/String;)V

    .line 345
    iget-object v0, p0, Lcom/helpshift/conversation/activeconversation/LiveUpdateDM$PingTimeoutF;->this$0:Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;

    iget-object v0, v0, Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;->disconnectWebSocketF:Lcom/helpshift/common/domain/F;

    invoke-virtual {v0}, Lcom/helpshift/common/domain/F;->f()V

    .line 346
    new-instance v0, Lcom/helpshift/conversation/activeconversation/LiveUpdateDM$ConnectWebSocketF;

    iget-object v1, p0, Lcom/helpshift/conversation/activeconversation/LiveUpdateDM$PingTimeoutF;->this$0:Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;

    iget-object v2, p0, Lcom/helpshift/conversation/activeconversation/LiveUpdateDM$PingTimeoutF;->this$0:Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;

    iget-object v2, v2, Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;->pingCount:Ljava/util/concurrent/atomic/AtomicInteger;

    invoke-virtual {v2}, Ljava/util/concurrent/atomic/AtomicInteger;->incrementAndGet()I

    move-result v2

    invoke-direct {v0, v1, v2}, Lcom/helpshift/conversation/activeconversation/LiveUpdateDM$ConnectWebSocketF;-><init>(Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;I)V

    invoke-virtual {v0}, Lcom/helpshift/conversation/activeconversation/LiveUpdateDM$ConnectWebSocketF;->f()V

    :cond_0
    return-void
.end method
