.class public Lnet/gogame/gowrap/ui/v2017_1/VideoNewsFeedItemView;
.super Lnet/gogame/gowrap/ui/v2017_1/AbstractNewsFeedItemView;
.source "VideoNewsFeedItemView.java"

# interfaces
.implements Lnet/gogame/gowrap/support/DownloadManager$Target;


# instance fields
.field private downloadResult:Lnet/gogame/gowrap/support/DownloadManager$DownloadResult;

.field private videoUri:Landroid/net/Uri;

.field private videoView:Landroid/widget/VideoView;


# direct methods
.method public constructor <init>(Landroid/content/Context;)V
    .locals 0

    .line 27
    invoke-direct {p0, p1}, Lnet/gogame/gowrap/ui/v2017_1/AbstractNewsFeedItemView;-><init>(Landroid/content/Context;)V

    return-void
.end method

.method public constructor <init>(Landroid/content/Context;Landroid/util/AttributeSet;)V
    .locals 0

    .line 31
    invoke-direct {p0, p1, p2}, Lnet/gogame/gowrap/ui/v2017_1/AbstractNewsFeedItemView;-><init>(Landroid/content/Context;Landroid/util/AttributeSet;)V

    return-void
.end method

.method public constructor <init>(Landroid/content/Context;Landroid/util/AttributeSet;I)V
    .locals 0

    .line 35
    invoke-direct {p0, p1, p2, p3}, Lnet/gogame/gowrap/ui/v2017_1/AbstractNewsFeedItemView;-><init>(Landroid/content/Context;Landroid/util/AttributeSet;I)V

    return-void
.end method

.method public constructor <init>(Landroid/content/Context;Landroid/util/AttributeSet;II)V
    .locals 0
    .annotation build Landroid/annotation/TargetApi;
        value = 0x15
    .end annotation

    .line 41
    invoke-direct {p0, p1, p2, p3, p4}, Lnet/gogame/gowrap/ui/v2017_1/AbstractNewsFeedItemView;-><init>(Landroid/content/Context;Landroid/util/AttributeSet;II)V

    .line 42
    invoke-virtual {p0, p1}, Lnet/gogame/gowrap/ui/v2017_1/VideoNewsFeedItemView;->customInit(Landroid/content/Context;)V

    return-void
.end method

.method private update()V
    .locals 4

    .line 83
    invoke-virtual {p0}, Lnet/gogame/gowrap/ui/v2017_1/VideoNewsFeedItemView;->isLayoutCompleted()Z

    move-result v0

    if-eqz v0, :cond_0

    iget-object v0, p0, Lnet/gogame/gowrap/ui/v2017_1/VideoNewsFeedItemView;->downloadResult:Lnet/gogame/gowrap/support/DownloadManager$DownloadResult;

    if-eqz v0, :cond_0

    .line 85
    :try_start_0
    iget-object v0, p0, Lnet/gogame/gowrap/ui/v2017_1/VideoNewsFeedItemView;->videoView:Landroid/widget/VideoView;

    .line 86
    invoke-virtual {v0}, Landroid/widget/VideoView;->getContext()Landroid/content/Context;

    move-result-object v0

    new-instance v1, Lnet/gogame/gowrap/ui/download/DownloadResultSource;

    iget-object v2, p0, Lnet/gogame/gowrap/ui/v2017_1/VideoNewsFeedItemView;->downloadResult:Lnet/gogame/gowrap/support/DownloadManager$DownloadResult;

    invoke-direct {v1, v2}, Lnet/gogame/gowrap/ui/download/DownloadResultSource;-><init>(Lnet/gogame/gowrap/support/DownloadManager$DownloadResult;)V

    iget-object v2, p0, Lnet/gogame/gowrap/ui/v2017_1/VideoNewsFeedItemView;->videoView:Landroid/widget/VideoView;

    .line 87
    invoke-virtual {v2}, Landroid/widget/VideoView;->getWidth()I

    move-result v2

    invoke-static {v2}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v2

    iget-object v3, p0, Lnet/gogame/gowrap/ui/v2017_1/VideoNewsFeedItemView;->videoView:Landroid/widget/VideoView;

    invoke-virtual {v3}, Landroid/widget/VideoView;->getHeight()I

    move-result v3

    invoke-static {v3}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v3

    .line 85
    invoke-static {v0, v1, v2, v3}, Lnet/gogame/gowrap/ui/utils/ImageUtils;->getSampledBitmapDrawable(Landroid/content/Context;Lnet/gogame/gowrap/ui/utils/ImageUtils$Source;Ljava/lang/Integer;Ljava/lang/Integer;)Landroid/graphics/drawable/BitmapDrawable;

    move-result-object v0

    .line 88
    iget-object v1, p0, Lnet/gogame/gowrap/ui/v2017_1/VideoNewsFeedItemView;->videoView:Landroid/widget/VideoView;

    invoke-virtual {v1, v0}, Landroid/widget/VideoView;->setBackgroundDrawable(Landroid/graphics/drawable/Drawable;)V
    :try_end_0
    .catch Ljava/lang/Throwable; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception v0

    const-string v1, "goWrap"

    const-string v2, "Exception"

    .line 90
    invoke-static {v1, v2, v0}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)I

    :cond_0
    :goto_0
    return-void
