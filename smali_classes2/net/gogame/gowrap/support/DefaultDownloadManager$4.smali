.class Lnet/gogame/gowrap/support/DefaultDownloadManager$4;
.super Ljava/lang/Object;
.source "DefaultDownloadManager.java"

# interfaces
.implements Lnet/gogame/gowrap/support/DownloadUtils$Callback;


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

.field final synthetic val$request:Lnet/gogame/gowrap/support/DownloadManager$Request;

.field final synthetic val$target:Lnet/gogame/gowrap/support/DownloadManager$Target;


# direct methods
.method constructor <init>(Lnet/gogame/gowrap/support/DefaultDownloadManager;Lnet/gogame/gowrap/support/DownloadManager$Target;Ljava/lang/String;Lnet/gogame/gowrap/support/DownloadManager$Request;)V
    .locals 0

    .line 138
    iput-object p1, p0, Lnet/gogame/gowrap/support/DefaultDownloadManager$4;->this$0:Lnet/gogame/gowrap/support/DefaultDownloadManager;

    iput-object p2, p0, Lnet/gogame/gowrap/support/DefaultDownloadManager$4;->val$target:Lnet/gogame/gowrap/support/DownloadManager$Target;

    iput-object p3, p0, Lnet/gogame/gowrap/support/DefaultDownloadManager$4;->val$key:Ljava/lang/String;

    iput-object p4, p0, Lnet/gogame/gowrap/support/DefaultDownloadManager$4;->val$request:Lnet/gogame/gowrap/support/DownloadManager$Request;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public onDownloadFailed()V
    .locals 2

    .line 158
    iget-object v0, p0, Lnet/gogame/gowrap/support/DefaultDownloadManager$4;->this$0:Lnet/gogame/gowrap/support/DefaultDownloadManager;

    invoke-static {v0}, Lnet/gogame/gowrap/support/DefaultDownloadManager;->access$200(Lnet/gogame/gowrap/support/DefaultDownloadManager;)V

    .line 159
    iget-object v0, p0, Lnet/gogame/gowrap/support/DefaultDownloadManager$4;->this$0:Lnet/gogame/gowrap/support/DefaultDownloadManager;

    invoke-static {v0}, Lnet/gogame/gowrap/support/DefaultDownloadManager;->access$300(Lnet/gogame/gowrap/support/DefaultDownloadManager;)Landroid/os/Handler;

    move-result-object v0

    new-instance v1, Lnet/gogame/gowrap/support/DefaultDownloadManager$4$2;

    invoke-direct {v1, p0}, Lnet/gogame/gowrap/support/DefaultDownloadManager$4$2;-><init>(Lnet/gogame/gowrap/support/DefaultDownloadManager$4;)V

    invoke-virtual {v0, v1}, Landroid/os/Handler;->post(Ljava/lang/Runnable;)Z

    return-void
.end method

.method public onDownloadSucceeded()V
    .locals 2

    .line 142
    iget-object v0, p0, Lnet/gogame/gowrap/support/DefaultDownloadManager$4;->this$0:Lnet/gogame/gowrap/support/DefaultDownloadManager;

    invoke-static {v0}, Lnet/gogame/gowrap/support/DefaultDownloadManager;->access$200(Lnet/gogame/gowrap/support/DefaultDownloadManager;)V

    .line 143
    iget-object v0, p0, Lnet/gogame/gowrap/support/DefaultDownloadManager$4;->this$0:Lnet/gogame/gowrap/support/DefaultDownloadManager;

    invoke-static {v0}, Lnet/gogame/gowrap/support/DefaultDownloadManager;->access$300(Lnet/gogame/gowrap/support/DefaultDownloadManager;)Landroid/os/Handler;

    move-result-object v0

    new-instance v1, Lnet/gogame/gowrap/support/DefaultDownloadManager$4$1;

    invoke-direct {v1, p0}, Lnet/gogame/gowrap/support/DefaultDownloadManager$4$1;-><init>(Lnet/gogame/gowrap/support/DefaultDownloadManager$4;)V

    invoke-virtual {v0, v1}, Landroid/os/Handler;->post(Ljava/lang/Runnable;)Z

    return-void
.end method
