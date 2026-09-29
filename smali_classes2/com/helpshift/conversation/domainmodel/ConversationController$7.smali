.class Lcom/helpshift/conversation/domainmodel/ConversationController$7;
.super Lcom/helpshift/common/domain/F;
.source "ConversationController.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lcom/helpshift/conversation/domainmodel/ConversationController;->resetPreIssueConversationsForUser(Lcom/helpshift/account/domainmodel/UserDM;)V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lcom/helpshift/conversation/domainmodel/ConversationController;

.field final synthetic val$conversation:Lcom/helpshift/conversation/activeconversation/model/Conversation;

.field final synthetic val$userDM:Lcom/helpshift/account/domainmodel/UserDM;


# direct methods
.method constructor <init>(Lcom/helpshift/conversation/domainmodel/ConversationController;Lcom/helpshift/conversation/activeconversation/model/Conversation;Lcom/helpshift/account/domainmodel/UserDM;)V
    .locals 0

    .line 1808
    iput-object p1, p0, Lcom/helpshift/conversation/domainmodel/ConversationController$7;->this$0:Lcom/helpshift/conversation/domainmodel/ConversationController;

    iput-object p2, p0, Lcom/helpshift/conversation/domainmodel/ConversationController$7;->val$conversation:Lcom/helpshift/conversation/activeconversation/model/Conversation;

    iput-object p3, p0, Lcom/helpshift/conversation/domainmodel/ConversationController$7;->val$userDM:Lcom/helpshift/account/domainmodel/UserDM;

    invoke-direct {p0}, Lcom/helpshift/common/domain/F;-><init>()V

    return-void
.end method


