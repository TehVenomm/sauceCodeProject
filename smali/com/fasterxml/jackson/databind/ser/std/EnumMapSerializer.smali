.class public Lcom/fasterxml/jackson/databind/ser/std/EnumMapSerializer;
.super Lcom/fasterxml/jackson/databind/ser/ContainerSerializer;
.source "EnumMapSerializer.java"

# interfaces
.implements Lcom/fasterxml/jackson/databind/ser/ContextualSerializer;


# annotations
.annotation runtime Lcom/fasterxml/jackson/databind/annotation/JacksonStdImpl;
.end annotation

.annotation system Ldalvik/annotation/Signature;
    value = {
        "Lcom/fasterxml/jackson/databind/ser/ContainerSerializer<",
        "Ljava/util/EnumMap<",
        "+",
        "Ljava/lang/Enum<",
        "*>;*>;>;",
        "Lcom/fasterxml/jackson/databind/ser/ContextualSerializer;"
    }
.end annotation

.annotation runtime Ljava/lang/Deprecated;
.end annotation


# instance fields
.field protected final _keyEnums:Lcom/fasterxml/jackson/databind/util/EnumValues;

.field protected final _property:Lcom/fasterxml/jackson/databind/BeanProperty;

.field protected final _staticTyping:Z

.field protected final _valueSerializer:Lcom/fasterxml/jackson/databind/JsonSerializer;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Lcom/fasterxml/jackson/databind/JsonSerializer<",
            "Ljava/lang/Object;",
            ">;"
        }
    .end annotation
.end field

.field protected final _valueType:Lcom/fasterxml/jackson/databind/JavaType;

.field protected final _valueTypeSerializer:Lcom/fasterxml/jackson/databind/jsontype/TypeSerializer;


# direct methods
.method public constructor <init>(Lcom/fasterxml/jackson/databind/JavaType;ZLcom/fasterxml/jackson/databind/util/EnumValues;Lcom/fasterxml/jackson/databind/jsontype/TypeSerializer;Lcom/fasterxml/jackson/databind/JsonSerializer;)V
    .locals 2
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Lcom/fasterxml/jackson/databind/JavaType;",
            "Z",
            "Lcom/fasterxml/jackson/databind/util/EnumValues;",
            "Lcom/fasterxml/jackson/databind/jsontype/TypeSerializer;",
            "Lcom/fasterxml/jackson/databind/JsonSerializer<",
            "Ljava/lang/Object;",
            ">;)V"
        }
    .end annotation

    .line 73
    const-class v0, Ljava/util/EnumMap;

    const/4 v1, 0x0

    invoke-direct {p0, v0, v1}, Lcom/fasterxml/jackson/databind/ser/ContainerSerializer;-><init>(Ljava/lang/Class;Z)V

    const/4 v0, 0x0

    .line 74
    iput-object v0, p0, Lcom/fasterxml/jackson/databind/ser/std/EnumMapSerializer;->_property:Lcom/fasterxml/jackson/databind/BeanProperty;

    if-nez p2, :cond_0

    if-eqz p1, :cond_1

    .line 75
    invoke-virtual {p1}, Lcom/fasterxml/jackson/databind/JavaType;->isFinal()Z

    move-result p2

    if-eqz p2, :cond_1

    :cond_0
    const/4 v1, 0x1

    :cond_1
    iput-boolean v1, p0, Lcom/fasterxml/jackson/databind/ser/std/EnumMapSerializer;->_staticTyping:Z

    .line 76
    iput-object p1, p0, Lcom/fasterxml/jackson/databind/ser/std/EnumMapSerializer;->_valueType:Lcom/fasterxml/jackson/databind/JavaType;

    .line 77
    iput-object p3, p0, Lcom/fasterxml/jackson/databind/ser/std/EnumMapSerializer;->_keyEnums:Lcom/fasterxml/jackson/databind/util/EnumValues;

    .line 78
    iput-object p4, p0, Lcom/fasterxml/jackson/databind/ser/std/EnumMapSerializer;->_valueTypeSerializer:Lcom/fasterxml/jackson/databind/jsontype/TypeSerializer;

    .line 79
    iput-object p5, p0, Lcom/fasterxml/jackson/databind/ser/std/EnumMapSerializer;->_valueSerializer:Lcom/fasterxml/jackson/databind/JsonSerializer;

    return-void
.end method

.method public constructor <init>(Lcom/fasterxml/jackson/databind/ser/std/EnumMapSerializer;Lcom/fasterxml/jackson/databind/BeanProperty;Lcom/fasterxml/jackson/databind/JsonSerializer;)V
    .locals 0
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Lcom/fasterxml/jackson/databind/ser/std/EnumMapSerializer;",
            "Lcom/fasterxml/jackson/databind/BeanProperty;",
            "Lcom/fasterxml/jackson/databind/JsonSerializer<",
            "*>;)V"
        }
    .end annotation

    .line 89
    invoke-direct {p0, p1}, Lcom/fasterxml/jackson/databind/ser/ContainerSerializer;-><init>(Lcom/fasterxml/jackson/databind/ser/ContainerSerializer;)V

    .line 90
    iput-object p2, p0, Lcom/fasterxml/jackson/databind/ser/std/EnumMapSerializer;->_property:Lcom/fasterxml/jackson/databind/BeanProperty;

    .line 91
    iget-boolean p2, p1, Lcom/fasterxml/jackson/databind/ser/std/EnumMapSerializer;->_staticTyping:Z

    iput-boolean p2, p0, Lcom/fasterxml/jackson/databind/ser/std/EnumMapSerializer;->_staticTyping:Z

    .line 92
    iget-object p2, p1, Lcom/fasterxml/jackson/databind/ser/std/EnumMapSerializer;->_valueType:Lcom/fasterxml/jackson/databind/JavaType;

    iput-object p2, p0, Lcom/fasterxml/jackson/databind/ser/std/EnumMapSerializer;->_valueType:Lcom/fasterxml/jackson/databind/JavaType;

    .line 93
    iget-object p2, p1, Lcom/fasterxml/jackson/databind/ser/std/EnumMapSerializer;->_keyEnums:Lcom/fasterxml/jackson/databind/util/EnumValues;

    iput-object p2, p0, Lcom/fasterxml/jackson/databind/ser/std/EnumMapSerializer;->_keyEnums:Lcom/fasterxml/jackson/databind/util/EnumValues;

    .line 94
    iget-object p1, p1, Lcom/fasterxml/jackson/databind/ser/std/EnumMapSerializer;->_valueTypeSerializer:Lcom/fasterxml/jackson/databind/jsontype/TypeSerializer;

    iput-object p1, p0, Lcom/fasterxml/jackson/databind/ser/std/EnumMapSerializer;->_valueTypeSerializer:Lcom/fasterxml/jackson/databind/jsontype/TypeSerializer;

    .line 95
    iput-object p3, p0, Lcom/fasterxml/jackson/databind/ser/std/EnumMapSerializer;->_valueSerializer:Lcom/fasterxml/jackson/databind/JsonSerializer;

    return-void
