.class public Lcom/helpshift/support/fragments/FaqFlowFragment;
.super Lcom/helpshift/support/fragments/MainFragment;
.source "FaqFlowFragment.java"

# interfaces
.implements Lcom/helpshift/support/contracts/FaqFlowView;


# static fields
.field public static final FRAGMENT_TAG:Ljava/lang/String; = "Helpshift_FaqFlowFrag"


# instance fields
.field private customContactUsFlows:Ljava/util/List;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/List<",
            "Lcom/helpshift/support/flows/Flow;",
            ">;"
        }
    .end annotation
.end field

.field private faqFlowController:Lcom/helpshift/support/controllers/FaqFlowController;

.field private selectQuestionView:Landroid/view/View;

.field private verticalDivider:Landroid/view/View;


# direct methods
.method public constructor <init>()V
    .locals 0

    .line 20
    invoke-direct {p0}, Lcom/helpshift/support/fragments/MainFragment;-><init>()V

    return-void
.end method

.method public static newInstance(Landroid/os/Bundle;Ljava/util/List;)Lcom/helpshift/support/fragments/FaqFlowFragment;
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Landroid/os/Bundle;",
            "Ljava/util/List<",
            "Lcom/helpshift/support/flows/Flow;",
            ">;)",
            "Lcom/helpshift/support/fragments/FaqFlowFragment;"
        }
    .end annotation

    .line 29
    new-instance v0, Lcom/helpshift/support/fragments/FaqFlowFragment;

    invoke-direct {v0}, Lcom/helpshift/support/fragments/FaqFlowFragment;-><init>()V

    .line 30
    invoke-virtual {v0, p0}, Lcom/helpshift/support/fragments/FaqFlowFragment;->setArguments(Landroid/os/Bundle;)V

    .line 31
    iput-object p1, v0, Lcom/helpshift/support/fragments/FaqFlowFragment;->customContactUsFlows:Ljava/util/List;

    return-object v0
.end method


# virtual methods
.method public getCustomContactUsFlows()Ljava/util/List;
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()",
            "Ljava/util/List<",
            "Lcom/helpshift/support/flows/Flow;",
            ">;"
        }
    .end annotation

    .line 156
    iget-object v0, p0, Lcom/helpshift/support/fragments/FaqFlowFragment;->customContactUsFlows:Ljava/util/List;

    return-object v0
.end method

.method public getFaqFlowController()Lcom/helpshift/support/controllers/FaqFlowController;
    .locals 1

    .line 36
    iget-object v0, p0, Lcom/helpshift/support/fragments/FaqFlowFragment;->faqFlowController:Lcom/helpshift/support/controllers/FaqFlowController;

    return-object v0
.end method

.method public getFaqFlowListener()Lcom/helpshift/support/contracts/FaqFragmentListener;
    .locals 1

    .line 134
    invoke-virtual {p0}, Lcom/helpshift/support/fragments/FaqFlowFragment;->getFaqFlowController()Lcom/helpshift/support/controllers/FaqFlowController;

    move-result-object v0

    return-object v0
.end method

.method public getSupportFragment()Lcom/helpshift/support/fragments/SupportFragment;
    .locals 1

    .line 129
    invoke-virtual {p0}, Lcom/helpshift/support/fragments/FaqFlowFragment;->getParentFragment()Landroidx/fragment/app/Fragment;

    move-result-object v0

    check-cast v0, Lcom/helpshift/support/fragments/SupportFragment;

    return-object v0
.end method

