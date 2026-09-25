.class public Lcom/helpshift/support/conversations/usersetup/UserSetupFragment;
.super Lcom/helpshift/support/fragments/MainFragment;
.source "UserSetupFragment.java"

# interfaces
.implements Lcom/helpshift/conversation/activeconversation/usersetup/UserSetupRenderer;
.implements Lcom/helpshift/network/connectivity/HSNetworkConnectivityCallback;


# static fields
.field public static final FRAGMENT_TAG:Ljava/lang/String; = "HSUserSetupFragment"


# instance fields
.field private offlineErrorView:Landroid/view/View;

.field private progressBar:Landroid/widget/ProgressBar;

.field private progressDescriptionView:Landroid/view/View;

.field private userSetupVM:Lcom/helpshift/conversation/usersetup/UserSetupVM;


# direct methods
.method public constructor <init>()V
    .locals 0

    .line 25
    invoke-direct {p0}, Lcom/helpshift/support/fragments/MainFragment;-><init>()V

    return-void
.end method

.method private addViewStateObservers()V
    .locals 3

    .line 69
    invoke-static {}, Lcom/helpshift/util/HelpshiftContext;->getCoreApi()Lcom/helpshift/CoreApi;

    move-result-object v0

    invoke-interface {v0}, Lcom/helpshift/CoreApi;->getDomain()Lcom/helpshift/common/domain/Domain;

    move-result-object v0

    .line 72
    iget-object v1, p0, Lcom/helpshift/support/conversations/usersetup/UserSetupFragment;->userSetupVM:Lcom/helpshift/conversation/usersetup/UserSetupVM;

    invoke-virtual {v1}, Lcom/helpshift/conversation/usersetup/UserSetupVM;->getProgressBarViewState()Lcom/helpshift/widget/BaseViewState;

    move-result-object v1

    new-instance v2, Lcom/helpshift/support/conversations/usersetup/UserSetupFragment$1;

    invoke-direct {v2, p0}, Lcom/helpshift/support/conversations/usersetup/UserSetupFragment$1;-><init>(Lcom/helpshift/support/conversations/usersetup/UserSetupFragment;)V

    invoke-virtual {v1, v0, v2}, Lcom/helpshift/widget/BaseViewState;->subscribe(Lcom/helpshift/common/domain/Domain;Lcom/helpshift/widget/HSObserver;)V

    .line 85
    iget-object v1, p0, Lcom/helpshift/support/conversations/usersetup/UserSetupFragment;->userSetupVM:Lcom/helpshift/conversation/usersetup/UserSetupVM;

    invoke-virtual {v1}, Lcom/helpshift/conversation/usersetup/UserSetupVM;->getDescriptionProgressViewState()Lcom/helpshift/widget/BaseViewState;

    move-result-object v1

    new-instance v2, Lcom/helpshift/support/conversations/usersetup/UserSetupFragment$2;

    invoke-direct {v2, p0}, Lcom/helpshift/support/conversations/usersetup/UserSetupFragment$2;-><init>(Lcom/helpshift/support/conversations/usersetup/UserSetupFragment;)V

    invoke-virtual {v1, v0, v2}, Lcom/helpshift/widget/BaseViewState;->subscribe(Lcom/helpshift/common/domain/Domain;Lcom/helpshift/widget/HSObserver;)V

    .line 99
    iget-object v1, p0, Lcom/helpshift/support/conversations/usersetup/UserSetupFragment;->userSetupVM:Lcom/helpshift/conversation/usersetup/UserSetupVM;

    invoke-virtual {v1}, Lcom/helpshift/conversation/usersetup/UserSetupVM;->getUserOfflineErrorViewState()Lcom/helpshift/widget/BaseViewState;

    move-result-object v1

    new-instance v2, Lcom/helpshift/support/conversations/usersetup/UserSetupFragment$3;

    invoke-direct {v2, p0}, Lcom/helpshift/support/conversations/usersetup/UserSetupFragment$3;-><init>(Lcom/helpshift/support/conversations/usersetup/UserSetupFragment;)V

    invoke-virtual {v1, v0, v2}, Lcom/helpshift/widget/BaseViewState;->subscribe(Lcom/helpshift/common/domain/Domain;Lcom/helpshift/widget/HSObserver;)V

    return-void
.end method

