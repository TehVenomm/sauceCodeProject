.class Lcom/helpshift/account/domainmodel/UserSetupDM$2;
.super Lcom/helpshift/common/domain/F;
.source "UserSetupDM.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lcom/helpshift/account/domainmodel/UserSetupDM;->updateUserSetupStateChange(Lcom/helpshift/account/domainmodel/UserSetupState;)V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lcom/helpshift/account/domainmodel/UserSetupDM;


# direct methods
.method constructor <init>(Lcom/helpshift/account/domainmodel/UserSetupDM;)V
    .locals 0

    .line 207
    iput-object p1, p0, Lcom/helpshift/account/domainmodel/UserSetupDM$2;->this$0:Lcom/helpshift/account/domainmodel/UserSetupDM;

    invoke-direct {p0}, Lcom/helpshift/common/domain/F;-><init>()V

    return-void
.end method


# virtual methods
.method public f()V
    .locals 4

    const/4 v0, 0x0

    .line 213
    :try_start_0
    iget-object v1, p0, Lcom/helpshift/account/domainmodel/UserSetupDM$2;->this$0:Lcom/helpshift/account/domainmodel/UserSetupDM;

    iget-object v1, v1, Lcom/helpshift/account/domainmodel/UserSetupDM;->domain:Lcom/helpshift/common/domain/Domain;

    invoke-virtual {v1}, Lcom/helpshift/common/domain/Domain;->getUserManagerDM()Lcom/helpshift/account/domainmodel/UserManagerDM;

    move-result-object v1

    invoke-virtual {v1}, Lcom/helpshift/account/domainmodel/UserManagerDM;->sendPushTokenSync()V
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    .line 217
    iget-object v1, p0, Lcom/helpshift/account/domainmodel/UserSetupDM$2;->this$0:Lcom/helpshift/account/domainmodel/UserSetupDM;

    iget-object v1, v1, Lcom/helpshift/account/domainmodel/UserSetupDM;->domain:Lcom/helpshift/common/domain/Domain;

    .line 218
    invoke-virtual {v1}, Lcom/helpshift/common/domain/Domain;->getConversationInboxManagerDM()Lcom/helpshift/conversation/domainmodel/ConversationInboxManagerDM;

    move-result-object v1

    invoke-virtual {v1}, Lcom/helpshift/conversation/domainmodel/ConversationInboxManagerDM;->getActiveConversationInboxDM()Lcom/helpshift/conversation/domainmodel/ConversationController;

    move-result-object v1

    invoke-virtual {v1}, Lcom/helpshift/conversation/domainmodel/ConversationController;->getConversationInboxPoller()Lcom/helpshift/conversation/ConversationInboxPoller;

    move-result-object v1

    .line 220
    iget-object v2, p0, Lcom/helpshift/account/domainmodel/UserSetupDM$2;->this$0:Lcom/helpshift/account/domainmodel/UserSetupDM;

    iget-object v2, v2, Lcom/helpshift/account/domainmodel/UserSetupDM;->userDM:Lcom/helpshift/account/domainmodel/UserDM;

    invoke-virtual {v2, v1}, Lcom/helpshift/account/domainmodel/UserDM;->addObserver(Ljava/util/Observer;)V

    .line 223
    invoke-virtual {v1, v0}, Lcom/helpshift/conversation/ConversationInboxPoller;->refreshPoller(Z)V

    return-void

    :catchall_0
    move-exception v1

    .line 217
    iget-object v2, p0, Lcom/helpshift/account/domainmodel/UserSetupDM$2;->this$0:Lcom/helpshift/account/domainmodel/UserSetupDM;

    iget-object v2, v2, Lcom/helpshift/account/domainmodel/UserSetupDM;->domain:Lcom/helpshift/common/domain/Domain;

    .line 218
    invoke-virtual {v2}, Lcom/helpshift/common/domain/Domain;->getConversationInboxManagerDM()Lcom/helpshift/conversation/domainmodel/ConversationInboxManagerDM;

    move-result-object v2

    invoke-virtual {v2}, Lcom/helpshift/conversation/domainmodel/ConversationInboxManagerDM;->getActiveConversationInboxDM()Lcom/helpshift/conversation/domainmodel/ConversationController;

    move-result-object v2

    invoke-virtual {v2}, Lcom/helpshift/conversation/domainmodel/ConversationController;->getConversationInboxPoller()Lcom/helpshift/conversation/ConversationInboxPoller;

    move-result-object v2

    .line 220
    iget-object v3, p0, Lcom/helpshift/account/domainmodel/UserSetupDM$2;->this$0:Lcom/helpshift/account/domainmodel/UserSetupDM;

    iget-object v3, v3, Lcom/helpshift/account/domainmodel/UserSetupDM;->userDM:Lcom/helpshift/account/domainmodel/UserDM;

    invoke-virtual {v3, v2}, Lcom/helpshift/account/domainmodel/UserDM;->addObserver(Ljava/util/Observer;)V

    .line 223
    invoke-virtual {v2, v0}, Lcom/helpshift/conversation/ConversationInboxPoller;->refreshPoller(Z)V

    .line 224
    throw v1
.end method
