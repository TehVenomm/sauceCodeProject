.class public Lnet/gogame/gowrap/ui/fab/PopupWindowFab;
.super Lnet/gogame/gowrap/ui/fab/AbstractFab;
.source "PopupWindowFab.java"


# static fields
.field private static final MAX_RETRIES:I = 0x4

.field private static final RETRY_MS:I = 0x1f4


# instance fields
.field private animatorSet:Landroid/animation/AnimatorSet;

.field private fabEndPositionX:I

.field private fabStartPositionX:I

.field private fixX:Z

.field private fixY:Z

.field private handler:Landroid/os/Handler;

.field private isFabLeft:Z

.field private isFirstTime:Z

.field private mPosX:Ljava/lang/Integer;

.field private mPosY:Ljava/lang/Integer;

.field private offsetX:I

.field private offsetY:I

.field private popupWindow:Landroid/widget/PopupWindow;

.field private retries:I

.field private screenBottomHeightLimit:I

.field private screenHeight:I

.field private screenTopHeightLimit:I

.field private screenWidth:I

.field private final self:Lnet/gogame/gowrap/ui/fab/PopupWindowFab;

.field private slideIn:Z

.field private slideOut:Z

.field private timer:Ljava/util/Timer;

.field private timerTask:Ljava/util/TimerTask;

.field private final updateFab:Ljava/lang/Runnable;

.field private final updatePopupWindowRunnable:Ljava/lang/Runnable;

.field private view:Landroid/view/View;


# direct methods
.method public constructor <init>(ZZ)V
    .locals 2

    .line 100
    invoke-direct {p0}, Lnet/gogame/gowrap/ui/fab/AbstractFab;-><init>()V

    .line 40
    iput-object p0, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->self:Lnet/gogame/gowrap/ui/fab/PopupWindowFab;

    const/4 v0, 0x0

    .line 45
    iput-object v0, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->popupWindow:Landroid/widget/PopupWindow;

    .line 46
    iput-object v0, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->view:Landroid/view/View;

    .line 47
    new-instance v1, Lnet/gogame/gowrap/ui/fab/PopupWindowFab$1;

    invoke-direct {v1, p0}, Lnet/gogame/gowrap/ui/fab/PopupWindowFab$1;-><init>(Lnet/gogame/gowrap/ui/fab/PopupWindowFab;)V

    iput-object v1, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->updateFab:Ljava/lang/Runnable;

    .line 68
    iput-object v0, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->animatorSet:Landroid/animation/AnimatorSet;

    const/4 v1, 0x0

    .line 69
    iput-boolean v1, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->slideIn:Z

    .line 70
    iput-boolean v1, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->slideOut:Z

    .line 71
    iput-boolean v1, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->isFirstTime:Z

    .line 72
    iput-object v0, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->handler:Landroid/os/Handler;

    .line 73
    iput v1, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->offsetX:I

    .line 74
    iput v1, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->offsetY:I

    .line 75
    iput-object v0, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->mPosX:Ljava/lang/Integer;

    .line 76
    iput-object v0, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->mPosY:Ljava/lang/Integer;

    .line 77
    new-instance v0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab$2;

    invoke-direct {v0, p0}, Lnet/gogame/gowrap/ui/fab/PopupWindowFab$2;-><init>(Lnet/gogame/gowrap/ui/fab/PopupWindowFab;)V

    iput-object v0, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->updatePopupWindowRunnable:Ljava/lang/Runnable;

    .line 90
    iput v1, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->retries:I

    .line 91
    iput v1, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->screenTopHeightLimit:I

    .line 92
    iput v1, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->screenBottomHeightLimit:I

    .line 93
    iput v1, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->screenHeight:I

    .line 94
    iput v1, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->screenWidth:I

    .line 95
    iput v1, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->fabStartPositionX:I

    .line 96
    iput v1, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->fabEndPositionX:I

    const/4 v0, 0x1

    .line 97
    iput-boolean v0, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->isFabLeft:Z

    .line 102
    iput-boolean p1, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->fixX:Z

    .line 103
    iput-boolean p2, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->fixY:Z

    return-void
.end method

.method static synthetic access$000(Lnet/gogame/gowrap/ui/fab/PopupWindowFab;)Landroid/view/View;
    .locals 0

    .line 36
    iget-object p0, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->view:Landroid/view/View;

    return-object p0
.end method

.method static synthetic access$100(Lnet/gogame/gowrap/ui/fab/PopupWindowFab;)Lnet/gogame/gowrap/ui/fab/PopupWindowFab;
    .locals 0

    .line 36
    iget-object p0, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->self:Lnet/gogame/gowrap/ui/fab/PopupWindowFab;

    return-object p0
.end method

.method static synthetic access$1000(Lnet/gogame/gowrap/ui/fab/PopupWindowFab;Landroid/app/Activity;Ljava/lang/Runnable;)V
    .locals 0

    .line 36
    invoke-direct {p0, p1, p2}, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->post(Landroid/app/Activity;Ljava/lang/Runnable;)V

    return-void
.end method

.method static synthetic access$1100(Lnet/gogame/gowrap/ui/fab/PopupWindowFab;)I
    .locals 0

    .line 36
    iget p0, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->screenWidth:I

    return p0
.end method

.method static synthetic access$1200(Lnet/gogame/gowrap/ui/fab/PopupWindowFab;)Ljava/lang/Integer;
    .locals 0

    .line 36
    iget-object p0, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->mPosX:Ljava/lang/Integer;

    return-object p0
.end method

