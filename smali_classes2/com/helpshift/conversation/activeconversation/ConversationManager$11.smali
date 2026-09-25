.class Lcom/helpshift/conversation/activeconversation/ConversationManager$11;
.super Lcom/helpshift/common/domain/F;
.source "ConversationManager.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lcom/helpshift/conversation/activeconversation/ConversationManager;->handleAdminSuggestedQuestionRead(Lcom/helpshift/conversation/activeconversation/model/Conversation;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lcom/helpshift/conversation/activeconversation/ConversationManager;

.field final synthetic val$conversation:Lcom/helpshift/conversation/activeconversation/model/Conversation;

.field final synthetic val$messageDM:Lcom/helpshift/conversation/activeconversation/message/MessageDM;

.field final synthetic val$questionPublishId:Ljava/lang/String;

.field final synthetic val$questionServerId:Ljava/lang/String;


# direct methods
.method constructor <init>(Lcom/helpshift/conversation/activeconversation/ConversationManager;Lcom/helpshift/conversation/activeconversation/message/MessageDM;Lcom/helpshift/conversation/activeconversation/model/Conversation;Ljava/lang/String;Ljava/lang/String;)V
    .locals 0

    .line 1707
    iput-object p1, p0, Lcom/helpshift/conversation/activeconversation/ConversationManager$11;->this$0:Lcom/helpshift/conversation/activeconversation/ConversationManager;

    iput-object p2, p0, Lcom/helpshift/conversation/activeconversation/ConversationManager$11;->val$messageDM:Lcom/helpshift/conversation/activeconversation/message/MessageDM;

    iput-object p3, p0, Lcom/helpshift/conversation/activeconversation/ConversationManager$11;->val$conversation:Lcom/helpshift/conversation/activeconversation/model/Conversation;

    iput-object p4, p0, Lcom/helpshift/conversation/activeconversation/ConversationManager$11;->val$questionServerId:Ljava/lang/String;

    iput-object p5, p0, Lcom/helpshift/conversation/activeconversation/ConversationManager$11;->val$questionPublishId:Ljava/lang/String;

    invoke-direct {p0}, Lcom/helpshift/common/domain/F;-><init>()V

    return-void
.end method


# virtual methods
.method public f()V
    .locals 5

    .line 1710
    iget-object v0, p0, Lcom/helpshift/conversation/activeconversation/ConversationManager$11;->val$messageDM:Lcom/helpshift/conversation/activeconversation/message/MessageDM;

    check-cast v0, Lcom/helpshift/conversation/activeconversation/message/FAQListMessageDM;

    iget-object v1, p0, Lcom/helpshift/conversation/activeconversation/ConversationManager$11;->val$conversation:Lcom/helpshift/conversation/activeconversation/model/Conversation;

    iget-object v2, p0, Lcom/helpshift/conversation/activeconversation/ConversationManager$11;->this$0:Lcom/helpshift/conversation/activeconversation/ConversationManager;

    iget-object v2, v2, Lcom/helpshift/conversation/activeconversation/ConversationManager;->userDM:Lcom/helpshift/account/domainmodel/UserDM;

    iget-object v3, p0, Lcom/helpshift/conversation/activeconversation/ConversationManager$11;->val$questionServerId:Ljava/lang/String;

    iget-object v4, p0, Lcom/helpshift/conversation/activeconversation/ConversationManager$11;->val$questionPublishId:Ljava/lang/String;

    invoke-virtual {v0, v1, v2, v3, v4}, Lcom/helpshift/conversation/activeconversation/message/FAQListMessageDM;->handleSuggestionClick(Lcom/helpshift/conversation/activeconversation/ConversationServerInfo;Lcom/helpshift/account/domainmodel/UserDM;Ljava/lang/String;Ljava/lang/String;)V

    return-void
.end method
