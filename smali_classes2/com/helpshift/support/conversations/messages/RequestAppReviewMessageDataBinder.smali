.class public Lcom/helpshift/support/conversations/messages/RequestAppReviewMessageDataBinder;
.super Lcom/helpshift/support/conversations/messages/MessageViewDataBinder;
.source "RequestAppReviewMessageDataBinder.java"


# annotations
.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Lcom/helpshift/support/conversations/messages/RequestAppReviewMessageDataBinder$ViewHolder;
    }
.end annotation

.annotation system Ldalvik/annotation/Signature;
    value = {
        "Lcom/helpshift/support/conversations/messages/MessageViewDataBinder<",
        "Lcom/helpshift/support/conversations/messages/RequestAppReviewMessageDataBinder$ViewHolder;",
        "Lcom/helpshift/conversation/activeconversation/message/RequestAppReviewMessageDM;",
        ">;"
    }
.end annotation


# direct methods
.method public constructor <init>(Landroid/content/Context;)V
    .locals 0

    .line 18
    invoke-direct {p0, p1}, Lcom/helpshift/support/conversations/messages/MessageViewDataBinder;-><init>(Landroid/content/Context;)V

    return-void
.end method


# virtual methods
.method public bridge synthetic bind(Landroidx/recyclerview/widget/RecyclerView$ViewHolder;Lcom/helpshift/conversation/activeconversation/message/MessageDM;)V
    .locals 0

    .line 14
    check-cast p1, Lcom/helpshift/support/conversations/messages/RequestAppReviewMessageDataBinder$ViewHolder;

    check-cast p2, Lcom/helpshift/conversation/activeconversation/message/RequestAppReviewMessageDM;

    invoke-virtual {p0, p1, p2}, Lcom/helpshift/support/conversations/messages/RequestAppReviewMessageDataBinder;->bind(Lcom/helpshift/support/conversations/messages/RequestAppReviewMessageDataBinder$ViewHolder;Lcom/helpshift/conversation/activeconversation/message/RequestAppReviewMessageDM;)V

    return-void
.end method

