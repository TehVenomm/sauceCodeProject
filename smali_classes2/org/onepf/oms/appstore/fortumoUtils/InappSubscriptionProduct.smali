.class public Lorg/onepf/oms/appstore/fortumoUtils/InappSubscriptionProduct;
.super Lorg/onepf/oms/appstore/fortumoUtils/InappBaseProduct;
.source "InappSubscriptionProduct.java"


# static fields
.field public static final ONE_MONTH:Ljava/lang/String; = "oneMonth"

.field public static final ONE_YEAR:Ljava/lang/String; = "oneYear"


# instance fields
.field private period:Ljava/lang/String;


# direct methods
.method public constructor <init>(Lorg/onepf/oms/appstore/fortumoUtils/InappBaseProduct;Ljava/lang/String;)V
    .locals 0
    .param p1    # Lorg/onepf/oms/appstore/fortumoUtils/InappBaseProduct;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param

    .line 35
    invoke-direct {p0, p1}, Lorg/onepf/oms/appstore/fortumoUtils/InappBaseProduct;-><init>(Lorg/onepf/oms/appstore/fortumoUtils/InappBaseProduct;)V

    .line 36
    iput-object p2, p0, Lorg/onepf/oms/appstore/fortumoUtils/InappSubscriptionProduct;->period:Ljava/lang/String;

    return-void
.end method


# virtual methods
.method public getPeriod()Ljava/lang/String;
    .locals 1

    .line 41
    iget-object v0, p0, Lorg/onepf/oms/appstore/fortumoUtils/InappSubscriptionProduct;->period:Ljava/lang/String;

    return-object v0
.end method

.method public setPeriod(Ljava/lang/String;)V
    .locals 1
    .param p1    # Ljava/lang/String;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param

    const-string v0, "oneMonth"

    .line 45
    invoke-virtual {p1, v0}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v0

    if-nez v0, :cond_1

    const-string v0, "oneYear"

    invoke-virtual {p1, v0}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v0

    if-eqz v0, :cond_0

    goto :goto_0

    .line 46
    :cond_0
    new-instance p1, Ljava/lang/IllegalStateException;

    const-string v0, "Wrong period value!"

    invoke-direct {p1, v0}, Ljava/lang/IllegalStateException;-><init>(Ljava/lang/String;)V

    throw p1

    .line 48
    :cond_1
    :goto_0
    iput-object p1, p0, Lorg/onepf/oms/appstore/fortumoUtils/InappSubscriptionProduct;->period:Ljava/lang/String;

    return-void
.end method

