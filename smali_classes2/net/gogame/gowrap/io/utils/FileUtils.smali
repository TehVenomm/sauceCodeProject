.class public final Lnet/gogame/gowrap/io/utils/FileUtils;
.super Ljava/lang/Object;
.source "FileUtils.java"


# direct methods
.method private constructor <init>()V
    .locals 0

    .line 17
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method

.method public static copy(Ljava/io/File;Ljava/io/File;)V
    .locals 2
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    const/4 v0, 0x0

    .line 23
    :try_start_0
    new-instance v1, Ljava/io/FileInputStream;

    invoke-direct {v1, p0}, Ljava/io/FileInputStream;-><init>(Ljava/io/File;)V
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_3

    .line 26
    :try_start_1
    new-instance p0, Ljava/io/FileOutputStream;

    invoke-direct {p0, p1}, Ljava/io/FileOutputStream;-><init>(Ljava/io/File;)V
    :try_end_1
    .catchall {:try_start_1 .. :try_end_1} :catchall_1

    .line 27
    :try_start_2
    invoke-static {v1, p0}, Lnet/gogame/gowrap/io/utils/IOUtils;->copy(Ljava/io/InputStream;Ljava/io/OutputStream;)J
    :try_end_2
    .catchall {:try_start_2 .. :try_end_2} :catchall_0

    .line 29
    :try_start_3
    invoke-static {p0}, Lnet/gogame/gowrap/io/utils/IOUtils;->closeQuietly(Ljava/io/OutputStream;)V
    :try_end_3
    .catchall {:try_start_3 .. :try_end_3} :catchall_2

    .line 32
    invoke-static {v1}, Lnet/gogame/gowrap/io/utils/IOUtils;->closeQuietly(Ljava/io/InputStream;)V

    return-void

    :catchall_0
    move-exception p1

    move-object v0, p0

    goto :goto_0

    :catchall_1
    move-exception p1

    .line 29
    :goto_0
    :try_start_4
    invoke-static {v0}, Lnet/gogame/gowrap/io/utils/IOUtils;->closeQuietly(Ljava/io/OutputStream;)V

    .line 30
    throw p1
    :try_end_4
    .catchall {:try_start_4 .. :try_end_4} :catchall_2

    :catchall_2
    move-exception p0

    goto :goto_1

    :catchall_3
    move-exception p0

    move-object v1, v0

    .line 32
    :goto_1
    invoke-static {v1}, Lnet/gogame/gowrap/io/utils/IOUtils;->closeQuietly(Ljava/io/InputStream;)V

    .line 33
    throw p0
.end method

.method public static copyFromAsset(Landroid/content/Context;Ljava/lang/String;Ljava/io/File;)V
    .locals 2
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    const/4 v0, 0x0

    .line 55
    :try_start_0
    new-instance v1, Ljava/io/FileOutputStream;

    invoke-direct {v1, p2}, Ljava/io/FileOutputStream;-><init>(Ljava/io/File;)V
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_1

    .line 56
    :try_start_1
    invoke-static {p0, p1, v1}, Lnet/gogame/gowrap/io/utils/IOUtils;->assetCopy(Landroid/content/Context;Ljava/lang/String;Ljava/io/OutputStream;)V
    :try_end_1
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    .line 58
    invoke-static {v1}, Lnet/gogame/gowrap/io/utils/IOUtils;->closeQuietly(Ljava/io/OutputStream;)V

    return-void

    :catchall_0
    move-exception p0

    move-object v0, v1

    goto :goto_0

    :catchall_1
    move-exception p0

    :goto_0
    invoke-static {v0}, Lnet/gogame/gowrap/io/utils/IOUtils;->closeQuietly(Ljava/io/OutputStream;)V

    .line 59
    throw p0
.end method

