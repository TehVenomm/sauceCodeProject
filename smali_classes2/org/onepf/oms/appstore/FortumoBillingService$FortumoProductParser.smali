.class Lorg/onepf/oms/appstore/FortumoBillingService$FortumoProductParser;
.super Ljava/lang/Object;
.source "FortumoBillingService.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lorg/onepf/oms/appstore/FortumoBillingService;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x8
    name = "FortumoProductParser"
.end annotation

.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Lorg/onepf/oms/appstore/FortumoBillingService$FortumoProductParser$FortumoDetails;
    }
.end annotation


# static fields
.field private static final CONSUMABLE_ATTR:Ljava/lang/String; = "consumable"

.field private static final FORTUMO_PRODUCTS_TAG:Ljava/lang/String; = "fortumo-products"

.field private static final ID_ATTR:Ljava/lang/String; = "id"

.field private static final NOOK_SERVICE_ID_ATTR:Ljava/lang/String; = "nook-service-id"

.field private static final NOOK_SERVICE_INAPP_SECRET_ATTR:Ljava/lang/String; = "nook-service-inapp-secret"

.field private static final PRODUCT_TAG:Ljava/lang/String; = "product"

.field private static final SERVICE_ID_ATTR:Ljava/lang/String; = "service-id"

.field private static final SERVICE_INAPP_SECRET_ATTR:Ljava/lang/String; = "service-inapp-secret"

.field private static final skuPattern:Ljava/util/regex/Pattern;


# direct methods
.method static constructor <clinit>()V
    .locals 1

    const-string v0, "([a-z]|[0-9]){1}[a-z0-9._]*"

    .line 399
    invoke-static {v0}, Ljava/util/regex/Pattern;->compile(Ljava/lang/String;)Ljava/util/regex/Pattern;

    move-result-object v0

    sput-object v0, Lorg/onepf/oms/appstore/FortumoBillingService$FortumoProductParser;->skuPattern:Ljava/util/regex/Pattern;

    return-void
.end method

.method private constructor <init>()V
    .locals 0

    .line 412
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method

