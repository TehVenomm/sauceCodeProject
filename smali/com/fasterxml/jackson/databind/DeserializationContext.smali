.class public abstract Lcom/fasterxml/jackson/databind/DeserializationContext;
.super Lcom/fasterxml/jackson/databind/DatabindContext;
.source "DeserializationContext.java"

# interfaces
.implements Ljava/io/Serializable;


# static fields
.field private static final MAX_ERROR_STR_LEN:I = 0x1f4

.field private static final serialVersionUID:J = -0x3b895e31609227cfL


# instance fields
.field protected transient _arrayBuilders:Lcom/fasterxml/jackson/databind/util/ArrayBuilders;

.field protected transient _attributes:Lcom/fasterxml/jackson/databind/cfg/ContextAttributes;

.field protected final _cache:Lcom/fasterxml/jackson/databind/deser/DeserializerCache;

.field protected final _config:Lcom/fasterxml/jackson/databind/DeserializationConfig;

.field protected _currentType:Lcom/fasterxml/jackson/databind/util/LinkedNode;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Lcom/fasterxml/jackson/databind/util/LinkedNode<",
            "Lcom/fasterxml/jackson/databind/JavaType;",
            ">;"
        }
    .end annotation
.end field

.field protected transient _dateFormat:Ljava/text/DateFormat;

.field protected final _factory:Lcom/fasterxml/jackson/databind/deser/DeserializerFactory;

.field protected final _featureFlags:I

.field protected final _injectableValues:Lcom/fasterxml/jackson/databind/InjectableValues;

.field protected transient _objectBuffer:Lcom/fasterxml/jackson/databind/util/ObjectBuffer;

.field protected transient _parser:Lcom/fasterxml/jackson/core/JsonParser;

.field protected final _view:Ljava/lang/Class;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/lang/Class<",
            "*>;"
        }
    .end annotation
.end field


# direct methods
.method protected constructor <init>(Lcom/fasterxml/jackson/databind/DeserializationContext;)V
    .locals 1

    .line 202
    invoke-direct {p0}, Lcom/fasterxml/jackson/databind/DatabindContext;-><init>()V

    .line 203
    new-instance v0, Lcom/fasterxml/jackson/databind/deser/DeserializerCache;

    invoke-direct {v0}, Lcom/fasterxml/jackson/databind/deser/DeserializerCache;-><init>()V

    iput-object v0, p0, Lcom/fasterxml/jackson/databind/DeserializationContext;->_cache:Lcom/fasterxml/jackson/databind/deser/DeserializerCache;

    .line 204
    iget-object v0, p1, Lcom/fasterxml/jackson/databind/DeserializationContext;->_factory:Lcom/fasterxml/jackson/databind/deser/DeserializerFactory;

    iput-object v0, p0, Lcom/fasterxml/jackson/databind/DeserializationContext;->_factory:Lcom/fasterxml/jackson/databind/deser/DeserializerFactory;

    .line 206
    iget-object v0, p1, Lcom/fasterxml/jackson/databind/DeserializationContext;->_config:Lcom/fasterxml/jackson/databind/DeserializationConfig;

    iput-object v0, p0, Lcom/fasterxml/jackson/databind/DeserializationContext;->_config:Lcom/fasterxml/jackson/databind/DeserializationConfig;

    .line 207
    iget v0, p1, Lcom/fasterxml/jackson/databind/DeserializationContext;->_featureFlags:I

    iput v0, p0, Lcom/fasterxml/jackson/databind/DeserializationContext;->_featureFlags:I

    .line 208
    iget-object p1, p1, Lcom/fasterxml/jackson/databind/DeserializationContext;->_view:Ljava/lang/Class;

    iput-object p1, p0, Lcom/fasterxml/jackson/databind/DeserializationContext;->_view:Ljava/lang/Class;

    const/4 p1, 0x0

    .line 209
    iput-object p1, p0, Lcom/fasterxml/jackson/databind/DeserializationContext;->_injectableValues:Lcom/fasterxml/jackson/databind/InjectableValues;

    return-void
.end method

.method protected constructor <init>(Lcom/fasterxml/jackson/databind/DeserializationContext;Lcom/fasterxml/jackson/databind/DeserializationConfig;Lcom/fasterxml/jackson/core/JsonParser;Lcom/fasterxml/jackson/databind/InjectableValues;)V
    .locals 1

    .line 187
    invoke-direct {p0}, Lcom/fasterxml/jackson/databind/DatabindContext;-><init>()V

    .line 188
    iget-object v0, p1, Lcom/fasterxml/jackson/databind/DeserializationContext;->_cache:Lcom/fasterxml/jackson/databind/deser/DeserializerCache;

    iput-object v0, p0, Lcom/fasterxml/jackson/databind/DeserializationContext;->_cache:Lcom/fasterxml/jackson/databind/deser/DeserializerCache;

    .line 189
    iget-object p1, p1, Lcom/fasterxml/jackson/databind/DeserializationContext;->_factory:Lcom/fasterxml/jackson/databind/deser/DeserializerFactory;

    iput-object p1, p0, Lcom/fasterxml/jackson/databind/DeserializationContext;->_factory:Lcom/fasterxml/jackson/databind/deser/DeserializerFactory;

    .line 191
    iput-object p2, p0, Lcom/fasterxml/jackson/databind/DeserializationContext;->_config:Lcom/fasterxml/jackson/databind/DeserializationConfig;

    .line 192
    invoke-virtual {p2}, Lcom/fasterxml/jackson/databind/DeserializationConfig;->getDeserializationFeatures()I

    move-result p1

    iput p1, p0, Lcom/fasterxml/jackson/databind/DeserializationContext;->_featureFlags:I

    .line 193
    invoke-virtual {p2}, Lcom/fasterxml/jackson/databind/DeserializationConfig;->getActiveView()Ljava/lang/Class;

    move-result-object p1

    iput-object p1, p0, Lcom/fasterxml/jackson/databind/DeserializationContext;->_view:Ljava/lang/Class;

    .line 194
    iput-object p3, p0, Lcom/fasterxml/jackson/databind/DeserializationContext;->_parser:Lcom/fasterxml/jackson/core/JsonParser;

    .line 195
    iput-object p4, p0, Lcom/fasterxml/jackson/databind/DeserializationContext;->_injectableValues:Lcom/fasterxml/jackson/databind/InjectableValues;

    .line 196
    invoke-virtual {p2}, Lcom/fasterxml/jackson/databind/DeserializationConfig;->getAttributes()Lcom/fasterxml/jackson/databind/cfg/ContextAttributes;

    move-result-object p1

    iput-object p1, p0, Lcom/fasterxml/jackson/databind/DeserializationContext;->_attributes:Lcom/fasterxml/jackson/databind/cfg/ContextAttributes;

    return-void
.end method

.method protected constructor <init>(Lcom/fasterxml/jackson/databind/DeserializationContext;Lcom/fasterxml/jackson/databind/deser/DeserializerFactory;)V
    .locals 1

    .line 169
    invoke-direct {p0}, Lcom/fasterxml/jackson/databind/DatabindContext;-><init>()V

    .line 170
    iget-object v0, p1, Lcom/fasterxml/jackson/databind/DeserializationContext;->_cache:Lcom/fasterxml/jackson/databind/deser/DeserializerCache;

    iput-object v0, p0, Lcom/fasterxml/jackson/databind/DeserializationContext;->_cache:Lcom/fasterxml/jackson/databind/deser/DeserializerCache;

    .line 171
    iput-object p2, p0, Lcom/fasterxml/jackson/databind/DeserializationContext;->_factory:Lcom/fasterxml/jackson/databind/deser/DeserializerFactory;

    .line 173
    iget-object p2, p1, Lcom/fasterxml/jackson/databind/DeserializationContext;->_config:Lcom/fasterxml/jackson/databind/DeserializationConfig;

    iput-object p2, p0, Lcom/fasterxml/jackson/databind/DeserializationContext;->_config:Lcom/fasterxml/jackson/databind/DeserializationConfig;

    .line 174
    iget p2, p1, Lcom/fasterxml/jackson/databind/DeserializationContext;->_featureFlags:I

    iput p2, p0, Lcom/fasterxml/jackson/databind/DeserializationContext;->_featureFlags:I

    .line 175
    iget-object p2, p1, Lcom/fasterxml/jackson/databind/DeserializationContext;->_view:Ljava/lang/Class;

    iput-object p2, p0, Lcom/fasterxml/jackson/databind/DeserializationContext;->_view:Ljava/lang/Class;

    .line 176
    iget-object p2, p1, Lcom/fasterxml/jackson/databind/DeserializationContext;->_parser:Lcom/fasterxml/jackson/core/JsonParser;

    iput-object p2, p0, Lcom/fasterxml/jackson/databind/DeserializationContext;->_parser:Lcom/fasterxml/jackson/core/JsonParser;

    .line 177
    iget-object p2, p1, Lcom/fasterxml/jackson/databind/DeserializationContext;->_injectableValues:Lcom/fasterxml/jackson/databind/InjectableValues;

    iput-object p2, p0, Lcom/fasterxml/jackson/databind/DeserializationContext;->_injectableValues:Lcom/fasterxml/jackson/databind/InjectableValues;

    .line 178
    iget-object p1, p1, Lcom/fasterxml/jackson/databind/DeserializationContext;->_attributes:Lcom/fasterxml/jackson/databind/cfg/ContextAttributes;

    iput-object p1, p0, Lcom/fasterxml/jackson/databind/DeserializationContext;->_attributes:Lcom/fasterxml/jackson/databind/cfg/ContextAttributes;

    return-void
.end method

