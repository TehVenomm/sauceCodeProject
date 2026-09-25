.class public Lnet/gogame/gowrap/ui/download/ImageViewTarget;
.super Ljava/lang/Object;
.source "ImageViewTarget.java"

# interfaces
.implements Lnet/gogame/gowrap/support/DownloadManager$Target;


# instance fields
.field private cancelled:Z

.field private imageView:Landroid/widget/ImageView;


# direct methods
.method public constructor <init>(Landroid/widget/ImageView;)V
    .locals 0

    .line 17
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 19
    iput-object p1, p0, Lnet/gogame/gowrap/ui/download/ImageViewTarget;->imageView:Landroid/widget/ImageView;

    return-void
.end method


# virtual methods
.method public isCancelled()Z
    .locals 1

    .line 23
    iget-boolean v0, p0, Lnet/gogame/gowrap/ui/download/ImageViewTarget;->cancelled:Z

    return v0
.end method

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

    .line 37
    iget-boolean v0, p0, Lnet/gogame/gowrap/ui/download/ImageViewTarget;->cancelled:Z

    if-eqz v0, :cond_0

    return-void

    .line 41
    :cond_0
    :try_start_0
    iget-object v0, p0, Lnet/gogame/gowrap/ui/download/ImageViewTarget;->imageView:Landroid/widget/ImageView;

    invoke-virtual {v0}, Landroid/widget/ImageView;->getContext()Landroid/content/Context;

    move-result-object v0

    new-instance v1, Lnet/gogame/gowrap/ui/download/DownloadResultSource;

    invoke-direct {v1, p1}, Lnet/gogame/gowrap/ui/download/DownloadResultSource;-><init>(Lnet/gogame/gowrap/support/DownloadManager$DownloadResult;)V

    iget-object p1, p0, Lnet/gogame/gowrap/ui/download/ImageViewTarget;->imageView:Landroid/widget/ImageView;

    .line 42
    invoke-virtual {p1}, Landroid/widget/ImageView;->getWidth()I

    move-result p1

    invoke-static {p1}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object p1

    iget-object v2, p0, Lnet/gogame/gowrap/ui/download/ImageViewTarget;->imageView:Landroid/widget/ImageView;

    .line 43
    invoke-virtual {v2}, Landroid/widget/ImageView;->getHeight()I

    move-result v2

    invoke-static {v2}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v2

    .line 41
    invoke-static {v0, v1, p1, v2}, Lnet/gogame/gowrap/ui/utils/ImageUtils;->getSampledBitmapDrawable(Landroid/content/Context;Lnet/gogame/gowrap/ui/utils/ImageUtils$Source;Ljava/lang/Integer;Ljava/lang/Integer;)Landroid/graphics/drawable/BitmapDrawable;

    move-result-object p1

    .line 44
    iget-object v0, p0, Lnet/gogame/gowrap/ui/download/ImageViewTarget;->imageView:Landroid/widget/ImageView;

    invoke-virtual {v0, p1}, Landroid/widget/ImageView;->setImageDrawable(Landroid/graphics/drawable/Drawable;)V
    :try_end_0
    .catch Ljava/lang/Throwable; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception p1

    const-string v0, "goWrap"

    const-string v1, "Exception"

    .line 46
    invoke-static {v0, v1, p1}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)I

    :goto_0
    return-void
.end method

.method public setCancelled(Z)V
    .locals 0

    .line 27
    iput-boolean p1, p0, Lnet/gogame/gowrap/ui/download/ImageViewTarget;->cancelled:Z

    return-void
.end method
