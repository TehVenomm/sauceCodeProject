.class public Lcom/helpshift/support/compositions/FaqFragment;
.super Lcom/helpshift/support/fragments/MainFragment;
.source "FaqFragment.java"

# interfaces
.implements Lcom/helpshift/support/contracts/FaqFlowViewParent;


# annotations
.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Lcom/helpshift/support/compositions/FaqFragment$Failure;,
        Lcom/helpshift/support/compositions/FaqFragment$Success;,
        Lcom/helpshift/support/compositions/FaqFragment$FaqLoadingStatus;
    }
.end annotation


# static fields
.field private static final TAG:Ljava/lang/String; = "Helpshift_FaqFragment"


# instance fields
.field private data:Lcom/helpshift/support/HSApiData;

.field private faqTagFilter:Lcom/helpshift/support/FaqTagFilter;

.field isDataUpdatePending:Z

.field sectionsSize:I


# direct methods
.method public constructor <init>()V
    .locals 1

    .line 37
    invoke-direct {p0}, Lcom/helpshift/support/fragments/MainFragment;-><init>()V

    const/4 v0, 0x0

    .line 40
    iput v0, p0, Lcom/helpshift/support/compositions/FaqFragment;->sectionsSize:I

    return-void
.end method

.method public static newInstance(Landroid/os/Bundle;)Lcom/helpshift/support/compositions/FaqFragment;
    .locals 1

    .line 46
    new-instance v0, Lcom/helpshift/support/compositions/FaqFragment;

    invoke-direct {v0}, Lcom/helpshift/support/compositions/FaqFragment;-><init>()V

    .line 47
    invoke-virtual {v0, p0}, Lcom/helpshift/support/compositions/FaqFragment;->setArguments(Landroid/os/Bundle;)V

    return-object v0
.end method

.method private sendFaqLoadedEvent()V
    .locals 1

    .line 125
    invoke-static {p0}, Lcom/helpshift/support/util/FragmentUtil;->getSupportFragment(Landroidx/fragment/app/Fragment;)Lcom/helpshift/support/fragments/SupportFragment;

    move-result-object v0

    if-eqz v0, :cond_0

    .line 127
    invoke-virtual {v0}, Lcom/helpshift/support/fragments/SupportFragment;->onFaqsLoaded()V

    :cond_0
    return-void
.end method


# virtual methods
.method public getFaqFlowListener()Lcom/helpshift/support/contracts/FaqFragmentListener;
    .locals 1

    .line 53
    invoke-virtual {p0}, Lcom/helpshift/support/compositions/FaqFragment;->getParentFragment()Landroidx/fragment/app/Fragment;

    move-result-object v0

    check-cast v0, Lcom/helpshift/support/contracts/FaqFlowViewParent;

    invoke-interface {v0}, Lcom/helpshift/support/contracts/FaqFlowViewParent;->getFaqFlowListener()Lcom/helpshift/support/contracts/FaqFragmentListener;

    move-result-object v0

    return-object v0
.end method

.method public onAttach(Landroid/content/Context;)V
    .locals 1

    .line 186
    invoke-super {p0, p1}, Lcom/helpshift/support/fragments/MainFragment;->onAttach(Landroid/content/Context;)V

    .line 187
    new-instance v0, Lcom/helpshift/support/HSApiData;

    invoke-direct {v0, p1}, Lcom/helpshift/support/HSApiData;-><init>(Landroid/content/Context;)V

    iput-object v0, p0, Lcom/helpshift/support/compositions/FaqFragment;->data:Lcom/helpshift/support/HSApiData;

    return-void
.end method

.method public onCreate(Landroid/os/Bundle;)V
    .locals 1

    .line 152
    invoke-super {p0, p1}, Lcom/helpshift/support/fragments/MainFragment;->onCreate(Landroid/os/Bundle;)V

    .line 153
    invoke-virtual {p0}, Lcom/helpshift/support/compositions/FaqFragment;->getArguments()Landroid/os/Bundle;

    move-result-object p1

    if-eqz p1, :cond_0

    const-string v0, "withTagsMatching"

    .line 155
    invoke-virtual {p1, v0}, Landroid/os/Bundle;->getSerializable(Ljava/lang/String;)Ljava/io/Serializable;

    move-result-object p1

    check-cast p1, Lcom/helpshift/support/FaqTagFilter;

    iput-object p1, p0, Lcom/helpshift/support/compositions/FaqFragment;->faqTagFilter:Lcom/helpshift/support/FaqTagFilter;

    :cond_0
    return-void
