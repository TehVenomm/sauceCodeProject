.class public Lcom/helpshift/util/Styles;
.super Ljava/lang/Object;
.source "Styles.java"


# direct methods
.method public constructor <init>()V
    .locals 0

    .line 15
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method

.method public static dpToPx(Landroid/content/Context;F)F
    .locals 0

    .line 48
    invoke-virtual {p0}, Landroid/content/Context;->getResources()Landroid/content/res/Resources;

    move-result-object p0

    invoke-virtual {p0}, Landroid/content/res/Resources;->getDisplayMetrics()Landroid/util/DisplayMetrics;

    move-result-object p0

    iget p0, p0, Landroid/util/DisplayMetrics;->density:F

    mul-float p1, p1, p0

    return p1
.end method

.method public static getColor(Landroid/content/Context;I)I
    .locals 2

    .line 17
    invoke-virtual {p0}, Landroid/content/Context;->getTheme()Landroid/content/res/Resources$Theme;

    move-result-object p0

    const/4 v0, 0x1

    new-array v0, v0, [I

    const/4 v1, 0x0

    aput p1, v0, v1

    invoke-virtual {p0, v0}, Landroid/content/res/Resources$Theme;->obtainStyledAttributes([I)Landroid/content/res/TypedArray;

    move-result-object p0

    const/4 p1, -0x1

    .line 18
    invoke-virtual {p0, v1, p1}, Landroid/content/res/TypedArray;->getColor(II)I

    move-result p1

    .line 19
    invoke-virtual {p0}, Landroid/content/res/TypedArray;->recycle()V

    return p1
.end method

.method public static getHexColor(I)Ljava/lang/String;
    .locals 3

    const-string v0, "#%06X"

    const/4 v1, 0x1

    .line 28
    new-array v1, v1, [Ljava/lang/Object;

    const v2, 0xffffff

    and-int/2addr p0, v2

    invoke-static {p0}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object p0

    const/4 v2, 0x0

    aput-object p0, v1, v2

    invoke-static {v0, v1}, Ljava/lang/String;->format(Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object p0

    return-object p0
.end method

.method public static getHexColor(Landroid/content/Context;I)Ljava/lang/String;
    .locals 0

    .line 24
    invoke-static {p0, p1}, Lcom/helpshift/util/Styles;->getColor(Landroid/content/Context;I)I

    move-result p0

    invoke-static {p0}, Lcom/helpshift/util/Styles;->getHexColor(I)Ljava/lang/String;

    move-result-object p0

    return-object p0
.end method

.method public static setActionButtonIconColor(Landroid/content/Context;Landroid/graphics/drawable/Drawable;)V
    .locals 1

    .line 32
    sget v0, Lcom/helpshift/R$attr;->hs__actionButtonIconColor:I

    invoke-static {p0, p1, v0}, Lcom/helpshift/util/Styles;->setColorFilter(Landroid/content/Context;Landroid/graphics/drawable/Drawable;I)V

    return-void
.end method

.method public static setColorFilter(Landroid/content/Context;Landroid/graphics/drawable/Drawable;I)V
    .locals 0

    if-eqz p1, :cond_0

    .line 37
    invoke-static {p0, p2}, Lcom/helpshift/util/Styles;->getColor(Landroid/content/Context;I)I

    move-result p0

    sget-object p2, Landroid/graphics/PorterDuff$Mode;->SRC_ATOP:Landroid/graphics/PorterDuff$Mode;

    invoke-virtual {p1, p0, p2}, Landroid/graphics/drawable/Drawable;->setColorFilter(ILandroid/graphics/PorterDuff$Mode;)V

    :cond_0
    return-void
.end method

.method public static setColorFilter(Landroid/graphics/drawable/Drawable;I)V
    .locals 1

    if-eqz p0, :cond_0

    .line 43
    sget-object v0, Landroid/graphics/PorterDuff$Mode;->SRC_ATOP:Landroid/graphics/PorterDuff$Mode;

    invoke-virtual {p0, p1, v0}, Landroid/graphics/drawable/Drawable;->setColorFilter(ILandroid/graphics/PorterDuff$Mode;)V

    :cond_0
    return-void
.end method

.method public static setDrawable(Landroid/content/Context;Landroid/view/View;II)V
    .locals 0

    .line 52
    invoke-static {p0, p2}, Landroidx/core/content/ContextCompat;->getDrawable(Landroid/content/Context;I)Landroid/graphics/drawable/Drawable;

    move-result-object p2

    .line 53
    invoke-static {p0, p2, p3}, Lcom/helpshift/util/Styles;->setColorFilter(Landroid/content/Context;Landroid/graphics/drawable/Drawable;I)V

    .line 54
    sget p0, Landroid/os/Build$VERSION;->SDK_INT:I

    const/16 p3, 0x10

    if-lt p0, p3, :cond_0

    .line 55
    invoke-virtual {p1, p2}, Landroid/view/View;->setBackground(Landroid/graphics/drawable/Drawable;)V

    goto :goto_0

    .line 58
    :cond_0
    invoke-virtual {p1, p2}, Landroid/view/View;->setBackgroundDrawable(Landroid/graphics/drawable/Drawable;)V

    :goto_0
    return-void
.end method

.method public static setGradientBackground(Landroid/view/View;IILandroid/graphics/drawable/GradientDrawable$Orientation;)V
    .locals 2
    .param p0    # Landroid/view/View;
        .annotation build Landroidx/annotation/NonNull;
        .end annotation
    .end param

    const/4 v0, 0x2

    .line 63
    new-array v0, v0, [I

    const/4 v1, 0x0

    aput p1, v0, v1

    const/4 p1, 0x1

    aput p2, v0, p1

    .line 64
    new-instance p1, Landroid/graphics/drawable/GradientDrawable;

    invoke-direct {p1, p3, v0}, Landroid/graphics/drawable/GradientDrawable;-><init>(Landroid/graphics/drawable/GradientDrawable$Orientation;[I)V

    .line 65
    invoke-static {p0, p1}, Landroidx/core/view/ViewCompat;->setBackground(Landroid/view/View;Landroid/graphics/drawable/Drawable;)V

    return-void
.end method
