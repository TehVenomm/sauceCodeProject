.class public Lcom/helpshift/support/conversations/messages/UserSelectableOptionViewDataBinder;
.super Lcom/helpshift/support/conversations/messages/MessageViewDataBinder;
.source "UserSelectableOptionViewDataBinder.java"


# annotations
.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Lcom/helpshift/support/conversations/messages/UserSelectableOptionViewDataBinder$OnOptionSelectedListener;,
        Lcom/helpshift/support/conversations/messages/UserSelectableOptionViewDataBinder$ViewHolder;
    }
.end annotation

.annotation system Ldalvik/annotation/Signature;
    value = {
        "Lcom/helpshift/support/conversations/messages/MessageViewDataBinder<",
        "Lcom/helpshift/support/conversations/messages/UserSelectableOptionViewDataBinder$ViewHolder;",
        "Lcom/helpshift/conversation/activeconversation/message/OptionInputMessageDM;",
        ">;"
    }
.end annotation


# direct methods
.method constructor <init>(Landroid/content/Context;)V
    .locals 0

    .line 25
    invoke-direct {p0, p1}, Lcom/helpshift/support/conversations/messages/MessageViewDataBinder;-><init>(Landroid/content/Context;)V

    return-void
.end method


# virtual methods
.method public bridge synthetic bind(Landroidx/recyclerview/widget/RecyclerView$ViewHolder;Lcom/helpshift/conversation/activeconversation/message/MessageDM;)V
    .locals 0

    .line 21
    check-cast p1, Lcom/helpshift/support/conversations/messages/UserSelectableOptionViewDataBinder$ViewHolder;

    check-cast p2, Lcom/helpshift/conversation/activeconversation/message/OptionInputMessageDM;

    invoke-virtual {p0, p1, p2}, Lcom/helpshift/support/conversations/messages/UserSelectableOptionViewDataBinder;->bind(Lcom/helpshift/support/conversations/messages/UserSelectableOptionViewDataBinder$ViewHolder;Lcom/helpshift/conversation/activeconversation/message/OptionInputMessageDM;)V

    return-void
.end method

