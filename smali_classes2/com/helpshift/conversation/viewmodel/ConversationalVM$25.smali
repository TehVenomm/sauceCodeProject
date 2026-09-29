.class Lcom/helpshift/conversation/viewmodel/ConversationalVM$25;
.super Lcom/helpshift/common/domain/F;
.source "ConversationalVM.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lcom/helpshift/conversation/viewmodel/ConversationalVM;->update(Ljava/util/Observable;Ljava/lang/Object;)V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lcom/helpshift/conversation/viewmodel/ConversationalVM;

.field final synthetic val$observable:Ljava/util/Observable;


# direct methods
.method constructor <init>(Lcom/helpshift/conversation/viewmodel/ConversationalVM;Ljava/util/Observable;)V
    .locals 0

    .line 1804
    iput-object p1, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM$25;->this$0:Lcom/helpshift/conversation/viewmodel/ConversationalVM;

    iput-object p2, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM$25;->val$observable:Ljava/util/Observable;

    invoke-direct {p0}, Lcom/helpshift/common/domain/F;-><init>()V

    return-void
.end method


# virtual methods
.method public f()V
    .locals 1

    .line 1807
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM$25;->this$0:Lcom/helpshift/conversation/viewmodel/ConversationalVM;

    iget-object v0, v0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->renderer:Lcom/helpshift/conversation/activeconversation/ConversationalRenderer;

    if-eqz v0, :cond_0

    .line 1808
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM$25;->val$observable:Ljava/util/Observable;

    instance-of v0, v0, Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;

    if-eqz v0, :cond_0

    .line 1815
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ConversationalVM$25;->this$0:Lcom/helpshift/conversation/viewmodel/ConversationalVM;

    invoke-virtual {v0}, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->refreshAll()V

    :cond_0
    return-void
.end method
