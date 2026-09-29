.class public Lcom/fasterxml/jackson/databind/introspect/BasicBeanDescription;
.super Lcom/fasterxml/jackson/databind/BeanDescription;
.source "BasicBeanDescription.java"


# instance fields
.field protected final _annotationIntrospector:Lcom/fasterxml/jackson/databind/AnnotationIntrospector;

.field protected _anyGetter:Lcom/fasterxml/jackson/databind/introspect/AnnotatedMember;

.field protected _anySetterMethod:Lcom/fasterxml/jackson/databind/introspect/AnnotatedMethod;

.field protected _bindings:Lcom/fasterxml/jackson/databind/type/TypeBindings;

.field protected final _classInfo:Lcom/fasterxml/jackson/databind/introspect/AnnotatedClass;

.field protected final _config:Lcom/fasterxml/jackson/databind/cfg/MapperConfig;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Lcom/fasterxml/jackson/databind/cfg/MapperConfig<",
            "*>;"
        }
    .end annotation
.end field

.field protected _ignoredPropertyNames:Ljava/util/Set;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/Set<",
            "Ljava/lang/String;",
            ">;"
        }
    .end annotation
.end field

.field protected _injectables:Ljava/util/Map;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/Map<",
            "Ljava/lang/Object;",
            "Lcom/fasterxml/jackson/databind/introspect/AnnotatedMember;",
            ">;"
        }
    .end annotation
.end field

.field protected _jsonValueMethod:Lcom/fasterxml/jackson/databind/introspect/AnnotatedMethod;

.field protected _objectIdInfo:Lcom/fasterxml/jackson/databind/introspect/ObjectIdInfo;

.field protected final _properties:Ljava/util/List;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/List<",
            "Lcom/fasterxml/jackson/databind/introspect/BeanPropertyDefinition;",
            ">;"
        }
    .end annotation
.end field


# direct methods
.method protected constructor <init>(Lcom/fasterxml/jackson/databind/cfg/MapperConfig;Lcom/fasterxml/jackson/databind/JavaType;Lcom/fasterxml/jackson/databind/introspect/AnnotatedClass;Ljava/util/List;)V
    .locals 0
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Lcom/fasterxml/jackson/databind/cfg/MapperConfig<",
            "*>;",
            "Lcom/fasterxml/jackson/databind/JavaType;",
            "Lcom/fasterxml/jackson/databind/introspect/AnnotatedClass;",
            "Ljava/util/List<",
            "Lcom/fasterxml/jackson/databind/introspect/BeanPropertyDefinition;",
            ">;)V"
        }
    .end annotation

    .line 90
    invoke-direct {p0, p2}, Lcom/fasterxml/jackson/databind/BeanDescription;-><init>(Lcom/fasterxml/jackson/databind/JavaType;)V

    .line 91
    iput-object p1, p0, Lcom/fasterxml/jackson/databind/introspect/BasicBeanDescription;->_config:Lcom/fasterxml/jackson/databind/cfg/MapperConfig;

    if-nez p1, :cond_0

    const/4 p1, 0x0

    goto :goto_0

    .line 92
    :cond_0
    invoke-virtual {p1}, Lcom/fasterxml/jackson/databind/cfg/MapperConfig;->getAnnotationIntrospector()Lcom/fasterxml/jackson/databind/AnnotationIntrospector;

    move-result-object p1

    :goto_0
    iput-object p1, p0, Lcom/fasterxml/jackson/databind/introspect/BasicBeanDescription;->_annotationIntrospector:Lcom/fasterxml/jackson/databind/AnnotationIntrospector;

    .line 93
    iput-object p3, p0, Lcom/fasterxml/jackson/databind/introspect/BasicBeanDescription;->_classInfo:Lcom/fasterxml/jackson/databind/introspect/AnnotatedClass;

    .line 94
    iput-object p4, p0, Lcom/fasterxml/jackson/databind/introspect/BasicBeanDescription;->_properties:Ljava/util/List;

    return-void
.end method

.method protected constructor <init>(Lcom/fasterxml/jackson/databind/introspect/POJOPropertiesCollector;)V
    .locals 4

    .line 99
    invoke-virtual {p1}, Lcom/fasterxml/jackson/databind/introspect/POJOPropertiesCollector;->getConfig()Lcom/fasterxml/jackson/databind/cfg/MapperConfig;

    move-result-object v0

    invoke-virtual {p1}, Lcom/fasterxml/jackson/databind/introspect/POJOPropertiesCollector;->getType()Lcom/fasterxml/jackson/databind/JavaType;

    move-result-object v1

    invoke-virtual {p1}, Lcom/fasterxml/jackson/databind/introspect/POJOPropertiesCollector;->getClassDef()Lcom/fasterxml/jackson/databind/introspect/AnnotatedClass;

    move-result-object v2

    invoke-virtual {p1}, Lcom/fasterxml/jackson/databind/introspect/POJOPropertiesCollector;->getProperties()Ljava/util/List;

    move-result-object v3

    invoke-direct {p0, v0, v1, v2, v3}, Lcom/fasterxml/jackson/databind/introspect/BasicBeanDescription;-><init>(Lcom/fasterxml/jackson/databind/cfg/MapperConfig;Lcom/fasterxml/jackson/databind/JavaType;Lcom/fasterxml/jackson/databind/introspect/AnnotatedClass;Ljava/util/List;)V

    .line 100
    invoke-virtual {p1}, Lcom/fasterxml/jackson/databind/introspect/POJOPropertiesCollector;->getObjectIdInfo()Lcom/fasterxml/jackson/databind/introspect/ObjectIdInfo;

    move-result-object p1

    iput-object p1, p0, Lcom/fasterxml/jackson/databind/introspect/BasicBeanDescription;->_objectIdInfo:Lcom/fasterxml/jackson/databind/introspect/ObjectIdInfo;

    return-void
.end method

.method public static forDeserialization(Lcom/fasterxml/jackson/databind/introspect/POJOPropertiesCollector;)Lcom/fasterxml/jackson/databind/introspect/BasicBeanDescription;
    .locals 2

    .line 109
    new-instance v0, Lcom/fasterxml/jackson/databind/introspect/BasicBeanDescription;

    invoke-direct {v0, p0}, Lcom/fasterxml/jackson/databind/introspect/BasicBeanDescription;-><init>(Lcom/fasterxml/jackson/databind/introspect/POJOPropertiesCollector;)V

    .line 110
    invoke-virtual {p0}, Lcom/fasterxml/jackson/databind/introspect/POJOPropertiesCollector;->getAnySetterMethod()Lcom/fasterxml/jackson/databind/introspect/AnnotatedMethod;

    move-result-object v1

    iput-object v1, v0, Lcom/fasterxml/jackson/databind/introspect/BasicBeanDescription;->_anySetterMethod:Lcom/fasterxml/jackson/databind/introspect/AnnotatedMethod;

    .line 111
    invoke-virtual {p0}, Lcom/fasterxml/jackson/databind/introspect/POJOPropertiesCollector;->getIgnoredPropertyNames()Ljava/util/Set;

    move-result-object v1

    iput-object v1, v0, Lcom/fasterxml/jackson/databind/introspect/BasicBeanDescription;->_ignoredPropertyNames:Ljava/util/Set;

    .line 112
    invoke-virtual {p0}, Lcom/fasterxml/jackson/databind/introspect/POJOPropertiesCollector;->getInjectables()Ljava/util/Map;

    move-result-object v1

    iput-object v1, v0, Lcom/fasterxml/jackson/databind/introspect/BasicBeanDescription;->_injectables:Ljava/util/Map;

    .line 113
    invoke-virtual {p0}, Lcom/fasterxml/jackson/databind/introspect/POJOPropertiesCollector;->getJsonValueMethod()Lcom/fasterxml/jackson/databind/introspect/AnnotatedMethod;

    move-result-object p0

    iput-object p0, v0, Lcom/fasterxml/jackson/databind/introspect/BasicBeanDescription;->_jsonValueMethod:Lcom/fasterxml/jackson/databind/introspect/AnnotatedMethod;

    return-object v0
