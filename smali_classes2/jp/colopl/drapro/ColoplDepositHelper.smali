.class public Ljp/colopl/drapro/ColoplDepositHelper;
.super Ljava/lang/Object;
.source "ColoplDepositHelper.java"


# annotations
.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Ljp/colopl/drapro/ColoplDepositHelper$PostDepositFinishedListener;,
        Ljp/colopl/drapro/ColoplDepositHelper$PrepareDepositFinishedListener;,
        Ljp/colopl/drapro/ColoplDepositHelper$PostDepositResult;,
        Ljp/colopl/drapro/ColoplDepositHelper$PrepareResult;
    }
.end annotation


# static fields
.field private static final DEPO_PREF_NAME:Ljava/lang/String; = "depohelper"

.field private static final PURCHASE_JSON_KEY_ITEMTYPE:Ljava/lang/String; = "itemType"

.field private static final PURCHASE_JSON_KEY_ORGJSON:Ljava/lang/String; = "orgjson"

.field private static final PURCHASE_JSON_KEY_SIGNATURE:Ljava/lang/String; = "signature"


# instance fields
.field private final TAG:Ljava/lang/String;

.field private mActivity:Ljp/colopl/drapro/StartActivity;

.field private mDepositedPurchase:Landroid/content/SharedPreferences;


# direct methods
.method public constructor <init>(Ljp/colopl/drapro/StartActivity;)V
    .locals 2

    .line 153
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    const-string v0, "ColoplDeposit"

    .line 26
    iput-object v0, p0, Ljp/colopl/drapro/ColoplDepositHelper;->TAG:Ljava/lang/String;

    const/4 v0, 0x0

    .line 31
    iput-object v0, p0, Ljp/colopl/drapro/ColoplDepositHelper;->mDepositedPurchase:Landroid/content/SharedPreferences;

    .line 154
    iput-object p1, p0, Ljp/colopl/drapro/ColoplDepositHelper;->mActivity:Ljp/colopl/drapro/StartActivity;

    const-string v0, "depohelper"

    const/4 v1, 0x0

    .line 155
    invoke-virtual {p1, v0, v1}, Ljp/colopl/drapro/StartActivity;->getSharedPreferences(Ljava/lang/String;I)Landroid/content/SharedPreferences;

    move-result-object p1

    iput-object p1, p0, Ljp/colopl/drapro/ColoplDepositHelper;->mDepositedPurchase:Landroid/content/SharedPreferences;

    return-void
.end method

.method static synthetic access$000(Ljp/colopl/drapro/ColoplDepositHelper;)Ljp/colopl/drapro/StartActivity;
    .locals 0

    .line 25
    iget-object p0, p0, Ljp/colopl/drapro/ColoplDepositHelper;->mActivity:Ljp/colopl/drapro/StartActivity;

    return-object p0
.end method

.method public static getJsonStringFromPurchase(Ljp/colopl/iab/Purchase;)Ljava/lang/String;
    .locals 4

    .line 38
    invoke-virtual {p0}, Ljp/colopl/iab/Purchase;->getOriginalJson()Ljava/lang/String;

    move-result-object v0

    .line 39
    invoke-virtual {p0}, Ljp/colopl/iab/Purchase;->getItemType()Ljava/lang/String;

    move-result-object v1

    .line 40
    invoke-virtual {p0}, Ljp/colopl/iab/Purchase;->getSignature()Ljava/lang/String;

    move-result-object p0

    .line 41
    new-instance v2, Lorg/json/JSONObject;

    invoke-direct {v2}, Lorg/json/JSONObject;-><init>()V

    :try_start_0
    const-string v3, "itemType"

    .line 43
    invoke-virtual {v2, v3, v1}, Lorg/json/JSONObject;->put(Ljava/lang/String;Ljava/lang/Object;)Lorg/json/JSONObject;

    const-string v1, "signature"

    .line 44
    invoke-virtual {v2, v1, p0}, Lorg/json/JSONObject;->put(Ljava/lang/String;Ljava/lang/Object;)Lorg/json/JSONObject;

    const-string p0, "orgjson"

    .line 45
    invoke-virtual {v2, p0, v0}, Lorg/json/JSONObject;->put(Ljava/lang/String;Ljava/lang/Object;)Lorg/json/JSONObject;
    :try_end_0
    .catch Lorg/json/JSONException; {:try_start_0 .. :try_end_0} :catch_0

    .line 50
    invoke-virtual {v2}, Lorg/json/JSONObject;->toString()Ljava/lang/String;

    move-result-object p0

    return-object p0

    :catch_0
    move-exception p0

    .line 47
    invoke-virtual {p0}, Lorg/json/JSONException;->printStackTrace()V

    const/4 p0, 0x0

    return-object p0
