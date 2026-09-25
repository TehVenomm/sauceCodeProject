.class public Lcom/github/droidfu/adapters/WebGalleryAdapter;
.super Landroid/widget/BaseAdapter;
.source "WebGalleryAdapter.java"


# annotations
.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Lcom/github/droidfu/adapters/WebGalleryAdapter$ViewHolder;
    }
.end annotation


# static fields
.field public static final NO_DRAWABLE:I = -0x1


# instance fields
.field private context:Landroid/content/Context;

.field private errorDrawable:Landroid/graphics/drawable/Drawable;

.field private imageUrls:Ljava/util/List;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/List<",
            "Ljava/lang/String;",
            ">;"
        }
    .end annotation
.end field

.field private progressDrawable:Landroid/graphics/drawable/Drawable;


# direct methods
.method public constructor <init>(Landroid/content/Context;)V
    .locals 1

    .line 48
    invoke-direct {p0}, Landroid/widget/BaseAdapter;-><init>()V

    const/4 v0, 0x0

    .line 49
    invoke-direct {p0, p1, v0, v0, v0}, Lcom/github/droidfu/adapters/WebGalleryAdapter;->initialize(Landroid/content/Context;Ljava/util/List;Landroid/graphics/drawable/Drawable;Landroid/graphics/drawable/Drawable;)V

    return-void
.end method

.method public constructor <init>(Landroid/content/Context;Ljava/util/List;)V
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Landroid/content/Context;",
            "Ljava/util/List<",
            "Ljava/lang/String;",
            ">;)V"
        }
    .end annotation

    .line 58
    invoke-direct {p0}, Landroid/widget/BaseAdapter;-><init>()V

    const/4 v0, 0x0

    .line 59
    invoke-direct {p0, p1, p2, v0, v0}, Lcom/github/droidfu/adapters/WebGalleryAdapter;->initialize(Landroid/content/Context;Ljava/util/List;Landroid/graphics/drawable/Drawable;Landroid/graphics/drawable/Drawable;)V

    return-void
.end method

.method public constructor <init>(Landroid/content/Context;Ljava/util/List;I)V
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Landroid/content/Context;",
            "Ljava/util/List<",
            "Ljava/lang/String;",
            ">;I)V"
        }
    .end annotation

    .line 70
    invoke-direct {p0}, Landroid/widget/BaseAdapter;-><init>()V

    .line 71
    invoke-virtual {p1}, Landroid/content/Context;->getResources()Landroid/content/res/Resources;

    move-result-object v0

    invoke-virtual {v0, p3}, Landroid/content/res/Resources;->getDrawable(I)Landroid/graphics/drawable/Drawable;

    move-result-object p3

    const/4 v0, 0x0

    invoke-direct {p0, p1, p2, p3, v0}, Lcom/github/droidfu/adapters/WebGalleryAdapter;->initialize(Landroid/content/Context;Ljava/util/List;Landroid/graphics/drawable/Drawable;Landroid/graphics/drawable/Drawable;)V

    return-void
.end method

.method public constructor <init>(Landroid/content/Context;Ljava/util/List;II)V
    .locals 3
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Landroid/content/Context;",
            "Ljava/util/List<",
            "Ljava/lang/String;",
            ">;II)V"
        }
    .end annotation

    .line 86
    invoke-direct {p0}, Landroid/widget/BaseAdapter;-><init>()V

    const/4 v0, 0x0

    const/4 v1, -0x1

    if-ne p3, v1, :cond_0

    move-object p3, v0

    goto :goto_0

    .line 87
    :cond_0
    invoke-virtual {p1}, Landroid/content/Context;->getResources()Landroid/content/res/Resources;

    move-result-object v2

    invoke-virtual {v2, p3}, Landroid/content/res/Resources;->getDrawable(I)Landroid/graphics/drawable/Drawable;

    move-result-object p3

    :goto_0
    if-ne p4, v1, :cond_1

    goto :goto_1

    :cond_1
    invoke-virtual {p1}, Landroid/content/Context;->getResources()Landroid/content/res/Resources;

    move-result-object v0

    invoke-virtual {v0, p4}, Landroid/content/res/Resources;->getDrawable(I)Landroid/graphics/drawable/Drawable;

    move-result-object v0

    :goto_1
    invoke-direct {p0, p1, p2, p3, v0}, Lcom/github/droidfu/adapters/WebGalleryAdapter;->initialize(Landroid/content/Context;Ljava/util/List;Landroid/graphics/drawable/Drawable;Landroid/graphics/drawable/Drawable;)V

    return-void
