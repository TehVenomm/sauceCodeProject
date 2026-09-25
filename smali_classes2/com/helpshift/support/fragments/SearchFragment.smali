.class public Lcom/helpshift/support/fragments/SearchFragment;
.super Lcom/helpshift/support/fragments/MainFragment;
.source "SearchFragment.java"


# annotations
.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Lcom/helpshift/support/fragments/SearchFragment$SearchResultFetcherTask;
    }
.end annotation


# static fields
.field private static final KEY_SEARCH_QUERY:Ljava/lang/String; = "key_search_query"

.field public static final TAG:Ljava/lang/String; = "Helpshift_SearchFrag"


# instance fields
.field currentQuery:Ljava/lang/String;

.field data:Lcom/helpshift/support/HSApiData;

.field faqTagFilter:Lcom/helpshift/support/FaqTagFilter;

.field private onContactUsClickedListener:Landroid/view/View$OnClickListener;

.field private onQuestionClickedListener:Landroid/view/View$OnClickListener;

.field searchList:Landroidx/recyclerview/widget/RecyclerView;

.field private final searchResultSuccessHandler:Landroid/os/Handler;

.field private sectionId:Ljava/lang/String;


# direct methods
.method public constructor <init>()V
    .locals 1

    .line 32
    invoke-direct {p0}, Lcom/helpshift/support/fragments/MainFragment;-><init>()V

    .line 43
    new-instance v0, Lcom/helpshift/support/fragments/SearchFragment$1;

    invoke-direct {v0, p0}, Lcom/helpshift/support/fragments/SearchFragment$1;-><init>(Lcom/helpshift/support/fragments/SearchFragment;)V

    iput-object v0, p0, Lcom/helpshift/support/fragments/SearchFragment;->searchResultSuccessHandler:Landroid/os/Handler;

    return-void
.end method

.method public static newInstance(Landroid/os/Bundle;)Lcom/helpshift/support/fragments/SearchFragment;
    .locals 1

    .line 64
    new-instance v0, Lcom/helpshift/support/fragments/SearchFragment;

    invoke-direct {v0}, Lcom/helpshift/support/fragments/SearchFragment;-><init>()V

    .line 65
    invoke-virtual {v0, p0}, Lcom/helpshift/support/fragments/SearchFragment;->setArguments(Landroid/os/Bundle;)V

    return-object v0
.end method


# virtual methods
.method public getCurrentQuery()Ljava/lang/String;
    .locals 1

    .line 74
    iget-object v0, p0, Lcom/helpshift/support/fragments/SearchFragment;->currentQuery:Ljava/lang/String;

    return-object v0
.end method

.method public getFaqFlowListener()Lcom/helpshift/support/contracts/FaqFragmentListener;
    .locals 1

    .line 70
    invoke-virtual {p0}, Lcom/helpshift/support/fragments/SearchFragment;->getParentFragment()Landroidx/fragment/app/Fragment;

    move-result-object v0

    check-cast v0, Lcom/helpshift/support/contracts/FaqFlowViewParent;

    invoke-interface {v0}, Lcom/helpshift/support/contracts/FaqFlowViewParent;->getFaqFlowListener()Lcom/helpshift/support/contracts/FaqFragmentListener;

    move-result-object v0

    return-object v0
.end method

.method public getNumberOfSearchResults()I
    .locals 2

    .line 175
    iget-object v0, p0, Lcom/helpshift/support/fragments/SearchFragment;->searchList:Landroidx/recyclerview/widget/RecyclerView;

    if-eqz v0, :cond_0

    .line 177
    iget-object v0, p0, Lcom/helpshift/support/fragments/SearchFragment;->searchList:Landroidx/recyclerview/widget/RecyclerView;

    invoke-virtual {v0}, Landroidx/recyclerview/widget/RecyclerView;->getAdapter()Landroidx/recyclerview/widget/RecyclerView$Adapter;

    move-result-object v0

    check-cast v0, Lcom/helpshift/support/adapters/SearchListAdapter;

    if-eqz v0, :cond_0

    .line 179
    invoke-virtual {v0}, Lcom/helpshift/support/adapters/SearchListAdapter;->getItemCount()I

    move-result v1

    invoke-virtual {v0}, Lcom/helpshift/support/adapters/SearchListAdapter;->getFooterCount()I

    move-result v0

    sub-int/2addr v1, v0

    goto :goto_0

    :cond_0
    const/4 v1, -0x1

    :goto_0
    return v1
.end method

