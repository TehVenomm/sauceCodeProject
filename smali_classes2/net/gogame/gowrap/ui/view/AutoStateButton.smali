.class public Lnet/gogame/gowrap/ui/view/AutoStateButton;
.super Landroid/widget/Button;
.source "AutoStateButton.java"


# annotations
.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Lnet/gogame/gowrap/ui/view/AutoStateButton$BackgroundDrawable;
    }
.end annotation


# direct methods
.method public constructor <init>(Landroid/content/Context;)V
    .locals 0

    .line 15
    invoke-direct {p0, p1}, Landroid/widget/Button;-><init>(Landroid/content/Context;)V

    return-void
.end method

.method public constructor <init>(Landroid/content/Context;Landroid/util/AttributeSet;)V
    .locals 0

    .line 19
    invoke-direct {p0, p1, p2}, Landroid/widget/Button;-><init>(Landroid/content/Context;Landroid/util/AttributeSet;)V

    return-void
.end method

.method public constructor <init>(Landroid/content/Context;Landroid/util/AttributeSet;I)V
    .locals 0

    .line 23
    invoke-direct {p0, p1, p2, p3}, Landroid/widget/Button;-><init>(Landroid/content/Context;Landroid/util/AttributeSet;I)V

    return-void
.end method


# virtual methods
.method public setBackgroundDrawable(Landroid/graphics/drawable/Drawable;)V
    .locals 1

    .line 28
    new-instance v0, Lnet/gogame/gowrap/ui/view/AutoStateButton$BackgroundDrawable;

    invoke-direct {v0, p0, p1}, Lnet/gogame/gowrap/ui/view/AutoStateButton$BackgroundDrawable;-><init>(Lnet/gogame/gowrap/ui/view/AutoStateButton;Landroid/graphics/drawable/Drawable;)V

    .line 29
    invoke-super {p0, v0}, Landroid/widget/Button;->setBackgroundDrawable(Landroid/graphics/drawable/Drawable;)V

    return-void
.end method