.end method


# virtual methods
.method public bridge synthetic _withValueTypeSerializer(Lcom/fasterxml/jackson/databind/jsontype/TypeSerializer;)Lcom/fasterxml/jackson/databind/ser/ContainerSerializer;
    .locals 0

    .line 31
    invoke-virtual {p0, p1}, Lcom/fasterxml/jackson/databind/ser/std/EnumMapSerializer;->_withValueTypeSerializer(Lcom/fasterxml/jackson/databind/jsontype/TypeSerializer;)Lcom/fasterxml/jackson/databind/ser/std/EnumMapSerializer;

    move-result-object p1

    return-object p1
.end method

.method public _withValueTypeSerializer(Lcom/fasterxml/jackson/databind/jsontype/TypeSerializer;)Lcom/fasterxml/jackson/databind/ser/std/EnumMapSerializer;
    .locals 7

    .line 100
    new-instance v6, Lcom/fasterxml/jackson/databind/ser/std/EnumMapSerializer;

    iget-object v1, p0, Lcom/fasterxml/jackson/databind/ser/std/EnumMapSerializer;->_valueType:Lcom/fasterxml/jackson/databind/JavaType;

    iget-boolean v2, p0, Lcom/fasterxml/jackson/databind/ser/std/EnumMapSerializer;->_staticTyping:Z

    iget-object v3, p0, Lcom/fasterxml/jackson/databind/ser/std/EnumMapSerializer;->_keyEnums:Lcom/fasterxml/jackson/databind/util/EnumValues;

    iget-object v5, p0, Lcom/fasterxml/jackson/databind/ser/std/EnumMapSerializer;->_valueSerializer:Lcom/fasterxml/jackson/databind/JsonSerializer;

    move-object v0, v6

    move-object v4, p1

    invoke-direct/range {v0 .. v5}, Lcom/fasterxml/jackson/databind/ser/std/EnumMapSerializer;-><init>(Lcom/fasterxml/jackson/databind/JavaType;ZLcom/fasterxml/jackson/databind/util/EnumValues;Lcom/fasterxml/jackson/databind/jsontype/TypeSerializer;Lcom/fasterxml/jackson/databind/JsonSerializer;)V

    return-object v6
.end method