.method public bind(Lcom/helpshift/support/conversations/messages/UserSelectableOptionViewDataBinder$ViewHolder;Lcom/helpshift/conversation/activeconversation/message/OptionInputMessageDM;)V
    .locals 23

    move-object/from16 v6, p0

    move-object/from16 v7, p1

    move-object/from16 v8, p2

    .line 42
    iget-object v0, v7, Lcom/helpshift/support/conversations/messages/UserSelectableOptionViewDataBinder$ViewHolder;->optionsContainer:Landroid/widget/LinearLayout;

    invoke-virtual {v0}, Landroid/widget/LinearLayout;->removeAllViews()V

    .line 44
    iget-object v0, v8, Lcom/helpshift/conversation/activeconversation/message/OptionInputMessageDM;->input:Lcom/helpshift/conversation/activeconversation/message/input/OptionInput;

    iget-object v0, v0, Lcom/helpshift/conversation/activeconversation/message/input/OptionInput;->inputLabel:Ljava/lang/String;

    invoke-static {v0}, Lcom/helpshift/common/StringUtils;->isEmpty(Ljava/lang/String;)Z

    move-result v0

    const/16 v9, 0x8

    const/4 v10, 0x0

    if-nez v0, :cond_0

    .line 45
    iget-object v0, v7, Lcom/helpshift/support/conversations/messages/UserSelectableOptionViewDataBinder$ViewHolder;->optionsHeaderTextView:Landroid/widget/TextView;

    invoke-virtual {v0, v10}, Landroid/widget/TextView;->setVisibility(I)V

    .line 46
    iget-object v0, v7, Lcom/helpshift/support/conversations/messages/UserSelectableOptionViewDataBinder$ViewHolder;->optionsHeaderTextView:Landroid/widget/TextView;

    iget-object v1, v8, Lcom/helpshift/conversation/activeconversation/message/OptionInputMessageDM;->input:Lcom/helpshift/conversation/activeconversation/message/input/OptionInput;

    iget-object v1, v1, Lcom/helpshift/conversation/activeconversation/message/input/OptionInput;->inputLabel:Ljava/lang/String;

    invoke-virtual {v0, v1}, Landroid/widget/TextView;->setText(Ljava/lang/CharSequence;)V

    goto :goto_0

    .line 49
    :cond_0
    iget-object v0, v7, Lcom/helpshift/support/conversations/messages/UserSelectableOptionViewDataBinder$ViewHolder;->optionsHeaderTextView:Landroid/widget/TextView;

    invoke-virtual {v0, v9}, Landroid/widget/TextView;->setVisibility(I)V

    .line 52
    :goto_0
    new-instance v22, Lcom/helpshift/support/conversations/messages/UserSelectableOptionViewDataBinder$OnOptionSelectedListener;

    iget-object v3, v6, Lcom/helpshift/support/conversations/messages/UserSelectableOptionViewDataBinder;->messageClickListener:Lcom/helpshift/support/conversations/messages/MessageViewDataBinder$MessageItemClickListener;

    const/4 v5, 0x0

    move-object/from16 v0, v22

    move-object/from16 v1, p0

    move-object/from16 v2, p1

    move-object/from16 v4, p2

    invoke-direct/range {v0 .. v5}, Lcom/helpshift/support/conversations/messages/UserSelectableOptionViewDataBinder$OnOptionSelectedListener;-><init>(Lcom/helpshift/support/conversations/messages/UserSelectableOptionViewDataBinder;Lcom/helpshift/support/conversations/messages/UserSelectableOptionViewDataBinder$ViewHolder;Lcom/helpshift/support/conversations/messages/MessageViewDataBinder$MessageItemClickListener;Lcom/helpshift/conversation/activeconversation/message/OptionInputMessageDM;Z)V

    .line 57
    iget-object v0, v6, Lcom/helpshift/support/conversations/messages/UserSelectableOptionViewDataBinder;->context:Landroid/content/Context;

    invoke-static {v0}, Lcom/helpshift/support/util/Styles;->isTablet(Landroid/content/Context;)Z

    move-result v0

    if-eqz v0, :cond_1

    const-wide v0, 0x3fe3333333333334L    # 0.6000000000000001

    :goto_1
    move-wide v13, v0

    goto :goto_2

    :cond_1
    const-wide v0, 0x3fe999999999999aL    # 0.8

    goto :goto_1

    .line 58
    :goto_2
    iget-object v0, v6, Lcom/helpshift/support/conversations/messages/UserSelectableOptionViewDataBinder;->context:Landroid/content/Context;

    invoke-virtual {v0}, Landroid/content/Context;->getResources()Landroid/content/res/Resources;

    move-result-object v0

    sget v1, Lcom/helpshift/R$dimen;->activity_horizontal_margin_medium:I

    invoke-virtual {v0, v1}, Landroid/content/res/Resources;->getDimension(I)F

    move-result v0

    float-to-int v15, v0

    .line 60
    new-instance v0, Lcom/helpshift/support/views/HSAdjustableSelectOptionsViewInflater;

    iget-object v12, v6, Lcom/helpshift/support/conversations/messages/UserSelectableOptionViewDataBinder;->context:Landroid/content/Context;

    iget-object v1, v7, Lcom/helpshift/support/conversations/messages/UserSelectableOptionViewDataBinder$ViewHolder;->optionsContainer:Landroid/widget/LinearLayout;

    sget v17, Lcom/helpshift/R$layout;->hs__msg_user_selectable_option:I

    sget v18, Lcom/helpshift/R$id;->selectable_option_text:I

    sget v19, Lcom/helpshift/R$drawable;->hs__pill:I

    sget v20, Lcom/helpshift/R$attr;->hs__selectableOptionColor:I

    iget-object v2, v8, Lcom/helpshift/conversation/activeconversation/message/OptionInputMessageDM;->input:Lcom/helpshift/conversation/activeconversation/message/input/OptionInput;

    iget-object v2, v2, Lcom/helpshift/conversation/activeconversation/message/input/OptionInput;->options:Ljava/util/List;

    move-object v11, v0

    move-object/from16 v16, v1

    move-object/from16 v21, v2

    invoke-direct/range {v11 .. v22}, Lcom/helpshift/support/views/HSAdjustableSelectOptionsViewInflater;-><init>(Landroid/content/Context;DILandroid/widget/LinearLayout;IIIILjava/util/List;Landroid/view/View$OnClickListener;)V

    .line 72
    invoke-virtual {v0}, Lcom/helpshift/support/views/HSAdjustableSelectOptionsViewInflater;->inflate()V

    .line 74
    iget-object v0, v8, Lcom/helpshift/conversation/activeconversation/message/OptionInputMessageDM;->input:Lcom/helpshift/conversation/activeconversation/message/input/OptionInput;

    iget-boolean v0, v0, Lcom/helpshift/conversation/activeconversation/message/input/OptionInput;->required:Z

    if-nez v0, :cond_2

    .line 77
    iget-object v0, v8, Lcom/helpshift/conversation/activeconversation/message/OptionInputMessageDM;->input:Lcom/helpshift/conversation/activeconversation/message/input/OptionInput;

    iget-object v0, v0, Lcom/helpshift/conversation/activeconversation/message/input/OptionInput;->skipLabel:Ljava/lang/String;

    invoke-static {v0}, Lcom/helpshift/common/StringUtils;->isEmpty(Ljava/lang/String;)Z

    move-result v0

    if-nez v0, :cond_2

    .line 81
    iget-object v0, v7, Lcom/helpshift/support/conversations/messages/UserSelectableOptionViewDataBinder$ViewHolder;->optionsSkipTextView:Landroid/widget/TextView;

    invoke-virtual {v0}, Landroid/widget/TextView;->getPaddingLeft()I

    move-result v0

    .line 82
    iget-object v1, v7, Lcom/helpshift/support/conversations/messages/UserSelectableOptionViewDataBinder$ViewHolder;->optionsSkipTextView:Landroid/widget/TextView;

    invoke-virtual {v1}, Landroid/widget/TextView;->getPaddingTop()I

    move-result v1

    .line 83
    iget-object v2, v7, Lcom/helpshift/support/conversations/messages/UserSelectableOptionViewDataBinder$ViewHolder;->optionsSkipTextView:Landroid/widget/TextView;

    invoke-virtual {v2}, Landroid/widget/TextView;->getPaddingRight()I

    move-result v2

    .line 84
    iget-object v3, v7, Lcom/helpshift/support/conversations/messages/UserSelectableOptionViewDataBinder$ViewHolder;->optionsSkipTextView:Landroid/widget/TextView;

    invoke-virtual {v3}, Landroid/widget/TextView;->getPaddingBottom()I

    move-result v3

    .line 85
    iget-object v4, v7, Lcom/helpshift/support/conversations/messages/UserSelectableOptionViewDataBinder$ViewHolder;->optionsSkipTextView:Landroid/widget/TextView;

    sget v5, Lcom/helpshift/R$drawable;->hs__pill_small:I

    sget v9, Lcom/helpshift/R$attr;->hs__selectableOptionColor:I

    invoke-virtual {v6, v4, v5, v9}, Lcom/helpshift/support/conversations/messages/UserSelectableOptionViewDataBinder;->setDrawable(Landroid/view/View;II)V

    .line 86
    iget-object v4, v7, Lcom/helpshift/support/conversations/messages/UserSelectableOptionViewDataBinder$ViewHolder;->optionsSkipTextView:Landroid/widget/TextView;

    invoke-virtual {v4, v0, v1, v2, v3}, Landroid/widget/TextView;->setPadding(IIII)V

    .line 88
    iget-object v0, v7, Lcom/helpshift/support/conversations/messages/UserSelectableOptionViewDataBinder$ViewHolder;->optionsSkipTextView:Landroid/widget/TextView;

    iget-object v1, v8, Lcom/helpshift/conversation/activeconversation/message/OptionInputMessageDM;->input:Lcom/helpshift/conversation/activeconversation/message/input/OptionInput;

    iget-object v1, v1, Lcom/helpshift/conversation/activeconversation/message/input/OptionInput;->skipLabel:Ljava/lang/String;

    invoke-virtual {v0, v1}, Landroid/widget/TextView;->setText(Ljava/lang/CharSequence;)V

    .line 89
    iget-object v0, v7, Lcom/helpshift/support/conversations/messages/UserSelectableOptionViewDataBinder$ViewHolder;->optionsSkipTextView:Landroid/widget/TextView;

    invoke-virtual {v0, v10}, Landroid/widget/TextView;->setVisibility(I)V

    .line 90
    iget-object v9, v7, Lcom/helpshift/support/conversations/messages/UserSelectableOptionViewDataBinder$ViewHolder;->optionsSkipTextView:Landroid/widget/TextView;

    new-instance v10, Lcom/helpshift/support/conversations/messages/UserSelectableOptionViewDataBinder$OnOptionSelectedListener;

    iget-object v3, v6, Lcom/helpshift/support/conversations/messages/UserSelectableOptionViewDataBinder;->messageClickListener:Lcom/helpshift/support/conversations/messages/MessageViewDataBinder$MessageItemClickListener;

    const/4 v5, 0x1

    move-object v0, v10

    move-object/from16 v1, p0

    move-object/from16 v2, p1

    move-object/from16 v4, p2

    invoke-direct/range {v0 .. v5}, Lcom/helpshift/support/conversations/messages/UserSelectableOptionViewDataBinder$OnOptionSelectedListener;-><init>(Lcom/helpshift/support/conversations/messages/UserSelectableOptionViewDataBinder;Lcom/helpshift/support/conversations/messages/UserSelectableOptionViewDataBinder$ViewHolder;Lcom/helpshift/support/conversations/messages/MessageViewDataBinder$MessageItemClickListener;Lcom/helpshift/conversation/activeconversation/message/OptionInputMessageDM;Z)V

    invoke-virtual {v9, v10}, Landroid/widget/TextView;->setOnClickListener(Landroid/view/View$OnClickListener;)V

    goto :goto_3

    .line 94
    :cond_2
    iget-object v0, v7, Lcom/helpshift/support/conversations/messages/UserSelectableOptionViewDataBinder$ViewHolder;->optionsSkipTextView:Landroid/widget/TextView;

    invoke-virtual {v0, v9}, Landroid/widget/TextView;->setVisibility(I)V

    :goto_3
    return-void
