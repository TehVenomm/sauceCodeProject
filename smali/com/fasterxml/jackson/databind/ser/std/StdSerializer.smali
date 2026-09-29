.class public abstract Lcom/fasterxml/jackson/databind/ser/std/StdSerializer;
.super Lcom/fasterxml/jackson/databind/JsonSerializer;
.source "StdSerializer.java"

# interfaces
.implements Lcom/fasterxml/jackson/databind/jsonFormatVisitors/JsonFormatVisitable;
.implements Lcom/fasterxml/jackson/databind/jsonschema/SchemaAware;
.implements Ljava/io/Serializable;


# annotations
.annotation system Ldalvik/annotation/Signature;
    value = {
        "<T:",
        "Ljava/lang/Object;",
        ">",
        "Lcom/fasterxml/jackson/databind/JsonSerializer<",
        "TT;>;",
        "Lcom/fasterxml/jackson/databind/jsonFormatVisitors/JsonFormatVisitable;",
        "Lcom/fasterxml/jackson/databind/jsonschema/SchemaAware;",
        "Ljava/io/Serializable;"
    }
.end annotation


# static fields
.field private static final serialVersionUID:J = 0x1L


# instance fields
.field protected final _handledType:Ljava/lang/Class;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/lang/Class<",
            "TT;>;"
        }
    .end annotation
.end field


# direct methods
.method protected constructor <init>(Lcom/fasterxml/jackson/databind/JavaType;)V
    .locals 0

    .line 50
    invoke-direct {p0}, Lcom/fasterxml/jackson/databind/JsonSerializer;-><init>()V

    .line 51
    invoke-virtual {p1}, Lcom/fasterxml/jackson/databind/JavaType;->getRawClass()Ljava/lang/Class;

    move-result-object p1

    iput-object p1, p0, Lcom/fasterxml/jackson/databind/ser/std/StdSerializer;->_handledType:Ljava/lang/Class;

    return-void
.end method

.method protected constructor <init>(Ljava/lang/Class;)V
    .locals 0
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/lang/Class<",
            "TT;>;)V"
        }
    .end annotation

    .line 45
    invoke-direct {p0}, Lcom/fasterxml/jackson/databind/JsonSerializer;-><init>()V

    .line 46
    iput-object p1, p0, Lcom/fasterxml/jackson/databind/ser/std/StdSerializer;->_handledType:Ljava/lang/Class;

    return-void
.end method

.method protected constructor <init>(Ljava/lang/Class;Z)V
    .locals 0
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/lang/Class<",
            "*>;Z)V"
        }
    .end annotation

    .line 59
    invoke-direct {p0}, Lcom/fasterxml/jackson/databind/JsonSerializer;-><init>()V

    .line 60
    iput-object p1, p0, Lcom/fasterxml/jackson/databind/ser/std/StdSerializer;->_handledType:Ljava/lang/Class;

    return-void
.end method


# virtual methods
.method public acceptJsonFormatVisitor(Lcom/fasterxml/jackson/databind/jsonFormatVisitors/JsonFormatVisitorWrapper;Lcom/fasterxml/jackson/databind/JavaType;)V
    .locals 0
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Lcom/fasterxml/jackson/databind/JsonMappingException;
        }
    .end annotation

    .line 142
    invoke-interface {p1, p2}, Lcom/fasterxml/jackson/databind/jsonFormatVisitors/JsonFormatVisitorWrapper;->expectAnyFormat(Lcom/fasterxml/jackson/databind/JavaType;)Lcom/fasterxml/jackson/databind/jsonFormatVisitors/JsonAnyFormatVisitor;

    return-void
.end method

.method protected createObjectNode()Lcom/fasterxml/jackson/databind/node/ObjectNode;
    .locals 1

    .line 114
    sget-object v0, Lcom/fasterxml/jackson/databind/node/JsonNodeFactory;->instance:Lcom/fasterxml/jackson/databind/node/JsonNodeFactory;

    invoke-virtual {v0}, Lcom/fasterxml/jackson/databind/node/JsonNodeFactory;->objectNode()Lcom/fasterxml/jackson/databind/node/ObjectNode;

    move-result-object v0

    return-object v0
.end method

.method protected createSchemaNode(Ljava/lang/String;)Lcom/fasterxml/jackson/databind/node/ObjectNode;
    .locals 2

    .line 119
    invoke-virtual {p0}, Lcom/fasterxml/jackson/databind/ser/std/StdSerializer;->createObjectNode()Lcom/fasterxml/jackson/databind/node/ObjectNode;

    move-result-object v0

    const-string v1, "type"

    .line 120
    invoke-virtual {v0, v1, p1}, Lcom/fasterxml/jackson/databind/node/ObjectNode;->put(Ljava/lang/String;Ljava/lang/String;)Lcom/fasterxml/jackson/databind/node/ObjectNode;

    return-object v0
