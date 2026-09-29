.class Lcom/helpshift/conversation/viewmodel/ConversationalVM$13;
.super Lcom/helpshift/common/domain/F;
.source "ConversationalVM.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lcom/helpshift/conversation/viewmodel/ConversationalVM;->handleOptionSelected(Lcom/helpshift/conversation/activeconversation/message/OptionInputMessageDM;Lcom/helpshift/conversation/activeconversation/message/input/OptionInput$Option;Z)V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lcom/helpshift/conversation/viewmodel/ConversationalVM;

.field final synthetic val$isSkipped:Z

.field final synthetic val$message:Lcom/helpshift/conversation/activeconversation/message/OptionInputMessageDM;

.field final synthetic val$selectedOption:Lcom/helpshift/conversation/activeconversation/message/input/OptionInput$Option;


# direct methods
.method constructor <init>(Lcom/helpshift/conversation/viewmodel/ConversationalVM;Lcom/helpshift/conversation/activeconversation/message/OptionInputMessageDM;Lcom/helpshift/conversation/activeconversation/message/input/OptionInput$Option;Z)V
    .locals 0

    .line 992
    iput-object p1, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM$13;->this$0:Lcom/helpshift/conversation/viewmodel/ConversationalVM;

    iput-object p2, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM$13;->val$message:Lcom/helpshift/conversation/activeconversation/message/OptionInputMessageDM;

    iput-object p3, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM$13;->val$selectedOption:Lcom/helpshift/conversation/activeconversation/message/input/OptionInput$Option;

    iput-boolean p4, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM$13;->val$isSkipped:Z

    invoke-direct {p0}, Lcom/helpshift/common/domain/F;-><init>()V

    return-void
.end method


# virtual methods
.method public f()V
    .locals 5

    .line 996
    :try_start_0
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM$13;->this$0:Lcom/helpshift/conversation/viewmodel/ConversationalVM;

    iget-object v0, v0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->conversationManager:Lcom/helpshift/conversation/activeconversation/ConversationManager;

    iget-object v1, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM$13;->this$0:Lcom/helpshift/conversation/viewmodel/ConversationalVM;

    iget-object v1, v1, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->viewableConversation:Lcom/helpshift/conversation/activeconversation/ViewableConversation;

    invoke-virtual {v1}, Lcom/helpshift/conversation/activeconversation/ViewableConversation;->getActiveConversation()Lcom/helpshift/conversation/activeconversation/model/Conversation;

    move-result-object v1

    iget-object v2, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM$13;->val$message:Lcom/helpshift/conversation/activeconversation/message/OptionInputMessageDM;

    iget-object v3, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM$13;->val$selectedOption:Lcom/helpshift/conversation/activeconversation/message/input/OptionInput$Option;

    iget-boolean v4, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM$13;->val$isSkipped:Z

    invoke-virtual {v0, v1, v2, v3, v4}, Lcom/helpshift/conversation/activeconversation/ConversationManager;->sendOptionInputMessage(Lcom/helpshift/conversation/activeconversation/model/Conversation;Lcom/helpshift/conversation/activeconversation/message/OptionInputMessageDM;Lcom/helpshift/conversation/activeconversation/message/input/OptionInput$Option;Z)V
    :try_end_0
    .catch Lcom/helpshift/common/exception/RootAPIException; {:try_start_0 .. :try_end_0} :catch_0

    .line 1006
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM$13;->this$0:Lcom/helpshift/conversation/viewmodel/ConversationalVM;

    iget-object v0, v0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->viewableConversation:Lcom/helpshift/conversation/activeconversation/ViewableConversation;

    invoke-virtual {v0}, Lcom/helpshift/conversation/activeconversation/ViewableConversation;->getActiveConversation()Lcom/helpshift/conversation/activeconversation/model/Conversation;

    move-result-object v0

    invoke-virtual {v0}, Lcom/helpshift/conversation/activeconversation/model/Conversation;->isIssueInProgress()Z

    move-result v0

    if-eqz v0, :cond_0

    .line 1007
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM$13;->this$0:Lcom/helpshift/conversation/viewmodel/ConversationalVM;

    iget-object v1, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM$13;->this$0:Lcom/helpshift/conversation/viewmodel/ConversationalVM;

    iget-boolean v1, v1, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->awaitingUserInputForBotStep:Z

    xor-int/lit8 v1, v1, 0x1

    invoke-virtual {v0, v1}, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->showFakeTypingIndicator(Z)V

    :cond_0
    return-void

    :catch_0
    move-exception v0

    .line 1001
    iget-object v1, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM$13;->this$0:Lcom/helpshift/conversation/viewmodel/ConversationalVM;

    invoke-static {v1, v0}, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->access$000(Lcom/helpshift/conversation/viewmodel/ConversationalVM;Lcom/helpshift/common/exception/RootAPIException;)V

    .line 1002
    throw v0
.end method
