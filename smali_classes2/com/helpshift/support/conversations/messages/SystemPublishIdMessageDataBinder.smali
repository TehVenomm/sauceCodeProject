.class public Lcom/helpshift/support/conversations/messages/SystemPublishIdMessageDataBinder;
.super Lcom/helpshift/support/conversations/messages/MessageViewDataBinder;
.source "SystemPublishIdMessageDataBinder.java"


# annotations
.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Lcom/helpshift/support/conversations/messages/SystemPublishIdMessageDataBinder$ViewHolder;
    }
.end annotation

.annotation system Ldalvik/annotation/Signature;
    value = {
        "Lcom/helpshift/support/conversations/messages/MessageViewDataBinder<",
        "Lcom/helpshift/support/conversations/messages/SystemPublishIdMessageDataBinder$ViewHolder;",
        "Lcom/helpshift/conversation/activeconversation/message/SystemPublishIdMessageDM;",
        ">;"
    }
.end annotation


# direct methods
.method public constructor <init>(Landroid/content/Context;)V
    .locals 0

    .line 17
    invoke-direct {p0, p1}, Lcom/helpshift/support/conversations/messages/MessageViewDataBinder;-><init>(Landroid/content/Context;)V

    return-void
.end method


# virtual methods
.method public bridge synthetic bind(Landroidx/recyclerview/widget/RecyclerView$ViewHolder;Lcom/helpshift/conversation/activeconversation/message/MessageDM;)V
    .locals 0

    .line 13
    check-cast p1, Lcom/helpshift/support/conversations/messages/SystemPublishIdMessageDataBinder$ViewHolder;

    check-cast p2, Lcom/helpshift/conversation/activeconversation/message/SystemPublishIdMessageDM;

    invoke-virtual {p0, p1, p2}, Lcom/helpshift/support/conversations/messages/SystemPublishIdMessageDataBinder;->bind(Lcom/helpshift/support/conversations/messages/SystemPublishIdMessageDataBinder$ViewHolder;Lcom/helpshift/conversation/activeconversation/message/SystemPublishIdMessageDM;)V

    return-void
.end method

