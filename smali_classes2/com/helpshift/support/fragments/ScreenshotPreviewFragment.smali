.class public Lcom/helpshift/support/fragments/ScreenshotPreviewFragment;
.super Lcom/helpshift/support/fragments/MainFragment;
.source "ScreenshotPreviewFragment.java"

# interfaces
.implements Landroid/view/View$OnClickListener;
.implements Lcom/helpshift/common/domain/AttachmentFileManagerDM$Listener;
.implements Lcom/helpshift/conversation/activeconversation/ScreenshotPreviewRenderer;


# annotations
.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Lcom/helpshift/support/fragments/ScreenshotPreviewFragment$Modes;,
        Lcom/helpshift/support/fragments/ScreenshotPreviewFragment$LaunchSource;,
        Lcom/helpshift/support/fragments/ScreenshotPreviewFragment$ScreenshotAction;
    }
.end annotation


# static fields
.field public static final FRAGMENT_TAG:Ljava/lang/String; = "ScreenshotPreviewFragment"

.field public static final KEY_MESSAGE_REFERS_ID:Ljava/lang/String; = "key_refers_id"

.field public static final KEY_SCREENSHOT_MODE:Ljava/lang/String; = "key_screenshot_mode"

.field private static final screenType:Lcom/helpshift/support/util/AppSessionConstants$Screen;


# instance fields
.field private attachmentMessageRefersId:Ljava/lang/String;

.field private buttonsContainer:Landroid/view/View;

.field private buttonsSeparator:Landroid/view/View;

.field imagePickerFile:Lcom/helpshift/conversation/dto/ImagePickerFile;

.field launchSource:Lcom/helpshift/support/fragments/ScreenshotPreviewFragment$LaunchSource;

.field private mode:I

.field progressBar:Landroid/widget/ProgressBar;

.field private screenshotPreview:Landroid/widget/ImageView;

.field private screenshotPreviewListener:Lcom/helpshift/support/contracts/ScreenshotPreviewListener;

.field private screenshotPreviewVM:Lcom/helpshift/conversation/viewmodel/ScreenshotPreviewVM;

.field private secondaryButton:Landroid/widget/Button;


# direct methods
.method static constructor <clinit>()V
    .locals 1

    .line 40
    sget-object v0, Lcom/helpshift/support/util/AppSessionConstants$Screen;->SCREENSHOT_PREVIEW:Lcom/helpshift/support/util/AppSessionConstants$Screen;

    sput-object v0, Lcom/helpshift/support/fragments/ScreenshotPreviewFragment;->screenType:Lcom/helpshift/support/util/AppSessionConstants$Screen;

    return-void
.end method

.method public constructor <init>()V
    .locals 0

    .line 33
    invoke-direct {p0}, Lcom/helpshift/support/fragments/MainFragment;-><init>()V

    return-void
.end method

.method public static newInstance(Lcom/helpshift/support/contracts/ScreenshotPreviewListener;)Lcom/helpshift/support/fragments/ScreenshotPreviewFragment;
    .locals 1

    .line 53
    new-instance v0, Lcom/helpshift/support/fragments/ScreenshotPreviewFragment;

    invoke-direct {v0}, Lcom/helpshift/support/fragments/ScreenshotPreviewFragment;-><init>()V

    .line 54
    iput-object p0, v0, Lcom/helpshift/support/fragments/ScreenshotPreviewFragment;->screenshotPreviewListener:Lcom/helpshift/support/contracts/ScreenshotPreviewListener;

    return-object v0
.end method