.method public acceptJsonFormatVisitor(Lcom/fasterxml/jackson/databind/jsonFormatVisitors/JsonFormatVisitorWrapper;Lcom/fasterxml/jackson/databind/JavaType;)V
    .locals 6
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Lcom/fasterxml/jackson/databind/JsonMappingException;
        }
    .end annotation

    if-nez p1, :cond_0

    return-void

    .line 335
    :cond_0
    invoke-interface {p1, p2}, Lcom/fasterxml/jackson/databind/jsonFormatVisitors/JsonFormatVisitorWrapper;->expectObjectFormat(Lcom/fasterxml/jackson/databind/JavaType;)Lcom/fasterxml/jackson/databind/jsonFormatVisitors/JsonObjectFormatVisitor;

    move-result-object v0

    if-nez v0, :cond_1

    return-void

    :cond_1
    const/4 v1, 0x1

    .line 339
    invoke-virtual {p2, v1}, Lcom/fasterxml/jackson/databind/JavaType;->containedType(I)Lcom/fasterxml/jackson/databind/JavaType;

    move-result-object v1

    .line 340
    iget-object v2, p0, Lcom/fasterxml/jackson/databind/ser/std/EnumMapSerializer;->_valueSerializer:Lcom/fasterxml/jackson/databind/JsonSerializer;

    if-nez v2, :cond_2

    if-eqz v1, :cond_2

    .line 342
    invoke-interface {p1}, Lcom/fasterxml/jackson/databind/jsonFormatVisitors/JsonFormatVisitorWrapper;->getProvider()Lcom/fasterxml/jackson/databind/SerializerProvider;

    move-result-object v2

    iget-object v3, p0, Lcom/fasterxml/jackson/databind/ser/std/EnumMapSerializer;->_property:Lcom/fasterxml/jackson/databind/BeanProperty;

    invoke-virtual {v2, v1, v3}, Lcom/fasterxml/jackson/databind/SerializerProvider;->findValueSerializer(Lcom/fasterxml/jackson/databind/JavaType;Lcom/fasterxml/jackson/databind/BeanProperty;)Lcom/fasterxml/jackson/databind/JsonSerializer;

    move-result-object v2

    :cond_2
    if-nez v1, :cond_3

    .line 345
    invoke-interface {p1}, Lcom/fasterxml/jackson/databind/jsonFormatVisitors/JsonFormatVisitorWrapper;->getProvider()Lcom/fasterxml/jackson/databind/SerializerProvider;

    move-result-object v1

    const-class v3, Ljava/lang/Object;

    invoke-virtual {v1, v3}, Lcom/fasterxml/jackson/databind/SerializerProvider;->constructType(Ljava/lang/reflect/Type;)Lcom/fasterxml/jackson/databind/JavaType;

    move-result-object v1

    .line 347
    :cond_3
    iget-object v3, p0, Lcom/fasterxml/jackson/databind/ser/std/EnumMapSerializer;->_keyEnums:Lcom/fasterxml/jackson/databind/util/EnumValues;

    if-nez v3, :cond_6

    const/4 v3, 0x0

    .line 349
    invoke-virtual {p2, v3}, Lcom/fasterxml/jackson/databind/JavaType;->containedType(I)Lcom/fasterxml/jackson/databind/JavaType;

    move-result-object v3

    if-eqz v3, :cond_5

    .line 353
    invoke-interface {p1}, Lcom/fasterxml/jackson/databind/jsonFormatVisitors/JsonFormatVisitorWrapper;->getProvider()Lcom/fasterxml/jackson/databind/SerializerProvider;

    move-result-object v4

    iget-object v5, p0, Lcom/fasterxml/jackson/databind/ser/std/EnumMapSerializer;->_property:Lcom/fasterxml/jackson/databind/BeanProperty;

    invoke-virtual {v4, v3, v5}, Lcom/fasterxml/jackson/databind/SerializerProvider;->findValueSerializer(Lcom/fasterxml/jackson/databind/JavaType;Lcom/fasterxml/jackson/databind/BeanProperty;)Lcom/fasterxml/jackson/databind/JsonSerializer;

    move-result-object v3

    .line 354
    instance-of v4, v3, Lcom/fasterxml/jackson/databind/ser/std/EnumSerializer;

    if-eqz v4, :cond_4

    .line 357
    check-cast v3, Lcom/fasterxml/jackson/databind/ser/std/EnumSerializer;

    invoke-virtual {v3}, Lcom/fasterxml/jackson/databind/ser/std/EnumSerializer;->getEnumValues()Lcom/fasterxml/jackson/databind/util/EnumValues;

    move-result-object v3

    goto :goto_0

    .line 355
    :cond_4
    new-instance p1, Ljava/lang/IllegalStateException;

    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    const-string v1, "Can not resolve Enum type of EnumMap: "

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0, p2}, Ljava/lang/StringBuilder;->append(Ljava/lang/Object;)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p2

    invoke-direct {p1, p2}, Ljava/lang/IllegalStateException;-><init>(Ljava/lang/String;)V

    throw p1

    .line 351
    :cond_5
    new-instance p1, Ljava/lang/IllegalStateException;

    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    const-string v1, "Can not resolve Enum type of EnumMap: "

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0, p2}, Ljava/lang/StringBuilder;->append(Ljava/lang/Object;)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p2

    invoke-direct {p1, p2}, Ljava/lang/IllegalStateException;-><init>(Ljava/lang/String;)V

    throw p1

    .line 359
    :cond_6
    :goto_0
    invoke-virtual {v3}, Lcom/fasterxml/jackson/databind/util/EnumValues;->internalMap()Ljava/util/EnumMap;

    move-result-object p2

    invoke-virtual {p2}, Ljava/util/EnumMap;->entrySet()Ljava/util/Set;

    move-result-object p2

    invoke-interface {p2}, Ljava/util/Set;->iterator()Ljava/util/Iterator;

    move-result-object p2

    :goto_1
    invoke-interface {p2}, Ljava/util/Iterator;->hasNext()Z

    move-result v3

    if-eqz v3, :cond_8

    invoke-interface {p2}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v3

    check-cast v3, Ljava/util/Map$Entry;

    .line 360
    invoke-interface {v3}, Ljava/util/Map$Entry;->getValue()Ljava/lang/Object;

    move-result-object v4

    check-cast v4, Lcom/fasterxml/jackson/core/SerializableString;

    invoke-interface {v4}, Lcom/fasterxml/jackson/core/SerializableString;->getValue()Ljava/lang/String;

    move-result-object v4

    if-nez v2, :cond_7

    .line 363
    invoke-interface {p1}, Lcom/fasterxml/jackson/databind/jsonFormatVisitors/JsonFormatVisitorWrapper;->getProvider()Lcom/fasterxml/jackson/databind/SerializerProvider;

    move-result-object v2

    invoke-interface {v3}, Ljava/util/Map$Entry;->getKey()Ljava/lang/Object;

    move-result-object v3

    invoke-virtual {v3}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    move-result-object v3

    iget-object v5, p0, Lcom/fasterxml/jackson/databind/ser/std/EnumMapSerializer;->_property:Lcom/fasterxml/jackson/databind/BeanProperty;

    invoke-virtual {v2, v3, v5}, Lcom/fasterxml/jackson/databind/SerializerProvider;->findValueSerializer(Ljava/lang/Class;Lcom/fasterxml/jackson/databind/BeanProperty;)Lcom/fasterxml/jackson/databind/JsonSerializer;

    move-result-object v2

    .line 365
    :cond_7
    invoke-interface {v0, v4, v2, v1}, Lcom/fasterxml/jackson/databind/jsonFormatVisitors/JsonObjectFormatVisitor;->property(Ljava/lang/String;Lcom/fasterxml/jackson/databind/jsonFormatVisitors/JsonFormatVisitable;Lcom/fasterxml/jackson/databind/JavaType;)V

    goto :goto_1

    :cond_8
    return-void
.end method

