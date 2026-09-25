.class public Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment;
.super Landroid/app/Fragment;
.source "NewsFeedFragment.java"


# static fields
.field private static final CUSTOM_GRID_LAYOUT_BUNDLE_NAME:Ljava/lang/String; = "customGridLayout"

.field private static final CUSTOM_GRID_LAYOUT_BUNDLE_PROPERTY_NAME_CHILD_COUNT:Ljava/lang/String; = "childCount"

.field private static final DEFAULT_PAGE_SIZE:I = 0x2

.field private static final LAUNCH_URL_EXTERNALLY:Z = false

.field private static final SCROLL_VIEW_BUNDLE_NAME:Ljava/lang/String; = "scrollView"

.field private static final SCROLL_VIEW_BUNDLE_PROPERTY_NAME_SCROLL_X:Ljava/lang/String; = "scrollX"

.field private static final SCROLL_VIEW_BUNDLE_PROPERTY_NAME_SCROLL_Y:Ljava/lang/String; = "scrollY"

.field private static final VIDEO_ENABLED:Z = false


# instance fields
.field private customGridLayout:Lnet/gogame/gowrap/ui/layout/CustomGridLayout;

.field private downloadManagerListener:Lnet/gogame/gowrap/support/DownloadManager$Listener;

.field private downloadingFeed:Z

.field private feed:Lnet/gogame/gowrap/model/feed/Feed;

.field private moreButton:Landroid/view/View;

.field private pageSize:I

.field private progressBar:Landroid/widget/ProgressBar;

.field private progressBar2:Landroid/widget/ProgressBar;

.field private savedInstanceState:Landroid/os/Bundle;

.field private scrollView:Landroid/widget/ScrollView;

.field private uiContext:Lnet/gogame/gowrap/ui/UIContext;

.field private viewExists:Z

.field private visitSiteForMoreButton:Landroid/view/View;


# direct methods
.method public constructor <init>()V
    .locals 1

    .line 38
    invoke-direct {p0}, Landroid/app/Fragment;-><init>()V

    const/4 v0, 0x2

    .line 49
    iput v0, p0, Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment;->pageSize:I

    const/4 v0, 0x0

    .line 59
    iput-boolean v0, p0, Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment;->viewExists:Z

    .line 60
    new-instance v0, Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment$1;

    invoke-direct {v0, p0}, Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment$1;-><init>(Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment;)V

    iput-object v0, p0, Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment;->downloadManagerListener:Lnet/gogame/gowrap/support/DownloadManager$Listener;

    return-void
.end method

.method static synthetic access$000(Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment;)V
    .locals 0

    .line 38
    invoke-direct {p0}, Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment;->updateProgress()V

    return-void
.end method

.method static synthetic access$102(Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment;Z)Z
    .locals 0

    .line 38
    iput-boolean p1, p0, Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment;->downloadingFeed:Z

    return p1
.end method

.method static synthetic access$202(Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment;Lnet/gogame/gowrap/model/feed/Feed;)Lnet/gogame/gowrap/model/feed/Feed;
    .locals 0

    .line 38
    iput-object p1, p0, Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment;->feed:Lnet/gogame/gowrap/model/feed/Feed;

    return-object p1
.end method

.method static synthetic access$300(Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment;Ljava/io/File;)Lnet/gogame/gowrap/model/feed/Feed;
    .locals 0
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    .line 38
    invoke-direct {p0, p1}, Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment;->readFeed(Ljava/io/File;)Lnet/gogame/gowrap/model/feed/Feed;

    move-result-object p0

    return-object p0
.end method

.method static synthetic access$400(Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment;)V
    .locals 0

    .line 38
    invoke-direct {p0}, Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment;->initializeNewsFeed()V

    return-void
.end method

.method static synthetic access$500(Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment;)Lnet/gogame/gowrap/ui/UIContext;
    .locals 0

    .line 38
    iget-object p0, p0, Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment;->uiContext:Lnet/gogame/gowrap/ui/UIContext;

    return-object p0
.end method

.method static synthetic access$600(Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment;)I
    .locals 0

    .line 38
    iget p0, p0, Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment;->pageSize:I

    return p0
.end method

.method static synthetic access$700(Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment;I)V
    .locals 0

    .line 38
    invoke-direct {p0, p1}, Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment;->appendItems(I)V

    return-void
.end method

.method static synthetic access$800(Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment;)Landroid/widget/ScrollView;
    .locals 0

    .line 38
    iget-object p0, p0, Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment;->scrollView:Landroid/widget/ScrollView;

    return-object p0
.end method

