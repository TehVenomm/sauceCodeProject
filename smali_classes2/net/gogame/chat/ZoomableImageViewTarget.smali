.class public Lnet/gogame/chat/ZoomableImageViewTarget;
.super Ljava/lang/Object;
.source "ZoomableImageViewTarget.java"

# interfaces
.implements Lcom/squareup/picasso/Target;


# instance fields
.field private final imageView:Lnet/gogame/chat/ZoomableImageView;


# direct methods
.method public constructor <init>(Lnet/gogame/chat/ZoomableImageView;)V
    .locals 0

    .line 14
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 16
    iput-object p1, p0, Lnet/gogame/chat/ZoomableImageViewTarget;->imageView:Lnet/gogame/chat/ZoomableImageView;

    return-void
.end method


# virtual methods
.method public onBitmapFailed(Landroid/graphics/drawable/Drawable;)V
    .locals 1

    .line 26
    iget-object v0, p0, Lnet/gogame/chat/ZoomableImageViewTarget;->imageView:Lnet/gogame/chat/ZoomableImageView;

    invoke-virtual {v0, p1}, Lnet/gogame/chat/ZoomableImageView;->setImageDrawable(Landroid/graphics/drawable/Drawable;)V

    return-void
.end method

.method public onBitmapLoaded(Landroid/graphics/Bitmap;Lcom/squareup/picasso/Picasso$LoadedFrom;)V
    .locals 0

    .line 21
    iget-object p2, p0, Lnet/gogame/chat/ZoomableImageViewTarget;->imageView:Lnet/gogame/chat/ZoomableImageView;

    invoke-virtual {p2, p1}, Lnet/gogame/chat/ZoomableImageView;->setImageBitmap(Landroid/graphics/Bitmap;)V

    return-void
.end method

.method public onPrepareLoad(Landroid/graphics/drawable/Drawable;)V
    .locals 1

    .line 31
    iget-object v0, p0, Lnet/gogame/chat/ZoomableImageViewTarget;->imageView:Lnet/gogame/chat/ZoomableImageView;

    invoke-virtual {v0, p1}, Lnet/gogame/chat/ZoomableImageView;->setImageDrawable(Landroid/graphics/drawable/Drawable;)V

    return-void
.end method