.method public createContextual(Lcom/fasterxml/jackson/databind/SerializerProvider;Lcom/fasterxml/jackson/databind/BeanProperty;)Lcom/fasterxml/jackson/databind/JsonSerializer;
    .locals 2
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Lcom/fasterxml/jackson/databind/SerializerProvider;",
            "Lcom/fasterxml/jackson/databind/BeanProperty;",
            ")",
            "Lcom/fasterxml/jackson/databind/JsonSerializer<",
            "*>;"
        }
    .end annotation

    .annotation system Ldalvik/annotation/Throws;
        value = {
            Lcom/fasterxml/jackson/databind/JsonMappingException;
        }
    .end annotation

    if-eqz p2, :cond_0

    .line 122
    invoke-interface {p2}, Lcom/fasterxml/jackson/databind/BeanProperty;->getMember()Lcom/fasterxml/jackson/databind/introspect/AnnotatedMember;

    move-result-object v0

    if-eqz v0, :cond_0

    .line 124
    invoke-virtual {p1}, Lcom/fasterxml/jackson/databind/SerializerProvider;->getAnnotationIntrospector()Lcom/fasterxml/jackson/databind/AnnotationIntrospector;

    move-result-object v1

    invoke-virtual {v1, v0}, Lcom/fasterxml/jackson/databind/AnnotationIntrospector;->findContentSerializer(Lcom/fasterxml/jackson/databind/introspect/Annotated;)Ljava/lang/Object;

    move-result-object v1

    if-eqz v1, :cond_0

    .line 126
    invoke-virtual {p1, v0, v1}, Lcom/fasterxml/jackson/databind/SerializerProvider;->serializerInstance(Lcom/fasterxml/jackson/databind/introspect/Annotated;Ljava/lang/Object;)Lcom/fasterxml/jackson/databind/JsonSerializer;

    move-result-object v0

    goto :goto_0

    :cond_0
    const/4 v0, 0x0

    :goto_0
    if-nez v0, :cond_1

    .line 131
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/ser/std/EnumMapSerializer;->_valueSerializer:Lcom/fasterxml/jackson/databind/JsonSerializer;

    .line 134
    :cond_1
    invoke-virtual {p0, p1, p2, v0}, Lcom/fasterxml/jackson/databind/ser/std/EnumMapSerializer;->findConvertingContentSerializer(Lcom/fasterxml/jackson/databind/SerializerProvider;Lcom/fasterxml/jackson/databind/BeanProperty;Lcom/fasterxml/jackson/databind/JsonSerializer;)Lcom/fasterxml/jackson/databind/JsonSerializer;

    move-result-object v0

    if-nez v0, :cond_2

    .line 136
    iget-boolean v1, p0, Lcom/fasterxml/jackson/databind/ser/std/EnumMapSerializer;->_staticTyping:Z

    if-eqz v1, :cond_3

    .line 137
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/ser/std/EnumMapSerializer;->_valueType:Lcom/fasterxml/jackson/databind/JavaType;

    invoke-virtual {p1, v0, p2}, Lcom/fasterxml/jackson/databind/SerializerProvider;->findValueSerializer(Lcom/fasterxml/jackson/databind/JavaType;Lcom/fasterxml/jackson/databind/BeanProperty;)Lcom/fasterxml/jackson/databind/JsonSerializer;

    move-result-object p1

    invoke-virtual {p0, p2, p1}, Lcom/fasterxml/jackson/databind/ser/std/EnumMapSerializer;->withValueSerializer(Lcom/fasterxml/jackson/databind/BeanProperty;Lcom/fasterxml/jackson/databind/JsonSerializer;)Lcom/fasterxml/jackson/databind/ser/std/EnumMapSerializer;

    move-result-object p1

    return-object p1

    .line 140
    :cond_2
    invoke-virtual {p1, v0, p2}, Lcom/fasterxml/jackson/databind/SerializerProvider;->handleSecondaryContextualization(Lcom/fasterxml/jackson/databind/JsonSerializer;Lcom/fasterxml/jackson/databind/BeanProperty;)Lcom/fasterxml/jackson/databind/JsonSerializer;

    move-result-object v0

    .line 142
    :cond_3
    iget-object p1, p0, Lcom/fasterxml/jackson/databind/ser/std/EnumMapSerializer;->_valueSerializer:Lcom/fasterxml/jackson/databind/JsonSerializer;

    if-eq v0, p1, :cond_4

    .line 143
    invoke-virtual {p0, p2, v0}, Lcom/fasterxml/jackson/databind/ser/std/EnumMapSerializer;->withValueSerializer(Lcom/fasterxml/jackson/databind/BeanProperty;Lcom/fasterxml/jackson/databind/JsonSerializer;)Lcom/fasterxml/jackson/databind/ser/std/EnumMapSerializer;

    move-result-object p1

    return-object p1

    :cond_4
    return-object p0
.end method

.method public getContentSerializer()Lcom/fasterxml/jackson/databind/JsonSerializer;
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()",
            "Lcom/fasterxml/jackson/databind/JsonSerializer<",
            "*>;"
        }
    .end annotation

    .line 161
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/ser/std/EnumMapSerializer;->_valueSerializer:Lcom/fasterxml/jackson/databind/JsonSerializer;

    return-object v0
.end method

.method public getContentType()Lcom/fasterxml/jackson/databind/JavaType;
    .locals 1

    .line 156
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/ser/std/EnumMapSerializer;->_valueType:Lcom/fasterxml/jackson/databind/JavaType;

    return-object v0
.end method

