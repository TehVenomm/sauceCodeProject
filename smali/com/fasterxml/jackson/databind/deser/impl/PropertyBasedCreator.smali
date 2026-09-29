.class public final Lcom/fasterxml/jackson/databind/deser/impl/PropertyBasedCreator;
.super Ljava/lang/Object;
.source "PropertyBasedCreator.java"


# instance fields
.field protected final _defaultValues:[Ljava/lang/Object;

.field protected final _properties:Ljava/util/HashMap;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/HashMap<",
            "Ljava/lang/String;",
            "Lcom/fasterxml/jackson/databind/deser/SettableBeanProperty;",
            ">;"
        }
    .end annotation
.end field

.field protected final _propertiesWithInjectables:[Lcom/fasterxml/jackson/databind/deser/SettableBeanProperty;

.field protected final _propertyCount:I

.field protected final _valueInstantiator:Lcom/fasterxml/jackson/databind/deser/ValueInstantiator;


# direct methods
.method protected constructor <init>(Lcom/fasterxml/jackson/databind/deser/ValueInstantiator;[Lcom/fasterxml/jackson/databind/deser/SettableBeanProperty;[Ljava/lang/Object;)V
    .locals 5

    .line 61
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 62
    iput-object p1, p0, Lcom/fasterxml/jackson/databind/deser/impl/PropertyBasedCreator;->_valueInstantiator:Lcom/fasterxml/jackson/databind/deser/ValueInstantiator;

    .line 63
    new-instance p1, Ljava/util/HashMap;

    invoke-direct {p1}, Ljava/util/HashMap;-><init>()V

    iput-object p1, p0, Lcom/fasterxml/jackson/databind/deser/impl/PropertyBasedCreator;->_properties:Ljava/util/HashMap;

    .line 65
    array-length p1, p2

    .line 66
    iput p1, p0, Lcom/fasterxml/jackson/databind/deser/impl/PropertyBasedCreator;->_propertyCount:I

    const/4 v0, 0x0

    const/4 v1, 0x0

    :goto_0
    if-ge v1, p1, :cond_2

    .line 68
    aget-object v2, p2, v1

    .line 69
    iget-object v3, p0, Lcom/fasterxml/jackson/databind/deser/impl/PropertyBasedCreator;->_properties:Ljava/util/HashMap;

    invoke-virtual {v2}, Lcom/fasterxml/jackson/databind/deser/SettableBeanProperty;->getName()Ljava/lang/String;

    move-result-object v4

    invoke-virtual {v3, v4, v2}, Ljava/util/HashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 70
    invoke-virtual {v2}, Lcom/fasterxml/jackson/databind/deser/SettableBeanProperty;->getInjectableValueId()Ljava/lang/Object;

    move-result-object v3

    if-eqz v3, :cond_1

    if-nez v0, :cond_0

    .line 73
    new-array v0, p1, [Lcom/fasterxml/jackson/databind/deser/SettableBeanProperty;

    .line 75
    :cond_0
    aput-object v2, v0, v1

    :cond_1
    add-int/lit8 v1, v1, 0x1

    goto :goto_0

    .line 78
    :cond_2
    iput-object p3, p0, Lcom/fasterxml/jackson/databind/deser/impl/PropertyBasedCreator;->_defaultValues:[Ljava/lang/Object;

    .line 79
    iput-object v0, p0, Lcom/fasterxml/jackson/databind/deser/impl/PropertyBasedCreator;->_propertiesWithInjectables:[Lcom/fasterxml/jackson/databind/deser/SettableBeanProperty;

    return-void
.end method

.method public static construct(Lcom/fasterxml/jackson/databind/DeserializationContext;Lcom/fasterxml/jackson/databind/deser/ValueInstantiator;[Lcom/fasterxml/jackson/databind/deser/SettableBeanProperty;)Lcom/fasterxml/jackson/databind/deser/impl/PropertyBasedCreator;
    .locals 8
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Lcom/fasterxml/jackson/databind/JsonMappingException;
        }
    .end annotation

    .line 90
    array-length v0, p2

    .line 91
    new-array v1, v0, [Lcom/fasterxml/jackson/databind/deser/SettableBeanProperty;

    const/4 v2, 0x0

    const/4 v3, 0x0

    move-object v4, v2

    :goto_0
    if-ge v3, v0, :cond_5

    .line 94
    aget-object v5, p2, v3

    .line 95
    invoke-virtual {v5}, Lcom/fasterxml/jackson/databind/deser/SettableBeanProperty;->hasValueDeserializer()Z

    move-result v6

    if-nez v6, :cond_0

    .line 96
    invoke-virtual {v5}, Lcom/fasterxml/jackson/databind/deser/SettableBeanProperty;->getType()Lcom/fasterxml/jackson/databind/JavaType;

    move-result-object v6

    invoke-virtual {p0, v6, v5}, Lcom/fasterxml/jackson/databind/DeserializationContext;->findContextualValueDeserializer(Lcom/fasterxml/jackson/databind/JavaType;Lcom/fasterxml/jackson/databind/BeanProperty;)Lcom/fasterxml/jackson/databind/JsonDeserializer;

    move-result-object v6

    invoke-virtual {v5, v6}, Lcom/fasterxml/jackson/databind/deser/SettableBeanProperty;->withValueDeserializer(Lcom/fasterxml/jackson/databind/JsonDeserializer;)Lcom/fasterxml/jackson/databind/deser/SettableBeanProperty;

    move-result-object v5

    .line 98
    :cond_0
    aput-object v5, v1, v3

    .line 101
    invoke-virtual {v5}, Lcom/fasterxml/jackson/databind/deser/SettableBeanProperty;->getValueDeserializer()Lcom/fasterxml/jackson/databind/JsonDeserializer;

    move-result-object v6

    if-nez v6, :cond_1

    move-object v6, v2

    goto :goto_1

    .line 102
    :cond_1
    invoke-virtual {v6}, Lcom/fasterxml/jackson/databind/JsonDeserializer;->getNullValue()Ljava/lang/Object;

    move-result-object v6

    :goto_1
    if-nez v6, :cond_2

    .line 103
    invoke-virtual {v5}, Lcom/fasterxml/jackson/databind/deser/SettableBeanProperty;->getType()Lcom/fasterxml/jackson/databind/JavaType;

    move-result-object v7

    invoke-virtual {v7}, Lcom/fasterxml/jackson/databind/JavaType;->isPrimitive()Z

    move-result v7

    if-eqz v7, :cond_2

    .line 104
    invoke-virtual {v5}, Lcom/fasterxml/jackson/databind/deser/SettableBeanProperty;->getType()Lcom/fasterxml/jackson/databind/JavaType;

    move-result-object v5

    invoke-virtual {v5}, Lcom/fasterxml/jackson/databind/JavaType;->getRawClass()Ljava/lang/Class;

    move-result-object v5

    invoke-static {v5}, Lcom/fasterxml/jackson/databind/util/ClassUtil;->defaultValue(Ljava/lang/Class;)Ljava/lang/Object;

    move-result-object v6

    :cond_2
    if-eqz v6, :cond_4

    if-nez v4, :cond_3

    .line 108
    new-array v4, v0, [Ljava/lang/Object;

    .line 110
    :cond_3
    aput-object v6, v4, v3

    :cond_4
    add-int/lit8 v3, v3, 0x1

    goto :goto_0

    .line 113
    :cond_5
    new-instance p0, Lcom/fasterxml/jackson/databind/deser/impl/PropertyBasedCreator;

    invoke-direct {p0, p1, v1, v4}, Lcom/fasterxml/jackson/databind/deser/impl/PropertyBasedCreator;-><init>(Lcom/fasterxml/jackson/databind/deser/ValueInstantiator;[Lcom/fasterxml/jackson/databind/deser/SettableBeanProperty;[Ljava/lang/Object;)V

    return-object p0
.end method


# virtual methods
.method public assignDeserializer(Lcom/fasterxml/jackson/databind/deser/SettableBeanProperty;Lcom/fasterxml/jackson/databind/JsonDeserializer;)V
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Lcom/fasterxml/jackson/databind/deser/SettableBeanProperty;",
            "Lcom/fasterxml/jackson/databind/JsonDeserializer<",
            "Ljava/lang/Object;",
            ">;)V"
        }
    .end annotation

    .line 117
    invoke-virtual {p1, p2}, Lcom/fasterxml/jackson/databind/deser/SettableBeanProperty;->withValueDeserializer(Lcom/fasterxml/jackson/databind/JsonDeserializer;)Lcom/fasterxml/jackson/databind/deser/SettableBeanProperty;

    move-result-object p1

    .line 118
    iget-object p2, p0, Lcom/fasterxml/jackson/databind/deser/impl/PropertyBasedCreator;->_properties:Ljava/util/HashMap;

    invoke-virtual {p1}, Lcom/fasterxml/jackson/databind/deser/SettableBeanProperty;->getName()Ljava/lang/String;

    move-result-object v0

    invoke-virtual {p2, v0, p1}, Ljava/util/HashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    return-void
.end method

.method public build(Lcom/fasterxml/jackson/databind/DeserializationContext;Lcom/fasterxml/jackson/databind/deser/impl/PropertyValueBuffer;)Ljava/lang/Object;
    .locals 2
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    .line 167
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/deser/impl/PropertyBasedCreator;->_valueInstantiator:Lcom/fasterxml/jackson/databind/deser/ValueInstantiator;

    iget-object v1, p0, Lcom/fasterxml/jackson/databind/deser/impl/PropertyBasedCreator;->_defaultValues:[Ljava/lang/Object;

    invoke-virtual {p2, v1}, Lcom/fasterxml/jackson/databind/deser/impl/PropertyValueBuffer;->getParameters([Ljava/lang/Object;)[Ljava/lang/Object;

    move-result-object v1

    invoke-virtual {v0, p1, v1}, Lcom/fasterxml/jackson/databind/deser/ValueInstantiator;->createFromObjectWith(Lcom/fasterxml/jackson/databind/DeserializationContext;[Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v0

    .line 169
    invoke-virtual {p2, p1, v0}, Lcom/fasterxml/jackson/databind/deser/impl/PropertyValueBuffer;->handleIdValue(Lcom/fasterxml/jackson/databind/DeserializationContext;Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object p1

    .line 172
    invoke-virtual {p2}, Lcom/fasterxml/jackson/databind/deser/impl/PropertyValueBuffer;->buffered()Lcom/fasterxml/jackson/databind/deser/impl/PropertyValue;

    move-result-object p2

    :goto_0
    if-eqz p2, :cond_0

    .line 173
    invoke-virtual {p2, p1}, Lcom/fasterxml/jackson/databind/deser/impl/PropertyValue;->assign(Ljava/lang/Object;)V

    .line 172
    iget-object p2, p2, Lcom/fasterxml/jackson/databind/deser/impl/PropertyValue;->next:Lcom/fasterxml/jackson/databind/deser/impl/PropertyValue;

    goto :goto_0

    :cond_0
    return-object p1
.end method

.method public findCreatorProperty(I)Lcom/fasterxml/jackson/databind/deser/SettableBeanProperty;
    .locals 3

    .line 136
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/deser/impl/PropertyBasedCreator;->_properties:Ljava/util/HashMap;

    invoke-virtual {v0}, Ljava/util/HashMap;->values()Ljava/util/Collection;

    move-result-object v0

    invoke-interface {v0}, Ljava/util/Collection;->iterator()Ljava/util/Iterator;

    move-result-object v0

    :cond_0
    invoke-interface {v0}, Ljava/util/Iterator;->hasNext()Z

    move-result v1

    if-eqz v1, :cond_1

    invoke-interface {v0}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Lcom/fasterxml/jackson/databind/deser/SettableBeanProperty;

    .line 137
    invoke-virtual {v1}, Lcom/fasterxml/jackson/databind/deser/SettableBeanProperty;->getPropertyIndex()I

    move-result v2

    if-ne v2, p1, :cond_0

    return-object v1

    :cond_1
    const/4 p1, 0x0

    return-object p1
.end method

.method public findCreatorProperty(Ljava/lang/String;)Lcom/fasterxml/jackson/databind/deser/SettableBeanProperty;
    .locals 1

    .line 132
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/deser/impl/PropertyBasedCreator;->_properties:Ljava/util/HashMap;

    invoke-virtual {v0, p1}, Ljava/util/HashMap;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object p1

    check-cast p1, Lcom/fasterxml/jackson/databind/deser/SettableBeanProperty;

    return-object p1
.end method

.method public properties()Ljava/util/Collection;
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()",
            "Ljava/util/Collection<",
            "Lcom/fasterxml/jackson/databind/deser/SettableBeanProperty;",
            ">;"
        }
    .end annotation

    .line 128
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/deser/impl/PropertyBasedCreator;->_properties:Ljava/util/HashMap;

    invoke-virtual {v0}, Ljava/util/HashMap;->values()Ljava/util/Collection;

    move-result-object v0

    return-object v0
.end method

.method public startBuilding(Lcom/fasterxml/jackson/core/JsonParser;Lcom/fasterxml/jackson/databind/DeserializationContext;Lcom/fasterxml/jackson/databind/deser/impl/ObjectIdReader;)Lcom/fasterxml/jackson/databind/deser/impl/PropertyValueBuffer;
    .locals 2

    .line 158
    new-instance v0, Lcom/fasterxml/jackson/databind/deser/impl/PropertyValueBuffer;

    iget v1, p0, Lcom/fasterxml/jackson/databind/deser/impl/PropertyBasedCreator;->_propertyCount:I

    invoke-direct {v0, p1, p2, v1, p3}, Lcom/fasterxml/jackson/databind/deser/impl/PropertyValueBuffer;-><init>(Lcom/fasterxml/jackson/core/JsonParser;Lcom/fasterxml/jackson/databind/DeserializationContext;ILcom/fasterxml/jackson/databind/deser/impl/ObjectIdReader;)V

    .line 159
    iget-object p1, p0, Lcom/fasterxml/jackson/databind/deser/impl/PropertyBasedCreator;->_propertiesWithInjectables:[Lcom/fasterxml/jackson/databind/deser/SettableBeanProperty;

    if-eqz p1, :cond_0

    .line 160
    iget-object p1, p0, Lcom/fasterxml/jackson/databind/deser/impl/PropertyBasedCreator;->_propertiesWithInjectables:[Lcom/fasterxml/jackson/databind/deser/SettableBeanProperty;

    invoke-virtual {v0, p1}, Lcom/fasterxml/jackson/databind/deser/impl/PropertyValueBuffer;->inject([Lcom/fasterxml/jackson/databind/deser/SettableBeanProperty;)V

    :cond_0
    return-object v0
.end method
