.class public Lcom/helpshift/util/FileUtil;
.super Ljava/lang/Object;
.source "FileUtil.java"


# static fields
.field public static final TAG:Ljava/lang/String; = "FileUtil"

.field private static final imageMimeTypes:Ljava/util/Set;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/Set<",
            "Ljava/lang/String;",
            ">;"
        }
    .end annotation
.end field


# direct methods
.method static constructor <clinit>()V
    .locals 8

    .line 29
    new-instance v0, Ljava/util/HashSet;

    const-string v1, "image/jpeg"

    const-string v2, "image/png"

    const-string v3, "image/gif"

    const-string v4, "image/x-png"

    const-string v5, "image/x-citrix-pjpeg"

    const-string v6, "image/x-citrix-gif"

    const-string v7, "image/pjpeg"

    filled-new-array/range {v1 .. v7}, [Ljava/lang/String;

    move-result-object v1

    .line 30
    invoke-static {v1}, Ljava/util/Arrays;->asList([Ljava/lang/Object;)Ljava/util/List;

    move-result-object v1

    invoke-direct {v0, v1}, Ljava/util/HashSet;-><init>(Ljava/util/Collection;)V

    sput-object v0, Lcom/helpshift/util/FileUtil;->imageMimeTypes:Ljava/util/Set;

    return-void
.end method

.method public constructor <init>()V
    .locals 0

    .line 25
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method

.method public static doesFileFromUriExistAndCanRead(Landroid/net/Uri;Landroid/content/Context;)Z
    .locals 4

    const/4 v0, 0x0

    .line 118
    :try_start_0
    invoke-virtual {p1}, Landroid/content/Context;->getContentResolver()Landroid/content/ContentResolver;

    move-result-object p1

    const-string v1, "r"

    invoke-virtual {p1, p0, v1}, Landroid/content/ContentResolver;->openFileDescriptor(Landroid/net/Uri;Ljava/lang/String;)Landroid/os/ParcelFileDescriptor;

    move-result-object p1
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_1
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    if-eqz p1, :cond_0

    const/4 v0, 0x1

    :cond_0
    if-eqz p1, :cond_1

    .line 129
    :try_start_1
    invoke-virtual {p1}, Landroid/os/ParcelFileDescriptor;->close()V
    :try_end_1
    .catch Ljava/io/IOException; {:try_start_1 .. :try_end_1} :catch_0

    :catch_0
    :cond_1
    return v0

    :catchall_0
    move-exception p0

    goto :goto_0

    :catch_1
    move-exception p1

    .line 122
    :try_start_2
    sget-object v1, Lcom/helpshift/util/FileUtil;->TAG:Ljava/lang/String;

    new-instance v2, Ljava/lang/StringBuilder;

    invoke-direct {v2}, Ljava/lang/StringBuilder;-><init>()V

    const-string v3, "Unable to open input file descriptor for doesFileFromUriExistAndCanRead: "

    invoke-virtual {v2, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v2, p0}, Ljava/lang/StringBuilder;->append(Ljava/lang/Object;)Ljava/lang/StringBuilder;

    invoke-virtual {v2}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p0

    .line 123
    invoke-static {v1, p0, p1}, Lcom/helpshift/util/HSLogger;->d(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)V
    :try_end_2
    .catchall {:try_start_2 .. :try_end_2} :catchall_0

    return v0

    .line 134
    :goto_0
    throw p0
.end method

.method public static getFileExtension(Landroid/content/Context;Landroid/net/Uri;)Ljava/lang/String;
    .locals 7

    const/4 v0, 0x0

    if-eqz p1, :cond_5

    if-nez p0, :cond_0

    goto :goto_3

    .line 197
    :cond_0
    :try_start_0
    invoke-virtual {p0}, Landroid/content/Context;->getContentResolver()Landroid/content/ContentResolver;

    move-result-object v1

    const/4 v3, 0x0

    const/4 v4, 0x0

    const/4 v5, 0x0

    const/4 v6, 0x0

    move-object v2, p1

    invoke-virtual/range {v1 .. v6}, Landroid/content/ContentResolver;->query(Landroid/net/Uri;[Ljava/lang/String;Ljava/lang/String;[Ljava/lang/String;Ljava/lang/String;)Landroid/database/Cursor;

    move-result-object p0
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    if-eqz p0, :cond_2

    .line 199
    :try_start_1
    invoke-interface {p0}, Landroid/database/Cursor;->moveToFirst()Z

    move-result p1

    if-eqz p1, :cond_2

    const-string p1, "_display_name"

    .line 200
    invoke-interface {p0, p1}, Landroid/database/Cursor;->getColumnIndex(Ljava/lang/String;)I

    move-result p1

    .line 201
    invoke-interface {p0, p1}, Landroid/database/Cursor;->getString(I)Ljava/lang/String;

    move-result-object p1

    .line 202
    invoke-static {p1}, Lcom/helpshift/util/FileUtil;->getFileExtensionFromFileName(Ljava/lang/String;)Ljava/lang/String;

    move-result-object p1
    :try_end_1
    .catch Ljava/lang/Exception; {:try_start_1 .. :try_end_1} :catch_1
    .catchall {:try_start_1 .. :try_end_1} :catchall_1

    if-eqz p0, :cond_1

    .line 210
    invoke-interface {p0}, Landroid/database/Cursor;->close()V

    :cond_1
    return-object p1

    :cond_2
    if-eqz p0, :cond_3

    :goto_0
    invoke-interface {p0}, Landroid/database/Cursor;->close()V

    goto :goto_1

    :catchall_0
    move-exception p1

    move-object p0, v0

    goto :goto_2

    :catch_0
    move-object p0, v0

    .line 206
    :catch_1
    :try_start_2
    sget-object p1, Lcom/helpshift/util/FileUtil;->TAG:Ljava/lang/String;

    const-string v1, "Unable to detect file extension via Uri"

    invoke-static {p1, v1}, Lcom/helpshift/util/HSLogger;->e(Ljava/lang/String;Ljava/lang/String;)V
    :try_end_2
    .catchall {:try_start_2 .. :try_end_2} :catchall_1

    if-eqz p0, :cond_3

    goto :goto_0

    :cond_3
    :goto_1
    return-object v0

    :catchall_1
    move-exception p1

    :goto_2
    if-eqz p0, :cond_4

    .line 210
    invoke-interface {p0}, Landroid/database/Cursor;->close()V

    .line 212
    :cond_4
    throw p1

    :cond_5
    :goto_3
    return-object v0
.end method

.method public static getFileExtension(Ljava/lang/String;)Ljava/lang/String;
    .locals 4

    .line 92
    invoke-static {p0}, Lcom/helpshift/common/StringUtils;->isEmpty(Ljava/lang/String;)Z

    move-result v0

    const/4 v1, 0x0

    if-eqz v0, :cond_0

    return-object v1

    :cond_0
    const/16 v0, 0x2f

    .line 95
    invoke-virtual {p0, v0}, Ljava/lang/String;->lastIndexOf(I)I

    move-result v0

    const/16 v2, 0x2e

    .line 96
    invoke-virtual {p0, v2}, Ljava/lang/String;->lastIndexOf(I)I

    move-result v2

    if-lez v2, :cond_3

    .line 103
    invoke-virtual {p0}, Ljava/lang/String;->length()I

    move-result v3

    add-int/lit8 v3, v3, -0x1

    if-lt v2, v3, :cond_1

    goto :goto_0

    :cond_1
    if-ge v0, v2, :cond_2

    add-int/lit8 v2, v2, 0x1

    .line 109
    invoke-virtual {p0, v2}, Ljava/lang/String;->substring(I)Ljava/lang/String;

    move-result-object p0

    return-object p0

    :cond_2
    return-object v1

    :cond_3
    :goto_0
    return-object v1
.end method

.method public static getFileExtensionFromFileName(Ljava/lang/String;)Ljava/lang/String;
    .locals 3

    .line 158
    invoke-static {p0}, Lcom/helpshift/common/StringUtils;->isEmpty(Ljava/lang/String;)Z

    move-result v0

    const/4 v1, 0x0

    if-eqz v0, :cond_0

    return-object v1

    :cond_0
    const/16 v0, 0x2e

    .line 161
    invoke-virtual {p0, v0}, Ljava/lang/String;->lastIndexOf(I)I

    move-result v0

    if-gtz v0, :cond_1

    return-object v1

    .line 171
    :cond_1
    invoke-virtual {p0}, Ljava/lang/String;->length()I

    move-result v2

    add-int/lit8 v2, v2, -0x1

    if-lt v0, v2, :cond_2

    return-object v1

    :cond_2
    add-int/lit8 v0, v0, 0x1

    .line 175
    invoke-virtual {p0, v0}, Ljava/lang/String;->substring(I)Ljava/lang/String;

    move-result-object p0

    const/16 v0, 0x2f

    .line 178
    invoke-virtual {p0, v0}, Ljava/lang/String;->indexOf(I)I

    move-result v0

    if-ltz v0, :cond_3

    return-object v1

    :cond_3
    return-object p0
.end method

.method public static getFileExtensionFromMimeType(Landroid/content/Context;Landroid/net/Uri;)Ljava/lang/String;
    .locals 2
    .param p1    # Landroid/net/Uri;
        .annotation build Landroidx/annotation/NonNull;
        .end annotation
    .end param

    const-string v0, "content"

    .line 140
    invoke-virtual {p1}, Landroid/net/Uri;->getScheme()Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v0

    if-eqz v0, :cond_0

    .line 141
    invoke-static {}, Landroid/webkit/MimeTypeMap;->getSingleton()Landroid/webkit/MimeTypeMap;

    move-result-object v0

    .line 142
    invoke-virtual {p0}, Landroid/content/Context;->getContentResolver()Landroid/content/ContentResolver;

    move-result-object p0

    invoke-virtual {p0, p1}, Landroid/content/ContentResolver;->getType(Landroid/net/Uri;)Ljava/lang/String;

    move-result-object p0

    invoke-virtual {v0, p0}, Landroid/webkit/MimeTypeMap;->getExtensionFromMimeType(Ljava/lang/String;)Ljava/lang/String;

    move-result-object p0

    goto :goto_0

    .line 145
    :cond_0
    new-instance p0, Ljava/io/File;

    .line 146
    invoke-virtual {p1}, Landroid/net/Uri;->getPath()Ljava/lang/String;

    move-result-object p1

    invoke-direct {p0, p1}, Ljava/io/File;-><init>(Ljava/lang/String;)V

    invoke-static {p0}, Landroid/net/Uri;->fromFile(Ljava/io/File;)Landroid/net/Uri;

    move-result-object p0

    invoke-virtual {p0}, Landroid/net/Uri;->toString()Ljava/lang/String;

    move-result-object p0

    invoke-static {p0}, Landroid/webkit/MimeTypeMap;->getFileExtensionFromUrl(Ljava/lang/String;)Ljava/lang/String;

    move-result-object p0

    :goto_0
    return-object p0
.end method

.method public static getMimeType(Ljava/lang/String;)Ljava/lang/String;
    .locals 3

    .line 77
    :try_start_0
    new-instance v0, Ljava/net/URL;

    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "file://"

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1, p0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p0

    invoke-direct {v0, p0}, Ljava/net/URL;-><init>(Ljava/lang/String;)V

    invoke-static {v0}, Lcom/helpshift/util/FileUtil;->getMimeType(Ljava/net/URL;)Ljava/lang/String;

    move-result-object p0
    :try_end_0
    .catch Ljava/net/MalformedURLException; {:try_start_0 .. :try_end_0} :catch_0

    return-object p0

    :catch_0
    move-exception p0

    .line 80
    sget-object v0, Lcom/helpshift/util/FileUtil;->TAG:Ljava/lang/String;

    const-string v1, "error in getting mimeType :"

    invoke-static {v0, v1, p0}, Lcom/helpshift/util/HSLogger;->d(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)V

    const/4 p0, 0x0

    return-object p0
.end method

.method public static getMimeType(Ljava/net/URL;)Ljava/lang/String;
    .locals 2

    .line 67
    :try_start_0
    invoke-virtual {p0}, Ljava/net/URL;->openConnection()Ljava/net/URLConnection;

    move-result-object p0

    invoke-virtual {p0}, Ljava/net/URLConnection;->getContentType()Ljava/lang/String;

    move-result-object p0
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    return-object p0

    :catch_0
    move-exception p0

    .line 70
    sget-object v0, Lcom/helpshift/util/FileUtil;->TAG:Ljava/lang/String;

    const-string v1, "openConnection() Exception :"

    invoke-static {v0, v1, p0}, Lcom/helpshift/util/HSLogger;->d(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)V

    const/4 p0, 0x0

    return-object p0
.end method

.method public static isSupportedMimeType(Ljava/lang/String;)Z
    .locals 1

    .line 39
    sget-object v0, Lcom/helpshift/util/FileUtil;->imageMimeTypes:Ljava/util/Set;

    invoke-interface {v0, p0}, Ljava/util/Set;->contains(Ljava/lang/Object;)Z

    move-result p0

    return p0
.end method

.method public static saveFile(Ljava/net/URL;Ljava/io/File;)V
    .locals 3

    const/4 v0, 0x0

    .line 47
    :try_start_0
    invoke-virtual {p0}, Ljava/net/URL;->openStream()Ljava/io/InputStream;

    move-result-object p0
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_2
    .catchall {:try_start_0 .. :try_end_0} :catchall_2

    .line 48
    :try_start_1
    new-instance v1, Ljava/io/FileOutputStream;

    invoke-direct {v1, p1}, Ljava/io/FileOutputStream;-><init>(Ljava/io/File;)V
    :try_end_1
    .catch Ljava/lang/Exception; {:try_start_1 .. :try_end_1} :catch_1
    .catchall {:try_start_1 .. :try_end_1} :catchall_1

    const/16 p1, 0x1f4

    .line 50
    :try_start_2
    new-array p1, p1, [B

    .line 52
    :goto_0
    array-length v0, p1

    const/4 v2, 0x0

    invoke-virtual {p0, p1, v2, v0}, Ljava/io/InputStream;->read([BII)I

    move-result v0

    if-ltz v0, :cond_0

    .line 53
    invoke-virtual {v1, p1, v2, v0}, Ljava/io/OutputStream;->write([BII)V
    :try_end_2
    .catch Ljava/lang/Exception; {:try_start_2 .. :try_end_2} :catch_0
    .catchall {:try_start_2 .. :try_end_2} :catchall_0

    goto :goto_0

    .line 60
    :cond_0
    invoke-static {p0}, Lcom/helpshift/util/IOUtils;->closeQuitely(Ljava/io/Closeable;)V

    goto :goto_3

    :catchall_0
    move-exception p1

    goto :goto_4

    :catch_0
    move-exception p1

    goto :goto_1

    :catchall_1
    move-exception p1

    move-object v1, v0

    goto :goto_4

    :catch_1
    move-exception p1

    move-object v1, v0

    :goto_1
    move-object v0, p0

    goto :goto_2

    :catchall_2
    move-exception p1

    move-object p0, v0

    move-object v1, p0

    goto :goto_4

    :catch_2
    move-exception p1

    move-object v1, v0

    .line 57
    :goto_2
    :try_start_3
    sget-object p0, Lcom/helpshift/util/FileUtil;->TAG:Ljava/lang/String;

    const-string v2, "saveFile Exception :"

    invoke-static {p0, v2, p1}, Lcom/helpshift/util/HSLogger;->d(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)V
    :try_end_3
    .catchall {:try_start_3 .. :try_end_3} :catchall_3

    .line 60
    invoke-static {v0}, Lcom/helpshift/util/IOUtils;->closeQuitely(Ljava/io/Closeable;)V

    .line 61
    :goto_3
    invoke-static {v1}, Lcom/helpshift/util/IOUtils;->closeQuitely(Ljava/io/Closeable;)V

    return-void

    :catchall_3
    move-exception p1

    move-object p0, v0

    .line 60
    :goto_4
    invoke-static {p0}, Lcom/helpshift/util/IOUtils;->closeQuitely(Ljava/io/Closeable;)V

    .line 61
    invoke-static {v1}, Lcom/helpshift/util/IOUtils;->closeQuitely(Ljava/io/Closeable;)V

    .line 62
    throw p1
.end method
