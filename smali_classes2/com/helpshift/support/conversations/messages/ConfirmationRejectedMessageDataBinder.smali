.class Lcom/helpshift/support/conversations/messages/ConfirmationRejectedMessageDataBinder;
.super Lcom/helpshift/support/conversations/messages/MessageViewDataBinder;
.source "ConfirmationRejectedMessageDataBinder.java"


# annotations
.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Lcom/helpshift/support/conversations/messages/ConfirmationRejectedMessageDataBinder$ViewHolder;
    }
.end annotation

.annotation system Ldalvik/annotation/Signature;
    value = {
        "Lcom/helpshift/support/conversations/messages/MessageViewDataBinder<",
        "Lcom/helpshift/support/conversations/messages/ConfirmationRejectedMessageDataBinder$ViewHolder;",
        "Lcom/helpshift/conversation/activeconversation/message/ConfirmationRejectedMessageDM;",
        ">;"
    }
.end annotation


# direct methods
.method constructor <init>(Landroid/content/Context;)V
    .locals 0

    .line 17
    invoke-direct {p0, p1}, Lcom/helpshift/support/conversations/messages/MessageViewDataBinder;-><init>(Landroid/content/Context;)V

    return-void
.end method


# virtual methods
.method public bridge synthetic bind(Landroidx/recyclerview/widget/RecyclerView$ViewHolder;Lcom/helpshift/conversation/activeconversation/message/MessageDM;)V
    .locals 0

    .line 13
    check-cast p1, Lcom/helpshift/support/conversations/messages/ConfirmationRejectedMessageDataBinder$ViewHolder;

    check-cast p2, Lcom/helpshift/conversation/activeconversation/message/ConfirmationRejectedMessageDM;

    invoke-virtual {p0, p1, p2}, Lcom/helpshift/support/conversations/messages/ConfirmationRejectedMessageDataBinder;->bind(Lcom/helpshift/support/conversations/messages/ConfirmationRejectedMessageDataBinder$ViewHolder;Lcom/helpshift/conversation/activeconversation/message/ConfirmationRejectedMessageDM;)V

    return-void
.end method

.method public bind(Lcom/helpshift/support/conversations/messages/ConfirmationRejectedMessageDataBinder$ViewHolder;Lcom/helpshift/conversation/activeconversation/message/ConfirmationRejectedMessageDM;)V
    .locals 4

    .line 29
    iget-object v0, p1, Lcom/helpshift/support/conversations/messages/ConfirmationRejectedMessageDataBinder$ViewHolder;->message:Landroid/widget/TextView;

    sget v1, Lcom/helpshift/R$string;->hs__cr_msg:I

    invoke-virtual {v0, v1}, Landroid/widget/TextView;->setText(I)V

    .line 30
    invoke-virtual {p2}, Lcom/helpshift/conversation/activeconversation/message/ConfirmationRejectedMessageDM;->getUiViewState()Lcom/helpshift/conversation/activeconversation/message/UIViewState;

    move-result-object v0

    .line 32
    invoke-virtual {v0}, Lcom/helpshift/conversation/activeconversation/message/UIViewState;->isRoundedBackground()Z

    move-result v1

    if-eqz v1, :cond_0

    sget v1, Lcom/helpshift/R$drawable;->hs__chat_bubble_rounded:I

    goto :goto_0

    :cond_0
    sget v1, Lcom/helpshift/R$drawable;->hs__chat_bubble_admin:I

    .line 33
    :goto_0
    iget-object v2, p1, Lcom/helpshift/support/conversations/messages/ConfirmationRejectedMessageDataBinder$ViewHolder;->messageContainer:Landroid/view/View;

    sget v3, Lcom/helpshift/R$attr;->hs__chatBubbleAdminBackgroundColor:I

    invoke-virtual {p0, v2, v1, v3}, Lcom/helpshift/support/conversations/messages/ConfirmationRejectedMessageDataBinder;->setDrawable(Landroid/view/View;II)V

    .line 34
    invoke-virtual {v0}, Lcom/helpshift/conversation/activeconversation/message/UIViewState;->isFooterVisible()Z

    move-result v1

    if-eqz v1, :cond_1

    .line 35
    iget-object v1, p1, Lcom/helpshift/support/conversations/messages/ConfirmationRejectedMessageDataBinder$ViewHolder;->subText:Landroid/widget/TextView;

    invoke-virtual {p2}, Lcom/helpshift/conversation/activeconversation/message/ConfirmationRejectedMessageDM;->getSubText()Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v1, v2}, Landroid/widget/TextView;->setText(Ljava/lang/CharSequence;)V

    .line 37
    :cond_1
    iget-object v1, p1, Lcom/helpshift/support/conversations/messages/ConfirmationRejectedMessageDataBinder$ViewHolder;->messageLayout:Landroid/view/View;

    invoke-virtual {p0, p2}, Lcom/helpshift/support/conversations/messages/ConfirmationRejectedMessageDataBinder;->getAdminMessageContentDesciption(Lcom/helpshift/conversation/activeconversation/message/MessageDM;)Ljava/lang/String;

    move-result-object p2

    invoke-virtual {v1, p2}, Landroid/view/View;->setContentDescription(Ljava/lang/CharSequence;)V

    .line 38
    iget-object p1, p1, Lcom/helpshift/support/conversations/messages/ConfirmationRejectedMessageDataBinder$ViewHolder;->subText:Landroid/widget/TextView;

    invoke-virtual {v0}, Lcom/helpshift/conversation/activeconversation/message/UIViewState;->isFooterVisible()Z

    move-result p2

    invoke-virtual {p0, p1, p2}, Lcom/helpshift/support/conversations/messages/ConfirmationRejectedMessageDataBinder;->setViewVisibility(Landroid/view/View;Z)V

    return-void
.end method

.method public bridge synthetic createViewHolder(Landroid/view/ViewGroup;)Landroidx/recyclerview/widget/RecyclerView$ViewHolder;
    .locals 0

    .line 13
    invoke-virtual {p0, p1}, Lcom/helpshift/support/conversations/messages/ConfirmationRejectedMessageDataBinder;->createViewHolder(Landroid/view/ViewGroup;)Lcom/helpshift/support/conversations/messages/ConfirmationRejectedMessageDataBinder$ViewHolder;

    move-result-object p1

    return-object p1
.end method

.method public createViewHolder(Landroid/view/ViewGroup;)Lcom/helpshift/support/conversations/messages/ConfirmationRejectedMessageDataBinder$ViewHolder;
    .locals 3

    .line 22
    invoke-virtual {p1}, Landroid/view/ViewGroup;->getContext()Landroid/content/Context;

    move-result-object v0

    invoke-static {v0}, Landroid/view/LayoutInflater;->from(Landroid/content/Context;)Landroid/view/LayoutInflater;

    move-result-object v0

    sget v1, Lcom/helpshift/R$layout;->hs__msg_txt_admin:I

    const/4 v2, 0x0

    invoke-virtual {v0, v1, p1, v2}, Landroid/view/LayoutInflater;->inflate(ILandroid/view/ViewGroup;Z)Landroid/view/View;

    move-result-object p1

    .line 24
    new-instance v0, Lcom/helpshift/support/conversations/messages/ConfirmationRejectedMessageDataBinder$ViewHolder;

    invoke-direct {v0, p0, p1}, Lcom/helpshift/support/conversations/messages/ConfirmationRejectedMessageDataBinder$ViewHolder;-><init>(Lcom/helpshift/support/conversations/messages/ConfirmationRejectedMessageDataBinder;Landroid/view/View;)V

    return-object v0
.end method
