.class public Lcom/github/droidfu/widgets/WebImageView;
.super Landroid/widget/ViewSwitcher;
.source "WebImageView.java"


# annotations
.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Lcom/github/droidfu/widgets/WebImageView$DefaultImageLoaderHandler;
    }
.end annotation


# instance fields
.field private errorDrawable:Landroid/graphics/drawable/Drawable;

.field private imageUrl:Ljava/lang/String;

.field private imageView:Landroid/widget/ImageView;

.field private isLoaded:Z

.field private loadingSpinner:Landroid/widget/ProgressBar;

.field private progressDrawable:Landroid/graphics/drawable/Drawable;

.field private scaleType:Landroid/widget/ImageView$ScaleType;


# direct methods
.method public constructor <init>(Landroid/content/Context;Landroid/util/AttributeSet;)V
    .locals 9

    .line 107
    invoke-direct {p0, p1, p2}, Landroid/widget/ViewSwitcher;-><init>(Landroid/content/Context;Landroid/util/AttributeSet;)V

    .line 50
    sget-object v0, Landroid/widget/ImageView$ScaleType;->CENTER_CROP:Landroid/widget/ImageView$ScaleType;

    iput-object v0, p0, Lcom/github/droidfu/widgets/WebImageView;->scaleType:Landroid/widget/ImageView$ScaleType;

    const-string v0, "http://github.com/droidfu/schema"

    const-string v1, "progressDrawable"

    const/4 v2, 0x0

    .line 110
    invoke-interface {p2, v0, v1, v2}, Landroid/util/AttributeSet;->getAttributeResourceValue(Ljava/lang/String;Ljava/lang/String;I)I

    move-result v0

    const-string v1, "http://github.com/droidfu/schema"

    const-string v3, "errorDrawable"

    .line 112
    invoke-interface {p2, v1, v3, v2}, Landroid/util/AttributeSet;->getAttributeResourceValue(Ljava/lang/String;Ljava/lang/String;I)I

    move-result v1

    const/4 v2, 0x0

    if-lez v0, :cond_0

    .line 116
    invoke-virtual {p1}, Landroid/content/Context;->getResources()Landroid/content/res/Resources;

    move-result-object v3

    invoke-virtual {v3, v0}, Landroid/content/res/Resources;->getDrawable(I)Landroid/graphics/drawable/Drawable;

    move-result-object v0

    move-object v6, v0

    goto :goto_0

    :cond_0
    move-object v6, v2

    :goto_0
    if-lez v1, :cond_1

    .line 120
    invoke-virtual {p1}, Landroid/content/Context;->getResources()Landroid/content/res/Resources;

    move-result-object v0

    invoke-virtual {v0, v1}, Landroid/content/res/Resources;->getDrawable(I)Landroid/graphics/drawable/Drawable;

    move-result-object v2

    :cond_1
    move-object v7, v2

    const-string v0, "http://github.com/droidfu/schema"

    const-string v1, "imageUrl"

    .line 122
    invoke-interface {p2, v0, v1}, Landroid/util/AttributeSet;->getAttributeValue(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v5

    const-string v0, "http://github.com/droidfu/schema"

    const-string v1, "autoLoad"

    const/4 v2, 0x1

    invoke-interface {p2, v0, v1, v2}, Landroid/util/AttributeSet;->getAttributeBooleanValue(Ljava/lang/String;Ljava/lang/String;Z)Z

    move-result v8

    move-object v3, p0

    move-object v4, p1

    invoke-direct/range {v3 .. v8}, Lcom/github/droidfu/widgets/WebImageView;->initialize(Landroid/content/Context;Ljava/lang/String;Landroid/graphics/drawable/Drawable;Landroid/graphics/drawable/Drawable;Z)V

    return-void
.end method

.method public constructor <init>(Landroid/content/Context;Ljava/lang/String;Landroid/graphics/drawable/Drawable;Landroid/graphics/drawable/Drawable;Z)V
    .locals 1

    .line 102
    invoke-direct {p0, p1}, Landroid/widget/ViewSwitcher;-><init>(Landroid/content/Context;)V

    .line 50
    sget-object v0, Landroid/widget/ImageView$ScaleType;->CENTER_CROP:Landroid/widget/ImageView$ScaleType;

    iput-object v0, p0, Lcom/github/droidfu/widgets/WebImageView;->scaleType:Landroid/widget/ImageView$ScaleType;

    .line 103
    invoke-direct/range {p0 .. p5}, Lcom/github/droidfu/widgets/WebImageView;->initialize(Landroid/content/Context;Ljava/lang/String;Landroid/graphics/drawable/Drawable;Landroid/graphics/drawable/Drawable;Z)V

    return-void
.end method

.method public constructor <init>(Landroid/content/Context;Ljava/lang/String;Landroid/graphics/drawable/Drawable;Z)V
    .locals 7

    .line 82
    invoke-direct {p0, p1}, Landroid/widget/ViewSwitcher;-><init>(Landroid/content/Context;)V

    .line 50
    sget-object v0, Landroid/widget/ImageView$ScaleType;->CENTER_CROP:Landroid/widget/ImageView$ScaleType;

    iput-object v0, p0, Lcom/github/droidfu/widgets/WebImageView;->scaleType:Landroid/widget/ImageView$ScaleType;

    const/4 v5, 0x0

    move-object v1, p0

    move-object v2, p1

    move-object v3, p2

    move-object v4, p3

    move v6, p4

    .line 83
    invoke-direct/range {v1 .. v6}, Lcom/github/droidfu/widgets/WebImageView;->initialize(Landroid/content/Context;Ljava/lang/String;Landroid/graphics/drawable/Drawable;Landroid/graphics/drawable/Drawable;Z)V

    return-void
.end method

.method public constructor <init>(Landroid/content/Context;Ljava/lang/String;Z)V
    .locals 7

    .line 64
    invoke-direct {p0, p1}, Landroid/widget/ViewSwitcher;-><init>(Landroid/content/Context;)V

    .line 50
    sget-object v0, Landroid/widget/ImageView$ScaleType;->CENTER_CROP:Landroid/widget/ImageView$ScaleType;

    iput-object v0, p0, Lcom/github/droidfu/widgets/WebImageView;->scaleType:Landroid/widget/ImageView$ScaleType;

    const/4 v4, 0x0

    const/4 v5, 0x0

    move-object v1, p0

    move-object v2, p1

    move-object v3, p2

    move v6, p3

    .line 65
    invoke-direct/range {v1 .. v6}, Lcom/github/droidfu/widgets/WebImageView;->initialize(Landroid/content/Context;Ljava/lang/String;Landroid/graphics/drawable/Drawable;Landroid/graphics/drawable/Drawable;Z)V

    return-void
.end method

.method static synthetic access$000(Lcom/github/droidfu/widgets/WebImageView;)Landroid/widget/ImageView;
    .locals 0

    .line 40
    iget-object p0, p0, Lcom/github/droidfu/widgets/WebImageView;->imageView:Landroid/widget/ImageView;

    return-object p0
.end method

.method static synthetic access$100(Lcom/github/droidfu/widgets/WebImageView;)Ljava/lang/String;
    .locals 0

    .line 40
    iget-object p0, p0, Lcom/github/droidfu/widgets/WebImageView;->imageUrl:Ljava/lang/String;

    return-object p0
.end method

.method static synthetic access$200(Lcom/github/droidfu/widgets/WebImageView;)Landroid/graphics/drawable/Drawable;
    .locals 0

    .line 40
    iget-object p0, p0, Lcom/github/droidfu/widgets/WebImageView;->errorDrawable:Landroid/graphics/drawable/Drawable;

    return-object p0
.end method

.method static synthetic access$302(Lcom/github/droidfu/widgets/WebImageView;Z)Z
    .locals 0

    .line 40
    iput-boolean p1, p0, Lcom/github/droidfu/widgets/WebImageView;->isLoaded:Z

    return p1
.end method

.method private addImageView(Landroid/content/Context;)V
    .locals 2

    .line 174
    new-instance v0, Landroid/widget/ImageView;

    invoke-direct {v0, p1}, Landroid/widget/ImageView;-><init>(Landroid/content/Context;)V

    iput-object v0, p0, Lcom/github/droidfu/widgets/WebImageView;->imageView:Landroid/widget/ImageView;

    .line 175
    iget-object p1, p0, Lcom/github/droidfu/widgets/WebImageView;->imageView:Landroid/widget/ImageView;

    iget-object v0, p0, Lcom/github/droidfu/widgets/WebImageView;->scaleType:Landroid/widget/ImageView$ScaleType;

    invoke-virtual {p1, v0}, Landroid/widget/ImageView;->setScaleType(Landroid/widget/ImageView$ScaleType;)V

    .line 176
    new-instance p1, Landroid/widget/FrameLayout$LayoutParams;

    const/4 v0, -0x2

    invoke-direct {p1, v0, v0}, Landroid/widget/FrameLayout$LayoutParams;-><init>(II)V

    const/16 v0, 0x11

    .line 177
    iput v0, p1, Landroid/widget/FrameLayout$LayoutParams;->gravity:I

    .line 178
    iget-object v0, p0, Lcom/github/droidfu/widgets/WebImageView;->imageView:Landroid/widget/ImageView;

    const/4 v1, 0x1

    invoke-virtual {p0, v0, v1, p1}, Lcom/github/droidfu/widgets/WebImageView;->addView(Landroid/view/View;ILandroid/view/ViewGroup$LayoutParams;)V

    return-void
.end method

.method private addLoadingSpinnerView(Landroid/content/Context;)V
    .locals 2

    .line 155
    new-instance v0, Landroid/widget/ProgressBar;

    invoke-direct {v0, p1}, Landroid/widget/ProgressBar;-><init>(Landroid/content/Context;)V

    iput-object v0, p0, Lcom/github/droidfu/widgets/WebImageView;->loadingSpinner:Landroid/widget/ProgressBar;

    .line 156
    iget-object p1, p0, Lcom/github/droidfu/widgets/WebImageView;->loadingSpinner:Landroid/widget/ProgressBar;

    const/4 v0, 0x1

    invoke-virtual {p1, v0}, Landroid/widget/ProgressBar;->setIndeterminate(Z)V

    .line 157
    iget-object p1, p0, Lcom/github/droidfu/widgets/WebImageView;->progressDrawable:Landroid/graphics/drawable/Drawable;

    if-nez p1, :cond_0

    .line 158
    iget-object p1, p0, Lcom/github/droidfu/widgets/WebImageView;->loadingSpinner:Landroid/widget/ProgressBar;

    invoke-virtual {p1}, Landroid/widget/ProgressBar;->getIndeterminateDrawable()Landroid/graphics/drawable/Drawable;

    move-result-object p1

    iput-object p1, p0, Lcom/github/droidfu/widgets/WebImageView;->progressDrawable:Landroid/graphics/drawable/Drawable;

    goto :goto_0

    .line 160
    :cond_0
    iget-object p1, p0, Lcom/github/droidfu/widgets/WebImageView;->loadingSpinner:Landroid/widget/ProgressBar;

    iget-object v0, p0, Lcom/github/droidfu/widgets/WebImageView;->progressDrawable:Landroid/graphics/drawable/Drawable;

    invoke-virtual {p1, v0}, Landroid/widget/ProgressBar;->setIndeterminateDrawable(Landroid/graphics/drawable/Drawable;)V

    .line 161
    iget-object p1, p0, Lcom/github/droidfu/widgets/WebImageView;->progressDrawable:Landroid/graphics/drawable/Drawable;

    instance-of p1, p1, Landroid/graphics/drawable/AnimationDrawable;

    if-eqz p1, :cond_1

    .line 162
    iget-object p1, p0, Lcom/github/droidfu/widgets/WebImageView;->progressDrawable:Landroid/graphics/drawable/Drawable;

    check-cast p1, Landroid/graphics/drawable/AnimationDrawable;

    invoke-virtual {p1}, Landroid/graphics/drawable/AnimationDrawable;->start()V

    .line 166
    :cond_1
    :goto_0
    new-instance p1, Landroid/widget/FrameLayout$LayoutParams;

    iget-object v0, p0, Lcom/github/droidfu/widgets/WebImageView;->progressDrawable:Landroid/graphics/drawable/Drawable;

    invoke-virtual {v0}, Landroid/graphics/drawable/Drawable;->getIntrinsicWidth()I

    move-result v0

    iget-object v1, p0, Lcom/github/droidfu/widgets/WebImageView;->progressDrawable:Landroid/graphics/drawable/Drawable;

    invoke-virtual {v1}, Landroid/graphics/drawable/Drawable;->getIntrinsicHeight()I

    move-result v1

    invoke-direct {p1, v0, v1}, Landroid/widget/FrameLayout$LayoutParams;-><init>(II)V

    const/16 v0, 0x11

    .line 168
    iput v0, p1, Landroid/widget/FrameLayout$LayoutParams;->gravity:I

    .line 170
    iget-object v0, p0, Lcom/github/droidfu/widgets/WebImageView;->loadingSpinner:Landroid/widget/ProgressBar;

    const/4 v1, 0x0

    invoke-virtual {p0, v0, v1, p1}, Lcom/github/droidfu/widgets/WebImageView;->addView(Landroid/view/View;ILandroid/view/ViewGroup$LayoutParams;)V

    return-void
.end method

.method private initialize(Landroid/content/Context;Ljava/lang/String;Landroid/graphics/drawable/Drawable;Landroid/graphics/drawable/Drawable;Z)V
    .locals 0

    .line 132
    iput-object p2, p0, Lcom/github/droidfu/widgets/WebImageView;->imageUrl:Ljava/lang/String;

    .line 133
    iput-object p3, p0, Lcom/github/droidfu/widgets/WebImageView;->progressDrawable:Landroid/graphics/drawable/Drawable;

    .line 134
    iput-object p4, p0, Lcom/github/droidfu/widgets/WebImageView;->errorDrawable:Landroid/graphics/drawable/Drawable;

    .line 136
    invoke-static {p1}, Lcom/github/droidfu/imageloader/ImageLoader;->initialize(Landroid/content/Context;)V

    .line 146
    invoke-direct {p0, p1}, Lcom/github/droidfu/widgets/WebImageView;->addLoadingSpinnerView(Landroid/content/Context;)V

    .line 147
    invoke-direct {p0, p1}, Lcom/github/droidfu/widgets/WebImageView;->addImageView(Landroid/content/Context;)V

    if-eqz p5, :cond_0

    if-eqz p2, :cond_0

    .line 150
    invoke-virtual {p0}, Lcom/github/droidfu/widgets/WebImageView;->loadImage()V

    :cond_0
    return-void
.end method


# virtual methods
.method public isLoaded()Z
    .locals 1

    .line 193
    iget-boolean v0, p0, Lcom/github/droidfu/widgets/WebImageView;->isLoaded:Z

    return v0
.end method

.method public loadImage()V
    .locals 2

    .line 185
    iget-object v0, p0, Lcom/github/droidfu/widgets/WebImageView;->imageUrl:Ljava/lang/String;

    if-eqz v0, :cond_0

    .line 189
    iget-object v0, p0, Lcom/github/droidfu/widgets/WebImageView;->imageUrl:Ljava/lang/String;

    new-instance v1, Lcom/github/droidfu/widgets/WebImageView$DefaultImageLoaderHandler;

    invoke-direct {v1, p0}, Lcom/github/droidfu/widgets/WebImageView$DefaultImageLoaderHandler;-><init>(Lcom/github/droidfu/widgets/WebImageView;)V

    invoke-static {v0, v1}, Lcom/github/droidfu/imageloader/ImageLoader;->start(Ljava/lang/String;Lcom/github/droidfu/imageloader/ImageLoaderHandler;)V

    return-void

    .line 186
    :cond_0
    new-instance v0, Ljava/lang/IllegalStateException;

    const-string v1, "image URL is null; did you forget to set it for this view?"

    invoke-direct {v0, v1}, Ljava/lang/IllegalStateException;-><init>(Ljava/lang/String;)V

    throw v0
.end method

.method public reset()V
    .locals 1

    .line 214
    invoke-super {p0}, Landroid/widget/ViewSwitcher;->reset()V

    const/4 v0, 0x0

    .line 216
    invoke-virtual {p0, v0}, Lcom/github/droidfu/widgets/WebImageView;->setDisplayedChild(I)V

    return-void
.end method

.method public setImageUrl(Ljava/lang/String;)V
    .locals 0

    .line 197
    iput-object p1, p0, Lcom/github/droidfu/widgets/WebImageView;->imageUrl:Ljava/lang/String;

    return-void
.end method

.method public setNoImageDrawable(I)V
    .locals 2

    .line 208
    iget-object v0, p0, Lcom/github/droidfu/widgets/WebImageView;->imageView:Landroid/widget/ImageView;

    invoke-virtual {p0}, Lcom/github/droidfu/widgets/WebImageView;->getContext()Landroid/content/Context;

    move-result-object v1

    invoke-virtual {v1}, Landroid/content/Context;->getResources()Landroid/content/res/Resources;

    move-result-object v1

    invoke-virtual {v1, p1}, Landroid/content/res/Resources;->getDrawable(I)Landroid/graphics/drawable/Drawable;

    move-result-object p1

    invoke-virtual {v0, p1}, Landroid/widget/ImageView;->setImageDrawable(Landroid/graphics/drawable/Drawable;)V

    const/4 p1, 0x1

    .line 209
    invoke-virtual {p0, p1}, Lcom/github/droidfu/widgets/WebImageView;->setDisplayedChild(I)V

    return-void
.end method
