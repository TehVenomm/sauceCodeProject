.class public Lcom/helpshift/support/fragments/SearchResultFragment;
.super Lcom/helpshift/support/fragments/MainFragment;
.source "SearchResultFragment.java"


# static fields
.field public static final BUNDLE_ARG_SEARCH_RESULTS:Ljava/lang/String; = "search_fragment_results"

.field public static final FRAGMENT_TAG:Ljava/lang/String; = "HSSearchResultFragment"


# instance fields
.field private onQuestionClickedListener:Landroid/view/View$OnClickListener;

.field searchResultList:Landroidx/recyclerview/widget/RecyclerView;

.field searchResultListener:Lcom/helpshift/support/contracts/SearchResultListener;

.field private sendAnywayClickedListener:Landroid/view/View$OnClickListener;


# direct methods
.method public constructor <init>()V
    .locals 0

    .line 18
    invoke-direct {p0}, Lcom/helpshift/support/fragments/MainFragment;-><init>()V

    return-void
.end method

.method public static newInstance(Landroid/os/Bundle;Lcom/helpshift/support/contracts/SearchResultListener;)Lcom/helpshift/support/fragments/SearchResultFragment;
    .locals 1

    .line 28
    new-instance v0, Lcom/helpshift/support/fragments/SearchResultFragment;

    invoke-direct {v0}, Lcom/helpshift/support/fragments/SearchResultFragment;-><init>()V

    .line 29
    invoke-virtual {v0, p0}, Lcom/helpshift/support/fragments/SearchResultFragment;->setArguments(Landroid/os/Bundle;)V

    .line 30
    iput-object p1, v0, Lcom/helpshift/support/fragments/SearchResultFragment;->searchResultListener:Lcom/helpshift/support/contracts/SearchResultListener;

    return-object v0
.end method

.method private showResults()V
    .locals 4

    .line 79
    invoke-virtual {p0}, Lcom/helpshift/support/fragments/SearchResultFragment;->getArguments()Landroid/os/Bundle;

    move-result-object v0

    const-string v1, "search_fragment_results"

    invoke-virtual {v0, v1}, Landroid/os/Bundle;->getParcelableArrayList(Ljava/lang/String;)Ljava/util/ArrayList;

    move-result-object v0

    if-eqz v0, :cond_0

    .line 80
    invoke-interface {v0}, Ljava/util/List;->size()I

    move-result v1

    const/4 v2, 0x3

    if-le v1, v2, :cond_0

    const/4 v1, 0x0

    .line 81
    invoke-interface {v0, v1, v2}, Ljava/util/List;->subList(II)Ljava/util/List;

    move-result-object v0

    .line 83
    :cond_0
    new-instance v1, Lcom/helpshift/support/adapters/SearchResultAdapter;

    iget-object v2, p0, Lcom/helpshift/support/fragments/SearchResultFragment;->onQuestionClickedListener:Landroid/view/View$OnClickListener;

    iget-object v3, p0, Lcom/helpshift/support/fragments/SearchResultFragment;->sendAnywayClickedListener:Landroid/view/View$OnClickListener;

    invoke-direct {v1, v0, v2, v3}, Lcom/helpshift/support/adapters/SearchResultAdapter;-><init>(Ljava/util/List;Landroid/view/View$OnClickListener;Landroid/view/View$OnClickListener;)V

    .line 86
    iget-object v0, p0, Lcom/helpshift/support/fragments/SearchResultFragment;->searchResultList:Landroidx/recyclerview/widget/RecyclerView;

    invoke-virtual {v0, v1}, Landroidx/recyclerview/widget/RecyclerView;->setAdapter(Landroidx/recyclerview/widget/RecyclerView$Adapter;)V

    return-void
.end method


