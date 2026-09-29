.class public Lcom/helpshift/support/conversations/messages/SystemDividerMessageDataBinder$ViewHolder;
.super Landroidx/recyclerview/widget/RecyclerView$ViewHolder;
.source "SystemDividerMessageDataBinder.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lcom/helpshift/support/conversations/messages/SystemDividerMessageDataBinder;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x1
    name = "ViewHolder"
.end annotation


# instance fields
.field private dividerHeader:Landroid/widget/TextView;

.field private dividerView:Landroid/view/View;

.field final synthetic this$0:Lcom/helpshift/support/conversations/messages/SystemDividerMessageDataBinder;


# direct methods
.method public constructor <init>(Lcom/helpshift/support/conversations/messages/SystemDividerMessageDataBinder;Landroid/view/View;)V
    .locals 0

    .line 49
    iput-object p1, p0, Lcom/helpshift/support/conversations/messages/SystemDividerMessageDataBinder$ViewHolder;->this$0:Lcom/helpshift/support/conversations/messages/SystemDividerMessageDataBinder;

    .line 50
    invoke-direct {p0, p2}, Landroidx/recyclerview/widget/RecyclerView$ViewHolder;-><init>(Landroid/view/View;)V

    .line 51
    sget p1, Lcom/helpshift/R$id;->conversations_divider:I

    invoke-virtual {p2, p1}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object p1

    iput-object p1, p0, Lcom/helpshift/support/conversations/messages/SystemDividerMessageDataBinder$ViewHolder;->dividerView:Landroid/view/View;

    .line 52
    sget p1, Lcom/helpshift/R$id;->conversation_closed_view:I

    invoke-virtual {p2, p1}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object p1

    check-cast p1, Landroid/widget/TextView;

    iput-object p1, p0, Lcom/helpshift/support/conversations/messages/SystemDividerMessageDataBinder$ViewHolder;->dividerHeader:Landroid/widget/TextView;

    return-void
.end method

.method static synthetic access$000(Lcom/helpshift/support/conversations/messages/SystemDividerMessageDataBinder$ViewHolder;)Landroid/widget/TextView;
    .locals 0

    .line 45
    iget-object p0, p0, Lcom/helpshift/support/conversations/messages/SystemDividerMessageDataBinder$ViewHolder;->dividerHeader:Landroid/widget/TextView;

    return-object p0
.end method

.method static synthetic access$100(Lcom/helpshift/support/conversations/messages/SystemDividerMessageDataBinder$ViewHolder;)Landroid/view/View;
    .locals 0

    .line 45
    iget-object p0, p0, Lcom/helpshift/support/conversations/messages/SystemDividerMessageDataBinder$ViewHolder;->dividerView:Landroid/view/View;

    return-object p0
.end method
