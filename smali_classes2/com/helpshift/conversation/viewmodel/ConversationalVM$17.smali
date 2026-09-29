.class Lcom/helpshift/conversation/viewmodel/ConversationalVM$17;
.super Lcom/helpshift/common/domain/F;
.source "ConversationalVM.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lcom/helpshift/conversation/viewmodel/ConversationalVM;->onNetworkUnAvailable()V
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

    .line 1191
    iput-object p1, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM$17;->this$0:Lcom/helpshift/conversation/viewmodel/ConversationalVM;

    invoke-direct {p0}, Lcom/helpshift/common/domain/F;-><init>()V

    return-void
.end method


# virtual methods
.method public f()V
    .locals 4

    .line 1194
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM$17;->this$0:Lcom/helpshift/conversation/viewmodel/ConversationalVM;

    const/4 v1, 0x0

    iput-boolean v1, v0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->isNetworkAvailable:Z

    .line 1197
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM$17;->this$0:Lcom/helpshift/conversation/viewmodel/ConversationalVM;

    iget-object v0, v0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->renderer:Lcom/helpshift/conversation/activeconversation/ConversationalRenderer;

    if-nez v0, :cond_0

    return-void

    .line 1200
    :cond_0
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM$17;->this$0:Lcom/helpshift/conversation/viewmodel/ConversationalVM;

    iget-object v0, v0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->viewableConversation:Lcom/helpshift/conversation/activeconversation/ViewableConversation;

    invoke-virtual {v0}, Lcom/helpshift/conversation/activeconversation/ViewableConversation;->getActiveConversation()Lcom/helpshift/conversation/activeconversation/model/Conversation;

    move-result-object v0

    .line 1202
    iget-object v2, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM$17;->this$0:Lcom/helpshift/conversation/viewmodel/ConversationalVM;

    invoke-virtual {v2, v1}, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->showFakeTypingIndicator(Z)V

    .line 1208
    invoke-virtual {v0}, Lcom/helpshift/conversation/activeconversation/model/Conversation;->isInPreIssueMode()Z

    move-result v2

    const/4 v3, 0x1

    if-eqz v2, :cond_1

    iget-object v0, v0, Lcom/helpshift/conversation/activeconversation/model/Conversation;->preConversationServerId:Ljava/lang/String;

    .line 1209
    invoke-static {v0}, Lcom/helpshift/common/StringUtils;->isEmpty(Ljava/lang/String;)Z

    move-result v0

    if-nez v0, :cond_1

    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM$17;->this$0:Lcom/helpshift/conversation/viewmodel/ConversationalVM;

    iget-boolean v0, v0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->awaitingUserInputForBotStep:Z

    if-nez v0, :cond_1

    const/4 v0, 0x1

    goto :goto_0

    :cond_1
    const/4 v0, 0x0

    .line 1210
    :goto_0
    iget-object v2, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM$17;->this$0:Lcom/helpshift/conversation/viewmodel/ConversationalVM;

    iget-boolean v2, v2, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->isInBetweenBotExecution:Z

    if-eqz v2, :cond_2

    iget-object v2, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM$17;->this$0:Lcom/helpshift/conversation/viewmodel/ConversationalVM;

    iget-boolean v2, v2, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->awaitingUserInputForBotStep:Z

    if-nez v2, :cond_2

    const/4 v1, 0x1

    :cond_2
    if-nez v0, :cond_3

    if-eqz v1, :cond_4

    .line 1213
    :cond_3
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM$17;->this$0:Lcom/helpshift/conversation/viewmodel/ConversationalVM;

    iget-object v0, v0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->renderer:Lcom/helpshift/conversation/activeconversation/ConversationalRenderer;

    invoke-interface {v0, v3}, Lcom/helpshift/conversation/activeconversation/ConversationalRenderer;->showNetworkErrorFooter(I)V

    :cond_4
    return-void
.end method
