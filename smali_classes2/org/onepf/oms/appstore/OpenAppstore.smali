.class public Lorg/onepf/oms/appstore/OpenAppstore;
.super Lorg/onepf/oms/DefaultAppstore;
.source "OpenAppstore.java"


# annotations
.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Lorg/onepf/oms/appstore/OpenAppstore$IOpenInAppBillingWrapper;
    }
.end annotation


# instance fields
.field private final appstoreName:Ljava/lang/String;

.field public componentName:Landroid/content/ComponentName;

.field private context:Landroid/content/Context;

.field private mBillingService:Lorg/onepf/oms/AppstoreInAppBillingService;
    .annotation build Lorg/jetbrains/annotations/Nullable;
    .end annotation
.end field

.field private openAppstoreService:Lorg/onepf/oms/IOpenAppstore;

.field private serviceConn:Landroid/content/ServiceConnection;


# direct methods
.method public constructor <init>(Landroid/content/Context;Ljava/lang/String;Lorg/onepf/oms/IOpenAppstore;Landroid/content/Intent;Ljava/lang/String;Landroid/content/ServiceConnection;)V
    .locals 6
    .param p1    # Landroid/content/Context;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param
    .param p4    # Landroid/content/Intent;
        .annotation build Lorg/jetbrains/annotations/Nullable;
        .end annotation
    .end param

    .line 60
    invoke-direct {p0}, Lorg/onepf/oms/DefaultAppstore;-><init>()V

    .line 61
    iput-object p1, p0, Lorg/onepf/oms/appstore/OpenAppstore;->context:Landroid/content/Context;

    .line 62
    iput-object p2, p0, Lorg/onepf/oms/appstore/OpenAppstore;->appstoreName:Ljava/lang/String;

    .line 63
    iput-object p3, p0, Lorg/onepf/oms/appstore/OpenAppstore;->openAppstoreService:Lorg/onepf/oms/IOpenAppstore;

    .line 64
    iput-object p6, p0, Lorg/onepf/oms/appstore/OpenAppstore;->serviceConn:Landroid/content/ServiceConnection;

    if-eqz p4, :cond_0

    .line 66
    new-instance p2, Lorg/onepf/oms/appstore/OpenAppstore$1;

    move-object v0, p2

    move-object v1, p0

    move-object v2, p1

    move-object v3, p5

    move-object v4, p0

    move-object v5, p4

    invoke-direct/range {v0 .. v5}, Lorg/onepf/oms/appstore/OpenAppstore$1;-><init>(Lorg/onepf/oms/appstore/OpenAppstore;Landroid/content/Context;Ljava/lang/String;Lorg/onepf/oms/Appstore;Landroid/content/Intent;)V

    iput-object p2, p0, Lorg/onepf/oms/appstore/OpenAppstore;->mBillingService:Lorg/onepf/oms/AppstoreInAppBillingService;

    :cond_0
    return-void
.end method

.method static synthetic access$100(Lorg/onepf/oms/appstore/OpenAppstore;)Landroid/content/ServiceConnection;
    .locals 0

    .line 39
    iget-object p0, p0, Lorg/onepf/oms/appstore/OpenAppstore;->serviceConn:Landroid/content/ServiceConnection;

    return-object p0
.end method

.method static synthetic access$200(Lorg/onepf/oms/appstore/OpenAppstore;)Landroid/content/Context;
    .locals 0

    .line 39
    iget-object p0, p0, Lorg/onepf/oms/appstore/OpenAppstore;->context:Landroid/content/Context;

    return-object p0
.end method


# virtual methods
.method public areOutsideLinksAllowed()Z
    .locals 2

    .line 160
    :try_start_0
    iget-object v0, p0, Lorg/onepf/oms/appstore/OpenAppstore;->openAppstoreService:Lorg/onepf/oms/IOpenAppstore;

    invoke-interface {v0}, Lorg/onepf/oms/IOpenAppstore;->areOutsideLinksAllowed()Z

    move-result v0
    :try_end_0
    .catch Landroid/os/RemoteException; {:try_start_0 .. :try_end_0} :catch_0

    return v0

    :catch_0
    move-exception v0

    const-string v1, "RemoteException"

    .line 162
    invoke-static {v1, v0}, Lorg/onepf/oms/util/Logger;->w(Ljava/lang/String;Ljava/lang/Throwable;)V

    const/4 v0, 0x0

    return v0
