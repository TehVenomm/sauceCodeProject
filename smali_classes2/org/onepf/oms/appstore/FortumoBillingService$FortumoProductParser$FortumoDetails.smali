.class Lorg/onepf/oms/appstore/FortumoBillingService$FortumoProductParser$FortumoDetails;
.super Ljava/lang/Object;
.source "FortumoBillingService.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lorg/onepf/oms/appstore/FortumoBillingService$FortumoProductParser;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x8
    name = "FortumoDetails"
.end annotation


# instance fields
.field private consumable:Z

.field private id:Ljava/lang/String;

.field private nookInAppSecret:Ljava/lang/String;

.field private nookServiceId:Ljava/lang/String;

.field private serviceId:Ljava/lang/String;

.field private serviceInAppSecret:Ljava/lang/String;


# direct methods
.method public constructor <init>(Ljava/lang/String;ZLjava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V
    .locals 0

    .line 502
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 503
    iput-object p1, p0, Lorg/onepf/oms/appstore/FortumoBillingService$FortumoProductParser$FortumoDetails;->id:Ljava/lang/String;

    .line 504
    iput-boolean p2, p0, Lorg/onepf/oms/appstore/FortumoBillingService$FortumoProductParser$FortumoDetails;->consumable:Z

    .line 505
    iput-object p3, p0, Lorg/onepf/oms/appstore/FortumoBillingService$FortumoProductParser$FortumoDetails;->serviceId:Ljava/lang/String;

    .line 506
    iput-object p4, p0, Lorg/onepf/oms/appstore/FortumoBillingService$FortumoProductParser$FortumoDetails;->serviceInAppSecret:Ljava/lang/String;

    .line 507
    iput-object p5, p0, Lorg/onepf/oms/appstore/FortumoBillingService$FortumoProductParser$FortumoDetails;->nookServiceId:Ljava/lang/String;

    .line 508
    iput-object p6, p0, Lorg/onepf/oms/appstore/FortumoBillingService$FortumoProductParser$FortumoDetails;->nookInAppSecret:Ljava/lang/String;

    return-void
.end method


# virtual methods
.method public getId()Ljava/lang/String;
    .locals 1

    .line 512
    iget-object v0, p0, Lorg/onepf/oms/appstore/FortumoBillingService$FortumoProductParser$FortumoDetails;->id:Ljava/lang/String;

    return-object v0
.end method

.method public getNookInAppSecret()Ljava/lang/String;
    .locals 1

    .line 532
    iget-object v0, p0, Lorg/onepf/oms/appstore/FortumoBillingService$FortumoProductParser$FortumoDetails;->nookInAppSecret:Ljava/lang/String;

    return-object v0
.end method

.method public getNookServiceId()Ljava/lang/String;
    .locals 1

    .line 528
    iget-object v0, p0, Lorg/onepf/oms/appstore/FortumoBillingService$FortumoProductParser$FortumoDetails;->nookServiceId:Ljava/lang/String;

    return-object v0
.end method

.method public getServiceId()Ljava/lang/String;
    .locals 1

    .line 520
    iget-object v0, p0, Lorg/onepf/oms/appstore/FortumoBillingService$FortumoProductParser$FortumoDetails;->serviceId:Ljava/lang/String;

    return-object v0
.end method

.method public getServiceInAppSecret()Ljava/lang/String;
    .locals 1

    .line 524
    iget-object v0, p0, Lorg/onepf/oms/appstore/FortumoBillingService$FortumoProductParser$FortumoDetails;->serviceInAppSecret:Ljava/lang/String;

    return-object v0
.end method

.method public isConsumable()Z
    .locals 1

    .line 516
    iget-boolean v0, p0, Lorg/onepf/oms/appstore/FortumoBillingService$FortumoProductParser$FortumoDetails;->consumable:Z

    return v0
.end method

.method public toString()Ljava/lang/String;
    .locals 3
    .annotation build Lorg/jetbrains/annotations/NotNull;
    .end annotation

    .line 538
    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    const-string v1, "FortumoDetails{id=\'"

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v1, p0, Lorg/onepf/oms/appstore/FortumoBillingService$FortumoProductParser$FortumoDetails;->id:Ljava/lang/String;

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const/16 v1, 0x27

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    const-string v2, ", serviceId=\'"

    invoke-virtual {v0, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v2, p0, Lorg/onepf/oms/appstore/FortumoBillingService$FortumoProductParser$FortumoDetails;->serviceId:Ljava/lang/String;

    invoke-virtual {v0, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    const-string v2, ", serviceInAppSecret=\'"

    invoke-virtual {v0, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v2, p0, Lorg/onepf/oms/appstore/FortumoBillingService$FortumoProductParser$FortumoDetails;->serviceInAppSecret:Ljava/lang/String;

    invoke-virtual {v0, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    const-string v2, ", nookServiceId=\'"

    invoke-virtual {v0, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v2, p0, Lorg/onepf/oms/appstore/FortumoBillingService$FortumoProductParser$FortumoDetails;->nookServiceId:Ljava/lang/String;

    invoke-virtual {v0, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    const-string v2, ", nookInAppSecret=\'"

    invoke-virtual {v0, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v2, p0, Lorg/onepf/oms/appstore/FortumoBillingService$FortumoProductParser$FortumoDetails;->nookInAppSecret:Ljava/lang/String;

    invoke-virtual {v0, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    const-string v1, ", consumable="

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-boolean v1, p0, Lorg/onepf/oms/appstore/FortumoBillingService$FortumoProductParser$FortumoDetails;->consumable:Z

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Z)Ljava/lang/StringBuilder;

    const/16 v1, 0x7d

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v0

    return-object v0
.end method
