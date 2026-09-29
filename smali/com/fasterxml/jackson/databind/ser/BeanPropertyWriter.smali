.class public Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;
.super Lcom/fasterxml/jackson/databind/ser/PropertyWriter;
.source "BeanPropertyWriter.java"

# interfaces
.implements Lcom/fasterxml/jackson/databind/BeanProperty;


# static fields
.field public static final MARKER_FOR_EMPTY:Ljava/lang/Object;


# instance fields
.field protected final _accessorMethod:Ljava/lang/reflect/Method;

.field protected final _cfgSerializationType:Lcom/fasterxml/jackson/databind/JavaType;

.field protected final _contextAnnotations:Lcom/fasterxml/jackson/databind/util/Annotations;

.field protected final _declaredType:Lcom/fasterxml/jackson/databind/JavaType;

.field protected transient _dynamicSerializers:Lcom/fasterxml/jackson/databind/ser/impl/PropertySerializerMap;

.field protected final _field:Ljava/lang/reflect/Field;

.field protected final _includeInViews:[Ljava/lang/Class;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "[",
            "Ljava/lang/Class<",
            "*>;"
        }
    .end annotation
.end field

.field protected _internalSettings:Ljava/util/HashMap;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/HashMap<",
            "Ljava/lang/Object;",
            "Ljava/lang/Object;",
            ">;"
        }
    .end annotation
.end field

.field protected final _member:Lcom/fasterxml/jackson/databind/introspect/AnnotatedMember;

.field protected final _metadata:Lcom/fasterxml/jackson/databind/PropertyMetadata;

.field protected final _name:Lcom/fasterxml/jackson/core/io/SerializedString;

.field protected _nonTrivialBaseType:Lcom/fasterxml/jackson/databind/JavaType;

.field protected _nullSerializer:Lcom/fasterxml/jackson/databind/JsonSerializer;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Lcom/fasterxml/jackson/databind/JsonSerializer<",
            "Ljava/lang/Object;",
            ">;"
        }
    .end annotation
.end field

.field protected _serializer:Lcom/fasterxml/jackson/databind/JsonSerializer;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Lcom/fasterxml/jackson/databind/JsonSerializer<",
            "Ljava/lang/Object;",
            ">;"
        }
    .end annotation
.end field

.field protected final _suppressNulls:Z

.field protected final _suppressableValue:Ljava/lang/Object;

.field protected _typeSerializer:Lcom/fasterxml/jackson/databind/jsontype/TypeSerializer;

.field protected final _wrapperName:Lcom/fasterxml/jackson/databind/PropertyName;


# direct methods
.method static constructor <clinit>()V
    .locals 1

    .line 41
    sget-object v0, Lcom/fasterxml/jackson/annotation/JsonInclude$Include;->NON_EMPTY:Lcom/fasterxml/jackson/annotation/JsonInclude$Include;

    sput-object v0, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->MARKER_FOR_EMPTY:Ljava/lang/Object;

    return-void
.end method

