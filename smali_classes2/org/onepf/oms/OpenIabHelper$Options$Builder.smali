.class public final Lorg/onepf/oms/OpenIabHelper$Options$Builder;
.super Ljava/lang/Object;
.source "OpenIabHelper.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lorg/onepf/oms/OpenIabHelper$Options;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x19
    name = "Builder"
.end annotation


# instance fields
.field private final availableStores:Ljava/util/Set;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/Set<",
            "Lorg/onepf/oms/Appstore;",
            ">;"
        }
    .end annotation
.end field

.field private final availableStoresNames:Ljava/util/Set;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/Set<",
            "Ljava/lang/String;",
            ">;"
        }
    .end annotation
.end field

.field private checkInventory:Z

.field private final preferredStoreNames:Ljava/util/Set;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/Set<",
            "Ljava/lang/String;",
            ">;"
        }
    .end annotation
.end field

.field private samsungCertificationRequestCode:I

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

.field private storeSearchStrategy:I

.field private verifyMode:I


# direct methods
.method public constructor <init>()V
    .locals 2

    .line 1833
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 1835
    new-instance v0, Ljava/util/LinkedHashSet;

    invoke-direct {v0}, Ljava/util/LinkedHashSet;-><init>()V

    iput-object v0, p0, Lorg/onepf/oms/OpenIabHelper$Options$Builder;->preferredStoreNames:Ljava/util/Set;

    .line 1836
    new-instance v0, Ljava/util/HashSet;

    invoke-direct {v0}, Ljava/util/HashSet;-><init>()V

    iput-object v0, p0, Lorg/onepf/oms/OpenIabHelper$Options$Builder;->availableStores:Ljava/util/Set;

    .line 1837
    new-instance v0, Ljava/util/LinkedHashSet;

    invoke-direct {v0}, Ljava/util/LinkedHashSet;-><init>()V

    iput-object v0, p0, Lorg/onepf/oms/OpenIabHelper$Options$Builder;->availableStoresNames:Ljava/util/Set;

    .line 1838
    new-instance v0, Ljava/util/HashMap;

    invoke-direct {v0}, Ljava/util/HashMap;-><init>()V

    iput-object v0, p0, Lorg/onepf/oms/OpenIabHelper$Options$Builder;->storeKeys:Ljava/util/Map;

    const/4 v0, 0x0

    .line 1839
    iput-boolean v0, p0, Lorg/onepf/oms/OpenIabHelper$Options$Builder;->checkInventory:Z

    const/16 v1, 0x383

    .line 1840
    iput v1, p0, Lorg/onepf/oms/OpenIabHelper$Options$Builder;->samsungCertificationRequestCode:I

    .line 1843
    iput v0, p0, Lorg/onepf/oms/OpenIabHelper$Options$Builder;->verifyMode:I

    .line 1846
    iput v0, p0, Lorg/onepf/oms/OpenIabHelper$Options$Builder;->storeSearchStrategy:I

    return-void
.end method


# virtual methods
.method public addAvailableStoreNames(Ljava/util/Collection;)Lorg/onepf/oms/OpenIabHelper$Options$Builder;
    .locals 1
    .param p1    # Ljava/util/Collection;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/Collection<",
            "Ljava/lang/String;",
            ">;)",
            "Lorg/onepf/oms/OpenIabHelper$Options$Builder;"
        }
    .end annotation

    .annotation build Lorg/jetbrains/annotations/NotNull;
    .end annotation

    .line 1895
    iget-object v0, p0, Lorg/onepf/oms/OpenIabHelper$Options$Builder;->availableStoresNames:Ljava/util/Set;

    invoke-interface {v0, p1}, Ljava/util/Set;->addAll(Ljava/util/Collection;)Z

    return-object p0
.end method

