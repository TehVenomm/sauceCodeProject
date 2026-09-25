.class public abstract Lnet/gogame/gowrap/ui/v2017_1/AbstractCustomImageButton;
.super Landroid/widget/FrameLayout;
.source "AbstractCustomImageButton.java"


# direct methods
.method public constructor <init>(Landroid/content/Context;)V
    .locals 0

    .line 18
    invoke-direct {p0, p1}, Landroid/widget/FrameLayout;-><init>(Landroid/content/Context;)V

    const/4 p1, 0x0

    .line 19
    invoke-virtual {p0, p1}, Lnet/gogame/gowrap/ui/v2017_1/AbstractCustomImageButton;->init(Landroid/util/AttributeSet;)V

    return-void
.end method

.method public constructor <init>(Landroid/content/Context;Landroid/util/AttributeSet;)V
    .locals 0

    .line 23
    invoke-direct {p0, p1, p2}, Landroid/widget/FrameLayout;-><init>(Landroid/content/Context;Landroid/util/AttributeSet;)V

    .line 24
    invoke-virtual {p0, p2}, Lnet/gogame/gowrap/ui/v2017_1/AbstractCustomImageButton;->init(Landroid/util/AttributeSet;)V

    return-void
.end method

.method public constructor <init>(Landroid/content/Context;Landroid/util/AttributeSet;I)V
    .locals 0

    .line 28
    invoke-direct {p0, p1, p2, p3}, Landroid/widget/FrameLayout;-><init>(Landroid/content/Context;Landroid/util/AttributeSet;I)V

    .line 29
    invoke-virtual {p0, p2}, Lnet/gogame/gowrap/ui/v2017_1/AbstractCustomImageButton;->init(Landroid/util/AttributeSet;)V

    return-void
.end method

.method public constructor <init>(Landroid/content/Context;Landroid/util/AttributeSet;II)V
    .locals 0
    .annotation build Landroid/annotation/TargetApi;
        value = 0x15
    .end annotation

    .line 35
    invoke-direct {p0, p1, p2, p3, p4}, Landroid/widget/FrameLayout;-><init>(Landroid/content/Context;Landroid/util/AttributeSet;II)V

    .line 36
    invoke-virtual {p0, p2}, Lnet/gogame/gowrap/ui/v2017_1/AbstractCustomImageButton;->init(Landroid/util/AttributeSet;)V

    return-void
.end method


# virtual methods
.method protected abstract init(Landroid/util/AttributeSet;)V
.end method

.method public setCaption(Ljava/lang/String;)V
    .locals 1

    .line 54
    sget v0, Lnet/gogame/gowrap/R$id;->net_gogame_gowrap_image_button_caption:I

    invoke-virtual {p0, v0}, Lnet/gogame/gowrap/ui/v2017_1/AbstractCustomImageButton;->findViewById(I)Landroid/view/View;

    move-result-object v0

    check-cast v0, Landroid/widget/TextView;

    if-eqz p1, :cond_0

    .line 57
    invoke-virtual {v0, p1}, Landroid/widget/TextView;->setText(Ljava/lang/CharSequence;)V

    const/4 p1, 0x0

    .line 58
    invoke-virtual {v0, p1}, Landroid/widget/TextView;->setVisibility(I)V

    goto :goto_0

    :cond_0
    const/16 p1, 0x8

    .line 60
    invoke-virtual {v0, p1}, Landroid/widget/TextView;->setVisibility(I)V

    :goto_0
    return-void
.end method

.method public setImage(Landroid/graphics/drawable/Drawable;)V
    .locals 1

    .line 48
    sget v0, Lnet/gogame/gowrap/R$id;->net_gogame_gowrap_image_button_image:I

    invoke-virtual {p0, v0}, Lnet/gogame/gowrap/ui/v2017_1/AbstractCustomImageButton;->findViewById(I)Landroid/view/View;

    move-result-object v0

    check-cast v0, Landroid/widget/ImageView;

    .line 50
    invoke-virtual {v0, p1}, Landroid/widget/ImageView;->setImageDrawable(Landroid/graphics/drawable/Drawable;)V

    return-void
.end method

.method public setLevel(I)V
    .locals 1

    .line 42
    sget v0, Lnet/gogame/gowrap/R$id;->net_gogame_gowrap_image_button_content_background:I

    invoke-virtual {p0, v0}, Lnet/gogame/gowrap/ui/v2017_1/AbstractCustomImageButton;->findViewById(I)Landroid/view/View;

    move-result-object v0

    check-cast v0, Landroid/widget/ImageView;

    .line 44
    invoke-virtual {v0, p1}, Landroid/widget/ImageView;->setImageLevel(I)V

    return-void
.end method

.method public setMasked(Z)V
    .locals 1

    .line 78
    sget v0, Lnet/gogame/gowrap/R$id;->net_gogame_gowrap_image_button_mask:I

    invoke-virtual {p0, v0}, Lnet/gogame/gowrap/ui/v2017_1/AbstractCustomImageButton;->findViewById(I)Landroid/view/View;

    move-result-object v0

    if-eqz v0, :cond_1

    if-eqz p1, :cond_0

    const/4 p1, 0x0

    .line 81
    invoke-virtual {v0, p1}, Landroid/view/View;->setVisibility(I)V

    goto :goto_0

    :cond_0
    const/4 p1, 0x4

    .line 83
    invoke-virtual {v0, p1}, Landroid/view/View;->setVisibility(I)V

    :cond_1
    :goto_0
    return-void
.end method

.method public setSubCaption(Ljava/lang/String;)V
    .locals 1

    .line 65
    sget v0, Lnet/gogame/gowrap/R$id;->net_gogame_gowrap_image_button_subcaption:I

    invoke-virtual {p0, v0}, Lnet/gogame/gowrap/ui/v2017_1/AbstractCustomImageButton;->findViewById(I)Landroid/view/View;

    move-result-object v0

    check-cast v0, Landroid/widget/TextView;

    if-eqz v0, :cond_1

    if-eqz p1, :cond_0

    .line 69
    invoke-virtual {v0, p1}, Landroid/widget/TextView;->setText(Ljava/lang/CharSequence;)V

    const/4 p1, 0x0

    .line 70
    invoke-virtual {v0, p1}, Landroid/widget/TextView;->setVisibility(I)V

    goto :goto_0

    :cond_0
    const/16 p1, 0x8

    .line 72
    invoke-virtual {v0, p1}, Landroid/widget/TextView;->setVisibility(I)V

    :cond_1
    :goto_0
    return-void
.end method
