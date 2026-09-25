.class Lcom/helpshift/conversation/viewmodel/ConversationalVM$1;
.super Lcom/helpshift/common/domain/F;
.source "ConversationalVM.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lcom/helpshift/conversation/viewmodel/ConversationalVM;->markMessagesAsSeenOnExit()V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lcom/helpshift/conversation/viewmodel/ConversationalVM;

.field final synthetic val$conversations:Ljava/util/List;


# direct methods
.method constructor <init>(Lcom/helpshift/conversation/viewmodel/ConversationalVM;Ljava/util/List;)V
    .locals 0

    .line 253
    iput-object p1, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM$1;->this$0:Lcom/helpshift/conversation/viewmodel/ConversationalVM;

    iput-object p2, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM$1;->val$conversations:Ljava/util/List;

    invoke-direct {p0}, Lcom/helpshift/common/domain/F;-><init>()V

    return-void
.end method


# virtual methods
.method public f()V
    .locals 3

    .line 261
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM$1;->val$conversations:Ljava/util/List;

    invoke-interface {v0}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v0

    :goto_0
    invoke-interface {v0}, Ljava/util/Iterator;->hasNext()Z

    move-result v1

    if-eqz v1, :cond_0

    invoke-interface {v0}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Lcom/helpshift/conversation/activeconversation/model/Conversation;

    .line 262
    iget-object v2, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM$1;->this$0:Lcom/helpshift/conversation/viewmodel/ConversationalVM;

    iget-object v2, v2, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->conversationManager:Lcom/helpshift/conversation/activeconversation/ConversationManager;

    invoke-virtual {v2, v1}, Lcom/helpshift/conversation/activeconversation/ConversationManager;->markMessagesAsSeen(Lcom/helpshift/conversation/activeconversation/model/Conversation;)V

    goto :goto_0

    :cond_0
    return-void
.end method
