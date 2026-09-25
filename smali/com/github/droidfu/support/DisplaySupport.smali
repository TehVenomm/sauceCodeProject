.class public Lcom/github/droidfu/support/DisplaySupport;
.super Ljava/lang/Object;
.source "DisplaySupport.java"


# static fields
.field public static final SCREEN_DENSITY_HIGH:I = 0xf0

.field public static final SCREEN_DENSITY_LOW:I = 0x78

.field public static final SCREEN_DENSITY_MEDIUM:I = 0xa0

.field private static screenDensity:I = -0x1


# direct methods
.method static constructor <clinit>()V
    .locals 0

    return-void
.end method

.method public constructor <init>()V
    .locals 0

    .line 25
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method

.method public static dipToPx(Landroid/content/Context;I)I
    .locals 0

    .line 34
    invoke-virtual {p0}, Landroid/content/Context;->getResources()Landroid/content/res/Resources;

    move-result-object p0

    invoke-virtual {p0}, Landroid/content/res/Resources;->getDisplayMetrics()Landroid/util/DisplayMetrics;

    move-result-object p0

    int-to-float p1, p1

    .line 35
    iget p0, p0, Landroid/util/DisplayMetrics;->density:F

    mul-float p1, p1, p0

    const/high16 p0, 0x3f000000    # 0.5f

    add-float/2addr p1, p0

    float-to-int p0, p1

    return p0
.end method

.method public static getScreenDensity(Landroid/content/Context;)I
    .locals 2

    .line 46
    sget v0, Lcom/github/droidfu/support/DisplaySupport;->screenDensity:I

    const/4 v1, -0x1

    if-ne v0, v1, :cond_0

    .line 47
    invoke-virtual {p0}, Landroid/content/Context;->getResources()Landroid/content/res/Resources;

    move-result-object p0

    invoke-virtual {p0}, Landroid/content/res/Resources;->getDisplayMetrics()Landroid/util/DisplayMetrics;

    move-result-object p0

    .line 49
    :try_start_0
    const-class v0, Landroid/util/DisplayMetrics;

    const-string v1, "densityDpi"

    invoke-virtual {v0, v1}, Ljava/lang/Class;->getField(Ljava/lang/String;)Ljava/lang/reflect/Field;

    move-result-object v0

    invoke-virtual {v0, p0}, Ljava/lang/reflect/Field;->getInt(Ljava/lang/Object;)I

    move-result p0

    sput p0, Lcom/github/droidfu/support/DisplaySupport;->screenDensity:I
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    const/16 p0, 0xa0

    .line 51
    sput p0, Lcom/github/droidfu/support/DisplaySupport;->screenDensity:I

    .line 54
    :cond_0
    :goto_0
    sget p0, Lcom/github/droidfu/support/DisplaySupport;->screenDensity:I

    return p0
.end method

.method public static scaleDrawable(Landroid/content/Context;III)Landroid/graphics/drawable/Drawable;
    .locals 1

    .line 40
    invoke-virtual {p0}, Landroid/content/Context;->getResources()Landroid/content/res/Resources;

    move-result-object p0

    invoke-static {p0, p1}, Landroid/graphics/BitmapFactory;->decodeResource(Landroid/content/res/Resources;I)Landroid/graphics/Bitmap;

    move-result-object p0

    .line 42
    new-instance p1, Landroid/graphics/drawable/BitmapDrawable;

    const/4 v0, 0x1

    invoke-static {p0, p2, p3, v0}, Landroid/graphics/Bitmap;->createScaledBitmap(Landroid/graphics/Bitmap;IIZ)Landroid/graphics/Bitmap;

    move-result-object p0

    invoke-direct {p1, p0}, Landroid/graphics/drawable/BitmapDrawable;-><init>(Landroid/graphics/Bitmap;)V

    return-object p1
.end method
