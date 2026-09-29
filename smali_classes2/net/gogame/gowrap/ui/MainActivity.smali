.class public Lnet/gogame/gowrap/ui/MainActivity;
.super Lnet/gogame/gowrap/ui/AbstractMainActivity;
.source "MainActivity.java"


# instance fields
.field private navbar:Landroid/view/View;

.field private progressIndicator:Landroid/widget/ProgressBar;


# direct methods
.method public constructor <init>()V
    .locals 0

    .line 25
    invoke-direct {p0}, Lnet/gogame/gowrap/ui/AbstractMainActivity;-><init>()V

    return-void
.end method

.method static synthetic access$000(Lnet/gogame/gowrap/ui/MainActivity;)V
    .locals 0

    .line 25
    invoke-direct {p0}, Lnet/gogame/gowrap/ui/MainActivity;->showLanguageMenu()V

    return-void
.end method

.method private showLanguageMenu()V
    .locals 7

    .line 193
    invoke-virtual {p0}, Lnet/gogame/gowrap/ui/MainActivity;->canShowLanguageMenu()Z

    move-result v0

    if-nez v0, :cond_0

    return-void

    .line 197
    :cond_0
    sget-object v0, Lnet/gogame/gowrap/integrations/core/Wrapper;->INSTANCE:Lnet/gogame/gowrap/integrations/core/Wrapper;

    invoke-virtual {v0, p0}, Lnet/gogame/gowrap/integrations/core/Wrapper;->getCurrentLocale(Landroid/content/Context;)Ljava/lang/String;

    move-result-object v0

    .line 198
    iget-object v1, p0, Lnet/gogame/gowrap/ui/MainActivity;->localeManager:Lnet/gogame/gowrap/support/LocaleManager;

    .line 199
    invoke-virtual {v1}, Lnet/gogame/gowrap/support/LocaleManager;->getSupportedLocaleDescriptors()Ljava/util/List;

    move-result-object v1

    const/4 v2, -0x1

    const/4 v3, 0x0

    const/4 v4, 0x0

    .line 201
    :goto_0
    invoke-interface {v1}, Ljava/util/List;->size()I

    move-result v5

    if-ge v4, v5, :cond_2

    .line 202
    invoke-interface {v1, v4}, Ljava/util/List;->get(I)Ljava/lang/Object;

    move-result-object v5

    check-cast v5, Lnet/gogame/gowrap/support/LocaleDescriptor;

    .line 203
    invoke-virtual {v5}, Lnet/gogame/gowrap/support/LocaleDescriptor;->getId()Ljava/lang/String;

    move-result-object v5

    invoke-static {v0, v5}, Lnet/gogame/gowrap/support/StringUtils;->isEquals(Ljava/lang/String;Ljava/lang/String;)Z

    move-result v5

    if-eqz v5, :cond_1

    move v2, v4

    goto :goto_1

    :cond_1
    add-int/lit8 v4, v4, 0x1

    goto :goto_0

    .line 210
    :cond_2
    :goto_1
    new-instance v0, Landroid/widget/ArrayAdapter;

    sget v1, Lnet/gogame/gowrap/R$layout;->net_gogame_gowrap_default_listview_item:I

    sget v4, Lnet/gogame/gowrap/R$id;->net_gogame_gowrap_text_view:I

    iget-object v5, p0, Lnet/gogame/gowrap/ui/MainActivity;->localeManager:Lnet/gogame/gowrap/support/LocaleManager;

    .line 212
    invoke-virtual {v5}, Lnet/gogame/gowrap/support/LocaleManager;->getSupportedLocaleDescriptors()Ljava/util/List;

    move-result-object v5

    invoke-direct {v0, p0, v1, v4, v5}, Landroid/widget/ArrayAdapter;-><init>(Landroid/content/Context;IILjava/util/List;)V

    .line 213
    invoke-virtual {p0}, Lnet/gogame/gowrap/ui/MainActivity;->getLayoutInflater()Landroid/view/LayoutInflater;

    move-result-object v1

    sget v4, Lnet/gogame/gowrap/R$layout;->net_gogame_gowrap_default_listview:I

    const/4 v5, 0x0

    .line 214
    invoke-virtual {v1, v4, v5, v3}, Landroid/view/LayoutInflater;->inflate(ILandroid/view/ViewGroup;Z)Landroid/view/View;

    move-result-object v1

    .line 215
    sget v3, Lnet/gogame/gowrap/R$id;->net_gogame_gowrap_list_view:I

    invoke-virtual {v1, v3}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object v3

    check-cast v3, Landroid/widget/ListView;

    .line 217
    invoke-virtual {v3, v0}, Landroid/widget/ListView;->setAdapter(Landroid/widget/ListAdapter;)V

    const/4 v4, 0x1

    if-ltz v2, :cond_3

    .line 219
    invoke-virtual {v3, v2, v4}, Landroid/widget/ListView;->setItemChecked(IZ)V

    .line 222
    :cond_3
    new-instance v5, Landroid/app/Dialog;

    sget v6, Lnet/gogame/gowrap/R$style;->net_gogame_gowrap_dialog:I

    invoke-direct {v5, p0, v6}, Landroid/app/Dialog;-><init>(Landroid/content/Context;I)V

    .line 223
    invoke-virtual {v5, v4}, Landroid/app/Dialog;->setCanceledOnTouchOutside(Z)V

    .line 224
    invoke-virtual {v5, v1}, Landroid/app/Dialog;->setContentView(Landroid/view/View;)V

    .line 226
    new-instance v1, Lnet/gogame/gowrap/ui/MainActivity$10;

    invoke-direct {v1, p0, v2, v0, v5}, Lnet/gogame/gowrap/ui/MainActivity$10;-><init>(Lnet/gogame/gowrap/ui/MainActivity;ILandroid/widget/ArrayAdapter;Landroid/app/Dialog;)V

    invoke-virtual {v3, v1}, Landroid/widget/ListView;->setOnItemClickListener(Landroid/widget/AdapterView$OnItemClickListener;)V

    .line 240
    invoke-virtual {v5}, Landroid/app/Dialog;->show()V

    return-void
