.class Lnet/gogame/gowrap/ui/v2017_2/NewsArticleFragment$1;
.super Ljava/lang/Object;
.source "NewsArticleFragment.java"

# interfaces
.implements Lnet/gogame/gowrap/support/DownloadManager$Listener;


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lnet/gogame/gowrap/ui/v2017_2/NewsArticleFragment;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lnet/gogame/gowrap/ui/v2017_2/NewsArticleFragment;


# direct methods
.method constructor <init>(Lnet/gogame/gowrap/ui/v2017_2/NewsArticleFragment;)V
    .locals 0

    .line 48
    iput-object p1, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsArticleFragment$1;->this$0:Lnet/gogame/gowrap/ui/v2017_2/NewsArticleFragment;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public onDownloadsFinished()V
    .locals 2

    .line 59
    iget-object v0, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsArticleFragment$1;->this$0:Lnet/gogame/gowrap/ui/v2017_2/NewsArticleFragment;

    invoke-static {v0}, Lnet/gogame/gowrap/ui/v2017_2/NewsArticleFragment;->access$000(Lnet/gogame/gowrap/ui/v2017_2/NewsArticleFragment;)Landroid/widget/ProgressBar;

    move-result-object v0

    if-eqz v0, :cond_0

    .line 60
    iget-object v0, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsArticleFragment$1;->this$0:Lnet/gogame/gowrap/ui/v2017_2/NewsArticleFragment;

    invoke-static {v0}, Lnet/gogame/gowrap/ui/v2017_2/NewsArticleFragment;->access$000(Lnet/gogame/gowrap/ui/v2017_2/NewsArticleFragment;)Landroid/widget/ProgressBar;

    move-result-object v0

    const/16 v1, 0x8

    invoke-virtual {v0, v1}, Landroid/widget/ProgressBar;->setVisibility(I)V

    :cond_0
    return-void
.end method

.method public onDownloadsStarted()V
    .locals 2

    .line 52
    iget-object v0, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsArticleFragment$1;->this$0:Lnet/gogame/gowrap/ui/v2017_2/NewsArticleFragment;

    invoke-static {v0}, Lnet/gogame/gowrap/ui/v2017_2/NewsArticleFragment;->access$000(Lnet/gogame/gowrap/ui/v2017_2/NewsArticleFragment;)Landroid/widget/ProgressBar;

    move-result-object v0

    if-eqz v0, :cond_0

    .line 53
    iget-object v0, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsArticleFragment$1;->this$0:Lnet/gogame/gowrap/ui/v2017_2/NewsArticleFragment;

    invoke-static {v0}, Lnet/gogame/gowrap/ui/v2017_2/NewsArticleFragment;->access$000(Lnet/gogame/gowrap/ui/v2017_2/NewsArticleFragment;)Landroid/widget/ProgressBar;

    move-result-object v0

    const/4 v1, 0x0

    invoke-virtual {v0, v1}, Landroid/widget/ProgressBar;->setVisibility(I)V

    :cond_0
    return-void
.end method