.method protected constructor <init>(Lcom/fasterxml/jackson/databind/deser/DeserializerFactory;)V
    .locals 1

    const/4 v0, 0x0

    .line 148
    invoke-direct {p0, p1, v0}, Lcom/fasterxml/jackson/databind/DeserializationContext;-><init>(Lcom/fasterxml/jackson/databind/deser/DeserializerFactory;Lcom/fasterxml/jackson/databind/deser/DeserializerCache;)V

    return-void
.end method

.method protected constructor <init>(Lcom/fasterxml/jackson/databind/deser/DeserializerFactory;Lcom/fasterxml/jackson/databind/deser/DeserializerCache;)V
    .locals 0

    .line 153
    invoke-direct {p0}, Lcom/fasterxml/jackson/databind/DatabindContext;-><init>()V

    if-eqz p1, :cond_1

    .line 157
    iput-object p1, p0, Lcom/fasterxml/jackson/databind/DeserializationContext;->_factory:Lcom/fasterxml/jackson/databind/deser/DeserializerFactory;

    if-nez p2, :cond_0

    .line 158
    new-instance p2, Lcom/fasterxml/jackson/databind/deser/DeserializerCache;

    invoke-direct {p2}, Lcom/fasterxml/jackson/databind/deser/DeserializerCache;-><init>()V

    :cond_0
    iput-object p2, p0, Lcom/fasterxml/jackson/databind/DeserializationContext;->_cache:Lcom/fasterxml/jackson/databind/deser/DeserializerCache;

    const/4 p1, 0x0

    .line 160
    iput p1, p0, Lcom/fasterxml/jackson/databind/DeserializationContext;->_featureFlags:I

    const/4 p1, 0x0

    .line 161
    iput-object p1, p0, Lcom/fasterxml/jackson/databind/DeserializationContext;->_config:Lcom/fasterxml/jackson/databind/DeserializationConfig;

    .line 162
    iput-object p1, p0, Lcom/fasterxml/jackson/databind/DeserializationContext;->_injectableValues:Lcom/fasterxml/jackson/databind/InjectableValues;

    .line 163
    iput-object p1, p0, Lcom/fasterxml/jackson/databind/DeserializationContext;->_view:Ljava/lang/Class;

    .line 164
    iput-object p1, p0, Lcom/fasterxml/jackson/databind/DeserializationContext;->_attributes:Lcom/fasterxml/jackson/databind/cfg/ContextAttributes;

    return-void

    .line 155
    :cond_1
    new-instance p1, Ljava/lang/IllegalArgumentException;

    const-string p2, "Can not pass null DeserializerFactory"

    invoke-direct {p1, p2}, Ljava/lang/IllegalArgumentException;-><init>(Ljava/lang/String;)V

    throw p1
.end method


# virtual methods
.method protected _calcName(Ljava/lang/Class;)Ljava/lang/String;
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/lang/Class<",
            "*>;)",
            "Ljava/lang/String;"
        }
    .end annotation

    .line 988
    invoke-virtual {p1}, Ljava/lang/Class;->isArray()Z

    move-result v0

    if-eqz v0, :cond_0

    .line 989
    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    invoke-virtual {p1}, Ljava/lang/Class;->getComponentType()Ljava/lang/Class;

    move-result-object p1

    invoke-virtual {p0, p1}, Lcom/fasterxml/jackson/databind/DeserializationContext;->_calcName(Ljava/lang/Class;)Ljava/lang/String;

    move-result-object p1

    invoke-virtual {v0, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string p1, "[]"

    invoke-virtual {v0, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p1

    return-object p1

    .line 991
    :cond_0
    invoke-virtual {p1}, Ljava/lang/Class;->getName()Ljava/lang/String;

    move-result-object p1

    return-object p1
.end method

.method protected _desc(Ljava/lang/String;)Ljava/lang/String;
    .locals 3

    .line 1004
    invoke-virtual {p1}, Ljava/lang/String;->length()I

    move-result v0

    const/16 v1, 0x1f4

    if-le v0, v1, :cond_0

    .line 1005
    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    const/4 v2, 0x0

    invoke-virtual {p1, v2, v1}, Ljava/lang/String;->substring(II)Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v0, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v2, "]...["

    invoke-virtual {v0, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p1}, Ljava/lang/String;->length()I

    move-result v2

    sub-int/2addr v2, v1

    invoke-virtual {p1, v2}, Ljava/lang/String;->substring(I)Ljava/lang/String;

    move-result-object p1

    invoke-virtual {v0, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p1

    :cond_0
    return-object p1
.end method

.method protected _valueDesc()Ljava/lang/String;
    .locals 1

    .line 996
    :try_start_0
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/DeserializationContext;->_parser:Lcom/fasterxml/jackson/core/JsonParser;

    invoke-virtual {v0}, Lcom/fasterxml/jackson/core/JsonParser;->getText()Ljava/lang/String;

    move-result-object v0

    invoke-virtual {p0, v0}, Lcom/fasterxml/jackson/databind/DeserializationContext;->_desc(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v0
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    return-object v0

    :catch_0
    const-string v0, "[N/A]"

    return-object v0
.end method

.method public abstract checkUnresolvedObjectId()V
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Lcom/fasterxml/jackson/databind/deser/UnresolvedForwardReference;
        }
    .end annotation
.end method

.method public constructCalendar(Ljava/util/Date;)Ljava/util/Calendar;
    .locals 1

    .line 704
    invoke-virtual {p0}, Lcom/fasterxml/jackson/databind/DeserializationContext;->getTimeZone()Ljava/util/TimeZone;

    move-result-object v0

    invoke-static {v0}, Ljava/util/Calendar;->getInstance(Ljava/util/TimeZone;)Ljava/util/Calendar;

    move-result-object v0

    .line 705
    invoke-virtual {v0, p1}, Ljava/util/Calendar;->setTime(Ljava/util/Date;)V

    return-object v0
.end method

.method public final constructType(Ljava/lang/Class;)Lcom/fasterxml/jackson/databind/JavaType;
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/lang/Class<",
            "*>;)",
            "Lcom/fasterxml/jackson/databind/JavaType;"
        }
    .end annotation

    .line 508
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/DeserializationContext;->_config:Lcom/fasterxml/jackson/databind/DeserializationConfig;

    invoke-virtual {v0, p1}, Lcom/fasterxml/jackson/databind/DeserializationConfig;->constructType(Ljava/lang/Class;)Lcom/fasterxml/jackson/databind/JavaType;

    move-result-object p1

    return-object p1
.end method

.method public abstract deserializerInstance(Lcom/fasterxml/jackson/databind/introspect/Annotated;Ljava/lang/Object;)Lcom/fasterxml/jackson/databind/JsonDeserializer;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Lcom/fasterxml/jackson/databind/introspect/Annotated;",
            "Ljava/lang/Object;",
            ")",
            "Lcom/fasterxml/jackson/databind/JsonDeserializer<",
            "Ljava/lang/Object;",
            ">;"
        }
    .end annotation

    .annotation system Ldalvik/annotation/Throws;
        value = {
            Lcom/fasterxml/jackson/databind/JsonMappingException;
        }
    .end annotation
.end method

.method protected determineClassName(Ljava/lang/Object;)Ljava/lang/String;
    .locals 0

    .line 978
    invoke-static {p1}, Lcom/fasterxml/jackson/databind/util/ClassUtil;->getClassDescription(Ljava/lang/Object;)Ljava/lang/String;

    move-result-object p1

    return-object p1
.end method

.method public endOfInputException(Ljava/lang/Class;)Lcom/fasterxml/jackson/databind/JsonMappingException;
    .locals 3
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/lang/Class<",
            "*>;)",
            "Lcom/fasterxml/jackson/databind/JsonMappingException;"
        }
    .end annotation

    .line 952
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/DeserializationContext;->_parser:Lcom/fasterxml/jackson/core/JsonParser;

    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "Unexpected end-of-input when trying to deserialize a "

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p1}, Ljava/lang/Class;->getName()Ljava/lang/String;

    move-result-object p1

    invoke-virtual {v1, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p1

    invoke-static {v0, p1}, Lcom/fasterxml/jackson/databind/JsonMappingException;->from(Lcom/fasterxml/jackson/core/JsonParser;Ljava/lang/String;)Lcom/fasterxml/jackson/databind/JsonMappingException;

    move-result-object p1

    return-object p1
.end method

.method public findClass(Ljava/lang/String;)Ljava/lang/Class;
    .locals 0
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/lang/String;",
            ")",
            "Ljava/lang/Class<",
            "*>;"
        }
    .end annotation

    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/lang/ClassNotFoundException;
        }
    .end annotation

    .line 520
    invoke-static {p1}, Lcom/fasterxml/jackson/databind/util/ClassUtil;->findClass(Ljava/lang/String;)Ljava/lang/Class;

    move-result-object p1

    return-object p1
.end method

