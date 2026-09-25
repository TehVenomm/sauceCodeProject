.class Lcom/helpshift/conversation/viewmodel/MessageListVM$6;
.super Lcom/helpshift/common/domain/F;
.source "MessageListVM.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lcom/helpshift/conversation/viewmodel/MessageListVM;->remove(Ljava/util/List;)V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lcom/helpshift/conversation/viewmodel/MessageListVM;

.field final synthetic val$filteredMessages:Ljava/util/List;


# direct methods
.method constructor <init>(Lcom/helpshift/conversation/viewmodel/MessageListVM;Ljava/util/List;)V
    .locals 0

    .line 1193
    iput-object p1, p0, Lcom/helpshift/conversation/viewmodel/MessageListVM$6;->this$0:Lcom/helpshift/conversation/viewmodel/MessageListVM;

    iput-object p2, p0, Lcom/helpshift/conversation/viewmodel/MessageListVM$6;->val$filteredMessages:Ljava/util/List;

    invoke-direct {p0}, Lcom/helpshift/common/domain/F;-><init>()V

    return-void
.end method


# virtual methods
.method public f()V
    .locals 5

    .line 1198
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/MessageListVM$6;->val$filteredMessages:Ljava/util/List;

    invoke-interface {v0}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v0

    const/4 v1, 0x0

    :goto_0
    invoke-interface {v0}, Ljava/util/Iterator;->hasNext()Z

    move-result v2

    if-eqz v2, :cond_1

    invoke-interface {v0}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Lcom/helpshift/conversation/activeconversation/message/MessageDM;

    .line 1199
    iget-object v3, p0, Lcom/helpshift/conversation/viewmodel/MessageListVM$6;->this$0:Lcom/helpshift/conversation/viewmodel/MessageListVM;

    iget-object v3, v3, Lcom/helpshift/conversation/viewmodel/MessageListVM;->uiMessageDMs:Ljava/util/List;

    invoke-interface {v3, v2}, Ljava/util/List;->indexOf(Ljava/lang/Object;)I

    move-result v2

    const/4 v3, -0x1

    if-ne v2, v3, :cond_0

    goto :goto_0

    .line 1209
    :cond_0
    iget-object v1, p0, Lcom/helpshift/conversation/viewmodel/MessageListVM$6;->this$0:Lcom/helpshift/conversation/viewmodel/MessageListVM;

    iget-object v1, v1, Lcom/helpshift/conversation/viewmodel/MessageListVM;->uiMessageDMs:Ljava/util/List;

    invoke-interface {v1, v2}, Ljava/util/List;->remove(I)Ljava/lang/Object;

    .line 1210
    iget-object v1, p0, Lcom/helpshift/conversation/viewmodel/MessageListVM$6;->this$0:Lcom/helpshift/conversation/viewmodel/MessageListVM;

    add-int/lit8 v3, v2, -0x1

    invoke-virtual {v1, v3}, Lcom/helpshift/conversation/viewmodel/MessageListVM;->checkAndUpdateGroupByDate(I)Z

    .line 1211
    iget-object v1, p0, Lcom/helpshift/conversation/viewmodel/MessageListVM$6;->this$0:Lcom/helpshift/conversation/viewmodel/MessageListVM;

    iget-object v4, p0, Lcom/helpshift/conversation/viewmodel/MessageListVM$6;->this$0:Lcom/helpshift/conversation/viewmodel/MessageListVM;

    iget-object v4, v4, Lcom/helpshift/conversation/viewmodel/MessageListVM;->uiMessageDMs:Ljava/util/List;

    add-int/lit8 v2, v2, 0x1

    invoke-virtual {v1, v4, v3, v2}, Lcom/helpshift/conversation/viewmodel/MessageListVM;->checkAndUpdateGroupByTime(Ljava/util/List;II)Lcom/helpshift/util/ValuePair;

    const/4 v1, 0x1

    goto :goto_0

    :cond_1
    if-eqz v1, :cond_2

    .line 1216
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/MessageListVM$6;->this$0:Lcom/helpshift/conversation/viewmodel/MessageListVM;

    invoke-virtual {v0}, Lcom/helpshift/conversation/viewmodel/MessageListVM;->notifyMessageListVMRefreshAll()V

    .line 1217
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/MessageListVM$6;->this$0:Lcom/helpshift/conversation/viewmodel/MessageListVM;

    invoke-virtual {v0}, Lcom/helpshift/conversation/viewmodel/MessageListVM;->notifyUIMessageListUpdated()V

    :cond_2
    return-void
.end method