.method public onAttach(Landroid/content/Context;)V
    .locals 3

    .line 41
    invoke-super {p0, p1}, Lcom/helpshift/support/fragments/MainFragment;->onAttach(Landroid/content/Context;)V

    .line 42
    iget-object v0, p0, Lcom/helpshift/support/fragments/FaqFlowFragment;->faqFlowController:Lcom/helpshift/support/controllers/FaqFlowController;

    if-nez v0, :cond_0

    .line 43
    new-instance v0, Lcom/helpshift/support/controllers/FaqFlowController;

    .line 45
    invoke-virtual {p0}, Lcom/helpshift/support/fragments/FaqFlowFragment;->getRetainedChildFragmentManager()Landroidx/fragment/app/FragmentManager;

    move-result-object v1

    .line 46
    invoke-virtual {p0}, Lcom/helpshift/support/fragments/FaqFlowFragment;->getArguments()Landroid/os/Bundle;

    move-result-object v2

    invoke-direct {v0, p0, p1, v1, v2}, Lcom/helpshift/support/controllers/FaqFlowController;-><init>(Lcom/helpshift/support/contracts/FaqFlowView;Landroid/content/Context;Landroidx/fragment/app/FragmentManager;Landroid/os/Bundle;)V

    iput-object v0, p0, Lcom/helpshift/support/fragments/FaqFlowFragment;->faqFlowController:Lcom/helpshift/support/controllers/FaqFlowController;

    goto :goto_0

    .line 49
    :cond_0
    iget-object p1, p0, Lcom/helpshift/support/fragments/FaqFlowFragment;->faqFlowController:Lcom/helpshift/support/controllers/FaqFlowController;

    invoke-virtual {p0}, Lcom/helpshift/support/fragments/FaqFlowFragment;->getRetainedChildFragmentManager()Landroidx/fragment/app/FragmentManager;

    move-result-object v0

    invoke-virtual {p1, v0}, Lcom/helpshift/support/controllers/FaqFlowController;->onFragmentManagerUpdate(Landroidx/fragment/app/FragmentManager;)V

    :goto_0
    return-void
.end method

.method public onCreateView(Landroid/view/LayoutInflater;Landroid/view/ViewGroup;Landroid/os/Bundle;)Landroid/view/View;
    .locals 1
    .annotation build Landroidx/annotation/Nullable;
    .end annotation

    .line 61
    sget p3, Lcom/helpshift/R$layout;->hs__faq_flow_fragment:I

    const/4 v0, 0x0

    invoke-virtual {p1, p3, p2, v0}, Landroid/view/LayoutInflater;->inflate(ILandroid/view/ViewGroup;Z)Landroid/view/View;

    move-result-object p1

    return-object p1
.end method

.method public onDestroy()V
    .locals 1

    .line 98
    invoke-super {p0}, Lcom/helpshift/support/fragments/MainFragment;->onDestroy()V

    const/4 v0, 0x0

    .line 99
    iput-object v0, p0, Lcom/helpshift/support/fragments/FaqFlowFragment;->faqFlowController:Lcom/helpshift/support/controllers/FaqFlowController;

    .line 100
    iput-object v0, p0, Lcom/helpshift/support/fragments/FaqFlowFragment;->verticalDivider:Landroid/view/View;

    .line 101
    iput-object v0, p0, Lcom/helpshift/support/fragments/FaqFlowFragment;->selectQuestionView:Landroid/view/View;

    .line 102
    invoke-virtual {p0}, Lcom/helpshift/support/fragments/FaqFlowFragment;->getSupportFragment()Lcom/helpshift/support/fragments/SupportFragment;

    move-result-object v0

    invoke-virtual {v0}, Lcom/helpshift/support/fragments/SupportFragment;->unRegisterSearchListener()V

    return-void
.end method

