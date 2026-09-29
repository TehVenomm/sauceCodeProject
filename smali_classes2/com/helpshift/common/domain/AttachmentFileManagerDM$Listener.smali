.class public interface abstract Lcom/helpshift/common/domain/AttachmentFileManagerDM$Listener;
.super Ljava/lang/Object;
.source "AttachmentFileManagerDM.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lcom/helpshift/common/domain/AttachmentFileManagerDM;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x609
    name = "Listener"
.end annotation


# virtual methods
.method public abstract onCompressAndCopyFailure(Lcom/helpshift/common/exception/RootAPIException;)V
.end method

.method public abstract onCompressAndCopySuccess(Lcom/helpshift/conversation/dto/ImagePickerFile;)V
.end method
