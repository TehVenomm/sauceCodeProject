.class Lcom/helpshift/support/fragments/ScreenshotPreviewFragment$2;
.super Ljava/lang/Object;
.source "ScreenshotPreviewFragment.java"

# interfaces
.implements Ljava/lang/Runnable;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lcom/helpshift/support/fragments/ScreenshotPreviewFragment;->onCompressAndCopySuccess(Lcom/helpshift/conversation/dto/ImagePickerFile;)V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lcom/helpshift/support/fragments/ScreenshotPreviewFragment;

.field final synthetic val$imagePickerFile:Lcom/helpshift/conversation/dto/ImagePickerFile;


# direct methods
.method constructor <init>(Lcom/helpshift/support/fragments/ScreenshotPreviewFragment;Lcom/helpshift/conversation/dto/ImagePickerFile;)V
    .locals 0

    .line 249
    iput-object p1, p0, Lcom/helpshift/support/fragments/ScreenshotPreviewFragment$2;->this$0:Lcom/helpshift/support/fragments/ScreenshotPreviewFragment;

    iput-object p2, p0, Lcom/helpshift/support/fragments/ScreenshotPreviewFragment$2;->val$imagePickerFile:Lcom/helpshift/conversation/dto/ImagePickerFile;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public run()V
    .locals 2

    .line 252
    iget-object v0, p0, Lcom/helpshift/support/fragments/ScreenshotPreviewFragment$2;->this$0:Lcom/helpshift/support/fragments/ScreenshotPreviewFragment;

    const/4 v1, 0x0

    invoke-virtual {v0, v1}, Lcom/helpshift/support/fragments/ScreenshotPreviewFragment;->toggleProgressBarViewsVisibility(Z)V

    .line 253
    iget-object v0, p0, Lcom/helpshift/support/fragments/ScreenshotPreviewFragment$2;->this$0:Lcom/helpshift/support/fragments/ScreenshotPreviewFragment;

    iget-object v1, p0, Lcom/helpshift/support/fragments/ScreenshotPreviewFragment$2;->val$imagePickerFile:Lcom/helpshift/conversation/dto/ImagePickerFile;

    iget-object v1, v1, Lcom/helpshift/conversation/dto/ImagePickerFile;->filePath:Ljava/lang/String;

    invoke-virtual {v0, v1}, Lcom/helpshift/support/fragments/ScreenshotPreviewFragment;->renderScreenshotPreview(Ljava/lang/String;)V

    return-void
.end method
