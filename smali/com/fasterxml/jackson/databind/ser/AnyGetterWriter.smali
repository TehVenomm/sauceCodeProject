.class public Lcom/fasterxml/jackson/databind/ser/AnyGetterWriter;
.super Ljava/lang/Object;
.source "AnyGetterWriter.java"


# instance fields
.field protected final _accessor:Lcom/fasterxml/jackson/databind/introspect/AnnotatedMember;

.field protected final _property:Lcom/fasterxml/jackson/databind/BeanProperty;

.field protected _serializer:Lcom/fasterxml/jackson/databind/ser/std/MapSerializer;


# direct methods
.method public constructor <init>(Lcom/fasterxml/jackson/databind/BeanProperty;Lcom/fasterxml/jackson/databind/introspect/AnnotatedMember;Lcom/fasterxml/jackson/databind/ser/std/MapSerializer;)V
    .locals 0

    .line 28
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 29
    iput-object p2, p0, Lcom/fasterxml/jackson/databind/ser/AnyGetterWriter;->_accessor:Lcom/fasterxml/jackson/databind/introspect/AnnotatedMember;

    .line 30
    iput-object p1, p0, Lcom/fasterxml/jackson/databind/ser/AnyGetterWriter;->_property:Lcom/fasterxml/jackson/databind/BeanProperty;

    .line 31
    iput-object p3, p0, Lcom/fasterxml/jackson/databind/ser/AnyGetterWriter;->_serializer:Lcom/fasterxml/jackson/databind/ser/std/MapSerializer;

    return-void
.end method