.method protected constructor <init>()V
    .locals 2

    .line 234
    invoke-direct {p0}, Lcom/fasterxml/jackson/databind/ser/PropertyWriter;-><init>()V

    const/4 v0, 0x0

    .line 235
    iput-object v0, p0, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_member:Lcom/fasterxml/jackson/databind/introspect/AnnotatedMember;

    .line 236
    iput-object v0, p0, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_contextAnnotations:Lcom/fasterxml/jackson/databind/util/Annotations;

    .line 238
    iput-object v0, p0, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_name:Lcom/fasterxml/jackson/core/io/SerializedString;

    .line 239
    iput-object v0, p0, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_wrapperName:Lcom/fasterxml/jackson/databind/PropertyName;

    .line 240
    iput-object v0, p0, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_metadata:Lcom/fasterxml/jackson/databind/PropertyMetadata;

    .line 241
    iput-object v0, p0, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_includeInViews:[Ljava/lang/Class;

    .line 243
    iput-object v0, p0, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_declaredType:Lcom/fasterxml/jackson/databind/JavaType;

    .line 244
    iput-object v0, p0, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_serializer:Lcom/fasterxml/jackson/databind/JsonSerializer;

    .line 245
    iput-object v0, p0, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_dynamicSerializers:Lcom/fasterxml/jackson/databind/ser/impl/PropertySerializerMap;

    .line 246
    iput-object v0, p0, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_typeSerializer:Lcom/fasterxml/jackson/databind/jsontype/TypeSerializer;

    .line 247
    iput-object v0, p0, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_cfgSerializationType:Lcom/fasterxml/jackson/databind/JavaType;

    .line 249
    iput-object v0, p0, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_accessorMethod:Ljava/lang/reflect/Method;

    .line 250
    iput-object v0, p0, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_field:Ljava/lang/reflect/Field;

    const/4 v1, 0x0

    .line 251
    iput-boolean v1, p0, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_suppressNulls:Z

    .line 252
    iput-object v0, p0, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_suppressableValue:Ljava/lang/Object;

    .line 254
    iput-object v0, p0, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_nullSerializer:Lcom/fasterxml/jackson/databind/JsonSerializer;

    return-void
.end method

.method public constructor <init>(Lcom/fasterxml/jackson/databind/introspect/BeanPropertyDefinition;Lcom/fasterxml/jackson/databind/introspect/AnnotatedMember;Lcom/fasterxml/jackson/databind/util/Annotations;Lcom/fasterxml/jackson/databind/JavaType;Lcom/fasterxml/jackson/databind/JsonSerializer;Lcom/fasterxml/jackson/databind/jsontype/TypeSerializer;Lcom/fasterxml/jackson/databind/JavaType;ZLjava/lang/Object;)V
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Lcom/fasterxml/jackson/databind/introspect/BeanPropertyDefinition;",
            "Lcom/fasterxml/jackson/databind/introspect/AnnotatedMember;",
            "Lcom/fasterxml/jackson/databind/util/Annotations;",
            "Lcom/fasterxml/jackson/databind/JavaType;",
            "Lcom/fasterxml/jackson/databind/JsonSerializer<",
            "*>;",
            "Lcom/fasterxml/jackson/databind/jsontype/TypeSerializer;",
            "Lcom/fasterxml/jackson/databind/JavaType;",
            "Z",
            "Ljava/lang/Object;",
            ")V"
        }
    .end annotation

    .line 194
    invoke-direct {p0}, Lcom/fasterxml/jackson/databind/ser/PropertyWriter;-><init>()V

    .line 195
    iput-object p2, p0, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_member:Lcom/fasterxml/jackson/databind/introspect/AnnotatedMember;

    .line 196
    iput-object p3, p0, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_contextAnnotations:Lcom/fasterxml/jackson/databind/util/Annotations;

    .line 198
    new-instance p3, Lcom/fasterxml/jackson/core/io/SerializedString;

    invoke-virtual {p1}, Lcom/fasterxml/jackson/databind/introspect/BeanPropertyDefinition;->getName()Ljava/lang/String;

    move-result-object v0

    invoke-direct {p3, v0}, Lcom/fasterxml/jackson/core/io/SerializedString;-><init>(Ljava/lang/String;)V

    iput-object p3, p0, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_name:Lcom/fasterxml/jackson/core/io/SerializedString;

    .line 199
    invoke-virtual {p1}, Lcom/fasterxml/jackson/databind/introspect/BeanPropertyDefinition;->getWrapperName()Lcom/fasterxml/jackson/databind/PropertyName;

    move-result-object p3

    iput-object p3, p0, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_wrapperName:Lcom/fasterxml/jackson/databind/PropertyName;

    .line 200
    invoke-virtual {p1}, Lcom/fasterxml/jackson/databind/introspect/BeanPropertyDefinition;->getMetadata()Lcom/fasterxml/jackson/databind/PropertyMetadata;

    move-result-object p3

    iput-object p3, p0, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_metadata:Lcom/fasterxml/jackson/databind/PropertyMetadata;

    .line 201
    invoke-virtual {p1}, Lcom/fasterxml/jackson/databind/introspect/BeanPropertyDefinition;->findViews()[Ljava/lang/Class;

    move-result-object p1

    iput-object p1, p0, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_includeInViews:[Ljava/lang/Class;

    .line 203
    iput-object p4, p0, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_declaredType:Lcom/fasterxml/jackson/databind/JavaType;

    .line 204
    iput-object p5, p0, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_serializer:Lcom/fasterxml/jackson/databind/JsonSerializer;

    const/4 p1, 0x0

    if-nez p5, :cond_0

    .line 205
    invoke-static {}, Lcom/fasterxml/jackson/databind/ser/impl/PropertySerializerMap;->emptyForProperties()Lcom/fasterxml/jackson/databind/ser/impl/PropertySerializerMap;

    move-result-object p3

    goto :goto_0

    :cond_0
    move-object p3, p1

    :goto_0
    iput-object p3, p0, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_dynamicSerializers:Lcom/fasterxml/jackson/databind/ser/impl/PropertySerializerMap;

    .line 206
    iput-object p6, p0, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_typeSerializer:Lcom/fasterxml/jackson/databind/jsontype/TypeSerializer;

    .line 207
    iput-object p7, p0, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_cfgSerializationType:Lcom/fasterxml/jackson/databind/JavaType;

    .line 209
    instance-of p3, p2, Lcom/fasterxml/jackson/databind/introspect/AnnotatedField;

    if-eqz p3, :cond_1

    .line 210
    iput-object p1, p0, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_accessorMethod:Ljava/lang/reflect/Method;

    .line 211
    invoke-virtual {p2}, Lcom/fasterxml/jackson/databind/introspect/AnnotatedMember;->getMember()Ljava/lang/reflect/Member;

    move-result-object p2

    check-cast p2, Ljava/lang/reflect/Field;

    iput-object p2, p0, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_field:Ljava/lang/reflect/Field;

    goto :goto_1

    .line 212
    :cond_1
    instance-of p3, p2, Lcom/fasterxml/jackson/databind/introspect/AnnotatedMethod;

    if-eqz p3, :cond_2

    .line 213
    invoke-virtual {p2}, Lcom/fasterxml/jackson/databind/introspect/AnnotatedMember;->getMember()Ljava/lang/reflect/Member;

    move-result-object p2

    check-cast p2, Ljava/lang/reflect/Method;

    iput-object p2, p0, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_accessorMethod:Ljava/lang/reflect/Method;

    .line 214
    iput-object p1, p0, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_field:Ljava/lang/reflect/Field;

    goto :goto_1

    .line 217
    :cond_2
    iput-object p1, p0, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_accessorMethod:Ljava/lang/reflect/Method;

    .line 218
    iput-object p1, p0, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_field:Ljava/lang/reflect/Field;

    .line 220
    :goto_1
    iput-boolean p8, p0, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_suppressNulls:Z

    .line 221
    iput-object p9, p0, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_suppressableValue:Ljava/lang/Object;

    .line 224
    iput-object p1, p0, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_nullSerializer:Lcom/fasterxml/jackson/databind/JsonSerializer;

    return-void
.end method

.method protected constructor <init>(Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;)V
    .locals 1

    .line 261
    iget-object v0, p1, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_name:Lcom/fasterxml/jackson/core/io/SerializedString;

    invoke-direct {p0, p1, v0}, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;-><init>(Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;Lcom/fasterxml/jackson/core/io/SerializedString;)V

    return-void
.end method

.method protected constructor <init>(Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;Lcom/fasterxml/jackson/core/io/SerializedString;)V
    .locals 1

    .line 298
    invoke-direct {p0}, Lcom/fasterxml/jackson/databind/ser/PropertyWriter;-><init>()V

    .line 299
    iput-object p2, p0, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_name:Lcom/fasterxml/jackson/core/io/SerializedString;

    .line 300
    iget-object p2, p1, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_wrapperName:Lcom/fasterxml/jackson/databind/PropertyName;

    iput-object p2, p0, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_wrapperName:Lcom/fasterxml/jackson/databind/PropertyName;

    .line 302
    iget-object p2, p1, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_member:Lcom/fasterxml/jackson/databind/introspect/AnnotatedMember;

    iput-object p2, p0, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_member:Lcom/fasterxml/jackson/databind/introspect/AnnotatedMember;

    .line 303
    iget-object p2, p1, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_contextAnnotations:Lcom/fasterxml/jackson/databind/util/Annotations;

    iput-object p2, p0, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_contextAnnotations:Lcom/fasterxml/jackson/databind/util/Annotations;

    .line 304
    iget-object p2, p1, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_declaredType:Lcom/fasterxml/jackson/databind/JavaType;

    iput-object p2, p0, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_declaredType:Lcom/fasterxml/jackson/databind/JavaType;

    .line 305
    iget-object p2, p1, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_accessorMethod:Ljava/lang/reflect/Method;

    iput-object p2, p0, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_accessorMethod:Ljava/lang/reflect/Method;

    .line 306
    iget-object p2, p1, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_field:Ljava/lang/reflect/Field;

    iput-object p2, p0, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_field:Ljava/lang/reflect/Field;

    .line 307
    iget-object p2, p1, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_serializer:Lcom/fasterxml/jackson/databind/JsonSerializer;

    iput-object p2, p0, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_serializer:Lcom/fasterxml/jackson/databind/JsonSerializer;

    .line 308
    iget-object p2, p1, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_nullSerializer:Lcom/fasterxml/jackson/databind/JsonSerializer;

    iput-object p2, p0, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_nullSerializer:Lcom/fasterxml/jackson/databind/JsonSerializer;

    .line 310
    iget-object p2, p1, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_internalSettings:Ljava/util/HashMap;

    if-eqz p2, :cond_0

    .line 311
    new-instance p2, Ljava/util/HashMap;

    iget-object v0, p1, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_internalSettings:Ljava/util/HashMap;

    invoke-direct {p2, v0}, Ljava/util/HashMap;-><init>(Ljava/util/Map;)V

    iput-object p2, p0, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_internalSettings:Ljava/util/HashMap;

    .line 313
    :cond_0
    iget-object p2, p1, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_cfgSerializationType:Lcom/fasterxml/jackson/databind/JavaType;

    iput-object p2, p0, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_cfgSerializationType:Lcom/fasterxml/jackson/databind/JavaType;

    .line 314
    iget-object p2, p1, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_dynamicSerializers:Lcom/fasterxml/jackson/databind/ser/impl/PropertySerializerMap;

    iput-object p2, p0, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_dynamicSerializers:Lcom/fasterxml/jackson/databind/ser/impl/PropertySerializerMap;

    .line 315
    iget-boolean p2, p1, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_suppressNulls:Z

    iput-boolean p2, p0, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_suppressNulls:Z

    .line 316
    iget-object p2, p1, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_suppressableValue:Ljava/lang/Object;

    iput-object p2, p0, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_suppressableValue:Ljava/lang/Object;

    .line 317
    iget-object p2, p1, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_includeInViews:[Ljava/lang/Class;

    iput-object p2, p0, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_includeInViews:[Ljava/lang/Class;

    .line 318
    iget-object p2, p1, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_typeSerializer:Lcom/fasterxml/jackson/databind/jsontype/TypeSerializer;

    iput-object p2, p0, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_typeSerializer:Lcom/fasterxml/jackson/databind/jsontype/TypeSerializer;

    .line 319
    iget-object p2, p1, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_nonTrivialBaseType:Lcom/fasterxml/jackson/databind/JavaType;

    iput-object p2, p0, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_nonTrivialBaseType:Lcom/fasterxml/jackson/databind/JavaType;

    .line 320
    iget-object p1, p1, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_metadata:Lcom/fasterxml/jackson/databind/PropertyMetadata;

    iput-object p1, p0, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_metadata:Lcom/fasterxml/jackson/databind/PropertyMetadata;

    return-void
.end method

.method protected constructor <init>(Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;Lcom/fasterxml/jackson/databind/PropertyName;)V
    .locals 1

    .line 268
    invoke-direct {p0}, Lcom/fasterxml/jackson/databind/ser/PropertyWriter;-><init>()V

    .line 274
    new-instance v0, Lcom/fasterxml/jackson/core/io/SerializedString;

    invoke-virtual {p2}, Lcom/fasterxml/jackson/databind/PropertyName;->getSimpleName()Ljava/lang/String;

    move-result-object p2

    invoke-direct {v0, p2}, Lcom/fasterxml/jackson/core/io/SerializedString;-><init>(Ljava/lang/String;)V

    iput-object v0, p0, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_name:Lcom/fasterxml/jackson/core/io/SerializedString;

    .line 275
    iget-object p2, p1, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_wrapperName:Lcom/fasterxml/jackson/databind/PropertyName;

    iput-object p2, p0, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_wrapperName:Lcom/fasterxml/jackson/databind/PropertyName;

    .line 277
    iget-object p2, p1, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_member:Lcom/fasterxml/jackson/databind/introspect/AnnotatedMember;

    iput-object p2, p0, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_member:Lcom/fasterxml/jackson/databind/introspect/AnnotatedMember;

    .line 278
    iget-object p2, p1, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_contextAnnotations:Lcom/fasterxml/jackson/databind/util/Annotations;

    iput-object p2, p0, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_contextAnnotations:Lcom/fasterxml/jackson/databind/util/Annotations;

    .line 279
    iget-object p2, p1, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_declaredType:Lcom/fasterxml/jackson/databind/JavaType;

    iput-object p2, p0, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_declaredType:Lcom/fasterxml/jackson/databind/JavaType;

    .line 280
    iget-object p2, p1, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_accessorMethod:Ljava/lang/reflect/Method;

    iput-object p2, p0, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_accessorMethod:Ljava/lang/reflect/Method;

    .line 281
    iget-object p2, p1, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_field:Ljava/lang/reflect/Field;

    iput-object p2, p0, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_field:Ljava/lang/reflect/Field;

    .line 282
    iget-object p2, p1, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_serializer:Lcom/fasterxml/jackson/databind/JsonSerializer;

    iput-object p2, p0, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_serializer:Lcom/fasterxml/jackson/databind/JsonSerializer;

    .line 283
    iget-object p2, p1, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_nullSerializer:Lcom/fasterxml/jackson/databind/JsonSerializer;

    iput-object p2, p0, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_nullSerializer:Lcom/fasterxml/jackson/databind/JsonSerializer;

    .line 285
    iget-object p2, p1, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_internalSettings:Ljava/util/HashMap;

    if-eqz p2, :cond_0

    .line 286
    new-instance p2, Ljava/util/HashMap;

    iget-object v0, p1, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_internalSettings:Ljava/util/HashMap;

    invoke-direct {p2, v0}, Ljava/util/HashMap;-><init>(Ljava/util/Map;)V

    iput-object p2, p0, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_internalSettings:Ljava/util/HashMap;

    .line 288
    :cond_0
    iget-object p2, p1, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_cfgSerializationType:Lcom/fasterxml/jackson/databind/JavaType;

    iput-object p2, p0, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_cfgSerializationType:Lcom/fasterxml/jackson/databind/JavaType;

    .line 289
    iget-object p2, p1, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_dynamicSerializers:Lcom/fasterxml/jackson/databind/ser/impl/PropertySerializerMap;

    iput-object p2, p0, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_dynamicSerializers:Lcom/fasterxml/jackson/databind/ser/impl/PropertySerializerMap;

    .line 290
    iget-boolean p2, p1, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_suppressNulls:Z

    iput-boolean p2, p0, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_suppressNulls:Z

    .line 291
    iget-object p2, p1, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_suppressableValue:Ljava/lang/Object;

    iput-object p2, p0, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_suppressableValue:Ljava/lang/Object;

    .line 292
    iget-object p2, p1, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_includeInViews:[Ljava/lang/Class;

    iput-object p2, p0, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_includeInViews:[Ljava/lang/Class;

    .line 293
    iget-object p2, p1, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_typeSerializer:Lcom/fasterxml/jackson/databind/jsontype/TypeSerializer;

    iput-object p2, p0, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_typeSerializer:Lcom/fasterxml/jackson/databind/jsontype/TypeSerializer;

    .line 294
    iget-object p2, p1, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_nonTrivialBaseType:Lcom/fasterxml/jackson/databind/JavaType;

    iput-object p2, p0, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_nonTrivialBaseType:Lcom/fasterxml/jackson/databind/JavaType;

    .line 295
    iget-object p1, p1, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_metadata:Lcom/fasterxml/jackson/databind/PropertyMetadata;

    iput-object p1, p0, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_metadata:Lcom/fasterxml/jackson/databind/PropertyMetadata;

    return-void
.end method


# virtual methods
.method protected _depositSchemaProperty(Lcom/fasterxml/jackson/databind/node/ObjectNode;Lcom/fasterxml/jackson/databind/JsonNode;)V
    .locals 1

    .line 409
    invoke-virtual {p0}, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->getName()Ljava/lang/String;

    move-result-object v0

    invoke-virtual {p1, v0, p2}, Lcom/fasterxml/jackson/databind/node/ObjectNode;->set(Ljava/lang/String;Lcom/fasterxml/jackson/databind/JsonNode;)Lcom/fasterxml/jackson/databind/JsonNode;

    return-void
.end method

.method protected _findAndAddDynamic(Lcom/fasterxml/jackson/databind/ser/impl/PropertySerializerMap;Ljava/lang/Class;Lcom/fasterxml/jackson/databind/SerializerProvider;)Lcom/fasterxml/jackson/databind/JsonSerializer;
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Lcom/fasterxml/jackson/databind/ser/impl/PropertySerializerMap;",
            "Ljava/lang/Class<",
            "*>;",
            "Lcom/fasterxml/jackson/databind/SerializerProvider;",
            ")",
            "Lcom/fasterxml/jackson/databind/JsonSerializer<",
            "Ljava/lang/Object;",
            ">;"
        }
    .end annotation

    .annotation system Ldalvik/annotation/Throws;
        value = {
            Lcom/fasterxml/jackson/databind/JsonMappingException;
        }
    .end annotation

    .line 734
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_nonTrivialBaseType:Lcom/fasterxml/jackson/databind/JavaType;

    if-eqz v0, :cond_0

    .line 735
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_nonTrivialBaseType:Lcom/fasterxml/jackson/databind/JavaType;

    invoke-virtual {p3, v0, p2}, Lcom/fasterxml/jackson/databind/SerializerProvider;->constructSpecializedType(Lcom/fasterxml/jackson/databind/JavaType;Ljava/lang/Class;)Lcom/fasterxml/jackson/databind/JavaType;

    move-result-object p2

    .line 736
    invoke-virtual {p1, p2, p3, p0}, Lcom/fasterxml/jackson/databind/ser/impl/PropertySerializerMap;->findAndAddPrimarySerializer(Lcom/fasterxml/jackson/databind/JavaType;Lcom/fasterxml/jackson/databind/SerializerProvider;Lcom/fasterxml/jackson/databind/BeanProperty;)Lcom/fasterxml/jackson/databind/ser/impl/PropertySerializerMap$SerializerAndMapResult;

    move-result-object p2

    goto :goto_0

    .line 738
    :cond_0
    invoke-virtual {p1, p2, p3, p0}, Lcom/fasterxml/jackson/databind/ser/impl/PropertySerializerMap;->findAndAddPrimarySerializer(Ljava/lang/Class;Lcom/fasterxml/jackson/databind/SerializerProvider;Lcom/fasterxml/jackson/databind/BeanProperty;)Lcom/fasterxml/jackson/databind/ser/impl/PropertySerializerMap$SerializerAndMapResult;

    move-result-object p2

    .line 741
    :goto_0
    iget-object p3, p2, Lcom/fasterxml/jackson/databind/ser/impl/PropertySerializerMap$SerializerAndMapResult;->map:Lcom/fasterxml/jackson/databind/ser/impl/PropertySerializerMap;

    if-eq p1, p3, :cond_1

    .line 742
    iget-object p1, p2, Lcom/fasterxml/jackson/databind/ser/impl/PropertySerializerMap$SerializerAndMapResult;->map:Lcom/fasterxml/jackson/databind/ser/impl/PropertySerializerMap;

    iput-object p1, p0, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_dynamicSerializers:Lcom/fasterxml/jackson/databind/ser/impl/PropertySerializerMap;

    .line 744
    :cond_1
    iget-object p1, p2, Lcom/fasterxml/jackson/databind/ser/impl/PropertySerializerMap$SerializerAndMapResult;->serializer:Lcom/fasterxml/jackson/databind/JsonSerializer;

    return-object p1
.end method

.method protected _handleSelfReference(Ljava/lang/Object;Lcom/fasterxml/jackson/core/JsonGenerator;Lcom/fasterxml/jackson/databind/SerializerProvider;Lcom/fasterxml/jackson/databind/JsonSerializer;)Z
    .locals 0
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/lang/Object;",
            "Lcom/fasterxml/jackson/core/JsonGenerator;",
            "Lcom/fasterxml/jackson/databind/SerializerProvider;",
            "Lcom/fasterxml/jackson/databind/JsonSerializer<",
            "*>;)Z"
        }
    .end annotation

    .annotation system Ldalvik/annotation/Throws;
        value = {
            Lcom/fasterxml/jackson/databind/JsonMappingException;
        }
    .end annotation

    .line 774
    sget-object p1, Lcom/fasterxml/jackson/databind/SerializationFeature;->FAIL_ON_SELF_REFERENCES:Lcom/fasterxml/jackson/databind/SerializationFeature;

    invoke-virtual {p3, p1}, Lcom/fasterxml/jackson/databind/SerializerProvider;->isEnabled(Lcom/fasterxml/jackson/databind/SerializationFeature;)Z

    move-result p1

    if-eqz p1, :cond_1

    invoke-virtual {p4}, Lcom/fasterxml/jackson/databind/JsonSerializer;->usesObjectId()Z

    move-result p1

    if-nez p1, :cond_1

    .line 780
    instance-of p1, p4, Lcom/fasterxml/jackson/databind/ser/std/BeanSerializerBase;

    if-nez p1, :cond_0

    goto :goto_0

    .line 781
    :cond_0
    new-instance p1, Lcom/fasterxml/jackson/databind/JsonMappingException;

    const-string p2, "Direct self-reference leading to cycle"

    invoke-direct {p1, p2}, Lcom/fasterxml/jackson/databind/JsonMappingException;-><init>(Ljava/lang/String;)V

    throw p1

    :cond_1
    :goto_0
    const/4 p1, 0x0

    return p1
.end method

.method public assignNullSerializer(Lcom/fasterxml/jackson/databind/JsonSerializer;)V
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Lcom/fasterxml/jackson/databind/JsonSerializer<",
            "Ljava/lang/Object;",
            ">;)V"
        }
    .end annotation

    .line 351
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_nullSerializer:Lcom/fasterxml/jackson/databind/JsonSerializer;

    if-eqz v0, :cond_1

    iget-object v0, p0, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_nullSerializer:Lcom/fasterxml/jackson/databind/JsonSerializer;

    if-ne v0, p1, :cond_0

    goto :goto_0

    .line 352
    :cond_0
    new-instance p1, Ljava/lang/IllegalStateException;

    const-string v0, "Can not override null serializer"

    invoke-direct {p1, v0}, Ljava/lang/IllegalStateException;-><init>(Ljava/lang/String;)V

    throw p1

    .line 354
    :cond_1
    :goto_0
    iput-object p1, p0, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_nullSerializer:Lcom/fasterxml/jackson/databind/JsonSerializer;

    return-void
.end method

.method public assignSerializer(Lcom/fasterxml/jackson/databind/JsonSerializer;)V
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Lcom/fasterxml/jackson/databind/JsonSerializer<",
            "Ljava/lang/Object;",
            ">;)V"
        }
    .end annotation

    .line 338
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_serializer:Lcom/fasterxml/jackson/databind/JsonSerializer;

    if-eqz v0, :cond_1

    iget-object v0, p0, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_serializer:Lcom/fasterxml/jackson/databind/JsonSerializer;

    if-ne v0, p1, :cond_0

    goto :goto_0

    .line 339
    :cond_0
    new-instance p1, Ljava/lang/IllegalStateException;

    const-string v0, "Can not override serializer"

    invoke-direct {p1, v0}, Ljava/lang/IllegalStateException;-><init>(Ljava/lang/String;)V

    throw p1

    .line 341
    :cond_1
    :goto_0
    iput-object p1, p0, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_serializer:Lcom/fasterxml/jackson/databind/JsonSerializer;

    return-void
.end method

.method public depositSchemaProperty(Lcom/fasterxml/jackson/databind/jsonFormatVisitors/JsonObjectFormatVisitor;)V
    .locals 1
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Lcom/fasterxml/jackson/databind/JsonMappingException;
        }
    .end annotation

    if-eqz p1, :cond_1

    .line 683
    invoke-virtual {p0}, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->isRequired()Z

    move-result v0

    if-eqz v0, :cond_0

    .line 684
    invoke-interface {p1, p0}, Lcom/fasterxml/jackson/databind/jsonFormatVisitors/JsonObjectFormatVisitor;->property(Lcom/fasterxml/jackson/databind/BeanProperty;)V

    goto :goto_0

    .line 686
    :cond_0
    invoke-interface {p1, p0}, Lcom/fasterxml/jackson/databind/jsonFormatVisitors/JsonObjectFormatVisitor;->optionalProperty(Lcom/fasterxml/jackson/databind/BeanProperty;)V

    :cond_1
    :goto_0
    return-void
.end method

.method public depositSchemaProperty(Lcom/fasterxml/jackson/databind/node/ObjectNode;Lcom/fasterxml/jackson/databind/SerializerProvider;)V
    .locals 4
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Lcom/fasterxml/jackson/databind/JsonMappingException;
        }
    .end annotation

    .annotation runtime Ljava/lang/Deprecated;
    .end annotation

    .line 706
    invoke-virtual {p0}, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->getSerializationType()Lcom/fasterxml/jackson/databind/JavaType;

    move-result-object v0

    if-nez v0, :cond_0

    .line 708
    invoke-virtual {p0}, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->getGenericPropertyType()Ljava/lang/reflect/Type;

    move-result-object v0

    goto :goto_0

    :cond_0
    invoke-virtual {v0}, Lcom/fasterxml/jackson/databind/JavaType;->getRawClass()Ljava/lang/Class;

    move-result-object v0

    .line 711
    :goto_0
    invoke-virtual {p0}, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->getSerializer()Lcom/fasterxml/jackson/databind/JsonSerializer;

    move-result-object v1

    if-nez v1, :cond_1

    .line 713
    invoke-virtual {p0}, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->getType()Lcom/fasterxml/jackson/databind/JavaType;

    move-result-object v1

    invoke-virtual {p2, v1, p0}, Lcom/fasterxml/jackson/databind/SerializerProvider;->findValueSerializer(Lcom/fasterxml/jackson/databind/JavaType;Lcom/fasterxml/jackson/databind/BeanProperty;)Lcom/fasterxml/jackson/databind/JsonSerializer;

    move-result-object v1

    .line 715
    :cond_1
    invoke-virtual {p0}, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->isRequired()Z

    move-result v2

    xor-int/lit8 v2, v2, 0x1

    .line 716
    instance-of v3, v1, Lcom/fasterxml/jackson/databind/jsonschema/SchemaAware;

    if-eqz v3, :cond_2

    .line 717
    check-cast v1, Lcom/fasterxml/jackson/databind/jsonschema/SchemaAware;

    invoke-interface {v1, p2, v0, v2}, Lcom/fasterxml/jackson/databind/jsonschema/SchemaAware;->getSchema(Lcom/fasterxml/jackson/databind/SerializerProvider;Ljava/lang/reflect/Type;Z)Lcom/fasterxml/jackson/databind/JsonNode;

    move-result-object p2

    goto :goto_1

    .line 719
    :cond_2
    invoke-static {}, Lcom/fasterxml/jackson/databind/jsonschema/JsonSchema;->getDefaultSchemaNode()Lcom/fasterxml/jackson/databind/JsonNode;

    move-result-object p2

    .line 721
    :goto_1
    invoke-virtual {p0, p1, p2}, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_depositSchemaProperty(Lcom/fasterxml/jackson/databind/node/ObjectNode;Lcom/fasterxml/jackson/databind/JsonNode;)V

    return-void
