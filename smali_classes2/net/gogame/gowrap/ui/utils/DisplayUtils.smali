.class public final Lnet/gogame/gowrap/ui/utils/DisplayUtils;
.super Ljava/lang/Object;
.source "DisplayUtils.java"


# direct methods
.method private constructor <init>()V
    .locals 0

    .line 24
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method

.method public static dpFromPx(Landroid/content/Context;F)F
    .locals 0

    .line 28
    invoke-virtual {p0}, Landroid/content/Context;->getResources()Landroid/content/res/Resources;

    move-result-object p0

    invoke-virtual {p0}, Landroid/content/res/Resources;->getDisplayMetrics()Landroid/util/DisplayMetrics;

    move-result-object p0

    iget p0, p0, Landroid/util/DisplayMetrics;->density:F

    div-float/2addr p1, p0

    return p1
.end method

.method public static getBaseScreenOrientation(I)I
    .locals 0

    packed-switch p0, :pswitch_data_0

    :pswitch_0
    const/4 p0, -0x1

    return p0

    :pswitch_1
    const/4 p0, 0x1

    return p0

    :pswitch_2
    const/4 p0, 0x0

    return p0

    nop

    :pswitch_data_0
    .packed-switch 0x0
        :pswitch_2
        :pswitch_1
        :pswitch_0
        :pswitch_0
        :pswitch_0
        :pswitch_0
        :pswitch_2
        :pswitch_1
        :pswitch_2
        :pswitch_1
        :pswitch_0
        :pswitch_2
        :pswitch_1
    .end packed-switch
.end method

.method public static getBaseScreenOrientation(Landroid/app/Activity;)I
    .locals 0

    .line 41
    invoke-static {p0}, Lnet/gogame/gowrap/ui/utils/DisplayUtils;->getScreenOrientation(Landroid/app/Activity;)I

    move-result p0

    invoke-static {p0}, Lnet/gogame/gowrap/ui/utils/DisplayUtils;->getBaseScreenOrientation(I)I

    move-result p0

    return p0
.end method

.method public static getScreenOrientation(Landroid/app/Activity;)I
    .locals 6

    .line 62
    sget v0, Landroid/os/Build$VERSION;->SDK_INT:I

    const/4 v1, 0x0

    const/4 v2, 0x1

    const/16 v3, 0xd

    if-lt v0, v3, :cond_9

    .line 63
    invoke-virtual {p0}, Landroid/app/Activity;->getWindowManager()Landroid/view/WindowManager;

    move-result-object p0

    invoke-interface {p0}, Landroid/view/WindowManager;->getDefaultDisplay()Landroid/view/Display;

    move-result-object p0

    .line 64
    invoke-virtual {p0}, Landroid/view/Display;->getRotation()I

    move-result v0

    .line 65
    new-instance v3, Landroid/graphics/Point;

    invoke-direct {v3}, Landroid/graphics/Point;-><init>()V

    .line 66
    invoke-virtual {p0, v3}, Landroid/view/Display;->getSize(Landroid/graphics/Point;)V

    .line 67
    iget p0, v3, Landroid/graphics/Point;->x:I

    iget v3, v3, Landroid/graphics/Point;->y:I

    if-le p0, v3, :cond_0

    const/4 p0, 0x1

    goto :goto_0

    :cond_0
    const/4 p0, 0x0

    :goto_0
    if-eq v0, v2, :cond_2

    const/4 v3, 0x3

    if-ne v0, v3, :cond_1

    goto :goto_1

    :cond_1
    const/4 v3, 0x0

    goto :goto_2

    :cond_2
    :goto_1
    const/4 v3, 0x1

    :goto_2
    const/16 v4, 0x9

    const/16 v5, 0x8

    if-eqz v3, :cond_7

    if-eqz p0, :cond_4

    if-ne v0, v2, :cond_3

    goto :goto_4

    :cond_3
    const/16 v1, 0x8

    goto :goto_4

    :cond_4
    if-ne v0, v2, :cond_6

    :cond_5
    const/16 v1, 0x9

    goto :goto_4

    :cond_6
    :goto_3
    const/4 v1, 0x1

    goto :goto_4

    :cond_7
    if-eqz p0, :cond_8

    if-nez v0, :cond_3

    goto :goto_4

    :cond_8
    if-nez v0, :cond_5

    goto :goto_3

    :goto_4
    return v1

    .line 94
    :cond_9
    invoke-virtual {p0}, Landroid/app/Activity;->getResources()Landroid/content/res/Resources;

    move-result-object p0

    invoke-virtual {p0}, Landroid/content/res/Resources;->getConfiguration()Landroid/content/res/Configuration;

    move-result-object p0

    iget p0, p0, Landroid/content/res/Configuration;->orientation:I

    const/4 v0, 0x2

    if-ne p0, v0, :cond_a

    return v1

    :cond_a
    return v2
