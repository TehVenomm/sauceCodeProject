.class public Lcom/helpshift/support/FaqTagFilter;
.super Ljava/lang/Object;
.source "FaqTagFilter.java"

# interfaces
.implements Ljava/io/Serializable;


# annotations
.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Lcom/helpshift/support/FaqTagFilter$Operator;
    }
.end annotation


# static fields
.field private static final serialVersionUID:J = 0x687366ad086a1553L


# instance fields
.field private operator:Ljava/lang/String;

.field private tags:[Ljava/lang/String;


# direct methods
.method public constructor <init>(Ljava/lang/String;[Ljava/lang/String;)V
    .locals 1

    .line 23
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    const-string v0, "undefined"

    .line 14
    iput-object v0, p0, Lcom/helpshift/support/FaqTagFilter;->operator:Ljava/lang/String;

    .line 24
    sget-object v0, Lcom/helpshift/support/FaqTagFilter$Operator;->valueSet:Ljava/util/HashSet;

    invoke-virtual {v0, p1}, Ljava/util/HashSet;->contains(Ljava/lang/Object;)Z

    move-result v0

    if-eqz v0, :cond_0

    .line 25
    iput-object p1, p0, Lcom/helpshift/support/FaqTagFilter;->operator:Ljava/lang/String;

    .line 27
    :cond_0
    iput-object p2, p0, Lcom/helpshift/support/FaqTagFilter;->tags:[Ljava/lang/String;

    return-void
.end method


# virtual methods
.method public equals(Ljava/lang/Object;)Z
    .locals 2

    .line 65
    instance-of v0, p1, Lcom/helpshift/support/FaqTagFilter;

    if-eqz v0, :cond_0

    iget-object v0, p0, Lcom/helpshift/support/FaqTagFilter;->operator:Ljava/lang/String;

    check-cast p1, Lcom/helpshift/support/FaqTagFilter;

    iget-object v1, p1, Lcom/helpshift/support/FaqTagFilter;->operator:Ljava/lang/String;

    .line 66
    invoke-virtual {v0, v1}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v0

    if-eqz v0, :cond_0

    iget-object v0, p0, Lcom/helpshift/support/FaqTagFilter;->tags:[Ljava/lang/String;

    iget-object p1, p1, Lcom/helpshift/support/FaqTagFilter;->tags:[Ljava/lang/String;

    .line 67
    invoke-static {v0, p1}, Ljava/util/Arrays;->equals([Ljava/lang/Object;[Ljava/lang/Object;)Z

    move-result p1

    if-eqz p1, :cond_0

    const/4 p1, 0x1

    goto :goto_0

    :cond_0
    const/4 p1, 0x0

    :goto_0
    return p1
.end method

.method public getOperator()Ljava/lang/String;
    .locals 1

    .line 34
    iget-object v0, p0, Lcom/helpshift/support/FaqTagFilter;->operator:Ljava/lang/String;

    return-object v0
.end method

.method public getTags()[Ljava/lang/String;
    .locals 1

    .line 41
    iget-object v0, p0, Lcom/helpshift/support/FaqTagFilter;->tags:[Ljava/lang/String;

    return-object v0
.end method

.method protected toMap()Ljava/util/Map;
    .locals 3
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()",
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Ljava/lang/Object;",
            ">;"
        }
    .end annotation

    .line 49
    iget-object v0, p0, Lcom/helpshift/support/FaqTagFilter;->operator:Ljava/lang/String;

    if-eqz v0, :cond_0

    sget-object v0, Lcom/helpshift/support/FaqTagFilter$Operator;->valueSet:Ljava/util/HashSet;

    iget-object v1, p0, Lcom/helpshift/support/FaqTagFilter;->operator:Ljava/lang/String;

    .line 50
    invoke-virtual {v0, v1}, Ljava/util/HashSet;->contains(Ljava/lang/Object;)Z

    move-result v0

    if-eqz v0, :cond_0

    iget-object v0, p0, Lcom/helpshift/support/FaqTagFilter;->tags:[Ljava/lang/String;

    if-eqz v0, :cond_0

    iget-object v0, p0, Lcom/helpshift/support/FaqTagFilter;->tags:[Ljava/lang/String;

    array-length v0, v0

    if-lez v0, :cond_0

    .line 53
    new-instance v0, Ljava/util/HashMap;

    invoke-direct {v0}, Ljava/util/HashMap;-><init>()V

    const-string v1, "operator"

    .line 54
    iget-object v2, p0, Lcom/helpshift/support/FaqTagFilter;->operator:Ljava/lang/String;

    invoke-interface {v0, v1, v2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string v1, "tags"

    .line 55
    iget-object v2, p0, Lcom/helpshift/support/FaqTagFilter;->tags:[Ljava/lang/String;

    invoke-interface {v0, v1, v2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    goto :goto_0

    :cond_0
    const/4 v0, 0x0

    :goto_0
    return-object v0
.end method
