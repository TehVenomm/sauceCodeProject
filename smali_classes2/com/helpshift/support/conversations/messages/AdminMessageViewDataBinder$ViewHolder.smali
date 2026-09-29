.class public final Lcom/helpshift/support/conversations/messages/AdminMessageViewDataBinder$ViewHolder;
.super Landroidx/recyclerview/widget/RecyclerView$ViewHolder;
.source "AdminMessageViewDataBinder.java"

# interfaces
.implements Landroid/view/View$OnCreateContextMenuListener;


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lcom/helpshift/support/conversations/messages/AdminMessageViewDataBinder;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x14
    name = "ViewHolder"
.end annotation


# instance fields
.field final dateText:Landroid/widget/TextView;

.field final messageContainer:Landroid/view/View;

.field final messageLayout:Landroid/view/View;

.field final messageText:Landroid/widget/TextView;

.field final synthetic this$0:Lcom/helpshift/support/conversations/messages/AdminMessageViewDataBinder;


# direct methods
.method constructor <init>(Lcom/helpshift/support/conversations/messages/AdminMessageViewDataBinder;Landroid/view/View;)V
    .locals 0

    .line 68
    iput-object p1, p0, Lcom/helpshift/support/conversations/messages/AdminMessageViewDataBinder$ViewHolder;->this$0:Lcom/helpshift/support/conversations/messages/AdminMessageViewDataBinder;

    .line 69
    invoke-direct {p0, p2}, Landroidx/recyclerview/widget/RecyclerView$ViewHolder;-><init>(Landroid/view/View;)V

    .line 70
    sget p1, Lcom/helpshift/R$id;->admin_text_message_layout:I

    invoke-virtual {p2, p1}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object p1

    iput-object p1, p0, Lcom/helpshift/support/conversations/messages/AdminMessageViewDataBinder$ViewHolder;->messageLayout:Landroid/view/View;

    .line 71
    sget p1, Lcom/helpshift/R$id;->admin_message_text:I

    invoke-virtual {p2, p1}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object p1

    check-cast p1, Landroid/widget/TextView;

    iput-object p1, p0, Lcom/helpshift/support/conversations/messages/AdminMessageViewDataBinder$ViewHolder;->messageText:Landroid/widget/TextView;

    .line 72
    sget p1, Lcom/helpshift/R$id;->admin_date_text:I

    invoke-virtual {p2, p1}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object p1

    check-cast p1, Landroid/widget/TextView;

    iput-object p1, p0, Lcom/helpshift/support/conversations/messages/AdminMessageViewDataBinder$ViewHolder;->dateText:Landroid/widget/TextView;

    .line 73
    sget p1, Lcom/helpshift/R$id;->admin_message_container:I

    invoke-virtual {p2, p1}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object p1

    iput-object p1, p0, Lcom/helpshift/support/conversations/messages/AdminMessageViewDataBinder$ViewHolder;->messageContainer:Landroid/view/View;

    return-void
.end method


# virtual methods
.method public onCreateContextMenu(Landroid/view/ContextMenu;Landroid/view/View;Landroid/view/ContextMenu$ContextMenuInfo;)V
    .locals 0

    .line 83
    iget-object p3, p0, Lcom/helpshift/support/conversations/messages/AdminMessageViewDataBinder$ViewHolder;->this$0:Lcom/helpshift/support/conversations/messages/AdminMessageViewDataBinder;

    iget-object p3, p3, Lcom/helpshift/support/conversations/messages/AdminMessageViewDataBinder;->messageClickListener:Lcom/helpshift/support/conversations/messages/MessageViewDataBinder$MessageItemClickListener;

    if-eqz p3, :cond_0

    .line 84
    check-cast p2, Landroid/widget/TextView;

    invoke-virtual {p2}, Landroid/widget/TextView;->getText()Ljava/lang/CharSequence;

    move-result-object p2

    invoke-interface {p2}, Ljava/lang/CharSequence;->toString()Ljava/lang/String;

    move-result-object p2

    .line 85
    iget-object p3, p0, Lcom/helpshift/support/conversations/messages/AdminMessageViewDataBinder$ViewHolder;->this$0:Lcom/helpshift/support/conversations/messages/AdminMessageViewDataBinder;

    iget-object p3, p3, Lcom/helpshift/support/conversations/messages/AdminMessageViewDataBinder;->messageClickListener:Lcom/helpshift/support/conversations/messages/MessageViewDataBinder$MessageItemClickListener;

    invoke-interface {p3, p1, p2}, Lcom/helpshift/support/conversations/messages/MessageViewDataBinder$MessageItemClickListener;->onCreateContextMenu(Landroid/view/ContextMenu;Ljava/lang/String;)V

    :cond_0
    return-void
.end method

.method setListeners()V
    .locals 1

    .line 77
    iget-object v0, p0, Lcom/helpshift/support/conversations/messages/AdminMessageViewDataBinder$ViewHolder;->messageText:Landroid/widget/TextView;

    invoke-virtual {v0, p0}, Landroid/widget/TextView;->setOnCreateContextMenuListener(Landroid/view/View$OnCreateContextMenuListener;)V

    return-void
.end method
