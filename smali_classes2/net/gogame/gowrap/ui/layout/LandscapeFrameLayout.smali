.class public Lnet/gogame/gowrap/ui/layout/LandscapeFrameLayout;
.super Landroid/widget/FrameLayout;
.source "LandscapeFrameLayout.java"


# instance fields
.field private rotate:Z


# direct methods
.method public constructor <init>(Landroid/content/Context;Landroid/util/AttributeSet;)V
    .locals 0

    .line 14
    invoke-direct {p0, p1, p2}, Landroid/widget/FrameLayout;-><init>(Landroid/content/Context;Landroid/util/AttributeSet;)V

    const/4 p1, 0x0

    .line 11
    iput-boolean p1, p0, Lnet/gogame/gowrap/ui/layout/LandscapeFrameLayout;->rotate:Z

    return-void
.end method


# virtual methods
.method protected onLayout(ZIIII)V
    .locals 8

    .line 42
    iget-boolean v0, p0, Lnet/gogame/gowrap/ui/layout/LandscapeFrameLayout;->rotate:Z

    if-eqz v0, :cond_2

    .line 44
    invoke-virtual {p0}, Lnet/gogame/gowrap/ui/layout/LandscapeFrameLayout;->getChildCount()I

    move-result v0

    const/4 v1, 0x0

    :goto_0
    if-ge v1, v0, :cond_1

    .line 46
    invoke-virtual {p0, v1}, Lnet/gogame/gowrap/ui/layout/LandscapeFrameLayout;->getChildAt(I)Landroid/view/View;

    move-result-object v2

    if-eqz v2, :cond_0

    const/high16 v3, 0x42b40000    # 90.0f

    .line 48
    invoke-virtual {v2, v3}, Landroid/view/View;->setRotation(F)V

    sub-int v3, p4, p2

    sub-int v4, p5, p3

    sub-int v5, v3, v4

    .line 51
    div-int/lit8 v5, v5, 0x2

    int-to-float v5, v5

    invoke-virtual {v2, v5}, Landroid/view/View;->setTranslationX(F)V

    sub-int/2addr v4, v3

    .line 52
    div-int/lit8 v4, v4, 0x2

    int-to-float v3, v4

    invoke-virtual {v2, v3}, Landroid/view/View;->setTranslationY(F)V

    :cond_0
    add-int/lit8 v1, v1, 0x1

    goto :goto_0

    :cond_1
    const/4 v4, 0x0

    const/4 v5, 0x0

    move-object v2, p0

    move v3, p1

    move v6, p5

    move v7, p4

    .line 55
    invoke-super/range {v2 .. v7}, Landroid/widget/FrameLayout;->onLayout(ZIIII)V

    goto :goto_1

    .line 57
    :cond_2
    invoke-super/range {p0 .. p5}, Landroid/widget/FrameLayout;->onLayout(ZIIII)V

    :goto_1
    return-void
.end method

.method protected onMeasure(II)V
    .locals 2

    .line 21
    sget v0, Landroid/os/Build$VERSION;->SDK_INT:I

    const/16 v1, 0x15

    if-lt v0, v1, :cond_0

    .line 22
    invoke-static {p1}, Landroid/view/View$MeasureSpec;->getMode(I)I

    move-result v0

    const/high16 v1, 0x40000000    # 2.0f

    if-ne v0, v1, :cond_0

    .line 23
    invoke-static {p2}, Landroid/view/View$MeasureSpec;->getMode(I)I

    move-result v0

    if-ne v0, v1, :cond_0

    .line 24
    invoke-static {p1}, Landroid/view/View$MeasureSpec;->getSize(I)I

    move-result v0

    invoke-static {p2}, Landroid/view/View$MeasureSpec;->getSize(I)I

    move-result v1

    if-ge v0, v1, :cond_0

    .line 26
    invoke-super {p0, p2, p1}, Landroid/widget/FrameLayout;->onMeasure(II)V

    .line 27
    invoke-static {p1}, Landroid/view/View$MeasureSpec;->getSize(I)I

    move-result p1

    .line 28
    invoke-static {p2}, Landroid/view/View$MeasureSpec;->getSize(I)I

    move-result p2

    .line 27
    invoke-virtual {p0, p1, p2}, Lnet/gogame/gowrap/ui/layout/LandscapeFrameLayout;->setMeasuredDimension(II)V

    const/4 p1, 0x1

    .line 29
    iput-boolean p1, p0, Lnet/gogame/gowrap/ui/layout/LandscapeFrameLayout;->rotate:Z

    goto :goto_0

    .line 32
    :cond_0
    invoke-super {p0, p1, p2}, Landroid/widget/FrameLayout;->onMeasure(II)V

    .line 33
    invoke-static {p1}, Landroid/view/View$MeasureSpec;->getSize(I)I

    move-result p1

    .line 34
    invoke-static {p2}, Landroid/view/View$MeasureSpec;->getSize(I)I

    move-result p2

    .line 33
    invoke-virtual {p0, p1, p2}, Lnet/gogame/gowrap/ui/layout/LandscapeFrameLayout;->setMeasuredDimension(II)V

    const/4 p1, 0x0

    .line 35
    iput-boolean p1, p0, Lnet/gogame/gowrap/ui/layout/LandscapeFrameLayout;->rotate:Z

    :goto_0
    return-void
.end method
