.class public Lcom/helpshift/support/conversations/messages/UserRedactedMessageDataBinder;
.super Lcom/helpshift/support/conversations/messages/MessageViewDataBinder;
.source "UserRedactedMessageDataBinder.java"


# annotations
.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Lcom/helpshift/support/conversations/messages/UserRedactedMessageDataBinder$ViewHolder;
    }
.end annotation

.annotation system Ldalvik/annotation/Signature;
    value = {
        "Lcom/helpshift/support/conversations/messages/MessageViewDataBinder<",
        "Lcom/helpshift/support/conversations/messages/UserRedactedMessageDataBinder$ViewHolder;",
        "Lcom/helpshift/conversation/activeconversation/message/MessageDM;",
        ">;"
    }
.end annotation


# direct methods
.method public constructor <init>(Landroid/content/Context;)V
    .locals 0

    .line 23
    invoke-direct {p0, p1}, Lcom/helpshift/support/conversations/messages/MessageViewDataBinder;-><init>(Landroid/content/Context;)V

    return-void
.end method


# virtual methods
.method public bridge synthetic bind(Landroidx/recyclerview/widget/RecyclerView$ViewHolder;Lcom/helpshift/conversation/activeconversation/message/MessageDM;)V
    .locals 0

    .line 19
    check-cast p1, Lcom/helpshift/support/conversations/messages/UserRedactedMessageDataBinder$ViewHolder;

    invoke-virtual {p0, p1, p2}, Lcom/helpshift/support/conversations/messages/UserRedactedMessageDataBinder;->bind(Lcom/helpshift/support/conversations/messages/UserRedactedMessageDataBinder$ViewHolder;Lcom/helpshift/conversation/activeconversation/message/MessageDM;)V

    return-void
.end method

