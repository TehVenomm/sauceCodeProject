.class Lcom/helpshift/conversation/viewmodel/ConversationalVM$8;
.super Lcom/helpshift/common/domain/F;
.source "ConversationalVM.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lcom/helpshift/conversation/viewmodel/ConversationalVM;->sendTextMessage(Ljava/lang/String;)V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lcom/helpshift/conversation/viewmodel/ConversationalVM;

.field final synthetic val$adminMessageWithTextInputDM:Lcom/helpshift/conversation/activeconversation/message/AdminMessageWithTextInputDM;

.field final synthetic val$reply:Ljava/lang/String;


# direct methods
.method constructor <init>(Lcom/helpshift/conversation/viewmodel/ConversationalVM;Ljava/lang/String;Lcom/helpshift/conversation/activeconversation/message/AdminMessageWithTextInputDM;)V
    .locals 0

    .line 776
    iput-object p1, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM$8;->this$0:Lcom/helpshift/conversation/viewmodel/ConversationalVM;

    iput-object p2, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM$8;->val$reply:Ljava/lang/String;

    iput-object p3, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM$8;->val$adminMessageWithTextInputDM:Lcom/helpshift/conversation/activeconversation/message/AdminMessageWithTextInputDM;

    invoke-direct {p0}, Lcom/helpshift/common/domain/F;-><init>()V

    return-void
.end method


# virtual methods
.method public f()V
    .locals 5

    .line 780
    :try_start_0
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM$8;->this$0:Lcom/helpshift/conversation/viewmodel/ConversationalVM;

    iget-object v0, v0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->conversationManager:Lcom/helpshift/conversation/activeconversation/ConversationManager;

    iget-object v1, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM$8;->this$0:Lcom/helpshift/conversation/viewmodel/ConversationalVM;

    iget-object v1, v1, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->viewableConversation:Lcom/helpshift/conversation/activeconversation/ViewableConversation;

    invoke-virtual {v1}, Lcom/helpshift/conversation/activeconversation/ViewableConversation;->getActiveConversation()Lcom/helpshift/conversation/activeconversation/model/Conversation;

    move-result-object v1

    iget-object v2, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM$8;->val$reply:Ljava/lang/String;

    iget-object v3, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM$8;->val$adminMessageWithTextInputDM:Lcom/helpshift/conversation/activeconversation/message/AdminMessageWithTextInputDM;

    const/4 v4, 0x0

    invoke-virtual {v0, v1, v2, v3, v4}, Lcom/helpshift/conversation/activeconversation/ConversationManager;->sendTextMessage(Lcom/helpshift/conversation/activeconversation/model/Conversation;Ljava/lang/String;Lcom/helpshift/conversation/activeconversation/message/AdminMessageWithTextInputDM;Z)V
    :try_end_0
    .catch Lcom/helpshift/common/exception/RootAPIException; {:try_start_0 .. :try_end_0} :catch_0

    .line 787
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM$8;->this$0:Lcom/helpshift/conversation/viewmodel/ConversationalVM;

    iget-object v1, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM$8;->this$0:Lcom/helpshift/conversation/viewmodel/ConversationalVM;

    iget-boolean v1, v1, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->awaitingUserInputForBotStep:Z

    xor-int/lit8 v1, v1, 0x1

    invoke-virtual {v0, v1}, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->showFakeTypingIndicator(Z)V

    return-void

    :catch_0
    move-exception v0

    .line 784
    iget-object v1, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM$8;->this$0:Lcom/helpshift/conversation/viewmodel/ConversationalVM;

    invoke-static {v1, v0}, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->access$000(Lcom/helpshift/conversation/viewmodel/ConversationalVM;Lcom/helpshift/common/exception/RootAPIException;)V

    .line 785
    throw v0
.end method
