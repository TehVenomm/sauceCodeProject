.class Lcom/helpshift/conversation/activeconversation/ConversationManager$4;
.super Lcom/helpshift/common/domain/F;
.source "ConversationManager.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lcom/helpshift/conversation/activeconversation/ConversationManager;->markConversationResolutionStatus(Lcom/helpshift/conversation/activeconversation/model/Conversation;Z)V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lcom/helpshift/conversation/activeconversation/ConversationManager;

.field final synthetic val$conversation:Lcom/helpshift/conversation/activeconversation/model/Conversation;

.field final synthetic val$messageDM:Lcom/helpshift/conversation/activeconversation/message/ConfirmationAcceptedMessageDM;


# direct methods
.method constructor <init>(Lcom/helpshift/conversation/activeconversation/ConversationManager;Lcom/helpshift/conversation/activeconversation/message/ConfirmationAcceptedMessageDM;Lcom/helpshift/conversation/activeconversation/model/Conversation;)V
    .locals 0

    .line 335
    iput-object p1, p0, Lcom/helpshift/conversation/activeconversation/ConversationManager$4;->this$0:Lcom/helpshift/conversation/activeconversation/ConversationManager;

    iput-object p2, p0, Lcom/helpshift/conversation/activeconversation/ConversationManager$4;->val$messageDM:Lcom/helpshift/conversation/activeconversation/message/ConfirmationAcceptedMessageDM;

    iput-object p3, p0, Lcom/helpshift/conversation/activeconversation/ConversationManager$4;->val$conversation:Lcom/helpshift/conversation/activeconversation/model/Conversation;

    invoke-direct {p0}, Lcom/helpshift/common/domain/F;-><init>()V

    return-void
.end method


# virtual methods
.method public f()V
    .locals 3

    .line 339
    :try_start_0
    iget-object v0, p0, Lcom/helpshift/conversation/activeconversation/ConversationManager$4;->val$messageDM:Lcom/helpshift/conversation/activeconversation/message/ConfirmationAcceptedMessageDM;

    iget-object v1, p0, Lcom/helpshift/conversation/activeconversation/ConversationManager$4;->this$0:Lcom/helpshift/conversation/activeconversation/ConversationManager;

    iget-object v1, v1, Lcom/helpshift/conversation/activeconversation/ConversationManager;->userDM:Lcom/helpshift/account/domainmodel/UserDM;

    iget-object v2, p0, Lcom/helpshift/conversation/activeconversation/ConversationManager$4;->val$conversation:Lcom/helpshift/conversation/activeconversation/model/Conversation;

    invoke-virtual {v0, v1, v2}, Lcom/helpshift/conversation/activeconversation/message/ConfirmationAcceptedMessageDM;->send(Lcom/helpshift/account/domainmodel/UserDM;Lcom/helpshift/conversation/activeconversation/ConversationServerInfo;)V
    :try_end_0
    .catch Lcom/helpshift/common/exception/RootAPIException; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception v0

    .line 342
    iget-object v1, v0, Lcom/helpshift/common/exception/RootAPIException;->exceptionType:Lcom/helpshift/common/exception/ExceptionType;

    sget-object v2, Lcom/helpshift/common/exception/NetworkException;->CONVERSATION_ARCHIVED:Lcom/helpshift/common/exception/NetworkException;

    if-ne v1, v2, :cond_0

    .line 343
    iget-object v0, p0, Lcom/helpshift/conversation/activeconversation/ConversationManager$4;->this$0:Lcom/helpshift/conversation/activeconversation/ConversationManager;

    iget-object v1, p0, Lcom/helpshift/conversation/activeconversation/ConversationManager$4;->val$conversation:Lcom/helpshift/conversation/activeconversation/model/Conversation;

    sget-object v2, Lcom/helpshift/conversation/dto/IssueState;->ARCHIVED:Lcom/helpshift/conversation/dto/IssueState;

    invoke-virtual {v0, v1, v2}, Lcom/helpshift/conversation/activeconversation/ConversationManager;->updateIssueStatus(Lcom/helpshift/conversation/activeconversation/model/Conversation;Lcom/helpshift/conversation/dto/IssueState;)V

    :goto_0
    return-void

    .line 346
    :cond_0
    throw v0
.end method