.end method

.method public onCreateView(Landroid/view/LayoutInflater;Landroid/view/ViewGroup;Landroid/os/Bundle;)Landroid/view/View;
    .locals 1
    .annotation build Landroidx/annotation/Nullable;
    .end annotation

    .line 162
    sget p3, Lcom/helpshift/R$layout;->hs__faq_fragment:I

    const/4 v0, 0x0

    invoke-virtual {p1, p3, p2, v0}, Landroid/view/LayoutInflater;->inflate(ILandroid/view/ViewGroup;Z)Landroid/view/View;

    move-result-object p1

    return-object p1
.end method

.method public onDestroyView()V
    .locals 1

    .line 180
    invoke-virtual {p0}, Lcom/helpshift/support/compositions/FaqFragment;->getView()Landroid/view/View;

    move-result-object v0

    invoke-static {v0}, Lcom/helpshift/support/util/SnackbarUtil;->hideSnackbar(Landroid/view/View;)V

    .line 181
    invoke-super {p0}, Lcom/helpshift/support/fragments/MainFragment;->onDestroyView()V

    return-void
.end method

.method public onResume()V
    .locals 4

    .line 167
    invoke-super {p0}, Lcom/helpshift/support/fragments/MainFragment;->onResume()V

    .line 168
    sget v0, Lcom/helpshift/R$string;->hs__help_header:I

    invoke-virtual {p0, v0}, Lcom/helpshift/support/compositions/FaqFragment;->getString(I)Ljava/lang/String;

    move-result-object v0

    invoke-virtual {p0, v0}, Lcom/helpshift/support/compositions/FaqFragment;->setToolbarTitle(Ljava/lang/String;)V

    .line 169
    iget v0, p0, Lcom/helpshift/support/compositions/FaqFragment;->sectionsSize:I

    if-nez v0, :cond_0

    const/4 v0, 0x0

    .line 170
    invoke-virtual {p0, v0}, Lcom/helpshift/support/compositions/FaqFragment;->updateFaqLoadingUI(I)V

    .line 172
    :cond_0
    iget-object v0, p0, Lcom/helpshift/support/compositions/FaqFragment;->data:Lcom/helpshift/support/HSApiData;

    new-instance v1, Lcom/helpshift/support/compositions/FaqFragment$Success;

    invoke-direct {v1, p0}, Lcom/helpshift/support/compositions/FaqFragment$Success;-><init>(Lcom/helpshift/support/compositions/FaqFragment;)V

    new-instance v2, Lcom/helpshift/support/compositions/FaqFragment$Failure;

    invoke-direct {v2, p0}, Lcom/helpshift/support/compositions/FaqFragment$Failure;-><init>(Lcom/helpshift/support/compositions/FaqFragment;)V

    iget-object v3, p0, Lcom/helpshift/support/compositions/FaqFragment;->faqTagFilter:Lcom/helpshift/support/FaqTagFilter;

    invoke-virtual {v0, v1, v2, v3}, Lcom/helpshift/support/HSApiData;->getSections(Landroid/os/Handler;Landroid/os/Handler;Lcom/helpshift/support/FaqTagFilter;)V

    .line 173
    invoke-virtual {p0}, Lcom/helpshift/support/compositions/FaqFragment;->isChangingConfigurations()Z

    move-result v0

    if-nez v0, :cond_1

    .line 174
    invoke-static {}, Lcom/helpshift/util/HelpshiftContext;->getCoreApi()Lcom/helpshift/CoreApi;

    move-result-object v0

    invoke-interface {v0}, Lcom/helpshift/CoreApi;->getAnalyticsEventDM()Lcom/helpshift/analytics/domainmodel/AnalyticsEventDM;

    move-result-object v0

    sget-object v1, Lcom/helpshift/analytics/AnalyticsEventType;->SUPPORT_LAUNCH:Lcom/helpshift/analytics/AnalyticsEventType;

    invoke-virtual {v0, v1}, Lcom/helpshift/analytics/domainmodel/AnalyticsEventDM;->pushEvent(Lcom/helpshift/analytics/AnalyticsEventType;)V

    :cond_1
    return-void
