.class public Lcom/helpshift/widget/ImageAttachmentViewState;
.super Lcom/helpshift/widget/HSBaseObservable;
.source "ImageAttachmentViewState.java"


# instance fields
.field protected clickable:Z

.field protected imagePickerFile:Lcom/helpshift/conversation/dto/ImagePickerFile;


# direct methods
.method public constructor <init>()V
    .locals 1

    .line 5
    invoke-direct {p0}, Lcom/helpshift/widget/HSBaseObservable;-><init>()V

    const/4 v0, 0x1

    .line 8
    iput-boolean v0, p0, Lcom/helpshift/widget/ImageAttachmentViewState;->clickable:Z

    return-void
.end method


# virtual methods
.method public getImagePath()Ljava/lang/String;
    .locals 1

    .line 15
    iget-object v0, p0, Lcom/helpshift/widget/ImageAttachmentViewState;->imagePickerFile:Lcom/helpshift/conversation/dto/ImagePickerFile;

    if-nez v0, :cond_0

    const-string v0, ""

    return-object v0

    .line 18
    :cond_0
    iget-object v0, p0, Lcom/helpshift/widget/ImageAttachmentViewState;->imagePickerFile:Lcom/helpshift/conversation/dto/ImagePickerFile;

    iget-object v0, v0, Lcom/helpshift/conversation/dto/ImagePickerFile;->filePath:Ljava/lang/String;

    if-nez v0, :cond_1

    const-string v0, ""

    goto :goto_0

    :cond_1
    iget-object v0, p0, Lcom/helpshift/widget/ImageAttachmentViewState;->imagePickerFile:Lcom/helpshift/conversation/dto/ImagePickerFile;

    iget-object v0, v0, Lcom/helpshift/conversation/dto/ImagePickerFile;->filePath:Ljava/lang/String;

    :goto_0
    return-object v0
.end method

.method public getImagePickerFile()Lcom/helpshift/conversation/dto/ImagePickerFile;
    .locals 1

    .line 11
    iget-object v0, p0, Lcom/helpshift/widget/ImageAttachmentViewState;->imagePickerFile:Lcom/helpshift/conversation/dto/ImagePickerFile;

    return-object v0
.end method

.method public isClickable()Z
    .locals 1

    .line 22
    iget-boolean v0, p0, Lcom/helpshift/widget/ImageAttachmentViewState;->clickable:Z

    return v0
.end method

.method protected notifyInitialState()V
    .locals 0

    .line 27
    invoke-virtual {p0, p0}, Lcom/helpshift/widget/ImageAttachmentViewState;->notifyChange(Ljava/lang/Object;)V

    return-void
.end method