.end method

.method public getAppstoreName()Ljava/lang/String;
    .locals 1

    .line 121
    iget-object v0, p0, Lorg/onepf/oms/appstore/OpenAppstore;->appstoreName:Ljava/lang/String;

    return-object v0
.end method

.method public getInAppBillingService()Lorg/onepf/oms/AppstoreInAppBillingService;
    .locals 1
    .annotation build Lorg/jetbrains/annotations/Nullable;
    .end annotation

    .line 170
    iget-object v0, p0, Lorg/onepf/oms/appstore/OpenAppstore;->mBillingService:Lorg/onepf/oms/AppstoreInAppBillingService;

    return-object v0
.end method

.method public getPackageVersion(Ljava/lang/String;)I
    .locals 4

    .line 112
    :try_start_0
    iget-object v0, p0, Lorg/onepf/oms/appstore/OpenAppstore;->openAppstoreService:Lorg/onepf/oms/IOpenAppstore;

    invoke-interface {v0, p1}, Lorg/onepf/oms/IOpenAppstore;->getPackageVersion(Ljava/lang/String;)I

    move-result v0
    :try_end_0
    .catch Landroid/os/RemoteException; {:try_start_0 .. :try_end_0} :catch_0

    return v0

    :catch_0
    move-exception v0

    const/4 v1, 0x2

    .line 114
    new-array v1, v1, [Ljava/lang/Object;

    const/4 v2, 0x0

    const-string v3, "getPackageVersion() packageName: "

    aput-object v3, v1, v2

    const/4 v2, 0x1

    aput-object p1, v1, v2

    invoke-static {v0, v1}, Lorg/onepf/oms/util/Logger;->e(Ljava/lang/Throwable;[Ljava/lang/Object;)V

    const/4 p1, -0x1

    return p1
.end method

.method public getProductPageIntent(Ljava/lang/String;)Landroid/content/Intent;
    .locals 1
    .annotation build Lorg/jetbrains/annotations/Nullable;
    .end annotation

    .line 128
    :try_start_0
    iget-object v0, p0, Lorg/onepf/oms/appstore/OpenAppstore;->openAppstoreService:Lorg/onepf/oms/IOpenAppstore;

    invoke-interface {v0, p1}, Lorg/onepf/oms/IOpenAppstore;->getProductPageIntent(Ljava/lang/String;)Landroid/content/Intent;

    move-result-object p1
    :try_end_0
    .catch Landroid/os/RemoteException; {:try_start_0 .. :try_end_0} :catch_0

    return-object p1

    :catch_0
    move-exception p1

    const-string v0, "RemoteException: "

    .line 130
    invoke-static {v0, p1}, Lorg/onepf/oms/util/Logger;->w(Ljava/lang/String;Ljava/lang/Throwable;)V

    const/4 p1, 0x0

    return-object p1
.end method

.method public getRateItPageIntent(Ljava/lang/String;)Landroid/content/Intent;
    .locals 1
    .annotation build Lorg/jetbrains/annotations/Nullable;
    .end annotation

    .line 139
    :try_start_0
    iget-object v0, p0, Lorg/onepf/oms/appstore/OpenAppstore;->openAppstoreService:Lorg/onepf/oms/IOpenAppstore;

    invoke-interface {v0, p1}, Lorg/onepf/oms/IOpenAppstore;->getRateItPageIntent(Ljava/lang/String;)Landroid/content/Intent;

    move-result-object p1
    :try_end_0
    .catch Landroid/os/RemoteException; {:try_start_0 .. :try_end_0} :catch_0

    return-object p1

    :catch_0
    move-exception p1

    const-string v0, "RemoteException"

    .line 141
    invoke-static {v0, p1}, Lorg/onepf/oms/util/Logger;->w(Ljava/lang/String;Ljava/lang/Throwable;)V

    const/4 p1, 0x0

    return-object p1
.end method

.method public getSameDeveloperPageIntent(Ljava/lang/String;)Landroid/content/Intent;
    .locals 1
    .annotation build Lorg/jetbrains/annotations/Nullable;
    .end annotation

    .line 150
    :try_start_0
    iget-object v0, p0, Lorg/onepf/oms/appstore/OpenAppstore;->openAppstoreService:Lorg/onepf/oms/IOpenAppstore;

    invoke-interface {v0, p1}, Lorg/onepf/oms/IOpenAppstore;->getSameDeveloperPageIntent(Ljava/lang/String;)Landroid/content/Intent;

    move-result-object p1
    :try_end_0
    .catch Landroid/os/RemoteException; {:try_start_0 .. :try_end_0} :catch_0

    return-object p1

    :catch_0
    move-exception p1

    const-string v0, "RemoteException"

    .line 152
    invoke-static {v0, p1}, Lorg/onepf/oms/util/Logger;->w(Ljava/lang/String;Ljava/lang/Throwable;)V

    const/4 p1, 0x0

    return-object p1
.end method

.method public isBillingAvailable(Ljava/lang/String;)Z
    .locals 4

    .line 102
    :try_start_0
    iget-object v0, p0, Lorg/onepf/oms/appstore/OpenAppstore;->openAppstoreService:Lorg/onepf/oms/IOpenAppstore;

    invoke-interface {v0, p1}, Lorg/onepf/oms/IOpenAppstore;->isBillingAvailable(Ljava/lang/String;)Z

    move-result v0
    :try_end_0
    .catch Landroid/os/RemoteException; {:try_start_0 .. :try_end_0} :catch_0

    return v0

    :catch_0
    move-exception v0

    const/4 v1, 0x2

    .line 104
    new-array v1, v1, [Ljava/lang/Object;

    const-string v2, "isBillingAvailable() packageName: "

    const/4 v3, 0x0

    aput-object v2, v1, v3

    const/4 v2, 0x1

    aput-object p1, v1, v2

    invoke-static {v0, v1}, Lorg/onepf/oms/util/Logger;->e(Ljava/lang/Throwable;[Ljava/lang/Object;)V

    return v3
.end method

.method public isPackageInstaller(Ljava/lang/String;)Z
    .locals 1

    .line 92
    :try_start_0
    iget-object v0, p0, Lorg/onepf/oms/appstore/OpenAppstore;->openAppstoreService:Lorg/onepf/oms/IOpenAppstore;

    invoke-interface {v0, p1}, Lorg/onepf/oms/IOpenAppstore;->isPackageInstaller(Ljava/lang/String;)Z

    move-result p1
    :try_end_0
    .catch Landroid/os/RemoteException; {:try_start_0 .. :try_end_0} :catch_0

    return p1

    :catch_0
    move-exception p1

    const-string v0, "RemoteException: "

    .line 94
    invoke-static {v0, p1}, Lorg/onepf/oms/util/Logger;->w(Ljava/lang/String;Ljava/lang/Throwable;)V

    const/4 p1, 0x0

    return p1
.end method

.method public toString()Ljava/lang/String;
    .locals 2
    .annotation build Lorg/jetbrains/annotations/NotNull;
    .end annotation

    .line 175
    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    const-string v1, "OpenStore {name: "

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v1, p0, Lorg/onepf/oms/appstore/OpenAppstore;->appstoreName:Ljava/lang/String;

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, ", component: "

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v1, p0, Lorg/onepf/oms/appstore/OpenAppstore;->componentName:Landroid/content/ComponentName;

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/Object;)Ljava/lang/StringBuilder;

    const-string v1, "}"

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v0

    return-object v0
.end method
