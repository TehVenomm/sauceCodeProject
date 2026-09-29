.class Lcom/helpshift/conversation/viewmodel/NewConversationVM$5$1;
.super Lcom/helpshift/common/domain/F;
.source "NewConversationVM.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lcom/helpshift/conversation/viewmodel/NewConversationVM$5;->f()V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$1:Lcom/helpshift/conversation/viewmodel/NewConversationVM$5;

.field final synthetic val$activeConversation:Lcom/helpshift/conversation/activeconversation/model/Conversation;


# direct methods
.method constructor <init>(Lcom/helpshift/conversation/viewmodel/NewConversationVM$5;Lcom/helpshift/conversation/activeconversation/model/Conversation;)V
    .locals 0

    .line 156
    iput-object p1, p0, Lcom/helpshift/conversation/viewmodel/NewConversationVM$5$1;->this$1:Lcom/helpshift/conversation/viewmodel/NewConversationVM$5;

    iput-object p2, p0, Lcom/helpshift/conversation/viewmodel/NewConversationVM$5$1;->val$activeConversation:Lcom/helpshift/conversation/activeconversation/model/Conversation;

    invoke-direct {p0}, Lcom/helpshift/common/domain/F;-><init>()V

    return-void
.end method


# virtual methods
.method public f()V
    .locals 3

    .line 159
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/NewConversationVM$5$1;->this$1:Lcom/helpshift/conversation/viewmodel/NewConversationVM$5;

    iget-object v0, v0, Lcom/helpshift/conversation/viewmodel/NewConversationVM$5;->this$0:Lcom/helpshift/conversation/viewmodel/NewConversationVM;

    iget-object v0, v0, Lcom/helpshift/conversation/viewmodel/NewConversationVM;->progressBarViewState:Lcom/helpshift/widget/MutableBaseViewState;

    const/4 v1, 0x0

    invoke-virtual {v0, v1}, Lcom/helpshift/widget/MutableBaseViewState;->setVisible(Z)V

    .line 160
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/NewConversationVM$5$1;->this$1:Lcom/helpshift/conversation/viewmodel/NewConversationVM$5;

    iget-object v0, v0, Lcom/helpshift/conversation/viewmodel/NewConversationVM$5;->this$0:Lcom/helpshift/conversation/viewmodel/NewConversationVM;

    iget-object v0, v0, Lcom/helpshift/conversation/viewmodel/NewConversationVM;->startConversationButtonViewState:Lcom/helpshift/widget/MutableBaseViewState;

    const/4 v1, 0x1

    invoke-virtual {v0, v1}, Lcom/helpshift/widget/MutableBaseViewState;->setVisible(Z)V

    .line 161
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/NewConversationVM$5$1;->this$1:Lcom/helpshift/conversation/viewmodel/NewConversationVM$5;

    iget-object v0, v0, Lcom/helpshift/conversation/viewmodel/NewConversationVM$5;->this$0:Lcom/helpshift/conversation/viewmodel/NewConversationVM;

    iget-object v0, v0, Lcom/helpshift/conversation/viewmodel/NewConversationVM;->imageAttachmentViewState:Lcom/helpshift/widget/MutableImageAttachmentViewState;

    invoke-virtual {v0, v1}, Lcom/helpshift/widget/MutableImageAttachmentViewState;->setClickable(Z)V

    .line 162
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/NewConversationVM$5$1;->this$1:Lcom/helpshift/conversation/viewmodel/NewConversationVM$5;

    iget-object v0, v0, Lcom/helpshift/conversation/viewmodel/NewConversationVM$5;->this$0:Lcom/helpshift/conversation/viewmodel/NewConversationVM;

    iget-object v0, v0, Lcom/helpshift/conversation/viewmodel/NewConversationVM;->attachImageButtonViewState:Lcom/helpshift/widget/MutableBaseViewState;

    iget-object v1, p0, Lcom/helpshift/conversation/viewmodel/NewConversationVM$5$1;->this$1:Lcom/helpshift/conversation/viewmodel/NewConversationVM$5;

    iget-object v1, v1, Lcom/helpshift/conversation/viewmodel/NewConversationVM$5;->this$0:Lcom/helpshift/conversation/viewmodel/NewConversationVM;

    iget-object v1, v1, Lcom/helpshift/conversation/viewmodel/NewConversationVM;->imageAttachmentViewState:Lcom/helpshift/widget/MutableImageAttachmentViewState;

    invoke-virtual {v1}, Lcom/helpshift/widget/MutableImageAttachmentViewState;->getImagePath()Ljava/lang/String;

    move-result-object v1

    invoke-static {v1}, Lcom/helpshift/common/StringUtils;->isEmpty(Ljava/lang/String;)Z

    move-result v1

    invoke-virtual {v0, v1}, Lcom/helpshift/widget/MutableBaseViewState;->setVisible(Z)V

    .line 163
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/NewConversationVM$5$1;->this$1:Lcom/helpshift/conversation/viewmodel/NewConversationVM$5;

    iget-object v0, v0, Lcom/helpshift/conversation/viewmodel/NewConversationVM$5;->this$0:Lcom/helpshift/conversation/viewmodel/NewConversationVM;

    iget-object v0, v0, Lcom/helpshift/conversation/viewmodel/NewConversationVM;->rendererWeakRef:Ljava/lang/ref/WeakReference;

    invoke-virtual {v0}, Ljava/lang/ref/WeakReference;->get()Ljava/lang/Object;

    move-result-object v0

    if-eqz v0, :cond_0

    .line 164
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/NewConversationVM$5$1;->this$1:Lcom/helpshift/conversation/viewmodel/NewConversationVM$5;

    iget-object v0, v0, Lcom/helpshift/conversation/viewmodel/NewConversationVM$5;->this$0:Lcom/helpshift/conversation/viewmodel/NewConversationVM;

    iget-object v0, v0, Lcom/helpshift/conversation/viewmodel/NewConversationVM;->rendererWeakRef:Ljava/lang/ref/WeakReference;

    invoke-virtual {v0}, Ljava/lang/ref/WeakReference;->get()Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Lcom/helpshift/conversation/viewmodel/NewConversationRenderer;

    iget-object v1, p0, Lcom/helpshift/conversation/viewmodel/NewConversationVM$5$1;->val$activeConversation:Lcom/helpshift/conversation/activeconversation/model/Conversation;

    iget-object v1, v1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->localId:Ljava/lang/Long;

    invoke-virtual {v1}, Ljava/lang/Long;->longValue()J

    move-result-wide v1

    invoke-interface {v0, v1, v2}, Lcom/helpshift/conversation/viewmodel/NewConversationRenderer;->gotoConversation(J)V

    :cond_0
    return-void
.end method
