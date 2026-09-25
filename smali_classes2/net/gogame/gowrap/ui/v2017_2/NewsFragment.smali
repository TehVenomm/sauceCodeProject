.class public Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;
.super Landroid/app/Fragment;
.source "NewsFragment.java"


# annotations
.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Lnet/gogame/gowrap/ui/v2017_2/NewsFragment$BannerTimerTask;
    }
.end annotation


# static fields
.field private static final BANNER_AUTOROTATE_PERIOD:J = 0xfa0L

.field private static final KEY_BANNERS:Ljava/lang/String; = "banners"

.field private static final KEY_BANNER_VIEW:Ljava/lang/String; = "bannerView"

.field private static final KEY_LIST_ADAPTER:Ljava/lang/String; = "listAdapter"

.field private static final KEY_LIST_VIEW:Ljava/lang/String; = "listView"


# instance fields
.field private bannerPeriodTextView:Landroid/widget/TextView;

.field private bannerView:Landroid/widget/ImageSwitcher;

.field private banners:Ljava/util/ArrayList;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/ArrayList<",
            "Lnet/gogame/gowrap/model/news/Banner;",
            ">;"
        }
    .end annotation
.end field

.field private currentBannerIndex:I

.field private final dateTimeFormat:Ljava/text/DateFormat;

.field private listAdapter:Lnet/gogame/gowrap/ui/v2017_2/NewsListAdapter;

.field private listView:Landroid/widget/ListView;

.field private progressBar:Landroid/widget/ProgressBar;

.field private reverseBannerSlideDirection:Z

.field private savedInstanceState:Landroid/os/Bundle;

.field private timer:Ljava/util/Timer;

.field private uiContext:Lnet/gogame/gowrap/ui/UIContext;


# direct methods
.method public constructor <init>()V
    .locals 2

    .line 57
    invoke-direct {p0}, Landroid/app/Fragment;-><init>()V

    .line 65
    new-instance v0, Ljava/text/SimpleDateFormat;

    const-string v1, "d/M/y HH:mm"

    invoke-direct {v0, v1}, Ljava/text/SimpleDateFormat;-><init>(Ljava/lang/String;)V

    iput-object v0, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;->dateTimeFormat:Ljava/text/DateFormat;

    const/4 v0, 0x0

    .line 76
    iput-boolean v0, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;->reverseBannerSlideDirection:Z

    return-void
.end method

.method static synthetic access$000(Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;)Lnet/gogame/gowrap/ui/v2017_2/NewsListAdapter;
    .locals 0

    .line 57
    iget-object p0, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;->listAdapter:Lnet/gogame/gowrap/ui/v2017_2/NewsListAdapter;

    return-object p0
.end method

.method static synthetic access$100(Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;)Lnet/gogame/gowrap/ui/UIContext;
    .locals 0

    .line 57
    iget-object p0, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;->uiContext:Lnet/gogame/gowrap/ui/UIContext;

    return-object p0
.end method

.method static synthetic access$1000(Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;Lnet/gogame/gowrap/model/news/NewsFeed;)V
    .locals 0

    .line 57
    invoke-direct {p0, p1}, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;->updateUI(Lnet/gogame/gowrap/model/news/NewsFeed;)V

    return-void
.end method

.method static synthetic access$1200(Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;)Ljava/text/DateFormat;
    .locals 0

    .line 57
    iget-object p0, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;->dateTimeFormat:Ljava/text/DateFormat;

    return-object p0
.end method

.method static synthetic access$1300(Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;)Landroid/widget/TextView;
    .locals 0

    .line 57
    iget-object p0, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;->bannerPeriodTextView:Landroid/widget/TextView;

    return-object p0
.end method

.method static synthetic access$200(Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;)Landroid/widget/ImageSwitcher;
    .locals 0

    .line 57
    iget-object p0, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;->bannerView:Landroid/widget/ImageSwitcher;

    return-object p0
.end method

.method static synthetic access$300(Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;)Ljava/util/ArrayList;
    .locals 0

    .line 57
    iget-object p0, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;->banners:Ljava/util/ArrayList;

    return-object p0
.end method

.method static synthetic access$400(Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;)I
    .locals 0

    .line 57
    iget p0, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;->currentBannerIndex:I

    return p0
.end method

.method static synthetic access$500(Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;Ljava/lang/String;)V
    .locals 0

    .line 57
    invoke-direct {p0, p1}, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;->launchUrl(Ljava/lang/String;)V

    return-void
.end method

.method static synthetic access$602(Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;Z)Z
    .locals 0

    .line 57
    iput-boolean p1, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;->reverseBannerSlideDirection:Z

    return p1
.end method

.method static synthetic access$700(Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;)V
    .locals 0

    .line 57
    invoke-direct {p0}, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;->advanceBanner()V

    return-void
.end method

