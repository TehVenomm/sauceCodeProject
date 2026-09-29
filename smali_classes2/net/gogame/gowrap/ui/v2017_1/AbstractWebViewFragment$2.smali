.class Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment$2;
.super Ljava/lang/Object;
.source "AbstractWebViewFragment.java"

# interfaces
.implements Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebChromeClient$ToggledFullscreenCallback;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment;->onCreateView(Landroid/view/LayoutInflater;Landroid/view/ViewGroup;Landroid/os/Bundle;)Landroid/view/View;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment;


# direct methods
.method constructor <init>(Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment;)V
    .locals 0

    .line 179
    iput-object p1, p0, Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment$2;->this$0:Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public toggledFullscreen(Z)V
    .locals 1

    .line 183
    iget-object v0, p0, Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment$2;->this$0:Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment;

    invoke-static {v0}, Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment;->access$000(Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment;)Landroid/content/Context;

    move-result-object v0

    instance-of v0, v0, Lnet/gogame/gowrap/ui/UIContext;

    if-eqz v0, :cond_1

    .line 184
    iget-object v0, p0, Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment$2;->this$0:Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment;

    invoke-static {v0}, Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment;->access$000(Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment;)Landroid/content/Context;

    move-result-object v0

    check-cast v0, Lnet/gogame/gowrap/ui/UIContext;

    if-eqz p1, :cond_0

    const/4 p1, 0x6

    .line 186
    invoke-static {p1}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object p1

    invoke-interface {v0, p1}, Lnet/gogame/gowrap/ui/UIContext;->enterFullscreen(Ljava/lang/Integer;)V

    goto :goto_0

    .line 188
    :cond_0
    invoke-interface {v0}, Lnet/gogame/gowrap/ui/UIContext;->exitFullscreen()V

    :cond_1
    :goto_0
    return-void
.end method
