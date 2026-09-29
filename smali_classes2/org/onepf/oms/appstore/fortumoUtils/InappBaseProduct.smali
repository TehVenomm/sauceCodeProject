.class public Lorg/onepf/oms/appstore/fortumoUtils/InappBaseProduct;
.super Ljava/lang/Object;
.source "InappBaseProduct.java"


# static fields
.field public static final PUBLISHED:Ljava/lang/String; = "published"

.field public static final UNPUBLISHED:Ljava/lang/String; = "unpublished"


# instance fields
.field autoFill:Z

.field baseDescription:Ljava/lang/String;

.field basePrice:F

.field baseTitle:Ljava/lang/String;

.field final localeToDescriptionMap:Ljava/util/HashMap;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/HashMap<",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            ">;"
        }
    .end annotation
.end field

.field final localeToPrice:Ljava/util/HashMap;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/HashMap<",
            "Ljava/lang/String;",
            "Ljava/lang/Float;",
            ">;"
        }
    .end annotation
.end field

.field final localeToTitleMap:Ljava/util/HashMap;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/HashMap<",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            ">;"
        }
    .end annotation
.end field

.field productId:Ljava/lang/String;

.field published:Z


# direct methods
.method public constructor <init>()V
    .locals 1

    .line 49
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 40
    new-instance v0, Ljava/util/HashMap;

    invoke-direct {v0}, Ljava/util/HashMap;-><init>()V

    iput-object v0, p0, Lorg/onepf/oms/appstore/fortumoUtils/InappBaseProduct;->localeToTitleMap:Ljava/util/HashMap;

    .line 43
    new-instance v0, Ljava/util/HashMap;

    invoke-direct {v0}, Ljava/util/HashMap;-><init>()V

    iput-object v0, p0, Lorg/onepf/oms/appstore/fortumoUtils/InappBaseProduct;->localeToDescriptionMap:Ljava/util/HashMap;

    .line 47
    new-instance v0, Ljava/util/HashMap;

    invoke-direct {v0}, Ljava/util/HashMap;-><init>()V

    iput-object v0, p0, Lorg/onepf/oms/appstore/fortumoUtils/InappBaseProduct;->localeToPrice:Ljava/util/HashMap;

    return-void
.end method

.method public constructor <init>(Lorg/onepf/oms/appstore/fortumoUtils/InappBaseProduct;)V
    .locals 2
    .param p1    # Lorg/onepf/oms/appstore/fortumoUtils/InappBaseProduct;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param

    .line 52
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 40
    new-instance v0, Ljava/util/HashMap;

    invoke-direct {v0}, Ljava/util/HashMap;-><init>()V

    iput-object v0, p0, Lorg/onepf/oms/appstore/fortumoUtils/InappBaseProduct;->localeToTitleMap:Ljava/util/HashMap;

    .line 43
    new-instance v0, Ljava/util/HashMap;

    invoke-direct {v0}, Ljava/util/HashMap;-><init>()V

    iput-object v0, p0, Lorg/onepf/oms/appstore/fortumoUtils/InappBaseProduct;->localeToDescriptionMap:Ljava/util/HashMap;

    .line 47
    new-instance v0, Ljava/util/HashMap;

    invoke-direct {v0}, Ljava/util/HashMap;-><init>()V

    iput-object v0, p0, Lorg/onepf/oms/appstore/fortumoUtils/InappBaseProduct;->localeToPrice:Ljava/util/HashMap;

    .line 53
    iget-boolean v0, p1, Lorg/onepf/oms/appstore/fortumoUtils/InappBaseProduct;->published:Z

    iput-boolean v0, p0, Lorg/onepf/oms/appstore/fortumoUtils/InappBaseProduct;->published:Z

    .line 54
    iget-object v0, p1, Lorg/onepf/oms/appstore/fortumoUtils/InappBaseProduct;->productId:Ljava/lang/String;

    iput-object v0, p0, Lorg/onepf/oms/appstore/fortumoUtils/InappBaseProduct;->productId:Ljava/lang/String;

    .line 55
    iget-object v0, p1, Lorg/onepf/oms/appstore/fortumoUtils/InappBaseProduct;->baseTitle:Ljava/lang/String;

    iput-object v0, p0, Lorg/onepf/oms/appstore/fortumoUtils/InappBaseProduct;->baseTitle:Ljava/lang/String;

    .line 56
    iget-object v0, p1, Lorg/onepf/oms/appstore/fortumoUtils/InappBaseProduct;->baseDescription:Ljava/lang/String;

    iput-object v0, p0, Lorg/onepf/oms/appstore/fortumoUtils/InappBaseProduct;->baseDescription:Ljava/lang/String;

    .line 57
    iget v0, p1, Lorg/onepf/oms/appstore/fortumoUtils/InappBaseProduct;->basePrice:F

    iput v0, p0, Lorg/onepf/oms/appstore/fortumoUtils/InappBaseProduct;->basePrice:F

    .line 58
    iget-object v0, p0, Lorg/onepf/oms/appstore/fortumoUtils/InappBaseProduct;->localeToTitleMap:Ljava/util/HashMap;

    iget-object v1, p1, Lorg/onepf/oms/appstore/fortumoUtils/InappBaseProduct;->localeToTitleMap:Ljava/util/HashMap;

    invoke-virtual {v0, v1}, Ljava/util/HashMap;->putAll(Ljava/util/Map;)V

    .line 59
    iget-object v0, p0, Lorg/onepf/oms/appstore/fortumoUtils/InappBaseProduct;->localeToDescriptionMap:Ljava/util/HashMap;

    iget-object v1, p1, Lorg/onepf/oms/appstore/fortumoUtils/InappBaseProduct;->localeToDescriptionMap:Ljava/util/HashMap;

    invoke-virtual {v0, v1}, Ljava/util/HashMap;->putAll(Ljava/util/Map;)V

    .line 60
    iget-object v0, p0, Lorg/onepf/oms/appstore/fortumoUtils/InappBaseProduct;->localeToPrice:Ljava/util/HashMap;

    iget-object p1, p1, Lorg/onepf/oms/appstore/fortumoUtils/InappBaseProduct;->localeToPrice:Ljava/util/HashMap;

    invoke-virtual {v0, p1}, Ljava/util/HashMap;->putAll(Ljava/util/Map;)V

    return-void
