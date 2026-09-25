.class Lnet/gogame/gowrap/ui/fab/PopupWindowFab$7;
.super Ljava/lang/Object;
.source "PopupWindowFab.java"

# interfaces
.implements Landroid/animation/Animator$AnimatorListener;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->getAnimationListener(Landroid/app/Activity;)Landroid/animation/Animator$AnimatorListener;
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

    .line 408
    iput-object p1, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab$7;->this$0:Lnet/gogame/gowrap/ui/fab/PopupWindowFab;

    iput-object p2, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab$7;->val$activity:Landroid/app/Activity;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public onAnimationCancel(Landroid/animation/Animator;)V
    .locals 0

    return-void
.end method

.method public onAnimationEnd(Landroid/animation/Animator;)V
    .locals 1

    .line 417
    iget-object p1, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab$7;->this$0:Lnet/gogame/gowrap/ui/fab/PopupWindowFab;

    iget-object v0, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab$7;->val$activity:Landroid/app/Activity;

    invoke-static {p1, v0}, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->access$2500(Lnet/gogame/gowrap/ui/fab/PopupWindowFab;Landroid/app/Activity;)V

    return-void
.end method

.method public onAnimationRepeat(Landroid/animation/Animator;)V
    .locals 0

    return-void
.end method

.method public onAnimationStart(Landroid/animation/Animator;)V
    .locals 0

    return-void
.end method