.end method


# virtual methods
.method protected enableOffers()V
    .locals 2

    const-string v0, "goWrap"

    const-string v1, "Offers enabled"

    .line 164
    invoke-static {v0, v1}, Landroid/util/Log;->v(Ljava/lang/String;Ljava/lang/String;)I

    .line 165
    sget v0, Lnet/gogame/gowrap/R$id;->net_gogame_gowrap_offers_button:I

    invoke-virtual {p0, v0}, Lnet/gogame/gowrap/ui/MainActivity;->findViewById(I)Landroid/view/View;

    move-result-object v0

    const/4 v1, 0x0

    .line 166
    invoke-virtual {v0, v1}, Landroid/view/View;->setVisibility(I)V

    return-void
.end method

.method protected getFragmentContainerViewId()I
    .locals 1

    .line 149
    sget v0, Lnet/gogame/gowrap/R$id;->net_gogame_gowrap_main_fragment_container:I

    return v0
.end method

.method public getGuid()Ljava/lang/String;
    .locals 1

    .line 173
    sget-object v0, Lnet/gogame/gowrap/GoWrapImpl;->INSTANCE:Lnet/gogame/gowrap/GoWrapImpl;

    invoke-virtual {v0}, Lnet/gogame/gowrap/GoWrapImpl;->getGuid()Ljava/lang/String;

    move-result-object v0

    return-object v0
.end method