.method public getSchema(Lcom/fasterxml/jackson/databind/SerializerProvider;Ljava/lang/reflect/Type;)Lcom/fasterxml/jackson/databind/JsonNode;
    .locals 8
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Lcom/fasterxml/jackson/databind/JsonMappingException;
        }
    .end annotation

    const-string v0, "object"

    const/4 v1, 0x1

    .line 302
    invoke-virtual {p0, v0, v1}, Lcom/fasterxml/jackson/databind/ser/std/EnumMapSerializer;->createSchemaNode(Ljava/lang/String;Z)Lcom/fasterxml/jackson/databind/node/ObjectNode;

    move-result-object v0

    .line 303
    instance-of v2, p2, Ljava/lang/reflect/ParameterizedType;

    if-eqz v2, :cond_2

    .line 304
    check-cast p2, Ljava/lang/reflect/ParameterizedType;

    invoke-interface {p2}, Ljava/lang/reflect/ParameterizedType;->getActualTypeArguments()[Ljava/lang/reflect/Type;

    move-result-object p2

    .line 305
    array-length v2, p2

    const/4 v3, 0x2

    if-ne v2, v3, :cond_2

    const/4 v2, 0x0

    .line 306
    aget-object v3, p2, v2

    invoke-virtual {p1, v3}, Lcom/fasterxml/jackson/databind/SerializerProvider;->constructType(Ljava/lang/reflect/Type;)Lcom/fasterxml/jackson/databind/JavaType;

    move-result-object v3

    .line 307
    aget-object p2, p2, v1

    invoke-virtual {p1, p2}, Lcom/fasterxml/jackson/databind/SerializerProvider;->constructType(Ljava/lang/reflect/Type;)Lcom/fasterxml/jackson/databind/JavaType;

    move-result-object p2

    .line 308
    sget-object v1, Lcom/fasterxml/jackson/databind/node/JsonNodeFactory;->instance:Lcom/fasterxml/jackson/databind/node/JsonNodeFactory;

    invoke-virtual {v1}, Lcom/fasterxml/jackson/databind/node/JsonNodeFactory;->objectNode()Lcom/fasterxml/jackson/databind/node/ObjectNode;

    move-result-object v1

    .line 309
    invoke-virtual {v3}, Lcom/fasterxml/jackson/databind/JavaType;->getRawClass()Ljava/lang/Class;

    move-result-object v3

    .line 310
    invoke-virtual {v3}, Ljava/lang/Class;->getEnumConstants()[Ljava/lang/Object;

    move-result-object v3

    check-cast v3, [Ljava/lang/Enum;

    array-length v4, v3

    :goto_0
    if-ge v2, v4, :cond_1

    aget-object v5, v3, v2

    .line 311
    invoke-virtual {p2}, Lcom/fasterxml/jackson/databind/JavaType;->getRawClass()Ljava/lang/Class;

    move-result-object v6

    iget-object v7, p0, Lcom/fasterxml/jackson/databind/ser/std/EnumMapSerializer;->_property:Lcom/fasterxml/jackson/databind/BeanProperty;

    invoke-virtual {p1, v6, v7}, Lcom/fasterxml/jackson/databind/SerializerProvider;->findValueSerializer(Ljava/lang/Class;Lcom/fasterxml/jackson/databind/BeanProperty;)Lcom/fasterxml/jackson/databind/JsonSerializer;

    move-result-object v6

    .line 312
    instance-of v7, v6, Lcom/fasterxml/jackson/databind/jsonschema/SchemaAware;

    if-eqz v7, :cond_0

    check-cast v6, Lcom/fasterxml/jackson/databind/jsonschema/SchemaAware;

    const/4 v7, 0x0

    invoke-interface {v6, p1, v7}, Lcom/fasterxml/jackson/databind/jsonschema/SchemaAware;->getSchema(Lcom/fasterxml/jackson/databind/SerializerProvider;Ljava/lang/reflect/Type;)Lcom/fasterxml/jackson/databind/JsonNode;

    move-result-object v6

    goto :goto_1

    :cond_0
    invoke-static {}, Lcom/fasterxml/jackson/databind/jsonschema/JsonSchema;->getDefaultSchemaNode()Lcom/fasterxml/jackson/databind/JsonNode;

    move-result-object v6

    .line 315
    :goto_1
    invoke-virtual {p1}, Lcom/fasterxml/jackson/databind/SerializerProvider;->getConfig()Lcom/fasterxml/jackson/databind/SerializationConfig;

    move-result-object v7

    invoke-virtual {v7}, Lcom/fasterxml/jackson/databind/SerializationConfig;->getAnnotationIntrospector()Lcom/fasterxml/jackson/databind/AnnotationIntrospector;

    move-result-object v7

    invoke-virtual {v7, v5}, Lcom/fasterxml/jackson/databind/AnnotationIntrospector;->findEnumValue(Ljava/lang/Enum;)Ljava/lang/String;

    move-result-object v5

    invoke-virtual {v1, v5, v6}, Lcom/fasterxml/jackson/databind/node/ObjectNode;->put(Ljava/lang/String;Lcom/fasterxml/jackson/databind/JsonNode;)Lcom/fasterxml/jackson/databind/JsonNode;

    add-int/lit8 v2, v2, 0x1

    goto :goto_0

    :cond_1
    const-string p1, "properties"

    .line 317
    invoke-virtual {v0, p1, v1}, Lcom/fasterxml/jackson/databind/node/ObjectNode;->put(Ljava/lang/String;Lcom/fasterxml/jackson/databind/JsonNode;)Lcom/fasterxml/jackson/databind/JsonNode;

    :cond_2
    return-object v0
.end method

.method public bridge synthetic hasSingleElement(Ljava/lang/Object;)Z
    .locals 0

    .line 31
    check-cast p1, Ljava/util/EnumMap;

    invoke-virtual {p0, p1}, Lcom/fasterxml/jackson/databind/ser/std/EnumMapSerializer;->hasSingleElement(Ljava/util/EnumMap;)Z

    move-result p1

    return p1
.end method

.method public hasSingleElement(Ljava/util/EnumMap;)Z
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/EnumMap<",
            "+",
            "Ljava/lang/Enum<",
            "*>;*>;)Z"
        }
    .end annotation

    .line 171
    invoke-virtual {p1}, Ljava/util/EnumMap;->size()I

    move-result p1

    const/4 v0, 0x1

    if-ne p1, v0, :cond_0

    goto :goto_0

    :cond_0
    const/4 v0, 0x0

    :goto_0
    return v0
.end method

.method public bridge synthetic isEmpty(Lcom/fasterxml/jackson/databind/SerializerProvider;Ljava/lang/Object;)Z
    .locals 0

    .line 31
    check-cast p2, Ljava/util/EnumMap;

    invoke-virtual {p0, p1, p2}, Lcom/fasterxml/jackson/databind/ser/std/EnumMapSerializer;->isEmpty(Lcom/fasterxml/jackson/databind/SerializerProvider;Ljava/util/EnumMap;)Z

    move-result p1

    return p1
.end method

.method public isEmpty(Lcom/fasterxml/jackson/databind/SerializerProvider;Ljava/util/EnumMap;)Z
    .locals 0
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Lcom/fasterxml/jackson/databind/SerializerProvider;",
            "Ljava/util/EnumMap<",
            "+",
            "Ljava/lang/Enum<",
            "*>;*>;)Z"
        }
    .end annotation

    if-eqz p2, :cond_1

    .line 166
    invoke-virtual {p2}, Ljava/util/EnumMap;->isEmpty()Z

    move-result p1

    if-eqz p1, :cond_0

    goto :goto_0

    :cond_0
    const/4 p1, 0x0

    goto :goto_1

    :cond_1
    :goto_0
    const/4 p1, 0x1

    :goto_1
    return p1
.end method

.method public bridge synthetic serialize(Ljava/lang/Object;Lcom/fasterxml/jackson/core/JsonGenerator;Lcom/fasterxml/jackson/databind/SerializerProvider;)V
    .locals 0
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;,
            Lcom/fasterxml/jackson/core/JsonGenerationException;
        }
    .end annotation

    .line 31
    check-cast p1, Ljava/util/EnumMap;

    invoke-virtual {p0, p1, p2, p3}, Lcom/fasterxml/jackson/databind/ser/std/EnumMapSerializer;->serialize(Ljava/util/EnumMap;Lcom/fasterxml/jackson/core/JsonGenerator;Lcom/fasterxml/jackson/databind/SerializerProvider;)V

    return-void
.end method