.method public final findContextualValueDeserializer(Lcom/fasterxml/jackson/databind/JavaType;Lcom/fasterxml/jackson/databind/BeanProperty;)Lcom/fasterxml/jackson/databind/JsonDeserializer;
    .locals 2
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Lcom/fasterxml/jackson/databind/JavaType;",
            "Lcom/fasterxml/jackson/databind/BeanProperty;",
            ")",
            "Lcom/fasterxml/jackson/databind/JsonDeserializer<",
            "Ljava/lang/Object;",
            ">;"
        }
    .end annotation

    .annotation system Ldalvik/annotation/Throws;
        value = {
            Lcom/fasterxml/jackson/databind/JsonMappingException;
        }
    .end annotation

    .line 406
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/DeserializationContext;->_cache:Lcom/fasterxml/jackson/databind/deser/DeserializerCache;

    iget-object v1, p0, Lcom/fasterxml/jackson/databind/DeserializationContext;->_factory:Lcom/fasterxml/jackson/databind/deser/DeserializerFactory;

    invoke-virtual {v0, p0, v1, p1}, Lcom/fasterxml/jackson/databind/deser/DeserializerCache;->findValueDeserializer(Lcom/fasterxml/jackson/databind/DeserializationContext;Lcom/fasterxml/jackson/databind/deser/DeserializerFactory;Lcom/fasterxml/jackson/databind/JavaType;)Lcom/fasterxml/jackson/databind/JsonDeserializer;

    move-result-object v0

    if-eqz v0, :cond_0

    .line 408
    invoke-virtual {p0, v0, p2, p1}, Lcom/fasterxml/jackson/databind/DeserializationContext;->handleSecondaryContextualization(Lcom/fasterxml/jackson/databind/JsonDeserializer;Lcom/fasterxml/jackson/databind/BeanProperty;Lcom/fasterxml/jackson/databind/JavaType;)Lcom/fasterxml/jackson/databind/JsonDeserializer;

    move-result-object v0

    :cond_0
    return-object v0
.end method

.method public final findInjectableValue(Ljava/lang/Object;Lcom/fasterxml/jackson/databind/BeanProperty;Ljava/lang/Object;)Ljava/lang/Object;
    .locals 1

    .line 316
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/DeserializationContext;->_injectableValues:Lcom/fasterxml/jackson/databind/InjectableValues;

    if-eqz v0, :cond_0

    .line 319
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/DeserializationContext;->_injectableValues:Lcom/fasterxml/jackson/databind/InjectableValues;

    invoke-virtual {v0, p1, p0, p2, p3}, Lcom/fasterxml/jackson/databind/InjectableValues;->findInjectableValue(Ljava/lang/Object;Lcom/fasterxml/jackson/databind/DeserializationContext;Lcom/fasterxml/jackson/databind/BeanProperty;Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object p1

    return-object p1

    .line 317
    :cond_0
    new-instance p2, Ljava/lang/IllegalStateException;

    new-instance p3, Ljava/lang/StringBuilder;

    invoke-direct {p3}, Ljava/lang/StringBuilder;-><init>()V

    const-string v0, "No \'injectableValues\' configured, can not inject value with id ["

    invoke-virtual {p3, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p3, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/Object;)Ljava/lang/StringBuilder;

    const-string p1, "]"

    invoke-virtual {p3, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p3}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p1

    invoke-direct {p2, p1}, Ljava/lang/IllegalStateException;-><init>(Ljava/lang/String;)V

    throw p2
.end method

.method public final findKeyDeserializer(Lcom/fasterxml/jackson/databind/JavaType;Lcom/fasterxml/jackson/databind/BeanProperty;)Lcom/fasterxml/jackson/databind/KeyDeserializer;
    .locals 2
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Lcom/fasterxml/jackson/databind/JsonMappingException;
        }
    .end annotation

    .line 462
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/DeserializationContext;->_cache:Lcom/fasterxml/jackson/databind/deser/DeserializerCache;

    iget-object v1, p0, Lcom/fasterxml/jackson/databind/DeserializationContext;->_factory:Lcom/fasterxml/jackson/databind/deser/DeserializerFactory;

    invoke-virtual {v0, p0, v1, p1}, Lcom/fasterxml/jackson/databind/deser/DeserializerCache;->findKeyDeserializer(Lcom/fasterxml/jackson/databind/DeserializationContext;Lcom/fasterxml/jackson/databind/deser/DeserializerFactory;Lcom/fasterxml/jackson/databind/JavaType;)Lcom/fasterxml/jackson/databind/KeyDeserializer;

    move-result-object p1

    .line 465
    instance-of v0, p1, Lcom/fasterxml/jackson/databind/deser/ContextualKeyDeserializer;

    if-eqz v0, :cond_0

    .line 466
    check-cast p1, Lcom/fasterxml/jackson/databind/deser/ContextualKeyDeserializer;

    invoke-interface {p1, p0, p2}, Lcom/fasterxml/jackson/databind/deser/ContextualKeyDeserializer;->createContextual(Lcom/fasterxml/jackson/databind/DeserializationContext;Lcom/fasterxml/jackson/databind/BeanProperty;)Lcom/fasterxml/jackson/databind/KeyDeserializer;

    move-result-object p1

    :cond_0
    return-object p1
.end method

.method public final findNonContextualValueDeserializer(Lcom/fasterxml/jackson/databind/JavaType;)Lcom/fasterxml/jackson/databind/JsonDeserializer;
    .locals 2
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Lcom/fasterxml/jackson/databind/JavaType;",
            ")",
            "Lcom/fasterxml/jackson/databind/JsonDeserializer<",
            "Ljava/lang/Object;",
            ">;"
        }
    .end annotation

    .annotation system Ldalvik/annotation/Throws;
        value = {
            Lcom/fasterxml/jackson/databind/JsonMappingException;
        }
    .end annotation

    .line 429
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/DeserializationContext;->_cache:Lcom/fasterxml/jackson/databind/deser/DeserializerCache;

    iget-object v1, p0, Lcom/fasterxml/jackson/databind/DeserializationContext;->_factory:Lcom/fasterxml/jackson/databind/deser/DeserializerFactory;

    invoke-virtual {v0, p0, v1, p1}, Lcom/fasterxml/jackson/databind/deser/DeserializerCache;->findValueDeserializer(Lcom/fasterxml/jackson/databind/DeserializationContext;Lcom/fasterxml/jackson/databind/deser/DeserializerFactory;Lcom/fasterxml/jackson/databind/JavaType;)Lcom/fasterxml/jackson/databind/JsonDeserializer;

    move-result-object p1

    return-object p1
.end method

.method public abstract findObjectId(Ljava/lang/Object;Lcom/fasterxml/jackson/annotation/ObjectIdGenerator;)Lcom/fasterxml/jackson/databind/deser/impl/ReadableObjectId;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/lang/Object;",
            "Lcom/fasterxml/jackson/annotation/ObjectIdGenerator<",
            "*>;)",
            "Lcom/fasterxml/jackson/databind/deser/impl/ReadableObjectId;"
        }
    .end annotation

    .annotation runtime Ljava/lang/Deprecated;
    .end annotation
.end method

.method public abstract findObjectId(Ljava/lang/Object;Lcom/fasterxml/jackson/annotation/ObjectIdGenerator;Lcom/fasterxml/jackson/annotation/ObjectIdResolver;)Lcom/fasterxml/jackson/databind/deser/impl/ReadableObjectId;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/lang/Object;",
            "Lcom/fasterxml/jackson/annotation/ObjectIdGenerator<",
            "*>;",
            "Lcom/fasterxml/jackson/annotation/ObjectIdResolver;",
            ")",
            "Lcom/fasterxml/jackson/databind/deser/impl/ReadableObjectId;"
        }
    .end annotation
.end method

.method public final findRootValueDeserializer(Lcom/fasterxml/jackson/databind/JavaType;)Lcom/fasterxml/jackson/databind/JsonDeserializer;
    .locals 4
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Lcom/fasterxml/jackson/databind/JavaType;",
            ")",
            "Lcom/fasterxml/jackson/databind/JsonDeserializer<",
            "Ljava/lang/Object;",
            ">;"
        }
    .end annotation

    .annotation system Ldalvik/annotation/Throws;
        value = {
            Lcom/fasterxml/jackson/databind/JsonMappingException;
        }
    .end annotation

    .line 439
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/DeserializationContext;->_cache:Lcom/fasterxml/jackson/databind/deser/DeserializerCache;

    iget-object v1, p0, Lcom/fasterxml/jackson/databind/DeserializationContext;->_factory:Lcom/fasterxml/jackson/databind/deser/DeserializerFactory;

    invoke-virtual {v0, p0, v1, p1}, Lcom/fasterxml/jackson/databind/deser/DeserializerCache;->findValueDeserializer(Lcom/fasterxml/jackson/databind/DeserializationContext;Lcom/fasterxml/jackson/databind/deser/DeserializerFactory;Lcom/fasterxml/jackson/databind/JavaType;)Lcom/fasterxml/jackson/databind/JsonDeserializer;

    move-result-object v0

    const/4 v1, 0x0

    if-nez v0, :cond_0

    return-object v1

    .line 444
    :cond_0
    invoke-virtual {p0, v0, v1, p1}, Lcom/fasterxml/jackson/databind/DeserializationContext;->handleSecondaryContextualization(Lcom/fasterxml/jackson/databind/JsonDeserializer;Lcom/fasterxml/jackson/databind/BeanProperty;Lcom/fasterxml/jackson/databind/JavaType;)Lcom/fasterxml/jackson/databind/JsonDeserializer;

    move-result-object v0

    .line 445
    iget-object v2, p0, Lcom/fasterxml/jackson/databind/DeserializationContext;->_factory:Lcom/fasterxml/jackson/databind/deser/DeserializerFactory;

    iget-object v3, p0, Lcom/fasterxml/jackson/databind/DeserializationContext;->_config:Lcom/fasterxml/jackson/databind/DeserializationConfig;

    invoke-virtual {v2, v3, p1}, Lcom/fasterxml/jackson/databind/deser/DeserializerFactory;->findTypeDeserializer(Lcom/fasterxml/jackson/databind/DeserializationConfig;Lcom/fasterxml/jackson/databind/JavaType;)Lcom/fasterxml/jackson/databind/jsontype/TypeDeserializer;

    move-result-object p1

    if-eqz p1, :cond_1

    .line 448
    invoke-virtual {p1, v1}, Lcom/fasterxml/jackson/databind/jsontype/TypeDeserializer;->forProperty(Lcom/fasterxml/jackson/databind/BeanProperty;)Lcom/fasterxml/jackson/databind/jsontype/TypeDeserializer;

    move-result-object p1

    .line 449
    new-instance v1, Lcom/fasterxml/jackson/databind/deser/impl/TypeWrappedDeserializer;

    invoke-direct {v1, p1, v0}, Lcom/fasterxml/jackson/databind/deser/impl/TypeWrappedDeserializer;-><init>(Lcom/fasterxml/jackson/databind/jsontype/TypeDeserializer;Lcom/fasterxml/jackson/databind/JsonDeserializer;)V

    return-object v1

    :cond_1
    return-object v0