# virtual methods
.method public getAndFilter(Ljava/lang/Object;Lcom/fasterxml/jackson/core/JsonGenerator;Lcom/fasterxml/jackson/databind/SerializerProvider;Lcom/fasterxml/jackson/databind/ser/PropertyFilter;)V
    .locals 7
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/lang/Exception;
        }
    .end annotation

    .line 55
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/ser/AnyGetterWriter;->_accessor:Lcom/fasterxml/jackson/databind/introspect/AnnotatedMember;

    invoke-virtual {v0, p1}, Lcom/fasterxml/jackson/databind/introspect/AnnotatedMember;->getValue(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object p1

    if-nez p1, :cond_0

    return-void

    .line 59
    :cond_0
    instance-of v0, p1, Ljava/util/Map;

    if-eqz v0, :cond_1

    .line 64
    iget-object v1, p0, Lcom/fasterxml/jackson/databind/ser/AnyGetterWriter;->_serializer:Lcom/fasterxml/jackson/databind/ser/std/MapSerializer;

    move-object v2, p1

    check-cast v2, Ljava/util/Map;

    const/4 v6, 0x0

    move-object v3, p2

    move-object v4, p3

    move-object v5, p4

    invoke-virtual/range {v1 .. v6}, Lcom/fasterxml/jackson/databind/ser/std/MapSerializer;->serializeFilteredFields(Ljava/util/Map;Lcom/fasterxml/jackson/core/JsonGenerator;Lcom/fasterxml/jackson/databind/SerializerProvider;Lcom/fasterxml/jackson/databind/ser/PropertyFilter;Ljava/lang/Object;)V

    return-void

    .line 60
    :cond_1
    new-instance p2, Lcom/fasterxml/jackson/databind/JsonMappingException;

    new-instance p3, Ljava/lang/StringBuilder;

    invoke-direct {p3}, Ljava/lang/StringBuilder;-><init>()V

    const-string p4, "Value returned by \'any-getter\' ("

    invoke-virtual {p3, p4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object p4, p0, Lcom/fasterxml/jackson/databind/ser/AnyGetterWriter;->_accessor:Lcom/fasterxml/jackson/databind/introspect/AnnotatedMember;

    invoke-virtual {p4}, Lcom/fasterxml/jackson/databind/introspect/AnnotatedMember;->getName()Ljava/lang/String;

    move-result-object p4

    invoke-virtual {p3, p4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string p4, "()) not java.util.Map but "

    invoke-virtual {p3, p4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p1}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    move-result-object p1

    invoke-virtual {p1}, Ljava/lang/Class;->getName()Ljava/lang/String;

    move-result-object p1

    invoke-virtual {p3, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p3}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p1

    invoke-direct {p2, p1}, Lcom/fasterxml/jackson/databind/JsonMappingException;-><init>(Ljava/lang/String;)V

    throw p2
.end method

.method public getAndSerialize(Ljava/lang/Object;Lcom/fasterxml/jackson/core/JsonGenerator;Lcom/fasterxml/jackson/databind/SerializerProvider;)V
    .locals 1
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/lang/Exception;
        }
    .end annotation

    .line 37
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/ser/AnyGetterWriter;->_accessor:Lcom/fasterxml/jackson/databind/introspect/AnnotatedMember;

    invoke-virtual {v0, p1}, Lcom/fasterxml/jackson/databind/introspect/AnnotatedMember;->getValue(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object p1

    if-nez p1, :cond_0

    return-void

    .line 41
    :cond_0
    instance-of v0, p1, Ljava/util/Map;

    if-eqz v0, :cond_1

    .line 45
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/ser/AnyGetterWriter;->_serializer:Lcom/fasterxml/jackson/databind/ser/std/MapSerializer;

    check-cast p1, Ljava/util/Map;

    invoke-virtual {v0, p1, p2, p3}, Lcom/fasterxml/jackson/databind/ser/std/MapSerializer;->serializeFields(Ljava/util/Map;Lcom/fasterxml/jackson/core/JsonGenerator;Lcom/fasterxml/jackson/databind/SerializerProvider;)V

    return-void

    .line 42
    :cond_1
    new-instance p2, Lcom/fasterxml/jackson/databind/JsonMappingException;

    new-instance p3, Ljava/lang/StringBuilder;

    invoke-direct {p3}, Ljava/lang/StringBuilder;-><init>()V

    const-string v0, "Value returned by \'any-getter\' ("

    invoke-virtual {p3, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v0, p0, Lcom/fasterxml/jackson/databind/ser/AnyGetterWriter;->_accessor:Lcom/fasterxml/jackson/databind/introspect/AnnotatedMember;

    invoke-virtual {v0}, Lcom/fasterxml/jackson/databind/introspect/AnnotatedMember;->getName()Ljava/lang/String;

    move-result-object v0

    invoke-virtual {p3, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v0, "()) not java.util.Map but "

    invoke-virtual {p3, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p1}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    move-result-object p1

    invoke-virtual {p1}, Ljava/lang/Class;->getName()Ljava/lang/String;

    move-result-object p1

    invoke-virtual {p3, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p3}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p1

    invoke-direct {p2, p1}, Lcom/fasterxml/jackson/databind/JsonMappingException;-><init>(Ljava/lang/String;)V

    throw p2
.end method

.method public resolve(Lcom/fasterxml/jackson/databind/SerializerProvider;)V
    .locals 2
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Lcom/fasterxml/jackson/databind/JsonMappingException;
        }
    .end annotation

    .line 71
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/ser/AnyGetterWriter;->_serializer:Lcom/fasterxml/jackson/databind/ser/std/MapSerializer;

    iget-object v1, p0, Lcom/fasterxml/jackson/databind/ser/AnyGetterWriter;->_property:Lcom/fasterxml/jackson/databind/BeanProperty;

    invoke-virtual {p1, v0, v1}, Lcom/fasterxml/jackson/databind/SerializerProvider;->handlePrimaryContextualization(Lcom/fasterxml/jackson/databind/JsonSerializer;Lcom/fasterxml/jackson/databind/BeanProperty;)Lcom/fasterxml/jackson/databind/JsonSerializer;

    move-result-object p1

    check-cast p1, Lcom/fasterxml/jackson/databind/ser/std/MapSerializer;

    iput-object p1, p0, Lcom/fasterxml/jackson/databind/ser/AnyGetterWriter;->_serializer:Lcom/fasterxml/jackson/databind/ser/std/MapSerializer;

    return-void
.end method
