.class final Lnet/gogame/gopay/sdk/iab/br;
.super Ljava/lang/Object;


# instance fields
.field a:I

.field b:Ljava/lang/String;

.field c:Lorg/json/JSONObject;


# direct methods
.method public constructor <init>(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V
    .locals 3

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    new-instance v0, Lorg/json/JSONObject;

    invoke-direct {v0}, Lorg/json/JSONObject;-><init>()V

    iput-object v0, p0, Lnet/gogame/gopay/sdk/iab/br;->c:Lorg/json/JSONObject;

    iget-object v0, p0, Lnet/gogame/gopay/sdk/iab/br;->c:Lorg/json/JSONObject;

    const-string v1, "packageName"

    invoke-virtual {v0, v1, p3}, Lorg/json/JSONObject;->put(Ljava/lang/String;Ljava/lang/Object;)Lorg/json/JSONObject;

    iget-object p3, p0, Lnet/gogame/gopay/sdk/iab/br;->c:Lorg/json/JSONObject;

    const-string v0, "productId"

    invoke-virtual {p3, v0, p1}, Lorg/json/JSONObject;->put(Ljava/lang/String;Ljava/lang/Object;)Lorg/json/JSONObject;

    const-string p1, "/"

    invoke-virtual {p2, p1}, Ljava/lang/String;->split(Ljava/lang/String;)[Ljava/lang/String;

    move-result-object p1

    array-length p2, p1

    const/4 p3, 0x1

    const/4 v0, 0x5

    if-lt p2, v0, :cond_1

    iget-object p2, p0, Lnet/gogame/gopay/sdk/iab/br;->c:Lorg/json/JSONObject;

    const-string v1, "orderId"

    aget-object p3, p1, p3

    invoke-virtual {p2, v1, p3}, Lorg/json/JSONObject;->put(Ljava/lang/String;Ljava/lang/Object;)Lorg/json/JSONObject;

    iget-object p2, p0, Lnet/gogame/gopay/sdk/iab/br;->c:Lorg/json/JSONObject;

    const-string p3, "purchaseToken"

    const/4 v1, 0x2

    aget-object v2, p1, v1

    invoke-virtual {p2, p3, v2}, Lorg/json/JSONObject;->put(Ljava/lang/String;Ljava/lang/Object;)Lorg/json/JSONObject;

    iget-object p2, p0, Lnet/gogame/gopay/sdk/iab/br;->c:Lorg/json/JSONObject;

    const-string p3, "purchaseTime"

    const/4 v2, 0x3

    aget-object v2, p1, v2

    invoke-virtual {p2, p3, v2}, Lorg/json/JSONObject;->put(Ljava/lang/String;Ljava/lang/Object;)Lorg/json/JSONObject;

    array-length p2, p1

    const/4 p3, 0x6

    const/4 v2, 0x4

    if-ne p2, p3, :cond_0

    iget-object p2, p0, Lnet/gogame/gopay/sdk/iab/br;->c:Lorg/json/JSONObject;

    const-string p3, "developerPayload"

    aget-object v2, p1, v2

    invoke-virtual {p2, p3, v2}, Lorg/json/JSONObject;->put(Ljava/lang/String;Ljava/lang/Object;)Lorg/json/JSONObject;

    aget-object p2, p1, v0

    :goto_0
    invoke-static {p2}, Ljava/lang/Integer;->parseInt(Ljava/lang/String;)I

    move-result p2

    iput p2, p0, Lnet/gogame/gopay/sdk/iab/br;->a:I

    goto :goto_1

    :cond_0
    aget-object p2, p1, v2

    goto :goto_0

    :goto_1
    aget-object p1, p1, v1

    iput-object p1, p0, Lnet/gogame/gopay/sdk/iab/br;->b:Ljava/lang/String;

    goto :goto_2

    :cond_1
    iget-object p2, p0, Lnet/gogame/gopay/sdk/iab/br;->c:Lorg/json/JSONObject;

    const-string v0, "orderId"

    const/4 v1, 0x0

    aget-object v1, p1, v1

    invoke-virtual {p2, v0, v1}, Lorg/json/JSONObject;->put(Ljava/lang/String;Ljava/lang/Object;)Lorg/json/JSONObject;

    aget-object p1, p1, p3

    invoke-static {p1}, Ljava/lang/Integer;->parseInt(Ljava/lang/String;)I

    move-result p1

    iput p1, p0, Lnet/gogame/gopay/sdk/iab/br;->a:I

    :goto_2
    iget-object p1, p0, Lnet/gogame/gopay/sdk/iab/br;->c:Lorg/json/JSONObject;

    const-string p2, "purchaseState"

    iget p3, p0, Lnet/gogame/gopay/sdk/iab/br;->a:I

    invoke-virtual {p1, p2, p3}, Lorg/json/JSONObject;->put(Ljava/lang/String;I)Lorg/json/JSONObject;

    return-void
.end method

.method public constructor <init>(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;JLjava/lang/String;I)V
    .locals 2

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    new-instance v0, Lorg/json/JSONObject;

    invoke-direct {v0}, Lorg/json/JSONObject;-><init>()V

    iput-object v0, p0, Lnet/gogame/gopay/sdk/iab/br;->c:Lorg/json/JSONObject;

    iput p8, p0, Lnet/gogame/gopay/sdk/iab/br;->a:I

    iput-object p4, p0, Lnet/gogame/gopay/sdk/iab/br;->b:Ljava/lang/String;

    iget-object v0, p0, Lnet/gogame/gopay/sdk/iab/br;->c:Lorg/json/JSONObject;

    const-string v1, "productId"

    invoke-virtual {v0, v1, p1}, Lorg/json/JSONObject;->put(Ljava/lang/String;Ljava/lang/Object;)Lorg/json/JSONObject;

    iget-object p1, p0, Lnet/gogame/gopay/sdk/iab/br;->c:Lorg/json/JSONObject;

    const-string v0, "orderId"

    invoke-virtual {p1, v0, p3}, Lorg/json/JSONObject;->put(Ljava/lang/String;Ljava/lang/Object;)Lorg/json/JSONObject;

    iget-object p1, p0, Lnet/gogame/gopay/sdk/iab/br;->c:Lorg/json/JSONObject;

    const-string p3, "purchaseState"

    invoke-virtual {p1, p3, p8}, Lorg/json/JSONObject;->put(Ljava/lang/String;I)Lorg/json/JSONObject;

    iget-object p1, p0, Lnet/gogame/gopay/sdk/iab/br;->c:Lorg/json/JSONObject;

    const-string p3, "packageName"

    invoke-virtual {p1, p3, p2}, Lorg/json/JSONObject;->put(Ljava/lang/String;Ljava/lang/Object;)Lorg/json/JSONObject;

    iget-object p1, p0, Lnet/gogame/gopay/sdk/iab/br;->c:Lorg/json/JSONObject;

    const-string p2, "developerPayload"

    invoke-virtual {p1, p2, p7}, Lorg/json/JSONObject;->put(Ljava/lang/String;Ljava/lang/Object;)Lorg/json/JSONObject;

    iget-object p1, p0, Lnet/gogame/gopay/sdk/iab/br;->c:Lorg/json/JSONObject;

    const-string p2, "purchaseTime"

    invoke-virtual {p1, p2, p5, p6}, Lorg/json/JSONObject;->put(Ljava/lang/String;J)Lorg/json/JSONObject;

    iget-object p1, p0, Lnet/gogame/gopay/sdk/iab/br;->c:Lorg/json/JSONObject;

    const-string p2, "purchaseToken"

    invoke-virtual {p1, p2, p4}, Lorg/json/JSONObject;->put(Ljava/lang/String;Ljava/lang/Object;)Lorg/json/JSONObject;

    return-void
.end method


# virtual methods
.method public final toString()Ljava/lang/String;
    .locals 1

    iget-object v0, p0, Lnet/gogame/gopay/sdk/iab/br;->c:Lorg/json/JSONObject;

    invoke-virtual {v0}, Lorg/json/JSONObject;->toString()Ljava/lang/String;

    move-result-object v0

    return-object v0
.end method
