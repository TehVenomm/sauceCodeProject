.class public Lcom/zopim/android/sdk/anim/AnimatorPack;
.super Ljava/lang/Object;


# annotations
.annotation build Landroid/annotation/TargetApi;
    value = 0xb
.end annotation

.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Lcom/zopim/android/sdk/anim/AnimatorPack$Direction;
    }
.end annotation


# static fields
.field private static final DURATION:J = 0x190L

.field private static final TAG:Ljava/lang/String; = "AnimatorPack"


# direct methods
.method static constructor <clinit>()V
    .locals 0

    return-void
.end method

.method public constructor <init>()V
    .locals 0

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method

.method public static crossfade(Landroid/view/View;Landroid/view/View;)Landroid/animation/AnimatorSet;
    .locals 1

    const/4 v0, 0x0

    invoke-static {p0, p1, v0, v0}, Lcom/zopim/android/sdk/anim/AnimatorPack;->crossfade(Landroid/view/View;Landroid/view/View;Landroid/animation/Animator$AnimatorListener;Landroid/animation/Animator$AnimatorListener;)Landroid/animation/AnimatorSet;

    move-result-object p0

    return-object p0
.end method

.method public static crossfade(Landroid/view/View;Landroid/view/View;Landroid/animation/Animator$AnimatorListener;Landroid/animation/Animator$AnimatorListener;)Landroid/animation/AnimatorSet;
    .locals 7

    const-string v0, "alpha"

    const/4 v1, 0x2

    new-array v2, v1, [F

    fill-array-data v2, :array_0

    invoke-static {p0, v0, v2}, Landroid/animation/ObjectAnimator;->ofFloat(Ljava/lang/Object;Ljava/lang/String;[F)Landroid/animation/ObjectAnimator;

    move-result-object v0

    const-string v2, "alpha"

    new-array v1, v1, [F

    fill-array-data v1, :array_1

    invoke-static {p1, v2, v1}, Landroid/animation/ObjectAnimator;->ofFloat(Ljava/lang/Object;Ljava/lang/String;[F)Landroid/animation/ObjectAnimator;

    move-result-object v1

    const/high16 v2, 0x3f000000    # 0.5f

    const/high16 v3, 0x3f800000    # 1.0f

    invoke-static {p0, v3, v2}, Lcom/zopim/android/sdk/anim/AnimatorPack;->scale(Landroid/view/View;FF)Landroid/animation/AnimatorSet;

    move-result-object v4

    invoke-static {p1, v2, v3}, Lcom/zopim/android/sdk/anim/AnimatorPack;->scale(Landroid/view/View;FF)Landroid/animation/AnimatorSet;

    move-result-object v2

    new-instance v3, Landroid/animation/AnimatorSet;

    invoke-direct {v3}, Landroid/animation/AnimatorSet;-><init>()V

    const-wide/16 v5, 0x190

    invoke-virtual {v3, v5, v6}, Landroid/animation/AnimatorSet;->setDuration(J)Landroid/animation/AnimatorSet;

    invoke-virtual {v3, v0}, Landroid/animation/AnimatorSet;->play(Landroid/animation/Animator;)Landroid/animation/AnimatorSet$Builder;

    move-result-object v5

    invoke-virtual {v5, v4}, Landroid/animation/AnimatorSet$Builder;->with(Landroid/animation/Animator;)Landroid/animation/AnimatorSet$Builder;

    move-result-object v4

    invoke-virtual {v4, v1}, Landroid/animation/AnimatorSet$Builder;->before(Landroid/animation/Animator;)Landroid/animation/AnimatorSet$Builder;

    move-result-object v4

    invoke-virtual {v4, v2}, Landroid/animation/AnimatorSet$Builder;->before(Landroid/animation/Animator;)Landroid/animation/AnimatorSet$Builder;

    if-eqz p2, :cond_0

    invoke-virtual {v0, p2}, Landroid/animation/ObjectAnimator;->addListener(Landroid/animation/Animator$AnimatorListener;)V

    :cond_0
    if-eqz p3, :cond_1

    invoke-virtual {v1, p3}, Landroid/animation/ObjectAnimator;->addListener(Landroid/animation/Animator$AnimatorListener;)V

    :cond_1
    new-instance p2, Lcom/zopim/android/sdk/anim/a;

    invoke-direct {p2, p0}, Lcom/zopim/android/sdk/anim/a;-><init>(Landroid/view/View;)V

    invoke-virtual {v0, p2}, Landroid/animation/ObjectAnimator;->addListener(Landroid/animation/Animator$AnimatorListener;)V

    new-instance p0, Lcom/zopim/android/sdk/anim/b;

    invoke-direct {p0, p1}, Lcom/zopim/android/sdk/anim/b;-><init>(Landroid/view/View;)V

    invoke-virtual {v1, p0}, Landroid/animation/ObjectAnimator;->addListener(Landroid/animation/Animator$AnimatorListener;)V

    return-object v3

    nop

    :array_0
    .array-data 4
        0x3f800000    # 1.0f
        0x3f000000    # 0.5f
    .end array-data

    :array_1
    .array-data 4
        0x3f000000    # 0.5f
        0x3f800000    # 1.0f
    .end array-data
.end method

.method public static fadeIn(Landroid/view/View;)Landroid/animation/Animator;
    .locals 5

    if-nez p0, :cond_0

    new-instance p0, Landroid/animation/AnimatorSet;

    invoke-direct {p0}, Landroid/animation/AnimatorSet;-><init>()V

    return-object p0

    :cond_0
    const-string v0, "alpha"

    const/4 v1, 0x2

    new-array v1, v1, [F

    fill-array-data v1, :array_0

    invoke-static {p0, v0, v1}, Landroid/animation/ObjectAnimator;->ofFloat(Ljava/lang/Object;Ljava/lang/String;[F)Landroid/animation/ObjectAnimator;

    move-result-object v0

    const/high16 v1, 0x3f000000    # 0.5f

    const/high16 v2, 0x3f800000    # 1.0f

    invoke-static {p0, v1, v2}, Lcom/zopim/android/sdk/anim/AnimatorPack;->scale(Landroid/view/View;FF)Landroid/animation/AnimatorSet;

    move-result-object v1

    new-instance v2, Landroid/animation/AnimatorSet;

    invoke-direct {v2}, Landroid/animation/AnimatorSet;-><init>()V

    const-wide/16 v3, 0x190

    invoke-virtual {v2, v3, v4}, Landroid/animation/AnimatorSet;->setDuration(J)Landroid/animation/AnimatorSet;

    invoke-virtual {v2, v0}, Landroid/animation/AnimatorSet;->play(Landroid/animation/Animator;)Landroid/animation/AnimatorSet$Builder;

    move-result-object v0

    invoke-virtual {v0, v1}, Landroid/animation/AnimatorSet$Builder;->with(Landroid/animation/Animator;)Landroid/animation/AnimatorSet$Builder;

    new-instance v0, Lcom/zopim/android/sdk/anim/f;

    invoke-direct {v0, p0}, Lcom/zopim/android/sdk/anim/f;-><init>(Landroid/view/View;)V

    invoke-virtual {v2, v0}, Landroid/animation/AnimatorSet;->addListener(Landroid/animation/Animator$AnimatorListener;)V

    return-object v2

    :array_0
    .array-data 4
        0x0
        0x3f800000    # 1.0f
    .end array-data
.end method

.method public static fadeOut(Landroid/view/View;)Landroid/animation/Animator;
    .locals 5

    if-nez p0, :cond_0

    new-instance p0, Landroid/animation/AnimatorSet;

    invoke-direct {p0}, Landroid/animation/AnimatorSet;-><init>()V

    return-object p0

    :cond_0
    const-string v0, "alpha"

    const/4 v1, 0x2

    new-array v1, v1, [F

    fill-array-data v1, :array_0

    invoke-static {p0, v0, v1}, Landroid/animation/ObjectAnimator;->ofFloat(Ljava/lang/Object;Ljava/lang/String;[F)Landroid/animation/ObjectAnimator;

    move-result-object v0

    const/high16 v1, 0x3f800000    # 1.0f

    const/high16 v2, 0x3f000000    # 0.5f

    invoke-static {p0, v1, v2}, Lcom/zopim/android/sdk/anim/AnimatorPack;->scale(Landroid/view/View;FF)Landroid/animation/AnimatorSet;

    move-result-object v1

    new-instance v2, Landroid/animation/AnimatorSet;

    invoke-direct {v2}, Landroid/animation/AnimatorSet;-><init>()V

    const-wide/16 v3, 0x190

    invoke-virtual {v2, v3, v4}, Landroid/animation/AnimatorSet;->setDuration(J)Landroid/animation/AnimatorSet;

    invoke-virtual {v2, v0}, Landroid/animation/AnimatorSet;->play(Landroid/animation/Animator;)Landroid/animation/AnimatorSet$Builder;

    move-result-object v0

    invoke-virtual {v0, v1}, Landroid/animation/AnimatorSet$Builder;->with(Landroid/animation/Animator;)Landroid/animation/AnimatorSet$Builder;

    new-instance v0, Lcom/zopim/android/sdk/anim/e;

    invoke-direct {v0, p0}, Lcom/zopim/android/sdk/anim/e;-><init>(Landroid/view/View;)V

    invoke-virtual {v2, v0}, Landroid/animation/AnimatorSet;->addListener(Landroid/animation/Animator$AnimatorListener;)V

    return-object v2

    :array_0
    .array-data 4
        0x3f800000    # 1.0f
        0x0
    .end array-data
.end method

.method public static scale(Landroid/view/View;FF)Landroid/animation/AnimatorSet;
    .locals 5

    const-string v0, "scaleX"

    const/4 v1, 0x2

    new-array v2, v1, [F

    const/4 v3, 0x0

    aput p1, v2, v3

    const/4 v4, 0x1

    aput p2, v2, v4

    invoke-static {p0, v0, v2}, Landroid/animation/ObjectAnimator;->ofFloat(Ljava/lang/Object;Ljava/lang/String;[F)Landroid/animation/ObjectAnimator;

    move-result-object v0

    const-string v2, "scaleY"

    new-array v1, v1, [F

    aput p1, v1, v3

    aput p2, v1, v4

    invoke-static {p0, v2, v1}, Landroid/animation/ObjectAnimator;->ofFloat(Ljava/lang/Object;Ljava/lang/String;[F)Landroid/animation/ObjectAnimator;

    move-result-object p0

    new-instance p1, Landroid/animation/AnimatorSet;

    invoke-direct {p1}, Landroid/animation/AnimatorSet;-><init>()V

    invoke-virtual {p1, v0}, Landroid/animation/AnimatorSet;->play(Landroid/animation/Animator;)Landroid/animation/AnimatorSet$Builder;

    move-result-object p2

    invoke-virtual {p2, p0}, Landroid/animation/AnimatorSet$Builder;->with(Landroid/animation/Animator;)Landroid/animation/AnimatorSet$Builder;

    const-wide/16 v0, 0x190

    invoke-virtual {p1, v0, v1}, Landroid/animation/AnimatorSet;->setDuration(J)Landroid/animation/AnimatorSet;

    return-object p1
.end method

.method public static slideIn(Landroid/view/View;Lcom/zopim/android/sdk/anim/AnimatorPack$Direction;)Landroid/animation/Animator;
    .locals 12

    const/4 v0, 0x0

    if-eqz p1, :cond_0

    if-eqz p0, :cond_0

    invoke-virtual {p0}, Landroid/view/View;->getTranslationX()F

    move-result v1

    invoke-virtual {p0}, Landroid/view/View;->getTranslationY()F

    move-result v2

    const/4 v3, 0x2

    new-array v4, v3, [I

    invoke-virtual {p0, v4}, Landroid/view/View;->getLocationOnScreen([I)V

    new-instance v5, Landroid/graphics/Rect;

    const/4 v6, 0x0

    aget v7, v4, v6

    const/4 v8, 0x1

    aget v9, v4, v8

    aget v10, v4, v6

    invoke-virtual {p0}, Landroid/view/View;->getWidth()I

    move-result v11

    add-int/2addr v10, v11

    aget v4, v4, v8

    invoke-virtual {p0}, Landroid/view/View;->getHeight()I

    move-result v11

    add-int/2addr v4, v11

    invoke-direct {v5, v7, v9, v10, v4}, Landroid/graphics/Rect;-><init>(IIII)V

    new-instance v4, Landroid/graphics/Rect;

    invoke-direct {v4}, Landroid/graphics/Rect;-><init>()V

    :try_start_0
    invoke-virtual {p0}, Landroid/view/View;->getContext()Landroid/content/Context;

    move-result-object v7

    check-cast v7, Landroid/app/Activity;

    invoke-virtual {v7}, Landroid/app/Activity;->getWindow()Landroid/view/Window;

    move-result-object v7

    invoke-virtual {v7}, Landroid/view/Window;->getDecorView()Landroid/view/View;

    move-result-object v7

    invoke-virtual {v7, v4}, Landroid/view/View;->getWindowVisibleDisplayFrame(Landroid/graphics/Rect;)V
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception v7

    sget-object v9, Lcom/zopim/android/sdk/anim/AnimatorPack;->TAG:Ljava/lang/String;

    new-instance v10, Ljava/lang/StringBuilder;

    invoke-direct {v10}, Ljava/lang/StringBuilder;-><init>()V

    const-string v11, "Can not get activity visible rectangle, will use phone view. "

    invoke-virtual {v10, v11}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v7}, Ljava/lang/Exception;->getMessage()Ljava/lang/String;

    move-result-object v7

    invoke-virtual {v10, v7}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v10}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v7

    invoke-static {v9, v7}, Landroid/util/Log;->w(Ljava/lang/String;Ljava/lang/String;)I

    invoke-virtual {p0}, Landroid/view/View;->getRootView()Landroid/view/View;

    move-result-object v7

    invoke-virtual {v7, v4}, Landroid/view/View;->getLocalVisibleRect(Landroid/graphics/Rect;)Z

    :goto_0
    sget-object v7, Lcom/zopim/android/sdk/anim/g;->a:[I

    invoke-virtual {p1}, Lcom/zopim/android/sdk/anim/AnimatorPack$Direction;->ordinal()I

    move-result p1

    aget p1, v7, p1

    packed-switch p1, :pswitch_data_0

    goto :goto_2

    :pswitch_0
    const-string p1, "translationY"

    new-array v0, v3, [F

    iget v1, v4, Landroid/graphics/Rect;->bottom:I

    iget v3, v5, Landroid/graphics/Rect;->top:I

    sub-int/2addr v1, v3

    invoke-static {v1}, Ljava/lang/Math;->abs(I)I

    move-result v1

    int-to-float v1, v1

    aput v1, v0, v6

    aput v2, v0, v8

    goto :goto_1

    :pswitch_1
    const-string p1, "translationX"

    new-array v0, v3, [F

    iget v2, v4, Landroid/graphics/Rect;->right:I

    iget v3, v5, Landroid/graphics/Rect;->left:I

    sub-int/2addr v2, v3

    invoke-static {v2}, Ljava/lang/Math;->abs(I)I

    move-result v2

    int-to-float v2, v2

    aput v2, v0, v6

    aput v1, v0, v8

    goto :goto_1

    :pswitch_2
    const-string p1, "translationY"

    new-array v0, v3, [F

    iget v1, v4, Landroid/graphics/Rect;->top:I

    iget v3, v5, Landroid/graphics/Rect;->bottom:I

    sub-int/2addr v1, v3

    invoke-static {v1}, Ljava/lang/Math;->abs(I)I

    move-result v1

    neg-int v1, v1

    int-to-float v1, v1

    aput v1, v0, v6

    aput v2, v0, v8

    goto :goto_1

    :pswitch_3
    const-string p1, "translationX"

    new-array v0, v3, [F

    iget v2, v4, Landroid/graphics/Rect;->left:I

    iget v3, v5, Landroid/graphics/Rect;->right:I

    sub-int/2addr v2, v3

    invoke-static {v2}, Ljava/lang/Math;->abs(I)I

    move-result v2

    neg-int v2, v2

    int-to-float v2, v2

    aput v2, v0, v6

    aput v1, v0, v8

    :goto_1
    invoke-static {p0, p1, v0}, Landroid/animation/ObjectAnimator;->ofFloat(Ljava/lang/Object;Ljava/lang/String;[F)Landroid/animation/ObjectAnimator;

    move-result-object p1

    move-object v0, p1

    :goto_2
    if-eqz v0, :cond_0

    const-wide/16 v1, 0x190

    invoke-virtual {v0, v1, v2}, Landroid/animation/ObjectAnimator;->setDuration(J)Landroid/animation/ObjectAnimator;

    new-instance p1, Lcom/zopim/android/sdk/anim/d;

    invoke-direct {p1, p0}, Lcom/zopim/android/sdk/anim/d;-><init>(Landroid/view/View;)V

    invoke-virtual {v0, p1}, Landroid/animation/ObjectAnimator;->addListener(Landroid/animation/Animator$AnimatorListener;)V

    :cond_0
    return-object v0

    nop

    :pswitch_data_0
    .packed-switch 0x1
        :pswitch_3
        :pswitch_2
        :pswitch_1
        :pswitch_0
    .end packed-switch
.end method

.method public static varargs slideInSequentially(Lcom/zopim/android/sdk/anim/AnimatorPack$Direction;JZZ[Landroid/view/View;)Landroid/animation/Animator;
    .locals 10

    if-eqz p0, :cond_4

    if-nez p5, :cond_0

    goto :goto_1

    :cond_0
    const-wide/16 v0, 0x0

    new-instance v2, Ljava/util/ArrayList;

    invoke-direct {v2}, Ljava/util/ArrayList;-><init>()V

    array-length v3, p5

    const/4 v4, 0x0

    :goto_0
    if-ge v4, v3, :cond_3

    aget-object v5, p5, v4

    new-instance v6, Landroid/animation/AnimatorSet;

    invoke-direct {v6}, Landroid/animation/AnimatorSet;-><init>()V

    invoke-virtual {v6, v0, v1}, Landroid/animation/AnimatorSet;->setStartDelay(J)V

    invoke-static {v5, p0}, Lcom/zopim/android/sdk/anim/AnimatorPack;->slideIn(Landroid/view/View;Lcom/zopim/android/sdk/anim/AnimatorPack$Direction;)Landroid/animation/Animator;

    move-result-object v7

    invoke-virtual {v6, v7}, Landroid/animation/AnimatorSet;->play(Landroid/animation/Animator;)Landroid/animation/AnimatorSet$Builder;

    if-eqz p3, :cond_1

    const-string v7, "alpha"

    const/4 v8, 0x2

    new-array v8, v8, [F

    fill-array-data v8, :array_0

    invoke-static {v5, v7, v8}, Landroid/animation/ObjectAnimator;->ofFloat(Ljava/lang/Object;Ljava/lang/String;[F)Landroid/animation/ObjectAnimator;

    move-result-object v7

    const-wide/16 v8, 0x190

    invoke-virtual {v7, v8, v9}, Landroid/animation/ObjectAnimator;->setDuration(J)Landroid/animation/ObjectAnimator;

    move-result-object v7

    invoke-virtual {v6, v7}, Landroid/animation/AnimatorSet;->play(Landroid/animation/Animator;)Landroid/animation/AnimatorSet$Builder;

    :cond_1
    if-eqz p4, :cond_2

    const/high16 v7, 0x3f000000    # 0.5f

    const/high16 v8, 0x3f800000    # 1.0f

    invoke-static {v5, v7, v8}, Lcom/zopim/android/sdk/anim/AnimatorPack;->scale(Landroid/view/View;FF)Landroid/animation/AnimatorSet;

    move-result-object v5

    invoke-virtual {v6, v5}, Landroid/animation/AnimatorSet;->play(Landroid/animation/Animator;)Landroid/animation/AnimatorSet$Builder;

    :cond_2
    invoke-interface {v2, v6}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    add-long/2addr v0, p1

    add-int/lit8 v4, v4, 0x1

    goto :goto_0

    :cond_3
    new-instance p0, Landroid/animation/AnimatorSet;

    invoke-direct {p0}, Landroid/animation/AnimatorSet;-><init>()V

    invoke-virtual {p0, v2}, Landroid/animation/AnimatorSet;->playTogether(Ljava/util/Collection;)V

    return-object p0

    :cond_4
    :goto_1
    new-instance p0, Landroid/animation/AnimatorSet;

    invoke-direct {p0}, Landroid/animation/AnimatorSet;-><init>()V

    return-object p0

    nop

    :array_0
    .array-data 4
        0x0
        0x3f800000    # 1.0f
    .end array-data
.end method

.method public static slideOut(Landroid/view/View;Lcom/zopim/android/sdk/anim/AnimatorPack$Direction;)Landroid/animation/Animator;
    .locals 12

    const/4 v0, 0x0

    if-eqz p1, :cond_0

    if-eqz p0, :cond_0

    invoke-virtual {p0}, Landroid/view/View;->getTranslationX()F

    move-result v1

    invoke-virtual {p0}, Landroid/view/View;->getTranslationY()F

    move-result v2

    const/4 v3, 0x2

    new-array v4, v3, [I

    invoke-virtual {p0, v4}, Landroid/view/View;->getLocationOnScreen([I)V

    new-instance v5, Landroid/graphics/Rect;

    const/4 v6, 0x0

    aget v7, v4, v6

    const/4 v8, 0x1

    aget v9, v4, v8

    aget v10, v4, v6

    invoke-virtual {p0}, Landroid/view/View;->getWidth()I

    move-result v11

    add-int/2addr v10, v11

    aget v4, v4, v8

    invoke-virtual {p0}, Landroid/view/View;->getHeight()I

    move-result v11

    add-int/2addr v4, v11

    invoke-direct {v5, v7, v9, v10, v4}, Landroid/graphics/Rect;-><init>(IIII)V

    new-instance v4, Landroid/graphics/Rect;

    invoke-direct {v4}, Landroid/graphics/Rect;-><init>()V

    :try_start_0
    invoke-virtual {p0}, Landroid/view/View;->getContext()Landroid/content/Context;

    move-result-object v7

    check-cast v7, Landroid/app/Activity;

    invoke-virtual {v7}, Landroid/app/Activity;->getWindow()Landroid/view/Window;

    move-result-object v7

    invoke-virtual {v7}, Landroid/view/Window;->getDecorView()Landroid/view/View;

    move-result-object v7

    invoke-virtual {v7, v4}, Landroid/view/View;->getWindowVisibleDisplayFrame(Landroid/graphics/Rect;)V
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception v7

    sget-object v9, Lcom/zopim/android/sdk/anim/AnimatorPack;->TAG:Ljava/lang/String;

    new-instance v10, Ljava/lang/StringBuilder;

    invoke-direct {v10}, Ljava/lang/StringBuilder;-><init>()V

    const-string v11, "Can not get activity visible rectangle, will use phone view. "

    invoke-virtual {v10, v11}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v7}, Ljava/lang/Exception;->getMessage()Ljava/lang/String;

    move-result-object v7

    invoke-virtual {v10, v7}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v10}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v7

    invoke-static {v9, v7}, Landroid/util/Log;->w(Ljava/lang/String;Ljava/lang/String;)I

    invoke-virtual {p0}, Landroid/view/View;->getRootView()Landroid/view/View;

    move-result-object v7

    invoke-virtual {v7, v4}, Landroid/view/View;->getLocalVisibleRect(Landroid/graphics/Rect;)Z

    :goto_0
    sget-object v7, Lcom/zopim/android/sdk/anim/g;->a:[I

    invoke-virtual {p1}, Lcom/zopim/android/sdk/anim/AnimatorPack$Direction;->ordinal()I

    move-result p1

    aget p1, v7, p1

    packed-switch p1, :pswitch_data_0

    goto :goto_2

    :pswitch_0
    const-string p1, "translationY"

    new-array v0, v3, [F

    aput v2, v0, v6

    iget v3, v4, Landroid/graphics/Rect;->bottom:I

    iget v4, v5, Landroid/graphics/Rect;->top:I

    sub-int/2addr v3, v4

    invoke-static {v3}, Ljava/lang/Math;->abs(I)I

    move-result v3

    int-to-float v3, v3

    add-float/2addr v3, v2

    aput v3, v0, v8

    goto :goto_1

    :pswitch_1
    const-string p1, "translationX"

    new-array v0, v3, [F

    aput v1, v0, v6

    iget v3, v4, Landroid/graphics/Rect;->right:I

    iget v4, v5, Landroid/graphics/Rect;->left:I

    sub-int/2addr v3, v4

    invoke-static {v3}, Ljava/lang/Math;->abs(I)I

    move-result v3

    int-to-float v3, v3

    add-float/2addr v3, v1

    aput v3, v0, v8

    goto :goto_1

    :pswitch_2
    const-string p1, "translationY"

    new-array v0, v3, [F

    aput v2, v0, v6

    iget v3, v4, Landroid/graphics/Rect;->top:I

    iget v4, v5, Landroid/graphics/Rect;->bottom:I

    sub-int/2addr v3, v4

    invoke-static {v3}, Ljava/lang/Math;->abs(I)I

    move-result v3

    int-to-float v3, v3

    sub-float v3, v2, v3

    aput v3, v0, v8

    goto :goto_1

    :pswitch_3
    const-string p1, "translationX"

    new-array v0, v3, [F

    aput v1, v0, v6

    iget v3, v4, Landroid/graphics/Rect;->left:I

    iget v4, v5, Landroid/graphics/Rect;->right:I

    sub-int/2addr v3, v4

    invoke-static {v3}, Ljava/lang/Math;->abs(I)I

    move-result v3

    int-to-float v3, v3

    sub-float v3, v1, v3

    aput v3, v0, v8

    :goto_1
    invoke-static {p0, p1, v0}, Landroid/animation/ObjectAnimator;->ofFloat(Ljava/lang/Object;Ljava/lang/String;[F)Landroid/animation/ObjectAnimator;

    move-result-object p1

    move-object v0, p1

    :goto_2
    const-wide/16 v3, 0x190

    invoke-virtual {v0, v3, v4}, Landroid/animation/ObjectAnimator;->setDuration(J)Landroid/animation/ObjectAnimator;

    new-instance p1, Lcom/zopim/android/sdk/anim/c;

    invoke-direct {p1, p0, v1, v2}, Lcom/zopim/android/sdk/anim/c;-><init>(Landroid/view/View;FF)V

    invoke-virtual {v0, p1}, Landroid/animation/ObjectAnimator;->addListener(Landroid/animation/Animator$AnimatorListener;)V

    :cond_0
    return-object v0

    nop

    :pswitch_data_0
    .packed-switch 0x1
        :pswitch_3
        :pswitch_2
        :pswitch_1
        :pswitch_0
    .end packed-switch
.end method

.method public static varargs slideOutSequentially(Lcom/zopim/android/sdk/anim/AnimatorPack$Direction;JZZ[Landroid/view/View;)Landroid/animation/Animator;
    .locals 10

    if-eqz p0, :cond_4

    if-nez p5, :cond_0

    goto :goto_1

    :cond_0
    const-wide/16 v0, 0x0

    new-instance v2, Ljava/util/ArrayList;

    invoke-direct {v2}, Ljava/util/ArrayList;-><init>()V

    array-length v3, p5

    const/4 v4, 0x0

    :goto_0
    if-ge v4, v3, :cond_3

    aget-object v5, p5, v4

    new-instance v6, Landroid/animation/AnimatorSet;

    invoke-direct {v6}, Landroid/animation/AnimatorSet;-><init>()V

    invoke-virtual {v6, v0, v1}, Landroid/animation/AnimatorSet;->setStartDelay(J)V

    invoke-static {v5, p0}, Lcom/zopim/android/sdk/anim/AnimatorPack;->slideOut(Landroid/view/View;Lcom/zopim/android/sdk/anim/AnimatorPack$Direction;)Landroid/animation/Animator;

    move-result-object v7

    invoke-virtual {v6, v7}, Landroid/animation/AnimatorSet;->play(Landroid/animation/Animator;)Landroid/animation/AnimatorSet$Builder;

    if-eqz p3, :cond_1

    const-string v7, "alpha"

    const/4 v8, 0x2

    new-array v8, v8, [F

    fill-array-data v8, :array_0

    invoke-static {v5, v7, v8}, Landroid/animation/ObjectAnimator;->ofFloat(Ljava/lang/Object;Ljava/lang/String;[F)Landroid/animation/ObjectAnimator;

    move-result-object v7

    const-wide/16 v8, 0x190

    invoke-virtual {v7, v8, v9}, Landroid/animation/ObjectAnimator;->setDuration(J)Landroid/animation/ObjectAnimator;

    move-result-object v7

    invoke-virtual {v6, v7}, Landroid/animation/AnimatorSet;->play(Landroid/animation/Animator;)Landroid/animation/AnimatorSet$Builder;

    :cond_1
    if-eqz p4, :cond_2

    const/high16 v7, 0x3f800000    # 1.0f

    const/high16 v8, 0x3f000000    # 0.5f

    invoke-static {v5, v7, v8}, Lcom/zopim/android/sdk/anim/AnimatorPack;->scale(Landroid/view/View;FF)Landroid/animation/AnimatorSet;

    move-result-object v5

    invoke-virtual {v6, v5}, Landroid/animation/AnimatorSet;->play(Landroid/animation/Animator;)Landroid/animation/AnimatorSet$Builder;

    :cond_2
    invoke-interface {v2, v6}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    add-long/2addr v0, p1

    add-int/lit8 v4, v4, 0x1

    goto :goto_0

    :cond_3
    new-instance p0, Landroid/animation/AnimatorSet;

    invoke-direct {p0}, Landroid/animation/AnimatorSet;-><init>()V

    invoke-virtual {p0, v2}, Landroid/animation/AnimatorSet;->playTogether(Ljava/util/Collection;)V

    return-object p0

    :cond_4
    :goto_1
    new-instance p0, Landroid/animation/AnimatorSet;

    invoke-direct {p0}, Landroid/animation/AnimatorSet;-><init>()V

    return-object p0

    nop

    :array_0
    .array-data 4
        0x3f800000    # 1.0f
        0x0
    .end array-data
.end method