.method private setScreenshotPreview()V
    .locals 3

    .line 168
    invoke-virtual {p0}, Lcom/helpshift/support/fragments/ScreenshotPreviewFragment;->isResumed()Z

    move-result v0

    if-eqz v0, :cond_3

    .line 169
    iget-object v0, p0, Lcom/helpshift/support/fragments/ScreenshotPreviewFragment;->imagePickerFile:Lcom/helpshift/conversation/dto/ImagePickerFile;

    if-nez v0, :cond_1

    .line 170
    iget-object v0, p0, Lcom/helpshift/support/fragments/ScreenshotPreviewFragment;->screenshotPreviewListener:Lcom/helpshift/support/contracts/ScreenshotPreviewListener;

    if-eqz v0, :cond_0

    .line 171
    iget-object v0, p0, Lcom/helpshift/support/fragments/ScreenshotPreviewFragment;->screenshotPreviewListener:Lcom/helpshift/support/contracts/ScreenshotPreviewListener;

    invoke-interface {v0}, Lcom/helpshift/support/contracts/ScreenshotPreviewListener;->removeScreenshotPreviewFragment()V

    :cond_0
    return-void

    .line 175
    :cond_1
    iget-object v0, p0, Lcom/helpshift/support/fragments/ScreenshotPreviewFragment;->imagePickerFile:Lcom/helpshift/conversation/dto/ImagePickerFile;

    iget-object v0, v0, Lcom/helpshift/conversation/dto/ImagePickerFile;->filePath:Ljava/lang/String;

    if-eqz v0, :cond_2

    .line 177
    iget-object v0, p0, Lcom/helpshift/support/fragments/ScreenshotPreviewFragment;->imagePickerFile:Lcom/helpshift/conversation/dto/ImagePickerFile;

    iget-object v0, v0, Lcom/helpshift/conversation/dto/ImagePickerFile;->filePath:Ljava/lang/String;

    invoke-virtual {p0, v0}, Lcom/helpshift/support/fragments/ScreenshotPreviewFragment;->renderScreenshotPreview(Ljava/lang/String;)V

    goto :goto_0

    .line 179
    :cond_2
    iget-object v0, p0, Lcom/helpshift/support/fragments/ScreenshotPreviewFragment;->imagePickerFile:Lcom/helpshift/conversation/dto/ImagePickerFile;

    iget-object v0, v0, Lcom/helpshift/conversation/dto/ImagePickerFile;->transientUri:Ljava/lang/Object;

    if-eqz v0, :cond_3

    const/4 v0, 0x1

    .line 182
    invoke-virtual {p0, v0}, Lcom/helpshift/support/fragments/ScreenshotPreviewFragment;->toggleProgressBarViewsVisibility(Z)V

    .line 183
    invoke-static {}, Lcom/helpshift/util/HelpshiftContext;->getCoreApi()Lcom/helpshift/CoreApi;

    move-result-object v0

    invoke-interface {v0}, Lcom/helpshift/CoreApi;->getAttachmentFileManagerDM()Lcom/helpshift/common/domain/AttachmentFileManagerDM;

    move-result-object v0

    iget-object v1, p0, Lcom/helpshift/support/fragments/ScreenshotPreviewFragment;->imagePickerFile:Lcom/helpshift/conversation/dto/ImagePickerFile;

    iget-object v2, p0, Lcom/helpshift/support/fragments/ScreenshotPreviewFragment;->attachmentMessageRefersId:Ljava/lang/String;

    invoke-virtual {v0, v1, v2, p0}, Lcom/helpshift/common/domain/AttachmentFileManagerDM;->compressAndCopyScreenshot(Lcom/helpshift/conversation/dto/ImagePickerFile;Ljava/lang/String;Lcom/helpshift/common/domain/AttachmentFileManagerDM$Listener;)V

    :cond_3
    :goto_0
    return-void
.end method

.method private static setSecondaryButtonText(Landroid/widget/Button;I)V
    .locals 1

    .line 59
    invoke-virtual {p0}, Landroid/widget/Button;->getResources()Landroid/content/res/Resources;

    move-result-object v0

    packed-switch p1, :pswitch_data_0

    const-string p1, ""

    goto :goto_0

    .line 69
    :pswitch_0
    sget p1, Lcom/helpshift/R$string;->hs__send_msg_btn:I

    invoke-virtual {v0, p1}, Landroid/content/res/Resources;->getString(I)Ljava/lang/String;

    move-result-object p1

    goto :goto_0

    .line 66
    :pswitch_1
    sget p1, Lcom/helpshift/R$string;->hs__screenshot_remove:I

    invoke-virtual {v0, p1}, Landroid/content/res/Resources;->getString(I)Ljava/lang/String;

    move-result-object p1

    goto :goto_0

    .line 63
    :pswitch_2
    sget p1, Lcom/helpshift/R$string;->hs__screenshot_add:I

    invoke-virtual {v0, p1}, Landroid/content/res/Resources;->getString(I)Ljava/lang/String;

    move-result-object p1

    .line 75
    :goto_0
    invoke-virtual {p0, p1}, Landroid/widget/Button;->setText(Ljava/lang/CharSequence;)V

    return-void

    :pswitch_data_0
    .packed-switch 0x1
        :pswitch_2
        :pswitch_1
        :pswitch_0
    .end packed-switch
