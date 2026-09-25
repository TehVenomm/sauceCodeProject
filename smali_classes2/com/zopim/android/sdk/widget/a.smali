.class Lcom/zopim/android/sdk/widget/a;
.super Landroid/animation/AnimatorListenerAdapter;


# instance fields
.field final synthetic a:Lcom/zopim/android/sdk/widget/ChatWidgetService;


# direct methods
.method constructor <init>(Lcom/zopim/android/sdk/widget/ChatWidgetService;)V
    .locals 0

    iput-object p1, p0, Lcom/zopim/android/sdk/widget/a;->a:Lcom/zopim/android/sdk/widget/ChatWidgetService;

    invoke-direct {p0}, Landroid/animation/AnimatorListenerAdapter;-><init>()V

    return-void
.end method


# virtual methods
.method public onAnimationEnd(Landroid/animation/Animator;)V
    .locals 1

    invoke-super {p0, p1}, Landroid/animation/AnimatorListenerAdapter;->onAnimationEnd(Landroid/animation/Animator;)V

    iget-object p1, p0, Lcom/zopim/android/sdk/widget/a;->a:Lcom/zopim/android/sdk/widget/ChatWidgetService;

    invoke-static {p1}, Lcom/zopim/android/sdk/widget/ChatWidgetService;->access$000(Lcom/zopim/android/sdk/widget/ChatWidgetService;)Lcom/zopim/android/sdk/chatlog/view/TypingIndicatorView;

    move-result-object p1

    const/high16 v0, 0x3f800000    # 1.0f

    invoke-virtual {p1, v0}, Lcom/zopim/android/sdk/chatlog/view/TypingIndicatorView;->setScaleX(F)V

    iget-object p1, p0, Lcom/zopim/android/sdk/widget/a;->a:Lcom/zopim/android/sdk/widget/ChatWidgetService;

    invoke-static {p1}, Lcom/zopim/android/sdk/widget/ChatWidgetService;->access$000(Lcom/zopim/android/sdk/widget/ChatWidgetService;)Lcom/zopim/android/sdk/chatlog/view/TypingIndicatorView;

    move-result-object p1

    invoke-virtual {p1, v0}, Lcom/zopim/android/sdk/chatlog/view/TypingIndicatorView;->setScaleY(F)V

    return-void
.end method