.method private getSupportController()Lcom/helpshift/support/controllers/SupportController;
    .locals 1

    .line 184
    invoke-virtual {p0}, Lcom/helpshift/support/conversations/usersetup/UserSetupFragment;->getParentFragment()Landroidx/fragment/app/Fragment;

    move-result-object v0

    check-cast v0, Lcom/helpshift/support/fragments/SupportFragment;

    .line 185
    invoke-virtual {v0}, Lcom/helpshift/support/fragments/SupportFragment;->getSupportController()Lcom/helpshift/support/controllers/SupportController;

    move-result-object v0

    return-object v0
.end method

.method private initialize(Landroid/view/View;)V
    .locals 2

    .line 120
    sget v0, Lcom/helpshift/R$id;->progressbar:I

    invoke-virtual {p1, v0}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object v0

    check-cast v0, Landroid/widget/ProgressBar;

    iput-object v0, p0, Lcom/helpshift/support/conversations/usersetup/UserSetupFragment;->progressBar:Landroid/widget/ProgressBar;

    .line 121
    invoke-virtual {p0}, Lcom/helpshift/support/conversations/usersetup/UserSetupFragment;->getContext()Landroid/content/Context;

    move-result-object v0

    iget-object v1, p0, Lcom/helpshift/support/conversations/usersetup/UserSetupFragment;->progressBar:Landroid/widget/ProgressBar;

    invoke-virtual {v1}, Landroid/widget/ProgressBar;->getIndeterminateDrawable()Landroid/graphics/drawable/Drawable;

    move-result-object v1

    invoke-static {v0, v1}, Lcom/helpshift/support/util/Styles;->setAccentColor(Landroid/content/Context;Landroid/graphics/drawable/Drawable;)V

    .line 122
    sget v0, Lcom/helpshift/R$id;->progress_description_text_view:I

    invoke-virtual {p1, v0}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object v0

    iput-object v0, p0, Lcom/helpshift/support/conversations/usersetup/UserSetupFragment;->progressDescriptionView:Landroid/view/View;

    .line 123
    sget v0, Lcom/helpshift/R$id;->offline_error_view:I

    invoke-virtual {p1, v0}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object v0

    iput-object v0, p0, Lcom/helpshift/support/conversations/usersetup/UserSetupFragment;->offlineErrorView:Landroid/view/View;

    .line 124
    sget v0, Lcom/helpshift/R$id;->info_icon:I

    invoke-virtual {p1, v0}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object p1

    check-cast p1, Landroid/widget/ImageView;

    .line 125
    invoke-virtual {p0}, Lcom/helpshift/support/conversations/usersetup/UserSetupFragment;->getContext()Landroid/content/Context;

    move-result-object v0

    invoke-virtual {p1}, Landroid/widget/ImageView;->getDrawable()Landroid/graphics/drawable/Drawable;

    move-result-object p1

    const v1, 0x1010036

    invoke-static {v0, p1, v1}, Lcom/helpshift/util/Styles;->setColorFilter(Landroid/content/Context;Landroid/graphics/drawable/Drawable;I)V

    .line 126
    invoke-static {}, Lcom/helpshift/util/HelpshiftContext;->getCoreApi()Lcom/helpshift/CoreApi;

    move-result-object p1

    invoke-interface {p1, p0}, Lcom/helpshift/CoreApi;->getUserSetupVM(Lcom/helpshift/conversation/activeconversation/usersetup/UserSetupRenderer;)Lcom/helpshift/conversation/usersetup/UserSetupVM;

    move-result-object p1

    iput-object p1, p0, Lcom/helpshift/support/conversations/usersetup/UserSetupFragment;->userSetupVM:Lcom/helpshift/conversation/usersetup/UserSetupVM;

    return-void
.end method

.method public static newInstance()Lcom/helpshift/support/conversations/usersetup/UserSetupFragment;
    .locals 1

    .line 35
    new-instance v0, Lcom/helpshift/support/conversations/usersetup/UserSetupFragment;

    invoke-direct {v0}, Lcom/helpshift/support/conversations/usersetup/UserSetupFragment;-><init>()V

    return-object v0
.end method

