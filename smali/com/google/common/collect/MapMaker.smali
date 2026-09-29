.class public final Lcom/google/common/collect/MapMaker;
.super Ljava/lang/Object;
.source "MapMaker.java"


# annotations
.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Lcom/google/common/collect/MapMaker$ComputationExceptionReference;,
        Lcom/google/common/collect/MapMaker$LinkedSoftEntry;,
        Lcom/google/common/collect/MapMaker$LinkedStrongEntry;,
        Lcom/google/common/collect/MapMaker$LinkedWeakEntry;,
        Lcom/google/common/collect/MapMaker$NullOutputExceptionReference;,
        Lcom/google/common/collect/MapMaker$QueueHolder;,
        Lcom/google/common/collect/MapMaker$ReferenceEntry;,
        Lcom/google/common/collect/MapMaker$SoftEntry;,
        Lcom/google/common/collect/MapMaker$SoftValueReference;,
        Lcom/google/common/collect/MapMaker$StrategyImpl;,
        Lcom/google/common/collect/MapMaker$Strength;,
        Lcom/google/common/collect/MapMaker$StrongEntry;,
        Lcom/google/common/collect/MapMaker$StrongValueReference;,
        Lcom/google/common/collect/MapMaker$ValueReference;,
        Lcom/google/common/collect/MapMaker$WeakEntry;,
        Lcom/google/common/collect/MapMaker$WeakValueReference;
    }
.end annotation


# static fields
.field private static final COMPUTING:Lcom/google/common/collect/MapMaker$ValueReference;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Lcom/google/common/collect/MapMaker$ValueReference<",
            "Ljava/lang/Object;",
            "Ljava/lang/Object;",
            ">;"
        }
    .end annotation
.end field


# instance fields
.field private final builder:Lcom/google/common/collect/CustomConcurrentHashMap$Builder;

.field private expirationNanos:J

.field private keyStrength:Lcom/google/common/collect/MapMaker$Strength;

.field private useCustomMap:Z

.field private valueStrength:Lcom/google/common/collect/MapMaker$Strength;


# direct methods
.method static constructor <clinit>()V
    .locals 1

    .line 751
    new-instance v0, Lcom/google/common/collect/MapMaker$1;

    invoke-direct {v0}, Lcom/google/common/collect/MapMaker$1;-><init>()V

    sput-object v0, Lcom/google/common/collect/MapMaker;->COMPUTING:Lcom/google/common/collect/MapMaker$ValueReference;

    return-void
.end method

.method public constructor <init>()V
    .locals 2

    .line 100
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 90
    sget-object v0, Lcom/google/common/collect/MapMaker$Strength;->STRONG:Lcom/google/common/collect/MapMaker$Strength;

    iput-object v0, p0, Lcom/google/common/collect/MapMaker;->keyStrength:Lcom/google/common/collect/MapMaker$Strength;

    .line 91
    sget-object v0, Lcom/google/common/collect/MapMaker$Strength;->STRONG:Lcom/google/common/collect/MapMaker$Strength;

    iput-object v0, p0, Lcom/google/common/collect/MapMaker;->valueStrength:Lcom/google/common/collect/MapMaker$Strength;

    const-wide/16 v0, 0x0

    .line 92
    iput-wide v0, p0, Lcom/google/common/collect/MapMaker;->expirationNanos:J

    .line 94
    new-instance v0, Lcom/google/common/collect/CustomConcurrentHashMap$Builder;

    invoke-direct {v0}, Lcom/google/common/collect/CustomConcurrentHashMap$Builder;-><init>()V

    iput-object v0, p0, Lcom/google/common/collect/MapMaker;->builder:Lcom/google/common/collect/CustomConcurrentHashMap$Builder;

    return-void
.end method

.method static synthetic access$0(Lcom/google/common/collect/MapMaker;)Lcom/google/common/collect/MapMaker$Strength;
    .locals 0

    .line 90
    iget-object p0, p0, Lcom/google/common/collect/MapMaker;->keyStrength:Lcom/google/common/collect/MapMaker$Strength;

    return-object p0
.end method

.method static synthetic access$1(Lcom/google/common/collect/MapMaker;)Lcom/google/common/collect/MapMaker$Strength;
    .locals 0

    .line 91
    iget-object p0, p0, Lcom/google/common/collect/MapMaker;->valueStrength:Lcom/google/common/collect/MapMaker$Strength;

    return-object p0
