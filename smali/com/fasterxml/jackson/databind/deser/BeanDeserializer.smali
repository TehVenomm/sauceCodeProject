.class public Lcom/fasterxml/jackson/databind/deser/BeanDeserializer;
.super Lcom/fasterxml/jackson/databind/deser/BeanDeserializerBase;
.source "BeanDeserializer.java"

# interfaces
.implements Ljava/io/Serializable;


# static fields
.field private static final serialVersionUID:J = 0x1L


# direct methods
.method protected constructor <init>(Lcom/fasterxml/jackson/databind/deser/BeanDeserializerBase;)V
    .locals 1

    .line 65
    iget-boolean v0, p1, Lcom/fasterxml/jackson/databind/deser/BeanDeserializerBase;->_ignoreAllUnknown:Z

    invoke-direct {p0, p1, v0}, Lcom/fasterxml/jackson/databind/deser/BeanDeserializerBase;-><init>(Lcom/fasterxml/jackson/databind/deser/BeanDeserializerBase;Z)V

    return-void
.end method

.method public constructor <init>(Lcom/fasterxml/jackson/databind/deser/BeanDeserializerBase;Lcom/fasterxml/jackson/databind/deser/impl/ObjectIdReader;)V
    .locals 0

    .line 77
    invoke-direct {p0, p1, p2}, Lcom/fasterxml/jackson/databind/deser/BeanDeserializerBase;-><init>(Lcom/fasterxml/jackson/databind/deser/BeanDeserializerBase;Lcom/fasterxml/jackson/databind/deser/impl/ObjectIdReader;)V

    return-void
.end method

.method protected constructor <init>(Lcom/fasterxml/jackson/databind/deser/BeanDeserializerBase;Lcom/fasterxml/jackson/databind/util/NameTransformer;)V
    .locals 0

    .line 73
    invoke-direct {p0, p1, p2}, Lcom/fasterxml/jackson/databind/deser/BeanDeserializerBase;-><init>(Lcom/fasterxml/jackson/databind/deser/BeanDeserializerBase;Lcom/fasterxml/jackson/databind/util/NameTransformer;)V

    return-void
.end method

.method public constructor <init>(Lcom/fasterxml/jackson/databind/deser/BeanDeserializerBase;Ljava/util/HashSet;)V
    .locals 0
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Lcom/fasterxml/jackson/databind/deser/BeanDeserializerBase;",
            "Ljava/util/HashSet<",
            "Ljava/lang/String;",
            ">;)V"
        }
    .end annotation

    .line 81
    invoke-direct {p0, p1, p2}, Lcom/fasterxml/jackson/databind/deser/BeanDeserializerBase;-><init>(Lcom/fasterxml/jackson/databind/deser/BeanDeserializerBase;Ljava/util/HashSet;)V

    return-void
.end method

.method protected constructor <init>(Lcom/fasterxml/jackson/databind/deser/BeanDeserializerBase;Z)V
    .locals 0

    .line 69
    invoke-direct {p0, p1, p2}, Lcom/fasterxml/jackson/databind/deser/BeanDeserializerBase;-><init>(Lcom/fasterxml/jackson/databind/deser/BeanDeserializerBase;Z)V

    return-void
.end method

.method public constructor <init>(Lcom/fasterxml/jackson/databind/deser/BeanDeserializerBuilder;Lcom/fasterxml/jackson/databind/BeanDescription;Lcom/fasterxml/jackson/databind/deser/impl/BeanPropertyMap;Ljava/util/Map;Ljava/util/HashSet;ZZ)V
    .locals 0
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Lcom/fasterxml/jackson/databind/deser/BeanDeserializerBuilder;",
            "Lcom/fasterxml/jackson/databind/BeanDescription;",
            "Lcom/fasterxml/jackson/databind/deser/impl/BeanPropertyMap;",
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Lcom/fasterxml/jackson/databind/deser/SettableBeanProperty;",
            ">;",
            "Ljava/util/HashSet<",
            "Ljava/lang/String;",
            ">;ZZ)V"
        }
    .end annotation

    .line 56
    invoke-direct/range {p0 .. p7}, Lcom/fasterxml/jackson/databind/deser/BeanDeserializerBase;-><init>(Lcom/fasterxml/jackson/databind/deser/BeanDeserializerBuilder;Lcom/fasterxml/jackson/databind/BeanDescription;Lcom/fasterxml/jackson/databind/deser/impl/BeanPropertyMap;Ljava/util/Map;Ljava/util/HashSet;ZZ)V

    return-void
.end method

.method private final vanillaDeserialize(Lcom/fasterxml/jackson/core/JsonParser;Lcom/fasterxml/jackson/databind/DeserializationContext;Lcom/fasterxml/jackson/core/JsonToken;)Ljava/lang/Object;
    .locals 2
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    .line 241
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/deser/BeanDeserializer;->_valueInstantiator:Lcom/fasterxml/jackson/databind/deser/ValueInstantiator;

    invoke-virtual {v0, p2}, Lcom/fasterxml/jackson/databind/deser/ValueInstantiator;->createUsingDefault(Lcom/fasterxml/jackson/databind/DeserializationContext;)Ljava/lang/Object;

    move-result-object v0

    .line 243
    invoke-virtual {p1, v0}, Lcom/fasterxml/jackson/core/JsonParser;->setCurrentValue(Ljava/lang/Object;)V

    .line 245
    :goto_0
    sget-object v1, Lcom/fasterxml/jackson/core/JsonToken;->FIELD_NAME:Lcom/fasterxml/jackson/core/JsonToken;

    if-ne p3, v1, :cond_1

    .line 246
    invoke-virtual {p1}, Lcom/fasterxml/jackson/core/JsonParser;->getCurrentName()Ljava/lang/String;

    move-result-object p3

    .line 247
    invoke-virtual {p1}, Lcom/fasterxml/jackson/core/JsonParser;->nextToken()Lcom/fasterxml/jackson/core/JsonToken;

    .line 248
    iget-object v1, p0, Lcom/fasterxml/jackson/databind/deser/BeanDeserializer;->_beanProperties:Lcom/fasterxml/jackson/databind/deser/impl/BeanPropertyMap;

    invoke-virtual {v1, p1, p2, v0, p3}, Lcom/fasterxml/jackson/databind/deser/impl/BeanPropertyMap;->findDeserializeAndSet(Lcom/fasterxml/jackson/core/JsonParser;Lcom/fasterxml/jackson/databind/DeserializationContext;Ljava/lang/Object;Ljava/lang/String;)Z

    move-result v1

    if-nez v1, :cond_0

    .line 249
    invoke-virtual {p0, p1, p2, v0, p3}, Lcom/fasterxml/jackson/databind/deser/BeanDeserializer;->handleUnknownVanilla(Lcom/fasterxml/jackson/core/JsonParser;Lcom/fasterxml/jackson/databind/DeserializationContext;Ljava/lang/Object;Ljava/lang/String;)V

    .line 245
    :cond_0
    invoke-virtual {p1}, Lcom/fasterxml/jackson/core/JsonParser;->nextToken()Lcom/fasterxml/jackson/core/JsonToken;

    move-result-object p3

    goto :goto_0

    :cond_1
    return-object v0
.end method


