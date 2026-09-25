.class Lnet/gogame/gowrap/ui/v2017_2/NewsFragment$5$2;
.super Ljava/lang/Object;
.source "NewsFragment.java"

# interfaces
.implements Ljava/lang/Runnable;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lnet/gogame/gowrap/ui/v2017_2/NewsFragment$5;->onDownloadSucceeded()V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$1:Lnet/gogame/gowrap/ui/v2017_2/NewsFragment$5;

.field final synthetic val$newsFeed:Lnet/gogame/gowrap/model/news/NewsFeed;


# direct methods
.method constructor <init>(Lnet/gogame/gowrap/ui/v2017_2/NewsFragment$5;Lnet/gogame/gowrap/model/news/NewsFeed;)V
    .locals 0

    .line 270
    iput-object p1, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment$5$2;->this$1:Lnet/gogame/gowrap/ui/v2017_2/NewsFragment$5;

    iput-object p2, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment$5$2;->val$newsFeed:Lnet/gogame/gowrap/model/news/NewsFeed;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public run()V
    .locals 2

    .line 274
    iget-object v0, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment$5$2;->this$1:Lnet/gogame/gowrap/ui/v2017_2/NewsFragment$5;

    iget-object v0, v0, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment$5;->this$0:Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;

    iget-object v1, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment$5$2;->val$newsFeed:Lnet/gogame/gowrap/model/news/NewsFeed;

    invoke-static {v0, v1}, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;->access$1000(Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;Lnet/gogame/gowrap/model/news/NewsFeed;)V

    return-void
.end method