.method public bind(Lcom/helpshift/support/conversations/messages/UserRedactedMessageDataBinder$ViewHolder;Lcom/helpshift/conversation/activeconversation/message/MessageDM;)V
    .locals 5

    .line 41
    iget-object v0, p1, Lcom/helpshift/support/conversations/messages/UserRedactedMessageDataBinder$ViewHolder;->messageText:Landroid/widget/TextView;

    iget-object v1, p2, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->body:Ljava/lang/String;

    invoke-virtual {p0, v1}, Lcom/helpshift/support/conversations/messages/UserRedactedMessageDataBinder;->escapeHtml(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v1

    invoke-virtual {p0, v1}, Lcom/helpshift/support/conversations/messages/UserRedactedMessageDataBinder;->getRedactedBodyText(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Landroid/widget/TextView;->setText(Ljava/lang/CharSequence;)V

    .line 42
    iget-object v0, p1, Lcom/helpshift/support/conversations/messages/UserRedactedMessageDataBinder$ViewHolder;->messageText:Landroid/widget/TextView;

    invoke-virtual {p0, v0}, Lcom/helpshift/support/conversations/messages/UserRedactedMessageDataBinder;->applyRedactionStyle(Landroid/widget/TextView;)V

    .line 43
    iget-object v0, p0, Lcom/helpshift/support/conversations/messages/UserRedactedMessageDataBinder;->context:Landroid/content/Context;

    sget v1, Lcom/helpshift/R$string;->hs__user_sent_message_voice_over:I

    const/4 v2, 0x1

    new-array v2, v2, [Ljava/lang/Object;

    .line 44
    invoke-virtual {p2}, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->getAccessbilityMessageTime()Ljava/lang/String;

    move-result-object v3

    const/4 v4, 0x0

    aput-object v3, v2, v4

    invoke-virtual {v0, v1, v2}, Landroid/content/Context;->getString(I[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v0

    .line 45
    iget-object v1, p1, Lcom/helpshift/support/conversations/messages/UserRedactedMessageDataBinder$ViewHolder;->messageLayout:Landroid/view/View;

    invoke-virtual {v1, v0}, Landroid/view/View;->setContentDescription(Ljava/lang/CharSequence;)V

    .line 46
    iget-object v0, p1, Lcom/helpshift/support/conversations/messages/UserRedactedMessageDataBinder$ViewHolder;->messageText:Landroid/widget/TextView;

    const/4 v1, 0x0

    invoke-virtual {p0, v0, v1}, Lcom/helpshift/support/conversations/messages/UserRedactedMessageDataBinder;->linkify(Landroid/widget/TextView;Lcom/helpshift/util/HSLinkify$LinkClickListener;)V

    .line 47
    invoke-virtual {p2}, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->getUiViewState()Lcom/helpshift/conversation/activeconversation/message/UIViewState;

    move-result-object v0

    .line 48
    iget-object v1, p1, Lcom/helpshift/support/conversations/messages/UserRedactedMessageDataBinder$ViewHolder;->messageBubble:Landroid/widget/FrameLayout;

    invoke-virtual {p0, v1, v0}, Lcom/helpshift/support/conversations/messages/UserRedactedMessageDataBinder;->setUserMessageContainerBackground(Landroid/view/View;Lcom/helpshift/conversation/activeconversation/message/UIViewState;)V

    .line 49
    iget-object p1, p1, Lcom/helpshift/support/conversations/messages/UserRedactedMessageDataBinder$ViewHolder;->subText:Landroid/widget/TextView;

    invoke-virtual {p2}, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->getSubText()Ljava/lang/String;

    move-result-object p2

    invoke-virtual {p0, p1, v0, p2}, Lcom/helpshift/support/conversations/messages/UserRedactedMessageDataBinder;->setUserMessageSubText(Landroid/widget/TextView;Lcom/helpshift/conversation/activeconversation/message/UIViewState;Ljava/lang/String;)V

    return-void
.end method

.method public bridge synthetic createViewHolder(Landroid/view/ViewGroup;)Landroidx/recyclerview/widget/RecyclerView$ViewHolder;
    .locals 0

    .line 19
    invoke-virtual {p0, p1}, Lcom/helpshift/support/conversations/messages/UserRedactedMessageDataBinder;->createViewHolder(Landroid/view/ViewGroup;)Lcom/helpshift/support/conversations/messages/UserRedactedMessageDataBinder$ViewHolder;

    move-result-object p1

    return-object p1
.end method

.method public createViewHolder(Landroid/view/ViewGroup;)Lcom/helpshift/support/conversations/messages/UserRedactedMessageDataBinder$ViewHolder;
    .locals 3

    .line 28
    invoke-virtual {p1}, Landroid/view/ViewGroup;->getContext()Landroid/content/Context;

    move-result-object v0

    invoke-static {v0}, Landroid/view/LayoutInflater;->from(Landroid/content/Context;)Landroid/view/LayoutInflater;

    move-result-object v0

    sget v1, Lcom/helpshift/R$layout;->hs__msg_txt_user:I

    const/4 v2, 0x0

    invoke-virtual {v0, v1, p1, v2}, Landroid/view/LayoutInflater;->inflate(ILandroid/view/ViewGroup;Z)Landroid/view/View;

    move-result-object p1

    .line 31
    new-instance v0, Lcom/helpshift/support/conversations/messages/UserRedactedMessageDataBinder$ViewHolder;

    invoke-direct {v0, p0, p1}, Lcom/helpshift/support/conversations/messages/UserRedactedMessageDataBinder$ViewHolder;-><init>(Lcom/helpshift/support/conversations/messages/UserRedactedMessageDataBinder;Landroid/view/View;)V

    .line 34
    iget-object p1, v0, Lcom/helpshift/support/conversations/messages/UserRedactedMessageDataBinder$ViewHolder;->messageBubble:Landroid/widget/FrameLayout;

    invoke-virtual {p1}, Landroid/widget/FrameLayout;->getLayoutParams()Landroid/view/ViewGroup$LayoutParams;

    move-result-object p1

    invoke-virtual {p0, p1}, Lcom/helpshift/support/conversations/messages/UserRedactedMessageDataBinder;->setUserMessageLayoutMargin(Landroid/view/ViewGroup$LayoutParams;)V

    .line 35
    invoke-virtual {v0}, Lcom/helpshift/support/conversations/messages/UserRedactedMessageDataBinder$ViewHolder;->setListeners()V

    return-object v0
.end method
