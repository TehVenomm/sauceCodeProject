.class public final Lnet/gogame/gopay/sdk/iab/bs;
.super Lnet/gogame/gopay/sdk/a;


# instance fields
.field d:Ljava/lang/ref/WeakReference;

.field e:I

.field f:Lnet/gogame/gopay/sdk/iab/a;


# direct methods
.method public constructor <init>(Landroid/content/Context;Lnet/gogame/gopay/sdk/iab/i;)V
    .locals 0

    invoke-direct {p0, p1}, Lnet/gogame/gopay/sdk/a;-><init>(Landroid/content/Context;)V

    const/4 p1, 0x0

    iput p1, p0, Lnet/gogame/gopay/sdk/iab/bs;->e:I

    new-instance p1, Ljava/lang/ref/WeakReference;

    invoke-direct {p1, p2}, Ljava/lang/ref/WeakReference;-><init>(Ljava/lang/Object;)V

    iput-object p1, p0, Lnet/gogame/gopay/sdk/iab/bs;->d:Ljava/lang/ref/WeakReference;

    return-void
.end method

.method private c(I)Lnet/gogame/gopay/sdk/iab/a;
    .locals 1

    if-nez p1, :cond_0

    iget-object p1, p0, Lnet/gogame/gopay/sdk/iab/bs;->f:Lnet/gogame/gopay/sdk/iab/a;

    return-object p1

    :cond_0
    iget-object v0, p0, Lnet/gogame/gopay/sdk/iab/bs;->d:Ljava/lang/ref/WeakReference;

    invoke-virtual {v0}, Ljava/lang/ref/WeakReference;->get()Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Lnet/gogame/gopay/sdk/iab/i;

    invoke-virtual {p0, p1}, Lnet/gogame/gopay/sdk/iab/bs;->b(I)Ljava/lang/Integer;

    move-result-object p1

    invoke-virtual {p1}, Ljava/lang/Integer;->intValue()I

    move-result p1

    invoke-virtual {v0, p1}, Lnet/gogame/gopay/sdk/iab/i;->getItem(I)Ljava/lang/Object;

    move-result-object p1

    check-cast p1, Lnet/gogame/gopay/sdk/iab/a;

    return-object p1
.end method


# virtual methods
.method public final a(Lnet/gogame/gopay/sdk/iab/a;)V
    .locals 0

    iput-object p1, p0, Lnet/gogame/gopay/sdk/iab/bs;->f:Lnet/gogame/gopay/sdk/iab/a;

    invoke-virtual {p0}, Lnet/gogame/gopay/sdk/iab/bs;->notifyDataSetChanged()V

    return-void
.end method

.method final a(Lnet/gogame/gopay/sdk/iab/bv;Landroid/graphics/Bitmap;)V
    .locals 6

    const/4 v0, 0x4

    const/4 v1, 0x0

    if-nez p2, :cond_0

    iget-object p2, p1, Lnet/gogame/gopay/sdk/iab/bv;->a:Landroid/widget/ImageView;

    invoke-virtual {p2, v0}, Landroid/widget/ImageView;->setVisibility(I)V

    iget-object p1, p1, Lnet/gogame/gopay/sdk/iab/bv;->b:Landroid/widget/TextView;

    invoke-virtual {p1, v1}, Landroid/widget/TextView;->setVisibility(I)V

    return-void

    :cond_0
    invoke-virtual {p2}, Landroid/graphics/Bitmap;->getWidth()I

    move-result v2

    invoke-virtual {p2}, Landroid/graphics/Bitmap;->getHeight()I

    move-result v3

    new-instance v4, Landroid/graphics/drawable/BitmapDrawable;

    iget-object v5, p0, Lnet/gogame/gopay/sdk/a;->a:Landroid/content/Context;

    invoke-virtual {v5}, Landroid/content/Context;->getResources()Landroid/content/res/Resources;

    move-result-object v5

    invoke-direct {v4, v5, p2}, Landroid/graphics/drawable/BitmapDrawable;-><init>(Landroid/content/res/Resources;Landroid/graphics/Bitmap;)V

    invoke-virtual {v4, v1, v1, v2, v3}, Landroid/graphics/drawable/Drawable;->setBounds(IIII)V

    iget-object p2, p1, Lnet/gogame/gopay/sdk/iab/bv;->a:Landroid/widget/ImageView;

    invoke-virtual {p2, v4}, Landroid/widget/ImageView;->setImageDrawable(Landroid/graphics/drawable/Drawable;)V

    iget-object p2, p1, Lnet/gogame/gopay/sdk/iab/bv;->a:Landroid/widget/ImageView;

    invoke-virtual {p2, v1}, Landroid/widget/ImageView;->setVisibility(I)V

    iget-object p1, p1, Lnet/gogame/gopay/sdk/iab/bv;->b:Landroid/widget/TextView;

    invoke-virtual {p1, v0}, Landroid/widget/TextView;->setVisibility(I)V

    return-void
