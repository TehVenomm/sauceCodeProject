.class public Lcom/helpshift/conversation/usersetup/UserSetupVM;
.super Ljava/lang/Object;
.source "UserSetupVM.java"

# interfaces
.implements Lcom/helpshift/account/domainmodel/UserSetupDM$UserSetupListener;
.implements Lcom/helpshift/account/AuthenticationFailureDM$AuthenticationFailureObserver;


# instance fields
.field private domain:Lcom/helpshift/common/domain/Domain;

.field private final errorViewState:Lcom/helpshift/widget/MutableBaseViewState;

.field private platform:Lcom/helpshift/common/platform/Platform;

.field private final progressBarViewState:Lcom/helpshift/widget/MutableBaseViewState;

.field private final progressDescriptionViewState:Lcom/helpshift/widget/MutableBaseViewState;

.field private renderer:Lcom/helpshift/conversation/activeconversation/usersetup/UserSetupRenderer;

.field private userSetupDM:Lcom/helpshift/account/domainmodel/UserSetupDM;


# direct methods
.method public constructor <init>(Lcom/helpshift/common/platform/Platform;Lcom/helpshift/common/domain/Domain;Lcom/helpshift/account/domainmodel/UserSetupDM;Lcom/helpshift/conversation/activeconversation/usersetup/UserSetupRenderer;)V
    .locals 0

    .line 34
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 35
    iput-object p1, p0, Lcom/helpshift/conversation/usersetup/UserSetupVM;->platform:Lcom/helpshift/common/platform/Platform;

    .line 36
    iput-object p3, p0, Lcom/helpshift/conversation/usersetup/UserSetupVM;->userSetupDM:Lcom/helpshift/account/domainmodel/UserSetupDM;

    .line 37
    iput-object p4, p0, Lcom/helpshift/conversation/usersetup/UserSetupVM;->renderer:Lcom/helpshift/conversation/activeconversation/usersetup/UserSetupRenderer;

    .line 38
    iput-object p2, p0, Lcom/helpshift/conversation/usersetup/UserSetupVM;->domain:Lcom/helpshift/common/domain/Domain;

    .line 39
    invoke-direct {p0}, Lcom/helpshift/conversation/usersetup/UserSetupVM;->buildProgressBarWidget()Lcom/helpshift/widget/MutableBaseViewState;

    move-result-object p1

    iput-object p1, p0, Lcom/helpshift/conversation/usersetup/UserSetupVM;->progressBarViewState:Lcom/helpshift/widget/MutableBaseViewState;

    .line 40
    new-instance p1, Lcom/helpshift/widget/MutableBaseViewState;

    invoke-direct {p1}, Lcom/helpshift/widget/MutableBaseViewState;-><init>()V

    iput-object p1, p0, Lcom/helpshift/conversation/usersetup/UserSetupVM;->progressDescriptionViewState:Lcom/helpshift/widget/MutableBaseViewState;

    .line 41
    new-instance p1, Lcom/helpshift/widget/MutableBaseViewState;

    invoke-direct {p1}, Lcom/helpshift/widget/MutableBaseViewState;-><init>()V

    iput-object p1, p0, Lcom/helpshift/conversation/usersetup/UserSetupVM;->errorViewState:Lcom/helpshift/widget/MutableBaseViewState;

    .line 42
    iget-object p1, p0, Lcom/helpshift/conversation/usersetup/UserSetupVM;->userSetupDM:Lcom/helpshift/account/domainmodel/UserSetupDM;

    invoke-virtual {p1, p0}, Lcom/helpshift/account/domainmodel/UserSetupDM;->registerUserSetupListener(Lcom/helpshift/account/domainmodel/UserSetupDM$UserSetupListener;)V

    .line 44
    iget-object p1, p0, Lcom/helpshift/conversation/usersetup/UserSetupVM;->domain:Lcom/helpshift/common/domain/Domain;

    invoke-virtual {p1}, Lcom/helpshift/common/domain/Domain;->getAuthenticationFailureDM()Lcom/helpshift/account/AuthenticationFailureDM;

    move-result-object p1

    invoke-virtual {p1, p0}, Lcom/helpshift/account/AuthenticationFailureDM;->registerListener(Lcom/helpshift/account/AuthenticationFailureDM$AuthenticationFailureObserver;)V

    return-void
