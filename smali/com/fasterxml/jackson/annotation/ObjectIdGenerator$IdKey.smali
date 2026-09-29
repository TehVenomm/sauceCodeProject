.class public final Lcom/fasterxml/jackson/annotation/ObjectIdGenerator$IdKey;
.super Ljava/lang/Object;
.source "ObjectIdGenerator.java"

# interfaces
.implements Ljava/io/Serializable;


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lcom/fasterxml/jackson/annotation/ObjectIdGenerator;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x19
    name = "IdKey"
.end annotation


# static fields
.field private static final serialVersionUID:J = 0x1L


# instance fields
.field private final hashCode:I

.field public final key:Ljava/lang/Object;

.field public final scope:Ljava/lang/Class;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/lang/Class<",
            "*>;"
        }
    .end annotation
.end field

.field public final type:Ljava/lang/Class;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/lang/Class<",
            "*>;"
        }
    .end annotation
.end field


# direct methods
.method public constructor <init>(Ljava/lang/Class;Ljava/lang/Class;Ljava/lang/Object;)V
    .locals 0
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/lang/Class<",
            "*>;",
            "Ljava/lang/Class<",
            "*>;",
            "Ljava/lang/Object;",
            ")V"
        }
    .end annotation

    .line 153
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 154
    iput-object p1, p0, Lcom/fasterxml/jackson/annotation/ObjectIdGenerator$IdKey;->type:Ljava/lang/Class;

    .line 155
    iput-object p2, p0, Lcom/fasterxml/jackson/annotation/ObjectIdGenerator$IdKey;->scope:Ljava/lang/Class;

    .line 156
    iput-object p3, p0, Lcom/fasterxml/jackson/annotation/ObjectIdGenerator$IdKey;->key:Ljava/lang/Object;

    .line 158
    invoke-virtual {p3}, Ljava/lang/Object;->hashCode()I

    move-result p3

    invoke-virtual {p1}, Ljava/lang/Class;->getName()Ljava/lang/String;

    move-result-object p1

    invoke-virtual {p1}, Ljava/lang/String;->hashCode()I

    move-result p1

    add-int/2addr p3, p1

    if-eqz p2, :cond_0

    .line 160
    invoke-virtual {p2}, Ljava/lang/Class;->getName()Ljava/lang/String;

    move-result-object p1

    invoke-virtual {p1}, Ljava/lang/String;->hashCode()I

    move-result p1

    xor-int/2addr p3, p1

    .line 162
    :cond_0
    iput p3, p0, Lcom/fasterxml/jackson/annotation/ObjectIdGenerator$IdKey;->hashCode:I

    return-void
.end method


# virtual methods
.method public equals(Ljava/lang/Object;)Z
    .locals 4

    const/4 v0, 0x1

    if-ne p1, p0, :cond_0

    return v0

    :cond_0
    const/4 v1, 0x0

    if-nez p1, :cond_1

    return v1

    .line 173
    :cond_1
    invoke-virtual {p1}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    move-result-object v2

    invoke-virtual {p0}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    move-result-object v3

    if-eq v2, v3, :cond_2

    return v1

    .line 174
    :cond_2
    check-cast p1, Lcom/fasterxml/jackson/annotation/ObjectIdGenerator$IdKey;

    .line 175
    iget-object v2, p1, Lcom/fasterxml/jackson/annotation/ObjectIdGenerator$IdKey;->key:Ljava/lang/Object;

    iget-object v3, p0, Lcom/fasterxml/jackson/annotation/ObjectIdGenerator$IdKey;->key:Ljava/lang/Object;

    invoke-virtual {v2, v3}, Ljava/lang/Object;->equals(Ljava/lang/Object;)Z

    move-result v2

    if-eqz v2, :cond_3

    iget-object v2, p1, Lcom/fasterxml/jackson/annotation/ObjectIdGenerator$IdKey;->type:Ljava/lang/Class;

    iget-object v3, p0, Lcom/fasterxml/jackson/annotation/ObjectIdGenerator$IdKey;->type:Ljava/lang/Class;

    if-ne v2, v3, :cond_3

    iget-object p1, p1, Lcom/fasterxml/jackson/annotation/ObjectIdGenerator$IdKey;->scope:Ljava/lang/Class;

    iget-object v2, p0, Lcom/fasterxml/jackson/annotation/ObjectIdGenerator$IdKey;->scope:Ljava/lang/Class;

    if-ne p1, v2, :cond_3

    goto :goto_0

    :cond_3
    const/4 v0, 0x0

    :goto_0
    return v0
.end method

.method public hashCode()I
    .locals 1

    .line 166
    iget v0, p0, Lcom/fasterxml/jackson/annotation/ObjectIdGenerator$IdKey;->hashCode:I

    return v0
.end method
