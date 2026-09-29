.class public final Lcom/helpshift/support/util/AttachmentUtil;
.super Ljava/lang/Object;
.source "AttachmentUtil.java"


# static fields
.field private static final IMAGE_MAX_DIMENSION:I = 0x400

.field private static final TAG:Ljava/lang/String; = "Helpshift_AttachUtil"


# direct methods
.method public constructor <init>()V
    .locals 0

    .line 39
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method

.method private static buildLocalAttachmentCopyFileName(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;
    .locals 1
    .param p0    # Ljava/lang/String;
        .annotation build Landroidx/annotation/Nullable;
        .end annotation
    .end param

    if-nez p0, :cond_0

    .line 261
    new-instance p0, Ljava/lang/StringBuilder;

    invoke-direct {p0}, Ljava/lang/StringBuilder;-><init>()V

    const-string v0, "localRscMessage_"

    invoke-virtual {p0, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-static {}, Ljava/util/UUID;->randomUUID()Ljava/util/UUID;

    move-result-object v0

    invoke-virtual {v0}, Ljava/util/UUID;->toString()Ljava/lang/String;

    move-result-object v0

    invoke-virtual {p0, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p0

    .line 266
    :cond_0
    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    invoke-virtual {v0, p0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string p0, "0-thumbnail"

    invoke-virtual {v0, p0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p0

    if-eqz p1, :cond_1

    .line 268
    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    invoke-virtual {v0, p0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string p0, "."

    invoke-virtual {v0, p0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p0

    :cond_1
    return-object p0
.end method

.method public static copyAttachment(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;
    .locals 7
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    .line 176
    invoke-static {}, Lcom/helpshift/util/HelpshiftContext;->getApplicationContext()Landroid/content/Context;

    move-result-object v0

    .line 177
    new-instance v1, Lcom/helpshift/support/HSApiData;

    invoke-direct {v1, v0}, Lcom/helpshift/support/HSApiData;-><init>(Landroid/content/Context;)V

    .line 180
    invoke-static {p0}, Lcom/helpshift/util/FileUtil;->getFileExtension(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v2

    const/4 v3, 0x0

    .line 182
    :try_start_0
    invoke-static {p1, v2}, Lcom/helpshift/support/util/AttachmentUtil;->buildLocalAttachmentCopyFileName(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object p1

    .line 183
    new-instance v2, Ljava/io/File;

    invoke-virtual {v0}, Landroid/content/Context;->getFilesDir()Ljava/io/File;

    move-result-object v4

    invoke-direct {v2, v4, p1}, Ljava/io/File;-><init>(Ljava/io/File;Ljava/lang/String;)V

    .line 184
    invoke-virtual {v2}, Ljava/io/File;->getAbsolutePath()Ljava/lang/String;

    move-result-object v4

    .line 185
    invoke-virtual {v2}, Ljava/io/File;->exists()Z

    move-result v2

    if-nez v2, :cond_1

    .line 186
    invoke-virtual {v1, p1}, Lcom/helpshift/support/HSApiData;->storeFile(Ljava/lang/String;)V

    .line 187
    new-instance v1, Ljava/io/FileInputStream;

    new-instance v2, Ljava/io/File;

    invoke-direct {v2, p0}, Ljava/io/File;-><init>(Ljava/lang/String;)V

    invoke-direct {v1, v2}, Ljava/io/FileInputStream;-><init>(Ljava/io/File;)V
    :try_end_0
    .catch Ljava/lang/NullPointerException; {:try_start_0 .. :try_end_0} :catch_2
    .catchall {:try_start_0 .. :try_end_0} :catchall_1

    const/4 p0, 0x0

    .line 188
    :try_start_1
    invoke-virtual {v0, p1, p0}, Landroid/content/Context;->openFileOutput(Ljava/lang/String;I)Ljava/io/FileOutputStream;

    move-result-object p1
    :try_end_1
    .catch Ljava/lang/NullPointerException; {:try_start_1 .. :try_end_1} :catch_1
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    const/16 v0, 0x400

    .line 191
    :try_start_2
    new-array v2, v0, [B

    .line 192
    :goto_0
    invoke-virtual {v1, v2}, Ljava/io/InputStream;->read([B)I

    move-result v5

    const/4 v6, -0x1

    if-eq v5, v6, :cond_0

    .line 193
    invoke-virtual {p1, v2, p0, v5}, Ljava/io/FileOutputStream;->write([BII)V

    goto :goto_0

    .line 195
    :cond_0
    invoke-static {v4}, Lcom/helpshift/util/ImageUtil;->isResizableImage(Ljava/lang/String;)Z

    move-result p0

    if-eqz p0, :cond_2

    .line 196
    invoke-static {v4, v0}, Lcom/helpshift/util/ImageUtil;->scaleDownAndSaveWithMaxDimension(Ljava/lang/String;I)V
    :try_end_2
    .catch Ljava/lang/NullPointerException; {:try_start_2 .. :try_end_2} :catch_0
    .catchall {:try_start_2 .. :try_end_2} :catchall_2

    goto :goto_1

    :catch_0
    move-exception p0

    goto :goto_2

    :catchall_0
    move-exception p0

    goto :goto_3

    :catch_1
    move-exception p0

    move-object p1, v3

    goto :goto_2

    :cond_1
    move-object p1, v3

    move-object v1, p1

    .line 207
    :cond_2
    :goto_1
    invoke-static {p1}, Lcom/helpshift/util/IOUtils;->closeQuitely(Ljava/io/Closeable;)V

    .line 208
    invoke-static {v1}, Lcom/helpshift/util/IOUtils;->closeQuitely(Ljava/io/Closeable;)V

    return-object v4

    :catchall_1
    move-exception p0

    move-object v1, v3

    goto :goto_3

    :catch_2
    move-exception p0

    move-object p1, v3

    move-object v1, p1

    :goto_2
    :try_start_3
    const-string v0, "Helpshift_AttachUtil"

    const-string v2, "NPE"

    .line 203
    invoke-static {v0, v2, p0}, Lcom/helpshift/util/HSLogger;->d(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)V
    :try_end_3
    .catchall {:try_start_3 .. :try_end_3} :catchall_2

    .line 207
    invoke-static {p1}, Lcom/helpshift/util/IOUtils;->closeQuitely(Ljava/io/Closeable;)V

    .line 208
    invoke-static {v1}, Lcom/helpshift/util/IOUtils;->closeQuitely(Ljava/io/Closeable;)V

    return-object v3

    :catchall_2
    move-exception p0

    move-object v3, p1

    .line 207
    :goto_3
    invoke-static {v3}, Lcom/helpshift/util/IOUtils;->closeQuitely(Ljava/io/Closeable;)V

    .line 208
    invoke-static {v1}, Lcom/helpshift/util/IOUtils;->closeQuitely(Ljava/io/Closeable;)V

    .line 209
    throw p0
.end method

.method public static copyAttachment(Lcom/helpshift/conversation/dto/ImagePickerFile;Ljava/lang/String;)V
    .locals 7
    .param p0    # Lcom/helpshift/conversation/dto/ImagePickerFile;
        .annotation build Landroidx/annotation/NonNull;
        .end annotation
    .end param
    .param p1    # Ljava/lang/String;
        .annotation build Landroidx/annotation/Nullable;
        .end annotation
    .end param
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    .line 214
    iget-object v0, p0, Lcom/helpshift/conversation/dto/ImagePickerFile;->transientUri:Ljava/lang/Object;

    check-cast v0, Landroid/net/Uri;

    if-nez v0, :cond_0

    const-string p0, "Helpshift_AttachUtil"

    const-string p1, "Can\'t proceed if uri is null"

    .line 216
    invoke-static {p0, p1}, Lcom/helpshift/util/HSLogger;->d(Ljava/lang/String;Ljava/lang/String;)V

    return-void

    .line 219
    :cond_0
    invoke-static {}, Lcom/helpshift/util/HelpshiftContext;->getApplicationContext()Landroid/content/Context;

    move-result-object v1

    .line 220
    new-instance v2, Lcom/helpshift/support/HSApiData;

    invoke-direct {v2, v1}, Lcom/helpshift/support/HSApiData;-><init>(Landroid/content/Context;)V

    const/4 v3, 0x0

    .line 225
    :try_start_0
    invoke-static {v1, v0}, Lcom/helpshift/util/FileUtil;->getFileExtensionFromMimeType(Landroid/content/Context;Landroid/net/Uri;)Ljava/lang/String;

    move-result-object v4

    .line 226
    invoke-static {p1, v4}, Lcom/helpshift/support/util/AttachmentUtil;->buildLocalAttachmentCopyFileName(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object p1

    .line 227
    new-instance v4, Ljava/io/File;

    invoke-virtual {v1}, Landroid/content/Context;->getFilesDir()Ljava/io/File;

    move-result-object v5

    invoke-direct {v4, v5, p1}, Ljava/io/File;-><init>(Ljava/io/File;Ljava/lang/String;)V

    .line 228
    invoke-virtual {v4}, Ljava/io/File;->getAbsolutePath()Ljava/lang/String;

    move-result-object v5

    .line 229
    invoke-virtual {v4}, Ljava/io/File;->exists()Z

    move-result v4

    const/4 v6, 0x1

    if-nez v4, :cond_2

    .line 230
    invoke-virtual {v2, p1}, Lcom/helpshift/support/HSApiData;->storeFile(Ljava/lang/String;)V

    .line 231
    invoke-virtual {v1}, Landroid/content/Context;->getContentResolver()Landroid/content/ContentResolver;

    move-result-object v2

    invoke-virtual {v2, v0}, Landroid/content/ContentResolver;->openInputStream(Landroid/net/Uri;)Ljava/io/InputStream;

    move-result-object v0
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_2

    const/4 v2, 0x0

    .line 232
    :try_start_1
    invoke-virtual {v1, p1, v2}, Landroid/content/Context;->openFileOutput(Ljava/lang/String;I)Ljava/io/FileOutputStream;

    move-result-object p1
    :try_end_1
    .catchall {:try_start_1 .. :try_end_1} :catchall_1

    const/16 v1, 0x2000

    .line 235
    :try_start_2
    new-array v1, v1, [B

    .line 236
    :goto_0
    invoke-virtual {v0, v1}, Ljava/io/InputStream;->read([B)I

    move-result v3

    const/4 v4, -0x1

    if-eq v3, v4, :cond_1

    .line 237
    invoke-virtual {p1, v1, v2, v3}, Ljava/io/FileOutputStream;->write([BII)V

    goto :goto_0

    .line 239
    :cond_1
    iput-object v5, p0, Lcom/helpshift/conversation/dto/ImagePickerFile;->filePath:Ljava/lang/String;

    .line 240
    iput-boolean v6, p0, Lcom/helpshift/conversation/dto/ImagePickerFile;->isFileCompressionAndCopyingDone:Z

    .line 241
    invoke-static {v5}, Lcom/helpshift/util/ImageUtil;->isResizableImage(Ljava/lang/String;)Z

    move-result p0

    if-eqz p0, :cond_3

    const/16 p0, 0x400

    .line 242
    invoke-static {v5, p0}, Lcom/helpshift/util/ImageUtil;->scaleDownAndSaveWithMaxDimension(Ljava/lang/String;I)V
    :try_end_2
    .catchall {:try_start_2 .. :try_end_2} :catchall_0

    goto :goto_1

    :catchall_0
    move-exception p0

    move-object v3, p1

    goto :goto_2

    :catchall_1
    move-exception p0

    goto :goto_2

    .line 247
    :cond_2
    :try_start_3
    iput-object v5, p0, Lcom/helpshift/conversation/dto/ImagePickerFile;->filePath:Ljava/lang/String;

    .line 248
    iput-boolean v6, p0, Lcom/helpshift/conversation/dto/ImagePickerFile;->isFileCompressionAndCopyingDone:Z
    :try_end_3
    .catchall {:try_start_3 .. :try_end_3} :catchall_2

    move-object p1, v3

    move-object v0, p1

    .line 252
    :cond_3
    :goto_1
    invoke-static {p1}, Lcom/helpshift/util/IOUtils;->closeQuitely(Ljava/io/Closeable;)V

    .line 253
    invoke-static {v0}, Lcom/helpshift/util/IOUtils;->closeQuitely(Ljava/io/Closeable;)V

    return-void

    :catchall_2
    move-exception p0

    move-object v0, v3

    .line 252
    :goto_2
    invoke-static {v3}, Lcom/helpshift/util/IOUtils;->closeQuitely(Ljava/io/Closeable;)V

    .line 253
    invoke-static {v0}, Lcom/helpshift/util/IOUtils;->closeQuitely(Ljava/io/Closeable;)V

    .line 254
    throw p0
.end method

.method private static doesFilePathExistAndCanRead(Ljava/lang/String;)Z
    .locals 2

    .line 167
    invoke-static {p0}, Landroid/text/TextUtils;->isEmpty(Ljava/lang/CharSequence;)Z

    move-result v0

    const/4 v1, 0x0

    if-nez v0, :cond_1

    .line 168
    new-instance v0, Ljava/io/File;

    invoke-direct {v0, p0}, Ljava/io/File;-><init>(Ljava/lang/String;)V

    .line 169
    invoke-virtual {v0}, Ljava/io/File;->exists()Z

    move-result p0

    if-eqz p0, :cond_0

    invoke-virtual {v0}, Ljava/io/File;->canRead()Z

    move-result p0

    if-eqz p0, :cond_0

    const/4 v1, 0x1

    :cond_0
    return v1

    :cond_1
    return v1
.end method

.method public static getBitmap(Ljava/lang/String;IZ)Landroid/graphics/Bitmap;
    .locals 3

    .line 144
    invoke-static {p0}, Lcom/helpshift/android/commons/downloader/HsUriUtils;->isValidUriPath(Ljava/lang/String;)Z

    move-result v0

    if-eqz v0, :cond_1

    const/4 v0, 0x0

    .line 147
    :try_start_0
    invoke-static {p0}, Landroid/net/Uri;->parse(Ljava/lang/String;)Landroid/net/Uri;

    move-result-object p0
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception p0

    const-string v1, "Helpshift_AttachUtil"

    const-string v2, "Error while converting to uri from file path"

    .line 150
    invoke-static {v1, v2, p0}, Lcom/helpshift/util/HSLogger;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)V

    move-object p0, v0

    :goto_0
    if-eqz p0, :cond_0

    .line 153
    sget v1, Landroid/os/Build$VERSION;->SDK_INT:I

    const/16 v2, 0x1c

    if-lt v1, v2, :cond_0

    .line 154
    invoke-static {p0, p1, p2}, Lcom/helpshift/support/util/AttachmentUtil;->loadBitmap(Landroid/net/Uri;IZ)Landroid/graphics/Bitmap;

    move-result-object p0

    return-object p0

    :cond_0
    return-object v0

    .line 162
    :cond_1
    invoke-static {p0, p1}, Lcom/helpshift/support/util/AttachmentUtil;->loadBitmap(Ljava/lang/String;I)Landroid/graphics/Bitmap;

    move-result-object p0

    return-object p0
.end method

.method private static getExifRotation(Ljava/lang/String;)I
    .locals 3

    const/4 v0, 0x0

    .line 292
    :try_start_0
    invoke-static {p0}, Lcom/helpshift/util/FileUtil;->getMimeType(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v1

    if-eqz v1, :cond_3

    const-string v2, "jpeg"

    .line 293
    invoke-virtual {v1, v2}, Ljava/lang/String;->contains(Ljava/lang/CharSequence;)Z

    move-result v1

    if-eqz v1, :cond_3

    .line 294
    new-instance v1, Landroid/media/ExifInterface;

    invoke-direct {v1, p0}, Landroid/media/ExifInterface;-><init>(Ljava/lang/String;)V

    const-string p0, "Orientation"

    const/4 v2, 0x1

    .line 295
    invoke-virtual {v1, p0, v2}, Landroid/media/ExifInterface;->getAttributeInt(Ljava/lang/String;I)I

    move-result p0
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    const/4 v1, 0x6

    if-ne p0, v1, :cond_0

    const/16 p0, 0x5a

    return p0

    :cond_0
    const/4 v1, 0x3

    if-ne p0, v1, :cond_1

    const/16 p0, 0xb4

    return p0

    :cond_1
    const/16 v1, 0x8

    if-ne p0, v1, :cond_2

    const/16 p0, 0x10e

    return p0

    :cond_2
    return v0

    :catch_0
    move-exception p0

    const-string v1, "Helpshift_AttachUtil"

    const-string v2, "Exception in getting exif rotation"

    .line 312
    invoke-static {v1, v2, p0}, Lcom/helpshift/util/HSLogger;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)V

    :cond_3
    return v0
.end method

.method public static getFileName(Ljava/lang/String;)Ljava/lang/String;
    .locals 1

    const-string v0, ""

    if-eqz p0, :cond_0

    .line 276
    new-instance v0, Ljava/io/File;

    invoke-direct {v0, p0}, Ljava/io/File;-><init>(Ljava/lang/String;)V

    .line 277
    invoke-virtual {v0}, Ljava/io/File;->getName()Ljava/lang/String;

    move-result-object v0

    :cond_0
    return-object v0
.end method

.method private static loadBitmap(Landroid/net/Uri;IZ)Landroid/graphics/Bitmap;
    .locals 1
    .annotation build Landroidx/annotation/RequiresApi;
        value = 0x1c
    .end annotation

    .line 47
    invoke-static {}, Lcom/helpshift/util/HelpshiftContext;->getApplicationContext()Landroid/content/Context;

    move-result-object v0

    invoke-virtual {v0}, Landroid/content/Context;->getContentResolver()Landroid/content/ContentResolver;

    move-result-object v0

    .line 50
    :try_start_0
    invoke-static {v0, p0}, Landroid/graphics/ImageDecoder;->createSource(Landroid/content/ContentResolver;Landroid/net/Uri;)Landroid/graphics/ImageDecoder$Source;

    move-result-object p0

    .line 51
    new-instance v0, Lcom/helpshift/support/util/AttachmentUtil$1;

    invoke-direct {v0, p1, p2}, Lcom/helpshift/support/util/AttachmentUtil$1;-><init>(IZ)V

    .line 85
    invoke-static {p0, v0}, Landroid/graphics/ImageDecoder;->decodeBitmap(Landroid/graphics/ImageDecoder$Source;Landroid/graphics/ImageDecoder$OnHeaderDecodedListener;)Landroid/graphics/Bitmap;

    move-result-object p0
    :try_end_0
    .catch Ljava/io/IOException; {:try_start_0 .. :try_end_0} :catch_0

    return-object p0

    :catch_0
    move-exception p0

    const-string p1, "Helpshift_AttachUtil"

    const-string p2, "Error while building bitmap from uri"

    .line 88
    invoke-static {p1, p2, p0}, Lcom/helpshift/util/HSLogger;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)V

    const/4 p0, 0x0

    return-object p0
.end method

.method private static loadBitmap(Ljava/lang/String;I)Landroid/graphics/Bitmap;
    .locals 8

    .line 95
    invoke-static {p0}, Lcom/helpshift/support/util/AttachmentUtil;->doesFilePathExistAndCanRead(Ljava/lang/String;)Z

    move-result v0

    if-nez v0, :cond_0

    const/4 p0, 0x0

    return-object p0

    .line 100
    :cond_0
    new-instance v0, Landroid/graphics/BitmapFactory$Options;

    invoke-direct {v0}, Landroid/graphics/BitmapFactory$Options;-><init>()V

    const/4 v1, 0x4

    if-lez p1, :cond_1

    .line 104
    iget v2, v0, Landroid/graphics/BitmapFactory$Options;->outWidth:I

    iget v3, v0, Landroid/graphics/BitmapFactory$Options;->outHeight:I

    invoke-static {v2, v3, p1}, Lcom/helpshift/util/ImageUtil;->calculateReqHeight(III)I

    move-result v2

    const/4 v3, 0x1

    .line 105
    iput-boolean v3, v0, Landroid/graphics/BitmapFactory$Options;->inJustDecodeBounds:Z

    .line 106
    invoke-static {p0, v0}, Landroid/graphics/BitmapFactory;->decodeFile(Ljava/lang/String;Landroid/graphics/BitmapFactory$Options;)Landroid/graphics/Bitmap;

    .line 107
    invoke-static {v0, p1, v2}, Lcom/helpshift/util/ImageUtil;->calculateInSampleSize(Landroid/graphics/BitmapFactory$Options;II)I

    move-result p1

    iput p1, v0, Landroid/graphics/BitmapFactory$Options;->inSampleSize:I

    .line 111
    iget p1, v0, Landroid/graphics/BitmapFactory$Options;->inSampleSize:I

    if-ge p1, v1, :cond_2

    .line 112
    iget p1, v0, Landroid/graphics/BitmapFactory$Options;->inSampleSize:I

    add-int/2addr p1, v3

    iput p1, v0, Landroid/graphics/BitmapFactory$Options;->inSampleSize:I

    goto :goto_0

    .line 118
    :cond_1
    iput v1, v0, Landroid/graphics/BitmapFactory$Options;->inSampleSize:I

    :cond_2
    :goto_0
    const/4 p1, 0x0

    .line 120
    iput-boolean p1, v0, Landroid/graphics/BitmapFactory$Options;->inJustDecodeBounds:Z

    .line 121
    invoke-static {p0, v0}, Landroid/graphics/BitmapFactory;->decodeFile(Ljava/lang/String;Landroid/graphics/BitmapFactory$Options;)Landroid/graphics/Bitmap;

    move-result-object v1

    if-eqz v1, :cond_3

    .line 130
    invoke-static {p0}, Lcom/helpshift/support/util/AttachmentUtil;->getExifRotation(Ljava/lang/String;)I

    move-result p0

    if-eqz p0, :cond_3

    .line 132
    new-instance v6, Landroid/graphics/Matrix;

    invoke-direct {v6}, Landroid/graphics/Matrix;-><init>()V

    int-to-float p0, p0

    .line 133
    invoke-virtual {v6, p0}, Landroid/graphics/Matrix;->preRotate(F)Z

    const/4 v2, 0x0

    const/4 v3, 0x0

    .line 135
    invoke-virtual {v1}, Landroid/graphics/Bitmap;->getWidth()I

    move-result v4

    invoke-virtual {v1}, Landroid/graphics/Bitmap;->getHeight()I

    move-result v5

    const/4 v7, 0x0

    invoke-static/range {v1 .. v7}, Landroid/graphics/Bitmap;->createBitmap(Landroid/graphics/Bitmap;IIIILandroid/graphics/Matrix;Z)Landroid/graphics/Bitmap;

    move-result-object v1

    :cond_3
    return-object v1
.end method
