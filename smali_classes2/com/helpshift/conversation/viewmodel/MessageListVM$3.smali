.class Lcom/helpshift/conversation/viewmodel/MessageListVM$3;
.super Lcom/helpshift/common/domain/F;
.source "MessageListVM.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lcom/helpshift/conversation/viewmodel/MessageListVM;->insertOrUpdateMessage(Lcom/helpshift/conversation/activeconversation/message/MessageDM;)V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lcom/helpshift/conversation/viewmodel/MessageListVM;

.field final synthetic val$messageDM:Lcom/helpshift/conversation/activeconversation/message/MessageDM;


# direct methods
.method constructor <init>(Lcom/helpshift/conversation/viewmodel/MessageListVM;Lcom/helpshift/conversation/activeconversation/message/MessageDM;)V
    .locals 0

    .line 591
    iput-object p1, p0, Lcom/helpshift/conversation/viewmodel/MessageListVM$3;->this$0:Lcom/helpshift/conversation/viewmodel/MessageListVM;

    iput-object p2, p0, Lcom/helpshift/conversation/viewmodel/MessageListVM$3;->val$messageDM:Lcom/helpshift/conversation/activeconversation/message/MessageDM;

    invoke-direct {p0}, Lcom/helpshift/common/domain/F;-><init>()V

    return-void
.end method


# virtual methods
.method public f()V
    .locals 3

    .line 599
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/MessageListVM$3;->this$0:Lcom/helpshift/conversation/viewmodel/MessageListVM;

    iget-object v0, v0, Lcom/helpshift/conversation/viewmodel/MessageListVM;->uiMessageDMs:Ljava/util/List;

    iget-object v1, p0, Lcom/helpshift/conversation/viewmodel/MessageListVM$3;->val$messageDM:Lcom/helpshift/conversation/activeconversation/message/MessageDM;

    invoke-interface {v0, v1}, Ljava/util/List;->contains(Ljava/lang/Object;)Z

    move-result v0

    if-eqz v0, :cond_0

    .line 600
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/MessageListVM$3;->this$0:Lcom/helpshift/conversation/viewmodel/MessageListVM;

    iget-object v1, p0, Lcom/helpshift/conversation/viewmodel/MessageListVM$3;->val$messageDM:Lcom/helpshift/conversation/activeconversation/message/MessageDM;

    invoke-virtual {v0, v1}, Lcom/helpshift/conversation/viewmodel/MessageListVM;->updateUIMessage(Lcom/helpshift/conversation/activeconversation/message/MessageDM;)V

    goto :goto_0

    .line 603
    :cond_0
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/MessageListVM$3;->this$0:Lcom/helpshift/conversation/viewmodel/MessageListVM;

    iget-object v1, p0, Lcom/helpshift/conversation/viewmodel/MessageListVM$3;->val$messageDM:Lcom/helpshift/conversation/activeconversation/message/MessageDM;

    invoke-virtual {v0, v1}, Lcom/helpshift/conversation/viewmodel/MessageListVM;->insertUIMessage(Lcom/helpshift/conversation/activeconversation/message/MessageDM;)V

    .line 604
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/MessageListVM$3;->this$0:Lcom/helpshift/conversation/viewmodel/MessageListVM;

    new-instance v1, Ljava/util/ArrayList;

    iget-object v2, p0, Lcom/helpshift/conversation/viewmodel/MessageListVM$3;->val$messageDM:Lcom/helpshift/conversation/activeconversation/message/MessageDM;

    invoke-static {v2}, Ljava/util/Collections;->singletonList(Ljava/lang/Object;)Ljava/util/List;

    move-result-object v2

    invoke-direct {v1, v2}, Ljava/util/ArrayList;-><init>(Ljava/util/Collection;)V

    invoke-static {v0, v1}, Lcom/helpshift/conversation/viewmodel/MessageListVM;->access$300(Lcom/helpshift/conversation/viewmodel/MessageListVM;Ljava/util/List;)V

    .line 606
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/MessageListVM$3;->this$0:Lcom/helpshift/conversation/viewmodel/MessageListVM;

    invoke-virtual {v0}, Lcom/helpshift/conversation/viewmodel/MessageListVM;->notifyMessageListVMRefreshAll()V

    .line 609
    :goto_0
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/MessageListVM$3;->this$0:Lcom/helpshift/conversation/viewmodel/MessageListVM;

    invoke-virtual {v0}, Lcom/helpshift/conversation/viewmodel/MessageListVM;->notifyUIMessageListUpdated()V

    return-void
.end method
