.class enum Lcom/google/common/collect/MapMaker$Strength$1;
.super Lcom/google/common/collect/MapMaker$Strength;
.source "MapMaker.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lcom/google/common/collect/MapMaker$Strength;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x4000
    name = null
.end annotation


# direct methods
.method constructor <init>(Ljava/lang/String;I)V
    .locals 1

    const/4 v0, 0x0

    .line 332
    invoke-direct {p0, p1, p2, v0}, Lcom/google/common/collect/MapMaker$Strength;-><init>(Ljava/lang/String;ILcom/google/common/collect/MapMaker$Strength;)V

    return-void
.end method


# virtual methods
.method copyEntry(Ljava/lang/Object;Lcom/google/common/collect/MapMaker$ReferenceEntry;Lcom/google/common/collect/MapMaker$ReferenceEntry;)Lcom/google/common/collect/MapMaker$ReferenceEntry;
    .locals 2
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "<K:",
            "Ljava/lang/Object;",
            "V:",
            "Ljava/lang/Object;",
            ">(TK;",
            "Lcom/google/common/collect/MapMaker$ReferenceEntry<",
            "TK;TV;>;",
            "Lcom/google/common/collect/MapMaker$ReferenceEntry<",
            "TK;TV;>;)",
            "Lcom/google/common/collect/MapMaker$ReferenceEntry<",
            "TK;TV;>;"
        }
    .end annotation

    .line 358
    check-cast p2, Lcom/google/common/collect/MapMaker$WeakEntry;

    if-nez p3, :cond_0

    .line 359
    new-instance p3, Lcom/google/common/collect/MapMaker$WeakEntry;

    iget-object v0, p2, Lcom/google/common/collect/MapMaker$WeakEntry;->internals:Lcom/google/common/collect/CustomConcurrentHashMap$Internals;

    iget p2, p2, Lcom/google/common/collect/MapMaker$WeakEntry;->hash:I

    invoke-direct {p3, v0, p1, p2}, Lcom/google/common/collect/MapMaker$WeakEntry;-><init>(Lcom/google/common/collect/CustomConcurrentHashMap$Internals;Ljava/lang/Object;I)V

    goto :goto_0

    .line 360
    :cond_0
    new-instance v0, Lcom/google/common/collect/MapMaker$LinkedWeakEntry;

    iget-object v1, p2, Lcom/google/common/collect/MapMaker$WeakEntry;->internals:Lcom/google/common/collect/CustomConcurrentHashMap$Internals;

    iget p2, p2, Lcom/google/common/collect/MapMaker$WeakEntry;->hash:I

    invoke-direct {v0, v1, p1, p2, p3}, Lcom/google/common/collect/MapMaker$LinkedWeakEntry;-><init>(Lcom/google/common/collect/CustomConcurrentHashMap$Internals;Ljava/lang/Object;ILcom/google/common/collect/MapMaker$ReferenceEntry;)V

    move-object p3, v0

    :goto_0
    return-object p3
.end method

.method equal(Ljava/lang/Object;Ljava/lang/Object;)Z
    .locals 0

    if-ne p1, p2, :cond_0

    const/4 p1, 0x1

    return p1

    :cond_0
    const/4 p1, 0x0

    return p1
.end method

.method hash(Ljava/lang/Object;)I
    .locals 0

    .line 340
    invoke-static {p1}, Ljava/lang/System;->identityHashCode(Ljava/lang/Object;)I

    move-result p1

    return p1
.end method

.method newEntry(Lcom/google/common/collect/CustomConcurrentHashMap$Internals;Ljava/lang/Object;ILcom/google/common/collect/MapMaker$ReferenceEntry;)Lcom/google/common/collect/MapMaker$ReferenceEntry;
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "<K:",
            "Ljava/lang/Object;",
            "V:",
            "Ljava/lang/Object;",
            ">(",
            "Lcom/google/common/collect/CustomConcurrentHashMap$Internals<",
            "TK;TV;",
            "Lcom/google/common/collect/MapMaker$ReferenceEntry<",
            "TK;TV;>;>;TK;I",
            "Lcom/google/common/collect/MapMaker$ReferenceEntry<",
            "TK;TV;>;)",
            "Lcom/google/common/collect/MapMaker$ReferenceEntry<",
            "TK;TV;>;"
        }
    .end annotation

    if-nez p4, :cond_0

    .line 351
    new-instance p4, Lcom/google/common/collect/MapMaker$WeakEntry;

    invoke-direct {p4, p1, p2, p3}, Lcom/google/common/collect/MapMaker$WeakEntry;-><init>(Lcom/google/common/collect/CustomConcurrentHashMap$Internals;Ljava/lang/Object;I)V

    goto :goto_0

    .line 352
    :cond_0
    new-instance v0, Lcom/google/common/collect/MapMaker$LinkedWeakEntry;

    invoke-direct {v0, p1, p2, p3, p4}, Lcom/google/common/collect/MapMaker$LinkedWeakEntry;-><init>(Lcom/google/common/collect/CustomConcurrentHashMap$Internals;Ljava/lang/Object;ILcom/google/common/collect/MapMaker$ReferenceEntry;)V

    move-object p4, v0

    :goto_0
    return-object p4
.end method

.method referenceValue(Lcom/google/common/collect/MapMaker$ReferenceEntry;Ljava/lang/Object;)Lcom/google/common/collect/MapMaker$ValueReference;
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "<K:",
            "Ljava/lang/Object;",
            "V:",
            "Ljava/lang/Object;",
            ">(",
            "Lcom/google/common/collect/MapMaker$ReferenceEntry<",
            "TK;TV;>;TV;)",
            "Lcom/google/common/collect/MapMaker$ValueReference<",
            "TK;TV;>;"
        }
    .end annotation

    .line 345
    new-instance v0, Lcom/google/common/collect/MapMaker$WeakValueReference;

    invoke-direct {v0, p2, p1}, Lcom/google/common/collect/MapMaker$WeakValueReference;-><init>(Ljava/lang/Object;Lcom/google/common/collect/MapMaker$ReferenceEntry;)V

    return-object v0
.end method
