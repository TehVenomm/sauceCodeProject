.class final Lnet/gogame/gopay/sdk/v;
.super Ljava/lang/Object;

# interfaces
.implements Landroid/view/View$OnClickListener;


# instance fields
.field final synthetic a:Lorg/onepf/oms/appstore/googleUtils/SkuDetails;

.field final synthetic b:Lnet/gogame/gopay/sdk/u;


# direct methods
.method constructor <init>(Lnet/gogame/gopay/sdk/u;Lorg/onepf/oms/appstore/googleUtils/SkuDetails;)V
    .locals 0

    iput-object p1, p0, Lnet/gogame/gopay/sdk/v;->b:Lnet/gogame/gopay/sdk/u;

    iput-object p2, p0, Lnet/gogame/gopay/sdk/v;->a:Lorg/onepf/oms/appstore/googleUtils/SkuDetails;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public final onClick(Landroid/view/View;)V
    .locals 4

    new-instance p1, Landroid/content/Intent;

    iget-object v0, p0, Lnet/gogame/gopay/sdk/v;->b:Lnet/gogame/gopay/sdk/u;

    invoke-static {v0}, Lnet/gogame/gopay/sdk/u;->a(Lnet/gogame/gopay/sdk/u;)Landroid/app/Activity;

    move-result-object v0

    const-class v1, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    invoke-direct {p1, v0, v1}, Landroid/content/Intent;-><init>(Landroid/content/Context;Ljava/lang/Class;)V

    const-string v0, "gid"

    iget-object v1, p0, Lnet/gogame/gopay/sdk/v;->b:Lnet/gogame/gopay/sdk/u;

    invoke-static {v1}, Lnet/gogame/gopay/sdk/u;->b(Lnet/gogame/gopay/sdk/u;)Lnet/gogame/gopay/sdk/m;

    move-result-object v1

    iget-object v1, v1, Lnet/gogame/gopay/sdk/m;->a:Ljava/lang/String;

    invoke-virtual {p1, v0, v1}, Landroid/content/Intent;->putExtra(Ljava/lang/String;Ljava/lang/String;)Landroid/content/Intent;

    const-string v0, "guid"

    iget-object v1, p0, Lnet/gogame/gopay/sdk/v;->b:Lnet/gogame/gopay/sdk/u;

    invoke-static {v1}, Lnet/gogame/gopay/sdk/u;->b(Lnet/gogame/gopay/sdk/u;)Lnet/gogame/gopay/sdk/m;

    move-result-object v1

    iget-object v1, v1, Lnet/gogame/gopay/sdk/m;->b:Ljava/lang/String;

    invoke-virtual {p1, v0, v1}, Landroid/content/Intent;->putExtra(Ljava/lang/String;Ljava/lang/String;)Landroid/content/Intent;

    const-string v0, "sku"

    iget-object v1, p0, Lnet/gogame/gopay/sdk/v;->a:Lorg/onepf/oms/appstore/googleUtils/SkuDetails;

    invoke-virtual {v1}, Lorg/onepf/oms/appstore/googleUtils/SkuDetails;->getSku()Ljava/lang/String;

    move-result-object v1

    invoke-virtual {p1, v0, v1}, Landroid/content/Intent;->putExtra(Ljava/lang/String;Ljava/lang/String;)Landroid/content/Intent;

    const-string v0, "itemType"

    iget-object v1, p0, Lnet/gogame/gopay/sdk/v;->a:Lorg/onepf/oms/appstore/googleUtils/SkuDetails;

    invoke-virtual {v1}, Lorg/onepf/oms/appstore/googleUtils/SkuDetails;->getItemType()Ljava/lang/String;

    move-result-object v1

    invoke-virtual {p1, v0, v1}, Landroid/content/Intent;->putExtra(Ljava/lang/String;Ljava/lang/String;)Landroid/content/Intent;

    const-string v0, "referenceId"

    invoke-static {}, Ljava/util/UUID;->randomUUID()Ljava/util/UUID;

    move-result-object v1

    invoke-virtual {v1}, Ljava/util/UUID;->toString()Ljava/lang/String;

    move-result-object v1

    const-string v2, "-"

    const-string v3, ""

    invoke-virtual {v1, v2, v3}, Ljava/lang/String;->replaceAll(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v1

    invoke-virtual {p1, v0, v1}, Landroid/content/Intent;->putExtra(Ljava/lang/String;Ljava/lang/String;)Landroid/content/Intent;

    :try_start_0
    iget-object v0, p0, Lnet/gogame/gopay/sdk/v;->b:Lnet/gogame/gopay/sdk/u;

    invoke-static {v0}, Lnet/gogame/gopay/sdk/u;->a(Lnet/gogame/gopay/sdk/u;)Landroid/app/Activity;

    move-result-object v0

    invoke-virtual {v0, p1}, Landroid/app/Activity;->startActivity(Landroid/content/Intent;)V
    :try_end_0
    .catch Landroid/content/ActivityNotFoundException; {:try_start_0 .. :try_end_0} :catch_0

    :catch_0
    return-void
.end method
