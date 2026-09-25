.class public Lorg/onepf/oms/appstore/googleUtils/Inventory;
.super Ljava/lang/Object;
.source "Inventory.java"


# instance fields
.field private final mPurchaseMap:Ljava/util/Map;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Lorg/onepf/oms/appstore/googleUtils/Purchase;",
            ">;"
        }
    .end annotation
.end field

.field private final mSkuMap:Ljava/util/Map;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Lorg/onepf/oms/appstore/googleUtils/SkuDetails;",
            ">;"
        }
    .end annotation
.end field


# direct methods
.method public constructor <init>()V
    .locals 1

    .line 35
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 32
    new-instance v0, Ljava/util/concurrent/ConcurrentHashMap;

    invoke-direct {v0}, Ljava/util/concurrent/ConcurrentHashMap;-><init>()V

    iput-object v0, p0, Lorg/onepf/oms/appstore/googleUtils/Inventory;->mSkuMap:Ljava/util/Map;

    .line 33
    new-instance v0, Ljava/util/concurrent/ConcurrentHashMap;

    invoke-direct {v0}, Ljava/util/concurrent/ConcurrentHashMap;-><init>()V

    iput-object v0, p0, Lorg/onepf/oms/appstore/googleUtils/Inventory;->mPurchaseMap:Ljava/util/Map;

    return-void
.end method


# virtual methods
.method public addPurchase(Lorg/onepf/oms/appstore/googleUtils/Purchase;)V
    .locals 2
    .param p1    # Lorg/onepf/oms/appstore/googleUtils/Purchase;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param

    .line 111
    iget-object v0, p0, Lorg/onepf/oms/appstore/googleUtils/Inventory;->mPurchaseMap:Ljava/util/Map;

    invoke-virtual {p1}, Lorg/onepf/oms/appstore/googleUtils/Purchase;->getSku()Ljava/lang/String;

    move-result-object v1

    invoke-interface {v0, v1, p1}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    return-void
.end method

.method public addSkuDetails(Lorg/onepf/oms/appstore/googleUtils/SkuDetails;)V
    .locals 2
    .param p1    # Lorg/onepf/oms/appstore/googleUtils/SkuDetails;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param

    .line 107
    iget-object v0, p0, Lorg/onepf/oms/appstore/googleUtils/Inventory;->mSkuMap:Ljava/util/Map;

    invoke-virtual {p1}, Lorg/onepf/oms/appstore/googleUtils/SkuDetails;->getSku()Ljava/lang/String;

    move-result-object v1

    invoke-interface {v0, v1, p1}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    return-void
.end method

.method public erasePurchase(Ljava/lang/String;)V
    .locals 1

    .line 75
    iget-object v0, p0, Lorg/onepf/oms/appstore/googleUtils/Inventory;->mPurchaseMap:Ljava/util/Map;

    invoke-interface {v0, p1}, Ljava/util/Map;->containsKey(Ljava/lang/Object;)Z

    move-result v0

    if-eqz v0, :cond_0

    iget-object v0, p0, Lorg/onepf/oms/appstore/googleUtils/Inventory;->mPurchaseMap:Ljava/util/Map;

    invoke-interface {v0, p1}, Ljava/util/Map;->remove(Ljava/lang/Object;)Ljava/lang/Object;

    :cond_0
    return-void
.end method

.method public getAllOwnedSkus()Ljava/util/List;
    .locals 2
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()",
            "Ljava/util/List<",
            "Ljava/lang/String;",
            ">;"
        }
    .end annotation

    .annotation build Lorg/jetbrains/annotations/NotNull;
    .end annotation

    .line 83
    new-instance v0, Ljava/util/ArrayList;

    iget-object v1, p0, Lorg/onepf/oms/appstore/googleUtils/Inventory;->mPurchaseMap:Ljava/util/Map;

    invoke-interface {v1}, Ljava/util/Map;->keySet()Ljava/util/Set;

    move-result-object v1

    invoke-direct {v0, v1}, Ljava/util/ArrayList;-><init>(Ljava/util/Collection;)V

    return-object v0
.end method

