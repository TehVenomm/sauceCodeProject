.class Lcom/helpshift/conversation/viewmodel/ConversationalVM$14;
.super Lcom/helpshift/common/domain/F;
.source "ConversationalVM.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lcom/helpshift/conversation/viewmodel/ConversationalVM;->hideListPicker(Z)V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lcom/helpshift/conversation/viewmodel/ConversationalVM;

.field final synthetic val$hideSmoothly:Z


# direct methods
.method constructor <init>(Lcom/helpshift/conversation/viewmodel/ConversationalVM;Z)V
    .locals 0

    .line 1014
    iput-object p1, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM$14;->this$0:Lcom/helpshift/conversation/viewmodel/ConversationalVM;

    iput-boolean p2, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM$14;->val$hideSmoothly:Z

    invoke-direct {p0}, Lcom/helpshift/common/domain/F;-><init>()V

    return-void
.end method


# virtual methods
.method public f()V
    .locals 2

    .line 1017
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM$14;->this$0:Lcom/helpshift/conversation/viewmodel/ConversationalVM;

    iget-object v0, v0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->renderer:Lcom/helpshift/conversation/activeconversation/ConversationalRenderer;

    if-eqz v0, :cond_0

    .line 1018
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM$14;->this$0:Lcom/helpshift/conversation/viewmodel/ConversationalVM;

    iget-object v0, v0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->renderer:Lcom/helpshift/conversation/activeconversation/ConversationalRenderer;

    iget-boolean v1, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM$14;->val$hideSmoothly:Z

    invoke-interface {v0, v1}, Lcom/helpshift/conversation/activeconversation/ConversationalRenderer;->hideListPicker(Z)V

    :cond_0
    return-void
.end method
