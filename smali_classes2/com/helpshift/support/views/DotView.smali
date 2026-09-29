.class public Lcom/helpshift/support/views/DotView;
.super Landroid/view/View;
.source "DotView.java"

# interfaces
.implements Landroid/animation/ValueAnimator$AnimatorUpdateListener;


# instance fields
.field private centerX:F

.field private centerY:F

.field private dotColor:I

.field private ovalRectF:Landroid/graphics/RectF;

.field private paint:Landroid/graphics/Paint;

.field private radius:F


# direct methods
.method public constructor <init>(Landroid/content/Context;I)V
    .locals 0

    .line 24
    invoke-direct {p0, p1}, Landroid/view/View;-><init>(Landroid/content/Context;)V

    const/high16 p1, -0x40800000    # -1.0f

    .line 19
    iput p1, p0, Lcom/helpshift/support/views/DotView;->centerX:F

    iput p1, p0, Lcom/helpshift/support/views/DotView;->centerY:F

    .line 25
    iput p2, p0, Lcom/helpshift/support/views/DotView;->dotColor:I

    .line 26
    invoke-direct {p0}, Lcom/helpshift/support/views/DotView;->setup()V

    return-void
.end method

.method public constructor <init>(Landroid/content/Context;Landroid/util/AttributeSet;)V
    .locals 1
    .param p2    # Landroid/util/AttributeSet;
        .annotation build Landroidx/annotation/Nullable;
        .end annotation
    .end param

    const/4 v0, 0x0

    .line 30
    invoke-direct {p0, p1, p2, v0}, Lcom/helpshift/support/views/DotView;-><init>(Landroid/content/Context;Landroid/util/AttributeSet;I)V

    return-void
.end method

.method public constructor <init>(Landroid/content/Context;Landroid/util/AttributeSet;I)V
    .locals 0
    .param p2    # Landroid/util/AttributeSet;
        .annotation build Landroidx/annotation/Nullable;
        .end annotation
    .end param

    .line 34
    invoke-direct {p0, p1, p2, p3}, Landroid/view/View;-><init>(Landroid/content/Context;Landroid/util/AttributeSet;I)V

    const/high16 p1, -0x40800000    # -1.0f

    .line 19
    iput p1, p0, Lcom/helpshift/support/views/DotView;->centerX:F

    iput p1, p0, Lcom/helpshift/support/views/DotView;->centerY:F

    return-void
.end method

.method private setup()V
    .locals 2

    .line 64
    new-instance v0, Landroid/graphics/RectF;

    invoke-direct {v0}, Landroid/graphics/RectF;-><init>()V

    iput-object v0, p0, Lcom/helpshift/support/views/DotView;->ovalRectF:Landroid/graphics/RectF;

    .line 65
    new-instance v0, Landroid/graphics/Paint;

    invoke-direct {v0}, Landroid/graphics/Paint;-><init>()V

    iput-object v0, p0, Lcom/helpshift/support/views/DotView;->paint:Landroid/graphics/Paint;

    .line 66
    iget-object v0, p0, Lcom/helpshift/support/views/DotView;->paint:Landroid/graphics/Paint;

    const/4 v1, 0x1

    invoke-virtual {v0, v1}, Landroid/graphics/Paint;->setAntiAlias(Z)V

    .line 67
    iget-object v0, p0, Lcom/helpshift/support/views/DotView;->paint:Landroid/graphics/Paint;

    iget v1, p0, Lcom/helpshift/support/views/DotView;->dotColor:I

    invoke-virtual {v0, v1}, Landroid/graphics/Paint;->setColor(I)V

    return-void
.end method

.method private updateOvalRectF()V
    .locals 3

    .line 57
    iget-object v0, p0, Lcom/helpshift/support/views/DotView;->ovalRectF:Landroid/graphics/RectF;

    iget v1, p0, Lcom/helpshift/support/views/DotView;->centerX:F

    iget v2, p0, Lcom/helpshift/support/views/DotView;->radius:F

    sub-float/2addr v1, v2

    iput v1, v0, Landroid/graphics/RectF;->left:F

    .line 58
    iget-object v0, p0, Lcom/helpshift/support/views/DotView;->ovalRectF:Landroid/graphics/RectF;

    iget v1, p0, Lcom/helpshift/support/views/DotView;->centerX:F

    iget v2, p0, Lcom/helpshift/support/views/DotView;->radius:F

    add-float/2addr v1, v2

    iput v1, v0, Landroid/graphics/RectF;->right:F

    .line 59
    iget-object v0, p0, Lcom/helpshift/support/views/DotView;->ovalRectF:Landroid/graphics/RectF;

    iget v1, p0, Lcom/helpshift/support/views/DotView;->centerY:F

    iget v2, p0, Lcom/helpshift/support/views/DotView;->radius:F

    sub-float/2addr v1, v2

    iput v1, v0, Landroid/graphics/RectF;->top:F

    .line 60
    iget-object v0, p0, Lcom/helpshift/support/views/DotView;->ovalRectF:Landroid/graphics/RectF;

    iget v1, p0, Lcom/helpshift/support/views/DotView;->centerY:F

    iget v2, p0, Lcom/helpshift/support/views/DotView;->radius:F

    add-float/2addr v1, v2

    iput v1, v0, Landroid/graphics/RectF;->bottom:F

    return-void