.method public onResume()V
    .locals 2

    .line 81
    invoke-super {p0}, Lcom/helpshift/support/fragments/MainFragment;->onResume()V

    .line 82
    iget-object v0, p0, Lcom/helpshift/support/fragments/FaqFlowFragment;->customContactUsFlows:Ljava/util/List;

    invoke-static {v0}, Lcom/helpshift/support/flows/CustomContactUsFlowListHolder;->setFlowList(Ljava/util/List;)V

    .line 83
    invoke-virtual {p0}, Lcom/helpshift/support/fragments/FaqFlowFragment;->getSupportFragment()Lcom/helpshift/support/fragments/SupportFragment;

    move-result-object v0

    iget-object v1, p0, Lcom/helpshift/support/fragments/FaqFlowFragment;->faqFlowController:Lcom/helpshift/support/controllers/FaqFlowController;

    invoke-virtual {v0, v1}, Lcom/helpshift/support/fragments/SupportFragment;->setSearchListeners(Lcom/helpshift/support/controllers/FaqFlowController;)V

    .line 84
    iget-object v0, p0, Lcom/helpshift/support/fragments/FaqFlowFragment;->faqFlowController:Lcom/helpshift/support/controllers/FaqFlowController;

    invoke-virtual {v0}, Lcom/helpshift/support/controllers/FaqFlowController;->start()V

    .line 85
    invoke-virtual {p0}, Lcom/helpshift/support/fragments/FaqFlowFragment;->updateSelectQuestionUI()V

    return-void
.end method

.method public onSaveInstanceState(Landroid/os/Bundle;)V
    .locals 1

    .line 90
    invoke-super {p0, p1}, Lcom/helpshift/support/fragments/MainFragment;->onSaveInstanceState(Landroid/os/Bundle;)V

    .line 91
    iget-object v0, p0, Lcom/helpshift/support/fragments/FaqFlowFragment;->faqFlowController:Lcom/helpshift/support/controllers/FaqFlowController;

    if-eqz v0, :cond_0

    .line 92
    iget-object v0, p0, Lcom/helpshift/support/fragments/FaqFlowFragment;->faqFlowController:Lcom/helpshift/support/controllers/FaqFlowController;

    invoke-virtual {v0, p1}, Lcom/helpshift/support/controllers/FaqFlowController;->onSaveInstanceState(Landroid/os/Bundle;)V

    :cond_0
    return-void
.end method

.method public onViewCreated(Landroid/view/View;Landroid/os/Bundle;)V
    .locals 0

    .line 66
    invoke-super {p0, p1, p2}, Lcom/helpshift/support/fragments/MainFragment;->onViewCreated(Landroid/view/View;Landroid/os/Bundle;)V

    .line 67
    sget p2, Lcom/helpshift/R$id;->vertical_divider:I

    invoke-virtual {p1, p2}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object p2

    iput-object p2, p0, Lcom/helpshift/support/fragments/FaqFlowFragment;->verticalDivider:Landroid/view/View;

    .line 68
    sget p2, Lcom/helpshift/R$id;->select_question_view:I

    invoke-virtual {p1, p2}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object p1

    iput-object p1, p0, Lcom/helpshift/support/fragments/FaqFlowFragment;->selectQuestionView:Landroid/view/View;

    return-void
.end method

.method public onViewStateRestored(Landroid/os/Bundle;)V
    .locals 1
    .param p1    # Landroid/os/Bundle;
        .annotation build Landroidx/annotation/Nullable;
        .end annotation
    .end param

    .line 73
    invoke-super {p0, p1}, Lcom/helpshift/support/fragments/MainFragment;->onViewStateRestored(Landroid/os/Bundle;)V

    if-eqz p1, :cond_0

    .line 74
    iget-object v0, p0, Lcom/helpshift/support/fragments/FaqFlowFragment;->faqFlowController:Lcom/helpshift/support/controllers/FaqFlowController;

    if-eqz v0, :cond_0

    .line 75
    iget-object v0, p0, Lcom/helpshift/support/fragments/FaqFlowFragment;->faqFlowController:Lcom/helpshift/support/controllers/FaqFlowController;

    invoke-virtual {v0, p1}, Lcom/helpshift/support/controllers/FaqFlowController;->onViewStateRestored(Landroid/os/Bundle;)V

    :cond_0
    return-void
.end method