.end method

.method private initialize(Landroid/content/Context;Ljava/util/List;Landroid/graphics/drawable/Drawable;Landroid/graphics/drawable/Drawable;)V
    .locals 0
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Landroid/content/Context;",
            "Ljava/util/List<",
            "Ljava/lang/String;",
            ">;",
            "Landroid/graphics/drawable/Drawable;",
            "Landroid/graphics/drawable/Drawable;",
            ")V"
        }
    .end annotation

    .line 95
    iput-object p2, p0, Lcom/github/droidfu/adapters/WebGalleryAdapter;->imageUrls:Ljava/util/List;

    .line 96
    iput-object p1, p0, Lcom/github/droidfu/adapters/WebGalleryAdapter;->context:Landroid/content/Context;

    .line 97
    iput-object p3, p0, Lcom/github/droidfu/adapters/WebGalleryAdapter;->progressDrawable:Landroid/graphics/drawable/Drawable;

    .line 98
    iput-object p4, p0, Lcom/github/droidfu/adapters/WebGalleryAdapter;->errorDrawable:Landroid/graphics/drawable/Drawable;

    return-void
.end method


# virtual methods
.method public getCount()I
    .locals 1

    .line 102
    iget-object v0, p0, Lcom/github/droidfu/adapters/WebGalleryAdapter;->imageUrls:Ljava/util/List;

    invoke-interface {v0}, Ljava/util/List;->size()I

    move-result v0

    return v0
.end method

.method public getImageUrls()Ljava/util/List;
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()",
            "Ljava/util/List<",
            "Ljava/lang/String;",
            ">;"
        }
    .end annotation

    .line 118
    iget-object v0, p0, Lcom/github/droidfu/adapters/WebGalleryAdapter;->imageUrls:Ljava/util/List;

    return-object v0
.end method

.method public getItem(I)Ljava/lang/Object;
    .locals 1

    .line 106
    iget-object v0, p0, Lcom/github/droidfu/adapters/WebGalleryAdapter;->imageUrls:Ljava/util/List;

    invoke-interface {v0, p1}, Ljava/util/List;->get(I)Ljava/lang/Object;

    move-result-object p1

    return-object p1
.end method

.method public getItemId(I)J
    .locals 2

    int-to-long v0, p1

    return-wide v0
.end method

.method public getProgressDrawable()Landroid/graphics/drawable/Drawable;
    .locals 1

    .line 126
    iget-object v0, p0, Lcom/github/droidfu/adapters/WebGalleryAdapter;->progressDrawable:Landroid/graphics/drawable/Drawable;

    return-object v0
.end method

