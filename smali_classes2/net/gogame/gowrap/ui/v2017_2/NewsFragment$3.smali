.class Lnet/gogame/gowrap/ui/v2017_2/NewsFragment$3;
.super Ljava/lang/Object;
.source "NewsFragment.java"

# interfaces
.implements Landroid/view/View$OnClickListener;


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


# direct methods
.method constructor <init>(Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;)V
    .locals 0

    .line 129
    iput-object p1, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment$3;->this$0:Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public onClick(Landroid/view/View;)V
    .locals 1

    .line 133
    iget-object p1, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment$3;->this$0:Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;

    invoke-static {p1}, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;->access$300(Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;)Ljava/util/ArrayList;

    move-result-object p1

    if-eqz p1, :cond_2

    iget-object p1, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment$3;->this$0:Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;

    invoke-static {p1}, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;->access$300(Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;)Ljava/util/ArrayList;

    move-result-object p1

    invoke-virtual {p1}, Ljava/util/ArrayList;->isEmpty()Z

    move-result p1

    if-nez p1, :cond_2

    iget-object p1, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment$3;->this$0:Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;

    .line 134
    invoke-static {p1}, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;->access$400(Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;)I

    move-result p1

    iget-object v0, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment$3;->this$0:Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;

    invoke-static {v0}, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;->access$300(Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;)Ljava/util/ArrayList;

    move-result-object v0

    invoke-virtual {v0}, Ljava/util/ArrayList;->size()I

    move-result v0

    if-lt p1, v0, :cond_0

    goto :goto_0

    .line 137
    :cond_0
    iget-object p1, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment$3;->this$0:Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;

    invoke-static {p1}, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;->access$300(Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;)Ljava/util/ArrayList;

    move-result-object p1

    iget-object v0, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment$3;->this$0:Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;

    invoke-static {v0}, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;->access$400(Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;)I

    move-result v0

    invoke-virtual {p1, v0}, Ljava/util/ArrayList;->get(I)Ljava/lang/Object;

    move-result-object p1

    check-cast p1, Lnet/gogame/gowrap/model/news/Banner;

    if-nez p1, :cond_1

    return-void

    .line 141
    :cond_1
    iget-object v0, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment$3;->this$0:Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;

    invoke-virtual {p1}, Lnet/gogame/gowrap/model/news/Banner;->getLink()Ljava/lang/String;

    move-result-object p1

    invoke-static {v0, p1}, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;->access$500(Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;Ljava/lang/String;)V

    return-void

    :cond_2
    :goto_0
    return-void
.end method
