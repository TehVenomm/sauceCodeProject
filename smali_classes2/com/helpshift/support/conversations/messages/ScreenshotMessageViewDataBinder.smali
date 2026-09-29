.class public Lcom/helpshift/support/conversations/messages/ScreenshotMessageViewDataBinder;
.super Lcom/helpshift/support/conversations/messages/MessageViewDataBinder;
.source "ScreenshotMessageViewDataBinder.java"


# annotations
.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Lcom/helpshift/support/conversations/messages/ScreenshotMessageViewDataBinder$ViewHolder;
    }
.end annotation

.annotation system Ldalvik/annotation/Signature;
    value = {
        "Lcom/helpshift/support/conversations/messages/MessageViewDataBinder<",
        "Lcom/helpshift/support/conversations/messages/ScreenshotMessageViewDataBinder$ViewHolder;",
        "Lcom/helpshift/conversation/activeconversation/message/ScreenshotMessageDM;",
        ">;"
    }
.end annotation


# direct methods
.method public constructor <init>(Landroid/content/Context;)V
    .locals 0

    .line 22
    invoke-direct {p0, p1}, Lcom/helpshift/support/conversations/messages/MessageViewDataBinder;-><init>(Landroid/content/Context;)V

    return-void
.end method


# virtual methods
.method public bridge synthetic bind(Landroidx/recyclerview/widget/RecyclerView$ViewHolder;Lcom/helpshift/conversation/activeconversation/message/MessageDM;)V
    .locals 0

    .line 18
    check-cast p1, Lcom/helpshift/support/conversations/messages/ScreenshotMessageViewDataBinder$ViewHolder;

    check-cast p2, Lcom/helpshift/conversation/activeconversation/message/ScreenshotMessageDM;

    invoke-virtual {p0, p1, p2}, Lcom/helpshift/support/conversations/messages/ScreenshotMessageViewDataBinder;->bind(Lcom/helpshift/support/conversations/messages/ScreenshotMessageViewDataBinder$ViewHolder;Lcom/helpshift/conversation/activeconversation/message/ScreenshotMessageDM;)V

    return-void
.end method