.end method

.method public final get(Ljava/lang/Object;)Ljava/lang/Object;
    .locals 2
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/lang/Exception;
        }
    .end annotation

    .line 756
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_accessorMethod:Ljava/lang/reflect/Method;

    if-nez v0, :cond_0

    iget-object v0, p0, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_field:Ljava/lang/reflect/Field;

    invoke-virtual {v0, p1}, Ljava/lang/reflect/Field;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object p1

    goto :goto_0

    :cond_0
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_accessorMethod:Ljava/lang/reflect/Method;

    const/4 v1, 0x0

    new-array v1, v1, [Ljava/lang/Object;

    invoke-virtual {v0, p1, v1}, Ljava/lang/reflect/Method;->invoke(Ljava/lang/Object;[Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object p1

    :goto_0
    return-object p1
.end method

.method public getAnnotation(Ljava/lang/Class;)Ljava/lang/annotation/Annotation;
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "<A::",
            "Ljava/lang/annotation/Annotation;",
            ">(",
            "Ljava/lang/Class<",
            "TA;>;)TA;"
        }
    .end annotation

    .line 396
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_member:Lcom/fasterxml/jackson/databind/introspect/AnnotatedMember;

    if-nez v0, :cond_0

    const/4 p1, 0x0

    goto :goto_0

    :cond_0
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_member:Lcom/fasterxml/jackson/databind/introspect/AnnotatedMember;

    invoke-virtual {v0, p1}, Lcom/fasterxml/jackson/databind/introspect/AnnotatedMember;->getAnnotation(Ljava/lang/Class;)Ljava/lang/annotation/Annotation;

    move-result-object p1

    :goto_0
    return-object p1
.end method

.method public getContextAnnotation(Ljava/lang/Class;)Ljava/lang/annotation/Annotation;
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "<A::",
            "Ljava/lang/annotation/Annotation;",
            ">(",
            "Ljava/lang/Class<",
            "TA;>;)TA;"
        }
    .end annotation

    .line 402
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_contextAnnotations:Lcom/fasterxml/jackson/databind/util/Annotations;

    if-nez v0, :cond_0

    const/4 p1, 0x0

    goto :goto_0

    :cond_0
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_contextAnnotations:Lcom/fasterxml/jackson/databind/util/Annotations;

    invoke-interface {v0, p1}, Lcom/fasterxml/jackson/databind/util/Annotations;->get(Ljava/lang/Class;)Ljava/lang/annotation/Annotation;

    move-result-object p1

    :goto_0
    return-object p1
.end method

.method public getFullName()Lcom/fasterxml/jackson/databind/PropertyName;
    .locals 2

    .line 385
    new-instance v0, Lcom/fasterxml/jackson/databind/PropertyName;

    iget-object v1, p0, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_name:Lcom/fasterxml/jackson/core/io/SerializedString;

    invoke-virtual {v1}, Lcom/fasterxml/jackson/core/io/SerializedString;->getValue()Ljava/lang/String;

    move-result-object v1

    invoke-direct {v0, v1}, Lcom/fasterxml/jackson/databind/PropertyName;-><init>(Ljava/lang/String;)V

    return-object v0
.end method

.method public getGenericPropertyType()Ljava/lang/reflect/Type;
    .locals 1

    .line 510
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_accessorMethod:Ljava/lang/reflect/Method;

    if-eqz v0, :cond_0

    .line 511
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_accessorMethod:Ljava/lang/reflect/Method;

    invoke-virtual {v0}, Ljava/lang/reflect/Method;->getGenericReturnType()Ljava/lang/reflect/Type;

    move-result-object v0

    return-object v0

    .line 513
    :cond_0
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_field:Ljava/lang/reflect/Field;

    if-eqz v0, :cond_1

    .line 514
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_field:Ljava/lang/reflect/Field;

    invoke-virtual {v0}, Ljava/lang/reflect/Field;->getGenericType()Ljava/lang/reflect/Type;

    move-result-object v0

    return-object v0

    :cond_1
    const/4 v0, 0x0

    return-object v0
.end method

.method public getInternalSetting(Ljava/lang/Object;)Ljava/lang/Object;
    .locals 1

    .line 435
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_internalSettings:Ljava/util/HashMap;

    if-nez v0, :cond_0

    const/4 p1, 0x0

    goto :goto_0

    :cond_0
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_internalSettings:Ljava/util/HashMap;

    invoke-virtual {v0, p1}, Ljava/util/HashMap;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object p1

    :goto_0
    return-object p1
.end method

.method public getMember()Lcom/fasterxml/jackson/databind/introspect/AnnotatedMember;
    .locals 1

    .line 405
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_member:Lcom/fasterxml/jackson/databind/introspect/AnnotatedMember;

    return-object v0
.end method

.method public getMetadata()Lcom/fasterxml/jackson/databind/PropertyMetadata;
    .locals 1

    .line 391
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_metadata:Lcom/fasterxml/jackson/databind/PropertyMetadata;

    return-object v0
.end method

.method public getName()Ljava/lang/String;
    .locals 1

    .line 381
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_name:Lcom/fasterxml/jackson/core/io/SerializedString;

    invoke-virtual {v0}, Lcom/fasterxml/jackson/core/io/SerializedString;->getValue()Ljava/lang/String;

    move-result-object v0

    return-object v0
.end method

.method public getPropertyType()Ljava/lang/Class;
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()",
            "Ljava/lang/Class<",
            "*>;"
        }
    .end annotation

    .line 501
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_accessorMethod:Ljava/lang/reflect/Method;

    if-eqz v0, :cond_0

    iget-object v0, p0, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_accessorMethod:Ljava/lang/reflect/Method;

    invoke-virtual {v0}, Ljava/lang/reflect/Method;->getReturnType()Ljava/lang/Class;

    move-result-object v0

    goto :goto_0

    :cond_0
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_field:Ljava/lang/reflect/Field;

    invoke-virtual {v0}, Ljava/lang/reflect/Field;->getType()Ljava/lang/Class;

    move-result-object v0

    :goto_0
    return-object v0