# virtual methods
.method public f()V
    .locals 5

    :try_start_0
    const-string v0, "Helpshift_ConvInboxDM"

    .line 1813
    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "Reseting preissue on backend: "

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v2, p0, Lcom/helpshift/conversation/domainmodel/ConversationController$7;->val$conversation:Lcom/helpshift/conversation/activeconversation/model/Conversation;

    iget-object v2, v2, Lcom/helpshift/conversation/activeconversation/model/Conversation;->preConversationServerId:Ljava/lang/String;

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-static {v0, v1}, Lcom/helpshift/util/HSLogger;->d(Ljava/lang/String;Ljava/lang/String;)V

    .line 1814
    iget-object v0, p0, Lcom/helpshift/conversation/domainmodel/ConversationController$7;->val$userDM:Lcom/helpshift/account/domainmodel/UserDM;

    invoke-static {v0}, Lcom/helpshift/common/domain/network/NetworkDataRequestUtil;->getUserRequestData(Lcom/helpshift/account/domainmodel/UserDM;)Ljava/util/HashMap;

    move-result-object v0

    const-string v1, "state"

    .line 1815
    sget-object v2, Lcom/helpshift/conversation/dto/IssueState;->REJECTED:Lcom/helpshift/conversation/dto/IssueState;

    invoke-virtual {v2}, Lcom/helpshift/conversation/dto/IssueState;->getValue()I

    move-result v2

    invoke-static {v2}, Ljava/lang/String;->valueOf(I)Ljava/lang/String;

    move-result-object v2

    invoke-interface {v0, v1, v2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 1818
    new-instance v1, Lcom/helpshift/common/domain/network/PUTNetwork;

    new-instance v2, Ljava/lang/StringBuilder;

    invoke-direct {v2}, Ljava/lang/StringBuilder;-><init>()V

    const-string v3, "/preissues/"

    invoke-virtual {v2, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v3, p0, Lcom/helpshift/conversation/domainmodel/ConversationController$7;->val$conversation:Lcom/helpshift/conversation/activeconversation/model/Conversation;

    iget-object v3, v3, Lcom/helpshift/conversation/activeconversation/model/Conversation;->preConversationServerId:Ljava/lang/String;

    invoke-virtual {v2, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v3, "/"

    invoke-virtual {v2, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v2}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v2

    iget-object v3, p0, Lcom/helpshift/conversation/domainmodel/ConversationController$7;->this$0:Lcom/helpshift/conversation/domainmodel/ConversationController;

    iget-object v3, v3, Lcom/helpshift/conversation/domainmodel/ConversationController;->domain:Lcom/helpshift/common/domain/Domain;

    iget-object v4, p0, Lcom/helpshift/conversation/domainmodel/ConversationController$7;->this$0:Lcom/helpshift/conversation/domainmodel/ConversationController;

    iget-object v4, v4, Lcom/helpshift/conversation/domainmodel/ConversationController;->platform:Lcom/helpshift/common/platform/Platform;

    invoke-direct {v1, v2, v3, v4}, Lcom/helpshift/common/domain/network/PUTNetwork;-><init>(Ljava/lang/String;Lcom/helpshift/common/domain/Domain;Lcom/helpshift/common/platform/Platform;)V

    .line 1820
    new-instance v2, Lcom/helpshift/common/domain/network/TSCorrectedNetwork;

    iget-object v3, p0, Lcom/helpshift/conversation/domainmodel/ConversationController$7;->this$0:Lcom/helpshift/conversation/domainmodel/ConversationController;

    iget-object v3, v3, Lcom/helpshift/conversation/domainmodel/ConversationController;->platform:Lcom/helpshift/common/platform/Platform;

    invoke-direct {v2, v1, v3}, Lcom/helpshift/common/domain/network/TSCorrectedNetwork;-><init>(Lcom/helpshift/common/domain/network/Network;Lcom/helpshift/common/platform/Platform;)V

    .line 1821
    new-instance v1, Lcom/helpshift/common/domain/network/GuardOKNetwork;

    invoke-direct {v1, v2}, Lcom/helpshift/common/domain/network/GuardOKNetwork;-><init>(Lcom/helpshift/common/domain/network/Network;)V

    .line 1824
    new-instance v2, Lcom/helpshift/common/platform/network/RequestData;

    invoke-direct {v2, v0}, Lcom/helpshift/common/platform/network/RequestData;-><init>(Ljava/util/Map;)V

    .line 1825
    invoke-interface {v1, v2}, Lcom/helpshift/common/domain/network/Network;->makeRequest(Lcom/helpshift/common/platform/network/RequestData;)Lcom/helpshift/common/platform/network/Response;

    .line 1828
    iget-object v0, p0, Lcom/helpshift/conversation/domainmodel/ConversationController$7;->this$0:Lcom/helpshift/conversation/domainmodel/ConversationController;

    iget-object v1, p0, Lcom/helpshift/conversation/domainmodel/ConversationController$7;->val$conversation:Lcom/helpshift/conversation/activeconversation/model/Conversation;

    iget-object v1, v1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->localId:Ljava/lang/Long;

    invoke-static {v0, v1}, Lcom/helpshift/conversation/domainmodel/ConversationController;->access$000(Lcom/helpshift/conversation/domainmodel/ConversationController;Ljava/lang/Long;)Lcom/helpshift/conversation/activeconversation/ViewableConversation;

    move-result-object v0

    if-nez v0, :cond_0

    .line 1832
    iget-object v0, p0, Lcom/helpshift/conversation/domainmodel/ConversationController$7;->val$conversation:Lcom/helpshift/conversation/activeconversation/model/Conversation;

    goto :goto_0

    .line 1836
    :cond_0
    invoke-virtual {v0}, Lcom/helpshift/conversation/activeconversation/ViewableConversation;->getActiveConversation()Lcom/helpshift/conversation/activeconversation/model/Conversation;

    move-result-object v0

    .line 1838
    :goto_0
    iget-object v1, p0, Lcom/helpshift/conversation/domainmodel/ConversationController$7;->this$0:Lcom/helpshift/conversation/domainmodel/ConversationController;

    iget-object v1, v1, Lcom/helpshift/conversation/domainmodel/ConversationController;->conversationManager:Lcom/helpshift/conversation/activeconversation/ConversationManager;

    sget-object v2, Lcom/helpshift/conversation/dto/IssueState;->REJECTED:Lcom/helpshift/conversation/dto/IssueState;

    invoke-virtual {v1, v0, v2}, Lcom/helpshift/conversation/activeconversation/ConversationManager;->updateIssueStatus(Lcom/helpshift/conversation/activeconversation/model/Conversation;Lcom/helpshift/conversation/dto/IssueState;)V
    :try_end_0
    .catch Lcom/helpshift/common/exception/RootAPIException; {:try_start_0 .. :try_end_0} :catch_0

    return-void

    :catch_0
    move-exception v0

    .line 1841
    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "Error resetting preissue : "

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v2, p0, Lcom/helpshift/conversation/domainmodel/ConversationController$7;->val$conversation:Lcom/helpshift/conversation/activeconversation/model/Conversation;

    iget-object v2, v2, Lcom/helpshift/conversation/activeconversation/model/Conversation;->preConversationServerId:Ljava/lang/String;

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    const-string v2, "Helpshift_ConvInboxDM"

    invoke-static {v2, v1, v0}, Lcom/helpshift/util/HSLogger;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)V

    .line 1842
    throw v0
.end method
