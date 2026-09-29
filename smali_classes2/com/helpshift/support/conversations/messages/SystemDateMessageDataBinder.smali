.class public Lcom/helpshift/support/conversations/messages/SystemDateMessageDataBinder;
.super Lcom/helpshift/support/conversations/messages/MessageViewDataBinder;
.source "SystemDateMessageDataBinder.java"


# annotations
.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Lcom/helpshift/support/conversations/messages/SystemDateMessageDataBinder$ViewHolder;
    }
.end annotation

.annotation system Ldalvik/annotation/Signature;
    value = {
        "Lcom/helpshift/support/conversations/messages/MessageViewDataBinder<",
        "Lcom/helpshift/support/conversations/messages/SystemDateMessageDataBinder$ViewHolder;",
        "Lcom/helpshift/conversation/activeconversation/message/SystemDateMessageDM;",
        ">;"
    }
.end annotation


# direct methods
.method public constructor <init>(Landroid/content/Context;)V
    .locals 0

    .line 16
    invoke-direct {p0, p1}, Lcom/helpshift/support/conversations/messages/MessageViewDataBinder;-><init>(Landroid/content/Context;)V

    return-void
.end method


# virtual methods
.method public bridge synthetic bind(Landroidx/recyclerview/widget/RecyclerView$ViewHolder;Lcom/helpshift/conversation/activeconversation/message/MessageDM;)V
    .locals 0

    .line 12
    check-cast p1, Lcom/helpshift/support/conversations/messages/SystemDateMessageDataBinder$ViewHolder;

    check-cast p2, Lcom/helpshift/conversation/activeconversation/message/SystemDateMessageDM;

    invoke-virtual {p0, p1, p2}, Lcom/helpshift/support/conversations/messages/SystemDateMessageDataBinder;->bind(Lcom/helpshift/support/conversations/messages/SystemDateMessageDataBinder$ViewHolder;Lcom/helpshift/conversation/activeconversation/message/SystemDateMessageDM;)V

    return-void
.end method

.method public bind(Lcom/helpshift/support/conversations/messages/SystemDateMessageDataBinder$ViewHolder;Lcom/helpshift/conversation/activeconversation/message/SystemDateMessageDM;)V
    .locals 3

    .line 28
    iget-object v0, p1, Lcom/helpshift/support/conversations/messages/SystemDateMessageDataBinder$ViewHolder;->itemView:Landroid/view/View;

    invoke-virtual {v0}, Landroid/view/View;->getLayoutParams()Landroid/view/ViewGroup$LayoutParams;

    move-result-object v0

    check-cast v0, Landroidx/recyclerview/widget/RecyclerView$LayoutParams;

    .line 29
    iget-boolean v1, p2, Lcom/helpshift/conversation/activeconversation/message/SystemDateMessageDM;->isFirstMessageInList:Z

    if-eqz v1, :cond_0

    .line 30
    iget-object v1, p0, Lcom/helpshift/support/conversations/messages/SystemDateMessageDataBinder;->context:Landroid/content/Context;

    const/high16 v2, 0x41900000    # 18.0f

    invoke-static {v1, v2}, Lcom/helpshift/util/Styles;->dpToPx(Landroid/content/Context;F)F

    move-result v1

    float-to-int v1, v1

    iput v1, v0, Landroidx/recyclerview/widget/RecyclerView$LayoutParams;->topMargin:I

    goto :goto_0

    .line 32
    :cond_0
    iget-object v1, p0, Lcom/helpshift/support/conversations/messages/SystemDateMessageDataBinder;->context:Landroid/content/Context;

    const/high16 v2, 0x40000000    # 2.0f

    invoke-static {v1, v2}, Lcom/helpshift/util/Styles;->dpToPx(Landroid/content/Context;F)F

    move-result v1

    float-to-int v1, v1

    iput v1, v0, Landroidx/recyclerview/widget/RecyclerView$LayoutParams;->topMargin:I

    .line 34
    :goto_0
    iget-object v1, p1, Lcom/helpshift/support/conversations/messages/SystemDateMessageDataBinder$ViewHolder;->itemView:Landroid/view/View;

    invoke-virtual {v1, v0}, Landroid/view/View;->setLayoutParams(Landroid/view/ViewGroup$LayoutParams;)V

    .line 35
    invoke-static {p1}, Lcom/helpshift/support/conversations/messages/SystemDateMessageDataBinder$ViewHolder;->access$000(Lcom/helpshift/support/conversations/messages/SystemDateMessageDataBinder$ViewHolder;)Landroid/widget/TextView;

    move-result-object p1

    invoke-virtual {p2}, Lcom/helpshift/conversation/activeconversation/message/SystemDateMessageDM;->getBodyText()Ljava/lang/String;

    move-result-object p2

    invoke-virtual {p1, p2}, Landroid/widget/TextView;->setText(Ljava/lang/CharSequence;)V

    return-void
.end method

.method public bridge synthetic createViewHolder(Landroid/view/ViewGroup;)Landroidx/recyclerview/widget/RecyclerView$ViewHolder;
    .locals 0

    .line 12
    invoke-virtual {p0, p1}, Lcom/helpshift/support/conversations/messages/SystemDateMessageDataBinder;->createViewHolder(Landroid/view/ViewGroup;)Lcom/helpshift/support/conversations/messages/SystemDateMessageDataBinder$ViewHolder;

    move-result-object p1

    return-object p1
.end method

.method public createViewHolder(Landroid/view/ViewGroup;)Lcom/helpshift/support/conversations/messages/SystemDateMessageDataBinder$ViewHolder;
    .locals 3

    .line 21
    invoke-virtual {p1}, Landroid/view/ViewGroup;->getContext()Landroid/content/Context;

    move-result-object v0

    invoke-static {v0}, Landroid/view/LayoutInflater;->from(Landroid/content/Context;)Landroid/view/LayoutInflater;

    move-result-object v0

    sget v1, Lcom/helpshift/R$layout;->hs__msg_system_layout:I

    const/4 v2, 0x0

    invoke-virtual {v0, v1, p1, v2}, Landroid/view/LayoutInflater;->inflate(ILandroid/view/ViewGroup;Z)Landroid/view/View;

    move-result-object p1

    .line 23
    new-instance v0, Lcom/helpshift/support/conversations/messages/SystemDateMessageDataBinder$ViewHolder;

    invoke-direct {v0, p0, p1}, Lcom/helpshift/support/conversations/messages/SystemDateMessageDataBinder$ViewHolder;-><init>(Lcom/helpshift/support/conversations/messages/SystemDateMessageDataBinder;Landroid/view/View;)V

    return-object v0
.end method
