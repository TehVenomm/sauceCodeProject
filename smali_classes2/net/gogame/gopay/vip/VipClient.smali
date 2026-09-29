.class public Lnet/gogame/gopay/vip/VipClient;
.super Ljava/lang/Object;
.source "SourceFile"

# interfaces
.implements Lnet/gogame/gopay/vip/IVipClient;


# static fields
.field public static final INSTANCE:Lnet/gogame/gopay/vip/VipClient;


# instance fields
.field private a:Ljava/lang/String;

.field private b:Ljava/lang/String;

.field private c:Ljava/lang/String;

.field private d:Ljava/lang/String;

.field private e:Ljava/lang/String;

.field private f:Lnet/gogame/gopay/vip/VipStatus;

.field private final g:Ljava/util/Set;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/Set<",
            "Lnet/gogame/gopay/vip/IVipClient$Listener;",
            ">;"
        }
    .end annotation
.end field

.field private final h:Ljava/util/Map;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            ">;"
        }
    .end annotation
.end field

.field private final i:Ljava/util/Map;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            ">;"
        }
    .end annotation
.end field

.field private j:Lnet/gogame/gopay/vip/TaskQueue;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Lnet/gogame/gopay/vip/TaskQueue<",
            "Lnet/gogame/gopay/vip/BaseEvent;",
            ">;"
        }
    .end annotation
.end field

.field private final k:Lnet/gogame/gopay/vip/TaskQueue$Listener;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Lnet/gogame/gopay/vip/TaskQueue$Listener<",
            "Lnet/gogame/gopay/vip/BaseEvent;",
            ">;"
        }
    .end annotation
.end field


# direct methods
.method static constructor <clinit>()V
    .locals 1

    .line 25
    new-instance v0, Lnet/gogame/gopay/vip/VipClient;

    invoke-direct {v0}, Lnet/gogame/gopay/vip/VipClient;-><init>()V

    sput-object v0, Lnet/gogame/gopay/vip/VipClient;->INSTANCE:Lnet/gogame/gopay/vip/VipClient;

    return-void
.end method

.method private constructor <init>()V
    .locals 2

    .line 66
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    const/4 v0, 0x0

    .line 27
    iput-object v0, p0, Lnet/gogame/gopay/vip/VipClient;->a:Ljava/lang/String;

    .line 28
    iput-object v0, p0, Lnet/gogame/gopay/vip/VipClient;->b:Ljava/lang/String;

    .line 30
    iput-object v0, p0, Lnet/gogame/gopay/vip/VipClient;->c:Ljava/lang/String;

    .line 31
    iput-object v0, p0, Lnet/gogame/gopay/vip/VipClient;->d:Ljava/lang/String;

    .line 32
    iput-object v0, p0, Lnet/gogame/gopay/vip/VipClient;->e:Ljava/lang/String;

    .line 34
    iput-object v0, p0, Lnet/gogame/gopay/vip/VipClient;->f:Lnet/gogame/gopay/vip/VipStatus;

    .line 35
    new-instance v1, Ljava/util/HashSet;

    invoke-direct {v1}, Ljava/util/HashSet;-><init>()V

    iput-object v1, p0, Lnet/gogame/gopay/vip/VipClient;->g:Ljava/util/Set;

    .line 37
    new-instance v1, Ljava/util/HashMap;

    invoke-direct {v1}, Ljava/util/HashMap;-><init>()V

    iput-object v1, p0, Lnet/gogame/gopay/vip/VipClient;->h:Ljava/util/Map;

    .line 38
    new-instance v1, Ljava/util/HashMap;

    invoke-direct {v1}, Ljava/util/HashMap;-><init>()V

    iput-object v1, p0, Lnet/gogame/gopay/vip/VipClient;->i:Ljava/util/Map;

    .line 40
    iput-object v0, p0, Lnet/gogame/gopay/vip/VipClient;->j:Lnet/gogame/gopay/vip/TaskQueue;

    .line 41
    new-instance v0, Lnet/gogame/gopay/vip/VipClient$1;

    invoke-direct {v0, p0}, Lnet/gogame/gopay/vip/VipClient$1;-><init>(Lnet/gogame/gopay/vip/VipClient;)V

    iput-object v0, p0, Lnet/gogame/gopay/vip/VipClient;->k:Lnet/gogame/gopay/vip/TaskQueue$Listener;

    return-void