.end method

.method public final getActiveView()Ljava/lang/Class;
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()",
            "Ljava/lang/Class<",
            "*>;"
        }
    .end annotation

    .line 222
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/DeserializationContext;->_view:Ljava/lang/Class;

    return-object v0
.end method

.method public final getAnnotationIntrospector()Lcom/fasterxml/jackson/databind/AnnotationIntrospector;
    .locals 1

    .line 226
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/DeserializationContext;->_config:Lcom/fasterxml/jackson/databind/DeserializationConfig;

    invoke-virtual {v0}, Lcom/fasterxml/jackson/databind/DeserializationConfig;->getAnnotationIntrospector()Lcom/fasterxml/jackson/databind/AnnotationIntrospector;

    move-result-object v0

    return-object v0
.end method

.method public final getArrayBuilders()Lcom/fasterxml/jackson/databind/util/ArrayBuilders;
    .locals 1

    .line 569
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/DeserializationContext;->_arrayBuilders:Lcom/fasterxml/jackson/databind/util/ArrayBuilders;

    if-nez v0, :cond_0

    .line 570
    new-instance v0, Lcom/fasterxml/jackson/databind/util/ArrayBuilders;

    invoke-direct {v0}, Lcom/fasterxml/jackson/databind/util/ArrayBuilders;-><init>()V

    iput-object v0, p0, Lcom/fasterxml/jackson/databind/DeserializationContext;->_arrayBuilders:Lcom/fasterxml/jackson/databind/util/ArrayBuilders;

    .line 572
    :cond_0
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/DeserializationContext;->_arrayBuilders:Lcom/fasterxml/jackson/databind/util/ArrayBuilders;

    return-object v0
.end method

.method public getAttribute(Ljava/lang/Object;)Ljava/lang/Object;
    .locals 1

    .line 242
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/DeserializationContext;->_attributes:Lcom/fasterxml/jackson/databind/cfg/ContextAttributes;

    invoke-virtual {v0, p1}, Lcom/fasterxml/jackson/databind/cfg/ContextAttributes;->getAttribute(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object p1

    return-object p1
.end method

.method public final getBase64Variant()Lcom/fasterxml/jackson/core/Base64Variant;
    .locals 1

    .line 331
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/DeserializationContext;->_config:Lcom/fasterxml/jackson/databind/DeserializationConfig;

    invoke-virtual {v0}, Lcom/fasterxml/jackson/databind/DeserializationConfig;->getBase64Variant()Lcom/fasterxml/jackson/core/Base64Variant;

    move-result-object v0

    return-object v0
.end method

.method public getConfig()Lcom/fasterxml/jackson/databind/DeserializationConfig;
    .locals 1

    .line 219
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/DeserializationContext;->_config:Lcom/fasterxml/jackson/databind/DeserializationConfig;

    return-object v0
.end method

.method public bridge synthetic getConfig()Lcom/fasterxml/jackson/databind/cfg/MapperConfig;
    .locals 1

    .line 41
    invoke-virtual {p0}, Lcom/fasterxml/jackson/databind/DeserializationContext;->getConfig()Lcom/fasterxml/jackson/databind/DeserializationConfig;

    move-result-object v0

    return-object v0
.end method

.method public getContextualType()Lcom/fasterxml/jackson/databind/JavaType;
    .locals 1

    .line 266
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/DeserializationContext;->_currentType:Lcom/fasterxml/jackson/databind/util/LinkedNode;

    if-nez v0, :cond_0

    const/4 v0, 0x0

    goto :goto_0

    :cond_0
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/DeserializationContext;->_currentType:Lcom/fasterxml/jackson/databind/util/LinkedNode;

    invoke-virtual {v0}, Lcom/fasterxml/jackson/databind/util/LinkedNode;->value()Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Lcom/fasterxml/jackson/databind/JavaType;

    :goto_0
    return-object v0
.end method

.method protected getDateFormat()Ljava/text/DateFormat;
    .locals 1

    .line 964
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/DeserializationContext;->_dateFormat:Ljava/text/DateFormat;

    if-eqz v0, :cond_0

    .line 965
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/DeserializationContext;->_dateFormat:Ljava/text/DateFormat;

    return-object v0

    .line 972
    :cond_0
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/DeserializationContext;->_config:Lcom/fasterxml/jackson/databind/DeserializationConfig;

    invoke-virtual {v0}, Lcom/fasterxml/jackson/databind/DeserializationConfig;->getDateFormat()Ljava/text/DateFormat;

    move-result-object v0

    .line 973
    invoke-virtual {v0}, Ljava/text/DateFormat;->clone()Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Ljava/text/DateFormat;

    iput-object v0, p0, Lcom/fasterxml/jackson/databind/DeserializationContext;->_dateFormat:Ljava/text/DateFormat;

    return-object v0
.end method

.method public getFactory()Lcom/fasterxml/jackson/databind/deser/DeserializerFactory;
    .locals 1

    .line 279
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/DeserializationContext;->_factory:Lcom/fasterxml/jackson/databind/deser/DeserializerFactory;

    return-object v0
.end method

.method public getLocale()Ljava/util/Locale;
    .locals 1

    .line 351
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/DeserializationContext;->_config:Lcom/fasterxml/jackson/databind/DeserializationConfig;

    invoke-virtual {v0}, Lcom/fasterxml/jackson/databind/DeserializationConfig;->getLocale()Ljava/util/Locale;

    move-result-object v0

    return-object v0
.end method

.method public final getNodeFactory()Lcom/fasterxml/jackson/databind/node/JsonNodeFactory;
    .locals 1

    .line 341
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/DeserializationContext;->_config:Lcom/fasterxml/jackson/databind/DeserializationConfig;

    invoke-virtual {v0}, Lcom/fasterxml/jackson/databind/DeserializationConfig;->getNodeFactory()Lcom/fasterxml/jackson/databind/node/JsonNodeFactory;

    move-result-object v0

    return-object v0
.end method

.method public final getParser()Lcom/fasterxml/jackson/core/JsonParser;
    .locals 1

    .line 311
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/DeserializationContext;->_parser:Lcom/fasterxml/jackson/core/JsonParser;

    return-object v0
.end method

.method public getTimeZone()Ljava/util/TimeZone;
    .locals 1

    .line 361
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/DeserializationContext;->_config:Lcom/fasterxml/jackson/databind/DeserializationConfig;

    invoke-virtual {v0}, Lcom/fasterxml/jackson/databind/DeserializationConfig;->getTimeZone()Ljava/util/TimeZone;

    move-result-object v0

    return-object v0
.end method

.method public final getTypeFactory()Lcom/fasterxml/jackson/databind/type/TypeFactory;
    .locals 1

    .line 231
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/DeserializationContext;->_config:Lcom/fasterxml/jackson/databind/DeserializationConfig;

    invoke-virtual {v0}, Lcom/fasterxml/jackson/databind/DeserializationConfig;->getTypeFactory()Lcom/fasterxml/jackson/databind/type/TypeFactory;

    move-result-object v0

    return-object v0
.end method

.method public handlePrimaryContextualization(Lcom/fasterxml/jackson/databind/JsonDeserializer;Lcom/fasterxml/jackson/databind/BeanProperty;)Lcom/fasterxml/jackson/databind/JsonDeserializer;
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Lcom/fasterxml/jackson/databind/JsonDeserializer<",
            "*>;",
            "Lcom/fasterxml/jackson/databind/BeanProperty;",
            ")",
            "Lcom/fasterxml/jackson/databind/JsonDeserializer<",
            "*>;"
        }
    .end annotation

    .annotation system Ldalvik/annotation/Throws;
        value = {
            Lcom/fasterxml/jackson/databind/JsonMappingException;
        }
    .end annotation

    .annotation runtime Ljava/lang/Deprecated;
    .end annotation

    .line 627
    invoke-static {}, Lcom/fasterxml/jackson/databind/type/TypeFactory;->unknownType()Lcom/fasterxml/jackson/databind/JavaType;

    move-result-object v0

    invoke-virtual {p0, p1, p2, v0}, Lcom/fasterxml/jackson/databind/DeserializationContext;->handlePrimaryContextualization(Lcom/fasterxml/jackson/databind/JsonDeserializer;Lcom/fasterxml/jackson/databind/BeanProperty;Lcom/fasterxml/jackson/databind/JavaType;)Lcom/fasterxml/jackson/databind/JsonDeserializer;

    move-result-object p1

    return-object p1
.end method

