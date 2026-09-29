.class public Lcom/helpshift/support/controllers/FaqFlowController;
.super Ljava/lang/Object;
.source "FaqFlowController.java"

# interfaces
.implements Lcom/helpshift/support/contracts/FaqFragmentListener;
.implements Landroidx/core/view/MenuItemCompat$OnActionExpandListener;
.implements Landroid/view/MenuItem$OnActionExpandListener;
.implements Landroidx/appcompat/widget/SearchView$OnQueryTextListener;


# instance fields
.field private final KEY_FAQ_CONTROLLER_STARTED_STATE:Ljava/lang/String;

.field private final bundle:Landroid/os/Bundle;

.field private currentQuery:Ljava/lang/String;

.field private final faqFlowView:Lcom/helpshift/support/contracts/FaqFlowView;

.field private fragmentManager:Landroidx/fragment/app/FragmentManager;

.field private isControllerStarted:Z

.field private final isScreenLarge:Z

.field private lastQuery:Ljava/lang/String;

.field private retainSearchFragmentState:Z


# direct methods
.method public constructor <init>(Lcom/helpshift/support/contracts/FaqFlowView;Landroid/content/Context;Landroidx/fragment/app/FragmentManager;Landroid/os/Bundle;)V
    .locals 1

    .line 57
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    const-string v0, "key_faq_controller_state"

    .line 44
    iput-object v0, p0, Lcom/helpshift/support/controllers/FaqFlowController;->KEY_FAQ_CONTROLLER_STARTED_STATE:Ljava/lang/String;

    const-string v0, ""

    .line 51
    iput-object v0, p0, Lcom/helpshift/support/controllers/FaqFlowController;->currentQuery:Ljava/lang/String;

    const-string v0, ""

    .line 52
    iput-object v0, p0, Lcom/helpshift/support/controllers/FaqFlowController;->lastQuery:Ljava/lang/String;

    .line 58
    iput-object p1, p0, Lcom/helpshift/support/controllers/FaqFlowController;->faqFlowView:Lcom/helpshift/support/contracts/FaqFlowView;

    .line 59
    invoke-static {p2}, Lcom/helpshift/support/util/Styles;->isTablet(Landroid/content/Context;)Z

    move-result p1

    iput-boolean p1, p0, Lcom/helpshift/support/controllers/FaqFlowController;->isScreenLarge:Z

    .line 60
    iput-object p3, p0, Lcom/helpshift/support/controllers/FaqFlowController;->fragmentManager:Landroidx/fragment/app/FragmentManager;

    .line 61
    iput-object p4, p0, Lcom/helpshift/support/controllers/FaqFlowController;->bundle:Landroid/os/Bundle;

    return-void
.end method

.method private onQuery(Ljava/lang/String;)Z
    .locals 3

    .line 230
    iget-boolean v0, p0, Lcom/helpshift/support/controllers/FaqFlowController;->retainSearchFragmentState:Z

    if-nez v0, :cond_0

    .line 231
    iget-object v0, p0, Lcom/helpshift/support/controllers/FaqFlowController;->fragmentManager:Landroidx/fragment/app/FragmentManager;

    const-string v1, "Helpshift_SearchFrag"

    invoke-virtual {v0, v1}, Landroidx/fragment/app/FragmentManager;->findFragmentByTag(Ljava/lang/String;)Landroidx/fragment/app/Fragment;

    move-result-object v0

    check-cast v0, Lcom/helpshift/support/fragments/SearchFragment;

    if-eqz v0, :cond_0

    .line 233
    iget-object v1, p0, Lcom/helpshift/support/controllers/FaqFlowController;->bundle:Landroid/os/Bundle;

    const-string v2, "sectionPublishId"

    invoke-virtual {v1, v2}, Landroid/os/Bundle;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, p1, v1}, Lcom/helpshift/support/fragments/SearchFragment;->onQuery(Ljava/lang/String;Ljava/lang/String;)V

    const/4 p1, 0x1

    goto :goto_0

    :cond_0
    const/4 p1, 0x0

    :goto_0
    return p1