.method static synthetic access$1202(Lnet/gogame/gowrap/ui/fab/PopupWindowFab;Ljava/lang/Integer;)Ljava/lang/Integer;
    .locals 0

    .line 36
    iput-object p1, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->mPosX:Ljava/lang/Integer;

    return-object p1
.end method

.method static synthetic access$1300(Lnet/gogame/gowrap/ui/fab/PopupWindowFab;)I
    .locals 0

    .line 36
    iget p0, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->fabEndPositionX:I

    return p0
.end method

.method static synthetic access$1402(Lnet/gogame/gowrap/ui/fab/PopupWindowFab;Z)Z
    .locals 0

    .line 36
    iput-boolean p1, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->isFabLeft:Z

    return p1
.end method

.method static synthetic access$1500(Lnet/gogame/gowrap/ui/fab/PopupWindowFab;)I
    .locals 0

    .line 36
    iget p0, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->fabStartPositionX:I

    return p0
.end method

.method static synthetic access$1600(Lnet/gogame/gowrap/ui/fab/PopupWindowFab;Landroid/app/Activity;I)Landroid/animation/AnimatorSet;
    .locals 0

    .line 36
    invoke-direct {p0, p1, p2}, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->getInsideAnimation(Landroid/app/Activity;I)Landroid/animation/AnimatorSet;

    move-result-object p0

    return-object p0
.end method

.method static synthetic access$1700(Lnet/gogame/gowrap/ui/fab/PopupWindowFab;Landroid/app/Activity;)V
    .locals 0

    .line 36
    invoke-direct {p0, p1}, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->updateFabLocation(Landroid/app/Activity;)V

    return-void
.end method

.method static synthetic access$1800(Lnet/gogame/gowrap/ui/fab/PopupWindowFab;)I
    .locals 0

    .line 36
    iget p0, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->offsetX:I

    return p0
.end method

.method static synthetic access$1802(Lnet/gogame/gowrap/ui/fab/PopupWindowFab;I)I
    .locals 0

    .line 36
    iput p1, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->offsetX:I

    return p1
.end method

.method static synthetic access$1900(Lnet/gogame/gowrap/ui/fab/PopupWindowFab;)I
    .locals 0

    .line 36
    iget p0, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->offsetY:I

    return p0
.end method

.method static synthetic access$200(Lnet/gogame/gowrap/ui/fab/PopupWindowFab;)Z
    .locals 0

    .line 36
    iget-boolean p0, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->fixY:Z

    return p0
.end method

.method static synthetic access$2000(Lnet/gogame/gowrap/ui/fab/PopupWindowFab;)I
    .locals 0

    .line 36
    iget p0, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->screenTopHeightLimit:I

    return p0
.end method

.method static synthetic access$202(Lnet/gogame/gowrap/ui/fab/PopupWindowFab;Z)Z
    .locals 0

    .line 36
    iput-boolean p1, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->fixY:Z

    return p1
.end method

.method static synthetic access$2100(Lnet/gogame/gowrap/ui/fab/PopupWindowFab;)I
    .locals 0

    .line 36
    iget p0, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->screenHeight:I

    return p0
.end method

.method static synthetic access$2200(Lnet/gogame/gowrap/ui/fab/PopupWindowFab;)I
    .locals 0

    .line 36
    iget p0, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->screenBottomHeightLimit:I

    return p0
.end method

.method static synthetic access$2302(Lnet/gogame/gowrap/ui/fab/PopupWindowFab;Ljava/lang/Integer;)Ljava/lang/Integer;
    .locals 0

    .line 36
    iput-object p1, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->mPosY:Ljava/lang/Integer;

    return-object p1
.end method

.method static synthetic access$2400(Lnet/gogame/gowrap/ui/fab/PopupWindowFab;Landroid/app/Activity;I)Landroid/animation/AnimatorSet;
    .locals 0

    .line 36
    invoke-direct {p0, p1, p2}, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->getAnimation(Landroid/app/Activity;I)Landroid/animation/AnimatorSet;

    move-result-object p0

    return-object p0
.end method

.method static synthetic access$2500(Lnet/gogame/gowrap/ui/fab/PopupWindowFab;Landroid/app/Activity;)V
    .locals 0

    .line 36
    invoke-direct {p0, p1}, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->setTimerForFab(Landroid/app/Activity;)V

    return-void
.end method

.method static synthetic access$300(Lnet/gogame/gowrap/ui/fab/PopupWindowFab;)Z
    .locals 0

    .line 36
    iget-boolean p0, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->fixX:Z

    return p0
.end method

.method static synthetic access$302(Lnet/gogame/gowrap/ui/fab/PopupWindowFab;Z)Z
    .locals 0

    .line 36
    iput-boolean p1, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->fixX:Z

    return p1
.end method

.method static synthetic access$400(Lnet/gogame/gowrap/ui/fab/PopupWindowFab;)Landroid/widget/PopupWindow;
    .locals 0

    .line 36
    iget-object p0, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->popupWindow:Landroid/widget/PopupWindow;

    return-object p0
.end method

.method static synthetic access$402(Lnet/gogame/gowrap/ui/fab/PopupWindowFab;Landroid/widget/PopupWindow;)Landroid/widget/PopupWindow;
    .locals 0

    .line 36
    iput-object p1, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->popupWindow:Landroid/widget/PopupWindow;

    return-object p1
.end method

.method static synthetic access$500(Lnet/gogame/gowrap/ui/fab/PopupWindowFab;)I
    .locals 0

    .line 36
    invoke-direct {p0}, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->getX()I

    move-result p0

    return p0
.end method

.method static synthetic access$600(Lnet/gogame/gowrap/ui/fab/PopupWindowFab;)I
    .locals 0

    .line 36
    invoke-direct {p0}, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->getY()I

    move-result p0

    return p0
