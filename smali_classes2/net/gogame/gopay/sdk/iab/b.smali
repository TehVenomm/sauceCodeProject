.class public final Lnet/gogame/gopay/sdk/iab/b;
.super Lnet/gogame/gopay/sdk/a;


# direct methods
.method public constructor <init>(Landroid/content/Context;)V
    .locals 0

    invoke-direct {p0, p1}, Lnet/gogame/gopay/sdk/a;-><init>(Landroid/content/Context;)V

    return-void
.end method

.method private a(ILandroid/view/View;)Landroid/view/View;
    .locals 16

    move-object/from16 v0, p0

    move/from16 v1, p1

    const/4 v2, 0x3

    const/4 v3, 0x4

    if-nez p2, :cond_1

    new-instance v4, Landroid/widget/LinearLayout;

    iget-object v5, v0, Lnet/gogame/gopay/sdk/a;->a:Landroid/content/Context;

    invoke-direct {v4, v5}, Landroid/widget/LinearLayout;-><init>(Landroid/content/Context;)V

    const/high16 v5, 0x3f800000    # 1.0f

    invoke-virtual {v4, v5}, Landroid/widget/LinearLayout;->setWeightSum(F)V

    const/4 v6, 0x0

    invoke-virtual {v4, v6}, Landroid/widget/LinearLayout;->setBackgroundColor(I)V

    invoke-virtual {v4, v6}, Landroid/widget/LinearLayout;->setOrientation(I)V

    const v7, 0x800003

    invoke-virtual {v4, v7}, Landroid/widget/LinearLayout;->setGravity(I)V

    new-instance v7, Landroid/widget/ImageView;

    iget-object v8, v0, Lnet/gogame/gopay/sdk/a;->a:Landroid/content/Context;

    invoke-direct {v7, v8}, Landroid/widget/ImageView;-><init>(Landroid/content/Context;)V

    const v8, 0x108000a

    invoke-virtual {v7, v8}, Landroid/widget/ImageView;->setImageResource(I)V

    sget-object v8, Landroid/widget/ImageView$ScaleType;->FIT_CENTER:Landroid/widget/ImageView$ScaleType;

    invoke-virtual {v7, v8}, Landroid/widget/ImageView;->setScaleType(Landroid/widget/ImageView$ScaleType;)V

    invoke-virtual {v0, v6}, Lnet/gogame/gopay/sdk/iab/b;->a(I)I

    move-result v8

    invoke-virtual {v0, v6}, Lnet/gogame/gopay/sdk/iab/b;->a(I)I

    move-result v9

    invoke-virtual {v0, v6}, Lnet/gogame/gopay/sdk/iab/b;->a(I)I

    move-result v10

    invoke-virtual {v0, v6}, Lnet/gogame/gopay/sdk/iab/b;->a(I)I

    move-result v11

    invoke-virtual {v7, v8, v9, v10, v11}, Landroid/widget/ImageView;->setPadding(IIII)V

    invoke-virtual {v7, v3}, Landroid/widget/ImageView;->setVisibility(I)V

    const/4 v8, 0x2

    invoke-static {v8}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v9

    invoke-virtual {v7, v9}, Landroid/widget/ImageView;->setTag(Ljava/lang/Object;)V

    new-instance v9, Landroid/widget/LinearLayout$LayoutParams;

    const/4 v10, -0x2

    const/4 v11, 0x0

    const/4 v12, -0x1

    invoke-direct {v9, v10, v12, v11}, Landroid/widget/LinearLayout$LayoutParams;-><init>(IIF)V

    const/16 v10, 0x10

    iput v10, v9, Landroid/widget/LinearLayout$LayoutParams;->gravity:I

    const/4 v13, 0x1

    invoke-virtual {v0, v13}, Lnet/gogame/gopay/sdk/iab/b;->a(I)I

    move-result v14

    invoke-virtual {v0, v8}, Lnet/gogame/gopay/sdk/iab/b;->a(I)I

    move-result v15

    invoke-virtual {v9, v14, v6, v15, v6}, Landroid/widget/LinearLayout$LayoutParams;->setMargins(IIII)V

    invoke-virtual {v4, v7, v9}, Landroid/widget/LinearLayout;->addView(Landroid/view/View;Landroid/view/ViewGroup$LayoutParams;)V

    new-instance v9, Landroid/widget/TextView;

    iget-object v14, v0, Lnet/gogame/gopay/sdk/a;->a:Landroid/content/Context;

    invoke-direct {v9, v14}, Landroid/widget/TextView;-><init>(Landroid/content/Context;)V

    invoke-virtual {v9, v6}, Landroid/widget/TextView;->setBackgroundColor(I)V

    const-string v14, "#000000"

    invoke-static {v14}, Landroid/graphics/Color;->parseColor(Ljava/lang/String;)I

    move-result v14

    invoke-virtual {v9, v14}, Landroid/widget/TextView;->setTextColor(I)V

    const/high16 v14, 0x41600000    # 14.0f

    invoke-virtual {v9, v8, v14}, Landroid/widget/TextView;->setTextSize(IF)V

    invoke-virtual {v9, v6, v6, v6, v6}, Landroid/widget/TextView;->setPadding(IIII)V

    const v15, 0x800013

    invoke-virtual {v9, v15}, Landroid/widget/TextView;->setGravity(I)V

    invoke-static {v13}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v15

    invoke-virtual {v9, v15}, Landroid/widget/TextView;->setTag(Ljava/lang/Object;)V

    invoke-virtual {v9, v13}, Landroid/widget/TextView;->setMaxLines(I)V

    new-instance v15, Landroid/widget/LinearLayout$LayoutParams;

    invoke-direct {v15, v12, v12, v5}, Landroid/widget/LinearLayout$LayoutParams;-><init>(IIF)V

    iput v10, v15, Landroid/widget/LinearLayout$LayoutParams;->gravity:I

    invoke-virtual {v0, v13}, Lnet/gogame/gopay/sdk/iab/b;->a(I)I

    move-result v5

    invoke-virtual {v0, v6}, Lnet/gogame/gopay/sdk/iab/b;->a(I)I

    move-result v12

    invoke-virtual {v0, v13}, Lnet/gogame/gopay/sdk/iab/b;->a(I)I

    move-result v8

    invoke-virtual {v0, v6}, Lnet/gogame/gopay/sdk/iab/b;->a(I)I

    move-result v10

    invoke-virtual {v15, v5, v12, v8, v10}, Landroid/widget/LinearLayout$LayoutParams;->setMargins(IIII)V

    invoke-virtual {v4, v9, v15}, Landroid/widget/LinearLayout;->addView(Landroid/view/View;Landroid/view/ViewGroup$LayoutParams;)V

    new-instance v5, Landroid/widget/ImageView;

    iget-object v8, v0, Lnet/gogame/gopay/sdk/a;->a:Landroid/content/Context;

    invoke-direct {v5, v8}, Landroid/widget/ImageView;-><init>(Landroid/content/Context;)V

    invoke-static {}, Lnet/gogame/gopay/sdk/support/m;->e()Landroid/graphics/Bitmap;

    move-result-object v8

    if-eqz v8, :cond_0

    invoke-virtual {v5, v8}, Landroid/widget/ImageView;->setImageBitmap(Landroid/graphics/Bitmap;)V

    :cond_0
    sget-object v8, Landroid/widget/ImageView$ScaleType;->FIT_XY:Landroid/widget/ImageView$ScaleType;

    invoke-virtual {v5, v8}, Landroid/widget/ImageView;->setScaleType(Landroid/widget/ImageView$ScaleType;)V

    invoke-virtual {v0, v13}, Lnet/gogame/gopay/sdk/iab/b;->a(I)I

    move-result v8

    invoke-virtual {v0, v13}, Lnet/gogame/gopay/sdk/iab/b;->a(I)I

    move-result v10

    invoke-virtual {v0, v13}, Lnet/gogame/gopay/sdk/iab/b;->a(I)I

    move-result v12

    invoke-virtual {v0, v13}, Lnet/gogame/gopay/sdk/iab/b;->a(I)I

    move-result v13

    invoke-virtual {v5, v8, v10, v12, v13}, Landroid/widget/ImageView;->setPadding(IIII)V

    invoke-virtual {v5, v3}, Landroid/widget/ImageView;->setVisibility(I)V

    invoke-static {v2}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v8

    invoke-virtual {v5, v8}, Landroid/widget/ImageView;->setTag(Ljava/lang/Object;)V

    new-instance v8, Landroid/widget/LinearLayout$LayoutParams;

    iget-object v10, v0, Lnet/gogame/gopay/sdk/a;->a:Landroid/content/Context;

    invoke-static {v10, v14}, Lnet/gogame/gopay/sdk/support/DisplayUtils;->pxFromDp(Landroid/content/Context;F)I

    move-result v10

    iget-object v12, v0, Lnet/gogame/gopay/sdk/a;->a:Landroid/content/Context;

    const/high16 v13, 0x41000000    # 8.0f

    invoke-static {v12, v13}, Lnet/gogame/gopay/sdk/support/DisplayUtils;->pxFromDp(Landroid/content/Context;F)I

    move-result v12

    invoke-direct {v8, v10, v12, v11}, Landroid/widget/LinearLayout$LayoutParams;-><init>(IIF)V

    const/16 v10, 0x10

    iput v10, v8, Landroid/widget/LinearLayout$LayoutParams;->gravity:I

    const/4 v10, 0x2

    invoke-virtual {v0, v10}, Lnet/gogame/gopay/sdk/iab/b;->a(I)I

    move-result v10

    const/16 v11, 0xa

    invoke-virtual {v0, v11}, Lnet/gogame/gopay/sdk/iab/b;->a(I)I

    move-result v11

    invoke-virtual {v8, v10, v6, v11, v6}, Landroid/widget/LinearLayout$LayoutParams;->setMargins(IIII)V

    invoke-virtual {v4, v5, v8}, Landroid/widget/LinearLayout;->addView(Landroid/view/View;Landroid/view/ViewGroup$LayoutParams;)V

    new-instance v5, Lnet/gogame/gopay/sdk/iab/bv;

    invoke-direct {v5, v7, v9, v1}, Lnet/gogame/gopay/sdk/iab/bv;-><init>(Landroid/widget/ImageView;Landroid/widget/TextView;I)V

    invoke-virtual {v4, v5}, Landroid/view/View;->setTag(Ljava/lang/Object;)V

    goto :goto_0

    :cond_1
    move-object/from16 v4, p2

    :goto_0
    invoke-static {v2}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v2

    invoke-virtual {v4, v2}, Landroid/view/View;->findViewWithTag(Ljava/lang/Object;)Landroid/view/View;

    move-result-object v2

    if-eqz v2, :cond_2

    invoke-virtual {v2, v3}, Landroid/view/View;->setVisibility(I)V

    :cond_2
    invoke-virtual {v4}, Landroid/view/View;->getTag()Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Lnet/gogame/gopay/sdk/iab/bv;

    iput v1, v2, Lnet/gogame/gopay/sdk/iab/bv;->c:I

    iget-object v5, v2, Lnet/gogame/gopay/sdk/iab/bv;->a:Landroid/widget/ImageView;

    invoke-virtual {v5, v3}, Landroid/widget/ImageView;->setVisibility(I)V

    iget-object v3, v2, Lnet/gogame/gopay/sdk/iab/bv;->b:Landroid/widget/TextView;

    iget v5, v2, Lnet/gogame/gopay/sdk/iab/bv;->c:I

    invoke-virtual {v0, v5}, Lnet/gogame/gopay/sdk/iab/b;->getItem(I)Ljava/lang/Object;

    move-result-object v5

    check-cast v5, Lnet/gogame/gopay/sdk/k;

    invoke-virtual {v5}, Lnet/gogame/gopay/sdk/k;->getDisplayName()Ljava/lang/String;

    move-result-object v5

    invoke-virtual {v3, v5}, Landroid/widget/TextView;->setText(Ljava/lang/CharSequence;)V

    invoke-virtual/range {p0 .. p0}, Lnet/gogame/gopay/sdk/iab/b;->a()Ljava/lang/String;

    move-result-object v3

    iget v5, v2, Lnet/gogame/gopay/sdk/iab/bv;->c:I

    invoke-virtual {v0, v5}, Lnet/gogame/gopay/sdk/iab/b;->getItem(I)Ljava/lang/Object;

    move-result-object v5

    check-cast v5, Lnet/gogame/gopay/sdk/k;

    invoke-virtual {v5}, Lnet/gogame/gopay/sdk/k;->getDisplayIcon()Ljava/lang/String;

    move-result-object v5

    new-instance v6, Lnet/gogame/gopay/sdk/iab/c;

    invoke-direct {v6, v0, v2, v1}, Lnet/gogame/gopay/sdk/iab/c;-><init>(Lnet/gogame/gopay/sdk/iab/b;Lnet/gogame/gopay/sdk/iab/bv;I)V

    invoke-static {v3, v5, v6}, Lnet/gogame/gopay/sdk/support/m;->c(Ljava/lang/String;Ljava/lang/String;Lnet/gogame/gopay/sdk/support/q;)V

    return-object v4
