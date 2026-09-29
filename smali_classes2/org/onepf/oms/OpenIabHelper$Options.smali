.class public Lorg/onepf/oms/OpenIabHelper$Options;
.super Ljava/lang/Object;
.source "OpenIabHelper.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lorg/onepf/oms/OpenIabHelper;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x9
    name = "Options"
.end annotation

.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Lorg/onepf/oms/OpenIabHelper$Options$Builder;
    }
.end annotation


# static fields
.field public static final SEARCH_STRATEGY_BEST_FIT:I = 0x1

.field public static final SEARCH_STRATEGY_INSTALLER:I = 0x0

.field public static final SEARCH_STRATEGY_INSTALLER_THEN_BEST_FIT:I = 0x2

.field public static final VERIFY_EVERYTHING:I = 0x0

.field public static final VERIFY_ONLY_KNOWN:I = 0x2

.field public static final VERIFY_SKIP:I = 0x1


# instance fields
.field private final availableStoreNames:Ljava/util/Set;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/Set<",
            "Ljava/lang/String;",
            ">;"
        }
    .end annotation
.end field

.field public final availableStores:Ljava/util/Set;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/Set<",
            "Lorg/onepf/oms/Appstore;",
            ">;"
        }
    .end annotation
.end field

.field public final checkInventory:Z

.field public final checkInventoryTimeoutMs:I

.field public final discoveryTimeoutMs:I

.field public final preferredStoreNames:Ljava/util/Set;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/Set<",
            "Ljava/lang/String;",
            ">;"
        }
    .end annotation
.end field

.field public final samsungCertificationRequestCode:I

.field private final storeKeys:Ljava/util/Map;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            ">;"
        }
    .end annotation
.end field

.field private final storeSearchStrategy:I

.field public final verifyMode:I


# direct methods
.method public constructor <init>()V
    .locals 2

    .line 1691
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    const/4 v0, 0x0

    .line 1653
    iput v0, p0, Lorg/onepf/oms/OpenIabHelper$Options;->discoveryTimeoutMs:I

    .line 1664
    iput v0, p0, Lorg/onepf/oms/OpenIabHelper$Options;->checkInventoryTimeoutMs:I

    .line 1692
    iput-boolean v0, p0, Lorg/onepf/oms/OpenIabHelper$Options;->checkInventory:Z

    .line 1693
    invoke-static {}, Ljava/util/Collections;->emptySet()Ljava/util/Set;

    move-result-object v1

    iput-object v1, p0, Lorg/onepf/oms/OpenIabHelper$Options;->availableStores:Ljava/util/Set;

    .line 1694
    invoke-static {}, Ljava/util/Collections;->emptySet()Ljava/util/Set;

    move-result-object v1

    iput-object v1, p0, Lorg/onepf/oms/OpenIabHelper$Options;->availableStoreNames:Ljava/util/Set;

    .line 1695
    invoke-static {}, Ljava/util/Collections;->emptyMap()Ljava/util/Map;

    move-result-object v1

    iput-object v1, p0, Lorg/onepf/oms/OpenIabHelper$Options;->storeKeys:Ljava/util/Map;

    .line 1696
    invoke-static {}, Ljava/util/Collections;->emptySet()Ljava/util/Set;

    move-result-object v1

    iput-object v1, p0, Lorg/onepf/oms/OpenIabHelper$Options;->preferredStoreNames:Ljava/util/Set;

    const/4 v1, 0x1

    .line 1697
    iput v1, p0, Lorg/onepf/oms/OpenIabHelper$Options;->verifyMode:I

    const/16 v1, 0x383

    .line 1698
    iput v1, p0, Lorg/onepf/oms/OpenIabHelper$Options;->samsungCertificationRequestCode:I

    .line 1699
    iput v0, p0, Lorg/onepf/oms/OpenIabHelper$Options;->storeSearchStrategy:I

    return-void
.end method

