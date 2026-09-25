.class public Lnet/gogame/gowrap/ui/v2017_1/SupportCustomImageButton;
.super Lnet/gogame/gowrap/ui/v2017_1/AbstractCustomImageButton;
.source "SupportCustomImageButton.java"


# direct methods
.method public constructor <init>(Landroid/content/Context;)V
    .locals 0

    .line 17
    invoke-direct {p0, p1}, Lnet/gogame/gowrap/ui/v2017_1/AbstractCustomImageButton;-><init>(Landroid/content/Context;)V

    const/4 p1, 0x0

    .line 18
    invoke-virtual {p0, p1}, Lnet/gogame/gowrap/ui/v2017_1/SupportCustomImageButton;->init(Landroid/util/AttributeSet;)V

    return-void
.end method

.method public constructor <init>(Landroid/content/Context;Landroid/util/AttributeSet;)V
    .locals 0

    .line 22
    invoke-direct {p0, p1, p2}, Lnet/gogame/gowrap/ui/v2017_1/AbstractCustomImageButton;-><init>(Landroid/content/Context;Landroid/util/AttributeSet;)V

    .line 23
    invoke-virtual {p0, p2}, Lnet/gogame/gowrap/ui/v2017_1/SupportCustomImageButton;->init(Landroid/util/AttributeSet;)V

    return-void
.end method

.method public constructor <init>(Landroid/content/Context;Landroid/util/AttributeSet;I)V
    .locals 0

    .line 27
    invoke-direct {p0, p1, p2, p3}, Lnet/gogame/gowrap/ui/v2017_1/AbstractCustomImageButton;-><init>(Landroid/content/Context;Landroid/util/AttributeSet;I)V

    .line 28
    invoke-virtual {p0, p2}, Lnet/gogame/gowrap/ui/v2017_1/SupportCustomImageButton;->init(Landroid/util/AttributeSet;)V

    return-void
.end method

.method public constructor <init>(Landroid/content/Context;Landroid/util/AttributeSet;II)V
    .locals 0
    .annotation build Landroid/annotation/TargetApi;
        value = 0x15
    .end annotation

    .line 34
    invoke-direct {p0, p1, p2, p3, p4}, Lnet/gogame/gowrap/ui/v2017_1/AbstractCustomImageButton;-><init>(Landroid/content/Context;Landroid/util/AttributeSet;II)V

    .line 35
    invoke-virtual {p0, p2}, Lnet/gogame/gowrap/ui/v2017_1/SupportCustomImageButton;->init(Landroid/util/AttributeSet;)V

    return-void
.end method


# virtual methods
.method protected init(Landroid/util/AttributeSet;)V
    .locals 5

    .line 40
    invoke-virtual {p0}, Lnet/gogame/gowrap/ui/v2017_1/SupportCustomImageButton;->getContext()Landroid/content/Context;

    move-result-object v0

    const-string v1, "layout_inflater"

    invoke-virtual {v0, v1}, Landroid/content/Context;->getSystemService(Ljava/lang/String;)Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Landroid/view/LayoutInflater;

    .line 42
    sget v1, Lnet/gogame/gowrap/R$layout;->net_gogame_gowrap_support_image_button:I

    const/4 v2, 0x0

    invoke-virtual {v0, v1, p0, v2}, Landroid/view/LayoutInflater;->inflate(ILandroid/view/ViewGroup;Z)Landroid/view/View;

    move-result-object v0

    .line 44
    invoke-virtual {p0, v0}, Lnet/gogame/gowrap/ui/v2017_1/SupportCustomImageButton;->addView(Landroid/view/View;)V

    .line 46
    invoke-virtual {p0}, Lnet/gogame/gowrap/ui/v2017_1/SupportCustomImageButton;->getContext()Landroid/content/Context;

    move-result-object v0

    invoke-virtual {v0}, Landroid/content/Context;->getTheme()Landroid/content/res/Resources$Theme;

    move-result-object v0

    sget-object v1, Lnet/gogame/gowrap/R$styleable;->CustomImageButton:[I

    invoke-virtual {v0, p1, v1, v2, v2}, Landroid/content/res/Resources$Theme;->obtainStyledAttributes(Landroid/util/AttributeSet;[III)Landroid/content/res/TypedArray;

    move-result-object p1

    .line 49
    :try_start_0
    sget v0, Lnet/gogame/gowrap/R$styleable;->CustomImageButton_image:I

    invoke-virtual {p1, v0}, Landroid/content/res/TypedArray;->getDrawable(I)Landroid/graphics/drawable/Drawable;

    move-result-object v0

    .line 50
    sget v1, Lnet/gogame/gowrap/R$styleable;->CustomImageButton_caption:I

    invoke-virtual {p1, v1}, Landroid/content/res/TypedArray;->getString(I)Ljava/lang/String;

    move-result-object v1

    .line 51
    sget v3, Lnet/gogame/gowrap/R$styleable;->CustomImageButton_subcaption:I

    invoke-virtual {p1, v3}, Landroid/content/res/TypedArray;->getString(I)Ljava/lang/String;

    move-result-object v3

    .line 53
    sget v4, Lnet/gogame/gowrap/R$styleable;->CustomImageButton_level:I

    invoke-virtual {p1, v4, v2}, Landroid/content/res/TypedArray;->getInt(II)I

    move-result v2

    .line 55
    invoke-virtual {p0, v0}, Lnet/gogame/gowrap/ui/v2017_1/SupportCustomImageButton;->setImage(Landroid/graphics/drawable/Drawable;)V

    .line 56
    invoke-virtual {p0, v1}, Lnet/gogame/gowrap/ui/v2017_1/SupportCustomImageButton;->setCaption(Ljava/lang/String;)V

    .line 57
    invoke-virtual {p0, v3}, Lnet/gogame/gowrap/ui/v2017_1/SupportCustomImageButton;->setSubCaption(Ljava/lang/String;)V

    .line 58
    invoke-virtual {p0, v2}, Lnet/gogame/gowrap/ui/v2017_1/SupportCustomImageButton;->setLevel(I)V
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    .line 60
    invoke-virtual {p1}, Landroid/content/res/TypedArray;->recycle()V

    return-void

    :catchall_0
    move-exception v0

    invoke-virtual {p1}, Landroid/content/res/TypedArray;->recycle()V

    .line 61
    throw v0
.end method
