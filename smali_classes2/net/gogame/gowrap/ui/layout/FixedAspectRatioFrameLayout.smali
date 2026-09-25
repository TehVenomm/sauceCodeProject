.class public Lnet/gogame/gowrap/ui/layout/FixedAspectRatioFrameLayout;
.super Landroid/widget/FrameLayout;
.source "FixedAspectRatioFrameLayout.java"


# instance fields
.field private mAspectRatioHeight:I

.field private mAspectRatioWidth:I

.field private maxHeight:F


# direct methods
.method public constructor <init>(Landroid/content/Context;)V
    .locals 0

    .line 17
    invoke-direct {p0, p1}, Landroid/widget/FrameLayout;-><init>(Landroid/content/Context;)V

    return-void
.end method

.method public constructor <init>(Landroid/content/Context;Landroid/util/AttributeSet;)V
    .locals 0

    .line 21
    invoke-direct {p0, p1, p2}, Landroid/widget/FrameLayout;-><init>(Landroid/content/Context;Landroid/util/AttributeSet;)V

    .line 23
    invoke-direct {p0, p1, p2}, Lnet/gogame/gowrap/ui/layout/FixedAspectRatioFrameLayout;->init(Landroid/content/Context;Landroid/util/AttributeSet;)V

    return-void
.end method

.method public constructor <init>(Landroid/content/Context;Landroid/util/AttributeSet;I)V
    .locals 0

    .line 27
    invoke-direct {p0, p1, p2, p3}, Landroid/widget/FrameLayout;-><init>(Landroid/content/Context;Landroid/util/AttributeSet;I)V

    .line 29
    invoke-direct {p0, p1, p2}, Lnet/gogame/gowrap/ui/layout/FixedAspectRatioFrameLayout;->init(Landroid/content/Context;Landroid/util/AttributeSet;)V

    return-void
.end method

.method private init(Landroid/content/Context;Landroid/util/AttributeSet;)V
    .locals 1

    .line 33
    sget-object v0, Lnet/gogame/gowrap/ui/common/R$styleable;->FixedAspectRatioFrameLayout:[I

    invoke-virtual {p1, p2, v0}, Landroid/content/Context;->obtainStyledAttributes(Landroid/util/AttributeSet;[I)Landroid/content/res/TypedArray;

    move-result-object p1

    .line 36
    sget p2, Lnet/gogame/gowrap/ui/common/R$styleable;->FixedAspectRatioFrameLayout_aspectRatioWidth:I

    const/4 v0, 0x4

    invoke-virtual {p1, p2, v0}, Landroid/content/res/TypedArray;->getInt(II)I

    move-result p2

    iput p2, p0, Lnet/gogame/gowrap/ui/layout/FixedAspectRatioFrameLayout;->mAspectRatioWidth:I

    .line 38
    sget p2, Lnet/gogame/gowrap/ui/common/R$styleable;->FixedAspectRatioFrameLayout_aspectRatioHeight:I

    const/4 v0, 0x3

    invoke-virtual {p1, p2, v0}, Landroid/content/res/TypedArray;->getInt(II)I

    move-result p2

    iput p2, p0, Lnet/gogame/gowrap/ui/layout/FixedAspectRatioFrameLayout;->mAspectRatioHeight:I

    .line 40
    sget p2, Lnet/gogame/gowrap/ui/common/R$styleable;->FixedAspectRatioFrameLayout_maxHeight:I

    const v0, 0x7f7fffff    # Float.MAX_VALUE

    invoke-virtual {p1, p2, v0}, Landroid/content/res/TypedArray;->getDimension(IF)F

    move-result p2

    iput p2, p0, Lnet/gogame/gowrap/ui/layout/FixedAspectRatioFrameLayout;->maxHeight:F

    .line 43
    invoke-virtual {p1}, Landroid/content/res/TypedArray;->recycle()V

    return-void
.end method


# virtual methods
.method protected onMeasure(II)V
    .locals 2

    .line 48
    invoke-static {p1}, Landroid/view/View$MeasureSpec;->getSize(I)I

    move-result p1

    .line 50
    invoke-static {p2}, Landroid/view/View$MeasureSpec;->getSize(I)I

    move-result p2

    .line 52
    iget v0, p0, Lnet/gogame/gowrap/ui/layout/FixedAspectRatioFrameLayout;->mAspectRatioHeight:I

    mul-int v0, v0, p1

    iget v1, p0, Lnet/gogame/gowrap/ui/layout/FixedAspectRatioFrameLayout;->mAspectRatioWidth:I

    div-int/2addr v0, v1

    if-le v0, p2, :cond_0

    .line 57
    iget p1, p0, Lnet/gogame/gowrap/ui/layout/FixedAspectRatioFrameLayout;->mAspectRatioWidth:I

    mul-int p1, p1, p2

    iget v0, p0, Lnet/gogame/gowrap/ui/layout/FixedAspectRatioFrameLayout;->mAspectRatioHeight:I

    div-int/2addr p1, v0

    goto :goto_0

    :cond_0
    move p2, v0

    :goto_0
    int-to-float v0, p2

    .line 64
    iget v1, p0, Lnet/gogame/gowrap/ui/layout/FixedAspectRatioFrameLayout;->maxHeight:F

    cmpl-float v0, v0, v1

    if-lez v0, :cond_1

    .line 65
    iget p1, p0, Lnet/gogame/gowrap/ui/layout/FixedAspectRatioFrameLayout;->maxHeight:F

    float-to-int p2, p1

    .line 66
    iget p1, p0, Lnet/gogame/gowrap/ui/layout/FixedAspectRatioFrameLayout;->mAspectRatioWidth:I

    mul-int p1, p1, p2

    iget v0, p0, Lnet/gogame/gowrap/ui/layout/FixedAspectRatioFrameLayout;->mAspectRatioHeight:I

    div-int/2addr p1, v0

    :cond_1
    const/high16 v0, 0x40000000    # 2.0f

    .line 69
    invoke-static {p1, v0}, Landroid/view/View$MeasureSpec;->makeMeasureSpec(II)I

    move-result p1

    .line 70
    invoke-static {p2, v0}, Landroid/view/View$MeasureSpec;->makeMeasureSpec(II)I

    move-result p2

    .line 69
    invoke-super {p0, p1, p2}, Landroid/widget/FrameLayout;->onMeasure(II)V

    return-void
.end method