.end method

.method private startFaqFragment()V
    .locals 5

    .line 87
    iget-object v0, p0, Lcom/helpshift/support/controllers/FaqFlowController;->bundle:Landroid/os/Bundle;

    invoke-static {v0}, Lcom/helpshift/support/compositions/FaqFragment;->newInstance(Landroid/os/Bundle;)Lcom/helpshift/support/compositions/FaqFragment;

    move-result-object v0

    .line 88
    iget-object v1, p0, Lcom/helpshift/support/controllers/FaqFlowController;->fragmentManager:Landroidx/fragment/app/FragmentManager;

    sget v2, Lcom/helpshift/R$id;->list_fragment_container:I

    const/4 v3, 0x0

    const/4 v4, 0x1

    invoke-static {v1, v2, v0, v3, v4}, Lcom/helpshift/support/util/FragmentUtil;->startFragmentWithoutBackStack(Landroidx/fragment/app/FragmentManager;ILandroidx/fragment/app/Fragment;Ljava/lang/String;Z)V

    return-void
.end method

.method private startQuestionListFragment()V
    .locals 5

    .line 96
    iget-object v0, p0, Lcom/helpshift/support/controllers/FaqFlowController;->bundle:Landroid/os/Bundle;

    invoke-static {v0}, Lcom/helpshift/support/fragments/QuestionListFragment;->newInstance(Landroid/os/Bundle;)Lcom/helpshift/support/fragments/QuestionListFragment;

    move-result-object v0

    .line 97
    iget-object v1, p0, Lcom/helpshift/support/controllers/FaqFlowController;->fragmentManager:Landroidx/fragment/app/FragmentManager;

    sget v2, Lcom/helpshift/R$id;->list_fragment_container:I

    const/4 v3, 0x0

    const/4 v4, 0x0

    invoke-static {v1, v2, v0, v3, v4}, Lcom/helpshift/support/util/FragmentUtil;->startFragmentWithoutBackStack(Landroidx/fragment/app/FragmentManager;ILandroidx/fragment/app/Fragment;Ljava/lang/String;Z)V

    return-void
.end method

.method private startSingleQuestionFragment()V
    .locals 5

    .line 105
    sget v0, Lcom/helpshift/R$id;->list_fragment_container:I

    .line 107
    iget-boolean v1, p0, Lcom/helpshift/support/controllers/FaqFlowController;->isScreenLarge:Z

    if-eqz v1, :cond_0

    .line 108
    sget v0, Lcom/helpshift/R$id;->single_question_container:I

    .line 110
    :cond_0
    iget-object v1, p0, Lcom/helpshift/support/controllers/FaqFlowController;->faqFlowView:Lcom/helpshift/support/contracts/FaqFlowView;

    invoke-interface {v1}, Lcom/helpshift/support/contracts/FaqFlowView;->getSupportFragment()Lcom/helpshift/support/fragments/SupportFragment;

    move-result-object v1

    invoke-virtual {v1}, Lcom/helpshift/support/fragments/SupportFragment;->getSupportController()Lcom/helpshift/support/controllers/SupportController;

    move-result-object v1

    const/4 v2, 0x1

    invoke-virtual {v1, v2}, Lcom/helpshift/support/controllers/SupportController;->setSearchPerformed(Z)V

    .line 111
    iget-object v1, p0, Lcom/helpshift/support/controllers/FaqFlowController;->bundle:Landroid/os/Bundle;

    iget-boolean v3, p0, Lcom/helpshift/support/controllers/FaqFlowController;->isScreenLarge:Z

    const/4 v4, 0x0

    invoke-static {v1, v2, v3, v4}, Lcom/helpshift/support/fragments/SingleQuestionFragment;->newInstance(Landroid/os/Bundle;IZLcom/helpshift/support/fragments/SingleQuestionFragment$QuestionReadListener;)Lcom/helpshift/support/fragments/SingleQuestionFragment;

    move-result-object v1

    .line 116
    iget-object v2, p0, Lcom/helpshift/support/controllers/FaqFlowController;->fragmentManager:Landroidx/fragment/app/FragmentManager;

    const/4 v3, 0x0

    invoke-static {v2, v0, v1, v4, v3}, Lcom/helpshift/support/util/FragmentUtil;->startFragmentWithoutBackStack(Landroidx/fragment/app/FragmentManager;ILandroidx/fragment/app/Fragment;Ljava/lang/String;Z)V

    return-void
