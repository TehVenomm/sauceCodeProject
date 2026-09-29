.class final Lcom/google/common/collect/CustomConcurrentHashMap$Builder;
.super Ljava/lang/Object;
.source "CustomConcurrentHashMap.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lcom/google/common/collect/CustomConcurrentHashMap;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x18
    name = "Builder"
.end annotation


# static fields
.field private static final DEFAULT_CONCURRENCY_LEVEL:I = 0x10

.field private static final DEFAULT_INITIAL_CAPACITY:I = 0x10

.field private static final UNSET_CONCURRENCY_LEVEL:I = -0x1

.field private static final UNSET_INITIAL_CAPACITY:I = -0x1


# instance fields
.field concurrencyLevel:I

.field initialCapacity:I


# direct methods
.method constructor <init>()V
    .locals 1

    .line 134
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    const/4 v0, -0x1

    .line 141
    iput v0, p0, Lcom/google/common/collect/CustomConcurrentHashMap$Builder;->initialCapacity:I

    .line 142
    iput v0, p0, Lcom/google/common/collect/CustomConcurrentHashMap$Builder;->concurrencyLevel:I

    return-void
.end method


# virtual methods
.method public buildComputingMap(Lcom/google/common/collect/CustomConcurrentHashMap$ComputingStrategy;Lcom/google/common/base/Function;)Ljava/util/concurrent/ConcurrentMap;
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "<K:",
            "Ljava/lang/Object;",
            "V:",
            "Ljava/lang/Object;",
            "E:",
            "Ljava/lang/Object;",
            ">(",
            "Lcom/google/common/collect/CustomConcurrentHashMap$ComputingStrategy<",
            "TK;TV;TE;>;",
            "Lcom/google/common/base/Function<",
            "-TK;+TV;>;)",
            "Ljava/util/concurrent/ConcurrentMap<",
            "TK;TV;>;"
        }
    .end annotation

    if-eqz p1, :cond_1

    if-eqz p2, :cond_0

    .line 265
    new-instance v0, Lcom/google/common/collect/CustomConcurrentHashMap$ComputingImpl;

    invoke-direct {v0, p1, p0, p2}, Lcom/google/common/collect/CustomConcurrentHashMap$ComputingImpl;-><init>(Lcom/google/common/collect/CustomConcurrentHashMap$ComputingStrategy;Lcom/google/common/collect/CustomConcurrentHashMap$Builder;Lcom/google/common/base/Function;)V

    return-object v0

    .line 262
    :cond_0
    new-instance p1, Ljava/lang/NullPointerException;

    const-string p2, "computer"

    invoke-direct {p1, p2}, Ljava/lang/NullPointerException;-><init>(Ljava/lang/String;)V

    throw p1

    .line 259
    :cond_1
    new-instance p1, Ljava/lang/NullPointerException;

    const-string p2, "strategy"

    invoke-direct {p1, p2}, Ljava/lang/NullPointerException;-><init>(Ljava/lang/String;)V

    throw p1
.end method

.method public buildMap(Lcom/google/common/collect/CustomConcurrentHashMap$Strategy;)Ljava/util/concurrent/ConcurrentMap;
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "<K:",
            "Ljava/lang/Object;",
            "V:",
            "Ljava/lang/Object;",
            "E:",
            "Ljava/lang/Object;",
            ">(",
            "Lcom/google/common/collect/CustomConcurrentHashMap$Strategy<",
            "TK;TV;TE;>;)",
            "Ljava/util/concurrent/ConcurrentMap<",
            "TK;TV;>;"
        }
    .end annotation

    if-eqz p1, :cond_0

    .line 212
    new-instance v0, Lcom/google/common/collect/CustomConcurrentHashMap$Impl;

    invoke-direct {v0, p1, p0}, Lcom/google/common/collect/CustomConcurrentHashMap$Impl;-><init>(Lcom/google/common/collect/CustomConcurrentHashMap$Strategy;Lcom/google/common/collect/CustomConcurrentHashMap$Builder;)V

    return-object v0

    .line 210
    :cond_0
    new-instance p1, Ljava/lang/NullPointerException;

    const-string v0, "strategy"

    invoke-direct {p1, v0}, Ljava/lang/NullPointerException;-><init>(Ljava/lang/String;)V

    throw p1