.method private removeViewStateObservers()V
    .locals 1

    .line 114
    iget-object v0, p0, Lcom/helpshift/support/conversations/usersetup/UserSetupFragment;->userSetupVM:Lcom/helpshift/conversation/usersetup/UserSetupVM;

    invoke-virtual {v0}, Lcom/helpshift/conversation/usersetup/UserSetupVM;->getProgressBarViewState()Lcom/helpshift/widget/BaseViewState;

    move-result-object v0

    invoke-virtual {v0}, Lcom/helpshift/widget/BaseViewState;->unsubscribe()V

    .line 115
    iget-object v0, p0, Lcom/helpshift/support/conversations/usersetup/UserSetupFragment;->userSetupVM:Lcom/helpshift/conversation/usersetup/UserSetupVM;

    invoke-virtual {v0}, Lcom/helpshift/conversation/usersetup/UserSetupVM;->getDescriptionProgressViewState()Lcom/helpshift/widget/BaseViewState;

    move-result-object v0

    invoke-virtual {v0}, Lcom/helpshift/widget/BaseViewState;->unsubscribe()V

    .line 116
    iget-object v0, p0, Lcom/helpshift/support/conversations/usersetup/UserSetupFragment;->userSetupVM:Lcom/helpshift/conversation/usersetup/UserSetupVM;

    invoke-virtual {v0}, Lcom/helpshift/conversation/usersetup/UserSetupVM;->getUserOfflineErrorViewState()Lcom/helpshift/widget/BaseViewState;

    move-result-object v0

    invoke-virtual {v0}, Lcom/helpshift/widget/BaseViewState;->unsubscribe()V

    return-void
.end method


# virtual methods
.method public hideNoInternetView()V
    .locals 2

    .line 150
    iget-object v0, p0, Lcom/helpshift/support/conversations/usersetup/UserSetupFragment;->offlineErrorView:Landroid/view/View;

    const/16 v1, 0x8

    invoke-virtual {v0, v1}, Landroid/view/View;->setVisibility(I)V

    return-void
.end method

.method public hideProgressBar()V
    .locals 2

    .line 160
    iget-object v0, p0, Lcom/helpshift/support/conversations/usersetup/UserSetupFragment;->progressBar:Landroid/widget/ProgressBar;

    const/16 v1, 0x8

    invoke-virtual {v0, v1}, Landroid/widget/ProgressBar;->setVisibility(I)V

    return-void
.end method

.method public hideProgressDescription()V
    .locals 2

    .line 170
    iget-object v0, p0, Lcom/helpshift/support/conversations/usersetup/UserSetupFragment;->progressDescriptionView:Landroid/view/View;

    const/16 v1, 0x8

    invoke-virtual {v0, v1}, Landroid/view/View;->setVisibility(I)V

    return-void
.end method

.method public onAuthenticationFailure()V
    .locals 1

    .line 180
    invoke-direct {p0}, Lcom/helpshift/support/conversations/usersetup/UserSetupFragment;->getSupportController()Lcom/helpshift/support/controllers/SupportController;

    move-result-object v0

    invoke-virtual {v0}, Lcom/helpshift/support/controllers/SupportController;->onAuthenticationFailure()V

    return-void
.end method

.method public onCreateView(Landroid/view/LayoutInflater;Landroid/view/ViewGroup;Landroid/os/Bundle;)Landroid/view/View;
    .locals 1

    .line 41
    sget p3, Lcom/helpshift/R$layout;->hs__user_setup_fragment:I

    const/4 v0, 0x0

    invoke-virtual {p1, p3, p2, v0}, Landroid/view/LayoutInflater;->inflate(ILandroid/view/ViewGroup;Z)Landroid/view/View;

    move-result-object p1

    return-object p1
.end method

.method public onDestroyView()V
    .locals 1

    .line 64
    iget-object v0, p0, Lcom/helpshift/support/conversations/usersetup/UserSetupFragment;->userSetupVM:Lcom/helpshift/conversation/usersetup/UserSetupVM;

    invoke-virtual {v0}, Lcom/helpshift/conversation/usersetup/UserSetupVM;->onDestroyView()V

    .line 65
    invoke-super {p0}, Lcom/helpshift/support/fragments/MainFragment;->onDestroyView()V

    return-void
.end method

.method public onNetworkAvailable()V
    .locals 1

    .line 190
    iget-object v0, p0, Lcom/helpshift/support/conversations/usersetup/UserSetupFragment;->userSetupVM:Lcom/helpshift/conversation/usersetup/UserSetupVM;

    invoke-virtual {v0}, Lcom/helpshift/conversation/usersetup/UserSetupVM;->onNetworkAvailable()V

    return-void
.end method

.method public onNetworkUnavailable()V
    .locals 1

    .line 195
    iget-object v0, p0, Lcom/helpshift/support/conversations/usersetup/UserSetupFragment;->userSetupVM:Lcom/helpshift/conversation/usersetup/UserSetupVM;

    invoke-virtual {v0}, Lcom/helpshift/conversation/usersetup/UserSetupVM;->onNetworkUnavailable()V

    return-void
