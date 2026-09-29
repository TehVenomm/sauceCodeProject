.class abstract Lcom/google/common/collect/CustomConcurrentHashMap$Impl$HashIterator;
.super Ljava/lang/Object;
.source "CustomConcurrentHashMap.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lcom/google/common/collect/CustomConcurrentHashMap$Impl;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x400
    name = "HashIterator"
.end annotation


# instance fields
.field currentTable:Ljava/util/concurrent/atomic/AtomicReferenceArray;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/concurrent/atomic/AtomicReferenceArray<",
            "TE;>;"
        }
    .end annotation
.end field

.field lastReturned:Lcom/google/common/collect/CustomConcurrentHashMap$Impl$WriteThroughEntry;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Lcom/google/common/collect/CustomConcurrentHashMap$Impl<",
            "TK;TV;TE;>.WriteThroughEntry;"
        }
    .end annotation
.end field

.field nextEntry:Ljava/lang/Object;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "TE;"
        }
    .end annotation
.end field

.field nextExternal:Lcom/google/common/collect/CustomConcurrentHashMap$Impl$WriteThroughEntry;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Lcom/google/common/collect/CustomConcurrentHashMap$Impl<",
            "TK;TV;TE;>.WriteThroughEntry;"
        }
    .end annotation
.end field

.field nextSegmentIndex:I

.field nextTableIndex:I

.field final synthetic this$1:Lcom/google/common/collect/CustomConcurrentHashMap$Impl;


