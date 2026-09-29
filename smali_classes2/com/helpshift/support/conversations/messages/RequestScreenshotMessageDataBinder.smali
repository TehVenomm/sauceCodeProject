.class public Lcom/helpshift/support/conversations/messages/RequestScreenshotMessageDataBinder;
.super Lcom/helpshift/support/conversations/messages/MessageViewDataBinder;
.source "RequestScreenshotMessageDataBinder.java"


# annotations
.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Lcom/helpshift/support/conversations/messages/RequestScreenshotMessageDataBinder$ViewHolder;
    }
.end annotation

.annotation system Ldalvik/annotation/Signature;
    value = {
        "Lcom/helpshift/support/conversations/messages/MessageViewDataBinder<",
        "Lcom/helpshift/support/conversations/messages/RequestScreenshotMessageDataBinder$ViewHolder;",
        "Lcom/helpshift/conversation/activeconversation/message/RequestScreenshotMessageDM;",
        ">;"
    }
.end annotation


# direct methods
.method public constructor <init>(Landroid/content/Context;)V
    .locals 0

    .line 21
    invoke-direct {p0, p1}, Lcom/helpshift/support/conversations/messages/MessageViewDataBinder;-><init>(Landroid/content/Context;)V

    return-void
.end method


# virtual methods
.method public bridge synthetic bind(Landroidx/recyclerview/widget/RecyclerView$ViewHolder;Lcom/helpshift/conversation/activeconversation/message/MessageDM;)V
    .locals 0

    .line 17
    check-cast p1, Lcom/helpshift/support/conversations/messages/RequestScreenshotMessageDataBinder$ViewHolder;

    check-cast p2, Lcom/helpshift/conversation/activeconversation/message/RequestScreenshotMessageDM;

    invoke-virtual {p0, p1, p2}, Lcom/helpshift/support/conversations/messages/RequestScreenshotMessageDataBinder;->bind(Lcom/helpshift/support/conversations/messages/RequestScreenshotMessageDataBinder$ViewHolder;Lcom/helpshift/conversation/activeconversation/message/RequestScreenshotMessageDM;)V

    return-void
.end method

