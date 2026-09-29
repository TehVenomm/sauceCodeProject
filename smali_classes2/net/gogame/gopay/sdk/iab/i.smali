.class public final Lnet/gogame/gopay/sdk/iab/i;
.super Lnet/gogame/gopay/sdk/a;


# instance fields
.field d:I

.field e:I

.field f:Landroid/view/View;


# direct methods
.method public constructor <init>(Landroid/content/Context;)V
    .locals 0

    invoke-direct {p0, p1}, Lnet/gogame/gopay/sdk/a;-><init>(Landroid/content/Context;)V

    const/4 p1, 0x1

    iput p1, p0, Lnet/gogame/gopay/sdk/iab/i;->d:I

    const/4 p1, -0x1

    iput p1, p0, Lnet/gogame/gopay/sdk/iab/i;->e:I

    return-void
.end method

.method private a(ILandroid/view/View;)Landroid/view/View;
    .locals 9

    const/4 v0, 0x3

    const/4 v1, 0x0

    if-nez p2, :cond_2

    new-instance p2, Landroid/widget/LinearLayout;

    iget-object v2, p0, Lnet/gogame/gopay/sdk/a;->a:Landroid/content/Context;

    invoke-direct {p2, v2}, Landroid/widget/LinearLayout;-><init>(Landroid/content/Context;)V

    invoke-virtual {p2, v1}, Landroid/widget/LinearLayout;->setBackgroundColor(I)V

    invoke-virtual {p2, v1}, Landroid/widget/LinearLayout;->setOrientation(I)V

    new-instance v2, Landroid/widget/AbsListView$LayoutParams;

    const/16 v3, 0x3c

    invoke-virtual {p0, v3}, Lnet/gogame/gopay/sdk/iab/i;->a(I)I

    move-result v3

    const/4 v4, -0x1

    invoke-direct {v2, v4, v3}, Landroid/widget/AbsListView$LayoutParams;-><init>(II)V

    invoke-virtual {p2, v2}, Landroid/widget/LinearLayout;->setLayoutParams(Landroid/view/ViewGroup$LayoutParams;)V

    const/4 v2, 0x5

    invoke-virtual {p0, v2}, Lnet/gogame/gopay/sdk/iab/i;->a(I)I

    move-result v3

    invoke-virtual {p0, v2}, Lnet/gogame/gopay/sdk/iab/i;->a(I)I

    move-result v5

    invoke-virtual {p0, v2}, Lnet/gogame/gopay/sdk/iab/i;->a(I)I

    move-result v6

    invoke-virtual {p0, v2}, Lnet/gogame/gopay/sdk/iab/i;->a(I)I

    move-result v7

    invoke-virtual {p2, v3, v5, v6, v7}, Landroid/widget/LinearLayout;->setPadding(IIII)V

    new-instance v3, Landroid/widget/ImageView;

    iget-object v5, p0, Lnet/gogame/gopay/sdk/a;->a:Landroid/content/Context;

    invoke-direct {v3, v5}, Landroid/widget/ImageView;-><init>(Landroid/content/Context;)V

    const/4 v5, 0x1

    invoke-static {v5}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v6

    invoke-virtual {v3, v6}, Landroid/widget/ImageView;->setTag(Ljava/lang/Object;)V

    sget-object v6, Landroid/widget/ImageView$ScaleType;->FIT_CENTER:Landroid/widget/ImageView$ScaleType;

    invoke-virtual {v3, v6}, Landroid/widget/ImageView;->setScaleType(Landroid/widget/ImageView$ScaleType;)V

    new-instance v6, Landroid/widget/LinearLayout$LayoutParams;

    const/16 v7, 0x64

    invoke-virtual {p0, v7}, Lnet/gogame/gopay/sdk/iab/i;->a(I)I

    move-result v7

    invoke-direct {v6, v7, v4}, Landroid/widget/LinearLayout$LayoutParams;-><init>(II)V

    const/16 v7, 0xa

    invoke-virtual {p0, v7}, Lnet/gogame/gopay/sdk/iab/i;->a(I)I

    move-result v7

    invoke-virtual {v6, v7, v1, v1, v1}, Landroid/widget/LinearLayout$LayoutParams;->setMargins(IIII)V

    invoke-virtual {p2, v3, v6}, Landroid/widget/LinearLayout;->addView(Landroid/view/View;Landroid/view/ViewGroup$LayoutParams;)V

    new-instance v6, Landroid/widget/TextView;

    iget-object v7, p0, Lnet/gogame/gopay/sdk/a;->a:Landroid/content/Context;

    invoke-direct {v6, v7}, Landroid/widget/TextView;-><init>(Landroid/content/Context;)V

    invoke-virtual {v6, v1}, Landroid/widget/TextView;->setBackgroundColor(I)V

    const/high16 v7, -0x1000000

    invoke-virtual {v6, v7}, Landroid/widget/TextView;->setTextColor(I)V

    sget-object v7, Landroid/graphics/Typeface;->DEFAULT:Landroid/graphics/Typeface;

    invoke-virtual {v6, v7}, Landroid/widget/TextView;->setTypeface(Landroid/graphics/Typeface;)V

    const/16 v7, 0x11

    invoke-virtual {v6, v7}, Landroid/widget/TextView;->setGravity(I)V

    const/high16 v8, 0x41000000    # 8.0f

    invoke-virtual {v6, v0, v8}, Landroid/widget/TextView;->setTextSize(IF)V

    const/4 v8, 0x2

    invoke-virtual {v6, v8}, Landroid/widget/TextView;->setMaxLines(I)V

    invoke-static {v8}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v8

    invoke-virtual {v6, v8}, Landroid/widget/TextView;->setTag(Ljava/lang/Object;)V

    sget v8, Landroid/os/Build$VERSION;->SDK_INT:I

    if-lt v8, v7, :cond_0

    invoke-virtual {v6, v2}, Landroid/widget/TextView;->setTextAlignment(I)V

    goto :goto_0

    :cond_0
    invoke-virtual {v6, v0}, Landroid/widget/TextView;->setGravity(I)V

    :goto_0
    new-instance v2, Landroid/widget/LinearLayout$LayoutParams;

    invoke-direct {v2, v4, v4}, Landroid/widget/LinearLayout$LayoutParams;-><init>(II)V

    const/16 v4, 0xc

    invoke-virtual {p0, v4}, Lnet/gogame/gopay/sdk/iab/i;->a(I)I

    move-result v4

    invoke-virtual {p0, v5}, Lnet/gogame/gopay/sdk/iab/i;->a(I)I

    move-result v5

    invoke-virtual {v2, v4, v1, v5, v1}, Landroid/widget/LinearLayout$LayoutParams;->setMargins(IIII)V

    const/high16 v4, 0x3f800000    # 1.0f

    iput v4, v2, Landroid/widget/LinearLayout$LayoutParams;->weight:F

    invoke-virtual {p2, v6, v2}, Landroid/widget/LinearLayout;->addView(Landroid/view/View;Landroid/view/ViewGroup$LayoutParams;)V

    new-instance v2, Landroid/widget/ImageView;

    iget-object v4, p0, Lnet/gogame/gopay/sdk/a;->a:Landroid/content/Context;

    invoke-direct {v2, v4}, Landroid/widget/ImageView;-><init>(Landroid/content/Context;)V

    invoke-static {}, Lnet/gogame/gopay/sdk/support/m;->c()Landroid/graphics/Bitmap;

    move-result-object v4

    if-eqz v4, :cond_1

    invoke-virtual {v2, v4}, Landroid/widget/ImageView;->setImageBitmap(Landroid/graphics/Bitmap;)V

    :cond_1
    sget-object v4, Landroid/widget/ImageView$ScaleType;->FIT_CENTER:Landroid/widget/ImageView$ScaleType;

    invoke-virtual {v2, v4}, Landroid/widget/ImageView;->setScaleType(Landroid/widget/ImageView$ScaleType;)V

    new-instance v4, Landroid/widget/LinearLayout$LayoutParams;

    const/16 v5, 0x14

    invoke-virtual {p0, v5}, Lnet/gogame/gopay/sdk/iab/i;->a(I)I

    move-result v7

    invoke-virtual {p0, v5}, Lnet/gogame/gopay/sdk/iab/i;->a(I)I

    move-result v5

    invoke-direct {v4, v7, v5}, Landroid/widget/LinearLayout$LayoutParams;-><init>(II)V

    const/16 v5, 0x10

    iput v5, v4, Landroid/widget/LinearLayout$LayoutParams;->gravity:I

    const/16 v5, 0x8

    invoke-virtual {p0, v5}, Lnet/gogame/gopay/sdk/iab/i;->a(I)I

    move-result v5

    invoke-virtual {v4, v1, v1, v5, v1}, Landroid/widget/LinearLayout$LayoutParams;->setMargins(IIII)V

    invoke-virtual {p2, v2, v4}, Landroid/widget/LinearLayout;->addView(Landroid/view/View;Landroid/view/ViewGroup$LayoutParams;)V

    invoke-static {v0}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v4

    invoke-virtual {v2, v4}, Landroid/widget/ImageView;->setTag(Ljava/lang/Object;)V

    new-instance v2, Lnet/gogame/gopay/sdk/iab/bv;

    invoke-direct {v2, v3, v6, p1}, Lnet/gogame/gopay/sdk/iab/bv;-><init>(Landroid/widget/ImageView;Landroid/widget/TextView;I)V

    invoke-virtual {p2, v2}, Landroid/widget/LinearLayout;->setTag(Ljava/lang/Object;)V

    :cond_2
    invoke-static {v0}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v0

    invoke-virtual {p2, v0}, Landroid/view/View;->findViewWithTag(Ljava/lang/Object;)Landroid/view/View;

    move-result-object v0

    iget v2, p0, Lnet/gogame/gopay/sdk/iab/i;->e:I

    const/4 v3, 0x4

    if-ne p1, v2, :cond_3

    goto :goto_1

    :cond_3
    const/4 v1, 0x4

    :goto_1
    invoke-virtual {v0, v1}, Landroid/view/View;->setVisibility(I)V

    invoke-virtual {p2}, Landroid/view/View;->getTag()Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Lnet/gogame/gopay/sdk/iab/bv;

    iput p1, v0, Lnet/gogame/gopay/sdk/iab/bv;->c:I

    iget-object v1, v0, Lnet/gogame/gopay/sdk/iab/bv;->b:Landroid/widget/TextView;

    iget v2, v0, Lnet/gogame/gopay/sdk/iab/bv;->c:I

    invoke-virtual {p0, v2}, Lnet/gogame/gopay/sdk/iab/i;->getItem(I)Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Lnet/gogame/gopay/sdk/iab/a;

    invoke-virtual {v2}, Lnet/gogame/gopay/sdk/iab/a;->getDisplayName()Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v1, v2}, Landroid/widget/TextView;->setText(Ljava/lang/CharSequence;)V

    iget-object v1, v0, Lnet/gogame/gopay/sdk/iab/bv;->a:Landroid/widget/ImageView;

    invoke-virtual {v1, v3}, Landroid/widget/ImageView;->setVisibility(I)V

    invoke-virtual {p0}, Lnet/gogame/gopay/sdk/iab/i;->a()Ljava/lang/String;

    move-result-object v1

    iget v2, v0, Lnet/gogame/gopay/sdk/iab/bv;->c:I

    invoke-virtual {p0, v2}, Lnet/gogame/gopay/sdk/iab/i;->getItem(I)Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Lnet/gogame/gopay/sdk/iab/a;

    invoke-virtual {v2}, Lnet/gogame/gopay/sdk/iab/a;->getDisplayIcon()Ljava/lang/String;

    move-result-object v2

    new-instance v3, Lnet/gogame/gopay/sdk/iab/j;

    invoke-direct {v3, p0, v0, p1}, Lnet/gogame/gopay/sdk/iab/j;-><init>(Lnet/gogame/gopay/sdk/iab/i;Lnet/gogame/gopay/sdk/iab/bv;I)V

    invoke-static {v1, v2, v3}, Lnet/gogame/gopay/sdk/support/m;->b(Ljava/lang/String;Ljava/lang/String;Lnet/gogame/gopay/sdk/support/q;)V

    return-object p2