.method public getAllOwnedSkus(Ljava/lang/String;)Ljava/util/List;
    .locals 4
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/lang/String;",
            ")",
            "Ljava/util/List<",
            "Ljava/lang/String;",
            ">;"
        }
    .end annotation

    .annotation build Lorg/jetbrains/annotations/NotNull;
    .end annotation

    .line 91
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    .line 92
    iget-object v1, p0, Lorg/onepf/oms/appstore/googleUtils/Inventory;->mPurchaseMap:Ljava/util/Map;

    invoke-interface {v1}, Ljava/util/Map;->values()Ljava/util/Collection;

    move-result-object v1

    invoke-interface {v1}, Ljava/util/Collection;->iterator()Ljava/util/Iterator;

    move-result-object v1

    :cond_0
    :goto_0
    invoke-interface {v1}, Ljava/util/Iterator;->hasNext()Z

    move-result v2

    if-eqz v2, :cond_1

    invoke-interface {v1}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Lorg/onepf/oms/appstore/googleUtils/Purchase;

    .line 93
    invoke-virtual {v2}, Lorg/onepf/oms/appstore/googleUtils/Purchase;->getItemType()Ljava/lang/String;

    move-result-object v3

    invoke-virtual {v3, p1}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v3

    if-eqz v3, :cond_0

    invoke-virtual {v2}, Lorg/onepf/oms/appstore/googleUtils/Purchase;->getSku()Ljava/lang/String;

    move-result-object v2

    invoke-interface {v0, v2}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    goto :goto_0

    :cond_1
    return-object v0
.end method

.method public getAllPurchases()Ljava/util/List;
    .locals 2
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()",
            "Ljava/util/List<",
            "Lorg/onepf/oms/appstore/googleUtils/Purchase;",
            ">;"
        }
    .end annotation

    .annotation build Lorg/jetbrains/annotations/NotNull;
    .end annotation

    .line 103
    new-instance v0, Ljava/util/ArrayList;

    iget-object v1, p0, Lorg/onepf/oms/appstore/googleUtils/Inventory;->mPurchaseMap:Ljava/util/Map;

    invoke-interface {v1}, Ljava/util/Map;->values()Ljava/util/Collection;

    move-result-object v1

    invoke-direct {v0, v1}, Ljava/util/ArrayList;-><init>(Ljava/util/Collection;)V

    return-object v0
.end method

.method public getPurchase(Ljava/lang/String;)Lorg/onepf/oms/appstore/googleUtils/Purchase;
    .locals 1

    .line 49
    iget-object v0, p0, Lorg/onepf/oms/appstore/googleUtils/Inventory;->mPurchaseMap:Ljava/util/Map;

    invoke-interface {v0, p1}, Ljava/util/Map;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object p1

    check-cast p1, Lorg/onepf/oms/appstore/googleUtils/Purchase;

    return-object p1
.end method

.method public getPurchaseMap()Ljava/util/Map;
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()",
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Lorg/onepf/oms/appstore/googleUtils/Purchase;",
            ">;"
        }
    .end annotation

    .line 119
    iget-object v0, p0, Lorg/onepf/oms/appstore/googleUtils/Inventory;->mPurchaseMap:Ljava/util/Map;

    invoke-static {v0}, Ljava/util/Collections;->unmodifiableMap(Ljava/util/Map;)Ljava/util/Map;

    move-result-object v0

    return-object v0
.end method

.method public getSkuDetails(Ljava/lang/String;)Lorg/onepf/oms/appstore/googleUtils/SkuDetails;
    .locals 1

    .line 42
    iget-object v0, p0, Lorg/onepf/oms/appstore/googleUtils/Inventory;->mSkuMap:Ljava/util/Map;

    invoke-interface {v0, p1}, Ljava/util/Map;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object p1

    check-cast p1, Lorg/onepf/oms/appstore/googleUtils/SkuDetails;

    return-object p1
.end method

.method public getSkuMap()Ljava/util/Map;
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()",
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Lorg/onepf/oms/appstore/googleUtils/SkuDetails;",
            ">;"
        }
    .end annotation

    .line 115
    iget-object v0, p0, Lorg/onepf/oms/appstore/googleUtils/Inventory;->mSkuMap:Ljava/util/Map;

    invoke-static {v0}, Ljava/util/Collections;->unmodifiableMap(Ljava/util/Map;)Ljava/util/Map;

    move-result-object v0

    return-object v0
.end method

.method public hasDetails(Ljava/lang/String;)Z
    .locals 1

    .line 63
    iget-object v0, p0, Lorg/onepf/oms/appstore/googleUtils/Inventory;->mSkuMap:Ljava/util/Map;

    invoke-interface {v0, p1}, Ljava/util/Map;->containsKey(Ljava/lang/Object;)Z

    move-result p1

    return p1
.end method

.method public hasPurchase(Ljava/lang/String;)Z
    .locals 1

    .line 56
    iget-object v0, p0, Lorg/onepf/oms/appstore/googleUtils/Inventory;->mPurchaseMap:Ljava/util/Map;

    invoke-interface {v0, p1}, Ljava/util/Map;->containsKey(Ljava/lang/Object;)Z

    move-result p1

    return p1
.end method