.method private appendItems(I)V
    .locals 10

    .line 312
    iget-object v0, p0, Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment;->feed:Lnet/gogame/gowrap/model/feed/Feed;

    if-eqz v0, :cond_8

    iget-object v0, p0, Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment;->feed:Lnet/gogame/gowrap/model/feed/Feed;

    invoke-virtual {v0}, Lnet/gogame/gowrap/model/feed/Feed;->getItems()Ljava/util/List;

    move-result-object v0

    if-nez v0, :cond_0

    goto/16 :goto_4

    :cond_0
    const/4 v0, 0x0

    const/4 v1, 0x0

    :goto_0
    const/4 v2, 0x1

    if-ge v1, p1, :cond_6

    .line 316
    iget-object v3, p0, Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment;->customGridLayout:Lnet/gogame/gowrap/ui/layout/CustomGridLayout;

    invoke-virtual {v3}, Lnet/gogame/gowrap/ui/layout/CustomGridLayout;->getChildCount()I

    move-result v3

    iget-object v4, p0, Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment;->feed:Lnet/gogame/gowrap/model/feed/Feed;

    invoke-virtual {v4}, Lnet/gogame/gowrap/model/feed/Feed;->getItems()Ljava/util/List;

    move-result-object v4

    invoke-interface {v4}, Ljava/util/List;->size()I

    move-result v4

    if-ge v3, v4, :cond_6

    .line 317
    iget-object v3, p0, Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment;->customGridLayout:Lnet/gogame/gowrap/ui/layout/CustomGridLayout;

    invoke-virtual {v3}, Lnet/gogame/gowrap/ui/layout/CustomGridLayout;->getChildCount()I

    move-result v3

    .line 318
    iget-object v4, p0, Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment;->feed:Lnet/gogame/gowrap/model/feed/Feed;

    invoke-virtual {v4}, Lnet/gogame/gowrap/model/feed/Feed;->getItems()Ljava/util/List;

    move-result-object v4

    invoke-interface {v4, v3}, Ljava/util/List;->get(I)Ljava/lang/Object;

    move-result-object v4

    check-cast v4, Lnet/gogame/gowrap/model/feed/Feed$Item;

    const/4 v5, 0x0

    .line 321
    invoke-virtual {v4}, Lnet/gogame/gowrap/model/feed/Feed$Item;->getMediaWidth()Ljava/lang/Integer;

    move-result-object v6

    invoke-virtual {v4}, Lnet/gogame/gowrap/model/feed/Feed$Item;->getMediaHeight()Ljava/lang/Integer;

    move-result-object v7

    invoke-direct {p0, v6, v7}, Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment;->getAspectRatio(Ljava/lang/Integer;Ljava/lang/Integer;)Ljava/lang/Double;

    move-result-object v6

    .line 323
    :try_start_0
    invoke-virtual {v4}, Lnet/gogame/gowrap/model/feed/Feed$Item;->getType()Ljava/lang/String;

    move-result-object v7

    const-string v8, "VIDEO"

    invoke-static {v7, v8}, Lnet/gogame/gowrap/support/StringUtils;->isEquals(Ljava/lang/String;Ljava/lang/String;)Z

    move-result v7

    if-eqz v7, :cond_2

    .line 335
    new-instance v7, Lnet/gogame/gowrap/ui/v2017_1/PhotoNewsFeedItemView;

    .line 336
    invoke-virtual {p0}, Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment;->getActivity()Landroid/app/Activity;

    move-result-object v8

    invoke-direct {v7, v8}, Lnet/gogame/gowrap/ui/v2017_1/PhotoNewsFeedItemView;-><init>(Landroid/content/Context;)V

    .line 337
    iget-object v8, p0, Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment;->uiContext:Lnet/gogame/gowrap/ui/UIContext;

    if-eqz v8, :cond_1

    .line 338
    iget-object v8, p0, Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment;->uiContext:Lnet/gogame/gowrap/ui/UIContext;

    invoke-interface {v8}, Lnet/gogame/gowrap/ui/UIContext;->getDownloadManager()Lnet/gogame/gowrap/support/DownloadManager;

    move-result-object v8

    .line 340
    invoke-virtual {v4}, Lnet/gogame/gowrap/model/feed/Feed$Item;->getMediaPreview()Ljava/lang/String;

    move-result-object v9

    invoke-static {v9}, Lnet/gogame/gowrap/support/DownloadManager$Request$Builder;->newBuilder(Ljava/lang/String;)Lnet/gogame/gowrap/support/DownloadManager$Request$Builder;

    move-result-object v9

    .line 341
    invoke-virtual {v9, v7}, Lnet/gogame/gowrap/support/DownloadManager$Request$Builder;->into(Lnet/gogame/gowrap/support/DownloadManager$Target;)Lnet/gogame/gowrap/support/DownloadManager$Request;

    move-result-object v9

    .line 338
    invoke-interface {v8, v9}, Lnet/gogame/gowrap/support/DownloadManager;->download(Lnet/gogame/gowrap/support/DownloadManager$Request;)V

    :cond_1
    :goto_1
    move-object v5, v7

    goto :goto_2

    .line 345
    :cond_2
    invoke-virtual {v4}, Lnet/gogame/gowrap/model/feed/Feed$Item;->getType()Ljava/lang/String;

    move-result-object v7

    const-string v8, "PHOTO"

    invoke-static {v7, v8}, Lnet/gogame/gowrap/support/StringUtils;->isEquals(Ljava/lang/String;Ljava/lang/String;)Z

    move-result v7

    if-eqz v7, :cond_5

    .line 346
    new-instance v7, Lnet/gogame/gowrap/ui/v2017_1/PhotoNewsFeedItemView;

    .line 347
    invoke-virtual {p0}, Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment;->getActivity()Landroid/app/Activity;

    move-result-object v8

    invoke-direct {v7, v8}, Lnet/gogame/gowrap/ui/v2017_1/PhotoNewsFeedItemView;-><init>(Landroid/content/Context;)V

    .line 348
    iget-object v8, p0, Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment;->uiContext:Lnet/gogame/gowrap/ui/UIContext;

    if-eqz v8, :cond_1

    .line 349
    iget-object v8, p0, Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment;->uiContext:Lnet/gogame/gowrap/ui/UIContext;

    invoke-interface {v8}, Lnet/gogame/gowrap/ui/UIContext;->getDownloadManager()Lnet/gogame/gowrap/support/DownloadManager;

    move-result-object v8

    .line 351
    invoke-virtual {v4}, Lnet/gogame/gowrap/model/feed/Feed$Item;->getMediaSource()Ljava/lang/String;

    move-result-object v9

    invoke-static {v9}, Lnet/gogame/gowrap/support/DownloadManager$Request$Builder;->newBuilder(Ljava/lang/String;)Lnet/gogame/gowrap/support/DownloadManager$Request$Builder;

    move-result-object v9

    .line 352
    invoke-virtual {v9, v7}, Lnet/gogame/gowrap/support/DownloadManager$Request$Builder;->into(Lnet/gogame/gowrap/support/DownloadManager$Target;)Lnet/gogame/gowrap/support/DownloadManager$Request;

    move-result-object v9

    .line 349
    invoke-interface {v8, v9}, Lnet/gogame/gowrap/support/DownloadManager;->download(Lnet/gogame/gowrap/support/DownloadManager$Request;)V
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_1

    :catch_0
    move-exception v7

    const-string v8, "goWrap"

    const-string v9, "Exception"

    .line 359
    invoke-static {v8, v9, v7}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)I

    :goto_2
    if-eqz v5, :cond_5

    .line 363
    invoke-virtual {v4}, Lnet/gogame/gowrap/model/feed/Feed$Item;->getCreatedTime()Ljava/lang/Long;

    move-result-object v7

    invoke-virtual {v5, v7}, Lnet/gogame/gowrap/ui/v2017_1/AbstractNewsFeedItemView;->setTimestamp(Ljava/lang/Long;)V

    .line 364
    invoke-virtual {v4}, Lnet/gogame/gowrap/model/feed/Feed$Item;->getMessage()Ljava/lang/String;

    move-result-object v7

    invoke-direct {p0, v7}, Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment;->sanitizeMessage(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v7

    invoke-virtual {v5, v7}, Lnet/gogame/gowrap/ui/v2017_1/AbstractNewsFeedItemView;->setMessage(Ljava/lang/String;)V

    .line 365
    invoke-virtual {p0}, Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment;->getActivity()Landroid/app/Activity;

    move-result-object v7

    invoke-virtual {v7}, Landroid/app/Activity;->getResources()Landroid/content/res/Resources;

    move-result-object v7

    sget v8, Lnet/gogame/gowrap/R$drawable;->net_gogame_gowrap_icon_share:I

    .line 366
    invoke-virtual {v7, v8}, Landroid/content/res/Resources;->getDrawable(I)Landroid/graphics/drawable/Drawable;

    move-result-object v7

    .line 365
    invoke-virtual {v5, v7}, Lnet/gogame/gowrap/ui/v2017_1/AbstractNewsFeedItemView;->setButtonImage(Landroid/graphics/drawable/Drawable;)V

    .line 367
    invoke-virtual {v6}, Ljava/lang/Double;->doubleValue()D

    move-result-wide v6

    invoke-virtual {v5, v6, v7}, Lnet/gogame/gowrap/ui/v2017_1/AbstractNewsFeedItemView;->setAspectRatio(D)V

    .line 368
    invoke-virtual {v5, v3}, Lnet/gogame/gowrap/ui/v2017_1/AbstractNewsFeedItemView;->setPosition(I)V

    .line 369
    iget-object v3, p0, Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment;->uiContext:Lnet/gogame/gowrap/ui/UIContext;

    if-eqz v3, :cond_4

    .line 370
    invoke-virtual {v4}, Lnet/gogame/gowrap/model/feed/Feed$Item;->getLink()Ljava/lang/String;

    move-result-object v3

    if-nez v3, :cond_3

    invoke-virtual {v4}, Lnet/gogame/gowrap/model/feed/Feed$Item;->getArticleLink()Ljava/lang/String;

    move-result-object v3

    if-eqz v3, :cond_4

    .line 371
    :cond_3
    new-instance v3, Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment$6;

    invoke-direct {v3, p0, v4}, Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment$6;-><init>(Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment;Lnet/gogame/gowrap/model/feed/Feed$Item;)V

    invoke-virtual {v5, v3}, Lnet/gogame/gowrap/ui/v2017_1/AbstractNewsFeedItemView;->setOnClickListener(Landroid/view/View$OnClickListener;)V

    .line 392
    invoke-virtual {v4}, Lnet/gogame/gowrap/model/feed/Feed$Item;->getLink()Ljava/lang/String;

    move-result-object v3

    if-eqz v3, :cond_4

    .line 393
    new-instance v3, Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment$7;

    invoke-direct {v3, p0, v4}, Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment$7;-><init>(Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment;Lnet/gogame/gowrap/model/feed/Feed$Item;)V

    invoke-virtual {v5, v3}, Lnet/gogame/gowrap/ui/v2017_1/AbstractNewsFeedItemView;->setButtonOnClickListener(Landroid/view/View$OnClickListener;)V

    .line 404
    :cond_4
    new-instance v3, Lnet/gogame/gowrap/ui/layout/CustomGridLayout$LayoutParams;

    const/4 v4, -0x1

    const/4 v6, -0x2

    invoke-direct {v3, v4, v6}, Lnet/gogame/gowrap/ui/layout/CustomGridLayout$LayoutParams;-><init>(II)V

    .line 407
    iput v2, v3, Lnet/gogame/gowrap/ui/layout/CustomGridLayout$LayoutParams;->gravity:I

    .line 408
    iput v0, v3, Lnet/gogame/gowrap/ui/layout/CustomGridLayout$LayoutParams;->topMargin:I

    .line 409
    iput v0, v3, Lnet/gogame/gowrap/ui/layout/CustomGridLayout$LayoutParams;->bottomMargin:I

    .line 410
    iput v0, v3, Lnet/gogame/gowrap/ui/layout/CustomGridLayout$LayoutParams;->leftMargin:I

    .line 411
    iput v0, v3, Lnet/gogame/gowrap/ui/layout/CustomGridLayout$LayoutParams;->rightMargin:I

    .line 412
    iget-object v2, p0, Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment;->customGridLayout:Lnet/gogame/gowrap/ui/layout/CustomGridLayout;

    invoke-virtual {v2, v5, v3}, Lnet/gogame/gowrap/ui/layout/CustomGridLayout;->addView(Landroid/view/View;Landroid/view/ViewGroup$LayoutParams;)V

    :cond_5
    add-int/lit8 v1, v1, 0x1

    goto/16 :goto_0

    .line 416
    :cond_6
    iget-object p1, p0, Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment;->feed:Lnet/gogame/gowrap/model/feed/Feed;

    invoke-virtual {p1}, Lnet/gogame/gowrap/model/feed/Feed;->getItems()Ljava/util/List;

    move-result-object p1

    invoke-interface {p1}, Ljava/util/List;->isEmpty()Z

    move-result p1

    if-nez p1, :cond_7

    iget-object p1, p0, Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment;->customGridLayout:Lnet/gogame/gowrap/ui/layout/CustomGridLayout;

    .line 417
    invoke-virtual {p1}, Lnet/gogame/gowrap/ui/layout/CustomGridLayout;->getChildCount()I

    move-result p1

    iget-object v1, p0, Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment;->feed:Lnet/gogame/gowrap/model/feed/Feed;

    invoke-virtual {v1}, Lnet/gogame/gowrap/model/feed/Feed;->getItems()Ljava/util/List;

    move-result-object v1

    invoke-interface {v1}, Ljava/util/List;->size()I

    move-result v1

    if-ge p1, v1, :cond_7

    .line 418
    iget-object p1, p0, Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment;->moreButton:Landroid/view/View;

    invoke-virtual {p1, v0}, Landroid/view/View;->setVisibility(I)V

    .line 419
    iget-object p1, p0, Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment;->moreButton:Landroid/view/View;

    invoke-virtual {p1, v2}, Landroid/view/View;->setSelected(Z)V

    goto :goto_3

    .line 421
    :cond_7
    iget-object p1, p0, Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment;->moreButton:Landroid/view/View;

    const/16 v1, 0x8

    invoke-virtual {p1, v1}, Landroid/view/View;->setVisibility(I)V

    .line 422
    iget-object p1, p0, Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment;->moreButton:Landroid/view/View;

    invoke-virtual {p1, v0}, Landroid/view/View;->setSelected(Z)V

    .line 424
    iget-object p1, p0, Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment;->visitSiteForMoreButton:Landroid/view/View;

    invoke-virtual {p1, v0}, Landroid/view/View;->setVisibility(I)V

    .line 425
    iget-object p1, p0, Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment;->visitSiteForMoreButton:Landroid/view/View;

    invoke-virtual {p1, v2}, Landroid/view/View;->setSelected(Z)V

    :goto_3
    return-void

    :cond_8
    :goto_4
    return-void
.end method

.method private getAspectRatio(Ljava/lang/Integer;Ljava/lang/Integer;)Ljava/lang/Double;
    .locals 2

    if-eqz p1, :cond_0

    if-eqz p2, :cond_0

    .line 306
    invoke-virtual {p1}, Ljava/lang/Integer;->doubleValue()D

    move-result-wide v0

    invoke-virtual {p2}, Ljava/lang/Integer;->doubleValue()D

    move-result-wide p1

    div-double/2addr v0, p1

    invoke-static {v0, v1}, Ljava/lang/Double;->valueOf(D)Ljava/lang/Double;

    move-result-object p1

    goto :goto_0

    :cond_0
    const/4 p1, 0x0

    :goto_0
    return-object p1
.end method

.method private initializeNewsFeed()V
    .locals 5

    .line 257
    iget-boolean v0, p0, Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment;->viewExists:Z

    if-nez v0, :cond_0

    return-void

    .line 260
    :cond_0
    iget-object v0, p0, Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment;->savedInstanceState:Landroid/os/Bundle;

    if-eqz v0, :cond_2

    .line 261
    iget-object v0, p0, Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment;->savedInstanceState:Landroid/os/Bundle;

    const-string v1, "customGridLayout"

    .line 262
    invoke-virtual {v0, v1}, Landroid/os/Bundle;->getBundle(Ljava/lang/String;)Landroid/os/Bundle;

    move-result-object v0

    if-eqz v0, :cond_1

    const-string v1, "childCount"

    .line 264
    iget v2, p0, Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment;->pageSize:I

    invoke-virtual {v0, v1, v2}, Landroid/os/Bundle;->getInt(Ljava/lang/String;I)I

    move-result v0

    .line 266
    invoke-direct {p0, v0}, Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment;->appendItems(I)V

    goto :goto_0

    .line 268
    :cond_1
    iget v0, p0, Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment;->pageSize:I

    invoke-direct {p0, v0}, Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment;->appendItems(I)V

    .line 271
    :goto_0
    iget-object v0, p0, Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment;->savedInstanceState:Landroid/os/Bundle;

    const-string v1, "scrollView"

    invoke-virtual {v0, v1}, Landroid/os/Bundle;->getBundle(Ljava/lang/String;)Landroid/os/Bundle;

    move-result-object v0

    if-eqz v0, :cond_3

    .line 273
    iget-object v1, p0, Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment;->scrollView:Landroid/widget/ScrollView;

    const/4 v2, 0x0

    invoke-virtual {v1, v2}, Landroid/widget/ScrollView;->getChildAt(I)Landroid/view/View;

    move-result-object v1

    .line 274
    iget-object v3, p0, Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment;->scrollView:Landroid/widget/ScrollView;

    if-eqz v3, :cond_3

    if-eqz v1, :cond_3

    const-string v3, "scrollX"

    .line 275
    invoke-virtual {v0, v3, v2}, Landroid/os/Bundle;->getInt(Ljava/lang/String;I)I

    move-result v3

    const-string v4, "scrollY"

    .line 277
    invoke-virtual {v0, v4, v2}, Landroid/os/Bundle;->getInt(Ljava/lang/String;I)I

    move-result v0

    .line 279
    new-instance v2, Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment$5;

    invoke-direct {v2, p0, v1, v0, v3}, Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment$5;-><init>(Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment;Landroid/view/View;II)V

    .line 291
    iget-object v0, p0, Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment;->scrollView:Landroid/widget/ScrollView;

    const-wide/16 v3, 0x3e8

    invoke-virtual {v0, v2, v3, v4}, Landroid/widget/ScrollView;->postDelayed(Ljava/lang/Runnable;J)Z

    goto :goto_1

    .line 295
    :cond_2
    iget v0, p0, Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment;->pageSize:I

    invoke-direct {p0, v0}, Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment;->appendItems(I)V

    :cond_3
    :goto_1
    return-void
.end method

.method private readFeed(Ljava/io/File;)Lnet/gogame/gowrap/model/feed/Feed;
    .locals 3
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    .line 430
    new-instance v0, Ljava/io/FileInputStream;

    invoke-direct {v0, p1}, Ljava/io/FileInputStream;-><init>(Ljava/io/File;)V

    .line 432
    :try_start_0
    new-instance p1, Ljava/io/InputStreamReader;

    const-string v1, "UTF-8"

    invoke-direct {p1, v0, v1}, Ljava/io/InputStreamReader;-><init>(Ljava/io/InputStream;Ljava/lang/String;)V
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_2

    .line 434
    :try_start_1
    new-instance v1, Landroid/util/JsonReader;

    invoke-direct {v1, p1}, Landroid/util/JsonReader;-><init>(Ljava/io/Reader;)V
    :try_end_1
    .catchall {:try_start_1 .. :try_end_1} :catchall_1

    .line 436
    :try_start_2
    new-instance v2, Lnet/gogame/gowrap/model/feed/Feed;

    invoke-direct {v2, v1}, Lnet/gogame/gowrap/model/feed/Feed;-><init>(Landroid/util/JsonReader;)V
    :try_end_2
    .catchall {:try_start_2 .. :try_end_2} :catchall_0

    .line 438
    :try_start_3
    invoke-static {v1}, Lnet/gogame/gowrap/support/JSONUtils;->closeQuietly(Landroid/util/JsonReader;)V
    :try_end_3
    .catchall {:try_start_3 .. :try_end_3} :catchall_1

    .line 441
    :try_start_4
    invoke-static {p1}, Lnet/gogame/gowrap/io/utils/IOUtils;->closeQuietly(Ljava/io/Reader;)V
    :try_end_4
    .catchall {:try_start_4 .. :try_end_4} :catchall_2

    .line 444
    invoke-static {v0}, Lnet/gogame/gowrap/io/utils/IOUtils;->closeQuietly(Ljava/io/InputStream;)V

    return-object v2

    :catchall_0
    move-exception v2

    .line 438
    :try_start_5
    invoke-static {v1}, Lnet/gogame/gowrap/support/JSONUtils;->closeQuietly(Landroid/util/JsonReader;)V

    .line 439
    throw v2
    :try_end_5
    .catchall {:try_start_5 .. :try_end_5} :catchall_1

    :catchall_1
    move-exception v1

    .line 441
    :try_start_6
    invoke-static {p1}, Lnet/gogame/gowrap/io/utils/IOUtils;->closeQuietly(Ljava/io/Reader;)V

    .line 442
    throw v1
    :try_end_6
    .catchall {:try_start_6 .. :try_end_6} :catchall_2

    :catchall_2
    move-exception p1

    .line 444
    invoke-static {v0}, Lnet/gogame/gowrap/io/utils/IOUtils;->closeQuietly(Ljava/io/InputStream;)V

    .line 445
    throw p1
.end method

.method private sanitizeMessage(Ljava/lang/String;)Ljava/lang/String;
    .locals 2

    const-string v0, "[\\n\\r\\s]+"

    const-string v1, " "

    .line 300
    invoke-virtual {p1, v0, v1}, Ljava/lang/String;->replaceAll(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object p1

    return-object p1
.end method

.method private updateProgress()V
    .locals 2

    .line 236
    iget-object v0, p0, Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment;->uiContext:Lnet/gogame/gowrap/ui/UIContext;

    if-nez v0, :cond_0

    return-void

    .line 239
    :cond_0
    iget-boolean v0, p0, Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment;->downloadingFeed:Z

    if-nez v0, :cond_2

    iget-object v0, p0, Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment;->uiContext:Lnet/gogame/gowrap/ui/UIContext;

    .line 240
    invoke-interface {v0}, Lnet/gogame/gowrap/ui/UIContext;->getDownloadManager()Lnet/gogame/gowrap/support/DownloadManager;

    move-result-object v0

    invoke-interface {v0}, Lnet/gogame/gowrap/support/DownloadManager;->isDownloading()Z

    move-result v0

    if-eqz v0, :cond_1

    goto :goto_0

    :cond_1
    const/4 v0, 0x0

    goto :goto_1

    :cond_2
    :goto_0
    const/4 v0, 0x1

    .line 241
    :goto_1
    iget-object v1, p0, Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment;->progressBar:Landroid/widget/ProgressBar;

    invoke-direct {p0, v1, v0}, Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment;->updateProgress(Landroid/widget/ProgressBar;Z)V

    .line 242
    iget-object v1, p0, Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment;->progressBar2:Landroid/widget/ProgressBar;

    invoke-direct {p0, v1, v0}, Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment;->updateProgress(Landroid/widget/ProgressBar;Z)V

    return-void
.end method

.method private updateProgress(Landroid/widget/ProgressBar;Z)V
    .locals 0

    if-nez p1, :cond_0

    return-void

    :cond_0
    if-eqz p2, :cond_1

    const/4 p2, 0x0

    .line 250
    invoke-virtual {p1, p2}, Landroid/widget/ProgressBar;->setVisibility(I)V

    goto :goto_0

    :cond_1
    const/16 p2, 0x8

    .line 252
    invoke-virtual {p1, p2}, Landroid/widget/ProgressBar;->setVisibility(I)V

    :goto_0
    return-void
.end method


# virtual methods
.method public onCreateView(Landroid/view/LayoutInflater;Landroid/view/ViewGroup;Landroid/os/Bundle;)Landroid/view/View;
    .locals 4

    .line 75
    sget p3, Lnet/gogame/gowrap/R$layout;->net_gogame_gowrap_fragment_newsfeed:I

    const/4 v0, 0x0

    invoke-virtual {p1, p3, p2, v0}, Landroid/view/LayoutInflater;->inflate(ILandroid/view/ViewGroup;Z)Landroid/view/View;

    move-result-object p1

    .line 77
    sget p2, Lnet/gogame/gowrap/R$id;->net_gogame_gowrap_newsfeed:I

    invoke-virtual {p1, p2}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object p2

    check-cast p2, Lnet/gogame/gowrap/ui/layout/CustomGridLayout;

    iput-object p2, p0, Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment;->customGridLayout:Lnet/gogame/gowrap/ui/layout/CustomGridLayout;

    const/4 p2, 0x1

    .line 79
    iput-boolean p2, p0, Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment;->viewExists:Z

    .line 81
    invoke-virtual {p0}, Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment;->getActivity()Landroid/app/Activity;

    move-result-object p3

    instance-of p3, p3, Lnet/gogame/gowrap/ui/UIContext;

    if-eqz p3, :cond_0

    .line 82
    invoke-virtual {p0}, Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment;->getActivity()Landroid/app/Activity;

    move-result-object p3

    check-cast p3, Lnet/gogame/gowrap/ui/UIContext;

    iput-object p3, p0, Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment;->uiContext:Lnet/gogame/gowrap/ui/UIContext;

    .line 85
    :cond_0
    sget p3, Lnet/gogame/gowrap/R$id;->net_gogame_gowrap_progressBar:I

    invoke-virtual {p1, p3}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object p3

    check-cast p3, Landroid/widget/ProgressBar;

    iput-object p3, p0, Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment;->progressBar:Landroid/widget/ProgressBar;

    .line 86
    sget p3, Lnet/gogame/gowrap/R$id;->net_gogame_gowrap_progressBar2:I

    invoke-virtual {p1, p3}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object p3

    check-cast p3, Landroid/widget/ProgressBar;

    iput-object p3, p0, Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment;->progressBar2:Landroid/widget/ProgressBar;

    .line 88
    sget p3, Lnet/gogame/gowrap/R$id;->net_gogame_gowrap_newsfeed_scroll_view:I

    invoke-virtual {p1, p3}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object p3

    check-cast p3, Landroid/widget/ScrollView;

    iput-object p3, p0, Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment;->scrollView:Landroid/widget/ScrollView;

    .line 90
    sget-object p3, Lnet/gogame/gowrap/integrations/core/CoreSupport;->INSTANCE:Lnet/gogame/gowrap/integrations/core/CoreSupport;

    invoke-virtual {p3}, Lnet/gogame/gowrap/integrations/core/CoreSupport;->getAppId()Ljava/lang/String;

    move-result-object p3

    if-eqz p3, :cond_1

    .line 92
    :try_start_0
    new-instance p3, Ljava/net/URL;

    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    const-string v1, "http://gw-sites.gogame.net/sites/"

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    sget-object v1, Lnet/gogame/gowrap/integrations/core/CoreSupport;->INSTANCE:Lnet/gogame/gowrap/integrations/core/CoreSupport;

    .line 93
    invoke-virtual {v1}, Lnet/gogame/gowrap/integrations/core/CoreSupport;->getAppId()Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, "/data/feed.json"

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v0

    invoke-direct {p3, v0}, Ljava/net/URL;-><init>(Ljava/lang/String;)V

    .line 94
    new-instance v0, Ljava/io/File;

    invoke-virtual {p0}, Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment;->getActivity()Landroid/app/Activity;

    move-result-object v1

    invoke-virtual {v1}, Landroid/app/Activity;->getCacheDir()Ljava/io/File;

    move-result-object v1

    const-string v2, "net/gogame/gowrap/feed.json"

    invoke-direct {v0, v1, v2}, Ljava/io/File;-><init>(Ljava/io/File;Ljava/lang/String;)V

    .line 96
    invoke-virtual {v0}, Ljava/io/File;->getParentFile()Ljava/io/File;

    move-result-object v1

    invoke-virtual {v1}, Ljava/io/File;->mkdirs()Z

    .line 97
    iput-boolean p2, p0, Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment;->downloadingFeed:Z

    .line 98
    invoke-direct {p0}, Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment;->updateProgress()V

    .line 99
    invoke-virtual {p0}, Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment;->getActivity()Landroid/app/Activity;

    move-result-object p2

    new-instance v1, Lnet/gogame/gowrap/support/DownloadUtils$FileTarget;

    invoke-direct {v1, v0}, Lnet/gogame/gowrap/support/DownloadUtils$FileTarget;-><init>(Ljava/io/File;)V

    .line 100
    invoke-virtual {v0}, Ljava/io/File;->isFile()Z

    move-result v2

    new-instance v3, Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment$2;

    invoke-direct {v3, p0, v0}, Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment$2;-><init>(Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment;Ljava/io/File;)V

    .line 99
    invoke-static {p2, p3, v1, v2, v3}, Lnet/gogame/gowrap/support/DownloadUtils;->download(Landroid/content/Context;Ljava/net/URL;Lnet/gogame/gowrap/support/DownloadUtils$Target;ZLnet/gogame/gowrap/support/DownloadUtils$Callback;)V
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception p2

    const-string p3, "goWrap"

    const-string v0, "Exception"

    .line 163
    invoke-static {p3, v0, p2}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)I

    .line 167
    :cond_1
    :goto_0
    sget p2, Lnet/gogame/gowrap/R$id;->net_gogame_gowrap_newsfeed_button_more:I

    invoke-virtual {p1, p2}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object p2

    iput-object p2, p0, Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment;->moreButton:Landroid/view/View;

    .line 168
    iget-object p2, p0, Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment;->moreButton:Landroid/view/View;

    new-instance p3, Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment$3;

    invoke-direct {p3, p0}, Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment$3;-><init>(Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment;)V

    invoke-virtual {p2, p3}, Landroid/view/View;->setOnClickListener(Landroid/view/View$OnClickListener;)V

    .line 176
    sget p2, Lnet/gogame/gowrap/R$id;->net_gogame_gowrap_newsfeed_button_visit_site_for_more:I

    invoke-virtual {p1, p2}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object p2

    iput-object p2, p0, Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment;->visitSiteForMoreButton:Landroid/view/View;

    .line 178
    iget-object p2, p0, Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment;->visitSiteForMoreButton:Landroid/view/View;

    new-instance p3, Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment$4;

    invoke-direct {p3, p0}, Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment$4;-><init>(Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment;)V

    invoke-virtual {p2, p3}, Landroid/view/View;->setOnClickListener(Landroid/view/View$OnClickListener;)V

    return-object p1
.end method

.method public onDestroyView()V
    .locals 1

    const/4 v0, 0x0

    .line 197
    iput-boolean v0, p0, Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment;->viewExists:Z

    .line 199
    invoke-super {p0}, Landroid/app/Fragment;->onDestroyView()V

    return-void
.end method

.method public onPause()V
    .locals 4

    .line 213
    invoke-super {p0}, Landroid/app/Fragment;->onPause()V

    .line 215
    iget-object v0, p0, Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment;->uiContext:Lnet/gogame/gowrap/ui/UIContext;

    if-eqz v0, :cond_0

    .line 216
    iget-object v0, p0, Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment;->uiContext:Lnet/gogame/gowrap/ui/UIContext;

    invoke-interface {v0}, Lnet/gogame/gowrap/ui/UIContext;->getDownloadManager()Lnet/gogame/gowrap/support/DownloadManager;

    move-result-object v0

    iget-object v1, p0, Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment;->downloadManagerListener:Lnet/gogame/gowrap/support/DownloadManager$Listener;

    invoke-interface {v0, v1}, Lnet/gogame/gowrap/support/DownloadManager;->removeListener(Lnet/gogame/gowrap/support/DownloadManager$Listener;)V

    .line 219
    :cond_0
    new-instance v0, Landroid/os/Bundle;

    invoke-direct {v0}, Landroid/os/Bundle;-><init>()V

    .line 220
    iget-object v1, p0, Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment;->customGridLayout:Lnet/gogame/gowrap/ui/layout/CustomGridLayout;

    if-eqz v1, :cond_1

    .line 221
    new-instance v1, Landroid/os/Bundle;

    invoke-direct {v1}, Landroid/os/Bundle;-><init>()V

    const-string v2, "childCount"

    .line 222
    iget-object v3, p0, Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment;->customGridLayout:Lnet/gogame/gowrap/ui/layout/CustomGridLayout;

    .line 223
    invoke-virtual {v3}, Lnet/gogame/gowrap/ui/layout/CustomGridLayout;->getChildCount()I

    move-result v3

    .line 222
    invoke-virtual {v1, v2, v3}, Landroid/os/Bundle;->putInt(Ljava/lang/String;I)V

    const-string v2, "customGridLayout"

    .line 224
    invoke-virtual {v0, v2, v1}, Landroid/os/Bundle;->putBundle(Ljava/lang/String;Landroid/os/Bundle;)V

    .line 226
    :cond_1
    iget-object v1, p0, Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment;->scrollView:Landroid/widget/ScrollView;

    if-eqz v1, :cond_2

    .line 227
    new-instance v1, Landroid/os/Bundle;

    invoke-direct {v1}, Landroid/os/Bundle;-><init>()V

    const-string v2, "scrollX"

    .line 228
    iget-object v3, p0, Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment;->scrollView:Landroid/widget/ScrollView;

    invoke-virtual {v3}, Landroid/widget/ScrollView;->getScrollX()I

    move-result v3

    invoke-virtual {v1, v2, v3}, Landroid/os/Bundle;->putInt(Ljava/lang/String;I)V

    const-string v2, "scrollY"

    .line 229
    iget-object v3, p0, Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment;->scrollView:Landroid/widget/ScrollView;

    invoke-virtual {v3}, Landroid/widget/ScrollView;->getScrollY()I

    move-result v3

    invoke-virtual {v1, v2, v3}, Landroid/os/Bundle;->putInt(Ljava/lang/String;I)V

    const-string v2, "scrollView"

    .line 230
    invoke-virtual {v0, v2, v1}, Landroid/os/Bundle;->putBundle(Ljava/lang/String;Landroid/os/Bundle;)V

    .line 232
    :cond_2
    iput-object v0, p0, Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment;->savedInstanceState:Landroid/os/Bundle;

    return-void
.end method

.method public onResume()V
    .locals 2

    .line 204
    invoke-super {p0}, Landroid/app/Fragment;->onResume()V

    .line 206
    iget-object v0, p0, Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment;->uiContext:Lnet/gogame/gowrap/ui/UIContext;

    if-eqz v0, :cond_0

    .line 207
    iget-object v0, p0, Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment;->uiContext:Lnet/gogame/gowrap/ui/UIContext;

    invoke-interface {v0}, Lnet/gogame/gowrap/ui/UIContext;->getDownloadManager()Lnet/gogame/gowrap/support/DownloadManager;

    move-result-object v0

    iget-object v1, p0, Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment;->downloadManagerListener:Lnet/gogame/gowrap/support/DownloadManager$Listener;

    invoke-interface {v0, v1}, Lnet/gogame/gowrap/support/DownloadManager;->addListener(Lnet/gogame/gowrap/support/DownloadManager$Listener;)V

    :cond_0
    return-void
.end method