.end method


# virtual methods
.method public deleteAttachmentLocalCopy()V
    .locals 2

    .line 162
    iget-object v0, p0, Lcom/helpshift/support/fragments/ScreenshotPreviewFragment;->launchSource:Lcom/helpshift/support/fragments/ScreenshotPreviewFragment$LaunchSource;

    sget-object v1, Lcom/helpshift/support/fragments/ScreenshotPreviewFragment$LaunchSource;->GALLERY_APP:Lcom/helpshift/support/fragments/ScreenshotPreviewFragment$LaunchSource;

    if-ne v0, v1, :cond_0

    .line 163
    invoke-static {}, Lcom/helpshift/util/HelpshiftContext;->getCoreApi()Lcom/helpshift/CoreApi;

    move-result-object v0

    invoke-interface {v0}, Lcom/helpshift/CoreApi;->getAttachmentFileManagerDM()Lcom/helpshift/common/domain/AttachmentFileManagerDM;

    move-result-object v0

    iget-object v1, p0, Lcom/helpshift/support/fragments/ScreenshotPreviewFragment;->imagePickerFile:Lcom/helpshift/conversation/dto/ImagePickerFile;

    invoke-virtual {v0, v1}, Lcom/helpshift/common/domain/AttachmentFileManagerDM;->deleteAttachmentLocalCopy(Lcom/helpshift/conversation/dto/ImagePickerFile;)V

    :cond_0
    return-void
.end method

.method public onAuthenticationFailure()V
    .locals 1

    .line 276
    invoke-virtual {p0}, Lcom/helpshift/support/fragments/ScreenshotPreviewFragment;->getParentFragment()Landroidx/fragment/app/Fragment;

    move-result-object v0

    check-cast v0, Lcom/helpshift/support/fragments/SupportFragment;

    invoke-virtual {v0}, Lcom/helpshift/support/fragments/SupportFragment;->getSupportController()Lcom/helpshift/support/controllers/SupportController;

    move-result-object v0

    if-eqz v0, :cond_0

    .line 278
    invoke-virtual {v0}, Lcom/helpshift/support/controllers/SupportController;->onAuthenticationFailure()V

    :cond_0
    return-void
.end method

