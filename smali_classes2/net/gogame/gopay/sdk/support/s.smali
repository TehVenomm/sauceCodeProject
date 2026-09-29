.class public final Lnet/gogame/gopay/sdk/support/s;
.super Ljava/lang/Object;


# static fields
.field private static final a:Ljava/util/Map;


# direct methods
.method static constructor <clinit>()V
    .locals 2

    new-instance v0, Ljava/util/HashMap;

    invoke-direct {v0}, Ljava/util/HashMap;-><init>()V

    sput-object v0, Lnet/gogame/gopay/sdk/support/s;->a:Ljava/util/Map;

    const-string v0, "store_title"

    const-string v1, "Store"

    invoke-static {v0, v1}, Lnet/gogame/gopay/sdk/support/s;->a(Ljava/lang/String;Ljava/lang/String;)V

    const-string v0, "select_payment_header"

    const-string v1, "Please select the payment method that you prefer"

    invoke-static {v0, v1}, Lnet/gogame/gopay/sdk/support/s;->a(Ljava/lang/String;Ljava/lang/String;)V

    const-string v0, "select_payment_subheader"

    const-string v1, "We only show the payment methods that are available in your country."

    invoke-static {v0, v1}, Lnet/gogame/gopay/sdk/support/s;->a(Ljava/lang/String;Ljava/lang/String;)V

    const-string v0, "back_button_caption"

    const-string v1, "Back"

    invoke-static {v0, v1}, Lnet/gogame/gopay/sdk/support/s;->a(Ljava/lang/String;Ljava/lang/String;)V

    return-void
.end method

.method public static a(Ljava/lang/String;)Ljava/lang/String;
    .locals 3

    invoke-static {}, Ljava/util/Locale;->getDefault()Ljava/util/Locale;

    move-result-object v0

    invoke-virtual {v0}, Ljava/util/Locale;->toString()Ljava/lang/String;

    move-result-object v0

    :goto_0
    invoke-static {v0, p0}, Lnet/gogame/gopay/sdk/support/s;->b(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v1

    if-eqz v1, :cond_0

    return-object v1

    :cond_0
    const/16 v1, 0x5f

    invoke-virtual {v0, v1}, Ljava/lang/String;->lastIndexOf(I)I

    move-result v1

    if-ltz v1, :cond_1

    const/4 v2, 0x0

    invoke-virtual {v0, v2, v1}, Ljava/lang/String;->substring(II)Ljava/lang/String;

    move-result-object v0

    goto :goto_0

    :cond_1
    const/4 v0, 0x0

    invoke-static {v0, p0}, Lnet/gogame/gopay/sdk/support/s;->b(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object p0

    return-object p0
.end method

.method private static a(Ljava/lang/String;Ljava/lang/String;)V
    .locals 2

    sget-object v0, Lnet/gogame/gopay/sdk/support/s;->a:Ljava/util/Map;

    invoke-interface {v0, p0}, Ljava/util/Map;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Ljava/util/Map;

    if-nez v0, :cond_0

    new-instance v0, Ljava/util/HashMap;

    invoke-direct {v0}, Ljava/util/HashMap;-><init>()V

    sget-object v1, Lnet/gogame/gopay/sdk/support/s;->a:Ljava/util/Map;

    invoke-interface {v1, p0, v0}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    :cond_0
    const/4 p0, 0x0

    invoke-interface {v0, p0, p1}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    return-void
.end method

.method private static b(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;
    .locals 1

    sget-object v0, Lnet/gogame/gopay/sdk/support/s;->a:Ljava/util/Map;

    invoke-interface {v0, p1}, Ljava/util/Map;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object p1

    check-cast p1, Ljava/util/Map;

    if-nez p1, :cond_0

    const/4 p0, 0x0

    return-object p0

    :cond_0
    invoke-interface {p1, p0}, Ljava/util/Map;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object p0

    check-cast p0, Ljava/lang/String;

    return-object p0
.end method