.method public handlePrimaryContextualization(Lcom/fasterxml/jackson/databind/JsonDeserializer;Lcom/fasterxml/jackson/databind/BeanProperty;Lcom/fasterxml/jackson/databind/JavaType;)Lcom/fasterxml/jackson/databind/JsonDeserializer;
    .locals 2
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Lcom/fasterxml/jackson/databind/JsonDeserializer<",
            "*>;",
            "Lcom/fasterxml/jackson/databind/BeanProperty;",
            "Lcom/fasterxml/jackson/databind/JavaType;",
            ")",
            "Lcom/fasterxml/jackson/databind/JsonDeserializer<",
            "*>;"
        }
    .end annotation

    .annotation system Ldalvik/annotation/Throws;
        value = {
            Lcom/fasterxml/jackson/databind/JsonMappingException;
        }
    .end annotation

    .line 611
    instance-of v0, p1, Lcom/fasterxml/jackson/databind/deser/ContextualDeserializer;

    if-eqz v0, :cond_0

    .line 612
    new-instance v0, Lcom/fasterxml/jackson/databind/util/LinkedNode;

    iget-object v1, p0, Lcom/fasterxml/jackson/databind/DeserializationContext;->_currentType:Lcom/fasterxml/jackson/databind/util/LinkedNode;

    invoke-direct {v0, p3, v1}, Lcom/fasterxml/jackson/databind/util/LinkedNode;-><init>(Ljava/lang/Object;Lcom/fasterxml/jackson/databind/util/LinkedNode;)V

    iput-object v0, p0, Lcom/fasterxml/jackson/databind/DeserializationContext;->_currentType:Lcom/fasterxml/jackson/databind/util/LinkedNode;

    .line 614
    :try_start_0
    check-cast p1, Lcom/fasterxml/jackson/databind/deser/ContextualDeserializer;

    invoke-interface {p1, p0, p2}, Lcom/fasterxml/jackson/databind/deser/ContextualDeserializer;->createContextual(Lcom/fasterxml/jackson/databind/DeserializationContext;Lcom/fasterxml/jackson/databind/BeanProperty;)Lcom/fasterxml/jackson/databind/JsonDeserializer;

    move-result-object p1
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    .line 616
    iget-object p2, p0, Lcom/fasterxml/jackson/databind/DeserializationContext;->_currentType:Lcom/fasterxml/jackson/databind/util/LinkedNode;

    invoke-virtual {p2}, Lcom/fasterxml/jackson/databind/util/LinkedNode;->next()Lcom/fasterxml/jackson/databind/util/LinkedNode;

    move-result-object p2

    iput-object p2, p0, Lcom/fasterxml/jackson/databind/DeserializationContext;->_currentType:Lcom/fasterxml/jackson/databind/util/LinkedNode;

    goto :goto_0

    :catchall_0
    move-exception p1

    iget-object p2, p0, Lcom/fasterxml/jackson/databind/DeserializationContext;->_currentType:Lcom/fasterxml/jackson/databind/util/LinkedNode;

    invoke-virtual {p2}, Lcom/fasterxml/jackson/databind/util/LinkedNode;->next()Lcom/fasterxml/jackson/databind/util/LinkedNode;

    move-result-object p2

    iput-object p2, p0, Lcom/fasterxml/jackson/databind/DeserializationContext;->_currentType:Lcom/fasterxml/jackson/databind/util/LinkedNode;

    throw p1

    :cond_0
    :goto_0
    return-object p1
.end method

.method public handleSecondaryContextualization(Lcom/fasterxml/jackson/databind/JsonDeserializer;Lcom/fasterxml/jackson/databind/BeanProperty;)Lcom/fasterxml/jackson/databind/JsonDeserializer;
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Lcom/fasterxml/jackson/databind/JsonDeserializer<",
            "*>;",
            "Lcom/fasterxml/jackson/databind/BeanProperty;",
            ")",
            "Lcom/fasterxml/jackson/databind/JsonDeserializer<",
            "*>;"
        }
    .end annotation

    .annotation system Ldalvik/annotation/Throws;
        value = {
            Lcom/fasterxml/jackson/databind/JsonMappingException;
        }
    .end annotation

    .annotation runtime Ljava/lang/Deprecated;
    .end annotation

    .line 666
    instance-of v0, p1, Lcom/fasterxml/jackson/databind/deser/ContextualDeserializer;

    if-eqz v0, :cond_0

    .line 667
    check-cast p1, Lcom/fasterxml/jackson/databind/deser/ContextualDeserializer;

    invoke-interface {p1, p0, p2}, Lcom/fasterxml/jackson/databind/deser/ContextualDeserializer;->createContextual(Lcom/fasterxml/jackson/databind/DeserializationContext;Lcom/fasterxml/jackson/databind/BeanProperty;)Lcom/fasterxml/jackson/databind/JsonDeserializer;

    move-result-object p1

    :cond_0
    return-object p1
.end method

.method public handleSecondaryContextualization(Lcom/fasterxml/jackson/databind/JsonDeserializer;Lcom/fasterxml/jackson/databind/BeanProperty;Lcom/fasterxml/jackson/databind/JavaType;)Lcom/fasterxml/jackson/databind/JsonDeserializer;
    .locals 2
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Lcom/fasterxml/jackson/databind/JsonDeserializer<",
            "*>;",
            "Lcom/fasterxml/jackson/databind/BeanProperty;",
            "Lcom/fasterxml/jackson/databind/JavaType;",
            ")",
            "Lcom/fasterxml/jackson/databind/JsonDeserializer<",
            "*>;"
        }
    .end annotation

    .annotation system Ldalvik/annotation/Throws;
        value = {
            Lcom/fasterxml/jackson/databind/JsonMappingException;
        }
    .end annotation

    .line 650
    instance-of v0, p1, Lcom/fasterxml/jackson/databind/deser/ContextualDeserializer;

    if-eqz v0, :cond_0

    .line 651
    new-instance v0, Lcom/fasterxml/jackson/databind/util/LinkedNode;

    iget-object v1, p0, Lcom/fasterxml/jackson/databind/DeserializationContext;->_currentType:Lcom/fasterxml/jackson/databind/util/LinkedNode;

    invoke-direct {v0, p3, v1}, Lcom/fasterxml/jackson/databind/util/LinkedNode;-><init>(Ljava/lang/Object;Lcom/fasterxml/jackson/databind/util/LinkedNode;)V

    iput-object v0, p0, Lcom/fasterxml/jackson/databind/DeserializationContext;->_currentType:Lcom/fasterxml/jackson/databind/util/LinkedNode;

    .line 653
    :try_start_0
    check-cast p1, Lcom/fasterxml/jackson/databind/deser/ContextualDeserializer;

    invoke-interface {p1, p0, p2}, Lcom/fasterxml/jackson/databind/deser/ContextualDeserializer;->createContextual(Lcom/fasterxml/jackson/databind/DeserializationContext;Lcom/fasterxml/jackson/databind/BeanProperty;)Lcom/fasterxml/jackson/databind/JsonDeserializer;

    move-result-object p1
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    .line 655
    iget-object p2, p0, Lcom/fasterxml/jackson/databind/DeserializationContext;->_currentType:Lcom/fasterxml/jackson/databind/util/LinkedNode;

    invoke-virtual {p2}, Lcom/fasterxml/jackson/databind/util/LinkedNode;->next()Lcom/fasterxml/jackson/databind/util/LinkedNode;

    move-result-object p2

    iput-object p2, p0, Lcom/fasterxml/jackson/databind/DeserializationContext;->_currentType:Lcom/fasterxml/jackson/databind/util/LinkedNode;

    goto :goto_0

    :catchall_0
    move-exception p1

    iget-object p2, p0, Lcom/fasterxml/jackson/databind/DeserializationContext;->_currentType:Lcom/fasterxml/jackson/databind/util/LinkedNode;

    invoke-virtual {p2}, Lcom/fasterxml/jackson/databind/util/LinkedNode;->next()Lcom/fasterxml/jackson/databind/util/LinkedNode;

    move-result-object p2

    iput-object p2, p0, Lcom/fasterxml/jackson/databind/DeserializationContext;->_currentType:Lcom/fasterxml/jackson/databind/util/LinkedNode;

    throw p1

    :cond_0
    :goto_0
    return-object p1
.end method

.method public handleUnknownProperty(Lcom/fasterxml/jackson/core/JsonParser;Lcom/fasterxml/jackson/databind/JsonDeserializer;Ljava/lang/Object;Ljava/lang/String;)Z
    .locals 8
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Lcom/fasterxml/jackson/core/JsonParser;",
            "Lcom/fasterxml/jackson/databind/JsonDeserializer<",
            "*>;",
            "Ljava/lang/Object;",
            "Ljava/lang/String;",
            ")Z"
        }
    .end annotation

    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;,
            Lcom/fasterxml/jackson/core/JsonProcessingException;
        }
    .end annotation

    .line 786
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/DeserializationContext;->_config:Lcom/fasterxml/jackson/databind/DeserializationConfig;

    invoke-virtual {v0}, Lcom/fasterxml/jackson/databind/DeserializationConfig;->getProblemHandlers()Lcom/fasterxml/jackson/databind/util/LinkedNode;

    move-result-object v0

    if-eqz v0, :cond_1

    :goto_0
    if-eqz v0, :cond_1

    .line 790
    invoke-virtual {v0}, Lcom/fasterxml/jackson/databind/util/LinkedNode;->value()Ljava/lang/Object;

    move-result-object v1

    move-object v2, v1

    check-cast v2, Lcom/fasterxml/jackson/databind/deser/DeserializationProblemHandler;

    move-object v3, p0

    move-object v4, p1

    move-object v5, p2

    move-object v6, p3

    move-object v7, p4

    invoke-virtual/range {v2 .. v7}, Lcom/fasterxml/jackson/databind/deser/DeserializationProblemHandler;->handleUnknownProperty(Lcom/fasterxml/jackson/databind/DeserializationContext;Lcom/fasterxml/jackson/core/JsonParser;Lcom/fasterxml/jackson/databind/JsonDeserializer;Ljava/lang/Object;Ljava/lang/String;)Z

    move-result v1

    if-eqz v1, :cond_0

    const/4 p1, 0x1

    return p1

    .line 793
    :cond_0
    invoke-virtual {v0}, Lcom/fasterxml/jackson/databind/util/LinkedNode;->next()Lcom/fasterxml/jackson/databind/util/LinkedNode;

    move-result-object v0

    goto :goto_0

    :cond_1
    const/4 p1, 0x0

    return p1
