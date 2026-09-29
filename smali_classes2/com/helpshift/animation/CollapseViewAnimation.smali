.class public Lcom/helpshift/animation/CollapseViewAnimation;
.super Landroid/view/animation/Animation;
.source "CollapseViewAnimation.java"


# instance fields
.field private view:Landroid/widget/LinearLayout;

.field private viewHeight:I


# direct methods
.method public constructor <init>(Landroid/widget/LinearLayout;)V
    .locals 0

    .line 17
    invoke-direct {p0}, Landroid/view/animation/Animation;-><init>()V

    .line 18
    iput-object p1, p0, Lcom/helpshift/animation/CollapseViewAnimation;->view:Landroid/widget/LinearLayout;

    .line 19
    invoke-virtual {p1}, Landroid/widget/LinearLayout;->getHeight()I

    move-result p1

    iput p1, p0, Lcom/helpshift/animation/CollapseViewAnimation;->viewHeight:I

    return-void
.end method


# virtual methods
.method protected applyTransformation(FLandroid/view/animation/Transformation;)V
    .locals 1

    .line 28
    iget p2, p0, Lcom/helpshift/animation/CollapseViewAnimation;->viewHeight:I

    int-to-float p2, p2

    const/high16 v0, 0x3f800000    # 1.0f

    sub-float/2addr v0, p1

    mul-float p2, p2, v0

    float-to-int p1, p2

    .line 29
    iget-object p2, p0, Lcom/helpshift/animation/CollapseViewAnimation;->view:Landroid/widget/LinearLayout;

    invoke-virtual {p2}, Landroid/widget/LinearLayout;->getLayoutParams()Landroid/view/ViewGroup$LayoutParams;

    move-result-object p2

    iput p1, p2, Landroid/view/ViewGroup$LayoutParams;->height:I

    .line 30
    iget-object p1, p0, Lcom/helpshift/animation/CollapseViewAnimation;->view:Landroid/widget/LinearLayout;

    invoke-virtual {p1}, Landroid/widget/LinearLayout;->requestLayout()V

    return-void
.end method

.method public willChangeBounds()Z
    .locals 1

    const/4 v0, 0x1

    return v0
.end method