.method public onAttach(Landroid/content/Context;)V
    .locals 1

    .line 123
    invoke-super {p0, p1}, Lcom/helpshift/support/fragments/MainFragment;->onAttach(Landroid/content/Context;)V

    .line 124
    new-instance v0, Lcom/helpshift/support/HSApiData;

    invoke-direct {v0, p1}, Lcom/helpshift/support/HSApiData;-><init>(Landroid/content/Context;)V

    iput-object v0, p0, Lcom/helpshift/support/fragments/SearchFragment;->data:Lcom/helpshift/support/HSApiData;

    .line 125
    iget-object p1, p0, Lcom/helpshift/support/fragments/SearchFragment;->data:Lcom/helpshift/support/HSApiData;

    invoke-virtual {p1}, Lcom/helpshift/support/HSApiData;->loadIndex()V

    return-void
.end method

.method public onCreate(Landroid/os/Bundle;)V
    .locals 1

    .line 79
    invoke-super {p0, p1}, Lcom/helpshift/support/fragments/MainFragment;->onCreate(Landroid/os/Bundle;)V

    .line 80
    invoke-virtual {p0}, Lcom/helpshift/support/fragments/SearchFragment;->getArguments()Landroid/os/Bundle;

    move-result-object p1

    if-eqz p1, :cond_0

    const-string v0, "withTagsMatching"

    .line 82
    invoke-virtual {p1, v0}, Landroid/os/Bundle;->getSerializable(Ljava/lang/String;)Ljava/io/Serializable;

    move-result-object p1

    check-cast p1, Lcom/helpshift/support/FaqTagFilter;

    iput-object p1, p0, Lcom/helpshift/support/fragments/SearchFragment;->faqTagFilter:Lcom/helpshift/support/FaqTagFilter;

    :cond_0
    return-void
.end method

.method public onCreateView(Landroid/view/LayoutInflater;Landroid/view/ViewGroup;Landroid/os/Bundle;)Landroid/view/View;
    .locals 1
    .annotation build Landroidx/annotation/Nullable;
    .end annotation

    .line 89
    sget p3, Lcom/helpshift/R$layout;->hs__search_fragment:I

    const/4 v0, 0x0

    invoke-virtual {p1, p3, p2, v0}, Landroid/view/LayoutInflater;->inflate(ILandroid/view/ViewGroup;Z)Landroid/view/View;

    move-result-object p1

    return-object p1
.end method

.method public onDestroyView()V
    .locals 2

    .line 132
    iget-object v0, p0, Lcom/helpshift/support/fragments/SearchFragment;->searchList:Landroidx/recyclerview/widget/RecyclerView;

    const/4 v1, 0x0

    invoke-virtual {v0, v1}, Landroidx/recyclerview/widget/RecyclerView;->setAdapter(Landroidx/recyclerview/widget/RecyclerView$Adapter;)V

    .line 133
    iput-object v1, p0, Lcom/helpshift/support/fragments/SearchFragment;->searchList:Landroidx/recyclerview/widget/RecyclerView;

    .line 134
    invoke-super {p0}, Lcom/helpshift/support/fragments/MainFragment;->onDestroyView()V

    return-void
.end method