.method protected onCreate(Landroid/os/Bundle;)V
    .locals 1

    .line 34
    invoke-super {p0, p1}, Lnet/gogame/gowrap/ui/AbstractMainActivity;->onCreate(Landroid/os/Bundle;)V

    .line 36
    sget p1, Lnet/gogame/gowrap/R$layout;->net_gogame_gowrap_main_ui:I

    invoke-virtual {p0, p1}, Lnet/gogame/gowrap/ui/MainActivity;->setContentView(I)V

    .line 40
    sget p1, Lnet/gogame/gowrap/R$id;->net_gogame_gowrap_navbar:I

    invoke-virtual {p0, p1}, Lnet/gogame/gowrap/ui/MainActivity;->findViewById(I)Landroid/view/View;

    move-result-object p1

    iput-object p1, p0, Lnet/gogame/gowrap/ui/MainActivity;->navbar:Landroid/view/View;

    .line 41
    iget-object p1, p0, Lnet/gogame/gowrap/ui/MainActivity;->navbar:Landroid/view/View;

    new-instance v0, Lnet/gogame/gowrap/ui/MainActivity$1;

    invoke-direct {v0, p0}, Lnet/gogame/gowrap/ui/MainActivity$1;-><init>(Lnet/gogame/gowrap/ui/MainActivity;)V

    invoke-virtual {p1, v0}, Landroid/view/View;->setOnClickListener(Landroid/view/View$OnClickListener;)V

    .line 54
    sget p1, Lnet/gogame/gowrap/R$id;->net_gogame_gowrap_back_button:I

    invoke-virtual {p0, p1}, Lnet/gogame/gowrap/ui/MainActivity;->findViewById(I)Landroid/view/View;

    move-result-object p1

    .line 55
    new-instance v0, Lnet/gogame/gowrap/ui/MainActivity$2;

    invoke-direct {v0, p0}, Lnet/gogame/gowrap/ui/MainActivity$2;-><init>(Lnet/gogame/gowrap/ui/MainActivity;)V

    invoke-virtual {p1, v0}, Landroid/view/View;->setOnClickListener(Landroid/view/View$OnClickListener;)V

    .line 63
    sget p1, Lnet/gogame/gowrap/R$id;->net_gogame_gowrap_info_button:I

    invoke-virtual {p0, p1}, Lnet/gogame/gowrap/ui/MainActivity;->findViewById(I)Landroid/view/View;

    move-result-object p1

    .line 64
    new-instance v0, Lnet/gogame/gowrap/ui/MainActivity$3;

    invoke-direct {v0, p0}, Lnet/gogame/gowrap/ui/MainActivity$3;-><init>(Lnet/gogame/gowrap/ui/MainActivity;)V

    invoke-virtual {p1, v0}, Landroid/view/View;->setOnClickListener(Landroid/view/View$OnClickListener;)V

    .line 72
    sget p1, Lnet/gogame/gowrap/R$id;->net_gogame_gowrap_language_button:I

    invoke-virtual {p0, p1}, Lnet/gogame/gowrap/ui/MainActivity;->findViewById(I)Landroid/view/View;

    move-result-object p1

    .line 73
    new-instance v0, Lnet/gogame/gowrap/ui/MainActivity$4;

    invoke-direct {v0, p0}, Lnet/gogame/gowrap/ui/MainActivity$4;-><init>(Lnet/gogame/gowrap/ui/MainActivity;)V

    invoke-virtual {p1, v0}, Landroid/view/View;->setOnClickListener(Landroid/view/View$OnClickListener;)V

    .line 80
    invoke-virtual {p0}, Lnet/gogame/gowrap/ui/MainActivity;->canShowLanguageMenu()Z

    move-result v0

    if-eqz v0, :cond_0

    const/4 v0, 0x0

    .line 81
    invoke-virtual {p1, v0}, Landroid/view/View;->setVisibility(I)V

    goto :goto_0

    :cond_0
    const/16 v0, 0x8

    .line 83
    invoke-virtual {p1, v0}, Landroid/view/View;->setVisibility(I)V

    .line 86
    :goto_0
    sget p1, Lnet/gogame/gowrap/R$id;->net_gogame_gowrap_community_button:I

    invoke-virtual {p0, p1}, Lnet/gogame/gowrap/ui/MainActivity;->findViewById(I)Landroid/view/View;

    move-result-object p1

    .line 87
    new-instance v0, Lnet/gogame/gowrap/ui/MainActivity$5;

    invoke-direct {v0, p0}, Lnet/gogame/gowrap/ui/MainActivity$5;-><init>(Lnet/gogame/gowrap/ui/MainActivity;)V

    invoke-virtual {p1, v0}, Landroid/view/View;->setOnClickListener(Landroid/view/View$OnClickListener;)V

    .line 98
    sget p1, Lnet/gogame/gowrap/R$id;->net_gogame_gowrap_help_button:I

    invoke-virtual {p0, p1}, Lnet/gogame/gowrap/ui/MainActivity;->findViewById(I)Landroid/view/View;

    move-result-object p1

    .line 99
    new-instance v0, Lnet/gogame/gowrap/ui/MainActivity$6;

    invoke-direct {v0, p0}, Lnet/gogame/gowrap/ui/MainActivity$6;-><init>(Lnet/gogame/gowrap/ui/MainActivity;)V

    invoke-virtual {p1, v0}, Landroid/view/View;->setOnClickListener(Landroid/view/View$OnClickListener;)V

    .line 110
    sget p1, Lnet/gogame/gowrap/R$id;->net_gogame_gowrap_contact_button:I

    invoke-virtual {p0, p1}, Lnet/gogame/gowrap/ui/MainActivity;->findViewById(I)Landroid/view/View;

    move-result-object p1

    .line 111
    new-instance v0, Lnet/gogame/gowrap/ui/MainActivity$7;

    invoke-direct {v0, p0}, Lnet/gogame/gowrap/ui/MainActivity$7;-><init>(Lnet/gogame/gowrap/ui/MainActivity;)V

    invoke-virtual {p1, v0}, Landroid/view/View;->setOnClickListener(Landroid/view/View$OnClickListener;)V

    .line 122
    sget p1, Lnet/gogame/gowrap/R$id;->net_gogame_gowrap_offers_button:I

    invoke-virtual {p0, p1}, Lnet/gogame/gowrap/ui/MainActivity;->findViewById(I)Landroid/view/View;

    move-result-object p1

    .line 123
    new-instance v0, Lnet/gogame/gowrap/ui/MainActivity$8;

    invoke-direct {v0, p0}, Lnet/gogame/gowrap/ui/MainActivity$8;-><init>(Lnet/gogame/gowrap/ui/MainActivity;)V

    invoke-virtual {p1, v0}, Landroid/view/View;->setOnClickListener(Landroid/view/View$OnClickListener;)V

    .line 133
    sget p1, Lnet/gogame/gowrap/R$id;->net_gogame_gowrap_close_button:I

    invoke-virtual {p0, p1}, Lnet/gogame/gowrap/ui/MainActivity;->findViewById(I)Landroid/view/View;

    move-result-object p1

    .line 134
    new-instance v0, Lnet/gogame/gowrap/ui/MainActivity$9;

    invoke-direct {v0, p0}, Lnet/gogame/gowrap/ui/MainActivity$9;-><init>(Lnet/gogame/gowrap/ui/MainActivity;)V

    invoke-virtual {p1, v0}, Landroid/view/View;->setOnClickListener(Landroid/view/View$OnClickListener;)V

    .line 142
    invoke-virtual {p0}, Lnet/gogame/gowrap/ui/MainActivity;->showInitialFragment()V

    return-void