.end method

.method public bridge synthetic createViewHolder(Landroid/view/ViewGroup;)Landroidx/recyclerview/widget/RecyclerView$ViewHolder;
    .locals 0

    .line 21
    invoke-virtual {p0, p1}, Lcom/helpshift/support/conversations/messages/UserSelectableOptionViewDataBinder;->createViewHolder(Landroid/view/ViewGroup;)Lcom/helpshift/support/conversations/messages/UserSelectableOptionViewDataBinder$ViewHolder;

    move-result-object p1

    return-object p1
.end method

.method public createViewHolder(Landroid/view/ViewGroup;)Lcom/helpshift/support/conversations/messages/UserSelectableOptionViewDataBinder$ViewHolder;
    .locals 3

    .line 31
    invoke-virtual {p1}, Landroid/view/ViewGroup;->getContext()Landroid/content/Context;

    move-result-object v0

    invoke-static {v0}, Landroid/view/LayoutInflater;->from(Landroid/content/Context;)Landroid/view/LayoutInflater;

    move-result-object v0

    sget v1, Lcom/helpshift/R$layout;->hs__msg_user_selectable_options_container:I

    const/4 v2, 0x0

    invoke-virtual {v0, v1, p1, v2}, Landroid/view/LayoutInflater;->inflate(ILandroid/view/ViewGroup;Z)Landroid/view/View;

    move-result-object p1

    .line 33
    new-instance v0, Lcom/helpshift/support/conversations/messages/UserSelectableOptionViewDataBinder$ViewHolder;

    invoke-direct {v0, p0, p1}, Lcom/helpshift/support/conversations/messages/UserSelectableOptionViewDataBinder$ViewHolder;-><init>(Lcom/helpshift/support/conversations/messages/UserSelectableOptionViewDataBinder;Landroid/view/View;)V

    .line 34
    invoke-virtual {v0, v2}, Lcom/helpshift/support/conversations/messages/UserSelectableOptionViewDataBinder$ViewHolder;->setIsRecyclable(Z)V

    return-object v0
.end method
