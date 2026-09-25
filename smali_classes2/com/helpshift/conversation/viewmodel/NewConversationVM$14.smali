.class Lcom/helpshift/conversation/viewmodel/NewConversationVM$14;
.super Lcom/helpshift/common/domain/F;
.source "NewConversationVM.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lcom/helpshift/conversation/viewmodel/NewConversationVM;->onAuthenticationFailure()V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lcom/helpshift/conversation/viewmodel/NewConversationVM;


# direct methods
.method constructor <init>(Lcom/helpshift/conversation/viewmodel/NewConversationVM;)V
    .locals 0

    .line 395
    iput-object p1, p0, Lcom/helpshift/conversation/viewmodel/NewConversationVM$14;->this$0:Lcom/helpshift/conversation/viewmodel/NewConversationVM;

    invoke-direct {p0}, Lcom/helpshift/common/domain/F;-><init>()V

    return-void
.end method


# virtual methods
.method public f()V
    .locals 1

    .line 398
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/NewConversationVM$14;->this$0:Lcom/helpshift/conversation/viewmodel/NewConversationVM;

    iget-object v0, v0, Lcom/helpshift/conversation/viewmodel/NewConversationVM;->rendererWeakRef:Ljava/lang/ref/WeakReference;

    invoke-virtual {v0}, Ljava/lang/ref/WeakReference;->get()Ljava/lang/Object;

    move-result-object v0

    if-eqz v0, :cond_0

    .line 399
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/NewConversationVM$14;->this$0:Lcom/helpshift/conversation/viewmodel/NewConversationVM;

    iget-object v0, v0, Lcom/helpshift/conversation/viewmodel/NewConversationVM;->rendererWeakRef:Ljava/lang/ref/WeakReference;

    invoke-virtual {v0}, Ljava/lang/ref/WeakReference;->get()Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Lcom/helpshift/conversation/viewmodel/NewConversationRenderer;

    invoke-interface {v0}, Lcom/helpshift/conversation/viewmodel/NewConversationRenderer;->onAuthenticationFailure()V

    :cond_0
    return-void
.end method
