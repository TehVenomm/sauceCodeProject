.class Lnet/gogame/gowrap/ui/fab/PopupWindowFab$8;
.super Ljava/lang/Object;
.source "PopupWindowFab.java"

# interfaces
.implements Landroid/animation/ValueAnimator$AnimatorUpdateListener;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->getAnimation(Landroid/app/Activity;I)Landroid/animation/AnimatorSet;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lnet/gogame/gowrap/ui/fab/PopupWindowFab;

.field final synthetic val$activity:Landroid/app/Activity;

.field final synthetic val$width:I


# direct methods
.method constructor <init>(Lnet/gogame/gowrap/ui/fab/PopupWindowFab;ILandroid/app/Activity;)V
    .locals 0

    .line 434
    iput-object p1, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab$8;->this$0:Lnet/gogame/gowrap/ui/fab/PopupWindowFab;

    iput p2, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab$8;->val$width:I

    iput-object p3, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab$8;->val$activity:Landroid/app/Activity;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public onAnimationUpdate(Landroid/animation/ValueAnimator;)V
    .locals 3

    .line 438
    iget-object v0, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab$8;->this$0:Lnet/gogame/gowrap/ui/fab/PopupWindowFab;

    invoke-virtual {p1}, Landroid/animation/ValueAnimator;->getAnimatedValue()Ljava/lang/Object;

    move-result-object p1

    check-cast p1, Ljava/lang/Integer;

    invoke-virtual {p1}, Ljava/lang/Integer;->intValue()I

    move-result p1

    invoke-static {v0, p1}, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->access$1802(Lnet/gogame/gowrap/ui/fab/PopupWindowFab;I)I

    .line 439
    iget-object p1, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab$8;->this$0:Lnet/gogame/gowrap/ui/fab/PopupWindowFab;

    invoke-static {p1}, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->access$000(Lnet/gogame/gowrap/ui/fab/PopupWindowFab;)Landroid/view/View;

    move-result-object p1

    if-eqz p1, :cond_0

    .line 440
    iget-object p1, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab$8;->this$0:Lnet/gogame/gowrap/ui/fab/PopupWindowFab;

    invoke-static {p1}, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->access$000(Lnet/gogame/gowrap/ui/fab/PopupWindowFab;)Landroid/view/View;

    move-result-object p1

    iget v0, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab$8;->val$width:I

    div-int/lit8 v0, v0, 0x2

    iget v1, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab$8;->val$width:I

    div-int/lit8 v1, v1, 0x2

    const/4 v2, 0x0

    invoke-virtual {p1, v0, v2, v1, v2}, Landroid/view/View;->setPadding(IIII)V

    .line 442
    :cond_0
    iget-object p1, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab$8;->this$0:Lnet/gogame/gowrap/ui/fab/PopupWindowFab;

    iget-object v0, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab$8;->val$activity:Landroid/app/Activity;

    invoke-static {p1, v0}, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->access$1700(Lnet/gogame/gowrap/ui/fab/PopupWindowFab;Landroid/app/Activity;)V

    return-void
.end method