.end method


# virtual methods
.method getTopMostFaqFragment()Landroidx/fragment/app/Fragment;
    .locals 1

    .line 277
    iget-object v0, p0, Lcom/helpshift/support/controllers/FaqFlowController;->fragmentManager:Landroidx/fragment/app/FragmentManager;

    invoke-static {v0}, Lcom/helpshift/support/util/FragmentUtil;->getTopMostFragment(Landroidx/fragment/app/FragmentManager;)Landroidx/fragment/app/Fragment;

    move-result-object v0

    return-object v0
.end method

.method public onContactUsClicked(Ljava/lang/String;)V
    .locals 1

    const/4 v0, 0x1

    .line 181
    invoke-virtual {p0, v0}, Lcom/helpshift/support/controllers/FaqFlowController;->setRetainSearchFragmentState(Z)V

    .line 182
    invoke-virtual {p0}, Lcom/helpshift/support/controllers/FaqFlowController;->performedSearch()V

    .line 183
    iget-object v0, p0, Lcom/helpshift/support/controllers/FaqFlowController;->faqFlowView:Lcom/helpshift/support/contracts/FaqFlowView;

    invoke-interface {v0}, Lcom/helpshift/support/contracts/FaqFlowView;->getSupportFragment()Lcom/helpshift/support/fragments/SupportFragment;

    move-result-object v0

    invoke-virtual {v0}, Lcom/helpshift/support/fragments/SupportFragment;->getSupportController()Lcom/helpshift/support/controllers/SupportController;

    move-result-object v0

    invoke-virtual {v0, p1}, Lcom/helpshift/support/controllers/SupportController;->onContactUsClicked(Ljava/lang/String;)V

    return-void
.end method

.method public onFragmentManagerUpdate(Landroidx/fragment/app/FragmentManager;)V
    .locals 0

    .line 65
    iput-object p1, p0, Lcom/helpshift/support/controllers/FaqFlowController;->fragmentManager:Landroidx/fragment/app/FragmentManager;

    return-void
.end method

.method public onMenuItemActionCollapse(Landroid/view/MenuItem;)Z
    .locals 1

    .line 202
    invoke-virtual {p0}, Lcom/helpshift/support/controllers/FaqFlowController;->performedSearch()V

    .line 203
    iget-boolean p1, p0, Lcom/helpshift/support/controllers/FaqFlowController;->retainSearchFragmentState:Z

    if-nez p1, :cond_0

    const-string p1, ""

    .line 204
    iput-object p1, p0, Lcom/helpshift/support/controllers/FaqFlowController;->lastQuery:Ljava/lang/String;

    iput-object p1, p0, Lcom/helpshift/support/controllers/FaqFlowController;->currentQuery:Ljava/lang/String;

    .line 205
    iget-object p1, p0, Lcom/helpshift/support/controllers/FaqFlowController;->fragmentManager:Landroidx/fragment/app/FragmentManager;

    const-class v0, Lcom/helpshift/support/fragments/SearchFragment;

    invoke-virtual {v0}, Ljava/lang/Class;->getName()Ljava/lang/String;

    move-result-object v0

    invoke-static {p1, v0}, Lcom/helpshift/support/util/FragmentUtil;->popBackStack(Landroidx/fragment/app/FragmentManager;Ljava/lang/String;)V

    :cond_0
    const/4 p1, 0x1

    return p1
.end method