.method public static gzipCopyFromAsset(Landroid/content/Context;Ljava/lang/String;Ljava/io/File;)V
    .locals 2
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    const/4 v0, 0x0

    .line 77
    :try_start_0
    invoke-virtual {p0}, Landroid/content/Context;->getAssets()Landroid/content/res/AssetManager;

    move-result-object p0

    invoke-virtual {p0, p1}, Landroid/content/res/AssetManager;->open(Ljava/lang/String;)Ljava/io/InputStream;

    move-result-object p0
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_3

    :try_start_1
    const-string v1, ".gz"

    .line 78
    invoke-virtual {p1, v1}, Ljava/lang/String;->endsWith(Ljava/lang/String;)Z

    move-result p1

    if-eqz p1, :cond_0

    .line 79
    new-instance p1, Ljava/util/zip/GZIPInputStream;

    invoke-direct {p1, p0}, Ljava/util/zip/GZIPInputStream;-><init>(Ljava/io/InputStream;)V
    :try_end_1
    .catchall {:try_start_1 .. :try_end_1} :catchall_2

    move-object p0, p1

    .line 83
    :cond_0
    :try_start_2
    new-instance p1, Ljava/io/FileOutputStream;

    invoke-direct {p1, p2}, Ljava/io/FileOutputStream;-><init>(Ljava/io/File;)V
    :try_end_2
    .catchall {:try_start_2 .. :try_end_2} :catchall_1

    .line 84
    :try_start_3
    invoke-virtual {p2}, Ljava/io/File;->getName()Ljava/lang/String;

    move-result-object p2

    const-string v0, ".gz"

    invoke-virtual {p2, v0}, Ljava/lang/String;->endsWith(Ljava/lang/String;)Z

    move-result p2

    if-eqz p2, :cond_1

    .line 85
    new-instance p2, Ljava/util/zip/GZIPOutputStream;

    invoke-direct {p2, p1}, Ljava/util/zip/GZIPOutputStream;-><init>(Ljava/io/OutputStream;)V
    :try_end_3
    .catchall {:try_start_3 .. :try_end_3} :catchall_0

    move-object v0, p2

    goto :goto_0

    :cond_1
    move-object v0, p1

    .line 87
    :goto_0
    :try_start_4
    invoke-static {p0, v0}, Lnet/gogame/gowrap/io/utils/IOUtils;->copy(Ljava/io/InputStream;Ljava/io/OutputStream;)J
    :try_end_4
    .catchall {:try_start_4 .. :try_end_4} :catchall_1

    .line 89
    :try_start_5
    invoke-static {v0}, Lnet/gogame/gowrap/io/utils/IOUtils;->closeQuietly(Ljava/io/OutputStream;)V
    :try_end_5
    .catchall {:try_start_5 .. :try_end_5} :catchall_2

    .line 92
    invoke-static {p0}, Lnet/gogame/gowrap/io/utils/IOUtils;->closeQuietly(Ljava/io/InputStream;)V

    return-void

    :catchall_0
    move-exception p2

    goto :goto_1

    :catchall_1
    move-exception p2

    move-object p1, v0

    .line 89
    :goto_1
    :try_start_6
    invoke-static {p1}, Lnet/gogame/gowrap/io/utils/IOUtils;->closeQuietly(Ljava/io/OutputStream;)V

    .line 90
    throw p2
    :try_end_6
    .catchall {:try_start_6 .. :try_end_6} :catchall_2

    :catchall_2
    move-exception p1

    goto :goto_2

    :catchall_3
    move-exception p1

    move-object p0, v0

    .line 92
    :goto_2
    invoke-static {p0}, Lnet/gogame/gowrap/io/utils/IOUtils;->closeQuietly(Ljava/io/InputStream;)V

    .line 93
    throw p1
.end method

.method public static move(Ljava/io/File;Ljava/io/File;)V
    .locals 0
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    .line 38
    invoke-static {p0, p1}, Lnet/gogame/gowrap/io/utils/FileUtils;->copy(Ljava/io/File;Ljava/io/File;)V

    .line 39
    invoke-virtual {p0}, Ljava/io/File;->delete()Z

    return-void
.end method

.method public static toString(Ljava/io/File;Ljava/lang/String;)Ljava/lang/String;
    .locals 2
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    const/4 v0, 0x0

    .line 65
    :try_start_0
    new-instance v1, Ljava/io/FileInputStream;

    invoke-direct {v1, p0}, Ljava/io/FileInputStream;-><init>(Ljava/io/File;)V
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_1

    .line 66
    :try_start_1
    invoke-static {v1, p1}, Lnet/gogame/gowrap/io/utils/IOUtils;->toString(Ljava/io/InputStream;Ljava/lang/String;)Ljava/lang/String;

    move-result-object p0
    :try_end_1
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    .line 68
    invoke-static {v1}, Lnet/gogame/gowrap/io/utils/IOUtils;->closeQuietly(Ljava/io/InputStream;)V

    return-object p0

    :catchall_0
    move-exception p0

    move-object v0, v1

    goto :goto_0

    :catchall_1
    move-exception p0

    :goto_0
    invoke-static {v0}, Lnet/gogame/gowrap/io/utils/IOUtils;->closeQuietly(Ljava/io/InputStream;)V

    .line 69
    throw p0
.end method

.method public static touch(Ljava/io/File;)V
    .locals 1
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    .line 45
    :try_start_0
    new-instance v0, Ljava/io/FileOutputStream;

    invoke-direct {v0, p0}, Ljava/io/FileOutputStream;-><init>(Ljava/io/File;)V
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    .line 47
    invoke-static {v0}, Lnet/gogame/gowrap/io/utils/IOUtils;->closeQuietly(Ljava/io/OutputStream;)V

    return-void

    :catchall_0
    move-exception p0

    const/4 v0, 0x0

    invoke-static {v0}, Lnet/gogame/gowrap/io/utils/IOUtils;->closeQuietly(Ljava/io/OutputStream;)V

    .line 48
    throw p0
.end method