.end method

.method public final hasDeserializationFeatures(I)Z
    .locals 1

    .line 300
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/DeserializationContext;->_config:Lcom/fasterxml/jackson/databind/DeserializationConfig;

    invoke-virtual {v0, p1}, Lcom/fasterxml/jackson/databind/DeserializationConfig;->hasDeserializationFeatures(I)Z

    move-result p1

    return p1
.end method

.method public hasValueDeserializerFor(Lcom/fasterxml/jackson/databind/JavaType;)Z
    .locals 1
    .annotation runtime Ljava/lang/Deprecated;
    .end annotation

    const/4 v0, 0x0

    .line 372
    invoke-virtual {p0, p1, v0}, Lcom/fasterxml/jackson/databind/DeserializationContext;->hasValueDeserializerFor(Lcom/fasterxml/jackson/databind/JavaType;Ljava/util/concurrent/atomic/AtomicReference;)Z

    move-result p1

    return p1
.end method

.method public hasValueDeserializerFor(Lcom/fasterxml/jackson/databind/JavaType;Ljava/util/concurrent/atomic/AtomicReference;)Z
    .locals 2
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Lcom/fasterxml/jackson/databind/JavaType;",
            "Ljava/util/concurrent/atomic/AtomicReference<",
            "Ljava/lang/Throwable;",
            ">;)Z"
        }
    .end annotation

    .line 384
    :try_start_0
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/DeserializationContext;->_cache:Lcom/fasterxml/jackson/databind/deser/DeserializerCache;

    iget-object v1, p0, Lcom/fasterxml/jackson/databind/DeserializationContext;->_factory:Lcom/fasterxml/jackson/databind/deser/DeserializerFactory;

    invoke-virtual {v0, p0, v1, p1}, Lcom/fasterxml/jackson/databind/deser/DeserializerCache;->hasValueDeserializerFor(Lcom/fasterxml/jackson/databind/DeserializationContext;Lcom/fasterxml/jackson/databind/deser/DeserializerFactory;Lcom/fasterxml/jackson/databind/JavaType;)Z

    move-result p1
    :try_end_0
    .catch Lcom/fasterxml/jackson/databind/JsonMappingException; {:try_start_0 .. :try_end_0} :catch_1
    .catch Ljava/lang/RuntimeException; {:try_start_0 .. :try_end_0} :catch_0

    return p1

    :catch_0
    move-exception p1

    if-eqz p2, :cond_0

    .line 393
    invoke-virtual {p2, p1}, Ljava/util/concurrent/atomic/AtomicReference;->set(Ljava/lang/Object;)V

    goto :goto_0

    .line 391
    :cond_0
    throw p1

    :catch_1
    move-exception p1

    if-eqz p2, :cond_1

    .line 387
    invoke-virtual {p2, p1}, Ljava/util/concurrent/atomic/AtomicReference;->set(Ljava/lang/Object;)V

    :cond_1
    :goto_0
    const/4 p1, 0x0

    return p1
.end method

.method public instantiationException(Ljava/lang/Class;Ljava/lang/String;)Lcom/fasterxml/jackson/databind/JsonMappingException;
    .locals 3
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/lang/Class<",
            "*>;",
            "Ljava/lang/String;",
            ")",
            "Lcom/fasterxml/jackson/databind/JsonMappingException;"
        }
    .end annotation

    .line 857
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/DeserializationContext;->_parser:Lcom/fasterxml/jackson/core/JsonParser;

    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "Can not construct instance of "

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p1}, Ljava/lang/Class;->getName()Ljava/lang/String;

    move-result-object p1

    invoke-virtual {v1, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string p1, ", problem: "

    invoke-virtual {v1, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1, p2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p1

    invoke-static {v0, p1}, Lcom/fasterxml/jackson/databind/JsonMappingException;->from(Lcom/fasterxml/jackson/core/JsonParser;Ljava/lang/String;)Lcom/fasterxml/jackson/databind/JsonMappingException;

    move-result-object p1

    return-object p1
.end method

.method public instantiationException(Ljava/lang/Class;Ljava/lang/Throwable;)Lcom/fasterxml/jackson/databind/JsonMappingException;
    .locals 3
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/lang/Class<",
            "*>;",
            "Ljava/lang/Throwable;",
            ")",
            "Lcom/fasterxml/jackson/databind/JsonMappingException;"
        }
    .end annotation

    .line 852
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/DeserializationContext;->_parser:Lcom/fasterxml/jackson/core/JsonParser;

    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "Can not construct instance of "

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p1}, Ljava/lang/Class;->getName()Ljava/lang/String;

    move-result-object p1

    invoke-virtual {v1, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string p1, ", problem: "

    invoke-virtual {v1, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p2}, Ljava/lang/Throwable;->getMessage()Ljava/lang/String;

    move-result-object p1

    invoke-virtual {v1, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p1

    invoke-static {v0, p1, p2}, Lcom/fasterxml/jackson/databind/JsonMappingException;->from(Lcom/fasterxml/jackson/core/JsonParser;Ljava/lang/String;Ljava/lang/Throwable;)Lcom/fasterxml/jackson/databind/JsonMappingException;

    move-result-object p1

    return-object p1
.end method

.method public final isEnabled(Lcom/fasterxml/jackson/databind/DeserializationFeature;)Z
    .locals 1

    .line 290
    iget v0, p0, Lcom/fasterxml/jackson/databind/DeserializationContext;->_featureFlags:I

    invoke-virtual {p1}, Lcom/fasterxml/jackson/databind/DeserializationFeature;->getMask()I

    move-result p1

    and-int/2addr p1, v0

    if-eqz p1, :cond_0

    const/4 p1, 0x1

    goto :goto_0

    :cond_0
    const/4 p1, 0x0

    :goto_0
    return p1
.end method

.method public abstract keyDeserializerInstance(Lcom/fasterxml/jackson/databind/introspect/Annotated;Ljava/lang/Object;)Lcom/fasterxml/jackson/databind/KeyDeserializer;
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Lcom/fasterxml/jackson/databind/JsonMappingException;
        }
    .end annotation
.end method

.method public final leaseObjectBuffer()Lcom/fasterxml/jackson/databind/util/ObjectBuffer;
    .locals 2

    .line 537
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/DeserializationContext;->_objectBuffer:Lcom/fasterxml/jackson/databind/util/ObjectBuffer;

    if-nez v0, :cond_0

    .line 539
    new-instance v0, Lcom/fasterxml/jackson/databind/util/ObjectBuffer;

    invoke-direct {v0}, Lcom/fasterxml/jackson/databind/util/ObjectBuffer;-><init>()V

    goto :goto_0

    :cond_0
    const/4 v1, 0x0

    .line 541
    iput-object v1, p0, Lcom/fasterxml/jackson/databind/DeserializationContext;->_objectBuffer:Lcom/fasterxml/jackson/databind/util/ObjectBuffer;

    :goto_0
    return-object v0
.end method

.method public mappingException(Ljava/lang/Class;)Lcom/fasterxml/jackson/databind/JsonMappingException;
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/lang/Class<",
            "*>;)",
            "Lcom/fasterxml/jackson/databind/JsonMappingException;"
        }
    .end annotation

    .line 831
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/DeserializationContext;->_parser:Lcom/fasterxml/jackson/core/JsonParser;

    invoke-virtual {v0}, Lcom/fasterxml/jackson/core/JsonParser;->getCurrentToken()Lcom/fasterxml/jackson/core/JsonToken;

    move-result-object v0

    invoke-virtual {p0, p1, v0}, Lcom/fasterxml/jackson/databind/DeserializationContext;->mappingException(Ljava/lang/Class;Lcom/fasterxml/jackson/core/JsonToken;)Lcom/fasterxml/jackson/databind/JsonMappingException;

    move-result-object p1

    return-object p1
.end method

