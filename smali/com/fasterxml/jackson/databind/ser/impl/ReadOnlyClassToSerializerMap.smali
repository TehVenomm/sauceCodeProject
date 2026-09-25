.class public final Lcom/fasterxml/jackson/databind/ser/impl/ReadOnlyClassToSerializerMap;
.super Ljava/lang/Object;
.source "ReadOnlyClassToSerializerMap.java"


# instance fields
.field protected _cacheKey:Lcom/fasterxml/jackson/databind/ser/SerializerCache$TypeKey;

.field protected final _map:Lcom/fasterxml/jackson/databind/ser/impl/JsonSerializerMap;


# direct methods
.method private constructor <init>(Lcom/fasterxml/jackson/databind/ser/impl/JsonSerializerMap;)V
    .locals 1

    .line 29
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    const/4 v0, 0x0

    .line 27
    iput-object v0, p0, Lcom/fasterxml/jackson/databind/ser/impl/ReadOnlyClassToSerializerMap;->_cacheKey:Lcom/fasterxml/jackson/databind/ser/SerializerCache$TypeKey;

    .line 30
    iput-object p1, p0, Lcom/fasterxml/jackson/databind/ser/impl/ReadOnlyClassToSerializerMap;->_map:Lcom/fasterxml/jackson/databind/ser/impl/JsonSerializerMap;

    return-void
.end method

.method public static from(Ljava/util/HashMap;)Lcom/fasterxml/jackson/databind/ser/impl/ReadOnlyClassToSerializerMap;
    .locals 2
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/HashMap<",
            "Lcom/fasterxml/jackson/databind/ser/SerializerCache$TypeKey;",
            "Lcom/fasterxml/jackson/databind/JsonSerializer<",
            "Ljava/lang/Object;",
            ">;>;)",
            "Lcom/fasterxml/jackson/databind/ser/impl/ReadOnlyClassToSerializerMap;"
        }
    .end annotation

    .line 43
    new-instance v0, Lcom/fasterxml/jackson/databind/ser/impl/ReadOnlyClassToSerializerMap;

    new-instance v1, Lcom/fasterxml/jackson/databind/ser/impl/JsonSerializerMap;

    invoke-direct {v1, p0}, Lcom/fasterxml/jackson/databind/ser/impl/JsonSerializerMap;-><init>(Ljava/util/Map;)V

    invoke-direct {v0, v1}, Lcom/fasterxml/jackson/databind/ser/impl/ReadOnlyClassToSerializerMap;-><init>(Lcom/fasterxml/jackson/databind/ser/impl/JsonSerializerMap;)V

    return-object v0
.end method


# virtual methods
.method public instance()Lcom/fasterxml/jackson/databind/ser/impl/ReadOnlyClassToSerializerMap;
    .locals 2

    .line 34
    new-instance v0, Lcom/fasterxml/jackson/databind/ser/impl/ReadOnlyClassToSerializerMap;

    iget-object v1, p0, Lcom/fasterxml/jackson/databind/ser/impl/ReadOnlyClassToSerializerMap;->_map:Lcom/fasterxml/jackson/databind/ser/impl/JsonSerializerMap;

    invoke-direct {v0, v1}, Lcom/fasterxml/jackson/databind/ser/impl/ReadOnlyClassToSerializerMap;-><init>(Lcom/fasterxml/jackson/databind/ser/impl/JsonSerializerMap;)V

    return-object v0
.end method