.method public bind(Lcom/helpshift/support/conversations/messages/RequestAppReviewMessageDataBinder$ViewHolder;Lcom/helpshift/conversation/activeconversation/message/RequestAppReviewMessageDM;)V
    .locals 4

    .line 31
    iget-object v0, p1, Lcom/helpshift/support/conversations/messages/RequestAppReviewMessageDataBinder$ViewHolder;->message:Landroid/widget/TextView;

    sget v1, Lcom/helpshift/R$string;->hs__review_request_message:I

    invoke-virtual {v0, v1}, Landroid/widget/TextView;->setText(I)V

    .line 32
    iget-boolean v0, p2, Lcom/helpshift/conversation/activeconversation/message/RequestAppReviewMessageDM;->isAnswered:Z

    if-eqz v0, :cond_0

    .line 33
    iget-object v0, p1, Lcom/helpshift/support/conversations/messages/RequestAppReviewMessageDataBinder$ViewHolder;->reviewButton:Landroid/widget/Button;

    const/16 v1, 0x8

    invoke-virtual {v0, v1}, Landroid/widget/Button;->setVisibility(I)V

    goto :goto_0

    .line 36
    :cond_0
    iget-object v0, p1, Lcom/helpshift/support/conversations/messages/RequestAppReviewMessageDataBinder$ViewHolder;->reviewButton:Landroid/widget/Button;

    const/4 v1, 0x0

    invoke-virtual {v0, v1}, Landroid/widget/Button;->setVisibility(I)V

    .line 38
    :goto_0
    invoke-virtual {p2}, Lcom/helpshift/conversation/activeconversation/message/RequestAppReviewMessageDM;->getUiViewState()Lcom/helpshift/conversation/activeconversation/message/UIViewState;

    move-result-object v0

    .line 40
    invoke-virtual {v0}, Lcom/helpshift/conversation/activeconversation/message/UIViewState;->isRoundedBackground()Z

    move-result v1

    if-eqz v1, :cond_1

    sget v1, Lcom/helpshift/R$drawable;->hs__chat_bubble_rounded:I

    goto :goto_1

    :cond_1
    sget v1, Lcom/helpshift/R$drawable;->hs__chat_bubble_admin:I

    .line 41
    :goto_1
    iget-object v2, p1, Lcom/helpshift/support/conversations/messages/RequestAppReviewMessageDataBinder$ViewHolder;->messageContainer:Landroid/view/View;

    sget v3, Lcom/helpshift/R$attr;->hs__chatBubbleAdminBackgroundColor:I

    invoke-virtual {p0, v2, v1, v3}, Lcom/helpshift/support/conversations/messages/RequestAppReviewMessageDataBinder;->setDrawable(Landroid/view/View;II)V

    .line 42
    invoke-virtual {v0}, Lcom/helpshift/conversation/activeconversation/message/UIViewState;->isFooterVisible()Z

    move-result v1

    if-eqz v1, :cond_2

    .line 43
    iget-object v1, p1, Lcom/helpshift/support/conversations/messages/RequestAppReviewMessageDataBinder$ViewHolder;->subText:Landroid/widget/TextView;

    invoke-virtual {p2}, Lcom/helpshift/conversation/activeconversation/message/RequestAppReviewMessageDM;->getSubText()Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v1, v2}, Landroid/widget/TextView;->setText(Ljava/lang/CharSequence;)V

    .line 45
    :cond_2
    iget-object v1, p1, Lcom/helpshift/support/conversations/messages/RequestAppReviewMessageDataBinder$ViewHolder;->subText:Landroid/widget/TextView;

    invoke-virtual {v0}, Lcom/helpshift/conversation/activeconversation/message/UIViewState;->isFooterVisible()Z

    move-result v0

    invoke-virtual {p0, v1, v0}, Lcom/helpshift/support/conversations/messages/RequestAppReviewMessageDataBinder;->setViewVisibility(Landroid/view/View;Z)V

    .line 46
    iget-boolean v0, p2, Lcom/helpshift/conversation/activeconversation/message/RequestAppReviewMessageDM;->isReviewButtonClickable:Z

    if-eqz v0, :cond_3

    .line 47
    iget-object v0, p1, Lcom/helpshift/support/conversations/messages/RequestAppReviewMessageDataBinder$ViewHolder;->reviewButton:Landroid/widget/Button;

    new-instance v1, Lcom/helpshift/support/conversations/messages/RequestAppReviewMessageDataBinder$1;

    invoke-direct {v1, p0, p2}, Lcom/helpshift/support/conversations/messages/RequestAppReviewMessageDataBinder$1;-><init>(Lcom/helpshift/support/conversations/messages/RequestAppReviewMessageDataBinder;Lcom/helpshift/conversation/activeconversation/message/RequestAppReviewMessageDM;)V

    invoke-virtual {v0, v1}, Landroid/widget/Button;->setOnClickListener(Landroid/view/View$OnClickListener;)V

    goto :goto_2

    .line 57
    :cond_3
    iget-object v0, p1, Lcom/helpshift/support/conversations/messages/RequestAppReviewMessageDataBinder$ViewHolder;->reviewButton:Landroid/widget/Button;

    const/4 v1, 0x0

    invoke-virtual {v0, v1}, Landroid/widget/Button;->setOnClickListener(Landroid/view/View$OnClickListener;)V

    .line 59
    :goto_2
    iget-object p1, p1, Lcom/helpshift/support/conversations/messages/RequestAppReviewMessageDataBinder$ViewHolder;->messageLayout:Landroid/view/View;

    invoke-virtual {p0, p2}, Lcom/helpshift/support/conversations/messages/RequestAppReviewMessageDataBinder;->getAdminMessageContentDesciption(Lcom/helpshift/conversation/activeconversation/message/MessageDM;)Ljava/lang/String;

    move-result-object p2

    invoke-virtual {p1, p2}, Landroid/view/View;->setContentDescription(Ljava/lang/CharSequence;)V

    return-void
.end method

.method public bridge synthetic createViewHolder(Landroid/view/ViewGroup;)Landroidx/recyclerview/widget/RecyclerView$ViewHolder;
    .locals 0

    .line 14
    invoke-virtual {p0, p1}, Lcom/helpshift/support/conversations/messages/RequestAppReviewMessageDataBinder;->createViewHolder(Landroid/view/ViewGroup;)Lcom/helpshift/support/conversations/messages/RequestAppReviewMessageDataBinder$ViewHolder;

    move-result-object p1

    return-object p1
.end method

.method public createViewHolder(Landroid/view/ViewGroup;)Lcom/helpshift/support/conversations/messages/RequestAppReviewMessageDataBinder$ViewHolder;
    .locals 3

    .line 23
    invoke-virtual {p1}, Landroid/view/ViewGroup;->getContext()Landroid/content/Context;

    move-result-object v0

    invoke-static {v0}, Landroid/view/LayoutInflater;->from(Landroid/content/Context;)Landroid/view/LayoutInflater;

    move-result-object v0

    sget v1, Lcom/helpshift/R$layout;->hs__msg_review_request:I

    const/4 v2, 0x0

    invoke-virtual {v0, v1, p1, v2}, Landroid/view/LayoutInflater;->inflate(ILandroid/view/ViewGroup;Z)Landroid/view/View;

    move-result-object p1

    .line 25
    new-instance v0, Lcom/helpshift/support/conversations/messages/RequestAppReviewMessageDataBinder$ViewHolder;

    invoke-direct {v0, p0, p1}, Lcom/helpshift/support/conversations/messages/RequestAppReviewMessageDataBinder$ViewHolder;-><init>(Lcom/helpshift/support/conversations/messages/RequestAppReviewMessageDataBinder;Landroid/view/View;)V

    return-object v0
.end method