.method public bind(Lcom/helpshift/support/conversations/messages/ScreenshotMessageViewDataBinder$ViewHolder;Lcom/helpshift/conversation/activeconversation/message/ScreenshotMessageDM;)V
    .locals 16

    move-object/from16 v0, p0

    move-object/from16 v1, p1

    move-object/from16 v2, p2

    .line 36
    invoke-virtual/range {p2 .. p2}, Lcom/helpshift/conversation/activeconversation/message/ScreenshotMessageDM;->getFilePath()Ljava/lang/String;

    move-result-object v3

    .line 42
    iget-object v4, v0, Lcom/helpshift/support/conversations/messages/ScreenshotMessageViewDataBinder;->context:Landroid/content/Context;

    const v5, 0x1010038

    invoke-static {v4, v5}, Lcom/helpshift/support/util/Styles;->getColor(Landroid/content/Context;I)I

    move-result v4

    .line 43
    invoke-static {v3}, Lcom/helpshift/util/TextUtils;->isEmpty(Ljava/lang/CharSequence;)Z

    move-result v5

    const/4 v6, 0x1

    xor-int/2addr v5, v6

    const/high16 v7, 0x3f000000    # 0.5f

    const-string v8, ""

    .line 47
    sget-object v9, Lcom/helpshift/support/conversations/messages/ScreenshotMessageViewDataBinder$2;->$SwitchMap$com$helpshift$conversation$activeconversation$message$UserMessageState:[I

    iget-object v10, v2, Lcom/helpshift/conversation/activeconversation/message/ScreenshotMessageDM;->state:Lcom/helpshift/conversation/activeconversation/message/UserMessageState;

    invoke-virtual {v10}, Lcom/helpshift/conversation/activeconversation/message/UserMessageState;->ordinal()I

    move-result v10

    aget v9, v9, v10

    const/4 v11, 0x0

    packed-switch v9, :pswitch_data_0

    move v6, v4

    move-object v14, v8

    const/4 v4, 0x0

    :goto_0
    const/4 v8, 0x0

    const/4 v9, 0x0

    :goto_1
    const/4 v12, 0x0

    :goto_2
    const/4 v13, 0x0

    goto/16 :goto_3

    :pswitch_0
    const/high16 v7, 0x3f800000    # 1.0f

    .line 67
    invoke-virtual/range {p2 .. p2}, Lcom/helpshift/conversation/activeconversation/message/ScreenshotMessageDM;->getSubText()Ljava/lang/String;

    move-result-object v8

    .line 68
    invoke-static {v3}, Lcom/helpshift/util/TextUtils;->isEmpty(Ljava/lang/CharSequence;)Z

    move-result v9

    xor-int/lit8 v12, v9, 0x1

    .line 73
    iget-object v13, v0, Lcom/helpshift/support/conversations/messages/ScreenshotMessageViewDataBinder;->context:Landroid/content/Context;

    sget v14, Lcom/helpshift/R$string;->hs__user_sent_message_voice_over:I

    new-array v6, v6, [Ljava/lang/Object;

    .line 74
    invoke-virtual/range {p2 .. p2}, Lcom/helpshift/conversation/activeconversation/message/ScreenshotMessageDM;->getAccessbilityMessageTime()Ljava/lang/String;

    move-result-object v15

    aput-object v15, v6, v11

    invoke-virtual {v13, v14, v6}, Landroid/content/Context;->getString(I[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v6

    move-object v14, v6

    move v13, v12

    const/4 v12, 0x0

    move v6, v4

    move-object v4, v8

    const/4 v8, 0x0

    goto :goto_3

    .line 62
    :pswitch_1
    iget-object v8, v0, Lcom/helpshift/support/conversations/messages/ScreenshotMessageViewDataBinder;->context:Landroid/content/Context;

    invoke-virtual {v8}, Landroid/content/Context;->getResources()Landroid/content/res/Resources;

    move-result-object v8

    sget v9, Lcom/helpshift/R$string;->hs__sending_msg:I

    invoke-virtual {v8, v9}, Landroid/content/res/Resources;->getString(I)Ljava/lang/String;

    move-result-object v8

    .line 63
    iget-object v9, v0, Lcom/helpshift/support/conversations/messages/ScreenshotMessageViewDataBinder;->context:Landroid/content/Context;

    sget v12, Lcom/helpshift/R$string;->hs__user_sending_message_voice_over:I

    invoke-virtual {v9, v12}, Landroid/content/Context;->getString(I)Ljava/lang/String;

    move-result-object v9

    move v6, v4

    move-object v4, v8

    move-object v14, v9

    const/4 v8, 0x0

    const/4 v9, 0x1

    goto :goto_1

    .line 56
    :pswitch_2
    iget-object v4, v0, Lcom/helpshift/support/conversations/messages/ScreenshotMessageViewDataBinder;->context:Landroid/content/Context;

    invoke-virtual {v4}, Landroid/content/Context;->getResources()Landroid/content/res/Resources;

    move-result-object v4

    sget v6, Lcom/helpshift/R$string;->hs__sending_fail_msg:I

    invoke-virtual {v4, v6}, Landroid/content/res/Resources;->getString(I)Ljava/lang/String;

    move-result-object v4

    .line 57
    iget-object v6, v0, Lcom/helpshift/support/conversations/messages/ScreenshotMessageViewDataBinder;->context:Landroid/content/Context;

    sget v8, Lcom/helpshift/R$attr;->hs__errorTextColor:I

    invoke-static {v6, v8}, Lcom/helpshift/support/util/Styles;->getColor(Landroid/content/Context;I)I

    move-result v6

    .line 58
    iget-object v8, v0, Lcom/helpshift/support/conversations/messages/ScreenshotMessageViewDataBinder;->context:Landroid/content/Context;

    sget v9, Lcom/helpshift/R$string;->hs__user_failed_message_voice_over:I

    invoke-virtual {v8, v9}, Landroid/content/Context;->getString(I)Ljava/lang/String;

    move-result-object v8

    move-object v14, v8

    goto :goto_0

    .line 51
    :pswitch_3
    iget-object v4, v0, Lcom/helpshift/support/conversations/messages/ScreenshotMessageViewDataBinder;->context:Landroid/content/Context;

    invoke-virtual {v4}, Landroid/content/Context;->getResources()Landroid/content/res/Resources;

    move-result-object v4

    sget v8, Lcom/helpshift/R$string;->hs__sending_fail_msg:I

    invoke-virtual {v4, v8}, Landroid/content/res/Resources;->getString(I)Ljava/lang/String;

    move-result-object v4

    .line 52
    iget-object v8, v0, Lcom/helpshift/support/conversations/messages/ScreenshotMessageViewDataBinder;->context:Landroid/content/Context;

    sget v9, Lcom/helpshift/R$attr;->hs__errorTextColor:I

    invoke-static {v8, v9}, Lcom/helpshift/support/util/Styles;->getColor(Landroid/content/Context;I)I

    move-result v8

    .line 53
    iget-object v9, v0, Lcom/helpshift/support/conversations/messages/ScreenshotMessageViewDataBinder;->context:Landroid/content/Context;

    sget v12, Lcom/helpshift/R$string;->hs__user_failed_message_voice_over:I

    invoke-virtual {v9, v12}, Landroid/content/Context;->getString(I)Ljava/lang/String;

    move-result-object v9

    move-object v12, v1

    move v6, v8

    move-object v14, v9

    const/4 v8, 0x1

    const/4 v9, 0x0

    goto :goto_2

    .line 78
    :goto_3
    invoke-virtual/range {p2 .. p2}, Lcom/helpshift/conversation/activeconversation/message/ScreenshotMessageDM;->getUiViewState()Lcom/helpshift/conversation/activeconversation/message/UIViewState;

    move-result-object v15

    .line 79
    iget-object v10, v1, Lcom/helpshift/support/conversations/messages/ScreenshotMessageViewDataBinder$ViewHolder;->roundedImageView:Lcom/helpshift/support/views/HSRoundedImageView;

    invoke-virtual {v10, v3}, Lcom/helpshift/support/views/HSRoundedImageView;->loadImage(Ljava/lang/String;)V

    .line 80
    iget-object v3, v1, Lcom/helpshift/support/conversations/messages/ScreenshotMessageViewDataBinder$ViewHolder;->roundedImageView:Lcom/helpshift/support/views/HSRoundedImageView;

    invoke-virtual {v3, v7}, Lcom/helpshift/support/views/HSRoundedImageView;->setAlpha(F)V

    .line 81
    iget-object v3, v1, Lcom/helpshift/support/conversations/messages/ScreenshotMessageViewDataBinder$ViewHolder;->roundedImageView:Lcom/helpshift/support/views/HSRoundedImageView;

    invoke-virtual {v0, v3, v5}, Lcom/helpshift/support/conversations/messages/ScreenshotMessageViewDataBinder;->setViewVisibility(Landroid/view/View;Z)V

    .line 82
    iget-object v3, v1, Lcom/helpshift/support/conversations/messages/ScreenshotMessageViewDataBinder$ViewHolder;->subText:Landroid/widget/TextView;

    invoke-virtual {v3, v11}, Landroid/widget/TextView;->setVisibility(I)V

    .line 84
    invoke-virtual {v15}, Lcom/helpshift/conversation/activeconversation/message/UIViewState;->isFooterVisible()Z

    move-result v3

    if-eqz v3, :cond_0

    .line 85
    iget-object v3, v1, Lcom/helpshift/support/conversations/messages/ScreenshotMessageViewDataBinder$ViewHolder;->subText:Landroid/widget/TextView;

    invoke-virtual {v3, v4}, Landroid/widget/TextView;->setText(Ljava/lang/CharSequence;)V

    .line 86
    iget-object v3, v1, Lcom/helpshift/support/conversations/messages/ScreenshotMessageViewDataBinder$ViewHolder;->subText:Landroid/widget/TextView;

    invoke-virtual {v3, v6}, Landroid/widget/TextView;->setTextColor(I)V

    .line 88
    :cond_0
    iget-object v3, v1, Lcom/helpshift/support/conversations/messages/ScreenshotMessageViewDataBinder$ViewHolder;->subText:Landroid/widget/TextView;

    invoke-virtual {v15}, Lcom/helpshift/conversation/activeconversation/message/UIViewState;->isFooterVisible()Z

    move-result v4

    invoke-virtual {v0, v3, v4}, Lcom/helpshift/support/conversations/messages/ScreenshotMessageViewDataBinder;->setViewVisibility(Landroid/view/View;Z)V

    .line 89
    invoke-static/range {p1 .. p1}, Lcom/helpshift/support/conversations/messages/ScreenshotMessageViewDataBinder$ViewHolder;->access$000(Lcom/helpshift/support/conversations/messages/ScreenshotMessageViewDataBinder$ViewHolder;)Landroid/widget/ProgressBar;

    move-result-object v3

    invoke-virtual {v0, v3, v9}, Lcom/helpshift/support/conversations/messages/ScreenshotMessageViewDataBinder;->setViewVisibility(Landroid/view/View;Z)V

    .line 90
    iget-object v3, v1, Lcom/helpshift/support/conversations/messages/ScreenshotMessageViewDataBinder$ViewHolder;->retryButton:Landroid/widget/ImageView;

    invoke-virtual {v0, v3, v8}, Lcom/helpshift/support/conversations/messages/ScreenshotMessageViewDataBinder;->setViewVisibility(Landroid/view/View;Z)V

    if-eqz v8, :cond_1

    .line 92
    iget-object v3, v1, Lcom/helpshift/support/conversations/messages/ScreenshotMessageViewDataBinder$ViewHolder;->retryButton:Landroid/widget/ImageView;

    invoke-virtual {v3, v12}, Landroid/widget/ImageView;->setOnClickListener(Landroid/view/View$OnClickListener;)V

    const/4 v4, 0x0

    goto :goto_4

    .line 95
    :cond_1
    iget-object v3, v1, Lcom/helpshift/support/conversations/messages/ScreenshotMessageViewDataBinder$ViewHolder;->retryButton:Landroid/widget/ImageView;

    const/4 v4, 0x0

    invoke-virtual {v3, v4}, Landroid/widget/ImageView;->setOnClickListener(Landroid/view/View$OnClickListener;)V

    :goto_4
    if-eqz v13, :cond_2

    .line 99
    iget-object v3, v1, Lcom/helpshift/support/conversations/messages/ScreenshotMessageViewDataBinder$ViewHolder;->roundedImageView:Lcom/helpshift/support/views/HSRoundedImageView;

    new-instance v4, Lcom/helpshift/support/conversations/messages/ScreenshotMessageViewDataBinder$1;

    invoke-direct {v4, v0, v2}, Lcom/helpshift/support/conversations/messages/ScreenshotMessageViewDataBinder$1;-><init>(Lcom/helpshift/support/conversations/messages/ScreenshotMessageViewDataBinder;Lcom/helpshift/conversation/activeconversation/message/ScreenshotMessageDM;)V

    invoke-virtual {v3, v4}, Lcom/helpshift/support/views/HSRoundedImageView;->setOnClickListener(Landroid/view/View$OnClickListener;)V

    goto :goto_5

    .line 107
    :cond_2
    iget-object v2, v1, Lcom/helpshift/support/conversations/messages/ScreenshotMessageViewDataBinder$ViewHolder;->roundedImageView:Lcom/helpshift/support/views/HSRoundedImageView;

    invoke-virtual {v2, v4}, Lcom/helpshift/support/views/HSRoundedImageView;->setOnClickListener(Landroid/view/View$OnClickListener;)V

    .line 109
    :goto_5
    iget-object v1, v1, Lcom/helpshift/support/conversations/messages/ScreenshotMessageViewDataBinder$ViewHolder;->messageLayout:Landroid/view/View;

    invoke-virtual {v1, v14}, Landroid/view/View;->setContentDescription(Ljava/lang/CharSequence;)V

    return-void

    nop

    :pswitch_data_0
    .packed-switch 0x1
        :pswitch_3
        :pswitch_2
        :pswitch_1
        :pswitch_0
    .end packed-switch
.end method

.method public bridge synthetic createViewHolder(Landroid/view/ViewGroup;)Landroidx/recyclerview/widget/RecyclerView$ViewHolder;
    .locals 0

    .line 18
    invoke-virtual {p0, p1}, Lcom/helpshift/support/conversations/messages/ScreenshotMessageViewDataBinder;->createViewHolder(Landroid/view/ViewGroup;)Lcom/helpshift/support/conversations/messages/ScreenshotMessageViewDataBinder$ViewHolder;

    move-result-object p1

    return-object p1
.end method

.method public createViewHolder(Landroid/view/ViewGroup;)Lcom/helpshift/support/conversations/messages/ScreenshotMessageViewDataBinder$ViewHolder;
    .locals 3

    .line 27
    iget-object v0, p0, Lcom/helpshift/support/conversations/messages/ScreenshotMessageViewDataBinder;->context:Landroid/content/Context;

    invoke-static {v0}, Landroid/view/LayoutInflater;->from(Landroid/content/Context;)Landroid/view/LayoutInflater;

    move-result-object v0

    sget v1, Lcom/helpshift/R$layout;->hs__msg_screenshot_status:I

    const/4 v2, 0x0

    .line 28
    invoke-virtual {v0, v1, p1, v2}, Landroid/view/LayoutInflater;->inflate(ILandroid/view/ViewGroup;Z)Landroid/view/View;

    move-result-object p1

    .line 30
    new-instance v0, Lcom/helpshift/support/conversations/messages/ScreenshotMessageViewDataBinder$ViewHolder;

    invoke-direct {v0, p0, p1}, Lcom/helpshift/support/conversations/messages/ScreenshotMessageViewDataBinder$ViewHolder;-><init>(Lcom/helpshift/support/conversations/messages/ScreenshotMessageViewDataBinder;Landroid/view/View;)V

    return-object v0
.end method