.end method

.method protected createSchemaNode(Ljava/lang/String;Z)Lcom/fasterxml/jackson/databind/node/ObjectNode;
    .locals 1

    .line 126
    invoke-virtual {p0, p1}, Lcom/fasterxml/jackson/databind/ser/std/StdSerializer;->createSchemaNode(Ljava/lang/String;)Lcom/fasterxml/jackson/databind/node/ObjectNode;

    move-result-object p1

    if-nez p2, :cond_0

    const-string v0, "required"

    xor-int/lit8 p2, p2, 0x1

    .line 129
    invoke-virtual {p1, v0, p2}, Lcom/fasterxml/jackson/databind/node/ObjectNode;->put(Ljava/lang/String;Z)Lcom/fasterxml/jackson/databind/node/ObjectNode;

    :cond_0
    return-object p1
.end method

.method protected findConvertingContentSerializer(Lcom/fasterxml/jackson/databind/SerializerProvider;Lcom/fasterxml/jackson/databind/BeanProperty;Lcom/fasterxml/jackson/databind/JsonSerializer;)Lcom/fasterxml/jackson/databind/JsonSerializer;
    .locals 2
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Lcom/fasterxml/jackson/databind/SerializerProvider;",
            "Lcom/fasterxml/jackson/databind/BeanProperty;",
            "Lcom/fasterxml/jackson/databind/JsonSerializer<",
            "*>;)",
            "Lcom/fasterxml/jackson/databind/JsonSerializer<",
            "*>;"
        }
    .end annotation

    .annotation system Ldalvik/annotation/Throws;
        value = {
            Lcom/fasterxml/jackson/databind/JsonMappingException;
        }
    .end annotation

    .line 254
    invoke-virtual {p1}, Lcom/fasterxml/jackson/databind/SerializerProvider;->getAnnotationIntrospector()Lcom/fasterxml/jackson/databind/AnnotationIntrospector;

    move-result-object v0

    if-eqz v0, :cond_1

    if-eqz p2, :cond_1

    .line 256
    invoke-interface {p2}, Lcom/fasterxml/jackson/databind/BeanProperty;->getMember()Lcom/fasterxml/jackson/databind/introspect/AnnotatedMember;

    move-result-object v1

    if-eqz v1, :cond_1

    .line 258
    invoke-virtual {v0, v1}, Lcom/fasterxml/jackson/databind/AnnotationIntrospector;->findSerializationContentConverter(Lcom/fasterxml/jackson/databind/introspect/AnnotatedMember;)Ljava/lang/Object;

    move-result-object v0

    if-eqz v0, :cond_1

    .line 260
    invoke-interface {p2}, Lcom/fasterxml/jackson/databind/BeanProperty;->getMember()Lcom/fasterxml/jackson/databind/introspect/AnnotatedMember;

    move-result-object p2

    invoke-virtual {p1, p2, v0}, Lcom/fasterxml/jackson/databind/SerializerProvider;->converterInstance(Lcom/fasterxml/jackson/databind/introspect/Annotated;Ljava/lang/Object;)Lcom/fasterxml/jackson/databind/util/Converter;

    move-result-object p2

    .line 261
    invoke-virtual {p1}, Lcom/fasterxml/jackson/databind/SerializerProvider;->getTypeFactory()Lcom/fasterxml/jackson/databind/type/TypeFactory;

    move-result-object v0

    invoke-interface {p2, v0}, Lcom/fasterxml/jackson/databind/util/Converter;->getOutputType(Lcom/fasterxml/jackson/databind/type/TypeFactory;)Lcom/fasterxml/jackson/databind/JavaType;

    move-result-object v0

    if-nez p3, :cond_0

    .line 263
    invoke-virtual {p1, v0}, Lcom/fasterxml/jackson/databind/SerializerProvider;->findValueSerializer(Lcom/fasterxml/jackson/databind/JavaType;)Lcom/fasterxml/jackson/databind/JsonSerializer;

    move-result-object p3

    .line 265
    :cond_0
    new-instance p1, Lcom/fasterxml/jackson/databind/ser/std/StdDelegatingSerializer;

    invoke-direct {p1, p2, v0, p3}, Lcom/fasterxml/jackson/databind/ser/std/StdDelegatingSerializer;-><init>(Lcom/fasterxml/jackson/databind/util/Converter;Lcom/fasterxml/jackson/databind/JavaType;Lcom/fasterxml/jackson/databind/JsonSerializer;)V

    return-object p1

    :cond_1
    return-object p3
.end method