.end method

.method public static forOtherUse(Lcom/fasterxml/jackson/databind/cfg/MapperConfig;Lcom/fasterxml/jackson/databind/JavaType;Lcom/fasterxml/jackson/databind/introspect/AnnotatedClass;)Lcom/fasterxml/jackson/databind/introspect/BasicBeanDescription;
    .locals 2
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Lcom/fasterxml/jackson/databind/cfg/MapperConfig<",
            "*>;",
            "Lcom/fasterxml/jackson/databind/JavaType;",
            "Lcom/fasterxml/jackson/databind/introspect/AnnotatedClass;",
            ")",
            "Lcom/fasterxml/jackson/databind/introspect/BasicBeanDescription;"
        }
    .end annotation

    .line 137
    new-instance v0, Lcom/fasterxml/jackson/databind/introspect/BasicBeanDescription;

    invoke-static {}, Ljava/util/Collections;->emptyList()Ljava/util/List;

    move-result-object v1

    invoke-direct {v0, p0, p1, p2, v1}, Lcom/fasterxml/jackson/databind/introspect/BasicBeanDescription;-><init>(Lcom/fasterxml/jackson/databind/cfg/MapperConfig;Lcom/fasterxml/jackson/databind/JavaType;Lcom/fasterxml/jackson/databind/introspect/AnnotatedClass;Ljava/util/List;)V

    return-object v0
.end method

.method public static forSerialization(Lcom/fasterxml/jackson/databind/introspect/POJOPropertiesCollector;)Lcom/fasterxml/jackson/databind/introspect/BasicBeanDescription;
    .locals 2

    .line 123
    new-instance v0, Lcom/fasterxml/jackson/databind/introspect/BasicBeanDescription;

    invoke-direct {v0, p0}, Lcom/fasterxml/jackson/databind/introspect/BasicBeanDescription;-><init>(Lcom/fasterxml/jackson/databind/introspect/POJOPropertiesCollector;)V

    .line 124
    invoke-virtual {p0}, Lcom/fasterxml/jackson/databind/introspect/POJOPropertiesCollector;->getJsonValueMethod()Lcom/fasterxml/jackson/databind/introspect/AnnotatedMethod;

    move-result-object v1

    iput-object v1, v0, Lcom/fasterxml/jackson/databind/introspect/BasicBeanDescription;->_jsonValueMethod:Lcom/fasterxml/jackson/databind/introspect/AnnotatedMethod;

    .line 125
    invoke-virtual {p0}, Lcom/fasterxml/jackson/databind/introspect/POJOPropertiesCollector;->getAnyGetter()Lcom/fasterxml/jackson/databind/introspect/AnnotatedMember;

    move-result-object p0

    iput-object p0, v0, Lcom/fasterxml/jackson/databind/introspect/BasicBeanDescription;->_anyGetter:Lcom/fasterxml/jackson/databind/introspect/AnnotatedMember;

    return-object v0
.end method