.end method

.method public onStop()V
    .locals 1

    .line 192
    invoke-super {p0}, Lcom/helpshift/support/fragments/MainFragment;->onStop()V

    const/4 v0, 0x1

    .line 193
    invoke-virtual {p0, v0}, Lcom/helpshift/support/compositions/FaqFragment;->updateFaqLoadingUI(I)V

    return-void
.end method

.method removeEmptySections(Ljava/util/ArrayList;)Ljava/util/ArrayList;
    .locals 5
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/ArrayList<",
            "Lcom/helpshift/support/Section;",
            ">;)",
            "Ljava/util/ArrayList<",
            "Lcom/helpshift/support/Section;",
            ">;"
        }
    .end annotation

    .line 209
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    .line 210
    invoke-virtual {p1}, Ljava/util/ArrayList;->iterator()Ljava/util/Iterator;

    move-result-object p1

    :cond_0
    :goto_0
    invoke-interface {p1}, Ljava/util/Iterator;->hasNext()Z

    move-result v1

    if-eqz v1, :cond_1

    invoke-interface {p1}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Lcom/helpshift/support/Section;

    .line 211
    iget-object v2, p0, Lcom/helpshift/support/compositions/FaqFragment;->data:Lcom/helpshift/support/HSApiData;

    invoke-virtual {v1}, Lcom/helpshift/support/Section;->getPublishId()Ljava/lang/String;

    move-result-object v3

    iget-object v4, p0, Lcom/helpshift/support/compositions/FaqFragment;->faqTagFilter:Lcom/helpshift/support/FaqTagFilter;

    invoke-virtual {v2, v3, v4}, Lcom/helpshift/support/HSApiData;->getFaqsForSection(Ljava/lang/String;Lcom/helpshift/support/FaqTagFilter;)Ljava/util/ArrayList;

    move-result-object v2

    if-eqz v2, :cond_0

    .line 212
    invoke-virtual {v2}, Ljava/util/ArrayList;->isEmpty()Z

    move-result v2

    if-nez v2, :cond_0

    .line 213
    invoke-virtual {v0, v1}, Ljava/util/ArrayList;->add(Ljava/lang/Object;)Z

    goto :goto_0

    :cond_1
    return-object v0
.end method

.method public retryGetSections()V
    .locals 4

    .line 202
    iget v0, p0, Lcom/helpshift/support/compositions/FaqFragment;->sectionsSize:I

    if-nez v0, :cond_0

    const/4 v0, 0x0

    .line 203
    invoke-virtual {p0, v0}, Lcom/helpshift/support/compositions/FaqFragment;->updateFaqLoadingUI(I)V

    .line 205
    :cond_0
    iget-object v0, p0, Lcom/helpshift/support/compositions/FaqFragment;->data:Lcom/helpshift/support/HSApiData;

    new-instance v1, Lcom/helpshift/support/compositions/FaqFragment$Success;

    invoke-direct {v1, p0}, Lcom/helpshift/support/compositions/FaqFragment$Success;-><init>(Lcom/helpshift/support/compositions/FaqFragment;)V

    new-instance v2, Lcom/helpshift/support/compositions/FaqFragment$Failure;

    invoke-direct {v2, p0}, Lcom/helpshift/support/compositions/FaqFragment$Failure;-><init>(Lcom/helpshift/support/compositions/FaqFragment;)V

    iget-object v3, p0, Lcom/helpshift/support/compositions/FaqFragment;->faqTagFilter:Lcom/helpshift/support/FaqTagFilter;

    invoke-virtual {v0, v1, v2, v3}, Lcom/helpshift/support/HSApiData;->getSections(Landroid/os/Handler;Landroid/os/Handler;Lcom/helpshift/support/FaqTagFilter;)V

    return-void
.end method

.method public shouldRefreshMenu()Z
    .locals 1

    const/4 v0, 0x1

    return v0
.end method

