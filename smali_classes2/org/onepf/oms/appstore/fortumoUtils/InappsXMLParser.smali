.class public Lorg/onepf/oms/appstore/fortumoUtils/InappsXMLParser;
.super Ljava/lang/Object;
.source "InappsXMLParser.java"


# static fields
.field private static final ATTR_AUTOFILL:Ljava/lang/String; = "autofill"

.field private static final ATTR_COUNTRY:Ljava/lang/String; = "country"

.field private static final ATTR_ID:Ljava/lang/String; = "id"

.field private static final ATTR_LOCALE:Ljava/lang/String; = "locale"

.field private static final ATTR_PERIOD:Ljava/lang/String; = "period"

.field private static final ATTR_PUBLISH_STATE:Ljava/lang/String; = "publish-state"

.field private static final TAG_COMMON_DESCRIPTION:Ljava/lang/String; = "description"

.field private static final TAG_COMMON_TITLE:Ljava/lang/String; = "title"

.field private static final TAG_INAPP_PRODUCTS:Ljava/lang/String; = "inapp-products"

.field private static final TAG_ITEM:Ljava/lang/String; = "item"

.field private static final TAG_ITEMS:Ljava/lang/String; = "items"

.field private static final TAG_PRICE:Ljava/lang/String; = "price"

.field private static final TAG_PRICE_BASE:Ljava/lang/String; = "price-base"

.field private static final TAG_PRICE_LOCAL:Ljava/lang/String; = "price-local"

.field private static final TAG_SUBSCRIPTION:Ljava/lang/String; = "subscription"

.field private static final TAG_SUBSCRIPTIONS:Ljava/lang/String; = "subscriptions"

.field private static final TAG_SUMMARY:Ljava/lang/String; = "summary"

.field private static final TAG_SUMMARY_BASE:Ljava/lang/String; = "summary-base"

.field private static final TAG_SUMMARY_LOCALIZATION:Ljava/lang/String; = "summary-localization"

.field private static final countryPattern:Ljava/util/regex/Pattern;

.field private static final localePattern:Ljava/util/regex/Pattern;

.field private static final skuPattern:Ljava/util/regex/Pattern;


# direct methods
.method static constructor <clinit>()V
    .locals 1

    const-string v0, "[A-Z][A-Z]"

    .line 39
    invoke-static {v0}, Ljava/util/regex/Pattern;->compile(Ljava/lang/String;)Ljava/util/regex/Pattern;

    move-result-object v0

    sput-object v0, Lorg/onepf/oms/appstore/fortumoUtils/InappsXMLParser;->countryPattern:Ljava/util/regex/Pattern;

    const-string v0, "[a-z][a-z]_[A-Z][A-Z]"

    .line 40
    invoke-static {v0}, Ljava/util/regex/Pattern;->compile(Ljava/lang/String;)Ljava/util/regex/Pattern;

    move-result-object v0

    sput-object v0, Lorg/onepf/oms/appstore/fortumoUtils/InappsXMLParser;->localePattern:Ljava/util/regex/Pattern;

    const-string v0, "([a-z]|[0-9]){1}[a-z0-9._]*"

    .line 41
    invoke-static {v0}, Ljava/util/regex/Pattern;->compile(Ljava/lang/String;)Ljava/util/regex/Pattern;

    move-result-object v0

    sput-object v0, Lorg/onepf/oms/appstore/fortumoUtils/InappsXMLParser;->skuPattern:Ljava/util/regex/Pattern;

    return-void
.end method

.method public constructor <init>()V
    .locals 0

    .line 38
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method

