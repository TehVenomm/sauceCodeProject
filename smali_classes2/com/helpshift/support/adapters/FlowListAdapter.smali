.class public Lcom/helpshift/support/adapters/FlowListAdapter;
.super Landroidx/recyclerview/widget/RecyclerView$Adapter;
.source "FlowListAdapter.java"


# annotations
.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Lcom/helpshift/support/adapters/FlowListAdapter$ViewHolder;
    }
.end annotation

.annotation system Ldalvik/annotation/Signature;
    value = {
        "Landroidx/recyclerview/widget/RecyclerView$Adapter<",
        "Lcom/helpshift/support/adapters/FlowListAdapter$ViewHolder;",
        ">;"
    }
.end annotation


# instance fields
.field private flows:Ljava/util/List;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/List<",
            "Lcom/helpshift/support/flows/Flow;",
            ">;"
        }
    .end annotation
.end field

.field private onClickListener:Landroid/view/View$OnClickListener;


# direct methods
.method public constructor <init>(Ljava/util/List;Landroid/view/View$OnClickListener;)V
    .locals 0
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/List<",
            "Lcom/helpshift/support/flows/Flow;",
            ">;",
            "Landroid/view/View$OnClickListener;",
            ")V"
        }
    .end annotation

    .line 18
    invoke-direct {p0}, Landroidx/recyclerview/widget/RecyclerView$Adapter;-><init>()V

    .line 19
    iput-object p1, p0, Lcom/helpshift/support/adapters/FlowListAdapter;->flows:Ljava/util/List;

    .line 20
    iput-object p2, p0, Lcom/helpshift/support/adapters/FlowListAdapter;->onClickListener:Landroid/view/View$OnClickListener;

    return-void
.end method


# virtual methods
.method public getItemCount()I
    .locals 1

    .line 49
    iget-object v0, p0, Lcom/helpshift/support/adapters/FlowListAdapter;->flows:Ljava/util/List;

    invoke-interface {v0}, Ljava/util/List;->size()I

    move-result v0

    return v0
.end method

.method public bridge synthetic onBindViewHolder(Landroidx/recyclerview/widget/RecyclerView$ViewHolder;I)V
    .locals 0

    .line 13
    check-cast p1, Lcom/helpshift/support/adapters/FlowListAdapter$ViewHolder;

    invoke-virtual {p0, p1, p2}, Lcom/helpshift/support/adapters/FlowListAdapter;->onBindViewHolder(Lcom/helpshift/support/adapters/FlowListAdapter$ViewHolder;I)V

    return-void
.end method

.method public onBindViewHolder(Lcom/helpshift/support/adapters/FlowListAdapter$ViewHolder;I)V
    .locals 2

    .line 34
    iget-object v0, p0, Lcom/helpshift/support/adapters/FlowListAdapter;->flows:Ljava/util/List;

    invoke-interface {v0, p2}, Ljava/util/List;->get(I)Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Lcom/helpshift/support/flows/Flow;

    .line 36
    invoke-interface {v0}, Lcom/helpshift/support/flows/Flow;->getLabelResId()I

    move-result v1

    if-eqz v1, :cond_0

    .line 38
    iget-object v1, p1, Lcom/helpshift/support/adapters/FlowListAdapter$ViewHolder;->textView:Landroid/widget/TextView;

    invoke-virtual {v1}, Landroid/widget/TextView;->getResources()Landroid/content/res/Resources;

    move-result-object v1

    invoke-interface {v0}, Lcom/helpshift/support/flows/Flow;->getLabelResId()I

    move-result v0

    invoke-virtual {v1, v0}, Landroid/content/res/Resources;->getString(I)Ljava/lang/String;

    move-result-object v0

    goto :goto_0

    .line 41
    :cond_0
    invoke-interface {v0}, Lcom/helpshift/support/flows/Flow;->getLabel()Ljava/lang/String;

    move-result-object v0

    .line 43
    :goto_0
    iget-object v1, p1, Lcom/helpshift/support/adapters/FlowListAdapter$ViewHolder;->textView:Landroid/widget/TextView;

    invoke-virtual {v1, v0}, Landroid/widget/TextView;->setText(Ljava/lang/CharSequence;)V

    .line 44
    iget-object p1, p1, Lcom/helpshift/support/adapters/FlowListAdapter$ViewHolder;->textView:Landroid/widget/TextView;

    invoke-static {p2}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object p2

    invoke-virtual {p1, p2}, Landroid/widget/TextView;->setTag(Ljava/lang/Object;)V

    return-void
.end method

.method public bridge synthetic onCreateViewHolder(Landroid/view/ViewGroup;I)Landroidx/recyclerview/widget/RecyclerView$ViewHolder;
    .locals 0

    .line 13
    invoke-virtual {p0, p1, p2}, Lcom/helpshift/support/adapters/FlowListAdapter;->onCreateViewHolder(Landroid/view/ViewGroup;I)Lcom/helpshift/support/adapters/FlowListAdapter$ViewHolder;

    move-result-object p1

    return-object p1
.end method

.method public onCreateViewHolder(Landroid/view/ViewGroup;I)Lcom/helpshift/support/adapters/FlowListAdapter$ViewHolder;
    .locals 2

    .line 26
    invoke-virtual {p1}, Landroid/view/ViewGroup;->getContext()Landroid/content/Context;

    move-result-object p2

    invoke-static {p2}, Landroid/view/LayoutInflater;->from(Landroid/content/Context;)Landroid/view/LayoutInflater;

    move-result-object p2

    sget v0, Lcom/helpshift/R$layout;->hs__simple_list_item_1:I

    const/4 v1, 0x0

    .line 27
    invoke-virtual {p2, v0, p1, v1}, Landroid/view/LayoutInflater;->inflate(ILandroid/view/ViewGroup;Z)Landroid/view/View;

    move-result-object p1

    check-cast p1, Landroid/widget/TextView;

    .line 28
    iget-object p2, p0, Lcom/helpshift/support/adapters/FlowListAdapter;->onClickListener:Landroid/view/View$OnClickListener;

    invoke-virtual {p1, p2}, Landroid/widget/TextView;->setOnClickListener(Landroid/view/View$OnClickListener;)V

    .line 29
    new-instance p2, Lcom/helpshift/support/adapters/FlowListAdapter$ViewHolder;

    invoke-direct {p2, p1}, Lcom/helpshift/support/adapters/FlowListAdapter$ViewHolder;-><init>(Landroid/widget/TextView;)V

    return-object p2
.end method