.method public serialize(Ljava/util/EnumMap;Lcom/fasterxml/jackson/core/JsonGenerator;Lcom/fasterxml/jackson/databind/SerializerProvider;)V
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/EnumMap<",
            "+",
            "Ljava/lang/Enum<",
            "*>;*>;",
            "Lcom/fasterxml/jackson/core/JsonGenerator;",
            "Lcom/fasterxml/jackson/databind/SerializerProvider;",
            ")V"
        }
    .end annotation

    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;,
            Lcom/fasterxml/jackson/core/JsonGenerationException;
        }
    .end annotation

    .line 184
    invoke-virtual {p2}, Lcom/fasterxml/jackson/core/JsonGenerator;->writeStartObject()V

    .line 185
    invoke-virtual {p1}, Ljava/util/EnumMap;->isEmpty()Z

    move-result v0

    if-nez v0, :cond_0

    .line 186
    invoke-virtual {p0, p1, p2, p3}, Lcom/fasterxml/jackson/databind/ser/std/EnumMapSerializer;->serializeContents(Ljava/util/EnumMap;Lcom/fasterxml/jackson/core/JsonGenerator;Lcom/fasterxml/jackson/databind/SerializerProvider;)V

    .line 188
    :cond_0
    invoke-virtual {p2}, Lcom/fasterxml/jackson/core/JsonGenerator;->writeEndObject()V

    return-void
.end method

.method protected serializeContents(Ljava/util/EnumMap;Lcom/fasterxml/jackson/core/JsonGenerator;Lcom/fasterxml/jackson/databind/SerializerProvider;)V
    .locals 10
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/EnumMap<",
            "+",
            "Ljava/lang/Enum<",
            "*>;*>;",
            "Lcom/fasterxml/jackson/core/JsonGenerator;",
            "Lcom/fasterxml/jackson/databind/SerializerProvider;",
            ")V"
        }
    .end annotation

    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;,
            Lcom/fasterxml/jackson/core/JsonGenerationException;
        }
    .end annotation

    .line 206
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/ser/std/EnumMapSerializer;->_valueSerializer:Lcom/fasterxml/jackson/databind/JsonSerializer;

    if-eqz v0, :cond_0

    .line 207
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/ser/std/EnumMapSerializer;->_valueSerializer:Lcom/fasterxml/jackson/databind/JsonSerializer;

    invoke-virtual {p0, p1, p2, p3, v0}, Lcom/fasterxml/jackson/databind/ser/std/EnumMapSerializer;->serializeContentsUsing(Ljava/util/EnumMap;Lcom/fasterxml/jackson/core/JsonGenerator;Lcom/fasterxml/jackson/databind/SerializerProvider;Lcom/fasterxml/jackson/databind/JsonSerializer;)V

    return-void

    .line 212
    :cond_0
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/ser/std/EnumMapSerializer;->_keyEnums:Lcom/fasterxml/jackson/databind/util/EnumValues;

    .line 213
    sget-object v1, Lcom/fasterxml/jackson/databind/SerializationFeature;->WRITE_NULL_MAP_VALUES:Lcom/fasterxml/jackson/databind/SerializationFeature;

    invoke-virtual {p3, v1}, Lcom/fasterxml/jackson/databind/SerializerProvider;->isEnabled(Lcom/fasterxml/jackson/databind/SerializationFeature;)Z

    move-result v1

    xor-int/lit8 v1, v1, 0x1

    .line 214
    iget-object v2, p0, Lcom/fasterxml/jackson/databind/ser/std/EnumMapSerializer;->_valueTypeSerializer:Lcom/fasterxml/jackson/databind/jsontype/TypeSerializer;

    .line 216
    invoke-virtual {p1}, Ljava/util/EnumMap;->entrySet()Ljava/util/Set;

    move-result-object v3

    invoke-interface {v3}, Ljava/util/Set;->iterator()Ljava/util/Iterator;

    move-result-object v3

    const/4 v4, 0x0

    move-object v5, v4

    :goto_0
    invoke-interface {v3}, Ljava/util/Iterator;->hasNext()Z

    move-result v6

    if-eqz v6, :cond_6

    invoke-interface {v3}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v6

    check-cast v6, Ljava/util/Map$Entry;

    .line 217
    invoke-interface {v6}, Ljava/util/Map$Entry;->getValue()Ljava/lang/Object;

    move-result-object v7

    if-eqz v1, :cond_1

    if-nez v7, :cond_1

    goto :goto_0

    .line 222
    :cond_1
    invoke-interface {v6}, Ljava/util/Map$Entry;->getKey()Ljava/lang/Object;

    move-result-object v8

    check-cast v8, Ljava/lang/Enum;

    if-nez v0, :cond_2

    .line 229
    invoke-virtual {v8}, Ljava/lang/Enum;->getDeclaringClass()Ljava/lang/Class;

    move-result-object v0

    iget-object v9, p0, Lcom/fasterxml/jackson/databind/ser/std/EnumMapSerializer;->_property:Lcom/fasterxml/jackson/databind/BeanProperty;

    invoke-virtual {p3, v0, v9}, Lcom/fasterxml/jackson/databind/SerializerProvider;->findValueSerializer(Ljava/lang/Class;Lcom/fasterxml/jackson/databind/BeanProperty;)Lcom/fasterxml/jackson/databind/JsonSerializer;

    move-result-object v0

    check-cast v0, Lcom/fasterxml/jackson/databind/ser/std/StdSerializer;

    .line 231
    check-cast v0, Lcom/fasterxml/jackson/databind/ser/std/EnumSerializer;

    invoke-virtual {v0}, Lcom/fasterxml/jackson/databind/ser/std/EnumSerializer;->getEnumValues()Lcom/fasterxml/jackson/databind/util/EnumValues;

    move-result-object v0

    .line 233
    :cond_2
    invoke-virtual {v0, v8}, Lcom/fasterxml/jackson/databind/util/EnumValues;->serializedValueFor(Ljava/lang/Enum;)Lcom/fasterxml/jackson/core/SerializableString;

    move-result-object v8

    invoke-virtual {p2, v8}, Lcom/fasterxml/jackson/core/JsonGenerator;->writeFieldName(Lcom/fasterxml/jackson/core/SerializableString;)V

    if-nez v7, :cond_3

    .line 235
    invoke-virtual {p3, p2}, Lcom/fasterxml/jackson/databind/SerializerProvider;->defaultSerializeNull(Lcom/fasterxml/jackson/core/JsonGenerator;)V

    goto :goto_0

    .line 238
    :cond_3
    invoke-virtual {v7}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    move-result-object v8

    if-ne v8, v4, :cond_4

    goto :goto_1

    .line 243
    :cond_4
    iget-object v4, p0, Lcom/fasterxml/jackson/databind/ser/std/EnumMapSerializer;->_property:Lcom/fasterxml/jackson/databind/BeanProperty;

    invoke-virtual {p3, v8, v4}, Lcom/fasterxml/jackson/databind/SerializerProvider;->findValueSerializer(Ljava/lang/Class;Lcom/fasterxml/jackson/databind/BeanProperty;)Lcom/fasterxml/jackson/databind/JsonSerializer;

    move-result-object v4

    move-object v5, v4

    move-object v4, v8

    :goto_1
    if-nez v2, :cond_5

    .line 249
    :try_start_0
    invoke-virtual {v5, v7, p2, p3}, Lcom/fasterxml/jackson/databind/JsonSerializer;->serialize(Ljava/lang/Object;Lcom/fasterxml/jackson/core/JsonGenerator;Lcom/fasterxml/jackson/databind/SerializerProvider;)V

    goto :goto_0

    :catch_0
    move-exception v7

    goto :goto_2

    .line 251
    :cond_5
    invoke-virtual {v5, v7, p2, p3, v2}, Lcom/fasterxml/jackson/databind/JsonSerializer;->serializeWithType(Ljava/lang/Object;Lcom/fasterxml/jackson/core/JsonGenerator;Lcom/fasterxml/jackson/databind/SerializerProvider;Lcom/fasterxml/jackson/databind/jsontype/TypeSerializer;)V
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    .line 255
    :goto_2
    invoke-interface {v6}, Ljava/util/Map$Entry;->getKey()Ljava/lang/Object;

    move-result-object v6

    check-cast v6, Ljava/lang/Enum;

    invoke-virtual {v6}, Ljava/lang/Enum;->name()Ljava/lang/String;

    move-result-object v6

    invoke-virtual {p0, p3, v7, p1, v6}, Lcom/fasterxml/jackson/databind/ser/std/EnumMapSerializer;->wrapAndThrow(Lcom/fasterxml/jackson/databind/SerializerProvider;Ljava/lang/Throwable;Ljava/lang/Object;Ljava/lang/String;)V

    goto :goto_0

    :cond_6
    return-void