.method public onMenuItemActionExpand(Landroid/view/MenuItem;)Z
    .locals 4

    .line 188
    iget-object p1, p0, Lcom/helpshift/support/controllers/FaqFlowController;->fragmentManager:Landroidx/fragment/app/FragmentManager;

    const-string v0, "Helpshift_SearchFrag"

    invoke-virtual {p1, v0}, Landroidx/fragment/app/FragmentManager;->findFragmentByTag(Ljava/lang/String;)Landroidx/fragment/app/Fragment;

    move-result-object p1

    check-cast p1, Lcom/helpshift/support/fragments/SearchFragment;

    if-nez p1, :cond_0

    .line 190
    iget-object p1, p0, Lcom/helpshift/support/controllers/FaqFlowController;->bundle:Landroid/os/Bundle;

    invoke-static {p1}, Lcom/helpshift/support/fragments/SearchFragment;->newInstance(Landroid/os/Bundle;)Lcom/helpshift/support/fragments/SearchFragment;

    move-result-object p1

    .line 191
    iget-object v0, p0, Lcom/helpshift/support/controllers/FaqFlowController;->fragmentManager:Landroidx/fragment/app/FragmentManager;

    sget v1, Lcom/helpshift/R$id;->list_fragment_container:I

    const-string v2, "Helpshift_SearchFrag"

    const/4 v3, 0x0

    invoke-static {v0, v1, p1, v2, v3}, Lcom/helpshift/support/util/FragmentUtil;->startFragmentWithBackStack(Landroidx/fragment/app/FragmentManager;ILandroidx/fragment/app/Fragment;Ljava/lang/String;Z)V

    :cond_0
    const/4 p1, 0x1

    return p1
.end method

.method public onQueryTextChange(Ljava/lang/String;)Z
    .locals 2

    .line 220
    invoke-static {p1}, Landroid/text/TextUtils;->isEmpty(Ljava/lang/CharSequence;)Z

    move-result v0

    if-eqz v0, :cond_0

    iget-object v0, p0, Lcom/helpshift/support/controllers/FaqFlowController;->currentQuery:Ljava/lang/String;

    .line 221
    invoke-virtual {v0}, Ljava/lang/String;->length()I

    move-result v0

    const/4 v1, 0x2

    if-le v0, v1, :cond_0

    .line 222
    invoke-virtual {p0}, Lcom/helpshift/support/controllers/FaqFlowController;->performedSearch()V

    .line 224
    :cond_0
    iput-object p1, p0, Lcom/helpshift/support/controllers/FaqFlowController;->currentQuery:Ljava/lang/String;

    .line 225
    invoke-direct {p0, p1}, Lcom/helpshift/support/controllers/FaqFlowController;->onQuery(Ljava/lang/String;)Z

    move-result p1

    return p1
.end method

.method public onQueryTextSubmit(Ljava/lang/String;)Z
    .locals 0

    const/4 p1, 0x0

    return p1
.end method

