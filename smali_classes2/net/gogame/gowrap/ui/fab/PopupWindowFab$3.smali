.class Lnet/gogame/gowrap/ui/fab/PopupWindowFab$3;
.super Ljava/util/TimerTask;
.source "PopupWindowFab.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->setTimerForFab(Landroid/app/Activity;)V
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

    .line 146
    iput-object p1, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab$3;->this$0:Lnet/gogame/gowrap/ui/fab/PopupWindowFab;

    iput-object p2, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab$3;->val$activity:Landroid/app/Activity;

    invoke-direct {p0}, Ljava/util/TimerTask;-><init>()V

    return-void
.end method


# virtual methods
.method public run()V
    .locals 3

    .line 148
    iget-object v0, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab$3;->this$0:Lnet/gogame/gowrap/ui/fab/PopupWindowFab;

    iget-object v1, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab$3;->val$activity:Landroid/app/Activity;

    new-instance v2, Lnet/gogame/gowrap/ui/fab/PopupWindowFab$3$1;

    invoke-direct {v2, p0}, Lnet/gogame/gowrap/ui/fab/PopupWindowFab$3$1;-><init>(Lnet/gogame/gowrap/ui/fab/PopupWindowFab$3;)V

    invoke-static {v0, v1, v2}, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->access$1000(Lnet/gogame/gowrap/ui/fab/PopupWindowFab;Landroid/app/Activity;Ljava/lang/Runnable;)V

    return-void
.end method
