.class Lnet/gogame/gowrap/ui/v2017_2/NewsFragment$5;
.super Ljava/lang/Object;
.source "NewsFragment.java"

# interfaces
.implements Lnet/gogame/gowrap/support/DownloadUtils$Callback;


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

.field final synthetic val$feedFile:Ljava/io/File;


# direct methods
.method constructor <init>(Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;Ljava/io/File;)V
    .locals 0

    .line 228
    iput-object p1, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment$5;->this$0:Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;

    iput-object p2, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment$5;->val$feedFile:Ljava/io/File;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method

.method static synthetic access$1100(Lnet/gogame/gowrap/ui/v2017_2/NewsFragment$5;)V
    .locals 0

    .line 228
    invoke-direct {p0}, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment$5;->showError()V

    return-void
.end method

.method static synthetic access$900(Lnet/gogame/gowrap/ui/v2017_2/NewsFragment$5;)V
    .locals 0

    .line 228
    invoke-direct {p0}, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment$5;->hideProgressBar()V

    return-void
.end method

.method private hideProgressBar()V
    .locals 2

    .line 231
    iget-object v0, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment$5;->this$0:Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;

    invoke-static {v0}, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;->access$800(Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;)Landroid/widget/ProgressBar;

    move-result-object v0

    if-eqz v0, :cond_0

    .line 232
    iget-object v0, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment$5;->this$0:Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;

    invoke-static {v0}, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;->access$800(Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;)Landroid/widget/ProgressBar;

    move-result-object v0

    const/16 v1, 0x8

    invoke-virtual {v0, v1}, Landroid/widget/ProgressBar;->setVisibility(I)V

    :cond_0
    return-void
.end method

.method private readFeed(Ljava/io/File;)Lnet/gogame/gowrap/model/news/NewsFeed;
    .locals 3
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    .line 237
    new-instance v0, Ljava/io/FileInputStream;

    invoke-direct {v0, p1}, Ljava/io/FileInputStream;-><init>(Ljava/io/File;)V

    .line 239
    :try_start_0
    new-instance p1, Ljava/io/InputStreamReader;

    const-string v1, "UTF-8"

    invoke-direct {p1, v0, v1}, Ljava/io/InputStreamReader;-><init>(Ljava/io/InputStream;Ljava/lang/String;)V
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_2

    .line 242
    :try_start_1
    new-instance v1, Landroid/util/JsonReader;

    invoke-direct {v1, p1}, Landroid/util/JsonReader;-><init>(Ljava/io/Reader;)V
    :try_end_1
    .catchall {:try_start_1 .. :try_end_1} :catchall_1

    .line 244
    :try_start_2
    new-instance v2, Lnet/gogame/gowrap/model/news/NewsFeed;

    invoke-direct {v2, v1}, Lnet/gogame/gowrap/model/news/NewsFeed;-><init>(Landroid/util/JsonReader;)V
    :try_end_2
    .catchall {:try_start_2 .. :try_end_2} :catchall_0

    .line 246
    :try_start_3
    invoke-virtual {v1}, Landroid/util/JsonReader;->close()V
    :try_end_3
    .catchall {:try_start_3 .. :try_end_3} :catchall_1

    .line 249
    :try_start_4
    invoke-static {p1}, Lnet/gogame/gowrap/io/utils/IOUtils;->closeQuietly(Ljava/io/Reader;)V
    :try_end_4
    .catchall {:try_start_4 .. :try_end_4} :catchall_2

    .line 252
    invoke-static {v0}, Lnet/gogame/gowrap/io/utils/IOUtils;->closeQuietly(Ljava/io/InputStream;)V

    return-object v2

    :catchall_0
    move-exception v2

    .line 246
    :try_start_5
    invoke-virtual {v1}, Landroid/util/JsonReader;->close()V

    .line 247
    throw v2
    :try_end_5
    .catchall {:try_start_5 .. :try_end_5} :catchall_1

    :catchall_1
    move-exception v1

    .line 249
    :try_start_6
    invoke-static {p1}, Lnet/gogame/gowrap/io/utils/IOUtils;->closeQuietly(Ljava/io/Reader;)V

    .line 250
    throw v1
    :try_end_6
    .catchall {:try_start_6 .. :try_end_6} :catchall_2

    :catchall_2
    move-exception p1

    .line 252
    invoke-static {v0}, Lnet/gogame/gowrap/io/utils/IOUtils;->closeQuietly(Ljava/io/InputStream;)V

    .line 253
    throw p1