.end method

.method private a()Ljava/util/Map;
    .locals 5
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()",
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            ">;"
        }
    .end annotation

    .line 228
    new-instance v0, Ljava/util/LinkedHashMap;

    invoke-direct {v0}, Ljava/util/LinkedHashMap;-><init>()V

    .line 229
    iget-object v1, p0, Lnet/gogame/gopay/vip/VipClient;->h:Ljava/util/Map;

    if-eqz v1, :cond_1

    .line 230
    iget-object v1, p0, Lnet/gogame/gopay/vip/VipClient;->h:Ljava/util/Map;

    invoke-interface {v1}, Ljava/util/Map;->entrySet()Ljava/util/Set;

    move-result-object v1

    invoke-interface {v1}, Ljava/util/Set;->iterator()Ljava/util/Iterator;

    move-result-object v1

    :cond_0
    :goto_0
    invoke-interface {v1}, Ljava/util/Iterator;->hasNext()Z

    move-result v2

    if-eqz v2, :cond_1

    invoke-interface {v1}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Ljava/util/Map$Entry;

    .line 231
    invoke-interface {v2}, Ljava/util/Map$Entry;->getKey()Ljava/lang/Object;

    move-result-object v3

    check-cast v3, Ljava/lang/String;

    .line 232
    invoke-interface {v2}, Ljava/util/Map$Entry;->getValue()Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Ljava/lang/String;

    if-eqz v3, :cond_0

    if-eqz v2, :cond_0

    .line 233
    invoke-interface {v0, v3}, Ljava/util/Map;->containsKey(Ljava/lang/Object;)Z

    move-result v4

    if-nez v4, :cond_0

    .line 234
    invoke-interface {v0, v3, v2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    goto :goto_0

    .line 238
    :cond_1
    iget-object v1, p0, Lnet/gogame/gopay/vip/VipClient;->a:Ljava/lang/String;

    if-eqz v1, :cond_2

    const-string v1, "appId"

    .line 239
    iget-object v2, p0, Lnet/gogame/gopay/vip/VipClient;->a:Ljava/lang/String;

    invoke-interface {v0, v1, v2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 241
    :cond_2
    iget-object v1, p0, Lnet/gogame/gopay/vip/VipClient;->c:Ljava/lang/String;

    if-eqz v1, :cond_3

    const-string v1, "bundle_id"

    .line 242
    iget-object v2, p0, Lnet/gogame/gopay/vip/VipClient;->c:Ljava/lang/String;

    invoke-interface {v0, v1, v2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 244
    :cond_3
    iget-object v1, p0, Lnet/gogame/gopay/vip/VipClient;->d:Ljava/lang/String;

    if-eqz v1, :cond_4

    const-string v1, "app_version"

    .line 245
    iget-object v2, p0, Lnet/gogame/gopay/vip/VipClient;->d:Ljava/lang/String;

    invoke-interface {v0, v1, v2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    :cond_4
    const-string v1, "platform"

    const-string v2, "android"

    .line 247
    invoke-interface {v0, v1, v2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string v1, "os_version"

    .line 248
    sget-object v2, Landroid/os/Build$VERSION;->RELEASE:Ljava/lang/String;

    invoke-interface {v0, v1, v2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string v1, "sdk"

    const-string v2, "gopay-vip-sdk-android"

    .line 249
    invoke-interface {v0, v1, v2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string v1, "sdk_version"

    const-string v2, "1.2.6"

    .line 250
    invoke-interface {v0, v1, v2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 251
    iget-object v1, p0, Lnet/gogame/gopay/vip/VipClient;->e:Ljava/lang/String;

    if-eqz v1, :cond_5

    const-string v1, "device_id"

    .line 252
    iget-object v2, p0, Lnet/gogame/gopay/vip/VipClient;->e:Ljava/lang/String;

    invoke-interface {v0, v1, v2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    :cond_5
    return-object v0
.end method

.method private a(Lnet/gogame/gopay/vip/PurchaseEvent;)Lnet/gogame/gopay/vip/BaseBillingResponse;
    .locals 5
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Lorg/json/JSONException;,
            Lnet/gogame/gopay/vip/UnauthorizedException;,
            Lnet/gogame/gopay/vip/HttpException;,
            Ljava/io/IOException;
        }
    .end annotation

    const/4 v0, 0x0

    if-nez p1, :cond_0

    return-object v0

    .line 292
    :cond_0
    invoke-direct {p0}, Lnet/gogame/gopay/vip/VipClient;->c()Ljava/util/Map;

    move-result-object v1

    .line 293
    invoke-virtual {p1}, Lnet/gogame/gopay/vip/PurchaseEvent;->getReferenceId()Ljava/lang/String;

    move-result-object v2

    if-eqz v2, :cond_1

    const-string v2, "reference_id"

    .line 294
    invoke-virtual {p1}, Lnet/gogame/gopay/vip/PurchaseEvent;->getReferenceId()Ljava/lang/String;

    move-result-object v3

    invoke-interface {v1, v2, v3}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 296
    :cond_1
    invoke-virtual {p1}, Lnet/gogame/gopay/vip/PurchaseEvent;->getGuid()Ljava/lang/String;

    move-result-object v2

    if-eqz v2, :cond_2

    const-string v2, "guid"

    .line 297
    invoke-virtual {p1}, Lnet/gogame/gopay/vip/PurchaseEvent;->getGuid()Ljava/lang/String;

    move-result-object v3

    invoke-interface {v1, v2, v3}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 299
    :cond_2
    invoke-virtual {p1}, Lnet/gogame/gopay/vip/PurchaseEvent;->getProductId()Ljava/lang/String;

    move-result-object v2

    if-eqz v2, :cond_3

    const-string v2, "sku_id"

    .line 300
    invoke-virtual {p1}, Lnet/gogame/gopay/vip/PurchaseEvent;->getProductId()Ljava/lang/String;

    move-result-object v3

    invoke-interface {v1, v2, v3}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 302
    :cond_3
    invoke-virtual {p1}, Lnet/gogame/gopay/vip/PurchaseEvent;->getCurrencyCode()Ljava/lang/String;

    move-result-object v2

    if-eqz v2, :cond_4

    const-string v2, "currency_code"

    .line 303
    invoke-virtual {p1}, Lnet/gogame/gopay/vip/PurchaseEvent;->getCurrencyCode()Ljava/lang/String;

    move-result-object v3

    invoke-interface {v1, v2, v3}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    :cond_4
    const-string v2, "price"

    .line 305
    invoke-virtual {p1}, Lnet/gogame/gopay/vip/PurchaseEvent;->getPrice()D

    move-result-wide v3

    invoke-static {v3, v4}, Ljava/lang/String;->valueOf(D)Ljava/lang/String;

    move-result-object v3

    invoke-interface {v1, v2, v3}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string v2, "timestamp"

    .line 306
    invoke-virtual {p1}, Lnet/gogame/gopay/vip/PurchaseEvent;->getTimestamp()J

    move-result-wide v3

    invoke-static {v3, v4}, Ljava/lang/String;->valueOf(J)Ljava/lang/String;

    move-result-object v3

    invoke-interface {v1, v2, v3}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 307
    invoke-virtual {p1}, Lnet/gogame/gopay/vip/PurchaseEvent;->getOrderId()Ljava/lang/String;

    move-result-object v2

    if-eqz v2, :cond_5

    const-string v2, "platform_order_id"

    .line 308
    invoke-virtual {p1}, Lnet/gogame/gopay/vip/PurchaseEvent;->getOrderId()Ljava/lang/String;

    move-result-object v3

    invoke-interface {v1, v2, v3}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 310
    :cond_5
    invoke-virtual {p1}, Lnet/gogame/gopay/vip/PurchaseEvent;->getVerificationStatus()Lnet/gogame/gopay/vip/PurchaseEvent$VerificationStatus;

    move-result-object v2

    if-eqz v2, :cond_6

    const-string v2, "verified"

    .line 311
    invoke-virtual {p1}, Lnet/gogame/gopay/vip/PurchaseEvent;->getVerificationStatus()Lnet/gogame/gopay/vip/PurchaseEvent$VerificationStatus;

    move-result-object v3

    invoke-virtual {v3}, Lnet/gogame/gopay/vip/PurchaseEvent$VerificationStatus;->getValue()I

    move-result v3

    invoke-static {v3}, Ljava/lang/String;->valueOf(I)Ljava/lang/String;

    move-result-object v3

    invoke-interface {v1, v2, v3}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    goto :goto_0

    :cond_6
    const-string v2, "verified"

    .line 313
    sget-object v3, Lnet/gogame/gopay/vip/PurchaseEvent$VerificationStatus;->NOT_VERIFIED:Lnet/gogame/gopay/vip/PurchaseEvent$VerificationStatus;

    .line 314
    invoke-virtual {v3}, Lnet/gogame/gopay/vip/PurchaseEvent$VerificationStatus;->getValue()I

    move-result v3

    .line 313
    invoke-static {v3}, Ljava/lang/String;->valueOf(I)Ljava/lang/String;

    move-result-object v3

    invoke-interface {v1, v2, v3}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    :goto_0
    const-string v2, "sandbox"

    .line 316
    invoke-virtual {p1}, Lnet/gogame/gopay/vip/PurchaseEvent;->isSandbox()Z

    move-result p1

    if-eqz p1, :cond_7

    const-string p1, "1"

    goto :goto_1

    :cond_7
    const-string p1, "0"

    :goto_1
    invoke-interface {v1, v2, p1}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string p1, "https://gp-vip.gogame.net/billing/v3/log_client_transaction/"

    .line 318
    iget-object v2, p0, Lnet/gogame/gopay/vip/VipClient;->b:Ljava/lang/String;

    iget-object v3, p0, Lnet/gogame/gopay/vip/VipClient;->i:Ljava/util/Map;

    invoke-static {p1, v1, v2, v3}, Lnet/gogame/gopay/vip/a;->a(Ljava/lang/String;Ljava/util/Map;Ljava/lang/String;Ljava/util/Map;)Lorg/json/JSONObject;

    move-result-object p1

    .line 320
    new-instance v1, Lnet/gogame/gopay/vip/BaseBillingResponse;

    invoke-direct {v1}, Lnet/gogame/gopay/vip/BaseBillingResponse;-><init>()V

    const-string v2, "status"

    const/4 v3, 0x0

    .line 321
    invoke-virtual {p1, v2, v3}, Lorg/json/JSONObject;->optBoolean(Ljava/lang/String;Z)Z

    move-result v2

    invoke-virtual {v1, v2}, Lnet/gogame/gopay/vip/BaseBillingResponse;->setStatus(Z)V

    const-string v2, "statusCode"

    .line 322
    invoke-virtual {p1, v2, v3}, Lorg/json/JSONObject;->optInt(Ljava/lang/String;I)I

    move-result v2

    invoke-virtual {v1, v2}, Lnet/gogame/gopay/vip/BaseBillingResponse;->setStatusCode(I)V

    const-string v2, "statusMsg"

    .line 323
    invoke-virtual {p1, v2, v0}, Lorg/json/JSONObject;->optString(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object p1

    invoke-virtual {v1, p1}, Lnet/gogame/gopay/vip/BaseBillingResponse;->setStatusMessage(Ljava/lang/String;)V

    return-object v1
.end method

.method static synthetic a(Lnet/gogame/gopay/vip/VipClient;Lnet/gogame/gopay/vip/PurchaseEvent;)Lnet/gogame/gopay/vip/BaseBillingResponse;
    .locals 0
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Lorg/json/JSONException;,
            Lnet/gogame/gopay/vip/UnauthorizedException;,
            Lnet/gogame/gopay/vip/HttpException;,
            Ljava/io/IOException;
        }
    .end annotation

    .line 22
    invoke-direct {p0, p1}, Lnet/gogame/gopay/vip/VipClient;->a(Lnet/gogame/gopay/vip/PurchaseEvent;)Lnet/gogame/gopay/vip/BaseBillingResponse;

    move-result-object p0

    return-object p0
.end method

.method private a(Ljava/lang/String;)Lnet/gogame/gopay/vip/VipStatus;
    .locals 5
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Lorg/json/JSONException;,
            Lnet/gogame/gopay/vip/UnauthorizedException;,
            Lnet/gogame/gopay/vip/HttpException;,
            Ljava/io/IOException;
        }
    .end annotation

    .line 275
    invoke-direct {p0}, Lnet/gogame/gopay/vip/VipClient;->b()Ljava/util/Map;

    move-result-object v0

    if-eqz p1, :cond_0

    const-string v1, "guid"

    .line 277
    invoke-interface {v0, v1, p1}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    :cond_0
    const-string v1, "https://gp-vip.gogame.net/vip/v1/get_vip_status/"

    .line 279
    iget-object v2, p0, Lnet/gogame/gopay/vip/VipClient;->b:Ljava/lang/String;

    iget-object v3, p0, Lnet/gogame/gopay/vip/VipClient;->i:Ljava/util/Map;

    invoke-static {v1, v0, v2, v3}, Lnet/gogame/gopay/vip/a;->a(Ljava/lang/String;Ljava/util/Map;Ljava/lang/String;Ljava/util/Map;)Lorg/json/JSONObject;

    move-result-object v0

    const-string v1, "vip_status"

    const/4 v2, 0x0

    .line 281
    invoke-virtual {v0, v1, v2}, Lorg/json/JSONObject;->optBoolean(Ljava/lang/String;Z)Z

    move-result v1

    const-string v3, "suspended"

    .line 282
    invoke-virtual {v0, v3, v2}, Lorg/json/JSONObject;->optBoolean(Ljava/lang/String;Z)Z

    move-result v2

    const-string v3, "suspension_message"

    const/4 v4, 0x0

    .line 283
    invoke-virtual {v0, v3, v4}, Lorg/json/JSONObject;->optString(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v0

    .line 284
    new-instance v3, Lnet/gogame/gopay/vip/VipStatus;

    invoke-direct {v3, p1, v1, v2, v0}, Lnet/gogame/gopay/vip/VipStatus;-><init>(Ljava/lang/String;ZZLjava/lang/String;)V

    return-object v3
.end method

.method static synthetic a(Lnet/gogame/gopay/vip/VipClient;Ljava/lang/String;)Lnet/gogame/gopay/vip/VipStatus;
    .locals 0
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Lorg/json/JSONException;,
            Lnet/gogame/gopay/vip/UnauthorizedException;,
            Lnet/gogame/gopay/vip/HttpException;,
            Ljava/io/IOException;
        }
    .end annotation

    .line 22
    invoke-direct {p0, p1}, Lnet/gogame/gopay/vip/VipClient;->a(Ljava/lang/String;)Lnet/gogame/gopay/vip/VipStatus;

    move-result-object p0

    return-object p0
.end method

.method static synthetic a(Lnet/gogame/gopay/vip/VipClient;Lnet/gogame/gopay/vip/VipStatus;)V
    .locals 0

    .line 22
    invoke-direct {p0, p1}, Lnet/gogame/gopay/vip/VipClient;->a(Lnet/gogame/gopay/vip/VipStatus;)V

    return-void
.end method

.method private a(Lnet/gogame/gopay/vip/VipStatus;)V
    .locals 4

    .line 144
    iput-object p1, p0, Lnet/gogame/gopay/vip/VipClient;->f:Lnet/gogame/gopay/vip/VipStatus;

    .line 145
    invoke-direct {p0, p1}, Lnet/gogame/gopay/vip/VipClient;->b(Lnet/gogame/gopay/vip/VipStatus;)V

    .line 146
    iget-object p1, p0, Lnet/gogame/gopay/vip/VipClient;->f:Lnet/gogame/gopay/vip/VipStatus;

    if-eqz p1, :cond_0

    const-string p1, "goPay"

    const-string v0, "VIP status for %s: %s / %s / %s"

    const/4 v1, 0x4

    .line 147
    new-array v1, v1, [Ljava/lang/Object;

    const/4 v2, 0x0

    iget-object v3, p0, Lnet/gogame/gopay/vip/VipClient;->f:Lnet/gogame/gopay/vip/VipStatus;

    .line 148
    invoke-virtual {v3}, Lnet/gogame/gopay/vip/VipStatus;->getGuid()Ljava/lang/String;

    move-result-object v3

    aput-object v3, v1, v2

    const/4 v2, 0x1

    iget-object v3, p0, Lnet/gogame/gopay/vip/VipClient;->f:Lnet/gogame/gopay/vip/VipStatus;

    invoke-virtual {v3}, Lnet/gogame/gopay/vip/VipStatus;->isVip()Z

    move-result v3

    invoke-static {v3}, Ljava/lang/Boolean;->valueOf(Z)Ljava/lang/Boolean;

    move-result-object v3

    aput-object v3, v1, v2

    const/4 v2, 0x2

    iget-object v3, p0, Lnet/gogame/gopay/vip/VipClient;->f:Lnet/gogame/gopay/vip/VipStatus;

    invoke-virtual {v3}, Lnet/gogame/gopay/vip/VipStatus;->isSuspended()Z

    move-result v3

    invoke-static {v3}, Ljava/lang/Boolean;->valueOf(Z)Ljava/lang/Boolean;

    move-result-object v3

    aput-object v3, v1, v2

    const/4 v2, 0x3

    iget-object v3, p0, Lnet/gogame/gopay/vip/VipClient;->f:Lnet/gogame/gopay/vip/VipStatus;

    .line 149
    invoke-virtual {v3}, Lnet/gogame/gopay/vip/VipStatus;->getSuspensionMessage()Ljava/lang/String;

    move-result-object v3

    aput-object v3, v1, v2

    .line 147
    invoke-static {v0, v1}, Ljava/lang/String;->format(Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v0

    invoke-static {p1, v0}, Landroid/util/Log;->v(Ljava/lang/String;Ljava/lang/String;)I

    goto :goto_0

    :cond_0
    const-string p1, "goPay"

    const-string v0, "VIP status cleared due to error"

    .line 151
    invoke-static {p1, v0}, Landroid/util/Log;->v(Ljava/lang/String;Ljava/lang/String;)I

    :goto_0
    return-void
.end method

.method private static a(Ljava/lang/Object;Ljava/lang/Object;)Z
    .locals 1

    if-nez p0, :cond_0

    if-nez p1, :cond_0

    const/4 p0, 0x1

    return p0

    :cond_0
    const/4 v0, 0x0

    if-eqz p0, :cond_1

    if-nez p1, :cond_1

    return v0

    :cond_1
    if-nez p0, :cond_2

    if-eqz p1, :cond_2

    return v0

    .line 211
    :cond_2
    invoke-virtual {p0, p1}, Ljava/lang/Object;->equals(Ljava/lang/Object;)Z

    move-result p0

    return p0
.end method

.method private b()Ljava/util/Map;
    .locals 3
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()",
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            ">;"
        }
    .end annotation

    .line 258
    invoke-direct {p0}, Lnet/gogame/gopay/vip/VipClient;->a()Ljava/util/Map;

    move-result-object v0

    .line 259
    iget-object v1, p0, Lnet/gogame/gopay/vip/VipClient;->a:Ljava/lang/String;

    if-eqz v1, :cond_0

    const-string v1, "appId"

    .line 260
    iget-object v2, p0, Lnet/gogame/gopay/vip/VipClient;->a:Ljava/lang/String;

    invoke-interface {v0, v1, v2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    :cond_0
    return-object v0
.end method

.method private b(Lnet/gogame/gopay/vip/VipStatus;)V
    .locals 4

    .line 216
    iget-object v0, p0, Lnet/gogame/gopay/vip/VipClient;->g:Ljava/util/Set;

    invoke-interface {v0}, Ljava/util/Set;->iterator()Ljava/util/Iterator;

    move-result-object v0

    :cond_0
    :goto_0
    invoke-interface {v0}, Ljava/util/Iterator;->hasNext()Z

    move-result v1

    if-eqz v1, :cond_1

    invoke-interface {v0}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Lnet/gogame/gopay/vip/IVipClient$Listener;

    if-eqz v1, :cond_0

    .line 219
    :try_start_0
    invoke-interface {v1, p1}, Lnet/gogame/gopay/vip/IVipClient$Listener;->onVipStatus(Lnet/gogame/gopay/vip/VipStatus;)V
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception v1

    const-string v2, "goPay"

    const-string v3, "Exception"

    .line 222
    invoke-static {v2, v3, v1}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)I

    goto :goto_0

    :cond_1
    return-void
.end method

.method private c()Ljava/util/Map;
    .locals 3
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()",
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            ">;"
        }
    .end annotation

    .line 266
    invoke-direct {p0}, Lnet/gogame/gopay/vip/VipClient;->a()Ljava/util/Map;

    move-result-object v0

    .line 267
    iget-object v1, p0, Lnet/gogame/gopay/vip/VipClient;->a:Ljava/lang/String;

    if-eqz v1, :cond_0

    const-string v1, "app_id"

    .line 268
    iget-object v2, p0, Lnet/gogame/gopay/vip/VipClient;->a:Ljava/lang/String;

    invoke-interface {v0, v1, v2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    :cond_0
    return-object v0
.end method

.method public static getAppVersion(Landroid/content/Context;)Ljava/lang/String;
    .locals 2

    .line 91
    invoke-virtual {p0}, Landroid/content/Context;->getPackageManager()Landroid/content/pm/PackageManager;

    move-result-object v0

    .line 93
    :try_start_0
    invoke-virtual {p0}, Landroid/content/Context;->getPackageName()Ljava/lang/String;

    move-result-object p0

    const/4 v1, 0x0

    invoke-virtual {v0, p0, v1}, Landroid/content/pm/PackageManager;->getPackageInfo(Ljava/lang/String;I)Landroid/content/pm/PackageInfo;

    move-result-object p0

    .line 94
    iget-object p0, p0, Landroid/content/pm/PackageInfo;->versionName:Ljava/lang/String;
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    return-object p0

    :catch_0
    const/4 p0, 0x0

    return-object p0
.end method


# virtual methods
.method public addListener(Lnet/gogame/gopay/vip/IVipClient$Listener;)V
    .locals 1

    if-eqz p1, :cond_0

    .line 127
    iget-object v0, p0, Lnet/gogame/gopay/vip/VipClient;->g:Ljava/util/Set;

    invoke-interface {v0, p1}, Ljava/util/Set;->add(Ljava/lang/Object;)Z

    :cond_0
    return-void
.end method

.method public checkVipStatus(Ljava/lang/String;Z)V
    .locals 1

    if-nez p1, :cond_0

    return-void

    :cond_0
    if-nez p2, :cond_1

    .line 160
    iget-object p2, p0, Lnet/gogame/gopay/vip/VipClient;->f:Lnet/gogame/gopay/vip/VipStatus;

    if-eqz p2, :cond_1

    iget-object p2, p0, Lnet/gogame/gopay/vip/VipClient;->f:Lnet/gogame/gopay/vip/VipStatus;

    invoke-virtual {p2}, Lnet/gogame/gopay/vip/VipStatus;->getGuid()Ljava/lang/String;

    move-result-object p2

    invoke-static {p2, p1}, Lnet/gogame/gopay/vip/VipClient;->a(Ljava/lang/Object;Ljava/lang/Object;)Z

    move-result p2

    if-nez p2, :cond_2

    .line 161
    :cond_1
    new-instance p2, Ljava/lang/Thread;

    new-instance v0, Lnet/gogame/gopay/vip/VipClient$2;

    invoke-direct {v0, p0, p1}, Lnet/gogame/gopay/vip/VipClient$2;-><init>(Lnet/gogame/gopay/vip/VipClient;Ljava/lang/String;)V

    invoke-direct {p2, v0}, Ljava/lang/Thread;-><init>(Ljava/lang/Runnable;)V

    .line 191
    invoke-virtual {p2}, Ljava/lang/Thread;->start()V

    :cond_2
    return-void
.end method

.method public getVipStatus()Lnet/gogame/gopay/vip/VipStatus;
    .locals 1

    .line 140
    iget-object v0, p0, Lnet/gogame/gopay/vip/VipClient;->f:Lnet/gogame/gopay/vip/VipStatus;

    return-object v0
.end method

.method public init(Landroid/content/Context;Ljava/lang/String;Ljava/lang/String;)V
    .locals 1

    .line 71
    iput-object p2, p0, Lnet/gogame/gopay/vip/VipClient;->a:Ljava/lang/String;

    .line 72
    iput-object p3, p0, Lnet/gogame/gopay/vip/VipClient;->b:Ljava/lang/String;

    .line 74
    invoke-virtual {p1}, Landroid/content/Context;->getApplicationContext()Landroid/content/Context;

    move-result-object p2

    invoke-virtual {p2}, Landroid/content/Context;->getPackageName()Ljava/lang/String;

    move-result-object p2

    iput-object p2, p0, Lnet/gogame/gopay/vip/VipClient;->c:Ljava/lang/String;

    .line 75
    invoke-static {p1}, Lnet/gogame/gopay/vip/VipClient;->getAppVersion(Landroid/content/Context;)Ljava/lang/String;

    move-result-object p2

    iput-object p2, p0, Lnet/gogame/gopay/vip/VipClient;->d:Ljava/lang/String;

    .line 76
    invoke-virtual {p1}, Landroid/content/Context;->getContentResolver()Landroid/content/ContentResolver;

    move-result-object p2

    const-string p3, "android_id"

    invoke-static {p2, p3}, Landroid/provider/Settings$Secure;->getString(Landroid/content/ContentResolver;Ljava/lang/String;)Ljava/lang/String;

    move-result-object p2

    iput-object p2, p0, Lnet/gogame/gopay/vip/VipClient;->e:Ljava/lang/String;

    .line 79
    iget-object p2, p0, Lnet/gogame/gopay/vip/VipClient;->j:Lnet/gogame/gopay/vip/TaskQueue;

    if-nez p2, :cond_0

    .line 80
    new-instance p2, Ljava/io/File;

    invoke-virtual {p1}, Landroid/content/Context;->getFilesDir()Ljava/io/File;

    move-result-object p3

    const-string v0, "gopay-vip-client-queue.dat"

    invoke-direct {p2, p3, v0}, Ljava/io/File;-><init>(Ljava/io/File;Ljava/lang/String;)V

    .line 82
    :try_start_0
    new-instance p3, Lnet/gogame/gopay/vip/CustomTaskQueue;

    iget-object v0, p0, Lnet/gogame/gopay/vip/VipClient;->k:Lnet/gogame/gopay/vip/TaskQueue$Listener;

    invoke-direct {p3, p1, p2, v0}, Lnet/gogame/gopay/vip/CustomTaskQueue;-><init>(Landroid/content/Context;Ljava/io/File;Lnet/gogame/gopay/vip/TaskQueue$Listener;)V

    iput-object p3, p0, Lnet/gogame/gopay/vip/VipClient;->j:Lnet/gogame/gopay/vip/TaskQueue;

    .line 83
    iget-object p1, p0, Lnet/gogame/gopay/vip/VipClient;->j:Lnet/gogame/gopay/vip/TaskQueue;

    invoke-interface {p1}, Lnet/gogame/gopay/vip/TaskQueue;->start()V
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception p1

    const-string p2, "goPay"

    const-string p3, "Exception"

    .line 85
    invoke-static {p2, p3, p1}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)I

    :cond_0
    :goto_0
    return-void
.end method

.method public removeListener(Lnet/gogame/gopay/vip/IVipClient$Listener;)V
    .locals 1

    if-eqz p1, :cond_0

    .line 134
    iget-object v0, p0, Lnet/gogame/gopay/vip/VipClient;->g:Ljava/util/Set;

    invoke-interface {v0, p1}, Ljava/util/Set;->remove(Ljava/lang/Object;)Z

    :cond_0
    return-void
.end method

.method public setExtraData(Ljava/lang/String;Ljava/lang/String;)V
    .locals 1

    if-nez p1, :cond_0

    return-void

    :cond_0
    if-eqz p2, :cond_1

    .line 106
    iget-object v0, p0, Lnet/gogame/gopay/vip/VipClient;->h:Ljava/util/Map;

    invoke-interface {v0, p1, p2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    goto :goto_0

    .line 108
    :cond_1
    iget-object p2, p0, Lnet/gogame/gopay/vip/VipClient;->h:Ljava/util/Map;

    invoke-interface {p2, p1}, Ljava/util/Map;->remove(Ljava/lang/Object;)Ljava/lang/Object;

    :goto_0
    return-void
.end method

.method public setExtraHeader(Ljava/lang/String;Ljava/lang/String;)V
    .locals 1

    if-nez p1, :cond_0

    return-void

    :cond_0
    if-eqz p2, :cond_1

    .line 118
    iget-object v0, p0, Lnet/gogame/gopay/vip/VipClient;->i:Ljava/util/Map;

    invoke-interface {v0, p1, p2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    goto :goto_0

    .line 120
    :cond_1
    iget-object p2, p0, Lnet/gogame/gopay/vip/VipClient;->i:Ljava/util/Map;

    invoke-interface {p2, p1}, Ljava/util/Map;->remove(Ljava/lang/Object;)Ljava/lang/Object;

    :goto_0
    return-void
.end method

.method public trackPurchase(Lnet/gogame/gopay/vip/PurchaseEvent;)V
    .locals 1

    if-nez p1, :cond_0

    return-void

    .line 200
    :cond_0
    iget-object v0, p0, Lnet/gogame/gopay/vip/VipClient;->j:Lnet/gogame/gopay/vip/TaskQueue;

    invoke-interface {v0, p1}, Lnet/gogame/gopay/vip/TaskQueue;->add(Ljava/lang/Object;)V

    return-void
.end method