.end method

.method static synthetic access$2(Lcom/google/common/collect/MapMaker;)J
    .locals 2

    .line 92
    iget-wide v0, p0, Lcom/google/common/collect/MapMaker;->expirationNanos:J

    return-wide v0
.end method

.method static synthetic access$3(Lcom/google/common/collect/MapMaker;)Lcom/google/common/collect/CustomConcurrentHashMap$Builder;
    .locals 0

    .line 94
    iget-object p0, p0, Lcom/google/common/collect/MapMaker;->builder:Lcom/google/common/collect/CustomConcurrentHashMap$Builder;

    return-object p0
.end method

.method static synthetic access$4()Lcom/google/common/collect/MapMaker$ValueReference;
    .locals 1

    .line 751
    sget-object v0, Lcom/google/common/collect/MapMaker;->COMPUTING:Lcom/google/common/collect/MapMaker$ValueReference;

    return-object v0
.end method

.method static synthetic access$5()Lcom/google/common/collect/MapMaker$ValueReference;
    .locals 1

    .line 770
    invoke-static {}, Lcom/google/common/collect/MapMaker;->computing()Lcom/google/common/collect/MapMaker$ValueReference;

    move-result-object v0

    return-object v0
.end method

.method private static computing()Lcom/google/common/collect/MapMaker$ValueReference;
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "<K:",
            "Ljava/lang/Object;",
            "V:",
            "Ljava/lang/Object;",
            ">()",
            "Lcom/google/common/collect/MapMaker$ValueReference<",
            "TK;TV;>;"
        }
    .end annotation

    .line 771
    sget-object v0, Lcom/google/common/collect/MapMaker;->COMPUTING:Lcom/google/common/collect/MapMaker$ValueReference;

    return-object v0
.end method

.method private setKeyStrength(Lcom/google/common/collect/MapMaker$Strength;)Lcom/google/common/collect/MapMaker;
    .locals 2

    .line 179
    iget-object v0, p0, Lcom/google/common/collect/MapMaker;->keyStrength:Lcom/google/common/collect/MapMaker$Strength;

    sget-object v1, Lcom/google/common/collect/MapMaker$Strength;->STRONG:Lcom/google/common/collect/MapMaker$Strength;

    if-ne v0, v1, :cond_0

    .line 182
    iput-object p1, p0, Lcom/google/common/collect/MapMaker;->keyStrength:Lcom/google/common/collect/MapMaker$Strength;

    const/4 p1, 0x1

    .line 183
    iput-boolean p1, p0, Lcom/google/common/collect/MapMaker;->useCustomMap:Z

    return-object p0

    .line 180
    :cond_0
    new-instance p1, Ljava/lang/IllegalStateException;

    new-instance v0, Ljava/lang/StringBuilder;

    const-string v1, "Key strength was already set to "

    invoke-direct {v0, v1}, Ljava/lang/StringBuilder;-><init>(Ljava/lang/String;)V

    iget-object v1, p0, Lcom/google/common/collect/MapMaker;->keyStrength:Lcom/google/common/collect/MapMaker$Strength;

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/Object;)Ljava/lang/StringBuilder;

    const-string v1, "."

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v0

    invoke-direct {p1, v0}, Ljava/lang/IllegalStateException;-><init>(Ljava/lang/String;)V

    throw p1
.end method

.method private setValueStrength(Lcom/google/common/collect/MapMaker$Strength;)Lcom/google/common/collect/MapMaker;
    .locals 2

    .line 232
    iget-object v0, p0, Lcom/google/common/collect/MapMaker;->valueStrength:Lcom/google/common/collect/MapMaker$Strength;

    sget-object v1, Lcom/google/common/collect/MapMaker$Strength;->STRONG:Lcom/google/common/collect/MapMaker$Strength;

    if-ne v0, v1, :cond_0

    .line 236
    iput-object p1, p0, Lcom/google/common/collect/MapMaker;->valueStrength:Lcom/google/common/collect/MapMaker$Strength;

    const/4 p1, 0x1

    .line 237
    iput-boolean p1, p0, Lcom/google/common/collect/MapMaker;->useCustomMap:Z

    return-object p0

    .line 233
    :cond_0
    new-instance p1, Ljava/lang/IllegalStateException;

    new-instance v0, Ljava/lang/StringBuilder;

    const-string v1, "Value strength was already set to "

    invoke-direct {v0, v1}, Ljava/lang/StringBuilder;-><init>(Ljava/lang/String;)V

    iget-object v1, p0, Lcom/google/common/collect/MapMaker;->valueStrength:Lcom/google/common/collect/MapMaker$Strength;

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/Object;)Ljava/lang/StringBuilder;

    const-string v1, "."

    .line 234
    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v0

    .line 233
    invoke-direct {p1, v0}, Ljava/lang/IllegalStateException;-><init>(Ljava/lang/String;)V

    throw p1
