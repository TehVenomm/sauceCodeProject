.class public Lnet/gogame/gowrap/support/DownloadUtils$FileTarget;
.super Ljava/lang/Object;
.source "DownloadUtils.java"

# interfaces
.implements Lnet/gogame/gowrap/support/DownloadUtils$Target;


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lnet/gogame/gowrap/support/DownloadUtils;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x9
    name = "FileTarget"
.end annotation


# instance fields
.field private final file:Ljava/io/File;

.field private tempFile:Ljava/io/File;


# direct methods
.method public constructor <init>(Ljava/io/File;)V
    .locals 0

    .line 205
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 207
    iput-object p1, p0, Lnet/gogame/gowrap/support/DownloadUtils$FileTarget;->file:Ljava/io/File;

    return-void
.end method


# virtual methods
.method public close()V
    .locals 2
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    .line 228
    iget-object v0, p0, Lnet/gogame/gowrap/support/DownloadUtils$FileTarget;->tempFile:Ljava/io/File;

    if-eqz v0, :cond_0

    .line 229
    iget-object v0, p0, Lnet/gogame/gowrap/support/DownloadUtils$FileTarget;->tempFile:Ljava/io/File;

    iget-object v1, p0, Lnet/gogame/gowrap/support/DownloadUtils$FileTarget;->file:Ljava/io/File;

    invoke-static {v0, v1}, Lnet/gogame/gowrap/io/utils/FileUtils;->copy(Ljava/io/File;Ljava/io/File;)V

    .line 230
    iget-object v0, p0, Lnet/gogame/gowrap/support/DownloadUtils$FileTarget;->tempFile:Ljava/io/File;

    invoke-virtual {v0}, Ljava/io/File;->delete()Z

    const/4 v0, 0x0

    .line 231
    iput-object v0, p0, Lnet/gogame/gowrap/support/DownloadUtils$FileTarget;->tempFile:Ljava/io/File;

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

    .line 217
    iget-object v0, p0, Lnet/gogame/gowrap/support/DownloadUtils$FileTarget;->tempFile:Ljava/io/File;

    if-nez v0, :cond_0

    const-string v0, "download-"

    const-string v1, ".tmp"

    .line 218
    invoke-static {v0, v1}, Ljava/io/File;->createTempFile(Ljava/lang/String;Ljava/lang/String;)Ljava/io/File;

    move-result-object v0

    iput-object v0, p0, Lnet/gogame/gowrap/support/DownloadUtils$FileTarget;->tempFile:Ljava/io/File;

    .line 219
    iget-object v0, p0, Lnet/gogame/gowrap/support/DownloadUtils$FileTarget;->tempFile:Ljava/io/File;

    invoke-virtual {v0}, Ljava/io/File;->deleteOnExit()V

    .line 220
    new-instance v0, Ljava/io/FileOutputStream;

    iget-object v1, p0, Lnet/gogame/gowrap/support/DownloadUtils$FileTarget;->tempFile:Ljava/io/File;

    invoke-direct {v0, v1}, Ljava/io/FileOutputStream;-><init>(Ljava/io/File;)V

    return-object v0

    .line 222
    :cond_0
    new-instance v0, Ljava/lang/IllegalStateException;

    const-string v1, "Output stream already opened"

    invoke-direct {v0, v1}, Ljava/lang/IllegalStateException;-><init>(Ljava/lang/String;)V

    throw v0
.end method

.method public setEtag(Ljava/lang/String;)V
    .locals 0
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    return-void
.end method
