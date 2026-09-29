.class Lcom/helpshift/conversation/viewmodel/ConversationalVM$21;
.super Lcom/helpshift/common/domain/F;
.source "ConversationalVM.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lcom/helpshift/conversation/viewmodel/ConversationalVM;->handleAdminSuggestedQuestionRead(Lcom/helpshift/conversation/activeconversation/message/MessageDM;Ljava/lang/String;Ljava/lang/String;)V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lcom/helpshift/conversation/viewmodel/ConversationalVM;

.field final synthetic val$conversationLocalId:Ljava/lang/Long;

.field final synthetic val$messageServerId:Ljava/lang/String;

.field final synthetic val$questionPublishId:Ljava/lang/String;

.field final synthetic val$questionServerId:Ljava/lang/String;


# direct methods
.method constructor <init>(Lcom/helpshift/conversation/viewmodel/ConversationalVM;Ljava/lang/Long;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V
    .locals 0

    .line 1346
    iput-object p1, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM$21;->this$0:Lcom/helpshift/conversation/viewmodel/ConversationalVM;

    iput-object p2, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM$21;->val$conversationLocalId:Ljava/lang/Long;

    iput-object p3, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM$21;->val$messageServerId:Ljava/lang/String;

    iput-object p4, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM$21;->val$questionServerId:Ljava/lang/String;

    iput-object p5, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM$21;->val$questionPublishId:Ljava/lang/String;

    invoke-direct {p0}, Lcom/helpshift/common/domain/F;-><init>()V

    return-void
.end method


# virtual methods
.method public f()V
    .locals 5

    .line 1349
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM$21;->this$0:Lcom/helpshift/conversation/viewmodel/ConversationalVM;

    iget-object v0, v0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->viewableConversation:Lcom/helpshift/conversation/activeconversation/ViewableConversation;

    invoke-virtual {v0}, Lcom/helpshift/conversation/activeconversation/ViewableConversation;->getAllConversations()Ljava/util/List;

    move-result-object v0

    .line 1351
    invoke-interface {v0}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v0

    :cond_0
    invoke-interface {v0}, Ljava/util/Iterator;->hasNext()Z

    move-result v1

    if-eqz v1, :cond_1

    invoke-interface {v0}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Lcom/helpshift/conversation/activeconversation/model/Conversation;

    .line 1352
    iget-object v2, v1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->localId:Ljava/lang/Long;

    iget-object v3, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM$21;->val$conversationLocalId:Ljava/lang/Long;

    invoke-virtual {v2, v3}, Ljava/lang/Long;->equals(Ljava/lang/Object;)Z

    move-result v2

    if-eqz v2, :cond_0

    goto :goto_0

    :cond_1
    const/4 v1, 0x0

    :goto_0
    if-eqz v1, :cond_2

    .line 1358
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM$21;->this$0:Lcom/helpshift/conversation/viewmodel/ConversationalVM;

    iget-object v0, v0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->conversationManager:Lcom/helpshift/conversation/activeconversation/ConversationManager;

    iget-object v2, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM$21;->val$messageServerId:Ljava/lang/String;

    iget-object v3, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM$21;->val$questionServerId:Ljava/lang/String;

    iget-object v4, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM$21;->val$questionPublishId:Ljava/lang/String;

    invoke-virtual {v0, v1, v2, v3, v4}, Lcom/helpshift/conversation/activeconversation/ConversationManager;->handleAdminSuggestedQuestionRead(Lcom/helpshift/conversation/activeconversation/model/Conversation;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V

    :cond_2
    return-void
.end method