.end method

.method public static hideSoftKeyboard(Landroid/app/Activity;)V
    .locals 2

    .line 113
    invoke-virtual {p0}, Landroid/app/Activity;->getCurrentFocus()Landroid/view/View;

    move-result-object v0

    if-eqz v0, :cond_0

    const-string v1, "input_method"

    .line 116
    invoke-virtual {p0, v1}, Landroid/app/Activity;->getSystemService(Ljava/lang/String;)Ljava/lang/Object;

    move-result-object p0

    check-cast p0, Landroid/view/inputmethod/InputMethodManager;

    if-eqz p0, :cond_0

    .line 118
    invoke-virtual {v0}, Landroid/view/View;->getWindowToken()Landroid/os/IBinder;

    move-result-object v0

    const/4 v1, 0x0

    invoke-virtual {p0, v0, v1}, Landroid/view/inputmethod/InputMethodManager;->hideSoftInputFromWindow(Landroid/os/IBinder;I)Z

    :cond_0
    return-void
.end method

.method public static lockOrientation(Landroid/app/Activity;)V
    .locals 2

    .line 104
    invoke-static {p0}, Lnet/gogame/gowrap/ui/utils/DisplayUtils;->getScreenOrientation(Landroid/app/Activity;)I

    move-result v0

    .line 106
    :try_start_0
    invoke-virtual {p0, v0}, Landroid/app/Activity;->setRequestedOrientation(I)V
    :try_end_0
    .catch Ljava/lang/Throwable; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception p0

    const-string v0, "goWrap"

    const-string v1, "Exception"

    .line 108
    invoke-static {v0, v1, p0}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)I

    :goto_0
    return-void
.end method

.method public static pxFromDp(Landroid/content/Context;F)I
    .locals 0

    .line 32
    invoke-virtual {p0}, Landroid/content/Context;->getResources()Landroid/content/res/Resources;

    move-result-object p0

    invoke-virtual {p0}, Landroid/content/res/Resources;->getDisplayMetrics()Landroid/util/DisplayMetrics;

    move-result-object p0

    iget p0, p0, Landroid/util/DisplayMetrics;->density:F

    mul-float p1, p1, p0

    invoke-static {p1}, Ljava/lang/Math;->round(F)I

    move-result p0

    return p0
.end method

.method public static pxFromSp(Landroid/content/Context;F)I
    .locals 1

    .line 37
    invoke-virtual {p0}, Landroid/content/Context;->getResources()Landroid/content/res/Resources;

    move-result-object p0

    invoke-virtual {p0}, Landroid/content/res/Resources;->getDisplayMetrics()Landroid/util/DisplayMetrics;

    move-result-object p0

    const/4 v0, 0x2

    .line 36
    invoke-static {v0, p1, p0}, Landroid/util/TypedValue;->applyDimension(IFLandroid/util/DisplayMetrics;)F

    move-result p0

    invoke-static {p0}, Ljava/lang/Math;->round(F)I

    move-result p0

    return p0
.end method

.method public static setLevel(Landroid/graphics/drawable/Drawable;I)V
    .locals 1

    .line 132
    instance-of v0, p0, Landroid/graphics/drawable/LevelListDrawable;

    if-eqz v0, :cond_0

    .line 133
    check-cast p0, Landroid/graphics/drawable/LevelListDrawable;

    .line 134
    invoke-virtual {p0, p1}, Landroid/graphics/drawable/LevelListDrawable;->setLevel(I)Z

    :cond_0
    return-void
.end method

.method public static showSoftKeyboard(Landroid/app/Activity;Landroid/widget/EditText;)V
    .locals 1

    const-string v0, "input_method"

    .line 125
    invoke-virtual {p0, v0}, Landroid/app/Activity;->getSystemService(Ljava/lang/String;)Ljava/lang/Object;

    move-result-object p0

    check-cast p0, Landroid/view/inputmethod/InputMethodManager;

    if-eqz p0, :cond_0

    const/4 v0, 0x1

    .line 127
    invoke-virtual {p0, p1, v0}, Landroid/view/inputmethod/InputMethodManager;->showSoftInput(Landroid/view/View;I)Z

    :cond_0
    return-void
.end method