.end method

.method public static getPurchaseFromJsonString(Ljava/lang/String;)Ljp/colopl/iab/Purchase;
    .locals 3

    .line 56
    :try_start_0
    new-instance v0, Lorg/json/JSONObject;

    invoke-direct {v0, p0}, Lorg/json/JSONObject;-><init>(Ljava/lang/String;)V

    const-string p0, "itemType"

    .line 57
    invoke-virtual {v0, p0}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object p0

    const-string v1, "signature"

    .line 58
    invoke-virtual {v0, v1}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v1

    const-string v2, "orgjson"

    .line 59
    invoke-virtual {v0, v2}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v0

    .line 60
    new-instance v2, Ljp/colopl/iab/Purchase;

    invoke-direct {v2, p0, v0, v1}, Ljp/colopl/iab/Purchase;-><init>(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V
    :try_end_0
    .catch Lorg/json/JSONException; {:try_start_0 .. :try_end_0} :catch_0

    return-object v2

    :catch_0
    move-exception p0

    .line 62
    invoke-virtual {p0}, Lorg/json/JSONException;->printStackTrace()V

    const/4 p0, 0x0

    return-object p0
.end method


# virtual methods
.method public addUndepositedPurchase(Ljp/colopl/iab/Purchase;)Z
    .locals 3

    const/4 v0, 0x0

    if-nez p1, :cond_0

    return v0

    .line 163
    :cond_0
    invoke-virtual {p1}, Ljp/colopl/iab/Purchase;->getOrderId()Ljava/lang/String;

    move-result-object v1

    .line 164
    invoke-static {p1}, Ljp/colopl/drapro/ColoplDepositHelper;->getJsonStringFromPurchase(Ljp/colopl/iab/Purchase;)Ljava/lang/String;

    move-result-object p1

    .line 165
    invoke-static {v1}, Landroid/text/TextUtils;->isEmpty(Ljava/lang/CharSequence;)Z

    move-result v2

    if-nez v2, :cond_2

    invoke-static {p1}, Landroid/text/TextUtils;->isEmpty(Ljava/lang/CharSequence;)Z

    move-result v2

    if-eqz v2, :cond_1

    goto :goto_0

    .line 170
    :cond_1
    iget-object v0, p0, Ljp/colopl/drapro/ColoplDepositHelper;->mDepositedPurchase:Landroid/content/SharedPreferences;

    invoke-interface {v0}, Landroid/content/SharedPreferences;->edit()Landroid/content/SharedPreferences$Editor;

    move-result-object v0

    invoke-interface {v0, v1, p1}, Landroid/content/SharedPreferences$Editor;->putString(Ljava/lang/String;Ljava/lang/String;)Landroid/content/SharedPreferences$Editor;

    move-result-object p1

    invoke-interface {p1}, Landroid/content/SharedPreferences$Editor;->commit()Z

    move-result p1

    return p1

    :cond_2
    :goto_0
    return v0
.end method

.method public getUndepositedPurchase()Ljava/util/ArrayList;
    .locals 4
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()",
            "Ljava/util/ArrayList<",
            "Ljp/colopl/iab/Purchase;",
            ">;"
        }
    .end annotation

    .line 199
    iget-object v0, p0, Ljp/colopl/drapro/ColoplDepositHelper;->mDepositedPurchase:Landroid/content/SharedPreferences;

    invoke-interface {v0}, Landroid/content/SharedPreferences;->getAll()Ljava/util/Map;

    move-result-object v0

    .line 200
    new-instance v1, Ljava/util/ArrayList;

    invoke-direct {v1}, Ljava/util/ArrayList;-><init>()V

    .line 201
    invoke-interface {v0}, Ljava/util/Map;->entrySet()Ljava/util/Set;

    move-result-object v0

    invoke-interface {v0}, Ljava/util/Set;->iterator()Ljava/util/Iterator;

    move-result-object v0

    :goto_0
    invoke-interface {v0}, Ljava/util/Iterator;->hasNext()Z

    move-result v2

    if-eqz v2, :cond_1

    invoke-interface {v0}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Ljava/util/Map$Entry;

    .line 202
    invoke-interface {v2}, Ljava/util/Map$Entry;->getValue()Ljava/lang/Object;

    move-result-object v3

    check-cast v3, Ljava/lang/String;

    .line 203
    invoke-static {v3}, Ljp/colopl/drapro/ColoplDepositHelper;->getPurchaseFromJsonString(Ljava/lang/String;)Ljp/colopl/iab/Purchase;

    move-result-object v3

    if-nez v3, :cond_0

    .line 205
    invoke-interface {v2}, Ljava/util/Map$Entry;->getKey()Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Ljava/lang/String;

    invoke-virtual {p0, v2}, Ljp/colopl/drapro/ColoplDepositHelper;->removeUndepositedPurchaseByOrderId(Ljava/lang/String;)Z

    goto :goto_0

    .line 208
    :cond_0
    invoke-virtual {v1, v3}, Ljava/util/ArrayList;->add(Ljava/lang/Object;)Z

    goto :goto_0

    :cond_1
    return-object v1