.end method

.method public final b(I)Ljava/lang/Integer;
    .locals 0

    if-nez p1, :cond_0

    const/4 p1, 0x0

    :goto_0
    invoke-super {p0, p1}, Lnet/gogame/gopay/sdk/a;->getItem(I)Ljava/lang/Object;

    move-result-object p1

    check-cast p1, Ljava/lang/Integer;

    return-object p1

    :cond_0
    add-int/lit8 p1, p1, -0x1

    goto :goto_0
.end method

.method public final getCount()I
    .locals 2

    invoke-super {p0}, Lnet/gogame/gopay/sdk/a;->getCount()I

    move-result v0

    iget-object v1, p0, Lnet/gogame/gopay/sdk/iab/bs;->f:Lnet/gogame/gopay/sdk/iab/a;

    if-eqz v1, :cond_0

    const/4 v1, 0x1

    goto :goto_0

    :cond_0
    const/4 v1, 0x0

    :goto_0
    add-int/2addr v0, v1

    return v0
.end method

.method public final synthetic getItem(I)Ljava/lang/Object;
    .locals 0

    invoke-virtual {p0, p1}, Lnet/gogame/gopay/sdk/iab/bs;->b(I)Ljava/lang/Integer;

    move-result-object p1

    return-object p1
.end method