.method static synthetic access$800(Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;)Landroid/widget/ProgressBar;
    .locals 0

    .line 57
    iget-object p0, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;->progressBar:Landroid/widget/ProgressBar;

    return-object p0
.end method

.method private advanceBanner()V
    .locals 1

    .line 329
    iget-boolean v0, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;->reverseBannerSlideDirection:Z

    if-eqz v0, :cond_0

    .line 330
    invoke-direct {p0}, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;->showPreviousBanner()V

    goto :goto_0

    .line 332
    :cond_0
    invoke-direct {p0}, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;->showNextBanner()V

    :goto_0
    return-void
.end method

.method private doShowBanner(Lnet/gogame/gowrap/model/news/Banner;)V
    .locals 2

    .line 384
    iget-object v0, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;->uiContext:Lnet/gogame/gowrap/ui/UIContext;

    if-eqz v0, :cond_1

    if-nez p1, :cond_0

    goto :goto_0

    .line 387
    :cond_0
    invoke-virtual {p0}, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;->getActivity()Landroid/app/Activity;

    move-result-object v0

    new-instance v1, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment$6;

    invoke-direct {v1, p0, p1}, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment$6;-><init>(Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;Lnet/gogame/gowrap/model/news/Banner;)V

    invoke-virtual {v0, v1}, Landroid/app/Activity;->runOnUiThread(Ljava/lang/Runnable;)V

    .line 409
    iget-object v0, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;->uiContext:Lnet/gogame/gowrap/ui/UIContext;

    invoke-interface {v0}, Lnet/gogame/gowrap/ui/UIContext;->getDownloadManager()Lnet/gogame/gowrap/support/DownloadManager;

    move-result-object v0

    .line 411
    invoke-virtual {p1}, Lnet/gogame/gowrap/model/news/Banner;->getImageUrl()Ljava/lang/String;

    move-result-object p1

    invoke-static {p1}, Lnet/gogame/gowrap/support/DownloadManager$Request$Builder;->newBuilder(Ljava/lang/String;)Lnet/gogame/gowrap/support/DownloadManager$Request$Builder;

    move-result-object p1

    new-instance v1, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment$7;

    invoke-direct {v1, p0}, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment$7;-><init>(Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;)V

    .line 412
    invoke-virtual {p1, v1}, Lnet/gogame/gowrap/support/DownloadManager$Request$Builder;->into(Lnet/gogame/gowrap/support/DownloadManager$Target;)Lnet/gogame/gowrap/support/DownloadManager$Request;

    move-result-object p1

    .line 409
    invoke-interface {v0, p1}, Lnet/gogame/gowrap/support/DownloadManager;->download(Lnet/gogame/gowrap/support/DownloadManager$Request;)V

    return-void

    :cond_1
    :goto_0
    return-void
.end method

.method private getParcelable(Ljava/lang/String;)Landroid/os/Parcelable;
    .locals 1

    .line 79
    iget-object v0, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;->savedInstanceState:Landroid/os/Bundle;

    if-eqz v0, :cond_1

    if-nez p1, :cond_0

    goto :goto_0

    .line 82
    :cond_0
    iget-object v0, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;->savedInstanceState:Landroid/os/Bundle;

    invoke-virtual {v0, p1}, Landroid/os/Bundle;->getParcelable(Ljava/lang/String;)Landroid/os/Parcelable;

    move-result-object p1

    return-object p1

    :cond_1
    :goto_0
    const/4 p1, 0x0

    return-object p1
.end method

.method private isInPeriod(JLjava/lang/Long;Ljava/lang/Long;)Z
    .locals 2

    if-eqz p3, :cond_0

    .line 441
    invoke-virtual {p3}, Ljava/lang/Long;->longValue()J

    move-result-wide v0

    cmp-long p3, p1, v0

    if-ltz p3, :cond_1

    :cond_0
    if-eqz p4, :cond_2

    .line 442
    invoke-virtual {p4}, Ljava/lang/Long;->longValue()J

    move-result-wide p3

    cmp-long v0, p1, p3

    if-gtz v0, :cond_1

    goto :goto_0

    :cond_1
    const/4 p1, 0x0

    goto :goto_1

    :cond_2
    :goto_0
    const/4 p1, 0x1

    :goto_1
    return p1
.end method

