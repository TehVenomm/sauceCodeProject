.class public Lnet/gogame/gowrap/ui/v2017_1/CommunityFragment;
.super Landroid/app/Fragment;
.source "CommunityFragment.java"


# direct methods
.method public constructor <init>()V
    .locals 0

    .line 16
    invoke-direct {p0}, Landroid/app/Fragment;-><init>()V

    return-void
.end method

.method private setup(Landroid/view/View;ILjava/lang/String;)V
    .locals 1

    const/4 v0, 0x1

    .line 46
    invoke-direct {p0, p1, p2, p3, v0}, Lnet/gogame/gowrap/ui/v2017_1/CommunityFragment;->setup(Landroid/view/View;ILjava/lang/String;Z)V

    return-void
.end method

.method private setup(Landroid/view/View;ILjava/lang/String;Z)V
    .locals 0

    .line 50
    invoke-virtual {p1, p2}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object p1

    if-nez p1, :cond_0

    return-void

    .line 54
    :cond_0
    invoke-static {p3}, Lnet/gogame/gowrap/support/StringUtils;->trimToNull(Ljava/lang/String;)Ljava/lang/String;

    move-result-object p2

    if-eqz p2, :cond_1

    .line 55
    invoke-virtual {p0}, Lnet/gogame/gowrap/ui/v2017_1/CommunityFragment;->getActivity()Landroid/app/Activity;

    move-result-object p3

    instance-of p3, p3, Lnet/gogame/gowrap/ui/UIContext;

    if-eqz p3, :cond_1

    .line 56
    invoke-virtual {p0}, Lnet/gogame/gowrap/ui/v2017_1/CommunityFragment;->getActivity()Landroid/app/Activity;

    move-result-object p3

    check-cast p3, Lnet/gogame/gowrap/ui/UIContext;

    const/4 p3, 0x0

    .line 57
    invoke-virtual {p1, p3}, Landroid/view/View;->setVisibility(I)V

    .line 58
    new-instance p3, Lnet/gogame/gowrap/ui/v2017_1/CommunityFragment$1;

    invoke-direct {p3, p0, p2}, Lnet/gogame/gowrap/ui/v2017_1/CommunityFragment$1;-><init>(Lnet/gogame/gowrap/ui/v2017_1/CommunityFragment;Ljava/lang/String;)V

    invoke-virtual {p1, p3}, Landroid/view/View;->setOnClickListener(Landroid/view/View$OnClickListener;)V

    goto :goto_0

    :cond_1
    if-eqz p4, :cond_2

    const/16 p2, 0x8

    .line 66
    invoke-virtual {p1, p2}, Landroid/view/View;->setVisibility(I)V

    :cond_2
    :goto_0
    return-void
.end method


