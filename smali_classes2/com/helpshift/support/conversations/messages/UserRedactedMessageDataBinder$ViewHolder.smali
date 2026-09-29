.class public final Lcom/helpshift/support/conversations/messages/UserRedactedMessageDataBinder$ViewHolder;
.super Landroidx/recyclerview/widget/RecyclerView$ViewHolder;
.source "UserRedactedMessageDataBinder.java"

# interfaces
.implements Landroid/view/View$OnCreateContextMenuListener;


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lcom/helpshift/support/conversations/messages/UserRedactedMessageDataBinder;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x14
    name = "ViewHolder"
.end annotation


# instance fields
.field final messageBubble:Landroid/widget/FrameLayout;

.field final messageLayout:Landroid/view/View;

.field final messageText:Landroid/widget/TextView;

.field final subText:Landroid/widget/TextView;

.field final synthetic this$0:Lcom/helpshift/support/conversations/messages/UserRedactedMessageDataBinder;


# direct methods
.method constructor <init>(Lcom/helpshift/support/conversations/messages/UserRedactedMessageDataBinder;Landroid/view/View;)V
    .locals 0

    .line 60
    iput-object p1, p0, Lcom/helpshift/support/conversations/messages/UserRedactedMessageDataBinder$ViewHolder;->this$0:Lcom/helpshift/support/conversations/messages/UserRedactedMessageDataBinder;

    .line 61
    invoke-direct {p0, p2}, Landroidx/recyclerview/widget/RecyclerView$ViewHolder;-><init>(Landroid/view/View;)V

    .line 62
    sget p1, Lcom/helpshift/R$id;->user_message_text:I

    invoke-virtual {p2, p1}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object p1

    check-cast p1, Landroid/widget/TextView;

    iput-object p1, p0, Lcom/helpshift/support/conversations/messages/UserRedactedMessageDataBinder$ViewHolder;->messageText:Landroid/widget/TextView;

    .line 63
    sget p1, Lcom/helpshift/R$id;->user_date_text:I

    invoke-virtual {p2, p1}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object p1

    check-cast p1, Landroid/widget/TextView;

    iput-object p1, p0, Lcom/helpshift/support/conversations/messages/UserRedactedMessageDataBinder$ViewHolder;->subText:Landroid/widget/TextView;

    .line 64
    sget p1, Lcom/helpshift/R$id;->user_message_container:I

    invoke-virtual {p2, p1}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object p1

    check-cast p1, Landroid/widget/FrameLayout;

    iput-object p1, p0, Lcom/helpshift/support/conversations/messages/UserRedactedMessageDataBinder$ViewHolder;->messageBubble:Landroid/widget/FrameLayout;

    .line 65
    sget p1, Lcom/helpshift/R$id;->user_text_message_layout:I

    invoke-virtual {p2, p1}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object p1

    iput-object p1, p0, Lcom/helpshift/support/conversations/messages/UserRedactedMessageDataBinder$ViewHolder;->messageLayout:Landroid/view/View;

    return-void
.end method


# virtual methods
.method public onCreateContextMenu(Landroid/view/ContextMenu;Landroid/view/View;Landroid/view/ContextMenu$ContextMenuInfo;)V
    .locals 0

    .line 75
    iget-object p3, p0, Lcom/helpshift/support/conversations/messages/UserRedactedMessageDataBinder$ViewHolder;->this$0:Lcom/helpshift/support/conversations/messages/UserRedactedMessageDataBinder;

    iget-object p3, p3, Lcom/helpshift/support/conversations/messages/UserRedactedMessageDataBinder;->messageClickListener:Lcom/helpshift/support/conversations/messages/MessageViewDataBinder$MessageItemClickListener;

    if-eqz p3, :cond_0

    .line 76
    check-cast p2, Landroid/widget/TextView;

    invoke-virtual {p2}, Landroid/widget/TextView;->getText()Ljava/lang/CharSequence;

    move-result-object p2

    invoke-interface {p2}, Ljava/lang/CharSequence;->toString()Ljava/lang/String;

    move-result-object p2

    .line 77
    iget-object p3, p0, Lcom/helpshift/support/conversations/messages/UserRedactedMessageDataBinder$ViewHolder;->this$0:Lcom/helpshift/support/conversations/messages/UserRedactedMessageDataBinder;

    iget-object p3, p3, Lcom/helpshift/support/conversations/messages/UserRedactedMessageDataBinder;->messageClickListener:Lcom/helpshift/support/conversations/messages/MessageViewDataBinder$MessageItemClickListener;

    invoke-interface {p3, p1, p2}, Lcom/helpshift/support/conversations/messages/MessageViewDataBinder$MessageItemClickListener;->onCreateContextMenu(Landroid/view/ContextMenu;Ljava/lang/String;)V

    :cond_0
    return-void
.end method

.method setListeners()V
    .locals 1

    .line 69
    iget-object v0, p0, Lcom/helpshift/support/conversations/messages/UserRedactedMessageDataBinder$ViewHolder;->messageText:Landroid/widget/TextView;

    invoke-virtual {v0, p0}, Landroid/widget/TextView;->setOnCreateContextMenuListener(Landroid/view/View$OnCreateContextMenuListener;)V

    return-void
.end method