.end method


# virtual methods
.method public addCountryPrice(Ljava/lang/String;F)V
    .locals 1

    .line 133
    iget-object v0, p0, Lorg/onepf/oms/appstore/fortumoUtils/InappBaseProduct;->localeToPrice:Ljava/util/HashMap;

    invoke-static {p2}, Ljava/lang/Float;->valueOf(F)Ljava/lang/Float;

    move-result-object p2

    invoke-virtual {v0, p1, p2}, Ljava/util/HashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    return-void
.end method

.method public addDescriptionLocalization(Ljava/lang/String;Ljava/lang/String;)V
    .locals 1

    .line 116
    iget-object v0, p0, Lorg/onepf/oms/appstore/fortumoUtils/InappBaseProduct;->localeToDescriptionMap:Ljava/util/HashMap;

    invoke-virtual {v0, p1, p2}, Ljava/util/HashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    return-void
.end method

.method public addTitleLocalization(Ljava/lang/String;Ljava/lang/String;)V
    .locals 1

    .line 99
    iget-object v0, p0, Lorg/onepf/oms/appstore/fortumoUtils/InappBaseProduct;->localeToTitleMap:Ljava/util/HashMap;

    invoke-virtual {v0, p1, p2}, Ljava/util/HashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    return-void
.end method

.method public getBaseDescription()Ljava/lang/String;
    .locals 1

    .line 91
    iget-object v0, p0, Lorg/onepf/oms/appstore/fortumoUtils/InappBaseProduct;->baseDescription:Ljava/lang/String;

    return-object v0
.end method

.method public getBasePrice()F
    .locals 1

    .line 154
    iget v0, p0, Lorg/onepf/oms/appstore/fortumoUtils/InappBaseProduct;->basePrice:F

    return v0
.end method

.method public getBaseTitle()Ljava/lang/String;
    .locals 1

    .line 83
    iget-object v0, p0, Lorg/onepf/oms/appstore/fortumoUtils/InappBaseProduct;->baseTitle:Ljava/lang/String;

    return-object v0
.end method