# virtual methods
.method public onCreateView(Landroid/view/LayoutInflater;Landroid/view/ViewGroup;Landroid/os/Bundle;)Landroid/view/View;
    .locals 2

    .line 20
    sget p3, Lnet/gogame/gowrap/R$layout;->net_gogame_gowrap_fragment_community:I

    const/4 v0, 0x0

    invoke-virtual {p1, p3, p2, v0}, Landroid/view/LayoutInflater;->inflate(ILandroid/view/ViewGroup;Z)Landroid/view/View;

    move-result-object p1

    .line 23
    sget-object p2, Lnet/gogame/gowrap/integrations/core/Wrapper;->INSTANCE:Lnet/gogame/gowrap/integrations/core/Wrapper;

    .line 24
    invoke-virtual {p0}, Lnet/gogame/gowrap/ui/v2017_1/CommunityFragment;->getActivity()Landroid/app/Activity;

    move-result-object p3

    invoke-virtual {p2, p3}, Lnet/gogame/gowrap/integrations/core/Wrapper;->getLocaleConfiguration(Landroid/content/Context;)Lnet/gogame/gowrap/model/configuration/Configuration$Integrations$Core$LocaleConfiguration;

    move-result-object p2

    if-eqz p2, :cond_0

    .line 26
    sget p3, Lnet/gogame/gowrap/R$id;->net_gogame_gowrap_community_header:I

    .line 27
    invoke-virtual {p2}, Lnet/gogame/gowrap/model/configuration/Configuration$Integrations$Core$LocaleConfiguration;->getWhatsNewUrl()Ljava/lang/String;

    move-result-object v1

    .line 26
    invoke-direct {p0, p1, p3, v1, v0}, Lnet/gogame/gowrap/ui/v2017_1/CommunityFragment;->setup(Landroid/view/View;ILjava/lang/String;Z)V

    .line 28
    sget p3, Lnet/gogame/gowrap/R$id;->net_gogame_gowrap_facebook_button:I

    .line 29
    invoke-virtual {p2}, Lnet/gogame/gowrap/model/configuration/Configuration$Integrations$Core$LocaleConfiguration;->getFacebookUrl()Ljava/lang/String;

    move-result-object v0

    .line 28
    invoke-direct {p0, p1, p3, v0}, Lnet/gogame/gowrap/ui/v2017_1/CommunityFragment;->setup(Landroid/view/View;ILjava/lang/String;)V

    .line 30
    sget p3, Lnet/gogame/gowrap/R$id;->net_gogame_gowrap_twitter_button:I

    .line 31
    invoke-virtual {p2}, Lnet/gogame/gowrap/model/configuration/Configuration$Integrations$Core$LocaleConfiguration;->getTwitterUrl()Ljava/lang/String;

    move-result-object v0

    .line 30
    invoke-direct {p0, p1, p3, v0}, Lnet/gogame/gowrap/ui/v2017_1/CommunityFragment;->setup(Landroid/view/View;ILjava/lang/String;)V

    .line 32
    sget p3, Lnet/gogame/gowrap/R$id;->net_gogame_gowrap_instagram_button:I

    .line 33
    invoke-virtual {p2}, Lnet/gogame/gowrap/model/configuration/Configuration$Integrations$Core$LocaleConfiguration;->getInstagramUrl()Ljava/lang/String;

    move-result-object v0

    .line 32
    invoke-direct {p0, p1, p3, v0}, Lnet/gogame/gowrap/ui/v2017_1/CommunityFragment;->setup(Landroid/view/View;ILjava/lang/String;)V

    .line 34
    sget p3, Lnet/gogame/gowrap/R$id;->net_gogame_gowrap_youtube_button:I

    .line 35
    invoke-virtual {p2}, Lnet/gogame/gowrap/model/configuration/Configuration$Integrations$Core$LocaleConfiguration;->getYoutubeUrl()Ljava/lang/String;

    move-result-object v0

    .line 34
    invoke-direct {p0, p1, p3, v0}, Lnet/gogame/gowrap/ui/v2017_1/CommunityFragment;->setup(Landroid/view/View;ILjava/lang/String;)V

    .line 36
    sget p3, Lnet/gogame/gowrap/R$id;->net_gogame_gowrap_wiki_button:I

    .line 37
    invoke-virtual {p2}, Lnet/gogame/gowrap/model/configuration/Configuration$Integrations$Core$LocaleConfiguration;->getWikiUrl()Ljava/lang/String;

    move-result-object v0

    .line 36
    invoke-direct {p0, p1, p3, v0}, Lnet/gogame/gowrap/ui/v2017_1/CommunityFragment;->setup(Landroid/view/View;ILjava/lang/String;)V

    .line 38
    sget p3, Lnet/gogame/gowrap/R$id;->net_gogame_gowrap_forum_button:I

    .line 39
    invoke-virtual {p2}, Lnet/gogame/gowrap/model/configuration/Configuration$Integrations$Core$LocaleConfiguration;->getForumUrl()Ljava/lang/String;

    move-result-object p2

    .line 38
    invoke-direct {p0, p1, p3, p2}, Lnet/gogame/gowrap/ui/v2017_1/CommunityFragment;->setup(Landroid/view/View;ILjava/lang/String;)V

    :cond_0
    return-object p1
.end method
