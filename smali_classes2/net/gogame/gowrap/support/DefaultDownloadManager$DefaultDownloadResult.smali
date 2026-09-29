.class Lnet/gogame/gowrap/support/DefaultDownloadManager$DefaultDownloadResult;
.super Ljava/lang/Object;
.source "DefaultDownloadManager.java"

# interfaces
.implements Lnet/gogame/gowrap/support/DownloadManager$DownloadResult;


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lnet/gogame/gowrap/support/DefaultDownloadManager;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x2
    name = "DefaultDownloadResult"
.end annotation


# instance fields
.field private final key:Ljava/lang/String;

.field private snapshot:Lnet/gogame/gowrap/support/DiskLruCache$Snapshot;

.field final synthetic this$0:Lnet/gogame/gowrap/support/DefaultDownloadManager;


# direct methods
.method public constructor <init>(Lnet/gogame/gowrap/support/DefaultDownloadManager;Ljava/lang/String;)V
    .locals 0

    .line 228
    iput-object p1, p0, Lnet/gogame/gowrap/support/DefaultDownloadManager$DefaultDownloadResult;->this$0:Lnet/gogame/gowrap/support/DefaultDownloadManager;

    .line 229
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 231
    iput-object p2, p0, Lnet/gogame/gowrap/support/DefaultDownloadManager$DefaultDownloadResult;->key:Ljava/lang/String;

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

    .line 249
    iget-object v0, p0, Lnet/gogame/gowrap/support/DefaultDownloadManager$DefaultDownloadResult;->snapshot:Lnet/gogame/gowrap/support/DiskLruCache$Snapshot;

    if-eqz v0, :cond_0

    .line 250
    iget-object v0, p0, Lnet/gogame/gowrap/support/DefaultDownloadManager$DefaultDownloadResult;->snapshot:Lnet/gogame/gowrap/support/DiskLruCache$Snapshot;

    invoke-virtual {v0}, Lnet/gogame/gowrap/support/DiskLruCache$Snapshot;->close()V

    const/4 v0, 0x0

    .line 251
    iput-object v0, p0, Lnet/gogame/gowrap/support/DefaultDownloadManager$DefaultDownloadResult;->snapshot:Lnet/gogame/gowrap/support/DiskLruCache$Snapshot;

    :cond_0
    return-void
.end method

.method public getInputStream()Ljava/io/InputStream;
    .locals 2
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    .line 236
    iget-object v0, p0, Lnet/gogame/gowrap/support/DefaultDownloadManager$DefaultDownloadResult;->snapshot:Lnet/gogame/gowrap/support/DiskLruCache$Snapshot;

    if-nez v0, :cond_1

    .line 237
    iget-object v0, p0, Lnet/gogame/gowrap/support/DefaultDownloadManager$DefaultDownloadResult;->this$0:Lnet/gogame/gowrap/support/DefaultDownloadManager;

    invoke-static {v0}, Lnet/gogame/gowrap/support/DefaultDownloadManager;->access$100(Lnet/gogame/gowrap/support/DefaultDownloadManager;)Lnet/gogame/gowrap/support/DiskLruCache;

    move-result-object v0

    iget-object v1, p0, Lnet/gogame/gowrap/support/DefaultDownloadManager$DefaultDownloadResult;->key:Ljava/lang/String;

    invoke-virtual {v0, v1}, Lnet/gogame/gowrap/support/DiskLruCache;->get(Ljava/lang/String;)Lnet/gogame/gowrap/support/DiskLruCache$Snapshot;

    move-result-object v0

    iput-object v0, p0, Lnet/gogame/gowrap/support/DefaultDownloadManager$DefaultDownloadResult;->snapshot:Lnet/gogame/gowrap/support/DiskLruCache$Snapshot;

    .line 238
    iget-object v0, p0, Lnet/gogame/gowrap/support/DefaultDownloadManager$DefaultDownloadResult;->snapshot:Lnet/gogame/gowrap/support/DiskLruCache$Snapshot;

    if-nez v0, :cond_0

    const/4 v0, 0x0

    return-object v0

    .line 241
    :cond_0
    iget-object v0, p0, Lnet/gogame/gowrap/support/DefaultDownloadManager$DefaultDownloadResult;->snapshot:Lnet/gogame/gowrap/support/DiskLruCache$Snapshot;

    const/4 v1, 0x0

    invoke-virtual {v0, v1}, Lnet/gogame/gowrap/support/DiskLruCache$Snapshot;->getInputStream(I)Ljava/io/InputStream;

    move-result-object v0

    return-object v0

    .line 243
    :cond_1
    new-instance v0, Ljava/lang/IllegalStateException;

    const-string v1, "Snapshot already open"

    invoke-direct {v0, v1}, Ljava/lang/IllegalStateException;-><init>(Ljava/lang/String;)V

    throw v0
.end method
