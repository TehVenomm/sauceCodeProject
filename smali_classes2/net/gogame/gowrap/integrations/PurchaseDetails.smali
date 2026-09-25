.class public Lnet/gogame/gowrap/integrations/PurchaseDetails;
.super Ljava/lang/Object;
.source "PurchaseDetails.java"


# annotations
.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Lnet/gogame/gowrap/integrations/PurchaseDetails$VerificationStatus;
    }
.end annotation


# instance fields
.field private comment:Ljava/lang/String;

.field private currencyCode:Ljava/lang/String;

.field private orderId:Ljava/lang/String;

.field private price:Ljava/lang/Double;

.field private productId:Ljava/lang/String;

.field private purchaseData:Ljava/lang/String;

.field private referenceId:Ljava/lang/String;

.field private sandbox:Z

.field private signature:Ljava/lang/String;

.field private timestamp:Ljava/util/Date;

.field private verificationStatus:Lnet/gogame/gowrap/integrations/PurchaseDetails$VerificationStatus;


# direct methods
.method public constructor <init>()V
    .locals 2

    .line 5
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    const/4 v0, 0x0

    .line 13
    iput-object v0, p0, Lnet/gogame/gowrap/integrations/PurchaseDetails;->referenceId:Ljava/lang/String;

    .line 14
    iput-object v0, p0, Lnet/gogame/gowrap/integrations/PurchaseDetails;->timestamp:Ljava/util/Date;

    .line 15
    iput-object v0, p0, Lnet/gogame/gowrap/integrations/PurchaseDetails;->productId:Ljava/lang/String;

    .line 16
    iput-object v0, p0, Lnet/gogame/gowrap/integrations/PurchaseDetails;->currencyCode:Ljava/lang/String;

    .line 17
    iput-object v0, p0, Lnet/gogame/gowrap/integrations/PurchaseDetails;->price:Ljava/lang/Double;

    .line 18
    iput-object v0, p0, Lnet/gogame/gowrap/integrations/PurchaseDetails;->orderId:Ljava/lang/String;

    .line 19
    iput-object v0, p0, Lnet/gogame/gowrap/integrations/PurchaseDetails;->verificationStatus:Lnet/gogame/gowrap/integrations/PurchaseDetails$VerificationStatus;

    const/4 v1, 0x0

    .line 20
    iput-boolean v1, p0, Lnet/gogame/gowrap/integrations/PurchaseDetails;->sandbox:Z

    .line 21
    iput-object v0, p0, Lnet/gogame/gowrap/integrations/PurchaseDetails;->purchaseData:Ljava/lang/String;

    .line 22
    iput-object v0, p0, Lnet/gogame/gowrap/integrations/PurchaseDetails;->signature:Ljava/lang/String;

    .line 23
    iput-object v0, p0, Lnet/gogame/gowrap/integrations/PurchaseDetails;->comment:Ljava/lang/String;

    return-void
.end method


# virtual methods
.method public getComment()Ljava/lang/String;
    .locals 1

    .line 106
    iget-object v0, p0, Lnet/gogame/gowrap/integrations/PurchaseDetails;->comment:Ljava/lang/String;

    return-object v0
.end method

.method public getCurrencyCode()Ljava/lang/String;
    .locals 1

    .line 50
    iget-object v0, p0, Lnet/gogame/gowrap/integrations/PurchaseDetails;->currencyCode:Ljava/lang/String;

    return-object v0
.end method

.method public getOrderId()Ljava/lang/String;
    .locals 1

    .line 66
    iget-object v0, p0, Lnet/gogame/gowrap/integrations/PurchaseDetails;->orderId:Ljava/lang/String;

    return-object v0
.end method

.method public getPrice()Ljava/lang/Double;
    .locals 1

    .line 58
    iget-object v0, p0, Lnet/gogame/gowrap/integrations/PurchaseDetails;->price:Ljava/lang/Double;

    return-object v0
.end method

.method public getProductId()Ljava/lang/String;
    .locals 1

    .line 42
    iget-object v0, p0, Lnet/gogame/gowrap/integrations/PurchaseDetails;->productId:Ljava/lang/String;

    return-object v0
.end method

.method public getPurchaseData()Ljava/lang/String;
    .locals 1

    .line 90
    iget-object v0, p0, Lnet/gogame/gowrap/integrations/PurchaseDetails;->purchaseData:Ljava/lang/String;

    return-object v0