.method public onClick(Landroid/view/View;)V
    .locals 2

    .line 203
    invoke-virtual {p1}, Landroid/view/View;->getId()I

    move-result p1

    .line 204
    sget v0, Lcom/helpshift/R$id;->secondary_button:I

    if-ne p1, v0, :cond_0

    iget-object v0, p0, Lcom/helpshift/support/fragments/ScreenshotPreviewFragment;->imagePickerFile:Lcom/helpshift/conversation/dto/ImagePickerFile;

    if-eqz v0, :cond_0

    .line 205
    iget p1, p0, Lcom/helpshift/support/fragments/ScreenshotPreviewFragment;->mode:I

    packed-switch p1, :pswitch_data_0

    goto :goto_0

    .line 210
    :pswitch_0
    iget-object p1, p0, Lcom/helpshift/support/fragments/ScreenshotPreviewFragment;->screenshotPreviewListener:Lcom/helpshift/support/contracts/ScreenshotPreviewListener;

    iget-object v0, p0, Lcom/helpshift/support/fragments/ScreenshotPreviewFragment;->imagePickerFile:Lcom/helpshift/conversation/dto/ImagePickerFile;

    iget-object v1, p0, Lcom/helpshift/support/fragments/ScreenshotPreviewFragment;->attachmentMessageRefersId:Ljava/lang/String;

    invoke-interface {p1, v0, v1}, Lcom/helpshift/support/contracts/ScreenshotPreviewListener;->sendScreenshot(Lcom/helpshift/conversation/dto/ImagePickerFile;Ljava/lang/String;)V

    goto :goto_0

    .line 214
    :pswitch_1
    invoke-static {}, Lcom/helpshift/util/HelpshiftContext;->getCoreApi()Lcom/helpshift/CoreApi;

    move-result-object p1

    invoke-interface {p1}, Lcom/helpshift/CoreApi;->getAttachmentFileManagerDM()Lcom/helpshift/common/domain/AttachmentFileManagerDM;

    move-result-object p1

    iget-object v0, p0, Lcom/helpshift/support/fragments/ScreenshotPreviewFragment;->imagePickerFile:Lcom/helpshift/conversation/dto/ImagePickerFile;

    invoke-virtual {p1, v0}, Lcom/helpshift/common/domain/AttachmentFileManagerDM;->deleteAttachmentLocalCopy(Lcom/helpshift/conversation/dto/ImagePickerFile;)V

    .line 215
    iget-object p1, p0, Lcom/helpshift/support/fragments/ScreenshotPreviewFragment;->screenshotPreviewListener:Lcom/helpshift/support/contracts/ScreenshotPreviewListener;

    invoke-interface {p1}, Lcom/helpshift/support/contracts/ScreenshotPreviewListener;->removeScreenshot()V

    goto :goto_0

    .line 207
    :pswitch_2
    iget-object p1, p0, Lcom/helpshift/support/fragments/ScreenshotPreviewFragment;->screenshotPreviewListener:Lcom/helpshift/support/contracts/ScreenshotPreviewListener;

    iget-object v0, p0, Lcom/helpshift/support/fragments/ScreenshotPreviewFragment;->imagePickerFile:Lcom/helpshift/conversation/dto/ImagePickerFile;

    invoke-interface {p1, v0}, Lcom/helpshift/support/contracts/ScreenshotPreviewListener;->addScreenshot(Lcom/helpshift/conversation/dto/ImagePickerFile;)V

    goto :goto_0

    .line 219
    :cond_0
    sget v0, Lcom/helpshift/R$id;->change:I

    if-ne p1, v0, :cond_2

    .line 220
    iget p1, p0, Lcom/helpshift/support/fragments/ScreenshotPreviewFragment;->mode:I

    const/4 v0, 0x2

    if-ne p1, v0, :cond_1

    const/4 p1, 0x1

    .line 221
    iput p1, p0, Lcom/helpshift/support/fragments/ScreenshotPreviewFragment;->mode:I

    .line 223
    :cond_1
    invoke-static {}, Lcom/helpshift/util/HelpshiftContext;->getCoreApi()Lcom/helpshift/CoreApi;

    move-result-object p1

    invoke-interface {p1}, Lcom/helpshift/CoreApi;->getAttachmentFileManagerDM()Lcom/helpshift/common/domain/AttachmentFileManagerDM;

    move-result-object p1

    iget-object v0, p0, Lcom/helpshift/support/fragments/ScreenshotPreviewFragment;->imagePickerFile:Lcom/helpshift/conversation/dto/ImagePickerFile;

    invoke-virtual {p1, v0}, Lcom/helpshift/common/domain/AttachmentFileManagerDM;->deleteAttachmentLocalCopy(Lcom/helpshift/conversation/dto/ImagePickerFile;)V

    .line 224
    new-instance p1, Landroid/os/Bundle;

    invoke-direct {p1}, Landroid/os/Bundle;-><init>()V

    const-string v0, "key_screenshot_mode"

    .line 225
    iget v1, p0, Lcom/helpshift/support/fragments/ScreenshotPreviewFragment;->mode:I

    invoke-virtual {p1, v0, v1}, Landroid/os/Bundle;->putInt(Ljava/lang/String;I)V

    const-string v0, "key_refers_id"

    .line 226
    iget-object v1, p0, Lcom/helpshift/support/fragments/ScreenshotPreviewFragment;->attachmentMessageRefersId:Ljava/lang/String;

    invoke-virtual {p1, v0, v1}, Landroid/os/Bundle;->putString(Ljava/lang/String;Ljava/lang/String;)V

    .line 227
    iget-object v0, p0, Lcom/helpshift/support/fragments/ScreenshotPreviewFragment;->screenshotPreviewListener:Lcom/helpshift/support/contracts/ScreenshotPreviewListener;

    invoke-interface {v0, p1}, Lcom/helpshift/support/contracts/ScreenshotPreviewListener;->changeScreenshot(Landroid/os/Bundle;)V

    :cond_2
    :goto_0
    return-void

    nop

    :pswitch_data_0
    .packed-switch 0x1
        :pswitch_2
        :pswitch_1
        :pswitch_0
    .end packed-switch
.end method