.end method


# virtual methods
.method public final getDropDownView(ILandroid/view/View;Landroid/view/ViewGroup;)Landroid/view/View;
    .locals 1

    invoke-direct {p0, p1, p2}, Lnet/gogame/gopay/sdk/iab/b;->a(ILandroid/view/View;)Landroid/view/View;

    move-result-object p1

    const/4 p2, 0x0

    invoke-virtual {p1, p2}, Landroid/view/View;->setBackgroundColor(I)V

    new-instance p2, Landroid/widget/AbsListView$LayoutParams;

    const/16 p3, 0x1e

    invoke-virtual {p0, p3}, Lnet/gogame/gopay/sdk/iab/b;->a(I)I

    move-result p3

    const/4 v0, -0x1

    invoke-direct {p2, v0, p3}, Landroid/widget/AbsListView$LayoutParams;-><init>(II)V

    invoke-virtual {p1, p2}, Landroid/view/View;->setLayoutParams(Landroid/view/ViewGroup$LayoutParams;)V

    return-object p1
.end method

.method public final getView(ILandroid/view/View;Landroid/view/ViewGroup;)Landroid/view/View;
    .locals 2

    invoke-direct {p0, p1, p2}, Lnet/gogame/gopay/sdk/iab/b;->a(ILandroid/view/View;)Landroid/view/View;

    move-result-object p1

    const/4 p2, 0x0

    invoke-virtual {p1, p2}, Landroid/view/View;->setBackgroundColor(I)V

    new-instance p3, Landroid/view/ViewGroup$LayoutParams;

    const/16 v0, 0x1e

    invoke-virtual {p0, v0}, Lnet/gogame/gopay/sdk/iab/b;->a(I)I

    move-result v0

    const/4 v1, -0x1

    invoke-direct {p3, v1, v0}, Landroid/view/ViewGroup$LayoutParams;-><init>(II)V

    invoke-virtual {p1, p3}, Landroid/view/View;->setLayoutParams(Landroid/view/ViewGroup$LayoutParams;)V

    const/4 p3, 0x3

    invoke-static {p3}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object p3

    invoke-virtual {p1, p3}, Landroid/view/View;->findViewWithTag(Ljava/lang/Object;)Landroid/view/View;

    move-result-object p3

    if-eqz p3, :cond_0

    invoke-virtual {p0}, Lnet/gogame/gopay/sdk/iab/b;->getCount()I

    move-result v0

    const/4 v1, 0x1

    if-le v0, v1, :cond_0

    invoke-virtual {p3, p2}, Landroid/view/View;->setVisibility(I)V

    :cond_0
    return-object p1
.end method