.method public onQuery(Ljava/lang/String;Ljava/lang/String;)V
    .locals 7

    .line 144
    iput-object p2, p0, Lcom/helpshift/support/fragments/SearchFragment;->sectionId:Ljava/lang/String;

    .line 146
    iget-object v0, p0, Lcom/helpshift/support/fragments/SearchFragment;->searchList:Landroidx/recyclerview/widget/RecyclerView;

    if-nez v0, :cond_0

    return-void

    :cond_0
    const/4 v0, 0x0

    .line 152
    invoke-static {}, Lcom/helpshift/util/HelpshiftContext;->getCoreApi()Lcom/helpshift/CoreApi;

    move-result-object v1

    invoke-interface {v1}, Lcom/helpshift/CoreApi;->getSDKConfigurationDM()Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;

    move-result-object v1

    const-string v2, "sdkLanguage"

    invoke-virtual {v1, v2}, Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v1

    .line 153
    invoke-static {v1}, Landroid/text/TextUtils;->isEmpty(Ljava/lang/CharSequence;)Z

    move-result v2

    if-eqz v2, :cond_1

    .line 154
    invoke-static {}, Ljava/util/Locale;->getDefault()Ljava/util/Locale;

    move-result-object v1

    invoke-virtual {v1}, Ljava/util/Locale;->getLanguage()Ljava/lang/String;

    move-result-object v1

    :cond_1
    const-string v2, "zh"

    .line 156
    invoke-virtual {v1, v2}, Ljava/lang/String;->startsWith(Ljava/lang/String;)Z

    move-result v2

    if-nez v2, :cond_3

    const-string v2, "ja"

    invoke-virtual {v1, v2}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v2

    if-nez v2, :cond_3

    const-string v2, "ko"

    invoke-virtual {v1, v2}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v1

    if-eqz v1, :cond_2

    goto :goto_0

    :cond_2
    const/4 v4, 0x0

    goto :goto_1

    :cond_3
    :goto_0
    const/4 v0, 0x1

    const/4 v4, 0x1

    :goto_1
    if-nez p1, :cond_4

    const-string p1, ""

    :goto_2
    move-object v3, p1

    goto :goto_3

    .line 164
    :cond_4
    invoke-virtual {p1}, Ljava/lang/String;->trim()Ljava/lang/String;

    move-result-object p1

    goto :goto_2

    .line 166
    :goto_3
    iput-object v3, p0, Lcom/helpshift/support/fragments/SearchFragment;->currentQuery:Ljava/lang/String;

    .line 167
    new-instance p1, Lcom/helpshift/support/fragments/SearchFragment$SearchResultFetcherTask;

    iget-object v6, p0, Lcom/helpshift/support/fragments/SearchFragment;->searchResultSuccessHandler:Landroid/os/Handler;

    move-object v1, p1

    move-object v2, p0

    move-object v5, p2

    invoke-direct/range {v1 .. v6}, Lcom/helpshift/support/fragments/SearchFragment$SearchResultFetcherTask;-><init>(Lcom/helpshift/support/fragments/SearchFragment;Ljava/lang/String;ZLjava/lang/String;Landroid/os/Handler;)V

    .line 168
    new-instance p2, Ljava/lang/Thread;

    const-string v0, "HS-search-query"

    invoke-direct {p2, p1, v0}, Ljava/lang/Thread;-><init>(Ljava/lang/Runnable;Ljava/lang/String;)V

    .line 169
    invoke-virtual {p2}, Ljava/lang/Thread;->start()V

    const-string p1, "Helpshift_SearchFrag"

    .line 170
    new-instance p2, Ljava/lang/StringBuilder;

    invoke-direct {p2}, Ljava/lang/StringBuilder;-><init>()V

    const-string v0, "Performing search : Query : "

    invoke-virtual {p2, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v0, p0, Lcom/helpshift/support/fragments/SearchFragment;->currentQuery:Ljava/lang/String;

    invoke-virtual {p2, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p2}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p2

    invoke-static {p1, p2}, Lcom/helpshift/util/HSLogger;->d(Ljava/lang/String;Ljava/lang/String;)V

    return-void
.end method

.method public onViewCreated(Landroid/view/View;Landroid/os/Bundle;)V
    .locals 1

    .line 94
    invoke-super {p0, p1, p2}, Lcom/helpshift/support/fragments/MainFragment;->onViewCreated(Landroid/view/View;Landroid/os/Bundle;)V

    .line 95
    sget p2, Lcom/helpshift/R$id;->search_list:I

    invoke-virtual {p1, p2}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object p2

    check-cast p2, Landroidx/recyclerview/widget/RecyclerView;

    iput-object p2, p0, Lcom/helpshift/support/fragments/SearchFragment;->searchList:Landroidx/recyclerview/widget/RecyclerView;

    .line 96
    iget-object p2, p0, Lcom/helpshift/support/fragments/SearchFragment;->searchList:Landroidx/recyclerview/widget/RecyclerView;

    new-instance v0, Landroidx/recyclerview/widget/LinearLayoutManager;

    invoke-virtual {p1}, Landroid/view/View;->getContext()Landroid/content/Context;

    move-result-object p1

    invoke-direct {v0, p1}, Landroidx/recyclerview/widget/LinearLayoutManager;-><init>(Landroid/content/Context;)V

    invoke-virtual {p2, v0}, Landroidx/recyclerview/widget/RecyclerView;->setLayoutManager(Landroidx/recyclerview/widget/RecyclerView$LayoutManager;)V

    .line 97
    new-instance p1, Lcom/helpshift/support/fragments/SearchFragment$2;

    invoke-direct {p1, p0}, Lcom/helpshift/support/fragments/SearchFragment$2;-><init>(Lcom/helpshift/support/fragments/SearchFragment;)V

    iput-object p1, p0, Lcom/helpshift/support/fragments/SearchFragment;->onQuestionClickedListener:Landroid/view/View$OnClickListener;

    .line 109
    new-instance p1, Lcom/helpshift/support/fragments/SearchFragment$3;

    invoke-direct {p1, p0}, Lcom/helpshift/support/fragments/SearchFragment$3;-><init>(Lcom/helpshift/support/fragments/SearchFragment;)V

    iput-object p1, p0, Lcom/helpshift/support/fragments/SearchFragment;->onContactUsClickedListener:Landroid/view/View$OnClickListener;

    .line 115
    invoke-virtual {p0}, Lcom/helpshift/support/fragments/SearchFragment;->getArguments()Landroid/os/Bundle;

    move-result-object p1

    if-eqz p1, :cond_0

    .line 116
    invoke-virtual {p0}, Lcom/helpshift/support/fragments/SearchFragment;->getArguments()Landroid/os/Bundle;

    move-result-object p1

    const-string p2, "sectionPublishId"

    invoke-virtual {p1, p2}, Landroid/os/Bundle;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object p1

    iput-object p1, p0, Lcom/helpshift/support/fragments/SearchFragment;->sectionId:Ljava/lang/String;

    .line 118
    :cond_0
    iget-object p1, p0, Lcom/helpshift/support/fragments/SearchFragment;->currentQuery:Ljava/lang/String;

    iget-object p2, p0, Lcom/helpshift/support/fragments/SearchFragment;->sectionId:Ljava/lang/String;

    invoke-virtual {p0, p1, p2}, Lcom/helpshift/support/fragments/SearchFragment;->onQuery(Ljava/lang/String;Ljava/lang/String;)V

    return-void
.end method

.method public shouldRefreshMenu()Z
    .locals 1

    const/4 v0, 0x1

    return v0
.end method

.method updateSearchResult(Ljava/util/List;)V
    .locals 6
    .param p1    # Ljava/util/List;
        .annotation build Landroidx/annotation/NonNull;
        .end annotation
    .end param
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/List<",
            "Lcom/helpshift/support/Faq;",
            ">;)V"
        }
    .end annotation

    .line 187
    iget-object v0, p0, Lcom/helpshift/support/fragments/SearchFragment;->searchList:Landroidx/recyclerview/widget/RecyclerView;

    if-nez v0, :cond_0

    return-void

    .line 192
    :cond_0
    new-instance v0, Lcom/helpshift/support/adapters/SearchListAdapter;

    iget-object v1, p0, Lcom/helpshift/support/fragments/SearchFragment;->currentQuery:Ljava/lang/String;

    iget-object v2, p0, Lcom/helpshift/support/fragments/SearchFragment;->onQuestionClickedListener:Landroid/view/View$OnClickListener;

    iget-object v3, p0, Lcom/helpshift/support/fragments/SearchFragment;->onContactUsClickedListener:Landroid/view/View$OnClickListener;

    invoke-direct {v0, v1, p1, v2, v3}, Lcom/helpshift/support/adapters/SearchListAdapter;-><init>(Ljava/lang/String;Ljava/util/List;Landroid/view/View$OnClickListener;Landroid/view/View$OnClickListener;)V

    const/4 v1, 0x1

    .line 196
    invoke-virtual {v0, v1}, Lcom/helpshift/support/adapters/SearchListAdapter;->setHasStableIds(Z)V

    .line 197
    iget-object v2, p0, Lcom/helpshift/support/fragments/SearchFragment;->searchList:Landroidx/recyclerview/widget/RecyclerView;

    invoke-virtual {v2}, Landroidx/recyclerview/widget/RecyclerView;->getAdapter()Landroidx/recyclerview/widget/RecyclerView$Adapter;

    move-result-object v2

    if-nez v2, :cond_1

    .line 198
    iget-object p1, p0, Lcom/helpshift/support/fragments/SearchFragment;->searchList:Landroidx/recyclerview/widget/RecyclerView;

    invoke-virtual {p1, v0}, Landroidx/recyclerview/widget/RecyclerView;->setAdapter(Landroidx/recyclerview/widget/RecyclerView$Adapter;)V

    goto :goto_0

    .line 201
    :cond_1
    iget-object v0, p0, Lcom/helpshift/support/fragments/SearchFragment;->searchList:Landroidx/recyclerview/widget/RecyclerView;

    new-instance v2, Lcom/helpshift/support/adapters/SearchListAdapter;

    iget-object v3, p0, Lcom/helpshift/support/fragments/SearchFragment;->currentQuery:Ljava/lang/String;

    iget-object v4, p0, Lcom/helpshift/support/fragments/SearchFragment;->onQuestionClickedListener:Landroid/view/View$OnClickListener;

    iget-object v5, p0, Lcom/helpshift/support/fragments/SearchFragment;->onContactUsClickedListener:Landroid/view/View$OnClickListener;

    invoke-direct {v2, v3, p1, v4, v5}, Lcom/helpshift/support/adapters/SearchListAdapter;-><init>(Ljava/lang/String;Ljava/util/List;Landroid/view/View$OnClickListener;Landroid/view/View$OnClickListener;)V

    invoke-virtual {v0, v2, v1}, Landroidx/recyclerview/widget/RecyclerView;->swapAdapter(Landroidx/recyclerview/widget/RecyclerView$Adapter;Z)V

    :goto_0
    return-void
.end method
