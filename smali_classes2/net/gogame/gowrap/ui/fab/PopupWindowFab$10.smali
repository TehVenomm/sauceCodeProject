.class Lnet/gogame/gowrap/ui/fab/PopupWindowFab$10;
.super Ljava/lang/Object;
.source "PopupWindowFab.java"

# interfaces
.implements Ljava/lang/Runnable;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->hide(Landroid/app/Activity;)V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lnet/gogame/gowrap/ui/fab/PopupWindowFab;


# direct methods
.method constructor <init>(Lnet/gogame/gowrap/ui/fab/PopupWindowFab;)V
    .locals 0

    .line 554
    iput-object p1, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab$10;->this$0:Lnet/gogame/gowrap/ui/fab/PopupWindowFab;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public run()V
    .locals 3

    .line 559
    :try_start_0
    iget-object v0, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab$10;->this$0:Lnet/gogame/gowrap/ui/fab/PopupWindowFab;

    invoke-static {v0}, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->access$400(Lnet/gogame/gowrap/ui/fab/PopupWindowFab;)Landroid/widget/PopupWindow;

    move-result-object v0

    if-eqz v0, :cond_0

    .line 560
    iget-object v0, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab$10;->this$0:Lnet/gogame/gowrap/ui/fab/PopupWindowFab;

    invoke-static {v0}, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->access$400(Lnet/gogame/gowrap/ui/fab/PopupWindowFab;)Landroid/widget/PopupWindow;

    move-result-object v0

    invoke-virtual {v0}, Landroid/widget/PopupWindow;->dismiss()V

    .line 561
    iget-object v0, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab$10;->this$0:Lnet/gogame/gowrap/ui/fab/PopupWindowFab;

    const/4 v1, 0x0

    invoke-static {v0, v1}, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->access$402(Lnet/gogame/gowrap/ui/fab/PopupWindowFab;Landroid/widget/PopupWindow;)Landroid/widget/PopupWindow;
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception v0

    const-string v1, "goWrap"

    const-string v2, "Exception"

    .line 564
    invoke-static {v1, v2, v0}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)I

    :cond_0
    :goto_0
    return-void
.end method