.end method


# virtual methods
.method public concurrencyLevel(I)Lcom/google/common/collect/MapMaker;
    .locals 1

    .line 138
    iget-object v0, p0, Lcom/google/common/collect/MapMaker;->builder:Lcom/google/common/collect/CustomConcurrentHashMap$Builder;

    invoke-virtual {v0, p1}, Lcom/google/common/collect/CustomConcurrentHashMap$Builder;->concurrencyLevel(I)Lcom/google/common/collect/CustomConcurrentHashMap$Builder;

    return-object p0
.end method

.method public expiration(JLjava/util/concurrent/TimeUnit;)Lcom/google/common/collect/MapMaker;
    .locals 5

    .line 256
    iget-wide v0, p0, Lcom/google/common/collect/MapMaker;->expirationNanos:J

    const-wide/16 v2, 0x0

    cmp-long v4, v0, v2

    if-nez v4, :cond_1

    cmp-long v0, p1, v2

    if-lez v0, :cond_0

    .line 263
    invoke-virtual {p3, p1, p2}, Ljava/util/concurrent/TimeUnit;->toNanos(J)J

    move-result-wide p1

    iput-wide p1, p0, Lcom/google/common/collect/MapMaker;->expirationNanos:J

    const/4 p1, 0x1

    .line 264
    iput-boolean p1, p0, Lcom/google/common/collect/MapMaker;->useCustomMap:Z

    return-object p0

    .line 261
    :cond_0
    new-instance p3, Ljava/lang/IllegalArgumentException;

    new-instance v0, Ljava/lang/StringBuilder;

    const-string v1, "invalid duration: "

    invoke-direct {v0, v1}, Ljava/lang/StringBuilder;-><init>(Ljava/lang/String;)V

    invoke-virtual {v0, p1, p2}, Ljava/lang/StringBuilder;->append(J)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p1

    invoke-direct {p3, p1}, Ljava/lang/IllegalArgumentException;-><init>(Ljava/lang/String;)V

    throw p3

    .line 257
    :cond_1
    new-instance p1, Ljava/lang/IllegalStateException;

    new-instance p2, Ljava/lang/StringBuilder;

    const-string p3, "expiration time of "

    invoke-direct {p2, p3}, Ljava/lang/StringBuilder;-><init>(Ljava/lang/String;)V

    iget-wide v0, p0, Lcom/google/common/collect/MapMaker;->expirationNanos:J

    invoke-virtual {p2, v0, v1}, Ljava/lang/StringBuilder;->append(J)Ljava/lang/StringBuilder;

    const-string p3, " ns was already set"

    .line 258
    invoke-virtual {p2, p3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p2}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p2

    .line 257
    invoke-direct {p1, p2}, Ljava/lang/IllegalStateException;-><init>(Ljava/lang/String;)V

    throw p1
.end method

.method public initialCapacity(I)Lcom/google/common/collect/MapMaker;
    .locals 1

    .line 114
    iget-object v0, p0, Lcom/google/common/collect/MapMaker;->builder:Lcom/google/common/collect/CustomConcurrentHashMap$Builder;

    invoke-virtual {v0, p1}, Lcom/google/common/collect/CustomConcurrentHashMap$Builder;->initialCapacity(I)Lcom/google/common/collect/CustomConcurrentHashMap$Builder;

    return-object p0
.end method

.method public makeComputingMap(Lcom/google/common/base/Function;)Ljava/util/concurrent/ConcurrentMap;
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "<K:",
            "Ljava/lang/Object;",
            "V:",
            "Ljava/lang/Object;",
            ">(",
            "Lcom/google/common/base/Function<",
            "-TK;+TV;>;)",
            "Ljava/util/concurrent/ConcurrentMap<",
            "TK;TV;>;"
        }
    .end annotation

    .line 326
    new-instance v0, Lcom/google/common/collect/MapMaker$StrategyImpl;

    invoke-direct {v0, p0, p1}, Lcom/google/common/collect/MapMaker$StrategyImpl;-><init>(Lcom/google/common/collect/MapMaker;Lcom/google/common/base/Function;)V

    iget-object p1, v0, Lcom/google/common/collect/MapMaker$StrategyImpl;->map:Ljava/util/concurrent/ConcurrentMap;

    return-object p1
