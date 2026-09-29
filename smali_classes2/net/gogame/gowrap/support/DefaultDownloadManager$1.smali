.class Lnet/gogame/gowrap/support/DefaultDownloadManager$1;
.super Ljava/lang/Object;
.source "DefaultDownloadManager.java"

# interfaces
.implements Ljava/lang/Runnable;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lnet/gogame/gowrap/support/DefaultDownloadManager;->onDownloadFinished()V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lnet/gogame/gowrap/support/DefaultDownloadManager;


# direct methods
.method constructor <init>(Lnet/gogame/gowrap/support/DefaultDownloadManager;)V
    .locals 0

    .line 67
    iput-object p1, p0, Lnet/gogame/gowrap/support/DefaultDownloadManager$1;->this$0:Lnet/gogame/gowrap/support/DefaultDownloadManager;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public run()V
    .locals 1

    .line 71
    iget-object v0, p0, Lnet/gogame/gowrap/support/DefaultDownloadManager$1;->this$0:Lnet/gogame/gowrap/support/DefaultDownloadManager;

    invoke-static {v0}, Lnet/gogame/gowrap/support/DefaultDownloadManager;->access$000(Lnet/gogame/gowrap/support/DefaultDownloadManager;)V

    return-void
.end method
