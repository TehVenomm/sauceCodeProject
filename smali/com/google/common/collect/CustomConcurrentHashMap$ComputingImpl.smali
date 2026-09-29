.class Lcom/google/common/collect/CustomConcurrentHashMap$ComputingImpl;
.super Lcom/google/common/collect/CustomConcurrentHashMap$Impl;
.source "CustomConcurrentHashMap.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lcom/google/common/collect/CustomConcurrentHashMap;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x8
    name = "ComputingImpl"
.end annotation

.annotation system Ldalvik/annotation/Signature;
    value = {
        "<K:",
        "Ljava/lang/Object;",
        "V:",
        "Ljava/lang/Object;",
        "E:",
        "Ljava/lang/Object;",
        ">",
        "Lcom/google/common/collect/CustomConcurrentHashMap$Impl<",
        "TK;TV;TE;>;"
    }
.end annotation


# static fields
.field static final serialVersionUID:J


# instance fields
.field final computer:Lcom/google/common/base/Function;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Lcom/google/common/base/Function<",
            "-TK;+TV;>;"
        }
    .end annotation
.end field

.field final computingStrategy:Lcom/google/common/collect/CustomConcurrentHashMap$ComputingStrategy;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Lcom/google/common/collect/CustomConcurrentHashMap$ComputingStrategy<",
            "TK;TV;TE;>;"
        }
    .end annotation
.end field


# direct methods
.method constructor <init>(Lcom/google/common/collect/CustomConcurrentHashMap$ComputingStrategy;Lcom/google/common/collect/CustomConcurrentHashMap$Builder;Lcom/google/common/base/Function;)V
    .locals 0
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Lcom/google/common/collect/CustomConcurrentHashMap$ComputingStrategy<",
            "TK;TV;TE;>;",
            "Lcom/google/common/collect/CustomConcurrentHashMap$Builder;",
            "Lcom/google/common/base/Function<",
            "-TK;+TV;>;)V"
        }
    .end annotation

    .line 2056
    invoke-direct {p0, p1, p2}, Lcom/google/common/collect/CustomConcurrentHashMap$Impl;-><init>(Lcom/google/common/collect/CustomConcurrentHashMap$Strategy;Lcom/google/common/collect/CustomConcurrentHashMap$Builder;)V

    .line 2057
    iput-object p1, p0, Lcom/google/common/collect/CustomConcurrentHashMap$ComputingImpl;->computingStrategy:Lcom/google/common/collect/CustomConcurrentHashMap$ComputingStrategy;

    .line 2058
    iput-object p3, p0, Lcom/google/common/collect/CustomConcurrentHashMap$ComputingImpl;->computer:Lcom/google/common/base/Function;

    return-void
.end method