.method public updateFaqLoadingUI(I)V
    .locals 3

    .line 132
    invoke-virtual {p0}, Lcom/helpshift/support/compositions/FaqFragment;->getParentFragment()Landroidx/fragment/app/Fragment;

    move-result-object v0

    check-cast v0, Lcom/helpshift/support/fragments/FaqFlowFragment;

    if-eqz v0, :cond_0

    .line 135
    invoke-virtual {v0}, Lcom/helpshift/support/fragments/FaqFlowFragment;->getParentFragment()Landroidx/fragment/app/Fragment;

    move-result-object v1

    check-cast v1, Lcom/helpshift/support/fragments/SupportFragment;

    goto :goto_0

    :cond_0
    const/4 v1, 0x0

    :goto_0
    if-eqz v1, :cond_2

    const/4 v2, 0x1

    if-ne p1, v2, :cond_1

    .line 139
    invoke-virtual {v0, v2}, Lcom/helpshift/support/fragments/FaqFlowFragment;->showVerticalDivider(Z)V

    .line 140
    invoke-virtual {v0}, Lcom/helpshift/support/fragments/FaqFlowFragment;->updateSelectQuestionUI()V

    goto :goto_1

    :cond_1
    const/4 v2, 0x0

    .line 143
    invoke-virtual {v0, v2}, Lcom/helpshift/support/fragments/FaqFlowFragment;->showVerticalDivider(Z)V

    .line 144
    invoke-virtual {v0, v2}, Lcom/helpshift/support/fragments/FaqFlowFragment;->updateSelectQuestionUI(Z)V

    .line 146
    :goto_1
    invoke-virtual {v1, p1}, Lcom/helpshift/support/fragments/SupportFragment;->updateFaqLoadingUI(I)V

    :cond_2
    return-void
.end method

