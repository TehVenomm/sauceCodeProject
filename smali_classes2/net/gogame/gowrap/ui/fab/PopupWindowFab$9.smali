.class Lnet/gogame/gowrap/ui/fab/PopupWindowFab$9;
.super Ljava/lang/Object;
.source "PopupWindowFab.java"

# interfaces
.implements Landroid/animation/ValueAnimator$AnimatorUpdateListener;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->getInsideAnimation(Landroid/app/Activity;I)Landroid/animation/AnimatorSet;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lnet/gogame/gowrap/ui/fab/PopupWindowFab;

.field final synthetic val$activity:Landroid/app/Activity;


# direct methods
.method constructor <init>(Lnet/gogame/gowrap/ui/fab/PopupWindowFab;Landroid/app/Activity;)V
    .locals 0

    .line 507
    iput-object p1, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab$9;->this$0:Lnet/gogame/gowrap/ui/fab/PopupWindowFab;

    iput-object p2, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab$9;->val$activity:Landroid/app/Activity;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public onAnimationUpdate(Landroid/animation/ValueAnimator;)V
    .locals 1

    .line 511
    iget-object v0, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab$9;->this$0:Lnet/gogame/gowrap/ui/fab/PopupWindowFab;

    invoke-virtual {p1}, Landroid/animation/ValueAnimator;->getAnimatedValue()Ljava/lang/Object;

    move-result-object p1

    check-cast p1, Ljava/lang/Integer;

    invoke-virtual {p1}, Ljava/lang/Integer;->intValue()I

    move-result p1

    invoke-static {v0, p1}, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->access$1802(Lnet/gogame/gowrap/ui/fab/PopupWindowFab;I)I

    .line 512
    iget-object p1, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab$9;->this$0:Lnet/gogame/gowrap/ui/fab/PopupWindowFab;

    iget-object v0, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab$9;->val$activity:Landroid/app/Activity;

    invoke-static {p1, v0}, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->access$1700(Lnet/gogame/gowrap/ui/fab/PopupWindowFab;Landroid/app/Activity;)V

    return-void
.end method