.method public mappingException(Ljava/lang/Class;Lcom/fasterxml/jackson/core/JsonToken;)Lcom/fasterxml/jackson/databind/JsonMappingException;
    .locals 3
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/lang/Class<",
            "*>;",
            "Lcom/fasterxml/jackson/core/JsonToken;",
            ")",
            "Lcom/fasterxml/jackson/databind/JsonMappingException;"
        }
    .end annotation

    .line 835
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/DeserializationContext;->_parser:Lcom/fasterxml/jackson/core/JsonParser;

    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "Can not deserialize instance of "

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p0, p1}, Lcom/fasterxml/jackson/databind/DeserializationContext;->_calcName(Ljava/lang/Class;)Ljava/lang/String;

    move-result-object p1

    invoke-virtual {v1, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string p1, " out of "

    invoke-virtual {v1, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1, p2}, Ljava/lang/StringBuilder;->append(Ljava/lang/Object;)Ljava/lang/StringBuilder;

    const-string p1, " token"

    invoke-virtual {v1, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p1

    invoke-static {v0, p1}, Lcom/fasterxml/jackson/databind/JsonMappingException;->from(Lcom/fasterxml/jackson/core/JsonParser;Ljava/lang/String;)Lcom/fasterxml/jackson/databind/JsonMappingException;

    move-result-object p1

    return-object p1
.end method

.method public mappingException(Ljava/lang/String;)Lcom/fasterxml/jackson/databind/JsonMappingException;
    .locals 1

    .line 843
    invoke-virtual {p0}, Lcom/fasterxml/jackson/databind/DeserializationContext;->getParser()Lcom/fasterxml/jackson/core/JsonParser;

    move-result-object v0

    invoke-static {v0, p1}, Lcom/fasterxml/jackson/databind/JsonMappingException;->from(Lcom/fasterxml/jackson/core/JsonParser;Ljava/lang/String;)Lcom/fasterxml/jackson/databind/JsonMappingException;

    move-result-object p1

    return-object p1
.end method

.method public parseDate(Ljava/lang/String;)Ljava/util/Date;
    .locals 4
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/lang/IllegalArgumentException;
        }
    .end annotation

    .line 691
    :try_start_0
    invoke-virtual {p0}, Lcom/fasterxml/jackson/databind/DeserializationContext;->getDateFormat()Ljava/text/DateFormat;

    move-result-object v0

    .line 692
    invoke-virtual {v0, p1}, Ljava/text/DateFormat;->parse(Ljava/lang/String;)Ljava/util/Date;

    move-result-object v0
    :try_end_0
    .catch Ljava/text/ParseException; {:try_start_0 .. :try_end_0} :catch_0

    return-object v0

    :catch_0
    move-exception v0

    .line 694
    new-instance v1, Ljava/lang/IllegalArgumentException;

    new-instance v2, Ljava/lang/StringBuilder;

    invoke-direct {v2}, Ljava/lang/StringBuilder;-><init>()V

    const-string v3, "Failed to parse Date value \'"

    invoke-virtual {v2, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v2, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string p1, "\': "

    invoke-virtual {v2, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/text/ParseException;->getMessage()Ljava/lang/String;

    move-result-object p1

    invoke-virtual {v2, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v2}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p1

    invoke-direct {v1, p1}, Ljava/lang/IllegalArgumentException;-><init>(Ljava/lang/String;)V

    throw v1
.end method

.method public readPropertyValue(Lcom/fasterxml/jackson/core/JsonParser;Lcom/fasterxml/jackson/databind/BeanProperty;Lcom/fasterxml/jackson/databind/JavaType;)Ljava/lang/Object;
    .locals 0
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "<T:",
            "Ljava/lang/Object;",
            ">(",
            "Lcom/fasterxml/jackson/core/JsonParser;",
            "Lcom/fasterxml/jackson/databind/BeanProperty;",
            "Lcom/fasterxml/jackson/databind/JavaType;",
            ")TT;"
        }
    .end annotation

    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    .line 758
    invoke-virtual {p0, p3, p2}, Lcom/fasterxml/jackson/databind/DeserializationContext;->findContextualValueDeserializer(Lcom/fasterxml/jackson/databind/JavaType;Lcom/fasterxml/jackson/databind/BeanProperty;)Lcom/fasterxml/jackson/databind/JsonDeserializer;

    move-result-object p2

    .line 762
    invoke-virtual {p2, p1, p0}, Lcom/fasterxml/jackson/databind/JsonDeserializer;->deserialize(Lcom/fasterxml/jackson/core/JsonParser;Lcom/fasterxml/jackson/databind/DeserializationContext;)Ljava/lang/Object;

    move-result-object p1

    return-object p1
.end method

.method public readPropertyValue(Lcom/fasterxml/jackson/core/JsonParser;Lcom/fasterxml/jackson/databind/BeanProperty;Ljava/lang/Class;)Ljava/lang/Object;
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "<T:",
            "Ljava/lang/Object;",
            ">(",
            "Lcom/fasterxml/jackson/core/JsonParser;",
            "Lcom/fasterxml/jackson/databind/BeanProperty;",
            "Ljava/lang/Class<",
            "TT;>;)TT;"
        }
    .end annotation

    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    .line 750
    invoke-virtual {p0}, Lcom/fasterxml/jackson/databind/DeserializationContext;->getTypeFactory()Lcom/fasterxml/jackson/databind/type/TypeFactory;

    move-result-object v0

    invoke-virtual {v0, p3}, Lcom/fasterxml/jackson/databind/type/TypeFactory;->constructType(Ljava/lang/reflect/Type;)Lcom/fasterxml/jackson/databind/JavaType;

    move-result-object p3

    invoke-virtual {p0, p1, p2, p3}, Lcom/fasterxml/jackson/databind/DeserializationContext;->readPropertyValue(Lcom/fasterxml/jackson/core/JsonParser;Lcom/fasterxml/jackson/databind/BeanProperty;Lcom/fasterxml/jackson/databind/JavaType;)Ljava/lang/Object;

    move-result-object p1

    return-object p1
.end method

.method public readValue(Lcom/fasterxml/jackson/core/JsonParser;Lcom/fasterxml/jackson/databind/JavaType;)Ljava/lang/Object;
    .locals 0
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "<T:",
            "Ljava/lang/Object;",
            ">(",
            "Lcom/fasterxml/jackson/core/JsonParser;",
            "Lcom/fasterxml/jackson/databind/JavaType;",
            ")TT;"
        }
    .end annotation

    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    .line 735
    invoke-virtual {p0, p2}, Lcom/fasterxml/jackson/databind/DeserializationContext;->findRootValueDeserializer(Lcom/fasterxml/jackson/databind/JavaType;)Lcom/fasterxml/jackson/databind/JsonDeserializer;

    move-result-object p2

    .line 738
    invoke-virtual {p2, p1, p0}, Lcom/fasterxml/jackson/databind/JsonDeserializer;->deserialize(Lcom/fasterxml/jackson/core/JsonParser;Lcom/fasterxml/jackson/databind/DeserializationContext;)Ljava/lang/Object;

    move-result-object p1

    return-object p1
.end method

.method public readValue(Lcom/fasterxml/jackson/core/JsonParser;Ljava/lang/Class;)Ljava/lang/Object;
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "<T:",
            "Ljava/lang/Object;",
            ">(",
            "Lcom/fasterxml/jackson/core/JsonParser;",
            "Ljava/lang/Class<",
            "TT;>;)TT;"
        }
    .end annotation

    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    .line 727
    invoke-virtual {p0}, Lcom/fasterxml/jackson/databind/DeserializationContext;->getTypeFactory()Lcom/fasterxml/jackson/databind/type/TypeFactory;

    move-result-object v0

    invoke-virtual {v0, p2}, Lcom/fasterxml/jackson/databind/type/TypeFactory;->constructType(Ljava/lang/reflect/Type;)Lcom/fasterxml/jackson/databind/JavaType;

    move-result-object p2

    invoke-virtual {p0, p1, p2}, Lcom/fasterxml/jackson/databind/DeserializationContext;->readValue(Lcom/fasterxml/jackson/core/JsonParser;Lcom/fasterxml/jackson/databind/JavaType;)Ljava/lang/Object;

    move-result-object p1

    return-object p1
.end method

.method public reportUnknownProperty(Ljava/lang/Object;Ljava/lang/String;Lcom/fasterxml/jackson/databind/JsonDeserializer;)V
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/lang/Object;",
            "Ljava/lang/String;",
            "Lcom/fasterxml/jackson/databind/JsonDeserializer<",
            "*>;)V"
        }
    .end annotation

    .annotation system Ldalvik/annotation/Throws;
        value = {
            Lcom/fasterxml/jackson/databind/JsonMappingException;
        }
    .end annotation

    .line 812
    sget-object v0, Lcom/fasterxml/jackson/databind/DeserializationFeature;->FAIL_ON_UNKNOWN_PROPERTIES:Lcom/fasterxml/jackson/databind/DeserializationFeature;

    invoke-virtual {p0, v0}, Lcom/fasterxml/jackson/databind/DeserializationContext;->isEnabled(Lcom/fasterxml/jackson/databind/DeserializationFeature;)Z

    move-result v0

    if-nez v0, :cond_0

    return-void

    :cond_0
    if-nez p3, :cond_1

    const/4 p3, 0x0

    goto :goto_0

    .line 816
    :cond_1
    invoke-virtual {p3}, Lcom/fasterxml/jackson/databind/JsonDeserializer;->getKnownPropertyNames()Ljava/util/Collection;

    move-result-object p3

    .line 817
    :goto_0
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/DeserializationContext;->_parser:Lcom/fasterxml/jackson/core/JsonParser;

    invoke-static {v0, p1, p2, p3}, Lcom/fasterxml/jackson/databind/exc/UnrecognizedPropertyException;->from(Lcom/fasterxml/jackson/core/JsonParser;Ljava/lang/Object;Ljava/lang/String;Ljava/util/Collection;)Lcom/fasterxml/jackson/databind/exc/UnrecognizedPropertyException;

    move-result-object p1

    throw p1
.end method

.method public final returnObjectBuffer(Lcom/fasterxml/jackson/databind/util/ObjectBuffer;)V
    .locals 2

    .line 557
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/DeserializationContext;->_objectBuffer:Lcom/fasterxml/jackson/databind/util/ObjectBuffer;

    if-eqz v0, :cond_0

    invoke-virtual {p1}, Lcom/fasterxml/jackson/databind/util/ObjectBuffer;->initialCapacity()I

    move-result v0

    iget-object v1, p0, Lcom/fasterxml/jackson/databind/DeserializationContext;->_objectBuffer:Lcom/fasterxml/jackson/databind/util/ObjectBuffer;

    invoke-virtual {v1}, Lcom/fasterxml/jackson/databind/util/ObjectBuffer;->initialCapacity()I

    move-result v1

    if-lt v0, v1, :cond_1

    .line 559
    :cond_0
    iput-object p1, p0, Lcom/fasterxml/jackson/databind/DeserializationContext;->_objectBuffer:Lcom/fasterxml/jackson/databind/util/ObjectBuffer;

    :cond_1
    return-void
.end method

