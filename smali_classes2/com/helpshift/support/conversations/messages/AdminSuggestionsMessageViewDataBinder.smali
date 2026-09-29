.class public Lcom/helpshift/support/conversations/messages/AdminSuggestionsMessageViewDataBinder;
.super Lcom/helpshift/support/conversations/messages/MessageViewDataBinder;
.source "AdminSuggestionsMessageViewDataBinder.java"


# annotations
.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Lcom/helpshift/support/conversations/messages/AdminSuggestionsMessageViewDataBinder$ViewHolder;
    }
.end annotation

.annotation system Ldalvik/annotation/Signature;
    value = {
        "Lcom/helpshift/support/conversations/messages/MessageViewDataBinder<",
        "Lcom/helpshift/support/conversations/messages/AdminSuggestionsMessageViewDataBinder$ViewHolder;",
        "Lcom/helpshift/conversation/activeconversation/message/FAQListMessageDM;",
        ">;"
    }
.end annotation


# direct methods
.method constructor <init>(Landroid/content/Context;)V
    .locals 0

    .line 27
    invoke-direct {p0, p1}, Lcom/helpshift/support/conversations/messages/MessageViewDataBinder;-><init>(Landroid/content/Context;)V

    return-void
.end method

.method private bindAdminMessage(Lcom/helpshift/support/conversations/messages/AdminSuggestionsMessageViewDataBinder$ViewHolder;Lcom/helpshift/conversation/activeconversation/message/MessageDM;)V
    .locals 3

    .line 114
    iget-object v0, p2, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->body:Ljava/lang/String;

    invoke-static {v0}, Lcom/helpshift/common/StringUtils;->isEmpty(Ljava/lang/String;)Z

    move-result v0

    if-eqz v0, :cond_0

    .line 115
    iget-object p1, p1, Lcom/helpshift/support/conversations/messages/AdminSuggestionsMessageViewDataBinder$ViewHolder;->messageContainer:Landroid/view/View;

    const/16 p2, 0x8

    invoke-virtual {p1, p2}, Landroid/view/View;->setVisibility(I)V

    goto :goto_1

    .line 118
    :cond_0
    iget-object v0, p1, Lcom/helpshift/support/conversations/messages/AdminSuggestionsMessageViewDataBinder$ViewHolder;->messageContainer:Landroid/view/View;

    const/4 v1, 0x0

    invoke-virtual {v0, v1}, Landroid/view/View;->setVisibility(I)V

    .line 119
    iget-object v0, p1, Lcom/helpshift/support/conversations/messages/AdminSuggestionsMessageViewDataBinder$ViewHolder;->messageText:Landroid/widget/TextView;

    iget-object v1, p2, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->body:Ljava/lang/String;

    invoke-virtual {p0, v1}, Lcom/helpshift/support/conversations/messages/AdminSuggestionsMessageViewDataBinder;->escapeHtml(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Landroid/widget/TextView;->setText(Ljava/lang/CharSequence;)V

    .line 120
    invoke-virtual {p2}, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->getUiViewState()Lcom/helpshift/conversation/activeconversation/message/UIViewState;

    move-result-object v0

    .line 123
    invoke-virtual {v0}, Lcom/helpshift/conversation/activeconversation/message/UIViewState;->isRoundedBackground()Z

    move-result v0

    if-eqz v0, :cond_1

    sget v0, Lcom/helpshift/R$drawable;->hs__chat_bubble_rounded:I

    goto :goto_0

    :cond_1
    sget v0, Lcom/helpshift/R$drawable;->hs__chat_bubble_admin:I

    .line 124
    :goto_0
    iget-object v1, p1, Lcom/helpshift/support/conversations/messages/AdminSuggestionsMessageViewDataBinder$ViewHolder;->messageContainer:Landroid/view/View;

    sget v2, Lcom/helpshift/R$attr;->hs__chatBubbleAdminBackgroundColor:I

    invoke-virtual {p0, v1, v0, v2}, Lcom/helpshift/support/conversations/messages/AdminSuggestionsMessageViewDataBinder;->setDrawable(Landroid/view/View;II)V

    .line 126
    iget-object v0, p1, Lcom/helpshift/support/conversations/messages/AdminSuggestionsMessageViewDataBinder$ViewHolder;->messageContainer:Landroid/view/View;

    invoke-virtual {p0, p2}, Lcom/helpshift/support/conversations/messages/AdminSuggestionsMessageViewDataBinder;->getAdminMessageContentDesciption(Lcom/helpshift/conversation/activeconversation/message/MessageDM;)Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Landroid/view/View;->setContentDescription(Ljava/lang/CharSequence;)V

    .line 127
    iget-object p1, p1, Lcom/helpshift/support/conversations/messages/AdminSuggestionsMessageViewDataBinder$ViewHolder;->messageText:Landroid/widget/TextView;

    new-instance v0, Lcom/helpshift/support/conversations/messages/AdminSuggestionsMessageViewDataBinder$2;

    invoke-direct {v0, p0, p2}, Lcom/helpshift/support/conversations/messages/AdminSuggestionsMessageViewDataBinder$2;-><init>(Lcom/helpshift/support/conversations/messages/AdminSuggestionsMessageViewDataBinder;Lcom/helpshift/conversation/activeconversation/message/MessageDM;)V

    invoke-virtual {p0, p1, v0}, Lcom/helpshift/support/conversations/messages/AdminSuggestionsMessageViewDataBinder;->linkify(Landroid/widget/TextView;Lcom/helpshift/util/HSLinkify$LinkClickListener;)V

    :goto_1
    return-void
.end method


# virtual methods
.method public bridge synthetic bind(Landroidx/recyclerview/widget/RecyclerView$ViewHolder;Lcom/helpshift/conversation/activeconversation/message/MessageDM;)V
    .locals 0

    .line 22
    check-cast p1, Lcom/helpshift/support/conversations/messages/AdminSuggestionsMessageViewDataBinder$ViewHolder;

    check-cast p2, Lcom/helpshift/conversation/activeconversation/message/FAQListMessageDM;

    invoke-virtual {p0, p1, p2}, Lcom/helpshift/support/conversations/messages/AdminSuggestionsMessageViewDataBinder;->bind(Lcom/helpshift/support/conversations/messages/AdminSuggestionsMessageViewDataBinder$ViewHolder;Lcom/helpshift/conversation/activeconversation/message/FAQListMessageDM;)V

    return-void
.end method

.method public bind(Lcom/helpshift/support/conversations/messages/AdminSuggestionsMessageViewDataBinder$ViewHolder;Lcom/helpshift/conversation/activeconversation/message/FAQListMessageDM;)V
    .locals 9

    .line 42
    invoke-direct {p0, p1, p2}, Lcom/helpshift/support/conversations/messages/AdminSuggestionsMessageViewDataBinder;->bindAdminMessage(Lcom/helpshift/support/conversations/messages/AdminSuggestionsMessageViewDataBinder$ViewHolder;Lcom/helpshift/conversation/activeconversation/message/MessageDM;)V

    .line 45
    iget-object v0, p1, Lcom/helpshift/support/conversations/messages/AdminSuggestionsMessageViewDataBinder$ViewHolder;->suggestionsList:Landroid/widget/TableLayout;

    invoke-virtual {v0}, Landroid/widget/TableLayout;->removeAllViews()V

    .line 51
    iget-object v0, p2, Lcom/helpshift/conversation/activeconversation/message/FAQListMessageDM;->faqs:Ljava/util/List;

    invoke-interface {v0}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v0

    const/4 v1, 0x0

    move-object v2, v1

    :goto_0
    invoke-interface {v0}, Ljava/util/Iterator;->hasNext()Z

    move-result v3

    if-eqz v3, :cond_1

    invoke-interface {v0}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Lcom/helpshift/conversation/activeconversation/message/FAQListMessageDM$FAQ;

    .line 53
    iget-object v3, p0, Lcom/helpshift/support/conversations/messages/AdminSuggestionsMessageViewDataBinder;->context:Landroid/content/Context;

    .line 54
    invoke-static {v3}, Landroid/view/LayoutInflater;->from(Landroid/content/Context;)Landroid/view/LayoutInflater;

    move-result-object v3

    sget v4, Lcom/helpshift/R$layout;->hs__msg_admin_suggesstion_item:I

    invoke-virtual {v3, v4, v1}, Landroid/view/LayoutInflater;->inflate(ILandroid/view/ViewGroup;)Landroid/view/View;

    move-result-object v3

    .line 55
    sget v4, Lcom/helpshift/R$id;->admin_suggestion_message:I

    invoke-virtual {v3, v4}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object v4

    check-cast v4, Landroid/widget/TextView;

    .line 56
    iget-object v5, v2, Lcom/helpshift/conversation/activeconversation/message/FAQListMessageDM$FAQ;->title:Ljava/lang/String;

    invoke-virtual {v4, v5}, Landroid/widget/TextView;->setText(Ljava/lang/CharSequence;)V

    .line 59
    sget v5, Landroid/os/Build$VERSION;->SDK_INT:I

    const/16 v6, 0x11

    if-lt v5, v6, :cond_0

    .line 60
    invoke-virtual {v4}, Landroid/widget/TextView;->getCompoundDrawablesRelative()[Landroid/graphics/drawable/Drawable;

    move-result-object v4

    goto :goto_1

    .line 63
    :cond_0
    invoke-virtual {v4}, Landroid/widget/TextView;->getCompoundDrawables()[Landroid/graphics/drawable/Drawable;

    move-result-object v4

    :goto_1
    const/4 v5, 0x2

    .line 66
    aget-object v4, v4, v5

    .line 67
    iget-object v5, p0, Lcom/helpshift/support/conversations/messages/AdminSuggestionsMessageViewDataBinder;->context:Landroid/content/Context;

    sget v6, Lcom/helpshift/R$attr;->hs__adminFaqSuggestionArrowColor:I

    invoke-static {v5, v4, v6}, Lcom/helpshift/util/Styles;->setColorFilter(Landroid/content/Context;Landroid/graphics/drawable/Drawable;I)V

    .line 73
    new-instance v4, Landroid/widget/TableRow;

    iget-object v5, p0, Lcom/helpshift/support/conversations/messages/AdminSuggestionsMessageViewDataBinder;->context:Landroid/content/Context;

    invoke-direct {v4, v5}, Landroid/widget/TableRow;-><init>(Landroid/content/Context;)V

    .line 74
    invoke-virtual {v4, v3}, Landroid/widget/TableRow;->addView(Landroid/view/View;)V

    .line 77
    iget-object v5, p0, Lcom/helpshift/support/conversations/messages/AdminSuggestionsMessageViewDataBinder;->context:Landroid/content/Context;

    invoke-static {v5}, Landroid/view/LayoutInflater;->from(Landroid/content/Context;)Landroid/view/LayoutInflater;

    move-result-object v5

    sget v6, Lcom/helpshift/R$layout;->hs__section_divider:I

    invoke-virtual {v5, v6, v1}, Landroid/view/LayoutInflater;->inflate(ILandroid/view/ViewGroup;)Landroid/view/View;

    move-result-object v5

    .line 78
    sget v6, Lcom/helpshift/R$id;->divider:I

    invoke-virtual {v5, v6}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object v6

    iget-object v7, p0, Lcom/helpshift/support/conversations/messages/AdminSuggestionsMessageViewDataBinder;->context:Landroid/content/Context;

    sget v8, Lcom/helpshift/R$attr;->hs__contentSeparatorColor:I

    .line 79
    invoke-static {v7, v8}, Lcom/helpshift/util/Styles;->getColor(Landroid/content/Context;I)I

    move-result v7

    invoke-virtual {v6, v7}, Landroid/view/View;->setBackgroundColor(I)V

    .line 80
    new-instance v6, Landroid/widget/TableRow;

    iget-object v7, p0, Lcom/helpshift/support/conversations/messages/AdminSuggestionsMessageViewDataBinder;->context:Landroid/content/Context;

    invoke-direct {v6, v7}, Landroid/widget/TableRow;-><init>(Landroid/content/Context;)V

    .line 81
    invoke-virtual {v6, v5}, Landroid/widget/TableRow;->addView(Landroid/view/View;)V

    .line 86
    iget-object v5, p1, Lcom/helpshift/support/conversations/messages/AdminSuggestionsMessageViewDataBinder$ViewHolder;->suggestionsList:Landroid/widget/TableLayout;

    invoke-virtual {v5, v4}, Landroid/widget/TableLayout;->addView(Landroid/view/View;)V

    .line 87
    iget-object v4, p1, Lcom/helpshift/support/conversations/messages/AdminSuggestionsMessageViewDataBinder$ViewHolder;->suggestionsList:Landroid/widget/TableLayout;

    invoke-virtual {v4, v6}, Landroid/widget/TableLayout;->addView(Landroid/view/View;)V

    .line 90
    new-instance v4, Lcom/helpshift/support/conversations/messages/AdminSuggestionsMessageViewDataBinder$1;

    invoke-direct {v4, p0, p2, v2}, Lcom/helpshift/support/conversations/messages/AdminSuggestionsMessageViewDataBinder$1;-><init>(Lcom/helpshift/support/conversations/messages/AdminSuggestionsMessageViewDataBinder;Lcom/helpshift/conversation/activeconversation/message/FAQListMessageDM;Lcom/helpshift/conversation/activeconversation/message/FAQListMessageDM$FAQ;)V

    invoke-virtual {v3, v4}, Landroid/view/View;->setOnClickListener(Landroid/view/View$OnClickListener;)V

    move-object v2, v6

    goto/16 :goto_0

    .line 100
    :cond_1
    iget-object v0, p1, Lcom/helpshift/support/conversations/messages/AdminSuggestionsMessageViewDataBinder$ViewHolder;->suggestionsList:Landroid/widget/TableLayout;

    invoke-virtual {v0, v2}, Landroid/widget/TableLayout;->removeView(Landroid/view/View;)V

    .line 103
    invoke-virtual {p2}, Lcom/helpshift/conversation/activeconversation/message/FAQListMessageDM;->getUiViewState()Lcom/helpshift/conversation/activeconversation/message/UIViewState;

    move-result-object v0

    .line 104
    iget-object v1, p1, Lcom/helpshift/support/conversations/messages/AdminSuggestionsMessageViewDataBinder$ViewHolder;->dateText:Landroid/widget/TextView;

    invoke-virtual {v0}, Lcom/helpshift/conversation/activeconversation/message/UIViewState;->isFooterVisible()Z

    move-result v2

    invoke-virtual {p0, v1, v2}, Lcom/helpshift/support/conversations/messages/AdminSuggestionsMessageViewDataBinder;->setViewVisibility(Landroid/view/View;Z)V

    .line 105
    invoke-virtual {v0}, Lcom/helpshift/conversation/activeconversation/message/UIViewState;->isFooterVisible()Z

    move-result v0

    if-eqz v0, :cond_2

    .line 106
    iget-object v0, p1, Lcom/helpshift/support/conversations/messages/AdminSuggestionsMessageViewDataBinder$ViewHolder;->dateText:Landroid/widget/TextView;

    invoke-virtual {p2}, Lcom/helpshift/conversation/activeconversation/message/FAQListMessageDM;->getSubText()Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Landroid/widget/TextView;->setText(Ljava/lang/CharSequence;)V

    .line 109
    :cond_2
    iget-object p1, p1, Lcom/helpshift/support/conversations/messages/AdminSuggestionsMessageViewDataBinder$ViewHolder;->messageLayout:Landroid/view/View;

    invoke-virtual {p0, p2}, Lcom/helpshift/support/conversations/messages/AdminSuggestionsMessageViewDataBinder;->getAdminMessageContentDesciption(Lcom/helpshift/conversation/activeconversation/message/MessageDM;)Ljava/lang/String;

    move-result-object p2

    invoke-virtual {p1, p2}, Landroid/view/View;->setContentDescription(Ljava/lang/CharSequence;)V

    return-void
.end method

.method public bridge synthetic createViewHolder(Landroid/view/ViewGroup;)Landroidx/recyclerview/widget/RecyclerView$ViewHolder;
    .locals 0

    .line 22
    invoke-virtual {p0, p1}, Lcom/helpshift/support/conversations/messages/AdminSuggestionsMessageViewDataBinder;->createViewHolder(Landroid/view/ViewGroup;)Lcom/helpshift/support/conversations/messages/AdminSuggestionsMessageViewDataBinder$ViewHolder;

    move-result-object p1

    return-object p1
.end method

.method public createViewHolder(Landroid/view/ViewGroup;)Lcom/helpshift/support/conversations/messages/AdminSuggestionsMessageViewDataBinder$ViewHolder;
    .locals 3

    .line 32
    iget-object v0, p0, Lcom/helpshift/support/conversations/messages/AdminSuggestionsMessageViewDataBinder;->context:Landroid/content/Context;

    invoke-static {v0}, Landroid/view/LayoutInflater;->from(Landroid/content/Context;)Landroid/view/LayoutInflater;

    move-result-object v0

    sget v1, Lcom/helpshift/R$layout;->hs__msg_admin_suggesstions_container:I

    const/4 v2, 0x0

    invoke-virtual {v0, v1, p1, v2}, Landroid/view/LayoutInflater;->inflate(ILandroid/view/ViewGroup;Z)Landroid/view/View;

    move-result-object p1

    .line 34
    new-instance v0, Lcom/helpshift/support/conversations/messages/AdminSuggestionsMessageViewDataBinder$ViewHolder;

    invoke-direct {v0, p0, p1}, Lcom/helpshift/support/conversations/messages/AdminSuggestionsMessageViewDataBinder$ViewHolder;-><init>(Lcom/helpshift/support/conversations/messages/AdminSuggestionsMessageViewDataBinder;Landroid/view/View;)V

    return-object v0
.end method
