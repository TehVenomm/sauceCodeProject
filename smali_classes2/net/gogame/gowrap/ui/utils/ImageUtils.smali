.class public final Lnet/gogame/gowrap/ui/utils/ImageUtils;
.super Ljava/lang/Object;
.source "ImageUtils.java"


# annotations
.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Lnet/gogame/gowrap/ui/utils/ImageUtils$DrawableResourceSource;,
        Lnet/gogame/gowrap/ui/utils/ImageUtils$FileSource;,
        Lnet/gogame/gowrap/ui/utils/ImageUtils$Source;
    }
.end annotation


# direct methods
.method private constructor <init>()V
    .locals 0

    .line 20
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method

.method private static calculateInSampleSize(Landroid/graphics/BitmapFactory$Options;II)I
    .locals 3

    .line 82
    iget v0, p0, Landroid/graphics/BitmapFactory$Options;->outHeight:I

    .line 83
    iget p0, p0, Landroid/graphics/BitmapFactory$Options;->outWidth:I

    const/4 v1, 0x1

    if-gt v0, p2, :cond_0

    if-le p0, p1, :cond_1

    .line 87
    :cond_0
    div-int/lit8 v0, v0, 0x2

    .line 88
    div-int/lit8 p0, p0, 0x2

    .line 92
    :goto_0
    div-int v2, v0, v1

    if-lt v2, p2, :cond_1

    div-int v2, p0, v1

    if-lt v2, p1, :cond_1

    mul-int/lit8 v1, v1, 0x2

    goto :goto_0

    :cond_1
    return v1
.end method

