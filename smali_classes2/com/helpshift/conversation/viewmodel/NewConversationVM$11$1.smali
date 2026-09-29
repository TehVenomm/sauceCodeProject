.class Lcom/helpshift/conversation/viewmodel/NewConversationVM$11$1;
.super Lcom/helpshift/common/domain/F;
.source "NewConversationVM.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lcom/helpshift/conversation/viewmodel/NewConversationVM$11;->f()V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$1:Lcom/helpshift/conversation/viewmodel/NewConversationVM$11;

.field final synthetic val$attachmentFile:Lcom/helpshift/conversation/dto/ImagePickerFile;


# direct methods
.method constructor <init>(Lcom/helpshift/conversation/viewmodel/NewConversationVM$11;Lcom/helpshift/conversation/dto/ImagePickerFile;)V
    .locals 0

    .line 362
    iput-object p1, p0, Lcom/helpshift/conversation/viewmodel/NewConversationVM$11$1;->this$1:Lcom/helpshift/conversation/viewmodel/NewConversationVM$11;

    iput-object p2, p0, Lcom/helpshift/conversation/viewmodel/NewConversationVM$11$1;->val$attachmentFile:Lcom/helpshift/conversation/dto/ImagePickerFile;

    invoke-direct {p0}, Lcom/helpshift/common/domain/F;-><init>()V

    return-void
.end method


# virtual methods
.method public f()V
    .locals 2

    .line 365
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/NewConversationVM$11$1;->this$1:Lcom/helpshift/conversation/viewmodel/NewConversationVM$11;

    iget-object v0, v0, Lcom/helpshift/conversation/viewmodel/NewConversationVM$11;->this$0:Lcom/helpshift/conversation/viewmodel/NewConversationVM;

    iget-object v0, v0, Lcom/helpshift/conversation/viewmodel/NewConversationVM;->rendererWeakRef:Ljava/lang/ref/WeakReference;

    invoke-virtual {v0}, Ljava/lang/ref/WeakReference;->get()Ljava/lang/Object;

    move-result-object v0

    if-eqz v0, :cond_0

    .line 366
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/NewConversationVM$11$1;->this$1:Lcom/helpshift/conversation/viewmodel/NewConversationVM$11;

    iget-object v0, v0, Lcom/helpshift/conversation/viewmodel/NewConversationVM$11;->this$0:Lcom/helpshift/conversation/viewmodel/NewConversationVM;

    iget-object v0, v0, Lcom/helpshift/conversation/viewmodel/NewConversationVM;->rendererWeakRef:Ljava/lang/ref/WeakReference;

    invoke-virtual {v0}, Ljava/lang/ref/WeakReference;->get()Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Lcom/helpshift/conversation/viewmodel/NewConversationRenderer;

    iget-object v1, p0, Lcom/helpshift/conversation/viewmodel/NewConversationVM$11$1;->val$attachmentFile:Lcom/helpshift/conversation/dto/ImagePickerFile;

    invoke-interface {v0, v1}, Lcom/helpshift/conversation/viewmodel/NewConversationRenderer;->showAttachmentPreviewScreenFromDraft(Lcom/helpshift/conversation/dto/ImagePickerFile;)V

    :cond_0
    return-void
.end method