.method public onCompressAndCopyFailure(Lcom/helpshift/common/exception/RootAPIException;)V
    .locals 1

    .line 233
    invoke-virtual {p0}, Lcom/helpshift/support/fragments/ScreenshotPreviewFragment;->getActivity()Landroidx/fragment/app/FragmentActivity;

    move-result-object p1

    if-eqz p1, :cond_0

    .line 234
    invoke-virtual {p0}, Lcom/helpshift/support/fragments/ScreenshotPreviewFragment;->getActivity()Landroidx/fragment/app/FragmentActivity;

    move-result-object p1

    new-instance v0, Lcom/helpshift/support/fragments/ScreenshotPreviewFragment$1;

    invoke-direct {v0, p0}, Lcom/helpshift/support/fragments/ScreenshotPreviewFragment$1;-><init>(Lcom/helpshift/support/fragments/ScreenshotPreviewFragment;)V

    invoke-virtual {p1, v0}, Landroidx/fragment/app/FragmentActivity;->runOnUiThread(Ljava/lang/Runnable;)V

    :cond_0
    return-void
.end method

.method public onCompressAndCopySuccess(Lcom/helpshift/conversation/dto/ImagePickerFile;)V
    .locals 2

    .line 248
    invoke-virtual {p0}, Lcom/helpshift/support/fragments/ScreenshotPreviewFragment;->getActivity()Landroidx/fragment/app/FragmentActivity;

    move-result-object v0

    if-eqz v0, :cond_0

    .line 249
    invoke-virtual {p0}, Lcom/helpshift/support/fragments/ScreenshotPreviewFragment;->getActivity()Landroidx/fragment/app/FragmentActivity;

    move-result-object v0

    new-instance v1, Lcom/helpshift/support/fragments/ScreenshotPreviewFragment$2;

    invoke-direct {v1, p0, p1}, Lcom/helpshift/support/fragments/ScreenshotPreviewFragment$2;-><init>(Lcom/helpshift/support/fragments/ScreenshotPreviewFragment;Lcom/helpshift/conversation/dto/ImagePickerFile;)V

    invoke-virtual {v0, v1}, Landroidx/fragment/app/FragmentActivity;->runOnUiThread(Ljava/lang/Runnable;)V

    :cond_0
    return-void
.end method

.method public onCreateView(Landroid/view/LayoutInflater;Landroid/view/ViewGroup;Landroid/os/Bundle;)Landroid/view/View;
    .locals 1
    .annotation build Landroidx/annotation/Nullable;
    .end annotation

    .line 95
    sget p3, Lcom/helpshift/R$layout;->hs__screenshot_preview_fragment:I

    const/4 v0, 0x0

    invoke-virtual {p1, p3, p2, v0}, Landroid/view/LayoutInflater;->inflate(ILandroid/view/ViewGroup;Z)Landroid/view/View;

    move-result-object p1

    return-object p1
.end method

.method public onDestroyView()V
    .locals 1

    .line 127
    iget-object v0, p0, Lcom/helpshift/support/fragments/ScreenshotPreviewFragment;->screenshotPreviewVM:Lcom/helpshift/conversation/viewmodel/ScreenshotPreviewVM;

    invoke-virtual {v0}, Lcom/helpshift/conversation/viewmodel/ScreenshotPreviewVM;->unregisterRenderer()V

    .line 128
    invoke-super {p0}, Lcom/helpshift/support/fragments/MainFragment;->onDestroyView()V

    return-void
.end method

.method public onPause()V
    .locals 1

    .line 140
    invoke-virtual {p0}, Lcom/helpshift/support/fragments/ScreenshotPreviewFragment;->getView()Landroid/view/View;

    move-result-object v0

    invoke-static {v0}, Lcom/helpshift/support/util/SnackbarUtil;->hideSnackbar(Landroid/view/View;)V

    .line 141
    invoke-super {p0}, Lcom/helpshift/support/fragments/MainFragment;->onPause()V

    return-void
.end method

