.class public Lcom/github/droidfu/imageloader/ImageLoaderHandler;
.super Landroid/os/Handler;
.source "ImageLoaderHandler.java"


# instance fields
.field private errorDrawable:Landroid/graphics/drawable/Drawable;

.field private imageUrl:Ljava/lang/String;

.field private imageView:Landroid/widget/ImageView;


# direct methods
.method public constructor <init>(Landroid/widget/ImageView;Ljava/lang/String;)V
    .locals 0

    .line 32
    invoke-direct {p0}, Landroid/os/Handler;-><init>()V

    .line 33
    iput-object p1, p0, Lcom/github/droidfu/imageloader/ImageLoaderHandler;->imageView:Landroid/widget/ImageView;

    .line 34
    iput-object p2, p0, Lcom/github/droidfu/imageloader/ImageLoaderHandler;->imageUrl:Ljava/lang/String;

    return-void
.end method

.method public constructor <init>(Landroid/widget/ImageView;Ljava/lang/String;Landroid/graphics/drawable/Drawable;)V
    .locals 0

    .line 39
    invoke-direct {p0, p1, p2}, Lcom/github/droidfu/imageloader/ImageLoaderHandler;-><init>(Landroid/widget/ImageView;Ljava/lang/String;)V

    .line 40
    iput-object p3, p0, Lcom/github/droidfu/imageloader/ImageLoaderHandler;->errorDrawable:Landroid/graphics/drawable/Drawable;

    return-void
.end method


# virtual methods
.method public getImageUrl()Ljava/lang/String;
    .locals 1

    .line 85
    iget-object v0, p0, Lcom/github/droidfu/imageloader/ImageLoaderHandler;->imageUrl:Ljava/lang/String;

    return-object v0
.end method

.method public getImageView()Landroid/widget/ImageView;
    .locals 1

    .line 93
    iget-object v0, p0, Lcom/github/droidfu/imageloader/ImageLoaderHandler;->imageView:Landroid/widget/ImageView;

    return-object v0
.end method

.method protected handleImageLoaded(Landroid/graphics/Bitmap;Landroid/os/Message;)Z
    .locals 1

    .line 70
    iget-object p2, p0, Lcom/github/droidfu/imageloader/ImageLoaderHandler;->imageView:Landroid/widget/ImageView;

    invoke-virtual {p2}, Landroid/widget/ImageView;->getTag()Ljava/lang/Object;

    move-result-object p2

    check-cast p2, Ljava/lang/String;

    .line 71
    iget-object v0, p0, Lcom/github/droidfu/imageloader/ImageLoaderHandler;->imageUrl:Ljava/lang/String;

    invoke-virtual {v0, p2}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result p2

    if-eqz p2, :cond_3

    if-nez p1, :cond_1

    .line 72
    iget-object p2, p0, Lcom/github/droidfu/imageloader/ImageLoaderHandler;->errorDrawable:Landroid/graphics/drawable/Drawable;

    if-nez p2, :cond_0

    goto :goto_0

    :cond_0
    iget-object p1, p0, Lcom/github/droidfu/imageloader/ImageLoaderHandler;->errorDrawable:Landroid/graphics/drawable/Drawable;

    check-cast p1, Landroid/graphics/drawable/BitmapDrawable;

    invoke-virtual {p1}, Landroid/graphics/drawable/BitmapDrawable;->getBitmap()Landroid/graphics/Bitmap;

    move-result-object p1

    :cond_1
    :goto_0
    if-eqz p1, :cond_2

    .line 75
    iget-object p2, p0, Lcom/github/droidfu/imageloader/ImageLoaderHandler;->imageView:Landroid/widget/ImageView;

    invoke-virtual {p2, p1}, Landroid/widget/ImageView;->setImageBitmap(Landroid/graphics/Bitmap;)V

    :cond_2
    const/4 p1, 0x1

    return p1

    :cond_3
    const/4 p1, 0x0

    return p1
.end method

.method protected final handleImageLoadedMessage(Landroid/os/Message;)V
    .locals 2

    .line 51
    invoke-virtual {p1}, Landroid/os/Message;->getData()Landroid/os/Bundle;

    move-result-object v0

    const-string v1, "droidfu:extra_bitmap"

    .line 52
    invoke-virtual {v0, v1}, Landroid/os/Bundle;->getParcelable(Ljava/lang/String;)Landroid/os/Parcelable;

    move-result-object v0

    check-cast v0, Landroid/graphics/Bitmap;

    .line 53
    invoke-virtual {p0, v0, p1}, Lcom/github/droidfu/imageloader/ImageLoaderHandler;->handleImageLoaded(Landroid/graphics/Bitmap;Landroid/os/Message;)Z

    return-void
.end method

.method public final handleMessage(Landroid/os/Message;)V
    .locals 1

    .line 45
    iget v0, p1, Landroid/os/Message;->what:I

    if-nez v0, :cond_0

    .line 46
    invoke-virtual {p0, p1}, Lcom/github/droidfu/imageloader/ImageLoaderHandler;->handleImageLoadedMessage(Landroid/os/Message;)V

    :cond_0
    return-void
.end method

.method public setImageUrl(Ljava/lang/String;)V
    .locals 0

    .line 89
    iput-object p1, p0, Lcom/github/droidfu/imageloader/ImageLoaderHandler;->imageUrl:Ljava/lang/String;

    return-void
.end method

.method public setImageView(Landroid/widget/ImageView;)V
    .locals 0

    .line 97
    iput-object p1, p0, Lcom/github/droidfu/imageloader/ImageLoaderHandler;->imageView:Landroid/widget/ImageView;

    return-void
.end method