# virtual methods
.method public onCreateView(Landroid/view/LayoutInflater;Landroid/view/ViewGroup;Landroid/os/Bundle;)Landroid/view/View;
    .locals 1
    .annotation build Landroidx/annotation/Nullable;
    .end annotation

    .line 41
    sget p3, Lcom/helpshift/R$layout;->hs__search_result_fragment:I

    const/4 v0, 0x0

    invoke-virtual {p1, p3, p2, v0}, Landroid/view/LayoutInflater;->inflate(ILandroid/view/ViewGroup;Z)Landroid/view/View;

    move-result-object p1

    return-object p1
.end method

.method public onResume()V
    .locals 1

    .line 72
    invoke-super {p0}, Lcom/helpshift/support/fragments/MainFragment;->onResume()V

    .line 73
    sget v0, Lcom/helpshift/R$string;->hs__search_result_title:I

    invoke-virtual {p0, v0}, Lcom/helpshift/support/fragments/SearchResultFragment;->getString(I)Ljava/lang/String;

    move-result-object v0

    invoke-virtual {p0, v0}, Lcom/helpshift/support/fragments/SearchResultFragment;->setToolbarTitle(Ljava/lang/String;)V

    .line 74
    invoke-direct {p0}, Lcom/helpshift/support/fragments/SearchResultFragment;->showResults()V

    return-void
.end method

.method public onViewCreated(Landroid/view/View;Landroid/os/Bundle;)V
    .locals 1

    .line 46
    invoke-super {p0, p1, p2}, Lcom/helpshift/support/fragments/MainFragment;->onViewCreated(Landroid/view/View;Landroid/os/Bundle;)V

    .line 48
    sget p2, Lcom/helpshift/R$id;->search_result:I

    invoke-virtual {p1, p2}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object p2

    check-cast p2, Landroidx/recyclerview/widget/RecyclerView;

    iput-object p2, p0, Lcom/helpshift/support/fragments/SearchResultFragment;->searchResultList:Landroidx/recyclerview/widget/RecyclerView;

    .line 49
    iget-object p2, p0, Lcom/helpshift/support/fragments/SearchResultFragment;->searchResultList:Landroidx/recyclerview/widget/RecyclerView;

    new-instance v0, Landroidx/recyclerview/widget/LinearLayoutManager;

    invoke-virtual {p1}, Landroid/view/View;->getContext()Landroid/content/Context;

    move-result-object p1

    invoke-direct {v0, p1}, Landroidx/recyclerview/widget/LinearLayoutManager;-><init>(Landroid/content/Context;)V

    invoke-virtual {p2, v0}, Landroidx/recyclerview/widget/RecyclerView;->setLayoutManager(Landroidx/recyclerview/widget/RecyclerView$LayoutManager;)V

    .line 50
    new-instance p1, Lcom/helpshift/support/fragments/SearchResultFragment$1;

    invoke-direct {p1, p0}, Lcom/helpshift/support/fragments/SearchResultFragment$1;-><init>(Lcom/helpshift/support/fragments/SearchResultFragment;)V

    iput-object p1, p0, Lcom/helpshift/support/fragments/SearchResultFragment;->onQuestionClickedListener:Landroid/view/View$OnClickListener;

    .line 62
    new-instance p1, Lcom/helpshift/support/fragments/SearchResultFragment$2;

    invoke-direct {p1, p0}, Lcom/helpshift/support/fragments/SearchResultFragment$2;-><init>(Lcom/helpshift/support/fragments/SearchResultFragment;)V

    iput-object p1, p0, Lcom/helpshift/support/fragments/SearchResultFragment;->sendAnywayClickedListener:Landroid/view/View$OnClickListener;

    return-void
.end method

.method public setSearchResultListener(Lcom/helpshift/support/contracts/SearchResultListener;)V
    .locals 0

    .line 35
    iput-object p1, p0, Lcom/helpshift/support/fragments/SearchResultFragment;->searchResultListener:Lcom/helpshift/support/contracts/SearchResultListener;

    return-void
.end method

.method public shouldRefreshMenu()Z
    .locals 1

    const/4 v0, 0x1

    return v0
.end method