.method public onResume()V
    .locals 2

    .line 118
    invoke-super {p0}, Lcom/helpshift/support/fragments/MainFragment;->onResume()V

    .line 119
    iget-object v0, p0, Lcom/helpshift/support/fragments/ScreenshotPreviewFragment;->secondaryButton:Landroid/widget/Button;

    iget v1, p0, Lcom/helpshift/support/fragments/ScreenshotPreviewFragment;->mode:I

    invoke-static {v0, v1}, Lcom/helpshift/support/fragments/ScreenshotPreviewFragment;->setSecondaryButtonText(Landroid/widget/Button;I)V

    .line 120
    invoke-direct {p0}, Lcom/helpshift/support/fragments/ScreenshotPreviewFragment;->setScreenshotPreview()V

    .line 121
    invoke-virtual {p0}, Lcom/helpshift/support/fragments/ScreenshotPreviewFragment;->getView()Landroid/view/View;

    move-result-object v0

    const/4 v1, 0x1

    invoke-virtual {v0, v1}, Landroid/view/View;->setFocusableInTouchMode(Z)V

    .line 122
    invoke-virtual {p0}, Lcom/helpshift/support/fragments/ScreenshotPreviewFragment;->getView()Landroid/view/View;

    move-result-object v0

    invoke-virtual {v0}, Landroid/view/View;->requestFocus()Z

    return-void
.end method

.method public onStart()V
    .locals 3

    .line 133
    invoke-super {p0}, Lcom/helpshift/support/fragments/MainFragment;->onStart()V

    .line 134
    invoke-static {}, Lcom/helpshift/support/storage/IMAppSessionStorage;->getInstance()Lcom/helpshift/support/storage/IMAppSessionStorage;

    move-result-object v0

    const-string v1, "current_open_screen"

    sget-object v2, Lcom/helpshift/support/fragments/ScreenshotPreviewFragment;->screenType:Lcom/helpshift/support/util/AppSessionConstants$Screen;

    invoke-virtual {v0, v1, v2}, Lcom/helpshift/support/storage/IMAppSessionStorage;->set(Ljava/lang/String;Ljava/io/Serializable;)Z

    return-void
.end method

.method public onStop()V
    .locals 2

    .line 146
    invoke-super {p0}, Lcom/helpshift/support/fragments/MainFragment;->onStop()V

    .line 147
    invoke-static {}, Lcom/helpshift/support/storage/IMAppSessionStorage;->getInstance()Lcom/helpshift/support/storage/IMAppSessionStorage;

    move-result-object v0

    const-string v1, "current_open_screen"

    .line 148
    invoke-virtual {v0, v1}, Lcom/helpshift/support/storage/IMAppSessionStorage;->get(Ljava/lang/String;)Ljava/io/Serializable;

    move-result-object v0

    check-cast v0, Lcom/helpshift/support/util/AppSessionConstants$Screen;

    if-eqz v0, :cond_0

    .line 149
    sget-object v1, Lcom/helpshift/support/fragments/ScreenshotPreviewFragment;->screenType:Lcom/helpshift/support/util/AppSessionConstants$Screen;

    invoke-virtual {v0, v1}, Lcom/helpshift/support/util/AppSessionConstants$Screen;->equals(Ljava/lang/Object;)Z

    move-result v0

    if-eqz v0, :cond_0

    .line 150
    invoke-static {}, Lcom/helpshift/support/storage/IMAppSessionStorage;->getInstance()Lcom/helpshift/support/storage/IMAppSessionStorage;

    move-result-object v0

    const-string v1, "current_open_screen"

    invoke-virtual {v0, v1}, Lcom/helpshift/support/storage/IMAppSessionStorage;->removeKey(Ljava/lang/String;)V

    :cond_0
    return-void
.end method