.end method

.method public onPause()V
    .locals 1

    .line 131
    invoke-direct {p0}, Lcom/helpshift/support/conversations/usersetup/UserSetupFragment;->removeViewStateObservers()V

    .line 133
    invoke-static {}, Lcom/helpshift/network/connectivity/HSConnectivityManager;->getInstance()Lcom/helpshift/network/connectivity/HSConnectivityManager;

    move-result-object v0

    invoke-virtual {v0, p0}, Lcom/helpshift/network/connectivity/HSConnectivityManager;->unregisterNetworkConnectivityListener(Lcom/helpshift/network/connectivity/HSNetworkConnectivityCallback;)V

    .line 134
    invoke-super {p0}, Lcom/helpshift/support/fragments/MainFragment;->onPause()V

    return-void
.end method

.method public onResume()V
    .locals 1

    .line 53
    invoke-super {p0}, Lcom/helpshift/support/fragments/MainFragment;->onResume()V

    .line 54
    invoke-direct {p0}, Lcom/helpshift/support/conversations/usersetup/UserSetupFragment;->addViewStateObservers()V

    .line 56
    sget v0, Lcom/helpshift/R$string;->hs__conversation_header:I

    invoke-virtual {p0, v0}, Lcom/helpshift/support/conversations/usersetup/UserSetupFragment;->getString(I)Ljava/lang/String;

    move-result-object v0

    invoke-virtual {p0, v0}, Lcom/helpshift/support/conversations/usersetup/UserSetupFragment;->setToolbarTitle(Ljava/lang/String;)V

    .line 58
    invoke-static {}, Lcom/helpshift/network/connectivity/HSConnectivityManager;->getInstance()Lcom/helpshift/network/connectivity/HSConnectivityManager;

    move-result-object v0

    invoke-virtual {v0, p0}, Lcom/helpshift/network/connectivity/HSConnectivityManager;->registerNetworkConnectivityListener(Lcom/helpshift/network/connectivity/HSNetworkConnectivityCallback;)V

    .line 59
    iget-object v0, p0, Lcom/helpshift/support/conversations/usersetup/UserSetupFragment;->userSetupVM:Lcom/helpshift/conversation/usersetup/UserSetupVM;

    invoke-virtual {v0}, Lcom/helpshift/conversation/usersetup/UserSetupVM;->onResume()V

    return-void
.end method

.method public onUserSetupComplete()V
    .locals 1

    .line 175
    invoke-direct {p0}, Lcom/helpshift/support/conversations/usersetup/UserSetupFragment;->getSupportController()Lcom/helpshift/support/controllers/SupportController;

    move-result-object v0

    invoke-virtual {v0}, Lcom/helpshift/support/controllers/SupportController;->onUserSetupSyncCompleted()V

    return-void
.end method

.method public onViewCreated(Landroid/view/View;Landroid/os/Bundle;)V
    .locals 0

    .line 47
    invoke-direct {p0, p1}, Lcom/helpshift/support/conversations/usersetup/UserSetupFragment;->initialize(Landroid/view/View;)V

    .line 48
    invoke-super {p0, p1, p2}, Lcom/helpshift/support/fragments/MainFragment;->onViewCreated(Landroid/view/View;Landroid/os/Bundle;)V

    return-void
.end method

.method public shouldRefreshMenu()Z
    .locals 1

    const/4 v0, 0x1

    return v0
.end method

.method public showNoInternetView()V
    .locals 2

    .line 145
    iget-object v0, p0, Lcom/helpshift/support/conversations/usersetup/UserSetupFragment;->offlineErrorView:Landroid/view/View;

    const/4 v1, 0x0

    invoke-virtual {v0, v1}, Landroid/view/View;->setVisibility(I)V

    return-void
.end method

.method public showProgressBar()V
    .locals 2

    .line 155
    iget-object v0, p0, Lcom/helpshift/support/conversations/usersetup/UserSetupFragment;->progressBar:Landroid/widget/ProgressBar;

    const/4 v1, 0x0

    invoke-virtual {v0, v1}, Landroid/widget/ProgressBar;->setVisibility(I)V

    return-void
.end method

.method public showProgressDescription()V
    .locals 2

    .line 165
    iget-object v0, p0, Lcom/helpshift/support/conversations/usersetup/UserSetupFragment;->progressDescriptionView:Landroid/view/View;

    const/4 v1, 0x0

    invoke-virtual {v0, v1}, Landroid/view/View;->setVisibility(I)V

    return-void
.end method