# virtual methods
.method protected final _deserializeOther(Lcom/fasterxml/jackson/core/JsonParser;Lcom/fasterxml/jackson/databind/DeserializationContext;Lcom/fasterxml/jackson/core/JsonToken;)Ljava/lang/Object;
    .locals 2
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    .line 151
    sget-object v0, Lcom/fasterxml/jackson/databind/deser/BeanDeserializer$1;->$SwitchMap$com$fasterxml$jackson$core$JsonToken:[I

    invoke-virtual {p3}, Lcom/fasterxml/jackson/core/JsonToken;->ordinal()I

    move-result v1

    aget v0, v0, v1

    packed-switch v0, :pswitch_data_0

    .line 176
    invoke-virtual {p0}, Lcom/fasterxml/jackson/databind/deser/BeanDeserializer;->handledType()Ljava/lang/Class;

    move-result-object p1

    invoke-virtual {p2, p1}, Lcom/fasterxml/jackson/databind/DeserializationContext;->mappingException(Ljava/lang/Class;)Lcom/fasterxml/jackson/databind/JsonMappingException;

    move-result-object p1

    throw p1

    .line 168
    :pswitch_0
    iget-boolean v0, p0, Lcom/fasterxml/jackson/databind/deser/BeanDeserializer;->_vanillaProcessing:Z

    if-eqz v0, :cond_0

    .line 169
    invoke-direct {p0, p1, p2, p3}, Lcom/fasterxml/jackson/databind/deser/BeanDeserializer;->vanillaDeserialize(Lcom/fasterxml/jackson/core/JsonParser;Lcom/fasterxml/jackson/databind/DeserializationContext;Lcom/fasterxml/jackson/core/JsonToken;)Ljava/lang/Object;

    move-result-object p1

    return-object p1

    .line 171
    :cond_0
    iget-object p3, p0, Lcom/fasterxml/jackson/databind/deser/BeanDeserializer;->_objectIdReader:Lcom/fasterxml/jackson/databind/deser/impl/ObjectIdReader;

    if-eqz p3, :cond_1

    .line 172
    invoke-virtual {p0, p1, p2}, Lcom/fasterxml/jackson/databind/deser/BeanDeserializer;->deserializeWithObjectId(Lcom/fasterxml/jackson/core/JsonParser;Lcom/fasterxml/jackson/databind/DeserializationContext;)Ljava/lang/Object;

    move-result-object p1

    return-object p1

    .line 174
    :cond_1
    invoke-virtual {p0, p1, p2}, Lcom/fasterxml/jackson/databind/deser/BeanDeserializer;->deserializeFromObject(Lcom/fasterxml/jackson/core/JsonParser;Lcom/fasterxml/jackson/databind/DeserializationContext;)Ljava/lang/Object;

    move-result-object p1

    return-object p1

    .line 165
    :pswitch_1
    invoke-virtual {p0, p1, p2}, Lcom/fasterxml/jackson/databind/deser/BeanDeserializer;->deserializeFromArray(Lcom/fasterxml/jackson/core/JsonParser;Lcom/fasterxml/jackson/databind/DeserializationContext;)Ljava/lang/Object;

    move-result-object p1

    return-object p1

    .line 162
    :pswitch_2
    invoke-virtual {p0, p1, p2}, Lcom/fasterxml/jackson/databind/deser/BeanDeserializer;->deserializeFromBoolean(Lcom/fasterxml/jackson/core/JsonParser;Lcom/fasterxml/jackson/databind/DeserializationContext;)Ljava/lang/Object;

    move-result-object p1

    return-object p1

    .line 159
    :pswitch_3
    invoke-virtual {p0, p1, p2}, Lcom/fasterxml/jackson/databind/deser/BeanDeserializer;->deserializeFromEmbedded(Lcom/fasterxml/jackson/core/JsonParser;Lcom/fasterxml/jackson/databind/DeserializationContext;)Ljava/lang/Object;

    move-result-object p1

    return-object p1

    .line 157
    :pswitch_4
    invoke-virtual {p0, p1, p2}, Lcom/fasterxml/jackson/databind/deser/BeanDeserializer;->deserializeFromDouble(Lcom/fasterxml/jackson/core/JsonParser;Lcom/fasterxml/jackson/databind/DeserializationContext;)Ljava/lang/Object;

    move-result-object p1

    return-object p1

    .line 155
    :pswitch_5
    invoke-virtual {p0, p1, p2}, Lcom/fasterxml/jackson/databind/deser/BeanDeserializer;->deserializeFromNumber(Lcom/fasterxml/jackson/core/JsonParser;Lcom/fasterxml/jackson/databind/DeserializationContext;)Ljava/lang/Object;

    move-result-object p1

    return-object p1

    .line 153
    :pswitch_6
    invoke-virtual {p0, p1, p2}, Lcom/fasterxml/jackson/databind/deser/BeanDeserializer;->deserializeFromString(Lcom/fasterxml/jackson/core/JsonParser;Lcom/fasterxml/jackson/databind/DeserializationContext;)Ljava/lang/Object;

    move-result-object p1

    return-object p1

    nop

    :pswitch_data_0
    .packed-switch 0x1
        :pswitch_6
        :pswitch_5
        :pswitch_4
        :pswitch_3
        :pswitch_2
        :pswitch_2
        :pswitch_1
        :pswitch_0
        :pswitch_0
    .end packed-switch
.end method

.method protected _deserializeUsingPropertyBased(Lcom/fasterxml/jackson/core/JsonParser;Lcom/fasterxml/jackson/databind/DeserializationContext;)Ljava/lang/Object;
    .locals 7
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    .line 367
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/deser/BeanDeserializer;->_propertyBasedCreator:Lcom/fasterxml/jackson/databind/deser/impl/PropertyBasedCreator;

    .line 368
    iget-object v1, p0, Lcom/fasterxml/jackson/databind/deser/BeanDeserializer;->_objectIdReader:Lcom/fasterxml/jackson/databind/deser/impl/ObjectIdReader;

    invoke-virtual {v0, p1, p2, v1}, Lcom/fasterxml/jackson/databind/deser/impl/PropertyBasedCreator;->startBuilding(Lcom/fasterxml/jackson/core/JsonParser;Lcom/fasterxml/jackson/databind/DeserializationContext;Lcom/fasterxml/jackson/databind/deser/impl/ObjectIdReader;)Lcom/fasterxml/jackson/databind/deser/impl/PropertyValueBuffer;

    move-result-object v1

    .line 373
    invoke-virtual {p1}, Lcom/fasterxml/jackson/core/JsonParser;->getCurrentToken()Lcom/fasterxml/jackson/core/JsonToken;

    move-result-object v2

    const/4 v3, 0x0

    move-object v4, v3

    .line 374
    :goto_0
    sget-object v5, Lcom/fasterxml/jackson/core/JsonToken;->FIELD_NAME:Lcom/fasterxml/jackson/core/JsonToken;

    if-ne v2, v5, :cond_a

    .line 375
    invoke-virtual {p1}, Lcom/fasterxml/jackson/core/JsonParser;->getCurrentName()Ljava/lang/String;

    move-result-object v2

    .line 376
    invoke-virtual {p1}, Lcom/fasterxml/jackson/core/JsonParser;->nextToken()Lcom/fasterxml/jackson/core/JsonToken;

    .line 378
    invoke-virtual {v0, v2}, Lcom/fasterxml/jackson/databind/deser/impl/PropertyBasedCreator;->findCreatorProperty(Ljava/lang/String;)Lcom/fasterxml/jackson/databind/deser/SettableBeanProperty;

    move-result-object v5

    if-eqz v5, :cond_3

    .line 381
    invoke-virtual {v5, p1, p2}, Lcom/fasterxml/jackson/databind/deser/SettableBeanProperty;->deserialize(Lcom/fasterxml/jackson/core/JsonParser;Lcom/fasterxml/jackson/databind/DeserializationContext;)Ljava/lang/Object;

    move-result-object v6

    .line 382
    invoke-virtual {v5}, Lcom/fasterxml/jackson/databind/deser/SettableBeanProperty;->getCreatorIndex()I

    move-result v5

    invoke-virtual {v1, v5, v6}, Lcom/fasterxml/jackson/databind/deser/impl/PropertyValueBuffer;->assignParameter(ILjava/lang/Object;)Z

    move-result v5

    if-eqz v5, :cond_9

    .line 383
    invoke-virtual {p1}, Lcom/fasterxml/jackson/core/JsonParser;->nextToken()Lcom/fasterxml/jackson/core/JsonToken;

    .line 386
    :try_start_0
    invoke-virtual {v0, p2, v1}, Lcom/fasterxml/jackson/databind/deser/impl/PropertyBasedCreator;->build(Lcom/fasterxml/jackson/databind/DeserializationContext;Lcom/fasterxml/jackson/databind/deser/impl/PropertyValueBuffer;)Ljava/lang/Object;

    move-result-object v0
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_1

    :catch_0
    move-exception v0

    .line 388
    iget-object v1, p0, Lcom/fasterxml/jackson/databind/deser/BeanDeserializer;->_beanType:Lcom/fasterxml/jackson/databind/JavaType;

    invoke-virtual {v1}, Lcom/fasterxml/jackson/databind/JavaType;->getRawClass()Ljava/lang/Class;

    move-result-object v1

    invoke-virtual {p0, v0, v1, v2, p2}, Lcom/fasterxml/jackson/databind/deser/BeanDeserializer;->wrapAndThrow(Ljava/lang/Throwable;Ljava/lang/Object;Ljava/lang/String;Lcom/fasterxml/jackson/databind/DeserializationContext;)V

    move-object v0, v3

    :goto_1
    if-eqz v0, :cond_2

    .line 395
    invoke-virtual {p1, v0}, Lcom/fasterxml/jackson/core/JsonParser;->setCurrentValue(Ljava/lang/Object;)V

    .line 398
    invoke-virtual {v0}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    move-result-object v1

    iget-object v2, p0, Lcom/fasterxml/jackson/databind/deser/BeanDeserializer;->_beanType:Lcom/fasterxml/jackson/databind/JavaType;

    invoke-virtual {v2}, Lcom/fasterxml/jackson/databind/JavaType;->getRawClass()Ljava/lang/Class;

    move-result-object v2

    if-eq v1, v2, :cond_0

    .line 399
    invoke-virtual {p0, p1, p2, v0, v4}, Lcom/fasterxml/jackson/databind/deser/BeanDeserializer;->handlePolymorphic(Lcom/fasterxml/jackson/core/JsonParser;Lcom/fasterxml/jackson/databind/DeserializationContext;Ljava/lang/Object;Lcom/fasterxml/jackson/databind/util/TokenBuffer;)Ljava/lang/Object;

    move-result-object p1

    return-object p1

    :cond_0
    if-eqz v4, :cond_1

    .line 402
    invoke-virtual {p0, p2, v0, v4}, Lcom/fasterxml/jackson/databind/deser/BeanDeserializer;->handleUnknownProperties(Lcom/fasterxml/jackson/databind/DeserializationContext;Ljava/lang/Object;Lcom/fasterxml/jackson/databind/util/TokenBuffer;)Ljava/lang/Object;

    move-result-object v0

    .line 405
    :cond_1
    invoke-virtual {p0, p1, p2, v0}, Lcom/fasterxml/jackson/databind/deser/BeanDeserializer;->deserialize(Lcom/fasterxml/jackson/core/JsonParser;Lcom/fasterxml/jackson/databind/DeserializationContext;Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object p1

    return-object p1

    .line 392
    :cond_2
    iget-object p1, p0, Lcom/fasterxml/jackson/databind/deser/BeanDeserializer;->_beanType:Lcom/fasterxml/jackson/databind/JavaType;

    invoke-virtual {p1}, Lcom/fasterxml/jackson/databind/JavaType;->getRawClass()Ljava/lang/Class;

    move-result-object p1

    const-string v0, "JSON Creator returned null"

    invoke-virtual {p2, p1, v0}, Lcom/fasterxml/jackson/databind/DeserializationContext;->instantiationException(Ljava/lang/Class;Ljava/lang/String;)Lcom/fasterxml/jackson/databind/JsonMappingException;

    move-result-object p1

    throw p1

    .line 410
    :cond_3
    invoke-virtual {v1, v2}, Lcom/fasterxml/jackson/databind/deser/impl/PropertyValueBuffer;->readIdProperty(Ljava/lang/String;)Z

    move-result v5

    if-eqz v5, :cond_4

    goto :goto_2

    .line 414
    :cond_4
    iget-object v5, p0, Lcom/fasterxml/jackson/databind/deser/BeanDeserializer;->_beanProperties:Lcom/fasterxml/jackson/databind/deser/impl/BeanPropertyMap;

    invoke-virtual {v5, v2}, Lcom/fasterxml/jackson/databind/deser/impl/BeanPropertyMap;->find(Ljava/lang/String;)Lcom/fasterxml/jackson/databind/deser/SettableBeanProperty;

    move-result-object v5

    if-eqz v5, :cond_5

    .line 416
    invoke-virtual {v5, p1, p2}, Lcom/fasterxml/jackson/databind/deser/SettableBeanProperty;->deserialize(Lcom/fasterxml/jackson/core/JsonParser;Lcom/fasterxml/jackson/databind/DeserializationContext;)Ljava/lang/Object;

    move-result-object v2

    invoke-virtual {v1, v5, v2}, Lcom/fasterxml/jackson/databind/deser/impl/PropertyValueBuffer;->bufferProperty(Lcom/fasterxml/jackson/databind/deser/SettableBeanProperty;Ljava/lang/Object;)V

    goto :goto_2

    .line 421
    :cond_5
    iget-object v5, p0, Lcom/fasterxml/jackson/databind/deser/BeanDeserializer;->_ignorableProps:Ljava/util/HashSet;

    if-eqz v5, :cond_6

    iget-object v5, p0, Lcom/fasterxml/jackson/databind/deser/BeanDeserializer;->_ignorableProps:Ljava/util/HashSet;

    invoke-virtual {v5, v2}, Ljava/util/HashSet;->contains(Ljava/lang/Object;)Z

    move-result v5

    if-eqz v5, :cond_6

    .line 422
    invoke-virtual {p0}, Lcom/fasterxml/jackson/databind/deser/BeanDeserializer;->handledType()Ljava/lang/Class;

    move-result-object v5

    invoke-virtual {p0, p1, p2, v5, v2}, Lcom/fasterxml/jackson/databind/deser/BeanDeserializer;->handleIgnoredProperty(Lcom/fasterxml/jackson/core/JsonParser;Lcom/fasterxml/jackson/databind/DeserializationContext;Ljava/lang/Object;Ljava/lang/String;)V

    goto :goto_2

    .line 426
    :cond_6
    iget-object v5, p0, Lcom/fasterxml/jackson/databind/deser/BeanDeserializer;->_anySetter:Lcom/fasterxml/jackson/databind/deser/SettableAnyProperty;

    if-eqz v5, :cond_7

    .line 427
    iget-object v5, p0, Lcom/fasterxml/jackson/databind/deser/BeanDeserializer;->_anySetter:Lcom/fasterxml/jackson/databind/deser/SettableAnyProperty;

    iget-object v6, p0, Lcom/fasterxml/jackson/databind/deser/BeanDeserializer;->_anySetter:Lcom/fasterxml/jackson/databind/deser/SettableAnyProperty;

    invoke-virtual {v6, p1, p2}, Lcom/fasterxml/jackson/databind/deser/SettableAnyProperty;->deserialize(Lcom/fasterxml/jackson/core/JsonParser;Lcom/fasterxml/jackson/databind/DeserializationContext;)Ljava/lang/Object;

    move-result-object v6

    invoke-virtual {v1, v5, v2, v6}, Lcom/fasterxml/jackson/databind/deser/impl/PropertyValueBuffer;->bufferAnyProperty(Lcom/fasterxml/jackson/databind/deser/SettableAnyProperty;Ljava/lang/String;Ljava/lang/Object;)V

    goto :goto_2

    :cond_7
    if-nez v4, :cond_8

    .line 432
    new-instance v4, Lcom/fasterxml/jackson/databind/util/TokenBuffer;

    invoke-direct {v4, p1}, Lcom/fasterxml/jackson/databind/util/TokenBuffer;-><init>(Lcom/fasterxml/jackson/core/JsonParser;)V

    .line 434
    :cond_8
    invoke-virtual {v4, v2}, Lcom/fasterxml/jackson/databind/util/TokenBuffer;->writeFieldName(Ljava/lang/String;)V

    .line 435
    invoke-virtual {v4, p1}, Lcom/fasterxml/jackson/databind/util/TokenBuffer;->copyCurrentStructure(Lcom/fasterxml/jackson/core/JsonParser;)V

    .line 374
    :cond_9
    :goto_2
    invoke-virtual {p1}, Lcom/fasterxml/jackson/core/JsonParser;->nextToken()Lcom/fasterxml/jackson/core/JsonToken;

    move-result-object v2

    goto/16 :goto_0

    .line 441
    :cond_a
    :try_start_1
    invoke-virtual {v0, p2, v1}, Lcom/fasterxml/jackson/databind/deser/impl/PropertyBasedCreator;->build(Lcom/fasterxml/jackson/databind/DeserializationContext;Lcom/fasterxml/jackson/databind/deser/impl/PropertyValueBuffer;)Ljava/lang/Object;

    move-result-object p1
    :try_end_1
    .catch Ljava/lang/Exception; {:try_start_1 .. :try_end_1} :catch_1

    goto :goto_3

    :catch_1
    move-exception p1

    .line 443
    invoke-virtual {p0, p1, p2}, Lcom/fasterxml/jackson/databind/deser/BeanDeserializer;->wrapInstantiationProblem(Ljava/lang/Throwable;Lcom/fasterxml/jackson/databind/DeserializationContext;)V

    move-object p1, v3

    :goto_3
    if-eqz v4, :cond_c

    .line 448
    invoke-virtual {p1}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    move-result-object v0

    iget-object v1, p0, Lcom/fasterxml/jackson/databind/deser/BeanDeserializer;->_beanType:Lcom/fasterxml/jackson/databind/JavaType;

    invoke-virtual {v1}, Lcom/fasterxml/jackson/databind/JavaType;->getRawClass()Ljava/lang/Class;

    move-result-object v1

    if-eq v0, v1, :cond_b

    .line 449
    invoke-virtual {p0, v3, p2, p1, v4}, Lcom/fasterxml/jackson/databind/deser/BeanDeserializer;->handlePolymorphic(Lcom/fasterxml/jackson/core/JsonParser;Lcom/fasterxml/jackson/databind/DeserializationContext;Ljava/lang/Object;Lcom/fasterxml/jackson/databind/util/TokenBuffer;)Ljava/lang/Object;

    move-result-object p1

    return-object p1

    .line 452
    :cond_b
    invoke-virtual {p0, p2, p1, v4}, Lcom/fasterxml/jackson/databind/deser/BeanDeserializer;->handleUnknownProperties(Lcom/fasterxml/jackson/databind/DeserializationContext;Ljava/lang/Object;Lcom/fasterxml/jackson/databind/util/TokenBuffer;)Ljava/lang/Object;

    move-result-object p1

    return-object p1

    :cond_c
    return-object p1
.end method

.method protected _missingToken(Lcom/fasterxml/jackson/core/JsonParser;Lcom/fasterxml/jackson/databind/DeserializationContext;)Ljava/lang/Object;
    .locals 0
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Lcom/fasterxml/jackson/core/JsonProcessingException;
        }
    .end annotation

    .line 183
    invoke-virtual {p0}, Lcom/fasterxml/jackson/databind/deser/BeanDeserializer;->handledType()Ljava/lang/Class;

    move-result-object p1

    invoke-virtual {p2, p1}, Lcom/fasterxml/jackson/databind/DeserializationContext;->endOfInputException(Ljava/lang/Class;)Lcom/fasterxml/jackson/databind/JsonMappingException;

    move-result-object p1

    throw p1
.end method

.method protected asArrayDeserializer()Lcom/fasterxml/jackson/databind/deser/BeanDeserializerBase;
    .locals 2

    .line 112
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/deser/BeanDeserializer;->_beanProperties:Lcom/fasterxml/jackson/databind/deser/impl/BeanPropertyMap;

    invoke-virtual {v0}, Lcom/fasterxml/jackson/databind/deser/impl/BeanPropertyMap;->getPropertiesInInsertionOrder()[Lcom/fasterxml/jackson/databind/deser/SettableBeanProperty;

    move-result-object v0

    .line 113
    new-instance v1, Lcom/fasterxml/jackson/databind/deser/impl/BeanAsArrayDeserializer;

    invoke-direct {v1, p0, v0}, Lcom/fasterxml/jackson/databind/deser/impl/BeanAsArrayDeserializer;-><init>(Lcom/fasterxml/jackson/databind/deser/BeanDeserializerBase;[Lcom/fasterxml/jackson/databind/deser/SettableBeanProperty;)V

    return-object v1
.end method

.method public deserialize(Lcom/fasterxml/jackson/core/JsonParser;Lcom/fasterxml/jackson/databind/DeserializationContext;)Ljava/lang/Object;
    .locals 2
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    .line 132
    invoke-virtual {p1}, Lcom/fasterxml/jackson/core/JsonParser;->getCurrentToken()Lcom/fasterxml/jackson/core/JsonToken;

    move-result-object v0

    .line 134
    sget-object v1, Lcom/fasterxml/jackson/core/JsonToken;->START_OBJECT:Lcom/fasterxml/jackson/core/JsonToken;

    if-ne v0, v1, :cond_2

    .line 135
    iget-boolean v0, p0, Lcom/fasterxml/jackson/databind/deser/BeanDeserializer;->_vanillaProcessing:Z

    if-eqz v0, :cond_0

    .line 136
    invoke-virtual {p1}, Lcom/fasterxml/jackson/core/JsonParser;->nextToken()Lcom/fasterxml/jackson/core/JsonToken;

    move-result-object v0

    invoke-direct {p0, p1, p2, v0}, Lcom/fasterxml/jackson/databind/deser/BeanDeserializer;->vanillaDeserialize(Lcom/fasterxml/jackson/core/JsonParser;Lcom/fasterxml/jackson/databind/DeserializationContext;Lcom/fasterxml/jackson/core/JsonToken;)Ljava/lang/Object;

    move-result-object p1

    return-object p1

    .line 138
    :cond_0
    invoke-virtual {p1}, Lcom/fasterxml/jackson/core/JsonParser;->nextToken()Lcom/fasterxml/jackson/core/JsonToken;

    .line 139
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/deser/BeanDeserializer;->_objectIdReader:Lcom/fasterxml/jackson/databind/deser/impl/ObjectIdReader;

    if-eqz v0, :cond_1

    .line 140
    invoke-virtual {p0, p1, p2}, Lcom/fasterxml/jackson/databind/deser/BeanDeserializer;->deserializeWithObjectId(Lcom/fasterxml/jackson/core/JsonParser;Lcom/fasterxml/jackson/databind/DeserializationContext;)Ljava/lang/Object;

    move-result-object p1

    return-object p1

    .line 142
    :cond_1
    invoke-virtual {p0, p1, p2}, Lcom/fasterxml/jackson/databind/deser/BeanDeserializer;->deserializeFromObject(Lcom/fasterxml/jackson/core/JsonParser;Lcom/fasterxml/jackson/databind/DeserializationContext;)Ljava/lang/Object;

    move-result-object p1

    return-object p1

    .line 144
    :cond_2
    invoke-virtual {p0, p1, p2, v0}, Lcom/fasterxml/jackson/databind/deser/BeanDeserializer;->_deserializeOther(Lcom/fasterxml/jackson/core/JsonParser;Lcom/fasterxml/jackson/databind/DeserializationContext;Lcom/fasterxml/jackson/core/JsonToken;)Ljava/lang/Object;

    move-result-object p1

    return-object p1
.end method

.method public deserialize(Lcom/fasterxml/jackson/core/JsonParser;Lcom/fasterxml/jackson/databind/DeserializationContext;Ljava/lang/Object;)Ljava/lang/Object;
    .locals 2
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    .line 196
    invoke-virtual {p1, p3}, Lcom/fasterxml/jackson/core/JsonParser;->setCurrentValue(Ljava/lang/Object;)V

    .line 197
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/deser/BeanDeserializer;->_injectables:[Lcom/fasterxml/jackson/databind/deser/impl/ValueInjector;

    if-eqz v0, :cond_0

    .line 198
    invoke-virtual {p0, p2, p3}, Lcom/fasterxml/jackson/databind/deser/BeanDeserializer;->injectValues(Lcom/fasterxml/jackson/databind/DeserializationContext;Ljava/lang/Object;)V

    .line 200
    :cond_0
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/deser/BeanDeserializer;->_unwrappedPropertyHandler:Lcom/fasterxml/jackson/databind/deser/impl/UnwrappedPropertyHandler;

    if-eqz v0, :cond_1

    .line 201
    invoke-virtual {p0, p1, p2, p3}, Lcom/fasterxml/jackson/databind/deser/BeanDeserializer;->deserializeWithUnwrapped(Lcom/fasterxml/jackson/core/JsonParser;Lcom/fasterxml/jackson/databind/DeserializationContext;Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object p1

    return-object p1

    .line 203
    :cond_1
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/deser/BeanDeserializer;->_externalTypeIdHandler:Lcom/fasterxml/jackson/databind/deser/impl/ExternalTypeHandler;

    if-eqz v0, :cond_2

    .line 204
    invoke-virtual {p0, p1, p2, p3}, Lcom/fasterxml/jackson/databind/deser/BeanDeserializer;->deserializeWithExternalTypeId(Lcom/fasterxml/jackson/core/JsonParser;Lcom/fasterxml/jackson/databind/DeserializationContext;Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object p1

    return-object p1

    .line 206
    :cond_2
    invoke-virtual {p1}, Lcom/fasterxml/jackson/core/JsonParser;->getCurrentToken()Lcom/fasterxml/jackson/core/JsonToken;

    move-result-object v0

    .line 208
    sget-object v1, Lcom/fasterxml/jackson/core/JsonToken;->START_OBJECT:Lcom/fasterxml/jackson/core/JsonToken;

    if-ne v0, v1, :cond_3

    .line 209
    invoke-virtual {p1}, Lcom/fasterxml/jackson/core/JsonParser;->nextToken()Lcom/fasterxml/jackson/core/JsonToken;

    move-result-object v0

    .line 211
    :cond_3
    iget-boolean v1, p0, Lcom/fasterxml/jackson/databind/deser/BeanDeserializer;->_needViewProcesing:Z

    if-eqz v1, :cond_4

    .line 212
    invoke-virtual {p2}, Lcom/fasterxml/jackson/databind/DeserializationContext;->getActiveView()Ljava/lang/Class;

    move-result-object v1

    if-eqz v1, :cond_4

    .line 214
    invoke-virtual {p0, p1, p2, p3, v1}, Lcom/fasterxml/jackson/databind/deser/BeanDeserializer;->deserializeWithView(Lcom/fasterxml/jackson/core/JsonParser;Lcom/fasterxml/jackson/databind/DeserializationContext;Ljava/lang/Object;Ljava/lang/Class;)Ljava/lang/Object;

    move-result-object p1

    return-object p1

    .line 217
    :cond_4
    :goto_0
    sget-object v1, Lcom/fasterxml/jackson/core/JsonToken;->FIELD_NAME:Lcom/fasterxml/jackson/core/JsonToken;

    if-ne v0, v1, :cond_6

    .line 218
    invoke-virtual {p1}, Lcom/fasterxml/jackson/core/JsonParser;->getCurrentName()Ljava/lang/String;

    move-result-object v0

    .line 219
    invoke-virtual {p1}, Lcom/fasterxml/jackson/core/JsonParser;->nextToken()Lcom/fasterxml/jackson/core/JsonToken;

    .line 220
    iget-object v1, p0, Lcom/fasterxml/jackson/databind/deser/BeanDeserializer;->_beanProperties:Lcom/fasterxml/jackson/databind/deser/impl/BeanPropertyMap;

    invoke-virtual {v1, p1, p2, p3, v0}, Lcom/fasterxml/jackson/databind/deser/impl/BeanPropertyMap;->findDeserializeAndSet(Lcom/fasterxml/jackson/core/JsonParser;Lcom/fasterxml/jackson/databind/DeserializationContext;Ljava/lang/Object;Ljava/lang/String;)Z

    move-result v1

    if-nez v1, :cond_5

    .line 221
    invoke-virtual {p0, p1, p2, p3, v0}, Lcom/fasterxml/jackson/databind/deser/BeanDeserializer;->handleUnknownVanilla(Lcom/fasterxml/jackson/core/JsonParser;Lcom/fasterxml/jackson/databind/DeserializationContext;Ljava/lang/Object;Ljava/lang/String;)V

    .line 217
    :cond_5
    invoke-virtual {p1}, Lcom/fasterxml/jackson/core/JsonParser;->nextToken()Lcom/fasterxml/jackson/core/JsonToken;

    move-result-object v0

    goto :goto_0

    :cond_6
    return-object p3
.end method

.method public deserializeFromObject(Lcom/fasterxml/jackson/core/JsonParser;Lcom/fasterxml/jackson/databind/DeserializationContext;)Ljava/lang/Object;
    .locals 3
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    .line 281
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/deser/BeanDeserializer;->_objectIdReader:Lcom/fasterxml/jackson/databind/deser/impl/ObjectIdReader;

    if-eqz v0, :cond_0

    iget-object v0, p0, Lcom/fasterxml/jackson/databind/deser/BeanDeserializer;->_objectIdReader:Lcom/fasterxml/jackson/databind/deser/impl/ObjectIdReader;

    invoke-virtual {v0}, Lcom/fasterxml/jackson/databind/deser/impl/ObjectIdReader;->maySerializeAsObject()Z

    move-result v0

    if-eqz v0, :cond_0

    .line 283
    invoke-virtual {p1}, Lcom/fasterxml/jackson/core/JsonParser;->getCurrentTokenId()I

    move-result v0

    const/4 v1, 0x5

    if-ne v0, v1, :cond_0

    iget-object v0, p0, Lcom/fasterxml/jackson/databind/deser/BeanDeserializer;->_objectIdReader:Lcom/fasterxml/jackson/databind/deser/impl/ObjectIdReader;

    invoke-virtual {p1}, Lcom/fasterxml/jackson/core/JsonParser;->getCurrentName()Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1, p1}, Lcom/fasterxml/jackson/databind/deser/impl/ObjectIdReader;->isValidReferencePropertyName(Ljava/lang/String;Lcom/fasterxml/jackson/core/JsonParser;)Z

    move-result v0

    if-eqz v0, :cond_0

    .line 285
    invoke-virtual {p0, p1, p2}, Lcom/fasterxml/jackson/databind/deser/BeanDeserializer;->deserializeFromObjectId(Lcom/fasterxml/jackson/core/JsonParser;Lcom/fasterxml/jackson/databind/DeserializationContext;)Ljava/lang/Object;

    move-result-object p1

    return-object p1

    .line 288
    :cond_0
    iget-boolean v0, p0, Lcom/fasterxml/jackson/databind/deser/BeanDeserializer;->_nonStandardCreation:Z

    if-eqz v0, :cond_4

    .line 289
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/deser/BeanDeserializer;->_unwrappedPropertyHandler:Lcom/fasterxml/jackson/databind/deser/impl/UnwrappedPropertyHandler;

    if-eqz v0, :cond_1

    .line 290
    invoke-virtual {p0, p1, p2}, Lcom/fasterxml/jackson/databind/deser/BeanDeserializer;->deserializeWithUnwrapped(Lcom/fasterxml/jackson/core/JsonParser;Lcom/fasterxml/jackson/databind/DeserializationContext;)Ljava/lang/Object;

    move-result-object p1

    return-object p1

    .line 292
    :cond_1
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/deser/BeanDeserializer;->_externalTypeIdHandler:Lcom/fasterxml/jackson/databind/deser/impl/ExternalTypeHandler;

    if-eqz v0, :cond_2

    .line 293
    invoke-virtual {p0, p1, p2}, Lcom/fasterxml/jackson/databind/deser/BeanDeserializer;->deserializeWithExternalTypeId(Lcom/fasterxml/jackson/core/JsonParser;Lcom/fasterxml/jackson/databind/DeserializationContext;)Ljava/lang/Object;

    move-result-object p1

    return-object p1

    .line 295
    :cond_2
    invoke-virtual {p0, p1, p2}, Lcom/fasterxml/jackson/databind/deser/BeanDeserializer;->deserializeFromObjectUsingNonDefault(Lcom/fasterxml/jackson/core/JsonParser;Lcom/fasterxml/jackson/databind/DeserializationContext;)Ljava/lang/Object;

    move-result-object p1

    .line 296
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/deser/BeanDeserializer;->_injectables:[Lcom/fasterxml/jackson/databind/deser/impl/ValueInjector;

    if-eqz v0, :cond_3

    .line 297
    invoke-virtual {p0, p2, p1}, Lcom/fasterxml/jackson/databind/deser/BeanDeserializer;->injectValues(Lcom/fasterxml/jackson/databind/DeserializationContext;Ljava/lang/Object;)V

    :cond_3
    return-object p1

    .line 313
    :cond_4
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/deser/BeanDeserializer;->_valueInstantiator:Lcom/fasterxml/jackson/databind/deser/ValueInstantiator;

    invoke-virtual {v0, p2}, Lcom/fasterxml/jackson/databind/deser/ValueInstantiator;->createUsingDefault(Lcom/fasterxml/jackson/databind/DeserializationContext;)Ljava/lang/Object;

    move-result-object v0

    .line 315
    invoke-virtual {p1, v0}, Lcom/fasterxml/jackson/core/JsonParser;->setCurrentValue(Ljava/lang/Object;)V

    .line 316
    invoke-virtual {p1}, Lcom/fasterxml/jackson/core/JsonParser;->canReadObjectId()Z

    move-result v1

    if-eqz v1, :cond_5

    .line 317
    invoke-virtual {p1}, Lcom/fasterxml/jackson/core/JsonParser;->getObjectId()Ljava/lang/Object;

    move-result-object v1

    if-eqz v1, :cond_5

    .line 319
    invoke-virtual {p0, p1, p2, v0, v1}, Lcom/fasterxml/jackson/databind/deser/BeanDeserializer;->_handleTypedObjectId(Lcom/fasterxml/jackson/core/JsonParser;Lcom/fasterxml/jackson/databind/DeserializationContext;Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 322
    :cond_5
    iget-object v1, p0, Lcom/fasterxml/jackson/databind/deser/BeanDeserializer;->_injectables:[Lcom/fasterxml/jackson/databind/deser/impl/ValueInjector;

    if-eqz v1, :cond_6

    .line 323
    invoke-virtual {p0, p2, v0}, Lcom/fasterxml/jackson/databind/deser/BeanDeserializer;->injectValues(Lcom/fasterxml/jackson/databind/DeserializationContext;Ljava/lang/Object;)V

    .line 325
    :cond_6
    iget-boolean v1, p0, Lcom/fasterxml/jackson/databind/deser/BeanDeserializer;->_needViewProcesing:Z

    if-eqz v1, :cond_7

    .line 326
    invoke-virtual {p2}, Lcom/fasterxml/jackson/databind/DeserializationContext;->getActiveView()Ljava/lang/Class;

    move-result-object v1

    if-eqz v1, :cond_7

    .line 328
    invoke-virtual {p0, p1, p2, v0, v1}, Lcom/fasterxml/jackson/databind/deser/BeanDeserializer;->deserializeWithView(Lcom/fasterxml/jackson/core/JsonParser;Lcom/fasterxml/jackson/databind/DeserializationContext;Ljava/lang/Object;Ljava/lang/Class;)Ljava/lang/Object;

    move-result-object p1

    return-object p1

    .line 331
    :cond_7
    invoke-virtual {p1}, Lcom/fasterxml/jackson/core/JsonParser;->getCurrentToken()Lcom/fasterxml/jackson/core/JsonToken;

    move-result-object v1

    .line 332
    :goto_0
    sget-object v2, Lcom/fasterxml/jackson/core/JsonToken;->FIELD_NAME:Lcom/fasterxml/jackson/core/JsonToken;

    if-ne v1, v2, :cond_9

    .line 333
    invoke-virtual {p1}, Lcom/fasterxml/jackson/core/JsonParser;->getCurrentName()Ljava/lang/String;

    move-result-object v1

    .line 334
    invoke-virtual {p1}, Lcom/fasterxml/jackson/core/JsonParser;->nextToken()Lcom/fasterxml/jackson/core/JsonToken;

    .line 335
    iget-object v2, p0, Lcom/fasterxml/jackson/databind/deser/BeanDeserializer;->_beanProperties:Lcom/fasterxml/jackson/databind/deser/impl/BeanPropertyMap;

    invoke-virtual {v2, p1, p2, v0, v1}, Lcom/fasterxml/jackson/databind/deser/impl/BeanPropertyMap;->findDeserializeAndSet(Lcom/fasterxml/jackson/core/JsonParser;Lcom/fasterxml/jackson/databind/DeserializationContext;Ljava/lang/Object;Ljava/lang/String;)Z

    move-result v2

    if-nez v2, :cond_8

    .line 336
    invoke-virtual {p0, p1, p2, v0, v1}, Lcom/fasterxml/jackson/databind/deser/BeanDeserializer;->handleUnknownVanilla(Lcom/fasterxml/jackson/core/JsonParser;Lcom/fasterxml/jackson/databind/DeserializationContext;Ljava/lang/Object;Ljava/lang/String;)V

    .line 332
    :cond_8
    invoke-virtual {p1}, Lcom/fasterxml/jackson/core/JsonParser;->nextToken()Lcom/fasterxml/jackson/core/JsonToken;

    move-result-object v1

    goto :goto_0

    :cond_9
    return-object v0
.end method

.method protected deserializeUsingPropertyBasedWithExternalTypeId(Lcom/fasterxml/jackson/core/JsonParser;Lcom/fasterxml/jackson/databind/DeserializationContext;)Ljava/lang/Object;
    .locals 7
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    .line 760
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/deser/BeanDeserializer;->_externalTypeIdHandler:Lcom/fasterxml/jackson/databind/deser/impl/ExternalTypeHandler;

    invoke-virtual {v0}, Lcom/fasterxml/jackson/databind/deser/impl/ExternalTypeHandler;->start()Lcom/fasterxml/jackson/databind/deser/impl/ExternalTypeHandler;

    move-result-object v0

    .line 761
    iget-object v1, p0, Lcom/fasterxml/jackson/databind/deser/BeanDeserializer;->_propertyBasedCreator:Lcom/fasterxml/jackson/databind/deser/impl/PropertyBasedCreator;

    .line 762
    iget-object v2, p0, Lcom/fasterxml/jackson/databind/deser/BeanDeserializer;->_objectIdReader:Lcom/fasterxml/jackson/databind/deser/impl/ObjectIdReader;

    invoke-virtual {v1, p1, p2, v2}, Lcom/fasterxml/jackson/databind/deser/impl/PropertyBasedCreator;->startBuilding(Lcom/fasterxml/jackson/core/JsonParser;Lcom/fasterxml/jackson/databind/DeserializationContext;Lcom/fasterxml/jackson/databind/deser/impl/ObjectIdReader;)Lcom/fasterxml/jackson/databind/deser/impl/PropertyValueBuffer;

    move-result-object v2

    .line 764
    new-instance v3, Lcom/fasterxml/jackson/databind/util/TokenBuffer;

    invoke-direct {v3, p1}, Lcom/fasterxml/jackson/databind/util/TokenBuffer;-><init>(Lcom/fasterxml/jackson/core/JsonParser;)V

    .line 765
    invoke-virtual {v3}, Lcom/fasterxml/jackson/databind/util/TokenBuffer;->writeStartObject()V

    .line 767
    invoke-virtual {p1}, Lcom/fasterxml/jackson/core/JsonParser;->getCurrentToken()Lcom/fasterxml/jackson/core/JsonToken;

    move-result-object v4

    .line 768
    :goto_0
    sget-object v5, Lcom/fasterxml/jackson/core/JsonToken;->FIELD_NAME:Lcom/fasterxml/jackson/core/JsonToken;

    const/4 v6, 0x0

    if-ne v4, v5, :cond_9

    .line 769
    invoke-virtual {p1}, Lcom/fasterxml/jackson/core/JsonParser;->getCurrentName()Ljava/lang/String;

    move-result-object v4

    .line 770
    invoke-virtual {p1}, Lcom/fasterxml/jackson/core/JsonParser;->nextToken()Lcom/fasterxml/jackson/core/JsonToken;

    .line 772
    invoke-virtual {v1, v4}, Lcom/fasterxml/jackson/databind/deser/impl/PropertyBasedCreator;->findCreatorProperty(Ljava/lang/String;)Lcom/fasterxml/jackson/databind/deser/SettableBeanProperty;

    move-result-object v5

    if-eqz v5, :cond_3

    .line 775
    invoke-virtual {v0, p1, p2, v4, v2}, Lcom/fasterxml/jackson/databind/deser/impl/ExternalTypeHandler;->handlePropertyValue(Lcom/fasterxml/jackson/core/JsonParser;Lcom/fasterxml/jackson/databind/DeserializationContext;Ljava/lang/String;Ljava/lang/Object;)Z

    move-result v6

    if-eqz v6, :cond_0

    goto/16 :goto_2

    .line 779
    :cond_0
    invoke-virtual {v5, p1, p2}, Lcom/fasterxml/jackson/databind/deser/SettableBeanProperty;->deserialize(Lcom/fasterxml/jackson/core/JsonParser;Lcom/fasterxml/jackson/databind/DeserializationContext;)Ljava/lang/Object;

    move-result-object v6

    .line 780
    invoke-virtual {v5}, Lcom/fasterxml/jackson/databind/deser/SettableBeanProperty;->getCreatorIndex()I

    move-result v5

    invoke-virtual {v2, v5, v6}, Lcom/fasterxml/jackson/databind/deser/impl/PropertyValueBuffer;->assignParameter(ILjava/lang/Object;)Z

    move-result v5

    if-eqz v5, :cond_8

    .line 781
    invoke-virtual {p1}, Lcom/fasterxml/jackson/core/JsonParser;->nextToken()Lcom/fasterxml/jackson/core/JsonToken;

    move-result-object v5

    .line 784
    :try_start_0
    invoke-virtual {v1, p2, v2}, Lcom/fasterxml/jackson/databind/deser/impl/PropertyBasedCreator;->build(Lcom/fasterxml/jackson/databind/DeserializationContext;Lcom/fasterxml/jackson/databind/deser/impl/PropertyValueBuffer;)Ljava/lang/Object;

    move-result-object v6
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    .line 790
    :goto_1
    sget-object v1, Lcom/fasterxml/jackson/core/JsonToken;->FIELD_NAME:Lcom/fasterxml/jackson/core/JsonToken;

    if-ne v5, v1, :cond_1

    .line 791
    invoke-virtual {p1}, Lcom/fasterxml/jackson/core/JsonParser;->nextToken()Lcom/fasterxml/jackson/core/JsonToken;

    .line 792
    invoke-virtual {v3, p1}, Lcom/fasterxml/jackson/databind/util/TokenBuffer;->copyCurrentStructure(Lcom/fasterxml/jackson/core/JsonParser;)V

    .line 793
    invoke-virtual {p1}, Lcom/fasterxml/jackson/core/JsonParser;->nextToken()Lcom/fasterxml/jackson/core/JsonToken;

    move-result-object v5

    goto :goto_1

    .line 795
    :cond_1
    invoke-virtual {v6}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    move-result-object v1

    iget-object v2, p0, Lcom/fasterxml/jackson/databind/deser/BeanDeserializer;->_beanType:Lcom/fasterxml/jackson/databind/JavaType;

    invoke-virtual {v2}, Lcom/fasterxml/jackson/databind/JavaType;->getRawClass()Ljava/lang/Class;

    move-result-object v2

    if-ne v1, v2, :cond_2

    .line 800
    invoke-virtual {v0, p1, p2, v6}, Lcom/fasterxml/jackson/databind/deser/impl/ExternalTypeHandler;->complete(Lcom/fasterxml/jackson/core/JsonParser;Lcom/fasterxml/jackson/databind/DeserializationContext;Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object p1

    return-object p1

    :cond_2
    const-string p1, "Can not create polymorphic instances with unwrapped values"

    .line 798
    invoke-virtual {p2, p1}, Lcom/fasterxml/jackson/databind/DeserializationContext;->mappingException(Ljava/lang/String;)Lcom/fasterxml/jackson/databind/JsonMappingException;

    move-result-object p1

    throw p1

    :catch_0
    move-exception v5

    .line 786
    iget-object v6, p0, Lcom/fasterxml/jackson/databind/deser/BeanDeserializer;->_beanType:Lcom/fasterxml/jackson/databind/JavaType;

    invoke-virtual {v6}, Lcom/fasterxml/jackson/databind/JavaType;->getRawClass()Ljava/lang/Class;

    move-result-object v6

    invoke-virtual {p0, v5, v6, v4, p2}, Lcom/fasterxml/jackson/databind/deser/BeanDeserializer;->wrapAndThrow(Ljava/lang/Throwable;Ljava/lang/Object;Ljava/lang/String;Lcom/fasterxml/jackson/databind/DeserializationContext;)V

    goto :goto_2

    .line 806
    :cond_3
    invoke-virtual {v2, v4}, Lcom/fasterxml/jackson/databind/deser/impl/PropertyValueBuffer;->readIdProperty(Ljava/lang/String;)Z

    move-result v5

    if-eqz v5, :cond_4

    goto :goto_2

    .line 810
    :cond_4
    iget-object v5, p0, Lcom/fasterxml/jackson/databind/deser/BeanDeserializer;->_beanProperties:Lcom/fasterxml/jackson/databind/deser/impl/BeanPropertyMap;

    invoke-virtual {v5, v4}, Lcom/fasterxml/jackson/databind/deser/impl/BeanPropertyMap;->find(Ljava/lang/String;)Lcom/fasterxml/jackson/databind/deser/SettableBeanProperty;

    move-result-object v5

    if-eqz v5, :cond_5

    .line 812
    invoke-virtual {v5, p1, p2}, Lcom/fasterxml/jackson/databind/deser/SettableBeanProperty;->deserialize(Lcom/fasterxml/jackson/core/JsonParser;Lcom/fasterxml/jackson/databind/DeserializationContext;)Ljava/lang/Object;

    move-result-object v4

    invoke-virtual {v2, v5, v4}, Lcom/fasterxml/jackson/databind/deser/impl/PropertyValueBuffer;->bufferProperty(Lcom/fasterxml/jackson/databind/deser/SettableBeanProperty;Ljava/lang/Object;)V

    goto :goto_2

    .line 816
    :cond_5
    invoke-virtual {v0, p1, p2, v4, v6}, Lcom/fasterxml/jackson/databind/deser/impl/ExternalTypeHandler;->handlePropertyValue(Lcom/fasterxml/jackson/core/JsonParser;Lcom/fasterxml/jackson/databind/DeserializationContext;Ljava/lang/String;Ljava/lang/Object;)Z

    move-result v5

    if-eqz v5, :cond_6

    goto :goto_2

    .line 822
    :cond_6
    iget-object v5, p0, Lcom/fasterxml/jackson/databind/deser/BeanDeserializer;->_ignorableProps:Ljava/util/HashSet;

    if-eqz v5, :cond_7

    iget-object v5, p0, Lcom/fasterxml/jackson/databind/deser/BeanDeserializer;->_ignorableProps:Ljava/util/HashSet;

    invoke-virtual {v5, v4}, Ljava/util/HashSet;->contains(Ljava/lang/Object;)Z

    move-result v5

    if-eqz v5, :cond_7

    .line 823
    invoke-virtual {p0}, Lcom/fasterxml/jackson/databind/deser/BeanDeserializer;->handledType()Ljava/lang/Class;

    move-result-object v5

    invoke-virtual {p0, p1, p2, v5, v4}, Lcom/fasterxml/jackson/databind/deser/BeanDeserializer;->handleIgnoredProperty(Lcom/fasterxml/jackson/core/JsonParser;Lcom/fasterxml/jackson/databind/DeserializationContext;Ljava/lang/Object;Ljava/lang/String;)V

    goto :goto_2

    .line 827
    :cond_7
    iget-object v5, p0, Lcom/fasterxml/jackson/databind/deser/BeanDeserializer;->_anySetter:Lcom/fasterxml/jackson/databind/deser/SettableAnyProperty;

    if-eqz v5, :cond_8

    .line 828
    iget-object v5, p0, Lcom/fasterxml/jackson/databind/deser/BeanDeserializer;->_anySetter:Lcom/fasterxml/jackson/databind/deser/SettableAnyProperty;

    iget-object v6, p0, Lcom/fasterxml/jackson/databind/deser/BeanDeserializer;->_anySetter:Lcom/fasterxml/jackson/databind/deser/SettableAnyProperty;

    invoke-virtual {v6, p1, p2}, Lcom/fasterxml/jackson/databind/deser/SettableAnyProperty;->deserialize(Lcom/fasterxml/jackson/core/JsonParser;Lcom/fasterxml/jackson/databind/DeserializationContext;)Ljava/lang/Object;

    move-result-object v6

    invoke-virtual {v2, v5, v4, v6}, Lcom/fasterxml/jackson/databind/deser/impl/PropertyValueBuffer;->bufferAnyProperty(Lcom/fasterxml/jackson/databind/deser/SettableAnyProperty;Ljava/lang/String;Ljava/lang/Object;)V

    .line 768
    :cond_8
    :goto_2
    invoke-virtual {p1}, Lcom/fasterxml/jackson/core/JsonParser;->nextToken()Lcom/fasterxml/jackson/core/JsonToken;

    move-result-object v4

    goto/16 :goto_0

    .line 834
    :cond_9
    :try_start_1
    invoke-virtual {v0, p1, p2, v2, v1}, Lcom/fasterxml/jackson/databind/deser/impl/ExternalTypeHandler;->complete(Lcom/fasterxml/jackson/core/JsonParser;Lcom/fasterxml/jackson/databind/DeserializationContext;Lcom/fasterxml/jackson/databind/deser/impl/PropertyValueBuffer;Lcom/fasterxml/jackson/databind/deser/impl/PropertyBasedCreator;)Ljava/lang/Object;

    move-result-object p1
    :try_end_1
    .catch Ljava/lang/Exception; {:try_start_1 .. :try_end_1} :catch_1

    return-object p1

    :catch_1
    move-exception p1

    .line 836
    invoke-virtual {p0, p1, p2}, Lcom/fasterxml/jackson/databind/deser/BeanDeserializer;->wrapInstantiationProblem(Ljava/lang/Throwable;Lcom/fasterxml/jackson/databind/DeserializationContext;)V

    return-object v6
.end method

.method protected deserializeUsingPropertyBasedWithUnwrapped(Lcom/fasterxml/jackson/core/JsonParser;Lcom/fasterxml/jackson/databind/DeserializationContext;)Ljava/lang/Object;
    .locals 6
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    .line 609
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/deser/BeanDeserializer;->_propertyBasedCreator:Lcom/fasterxml/jackson/databind/deser/impl/PropertyBasedCreator;

    .line 610
    iget-object v1, p0, Lcom/fasterxml/jackson/databind/deser/BeanDeserializer;->_objectIdReader:Lcom/fasterxml/jackson/databind/deser/impl/ObjectIdReader;

    invoke-virtual {v0, p1, p2, v1}, Lcom/fasterxml/jackson/databind/deser/impl/PropertyBasedCreator;->startBuilding(Lcom/fasterxml/jackson/core/JsonParser;Lcom/fasterxml/jackson/databind/DeserializationContext;Lcom/fasterxml/jackson/databind/deser/impl/ObjectIdReader;)Lcom/fasterxml/jackson/databind/deser/impl/PropertyValueBuffer;

    move-result-object v1

    .line 612
    new-instance v2, Lcom/fasterxml/jackson/databind/util/TokenBuffer;

    invoke-direct {v2, p1}, Lcom/fasterxml/jackson/databind/util/TokenBuffer;-><init>(Lcom/fasterxml/jackson/core/JsonParser;)V

    .line 613
    invoke-virtual {v2}, Lcom/fasterxml/jackson/databind/util/TokenBuffer;->writeStartObject()V

    .line 615
    invoke-virtual {p1}, Lcom/fasterxml/jackson/core/JsonParser;->getCurrentToken()Lcom/fasterxml/jackson/core/JsonToken;

    move-result-object v3

    .line 616
    :goto_0
    sget-object v4, Lcom/fasterxml/jackson/core/JsonToken;->FIELD_NAME:Lcom/fasterxml/jackson/core/JsonToken;

    if-ne v3, v4, :cond_7

    .line 617
    invoke-virtual {p1}, Lcom/fasterxml/jackson/core/JsonParser;->getCurrentName()Ljava/lang/String;

    move-result-object v3

    .line 618
    invoke-virtual {p1}, Lcom/fasterxml/jackson/core/JsonParser;->nextToken()Lcom/fasterxml/jackson/core/JsonToken;

    .line 620
    invoke-virtual {v0, v3}, Lcom/fasterxml/jackson/databind/deser/impl/PropertyBasedCreator;->findCreatorProperty(Ljava/lang/String;)Lcom/fasterxml/jackson/databind/deser/SettableBeanProperty;

    move-result-object v4

    if-eqz v4, :cond_2

    .line 623
    invoke-virtual {v4, p1, p2}, Lcom/fasterxml/jackson/databind/deser/SettableBeanProperty;->deserialize(Lcom/fasterxml/jackson/core/JsonParser;Lcom/fasterxml/jackson/databind/DeserializationContext;)Ljava/lang/Object;

    move-result-object v5

    .line 624
    invoke-virtual {v4}, Lcom/fasterxml/jackson/databind/deser/SettableBeanProperty;->getCreatorIndex()I

    move-result v4

    invoke-virtual {v1, v4, v5}, Lcom/fasterxml/jackson/databind/deser/impl/PropertyValueBuffer;->assignParameter(ILjava/lang/Object;)Z

    move-result v4

    if-eqz v4, :cond_6

    .line 625
    invoke-virtual {p1}, Lcom/fasterxml/jackson/core/JsonParser;->nextToken()Lcom/fasterxml/jackson/core/JsonToken;

    move-result-object v4

    .line 628
    :try_start_0
    invoke-virtual {v0, p2, v1}, Lcom/fasterxml/jackson/databind/deser/impl/PropertyBasedCreator;->build(Lcom/fasterxml/jackson/databind/DeserializationContext;Lcom/fasterxml/jackson/databind/deser/impl/PropertyValueBuffer;)Ljava/lang/Object;

    move-result-object v5
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    .line 634
    invoke-virtual {p1, v5}, Lcom/fasterxml/jackson/core/JsonParser;->setCurrentValue(Ljava/lang/Object;)V

    .line 636
    :goto_1
    sget-object v0, Lcom/fasterxml/jackson/core/JsonToken;->FIELD_NAME:Lcom/fasterxml/jackson/core/JsonToken;

    if-ne v4, v0, :cond_0

    .line 637
    invoke-virtual {p1}, Lcom/fasterxml/jackson/core/JsonParser;->nextToken()Lcom/fasterxml/jackson/core/JsonToken;

    .line 638
    invoke-virtual {v2, p1}, Lcom/fasterxml/jackson/databind/util/TokenBuffer;->copyCurrentStructure(Lcom/fasterxml/jackson/core/JsonParser;)V

    .line 639
    invoke-virtual {p1}, Lcom/fasterxml/jackson/core/JsonParser;->nextToken()Lcom/fasterxml/jackson/core/JsonToken;

    move-result-object v4

    goto :goto_1

    .line 641
    :cond_0
    invoke-virtual {v2}, Lcom/fasterxml/jackson/databind/util/TokenBuffer;->writeEndObject()V

    .line 642
    invoke-virtual {v5}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    move-result-object v0

    iget-object v1, p0, Lcom/fasterxml/jackson/databind/deser/BeanDeserializer;->_beanType:Lcom/fasterxml/jackson/databind/JavaType;

    invoke-virtual {v1}, Lcom/fasterxml/jackson/databind/JavaType;->getRawClass()Ljava/lang/Class;

    move-result-object v1

    if-ne v0, v1, :cond_1

    .line 648
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/deser/BeanDeserializer;->_unwrappedPropertyHandler:Lcom/fasterxml/jackson/databind/deser/impl/UnwrappedPropertyHandler;

    invoke-virtual {v0, p1, p2, v5, v2}, Lcom/fasterxml/jackson/databind/deser/impl/UnwrappedPropertyHandler;->processUnwrapped(Lcom/fasterxml/jackson/core/JsonParser;Lcom/fasterxml/jackson/databind/DeserializationContext;Ljava/lang/Object;Lcom/fasterxml/jackson/databind/util/TokenBuffer;)Ljava/lang/Object;

    move-result-object p1

    return-object p1

    .line 645
    :cond_1
    invoke-virtual {v2}, Lcom/fasterxml/jackson/databind/util/TokenBuffer;->close()V

    const-string p1, "Can not create polymorphic instances with unwrapped values"

    .line 646
    invoke-virtual {p2, p1}, Lcom/fasterxml/jackson/databind/DeserializationContext;->mappingException(Ljava/lang/String;)Lcom/fasterxml/jackson/databind/JsonMappingException;

    move-result-object p1

    throw p1

    :catch_0
    move-exception v4

    .line 630
    iget-object v5, p0, Lcom/fasterxml/jackson/databind/deser/BeanDeserializer;->_beanType:Lcom/fasterxml/jackson/databind/JavaType;

    invoke-virtual {v5}, Lcom/fasterxml/jackson/databind/JavaType;->getRawClass()Ljava/lang/Class;

    move-result-object v5

    invoke-virtual {p0, v4, v5, v3, p2}, Lcom/fasterxml/jackson/databind/deser/BeanDeserializer;->wrapAndThrow(Ljava/lang/Throwable;Ljava/lang/Object;Ljava/lang/String;Lcom/fasterxml/jackson/databind/DeserializationContext;)V

    goto :goto_2

    .line 653
    :cond_2
    invoke-virtual {v1, v3}, Lcom/fasterxml/jackson/databind/deser/impl/PropertyValueBuffer;->readIdProperty(Ljava/lang/String;)Z

    move-result v4

    if-eqz v4, :cond_3

    goto :goto_2

    .line 657
    :cond_3
    iget-object v4, p0, Lcom/fasterxml/jackson/databind/deser/BeanDeserializer;->_beanProperties:Lcom/fasterxml/jackson/databind/deser/impl/BeanPropertyMap;

    invoke-virtual {v4, v3}, Lcom/fasterxml/jackson/databind/deser/impl/BeanPropertyMap;->find(Ljava/lang/String;)Lcom/fasterxml/jackson/databind/deser/SettableBeanProperty;

    move-result-object v4

    if-eqz v4, :cond_4

    .line 659
    invoke-virtual {v4, p1, p2}, Lcom/fasterxml/jackson/databind/deser/SettableBeanProperty;->deserialize(Lcom/fasterxml/jackson/core/JsonParser;Lcom/fasterxml/jackson/databind/DeserializationContext;)Ljava/lang/Object;

    move-result-object v3

    invoke-virtual {v1, v4, v3}, Lcom/fasterxml/jackson/databind/deser/impl/PropertyValueBuffer;->bufferProperty(Lcom/fasterxml/jackson/databind/deser/SettableBeanProperty;Ljava/lang/Object;)V

    goto :goto_2

    .line 665
    :cond_4
    iget-object v4, p0, Lcom/fasterxml/jackson/databind/deser/BeanDeserializer;->_ignorableProps:Ljava/util/HashSet;

    if-eqz v4, :cond_5

    iget-object v4, p0, Lcom/fasterxml/jackson/databind/deser/BeanDeserializer;->_ignorableProps:Ljava/util/HashSet;

    invoke-virtual {v4, v3}, Ljava/util/HashSet;->contains(Ljava/lang/Object;)Z

    move-result v4

    if-eqz v4, :cond_5

    .line 666
    invoke-virtual {p0}, Lcom/fasterxml/jackson/databind/deser/BeanDeserializer;->handledType()Ljava/lang/Class;

    move-result-object v4

    invoke-virtual {p0, p1, p2, v4, v3}, Lcom/fasterxml/jackson/databind/deser/BeanDeserializer;->handleIgnoredProperty(Lcom/fasterxml/jackson/core/JsonParser;Lcom/fasterxml/jackson/databind/DeserializationContext;Ljava/lang/Object;Ljava/lang/String;)V

    goto :goto_2

    .line 669
    :cond_5
    invoke-virtual {v2, v3}, Lcom/fasterxml/jackson/databind/util/TokenBuffer;->writeFieldName(Ljava/lang/String;)V

    .line 670
    invoke-virtual {v2, p1}, Lcom/fasterxml/jackson/databind/util/TokenBuffer;->copyCurrentStructure(Lcom/fasterxml/jackson/core/JsonParser;)V

    .line 672
    iget-object v4, p0, Lcom/fasterxml/jackson/databind/deser/BeanDeserializer;->_anySetter:Lcom/fasterxml/jackson/databind/deser/SettableAnyProperty;

    if-eqz v4, :cond_6

    .line 673
    iget-object v4, p0, Lcom/fasterxml/jackson/databind/deser/BeanDeserializer;->_anySetter:Lcom/fasterxml/jackson/databind/deser/SettableAnyProperty;

    iget-object v5, p0, Lcom/fasterxml/jackson/databind/deser/BeanDeserializer;->_anySetter:Lcom/fasterxml/jackson/databind/deser/SettableAnyProperty;

    invoke-virtual {v5, p1, p2}, Lcom/fasterxml/jackson/databind/deser/SettableAnyProperty;->deserialize(Lcom/fasterxml/jackson/core/JsonParser;Lcom/fasterxml/jackson/databind/DeserializationContext;)Ljava/lang/Object;

    move-result-object v5

    invoke-virtual {v1, v4, v3, v5}, Lcom/fasterxml/jackson/databind/deser/impl/PropertyValueBuffer;->bufferAnyProperty(Lcom/fasterxml/jackson/databind/deser/SettableAnyProperty;Ljava/lang/String;Ljava/lang/Object;)V

    .line 616
    :cond_6
    :goto_2
    invoke-virtual {p1}, Lcom/fasterxml/jackson/core/JsonParser;->nextToken()Lcom/fasterxml/jackson/core/JsonToken;

    move-result-object v3

    goto/16 :goto_0

    .line 680
    :cond_7
    :try_start_1
    invoke-virtual {v0, p2, v1}, Lcom/fasterxml/jackson/databind/deser/impl/PropertyBasedCreator;->build(Lcom/fasterxml/jackson/databind/DeserializationContext;Lcom/fasterxml/jackson/databind/deser/impl/PropertyValueBuffer;)Ljava/lang/Object;

    move-result-object v0
    :try_end_1
    .catch Ljava/lang/Exception; {:try_start_1 .. :try_end_1} :catch_1

    .line 685
    iget-object v1, p0, Lcom/fasterxml/jackson/databind/deser/BeanDeserializer;->_unwrappedPropertyHandler:Lcom/fasterxml/jackson/databind/deser/impl/UnwrappedPropertyHandler;

    invoke-virtual {v1, p1, p2, v0, v2}, Lcom/fasterxml/jackson/databind/deser/impl/UnwrappedPropertyHandler;->processUnwrapped(Lcom/fasterxml/jackson/core/JsonParser;Lcom/fasterxml/jackson/databind/DeserializationContext;Ljava/lang/Object;Lcom/fasterxml/jackson/databind/util/TokenBuffer;)Ljava/lang/Object;

    move-result-object p1

    return-object p1

    :catch_1
    move-exception p1

    .line 682
    invoke-virtual {p0, p1, p2}, Lcom/fasterxml/jackson/databind/deser/BeanDeserializer;->wrapInstantiationProblem(Ljava/lang/Throwable;Lcom/fasterxml/jackson/databind/DeserializationContext;)V

    const/4 p1, 0x0

    return-object p1
.end method

.method protected deserializeWithExternalTypeId(Lcom/fasterxml/jackson/core/JsonParser;Lcom/fasterxml/jackson/databind/DeserializationContext;)Ljava/lang/Object;
    .locals 1
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    .line 698
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/deser/BeanDeserializer;->_propertyBasedCreator:Lcom/fasterxml/jackson/databind/deser/impl/PropertyBasedCreator;

    if-eqz v0, :cond_0

    .line 699
    invoke-virtual {p0, p1, p2}, Lcom/fasterxml/jackson/databind/deser/BeanDeserializer;->deserializeUsingPropertyBasedWithExternalTypeId(Lcom/fasterxml/jackson/core/JsonParser;Lcom/fasterxml/jackson/databind/DeserializationContext;)Ljava/lang/Object;

    move-result-object p1

    return-object p1

    .line 701
    :cond_0
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/deser/BeanDeserializer;->_valueInstantiator:Lcom/fasterxml/jackson/databind/deser/ValueInstantiator;

    invoke-virtual {v0, p2}, Lcom/fasterxml/jackson/databind/deser/ValueInstantiator;->createUsingDefault(Lcom/fasterxml/jackson/databind/DeserializationContext;)Ljava/lang/Object;

    move-result-object v0

    invoke-virtual {p0, p1, p2, v0}, Lcom/fasterxml/jackson/databind/deser/BeanDeserializer;->deserializeWithExternalTypeId(Lcom/fasterxml/jackson/core/JsonParser;Lcom/fasterxml/jackson/databind/DeserializationContext;Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object p1

    return-object p1
.end method

.method protected deserializeWithExternalTypeId(Lcom/fasterxml/jackson/core/JsonParser;Lcom/fasterxml/jackson/databind/DeserializationContext;Ljava/lang/Object;)Ljava/lang/Object;
    .locals 5
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    .line 708
    iget-boolean v0, p0, Lcom/fasterxml/jackson/databind/deser/BeanDeserializer;->_needViewProcesing:Z

    if-eqz v0, :cond_0

    invoke-virtual {p2}, Lcom/fasterxml/jackson/databind/DeserializationContext;->getActiveView()Ljava/lang/Class;

    move-result-object v0

    goto :goto_0

    :cond_0
    const/4 v0, 0x0

    .line 709
    :goto_0
    iget-object v1, p0, Lcom/fasterxml/jackson/databind/deser/BeanDeserializer;->_externalTypeIdHandler:Lcom/fasterxml/jackson/databind/deser/impl/ExternalTypeHandler;

    invoke-virtual {v1}, Lcom/fasterxml/jackson/databind/deser/impl/ExternalTypeHandler;->start()Lcom/fasterxml/jackson/databind/deser/impl/ExternalTypeHandler;

    move-result-object v1

    .line 711
    invoke-virtual {p1}, Lcom/fasterxml/jackson/core/JsonParser;->getCurrentToken()Lcom/fasterxml/jackson/core/JsonToken;

    move-result-object v2

    :goto_1
    sget-object v3, Lcom/fasterxml/jackson/core/JsonToken;->FIELD_NAME:Lcom/fasterxml/jackson/core/JsonToken;

    if-ne v2, v3, :cond_7

    .line 712
    invoke-virtual {p1}, Lcom/fasterxml/jackson/core/JsonParser;->getCurrentName()Ljava/lang/String;

    move-result-object v2

    .line 713
    invoke-virtual {p1}, Lcom/fasterxml/jackson/core/JsonParser;->nextToken()Lcom/fasterxml/jackson/core/JsonToken;

    move-result-object v3

    .line 714
    iget-object v4, p0, Lcom/fasterxml/jackson/databind/deser/BeanDeserializer;->_beanProperties:Lcom/fasterxml/jackson/databind/deser/impl/BeanPropertyMap;

    invoke-virtual {v4, v2}, Lcom/fasterxml/jackson/databind/deser/impl/BeanPropertyMap;->find(Ljava/lang/String;)Lcom/fasterxml/jackson/databind/deser/SettableBeanProperty;

    move-result-object v4

    if-eqz v4, :cond_3

    .line 717
    invoke-virtual {v3}, Lcom/fasterxml/jackson/core/JsonToken;->isScalarValue()Z

    move-result v3

    if-eqz v3, :cond_1

    .line 718
    invoke-virtual {v1, p1, p2, v2, p3}, Lcom/fasterxml/jackson/databind/deser/impl/ExternalTypeHandler;->handleTypePropertyValue(Lcom/fasterxml/jackson/core/JsonParser;Lcom/fasterxml/jackson/databind/DeserializationContext;Ljava/lang/String;Ljava/lang/Object;)Z

    :cond_1
    if-eqz v0, :cond_2

    .line 720
    invoke-virtual {v4, v0}, Lcom/fasterxml/jackson/databind/deser/SettableBeanProperty;->visibleInView(Ljava/lang/Class;)Z

    move-result v3

    if-nez v3, :cond_2

    .line 721
    invoke-virtual {p1}, Lcom/fasterxml/jackson/core/JsonParser;->skipChildren()Lcom/fasterxml/jackson/core/JsonParser;

    goto :goto_2

    .line 725
    :cond_2
    :try_start_0
    invoke-virtual {v4, p1, p2, p3}, Lcom/fasterxml/jackson/databind/deser/SettableBeanProperty;->deserializeAndSet(Lcom/fasterxml/jackson/core/JsonParser;Lcom/fasterxml/jackson/databind/DeserializationContext;Ljava/lang/Object;)V
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_2

    :catch_0
    move-exception v3

    .line 727
    invoke-virtual {p0, v3, p3, v2, p2}, Lcom/fasterxml/jackson/databind/deser/BeanDeserializer;->wrapAndThrow(Ljava/lang/Throwable;Ljava/lang/Object;Ljava/lang/String;Lcom/fasterxml/jackson/databind/DeserializationContext;)V

    goto :goto_2

    .line 732
    :cond_3
    iget-object v3, p0, Lcom/fasterxml/jackson/databind/deser/BeanDeserializer;->_ignorableProps:Ljava/util/HashSet;

    if-eqz v3, :cond_4

    iget-object v3, p0, Lcom/fasterxml/jackson/databind/deser/BeanDeserializer;->_ignorableProps:Ljava/util/HashSet;

    invoke-virtual {v3, v2}, Ljava/util/HashSet;->contains(Ljava/lang/Object;)Z

    move-result v3

    if-eqz v3, :cond_4

    .line 733
    invoke-virtual {p0, p1, p2, p3, v2}, Lcom/fasterxml/jackson/databind/deser/BeanDeserializer;->handleIgnoredProperty(Lcom/fasterxml/jackson/core/JsonParser;Lcom/fasterxml/jackson/databind/DeserializationContext;Ljava/lang/Object;Ljava/lang/String;)V

    goto :goto_2

    .line 737
    :cond_4
    invoke-virtual {v1, p1, p2, v2, p3}, Lcom/fasterxml/jackson/databind/deser/impl/ExternalTypeHandler;->handlePropertyValue(Lcom/fasterxml/jackson/core/JsonParser;Lcom/fasterxml/jackson/databind/DeserializationContext;Ljava/lang/String;Ljava/lang/Object;)Z

    move-result v3

    if-eqz v3, :cond_5

    goto :goto_2

    .line 741
    :cond_5
    iget-object v3, p0, Lcom/fasterxml/jackson/databind/deser/BeanDeserializer;->_anySetter:Lcom/fasterxml/jackson/databind/deser/SettableAnyProperty;

    if-eqz v3, :cond_6

    .line 743
    :try_start_1
    iget-object v3, p0, Lcom/fasterxml/jackson/databind/deser/BeanDeserializer;->_anySetter:Lcom/fasterxml/jackson/databind/deser/SettableAnyProperty;

    invoke-virtual {v3, p1, p2, p3, v2}, Lcom/fasterxml/jackson/databind/deser/SettableAnyProperty;->deserializeAndSet(Lcom/fasterxml/jackson/core/JsonParser;Lcom/fasterxml/jackson/databind/DeserializationContext;Ljava/lang/Object;Ljava/lang/String;)V
    :try_end_1
    .catch Ljava/lang/Exception; {:try_start_1 .. :try_end_1} :catch_1

    goto :goto_2

    :catch_1
    move-exception v3

    .line 745
    invoke-virtual {p0, v3, p3, v2, p2}, Lcom/fasterxml/jackson/databind/deser/BeanDeserializer;->wrapAndThrow(Ljava/lang/Throwable;Ljava/lang/Object;Ljava/lang/String;Lcom/fasterxml/jackson/databind/DeserializationContext;)V

    goto :goto_2

    .line 750
    :cond_6
    invoke-virtual {p0, p1, p2, p3, v2}, Lcom/fasterxml/jackson/databind/deser/BeanDeserializer;->handleUnknownProperty(Lcom/fasterxml/jackson/core/JsonParser;Lcom/fasterxml/jackson/databind/DeserializationContext;Ljava/lang/Object;Ljava/lang/String;)V

    .line 711
    :goto_2
    invoke-virtual {p1}, Lcom/fasterxml/jackson/core/JsonParser;->nextToken()Lcom/fasterxml/jackson/core/JsonToken;

    move-result-object v2

    goto :goto_1

    .line 753
    :cond_7
    invoke-virtual {v1, p1, p2, p3}, Lcom/fasterxml/jackson/databind/deser/impl/ExternalTypeHandler;->complete(Lcom/fasterxml/jackson/core/JsonParser;Lcom/fasterxml/jackson/databind/DeserializationContext;Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object p1

    return-object p1
.end method

.method protected deserializeWithUnwrapped(Lcom/fasterxml/jackson/core/JsonParser;Lcom/fasterxml/jackson/databind/DeserializationContext;)Ljava/lang/Object;
    .locals 6
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    .line 504
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/deser/BeanDeserializer;->_delegateDeserializer:Lcom/fasterxml/jackson/databind/JsonDeserializer;

    if-eqz v0, :cond_0

    .line 505
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/deser/BeanDeserializer;->_valueInstantiator:Lcom/fasterxml/jackson/databind/deser/ValueInstantiator;

    iget-object v1, p0, Lcom/fasterxml/jackson/databind/deser/BeanDeserializer;->_delegateDeserializer:Lcom/fasterxml/jackson/databind/JsonDeserializer;

    invoke-virtual {v1, p1, p2}, Lcom/fasterxml/jackson/databind/JsonDeserializer;->deserialize(Lcom/fasterxml/jackson/core/JsonParser;Lcom/fasterxml/jackson/databind/DeserializationContext;)Ljava/lang/Object;

    move-result-object p1

    invoke-virtual {v0, p2, p1}, Lcom/fasterxml/jackson/databind/deser/ValueInstantiator;->createUsingDelegate(Lcom/fasterxml/jackson/databind/DeserializationContext;Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object p1

    return-object p1

    .line 507
    :cond_0
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/deser/BeanDeserializer;->_propertyBasedCreator:Lcom/fasterxml/jackson/databind/deser/impl/PropertyBasedCreator;

    if-eqz v0, :cond_1

    .line 508
    invoke-virtual {p0, p1, p2}, Lcom/fasterxml/jackson/databind/deser/BeanDeserializer;->deserializeUsingPropertyBasedWithUnwrapped(Lcom/fasterxml/jackson/core/JsonParser;Lcom/fasterxml/jackson/databind/DeserializationContext;)Ljava/lang/Object;

    move-result-object p1

    return-object p1

    .line 510
    :cond_1
    new-instance v0, Lcom/fasterxml/jackson/databind/util/TokenBuffer;

    invoke-direct {v0, p1}, Lcom/fasterxml/jackson/databind/util/TokenBuffer;-><init>(Lcom/fasterxml/jackson/core/JsonParser;)V

    .line 511
    invoke-virtual {v0}, Lcom/fasterxml/jackson/databind/util/TokenBuffer;->writeStartObject()V

    .line 512
    iget-object v1, p0, Lcom/fasterxml/jackson/databind/deser/BeanDeserializer;->_valueInstantiator:Lcom/fasterxml/jackson/databind/deser/ValueInstantiator;

    invoke-virtual {v1, p2}, Lcom/fasterxml/jackson/databind/deser/ValueInstantiator;->createUsingDefault(Lcom/fasterxml/jackson/databind/DeserializationContext;)Ljava/lang/Object;

    move-result-object v1

    .line 515
    invoke-virtual {p1, v1}, Lcom/fasterxml/jackson/core/JsonParser;->setCurrentValue(Ljava/lang/Object;)V

    .line 517
    iget-object v2, p0, Lcom/fasterxml/jackson/databind/deser/BeanDeserializer;->_injectables:[Lcom/fasterxml/jackson/databind/deser/impl/ValueInjector;

    if-eqz v2, :cond_2

    .line 518
    invoke-virtual {p0, p2, v1}, Lcom/fasterxml/jackson/databind/deser/BeanDeserializer;->injectValues(Lcom/fasterxml/jackson/databind/DeserializationContext;Ljava/lang/Object;)V

    .line 520
    :cond_2
    iget-boolean v2, p0, Lcom/fasterxml/jackson/databind/deser/BeanDeserializer;->_needViewProcesing:Z

    if-eqz v2, :cond_3

    invoke-virtual {p2}, Lcom/fasterxml/jackson/databind/DeserializationContext;->getActiveView()Ljava/lang/Class;

    move-result-object v2

    goto :goto_0

    :cond_3
    const/4 v2, 0x0

    .line 521
    :goto_0
    invoke-virtual {p1}, Lcom/fasterxml/jackson/core/JsonParser;->getCurrentToken()Lcom/fasterxml/jackson/core/JsonToken;

    move-result-object v3

    .line 522
    :goto_1
    sget-object v4, Lcom/fasterxml/jackson/core/JsonToken;->FIELD_NAME:Lcom/fasterxml/jackson/core/JsonToken;

    if-ne v3, v4, :cond_8

    .line 523
    invoke-virtual {p1}, Lcom/fasterxml/jackson/core/JsonParser;->getCurrentName()Ljava/lang/String;

    move-result-object v3

    .line 524
    invoke-virtual {p1}, Lcom/fasterxml/jackson/core/JsonParser;->nextToken()Lcom/fasterxml/jackson/core/JsonToken;

    .line 525
    iget-object v4, p0, Lcom/fasterxml/jackson/databind/deser/BeanDeserializer;->_beanProperties:Lcom/fasterxml/jackson/databind/deser/impl/BeanPropertyMap;

    invoke-virtual {v4, v3}, Lcom/fasterxml/jackson/databind/deser/impl/BeanPropertyMap;->find(Ljava/lang/String;)Lcom/fasterxml/jackson/databind/deser/SettableBeanProperty;

    move-result-object v4

    if-eqz v4, :cond_5

    if-eqz v2, :cond_4

    .line 527
    invoke-virtual {v4, v2}, Lcom/fasterxml/jackson/databind/deser/SettableBeanProperty;->visibleInView(Ljava/lang/Class;)Z

    move-result v5

    if-nez v5, :cond_4

    .line 528
    invoke-virtual {p1}, Lcom/fasterxml/jackson/core/JsonParser;->skipChildren()Lcom/fasterxml/jackson/core/JsonParser;

    goto :goto_2

    .line 532
    :cond_4
    :try_start_0
    invoke-virtual {v4, p1, p2, v1}, Lcom/fasterxml/jackson/databind/deser/SettableBeanProperty;->deserializeAndSet(Lcom/fasterxml/jackson/core/JsonParser;Lcom/fasterxml/jackson/databind/DeserializationContext;Ljava/lang/Object;)V
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_2

    :catch_0
    move-exception v4

    .line 534
    invoke-virtual {p0, v4, v1, v3, p2}, Lcom/fasterxml/jackson/databind/deser/BeanDeserializer;->wrapAndThrow(Ljava/lang/Throwable;Ljava/lang/Object;Ljava/lang/String;Lcom/fasterxml/jackson/databind/DeserializationContext;)V

    goto :goto_2

    .line 539
    :cond_5
    iget-object v4, p0, Lcom/fasterxml/jackson/databind/deser/BeanDeserializer;->_ignorableProps:Ljava/util/HashSet;

    if-eqz v4, :cond_6

    iget-object v4, p0, Lcom/fasterxml/jackson/databind/deser/BeanDeserializer;->_ignorableProps:Ljava/util/HashSet;

    invoke-virtual {v4, v3}, Ljava/util/HashSet;->contains(Ljava/lang/Object;)Z

    move-result v4

    if-eqz v4, :cond_6

    .line 540
    invoke-virtual {p0, p1, p2, v1, v3}, Lcom/fasterxml/jackson/databind/deser/BeanDeserializer;->handleIgnoredProperty(Lcom/fasterxml/jackson/core/JsonParser;Lcom/fasterxml/jackson/databind/DeserializationContext;Ljava/lang/Object;Ljava/lang/String;)V

    goto :goto_2

    .line 544
    :cond_6
    invoke-virtual {v0, v3}, Lcom/fasterxml/jackson/databind/util/TokenBuffer;->writeFieldName(Ljava/lang/String;)V

    .line 545
    invoke-virtual {v0, p1}, Lcom/fasterxml/jackson/databind/util/TokenBuffer;->copyCurrentStructure(Lcom/fasterxml/jackson/core/JsonParser;)V

    .line 547
    iget-object v4, p0, Lcom/fasterxml/jackson/databind/deser/BeanDeserializer;->_anySetter:Lcom/fasterxml/jackson/databind/deser/SettableAnyProperty;

    if-eqz v4, :cond_7

    .line 549
    :try_start_1
    iget-object v4, p0, Lcom/fasterxml/jackson/databind/deser/BeanDeserializer;->_anySetter:Lcom/fasterxml/jackson/databind/deser/SettableAnyProperty;

    invoke-virtual {v4, p1, p2, v1, v3}, Lcom/fasterxml/jackson/databind/deser/SettableAnyProperty;->deserializeAndSet(Lcom/fasterxml/jackson/core/JsonParser;Lcom/fasterxml/jackson/databind/DeserializationContext;Ljava/lang/Object;Ljava/lang/String;)V
    :try_end_1
    .catch Ljava/lang/Exception; {:try_start_1 .. :try_end_1} :catch_1

    goto :goto_2

    :catch_1
    move-exception v4

    .line 551
    invoke-virtual {p0, v4, v1, v3, p2}, Lcom/fasterxml/jackson/databind/deser/BeanDeserializer;->wrapAndThrow(Ljava/lang/Throwable;Ljava/lang/Object;Ljava/lang/String;Lcom/fasterxml/jackson/databind/DeserializationContext;)V

    .line 522
    :cond_7
    :goto_2
    invoke-virtual {p1}, Lcom/fasterxml/jackson/core/JsonParser;->nextToken()Lcom/fasterxml/jackson/core/JsonToken;

    move-result-object v3

    goto :goto_1

    .line 556
    :cond_8
    invoke-virtual {v0}, Lcom/fasterxml/jackson/databind/util/TokenBuffer;->writeEndObject()V

    .line 557
    iget-object v2, p0, Lcom/fasterxml/jackson/databind/deser/BeanDeserializer;->_unwrappedPropertyHandler:Lcom/fasterxml/jackson/databind/deser/impl/UnwrappedPropertyHandler;

    invoke-virtual {v2, p1, p2, v1, v0}, Lcom/fasterxml/jackson/databind/deser/impl/UnwrappedPropertyHandler;->processUnwrapped(Lcom/fasterxml/jackson/core/JsonParser;Lcom/fasterxml/jackson/databind/DeserializationContext;Ljava/lang/Object;Lcom/fasterxml/jackson/databind/util/TokenBuffer;)Ljava/lang/Object;

    return-object v1
.end method

.method protected deserializeWithUnwrapped(Lcom/fasterxml/jackson/core/JsonParser;Lcom/fasterxml/jackson/databind/DeserializationContext;Ljava/lang/Object;)Ljava/lang/Object;
    .locals 5
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    .line 565
    invoke-virtual {p1}, Lcom/fasterxml/jackson/core/JsonParser;->getCurrentToken()Lcom/fasterxml/jackson/core/JsonToken;

    move-result-object v0

    .line 566
    sget-object v1, Lcom/fasterxml/jackson/core/JsonToken;->START_OBJECT:Lcom/fasterxml/jackson/core/JsonToken;

    if-ne v0, v1, :cond_0

    .line 567
    invoke-virtual {p1}, Lcom/fasterxml/jackson/core/JsonParser;->nextToken()Lcom/fasterxml/jackson/core/JsonToken;

    move-result-object v0

    .line 569
    :cond_0
    new-instance v1, Lcom/fasterxml/jackson/databind/util/TokenBuffer;

    invoke-direct {v1, p1}, Lcom/fasterxml/jackson/databind/util/TokenBuffer;-><init>(Lcom/fasterxml/jackson/core/JsonParser;)V

    .line 570
    invoke-virtual {v1}, Lcom/fasterxml/jackson/databind/util/TokenBuffer;->writeStartObject()V

    .line 571
    iget-boolean v2, p0, Lcom/fasterxml/jackson/databind/deser/BeanDeserializer;->_needViewProcesing:Z

    if-eqz v2, :cond_1

    invoke-virtual {p2}, Lcom/fasterxml/jackson/databind/DeserializationContext;->getActiveView()Ljava/lang/Class;

    move-result-object v2

    goto :goto_0

    :cond_1
    const/4 v2, 0x0

    .line 572
    :goto_0
    sget-object v3, Lcom/fasterxml/jackson/core/JsonToken;->FIELD_NAME:Lcom/fasterxml/jackson/core/JsonToken;

    if-ne v0, v3, :cond_6

    .line 573
    invoke-virtual {p1}, Lcom/fasterxml/jackson/core/JsonParser;->getCurrentName()Ljava/lang/String;

    move-result-object v0

    .line 574
    iget-object v3, p0, Lcom/fasterxml/jackson/databind/deser/BeanDeserializer;->_beanProperties:Lcom/fasterxml/jackson/databind/deser/impl/BeanPropertyMap;

    invoke-virtual {v3, v0}, Lcom/fasterxml/jackson/databind/deser/impl/BeanPropertyMap;->find(Ljava/lang/String;)Lcom/fasterxml/jackson/databind/deser/SettableBeanProperty;

    move-result-object v3

    .line 575
    invoke-virtual {p1}, Lcom/fasterxml/jackson/core/JsonParser;->nextToken()Lcom/fasterxml/jackson/core/JsonToken;

    if-eqz v3, :cond_3

    if-eqz v2, :cond_2

    .line 577
    invoke-virtual {v3, v2}, Lcom/fasterxml/jackson/databind/deser/SettableBeanProperty;->visibleInView(Ljava/lang/Class;)Z

    move-result v4

    if-nez v4, :cond_2

    .line 578
    invoke-virtual {p1}, Lcom/fasterxml/jackson/core/JsonParser;->skipChildren()Lcom/fasterxml/jackson/core/JsonParser;

    goto :goto_1

    .line 582
    :cond_2
    :try_start_0
    invoke-virtual {v3, p1, p2, p3}, Lcom/fasterxml/jackson/databind/deser/SettableBeanProperty;->deserializeAndSet(Lcom/fasterxml/jackson/core/JsonParser;Lcom/fasterxml/jackson/databind/DeserializationContext;Ljava/lang/Object;)V
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_1

    :catch_0
    move-exception v3

    .line 584
    invoke-virtual {p0, v3, p3, v0, p2}, Lcom/fasterxml/jackson/databind/deser/BeanDeserializer;->wrapAndThrow(Ljava/lang/Throwable;Ljava/lang/Object;Ljava/lang/String;Lcom/fasterxml/jackson/databind/DeserializationContext;)V

    goto :goto_1

    .line 588
    :cond_3
    iget-object v3, p0, Lcom/fasterxml/jackson/databind/deser/BeanDeserializer;->_ignorableProps:Ljava/util/HashSet;

    if-eqz v3, :cond_4

    iget-object v3, p0, Lcom/fasterxml/jackson/databind/deser/BeanDeserializer;->_ignorableProps:Ljava/util/HashSet;

    invoke-virtual {v3, v0}, Ljava/util/HashSet;->contains(Ljava/lang/Object;)Z

    move-result v3

    if-eqz v3, :cond_4

    .line 589
    invoke-virtual {p0, p1, p2, p3, v0}, Lcom/fasterxml/jackson/databind/deser/BeanDeserializer;->handleIgnoredProperty(Lcom/fasterxml/jackson/core/JsonParser;Lcom/fasterxml/jackson/databind/DeserializationContext;Ljava/lang/Object;Ljava/lang/String;)V

    goto :goto_1

    .line 593
    :cond_4
    invoke-virtual {v1, v0}, Lcom/fasterxml/jackson/databind/util/TokenBuffer;->writeFieldName(Ljava/lang/String;)V

    .line 594
    invoke-virtual {v1, p1}, Lcom/fasterxml/jackson/databind/util/TokenBuffer;->copyCurrentStructure(Lcom/fasterxml/jackson/core/JsonParser;)V

    .line 596
    iget-object v3, p0, Lcom/fasterxml/jackson/databind/deser/BeanDeserializer;->_anySetter:Lcom/fasterxml/jackson/databind/deser/SettableAnyProperty;

    if-eqz v3, :cond_5

    .line 597
    iget-object v3, p0, Lcom/fasterxml/jackson/databind/deser/BeanDeserializer;->_anySetter:Lcom/fasterxml/jackson/databind/deser/SettableAnyProperty;

    invoke-virtual {v3, p1, p2, p3, v0}, Lcom/fasterxml/jackson/databind/deser/SettableAnyProperty;->deserializeAndSet(Lcom/fasterxml/jackson/core/JsonParser;Lcom/fasterxml/jackson/databind/DeserializationContext;Ljava/lang/Object;Ljava/lang/String;)V

    .line 572
    :cond_5
    :goto_1
    invoke-virtual {p1}, Lcom/fasterxml/jackson/core/JsonParser;->nextToken()Lcom/fasterxml/jackson/core/JsonToken;

    move-result-object v0

    goto :goto_0

    .line 600
    :cond_6
    invoke-virtual {v1}, Lcom/fasterxml/jackson/databind/util/TokenBuffer;->writeEndObject()V

    .line 601
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/deser/BeanDeserializer;->_unwrappedPropertyHandler:Lcom/fasterxml/jackson/databind/deser/impl/UnwrappedPropertyHandler;

    invoke-virtual {v0, p1, p2, p3, v1}, Lcom/fasterxml/jackson/databind/deser/impl/UnwrappedPropertyHandler;->processUnwrapped(Lcom/fasterxml/jackson/core/JsonParser;Lcom/fasterxml/jackson/databind/DeserializationContext;Ljava/lang/Object;Lcom/fasterxml/jackson/databind/util/TokenBuffer;)Ljava/lang/Object;

    return-object p3
.end method

.method protected final deserializeWithView(Lcom/fasterxml/jackson/core/JsonParser;Lcom/fasterxml/jackson/databind/DeserializationContext;Ljava/lang/Object;Ljava/lang/Class;)Ljava/lang/Object;
    .locals 3
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Lcom/fasterxml/jackson/core/JsonParser;",
            "Lcom/fasterxml/jackson/databind/DeserializationContext;",
            "Ljava/lang/Object;",
            "Ljava/lang/Class<",
            "*>;)",
            "Ljava/lang/Object;"
        }
    .end annotation

    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    .line 467
    invoke-virtual {p1}, Lcom/fasterxml/jackson/core/JsonParser;->getCurrentToken()Lcom/fasterxml/jackson/core/JsonToken;

    move-result-object v0

    .line 468
    :goto_0
    sget-object v1, Lcom/fasterxml/jackson/core/JsonToken;->FIELD_NAME:Lcom/fasterxml/jackson/core/JsonToken;

    if-ne v0, v1, :cond_2

    .line 469
    invoke-virtual {p1}, Lcom/fasterxml/jackson/core/JsonParser;->getCurrentName()Ljava/lang/String;

    move-result-object v0

    .line 471
    invoke-virtual {p1}, Lcom/fasterxml/jackson/core/JsonParser;->nextToken()Lcom/fasterxml/jackson/core/JsonToken;

    .line 472
    iget-object v1, p0, Lcom/fasterxml/jackson/databind/deser/BeanDeserializer;->_beanProperties:Lcom/fasterxml/jackson/databind/deser/impl/BeanPropertyMap;

    invoke-virtual {v1, v0}, Lcom/fasterxml/jackson/databind/deser/impl/BeanPropertyMap;->find(Ljava/lang/String;)Lcom/fasterxml/jackson/databind/deser/SettableBeanProperty;

    move-result-object v1

    if-eqz v1, :cond_1

    .line 474
    invoke-virtual {v1, p4}, Lcom/fasterxml/jackson/databind/deser/SettableBeanProperty;->visibleInView(Ljava/lang/Class;)Z

    move-result v2

    if-nez v2, :cond_0

    .line 475
    invoke-virtual {p1}, Lcom/fasterxml/jackson/core/JsonParser;->skipChildren()Lcom/fasterxml/jackson/core/JsonParser;

    goto :goto_1

    .line 479
    :cond_0
    :try_start_0
    invoke-virtual {v1, p1, p2, p3}, Lcom/fasterxml/jackson/databind/deser/SettableBeanProperty;->deserializeAndSet(Lcom/fasterxml/jackson/core/JsonParser;Lcom/fasterxml/jackson/databind/DeserializationContext;Ljava/lang/Object;)V
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_1

    :catch_0
    move-exception v1

    .line 481
    invoke-virtual {p0, v1, p3, v0, p2}, Lcom/fasterxml/jackson/databind/deser/BeanDeserializer;->wrapAndThrow(Ljava/lang/Throwable;Ljava/lang/Object;Ljava/lang/String;Lcom/fasterxml/jackson/databind/DeserializationContext;)V

    goto :goto_1

    .line 485
    :cond_1
    invoke-virtual {p0, p1, p2, p3, v0}, Lcom/fasterxml/jackson/databind/deser/BeanDeserializer;->handleUnknownVanilla(Lcom/fasterxml/jackson/core/JsonParser;Lcom/fasterxml/jackson/databind/DeserializationContext;Ljava/lang/Object;Ljava/lang/String;)V

    .line 468
    :goto_1
    invoke-virtual {p1}, Lcom/fasterxml/jackson/core/JsonParser;->nextToken()Lcom/fasterxml/jackson/core/JsonToken;

    move-result-object v0

    goto :goto_0

    :cond_2
    return-object p3
.end method

.method public unwrappingDeserializer(Lcom/fasterxml/jackson/databind/util/NameTransformer;)Lcom/fasterxml/jackson/databind/JsonDeserializer;
    .locals 2
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Lcom/fasterxml/jackson/databind/util/NameTransformer;",
            ")",
            "Lcom/fasterxml/jackson/databind/JsonDeserializer<",
            "Ljava/lang/Object;",
            ">;"
        }
    .end annotation

    .line 90
    invoke-virtual {p0}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    move-result-object v0

    const-class v1, Lcom/fasterxml/jackson/databind/deser/BeanDeserializer;

    if-eq v0, v1, :cond_0

    return-object p0

    .line 97
    :cond_0
    new-instance v0, Lcom/fasterxml/jackson/databind/deser/BeanDeserializer;

    invoke-direct {v0, p0, p1}, Lcom/fasterxml/jackson/databind/deser/BeanDeserializer;-><init>(Lcom/fasterxml/jackson/databind/deser/BeanDeserializerBase;Lcom/fasterxml/jackson/databind/util/NameTransformer;)V

    return-object v0
.end method

.method public withIgnorableProperties(Ljava/util/HashSet;)Lcom/fasterxml/jackson/databind/deser/BeanDeserializer;
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/HashSet<",
            "Ljava/lang/String;",
            ">;)",
            "Lcom/fasterxml/jackson/databind/deser/BeanDeserializer;"
        }
    .end annotation

    .line 107
    new-instance v0, Lcom/fasterxml/jackson/databind/deser/BeanDeserializer;

    invoke-direct {v0, p0, p1}, Lcom/fasterxml/jackson/databind/deser/BeanDeserializer;-><init>(Lcom/fasterxml/jackson/databind/deser/BeanDeserializerBase;Ljava/util/HashSet;)V

    return-object v0
.end method

.method public bridge synthetic withIgnorableProperties(Ljava/util/HashSet;)Lcom/fasterxml/jackson/databind/deser/BeanDeserializerBase;
    .locals 0

    .line 17
    invoke-virtual {p0, p1}, Lcom/fasterxml/jackson/databind/deser/BeanDeserializer;->withIgnorableProperties(Ljava/util/HashSet;)Lcom/fasterxml/jackson/databind/deser/BeanDeserializer;

    move-result-object p1

    return-object p1
.end method

.method public withObjectIdReader(Lcom/fasterxml/jackson/databind/deser/impl/ObjectIdReader;)Lcom/fasterxml/jackson/databind/deser/BeanDeserializer;
    .locals 1

    .line 102
    new-instance v0, Lcom/fasterxml/jackson/databind/deser/BeanDeserializer;

    invoke-direct {v0, p0, p1}, Lcom/fasterxml/jackson/databind/deser/BeanDeserializer;-><init>(Lcom/fasterxml/jackson/databind/deser/BeanDeserializerBase;Lcom/fasterxml/jackson/databind/deser/impl/ObjectIdReader;)V

    return-object v0
.end method

.method public bridge synthetic withObjectIdReader(Lcom/fasterxml/jackson/databind/deser/impl/ObjectIdReader;)Lcom/fasterxml/jackson/databind/deser/BeanDeserializerBase;
    .locals 0

    .line 17
    invoke-virtual {p0, p1}, Lcom/fasterxml/jackson/databind/deser/BeanDeserializer;->withObjectIdReader(Lcom/fasterxml/jackson/databind/deser/impl/ObjectIdReader;)Lcom/fasterxml/jackson/databind/deser/BeanDeserializer;

    move-result-object p1

    return-object p1
.end method
