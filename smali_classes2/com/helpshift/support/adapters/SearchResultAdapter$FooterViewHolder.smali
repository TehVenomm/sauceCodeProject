.class public Lcom/helpshift/support/adapters/SearchResultAdapter$FooterViewHolder;
.super Landroidx/recyclerview/widget/RecyclerView$ViewHolder;
.source "SearchResultAdapter.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lcom/helpshift/support/adapters/SearchResultAdapter;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0xc
    name = "FooterViewHolder"
.end annotation


# instance fields
.field button:Landroid/widget/Button;

.field rootView:Landroid/widget/LinearLayout;


# direct methods
.method public constructor <init>(Landroid/widget/LinearLayout;)V
    .locals 1

    .line 214
    invoke-direct {p0, p1}, Landroidx/recyclerview/widget/RecyclerView$ViewHolder;-><init>(Landroid/view/View;)V

    .line 215
    iput-object p1, p0, Lcom/helpshift/support/adapters/SearchResultAdapter$FooterViewHolder;->rootView:Landroid/widget/LinearLayout;

    .line 216
    sget v0, Lcom/helpshift/R$id;->send_anyway_button:I

    invoke-virtual {p1, v0}, Landroid/widget/LinearLayout;->findViewById(I)Landroid/view/View;

    move-result-object p1

    check-cast p1, Landroid/widget/Button;

    iput-object p1, p0, Lcom/helpshift/support/adapters/SearchResultAdapter$FooterViewHolder;->button:Landroid/widget/Button;

    return-void
.end method
