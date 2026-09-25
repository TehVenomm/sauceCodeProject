.class Lcom/helpshift/conversation/viewmodel/MessageListVM$4;
.super Lcom/helpshift/common/domain/F;
.source "MessageListVM.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lcom/helpshift/conversation/viewmodel/MessageListVM;->checkAndPrependSystemMessagesBeforeFirstMessage()V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lcom/helpshift/conversation/viewmodel/MessageListVM;


# direct methods
.method constructor <init>(Lcom/helpshift/conversation/viewmodel/MessageListVM;)V
    .locals 0

    .line 1060
    iput-object p1, p0, Lcom/helpshift/conversation/viewmodel/MessageListVM$4;->this$0:Lcom/helpshift/conversation/viewmodel/MessageListVM;

    invoke-direct {p0}, Lcom/helpshift/common/domain/F;-><init>()V

    return-void
.end method


# virtual methods
.method public f()V
    .locals 6

    .line 1063
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/MessageListVM$4;->this$0:Lcom/helpshift/conversation/viewmodel/MessageListVM;

    iget-object v0, v0, Lcom/helpshift/conversation/viewmodel/MessageListVM;->uiMessageDMs:Ljava/util/List;

    invoke-interface {v0}, Ljava/util/List;->size()I

    move-result v0

    if-nez v0, :cond_0

    return-void

    .line 1069
    :cond_0
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/MessageListVM$4;->this$0:Lcom/helpshift/conversation/viewmodel/MessageListVM;

    iget-object v0, v0, Lcom/helpshift/conversation/viewmodel/MessageListVM;->uiMessageDMs:Ljava/util/List;

    const/4 v1, 0x0

    invoke-interface {v0, v1}, Ljava/util/List;->get(I)Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Lcom/helpshift/conversation/activeconversation/message/MessageDM;

    .line 1071
    iget-object v2, p0, Lcom/helpshift/conversation/viewmodel/MessageListVM$4;->this$0:Lcom/helpshift/conversation/viewmodel/MessageListVM;

    invoke-static {v2, v0}, Lcom/helpshift/conversation/viewmodel/MessageListVM;->access$400(Lcom/helpshift/conversation/viewmodel/MessageListVM;Lcom/helpshift/conversation/activeconversation/message/MessageDM;)Z

    move-result v2

    if-eqz v2, :cond_1

    return-void

    .line 1076
    :cond_1
    iget-object v2, p0, Lcom/helpshift/conversation/viewmodel/MessageListVM$4;->this$0:Lcom/helpshift/conversation/viewmodel/MessageListVM;

    iget-object v2, v2, Lcom/helpshift/conversation/viewmodel/MessageListVM;->domain:Lcom/helpshift/common/domain/Domain;

    .line 1077
    invoke-virtual {v2}, Lcom/helpshift/common/domain/Domain;->getSDKConfigurationDM()Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;

    move-result-object v2

    const-string v3, "showConversationInfoScreen"

    invoke-virtual {v2, v3}, Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;->getBoolean(Ljava/lang/String;)Z

    move-result v2

    .line 1078
    iget-object v3, p0, Lcom/helpshift/conversation/viewmodel/MessageListVM$4;->this$0:Lcom/helpshift/conversation/viewmodel/MessageListVM;

    const/4 v4, 0x0

    const/4 v5, 0x1

    invoke-static {v3, v4, v0, v5, v2}, Lcom/helpshift/conversation/viewmodel/MessageListVM;->access$500(Lcom/helpshift/conversation/viewmodel/MessageListVM;Lcom/helpshift/conversation/activeconversation/message/MessageDM;Lcom/helpshift/conversation/activeconversation/message/MessageDM;ZZ)Ljava/util/List;

    move-result-object v0

    .line 1083
    invoke-static {v0}, Lcom/helpshift/common/ListUtils;->isEmpty(Ljava/util/List;)Z

    move-result v2

    if-eqz v2, :cond_2

    return-void

    .line 1087
    :cond_2
    iget-object v2, p0, Lcom/helpshift/conversation/viewmodel/MessageListVM$4;->this$0:Lcom/helpshift/conversation/viewmodel/MessageListVM;

    iget-object v2, v2, Lcom/helpshift/conversation/viewmodel/MessageListVM;->uiMessageDMs:Ljava/util/List;

    invoke-interface {v2, v1, v0}, Ljava/util/List;->addAll(ILjava/util/Collection;)Z

    .line 1089
    iget-object v2, p0, Lcom/helpshift/conversation/viewmodel/MessageListVM$4;->this$0:Lcom/helpshift/conversation/viewmodel/MessageListVM;

    iget-object v2, v2, Lcom/helpshift/conversation/viewmodel/MessageListVM;->messageListVMCallback:Lcom/helpshift/conversation/viewmodel/MessageListVMCallback;

    if-eqz v2, :cond_3

    .line 1090
    iget-object v2, p0, Lcom/helpshift/conversation/viewmodel/MessageListVM$4;->this$0:Lcom/helpshift/conversation/viewmodel/MessageListVM;

    iget-object v2, v2, Lcom/helpshift/conversation/viewmodel/MessageListVM;->messageListVMCallback:Lcom/helpshift/conversation/viewmodel/MessageListVMCallback;

    invoke-interface {v0}, Ljava/util/List;->size()I

    move-result v0

    invoke-interface {v2, v1, v0}, Lcom/helpshift/conversation/viewmodel/MessageListVMCallback;->appendMessages(II)V

    :cond_3
    return-void
.end method
