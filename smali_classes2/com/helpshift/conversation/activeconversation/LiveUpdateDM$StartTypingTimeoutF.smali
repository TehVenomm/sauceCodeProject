.class Lcom/helpshift/conversation/activeconversation/LiveUpdateDM$StartTypingTimeoutF;
.super Lcom/helpshift/common/domain/F;
.source "LiveUpdateDM.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x2
    name = "StartTypingTimeoutF"
.end annotation


# instance fields
.field id:I

.field final synthetic this$0:Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;


# direct methods
.method constructor <init>(Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;I)V
    .locals 0

    .line 312
    iput-object p1, p0, Lcom/helpshift/conversation/activeconversation/LiveUpdateDM$StartTypingTimeoutF;->this$0:Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;

    invoke-direct {p0}, Lcom/helpshift/common/domain/F;-><init>()V

    .line 313
    iput p2, p0, Lcom/helpshift/conversation/activeconversation/LiveUpdateDM$StartTypingTimeoutF;->id:I

    return-void
.end method


# virtual methods
.method public f()V
    .locals 2

    .line 318
    iget v0, p0, Lcom/helpshift/conversation/activeconversation/LiveUpdateDM$StartTypingTimeoutF;->id:I

    iget-object v1, p0, Lcom/helpshift/conversation/activeconversation/LiveUpdateDM$StartTypingTimeoutF;->this$0:Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;

    iget-object v1, v1, Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;->startTypingId:Ljava/util/concurrent/atomic/AtomicInteger;

    invoke-virtual {v1}, Ljava/util/concurrent/atomic/AtomicInteger;->get()I

    move-result v1

    if-ne v0, v1, :cond_0

    iget-object v0, p0, Lcom/helpshift/conversation/activeconversation/LiveUpdateDM$StartTypingTimeoutF;->this$0:Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;

    iget-object v0, v0, Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;->listener:Lcom/helpshift/conversation/activeconversation/LiveUpdateDM$TypingIndicatorListener;

    if-eqz v0, :cond_0

    const-string v0, "Helpshift_LiveUpdateDM"

    const-string v1, "Start Typing action timed out, disabling TAI"

    .line 319
    invoke-static {v0, v1}, Lcom/helpshift/util/HSLogger;->d(Ljava/lang/String;Ljava/lang/String;)V

    .line 320
    iget-object v0, p0, Lcom/helpshift/conversation/activeconversation/LiveUpdateDM$StartTypingTimeoutF;->this$0:Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;

    const/4 v1, 0x0

    iput-boolean v1, v0, Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;->isAgentTyping:Z

    .line 321
    iget-object v0, p0, Lcom/helpshift/conversation/activeconversation/LiveUpdateDM$StartTypingTimeoutF;->this$0:Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;

    invoke-virtual {v0}, Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;->notifyListener()V

    :cond_0
    return-void
.end method
