.class Lcom/helpshift/conversation/viewmodel/ConversationalVM$20;
.super Lcom/helpshift/common/domain/F;
.source "ConversationalVM.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lcom/helpshift/conversation/viewmodel/ConversationalVM;->onCreateConversationFailure(Ljava/lang/Exception;)V
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

    .line 1322
    iput-object p1, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM$20;->this$0:Lcom/helpshift/conversation/viewmodel/ConversationalVM;

    invoke-direct {p0}, Lcom/helpshift/common/domain/F;-><init>()V

    return-void
.end method


# virtual methods
.method public f()V
    .locals 2

    .line 1326
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM$20;->this$0:Lcom/helpshift/conversation/viewmodel/ConversationalVM;

    const/4 v1, 0x0

    invoke-virtual {v0, v1}, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->showFakeTypingIndicator(Z)V

    .line 1327
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM$20;->this$0:Lcom/helpshift/conversation/viewmodel/ConversationalVM;

    iget-object v0, v0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->renderer:Lcom/helpshift/conversation/activeconversation/ConversationalRenderer;

    if-eqz v0, :cond_1

    .line 1328
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM$20;->this$0:Lcom/helpshift/conversation/viewmodel/ConversationalVM;

    iget-object v0, v0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->messageListVM:Lcom/helpshift/conversation/viewmodel/MessageListVM;

    invoke-virtual {v0}, Lcom/helpshift/conversation/viewmodel/MessageListVM;->getLastUIMessage()Lcom/helpshift/conversation/activeconversation/message/MessageDM;

    move-result-object v0

    .line 1329
    instance-of v1, v0, Lcom/helpshift/conversation/activeconversation/message/UserMessageDM;

    if-eqz v1, :cond_0

    .line 1330
    check-cast v0, Lcom/helpshift/conversation/activeconversation/message/UserMessageDM;

    .line 1331
    sget-object v1, Lcom/helpshift/conversation/activeconversation/message/UserMessageState;->UNSENT_RETRYABLE:Lcom/helpshift/conversation/activeconversation/message/UserMessageState;

    invoke-virtual {v0, v1}, Lcom/helpshift/conversation/activeconversation/message/UserMessageDM;->setState(Lcom/helpshift/conversation/activeconversation/message/UserMessageState;)V

    .line 1333
    :cond_0
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM$20;->this$0:Lcom/helpshift/conversation/viewmodel/ConversationalVM;

    iget-boolean v0, v0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->isNetworkAvailable:Z

    if-nez v0, :cond_1

    .line 1334
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM$20;->this$0:Lcom/helpshift/conversation/viewmodel/ConversationalVM;

    iget-object v0, v0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->renderer:Lcom/helpshift/conversation/activeconversation/ConversationalRenderer;

    const/4 v1, 0x1

    invoke-interface {v0, v1}, Lcom/helpshift/conversation/activeconversation/ConversationalRenderer;->showNetworkErrorFooter(I)V

    :cond_1
    return-void
.end method
