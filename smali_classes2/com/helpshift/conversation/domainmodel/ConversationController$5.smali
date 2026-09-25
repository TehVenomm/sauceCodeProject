.class Lcom/helpshift/conversation/domainmodel/ConversationController$5;
.super Lcom/helpshift/common/domain/F;
.source "ConversationController.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lcom/helpshift/conversation/domainmodel/ConversationController;->clearNotification(Lcom/helpshift/conversation/activeconversation/model/Conversation;)V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lcom/helpshift/conversation/domainmodel/ConversationController;

.field final synthetic val$conversation:Lcom/helpshift/conversation/activeconversation/model/Conversation;


# direct methods
.method constructor <init>(Lcom/helpshift/conversation/domainmodel/ConversationController;Lcom/helpshift/conversation/activeconversation/model/Conversation;)V
    .locals 0

    .line 1607
    iput-object p1, p0, Lcom/helpshift/conversation/domainmodel/ConversationController$5;->this$0:Lcom/helpshift/conversation/domainmodel/ConversationController;

    iput-object p2, p0, Lcom/helpshift/conversation/domainmodel/ConversationController$5;->val$conversation:Lcom/helpshift/conversation/activeconversation/model/Conversation;

    invoke-direct {p0}, Lcom/helpshift/common/domain/F;-><init>()V

    return-void
.end method


# virtual methods
.method public f()V
    .locals 2

    .line 1610
    iget-object v0, p0, Lcom/helpshift/conversation/domainmodel/ConversationController$5;->this$0:Lcom/helpshift/conversation/domainmodel/ConversationController;

    iget-object v0, v0, Lcom/helpshift/conversation/domainmodel/ConversationController;->platform:Lcom/helpshift/common/platform/Platform;

    iget-object v1, p0, Lcom/helpshift/conversation/domainmodel/ConversationController$5;->val$conversation:Lcom/helpshift/conversation/activeconversation/model/Conversation;

    iget-object v1, v1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->localUUID:Ljava/lang/String;

    invoke-interface {v0, v1}, Lcom/helpshift/common/platform/Platform;->clearNotifications(Ljava/lang/String;)V

    return-void
.end method
