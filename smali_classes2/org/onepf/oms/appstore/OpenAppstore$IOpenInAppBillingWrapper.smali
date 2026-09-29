.class final Lorg/onepf/oms/appstore/OpenAppstore$IOpenInAppBillingWrapper;
.super Ljava/lang/Object;
.source "OpenAppstore.java"

# interfaces
.implements Lcom/android/vending/billing/IInAppBillingService;


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lorg/onepf/oms/appstore/OpenAppstore;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x1a
    name = "IOpenInAppBillingWrapper"
.end annotation


# instance fields
.field private final openStoreBilling:Lorg/onepf/oms/IOpenInAppBillingService;


# direct methods
.method private constructor <init>(Lorg/onepf/oms/IOpenInAppBillingService;)V
    .locals 0

    .line 184
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 185
    iput-object p1, p0, Lorg/onepf/oms/appstore/OpenAppstore$IOpenInAppBillingWrapper;->openStoreBilling:Lorg/onepf/oms/IOpenInAppBillingService;

    return-void
.end method

.method synthetic constructor <init>(Lorg/onepf/oms/IOpenInAppBillingService;Lorg/onepf/oms/appstore/OpenAppstore$1;)V
    .locals 0

    .line 181
    invoke-direct {p0, p1}, Lorg/onepf/oms/appstore/OpenAppstore$IOpenInAppBillingWrapper;-><init>(Lorg/onepf/oms/IOpenInAppBillingService;)V

    return-void
.end method


# virtual methods
.method public asBinder()Landroid/os/IBinder;
    .locals 1

    .line 190
    iget-object v0, p0, Lorg/onepf/oms/appstore/OpenAppstore$IOpenInAppBillingWrapper;->openStoreBilling:Lorg/onepf/oms/IOpenInAppBillingService;

    invoke-interface {v0}, Lorg/onepf/oms/IOpenInAppBillingService;->asBinder()Landroid/os/IBinder;

    move-result-object v0

    return-object v0
.end method

.method public consumePurchase(ILjava/lang/String;Ljava/lang/String;)I
    .locals 1
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Landroid/os/RemoteException;
        }
    .end annotation

    .line 215
    iget-object v0, p0, Lorg/onepf/oms/appstore/OpenAppstore$IOpenInAppBillingWrapper;->openStoreBilling:Lorg/onepf/oms/IOpenInAppBillingService;

    invoke-interface {v0, p1, p2, p3}, Lorg/onepf/oms/IOpenInAppBillingService;->consumePurchase(ILjava/lang/String;Ljava/lang/String;)I

    move-result p1

    return p1
.end method

.method public getBuyIntent(ILjava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)Landroid/os/Bundle;
    .locals 6
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Landroid/os/RemoteException;
        }
    .end annotation

    .line 210
    iget-object v0, p0, Lorg/onepf/oms/appstore/OpenAppstore$IOpenInAppBillingWrapper;->openStoreBilling:Lorg/onepf/oms/IOpenInAppBillingService;

    move v1, p1

    move-object v2, p2

    move-object v3, p3

    move-object v4, p4

    move-object v5, p5

    invoke-interface/range {v0 .. v5}, Lorg/onepf/oms/IOpenInAppBillingService;->getBuyIntent(ILjava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)Landroid/os/Bundle;

    move-result-object p1

    return-object p1
.end method

.method public getPurchases(ILjava/lang/String;Ljava/lang/String;Ljava/lang/String;)Landroid/os/Bundle;
    .locals 1
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Landroid/os/RemoteException;
        }
    .end annotation

    .line 205
    iget-object v0, p0, Lorg/onepf/oms/appstore/OpenAppstore$IOpenInAppBillingWrapper;->openStoreBilling:Lorg/onepf/oms/IOpenInAppBillingService;

    invoke-interface {v0, p1, p2, p3, p4}, Lorg/onepf/oms/IOpenInAppBillingService;->getPurchases(ILjava/lang/String;Ljava/lang/String;Ljava/lang/String;)Landroid/os/Bundle;

    move-result-object p1

    return-object p1
.end method

.method public getSkuDetails(ILjava/lang/String;Ljava/lang/String;Landroid/os/Bundle;)Landroid/os/Bundle;
    .locals 1
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Landroid/os/RemoteException;
        }
    .end annotation

    .line 200
    iget-object v0, p0, Lorg/onepf/oms/appstore/OpenAppstore$IOpenInAppBillingWrapper;->openStoreBilling:Lorg/onepf/oms/IOpenInAppBillingService;

    invoke-interface {v0, p1, p2, p3, p4}, Lorg/onepf/oms/IOpenInAppBillingService;->getSkuDetails(ILjava/lang/String;Ljava/lang/String;Landroid/os/Bundle;)Landroid/os/Bundle;

    move-result-object p1

    return-object p1
.end method

.method public isBillingSupported(ILjava/lang/String;Ljava/lang/String;)I
    .locals 1
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Landroid/os/RemoteException;
        }
    .end annotation

    .line 195
    iget-object v0, p0, Lorg/onepf/oms/appstore/OpenAppstore$IOpenInAppBillingWrapper;->openStoreBilling:Lorg/onepf/oms/IOpenInAppBillingService;

    invoke-interface {v0, p1, p2, p3}, Lorg/onepf/oms/IOpenInAppBillingService;->isBillingSupported(ILjava/lang/String;Ljava/lang/String;)I

    move-result p1

    return p1
.end method