.end method

.method public getRawSerializationType()Ljava/lang/Class;
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()",
            "Ljava/lang/Class<",
            "*>;"
        }
    .end annotation

    .line 497
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_cfgSerializationType:Lcom/fasterxml/jackson/databind/JavaType;

    if-nez v0, :cond_0

    const/4 v0, 0x0

    goto :goto_0

    :cond_0
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_cfgSerializationType:Lcom/fasterxml/jackson/databind/JavaType;

    invoke-virtual {v0}, Lcom/fasterxml/jackson/databind/JavaType;->getRawClass()Ljava/lang/Class;

    move-result-object v0

    :goto_0
    return-object v0
.end method

.method public getSerializationType()Lcom/fasterxml/jackson/databind/JavaType;
    .locals 1

    .line 494
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_cfgSerializationType:Lcom/fasterxml/jackson/databind/JavaType;

    return-object v0
.end method

.method public getSerializedName()Lcom/fasterxml/jackson/core/SerializableString;
    .locals 1

    .line 473
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_name:Lcom/fasterxml/jackson/core/io/SerializedString;

    return-object v0
.end method

.method public getSerializer()Lcom/fasterxml/jackson/databind/JsonSerializer;
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()",
            "Lcom/fasterxml/jackson/databind/JsonSerializer<",
            "Ljava/lang/Object;",
            ">;"
        }
    .end annotation

    .line 492
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_serializer:Lcom/fasterxml/jackson/databind/JsonSerializer;

    return-object v0
