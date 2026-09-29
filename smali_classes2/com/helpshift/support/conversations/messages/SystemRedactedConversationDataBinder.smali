.class public Lcom/helpshift/support/conversations/messages/SystemRedactedConversationDataBinder;
.super Lcom/helpshift/support/conversations/messages/MessageViewDataBinder;
.source "SystemRedactedConversationDataBinder.java"


# annotations
.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Lcom/helpshift/support/conversations/messages/SystemRedactedConversationDataBinder$ViewHolder;
    }
.end annotation

.annotation system Ldalvik/annotation/Signature;
    value = {
        "Lcom/helpshift/support/conversations/messages/MessageViewDataBinder<",
        "Lcom/helpshift/support/conversations/messages/SystemRedactedConversationDataBinder$ViewHolder;",
        "Lcom/helpshift/conversation/activeconversation/message/SystemRedactedConversationMessageDM;",
        ">;"
    }
.end annotation


# direct methods
.method public constructor <init>(Landroid/content/Context;)V
    .locals 0

    .line 20
    invoke-direct {p0, p1}, Lcom/helpshift/support/conversations/messages/MessageViewDataBinder;-><init>(Landroid/content/Context;)V

    return-void
.end method


# virtual methods
.method public bridge synthetic bind(Landroidx/recyclerview/widget/RecyclerView$ViewHolder;Lcom/helpshift/conversation/activeconversation/message/MessageDM;)V
    .locals 0

    .line 15
    check-cast p1, Lcom/helpshift/support/conversations/messages/SystemRedactedConversationDataBinder$ViewHolder;

    check-cast p2, Lcom/helpshift/conversation/activeconversation/message/SystemRedactedConversationMessageDM;

    invoke-virtual {p0, p1, p2}, Lcom/helpshift/support/conversations/messages/SystemRedactedConversationDataBinder;->bind(Lcom/helpshift/support/conversations/messages/SystemRedactedConversationDataBinder$ViewHolder;Lcom/helpshift/conversation/activeconversation/message/SystemRedactedConversationMessageDM;)V

    return-void
.end method

.method public bind(Lcom/helpshift/support/conversations/messages/SystemRedactedConversationDataBinder$ViewHolder;Lcom/helpshift/conversation/activeconversation/message/SystemRedactedConversationMessageDM;)V
    .locals 4

    .line 35
    iget v0, p2, Lcom/helpshift/conversation/activeconversation/message/SystemRedactedConversationMessageDM;->contiguousRedactedConversationsCount:I

    .line 36
    iget p2, p2, Lcom/helpshift/conversation/activeconversation/message/SystemRedactedConversationMessageDM;->contiguousRedactedConversationsCount:I

    const/4 v1, 0x1

    if-le p2, v1, :cond_0

    .line 37
    iget-object p2, p0, Lcom/helpshift/support/conversations/messages/SystemRedactedConversationDataBinder;->context:Landroid/content/Context;

    sget v2, Lcom/helpshift/R$string;->hs__conversation_redacted_status_multiple:I

    new-array v1, v1, [Ljava/lang/Object;

    const/4 v3, 0x0

    invoke-static {v0}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v0

    aput-object v0, v1, v3

    invoke-virtual {p2, v2, v1}, Landroid/content/Context;->getString(I[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object p2

    goto :goto_0

    .line 40
    :cond_0
    iget-object p2, p0, Lcom/helpshift/support/conversations/messages/SystemRedactedConversationDataBinder;->context:Landroid/content/Context;

    sget v0, Lcom/helpshift/R$string;->hs__conversation_redacted_status:I

    invoke-virtual {p2, v0}, Landroid/content/Context;->getString(I)Ljava/lang/String;

    move-result-object p2

    .line 42
    :goto_0
    invoke-static {p1}, Lcom/helpshift/support/conversations/messages/SystemRedactedConversationDataBinder$ViewHolder;->access$000(Lcom/helpshift/support/conversations/messages/SystemRedactedConversationDataBinder$ViewHolder;)Landroid/widget/TextView;

    move-result-object p1

    invoke-virtual {p1, p2}, Landroid/widget/TextView;->setText(Ljava/lang/CharSequence;)V

    return-void
.end method

.method public bridge synthetic createViewHolder(Landroid/view/ViewGroup;)Landroidx/recyclerview/widget/RecyclerView$ViewHolder;
    .locals 0

    .line 15
    invoke-virtual {p0, p1}, Lcom/helpshift/support/conversations/messages/SystemRedactedConversationDataBinder;->createViewHolder(Landroid/view/ViewGroup;)Lcom/helpshift/support/conversations/messages/SystemRedactedConversationDataBinder$ViewHolder;

    move-result-object p1

    return-object p1
.end method

.method public createViewHolder(Landroid/view/ViewGroup;)Lcom/helpshift/support/conversations/messages/SystemRedactedConversationDataBinder$ViewHolder;
    .locals 3

    .line 26
    invoke-virtual {p1}, Landroid/view/ViewGroup;->getContext()Landroid/content/Context;

    move-result-object v0

    invoke-static {v0}, Landroid/view/LayoutInflater;->from(Landroid/content/Context;)Landroid/view/LayoutInflater;

    move-result-object v0

    sget v1, Lcom/helpshift/R$layout;->hs__msg_system_conversation_redacted_layout:I

    const/4 v2, 0x0

    invoke-virtual {v0, v1, p1, v2}, Landroid/view/LayoutInflater;->inflate(ILandroid/view/ViewGroup;Z)Landroid/view/View;

    move-result-object p1

    .line 28
    new-instance v0, Lcom/helpshift/support/conversations/messages/SystemRedactedConversationDataBinder$ViewHolder;

    invoke-direct {v0, p0, p1}, Lcom/helpshift/support/conversations/messages/SystemRedactedConversationDataBinder$ViewHolder;-><init>(Lcom/helpshift/support/conversations/messages/SystemRedactedConversationDataBinder;Landroid/view/View;)V

    return-object v0
.end method
