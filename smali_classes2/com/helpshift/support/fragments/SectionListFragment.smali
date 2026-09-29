.class public Lcom/helpshift/support/fragments/SectionListFragment;
.super Lcom/helpshift/support/fragments/MainFragment;
.source "SectionListFragment.java"


# instance fields
.field private sectionList:Landroidx/recyclerview/widget/RecyclerView;


# direct methods
.method public constructor <init>()V
    .locals 0

    .line 19
    invoke-direct {p0}, Lcom/helpshift/support/fragments/MainFragment;-><init>()V

    return-void
.end method

.method public static newInstance(Landroid/os/Bundle;)Lcom/helpshift/support/fragments/SectionListFragment;
    .locals 1
    .param p0    # Landroid/os/Bundle;
        .annotation build Landroidx/annotation/Nullable;
        .end annotation
    .end param

    .line 24
    new-instance v0, Lcom/helpshift/support/fragments/SectionListFragment;

    invoke-direct {v0}, Lcom/helpshift/support/fragments/SectionListFragment;-><init>()V

    .line 25
    invoke-virtual {v0, p0}, Lcom/helpshift/support/fragments/SectionListFragment;->setArguments(Landroid/os/Bundle;)V

    return-object v0
.end method


# virtual methods
.method public getFaqFlowListener()Lcom/helpshift/support/contracts/FaqFragmentListener;
    .locals 1

    .line 30
    invoke-virtual {p0}, Lcom/helpshift/support/fragments/SectionListFragment;->getParentFragment()Landroidx/fragment/app/Fragment;

    move-result-object v0

    check-cast v0, Lcom/helpshift/support/contracts/FaqFlowViewParent;

    invoke-interface {v0}, Lcom/helpshift/support/contracts/FaqFlowViewParent;->getFaqFlowListener()Lcom/helpshift/support/contracts/FaqFragmentListener;

    move-result-object v0

    return-object v0
.end method

.method public onCreateView(Landroid/view/LayoutInflater;Landroid/view/ViewGroup;Landroid/os/Bundle;)Landroid/view/View;
    .locals 1
    .annotation build Landroidx/annotation/Nullable;
    .end annotation

    .line 36
    sget p3, Lcom/helpshift/R$layout;->hs__section_list_fragment:I

    const/4 v0, 0x0

    invoke-virtual {p1, p3, p2, v0}, Landroid/view/LayoutInflater;->inflate(ILandroid/view/ViewGroup;Z)Landroid/view/View;

    move-result-object p1

    return-object p1
.end method

.method public onDestroyView()V
    .locals 2

    .line 72
    iget-object v0, p0, Lcom/helpshift/support/fragments/SectionListFragment;->sectionList:Landroidx/recyclerview/widget/RecyclerView;

    const/4 v1, 0x0

    invoke-virtual {v0, v1}, Landroidx/recyclerview/widget/RecyclerView;->setAdapter(Landroidx/recyclerview/widget/RecyclerView$Adapter;)V

    .line 73
    iput-object v1, p0, Lcom/helpshift/support/fragments/SectionListFragment;->sectionList:Landroidx/recyclerview/widget/RecyclerView;

    .line 74
    invoke-super {p0}, Lcom/helpshift/support/fragments/MainFragment;->onDestroyView()V

    return-void
.end method

.method public onViewCreated(Landroid/view/View;Landroid/os/Bundle;)V
    .locals 3

    .line 41
    invoke-super {p0, p1, p2}, Lcom/helpshift/support/fragments/MainFragment;->onViewCreated(Landroid/view/View;Landroid/os/Bundle;)V

    .line 43
    invoke-virtual {p0}, Lcom/helpshift/support/fragments/SectionListFragment;->getArguments()Landroid/os/Bundle;

    move-result-object p2

    const-string v0, "sections"

    invoke-virtual {p2, v0}, Landroid/os/Bundle;->getParcelableArrayList(Ljava/lang/String;)Ljava/util/ArrayList;

    move-result-object p2

    .line 44
    invoke-virtual {p0}, Lcom/helpshift/support/fragments/SectionListFragment;->getArguments()Landroid/os/Bundle;

    move-result-object v0

    const-string v1, "withTagsMatching"

    invoke-virtual {v0, v1}, Landroid/os/Bundle;->getSerializable(Ljava/lang/String;)Ljava/io/Serializable;

    move-result-object v0

    check-cast v0, Lcom/helpshift/support/FaqTagFilter;

    .line 46
    sget v1, Lcom/helpshift/R$id;->section_list:I

    invoke-virtual {p1, v1}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object v1

    check-cast v1, Landroidx/recyclerview/widget/RecyclerView;

    iput-object v1, p0, Lcom/helpshift/support/fragments/SectionListFragment;->sectionList:Landroidx/recyclerview/widget/RecyclerView;

    .line 47
    iget-object v1, p0, Lcom/helpshift/support/fragments/SectionListFragment;->sectionList:Landroidx/recyclerview/widget/RecyclerView;

    new-instance v2, Landroidx/recyclerview/widget/LinearLayoutManager;

    invoke-virtual {p1}, Landroid/view/View;->getContext()Landroid/content/Context;

    move-result-object p1

    invoke-direct {v2, p1}, Landroidx/recyclerview/widget/LinearLayoutManager;-><init>(Landroid/content/Context;)V

    invoke-virtual {v1, v2}, Landroidx/recyclerview/widget/RecyclerView;->setLayoutManager(Landroidx/recyclerview/widget/RecyclerView$LayoutManager;)V

    .line 48
    new-instance p1, Lcom/helpshift/support/fragments/SectionListFragment$1;

    invoke-direct {p1, p0, p2, v0}, Lcom/helpshift/support/fragments/SectionListFragment$1;-><init>(Lcom/helpshift/support/fragments/SectionListFragment;Ljava/util/ArrayList;Lcom/helpshift/support/FaqTagFilter;)V

    .line 59
    iget-object v0, p0, Lcom/helpshift/support/fragments/SectionListFragment;->sectionList:Landroidx/recyclerview/widget/RecyclerView;

    new-instance v1, Lcom/helpshift/support/adapters/SectionListAdapter;

    invoke-direct {v1, p2, p1}, Lcom/helpshift/support/adapters/SectionListAdapter;-><init>(Ljava/util/List;Landroid/view/View$OnClickListener;)V

    invoke-virtual {v0, v1}, Landroidx/recyclerview/widget/RecyclerView;->setAdapter(Landroidx/recyclerview/widget/RecyclerView$Adapter;)V

    return-void
.end method

.method public shouldRefreshMenu()Z
    .locals 1

    const/4 v0, 0x0

    return v0
.end method