.method public bridge synthetic setAttribute(Ljava/lang/Object;Ljava/lang/Object;)Lcom/fasterxml/jackson/databind/DatabindContext;
    .locals 0

    .line 41
    invoke-virtual {p0, p1, p2}, Lcom/fasterxml/jackson/databind/DeserializationContext;->setAttribute(Ljava/lang/Object;Ljava/lang/Object;)Lcom/fasterxml/jackson/databind/DeserializationContext;

    move-result-object p1

    return-object p1
.end method

.method public setAttribute(Ljava/lang/Object;Ljava/lang/Object;)Lcom/fasterxml/jackson/databind/DeserializationContext;
    .locals 1

    .line 248
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/DeserializationContext;->_attributes:Lcom/fasterxml/jackson/databind/cfg/ContextAttributes;

    invoke-virtual {v0, p1, p2}, Lcom/fasterxml/jackson/databind/cfg/ContextAttributes;->withPerCallAttribute(Ljava/lang/Object;Ljava/lang/Object;)Lcom/fasterxml/jackson/databind/cfg/ContextAttributes;

    move-result-object p1

    iput-object p1, p0, Lcom/fasterxml/jackson/databind/DeserializationContext;->_attributes:Lcom/fasterxml/jackson/databind/cfg/ContextAttributes;

    return-object p0
.end method

.method public unknownTypeException(Lcom/fasterxml/jackson/databind/JavaType;Ljava/lang/String;)Lcom/fasterxml/jackson/databind/JsonMappingException;
    .locals 3
    .annotation runtime Ljava/lang/Deprecated;
    .end annotation

    .line 936
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/DeserializationContext;->_parser:Lcom/fasterxml/jackson/core/JsonParser;

    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "Could not resolve type id \'"

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1, p2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string p2, "\' into a subtype of "

    invoke-virtual {v1, p2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/Object;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p1

    invoke-static {v0, p1}, Lcom/fasterxml/jackson/databind/JsonMappingException;->from(Lcom/fasterxml/jackson/core/JsonParser;Ljava/lang/String;)Lcom/fasterxml/jackson/databind/JsonMappingException;

    move-result-object p1

    return-object p1
.end method

.method public unknownTypeException(Lcom/fasterxml/jackson/databind/JavaType;Ljava/lang/String;Ljava/lang/String;)Lcom/fasterxml/jackson/databind/JsonMappingException;
    .locals 2

    .line 944
    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    const-string v1, "Could not resolve type id \'"

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0, p2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string p2, "\' into a subtype of "

    invoke-virtual {v0, p2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/Object;)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p1

    if-eqz p3, :cond_0

    .line 946
    new-instance p2, Ljava/lang/StringBuilder;

    invoke-direct {p2}, Ljava/lang/StringBuilder;-><init>()V

    invoke-virtual {p2, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string p1, ": "

    invoke-virtual {p2, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p2, p3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p2}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p1

    .line 948
    :cond_0
    iget-object p2, p0, Lcom/fasterxml/jackson/databind/DeserializationContext;->_parser:Lcom/fasterxml/jackson/core/JsonParser;

    invoke-static {p2, p1}, Lcom/fasterxml/jackson/databind/JsonMappingException;->from(Lcom/fasterxml/jackson/core/JsonParser;Ljava/lang/String;)Lcom/fasterxml/jackson/databind/JsonMappingException;

    move-result-object p1

    return-object p1
.end method

.method public weirdKeyException(Ljava/lang/Class;Ljava/lang/String;Ljava/lang/String;)Lcom/fasterxml/jackson/databind/JsonMappingException;
    .locals 3
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/lang/Class<",
            "*>;",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            ")",
            "Lcom/fasterxml/jackson/databind/JsonMappingException;"
        }
    .end annotation

    .line 913
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/DeserializationContext;->_parser:Lcom/fasterxml/jackson/core/JsonParser;

    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "Can not construct Map key of type "

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p1}, Ljava/lang/Class;->getName()Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v2, " from String \""

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p0, p2}, Lcom/fasterxml/jackson/databind/DeserializationContext;->_desc(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v2, "\": "

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1, p3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p3

    invoke-static {v0, p3, p2, p1}, Lcom/fasterxml/jackson/databind/exc/InvalidFormatException;->from(Lcom/fasterxml/jackson/core/JsonParser;Ljava/lang/String;Ljava/lang/Object;Ljava/lang/Class;)Lcom/fasterxml/jackson/databind/exc/InvalidFormatException;

    move-result-object p1

    return-object p1
.end method

.method public weirdNumberException(Ljava/lang/Class;Ljava/lang/String;)Lcom/fasterxml/jackson/databind/JsonMappingException;
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/lang/Class<",
            "*>;",
            "Ljava/lang/String;",
            ")",
            "Lcom/fasterxml/jackson/databind/JsonMappingException;"
        }
    .end annotation

    .annotation runtime Ljava/lang/Deprecated;
    .end annotation

    const/4 v0, 0x0

    .line 894
    invoke-virtual {p0, v0, p1, p2}, Lcom/fasterxml/jackson/databind/DeserializationContext;->weirdStringException(Ljava/lang/String;Ljava/lang/Class;Ljava/lang/String;)Lcom/fasterxml/jackson/databind/JsonMappingException;

    move-result-object p1

    return-object p1
.end method

.method public weirdNumberException(Ljava/lang/Number;Ljava/lang/Class;Ljava/lang/String;)Lcom/fasterxml/jackson/databind/JsonMappingException;
    .locals 2
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/lang/Number;",
            "Ljava/lang/Class<",
            "*>;",
            "Ljava/lang/String;",
            ")",
            "Lcom/fasterxml/jackson/databind/JsonMappingException;"
        }
    .end annotation

    .line 902
    iget-object p1, p0, Lcom/fasterxml/jackson/databind/DeserializationContext;->_parser:Lcom/fasterxml/jackson/core/JsonParser;

    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    const-string v1, "Can not construct instance of "

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p2}, Ljava/lang/Class;->getName()Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, " from number value ("

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p0}, Lcom/fasterxml/jackson/databind/DeserializationContext;->_valueDesc()Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, "): "

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0, p3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p3

    const/4 v0, 0x0

    invoke-static {p1, p3, v0, p2}, Lcom/fasterxml/jackson/databind/exc/InvalidFormatException;->from(Lcom/fasterxml/jackson/core/JsonParser;Ljava/lang/String;Ljava/lang/Object;Ljava/lang/Class;)Lcom/fasterxml/jackson/databind/exc/InvalidFormatException;

    move-result-object p1

    return-object p1
.end method

.method public weirdStringException(Ljava/lang/Class;Ljava/lang/String;)Lcom/fasterxml/jackson/databind/JsonMappingException;
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/lang/Class<",
            "*>;",
            "Ljava/lang/String;",
            ")",
            "Lcom/fasterxml/jackson/databind/JsonMappingException;"
        }
    .end annotation

    .annotation runtime Ljava/lang/Deprecated;
    .end annotation

    const/4 v0, 0x0

    .line 869
    invoke-virtual {p0, v0, p1, p2}, Lcom/fasterxml/jackson/databind/DeserializationContext;->weirdStringException(Ljava/lang/String;Ljava/lang/Class;Ljava/lang/String;)Lcom/fasterxml/jackson/databind/JsonMappingException;

    move-result-object p1

    return-object p1
.end method

.method public weirdStringException(Ljava/lang/String;Ljava/lang/Class;Ljava/lang/String;)Lcom/fasterxml/jackson/databind/JsonMappingException;
    .locals 3
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/lang/String;",
            "Ljava/lang/Class<",
            "*>;",
            "Ljava/lang/String;",
            ")",
            "Lcom/fasterxml/jackson/databind/JsonMappingException;"
        }
    .end annotation

    .line 883
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/DeserializationContext;->_parser:Lcom/fasterxml/jackson/core/JsonParser;

    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "Can not construct instance of "

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p2}, Ljava/lang/Class;->getName()Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v2, " from String value \'"

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p0}, Lcom/fasterxml/jackson/databind/DeserializationContext;->_valueDesc()Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v2, "\': "

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1, p3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p3

    invoke-static {v0, p3, p1, p2}, Lcom/fasterxml/jackson/databind/exc/InvalidFormatException;->from(Lcom/fasterxml/jackson/core/JsonParser;Ljava/lang/String;Ljava/lang/Object;Ljava/lang/Class;)Lcom/fasterxml/jackson/databind/exc/InvalidFormatException;

    move-result-object p1

    return-object p1
.end method

.method public wrongTokenException(Lcom/fasterxml/jackson/core/JsonParser;Lcom/fasterxml/jackson/core/JsonToken;Ljava/lang/String;)Lcom/fasterxml/jackson/databind/JsonMappingException;
    .locals 2

    .line 923
    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    const-string v1, "Unexpected token ("

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p1}, Lcom/fasterxml/jackson/core/JsonParser;->getCurrentToken()Lcom/fasterxml/jackson/core/JsonToken;

    move-result-object v1

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/Object;)Ljava/lang/StringBuilder;

    const-string v1, "), expected "

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0, p2}, Ljava/lang/StringBuilder;->append(Ljava/lang/Object;)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p2

    if-eqz p3, :cond_0

    .line 925
    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    invoke-virtual {v0, p2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string p2, ": "

    invoke-virtual {v0, p2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0, p3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p2

    .line 927
    :cond_0
    invoke-static {p1, p2}, Lcom/fasterxml/jackson/databind/JsonMappingException;->from(Lcom/fasterxml/jackson/core/JsonParser;Ljava/lang/String;)Lcom/fasterxml/jackson/databind/JsonMappingException;

    move-result-object p1

    return-object p1
.end method
