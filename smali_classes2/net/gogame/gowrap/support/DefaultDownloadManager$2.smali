.class Lnet/gogame/gowrap/support/DefaultDownloadManager$2;
.super Ljava/lang/Object;
.source "DefaultDownloadManager.java"

# interfaces
.implements Ljava/lang/Runnable;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lnet/gogame/gowrap/support/DefaultDownloadManager;->download(Lnet/gogame/gowrap/support/DownloadManager$Request;)V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lnet/gogame/gowrap/support/DefaultDownloadManager;

.field final synthetic val$key:Ljava/lang/String;

.field final synthetic val$target:Lnet/gogame/gowrap/support/DownloadManager$Target;


# direct methods
.method constructor <init>(Lnet/gogame/gowrap/support/DefaultDownloadManager;Lnet/gogame/gowrap/support/DownloadManager$Target;Ljava/lang/String;)V
    .locals 0

    .line 89
    iput-object p1, p0, Lnet/gogame/gowrap/support/DefaultDownloadManager$2;->this$0:Lnet/gogame/gowrap/support/DefaultDownloadManager;

    iput-object p2, p0, Lnet/gogame/gowrap/support/DefaultDownloadManager$2;->val$target:Lnet/gogame/gowrap/support/DownloadManager$Target;

    iput-object p3, p0, Lnet/gogame/gowrap/support/DefaultDownloadManager$2;->val$key:Ljava/lang/String;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public run()V
    .locals 4

    .line 94
    :try_start_0
    iget-object v0, p0, Lnet/gogame/gowrap/support/DefaultDownloadManager$2;->val$target:Lnet/gogame/gowrap/support/DownloadManager$Target;

    new-instance v1, Lnet/gogame/gowrap/support/DefaultDownloadManager$DefaultDownloadResult;

    iget-object v2, p0, Lnet/gogame/gowrap/support/DefaultDownloadManager$2;->this$0:Lnet/gogame/gowrap/support/DefaultDownloadManager;

    iget-object v3, p0, Lnet/gogame/gowrap/support/DefaultDownloadManager$2;->val$key:Ljava/lang/String;

    invoke-direct {v1, v2, v3}, Lnet/gogame/gowrap/support/DefaultDownloadManager$DefaultDownloadResult;-><init>(Lnet/gogame/gowrap/support/DefaultDownloadManager;Ljava/lang/String;)V

    invoke-interface {v0, v1}, Lnet/gogame/gowrap/support/DownloadManager$Target;->onDownloadSucceeded(Lnet/gogame/gowrap/support/DownloadManager$DownloadResult;)V
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception v0

    const-string v1, "goWrap"

    const-string v2, "Exception"

    .line 96
    invoke-static {v1, v2, v0}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)I

    :goto_0
    return-void
.end method