.end method

.method protected serializeContentsUsing(Ljava/util/EnumMap;Lcom/fasterxml/jackson/core/JsonGenerator;Lcom/fasterxml/jackson/databind/SerializerProvider;Lcom/fasterxml/jackson/databind/JsonSerializer;)V
    .locals 8
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/EnumMap<",
            "+",
            "Ljava/lang/Enum<",
            "*>;*>;",
            "Lcom/fasterxml/jackson/core/JsonGenerator;",
            "Lcom/fasterxml/jackson/databind/SerializerProvider;",
            "Lcom/fasterxml/jackson/databind/JsonSerializer<",
            "Ljava/lang/Object;",
            ">;)V"
        }
    .end annotation

    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;,
            Lcom/fasterxml/jackson/core/JsonGenerationException;
        }
    .end annotation

    .line 264
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/ser/std/EnumMapSerializer;->_keyEnums:Lcom/fasterxml/jackson/databind/util/EnumValues;

    .line 265
    sget-object v1, Lcom/fasterxml/jackson/databind/SerializationFeature;->WRITE_NULL_MAP_VALUES:Lcom/fasterxml/jackson/databind/SerializationFeature;

    invoke-virtual {p3, v1}, Lcom/fasterxml/jackson/databind/SerializerProvider;->isEnabled(Lcom/fasterxml/jackson/databind/SerializationFeature;)Z

    move-result v1

    xor-int/lit8 v1, v1, 0x1

    .line 266
    iget-object v2, p0, Lcom/fasterxml/jackson/databind/ser/std/EnumMapSerializer;->_valueTypeSerializer:Lcom/fasterxml/jackson/databind/jsontype/TypeSerializer;

    .line 268
    invoke-virtual {p1}, Ljava/util/EnumMap;->entrySet()Ljava/util/Set;

    move-result-object v3

    invoke-interface {v3}, Ljava/util/Set;->iterator()Ljava/util/Iterator;

    move-result-object v3

    :goto_0
    invoke-interface {v3}, Ljava/util/Iterator;->hasNext()Z

    move-result v4

    if-eqz v4, :cond_4

    invoke-interface {v3}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v4

    check-cast v4, Ljava/util/Map$Entry;

    .line 269
    invoke-interface {v4}, Ljava/util/Map$Entry;->getValue()Ljava/lang/Object;

    move-result-object v5

    if-eqz v1, :cond_0

    if-nez v5, :cond_0

    goto :goto_0

    .line 273
    :cond_0
    invoke-interface {v4}, Ljava/util/Map$Entry;->getKey()Ljava/lang/Object;

    move-result-object v6

    check-cast v6, Ljava/lang/Enum;

    if-nez v0, :cond_1

    .line 276
    invoke-virtual {v6}, Ljava/lang/Enum;->getDeclaringClass()Ljava/lang/Class;

    move-result-object v0

    iget-object v7, p0, Lcom/fasterxml/jackson/databind/ser/std/EnumMapSerializer;->_property:Lcom/fasterxml/jackson/databind/BeanProperty;

    invoke-virtual {p3, v0, v7}, Lcom/fasterxml/jackson/databind/SerializerProvider;->findValueSerializer(Ljava/lang/Class;Lcom/fasterxml/jackson/databind/BeanProperty;)Lcom/fasterxml/jackson/databind/JsonSerializer;

    move-result-object v0

    check-cast v0, Lcom/fasterxml/jackson/databind/ser/std/StdSerializer;

    .line 278
    check-cast v0, Lcom/fasterxml/jackson/databind/ser/std/EnumSerializer;

    invoke-virtual {v0}, Lcom/fasterxml/jackson/databind/ser/std/EnumSerializer;->getEnumValues()Lcom/fasterxml/jackson/databind/util/EnumValues;

    move-result-object v0

    .line 280
    :cond_1
    invoke-virtual {v0, v6}, Lcom/fasterxml/jackson/databind/util/EnumValues;->serializedValueFor(Ljava/lang/Enum;)Lcom/fasterxml/jackson/core/SerializableString;

    move-result-object v6

    invoke-virtual {p2, v6}, Lcom/fasterxml/jackson/core/JsonGenerator;->writeFieldName(Lcom/fasterxml/jackson/core/SerializableString;)V

    if-nez v5, :cond_2

    .line 282
    invoke-virtual {p3, p2}, Lcom/fasterxml/jackson/databind/SerializerProvider;->defaultSerializeNull(Lcom/fasterxml/jackson/core/JsonGenerator;)V

    goto :goto_0

    :cond_2
    if-nez v2, :cond_3

    .line 287
    :try_start_0
    invoke-virtual {p4, v5, p2, p3}, Lcom/fasterxml/jackson/databind/JsonSerializer;->serialize(Ljava/lang/Object;Lcom/fasterxml/jackson/core/JsonGenerator;Lcom/fasterxml/jackson/databind/SerializerProvider;)V

    goto :goto_0

    :catch_0
    move-exception v5

    goto :goto_1

    .line 289
    :cond_3
    invoke-virtual {p4, v5, p2, p3, v2}, Lcom/fasterxml/jackson/databind/JsonSerializer;->serializeWithType(Ljava/lang/Object;Lcom/fasterxml/jackson/core/JsonGenerator;Lcom/fasterxml/jackson/databind/SerializerProvider;Lcom/fasterxml/jackson/databind/jsontype/TypeSerializer;)V
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    .line 292
    :goto_1
    invoke-interface {v4}, Ljava/util/Map$Entry;->getKey()Ljava/lang/Object;

    move-result-object v4

    check-cast v4, Ljava/lang/Enum;

    invoke-virtual {v4}, Ljava/lang/Enum;->name()Ljava/lang/String;

    move-result-object v4

    invoke-virtual {p0, p3, v5, p1, v4}, Lcom/fasterxml/jackson/databind/ser/std/EnumMapSerializer;->wrapAndThrow(Lcom/fasterxml/jackson/databind/SerializerProvider;Ljava/lang/Throwable;Ljava/lang/Object;Ljava/lang/String;)V

    goto :goto_0

    :cond_4
    return-void