# virtual methods
.method public _createConverter(Ljava/lang/Object;)Lcom/fasterxml/jackson/databind/util/Converter;
    .locals 3
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/lang/Object;",
            ")",
            "Lcom/fasterxml/jackson/databind/util/Converter<",
            "Ljava/lang/Object;",
            "Ljava/lang/Object;",
            ">;"
        }
    .end annotation

    const/4 v0, 0x0

    if-nez p1, :cond_0

    return-object v0

    .line 634
    :cond_0
    instance-of v1, p1, Lcom/fasterxml/jackson/databind/util/Converter;

    if-eqz v1, :cond_1

    .line 635
    check-cast p1, Lcom/fasterxml/jackson/databind/util/Converter;

    return-object p1

    .line 637
    :cond_1
    instance-of v1, p1, Ljava/lang/Class;

    if-eqz v1, :cond_7

    .line 641
    check-cast p1, Ljava/lang/Class;

    .line 643
    const-class v1, Lcom/fasterxml/jackson/databind/util/Converter$None;

    if-eq p1, v1, :cond_6

    invoke-static {p1}, Lcom/fasterxml/jackson/databind/util/ClassUtil;->isBogusClass(Ljava/lang/Class;)Z

    move-result v1

    if-eqz v1, :cond_2

    goto :goto_1

    .line 646
    :cond_2
    const-class v1, Lcom/fasterxml/jackson/databind/util/Converter;

    invoke-virtual {v1, p1}, Ljava/lang/Class;->isAssignableFrom(Ljava/lang/Class;)Z

    move-result v1

    if-eqz v1, :cond_5

    .line 650
    iget-object v1, p0, Lcom/fasterxml/jackson/databind/introspect/BasicBeanDescription;->_config:Lcom/fasterxml/jackson/databind/cfg/MapperConfig;

    invoke-virtual {v1}, Lcom/fasterxml/jackson/databind/cfg/MapperConfig;->getHandlerInstantiator()Lcom/fasterxml/jackson/databind/cfg/HandlerInstantiator;

    move-result-object v1

    if-nez v1, :cond_3

    goto :goto_0

    .line 651
    :cond_3
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/introspect/BasicBeanDescription;->_config:Lcom/fasterxml/jackson/databind/cfg/MapperConfig;

    iget-object v2, p0, Lcom/fasterxml/jackson/databind/introspect/BasicBeanDescription;->_classInfo:Lcom/fasterxml/jackson/databind/introspect/AnnotatedClass;

    invoke-virtual {v1, v0, v2, p1}, Lcom/fasterxml/jackson/databind/cfg/HandlerInstantiator;->converterInstance(Lcom/fasterxml/jackson/databind/cfg/MapperConfig;Lcom/fasterxml/jackson/databind/introspect/Annotated;Ljava/lang/Class;)Lcom/fasterxml/jackson/databind/util/Converter;

    move-result-object v0

    :goto_0
    if-nez v0, :cond_4

    .line 653
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/introspect/BasicBeanDescription;->_config:Lcom/fasterxml/jackson/databind/cfg/MapperConfig;

    invoke-virtual {v0}, Lcom/fasterxml/jackson/databind/cfg/MapperConfig;->canOverrideAccessModifiers()Z

    move-result v0

    invoke-static {p1, v0}, Lcom/fasterxml/jackson/databind/util/ClassUtil;->createInstance(Ljava/lang/Class;Z)Ljava/lang/Object;

    move-result-object p1

    move-object v0, p1

    check-cast v0, Lcom/fasterxml/jackson/databind/util/Converter;

    :cond_4
    return-object v0

    .line 647
    :cond_5
    new-instance v0, Ljava/lang/IllegalStateException;

    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "AnnotationIntrospector returned Class "

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p1}, Ljava/lang/Class;->getName()Ljava/lang/String;

    move-result-object p1

    invoke-virtual {v1, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string p1, "; expected Class<Converter>"

    invoke-virtual {v1, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p1

    invoke-direct {v0, p1}, Ljava/lang/IllegalStateException;-><init>(Ljava/lang/String;)V

    throw v0

    :cond_6
    :goto_1
    return-object v0

    .line 638
    :cond_7
    new-instance v0, Ljava/lang/IllegalStateException;

    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "AnnotationIntrospector returned Converter definition of type "

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p1}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    move-result-object p1

    invoke-virtual {p1}, Ljava/lang/Class;->getName()Ljava/lang/String;

    move-result-object p1

    invoke-virtual {v1, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string p1, "; expected type Converter or Class<Converter> instead"

    invoke-virtual {v1, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p1

    invoke-direct {v0, p1}, Ljava/lang/IllegalStateException;-><init>(Ljava/lang/String;)V

    throw v0
.end method

.method protected _findCreatorPropertyName(Lcom/fasterxml/jackson/databind/introspect/AnnotatedParameter;)Lcom/fasterxml/jackson/databind/PropertyName;
    .locals 2

    .line 548
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/introspect/BasicBeanDescription;->_annotationIntrospector:Lcom/fasterxml/jackson/databind/AnnotationIntrospector;

    invoke-virtual {v0, p1}, Lcom/fasterxml/jackson/databind/AnnotationIntrospector;->findNameForDeserialization(Lcom/fasterxml/jackson/databind/introspect/Annotated;)Lcom/fasterxml/jackson/databind/PropertyName;

    move-result-object v0

    if-eqz v0, :cond_0

    .line 549
    invoke-virtual {v0}, Lcom/fasterxml/jackson/databind/PropertyName;->isEmpty()Z

    move-result v1

    if-eqz v1, :cond_1

    .line 550
    :cond_0
    iget-object v1, p0, Lcom/fasterxml/jackson/databind/introspect/BasicBeanDescription;->_annotationIntrospector:Lcom/fasterxml/jackson/databind/AnnotationIntrospector;

    invoke-virtual {v1, p1}, Lcom/fasterxml/jackson/databind/AnnotationIntrospector;->findImplicitPropertyName(Lcom/fasterxml/jackson/databind/introspect/AnnotatedMember;)Ljava/lang/String;

    move-result-object p1

    if-eqz p1, :cond_1

    .line 551
    invoke-virtual {p1}, Ljava/lang/String;->isEmpty()Z

    move-result v1

    if-nez v1, :cond_1

    .line 552
    new-instance v0, Lcom/fasterxml/jackson/databind/PropertyName;

    invoke-direct {v0, p1}, Lcom/fasterxml/jackson/databind/PropertyName;-><init>(Ljava/lang/String;)V

    :cond_1
    return-object v0
.end method

.method public _findPropertyFields(Ljava/util/Collection;Z)Ljava/util/LinkedHashMap;
    .locals 4
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/Collection<",
            "Ljava/lang/String;",
            ">;Z)",
            "Ljava/util/LinkedHashMap<",
            "Ljava/lang/String;",
            "Lcom/fasterxml/jackson/databind/introspect/AnnotatedField;",
            ">;"
        }
    .end annotation

    .line 606
    new-instance p2, Ljava/util/LinkedHashMap;

    invoke-direct {p2}, Ljava/util/LinkedHashMap;-><init>()V

    .line 607
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/introspect/BasicBeanDescription;->_properties:Ljava/util/List;

    invoke-interface {v0}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v0

    :cond_0
    :goto_0
    invoke-interface {v0}, Ljava/util/Iterator;->hasNext()Z

    move-result v1

    if-eqz v1, :cond_2

    invoke-interface {v0}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Lcom/fasterxml/jackson/databind/introspect/BeanPropertyDefinition;

    .line 608
    invoke-virtual {v1}, Lcom/fasterxml/jackson/databind/introspect/BeanPropertyDefinition;->getField()Lcom/fasterxml/jackson/databind/introspect/AnnotatedField;

    move-result-object v2

    if-eqz v2, :cond_0

    .line 610
    invoke-virtual {v1}, Lcom/fasterxml/jackson/databind/introspect/BeanPropertyDefinition;->getName()Ljava/lang/String;

    move-result-object v1

    if-eqz p1, :cond_1

    .line 612
    invoke-interface {p1, v1}, Ljava/util/Collection;->contains(Ljava/lang/Object;)Z

    move-result v3

    if-eqz v3, :cond_1

    goto :goto_0

    .line 616
    :cond_1
    invoke-virtual {p2, v1, v2}, Ljava/util/LinkedHashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    goto :goto_0

    :cond_2
    return-object p2
.end method

.method public bindingsForBeanType()Lcom/fasterxml/jackson/databind/type/TypeBindings;
    .locals 3

    .line 210
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/introspect/BasicBeanDescription;->_bindings:Lcom/fasterxml/jackson/databind/type/TypeBindings;

    if-nez v0, :cond_0

    .line 211
    new-instance v0, Lcom/fasterxml/jackson/databind/type/TypeBindings;

    iget-object v1, p0, Lcom/fasterxml/jackson/databind/introspect/BasicBeanDescription;->_config:Lcom/fasterxml/jackson/databind/cfg/MapperConfig;

    invoke-virtual {v1}, Lcom/fasterxml/jackson/databind/cfg/MapperConfig;->getTypeFactory()Lcom/fasterxml/jackson/databind/type/TypeFactory;

    move-result-object v1

    iget-object v2, p0, Lcom/fasterxml/jackson/databind/introspect/BasicBeanDescription;->_type:Lcom/fasterxml/jackson/databind/JavaType;

    invoke-direct {v0, v1, v2}, Lcom/fasterxml/jackson/databind/type/TypeBindings;-><init>(Lcom/fasterxml/jackson/databind/type/TypeFactory;Lcom/fasterxml/jackson/databind/JavaType;)V

    iput-object v0, p0, Lcom/fasterxml/jackson/databind/introspect/BasicBeanDescription;->_bindings:Lcom/fasterxml/jackson/databind/type/TypeBindings;

    .line 213
    :cond_0
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/introspect/BasicBeanDescription;->_bindings:Lcom/fasterxml/jackson/databind/type/TypeBindings;

    return-object v0
.end method

.method public findAnyGetter()Lcom/fasterxml/jackson/databind/introspect/AnnotatedMember;
    .locals 3
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/lang/IllegalArgumentException;
        }
    .end annotation

    .line 357
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/introspect/BasicBeanDescription;->_anyGetter:Lcom/fasterxml/jackson/databind/introspect/AnnotatedMember;

    if-eqz v0, :cond_1

    .line 361
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/introspect/BasicBeanDescription;->_anyGetter:Lcom/fasterxml/jackson/databind/introspect/AnnotatedMember;

    invoke-virtual {v0}, Lcom/fasterxml/jackson/databind/introspect/AnnotatedMember;->getRawType()Ljava/lang/Class;

    move-result-object v0

    .line 362
    const-class v1, Ljava/util/Map;

    invoke-virtual {v1, v0}, Ljava/lang/Class;->isAssignableFrom(Ljava/lang/Class;)Z

    move-result v0

    if-eqz v0, :cond_0

    goto :goto_0

    .line 363
    :cond_0
    new-instance v0, Ljava/lang/IllegalArgumentException;

    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "Invalid \'any-getter\' annotation on method "

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v2, p0, Lcom/fasterxml/jackson/databind/introspect/BasicBeanDescription;->_anyGetter:Lcom/fasterxml/jackson/databind/introspect/AnnotatedMember;

    invoke-virtual {v2}, Lcom/fasterxml/jackson/databind/introspect/AnnotatedMember;->getName()Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v2, "(): return type is not instance of java.util.Map"

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-direct {v0, v1}, Ljava/lang/IllegalArgumentException;-><init>(Ljava/lang/String;)V

    throw v0

    .line 366
    :cond_1
    :goto_0
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/introspect/BasicBeanDescription;->_anyGetter:Lcom/fasterxml/jackson/databind/introspect/AnnotatedMember;

    return-object v0
.end method

.method public findAnySetter()Lcom/fasterxml/jackson/databind/introspect/AnnotatedMethod;
    .locals 4
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/lang/IllegalArgumentException;
        }
    .end annotation

    .line 232
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/introspect/BasicBeanDescription;->_anySetterMethod:Lcom/fasterxml/jackson/databind/introspect/AnnotatedMethod;

    if-eqz v0, :cond_1

    .line 241
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/introspect/BasicBeanDescription;->_anySetterMethod:Lcom/fasterxml/jackson/databind/introspect/AnnotatedMethod;

    const/4 v1, 0x0

    invoke-virtual {v0, v1}, Lcom/fasterxml/jackson/databind/introspect/AnnotatedMethod;->getRawParameterType(I)Ljava/lang/Class;

    move-result-object v0

    .line 242
    const-class v1, Ljava/lang/String;

    if-eq v0, v1, :cond_1

    const-class v1, Ljava/lang/Object;

    if-ne v0, v1, :cond_0

    goto :goto_0

    .line 243
    :cond_0
    new-instance v1, Ljava/lang/IllegalArgumentException;

    new-instance v2, Ljava/lang/StringBuilder;

    invoke-direct {v2}, Ljava/lang/StringBuilder;-><init>()V

    const-string v3, "Invalid \'any-setter\' annotation on method "

    invoke-virtual {v2, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v3, p0, Lcom/fasterxml/jackson/databind/introspect/BasicBeanDescription;->_anySetterMethod:Lcom/fasterxml/jackson/databind/introspect/AnnotatedMethod;

    invoke-virtual {v3}, Lcom/fasterxml/jackson/databind/introspect/AnnotatedMethod;->getName()Ljava/lang/String;

    move-result-object v3

    invoke-virtual {v2, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v3, "(): first argument not of type String or Object, but "

    invoke-virtual {v2, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/Class;->getName()Ljava/lang/String;

    move-result-object v0

    invoke-virtual {v2, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v2}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v0

    invoke-direct {v1, v0}, Ljava/lang/IllegalArgumentException;-><init>(Ljava/lang/String;)V

    throw v1

    .line 246
    :cond_1
    :goto_0
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/introspect/BasicBeanDescription;->_anySetterMethod:Lcom/fasterxml/jackson/databind/introspect/AnnotatedMethod;

    return-object v0
.end method

.method public findBackReferenceProperties()Ljava/util/Map;
    .locals 5
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()",
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Lcom/fasterxml/jackson/databind/introspect/AnnotatedMember;",
            ">;"
        }
    .end annotation

    .line 375
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/introspect/BasicBeanDescription;->_properties:Ljava/util/List;

    invoke-interface {v0}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v0

    const/4 v1, 0x0

    :cond_0
    :goto_0
    invoke-interface {v0}, Ljava/util/Iterator;->hasNext()Z

    move-result v2

    if-eqz v2, :cond_4

    invoke-interface {v0}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Lcom/fasterxml/jackson/databind/introspect/BeanPropertyDefinition;

    .line 387
    invoke-virtual {v2}, Lcom/fasterxml/jackson/databind/introspect/BeanPropertyDefinition;->getMutator()Lcom/fasterxml/jackson/databind/introspect/AnnotatedMember;

    move-result-object v2

    if-nez v2, :cond_1

    goto :goto_0

    .line 391
    :cond_1
    iget-object v3, p0, Lcom/fasterxml/jackson/databind/introspect/BasicBeanDescription;->_annotationIntrospector:Lcom/fasterxml/jackson/databind/AnnotationIntrospector;

    invoke-virtual {v3, v2}, Lcom/fasterxml/jackson/databind/AnnotationIntrospector;->findReferenceType(Lcom/fasterxml/jackson/databind/introspect/AnnotatedMember;)Lcom/fasterxml/jackson/databind/AnnotationIntrospector$ReferenceProperty;

    move-result-object v3

    if-eqz v3, :cond_0

    .line 392
    invoke-virtual {v3}, Lcom/fasterxml/jackson/databind/AnnotationIntrospector$ReferenceProperty;->isBackReference()Z

    move-result v4

    if-eqz v4, :cond_0

    if-nez v1, :cond_2

    .line 394
    new-instance v1, Ljava/util/HashMap;

    invoke-direct {v1}, Ljava/util/HashMap;-><init>()V

    .line 396
    :cond_2
    invoke-virtual {v3}, Lcom/fasterxml/jackson/databind/AnnotationIntrospector$ReferenceProperty;->getName()Ljava/lang/String;

    move-result-object v3

    .line 397
    invoke-virtual {v1, v3, v2}, Ljava/util/HashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v2

    if-nez v2, :cond_3

    goto :goto_0

    .line 398
    :cond_3
    new-instance v0, Ljava/lang/IllegalArgumentException;

    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "Multiple back-reference properties with name \'"

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v2, "\'"

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-direct {v0, v1}, Ljava/lang/IllegalArgumentException;-><init>(Ljava/lang/String;)V

    throw v0

    :cond_4
    return-object v1
.end method

.method public findCreatorParameterNames()Ljava/util/List;
    .locals 8
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()",
            "Ljava/util/List<",
            "Lcom/fasterxml/jackson/databind/PropertyName;",
            ">;"
        }
    .end annotation

    .annotation runtime Ljava/lang/Deprecated;
    .end annotation

    const/4 v0, 0x0

    const/4 v1, 0x0

    :goto_0
    const/4 v2, 0x2

    if-ge v1, v2, :cond_6

    if-nez v1, :cond_0

    .line 525
    invoke-virtual {p0}, Lcom/fasterxml/jackson/databind/introspect/BasicBeanDescription;->getConstructors()Ljava/util/List;

    move-result-object v2

    goto :goto_1

    :cond_0
    invoke-virtual {p0}, Lcom/fasterxml/jackson/databind/introspect/BasicBeanDescription;->getFactoryMethods()Ljava/util/List;

    move-result-object v2

    .line 527
    :goto_1
    invoke-interface {v2}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v2

    :cond_1
    :goto_2
    invoke-interface {v2}, Ljava/util/Iterator;->hasNext()Z

    move-result v3

    if-eqz v3, :cond_5

    invoke-interface {v2}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v3

    check-cast v3, Lcom/fasterxml/jackson/databind/introspect/AnnotatedWithParams;

    .line 528
    invoke-virtual {v3}, Lcom/fasterxml/jackson/databind/introspect/AnnotatedWithParams;->getParameterCount()I

    move-result v4

    const/4 v5, 0x1

    if-ge v4, v5, :cond_2

    goto :goto_2

    .line 530
    :cond_2
    invoke-virtual {v3, v0}, Lcom/fasterxml/jackson/databind/introspect/AnnotatedWithParams;->getParameter(I)Lcom/fasterxml/jackson/databind/introspect/AnnotatedParameter;

    move-result-object v6

    invoke-virtual {p0, v6}, Lcom/fasterxml/jackson/databind/introspect/BasicBeanDescription;->_findCreatorPropertyName(Lcom/fasterxml/jackson/databind/introspect/AnnotatedParameter;)Lcom/fasterxml/jackson/databind/PropertyName;

    move-result-object v6

    if-eqz v6, :cond_1

    .line 531
    invoke-virtual {v6}, Lcom/fasterxml/jackson/databind/PropertyName;->isEmpty()Z

    move-result v7

    if-eqz v7, :cond_3

    goto :goto_2

    .line 534
    :cond_3
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    .line 535
    invoke-interface {v0, v6}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    :goto_3
    if-ge v5, v4, :cond_4

    .line 537
    invoke-virtual {v3, v5}, Lcom/fasterxml/jackson/databind/introspect/AnnotatedWithParams;->getParameter(I)Lcom/fasterxml/jackson/databind/introspect/AnnotatedParameter;

    move-result-object v1

    invoke-virtual {p0, v1}, Lcom/fasterxml/jackson/databind/introspect/BasicBeanDescription;->_findCreatorPropertyName(Lcom/fasterxml/jackson/databind/introspect/AnnotatedParameter;)Lcom/fasterxml/jackson/databind/PropertyName;

    move-result-object v1

    .line 538
    invoke-interface {v0, v1}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    add-int/lit8 v5, v5, 0x1

    goto :goto_3

    :cond_4
    return-object v0

    :cond_5
    add-int/lit8 v1, v1, 0x1

    goto :goto_0

    .line 543
    :cond_6
    invoke-static {}, Ljava/util/Collections;->emptyList()Ljava/util/List;

    move-result-object v0

    return-object v0
.end method

.method public findCreatorPropertyNames()Ljava/util/List;
    .locals 3
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()",
            "Ljava/util/List<",
            "Ljava/lang/String;",
            ">;"
        }
    .end annotation

    .annotation runtime Ljava/lang/Deprecated;
    .end annotation

    .line 507
    invoke-virtual {p0}, Lcom/fasterxml/jackson/databind/introspect/BasicBeanDescription;->findCreatorParameterNames()Ljava/util/List;

    move-result-object v0

    .line 508
    invoke-interface {v0}, Ljava/util/List;->isEmpty()Z

    move-result v1

    if-eqz v1, :cond_0

    .line 509
    invoke-static {}, Ljava/util/Collections;->emptyList()Ljava/util/List;

    move-result-object v0

    return-object v0

    .line 511
    :cond_0
    new-instance v1, Ljava/util/ArrayList;

    invoke-interface {v0}, Ljava/util/List;->size()I

    move-result v2

    invoke-direct {v1, v2}, Ljava/util/ArrayList;-><init>(I)V

    .line 512
    invoke-interface {v0}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v0

    :goto_0
    invoke-interface {v0}, Ljava/util/Iterator;->hasNext()Z

    move-result v2

    if-eqz v2, :cond_1

    invoke-interface {v0}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Lcom/fasterxml/jackson/databind/PropertyName;

    .line 513
    invoke-virtual {v2}, Lcom/fasterxml/jackson/databind/PropertyName;->getSimpleName()Ljava/lang/String;

    move-result-object v2

    invoke-interface {v1, v2}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    goto :goto_0

    :cond_1
    return-object v1
