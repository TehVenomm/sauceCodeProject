.class Lnet/gogame/gowrap/ui/fab/PopupWindowFab$3$1$1;
.super Ljava/lang/Object;
.source "PopupWindowFab.java"

# interfaces
.implements Ljava/lang/Runnable;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lnet/gogame/gowrap/ui/fab/PopupWindowFab$3$1;->run()V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$2:Lnet/gogame/gowrap/ui/fab/PopupWindowFab$3$1;


# direct methods
.method constructor <init>(Lnet/gogame/gowrap/ui/fab/PopupWindowFab$3$1;)V
    .locals 0

    .line 152
    iput-object p1, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab$3$1$1;->this$2:Lnet/gogame/gowrap/ui/fab/PopupWindowFab$3$1;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public run()V
    .locals 4

    .line 157
    :try_start_0
    iget-object v0, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab$3$1$1;->this$2:Lnet/gogame/gowrap/ui/fab/PopupWindowFab$3$1;

    iget-object v0, v0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab$3$1;->this$1:Lnet/gogame/gowrap/ui/fab/PopupWindowFab$3;

    iget-object v0, v0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab$3;->this$0:Lnet/gogame/gowrap/ui/fab/PopupWindowFab;

    invoke-static {v0}, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->access$000(Lnet/gogame/gowrap/ui/fab/PopupWindowFab;)Landroid/view/View;

    move-result-object v0

    if-nez v0, :cond_0

    return-void

    .line 160
    :cond_0
    iget-object v0, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab$3$1$1;->this$2:Lnet/gogame/gowrap/ui/fab/PopupWindowFab$3$1;

    iget-object v0, v0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab$3$1;->this$1:Lnet/gogame/gowrap/ui/fab/PopupWindowFab$3;

    iget-object v0, v0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab$3;->this$0:Lnet/gogame/gowrap/ui/fab/PopupWindowFab;

    invoke-static {v0}, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->access$000(Lnet/gogame/gowrap/ui/fab/PopupWindowFab;)Landroid/view/View;

    move-result-object v0

    invoke-virtual {v0}, Landroid/view/View;->getWidth()I

    move-result v0

    if-lez v0, :cond_1

    .line 161
    iget-object v0, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab$3$1$1;->this$2:Lnet/gogame/gowrap/ui/fab/PopupWindowFab$3$1;

    iget-object v0, v0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab$3$1;->this$1:Lnet/gogame/gowrap/ui/fab/PopupWindowFab$3;

    iget-object v0, v0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab$3;->this$0:Lnet/gogame/gowrap/ui/fab/PopupWindowFab;

    iget-object v1, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab$3$1$1;->this$2:Lnet/gogame/gowrap/ui/fab/PopupWindowFab$3$1;

    iget-object v1, v1, Lnet/gogame/gowrap/ui/fab/PopupWindowFab$3$1;->this$1:Lnet/gogame/gowrap/ui/fab/PopupWindowFab$3;

    iget-object v1, v1, Lnet/gogame/gowrap/ui/fab/PopupWindowFab$3;->this$0:Lnet/gogame/gowrap/ui/fab/PopupWindowFab;

    invoke-static {v1}, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->access$800(Lnet/gogame/gowrap/ui/fab/PopupWindowFab;)Landroid/animation/AnimatorSet;

    move-result-object v1

    invoke-static {v0, v1}, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->access$702(Lnet/gogame/gowrap/ui/fab/PopupWindowFab;Landroid/animation/AnimatorSet;)Landroid/animation/AnimatorSet;

    .line 162
    iget-object v0, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab$3$1$1;->this$2:Lnet/gogame/gowrap/ui/fab/PopupWindowFab$3$1;

    iget-object v0, v0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab$3$1;->this$1:Lnet/gogame/gowrap/ui/fab/PopupWindowFab$3;

    iget-object v0, v0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab$3;->this$0:Lnet/gogame/gowrap/ui/fab/PopupWindowFab;

    invoke-static {v0}, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->access$700(Lnet/gogame/gowrap/ui/fab/PopupWindowFab;)Landroid/animation/AnimatorSet;

    move-result-object v0

    invoke-virtual {v0}, Landroid/animation/AnimatorSet;->start()V

    goto :goto_0

    .line 164
    :cond_1
    iget-object v0, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab$3$1$1;->this$2:Lnet/gogame/gowrap/ui/fab/PopupWindowFab$3$1;

    iget-object v0, v0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab$3$1;->this$1:Lnet/gogame/gowrap/ui/fab/PopupWindowFab$3;

    iget-object v0, v0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab$3;->this$0:Lnet/gogame/gowrap/ui/fab/PopupWindowFab;

    iget-object v1, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab$3$1$1;->this$2:Lnet/gogame/gowrap/ui/fab/PopupWindowFab$3$1;

    iget-object v1, v1, Lnet/gogame/gowrap/ui/fab/PopupWindowFab$3$1;->this$1:Lnet/gogame/gowrap/ui/fab/PopupWindowFab$3;

    iget-object v1, v1, Lnet/gogame/gowrap/ui/fab/PopupWindowFab$3;->val$activity:Landroid/app/Activity;

    const-wide/16 v2, 0x64

    invoke-static {v0, v1, p0, v2, v3}, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->access$900(Lnet/gogame/gowrap/ui/fab/PopupWindowFab;Landroid/app/Activity;Ljava/lang/Runnable;J)V
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception v0

    const-string v1, "goWrap"

    const-string v2, "Exception"

    .line 167
    invoke-static {v1, v2, v0}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)I

    :goto_0
    return-void
.end method