.end method

.method static synthetic access$000(Lcom/helpshift/conversation/usersetup/UserSetupVM;)Lcom/helpshift/conversation/activeconversation/usersetup/UserSetupRenderer;
    .locals 0

    .line 20
    iget-object p0, p0, Lcom/helpshift/conversation/usersetup/UserSetupVM;->renderer:Lcom/helpshift/conversation/activeconversation/usersetup/UserSetupRenderer;

    return-object p0
.end method

.method static synthetic access$100(Lcom/helpshift/conversation/usersetup/UserSetupVM;)Lcom/helpshift/widget/MutableBaseViewState;
    .locals 0

    .line 20
    iget-object p0, p0, Lcom/helpshift/conversation/usersetup/UserSetupVM;->progressBarViewState:Lcom/helpshift/widget/MutableBaseViewState;

    return-object p0
.end method

.method static synthetic access$200(Lcom/helpshift/conversation/usersetup/UserSetupVM;)Lcom/helpshift/widget/MutableBaseViewState;
    .locals 0

    .line 20
    iget-object p0, p0, Lcom/helpshift/conversation/usersetup/UserSetupVM;->errorViewState:Lcom/helpshift/widget/MutableBaseViewState;

    return-object p0
.end method

.method static synthetic access$300(Lcom/helpshift/conversation/usersetup/UserSetupVM;)V
    .locals 0

    .line 20
    invoke-direct {p0}, Lcom/helpshift/conversation/usersetup/UserSetupVM;->showOfflineError()V

    return-void
.end method

.method private buildProgressBarWidget()Lcom/helpshift/widget/MutableBaseViewState;
    .locals 3

    .line 48
    new-instance v0, Lcom/helpshift/widget/MutableBaseViewState;

    invoke-direct {v0}, Lcom/helpshift/widget/MutableBaseViewState;-><init>()V

    .line 49
    iget-object v1, p0, Lcom/helpshift/conversation/usersetup/UserSetupVM;->userSetupDM:Lcom/helpshift/account/domainmodel/UserSetupDM;

    invoke-virtual {v1}, Lcom/helpshift/account/domainmodel/UserSetupDM;->getState()Lcom/helpshift/account/domainmodel/UserSetupState;

    move-result-object v1

    .line 51
    sget-object v2, Lcom/helpshift/account/domainmodel/UserSetupState;->IN_PROGRESS:Lcom/helpshift/account/domainmodel/UserSetupState;

    if-ne v1, v2, :cond_0

    const/4 v1, 0x1

    goto :goto_0

    :cond_0
    const/4 v1, 0x0

    :goto_0
    invoke-virtual {v0, v1}, Lcom/helpshift/widget/MutableBaseViewState;->setVisible(Z)V

    return-object v0
.end method

.method private handleUserSetupComplete()V
    .locals 2

    .line 94
    iget-object v0, p0, Lcom/helpshift/conversation/usersetup/UserSetupVM;->domain:Lcom/helpshift/common/domain/Domain;

    new-instance v1, Lcom/helpshift/conversation/usersetup/UserSetupVM$1;

    invoke-direct {v1, p0}, Lcom/helpshift/conversation/usersetup/UserSetupVM$1;-><init>(Lcom/helpshift/conversation/usersetup/UserSetupVM;)V

    invoke-virtual {v0, v1}, Lcom/helpshift/common/domain/Domain;->runOnUI(Lcom/helpshift/common/domain/F;)V

    return-void
.end method

