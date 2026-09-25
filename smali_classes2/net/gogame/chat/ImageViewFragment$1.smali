.class Lnet/gogame/chat/ImageViewFragment$1;
.super Lnet/gogame/chat/ZoomableImageViewTarget;
.source "ImageViewFragment.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lnet/gogame/chat/ImageViewFragment;->onCreateView(Landroid/view/LayoutInflater;Landroid/view/ViewGroup;Landroid/os/Bundle;)Landroid/view/View;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lnet/gogame/chat/ImageViewFragment;

.field final synthetic val$progressBar:Landroid/widget/ProgressBar;


# direct methods
.method constructor <init>(Lnet/gogame/chat/ImageViewFragment;Lnet/gogame/chat/ZoomableImageView;Landroid/widget/ProgressBar;)V
    .locals 0

    .line 38
    iput-object p1, p0, Lnet/gogame/chat/ImageViewFragment$1;->this$0:Lnet/gogame/chat/ImageViewFragment;

    iput-object p3, p0, Lnet/gogame/chat/ImageViewFragment$1;->val$progressBar:Landroid/widget/ProgressBar;

    invoke-direct {p0, p2}, Lnet/gogame/chat/ZoomableImageViewTarget;-><init>(Lnet/gogame/chat/ZoomableImageView;)V

    return-void
.end method


# virtual methods
.method public onBitmapLoaded(Landroid/graphics/Bitmap;Lcom/squareup/picasso/Picasso$LoadedFrom;)V
    .locals 2

    .line 42
    iget-object v0, p0, Lnet/gogame/chat/ImageViewFragment$1;->val$progressBar:Landroid/widget/ProgressBar;

    const/16 v1, 0x8

    invoke-virtual {v0, v1}, Landroid/widget/ProgressBar;->setVisibility(I)V

    .line 43
    invoke-super {p0, p1, p2}, Lnet/gogame/chat/ZoomableImageViewTarget;->onBitmapLoaded(Landroid/graphics/Bitmap;Lcom/squareup/picasso/Picasso$LoadedFrom;)V

    return-void
.end method
