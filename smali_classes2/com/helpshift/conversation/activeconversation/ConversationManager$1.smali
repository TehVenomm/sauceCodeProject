.class Lcom/helpshift/conversation/activeconversation/ConversationManager$1;
.super Lcom/helpshift/common/domain/F;
.source "ConversationManager.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lcom/helpshift/conversation/activeconversation/ConversationManager;->checkForReOpen(Lcom/helpshift/conversation/activeconversation/model/Conversation;ILjava/lang/String;Z)Z
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lcom/helpshift/conversation/activeconversation/ConversationManager;

.field final synthetic val$conversation:Lcom/helpshift/conversation/activeconversation/model/Conversation;

.field final synthetic val$followupAcceptedMessageDM:Lcom/helpshift/conversation/activeconversation/message/FollowupAcceptedMessageDM;


# direct methods
.method constructor <init>(Lcom/helpshift/conversation/activeconversation/ConversationManager;Lcom/helpshift/conversation/activeconversation/message/FollowupAcceptedMessageDM;Lcom/helpshift/conversation/activeconversation/model/Conversation;)V
    .locals 0

    .line 179
    iput-object p1, p0, Lcom/helpshift/conversation/activeconversation/ConversationManager$1;->this$0:Lcom/helpshift/conversation/activeconversation/ConversationManager;

    iput-object p2, p0, Lcom/helpshift/conversation/activeconversation/ConversationManager$1;->val$followupAcceptedMessageDM:Lcom/helpshift/conversation/activeconversation/message/FollowupAcceptedMessageDM;

    iput-object p3, p0, Lcom/helpshift/conversation/activeconversation/ConversationManager$1;->val$conversation:Lcom/helpshift/conversation/activeconversation/model/Conversation;

    invoke-direct {p0}, Lcom/helpshift/common/domain/F;-><init>()V

    return-void
.end method


# virtual methods
.method public f()V
    .locals 3

    .line 182
    iget-object v0, p0, Lcom/helpshift/conversation/activeconversation/ConversationManager$1;->val$followupAcceptedMessageDM:Lcom/helpshift/conversation/activeconversation/message/FollowupAcceptedMessageDM;

    iget-object v1, p0, Lcom/helpshift/conversation/activeconversation/ConversationManager$1;->this$0:Lcom/helpshift/conversation/activeconversation/ConversationManager;

    iget-object v1, v1, Lcom/helpshift/conversation/activeconversation/ConversationManager;->userDM:Lcom/helpshift/account/domainmodel/UserDM;

    iget-object v2, p0, Lcom/helpshift/conversation/activeconversation/ConversationManager$1;->val$conversation:Lcom/helpshift/conversation/activeconversation/model/Conversation;

    invoke-virtual {v0, v1, v2}, Lcom/helpshift/conversation/activeconversation/message/FollowupAcceptedMessageDM;->send(Lcom/helpshift/account/domainmodel/UserDM;Lcom/helpshift/conversation/activeconversation/ConversationServerInfo;)V

    return-void
.end method