.method private static inWrongNode(Ljava/lang/String;Ljava/lang/String;)V
    .locals 3

    .line 275
    new-instance v0, Ljava/lang/IllegalStateException;

    const/4 v1, 0x2

    new-array v1, v1, [Ljava/lang/Object;

    const/4 v2, 0x0

    aput-object p0, v1, v2

    const/4 p0, 0x1

    aput-object p1, v1, p0

    const-string p0, "%s is not inside %s"

    invoke-static {p0, v1}, Ljava/lang/String;->format(Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object p0

    invoke-direct {v0, p0}, Ljava/lang/IllegalStateException;-><init>(Ljava/lang/String;)V

    throw v0
.end method

.method private static inWrongNode(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V
    .locals 3

    .line 279
    new-instance v0, Ljava/lang/IllegalStateException;

    const/4 v1, 0x3

    new-array v1, v1, [Ljava/lang/Object;

    const/4 v2, 0x0

    aput-object p0, v1, v2

    const/4 p0, 0x1

    aput-object p1, v1, p0

    const/4 p0, 0x2

    aput-object p2, v1, p0

    const-string p0, "%s is not inside %s or %s"

    invoke-static {p0, v1}, Ljava/lang/String;->format(Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object p0

    invoke-direct {v0, p0}, Ljava/lang/IllegalStateException;-><init>(Ljava/lang/String;)V

    throw v0
.end method


# virtual methods
.method public parse(Landroid/content/Context;)Landroid/util/Pair;
    .locals 25
    .param p1    # Landroid/content/Context;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Landroid/content/Context;",
            ")",
            "Landroid/util/Pair<",
            "Ljava/util/List<",
            "Lorg/onepf/oms/appstore/fortumoUtils/InappBaseProduct;",
            ">;",
            "Ljava/util/List<",
            "Lorg/onepf/oms/appstore/fortumoUtils/InappSubscriptionProduct;",
            ">;>;"
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

    .line 78
    invoke-static {}, Lorg/xmlpull/v1/XmlPullParserFactory;->newInstance()Lorg/xmlpull/v1/XmlPullParserFactory;

    move-result-object v0

    const/4 v1, 0x1

    .line 79
    invoke-virtual {v0, v1}, Lorg/xmlpull/v1/XmlPullParserFactory;->setNamespaceAware(Z)V

    .line 80
    invoke-virtual {v0}, Lorg/xmlpull/v1/XmlPullParserFactory;->newPullParser()Lorg/xmlpull/v1/XmlPullParser;

    move-result-object v0

    .line 81
    invoke-virtual/range {p1 .. p1}, Landroid/content/Context;->getAssets()Landroid/content/res/AssetManager;

    move-result-object v2

    const-string v3, "inapps_products.xml"

    invoke-virtual {v2, v3}, Landroid/content/res/AssetManager;->open(Ljava/lang/String;)Ljava/io/InputStream;

    move-result-object v2

    const/4 v3, 0x0

    invoke-interface {v0, v2, v3}, Lorg/xmlpull/v1/XmlPullParser;->setInput(Ljava/io/InputStream;Ljava/lang/String;)V

    .line 83
    new-instance v2, Ljava/util/ArrayList;

    invoke-direct {v2}, Ljava/util/ArrayList;-><init>()V

    .line 84
    new-instance v4, Ljava/util/ArrayList;

    invoke-direct {v4}, Ljava/util/ArrayList;-><init>()V

    .line 104
    invoke-interface {v0}, Lorg/xmlpull/v1/XmlPullParser;->getEventType()I

    move-result v5

    move-object v8, v3

    move-object v11, v8

    move-object v15, v11

    move-object/from16 v17, v15

    move-object/from16 v18, v17

    move-object/from16 v20, v18

    const/4 v7, 0x0

    const/4 v9, 0x0

    const/4 v10, 0x0

    const/4 v12, 0x0

    const/4 v13, 0x0

    const/4 v14, 0x0

    const/16 v16, 0x0

    const/16 v19, 0x0

    const/16 v21, 0x0

    :goto_0
    if-eq v5, v1, :cond_2f

    .line 106
    invoke-interface {v0}, Lorg/xmlpull/v1/XmlPullParser;->getName()Ljava/lang/String;

    move-result-object v6

    packed-switch v5, :pswitch_data_0

    move-object/from16 v23, v3

    move-object/from16 v5, v17

    move-object/from16 v24, v18

    move-object/from16 v22, v20

    :goto_1
    const/4 v3, 0x0

    const/4 v6, 0x0

    goto/16 :goto_c

    .line 199
    :pswitch_0
    invoke-interface {v0}, Lorg/xmlpull/v1/XmlPullParser;->getText()Ljava/lang/String;

    move-result-object v5

    move-object/from16 v23, v3

    move-object v15, v5

    :goto_2
    const/4 v3, 0x0

    :goto_3
    const/4 v6, 0x0

    goto/16 :goto_d

    :pswitch_1
    const-string v5, "inapp-products"

    .line 202
    invoke-virtual {v6, v5}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v5

    if-eqz v5, :cond_0

    move-object/from16 v23, v3

    const/4 v3, 0x0

    const/4 v6, 0x0

    const/4 v7, 0x0

    goto/16 :goto_d

    :cond_0
    const-string v5, "items"

    .line 204
    invoke-virtual {v6, v5}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v5

    if-eqz v5, :cond_1

    move-object/from16 v23, v3

    const/4 v3, 0x0

    const/4 v6, 0x0

    const/4 v10, 0x0

    goto/16 :goto_d

    :cond_1
    const-string v5, "subscriptions"

    .line 206
    invoke-virtual {v6, v5}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v5

    if-eqz v5, :cond_2

    move-object/from16 v23, v3

    const/4 v3, 0x0

    const/4 v6, 0x0

    const/4 v9, 0x0

    goto/16 :goto_d

    :cond_2
    const-string v5, "item"

    .line 208
    invoke-virtual {v6, v5}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v5

    if-eqz v5, :cond_3

    .line 209
    invoke-virtual {v8}, Lorg/onepf/oms/appstore/fortumoUtils/InappBaseProduct;->validateItem()V

    .line 210
    invoke-interface {v2, v8}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    move-object/from16 v23, v3

    const/4 v3, 0x0

    const/4 v6, 0x0

    const/4 v8, 0x0

    goto/16 :goto_d

    :cond_3
    const-string v5, "subscription"

    .line 212
    invoke-virtual {v6, v5}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v5

    if-eqz v5, :cond_4

    .line 213
    new-instance v5, Lorg/onepf/oms/appstore/fortumoUtils/InappSubscriptionProduct;

    invoke-direct {v5, v8, v11}, Lorg/onepf/oms/appstore/fortumoUtils/InappSubscriptionProduct;-><init>(Lorg/onepf/oms/appstore/fortumoUtils/InappBaseProduct;Ljava/lang/String;)V

    .line 214
    invoke-virtual {v5}, Lorg/onepf/oms/appstore/fortumoUtils/InappSubscriptionProduct;->validateItem()V

    .line 215
    invoke-interface {v4, v5}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    move-object/from16 v23, v3

    const/4 v3, 0x0

    const/4 v6, 0x0

    const/4 v8, 0x0

    const/4 v11, 0x0

    const/4 v13, 0x0

    goto/16 :goto_d

    :cond_4
    const-string v5, "summary"

    .line 219
    invoke-virtual {v6, v5}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v5

    if-eqz v5, :cond_5

    move-object/from16 v23, v3

    const/4 v3, 0x0

    const/4 v6, 0x0

    const/4 v14, 0x0

    goto/16 :goto_d

    :cond_5
    const-string v5, "title"

    .line 221
    invoke-virtual {v6, v5}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v5

    if-eqz v5, :cond_7

    .line 222
    invoke-virtual {v15}, Ljava/lang/String;->length()I

    move-result v3

    if-lt v3, v1, :cond_6

    const/16 v5, 0x37

    if-gt v3, v5, :cond_6

    move-object/from16 v23, v15

    goto/16 :goto_2

    .line 224
    :cond_6
    new-instance v0, Ljava/lang/IllegalStateException;

    new-array v1, v1, [Ljava/lang/Object;

    invoke-static {v3}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v2

    const/4 v3, 0x0

    aput-object v2, v1, v3

    const-string v2, "Wrong title length: %d. Must be 1-55 symbols"

    invoke-static {v2, v1}, Ljava/lang/String;->format(Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v1

    invoke-direct {v0, v1}, Ljava/lang/IllegalStateException;-><init>(Ljava/lang/String;)V

    throw v0

    :cond_7
    const-string v5, "description"

    .line 227
    invoke-virtual {v6, v5}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v5

    if-eqz v5, :cond_9

    .line 228
    invoke-virtual {v15}, Ljava/lang/String;->length()I

    move-result v5

    if-lt v5, v1, :cond_8

    const/16 v6, 0x50

    if-gt v5, v6, :cond_8

    move-object/from16 v23, v3

    move-object/from16 v17, v15

    goto/16 :goto_2

    .line 230
    :cond_8
    new-instance v0, Ljava/lang/IllegalStateException;

    new-array v1, v1, [Ljava/lang/Object;

    invoke-static {v5}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v2

    const/4 v3, 0x0

    aput-object v2, v1, v3

    const-string v2, "Wrong description length: %d. Must be 1-80 symbols"

    invoke-static {v2, v1}, Ljava/lang/String;->format(Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v1

    invoke-direct {v0, v1}, Ljava/lang/IllegalStateException;-><init>(Ljava/lang/String;)V

    throw v0

    :cond_9
    const-string v5, "summary-base"

    .line 233
    invoke-virtual {v6, v5}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v5

    if-eqz v5, :cond_a

    .line 234
    invoke-virtual {v8, v3}, Lorg/onepf/oms/appstore/fortumoUtils/InappBaseProduct;->setBaseTitle(Ljava/lang/String;)V

    move-object/from16 v5, v17

    .line 235
    invoke-virtual {v8, v5}, Lorg/onepf/oms/appstore/fortumoUtils/InappBaseProduct;->setBaseDescription(Ljava/lang/String;)V

    const/4 v3, 0x0

    const/4 v6, 0x0

    const/16 v16, 0x0

    const/16 v17, 0x0

    :goto_4
    const/16 v23, 0x0

    goto/16 :goto_d

    :cond_a
    move-object/from16 v5, v17

    const-string v1, "summary-localization"

    .line 239
    invoke-virtual {v6, v1}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v1

    if-eqz v1, :cond_b

    move-object/from16 v1, v20

    .line 240
    invoke-virtual {v8, v1, v3}, Lorg/onepf/oms/appstore/fortumoUtils/InappBaseProduct;->addTitleLocalization(Ljava/lang/String;Ljava/lang/String;)V

    .line 241
    invoke-virtual {v8, v1, v5}, Lorg/onepf/oms/appstore/fortumoUtils/InappBaseProduct;->addDescriptionLocalization(Ljava/lang/String;Ljava/lang/String;)V

    const/4 v1, 0x1

    const/4 v3, 0x0

    const/4 v6, 0x0

    const/16 v17, 0x0

    const/16 v19, 0x0

    const/16 v20, 0x0

    goto :goto_4

    :cond_b
    move-object/from16 v22, v20

    const-string v1, "price-base"

    .line 246
    invoke-virtual {v6, v1}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v1

    if-nez v1, :cond_e

    const-string v1, "price-local"

    invoke-virtual {v6, v1}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v1

    if-eqz v1, :cond_c

    goto :goto_5

    :cond_c
    const-string v1, "price"

    .line 260
    invoke-virtual {v6, v1}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v1

    if-eqz v1, :cond_d

    move-object/from16 v23, v3

    move-object/from16 v17, v5

    move-object/from16 v20, v22

    const/4 v1, 0x1

    const/4 v3, 0x0

    const/4 v6, 0x0

    const/16 v21, 0x0

    goto/16 :goto_d

    :cond_d
    move-object/from16 v23, v3

    move-object/from16 v24, v18

    goto/16 :goto_6

    .line 249
    :cond_e
    :goto_5
    :try_start_0
    invoke-static {v15}, Ljava/lang/Float;->parseFloat(Ljava/lang/String;)F

    move-result v1
    :try_end_0
    .catch Ljava/lang/NumberFormatException; {:try_start_0 .. :try_end_0} :catch_0

    move-object/from16 v23, v3

    const-string v3, "price-base"

    .line 254
    invoke-virtual {v6, v3}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v3

    if-eqz v3, :cond_f

    .line 255
    invoke-virtual {v8, v1}, Lorg/onepf/oms/appstore/fortumoUtils/InappBaseProduct;->setBasePrice(F)V

    goto/16 :goto_7

    :cond_f
    move-object/from16 v3, v18

    .line 257
    invoke-virtual {v8, v3, v1}, Lorg/onepf/oms/appstore/fortumoUtils/InappBaseProduct;->addCountryPrice(Ljava/lang/String;F)V

    const/16 v18, 0x0

    goto/16 :goto_7

    .line 251
    :catch_0
    new-instance v0, Ljava/lang/IllegalStateException;

    const/4 v1, 0x1

    new-array v1, v1, [Ljava/lang/Object;

    const/4 v2, 0x0

    aput-object v15, v1, v2

    const-string v2, "Wrong price: %s. Must be decimal."

    invoke-static {v2, v1}, Ljava/lang/String;->format(Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v1

    invoke-direct {v0, v1}, Ljava/lang/IllegalStateException;-><init>(Ljava/lang/String;)V

    throw v0

    :pswitch_2
    move-object/from16 v23, v3

    move-object/from16 v5, v17

    move-object/from16 v3, v18

    move-object/from16 v22, v20

    const-string v1, "inapp-products"

    .line 109
    invoke-virtual {v6, v1}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v1

    if-eqz v1, :cond_10

    move-object/from16 v18, v3

    move-object/from16 v17, v5

    move-object/from16 v20, v22

    const/4 v1, 0x1

    const/4 v3, 0x0

    const/4 v6, 0x0

    const/4 v7, 0x1

    goto/16 :goto_d

    :cond_10
    const-string v1, "items"

    .line 111
    invoke-virtual {v6, v1}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v1

    if-eqz v1, :cond_12

    if-nez v7, :cond_11

    const-string v1, "items"

    const-string v6, "inapp-products"

    .line 113
    invoke-static {v1, v6}, Lorg/onepf/oms/appstore/fortumoUtils/InappsXMLParser;->inWrongNode(Ljava/lang/String;Ljava/lang/String;)V

    :cond_11
    move-object/from16 v18, v3

    move-object/from16 v17, v5

    move-object/from16 v20, v22

    const/4 v1, 0x1

    const/4 v3, 0x0

    const/4 v6, 0x0

    const/4 v10, 0x1

    goto/16 :goto_d

    :cond_12
    const-string v1, "subscriptions"

    .line 116
    invoke-virtual {v6, v1}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v1

    if-eqz v1, :cond_14

    if-nez v7, :cond_13

    const-string v1, "subscriptions"

    const-string v6, "inapp-products"

    .line 118
    invoke-static {v1, v6}, Lorg/onepf/oms/appstore/fortumoUtils/InappsXMLParser;->inWrongNode(Ljava/lang/String;Ljava/lang/String;)V

    :cond_13
    move-object/from16 v18, v3

    move-object/from16 v17, v5

    move-object/from16 v20, v22

    const/4 v1, 0x1

    const/4 v3, 0x0

    const/4 v6, 0x0

    const/4 v9, 0x1

    goto/16 :goto_d

    :cond_14
    const-string v1, "item"

    .line 121
    invoke-virtual {v6, v1}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v1

    if-nez v1, :cond_26

    const-string v1, "subscription"

    invoke-virtual {v6, v1}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v1

    if-eqz v1, :cond_15

    goto/16 :goto_8

    :cond_15
    const-string v1, "summary"

    .line 150
    invoke-virtual {v6, v1}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v1

    if-eqz v1, :cond_17

    if-nez v12, :cond_16

    if-nez v13, :cond_16

    const-string v1, "summary"

    const-string v6, "item"

    const-string v14, "subscription"

    .line 152
    invoke-static {v1, v6, v14}, Lorg/onepf/oms/appstore/fortumoUtils/InappsXMLParser;->inWrongNode(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V

    :cond_16
    move-object/from16 v18, v3

    move-object/from16 v17, v5

    move-object/from16 v20, v22

    const/4 v1, 0x1

    const/4 v3, 0x0

    const/4 v6, 0x0

    const/4 v14, 0x1

    goto/16 :goto_d

    :cond_17
    const-string v1, "summary-base"

    .line 155
    invoke-virtual {v6, v1}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v1

    if-eqz v1, :cond_19

    if-nez v14, :cond_18

    const-string v1, "summary-base"

    const-string v6, "summary"

    .line 157
    invoke-static {v1, v6}, Lorg/onepf/oms/appstore/fortumoUtils/InappsXMLParser;->inWrongNode(Ljava/lang/String;Ljava/lang/String;)V

    :cond_18
    move-object/from16 v18, v3

    move-object/from16 v17, v5

    move-object/from16 v20, v22

    const/4 v1, 0x1

    const/4 v3, 0x0

    const/4 v6, 0x0

    const/16 v16, 0x1

    goto/16 :goto_d

    :cond_19
    const-string v1, "summary-localization"

    .line 160
    invoke-virtual {v6, v1}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v1

    if-eqz v1, :cond_1c

    if-nez v14, :cond_1a

    const-string v1, "summary-localization"

    const-string v6, "summary"

    .line 162
    invoke-static {v1, v6}, Lorg/onepf/oms/appstore/fortumoUtils/InappsXMLParser;->inWrongNode(Ljava/lang/String;Ljava/lang/String;)V

    :cond_1a
    const-string v1, "locale"

    const/4 v6, 0x0

    .line 164
    invoke-interface {v0, v6, v1}, Lorg/xmlpull/v1/XmlPullParser;->getAttributeValue(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v1

    .line 165
    sget-object v6, Lorg/onepf/oms/appstore/fortumoUtils/InappsXMLParser;->localePattern:Ljava/util/regex/Pattern;

    invoke-virtual {v6, v1}, Ljava/util/regex/Pattern;->matcher(Ljava/lang/CharSequence;)Ljava/util/regex/Matcher;

    move-result-object v6

    invoke-virtual {v6}, Ljava/util/regex/Matcher;->matches()Z

    move-result v6

    if-eqz v6, :cond_1b

    move-object/from16 v20, v1

    move-object/from16 v18, v3

    move-object/from16 v17, v5

    const/4 v1, 0x1

    const/4 v3, 0x0

    const/4 v6, 0x0

    const/16 v19, 0x1

    goto/16 :goto_d

    .line 166
    :cond_1b
    new-instance v0, Ljava/lang/IllegalStateException;

    const/4 v1, 0x1

    new-array v1, v1, [Ljava/lang/Object;

    const/4 v2, 0x0

    aput-object v3, v1, v2

    const-string v2, "Wrong \"locale\" attribute value: %s. Must match [a-z][a-z]_[A-Z][A-Z]."

    invoke-static {v2, v1}, Ljava/lang/String;->format(Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v1

    invoke-direct {v0, v1}, Ljava/lang/IllegalStateException;-><init>(Ljava/lang/String;)V

    throw v0

    :cond_1c
    const-string v1, "title"

    .line 169
    invoke-virtual {v6, v1}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v1

    if-eqz v1, :cond_1f

    if-nez v16, :cond_1d

    if-nez v19, :cond_1d

    const-string v1, "title"

    const-string v6, "summary-base"

    move-object/from16 v24, v3

    const-string v3, "summary-localization"

    .line 171
    invoke-static {v1, v6, v3}, Lorg/onepf/oms/appstore/fortumoUtils/InappsXMLParser;->inWrongNode(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V

    goto :goto_6

    :cond_1d
    move-object/from16 v24, v3

    :cond_1e
    :goto_6
    const/4 v1, 0x1

    goto/16 :goto_1

    :cond_1f
    move-object/from16 v24, v3

    const-string v1, "description"

    .line 173
    invoke-virtual {v6, v1}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v1

    if-eqz v1, :cond_20

    if-nez v16, :cond_1e

    if-nez v19, :cond_1e

    const-string v1, "description"

    const-string v3, "summary-base"

    const-string v6, "summary-localization"

    .line 175
    invoke-static {v1, v3, v6}, Lorg/onepf/oms/appstore/fortumoUtils/InappsXMLParser;->inWrongNode(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V

    goto :goto_6

    :cond_20
    const-string v1, "price"

    .line 177
    invoke-virtual {v6, v1}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v1

    if-eqz v1, :cond_22

    if-nez v12, :cond_21

    if-nez v13, :cond_21

    const-string v1, "price"

    const-string v3, "item"

    const-string v6, "subscription"

    .line 179
    invoke-static {v1, v3, v6}, Lorg/onepf/oms/appstore/fortumoUtils/InappsXMLParser;->inWrongNode(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V

    :cond_21
    const-string v1, "autofill"

    const/4 v3, 0x0

    .line 181
    invoke-interface {v0, v3, v1}, Lorg/xmlpull/v1/XmlPullParser;->getAttributeValue(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v1

    invoke-static {v1}, Ljava/lang/Boolean;->parseBoolean(Ljava/lang/String;)Z

    move-result v1

    invoke-virtual {v8, v1}, Lorg/onepf/oms/appstore/fortumoUtils/InappBaseProduct;->setAutoFill(Z)V

    move-object/from16 v17, v5

    move-object/from16 v20, v22

    move-object/from16 v18, v24

    const/4 v1, 0x1

    const/4 v3, 0x0

    const/4 v6, 0x0

    const/16 v21, 0x1

    goto/16 :goto_d

    :cond_22
    const-string v1, "price-base"

    .line 183
    invoke-virtual {v6, v1}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v1

    if-eqz v1, :cond_23

    if-nez v21, :cond_1e

    const-string v1, "price-base"

    const-string v3, "price"

    .line 185
    invoke-static {v1, v3}, Lorg/onepf/oms/appstore/fortumoUtils/InappsXMLParser;->inWrongNode(Ljava/lang/String;Ljava/lang/String;)V

    goto :goto_6

    :cond_23
    const-string v1, "price-local"

    .line 187
    invoke-virtual {v6, v1}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v1

    if-eqz v1, :cond_1e

    if-nez v21, :cond_24

    const-string v1, "price-local"

    const-string v3, "price"

    .line 189
    invoke-static {v1, v3}, Lorg/onepf/oms/appstore/fortumoUtils/InappsXMLParser;->inWrongNode(Ljava/lang/String;Ljava/lang/String;)V

    :cond_24
    const-string v1, "country"

    const/4 v3, 0x0

    .line 191
    invoke-interface {v0, v3, v1}, Lorg/xmlpull/v1/XmlPullParser;->getAttributeValue(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v1

    .line 192
    sget-object v3, Lorg/onepf/oms/appstore/fortumoUtils/InappsXMLParser;->countryPattern:Ljava/util/regex/Pattern;

    invoke-virtual {v3, v1}, Ljava/util/regex/Pattern;->matcher(Ljava/lang/CharSequence;)Ljava/util/regex/Matcher;

    move-result-object v3

    .line 193
    invoke-virtual {v3}, Ljava/util/regex/Matcher;->matches()Z

    move-result v3

    if-eqz v3, :cond_25

    move-object/from16 v18, v1

    :goto_7
    move-object/from16 v17, v5

    move-object/from16 v20, v22

    const/4 v1, 0x1

    goto/16 :goto_2

    .line 194
    :cond_25
    new-instance v0, Ljava/lang/IllegalStateException;

    const/4 v2, 0x1

    new-array v2, v2, [Ljava/lang/Object;

    const/4 v3, 0x0

    aput-object v1, v2, v3

    const-string v1, "Wrong \"country\" attribute value: %s. Must match [A-Z][A-Z]."

    invoke-static {v1, v2}, Ljava/lang/String;->format(Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v1

    invoke-direct {v0, v1}, Ljava/lang/IllegalStateException;-><init>(Ljava/lang/String;)V

    throw v0

    :cond_26
    :goto_8
    move-object/from16 v24, v3

    const-string v1, "subscription"

    .line 122
    invoke-virtual {v6, v1}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v1

    const/4 v3, 0x2

    const/4 v6, 0x3

    if-eqz v1, :cond_2a

    if-nez v9, :cond_27

    const-string v1, "subscription"

    const-string v8, "subscriptions"

    .line 124
    invoke-static {v1, v8}, Lorg/onepf/oms/appstore/fortumoUtils/InappsXMLParser;->inWrongNode(Ljava/lang/String;Ljava/lang/String;)V

    :cond_27
    const-string v1, "period"

    const/4 v8, 0x0

    .line 126
    invoke-interface {v0, v8, v1}, Lorg/xmlpull/v1/XmlPullParser;->getAttributeValue(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v11

    const-string v1, "oneMonth"

    .line 127
    invoke-virtual {v1, v11}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v1

    if-nez v1, :cond_29

    const-string v1, "oneYear"

    invoke-virtual {v1, v11}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v1

    if-eqz v1, :cond_28

    goto :goto_9

    .line 128
    :cond_28
    new-instance v0, Ljava/lang/IllegalStateException;

    new-array v1, v6, [Ljava/lang/Object;

    const/4 v2, 0x0

    aput-object v11, v1, v2

    const-string v2, "oneMonth"

    const/4 v4, 0x1

    aput-object v2, v1, v4

    const-string v2, "oneYear"

    aput-object v2, v1, v3

    const-string v2, "Wrong \"period\" value: %s. Must be \"%s\" or \"%s\"."

    invoke-static {v2, v1}, Ljava/lang/String;->format(Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v1

    invoke-direct {v0, v1}, Ljava/lang/IllegalStateException;-><init>(Ljava/lang/String;)V

    throw v0

    :cond_29
    :goto_9
    const/4 v13, 0x1

    goto :goto_a

    :cond_2a
    if-nez v10, :cond_2b

    const-string v1, "items"

    const-string v8, "items"

    .line 134
    invoke-static {v1, v8}, Lorg/onepf/oms/appstore/fortumoUtils/InappsXMLParser;->inWrongNode(Ljava/lang/String;Ljava/lang/String;)V

    :cond_2b
    const/4 v12, 0x1

    .line 138
    :goto_a
    new-instance v1, Lorg/onepf/oms/appstore/fortumoUtils/InappBaseProduct;

    invoke-direct {v1}, Lorg/onepf/oms/appstore/fortumoUtils/InappBaseProduct;-><init>()V

    const-string v8, "id"

    const/4 v3, 0x0

    .line 139
    invoke-interface {v0, v3, v8}, Lorg/xmlpull/v1/XmlPullParser;->getAttributeValue(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v8

    .line 140
    sget-object v6, Lorg/onepf/oms/appstore/fortumoUtils/InappsXMLParser;->skuPattern:Ljava/util/regex/Pattern;

    invoke-virtual {v6, v8}, Ljava/util/regex/Pattern;->matcher(Ljava/lang/CharSequence;)Ljava/util/regex/Matcher;

    move-result-object v6

    .line 141
    invoke-virtual {v6}, Ljava/util/regex/Matcher;->matches()Z

    move-result v6

    if-eqz v6, :cond_2e

    .line 144
    invoke-virtual {v1, v8}, Lorg/onepf/oms/appstore/fortumoUtils/InappBaseProduct;->setProductId(Ljava/lang/String;)V

    const-string v6, "publish-state"

    .line 145
    invoke-interface {v0, v3, v6}, Lorg/xmlpull/v1/XmlPullParser;->getAttributeValue(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v6

    const-string v8, "unpublished"

    .line 146
    invoke-virtual {v8, v6}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v8

    if-nez v8, :cond_2d

    const-string v8, "published"

    invoke-virtual {v8, v6}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v8

    if-eqz v8, :cond_2c

    goto :goto_b

    .line 147
    :cond_2c
    new-instance v0, Ljava/lang/IllegalStateException;

    const/4 v1, 0x3

    new-array v1, v1, [Ljava/lang/Object;

    const/4 v2, 0x0

    aput-object v6, v1, v2

    const-string v2, "unpublished"

    const/4 v8, 0x1

    aput-object v2, v1, v8

    const-string v2, "published"

    const/4 v3, 0x2

    aput-object v2, v1, v3

    const-string v2, "Wrong publish state value: %s. Must be \"%s\" or \"%s\""

    invoke-static {v2, v1}, Ljava/lang/String;->format(Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v1

    invoke-direct {v0, v1}, Ljava/lang/IllegalStateException;-><init>(Ljava/lang/String;)V

    throw v0

    :cond_2d
    :goto_b
    const/4 v8, 0x1

    .line 149
    invoke-virtual {v1, v6}, Lorg/onepf/oms/appstore/fortumoUtils/InappBaseProduct;->setPublished(Ljava/lang/String;)V

    move-object v8, v1

    move-object/from16 v17, v5

    move-object/from16 v20, v22

    move-object/from16 v18, v24

    const/4 v1, 0x1

    goto/16 :goto_3

    :cond_2e
    const/4 v1, 0x1

    .line 142
    new-instance v0, Ljava/lang/IllegalStateException;

    new-array v1, v1, [Ljava/lang/Object;

    const/4 v6, 0x0

    aput-object v8, v1, v6

    const-string v2, "Wrong SKU ID: %s. SKU must match \"([a-z]|[0-9]){1}[a-z0-9._]*\""

    invoke-static {v2, v1}, Ljava/lang/String;->format(Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v1

    invoke-direct {v0, v1}, Ljava/lang/IllegalStateException;-><init>(Ljava/lang/String;)V

    throw v0

    :goto_c
    move-object/from16 v17, v5

    move-object/from16 v20, v22

    move-object/from16 v18, v24

    .line 267
    :goto_d
    invoke-interface {v0}, Lorg/xmlpull/v1/XmlPullParser;->next()I

    move-result v5

    move-object/from16 v3, v23

    goto/16 :goto_0

    .line 270
    :cond_2f
    new-instance v0, Landroid/util/Pair;

    invoke-direct {v0, v2, v4}, Landroid/util/Pair;-><init>(Ljava/lang/Object;Ljava/lang/Object;)V

    return-object v0

    nop

    :pswitch_data_0
    .packed-switch 0x2
        :pswitch_2
        :pswitch_1
        :pswitch_0
    .end packed-switch
.end method
