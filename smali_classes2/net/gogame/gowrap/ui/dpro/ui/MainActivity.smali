.class public Lnet/gogame/gowrap/ui/dpro/ui/MainActivity;
.super Lnet/gogame/gowrap/ui/AbstractMainActivity;
.source "MainActivity.java"


# instance fields
.field private progressIndicator:Landroid/widget/ProgressBar;


# direct methods
.method public constructor <init>()V
    .locals 0

    .line 18
    invoke-direct {p0}, Lnet/gogame/gowrap/ui/AbstractMainActivity;-><init>()V

    return-void
.end method

.method static synthetic access$000(Lnet/gogame/gowrap/ui/dpro/ui/MainActivity;)V
    .locals 0

    .line 18
    invoke-direct {p0}, Lnet/gogame/gowrap/ui/dpro/ui/MainActivity;->showNews()V

    return-void
.end method

.method private showNews()V
    .locals 1

    .line 88
    invoke-virtual {p0}, Lnet/gogame/gowrap/ui/dpro/ui/MainActivity;->isUseNews2017_2()Z

    move-result v0

    if-eqz v0, :cond_0

    .line 89
    new-instance v0, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;

    invoke-direct {v0}, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;-><init>()V

    invoke-virtual {p0, v0}, Lnet/gogame/gowrap/ui/dpro/ui/MainActivity;->pushFragment(Landroid/app/Fragment;)V

    goto :goto_0

    .line 91
    :cond_0
    new-instance v0, Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment;

    invoke-direct {v0}, Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment;-><init>()V

    invoke-virtual {p0, v0}, Lnet/gogame/gowrap/ui/dpro/ui/MainActivity;->pushFragment(Landroid/app/Fragment;)V

    :goto_0
    return-void
.end method


# virtual methods
.method protected enableOffers()V
    .locals 0

    return-void
.end method

.method protected getFragmentContainerViewId()I
    .locals 1

    .line 99
    sget v0, Lnet/gogame/gowrap/ui/dpro/R$id;->net_gogame_gowrap_main_fragment_container:I

    return v0
.end method

.method public getGuid()Ljava/lang/String;
    .locals 1

    .line 121
    sget-object v0, Lnet/gogame/gowrap/GoWrapImpl;->INSTANCE:Lnet/gogame/gowrap/GoWrapImpl;

    invoke-virtual {v0}, Lnet/gogame/gowrap/GoWrapImpl;->getGuid()Ljava/lang/String;

    move-result-object v0

    return-object v0
.end method

.method protected onCreate(Landroid/os/Bundle;)V
    .locals 1

    .line 26
    invoke-super {p0, p1}, Lnet/gogame/gowrap/ui/AbstractMainActivity;->onCreate(Landroid/os/Bundle;)V

    .line 28
    sget p1, Lnet/gogame/gowrap/ui/dpro/R$layout;->net_gogame_gowrap_dpro_main_ui:I

    invoke-virtual {p0, p1}, Lnet/gogame/gowrap/ui/dpro/ui/MainActivity;->setContentView(I)V

    .line 32
    sget p1, Lnet/gogame/gowrap/ui/dpro/R$id;->net_gogame_gowrap_main_tabbed_panel:I

    invoke-virtual {p0, p1}, Lnet/gogame/gowrap/ui/dpro/ui/MainActivity;->findViewById(I)Landroid/view/View;

    move-result-object p1

    check-cast p1, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;

    .line 34
    new-instance v0, Lnet/gogame/gowrap/ui/dpro/ui/MainActivity$1;

    invoke-direct {v0, p0}, Lnet/gogame/gowrap/ui/dpro/ui/MainActivity$1;-><init>(Lnet/gogame/gowrap/ui/dpro/ui/MainActivity;)V

    invoke-virtual {p1, v0}, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;->setListener(Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel$Listener;)V

    .line 69
    sget p1, Lnet/gogame/gowrap/ui/dpro/R$id;->net_gogame_gowrap_main_sns_button:I

    invoke-virtual {p0, p1}, Lnet/gogame/gowrap/ui/dpro/ui/MainActivity;->findViewById(I)Landroid/view/View;

    move-result-object p1

    .line 70
    new-instance v0, Lnet/gogame/gowrap/ui/dpro/ui/MainActivity$2;

    invoke-direct {v0, p0}, Lnet/gogame/gowrap/ui/dpro/ui/MainActivity$2;-><init>(Lnet/gogame/gowrap/ui/dpro/ui/MainActivity;)V

    invoke-virtual {p1, v0}, Landroid/view/View;->setOnClickListener(Landroid/view/View$OnClickListener;)V

    .line 84
    invoke-direct {p0}, Lnet/gogame/gowrap/ui/dpro/ui/MainActivity;->showNews()V

    return-void
.end method

.method protected onEnterFullscreen()V
    .locals 0

    return-void
.end method

.method protected onExitFullscreen()V
    .locals 0

    return-void
.end method

.method public onLoadingFinished()V
    .locals 2

    .line 133
    iget-object v0, p0, Lnet/gogame/gowrap/ui/dpro/ui/MainActivity;->progressIndicator:Landroid/widget/ProgressBar;

    if-eqz v0, :cond_0

    .line 134
    iget-object v0, p0, Lnet/gogame/gowrap/ui/dpro/ui/MainActivity;->progressIndicator:Landroid/widget/ProgressBar;

    const/16 v1, 0x8

    invoke-virtual {v0, v1}, Landroid/widget/ProgressBar;->setVisibility(I)V

    :cond_0
    return-void
.end method

.method public onLoadingStarted()V
    .locals 2

    .line 126
    iget-object v0, p0, Lnet/gogame/gowrap/ui/dpro/ui/MainActivity;->progressIndicator:Landroid/widget/ProgressBar;

    if-eqz v0, :cond_0

    .line 127
    iget-object v0, p0, Lnet/gogame/gowrap/ui/dpro/ui/MainActivity;->progressIndicator:Landroid/widget/ProgressBar;

    const/4 v1, 0x0

    invoke-virtual {v0, v1}, Landroid/widget/ProgressBar;->setVisibility(I)V

    :cond_0
    return-void
.end method
