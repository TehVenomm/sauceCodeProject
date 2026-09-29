.class Lnet/gogame/gowrap/ui/v2017_2/NewsFragment$7;
.super Ljava/lang/Object;
.source "NewsFragment.java"

# interfaces
.implements Lnet/gogame/gowrap/support/DownloadManager$Target;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;->doShowBanner(Lnet/gogame/gowrap/model/news/Banner;)V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;


# direct methods
.method constructor <init>(Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;)V
    .locals 0

    .line 412
    iput-object p1, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment$7;->this$0:Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public onDownloadFailed(Landroid/graphics/drawable/Drawable;)V
    .locals 0

    return-void
.end method

.method public onDownloadStarted(Landroid/graphics/drawable/Drawable;)V
    .locals 0

    return-void
.end method

.method public onDownloadSucceeded(Lnet/gogame/gowrap/support/DownloadManager$DownloadResult;)V
    .locals 3

    .line 423
    :try_start_0
    iget-object v0, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment$7;->this$0:Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;

    .line 424
    invoke-static {v0}, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;->access$200(Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;)Landroid/widget/ImageSwitcher;

    move-result-object v0

    invoke-virtual {v0}, Landroid/widget/ImageSwitcher;->getContext()Landroid/content/Context;

    move-result-object v0

    new-instance v1, Lnet/gogame/gowrap/ui/download/DownloadResultSource;

    invoke-direct {v1, p1}, Lnet/gogame/gowrap/ui/download/DownloadResultSource;-><init>(Lnet/gogame/gowrap/support/DownloadManager$DownloadResult;)V

    iget-object p1, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment$7;->this$0:Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;

    .line 426
    invoke-static {p1}, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;->access$200(Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;)Landroid/widget/ImageSwitcher;

    move-result-object p1

    invoke-virtual {p1}, Landroid/widget/ImageSwitcher;->getWidth()I

    move-result p1

    invoke-static {p1}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object p1

    iget-object v2, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment$7;->this$0:Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;

    invoke-static {v2}, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;->access$200(Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;)Landroid/widget/ImageSwitcher;

    move-result-object v2

    invoke-virtual {v2}, Landroid/widget/ImageSwitcher;->getHeight()I

    move-result v2

    invoke-static {v2}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v2

    .line 423
    invoke-static {v0, v1, p1, v2}, Lnet/gogame/gowrap/ui/utils/ImageUtils;->getSampledBitmapDrawable(Landroid/content/Context;Lnet/gogame/gowrap/ui/utils/ImageUtils$Source;Ljava/lang/Integer;Ljava/lang/Integer;)Landroid/graphics/drawable/BitmapDrawable;

    move-result-object p1

    .line 427
    iget-object v0, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment$7;->this$0:Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;

    invoke-static {v0}, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;->access$200(Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;)Landroid/widget/ImageSwitcher;

    move-result-object v0

    invoke-virtual {v0, p1}, Landroid/widget/ImageSwitcher;->setImageDrawable(Landroid/graphics/drawable/Drawable;)V
    :try_end_0
    .catch Ljava/lang/Throwable; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception p1

    const-string v0, "goWrap"

    const-string v1, "Exception"

    .line 429
    invoke-static {v0, v1, p1}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)I

    :goto_0
    return-void
.end method