.method public toString()Ljava/lang/String;
    .locals 3
    .annotation build Lorg/jetbrains/annotations/NotNull;
    .end annotation

    .line 68
    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    const-string v1, "InappSubscriptionProduct{published="

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-boolean v1, p0, Lorg/onepf/oms/appstore/fortumoUtils/InappSubscriptionProduct;->published:Z

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Z)Ljava/lang/StringBuilder;

    const-string v1, ", productId=\'"

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v1, p0, Lorg/onepf/oms/appstore/fortumoUtils/InappSubscriptionProduct;->productId:Ljava/lang/String;

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const/16 v1, 0x27

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    const-string v2, ", baseTitle=\'"

    invoke-virtual {v0, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v2, p0, Lorg/onepf/oms/appstore/fortumoUtils/InappSubscriptionProduct;->baseTitle:Ljava/lang/String;

    invoke-virtual {v0, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    const-string v2, ", localeToTitleMap="

    invoke-virtual {v0, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v2, p0, Lorg/onepf/oms/appstore/fortumoUtils/InappSubscriptionProduct;->localeToTitleMap:Ljava/util/HashMap;

    invoke-virtual {v0, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/Object;)Ljava/lang/StringBuilder;

    const-string v2, ", baseDescription=\'"

    invoke-virtual {v0, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v2, p0, Lorg/onepf/oms/appstore/fortumoUtils/InappSubscriptionProduct;->baseDescription:Ljava/lang/String;

    invoke-virtual {v0, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    const-string v2, ", localeToDescriptionMap="

    invoke-virtual {v0, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v2, p0, Lorg/onepf/oms/appstore/fortumoUtils/InappSubscriptionProduct;->localeToDescriptionMap:Ljava/util/HashMap;

    invoke-virtual {v0, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/Object;)Ljava/lang/StringBuilder;

    const-string v2, ", autoFill="

    invoke-virtual {v0, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-boolean v2, p0, Lorg/onepf/oms/appstore/fortumoUtils/InappSubscriptionProduct;->autoFill:Z

    invoke-virtual {v0, v2}, Ljava/lang/StringBuilder;->append(Z)Ljava/lang/StringBuilder;

    const-string v2, ", basePrice="

    invoke-virtual {v0, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget v2, p0, Lorg/onepf/oms/appstore/fortumoUtils/InappSubscriptionProduct;->basePrice:F

    invoke-virtual {v0, v2}, Ljava/lang/StringBuilder;->append(F)Ljava/lang/StringBuilder;

    const-string v2, ", localeToPrice="

    invoke-virtual {v0, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v2, p0, Lorg/onepf/oms/appstore/fortumoUtils/InappSubscriptionProduct;->localeToPrice:Ljava/util/HashMap;

    invoke-virtual {v0, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/Object;)Ljava/lang/StringBuilder;

    const-string v2, ", period=\'"

    invoke-virtual {v0, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v2, p0, Lorg/onepf/oms/appstore/fortumoUtils/InappSubscriptionProduct;->period:Ljava/lang/String;

    invoke-virtual {v0, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    const/16 v1, 0x7d

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v0

    return-object v0
.end method

.method public validateItem()V
    .locals 4

    .line 53
    invoke-virtual {p0}, Lorg/onepf/oms/appstore/fortumoUtils/InappSubscriptionProduct;->getValidateInfo()Ljava/lang/StringBuilder;

    move-result-object v0

    .line 54
    iget-object v1, p0, Lorg/onepf/oms/appstore/fortumoUtils/InappSubscriptionProduct;->period:Ljava/lang/String;

    invoke-static {v1}, Landroid/text/TextUtils;->isEmpty(Ljava/lang/CharSequence;)Z

    move-result v1

    if-nez v1, :cond_0

    iget-object v1, p0, Lorg/onepf/oms/appstore/fortumoUtils/InappSubscriptionProduct;->period:Ljava/lang/String;

    const-string v2, "oneMonth"

    invoke-virtual {v1, v2}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v1

    if-nez v1, :cond_2

    iget-object v1, p0, Lorg/onepf/oms/appstore/fortumoUtils/InappSubscriptionProduct;->period:Ljava/lang/String;

    const-string v2, "oneYear"

    invoke-virtual {v1, v2}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v1

    if-nez v1, :cond_2

    .line 55
    :cond_0
    invoke-virtual {v0}, Ljava/lang/StringBuilder;->length()I

    move-result v1

    if-lez v1, :cond_1

    const-string v1, ", "

    .line 56
    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    :cond_1
    const-string v1, "period is not valid"

    .line 58
    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    .line 60
    :cond_2
    invoke-virtual {v0}, Ljava/lang/StringBuilder;->length()I

    move-result v1

    if-gtz v1, :cond_3

    return-void

    .line 61
    :cond_3
    new-instance v1, Ljava/lang/IllegalStateException;

    new-instance v2, Ljava/lang/StringBuilder;

    invoke-direct {v2}, Ljava/lang/StringBuilder;-><init>()V

    const-string v3, "subscription product is not valid: "

    invoke-virtual {v2, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v2, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/Object;)Ljava/lang/StringBuilder;

    invoke-virtual {v2}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v0

    invoke-direct {v1, v0}, Ljava/lang/IllegalStateException;-><init>(Ljava/lang/String;)V

    throw v1
.end method
