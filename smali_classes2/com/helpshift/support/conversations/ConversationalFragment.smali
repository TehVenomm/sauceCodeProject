.class public Lcom/helpshift/support/conversations/ConversationalFragment;
.super Lcom/helpshift/support/conversations/BaseConversationFragment;
.source "ConversationalFragment.java"

# interfaces
.implements Lcom/helpshift/support/conversations/messages/MessagesAdapterClickListener;
.implements Lcom/helpshift/support/conversations/ConversationalFragmentRouter;
.implements Lcom/helpshift/support/fragments/IMenuItemEventListener;
.implements Lcom/helpshift/support/conversations/HSRecyclerViewScrollListener$RecyclerViewScrollCallback;
.implements Lcom/helpshift/network/connectivity/HSNetworkConnectivityCallback;


# static fields
.field public static final BUNDLE_ARG_CONVERSATION_LOCAL_ID:Ljava/lang/String; = "issueId"

.field public static final BUNDLE_ARG_SHOW_CONVERSATION_HISTORY:Ljava/lang/String; = "show_conv_history"

.field public static final FRAGMENT_TAG:Ljava/lang/String; = "HSConversationFragment"

.field private static final TAG:Ljava/lang/String; = "Helpshift_ConvalFrag"


# instance fields
.field private final SHOULD_SHOW_UNREAD_MESSAGE_INDICATOR:Ljava/lang/String;

.field protected conversationId:Ljava/lang/Long;

.field conversationalVM:Lcom/helpshift/conversation/viewmodel/ConversationalVM;

.field private hsRecyclerViewScrollListener:Lcom/helpshift/support/conversations/HSRecyclerViewScrollListener;

.field private imageRefersId:Ljava/lang/String;

.field private isConversationVMInitialized:Z

.field private lastSoftInputMode:I

.field private lastWindowFlags:I

.field private messagesRecyclerView:Landroidx/recyclerview/widget/RecyclerView;

.field private readableAttachmentMessage:Lcom/helpshift/conversation/activeconversation/message/AttachmentMessageDM;

.field protected renderer:Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;

.field protected retainMessageBoxOnUI:Z

.field private selectedImageFile:Lcom/helpshift/conversation/dto/ImagePickerFile;

.field private selectedImageRefersId:Ljava/lang/String;

.field private shouldShowConversationHistory:Z

.field private shouldUpdateAttachment:Z


# direct methods
.method public constructor <init>()V
    .locals 1

    .line 81
    invoke-direct {p0}, Lcom/helpshift/support/conversations/BaseConversationFragment;-><init>()V

    const-string v0, "should_show_unread_message_indicator"

    .line 91
    iput-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragment;->SHOULD_SHOW_UNREAD_MESSAGE_INDICATOR:Ljava/lang/String;

    const/4 v0, 0x0

    .line 103
    iput-boolean v0, p0, Lcom/helpshift/support/conversations/ConversationalFragment;->isConversationVMInitialized:Z

    return-void
.end method

.method static synthetic access$000(Lcom/helpshift/support/conversations/ConversationalFragment;Ljava/lang/String;)V
    .locals 0

    .line 81
    invoke-virtual {p0, p1}, Lcom/helpshift/support/conversations/ConversationalFragment;->copyToClipboard(Ljava/lang/String;)V

    return-void
.end method

.method private addViewStateObservers()V
    .locals 3

    .line 237
    invoke-static {}, Lcom/helpshift/util/HelpshiftContext;->getCoreApi()Lcom/helpshift/CoreApi;

    move-result-object v0

    invoke-interface {v0}, Lcom/helpshift/CoreApi;->getDomain()Lcom/helpshift/common/domain/Domain;

    move-result-object v0

    .line 241
    iget-object v1, p0, Lcom/helpshift/support/conversations/ConversationalFragment;->conversationalVM:Lcom/helpshift/conversation/viewmodel/ConversationalVM;

    invoke-virtual {v1}, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->getReplyFieldViewState()Lcom/helpshift/widget/ReplyFieldViewState;

    move-result-object v1

    new-instance v2, Lcom/helpshift/support/conversations/ConversationalFragment$4;

    invoke-direct {v2, p0}, Lcom/helpshift/support/conversations/ConversationalFragment$4;-><init>(Lcom/helpshift/support/conversations/ConversationalFragment;)V

    invoke-virtual {v1, v0, v2}, Lcom/helpshift/widget/ReplyFieldViewState;->subscribe(Lcom/helpshift/common/domain/Domain;Lcom/helpshift/widget/HSObserver;)V

    .line 251
    iget-object v1, p0, Lcom/helpshift/support/conversations/ConversationalFragment;->conversationalVM:Lcom/helpshift/conversation/viewmodel/ConversationalVM;

    invoke-virtual {v1}, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->getHistoryLoadingViewState()Lcom/helpshift/widget/HistoryLoadingViewState;

    move-result-object v1

    new-instance v2, Lcom/helpshift/support/conversations/ConversationalFragment$5;

    invoke-direct {v2, p0}, Lcom/helpshift/support/conversations/ConversationalFragment$5;-><init>(Lcom/helpshift/support/conversations/ConversationalFragment;)V

    invoke-virtual {v1, v0, v2}, Lcom/helpshift/widget/HistoryLoadingViewState;->subscribe(Lcom/helpshift/common/domain/Domain;Lcom/helpshift/widget/HSObserver;)V

    .line 260
    iget-object v1, p0, Lcom/helpshift/support/conversations/ConversationalFragment;->conversationalVM:Lcom/helpshift/conversation/viewmodel/ConversationalVM;

    invoke-virtual {v1}, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->getScrollJumperViewState()Lcom/helpshift/widget/ScrollJumperViewState;

    move-result-object v1

    new-instance v2, Lcom/helpshift/support/conversations/ConversationalFragment$6;

    invoke-direct {v2, p0}, Lcom/helpshift/support/conversations/ConversationalFragment$6;-><init>(Lcom/helpshift/support/conversations/ConversationalFragment;)V

    invoke-virtual {v1, v0, v2}, Lcom/helpshift/widget/ScrollJumperViewState;->subscribe(Lcom/helpshift/common/domain/Domain;Lcom/helpshift/widget/HSObserver;)V

    .line 269
    iget-object v1, p0, Lcom/helpshift/support/conversations/ConversationalFragment;->conversationalVM:Lcom/helpshift/conversation/viewmodel/ConversationalVM;

    invoke-virtual {v1}, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->getConversationFooterViewState()Lcom/helpshift/widget/ConversationFooterViewState;

    move-result-object v1

    new-instance v2, Lcom/helpshift/support/conversations/ConversationalFragment$7;

    invoke-direct {v2, p0}, Lcom/helpshift/support/conversations/ConversationalFragment$7;-><init>(Lcom/helpshift/support/conversations/ConversationalFragment;)V

    invoke-virtual {v1, v0, v2}, Lcom/helpshift/widget/ConversationFooterViewState;->subscribe(Lcom/helpshift/common/domain/Domain;Lcom/helpshift/widget/HSObserver;)V

    .line 278
    iget-object v1, p0, Lcom/helpshift/support/conversations/ConversationalFragment;->conversationalVM:Lcom/helpshift/conversation/viewmodel/ConversationalVM;

    invoke-virtual {v1}, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->getReplyBoxViewState()Lcom/helpshift/widget/ReplyBoxViewState;

    move-result-object v1

    new-instance v2, Lcom/helpshift/support/conversations/ConversationalFragment$8;

    invoke-direct {v2, p0}, Lcom/helpshift/support/conversations/ConversationalFragment$8;-><init>(Lcom/helpshift/support/conversations/ConversationalFragment;)V

    invoke-virtual {v1, v0, v2}, Lcom/helpshift/widget/ReplyBoxViewState;->subscribe(Lcom/helpshift/common/domain/Domain;Lcom/helpshift/widget/HSObserver;)V

    .line 287
    iget-object v1, p0, Lcom/helpshift/support/conversations/ConversationalFragment;->conversationalVM:Lcom/helpshift/conversation/viewmodel/ConversationalVM;

    invoke-virtual {v1}, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->getReplyButtonViewState()Lcom/helpshift/widget/BaseViewState;

    move-result-object v1

    new-instance v2, Lcom/helpshift/support/conversations/ConversationalFragment$9;

    invoke-direct {v2, p0}, Lcom/helpshift/support/conversations/ConversationalFragment$9;-><init>(Lcom/helpshift/support/conversations/ConversationalFragment;)V

    invoke-virtual {v1, v0, v2}, Lcom/helpshift/widget/BaseViewState;->subscribe(Lcom/helpshift/common/domain/Domain;Lcom/helpshift/widget/HSObserver;)V

    .line 296
    iget-object v1, p0, Lcom/helpshift/support/conversations/ConversationalFragment;->conversationalVM:Lcom/helpshift/conversation/viewmodel/ConversationalVM;

    invoke-virtual {v1}, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->getAttachImageButtonViewState()Lcom/helpshift/widget/BaseViewState;

    move-result-object v1

    new-instance v2, Lcom/helpshift/support/conversations/ConversationalFragment$10;

    invoke-direct {v2, p0}, Lcom/helpshift/support/conversations/ConversationalFragment$10;-><init>(Lcom/helpshift/support/conversations/ConversationalFragment;)V

    invoke-virtual {v1, v0, v2}, Lcom/helpshift/widget/BaseViewState;->subscribe(Lcom/helpshift/common/domain/Domain;Lcom/helpshift/widget/HSObserver;)V

    .line 305
    iget-object v1, p0, Lcom/helpshift/support/conversations/ConversationalFragment;->conversationalVM:Lcom/helpshift/conversation/viewmodel/ConversationalVM;

    invoke-virtual {v1}, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->getConfirmationBoxViewState()Lcom/helpshift/widget/BaseViewState;

    move-result-object v1

    new-instance v2, Lcom/helpshift/support/conversations/ConversationalFragment$11;

    invoke-direct {v2, p0}, Lcom/helpshift/support/conversations/ConversationalFragment$11;-><init>(Lcom/helpshift/support/conversations/ConversationalFragment;)V

    invoke-virtual {v1, v0, v2}, Lcom/helpshift/widget/BaseViewState;->subscribe(Lcom/helpshift/common/domain/Domain;Lcom/helpshift/widget/HSObserver;)V

    return-void