.method private handleUserSetupState(Lcom/helpshift/account/domainmodel/UserSetupState;)V
    .locals 1

    .line 71
    iget-object v0, p0, Lcom/helpshift/conversation/usersetup/UserSetupVM;->platform:Lcom/helpshift/common/platform/Platform;

    invoke-interface {v0}, Lcom/helpshift/common/platform/Platform;->isOnline()Z

    move-result v0

    if-nez v0, :cond_0

    .line 72
    invoke-virtual {p0}, Lcom/helpshift/conversation/usersetup/UserSetupVM;->onNetworkUnavailable()V

    return-void

    .line 75
    :cond_0
    sget-object v0, Lcom/helpshift/conversation/usersetup/UserSetupVM$5;->$SwitchMap$com$helpshift$account$domainmodel$UserSetupState:[I

    invoke-virtual {p1}, Lcom/helpshift/account/domainmodel/UserSetupState;->ordinal()I

    move-result p1

    aget p1, v0, p1

    const/4 v0, 0x1

    packed-switch p1, :pswitch_data_0

    goto :goto_0

    .line 88
    :pswitch_0
    invoke-direct {p0}, Lcom/helpshift/conversation/usersetup/UserSetupVM;->handleUserSetupComplete()V

    goto :goto_0

    .line 83
    :pswitch_1
    iget-object p1, p0, Lcom/helpshift/conversation/usersetup/UserSetupVM;->progressBarViewState:Lcom/helpshift/widget/MutableBaseViewState;

    invoke-virtual {p1, v0}, Lcom/helpshift/widget/MutableBaseViewState;->setVisible(Z)V

    .line 84
    iget-object p1, p0, Lcom/helpshift/conversation/usersetup/UserSetupVM;->errorViewState:Lcom/helpshift/widget/MutableBaseViewState;

    const/4 v0, 0x0

    invoke-virtual {p1, v0}, Lcom/helpshift/widget/MutableBaseViewState;->setVisible(Z)V

    goto :goto_0

    .line 78
    :pswitch_2
    iget-object p1, p0, Lcom/helpshift/conversation/usersetup/UserSetupVM;->progressDescriptionViewState:Lcom/helpshift/widget/MutableBaseViewState;

    invoke-virtual {p1, v0}, Lcom/helpshift/widget/MutableBaseViewState;->setVisible(Z)V

    .line 80
    iget-object p1, p0, Lcom/helpshift/conversation/usersetup/UserSetupVM;->progressBarViewState:Lcom/helpshift/widget/MutableBaseViewState;

    invoke-virtual {p1, v0}, Lcom/helpshift/widget/MutableBaseViewState;->setVisible(Z)V

    :goto_0
    return-void

    :pswitch_data_0
    .packed-switch 0x1
        :pswitch_2
        :pswitch_2
        :pswitch_1
        :pswitch_0
    .end packed-switch
.end method

.method private showOfflineError()V
    .locals 2

    .line 145
    iget-object v0, p0, Lcom/helpshift/conversation/usersetup/UserSetupVM;->progressBarViewState:Lcom/helpshift/widget/MutableBaseViewState;

    const/4 v1, 0x0

    invoke-virtual {v0, v1}, Lcom/helpshift/widget/MutableBaseViewState;->setVisible(Z)V

    .line 146
    iget-object v0, p0, Lcom/helpshift/conversation/usersetup/UserSetupVM;->progressDescriptionViewState:Lcom/helpshift/widget/MutableBaseViewState;

    invoke-virtual {v0, v1}, Lcom/helpshift/widget/MutableBaseViewState;->setVisible(Z)V

    .line 147
    iget-object v0, p0, Lcom/helpshift/conversation/usersetup/UserSetupVM;->errorViewState:Lcom/helpshift/widget/MutableBaseViewState;

    const/4 v1, 0x1

    invoke-virtual {v0, v1}, Lcom/helpshift/widget/MutableBaseViewState;->setVisible(Z)V

    return-void
.end method


# virtual methods
.method public getDescriptionProgressViewState()Lcom/helpshift/widget/BaseViewState;
    .locals 1

    .line 155
    iget-object v0, p0, Lcom/helpshift/conversation/usersetup/UserSetupVM;->progressDescriptionViewState:Lcom/helpshift/widget/MutableBaseViewState;

    return-object v0
.end method

.method public getProgressBarViewState()Lcom/helpshift/widget/BaseViewState;
    .locals 1

    .line 151
    iget-object v0, p0, Lcom/helpshift/conversation/usersetup/UserSetupVM;->progressBarViewState:Lcom/helpshift/widget/MutableBaseViewState;

    return-object v0
.end method

.method public getUserOfflineErrorViewState()Lcom/helpshift/widget/BaseViewState;
    .locals 1

    .line 159
    iget-object v0, p0, Lcom/helpshift/conversation/usersetup/UserSetupVM;->errorViewState:Lcom/helpshift/widget/MutableBaseViewState;

    return-object v0
.end method

.method public onAuthenticationFailure()V
    .locals 2

    .line 111
    iget-object v0, p0, Lcom/helpshift/conversation/usersetup/UserSetupVM;->domain:Lcom/helpshift/common/domain/Domain;

    new-instance v1, Lcom/helpshift/conversation/usersetup/UserSetupVM$2;

    invoke-direct {v1, p0}, Lcom/helpshift/conversation/usersetup/UserSetupVM$2;-><init>(Lcom/helpshift/conversation/usersetup/UserSetupVM;)V

    invoke-virtual {v0, v1}, Lcom/helpshift/common/domain/Domain;->runOnUI(Lcom/helpshift/common/domain/F;)V

    return-void
.end method

.method public onDestroyView()V
    .locals 1

    const/4 v0, 0x0

    .line 66
    iput-object v0, p0, Lcom/helpshift/conversation/usersetup/UserSetupVM;->renderer:Lcom/helpshift/conversation/activeconversation/usersetup/UserSetupRenderer;

    return-void
.end method

.method public onNetworkAvailable()V
    .locals 2

    .line 123
    iget-object v0, p0, Lcom/helpshift/conversation/usersetup/UserSetupVM;->domain:Lcom/helpshift/common/domain/Domain;

    new-instance v1, Lcom/helpshift/conversation/usersetup/UserSetupVM$3;

    invoke-direct {v1, p0}, Lcom/helpshift/conversation/usersetup/UserSetupVM$3;-><init>(Lcom/helpshift/conversation/usersetup/UserSetupVM;)V

    invoke-virtual {v0, v1}, Lcom/helpshift/common/domain/Domain;->runOnUI(Lcom/helpshift/common/domain/F;)V

    return-void
.end method

.method public onNetworkUnavailable()V
    .locals 2

    .line 135
    iget-object v0, p0, Lcom/helpshift/conversation/usersetup/UserSetupVM;->domain:Lcom/helpshift/common/domain/Domain;

    new-instance v1, Lcom/helpshift/conversation/usersetup/UserSetupVM$4;

    invoke-direct {v1, p0}, Lcom/helpshift/conversation/usersetup/UserSetupVM$4;-><init>(Lcom/helpshift/conversation/usersetup/UserSetupVM;)V

    invoke-virtual {v0, v1}, Lcom/helpshift/common/domain/Domain;->runOnUI(Lcom/helpshift/common/domain/F;)V

    return-void
.end method

.method public onResume()V
    .locals 2

    .line 56
    iget-object v0, p0, Lcom/helpshift/conversation/usersetup/UserSetupVM;->userSetupDM:Lcom/helpshift/account/domainmodel/UserSetupDM;

    invoke-virtual {v0}, Lcom/helpshift/account/domainmodel/UserSetupDM;->getState()Lcom/helpshift/account/domainmodel/UserSetupState;

    move-result-object v0

    .line 57
    sget-object v1, Lcom/helpshift/account/domainmodel/UserSetupState;->COMPLETED:Lcom/helpshift/account/domainmodel/UserSetupState;

    if-ne v0, v1, :cond_0

    .line 58
    invoke-direct {p0}, Lcom/helpshift/conversation/usersetup/UserSetupVM;->handleUserSetupComplete()V

    goto :goto_0

    .line 61
    :cond_0
    iget-object v0, p0, Lcom/helpshift/conversation/usersetup/UserSetupVM;->userSetupDM:Lcom/helpshift/account/domainmodel/UserSetupDM;

    invoke-virtual {v0}, Lcom/helpshift/account/domainmodel/UserSetupDM;->startSetup()V

    :goto_0
    return-void
.end method

.method public userSetupStateChanged(Lcom/helpshift/account/domainmodel/UserDM;Lcom/helpshift/account/domainmodel/UserSetupState;)V
    .locals 0

    .line 106
    invoke-direct {p0, p2}, Lcom/helpshift/conversation/usersetup/UserSetupVM;->handleUserSetupState(Lcom/helpshift/account/domainmodel/UserSetupState;)V

    return-void
.end method