.method protected findPropertyFilter(Lcom/fasterxml/jackson/databind/SerializerProvider;Ljava/lang/Object;Ljava/lang/Object;)Lcom/fasterxml/jackson/databind/ser/PropertyFilter;
    .locals 1
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Lcom/fasterxml/jackson/databind/JsonMappingException;
        }
    .end annotation

    .line 282
    invoke-virtual {p1}, Lcom/fasterxml/jackson/databind/SerializerProvider;->getFilterProvider()Lcom/fasterxml/jackson/databind/ser/FilterProvider;

    move-result-object p1

    if-eqz p1, :cond_0

    .line 287
    invoke-virtual {p1, p2, p3}, Lcom/fasterxml/jackson/databind/ser/FilterProvider;->findPropertyFilter(Ljava/lang/Object;Ljava/lang/Object;)Lcom/fasterxml/jackson/databind/ser/PropertyFilter;

    move-result-object p1

    return-object p1

    .line 285
    :cond_0
    new-instance p1, Lcom/fasterxml/jackson/databind/JsonMappingException;

    new-instance p3, Ljava/lang/StringBuilder;

    invoke-direct {p3}, Ljava/lang/StringBuilder;-><init>()V

    const-string v0, "Can not resolve PropertyFilter with id \'"

    invoke-virtual {p3, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p3, p2}, Ljava/lang/StringBuilder;->append(Ljava/lang/Object;)Ljava/lang/StringBuilder;

    const-string p2, "\'; no FilterProvider configured"

    invoke-virtual {p3, p2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p3}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p2

    invoke-direct {p1, p2}, Lcom/fasterxml/jackson/databind/JsonMappingException;-><init>(Ljava/lang/String;)V

    throw p1
.end method

.method public getSchema(Lcom/fasterxml/jackson/databind/SerializerProvider;Ljava/lang/reflect/Type;)Lcom/fasterxml/jackson/databind/JsonNode;
    .locals 0
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Lcom/fasterxml/jackson/databind/JsonMappingException;
        }
    .end annotation

    const-string p1, "string"

    .line 95
    invoke-virtual {p0, p1}, Lcom/fasterxml/jackson/databind/ser/std/StdSerializer;->createSchemaNode(Ljava/lang/String;)Lcom/fasterxml/jackson/databind/node/ObjectNode;

    move-result-object p1

    return-object p1
.end method

.method public getSchema(Lcom/fasterxml/jackson/databind/SerializerProvider;Ljava/lang/reflect/Type;Z)Lcom/fasterxml/jackson/databind/JsonNode;
    .locals 0
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Lcom/fasterxml/jackson/databind/JsonMappingException;
        }
    .end annotation

    .line 106
    invoke-virtual {p0, p1, p2}, Lcom/fasterxml/jackson/databind/ser/std/StdSerializer;->getSchema(Lcom/fasterxml/jackson/databind/SerializerProvider;Ljava/lang/reflect/Type;)Lcom/fasterxml/jackson/databind/JsonNode;

    move-result-object p1

    check-cast p1, Lcom/fasterxml/jackson/databind/node/ObjectNode;

    if-nez p3, :cond_0

    const-string p2, "required"

    xor-int/lit8 p3, p3, 0x1

    .line 108
    invoke-virtual {p1, p2, p3}, Lcom/fasterxml/jackson/databind/node/ObjectNode;->put(Ljava/lang/String;Z)Lcom/fasterxml/jackson/databind/node/ObjectNode;

    :cond_0
    return-object p1
.end method

.method public handledType()Ljava/lang/Class;
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()",
            "Ljava/lang/Class<",
            "TT;>;"
        }
    .end annotation

    .line 69
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/ser/std/StdSerializer;->_handledType:Ljava/lang/Class;

    return-object v0
.end method

.method protected isDefaultSerializer(Lcom/fasterxml/jackson/databind/JsonSerializer;)Z
    .locals 0
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Lcom/fasterxml/jackson/databind/JsonSerializer<",
            "*>;)Z"
        }
    .end annotation

    .line 232
    invoke-static {p1}, Lcom/fasterxml/jackson/databind/util/ClassUtil;->isJacksonStdImpl(Ljava/lang/Object;)Z

    move-result p1

    return p1
.end method

