.class Lnet/gogame/gowrap/ui/v2017_2/NewsFragment$5$4;
.super Ljava/lang/Object;
.source "NewsFragment.java"

# interfaces
.implements Ljava/lang/Runnable;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lnet/gogame/gowrap/ui/v2017_2/NewsFragment$5;->showError()V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$1:Lnet/gogame/gowrap/ui/v2017_2/NewsFragment$5;


# direct methods
.method constructor <init>(Lnet/gogame/gowrap/ui/v2017_2/NewsFragment$5;)V
    .locals 0

    .line 307
    iput-object p1, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment$5$4;->this$1:Lnet/gogame/gowrap/ui/v2017_2/NewsFragment$5;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public run()V
    .locals 2

    .line 312
    iget-object v0, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment$5$4;->this$1:Lnet/gogame/gowrap/ui/v2017_2/NewsFragment$5;

    iget-object v0, v0, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment$5;->this$0:Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;

    invoke-virtual {v0}, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;->getView()Landroid/view/View;

    move-result-object v0

    sget v1, Lnet/gogame/gowrap/R$id;->net_gogame_gowrap_error_container:I

    invoke-virtual {v0, v1}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object v0

    const/4 v1, 0x0

    .line 314
    invoke-virtual {v0, v1}, Landroid/view/View;->setVisibility(I)V

    return-void
.end method