.end method

.method public bridge synthetic serializeWithType(Ljava/lang/Object;Lcom/fasterxml/jackson/core/JsonGenerator;Lcom/fasterxml/jackson/databind/SerializerProvider;Lcom/fasterxml/jackson/databind/jsontype/TypeSerializer;)V
    .locals 0
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    .line 31
    check-cast p1, Ljava/util/EnumMap;

    invoke-virtual {p0, p1, p2, p3, p4}, Lcom/fasterxml/jackson/databind/ser/std/EnumMapSerializer;->serializeWithType(Ljava/util/EnumMap;Lcom/fasterxml/jackson/core/JsonGenerator;Lcom/fasterxml/jackson/databind/SerializerProvider;Lcom/fasterxml/jackson/databind/jsontype/TypeSerializer;)V

    return-void
.end method

.method public serializeWithType(Ljava/util/EnumMap;Lcom/fasterxml/jackson/core/JsonGenerator;Lcom/fasterxml/jackson/databind/SerializerProvider;Lcom/fasterxml/jackson/databind/jsontype/TypeSerializer;)V
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/EnumMap<",
            "+",
            "Ljava/lang/Enum<",
            "*>;*>;",
            "Lcom/fasterxml/jackson/core/JsonGenerator;",
            "Lcom/fasterxml/jackson/databind/SerializerProvider;",
            "Lcom/fasterxml/jackson/databind/jsontype/TypeSerializer;",
            ")V"
        }
    .end annotation

    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;,
            Lcom/fasterxml/jackson/core/JsonGenerationException;
        }
    .end annotation

    .line 196
    invoke-virtual {p4, p1, p2}, Lcom/fasterxml/jackson/databind/jsontype/TypeSerializer;->writeTypePrefixForObject(Ljava/lang/Object;Lcom/fasterxml/jackson/core/JsonGenerator;)V

    .line 197
    invoke-virtual {p1}, Ljava/util/EnumMap;->isEmpty()Z

    move-result v0

    if-nez v0, :cond_0

    .line 198
    invoke-virtual {p0, p1, p2, p3}, Lcom/fasterxml/jackson/databind/ser/std/EnumMapSerializer;->serializeContents(Ljava/util/EnumMap;Lcom/fasterxml/jackson/core/JsonGenerator;Lcom/fasterxml/jackson/databind/SerializerProvider;)V

    .line 200
    :cond_0
    invoke-virtual {p4, p1, p2}, Lcom/fasterxml/jackson/databind/jsontype/TypeSerializer;->writeTypeSuffixForObject(Ljava/lang/Object;Lcom/fasterxml/jackson/core/JsonGenerator;)V

    return-void
.end method

.method public withValueSerializer(Lcom/fasterxml/jackson/databind/BeanProperty;Lcom/fasterxml/jackson/databind/JsonSerializer;)Lcom/fasterxml/jackson/databind/ser/std/EnumMapSerializer;
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Lcom/fasterxml/jackson/databind/BeanProperty;",
            "Lcom/fasterxml/jackson/databind/JsonSerializer<",
            "*>;)",
            "Lcom/fasterxml/jackson/databind/ser/std/EnumMapSerializer;"
        }
    .end annotation

    .line 104
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/ser/std/EnumMapSerializer;->_property:Lcom/fasterxml/jackson/databind/BeanProperty;

    if-ne v0, p1, :cond_0

    iget-object v0, p0, Lcom/fasterxml/jackson/databind/ser/std/EnumMapSerializer;->_valueSerializer:Lcom/fasterxml/jackson/databind/JsonSerializer;

    if-ne p2, v0, :cond_0

    return-object p0

    .line 107
    :cond_0
    new-instance v0, Lcom/fasterxml/jackson/databind/ser/std/EnumMapSerializer;

    invoke-direct {v0, p0, p1, p2}, Lcom/fasterxml/jackson/databind/ser/std/EnumMapSerializer;-><init>(Lcom/fasterxml/jackson/databind/ser/std/EnumMapSerializer;Lcom/fasterxml/jackson/databind/BeanProperty;Lcom/fasterxml/jackson/databind/JsonSerializer;)V

    return-object v0
.end method