.method updateFragment(Lcom/helpshift/support/compositions/FaqFragment;Ljava/util/ArrayList;)V
    .locals 10
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Lcom/helpshift/support/compositions/FaqFragment;",
            "Ljava/util/ArrayList<",
            "Lcom/helpshift/support/Section;",
            ">;)V"
        }
    .end annotation

    .line 59
    invoke-direct {p0}, Lcom/helpshift/support/compositions/FaqFragment;->sendFaqLoadedEvent()V

    .line 65
    invoke-virtual {p1}, Lcom/helpshift/support/compositions/FaqFragment;->getRetainedChildFragmentManager()Landroidx/fragment/app/FragmentManager;

    move-result-object v0

    sget v1, Lcom/helpshift/R$id;->faq_fragment_container:I

    .line 66
    invoke-virtual {v0, v1}, Landroidx/fragment/app/FragmentManager;->findFragmentById(I)Landroidx/fragment/app/Fragment;

    move-result-object v0

    if-eqz v0, :cond_0

    .line 67
    iget-boolean v0, p0, Lcom/helpshift/support/compositions/FaqFragment;->isDataUpdatePending:Z

    if-nez v0, :cond_0

    return-void

    .line 71
    :cond_0
    iget-object v0, p1, Lcom/helpshift/support/compositions/FaqFragment;->data:Lcom/helpshift/support/HSApiData;

    iget-object v1, p1, Lcom/helpshift/support/compositions/FaqFragment;->faqTagFilter:Lcom/helpshift/support/FaqTagFilter;

    invoke-virtual {v0, p2, v1}, Lcom/helpshift/support/HSApiData;->getPopulatedSections(Ljava/util/ArrayList;Lcom/helpshift/support/FaqTagFilter;)Ljava/util/ArrayList;

    move-result-object p2

    .line 72
    invoke-virtual {p2}, Ljava/util/ArrayList;->size()I

    move-result v0

    const/4 v1, 0x1

    const/4 v2, 0x0

    if-ne v0, v1, :cond_1

    .line 73
    new-instance v0, Landroid/os/Bundle;

    invoke-direct {v0}, Landroid/os/Bundle;-><init>()V

    const-string v1, "sectionPublishId"

    .line 74
    invoke-virtual {p2, v2}, Ljava/util/ArrayList;->get(I)Ljava/lang/Object;

    move-result-object p2

    check-cast p2, Lcom/helpshift/support/Section;

    invoke-virtual {p2}, Lcom/helpshift/support/Section;->getPublishId()Ljava/lang/String;

    move-result-object p2

    invoke-virtual {v0, v1, p2}, Landroid/os/Bundle;->putString(Ljava/lang/String;Ljava/lang/String;)V

    const-string p2, "withTagsMatching"

    .line 75
    invoke-virtual {p0}, Lcom/helpshift/support/compositions/FaqFragment;->getArguments()Landroid/os/Bundle;

    move-result-object v1

    const-string v3, "withTagsMatching"

    invoke-virtual {v1, v3}, Landroid/os/Bundle;->getSerializable(Ljava/lang/String;)Ljava/io/Serializable;

    move-result-object v1

    invoke-virtual {v0, p2, v1}, Landroid/os/Bundle;->putSerializable(Ljava/lang/String;Ljava/io/Serializable;)V

    .line 77
    invoke-static {v0}, Lcom/helpshift/support/fragments/QuestionListFragment;->newInstance(Landroid/os/Bundle;)Lcom/helpshift/support/fragments/QuestionListFragment;

    move-result-object v5

    .line 79
    :try_start_0
    invoke-virtual {p1}, Lcom/helpshift/support/compositions/FaqFragment;->getRetainedChildFragmentManager()Landroidx/fragment/app/FragmentManager;

    move-result-object v3

    sget v4, Lcom/helpshift/R$id;->faq_fragment_container:I

    const/4 v6, 0x0

    const/4 v7, 0x0

    const/4 v8, 0x0

    iget-boolean v9, p0, Lcom/helpshift/support/compositions/FaqFragment;->isDataUpdatePending:Z

    invoke-static/range {v3 .. v9}, Lcom/helpshift/support/util/FragmentUtil;->startFragment(Landroidx/fragment/app/FragmentManager;ILandroidx/fragment/app/Fragment;Ljava/lang/String;Ljava/lang/String;ZZ)V

    .line 86
    iput-boolean v2, p0, Lcom/helpshift/support/compositions/FaqFragment;->isDataUpdatePending:Z
    :try_end_0
    .catch Ljava/lang/IllegalStateException; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    .line 98
    :cond_1
    new-instance v0, Landroid/os/Bundle;

    invoke-direct {v0}, Landroid/os/Bundle;-><init>()V

    const-string v1, "sections"

    .line 99
    invoke-virtual {v0, v1, p2}, Landroid/os/Bundle;->putParcelableArrayList(Ljava/lang/String;Ljava/util/ArrayList;)V

    const-string p2, "withTagsMatching"

    .line 100
    invoke-virtual {p0}, Lcom/helpshift/support/compositions/FaqFragment;->getArguments()Landroid/os/Bundle;

    move-result-object v1

    const-string v3, "withTagsMatching"

    invoke-virtual {v1, v3}, Landroid/os/Bundle;->getSerializable(Ljava/lang/String;)Ljava/io/Serializable;

    move-result-object v1

    invoke-virtual {v0, p2, v1}, Landroid/os/Bundle;->putSerializable(Ljava/lang/String;Ljava/io/Serializable;)V

    .line 102
    invoke-static {v0}, Lcom/helpshift/support/fragments/SectionListFragment;->newInstance(Landroid/os/Bundle;)Lcom/helpshift/support/fragments/SectionListFragment;

    move-result-object v5

    .line 104
    :try_start_1
    invoke-virtual {p1}, Lcom/helpshift/support/compositions/FaqFragment;->getRetainedChildFragmentManager()Landroidx/fragment/app/FragmentManager;

    move-result-object v3

    sget v4, Lcom/helpshift/R$id;->faq_fragment_container:I

    const/4 v6, 0x0

    const/4 v7, 0x0

    const/4 v8, 0x0

    iget-boolean v9, p0, Lcom/helpshift/support/compositions/FaqFragment;->isDataUpdatePending:Z

    invoke-static/range {v3 .. v9}, Lcom/helpshift/support/util/FragmentUtil;->startFragment(Landroidx/fragment/app/FragmentManager;ILandroidx/fragment/app/Fragment;Ljava/lang/String;Ljava/lang/String;ZZ)V

    .line 111
    iput-boolean v2, p0, Lcom/helpshift/support/compositions/FaqFragment;->isDataUpdatePending:Z
    :try_end_1
    .catch Ljava/lang/IllegalStateException; {:try_start_1 .. :try_end_1} :catch_0

    :catch_0
    :goto_0
    return-void
.end method