.method public static getSampledBitmap(Landroid/content/Context;Lnet/gogame/gowrap/ui/utils/ImageUtils$Source;Ljava/lang/Integer;Ljava/lang/Integer;)Landroid/graphics/Bitmap;
    .locals 3
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    const/4 v0, 0x0

    if-eqz p2, :cond_1

    if-eqz p3, :cond_1

    .line 27
    new-instance v1, Landroid/graphics/BitmapFactory$Options;

    invoke-direct {v1}, Landroid/graphics/BitmapFactory$Options;-><init>()V

    const/4 v2, 0x1

    .line 28
    iput-boolean v2, v1, Landroid/graphics/BitmapFactory$Options;->inJustDecodeBounds:Z

    .line 29
    invoke-virtual {p0}, Landroid/content/Context;->getApplicationContext()Landroid/content/Context;

    move-result-object p0

    invoke-virtual {p0}, Landroid/content/Context;->getResources()Landroid/content/res/Resources;

    move-result-object p0

    .line 30
    invoke-virtual {p0}, Landroid/content/res/Resources;->getDisplayMetrics()Landroid/util/DisplayMetrics;

    move-result-object p0

    .line 31
    iget v2, p0, Landroid/util/DisplayMetrics;->densityDpi:I

    iput v2, v1, Landroid/graphics/BitmapFactory$Options;->inScreenDensity:I

    .line 32
    iget p0, p0, Landroid/util/DisplayMetrics;->densityDpi:I

    iput p0, v1, Landroid/graphics/BitmapFactory$Options;->inTargetDensity:I

    const/16 p0, 0xa0

    .line 33
    iput p0, v1, Landroid/graphics/BitmapFactory$Options;->inDensity:I

    .line 34
    invoke-interface {p1}, Lnet/gogame/gowrap/ui/utils/ImageUtils$Source;->getInputStream()Ljava/io/InputStream;

    move-result-object p0

    if-nez p0, :cond_0

    .line 41
    invoke-static {p0}, Lnet/gogame/gowrap/io/utils/IOUtils;->closeQuietly(Ljava/io/InputStream;)V

    .line 42
    invoke-interface {p1}, Lnet/gogame/gowrap/ui/utils/ImageUtils$Source;->close()V

    return-object v0

    .line 39
    :cond_0
    :try_start_0
    invoke-static {p0, v0, v1}, Landroid/graphics/BitmapFactory;->decodeStream(Ljava/io/InputStream;Landroid/graphics/Rect;Landroid/graphics/BitmapFactory$Options;)Landroid/graphics/Bitmap;
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_1

    .line 41
    invoke-static {p0}, Lnet/gogame/gowrap/io/utils/IOUtils;->closeQuietly(Ljava/io/InputStream;)V

    .line 42
    invoke-interface {p1}, Lnet/gogame/gowrap/ui/utils/ImageUtils$Source;->close()V

    .line 44
    invoke-virtual {p2}, Ljava/lang/Integer;->intValue()I

    move-result p0

    invoke-virtual {p3}, Ljava/lang/Integer;->intValue()I

    move-result p2

    invoke-static {v1, p0, p2}, Lnet/gogame/gowrap/ui/utils/ImageUtils;->calculateInSampleSize(Landroid/graphics/BitmapFactory$Options;II)I

    const/4 p0, 0x0

    .line 45
    iput-boolean p0, v1, Landroid/graphics/BitmapFactory$Options;->inJustDecodeBounds:Z

    .line 46
    invoke-interface {p1}, Lnet/gogame/gowrap/ui/utils/ImageUtils$Source;->getInputStream()Ljava/io/InputStream;

    move-result-object p0

    .line 48
    :try_start_1
    invoke-static {p0, v0, v1}, Landroid/graphics/BitmapFactory;->decodeStream(Ljava/io/InputStream;Landroid/graphics/Rect;Landroid/graphics/BitmapFactory$Options;)Landroid/graphics/Bitmap;

    move-result-object p2
    :try_end_1
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    .line 50
    invoke-static {p0}, Lnet/gogame/gowrap/io/utils/IOUtils;->closeQuietly(Ljava/io/InputStream;)V

    .line 51
    invoke-interface {p1}, Lnet/gogame/gowrap/ui/utils/ImageUtils$Source;->close()V

    return-object p2

    :catchall_0
    move-exception p2

    .line 50
    invoke-static {p0}, Lnet/gogame/gowrap/io/utils/IOUtils;->closeQuietly(Ljava/io/InputStream;)V

    .line 51
    invoke-interface {p1}, Lnet/gogame/gowrap/ui/utils/ImageUtils$Source;->close()V

    .line 52
    throw p2

    :catchall_1
    move-exception p2

    .line 41
    invoke-static {p0}, Lnet/gogame/gowrap/io/utils/IOUtils;->closeQuietly(Ljava/io/InputStream;)V

    .line 42
    invoke-interface {p1}, Lnet/gogame/gowrap/ui/utils/ImageUtils$Source;->close()V

    .line 43
    throw p2

    .line 54
    :cond_1
    invoke-interface {p1}, Lnet/gogame/gowrap/ui/utils/ImageUtils$Source;->getInputStream()Ljava/io/InputStream;

    move-result-object p0

    if-nez p0, :cond_2

    .line 61
    invoke-static {p0}, Lnet/gogame/gowrap/io/utils/IOUtils;->closeQuietly(Ljava/io/InputStream;)V

    .line 62
    invoke-interface {p1}, Lnet/gogame/gowrap/ui/utils/ImageUtils$Source;->close()V

    return-object v0

    .line 59
    :cond_2
    :try_start_2
    invoke-static {p0}, Landroid/graphics/BitmapFactory;->decodeStream(Ljava/io/InputStream;)Landroid/graphics/Bitmap;

    move-result-object p2
    :try_end_2
    .catchall {:try_start_2 .. :try_end_2} :catchall_2

    .line 61
    invoke-static {p0}, Lnet/gogame/gowrap/io/utils/IOUtils;->closeQuietly(Ljava/io/InputStream;)V

    .line 62
    invoke-interface {p1}, Lnet/gogame/gowrap/ui/utils/ImageUtils$Source;->close()V

    return-object p2

    :catchall_2
    move-exception p2

    .line 61
    invoke-static {p0}, Lnet/gogame/gowrap/io/utils/IOUtils;->closeQuietly(Ljava/io/InputStream;)V

    .line 62
    invoke-interface {p1}, Lnet/gogame/gowrap/ui/utils/ImageUtils$Source;->close()V

    .line 63
    throw p2
.end method

.method public static getSampledBitmapDrawable(Landroid/content/Context;Lnet/gogame/gowrap/ui/utils/ImageUtils$Source;Ljava/lang/Integer;Ljava/lang/Integer;)Landroid/graphics/drawable/BitmapDrawable;
    .locals 0
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    .line 70
    invoke-static {p0, p1, p2, p3}, Lnet/gogame/gowrap/ui/utils/ImageUtils;->getSampledBitmap(Landroid/content/Context;Lnet/gogame/gowrap/ui/utils/ImageUtils$Source;Ljava/lang/Integer;Ljava/lang/Integer;)Landroid/graphics/Bitmap;

    move-result-object p0

    if-eqz p0, :cond_0

    const/16 p1, 0x78

    .line 72
    invoke-virtual {p0, p1}, Landroid/graphics/Bitmap;->setDensity(I)V

    .line 73
    new-instance p1, Landroid/graphics/drawable/BitmapDrawable;

    invoke-direct {p1, p0}, Landroid/graphics/drawable/BitmapDrawable;-><init>(Landroid/graphics/Bitmap;)V

    return-object p1

    :cond_0
    const/4 p0, 0x0

    return-object p0
.end method
