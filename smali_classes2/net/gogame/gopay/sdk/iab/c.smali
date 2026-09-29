.class final Lnet/gogame/gopay/sdk/iab/c;
.super Ljava/lang/Object;

# interfaces
.implements Lnet/gogame/gopay/sdk/support/q;


# instance fields
.field a:Lnet/gogame/gopay/sdk/iab/bv;

.field final synthetic b:Lnet/gogame/gopay/sdk/iab/bv;

.field final synthetic c:I

.field final synthetic d:Lnet/gogame/gopay/sdk/iab/b;


# direct methods
.method constructor <init>(Lnet/gogame/gopay/sdk/iab/b;Lnet/gogame/gopay/sdk/iab/bv;I)V
    .locals 0

    iput-object p1, p0, Lnet/gogame/gopay/sdk/iab/c;->d:Lnet/gogame/gopay/sdk/iab/b;

    iput-object p2, p0, Lnet/gogame/gopay/sdk/iab/c;->b:Lnet/gogame/gopay/sdk/iab/bv;

    iput p3, p0, Lnet/gogame/gopay/sdk/iab/c;->c:I

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    iget-object p1, p0, Lnet/gogame/gopay/sdk/iab/c;->b:Lnet/gogame/gopay/sdk/iab/bv;

    iput-object p1, p0, Lnet/gogame/gopay/sdk/iab/c;->a:Lnet/gogame/gopay/sdk/iab/bv;

    return-void
.end method


# virtual methods
.method public final a(Landroid/graphics/Bitmap;)V
    .locals 4

    iget-object v0, p0, Lnet/gogame/gopay/sdk/iab/c;->a:Lnet/gogame/gopay/sdk/iab/bv;

    iget v0, v0, Lnet/gogame/gopay/sdk/iab/bv;->c:I

    iget v1, p0, Lnet/gogame/gopay/sdk/iab/c;->c:I

    if-ne v0, v1, :cond_1

    if-eqz p1, :cond_0

    iget-object v0, p0, Lnet/gogame/gopay/sdk/iab/c;->d:Lnet/gogame/gopay/sdk/iab/b;

    invoke-virtual {p1}, Landroid/graphics/Bitmap;->getWidth()I

    move-result v1

    invoke-virtual {v0, v1}, Lnet/gogame/gopay/sdk/iab/b;->a(I)I

    move-result v0

    iget-object v1, p0, Lnet/gogame/gopay/sdk/iab/c;->d:Lnet/gogame/gopay/sdk/iab/b;

    invoke-virtual {p1}, Landroid/graphics/Bitmap;->getHeight()I

    move-result v2

    invoke-virtual {v1, v2}, Lnet/gogame/gopay/sdk/iab/b;->a(I)I

    move-result v1

    new-instance v2, Landroid/graphics/drawable/BitmapDrawable;

    iget-object v3, p0, Lnet/gogame/gopay/sdk/iab/c;->d:Lnet/gogame/gopay/sdk/iab/b;

    iget-object v3, v3, Lnet/gogame/gopay/sdk/a;->a:Landroid/content/Context;

    invoke-virtual {v3}, Landroid/content/Context;->getResources()Landroid/content/res/Resources;

    move-result-object v3

    invoke-direct {v2, v3, p1}, Landroid/graphics/drawable/BitmapDrawable;-><init>(Landroid/content/res/Resources;Landroid/graphics/Bitmap;)V

    const/4 p1, 0x0

    invoke-virtual {v2, p1, p1, v0, v1}, Landroid/graphics/drawable/Drawable;->setBounds(IIII)V

    iget-object v3, p0, Lnet/gogame/gopay/sdk/iab/c;->a:Lnet/gogame/gopay/sdk/iab/bv;

    iget-object v3, v3, Lnet/gogame/gopay/sdk/iab/bv;->a:Landroid/widget/ImageView;

    invoke-virtual {v3, v2}, Landroid/widget/ImageView;->setImageDrawable(Landroid/graphics/drawable/Drawable;)V

    iget-object v2, p0, Lnet/gogame/gopay/sdk/iab/c;->a:Lnet/gogame/gopay/sdk/iab/bv;

    iget-object v2, v2, Lnet/gogame/gopay/sdk/iab/bv;->a:Landroid/widget/ImageView;

    invoke-virtual {v2, v0}, Landroid/widget/ImageView;->setMinimumWidth(I)V

    iget-object v0, p0, Lnet/gogame/gopay/sdk/iab/c;->a:Lnet/gogame/gopay/sdk/iab/bv;

    iget-object v0, v0, Lnet/gogame/gopay/sdk/iab/bv;->a:Landroid/widget/ImageView;

    invoke-virtual {v0, v1}, Landroid/widget/ImageView;->setMinimumHeight(I)V

    iget-object v0, p0, Lnet/gogame/gopay/sdk/iab/c;->a:Lnet/gogame/gopay/sdk/iab/bv;

    iget-object v0, v0, Lnet/gogame/gopay/sdk/iab/bv;->a:Landroid/widget/ImageView;

    invoke-virtual {v0, p1}, Landroid/widget/ImageView;->setVisibility(I)V

    return-void

    :cond_0
    iget-object p1, p0, Lnet/gogame/gopay/sdk/iab/c;->a:Lnet/gogame/gopay/sdk/iab/bv;

    iget-object p1, p1, Lnet/gogame/gopay/sdk/iab/bv;->a:Landroid/widget/ImageView;

    const/4 v0, 0x4

    invoke-virtual {p1, v0}, Landroid/widget/ImageView;->setVisibility(I)V

    :cond_1
    return-void
.end method