.method public typedValueSerializer(Lcom/fasterxml/jackson/databind/JavaType;)Lcom/fasterxml/jackson/databind/JsonSerializer;
    .locals 2
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Lcom/fasterxml/jackson/databind/JavaType;",
            ")",
            "Lcom/fasterxml/jackson/databind/JsonSerializer<",
            "Ljava/lang/Object;",
            ">;"
        }
    .end annotation

    .line 47
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/ser/impl/ReadOnlyClassToSerializerMap;->_cacheKey:Lcom/fasterxml/jackson/databind/ser/SerializerCache$TypeKey;

    if-nez v0, :cond_0

    .line 48
    new-instance v0, Lcom/fasterxml/jackson/databind/ser/SerializerCache$TypeKey;

    const/4 v1, 0x1

    invoke-direct {v0, p1, v1}, Lcom/fasterxml/jackson/databind/ser/SerializerCache$TypeKey;-><init>(Lcom/fasterxml/jackson/databind/JavaType;Z)V

    iput-object v0, p0, Lcom/fasterxml/jackson/databind/ser/impl/ReadOnlyClassToSerializerMap;->_cacheKey:Lcom/fasterxml/jackson/databind/ser/SerializerCache$TypeKey;

    goto :goto_0

    .line 50
    :cond_0
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/ser/impl/ReadOnlyClassToSerializerMap;->_cacheKey:Lcom/fasterxml/jackson/databind/ser/SerializerCache$TypeKey;

    invoke-virtual {v0, p1}, Lcom/fasterxml/jackson/databind/ser/SerializerCache$TypeKey;->resetTyped(Lcom/fasterxml/jackson/databind/JavaType;)V

    .line 52
    :goto_0
    iget-object p1, p0, Lcom/fasterxml/jackson/databind/ser/impl/ReadOnlyClassToSerializerMap;->_map:Lcom/fasterxml/jackson/databind/ser/impl/JsonSerializerMap;

    iget-object v0, p0, Lcom/fasterxml/jackson/databind/ser/impl/ReadOnlyClassToSerializerMap;->_cacheKey:Lcom/fasterxml/jackson/databind/ser/SerializerCache$TypeKey;

    invoke-virtual {p1, v0}, Lcom/fasterxml/jackson/databind/ser/impl/JsonSerializerMap;->find(Lcom/fasterxml/jackson/databind/ser/SerializerCache$TypeKey;)Lcom/fasterxml/jackson/databind/JsonSerializer;

    move-result-object p1

    return-object p1
.end method

.method public typedValueSerializer(Ljava/lang/Class;)Lcom/fasterxml/jackson/databind/JsonSerializer;
    .locals 2
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/lang/Class<",
            "*>;)",
            "Lcom/fasterxml/jackson/databind/JsonSerializer<",
            "Ljava/lang/Object;",
            ">;"
        }
    .end annotation

    .line 56
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/ser/impl/ReadOnlyClassToSerializerMap;->_cacheKey:Lcom/fasterxml/jackson/databind/ser/SerializerCache$TypeKey;

    if-nez v0, :cond_0

    .line 57
    new-instance v0, Lcom/fasterxml/jackson/databind/ser/SerializerCache$TypeKey;

    const/4 v1, 0x1

    invoke-direct {v0, p1, v1}, Lcom/fasterxml/jackson/databind/ser/SerializerCache$TypeKey;-><init>(Ljava/lang/Class;Z)V

    iput-object v0, p0, Lcom/fasterxml/jackson/databind/ser/impl/ReadOnlyClassToSerializerMap;->_cacheKey:Lcom/fasterxml/jackson/databind/ser/SerializerCache$TypeKey;

    goto :goto_0

    .line 59
    :cond_0
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/ser/impl/ReadOnlyClassToSerializerMap;->_cacheKey:Lcom/fasterxml/jackson/databind/ser/SerializerCache$TypeKey;

    invoke-virtual {v0, p1}, Lcom/fasterxml/jackson/databind/ser/SerializerCache$TypeKey;->resetTyped(Ljava/lang/Class;)V

    .line 61
    :goto_0
    iget-object p1, p0, Lcom/fasterxml/jackson/databind/ser/impl/ReadOnlyClassToSerializerMap;->_map:Lcom/fasterxml/jackson/databind/ser/impl/JsonSerializerMap;

    iget-object v0, p0, Lcom/fasterxml/jackson/databind/ser/impl/ReadOnlyClassToSerializerMap;->_cacheKey:Lcom/fasterxml/jackson/databind/ser/SerializerCache$TypeKey;

    invoke-virtual {p1, v0}, Lcom/fasterxml/jackson/databind/ser/impl/JsonSerializerMap;->find(Lcom/fasterxml/jackson/databind/ser/SerializerCache$TypeKey;)Lcom/fasterxml/jackson/databind/JsonSerializer;

    move-result-object p1

    return-object p1
.end method