.method public final getView(ILandroid/view/View;Landroid/view/ViewGroup;)Landroid/view/View;
    .locals 11

    const/4 p3, 0x2

    const/4 v0, 0x1

    const/4 v1, -0x1

    const/4 v2, 0x0

    if-nez p2, :cond_0

    new-instance p2, Landroid/widget/RelativeLayout;

    iget-object v3, p0, Lnet/gogame/gopay/sdk/a;->a:Landroid/content/Context;

    invoke-direct {p2, v3}, Landroid/widget/RelativeLayout;-><init>(Landroid/content/Context;)V

    new-instance v3, Landroid/widget/AbsListView$LayoutParams;

    const/16 v4, 0x50

    invoke-virtual {p0, v4}, Lnet/gogame/gopay/sdk/iab/bs;->a(I)I

    move-result v4

    invoke-direct {v3, v4, v1}, Landroid/widget/AbsListView$LayoutParams;-><init>(II)V

    invoke-virtual {p2, v3}, Landroid/widget/RelativeLayout;->setLayoutParams(Landroid/view/ViewGroup$LayoutParams;)V

    new-instance v3, Landroid/widget/ImageView;

    iget-object v4, p0, Lnet/gogame/gopay/sdk/a;->a:Landroid/content/Context;

    invoke-direct {v3, v4}, Landroid/widget/ImageView;-><init>(Landroid/content/Context;)V

    invoke-static {v0}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v4

    invoke-virtual {v3, v4}, Landroid/widget/ImageView;->setTag(Ljava/lang/Object;)V

    sget-object v4, Landroid/widget/ImageView$ScaleType;->FIT_CENTER:Landroid/widget/ImageView$ScaleType;

    invoke-virtual {v3, v4}, Landroid/widget/ImageView;->setScaleType(Landroid/widget/ImageView$ScaleType;)V

    new-instance v4, Landroid/widget/RelativeLayout$LayoutParams;

    const/16 v5, 0x4d

    invoke-virtual {p0, v5}, Lnet/gogame/gopay/sdk/iab/bs;->a(I)I

    move-result v5

    const/16 v6, 0x31

    invoke-virtual {p0, v6}, Lnet/gogame/gopay/sdk/iab/bs;->a(I)I

    move-result v6

    invoke-direct {v4, v5, v6}, Landroid/widget/RelativeLayout$LayoutParams;-><init>(II)V

    const/16 v5, 0xd

    invoke-virtual {v4, v5}, Landroid/widget/RelativeLayout$LayoutParams;->addRule(I)V

    invoke-virtual {p2, v3, v4}, Landroid/widget/RelativeLayout;->addView(Landroid/view/View;Landroid/view/ViewGroup$LayoutParams;)V

    new-instance v4, Landroid/widget/TextView;

    iget-object v6, p0, Lnet/gogame/gopay/sdk/a;->a:Landroid/content/Context;

    invoke-direct {v4, v6}, Landroid/widget/TextView;-><init>(Landroid/content/Context;)V

    invoke-virtual {v4, v2}, Landroid/widget/TextView;->setBackgroundColor(I)V

    const/high16 v6, -0x1000000

    invoke-virtual {v4, v6}, Landroid/widget/TextView;->setTextColor(I)V

    const/16 v6, 0x11

    invoke-virtual {v4, v6}, Landroid/widget/TextView;->setGravity(I)V

    const/4 v6, 0x3

    const/high16 v7, 0x40c00000    # 6.0f

    invoke-virtual {v4, v6, v7}, Landroid/widget/TextView;->setTextSize(IF)V

    invoke-static {p3}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v6

    invoke-virtual {v4, v6}, Landroid/widget/TextView;->setTag(Ljava/lang/Object;)V

    new-instance v6, Landroid/widget/RelativeLayout$LayoutParams;

    invoke-direct {v6, v1, v1}, Landroid/widget/RelativeLayout$LayoutParams;-><init>(II)V

    invoke-virtual {v6, v5}, Landroid/widget/RelativeLayout$LayoutParams;->addRule(I)V

    invoke-virtual {p0, v0}, Lnet/gogame/gopay/sdk/iab/bs;->a(I)I

    move-result v5

    invoke-virtual {p0, v0}, Lnet/gogame/gopay/sdk/iab/bs;->a(I)I

    move-result v7

    invoke-virtual {v6, v5, v2, v7, v2}, Landroid/widget/RelativeLayout$LayoutParams;->setMargins(IIII)V

    invoke-virtual {p2, v4, v6}, Landroid/widget/RelativeLayout;->addView(Landroid/view/View;Landroid/view/ViewGroup$LayoutParams;)V

    new-instance v5, Lnet/gogame/gopay/sdk/iab/bv;

    invoke-direct {v5, v3, v4, p1}, Lnet/gogame/gopay/sdk/iab/bv;-><init>(Landroid/widget/ImageView;Landroid/widget/TextView;I)V

    invoke-virtual {p2, v5}, Landroid/view/View;->setTag(Ljava/lang/Object;)V

    :cond_0
    iget v3, p0, Lnet/gogame/gopay/sdk/iab/bs;->e:I

    if-ne p1, v3, :cond_2

    invoke-virtual {p0, p3}, Lnet/gogame/gopay/sdk/iab/bs;->a(I)I

    move-result v3

    new-instance v4, Landroid/graphics/drawable/GradientDrawable;

    invoke-direct {v4}, Landroid/graphics/drawable/GradientDrawable;-><init>()V

    const/16 v5, 0x5c

    const/16 v6, 0xb0

    const/16 v7, 0x3b

    invoke-static {v5, v6, v7}, Landroid/graphics/Color;->rgb(III)I

    move-result v5

    invoke-virtual {v4, v3, v5}, Landroid/graphics/drawable/GradientDrawable;->setStroke(II)V

    invoke-virtual {v4, v1}, Landroid/graphics/drawable/GradientDrawable;->setColor(I)V

    invoke-virtual {v4, p3}, Landroid/graphics/drawable/GradientDrawable;->setGradientType(I)V

    new-array p3, v0, [Landroid/graphics/drawable/Drawable;

    aput-object v4, p3, v2

    new-instance v0, Landroid/graphics/drawable/LayerDrawable;

    invoke-direct {v0, p3}, Landroid/graphics/drawable/LayerDrawable;-><init>([Landroid/graphics/drawable/Drawable;)V

    const/4 v6, 0x0

    neg-int v9, v3

    const/4 v10, 0x0

    move-object v5, v0

    move v7, v9

    move v8, v9

    invoke-virtual/range {v5 .. v10}, Landroid/graphics/drawable/LayerDrawable;->setLayerInset(IIIII)V

    const/high16 p3, 0x3f800000    # 1.0f

    invoke-virtual {p2, p3}, Landroid/view/View;->setAlpha(F)V

    sget p3, Landroid/os/Build$VERSION;->SDK_INT:I

    const/16 v1, 0x10

    if-lt p3, v1, :cond_1

    invoke-virtual {p2, v0}, Landroid/view/View;->setBackground(Landroid/graphics/drawable/Drawable;)V

    goto :goto_0

    :cond_1
    invoke-virtual {p2, v0}, Landroid/view/View;->setBackgroundDrawable(Landroid/graphics/drawable/Drawable;)V

    goto :goto_0

    :cond_2
    const/high16 p3, 0x3f000000    # 0.5f

    invoke-virtual {p2, p3}, Landroid/view/View;->setAlpha(F)V

    invoke-virtual {p2, v2}, Landroid/view/View;->setBackgroundColor(I)V

    :goto_0
    invoke-virtual {p2}, Landroid/view/View;->getTag()Ljava/lang/Object;

    move-result-object p3

    check-cast p3, Lnet/gogame/gopay/sdk/iab/bv;

    iput p1, p3, Lnet/gogame/gopay/sdk/iab/bv;->c:I

    iget-object v0, p3, Lnet/gogame/gopay/sdk/iab/bv;->b:Landroid/widget/TextView;

    iget v1, p3, Lnet/gogame/gopay/sdk/iab/bv;->c:I

    invoke-direct {p0, v1}, Lnet/gogame/gopay/sdk/iab/bs;->c(I)Lnet/gogame/gopay/sdk/iab/a;

    move-result-object v1

    invoke-virtual {v1}, Lnet/gogame/gopay/sdk/iab/a;->getDisplayName()Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Landroid/widget/TextView;->setText(Ljava/lang/CharSequence;)V

    iget-object v0, p3, Lnet/gogame/gopay/sdk/iab/bv;->a:Landroid/widget/ImageView;

    const/4 v1, 0x4

    invoke-virtual {v0, v1}, Landroid/widget/ImageView;->setVisibility(I)V

    invoke-virtual {p0}, Lnet/gogame/gopay/sdk/iab/bs;->a()Ljava/lang/String;

    move-result-object v0

    if-nez v0, :cond_3

    iget-object v0, p3, Lnet/gogame/gopay/sdk/iab/bv;->b:Landroid/widget/TextView;

    invoke-virtual {v0, v2}, Landroid/widget/TextView;->setVisibility(I)V

    :cond_3
    iget v0, p3, Lnet/gogame/gopay/sdk/iab/bv;->c:I

    invoke-direct {p0, v0}, Lnet/gogame/gopay/sdk/iab/bs;->c(I)Lnet/gogame/gopay/sdk/iab/a;

    move-result-object v0

    instance-of v1, v0, Lnet/gogame/gopay/sdk/iab/f;

    if-eqz v1, :cond_4

    invoke-static {}, Lnet/gogame/gopay/sdk/support/m;->i()Landroid/graphics/Bitmap;

    move-result-object p1

    invoke-virtual {p0, p3, p1}, Lnet/gogame/gopay/sdk/iab/bs;->a(Lnet/gogame/gopay/sdk/iab/bv;Landroid/graphics/Bitmap;)V

    goto :goto_1

    :cond_4
    invoke-virtual {p0}, Lnet/gogame/gopay/sdk/iab/bs;->a()Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0}, Lnet/gogame/gopay/sdk/iab/a;->getDisplayIcon()Ljava/lang/String;

    move-result-object v0

    new-instance v2, Lnet/gogame/gopay/sdk/iab/bt;

    invoke-direct {v2, p0, p3, p1}, Lnet/gogame/gopay/sdk/iab/bt;-><init>(Lnet/gogame/gopay/sdk/iab/bs;Lnet/gogame/gopay/sdk/iab/bv;I)V

    invoke-static {v1, v0, v2}, Lnet/gogame/gopay/sdk/support/m;->b(Ljava/lang/String;Ljava/lang/String;Lnet/gogame/gopay/sdk/support/q;)V

    :goto_1
    return-object p2
.end method