.method public getView(ILandroid/view/View;Landroid/view/ViewGroup;)Landroid/view/View;
    .locals 8

    .line 134
    invoke-virtual {p0, p1}, Lcom/github/droidfu/adapters/WebGalleryAdapter;->getItem(I)Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Ljava/lang/String;

    if-nez p2, :cond_0

    .line 141
    new-instance p2, Lcom/github/droidfu/widgets/WebImageView;

    iget-object v2, p0, Lcom/github/droidfu/adapters/WebGalleryAdapter;->context:Landroid/content/Context;

    const/4 v3, 0x0

    iget-object v4, p0, Lcom/github/droidfu/adapters/WebGalleryAdapter;->progressDrawable:Landroid/graphics/drawable/Drawable;

    iget-object v5, p0, Lcom/github/droidfu/adapters/WebGalleryAdapter;->errorDrawable:Landroid/graphics/drawable/Drawable;

    const/4 v6, 0x0

    move-object v1, p2

    invoke-direct/range {v1 .. v6}, Lcom/github/droidfu/widgets/WebImageView;-><init>(Landroid/content/Context;Ljava/lang/String;Landroid/graphics/drawable/Drawable;Landroid/graphics/drawable/Drawable;Z)V

    .line 143
    new-instance v1, Landroid/widget/FrameLayout$LayoutParams;

    const/4 v2, -0x2

    invoke-direct {v1, v2, v2}, Landroid/widget/FrameLayout$LayoutParams;-><init>(II)V

    const/16 v2, 0x11

    .line 145
    iput v2, v1, Landroid/widget/FrameLayout$LayoutParams;->gravity:I

    .line 146
    invoke-virtual {p2, v1}, Lcom/github/droidfu/widgets/WebImageView;->setLayoutParams(Landroid/view/ViewGroup$LayoutParams;)V

    .line 149
    new-instance v1, Landroid/widget/FrameLayout;

    iget-object v2, p0, Lcom/github/droidfu/adapters/WebGalleryAdapter;->context:Landroid/content/Context;

    invoke-direct {v1, v2}, Landroid/widget/FrameLayout;-><init>(Landroid/content/Context;)V

    .line 150
    new-instance v2, Landroid/widget/Gallery$LayoutParams;

    const/4 v3, -0x1

    invoke-direct {v2, v3, v3}, Landroid/widget/Gallery$LayoutParams;-><init>(II)V

    invoke-virtual {v1, v2}, Landroid/widget/FrameLayout;->setLayoutParams(Landroid/view/ViewGroup$LayoutParams;)V

    const/4 v2, 0x0

    .line 152
    invoke-virtual {v1, p2, v2}, Landroid/widget/FrameLayout;->addView(Landroid/view/View;I)V

    .line 156
    new-instance v2, Lcom/github/droidfu/adapters/WebGalleryAdapter$ViewHolder;

    const/4 v3, 0x0

    invoke-direct {v2, v3}, Lcom/github/droidfu/adapters/WebGalleryAdapter$ViewHolder;-><init>(Lcom/github/droidfu/adapters/WebGalleryAdapter$1;)V

    .line 157
    invoke-static {v2, p2}, Lcom/github/droidfu/adapters/WebGalleryAdapter$ViewHolder;->access$102(Lcom/github/droidfu/adapters/WebGalleryAdapter$ViewHolder;Lcom/github/droidfu/widgets/WebImageView;)Lcom/github/droidfu/widgets/WebImageView;

    .line 158
    invoke-virtual {v1, v2}, Landroid/view/View;->setTag(Ljava/lang/Object;)V

    goto :goto_0

    .line 160
    :cond_0
    invoke-virtual {p2}, Landroid/view/View;->getTag()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Lcom/github/droidfu/adapters/WebGalleryAdapter$ViewHolder;

    .line 161
    invoke-static {v1}, Lcom/github/droidfu/adapters/WebGalleryAdapter$ViewHolder;->access$100(Lcom/github/droidfu/adapters/WebGalleryAdapter$ViewHolder;)Lcom/github/droidfu/widgets/WebImageView;

    move-result-object v1

    move-object v7, v1

    move-object v1, p2

    move-object p2, v7

    .line 165
    :goto_0
    invoke-virtual {p2}, Lcom/github/droidfu/widgets/WebImageView;->reset()V

    .line 167
    invoke-virtual {p2, v0}, Lcom/github/droidfu/widgets/WebImageView;->setImageUrl(Ljava/lang/String;)V

    .line 168
    invoke-virtual {p2}, Lcom/github/droidfu/widgets/WebImageView;->loadImage()V

    .line 170
    invoke-virtual {p0, p1, v1, p3}, Lcom/github/droidfu/adapters/WebGalleryAdapter;->onGetView(ILandroid/view/View;Landroid/view/ViewGroup;)V

    return-object v1
.end method

.method protected onGetView(ILandroid/view/View;Landroid/view/ViewGroup;)V
    .locals 0

    return-void
.end method

.method public setImageUrls(Ljava/util/List;)V
    .locals 0
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/List<",
            "Ljava/lang/String;",
            ">;)V"
        }
    .end annotation

    .line 114
    iput-object p1, p0, Lcom/github/droidfu/adapters/WebGalleryAdapter;->imageUrls:Ljava/util/List;

    return-void
.end method

.method public setProgressDrawable(Landroid/graphics/drawable/Drawable;)V
    .locals 0

    .line 122
    iput-object p1, p0, Lcom/github/droidfu/adapters/WebGalleryAdapter;->progressDrawable:Landroid/graphics/drawable/Drawable;

    return-void
.end method