.end method

.method protected onEnterFullscreen()V
    .locals 2

    .line 154
    iget-object v0, p0, Lnet/gogame/gowrap/ui/MainActivity;->navbar:Landroid/view/View;

    const/16 v1, 0x8

    invoke-virtual {v0, v1}, Landroid/view/View;->setVisibility(I)V

    return-void
.end method

.method protected onExitFullscreen()V
    .locals 2

    .line 159
    iget-object v0, p0, Lnet/gogame/gowrap/ui/MainActivity;->navbar:Landroid/view/View;

    const/4 v1, 0x0

    invoke-virtual {v0, v1}, Landroid/view/View;->setVisibility(I)V

    return-void
.end method

.method public onLoadingFinished()V
    .locals 2

    .line 185
    iget-object v0, p0, Lnet/gogame/gowrap/ui/MainActivity;->progressIndicator:Landroid/widget/ProgressBar;

    if-eqz v0, :cond_0

    .line 186
    iget-object v0, p0, Lnet/gogame/gowrap/ui/MainActivity;->progressIndicator:Landroid/widget/ProgressBar;

    const/16 v1, 0x8

    invoke-virtual {v0, v1}, Landroid/widget/ProgressBar;->setVisibility(I)V

    :cond_0
    return-void
.end method

.method public onLoadingStarted()V
    .locals 2

    .line 178
    iget-object v0, p0, Lnet/gogame/gowrap/ui/MainActivity;->progressIndicator:Landroid/widget/ProgressBar;

    if-eqz v0, :cond_0

    .line 179
    iget-object v0, p0, Lnet/gogame/gowrap/ui/MainActivity;->progressIndicator:Landroid/widget/ProgressBar;

    const/4 v1, 0x0

    invoke-virtual {v0, v1}, Landroid/widget/ProgressBar;->setVisibility(I)V

    :cond_0
    return-void
.end method