.end method

.method public makeMap()Ljava/util/concurrent/ConcurrentMap;
    .locals 4
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "<K:",
            "Ljava/lang/Object;",
            "V:",
            "Ljava/lang/Object;",
            ">()",
            "Ljava/util/concurrent/ConcurrentMap<",
            "TK;TV;>;"
        }
    .end annotation

    .line 280
    iget-boolean v0, p0, Lcom/google/common/collect/MapMaker;->useCustomMap:Z

    if-eqz v0, :cond_0

    new-instance v0, Lcom/google/common/collect/MapMaker$StrategyImpl;

    invoke-direct {v0, p0}, Lcom/google/common/collect/MapMaker$StrategyImpl;-><init>(Lcom/google/common/collect/MapMaker;)V

    iget-object v0, v0, Lcom/google/common/collect/MapMaker$StrategyImpl;->map:Ljava/util/concurrent/ConcurrentMap;

    goto :goto_0

    :cond_0
    new-instance v0, Ljava/util/concurrent/ConcurrentHashMap;

    .line 281
    iget-object v1, p0, Lcom/google/common/collect/MapMaker;->builder:Lcom/google/common/collect/CustomConcurrentHashMap$Builder;

    invoke-virtual {v1}, Lcom/google/common/collect/CustomConcurrentHashMap$Builder;->getInitialCapacity()I

    move-result v1

    const/high16 v2, 0x3f400000    # 0.75f

    iget-object v3, p0, Lcom/google/common/collect/MapMaker;->builder:Lcom/google/common/collect/CustomConcurrentHashMap$Builder;

    invoke-virtual {v3}, Lcom/google/common/collect/CustomConcurrentHashMap$Builder;->getConcurrencyLevel()I

    move-result v3

    invoke-direct {v0, v1, v2, v3}, Ljava/util/concurrent/ConcurrentHashMap;-><init>(IFI)V

    :goto_0
    return-object v0
.end method

.method public softKeys()Lcom/google/common/collect/MapMaker;
    .locals 1

    .line 175
    sget-object v0, Lcom/google/common/collect/MapMaker$Strength;->SOFT:Lcom/google/common/collect/MapMaker$Strength;

    invoke-direct {p0, v0}, Lcom/google/common/collect/MapMaker;->setKeyStrength(Lcom/google/common/collect/MapMaker$Strength;)Lcom/google/common/collect/MapMaker;

    move-result-object v0

    return-object v0
.end method

.method public softValues()Lcom/google/common/collect/MapMaker;
    .locals 1

    .line 228
    sget-object v0, Lcom/google/common/collect/MapMaker$Strength;->SOFT:Lcom/google/common/collect/MapMaker$Strength;

    invoke-direct {p0, v0}, Lcom/google/common/collect/MapMaker;->setValueStrength(Lcom/google/common/collect/MapMaker$Strength;)Lcom/google/common/collect/MapMaker;

    move-result-object v0

    return-object v0
.end method

.method public weakKeys()Lcom/google/common/collect/MapMaker;
    .locals 1

    .line 157
    sget-object v0, Lcom/google/common/collect/MapMaker$Strength;->WEAK:Lcom/google/common/collect/MapMaker$Strength;

    invoke-direct {p0, v0}, Lcom/google/common/collect/MapMaker;->setKeyStrength(Lcom/google/common/collect/MapMaker$Strength;)Lcom/google/common/collect/MapMaker;

    move-result-object v0

    return-object v0
.end method

.method public weakValues()Lcom/google/common/collect/MapMaker;
    .locals 1

    .line 206
    sget-object v0, Lcom/google/common/collect/MapMaker$Strength;->WEAK:Lcom/google/common/collect/MapMaker$Strength;

    invoke-direct {p0, v0}, Lcom/google/common/collect/MapMaker;->setValueStrength(Lcom/google/common/collect/MapMaker$Strength;)Lcom/google/common/collect/MapMaker;

    move-result-object v0

    return-object v0
.end method
