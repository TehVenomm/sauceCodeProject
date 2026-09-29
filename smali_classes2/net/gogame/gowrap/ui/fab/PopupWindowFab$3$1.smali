.class Lnet/gogame/gowrap/ui/fab/PopupWindowFab$3$1;
.super Ljava/lang/Object;
.source "PopupWindowFab.java"

# interfaces
.implements Ljava/lang/Runnable;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lnet/gogame/gowrap/ui/fab/PopupWindowFab$3;->run()V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$1:Lnet/gogame/gowrap/ui/fab/PopupWindowFab$3;


# direct methods
.method constructor <init>(Lnet/gogame/gowrap/ui/fab/PopupWindowFab$3;)V
    .locals 0

    .line 148
    iput-object p1, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab$3$1;->this$1:Lnet/gogame/gowrap/ui/fab/PopupWindowFab$3;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public run()V
    .locals 5

    .line 152
    new-instance v0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab$3$1$1;

    invoke-direct {v0, p0}, Lnet/gogame/gowrap/ui/fab/PopupWindowFab$3$1$1;-><init>(Lnet/gogame/gowrap/ui/fab/PopupWindowFab$3$1;)V

    .line 171
    iget-object v1, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab$3$1;->this$1:Lnet/gogame/gowrap/ui/fab/PopupWindowFab$3;

    iget-object v1, v1, Lnet/gogame/gowrap/ui/fab/PopupWindowFab$3;->this$0:Lnet/gogame/gowrap/ui/fab/PopupWindowFab;

    iget-object v2, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab$3$1;->this$1:Lnet/gogame/gowrap/ui/fab/PopupWindowFab$3;

    iget-object v2, v2, Lnet/gogame/gowrap/ui/fab/PopupWindowFab$3;->val$activity:Landroid/app/Activity;

    const-wide/16 v3, 0x64

    invoke-static {v1, v2, v0, v3, v4}, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->access$900(Lnet/gogame/gowrap/ui/fab/PopupWindowFab;Landroid/app/Activity;Ljava/lang/Runnable;J)V

    return-void
.end method