.method private constructor <init>(Ljava/util/Set;Ljava/util/Set;Ljava/util/Map;ZILjava/util/Set;II)V
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/Set<",
            "Lorg/onepf/oms/Appstore;",
            ">;",
            "Ljava/util/Set<",
            "Ljava/lang/String;",
            ">;",
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            ">;ZI",
            "Ljava/util/Set<",
            "Ljava/lang/String;",
            ">;II)V"
        }
    .end annotation

    .line 1709
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    const/4 v0, 0x0

    .line 1653
    iput v0, p0, Lorg/onepf/oms/OpenIabHelper$Options;->discoveryTimeoutMs:I

    .line 1664
    iput v0, p0, Lorg/onepf/oms/OpenIabHelper$Options;->checkInventoryTimeoutMs:I

    .line 1710
    iput-boolean p4, p0, Lorg/onepf/oms/OpenIabHelper$Options;->checkInventory:Z

    .line 1711
    iput-object p1, p0, Lorg/onepf/oms/OpenIabHelper$Options;->availableStores:Ljava/util/Set;

    .line 1712
    iput-object p2, p0, Lorg/onepf/oms/OpenIabHelper$Options;->availableStoreNames:Ljava/util/Set;

    .line 1713
    iput-object p3, p0, Lorg/onepf/oms/OpenIabHelper$Options;->storeKeys:Ljava/util/Map;

    .line 1714
    iput-object p6, p0, Lorg/onepf/oms/OpenIabHelper$Options;->preferredStoreNames:Ljava/util/Set;

    .line 1715
    iput p5, p0, Lorg/onepf/oms/OpenIabHelper$Options;->verifyMode:I

    .line 1716
    iput p7, p0, Lorg/onepf/oms/OpenIabHelper$Options;->samsungCertificationRequestCode:I

    .line 1717
    iput p8, p0, Lorg/onepf/oms/OpenIabHelper$Options;->storeSearchStrategy:I

    return-void
.end method

.method synthetic constructor <init>(Ljava/util/Set;Ljava/util/Set;Ljava/util/Map;ZILjava/util/Set;IILorg/onepf/oms/OpenIabHelper$1;)V
    .locals 0

    .line 1591
    invoke-direct/range {p0 .. p8}, Lorg/onepf/oms/OpenIabHelper$Options;-><init>(Ljava/util/Set;Ljava/util/Set;Ljava/util/Map;ZILjava/util/Set;II)V

    return-void
.end method


# virtual methods
.method public getAvailableStoreByName(Ljava/lang/String;)Lorg/onepf/oms/Appstore;
    .locals 3
    .param p1    # Ljava/lang/String;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param
    .annotation build Lorg/jetbrains/annotations/Nullable;
    .end annotation

    .line 1822
    iget-object v0, p0, Lorg/onepf/oms/OpenIabHelper$Options;->availableStores:Ljava/util/Set;

    invoke-interface {v0}, Ljava/util/Set;->iterator()Ljava/util/Iterator;

    move-result-object v0

    :cond_0
    invoke-interface {v0}, Ljava/util/Iterator;->hasNext()Z

    move-result v1

    if-eqz v1, :cond_1

    invoke-interface {v0}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Lorg/onepf/oms/Appstore;

    .line 1823
    invoke-interface {v1}, Lorg/onepf/oms/Appstore;->getAppstoreName()Ljava/lang/String;

    move-result-object v2

    invoke-virtual {p1, v2}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v2

    if-eqz v2, :cond_0

    return-object v1

    :cond_1
    const/4 p1, 0x0

    return-object p1
.end method

.method public getAvailableStoreNames()Ljava/util/Set;
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()",
            "Ljava/util/Set<",
            "Ljava/lang/String;",
            ">;"
        }
    .end annotation

    .line 1787
    iget-object v0, p0, Lorg/onepf/oms/OpenIabHelper$Options;->availableStoreNames:Ljava/util/Set;

    return-object v0
.end method

.method public getAvailableStores()Ljava/util/Set;
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()",
            "Ljava/util/Set<",
            "Lorg/onepf/oms/Appstore;",
            ">;"
        }
    .end annotation

    .annotation build Lorg/jetbrains/annotations/NotNull;
    .end annotation

    .line 1779
    iget-object v0, p0, Lorg/onepf/oms/OpenIabHelper$Options;->availableStores:Ljava/util/Set;

    return-object v0
.end method

