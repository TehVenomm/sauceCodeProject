.class public final Lnet/gogame/gopay/sdk/support/v;
.super Ljava/lang/Object;


# direct methods
.method public static a(Ljava/lang/String;Ljava/lang/String;)V
    .locals 3

    new-instance v0, Ljava/io/File;

    invoke-direct {v0, p1}, Ljava/io/File;-><init>(Ljava/lang/String;)V

    invoke-virtual {v0}, Ljava/io/File;->exists()Z

    move-result v1

    if-nez v1, :cond_0

    invoke-virtual {v0}, Ljava/io/File;->mkdir()Z

    :cond_0
    new-instance v0, Ljava/util/zip/ZipInputStream;

    new-instance v1, Ljava/io/FileInputStream;

    invoke-direct {v1, p0}, Ljava/io/FileInputStream;-><init>(Ljava/lang/String;)V

    invoke-direct {v0, v1}, Ljava/util/zip/ZipInputStream;-><init>(Ljava/io/InputStream;)V

    invoke-virtual {v0}, Ljava/util/zip/ZipInputStream;->getNextEntry()Ljava/util/zip/ZipEntry;

    move-result-object p0

    invoke-virtual {p0}, Ljava/util/zip/ZipEntry;->getName()Ljava/lang/String;

    move-result-object v1

    const-string v2, "assets"

    invoke-virtual {v1, v2}, Ljava/lang/String;->contains(Ljava/lang/CharSequence;)Z

    move-result v1

    if-nez v1, :cond_1

    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    invoke-virtual {v1, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string p1, "/assets"

    invoke-virtual {v1, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p1

    :cond_1
    :goto_0
    if-eqz p0, :cond_4

    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    invoke-virtual {v1, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    sget-object v2, Ljava/io/File;->separator:Ljava/lang/String;

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p0}, Ljava/util/zip/ZipEntry;->getName()Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    new-instance v2, Ljava/io/File;

    invoke-direct {v2, v1}, Ljava/io/File;-><init>(Ljava/lang/String;)V

    invoke-virtual {p0}, Ljava/util/zip/ZipEntry;->isDirectory()Z

    move-result p0

    if-nez p0, :cond_3

    invoke-virtual {v2}, Ljava/io/File;->getParentFile()Ljava/io/File;

    move-result-object p0

    invoke-virtual {p0}, Ljava/io/File;->exists()Z

    move-result p0

    if-nez p0, :cond_2

    invoke-virtual {v2}, Ljava/io/File;->getParentFile()Ljava/io/File;

    move-result-object p0

    invoke-virtual {p0}, Ljava/io/File;->mkdir()Z

    :cond_2
    invoke-static {v0, v1}, Lnet/gogame/gopay/sdk/support/v;->a(Ljava/util/zip/ZipInputStream;Ljava/lang/String;)V

    goto :goto_1

    :cond_3
    invoke-virtual {v2}, Ljava/io/File;->mkdir()Z

    :goto_1
    invoke-virtual {v0}, Ljava/util/zip/ZipInputStream;->closeEntry()V

    invoke-virtual {v0}, Ljava/util/zip/ZipInputStream;->getNextEntry()Ljava/util/zip/ZipEntry;

    move-result-object p0

    goto :goto_0

    :cond_4
    invoke-virtual {v0}, Ljava/util/zip/ZipInputStream;->close()V

    return-void
.end method

.method private static a(Ljava/util/zip/ZipInputStream;Ljava/lang/String;)V
    .locals 3

    new-instance v0, Ljava/io/BufferedOutputStream;

    new-instance v1, Ljava/io/FileOutputStream;

    invoke-direct {v1, p1}, Ljava/io/FileOutputStream;-><init>(Ljava/lang/String;)V

    invoke-direct {v0, v1}, Ljava/io/BufferedOutputStream;-><init>(Ljava/io/OutputStream;)V

    const/16 p1, 0x1000

    new-array p1, p1, [B

    :goto_0
    invoke-virtual {p0, p1}, Ljava/util/zip/ZipInputStream;->read([B)I

    move-result v1

    const/4 v2, -0x1

    if-eq v1, v2, :cond_0

    const/4 v2, 0x0

    invoke-virtual {v0, p1, v2, v1}, Ljava/io/BufferedOutputStream;->write([BII)V

    goto :goto_0

    :cond_0
    invoke-virtual {v0}, Ljava/io/BufferedOutputStream;->close()V

    return-void
.end method