.end method

.method private b(ILandroid/view/View;)Landroid/view/View;
    .locals 8

    const/4 v0, 0x0

    const/4 v1, -0x1

    if-nez p2, :cond_0

    new-instance p2, Landroid/widget/RelativeLayout;

    iget-object v2, p0, Lnet/gogame/gopay/sdk/a;->a:Landroid/content/Context;

    invoke-direct {p2, v2}, Landroid/widget/RelativeLayout;-><init>(Landroid/content/Context;)V

    new-instance v2, Landroid/widget/AbsListView$LayoutParams;

    const/16 v3, 0x3c

    invoke-virtual {p0, v3}, Lnet/gogame/gopay/sdk/iab/i;->a(I)I

    move-result v3

    invoke-direct {v2, v1, v3}, Landroid/widget/AbsListView$LayoutParams;-><init>(II)V

    invoke-virtual {p2, v2}, Landroid/widget/RelativeLayout;->setLayoutParams(Landroid/view/ViewGroup$LayoutParams;)V

    new-instance v2, Landroid/widget/ImageView;

    iget-object v3, p0, Lnet/gogame/gopay/sdk/a;->a:Landroid/content/Context;

    invoke-direct {v2, v3}, Landroid/widget/ImageView;-><init>(Landroid/content/Context;)V

    const/4 v3, 0x1

    invoke-static {v3}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v4

    invoke-virtual {v2, v4}, Landroid/widget/ImageView;->setTag(Ljava/lang/Object;)V

    sget-object v4, Landroid/widget/ImageView$ScaleType;->FIT_CENTER:Landroid/widget/ImageView$ScaleType;

    invoke-virtual {v2, v4}, Landroid/widget/ImageView;->setScaleType(Landroid/widget/ImageView$ScaleType;)V

    new-instance v4, Landroid/widget/RelativeLayout$LayoutParams;

    const/4 v5, -0x2

    invoke-direct {v4, v5, v5}, Landroid/widget/RelativeLayout$LayoutParams;-><init>(II)V

    const/16 v5, 0xd

    invoke-virtual {v4, v5}, Landroid/widget/RelativeLayout$LayoutParams;->addRule(I)V

    invoke-virtual {p2, v2, v4}, Landroid/widget/RelativeLayout;->addView(Landroid/view/View;Landroid/view/ViewGroup$LayoutParams;)V

    new-instance v4, Landroid/widget/TextView;

    iget-object v6, p0, Lnet/gogame/gopay/sdk/a;->a:Landroid/content/Context;

    invoke-direct {v4, v6}, Landroid/widget/TextView;-><init>(Landroid/content/Context;)V

    invoke-virtual {v4, v0}, Landroid/widget/TextView;->setBackgroundColor(I)V

    const/high16 v6, -0x1000000

    invoke-virtual {v4, v6}, Landroid/widget/TextView;->setTextColor(I)V

    const/16 v6, 0x11

    invoke-virtual {v4, v6}, Landroid/widget/TextView;->setGravity(I)V

    const/4 v6, 0x3

    const/high16 v7, 0x40c00000    # 6.0f

    invoke-virtual {v4, v6, v7}, Landroid/widget/TextView;->setTextSize(IF)V

    const/4 v6, 0x2

    invoke-static {v6}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v6

    invoke-virtual {v4, v6}, Landroid/widget/TextView;->setTag(Ljava/lang/Object;)V

    new-instance v6, Landroid/widget/RelativeLayout$LayoutParams;

    invoke-direct {v6, v1, v1}, Landroid/widget/RelativeLayout$LayoutParams;-><init>(II)V

    invoke-virtual {v6, v5}, Landroid/widget/RelativeLayout$LayoutParams;->addRule(I)V

    invoke-virtual {p0, v3}, Lnet/gogame/gopay/sdk/iab/i;->a(I)I

    move-result v5

    invoke-virtual {p0, v3}, Lnet/gogame/gopay/sdk/iab/i;->a(I)I

    move-result v3

    invoke-virtual {v6, v5, v0, v3, v0}, Landroid/widget/RelativeLayout$LayoutParams;->setMargins(IIII)V

    invoke-virtual {p2, v4, v6}, Landroid/widget/RelativeLayout;->addView(Landroid/view/View;Landroid/view/ViewGroup$LayoutParams;)V

    new-instance v3, Lnet/gogame/gopay/sdk/iab/bv;

    invoke-direct {v3, v2, v4, p1}, Lnet/gogame/gopay/sdk/iab/bv;-><init>(Landroid/widget/ImageView;Landroid/widget/TextView;I)V

    invoke-virtual {p2, v3}, Landroid/view/View;->setTag(Ljava/lang/Object;)V

    :cond_0
    iget v2, p0, Lnet/gogame/gopay/sdk/iab/i;->e:I

    if-ne p1, v2, :cond_1

    iput-object p2, p0, Lnet/gogame/gopay/sdk/iab/i;->f:Landroid/view/View;

    :goto_0
    invoke-virtual {p2, v1}, Landroid/view/View;->setBackgroundColor(I)V

    goto :goto_1

    :cond_1
    const/16 v1, 0xf1

    invoke-static {v1, v1, v1}, Landroid/graphics/Color;->rgb(III)I

    move-result v1

    goto :goto_0

    :goto_1
    invoke-virtual {p2}, Landroid/view/View;->getTag()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Lnet/gogame/gopay/sdk/iab/bv;

    iput p1, v1, Lnet/gogame/gopay/sdk/iab/bv;->c:I

    iget-object v2, v1, Lnet/gogame/gopay/sdk/iab/bv;->b:Landroid/widget/TextView;

    invoke-virtual {v2, v0}, Landroid/widget/TextView;->setVisibility(I)V

    iget-object v0, v1, Lnet/gogame/gopay/sdk/iab/bv;->b:Landroid/widget/TextView;

    iget v2, v1, Lnet/gogame/gopay/sdk/iab/bv;->c:I

    invoke-virtual {p0, v2}, Lnet/gogame/gopay/sdk/iab/i;->getItem(I)Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Lnet/gogame/gopay/sdk/iab/a;

    invoke-virtual {v2}, Lnet/gogame/gopay/sdk/iab/a;->getDisplayName()Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v0, v2}, Landroid/widget/TextView;->setText(Ljava/lang/CharSequence;)V

    iget-object v0, v1, Lnet/gogame/gopay/sdk/iab/bv;->a:Landroid/widget/ImageView;

    const/4 v2, 0x4

    invoke-virtual {v0, v2}, Landroid/widget/ImageView;->setVisibility(I)V

    invoke-virtual {p0}, Lnet/gogame/gopay/sdk/iab/i;->a()Ljava/lang/String;

    move-result-object v0

    iget v2, v1, Lnet/gogame/gopay/sdk/iab/bv;->c:I

    invoke-virtual {p0, v2}, Lnet/gogame/gopay/sdk/iab/i;->getItem(I)Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Lnet/gogame/gopay/sdk/iab/a;

    invoke-virtual {v2}, Lnet/gogame/gopay/sdk/iab/a;->getDisplayIcon()Ljava/lang/String;

    move-result-object v2

    new-instance v3, Lnet/gogame/gopay/sdk/iab/k;

    invoke-direct {v3, p0, v1, p1}, Lnet/gogame/gopay/sdk/iab/k;-><init>(Lnet/gogame/gopay/sdk/iab/i;Lnet/gogame/gopay/sdk/iab/bv;I)V

    invoke-static {v0, v2, v3}, Lnet/gogame/gopay/sdk/support/m;->b(Ljava/lang/String;Ljava/lang/String;Lnet/gogame/gopay/sdk/support/q;)V

    return-object p2
