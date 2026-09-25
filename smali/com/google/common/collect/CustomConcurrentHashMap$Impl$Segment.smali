.class final Lcom/google/common/collect/CustomConcurrentHashMap$Impl$Segment;
.super Ljava/util/concurrent/locks/ReentrantLock;
.source "CustomConcurrentHashMap.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lcom/google/common/collect/CustomConcurrentHashMap$Impl;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x10
    name = "Segment"
.end annotation


# instance fields
.field volatile count:I

.field modCount:I

.field volatile table:Ljava/util/concurrent/atomic/AtomicReferenceArray;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/concurrent/atomic/AtomicReferenceArray<",
            "TE;>;"
        }
    .end annotation
.end field

.field final synthetic this$1:Lcom/google/common/collect/CustomConcurrentHashMap$Impl;

.field threshold:I


# direct methods
.method constructor <init>(Lcom/google/common/collect/CustomConcurrentHashMap$Impl;I)V
    .locals 0

    .line 785
    iput-object p1, p0, Lcom/google/common/collect/CustomConcurrentHashMap$Impl$Segment;->this$1:Lcom/google/common/collect/CustomConcurrentHashMap$Impl;

    .line 783
    invoke-direct {p0}, Ljava/util/concurrent/locks/ReentrantLock;-><init>()V

    .line 784
    invoke-virtual {p0, p2}, Lcom/google/common/collect/CustomConcurrentHashMap$Impl$Segment;->newEntryArray(I)Ljava/util/concurrent/atomic/AtomicReferenceArray;

    move-result-object p1

    invoke-virtual {p0, p1}, Lcom/google/common/collect/CustomConcurrentHashMap$Impl$Segment;->setTable(Ljava/util/concurrent/atomic/AtomicReferenceArray;)V

    return-void
.end method


# virtual methods
.method clear()V
    .locals 4

    .line 1226
    iget v0, p0, Lcom/google/common/collect/CustomConcurrentHashMap$Impl$Segment;->count:I

    if-eqz v0, :cond_1

    .line 1227
    invoke-virtual {p0}, Lcom/google/common/collect/CustomConcurrentHashMap$Impl$Segment;->lock()V

    .line 1229
    :try_start_0
    iget-object v0, p0, Lcom/google/common/collect/CustomConcurrentHashMap$Impl$Segment;->table:Ljava/util/concurrent/atomic/AtomicReferenceArray;

    const/4 v1, 0x0

    const/4 v2, 0x0

    .line 1230
    :goto_0
    invoke-virtual {v0}, Ljava/util/concurrent/atomic/AtomicReferenceArray;->length()I

    move-result v3

    if-lt v2, v3, :cond_0

    .line 1233
    iget v0, p0, Lcom/google/common/collect/CustomConcurrentHashMap$Impl$Segment;->modCount:I

    add-int/lit8 v0, v0, 0x1

    iput v0, p0, Lcom/google/common/collect/CustomConcurrentHashMap$Impl$Segment;->modCount:I

    .line 1234
    iput v1, p0, Lcom/google/common/collect/CustomConcurrentHashMap$Impl$Segment;->count:I
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    .line 1236
    invoke-virtual {p0}, Lcom/google/common/collect/CustomConcurrentHashMap$Impl$Segment;->unlock()V

    goto :goto_1

    :cond_0
    const/4 v3, 0x0

    .line 1231
    :try_start_1
    invoke-virtual {v0, v2, v3}, Ljava/util/concurrent/atomic/AtomicReferenceArray;->set(ILjava/lang/Object;)V
    :try_end_1
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    add-int/lit8 v2, v2, 0x1

    goto :goto_0

    :catchall_0
    move-exception v0

    .line 1236
    invoke-virtual {p0}, Lcom/google/common/collect/CustomConcurrentHashMap$Impl$Segment;->unlock()V

    .line 1237
    throw v0

    :cond_1
    :goto_1
    return-void
.end method

