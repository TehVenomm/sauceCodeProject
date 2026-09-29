.class Lcom/helpshift/conversation/viewmodel/NewConversationVM$5;
.super Lcom/helpshift/common/domain/F;
.source "NewConversationVM.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lcom/helpshift/conversation/viewmodel/NewConversationVM;->showSearchOrStartNewConversationInternal(Z)V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lcom/helpshift/conversation/viewmodel/NewConversationVM;

.field final synthetic val$checkForShowingSearch:Z


# direct methods
.method constructor <init>(Lcom/helpshift/conversation/viewmodel/NewConversationVM;Z)V
    .locals 0

    .line 140
    iput-object p1, p0, Lcom/helpshift/conversation/viewmodel/NewConversationVM$5;->this$0:Lcom/helpshift/conversation/viewmodel/NewConversationVM;

    iput-boolean p2, p0, Lcom/helpshift/conversation/viewmodel/NewConversationVM$5;->val$checkForShowingSearch:Z

    invoke-direct {p0}, Lcom/helpshift/common/domain/F;-><init>()V

    return-void
.end method


# virtual methods
.method public f()V
    .locals 5

    .line 143
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/NewConversationVM$5;->this$0:Lcom/helpshift/conversation/viewmodel/NewConversationVM;

    invoke-static {v0}, Lcom/helpshift/conversation/viewmodel/NewConversationVM;->access$000(Lcom/helpshift/conversation/viewmodel/NewConversationVM;)Z

    move-result v0

    if-eqz v0, :cond_3

    .line 144
    iget-boolean v0, p0, Lcom/helpshift/conversation/viewmodel/NewConversationVM$5;->val$checkForShowingSearch:Z

    if-eqz v0, :cond_1

    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/NewConversationVM$5;->this$0:Lcom/helpshift/conversation/viewmodel/NewConversationVM;

    invoke-virtual {v0}, Lcom/helpshift/conversation/viewmodel/NewConversationVM;->shouldShowSearchOnNewConversation()Z

    move-result v0

    if-eqz v0, :cond_1

    .line 145
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/NewConversationVM$5;->this$0:Lcom/helpshift/conversation/viewmodel/NewConversationVM;

    iget-object v0, v0, Lcom/helpshift/conversation/viewmodel/NewConversationVM;->conversationController:Lcom/helpshift/conversation/domainmodel/ConversationController;

    iget-object v1, p0, Lcom/helpshift/conversation/viewmodel/NewConversationVM$5;->this$0:Lcom/helpshift/conversation/viewmodel/NewConversationVM;

    iget-object v1, v1, Lcom/helpshift/conversation/viewmodel/NewConversationVM;->descriptionViewState:Lcom/helpshift/widget/MutableTextViewState;

    invoke-virtual {v1}, Lcom/helpshift/widget/MutableTextViewState;->getText()Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Lcom/helpshift/conversation/domainmodel/ConversationController;->getFAQSearchResults(Ljava/lang/String;)Ljava/util/ArrayList;

    move-result-object v0

    .line 146
    invoke-virtual {v0}, Ljava/util/ArrayList;->size()I

    move-result v1

    if-lez v1, :cond_1

    .line 147
    iget-object v1, p0, Lcom/helpshift/conversation/viewmodel/NewConversationVM$5;->this$0:Lcom/helpshift/conversation/viewmodel/NewConversationVM;

    iget-object v1, v1, Lcom/helpshift/conversation/viewmodel/NewConversationVM;->rendererWeakRef:Ljava/lang/ref/WeakReference;

    invoke-virtual {v1}, Ljava/lang/ref/WeakReference;->get()Ljava/lang/Object;

    move-result-object v1

    if-eqz v1, :cond_0

    .line 148
    iget-object v1, p0, Lcom/helpshift/conversation/viewmodel/NewConversationVM$5;->this$0:Lcom/helpshift/conversation/viewmodel/NewConversationVM;

    iget-object v1, v1, Lcom/helpshift/conversation/viewmodel/NewConversationVM;->rendererWeakRef:Ljava/lang/ref/WeakReference;

    invoke-virtual {v1}, Ljava/lang/ref/WeakReference;->get()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Lcom/helpshift/conversation/viewmodel/NewConversationRenderer;

    invoke-interface {v1, v0}, Lcom/helpshift/conversation/viewmodel/NewConversationRenderer;->showSearchResultFragment(Ljava/util/ArrayList;)V

    :cond_0
    return-void

    .line 154
    :cond_1
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/NewConversationVM$5;->this$0:Lcom/helpshift/conversation/viewmodel/NewConversationVM;

    iget-object v0, v0, Lcom/helpshift/conversation/viewmodel/NewConversationVM;->conversationController:Lcom/helpshift/conversation/domainmodel/ConversationController;

    invoke-virtual {v0}, Lcom/helpshift/conversation/domainmodel/ConversationController;->getActiveConversationFromStorage()Lcom/helpshift/conversation/activeconversation/model/Conversation;

    move-result-object v0

    if-eqz v0, :cond_2

    .line 156
    iget-object v1, p0, Lcom/helpshift/conversation/viewmodel/NewConversationVM$5;->this$0:Lcom/helpshift/conversation/viewmodel/NewConversationVM;

    iget-object v1, v1, Lcom/helpshift/conversation/viewmodel/NewConversationVM;->domain:Lcom/helpshift/common/domain/Domain;

    new-instance v2, Lcom/helpshift/conversation/viewmodel/NewConversationVM$5$1;

    invoke-direct {v2, p0, v0}, Lcom/helpshift/conversation/viewmodel/NewConversationVM$5$1;-><init>(Lcom/helpshift/conversation/viewmodel/NewConversationVM$5;Lcom/helpshift/conversation/activeconversation/model/Conversation;)V

    invoke-virtual {v1, v2}, Lcom/helpshift/common/domain/Domain;->runOnUI(Lcom/helpshift/common/domain/F;)V

    goto :goto_0

    :cond_2
    const-string v0, "Helpshift_NewConvVM"

    const-string v1, "Creating new conversation"

    .line 170
    invoke-static {v0, v1}, Lcom/helpshift/util/HSLogger;->d(Ljava/lang/String;Ljava/lang/String;)V

    .line 171
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/NewConversationVM$5;->this$0:Lcom/helpshift/conversation/viewmodel/NewConversationVM;

    iget-object v0, v0, Lcom/helpshift/conversation/viewmodel/NewConversationVM;->progressBarViewState:Lcom/helpshift/widget/MutableBaseViewState;

    const/4 v1, 0x1

    invoke-virtual {v0, v1}, Lcom/helpshift/widget/MutableBaseViewState;->setVisible(Z)V

    .line 172
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/NewConversationVM$5;->this$0:Lcom/helpshift/conversation/viewmodel/NewConversationVM;

    iget-object v0, v0, Lcom/helpshift/conversation/viewmodel/NewConversationVM;->startConversationButtonViewState:Lcom/helpshift/widget/MutableBaseViewState;

    const/4 v1, 0x0

    invoke-virtual {v0, v1}, Lcom/helpshift/widget/MutableBaseViewState;->setVisible(Z)V

    .line 173
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/NewConversationVM$5;->this$0:Lcom/helpshift/conversation/viewmodel/NewConversationVM;

    iget-object v0, v0, Lcom/helpshift/conversation/viewmodel/NewConversationVM;->attachImageButtonViewState:Lcom/helpshift/widget/MutableBaseViewState;

    invoke-virtual {v0, v1}, Lcom/helpshift/widget/MutableBaseViewState;->setVisible(Z)V

    .line 174
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/NewConversationVM$5;->this$0:Lcom/helpshift/conversation/viewmodel/NewConversationVM;

    iget-object v0, v0, Lcom/helpshift/conversation/viewmodel/NewConversationVM;->imageAttachmentViewState:Lcom/helpshift/widget/MutableImageAttachmentViewState;

    invoke-virtual {v0, v1}, Lcom/helpshift/widget/MutableImageAttachmentViewState;->setClickable(Z)V

    .line 175
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/NewConversationVM$5;->this$0:Lcom/helpshift/conversation/viewmodel/NewConversationVM;

    iget-object v0, v0, Lcom/helpshift/conversation/viewmodel/NewConversationVM;->conversationController:Lcom/helpshift/conversation/domainmodel/ConversationController;

    iget-object v1, p0, Lcom/helpshift/conversation/viewmodel/NewConversationVM$5;->this$0:Lcom/helpshift/conversation/viewmodel/NewConversationVM;

    iget-object v1, v1, Lcom/helpshift/conversation/viewmodel/NewConversationVM;->descriptionViewState:Lcom/helpshift/widget/MutableTextViewState;

    invoke-virtual {v1}, Lcom/helpshift/widget/MutableTextViewState;->getText()Ljava/lang/String;

    move-result-object v1

    iget-object v2, p0, Lcom/helpshift/conversation/viewmodel/NewConversationVM$5;->this$0:Lcom/helpshift/conversation/viewmodel/NewConversationVM;

    iget-object v2, v2, Lcom/helpshift/conversation/viewmodel/NewConversationVM;->nameViewState:Lcom/helpshift/widget/MutableTextViewState;

    .line 176
    invoke-virtual {v2}, Lcom/helpshift/widget/MutableTextViewState;->getText()Ljava/lang/String;

    move-result-object v2

    iget-object v3, p0, Lcom/helpshift/conversation/viewmodel/NewConversationVM$5;->this$0:Lcom/helpshift/conversation/viewmodel/NewConversationVM;

    iget-object v3, v3, Lcom/helpshift/conversation/viewmodel/NewConversationVM;->emailViewState:Lcom/helpshift/widget/MutableTextViewState;

    .line 177
    invoke-virtual {v3}, Lcom/helpshift/widget/MutableTextViewState;->getText()Ljava/lang/String;

    move-result-object v3

    iget-object v4, p0, Lcom/helpshift/conversation/viewmodel/NewConversationVM$5;->this$0:Lcom/helpshift/conversation/viewmodel/NewConversationVM;

    iget-object v4, v4, Lcom/helpshift/conversation/viewmodel/NewConversationVM;->imageAttachmentViewState:Lcom/helpshift/widget/MutableImageAttachmentViewState;

    .line 178
    invoke-virtual {v4}, Lcom/helpshift/widget/MutableImageAttachmentViewState;->getImagePickerFile()Lcom/helpshift/conversation/dto/ImagePickerFile;

    move-result-object v4

    .line 175
    invoke-virtual {v0, v1, v2, v3, v4}, Lcom/helpshift/conversation/domainmodel/ConversationController;->startNewConversation(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Lcom/helpshift/conversation/dto/ImagePickerFile;)V

    :cond_3
    :goto_0
    return-void
.end method
