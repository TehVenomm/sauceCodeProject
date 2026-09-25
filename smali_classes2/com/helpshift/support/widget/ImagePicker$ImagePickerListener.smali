.class public interface abstract Lcom/helpshift/support/widget/ImagePicker$ImagePickerListener;
.super Ljava/lang/Object;
.source "ImagePicker.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lcom/helpshift/support/widget/ImagePicker;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x609
    name = "ImagePickerListener"
.end annotation


# virtual methods
.method public abstract askForReadStoragePermission()V
.end method

.method public abstract onImagePickerResultFailure(ILjava/lang/Long;)V
.end method

.method public abstract onImagePickerResultSuccess(Lcom/helpshift/conversation/dto/ImagePickerFile;Landroid/os/Bundle;)V
.end method