.method public onQuestionSelected(Ljava/lang/String;Ljava/util/ArrayList;)V
    .locals 4
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/lang/String;",
            "Ljava/util/ArrayList<",
            "Ljava/lang/String;",
            ">;)V"
        }
    .end annotation

    .line 149
    invoke-virtual {p0}, Lcom/helpshift/support/controllers/FaqFlowController;->performedSearch()V

    .line 150
    iget-object v0, p0, Lcom/helpshift/support/controllers/FaqFlowController;->faqFlowView:Lcom/helpshift/support/contracts/FaqFlowView;

    invoke-interface {v0}, Lcom/helpshift/support/contracts/FaqFlowView;->getSupportFragment()Lcom/helpshift/support/fragments/SupportFragment;

    move-result-object v0

    invoke-virtual {v0}, Lcom/helpshift/support/fragments/SupportFragment;->getSupportController()Lcom/helpshift/support/controllers/SupportController;

    move-result-object v0

    const/4 v1, 0x1

    invoke-virtual {v0, v1}, Lcom/helpshift/support/controllers/SupportController;->setSearchPerformed(Z)V

    .line 152
    new-instance v0, Landroid/os/Bundle;

    invoke-direct {v0}, Landroid/os/Bundle;-><init>()V

    const-string v2, "questionPublishId"

    .line 153
    invoke-virtual {v0, v2, p1}, Landroid/os/Bundle;->putString(Ljava/lang/String;Ljava/lang/String;)V

    const-string p1, "searchTerms"

    .line 154
    invoke-virtual {v0, p1, p2}, Landroid/os/Bundle;->putStringArrayList(Ljava/lang/String;Ljava/util/ArrayList;)V

    .line 155
    iget-boolean p1, p0, Lcom/helpshift/support/controllers/FaqFlowController;->isScreenLarge:Z

    const/4 p2, 0x0

    const/4 v2, 0x0

    if-eqz p1, :cond_0

    .line 156
    sget p1, Lcom/helpshift/R$id;->details_fragment_container:I

    .line 157
    iget-object v3, p0, Lcom/helpshift/support/controllers/FaqFlowController;->fragmentManager:Landroidx/fragment/app/FragmentManager;

    .line 159
    invoke-static {v0, v1, v2, p2}, Lcom/helpshift/support/fragments/SingleQuestionFragment;->newInstance(Landroid/os/Bundle;IZLcom/helpshift/support/fragments/SingleQuestionFragment$QuestionReadListener;)Lcom/helpshift/support/fragments/SingleQuestionFragment;

    move-result-object v0

    .line 157
    invoke-static {v3, p1, v0, p2, v2}, Lcom/helpshift/support/util/FragmentUtil;->startFragmentWithoutBackStack(Landroidx/fragment/app/FragmentManager;ILandroidx/fragment/app/Fragment;Ljava/lang/String;Z)V

    goto :goto_0

    .line 167
    :cond_0
    sget p1, Lcom/helpshift/R$id;->list_fragment_container:I

    .line 168
    iget-object v3, p0, Lcom/helpshift/support/controllers/FaqFlowController;->fragmentManager:Landroidx/fragment/app/FragmentManager;

    .line 170
    invoke-static {v0, v1, v2, p2}, Lcom/helpshift/support/fragments/SingleQuestionFragment;->newInstance(Landroid/os/Bundle;IZLcom/helpshift/support/fragments/SingleQuestionFragment$QuestionReadListener;)Lcom/helpshift/support/fragments/SingleQuestionFragment;

    move-result-object v0

    .line 168
    invoke-static {v3, p1, v0, p2, v2}, Lcom/helpshift/support/util/FragmentUtil;->startFragmentWithBackStack(Landroidx/fragment/app/FragmentManager;ILandroidx/fragment/app/Fragment;Ljava/lang/String;Z)V

    :goto_0
    return-void
.end method

.method public onSaveInstanceState(Landroid/os/Bundle;)V
    .locals 2

    const-string v0, "key_faq_controller_state"

    .line 266
    iget-boolean v1, p0, Lcom/helpshift/support/controllers/FaqFlowController;->isControllerStarted:Z

    invoke-virtual {p1, v0, v1}, Landroid/os/Bundle;->putBoolean(Ljava/lang/String;Z)V

    return-void
.end method

.method public onSectionSelected(Landroid/os/Bundle;)V
    .locals 4

    .line 129
    iget-boolean v0, p0, Lcom/helpshift/support/controllers/FaqFlowController;->isScreenLarge:Z

    const/4 v1, 0x0

    const/4 v2, 0x0

    if-eqz v0, :cond_0

    .line 130
    invoke-static {p1}, Lcom/helpshift/support/fragments/QuestionListFragment;->newInstance(Landroid/os/Bundle;)Lcom/helpshift/support/fragments/QuestionListFragment;

    move-result-object p1

    .line 131
    iget-object v0, p0, Lcom/helpshift/support/controllers/FaqFlowController;->fragmentManager:Landroidx/fragment/app/FragmentManager;

    sget v3, Lcom/helpshift/R$id;->list_fragment_container:I

    invoke-static {v0, v3, p1, v2, v1}, Lcom/helpshift/support/util/FragmentUtil;->startFragmentWithBackStack(Landroidx/fragment/app/FragmentManager;ILandroidx/fragment/app/Fragment;Ljava/lang/String;Z)V

    goto :goto_0

    .line 138
    :cond_0
    invoke-static {p1}, Lcom/helpshift/support/compositions/SectionPagerFragment;->newInstance(Landroid/os/Bundle;)Lcom/helpshift/support/compositions/SectionPagerFragment;

    move-result-object p1

    .line 139
    iget-object v0, p0, Lcom/helpshift/support/controllers/FaqFlowController;->fragmentManager:Landroidx/fragment/app/FragmentManager;

    sget v3, Lcom/helpshift/R$id;->list_fragment_container:I

    invoke-static {v0, v3, p1, v2, v1}, Lcom/helpshift/support/util/FragmentUtil;->startFragmentWithBackStack(Landroidx/fragment/app/FragmentManager;ILandroidx/fragment/app/Fragment;Ljava/lang/String;Z)V

    :goto_0
    return-void
