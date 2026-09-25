.class Lnet/gogame/gowrap/ui/v2017_2/NewsFragment$1;
.super Ljava/lang/Object;
.source "NewsFragment.java"

# interfaces
.implements Landroid/widget/AdapterView$OnItemClickListener;


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

    .line 101
    iput-object p1, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment$1;->this$0:Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public onItemClick(Landroid/widget/AdapterView;Landroid/view/View;IJ)V
    .locals 0
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Landroid/widget/AdapterView<",
            "*>;",
            "Landroid/view/View;",
            "IJ)V"
        }
    .end annotation

    .line 105
    iget-object p1, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment$1;->this$0:Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;

    invoke-static {p1}, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;->access$000(Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;)Lnet/gogame/gowrap/ui/v2017_2/NewsListAdapter;

    move-result-object p1

    invoke-virtual {p1, p3}, Lnet/gogame/gowrap/ui/v2017_2/NewsListAdapter;->getItem(I)Ljava/lang/Object;

    move-result-object p1

    check-cast p1, Lnet/gogame/gowrap/model/news/Article;

    .line 106
    iget-object p2, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment$1;->this$0:Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;

    invoke-static {p2}, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;->access$100(Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;)Lnet/gogame/gowrap/ui/UIContext;

    move-result-object p2

    if-eqz p2, :cond_0

    if-eqz p1, :cond_0

    .line 107
    iget-object p2, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment$1;->this$0:Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;

    invoke-static {p2}, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;->access$000(Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;)Lnet/gogame/gowrap/ui/v2017_2/NewsListAdapter;

    move-result-object p2

    invoke-virtual {p2, p1}, Lnet/gogame/gowrap/ui/v2017_2/NewsListAdapter;->markAsRead(Lnet/gogame/gowrap/model/news/Article;)V

    .line 108
    invoke-static {p1}, Lnet/gogame/gowrap/ui/v2017_2/NewsArticleFragment;->create(Lnet/gogame/gowrap/model/news/Article;)Lnet/gogame/gowrap/ui/v2017_2/NewsArticleFragment;

    move-result-object p1

    .line 109
    iget-object p2, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment$1;->this$0:Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;

    invoke-static {p2}, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;->access$100(Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;)Lnet/gogame/gowrap/ui/UIContext;

    move-result-object p2

    invoke-interface {p2, p1}, Lnet/gogame/gowrap/ui/UIContext;->pushFragment(Landroid/app/Fragment;)V

    :cond_0
    return-void
.end method