.end method

.method static synthetic access$700(Lnet/gogame/gowrap/ui/fab/PopupWindowFab;)Landroid/animation/AnimatorSet;
    .locals 0

    .line 36
    iget-object p0, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->animatorSet:Landroid/animation/AnimatorSet;

    return-object p0
.end method

.method static synthetic access$702(Lnet/gogame/gowrap/ui/fab/PopupWindowFab;Landroid/animation/AnimatorSet;)Landroid/animation/AnimatorSet;
    .locals 0

    .line 36
    iput-object p1, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->animatorSet:Landroid/animation/AnimatorSet;

    return-object p1
.end method

.method static synthetic access$800(Lnet/gogame/gowrap/ui/fab/PopupWindowFab;)Landroid/animation/AnimatorSet;
    .locals 0

    .line 36
    invoke-direct {p0}, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->getBlinkingAnimation()Landroid/animation/AnimatorSet;

    move-result-object p0

    return-object p0
.end method

.method static synthetic access$900(Lnet/gogame/gowrap/ui/fab/PopupWindowFab;Landroid/app/Activity;Ljava/lang/Runnable;J)V
    .locals 0

    .line 36
    invoke-direct {p0, p1, p2, p3, p4}, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->postDelayed(Landroid/app/Activity;Ljava/lang/Runnable;J)V

    return-void
.end method