.end method

.method public findDefaultConstructor()Lcom/fasterxml/jackson/databind/introspect/AnnotatedConstructor;
    .locals 1

    .line 226
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/introspect/BasicBeanDescription;->_classInfo:Lcom/fasterxml/jackson/databind/introspect/AnnotatedClass;

    invoke-virtual {v0}, Lcom/fasterxml/jackson/databind/introspect/AnnotatedClass;->getDefaultConstructor()Lcom/fasterxml/jackson/databind/introspect/AnnotatedConstructor;

    move-result-object v0

    return-object v0
.end method

.method public findDeserializationConverter()Lcom/fasterxml/jackson/databind/util/Converter;
    .locals 2
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()",
            "Lcom/fasterxml/jackson/databind/util/Converter<",
            "Ljava/lang/Object;",
            "Ljava/lang/Object;",
            ">;"
        }
    .end annotation

    .line 581
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/introspect/BasicBeanDescription;->_annotationIntrospector:Lcom/fasterxml/jackson/databind/AnnotationIntrospector;

    if-nez v0, :cond_0

    const/4 v0, 0x0

    return-object v0

    .line 584
    :cond_0
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/introspect/BasicBeanDescription;->_annotationIntrospector:Lcom/fasterxml/jackson/databind/AnnotationIntrospector;

    iget-object v1, p0, Lcom/fasterxml/jackson/databind/introspect/BasicBeanDescription;->_classInfo:Lcom/fasterxml/jackson/databind/introspect/AnnotatedClass;

    invoke-virtual {v0, v1}, Lcom/fasterxml/jackson/databind/AnnotationIntrospector;->findDeserializationConverter(Lcom/fasterxml/jackson/databind/introspect/Annotated;)Ljava/lang/Object;

    move-result-object v0

    invoke-virtual {p0, v0}, Lcom/fasterxml/jackson/databind/introspect/BasicBeanDescription;->_createConverter(Ljava/lang/Object;)Lcom/fasterxml/jackson/databind/util/Converter;

    move-result-object v0

    return-object v0
