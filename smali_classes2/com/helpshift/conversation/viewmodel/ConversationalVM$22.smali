.class Lcom/helpshift/conversation/viewmodel/ConversationalVM$22;
.super Lcom/helpshift/common/domain/F;
.source "ConversationalVM.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lcom/helpshift/conversation/viewmodel/ConversationalVM;->updateReplyBoxVisibility()V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lcom/helpshift/conversation/viewmodel/ConversationalVM;


# direct methods
.method constructor <init>(Lcom/helpshift/conversation/viewmodel/ConversationalVM;)V
    .locals 0

    .line 1462
    iput-object p1, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM$22;->this$0:Lcom/helpshift/conversation/viewmodel/ConversationalVM;

    invoke-direct {p0}, Lcom/helpshift/common/domain/F;-><init>()V

    return-void
.end method


# virtual methods
.method public f()V
    .locals 2

    .line 1465
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM$22;->this$0:Lcom/helpshift/conversation/viewmodel/ConversationalVM;

    iget-object v0, v0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->renderer:Lcom/helpshift/conversation/activeconversation/ConversationalRenderer;

    if-nez v0, :cond_0

    return-void

    .line 1469
    :cond_0
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM$22;->this$0:Lcom/helpshift/conversation/viewmodel/ConversationalVM;

    iget-object v0, v0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->replyBoxViewState:Lcom/helpshift/widget/MutableReplyBoxViewState;

    const/4 v1, 0x0

    invoke-virtual {v0, v1}, Lcom/helpshift/widget/MutableReplyBoxViewState;->setVisible(Z)V

    .line 1470
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM$22;->this$0:Lcom/helpshift/conversation/viewmodel/ConversationalVM;

    iget-object v1, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM$22;->this$0:Lcom/helpshift/conversation/viewmodel/ConversationalVM;

    invoke-static {v1}, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->access$200(Lcom/helpshift/conversation/viewmodel/ConversationalVM;)Lcom/helpshift/conversation/activeconversation/message/MessageDM;

    move-result-object v1

    check-cast v1, Lcom/helpshift/conversation/activeconversation/message/OptionInputMessageDM;

    invoke-static {v0, v1}, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->access$300(Lcom/helpshift/conversation/viewmodel/ConversationalVM;Lcom/helpshift/conversation/activeconversation/message/OptionInputMessageDM;)V

    return-void
.end method
