.class public Lnet/gogame/gowrap/ui/download/DownloadResultSource;
.super Ljava/lang/Object;
.source "DownloadResultSource.java"

# interfaces
.implements Lnet/gogame/gowrap/ui/utils/ImageUtils$Source;


# instance fields
.field private final downloadResult:Lnet/gogame/gowrap/support/DownloadManager$DownloadResult;


# direct methods
.method public constructor <init>(Lnet/gogame/gowrap/support/DownloadManager$DownloadResult;)V
    .locals 0

    .line 14
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 16
    iput-object p1, p0, Lnet/gogame/gowrap/ui/download/DownloadResultSource;->downloadResult:Lnet/gogame/gowrap/support/DownloadManager$DownloadResult;

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

    .line 26
    iget-object v0, p0, Lnet/gogame/gowrap/ui/download/DownloadResultSource;->downloadResult:Lnet/gogame/gowrap/support/DownloadManager$DownloadResult;

    invoke-interface {v0}, Lnet/gogame/gowrap/support/DownloadManager$DownloadResult;->close()V

    return-void
.end method

.method public getInputStream()Ljava/io/InputStream;
    .locals 1
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    .line 21
    iget-object v0, p0, Lnet/gogame/gowrap/ui/download/DownloadResultSource;->downloadResult:Lnet/gogame/gowrap/support/DownloadManager$DownloadResult;

    invoke-interface {v0}, Lnet/gogame/gowrap/support/DownloadManager$DownloadResult;->getInputStream()Ljava/io/InputStream;

    move-result-object v0

    return-object v0
.end method