.end method


# virtual methods
.method public onAnimationUpdate(Landroid/animation/ValueAnimator;)V
    .locals 3

    .line 72
    invoke-virtual {p1}, Landroid/animation/ValueAnimator;->getAnimatedValue()Ljava/lang/Object;

    move-result-object p1

    check-cast p1, Ljava/lang/Integer;

    invoke-virtual {p1}, Ljava/lang/Integer;->intValue()I

    move-result p1

    iget v0, p0, Lcom/helpshift/support/views/DotView;->dotColor:I

    .line 73
    invoke-static {v0}, Landroid/graphics/Color;->red(I)I

    move-result v0

    iget v1, p0, Lcom/helpshift/support/views/DotView;->dotColor:I

    .line 74
    invoke-static {v1}, Landroid/graphics/Color;->green(I)I

    move-result v1

    iget v2, p0, Lcom/helpshift/support/views/DotView;->dotColor:I

    .line 75
    invoke-static {v2}, Landroid/graphics/Color;->blue(I)I

    move-result v2

    .line 72
    invoke-static {p1, v0, v1, v2}, Landroid/graphics/Color;->argb(IIII)I

    move-result p1

    iput p1, p0, Lcom/helpshift/support/views/DotView;->dotColor:I

    .line 76
    iget-object p1, p0, Lcom/helpshift/support/views/DotView;->paint:Landroid/graphics/Paint;

    iget v0, p0, Lcom/helpshift/support/views/DotView;->dotColor:I

    invoke-virtual {p1, v0}, Landroid/graphics/Paint;->setColor(I)V

    .line 77
    invoke-virtual {p0}, Lcom/helpshift/support/views/DotView;->invalidate()V

    return-void
.end method

.method protected onDraw(Landroid/graphics/Canvas;)V
    .locals 2

    .line 44
    iget-object v0, p0, Lcom/helpshift/support/views/DotView;->ovalRectF:Landroid/graphics/RectF;

    iget-object v1, p0, Lcom/helpshift/support/views/DotView;->paint:Landroid/graphics/Paint;

    invoke-virtual {p1, v0, v1}, Landroid/graphics/Canvas;->drawOval(Landroid/graphics/RectF;Landroid/graphics/Paint;)V

    return-void
.end method

.method protected onLayout(ZIIII)V
    .locals 0

    .line 49
    invoke-super/range {p0 .. p5}, Landroid/view/View;->onLayout(ZIIII)V

    .line 50
    invoke-virtual {p0}, Lcom/helpshift/support/views/DotView;->getWidth()I

    move-result p1

    div-int/lit8 p1, p1, 0x2

    int-to-float p1, p1

    iput p1, p0, Lcom/helpshift/support/views/DotView;->centerX:F

    .line 51
    invoke-virtual {p0}, Lcom/helpshift/support/views/DotView;->getHeight()I

    move-result p1

    div-int/lit8 p1, p1, 0x2

    int-to-float p1, p1

    iput p1, p0, Lcom/helpshift/support/views/DotView;->centerY:F

    .line 52
    iget p1, p0, Lcom/helpshift/support/views/DotView;->centerX:F

    iget p2, p0, Lcom/helpshift/support/views/DotView;->centerY:F

    invoke-static {p1, p2}, Ljava/lang/Math;->min(FF)F

    move-result p1

    iput p1, p0, Lcom/helpshift/support/views/DotView;->radius:F

    .line 53
    invoke-direct {p0}, Lcom/helpshift/support/views/DotView;->updateOvalRectF()V

    return-void
.end method

.method public setDotColor(I)V
    .locals 0

    .line 38
    iput p1, p0, Lcom/helpshift/support/views/DotView;->dotColor:I

    .line 39
    invoke-virtual {p0}, Lcom/helpshift/support/views/DotView;->invalidate()V

    return-void
.end method