.end method


# virtual methods
.method protected customInit(Landroid/content/Context;)V
    .locals 1

    .line 47
    sget v0, Lnet/gogame/gowrap/R$id;->net_gogame_gowrap_newsfeed_item_video:I

    invoke-virtual {p0, v0}, Lnet/gogame/gowrap/ui/v2017_1/VideoNewsFeedItemView;->findViewById(I)Landroid/view/View;

    move-result-object v0

    check-cast v0, Landroid/widget/VideoView;

    iput-object v0, p0, Lnet/gogame/gowrap/ui/v2017_1/VideoNewsFeedItemView;->videoView:Landroid/widget/VideoView;

    .line 48
    new-instance v0, Landroid/widget/MediaController;

    invoke-direct {v0, p1}, Landroid/widget/MediaController;-><init>(Landroid/content/Context;)V

    .line 49
    invoke-virtual {v0, p0}, Landroid/widget/MediaController;->setAnchorView(Landroid/view/View;)V

    .line 50
    iget-object p1, p0, Lnet/gogame/gowrap/ui/v2017_1/VideoNewsFeedItemView;->videoView:Landroid/widget/VideoView;

    invoke-virtual {p1, v0}, Landroid/widget/VideoView;->setMediaController(Landroid/widget/MediaController;)V

    return-void
.end method

.method protected getButtonClickResourceIds()[I
    .locals 3

    const/4 v0, 0x1

    .line 72
    new-array v0, v0, [I

    sget v1, Lnet/gogame/gowrap/R$id;->net_gogame_gowrap_newsfeed_item_button:I

    const/4 v2, 0x0

    aput v1, v0, v2

    return-object v0
.end method

.method protected getClickResourceIds()[I
    .locals 3

    const/4 v0, 0x1

    .line 65
    new-array v0, v0, [I

    sget v1, Lnet/gogame/gowrap/R$id;->net_gogame_gowrap_newsfeed_item_content:I

    const/4 v2, 0x0

    aput v1, v0, v2

    return-object v0
.end method

.method protected getResizingViewResourceId()Ljava/lang/Integer;
    .locals 1

    .line 60
    sget v0, Lnet/gogame/gowrap/R$id;->net_gogame_gowrap_newsfeed_item_video:I

    invoke-static {v0}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v0

    return-object v0
.end method

.method protected getViewResourceId()I
    .locals 1

    .line 55
    sget v0, Lnet/gogame/gowrap/R$layout;->net_gogame_gowrap_newsfeed_video_item:I

    return v0
.end method

.method public onDownloadFailed(Landroid/graphics/drawable/Drawable;)V
    .locals 1

    .line 118
    iget-object v0, p0, Lnet/gogame/gowrap/ui/v2017_1/VideoNewsFeedItemView;->videoView:Landroid/widget/VideoView;

    invoke-virtual {v0, p1}, Landroid/widget/VideoView;->setBackgroundDrawable(Landroid/graphics/drawable/Drawable;)V

    return-void
.end method

.method public onDownloadStarted(Landroid/graphics/drawable/Drawable;)V
    .locals 1

    if-eqz p1, :cond_0

    .line 106
    iget-object v0, p0, Lnet/gogame/gowrap/ui/v2017_1/VideoNewsFeedItemView;->videoView:Landroid/widget/VideoView;

    invoke-virtual {v0, p1}, Landroid/widget/VideoView;->setBackgroundDrawable(Landroid/graphics/drawable/Drawable;)V

    :cond_0
    return-void
.end method

.method public onDownloadSucceeded(Lnet/gogame/gowrap/support/DownloadManager$DownloadResult;)V
    .locals 0

    .line 112
    iput-object p1, p0, Lnet/gogame/gowrap/ui/v2017_1/VideoNewsFeedItemView;->downloadResult:Lnet/gogame/gowrap/support/DownloadManager$DownloadResult;

    .line 113
    invoke-direct {p0}, Lnet/gogame/gowrap/ui/v2017_1/VideoNewsFeedItemView;->update()V

    return-void
.end method

.method protected onLayoutCompleted()V
    .locals 0

    .line 79
    invoke-direct {p0}, Lnet/gogame/gowrap/ui/v2017_1/VideoNewsFeedItemView;->update()V

    return-void
.end method

.method public setPreviewImage(Landroid/graphics/drawable/Drawable;)V
    .locals 1

    .line 96
    iget-object v0, p0, Lnet/gogame/gowrap/ui/v2017_1/VideoNewsFeedItemView;->videoView:Landroid/widget/VideoView;

    invoke-virtual {v0, p1}, Landroid/widget/VideoView;->setBackgroundDrawable(Landroid/graphics/drawable/Drawable;)V

    return-void
.end method

.method public setVideo(Landroid/net/Uri;)V
    .locals 0

    .line 100
    iput-object p1, p0, Lnet/gogame/gowrap/ui/v2017_1/VideoNewsFeedItemView;->videoUri:Landroid/net/Uri;

    return-void
.end method
