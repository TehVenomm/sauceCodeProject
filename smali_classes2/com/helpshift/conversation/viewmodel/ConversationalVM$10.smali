.class Lcom/helpshift/conversation/viewmodel/ConversationalVM$10;
.super Lcom/helpshift/common/domain/F;
.source "ConversationalVM.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lcom/helpshift/conversation/viewmodel/ConversationalVM;->retryMessage(Lcom/helpshift/conversation/activeconversation/message/MessageDM;)V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lcom/helpshift/conversation/viewmodel/ConversationalVM;

.field final synthetic val$readableMessage:Lcom/helpshift/conversation/activeconversation/message/MessageDM;


# direct methods
.method constructor <init>(Lcom/helpshift/conversation/viewmodel/ConversationalVM;Lcom/helpshift/conversation/activeconversation/message/MessageDM;)V
    .locals 0

    .line 807
    iput-object p1, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM$10;->this$0:Lcom/helpshift/conversation/viewmodel/ConversationalVM;

    iput-object p2, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM$10;->val$readableMessage:Lcom/helpshift/conversation/activeconversation/message/MessageDM;

    invoke-direct {p0}, Lcom/helpshift/common/domain/F;-><init>()V

    return-void
.end method


# virtual methods
.method public f()V
    .locals 3

    .line 811
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM$10;->this$0:Lcom/helpshift/conversation/viewmodel/ConversationalVM;

    iget-object v0, v0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->viewableConversation:Lcom/helpshift/conversation/activeconversation/ViewableConversation;

    invoke-virtual {v0}, Lcom/helpshift/conversation/activeconversation/ViewableConversation;->getActiveConversation()Lcom/helpshift/conversation/activeconversation/model/Conversation;

    move-result-object v0

    .line 812
    iget-object v1, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM$10;->val$readableMessage:Lcom/helpshift/conversation/activeconversation/message/MessageDM;

    instance-of v1, v1, Lcom/helpshift/conversation/activeconversation/message/UserMessageDM;

    if-eqz v1, :cond_0

    iget-object v1, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM$10;->this$0:Lcom/helpshift/conversation/viewmodel/ConversationalVM;

    iget-object v1, v1, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->conversationManager:Lcom/helpshift/conversation/activeconversation/ConversationManager;

    invoke-virtual {v1, v0}, Lcom/helpshift/conversation/activeconversation/ConversationManager;->isSynced(Lcom/helpshift/conversation/activeconversation/model/Conversation;)Z

    move-result v1

    if-nez v1, :cond_0

    const-string v0, "Helpshift_ConvsatnlVM"

    const-string v1, "User retrying message to file preissue."

    .line 813
    invoke-static {v0, v1}, Lcom/helpshift/util/HSLogger;->d(Ljava/lang/String;Ljava/lang/String;)V

    .line 814
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM$10;->val$readableMessage:Lcom/helpshift/conversation/activeconversation/message/MessageDM;

    check-cast v0, Lcom/helpshift/conversation/activeconversation/message/UserMessageDM;

    .line 815
    sget-object v1, Lcom/helpshift/conversation/activeconversation/message/UserMessageState;->SENDING:Lcom/helpshift/conversation/activeconversation/message/UserMessageState;

    invoke-virtual {v0, v1}, Lcom/helpshift/conversation/activeconversation/message/UserMessageDM;->setState(Lcom/helpshift/conversation/activeconversation/message/UserMessageState;)V

    .line 816
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM$10;->this$0:Lcom/helpshift/conversation/viewmodel/ConversationalVM;

    iget-object v1, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM$10;->val$readableMessage:Lcom/helpshift/conversation/activeconversation/message/MessageDM;

    iget-object v1, v1, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->body:Ljava/lang/String;

    const/4 v2, 0x1

    invoke-virtual {v0, v1, v2}, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->createPreIssue(Ljava/lang/String;Z)V

    goto :goto_0

    .line 818
    :cond_0
    iget-object v1, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM$10;->this$0:Lcom/helpshift/conversation/viewmodel/ConversationalVM;

    iget-boolean v1, v1, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->isNetworkAvailable:Z

    if-eqz v1, :cond_1

    .line 820
    iget-object v1, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM$10;->this$0:Lcom/helpshift/conversation/viewmodel/ConversationalVM;

    iget-object v1, v1, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->conversationManager:Lcom/helpshift/conversation/activeconversation/ConversationManager;

    iget-object v2, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM$10;->val$readableMessage:Lcom/helpshift/conversation/activeconversation/message/MessageDM;

    invoke-virtual {v1, v0, v2}, Lcom/helpshift/conversation/activeconversation/ConversationManager;->retryMessage(Lcom/helpshift/conversation/activeconversation/model/Conversation;Lcom/helpshift/conversation/activeconversation/message/MessageDM;)V

    .line 821
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM$10;->this$0:Lcom/helpshift/conversation/viewmodel/ConversationalVM;

    iget-object v1, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM$10;->this$0:Lcom/helpshift/conversation/viewmodel/ConversationalVM;

    iget-boolean v1, v1, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->isInBetweenBotExecution:Z

    invoke-virtual {v0, v1}, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->showFakeTypingIndicator(Z)V

    :cond_1
    :goto_0
    return-void
.end method