.end method

.method public findExpectedFormat(Lcom/fasterxml/jackson/annotation/JsonFormat$Value;)Lcom/fasterxml/jackson/annotation/JsonFormat$Value;
    .locals 2

    .line 302
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/introspect/BasicBeanDescription;->_annotationIntrospector:Lcom/fasterxml/jackson/databind/AnnotationIntrospector;

    if-eqz v0, :cond_0

    .line 303
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/introspect/BasicBeanDescription;->_annotationIntrospector:Lcom/fasterxml/jackson/databind/AnnotationIntrospector;

    iget-object v1, p0, Lcom/fasterxml/jackson/databind/introspect/BasicBeanDescription;->_classInfo:Lcom/fasterxml/jackson/databind/introspect/AnnotatedClass;

    invoke-virtual {v0, v1}, Lcom/fasterxml/jackson/databind/AnnotationIntrospector;->findFormat(Lcom/fasterxml/jackson/databind/introspect/Annotated;)Lcom/fasterxml/jackson/annotation/JsonFormat$Value;

    move-result-object v0

    if-eqz v0, :cond_0

    return-object v0

    :cond_0
    return-object p1
.end method

.method public varargs findFactoryMethod([Ljava/lang/Class;)Ljava/lang/reflect/Method;
    .locals 6
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "([",
            "Ljava/lang/Class<",
            "*>;)",
            "Ljava/lang/reflect/Method;"
        }
    .end annotation

    .line 452
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/introspect/BasicBeanDescription;->_classInfo:Lcom/fasterxml/jackson/databind/introspect/AnnotatedClass;

    invoke-virtual {v0}, Lcom/fasterxml/jackson/databind/introspect/AnnotatedClass;->getStaticMethods()Ljava/util/List;

    move-result-object v0

    invoke-interface {v0}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v0

    :cond_0
    invoke-interface {v0}, Ljava/util/Iterator;->hasNext()Z

    move-result v1

    if-eqz v1, :cond_2

    invoke-interface {v0}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Lcom/fasterxml/jackson/databind/introspect/AnnotatedMethod;

    .line 453
    invoke-virtual {p0, v1}, Lcom/fasterxml/jackson/databind/introspect/BasicBeanDescription;->isFactoryMethod(Lcom/fasterxml/jackson/databind/introspect/AnnotatedMethod;)Z

    move-result v2

    if-eqz v2, :cond_0

    const/4 v2, 0x0

    .line 455
    invoke-virtual {v1, v2}, Lcom/fasterxml/jackson/databind/introspect/AnnotatedMethod;->getRawParameterType(I)Ljava/lang/Class;

    move-result-object v3

    .line 456
    array-length v4, p1

    :goto_0
    if-ge v2, v4, :cond_0

    aget-object v5, p1, v2

    .line 458
    invoke-virtual {v3, v5}, Ljava/lang/Class;->isAssignableFrom(Ljava/lang/Class;)Z

    move-result v5

    if-eqz v5, :cond_1

    .line 459
    invoke-virtual {v1}, Lcom/fasterxml/jackson/databind/introspect/AnnotatedMethod;->getAnnotated()Ljava/lang/reflect/Method;

    move-result-object p1

    return-object p1

    :cond_1
    add-int/lit8 v2, v2, 0x1

    goto :goto_0

    :cond_2
    const/4 p1, 0x0

    return-object p1