.end method

.method public onViewStateRestored(Landroid/os/Bundle;)V
    .locals 1

    .line 270
    iget-boolean v0, p0, Lcom/helpshift/support/controllers/FaqFlowController;->isControllerStarted:Z

    if-nez v0, :cond_0

    const-string v0, "key_faq_controller_state"

    .line 271
    invoke-virtual {p1, v0}, Landroid/os/Bundle;->containsKey(Ljava/lang/String;)Z

    move-result v0

    if-eqz v0, :cond_0

    const-string v0, "key_faq_controller_state"

    .line 272
    invoke-virtual {p1, v0}, Landroid/os/Bundle;->getBoolean(Ljava/lang/String;)Z

    move-result p1

    iput-boolean p1, p0, Lcom/helpshift/support/controllers/FaqFlowController;->isControllerStarted:Z

    :cond_0
    return-void
.end method

.method public performedSearch()V
    .locals 4

    .line 241
    iget-object v0, p0, Lcom/helpshift/support/controllers/FaqFlowController;->currentQuery:Ljava/lang/String;

    invoke-virtual {v0}, Ljava/lang/String;->trim()Ljava/lang/String;

    move-result-object v0

    invoke-static {v0}, Landroid/text/TextUtils;->isEmpty(Ljava/lang/CharSequence;)Z

    move-result v0

    if-nez v0, :cond_0

    iget-object v0, p0, Lcom/helpshift/support/controllers/FaqFlowController;->lastQuery:Ljava/lang/String;

    iget-object v1, p0, Lcom/helpshift/support/controllers/FaqFlowController;->currentQuery:Ljava/lang/String;

    .line 242
    invoke-virtual {v0, v1}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v0

    if-nez v0, :cond_0

    .line 243
    iget-object v0, p0, Lcom/helpshift/support/controllers/FaqFlowController;->faqFlowView:Lcom/helpshift/support/contracts/FaqFlowView;

    invoke-interface {v0}, Lcom/helpshift/support/contracts/FaqFlowView;->getSupportFragment()Lcom/helpshift/support/fragments/SupportFragment;

    move-result-object v0

    invoke-virtual {v0}, Lcom/helpshift/support/fragments/SupportFragment;->getSupportController()Lcom/helpshift/support/controllers/SupportController;

    move-result-object v0

    const/4 v1, 0x1

    invoke-virtual {v0, v1}, Lcom/helpshift/support/controllers/SupportController;->setSearchPerformed(Z)V

    .line 244
    iget-object v0, p0, Lcom/helpshift/support/controllers/FaqFlowController;->bundle:Landroid/os/Bundle;

    const-string v2, "search_performed"

    invoke-virtual {v0, v2, v1}, Landroid/os/Bundle;->putBoolean(Ljava/lang/String;Z)V

    .line 246
    iget-object v0, p0, Lcom/helpshift/support/controllers/FaqFlowController;->fragmentManager:Landroidx/fragment/app/FragmentManager;

    const-string v1, "Helpshift_SearchFrag"

    invoke-virtual {v0, v1}, Landroidx/fragment/app/FragmentManager;->findFragmentByTag(Ljava/lang/String;)Landroidx/fragment/app/Fragment;

    move-result-object v0

    check-cast v0, Lcom/helpshift/support/fragments/SearchFragment;

    if-eqz v0, :cond_0

    .line 248
    invoke-virtual {v0}, Lcom/helpshift/support/fragments/SearchFragment;->getNumberOfSearchResults()I

    move-result v0

    if-ltz v0, :cond_0

    .line 251
    new-instance v1, Ljava/util/HashMap;

    invoke-direct {v1}, Ljava/util/HashMap;-><init>()V

    const-string v2, "s"

    .line 252
    iget-object v3, p0, Lcom/helpshift/support/controllers/FaqFlowController;->currentQuery:Ljava/lang/String;

    invoke-interface {v1, v2, v3}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string v2, "n"

    .line 253
    invoke-static {v0}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v0

    invoke-interface {v1, v2, v0}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string v0, "nt"

    .line 255
    invoke-static {}, Lcom/helpshift/util/HelpshiftContext;->getApplicationContext()Landroid/content/Context;

    move-result-object v2

    invoke-static {v2}, Lcom/helpshift/util/HelpshiftConnectionUtil;->isOnline(Landroid/content/Context;)Z

    move-result v2

    invoke-static {v2}, Ljava/lang/Boolean;->valueOf(Z)Ljava/lang/Boolean;

    move-result-object v2

    .line 254
    invoke-interface {v1, v0, v2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 256
    invoke-static {}, Lcom/helpshift/util/HelpshiftContext;->getCoreApi()Lcom/helpshift/CoreApi;

    move-result-object v0

    invoke-interface {v0}, Lcom/helpshift/CoreApi;->getAnalyticsEventDM()Lcom/helpshift/analytics/domainmodel/AnalyticsEventDM;

    move-result-object v0

    sget-object v2, Lcom/helpshift/analytics/AnalyticsEventType;->PERFORMED_SEARCH:Lcom/helpshift/analytics/AnalyticsEventType;

    invoke-virtual {v0, v2, v1}, Lcom/helpshift/analytics/domainmodel/AnalyticsEventDM;->pushEvent(Lcom/helpshift/analytics/AnalyticsEventType;Ljava/util/Map;)V

    .line 259
    iget-object v0, p0, Lcom/helpshift/support/controllers/FaqFlowController;->currentQuery:Ljava/lang/String;

    iput-object v0, p0, Lcom/helpshift/support/controllers/FaqFlowController;->lastQuery:Ljava/lang/String;

    :cond_0
    return-void
.end method

.method public setRetainSearchFragmentState(Z)V
    .locals 0

    .line 124
    iput-boolean p1, p0, Lcom/helpshift/support/controllers/FaqFlowController;->retainSearchFragmentState:Z

    return-void
.end method

.method public start()V
    .locals 3

    .line 69
    iget-boolean v0, p0, Lcom/helpshift/support/controllers/FaqFlowController;->isControllerStarted:Z

    if-nez v0, :cond_0

    .line 70
    iget-object v0, p0, Lcom/helpshift/support/controllers/FaqFlowController;->bundle:Landroid/os/Bundle;

    const-string v1, "support_mode"

    const/4 v2, 0x0

    invoke-virtual {v0, v1, v2}, Landroid/os/Bundle;->getInt(Ljava/lang/String;I)I

    move-result v0

    packed-switch v0, :pswitch_data_0

    .line 79
    invoke-direct {p0}, Lcom/helpshift/support/controllers/FaqFlowController;->startFaqFragment()V

    goto :goto_0

    .line 76
    :pswitch_0
    invoke-direct {p0}, Lcom/helpshift/support/controllers/FaqFlowController;->startSingleQuestionFragment()V

    goto :goto_0

    .line 73
    :pswitch_1
    invoke-direct {p0}, Lcom/helpshift/support/controllers/FaqFlowController;->startQuestionListFragment()V

    :cond_0
    :goto_0
    const/4 v0, 0x1

    .line 83
    iput-boolean v0, p0, Lcom/helpshift/support/controllers/FaqFlowController;->isControllerStarted:Z

    return-void

    nop

    :pswitch_data_0
    .packed-switch 0x2
        :pswitch_1
        :pswitch_0
    .end packed-switch
.end method
