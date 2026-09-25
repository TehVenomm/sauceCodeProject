.class Lcom/helpshift/support/conversations/messages/AdminImageAttachmentMessageDataBinder;
.super Lcom/helpshift/support/conversations/messages/MessageViewDataBinder;
.source "AdminImageAttachmentMessageDataBinder.java"


# annotations
.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Lcom/helpshift/support/conversations/messages/AdminImageAttachmentMessageDataBinder$ViewHolder;
    }
.end annotation

.annotation system Ldalvik/annotation/Signature;
    value = {
        "Lcom/helpshift/support/conversations/messages/MessageViewDataBinder<",
        "Lcom/helpshift/support/conversations/messages/AdminImageAttachmentMessageDataBinder$ViewHolder;",
        "Lcom/helpshift/conversation/activeconversation/message/AdminImageAttachmentMessageDM;",
        ">;"
    }
.end annotation


# direct methods
.method constructor <init>(Landroid/content/Context;)V
    .locals 0

    .line 22
    invoke-direct {p0, p1}, Lcom/helpshift/support/conversations/messages/MessageViewDataBinder;-><init>(Landroid/content/Context;)V

    return-void
.end method


# virtual methods
.method public bridge synthetic bind(Landroidx/recyclerview/widget/RecyclerView$ViewHolder;Lcom/helpshift/conversation/activeconversation/message/MessageDM;)V
    .locals 0

    .line 17
    check-cast p1, Lcom/helpshift/support/conversations/messages/AdminImageAttachmentMessageDataBinder$ViewHolder;

    check-cast p2, Lcom/helpshift/conversation/activeconversation/message/AdminImageAttachmentMessageDM;

    invoke-virtual {p0, p1, p2}, Lcom/helpshift/support/conversations/messages/AdminImageAttachmentMessageDataBinder;->bind(Lcom/helpshift/support/conversations/messages/AdminImageAttachmentMessageDataBinder$ViewHolder;Lcom/helpshift/conversation/activeconversation/message/AdminImageAttachmentMessageDM;)V

    return-void
.end method

