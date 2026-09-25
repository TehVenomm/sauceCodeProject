.class Lcom/github/droidfu/widgets/WebImageView$DefaultImageLoaderHandler;
.super Lcom/github/droidfu/imageloader/ImageLoaderHandler;
.source "WebImageView.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lcom/github/droidfu/widgets/WebImageView;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x2
    name = "DefaultImageLoaderHandler"
.end annotation


# instance fields
.field final synthetic this$0:Lcom/github/droidfu/widgets/WebImageView;


# direct methods
.method public constructor <init>(Lcom/github/droidfu/widgets/WebImageView;)V
    .locals 2

    .line 221
    iput-object p1, p0, Lcom/github/droidfu/widgets/WebImageView$DefaultImageLoaderHandler;->this$0:Lcom/github/droidfu/widgets/WebImageView;

    .line 222
    invoke-static {p1}, Lcom/github/droidfu/widgets/WebImageView;->access$000(Lcom/github/droidfu/widgets/WebImageView;)Landroid/widget/ImageView;

    move-result-object v0

    invoke-static {p1}, Lcom/github/droidfu/widgets/WebImageView;->access$100(Lcom/github/droidfu/widgets/WebImageView;)Ljava/lang/String;

    move-result-object v1

    invoke-static {p1}, Lcom/github/droidfu/widgets/WebImageView;->access$200(Lcom/github/droidfu/widgets/WebImageView;)Landroid/graphics/drawable/Drawable;

    move-result-object p1

    invoke-direct {p0, v0, v1, p1}, Lcom/github/droidfu/imageloader/ImageLoaderHandler;-><init>(Landroid/widget/ImageView;Ljava/lang/String;Landroid/graphics/drawable/Drawable;)V

    return-void
.end method


# virtual methods
.method protected handleImageLoaded(Landroid/graphics/Bitmap;Landroid/os/Message;)Z
    .locals 1

    .line 227
    invoke-super {p0, p1, p2}, Lcom/github/droidfu/imageloader/ImageLoaderHandler;->handleImageLoaded(Landroid/graphics/Bitmap;Landroid/os/Message;)Z

    move-result p1

    if-eqz p1, :cond_0

    .line 229
    iget-object p2, p0, Lcom/github/droidfu/widgets/WebImageView$DefaultImageLoaderHandler;->this$0:Lcom/github/droidfu/widgets/WebImageView;

    const/4 v0, 0x1

    invoke-static {p2, v0}, Lcom/github/droidfu/widgets/WebImageView;->access$302(Lcom/github/droidfu/widgets/WebImageView;Z)Z

    .line 230
    iget-object p2, p0, Lcom/github/droidfu/widgets/WebImageView$DefaultImageLoaderHandler;->this$0:Lcom/github/droidfu/widgets/WebImageView;

    invoke-virtual {p2, v0}, Lcom/github/droidfu/widgets/WebImageView;->setDisplayedChild(I)V

    :cond_0
    return p1
.end method
