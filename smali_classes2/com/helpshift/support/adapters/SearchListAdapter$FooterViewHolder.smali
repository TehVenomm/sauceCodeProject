.class Lcom/helpshift/support/adapters/SearchListAdapter$FooterViewHolder;
.super Landroidx/recyclerview/widget/RecyclerView$ViewHolder;
.source "SearchListAdapter.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lcom/helpshift/support/adapters/SearchListAdapter;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0xa
    name = "FooterViewHolder"
.end annotation


# instance fields
.field button:Landroid/widget/Button;

.field contactUsHintText:Landroid/widget/TextView;

.field contactUsView:Landroid/widget/LinearLayout;

.field divider:Landroid/view/View;

.field noFaqsView:Landroid/widget/TextView;


# direct methods
.method constructor <init>(Landroid/widget/LinearLayout;)V
    .locals 1

    .line 237
    invoke-direct {p0, p1}, Landroidx/recyclerview/widget/RecyclerView$ViewHolder;-><init>(Landroid/view/View;)V

    .line 238
    sget v0, Lcom/helpshift/R$id;->contact_us_view:I

    invoke-virtual {p1, v0}, Landroid/widget/LinearLayout;->findViewById(I)Landroid/view/View;

    move-result-object v0

    check-cast v0, Landroid/widget/LinearLayout;

    iput-object v0, p0, Lcom/helpshift/support/adapters/SearchListAdapter$FooterViewHolder;->contactUsView:Landroid/widget/LinearLayout;

    .line 239
    sget v0, Lcom/helpshift/R$id;->contact_us_hint_text:I

    invoke-virtual {p1, v0}, Landroid/widget/LinearLayout;->findViewById(I)Landroid/view/View;

    move-result-object v0

    check-cast v0, Landroid/widget/TextView;

    iput-object v0, p0, Lcom/helpshift/support/adapters/SearchListAdapter$FooterViewHolder;->contactUsHintText:Landroid/widget/TextView;

    .line 240
    sget v0, Lcom/helpshift/R$id;->report_issue:I

    invoke-virtual {p1, v0}, Landroid/widget/LinearLayout;->findViewById(I)Landroid/view/View;

    move-result-object v0

    check-cast v0, Landroid/widget/Button;

    iput-object v0, p0, Lcom/helpshift/support/adapters/SearchListAdapter$FooterViewHolder;->button:Landroid/widget/Button;

    .line 241
    sget v0, Lcom/helpshift/R$id;->no_faqs_view:I

    invoke-virtual {p1, v0}, Landroid/widget/LinearLayout;->findViewById(I)Landroid/view/View;

    move-result-object v0

    check-cast v0, Landroid/widget/TextView;

    iput-object v0, p0, Lcom/helpshift/support/adapters/SearchListAdapter$FooterViewHolder;->noFaqsView:Landroid/widget/TextView;

    .line 242
    sget v0, Lcom/helpshift/R$id;->search_list_footer_divider:I

    invoke-virtual {p1, v0}, Landroid/widget/LinearLayout;->findViewById(I)Landroid/view/View;

    move-result-object p1

    iput-object p1, p0, Lcom/helpshift/support/adapters/SearchListAdapter$FooterViewHolder;->divider:Landroid/view/View;

    return-void
.end method
