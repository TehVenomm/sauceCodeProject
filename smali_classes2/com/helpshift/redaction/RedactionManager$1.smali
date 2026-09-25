.class Lcom/helpshift/redaction/RedactionManager$1;
.super Lcom/helpshift/common/domain/F;
.source "RedactionManager.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lcom/helpshift/redaction/RedactionManager;->executeRedaction()V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lcom/helpshift/redaction/RedactionManager;


# direct methods
.method constructor <init>(Lcom/helpshift/redaction/RedactionManager;)V
    .locals 0

    .line 41
    iput-object p1, p0, Lcom/helpshift/redaction/RedactionManager$1;->this$0:Lcom/helpshift/redaction/RedactionManager;

    invoke-direct {p0}, Lcom/helpshift/common/domain/F;-><init>()V

    return-void
.end method


# virtual methods
.method public f()V
    .locals 3

    .line 44
    iget-object v0, p0, Lcom/helpshift/redaction/RedactionManager$1;->this$0:Lcom/helpshift/redaction/RedactionManager;

    invoke-static {v0}, Lcom/helpshift/redaction/RedactionManager;->access$000(Lcom/helpshift/redaction/RedactionManager;)Lcom/helpshift/common/domain/Domain;

    move-result-object v0

    invoke-virtual {v0}, Lcom/helpshift/common/domain/Domain;->getConversationInboxManagerDM()Lcom/helpshift/conversation/domainmodel/ConversationInboxManagerDM;

    move-result-object v0

    .line 45
    iget-object v1, p0, Lcom/helpshift/redaction/RedactionManager$1;->this$0:Lcom/helpshift/redaction/RedactionManager;

    invoke-static {v1}, Lcom/helpshift/redaction/RedactionManager;->access$100(Lcom/helpshift/redaction/RedactionManager;)Lcom/helpshift/account/domainmodel/UserDM;

    move-result-object v1

    invoke-virtual {v0, v1}, Lcom/helpshift/conversation/domainmodel/ConversationInboxManagerDM;->getConversationInboxDM(Lcom/helpshift/account/domainmodel/UserDM;)Lcom/helpshift/conversation/domainmodel/ConversationController;

    move-result-object v0

    .line 47
    invoke-virtual {v0}, Lcom/helpshift/conversation/domainmodel/ConversationController;->getConversationInboxPoller()Lcom/helpshift/conversation/ConversationInboxPoller;

    move-result-object v1

    invoke-virtual {v1}, Lcom/helpshift/conversation/ConversationInboxPoller;->stop()V

    .line 50
    invoke-virtual {v0}, Lcom/helpshift/conversation/domainmodel/ConversationController;->redactConversations()V

    .line 53
    iget-object v0, p0, Lcom/helpshift/redaction/RedactionManager$1;->this$0:Lcom/helpshift/redaction/RedactionManager;

    invoke-static {v0}, Lcom/helpshift/redaction/RedactionManager;->access$000(Lcom/helpshift/redaction/RedactionManager;)Lcom/helpshift/common/domain/Domain;

    move-result-object v0

    invoke-virtual {v0}, Lcom/helpshift/common/domain/Domain;->getUserManagerDM()Lcom/helpshift/account/domainmodel/UserManagerDM;

    move-result-object v0

    iget-object v1, p0, Lcom/helpshift/redaction/RedactionManager$1;->this$0:Lcom/helpshift/redaction/RedactionManager;

    invoke-static {v1}, Lcom/helpshift/redaction/RedactionManager;->access$100(Lcom/helpshift/redaction/RedactionManager;)Lcom/helpshift/account/domainmodel/UserDM;

    move-result-object v1

    invoke-virtual {v0, v1}, Lcom/helpshift/account/domainmodel/UserManagerDM;->resetSyncState(Lcom/helpshift/account/domainmodel/UserDM;)V

    .line 56
    iget-object v0, p0, Lcom/helpshift/redaction/RedactionManager$1;->this$0:Lcom/helpshift/redaction/RedactionManager;

    sget-object v1, Lcom/helpshift/redaction/RedactionState;->IN_PROGRESS:Lcom/helpshift/redaction/RedactionState;

    sget-object v2, Lcom/helpshift/redaction/RedactionState;->COMPLETED:Lcom/helpshift/redaction/RedactionState;

    invoke-static {v0, v1, v2}, Lcom/helpshift/redaction/RedactionManager;->access$200(Lcom/helpshift/redaction/RedactionManager;Lcom/helpshift/redaction/RedactionState;Lcom/helpshift/redaction/RedactionState;)V

    return-void
.end method