.method public bind(Lcom/helpshift/support/conversations/messages/RequestScreenshotMessageDataBinder$ViewHolder;Lcom/helpshift/conversation/activeconversation/message/RequestScreenshotMessageDM;)V
    .locals 4

    .line 33
    iget-object v0, p1, Lcom/helpshift/support/conversations/messages/RequestScreenshotMessageDataBinder$ViewHolder;->adminRequestText:Landroid/widget/TextView;

    iget-object v1, p2, Lcom/helpshift/conversation/activeconversation/message/RequestScreenshotMessageDM;->body:Ljava/lang/String;

    invoke-virtual {p0, v1}, Lcom/helpshift/support/conversations/messages/RequestScreenshotMessageDataBinder;->escapeHtml(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Landroid/widget/TextView;->setText(Ljava/lang/CharSequence;)V

    .line 35
    iget-object v0, p1, Lcom/helpshift/support/conversations/messages/RequestScreenshotMessageDataBinder$ViewHolder;->attachButton:Landroid/widget/Button;

    iget-boolean v1, p2, Lcom/helpshift/conversation/activeconversation/message/RequestScreenshotMessageDM;->isAnswered:Z

    xor-int/lit8 v1, v1, 0x1

    invoke-virtual {p0, v0, v1}, Lcom/helpshift/support/conversations/messages/RequestScreenshotMessageDataBinder;->setViewVisibility(Landroid/view/View;Z)V

    .line 36
    invoke-virtual {p2}, Lcom/helpshift/conversation/activeconversation/message/RequestScreenshotMessageDM;->getUiViewState()Lcom/helpshift/conversation/activeconversation/message/UIViewState;

    move-result-object v0

    .line 38
    invoke-virtual {v0}, Lcom/helpshift/conversation/activeconversation/message/UIViewState;->isRoundedBackground()Z

    move-result v1

    if-eqz v1, :cond_0

    sget v1, Lcom/helpshift/R$drawable;->hs__chat_bubble_rounded:I

    goto :goto_0

    :cond_0
    sget v1, Lcom/helpshift/R$drawable;->hs__chat_bubble_admin:I

    .line 39
    :goto_0
    invoke-static {p1}, Lcom/helpshift/support/conversations/messages/RequestScreenshotMessageDataBinder$ViewHolder;->access$000(Lcom/helpshift/support/conversations/messages/RequestScreenshotMessageDataBinder$ViewHolder;)Landroid/widget/LinearLayout;

    move-result-object v2

    sget v3, Lcom/helpshift/R$attr;->hs__chatBubbleAdminBackgroundColor:I

    invoke-virtual {p0, v2, v1, v3}, Lcom/helpshift/support/conversations/messages/RequestScreenshotMessageDataBinder;->setDrawable(Landroid/view/View;II)V

    .line 41
    invoke-virtual {v0}, Lcom/helpshift/conversation/activeconversation/message/UIViewState;->isFooterVisible()Z

    move-result v1

    if-eqz v1, :cond_1

    .line 42
    iget-object v1, p1, Lcom/helpshift/support/conversations/messages/RequestScreenshotMessageDataBinder$ViewHolder;->subText:Landroid/widget/TextView;

    invoke-virtual {p2}, Lcom/helpshift/conversation/activeconversation/message/RequestScreenshotMessageDM;->getSubText()Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v1, v2}, Landroid/widget/TextView;->setText(Ljava/lang/CharSequence;)V

    .line 44
    :cond_1
    iget-object v1, p1, Lcom/helpshift/support/conversations/messages/RequestScreenshotMessageDataBinder$ViewHolder;->subText:Landroid/widget/TextView;

    invoke-virtual {v0}, Lcom/helpshift/conversation/activeconversation/message/UIViewState;->isFooterVisible()Z

    move-result v0

    invoke-virtual {p0, v1, v0}, Lcom/helpshift/support/conversations/messages/RequestScreenshotMessageDataBinder;->setViewVisibility(Landroid/view/View;Z)V

    .line 45
    iget-object v0, p1, Lcom/helpshift/support/conversations/messages/RequestScreenshotMessageDataBinder$ViewHolder;->attachButton:Landroid/widget/Button;

    new-instance v1, Lcom/helpshift/support/conversations/messages/RequestScreenshotMessageDataBinder$1;

    invoke-direct {v1, p0, p2}, Lcom/helpshift/support/conversations/messages/RequestScreenshotMessageDataBinder$1;-><init>(Lcom/helpshift/support/conversations/messages/RequestScreenshotMessageDataBinder;Lcom/helpshift/conversation/activeconversation/message/RequestScreenshotMessageDM;)V

    invoke-virtual {v0, v1}, Landroid/widget/Button;->setOnClickListener(Landroid/view/View$OnClickListener;)V

    .line 54
    iget-object v0, p1, Lcom/helpshift/support/conversations/messages/RequestScreenshotMessageDataBinder$ViewHolder;->messageLayout:Landroid/view/View;

    invoke-virtual {p0, p2}, Lcom/helpshift/support/conversations/messages/RequestScreenshotMessageDataBinder;->getAdminMessageContentDesciption(Lcom/helpshift/conversation/activeconversation/message/MessageDM;)Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Landroid/view/View;->setContentDescription(Ljava/lang/CharSequence;)V

    .line 56
    iget-object p1, p1, Lcom/helpshift/support/conversations/messages/RequestScreenshotMessageDataBinder$ViewHolder;->adminRequestText:Landroid/widget/TextView;

    new-instance v0, Lcom/helpshift/support/conversations/messages/RequestScreenshotMessageDataBinder$2;

    invoke-direct {v0, p0, p2}, Lcom/helpshift/support/conversations/messages/RequestScreenshotMessageDataBinder$2;-><init>(Lcom/helpshift/support/conversations/messages/RequestScreenshotMessageDataBinder;Lcom/helpshift/conversation/activeconversation/message/RequestScreenshotMessageDM;)V

    invoke-virtual {p0, p1, v0}, Lcom/helpshift/support/conversations/messages/RequestScreenshotMessageDataBinder;->linkify(Landroid/widget/TextView;Lcom/helpshift/util/HSLinkify$LinkClickListener;)V

    return-void
.end method

.method public bridge synthetic createViewHolder(Landroid/view/ViewGroup;)Landroidx/recyclerview/widget/RecyclerView$ViewHolder;
    .locals 0

    .line 17
    invoke-virtual {p0, p1}, Lcom/helpshift/support/conversations/messages/RequestScreenshotMessageDataBinder;->createViewHolder(Landroid/view/ViewGroup;)Lcom/helpshift/support/conversations/messages/RequestScreenshotMessageDataBinder$ViewHolder;

    move-result-object p1

    return-object p1
.end method

.method public createViewHolder(Landroid/view/ViewGroup;)Lcom/helpshift/support/conversations/messages/RequestScreenshotMessageDataBinder$ViewHolder;
    .locals 3

    .line 26
    invoke-virtual {p1}, Landroid/view/ViewGroup;->getContext()Landroid/content/Context;

    move-result-object v0

    invoke-static {v0}, Landroid/view/LayoutInflater;->from(Landroid/content/Context;)Landroid/view/LayoutInflater;

    move-result-object v0

    sget v1, Lcom/helpshift/R$layout;->hs__msg_request_screenshot:I

    const/4 v2, 0x0

    invoke-virtual {v0, v1, p1, v2}, Landroid/view/LayoutInflater;->inflate(ILandroid/view/ViewGroup;Z)Landroid/view/View;

    move-result-object p1

    .line 28
    new-instance v0, Lcom/helpshift/support/conversations/messages/RequestScreenshotMessageDataBinder$ViewHolder;

    invoke-direct {v0, p0, p1}, Lcom/helpshift/support/conversations/messages/RequestScreenshotMessageDataBinder$ViewHolder;-><init>(Lcom/helpshift/support/conversations/messages/RequestScreenshotMessageDataBinder;Landroid/view/View;)V

    return-object v0
.end method
