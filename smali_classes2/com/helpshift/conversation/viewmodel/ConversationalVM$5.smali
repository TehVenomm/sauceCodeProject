.class Lcom/helpshift/conversation/viewmodel/ConversationalVM$5;
.super Lcom/helpshift/common/domain/F;
.source "ConversationalVM.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lcom/helpshift/conversation/viewmodel/ConversationalVM;->showFakeTypingIndicator(Z)V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lcom/helpshift/conversation/viewmodel/ConversationalVM;

.field final synthetic val$showFakeTAI:Z


# direct methods
.method constructor <init>(Lcom/helpshift/conversation/viewmodel/ConversationalVM;Z)V
    .locals 0

    .line 673
    iput-object p1, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM$5;->this$0:Lcom/helpshift/conversation/viewmodel/ConversationalVM;

    iput-boolean p2, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM$5;->val$showFakeTAI:Z

    invoke-direct {p0}, Lcom/helpshift/common/domain/F;-><init>()V

    return-void
.end method


# virtual methods
.method public f()V
    .locals 2

    .line 676
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM$5;->this$0:Lcom/helpshift/conversation/viewmodel/ConversationalVM;

    iget-object v0, v0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->renderer:Lcom/helpshift/conversation/activeconversation/ConversationalRenderer;

    if-nez v0, :cond_0

    return-void

    .line 683
    :cond_0
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM$5;->this$0:Lcom/helpshift/conversation/viewmodel/ConversationalVM;

    iget-object v0, v0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->viewableConversation:Lcom/helpshift/conversation/activeconversation/ViewableConversation;

    invoke-virtual {v0}, Lcom/helpshift/conversation/activeconversation/ViewableConversation;->getActiveConversation()Lcom/helpshift/conversation/activeconversation/model/Conversation;

    move-result-object v0

    invoke-virtual {v0}, Lcom/helpshift/conversation/activeconversation/model/Conversation;->isIssueInProgress()Z

    move-result v0

    const/4 v1, 0x0

    if-nez v0, :cond_1

    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM$5;->this$0:Lcom/helpshift/conversation/viewmodel/ConversationalVM;

    iget-object v0, v0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->viewableConversation:Lcom/helpshift/conversation/activeconversation/ViewableConversation;

    .line 684
    invoke-virtual {v0}, Lcom/helpshift/conversation/activeconversation/ViewableConversation;->getActiveConversation()Lcom/helpshift/conversation/activeconversation/model/Conversation;

    move-result-object v0

    invoke-virtual {v0}, Lcom/helpshift/conversation/activeconversation/model/Conversation;->isInPreIssueMode()Z

    move-result v0

    if-nez v0, :cond_1

    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM$5;->this$0:Lcom/helpshift/conversation/viewmodel/ConversationalVM;

    iget-boolean v0, v0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->isInBetweenBotExecution:Z

    if-eqz v0, :cond_3

    .line 686
    :cond_1
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM$5;->this$0:Lcom/helpshift/conversation/viewmodel/ConversationalVM;

    iget-object v0, v0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->viewableConversation:Lcom/helpshift/conversation/activeconversation/ViewableConversation;

    .line 687
    invoke-virtual {v0}, Lcom/helpshift/conversation/activeconversation/ViewableConversation;->isAgentTyping()Z

    move-result v0

    if-nez v0, :cond_2

    iget-boolean v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM$5;->val$showFakeTAI:Z

    if-eqz v0, :cond_3

    :cond_2
    const/4 v0, 0x1

    const/4 v1, 0x1

    .line 689
    :cond_3
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM$5;->this$0:Lcom/helpshift/conversation/viewmodel/ConversationalVM;

    invoke-virtual {v0, v1}, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->updateTypingIndicatorStatus(Z)V

    return-void
.end method
