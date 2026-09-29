.class Lcom/helpshift/conversation/viewmodel/NewConversationVM$7;
.super Lcom/helpshift/common/domain/F;
.source "NewConversationVM.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lcom/helpshift/conversation/viewmodel/NewConversationVM;->handleException(Ljava/lang/Exception;)V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lcom/helpshift/conversation/viewmodel/NewConversationVM;

.field final synthetic val$e:Ljava/lang/Exception;


# direct methods
.method constructor <init>(Lcom/helpshift/conversation/viewmodel/NewConversationVM;Ljava/lang/Exception;)V
    .locals 0

    .line 233
    iput-object p1, p0, Lcom/helpshift/conversation/viewmodel/NewConversationVM$7;->this$0:Lcom/helpshift/conversation/viewmodel/NewConversationVM;

    iput-object p2, p0, Lcom/helpshift/conversation/viewmodel/NewConversationVM$7;->val$e:Ljava/lang/Exception;

    invoke-direct {p0}, Lcom/helpshift/common/domain/F;-><init>()V

    return-void
.end method


# virtual methods
.method public f()V
    .locals 2

    .line 236
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/NewConversationVM$7;->val$e:Ljava/lang/Exception;

    instance-of v0, v0, Lcom/helpshift/common/exception/RootAPIException;

    if-eqz v0, :cond_0

    .line 237
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/NewConversationVM$7;->val$e:Ljava/lang/Exception;

    check-cast v0, Lcom/helpshift/common/exception/RootAPIException;

    .line 238
    iget-object v1, p0, Lcom/helpshift/conversation/viewmodel/NewConversationVM$7;->this$0:Lcom/helpshift/conversation/viewmodel/NewConversationVM;

    iget-object v1, v1, Lcom/helpshift/conversation/viewmodel/NewConversationVM;->rendererWeakRef:Ljava/lang/ref/WeakReference;

    invoke-virtual {v1}, Ljava/lang/ref/WeakReference;->get()Ljava/lang/Object;

    move-result-object v1

    if-eqz v1, :cond_0

    .line 239
    iget-object v1, p0, Lcom/helpshift/conversation/viewmodel/NewConversationVM$7;->this$0:Lcom/helpshift/conversation/viewmodel/NewConversationVM;

    iget-object v1, v1, Lcom/helpshift/conversation/viewmodel/NewConversationVM;->rendererWeakRef:Ljava/lang/ref/WeakReference;

    invoke-virtual {v1}, Ljava/lang/ref/WeakReference;->get()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Lcom/helpshift/conversation/viewmodel/NewConversationRenderer;

    .line 240
    iget-object v0, v0, Lcom/helpshift/common/exception/RootAPIException;->exceptionType:Lcom/helpshift/common/exception/ExceptionType;

    invoke-interface {v1, v0}, Lcom/helpshift/conversation/viewmodel/NewConversationRenderer;->showErrorView(Lcom/helpshift/common/exception/ExceptionType;)V

    :cond_0
    return-void
.end method