.end method

.method public findInjectables()Ljava/util/Map;
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()",
            "Ljava/util/Map<",
            "Ljava/lang/Object;",
            "Lcom/fasterxml/jackson/databind/introspect/AnnotatedMember;",
            ">;"
        }
    .end annotation

    .line 251
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/introspect/BasicBeanDescription;->_injectables:Ljava/util/Map;

    return-object v0
.end method

.method public findJsonValueMethod()Lcom/fasterxml/jackson/databind/introspect/AnnotatedMethod;
    .locals 1

    .line 186
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/introspect/BasicBeanDescription;->_jsonValueMethod:Lcom/fasterxml/jackson/databind/introspect/AnnotatedMethod;

    return-object v0
.end method

.method public findMethod(Ljava/lang/String;[Ljava/lang/Class;)Lcom/fasterxml/jackson/databind/introspect/AnnotatedMethod;
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/lang/String;",
            "[",
            "Ljava/lang/Class<",
            "*>;)",
            "Lcom/fasterxml/jackson/databind/introspect/AnnotatedMethod;"
        }
    .end annotation

    .line 290
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/introspect/BasicBeanDescription;->_classInfo:Lcom/fasterxml/jackson/databind/introspect/AnnotatedClass;

    invoke-virtual {v0, p1, p2}, Lcom/fasterxml/jackson/databind/introspect/AnnotatedClass;->findMethod(Ljava/lang/String;[Ljava/lang/Class;)Lcom/fasterxml/jackson/databind/introspect/AnnotatedMethod;

    move-result-object p1

    return-object p1
.end method

.method public findPOJOBuilder()Ljava/lang/Class;
    .locals 2
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()",
            "Ljava/lang/Class<",
            "*>;"
        }
    .end annotation

    .line 567
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/introspect/BasicBeanDescription;->_annotationIntrospector:Lcom/fasterxml/jackson/databind/AnnotationIntrospector;

    if-nez v0, :cond_0

    const/4 v0, 0x0

    goto :goto_0

    :cond_0
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/introspect/BasicBeanDescription;->_annotationIntrospector:Lcom/fasterxml/jackson/databind/AnnotationIntrospector;

    iget-object v1, p0, Lcom/fasterxml/jackson/databind/introspect/BasicBeanDescription;->_classInfo:Lcom/fasterxml/jackson/databind/introspect/AnnotatedClass;

    invoke-virtual {v0, v1}, Lcom/fasterxml/jackson/databind/AnnotationIntrospector;->findPOJOBuilder(Lcom/fasterxml/jackson/databind/introspect/AnnotatedClass;)Ljava/lang/Class;

    move-result-object v0

    :goto_0
    return-object v0
.end method

.method public findPOJOBuilderConfig()Lcom/fasterxml/jackson/databind/annotation/JsonPOJOBuilder$Value;
    .locals 2

    .line 574
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/introspect/BasicBeanDescription;->_annotationIntrospector:Lcom/fasterxml/jackson/databind/AnnotationIntrospector;

    if-nez v0, :cond_0

    const/4 v0, 0x0

    goto :goto_0

    :cond_0
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/introspect/BasicBeanDescription;->_annotationIntrospector:Lcom/fasterxml/jackson/databind/AnnotationIntrospector;

    iget-object v1, p0, Lcom/fasterxml/jackson/databind/introspect/BasicBeanDescription;->_classInfo:Lcom/fasterxml/jackson/databind/introspect/AnnotatedClass;

    invoke-virtual {v0, v1}, Lcom/fasterxml/jackson/databind/AnnotationIntrospector;->findPOJOBuilderConfig(Lcom/fasterxml/jackson/databind/introspect/AnnotatedClass;)Lcom/fasterxml/jackson/databind/annotation/JsonPOJOBuilder$Value;

    move-result-object v0

    :goto_0
    return-object v0
.end method

.method public findProperties()Ljava/util/List;
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()",
            "Ljava/util/List<",
            "Lcom/fasterxml/jackson/databind/introspect/BeanPropertyDefinition;",
            ">;"
        }
    .end annotation

    .line 181
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/introspect/BasicBeanDescription;->_properties:Ljava/util/List;

    return-object v0
.end method

.method public findSerializationConverter()Lcom/fasterxml/jackson/databind/util/Converter;
    .locals 2
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()",
            "Lcom/fasterxml/jackson/databind/util/Converter<",
            "Ljava/lang/Object;",
            "Ljava/lang/Object;",
            ">;"
        }
    .end annotation

    .line 320
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/introspect/BasicBeanDescription;->_annotationIntrospector:Lcom/fasterxml/jackson/databind/AnnotationIntrospector;

    if-nez v0, :cond_0

    const/4 v0, 0x0

    return-object v0

    .line 323
    :cond_0
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/introspect/BasicBeanDescription;->_annotationIntrospector:Lcom/fasterxml/jackson/databind/AnnotationIntrospector;

    iget-object v1, p0, Lcom/fasterxml/jackson/databind/introspect/BasicBeanDescription;->_classInfo:Lcom/fasterxml/jackson/databind/introspect/AnnotatedClass;

    invoke-virtual {v0, v1}, Lcom/fasterxml/jackson/databind/AnnotationIntrospector;->findSerializationConverter(Lcom/fasterxml/jackson/databind/introspect/Annotated;)Ljava/lang/Object;

    move-result-object v0

    invoke-virtual {p0, v0}, Lcom/fasterxml/jackson/databind/introspect/BasicBeanDescription;->_createConverter(Ljava/lang/Object;)Lcom/fasterxml/jackson/databind/util/Converter;

    move-result-object v0

    return-object v0
.end method

.method public findSerializationInclusion(Lcom/fasterxml/jackson/annotation/JsonInclude$Include;)Lcom/fasterxml/jackson/annotation/JsonInclude$Include;
    .locals 2

    .line 334
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/introspect/BasicBeanDescription;->_annotationIntrospector:Lcom/fasterxml/jackson/databind/AnnotationIntrospector;

    if-nez v0, :cond_0

    return-object p1

    .line 337
    :cond_0
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/introspect/BasicBeanDescription;->_annotationIntrospector:Lcom/fasterxml/jackson/databind/AnnotationIntrospector;

    iget-object v1, p0, Lcom/fasterxml/jackson/databind/introspect/BasicBeanDescription;->_classInfo:Lcom/fasterxml/jackson/databind/introspect/AnnotatedClass;

    invoke-virtual {v0, v1, p1}, Lcom/fasterxml/jackson/databind/AnnotationIntrospector;->findSerializationInclusion(Lcom/fasterxml/jackson/databind/introspect/Annotated;Lcom/fasterxml/jackson/annotation/JsonInclude$Include;)Lcom/fasterxml/jackson/annotation/JsonInclude$Include;

    move-result-object p1

    return-object p1
