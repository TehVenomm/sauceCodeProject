.class Lcom/helpshift/conversation/viewmodel/NewConversationVM$8;
.super Lcom/helpshift/common/domain/F;
.source "NewConversationVM.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lcom/helpshift/conversation/viewmodel/NewConversationVM;->setSearchQuery(Ljava/lang/String;)V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lcom/helpshift/conversation/viewmodel/NewConversationVM;

.field final synthetic val$searchQuery:Ljava/lang/String;


# direct methods
.method constructor <init>(Lcom/helpshift/conversation/viewmodel/NewConversationVM;Ljava/lang/String;)V
    .locals 0

    .line 304
    iput-object p1, p0, Lcom/helpshift/conversation/viewmodel/NewConversationVM$8;->this$0:Lcom/helpshift/conversation/viewmodel/NewConversationVM;

    iput-object p2, p0, Lcom/helpshift/conversation/viewmodel/NewConversationVM$8;->val$searchQuery:Ljava/lang/String;

    invoke-direct {p0}, Lcom/helpshift/common/domain/F;-><init>()V

    return-void
.end method


# virtual methods
.method public f()V
    .locals 4

    .line 307
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/NewConversationVM$8;->this$0:Lcom/helpshift/conversation/viewmodel/NewConversationVM;

    iget-object v0, v0, Lcom/helpshift/conversation/viewmodel/NewConversationVM;->descriptionViewState:Lcom/helpshift/widget/MutableTextViewState;

    invoke-virtual {v0}, Lcom/helpshift/widget/MutableTextViewState;->getText()Ljava/lang/String;

    move-result-object v0

    invoke-static {v0}, Lcom/helpshift/common/StringUtils;->isEmpty(Ljava/lang/String;)Z

    move-result v0

    if-eqz v0, :cond_0

    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/NewConversationVM$8;->val$searchQuery:Ljava/lang/String;

    .line 308
    invoke-static {v0}, Lcom/helpshift/common/StringUtils;->isEmpty(Ljava/lang/String;)Z

    move-result v0

    if-nez v0, :cond_0

    .line 309
    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    iget-object v1, p0, Lcom/helpshift/conversation/viewmodel/NewConversationVM$8;->val$searchQuery:Ljava/lang/String;

    const/4 v2, 0x0

    const/4 v3, 0x1

    invoke-virtual {v1, v2, v3}, Ljava/lang/String;->substring(II)Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v1}, Ljava/lang/String;->toUpperCase()Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v1, p0, Lcom/helpshift/conversation/viewmodel/NewConversationVM$8;->val$searchQuery:Ljava/lang/String;

    .line 310
    invoke-virtual {v1, v3}, Ljava/lang/String;->substring(I)Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v0

    .line 311
    iget-object v1, p0, Lcom/helpshift/conversation/viewmodel/NewConversationVM$8;->this$0:Lcom/helpshift/conversation/viewmodel/NewConversationVM;

    iget-object v1, v1, Lcom/helpshift/conversation/viewmodel/NewConversationVM;->descriptionViewState:Lcom/helpshift/widget/MutableTextViewState;

    invoke-virtual {v1, v0}, Lcom/helpshift/widget/MutableTextViewState;->setText(Ljava/lang/String;)V

    :cond_0
    return-void
.end method