.method public getCheckInventoryTimeout()J
    .locals 2
    .annotation runtime Ljava/lang/Deprecated;
    .end annotation

    const-wide/16 v0, 0x0

    return-wide v0
.end method

.method public getDiscoveryTimeout()J
    .locals 2
    .annotation runtime Ljava/lang/Deprecated;
    .end annotation

    const-wide/16 v0, 0x0

    return-wide v0
.end method

.method public getPreferredStoreNames()Ljava/util/Set;
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()",
            "Ljava/util/Set<",
            "Ljava/lang/String;",
            ">;"
        }
    .end annotation

    .annotation build Lorg/jetbrains/annotations/NotNull;
    .end annotation

    .line 1798
    iget-object v0, p0, Lorg/onepf/oms/OpenIabHelper$Options;->preferredStoreNames:Ljava/util/Set;

    return-object v0
.end method

.method public getSamsungCertificationRequestCode()I
    .locals 1

    .line 1724
    iget v0, p0, Lorg/onepf/oms/OpenIabHelper$Options;->samsungCertificationRequestCode:I

    return v0
.end method

.method public getStoreKeys()Ljava/util/Map;
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()",
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            ">;"
        }
    .end annotation

    .annotation build Lorg/jetbrains/annotations/NotNull;
    .end annotation

    .line 1808
    iget-object v0, p0, Lorg/onepf/oms/OpenIabHelper$Options;->storeKeys:Ljava/util/Map;

    return-object v0
.end method

.method public getStoreSearchStrategy()I
    .locals 1

    .line 1746
    iget v0, p0, Lorg/onepf/oms/OpenIabHelper$Options;->storeSearchStrategy:I

    return v0
.end method

.method public getVerifyMode()I
    .locals 1

    .line 1735
    iget v0, p0, Lorg/onepf/oms/OpenIabHelper$Options;->verifyMode:I

    return v0
.end method

.method public isCheckInventory()Z
    .locals 1

    .line 1753
    iget-boolean v0, p0, Lorg/onepf/oms/OpenIabHelper$Options;->checkInventory:Z

    return v0
.end method

.method public toString()Ljava/lang/String;
    .locals 3

    .line 2079
    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    const-string v1, "Options={availableStores="

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v1, p0, Lorg/onepf/oms/OpenIabHelper$Options;->availableStores:Ljava/util/Set;

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/Object;)Ljava/lang/StringBuilder;

    const-string v1, ", availableStoreNames="

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v1, p0, Lorg/onepf/oms/OpenIabHelper$Options;->availableStoreNames:Ljava/util/Set;

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/Object;)Ljava/lang/StringBuilder;

    const-string v1, ", preferredStoreNames="

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v1, p0, Lorg/onepf/oms/OpenIabHelper$Options;->preferredStoreNames:Ljava/util/Set;

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/Object;)Ljava/lang/StringBuilder;

    const-string v1, ", discoveryTimeoutMs="

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const/4 v1, 0x0

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    const-string v2, ", checkInventory="

    invoke-virtual {v0, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-boolean v2, p0, Lorg/onepf/oms/OpenIabHelper$Options;->checkInventory:Z

    invoke-virtual {v0, v2}, Ljava/lang/StringBuilder;->append(Z)Ljava/lang/StringBuilder;

    const-string v2, ", checkInventoryTimeoutMs="

    invoke-virtual {v0, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    const-string v1, ", verifyMode="

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget v1, p0, Lorg/onepf/oms/OpenIabHelper$Options;->verifyMode:I

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    const-string v1, ", storeSearchStrategy="

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget v1, p0, Lorg/onepf/oms/OpenIabHelper$Options;->storeSearchStrategy:I

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    const-string v1, ", storeKeys="

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v1, p0, Lorg/onepf/oms/OpenIabHelper$Options;->storeKeys:Ljava/util/Map;

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/Object;)Ljava/lang/StringBuilder;

    const-string v1, ", samsungCertificationRequestCode="

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget v1, p0, Lorg/onepf/oms/OpenIabHelper$Options;->samsungCertificationRequestCode:I

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    const/16 v1, 0x7d

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v0

    return-object v0
.end method
