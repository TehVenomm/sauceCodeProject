.class final Lcom/google/common/collect/CustomConcurrentHashMap$Impl$EntrySet;
.super Ljava/util/AbstractSet;
.source "CustomConcurrentHashMap.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lcom/google/common/collect/CustomConcurrentHashMap$Impl;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x10
    name = "EntrySet"
.end annotation

.annotation system Ldalvik/annotation/Signature;
    value = {
        "Ljava/util/AbstractSet<",
        "Ljava/util/Map$Entry<",
        "TK;TV;>;>;"
    }
.end annotation


# instance fields
.field final synthetic this$1:Lcom/google/common/collect/CustomConcurrentHashMap$Impl;


# direct methods
.method constructor <init>(Lcom/google/common/collect/CustomConcurrentHashMap$Impl;)V
    .locals 0

    .line 1901
    iput-object p1, p0, Lcom/google/common/collect/CustomConcurrentHashMap$Impl$EntrySet;->this$1:Lcom/google/common/collect/CustomConcurrentHashMap$Impl;

    invoke-direct {p0}, Ljava/util/AbstractSet;-><init>()V

    return-void
.end method


# virtual methods
.method public clear()V
    .locals 1

    .line 1945
    iget-object v0, p0, Lcom/google/common/collect/CustomConcurrentHashMap$Impl$EntrySet;->this$1:Lcom/google/common/collect/CustomConcurrentHashMap$Impl;

    invoke-virtual {v0}, Lcom/google/common/collect/CustomConcurrentHashMap$Impl;->clear()V

    return-void
.end method

.method public contains(Ljava/lang/Object;)Z
    .locals 3

    .line 1910
    instance-of v0, p1, Ljava/util/Map$Entry;

    const/4 v1, 0x0

    if-nez v0, :cond_0

    return v1

    .line 1913
    :cond_0
    check-cast p1, Ljava/util/Map$Entry;

    .line 1914
    invoke-interface {p1}, Ljava/util/Map$Entry;->getKey()Ljava/lang/Object;

    move-result-object v0

    if-nez v0, :cond_1

    return v1

    .line 1918
    :cond_1
    iget-object v2, p0, Lcom/google/common/collect/CustomConcurrentHashMap$Impl$EntrySet;->this$1:Lcom/google/common/collect/CustomConcurrentHashMap$Impl;

    invoke-virtual {v2, v0}, Lcom/google/common/collect/CustomConcurrentHashMap$Impl;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v0

    if-eqz v0, :cond_2

    .line 1920
    iget-object v2, p0, Lcom/google/common/collect/CustomConcurrentHashMap$Impl$EntrySet;->this$1:Lcom/google/common/collect/CustomConcurrentHashMap$Impl;

    iget-object v2, v2, Lcom/google/common/collect/CustomConcurrentHashMap$Impl;->strategy:Lcom/google/common/collect/CustomConcurrentHashMap$Strategy;

    invoke-interface {p1}, Ljava/util/Map$Entry;->getValue()Ljava/lang/Object;

    move-result-object p1

    invoke-interface {v2, v0, p1}, Lcom/google/common/collect/CustomConcurrentHashMap$Strategy;->equalValues(Ljava/lang/Object;Ljava/lang/Object;)Z

    move-result p1

    if-eqz p1, :cond_2

    const/4 p1, 0x1

    return p1

    :cond_2
    return v1
.end method

.method public isEmpty()Z
    .locals 1

    .line 1940
    iget-object v0, p0, Lcom/google/common/collect/CustomConcurrentHashMap$Impl$EntrySet;->this$1:Lcom/google/common/collect/CustomConcurrentHashMap$Impl;

    invoke-virtual {v0}, Lcom/google/common/collect/CustomConcurrentHashMap$Impl;->isEmpty()Z

    move-result v0

    return v0
.end method

.method public iterator()Ljava/util/Iterator;
    .locals 2
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()",
            "Ljava/util/Iterator<",
            "Ljava/util/Map$Entry<",
            "TK;TV;>;>;"
        }
    .end annotation

    .line 1905
    new-instance v0, Lcom/google/common/collect/CustomConcurrentHashMap$Impl$EntryIterator;

    iget-object v1, p0, Lcom/google/common/collect/CustomConcurrentHashMap$Impl$EntrySet;->this$1:Lcom/google/common/collect/CustomConcurrentHashMap$Impl;

    invoke-direct {v0, v1}, Lcom/google/common/collect/CustomConcurrentHashMap$Impl$EntryIterator;-><init>(Lcom/google/common/collect/CustomConcurrentHashMap$Impl;)V

    return-object v0
.end method

.method public remove(Ljava/lang/Object;)Z
    .locals 3

    .line 1925
    instance-of v0, p1, Ljava/util/Map$Entry;

    const/4 v1, 0x0

    if-nez v0, :cond_0

    return v1

    .line 1928
    :cond_0
    check-cast p1, Ljava/util/Map$Entry;

    .line 1929
    invoke-interface {p1}, Ljava/util/Map$Entry;->getKey()Ljava/lang/Object;

    move-result-object v0

    if-eqz v0, :cond_1

    .line 1930
    iget-object v2, p0, Lcom/google/common/collect/CustomConcurrentHashMap$Impl$EntrySet;->this$1:Lcom/google/common/collect/CustomConcurrentHashMap$Impl;

    invoke-interface {p1}, Ljava/util/Map$Entry;->getValue()Ljava/lang/Object;

    move-result-object p1

    invoke-virtual {v2, v0, p1}, Lcom/google/common/collect/CustomConcurrentHashMap$Impl;->remove(Ljava/lang/Object;Ljava/lang/Object;)Z

    move-result p1

    if-eqz p1, :cond_1

    const/4 p1, 0x1

    return p1

    :cond_1
    return v1
.end method

.method public size()I
    .locals 1

    .line 1935
    iget-object v0, p0, Lcom/google/common/collect/CustomConcurrentHashMap$Impl$EntrySet;->this$1:Lcom/google/common/collect/CustomConcurrentHashMap$Impl;

    invoke-virtual {v0}, Lcom/google/common/collect/CustomConcurrentHashMap$Impl;->size()I

    move-result v0

    return v0
.end method
