.class Lnet/gogame/gowrap/ui/view/AutoStateButton$BackgroundDrawable;
.super Landroid/graphics/drawable/LayerDrawable;
.source "AutoStateButton.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lnet/gogame/gowrap/ui/view/AutoStateButton;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x2
    name = "BackgroundDrawable"
.end annotation


# instance fields
.field private disabledAlpha:I

.field private fullAlpha:I

.field private pressedFilter:Landroid/graphics/ColorFilter;

.field final synthetic this$0:Lnet/gogame/gowrap/ui/view/AutoStateButton;


# direct methods
.method public constructor <init>(Lnet/gogame/gowrap/ui/view/AutoStateButton;Landroid/graphics/drawable/Drawable;)V
    .locals 2

    .line 38
    iput-object p1, p0, Lnet/gogame/gowrap/ui/view/AutoStateButton$BackgroundDrawable;->this$0:Lnet/gogame/gowrap/ui/view/AutoStateButton;

    const/4 p1, 0x1

    .line 39
    new-array v0, p1, [Landroid/graphics/drawable/Drawable;

    const/4 v1, 0x0

    aput-object p2, v0, v1

    invoke-direct {p0, v0}, Landroid/graphics/drawable/LayerDrawable;-><init>([Landroid/graphics/drawable/Drawable;)V

    .line 34
    new-instance p2, Landroid/graphics/LightingColorFilter;

    const v0, -0x333334

    invoke-direct {p2, v0, p1}, Landroid/graphics/LightingColorFilter;-><init>(II)V

    iput-object p2, p0, Lnet/gogame/gowrap/ui/view/AutoStateButton$BackgroundDrawable;->pressedFilter:Landroid/graphics/ColorFilter;

    const/16 p1, 0x64

    .line 35
    iput p1, p0, Lnet/gogame/gowrap/ui/view/AutoStateButton$BackgroundDrawable;->disabledAlpha:I

    const/16 p1, 0xff

    .line 36
    iput p1, p0, Lnet/gogame/gowrap/ui/view/AutoStateButton$BackgroundDrawable;->fullAlpha:I

    return-void
.end method


# virtual methods
.method public isStateful()Z
    .locals 1

    const/4 v0, 0x1

    return v0
.end method

.method protected onStateChange([I)Z
    .locals 7

    .line 47
    array-length v0, p1

    const/4 v1, 0x0

    const/4 v2, 0x0

    const/4 v3, 0x0

    :goto_0
    if-ge v1, v0, :cond_2

    aget v4, p1, v1

    const v5, 0x101009e

    const/4 v6, 0x1

    if-ne v4, v5, :cond_0

    const/4 v2, 0x1

    goto :goto_1

    :cond_0
    const v5, 0x10100a7

    if-ne v4, v5, :cond_1

    const/4 v3, 0x1

    :cond_1
    :goto_1
    add-int/lit8 v1, v1, 0x1

    goto :goto_0

    .line 54
    :cond_2
    invoke-virtual {p0}, Lnet/gogame/gowrap/ui/view/AutoStateButton$BackgroundDrawable;->mutate()Landroid/graphics/drawable/Drawable;

    if-eqz v2, :cond_3

    if-eqz v3, :cond_3

    .line 56
    iget-object v0, p0, Lnet/gogame/gowrap/ui/view/AutoStateButton$BackgroundDrawable;->pressedFilter:Landroid/graphics/ColorFilter;

    invoke-virtual {p0, v0}, Lnet/gogame/gowrap/ui/view/AutoStateButton$BackgroundDrawable;->setColorFilter(Landroid/graphics/ColorFilter;)V

    goto :goto_2

    :cond_3
    const/4 v0, 0x0

    if-nez v2, :cond_4

    .line 58
    invoke-virtual {p0, v0}, Lnet/gogame/gowrap/ui/view/AutoStateButton$BackgroundDrawable;->setColorFilter(Landroid/graphics/ColorFilter;)V

    .line 59
    iget v0, p0, Lnet/gogame/gowrap/ui/view/AutoStateButton$BackgroundDrawable;->disabledAlpha:I

    invoke-virtual {p0, v0}, Lnet/gogame/gowrap/ui/view/AutoStateButton$BackgroundDrawable;->setAlpha(I)V

    goto :goto_2

    .line 61
    :cond_4
    invoke-virtual {p0, v0}, Lnet/gogame/gowrap/ui/view/AutoStateButton$BackgroundDrawable;->setColorFilter(Landroid/graphics/ColorFilter;)V

    .line 62
    iget v0, p0, Lnet/gogame/gowrap/ui/view/AutoStateButton$BackgroundDrawable;->fullAlpha:I

    invoke-virtual {p0, v0}, Lnet/gogame/gowrap/ui/view/AutoStateButton$BackgroundDrawable;->setAlpha(I)V

    .line 65
    :goto_2
    invoke-virtual {p0}, Lnet/gogame/gowrap/ui/view/AutoStateButton$BackgroundDrawable;->invalidateSelf()V

    .line 67
    invoke-super {p0, p1}, Landroid/graphics/drawable/LayerDrawable;->onStateChange([I)Z

    move-result p1

    return p1
.end method
