.class public Lcom/helpshift/android/commons/downloader/HsUriUtils;
.super Ljava/lang/Object;
.source "HsUriUtils.java"


# direct methods
.method public constructor <init>()V
    .locals 0

    .line 10
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method

.method public static canReadFileAtUri(Landroid/content/Context;Ljava/lang/String;)Z
    .locals 3

    .line 26
    invoke-static {p1}, Lcom/helpshift/android/commons/downloader/HsUriUtils;->isValidUriPath(Ljava/lang/String;)Z

    move-result v0

    const/4 v1, 0x0

    if-nez v0, :cond_0

    return v1

    :cond_0
    const/4 v0, 0x0

    .line 33
    :try_start_0
    invoke-static {p1}, Landroid/net/Uri;->parse(Ljava/lang/String;)Landroid/net/Uri;

    move-result-object p1

    .line 34
    invoke-virtual {p0}, Landroid/content/Context;->getContentResolver()Landroid/content/ContentResolver;

    move-result-object p0

    const-string v2, "r"

    .line 35
    invoke-virtual {p0, p1, v2}, Landroid/content/ContentResolver;->openFileDescriptor(Landroid/net/Uri;Ljava/lang/String;)Landroid/os/ParcelFileDescriptor;

    move-result-object p0
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    if-eqz p0, :cond_1

    const/4 p1, 0x1

    const/4 v1, 0x1

    .line 44
    :cond_1
    invoke-static {p0}, Lcom/helpshift/android/commons/downloader/HsUriUtils;->closeParcelFileDescriptor(Landroid/os/ParcelFileDescriptor;)V

    goto :goto_0

    :catchall_0
    move-exception p0

    invoke-static {v0}, Lcom/helpshift/android/commons/downloader/HsUriUtils;->closeParcelFileDescriptor(Landroid/os/ParcelFileDescriptor;)V

    .line 45
    throw p0

    .line 44
    :catch_0
    invoke-static {v0}, Lcom/helpshift/android/commons/downloader/HsUriUtils;->closeParcelFileDescriptor(Landroid/os/ParcelFileDescriptor;)V

    :goto_0
    return v1
.end method

.method public static closeParcelFileDescriptor(Landroid/os/ParcelFileDescriptor;)V
    .locals 0

    if-eqz p0, :cond_0

    .line 52
    :try_start_0
    invoke-virtual {p0}, Landroid/os/ParcelFileDescriptor;->close()V
    :try_end_0
    .catch Ljava/io/IOException; {:try_start_0 .. :try_end_0} :catch_0

    :catch_0
    :cond_0
    return-void
.end method

.method public static isValidUriPath(Ljava/lang/String;)Z
    .locals 1

    if-eqz p0, :cond_1

    .line 13
    invoke-virtual {p0}, Ljava/lang/String;->length()I

    move-result v0

    if-nez v0, :cond_0

    goto :goto_0

    :cond_0
    const-string v0, "content://"

    .line 22
    invoke-virtual {p0, v0}, Ljava/lang/String;->startsWith(Ljava/lang/String;)Z

    move-result p0

    return p0

    :cond_1
    :goto_0
    const/4 p0, 0x0

    return p0
.end method
