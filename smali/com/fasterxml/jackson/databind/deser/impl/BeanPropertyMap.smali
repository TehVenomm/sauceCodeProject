.class public final Lcom/fasterxml/jackson/databind/deser/impl/BeanPropertyMap;
.super Ljava/lang/Object;
.source "BeanPropertyMap.java"

# interfaces
.implements Ljava/lang/Iterable;
.implements Ljava/io/Serializable;


# annotations
.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Lcom/fasterxml/jackson/databind/deser/impl/BeanPropertyMap$IteratorImpl;,
        Lcom/fasterxml/jackson/databind/deser/impl/BeanPropertyMap$Bucket;
    }
.end annotation

.annotation system Ldalvik/annotation/Signature;
    value = {
        "Ljava/lang/Object;",
        "Ljava/lang/Iterable<",
        "Lcom/fasterxml/jackson/databind/deser/SettableBeanProperty;",
        ">;",
        "Ljava/io/Serializable;"
    }
.end annotation


# static fields
.field private static final serialVersionUID:J = 0x1L


# instance fields
.field private final _buckets:[Lcom/fasterxml/jackson/databind/deser/impl/BeanPropertyMap$Bucket;

.field private final _caseInsensitive:Z

.field private final _hashMask:I

.field private _nextBucketIndex:I

.field private final _size:I


# direct methods
.method public constructor <init>(Ljava/util/Collection;Z)V
    .locals 7
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/Collection<",
            "Lcom/fasterxml/jackson/databind/deser/SettableBeanProperty;",
            ">;Z)V"
        }
    .end annotation

    .line 54
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    const/4 v0, 0x0

    .line 51
    iput v0, p0, Lcom/fasterxml/jackson/databind/deser/impl/BeanPropertyMap;->_nextBucketIndex:I

    .line 55
    iput-boolean p2, p0, Lcom/fasterxml/jackson/databind/deser/impl/BeanPropertyMap;->_caseInsensitive:Z

    .line 56
    invoke-interface {p1}, Ljava/util/Collection;->size()I

    move-result p2

    iput p2, p0, Lcom/fasterxml/jackson/databind/deser/impl/BeanPropertyMap;->_size:I

    .line 57
    iget p2, p0, Lcom/fasterxml/jackson/databind/deser/impl/BeanPropertyMap;->_size:I

    invoke-static {p2}, Lcom/fasterxml/jackson/databind/deser/impl/BeanPropertyMap;->findSize(I)I

    move-result p2

    add-int/lit8 v0, p2, -0x1

    .line 58
    iput v0, p0, Lcom/fasterxml/jackson/databind/deser/impl/BeanPropertyMap;->_hashMask:I

    .line 59
    new-array p2, p2, [Lcom/fasterxml/jackson/databind/deser/impl/BeanPropertyMap$Bucket;

    .line 60
    invoke-interface {p1}, Ljava/util/Collection;->iterator()Ljava/util/Iterator;

    move-result-object p1

    :goto_0
    invoke-interface {p1}, Ljava/util/Iterator;->hasNext()Z

    move-result v0

    if-eqz v0, :cond_0

    invoke-interface {p1}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Lcom/fasterxml/jackson/databind/deser/SettableBeanProperty;

    .line 61
    invoke-direct {p0, v0}, Lcom/fasterxml/jackson/databind/deser/impl/BeanPropertyMap;->getPropertyName(Lcom/fasterxml/jackson/databind/deser/SettableBeanProperty;)Ljava/lang/String;

    move-result-object v1

    .line 62
    invoke-virtual {v1}, Ljava/lang/String;->hashCode()I

    move-result v2

    iget v3, p0, Lcom/fasterxml/jackson/databind/deser/impl/BeanPropertyMap;->_hashMask:I

    and-int/2addr v2, v3

    .line 63
    new-instance v3, Lcom/fasterxml/jackson/databind/deser/impl/BeanPropertyMap$Bucket;

    aget-object v4, p2, v2

    iget v5, p0, Lcom/fasterxml/jackson/databind/deser/impl/BeanPropertyMap;->_nextBucketIndex:I

    add-int/lit8 v6, v5, 0x1

    iput v6, p0, Lcom/fasterxml/jackson/databind/deser/impl/BeanPropertyMap;->_nextBucketIndex:I

    invoke-direct {v3, v4, v1, v0, v5}, Lcom/fasterxml/jackson/databind/deser/impl/BeanPropertyMap$Bucket;-><init>(Lcom/fasterxml/jackson/databind/deser/impl/BeanPropertyMap$Bucket;Ljava/lang/String;Lcom/fasterxml/jackson/databind/deser/SettableBeanProperty;I)V

    aput-object v3, p2, v2

    goto :goto_0

    .line 65
    :cond_0
    iput-object p2, p0, Lcom/fasterxml/jackson/databind/deser/impl/BeanPropertyMap;->_buckets:[Lcom/fasterxml/jackson/databind/deser/impl/BeanPropertyMap$Bucket;

    return-void
.end method

.method private constructor <init>([Lcom/fasterxml/jackson/databind/deser/impl/BeanPropertyMap$Bucket;IIZ)V
    .locals 1

    .line 69
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    const/4 v0, 0x0

    .line 51
    iput v0, p0, Lcom/fasterxml/jackson/databind/deser/impl/BeanPropertyMap;->_nextBucketIndex:I

    .line 70
    iput-object p1, p0, Lcom/fasterxml/jackson/databind/deser/impl/BeanPropertyMap;->_buckets:[Lcom/fasterxml/jackson/databind/deser/impl/BeanPropertyMap$Bucket;

    .line 71
    iput p2, p0, Lcom/fasterxml/jackson/databind/deser/impl/BeanPropertyMap;->_size:I

    .line 72
    array-length p1, p1

    add-int/lit8 p1, p1, -0x1

    iput p1, p0, Lcom/fasterxml/jackson/databind/deser/impl/BeanPropertyMap;->_hashMask:I

    .line 73
    iput p3, p0, Lcom/fasterxml/jackson/databind/deser/impl/BeanPropertyMap;->_nextBucketIndex:I

    .line 74
    iput-boolean p4, p0, Lcom/fasterxml/jackson/databind/deser/impl/BeanPropertyMap;->_caseInsensitive:Z

    return-void
.end method

.method private final _findDeserializeAndSet2(Lcom/fasterxml/jackson/core/JsonParser;Lcom/fasterxml/jackson/databind/DeserializationContext;Ljava/lang/Object;Ljava/lang/String;I)Z
    .locals 2
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    .line 298
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/deser/impl/BeanPropertyMap;->_buckets:[Lcom/fasterxml/jackson/databind/deser/impl/BeanPropertyMap$Bucket;

    aget-object v0, v0, p5

    .line 300
    :cond_0
    iget-object v0, v0, Lcom/fasterxml/jackson/databind/deser/impl/BeanPropertyMap$Bucket;->next:Lcom/fasterxml/jackson/databind/deser/impl/BeanPropertyMap$Bucket;

    if-nez v0, :cond_1

    .line 301
    invoke-direct {p0, p4, p5}, Lcom/fasterxml/jackson/databind/deser/impl/BeanPropertyMap;->_findWithEquals(Ljava/lang/String;I)Lcom/fasterxml/jackson/databind/deser/SettableBeanProperty;

    move-result-object p5

    if-nez p5, :cond_2

    const/4 p1, 0x0

    return p1

    .line 307
    :cond_1
    iget-object v1, v0, Lcom/fasterxml/jackson/databind/deser/impl/BeanPropertyMap$Bucket;->key:Ljava/lang/String;

    if-ne v1, p4, :cond_0

    .line 308
    iget-object p5, v0, Lcom/fasterxml/jackson/databind/deser/impl/BeanPropertyMap$Bucket;->value:Lcom/fasterxml/jackson/databind/deser/SettableBeanProperty;

    .line 313
    :cond_2
    :try_start_0
    invoke-virtual {p5, p1, p2, p3}, Lcom/fasterxml/jackson/databind/deser/SettableBeanProperty;->deserializeAndSet(Lcom/fasterxml/jackson/core/JsonParser;Lcom/fasterxml/jackson/databind/DeserializationContext;Ljava/lang/Object;)V
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception p1

    .line 315
    invoke-virtual {p0, p1, p3, p4, p2}, Lcom/fasterxml/jackson/databind/deser/impl/BeanPropertyMap;->wrapAndThrow(Ljava/lang/Throwable;Ljava/lang/Object;Ljava/lang/String;Lcom/fasterxml/jackson/databind/DeserializationContext;)V

    :goto_0
    const/4 p1, 0x1

    return p1
.end method

.method private _findWithEquals(Ljava/lang/String;I)Lcom/fasterxml/jackson/databind/deser/SettableBeanProperty;
    .locals 1

    .line 403
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/deser/impl/BeanPropertyMap;->_buckets:[Lcom/fasterxml/jackson/databind/deser/impl/BeanPropertyMap$Bucket;

    aget-object p2, v0, p2

    :goto_0
    if-eqz p2, :cond_1

    .line 405
    iget-object v0, p2, Lcom/fasterxml/jackson/databind/deser/impl/BeanPropertyMap$Bucket;->key:Ljava/lang/String;

    invoke-virtual {p1, v0}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v0

    if-eqz v0, :cond_0

    .line 406
    iget-object p1, p2, Lcom/fasterxml/jackson/databind/deser/impl/BeanPropertyMap$Bucket;->value:Lcom/fasterxml/jackson/databind/deser/SettableBeanProperty;

    return-object p1

    .line 408
    :cond_0
    iget-object p2, p2, Lcom/fasterxml/jackson/databind/deser/impl/BeanPropertyMap$Bucket;->next:Lcom/fasterxml/jackson/databind/deser/impl/BeanPropertyMap$Bucket;

    goto :goto_0

    :cond_1
    const/4 p1, 0x0

    return-object p1
.end method

.method private static final findSize(I)I
    .locals 1

    const/16 v0, 0x20

    if-gt p0, v0, :cond_0

    add-int/2addr p0, p0

    goto :goto_0

    :cond_0
    shr-int/lit8 v0, p0, 0x2

    add-int/2addr p0, v0

    :goto_0
    const/4 v0, 0x2

    :goto_1
    if-ge v0, p0, :cond_1

    add-int/2addr v0, v0

    goto :goto_1

    :cond_1
    return v0
.end method

.method private getPropertyName(Lcom/fasterxml/jackson/databind/deser/SettableBeanProperty;)Ljava/lang/String;
    .locals 1

    .line 167
    iget-boolean v0, p0, Lcom/fasterxml/jackson/databind/deser/impl/BeanPropertyMap;->_caseInsensitive:Z

    if-eqz v0, :cond_0

    invoke-virtual {p1}, Lcom/fasterxml/jackson/databind/deser/SettableBeanProperty;->getName()Ljava/lang/String;

    move-result-object p1

    invoke-virtual {p1}, Ljava/lang/String;->toLowerCase()Ljava/lang/String;

    move-result-object p1

    goto :goto_0

    :cond_0
    invoke-virtual {p1}, Lcom/fasterxml/jackson/databind/deser/SettableBeanProperty;->getName()Ljava/lang/String;

    move-result-object p1

    :goto_0
    return-object p1
.end method


# virtual methods
.method public assignIndexes()Lcom/fasterxml/jackson/databind/deser/impl/BeanPropertyMap;
    .locals 7

    .line 144
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/deser/impl/BeanPropertyMap;->_buckets:[Lcom/fasterxml/jackson/databind/deser/impl/BeanPropertyMap$Bucket;

    array-length v1, v0

    const/4 v2, 0x0

    const/4 v3, 0x0

    :goto_0
    if-ge v2, v1, :cond_1

    aget-object v4, v0, v2

    :goto_1
    if-eqz v4, :cond_0

    .line 146
    iget-object v5, v4, Lcom/fasterxml/jackson/databind/deser/impl/BeanPropertyMap$Bucket;->value:Lcom/fasterxml/jackson/databind/deser/SettableBeanProperty;

    add-int/lit8 v6, v3, 0x1

    invoke-virtual {v5, v3}, Lcom/fasterxml/jackson/databind/deser/SettableBeanProperty;->assignIndex(I)V

    .line 147
    iget-object v4, v4, Lcom/fasterxml/jackson/databind/deser/impl/BeanPropertyMap$Bucket;->next:Lcom/fasterxml/jackson/databind/deser/impl/BeanPropertyMap$Bucket;

    move v3, v6

    goto :goto_1

    :cond_0
    add-int/lit8 v2, v2, 0x1

    goto :goto_0

    :cond_1
    return-object p0
.end method

.method public find(I)Lcom/fasterxml/jackson/databind/deser/SettableBeanProperty;
    .locals 4

    .line 325
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/deser/impl/BeanPropertyMap;->_buckets:[Lcom/fasterxml/jackson/databind/deser/impl/BeanPropertyMap$Bucket;

    array-length v0, v0

    const/4 v1, 0x0

    :goto_0
    if-ge v1, v0, :cond_2

    .line 326
    iget-object v2, p0, Lcom/fasterxml/jackson/databind/deser/impl/BeanPropertyMap;->_buckets:[Lcom/fasterxml/jackson/databind/deser/impl/BeanPropertyMap$Bucket;

    aget-object v2, v2, v1

    :goto_1
    if-eqz v2, :cond_1

    .line 327
    iget v3, v2, Lcom/fasterxml/jackson/databind/deser/impl/BeanPropertyMap$Bucket;->index:I

    if-ne v3, p1, :cond_0

    .line 328
    iget-object p1, v2, Lcom/fasterxml/jackson/databind/deser/impl/BeanPropertyMap$Bucket;->value:Lcom/fasterxml/jackson/databind/deser/SettableBeanProperty;

    return-object p1

    .line 326
    :cond_0
    iget-object v2, v2, Lcom/fasterxml/jackson/databind/deser/impl/BeanPropertyMap$Bucket;->next:Lcom/fasterxml/jackson/databind/deser/impl/BeanPropertyMap$Bucket;

    goto :goto_1

    :cond_1
    add-int/lit8 v1, v1, 0x1

    goto :goto_0

    :cond_2
    const/4 p1, 0x0

    return-object p1
.end method

.method public find(Ljava/lang/String;)Lcom/fasterxml/jackson/databind/deser/SettableBeanProperty;
    .locals 3

    if-eqz p1, :cond_4

    .line 239
    iget-boolean v0, p0, Lcom/fasterxml/jackson/databind/deser/impl/BeanPropertyMap;->_caseInsensitive:Z

    if-eqz v0, :cond_0

    .line 240
    invoke-virtual {p1}, Ljava/lang/String;->toLowerCase()Ljava/lang/String;

    move-result-object p1

    .line 242
    :cond_0
    invoke-virtual {p1}, Ljava/lang/String;->hashCode()I

    move-result v0

    iget v1, p0, Lcom/fasterxml/jackson/databind/deser/impl/BeanPropertyMap;->_hashMask:I

    and-int/2addr v0, v1

    .line 243
    iget-object v1, p0, Lcom/fasterxml/jackson/databind/deser/impl/BeanPropertyMap;->_buckets:[Lcom/fasterxml/jackson/databind/deser/impl/BeanPropertyMap$Bucket;

    aget-object v1, v1, v0

    if-nez v1, :cond_1

    const/4 p1, 0x0

    return-object p1

    .line 249
    :cond_1
    iget-object v2, v1, Lcom/fasterxml/jackson/databind/deser/impl/BeanPropertyMap$Bucket;->key:Ljava/lang/String;

    if-ne v2, p1, :cond_2

    .line 250
    iget-object p1, v1, Lcom/fasterxml/jackson/databind/deser/impl/BeanPropertyMap$Bucket;->value:Lcom/fasterxml/jackson/databind/deser/SettableBeanProperty;

    return-object p1

    .line 252
    :cond_2
    iget-object v1, v1, Lcom/fasterxml/jackson/databind/deser/impl/BeanPropertyMap$Bucket;->next:Lcom/fasterxml/jackson/databind/deser/impl/BeanPropertyMap$Bucket;

    if-eqz v1, :cond_3

    .line 253
    iget-object v2, v1, Lcom/fasterxml/jackson/databind/deser/impl/BeanPropertyMap$Bucket;->key:Ljava/lang/String;

    if-ne v2, p1, :cond_2

    .line 254
    iget-object p1, v1, Lcom/fasterxml/jackson/databind/deser/impl/BeanPropertyMap$Bucket;->value:Lcom/fasterxml/jackson/databind/deser/SettableBeanProperty;

    return-object p1

    .line 258
    :cond_3
    invoke-direct {p0, p1, v0}, Lcom/fasterxml/jackson/databind/deser/impl/BeanPropertyMap;->_findWithEquals(Ljava/lang/String;I)Lcom/fasterxml/jackson/databind/deser/SettableBeanProperty;

    move-result-object p1

    return-object p1

    .line 237
    :cond_4
    new-instance p1, Ljava/lang/IllegalArgumentException;

    const-string v0, "Can not pass null property name"

    invoke-direct {p1, v0}, Ljava/lang/IllegalArgumentException;-><init>(Ljava/lang/String;)V

    throw p1
.end method

.method public findDeserializeAndSet(Lcom/fasterxml/jackson/core/JsonParser;Lcom/fasterxml/jackson/databind/DeserializationContext;Ljava/lang/Object;Ljava/lang/String;)Z
    .locals 6
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    .line 273
    iget-boolean v0, p0, Lcom/fasterxml/jackson/databind/deser/impl/BeanPropertyMap;->_caseInsensitive:Z

    if-eqz v0, :cond_0

    .line 274
    invoke-virtual {p4}, Ljava/lang/String;->toLowerCase()Ljava/lang/String;

    move-result-object p4

    :cond_0
    move-object v4, p4

    .line 276
    invoke-virtual {v4}, Ljava/lang/String;->hashCode()I

    move-result p4

    iget v0, p0, Lcom/fasterxml/jackson/databind/deser/impl/BeanPropertyMap;->_hashMask:I

    and-int v5, p4, v0

    .line 277
    iget-object p4, p0, Lcom/fasterxml/jackson/databind/deser/impl/BeanPropertyMap;->_buckets:[Lcom/fasterxml/jackson/databind/deser/impl/BeanPropertyMap$Bucket;

    aget-object p4, p4, v5

    if-nez p4, :cond_1

    const/4 p1, 0x0

    return p1

    .line 283
    :cond_1
    iget-object v0, p4, Lcom/fasterxml/jackson/databind/deser/impl/BeanPropertyMap$Bucket;->key:Ljava/lang/String;

    if-ne v0, v4, :cond_2

    .line 285
    :try_start_0
    iget-object p4, p4, Lcom/fasterxml/jackson/databind/deser/impl/BeanPropertyMap$Bucket;->value:Lcom/fasterxml/jackson/databind/deser/SettableBeanProperty;

    invoke-virtual {p4, p1, p2, p3}, Lcom/fasterxml/jackson/databind/deser/SettableBeanProperty;->deserializeAndSet(Lcom/fasterxml/jackson/core/JsonParser;Lcom/fasterxml/jackson/databind/DeserializationContext;Ljava/lang/Object;)V
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception p1

    .line 287
    invoke-virtual {p0, p1, p3, v4, p2}, Lcom/fasterxml/jackson/databind/deser/impl/BeanPropertyMap;->wrapAndThrow(Ljava/lang/Throwable;Ljava/lang/Object;Ljava/lang/String;Lcom/fasterxml/jackson/databind/DeserializationContext;)V

    :goto_0
    const/4 p1, 0x1

    return p1

    :cond_2
    move-object v0, p0

    move-object v1, p1

    move-object v2, p2

    move-object v3, p3

    .line 291
    invoke-direct/range {v0 .. v5}, Lcom/fasterxml/jackson/databind/deser/impl/BeanPropertyMap;->_findDeserializeAndSet2(Lcom/fasterxml/jackson/core/JsonParser;Lcom/fasterxml/jackson/databind/DeserializationContext;Ljava/lang/Object;Ljava/lang/String;I)Z

    move-result p1

    return p1
.end method

.method public getPropertiesInInsertionOrder()[Lcom/fasterxml/jackson/databind/deser/SettableBeanProperty;
    .locals 7

    .line 216
    iget v0, p0, Lcom/fasterxml/jackson/databind/deser/impl/BeanPropertyMap;->_nextBucketIndex:I

    .line 217
    new-array v0, v0, [Lcom/fasterxml/jackson/databind/deser/SettableBeanProperty;

    .line 218
    iget-object v1, p0, Lcom/fasterxml/jackson/databind/deser/impl/BeanPropertyMap;->_buckets:[Lcom/fasterxml/jackson/databind/deser/impl/BeanPropertyMap$Bucket;

    array-length v2, v1

    const/4 v3, 0x0

    :goto_0
    if-ge v3, v2, :cond_1

    aget-object v4, v1, v3

    :goto_1
    if-eqz v4, :cond_0

    .line 220
    iget v5, v4, Lcom/fasterxml/jackson/databind/deser/impl/BeanPropertyMap$Bucket;->index:I

    iget-object v6, v4, Lcom/fasterxml/jackson/databind/deser/impl/BeanPropertyMap$Bucket;->value:Lcom/fasterxml/jackson/databind/deser/SettableBeanProperty;

    aput-object v6, v0, v5

    .line 219
    iget-object v4, v4, Lcom/fasterxml/jackson/databind/deser/impl/BeanPropertyMap$Bucket;->next:Lcom/fasterxml/jackson/databind/deser/impl/BeanPropertyMap$Bucket;

    goto :goto_1

    :cond_0
    add-int/lit8 v3, v3, 0x1

    goto :goto_0

    :cond_1
    return-object v0
.end method

.method public iterator()Ljava/util/Iterator;
    .locals 2
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()",
            "Ljava/util/Iterator<",
            "Lcom/fasterxml/jackson/databind/deser/SettableBeanProperty;",
            ">;"
        }
    .end annotation

    .line 203
    new-instance v0, Lcom/fasterxml/jackson/databind/deser/impl/BeanPropertyMap$IteratorImpl;

    iget-object v1, p0, Lcom/fasterxml/jackson/databind/deser/impl/BeanPropertyMap;->_buckets:[Lcom/fasterxml/jackson/databind/deser/impl/BeanPropertyMap$Bucket;

    invoke-direct {v0, v1}, Lcom/fasterxml/jackson/databind/deser/impl/BeanPropertyMap$IteratorImpl;-><init>([Lcom/fasterxml/jackson/databind/deser/impl/BeanPropertyMap$Bucket;)V

    return-object v0
.end method

.method public remove(Lcom/fasterxml/jackson/databind/deser/SettableBeanProperty;)V
    .locals 10

    .line 376
    invoke-direct {p0, p1}, Lcom/fasterxml/jackson/databind/deser/impl/BeanPropertyMap;->getPropertyName(Lcom/fasterxml/jackson/databind/deser/SettableBeanProperty;)Ljava/lang/String;

    move-result-object v0

    .line 377
    invoke-virtual {v0}, Ljava/lang/String;->hashCode()I

    move-result v1

    iget-object v2, p0, Lcom/fasterxml/jackson/databind/deser/impl/BeanPropertyMap;->_buckets:[Lcom/fasterxml/jackson/databind/deser/impl/BeanPropertyMap$Bucket;

    array-length v2, v2

    const/4 v3, 0x1

    sub-int/2addr v2, v3

    and-int/2addr v1, v2

    .line 381
    iget-object v2, p0, Lcom/fasterxml/jackson/databind/deser/impl/BeanPropertyMap;->_buckets:[Lcom/fasterxml/jackson/databind/deser/impl/BeanPropertyMap$Bucket;

    aget-object v2, v2, v1

    const/4 v4, 0x0

    const/4 v5, 0x0

    :goto_0
    if-eqz v2, :cond_1

    if-nez v5, :cond_0

    .line 383
    iget-object v6, v2, Lcom/fasterxml/jackson/databind/deser/impl/BeanPropertyMap$Bucket;->key:Ljava/lang/String;

    invoke-virtual {v6, v0}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v6

    if-eqz v6, :cond_0

    const/4 v5, 0x1

    goto :goto_1

    .line 386
    :cond_0
    new-instance v6, Lcom/fasterxml/jackson/databind/deser/impl/BeanPropertyMap$Bucket;

    iget-object v7, v2, Lcom/fasterxml/jackson/databind/deser/impl/BeanPropertyMap$Bucket;->key:Ljava/lang/String;

    iget-object v8, v2, Lcom/fasterxml/jackson/databind/deser/impl/BeanPropertyMap$Bucket;->value:Lcom/fasterxml/jackson/databind/deser/SettableBeanProperty;

    iget v9, v2, Lcom/fasterxml/jackson/databind/deser/impl/BeanPropertyMap$Bucket;->index:I

    invoke-direct {v6, v4, v7, v8, v9}, Lcom/fasterxml/jackson/databind/deser/impl/BeanPropertyMap$Bucket;-><init>(Lcom/fasterxml/jackson/databind/deser/impl/BeanPropertyMap$Bucket;Ljava/lang/String;Lcom/fasterxml/jackson/databind/deser/SettableBeanProperty;I)V

    move-object v4, v6

    .line 381
    :goto_1
    iget-object v2, v2, Lcom/fasterxml/jackson/databind/deser/impl/BeanPropertyMap$Bucket;->next:Lcom/fasterxml/jackson/databind/deser/impl/BeanPropertyMap$Bucket;

    goto :goto_0

    :cond_1
    if-eqz v5, :cond_2

    .line 392
    iget-object p1, p0, Lcom/fasterxml/jackson/databind/deser/impl/BeanPropertyMap;->_buckets:[Lcom/fasterxml/jackson/databind/deser/impl/BeanPropertyMap$Bucket;

    aput-object v4, p1, v1

    return-void

    .line 390
    :cond_2
    new-instance v0, Ljava/util/NoSuchElementException;

    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "No entry \'"

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/Object;)Ljava/lang/StringBuilder;

    const-string p1, "\' found, can\'t remove"

    invoke-virtual {v1, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p1

    invoke-direct {v0, p1}, Ljava/util/NoSuchElementException;-><init>(Ljava/lang/String;)V

    throw v0
.end method

.method public renameAll(Lcom/fasterxml/jackson/databind/util/NameTransformer;)Lcom/fasterxml/jackson/databind/deser/impl/BeanPropertyMap;
    .locals 5

    if-eqz p1, :cond_3

    .line 116
    sget-object v0, Lcom/fasterxml/jackson/databind/util/NameTransformer;->NOP:Lcom/fasterxml/jackson/databind/util/NameTransformer;

    if-ne p1, v0, :cond_0

    goto :goto_1

    .line 119
    :cond_0
    invoke-virtual {p0}, Lcom/fasterxml/jackson/databind/deser/impl/BeanPropertyMap;->iterator()Ljava/util/Iterator;

    move-result-object v0

    .line 120
    new-instance v1, Ljava/util/ArrayList;

    invoke-direct {v1}, Ljava/util/ArrayList;-><init>()V

    .line 121
    :goto_0
    invoke-interface {v0}, Ljava/util/Iterator;->hasNext()Z

    move-result v2

    if-eqz v2, :cond_2

    .line 122
    invoke-interface {v0}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Lcom/fasterxml/jackson/databind/deser/SettableBeanProperty;

    .line 123
    invoke-virtual {v2}, Lcom/fasterxml/jackson/databind/deser/SettableBeanProperty;->getName()Ljava/lang/String;

    move-result-object v3

    invoke-virtual {p1, v3}, Lcom/fasterxml/jackson/databind/util/NameTransformer;->transform(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v3

    .line 124
    invoke-virtual {v2, v3}, Lcom/fasterxml/jackson/databind/deser/SettableBeanProperty;->withSimpleName(Ljava/lang/String;)Lcom/fasterxml/jackson/databind/deser/SettableBeanProperty;

    move-result-object v2

    .line 125
    invoke-virtual {v2}, Lcom/fasterxml/jackson/databind/deser/SettableBeanProperty;->getValueDeserializer()Lcom/fasterxml/jackson/databind/JsonDeserializer;

    move-result-object v3

    if-eqz v3, :cond_1

    .line 128
    invoke-virtual {v3, p1}, Lcom/fasterxml/jackson/databind/JsonDeserializer;->unwrappingDeserializer(Lcom/fasterxml/jackson/databind/util/NameTransformer;)Lcom/fasterxml/jackson/databind/JsonDeserializer;

    move-result-object v4

    if-eq v4, v3, :cond_1

    .line 131
    invoke-virtual {v2, v4}, Lcom/fasterxml/jackson/databind/deser/SettableBeanProperty;->withValueDeserializer(Lcom/fasterxml/jackson/databind/JsonDeserializer;)Lcom/fasterxml/jackson/databind/deser/SettableBeanProperty;

    move-result-object v2

    .line 134
    :cond_1
    invoke-virtual {v1, v2}, Ljava/util/ArrayList;->add(Ljava/lang/Object;)Z

    goto :goto_0

    .line 137
    :cond_2
    new-instance p1, Lcom/fasterxml/jackson/databind/deser/impl/BeanPropertyMap;

    iget-boolean v0, p0, Lcom/fasterxml/jackson/databind/deser/impl/BeanPropertyMap;->_caseInsensitive:Z

    invoke-direct {p1, v1, v0}, Lcom/fasterxml/jackson/databind/deser/impl/BeanPropertyMap;-><init>(Ljava/util/Collection;Z)V

    return-object p1

    :cond_3
    :goto_1
    return-object p0
.end method

.method public replace(Lcom/fasterxml/jackson/databind/deser/SettableBeanProperty;)V
    .locals 9

    .line 342
    invoke-direct {p0, p1}, Lcom/fasterxml/jackson/databind/deser/impl/BeanPropertyMap;->getPropertyName(Lcom/fasterxml/jackson/databind/deser/SettableBeanProperty;)Ljava/lang/String;

    move-result-object v0

    .line 343
    invoke-virtual {v0}, Ljava/lang/String;->hashCode()I

    move-result v1

    iget-object v2, p0, Lcom/fasterxml/jackson/databind/deser/impl/BeanPropertyMap;->_buckets:[Lcom/fasterxml/jackson/databind/deser/impl/BeanPropertyMap$Bucket;

    array-length v2, v2

    add-int/lit8 v2, v2, -0x1

    and-int/2addr v1, v2

    .line 351
    iget-object v2, p0, Lcom/fasterxml/jackson/databind/deser/impl/BeanPropertyMap;->_buckets:[Lcom/fasterxml/jackson/databind/deser/impl/BeanPropertyMap$Bucket;

    aget-object v2, v2, v1

    const/4 v3, 0x0

    const/4 v4, -0x1

    :goto_0
    if-eqz v2, :cond_1

    if-gez v4, :cond_0

    .line 353
    iget-object v5, v2, Lcom/fasterxml/jackson/databind/deser/impl/BeanPropertyMap$Bucket;->key:Ljava/lang/String;

    invoke-virtual {v5, v0}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v5

    if-eqz v5, :cond_0

    .line 354
    iget v4, v2, Lcom/fasterxml/jackson/databind/deser/impl/BeanPropertyMap$Bucket;->index:I

    goto :goto_1

    .line 356
    :cond_0
    new-instance v5, Lcom/fasterxml/jackson/databind/deser/impl/BeanPropertyMap$Bucket;

    iget-object v6, v2, Lcom/fasterxml/jackson/databind/deser/impl/BeanPropertyMap$Bucket;->key:Ljava/lang/String;

    iget-object v7, v2, Lcom/fasterxml/jackson/databind/deser/impl/BeanPropertyMap$Bucket;->value:Lcom/fasterxml/jackson/databind/deser/SettableBeanProperty;

    iget v8, v2, Lcom/fasterxml/jackson/databind/deser/impl/BeanPropertyMap$Bucket;->index:I

    invoke-direct {v5, v3, v6, v7, v8}, Lcom/fasterxml/jackson/databind/deser/impl/BeanPropertyMap$Bucket;-><init>(Lcom/fasterxml/jackson/databind/deser/impl/BeanPropertyMap$Bucket;Ljava/lang/String;Lcom/fasterxml/jackson/databind/deser/SettableBeanProperty;I)V

    move-object v3, v5

    .line 351
    :goto_1
    iget-object v2, v2, Lcom/fasterxml/jackson/databind/deser/impl/BeanPropertyMap$Bucket;->next:Lcom/fasterxml/jackson/databind/deser/impl/BeanPropertyMap$Bucket;

    goto :goto_0

    :cond_1
    if-ltz v4, :cond_2

    .line 366
    iget-object v2, p0, Lcom/fasterxml/jackson/databind/deser/impl/BeanPropertyMap;->_buckets:[Lcom/fasterxml/jackson/databind/deser/impl/BeanPropertyMap$Bucket;

    new-instance v5, Lcom/fasterxml/jackson/databind/deser/impl/BeanPropertyMap$Bucket;

    invoke-direct {v5, v3, v0, p1, v4}, Lcom/fasterxml/jackson/databind/deser/impl/BeanPropertyMap$Bucket;-><init>(Lcom/fasterxml/jackson/databind/deser/impl/BeanPropertyMap$Bucket;Ljava/lang/String;Lcom/fasterxml/jackson/databind/deser/SettableBeanProperty;I)V

    aput-object v5, v2, v1

    return-void

    .line 361
    :cond_2
    new-instance v0, Ljava/util/NoSuchElementException;

    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "No entry \'"

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/Object;)Ljava/lang/StringBuilder;

    const-string p1, "\' found, can\'t replace"

    invoke-virtual {v1, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p1

    invoke-direct {v0, p1}, Ljava/util/NoSuchElementException;-><init>(Ljava/lang/String;)V

    throw v0
.end method

.method public size()I
    .locals 1

    .line 232
    iget v0, p0, Lcom/fasterxml/jackson/databind/deser/impl/BeanPropertyMap;->_size:I

    return v0
.end method

.method public toString()Ljava/lang/String;
    .locals 7

    .line 179
    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    const-string v1, "Properties=["

    .line 180
    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    .line 182
    invoke-virtual {p0}, Lcom/fasterxml/jackson/databind/deser/impl/BeanPropertyMap;->getPropertiesInInsertionOrder()[Lcom/fasterxml/jackson/databind/deser/SettableBeanProperty;

    move-result-object v1

    array-length v2, v1

    const/4 v3, 0x0

    const/4 v4, 0x0

    :goto_0
    if-ge v3, v2, :cond_2

    aget-object v5, v1, v3

    if-nez v5, :cond_0

    goto :goto_1

    :cond_0
    add-int/lit8 v6, v4, 0x1

    if-lez v4, :cond_1

    const-string v4, ", "

    .line 187
    invoke-virtual {v0, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    .line 189
    :cond_1
    invoke-virtual {v5}, Lcom/fasterxml/jackson/databind/deser/SettableBeanProperty;->getName()Ljava/lang/String;

    move-result-object v4

    invoke-virtual {v0, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const/16 v4, 0x28

    .line 190
    invoke-virtual {v0, v4}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    .line 191
    invoke-virtual {v5}, Lcom/fasterxml/jackson/databind/deser/SettableBeanProperty;->getType()Lcom/fasterxml/jackson/databind/JavaType;

    move-result-object v4

    invoke-virtual {v0, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/Object;)Ljava/lang/StringBuilder;

    const/16 v4, 0x29

    .line 192
    invoke-virtual {v0, v4}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    move v4, v6

    :goto_1
    add-int/lit8 v3, v3, 0x1

    goto :goto_0

    :cond_2
    const/16 v1, 0x5d

    .line 194
    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    .line 195
    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v0

    return-object v0
.end method

.method public withProperty(Lcom/fasterxml/jackson/databind/deser/SettableBeanProperty;)Lcom/fasterxml/jackson/databind/deser/impl/BeanPropertyMap;
    .locals 7

    .line 89
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/deser/impl/BeanPropertyMap;->_buckets:[Lcom/fasterxml/jackson/databind/deser/impl/BeanPropertyMap$Bucket;

    array-length v0, v0

    .line 90
    new-array v1, v0, [Lcom/fasterxml/jackson/databind/deser/impl/BeanPropertyMap$Bucket;

    .line 91
    iget-object v2, p0, Lcom/fasterxml/jackson/databind/deser/impl/BeanPropertyMap;->_buckets:[Lcom/fasterxml/jackson/databind/deser/impl/BeanPropertyMap$Bucket;

    const/4 v3, 0x0

    invoke-static {v2, v3, v1, v3, v0}, Ljava/lang/System;->arraycopy(Ljava/lang/Object;ILjava/lang/Object;II)V

    .line 92
    invoke-direct {p0, p1}, Lcom/fasterxml/jackson/databind/deser/impl/BeanPropertyMap;->getPropertyName(Lcom/fasterxml/jackson/databind/deser/SettableBeanProperty;)Ljava/lang/String;

    move-result-object v2

    .line 94
    invoke-virtual {p0, v2}, Lcom/fasterxml/jackson/databind/deser/impl/BeanPropertyMap;->find(Ljava/lang/String;)Lcom/fasterxml/jackson/databind/deser/SettableBeanProperty;

    move-result-object v3

    if-nez v3, :cond_0

    .line 99
    invoke-virtual {v2}, Ljava/lang/String;->hashCode()I

    move-result v0

    iget v3, p0, Lcom/fasterxml/jackson/databind/deser/impl/BeanPropertyMap;->_hashMask:I

    and-int/2addr v0, v3

    .line 100
    new-instance v3, Lcom/fasterxml/jackson/databind/deser/impl/BeanPropertyMap$Bucket;

    aget-object v4, v1, v0

    iget v5, p0, Lcom/fasterxml/jackson/databind/deser/impl/BeanPropertyMap;->_nextBucketIndex:I

    add-int/lit8 v6, v5, 0x1

    iput v6, p0, Lcom/fasterxml/jackson/databind/deser/impl/BeanPropertyMap;->_nextBucketIndex:I

    invoke-direct {v3, v4, v2, p1, v5}, Lcom/fasterxml/jackson/databind/deser/impl/BeanPropertyMap$Bucket;-><init>(Lcom/fasterxml/jackson/databind/deser/impl/BeanPropertyMap$Bucket;Ljava/lang/String;Lcom/fasterxml/jackson/databind/deser/SettableBeanProperty;I)V

    aput-object v3, v1, v0

    .line 102
    new-instance p1, Lcom/fasterxml/jackson/databind/deser/impl/BeanPropertyMap;

    iget v0, p0, Lcom/fasterxml/jackson/databind/deser/impl/BeanPropertyMap;->_size:I

    add-int/lit8 v0, v0, 0x1

    iget v2, p0, Lcom/fasterxml/jackson/databind/deser/impl/BeanPropertyMap;->_nextBucketIndex:I

    iget-boolean v3, p0, Lcom/fasterxml/jackson/databind/deser/impl/BeanPropertyMap;->_caseInsensitive:Z

    invoke-direct {p1, v1, v0, v2, v3}, Lcom/fasterxml/jackson/databind/deser/impl/BeanPropertyMap;-><init>([Lcom/fasterxml/jackson/databind/deser/impl/BeanPropertyMap$Bucket;IIZ)V

    return-object p1

    .line 105
    :cond_0
    new-instance v2, Lcom/fasterxml/jackson/databind/deser/impl/BeanPropertyMap;

    iget v3, p0, Lcom/fasterxml/jackson/databind/deser/impl/BeanPropertyMap;->_nextBucketIndex:I

    iget-boolean v4, p0, Lcom/fasterxml/jackson/databind/deser/impl/BeanPropertyMap;->_caseInsensitive:Z

    invoke-direct {v2, v1, v0, v3, v4}, Lcom/fasterxml/jackson/databind/deser/impl/BeanPropertyMap;-><init>([Lcom/fasterxml/jackson/databind/deser/impl/BeanPropertyMap$Bucket;IIZ)V

    .line 106
    invoke-virtual {v2, p1}, Lcom/fasterxml/jackson/databind/deser/impl/BeanPropertyMap;->replace(Lcom/fasterxml/jackson/databind/deser/SettableBeanProperty;)V

    return-object v2
.end method

.method protected wrapAndThrow(Ljava/lang/Throwable;Ljava/lang/Object;Ljava/lang/String;Lcom/fasterxml/jackson/databind/DeserializationContext;)V
    .locals 1
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    .line 420
    :goto_0
    instance-of v0, p1, Ljava/lang/reflect/InvocationTargetException;

    if-eqz v0, :cond_0

    invoke-virtual {p1}, Ljava/lang/Throwable;->getCause()Ljava/lang/Throwable;

    move-result-object v0

    if-eqz v0, :cond_0

    .line 421
    invoke-virtual {p1}, Ljava/lang/Throwable;->getCause()Ljava/lang/Throwable;

    move-result-object p1

    goto :goto_0

    .line 424
    :cond_0
    instance-of v0, p1, Ljava/lang/Error;

    if-nez v0, :cond_6

    if-eqz p4, :cond_2

    .line 428
    sget-object v0, Lcom/fasterxml/jackson/databind/DeserializationFeature;->WRAP_EXCEPTIONS:Lcom/fasterxml/jackson/databind/DeserializationFeature;

    invoke-virtual {p4, v0}, Lcom/fasterxml/jackson/databind/DeserializationContext;->isEnabled(Lcom/fasterxml/jackson/databind/DeserializationFeature;)Z

    move-result p4

    if-eqz p4, :cond_1

    goto :goto_1

    :cond_1
    const/4 p4, 0x0

    goto :goto_2

    :cond_2
    :goto_1
    const/4 p4, 0x1

    .line 430
    :goto_2
    instance-of v0, p1, Ljava/io/IOException;

    if-eqz v0, :cond_4

    if-eqz p4, :cond_3

    .line 431
    instance-of p4, p1, Lcom/fasterxml/jackson/core/JsonProcessingException;

    if-nez p4, :cond_5

    .line 432
    :cond_3
    check-cast p1, Ljava/io/IOException;

    throw p1

    :cond_4
    if-nez p4, :cond_5

    .line 435
    instance-of p4, p1, Ljava/lang/RuntimeException;

    if-eqz p4, :cond_5

    .line 436
    check-cast p1, Ljava/lang/RuntimeException;

    throw p1

    .line 439
    :cond_5
    invoke-static {p1, p2, p3}, Lcom/fasterxml/jackson/databind/JsonMappingException;->wrapWithPath(Ljava/lang/Throwable;Ljava/lang/Object;Ljava/lang/String;)Lcom/fasterxml/jackson/databind/JsonMappingException;

    move-result-object p1

    throw p1

    .line 425
    :cond_6
    check-cast p1, Ljava/lang/Error;

    throw p1
.end method
