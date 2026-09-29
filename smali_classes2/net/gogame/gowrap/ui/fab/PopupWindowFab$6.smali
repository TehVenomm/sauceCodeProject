.class Lnet/gogame/gowrap/ui/fab/PopupWindowFab$6;
.super Ljava/lang/Object;
.source "PopupWindowFab.java"

# interfaces
.implements Ljava/lang/Runnable;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->show(Landroid/app/Activity;)V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lnet/gogame/gowrap/ui/fab/PopupWindowFab;

.field final synthetic val$activity:Landroid/app/Activity;

.field final synthetic val$leafParent:Landroid/view/ViewGroup;

.field final synthetic val$metrics:Landroid/util/DisplayMetrics;


# direct methods
.method constructor <init>(Lnet/gogame/gowrap/ui/fab/PopupWindowFab;Landroid/util/DisplayMetrics;Landroid/view/ViewGroup;Landroid/app/Activity;)V
    .locals 0

    .line 365
    iput-object p1, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab$6;->this$0:Lnet/gogame/gowrap/ui/fab/PopupWindowFab;

    iput-object p2, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab$6;->val$metrics:Landroid/util/DisplayMetrics;

    iput-object p3, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab$6;->val$leafParent:Landroid/view/ViewGroup;

    iput-object p4, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab$6;->val$activity:Landroid/app/Activity;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public run()V
    .locals 5

    .line 370
    :try_start_0
    iget-object v0, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab$6;->this$0:Lnet/gogame/gowrap/ui/fab/PopupWindowFab;

    invoke-static {v0}, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->access$400(Lnet/gogame/gowrap/ui/fab/PopupWindowFab;)Landroid/widget/PopupWindow;

    move-result-object v0

    if-nez v0, :cond_0

    return-void

    .line 373
    :cond_0
    sget-object v0, Lnet/gogame/gowrap/integrations/core/Wrapper;->INSTANCE:Lnet/gogame/gowrap/integrations/core/Wrapper;

    invoke-virtual {v0}, Lnet/gogame/gowrap/integrations/core/Wrapper;->isSlideOut()Z

    move-result v0

    if-eqz v0, :cond_1

    .line 374
    iget-object v0, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab$6;->this$0:Lnet/gogame/gowrap/ui/fab/PopupWindowFab;

    iget-object v1, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab$6;->val$metrics:Landroid/util/DisplayMetrics;

    iget v1, v1, Landroid/util/DisplayMetrics;->widthPixels:I

    neg-int v1, v1

    invoke-static {v0, v1}, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->access$1802(Lnet/gogame/gowrap/ui/fab/PopupWindowFab;I)I

    .line 376
    :cond_1
    iget-object v0, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab$6;->this$0:Lnet/gogame/gowrap/ui/fab/PopupWindowFab;

    invoke-static {v0}, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->access$400(Lnet/gogame/gowrap/ui/fab/PopupWindowFab;)Landroid/widget/PopupWindow;

    move-result-object v0

    iget-object v1, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab$6;->val$leafParent:Landroid/view/ViewGroup;

    const/4 v2, 0x0

    iget-object v3, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab$6;->this$0:Lnet/gogame/gowrap/ui/fab/PopupWindowFab;

    invoke-static {v3}, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->access$500(Lnet/gogame/gowrap/ui/fab/PopupWindowFab;)I

    move-result v3

    iget-object v4, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab$6;->this$0:Lnet/gogame/gowrap/ui/fab/PopupWindowFab;

    invoke-static {v4}, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->access$600(Lnet/gogame/gowrap/ui/fab/PopupWindowFab;)I

    move-result v4

    invoke-virtual {v0, v1, v2, v3, v4}, Landroid/widget/PopupWindow;->showAtLocation(Landroid/view/View;III)V

    .line 377
    sget-object v0, Lnet/gogame/gowrap/integrations/core/Wrapper;->INSTANCE:Lnet/gogame/gowrap/integrations/core/Wrapper;

    invoke-virtual {v0}, Lnet/gogame/gowrap/integrations/core/Wrapper;->isSlideOut()Z

    move-result v0

    if-nez v0, :cond_2

    sget-object v0, Lnet/gogame/gowrap/integrations/core/Wrapper;->INSTANCE:Lnet/gogame/gowrap/integrations/core/Wrapper;

    invoke-virtual {v0}, Lnet/gogame/gowrap/integrations/core/Wrapper;->isSlideIn()Z

    move-result v0

    if-eqz v0, :cond_3

    .line 378
    :cond_2
    new-instance v0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab$6$1;

    invoke-direct {v0, p0}, Lnet/gogame/gowrap/ui/fab/PopupWindowFab$6$1;-><init>(Lnet/gogame/gowrap/ui/fab/PopupWindowFab$6;)V

    .line 397
    iget-object v1, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab$6;->this$0:Lnet/gogame/gowrap/ui/fab/PopupWindowFab;

    iget-object v2, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab$6;->val$activity:Landroid/app/Activity;

    const-wide/16 v3, 0x64

    invoke-static {v1, v2, v0, v3, v4}, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->access$900(Lnet/gogame/gowrap/ui/fab/PopupWindowFab;Landroid/app/Activity;Ljava/lang/Runnable;J)V
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception v0

    const-string v1, "goWrap"

    const-string v2, "Exception"

    .line 400
    invoke-static {v1, v2, v0}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)I

    :cond_3
    :goto_0
    return-void
.end method
