.class Lnet/gogame/gowrap/ui/v2017_2/NewsFragment$2;
.super Ljava/lang/Object;
.source "NewsFragment.java"

# interfaces
.implements Landroid/widget/ViewSwitcher$ViewFactory;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;->onCreateView(Landroid/view/LayoutInflater;Landroid/view/ViewGroup;Landroid/os/Bundle;)Landroid/view/View;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;

.field final synthetic val$inflater:Landroid/view/LayoutInflater;


# direct methods
.method constructor <init>(Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;Landroid/view/LayoutInflater;)V
    .locals 0

    .line 115
    iput-object p1, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment$2;->this$0:Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;

    iput-object p2, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment$2;->val$inflater:Landroid/view/LayoutInflater;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public makeView()Landroid/view/View;
    .locals 4

    .line 119
    iget-object v0, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment$2;->val$inflater:Landroid/view/LayoutInflater;

    sget v1, Lnet/gogame/gowrap/R$layout;->net_gogame_gowrap_v2017_2_fragment_news_banner:I

    iget-object v2, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment$2;->this$0:Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;

    .line 120
    invoke-static {v2}, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;->access$200(Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;)Landroid/widget/ImageSwitcher;

    move-result-object v2

    const/4 v3, 0x0

    .line 119
    invoke-virtual {v0, v1, v2, v3}, Landroid/view/LayoutInflater;->inflate(ILandroid/view/ViewGroup;Z)Landroid/view/View;

    move-result-object v0

    check-cast v0, Landroid/widget/ImageView;

    .line 122
    sget v1, Landroid/os/Build$VERSION;->SDK_INT:I

    const/16 v2, 0x15

    if-lt v1, v2, :cond_0

    const/4 v1, 0x1

    .line 123
    invoke-virtual {v0, v1}, Landroid/widget/ImageView;->setClipToOutline(Z)V

    :cond_0
    return-object v0
.end method
