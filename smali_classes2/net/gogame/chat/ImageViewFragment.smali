.class public Lnet/gogame/chat/ImageViewFragment;
.super Landroidx/fragment/app/Fragment;
.source "ImageViewFragment.java"


# instance fields
.field private target:Lcom/squareup/picasso/Target;


# direct methods
.method public constructor <init>()V
    .locals 0

    .line 17
    invoke-direct {p0}, Landroidx/fragment/app/Fragment;-><init>()V

    return-void
.end method


# virtual methods
.method public onCreateView(Landroid/view/LayoutInflater;Landroid/view/ViewGroup;Landroid/os/Bundle;)Landroid/view/View;
    .locals 2
    .annotation build Landroidx/annotation/Nullable;
    .end annotation

    .line 25
    sget p3, Lcom/zopim/android/sdk/R$layout;->net_gogame_chat_fragment_image_view:I

    const/4 v0, 0x0

    invoke-virtual {p1, p3, p2, v0}, Landroid/view/LayoutInflater;->inflate(ILandroid/view/ViewGroup;Z)Landroid/view/View;

    move-result-object p1

    .line 27
    sget p2, Lcom/zopim/android/sdk/R$id;->imageView:I

    invoke-virtual {p1, p2}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object p2

    check-cast p2, Lnet/gogame/chat/ZoomableImageView;

    .line 28
    sget p3, Lcom/zopim/android/sdk/R$id;->progressBar:I

    invoke-virtual {p1, p3}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object p3

    check-cast p3, Landroid/widget/ProgressBar;

    .line 29
    invoke-virtual {p0}, Lnet/gogame/chat/ImageViewFragment;->getActivity()Landroidx/fragment/app/FragmentActivity;

    move-result-object v0

    invoke-virtual {p2}, Lnet/gogame/chat/ZoomableImageView;->getWindowToken()Landroid/os/IBinder;

    move-result-object v1

    invoke-static {v0, v1}, Lnet/gogame/chat/DisplayUtils;->hideKeyboard(Landroid/content/Context;Landroid/os/IBinder;)V

    .line 32
    invoke-virtual {p0}, Lnet/gogame/chat/ImageViewFragment;->getArguments()Landroid/os/Bundle;

    move-result-object v0

    if-eqz v0, :cond_0

    .line 33
    invoke-virtual {p0}, Lnet/gogame/chat/ImageViewFragment;->getArguments()Landroid/os/Bundle;

    move-result-object v0

    const-string v1, "uri"

    invoke-virtual {v0, v1}, Landroid/os/Bundle;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v0

    if-eqz v0, :cond_0

    .line 34
    invoke-virtual {p0}, Lnet/gogame/chat/ImageViewFragment;->getArguments()Landroid/os/Bundle;

    move-result-object v0

    const-string v1, "uri"

    invoke-virtual {v0, v1}, Landroid/os/Bundle;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v0

    invoke-static {v0}, Landroid/net/Uri;->parse(Ljava/lang/String;)Landroid/net/Uri;

    move-result-object v0

    goto :goto_0

    :cond_0
    const/4 v0, 0x0

    :goto_0
    if-eqz v0, :cond_1

    .line 38
    new-instance v1, Lnet/gogame/chat/ImageViewFragment$1;

    invoke-direct {v1, p0, p2, p3}, Lnet/gogame/chat/ImageViewFragment$1;-><init>(Lnet/gogame/chat/ImageViewFragment;Lnet/gogame/chat/ZoomableImageView;Landroid/widget/ProgressBar;)V

    iput-object v1, p0, Lnet/gogame/chat/ImageViewFragment;->target:Lcom/squareup/picasso/Target;

    .line 46
    invoke-virtual {p0}, Lnet/gogame/chat/ImageViewFragment;->getActivity()Landroidx/fragment/app/FragmentActivity;

    move-result-object p2

    invoke-static {p2}, Lcom/squareup/picasso/Picasso;->with(Landroid/content/Context;)Lcom/squareup/picasso/Picasso;

    move-result-object p2

    invoke-virtual {p2, v0}, Lcom/squareup/picasso/Picasso;->load(Landroid/net/Uri;)Lcom/squareup/picasso/RequestCreator;

    move-result-object p2

    iget-object p3, p0, Lnet/gogame/chat/ImageViewFragment;->target:Lcom/squareup/picasso/Target;

    invoke-virtual {p2, p3}, Lcom/squareup/picasso/RequestCreator;->into(Lcom/squareup/picasso/Target;)V

    :cond_1
    return-object p1
.end method