.end method

.method public getReferenceId()Ljava/lang/String;
    .locals 1

    .line 26
    iget-object v0, p0, Lnet/gogame/gowrap/integrations/PurchaseDetails;->referenceId:Ljava/lang/String;

    return-object v0
.end method

.method public getSignature()Ljava/lang/String;
    .locals 1

    .line 98
    iget-object v0, p0, Lnet/gogame/gowrap/integrations/PurchaseDetails;->signature:Ljava/lang/String;

    return-object v0
.end method

.method public getTimestamp()Ljava/util/Date;
    .locals 1

    .line 34
    iget-object v0, p0, Lnet/gogame/gowrap/integrations/PurchaseDetails;->timestamp:Ljava/util/Date;

    return-object v0
.end method

.method public getVerificationStatus()Lnet/gogame/gowrap/integrations/PurchaseDetails$VerificationStatus;
    .locals 1

    .line 74
    iget-object v0, p0, Lnet/gogame/gowrap/integrations/PurchaseDetails;->verificationStatus:Lnet/gogame/gowrap/integrations/PurchaseDetails$VerificationStatus;

    return-object v0
.end method

.method public isSandbox()Z
    .locals 1

    .line 82
    iget-boolean v0, p0, Lnet/gogame/gowrap/integrations/PurchaseDetails;->sandbox:Z

    return v0
.end method

.method public setComment(Ljava/lang/String;)V
    .locals 0

    .line 110
    iput-object p1, p0, Lnet/gogame/gowrap/integrations/PurchaseDetails;->comment:Ljava/lang/String;

    return-void
.end method

.method public setCurrencyCode(Ljava/lang/String;)V
    .locals 0

    .line 54
    iput-object p1, p0, Lnet/gogame/gowrap/integrations/PurchaseDetails;->currencyCode:Ljava/lang/String;

    return-void
.end method

.method public setOrderId(Ljava/lang/String;)V
    .locals 0

    .line 70
    iput-object p1, p0, Lnet/gogame/gowrap/integrations/PurchaseDetails;->orderId:Ljava/lang/String;

    return-void
.end method

.method public setPrice(Ljava/lang/Double;)V
    .locals 0

    .line 62
    iput-object p1, p0, Lnet/gogame/gowrap/integrations/PurchaseDetails;->price:Ljava/lang/Double;

    return-void
.end method

.method public setProductId(Ljava/lang/String;)V
    .locals 0

    .line 46
    iput-object p1, p0, Lnet/gogame/gowrap/integrations/PurchaseDetails;->productId:Ljava/lang/String;

    return-void
.end method

.method public setPurchaseData(Ljava/lang/String;)V
    .locals 0

    .line 94
    iput-object p1, p0, Lnet/gogame/gowrap/integrations/PurchaseDetails;->purchaseData:Ljava/lang/String;

    return-void
.end method

.method public setReferenceId(Ljava/lang/String;)V
    .locals 0

    .line 30
    iput-object p1, p0, Lnet/gogame/gowrap/integrations/PurchaseDetails;->referenceId:Ljava/lang/String;

    return-void
.end method

.method public setSandbox(Z)V
    .locals 0

    .line 86
    iput-boolean p1, p0, Lnet/gogame/gowrap/integrations/PurchaseDetails;->sandbox:Z

    return-void
.end method

.method public setSignature(Ljava/lang/String;)V
    .locals 0

    .line 102
    iput-object p1, p0, Lnet/gogame/gowrap/integrations/PurchaseDetails;->signature:Ljava/lang/String;

    return-void
.end method

.method public setTimestamp(Ljava/util/Date;)V
    .locals 0

    .line 38
    iput-object p1, p0, Lnet/gogame/gowrap/integrations/PurchaseDetails;->timestamp:Ljava/util/Date;

    return-void
.end method

.method public setVerificationStatus(Lnet/gogame/gowrap/integrations/PurchaseDetails$VerificationStatus;)V
    .locals 0

    .line 78
    iput-object p1, p0, Lnet/gogame/gowrap/integrations/PurchaseDetails;->verificationStatus:Lnet/gogame/gowrap/integrations/PurchaseDetails$VerificationStatus;

    return-void
.end method
