.class Lcom/helpshift/conversation/viewmodel/ConversationalVM$9;
.super Lcom/helpshift/common/domain/F;
.source "ConversationalVM.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lcom/helpshift/conversation/viewmodel/ConversationalVM;->showErrorForNoNetwork(Lcom/helpshift/common/exception/RootAPIException;)V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lcom/helpshift/conversation/viewmodel/ConversationalVM;


# direct methods
.method constructor <init>(Lcom/helpshift/conversation/viewmodel/ConversationalVM;)V
    .locals 0

    .line 794
    iput-object p1, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM$9;->this$0:Lcom/helpshift/conversation/viewmodel/ConversationalVM;

    invoke-direct {p0}, Lcom/helpshift/common/domain/F;-><init>()V

    return-void
.end method


# virtual methods
.method public f()V
    .locals 2

    .line 797
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM$9;->this$0:Lcom/helpshift/conversation/viewmodel/ConversationalVM;

    iget-object v0, v0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->renderer:Lcom/helpshift/conversation/activeconversation/ConversationalRenderer;

    if-eqz v0, :cond_0

    .line 798
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM$9;->this$0:Lcom/helpshift/conversation/viewmodel/ConversationalVM;

    iget-object v0, v0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->renderer:Lcom/helpshift/conversation/activeconversation/ConversationalRenderer;

    const/4 v1, 0x1

    invoke-interface {v0, v1}, Lcom/helpshift/conversation/activeconversation/ConversationalRenderer;->showNetworkErrorFooter(I)V

    :cond_0
    return-void
.end method
