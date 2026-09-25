.class Lnet/gogame/gowrap/wrapper/OverlayHelper$2;
.super Ljava/lang/Object;
.source "OverlayHelper.java"

# interfaces
.implements Ljava/lang/Runnable;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lnet/gogame/gowrap/wrapper/OverlayHelper;->show(III)V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lnet/gogame/gowrap/wrapper/OverlayHelper;

.field final synthetic val$gravity:I

.field final synthetic val$parentLayout:Landroid/view/ViewGroup;

.field final synthetic val$x:I

.field final synthetic val$y:I


# direct methods
.method constructor <init>(Lnet/gogame/gowrap/wrapper/OverlayHelper;Landroid/view/ViewGroup;III)V
    .locals 0

    .line 69
    iput-object p1, p0, Lnet/gogame/gowrap/wrapper/OverlayHelper$2;->this$0:Lnet/gogame/gowrap/wrapper/OverlayHelper;

    iput-object p2, p0, Lnet/gogame/gowrap/wrapper/OverlayHelper$2;->val$parentLayout:Landroid/view/ViewGroup;

    iput p3, p0, Lnet/gogame/gowrap/wrapper/OverlayHelper$2;->val$gravity:I

    iput p4, p0, Lnet/gogame/gowrap/wrapper/OverlayHelper$2;->val$x:I

    iput p5, p0, Lnet/gogame/gowrap/wrapper/OverlayHelper$2;->val$y:I

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public run()V
    .locals 5

    .line 73
    iget-object v0, p0, Lnet/gogame/gowrap/wrapper/OverlayHelper$2;->this$0:Lnet/gogame/gowrap/wrapper/OverlayHelper;

    invoke-static {v0}, Lnet/gogame/gowrap/wrapper/OverlayHelper;->access$000(Lnet/gogame/gowrap/wrapper/OverlayHelper;)Landroid/widget/PopupWindow;

    move-result-object v0

    if-eqz v0, :cond_0

    .line 75
    :try_start_0
    iget-object v0, p0, Lnet/gogame/gowrap/wrapper/OverlayHelper$2;->this$0:Lnet/gogame/gowrap/wrapper/OverlayHelper;

    invoke-static {v0}, Lnet/gogame/gowrap/wrapper/OverlayHelper;->access$000(Lnet/gogame/gowrap/wrapper/OverlayHelper;)Landroid/widget/PopupWindow;

    move-result-object v0

    iget-object v1, p0, Lnet/gogame/gowrap/wrapper/OverlayHelper$2;->val$parentLayout:Landroid/view/ViewGroup;

    iget v2, p0, Lnet/gogame/gowrap/wrapper/OverlayHelper$2;->val$gravity:I

    iget v3, p0, Lnet/gogame/gowrap/wrapper/OverlayHelper$2;->val$x:I

    iget v4, p0, Lnet/gogame/gowrap/wrapper/OverlayHelper$2;->val$y:I

    invoke-virtual {v0, v1, v2, v3, v4}, Landroid/widget/PopupWindow;->showAtLocation(Landroid/view/View;III)V
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception v0

    const-string v1, "goWrap"

    const-string v2, "Exception"

    .line 77
    invoke-static {v1, v2, v0}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)I

    :cond_0
    :goto_0
    return-void
.end method