.end method

.method private checkWriteStoragePermissionAndDelegateToVM(ZLcom/helpshift/conversation/activeconversation/message/AttachmentMessageDM;)V
    .locals 2

    const/4 v0, 0x0

    .line 613
    iput-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragment;->readableAttachmentMessage:Lcom/helpshift/conversation/activeconversation/message/AttachmentMessageDM;

    if-eqz p1, :cond_0

    .line 615
    invoke-static {}, Lcom/helpshift/util/HelpshiftContext;->getPlatform()Lcom/helpshift/common/platform/Platform;

    move-result-object p1

    invoke-interface {p1}, Lcom/helpshift/common/platform/Platform;->getDevice()Lcom/helpshift/common/platform/Device;

    move-result-object p1

    .line 616
    sget-object v0, Lcom/helpshift/support/conversations/ConversationalFragment$14;->$SwitchMap$com$helpshift$common$platform$Device$PermissionState:[I

    sget-object v1, Lcom/helpshift/common/platform/Device$PermissionType;->WRITE_STORAGE:Lcom/helpshift/common/platform/Device$PermissionType;

    .line 617
    invoke-interface {p1, v1}, Lcom/helpshift/common/platform/Device;->checkPermission(Lcom/helpshift/common/platform/Device$PermissionType;)Lcom/helpshift/common/platform/Device$PermissionState;

    move-result-object p1

    .line 616
    invoke-virtual {p1}, Lcom/helpshift/common/platform/Device$PermissionState;->ordinal()I

    move-result p1

    aget p1, v0, p1

    packed-switch p1, :pswitch_data_0

    goto :goto_0

    .line 625
    :pswitch_0
    iput-object p2, p0, Lcom/helpshift/support/conversations/ConversationalFragment;->readableAttachmentMessage:Lcom/helpshift/conversation/activeconversation/message/AttachmentMessageDM;

    const/4 p1, 0x1

    .line 626
    invoke-virtual {p0, p1}, Lcom/helpshift/support/conversations/ConversationalFragment;->requestWriteExternalStoragePermission(Z)V

    goto :goto_0

    .line 622
    :pswitch_1
    iget-object p1, p2, Lcom/helpshift/conversation/activeconversation/message/AttachmentMessageDM;->attachmentUrl:Ljava/lang/String;

    invoke-direct {p0, p1}, Lcom/helpshift/support/conversations/ConversationalFragment;->startDownloadWithSystemService(Ljava/lang/String;)V

    goto :goto_0

    .line 619
    :pswitch_2
    iget-object p1, p0, Lcom/helpshift/support/conversations/ConversationalFragment;->conversationalVM:Lcom/helpshift/conversation/viewmodel/ConversationalVM;

    invoke-virtual {p1, p2}, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->handleAdminAttachmentMessageClick(Lcom/helpshift/conversation/activeconversation/message/AttachmentMessageDM;)V

    goto :goto_0

    .line 631
    :cond_0
    iget-object p1, p0, Lcom/helpshift/support/conversations/ConversationalFragment;->conversationalVM:Lcom/helpshift/conversation/viewmodel/ConversationalVM;

    invoke-virtual {p1, p2}, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->handleAdminAttachmentMessageClick(Lcom/helpshift/conversation/activeconversation/message/AttachmentMessageDM;)V

    :goto_0
    return-void

    :pswitch_data_0
    .packed-switch 0x1
        :pswitch_2
        :pswitch_1
        :pswitch_0
    .end packed-switch
.end method

.method private getParentWindow()Landroid/view/Window;
    .locals 3

    .line 353
    invoke-virtual {p0}, Lcom/helpshift/support/conversations/ConversationalFragment;->getParentFragment()Landroidx/fragment/app/Fragment;

    move-result-object v0

    const/4 v1, 0x5

    :goto_0
    add-int/lit8 v2, v1, -0x1

    if-lez v1, :cond_1

    if-eqz v0, :cond_1

    .line 360
    instance-of v1, v0, Landroidx/fragment/app/DialogFragment;

    if-eqz v1, :cond_0

    .line 361
    move-object v1, v0

    check-cast v1, Landroidx/fragment/app/DialogFragment;

    .line 362
    invoke-virtual {v1}, Landroidx/fragment/app/DialogFragment;->getDialog()Landroid/app/Dialog;

    move-result-object v1

    if-eqz v1, :cond_0

    .line 364
    invoke-virtual {v1}, Landroid/app/Dialog;->getWindow()Landroid/view/Window;

    move-result-object v0

    return-object v0

    .line 369
    :cond_0
    invoke-virtual {v0}, Landroidx/fragment/app/Fragment;->getParentFragment()Landroidx/fragment/app/Fragment;

    move-result-object v0

    move v1, v2

    goto :goto_0

    .line 373
    :cond_1
    invoke-virtual {p0}, Lcom/helpshift/support/conversations/ConversationalFragment;->getActivity()Landroidx/fragment/app/FragmentActivity;

    move-result-object v0

    invoke-virtual {v0}, Landroidx/fragment/app/FragmentActivity;->getWindow()Landroid/view/Window;

    move-result-object v0

    return-object v0
.end method

.method public static newInstance(Landroid/os/Bundle;)Lcom/helpshift/support/conversations/ConversationalFragment;
    .locals 1

    .line 112
    new-instance v0, Lcom/helpshift/support/conversations/ConversationalFragment;

    invoke-direct {v0}, Lcom/helpshift/support/conversations/ConversationalFragment;-><init>()V

    .line 113
    invoke-virtual {v0, p0}, Lcom/helpshift/support/conversations/ConversationalFragment;->setArguments(Landroid/os/Bundle;)V

    return-object v0
.end method

.method private removeViewStateObservers()V
    .locals 1

    .line 316
    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragment;->conversationalVM:Lcom/helpshift/conversation/viewmodel/ConversationalVM;

    invoke-virtual {v0}, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->getReplyFieldViewState()Lcom/helpshift/widget/ReplyFieldViewState;

    move-result-object v0

    invoke-virtual {v0}, Lcom/helpshift/widget/ReplyFieldViewState;->unsubscribe()V

    .line 317
    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragment;->conversationalVM:Lcom/helpshift/conversation/viewmodel/ConversationalVM;

    invoke-virtual {v0}, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->getHistoryLoadingViewState()Lcom/helpshift/widget/HistoryLoadingViewState;

    move-result-object v0

    invoke-virtual {v0}, Lcom/helpshift/widget/HistoryLoadingViewState;->unsubscribe()V

    .line 318
    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragment;->conversationalVM:Lcom/helpshift/conversation/viewmodel/ConversationalVM;

    invoke-virtual {v0}, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->getScrollJumperViewState()Lcom/helpshift/widget/ScrollJumperViewState;

    move-result-object v0

    invoke-virtual {v0}, Lcom/helpshift/widget/ScrollJumperViewState;->unsubscribe()V

    .line 319
    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragment;->conversationalVM:Lcom/helpshift/conversation/viewmodel/ConversationalVM;

    invoke-virtual {v0}, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->getConversationFooterViewState()Lcom/helpshift/widget/ConversationFooterViewState;

    move-result-object v0

    invoke-virtual {v0}, Lcom/helpshift/widget/ConversationFooterViewState;->unsubscribe()V

    .line 320
    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragment;->conversationalVM:Lcom/helpshift/conversation/viewmodel/ConversationalVM;

    invoke-virtual {v0}, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->getAttachImageButtonViewState()Lcom/helpshift/widget/BaseViewState;

    move-result-object v0

    invoke-virtual {v0}, Lcom/helpshift/widget/BaseViewState;->unsubscribe()V

    .line 321
    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragment;->conversationalVM:Lcom/helpshift/conversation/viewmodel/ConversationalVM;

    invoke-virtual {v0}, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->getReplyBoxViewState()Lcom/helpshift/widget/ReplyBoxViewState;

    move-result-object v0

    invoke-virtual {v0}, Lcom/helpshift/widget/ReplyBoxViewState;->unsubscribe()V

    .line 322
    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragment;->conversationalVM:Lcom/helpshift/conversation/viewmodel/ConversationalVM;

    invoke-virtual {v0}, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->getReplyButtonViewState()Lcom/helpshift/widget/BaseViewState;

    move-result-object v0

    invoke-virtual {v0}, Lcom/helpshift/widget/BaseViewState;->unsubscribe()V

    .line 323
    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragment;->conversationalVM:Lcom/helpshift/conversation/viewmodel/ConversationalVM;

    invoke-virtual {v0}, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->getConfirmationBoxViewState()Lcom/helpshift/widget/BaseViewState;

    move-result-object v0

    invoke-virtual {v0}, Lcom/helpshift/widget/BaseViewState;->unsubscribe()V

    return-void
.end method

.method private startDownloadWithSystemService(Ljava/lang/String;)V
    .locals 2

    .line 655
    invoke-virtual {p0}, Lcom/helpshift/support/conversations/ConversationalFragment;->getContext()Landroid/content/Context;

    move-result-object v0

    const-string v1, "download"

    invoke-virtual {v0, v1}, Landroid/content/Context;->getSystemService(Ljava/lang/String;)Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Landroid/app/DownloadManager;

    if-nez v0, :cond_0

    return-void

    .line 661
    :cond_0
    invoke-static {p1}, Landroid/net/Uri;->parse(Ljava/lang/String;)Landroid/net/Uri;

    move-result-object p1

    .line 662
    new-instance v1, Landroid/app/DownloadManager$Request;

    invoke-direct {v1, p1}, Landroid/app/DownloadManager$Request;-><init>(Landroid/net/Uri;)V

    const/4 p1, 0x1

    .line 663
    invoke-virtual {v1, p1}, Landroid/app/DownloadManager$Request;->setNotificationVisibility(I)Landroid/app/DownloadManager$Request;

    .line 664
    invoke-virtual {v0, v1}, Landroid/app/DownloadManager;->enqueue(Landroid/app/DownloadManager$Request;)J

    .line 665
    invoke-virtual {p0}, Lcom/helpshift/support/conversations/ConversationalFragment;->isDetached()Z

    move-result p1

    if-nez p1, :cond_1

    .line 666
    invoke-virtual {p0}, Lcom/helpshift/support/conversations/ConversationalFragment;->getView()Landroid/view/View;

    move-result-object p1

    sget v0, Lcom/helpshift/R$string;->hs__starting_download:I

    const/4 v1, -0x1

    invoke-static {p1, v0, v1}, Lcom/helpshift/support/util/SnackbarUtil;->showSnackbar(Landroid/view/View;II)V

    :cond_1
    return-void
.end method


# virtual methods
.method protected getScreenshotMode()I
    .locals 1

    const/4 v0, 0x3

    return v0
.end method

.method protected getToolbarTitle()Ljava/lang/String;
    .locals 1

    .line 542
    sget v0, Lcom/helpshift/R$string;->hs__conversation_header:I

    invoke-virtual {p0, v0}, Lcom/helpshift/support/conversations/ConversationalFragment;->getString(I)Ljava/lang/String;

    move-result-object v0

    return-object v0
.end method

.method protected getViewName()Lcom/helpshift/support/util/AppSessionConstants$Screen;
    .locals 1

    .line 502
    sget-object v0, Lcom/helpshift/support/util/AppSessionConstants$Screen;->CONVERSATION:Lcom/helpshift/support/util/AppSessionConstants$Screen;

    return-object v0
.end method

.method public handleAdminImageAttachmentMessageClick(Lcom/helpshift/conversation/activeconversation/message/AdminImageAttachmentMessageDM;)V
    .locals 1

    const/4 v0, 0x1

    .line 597
    invoke-direct {p0, v0, p1}, Lcom/helpshift/support/conversations/ConversationalFragment;->checkWriteStoragePermissionAndDelegateToVM(ZLcom/helpshift/conversation/activeconversation/message/AttachmentMessageDM;)V

    return-void
.end method

.method public handleGenericAttachmentMessageClick(Lcom/helpshift/conversation/activeconversation/message/AdminAttachmentMessageDM;)V
    .locals 1

    .line 591
    invoke-virtual {p1}, Lcom/helpshift/conversation/activeconversation/message/AdminAttachmentMessageDM;->isWriteStoragePermissionRequired()Z

    move-result v0

    invoke-direct {p0, v0, p1}, Lcom/helpshift/support/conversations/ConversationalFragment;->checkWriteStoragePermissionAndDelegateToVM(ZLcom/helpshift/conversation/activeconversation/message/AttachmentMessageDM;)V

    return-void
.end method

.method public handleOptionSelected(Lcom/helpshift/conversation/activeconversation/message/OptionInputMessageDM;Lcom/helpshift/conversation/activeconversation/message/input/OptionInput$Option;Z)V
    .locals 1

    .line 415
    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragment;->conversationalVM:Lcom/helpshift/conversation/viewmodel/ConversationalVM;

    invoke-virtual {v0, p1, p2, p3}, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->handleOptionSelected(Lcom/helpshift/conversation/activeconversation/message/OptionInputMessageDM;Lcom/helpshift/conversation/activeconversation/message/input/OptionInput$Option;Z)V

    return-void
.end method

.method public handleOptionSelectedForPicker(Lcom/helpshift/conversation/viewmodel/OptionUIModel;Z)V
    .locals 1

    .line 450
    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragment;->conversationalVM:Lcom/helpshift/conversation/viewmodel/ConversationalVM;

    invoke-virtual {v0, p1, p2}, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->handleOptionSelectedForPicker(Lcom/helpshift/conversation/viewmodel/OptionUIModel;Z)V

    return-void
.end method

.method public handleReplyReviewButtonClick(Lcom/helpshift/conversation/activeconversation/message/RequestAppReviewMessageDM;)V
    .locals 1

    .line 581
    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragment;->conversationalVM:Lcom/helpshift/conversation/viewmodel/ConversationalVM;

    invoke-virtual {v0, p1}, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->handleAppReviewRequestClick(Lcom/helpshift/conversation/activeconversation/message/RequestAppReviewMessageDM;)V

    return-void
.end method

.method public handleScreenshotAction(Lcom/helpshift/support/fragments/ScreenshotPreviewFragment$ScreenshotAction;Lcom/helpshift/conversation/dto/ImagePickerFile;Ljava/lang/String;)Z
    .locals 1
    .param p3    # Ljava/lang/String;
        .annotation build Landroidx/annotation/Nullable;
        .end annotation
    .end param

    .line 523
    sget-object v0, Lcom/helpshift/support/conversations/ConversationalFragment$14;->$SwitchMap$com$helpshift$support$fragments$ScreenshotPreviewFragment$ScreenshotAction:[I

    invoke-virtual {p1}, Lcom/helpshift/support/fragments/ScreenshotPreviewFragment$ScreenshotAction;->ordinal()I

    move-result p1

    aget p1, v0, p1

    const/4 v0, 0x1

    if-eq p1, v0, :cond_0

    const/4 p1, 0x0

    return p1

    .line 526
    :cond_0
    iget-boolean p1, p0, Lcom/helpshift/support/conversations/ConversationalFragment;->isConversationVMInitialized:Z

    if-eqz p1, :cond_1

    iget-object p1, p0, Lcom/helpshift/support/conversations/ConversationalFragment;->conversationalVM:Lcom/helpshift/conversation/viewmodel/ConversationalVM;

    if-eqz p1, :cond_1

    .line 527
    iget-object p1, p0, Lcom/helpshift/support/conversations/ConversationalFragment;->conversationalVM:Lcom/helpshift/conversation/viewmodel/ConversationalVM;

    invoke-virtual {p1, p2, p3}, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->sendScreenShot(Lcom/helpshift/conversation/dto/ImagePickerFile;Ljava/lang/String;)V

    goto :goto_0

    .line 531
    :cond_1
    iput-object p2, p0, Lcom/helpshift/support/conversations/ConversationalFragment;->selectedImageFile:Lcom/helpshift/conversation/dto/ImagePickerFile;

    .line 532
    iput-object p3, p0, Lcom/helpshift/support/conversations/ConversationalFragment;->selectedImageRefersId:Ljava/lang/String;

    .line 533
    iput-boolean v0, p0, Lcom/helpshift/support/conversations/ConversationalFragment;->shouldUpdateAttachment:Z

    :goto_0
    return v0
.end method

.method protected initConversationVM()V
    .locals 5

    .line 327
    invoke-static {}, Lcom/helpshift/util/HelpshiftContext;->getCoreApi()Lcom/helpshift/CoreApi;

    move-result-object v0

    iget-boolean v1, p0, Lcom/helpshift/support/conversations/ConversationalFragment;->shouldShowConversationHistory:Z

    iget-object v2, p0, Lcom/helpshift/support/conversations/ConversationalFragment;->conversationId:Ljava/lang/Long;

    iget-object v3, p0, Lcom/helpshift/support/conversations/ConversationalFragment;->renderer:Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;

    iget-boolean v4, p0, Lcom/helpshift/support/conversations/ConversationalFragment;->retainMessageBoxOnUI:Z

    invoke-interface {v0, v1, v2, v3, v4}, Lcom/helpshift/CoreApi;->getConversationalViewModel(ZLjava/lang/Long;Lcom/helpshift/conversation/activeconversation/ConversationalRenderer;Z)Lcom/helpshift/conversation/viewmodel/ConversationalVM;

    move-result-object v0

    iput-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragment;->conversationalVM:Lcom/helpshift/conversation/viewmodel/ConversationalVM;

    return-void
.end method

.method protected initRenderer(Landroidx/recyclerview/widget/RecyclerView;Landroid/view/View;Landroid/view/View;Landroid/view/View;)V
    .locals 11

    .line 336
    new-instance v10, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;

    invoke-virtual {p0}, Lcom/helpshift/support/conversations/ConversationalFragment;->getContext()Landroid/content/Context;

    move-result-object v1

    .line 337
    invoke-direct {p0}, Lcom/helpshift/support/conversations/ConversationalFragment;->getParentWindow()Landroid/view/Window;

    move-result-object v2

    .line 339
    invoke-virtual {p0}, Lcom/helpshift/support/conversations/ConversationalFragment;->getView()Landroid/view/View;

    move-result-object v4

    .line 343
    invoke-virtual {p0}, Lcom/helpshift/support/conversations/ConversationalFragment;->getSupportFragment()Lcom/helpshift/support/fragments/SupportFragment;

    move-result-object v8

    move-object v0, v10

    move-object v3, p1

    move-object v5, p2

    move-object v6, p3

    move-object v7, p4

    move-object v9, p0

    invoke-direct/range {v0 .. v9}, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;-><init>(Landroid/content/Context;Landroid/view/Window;Landroidx/recyclerview/widget/RecyclerView;Landroid/view/View;Landroid/view/View;Landroid/view/View;Landroid/view/View;Lcom/helpshift/support/fragments/IToolbarMenuItemRenderer;Lcom/helpshift/support/conversations/ConversationalFragmentRouter;)V

    iput-object v10, p0, Lcom/helpshift/support/conversations/ConversationalFragment;->renderer:Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;

    return-void
.end method

.method protected initialize(Landroid/view/View;)V
    .locals 7

    .line 162
    sget v0, Lcom/helpshift/R$id;->hs__messagesList:I

    invoke-virtual {p1, v0}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object v0

    check-cast v0, Landroidx/recyclerview/widget/RecyclerView;

    iput-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragment;->messagesRecyclerView:Landroidx/recyclerview/widget/RecyclerView;

    .line 163
    sget v0, Lcom/helpshift/R$id;->hs__confirmation:I

    invoke-virtual {p1, v0}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object v0

    .line 164
    sget v1, Lcom/helpshift/R$id;->scroll_indicator:I

    invoke-virtual {p1, v1}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object v1

    .line 165
    sget v2, Lcom/helpshift/R$id;->unread_indicator_red_dot:I

    invoke-virtual {p1, v2}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object v2

    .line 166
    sget v3, Lcom/helpshift/R$id;->unread_indicator_red_dot_image_view:I

    invoke-virtual {p1, v3}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object v3

    .line 171
    sget v4, Landroid/os/Build$VERSION;->SDK_INT:I

    const/16 v5, 0x15

    if-ge v4, v5, :cond_0

    .line 172
    invoke-virtual {p0}, Lcom/helpshift/support/conversations/ConversationalFragment;->getContext()Landroid/content/Context;

    move-result-object v4

    sget v5, Lcom/helpshift/R$drawable;->hs__ring:I

    invoke-static {v4, v5}, Landroidx/core/content/ContextCompat;->getDrawable(Landroid/content/Context;I)Landroid/graphics/drawable/Drawable;

    move-result-object v4

    .line 173
    invoke-virtual {v1, v4}, Landroid/view/View;->setBackgroundDrawable(Landroid/graphics/drawable/Drawable;)V

    .line 174
    invoke-virtual {v2, v4}, Landroid/view/View;->setBackgroundDrawable(Landroid/graphics/drawable/Drawable;)V

    .line 177
    :cond_0
    invoke-virtual {p0}, Lcom/helpshift/support/conversations/ConversationalFragment;->getContext()Landroid/content/Context;

    move-result-object v4

    sget v5, Lcom/helpshift/R$drawable;->hs__circle:I

    sget v6, Lcom/helpshift/R$attr;->colorAccent:I

    invoke-static {v4, v3, v5, v6}, Lcom/helpshift/util/Styles;->setDrawable(Landroid/content/Context;Landroid/view/View;II)V

    .line 180
    iget-object v3, p0, Lcom/helpshift/support/conversations/ConversationalFragment;->messagesRecyclerView:Landroidx/recyclerview/widget/RecyclerView;

    invoke-virtual {p0, v3, v0, v1, v2}, Lcom/helpshift/support/conversations/ConversationalFragment;->initRenderer(Landroidx/recyclerview/widget/RecyclerView;Landroid/view/View;Landroid/view/View;Landroid/view/View;)V

    .line 181
    invoke-virtual {p0}, Lcom/helpshift/support/conversations/ConversationalFragment;->initConversationVM()V

    .line 182
    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragment;->renderer:Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;

    invoke-virtual {v0}, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->setReplyboxListeners()V

    const/4 v0, 0x0

    .line 188
    iput-boolean v0, p0, Lcom/helpshift/support/conversations/ConversationalFragment;->retainMessageBoxOnUI:Z

    .line 190
    iget-object v1, p0, Lcom/helpshift/support/conversations/ConversationalFragment;->conversationalVM:Lcom/helpshift/conversation/viewmodel/ConversationalVM;

    invoke-virtual {v1}, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->startLiveUpdates()V

    const/4 v1, 0x1

    .line 193
    iput-boolean v1, p0, Lcom/helpshift/support/conversations/ConversationalFragment;->isConversationVMInitialized:Z

    .line 196
    iget-boolean v1, p0, Lcom/helpshift/support/conversations/ConversationalFragment;->shouldUpdateAttachment:Z

    if-eqz v1, :cond_1

    .line 197
    iget-object v1, p0, Lcom/helpshift/support/conversations/ConversationalFragment;->conversationalVM:Lcom/helpshift/conversation/viewmodel/ConversationalVM;

    iget-object v2, p0, Lcom/helpshift/support/conversations/ConversationalFragment;->selectedImageFile:Lcom/helpshift/conversation/dto/ImagePickerFile;

    iget-object v3, p0, Lcom/helpshift/support/conversations/ConversationalFragment;->selectedImageRefersId:Ljava/lang/String;

    invoke-virtual {v1, v2, v3}, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->sendScreenShot(Lcom/helpshift/conversation/dto/ImagePickerFile;Ljava/lang/String;)V

    .line 198
    iput-boolean v0, p0, Lcom/helpshift/support/conversations/ConversationalFragment;->shouldUpdateAttachment:Z

    .line 201
    :cond_1
    sget v0, Lcom/helpshift/R$id;->resolution_accepted_button:I

    invoke-virtual {p1, v0}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object v0

    new-instance v1, Lcom/helpshift/support/conversations/ConversationalFragment$1;

    invoke-direct {v1, p0}, Lcom/helpshift/support/conversations/ConversationalFragment$1;-><init>(Lcom/helpshift/support/conversations/ConversationalFragment;)V

    .line 202
    invoke-virtual {v0, v1}, Landroid/view/View;->setOnClickListener(Landroid/view/View$OnClickListener;)V

    .line 209
    sget v0, Lcom/helpshift/R$id;->resolution_rejected_button:I

    invoke-virtual {p1, v0}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object v0

    new-instance v1, Lcom/helpshift/support/conversations/ConversationalFragment$2;

    invoke-direct {v1, p0}, Lcom/helpshift/support/conversations/ConversationalFragment$2;-><init>(Lcom/helpshift/support/conversations/ConversationalFragment;)V

    .line 210
    invoke-virtual {v0, v1}, Landroid/view/View;->setOnClickListener(Landroid/view/View$OnClickListener;)V

    .line 219
    sget v0, Lcom/helpshift/R$id;->scroll_jump_button:I

    invoke-virtual {p1, v0}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object p1

    check-cast p1, Landroid/widget/ImageButton;

    .line 220
    invoke-virtual {p0}, Lcom/helpshift/support/conversations/ConversationalFragment;->getContext()Landroid/content/Context;

    move-result-object v0

    sget v1, Lcom/helpshift/R$drawable;->hs__circle_shape_scroll_jump:I

    sget v2, Lcom/helpshift/R$attr;->hs__composeBackgroundColor:I

    invoke-static {v0, p1, v1, v2}, Lcom/helpshift/util/Styles;->setDrawable(Landroid/content/Context;Landroid/view/View;II)V

    .line 222
    invoke-virtual {p0}, Lcom/helpshift/support/conversations/ConversationalFragment;->getContext()Landroid/content/Context;

    move-result-object v0

    invoke-virtual {p1}, Landroid/widget/ImageButton;->getDrawable()Landroid/graphics/drawable/Drawable;

    move-result-object v1

    sget v2, Lcom/helpshift/R$attr;->hs__selectableOptionColor:I

    invoke-static {v0, v1, v2}, Lcom/helpshift/util/Styles;->setColorFilter(Landroid/content/Context;Landroid/graphics/drawable/Drawable;I)V

    .line 224
    new-instance v0, Lcom/helpshift/support/conversations/ConversationalFragment$3;

    invoke-direct {v0, p0}, Lcom/helpshift/support/conversations/ConversationalFragment$3;-><init>(Lcom/helpshift/support/conversations/ConversationalFragment;)V

    invoke-virtual {p1, v0}, Landroid/widget/ImageButton;->setOnClickListener(Landroid/view/View$OnClickListener;)V

    .line 231
    new-instance p1, Lcom/helpshift/support/conversations/HSRecyclerViewScrollListener;

    new-instance v0, Landroid/os/Handler;

    invoke-direct {v0}, Landroid/os/Handler;-><init>()V

    invoke-direct {p1, v0, p0}, Lcom/helpshift/support/conversations/HSRecyclerViewScrollListener;-><init>(Landroid/os/Handler;Lcom/helpshift/support/conversations/HSRecyclerViewScrollListener$RecyclerViewScrollCallback;)V

    iput-object p1, p0, Lcom/helpshift/support/conversations/ConversationalFragment;->hsRecyclerViewScrollListener:Lcom/helpshift/support/conversations/HSRecyclerViewScrollListener;

    .line 232
    iget-object p1, p0, Lcom/helpshift/support/conversations/ConversationalFragment;->messagesRecyclerView:Landroidx/recyclerview/widget/RecyclerView;

    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragment;->hsRecyclerViewScrollListener:Lcom/helpshift/support/conversations/HSRecyclerViewScrollListener;

    invoke-virtual {p1, v0}, Landroidx/recyclerview/widget/RecyclerView;->addOnScrollListener(Landroidx/recyclerview/widget/RecyclerView$OnScrollListener;)V

    return-void
.end method

.method public launchImagePicker(Lcom/helpshift/conversation/activeconversation/message/RequestScreenshotMessageDM;)V
    .locals 2

    .line 571
    iget-object p1, p1, Lcom/helpshift/conversation/activeconversation/message/RequestScreenshotMessageDM;->serverId:Ljava/lang/String;

    iput-object p1, p0, Lcom/helpshift/support/conversations/ConversationalFragment;->imageRefersId:Ljava/lang/String;

    .line 572
    iget-object p1, p0, Lcom/helpshift/support/conversations/ConversationalFragment;->conversationalVM:Lcom/helpshift/conversation/viewmodel/ConversationalVM;

    invoke-virtual {p1}, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->onImageAttachmentButtonClick()V

    .line 573
    new-instance p1, Landroid/os/Bundle;

    invoke-direct {p1}, Landroid/os/Bundle;-><init>()V

    const-string v0, "key_screenshot_mode"

    .line 574
    invoke-virtual {p0}, Lcom/helpshift/support/conversations/ConversationalFragment;->getScreenshotMode()I

    move-result v1

    invoke-virtual {p1, v0, v1}, Landroid/os/Bundle;->putInt(Ljava/lang/String;I)V

    const-string v0, "key_refers_id"

    .line 575
    iget-object v1, p0, Lcom/helpshift/support/conversations/ConversationalFragment;->imageRefersId:Ljava/lang/String;

    invoke-virtual {p1, v0, v1}, Landroid/os/Bundle;->putString(Ljava/lang/String;Ljava/lang/String;)V

    .line 576
    invoke-virtual {p0}, Lcom/helpshift/support/conversations/ConversationalFragment;->getSupportFragment()Lcom/helpshift/support/fragments/SupportFragment;

    move-result-object v0

    const/4 v1, 0x1

    invoke-virtual {v0, v1, p1}, Lcom/helpshift/support/fragments/SupportFragment;->launchImagePicker(ZLandroid/os/Bundle;)V

    return-void
.end method

.method public onAdminMessageLinkClicked(Ljava/lang/String;Lcom/helpshift/conversation/activeconversation/message/MessageDM;)V
    .locals 1

    .line 547
    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragment;->conversationalVM:Lcom/helpshift/conversation/viewmodel/ConversationalVM;

    invoke-virtual {v0, p1, p2}, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->onAdminMessageLinkClicked(Ljava/lang/String;Lcom/helpshift/conversation/activeconversation/message/MessageDM;)V

    return-void
.end method

.method public onAdminSuggestedQuestionSelected(Lcom/helpshift/conversation/activeconversation/message/MessageDM;Ljava/lang/String;Ljava/lang/String;)V
    .locals 2

    .line 421
    invoke-virtual {p0}, Lcom/helpshift/support/conversations/ConversationalFragment;->getSupportController()Lcom/helpshift/support/controllers/SupportController;

    move-result-object v0

    new-instance v1, Lcom/helpshift/support/conversations/ConversationalFragment$12;

    invoke-direct {v1, p0, p1, p2}, Lcom/helpshift/support/conversations/ConversationalFragment$12;-><init>(Lcom/helpshift/support/conversations/ConversationalFragment;Lcom/helpshift/conversation/activeconversation/message/MessageDM;Ljava/lang/String;)V

    invoke-virtual {v0, p2, p3, v1}, Lcom/helpshift/support/controllers/SupportController;->onAdminSuggestedQuestionSelected(Ljava/lang/String;Ljava/lang/String;Lcom/helpshift/support/fragments/SingleQuestionFragment$QuestionReadListener;)V

    return-void
.end method

.method public onAttach(Landroid/content/Context;)V
    .locals 0

    .line 119
    invoke-super {p0, p1}, Lcom/helpshift/support/conversations/BaseConversationFragment;->onAttach(Landroid/content/Context;)V

    .line 120
    invoke-virtual {p0}, Lcom/helpshift/support/conversations/ConversationalFragment;->isChangingConfigurations()Z

    move-result p1

    if-eqz p1, :cond_0

    iget-object p1, p0, Lcom/helpshift/support/conversations/ConversationalFragment;->renderer:Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;

    if-eqz p1, :cond_0

    .line 121
    iget-object p1, p0, Lcom/helpshift/support/conversations/ConversationalFragment;->renderer:Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;

    invoke-virtual {p1}, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->isReplyBoxVisible()Z

    move-result p1

    iput-boolean p1, p0, Lcom/helpshift/support/conversations/ConversationalFragment;->retainMessageBoxOnUI:Z

    :cond_0
    return-void
.end method

.method public onAuthenticationFailure()V
    .locals 1

    .line 755
    invoke-virtual {p0}, Lcom/helpshift/support/conversations/ConversationalFragment;->getSupportController()Lcom/helpshift/support/controllers/SupportController;

    move-result-object v0

    invoke-virtual {v0}, Lcom/helpshift/support/controllers/SupportController;->onAuthenticationFailure()V

    return-void
.end method

.method public onBackPressed()Z
    .locals 1

    .line 476
    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragment;->renderer:Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;

    invoke-virtual {v0}, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->onBackPressed()Z

    move-result v0

    return v0
.end method

.method public onCSATSurveySubmitted(ILjava/lang/String;)V
    .locals 1

    .line 603
    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragment;->conversationalVM:Lcom/helpshift/conversation/viewmodel/ConversationalVM;

    invoke-virtual {v0, p1, p2}, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->onCSATSurveySubmitted(ILjava/lang/String;)V

    return-void
.end method

.method public onCreateContextMenu(Landroid/view/ContextMenu;Ljava/lang/String;)V
    .locals 2

    .line 552
    invoke-static {p2}, Lcom/helpshift/common/StringUtils;->isEmpty(Ljava/lang/String;)Z

    move-result v0

    if-nez v0, :cond_0

    .line 553
    sget v0, Lcom/helpshift/R$string;->hs__copy:I

    const/4 v1, 0x0

    invoke-interface {p1, v1, v1, v1, v0}, Landroid/view/ContextMenu;->add(IIII)Landroid/view/MenuItem;

    move-result-object p1

    .line 554
    new-instance v0, Lcom/helpshift/support/conversations/ConversationalFragment$13;

    invoke-direct {v0, p0, p2}, Lcom/helpshift/support/conversations/ConversationalFragment$13;-><init>(Lcom/helpshift/support/conversations/ConversationalFragment;Ljava/lang/String;)V

    invoke-interface {p1, v0}, Landroid/view/MenuItem;->setOnMenuItemClickListener(Landroid/view/MenuItem$OnMenuItemClickListener;)Landroid/view/MenuItem;

    :cond_0
    return-void
.end method

.method public onCreateOptionMenuCalled()V
    .locals 2

    .line 721
    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragment;->conversationalVM:Lcom/helpshift/conversation/viewmodel/ConversationalVM;

    invoke-virtual {v0}, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->renderMenuItems()V

    .line 726
    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragment;->renderer:Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;

    iget-object v1, p0, Lcom/helpshift/support/conversations/ConversationalFragment;->conversationalVM:Lcom/helpshift/conversation/viewmodel/ConversationalVM;

    invoke-virtual {v1}, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->getAttachImageButtonViewState()Lcom/helpshift/widget/BaseViewState;

    move-result-object v1

    invoke-virtual {v1}, Lcom/helpshift/widget/BaseViewState;->isVisible()Z

    move-result v1

    invoke-virtual {v0, v1}, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->updateImageAttachmentButtonView(Z)V

    return-void
.end method

.method public onCreateView(Landroid/view/LayoutInflater;Landroid/view/ViewGroup;Landroid/os/Bundle;)Landroid/view/View;
    .locals 1

    .line 128
    invoke-virtual {p0}, Lcom/helpshift/support/conversations/ConversationalFragment;->getActivity()Landroidx/fragment/app/FragmentActivity;

    move-result-object p3

    invoke-virtual {p3}, Landroidx/fragment/app/FragmentActivity;->getWindow()Landroid/view/Window;

    move-result-object p3

    invoke-virtual {p3}, Landroid/view/Window;->getAttributes()Landroid/view/WindowManager$LayoutParams;

    move-result-object p3

    iget p3, p3, Landroid/view/WindowManager$LayoutParams;->flags:I

    iput p3, p0, Lcom/helpshift/support/conversations/ConversationalFragment;->lastWindowFlags:I

    .line 129
    invoke-virtual {p0}, Lcom/helpshift/support/conversations/ConversationalFragment;->getActivity()Landroidx/fragment/app/FragmentActivity;

    move-result-object p3

    invoke-virtual {p3}, Landroidx/fragment/app/FragmentActivity;->getWindow()Landroid/view/Window;

    move-result-object p3

    const/16 v0, 0x800

    invoke-virtual {p3, v0}, Landroid/view/Window;->addFlags(I)V

    .line 130
    invoke-virtual {p0}, Lcom/helpshift/support/conversations/ConversationalFragment;->getActivity()Landroidx/fragment/app/FragmentActivity;

    move-result-object p3

    invoke-virtual {p3}, Landroidx/fragment/app/FragmentActivity;->getWindow()Landroid/view/Window;

    move-result-object p3

    const/16 v0, 0x400

    invoke-virtual {p3, v0}, Landroid/view/Window;->clearFlags(I)V

    .line 131
    sget p3, Lcom/helpshift/R$layout;->hs__conversation_fragment:I

    const/4 v0, 0x0

    invoke-virtual {p1, p3, p2, v0}, Landroid/view/LayoutInflater;->inflate(ILandroid/view/ViewGroup;Z)Landroid/view/View;

    move-result-object p1

    return-object p1
.end method

.method public onDestroy()V
    .locals 1

    .line 775
    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragment;->conversationalVM:Lcom/helpshift/conversation/viewmodel/ConversationalVM;

    invoke-virtual {v0}, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->onDestroy()V

    .line 776
    invoke-super {p0}, Lcom/helpshift/support/conversations/BaseConversationFragment;->onDestroy()V

    return-void
.end method

.method public onDestroyView()V
    .locals 3

    .line 685
    invoke-virtual {p0}, Lcom/helpshift/support/conversations/ConversationalFragment;->getActivity()Landroidx/fragment/app/FragmentActivity;

    move-result-object v0

    if-eqz v0, :cond_0

    .line 686
    invoke-virtual {p0}, Lcom/helpshift/support/conversations/ConversationalFragment;->getActivity()Landroidx/fragment/app/FragmentActivity;

    move-result-object v0

    invoke-virtual {v0}, Landroidx/fragment/app/FragmentActivity;->getWindow()Landroid/view/Window;

    move-result-object v0

    const/16 v1, 0x800

    invoke-virtual {v0, v1}, Landroid/view/Window;->clearFlags(I)V

    .line 687
    invoke-virtual {p0}, Lcom/helpshift/support/conversations/ConversationalFragment;->getActivity()Landroidx/fragment/app/FragmentActivity;

    move-result-object v0

    invoke-virtual {v0}, Landroidx/fragment/app/FragmentActivity;->getWindow()Landroid/view/Window;

    move-result-object v0

    iget v1, p0, Lcom/helpshift/support/conversations/ConversationalFragment;->lastWindowFlags:I

    iget v2, p0, Lcom/helpshift/support/conversations/ConversationalFragment;->lastWindowFlags:I

    invoke-virtual {v0, v1, v2}, Landroid/view/Window;->setFlags(II)V

    :cond_0
    const/4 v0, 0x0

    .line 689
    iput-boolean v0, p0, Lcom/helpshift/support/conversations/ConversationalFragment;->isConversationVMInitialized:Z

    .line 690
    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragment;->conversationalVM:Lcom/helpshift/conversation/viewmodel/ConversationalVM;

    const/4 v1, -0x1

    invoke-virtual {v0, v1}, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->setConversationViewState(I)V

    .line 691
    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragment;->renderer:Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;

    invoke-virtual {v0}, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->unregisterFragmentRenderer()V

    .line 692
    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragment;->conversationalVM:Lcom/helpshift/conversation/viewmodel/ConversationalVM;

    invoke-virtual {v0}, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->unregisterRenderer()V

    .line 693
    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragment;->renderer:Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;

    invoke-virtual {v0}, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->destroy()V

    .line 696
    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragment;->messagesRecyclerView:Landroidx/recyclerview/widget/RecyclerView;

    iget-object v1, p0, Lcom/helpshift/support/conversations/ConversationalFragment;->hsRecyclerViewScrollListener:Lcom/helpshift/support/conversations/HSRecyclerViewScrollListener;

    invoke-virtual {v0, v1}, Landroidx/recyclerview/widget/RecyclerView;->removeOnScrollListener(Landroidx/recyclerview/widget/RecyclerView$OnScrollListener;)V

    const/4 v0, 0x0

    .line 697
    iput-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragment;->messagesRecyclerView:Landroidx/recyclerview/widget/RecyclerView;

    .line 698
    invoke-super {p0}, Lcom/helpshift/support/conversations/BaseConversationFragment;->onDestroyView()V

    return-void
.end method

.method public onDetach()V
    .locals 2

    .line 677
    invoke-virtual {p0}, Lcom/helpshift/support/conversations/ConversationalFragment;->isChangingConfigurations()Z

    move-result v0

    if-nez v0, :cond_0

    .line 678
    invoke-static {}, Lcom/helpshift/util/HelpshiftContext;->getCoreApi()Lcom/helpshift/CoreApi;

    move-result-object v0

    invoke-interface {v0}, Lcom/helpshift/CoreApi;->getConversationInboxPoller()Lcom/helpshift/conversation/ConversationInboxPoller;

    move-result-object v0

    const/4 v1, 0x1

    invoke-virtual {v0, v1}, Lcom/helpshift/conversation/ConversationInboxPoller;->startAppPoller(Z)V

    .line 680
    :cond_0
    invoke-super {p0}, Lcom/helpshift/support/conversations/BaseConversationFragment;->onDetach()V

    return-void
.end method

.method public onFocusChanged(Z)V
    .locals 1

    .line 485
    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragment;->renderer:Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;

    if-eqz v0, :cond_0

    .line 486
    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragment;->renderer:Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;

    invoke-virtual {v0, p1}, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->onFocusChanged(Z)V

    :cond_0
    return-void
.end method

.method public onHistoryLoadingRetryClicked()V
    .locals 1

    .line 608
    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragment;->conversationalVM:Lcom/helpshift/conversation/viewmodel/ConversationalVM;

    invoke-virtual {v0}, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->retryHistoryLoadingMessages()V

    return-void
.end method

.method public onListPickerSearchQueryChange(Ljava/lang/String;)V
    .locals 1

    .line 445
    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragment;->conversationalVM:Lcom/helpshift/conversation/viewmodel/ConversationalVM;

    invoke-virtual {v0, p1}, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->onListPickerSearchQueryChange(Ljava/lang/String;)V

    return-void
.end method

.method public onMenuItemClicked(Lcom/helpshift/support/fragments/HSMenuItemType;)V
    .locals 4

    .line 731
    sget-object v0, Lcom/helpshift/support/conversations/ConversationalFragment$14;->$SwitchMap$com$helpshift$support$fragments$HSMenuItemType:[I

    invoke-virtual {p1}, Lcom/helpshift/support/fragments/HSMenuItemType;->ordinal()I

    move-result p1

    aget p1, v0, p1

    const/4 v0, 0x1

    if-eq p1, v0, :cond_0

    goto :goto_0

    :cond_0
    const/4 p1, 0x0

    .line 733
    iput-object p1, p0, Lcom/helpshift/support/conversations/ConversationalFragment;->imageRefersId:Ljava/lang/String;

    .line 734
    iget-object v1, p0, Lcom/helpshift/support/conversations/ConversationalFragment;->conversationalVM:Lcom/helpshift/conversation/viewmodel/ConversationalVM;

    invoke-virtual {v1}, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->onImageAttachmentButtonClick()V

    .line 735
    new-instance v1, Landroid/os/Bundle;

    invoke-direct {v1}, Landroid/os/Bundle;-><init>()V

    const-string v2, "key_screenshot_mode"

    .line 736
    invoke-virtual {p0}, Lcom/helpshift/support/conversations/ConversationalFragment;->getScreenshotMode()I

    move-result v3

    invoke-virtual {v1, v2, v3}, Landroid/os/Bundle;->putInt(Ljava/lang/String;I)V

    const-string v2, "key_refers_id"

    .line 737
    invoke-virtual {v1, v2, p1}, Landroid/os/Bundle;->putString(Ljava/lang/String;Ljava/lang/String;)V

    .line 738
    invoke-virtual {p0}, Lcom/helpshift/support/conversations/ConversationalFragment;->getSupportFragment()Lcom/helpshift/support/fragments/SupportFragment;

    move-result-object p1

    invoke-virtual {p1, v0, v1}, Lcom/helpshift/support/fragments/SupportFragment;->launchImagePicker(ZLandroid/os/Bundle;)V

    :goto_0
    return-void
.end method

.method public onNetworkAvailable()V
    .locals 1

    .line 492
    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragment;->conversationalVM:Lcom/helpshift/conversation/viewmodel/ConversationalVM;

    invoke-virtual {v0}, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->onNetworkAvailable()V

    return-void
.end method

.method public onNetworkUnavailable()V
    .locals 1

    .line 497
    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragment;->conversationalVM:Lcom/helpshift/conversation/viewmodel/ConversationalVM;

    invoke-virtual {v0}, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->onNetworkUnAvailable()V

    return-void
.end method

.method public onPause()V
    .locals 2

    .line 405
    invoke-static {}, Lcom/helpshift/network/connectivity/HSConnectivityManager;->getInstance()Lcom/helpshift/network/connectivity/HSConnectivityManager;

    move-result-object v0

    invoke-virtual {v0, p0}, Lcom/helpshift/network/connectivity/HSConnectivityManager;->unregisterNetworkConnectivityListener(Lcom/helpshift/network/connectivity/HSNetworkConnectivityCallback;)V

    .line 406
    invoke-virtual {p0}, Lcom/helpshift/support/conversations/ConversationalFragment;->getActivity()Landroidx/fragment/app/FragmentActivity;

    move-result-object v0

    invoke-virtual {v0}, Landroidx/fragment/app/FragmentActivity;->getWindow()Landroid/view/Window;

    move-result-object v0

    iget v1, p0, Lcom/helpshift/support/conversations/ConversationalFragment;->lastSoftInputMode:I

    invoke-virtual {v0, v1}, Landroid/view/Window;->setSoftInputMode(I)V

    .line 407
    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragment;->renderer:Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;

    invoke-virtual {v0}, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->hideKeyboard()V

    .line 408
    invoke-direct {p0}, Lcom/helpshift/support/conversations/ConversationalFragment;->removeViewStateObservers()V

    .line 409
    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragment;->conversationalVM:Lcom/helpshift/conversation/viewmodel/ConversationalVM;

    invoke-virtual {v0}, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->onPause()V

    .line 410
    invoke-super {p0}, Lcom/helpshift/support/conversations/BaseConversationFragment;->onPause()V

    return-void
.end method

.method protected onPermissionGranted(I)V
    .locals 2

    packed-switch p1, :pswitch_data_0

    goto :goto_0

    .line 645
    :pswitch_0
    iget-object p1, p0, Lcom/helpshift/support/conversations/ConversationalFragment;->readableAttachmentMessage:Lcom/helpshift/conversation/activeconversation/message/AttachmentMessageDM;

    if-eqz p1, :cond_0

    .line 646
    iget-object p1, p0, Lcom/helpshift/support/conversations/ConversationalFragment;->conversationalVM:Lcom/helpshift/conversation/viewmodel/ConversationalVM;

    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragment;->readableAttachmentMessage:Lcom/helpshift/conversation/activeconversation/message/AttachmentMessageDM;

    invoke-virtual {p1, v0}, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->handleAdminAttachmentMessageClick(Lcom/helpshift/conversation/activeconversation/message/AttachmentMessageDM;)V

    const/4 p1, 0x0

    .line 647
    iput-object p1, p0, Lcom/helpshift/support/conversations/ConversationalFragment;->readableAttachmentMessage:Lcom/helpshift/conversation/activeconversation/message/AttachmentMessageDM;

    goto :goto_0

    .line 639
    :pswitch_1
    new-instance p1, Landroid/os/Bundle;

    invoke-direct {p1}, Landroid/os/Bundle;-><init>()V

    const-string v0, "key_screenshot_mode"

    .line 640
    invoke-virtual {p0}, Lcom/helpshift/support/conversations/ConversationalFragment;->getScreenshotMode()I

    move-result v1

    invoke-virtual {p1, v0, v1}, Landroid/os/Bundle;->putInt(Ljava/lang/String;I)V

    const-string v0, "key_refers_id"

    .line 641
    iget-object v1, p0, Lcom/helpshift/support/conversations/ConversationalFragment;->imageRefersId:Ljava/lang/String;

    invoke-virtual {p1, v0, v1}, Landroid/os/Bundle;->putString(Ljava/lang/String;Ljava/lang/String;)V

    .line 642
    invoke-virtual {p0}, Lcom/helpshift/support/conversations/ConversationalFragment;->getSupportFragment()Lcom/helpshift/support/fragments/SupportFragment;

    move-result-object v0

    const/4 v1, 0x0

    invoke-virtual {v0, v1, p1}, Lcom/helpshift/support/fragments/SupportFragment;->launchImagePicker(ZLandroid/os/Bundle;)V

    :cond_0
    :goto_0
    return-void

    nop

    :pswitch_data_0
    .packed-switch 0x2
        :pswitch_1
        :pswitch_0
    .end packed-switch
.end method

.method public onResume()V
    .locals 4

    .line 378
    invoke-super {p0}, Lcom/helpshift/support/conversations/BaseConversationFragment;->onResume()V

    .line 379
    invoke-direct {p0}, Lcom/helpshift/support/conversations/ConversationalFragment;->addViewStateObservers()V

    .line 380
    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragment;->conversationalVM:Lcom/helpshift/conversation/viewmodel/ConversationalVM;

    invoke-virtual {v0}, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->onResume()V

    .line 381
    invoke-virtual {p0}, Lcom/helpshift/support/conversations/ConversationalFragment;->getActivity()Landroidx/fragment/app/FragmentActivity;

    move-result-object v0

    invoke-virtual {v0}, Landroidx/fragment/app/FragmentActivity;->getWindow()Landroid/view/Window;

    move-result-object v0

    invoke-virtual {v0}, Landroid/view/Window;->getAttributes()Landroid/view/WindowManager$LayoutParams;

    move-result-object v0

    iget v0, v0, Landroid/view/WindowManager$LayoutParams;->softInputMode:I

    iput v0, p0, Lcom/helpshift/support/conversations/ConversationalFragment;->lastSoftInputMode:I

    .line 382
    invoke-virtual {p0}, Lcom/helpshift/support/conversations/ConversationalFragment;->getActivity()Landroidx/fragment/app/FragmentActivity;

    move-result-object v0

    invoke-virtual {v0}, Landroidx/fragment/app/FragmentActivity;->getWindow()Landroid/view/Window;

    move-result-object v0

    const/16 v1, 0x10

    invoke-virtual {v0, v1}, Landroid/view/Window;->setSoftInputMode(I)V

    .line 383
    invoke-virtual {p0}, Lcom/helpshift/support/conversations/ConversationalFragment;->isChangingConfigurations()Z

    move-result v0

    if-nez v0, :cond_1

    .line 384
    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragment;->conversationalVM:Lcom/helpshift/conversation/viewmodel/ConversationalVM;

    iget-object v0, v0, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->viewableConversation:Lcom/helpshift/conversation/activeconversation/ViewableConversation;

    invoke-virtual {v0}, Lcom/helpshift/conversation/activeconversation/ViewableConversation;->getActiveConversation()Lcom/helpshift/conversation/activeconversation/model/Conversation;

    move-result-object v0

    .line 385
    iget-object v1, v0, Lcom/helpshift/conversation/activeconversation/model/Conversation;->serverId:Ljava/lang/String;

    .line 386
    iget-object v0, v0, Lcom/helpshift/conversation/activeconversation/model/Conversation;->preConversationServerId:Ljava/lang/String;

    .line 387
    new-instance v2, Ljava/util/HashMap;

    invoke-direct {v2}, Ljava/util/HashMap;-><init>()V

    .line 388
    invoke-static {v1}, Lcom/helpshift/common/StringUtils;->isEmpty(Ljava/lang/String;)Z

    move-result v3

    if-nez v3, :cond_0

    const-string v0, "id"

    .line 389
    invoke-virtual {v2, v0, v1}, Ljava/util/HashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 390
    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragment;->conversationalVM:Lcom/helpshift/conversation/viewmodel/ConversationalVM;

    sget-object v1, Lcom/helpshift/analytics/AnalyticsEventType;->OPEN_ISSUE:Lcom/helpshift/analytics/AnalyticsEventType;

    invoke-virtual {v0, v1, v2}, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->pushAnalyticsEvent(Lcom/helpshift/analytics/AnalyticsEventType;Ljava/util/Map;)V

    goto :goto_0

    .line 391
    :cond_0
    invoke-static {v0}, Lcom/helpshift/common/StringUtils;->isEmpty(Ljava/lang/String;)Z

    move-result v1

    if-nez v1, :cond_1

    const-string v1, "preissue_id"

    .line 392
    invoke-virtual {v2, v1, v0}, Ljava/util/HashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 393
    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragment;->conversationalVM:Lcom/helpshift/conversation/viewmodel/ConversationalVM;

    sget-object v1, Lcom/helpshift/analytics/AnalyticsEventType;->REPORTED_ISSUE:Lcom/helpshift/analytics/AnalyticsEventType;

    invoke-virtual {v0, v1, v2}, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->pushAnalyticsEvent(Lcom/helpshift/analytics/AnalyticsEventType;Ljava/util/Map;)V

    .line 397
    :cond_1
    :goto_0
    invoke-static {}, Lcom/helpshift/network/connectivity/HSConnectivityManager;->getInstance()Lcom/helpshift/network/connectivity/HSConnectivityManager;

    move-result-object v0

    invoke-virtual {v0, p0}, Lcom/helpshift/network/connectivity/HSConnectivityManager;->registerNetworkConnectivityListener(Lcom/helpshift/network/connectivity/HSNetworkConnectivityCallback;)V

    .line 399
    invoke-static {}, Lcom/helpshift/util/HelpshiftContext;->getCoreApi()Lcom/helpshift/CoreApi;

    move-result-object v0

    invoke-interface {v0}, Lcom/helpshift/CoreApi;->getAutoRetryFailedEventDM()Lcom/helpshift/common/AutoRetryFailedEventDM;

    move-result-object v0

    invoke-virtual {v0}, Lcom/helpshift/common/AutoRetryFailedEventDM;->resetBackoff()V

    .line 400
    invoke-static {}, Lcom/helpshift/util/HelpshiftContext;->getCoreApi()Lcom/helpshift/CoreApi;

    move-result-object v0

    invoke-interface {v0}, Lcom/helpshift/CoreApi;->getAutoRetryFailedEventDM()Lcom/helpshift/common/AutoRetryFailedEventDM;

    move-result-object v0

    sget-object v1, Lcom/helpshift/common/AutoRetryFailedEventDM$EventType;->CONVERSATION:Lcom/helpshift/common/AutoRetryFailedEventDM$EventType;

    invoke-virtual {v0, v1}, Lcom/helpshift/common/AutoRetryFailedEventDM;->sendEventForcefully(Lcom/helpshift/common/AutoRetryFailedEventDM$EventType;)V

    return-void
.end method

.method public onSaveInstanceState(Landroid/os/Bundle;)V
    .locals 2

    const-string v0, "should_show_unread_message_indicator"

    .line 703
    iget-object v1, p0, Lcom/helpshift/support/conversations/ConversationalFragment;->conversationalVM:Lcom/helpshift/conversation/viewmodel/ConversationalVM;

    invoke-virtual {v1}, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->shouldShowUnreadMessagesIndicator()Z

    move-result v1

    invoke-virtual {p1, v0, v1}, Landroid/os/Bundle;->putBoolean(Ljava/lang/String;Z)V

    .line 704
    invoke-super {p0, p1}, Lcom/helpshift/support/conversations/BaseConversationFragment;->onSaveInstanceState(Landroid/os/Bundle;)V

    return-void
.end method

.method public onScreenshotMessageClicked(Lcom/helpshift/conversation/activeconversation/message/ScreenshotMessageDM;)V
    .locals 1

    .line 586
    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragment;->conversationalVM:Lcom/helpshift/conversation/viewmodel/ConversationalVM;

    invoke-virtual {v0, p1}, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->handleScreenshotMessageClick(Lcom/helpshift/conversation/activeconversation/message/ScreenshotMessageDM;)V

    return-void
.end method

.method public onScrolledToBottom()V
    .locals 1

    .line 765
    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragment;->conversationalVM:Lcom/helpshift/conversation/viewmodel/ConversationalVM;

    invoke-virtual {v0}, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->onScrolledToBottom()V

    return-void
.end method

.method public onScrolledToTop()V
    .locals 1

    .line 760
    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragment;->conversationalVM:Lcom/helpshift/conversation/viewmodel/ConversationalVM;

    invoke-virtual {v0}, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->onScrolledToTop()V

    return-void
.end method

.method public onScrolling()V
    .locals 1

    .line 770
    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragment;->conversationalVM:Lcom/helpshift/conversation/viewmodel/ConversationalVM;

    invoke-virtual {v0}, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->onScrolling()V

    return-void
.end method

.method public onSendButtonClick()V
    .locals 1

    .line 745
    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragment;->conversationalVM:Lcom/helpshift/conversation/viewmodel/ConversationalVM;

    invoke-virtual {v0}, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->sendTextMessage()V

    return-void
.end method

.method public onSkipClick()V
    .locals 1

    .line 750
    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragment;->conversationalVM:Lcom/helpshift/conversation/viewmodel/ConversationalVM;

    invoke-virtual {v0}, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->onSkipClick()V

    return-void
.end method

.method public onStartNewConversationButtonClick()V
    .locals 1

    .line 440
    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragment;->conversationalVM:Lcom/helpshift/conversation/viewmodel/ConversationalVM;

    invoke-virtual {v0}, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->onNewConversationButtonClicked()V

    return-void
.end method

.method public onTextChanged(Ljava/lang/CharSequence;III)V
    .locals 0

    .line 433
    iget-object p2, p0, Lcom/helpshift/support/conversations/ConversationalFragment;->renderer:Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;

    invoke-virtual {p2}, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->hideReplyValidationFailedError()V

    if-eqz p1, :cond_0

    .line 434
    invoke-interface {p1}, Ljava/lang/CharSequence;->toString()Ljava/lang/String;

    move-result-object p1

    invoke-static {p1}, Lcom/helpshift/common/StringUtils;->isEmpty(Ljava/lang/String;)Z

    move-result p1

    if-nez p1, :cond_0

    const/4 p1, 0x1

    goto :goto_0

    :cond_0
    const/4 p1, 0x0

    .line 435
    :goto_0
    iget-object p2, p0, Lcom/helpshift/support/conversations/ConversationalFragment;->conversationalVM:Lcom/helpshift/conversation/viewmodel/ConversationalVM;

    invoke-virtual {p2, p1}, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->toggleReplySendButton(Z)V

    return-void
.end method

.method public onViewCreated(Landroid/view/View;Landroid/os/Bundle;)V
    .locals 3

    .line 138
    invoke-virtual {p0}, Lcom/helpshift/support/conversations/ConversationalFragment;->getArguments()Landroid/os/Bundle;

    move-result-object v0

    if-eqz v0, :cond_0

    const-string v1, "issueId"

    .line 140
    invoke-virtual {v0, v1}, Landroid/os/Bundle;->getLong(Ljava/lang/String;)J

    move-result-wide v1

    invoke-static {v1, v2}, Ljava/lang/Long;->valueOf(J)Ljava/lang/Long;

    move-result-object v1

    iput-object v1, p0, Lcom/helpshift/support/conversations/ConversationalFragment;->conversationId:Ljava/lang/Long;

    const-string v1, "show_conv_history"

    .line 142
    invoke-virtual {v0, v1}, Landroid/os/Bundle;->getBoolean(Ljava/lang/String;)Z

    move-result v1

    iput-boolean v1, p0, Lcom/helpshift/support/conversations/ConversationalFragment;->shouldShowConversationHistory:Z

    const-string v1, "create_new_pre_issue"

    .line 143
    invoke-virtual {v0, v1}, Landroid/os/Bundle;->getBoolean(Ljava/lang/String;)Z

    move-result v0

    goto :goto_0

    :cond_0
    const/4 v0, 0x0

    .line 146
    :goto_0
    invoke-virtual {p0, p1}, Lcom/helpshift/support/conversations/ConversationalFragment;->initialize(Landroid/view/View;)V

    .line 147
    invoke-super {p0, p1, p2}, Lcom/helpshift/support/conversations/BaseConversationFragment;->onViewCreated(Landroid/view/View;Landroid/os/Bundle;)V

    if-eqz p2, :cond_1

    const-string p1, "should_show_unread_message_indicator"

    .line 150
    invoke-virtual {p2, p1}, Landroid/os/Bundle;->getBoolean(Ljava/lang/String;)Z

    move-result p1

    .line 152
    iget-object v1, p0, Lcom/helpshift/support/conversations/ConversationalFragment;->conversationalVM:Lcom/helpshift/conversation/viewmodel/ConversationalVM;

    invoke-virtual {v1, p1}, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->updateUnreadMessageCountIndicator(Z)V

    :cond_1
    if-eqz v0, :cond_2

    if-nez p2, :cond_2

    .line 156
    iget-object p1, p0, Lcom/helpshift/support/conversations/ConversationalFragment;->conversationalVM:Lcom/helpshift/conversation/viewmodel/ConversationalVM;

    invoke-virtual {p1}, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->forceClickOnNewConversationButton()V

    :cond_2
    const-string p1, "Helpshift_ConvalFrag"

    const-string p2, "Now showing conversation screen"

    .line 158
    invoke-static {p1, p2}, Lcom/helpshift/util/HSLogger;->d(Ljava/lang/String;Ljava/lang/String;)V

    return-void
.end method

.method public openFreshConversationScreen(Ljava/util/Map;)V
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Ljava/lang/Boolean;",
            ">;)V"
        }
    .end annotation

    .line 672
    invoke-virtual {p0}, Lcom/helpshift/support/conversations/ConversationalFragment;->getSupportFragment()Lcom/helpshift/support/fragments/SupportFragment;

    move-result-object v0

    invoke-virtual {v0}, Lcom/helpshift/support/fragments/SupportFragment;->getSupportController()Lcom/helpshift/support/controllers/SupportController;

    move-result-object v0

    invoke-virtual {v0, p1}, Lcom/helpshift/support/controllers/SupportController;->startConversationFlow(Ljava/util/Map;)V

    return-void
.end method

.method public resetToolbarImportanceForAccessibility()V
    .locals 1

    .line 463
    invoke-virtual {p0}, Lcom/helpshift/support/conversations/ConversationalFragment;->getSupportFragment()Lcom/helpshift/support/fragments/SupportFragment;

    move-result-object v0

    if-eqz v0, :cond_0

    .line 465
    invoke-virtual {v0}, Lcom/helpshift/support/fragments/SupportFragment;->resetToolbarImportanceForAccessibility()V

    :cond_0
    return-void
.end method

.method public retryMessage(Lcom/helpshift/conversation/activeconversation/message/MessageDM;)V
    .locals 1

    .line 566
    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragment;->conversationalVM:Lcom/helpshift/conversation/viewmodel/ConversationalVM;

    invoke-virtual {v0, p1}, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->retryMessage(Lcom/helpshift/conversation/activeconversation/message/MessageDM;)V

    return-void
.end method

.method public setToolbarImportanceForAccessibility(I)V
    .locals 1

    .line 455
    invoke-virtual {p0}, Lcom/helpshift/support/conversations/ConversationalFragment;->getSupportFragment()Lcom/helpshift/support/fragments/SupportFragment;

    move-result-object v0

    if-eqz v0, :cond_0

    .line 457
    invoke-virtual {v0, p1}, Lcom/helpshift/support/fragments/SupportFragment;->setToolbarImportanceForAccessibility(I)V

    :cond_0
    return-void
.end method

.method public startLiveUpdates()V
    .locals 1

    .line 708
    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragment;->conversationalVM:Lcom/helpshift/conversation/viewmodel/ConversationalVM;

    if-eqz v0, :cond_0

    .line 709
    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragment;->conversationalVM:Lcom/helpshift/conversation/viewmodel/ConversationalVM;

    invoke-virtual {v0}, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->startLiveUpdates()V

    :cond_0
    return-void
.end method

.method public stopLiveUpdates()V
    .locals 1

    .line 714
    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragment;->conversationalVM:Lcom/helpshift/conversation/viewmodel/ConversationalVM;

    if-eqz v0, :cond_0

    .line 715
    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragment;->conversationalVM:Lcom/helpshift/conversation/viewmodel/ConversationalVM;

    invoke-virtual {v0}, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->stopLiveUpdates()V

    :cond_0
    return-void
.end method
