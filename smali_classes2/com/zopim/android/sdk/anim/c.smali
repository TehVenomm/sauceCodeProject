.class final Lcom/zopim/android/sdk/anim/c;
.super Landroid/animation/AnimatorListenerAdapter;


# instance fields
.field final synthetic a:Landroid/view/View;

.field final synthetic b:F

.field final synthetic c:F


# direct methods
.method constructor <init>(Landroid/view/View;FF)V
    .locals 0

    iput-object p1, p0, Lcom/zopim/android/sdk/anim/c;->a:Landroid/view/View;

    iput p2, p0, Lcom/zopim/android/sdk/anim/c;->b:F

    iput p3, p0, Lcom/zopim/android/sdk/anim/c;->c:F

    invoke-direct {p0}, Landroid/animation/AnimatorListenerAdapter;-><init>()V

    return-void
.end method


# virtual methods
.method public onAnimationEnd(Landroid/animation/Animator;)V
    .locals 1

    invoke-super {p0, p1}, Landroid/animation/AnimatorListenerAdapter;->onAnimationEnd(Landroid/animation/Animator;)V

    iget-object p1, p0, Lcom/zopim/android/sdk/anim/c;->a:Landroid/view/View;

    const/4 v0, 0x4

    invoke-virtual {p1, v0}, Landroid/view/View;->setVisibility(I)V

    iget-object p1, p0, Lcom/zopim/android/sdk/anim/c;->a:Landroid/view/View;

    iget v0, p0, Lcom/zopim/android/sdk/anim/c;->b:F

    invoke-virtual {p1, v0}, Landroid/view/View;->setTranslationX(F)V

    iget-object p1, p0, Lcom/zopim/android/sdk/anim/c;->a:Landroid/view/View;

    iget v0, p0, Lcom/zopim/android/sdk/anim/c;->c:F

    invoke-virtual {p1, v0}, Landroid/view/View;->setTranslationY(F)V

    return-void
.end method

.method public onAnimationStart(Landroid/animation/Animator;)V
    .locals 1

    invoke-super {p0, p1}, Landroid/animation/AnimatorListenerAdapter;->onAnimationStart(Landroid/animation/Animator;)V

    iget-object p1, p0, Lcom/zopim/android/sdk/anim/c;->a:Landroid/view/View;

    const/4 v0, 0x0

    invoke-virtual {p1, v0}, Landroid/view/View;->setVisibility(I)V

    return-void
.end method
