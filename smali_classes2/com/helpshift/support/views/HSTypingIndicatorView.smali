.class public Lcom/helpshift/support/views/HSTypingIndicatorView;
.super Landroid/widget/LinearLayout;
.source "HSTypingIndicatorView.java"


# static fields
.field private static final ALPHA_DARK:I = 0xb3

.field private static final ALPHA_LIGHT:I = 0x4c


# instance fields
.field private final ANIMATION_DURATION:J

.field private final LOOP_START_DELAY:J

.field private LightDotColor:I

.field dotAnimatorSet:Landroid/animation/AnimatorSet;

.field dotAnimators:[Landroid/animation/Animator;

.field private dotDiameter:F

.field private dots:[Lcom/helpshift/support/views/DotView;

.field private interDotPadding:F


# direct methods
.method public constructor <init>(Landroid/content/Context;Landroid/util/AttributeSet;)V
    .locals 1
    .param p2    # Landroid/util/AttributeSet;
        .annotation build Landroidx/annotation/Nullable;
        .end annotation
    .end param

    const/4 v0, 0x0

    .line 30
    invoke-direct {p0, p1, p2, v0}, Lcom/helpshift/support/views/HSTypingIndicatorView;-><init>(Landroid/content/Context;Landroid/util/AttributeSet;I)V

    return-void
.end method

.method public constructor <init>(Landroid/content/Context;Landroid/util/AttributeSet;I)V
    .locals 2
    .param p2    # Landroid/util/AttributeSet;
        .annotation build Landroidx/annotation/Nullable;
        .end annotation
    .end param

    .line 34
    invoke-direct {p0, p1, p2, p3}, Landroid/widget/LinearLayout;-><init>(Landroid/content/Context;Landroid/util/AttributeSet;I)V

    const-wide/16 v0, 0x384

    .line 19
    iput-wide v0, p0, Lcom/helpshift/support/views/HSTypingIndicatorView;->ANIMATION_DURATION:J

    const-wide/16 v0, 0x1c2

    .line 20
    iput-wide v0, p0, Lcom/helpshift/support/views/HSTypingIndicatorView;->LOOP_START_DELAY:J

    const/4 p3, 0x3

    .line 22
    new-array p3, p3, [Landroid/animation/Animator;

    iput-object p3, p0, Lcom/helpshift/support/views/HSTypingIndicatorView;->dotAnimators:[Landroid/animation/Animator;

    .line 35
    invoke-direct {p0, p1, p2}, Lcom/helpshift/support/views/HSTypingIndicatorView;->initAttributes(Landroid/content/Context;Landroid/util/AttributeSet;)V

    .line 36
    invoke-direct {p0}, Lcom/helpshift/support/views/HSTypingIndicatorView;->setup()V

    return-void
.end method

.method private initAttributes(Landroid/content/Context;Landroid/util/AttributeSet;)V
    .locals 3

    .line 40
    invoke-virtual {p1}, Landroid/content/Context;->getTheme()Landroid/content/res/Resources$Theme;

    move-result-object p1

    sget-object v0, Lcom/helpshift/R$styleable;->HSTypingIndicatorView:[I

    const/4 v1, 0x0

    invoke-virtual {p1, p2, v0, v1, v1}, Landroid/content/res/Resources$Theme;->obtainStyledAttributes(Landroid/util/AttributeSet;[III)Landroid/content/res/TypedArray;

    move-result-object p1

    .line 44
    sget p2, Lcom/helpshift/R$styleable;->HSTypingIndicatorView_hs__dotColor:I

    invoke-virtual {p1, p2, v1}, Landroid/content/res/TypedArray;->getColor(II)I

    move-result p2

    .line 46
    invoke-static {p2}, Landroid/graphics/Color;->red(I)I

    move-result v0

    .line 47
    invoke-static {p2}, Landroid/graphics/Color;->green(I)I

    move-result v1

    .line 48
    invoke-static {p2}, Landroid/graphics/Color;->blue(I)I

    move-result p2

    const/16 v2, 0x4c

    .line 45
    invoke-static {v2, v0, v1, p2}, Landroid/graphics/Color;->argb(IIII)I

    move-result p2

    iput p2, p0, Lcom/helpshift/support/views/HSTypingIndicatorView;->LightDotColor:I

    .line 50
    sget p2, Lcom/helpshift/R$styleable;->HSTypingIndicatorView_hs__interDotPadding:I

    const/4 v0, 0x0

    invoke-virtual {p1, p2, v0}, Landroid/content/res/TypedArray;->getDimension(IF)F

    move-result p2

    iput p2, p0, Lcom/helpshift/support/views/HSTypingIndicatorView;->interDotPadding:F

    .line 51
    sget p2, Lcom/helpshift/R$styleable;->HSTypingIndicatorView_hs__dotDiameter:I

    invoke-virtual {p1, p2, v0}, Landroid/content/res/TypedArray;->getDimension(IF)F

    move-result p2

    iput p2, p0, Lcom/helpshift/support/views/HSTypingIndicatorView;->dotDiameter:F

    .line 53
    invoke-virtual {p1}, Landroid/content/res/TypedArray;->recycle()V

    return-void
.end method

.method private setup()V
    .locals 10

    const/4 v0, 0x3

    .line 116
    new-array v1, v0, [Lcom/helpshift/support/views/DotView;

    iput-object v1, p0, Lcom/helpshift/support/views/HSTypingIndicatorView;->dots:[Lcom/helpshift/support/views/DotView;

    const/4 v1, 0x0

    const/4 v2, 0x0

    :goto_0
    if-ge v2, v0, :cond_0

    .line 118
    iget-object v3, p0, Lcom/helpshift/support/views/HSTypingIndicatorView;->dots:[Lcom/helpshift/support/views/DotView;

    new-instance v4, Lcom/helpshift/support/views/DotView;

    invoke-virtual {p0}, Lcom/helpshift/support/views/HSTypingIndicatorView;->getContext()Landroid/content/Context;

    move-result-object v5

    iget v6, p0, Lcom/helpshift/support/views/HSTypingIndicatorView;->LightDotColor:I

    invoke-direct {v4, v5, v6}, Lcom/helpshift/support/views/DotView;-><init>(Landroid/content/Context;I)V

    aput-object v4, v3, v2

    .line 120
    iget v3, p0, Lcom/helpshift/support/views/HSTypingIndicatorView;->interDotPadding:F

    const/high16 v4, 0x40000000    # 2.0f

    div-float/2addr v3, v4

    .line 121
    iget v5, p0, Lcom/helpshift/support/views/HSTypingIndicatorView;->interDotPadding:F

    div-float/2addr v5, v4

    const-wide/16 v6, 0x0

    const/4 v4, 0x0

    packed-switch v2, :pswitch_data_0

    goto :goto_1

    :pswitch_0
    const-wide/16 v6, 0x1c2

    const/4 v5, 0x0

    goto :goto_1

    :pswitch_1
    const-wide/16 v6, 0xe1

    goto :goto_1

    :pswitch_2
    const/4 v3, 0x0

    .line 137
    :goto_1
    new-instance v4, Landroid/widget/LinearLayout$LayoutParams;

    iget v8, p0, Lcom/helpshift/support/views/HSTypingIndicatorView;->dotDiameter:F

    float-to-int v8, v8

    iget v9, p0, Lcom/helpshift/support/views/HSTypingIndicatorView;->dotDiameter:F

    float-to-int v9, v9

    invoke-direct {v4, v8, v9}, Landroid/widget/LinearLayout$LayoutParams;-><init>(II)V

    float-to-int v3, v3

    float-to-int v5, v5

    .line 139
    invoke-virtual {v4, v3, v1, v5, v1}, Landroid/widget/LinearLayout$LayoutParams;->setMargins(IIII)V

    .line 140
    iget-object v3, p0, Lcom/helpshift/support/views/HSTypingIndicatorView;->dots:[Lcom/helpshift/support/views/DotView;

    aget-object v3, v3, v2

    invoke-virtual {p0, v3, v4}, Lcom/helpshift/support/views/HSTypingIndicatorView;->addView(Landroid/view/View;Landroid/view/ViewGroup$LayoutParams;)V

    .line 142
    iget-object v3, p0, Lcom/helpshift/support/views/HSTypingIndicatorView;->dotAnimators:[Landroid/animation/Animator;

    iget-object v4, p0, Lcom/helpshift/support/views/HSTypingIndicatorView;->dots:[Lcom/helpshift/support/views/DotView;

    aget-object v4, v4, v2

    invoke-virtual {p0, v6, v7, v4}, Lcom/helpshift/support/views/HSTypingIndicatorView;->getAnimator(JLandroid/animation/ValueAnimator$AnimatorUpdateListener;)Landroid/animation/ValueAnimator;

    move-result-object v4

    aput-object v4, v3, v2

    add-int/lit8 v2, v2, 0x1

    goto :goto_0

    :cond_0
    return-void

    :pswitch_data_0
    .packed-switch 0x0
        :pswitch_2
        :pswitch_1
        :pswitch_0
    .end packed-switch
.end method

.method private startTypingAnimation()V
    .locals 2

    .line 68
    iget-object v0, p0, Lcom/helpshift/support/views/HSTypingIndicatorView;->dotAnimatorSet:Landroid/animation/AnimatorSet;

    if-nez v0, :cond_0

    .line 69
    new-instance v0, Landroid/animation/AnimatorSet;

    invoke-direct {v0}, Landroid/animation/AnimatorSet;-><init>()V

    iput-object v0, p0, Lcom/helpshift/support/views/HSTypingIndicatorView;->dotAnimatorSet:Landroid/animation/AnimatorSet;

    .line 70
    iget-object v0, p0, Lcom/helpshift/support/views/HSTypingIndicatorView;->dotAnimatorSet:Landroid/animation/AnimatorSet;

    iget-object v1, p0, Lcom/helpshift/support/views/HSTypingIndicatorView;->dotAnimators:[Landroid/animation/Animator;

    invoke-virtual {v0, v1}, Landroid/animation/AnimatorSet;->playTogether([Landroid/animation/Animator;)V

    .line 71
    iget-object v0, p0, Lcom/helpshift/support/views/HSTypingIndicatorView;->dotAnimatorSet:Landroid/animation/AnimatorSet;

    new-instance v1, Lcom/helpshift/support/views/HSTypingIndicatorView$1;

    invoke-direct {v1, p0}, Lcom/helpshift/support/views/HSTypingIndicatorView$1;-><init>(Lcom/helpshift/support/views/HSTypingIndicatorView;)V

    invoke-virtual {v0, v1}, Landroid/animation/AnimatorSet;->addListener(Landroid/animation/Animator$AnimatorListener;)V

    .line 93
    iget-object v0, p0, Lcom/helpshift/support/views/HSTypingIndicatorView;->dotAnimatorSet:Landroid/animation/AnimatorSet;

    invoke-virtual {v0}, Landroid/animation/AnimatorSet;->start()V

    :cond_0
    return-void
.end method

.method private stopTypingAnimation()V
    .locals 5

    .line 98
    iget-object v0, p0, Lcom/helpshift/support/views/HSTypingIndicatorView;->dotAnimatorSet:Landroid/animation/AnimatorSet;

    if-eqz v0, :cond_1

    .line 99
    iget-object v0, p0, Lcom/helpshift/support/views/HSTypingIndicatorView;->dotAnimatorSet:Landroid/animation/AnimatorSet;

    invoke-virtual {v0}, Landroid/animation/AnimatorSet;->getChildAnimations()Ljava/util/ArrayList;

    move-result-object v0

    invoke-virtual {v0}, Ljava/util/ArrayList;->iterator()Ljava/util/Iterator;

    move-result-object v0

    :goto_0
    invoke-interface {v0}, Ljava/util/Iterator;->hasNext()Z

    move-result v1

    if-eqz v1, :cond_0

    invoke-interface {v0}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Landroid/animation/Animator;

    .line 100
    invoke-virtual {v1}, Landroid/animation/Animator;->cancel()V

    goto :goto_0

    .line 102
    :cond_0
    iget-object v0, p0, Lcom/helpshift/support/views/HSTypingIndicatorView;->dotAnimatorSet:Landroid/animation/AnimatorSet;

    invoke-virtual {v0}, Landroid/animation/AnimatorSet;->cancel()V

    .line 106
    iget-object v0, p0, Lcom/helpshift/support/views/HSTypingIndicatorView;->dotAnimatorSet:Landroid/animation/AnimatorSet;

    invoke-virtual {v0}, Landroid/animation/AnimatorSet;->removeAllListeners()V

    const/4 v0, 0x0

    .line 107
    iput-object v0, p0, Lcom/helpshift/support/views/HSTypingIndicatorView;->dotAnimatorSet:Landroid/animation/AnimatorSet;

    .line 109
    iget-object v0, p0, Lcom/helpshift/support/views/HSTypingIndicatorView;->dots:[Lcom/helpshift/support/views/DotView;

    array-length v1, v0

    const/4 v2, 0x0

    :goto_1
    if-ge v2, v1, :cond_1

    aget-object v3, v0, v2

    .line 110
    iget v4, p0, Lcom/helpshift/support/views/HSTypingIndicatorView;->LightDotColor:I

    invoke-virtual {v3, v4}, Lcom/helpshift/support/views/DotView;->setDotColor(I)V

    add-int/lit8 v2, v2, 0x1

    goto :goto_1

    :cond_1
    return-void
.end method


# virtual methods
.method public getAnimator(JLandroid/animation/ValueAnimator$AnimatorUpdateListener;)Landroid/animation/ValueAnimator;
    .locals 1

    const/4 v0, 0x3

    .line 147
    new-array v0, v0, [I

    fill-array-data v0, :array_0

    invoke-static {v0}, Landroid/animation/ValueAnimator;->ofInt([I)Landroid/animation/ValueAnimator;

    move-result-object v0

    .line 150
    invoke-virtual {v0, p1, p2}, Landroid/animation/ValueAnimator;->setStartDelay(J)V

    const-wide/16 p1, 0x384

    .line 151
    invoke-virtual {v0, p1, p2}, Landroid/animation/ValueAnimator;->setDuration(J)Landroid/animation/ValueAnimator;

    .line 152
    new-instance p1, Landroid/view/animation/LinearInterpolator;

    invoke-direct {p1}, Landroid/view/animation/LinearInterpolator;-><init>()V

    invoke-virtual {v0, p1}, Landroid/animation/ValueAnimator;->setInterpolator(Landroid/animation/TimeInterpolator;)V

    .line 153
    invoke-virtual {v0, p3}, Landroid/animation/ValueAnimator;->addUpdateListener(Landroid/animation/ValueAnimator$AnimatorUpdateListener;)V

    return-object v0

    :array_0
    .array-data 4
        0x4c
        0xb3
        0x4c
    .end array-data
.end method

.method protected onWindowVisibilityChanged(I)V
    .locals 0

    .line 58
    invoke-super {p0, p1}, Landroid/widget/LinearLayout;->onWindowVisibilityChanged(I)V

    if-nez p1, :cond_0

    .line 60
    invoke-direct {p0}, Lcom/helpshift/support/views/HSTypingIndicatorView;->startTypingAnimation()V

    goto :goto_0

    .line 63
    :cond_0
    invoke-direct {p0}, Lcom/helpshift/support/views/HSTypingIndicatorView;->stopTypingAnimation()V

    :goto_0
    return-void
.end method
