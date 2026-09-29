.class public Lcom/helpshift/support/conversations/messages/SystemDateMessageDataBinder$ViewHolder;
.super Landroidx/recyclerview/widget/RecyclerView$ViewHolder;
.source "SystemDateMessageDataBinder.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lcom/helpshift/support/conversations/messages/SystemDateMessageDataBinder;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x1
    name = "ViewHolder"
.end annotation


# instance fields
.field private final body:Landroid/widget/TextView;

.field final synthetic this$0:Lcom/helpshift/support/conversations/messages/SystemDateMessageDataBinder;


# direct methods
.method public constructor <init>(Lcom/helpshift/support/conversations/messages/SystemDateMessageDataBinder;Landroid/view/View;)V
    .locals 0

    .line 41
    iput-object p1, p0, Lcom/helpshift/support/conversations/messages/SystemDateMessageDataBinder$ViewHolder;->this$0:Lcom/helpshift/support/conversations/messages/SystemDateMessageDataBinder;

    .line 42
    invoke-direct {p0, p2}, Landroidx/recyclerview/widget/RecyclerView$ViewHolder;-><init>(Landroid/view/View;)V

    .line 43
    sget p1, Lcom/helpshift/R$id;->system_message:I

    invoke-virtual {p2, p1}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object p1

    check-cast p1, Landroid/widget/TextView;

    iput-object p1, p0, Lcom/helpshift/support/conversations/messages/SystemDateMessageDataBinder$ViewHolder;->body:Landroid/widget/TextView;

    return-void
.end method

.method static synthetic access$000(Lcom/helpshift/support/conversations/messages/SystemDateMessageDataBinder$ViewHolder;)Landroid/widget/TextView;
    .locals 0

    .line 38
    iget-object p0, p0, Lcom/helpshift/support/conversations/messages/SystemDateMessageDataBinder$ViewHolder;->body:Landroid/widget/TextView;

    return-object p0
.end method
