.class public final Lcom/helpshift/support/conversations/messages/RequestAppReviewMessageDataBinder$ViewHolder;
.super Landroidx/recyclerview/widget/RecyclerView$ViewHolder;
.source "RequestAppReviewMessageDataBinder.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lcom/helpshift/support/conversations/messages/RequestAppReviewMessageDataBinder;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x14
    name = "ViewHolder"
.end annotation


# instance fields
.field final message:Landroid/widget/TextView;

.field final messageContainer:Landroid/view/View;

.field final messageLayout:Landroid/view/View;

.field final reviewButton:Landroid/widget/Button;

.field final subText:Landroid/widget/TextView;

.field final synthetic this$0:Lcom/helpshift/support/conversations/messages/RequestAppReviewMessageDataBinder;


# direct methods
.method constructor <init>(Lcom/helpshift/support/conversations/messages/RequestAppReviewMessageDataBinder;Landroid/view/View;)V
    .locals 0

    .line 70
    iput-object p1, p0, Lcom/helpshift/support/conversations/messages/RequestAppReviewMessageDataBinder$ViewHolder;->this$0:Lcom/helpshift/support/conversations/messages/RequestAppReviewMessageDataBinder;

    .line 71
    invoke-direct {p0, p2}, Landroidx/recyclerview/widget/RecyclerView$ViewHolder;-><init>(Landroid/view/View;)V

    .line 72
    sget p1, Lcom/helpshift/R$id;->admin_review_message_layout:I

    invoke-virtual {p2, p1}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object p1

    iput-object p1, p0, Lcom/helpshift/support/conversations/messages/RequestAppReviewMessageDataBinder$ViewHolder;->messageLayout:Landroid/view/View;

    .line 73
    sget p1, Lcom/helpshift/R$id;->review_request_message:I

    invoke-virtual {p2, p1}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object p1

    check-cast p1, Landroid/widget/TextView;

    iput-object p1, p0, Lcom/helpshift/support/conversations/messages/RequestAppReviewMessageDataBinder$ViewHolder;->message:Landroid/widget/TextView;

    .line 74
    sget p1, Lcom/helpshift/R$id;->review_request_button:I

    invoke-virtual {p2, p1}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object p1

    check-cast p1, Landroid/widget/Button;

    iput-object p1, p0, Lcom/helpshift/support/conversations/messages/RequestAppReviewMessageDataBinder$ViewHolder;->reviewButton:Landroid/widget/Button;

    .line 75
    sget p1, Lcom/helpshift/R$id;->review_request_date:I

    invoke-virtual {p2, p1}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object p1

    check-cast p1, Landroid/widget/TextView;

    iput-object p1, p0, Lcom/helpshift/support/conversations/messages/RequestAppReviewMessageDataBinder$ViewHolder;->subText:Landroid/widget/TextView;

    .line 76
    sget p1, Lcom/helpshift/R$id;->review_request_message_container:I

    invoke-virtual {p2, p1}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object p1

    iput-object p1, p0, Lcom/helpshift/support/conversations/messages/RequestAppReviewMessageDataBinder$ViewHolder;->messageContainer:Landroid/view/View;

    return-void
.end method
