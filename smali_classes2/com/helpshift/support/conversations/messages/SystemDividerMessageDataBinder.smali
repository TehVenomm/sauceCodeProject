.class public Lcom/helpshift/support/conversations/messages/SystemDividerMessageDataBinder;
.super Lcom/helpshift/support/conversations/messages/MessageViewDataBinder;
.source "SystemDividerMessageDataBinder.java"


# annotations
.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Lcom/helpshift/support/conversations/messages/SystemDividerMessageDataBinder$ViewHolder;
    }
.end annotation

.annotation system Ldalvik/annotation/Signature;
    value = {
        "Lcom/helpshift/support/conversations/messages/MessageViewDataBinder<",
        "Lcom/helpshift/support/conversations/messages/SystemDividerMessageDataBinder$ViewHolder;",
        "Lcom/helpshift/conversation/activeconversation/message/SystemDividerMessageDM;",
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

    .line 16
    check-cast p1, Lcom/helpshift/support/conversations/messages/SystemDividerMessageDataBinder$ViewHolder;

    check-cast p2, Lcom/helpshift/conversation/activeconversation/message/SystemDividerMessageDM;

    invoke-virtual {p0, p1, p2}, Lcom/helpshift/support/conversations/messages/SystemDividerMessageDataBinder;->bind(Lcom/helpshift/support/conversations/messages/SystemDividerMessageDataBinder$ViewHolder;Lcom/helpshift/conversation/activeconversation/message/SystemDividerMessageDM;)V

    return-void
.end method

.method public bind(Lcom/helpshift/support/conversations/messages/SystemDividerMessageDataBinder$ViewHolder;Lcom/helpshift/conversation/activeconversation/message/SystemDividerMessageDM;)V
    .locals 1

    .line 33
    iget-boolean p2, p2, Lcom/helpshift/conversation/activeconversation/message/SystemDividerMessageDM;->showDividerText:Z

    if-eqz p2, :cond_0

    .line 34
    invoke-static {p1}, Lcom/helpshift/support/conversations/messages/SystemDividerMessageDataBinder$ViewHolder;->access$000(Lcom/helpshift/support/conversations/messages/SystemDividerMessageDataBinder$ViewHolder;)Landroid/widget/TextView;

    move-result-object p2

    const/4 v0, 0x0

    invoke-virtual {p2, v0}, Landroid/widget/TextView;->setVisibility(I)V

    const-string p2, ""

    goto :goto_0

    .line 39
    :cond_0
    invoke-static {p1}, Lcom/helpshift/support/conversations/messages/SystemDividerMessageDataBinder$ViewHolder;->access$000(Lcom/helpshift/support/conversations/messages/SystemDividerMessageDataBinder$ViewHolder;)Landroid/widget/TextView;

    move-result-object p2

    const/16 v0, 0x8

    invoke-virtual {p2, v0}, Landroid/widget/TextView;->setVisibility(I)V

    .line 40
    iget-object p2, p0, Lcom/helpshift/support/conversations/messages/SystemDividerMessageDataBinder;->context:Landroid/content/Context;

    sget v0, Lcom/helpshift/R$string;->hs__conversations_divider_voice_over:I

    invoke-virtual {p2, v0}, Landroid/content/Context;->getString(I)Ljava/lang/String;

    move-result-object p2

    .line 42
    :goto_0
    invoke-static {p1}, Lcom/helpshift/support/conversations/messages/SystemDividerMessageDataBinder$ViewHolder;->access$100(Lcom/helpshift/support/conversations/messages/SystemDividerMessageDataBinder$ViewHolder;)Landroid/view/View;

    move-result-object p1

    invoke-virtual {p1, p2}, Landroid/view/View;->setContentDescription(Ljava/lang/CharSequence;)V

    return-void
.end method

.method public bridge synthetic createViewHolder(Landroid/view/ViewGroup;)Landroidx/recyclerview/widget/RecyclerView$ViewHolder;
    .locals 0

    .line 16
    invoke-virtual {p0, p1}, Lcom/helpshift/support/conversations/messages/SystemDividerMessageDataBinder;->createViewHolder(Landroid/view/ViewGroup;)Lcom/helpshift/support/conversations/messages/SystemDividerMessageDataBinder$ViewHolder;

    move-result-object p1

    return-object p1
.end method

.method public createViewHolder(Landroid/view/ViewGroup;)Lcom/helpshift/support/conversations/messages/SystemDividerMessageDataBinder$ViewHolder;
    .locals 3

    .line 25
    invoke-virtual {p1}, Landroid/view/ViewGroup;->getContext()Landroid/content/Context;

    move-result-object v0

    invoke-static {v0}, Landroid/view/LayoutInflater;->from(Landroid/content/Context;)Landroid/view/LayoutInflater;

    move-result-object v0

    sget v1, Lcom/helpshift/R$layout;->hs__msg_system_divider_layout:I

    const/4 v2, 0x0

    invoke-virtual {v0, v1, p1, v2}, Landroid/view/LayoutInflater;->inflate(ILandroid/view/ViewGroup;Z)Landroid/view/View;

    move-result-object p1

    .line 27
    new-instance v0, Lcom/helpshift/support/conversations/messages/SystemDividerMessageDataBinder$ViewHolder;

    invoke-direct {v0, p0, p1}, Lcom/helpshift/support/conversations/messages/SystemDividerMessageDataBinder$ViewHolder;-><init>(Lcom/helpshift/support/conversations/messages/SystemDividerMessageDataBinder;Landroid/view/View;)V

    return-object v0
.end method
