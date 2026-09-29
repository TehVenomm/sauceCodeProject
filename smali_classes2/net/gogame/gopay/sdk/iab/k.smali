.class final Lnet/gogame/gopay/sdk/iab/k;
.super Ljava/lang/Object;

# interfaces
.implements Lnet/gogame/gopay/sdk/support/q;


# instance fields
.field a:Lnet/gogame/gopay/sdk/iab/bv;

.field final synthetic b:Lnet/gogame/gopay/sdk/iab/bv;

.field final synthetic c:I

.field final synthetic d:Lnet/gogame/gopay/sdk/iab/i;


# direct methods
.method constructor <init>(Lnet/gogame/gopay/sdk/iab/i;Lnet/gogame/gopay/sdk/iab/bv;I)V
    .locals 0

    iput-object p1, p0, Lnet/gogame/gopay/sdk/iab/k;->d:Lnet/gogame/gopay/sdk/iab/i;

    iput-object p2, p0, Lnet/gogame/gopay/sdk/iab/k;->b:Lnet/gogame/gopay/sdk/iab/bv;

    iput p3, p0, Lnet/gogame/gopay/sdk/iab/k;->c:I

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    iget-object p1, p0, Lnet/gogame/gopay/sdk/iab/k;->b:Lnet/gogame/gopay/sdk/iab/bv;

    iput-object p1, p0, Lnet/gogame/gopay/sdk/iab/k;->a:Lnet/gogame/gopay/sdk/iab/bv;

    return-void
.end method


# virtual methods
.method public final a(Landroid/graphics/Bitmap;)V
    .locals 6

    iget-object v0, p0, Lnet/gogame/gopay/sdk/iab/k;->a:Lnet/gogame/gopay/sdk/iab/bv;

    iget v0, v0, Lnet/gogame/gopay/sdk/iab/bv;->c:I

    iget v1, p0, Lnet/gogame/gopay/sdk/iab/k;->c:I

    if-ne v0, v1, :cond_1

    const/4 v0, 0x4

    const/4 v1, 0x0

    if-eqz p1, :cond_0

    invoke-virtual {p1}, Landroid/graphics/Bitmap;->getWidth()I

    move-result v2

    invoke-virtual {p1}, Landroid/graphics/Bitmap;->getHeight()I

    move-result v3

    new-instance v4, Landroid/graphics/drawable/BitmapDrawable;

    iget-object v5, p0, Lnet/gogame/gopay/sdk/iab/k;->d:Lnet/gogame/gopay/sdk/iab/i;

    iget-object v5, v5, Lnet/gogame/gopay/sdk/a;->a:Landroid/content/Context;

    invoke-virtual {v5}, Landroid/content/Context;->getResources()Landroid/content/res/Resources;

    move-result-object v5

    invoke-direct {v4, v5, p1}, Landroid/graphics/drawable/BitmapDrawable;-><init>(Landroid/content/res/Resources;Landroid/graphics/Bitmap;)V

    invoke-virtual {v4, v1, v1, v2, v3}, Landroid/graphics/drawable/Drawable;->setBounds(IIII)V

    iget-object p1, p0, Lnet/gogame/gopay/sdk/iab/k;->a:Lnet/gogame/gopay/sdk/iab/bv;

    iget-object p1, p1, Lnet/gogame/gopay/sdk/iab/bv;->a:Landroid/widget/ImageView;

    invoke-virtual {p1, v4}, Landroid/widget/ImageView;->setImageDrawable(Landroid/graphics/drawable/Drawable;)V

    iget-object p1, p0, Lnet/gogame/gopay/sdk/iab/k;->a:Lnet/gogame/gopay/sdk/iab/bv;

    iget-object p1, p1, Lnet/gogame/gopay/sdk/iab/bv;->a:Landroid/widget/ImageView;

    iget-object v4, p0, Lnet/gogame/gopay/sdk/iab/k;->d:Lnet/gogame/gopay/sdk/iab/i;

    invoke-virtual {v4, v2}, Lnet/gogame/gopay/sdk/iab/i;->a(I)I

    move-result v2

    invoke-virtual {p1, v2}, Landroid/widget/ImageView;->setMinimumWidth(I)V

    iget-object p1, p0, Lnet/gogame/gopay/sdk/iab/k;->a:Lnet/gogame/gopay/sdk/iab/bv;

    iget-object p1, p1, Lnet/gogame/gopay/sdk/iab/bv;->a:Landroid/widget/ImageView;

    iget-object v2, p0, Lnet/gogame/gopay/sdk/iab/k;->d:Lnet/gogame/gopay/sdk/iab/i;

    invoke-virtual {v2, v3}, Lnet/gogame/gopay/sdk/iab/i;->a(I)I

    move-result v2

    invoke-virtual {p1, v2}, Landroid/widget/ImageView;->setMinimumHeight(I)V

    iget-object p1, p0, Lnet/gogame/gopay/sdk/iab/k;->a:Lnet/gogame/gopay/sdk/iab/bv;

    iget-object p1, p1, Lnet/gogame/gopay/sdk/iab/bv;->b:Landroid/widget/TextView;

    invoke-virtual {p1, v0}, Landroid/widget/TextView;->setVisibility(I)V

    iget-object p1, p0, Lnet/gogame/gopay/sdk/iab/k;->a:Lnet/gogame/gopay/sdk/iab/bv;

    iget-object p1, p1, Lnet/gogame/gopay/sdk/iab/bv;->a:Landroid/widget/ImageView;

    invoke-virtual {p1, v1}, Landroid/widget/ImageView;->setVisibility(I)V

    return-void

    :cond_0
    iget-object p1, p0, Lnet/gogame/gopay/sdk/iab/k;->a:Lnet/gogame/gopay/sdk/iab/bv;

    iget-object p1, p1, Lnet/gogame/gopay/sdk/iab/bv;->a:Landroid/widget/ImageView;

    invoke-virtual {p1, v0}, Landroid/widget/ImageView;->setVisibility(I)V

    iget-object p1, p0, Lnet/gogame/gopay/sdk/iab/k;->a:Lnet/gogame/gopay/sdk/iab/bv;

    iget-object p1, p1, Lnet/gogame/gopay/sdk/iab/bv;->b:Landroid/widget/TextView;

    invoke-virtual {p1, v1}, Landroid/widget/TextView;->setVisibility(I)V

    :cond_1
    return-void
.end method
