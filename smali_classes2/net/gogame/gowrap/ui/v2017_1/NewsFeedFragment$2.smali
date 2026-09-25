.class Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment$2;
.super Ljava/lang/Object;
.source "NewsFeedFragment.java"

# interfaces
.implements Lnet/gogame/gowrap/support/DownloadUtils$Callback;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment;->onCreateView(Landroid/view/LayoutInflater;Landroid/view/ViewGroup;Landroid/os/Bundle;)Landroid/view/View;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment;

.field final synthetic val$feedFile:Ljava/io/File;


# direct methods
.method constructor <init>(Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment;Ljava/io/File;)V
    .locals 0

    .line 101
    iput-object p1, p0, Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment$2;->this$0:Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment;

    iput-object p2, p0, Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment$2;->val$feedFile:Ljava/io/File;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public onDownloadFailed()V
    .locals 3

    .line 144
    iget-object v0, p0, Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment$2;->this$0:Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment;

    const/4 v1, 0x0

    invoke-static {v0, v1}, Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment;->access$102(Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment;Z)Z

    .line 145
    iget-object v0, p0, Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment$2;->this$0:Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment;

    invoke-virtual {v0}, Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment;->getActivity()Landroid/app/Activity;

    move-result-object v0

    if-nez v0, :cond_0

    return-void

    .line 149
    :cond_0
    :try_start_0
    iget-object v0, p0, Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment$2;->this$0:Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment;

    invoke-virtual {v0}, Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment;->getActivity()Landroid/app/Activity;

    move-result-object v0

    new-instance v1, Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment$2$4;

    invoke-direct {v1, p0}, Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment$2$4;-><init>(Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment$2;)V

    invoke-virtual {v0, v1}, Landroid/app/Activity;->runOnUiThread(Ljava/lang/Runnable;)V
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception v0

    const-string v1, "goWrap"

    const-string v2, "Exception"

    .line 158
    invoke-static {v1, v2, v0}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)I

    :goto_0
    return-void
.end method

.method public onDownloadSucceeded()V
    .locals 3

    .line 105
    iget-object v0, p0, Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment$2;->this$0:Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment;

    const/4 v1, 0x0

    invoke-static {v0, v1}, Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment;->access$102(Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment;Z)Z

    .line 106
    iget-object v0, p0, Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment$2;->this$0:Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment;

    invoke-virtual {v0}, Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment;->getActivity()Landroid/app/Activity;

    move-result-object v0

    if-nez v0, :cond_0

    return-void

    .line 110
    :cond_0
    :try_start_0
    iget-object v0, p0, Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment$2;->this$0:Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment;

    invoke-virtual {v0}, Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment;->getActivity()Landroid/app/Activity;

    move-result-object v0

    new-instance v1, Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment$2$1;

    invoke-direct {v1, p0}, Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment$2$1;-><init>(Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment$2;)V

    invoke-virtual {v0, v1}, Landroid/app/Activity;->runOnUiThread(Ljava/lang/Runnable;)V

    .line 117
    iget-object v0, p0, Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment$2;->this$0:Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment;

    iget-object v1, p0, Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment$2;->this$0:Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment;

    iget-object v2, p0, Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment$2;->val$feedFile:Ljava/io/File;

    invoke-static {v1, v2}, Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment;->access$300(Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment;Ljava/io/File;)Lnet/gogame/gowrap/model/feed/Feed;

    move-result-object v1

    invoke-static {v0, v1}, Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment;->access$202(Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment;Lnet/gogame/gowrap/model/feed/Feed;)Lnet/gogame/gowrap/model/feed/Feed;

    .line 118
    iget-object v0, p0, Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment$2;->this$0:Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment;

    invoke-virtual {v0}, Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment;->getActivity()Landroid/app/Activity;

    move-result-object v0

    new-instance v1, Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment$2$2;

    invoke-direct {v1, p0}, Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment$2$2;-><init>(Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment$2;)V

    invoke-virtual {v0, v1}, Landroid/app/Activity;->runOnUiThread(Ljava/lang/Runnable;)V
    :try_end_0
    .catch Ljava/lang/Throwable; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception v0

    const-string v1, "goWrap"

    const-string v2, "Exception"

    .line 126
    invoke-static {v1, v2, v0}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)I

    .line 128
    :try_start_1
    iget-object v0, p0, Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment$2;->this$0:Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment;

    invoke-virtual {v0}, Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment;->getActivity()Landroid/app/Activity;

    move-result-object v0

    new-instance v1, Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment$2$3;

    invoke-direct {v1, p0}, Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment$2$3;-><init>(Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment$2;)V

    invoke-virtual {v0, v1}, Landroid/app/Activity;->runOnUiThread(Ljava/lang/Runnable;)V
    :try_end_1
    .catch Ljava/lang/Exception; {:try_start_1 .. :try_end_1} :catch_1

    goto :goto_0

    :catch_1
    move-exception v0

    const-string v1, "goWrap"

    const-string v2, "Exception"

    .line 137
    invoke-static {v1, v2, v0}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)I

    :goto_0
    return-void
.end method
