.class public final Lcom/helpshift/support/conversations/messages/UserMessageViewDataBinder$ViewHolder;
.super Landroidx/recyclerview/widget/RecyclerView$ViewHolder;
.source "UserMessageViewDataBinder.java"

# interfaces
.implements Landroid/view/View$OnCreateContextMenuListener;
.implements Landroid/view/View$OnClickListener;


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lcom/helpshift/support/conversations/messages/UserMessageViewDataBinder;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x14
    name = "ViewHolder"
.end annotation


# instance fields
.field final messageBubble:Landroid/widget/FrameLayout;

.field final messageLayout:Landroid/view/View;

.field final messageText:Landroid/widget/TextView;

.field final retryButton:Landroid/widget/ImageView;

.field final subText:Landroid/widget/TextView;

.field final synthetic this$0:Lcom/helpshift/support/conversations/messages/UserMessageViewDataBinder;


# direct methods
.method constructor <init>(Lcom/helpshift/support/conversations/messages/UserMessageViewDataBinder;Landroid/view/View;)V
    .locals 0

    .line 104
    iput-object p1, p0, Lcom/helpshift/support/conversations/messages/UserMessageViewDataBinder$ViewHolder;->this$0:Lcom/helpshift/support/conversations/messages/UserMessageViewDataBinder;

    .line 105
    invoke-direct {p0, p2}, Landroidx/recyclerview/widget/RecyclerView$ViewHolder;-><init>(Landroid/view/View;)V

    .line 106
    sget p1, Lcom/helpshift/R$id;->user_message_text:I

    invoke-virtual {p2, p1}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object p1

    check-cast p1, Landroid/widget/TextView;

    iput-object p1, p0, Lcom/helpshift/support/conversations/messages/UserMessageViewDataBinder$ViewHolder;->messageText:Landroid/widget/TextView;

    .line 107
    sget p1, Lcom/helpshift/R$id;->user_date_text:I

    invoke-virtual {p2, p1}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object p1

    check-cast p1, Landroid/widget/TextView;

    iput-object p1, p0, Lcom/helpshift/support/conversations/messages/UserMessageViewDataBinder$ViewHolder;->subText:Landroid/widget/TextView;

    .line 108
    sget p1, Lcom/helpshift/R$id;->user_message_container:I

    invoke-virtual {p2, p1}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object p1

    check-cast p1, Landroid/widget/FrameLayout;

    iput-object p1, p0, Lcom/helpshift/support/conversations/messages/UserMessageViewDataBinder$ViewHolder;->messageBubble:Landroid/widget/FrameLayout;

    .line 109
    sget p1, Lcom/helpshift/R$id;->user_message_retry_button:I

    invoke-virtual {p2, p1}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object p1

    check-cast p1, Landroid/widget/ImageView;

    iput-object p1, p0, Lcom/helpshift/support/conversations/messages/UserMessageViewDataBinder$ViewHolder;->retryButton:Landroid/widget/ImageView;

    .line 110
    sget p1, Lcom/helpshift/R$id;->user_text_message_layout:I

    invoke-virtual {p2, p1}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object p1

    iput-object p1, p0, Lcom/helpshift/support/conversations/messages/UserMessageViewDataBinder$ViewHolder;->messageLayout:Landroid/view/View;

    return-void
.end method


# virtual methods
.method public onClick(Landroid/view/View;)V
    .locals 1

    .line 128
    iget-object p1, p0, Lcom/helpshift/support/conversations/messages/UserMessageViewDataBinder$ViewHolder;->this$0:Lcom/helpshift/support/conversations/messages/UserMessageViewDataBinder;

    iget-object p1, p1, Lcom/helpshift/support/conversations/messages/UserMessageViewDataBinder;->messageClickListener:Lcom/helpshift/support/conversations/messages/MessageViewDataBinder$MessageItemClickListener;

    if-eqz p1, :cond_0

    .line 129
    iget-object p1, p0, Lcom/helpshift/support/conversations/messages/UserMessageViewDataBinder$ViewHolder;->this$0:Lcom/helpshift/support/conversations/messages/UserMessageViewDataBinder;

    iget-object p1, p1, Lcom/helpshift/support/conversations/messages/UserMessageViewDataBinder;->messageClickListener:Lcom/helpshift/support/conversations/messages/MessageViewDataBinder$MessageItemClickListener;

    invoke-virtual {p0}, Lcom/helpshift/support/conversations/messages/UserMessageViewDataBinder$ViewHolder;->getAdapterPosition()I

    move-result v0

    invoke-interface {p1, v0}, Lcom/helpshift/support/conversations/messages/MessageViewDataBinder$MessageItemClickListener;->retryMessage(I)V

    :cond_0
    return-void
.end method

.method public onCreateContextMenu(Landroid/view/ContextMenu;Landroid/view/View;Landroid/view/ContextMenu$ContextMenuInfo;)V
    .locals 0

    .line 120
    iget-object p3, p0, Lcom/helpshift/support/conversations/messages/UserMessageViewDataBinder$ViewHolder;->this$0:Lcom/helpshift/support/conversations/messages/UserMessageViewDataBinder;

    iget-object p3, p3, Lcom/helpshift/support/conversations/messages/UserMessageViewDataBinder;->messageClickListener:Lcom/helpshift/support/conversations/messages/MessageViewDataBinder$MessageItemClickListener;

    if-eqz p3, :cond_0

    .line 121
    check-cast p2, Landroid/widget/TextView;

    invoke-virtual {p2}, Landroid/widget/TextView;->getText()Ljava/lang/CharSequence;

    move-result-object p2

    invoke-interface {p2}, Ljava/lang/CharSequence;->toString()Ljava/lang/String;

    move-result-object p2

    .line 122
    iget-object p3, p0, Lcom/helpshift/support/conversations/messages/UserMessageViewDataBinder$ViewHolder;->this$0:Lcom/helpshift/support/conversations/messages/UserMessageViewDataBinder;

    iget-object p3, p3, Lcom/helpshift/support/conversations/messages/UserMessageViewDataBinder;->messageClickListener:Lcom/helpshift/support/conversations/messages/MessageViewDataBinder$MessageItemClickListener;

    invoke-interface {p3, p1, p2}, Lcom/helpshift/support/conversations/messages/MessageViewDataBinder$MessageItemClickListener;->onCreateContextMenu(Landroid/view/ContextMenu;Ljava/lang/String;)V

    :cond_0
    return-void
.end method

.method setListeners()V
    .locals 1

    .line 114
    iget-object v0, p0, Lcom/helpshift/support/conversations/messages/UserMessageViewDataBinder$ViewHolder;->messageText:Landroid/widget/TextView;

    invoke-virtual {v0, p0}, Landroid/widget/TextView;->setOnCreateContextMenuListener(Landroid/view/View$OnCreateContextMenuListener;)V

    return-void
.end method