.method public untypedValueSerializer(Lcom/fasterxml/jackson/databind/JavaType;)Lcom/fasterxml/jackson/databind/JsonSerializer;
    .locals 2
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Lcom/fasterxml/jackson/databind/JavaType;",
            ")",
            "Lcom/fasterxml/jackson/databind/JsonSerializer<",
            "Ljava/lang/Object;",
            ">;"
        }
    .end annotation

    .line 65
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/ser/impl/ReadOnlyClassToSerializerMap;->_cacheKey:Lcom/fasterxml/jackson/databind/ser/SerializerCache$TypeKey;

    if-nez v0, :cond_0

    .line 66
    new-instance v0, Lcom/fasterxml/jackson/databind/ser/SerializerCache$TypeKey;

    const/4 v1, 0x0

    invoke-direct {v0, p1, v1}, Lcom/fasterxml/jackson/databind/ser/SerializerCache$TypeKey;-><init>(Lcom/fasterxml/jackson/databind/JavaType;Z)V

    iput-object v0, p0, Lcom/fasterxml/jackson/databind/ser/impl/ReadOnlyClassToSerializerMap;->_cacheKey:Lcom/fasterxml/jackson/databind/ser/SerializerCache$TypeKey;

    goto :goto_0

    .line 68
    :cond_0
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/ser/impl/ReadOnlyClassToSerializerMap;->_cacheKey:Lcom/fasterxml/jackson/databind/ser/SerializerCache$TypeKey;

    invoke-virtual {v0, p1}, Lcom/fasterxml/jackson/databind/ser/SerializerCache$TypeKey;->resetUntyped(Lcom/fasterxml/jackson/databind/JavaType;)V

    .line 70
    :goto_0
    iget-object p1, p0, Lcom/fasterxml/jackson/databind/ser/impl/ReadOnlyClassToSerializerMap;->_map:Lcom/fasterxml/jackson/databind/ser/impl/JsonSerializerMap;

    iget-object v0, p0, Lcom/fasterxml/jackson/databind/ser/impl/ReadOnlyClassToSerializerMap;->_cacheKey:Lcom/fasterxml/jackson/databind/ser/SerializerCache$TypeKey;

    invoke-virtual {p1, v0}, Lcom/fasterxml/jackson/databind/ser/impl/JsonSerializerMap;->find(Lcom/fasterxml/jackson/databind/ser/SerializerCache$TypeKey;)Lcom/fasterxml/jackson/databind/JsonSerializer;

    move-result-object p1

    return-object p1
.end method

.method public untypedValueSerializer(Ljava/lang/Class;)Lcom/fasterxml/jackson/databind/JsonSerializer;
    .locals 2
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/lang/Class<",
            "*>;)",
            "Lcom/fasterxml/jackson/databind/JsonSerializer<",
            "Ljava/lang/Object;",
            ">;"
        }
    .end annotation

    .line 74
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/ser/impl/ReadOnlyClassToSerializerMap;->_cacheKey:Lcom/fasterxml/jackson/databind/ser/SerializerCache$TypeKey;

    if-nez v0, :cond_0

    .line 75
    new-instance v0, Lcom/fasterxml/jackson/databind/ser/SerializerCache$TypeKey;

    const/4 v1, 0x0

    invoke-direct {v0, p1, v1}, Lcom/fasterxml/jackson/databind/ser/SerializerCache$TypeKey;-><init>(Ljava/lang/Class;Z)V

    iput-object v0, p0, Lcom/fasterxml/jackson/databind/ser/impl/ReadOnlyClassToSerializerMap;->_cacheKey:Lcom/fasterxml/jackson/databind/ser/SerializerCache$TypeKey;

    goto :goto_0

    .line 77
    :cond_0
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/ser/impl/ReadOnlyClassToSerializerMap;->_cacheKey:Lcom/fasterxml/jackson/databind/ser/SerializerCache$TypeKey;

    invoke-virtual {v0, p1}, Lcom/fasterxml/jackson/databind/ser/SerializerCache$TypeKey;->resetUntyped(Ljava/lang/Class;)V

    .line 79
    :goto_0
    iget-object p1, p0, Lcom/fasterxml/jackson/databind/ser/impl/ReadOnlyClassToSerializerMap;->_map:Lcom/fasterxml/jackson/databind/ser/impl/JsonSerializerMap;

    iget-object v0, p0, Lcom/fasterxml/jackson/databind/ser/impl/ReadOnlyClassToSerializerMap;->_cacheKey:Lcom/fasterxml/jackson/databind/ser/SerializerCache$TypeKey;

    invoke-virtual {p1, v0}, Lcom/fasterxml/jackson/databind/ser/impl/JsonSerializerMap;->find(Lcom/fasterxml/jackson/databind/ser/SerializerCache$TypeKey;)Lcom/fasterxml/jackson/databind/JsonSerializer;

    move-result-object p1

    return-object p1
.end method
