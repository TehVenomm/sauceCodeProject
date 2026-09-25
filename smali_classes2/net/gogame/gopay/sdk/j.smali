.class public final Lnet/gogame/gopay/sdk/j;
.super Ljava/lang/Object;


# static fields
.field private static a:Ljava/lang/String; = null

.field private static b:Ljava/lang/String; = null

.field private static c:Ljava/lang/String; = null

.field private static d:Ljava/lang/String; = null

.field private static e:Ljava/lang/String; = null

.field private static f:Ljava/lang/String; = ""

.field private static g:Ljava/lang/String; = ""

.field private static h:Ljava/lang/String; = ""


# direct methods
.method static constructor <clinit>()V
    .locals 0

    return-void
.end method

.method public static a()Ljava/lang/String;
    .locals 1

    sget-object v0, Lnet/gogame/gopay/sdk/j;->a:Ljava/lang/String;

    return-object v0
.end method

.method private static a(Ljava/lang/String;Ljava/util/Map;)Ljava/lang/String;
    .locals 5

    if-nez p1, :cond_0

    new-instance p1, Ljava/util/HashMap;

    invoke-direct {p1}, Ljava/util/HashMap;-><init>()V

    :cond_0
    const-string v0, "sdk"

    const-string v1, "goPaySDK-android"

    invoke-interface {p1, v0, v1}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string v0, "sdk_version"

    const-string v1, "1.1.7"

    invoke-interface {p1, v0, v1}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string v0, "lang"

    invoke-static {}, Ljava/util/Locale;->getDefault()Ljava/util/Locale;

    move-result-object v1

    invoke-virtual {v1}, Ljava/util/Locale;->getISO3Language()Ljava/lang/String;

    move-result-object v1

    invoke-interface {p1, v0, v1}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string v0, "tz"

    invoke-static {}, Ljava/util/TimeZone;->getDefault()Ljava/util/TimeZone;

    move-result-object v1

    invoke-virtual {v1}, Ljava/util/TimeZone;->getID()Ljava/lang/String;

    move-result-object v1

    invoke-interface {p1, v0, v1}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string v0, "game_version"

    sget-object v1, Lnet/gogame/gopay/sdk/j;->h:Ljava/lang/String;

    invoke-interface {p1, v0, v1}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    sget-object v0, Lnet/gogame/gopay/sdk/j;->f:Ljava/lang/String;

    if-eqz v0, :cond_1

    const-string v0, "device_id"

    sget-object v1, Lnet/gogame/gopay/sdk/j;->f:Ljava/lang/String;

    invoke-interface {p1, v0, v1}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    :cond_1
    sget-object v0, Lnet/gogame/gopay/sdk/j;->g:Ljava/lang/String;

    if-eqz v0, :cond_2

    const-string v0, "bundle_id"

    sget-object v1, Lnet/gogame/gopay/sdk/j;->g:Ljava/lang/String;

    invoke-interface {p1, v0, v1}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    :cond_2
    sget-object v0, Lnet/gogame/gopay/sdk/j;->e:Ljava/lang/String;

    if-eqz v0, :cond_3

    sget-object v0, Lnet/gogame/gopay/sdk/j;->e:Ljava/lang/String;

    invoke-virtual {v0}, Ljava/lang/String;->length()I

    move-result v0

    if-lez v0, :cond_3

    const-string v0, "glang"

    sget-object v1, Lnet/gogame/gopay/sdk/j;->e:Ljava/lang/String;

    invoke-interface {p1, v0, v1}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    :cond_3
    const-string v0, "GET"

    const-string v1, ""

    const-string v2, "http"

    invoke-virtual {p0, v2}, Ljava/lang/String;->indexOf(Ljava/lang/String;)I

    move-result v2

    if-ltz v2, :cond_4

    const-string v1, "http"

    :cond_4
    const-string v2, "https"

    invoke-virtual {p0, v2}, Ljava/lang/String;->indexOf(Ljava/lang/String;)I

    move-result v2

    if-ltz v2, :cond_5

    const-string v1, "https"

    :cond_5
    const-string v2, "http://"

    const-string v3, ""

    invoke-virtual {p0, v2, v3}, Ljava/lang/String;->replace(Ljava/lang/CharSequence;Ljava/lang/CharSequence;)Ljava/lang/String;

    move-result-object v2

    const-string v3, "https://"

    const-string v4, ""

    invoke-virtual {v2, v3, v4}, Ljava/lang/String;->replace(Ljava/lang/CharSequence;Ljava/lang/CharSequence;)Ljava/lang/String;

    move-result-object v2

    new-instance v3, Ljava/lang/StringBuilder;

    invoke-direct {v3}, Ljava/lang/StringBuilder;-><init>()V

    invoke-virtual {v3, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v3, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v3, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v3}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v0

    if-eqz p1, :cond_6

    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    invoke-virtual {v1, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-static {p1}, Lnet/gogame/gopay/sdk/j;->a(Ljava/util/Map;)Ljava/lang/String;

    move-result-object v0

    invoke-virtual {v1, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v0

    :cond_6
    sget-object v1, Lnet/gogame/gopay/sdk/j;->c:Ljava/lang/String;

    const-string v2, "HmacSHA256"

    invoke-static {v0, v1, v2}, Lnet/gogame/gopay/sdk/j;->c(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v0

    sput-object v0, Lnet/gogame/gopay/sdk/j;->b:Ljava/lang/String;

    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    invoke-virtual {v0, p0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string p0, "?"

    invoke-virtual {v0, p0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-static {p1}, Lnet/gogame/gopay/sdk/j;->a(Ljava/util/Map;)Ljava/lang/String;

    move-result-object p0

    invoke-virtual {v0, p0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p0

    return-object p0
.end method

.method private static a(Ljava/util/Map;)Ljava/lang/String;
    .locals 5

    const-string v0, ""

    invoke-interface {p0}, Ljava/util/Map;->entrySet()Ljava/util/Set;

    move-result-object p0

    invoke-interface {p0}, Ljava/util/Set;->iterator()Ljava/util/Iterator;

    move-result-object p0

    :catch_0
    :goto_0
    invoke-interface {p0}, Ljava/util/Iterator;->hasNext()Z

    move-result v1

    if-eqz v1, :cond_1

    invoke-interface {p0}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Ljava/util/Map$Entry;

    invoke-virtual {v0}, Ljava/lang/String;->isEmpty()Z

    move-result v2

    if-nez v2, :cond_0

    new-instance v2, Ljava/lang/StringBuilder;

    invoke-direct {v2}, Ljava/lang/StringBuilder;-><init>()V

    invoke-virtual {v2, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v0, "&"

    invoke-virtual {v2, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v2}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v0

    :cond_0
    :try_start_0
    new-instance v2, Ljava/lang/StringBuilder;

    invoke-direct {v2}, Ljava/lang/StringBuilder;-><init>()V

    invoke-virtual {v2, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-interface {v1}, Ljava/util/Map$Entry;->getKey()Ljava/lang/Object;

    move-result-object v3

    check-cast v3, Ljava/lang/String;

    const-string v4, "UTF-8"

    invoke-static {v3, v4}, Ljava/net/URLEncoder;->encode(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v3

    invoke-virtual {v2, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v3, "="

    invoke-virtual {v2, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-interface {v1}, Ljava/util/Map$Entry;->getValue()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Ljava/lang/String;

    const-string v3, "UTF-8"

    invoke-static {v1, v3}, Ljava/net/URLEncoder;->encode(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v2, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v2}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    move-object v0, v1

    goto :goto_0

    :cond_1
    return-object v0
.end method

.method private static a([B)Ljava/lang/String;
    .locals 7

    new-instance v0, Ljava/lang/StringBuilder;

    array-length v1, p0

    mul-int/lit8 v1, v1, 0x2

    invoke-direct {v0, v1}, Ljava/lang/StringBuilder;-><init>(I)V

    array-length v1, p0

    const/4 v2, 0x0

    const/4 v3, 0x0

    :goto_0
    if-ge v3, v1, :cond_0

    aget-byte v4, p0, v3

    const-string v5, "%02x"

    const/4 v6, 0x1

    new-array v6, v6, [Ljava/lang/Object;

    and-int/lit16 v4, v4, 0xff

    invoke-static {v4}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v4

    aput-object v4, v6, v2

    invoke-static {v5, v6}, Ljava/lang/String;->format(Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v4

    invoke-virtual {v0, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    add-int/lit8 v3, v3, 0x1

    goto :goto_0

    :cond_0
    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p0

    return-object p0
.end method

.method private static a(Lorg/json/JSONArray;)Ljava/util/List;
    .locals 7

    invoke-virtual {p0}, Lorg/json/JSONArray;->length()I

    move-result v0

    if-lez v0, :cond_1

    new-instance v1, Ljava/util/ArrayList;

    invoke-direct {v1}, Ljava/util/ArrayList;-><init>()V

    const/4 v2, 0x0

    :goto_0
    if-ge v2, v0, :cond_0

    invoke-virtual {p0, v2}, Lorg/json/JSONArray;->getJSONObject(I)Lorg/json/JSONObject;

    move-result-object v3

    const-string v4, "code"

    invoke-virtual {v3, v4}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v4

    const-string v5, "name"

    invoke-virtual {v3, v5}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v5

    const-string v6, "displayIcon"

    invoke-virtual {v3, v6}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v3

    new-instance v6, Lnet/gogame/gopay/sdk/Country;

    invoke-direct {v6, v4, v5, v3}, Lnet/gogame/gopay/sdk/Country;-><init>(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V

    invoke-interface {v1, v6}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    add-int/lit8 v2, v2, 0x1

    goto :goto_0

    :cond_0
    invoke-static {v1}, Ljava/util/Collections;->unmodifiableList(Ljava/util/List;)Ljava/util/List;

    move-result-object p0

    goto :goto_1

    :cond_1
    const/4 p0, 0x0

    :goto_1
    return-object p0
.end method

.method private static a(Lorg/json/JSONObject;)Lnet/gogame/gopay/sdk/f;
    .locals 4
    .param p0    # Lorg/json/JSONObject;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param

    new-instance v0, Lnet/gogame/gopay/sdk/f;

    const-string v1, "statusCode"

    invoke-virtual {p0, v1}, Lorg/json/JSONObject;->getInt(Ljava/lang/String;)I

    move-result v1

    const-string v2, "status"

    invoke-virtual {p0, v2}, Lorg/json/JSONObject;->getBoolean(Ljava/lang/String;)Z

    move-result v2

    const-string v3, "statusMsg"

    invoke-virtual {p0, v3}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object p0

    invoke-direct {v0, v1, v2, p0}, Lnet/gogame/gopay/sdk/f;-><init>(IZLjava/lang/String;)V

    return-object v0
.end method

.method public static a(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Z)Lnet/gogame/gopay/sdk/g;
    .locals 2
    .param p0    # Ljava/lang/String;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param
    .param p2    # Ljava/lang/String;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param

    :try_start_0
    new-instance v0, Ljava/util/HashMap;

    invoke-direct {v0}, Ljava/util/HashMap;-><init>()V

    const-string v1, "appId"

    invoke-interface {v0, v1, p0}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    if-eqz p1, :cond_0

    const-string p0, "guid"

    invoke-interface {v0, p0, p1}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    :cond_0
    const-string p0, "product_id"

    invoke-interface {v0, p0, p2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    if-eqz p3, :cond_1

    const-string p0, "country"

    invoke-interface {v0, p0, p3}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    :cond_1
    if-eqz p4, :cond_2

    const-string p0, "requestCountries"

    const-string p1, "1"

    invoke-interface {v0, p0, p1}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    :cond_2
    const-string p0, "https://gp-api.gogame.net/billing/v2/get_product_details/"

    invoke-static {p0, v0}, Lnet/gogame/gopay/sdk/j;->a(Ljava/lang/String;Ljava/util/Map;)Ljava/lang/String;

    move-result-object p0

    invoke-static {p0}, Lnet/gogame/gopay/sdk/j;->j(Ljava/lang/String;)Lorg/json/JSONObject;

    move-result-object p0

    invoke-static {p0}, Lnet/gogame/gopay/sdk/j;->f(Lorg/json/JSONObject;)Lnet/gogame/gopay/sdk/g;

    move-result-object p0
    :try_end_0
    .catch Ljava/io/IOException; {:try_start_0 .. :try_end_0} :catch_1
    .catch Lorg/json/JSONException; {:try_start_0 .. :try_end_0} :catch_0

    return-object p0

    :catch_0
    move-exception p0

    new-instance p1, Lorg/onepf/oms/appstore/googleUtils/IabException;

    const/16 p2, -0x3ea

    const-string p3, "Bad response"

    invoke-direct {p1, p2, p3, p0}, Lorg/onepf/oms/appstore/googleUtils/IabException;-><init>(ILjava/lang/String;Ljava/lang/Exception;)V

    throw p1

    :catch_1
    move-exception p0

    new-instance p1, Lorg/onepf/oms/appstore/googleUtils/IabException;

    const/16 p2, -0x3f0

    invoke-virtual {p0}, Ljava/io/IOException;->getLocalizedMessage()Ljava/lang/String;

    move-result-object p3

    invoke-direct {p1, p2, p3, p0}, Lorg/onepf/oms/appstore/googleUtils/IabException;-><init>(ILjava/lang/String;Ljava/lang/Exception;)V

    throw p1
.end method

.method public static a(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)Lnet/gogame/gopay/sdk/h;
    .locals 2
    .param p0    # Ljava/lang/String;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param

    :try_start_0
    new-instance v0, Ljava/util/HashMap;

    invoke-direct {v0}, Ljava/util/HashMap;-><init>()V

    const-string v1, "appId"

    invoke-interface {v0, v1, p0}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    if-eqz p1, :cond_0

    const-string p0, "guid"

    invoke-interface {v0, p0, p1}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    :cond_0
    if-eqz p2, :cond_1

    const-string p0, "country"

    invoke-interface {v0, p0, p2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    :cond_1
    const-string p0, "requestCountries"

    const-string p1, "1"

    invoke-interface {v0, p0, p1}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string p0, "https://gp-api.gogame.net/billing/v2/get_sku_details/"

    invoke-static {p0, v0}, Lnet/gogame/gopay/sdk/j;->a(Ljava/lang/String;Ljava/util/Map;)Ljava/lang/String;

    move-result-object p0

    invoke-static {p0}, Lnet/gogame/gopay/sdk/j;->j(Ljava/lang/String;)Lorg/json/JSONObject;

    move-result-object p0

    invoke-static {p0}, Lnet/gogame/gopay/sdk/j;->e(Lorg/json/JSONObject;)Lnet/gogame/gopay/sdk/h;

    move-result-object p0
    :try_end_0
    .catch Ljava/io/IOException; {:try_start_0 .. :try_end_0} :catch_1
    .catch Lorg/json/JSONException; {:try_start_0 .. :try_end_0} :catch_0

    return-object p0

    :catch_0
    move-exception p0

    new-instance p1, Lorg/onepf/oms/appstore/googleUtils/IabException;

    const/16 p2, -0x3ea

    const-string v0, "Bad response"

    invoke-direct {p1, p2, v0, p0}, Lorg/onepf/oms/appstore/googleUtils/IabException;-><init>(ILjava/lang/String;Ljava/lang/Exception;)V

    throw p1

    :catch_1
    move-exception p0

    new-instance p1, Lorg/onepf/oms/appstore/googleUtils/IabException;

    const/16 p2, -0x3f0

    invoke-virtual {p0}, Ljava/io/IOException;->getLocalizedMessage()Ljava/lang/String;

    move-result-object v0

    invoke-direct {p1, p2, v0, p0}, Lorg/onepf/oms/appstore/googleUtils/IabException;-><init>(ILjava/lang/String;Ljava/lang/Exception;)V

    throw p1
.end method

.method public static a(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)Lnet/gogame/gopay/sdk/n;
    .locals 3
    .param p0    # Ljava/lang/String;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param
    .param p1    # Ljava/lang/String;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param
    .param p2    # Ljava/lang/String;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param
    .param p3    # Ljava/lang/String;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param

    const/16 v0, -0x3ea

    :try_start_0
    new-instance v1, Ljava/util/HashMap;

    invoke-direct {v1}, Ljava/util/HashMap;-><init>()V

    const-string v2, "appId"

    invoke-interface {v1, v2, p0}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string p0, "guid"

    invoke-interface {v1, p0, p1}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string p0, "product_id"

    invoke-interface {v1, p0, p2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string p0, "payment_method"

    invoke-interface {v1, p0, p3}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    if-eqz p4, :cond_0

    const-string p0, "country"

    invoke-interface {v1, p0, p4}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    :cond_0
    if-eqz p5, :cond_1

    const-string p0, "payload"

    invoke-interface {v1, p0, p5}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    :cond_1
    const-string p0, "https://gp-api.gogame.net/billing/v2/start_purchase/"

    invoke-static {p0, v1}, Lnet/gogame/gopay/sdk/j;->a(Ljava/lang/String;Ljava/util/Map;)Ljava/lang/String;

    move-result-object p0

    invoke-static {p0}, Lnet/gogame/gopay/sdk/j;->j(Ljava/lang/String;)Lorg/json/JSONObject;

    move-result-object p0

    invoke-static {p0}, Lnet/gogame/gopay/sdk/j;->g(Lorg/json/JSONObject;)Lnet/gogame/gopay/sdk/n;

    move-result-object p0
    :try_end_0
    .catch Ljava/io/IOException; {:try_start_0 .. :try_end_0} :catch_2
    .catch Lorg/json/JSONException; {:try_start_0 .. :try_end_0} :catch_1
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    return-object p0

    :catch_0
    move-exception p0

    new-instance p1, Lorg/onepf/oms/appstore/googleUtils/IabException;

    const-string p2, "Bad response"

    invoke-direct {p1, v0, p2, p0}, Lorg/onepf/oms/appstore/googleUtils/IabException;-><init>(ILjava/lang/String;Ljava/lang/Exception;)V

    throw p1

    :catch_1
    move-exception p0

    new-instance p1, Lorg/onepf/oms/appstore/googleUtils/IabException;

    const-string p2, "Bad response"

    invoke-direct {p1, v0, p2, p0}, Lorg/onepf/oms/appstore/googleUtils/IabException;-><init>(ILjava/lang/String;Ljava/lang/Exception;)V

    throw p1

    :catch_2
    move-exception p0

    new-instance p1, Lorg/onepf/oms/appstore/googleUtils/IabException;

    const/16 p2, -0x3f0

    invoke-virtual {p0}, Ljava/io/IOException;->getLocalizedMessage()Ljava/lang/String;

    move-result-object p3

    invoke-direct {p1, p2, p3, p0}, Lorg/onepf/oms/appstore/googleUtils/IabException;-><init>(ILjava/lang/String;Ljava/lang/Exception;)V

    throw p1
.end method

.method public static a(Ljava/lang/String;Ljava/lang/String;Lorg/onepf/oms/appstore/googleUtils/SkuDetails;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)Lnet/gogame/gopay/sdk/n;
    .locals 3
    .param p0    # Ljava/lang/String;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param
    .param p1    # Ljava/lang/String;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param
    .param p2    # Lorg/onepf/oms/appstore/googleUtils/SkuDetails;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param
    .param p3    # Ljava/lang/String;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param
    .param p4    # Ljava/lang/String;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param
    .param p5    # Ljava/lang/String;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param

    const/16 v0, -0x3ea

    :try_start_0
    new-instance v1, Ljava/util/HashMap;

    invoke-direct {v1}, Ljava/util/HashMap;-><init>()V

    const-string v2, "appId"

    invoke-interface {v1, v2, p0}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string p0, "guid"

    invoke-interface {v1, p0, p1}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string p0, "product_id"

    invoke-virtual {p2}, Lorg/onepf/oms/appstore/googleUtils/SkuDetails;->getSku()Ljava/lang/String;

    move-result-object p1

    invoke-interface {v1, p0, p1}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string p0, "payment_method"

    invoke-interface {v1, p0, p5}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    if-eqz p6, :cond_0

    const-string p0, "country"

    invoke-interface {v1, p0, p6}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    :cond_0
    if-eqz p7, :cond_1

    const-string p0, "payload"

    invoke-interface {v1, p0, p7}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    :cond_1
    if-eqz p2, :cond_4

    invoke-virtual {p2}, Lorg/onepf/oms/appstore/googleUtils/SkuDetails;->getTitle()Ljava/lang/String;

    move-result-object p0

    if-eqz p0, :cond_2

    const-string p0, "title"

    invoke-virtual {p2}, Lorg/onepf/oms/appstore/googleUtils/SkuDetails;->getTitle()Ljava/lang/String;

    move-result-object p1

    invoke-interface {v1, p0, p1}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    :cond_2
    invoke-virtual {p2}, Lorg/onepf/oms/appstore/googleUtils/SkuDetails;->getDescription()Ljava/lang/String;

    move-result-object p0

    if-eqz p0, :cond_3

    const-string p0, "description"

    invoke-virtual {p2}, Lorg/onepf/oms/appstore/googleUtils/SkuDetails;->getDescription()Ljava/lang/String;

    move-result-object p1

    invoke-interface {v1, p0, p1}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    :cond_3
    invoke-virtual {p2}, Lorg/onepf/oms/appstore/googleUtils/SkuDetails;->getPrice()Ljava/lang/String;

    move-result-object p0

    if-eqz p0, :cond_4

    const-string p0, "price"

    invoke-virtual {p2}, Lorg/onepf/oms/appstore/googleUtils/SkuDetails;->getPrice()Ljava/lang/String;

    move-result-object p1

    invoke-interface {v1, p0, p1}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    :cond_4
    if-eqz p3, :cond_5

    const-string p0, "price_amount_micros"

    invoke-interface {v1, p0, p3}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    :cond_5
    if-eqz p4, :cond_6

    const-string p0, "price_currency_code"

    invoke-interface {v1, p0, p4}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    :cond_6
    const-string p0, "https://gp-api.gogame.net/billing/v2/start_purchase/"

    invoke-static {p0, v1}, Lnet/gogame/gopay/sdk/j;->a(Ljava/lang/String;Ljava/util/Map;)Ljava/lang/String;

    move-result-object p0

    invoke-static {p0}, Lnet/gogame/gopay/sdk/j;->j(Ljava/lang/String;)Lorg/json/JSONObject;

    move-result-object p0

    invoke-static {p0}, Lnet/gogame/gopay/sdk/j;->g(Lorg/json/JSONObject;)Lnet/gogame/gopay/sdk/n;

    move-result-object p0
    :try_end_0
    .catch Ljava/io/IOException; {:try_start_0 .. :try_end_0} :catch_2
    .catch Lorg/json/JSONException; {:try_start_0 .. :try_end_0} :catch_1
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    return-object p0

    :catch_0
    move-exception p0

    new-instance p1, Lorg/onepf/oms/appstore/googleUtils/IabException;

    const-string p2, "Bad response"

    invoke-direct {p1, v0, p2, p0}, Lorg/onepf/oms/appstore/googleUtils/IabException;-><init>(ILjava/lang/String;Ljava/lang/Exception;)V

    throw p1

    :catch_1
    move-exception p0

    new-instance p1, Lorg/onepf/oms/appstore/googleUtils/IabException;

    const-string p2, "Bad response"

    invoke-direct {p1, v0, p2, p0}, Lorg/onepf/oms/appstore/googleUtils/IabException;-><init>(ILjava/lang/String;Ljava/lang/Exception;)V

    throw p1

    :catch_2
    move-exception p0

    new-instance p1, Lorg/onepf/oms/appstore/googleUtils/IabException;

    const/16 p2, -0x3f0

    invoke-virtual {p0}, Ljava/io/IOException;->getLocalizedMessage()Ljava/lang/String;

    move-result-object p3

    invoke-direct {p1, p2, p3, p0}, Lorg/onepf/oms/appstore/googleUtils/IabException;-><init>(ILjava/lang/String;Ljava/lang/Exception;)V

    throw p1
.end method

.method public static a(Ljava/lang/String;)V
    .locals 0

    sput-object p0, Lnet/gogame/gopay/sdk/j;->a:Ljava/lang/String;

    return-void
.end method

.method public static a(Ljava/lang/String;Ljava/lang/String;)V
    .locals 2
    .param p0    # Ljava/lang/String;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param
    .param p1    # Ljava/lang/String;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param

    :try_start_0
    new-instance v0, Ljava/util/HashMap;

    invoke-direct {v0}, Ljava/util/HashMap;-><init>()V

    const-string v1, "appId"

    invoke-interface {v0, v1, p0}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string p0, "guid"

    invoke-interface {v0, p0, p1}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    sget-object p0, Lnet/gogame/gopay/sdk/j;->d:Ljava/lang/String;

    if-eqz p0, :cond_0

    sget-object p0, Lnet/gogame/gopay/sdk/j;->c:Ljava/lang/String;
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_1

    if-eqz p0, :cond_0

    :try_start_1
    const-string p0, "uid"

    sget-object p1, Lnet/gogame/gopay/sdk/j;->c:Ljava/lang/String;

    sget-object v1, Lnet/gogame/gopay/sdk/j;->d:Ljava/lang/String;

    invoke-static {p1, v1}, Lnet/gogame/gopay/sdk/support/IOUtils;->encrypt(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object p1

    invoke-interface {v0, p0, p1}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;
    :try_end_1
    .catch Ljava/lang/Exception; {:try_start_1 .. :try_end_1} :catch_0

    goto :goto_0

    :catch_0
    move-exception p0

    :try_start_2
    invoke-virtual {p0}, Ljava/lang/Exception;->printStackTrace()V

    :cond_0
    :goto_0
    const-string p0, "https://gp-api.gogame.net/billing/v2/status/"

    invoke-static {p0, v0}, Lnet/gogame/gopay/sdk/j;->a(Ljava/lang/String;Ljava/util/Map;)Ljava/lang/String;

    move-result-object p0

    invoke-static {p0}, Lnet/gogame/gopay/sdk/j;->j(Ljava/lang/String;)Lorg/json/JSONObject;
    :try_end_2
    .catch Ljava/lang/Exception; {:try_start_2 .. :try_end_2} :catch_1

    return-void

    :catch_1
    move-exception p0

    invoke-virtual {p0}, Ljava/lang/Exception;->printStackTrace()V

    return-void
.end method

.method public static b(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)Lnet/gogame/gopay/sdk/GetCountryDetailsResponse;
    .locals 2
    .param p0    # Ljava/lang/String;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param

    :try_start_0
    new-instance v0, Ljava/util/HashMap;

    invoke-direct {v0}, Ljava/util/HashMap;-><init>()V

    const-string v1, "appId"

    invoke-interface {v0, v1, p0}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    if-eqz p1, :cond_0

    const-string p0, "guid"

    invoke-interface {v0, p0, p1}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    :cond_0
    if-eqz p2, :cond_1

    const-string p0, "product_id"

    invoke-interface {v0, p0, p2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    :cond_1
    const-string p0, "https://gp-api.gogame.net/billing/v2/get_country_details/"

    invoke-static {p0, v0}, Lnet/gogame/gopay/sdk/j;->a(Ljava/lang/String;Ljava/util/Map;)Ljava/lang/String;

    move-result-object p0

    invoke-static {p0}, Lnet/gogame/gopay/sdk/j;->j(Ljava/lang/String;)Lorg/json/JSONObject;

    move-result-object p0

    const-string p1, "country"

    invoke-virtual {p0, p1}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object p1

    const-string p2, "countries"

    invoke-virtual {p0, p2}, Lorg/json/JSONObject;->getJSONArray(Ljava/lang/String;)Lorg/json/JSONArray;

    move-result-object p2

    invoke-static {p2}, Lnet/gogame/gopay/sdk/j;->a(Lorg/json/JSONArray;)Ljava/util/List;

    move-result-object p2

    const-string v0, "baseUrls"

    invoke-virtual {p0, v0}, Lorg/json/JSONObject;->getJSONObject(Ljava/lang/String;)Lorg/json/JSONObject;

    move-result-object p0

    invoke-static {p0}, Lnet/gogame/gopay/sdk/j;->h(Lorg/json/JSONObject;)Ljava/util/Map;

    move-result-object p0

    new-instance v0, Lnet/gogame/gopay/sdk/GetCountryDetailsResponse;

    invoke-direct {v0, p1, p2, p0}, Lnet/gogame/gopay/sdk/GetCountryDetailsResponse;-><init>(Ljava/lang/String;Ljava/util/List;Ljava/util/Map;)V
    :try_end_0
    .catch Ljava/io/IOException; {:try_start_0 .. :try_end_0} :catch_1
    .catch Lorg/json/JSONException; {:try_start_0 .. :try_end_0} :catch_0

    return-object v0

    :catch_0
    move-exception p0

    new-instance p1, Lorg/onepf/oms/appstore/googleUtils/IabException;

    const/16 p2, -0x3ea

    const-string v0, "Bad response"

    invoke-direct {p1, p2, v0, p0}, Lorg/onepf/oms/appstore/googleUtils/IabException;-><init>(ILjava/lang/String;Ljava/lang/Exception;)V

    throw p1

    :catch_1
    move-exception p0

    new-instance p1, Lorg/onepf/oms/appstore/googleUtils/IabException;

    const/16 p2, -0x3f0

    invoke-virtual {p0}, Ljava/io/IOException;->getLocalizedMessage()Ljava/lang/String;

    move-result-object v0

    invoke-direct {p1, p2, v0, p0}, Lorg/onepf/oms/appstore/googleUtils/IabException;-><init>(ILjava/lang/String;Ljava/lang/Exception;)V

    throw p1
.end method

.method public static b()Lnet/gogame/gopay/sdk/iab/bu;
    .locals 6

    const/4 v0, 0x0

    :try_start_0
    const-string v1, "https://gp-api.gogame.net/billing/v2/get_assets_path/"

    invoke-static {v1, v0}, Lnet/gogame/gopay/sdk/j;->a(Ljava/lang/String;Ljava/util/Map;)Ljava/lang/String;

    move-result-object v1

    invoke-static {v1}, Lnet/gogame/gopay/sdk/j;->j(Ljava/lang/String;)Lorg/json/JSONObject;

    move-result-object v1

    new-instance v2, Lnet/gogame/gopay/sdk/iab/bu;

    const-string v3, "version"

    invoke-virtual {v1, v3}, Lorg/json/JSONObject;->getDouble(Ljava/lang/String;)D

    move-result-wide v3

    double-to-float v3, v3

    const-string v4, "file"

    invoke-virtual {v1, v4}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v4

    const-string v5, "skip"

    invoke-virtual {v1, v5}, Lorg/json/JSONObject;->getBoolean(Ljava/lang/String;)Z

    move-result v1

    invoke-direct {v2, v3, v4, v1}, Lnet/gogame/gopay/sdk/iab/bu;-><init>(FLjava/lang/String;Z)V
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    return-object v2

    :catch_0
    move-exception v1

    invoke-virtual {v1}, Ljava/lang/Exception;->printStackTrace()V

    return-object v0
.end method

.method private static b(Lorg/json/JSONObject;)Lorg/onepf/oms/appstore/googleUtils/SkuDetails;
    .locals 7
    .param p0    # Lorg/json/JSONObject;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param
    .annotation build Lorg/jetbrains/annotations/NotNull;
    .end annotation

    const-string v0, "product_id"

    invoke-virtual {p0, v0}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v3

    const-string v0, "price"

    invoke-virtual {p0, v0}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v5

    const-string v0, "title"

    invoke-virtual {p0, v0}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v4

    const-string v0, "description"

    invoke-virtual {p0, v0}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v6

    const-string v0, "type"

    invoke-virtual {p0, v0}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v2

    new-instance p0, Lorg/onepf/oms/appstore/googleUtils/SkuDetails;

    move-object v1, p0

    invoke-direct/range {v1 .. v6}, Lorg/onepf/oms/appstore/googleUtils/SkuDetails;-><init>(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V

    return-object p0
.end method

.method public static b(Ljava/lang/String;)V
    .locals 0

    sput-object p0, Lnet/gogame/gopay/sdk/j;->c:Ljava/lang/String;

    return-void
.end method

.method private static c(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;
    .locals 2

    const-string v0, ""

    :try_start_0
    new-instance v1, Ljavax/crypto/spec/SecretKeySpec;

    invoke-virtual {p1}, Ljava/lang/String;->getBytes()[B

    move-result-object p1

    invoke-direct {v1, p1, p2}, Ljavax/crypto/spec/SecretKeySpec;-><init>([BLjava/lang/String;)V

    invoke-static {p2}, Ljavax/crypto/Mac;->getInstance(Ljava/lang/String;)Ljavax/crypto/Mac;

    move-result-object p1

    invoke-virtual {p1, v1}, Ljavax/crypto/Mac;->init(Ljava/security/Key;)V

    invoke-virtual {p0}, Ljava/lang/String;->getBytes()[B

    move-result-object p0

    invoke-virtual {p1, p0}, Ljavax/crypto/Mac;->doFinal([B)[B

    move-result-object p0

    invoke-static {p0}, Lnet/gogame/gopay/sdk/j;->a([B)Ljava/lang/String;

    move-result-object p0
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-object p0, v0

    :goto_0
    return-object p0
.end method

.method private static c(Lorg/json/JSONObject;)Lnet/gogame/gopay/sdk/k;
    .locals 12
    .param p0    # Lorg/json/JSONObject;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param
    .annotation build Lorg/jetbrains/annotations/NotNull;
    .end annotation

    const-string v0, "id"

    invoke-virtual {p0, v0}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v2

    const-string v0, "name"

    invoke-virtual {p0, v0}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v3

    const-string v0, "displayName"

    invoke-virtual {p0, v0}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v4

    const-string v0, "displayIcon"

    invoke-virtual {p0, v0}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v5

    new-instance v6, Ljava/util/ArrayList;

    invoke-direct {v6}, Ljava/util/ArrayList;-><init>()V

    const-string v0, "paymentMethods"

    invoke-virtual {p0, v0}, Lorg/json/JSONObject;->getJSONArray(Ljava/lang/String;)Lorg/json/JSONArray;

    move-result-object p0

    invoke-virtual {p0}, Lorg/json/JSONArray;->length()I

    move-result v0

    const/4 v1, 0x0

    :goto_0
    if-ge v1, v0, :cond_0

    invoke-virtual {p0, v1}, Lorg/json/JSONArray;->getJSONObject(I)Lorg/json/JSONObject;

    move-result-object v7

    const-string v8, "id"

    invoke-virtual {v7, v8}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v8

    const-string v9, "name"

    invoke-virtual {v7, v9}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v9

    const-string v10, "displayName"

    invoke-virtual {v7, v10}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v10

    const-string v11, "displayIcon"

    invoke-virtual {v7, v11}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v7

    new-instance v11, Lnet/gogame/gopay/sdk/l;

    invoke-direct {v11, v8, v9, v10, v7}, Lnet/gogame/gopay/sdk/l;-><init>(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V

    invoke-interface {v6, v11}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    add-int/lit8 v1, v1, 0x1

    goto :goto_0

    :cond_0
    new-instance p0, Lnet/gogame/gopay/sdk/k;

    move-object v1, p0

    invoke-direct/range {v1 .. v6}, Lnet/gogame/gopay/sdk/k;-><init>(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/util/List;)V

    return-object p0
.end method

.method public static c(Ljava/lang/String;)V
    .locals 0

    sput-object p0, Lnet/gogame/gopay/sdk/j;->d:Ljava/lang/String;

    return-void
.end method

.method private static d(Lorg/json/JSONObject;)Lnet/gogame/gopay/sdk/c;
    .locals 1
    .annotation build Lorg/jetbrains/annotations/NotNull;
    .end annotation

    const-string v0, "disableCountrySelection"

    invoke-virtual {p0, v0}, Lorg/json/JSONObject;->has(Ljava/lang/String;)Z

    move-result v0

    if-eqz v0, :cond_0

    const-string v0, "disableCountrySelection"

    invoke-virtual {p0, v0}, Lorg/json/JSONObject;->getBoolean(Ljava/lang/String;)Z

    move-result p0

    goto :goto_0

    :cond_0
    const/4 p0, 0x0

    :goto_0
    new-instance v0, Lnet/gogame/gopay/sdk/c;

    invoke-direct {v0, p0}, Lnet/gogame/gopay/sdk/c;-><init>(Z)V

    return-object v0
.end method

.method public static d(Ljava/lang/String;)V
    .locals 0

    sput-object p0, Lnet/gogame/gopay/sdk/j;->f:Ljava/lang/String;

    return-void
.end method

.method private static e(Lorg/json/JSONObject;)Lnet/gogame/gopay/sdk/h;
    .locals 8
    .param p0    # Lorg/json/JSONObject;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param
    .annotation build Lorg/jetbrains/annotations/NotNull;
    .end annotation

    const-string v0, "country"

    invoke-virtual {p0, v0}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v2

    new-instance v3, Ljava/util/ArrayList;

    invoke-direct {v3}, Ljava/util/ArrayList;-><init>()V

    const-string v0, "items"

    invoke-virtual {p0, v0}, Lorg/json/JSONObject;->getJSONArray(Ljava/lang/String;)Lorg/json/JSONArray;

    move-result-object v0

    const/4 v1, 0x0

    :goto_0
    invoke-virtual {v0}, Lorg/json/JSONArray;->length()I

    move-result v4

    if-ge v1, v4, :cond_0

    invoke-virtual {v0, v1}, Lorg/json/JSONArray;->getJSONObject(I)Lorg/json/JSONObject;

    move-result-object v4

    invoke-static {v4}, Lnet/gogame/gopay/sdk/j;->b(Lorg/json/JSONObject;)Lorg/onepf/oms/appstore/googleUtils/SkuDetails;

    move-result-object v4

    invoke-interface {v3, v4}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    add-int/lit8 v1, v1, 0x1

    goto :goto_0

    :cond_0
    const-string v0, "countries"

    invoke-virtual {p0, v0}, Lorg/json/JSONObject;->has(Ljava/lang/String;)Z

    move-result v0

    const/4 v1, 0x0

    if-eqz v0, :cond_1

    const-string v0, "countries"

    invoke-virtual {p0, v0}, Lorg/json/JSONObject;->getJSONArray(Ljava/lang/String;)Lorg/json/JSONArray;

    move-result-object v0

    invoke-static {v0}, Lnet/gogame/gopay/sdk/j;->a(Lorg/json/JSONArray;)Ljava/util/List;

    move-result-object v0

    move-object v4, v0

    goto :goto_1

    :cond_1
    move-object v4, v1

    :goto_1
    const-string v0, "baseUrls"

    invoke-virtual {p0, v0}, Lorg/json/JSONObject;->has(Ljava/lang/String;)Z

    move-result v0

    if-eqz v0, :cond_2

    const-string v0, "baseUrls"

    invoke-virtual {p0, v0}, Lorg/json/JSONObject;->getJSONObject(Ljava/lang/String;)Lorg/json/JSONObject;

    move-result-object v0

    invoke-static {v0}, Lnet/gogame/gopay/sdk/j;->h(Lorg/json/JSONObject;)Ljava/util/Map;

    move-result-object v0

    move-object v5, v0

    goto :goto_2

    :cond_2
    move-object v5, v1

    :goto_2
    const-string v0, "config"

    invoke-virtual {p0, v0}, Lorg/json/JSONObject;->has(Ljava/lang/String;)Z

    move-result v0

    if-eqz v0, :cond_3

    const-string v0, "config"

    invoke-virtual {p0, v0}, Lorg/json/JSONObject;->getJSONObject(Ljava/lang/String;)Lorg/json/JSONObject;

    move-result-object v0

    invoke-static {v0}, Lnet/gogame/gopay/sdk/j;->d(Lorg/json/JSONObject;)Lnet/gogame/gopay/sdk/c;

    move-result-object v0

    move-object v6, v0

    goto :goto_3

    :cond_3
    move-object v6, v1

    :goto_3
    const-string v0, "redirectUrl"

    invoke-virtual {p0, v0}, Lorg/json/JSONObject;->has(Ljava/lang/String;)Z

    move-result v0

    if-eqz v0, :cond_4

    const-string v0, "redirectUrl"

    invoke-virtual {p0, v0}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object p0

    move-object v7, p0

    goto :goto_4

    :cond_4
    move-object v7, v1

    :goto_4
    new-instance p0, Lnet/gogame/gopay/sdk/h;

    move-object v1, p0

    invoke-direct/range {v1 .. v7}, Lnet/gogame/gopay/sdk/h;-><init>(Ljava/lang/String;Ljava/util/List;Ljava/util/List;Ljava/util/Map;Lnet/gogame/gopay/sdk/c;Ljava/lang/String;)V

    return-object p0
.end method

.method public static e(Ljava/lang/String;)V
    .locals 0

    sput-object p0, Lnet/gogame/gopay/sdk/j;->g:Ljava/lang/String;

    return-void
.end method

.method private static f(Lorg/json/JSONObject;)Lnet/gogame/gopay/sdk/g;
    .locals 10
    .param p0    # Lorg/json/JSONObject;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param
    .annotation build Lorg/jetbrains/annotations/NotNull;
    .end annotation

    const-string v0, "status"

    invoke-virtual {p0, v0}, Lorg/json/JSONObject;->getBoolean(Ljava/lang/String;)Z

    move-result v0

    if-nez v0, :cond_0

    new-instance v0, Lnet/gogame/gopay/sdk/g;

    invoke-static {p0}, Lnet/gogame/gopay/sdk/j;->a(Lorg/json/JSONObject;)Lnet/gogame/gopay/sdk/f;

    move-result-object p0

    invoke-direct {v0, p0}, Lnet/gogame/gopay/sdk/g;-><init>(Lnet/gogame/gopay/sdk/f;)V

    return-object v0

    :cond_0
    const-string v0, "country"

    invoke-virtual {p0, v0}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v2

    const-string v0, "currencyCode"

    invoke-virtual {p0, v0}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v3

    const-string v0, "item"

    invoke-virtual {p0, v0}, Lorg/json/JSONObject;->getJSONObject(Ljava/lang/String;)Lorg/json/JSONObject;

    move-result-object v0

    invoke-static {v0}, Lnet/gogame/gopay/sdk/j;->b(Lorg/json/JSONObject;)Lorg/onepf/oms/appstore/googleUtils/SkuDetails;

    move-result-object v4

    new-instance v5, Ljava/util/ArrayList;

    invoke-direct {v5}, Ljava/util/ArrayList;-><init>()V

    const-string v0, "paymentTypes"

    invoke-virtual {p0, v0}, Lorg/json/JSONObject;->getJSONArray(Ljava/lang/String;)Lorg/json/JSONArray;

    move-result-object v0

    invoke-virtual {v0}, Lorg/json/JSONArray;->length()I

    move-result v1

    const/4 v6, 0x0

    :goto_0
    if-ge v6, v1, :cond_1

    invoke-virtual {v0, v6}, Lorg/json/JSONArray;->getJSONObject(I)Lorg/json/JSONObject;

    move-result-object v7

    invoke-static {v7}, Lnet/gogame/gopay/sdk/j;->c(Lorg/json/JSONObject;)Lnet/gogame/gopay/sdk/k;

    move-result-object v7

    invoke-interface {v5, v7}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    add-int/lit8 v6, v6, 0x1

    goto :goto_0

    :cond_1
    const-string v0, "baseUrls"

    invoke-virtual {p0, v0}, Lorg/json/JSONObject;->getJSONObject(Ljava/lang/String;)Lorg/json/JSONObject;

    move-result-object v0

    invoke-static {v0}, Lnet/gogame/gopay/sdk/j;->h(Lorg/json/JSONObject;)Ljava/util/Map;

    move-result-object v6

    const-string v0, "countries"

    invoke-virtual {p0, v0}, Lorg/json/JSONObject;->has(Ljava/lang/String;)Z

    move-result v0

    const/4 v1, 0x0

    if-eqz v0, :cond_2

    const-string v0, "countries"

    invoke-virtual {p0, v0}, Lorg/json/JSONObject;->getJSONArray(Ljava/lang/String;)Lorg/json/JSONArray;

    move-result-object v0

    invoke-static {v0}, Lnet/gogame/gopay/sdk/j;->a(Lorg/json/JSONArray;)Ljava/util/List;

    move-result-object v0

    move-object v7, v0

    goto :goto_1

    :cond_2
    move-object v7, v1

    :goto_1
    const-string v0, "config"

    invoke-virtual {p0, v0}, Lorg/json/JSONObject;->has(Ljava/lang/String;)Z

    move-result v0

    if-eqz v0, :cond_3

    const-string v0, "config"

    invoke-virtual {p0, v0}, Lorg/json/JSONObject;->getJSONObject(Ljava/lang/String;)Lorg/json/JSONObject;

    move-result-object v0

    invoke-static {v0}, Lnet/gogame/gopay/sdk/j;->d(Lorg/json/JSONObject;)Lnet/gogame/gopay/sdk/c;

    move-result-object v0

    move-object v8, v0

    goto :goto_2

    :cond_3
    move-object v8, v1

    :goto_2
    const-string v0, "redirectUrl"

    invoke-virtual {p0, v0}, Lorg/json/JSONObject;->has(Ljava/lang/String;)Z

    move-result v0

    if-eqz v0, :cond_4

    const-string v0, "redirectUrl"

    invoke-virtual {p0, v0}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object p0

    move-object v9, p0

    goto :goto_3

    :cond_4
    move-object v9, v1

    :goto_3
    new-instance p0, Lnet/gogame/gopay/sdk/g;

    move-object v1, p0

    invoke-direct/range {v1 .. v9}, Lnet/gogame/gopay/sdk/g;-><init>(Ljava/lang/String;Ljava/lang/String;Lorg/onepf/oms/appstore/googleUtils/SkuDetails;Ljava/util/List;Ljava/util/Map;Ljava/util/List;Lnet/gogame/gopay/sdk/c;Ljava/lang/String;)V

    return-object p0
.end method

.method public static f(Ljava/lang/String;)V
    .locals 0

    sput-object p0, Lnet/gogame/gopay/sdk/j;->h:Ljava/lang/String;

    return-void
.end method

.method private static g(Lorg/json/JSONObject;)Lnet/gogame/gopay/sdk/n;
    .locals 2
    .param p0    # Lorg/json/JSONObject;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param
    .annotation build Lorg/jetbrains/annotations/NotNull;
    .end annotation

    const-string v0, "status"

    invoke-virtual {p0, v0}, Lorg/json/JSONObject;->getBoolean(Ljava/lang/String;)Z

    move-result v0

    if-nez v0, :cond_0

    new-instance v0, Lnet/gogame/gopay/sdk/n;

    invoke-static {p0}, Lnet/gogame/gopay/sdk/j;->a(Lorg/json/JSONObject;)Lnet/gogame/gopay/sdk/f;

    move-result-object p0

    invoke-direct {v0, p0}, Lnet/gogame/gopay/sdk/n;-><init>(Lnet/gogame/gopay/sdk/f;)V

    return-object v0

    :cond_0
    new-instance v0, Lnet/gogame/gopay/sdk/n;

    const-string v1, "redirectUrl"

    invoke-virtual {p0, v1}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v1

    invoke-direct {v0, v1, p0}, Lnet/gogame/gopay/sdk/n;-><init>(Ljava/lang/String;Lorg/json/JSONObject;)V

    return-object v0
.end method

.method public static g(Ljava/lang/String;)V
    .locals 0

    sput-object p0, Lnet/gogame/gopay/sdk/j;->e:Ljava/lang/String;

    return-void
.end method

.method private static h(Ljava/lang/String;)Ljava/net/HttpURLConnection;
    .locals 3

    const/16 v0, -0x3e9

    :try_start_0
    new-instance v1, Ljava/net/URL;

    invoke-direct {v1, p0}, Ljava/net/URL;-><init>(Ljava/lang/String;)V

    invoke-virtual {v1}, Ljava/net/URL;->openConnection()Ljava/net/URLConnection;

    move-result-object p0

    check-cast p0, Ljava/net/HttpURLConnection;

    const-string v1, "Go-Token"

    sget-object v2, Lnet/gogame/gopay/sdk/j;->b:Ljava/lang/String;

    invoke-virtual {p0, v1, v2}, Ljava/net/HttpURLConnection;->addRequestProperty(Ljava/lang/String;Ljava/lang/String;)V

    const-string v1, "http.keepAlive"

    const-string v2, "false"

    invoke-virtual {p0, v1, v2}, Ljava/net/HttpURLConnection;->setRequestProperty(Ljava/lang/String;Ljava/lang/String;)V

    const-string v1, "GET"

    invoke-virtual {p0, v1}, Ljava/net/HttpURLConnection;->setRequestMethod(Ljava/lang/String;)V

    const/16 v1, 0x1388

    invoke-virtual {p0, v1}, Ljava/net/HttpURLConnection;->setConnectTimeout(I)V

    invoke-virtual {p0, v1}, Ljava/net/HttpURLConnection;->setReadTimeout(I)V

    const/4 v1, 0x1

    invoke-virtual {p0, v1}, Ljava/net/HttpURLConnection;->setDoInput(Z)V

    invoke-virtual {p0}, Ljava/net/HttpURLConnection;->connect()V
    :try_end_0
    .catch Ljava/net/SocketTimeoutException; {:try_start_0 .. :try_end_0} :catch_3
    .catch Ljava/net/ConnectException; {:try_start_0 .. :try_end_0} :catch_2
    .catch Ljava/io/IOException; {:try_start_0 .. :try_end_0} :catch_1
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    return-object p0

    :catch_0
    move-exception p0

    new-instance v0, Lorg/onepf/oms/appstore/googleUtils/IabException;

    const/16 v1, -0x3f0

    invoke-virtual {p0}, Ljava/lang/Exception;->getLocalizedMessage()Ljava/lang/String;

    move-result-object v2

    invoke-direct {v0, v1, v2, p0}, Lorg/onepf/oms/appstore/googleUtils/IabException;-><init>(ILjava/lang/String;Ljava/lang/Exception;)V

    throw v0

    :catch_1
    move-exception p0

    new-instance v1, Lorg/onepf/oms/appstore/googleUtils/IabException;

    const-string v2, "I/O exception"

    invoke-direct {v1, v0, v2, p0}, Lorg/onepf/oms/appstore/googleUtils/IabException;-><init>(ILjava/lang/String;Ljava/lang/Exception;)V

    throw v1

    :catch_2
    move-exception p0

    new-instance v1, Lorg/onepf/oms/appstore/googleUtils/IabException;

    const-string v2, "Connection failed"

    invoke-direct {v1, v0, v2, p0}, Lorg/onepf/oms/appstore/googleUtils/IabException;-><init>(ILjava/lang/String;Ljava/lang/Exception;)V

    throw v1

    :catch_3
    move-exception p0

    new-instance v1, Lorg/onepf/oms/appstore/googleUtils/IabException;

    const-string v2, "Timeout"

    invoke-direct {v1, v0, v2, p0}, Lorg/onepf/oms/appstore/googleUtils/IabException;-><init>(ILjava/lang/String;Ljava/lang/Exception;)V

    throw v1
.end method

.method private static h(Lorg/json/JSONObject;)Ljava/util/Map;
    .locals 4
    .param p0    # Lorg/json/JSONObject;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param
    .annotation build Lorg/jetbrains/annotations/NotNull;
    .end annotation

    new-instance v0, Ljava/util/HashMap;

    invoke-direct {v0}, Ljava/util/HashMap;-><init>()V

    invoke-virtual {p0}, Lorg/json/JSONObject;->keys()Ljava/util/Iterator;

    move-result-object v1

    :goto_0
    invoke-interface {v1}, Ljava/util/Iterator;->hasNext()Z

    move-result v2

    if-eqz v2, :cond_0

    invoke-interface {v1}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Ljava/lang/String;

    invoke-virtual {p0, v2}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v3

    invoke-interface {v0, v2, v3}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    goto :goto_0

    :cond_0
    return-object v0
.end method

.method private static i(Ljava/lang/String;)Ljava/lang/String;
    .locals 5

    const/16 v0, -0x3e9

    :try_start_0
    invoke-static {p0}, Lnet/gogame/gopay/sdk/j;->h(Ljava/lang/String;)Ljava/net/HttpURLConnection;

    move-result-object p0

    new-instance v1, Ljava/lang/StringBuilder;

    const-string v2, "<< "

    invoke-direct {v1, v2}, Ljava/lang/StringBuilder;-><init>(Ljava/lang/String;)V

    invoke-virtual {p0}, Ljava/net/HttpURLConnection;->getResponseCode()I

    move-result v2

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    const-string v2, " "

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p0}, Ljava/net/HttpURLConnection;->getResponseMessage()Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p0}, Ljava/net/HttpURLConnection;->getResponseCode()I

    move-result v1
    :try_end_0
    .catch Ljava/net/SocketTimeoutException; {:try_start_0 .. :try_end_0} :catch_3
    .catch Ljava/net/ConnectException; {:try_start_0 .. :try_end_0} :catch_2
    .catch Ljava/io/IOException; {:try_start_0 .. :try_end_0} :catch_1
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    const/16 v2, 0xc8

    if-ne v1, v2, :cond_2

    const/4 v1, 0x0

    :try_start_1
    invoke-virtual {p0}, Ljava/net/HttpURLConnection;->getInputStream()Ljava/io/InputStream;

    move-result-object p0
    :try_end_1
    .catchall {:try_start_1 .. :try_end_1} :catchall_1

    :try_start_2
    const-string v1, "UTF-8"

    invoke-static {p0, v1}, Lnet/gogame/gopay/sdk/support/IOUtils;->readString(Ljava/io/InputStream;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v1
    :try_end_2
    .catchall {:try_start_2 .. :try_end_2} :catchall_0

    if-eqz p0, :cond_0

    :try_start_3
    invoke-virtual {p0}, Ljava/io/InputStream;->close()V

    :cond_0
    return-object v1

    :catchall_0
    move-exception v1

    goto :goto_0

    :catchall_1
    move-exception p0

    move-object v4, v1

    move-object v1, p0

    move-object p0, v4

    :goto_0
    if-eqz p0, :cond_1

    invoke-virtual {p0}, Ljava/io/InputStream;->close()V

    :cond_1
    throw v1

    :cond_2
    new-instance v1, Lorg/onepf/oms/appstore/googleUtils/IabException;

    new-instance v2, Ljava/lang/StringBuilder;

    invoke-direct {v2}, Ljava/lang/StringBuilder;-><init>()V

    invoke-virtual {p0}, Ljava/net/HttpURLConnection;->getResponseCode()I

    move-result v3

    invoke-virtual {v2, v3}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    const-string v3, " "

    invoke-virtual {v2, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p0}, Ljava/net/HttpURLConnection;->getResponseMessage()Ljava/lang/String;

    move-result-object p0

    invoke-virtual {v2, p0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v2}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p0

    invoke-direct {v1, v0, p0}, Lorg/onepf/oms/appstore/googleUtils/IabException;-><init>(ILjava/lang/String;)V

    throw v1
    :try_end_3
    .catch Ljava/net/SocketTimeoutException; {:try_start_3 .. :try_end_3} :catch_3
    .catch Ljava/net/ConnectException; {:try_start_3 .. :try_end_3} :catch_2
    .catch Ljava/io/IOException; {:try_start_3 .. :try_end_3} :catch_1
    .catch Ljava/lang/Exception; {:try_start_3 .. :try_end_3} :catch_0

    :catch_0
    move-exception p0

    new-instance v0, Lorg/onepf/oms/appstore/googleUtils/IabException;

    const/16 v1, -0x3f0

    invoke-virtual {p0}, Ljava/lang/Exception;->getLocalizedMessage()Ljava/lang/String;

    move-result-object v2

    invoke-direct {v0, v1, v2, p0}, Lorg/onepf/oms/appstore/googleUtils/IabException;-><init>(ILjava/lang/String;Ljava/lang/Exception;)V

    throw v0

    :catch_1
    move-exception p0

    new-instance v1, Lorg/onepf/oms/appstore/googleUtils/IabException;

    const-string v2, "I/O exception"

    invoke-direct {v1, v0, v2, p0}, Lorg/onepf/oms/appstore/googleUtils/IabException;-><init>(ILjava/lang/String;Ljava/lang/Exception;)V

    throw v1

    :catch_2
    move-exception p0

    new-instance v1, Lorg/onepf/oms/appstore/googleUtils/IabException;

    const-string v2, "Connection failed"

    invoke-direct {v1, v0, v2, p0}, Lorg/onepf/oms/appstore/googleUtils/IabException;-><init>(ILjava/lang/String;Ljava/lang/Exception;)V

    throw v1

    :catch_3
    move-exception p0

    new-instance v1, Lorg/onepf/oms/appstore/googleUtils/IabException;

    const-string v2, "Timeout"

    invoke-direct {v1, v0, v2, p0}, Lorg/onepf/oms/appstore/googleUtils/IabException;-><init>(ILjava/lang/String;Ljava/lang/Exception;)V

    throw v1
.end method

.method private static j(Ljava/lang/String;)Lorg/json/JSONObject;
    .locals 3

    :try_start_0
    invoke-static {p0}, Lnet/gogame/gopay/sdk/j;->i(Ljava/lang/String;)Ljava/lang/String;

    move-result-object p0

    new-instance v0, Lorg/json/JSONObject;

    invoke-direct {v0, p0}, Lorg/json/JSONObject;-><init>(Ljava/lang/String;)V
    :try_end_0
    .catch Lorg/json/JSONException; {:try_start_0 .. :try_end_0} :catch_0

    return-object v0

    :catch_0
    move-exception p0

    new-instance v0, Lorg/onepf/oms/appstore/googleUtils/IabException;

    const/16 v1, -0x3ea

    const-string v2, "Bad response"

    invoke-direct {v0, v1, v2, p0}, Lorg/onepf/oms/appstore/googleUtils/IabException;-><init>(ILjava/lang/String;Ljava/lang/Exception;)V

    throw v0
.end method
