.class Lnet/gogame/gowrap/ui/fab/PopupWindowFab$6$1;
.super Ljava/lang/Object;
.source "PopupWindowFab.java"

# interfaces
.implements Ljava/lang/Runnable;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lnet/gogame/gowrap/ui/fab/PopupWindowFab$6;->run()V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$1:Lnet/gogame/gowrap/ui/fab/PopupWindowFab$6;


# direct methods
.method constructor <init>(Lnet/gogame/gowrap/ui/fab/PopupWindowFab$6;)V
    .locals 0

    .line 378
    iput-object p1, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab$6$1;->this$1:Lnet/gogame/gowrap/ui/fab/PopupWindowFab$6;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public run()V
    .locals 4

    .line 383
    :try_start_0
    iget-object v0, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab$6$1;->this$1:Lnet/gogame/gowrap/ui/fab/PopupWindowFab$6;

    iget-object v0, v0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab$6;->this$0:Lnet/gogame/gowrap/ui/fab/PopupWindowFab;

    invoke-static {v0}, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->access$000(Lnet/gogame/gowrap/ui/fab/PopupWindowFab;)Landroid/view/View;

    move-result-object v0

    if-nez v0, :cond_0

    return-void

    .line 386
    :cond_0
    iget-object v0, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab$6$1;->this$1:Lnet/gogame/gowrap/ui/fab/PopupWindowFab$6;

    iget-object v0, v0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab$6;->this$0:Lnet/gogame/gowrap/ui/fab/PopupWindowFab;

    invoke-static {v0}, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->access$000(Lnet/gogame/gowrap/ui/fab/PopupWindowFab;)Landroid/view/View;

    move-result-object v0

    invoke-virtual {v0}, Landroid/view/View;->getWidth()I

    move-result v0

    if-lez v0, :cond_1

    .line 387
    iget-object v0, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab$6$1;->this$1:Lnet/gogame/gowrap/ui/fab/PopupWindowFab$6;

    iget-object v0, v0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab$6;->this$0:Lnet/gogame/gowrap/ui/fab/PopupWindowFab;

    iget-object v1, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab$6$1;->this$1:Lnet/gogame/gowrap/ui/fab/PopupWindowFab$6;

    iget-object v1, v1, Lnet/gogame/gowrap/ui/fab/PopupWindowFab$6;->this$0:Lnet/gogame/gowrap/ui/fab/PopupWindowFab;

    iget-object v2, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab$6$1;->this$1:Lnet/gogame/gowrap/ui/fab/PopupWindowFab$6;

    iget-object v2, v2, Lnet/gogame/gowrap/ui/fab/PopupWindowFab$6;->val$activity:Landroid/app/Activity;

    iget-object v3, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab$6$1;->this$1:Lnet/gogame/gowrap/ui/fab/PopupWindowFab$6;

    iget-object v3, v3, Lnet/gogame/gowrap/ui/fab/PopupWindowFab$6;->this$0:Lnet/gogame/gowrap/ui/fab/PopupWindowFab;

    invoke-static {v3}, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->access$000(Lnet/gogame/gowrap/ui/fab/PopupWindowFab;)Landroid/view/View;

    move-result-object v3

    invoke-virtual {v3}, Landroid/view/View;->getWidth()I

    move-result v3

    invoke-static {v1, v2, v3}, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->access$2400(Lnet/gogame/gowrap/ui/fab/PopupWindowFab;Landroid/app/Activity;I)Landroid/animation/AnimatorSet;

    move-result-object v1

    invoke-static {v0, v1}, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->access$702(Lnet/gogame/gowrap/ui/fab/PopupWindowFab;Landroid/animation/AnimatorSet;)Landroid/animation/AnimatorSet;

    .line 388
    iget-object v0, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab$6$1;->this$1:Lnet/gogame/gowrap/ui/fab/PopupWindowFab$6;

    iget-object v0, v0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab$6;->this$0:Lnet/gogame/gowrap/ui/fab/PopupWindowFab;

    invoke-static {v0}, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->access$700(Lnet/gogame/gowrap/ui/fab/PopupWindowFab;)Landroid/animation/AnimatorSet;

    move-result-object v0

    invoke-virtual {v0}, Landroid/animation/AnimatorSet;->start()V

    goto :goto_0

    .line 390
    :cond_1
    iget-object v0, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab$6$1;->this$1:Lnet/gogame/gowrap/ui/fab/PopupWindowFab$6;

    iget-object v0, v0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab$6;->this$0:Lnet/gogame/gowrap/ui/fab/PopupWindowFab;

    iget-object v1, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab$6$1;->this$1:Lnet/gogame/gowrap/ui/fab/PopupWindowFab$6;

    iget-object v1, v1, Lnet/gogame/gowrap/ui/fab/PopupWindowFab$6;->val$activity:Landroid/app/Activity;

    const-wide/16 v2, 0x64

    invoke-static {v0, v1, p0, v2, v3}, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->access$900(Lnet/gogame/gowrap/ui/fab/PopupWindowFab;Landroid/app/Activity;Ljava/lang/Runnable;J)V
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception v0

    const-string v1, "goWrap"

    const-string v2, "Exception"

    .line 393
    invoke-static {v1, v2, v0}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)I

    :goto_0
    return-void
.end method
