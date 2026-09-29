.class public Lorg/onepf/oms/appstore/googleUtils/SkuDetails;
.super Ljava/lang/Object;
.source "SkuDetails.java"


# instance fields
.field mDescription:Ljava/lang/String;

.field mItemType:Ljava/lang/String;

.field mJson:Ljava/lang/String;

.field mPrice:Ljava/lang/String;

.field mSku:Ljava/lang/String;

.field mTitle:Ljava/lang/String;

.field mType:Ljava/lang/String;


# direct methods
.method public constructor <init>(Ljava/lang/String;)V
    .locals 1
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Lorg/json/JSONException;
        }
    .end annotation

    const-string v0, "inapp"

    .line 35
    invoke-direct {p0, v0, p1}, Lorg/onepf/oms/appstore/googleUtils/SkuDetails;-><init>(Ljava/lang/String;Ljava/lang/String;)V

    return-void
.end method

.method public constructor <init>(Ljava/lang/String;Ljava/lang/String;)V
    .locals 0
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Lorg/json/JSONException;
        }
    .end annotation

    .line 53
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 54
    iput-object p1, p0, Lorg/onepf/oms/appstore/googleUtils/SkuDetails;->mItemType:Ljava/lang/String;

    .line 55
    iput-object p2, p0, Lorg/onepf/oms/appstore/googleUtils/SkuDetails;->mJson:Ljava/lang/String;

    .line 56
    new-instance p1, Lorg/json/JSONObject;

    iget-object p2, p0, Lorg/onepf/oms/appstore/googleUtils/SkuDetails;->mJson:Ljava/lang/String;

    invoke-direct {p1, p2}, Lorg/json/JSONObject;-><init>(Ljava/lang/String;)V

    const-string p2, "productId"

    .line 57
    invoke-virtual {p1, p2}, Lorg/json/JSONObject;->optString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object p2

    iput-object p2, p0, Lorg/onepf/oms/appstore/googleUtils/SkuDetails;->mSku:Ljava/lang/String;

    const-string p2, "type"

    .line 58
    invoke-virtual {p1, p2}, Lorg/json/JSONObject;->optString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object p2

    iput-object p2, p0, Lorg/onepf/oms/appstore/googleUtils/SkuDetails;->mType:Ljava/lang/String;

    const-string p2, "price"

    .line 59
    invoke-virtual {p1, p2}, Lorg/json/JSONObject;->optString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object p2

    iput-object p2, p0, Lorg/onepf/oms/appstore/googleUtils/SkuDetails;->mPrice:Ljava/lang/String;

    const-string p2, "title"

    .line 60
    invoke-virtual {p1, p2}, Lorg/json/JSONObject;->optString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object p2

    iput-object p2, p0, Lorg/onepf/oms/appstore/googleUtils/SkuDetails;->mTitle:Ljava/lang/String;

    const-string p2, "description"

    .line 61
    invoke-virtual {p1, p2}, Lorg/json/JSONObject;->optString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object p1

    iput-object p1, p0, Lorg/onepf/oms/appstore/googleUtils/SkuDetails;->mDescription:Ljava/lang/String;

    return-void
.end method

