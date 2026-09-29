.class Lnet/gogame/gowrap/wrapper/BannerHelper$1;
.super Ljava/lang/Object;
.source "BannerHelper.java"

# interfaces
.implements Ljava/lang/Runnable;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lnet/gogame/gowrap/wrapper/BannerHelper;->show()Z
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lnet/gogame/gowrap/wrapper/BannerHelper;


# direct methods
.method constructor <init>(Lnet/gogame/gowrap/wrapper/BannerHelper;)V
    .locals 0

    .line 44
    iput-object p1, p0, Lnet/gogame/gowrap/wrapper/BannerHelper$1;->this$0:Lnet/gogame/gowrap/wrapper/BannerHelper;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public run()V
    .locals 5

    .line 49
    :try_start_0
    iget-object v0, p0, Lnet/gogame/gowrap/wrapper/BannerHelper$1;->this$0:Lnet/gogame/gowrap/wrapper/BannerHelper;

    new-instance v1, Landroid/widget/PopupWindow;

    iget-object v2, p0, Lnet/gogame/gowrap/wrapper/BannerHelper$1;->this$0:Lnet/gogame/gowrap/wrapper/BannerHelper;

    invoke-static {v2}, Lnet/gogame/gowrap/wrapper/BannerHelper;->access$100(Lnet/gogame/gowrap/wrapper/BannerHelper;)Landroid/view/View;

    move-result-object v2

    const/4 v3, -0x2

    const/4 v4, 0x0

    invoke-direct {v1, v2, v3, v3, v4}, Landroid/widget/PopupWindow;-><init>(Landroid/view/View;IIZ)V

    invoke-static {v0, v1}, Lnet/gogame/gowrap/wrapper/BannerHelper;->access$002(Lnet/gogame/gowrap/wrapper/BannerHelper;Landroid/widget/PopupWindow;)Landroid/widget/PopupWindow;

    .line 51
    iget-object v0, p0, Lnet/gogame/gowrap/wrapper/BannerHelper$1;->this$0:Lnet/gogame/gowrap/wrapper/BannerHelper;

    invoke-static {v0}, Lnet/gogame/gowrap/wrapper/BannerHelper;->access$000(Lnet/gogame/gowrap/wrapper/BannerHelper;)Landroid/widget/PopupWindow;

    move-result-object v0

    invoke-virtual {v0, v4}, Landroid/widget/PopupWindow;->setClippingEnabled(Z)V

    .line 52
    iget-object v0, p0, Lnet/gogame/gowrap/wrapper/BannerHelper$1;->this$0:Lnet/gogame/gowrap/wrapper/BannerHelper;

    invoke-static {v0}, Lnet/gogame/gowrap/wrapper/BannerHelper;->access$000(Lnet/gogame/gowrap/wrapper/BannerHelper;)Landroid/widget/PopupWindow;

    move-result-object v0

    iget-object v1, p0, Lnet/gogame/gowrap/wrapper/BannerHelper$1;->this$0:Lnet/gogame/gowrap/wrapper/BannerHelper;

    invoke-static {v1}, Lnet/gogame/gowrap/wrapper/BannerHelper;->access$200(Lnet/gogame/gowrap/wrapper/BannerHelper;)Landroid/view/ViewGroup;

    move-result-object v1

    iget-object v2, p0, Lnet/gogame/gowrap/wrapper/BannerHelper$1;->this$0:Lnet/gogame/gowrap/wrapper/BannerHelper;

    invoke-static {v2}, Lnet/gogame/gowrap/wrapper/BannerHelper;->access$300(Lnet/gogame/gowrap/wrapper/BannerHelper;)I

    move-result v2

    invoke-virtual {v0, v1, v2, v4, v4}, Landroid/widget/PopupWindow;->showAtLocation(Landroid/view/View;III)V
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception v0

    const-string v1, "goWrap"

    const-string v2, "Exception"

    .line 54
    invoke-static {v1, v2, v0}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)I

    :goto_0
    return-void
.end method
