.class Lnet/gogame/gowrap/support/DefaultDownloadManager$3;
.super Ljava/lang/Object;
.source "DefaultDownloadManager.java"

# interfaces
.implements Lnet/gogame/gowrap/support/DownloadUtils$Target;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lnet/gogame/gowrap/support/DefaultDownloadManager;->download(Lnet/gogame/gowrap/support/DownloadManager$Request;)V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field private editor:Lnet/gogame/gowrap/support/DiskLruCache$Editor;

.field final synthetic this$0:Lnet/gogame/gowrap/support/DefaultDownloadManager;

.field final synthetic val$key:Ljava/lang/String;


# direct methods
.method constructor <init>(Lnet/gogame/gowrap/support/DefaultDownloadManager;Ljava/lang/String;)V
    .locals 0

    .line 110
    iput-object p1, p0, Lnet/gogame/gowrap/support/DefaultDownloadManager$3;->this$0:Lnet/gogame/gowrap/support/DefaultDownloadManager;

    iput-object p2, p0, Lnet/gogame/gowrap/support/DefaultDownloadManager$3;->val$key:Ljava/lang/String;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public close()V
    .locals 1
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    .line 116
    iget-object v0, p0, Lnet/gogame/gowrap/support/DefaultDownloadManager$3;->editor:Lnet/gogame/gowrap/support/DiskLruCache$Editor;

    if-eqz v0, :cond_0

    .line 117
    iget-object v0, p0, Lnet/gogame/gowrap/support/DefaultDownloadManager$3;->editor:Lnet/gogame/gowrap/support/DiskLruCache$Editor;

    invoke-virtual {v0}, Lnet/gogame/gowrap/support/DiskLruCache$Editor;->commit()V

    :cond_0
    return-void
.end method

.method public getOutputStream()Ljava/io/OutputStream;
    .locals 2
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    .line 132
    iget-object v0, p0, Lnet/gogame/gowrap/support/DefaultDownloadManager$3;->editor:Lnet/gogame/gowrap/support/DiskLruCache$Editor;

    if-nez v0, :cond_0

    .line 133
    iget-object v0, p0, Lnet/gogame/gowrap/support/DefaultDownloadManager$3;->this$0:Lnet/gogame/gowrap/support/DefaultDownloadManager;

    invoke-static {v0}, Lnet/gogame/gowrap/support/DefaultDownloadManager;->access$100(Lnet/gogame/gowrap/support/DefaultDownloadManager;)Lnet/gogame/gowrap/support/DiskLruCache;

    move-result-object v0

    iget-object v1, p0, Lnet/gogame/gowrap/support/DefaultDownloadManager$3;->val$key:Ljava/lang/String;

    invoke-virtual {v0, v1}, Lnet/gogame/gowrap/support/DiskLruCache;->edit(Ljava/lang/String;)Lnet/gogame/gowrap/support/DiskLruCache$Editor;

    move-result-object v0

    iput-object v0, p0, Lnet/gogame/gowrap/support/DefaultDownloadManager$3;->editor:Lnet/gogame/gowrap/support/DiskLruCache$Editor;

    .line 135
    :cond_0
    iget-object v0, p0, Lnet/gogame/gowrap/support/DefaultDownloadManager$3;->editor:Lnet/gogame/gowrap/support/DiskLruCache$Editor;

    const/4 v1, 0x0

    invoke-virtual {v0, v1}, Lnet/gogame/gowrap/support/DiskLruCache$Editor;->newOutputStream(I)Ljava/io/OutputStream;

    move-result-object v0

    return-object v0
.end method

.method public setEtag(Ljava/lang/String;)V
    .locals 2
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    .line 124
    iget-object v0, p0, Lnet/gogame/gowrap/support/DefaultDownloadManager$3;->editor:Lnet/gogame/gowrap/support/DiskLruCache$Editor;

    if-nez v0, :cond_0

    .line 125
    iget-object v0, p0, Lnet/gogame/gowrap/support/DefaultDownloadManager$3;->this$0:Lnet/gogame/gowrap/support/DefaultDownloadManager;

    invoke-static {v0}, Lnet/gogame/gowrap/support/DefaultDownloadManager;->access$100(Lnet/gogame/gowrap/support/DefaultDownloadManager;)Lnet/gogame/gowrap/support/DiskLruCache;

    move-result-object v0

    iget-object v1, p0, Lnet/gogame/gowrap/support/DefaultDownloadManager$3;->val$key:Ljava/lang/String;

    invoke-virtual {v0, v1}, Lnet/gogame/gowrap/support/DiskLruCache;->edit(Ljava/lang/String;)Lnet/gogame/gowrap/support/DiskLruCache$Editor;

    move-result-object v0

    iput-object v0, p0, Lnet/gogame/gowrap/support/DefaultDownloadManager$3;->editor:Lnet/gogame/gowrap/support/DiskLruCache$Editor;

    .line 127
    :cond_0
    iget-object v0, p0, Lnet/gogame/gowrap/support/DefaultDownloadManager$3;->editor:Lnet/gogame/gowrap/support/DiskLruCache$Editor;

    const/4 v1, 0x1

    invoke-virtual {v0, v1, p1}, Lnet/gogame/gowrap/support/DiskLruCache$Editor;->set(ILjava/lang/String;)V

    return-void
.end method
