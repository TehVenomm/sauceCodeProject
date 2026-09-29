.class Lnet/gogame/gowrap/ui/fab/PopupWindowFab$4;
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


# direct methods
.method constructor <init>(Lnet/gogame/gowrap/ui/fab/PopupWindowFab;Landroid/app/Activity;)V
    .locals 0

    .line 204
    iput-object p1, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab$4;->this$0:Lnet/gogame/gowrap/ui/fab/PopupWindowFab;

    iput-object p2, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab$4;->val$activity:Landroid/app/Activity;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public run()V
    .locals 3

    .line 209
    :try_start_0
    iget-object v0, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab$4;->this$0:Lnet/gogame/gowrap/ui/fab/PopupWindowFab;

    iget-object v1, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab$4;->val$activity:Landroid/app/Activity;

    invoke-virtual {v0, v1}, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->show(Landroid/app/Activity;)V
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception v0

    const-string v1, "goWrap"

    const-string v2, "Exception"

    .line 211
    invoke-static {v1, v2, v0}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)I

    :goto_0
    return-void
.end method