.method public abstract serialize(Ljava/lang/Object;Lcom/fasterxml/jackson/core/JsonGenerator;Lcom/fasterxml/jackson/databind/SerializerProvider;)V
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(TT;",
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
.end method

.method public wrapAndThrow(Lcom/fasterxml/jackson/databind/SerializerProvider;Ljava/lang/Throwable;Ljava/lang/Object;I)V
    .locals 1
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    .line 197
    :goto_0
    instance-of v0, p2, Ljava/lang/reflect/InvocationTargetException;

    if-eqz v0, :cond_0

    invoke-virtual {p2}, Ljava/lang/Throwable;->getCause()Ljava/lang/Throwable;

    move-result-object v0

    if-eqz v0, :cond_0

    .line 198
    invoke-virtual {p2}, Ljava/lang/Throwable;->getCause()Ljava/lang/Throwable;

    move-result-object p2

    goto :goto_0

    .line 201
    :cond_0
    instance-of v0, p2, Ljava/lang/Error;

    if-nez v0, :cond_6

    if-eqz p1, :cond_2

    .line 205
    sget-object v0, Lcom/fasterxml/jackson/databind/SerializationFeature;->WRAP_EXCEPTIONS:Lcom/fasterxml/jackson/databind/SerializationFeature;

    invoke-virtual {p1, v0}, Lcom/fasterxml/jackson/databind/SerializerProvider;->isEnabled(Lcom/fasterxml/jackson/databind/SerializationFeature;)Z

    move-result p1

    if-eqz p1, :cond_1

    goto :goto_1

    :cond_1
    const/4 p1, 0x0

    goto :goto_2

    :cond_2
    :goto_1
    const/4 p1, 0x1

    .line 206
    :goto_2
    instance-of v0, p2, Ljava/io/IOException;

    if-eqz v0, :cond_4

    if-eqz p1, :cond_3

    .line 207
    instance-of p1, p2, Lcom/fasterxml/jackson/databind/JsonMappingException;

    if-nez p1, :cond_5

    .line 208
    :cond_3
    check-cast p2, Ljava/io/IOException;

    throw p2

    :cond_4
    if-nez p1, :cond_5

    .line 211
    instance-of p1, p2, Ljava/lang/RuntimeException;

    if-eqz p1, :cond_5

    .line 212
    check-cast p2, Ljava/lang/RuntimeException;

    throw p2

    .line 216
    :cond_5
    invoke-static {p2, p3, p4}, Lcom/fasterxml/jackson/databind/JsonMappingException;->wrapWithPath(Ljava/lang/Throwable;Ljava/lang/Object;I)Lcom/fasterxml/jackson/databind/JsonMappingException;

    move-result-object p1

    throw p1

    .line 202
    :cond_6
    check-cast p2, Ljava/lang/Error;

    throw p2
.end method

.method public wrapAndThrow(Lcom/fasterxml/jackson/databind/SerializerProvider;Ljava/lang/Throwable;Ljava/lang/Object;Ljava/lang/String;)V
    .locals 1
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    .line 171
    :goto_0
    instance-of v0, p2, Ljava/lang/reflect/InvocationTargetException;

    if-eqz v0, :cond_0

    invoke-virtual {p2}, Ljava/lang/Throwable;->getCause()Ljava/lang/Throwable;

    move-result-object v0

    if-eqz v0, :cond_0

    .line 172
    invoke-virtual {p2}, Ljava/lang/Throwable;->getCause()Ljava/lang/Throwable;

    move-result-object p2

    goto :goto_0

    .line 175
    :cond_0
    instance-of v0, p2, Ljava/lang/Error;

    if-nez v0, :cond_6

    if-eqz p1, :cond_2

    .line 179
    sget-object v0, Lcom/fasterxml/jackson/databind/SerializationFeature;->WRAP_EXCEPTIONS:Lcom/fasterxml/jackson/databind/SerializationFeature;

    invoke-virtual {p1, v0}, Lcom/fasterxml/jackson/databind/SerializerProvider;->isEnabled(Lcom/fasterxml/jackson/databind/SerializationFeature;)Z

    move-result p1

    if-eqz p1, :cond_1

    goto :goto_1

    :cond_1
    const/4 p1, 0x0

    goto :goto_2

    :cond_2
    :goto_1
    const/4 p1, 0x1

    .line 180
    :goto_2
    instance-of v0, p2, Ljava/io/IOException;

    if-eqz v0, :cond_4

    if-eqz p1, :cond_3

    .line 181
    instance-of p1, p2, Lcom/fasterxml/jackson/databind/JsonMappingException;

    if-nez p1, :cond_5

    .line 182
    :cond_3
    check-cast p2, Ljava/io/IOException;

    throw p2

    :cond_4
    if-nez p1, :cond_5

    .line 185
    instance-of p1, p2, Ljava/lang/RuntimeException;

    if-eqz p1, :cond_5

    .line 186
    check-cast p2, Ljava/lang/RuntimeException;

    throw p2

    .line 190
    :cond_5
    invoke-static {p2, p3, p4}, Lcom/fasterxml/jackson/databind/JsonMappingException;->wrapWithPath(Ljava/lang/Throwable;Ljava/lang/Object;Ljava/lang/String;)Lcom/fasterxml/jackson/databind/JsonMappingException;

    move-result-object p1

    throw p1

    .line 176
    :cond_6
    check-cast p2, Ljava/lang/Error;

    throw p2
.end method