.end method

.method public findSerializationInclusionForContent(Lcom/fasterxml/jackson/annotation/JsonInclude$Include;)Lcom/fasterxml/jackson/annotation/JsonInclude$Include;
    .locals 2

    .line 342
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/introspect/BasicBeanDescription;->_annotationIntrospector:Lcom/fasterxml/jackson/databind/AnnotationIntrospector;

    if-nez v0, :cond_0

    return-object p1

    .line 345
    :cond_0
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/introspect/BasicBeanDescription;->_annotationIntrospector:Lcom/fasterxml/jackson/databind/AnnotationIntrospector;

    iget-object v1, p0, Lcom/fasterxml/jackson/databind/introspect/BasicBeanDescription;->_classInfo:Lcom/fasterxml/jackson/databind/introspect/AnnotatedClass;

    invoke-virtual {v0, v1, p1}, Lcom/fasterxml/jackson/databind/AnnotationIntrospector;->findSerializationInclusionForContent(Lcom/fasterxml/jackson/databind/introspect/Annotated;Lcom/fasterxml/jackson/annotation/JsonInclude$Include;)Lcom/fasterxml/jackson/annotation/JsonInclude$Include;

    move-result-object p1

    return-object p1
.end method

.method public varargs findSingleArgConstructor([Ljava/lang/Class;)Ljava/lang/reflect/Constructor;
    .locals 6
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "([",
            "Ljava/lang/Class<",
            "*>;)",
            "Ljava/lang/reflect/Constructor<",
            "*>;"
        }
    .end annotation

    .line 431
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/introspect/BasicBeanDescription;->_classInfo:Lcom/fasterxml/jackson/databind/introspect/AnnotatedClass;

    invoke-virtual {v0}, Lcom/fasterxml/jackson/databind/introspect/AnnotatedClass;->getConstructors()Ljava/util/List;

    move-result-object v0

    invoke-interface {v0}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v0

    :cond_0
    invoke-interface {v0}, Ljava/util/Iterator;->hasNext()Z

    move-result v1

    if-eqz v1, :cond_2

    invoke-interface {v0}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Lcom/fasterxml/jackson/databind/introspect/AnnotatedConstructor;

    .line 436
    invoke-virtual {v1}, Lcom/fasterxml/jackson/databind/introspect/AnnotatedConstructor;->getParameterCount()I

    move-result v2

    const/4 v3, 0x1

    if-ne v2, v3, :cond_0

    const/4 v2, 0x0

    .line 437
    invoke-virtual {v1, v2}, Lcom/fasterxml/jackson/databind/introspect/AnnotatedConstructor;->getRawParameterType(I)Ljava/lang/Class;

    move-result-object v3

    .line 438
    array-length v4, p1

    :goto_0
    if-ge v2, v4, :cond_0

    aget-object v5, p1, v2

    if-ne v5, v3, :cond_1

    .line 440
    invoke-virtual {v1}, Lcom/fasterxml/jackson/databind/introspect/AnnotatedConstructor;->getAnnotated()Ljava/lang/reflect/Constructor;

    move-result-object p1

    return-object p1

    :cond_1
    add-int/lit8 v2, v2, 0x1

    goto :goto_0

    :cond_2
    const/4 p1, 0x0

    return-object p1
.end method

.method public getClassAnnotations()Lcom/fasterxml/jackson/databind/util/Annotations;
    .locals 1

    .line 204
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/introspect/BasicBeanDescription;->_classInfo:Lcom/fasterxml/jackson/databind/introspect/AnnotatedClass;

    invoke-virtual {v0}, Lcom/fasterxml/jackson/databind/introspect/AnnotatedClass;->getAnnotations()Lcom/fasterxml/jackson/databind/util/Annotations;

    move-result-object v0

    return-object v0
.end method

.method public getClassInfo()Lcom/fasterxml/jackson/databind/introspect/AnnotatedClass;
    .locals 1

    .line 174
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/introspect/BasicBeanDescription;->_classInfo:Lcom/fasterxml/jackson/databind/introspect/AnnotatedClass;

    return-object v0
.end method

.method public getConstructors()Ljava/util/List;
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()",
            "Ljava/util/List<",
            "Lcom/fasterxml/jackson/databind/introspect/AnnotatedConstructor;",
            ">;"
        }
    .end annotation

    .line 256
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/introspect/BasicBeanDescription;->_classInfo:Lcom/fasterxml/jackson/databind/introspect/AnnotatedClass;

    invoke-virtual {v0}, Lcom/fasterxml/jackson/databind/introspect/AnnotatedClass;->getConstructors()Ljava/util/List;

    move-result-object v0

    return-object v0
.end method

.method public getFactoryMethods()Ljava/util/List;
    .locals 4
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()",
            "Ljava/util/List<",
            "Lcom/fasterxml/jackson/databind/introspect/AnnotatedMethod;",
            ">;"
        }
    .end annotation

    .line 415
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/introspect/BasicBeanDescription;->_classInfo:Lcom/fasterxml/jackson/databind/introspect/AnnotatedClass;

    invoke-virtual {v0}, Lcom/fasterxml/jackson/databind/introspect/AnnotatedClass;->getStaticMethods()Ljava/util/List;

    move-result-object v0

    .line 416
    invoke-interface {v0}, Ljava/util/List;->isEmpty()Z

    move-result v1

    if-eqz v1, :cond_0

    return-object v0

    .line 419
    :cond_0
    new-instance v1, Ljava/util/ArrayList;

    invoke-direct {v1}, Ljava/util/ArrayList;-><init>()V

    .line 420
    invoke-interface {v0}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v0

    :cond_1
    :goto_0
    invoke-interface {v0}, Ljava/util/Iterator;->hasNext()Z

    move-result v2

    if-eqz v2, :cond_2

    invoke-interface {v0}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Lcom/fasterxml/jackson/databind/introspect/AnnotatedMethod;

    .line 421
    invoke-virtual {p0, v2}, Lcom/fasterxml/jackson/databind/introspect/BasicBeanDescription;->isFactoryMethod(Lcom/fasterxml/jackson/databind/introspect/AnnotatedMethod;)Z

    move-result v3

    if-eqz v3, :cond_1

    .line 422
    invoke-virtual {v1, v2}, Ljava/util/ArrayList;->add(Ljava/lang/Object;)Z

    goto :goto_0

    :cond_2
    return-object v1
.end method

.method public getIgnoredPropertyNames()Ljava/util/Set;
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()",
            "Ljava/util/Set<",
            "Ljava/lang/String;",
            ">;"
        }
    .end annotation

    .line 191
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/introspect/BasicBeanDescription;->_ignoredPropertyNames:Ljava/util/Set;

    if-nez v0, :cond_0

    .line 192
    invoke-static {}, Ljava/util/Collections;->emptySet()Ljava/util/Set;

    move-result-object v0

    return-object v0

    .line 194
    :cond_0
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/introspect/BasicBeanDescription;->_ignoredPropertyNames:Ljava/util/Set;

    return-object v0
.end method

.method public getObjectIdInfo()Lcom/fasterxml/jackson/databind/introspect/ObjectIdInfo;
    .locals 1

    .line 177
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/introspect/BasicBeanDescription;->_objectIdInfo:Lcom/fasterxml/jackson/databind/introspect/ObjectIdInfo;

    return-object v0
.end method

