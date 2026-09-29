.class final Lnet/gogame/gopay/sdk/iab/be;
.super Ljava/lang/Object;

# interfaces
.implements Lnet/gogame/gopay/sdk/support/u;


# instance fields
.field final synthetic a:F

.field final synthetic b:Lnet/gogame/gopay/sdk/iab/bd;


# direct methods
.method constructor <init>(Lnet/gogame/gopay/sdk/iab/bd;F)V
    .locals 0

    iput-object p1, p0, Lnet/gogame/gopay/sdk/iab/be;->b:Lnet/gogame/gopay/sdk/iab/bd;

    iput p2, p0, Lnet/gogame/gopay/sdk/iab/be;->a:F

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public final a()V
    .locals 3

    iget-object v0, p0, Lnet/gogame/gopay/sdk/iab/be;->b:Lnet/gogame/gopay/sdk/iab/bd;

    iget-object v0, v0, Lnet/gogame/gopay/sdk/iab/bd;->a:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    invoke-static {v0}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->b(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;)Landroid/content/SharedPreferences;

    move-result-object v0

    invoke-interface {v0}, Landroid/content/SharedPreferences;->edit()Landroid/content/SharedPreferences$Editor;

    move-result-object v0

    const-string v1, "_version_"

    iget v2, p0, Lnet/gogame/gopay/sdk/iab/be;->a:F

    invoke-interface {v0, v1, v2}, Landroid/content/SharedPreferences$Editor;->putFloat(Ljava/lang/String;F)Landroid/content/SharedPreferences$Editor;

    move-result-object v0

    invoke-interface {v0}, Landroid/content/SharedPreferences$Editor;->apply()V

    invoke-static {}, Lnet/gogame/gopay/sdk/support/m;->b()V

    iget-object v0, p0, Lnet/gogame/gopay/sdk/iab/be;->b:Lnet/gogame/gopay/sdk/iab/bd;

    iget-object v0, v0, Lnet/gogame/gopay/sdk/iab/bd;->a:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    invoke-static {v0}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->c(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;)V

    return-void
.end method
