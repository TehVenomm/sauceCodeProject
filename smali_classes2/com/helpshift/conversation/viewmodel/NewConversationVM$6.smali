.class Lcom/helpshift/conversation/viewmodel/NewConversationVM$6;
.super Lcom/helpshift/common/domain/F;
.source "NewConversationVM.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lcom/helpshift/conversation/viewmodel/NewConversationVM;->onCreateConversationSuccess(J)V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lcom/helpshift/conversation/viewmodel/NewConversationVM;

.field final synthetic val$conversationLocalId:J


# direct methods
.method constructor <init>(Lcom/helpshift/conversation/viewmodel/NewConversationVM;J)V
    .locals 0

    .line 197
    iput-object p1, p0, Lcom/helpshift/conversation/viewmodel/NewConversationVM$6;->this$0:Lcom/helpshift/conversation/viewmodel/NewConversationVM;

    iput-wide p2, p0, Lcom/helpshift/conversation/viewmodel/NewConversationVM$6;->val$conversationLocalId:J

    invoke-direct {p0}, Lcom/helpshift/common/domain/F;-><init>()V

    return-void
.end method


# virtual methods
.method public f()V
    .locals 3

    .line 200
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/NewConversationVM$6;->this$0:Lcom/helpshift/conversation/viewmodel/NewConversationVM;

    iget-object v0, v0, Lcom/helpshift/conversation/viewmodel/NewConversationVM;->rendererWeakRef:Ljava/lang/ref/WeakReference;

    invoke-virtual {v0}, Ljava/lang/ref/WeakReference;->get()Ljava/lang/Object;

    move-result-object v0

    if-eqz v0, :cond_1

    .line 201
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/NewConversationVM$6;->this$0:Lcom/helpshift/conversation/viewmodel/NewConversationVM;

    iget-object v0, v0, Lcom/helpshift/conversation/viewmodel/NewConversationVM;->rendererWeakRef:Ljava/lang/ref/WeakReference;

    invoke-virtual {v0}, Ljava/lang/ref/WeakReference;->get()Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Lcom/helpshift/conversation/viewmodel/NewConversationRenderer;

    .line 202
    iget-object v1, p0, Lcom/helpshift/conversation/viewmodel/NewConversationVM$6;->this$0:Lcom/helpshift/conversation/viewmodel/NewConversationVM;

    iget-object v1, v1, Lcom/helpshift/conversation/viewmodel/NewConversationVM;->sdkConfigurationDM:Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;

    const-string v2, "gotoConversationAfterContactUs"

    invoke-virtual {v1, v2}, Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;->getBoolean(Ljava/lang/String;)Z

    move-result v1

    if-eqz v1, :cond_0

    iget-object v1, p0, Lcom/helpshift/conversation/viewmodel/NewConversationVM$6;->this$0:Lcom/helpshift/conversation/viewmodel/NewConversationVM;

    iget-object v1, v1, Lcom/helpshift/conversation/viewmodel/NewConversationVM;->sdkConfigurationDM:Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;

    const-string v2, "disableInAppConversation"

    .line 203
    invoke-virtual {v1, v2}, Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;->getBoolean(Ljava/lang/String;)Z

    move-result v1

    if-nez v1, :cond_0

    .line 204
    iget-wide v1, p0, Lcom/helpshift/conversation/viewmodel/NewConversationVM$6;->val$conversationLocalId:J

    invoke-interface {v0, v1, v2}, Lcom/helpshift/conversation/viewmodel/NewConversationRenderer;->gotoConversation(J)V

    goto :goto_0

    .line 207
    :cond_0
    invoke-interface {v0}, Lcom/helpshift/conversation/viewmodel/NewConversationRenderer;->showConversationStartedMessage()V

    .line 208
    invoke-interface {v0}, Lcom/helpshift/conversation/viewmodel/NewConversationRenderer;->exit()V

    :cond_1
    :goto_0
    return-void
.end method
