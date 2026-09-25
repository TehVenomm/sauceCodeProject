.class Lcom/helpshift/conversation/viewmodel/ConversationalVM$19;
.super Lcom/helpshift/common/domain/F;
.source "ConversationalVM.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lcom/helpshift/conversation/viewmodel/ConversationalVM;->handleIdempotentPreIssueCreationSuccess()V
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

    .line 1304
    iput-object p1, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM$19;->this$0:Lcom/helpshift/conversation/viewmodel/ConversationalVM;

    invoke-direct {p0}, Lcom/helpshift/common/domain/F;-><init>()V

    return-void
.end method


# virtual methods
.method public f()V
    .locals 1

    .line 1308
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM$19;->this$0:Lcom/helpshift/conversation/viewmodel/ConversationalVM;

    invoke-virtual {v0}, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->handlePreIssueCreationSuccess()V

    .line 1311
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM$19;->this$0:Lcom/helpshift/conversation/viewmodel/ConversationalVM;

    iget-object v0, v0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->renderer:Lcom/helpshift/conversation/activeconversation/ConversationalRenderer;

    if-eqz v0, :cond_0

    .line 1312
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM$19;->this$0:Lcom/helpshift/conversation/viewmodel/ConversationalVM;

    invoke-virtual {v0}, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->refreshVM()V

    :cond_0
    return-void
.end method