.method public onViewCreated(Landroid/view/View;Landroid/os/Bundle;)V
    .locals 0

    .line 100
    invoke-super {p0, p1, p2}, Lcom/helpshift/support/fragments/MainFragment;->onViewCreated(Landroid/view/View;Landroid/os/Bundle;)V

    .line 101
    invoke-static {}, Lcom/helpshift/util/HelpshiftContext;->getCoreApi()Lcom/helpshift/CoreApi;

    move-result-object p2

    invoke-interface {p2, p0}, Lcom/helpshift/CoreApi;->getScreenshotPreviewModel(Lcom/helpshift/conversation/activeconversation/ScreenshotPreviewRenderer;)Lcom/helpshift/conversation/viewmodel/ScreenshotPreviewVM;

    move-result-object p2

    iput-object p2, p0, Lcom/helpshift/support/fragments/ScreenshotPreviewFragment;->screenshotPreviewVM:Lcom/helpshift/conversation/viewmodel/ScreenshotPreviewVM;

    .line 102
    sget p2, Lcom/helpshift/R$id;->screenshot_preview:I

    invoke-virtual {p1, p2}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object p2

    check-cast p2, Landroid/widget/ImageView;

    iput-object p2, p0, Lcom/helpshift/support/fragments/ScreenshotPreviewFragment;->screenshotPreview:Landroid/widget/ImageView;

    .line 104
    sget p2, Lcom/helpshift/R$id;->change:I

    invoke-virtual {p1, p2}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object p2

    check-cast p2, Landroid/widget/Button;

    .line 105
    invoke-virtual {p2, p0}, Landroid/widget/Button;->setOnClickListener(Landroid/view/View$OnClickListener;)V

    .line 107
    sget p2, Lcom/helpshift/R$id;->secondary_button:I

    invoke-virtual {p1, p2}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object p2

    check-cast p2, Landroid/widget/Button;

    iput-object p2, p0, Lcom/helpshift/support/fragments/ScreenshotPreviewFragment;->secondaryButton:Landroid/widget/Button;

    .line 108
    iget-object p2, p0, Lcom/helpshift/support/fragments/ScreenshotPreviewFragment;->secondaryButton:Landroid/widget/Button;

    invoke-virtual {p2, p0}, Landroid/widget/Button;->setOnClickListener(Landroid/view/View$OnClickListener;)V

    .line 110
    sget p2, Lcom/helpshift/R$id;->screenshot_loading_indicator:I

    invoke-virtual {p1, p2}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object p2

    check-cast p2, Landroid/widget/ProgressBar;

    iput-object p2, p0, Lcom/helpshift/support/fragments/ScreenshotPreviewFragment;->progressBar:Landroid/widget/ProgressBar;

    .line 112
    sget p2, Lcom/helpshift/R$id;->button_containers:I

    invoke-virtual {p1, p2}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object p2

    iput-object p2, p0, Lcom/helpshift/support/fragments/ScreenshotPreviewFragment;->buttonsContainer:Landroid/view/View;

    .line 113
    sget p2, Lcom/helpshift/R$id;->buttons_separator:I

    invoke-virtual {p1, p2}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object p1

    iput-object p1, p0, Lcom/helpshift/support/fragments/ScreenshotPreviewFragment;->buttonsSeparator:Landroid/view/View;

    return-void
.end method

.method renderScreenshotPreview(Ljava/lang/String;)V
    .locals 2

    .line 192
    invoke-virtual {p0}, Lcom/helpshift/support/fragments/ScreenshotPreviewFragment;->getView()Landroid/view/View;

    move-result-object v0

    invoke-virtual {v0}, Landroid/view/View;->isHardwareAccelerated()Z

    move-result v0

    const/4 v1, -0x1

    invoke-static {p1, v1, v0}, Lcom/helpshift/support/util/AttachmentUtil;->getBitmap(Ljava/lang/String;IZ)Landroid/graphics/Bitmap;

    move-result-object p1

    if-eqz p1, :cond_0

    .line 194
    iget-object v0, p0, Lcom/helpshift/support/fragments/ScreenshotPreviewFragment;->screenshotPreview:Landroid/widget/ImageView;

    invoke-virtual {v0, p1}, Landroid/widget/ImageView;->setImageBitmap(Landroid/graphics/Bitmap;)V

    goto :goto_0

    .line 196
    :cond_0
    iget-object p1, p0, Lcom/helpshift/support/fragments/ScreenshotPreviewFragment;->screenshotPreviewListener:Lcom/helpshift/support/contracts/ScreenshotPreviewListener;

    if-eqz p1, :cond_1

    .line 197
    iget-object p1, p0, Lcom/helpshift/support/fragments/ScreenshotPreviewFragment;->screenshotPreviewListener:Lcom/helpshift/support/contracts/ScreenshotPreviewListener;

    invoke-interface {p1}, Lcom/helpshift/support/contracts/ScreenshotPreviewListener;->removeScreenshotPreviewFragment()V

    :cond_1
    :goto_0
    return-void
.end method

