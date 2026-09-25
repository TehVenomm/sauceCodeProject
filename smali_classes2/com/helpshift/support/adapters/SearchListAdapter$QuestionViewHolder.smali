.class Lcom/helpshift/support/adapters/SearchListAdapter$QuestionViewHolder;
.super Landroidx/recyclerview/widget/RecyclerView$ViewHolder;
.source "SearchListAdapter.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lcom/helpshift/support/adapters/SearchListAdapter;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0xa
    name = "QuestionViewHolder"
.end annotation


# instance fields
.field textView:Landroid/widget/TextView;


# direct methods
.method public constructor <init>(Landroid/widget/TextView;)V
    .locals 0

    .line 224
    invoke-direct {p0, p1}, Landroidx/recyclerview/widget/RecyclerView$ViewHolder;-><init>(Landroid/view/View;)V

    .line 225
    iput-object p1, p0, Lcom/helpshift/support/adapters/SearchListAdapter$QuestionViewHolder;->textView:Landroid/widget/TextView;

    return-void
.end method