.method public bind(Lcom/helpshift/support/conversations/messages/SystemPublishIdMessageDataBinder$ViewHolder;Lcom/helpshift/conversation/activeconversation/message/SystemPublishIdMessageDM;)V
    .locals 7

    .line 33
    iget-object v0, p1, Lcom/helpshift/support/conversations/messages/SystemPublishIdMessageDataBinder$ViewHolder;->itemView:Landroid/view/View;

    invoke-virtual {v0}, Landroid/view/View;->getLayoutParams()Landroid/view/ViewGroup$LayoutParams;

    move-result-object v0

    check-cast v0, Landroidx/recyclerview/widget/RecyclerView$LayoutParams;

    .line 34
    iget-boolean v1, p2, Lcom/helpshift/conversation/activeconversation/message/SystemPublishIdMessageDM;->isFirstMessageInList:Z

    const/4 v2, 0x0

    if-eqz v1, :cond_0

    .line 35
    iget-object v1, p0, Lcom/helpshift/support/conversations/messages/SystemPublishIdMessageDataBinder;->context:Landroid/content/Context;

    const/high16 v3, 0x41900000    # 18.0f

    invoke-static {v1, v3}, Lcom/helpshift/util/Styles;->dpToPx(Landroid/content/Context;F)F

    move-result v1

    float-to-int v1, v1

    iput v1, v0, Landroidx/recyclerview/widget/RecyclerView$LayoutParams;->topMargin:I

    goto :goto_0

    .line 38
    :cond_0
    iput v2, v0, Landroidx/recyclerview/widget/RecyclerView$LayoutParams;->topMargin:I

    .line 40
    :goto_0
    iget-object v1, p1, Lcom/helpshift/support/conversations/messages/SystemPublishIdMessageDataBinder$ViewHolder;->itemView:Landroid/view/View;

    invoke-virtual {v1, v0}, Landroid/view/View;->setLayoutParams(Landroid/view/ViewGroup$LayoutParams;)V

    .line 41
    invoke-static {p1}, Lcom/helpshift/support/conversations/messages/SystemPublishIdMessageDataBinder$ViewHolder;->access$000(Lcom/helpshift/support/conversations/messages/SystemPublishIdMessageDataBinder$ViewHolder;)Landroid/widget/TextView;

    move-result-object v0

    iget-object v1, p0, Lcom/helpshift/support/conversations/messages/SystemPublishIdMessageDataBinder;->context:Landroid/content/Context;

    sget v3, Lcom/helpshift/R$string;->hs__conversation_issue_id_header:I

    const/4 v4, 0x1

    new-array v5, v4, [Ljava/lang/Object;

    iget-object v6, p2, Lcom/helpshift/conversation/activeconversation/message/SystemPublishIdMessageDM;->body:Ljava/lang/String;

    aput-object v6, v5, v2

    invoke-virtual {v1, v3, v5}, Landroid/content/Context;->getString(I[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Landroid/widget/TextView;->setText(Ljava/lang/CharSequence;)V

    .line 42
    iget-object v0, p0, Lcom/helpshift/support/conversations/messages/SystemPublishIdMessageDataBinder;->context:Landroid/content/Context;

    sget v1, Lcom/helpshift/R$string;->hs__conversation_publish_id_voice_over:I

    new-array v3, v4, [Ljava/lang/Object;

    iget-object p2, p2, Lcom/helpshift/conversation/activeconversation/message/SystemPublishIdMessageDM;->body:Ljava/lang/String;

    aput-object p2, v3, v2

    invoke-virtual {v0, v1, v3}, Landroid/content/Context;->getString(I[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object p2

    .line 43
    invoke-static {p1}, Lcom/helpshift/support/conversations/messages/SystemPublishIdMessageDataBinder$ViewHolder;->access$000(Lcom/helpshift/support/conversations/messages/SystemPublishIdMessageDataBinder$ViewHolder;)Landroid/widget/TextView;

    move-result-object p1

    invoke-virtual {p1, p2}, Landroid/widget/TextView;->setContentDescription(Ljava/lang/CharSequence;)V

    return-void
.end method

.method public bridge synthetic createViewHolder(Landroid/view/ViewGroup;)Landroidx/recyclerview/widget/RecyclerView$ViewHolder;
    .locals 0

    .line 13
    invoke-virtual {p0, p1}, Lcom/helpshift/support/conversations/messages/SystemPublishIdMessageDataBinder;->createViewHolder(Landroid/view/ViewGroup;)Lcom/helpshift/support/conversations/messages/SystemPublishIdMessageDataBinder$ViewHolder;

    move-result-object p1

    return-object p1
.end method

.method public createViewHolder(Landroid/view/ViewGroup;)Lcom/helpshift/support/conversations/messages/SystemPublishIdMessageDataBinder$ViewHolder;
    .locals 3

    .line 22
    invoke-virtual {p1}, Landroid/view/ViewGroup;->getContext()Landroid/content/Context;

    move-result-object v0

    invoke-static {v0}, Landroid/view/LayoutInflater;->from(Landroid/content/Context;)Landroid/view/LayoutInflater;

    move-result-object v0

    sget v1, Lcom/helpshift/R$layout;->hs__msg_publish_id_layout:I

    const/4 v2, 0x0

    invoke-virtual {v0, v1, p1, v2}, Landroid/view/LayoutInflater;->inflate(ILandroid/view/ViewGroup;Z)Landroid/view/View;

    move-result-object p1

    .line 25
    new-instance v0, Lcom/helpshift/support/conversations/messages/SystemPublishIdMessageDataBinder$ViewHolder;

    invoke-direct {v0, p0, p1}, Lcom/helpshift/support/conversations/messages/SystemPublishIdMessageDataBinder$ViewHolder;-><init>(Lcom/helpshift/support/conversations/messages/SystemPublishIdMessageDataBinder;Landroid/view/View;)V

    .line 26
    invoke-virtual {v0}, Lcom/helpshift/support/conversations/messages/SystemPublishIdMessageDataBinder$ViewHolder;->setListeners()V

    return-object v0
.end method