.method public hasKnownClassAnnotations()Z
    .locals 1

    .line 199
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/introspect/BasicBeanDescription;->_classInfo:Lcom/fasterxml/jackson/databind/introspect/AnnotatedClass;

    invoke-virtual {v0}, Lcom/fasterxml/jackson/databind/introspect/AnnotatedClass;->hasAnnotations()Z

    move-result v0

    return v0
.end method

.method public instantiateBean(Z)Ljava/lang/Object;
    .locals 3

    .line 262
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/introspect/BasicBeanDescription;->_classInfo:Lcom/fasterxml/jackson/databind/introspect/AnnotatedClass;

    invoke-virtual {v0}, Lcom/fasterxml/jackson/databind/introspect/AnnotatedClass;->getDefaultConstructor()Lcom/fasterxml/jackson/databind/introspect/AnnotatedConstructor;

    move-result-object v0

    if-nez v0, :cond_0

    const/4 p1, 0x0

    return-object p1

    :cond_0
    if-eqz p1, :cond_1

    .line 267
    invoke-virtual {v0}, Lcom/fasterxml/jackson/databind/introspect/AnnotatedConstructor;->fixAccess()V

    .line 270
    :cond_1
    :try_start_0
    invoke-virtual {v0}, Lcom/fasterxml/jackson/databind/introspect/AnnotatedConstructor;->getAnnotated()Ljava/lang/reflect/Constructor;

    move-result-object p1

    const/4 v0, 0x0

    new-array v0, v0, [Ljava/lang/Object;

    invoke-virtual {p1, v0}, Ljava/lang/reflect/Constructor;->newInstance([Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object p1
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    return-object p1

    :catch_0
    move-exception p1

    .line 273
    :goto_0
    invoke-virtual {p1}, Ljava/lang/Throwable;->getCause()Ljava/lang/Throwable;

    move-result-object v0

    if-eqz v0, :cond_2

    .line 274
    invoke-virtual {p1}, Ljava/lang/Throwable;->getCause()Ljava/lang/Throwable;

    move-result-object p1

    goto :goto_0

    .line 276
    :cond_2
    instance-of v0, p1, Ljava/lang/Error;

    if-nez v0, :cond_4

    .line 277
    instance-of v0, p1, Ljava/lang/RuntimeException;

    if-eqz v0, :cond_3

    check-cast p1, Ljava/lang/RuntimeException;

    throw p1

    .line 278
    :cond_3
    new-instance v0, Ljava/lang/IllegalArgumentException;

    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "Failed to instantiate bean of type "

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v2, p0, Lcom/fasterxml/jackson/databind/introspect/BasicBeanDescription;->_classInfo:Lcom/fasterxml/jackson/databind/introspect/AnnotatedClass;

    invoke-virtual {v2}, Lcom/fasterxml/jackson/databind/introspect/AnnotatedClass;->getAnnotated()Ljava/lang/Class;

    move-result-object v2

    invoke-virtual {v2}, Ljava/lang/Class;->getName()Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v2, ": ("

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p1}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    move-result-object v2

    invoke-virtual {v2}, Ljava/lang/Class;->getName()Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v2, ") "

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p1}, Ljava/lang/Throwable;->getMessage()Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-direct {v0, v1, p1}, Ljava/lang/IllegalArgumentException;-><init>(Ljava/lang/String;Ljava/lang/Throwable;)V

    throw v0

    .line 276
    :cond_4
    check-cast p1, Ljava/lang/Error;

    throw p1
.end method

.method protected isFactoryMethod(Lcom/fasterxml/jackson/databind/introspect/AnnotatedMethod;)Z
    .locals 4

    .line 473
    invoke-virtual {p1}, Lcom/fasterxml/jackson/databind/introspect/AnnotatedMethod;->getRawReturnType()Ljava/lang/Class;

    move-result-object v0

    .line 474
    invoke-virtual {p0}, Lcom/fasterxml/jackson/databind/introspect/BasicBeanDescription;->getBeanClass()Ljava/lang/Class;

    move-result-object v1

    invoke-virtual {v1, v0}, Ljava/lang/Class;->isAssignableFrom(Ljava/lang/Class;)Z

    move-result v0

    const/4 v1, 0x0

    if-nez v0, :cond_0

    return v1

    .line 482
    :cond_0
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/introspect/BasicBeanDescription;->_annotationIntrospector:Lcom/fasterxml/jackson/databind/AnnotationIntrospector;

    invoke-virtual {v0, p1}, Lcom/fasterxml/jackson/databind/AnnotationIntrospector;->hasCreatorAnnotation(Lcom/fasterxml/jackson/databind/introspect/Annotated;)Z

    move-result v0

    const/4 v2, 0x1

    if-eqz v0, :cond_1

    return v2

    .line 485
    :cond_1
    invoke-virtual {p1}, Lcom/fasterxml/jackson/databind/introspect/AnnotatedMethod;->getName()Ljava/lang/String;

    move-result-object v0

    const-string v3, "valueOf"

    .line 486
    invoke-virtual {v3, v0}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v3

    if-eqz v3, :cond_2

    return v2

    :cond_2
    const-string v3, "fromString"

    .line 490
    invoke-virtual {v3, v0}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v0

    if-eqz v0, :cond_4

    .line 491
    invoke-virtual {p1}, Lcom/fasterxml/jackson/databind/introspect/AnnotatedMethod;->getParameterCount()I

    move-result v0

    if-ne v2, v0, :cond_4

    .line 492
    invoke-virtual {p1, v1}, Lcom/fasterxml/jackson/databind/introspect/AnnotatedMethod;->getRawParameterType(I)Ljava/lang/Class;

    move-result-object p1

    .line 493
    const-class v0, Ljava/lang/String;

    if-eq p1, v0, :cond_3

    const-class v0, Ljava/lang/CharSequence;

    invoke-virtual {v0, p1}, Ljava/lang/Class;->isAssignableFrom(Ljava/lang/Class;)Z

    move-result p1

    if-eqz p1, :cond_4

    :cond_3
    return v2

    :cond_4
    return v1
.end method

.method public removeProperty(Ljava/lang/String;)Z
    .locals 2

    .line 156
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/introspect/BasicBeanDescription;->_properties:Ljava/util/List;

    invoke-interface {v0}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v0

    .line 157
    :cond_0
    invoke-interface {v0}, Ljava/util/Iterator;->hasNext()Z

    move-result v1

    if-eqz v1, :cond_1

    .line 158
    invoke-interface {v0}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Lcom/fasterxml/jackson/databind/introspect/BeanPropertyDefinition;

    .line 159
    invoke-virtual {v1}, Lcom/fasterxml/jackson/databind/introspect/BeanPropertyDefinition;->getName()Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v1, p1}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v1

    if-eqz v1, :cond_0

    .line 160
    invoke-interface {v0}, Ljava/util/Iterator;->remove()V

    const/4 p1, 0x1

    return p1

    :cond_1
    const/4 p1, 0x0

    return p1
.end method

.method public resolveType(Ljava/lang/reflect/Type;)Lcom/fasterxml/jackson/databind/JavaType;
    .locals 1

    if-nez p1, :cond_0

    const/4 p1, 0x0

    return-object p1

    .line 221
    :cond_0
    invoke-virtual {p0}, Lcom/fasterxml/jackson/databind/introspect/BasicBeanDescription;->bindingsForBeanType()Lcom/fasterxml/jackson/databind/type/TypeBindings;

    move-result-object v0

    invoke-virtual {v0, p1}, Lcom/fasterxml/jackson/databind/type/TypeBindings;->resolveType(Ljava/lang/reflect/Type;)Lcom/fasterxml/jackson/databind/JavaType;

    move-result-object p1

    return-object p1
.end method
