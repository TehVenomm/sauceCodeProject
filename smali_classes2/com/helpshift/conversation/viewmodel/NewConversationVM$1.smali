.class Lcom/helpshift/conversation/viewmodel/NewConversationVM$1;
.super Lcom/helpshift/common/domain/F;
.source "NewConversationVM.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lcom/helpshift/conversation/viewmodel/NewConversationVM;->setDescription(Ljava/lang/String;)V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lcom/helpshift/conversation/viewmodel/NewConversationVM;

.field final synthetic val$description:Ljava/lang/String;


# direct methods
.method constructor <init>(Lcom/helpshift/conversation/viewmodel/NewConversationVM;Ljava/lang/String;)V
    .locals 0

    .line 83
    iput-object p1, p0, Lcom/helpshift/conversation/viewmodel/NewConversationVM$1;->this$0:Lcom/helpshift/conversation/viewmodel/NewConversationVM;

    iput-object p2, p0, Lcom/helpshift/conversation/viewmodel/NewConversationVM$1;->val$description:Ljava/lang/String;

    invoke-direct {p0}, Lcom/helpshift/common/domain/F;-><init>()V

    return-void
.end method


# virtual methods
.method public f()V
    .locals 2

    .line 86
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/NewConversationVM$1;->this$0:Lcom/helpshift/conversation/viewmodel/NewConversationVM;

    iget-object v0, v0, Lcom/helpshift/conversation/viewmodel/NewConversationVM;->descriptionViewState:Lcom/helpshift/widget/MutableTextViewState;

    invoke-virtual {v0}, Lcom/helpshift/widget/MutableTextViewState;->getText()Ljava/lang/String;

    move-result-object v0

    .line 87
    iget-object v1, p0, Lcom/helpshift/conversation/viewmodel/NewConversationVM$1;->val$description:Ljava/lang/String;

    invoke-virtual {v0, v1}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v0

    if-nez v0, :cond_0

    .line 88
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/NewConversationVM$1;->this$0:Lcom/helpshift/conversation/viewmodel/NewConversationVM;

    iget-object v0, v0, Lcom/helpshift/conversation/viewmodel/NewConversationVM;->descriptionViewState:Lcom/helpshift/widget/MutableTextViewState;

    iget-object v1, p0, Lcom/helpshift/conversation/viewmodel/NewConversationVM$1;->val$description:Ljava/lang/String;

    invoke-virtual {v0, v1}, Lcom/helpshift/widget/MutableTextViewState;->setText(Ljava/lang/String;)V

    .line 89
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/NewConversationVM$1;->this$0:Lcom/helpshift/conversation/viewmodel/NewConversationVM;

    iget-object v0, v0, Lcom/helpshift/conversation/viewmodel/NewConversationVM;->widgetGateway:Lcom/helpshift/widget/WidgetGateway;

    iget-object v1, p0, Lcom/helpshift/conversation/viewmodel/NewConversationVM$1;->this$0:Lcom/helpshift/conversation/viewmodel/NewConversationVM;

    iget-object v1, v1, Lcom/helpshift/conversation/viewmodel/NewConversationVM;->descriptionViewState:Lcom/helpshift/widget/MutableTextViewState;

    invoke-virtual {v0, v1}, Lcom/helpshift/widget/WidgetGateway;->save(Lcom/helpshift/widget/MutableTextViewState;)V

    :cond_0
    return-void
.end method