.method public retryGetSections()V
    .locals 1

    .line 149
    invoke-virtual {p0}, Lcom/helpshift/support/fragments/FaqFlowFragment;->getRetainedChildFragmentManager()Landroidx/fragment/app/FragmentManager;

    move-result-object v0

    invoke-static {v0}, Lcom/helpshift/support/util/FragmentUtil;->getFaqFragment(Landroidx/fragment/app/FragmentManager;)Lcom/helpshift/support/compositions/FaqFragment;

    move-result-object v0

    if-eqz v0, :cond_0

    .line 151
    invoke-virtual {v0}, Lcom/helpshift/support/compositions/FaqFragment;->retryGetSections()V

    :cond_0
    return-void
.end method

.method public shouldRefreshMenu()Z
    .locals 1

    const/4 v0, 0x0

    return v0
.end method

.method public showVerticalDivider(Z)V
    .locals 1

    .line 138
    iget-object v0, p0, Lcom/helpshift/support/fragments/FaqFlowFragment;->verticalDivider:Landroid/view/View;

    if-eqz v0, :cond_1

    if-eqz p1, :cond_0

    .line 140
    iget-object p1, p0, Lcom/helpshift/support/fragments/FaqFlowFragment;->verticalDivider:Landroid/view/View;

    const/4 v0, 0x0

    invoke-virtual {p1, v0}, Landroid/view/View;->setVisibility(I)V

    goto :goto_0

    .line 143
    :cond_0
    iget-object p1, p0, Lcom/helpshift/support/fragments/FaqFlowFragment;->verticalDivider:Landroid/view/View;

    const/16 v0, 0x8

    invoke-virtual {p1, v0}, Landroid/view/View;->setVisibility(I)V

    :cond_1
    :goto_0
    return-void
.end method

.method public updateSelectQuestionUI()V
    .locals 2

    .line 106
    invoke-virtual {p0}, Lcom/helpshift/support/fragments/FaqFlowFragment;->isScreenLarge()Z

    move-result v0

    if-eqz v0, :cond_1

    iget-object v0, p0, Lcom/helpshift/support/fragments/FaqFlowFragment;->selectQuestionView:Landroid/view/View;

    if-eqz v0, :cond_1

    .line 107
    invoke-virtual {p0}, Lcom/helpshift/support/fragments/FaqFlowFragment;->getRetainedChildFragmentManager()Landroidx/fragment/app/FragmentManager;

    move-result-object v0

    sget v1, Lcom/helpshift/R$id;->details_fragment_container:I

    invoke-virtual {v0, v1}, Landroidx/fragment/app/FragmentManager;->findFragmentById(I)Landroidx/fragment/app/Fragment;

    move-result-object v0

    if-nez v0, :cond_0

    const/4 v0, 0x1

    .line 108
    invoke-virtual {p0, v0}, Lcom/helpshift/support/fragments/FaqFlowFragment;->updateSelectQuestionUI(Z)V

    goto :goto_0

    :cond_0
    const/4 v0, 0x0

    .line 111
    invoke-virtual {p0, v0}, Lcom/helpshift/support/fragments/FaqFlowFragment;->updateSelectQuestionUI(Z)V

    :cond_1
    :goto_0
    return-void
.end method

.method public updateSelectQuestionUI(Z)V
    .locals 1

    .line 117
    iget-object v0, p0, Lcom/helpshift/support/fragments/FaqFlowFragment;->selectQuestionView:Landroid/view/View;

    if-eqz v0, :cond_1

    if-eqz p1, :cond_0

    .line 119
    iget-object p1, p0, Lcom/helpshift/support/fragments/FaqFlowFragment;->selectQuestionView:Landroid/view/View;

    const/4 v0, 0x0

    invoke-virtual {p1, v0}, Landroid/view/View;->setVisibility(I)V

    goto :goto_0

    .line 122
    :cond_0
    iget-object p1, p0, Lcom/helpshift/support/fragments/FaqFlowFragment;->selectQuestionView:Landroid/view/View;

    const/16 v0, 0x8

    invoke-virtual {p1, v0}, Landroid/view/View;->setVisibility(I)V

    :cond_1
    :goto_0
    return-void
.end method