.method private launchUrl(Ljava/lang/String;)V
    .locals 6

    if-nez p1, :cond_0

    return-void

    .line 520
    :cond_0
    :try_start_0
    invoke-static {p1}, Landroid/net/Uri;->parse(Ljava/lang/String;)Landroid/net/Uri;

    move-result-object v0

    .line 521
    invoke-virtual {v0}, Landroid/net/Uri;->getScheme()Ljava/lang/String;

    move-result-object v1

    const-string v2, "article"

    invoke-static {v1, v2}, Lnet/gogame/gowrap/support/StringUtils;->isEquals(Ljava/lang/String;Ljava/lang/String;)Z

    move-result v1
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    if-eqz v1, :cond_2

    .line 523
    :try_start_1
    invoke-virtual {v0}, Landroid/net/Uri;->getSchemeSpecificPart()Ljava/lang/String;

    move-result-object p1

    invoke-static {p1}, Ljava/lang/Long;->parseLong(Ljava/lang/String;)J

    move-result-wide v0

    const/4 p1, 0x0

    .line 524
    :goto_0
    iget-object v2, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;->listAdapter:Lnet/gogame/gowrap/ui/v2017_2/NewsListAdapter;

    invoke-virtual {v2}, Lnet/gogame/gowrap/ui/v2017_2/NewsListAdapter;->getCount()I

    move-result v2

    if-ge p1, v2, :cond_4

    .line 525
    iget-object v2, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;->listAdapter:Lnet/gogame/gowrap/ui/v2017_2/NewsListAdapter;

    invoke-virtual {v2, p1}, Lnet/gogame/gowrap/ui/v2017_2/NewsListAdapter;->getItem(I)Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Lnet/gogame/gowrap/model/news/Article;

    if-eqz v2, :cond_1

    .line 526
    invoke-virtual {v2}, Lnet/gogame/gowrap/model/news/Article;->getId()J

    move-result-wide v3

    cmp-long v5, v3, v0

    if-nez v5, :cond_1

    iget-object v3, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;->uiContext:Lnet/gogame/gowrap/ui/UIContext;

    if-eqz v3, :cond_1

    .line 527
    invoke-static {v2}, Lnet/gogame/gowrap/ui/v2017_2/NewsArticleFragment;->create(Lnet/gogame/gowrap/model/news/Article;)Lnet/gogame/gowrap/ui/v2017_2/NewsArticleFragment;

    move-result-object p1

    .line 528
    iget-object v0, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;->uiContext:Lnet/gogame/gowrap/ui/UIContext;

    invoke-interface {v0, p1}, Lnet/gogame/gowrap/ui/UIContext;->pushFragment(Landroid/app/Fragment;)V
    :try_end_1
    .catch Ljava/lang/Exception; {:try_start_1 .. :try_end_1} :catch_1

    goto :goto_1

    :cond_1
    add-int/lit8 p1, p1, 0x1

    goto :goto_0

    .line 535
    :cond_2
    :try_start_2
    sget-object v0, Lnet/gogame/gowrap/GoWrapImpl;->INSTANCE:Lnet/gogame/gowrap/GoWrapImpl;

    invoke-virtual {v0, p1}, Lnet/gogame/gowrap/GoWrapImpl;->handleCustomUri(Ljava/lang/String;)Z

    move-result v0

    if-eqz v0, :cond_3

    goto :goto_1

    .line 538
    :cond_3
    invoke-virtual {p0}, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;->getActivity()Landroid/app/Activity;

    move-result-object v0

    invoke-static {v0, p1}, Lnet/gogame/gowrap/ui/utils/ExternalAppLauncher;->openUrlInExternalBrowser(Landroid/app/Activity;Ljava/lang/String;)Z
    :try_end_2
    .catch Ljava/lang/Exception; {:try_start_2 .. :try_end_2} :catch_0

    goto :goto_1

    :catch_0
    move-exception p1

    const-string v0, "goWrap"

    const-string v1, "Exception"

    .line 541
    invoke-static {v0, v1, p1}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)I

    :catch_1
    :cond_4
    :goto_1
    return-void
.end method

.method private showBanner(I)V
    .locals 1

    .line 375
    iget-object v0, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;->banners:Ljava/util/ArrayList;

    if-eqz v0, :cond_1

    iget-object v0, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;->banners:Ljava/util/ArrayList;

    invoke-virtual {v0}, Ljava/util/ArrayList;->size()I

    move-result v0

    if-lt p1, v0, :cond_0

    goto :goto_0

    .line 378
    :cond_0
    iget-object v0, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;->banners:Ljava/util/ArrayList;

    invoke-virtual {v0, p1}, Ljava/util/ArrayList;->get(I)Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Lnet/gogame/gowrap/model/news/Banner;

    .line 379
    invoke-direct {p0, v0}, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;->doShowBanner(Lnet/gogame/gowrap/model/news/Banner;)V

    .line 380
    iput p1, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;->currentBannerIndex:I

    return-void

    :cond_1
    :goto_0
    return-void
.end method

