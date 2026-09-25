.class Lcom/helpshift/conversation/viewmodel/NewConversationVM$13;
.super Lcom/helpshift/common/domain/F;
.source "NewConversationVM.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lcom/helpshift/conversation/viewmodel/NewConversationVM;->setConversationViewState(I)V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lcom/helpshift/conversation/viewmodel/NewConversationVM;

.field final synthetic val$conversationViewState:I


# direct methods
.method constructor <init>(Lcom/helpshift/conversation/viewmodel/NewConversationVM;I)V
    .locals 0

    .line 385
    iput-object p1, p0, Lcom/helpshift/conversation/viewmodel/NewConversationVM$13;->this$0:Lcom/helpshift/conversation/viewmodel/NewConversationVM;

    iput p2, p0, Lcom/helpshift/conversation/viewmodel/NewConversationVM$13;->val$conversationViewState:I

    invoke-direct {p0}, Lcom/helpshift/common/domain/F;-><init>()V

    return-void
.end method


# virtual methods
.method public f()V
    .locals 2

    .line 388
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/NewConversationVM$13;->this$0:Lcom/helpshift/conversation/viewmodel/NewConversationVM;

    iget-object v0, v0, Lcom/helpshift/conversation/viewmodel/NewConversationVM;->conversationController:Lcom/helpshift/conversation/domainmodel/ConversationController;

    iget v1, p0, Lcom/helpshift/conversation/viewmodel/NewConversationVM$13;->val$conversationViewState:I

    invoke-virtual {v0, v1}, Lcom/helpshift/conversation/domainmodel/ConversationController;->setConversationViewState(I)V

    return-void
.end method