.method public bind(Lcom/helpshift/support/conversations/messages/AdminImageAttachmentMessageDataBinder$ViewHolder;Lcom/helpshift/conversation/activeconversation/message/AdminImageAttachmentMessageDM;)V
    .locals 11

    const-string v0, ""

    .line 43
    invoke-virtual {p2}, Lcom/helpshift/conversation/activeconversation/message/AdminImageAttachmentMessageDM;->getFormattedFileSize()Ljava/lang/String;

    move-result-object v1

    .line 44
    sget-object v2, Lcom/helpshift/support/conversations/messages/AdminImageAttachmentMessageDataBinder$2;->$SwitchMap$com$helpshift$conversation$activeconversation$message$AdminImageAttachmentMessageDM$AdminImageAttachmentState:[I

    iget-object v3, p2, Lcom/helpshift/conversation/activeconversation/message/AdminImageAttachmentMessageDM;->state:Lcom/helpshift/conversation/activeconversation/message/AdminImageAttachmentMessageDM$AdminImageAttachmentState;

    invoke-virtual {v3}, Lcom/helpshift/conversation/activeconversation/message/AdminImageAttachmentMessageDM$AdminImageAttachmentState;->ordinal()I

    move-result v3

    aget v2, v2, v3

    const/4 v3, 0x0

    const/4 v4, 0x0

    const/4 v5, 0x1

    packed-switch v2, :pswitch_data_0

    move-object v9, v0

    move-object v7, v1

    move-object v6, v3

    const/4 v0, 0x1

    const/4 v1, 0x0

    :goto_0
    const/4 v2, 0x1

    const/4 v4, 0x1

    :goto_1
    const/4 v8, 0x1

    goto/16 :goto_4

    .line 74
    :pswitch_0
    invoke-virtual {p2}, Lcom/helpshift/conversation/activeconversation/message/AdminImageAttachmentMessageDM;->checkAndGetFilePath()Ljava/lang/String;

    move-result-object v0

    .line 77
    iget-object v2, p0, Lcom/helpshift/support/conversations/messages/AdminImageAttachmentMessageDataBinder;->context:Landroid/content/Context;

    sget v6, Lcom/helpshift/R$string;->hs__image_downloaded_voice_over:I

    invoke-virtual {v2, v6}, Landroid/content/Context;->getString(I)Ljava/lang/String;

    move-result-object v2

    move-object v6, v0

    move-object v7, v1

    move-object v9, v2

    const/4 v0, 0x0

    const/4 v1, 0x0

    const/4 v2, 0x0

    goto :goto_1

    .line 65
    :pswitch_1
    invoke-virtual {p2}, Lcom/helpshift/conversation/activeconversation/message/AdminImageAttachmentMessageDM;->checkAndGetThumbnailFilePath()Ljava/lang/String;

    move-result-object v0

    .line 67
    invoke-virtual {p2}, Lcom/helpshift/conversation/activeconversation/message/AdminImageAttachmentMessageDM;->getDownloadProgressAndFileSize()Ljava/lang/String;

    move-result-object v1

    .line 68
    iget-object v2, p0, Lcom/helpshift/support/conversations/messages/AdminImageAttachmentMessageDataBinder;->context:Landroid/content/Context;

    sget v6, Lcom/helpshift/R$string;->hs__image_downloading_voice_over:I

    const/4 v7, 0x2

    new-array v7, v7, [Ljava/lang/Object;

    .line 69
    invoke-virtual {p2}, Lcom/helpshift/conversation/activeconversation/message/AdminImageAttachmentMessageDM;->getDownloadedProgressSize()Ljava/lang/String;

    move-result-object v8

    aput-object v8, v7, v4

    .line 70
    invoke-virtual {p2}, Lcom/helpshift/conversation/activeconversation/message/AdminImageAttachmentMessageDM;->getFormattedFileSize()Ljava/lang/String;

    move-result-object v8

    aput-object v8, v7, v5

    .line 68
    invoke-virtual {v2, v6, v7}, Landroid/content/Context;->getString(I[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v2

    move-object v6, v0

    move-object v7, v1

    move-object v9, v2

    const/4 v0, 0x1

    const/4 v1, 0x0

    const/4 v2, 0x1

    const/4 v4, 0x1

    const/4 v8, 0x0

    goto :goto_4

    .line 58
    :pswitch_2
    invoke-virtual {p2}, Lcom/helpshift/conversation/activeconversation/message/AdminImageAttachmentMessageDM;->checkAndGetThumbnailFilePath()Ljava/lang/String;

    move-result-object v0

    .line 61
    iget-object v2, p0, Lcom/helpshift/support/conversations/messages/AdminImageAttachmentMessageDataBinder;->context:Landroid/content/Context;

    sget v6, Lcom/helpshift/R$string;->hs__image_not_downloaded_voice_over:I

    new-array v7, v5, [Ljava/lang/Object;

    .line 62
    invoke-virtual {p2}, Lcom/helpshift/conversation/activeconversation/message/AdminImageAttachmentMessageDM;->getFormattedFileSize()Ljava/lang/String;

    move-result-object v8

    aput-object v8, v7, v4

    .line 61
    invoke-virtual {v2, v6, v7}, Landroid/content/Context;->getString(I[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v2

    move-object v6, v0

    move-object v7, v1

    move-object v9, v2

    goto :goto_3

    .line 54
    :pswitch_3
    iget-object v0, p0, Lcom/helpshift/support/conversations/messages/AdminImageAttachmentMessageDataBinder;->context:Landroid/content/Context;

    sget v2, Lcom/helpshift/R$string;->hs__image_not_downloaded_voice_over:I

    new-array v6, v5, [Ljava/lang/Object;

    .line 55
    invoke-virtual {p2}, Lcom/helpshift/conversation/activeconversation/message/AdminImageAttachmentMessageDM;->getFormattedFileSize()Ljava/lang/String;

    move-result-object v7

    aput-object v7, v6, v4

    .line 54
    invoke-virtual {v0, v2, v6}, Landroid/content/Context;->getString(I[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v0

    goto :goto_2

    .line 48
    :pswitch_4
    iget-object v0, p0, Lcom/helpshift/support/conversations/messages/AdminImageAttachmentMessageDataBinder;->context:Landroid/content/Context;

    sget v2, Lcom/helpshift/R$string;->hs__image_not_downloaded_voice_over:I

    new-array v6, v5, [Ljava/lang/Object;

    .line 49
    invoke-virtual {p2}, Lcom/helpshift/conversation/activeconversation/message/AdminImageAttachmentMessageDM;->getFormattedFileSize()Ljava/lang/String;

    move-result-object v7

    aput-object v7, v6, v4

    .line 48
    invoke-virtual {v0, v2, v6}, Landroid/content/Context;->getString(I[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v0

    :goto_2
    move-object v9, v0

    move-object v7, v1

    move-object v6, v3

    :goto_3
    const/4 v0, 0x0

    const/4 v1, 0x1

    goto :goto_0

    .line 81
    :goto_4
    iget-object v10, p1, Lcom/helpshift/support/conversations/messages/AdminImageAttachmentMessageDataBinder$ViewHolder;->downloadProgressbarContainer:Landroid/view/View;

    invoke-virtual {p0, v10, v4}, Lcom/helpshift/support/conversations/messages/AdminImageAttachmentMessageDataBinder;->setViewVisibility(Landroid/view/View;Z)V

    .line 82
    iget-object v4, p1, Lcom/helpshift/support/conversations/messages/AdminImageAttachmentMessageDataBinder$ViewHolder;->progressBarView:Landroid/widget/ProgressBar;

    invoke-virtual {p0, v4, v0}, Lcom/helpshift/support/conversations/messages/AdminImageAttachmentMessageDataBinder;->setViewVisibility(Landroid/view/View;Z)V

    .line 83
    iget-object v0, p1, Lcom/helpshift/support/conversations/messages/AdminImageAttachmentMessageDataBinder$ViewHolder;->downloadButtonView:Landroid/view/View;

    invoke-virtual {p0, v0, v1}, Lcom/helpshift/support/conversations/messages/AdminImageAttachmentMessageDataBinder;->setViewVisibility(Landroid/view/View;Z)V

    if-eqz v2, :cond_0

    .line 85
    iget-object v0, p1, Lcom/helpshift/support/conversations/messages/AdminImageAttachmentMessageDataBinder$ViewHolder;->roundedImageView:Lcom/helpshift/support/views/HSRoundedImageView;

    const/high16 v2, 0x3e800000    # 0.25f

    invoke-virtual {v0, v2}, Lcom/helpshift/support/views/HSRoundedImageView;->setAlpha(F)V

    goto :goto_5

    .line 88
    :cond_0
    iget-object v0, p1, Lcom/helpshift/support/conversations/messages/AdminImageAttachmentMessageDataBinder$ViewHolder;->roundedImageView:Lcom/helpshift/support/views/HSRoundedImageView;

    const/high16 v2, 0x3f800000    # 1.0f

    invoke-virtual {v0, v2}, Lcom/helpshift/support/views/HSRoundedImageView;->setAlpha(F)V

    .line 90
    :goto_5
    iget-object v0, p1, Lcom/helpshift/support/conversations/messages/AdminImageAttachmentMessageDataBinder$ViewHolder;->fileSize:Landroid/widget/TextView;

    invoke-virtual {p0, v0, v5}, Lcom/helpshift/support/conversations/messages/AdminImageAttachmentMessageDataBinder;->setViewVisibility(Landroid/view/View;Z)V

    .line 92
    iget-object v0, p1, Lcom/helpshift/support/conversations/messages/AdminImageAttachmentMessageDataBinder$ViewHolder;->roundedImageView:Lcom/helpshift/support/views/HSRoundedImageView;

    invoke-virtual {v0, v6}, Lcom/helpshift/support/views/HSRoundedImageView;->loadImage(Ljava/lang/String;)V

    .line 93
    invoke-virtual {p2}, Lcom/helpshift/conversation/activeconversation/message/AdminImageAttachmentMessageDM;->getUiViewState()Lcom/helpshift/conversation/activeconversation/message/UIViewState;

    move-result-object v0

    .line 94
    invoke-virtual {v0}, Lcom/helpshift/conversation/activeconversation/message/UIViewState;->isFooterVisible()Z

    move-result v2

    if-eqz v2, :cond_1

    .line 95
    iget-object v2, p1, Lcom/helpshift/support/conversations/messages/AdminImageAttachmentMessageDataBinder$ViewHolder;->subText:Landroid/widget/TextView;

    invoke-virtual {p2}, Lcom/helpshift/conversation/activeconversation/message/AdminImageAttachmentMessageDM;->getSubText()Ljava/lang/String;

    move-result-object v4

    invoke-virtual {v2, v4}, Landroid/widget/TextView;->setText(Ljava/lang/CharSequence;)V

    .line 97
    :cond_1
    iget-object v2, p1, Lcom/helpshift/support/conversations/messages/AdminImageAttachmentMessageDataBinder$ViewHolder;->subText:Landroid/widget/TextView;

    invoke-virtual {v0}, Lcom/helpshift/conversation/activeconversation/message/UIViewState;->isFooterVisible()Z

    move-result v0

    invoke-virtual {p0, v2, v0}, Lcom/helpshift/support/conversations/messages/AdminImageAttachmentMessageDataBinder;->setViewVisibility(Landroid/view/View;Z)V

    .line 100
    iget-object v0, p1, Lcom/helpshift/support/conversations/messages/AdminImageAttachmentMessageDataBinder$ViewHolder;->fileSize:Landroid/widget/TextView;

    invoke-virtual {v0, v7}, Landroid/widget/TextView;->setText(Ljava/lang/CharSequence;)V

    .line 103
    new-instance v0, Lcom/helpshift/support/conversations/messages/AdminImageAttachmentMessageDataBinder$1;

    invoke-direct {v0, p0, p2}, Lcom/helpshift/support/conversations/messages/AdminImageAttachmentMessageDataBinder$1;-><init>(Lcom/helpshift/support/conversations/messages/AdminImageAttachmentMessageDataBinder;Lcom/helpshift/conversation/activeconversation/message/AdminImageAttachmentMessageDM;)V

    if-eqz v1, :cond_2

    .line 113
    iget-object v1, p1, Lcom/helpshift/support/conversations/messages/AdminImageAttachmentMessageDataBinder$ViewHolder;->downloadButtonView:Landroid/view/View;

    invoke-virtual {v1, v0}, Landroid/view/View;->setOnClickListener(Landroid/view/View$OnClickListener;)V

    goto :goto_6

    .line 116
    :cond_2
    iget-object v1, p1, Lcom/helpshift/support/conversations/messages/AdminImageAttachmentMessageDataBinder$ViewHolder;->downloadButtonView:Landroid/view/View;

    invoke-virtual {v1, v3}, Landroid/view/View;->setOnClickListener(Landroid/view/View$OnClickListener;)V

    :goto_6
    if-eqz v8, :cond_3

    .line 120
    iget-object v1, p1, Lcom/helpshift/support/conversations/messages/AdminImageAttachmentMessageDataBinder$ViewHolder;->roundedImageView:Lcom/helpshift/support/views/HSRoundedImageView;

    invoke-virtual {v1, v0}, Lcom/helpshift/support/views/HSRoundedImageView;->setOnClickListener(Landroid/view/View$OnClickListener;)V

    goto :goto_7

    .line 123
    :cond_3
    iget-object v0, p1, Lcom/helpshift/support/conversations/messages/AdminImageAttachmentMessageDataBinder$ViewHolder;->roundedImageView:Lcom/helpshift/support/views/HSRoundedImageView;

    invoke-virtual {v0, v3}, Lcom/helpshift/support/views/HSRoundedImageView;->setOnClickListener(Landroid/view/View$OnClickListener;)V

    .line 125
    :goto_7
    iget-object v0, p1, Lcom/helpshift/support/conversations/messages/AdminImageAttachmentMessageDataBinder$ViewHolder;->roundedImageView:Lcom/helpshift/support/views/HSRoundedImageView;

    invoke-virtual {v0, v9}, Lcom/helpshift/support/views/HSRoundedImageView;->setContentDescription(Ljava/lang/CharSequence;)V

    .line 126
    iget-object p1, p1, Lcom/helpshift/support/conversations/messages/AdminImageAttachmentMessageDataBinder$ViewHolder;->messageLayout:Landroid/view/View;

    invoke-virtual {p0, p2}, Lcom/helpshift/support/conversations/messages/AdminImageAttachmentMessageDataBinder;->getAdminMessageContentDesciption(Lcom/helpshift/conversation/activeconversation/message/MessageDM;)Ljava/lang/String;

    move-result-object p2

    invoke-virtual {p1, p2}, Landroid/view/View;->setContentDescription(Ljava/lang/CharSequence;)V

    return-void

    nop

    :pswitch_data_0
    .packed-switch 0x1
        :pswitch_4
        :pswitch_3
        :pswitch_2
        :pswitch_1
        :pswitch_0
    .end packed-switch
.end method

.method public bridge synthetic createViewHolder(Landroid/view/ViewGroup;)Landroidx/recyclerview/widget/RecyclerView$ViewHolder;
    .locals 0

    .line 17
    invoke-virtual {p0, p1}, Lcom/helpshift/support/conversations/messages/AdminImageAttachmentMessageDataBinder;->createViewHolder(Landroid/view/ViewGroup;)Lcom/helpshift/support/conversations/messages/AdminImageAttachmentMessageDataBinder$ViewHolder;

    move-result-object p1

    return-object p1
.end method

.method public createViewHolder(Landroid/view/ViewGroup;)Lcom/helpshift/support/conversations/messages/AdminImageAttachmentMessageDataBinder$ViewHolder;
    .locals 3

    .line 27
    iget-object v0, p0, Lcom/helpshift/support/conversations/messages/AdminImageAttachmentMessageDataBinder;->context:Landroid/content/Context;

    invoke-static {v0}, Landroid/view/LayoutInflater;->from(Landroid/content/Context;)Landroid/view/LayoutInflater;

    move-result-object v0

    sget v1, Lcom/helpshift/R$layout;->hs__msg_attachment_image:I

    const/4 v2, 0x0

    .line 28
    invoke-virtual {v0, v1, p1, v2}, Landroid/view/LayoutInflater;->inflate(ILandroid/view/ViewGroup;Z)Landroid/view/View;

    move-result-object p1

    .line 29
    new-instance v0, Lcom/helpshift/support/conversations/messages/AdminImageAttachmentMessageDataBinder$ViewHolder;

    invoke-direct {v0, p0, p1}, Lcom/helpshift/support/conversations/messages/AdminImageAttachmentMessageDataBinder$ViewHolder;-><init>(Lcom/helpshift/support/conversations/messages/AdminImageAttachmentMessageDataBinder;Landroid/view/View;)V

    return-object v0
.end method
