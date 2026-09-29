.class Lcom/google/common/collect/CustomConcurrentHashMap$Impl$InternalsImpl;
.super Ljava/lang/Object;
.source "CustomConcurrentHashMap.java"

# interfaces
.implements Lcom/google/common/collect/CustomConcurrentHashMap$Internals;
.implements Ljava/io/Serializable;


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lcom/google/common/collect/CustomConcurrentHashMap$Impl;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = "InternalsImpl"
.end annotation

.annotation system Ldalvik/annotation/Signature;
    value = {
        "Ljava/lang/Object;",
        "Lcom/google/common/collect/CustomConcurrentHashMap$Internals<",
        "TK;TV;TE;>;",
        "Ljava/io/Serializable;"
    }
.end annotation


# static fields
.field static final serialVersionUID:J


# instance fields
.field final synthetic this$1:Lcom/google/common/collect/CustomConcurrentHashMap$Impl;


# direct methods
.method constructor <init>(Lcom/google/common/collect/CustomConcurrentHashMap$Impl;)V
    .locals 0

    .line 667
    iput-object p1, p0, Lcom/google/common/collect/CustomConcurrentHashMap$Impl$InternalsImpl;->this$1:Lcom/google/common/collect/CustomConcurrentHashMap$Impl;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public getEntry(Ljava/lang/Object;)Ljava/lang/Object;
    .locals 2
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(TK;)TE;"
        }
    .end annotation

    if-eqz p1, :cond_0

    .line 675
    iget-object v0, p0, Lcom/google/common/collect/CustomConcurrentHashMap$Impl$InternalsImpl;->this$1:Lcom/google/common/collect/CustomConcurrentHashMap$Impl;

    invoke-virtual {v0, p1}, Lcom/google/common/collect/CustomConcurrentHashMap$Impl;->hash(Ljava/lang/Object;)I

    move-result v0

    .line 676
    iget-object v1, p0, Lcom/google/common/collect/CustomConcurrentHashMap$Impl$InternalsImpl;->this$1:Lcom/google/common/collect/CustomConcurrentHashMap$Impl;

    invoke-virtual {v1, v0}, Lcom/google/common/collect/CustomConcurrentHashMap$Impl;->segmentFor(I)Lcom/google/common/collect/CustomConcurrentHashMap$Impl$Segment;

    move-result-object v1

    invoke-virtual {v1, p1, v0}, Lcom/google/common/collect/CustomConcurrentHashMap$Impl$Segment;->getEntry(Ljava/lang/Object;I)Ljava/lang/Object;

    move-result-object p1

    return-object p1

    .line 673
    :cond_0
    new-instance p1, Ljava/lang/NullPointerException;

    const-string v0, "key"

    invoke-direct {p1, v0}, Ljava/lang/NullPointerException;-><init>(Ljava/lang/String;)V

    throw p1
.end method

.method public removeEntry(Ljava/lang/Object;)Z
    .locals 2
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(TE;)Z"
        }
    .end annotation

    if-eqz p1, :cond_0

    .line 691
    iget-object v0, p0, Lcom/google/common/collect/CustomConcurrentHashMap$Impl$InternalsImpl;->this$1:Lcom/google/common/collect/CustomConcurrentHashMap$Impl;

    iget-object v0, v0, Lcom/google/common/collect/CustomConcurrentHashMap$Impl;->strategy:Lcom/google/common/collect/CustomConcurrentHashMap$Strategy;

    invoke-interface {v0, p1}, Lcom/google/common/collect/CustomConcurrentHashMap$Strategy;->getHash(Ljava/lang/Object;)I

    move-result v0

    .line 692
    iget-object v1, p0, Lcom/google/common/collect/CustomConcurrentHashMap$Impl$InternalsImpl;->this$1:Lcom/google/common/collect/CustomConcurrentHashMap$Impl;

    invoke-virtual {v1, v0}, Lcom/google/common/collect/CustomConcurrentHashMap$Impl;->segmentFor(I)Lcom/google/common/collect/CustomConcurrentHashMap$Impl$Segment;

    move-result-object v1

    invoke-virtual {v1, p1, v0}, Lcom/google/common/collect/CustomConcurrentHashMap$Impl$Segment;->removeEntry(Ljava/lang/Object;I)Z

    move-result p1

    return p1

    .line 689
    :cond_0
    new-instance p1, Ljava/lang/NullPointerException;

    const-string v0, "entry"

    invoke-direct {p1, v0}, Ljava/lang/NullPointerException;-><init>(Ljava/lang/String;)V

    throw p1
.end method

.method public removeEntry(Ljava/lang/Object;Ljava/lang/Object;)Z
    .locals 2
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(TE;TV;)Z"
        }
    .end annotation

    if-eqz p1, :cond_0

    .line 683
    iget-object v0, p0, Lcom/google/common/collect/CustomConcurrentHashMap$Impl$InternalsImpl;->this$1:Lcom/google/common/collect/CustomConcurrentHashMap$Impl;

    iget-object v0, v0, Lcom/google/common/collect/CustomConcurrentHashMap$Impl;->strategy:Lcom/google/common/collect/CustomConcurrentHashMap$Strategy;

    invoke-interface {v0, p1}, Lcom/google/common/collect/CustomConcurrentHashMap$Strategy;->getHash(Ljava/lang/Object;)I

    move-result v0

    .line 684
    iget-object v1, p0, Lcom/google/common/collect/CustomConcurrentHashMap$Impl$InternalsImpl;->this$1:Lcom/google/common/collect/CustomConcurrentHashMap$Impl;

    invoke-virtual {v1, v0}, Lcom/google/common/collect/CustomConcurrentHashMap$Impl;->segmentFor(I)Lcom/google/common/collect/CustomConcurrentHashMap$Impl$Segment;

    move-result-object v1

    invoke-virtual {v1, p1, v0, p2}, Lcom/google/common/collect/CustomConcurrentHashMap$Impl$Segment;->removeEntry(Ljava/lang/Object;ILjava/lang/Object;)Z

    move-result p1

    return p1

    .line 681
    :cond_0
    new-instance p1, Ljava/lang/NullPointerException;

    const-string p2, "entry"

    invoke-direct {p1, p2}, Ljava/lang/NullPointerException;-><init>(Ljava/lang/String;)V

    throw p1
.end method
