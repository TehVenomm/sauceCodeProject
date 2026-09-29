.class Lcom/google/common/collect/MapMaker$StrategyImpl$FutureValueReference;
.super Ljava/lang/Object;
.source "MapMaker.java"

# interfaces
.implements Lcom/google/common/collect/MapMaker$ValueReference;


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lcom/google/common/collect/MapMaker$StrategyImpl;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x2
    name = "FutureValueReference"
.end annotation

.annotation system Ldalvik/annotation/Signature;
    value = {
        "Ljava/lang/Object;",
        "Lcom/google/common/collect/MapMaker$ValueReference<",
        "TK;TV;>;"
    }
.end annotation


# instance fields
.field final newEntry:Lcom/google/common/collect/MapMaker$ReferenceEntry;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Lcom/google/common/collect/MapMaker$ReferenceEntry<",
            "TK;TV;>;"
        }
    .end annotation
.end field

.field final original:Lcom/google/common/collect/MapMaker$ReferenceEntry;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Lcom/google/common/collect/MapMaker$ReferenceEntry<",
            "TK;TV;>;"
        }
    .end annotation
.end field

.field final synthetic this$1:Lcom/google/common/collect/MapMaker$StrategyImpl;


# direct methods
.method constructor <init>(Lcom/google/common/collect/MapMaker$StrategyImpl;Lcom/google/common/collect/MapMaker$ReferenceEntry;Lcom/google/common/collect/MapMaker$ReferenceEntry;)V
    .locals 0
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Lcom/google/common/collect/MapMaker$ReferenceEntry<",
            "TK;TV;>;",
            "Lcom/google/common/collect/MapMaker$ReferenceEntry<",
            "TK;TV;>;)V"
        }
    .end annotation

    .line 628
    iput-object p1, p0, Lcom/google/common/collect/MapMaker$StrategyImpl$FutureValueReference;->this$1:Lcom/google/common/collect/MapMaker$StrategyImpl;

    .line 625
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 626
    iput-object p2, p0, Lcom/google/common/collect/MapMaker$StrategyImpl$FutureValueReference;->original:Lcom/google/common/collect/MapMaker$ReferenceEntry;

    .line 627
    iput-object p3, p0, Lcom/google/common/collect/MapMaker$StrategyImpl$FutureValueReference;->newEntry:Lcom/google/common/collect/MapMaker$ReferenceEntry;

    return-void
.end method


# virtual methods
.method public copyFor(Lcom/google/common/collect/MapMaker$ReferenceEntry;)Lcom/google/common/collect/MapMaker$ValueReference;
    .locals 3
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Lcom/google/common/collect/MapMaker$ReferenceEntry<",
            "TK;TV;>;)",
            "Lcom/google/common/collect/MapMaker$ValueReference<",
            "TK;TV;>;"
        }
    .end annotation

    .line 644
    new-instance v0, Lcom/google/common/collect/MapMaker$StrategyImpl$FutureValueReference;

    iget-object v1, p0, Lcom/google/common/collect/MapMaker$StrategyImpl$FutureValueReference;->this$1:Lcom/google/common/collect/MapMaker$StrategyImpl;

    iget-object v2, p0, Lcom/google/common/collect/MapMaker$StrategyImpl$FutureValueReference;->original:Lcom/google/common/collect/MapMaker$ReferenceEntry;

    invoke-direct {v0, v1, v2, p1}, Lcom/google/common/collect/MapMaker$StrategyImpl$FutureValueReference;-><init>(Lcom/google/common/collect/MapMaker$StrategyImpl;Lcom/google/common/collect/MapMaker$ReferenceEntry;Lcom/google/common/collect/MapMaker$ReferenceEntry;)V

    return-object v0
.end method

.method public get()Ljava/lang/Object;
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()TV;"
        }
    .end annotation

    .line 633
    :try_start_0
    iget-object v0, p0, Lcom/google/common/collect/MapMaker$StrategyImpl$FutureValueReference;->original:Lcom/google/common/collect/MapMaker$ReferenceEntry;

    invoke-interface {v0}, Lcom/google/common/collect/MapMaker$ReferenceEntry;->getValueReference()Lcom/google/common/collect/MapMaker$ValueReference;

    move-result-object v0

    invoke-interface {v0}, Lcom/google/common/collect/MapMaker$ValueReference;->get()Ljava/lang/Object;

    move-result-object v0
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    return-object v0

    :catchall_0
    move-exception v0

    .line 638
    invoke-virtual {p0}, Lcom/google/common/collect/MapMaker$StrategyImpl$FutureValueReference;->removeEntry()V

    .line 640
    throw v0
.end method

.method removeEntry()V
    .locals 2

    .line 667
    iget-object v0, p0, Lcom/google/common/collect/MapMaker$StrategyImpl$FutureValueReference;->this$1:Lcom/google/common/collect/MapMaker$StrategyImpl;

    iget-object v0, v0, Lcom/google/common/collect/MapMaker$StrategyImpl;->internals:Lcom/google/common/collect/CustomConcurrentHashMap$Internals;

    iget-object v1, p0, Lcom/google/common/collect/MapMaker$StrategyImpl$FutureValueReference;->newEntry:Lcom/google/common/collect/MapMaker$ReferenceEntry;

    invoke-interface {v0, v1}, Lcom/google/common/collect/CustomConcurrentHashMap$Internals;->removeEntry(Ljava/lang/Object;)Z

    return-void
.end method

.method public waitForValue()Ljava/lang/Object;
    .locals 2
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()TV;"
        }
    .end annotation

    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/lang/InterruptedException;
        }
    .end annotation

    .line 651
    :try_start_0
    iget-object v0, p0, Lcom/google/common/collect/MapMaker$StrategyImpl$FutureValueReference;->this$1:Lcom/google/common/collect/MapMaker$StrategyImpl;

    iget-object v1, p0, Lcom/google/common/collect/MapMaker$StrategyImpl$FutureValueReference;->original:Lcom/google/common/collect/MapMaker$ReferenceEntry;

    invoke-virtual {v0, v1}, Lcom/google/common/collect/MapMaker$StrategyImpl;->waitForValue(Lcom/google/common/collect/MapMaker$ReferenceEntry;)Ljava/lang/Object;

    move-result-object v0
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    return-object v0

    :catchall_0
    move-exception v0

    .line 656
    invoke-virtual {p0}, Lcom/google/common/collect/MapMaker$StrategyImpl$FutureValueReference;->removeEntry()V

    .line 658
    throw v0
.end method