.method public setParams(Landroid/os/Bundle;Lcom/helpshift/conversation/dto/ImagePickerFile;Lcom/helpshift/support/fragments/ScreenshotPreviewFragment$LaunchSource;)V
    .locals 1
    .param p1    # Landroid/os/Bundle;
        .annotation build Landroidx/annotation/NonNull;
        .end annotation
    .end param

    const-string v0, "key_screenshot_mode"

    .line 80
    invoke-virtual {p1, v0}, Landroid/os/Bundle;->getInt(Ljava/lang/String;)I

    move-result v0

    iput v0, p0, Lcom/helpshift/support/fragments/ScreenshotPreviewFragment;->mode:I

    const-string v0, "key_refers_id"

    .line 81
    invoke-virtual {p1, v0}, Landroid/os/Bundle;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object p1

    iput-object p1, p0, Lcom/helpshift/support/fragments/ScreenshotPreviewFragment;->attachmentMessageRefersId:Ljava/lang/String;

    .line 82
    iput-object p2, p0, Lcom/helpshift/support/fragments/ScreenshotPreviewFragment;->imagePickerFile:Lcom/helpshift/conversation/dto/ImagePickerFile;

    .line 83
    iput-object p3, p0, Lcom/helpshift/support/fragments/ScreenshotPreviewFragment;->launchSource:Lcom/helpshift/support/fragments/ScreenshotPreviewFragment$LaunchSource;

    .line 84
    invoke-direct {p0}, Lcom/helpshift/support/fragments/ScreenshotPreviewFragment;->setScreenshotPreview()V

    return-void
.end method

.method public setScreenshotPreviewListener(Lcom/helpshift/support/contracts/ScreenshotPreviewListener;)V
    .locals 0

    .line 88
    iput-object p1, p0, Lcom/helpshift/support/fragments/ScreenshotPreviewFragment;->screenshotPreviewListener:Lcom/helpshift/support/contracts/ScreenshotPreviewListener;

    return-void
.end method

.method public shouldRefreshMenu()Z
    .locals 1

    const/4 v0, 0x1

    return v0
.end method

.method toggleProgressBarViewsVisibility(Z)V
    .locals 2

    const/16 v0, 0x8

    const/4 v1, 0x0

    if-eqz p1, :cond_0

    .line 261
    iget-object p1, p0, Lcom/helpshift/support/fragments/ScreenshotPreviewFragment;->progressBar:Landroid/widget/ProgressBar;

    invoke-virtual {p1, v1}, Landroid/widget/ProgressBar;->setVisibility(I)V

    .line 262
    iget-object p1, p0, Lcom/helpshift/support/fragments/ScreenshotPreviewFragment;->buttonsContainer:Landroid/view/View;

    invoke-virtual {p1, v0}, Landroid/view/View;->setVisibility(I)V

    .line 263
    iget-object p1, p0, Lcom/helpshift/support/fragments/ScreenshotPreviewFragment;->buttonsSeparator:Landroid/view/View;

    invoke-virtual {p1, v0}, Landroid/view/View;->setVisibility(I)V

    .line 264
    iget-object p1, p0, Lcom/helpshift/support/fragments/ScreenshotPreviewFragment;->screenshotPreview:Landroid/widget/ImageView;

    invoke-virtual {p1, v0}, Landroid/widget/ImageView;->setVisibility(I)V

    goto :goto_0

    .line 267
    :cond_0
    iget-object p1, p0, Lcom/helpshift/support/fragments/ScreenshotPreviewFragment;->progressBar:Landroid/widget/ProgressBar;

    invoke-virtual {p1, v0}, Landroid/widget/ProgressBar;->setVisibility(I)V

    .line 268
    iget-object p1, p0, Lcom/helpshift/support/fragments/ScreenshotPreviewFragment;->buttonsContainer:Landroid/view/View;

    invoke-virtual {p1, v1}, Landroid/view/View;->setVisibility(I)V

    .line 269
    iget-object p1, p0, Lcom/helpshift/support/fragments/ScreenshotPreviewFragment;->buttonsSeparator:Landroid/view/View;

    invoke-virtual {p1, v1}, Landroid/view/View;->setVisibility(I)V

    .line 270
    iget-object p1, p0, Lcom/helpshift/support/fragments/ScreenshotPreviewFragment;->screenshotPreview:Landroid/widget/ImageView;

    invoke-virtual {p1, v1}, Landroid/widget/ImageView;->setVisibility(I)V

    :goto_0
    return-void
.end method
