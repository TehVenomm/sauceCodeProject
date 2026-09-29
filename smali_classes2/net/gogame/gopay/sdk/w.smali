.class public final Lnet/gogame/gopay/sdk/w;
.super Landroid/widget/LinearLayout;


# instance fields
.field private final a:Lnet/gogame/gopay/sdk/u;


# direct methods
.method public constructor <init>(Landroid/app/Activity;Lnet/gogame/gopay/sdk/m;)V
    .locals 6

    invoke-direct {p0, p1}, Landroid/widget/LinearLayout;-><init>(Landroid/content/Context;)V

    new-instance v0, Lnet/gogame/gopay/sdk/u;

    invoke-direct {v0, p1, p2}, Lnet/gogame/gopay/sdk/u;-><init>(Landroid/app/Activity;Lnet/gogame/gopay/sdk/m;)V

    iput-object v0, p0, Lnet/gogame/gopay/sdk/w;->a:Lnet/gogame/gopay/sdk/u;

    const/4 p2, 0x1

    invoke-virtual {p0, p2}, Lnet/gogame/gopay/sdk/w;->setOrientation(I)V

    const/4 p2, -0x1

    invoke-virtual {p0, p2}, Lnet/gogame/gopay/sdk/w;->setBackgroundColor(I)V

    new-instance v0, Landroid/widget/ListView;

    invoke-direct {v0, p1}, Landroid/widget/ListView;-><init>(Landroid/content/Context;)V

    const/4 v1, 0x0

    invoke-virtual {v0, v1}, Landroid/widget/ListView;->setHeaderDividersEnabled(Z)V

    invoke-virtual {v0, v1}, Landroid/widget/ListView;->setFooterDividersEnabled(Z)V

    const/4 v2, 0x0

    invoke-virtual {v0, v2}, Landroid/widget/ListView;->setDivider(Landroid/graphics/drawable/Drawable;)V

    const/high16 v2, 0x40c00000    # 6.0f

    invoke-static {p1, v2}, Lnet/gogame/gopay/sdk/support/DisplayUtils;->pxFromDp(Landroid/content/Context;F)I

    move-result v2

    invoke-virtual {v0, v2}, Landroid/widget/ListView;->setDividerHeight(I)V

    iget-object v2, p0, Lnet/gogame/gopay/sdk/w;->a:Lnet/gogame/gopay/sdk/u;

    invoke-virtual {v0, v2}, Landroid/widget/ListView;->setAdapter(Landroid/widget/ListAdapter;)V

    const/high16 v2, 0x40800000    # 4.0f

    invoke-static {p1, v2}, Lnet/gogame/gopay/sdk/support/DisplayUtils;->pxFromDp(Landroid/content/Context;F)I

    move-result v3

    const/high16 v4, 0x41000000    # 8.0f

    invoke-static {p1, v4}, Lnet/gogame/gopay/sdk/support/DisplayUtils;->pxFromDp(Landroid/content/Context;F)I

    move-result v4

    invoke-static {p1, v2}, Lnet/gogame/gopay/sdk/support/DisplayUtils;->pxFromDp(Landroid/content/Context;F)I

    move-result v2

    const/high16 v5, 0x41d80000    # 27.0f

    invoke-static {p1, v5}, Lnet/gogame/gopay/sdk/support/DisplayUtils;->pxFromDp(Landroid/content/Context;F)I

    move-result p1

    invoke-virtual {v0, v3, v4, v2, p1}, Landroid/widget/ListView;->setPadding(IIII)V

    invoke-virtual {v0, v1}, Landroid/widget/ListView;->setClipToPadding(Z)V

    new-instance p1, Landroid/widget/LinearLayout$LayoutParams;

    invoke-direct {p1, p2, v1}, Landroid/widget/LinearLayout$LayoutParams;-><init>(II)V

    const/high16 p2, 0x3f800000    # 1.0f

    iput p2, p1, Landroid/widget/LinearLayout$LayoutParams;->weight:F

    invoke-virtual {v0, p1}, Landroid/widget/ListView;->setLayoutParams(Landroid/view/ViewGroup$LayoutParams;)V

    invoke-virtual {p0, v0}, Lnet/gogame/gopay/sdk/w;->addView(Landroid/view/View;)V

    return-void
.end method


# virtual methods
.method public final setData(Lnet/gogame/gopay/sdk/h;)V
    .locals 1

    iget-object v0, p0, Lnet/gogame/gopay/sdk/w;->a:Lnet/gogame/gopay/sdk/u;

    iput-object p1, v0, Lnet/gogame/gopay/sdk/u;->a:Lnet/gogame/gopay/sdk/h;

    invoke-virtual {v0}, Lnet/gogame/gopay/sdk/u;->notifyDataSetChanged()V

    return-void
.end method