.method static parse(Landroid/content/Context;Z)Ljava/util/Map;
    .locals 14
    .param p0    # Landroid/content/Context;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Landroid/content/Context;",
            "Z)",
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Lorg/onepf/oms/appstore/FortumoBillingService$FortumoProductParser$FortumoDetails;",
            ">;"
        }
    .end annotation

    .annotation system Ldalvik/annotation/Throws;
        value = {
            Lorg/xmlpull/v1/XmlPullParserException;,
            Ljava/io/IOException;
        }
    .end annotation

    .annotation build Lorg/jetbrains/annotations/NotNull;
    .end annotation

    .line 418
    invoke-static {}, Lorg/xmlpull/v1/XmlPullParserFactory;->newInstance()Lorg/xmlpull/v1/XmlPullParserFactory;

    move-result-object v0

    const/4 v1, 0x1

    .line 419
    invoke-virtual {v0, v1}, Lorg/xmlpull/v1/XmlPullParserFactory;->setNamespaceAware(Z)V

    .line 420
    invoke-virtual {v0}, Lorg/xmlpull/v1/XmlPullParserFactory;->newPullParser()Lorg/xmlpull/v1/XmlPullParser;

    move-result-object v0

    .line 421
    invoke-virtual {p0}, Landroid/content/Context;->getAssets()Landroid/content/res/AssetManager;

    move-result-object p0

    const-string v2, "fortumo_inapps_details.xml"

    invoke-virtual {p0, v2}, Landroid/content/res/AssetManager;->open(Ljava/lang/String;)Ljava/io/InputStream;

    move-result-object p0

    const/4 v2, 0x0

    invoke-interface {v0, p0, v2}, Lorg/xmlpull/v1/XmlPullParser;->setInput(Ljava/io/InputStream;Ljava/lang/String;)V

    .line 423
    new-instance p0, Ljava/util/HashMap;

    invoke-direct {p0}, Ljava/util/HashMap;-><init>()V

    .line 427
    invoke-interface {v0}, Lorg/xmlpull/v1/XmlPullParser;->getEventType()I

    move-result v3

    const/4 v4, 0x0

    move-object v5, v2

    const/4 v6, 0x0

    :goto_0
    if-eq v3, v1, :cond_a

    .line 429
    invoke-interface {v0}, Lorg/xmlpull/v1/XmlPullParser;->getName()Ljava/lang/String;

    move-result-object v7

    packed-switch v3, :pswitch_data_0

    goto/16 :goto_2

    :pswitch_0
    const-string v3, "product"

    .line 470
    invoke-virtual {v7, v3}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v3

    if-eqz v3, :cond_0

    if-eqz v5, :cond_9

    .line 472
    invoke-virtual {v5}, Lorg/onepf/oms/appstore/FortumoBillingService$FortumoProductParser$FortumoDetails;->getId()Ljava/lang/String;

    move-result-object v3

    invoke-virtual {p0, v3, v5}, Ljava/util/HashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    move-object v5, v2

    goto/16 :goto_2

    :cond_0
    const-string v3, "fortumo-products"

    .line 475
    invoke-virtual {v7, v3}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v3

    if-eqz v3, :cond_9

    const/4 v6, 0x0

    goto/16 :goto_2

    :pswitch_1
    const-string v3, "fortumo-products"

    .line 432
    invoke-virtual {v7, v3}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v3

    if-eqz v3, :cond_1

    const/4 v6, 0x1

    goto/16 :goto_2

    :cond_1
    const-string v3, "product"

    .line 434
    invoke-virtual {v7, v3}, Ljava/lang/String;->equalsIgnoreCase(Ljava/lang/String;)Z

    move-result v3

    if-eqz v3, :cond_9

    if-eqz v6, :cond_8

    const-string v3, "id"

    .line 438
    invoke-interface {v0, v2, v3}, Lorg/xmlpull/v1/XmlPullParser;->getAttributeValue(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v8

    .line 439
    sget-object v3, Lorg/onepf/oms/appstore/FortumoBillingService$FortumoProductParser;->skuPattern:Ljava/util/regex/Pattern;

    invoke-virtual {v3, v8}, Ljava/util/regex/Pattern;->matcher(Ljava/lang/CharSequence;)Ljava/util/regex/Matcher;

    move-result-object v3

    invoke-virtual {v3}, Ljava/util/regex/Matcher;->matches()Z

    move-result v3

    if-eqz v3, :cond_7

    const-string v3, "service-id"

    .line 443
    invoke-interface {v0, v2, v3}, Lorg/xmlpull/v1/XmlPullParser;->getAttributeValue(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v10

    const-string v3, "service-inapp-secret"

    .line 444
    invoke-interface {v0, v2, v3}, Lorg/xmlpull/v1/XmlPullParser;->getAttributeValue(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v11

    .line 445
    invoke-static {v10, v11}, Lorg/onepf/oms/appstore/FortumoBillingService$FortumoProductParser;->serviceInfoIsComplete(Ljava/lang/String;Ljava/lang/String;)Z

    move-result v3

    if-eqz v3, :cond_6

    const-string v3, "nook-service-id"

    .line 449
    invoke-interface {v0, v2, v3}, Lorg/xmlpull/v1/XmlPullParser;->getAttributeValue(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v12

    const-string v3, "nook-service-inapp-secret"

    .line 450
    invoke-interface {v0, v2, v3}, Lorg/xmlpull/v1/XmlPullParser;->getAttributeValue(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v13

    .line 451
    invoke-static {v12, v13}, Lorg/onepf/oms/appstore/FortumoBillingService$FortumoProductParser;->serviceInfoIsComplete(Ljava/lang/String;Ljava/lang/String;)Z

    move-result v3

    if-eqz v3, :cond_5

    if-eqz p1, :cond_3

    .line 456
    invoke-static {v12}, Landroid/text/TextUtils;->isEmpty(Ljava/lang/CharSequence;)Z

    move-result v3

    if-nez v3, :cond_2

    invoke-static {v13}, Landroid/text/TextUtils;->isEmpty(Ljava/lang/CharSequence;)Z

    move-result v3

    if-nez v3, :cond_2

    goto :goto_1

    .line 457
    :cond_2
    new-instance p0, Ljava/lang/IllegalStateException;

    const-string p1, "fortumo nook-service-id attribute and nook-service-inapp-secret values must be non-empty!"

    invoke-direct {p0, p1}, Ljava/lang/IllegalStateException;-><init>(Ljava/lang/String;)V

    throw p0

    .line 460
    :cond_3
    invoke-static {v10}, Landroid/text/TextUtils;->isEmpty(Ljava/lang/CharSequence;)Z

    move-result v3

    if-nez v3, :cond_4

    invoke-static {v11}, Landroid/text/TextUtils;->isEmpty(Ljava/lang/CharSequence;)Z

    move-result v3

    if-nez v3, :cond_4

    .line 464
    :goto_1
    new-instance v3, Lorg/onepf/oms/appstore/FortumoBillingService$FortumoProductParser$FortumoDetails;

    const-string v5, "consumable"

    invoke-interface {v0, v2, v5}, Lorg/xmlpull/v1/XmlPullParser;->getAttributeValue(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v5

    invoke-static {v5}, Ljava/lang/Boolean;->parseBoolean(Ljava/lang/String;)Z

    move-result v9

    move-object v7, v3

    invoke-direct/range {v7 .. v13}, Lorg/onepf/oms/appstore/FortumoBillingService$FortumoProductParser$FortumoDetails;-><init>(Ljava/lang/String;ZLjava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V

    move-object v5, v3

    goto :goto_2

    .line 461
    :cond_4
    new-instance p0, Ljava/lang/IllegalStateException;

    const-string p1, "fortumo service-id attribute and service-inapp-secret values must be non-empty!"

    invoke-direct {p0, p1}, Ljava/lang/IllegalStateException;-><init>(Ljava/lang/String;)V

    throw p0

    .line 452
    :cond_5
    new-instance p0, Ljava/lang/IllegalStateException;

    new-array p1, v1, [Ljava/lang/Object;

    aput-object v8, p1, v4

    const-string v0, "%s: service data is NOT complete"

    invoke-static {v0, p1}, Ljava/lang/String;->format(Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object p1

    invoke-direct {p0, p1}, Ljava/lang/IllegalStateException;-><init>(Ljava/lang/String;)V

    throw p0

    .line 446
    :cond_6
    new-instance p0, Ljava/lang/IllegalStateException;

    new-array p1, v1, [Ljava/lang/Object;

    aput-object v8, p1, v4

    const-string v0, "%s: service data is NOT complete"

    invoke-static {v0, p1}, Ljava/lang/String;->format(Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object p1

    invoke-direct {p0, p1}, Ljava/lang/IllegalStateException;-><init>(Ljava/lang/String;)V

    throw p0

    .line 440
    :cond_7
    new-instance p0, Ljava/lang/IllegalStateException;

    new-array p1, v1, [Ljava/lang/Object;

    aput-object v8, p1, v4

    const-string v0, "Wrong SKU: %s. SKU must match \"([a-z]|[0-9]){1}[a-z0-9._]*\"."

    invoke-static {v0, p1}, Ljava/lang/String;->format(Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object p1

    invoke-direct {p0, p1}, Ljava/lang/IllegalStateException;-><init>(Ljava/lang/String;)V

    throw p0

    .line 436
    :cond_8
    new-instance p0, Ljava/lang/IllegalStateException;

    const/4 p1, 0x2

    new-array p1, p1, [Ljava/lang/Object;

    const-string v0, "product"

    aput-object v0, p1, v4

    const-string v0, "fortumo-products"

    aput-object v0, p1, v1

    const-string v0, "%s is not inside %s"

    invoke-static {v0, p1}, Ljava/lang/String;->format(Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object p1

    invoke-direct {p0, p1}, Ljava/lang/IllegalStateException;-><init>(Ljava/lang/String;)V

    throw p0

    .line 483
    :cond_9
    :goto_2
    invoke-interface {v0}, Lorg/xmlpull/v1/XmlPullParser;->next()I

    move-result v3

    goto/16 :goto_0

    :cond_a
    return-object p0

    :pswitch_data_0
    .packed-switch 0x2
        :pswitch_1
        :pswitch_0
    .end packed-switch
.end method

.method private static serviceInfoIsComplete(Ljava/lang/String;Ljava/lang/String;)Z
    .locals 1

    .line 489
    invoke-static {p0}, Landroid/text/TextUtils;->isEmpty(Ljava/lang/CharSequence;)Z

    move-result v0

    if-eqz v0, :cond_1

    invoke-static {p1}, Landroid/text/TextUtils;->isEmpty(Ljava/lang/CharSequence;)Z

    move-result v0

    if-nez v0, :cond_1

    invoke-static {p1}, Landroid/text/TextUtils;->isEmpty(Ljava/lang/CharSequence;)Z

    move-result p1

    if-eqz p1, :cond_0

    invoke-static {p0}, Landroid/text/TextUtils;->isEmpty(Ljava/lang/CharSequence;)Z

    move-result p0

    if-nez p0, :cond_0

    goto :goto_0

    :cond_0
    const/4 p0, 0x0

    goto :goto_1

    :cond_1
    :goto_0
    const/4 p0, 0x1

    :goto_1
    return p0
.end method
