.class Lcom/helpshift/conversation/viewmodel/MessageListVM$2;
.super Lcom/helpshift/common/domain/F;
.source "MessageListVM.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lcom/helpshift/conversation/viewmodel/MessageListVM;->addMessages(Ljava/util/Collection;)V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lcom/helpshift/conversation/viewmodel/MessageListVM;

.field final synthetic val$newUISupportedMessages:Ljava/util/List;


# direct methods
.method constructor <init>(Lcom/helpshift/conversation/viewmodel/MessageListVM;Ljava/util/List;)V
    .locals 0

    .line 496
    iput-object p1, p0, Lcom/helpshift/conversation/viewmodel/MessageListVM$2;->this$0:Lcom/helpshift/conversation/viewmodel/MessageListVM;

    iput-object p2, p0, Lcom/helpshift/conversation/viewmodel/MessageListVM$2;->val$newUISupportedMessages:Ljava/util/List;

    invoke-direct {p0}, Lcom/helpshift/common/domain/F;-><init>()V

    return-void
.end method


# virtual methods
.method public f()V
    .locals 5

    .line 499
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/MessageListVM$2;->this$0:Lcom/helpshift/conversation/viewmodel/MessageListVM;

    iget-object v1, p0, Lcom/helpshift/conversation/viewmodel/MessageListVM$2;->this$0:Lcom/helpshift/conversation/viewmodel/MessageListVM;

    iget-object v1, v1, Lcom/helpshift/conversation/viewmodel/MessageListVM;->uiMessageDMs:Ljava/util/List;

    invoke-interface {v1}, Ljava/util/List;->size()I

    move-result v1

    add-int/lit8 v1, v1, -0x1

    invoke-static {v0, v1}, Lcom/helpshift/conversation/viewmodel/MessageListVM;->access$000(Lcom/helpshift/conversation/viewmodel/MessageListVM;I)Lcom/helpshift/conversation/activeconversation/message/MessageDM;

    move-result-object v0

    if-eqz v0, :cond_1

    .line 501
    invoke-virtual {v0}, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->getEpochCreatedAtTime()J

    move-result-wide v0

    iget-object v2, p0, Lcom/helpshift/conversation/viewmodel/MessageListVM$2;->val$newUISupportedMessages:Ljava/util/List;

    const/4 v3, 0x0

    invoke-interface {v2, v3}, Ljava/util/List;->get(I)Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Lcom/helpshift/conversation/activeconversation/message/MessageDM;

    invoke-virtual {v2}, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->getEpochCreatedAtTime()J

    move-result-wide v2

    cmp-long v4, v0, v2

    if-gtz v4, :cond_0

    goto :goto_0

    .line 505
    :cond_0
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/MessageListVM$2;->this$0:Lcom/helpshift/conversation/viewmodel/MessageListVM;

    iget-object v1, p0, Lcom/helpshift/conversation/viewmodel/MessageListVM$2;->val$newUISupportedMessages:Ljava/util/List;

    invoke-static {v0, v1}, Lcom/helpshift/conversation/viewmodel/MessageListVM;->access$200(Lcom/helpshift/conversation/viewmodel/MessageListVM;Ljava/util/List;)V

    goto :goto_1

    .line 502
    :cond_1
    :goto_0
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/MessageListVM$2;->this$0:Lcom/helpshift/conversation/viewmodel/MessageListVM;

    iget-object v1, p0, Lcom/helpshift/conversation/viewmodel/MessageListVM$2;->val$newUISupportedMessages:Ljava/util/List;

    invoke-static {v0, v1}, Lcom/helpshift/conversation/viewmodel/MessageListVM;->access$100(Lcom/helpshift/conversation/viewmodel/MessageListVM;Ljava/util/List;)V

    .line 507
    :goto_1
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/MessageListVM$2;->this$0:Lcom/helpshift/conversation/viewmodel/MessageListVM;

    iget-object v1, p0, Lcom/helpshift/conversation/viewmodel/MessageListVM$2;->val$newUISupportedMessages:Ljava/util/List;

    invoke-static {v0, v1}, Lcom/helpshift/conversation/viewmodel/MessageListVM;->access$300(Lcom/helpshift/conversation/viewmodel/MessageListVM;Ljava/util/List;)V

    .line 508
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/MessageListVM$2;->this$0:Lcom/helpshift/conversation/viewmodel/MessageListVM;

    invoke-virtual {v0}, Lcom/helpshift/conversation/viewmodel/MessageListVM;->notifyUIMessageListUpdated()V

    return-void
.end method