# virtual methods
.method public get(Ljava/lang/Object;)Ljava/lang/Object;
    .locals 9
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/lang/Object;",
            ")TV;"
        }
    .end annotation

    if-eqz p1, :cond_9

    .line 2078
    invoke-virtual {p0, p1}, Lcom/google/common/collect/CustomConcurrentHashMap$ComputingImpl;->hash(Ljava/lang/Object;)I

    move-result v0

    .line 2079
    invoke-virtual {p0, v0}, Lcom/google/common/collect/CustomConcurrentHashMap$ComputingImpl;->segmentFor(I)Lcom/google/common/collect/CustomConcurrentHashMap$Impl$Segment;

    move-result-object v1

    .line 2081
    :cond_0
    :goto_0
    invoke-virtual {v1, p1, v0}, Lcom/google/common/collect/CustomConcurrentHashMap$Impl$Segment;->getEntry(Ljava/lang/Object;I)Ljava/lang/Object;

    move-result-object v2

    const/4 v3, 0x0

    const/4 v4, 0x1

    if-nez v2, :cond_5

    .line 2084
    invoke-virtual {v1}, Lcom/google/common/collect/CustomConcurrentHashMap$Impl$Segment;->lock()V

    .line 2088
    :try_start_0
    invoke-virtual {v1, p1, v0}, Lcom/google/common/collect/CustomConcurrentHashMap$Impl$Segment;->getEntry(Ljava/lang/Object;I)Ljava/lang/Object;

    move-result-object v2

    if-nez v2, :cond_2

    .line 2092
    iget v2, v1, Lcom/google/common/collect/CustomConcurrentHashMap$Impl$Segment;->count:I

    add-int/lit8 v5, v2, 0x1

    .line 2093
    iget v6, v1, Lcom/google/common/collect/CustomConcurrentHashMap$Impl$Segment;->threshold:I

    if-le v2, v6, :cond_1

    .line 2095
    invoke-virtual {v1}, Lcom/google/common/collect/CustomConcurrentHashMap$Impl$Segment;->expand()V

    .line 2097
    :cond_1
    iget-object v2, v1, Lcom/google/common/collect/CustomConcurrentHashMap$Impl$Segment;->table:Ljava/util/concurrent/atomic/AtomicReferenceArray;

    .line 2098
    invoke-virtual {v2}, Ljava/util/concurrent/atomic/AtomicReferenceArray;->length()I

    move-result v6

    sub-int/2addr v6, v4

    and-int/2addr v6, v0

    .line 2099
    invoke-virtual {v2, v6}, Ljava/util/concurrent/atomic/AtomicReferenceArray;->get(I)Ljava/lang/Object;

    move-result-object v7

    .line 2100
    iget v8, v1, Lcom/google/common/collect/CustomConcurrentHashMap$Impl$Segment;->modCount:I

    add-int/2addr v8, v4

    iput v8, v1, Lcom/google/common/collect/CustomConcurrentHashMap$Impl$Segment;->modCount:I

    .line 2101
    iget-object v8, p0, Lcom/google/common/collect/CustomConcurrentHashMap$ComputingImpl;->computingStrategy:Lcom/google/common/collect/CustomConcurrentHashMap$ComputingStrategy;

    invoke-interface {v8, p1, v0, v7}, Lcom/google/common/collect/CustomConcurrentHashMap$ComputingStrategy;->newEntry(Ljava/lang/Object;ILjava/lang/Object;)Ljava/lang/Object;

    move-result-object v7

    .line 2102
    invoke-virtual {v2, v6, v7}, Ljava/util/concurrent/atomic/AtomicReferenceArray;->set(ILjava/lang/Object;)V

    .line 2103
    iput v5, v1, Lcom/google/common/collect/CustomConcurrentHashMap$Impl$Segment;->count:I
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_1

    const/4 v2, 0x1

    goto :goto_1

    :cond_2
    move-object v7, v2

    const/4 v2, 0x0

    .line 2106
    :goto_1
    invoke-virtual {v1}, Lcom/google/common/collect/CustomConcurrentHashMap$Impl$Segment;->unlock()V

    if-eqz v2, :cond_4

    .line 2113
    :try_start_1
    iget-object v2, p0, Lcom/google/common/collect/CustomConcurrentHashMap$ComputingImpl;->computingStrategy:Lcom/google/common/collect/CustomConcurrentHashMap$ComputingStrategy;

    iget-object v3, p0, Lcom/google/common/collect/CustomConcurrentHashMap$ComputingImpl;->computer:Lcom/google/common/base/Function;

    invoke-interface {v2, p1, v7, v3}, Lcom/google/common/collect/CustomConcurrentHashMap$ComputingStrategy;->compute(Ljava/lang/Object;Ljava/lang/Object;Lcom/google/common/base/Function;)Ljava/lang/Object;

    move-result-object p1

    if-eqz p1, :cond_3

    return-object p1

    .line 2115
    :cond_3
    new-instance p1, Ljava/lang/NullPointerException;

    const-string v2, "compute() returned null unexpectedly"

    invoke-direct {p1, v2}, Ljava/lang/NullPointerException;-><init>(Ljava/lang/String;)V

    throw p1
    :try_end_1
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    :catchall_0
    move-exception p1

    .line 2122
    invoke-virtual {v1, v7, v0}, Lcom/google/common/collect/CustomConcurrentHashMap$Impl$Segment;->removeEntry(Ljava/lang/Object;I)Z

    .line 2124
    throw p1

    :cond_4
    move-object v2, v7

    goto :goto_2

    :catchall_1
    move-exception p1

    .line 2106
    invoke-virtual {v1}, Lcom/google/common/collect/CustomConcurrentHashMap$Impl$Segment;->unlock()V

    .line 2107
    throw p1

    .line 2133
    :cond_5
    :goto_2
    :try_start_2
    iget-object v5, p0, Lcom/google/common/collect/CustomConcurrentHashMap$ComputingImpl;->computingStrategy:Lcom/google/common/collect/CustomConcurrentHashMap$ComputingStrategy;

    invoke-interface {v5, v2}, Lcom/google/common/collect/CustomConcurrentHashMap$ComputingStrategy;->waitForValue(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v5

    if-nez v5, :cond_6

    .line 2136
    invoke-virtual {v1, v2, v0}, Lcom/google/common/collect/CustomConcurrentHashMap$Impl$Segment;->removeEntry(Ljava/lang/Object;I)Z
    :try_end_2
    .catch Ljava/lang/InterruptedException; {:try_start_2 .. :try_end_2} :catch_0
    .catchall {:try_start_2 .. :try_end_2} :catchall_2

    if-eqz v3, :cond_0

    .line 2146
    invoke-static {}, Ljava/lang/Thread;->currentThread()Ljava/lang/Thread;

    move-result-object v2

    invoke-virtual {v2}, Ljava/lang/Thread;->interrupt()V

    goto :goto_0

    :cond_6
    if-eqz v3, :cond_7

    invoke-static {}, Ljava/lang/Thread;->currentThread()Ljava/lang/Thread;

    move-result-object p1

    invoke-virtual {p1}, Ljava/lang/Thread;->interrupt()V

    :cond_7
    return-object v5

    :catchall_2
    move-exception p1

    if-eqz v3, :cond_8

    invoke-static {}, Ljava/lang/Thread;->currentThread()Ljava/lang/Thread;

    move-result-object v0

    invoke-virtual {v0}, Ljava/lang/Thread;->interrupt()V

    .line 2148
    :cond_8
    throw p1

    :catch_0
    const/4 v3, 0x1

    goto :goto_2

    .line 2075
    :cond_9
    new-instance p1, Ljava/lang/NullPointerException;

    const-string v0, "key"

    invoke-direct {p1, v0}, Ljava/lang/NullPointerException;-><init>(Ljava/lang/String;)V

    throw p1
.end method
