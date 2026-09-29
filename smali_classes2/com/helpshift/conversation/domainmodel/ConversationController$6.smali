.class Lcom/helpshift/conversation/domainmodel/ConversationController$6;
.super Lcom/helpshift/common/domain/F;
.source "ConversationController.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lcom/helpshift/conversation/domainmodel/ConversationController;->deleteCachedFilesForResolvedConversations()V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lcom/helpshift/conversation/domainmodel/ConversationController;


# direct methods
.method constructor <init>(Lcom/helpshift/conversation/domainmodel/ConversationController;)V
    .locals 0

    .line 1761
    iput-object p1, p0, Lcom/helpshift/conversation/domainmodel/ConversationController$6;->this$0:Lcom/helpshift/conversation/domainmodel/ConversationController;

    invoke-direct {p0}, Lcom/helpshift/common/domain/F;-><init>()V

    return-void
.end method


# virtual methods
.method public f()V
    .locals 4

    .line 1764
    iget-object v0, p0, Lcom/helpshift/conversation/domainmodel/ConversationController$6;->this$0:Lcom/helpshift/conversation/domainmodel/ConversationController;

    iget-object v0, v0, Lcom/helpshift/conversation/domainmodel/ConversationController;->conversationDAO:Lcom/helpshift/conversation/dao/ConversationDAO;

    iget-object v1, p0, Lcom/helpshift/conversation/domainmodel/ConversationController$6;->this$0:Lcom/helpshift/conversation/domainmodel/ConversationController;

    iget-object v1, v1, Lcom/helpshift/conversation/domainmodel/ConversationController;->userDM:Lcom/helpshift/account/domainmodel/UserDM;

    .line 1765
    invoke-virtual {v1}, Lcom/helpshift/account/domainmodel/UserDM;->getLocalId()Ljava/lang/Long;

    move-result-object v1

    invoke-virtual {v1}, Ljava/lang/Long;->longValue()J

    move-result-wide v1

    invoke-interface {v0, v1, v2}, Lcom/helpshift/conversation/dao/ConversationDAO;->readConversationsWithoutMessages(J)Ljava/util/List;

    move-result-object v0

    .line 1766
    invoke-interface {v0}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v0

    :cond_0
    :goto_0
    invoke-interface {v0}, Ljava/util/Iterator;->hasNext()Z

    move-result v1

    if-eqz v1, :cond_1

    invoke-interface {v0}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Lcom/helpshift/conversation/activeconversation/model/Conversation;

    .line 1767
    iget-object v2, p0, Lcom/helpshift/conversation/domainmodel/ConversationController$6;->this$0:Lcom/helpshift/conversation/domainmodel/ConversationController;

    iget-object v2, v2, Lcom/helpshift/conversation/domainmodel/ConversationController;->userDM:Lcom/helpshift/account/domainmodel/UserDM;

    invoke-virtual {v2}, Lcom/helpshift/account/domainmodel/UserDM;->getLocalId()Ljava/lang/Long;

    move-result-object v2

    invoke-virtual {v2}, Ljava/lang/Long;->longValue()J

    move-result-wide v2

    iput-wide v2, v1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->userLocalId:J

    .line 1768
    iget-object v2, p0, Lcom/helpshift/conversation/domainmodel/ConversationController$6;->this$0:Lcom/helpshift/conversation/domainmodel/ConversationController;

    iget-object v2, v2, Lcom/helpshift/conversation/domainmodel/ConversationController;->conversationManager:Lcom/helpshift/conversation/activeconversation/ConversationManager;

    invoke-virtual {v2, v1}, Lcom/helpshift/conversation/activeconversation/ConversationManager;->shouldOpen(Lcom/helpshift/conversation/activeconversation/model/Conversation;)Z

    move-result v2

    if-nez v2, :cond_0

    .line 1769
    iget-object v2, p0, Lcom/helpshift/conversation/domainmodel/ConversationController$6;->this$0:Lcom/helpshift/conversation/domainmodel/ConversationController;

    iget-object v2, v2, Lcom/helpshift/conversation/domainmodel/ConversationController;->conversationManager:Lcom/helpshift/conversation/activeconversation/ConversationManager;

    invoke-virtual {v2, v1}, Lcom/helpshift/conversation/activeconversation/ConversationManager;->deleteCachedScreenshotFiles(Lcom/helpshift/conversation/activeconversation/model/Conversation;)V

    goto :goto_0

    :cond_1
    return-void
.end method