.method private showNextBanner()V
    .locals 4

    .line 337
    iget-object v0, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;->banners:Ljava/util/ArrayList;

    if-eqz v0, :cond_2

    iget-object v0, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;->banners:Ljava/util/ArrayList;

    invoke-virtual {v0}, Ljava/util/ArrayList;->isEmpty()Z

    move-result v0

    if-eqz v0, :cond_0

    goto :goto_0

    .line 340
    :cond_0
    iget v0, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;->currentBannerIndex:I

    add-int/lit8 v0, v0, 0x1

    iget-object v1, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;->banners:Ljava/util/ArrayList;

    invoke-virtual {v1}, Ljava/util/ArrayList;->size()I

    move-result v1

    rem-int/2addr v0, v1

    .line 341
    iget v1, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;->currentBannerIndex:I

    if-ne v0, v1, :cond_1

    return-void

    .line 345
    :cond_1
    invoke-virtual {p0}, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;->getActivity()Landroid/app/Activity;

    move-result-object v1

    sget v2, Lnet/gogame/gowrap/R$anim;->net_gogame_gowrap_slide_in_right:I

    invoke-static {v1, v2}, Landroid/view/animation/AnimationUtils;->loadAnimation(Landroid/content/Context;I)Landroid/view/animation/Animation;

    move-result-object v1

    .line 347
    invoke-virtual {p0}, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;->getActivity()Landroid/app/Activity;

    move-result-object v2

    sget v3, Lnet/gogame/gowrap/R$anim;->net_gogame_gowrap_slide_out_left:I

    invoke-static {v2, v3}, Landroid/view/animation/AnimationUtils;->loadAnimation(Landroid/content/Context;I)Landroid/view/animation/Animation;

    move-result-object v2

    .line 349
    iget-object v3, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;->bannerView:Landroid/widget/ImageSwitcher;

    invoke-virtual {v3, v1}, Landroid/widget/ImageSwitcher;->setInAnimation(Landroid/view/animation/Animation;)V

    .line 350
    iget-object v1, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;->bannerView:Landroid/widget/ImageSwitcher;

    invoke-virtual {v1, v2}, Landroid/widget/ImageSwitcher;->setOutAnimation(Landroid/view/animation/Animation;)V

    .line 352
    invoke-direct {p0, v0}, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;->showBanner(I)V

    return-void

    :cond_2
    :goto_0
    return-void
.end method

.method private showPreviousBanner()V
    .locals 4

    .line 356
    iget-object v0, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;->banners:Ljava/util/ArrayList;

    if-eqz v0, :cond_2

    iget-object v0, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;->banners:Ljava/util/ArrayList;

    invoke-virtual {v0}, Ljava/util/ArrayList;->isEmpty()Z

    move-result v0

    if-eqz v0, :cond_0

    goto :goto_0

    .line 359
    :cond_0
    iget v0, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;->currentBannerIndex:I

    iget-object v1, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;->banners:Ljava/util/ArrayList;

    invoke-virtual {v1}, Ljava/util/ArrayList;->size()I

    move-result v1

    add-int/2addr v0, v1

    add-int/lit8 v0, v0, -0x1

    iget-object v1, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;->banners:Ljava/util/ArrayList;

    invoke-virtual {v1}, Ljava/util/ArrayList;->size()I

    move-result v1

    rem-int/2addr v0, v1

    .line 360
    iget v1, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;->currentBannerIndex:I

    if-ne v0, v1, :cond_1

    return-void

    .line 364
    :cond_1
    invoke-virtual {p0}, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;->getActivity()Landroid/app/Activity;

    move-result-object v1

    sget v2, Lnet/gogame/gowrap/R$anim;->net_gogame_gowrap_slide_in_left:I

    invoke-static {v1, v2}, Landroid/view/animation/AnimationUtils;->loadAnimation(Landroid/content/Context;I)Landroid/view/animation/Animation;

    move-result-object v1

    .line 366
    invoke-virtual {p0}, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;->getActivity()Landroid/app/Activity;

    move-result-object v2

    sget v3, Lnet/gogame/gowrap/R$anim;->net_gogame_gowrap_slide_out_right:I

    invoke-static {v2, v3}, Landroid/view/animation/AnimationUtils;->loadAnimation(Landroid/content/Context;I)Landroid/view/animation/Animation;

    move-result-object v2

    .line 368
    iget-object v3, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;->bannerView:Landroid/widget/ImageSwitcher;

    invoke-virtual {v3, v1}, Landroid/widget/ImageSwitcher;->setInAnimation(Landroid/view/animation/Animation;)V

    .line 369
    iget-object v1, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;->bannerView:Landroid/widget/ImageSwitcher;

    invoke-virtual {v1, v2}, Landroid/widget/ImageSwitcher;->setOutAnimation(Landroid/view/animation/Animation;)V

    .line 371
    invoke-direct {p0, v0}, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;->showBanner(I)V

    return-void

    :cond_2
    :goto_0
    return-void
.end method