.end method

.method public concurrencyLevel(I)Lcom/google/common/collect/CustomConcurrentHashMap$Builder;
    .locals 2

    .line 183
    iget v0, p0, Lcom/google/common/collect/CustomConcurrentHashMap$Builder;->concurrencyLevel:I

    const/4 v1, -0x1

    if-ne v0, v1, :cond_1

    if-lez p1, :cond_0

    .line 190
    iput p1, p0, Lcom/google/common/collect/CustomConcurrentHashMap$Builder;->concurrencyLevel:I

    return-object p0

    .line 188
    :cond_0
    new-instance p1, Ljava/lang/IllegalArgumentException;

    invoke-direct {p1}, Ljava/lang/IllegalArgumentException;-><init>()V

    throw p1

    .line 184
    :cond_1
    new-instance p1, Ljava/lang/IllegalStateException;

    new-instance v0, Ljava/lang/StringBuilder;

    const-string v1, "concurrency level was already set to "

    invoke-direct {v0, v1}, Ljava/lang/StringBuilder;-><init>(Ljava/lang/String;)V

    .line 185
    iget v1, p0, Lcom/google/common/collect/CustomConcurrentHashMap$Builder;->concurrencyLevel:I

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v0

    .line 184
    invoke-direct {p1, v0}, Ljava/lang/IllegalStateException;-><init>(Ljava/lang/String;)V

    throw p1
.end method

.method getConcurrencyLevel()I
    .locals 2

    .line 274
    iget v0, p0, Lcom/google/common/collect/CustomConcurrentHashMap$Builder;->concurrencyLevel:I

    const/4 v1, -0x1

    if-ne v0, v1, :cond_0

    const/16 v0, 0x10

    goto :goto_0

    .line 275
    :cond_0
    iget v0, p0, Lcom/google/common/collect/CustomConcurrentHashMap$Builder;->concurrencyLevel:I

    :goto_0
    return v0
.end method

.method getInitialCapacity()I
    .locals 2

    .line 269
    iget v0, p0, Lcom/google/common/collect/CustomConcurrentHashMap$Builder;->initialCapacity:I

    const/4 v1, -0x1

    if-ne v0, v1, :cond_0

    const/16 v0, 0x10

    goto :goto_0

    .line 270
    :cond_0
    iget v0, p0, Lcom/google/common/collect/CustomConcurrentHashMap$Builder;->initialCapacity:I

    :goto_0
    return v0
.end method

.method public initialCapacity(I)Lcom/google/common/collect/CustomConcurrentHashMap$Builder;
    .locals 2

    .line 154
    iget v0, p0, Lcom/google/common/collect/CustomConcurrentHashMap$Builder;->initialCapacity:I

    const/4 v1, -0x1

    if-ne v0, v1, :cond_1

    if-ltz p1, :cond_0

    .line 161
    iput p1, p0, Lcom/google/common/collect/CustomConcurrentHashMap$Builder;->initialCapacity:I

    return-object p0

    .line 159
    :cond_0
    new-instance p1, Ljava/lang/IllegalArgumentException;

    invoke-direct {p1}, Ljava/lang/IllegalArgumentException;-><init>()V

    throw p1

    .line 155
    :cond_1
    new-instance p1, Ljava/lang/IllegalStateException;

    new-instance v0, Ljava/lang/StringBuilder;

    const-string v1, "initial capacity was already set to "

    invoke-direct {v0, v1}, Ljava/lang/StringBuilder;-><init>(Ljava/lang/String;)V

    .line 156
    iget v1, p0, Lcom/google/common/collect/CustomConcurrentHashMap$Builder;->initialCapacity:I

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v0

    .line 155
    invoke-direct {p1, v0}, Ljava/lang/IllegalStateException;-><init>(Ljava/lang/String;)V

    throw p1
.end method
