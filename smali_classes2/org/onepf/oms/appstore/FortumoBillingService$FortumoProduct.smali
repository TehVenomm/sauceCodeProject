.class Lorg/onepf/oms/appstore/FortumoBillingService$FortumoProduct;
.super Lorg/onepf/oms/appstore/fortumoUtils/InappBaseProduct;
.source "FortumoBillingService.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lorg/onepf/oms/appstore/FortumoBillingService;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x8
    name = "FortumoProduct"
.end annotation


# instance fields
.field private fortumoDetails:Lorg/onepf/oms/appstore/FortumoBillingService$FortumoProductParser$FortumoDetails;

.field private fortumoPrice:Ljava/lang/String;


# direct methods
.method public constructor <init>(Lorg/onepf/oms/appstore/fortumoUtils/InappBaseProduct;Lorg/onepf/oms/appstore/FortumoBillingService$FortumoProductParser$FortumoDetails;Ljava/lang/String;)V
    .locals 0
    .param p1    # Lorg/onepf/oms/appstore/fortumoUtils/InappBaseProduct;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param

    .line 325
    invoke-direct {p0, p1}, Lorg/onepf/oms/appstore/fortumoUtils/InappBaseProduct;-><init>(Lorg/onepf/oms/appstore/fortumoUtils/InappBaseProduct;)V

    .line 326
    iput-object p2, p0, Lorg/onepf/oms/appstore/FortumoBillingService$FortumoProduct;->fortumoDetails:Lorg/onepf/oms/appstore/FortumoBillingService$FortumoProductParser$FortumoDetails;

    .line 327
    iput-object p3, p0, Lorg/onepf/oms/appstore/FortumoBillingService$FortumoProduct;->fortumoPrice:Ljava/lang/String;

    return-void
.end method


# virtual methods
.method public getFortumoPrice()Ljava/lang/String;
    .locals 1

    .line 344
    iget-object v0, p0, Lorg/onepf/oms/appstore/FortumoBillingService$FortumoProduct;->fortumoPrice:Ljava/lang/String;

    return-object v0
.end method

.method public getInAppSecret()Ljava/lang/String;
    .locals 1

    .line 335
    iget-object v0, p0, Lorg/onepf/oms/appstore/FortumoBillingService$FortumoProduct;->fortumoDetails:Lorg/onepf/oms/appstore/FortumoBillingService$FortumoProductParser$FortumoDetails;

    invoke-virtual {v0}, Lorg/onepf/oms/appstore/FortumoBillingService$FortumoProductParser$FortumoDetails;->getServiceInAppSecret()Ljava/lang/String;

    move-result-object v0

    return-object v0
.end method

.method public getNookInAppSecret()Ljava/lang/String;
    .locals 1

    .line 352
    iget-object v0, p0, Lorg/onepf/oms/appstore/FortumoBillingService$FortumoProduct;->fortumoDetails:Lorg/onepf/oms/appstore/FortumoBillingService$FortumoProductParser$FortumoDetails;

    invoke-virtual {v0}, Lorg/onepf/oms/appstore/FortumoBillingService$FortumoProductParser$FortumoDetails;->getNookInAppSecret()Ljava/lang/String;

    move-result-object v0

    return-object v0
.end method

.method public getNookServiceId()Ljava/lang/String;
    .locals 1

    .line 348
    iget-object v0, p0, Lorg/onepf/oms/appstore/FortumoBillingService$FortumoProduct;->fortumoDetails:Lorg/onepf/oms/appstore/FortumoBillingService$FortumoProductParser$FortumoDetails;

    invoke-virtual {v0}, Lorg/onepf/oms/appstore/FortumoBillingService$FortumoProductParser$FortumoDetails;->getNookServiceId()Ljava/lang/String;

    move-result-object v0

    return-object v0
.end method

.method public getServiceId()Ljava/lang/String;
    .locals 1

    .line 331
    iget-object v0, p0, Lorg/onepf/oms/appstore/FortumoBillingService$FortumoProduct;->fortumoDetails:Lorg/onepf/oms/appstore/FortumoBillingService$FortumoProductParser$FortumoDetails;

    invoke-virtual {v0}, Lorg/onepf/oms/appstore/FortumoBillingService$FortumoProductParser$FortumoDetails;->getServiceId()Ljava/lang/String;

    move-result-object v0

    return-object v0
.end method

.method public isConsumable()Z
    .locals 1

    .line 357
    iget-object v0, p0, Lorg/onepf/oms/appstore/FortumoBillingService$FortumoProduct;->fortumoDetails:Lorg/onepf/oms/appstore/FortumoBillingService$FortumoProductParser$FortumoDetails;

    invoke-virtual {v0}, Lorg/onepf/oms/appstore/FortumoBillingService$FortumoProductParser$FortumoDetails;->isConsumable()Z

    move-result v0

    return v0
.end method

.method public toSkuDetails(Ljava/lang/String;)Lorg/onepf/oms/appstore/googleUtils/SkuDetails;
    .locals 7
    .annotation build Lorg/jetbrains/annotations/NotNull;
    .end annotation

    .line 340
    new-instance v6, Lorg/onepf/oms/appstore/googleUtils/SkuDetails;

    const-string v1, "inapp"

    invoke-virtual {p0}, Lorg/onepf/oms/appstore/FortumoBillingService$FortumoProduct;->getProductId()Ljava/lang/String;

    move-result-object v2

    invoke-virtual {p0}, Lorg/onepf/oms/appstore/FortumoBillingService$FortumoProduct;->getTitle()Ljava/lang/String;

    move-result-object v3

    invoke-virtual {p0}, Lorg/onepf/oms/appstore/FortumoBillingService$FortumoProduct;->getDescription()Ljava/lang/String;

    move-result-object v5

    move-object v0, v6

    move-object v4, p1

    invoke-direct/range {v0 .. v5}, Lorg/onepf/oms/appstore/googleUtils/SkuDetails;-><init>(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V

    return-object v6
.end method

.method public toString()Ljava/lang/String;
    .locals 2
    .annotation build Lorg/jetbrains/annotations/NotNull;
    .end annotation

    .line 363
    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    const-string v1, "FortumoProduct{fortumoDetails="

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v1, p0, Lorg/onepf/oms/appstore/FortumoBillingService$FortumoProduct;->fortumoDetails:Lorg/onepf/oms/appstore/FortumoBillingService$FortumoProductParser$FortumoDetails;

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/Object;)Ljava/lang/StringBuilder;

    const-string v1, ", fortumoPrice=\'"

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v1, p0, Lorg/onepf/oms/appstore/FortumoBillingService$FortumoProduct;->fortumoPrice:Ljava/lang/String;

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const/16 v1, 0x27

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    const/16 v1, 0x7d

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v0

    return-object v0
.end method
