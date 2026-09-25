.class Lcom/helpshift/conversation/viewmodel/NewConversationVM$12;
.super Lcom/helpshift/common/domain/F;
.source "NewConversationVM.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lcom/helpshift/conversation/viewmodel/NewConversationVM;->setShouldDropCustomMetadata(Z)V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lcom/helpshift/conversation/viewmodel/NewConversationVM;

.field final synthetic val$dropMetadata:Z


# direct methods
.method constructor <init>(Lcom/helpshift/conversation/viewmodel/NewConversationVM;Z)V
    .locals 0

    .line 376
    iput-object p1, p0, Lcom/helpshift/conversation/viewmodel/NewConversationVM$12;->this$0:Lcom/helpshift/conversation/viewmodel/NewConversationVM;

    iput-boolean p2, p0, Lcom/helpshift/conversation/viewmodel/NewConversationVM$12;->val$dropMetadata:Z

    invoke-direct {p0}, Lcom/helpshift/common/domain/F;-><init>()V

    return-void
.end method


# virtual methods
.method public f()V
    .locals 2

    .line 379
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/NewConversationVM$12;->this$0:Lcom/helpshift/conversation/viewmodel/NewConversationVM;

    iget-object v0, v0, Lcom/helpshift/conversation/viewmodel/NewConversationVM;->conversationController:Lcom/helpshift/conversation/domainmodel/ConversationController;

    iget-boolean v1, p0, Lcom/helpshift/conversation/viewmodel/NewConversationVM$12;->val$dropMetadata:Z

    invoke-virtual {v0, v1}, Lcom/helpshift/conversation/domainmodel/ConversationController;->setShouldDropCustomMetadata(Z)V

    return-void
.end method