.method containsKey(Ljava/lang/Object;I)Z
    .locals 4

    .line 842
    iget-object v0, p0, Lcom/google/common/collect/CustomConcurrentHashMap$Impl$Segment;->this$1:Lcom/google/common/collect/CustomConcurrentHashMap$Impl;

    iget-object v0, v0, Lcom/google/common/collect/CustomConcurrentHashMap$Impl;->strategy:Lcom/google/common/collect/CustomConcurrentHashMap$Strategy;

    .line 843
    iget v1, p0, Lcom/google/common/collect/CustomConcurrentHashMap$Impl$Segment;->count:I

    const/4 v2, 0x0

    if-eqz v1, :cond_5

    .line 844
    invoke-virtual {p0, p2}, Lcom/google/common/collect/CustomConcurrentHashMap$Impl$Segment;->getFirst(I)Ljava/lang/Object;

    move-result-object v1

    :goto_0
    if-nez v1, :cond_0

    goto :goto_2

    .line 845
    :cond_0
    invoke-interface {v0, v1}, Lcom/google/common/collect/CustomConcurrentHashMap$Strategy;->getHash(Ljava/lang/Object;)I

    move-result v3

    if-eq v3, p2, :cond_1

    goto :goto_1

    .line 849
    :cond_1
    invoke-interface {v0, v1}, Lcom/google/common/collect/CustomConcurrentHashMap$Strategy;->getKey(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v3

    if-nez v3, :cond_2

    goto :goto_1

    .line 854
    :cond_2
    invoke-interface {v0, v3, p1}, Lcom/google/common/collect/CustomConcurrentHashMap$Strategy;->equalKeys(Ljava/lang/Object;Ljava/lang/Object;)Z

    move-result v3

    if-eqz v3, :cond_4

    .line 856
    invoke-interface {v0, v1}, Lcom/google/common/collect/CustomConcurrentHashMap$Strategy;->getValue(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object p1

    if-eqz p1, :cond_3

    const/4 p1, 0x1

    return p1

    :cond_3
    return v2

    .line 844
    :cond_4
    :goto_1
    invoke-interface {v0, v1}, Lcom/google/common/collect/CustomConcurrentHashMap$Strategy;->getNext(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v1

    goto :goto_0

    :cond_5
    :goto_2
    return v2
.end method

.method containsValue(Ljava/lang/Object;)Z
    .locals 7

    .line 865
    iget-object v0, p0, Lcom/google/common/collect/CustomConcurrentHashMap$Impl$Segment;->this$1:Lcom/google/common/collect/CustomConcurrentHashMap$Impl;

    iget-object v0, v0, Lcom/google/common/collect/CustomConcurrentHashMap$Impl;->strategy:Lcom/google/common/collect/CustomConcurrentHashMap$Strategy;

    .line 866
    iget v1, p0, Lcom/google/common/collect/CustomConcurrentHashMap$Impl$Segment;->count:I

    const/4 v2, 0x0

    if-eqz v1, :cond_4

    .line 867
    iget-object v1, p0, Lcom/google/common/collect/CustomConcurrentHashMap$Impl$Segment;->table:Ljava/util/concurrent/atomic/AtomicReferenceArray;

    .line 868
    invoke-virtual {v1}, Ljava/util/concurrent/atomic/AtomicReferenceArray;->length()I

    move-result v3

    const/4 v4, 0x0

    :goto_0
    if-lt v4, v3, :cond_0

    goto :goto_3

    .line 870
    :cond_0
    invoke-virtual {v1, v4}, Ljava/util/concurrent/atomic/AtomicReferenceArray;->get(I)Ljava/lang/Object;

    move-result-object v5

    :goto_1
    if-nez v5, :cond_1

    add-int/lit8 v4, v4, 0x1

    goto :goto_0

    .line 871
    :cond_1
    invoke-interface {v0, v5}, Lcom/google/common/collect/CustomConcurrentHashMap$Strategy;->getValue(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v6

    if-nez v6, :cond_2

    goto :goto_2

    .line 880
    :cond_2
    invoke-interface {v0, v6, p1}, Lcom/google/common/collect/CustomConcurrentHashMap$Strategy;->equalValues(Ljava/lang/Object;Ljava/lang/Object;)Z

    move-result v6

    if-eqz v6, :cond_3

    const/4 p1, 0x1

    return p1

    .line 870
    :cond_3
    :goto_2
    invoke-interface {v0, v5}, Lcom/google/common/collect/CustomConcurrentHashMap$Strategy;->getNext(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v5

    goto :goto_1

    :cond_4
    :goto_3
    return v2
.end method

.method expand()V
    .locals 11

    .line 993
    iget-object v0, p0, Lcom/google/common/collect/CustomConcurrentHashMap$Impl$Segment;->table:Ljava/util/concurrent/atomic/AtomicReferenceArray;

    .line 994
    invoke-virtual {v0}, Ljava/util/concurrent/atomic/AtomicReferenceArray;->length()I

    move-result v1

    const/high16 v2, 0x40000000    # 2.0f

    if-lt v1, v2, :cond_0

    return-void

    .line 1012
    :cond_0
    iget-object v2, p0, Lcom/google/common/collect/CustomConcurrentHashMap$Impl$Segment;->this$1:Lcom/google/common/collect/CustomConcurrentHashMap$Impl;

    iget-object v2, v2, Lcom/google/common/collect/CustomConcurrentHashMap$Impl;->strategy:Lcom/google/common/collect/CustomConcurrentHashMap$Strategy;

    shl-int/lit8 v3, v1, 0x1

    .line 1013
    invoke-virtual {p0, v3}, Lcom/google/common/collect/CustomConcurrentHashMap$Impl$Segment;->newEntryArray(I)Ljava/util/concurrent/atomic/AtomicReferenceArray;

    move-result-object v3

    .line 1014
    invoke-virtual {v3}, Ljava/util/concurrent/atomic/AtomicReferenceArray;->length()I

    move-result v4

    mul-int/lit8 v4, v4, 0x3

    div-int/lit8 v4, v4, 0x4

    iput v4, p0, Lcom/google/common/collect/CustomConcurrentHashMap$Impl$Segment;->threshold:I

    .line 1015
    invoke-virtual {v3}, Ljava/util/concurrent/atomic/AtomicReferenceArray;->length()I

    move-result v4

    add-int/lit8 v4, v4, -0x1

    const/4 v5, 0x0

    :goto_0
    if-lt v5, v1, :cond_1

    .line 1062
    iput-object v3, p0, Lcom/google/common/collect/CustomConcurrentHashMap$Impl$Segment;->table:Ljava/util/concurrent/atomic/AtomicReferenceArray;

    return-void

    .line 1020
    :cond_1
    invoke-virtual {v0, v5}, Ljava/util/concurrent/atomic/AtomicReferenceArray;->get(I)Ljava/lang/Object;

    move-result-object v6

    if-eqz v6, :cond_7

    .line 1023
    invoke-interface {v2, v6}, Lcom/google/common/collect/CustomConcurrentHashMap$Strategy;->getNext(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v7

    .line 1024
    invoke-interface {v2, v6}, Lcom/google/common/collect/CustomConcurrentHashMap$Strategy;->getHash(Ljava/lang/Object;)I

    move-result v8

    and-int/2addr v8, v4

    if-nez v7, :cond_2

    .line 1028
    invoke-virtual {v3, v8, v6}, Ljava/util/concurrent/atomic/AtomicReferenceArray;->set(ILjava/lang/Object;)V

    goto :goto_3

    :cond_2
    move-object v9, v6

    :goto_1
    if-nez v7, :cond_5

    .line 1046
    invoke-virtual {v3, v8, v9}, Ljava/util/concurrent/atomic/AtomicReferenceArray;->set(ILjava/lang/Object;)V

    :goto_2
    if-ne v6, v9, :cond_3

    goto :goto_3

    .line 1050
    :cond_3
    invoke-interface {v2, v6}, Lcom/google/common/collect/CustomConcurrentHashMap$Strategy;->getKey(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v7

    if-eqz v7, :cond_4

    .line 1052
    invoke-interface {v2, v6}, Lcom/google/common/collect/CustomConcurrentHashMap$Strategy;->getHash(Ljava/lang/Object;)I

    move-result v8

    and-int/2addr v8, v4

    .line 1053
    invoke-virtual {v3, v8}, Ljava/util/concurrent/atomic/AtomicReferenceArray;->get(I)Ljava/lang/Object;

    move-result-object v10

    .line 1054
    invoke-interface {v2, v7, v6, v10}, Lcom/google/common/collect/CustomConcurrentHashMap$Strategy;->copyEntry(Ljava/lang/Object;Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v7

    invoke-virtual {v3, v8, v7}, Ljava/util/concurrent/atomic/AtomicReferenceArray;->set(ILjava/lang/Object;)V

    .line 1049
    :cond_4
    invoke-interface {v2, v6}, Lcom/google/common/collect/CustomConcurrentHashMap$Strategy;->getNext(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v6

    goto :goto_2

    .line 1038
    :cond_5
    invoke-interface {v2, v7}, Lcom/google/common/collect/CustomConcurrentHashMap$Strategy;->getHash(Ljava/lang/Object;)I

    move-result v10

    and-int/2addr v10, v4

    if-eq v10, v8, :cond_6

    move-object v9, v7

    move v8, v10

    .line 1037
    :cond_6
    invoke-interface {v2, v7}, Lcom/google/common/collect/CustomConcurrentHashMap$Strategy;->getNext(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v7

    goto :goto_1

    :cond_7
    :goto_3
    add-int/lit8 v5, v5, 0x1

    goto :goto_0
.end method

.method get(Ljava/lang/Object;I)Ljava/lang/Object;
    .locals 0
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/lang/Object;",
            "I)TV;"
        }
    .end annotation

    .line 833
    invoke-virtual {p0, p1, p2}, Lcom/google/common/collect/CustomConcurrentHashMap$Impl$Segment;->getEntry(Ljava/lang/Object;I)Ljava/lang/Object;

    move-result-object p1

    if-nez p1, :cond_0

    const/4 p1, 0x0

    return-object p1

    .line 838
    :cond_0
    iget-object p2, p0, Lcom/google/common/collect/CustomConcurrentHashMap$Impl$Segment;->this$1:Lcom/google/common/collect/CustomConcurrentHashMap$Impl;

    iget-object p2, p2, Lcom/google/common/collect/CustomConcurrentHashMap$Impl;->strategy:Lcom/google/common/collect/CustomConcurrentHashMap$Strategy;

    invoke-interface {p2, p1}, Lcom/google/common/collect/CustomConcurrentHashMap$Strategy;->getValue(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object p1

    return-object p1
.end method

.method public getEntry(Ljava/lang/Object;I)Ljava/lang/Object;
    .locals 3
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/lang/Object;",
            "I)TE;"
        }
    .end annotation

    .line 811
    iget-object v0, p0, Lcom/google/common/collect/CustomConcurrentHashMap$Impl$Segment;->this$1:Lcom/google/common/collect/CustomConcurrentHashMap$Impl;

    iget-object v0, v0, Lcom/google/common/collect/CustomConcurrentHashMap$Impl;->strategy:Lcom/google/common/collect/CustomConcurrentHashMap$Strategy;

    .line 812
    iget v1, p0, Lcom/google/common/collect/CustomConcurrentHashMap$Impl$Segment;->count:I

    if-eqz v1, :cond_4

    .line 813
    invoke-virtual {p0, p2}, Lcom/google/common/collect/CustomConcurrentHashMap$Impl$Segment;->getFirst(I)Ljava/lang/Object;

    move-result-object v1

    :goto_0
    if-nez v1, :cond_0

    goto :goto_2

    .line 814
    :cond_0
    invoke-interface {v0, v1}, Lcom/google/common/collect/CustomConcurrentHashMap$Strategy;->getHash(Ljava/lang/Object;)I

    move-result v2

    if-eq v2, p2, :cond_1

    goto :goto_1

    .line 818
    :cond_1
    invoke-interface {v0, v1}, Lcom/google/common/collect/CustomConcurrentHashMap$Strategy;->getKey(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v2

    if-nez v2, :cond_2

    goto :goto_1

    .line 823
    :cond_2
    invoke-interface {v0, v2, p1}, Lcom/google/common/collect/CustomConcurrentHashMap$Strategy;->equalKeys(Ljava/lang/Object;Ljava/lang/Object;)Z

    move-result v2

    if-eqz v2, :cond_3

    return-object v1

    .line 813
    :cond_3
    :goto_1
    invoke-interface {v0, v1}, Lcom/google/common/collect/CustomConcurrentHashMap$Strategy;->getNext(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v1

    goto :goto_0

    :cond_4
    :goto_2
    const/4 p1, 0x0

    return-object p1
.end method

.method getFirst(I)Ljava/lang/Object;
    .locals 2
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(I)TE;"
        }
    .end annotation

    .line 804
    iget-object v0, p0, Lcom/google/common/collect/CustomConcurrentHashMap$Impl$Segment;->table:Ljava/util/concurrent/atomic/AtomicReferenceArray;

    .line 805
    invoke-virtual {v0}, Ljava/util/concurrent/atomic/AtomicReferenceArray;->length()I

    move-result v1

    add-int/lit8 v1, v1, -0x1

    and-int/2addr p1, v1

    invoke-virtual {v0, p1}, Ljava/util/concurrent/atomic/AtomicReferenceArray;->get(I)Ljava/lang/Object;

    move-result-object p1

    return-object p1
.end method

.method newEntryArray(I)Ljava/util/concurrent/atomic/AtomicReferenceArray;
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(I)",
            "Ljava/util/concurrent/atomic/AtomicReferenceArray<",
            "TE;>;"
        }
    .end annotation

    .line 788
    new-instance v0, Ljava/util/concurrent/atomic/AtomicReferenceArray;

    invoke-direct {v0, p1}, Ljava/util/concurrent/atomic/AtomicReferenceArray;-><init>(I)V

    return-object v0
.end method

.method put(Ljava/lang/Object;ILjava/lang/Object;Z)Ljava/lang/Object;
    .locals 8
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(TK;ITV;Z)TV;"
        }
    .end annotation

    .line 945
    iget-object v0, p0, Lcom/google/common/collect/CustomConcurrentHashMap$Impl$Segment;->this$1:Lcom/google/common/collect/CustomConcurrentHashMap$Impl;

    iget-object v0, v0, Lcom/google/common/collect/CustomConcurrentHashMap$Impl;->strategy:Lcom/google/common/collect/CustomConcurrentHashMap$Strategy;

    .line 946
    invoke-virtual {p0}, Lcom/google/common/collect/CustomConcurrentHashMap$Impl$Segment;->lock()V

    .line 948
    :try_start_0
    iget v1, p0, Lcom/google/common/collect/CustomConcurrentHashMap$Impl$Segment;->count:I

    add-int/lit8 v2, v1, 0x1

    .line 949
    iget v3, p0, Lcom/google/common/collect/CustomConcurrentHashMap$Impl$Segment;->threshold:I

    if-le v1, v3, :cond_0

    .line 950
    invoke-virtual {p0}, Lcom/google/common/collect/CustomConcurrentHashMap$Impl$Segment;->expand()V

    .line 953
    :cond_0
    iget-object v1, p0, Lcom/google/common/collect/CustomConcurrentHashMap$Impl$Segment;->table:Ljava/util/concurrent/atomic/AtomicReferenceArray;

    .line 954
    invoke-virtual {v1}, Ljava/util/concurrent/atomic/AtomicReferenceArray;->length()I

    move-result v3

    add-int/lit8 v3, v3, -0x1

    and-int/2addr v3, p2

    .line 956
    invoke-virtual {v1, v3}, Ljava/util/concurrent/atomic/AtomicReferenceArray;->get(I)Ljava/lang/Object;

    move-result-object v4

    move-object v5, v4

    :goto_0
    if-nez v5, :cond_1

    .line 978
    iget p4, p0, Lcom/google/common/collect/CustomConcurrentHashMap$Impl$Segment;->modCount:I

    add-int/lit8 p4, p4, 0x1

    iput p4, p0, Lcom/google/common/collect/CustomConcurrentHashMap$Impl$Segment;->modCount:I

    .line 979
    invoke-interface {v0, p1, p2, v4}, Lcom/google/common/collect/CustomConcurrentHashMap$Strategy;->newEntry(Ljava/lang/Object;ILjava/lang/Object;)Ljava/lang/Object;

    move-result-object p1

    .line 980
    invoke-interface {v0, p1, p3}, Lcom/google/common/collect/CustomConcurrentHashMap$Strategy;->setValue(Ljava/lang/Object;Ljava/lang/Object;)V

    .line 981
    invoke-virtual {v1, v3, p1}, Ljava/util/concurrent/atomic/AtomicReferenceArray;->set(ILjava/lang/Object;)V

    .line 982
    iput v2, p0, Lcom/google/common/collect/CustomConcurrentHashMap$Impl$Segment;->count:I
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    .line 985
    invoke-virtual {p0}, Lcom/google/common/collect/CustomConcurrentHashMap$Impl$Segment;->unlock()V

    const/4 p1, 0x0

    return-object p1

    .line 960
    :cond_1
    :try_start_1
    invoke-interface {v0, v5}, Lcom/google/common/collect/CustomConcurrentHashMap$Strategy;->getKey(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v6

    .line 961
    invoke-interface {v0, v5}, Lcom/google/common/collect/CustomConcurrentHashMap$Strategy;->getHash(Ljava/lang/Object;)I

    move-result v7

    if-ne v7, p2, :cond_3

    if-eqz v6, :cond_3

    invoke-interface {v0, p1, v6}, Lcom/google/common/collect/CustomConcurrentHashMap$Strategy;->equalKeys(Ljava/lang/Object;Ljava/lang/Object;)Z

    move-result v6

    if-eqz v6, :cond_3

    .line 967
    invoke-interface {v0, v5}, Lcom/google/common/collect/CustomConcurrentHashMap$Strategy;->getValue(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object p1
    :try_end_1
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    if-eqz p4, :cond_2

    if-eqz p1, :cond_2

    .line 985
    invoke-virtual {p0}, Lcom/google/common/collect/CustomConcurrentHashMap$Impl$Segment;->unlock()V

    return-object p1

    .line 972
    :cond_2
    :try_start_2
    invoke-interface {v0, v5, p3}, Lcom/google/common/collect/CustomConcurrentHashMap$Strategy;->setValue(Ljava/lang/Object;Ljava/lang/Object;)V
    :try_end_2
    .catchall {:try_start_2 .. :try_end_2} :catchall_0

    .line 985
    invoke-virtual {p0}, Lcom/google/common/collect/CustomConcurrentHashMap$Impl$Segment;->unlock()V

    return-object p1

    .line 959
    :cond_3
    :try_start_3
    invoke-interface {v0, v5}, Lcom/google/common/collect/CustomConcurrentHashMap$Strategy;->getNext(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v5
    :try_end_3
    .catchall {:try_start_3 .. :try_end_3} :catchall_0

    goto :goto_0

    :catchall_0
    move-exception p1

    .line 985
    invoke-virtual {p0}, Lcom/google/common/collect/CustomConcurrentHashMap$Impl$Segment;->unlock()V

    .line 986
    throw p1
.end method

.method remove(Ljava/lang/Object;I)Ljava/lang/Object;
    .locals 8
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/lang/Object;",
            "I)TV;"
        }
    .end annotation

    .line 1066
    iget-object v0, p0, Lcom/google/common/collect/CustomConcurrentHashMap$Impl$Segment;->this$1:Lcom/google/common/collect/CustomConcurrentHashMap$Impl;

    iget-object v0, v0, Lcom/google/common/collect/CustomConcurrentHashMap$Impl;->strategy:Lcom/google/common/collect/CustomConcurrentHashMap$Strategy;

    .line 1067
    invoke-virtual {p0}, Lcom/google/common/collect/CustomConcurrentHashMap$Impl$Segment;->lock()V

    .line 1069
    :try_start_0
    iget v1, p0, Lcom/google/common/collect/CustomConcurrentHashMap$Impl$Segment;->count:I

    add-int/lit8 v1, v1, -0x1

    .line 1070
    iget-object v2, p0, Lcom/google/common/collect/CustomConcurrentHashMap$Impl$Segment;->table:Ljava/util/concurrent/atomic/AtomicReferenceArray;

    .line 1071
    invoke-virtual {v2}, Ljava/util/concurrent/atomic/AtomicReferenceArray;->length()I

    move-result v3

    add-int/lit8 v3, v3, -0x1

    and-int/2addr v3, p2

    .line 1072
    invoke-virtual {v2, v3}, Ljava/util/concurrent/atomic/AtomicReferenceArray;->get(I)Ljava/lang/Object;

    move-result-object v4
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    move-object v5, v4

    :goto_0
    if-nez v5, :cond_0

    .line 1099
    invoke-virtual {p0}, Lcom/google/common/collect/CustomConcurrentHashMap$Impl$Segment;->unlock()V

    const/4 p1, 0x0

    return-object p1

    .line 1075
    :cond_0
    :try_start_1
    invoke-interface {v0, v5}, Lcom/google/common/collect/CustomConcurrentHashMap$Strategy;->getKey(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v6

    .line 1076
    invoke-interface {v0, v5}, Lcom/google/common/collect/CustomConcurrentHashMap$Strategy;->getHash(Ljava/lang/Object;)I

    move-result v7

    if-ne v7, p2, :cond_3

    if-eqz v6, :cond_3

    invoke-interface {v0, v6, p1}, Lcom/google/common/collect/CustomConcurrentHashMap$Strategy;->equalKeys(Ljava/lang/Object;Ljava/lang/Object;)Z

    move-result v6

    if-eqz v6, :cond_3

    .line 1077
    iget-object p1, p0, Lcom/google/common/collect/CustomConcurrentHashMap$Impl$Segment;->this$1:Lcom/google/common/collect/CustomConcurrentHashMap$Impl;

    iget-object p1, p1, Lcom/google/common/collect/CustomConcurrentHashMap$Impl;->strategy:Lcom/google/common/collect/CustomConcurrentHashMap$Strategy;

    invoke-interface {p1, v5}, Lcom/google/common/collect/CustomConcurrentHashMap$Strategy;->getValue(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object p1

    .line 1081
    iget p2, p0, Lcom/google/common/collect/CustomConcurrentHashMap$Impl$Segment;->modCount:I

    add-int/lit8 p2, p2, 0x1

    iput p2, p0, Lcom/google/common/collect/CustomConcurrentHashMap$Impl$Segment;->modCount:I

    .line 1082
    invoke-interface {v0, v5}, Lcom/google/common/collect/CustomConcurrentHashMap$Strategy;->getNext(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object p2

    :goto_1
    if-ne v4, v5, :cond_1

    .line 1091
    invoke-virtual {v2, v3, p2}, Ljava/util/concurrent/atomic/AtomicReferenceArray;->set(ILjava/lang/Object;)V

    .line 1092
    iput v1, p0, Lcom/google/common/collect/CustomConcurrentHashMap$Impl$Segment;->count:I
    :try_end_1
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    .line 1099
    invoke-virtual {p0}, Lcom/google/common/collect/CustomConcurrentHashMap$Impl$Segment;->unlock()V

    return-object p1

    .line 1084
    :cond_1
    :try_start_2
    invoke-interface {v0, v4}, Lcom/google/common/collect/CustomConcurrentHashMap$Strategy;->getKey(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v6

    if-eqz v6, :cond_2

    .line 1086
    invoke-interface {v0, v6, v4, p2}, Lcom/google/common/collect/CustomConcurrentHashMap$Strategy;->copyEntry(Ljava/lang/Object;Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object p2

    .line 1083
    :cond_2
    invoke-interface {v0, v4}, Lcom/google/common/collect/CustomConcurrentHashMap$Strategy;->getNext(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v4

    goto :goto_1

    .line 1074
    :cond_3
    invoke-interface {v0, v5}, Lcom/google/common/collect/CustomConcurrentHashMap$Strategy;->getNext(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v5
    :try_end_2
    .catchall {:try_start_2 .. :try_end_2} :catchall_0

    goto :goto_0

    :catchall_0
    move-exception p1

    .line 1099
    invoke-virtual {p0}, Lcom/google/common/collect/CustomConcurrentHashMap$Impl$Segment;->unlock()V

    .line 1100
    throw p1
.end method

.method remove(Ljava/lang/Object;ILjava/lang/Object;)Z
    .locals 10

    .line 1104
    iget-object v0, p0, Lcom/google/common/collect/CustomConcurrentHashMap$Impl$Segment;->this$1:Lcom/google/common/collect/CustomConcurrentHashMap$Impl;

    iget-object v0, v0, Lcom/google/common/collect/CustomConcurrentHashMap$Impl;->strategy:Lcom/google/common/collect/CustomConcurrentHashMap$Strategy;

    .line 1105
    invoke-virtual {p0}, Lcom/google/common/collect/CustomConcurrentHashMap$Impl$Segment;->lock()V

    .line 1107
    :try_start_0
    iget v1, p0, Lcom/google/common/collect/CustomConcurrentHashMap$Impl$Segment;->count:I

    const/4 v2, 0x1

    sub-int/2addr v1, v2

    .line 1108
    iget-object v3, p0, Lcom/google/common/collect/CustomConcurrentHashMap$Impl$Segment;->table:Ljava/util/concurrent/atomic/AtomicReferenceArray;

    .line 1109
    invoke-virtual {v3}, Ljava/util/concurrent/atomic/AtomicReferenceArray;->length()I

    move-result v4

    sub-int/2addr v4, v2

    and-int/2addr v4, p2

    .line 1110
    invoke-virtual {v3, v4}, Ljava/util/concurrent/atomic/AtomicReferenceArray;->get(I)Ljava/lang/Object;

    move-result-object v5
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    move-object v6, v5

    :goto_0
    const/4 v7, 0x0

    if-nez v6, :cond_0

    .line 1143
    invoke-virtual {p0}, Lcom/google/common/collect/CustomConcurrentHashMap$Impl$Segment;->unlock()V

    return v7

    .line 1113
    :cond_0
    :try_start_1
    invoke-interface {v0, v6}, Lcom/google/common/collect/CustomConcurrentHashMap$Strategy;->getKey(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v8

    .line 1114
    invoke-interface {v0, v6}, Lcom/google/common/collect/CustomConcurrentHashMap$Strategy;->getHash(Ljava/lang/Object;)I

    move-result v9

    if-ne v9, p2, :cond_5

    if-eqz v8, :cond_5

    invoke-interface {v0, v8, p1}, Lcom/google/common/collect/CustomConcurrentHashMap$Strategy;->equalKeys(Ljava/lang/Object;Ljava/lang/Object;)Z

    move-result v8

    if-eqz v8, :cond_5

    .line 1115
    iget-object p1, p0, Lcom/google/common/collect/CustomConcurrentHashMap$Impl$Segment;->this$1:Lcom/google/common/collect/CustomConcurrentHashMap$Impl;

    iget-object p1, p1, Lcom/google/common/collect/CustomConcurrentHashMap$Impl;->strategy:Lcom/google/common/collect/CustomConcurrentHashMap$Strategy;

    invoke-interface {p1, v6}, Lcom/google/common/collect/CustomConcurrentHashMap$Strategy;->getValue(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object p1

    if-eq p3, p1, :cond_2

    if-eqz p3, :cond_1

    if-eqz p1, :cond_1

    .line 1117
    invoke-interface {v0, p1, p3}, Lcom/google/common/collect/CustomConcurrentHashMap$Strategy;->equalValues(Ljava/lang/Object;Ljava/lang/Object;)Z

    move-result p1
    :try_end_1
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    if-eqz p1, :cond_1

    goto :goto_1

    .line 1143
    :cond_1
    invoke-virtual {p0}, Lcom/google/common/collect/CustomConcurrentHashMap$Impl$Segment;->unlock()V

    return v7

    .line 1122
    :cond_2
    :goto_1
    :try_start_2
    iget p1, p0, Lcom/google/common/collect/CustomConcurrentHashMap$Impl$Segment;->modCount:I

    add-int/2addr p1, v2

    iput p1, p0, Lcom/google/common/collect/CustomConcurrentHashMap$Impl$Segment;->modCount:I

    .line 1123
    invoke-interface {v0, v6}, Lcom/google/common/collect/CustomConcurrentHashMap$Strategy;->getNext(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object p1

    :goto_2
    if-ne v5, v6, :cond_3

    .line 1132
    invoke-virtual {v3, v4, p1}, Ljava/util/concurrent/atomic/AtomicReferenceArray;->set(ILjava/lang/Object;)V

    .line 1133
    iput v1, p0, Lcom/google/common/collect/CustomConcurrentHashMap$Impl$Segment;->count:I
    :try_end_2
    .catchall {:try_start_2 .. :try_end_2} :catchall_0

    .line 1143
    invoke-virtual {p0}, Lcom/google/common/collect/CustomConcurrentHashMap$Impl$Segment;->unlock()V

    return v2

    .line 1125
    :cond_3
    :try_start_3
    invoke-interface {v0, v5}, Lcom/google/common/collect/CustomConcurrentHashMap$Strategy;->getKey(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object p2

    if-eqz p2, :cond_4

    .line 1127
    invoke-interface {v0, p2, v5, p1}, Lcom/google/common/collect/CustomConcurrentHashMap$Strategy;->copyEntry(Ljava/lang/Object;Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object p1

    .line 1124
    :cond_4
    invoke-interface {v0, v5}, Lcom/google/common/collect/CustomConcurrentHashMap$Strategy;->getNext(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v5

    goto :goto_2

    .line 1112
    :cond_5
    invoke-interface {v0, v6}, Lcom/google/common/collect/CustomConcurrentHashMap$Strategy;->getNext(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v6
    :try_end_3
    .catchall {:try_start_3 .. :try_end_3} :catchall_0

    goto :goto_0

    :catchall_0
    move-exception p1

    .line 1143
    invoke-virtual {p0}, Lcom/google/common/collect/CustomConcurrentHashMap$Impl$Segment;->unlock()V

    .line 1144
    throw p1
.end method

.method public removeEntry(Ljava/lang/Object;I)Z
    .locals 8
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(TE;I)Z"
        }
    .end annotation

    .line 1190
    iget-object v0, p0, Lcom/google/common/collect/CustomConcurrentHashMap$Impl$Segment;->this$1:Lcom/google/common/collect/CustomConcurrentHashMap$Impl;

    iget-object v0, v0, Lcom/google/common/collect/CustomConcurrentHashMap$Impl;->strategy:Lcom/google/common/collect/CustomConcurrentHashMap$Strategy;

    .line 1191
    invoke-virtual {p0}, Lcom/google/common/collect/CustomConcurrentHashMap$Impl$Segment;->lock()V

    .line 1193
    :try_start_0
    iget v1, p0, Lcom/google/common/collect/CustomConcurrentHashMap$Impl$Segment;->count:I

    const/4 v2, 0x1

    sub-int/2addr v1, v2

    .line 1194
    iget-object v3, p0, Lcom/google/common/collect/CustomConcurrentHashMap$Impl$Segment;->table:Ljava/util/concurrent/atomic/AtomicReferenceArray;

    .line 1195
    invoke-virtual {v3}, Ljava/util/concurrent/atomic/AtomicReferenceArray;->length()I

    move-result v4

    sub-int/2addr v4, v2

    and-int/2addr v4, p2

    .line 1196
    invoke-virtual {v3, v4}, Ljava/util/concurrent/atomic/AtomicReferenceArray;->get(I)Ljava/lang/Object;

    move-result-object v5
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    move-object v6, v5

    :goto_0
    if-nez v6, :cond_0

    .line 1221
    invoke-virtual {p0}, Lcom/google/common/collect/CustomConcurrentHashMap$Impl$Segment;->unlock()V

    const/4 p1, 0x0

    return p1

    .line 1199
    :cond_0
    :try_start_1
    invoke-interface {v0, v6}, Lcom/google/common/collect/CustomConcurrentHashMap$Strategy;->getHash(Ljava/lang/Object;)I

    move-result v7

    if-ne v7, p2, :cond_3

    invoke-virtual {p1, v6}, Ljava/lang/Object;->equals(Ljava/lang/Object;)Z

    move-result v7

    if-eqz v7, :cond_3

    .line 1203
    iget p1, p0, Lcom/google/common/collect/CustomConcurrentHashMap$Impl$Segment;->modCount:I

    add-int/2addr p1, v2

    iput p1, p0, Lcom/google/common/collect/CustomConcurrentHashMap$Impl$Segment;->modCount:I

    .line 1204
    invoke-interface {v0, v6}, Lcom/google/common/collect/CustomConcurrentHashMap$Strategy;->getNext(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object p1

    :goto_1
    if-ne v5, v6, :cond_1

    .line 1213
    invoke-virtual {v3, v4, p1}, Ljava/util/concurrent/atomic/AtomicReferenceArray;->set(ILjava/lang/Object;)V

    .line 1214
    iput v1, p0, Lcom/google/common/collect/CustomConcurrentHashMap$Impl$Segment;->count:I
    :try_end_1
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    .line 1221
    invoke-virtual {p0}, Lcom/google/common/collect/CustomConcurrentHashMap$Impl$Segment;->unlock()V

    return v2

    .line 1206
    :cond_1
    :try_start_2
    invoke-interface {v0, v5}, Lcom/google/common/collect/CustomConcurrentHashMap$Strategy;->getKey(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object p2

    if-eqz p2, :cond_2

    .line 1208
    invoke-interface {v0, p2, v5, p1}, Lcom/google/common/collect/CustomConcurrentHashMap$Strategy;->copyEntry(Ljava/lang/Object;Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object p1

    .line 1205
    :cond_2
    invoke-interface {v0, v5}, Lcom/google/common/collect/CustomConcurrentHashMap$Strategy;->getNext(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v5

    goto :goto_1

    .line 1198
    :cond_3
    invoke-interface {v0, v6}, Lcom/google/common/collect/CustomConcurrentHashMap$Strategy;->getNext(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v6
    :try_end_2
    .catchall {:try_start_2 .. :try_end_2} :catchall_0

    goto :goto_0

    :catchall_0
    move-exception p1

    .line 1221
    invoke-virtual {p0}, Lcom/google/common/collect/CustomConcurrentHashMap$Impl$Segment;->unlock()V

    .line 1222
    throw p1
.end method

.method public removeEntry(Ljava/lang/Object;ILjava/lang/Object;)Z
    .locals 9
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(TE;ITV;)Z"
        }
    .end annotation

    .line 1148
    iget-object v0, p0, Lcom/google/common/collect/CustomConcurrentHashMap$Impl$Segment;->this$1:Lcom/google/common/collect/CustomConcurrentHashMap$Impl;

    iget-object v0, v0, Lcom/google/common/collect/CustomConcurrentHashMap$Impl;->strategy:Lcom/google/common/collect/CustomConcurrentHashMap$Strategy;

    .line 1149
    invoke-virtual {p0}, Lcom/google/common/collect/CustomConcurrentHashMap$Impl$Segment;->lock()V

    .line 1151
    :try_start_0
    iget v1, p0, Lcom/google/common/collect/CustomConcurrentHashMap$Impl$Segment;->count:I

    const/4 v2, 0x1

    sub-int/2addr v1, v2

    .line 1152
    iget-object v3, p0, Lcom/google/common/collect/CustomConcurrentHashMap$Impl$Segment;->table:Ljava/util/concurrent/atomic/AtomicReferenceArray;

    .line 1153
    invoke-virtual {v3}, Ljava/util/concurrent/atomic/AtomicReferenceArray;->length()I

    move-result v4

    sub-int/2addr v4, v2

    and-int/2addr v4, p2

    .line 1154
    invoke-virtual {v3, v4}, Ljava/util/concurrent/atomic/AtomicReferenceArray;->get(I)Ljava/lang/Object;

    move-result-object v5
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    move-object v6, v5

    :goto_0
    const/4 v7, 0x0

    if-nez v6, :cond_0

    .line 1185
    invoke-virtual {p0}, Lcom/google/common/collect/CustomConcurrentHashMap$Impl$Segment;->unlock()V

    return v7

    .line 1157
    :cond_0
    :try_start_1
    invoke-interface {v0, v6}, Lcom/google/common/collect/CustomConcurrentHashMap$Strategy;->getHash(Ljava/lang/Object;)I

    move-result v8

    if-ne v8, p2, :cond_5

    invoke-virtual {p1, v6}, Ljava/lang/Object;->equals(Ljava/lang/Object;)Z

    move-result v8

    if-eqz v8, :cond_5

    .line 1158
    invoke-interface {v0, v6}, Lcom/google/common/collect/CustomConcurrentHashMap$Strategy;->getValue(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object p1

    if-eq p1, p3, :cond_2

    if-eqz p3, :cond_1

    .line 1160
    invoke-interface {v0, p1, p3}, Lcom/google/common/collect/CustomConcurrentHashMap$Strategy;->equalValues(Ljava/lang/Object;Ljava/lang/Object;)Z

    move-result p1
    :try_end_1
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    if-eqz p1, :cond_1

    goto :goto_1

    .line 1185
    :cond_1
    invoke-virtual {p0}, Lcom/google/common/collect/CustomConcurrentHashMap$Impl$Segment;->unlock()V

    return v7

    .line 1164
    :cond_2
    :goto_1
    :try_start_2
    iget p1, p0, Lcom/google/common/collect/CustomConcurrentHashMap$Impl$Segment;->modCount:I

    add-int/2addr p1, v2

    iput p1, p0, Lcom/google/common/collect/CustomConcurrentHashMap$Impl$Segment;->modCount:I

    .line 1165
    invoke-interface {v0, v6}, Lcom/google/common/collect/CustomConcurrentHashMap$Strategy;->getNext(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object p1

    :goto_2
    if-ne v5, v6, :cond_3

    .line 1174
    invoke-virtual {v3, v4, p1}, Ljava/util/concurrent/atomic/AtomicReferenceArray;->set(ILjava/lang/Object;)V

    .line 1175
    iput v1, p0, Lcom/google/common/collect/CustomConcurrentHashMap$Impl$Segment;->count:I
    :try_end_2
    .catchall {:try_start_2 .. :try_end_2} :catchall_0

    .line 1185
    invoke-virtual {p0}, Lcom/google/common/collect/CustomConcurrentHashMap$Impl$Segment;->unlock()V

    return v2

    .line 1167
    :cond_3
    :try_start_3
    invoke-interface {v0, v5}, Lcom/google/common/collect/CustomConcurrentHashMap$Strategy;->getKey(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object p2

    if-eqz p2, :cond_4

    .line 1169
    invoke-interface {v0, p2, v5, p1}, Lcom/google/common/collect/CustomConcurrentHashMap$Strategy;->copyEntry(Ljava/lang/Object;Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object p1

    .line 1166
    :cond_4
    invoke-interface {v0, v5}, Lcom/google/common/collect/CustomConcurrentHashMap$Strategy;->getNext(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v5

    goto :goto_2

    .line 1156
    :cond_5
    invoke-interface {v0, v6}, Lcom/google/common/collect/CustomConcurrentHashMap$Strategy;->getNext(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v6
    :try_end_3
    .catchall {:try_start_3 .. :try_end_3} :catchall_0

    goto :goto_0

    :catchall_0
    move-exception p1

    .line 1185
    invoke-virtual {p0}, Lcom/google/common/collect/CustomConcurrentHashMap$Impl$Segment;->unlock()V

    .line 1186
    throw p1
.end method

.method replace(Ljava/lang/Object;ILjava/lang/Object;)Ljava/lang/Object;
    .locals 5
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(TK;ITV;)TV;"
        }
    .end annotation

    .line 919
    iget-object v0, p0, Lcom/google/common/collect/CustomConcurrentHashMap$Impl$Segment;->this$1:Lcom/google/common/collect/CustomConcurrentHashMap$Impl;

    iget-object v0, v0, Lcom/google/common/collect/CustomConcurrentHashMap$Impl;->strategy:Lcom/google/common/collect/CustomConcurrentHashMap$Strategy;

    .line 920
    invoke-virtual {p0}, Lcom/google/common/collect/CustomConcurrentHashMap$Impl$Segment;->lock()V

    .line 922
    :try_start_0
    invoke-virtual {p0, p2}, Lcom/google/common/collect/CustomConcurrentHashMap$Impl$Segment;->getFirst(I)Ljava/lang/Object;

    move-result-object v1
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    :goto_0
    const/4 v2, 0x0

    if-nez v1, :cond_0

    .line 940
    invoke-virtual {p0}, Lcom/google/common/collect/CustomConcurrentHashMap$Impl$Segment;->unlock()V

    return-object v2

    .line 923
    :cond_0
    :try_start_1
    invoke-interface {v0, v1}, Lcom/google/common/collect/CustomConcurrentHashMap$Strategy;->getKey(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v3

    .line 924
    invoke-interface {v0, v1}, Lcom/google/common/collect/CustomConcurrentHashMap$Strategy;->getHash(Ljava/lang/Object;)I

    move-result v4

    if-ne v4, p2, :cond_2

    if-eqz v3, :cond_2

    invoke-interface {v0, p1, v3}, Lcom/google/common/collect/CustomConcurrentHashMap$Strategy;->equalKeys(Ljava/lang/Object;Ljava/lang/Object;)Z

    move-result v3

    if-eqz v3, :cond_2

    .line 928
    invoke-interface {v0, v1}, Lcom/google/common/collect/CustomConcurrentHashMap$Strategy;->getValue(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object p1
    :try_end_1
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    if-nez p1, :cond_1

    .line 940
    invoke-virtual {p0}, Lcom/google/common/collect/CustomConcurrentHashMap$Impl$Segment;->unlock()V

    return-object v2

    .line 933
    :cond_1
    :try_start_2
    invoke-interface {v0, v1, p3}, Lcom/google/common/collect/CustomConcurrentHashMap$Strategy;->setValue(Ljava/lang/Object;Ljava/lang/Object;)V
    :try_end_2
    .catchall {:try_start_2 .. :try_end_2} :catchall_0

    .line 940
    invoke-virtual {p0}, Lcom/google/common/collect/CustomConcurrentHashMap$Impl$Segment;->unlock()V

    return-object p1

    .line 922
    :cond_2
    :try_start_3
    invoke-interface {v0, v1}, Lcom/google/common/collect/CustomConcurrentHashMap$Strategy;->getNext(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v1
    :try_end_3
    .catchall {:try_start_3 .. :try_end_3} :catchall_0

    goto :goto_0

    :catchall_0
    move-exception p1

    .line 940
    invoke-virtual {p0}, Lcom/google/common/collect/CustomConcurrentHashMap$Impl$Segment;->unlock()V

    .line 941
    throw p1
.end method

.method replace(Ljava/lang/Object;ILjava/lang/Object;Ljava/lang/Object;)Z
    .locals 5
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(TK;ITV;TV;)Z"
        }
    .end annotation

    .line 891
    iget-object v0, p0, Lcom/google/common/collect/CustomConcurrentHashMap$Impl$Segment;->this$1:Lcom/google/common/collect/CustomConcurrentHashMap$Impl;

    iget-object v0, v0, Lcom/google/common/collect/CustomConcurrentHashMap$Impl;->strategy:Lcom/google/common/collect/CustomConcurrentHashMap$Strategy;

    .line 892
    invoke-virtual {p0}, Lcom/google/common/collect/CustomConcurrentHashMap$Impl$Segment;->lock()V

    .line 894
    :try_start_0
    invoke-virtual {p0, p2}, Lcom/google/common/collect/CustomConcurrentHashMap$Impl$Segment;->getFirst(I)Ljava/lang/Object;

    move-result-object v1
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    :goto_0
    const/4 v2, 0x0

    if-nez v1, :cond_0

    .line 914
    invoke-virtual {p0}, Lcom/google/common/collect/CustomConcurrentHashMap$Impl$Segment;->unlock()V

    return v2

    .line 895
    :cond_0
    :try_start_1
    invoke-interface {v0, v1}, Lcom/google/common/collect/CustomConcurrentHashMap$Strategy;->getKey(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v3

    .line 896
    invoke-interface {v0, v1}, Lcom/google/common/collect/CustomConcurrentHashMap$Strategy;->getHash(Ljava/lang/Object;)I

    move-result v4

    if-ne v4, p2, :cond_2

    if-eqz v3, :cond_2

    invoke-interface {v0, p1, v3}, Lcom/google/common/collect/CustomConcurrentHashMap$Strategy;->equalKeys(Ljava/lang/Object;Ljava/lang/Object;)Z

    move-result v3

    if-eqz v3, :cond_2

    .line 900
    invoke-interface {v0, v1}, Lcom/google/common/collect/CustomConcurrentHashMap$Strategy;->getValue(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v3
    :try_end_1
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    if-nez v3, :cond_1

    .line 914
    invoke-virtual {p0}, Lcom/google/common/collect/CustomConcurrentHashMap$Impl$Segment;->unlock()V

    return v2

    .line 905
    :cond_1
    :try_start_2
    invoke-interface {v0, v3, p3}, Lcom/google/common/collect/CustomConcurrentHashMap$Strategy;->equalValues(Ljava/lang/Object;Ljava/lang/Object;)Z

    move-result v2

    if-eqz v2, :cond_2

    .line 906
    invoke-interface {v0, v1, p4}, Lcom/google/common/collect/CustomConcurrentHashMap$Strategy;->setValue(Ljava/lang/Object;Ljava/lang/Object;)V
    :try_end_2
    .catchall {:try_start_2 .. :try_end_2} :catchall_0

    .line 914
    invoke-virtual {p0}, Lcom/google/common/collect/CustomConcurrentHashMap$Impl$Segment;->unlock()V

    const/4 p1, 0x1

    return p1

    .line 894
    :cond_2
    :try_start_3
    invoke-interface {v0, v1}, Lcom/google/common/collect/CustomConcurrentHashMap$Strategy;->getNext(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v1
    :try_end_3
    .catchall {:try_start_3 .. :try_end_3} :catchall_0

    goto :goto_0

    :catchall_0
    move-exception p1

    .line 914
    invoke-virtual {p0}, Lcom/google/common/collect/CustomConcurrentHashMap$Impl$Segment;->unlock()V

    .line 915
    throw p1
.end method

.method setTable(Ljava/util/concurrent/atomic/AtomicReferenceArray;)V
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/concurrent/atomic/AtomicReferenceArray<",
            "TE;>;)V"
        }
    .end annotation

    .line 796
    invoke-virtual {p1}, Ljava/util/concurrent/atomic/AtomicReferenceArray;->length()I

    move-result v0

    mul-int/lit8 v0, v0, 0x3

    div-int/lit8 v0, v0, 0x4

    iput v0, p0, Lcom/google/common/collect/CustomConcurrentHashMap$Impl$Segment;->threshold:I

    .line 797
    iput-object p1, p0, Lcom/google/common/collect/CustomConcurrentHashMap$Impl$Segment;->table:Ljava/util/concurrent/atomic/AtomicReferenceArray;

    return-void
.end method