# direct methods
.method constructor <init>(Lcom/google/common/collect/CustomConcurrentHashMap$Impl;)V
    .locals 0

    .line 1678
    iput-object p1, p0, Lcom/google/common/collect/CustomConcurrentHashMap$Impl$HashIterator;->this$1:Lcom/google/common/collect/CustomConcurrentHashMap$Impl;

    .line 1674
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 1675
    iget-object p1, p1, Lcom/google/common/collect/CustomConcurrentHashMap$Impl;->segments:[Lcom/google/common/collect/CustomConcurrentHashMap$Impl$Segment;

    array-length p1, p1

    add-int/lit8 p1, p1, -0x1

    iput p1, p0, Lcom/google/common/collect/CustomConcurrentHashMap$Impl$HashIterator;->nextSegmentIndex:I

    const/4 p1, -0x1

    .line 1676
    iput p1, p0, Lcom/google/common/collect/CustomConcurrentHashMap$Impl$HashIterator;->nextTableIndex:I

    .line 1677
    invoke-virtual {p0}, Lcom/google/common/collect/CustomConcurrentHashMap$Impl$HashIterator;->advance()V

    return-void
.end method


# virtual methods
.method final advance()V
    .locals 3

    const/4 v0, 0x0

    .line 1685
    iput-object v0, p0, Lcom/google/common/collect/CustomConcurrentHashMap$Impl$HashIterator;->nextExternal:Lcom/google/common/collect/CustomConcurrentHashMap$Impl$WriteThroughEntry;

    .line 1687
    invoke-virtual {p0}, Lcom/google/common/collect/CustomConcurrentHashMap$Impl$HashIterator;->nextInChain()Z

    move-result v0

    if-eqz v0, :cond_0

    return-void

    .line 1691
    :cond_0
    invoke-virtual {p0}, Lcom/google/common/collect/CustomConcurrentHashMap$Impl$HashIterator;->nextInTable()Z

    move-result v0

    if-eqz v0, :cond_1

    return-void

    .line 1695
    :cond_1
    iget v0, p0, Lcom/google/common/collect/CustomConcurrentHashMap$Impl$HashIterator;->nextSegmentIndex:I

    if-gez v0, :cond_2

    return-void

    .line 1696
    :cond_2
    iget-object v0, p0, Lcom/google/common/collect/CustomConcurrentHashMap$Impl$HashIterator;->this$1:Lcom/google/common/collect/CustomConcurrentHashMap$Impl;

    iget-object v0, v0, Lcom/google/common/collect/CustomConcurrentHashMap$Impl;->segments:[Lcom/google/common/collect/CustomConcurrentHashMap$Impl$Segment;

    iget v1, p0, Lcom/google/common/collect/CustomConcurrentHashMap$Impl$HashIterator;->nextSegmentIndex:I

    add-int/lit8 v2, v1, -0x1

    iput v2, p0, Lcom/google/common/collect/CustomConcurrentHashMap$Impl$HashIterator;->nextSegmentIndex:I

    aget-object v0, v0, v1

    .line 1697
    iget v1, v0, Lcom/google/common/collect/CustomConcurrentHashMap$Impl$Segment;->count:I

    if-eqz v1, :cond_1

    .line 1698
    iget-object v0, v0, Lcom/google/common/collect/CustomConcurrentHashMap$Impl$Segment;->table:Ljava/util/concurrent/atomic/AtomicReferenceArray;

    iput-object v0, p0, Lcom/google/common/collect/CustomConcurrentHashMap$Impl$HashIterator;->currentTable:Ljava/util/concurrent/atomic/AtomicReferenceArray;

    .line 1699
    iget-object v0, p0, Lcom/google/common/collect/CustomConcurrentHashMap$Impl$HashIterator;->currentTable:Ljava/util/concurrent/atomic/AtomicReferenceArray;

    invoke-virtual {v0}, Ljava/util/concurrent/atomic/AtomicReferenceArray;->length()I

    move-result v0

    add-int/lit8 v0, v0, -0x1

    iput v0, p0, Lcom/google/common/collect/CustomConcurrentHashMap$Impl$HashIterator;->nextTableIndex:I

    .line 1700
    invoke-virtual {p0}, Lcom/google/common/collect/CustomConcurrentHashMap$Impl$HashIterator;->nextInTable()Z

    move-result v0

    if-eqz v0, :cond_1

    return-void
.end method

.method advanceTo(Ljava/lang/Object;)Z
    .locals 3
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(TE;)Z"
        }
    .end annotation

    .line 1743
    iget-object v0, p0, Lcom/google/common/collect/CustomConcurrentHashMap$Impl$HashIterator;->this$1:Lcom/google/common/collect/CustomConcurrentHashMap$Impl;

    iget-object v0, v0, Lcom/google/common/collect/CustomConcurrentHashMap$Impl;->strategy:Lcom/google/common/collect/CustomConcurrentHashMap$Strategy;

    .line 1744
    invoke-interface {v0, p1}, Lcom/google/common/collect/CustomConcurrentHashMap$Strategy;->getKey(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v1

    .line 1745
    invoke-interface {v0, p1}, Lcom/google/common/collect/CustomConcurrentHashMap$Strategy;->getValue(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object p1

    if-eqz v1, :cond_0

    if-eqz p1, :cond_0

    .line 1747
    new-instance v0, Lcom/google/common/collect/CustomConcurrentHashMap$Impl$WriteThroughEntry;

    iget-object v2, p0, Lcom/google/common/collect/CustomConcurrentHashMap$Impl$HashIterator;->this$1:Lcom/google/common/collect/CustomConcurrentHashMap$Impl;

    invoke-direct {v0, v2, v1, p1}, Lcom/google/common/collect/CustomConcurrentHashMap$Impl$WriteThroughEntry;-><init>(Lcom/google/common/collect/CustomConcurrentHashMap$Impl;Ljava/lang/Object;Ljava/lang/Object;)V

    iput-object v0, p0, Lcom/google/common/collect/CustomConcurrentHashMap$Impl$HashIterator;->nextExternal:Lcom/google/common/collect/CustomConcurrentHashMap$Impl$WriteThroughEntry;

    const/4 p1, 0x1

    return p1

    :cond_0
    const/4 p1, 0x0

    return p1
.end method

.method public hasMoreElements()Z
    .locals 1

    .line 1681
    invoke-virtual {p0}, Lcom/google/common/collect/CustomConcurrentHashMap$Impl$HashIterator;->hasNext()Z

    move-result v0

    return v0
.end method

.method public hasNext()Z
    .locals 1

    .line 1756
    iget-object v0, p0, Lcom/google/common/collect/CustomConcurrentHashMap$Impl$HashIterator;->nextExternal:Lcom/google/common/collect/CustomConcurrentHashMap$Impl$WriteThroughEntry;

    if-eqz v0, :cond_0

    const/4 v0, 0x1

    return v0

    :cond_0
    const/4 v0, 0x0

    return v0
.end method

.method nextEntry()Lcom/google/common/collect/CustomConcurrentHashMap$Impl$WriteThroughEntry;
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()",
            "Lcom/google/common/collect/CustomConcurrentHashMap$Impl<",
            "TK;TV;TE;>.WriteThroughEntry;"
        }
    .end annotation

    .line 1760
    iget-object v0, p0, Lcom/google/common/collect/CustomConcurrentHashMap$Impl$HashIterator;->nextExternal:Lcom/google/common/collect/CustomConcurrentHashMap$Impl$WriteThroughEntry;

    if-eqz v0, :cond_0

    .line 1763
    iget-object v0, p0, Lcom/google/common/collect/CustomConcurrentHashMap$Impl$HashIterator;->nextExternal:Lcom/google/common/collect/CustomConcurrentHashMap$Impl$WriteThroughEntry;

    iput-object v0, p0, Lcom/google/common/collect/CustomConcurrentHashMap$Impl$HashIterator;->lastReturned:Lcom/google/common/collect/CustomConcurrentHashMap$Impl$WriteThroughEntry;

    .line 1764
    invoke-virtual {p0}, Lcom/google/common/collect/CustomConcurrentHashMap$Impl$HashIterator;->advance()V

    .line 1765
    iget-object v0, p0, Lcom/google/common/collect/CustomConcurrentHashMap$Impl$HashIterator;->lastReturned:Lcom/google/common/collect/CustomConcurrentHashMap$Impl$WriteThroughEntry;

    return-object v0

    .line 1761
    :cond_0
    new-instance v0, Ljava/util/NoSuchElementException;

    invoke-direct {v0}, Ljava/util/NoSuchElementException;-><init>()V

    throw v0
.end method

.method nextInChain()Z
    .locals 2

    .line 1712
    iget-object v0, p0, Lcom/google/common/collect/CustomConcurrentHashMap$Impl$HashIterator;->this$1:Lcom/google/common/collect/CustomConcurrentHashMap$Impl;

    iget-object v0, v0, Lcom/google/common/collect/CustomConcurrentHashMap$Impl;->strategy:Lcom/google/common/collect/CustomConcurrentHashMap$Strategy;

    .line 1713
    iget-object v1, p0, Lcom/google/common/collect/CustomConcurrentHashMap$Impl$HashIterator;->nextEntry:Ljava/lang/Object;

    if-eqz v1, :cond_2

    .line 1714
    :cond_0
    iget-object v1, p0, Lcom/google/common/collect/CustomConcurrentHashMap$Impl$HashIterator;->nextEntry:Ljava/lang/Object;

    invoke-interface {v0, v1}, Lcom/google/common/collect/CustomConcurrentHashMap$Strategy;->getNext(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v1

    iput-object v1, p0, Lcom/google/common/collect/CustomConcurrentHashMap$Impl$HashIterator;->nextEntry:Ljava/lang/Object;

    iget-object v1, p0, Lcom/google/common/collect/CustomConcurrentHashMap$Impl$HashIterator;->nextEntry:Ljava/lang/Object;

    if-nez v1, :cond_1

    goto :goto_0

    .line 1715
    :cond_1
    iget-object v1, p0, Lcom/google/common/collect/CustomConcurrentHashMap$Impl$HashIterator;->nextEntry:Ljava/lang/Object;

    invoke-virtual {p0, v1}, Lcom/google/common/collect/CustomConcurrentHashMap$Impl$HashIterator;->advanceTo(Ljava/lang/Object;)Z

    move-result v1

    if-eqz v1, :cond_0

    const/4 v0, 0x1

    return v0

    :cond_2
    :goto_0
    const/4 v0, 0x0

    return v0
.end method

.method nextInTable()Z
    .locals 3

    .line 1728
    :cond_0
    iget v0, p0, Lcom/google/common/collect/CustomConcurrentHashMap$Impl$HashIterator;->nextTableIndex:I

    if-gez v0, :cond_1

    const/4 v0, 0x0

    return v0

    .line 1729
    :cond_1
    iget-object v0, p0, Lcom/google/common/collect/CustomConcurrentHashMap$Impl$HashIterator;->currentTable:Ljava/util/concurrent/atomic/AtomicReferenceArray;

    iget v1, p0, Lcom/google/common/collect/CustomConcurrentHashMap$Impl$HashIterator;->nextTableIndex:I

    add-int/lit8 v2, v1, -0x1

    iput v2, p0, Lcom/google/common/collect/CustomConcurrentHashMap$Impl$HashIterator;->nextTableIndex:I

    invoke-virtual {v0, v1}, Ljava/util/concurrent/atomic/AtomicReferenceArray;->get(I)Ljava/lang/Object;

    move-result-object v0

    iput-object v0, p0, Lcom/google/common/collect/CustomConcurrentHashMap$Impl$HashIterator;->nextEntry:Ljava/lang/Object;

    if-eqz v0, :cond_0

    .line 1730
    iget-object v0, p0, Lcom/google/common/collect/CustomConcurrentHashMap$Impl$HashIterator;->nextEntry:Ljava/lang/Object;

    invoke-virtual {p0, v0}, Lcom/google/common/collect/CustomConcurrentHashMap$Impl$HashIterator;->advanceTo(Ljava/lang/Object;)Z

    move-result v0

    if-nez v0, :cond_2

    invoke-virtual {p0}, Lcom/google/common/collect/CustomConcurrentHashMap$Impl$HashIterator;->nextInChain()Z

    move-result v0

    if-eqz v0, :cond_0

    :cond_2
    const/4 v0, 0x1

    return v0
.end method

.method public remove()V
    .locals 2

    .line 1769
    iget-object v0, p0, Lcom/google/common/collect/CustomConcurrentHashMap$Impl$HashIterator;->lastReturned:Lcom/google/common/collect/CustomConcurrentHashMap$Impl$WriteThroughEntry;

    if-eqz v0, :cond_0

    .line 1772
    iget-object v0, p0, Lcom/google/common/collect/CustomConcurrentHashMap$Impl$HashIterator;->this$1:Lcom/google/common/collect/CustomConcurrentHashMap$Impl;

    iget-object v1, p0, Lcom/google/common/collect/CustomConcurrentHashMap$Impl$HashIterator;->lastReturned:Lcom/google/common/collect/CustomConcurrentHashMap$Impl$WriteThroughEntry;

    invoke-virtual {v1}, Lcom/google/common/collect/CustomConcurrentHashMap$Impl$WriteThroughEntry;->getKey()Ljava/lang/Object;

    move-result-object v1

    invoke-virtual {v0, v1}, Lcom/google/common/collect/CustomConcurrentHashMap$Impl;->remove(Ljava/lang/Object;)Ljava/lang/Object;

    const/4 v0, 0x0

    .line 1773
    iput-object v0, p0, Lcom/google/common/collect/CustomConcurrentHashMap$Impl$HashIterator;->lastReturned:Lcom/google/common/collect/CustomConcurrentHashMap$Impl$WriteThroughEntry;

    return-void

    .line 1770
    :cond_0
    new-instance v0, Ljava/lang/IllegalStateException;

    invoke-direct {v0}, Ljava/lang/IllegalStateException;-><init>()V

    throw v0
.end method
