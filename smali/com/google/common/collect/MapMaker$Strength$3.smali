.class enum Lcom/google/common/collect/MapMaker$Strength$3;
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

    .line 396
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

    .line 422
    check-cast p2, Lcom/google/common/collect/MapMaker$StrongEntry;

    if-nez p3, :cond_0

    .line 423
    new-instance p3, Lcom/google/common/collect/MapMaker$StrongEntry;

    iget-object v0, p2, Lcom/google/common/collect/MapMaker$StrongEntry;->internals:Lcom/google/common/collect/CustomConcurrentHashMap$Internals;

    iget p2, p2, Lcom/google/common/collect/MapMaker$StrongEntry;->hash:I

    invoke-direct {p3, v0, p1, p2}, Lcom/google/common/collect/MapMaker$StrongEntry;-><init>(Lcom/google/common/collect/CustomConcurrentHashMap$Internals;Ljava/lang/Object;I)V

    goto :goto_0

    .line 424
    :cond_0
    new-instance v0, Lcom/google/common/collect/MapMaker$LinkedStrongEntry;

    iget-object v1, p2, Lcom/google/common/collect/MapMaker$StrongEntry;->internals:Lcom/google/common/collect/CustomConcurrentHashMap$Internals;

    iget p2, p2, Lcom/google/common/collect/MapMaker$StrongEntry;->hash:I

    invoke-direct {v0, v1, p1, p2, p3}, Lcom/google/common/collect/MapMaker$LinkedStrongEntry;-><init>(Lcom/google/common/collect/CustomConcurrentHashMap$Internals;Ljava/lang/Object;ILcom/google/common/collect/MapMaker$ReferenceEntry;)V

    move-object p3, v0

    :goto_0
    return-object p3
.end method

.method equal(Ljava/lang/Object;Ljava/lang/Object;)Z
    .locals 0

    .line 399
    invoke-virtual {p1, p2}, Ljava/lang/Object;->equals(Ljava/lang/Object;)Z

    move-result p1

    return p1
.end method

.method hash(Ljava/lang/Object;)I
    .locals 0

    .line 404
    invoke-virtual {p1}, Ljava/lang/Object;->hashCode()I

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

    .line 415
    new-instance p4, Lcom/google/common/collect/MapMaker$StrongEntry;

    invoke-direct {p4, p1, p2, p3}, Lcom/google/common/collect/MapMaker$StrongEntry;-><init>(Lcom/google/common/collect/CustomConcurrentHashMap$Internals;Ljava/lang/Object;I)V

    goto :goto_0

    .line 416
    :cond_0
    new-instance v0, Lcom/google/common/collect/MapMaker$LinkedStrongEntry;

    invoke-direct {v0, p1, p2, p3, p4}, Lcom/google/common/collect/MapMaker$LinkedStrongEntry;-><init>(Lcom/google/common/collect/CustomConcurrentHashMap$Internals;Ljava/lang/Object;ILcom/google/common/collect/MapMaker$ReferenceEntry;)V

    move-object p4, v0

    :goto_0
    return-object p4
.end method

.method referenceValue(Lcom/google/common/collect/MapMaker$ReferenceEntry;Ljava/lang/Object;)Lcom/google/common/collect/MapMaker$ValueReference;
    .locals 0
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

    .line 409
    new-instance p1, Lcom/google/common/collect/MapMaker$StrongValueReference;

    invoke-direct {p1, p2}, Lcom/google/common/collect/MapMaker$StrongValueReference;-><init>(Ljava/lang/Object;)V

    return-object p1
.end method