.end method

.method public getType()Lcom/fasterxml/jackson/databind/JavaType;
    .locals 1

    .line 388
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_declaredType:Lcom/fasterxml/jackson/databind/JavaType;

    return-object v0
.end method

.method public getViews()[Ljava/lang/Class;
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()[",
            "Ljava/lang/Class<",
            "*>;"
        }
    .end annotation

    .line 519
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_includeInViews:[Ljava/lang/Class;

    return-object v0
.end method

.method public getWrapperName()Lcom/fasterxml/jackson/databind/PropertyName;
    .locals 1

    .line 389
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_wrapperName:Lcom/fasterxml/jackson/databind/PropertyName;

    return-object v0
.end method

.method public hasNullSerializer()Z
    .locals 1

    .line 476
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_nullSerializer:Lcom/fasterxml/jackson/databind/JsonSerializer;

    if-eqz v0, :cond_0

    const/4 v0, 0x1

    goto :goto_0

    :cond_0
    const/4 v0, 0x0

    :goto_0
    return v0
.end method

.method public hasSerializer()Z
    .locals 1

    .line 475
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_serializer:Lcom/fasterxml/jackson/databind/JsonSerializer;

    if-eqz v0, :cond_0

    const/4 v0, 0x1

    goto :goto_0

    :cond_0
    const/4 v0, 0x0

    :goto_0
    return v0
.end method

.method public isRequired()Z
    .locals 1

    .line 390
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_metadata:Lcom/fasterxml/jackson/databind/PropertyMetadata;

    invoke-virtual {v0}, Lcom/fasterxml/jackson/databind/PropertyMetadata;->isRequired()Z

    move-result v0

    return v0
.end method

.method public isUnwrapping()Z
    .locals 1

    const/4 v0, 0x0

    return v0
.end method

.method public isVirtual()Z
    .locals 1

    const/4 v0, 0x0

    return v0
.end method

.method public removeInternalSetting(Ljava/lang/Object;)Ljava/lang/Object;
    .locals 2

    .line 457
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_internalSettings:Ljava/util/HashMap;

    const/4 v1, 0x0

    if-eqz v0, :cond_0

    .line 458
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_internalSettings:Ljava/util/HashMap;

    invoke-virtual {v0, p1}, Ljava/util/HashMap;->remove(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object p1

    .line 460
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_internalSettings:Ljava/util/HashMap;

    invoke-virtual {v0}, Ljava/util/HashMap;->size()I

    move-result v0

    if-nez v0, :cond_1

    .line 461
    iput-object v1, p0, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_internalSettings:Ljava/util/HashMap;

    goto :goto_0

    :cond_0
    move-object p1, v1

    :cond_1
    :goto_0
    return-object p1
.end method

.method public rename(Lcom/fasterxml/jackson/databind/util/NameTransformer;)Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;
    .locals 2

    .line 324
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_name:Lcom/fasterxml/jackson/core/io/SerializedString;

    invoke-virtual {v0}, Lcom/fasterxml/jackson/core/io/SerializedString;->getValue()Ljava/lang/String;

    move-result-object v0

    invoke-virtual {p1, v0}, Lcom/fasterxml/jackson/databind/util/NameTransformer;->transform(Ljava/lang/String;)Ljava/lang/String;

    move-result-object p1

    .line 325
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_name:Lcom/fasterxml/jackson/core/io/SerializedString;

    invoke-virtual {v0}, Lcom/fasterxml/jackson/core/io/SerializedString;->toString()Ljava/lang/String;

    move-result-object v0

    invoke-virtual {p1, v0}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v0

    if-eqz v0, :cond_0

    return-object p0

    .line 328
    :cond_0
    new-instance v0, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;

    new-instance v1, Lcom/fasterxml/jackson/databind/PropertyName;

    invoke-direct {v1, p1}, Lcom/fasterxml/jackson/databind/PropertyName;-><init>(Ljava/lang/String;)V

    invoke-direct {v0, p0, v1}, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;-><init>(Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;Lcom/fasterxml/jackson/databind/PropertyName;)V

    return-object v0
.end method

.method public serializeAsElement(Ljava/lang/Object;Lcom/fasterxml/jackson/core/JsonGenerator;Lcom/fasterxml/jackson/databind/SerializerProvider;)V
    .locals 4
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/lang/Exception;
        }
    .end annotation

    .line 608
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_accessorMethod:Ljava/lang/reflect/Method;

    if-nez v0, :cond_0

    iget-object v0, p0, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_field:Ljava/lang/reflect/Field;

    invoke-virtual {v0, p1}, Ljava/lang/reflect/Field;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v0

    goto :goto_0

    :cond_0
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_accessorMethod:Ljava/lang/reflect/Method;

    const/4 v1, 0x0

    new-array v1, v1, [Ljava/lang/Object;

    invoke-virtual {v0, p1, v1}, Ljava/lang/reflect/Method;->invoke(Ljava/lang/Object;[Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v0

    :goto_0
    if-nez v0, :cond_2

    .line 610
    iget-object p1, p0, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_nullSerializer:Lcom/fasterxml/jackson/databind/JsonSerializer;

    if-eqz p1, :cond_1

    .line 611
    iget-object p1, p0, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_nullSerializer:Lcom/fasterxml/jackson/databind/JsonSerializer;

    const/4 v0, 0x0

    invoke-virtual {p1, v0, p2, p3}, Lcom/fasterxml/jackson/databind/JsonSerializer;->serialize(Ljava/lang/Object;Lcom/fasterxml/jackson/core/JsonGenerator;Lcom/fasterxml/jackson/databind/SerializerProvider;)V

    goto :goto_1

    .line 613
    :cond_1
    invoke-virtual {p2}, Lcom/fasterxml/jackson/core/JsonGenerator;->writeNull()V

    :goto_1
    return-void

    .line 618
    :cond_2
    iget-object v1, p0, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_serializer:Lcom/fasterxml/jackson/databind/JsonSerializer;

    if-nez v1, :cond_4

    .line 620
    invoke-virtual {v0}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    move-result-object v1

    .line 621
    iget-object v2, p0, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_dynamicSerializers:Lcom/fasterxml/jackson/databind/ser/impl/PropertySerializerMap;

    .line 622
    invoke-virtual {v2, v1}, Lcom/fasterxml/jackson/databind/ser/impl/PropertySerializerMap;->serializerFor(Ljava/lang/Class;)Lcom/fasterxml/jackson/databind/JsonSerializer;

    move-result-object v3

    if-nez v3, :cond_3

    .line 624
    invoke-virtual {p0, v2, v1, p3}, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_findAndAddDynamic(Lcom/fasterxml/jackson/databind/ser/impl/PropertySerializerMap;Ljava/lang/Class;Lcom/fasterxml/jackson/databind/SerializerProvider;)Lcom/fasterxml/jackson/databind/JsonSerializer;

    move-result-object v1

    goto :goto_2

    :cond_3
    move-object v1, v3

    .line 628
    :cond_4
    :goto_2
    iget-object v2, p0, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_suppressableValue:Ljava/lang/Object;

    if-eqz v2, :cond_6

    .line 629
    sget-object v2, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->MARKER_FOR_EMPTY:Ljava/lang/Object;

    iget-object v3, p0, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_suppressableValue:Ljava/lang/Object;

    if-ne v2, v3, :cond_5

    .line 630
    invoke-virtual {v1, p3, v0}, Lcom/fasterxml/jackson/databind/JsonSerializer;->isEmpty(Lcom/fasterxml/jackson/databind/SerializerProvider;Ljava/lang/Object;)Z

    move-result v2

    if-eqz v2, :cond_6

    .line 631
    invoke-virtual {p0, p1, p2, p3}, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->serializeAsPlaceholder(Ljava/lang/Object;Lcom/fasterxml/jackson/core/JsonGenerator;Lcom/fasterxml/jackson/databind/SerializerProvider;)V

    return-void

    .line 634
    :cond_5
    iget-object v2, p0, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_suppressableValue:Ljava/lang/Object;

    invoke-virtual {v2, v0}, Ljava/lang/Object;->equals(Ljava/lang/Object;)Z

    move-result v2

    if-eqz v2, :cond_6

    .line 635
    invoke-virtual {p0, p1, p2, p3}, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->serializeAsPlaceholder(Ljava/lang/Object;Lcom/fasterxml/jackson/core/JsonGenerator;Lcom/fasterxml/jackson/databind/SerializerProvider;)V

    return-void

    :cond_6
    if-ne v0, p1, :cond_7

    .line 641
    invoke-virtual {p0, p1, p2, p3, v1}, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_handleSelfReference(Ljava/lang/Object;Lcom/fasterxml/jackson/core/JsonGenerator;Lcom/fasterxml/jackson/databind/SerializerProvider;Lcom/fasterxml/jackson/databind/JsonSerializer;)Z

    move-result p1

    if-eqz p1, :cond_7

    return-void

    .line 645
    :cond_7
    iget-object p1, p0, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_typeSerializer:Lcom/fasterxml/jackson/databind/jsontype/TypeSerializer;

    if-nez p1, :cond_8

    .line 646
    invoke-virtual {v1, v0, p2, p3}, Lcom/fasterxml/jackson/databind/JsonSerializer;->serialize(Ljava/lang/Object;Lcom/fasterxml/jackson/core/JsonGenerator;Lcom/fasterxml/jackson/databind/SerializerProvider;)V

    goto :goto_3

    .line 648
    :cond_8
    iget-object p1, p0, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_typeSerializer:Lcom/fasterxml/jackson/databind/jsontype/TypeSerializer;

    invoke-virtual {v1, v0, p2, p3, p1}, Lcom/fasterxml/jackson/databind/JsonSerializer;->serializeWithType(Ljava/lang/Object;Lcom/fasterxml/jackson/core/JsonGenerator;Lcom/fasterxml/jackson/databind/SerializerProvider;Lcom/fasterxml/jackson/databind/jsontype/TypeSerializer;)V

    :goto_3
    return-void
.end method

.method public serializeAsField(Ljava/lang/Object;Lcom/fasterxml/jackson/core/JsonGenerator;Lcom/fasterxml/jackson/databind/SerializerProvider;)V
    .locals 4
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/lang/Exception;
        }
    .end annotation

    .line 536
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_accessorMethod:Ljava/lang/reflect/Method;

    if-nez v0, :cond_0

    iget-object v0, p0, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_field:Ljava/lang/reflect/Field;

    invoke-virtual {v0, p1}, Ljava/lang/reflect/Field;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v0

    goto :goto_0

    :cond_0
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_accessorMethod:Ljava/lang/reflect/Method;

    const/4 v1, 0x0

    new-array v1, v1, [Ljava/lang/Object;

    invoke-virtual {v0, p1, v1}, Ljava/lang/reflect/Method;->invoke(Ljava/lang/Object;[Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v0

    :goto_0
    if-nez v0, :cond_2

    .line 540
    iget-object p1, p0, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_nullSerializer:Lcom/fasterxml/jackson/databind/JsonSerializer;

    if-eqz p1, :cond_1

    .line 541
    iget-object p1, p0, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_name:Lcom/fasterxml/jackson/core/io/SerializedString;

    invoke-virtual {p2, p1}, Lcom/fasterxml/jackson/core/JsonGenerator;->writeFieldName(Lcom/fasterxml/jackson/core/SerializableString;)V

    .line 542
    iget-object p1, p0, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_nullSerializer:Lcom/fasterxml/jackson/databind/JsonSerializer;

    const/4 v0, 0x0

    invoke-virtual {p1, v0, p2, p3}, Lcom/fasterxml/jackson/databind/JsonSerializer;->serialize(Ljava/lang/Object;Lcom/fasterxml/jackson/core/JsonGenerator;Lcom/fasterxml/jackson/databind/SerializerProvider;)V

    :cond_1
    return-void

    .line 547
    :cond_2
    iget-object v1, p0, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_serializer:Lcom/fasterxml/jackson/databind/JsonSerializer;

    if-nez v1, :cond_4

    .line 549
    invoke-virtual {v0}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    move-result-object v1

    .line 550
    iget-object v2, p0, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_dynamicSerializers:Lcom/fasterxml/jackson/databind/ser/impl/PropertySerializerMap;

    .line 551
    invoke-virtual {v2, v1}, Lcom/fasterxml/jackson/databind/ser/impl/PropertySerializerMap;->serializerFor(Ljava/lang/Class;)Lcom/fasterxml/jackson/databind/JsonSerializer;

    move-result-object v3

    if-nez v3, :cond_3

    .line 553
    invoke-virtual {p0, v2, v1, p3}, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_findAndAddDynamic(Lcom/fasterxml/jackson/databind/ser/impl/PropertySerializerMap;Ljava/lang/Class;Lcom/fasterxml/jackson/databind/SerializerProvider;)Lcom/fasterxml/jackson/databind/JsonSerializer;

    move-result-object v1

    goto :goto_1

    :cond_3
    move-object v1, v3

    .line 557
    :cond_4
    :goto_1
    iget-object v2, p0, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_suppressableValue:Ljava/lang/Object;

    if-eqz v2, :cond_6

    .line 558
    sget-object v2, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->MARKER_FOR_EMPTY:Ljava/lang/Object;

    iget-object v3, p0, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_suppressableValue:Ljava/lang/Object;

    if-ne v2, v3, :cond_5

    .line 559
    invoke-virtual {v1, p3, v0}, Lcom/fasterxml/jackson/databind/JsonSerializer;->isEmpty(Lcom/fasterxml/jackson/databind/SerializerProvider;Ljava/lang/Object;)Z

    move-result v2

    if-eqz v2, :cond_6

    return-void

    .line 562
    :cond_5
    iget-object v2, p0, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_suppressableValue:Ljava/lang/Object;

    invoke-virtual {v2, v0}, Ljava/lang/Object;->equals(Ljava/lang/Object;)Z

    move-result v2

    if-eqz v2, :cond_6

    return-void

    :cond_6
    if-ne v0, p1, :cond_7

    .line 569
    invoke-virtual {p0, p1, p2, p3, v1}, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_handleSelfReference(Ljava/lang/Object;Lcom/fasterxml/jackson/core/JsonGenerator;Lcom/fasterxml/jackson/databind/SerializerProvider;Lcom/fasterxml/jackson/databind/JsonSerializer;)Z

    move-result p1

    if-eqz p1, :cond_7

    return-void

    .line 573
    :cond_7
    iget-object p1, p0, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_name:Lcom/fasterxml/jackson/core/io/SerializedString;

    invoke-virtual {p2, p1}, Lcom/fasterxml/jackson/core/JsonGenerator;->writeFieldName(Lcom/fasterxml/jackson/core/SerializableString;)V

    .line 574
    iget-object p1, p0, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_typeSerializer:Lcom/fasterxml/jackson/databind/jsontype/TypeSerializer;

    if-nez p1, :cond_8

    .line 575
    invoke-virtual {v1, v0, p2, p3}, Lcom/fasterxml/jackson/databind/JsonSerializer;->serialize(Ljava/lang/Object;Lcom/fasterxml/jackson/core/JsonGenerator;Lcom/fasterxml/jackson/databind/SerializerProvider;)V

    goto :goto_2

    .line 577
    :cond_8
    iget-object p1, p0, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_typeSerializer:Lcom/fasterxml/jackson/databind/jsontype/TypeSerializer;

    invoke-virtual {v1, v0, p2, p3, p1}, Lcom/fasterxml/jackson/databind/JsonSerializer;->serializeWithType(Ljava/lang/Object;Lcom/fasterxml/jackson/core/JsonGenerator;Lcom/fasterxml/jackson/databind/SerializerProvider;Lcom/fasterxml/jackson/databind/jsontype/TypeSerializer;)V

    :goto_2
    return-void
.end method

.method public serializeAsOmittedField(Ljava/lang/Object;Lcom/fasterxml/jackson/core/JsonGenerator;Lcom/fasterxml/jackson/databind/SerializerProvider;)V
    .locals 0
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/lang/Exception;
        }
    .end annotation

    .line 591
    invoke-virtual {p2}, Lcom/fasterxml/jackson/core/JsonGenerator;->canOmitFields()Z

    move-result p1

    if-nez p1, :cond_0

    .line 592
    iget-object p1, p0, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_name:Lcom/fasterxml/jackson/core/io/SerializedString;

    invoke-virtual {p1}, Lcom/fasterxml/jackson/core/io/SerializedString;->getValue()Ljava/lang/String;

    move-result-object p1

    invoke-virtual {p2, p1}, Lcom/fasterxml/jackson/core/JsonGenerator;->writeOmittedField(Ljava/lang/String;)V

    :cond_0
    return-void
.end method

.method public serializeAsPlaceholder(Ljava/lang/Object;Lcom/fasterxml/jackson/core/JsonGenerator;Lcom/fasterxml/jackson/databind/SerializerProvider;)V
    .locals 1
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/lang/Exception;
        }
    .end annotation

    .line 664
    iget-object p1, p0, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_nullSerializer:Lcom/fasterxml/jackson/databind/JsonSerializer;

    if-eqz p1, :cond_0

    .line 665
    iget-object p1, p0, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_nullSerializer:Lcom/fasterxml/jackson/databind/JsonSerializer;

    const/4 v0, 0x0

    invoke-virtual {p1, v0, p2, p3}, Lcom/fasterxml/jackson/databind/JsonSerializer;->serialize(Ljava/lang/Object;Lcom/fasterxml/jackson/core/JsonGenerator;Lcom/fasterxml/jackson/databind/SerializerProvider;)V

    goto :goto_0

    .line 667
    :cond_0
    invoke-virtual {p2}, Lcom/fasterxml/jackson/core/JsonGenerator;->writeNull()V

    :goto_0
    return-void
.end method

.method public setInternalSetting(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;
    .locals 1

    .line 444
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_internalSettings:Ljava/util/HashMap;

    if-nez v0, :cond_0

    .line 445
    new-instance v0, Ljava/util/HashMap;

    invoke-direct {v0}, Ljava/util/HashMap;-><init>()V

    iput-object v0, p0, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_internalSettings:Ljava/util/HashMap;

    .line 447
    :cond_0
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_internalSettings:Ljava/util/HashMap;

    invoke-virtual {v0, p1, p2}, Ljava/util/HashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object p1

    return-object p1
.end method

.method public setNonTrivialBaseType(Lcom/fasterxml/jackson/databind/JavaType;)V
    .locals 0

    .line 371
    iput-object p1, p0, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_nonTrivialBaseType:Lcom/fasterxml/jackson/databind/JavaType;

    return-void
.end method

.method public toString()Ljava/lang/String;
    .locals 3

    .line 789
    new-instance v0, Ljava/lang/StringBuilder;

    const/16 v1, 0x28

    invoke-direct {v0, v1}, Ljava/lang/StringBuilder;-><init>(I)V

    const-string v1, "property \'"

    .line 790
    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p0}, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->getName()Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, "\' ("

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    .line 791
    iget-object v1, p0, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_accessorMethod:Ljava/lang/reflect/Method;

    if-eqz v1, :cond_0

    const-string v1, "via method "

    .line 792
    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v1, p0, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_accessorMethod:Ljava/lang/reflect/Method;

    invoke-virtual {v1}, Ljava/lang/reflect/Method;->getDeclaringClass()Ljava/lang/Class;

    move-result-object v1

    invoke-virtual {v1}, Ljava/lang/Class;->getName()Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, "#"

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v1, p0, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_accessorMethod:Ljava/lang/reflect/Method;

    invoke-virtual {v1}, Ljava/lang/reflect/Method;->getName()Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    goto :goto_0

    .line 793
    :cond_0
    iget-object v1, p0, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_field:Ljava/lang/reflect/Field;

    if-eqz v1, :cond_1

    const-string v1, "field \""

    .line 794
    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v1, p0, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_field:Ljava/lang/reflect/Field;

    invoke-virtual {v1}, Ljava/lang/reflect/Field;->getDeclaringClass()Ljava/lang/Class;

    move-result-object v1

    invoke-virtual {v1}, Ljava/lang/Class;->getName()Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, "#"

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v1, p0, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_field:Ljava/lang/reflect/Field;

    invoke-virtual {v1}, Ljava/lang/reflect/Field;->getName()Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    goto :goto_0

    :cond_1
    const-string v1, "virtual"

    .line 796
    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    .line 798
    :goto_0
    iget-object v1, p0, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_serializer:Lcom/fasterxml/jackson/databind/JsonSerializer;

    if-nez v1, :cond_2

    const-string v1, ", no static serializer"

    .line 799
    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    goto :goto_1

    .line 801
    :cond_2
    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, ", static serializer of type "

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v2, p0, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_serializer:Lcom/fasterxml/jackson/databind/JsonSerializer;

    invoke-virtual {v2}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    move-result-object v2

    invoke-virtual {v2}, Ljava/lang/Class;->getName()Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    :goto_1
    const/16 v1, 0x29

    .line 803
    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    .line 804
    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v0

    return-object v0
.end method

.method public unwrappingWriter(Lcom/fasterxml/jackson/databind/util/NameTransformer;)Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;
    .locals 1

    .line 362
    new-instance v0, Lcom/fasterxml/jackson/databind/ser/impl/UnwrappingBeanPropertyWriter;

    invoke-direct {v0, p0, p1}, Lcom/fasterxml/jackson/databind/ser/impl/UnwrappingBeanPropertyWriter;-><init>(Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;Lcom/fasterxml/jackson/databind/util/NameTransformer;)V

    return-object v0
.end method

.method public willSuppressNulls()Z
    .locals 1

    .line 489
    iget-boolean v0, p0, Lcom/fasterxml/jackson/databind/ser/BeanPropertyWriter;->_suppressNulls:Z

    return v0
.end method
