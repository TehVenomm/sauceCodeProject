.class final Lnet/gogame/gopay/sdk/iab/aw;
.super Ljava/lang/Object;

# interfaces
.implements Ljava/lang/Runnable;


# instance fields
.field final synthetic a:Ljava/lang/String;

.field final synthetic b:Lnet/gogame/gopay/sdk/iab/at;


# direct methods
.method constructor <init>(Lnet/gogame/gopay/sdk/iab/at;Ljava/lang/String;)V
    .locals 0

    iput-object p1, p0, Lnet/gogame/gopay/sdk/iab/aw;->b:Lnet/gogame/gopay/sdk/iab/at;

    iput-object p2, p0, Lnet/gogame/gopay/sdk/iab/aw;->a:Ljava/lang/String;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public final run()V
    .locals 4

    iget-object v0, p0, Lnet/gogame/gopay/sdk/iab/aw;->b:Lnet/gogame/gopay/sdk/iab/at;

    iget-object v0, v0, Lnet/gogame/gopay/sdk/iab/at;->b:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    invoke-static {v0}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->C(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;)Landroid/webkit/WebView;

    move-result-object v0

    iget-object v1, p0, Lnet/gogame/gopay/sdk/iab/aw;->a:Ljava/lang/String;

    const-string v2, "text/html; charset=UTF-8"

    const/4 v3, 0x0

    invoke-virtual {v0, v1, v2, v3}, Landroid/webkit/WebView;->loadData(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V

    return-void
.end method
