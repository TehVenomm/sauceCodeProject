.class Lcom/helpshift/conversation/viewmodel/NewConversationVM$11;
.super Lcom/helpshift/common/domain/F;
.source "NewConversationVM.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lcom/helpshift/conversation/viewmodel/NewConversationVM;->handleImageAttachmentClick()V
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

    .line 357
    iput-object p1, p0, Lcom/helpshift/conversation/viewmodel/NewConversationVM$11;->this$0:Lcom/helpshift/conversation/viewmodel/NewConversationVM;

    invoke-direct {p0}, Lcom/helpshift/common/domain/F;-><init>()V

    return-void
.end method


# virtual methods
.method public f()V
    .locals 3

    .line 360
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/NewConversationVM$11;->this$0:Lcom/helpshift/conversation/viewmodel/NewConversationVM;

    iget-object v0, v0, Lcom/helpshift/conversation/viewmodel/NewConversationVM;->imageAttachmentViewState:Lcom/helpshift/widget/MutableImageAttachmentViewState;

    invoke-virtual {v0}, Lcom/helpshift/widget/MutableImageAttachmentViewState;->getImagePickerFile()Lcom/helpshift/conversation/dto/ImagePickerFile;

    move-result-object v0

    if-eqz v0, :cond_0

    .line 361
    iget-object v1, v0, Lcom/helpshift/conversation/dto/ImagePickerFile;->filePath:Ljava/lang/String;

    invoke-static {v1}, Lcom/helpshift/common/StringUtils;->isEmpty(Ljava/lang/String;)Z

    move-result v1

    if-nez v1, :cond_0

    .line 362
    iget-object v1, p0, Lcom/helpshift/conversation/viewmodel/NewConversationVM$11;->this$0:Lcom/helpshift/conversation/viewmodel/NewConversationVM;

    iget-object v1, v1, Lcom/helpshift/conversation/viewmodel/NewConversationVM;->domain:Lcom/helpshift/common/domain/Domain;

    new-instance v2, Lcom/helpshift/conversation/viewmodel/NewConversationVM$11$1;

    invoke-direct {v2, p0, v0}, Lcom/helpshift/conversation/viewmodel/NewConversationVM$11$1;-><init>(Lcom/helpshift/conversation/viewmodel/NewConversationVM$11;Lcom/helpshift/conversation/dto/ImagePickerFile;)V

    invoke-virtual {v1, v2}, Lcom/helpshift/common/domain/Domain;->runOnUI(Lcom/helpshift/common/domain/F;)V

    :cond_0
    return-void
.end method
