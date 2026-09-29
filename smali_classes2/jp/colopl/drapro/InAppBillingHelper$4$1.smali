.class Ljp/colopl/drapro/InAppBillingHelper$4$1;
.super Ljava/lang/Object;
.source "InAppBillingHelper.java"

# interfaces
.implements Ljp/colopl/network/HttpRequestListener;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Ljp/colopl/drapro/InAppBillingHelper$4;->onQueryInventoryFinished(Ljp/colopl/iab/IabResult;Ljp/colopl/iab/Inventory;)V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Ljp/colopl/drapro/InAppBillingHelper$4;

.field final synthetic val$pur:Ljp/colopl/iab/Purchase;


# direct methods
.method constructor <init>(Ljp/colopl/drapro/InAppBillingHelper$4;Ljp/colopl/iab/Purchase;)V
    .locals 0

    .line 358
    iput-object p1, p0, Ljp/colopl/drapro/InAppBillingHelper$4$1;->this$0:Ljp/colopl/drapro/InAppBillingHelper$4;

    iput-object p2, p0, Ljp/colopl/drapro/InAppBillingHelper$4$1;->val$pur:Ljp/colopl/iab/Purchase;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public onReceiveError(Ljp/colopl/network/HttpPostAsyncTask;Ljava/lang/Exception;)V
    .locals 2

    .line 379
    sget p1, Ljp/colopl/drapro/InAppBillingHelper;->requestCount:I

    add-int/lit8 p1, p1, -0x1

    sput p1, Ljp/colopl/drapro/InAppBillingHelper;->requestCount:I

    const-string p1, "InAppBillingHelper"

    .line 380
    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    const-string v1, "[PromoCode] Http request error : "

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p2}, Ljava/lang/Exception;->getMessage()Ljava/lang/String;

    move-result-object p2

    invoke-virtual {v0, p2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p2

    invoke-static {p1, p2}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;)I

    .line 381
    invoke-static {}, Ljp/colopl/drapro/InAppBillingHelper;->ConsumePromotionItems()V

    return-void
.end method

.method public onReceiveResponse(Ljp/colopl/network/HttpPostAsyncTask;Ljava/lang/String;)V
    .locals 2

    .line 361
    sget p1, Ljp/colopl/drapro/InAppBillingHelper;->requestCount:I

    add-int/lit8 p1, p1, -0x1

    sput p1, Ljp/colopl/drapro/InAppBillingHelper;->requestCount:I

    .line 364
    :try_start_0
    new-instance p1, Lorg/json/JSONObject;

    invoke-direct {p1, p2}, Lorg/json/JSONObject;-><init>(Ljava/lang/String;)V

    const-string p2, "InAppBillingHelper"

    .line 365
    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    const-string v1, "json string: "

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p1}, Lorg/json/JSONObject;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v0

    invoke-static {p2, v0}, Ljp/colopl/util/Util;->dLog(Ljava/lang/String;Ljava/lang/String;)V

    const-string p2, "code"

    .line 366
    invoke-virtual {p1, p2}, Lorg/json/JSONObject;->getInt(Ljava/lang/String;)I

    move-result p1

    const/16 p2, 0x64

    if-ne p1, p2, :cond_0

    .line 368
    sget-object p1, Ljp/colopl/drapro/InAppBillingHelper;->consumeList:Ljava/util/ArrayList;

    iget-object p2, p0, Ljp/colopl/drapro/InAppBillingHelper$4$1;->val$pur:Ljp/colopl/iab/Purchase;

    invoke-virtual {p1, p2}, Ljava/util/ArrayList;->add(Ljava/lang/Object;)Z
    :try_end_0
    .catch Lorg/json/JSONException; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception p1

    const-string p2, "InAppBillingHelper"

    .line 371
    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    const-string v1, "[IABV3] onReceiveResponse json recieve error! "

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p1}, Lorg/json/JSONException;->getMessage()Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v0

    invoke-static {p2, v0}, Ljp/colopl/util/Util;->dLog(Ljava/lang/String;Ljava/lang/String;)V

    .line 372
    invoke-virtual {p1}, Lorg/json/JSONException;->printStackTrace()V

    .line 374
    :cond_0
    :goto_0
    invoke-static {}, Ljp/colopl/drapro/InAppBillingHelper;->ConsumePromotionItems()V

    return-void
.end method