.end method

.method public postDepositAsync(Ljp/colopl/iab/Purchase;Ljp/colopl/drapro/ColoplDepositHelper$PostDepositFinishedListener;)V
    .locals 4

    .line 305
    new-instance v0, Ljava/util/ArrayList;

    const/4 v1, 0x3

    invoke-direct {v0, v1}, Ljava/util/ArrayList;-><init>(I)V

    .line 306
    new-instance v1, Lorg/apache/http/message/BasicNameValuePair;

    const-string v2, "mainToken"

    iget-object v3, p0, Ljp/colopl/drapro/ColoplDepositHelper;->mActivity:Ljp/colopl/drapro/StartActivity;

    invoke-virtual {v3}, Ljp/colopl/drapro/StartActivity;->getConfig()Ljp/colopl/config/Config;

    move-result-object v3

    invoke-virtual {v3}, Ljp/colopl/config/Config;->getSession()Ljp/colopl/config/Session;

    move-result-object v3

    invoke-virtual {v3}, Ljp/colopl/config/Session;->getSid()Ljava/lang/String;

    move-result-object v3

    invoke-direct {v1, v2, v3}, Lorg/apache/http/message/BasicNameValuePair;-><init>(Ljava/lang/String;Ljava/lang/String;)V

    invoke-interface {v0, v1}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    .line 307
    new-instance v1, Lorg/apache/http/message/BasicNameValuePair;

    const-string v2, "signedData"

    invoke-virtual {p1}, Ljp/colopl/iab/Purchase;->getOriginalJson()Ljava/lang/String;

    move-result-object v3

    invoke-direct {v1, v2, v3}, Lorg/apache/http/message/BasicNameValuePair;-><init>(Ljava/lang/String;Ljava/lang/String;)V

    invoke-interface {v0, v1}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    .line 308
    new-instance v1, Lorg/apache/http/message/BasicNameValuePair;

    const-string v2, "signature"

    invoke-virtual {p1}, Ljp/colopl/iab/Purchase;->getSignature()Ljava/lang/String;

    move-result-object v3

    invoke-direct {v1, v2, v3}, Lorg/apache/http/message/BasicNameValuePair;-><init>(Ljava/lang/String;Ljava/lang/String;)V

    invoke-interface {v0, v1}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    .line 309
    new-instance v1, Lorg/apache/http/message/BasicNameValuePair;

    const-string v2, "iabver"

    const-string v3, "3"

    invoke-direct {v1, v2, v3}, Lorg/apache/http/message/BasicNameValuePair;-><init>(Ljava/lang/String;Ljava/lang/String;)V

    invoke-interface {v0, v1}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    .line 310
    new-instance v1, Lorg/apache/http/message/BasicNameValuePair;

    const-string v2, "apv"

    iget-object v3, p0, Ljp/colopl/drapro/ColoplDepositHelper;->mActivity:Ljp/colopl/drapro/StartActivity;

    invoke-virtual {v3}, Ljp/colopl/drapro/StartActivity;->getConfig()Ljp/colopl/config/Config;

    move-result-object v3

    invoke-virtual {v3}, Ljp/colopl/config/Config;->getVersionCode()I

    move-result v3

    invoke-static {v3}, Ljava/lang/String;->valueOf(I)Ljava/lang/String;

    move-result-object v3

    invoke-direct {v1, v2, v3}, Lorg/apache/http/message/BasicNameValuePair;-><init>(Ljava/lang/String;Ljava/lang/String;)V

    invoke-interface {v0, v1}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    const-string v1, "ColoplDeposit"

    .line 311
    new-instance v2, Ljava/lang/StringBuilder;

    invoke-direct {v2}, Ljava/lang/StringBuilder;-><init>()V

    const-string v3, "[IABV3] "

    invoke-virtual {v2, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v2, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/Object;)Ljava/lang/StringBuilder;

    const-string v3, "  signature="

    invoke-virtual {v2, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    .line 312
    invoke-virtual {p1}, Ljp/colopl/iab/Purchase;->getSignature()Ljava/lang/String;

    move-result-object v3

    invoke-virtual {v2, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v3, " signedData="

    invoke-virtual {v2, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p1}, Ljp/colopl/iab/Purchase;->getOriginalJson()Ljava/lang/String;

    move-result-object v3

    invoke-virtual {v2, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v3, " apv="

    invoke-virtual {v2, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v3, p0, Ljp/colopl/drapro/ColoplDepositHelper;->mActivity:Ljp/colopl/drapro/StartActivity;

    .line 313
    invoke-virtual {v3}, Ljp/colopl/drapro/StartActivity;->getConfig()Ljp/colopl/config/Config;

    move-result-object v3

    invoke-virtual {v3}, Ljp/colopl/config/Config;->getVersionCode()I

    move-result v3

    invoke-static {v3}, Ljava/lang/String;->valueOf(I)Ljava/lang/String;

    move-result-object v3

    invoke-virtual {v2, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v3, "mainToken="

    invoke-virtual {v2, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v3, p0, Ljp/colopl/drapro/ColoplDepositHelper;->mActivity:Ljp/colopl/drapro/StartActivity;

    invoke-virtual {v3}, Ljp/colopl/drapro/StartActivity;->getConfig()Ljp/colopl/config/Config;

    move-result-object v3

    invoke-virtual {v3}, Ljp/colopl/config/Config;->getSession()Ljp/colopl/config/Session;

    move-result-object v3

    invoke-virtual {v3}, Ljp/colopl/config/Session;->getSid()Ljava/lang/String;

    move-result-object v3

    invoke-virtual {v2, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v2}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v2

    .line 311
    invoke-static {v1, v2}, Ljp/colopl/util/Util;->dLog(Ljava/lang/String;Ljava/lang/String;)V

    const-string v1, "/depo/post"

    .line 316
    invoke-static {v1}, Ljp/colopl/drapro/AnalyticsHelper;->trackPageView(Ljava/lang/String;)V

    .line 318
    invoke-static {}, Ljp/colopl/drapro/NetworkHelper;->getItemShopDepositUrl()Ljava/lang/String;

    move-result-object v1

    .line 319
    new-instance v2, Ljp/colopl/network/HttpPostAsyncTask;

    iget-object v3, p0, Ljp/colopl/drapro/ColoplDepositHelper;->mActivity:Ljp/colopl/drapro/StartActivity;

    invoke-direct {v2, v3, v1, v0}, Ljp/colopl/network/HttpPostAsyncTask;-><init>(Landroid/content/Context;Ljava/lang/String;Ljava/util/List;)V

    .line 320
    new-instance v0, Ljp/colopl/drapro/ColoplDepositHelper$2;

    invoke-direct {v0, p0, p1, p2}, Ljp/colopl/drapro/ColoplDepositHelper$2;-><init>(Ljp/colopl/drapro/ColoplDepositHelper;Ljp/colopl/iab/Purchase;Ljp/colopl/drapro/ColoplDepositHelper$PostDepositFinishedListener;)V

    invoke-virtual {v2, v0}, Ljp/colopl/network/HttpPostAsyncTask;->setListener(Ljp/colopl/network/HttpRequestListener;)V

    const/4 p1, 0x0

    .line 376
    new-array p1, p1, [Ljava/lang/Void;

    invoke-virtual {v2, p1}, Ljp/colopl/network/HttpPostAsyncTask;->execute([Ljava/lang/Object;)Landroid/os/AsyncTask;

    return-void
.end method

.method public prepareDepositAsync(Ljava/lang/String;Ljp/colopl/drapro/ColoplDepositHelper$PrepareDepositFinishedListener;)V
    .locals 6

    .line 227
    invoke-virtual {p1}, Ljava/lang/String;->toLowerCase()Ljava/lang/String;

    move-result-object p1

    .line 228
    iget-object v0, p0, Ljp/colopl/drapro/ColoplDepositHelper;->mActivity:Ljp/colopl/drapro/StartActivity;

    invoke-virtual {v0}, Ljp/colopl/drapro/StartActivity;->getPackageName()Ljava/lang/String;

    move-result-object v0

    .line 229
    invoke-static {}, Ljava/lang/System;->currentTimeMillis()J

    move-result-wide v1

    invoke-static {v1, v2}, Ljava/lang/String;->valueOf(J)Ljava/lang/String;

    move-result-object v1

    .line 230
    new-instance v2, Ljava/lang/StringBuilder;

    invoke-direct {v2}, Ljava/lang/StringBuilder;-><init>()V

    invoke-virtual {v2, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v0, ".g."

    invoke-virtual {v2, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v2}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v0

    invoke-virtual {p1, v0}, Ljava/lang/String;->startsWith(Ljava/lang/String;)Z

    move-result v0

    if-eqz v0, :cond_0

    .line 231
    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, ":"

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    sget-object v1, Ljp/colopl/drapro/InAppBillingHelper;->userId:Ljava/lang/String;

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    .line 234
    :cond_0
    new-instance v0, Ljava/util/ArrayList;

    const/4 v2, 0x3

    invoke-direct {v0, v2}, Ljava/util/ArrayList;-><init>(I)V

    .line 235
    new-instance v2, Lorg/apache/http/message/BasicNameValuePair;

    const-string v3, "mainToken"

    iget-object v4, p0, Ljp/colopl/drapro/ColoplDepositHelper;->mActivity:Ljp/colopl/drapro/StartActivity;

    invoke-virtual {v4}, Ljp/colopl/drapro/StartActivity;->getConfig()Ljp/colopl/config/Config;

    move-result-object v4

    invoke-virtual {v4}, Ljp/colopl/config/Config;->getSession()Ljp/colopl/config/Session;

    move-result-object v4

    invoke-virtual {v4}, Ljp/colopl/config/Session;->getSid()Ljava/lang/String;

    move-result-object v4

    invoke-direct {v2, v3, v4}, Lorg/apache/http/message/BasicNameValuePair;-><init>(Ljava/lang/String;Ljava/lang/String;)V

    invoke-interface {v0, v2}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    .line 236
    new-instance v2, Lorg/apache/http/message/BasicNameValuePair;

    const-string v3, "payload"

    invoke-direct {v2, v3, v1}, Lorg/apache/http/message/BasicNameValuePair;-><init>(Ljava/lang/String;Ljava/lang/String;)V

    invoke-interface {v0, v2}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    .line 237
    new-instance v2, Lorg/apache/http/message/BasicNameValuePair;

    const-string v3, "itemId"

    invoke-direct {v2, v3, p1}, Lorg/apache/http/message/BasicNameValuePair;-><init>(Ljava/lang/String;Ljava/lang/String;)V

    invoke-interface {v0, v2}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    .line 238
    invoke-static {}, Ljp/colopl/drapro/NetworkHelper;->getItemShopRequestUrl()Ljava/lang/String;

    move-result-object v2

    const-string v3, "ColoplDeposit"

    .line 240
    new-instance v4, Ljava/lang/StringBuilder;

    invoke-direct {v4}, Ljava/lang/StringBuilder;-><init>()V

    const-string v5, "[IABV3] prepareDepositAsync, requesting url: "

    invoke-virtual {v4, v5}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v4, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v5, ", mainToken: "

    invoke-virtual {v4, v5}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v5, p0, Ljp/colopl/drapro/ColoplDepositHelper;->mActivity:Ljp/colopl/drapro/StartActivity;

    .line 241
    invoke-virtual {v5}, Ljp/colopl/drapro/StartActivity;->getConfig()Ljp/colopl/config/Config;

    move-result-object v5

    invoke-virtual {v5}, Ljp/colopl/config/Config;->getSession()Ljp/colopl/config/Session;

    move-result-object v5

    invoke-virtual {v5}, Ljp/colopl/config/Session;->getSid()Ljava/lang/String;

    move-result-object v5

    invoke-virtual {v4, v5}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v5, ", payload: "

    invoke-virtual {v4, v5}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v4, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v5, ", itemId: "

    invoke-virtual {v4, v5}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v4, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v4}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v4

    .line 240
    invoke-static {v3, v4}, Landroid/util/Log;->i(Ljava/lang/String;Ljava/lang/String;)I

    const-string v3, "/depo/pre"

    .line 244
    invoke-static {v3}, Ljp/colopl/drapro/AnalyticsHelper;->trackPageView(Ljava/lang/String;)V

    .line 246
    new-instance v3, Ljp/colopl/network/HttpPostAsyncTask;

    iget-object v4, p0, Ljp/colopl/drapro/ColoplDepositHelper;->mActivity:Ljp/colopl/drapro/StartActivity;

    invoke-direct {v3, v4, v2, v0}, Ljp/colopl/network/HttpPostAsyncTask;-><init>(Landroid/content/Context;Ljava/lang/String;Ljava/util/List;)V

    .line 247
    new-instance v0, Ljp/colopl/drapro/ColoplDepositHelper$1;

    invoke-direct {v0, p0, p1, v1, p2}, Ljp/colopl/drapro/ColoplDepositHelper$1;-><init>(Ljp/colopl/drapro/ColoplDepositHelper;Ljava/lang/String;Ljava/lang/String;Ljp/colopl/drapro/ColoplDepositHelper$PrepareDepositFinishedListener;)V

    invoke-virtual {v3, v0}, Ljp/colopl/network/HttpPostAsyncTask;->setListener(Ljp/colopl/network/HttpRequestListener;)V

    const/4 p1, 0x0

    .line 295
    new-array p1, p1, [Ljava/lang/Void;

    invoke-virtual {v3, p1}, Ljp/colopl/network/HttpPostAsyncTask;->execute([Ljava/lang/Object;)Landroid/os/AsyncTask;

    return-void
.end method

.method public removeUndepositedPurchase(Ljp/colopl/iab/Purchase;)Z
    .locals 2

    const/4 v0, 0x0

    if-nez p1, :cond_0

    return v0

    .line 190
    :cond_0
    invoke-virtual {p1}, Ljp/colopl/iab/Purchase;->getOrderId()Ljava/lang/String;

    move-result-object p1

    .line 191
    invoke-static {p1}, Landroid/text/TextUtils;->isEmpty(Ljava/lang/CharSequence;)Z

    move-result v1

    if-eqz v1, :cond_1

    return v0

    .line 194
    :cond_1
    invoke-virtual {p0, p1}, Ljp/colopl/drapro/ColoplDepositHelper;->removeUndepositedPurchaseByOrderId(Ljava/lang/String;)Z

    move-result p1

    return p1
.end method

.method public removeUndepositedPurchaseByOrderId(Ljava/lang/String;)Z
    .locals 1

    .line 176
    invoke-static {p1}, Landroid/text/TextUtils;->isEmpty(Ljava/lang/CharSequence;)Z

    move-result v0

    if-eqz v0, :cond_0

    const/4 p1, 0x0

    return p1

    .line 181
    :cond_0
    iget-object v0, p0, Ljp/colopl/drapro/ColoplDepositHelper;->mDepositedPurchase:Landroid/content/SharedPreferences;

    invoke-interface {v0}, Landroid/content/SharedPreferences;->edit()Landroid/content/SharedPreferences$Editor;

    move-result-object v0

    invoke-interface {v0, p1}, Landroid/content/SharedPreferences$Editor;->remove(Ljava/lang/String;)Landroid/content/SharedPreferences$Editor;

    move-result-object p1

    invoke-interface {p1}, Landroid/content/SharedPreferences$Editor;->commit()Z

    move-result p1

    return p1
.end method