.method private getAnimation(Landroid/app/Activity;I)Landroid/animation/AnimatorSet;
    .locals 12
    .annotation build Landroid/annotation/TargetApi;
        value = 0xb
    .end annotation

    .line 434
    new-instance v0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab$8;

    invoke-direct {v0, p0, p2, p1}, Lnet/gogame/gowrap/ui/fab/PopupWindowFab$8;-><init>(Lnet/gogame/gowrap/ui/fab/PopupWindowFab;ILandroid/app/Activity;)V

    .line 446
    new-instance v1, Ljava/util/ArrayList;

    invoke-direct {v1}, Ljava/util/ArrayList;-><init>()V

    .line 448
    sget-object v2, Lnet/gogame/gowrap/integrations/core/Wrapper;->INSTANCE:Lnet/gogame/gowrap/integrations/core/Wrapper;

    invoke-virtual {v2}, Lnet/gogame/gowrap/integrations/core/Wrapper;->isSlideOut()Z

    move-result v2

    const/4 v3, 0x1

    const-wide/16 v4, 0x1f4

    const/4 v6, 0x2

    const/4 v7, 0x0

    if-eqz v2, :cond_1

    .line 450
    iget-boolean v2, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->isFabLeft:Z

    if-eqz v2, :cond_0

    neg-int v2, p2

    goto :goto_0

    :cond_0
    move v2, p2

    .line 455
    :goto_0
    new-instance v8, Landroid/animation/ValueAnimator;

    invoke-direct {v8}, Landroid/animation/ValueAnimator;-><init>()V

    .line 456
    invoke-virtual {v8, v4, v5}, Landroid/animation/ValueAnimator;->setDuration(J)Landroid/animation/ValueAnimator;

    .line 457
    new-array v9, v6, [I

    aput v2, v9, v7

    aput v7, v9, v3

    invoke-virtual {v8, v9}, Landroid/animation/ValueAnimator;->setIntValues([I)V

    .line 458
    invoke-virtual {v8, v0}, Landroid/animation/ValueAnimator;->addUpdateListener(Landroid/animation/ValueAnimator$AnimatorUpdateListener;)V

    .line 459
    invoke-interface {v1, v8}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    .line 462
    :cond_1
    new-instance v2, Landroid/animation/ValueAnimator;

    invoke-direct {v2}, Landroid/animation/ValueAnimator;-><init>()V

    const-wide/16 v8, 0x1388

    .line 463
    invoke-virtual {v2, v8, v9}, Landroid/animation/ValueAnimator;->setDuration(J)Landroid/animation/ValueAnimator;

    .line 464
    new-array v8, v6, [I

    fill-array-data v8, :array_0

    invoke-virtual {v2, v8}, Landroid/animation/ValueAnimator;->setIntValues([I)V

    .line 465
    invoke-virtual {v2, v0}, Landroid/animation/ValueAnimator;->addUpdateListener(Landroid/animation/ValueAnimator$AnimatorUpdateListener;)V

    .line 466
    invoke-interface {v1, v2}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    .line 468
    sget-object v2, Lnet/gogame/gowrap/integrations/core/Wrapper;->INSTANCE:Lnet/gogame/gowrap/integrations/core/Wrapper;

    invoke-virtual {v2}, Lnet/gogame/gowrap/integrations/core/Wrapper;->isSlideIn()Z

    move-result v2

    if-eqz v2, :cond_3

    .line 470
    iget-boolean v2, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->isFabLeft:Z

    const-wide v8, 0x3fe3333333333333L    # 0.6

    if-eqz v2, :cond_2

    int-to-double v10, p2

    .line 471
    invoke-static {v10, v11}, Ljava/lang/Double;->isNaN(D)Z

    mul-double v10, v10, v8

    invoke-static {v10, v11}, Ljava/lang/Math;->ceil(D)D

    move-result-wide v8

    double-to-int p2, v8

    neg-int p2, p2

    goto :goto_1

    :cond_2
    int-to-double v10, p2

    .line 473
    invoke-static {v10, v11}, Ljava/lang/Double;->isNaN(D)Z

    mul-double v10, v10, v8

    invoke-static {v10, v11}, Ljava/lang/Math;->ceil(D)D

    move-result-wide v8

    double-to-int p2, v8

    .line 475
    :goto_1
    new-instance v2, Landroid/animation/ValueAnimator;

    invoke-direct {v2}, Landroid/animation/ValueAnimator;-><init>()V

    .line 476
    invoke-virtual {v2, v4, v5}, Landroid/animation/ValueAnimator;->setDuration(J)Landroid/animation/ValueAnimator;

    .line 477
    new-array v4, v6, [I

    aput v7, v4, v7

    aput p2, v4, v3

    invoke-virtual {v2, v4}, Landroid/animation/ValueAnimator;->setIntValues([I)V

    .line 478
    invoke-virtual {v2, v0}, Landroid/animation/ValueAnimator;->addUpdateListener(Landroid/animation/ValueAnimator$AnimatorUpdateListener;)V

    .line 479
    invoke-direct {p0, p1}, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->getAnimationListener(Landroid/app/Activity;)Landroid/animation/Animator$AnimatorListener;

    move-result-object p1

    invoke-virtual {v2, p1}, Landroid/animation/ValueAnimator;->addListener(Landroid/animation/Animator$AnimatorListener;)V

    .line 480
    invoke-interface {v1, v2}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    .line 483
    :cond_3
    new-instance p1, Landroid/animation/AnimatorSet;

    invoke-direct {p1}, Landroid/animation/AnimatorSet;-><init>()V

    .line 484
    invoke-interface {v1}, Ljava/util/List;->size()I

    move-result p2

    new-array p2, p2, [Landroid/animation/ValueAnimator;

    invoke-interface {v1, p2}, Ljava/util/List;->toArray([Ljava/lang/Object;)[Ljava/lang/Object;

    move-result-object p2

    check-cast p2, [Landroid/animation/Animator;

    invoke-virtual {p1, p2}, Landroid/animation/AnimatorSet;->playSequentially([Landroid/animation/Animator;)V

    return-object p1

    nop

    :array_0
    .array-data 4
        0x0
        0x0
    .end array-data
.end method

.method private getAnimationListener(Landroid/app/Activity;)Landroid/animation/Animator$AnimatorListener;
    .locals 1

    .line 408
    new-instance v0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab$7;

    invoke-direct {v0, p0, p1}, Lnet/gogame/gowrap/ui/fab/PopupWindowFab$7;-><init>(Lnet/gogame/gowrap/ui/fab/PopupWindowFab;Landroid/app/Activity;)V

    return-object v0
.end method

.method private getBlinkingAnimation()Landroid/animation/AnimatorSet;
    .locals 6
    .annotation build Landroid/annotation/TargetApi;
        value = 0xb
    .end annotation

    .line 491
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    .line 492
    sget v1, Landroid/os/Build$VERSION;->SDK_INT:I

    const/16 v2, 0xe

    if-lt v1, v2, :cond_0

    .line 493
    iget-object v1, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->view:Landroid/view/View;

    sget-object v2, Landroid/view/View;->ALPHA:Landroid/util/Property;

    const/4 v3, 0x2

    new-array v4, v3, [F

    fill-array-data v4, :array_0

    invoke-static {v1, v2, v4}, Landroid/animation/ObjectAnimator;->ofFloat(Ljava/lang/Object;Landroid/util/Property;[F)Landroid/animation/ObjectAnimator;

    move-result-object v1

    const-wide/16 v4, 0xfa

    .line 494
    invoke-virtual {v1, v4, v5}, Landroid/animation/ValueAnimator;->setDuration(J)Landroid/animation/ValueAnimator;

    const/16 v2, 0xa

    .line 495
    invoke-virtual {v1, v2}, Landroid/animation/ValueAnimator;->setRepeatCount(I)V

    .line 496
    invoke-virtual {v1, v3}, Landroid/animation/ValueAnimator;->setRepeatMode(I)V

    .line 497
    invoke-interface {v0, v1}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    .line 499
    :cond_0
    new-instance v1, Landroid/animation/AnimatorSet;

    invoke-direct {v1}, Landroid/animation/AnimatorSet;-><init>()V

    .line 500
    invoke-interface {v0}, Ljava/util/List;->size()I

    move-result v2

    new-array v2, v2, [Landroid/animation/ValueAnimator;

    invoke-interface {v0, v2}, Ljava/util/List;->toArray([Ljava/lang/Object;)[Ljava/lang/Object;

    move-result-object v0

    check-cast v0, [Landroid/animation/Animator;

    invoke-virtual {v1, v0}, Landroid/animation/AnimatorSet;->playSequentially([Landroid/animation/Animator;)V

    return-object v1

    :array_0
    .array-data 4
        0x0
        0x3f800000    # 1.0f
    .end array-data
.end method

.method private getHandler(Landroid/app/Activity;)Landroid/os/Handler;
    .locals 2

    .line 115
    iget-object v0, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->handler:Landroid/os/Handler;

    if-eqz v0, :cond_0

    iget-object v0, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->handler:Landroid/os/Handler;

    invoke-virtual {v0}, Landroid/os/Handler;->getLooper()Landroid/os/Looper;

    move-result-object v0

    invoke-virtual {p1}, Landroid/app/Activity;->getMainLooper()Landroid/os/Looper;

    move-result-object v1

    if-eq v0, v1, :cond_1

    .line 116
    :cond_0
    new-instance v0, Landroid/os/Handler;

    invoke-virtual {p1}, Landroid/app/Activity;->getMainLooper()Landroid/os/Looper;

    move-result-object p1

    invoke-direct {v0, p1}, Landroid/os/Handler;-><init>(Landroid/os/Looper;)V

    iput-object v0, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->handler:Landroid/os/Handler;

    .line 118
    :cond_1
    iget-object p1, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->handler:Landroid/os/Handler;

    return-object p1
.end method

.method private getInsideAnimation(Landroid/app/Activity;I)Landroid/animation/AnimatorSet;
    .locals 8
    .annotation build Landroid/annotation/TargetApi;
        value = 0xb
    .end annotation

    .line 507
    new-instance v0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab$9;

    invoke-direct {v0, p0, p1}, Lnet/gogame/gowrap/ui/fab/PopupWindowFab$9;-><init>(Lnet/gogame/gowrap/ui/fab/PopupWindowFab;Landroid/app/Activity;)V

    .line 516
    new-instance v1, Ljava/util/ArrayList;

    invoke-direct {v1}, Ljava/util/ArrayList;-><init>()V

    .line 518
    new-instance v2, Landroid/animation/ValueAnimator;

    invoke-direct {v2}, Landroid/animation/ValueAnimator;-><init>()V

    const-wide/16 v3, 0xbb8

    .line 519
    invoke-virtual {v2, v3, v4}, Landroid/animation/ValueAnimator;->setDuration(J)Landroid/animation/ValueAnimator;

    const/4 v3, 0x2

    .line 520
    new-array v4, v3, [I

    fill-array-data v4, :array_0

    invoke-virtual {v2, v4}, Landroid/animation/ValueAnimator;->setIntValues([I)V

    .line 521
    invoke-virtual {v2, v0}, Landroid/animation/ValueAnimator;->addUpdateListener(Landroid/animation/ValueAnimator$AnimatorUpdateListener;)V

    .line 522
    invoke-interface {v1, v2}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    .line 524
    sget-object v2, Lnet/gogame/gowrap/integrations/core/Wrapper;->INSTANCE:Lnet/gogame/gowrap/integrations/core/Wrapper;

    invoke-virtual {v2}, Lnet/gogame/gowrap/integrations/core/Wrapper;->isSlideIn()Z

    move-result v2

    if-eqz v2, :cond_1

    .line 526
    iget-boolean v2, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->isFabLeft:Z

    const-wide v4, 0x3fd3333333333333L    # 0.3

    if-eqz v2, :cond_0

    int-to-double v6, p2

    .line 527
    invoke-static {v6, v7}, Ljava/lang/Double;->isNaN(D)Z

    mul-double v6, v6, v4

    invoke-static {v6, v7}, Ljava/lang/Math;->ceil(D)D

    move-result-wide v4

    double-to-int p2, v4

    neg-int p2, p2

    goto :goto_0

    :cond_0
    int-to-double v6, p2

    .line 529
    invoke-static {v6, v7}, Ljava/lang/Double;->isNaN(D)Z

    mul-double v6, v6, v4

    invoke-static {v6, v7}, Ljava/lang/Math;->ceil(D)D

    move-result-wide v4

    double-to-int p2, v4

    .line 532
    :goto_0
    new-instance v2, Landroid/animation/ValueAnimator;

    invoke-direct {v2}, Landroid/animation/ValueAnimator;-><init>()V

    const-wide/16 v4, 0x1f4

    .line 533
    invoke-virtual {v2, v4, v5}, Landroid/animation/ValueAnimator;->setDuration(J)Landroid/animation/ValueAnimator;

    .line 534
    new-array v3, v3, [I

    const/4 v4, 0x0

    aput v4, v3, v4

    const/4 v4, 0x1

    aput p2, v3, v4

    invoke-virtual {v2, v3}, Landroid/animation/ValueAnimator;->setIntValues([I)V

    .line 535
    invoke-virtual {v2, v0}, Landroid/animation/ValueAnimator;->addUpdateListener(Landroid/animation/ValueAnimator$AnimatorUpdateListener;)V

    .line 536
    invoke-direct {p0, p1}, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->getAnimationListener(Landroid/app/Activity;)Landroid/animation/Animator$AnimatorListener;

    move-result-object p1

    invoke-virtual {v2, p1}, Landroid/animation/ValueAnimator;->addListener(Landroid/animation/Animator$AnimatorListener;)V

    .line 537
    invoke-interface {v1, v2}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    .line 540
    :cond_1
    new-instance p1, Landroid/animation/AnimatorSet;

    invoke-direct {p1}, Landroid/animation/AnimatorSet;-><init>()V

    .line 541
    invoke-interface {v1}, Ljava/util/List;->size()I

    move-result p2

    new-array p2, p2, [Landroid/animation/ValueAnimator;

    invoke-interface {v1, p2}, Ljava/util/List;->toArray([Ljava/lang/Object;)[Ljava/lang/Object;

    move-result-object p2

    check-cast p2, [Landroid/animation/Animator;

    invoke-virtual {p1, p2}, Landroid/animation/AnimatorSet;->playSequentially([Landroid/animation/Animator;)V

    return-object p1

    nop

    :array_0
    .array-data 4
        0x0
        0x0
    .end array-data
.end method

.method private getX()I
    .locals 2

    .line 107
    iget-object v0, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->mPosX:Ljava/lang/Integer;

    invoke-virtual {v0}, Ljava/lang/Integer;->intValue()I

    move-result v0

    iget v1, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->offsetX:I

    add-int/2addr v0, v1

    return v0
.end method

.method private getY()I
    .locals 2

    .line 111
    iget-object v0, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->mPosY:Ljava/lang/Integer;

    invoke-virtual {v0}, Ljava/lang/Integer;->intValue()I

    move-result v0

    iget v1, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->offsetY:I

    add-int/2addr v0, v1

    return v0
.end method

.method private post(Landroid/app/Activity;Ljava/lang/Runnable;)V
    .locals 0

    .line 122
    invoke-direct {p0, p1}, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->getHandler(Landroid/app/Activity;)Landroid/os/Handler;

    move-result-object p1

    invoke-virtual {p1, p2}, Landroid/os/Handler;->post(Ljava/lang/Runnable;)Z

    return-void
.end method

.method private postDelayed(Landroid/app/Activity;Ljava/lang/Runnable;J)V
    .locals 0

    .line 126
    invoke-direct {p0, p1}, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->getHandler(Landroid/app/Activity;)Landroid/os/Handler;

    move-result-object p1

    invoke-virtual {p1, p2, p3, p4}, Landroid/os/Handler;->postDelayed(Ljava/lang/Runnable;J)Z

    return-void
.end method

.method private setTimerForFab(Landroid/app/Activity;)V
    .locals 7

    .line 145
    new-instance v0, Ljava/util/Timer;

    invoke-direct {v0}, Ljava/util/Timer;-><init>()V

    iput-object v0, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->timer:Ljava/util/Timer;

    .line 146
    new-instance v0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab$3;

    invoke-direct {v0, p0, p1}, Lnet/gogame/gowrap/ui/fab/PopupWindowFab$3;-><init>(Lnet/gogame/gowrap/ui/fab/PopupWindowFab;Landroid/app/Activity;)V

    iput-object v0, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->timerTask:Ljava/util/TimerTask;

    .line 176
    iget-object v1, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->timer:Ljava/util/Timer;

    iget-object v2, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->timerTask:Ljava/util/TimerTask;

    const-wide/32 v3, 0xea60

    const-wide/32 v5, 0xea60

    invoke-virtual/range {v1 .. v6}, Ljava/util/Timer;->schedule(Ljava/util/TimerTask;JJ)V

    return-void
.end method

.method private updateFabLocation(Landroid/app/Activity;)V
    .locals 1

    .line 547
    iget-object v0, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->updatePopupWindowRunnable:Ljava/lang/Runnable;

    invoke-direct {p0, p1, v0}, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->post(Landroid/app/Activity;Ljava/lang/Runnable;)V

    return-void
.end method


# virtual methods
.method public cancelAnimation(Landroid/app/Activity;)V
    .locals 2

    .line 574
    iget-object v0, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->animatorSet:Landroid/animation/AnimatorSet;

    if-eqz v0, :cond_0

    .line 575
    iget-object v0, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->animatorSet:Landroid/animation/AnimatorSet;

    .line 576
    new-instance v1, Lnet/gogame/gowrap/ui/fab/PopupWindowFab$11;

    invoke-direct {v1, p0, v0}, Lnet/gogame/gowrap/ui/fab/PopupWindowFab$11;-><init>(Lnet/gogame/gowrap/ui/fab/PopupWindowFab;Landroid/animation/AnimatorSet;)V

    invoke-direct {p0, p1, v1}, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->post(Landroid/app/Activity;Ljava/lang/Runnable;)V

    const/4 p1, 0x0

    .line 587
    iput-object p1, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->animatorSet:Landroid/animation/AnimatorSet;

    :cond_0
    return-void
.end method

.method public cancelTimer()V
    .locals 1

    .line 592
    iget-object v0, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->timer:Ljava/util/Timer;

    if-eqz v0, :cond_0

    .line 593
    iget-object v0, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->timer:Ljava/util/Timer;

    invoke-virtual {v0}, Ljava/util/Timer;->cancel()V

    .line 596
    :cond_0
    iget-object v0, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->timerTask:Ljava/util/TimerTask;

    if-eqz v0, :cond_1

    .line 597
    iget-object v0, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->timerTask:Ljava/util/TimerTask;

    invoke-virtual {v0}, Ljava/util/TimerTask;->cancel()Z

    :cond_1
    return-void
.end method

.method public destroy(Landroid/app/Activity;)V
    .locals 0

    .line 603
    invoke-virtual {p0, p1}, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->hide(Landroid/app/Activity;)V

    return-void
.end method

.method public handleTouchEvent(Landroid/view/MotionEvent;)Z
    .locals 6

    .line 131
    new-instance v0, Landroid/graphics/Rect;

    invoke-direct {p0}, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->getX()I

    move-result v1

    invoke-direct {p0}, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->getY()I

    move-result v2

    invoke-direct {p0}, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->getX()I

    move-result v3

    iget-object v4, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->view:Landroid/view/View;

    invoke-virtual {v4}, Landroid/view/View;->getWidth()I

    move-result v4

    add-int/2addr v3, v4

    .line 132
    invoke-direct {p0}, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->getY()I

    move-result v4

    iget-object v5, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->view:Landroid/view/View;

    invoke-virtual {v5}, Landroid/view/View;->getHeight()I

    move-result v5

    add-int/2addr v4, v5

    invoke-direct {v0, v1, v2, v3, v4}, Landroid/graphics/Rect;-><init>(IIII)V

    .line 133
    invoke-virtual {p1}, Landroid/view/MotionEvent;->getX()F

    move-result v1

    float-to-int v1, v1

    invoke-virtual {p1}, Landroid/view/MotionEvent;->getY()F

    move-result v2

    float-to-int v2, v2

    invoke-virtual {v0, v1, v2}, Landroid/graphics/Rect;->contains(II)Z

    move-result v0

    if-nez v0, :cond_0

    const/4 p1, 0x0

    return p1

    .line 136
    :cond_0
    iget-object v0, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->view:Landroid/view/View;

    invoke-virtual {v0, p1}, Landroid/view/View;->dispatchTouchEvent(Landroid/view/MotionEvent;)Z

    move-result p1

    return p1
.end method

.method public hide(Landroid/app/Activity;)V
    .locals 1

    .line 552
    invoke-virtual {p0, p1}, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->cancelAnimation(Landroid/app/Activity;)V

    .line 554
    new-instance v0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab$10;

    invoke-direct {v0, p0}, Lnet/gogame/gowrap/ui/fab/PopupWindowFab$10;-><init>(Lnet/gogame/gowrap/ui/fab/PopupWindowFab;)V

    invoke-direct {p0, p1, v0}, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->post(Landroid/app/Activity;Ljava/lang/Runnable;)V

    .line 568
    iget-object p1, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->view:Landroid/view/View;

    if-eqz p1, :cond_0

    const/4 p1, 0x0

    .line 569
    iput-object p1, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->view:Landroid/view/View;

    :cond_0
    return-void
.end method

.method public show(Landroid/app/Activity;)V
    .locals 8

    .line 182
    iget-object v0, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->popupWindow:Landroid/widget/PopupWindow;

    if-eqz v0, :cond_0

    return-void

    .line 186
    :cond_0
    invoke-virtual {p0}, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->cancelTimer()V

    .line 189
    new-instance v0, Landroid/util/DisplayMetrics;

    invoke-direct {v0}, Landroid/util/DisplayMetrics;-><init>()V

    .line 190
    invoke-virtual {p1}, Landroid/app/Activity;->getWindowManager()Landroid/view/WindowManager;

    move-result-object v1

    invoke-interface {v1}, Landroid/view/WindowManager;->getDefaultDisplay()Landroid/view/Display;

    move-result-object v1

    invoke-virtual {v1, v0}, Landroid/view/Display;->getMetrics(Landroid/util/DisplayMetrics;)V

    .line 191
    iget v1, v0, Landroid/util/DisplayMetrics;->heightPixels:I

    iput v1, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->screenHeight:I

    .line 192
    iget v0, v0, Landroid/util/DisplayMetrics;->widthPixels:I

    iput v0, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->screenWidth:I

    const/high16 v0, 0x42200000    # 40.0f

    .line 194
    invoke-static {p1, v0}, Lnet/gogame/gowrap/ui/utils/DisplayUtils;->pxFromDp(Landroid/content/Context;F)I

    move-result v0

    iput v0, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->screenTopHeightLimit:I

    const/high16 v0, 0x42480000    # 50.0f

    .line 195
    invoke-static {p1, v0}, Lnet/gogame/gowrap/ui/utils/DisplayUtils;->pxFromDp(Landroid/content/Context;F)I

    move-result v0

    iput v0, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->screenBottomHeightLimit:I

    .line 198
    invoke-static {p1}, Lnet/gogame/gowrap/wrapper/OverlayUIHelper;->getRootView(Landroid/app/Activity;)Landroid/view/View;

    move-result-object v0

    .line 199
    invoke-static {v0}, Lnet/gogame/gowrap/wrapper/OverlayUIHelper;->getLeafView(Landroid/view/View;)Landroid/view/View;

    move-result-object v1

    if-nez v1, :cond_2

    .line 202
    iget v1, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->retries:I

    add-int/lit8 v1, v1, 0x1

    iput v1, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->retries:I

    .line 203
    iget v1, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->retries:I

    const/4 v2, 0x4

    if-gt v1, v2, :cond_1

    .line 204
    new-instance v0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab$4;

    invoke-direct {v0, p0, p1}, Lnet/gogame/gowrap/ui/fab/PopupWindowFab$4;-><init>(Lnet/gogame/gowrap/ui/fab/PopupWindowFab;Landroid/app/Activity;)V

    const-wide/16 v1, 0x1f4

    invoke-direct {p0, p1, v0, v1, v2}, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->postDelayed(Landroid/app/Activity;Ljava/lang/Runnable;J)V

    return-void

    :cond_1
    move-object v1, v0

    :cond_2
    const/4 v2, 0x0

    .line 221
    iput v2, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->retries:I

    .line 223
    new-instance v3, Lnet/gogame/gowrap/ui/fab/PopupWindowFab$5;

    invoke-direct {v3, p0, p1}, Lnet/gogame/gowrap/ui/fab/PopupWindowFab$5;-><init>(Lnet/gogame/gowrap/ui/fab/PopupWindowFab;Landroid/app/Activity;)V

    .line 332
    sget v4, Lnet/gogame/gowrap/R$drawable;->net_gogame_gowrap_fab:I

    invoke-virtual {p0, p1, v4}, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->createImageView(Landroid/content/Context;I)Landroid/view/View;

    move-result-object v4

    iput-object v4, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->view:Landroid/view/View;

    .line 333
    iget-object v4, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->view:Landroid/view/View;

    invoke-virtual {v4, v3}, Landroid/view/View;->setOnTouchListener(Landroid/view/View$OnTouchListener;)V

    .line 336
    invoke-virtual {p1}, Landroid/app/Activity;->getResources()Landroid/content/res/Resources;

    move-result-object v4

    sget v5, Lnet/gogame/gowrap/R$drawable;->net_gogame_gowrap_fab:I

    invoke-virtual {v4, v5}, Landroid/content/res/Resources;->getDrawable(I)Landroid/graphics/drawable/Drawable;

    move-result-object v4

    .line 337
    invoke-virtual {v4}, Landroid/graphics/drawable/Drawable;->getIntrinsicWidth()I

    move-result v4

    .line 339
    sget-object v5, Lnet/gogame/gowrap/integrations/core/Wrapper;->INSTANCE:Lnet/gogame/gowrap/integrations/core/Wrapper;

    invoke-virtual {v5}, Lnet/gogame/gowrap/integrations/core/Wrapper;->isSlideOut()Z

    move-result v5

    if-nez v5, :cond_4

    sget-object v5, Lnet/gogame/gowrap/integrations/core/Wrapper;->INSTANCE:Lnet/gogame/gowrap/integrations/core/Wrapper;

    invoke-virtual {v5}, Lnet/gogame/gowrap/integrations/core/Wrapper;->isSlideIn()Z

    move-result v5

    if-eqz v5, :cond_3

    goto :goto_0

    :cond_3
    int-to-double v4, v4

    const-wide v6, 0x3fc999999999999aL    # 0.2

    .line 343
    invoke-static {v4, v5}, Ljava/lang/Double;->isNaN(D)Z

    mul-double v6, v6, v4

    invoke-static {v6, v7}, Ljava/lang/Math;->round(D)J

    move-result-wide v6

    long-to-int v6, v6

    neg-int v6, v6

    iput v6, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->fabStartPositionX:I

    const-wide v6, 0x3fe999999999999aL    # 0.8

    .line 344
    invoke-static {v4, v5}, Ljava/lang/Double;->isNaN(D)Z

    mul-double v4, v4, v6

    invoke-static {v4, v5}, Ljava/lang/Math;->round(D)J

    move-result-wide v4

    long-to-int v4, v4

    iput v4, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->fabEndPositionX:I

    goto :goto_1

    :cond_4
    :goto_0
    int-to-double v4, v4

    const-wide v6, 0x3fe4cccccccccccdL    # 0.65

    .line 340
    invoke-static {v4, v5}, Ljava/lang/Double;->isNaN(D)Z

    mul-double v6, v6, v4

    invoke-static {v6, v7}, Ljava/lang/Math;->round(D)J

    move-result-wide v6

    long-to-int v6, v6

    neg-int v6, v6

    iput v6, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->fabStartPositionX:I

    const-wide v6, 0x3ff4cccccccccccdL    # 1.3

    .line 341
    invoke-static {v4, v5}, Ljava/lang/Double;->isNaN(D)Z

    mul-double v4, v4, v6

    invoke-static {v4, v5}, Ljava/lang/Math;->round(D)J

    move-result-wide v4

    long-to-int v4, v4

    iput v4, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->fabEndPositionX:I

    :goto_1
    const-string v4, "window"

    .line 349
    invoke-virtual {p1, v4}, Landroid/app/Activity;->getSystemService(Ljava/lang/String;)Ljava/lang/Object;

    move-result-object v4

    check-cast v4, Landroid/view/WindowManager;

    .line 351
    new-instance v5, Landroid/util/DisplayMetrics;

    invoke-direct {v5}, Landroid/util/DisplayMetrics;-><init>()V

    .line 352
    invoke-interface {v4}, Landroid/view/WindowManager;->getDefaultDisplay()Landroid/view/Display;

    move-result-object v4

    invoke-virtual {v4, v5}, Landroid/view/Display;->getMetrics(Landroid/util/DisplayMetrics;)V

    .line 354
    iget-object v4, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->mPosX:Ljava/lang/Integer;

    if-eqz v4, :cond_5

    iget-object v4, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->mPosY:Ljava/lang/Integer;

    if-nez v4, :cond_6

    .line 355
    :cond_5
    iget v4, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->fabStartPositionX:I

    invoke-static {v4}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v4

    iput-object v4, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->mPosX:Ljava/lang/Integer;

    .line 356
    iget v4, v5, Landroid/util/DisplayMetrics;->heightPixels:I

    div-int/lit8 v4, v4, 0x2

    invoke-static {v4}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v4

    iput-object v4, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->mPosY:Ljava/lang/Integer;

    .line 359
    :cond_6
    new-instance v4, Landroid/widget/PopupWindow;

    iget-object v6, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->view:Landroid/view/View;

    const/4 v7, -0x2

    invoke-direct {v4, v6, v7, v7, v2}, Landroid/widget/PopupWindow;-><init>(Landroid/view/View;IIZ)V

    iput-object v4, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->popupWindow:Landroid/widget/PopupWindow;

    .line 361
    iget-object v4, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->popupWindow:Landroid/widget/PopupWindow;

    invoke-virtual {v4, v2}, Landroid/widget/PopupWindow;->setClippingEnabled(Z)V

    .line 362
    invoke-virtual {v1}, Landroid/view/View;->getParent()Landroid/view/ViewParent;

    move-result-object v1

    check-cast v1, Landroid/view/ViewGroup;

    .line 365
    new-instance v2, Lnet/gogame/gowrap/ui/fab/PopupWindowFab$6;

    invoke-direct {v2, p0, v5, v1, p1}, Lnet/gogame/gowrap/ui/fab/PopupWindowFab$6;-><init>(Lnet/gogame/gowrap/ui/fab/PopupWindowFab;Landroid/util/DisplayMetrics;Landroid/view/ViewGroup;Landroid/app/Activity;)V

    invoke-virtual {v0, v2}, Landroid/view/View;->post(Ljava/lang/Runnable;)Z

    .line 404
    iget-object p1, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->popupWindow:Landroid/widget/PopupWindow;

    invoke-virtual {p1, v3}, Landroid/widget/PopupWindow;->setTouchInterceptor(Landroid/view/View$OnTouchListener;)V

    return-void
.end method

.method public update(Landroid/app/Activity;)V
    .locals 1

    .line 141
    iget-object v0, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->updateFab:Ljava/lang/Runnable;

    invoke-virtual {p1, v0}, Landroid/app/Activity;->runOnUiThread(Ljava/lang/Runnable;)V

    return-void
.end method
