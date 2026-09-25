.class public final Lcom/helpshift/support/conversations/messages/ConfirmationRejectedMessageDataBinder$ViewHolder;
.super Landroidx/recyclerview/widget/RecyclerView$ViewHolder;
.source "ConfirmationRejectedMessageDataBinder.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lcom/helpshift/support/conversations/messages/ConfirmationRejectedMessageDataBinder;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x14
    name = "ViewHolder"
.end annotation


# instance fields
.field final message:Landroid/widget/TextView;

.field final messageContainer:Landroid/view/View;

.field final messageLayout:Landroid/view/View;

.field final subText:Landroid/widget/TextView;

.field final synthetic this$0:Lcom/helpshift/support/conversations/messages/ConfirmationRejectedMessageDataBinder;


# direct methods
.method constructor <init>(Lcom/helpshift/support/conversations/messages/ConfirmationRejectedMessageDataBinder;Landroid/view/View;)V
    .locals 0

    .line 48
    iput-object p1, p0, Lcom/helpshift/support/conversations/messages/ConfirmationRejectedMessageDataBinder$ViewHolder;->this$0:Lcom/helpshift/support/conversations/messages/ConfirmationRejectedMessageDataBinder;

    .line 49
    invoke-direct {p0, p2}, Landroidx/recyclerview/widget/RecyclerView$ViewHolder;-><init>(Landroid/view/View;)V

    .line 50
    sget p1, Lcom/helpshift/R$id;->admin_text_message_layout:I

    invoke-virtual {p2, p1}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object p1

    iput-object p1, p0, Lcom/helpshift/support/conversations/messages/ConfirmationRejectedMessageDataBinder$ViewHolder;->messageLayout:Landroid/view/View;

    .line 51
    sget p1, Lcom/helpshift/R$id;->admin_message_text:I

    invoke-virtual {p2, p1}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object p1

    check-cast p1, Landroid/widget/TextView;

    iput-object p1, p0, Lcom/helpshift/support/conversations/messages/ConfirmationRejectedMessageDataBinder$ViewHolder;->message:Landroid/widget/TextView;

    .line 52
    sget p1, Lcom/helpshift/R$id;->admin_date_text:I

    invoke-virtual {p2, p1}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object p1

    check-cast p1, Landroid/widget/TextView;

    iput-object p1, p0, Lcom/helpshift/support/conversations/messages/ConfirmationRejectedMessageDataBinder$ViewHolder;->subText:Landroid/widget/TextView;

    .line 53
    sget p1, Lcom/helpshift/R$id;->admin_message_container:I

    invoke-virtual {p2, p1}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object p1

    iput-object p1, p0, Lcom/helpshift/support/conversations/messages/ConfirmationRejectedMessageDataBinder$ViewHolder;->messageContainer:Landroid/view/View;

    return-void
.end method
