.class final Lnet/gogame/gopay/sdk/q;
.super Ljava/lang/Object;

# interfaces
.implements Ljava/lang/Runnable;


# instance fields
.field final synthetic a:Lnet/gogame/gopay/sdk/h;

.field final synthetic b:Lnet/gogame/gopay/sdk/StoreActivity;


# direct methods
.method constructor <init>(Lnet/gogame/gopay/sdk/StoreActivity;Lnet/gogame/gopay/sdk/h;)V
    .locals 0

    iput-object p1, p0, Lnet/gogame/gopay/sdk/q;->b:Lnet/gogame/gopay/sdk/StoreActivity;

    iput-object p2, p0, Lnet/gogame/gopay/sdk/q;->a:Lnet/gogame/gopay/sdk/h;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public final run()V
    .locals 3

    iget-object v0, p0, Lnet/gogame/gopay/sdk/q;->b:Lnet/gogame/gopay/sdk/StoreActivity;

    invoke-static {v0}, Lnet/gogame/gopay/sdk/StoreActivity;->a(Lnet/gogame/gopay/sdk/StoreActivity;)Lnet/gogame/gopay/sdk/d;

    move-result-object v0

    iget-object v1, p0, Lnet/gogame/gopay/sdk/q;->a:Lnet/gogame/gopay/sdk/h;

    iget-object v1, v1, Lnet/gogame/gopay/sdk/h;->d:Ljava/util/Map;

    const-string v2, "country"

    invoke-interface {v1, v2}, Ljava/util/Map;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Ljava/lang/String;

    iget-object v2, p0, Lnet/gogame/gopay/sdk/q;->a:Lnet/gogame/gopay/sdk/h;

    iget-object v2, v2, Lnet/gogame/gopay/sdk/h;->c:Ljava/util/List;

    invoke-virtual {v0, v1, v2}, Lnet/gogame/gopay/sdk/d;->a(Ljava/lang/String;Ljava/util/List;)V

    iget-object v0, p0, Lnet/gogame/gopay/sdk/q;->b:Lnet/gogame/gopay/sdk/StoreActivity;

    invoke-static {v0}, Lnet/gogame/gopay/sdk/StoreActivity;->c(Lnet/gogame/gopay/sdk/StoreActivity;)Lnet/gogame/gopay/sdk/w;

    move-result-object v0

    iget-object v1, p0, Lnet/gogame/gopay/sdk/q;->a:Lnet/gogame/gopay/sdk/h;

    invoke-virtual {v0, v1}, Lnet/gogame/gopay/sdk/w;->setData(Lnet/gogame/gopay/sdk/h;)V

    return-void
.end method