.end method


# virtual methods
.method public final getDropDownView(ILandroid/view/View;Landroid/view/ViewGroup;)Landroid/view/View;
    .locals 1

    iget p3, p0, Lnet/gogame/gopay/sdk/iab/i;->d:I

    const/4 v0, 0x1

    if-ne p3, v0, :cond_0

    invoke-direct {p0, p1, p2}, Lnet/gogame/gopay/sdk/iab/i;->a(ILandroid/view/View;)Landroid/view/View;

    move-result-object p1

    return-object p1

    :cond_0
    invoke-direct {p0, p1, p2}, Lnet/gogame/gopay/sdk/iab/i;->b(ILandroid/view/View;)Landroid/view/View;

    move-result-object p1

    return-object p1
.end method

.method public final getView(ILandroid/view/View;Landroid/view/ViewGroup;)Landroid/view/View;
    .locals 1

    iget p3, p0, Lnet/gogame/gopay/sdk/iab/i;->d:I

    const/4 v0, 0x1

    if-ne p3, v0, :cond_0

    invoke-direct {p0, p1, p2}, Lnet/gogame/gopay/sdk/iab/i;->a(ILandroid/view/View;)Landroid/view/View;

    move-result-object p1

    return-object p1

    :cond_0
    invoke-direct {p0, p1, p2}, Lnet/gogame/gopay/sdk/iab/i;->b(ILandroid/view/View;)Landroid/view/View;

    move-result-object p1

    return-object p1
.end method