.method public varargs addAvailableStoreNames([Ljava/lang/String;)Lorg/onepf/oms/OpenIabHelper$Options$Builder;
    .locals 0
    .param p1    # [Ljava/lang/String;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param
    .annotation build Lorg/jetbrains/annotations/NotNull;
    .end annotation

    .line 1882
    invoke-static {p1}, Ljava/util/Arrays;->asList([Ljava/lang/Object;)Ljava/util/List;

    move-result-object p1

    invoke-virtual {p0, p1}, Lorg/onepf/oms/OpenIabHelper$Options$Builder;->addAvailableStoreNames(Ljava/util/Collection;)Lorg/onepf/oms/OpenIabHelper$Options$Builder;

    return-object p0
.end method

.method public addAvailableStores(Ljava/util/Collection;)Lorg/onepf/oms/OpenIabHelper$Options$Builder;
    .locals 1
    .param p1    # Ljava/util/Collection;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/Collection<",
            "Lorg/onepf/oms/Appstore;",
            ">;)",
            "Lorg/onepf/oms/OpenIabHelper$Options$Builder;"
        }
    .end annotation

    .annotation build Lorg/jetbrains/annotations/NotNull;
    .end annotation

    .line 1870
    iget-object v0, p0, Lorg/onepf/oms/OpenIabHelper$Options$Builder;->availableStores:Ljava/util/Set;

    invoke-interface {v0, p1}, Ljava/util/Set;->addAll(Ljava/util/Collection;)Z

    return-object p0
.end method

.method public varargs addAvailableStores([Lorg/onepf/oms/Appstore;)Lorg/onepf/oms/OpenIabHelper$Options$Builder;
    .locals 0
    .param p1    # [Lorg/onepf/oms/Appstore;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param
    .annotation build Lorg/jetbrains/annotations/NotNull;
    .end annotation

    .line 1857
    invoke-static {p1}, Ljava/util/Arrays;->asList([Ljava/lang/Object;)Ljava/util/List;

    move-result-object p1

    invoke-virtual {p0, p1}, Lorg/onepf/oms/OpenIabHelper$Options$Builder;->addAvailableStores(Ljava/util/Collection;)Lorg/onepf/oms/OpenIabHelper$Options$Builder;

    return-object p0
.end method

.method public addPreferredStoreName(Ljava/util/Collection;)Lorg/onepf/oms/OpenIabHelper$Options$Builder;
    .locals 1
    .param p1    # Ljava/util/Collection;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/Collection<",
            "Ljava/lang/String;",
            ">;)",
            "Lorg/onepf/oms/OpenIabHelper$Options$Builder;"
        }
    .end annotation

    .annotation build Lorg/jetbrains/annotations/NotNull;
    .end annotation

    .line 2035
    iget-object v0, p0, Lorg/onepf/oms/OpenIabHelper$Options$Builder;->preferredStoreNames:Ljava/util/Set;

    invoke-interface {v0, p1}, Ljava/util/Set;->addAll(Ljava/util/Collection;)Z

    return-object p0
.end method

.method public varargs addPreferredStoreName([Ljava/lang/String;)Lorg/onepf/oms/OpenIabHelper$Options$Builder;
    .locals 0
    .param p1    # [Ljava/lang/String;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param
    .annotation build Lorg/jetbrains/annotations/NotNull;
    .end annotation

    .line 2022
    invoke-static {p1}, Ljava/util/Arrays;->asList([Ljava/lang/Object;)Ljava/util/List;

    move-result-object p1

    invoke-virtual {p0, p1}, Lorg/onepf/oms/OpenIabHelper$Options$Builder;->addPreferredStoreName(Ljava/util/Collection;)Lorg/onepf/oms/OpenIabHelper$Options$Builder;

    return-object p0
.end method

.method public addStoreKey(Ljava/lang/String;Ljava/lang/String;)Lorg/onepf/oms/OpenIabHelper$Options$Builder;
    .locals 4
    .param p1    # Ljava/lang/String;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param
    .param p2    # Ljava/lang/String;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param
    .annotation build Lorg/jetbrains/annotations/NotNull;
    .end annotation

    .line 1946
    :try_start_0
    invoke-static {p2}, Lorg/onepf/oms/appstore/googleUtils/Security;->generatePublicKey(Ljava/lang/String;)Ljava/security/PublicKey;
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    .line 1953
    iget-object v0, p0, Lorg/onepf/oms/OpenIabHelper$Options$Builder;->storeKeys:Ljava/util/Map;

    invoke-interface {v0, p1, p2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    return-object p0

    :catch_0
    move-exception v0

    .line 1948
    new-instance v1, Ljava/lang/IllegalArgumentException;

    const/4 v2, 0x2

    new-array v2, v2, [Ljava/lang/Object;

    const/4 v3, 0x0

    aput-object p1, v2, v3

    const/4 p1, 0x1

    aput-object p2, v2, p1

    const-string p1, "Invalid publicKey for store: %s, key: %s."

    invoke-static {p1, v2}, Ljava/lang/String;->format(Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object p1

    invoke-direct {v1, p1, v0}, Ljava/lang/IllegalArgumentException;-><init>(Ljava/lang/String;Ljava/lang/Throwable;)V

    throw v1
.end method

.method public addStoreKeys(Ljava/util/Map;)Lorg/onepf/oms/OpenIabHelper$Options$Builder;
    .locals 4
    .param p1    # Ljava/util/Map;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            ">;)",
            "Lorg/onepf/oms/OpenIabHelper$Options$Builder;"
        }
    .end annotation

    .annotation build Lorg/jetbrains/annotations/NotNull;
    .end annotation

    .line 1968
    invoke-interface {p1}, Ljava/util/Map;->keySet()Ljava/util/Set;

    move-result-object v0

    invoke-interface {v0}, Ljava/util/Set;->iterator()Ljava/util/Iterator;

    move-result-object v0

    :cond_0
    :goto_0
    invoke-interface {v0}, Ljava/util/Iterator;->hasNext()Z

    move-result v1

    if-eqz v1, :cond_1

    invoke-interface {v0}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Ljava/lang/String;

    .line 1970
    invoke-interface {p1, v1}, Ljava/util/Map;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Ljava/lang/String;

    invoke-static {v2}, Landroid/text/TextUtils;->isEmpty(Ljava/lang/CharSequence;)Z

    move-result v3

    if-nez v3, :cond_0

    .line 1971
    invoke-virtual {p0, v1, v2}, Lorg/onepf/oms/OpenIabHelper$Options$Builder;->addStoreKey(Ljava/lang/String;Ljava/lang/String;)Lorg/onepf/oms/OpenIabHelper$Options$Builder;

    goto :goto_0

    :cond_1
    return-object p0
.end method

.method public build()Lorg/onepf/oms/OpenIabHelper$Options;
    .locals 11
    .annotation build Lorg/jetbrains/annotations/NotNull;
    .end annotation

    .line 2065
    new-instance v10, Lorg/onepf/oms/OpenIabHelper$Options;

    iget-object v0, p0, Lorg/onepf/oms/OpenIabHelper$Options$Builder;->availableStores:Ljava/util/Set;

    invoke-static {v0}, Ljava/util/Collections;->unmodifiableSet(Ljava/util/Set;)Ljava/util/Set;

    move-result-object v1

    iget-object v0, p0, Lorg/onepf/oms/OpenIabHelper$Options$Builder;->availableStoresNames:Ljava/util/Set;

    invoke-static {v0}, Ljava/util/Collections;->unmodifiableSet(Ljava/util/Set;)Ljava/util/Set;

    move-result-object v2

    iget-object v0, p0, Lorg/onepf/oms/OpenIabHelper$Options$Builder;->storeKeys:Ljava/util/Map;

    invoke-static {v0}, Ljava/util/Collections;->unmodifiableMap(Ljava/util/Map;)Ljava/util/Map;

    move-result-object v3

    iget-boolean v4, p0, Lorg/onepf/oms/OpenIabHelper$Options$Builder;->checkInventory:Z

    iget v5, p0, Lorg/onepf/oms/OpenIabHelper$Options$Builder;->verifyMode:I

    iget-object v0, p0, Lorg/onepf/oms/OpenIabHelper$Options$Builder;->preferredStoreNames:Ljava/util/Set;

    invoke-static {v0}, Ljava/util/Collections;->unmodifiableSet(Ljava/util/Set;)Ljava/util/Set;

    move-result-object v6

    iget v7, p0, Lorg/onepf/oms/OpenIabHelper$Options$Builder;->samsungCertificationRequestCode:I

    iget v8, p0, Lorg/onepf/oms/OpenIabHelper$Options$Builder;->storeSearchStrategy:I

    const/4 v9, 0x0

    move-object v0, v10

    invoke-direct/range {v0 .. v9}, Lorg/onepf/oms/OpenIabHelper$Options;-><init>(Ljava/util/Set;Ljava/util/Set;Ljava/util/Map;ZILjava/util/Set;IILorg/onepf/oms/OpenIabHelper$1;)V

    return-object v10
.end method

.method public setCheckInventory(Z)Lorg/onepf/oms/OpenIabHelper$Options$Builder;
    .locals 0
    .annotation build Lorg/jetbrains/annotations/NotNull;
    .end annotation

    .line 1908
    iput-boolean p1, p0, Lorg/onepf/oms/OpenIabHelper$Options$Builder;->checkInventory:Z

    return-object p0
.end method

.method public setCheckInventoryTimeout(I)Lorg/onepf/oms/OpenIabHelper$Options$Builder;
    .locals 0
    .annotation runtime Ljava/lang/Deprecated;
    .end annotation

    .annotation build Lorg/jetbrains/annotations/NotNull;
    .end annotation

    return-object p0
.end method

.method public setDiscoveryTimeout(I)Lorg/onepf/oms/OpenIabHelper$Options$Builder;
    .locals 0
    .annotation runtime Ljava/lang/Deprecated;
    .end annotation

    .annotation build Lorg/jetbrains/annotations/NotNull;
    .end annotation

    return-object p0
.end method

.method public setSamsungCertificationRequestCode(I)Lorg/onepf/oms/OpenIabHelper$Options$Builder;
    .locals 3
    .annotation build Lorg/jetbrains/annotations/NotNull;
    .end annotation

    if-lez p1, :cond_0

    .line 2054
    iput p1, p0, Lorg/onepf/oms/OpenIabHelper$Options$Builder;->samsungCertificationRequestCode:I

    return-object p0

    .line 2050
    :cond_0
    new-instance v0, Ljava/lang/IllegalArgumentException;

    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "Value \'"

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1, p1}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    const-string p1, "\' can\'t be request code. Request code must be a positive value."

    invoke-virtual {v1, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p1

    invoke-direct {v0, p1}, Ljava/lang/IllegalArgumentException;-><init>(Ljava/lang/String;)V

    throw v0
.end method

.method public setStoreSearchStrategy(I)Lorg/onepf/oms/OpenIabHelper$Options$Builder;
    .locals 0
    .annotation build Lorg/jetbrains/annotations/NotNull;
    .end annotation

    .line 2008
    iput p1, p0, Lorg/onepf/oms/OpenIabHelper$Options$Builder;->storeSearchStrategy:I

    return-object p0
.end method

.method public setVerifyMode(I)Lorg/onepf/oms/OpenIabHelper$Options$Builder;
    .locals 0
    .annotation build Lorg/jetbrains/annotations/NotNull;
    .end annotation

    .line 1991
    iput p1, p0, Lorg/onepf/oms/OpenIabHelper$Options$Builder;->verifyMode:I

    return-object p0
.end method