.method public getDescription()Ljava/lang/String;
    .locals 1

    .line 129
    invoke-static {}, Ljava/util/Locale;->getDefault()Ljava/util/Locale;

    move-result-object v0

    invoke-virtual {v0}, Ljava/util/Locale;->toString()Ljava/lang/String;

    move-result-object v0

    invoke-virtual {p0, v0}, Lorg/onepf/oms/appstore/fortumoUtils/InappBaseProduct;->getDescriptionByLocale(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v0

    return-object v0
.end method

.method public getDescriptionByLocale(Ljava/lang/String;)Ljava/lang/String;
    .locals 1

    .line 120
    iget-object v0, p0, Lorg/onepf/oms/appstore/fortumoUtils/InappBaseProduct;->localeToDescriptionMap:Ljava/util/HashMap;

    invoke-virtual {v0, p1}, Ljava/util/HashMap;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object p1

    check-cast p1, Ljava/lang/String;

    .line 121
    invoke-static {p1}, Landroid/text/TextUtils;->isEmpty(Ljava/lang/CharSequence;)Z

    move-result v0

    if-nez v0, :cond_0

    return-object p1

    .line 124
    :cond_0
    iget-object p1, p0, Lorg/onepf/oms/appstore/fortumoUtils/InappBaseProduct;->baseDescription:Ljava/lang/String;

    return-object p1
.end method

.method public getPriceByCountryCode(Ljava/lang/String;)F
    .locals 1

    .line 137
    iget-object v0, p0, Lorg/onepf/oms/appstore/fortumoUtils/InappBaseProduct;->localeToPrice:Ljava/util/HashMap;

    invoke-virtual {v0, p1}, Ljava/util/HashMap;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object p1

    check-cast p1, Ljava/lang/Float;

    if-eqz p1, :cond_0

    .line 139
    invoke-virtual {p1}, Ljava/lang/Float;->floatValue()F

    move-result p1

    return p1

    .line 141
    :cond_0
    iget p1, p0, Lorg/onepf/oms/appstore/fortumoUtils/InappBaseProduct;->basePrice:F

    return p1
.end method

.method public getPriceDetails()Ljava/lang/String;
    .locals 5

    .line 146
    invoke-static {}, Ljava/util/Locale;->getDefault()Ljava/util/Locale;

    move-result-object v0

    .line 147
    iget-object v1, p0, Lorg/onepf/oms/appstore/fortumoUtils/InappBaseProduct;->localeToPrice:Ljava/util/HashMap;

    invoke-virtual {v0}, Ljava/util/Locale;->getCountry()Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v1, v2}, Ljava/util/HashMap;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Ljava/lang/Float;

    if-eqz v1, :cond_0

    .line 148
    invoke-virtual {v1}, Ljava/lang/Float;->floatValue()F

    move-result v2

    goto :goto_0

    :cond_0
    iget v2, p0, Lorg/onepf/oms/appstore/fortumoUtils/InappBaseProduct;->basePrice:F

    :goto_0
    if-eqz v1, :cond_1

    .line 149
    :goto_1
    invoke-static {v0}, Ljava/util/Currency;->getInstance(Ljava/util/Locale;)Ljava/util/Currency;

    move-result-object v0

    invoke-virtual {v0}, Ljava/util/Currency;->getSymbol()Ljava/lang/String;

    move-result-object v0

    goto :goto_2

    :cond_1
    sget-object v0, Ljava/util/Locale;->US:Ljava/util/Locale;

    goto :goto_1

    :goto_2
    const-string v1, "%.2f %s"

    const/4 v3, 0x2

    .line 150
    new-array v3, v3, [Ljava/lang/Object;

    const/4 v4, 0x0

    invoke-static {v2}, Ljava/lang/Float;->valueOf(F)Ljava/lang/Float;

    move-result-object v2

    aput-object v2, v3, v4

    const/4 v2, 0x1

    aput-object v0, v3, v2

    invoke-static {v1, v3}, Ljava/lang/String;->format(Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v0

    return-object v0
.end method

.method public getProductId()Ljava/lang/String;
    .locals 1

    .line 64
    iget-object v0, p0, Lorg/onepf/oms/appstore/fortumoUtils/InappBaseProduct;->productId:Ljava/lang/String;

    return-object v0
.end method

.method public getTitle()Ljava/lang/String;
    .locals 1

    .line 112
    invoke-static {}, Ljava/util/Locale;->getDefault()Ljava/util/Locale;

    move-result-object v0

    invoke-virtual {v0}, Ljava/util/Locale;->toString()Ljava/lang/String;

    move-result-object v0

    invoke-virtual {p0, v0}, Lorg/onepf/oms/appstore/fortumoUtils/InappBaseProduct;->getTitleByLocale(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v0

    return-object v0
.end method

.method public getTitleByLocale(Ljava/lang/String;)Ljava/lang/String;
    .locals 1

    .line 103
    iget-object v0, p0, Lorg/onepf/oms/appstore/fortumoUtils/InappBaseProduct;->localeToTitleMap:Ljava/util/HashMap;

    invoke-virtual {v0, p1}, Ljava/util/HashMap;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object p1

    check-cast p1, Ljava/lang/String;

    .line 104
    invoke-static {p1}, Landroid/text/TextUtils;->isEmpty(Ljava/lang/CharSequence;)Z

    move-result v0

    if-nez v0, :cond_0

    return-object p1

    .line 107
    :cond_0
    iget-object p1, p0, Lorg/onepf/oms/appstore/fortumoUtils/InappBaseProduct;->baseTitle:Ljava/lang/String;

    return-object p1
.end method

.method protected getValidateInfo()Ljava/lang/StringBuilder;
    .locals 3
    .annotation build Lorg/jetbrains/annotations/NotNull;
    .end annotation

    .line 179
    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    .line 180
    iget-object v1, p0, Lorg/onepf/oms/appstore/fortumoUtils/InappBaseProduct;->productId:Ljava/lang/String;

    invoke-static {v1}, Landroid/text/TextUtils;->isEmpty(Ljava/lang/CharSequence;)Z

    move-result v1

    if-eqz v1, :cond_0

    const-string v1, "product id is empty"

    .line 181
    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    .line 183
    :cond_0
    iget-object v1, p0, Lorg/onepf/oms/appstore/fortumoUtils/InappBaseProduct;->baseTitle:Ljava/lang/String;

    invoke-static {v1}, Landroid/text/TextUtils;->isEmpty(Ljava/lang/CharSequence;)Z

    move-result v1

    if-eqz v1, :cond_2

    .line 184
    invoke-virtual {v0}, Ljava/lang/StringBuilder;->length()I

    move-result v1

    if-lez v1, :cond_1

    const-string v1, ", "

    .line 185
    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    :cond_1
    const-string v1, "base title is empty"

    .line 187
    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    .line 189
    :cond_2
    iget-object v1, p0, Lorg/onepf/oms/appstore/fortumoUtils/InappBaseProduct;->baseDescription:Ljava/lang/String;

    invoke-static {v1}, Landroid/text/TextUtils;->isEmpty(Ljava/lang/CharSequence;)Z

    move-result v1

    if-eqz v1, :cond_4

    .line 190
    invoke-virtual {v0}, Ljava/lang/StringBuilder;->length()I

    move-result v1

    if-lez v1, :cond_3

    const-string v1, ", "

    .line 191
    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    :cond_3
    const-string v1, "base description is empty"

    .line 193
    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    .line 195
    :cond_4
    iget v1, p0, Lorg/onepf/oms/appstore/fortumoUtils/InappBaseProduct;->basePrice:F

    const/4 v2, 0x0

    cmpl-float v1, v1, v2

    if-nez v1, :cond_6

    .line 196
    invoke-virtual {v0}, Ljava/lang/StringBuilder;->length()I

    move-result v1

    if-lez v1, :cond_5

    const-string v1, ", "

    .line 197
    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    :cond_5
    const-string v1, "base price is not defined"

    .line 199
    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    :cond_6
    return-object v0
.end method

.method public isAutoFill()Z
    .locals 1

    .line 162
    iget-boolean v0, p0, Lorg/onepf/oms/appstore/fortumoUtils/InappBaseProduct;->autoFill:Z

    return v0
.end method

.method public isPublished()Z
    .locals 1

    .line 72
    iget-boolean v0, p0, Lorg/onepf/oms/appstore/fortumoUtils/InappBaseProduct;->published:Z

    return v0
.end method

.method public setAutoFill(Z)V
    .locals 0

    .line 166
    iput-boolean p1, p0, Lorg/onepf/oms/appstore/fortumoUtils/InappBaseProduct;->autoFill:Z

    return-void
.end method

.method public setBaseDescription(Ljava/lang/String;)V
    .locals 0

    .line 95
    iput-object p1, p0, Lorg/onepf/oms/appstore/fortumoUtils/InappBaseProduct;->baseDescription:Ljava/lang/String;

    return-void
.end method

.method public setBasePrice(F)V
    .locals 0

    .line 158
    iput p1, p0, Lorg/onepf/oms/appstore/fortumoUtils/InappBaseProduct;->basePrice:F

    return-void
.end method

.method public setBaseTitle(Ljava/lang/String;)V
    .locals 0

    .line 87
    iput-object p1, p0, Lorg/onepf/oms/appstore/fortumoUtils/InappBaseProduct;->baseTitle:Ljava/lang/String;

    return-void
.end method

.method public setProductId(Ljava/lang/String;)V
    .locals 0

    .line 68
    iput-object p1, p0, Lorg/onepf/oms/appstore/fortumoUtils/InappBaseProduct;->productId:Ljava/lang/String;

    return-void
.end method

.method public setPublished(Ljava/lang/String;)V
    .locals 3
    .param p1    # Ljava/lang/String;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param

    const-string v0, "published"

    .line 76
    invoke-virtual {p1, v0}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v0

    if-nez v0, :cond_1

    const-string v0, "unpublished"

    invoke-virtual {p1, v0}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v0

    if-eqz v0, :cond_0

    goto :goto_0

    .line 77
    :cond_0
    new-instance v0, Ljava/lang/IllegalArgumentException;

    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "Wrong \"publish-state\" attr value "

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p1

    invoke-direct {v0, p1}, Ljava/lang/IllegalArgumentException;-><init>(Ljava/lang/String;)V

    throw v0

    :cond_1
    :goto_0
    const-string v0, "published"

    .line 79
    invoke-virtual {p1, v0}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result p1

    iput-boolean p1, p0, Lorg/onepf/oms/appstore/fortumoUtils/InappBaseProduct;->published:Z

    return-void
.end method

.method public toString()Ljava/lang/String;
    .locals 3

    .line 206
    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    const-string v1, "InappBaseProduct{published="

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-boolean v1, p0, Lorg/onepf/oms/appstore/fortumoUtils/InappBaseProduct;->published:Z

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Z)Ljava/lang/StringBuilder;

    const-string v1, ", productId=\'"

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v1, p0, Lorg/onepf/oms/appstore/fortumoUtils/InappBaseProduct;->productId:Ljava/lang/String;

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const/16 v1, 0x27

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    const-string v2, ", baseTitle=\'"

    invoke-virtual {v0, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v2, p0, Lorg/onepf/oms/appstore/fortumoUtils/InappBaseProduct;->baseTitle:Ljava/lang/String;

    invoke-virtual {v0, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    const-string v2, ", localeToTitleMap="

    invoke-virtual {v0, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v2, p0, Lorg/onepf/oms/appstore/fortumoUtils/InappBaseProduct;->localeToTitleMap:Ljava/util/HashMap;

    invoke-virtual {v0, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/Object;)Ljava/lang/StringBuilder;

    const-string v2, ", baseDescription=\'"

    invoke-virtual {v0, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v2, p0, Lorg/onepf/oms/appstore/fortumoUtils/InappBaseProduct;->baseDescription:Ljava/lang/String;

    invoke-virtual {v0, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    const-string v1, ", localeToDescriptionMap="

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v1, p0, Lorg/onepf/oms/appstore/fortumoUtils/InappBaseProduct;->localeToDescriptionMap:Ljava/util/HashMap;

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/Object;)Ljava/lang/StringBuilder;

    const-string v1, ", autoFill="

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-boolean v1, p0, Lorg/onepf/oms/appstore/fortumoUtils/InappBaseProduct;->autoFill:Z

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Z)Ljava/lang/StringBuilder;

    const-string v1, ", basePrice="

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget v1, p0, Lorg/onepf/oms/appstore/fortumoUtils/InappBaseProduct;->basePrice:F

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(F)Ljava/lang/StringBuilder;

    const-string v1, ", localeToPrice="

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v1, p0, Lorg/onepf/oms/appstore/fortumoUtils/InappBaseProduct;->localeToPrice:Ljava/util/HashMap;

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/Object;)Ljava/lang/StringBuilder;

    const/16 v1, 0x7d

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v0

    return-object v0
.end method

.method public validateItem()V
    .locals 4

    .line 171
    invoke-virtual {p0}, Lorg/onepf/oms/appstore/fortumoUtils/InappBaseProduct;->getValidateInfo()Ljava/lang/StringBuilder;

    move-result-object v0

    .line 172
    invoke-virtual {v0}, Ljava/lang/StringBuilder;->length()I

    move-result v1

    if-gtz v1, :cond_0

    return-void

    .line 173
    :cond_0
    new-instance v1, Ljava/lang/IllegalStateException;

    new-instance v2, Ljava/lang/StringBuilder;

    invoke-direct {v2}, Ljava/lang/StringBuilder;-><init>()V

    const-string v3, "in-app product is not valid: "

    invoke-virtual {v2, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v0

    invoke-virtual {v2, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v2}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v0

    invoke-direct {v1, v0}, Ljava/lang/IllegalStateException;-><init>(Ljava/lang/String;)V

    throw v1
.end method
