.class Lcom/helpshift/conversation/viewmodel/ScreenshotPreviewVM$1;
.super Lcom/helpshift/common/domain/F;
.source "ScreenshotPreviewVM.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lcom/helpshift/conversation/viewmodel/ScreenshotPreviewVM;->onAuthenticationFailure()V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lcom/helpshift/conversation/viewmodel/ScreenshotPreviewVM;


# direct methods
.method constructor <init>(Lcom/helpshift/conversation/viewmodel/ScreenshotPreviewVM;)V
    .locals 0

    .line 22
    iput-object p1, p0, Lcom/helpshift/conversation/viewmodel/ScreenshotPreviewVM$1;->this$0:Lcom/helpshift/conversation/viewmodel/ScreenshotPreviewVM;

    invoke-direct {p0}, Lcom/helpshift/common/domain/F;-><init>()V

    return-void
.end method


# virtual methods
.method public f()V
    .locals 1

    .line 25
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ScreenshotPreviewVM$1;->this$0:Lcom/helpshift/conversation/viewmodel/ScreenshotPreviewVM;

    invoke-static {v0}, Lcom/helpshift/conversation/viewmodel/ScreenshotPreviewVM;->access$000(Lcom/helpshift/conversation/viewmodel/ScreenshotPreviewVM;)Lcom/helpshift/conversation/activeconversation/ScreenshotPreviewRenderer;

    move-result-object v0

    if-eqz v0, :cond_0

    .line 26
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ScreenshotPreviewVM$1;->this$0:Lcom/helpshift/conversation/viewmodel/ScreenshotPreviewVM;

    invoke-static {v0}, Lcom/helpshift/conversation/viewmodel/ScreenshotPreviewVM;->access$000(Lcom/helpshift/conversation/viewmodel/ScreenshotPreviewVM;)Lcom/helpshift/conversation/activeconversation/ScreenshotPreviewRenderer;

    move-result-object v0

    invoke-interface {v0}, Lcom/helpshift/conversation/activeconversation/ScreenshotPreviewRenderer;->onAuthenticationFailure()V

    :cond_0
    return-void
.end method
