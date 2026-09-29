.class Lnet/gogame/gowrap/ui/fab/PopupWindowFab$2;
.super Ljava/lang/Object;
.source "PopupWindowFab.java"

# interfaces
.implements Ljava/lang/Runnable;


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lnet/gogame/gowrap/ui/fab/PopupWindowFab;
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

    .line 77
    iput-object p1, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab$2;->this$0:Lnet/gogame/gowrap/ui/fab/PopupWindowFab;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public run()V
    .locals 4

    .line 82
    :try_start_0
    iget-object v0, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab$2;->this$0:Lnet/gogame/gowrap/ui/fab/PopupWindowFab;

    invoke-static {v0}, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->access$400(Lnet/gogame/gowrap/ui/fab/PopupWindowFab;)Landroid/widget/PopupWindow;

    move-result-object v0

    if-eqz v0, :cond_0

    .line 83
    iget-object v0, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab$2;->this$0:Lnet/gogame/gowrap/ui/fab/PopupWindowFab;

    invoke-static {v0}, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->access$400(Lnet/gogame/gowrap/ui/fab/PopupWindowFab;)Landroid/widget/PopupWindow;

    move-result-object v0

    iget-object v1, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab$2;->this$0:Lnet/gogame/gowrap/ui/fab/PopupWindowFab;

    invoke-static {v1}, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->access$500(Lnet/gogame/gowrap/ui/fab/PopupWindowFab;)I

    move-result v1

    iget-object v2, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab$2;->this$0:Lnet/gogame/gowrap/ui/fab/PopupWindowFab;

    invoke-static {v2}, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->access$600(Lnet/gogame/gowrap/ui/fab/PopupWindowFab;)I

    move-result v2

    const/4 v3, -0x1

    invoke-virtual {v0, v1, v2, v3, v3}, Landroid/widget/PopupWindow;->update(IIII)V
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception v0

    const-string v1, "goWrap"

    const-string v2, "Exception"

    .line 86
    invoke-static {v1, v2, v0}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)I

    :cond_0
    :goto_0
    return-void
.end method
