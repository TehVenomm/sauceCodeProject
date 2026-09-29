.class public Lnet/gogame/gowrap/ui/view/ScalingImageView;
.super Landroid/widget/ImageView;
.source "ScalingImageView.java"


# direct methods
.method public constructor <init>(Landroid/content/Context;)V
    .locals 0

    .line 11
    invoke-direct {p0, p1}, Landroid/widget/ImageView;-><init>(Landroid/content/Context;)V

    return-void
.end method

.method public constructor <init>(Landroid/content/Context;Landroid/util/AttributeSet;)V
    .locals 0

    .line 15
    invoke-direct {p0, p1, p2}, Landroid/widget/ImageView;-><init>(Landroid/content/Context;Landroid/util/AttributeSet;)V

    return-void
.end method

.method public constructor <init>(Landroid/content/Context;Landroid/util/AttributeSet;I)V
    .locals 0

    .line 19
    invoke-direct {p0, p1, p2, p3}, Landroid/widget/ImageView;-><init>(Landroid/content/Context;Landroid/util/AttributeSet;I)V

    return-void
.end method


# virtual methods
.method protected onMeasure(II)V
    .locals 7

    .line 24
    invoke-virtual {p0}, Lnet/gogame/gowrap/ui/view/ScalingImageView;->getDrawable()Landroid/graphics/drawable/Drawable;

    move-result-object v0

    if-eqz v0, :cond_5

    .line 26
    invoke-virtual {v0}, Landroid/graphics/drawable/Drawable;->getIntrinsicWidth()I

    move-result v1

    .line 27
    invoke-virtual {v0}, Landroid/graphics/drawable/Drawable;->getIntrinsicHeight()I

    move-result v0

    int-to-float v2, v1

    int-to-float v0, v0

    div-float/2addr v2, v0

    .line 30
    invoke-static {p1}, Landroid/view/View$MeasureSpec;->getMode(I)I

    move-result v0

    .line 31
    invoke-static {p1}, Landroid/view/View$MeasureSpec;->getSize(I)I

    move-result p1

    .line 32
    invoke-virtual {p0}, Lnet/gogame/gowrap/ui/view/ScalingImageView;->getPaddingLeft()I

    move-result v3

    sub-int/2addr p1, v3

    invoke-virtual {p0}, Lnet/gogame/gowrap/ui/view/ScalingImageView;->getPaddingRight()I

    move-result v3

    sub-int/2addr p1, v3

    .line 34
    invoke-static {p2}, Landroid/view/View$MeasureSpec;->getMode(I)I

    move-result v3

    .line 35
    invoke-static {p2}, Landroid/view/View$MeasureSpec;->getSize(I)I

    move-result p2

    .line 36
    invoke-virtual {p0}, Lnet/gogame/gowrap/ui/view/ScalingImageView;->getPaddingTop()I

    move-result v4

    sub-int v4, p2, v4

    invoke-virtual {p0}, Lnet/gogame/gowrap/ui/view/ScalingImageView;->getPaddingBottom()I

    move-result v5

    sub-int/2addr v4, v5

    const/high16 v5, 0x40000000    # 2.0f

    const/high16 v6, -0x80000000

    if-eq v0, v6, :cond_1

    if-eq v0, v5, :cond_0

    goto :goto_0

    :cond_0
    move v1, p1

    goto :goto_0

    .line 41
    :cond_1
    invoke-static {v1, p1}, Ljava/lang/Math;->min(II)I

    move-result v1

    :goto_0
    int-to-float p1, v1

    div-float/2addr p1, v2

    float-to-int p1, p1

    if-eq v3, v6, :cond_3

    if-eq v3, v5, :cond_2

    goto :goto_1

    :cond_2
    int-to-float p1, v4

    mul-float p1, p1, v2

    float-to-int v1, p1

    goto :goto_2

    :cond_3
    if-lez p2, :cond_4

    .line 56
    invoke-static {p1, v4}, Ljava/lang/Math;->min(II)I

    move-result v4

    int-to-float p1, v4

    mul-float p1, p1, v2

    float-to-int v1, p1

    goto :goto_2

    :cond_4
    :goto_1
    move v4, p1

    .line 69
    :goto_2
    invoke-virtual {p0}, Lnet/gogame/gowrap/ui/view/ScalingImageView;->getPaddingLeft()I

    move-result p1

    add-int/2addr v1, p1

    invoke-virtual {p0}, Lnet/gogame/gowrap/ui/view/ScalingImageView;->getPaddingRight()I

    move-result p1

    add-int/2addr v1, p1

    .line 70
    invoke-virtual {p0}, Lnet/gogame/gowrap/ui/view/ScalingImageView;->getPaddingTop()I

    move-result p1

    add-int/2addr v4, p1

    invoke-virtual {p0}, Lnet/gogame/gowrap/ui/view/ScalingImageView;->getPaddingBottom()I

    move-result p1

    add-int/2addr v4, p1

    .line 72
    invoke-static {v1, v6}, Landroid/view/View$MeasureSpec;->makeMeasureSpec(II)I

    move-result p1

    .line 73
    invoke-static {v4, v6}, Landroid/view/View$MeasureSpec;->makeMeasureSpec(II)I

    move-result p2

    .line 72
    invoke-super {p0, p1, p2}, Landroid/widget/ImageView;->onMeasure(II)V

    goto :goto_3

    .line 75
    :cond_5
    invoke-super {p0, p1, p2}, Landroid/widget/ImageView;->onMeasure(II)V

    :goto_3
    return-void
.end method