.method public constructor <init>(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V
    .locals 1

    .line 38
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    const-string v0, "inapp"

    .line 39
    iput-object v0, p0, Lorg/onepf/oms/appstore/googleUtils/SkuDetails;->mItemType:Ljava/lang/String;

    .line 40
    iput-object p1, p0, Lorg/onepf/oms/appstore/googleUtils/SkuDetails;->mSku:Ljava/lang/String;

    .line 41
    iput-object p2, p0, Lorg/onepf/oms/appstore/googleUtils/SkuDetails;->mTitle:Ljava/lang/String;

    .line 42
    iput-object p3, p0, Lorg/onepf/oms/appstore/googleUtils/SkuDetails;->mPrice:Ljava/lang/String;

    return-void
.end method

.method public constructor <init>(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V
    .locals 0

    .line 45
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 46
    iput-object p1, p0, Lorg/onepf/oms/appstore/googleUtils/SkuDetails;->mItemType:Ljava/lang/String;

    .line 47
    iput-object p2, p0, Lorg/onepf/oms/appstore/googleUtils/SkuDetails;->mSku:Ljava/lang/String;

    .line 48
    iput-object p3, p0, Lorg/onepf/oms/appstore/googleUtils/SkuDetails;->mTitle:Ljava/lang/String;

    .line 49
    iput-object p4, p0, Lorg/onepf/oms/appstore/googleUtils/SkuDetails;->mPrice:Ljava/lang/String;

    .line 50
    iput-object p5, p0, Lorg/onepf/oms/appstore/googleUtils/SkuDetails;->mDescription:Ljava/lang/String;

    return-void
.end method


# virtual methods
.method public getDescription()Ljava/lang/String;
    .locals 1

    .line 81
    iget-object v0, p0, Lorg/onepf/oms/appstore/googleUtils/SkuDetails;->mDescription:Ljava/lang/String;

    return-object v0
.end method

.method public getItemType()Ljava/lang/String;
    .locals 1

    .line 85
    iget-object v0, p0, Lorg/onepf/oms/appstore/googleUtils/SkuDetails;->mItemType:Ljava/lang/String;

    return-object v0
.end method

.method public getJson()Ljava/lang/String;
    .locals 1

    .line 89
    iget-object v0, p0, Lorg/onepf/oms/appstore/googleUtils/SkuDetails;->mJson:Ljava/lang/String;

    return-object v0
.end method

.method public getPrice()Ljava/lang/String;
    .locals 1

    .line 73
    iget-object v0, p0, Lorg/onepf/oms/appstore/googleUtils/SkuDetails;->mPrice:Ljava/lang/String;

    return-object v0
.end method

.method public getSku()Ljava/lang/String;
    .locals 1

    .line 65
    iget-object v0, p0, Lorg/onepf/oms/appstore/googleUtils/SkuDetails;->mSku:Ljava/lang/String;

    return-object v0
.end method

.method public getTitle()Ljava/lang/String;
    .locals 1

    .line 77
    iget-object v0, p0, Lorg/onepf/oms/appstore/googleUtils/SkuDetails;->mTitle:Ljava/lang/String;

    return-object v0
.end method

.method public getType()Ljava/lang/String;
    .locals 1

    .line 69
    iget-object v0, p0, Lorg/onepf/oms/appstore/googleUtils/SkuDetails;->mType:Ljava/lang/String;

    return-object v0
.end method

.method public setSku(Ljava/lang/String;)V
    .locals 0

    .line 93
    iput-object p1, p0, Lorg/onepf/oms/appstore/googleUtils/SkuDetails;->mSku:Ljava/lang/String;

    return-void
.end method

.method public toString()Ljava/lang/String;
    .locals 4

    const-string v0, "SkuDetails: type = %s, SKU = %s, title = %s, price = %s, description = %s"

    const/4 v1, 0x5

    .line 98
    new-array v1, v1, [Ljava/lang/Object;

    iget-object v2, p0, Lorg/onepf/oms/appstore/googleUtils/SkuDetails;->mItemType:Ljava/lang/String;

    const/4 v3, 0x0

    aput-object v2, v1, v3

    iget-object v2, p0, Lorg/onepf/oms/appstore/googleUtils/SkuDetails;->mSku:Ljava/lang/String;

    const/4 v3, 0x1

    aput-object v2, v1, v3

    iget-object v2, p0, Lorg/onepf/oms/appstore/googleUtils/SkuDetails;->mTitle:Ljava/lang/String;

    const/4 v3, 0x2

    aput-object v2, v1, v3

    iget-object v2, p0, Lorg/onepf/oms/appstore/googleUtils/SkuDetails;->mPrice:Ljava/lang/String;

    const/4 v3, 0x3

    aput-object v2, v1, v3

    iget-object v2, p0, Lorg/onepf/oms/appstore/googleUtils/SkuDetails;->mDescription:Ljava/lang/String;

    const/4 v3, 0x4

    aput-object v2, v1, v3

    invoke-static {v0, v1}, Ljava/lang/String;->format(Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v0

    return-object v0
.end method
