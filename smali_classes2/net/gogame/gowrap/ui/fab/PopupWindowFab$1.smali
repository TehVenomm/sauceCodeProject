.class Lnet/gogame/gowrap/ui/fab/PopupWindowFab$1;
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

    .line 47
    iput-object p1, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab$1;->this$0:Lnet/gogame/gowrap/ui/fab/PopupWindowFab;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public run()V
    .locals 3

    .line 52
    :try_start_0
    iget-object v0, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab$1;->this$0:Lnet/gogame/gowrap/ui/fab/PopupWindowFab;

    invoke-static {v0}, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->access$000(Lnet/gogame/gowrap/ui/fab/PopupWindowFab;)Landroid/view/View;

    move-result-object v0

    if-eqz v0, :cond_1

    .line 53
    sget-object v0, Lnet/gogame/gowrap/integrations/core/Wrapper;->INSTANCE:Lnet/gogame/gowrap/integrations/core/Wrapper;

    invoke-virtual {v0}, Lnet/gogame/gowrap/integrations/core/Wrapper;->isServerDown()Z

    move-result v0

    const/4 v1, 0x0

    const/4 v2, 0x1

    if-eqz v0, :cond_0

    .line 54
    iget-object v0, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab$1;->this$0:Lnet/gogame/gowrap/ui/fab/PopupWindowFab;

    invoke-static {v0}, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->access$000(Lnet/gogame/gowrap/ui/fab/PopupWindowFab;)Landroid/view/View;

    move-result-object v0

    check-cast v0, Landroid/widget/RelativeLayout;

    invoke-virtual {v0, v2}, Landroid/widget/RelativeLayout;->getChildAt(I)Landroid/view/View;

    move-result-object v0

    invoke-virtual {v0, v1}, Landroid/view/View;->setVisibility(I)V

    .line 55
    iget-object v0, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab$1;->this$0:Lnet/gogame/gowrap/ui/fab/PopupWindowFab;

    invoke-static {v0}, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->access$100(Lnet/gogame/gowrap/ui/fab/PopupWindowFab;)Lnet/gogame/gowrap/ui/fab/PopupWindowFab;

    move-result-object v0

    invoke-static {v0, v2}, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->access$202(Lnet/gogame/gowrap/ui/fab/PopupWindowFab;Z)Z

    .line 56
    iget-object v0, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab$1;->this$0:Lnet/gogame/gowrap/ui/fab/PopupWindowFab;

    invoke-static {v0}, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->access$100(Lnet/gogame/gowrap/ui/fab/PopupWindowFab;)Lnet/gogame/gowrap/ui/fab/PopupWindowFab;

    move-result-object v0

    invoke-static {v0, v2}, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->access$302(Lnet/gogame/gowrap/ui/fab/PopupWindowFab;Z)Z

    goto :goto_0

    .line 58
    :cond_0
    iget-object v0, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab$1;->this$0:Lnet/gogame/gowrap/ui/fab/PopupWindowFab;

    invoke-static {v0}, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->access$000(Lnet/gogame/gowrap/ui/fab/PopupWindowFab;)Landroid/view/View;

    move-result-object v0

    check-cast v0, Landroid/widget/RelativeLayout;

    invoke-virtual {v0, v2}, Landroid/widget/RelativeLayout;->getChildAt(I)Landroid/view/View;

    move-result-object v0

    const/16 v2, 0x8

    invoke-virtual {v0, v2}, Landroid/view/View;->setVisibility(I)V

    .line 59
    iget-object v0, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab$1;->this$0:Lnet/gogame/gowrap/ui/fab/PopupWindowFab;

    invoke-static {v0}, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->access$100(Lnet/gogame/gowrap/ui/fab/PopupWindowFab;)Lnet/gogame/gowrap/ui/fab/PopupWindowFab;

    move-result-object v0

    invoke-static {v0, v1}, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->access$202(Lnet/gogame/gowrap/ui/fab/PopupWindowFab;Z)Z

    .line 60
    iget-object v0, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab$1;->this$0:Lnet/gogame/gowrap/ui/fab/PopupWindowFab;

    invoke-static {v0}, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->access$100(Lnet/gogame/gowrap/ui/fab/PopupWindowFab;)Lnet/gogame/gowrap/ui/fab/PopupWindowFab;

    move-result-object v0

    invoke-static {v0, v1}, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->access$302(Lnet/gogame/gowrap/ui/fab/PopupWindowFab;Z)Z
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception v0

    const-string v1, "goWrap"

    const-string v2, "Exception"

    .line 64
    invoke-static {v1, v2, v0}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)I

    :cond_1
    :goto_0
    return-void
.end method