.method private updateUI(Lnet/gogame/gowrap/model/news/NewsFeed;)V
    .locals 7

    if-nez p1, :cond_0

    .line 447
    iget-object p1, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;->bannerView:Landroid/widget/ImageSwitcher;

    const/4 v0, 0x0

    invoke-virtual {p1, v0}, Landroid/widget/ImageSwitcher;->setImageDrawable(Landroid/graphics/drawable/Drawable;)V

    .line 448
    iget-object p1, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;->listAdapter:Lnet/gogame/gowrap/ui/v2017_2/NewsListAdapter;

    invoke-virtual {p1, v0}, Lnet/gogame/gowrap/ui/v2017_2/NewsListAdapter;->setElements(Ljava/util/List;)V

    goto/16 :goto_2

    .line 450
    :cond_0
    invoke-static {}, Ljava/lang/System;->currentTimeMillis()J

    move-result-wide v0

    .line 452
    new-instance v2, Ljava/util/ArrayList;

    invoke-direct {v2}, Ljava/util/ArrayList;-><init>()V

    .line 453
    invoke-virtual {p1}, Lnet/gogame/gowrap/model/news/NewsFeed;->getBanners()Ljava/util/List;

    move-result-object v3

    if-eqz v3, :cond_2

    .line 454
    invoke-virtual {p1}, Lnet/gogame/gowrap/model/news/NewsFeed;->getBanners()Ljava/util/List;

    move-result-object v3

    invoke-interface {v3}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v3

    :cond_1
    :goto_0
    invoke-interface {v3}, Ljava/util/Iterator;->hasNext()Z

    move-result v4

    if-eqz v4, :cond_2

    invoke-interface {v3}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v4

    check-cast v4, Lnet/gogame/gowrap/model/news/Banner;

    if-eqz v4, :cond_1

    .line 455
    invoke-virtual {v4}, Lnet/gogame/gowrap/model/news/Banner;->getStartDateTime()Ljava/lang/Long;

    move-result-object v5

    .line 456
    invoke-virtual {v4}, Lnet/gogame/gowrap/model/news/Banner;->getEndDateTime()Ljava/lang/Long;

    move-result-object v6

    .line 455
    invoke-direct {p0, v0, v1, v5, v6}, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;->isInPeriod(JLjava/lang/Long;Ljava/lang/Long;)Z

    move-result v5

    if-eqz v5, :cond_1

    .line 457
    invoke-virtual {v2, v4}, Ljava/util/ArrayList;->add(Ljava/lang/Object;)Z

    goto :goto_0

    .line 461
    :cond_2
    iput-object v2, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;->banners:Ljava/util/ArrayList;

    .line 463
    new-instance v2, Ljava/util/ArrayList;

    invoke-direct {v2}, Ljava/util/ArrayList;-><init>()V

    .line 464
    invoke-virtual {p1}, Lnet/gogame/gowrap/model/news/NewsFeed;->getArticles()Ljava/util/List;

    move-result-object v3

    if-eqz v3, :cond_4

    .line 465
    invoke-virtual {p1}, Lnet/gogame/gowrap/model/news/NewsFeed;->getArticles()Ljava/util/List;

    move-result-object p1

    invoke-interface {p1}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object p1

    :cond_3
    :goto_1
    invoke-interface {p1}, Ljava/util/Iterator;->hasNext()Z

    move-result v3

    if-eqz v3, :cond_4

    invoke-interface {p1}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v3

    check-cast v3, Lnet/gogame/gowrap/model/news/Article;

    if-eqz v3, :cond_3

    .line 466
    invoke-virtual {v3}, Lnet/gogame/gowrap/model/news/Article;->getStartDateTime()Ljava/lang/Long;

    move-result-object v4

    .line 467
    invoke-virtual {v3}, Lnet/gogame/gowrap/model/news/Article;->getEndDateTime()Ljava/lang/Long;

    move-result-object v5

    .line 466
    invoke-direct {p0, v0, v1, v4, v5}, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;->isInPeriod(JLjava/lang/Long;Ljava/lang/Long;)Z

    move-result v4

    if-eqz v4, :cond_3

    .line 468
    invoke-interface {v2, v3}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    goto :goto_1

    .line 472
    :cond_4
    iget-object p1, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;->listAdapter:Lnet/gogame/gowrap/ui/v2017_2/NewsListAdapter;

    invoke-virtual {p1, v2}, Lnet/gogame/gowrap/ui/v2017_2/NewsListAdapter;->setElements(Ljava/util/List;)V

    const/4 p1, 0x0

    .line 474
    invoke-direct {p0, p1}, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;->showBanner(I)V

    :goto_2
    return-void
.end method


