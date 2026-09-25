.class Lcom/helpshift/common/domain/AttachmentFileManagerDM$1;
.super Lcom/helpshift/common/domain/F;
.source "AttachmentFileManagerDM.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lcom/helpshift/common/domain/AttachmentFileManagerDM;->compressAndCopyScreenshot(Lcom/helpshift/conversation/dto/ImagePickerFile;Ljava/lang/String;Lcom/helpshift/common/domain/AttachmentFileManagerDM$Listener;)V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lcom/helpshift/common/domain/AttachmentFileManagerDM;

.field final synthetic val$imagePickerFile:Lcom/helpshift/conversation/dto/ImagePickerFile;

.field final synthetic val$listener:Lcom/helpshift/common/domain/AttachmentFileManagerDM$Listener;

.field final synthetic val$refersId:Ljava/lang/String;


# direct methods
.method constructor <init>(Lcom/helpshift/common/domain/AttachmentFileManagerDM;Lcom/helpshift/conversation/dto/ImagePickerFile;Ljava/lang/String;Lcom/helpshift/common/domain/AttachmentFileManagerDM$Listener;)V
    .locals 0

    .line 35
    iput-object p1, p0, Lcom/helpshift/common/domain/AttachmentFileManagerDM$1;->this$0:Lcom/helpshift/common/domain/AttachmentFileManagerDM;

    iput-object p2, p0, Lcom/helpshift/common/domain/AttachmentFileManagerDM$1;->val$imagePickerFile:Lcom/helpshift/conversation/dto/ImagePickerFile;

    iput-object p3, p0, Lcom/helpshift/common/domain/AttachmentFileManagerDM$1;->val$refersId:Ljava/lang/String;

    iput-object p4, p0, Lcom/helpshift/common/domain/AttachmentFileManagerDM$1;->val$listener:Lcom/helpshift/common/domain/AttachmentFileManagerDM$Listener;

    invoke-direct {p0}, Lcom/helpshift/common/domain/F;-><init>()V

    return-void
.end method


# virtual methods
.method public f()V
    .locals 3

    .line 39
    :try_start_0
    iget-object v0, p0, Lcom/helpshift/common/domain/AttachmentFileManagerDM$1;->this$0:Lcom/helpshift/common/domain/AttachmentFileManagerDM;

    iget-object v0, v0, Lcom/helpshift/common/domain/AttachmentFileManagerDM;->platform:Lcom/helpshift/common/platform/Platform;

    iget-object v1, p0, Lcom/helpshift/common/domain/AttachmentFileManagerDM$1;->val$imagePickerFile:Lcom/helpshift/conversation/dto/ImagePickerFile;

    iget-object v2, p0, Lcom/helpshift/common/domain/AttachmentFileManagerDM$1;->val$refersId:Ljava/lang/String;

    invoke-interface {v0, v1, v2}, Lcom/helpshift/common/platform/Platform;->compressAndCopyScreenshot(Lcom/helpshift/conversation/dto/ImagePickerFile;Ljava/lang/String;)V

    .line 40
    iget-object v0, p0, Lcom/helpshift/common/domain/AttachmentFileManagerDM$1;->val$listener:Lcom/helpshift/common/domain/AttachmentFileManagerDM$Listener;

    iget-object v1, p0, Lcom/helpshift/common/domain/AttachmentFileManagerDM$1;->val$imagePickerFile:Lcom/helpshift/conversation/dto/ImagePickerFile;

    invoke-interface {v0, v1}, Lcom/helpshift/common/domain/AttachmentFileManagerDM$Listener;->onCompressAndCopySuccess(Lcom/helpshift/conversation/dto/ImagePickerFile;)V
    :try_end_0
    .catch Lcom/helpshift/common/exception/RootAPIException; {:try_start_0 .. :try_end_0} :catch_0

    return-void

    :catch_0
    move-exception v0

    .line 43
    iget-object v1, p0, Lcom/helpshift/common/domain/AttachmentFileManagerDM$1;->val$listener:Lcom/helpshift/common/domain/AttachmentFileManagerDM$Listener;

    invoke-interface {v1, v0}, Lcom/helpshift/common/domain/AttachmentFileManagerDM$Listener;->onCompressAndCopyFailure(Lcom/helpshift/common/exception/RootAPIException;)V

    .line 44
    throw v0
.end method
