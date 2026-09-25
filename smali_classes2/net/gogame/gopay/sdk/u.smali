.class public final Lnet/gogame/gopay/sdk/u;
.super Landroid/widget/BaseAdapter;


# instance fields
.field a:Lnet/gogame/gopay/sdk/h;

.field private final b:Landroid/app/Activity;

.field private final c:Lnet/gogame/gopay/sdk/m;


# direct methods
.method public constructor <init>(Landroid/app/Activity;Lnet/gogame/gopay/sdk/m;)V
    .locals 0

    invoke-direct {p0}, Landroid/widget/BaseAdapter;-><init>()V

    iput-object p1, p0, Lnet/gogame/gopay/sdk/u;->b:Landroid/app/Activity;

    iput-object p2, p0, Lnet/gogame/gopay/sdk/u;->c:Lnet/gogame/gopay/sdk/m;

    return-void
.end method

.method static synthetic a(Lnet/gogame/gopay/sdk/u;)Landroid/app/Activity;
    .locals 0

    iget-object p0, p0, Lnet/gogame/gopay/sdk/u;->b:Landroid/app/Activity;

    return-object p0
.end method

.method static synthetic b(Lnet/gogame/gopay/sdk/u;)Lnet/gogame/gopay/sdk/m;
    .locals 0

    iget-object p0, p0, Lnet/gogame/gopay/sdk/u;->c:Lnet/gogame/gopay/sdk/m;

    return-object p0
.end method


# virtual methods
.method public final getCount()I
    .locals 1

    iget-object v0, p0, Lnet/gogame/gopay/sdk/u;->a:Lnet/gogame/gopay/sdk/h;

    if-nez v0, :cond_0

    const/4 v0, 0x0

    return v0

    :cond_0
    iget-object v0, p0, Lnet/gogame/gopay/sdk/u;->a:Lnet/gogame/gopay/sdk/h;

    iget-object v0, v0, Lnet/gogame/gopay/sdk/h;->b:Ljava/util/List;

    invoke-interface {v0}, Ljava/util/List;->size()I

    move-result v0

    return v0
.end method

.method public final getItem(I)Ljava/lang/Object;
    .locals 1

    iget-object v0, p0, Lnet/gogame/gopay/sdk/u;->a:Lnet/gogame/gopay/sdk/h;

    iget-object v0, v0, Lnet/gogame/gopay/sdk/h;->b:Ljava/util/List;

    invoke-interface {v0, p1}, Ljava/util/List;->get(I)Ljava/lang/Object;

    move-result-object p1

    return-object p1
.end method

.method public final getItemId(I)J
    .locals 2

    int-to-long v0, p1

    return-wide v0
.end method

