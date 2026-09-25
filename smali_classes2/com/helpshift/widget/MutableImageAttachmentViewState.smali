.class public Lcom/helpshift/widget/MutableImageAttachmentViewState;
.super Lcom/helpshift/widget/ImageAttachmentViewState;
.source "MutableImageAttachmentViewState.java"


# direct methods
.method public constructor <init>()V
    .locals 0

    .line 5
    invoke-direct {p0}, Lcom/helpshift/widget/ImageAttachmentViewState;-><init>()V

    return-void
.end method


# virtual methods
.method public setClickable(Z)V
    .locals 0

    .line 13
    iput-boolean p1, p0, Lcom/helpshift/widget/MutableImageAttachmentViewState;->clickable:Z

    .line 14
    invoke-virtual {p0, p0}, Lcom/helpshift/widget/MutableImageAttachmentViewState;->notifyChange(Ljava/lang/Object;)V

    return-void
.end method

.method public setImagePickerFile(Lcom/helpshift/conversation/dto/ImagePickerFile;)V
    .locals 0

    .line 8
    iput-object p1, p0, Lcom/helpshift/widget/MutableImageAttachmentViewState;->imagePickerFile:Lcom/helpshift/conversation/dto/ImagePickerFile;

    .line 9
    invoke-virtual {p0, p0}, Lcom/helpshift/widget/MutableImageAttachmentViewState;->notifyChange(Ljava/lang/Object;)V

    return-void
.end method