.end method

.method private showError()V
    .locals 2

    .line 304
    iget-object v0, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment$5;->this$0:Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;

    invoke-virtual {v0}, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;->getView()Landroid/view/View;

    move-result-object v0

    if-nez v0, :cond_0

    return-void

    .line 307
    :cond_0
    iget-object v0, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment$5;->this$0:Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;

    invoke-virtual {v0}, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;->getActivity()Landroid/app/Activity;

    move-result-object v0

    new-instance v1, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment$5$4;

    invoke-direct {v1, p0}, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment$5$4;-><init>(Lnet/gogame/gowrap/ui/v2017_2/NewsFragment$5;)V

    invoke-virtual {v0, v1}, Landroid/app/Activity;->runOnUiThread(Ljava/lang/Runnable;)V

    return-void
.end method


# virtual methods
.method public onDownloadFailed()V
    .locals 3

    .line 285
    iget-object v0, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment$5;->this$0:Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;

    invoke-virtual {v0}, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;->getActivity()Landroid/app/Activity;

    move-result-object v0

    if-nez v0, :cond_0

    return-void

    .line 289
    :cond_0
    :try_start_0
    iget-object v0, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment$5;->this$0:Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;

    invoke-virtual {v0}, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;->getActivity()Landroid/app/Activity;

    move-result-object v0

    new-instance v1, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment$5$3;

    invoke-direct {v1, p0}, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment$5$3;-><init>(Lnet/gogame/gowrap/ui/v2017_2/NewsFragment$5;)V

    invoke-virtual {v0, v1}, Landroid/app/Activity;->runOnUiThread(Ljava/lang/Runnable;)V
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception v0

    const-string v1, "goWrap"

    const-string v2, "Exception"

    .line 298
    invoke-static {v1, v2, v0}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)I

    .line 299
    invoke-direct {p0}, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment$5;->showError()V

    :goto_0
    return-void
.end method

.method public onDownloadSucceeded()V
    .locals 3

    .line 258
    iget-object v0, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment$5;->this$0:Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;

    invoke-virtual {v0}, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;->getActivity()Landroid/app/Activity;

    move-result-object v0

    if-nez v0, :cond_0

    return-void

    .line 262
    :cond_0
    :try_start_0
    iget-object v0, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment$5;->this$0:Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;

    invoke-virtual {v0}, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;->getActivity()Landroid/app/Activity;

    move-result-object v0

    new-instance v1, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment$5$1;

    invoke-direct {v1, p0}, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment$5$1;-><init>(Lnet/gogame/gowrap/ui/v2017_2/NewsFragment$5;)V

    invoke-virtual {v0, v1}, Landroid/app/Activity;->runOnUiThread(Ljava/lang/Runnable;)V

    .line 269
    iget-object v0, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment$5;->val$feedFile:Ljava/io/File;

    invoke-direct {p0, v0}, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment$5;->readFeed(Ljava/io/File;)Lnet/gogame/gowrap/model/news/NewsFeed;

    move-result-object v0

    .line 270
    iget-object v1, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment$5;->this$0:Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;

    invoke-virtual {v1}, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;->getActivity()Landroid/app/Activity;

    move-result-object v1

    new-instance v2, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment$5$2;

    invoke-direct {v2, p0, v0}, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment$5$2;-><init>(Lnet/gogame/gowrap/ui/v2017_2/NewsFragment$5;Lnet/gogame/gowrap/model/news/NewsFeed;)V

    invoke-virtual {v1, v2}, Landroid/app/Activity;->runOnUiThread(Ljava/lang/Runnable;)V
    :try_end_0
    .catch Ljava/lang/Throwable; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception v0

    const-string v1, "goWrap"

    const-string v2, "Exception"

    .line 278
    invoke-static {v1, v2, v0}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)I

    .line 279
    invoke-direct {p0}, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment$5;->showError()V

    :goto_0
    return-void
.end method