# virtual methods
.method public onCreateView(Landroid/view/LayoutInflater;Landroid/view/ViewGroup;Landroid/os/Bundle;)Landroid/view/View;
    .locals 4

    .line 87
    invoke-virtual {p0}, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;->getActivity()Landroid/app/Activity;

    move-result-object p3

    instance-of p3, p3, Lnet/gogame/gowrap/ui/UIContext;

    if-eqz p3, :cond_0

    .line 88
    invoke-virtual {p0}, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;->getActivity()Landroid/app/Activity;

    move-result-object p3

    check-cast p3, Lnet/gogame/gowrap/ui/UIContext;

    iput-object p3, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;->uiContext:Lnet/gogame/gowrap/ui/UIContext;

    .line 91
    :cond_0
    sget p3, Lnet/gogame/gowrap/R$layout;->net_gogame_gowrap_v2017_2_fragment_news:I

    const/4 v0, 0x0

    invoke-virtual {p1, p3, p2, v0}, Landroid/view/LayoutInflater;->inflate(ILandroid/view/ViewGroup;Z)Landroid/view/View;

    move-result-object p2

    .line 94
    sget p3, Lnet/gogame/gowrap/R$id;->net_gogame_gowrap_progress_indicator:I

    invoke-virtual {p2, p3}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object p3

    check-cast p3, Landroid/widget/ProgressBar;

    iput-object p3, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;->progressBar:Landroid/widget/ProgressBar;

    .line 96
    new-instance p3, Lnet/gogame/gowrap/ui/v2017_2/NewsListAdapter;

    invoke-virtual {p0}, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;->getActivity()Landroid/app/Activity;

    move-result-object v1

    invoke-direct {p3, v1}, Lnet/gogame/gowrap/ui/v2017_2/NewsListAdapter;-><init>(Landroid/content/Context;)V

    iput-object p3, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;->listAdapter:Lnet/gogame/gowrap/ui/v2017_2/NewsListAdapter;

    .line 97
    iget-object p3, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;->listAdapter:Lnet/gogame/gowrap/ui/v2017_2/NewsListAdapter;

    const-string v1, "listAdapter"

    invoke-direct {p0, v1}, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;->getParcelable(Ljava/lang/String;)Landroid/os/Parcelable;

    move-result-object v1

    invoke-virtual {p3, v1}, Lnet/gogame/gowrap/ui/v2017_2/NewsListAdapter;->onRestoreInstanceState(Landroid/os/Parcelable;)V

    .line 99
    sget p3, Lnet/gogame/gowrap/R$id;->net_gogame_gowrap_news_listview:I

    invoke-virtual {p2, p3}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object p3

    check-cast p3, Landroid/widget/ListView;

    iput-object p3, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;->listView:Landroid/widget/ListView;

    .line 100
    iget-object p3, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;->listView:Landroid/widget/ListView;

    iget-object v1, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;->listAdapter:Lnet/gogame/gowrap/ui/v2017_2/NewsListAdapter;

    invoke-virtual {p3, v1}, Landroid/widget/ListView;->setAdapter(Landroid/widget/ListAdapter;)V

    .line 101
    iget-object p3, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;->listView:Landroid/widget/ListView;

    new-instance v1, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment$1;

    invoke-direct {v1, p0}, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment$1;-><init>(Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;)V

    invoke-virtual {p3, v1}, Landroid/widget/ListView;->setOnItemClickListener(Landroid/widget/AdapterView$OnItemClickListener;)V

    .line 114
    sget p3, Lnet/gogame/gowrap/R$id;->net_gogame_gowrap_news_banners:I

    invoke-virtual {p2, p3}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object p3

    check-cast p3, Landroid/widget/ImageSwitcher;

    iput-object p3, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;->bannerView:Landroid/widget/ImageSwitcher;

    .line 115
    iget-object p3, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;->bannerView:Landroid/widget/ImageSwitcher;

    new-instance v1, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment$2;

    invoke-direct {v1, p0, p1}, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment$2;-><init>(Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;Landroid/view/LayoutInflater;)V

    invoke-virtual {p3, v1}, Landroid/widget/ImageSwitcher;->setFactory(Landroid/widget/ViewSwitcher$ViewFactory;)V

    .line 128
    iget-object p1, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;->bannerView:Landroid/widget/ImageSwitcher;

    invoke-virtual {p1, v0}, Landroid/widget/ImageSwitcher;->setAnimateFirstView(Z)V

    .line 129
    iget-object p1, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;->bannerView:Landroid/widget/ImageSwitcher;

    new-instance p3, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment$3;

    invoke-direct {p3, p0}, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment$3;-><init>(Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;)V

    invoke-virtual {p1, p3}, Landroid/widget/ImageSwitcher;->setOnClickListener(Landroid/view/View$OnClickListener;)V

    .line 144
    iget-object p1, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;->bannerView:Landroid/widget/ImageSwitcher;

    new-instance p3, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment$4;

    invoke-direct {p3, p0}, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment$4;-><init>(Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;)V

    invoke-virtual {p1, p3}, Landroid/widget/ImageSwitcher;->setOnTouchListener(Landroid/view/View$OnTouchListener;)V

    .line 200
    sget p1, Lnet/gogame/gowrap/R$id;->net_gogame_gowrap_news_banner_period:I

    invoke-virtual {p2, p1}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object p1

    check-cast p1, Landroid/widget/TextView;

    iput-object p1, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;->bannerPeriodTextView:Landroid/widget/TextView;

    .line 205
    iget-object p1, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;->savedInstanceState:Landroid/os/Bundle;

    if-eqz p1, :cond_2

    const-string p1, "listView"

    .line 206
    invoke-direct {p0, p1}, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;->getParcelable(Ljava/lang/String;)Landroid/os/Parcelable;

    move-result-object p1

    if-eqz p1, :cond_1

    .line 207
    iget-object p1, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;->listView:Landroid/widget/ListView;

    const-string p3, "listView"

    invoke-direct {p0, p3}, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;->getParcelable(Ljava/lang/String;)Landroid/os/Parcelable;

    move-result-object p3

    invoke-virtual {p1, p3}, Landroid/widget/ListView;->onRestoreInstanceState(Landroid/os/Parcelable;)V

    :cond_1
    const-string p1, "bannerView"

    .line 209
    invoke-direct {p0, p1}, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;->getParcelable(Ljava/lang/String;)Landroid/os/Parcelable;

    move-result-object p1

    if-eqz p1, :cond_4

    const-string p1, "bannerView"

    .line 210
    invoke-direct {p0, p1}, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;->getParcelable(Ljava/lang/String;)Landroid/os/Parcelable;

    move-result-object p1

    check-cast p1, Landroid/os/Bundle;

    const-string p3, "banners"

    .line 211
    invoke-virtual {p1, p3}, Landroid/os/Bundle;->getSerializable(Ljava/lang/String;)Ljava/io/Serializable;

    move-result-object p1

    check-cast p1, Ljava/util/ArrayList;

    iput-object p1, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;->banners:Ljava/util/ArrayList;

    .line 212
    invoke-direct {p0, v0}, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;->showBanner(I)V

    goto :goto_0

    .line 215
    :cond_2
    sget-object p1, Lnet/gogame/gowrap/integrations/core/CoreSupport;->INSTANCE:Lnet/gogame/gowrap/integrations/core/CoreSupport;

    invoke-virtual {p1}, Lnet/gogame/gowrap/integrations/core/CoreSupport;->getAppId()Ljava/lang/String;

    move-result-object p1

    if-eqz p1, :cond_4

    .line 217
    :try_start_0
    new-instance p1, Ljava/net/URL;

    new-instance p3, Ljava/lang/StringBuilder;

    invoke-direct {p3}, Ljava/lang/StringBuilder;-><init>()V

    const-string v1, "http://gw-sites.gogame.net/news/"

    invoke-virtual {p3, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    sget-object v1, Lnet/gogame/gowrap/integrations/core/CoreSupport;->INSTANCE:Lnet/gogame/gowrap/integrations/core/CoreSupport;

    .line 218
    invoke-virtual {v1}, Lnet/gogame/gowrap/integrations/core/CoreSupport;->getAppId()Ljava/lang/String;

    move-result-object v1

    invoke-virtual {p3, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, "/news_android.json"

    invoke-virtual {p3, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p3}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p3

    invoke-direct {p1, p3}, Ljava/net/URL;-><init>(Ljava/lang/String;)V

    .line 220
    new-instance p3, Ljava/io/File;

    invoke-virtual {p0}, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;->getActivity()Landroid/app/Activity;

    move-result-object v1

    invoke-virtual {v1}, Landroid/app/Activity;->getCacheDir()Ljava/io/File;

    move-result-object v1

    const-string v2, "net/gogame/gowrap/news.json"

    invoke-direct {p3, v1, v2}, Ljava/io/File;-><init>(Ljava/io/File;Ljava/lang/String;)V

    .line 222
    invoke-virtual {p3}, Ljava/io/File;->getParentFile()Ljava/io/File;

    move-result-object v1

    invoke-virtual {v1}, Ljava/io/File;->mkdirs()Z

    .line 223
    iget-object v1, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;->progressBar:Landroid/widget/ProgressBar;

    if-eqz v1, :cond_3

    .line 224
    iget-object v1, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;->progressBar:Landroid/widget/ProgressBar;

    invoke-virtual {v1, v0}, Landroid/widget/ProgressBar;->setVisibility(I)V

    .line 226
    :cond_3
    invoke-virtual {p0}, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;->getActivity()Landroid/app/Activity;

    move-result-object v0

    new-instance v1, Lnet/gogame/gowrap/support/DownloadUtils$FileTarget;

    invoke-direct {v1, p3}, Lnet/gogame/gowrap/support/DownloadUtils$FileTarget;-><init>(Ljava/io/File;)V

    .line 227
    invoke-virtual {p3}, Ljava/io/File;->isFile()Z

    move-result v2

    new-instance v3, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment$5;

    invoke-direct {v3, p0, p3}, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment$5;-><init>(Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;Ljava/io/File;)V

    .line 226
    invoke-static {v0, p1, v1, v2, v3}, Lnet/gogame/gowrap/support/DownloadUtils;->download(Landroid/content/Context;Ljava/net/URL;Lnet/gogame/gowrap/support/DownloadUtils$Target;ZLnet/gogame/gowrap/support/DownloadUtils$Callback;)V
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception p1

    const-string p3, "goWrap"

    const-string v0, "Exception"

    .line 320
    invoke-static {p3, v0, p1}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)I

    :cond_4
    :goto_0
    return-object p2
.end method

.method public onPause()V
    .locals 4

    .line 491
    invoke-super {p0}, Landroid/app/Fragment;->onPause()V

    .line 493
    iget-object v0, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;->timer:Ljava/util/Timer;

    if-eqz v0, :cond_0

    .line 494
    iget-object v0, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;->timer:Ljava/util/Timer;

    invoke-virtual {v0}, Ljava/util/Timer;->cancel()V

    const/4 v0, 0x0

    .line 495
    iput-object v0, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;->timer:Ljava/util/Timer;

    .line 498
    :cond_0
    new-instance v0, Landroid/os/Bundle;

    invoke-direct {v0}, Landroid/os/Bundle;-><init>()V

    .line 499
    iget-object v1, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;->listAdapter:Lnet/gogame/gowrap/ui/v2017_2/NewsListAdapter;

    if-eqz v1, :cond_1

    const-string v1, "listAdapter"

    .line 500
    iget-object v2, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;->listAdapter:Lnet/gogame/gowrap/ui/v2017_2/NewsListAdapter;

    .line 501
    invoke-virtual {v2}, Lnet/gogame/gowrap/ui/v2017_2/NewsListAdapter;->onSaveInstanceState()Landroid/os/Parcelable;

    move-result-object v2

    .line 500
    invoke-virtual {v0, v1, v2}, Landroid/os/Bundle;->putParcelable(Ljava/lang/String;Landroid/os/Parcelable;)V

    .line 503
    :cond_1
    iget-object v1, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;->listView:Landroid/widget/ListView;

    if-eqz v1, :cond_2

    const-string v1, "listView"

    .line 504
    iget-object v2, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;->listView:Landroid/widget/ListView;

    .line 505
    invoke-virtual {v2}, Landroid/widget/ListView;->onSaveInstanceState()Landroid/os/Parcelable;

    move-result-object v2

    .line 504
    invoke-virtual {v0, v1, v2}, Landroid/os/Bundle;->putParcelable(Ljava/lang/String;Landroid/os/Parcelable;)V

    .line 508
    :cond_2
    new-instance v1, Landroid/os/Bundle;

    invoke-direct {v1}, Landroid/os/Bundle;-><init>()V

    const-string v2, "banners"

    .line 509
    iget-object v3, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;->banners:Ljava/util/ArrayList;

    invoke-virtual {v1, v2, v3}, Landroid/os/Bundle;->putSerializable(Ljava/lang/String;Ljava/io/Serializable;)V

    const-string v2, "bannerView"

    .line 510
    invoke-virtual {v0, v2, v1}, Landroid/os/Bundle;->putParcelable(Ljava/lang/String;Landroid/os/Parcelable;)V

    .line 512
    iput-object v0, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;->savedInstanceState:Landroid/os/Bundle;

    return-void
.end method

.method public onResume()V
    .locals 7

    .line 480
    invoke-super {p0}, Landroid/app/Fragment;->onResume()V

    .line 482
    iget-object v0, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;->timer:Ljava/util/Timer;

    if-nez v0, :cond_0

    .line 483
    new-instance v0, Ljava/util/Timer;

    invoke-direct {v0}, Ljava/util/Timer;-><init>()V

    iput-object v0, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;->timer:Ljava/util/Timer;

    .line 484
    iget-object v1, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;->timer:Ljava/util/Timer;

    new-instance v2, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment$BannerTimerTask;

    const/4 v0, 0x0

    invoke-direct {v2, p0, v0}, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment$BannerTimerTask;-><init>(Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;Lnet/gogame/gowrap/ui/v2017_2/NewsFragment$1;)V

    const-wide/16 v3, 0xfa0

    const-wide/16 v5, 0xfa0

    invoke-virtual/range {v1 .. v6}, Ljava/util/Timer;->schedule(Ljava/util/TimerTask;JJ)V

    :cond_0
    return-void
.end method
