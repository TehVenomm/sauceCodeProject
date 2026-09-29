.class public final Lcom/helpshift/support/conversations/messages/UserSelectableOptionViewDataBinder$ViewHolder;
.super Landroidx/recyclerview/widget/RecyclerView$ViewHolder;
.source "UserSelectableOptionViewDataBinder.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lcom/helpshift/support/conversations/messages/UserSelectableOptionViewDataBinder;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x14
    name = "ViewHolder"
.end annotation


# instance fields
.field final optionsContainer:Landroid/widget/LinearLayout;

.field final optionsHeaderTextView:Landroid/widget/TextView;

.field final optionsMessageView:Landroid/widget/LinearLayout;

.field final optionsSkipTextView:Landroid/widget/TextView;

.field final synthetic this$0:Lcom/helpshift/support/conversations/messages/UserSelectableOptionViewDataBinder;


# direct methods
.method constructor <init>(Lcom/helpshift/support/conversations/messages/UserSelectableOptionViewDataBinder;Landroid/view/View;)V
    .locals 0

    .line 105
    iput-object p1, p0, Lcom/helpshift/support/conversations/messages/UserSelectableOptionViewDataBinder$ViewHolder;->this$0:Lcom/helpshift/support/conversations/messages/UserSelectableOptionViewDataBinder;

    .line 106
    invoke-direct {p0, p2}, Landroidx/recyclerview/widget/RecyclerView$ViewHolder;-><init>(Landroid/view/View;)V

    .line 107
    sget p1, Lcom/helpshift/R$id;->options_message_view:I

    invoke-virtual {p2, p1}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object p1

    check-cast p1, Landroid/widget/LinearLayout;

    iput-object p1, p0, Lcom/helpshift/support/conversations/messages/UserSelectableOptionViewDataBinder$ViewHolder;->optionsMessageView:Landroid/widget/LinearLayout;

    .line 108
    sget p1, Lcom/helpshift/R$id;->selectable_options_container:I

    invoke-virtual {p2, p1}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object p1

    check-cast p1, Landroid/widget/LinearLayout;

    iput-object p1, p0, Lcom/helpshift/support/conversations/messages/UserSelectableOptionViewDataBinder$ViewHolder;->optionsContainer:Landroid/widget/LinearLayout;

    .line 109
    sget p1, Lcom/helpshift/R$id;->options_header:I

    invoke-virtual {p2, p1}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object p1

    check-cast p1, Landroid/widget/TextView;

    iput-object p1, p0, Lcom/helpshift/support/conversations/messages/UserSelectableOptionViewDataBinder$ViewHolder;->optionsHeaderTextView:Landroid/widget/TextView;

    .line 110
    sget p1, Lcom/helpshift/R$id;->selectable_option_skip:I

    invoke-virtual {p2, p1}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object p1

    check-cast p1, Landroid/widget/TextView;

    iput-object p1, p0, Lcom/helpshift/support/conversations/messages/UserSelectableOptionViewDataBinder$ViewHolder;->optionsSkipTextView:Landroid/widget/TextView;

    return-void
.end method