.method public final getView(ILandroid/view/View;Landroid/view/ViewGroup;)Landroid/view/View;
    .locals 12

    invoke-virtual {p0, p1}, Lnet/gogame/gopay/sdk/u;->getItem(I)Ljava/lang/Object;

    move-result-object p1

    check-cast p1, Lorg/onepf/oms/appstore/googleUtils/SkuDetails;

    invoke-virtual {p3}, Landroid/view/ViewGroup;->getContext()Landroid/content/Context;

    move-result-object p2

    new-instance p3, Landroid/widget/LinearLayout;

    invoke-direct {p3, p2}, Landroid/widget/LinearLayout;-><init>(Landroid/content/Context;)V

    const/4 v0, 0x0

    invoke-virtual {p3, v0}, Landroid/widget/LinearLayout;->setOrientation(I)V

    const/4 v0, -0x1

    invoke-virtual {p3, v0}, Landroid/widget/LinearLayout;->setBackgroundColor(I)V

    const/high16 v0, 0x41000000    # 8.0f

    invoke-static {p2, v0}, Lnet/gogame/gopay/sdk/support/DisplayUtils;->pxFromDp(Landroid/content/Context;F)I

    move-result v1

    invoke-static {p2, v0}, Lnet/gogame/gopay/sdk/support/DisplayUtils;->pxFromDp(Landroid/content/Context;F)I

    move-result v2

    invoke-static {p2, v0}, Lnet/gogame/gopay/sdk/support/DisplayUtils;->pxFromDp(Landroid/content/Context;F)I

    move-result v3

    invoke-static {p2, v0}, Lnet/gogame/gopay/sdk/support/DisplayUtils;->pxFromDp(Landroid/content/Context;F)I

    move-result v4

    invoke-virtual {p3, v1, v2, v3, v4}, Landroid/widget/LinearLayout;->setPadding(IIII)V

    new-instance v1, Landroid/widget/TextView;

    invoke-direct {v1, p2}, Landroid/widget/TextView;-><init>(Landroid/content/Context;)V

    const/4 v2, 0x3

    invoke-virtual {v1, v2, v0}, Landroid/widget/TextView;->setTextSize(IF)V

    sget-object v3, Landroid/graphics/Typeface;->DEFAULT_BOLD:Landroid/graphics/Typeface;

    invoke-virtual {v1, v3}, Landroid/widget/TextView;->setTypeface(Landroid/graphics/Typeface;)V

    const/high16 v3, -0x1000000

    invoke-virtual {v1, v3}, Landroid/widget/TextView;->setTextColor(I)V

    invoke-virtual {p1}, Lorg/onepf/oms/appstore/googleUtils/SkuDetails;->getTitle()Ljava/lang/String;

    move-result-object v3

    invoke-virtual {v1, v3}, Landroid/widget/TextView;->setText(Ljava/lang/CharSequence;)V

    sget v3, Landroid/os/Build$VERSION;->SDK_INT:I

    const/16 v4, 0x11

    if-lt v3, v4, :cond_0

    const/4 v3, 0x5

    invoke-virtual {v1, v3}, Landroid/widget/TextView;->setTextAlignment(I)V

    :cond_0
    new-instance v3, Landroid/widget/LinearLayout$LayoutParams;

    const/4 v5, -0x2

    invoke-direct {v3, v5, v5}, Landroid/widget/LinearLayout$LayoutParams;-><init>(II)V

    const/high16 v6, 0x3f800000    # 1.0f

    iput v6, v3, Landroid/widget/LinearLayout$LayoutParams;->weight:F

    const/high16 v6, 0x40c00000    # 6.0f

    invoke-static {p2, v6}, Lnet/gogame/gopay/sdk/support/DisplayUtils;->pxFromDp(Landroid/content/Context;F)I

    move-result v7

    const/4 v8, 0x0

    invoke-static {p2, v8}, Lnet/gogame/gopay/sdk/support/DisplayUtils;->pxFromDp(Landroid/content/Context;F)I

    move-result v9

    invoke-static {p2, v8}, Lnet/gogame/gopay/sdk/support/DisplayUtils;->pxFromDp(Landroid/content/Context;F)I

    move-result v10

    invoke-static {p2, v8}, Lnet/gogame/gopay/sdk/support/DisplayUtils;->pxFromDp(Landroid/content/Context;F)I

    move-result v11

    invoke-virtual {v3, v7, v9, v10, v11}, Landroid/widget/LinearLayout$LayoutParams;->setMargins(IIII)V

    invoke-virtual {v1, v3}, Landroid/widget/TextView;->setLayoutParams(Landroid/view/ViewGroup$LayoutParams;)V

    invoke-virtual {p3, v1}, Landroid/widget/LinearLayout;->addView(Landroid/view/View;)V

    new-instance v1, Landroid/widget/TextView;

    invoke-direct {v1, p2}, Landroid/widget/TextView;-><init>(Landroid/content/Context;)V

    invoke-virtual {v1, v2, v0}, Landroid/widget/TextView;->setTextSize(IF)V

    sget-object v0, Landroid/graphics/Typeface;->DEFAULT:Landroid/graphics/Typeface;

    invoke-virtual {v1, v0}, Landroid/widget/TextView;->setTypeface(Landroid/graphics/Typeface;)V

    const-string v0, "#818181"

    invoke-static {v0}, Landroid/graphics/Color;->parseColor(Ljava/lang/String;)I

    move-result v0

    invoke-virtual {v1, v0}, Landroid/widget/TextView;->setTextColor(I)V

    invoke-virtual {p1}, Lorg/onepf/oms/appstore/googleUtils/SkuDetails;->getPrice()Ljava/lang/String;

    move-result-object v0

    invoke-virtual {v1, v0}, Landroid/widget/TextView;->setText(Ljava/lang/CharSequence;)V

    sget v0, Landroid/os/Build$VERSION;->SDK_INT:I

    if-lt v0, v4, :cond_1

    const/4 v0, 0x6

    invoke-virtual {v1, v0}, Landroid/widget/TextView;->setTextAlignment(I)V

    :cond_1
    new-instance v0, Landroid/widget/LinearLayout$LayoutParams;

    invoke-direct {v0, v5, v5}, Landroid/widget/LinearLayout$LayoutParams;-><init>(II)V

    invoke-static {p2, v8}, Lnet/gogame/gopay/sdk/support/DisplayUtils;->pxFromDp(Landroid/content/Context;F)I

    move-result v2

    invoke-static {p2, v8}, Lnet/gogame/gopay/sdk/support/DisplayUtils;->pxFromDp(Landroid/content/Context;F)I

    move-result v3

    invoke-static {p2, v6}, Lnet/gogame/gopay/sdk/support/DisplayUtils;->pxFromDp(Landroid/content/Context;F)I

    move-result v4

    invoke-static {p2, v8}, Lnet/gogame/gopay/sdk/support/DisplayUtils;->pxFromDp(Landroid/content/Context;F)I

    move-result v5

    invoke-virtual {v0, v2, v3, v4, v5}, Landroid/widget/LinearLayout$LayoutParams;->setMargins(IIII)V

    invoke-virtual {v1, v0}, Landroid/widget/TextView;->setLayoutParams(Landroid/view/ViewGroup$LayoutParams;)V

    invoke-virtual {p3, v1}, Landroid/widget/LinearLayout;->addView(Landroid/view/View;)V

    new-instance v0, Landroid/graphics/drawable/GradientDrawable;

    sget-object v1, Landroid/graphics/drawable/GradientDrawable$Orientation;->TOP_BOTTOM:Landroid/graphics/drawable/GradientDrawable$Orientation;

    const/4 v2, 0x2

    new-array v2, v2, [I

    fill-array-data v2, :array_0

    invoke-direct {v0, v1, v2}, Landroid/graphics/drawable/GradientDrawable;-><init>(Landroid/graphics/drawable/GradientDrawable$Orientation;[I)V

    const/high16 v1, 0x3fc00000    # 1.5f

    invoke-static {p2, v1}, Lnet/gogame/gopay/sdk/support/DisplayUtils;->pxFromDp(Landroid/content/Context;F)I

    move-result v1

    const-string v2, "#87b856"

    invoke-static {v2}, Landroid/graphics/Color;->parseColor(Ljava/lang/String;)I

    move-result v2

    invoke-virtual {v0, v1, v2}, Landroid/graphics/drawable/GradientDrawable;->setStroke(II)V

    const/high16 v1, 0x42480000    # 50.0f

    invoke-static {p2, v1}, Lnet/gogame/gopay/sdk/support/DisplayUtils;->pxFromDp(Landroid/content/Context;F)I

    move-result p2

    int-to-float p2, p2

    invoke-virtual {v0, p2}, Landroid/graphics/drawable/GradientDrawable;->setCornerRadius(F)V

    sget p2, Landroid/os/Build$VERSION;->SDK_INT:I

    const/16 v1, 0x10

    if-lt p2, v1, :cond_2

    invoke-virtual {p3, v0}, Landroid/widget/LinearLayout;->setBackground(Landroid/graphics/drawable/Drawable;)V

    :cond_2
    new-instance p2, Lnet/gogame/gopay/sdk/v;

    invoke-direct {p2, p0, p1}, Lnet/gogame/gopay/sdk/v;-><init>(Lnet/gogame/gopay/sdk/u;Lorg/onepf/oms/appstore/googleUtils/SkuDetails;)V

    invoke-virtual {p3, p2}, Landroid/widget/LinearLayout;->setOnClickListener(Landroid/view/View$OnClickListener;)V

    return-object p3

    nop

    :array_0
    .array-data 4
        -0x1
        -0x1
    .end array-data
.end method
