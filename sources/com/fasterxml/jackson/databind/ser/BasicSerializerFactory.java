package com.fasterxml.jackson.databind.ser;

import com.fasterxml.jackson.annotation.JsonFormat.Shape;
import com.fasterxml.jackson.annotation.JsonFormat.Value;
import com.fasterxml.jackson.annotation.JsonInclude;
import com.fasterxml.jackson.annotation.JsonInclude.Include;
import com.fasterxml.jackson.databind.BeanDescription;
import com.fasterxml.jackson.databind.JavaType;
import com.fasterxml.jackson.databind.JsonMappingException;
import com.fasterxml.jackson.databind.JsonSerializable;
import com.fasterxml.jackson.databind.JsonSerializer;
import com.fasterxml.jackson.databind.MapperFeature;
import com.fasterxml.jackson.databind.SerializationConfig;
import com.fasterxml.jackson.databind.SerializerProvider;
import com.fasterxml.jackson.databind.annotation.JsonSerialize.Typing;
import com.fasterxml.jackson.databind.cfg.SerializerFactoryConfig;
import com.fasterxml.jackson.databind.ext.OptionalHandlerFactory;
import com.fasterxml.jackson.databind.introspect.Annotated;
import com.fasterxml.jackson.databind.introspect.AnnotatedClass;
import com.fasterxml.jackson.databind.introspect.AnnotatedMethod;
import com.fasterxml.jackson.databind.introspect.BasicBeanDescription;
import com.fasterxml.jackson.databind.jsontype.TypeResolverBuilder;
import com.fasterxml.jackson.databind.jsontype.TypeSerializer;
import com.fasterxml.jackson.databind.ser.impl.IndexedListSerializer;
import com.fasterxml.jackson.databind.ser.impl.IndexedStringListSerializer;
import com.fasterxml.jackson.databind.ser.impl.IteratorSerializer;
import com.fasterxml.jackson.databind.ser.impl.MapEntrySerializer;
import com.fasterxml.jackson.databind.ser.impl.StringArraySerializer;
import com.fasterxml.jackson.databind.ser.impl.StringCollectionSerializer;
import com.fasterxml.jackson.databind.ser.std.BooleanSerializer;
import com.fasterxml.jackson.databind.ser.std.ByteBufferSerializer;
import com.fasterxml.jackson.databind.ser.std.CalendarSerializer;
import com.fasterxml.jackson.databind.ser.std.CollectionSerializer;
import com.fasterxml.jackson.databind.ser.std.DateSerializer;
import com.fasterxml.jackson.databind.ser.std.EnumSerializer;
import com.fasterxml.jackson.databind.ser.std.EnumSetSerializer;
import com.fasterxml.jackson.databind.ser.std.InetAddressSerializer;
import com.fasterxml.jackson.databind.ser.std.InetSocketAddressSerializer;
import com.fasterxml.jackson.databind.ser.std.IterableSerializer;
import com.fasterxml.jackson.databind.ser.std.JsonValueSerializer;
import com.fasterxml.jackson.databind.ser.std.NumberSerializer;
import com.fasterxml.jackson.databind.ser.std.NumberSerializers;
import com.fasterxml.jackson.databind.ser.std.ObjectArraySerializer;
import com.fasterxml.jackson.databind.ser.std.SerializableSerializer;
import com.fasterxml.jackson.databind.ser.std.StdArraySerializers;
import com.fasterxml.jackson.databind.ser.std.StdDelegatingSerializer;
import com.fasterxml.jackson.databind.ser.std.StdJdkSerializers;
import com.fasterxml.jackson.databind.ser.std.StdKeySerializers;
import com.fasterxml.jackson.databind.ser.std.StringSerializer;
import com.fasterxml.jackson.databind.ser.std.TimeZoneSerializer;
import com.fasterxml.jackson.databind.ser.std.ToStringSerializer;
import com.fasterxml.jackson.databind.ser.std.TokenBufferSerializer;
import com.fasterxml.jackson.databind.type.ArrayType;
import com.fasterxml.jackson.databind.type.CollectionLikeType;
import com.fasterxml.jackson.databind.type.CollectionType;
import com.fasterxml.jackson.databind.type.MapLikeType;
import com.fasterxml.jackson.databind.type.MapType;
import com.fasterxml.jackson.databind.type.TypeFactory;
import com.fasterxml.jackson.databind.util.ClassUtil;
import com.fasterxml.jackson.databind.util.Converter;
import com.fasterxml.jackson.databind.util.TokenBuffer;
import java.io.Serializable;
import java.lang.reflect.Method;
import java.math.BigDecimal;
import java.math.BigInteger;
import java.net.InetAddress;
import java.net.InetSocketAddress;
import java.nio.ByteBuffer;
import java.nio.charset.Charset;
import java.util.Calendar;
import java.util.Collection;
import java.util.Date;
import java.util.EnumSet;
import java.util.HashMap;
import java.util.Iterator;
import java.util.Map.Entry;
import java.util.RandomAccess;
import java.util.TimeZone;

public abstract class BasicSerializerFactory extends SerializerFactory implements Serializable {
    protected static final HashMap<String, JsonSerializer<?>> _concrete;
    protected static final HashMap<String, Class<? extends JsonSerializer<?>>> _concreteLazy;
    protected final SerializerFactoryConfig _factoryConfig;

    public abstract JsonSerializer<Object> createSerializer(SerializerProvider serializerProvider, JavaType javaType) throws JsonMappingException;

    /* access modifiers changed from: protected */
    public abstract Iterable<Serializers> customSerializers();

    public abstract SerializerFactory withConfig(SerializerFactoryConfig serializerFactoryConfig);

    static {
        HashMap<String, Class<? extends JsonSerializer<?>>> hashMap = new HashMap<>();
        HashMap<String, JsonSerializer<?>> hashMap2 = new HashMap<>();
        hashMap2.put(String.class.getName(), new StringSerializer());
        ToStringSerializer toStringSerializer = ToStringSerializer.instance;
        hashMap2.put(StringBuffer.class.getName(), toStringSerializer);
        hashMap2.put(StringBuilder.class.getName(), toStringSerializer);
        hashMap2.put(Character.class.getName(), toStringSerializer);
        hashMap2.put(Character.TYPE.getName(), toStringSerializer);
        NumberSerializers.addAll(hashMap2);
        hashMap2.put(Boolean.TYPE.getName(), new BooleanSerializer(true));
        hashMap2.put(Boolean.class.getName(), new BooleanSerializer(false));
        hashMap2.put(BigInteger.class.getName(), new NumberSerializer(BigInteger.class));
        hashMap2.put(BigDecimal.class.getName(), new NumberSerializer(BigDecimal.class));
        hashMap2.put(Calendar.class.getName(), CalendarSerializer.instance);
        hashMap2.put(Date.class.getName(), DateSerializer.instance);
        for (Entry entry : StdJdkSerializers.all()) {
            Object value = entry.getValue();
            if (value instanceof JsonSerializer) {
                hashMap2.put(((Class) entry.getKey()).getName(), (JsonSerializer) value);
            } else if (value instanceof Class) {
                hashMap.put(((Class) entry.getKey()).getName(), (Class) value);
            } else {
                throw new IllegalStateException("Internal error: unrecognized value of type " + entry.getClass().getName());
            }
        }
        hashMap.put(TokenBuffer.class.getName(), TokenBufferSerializer.class);
        _concrete = hashMap2;
        _concreteLazy = hashMap;
    }

    protected BasicSerializerFactory(SerializerFactoryConfig serializerFactoryConfig) {
        if (serializerFactoryConfig == null) {
            serializerFactoryConfig = new SerializerFactoryConfig();
        }
        this._factoryConfig = serializerFactoryConfig;
    }

    public SerializerFactoryConfig getFactoryConfig() {
        return this._factoryConfig;
    }

    public final SerializerFactory withAdditionalSerializers(Serializers serializers) {
        return withConfig(this._factoryConfig.withAdditionalSerializers(serializers));
    }

    public final SerializerFactory withAdditionalKeySerializers(Serializers serializers) {
        return withConfig(this._factoryConfig.withAdditionalKeySerializers(serializers));
    }

    public final SerializerFactory withSerializerModifier(BeanSerializerModifier beanSerializerModifier) {
        return withConfig(this._factoryConfig.withSerializerModifier(beanSerializerModifier));
    }

    public JsonSerializer<Object> createKeySerializer(SerializationConfig serializationConfig, JavaType javaType, JsonSerializer<Object> jsonSerializer) {
        BeanDescription introspectClassAnnotations = serializationConfig.introspectClassAnnotations(javaType.getRawClass());
        JsonSerializer<Object> jsonSerializer2 = null;
        if (this._factoryConfig.hasKeySerializers()) {
            for (Serializers findSerializer : this._factoryConfig.keySerializers()) {
                jsonSerializer2 = findSerializer.findSerializer(serializationConfig, javaType, introspectClassAnnotations);
                if (jsonSerializer2 != null) {
                    break;
                }
            }
        }
        if (jsonSerializer2 != null) {
            jsonSerializer = jsonSerializer2;
        } else if (jsonSerializer == null) {
            jsonSerializer = StdKeySerializers.getStdKeySerializer(serializationConfig, javaType.getRawClass(), false);
            if (jsonSerializer == null) {
                BeanDescription introspect = serializationConfig.introspect(javaType);
                AnnotatedMethod findJsonValueMethod = introspect.findJsonValueMethod();
                if (findJsonValueMethod != null) {
                    JsonSerializer stdKeySerializer = StdKeySerializers.getStdKeySerializer(serializationConfig, findJsonValueMethod.getRawReturnType(), true);
                    Method annotated = findJsonValueMethod.getAnnotated();
                    if (serializationConfig.canOverrideAccessModifiers()) {
                        ClassUtil.checkAndFixAccess(annotated, serializationConfig.isEnabled(MapperFeature.OVERRIDE_PUBLIC_ACCESS_MODIFIERS));
                    }
                    jsonSerializer = new JsonValueSerializer<>(annotated, stdKeySerializer);
                    introspectClassAnnotations = introspect;
                } else {
                    jsonSerializer = StdKeySerializers.getFallbackKeySerializer(serializationConfig, javaType.getRawClass());
                    introspectClassAnnotations = introspect;
                }
            }
        }
        if (this._factoryConfig.hasSerializerModifiers()) {
            for (BeanSerializerModifier modifyKeySerializer : this._factoryConfig.serializerModifiers()) {
                jsonSerializer = modifyKeySerializer.modifyKeySerializer(serializationConfig, javaType, introspectClassAnnotations, jsonSerializer);
            }
        }
        return jsonSerializer;
    }

    public TypeSerializer createTypeSerializer(SerializationConfig serializationConfig, JavaType javaType) {
        Collection collectAndResolveSubtypesByClass;
        AnnotatedClass classInfo = serializationConfig.introspectClassAnnotations(javaType.getRawClass()).getClassInfo();
        TypeResolverBuilder findTypeResolver = serializationConfig.getAnnotationIntrospector().findTypeResolver(serializationConfig, classInfo, javaType);
        if (findTypeResolver == null) {
            findTypeResolver = serializationConfig.getDefaultTyper(javaType);
            collectAndResolveSubtypesByClass = null;
        } else {
            collectAndResolveSubtypesByClass = serializationConfig.getSubtypeResolver().collectAndResolveSubtypesByClass(serializationConfig, classInfo);
        }
        if (findTypeResolver == null) {
            return null;
        }
        return findTypeResolver.buildTypeSerializer(serializationConfig, javaType, collectAndResolveSubtypesByClass);
    }

    /* access modifiers changed from: protected */
    public final JsonSerializer<?> findSerializerByLookup(JavaType javaType, SerializationConfig serializationConfig, BeanDescription beanDescription, boolean z) {
        String name = javaType.getRawClass().getName();
        JsonSerializer<?> jsonSerializer = (JsonSerializer) _concrete.get(name);
        if (jsonSerializer != null) {
            return jsonSerializer;
        }
        Class cls = (Class) _concreteLazy.get(name);
        if (cls == null) {
            return jsonSerializer;
        }
        try {
            return (JsonSerializer) cls.newInstance();
        } catch (Exception e) {
            throw new IllegalStateException("Failed to instantiate standard serializer (of type " + cls.getName() + "): " + e.getMessage(), e);
        }
    }

    /* access modifiers changed from: protected */
    public final JsonSerializer<?> findSerializerByAnnotations(SerializerProvider serializerProvider, JavaType javaType, BeanDescription beanDescription) throws JsonMappingException {
        if (JsonSerializable.class.isAssignableFrom(javaType.getRawClass())) {
            return SerializableSerializer.instance;
        }
        AnnotatedMethod findJsonValueMethod = beanDescription.findJsonValueMethod();
        if (findJsonValueMethod == null) {
            return null;
        }
        Method annotated = findJsonValueMethod.getAnnotated();
        if (serializerProvider.canOverrideAccessModifiers()) {
            ClassUtil.checkAndFixAccess(annotated, serializerProvider.isEnabled(MapperFeature.OVERRIDE_PUBLIC_ACCESS_MODIFIERS));
        }
        return new JsonValueSerializer(annotated, findSerializerFromAnnotation(serializerProvider, findJsonValueMethod));
    }

    /* access modifiers changed from: protected */
    public final JsonSerializer<?> findSerializerByPrimaryType(SerializerProvider serializerProvider, JavaType javaType, BeanDescription beanDescription, boolean z) throws JsonMappingException {
        Class rawClass = javaType.getRawClass();
        JsonSerializer<?> findOptionalStdSerializer = findOptionalStdSerializer(serializerProvider, javaType, beanDescription, z);
        if (findOptionalStdSerializer != null) {
            return findOptionalStdSerializer;
        }
        if (Calendar.class.isAssignableFrom(rawClass)) {
            return CalendarSerializer.instance;
        }
        if (Date.class.isAssignableFrom(rawClass)) {
            return DateSerializer.instance;
        }
        if (Entry.class.isAssignableFrom(rawClass)) {
            JavaType findSuperType = javaType.findSuperType(Entry.class);
            JavaType containedType = findSuperType.containedType(0);
            if (containedType == null) {
                containedType = TypeFactory.unknownType();
            }
            JavaType containedType2 = findSuperType.containedType(1);
            if (containedType2 == null) {
                containedType2 = TypeFactory.unknownType();
            }
            return buildMapEntrySerializer(serializerProvider.getConfig(), javaType, beanDescription, z, containedType, containedType2);
        } else if (ByteBuffer.class.isAssignableFrom(rawClass)) {
            return new ByteBufferSerializer();
        } else {
            if (InetAddress.class.isAssignableFrom(rawClass)) {
                return new InetAddressSerializer();
            }
            if (InetSocketAddress.class.isAssignableFrom(rawClass)) {
                return new InetSocketAddressSerializer();
            }
            if (TimeZone.class.isAssignableFrom(rawClass)) {
                return new TimeZoneSerializer();
            }
            if (Charset.class.isAssignableFrom(rawClass)) {
                return ToStringSerializer.instance;
            }
            if (Number.class.isAssignableFrom(rawClass)) {
                if (beanDescription.findExpectedFormat(null) != null) {
                    switch (beanDescription.findExpectedFormat(null).getShape()) {
                        case STRING:
                            return ToStringSerializer.instance;
                        case OBJECT:
                        case ARRAY:
                            return null;
                    }
                }
                return NumberSerializer.instance;
            } else if (Enum.class.isAssignableFrom(rawClass)) {
                return buildEnumSerializer(serializerProvider.getConfig(), javaType, beanDescription);
            } else {
                return null;
            }
        }
    }

    /* access modifiers changed from: protected */
    public JsonSerializer<?> findOptionalStdSerializer(SerializerProvider serializerProvider, JavaType javaType, BeanDescription beanDescription, boolean z) throws JsonMappingException {
        return OptionalHandlerFactory.instance.findSerializer(serializerProvider.getConfig(), javaType, beanDescription);
    }

    /* access modifiers changed from: protected */
    public final JsonSerializer<?> findSerializerByAddonType(SerializationConfig serializationConfig, JavaType javaType, BeanDescription beanDescription, boolean z) throws JsonMappingException {
        Class rawClass = javaType.getRawClass();
        if (Iterator.class.isAssignableFrom(rawClass)) {
            JavaType[] findTypeParameters = serializationConfig.getTypeFactory().findTypeParameters(javaType, Iterator.class);
            return buildIteratorSerializer(serializationConfig, javaType, beanDescription, z, (findTypeParameters == null || findTypeParameters.length != 1) ? TypeFactory.unknownType() : findTypeParameters[0]);
        } else if (Iterable.class.isAssignableFrom(rawClass)) {
            JavaType[] findTypeParameters2 = serializationConfig.getTypeFactory().findTypeParameters(javaType, Iterable.class);
            return buildIterableSerializer(serializationConfig, javaType, beanDescription, z, (findTypeParameters2 == null || findTypeParameters2.length != 1) ? TypeFactory.unknownType() : findTypeParameters2[0]);
        } else if (CharSequence.class.isAssignableFrom(rawClass)) {
            return ToStringSerializer.instance;
        } else {
            return null;
        }
    }

    /* access modifiers changed from: protected */
    public JsonSerializer<Object> findSerializerFromAnnotation(SerializerProvider serializerProvider, Annotated annotated) throws JsonMappingException {
        Object findSerializer = serializerProvider.getAnnotationIntrospector().findSerializer(annotated);
        if (findSerializer == null) {
            return null;
        }
        return findConvertingSerializer(serializerProvider, annotated, serializerProvider.serializerInstance(annotated, findSerializer));
    }

    /* access modifiers changed from: protected */
    public JsonSerializer<?> findConvertingSerializer(SerializerProvider serializerProvider, Annotated annotated, JsonSerializer<?> jsonSerializer) throws JsonMappingException {
        Converter findConverter = findConverter(serializerProvider, annotated);
        return findConverter == null ? jsonSerializer : new StdDelegatingSerializer(findConverter, findConverter.getOutputType(serializerProvider.getTypeFactory()), jsonSerializer);
    }

    /* access modifiers changed from: protected */
    public Converter<Object, Object> findConverter(SerializerProvider serializerProvider, Annotated annotated) throws JsonMappingException {
        Object findSerializationConverter = serializerProvider.getAnnotationIntrospector().findSerializationConverter(annotated);
        if (findSerializationConverter == null) {
            return null;
        }
        return serializerProvider.converterInstance(annotated, findSerializationConverter);
    }

    /* access modifiers changed from: protected */
    public JsonSerializer<?> buildContainerSerializer(SerializerProvider serializerProvider, JavaType javaType, BeanDescription beanDescription, boolean z) throws JsonMappingException {
        boolean z2;
        JsonSerializer<?> jsonSerializer;
        SerializationConfig config = serializerProvider.getConfig();
        if (!z && javaType.useStaticType() && (!javaType.isContainerType() || javaType.getContentType().getRawClass() != Object.class)) {
            z = true;
        }
        TypeSerializer createTypeSerializer = createTypeSerializer(config, javaType.getContentType());
        if (createTypeSerializer != null) {
            z2 = false;
        } else {
            z2 = z;
        }
        JsonSerializer _findContentSerializer = _findContentSerializer(serializerProvider, beanDescription.getClassInfo());
        if (javaType.isMapLikeType()) {
            MapLikeType mapLikeType = (MapLikeType) javaType;
            JsonSerializer _findKeySerializer = _findKeySerializer(serializerProvider, beanDescription.getClassInfo());
            if (!mapLikeType.isTrueMapType()) {
                JsonSerializer<?> jsonSerializer2 = null;
                MapLikeType mapLikeType2 = (MapLikeType) javaType;
                for (Serializers findMapLikeSerializer : customSerializers()) {
                    jsonSerializer2 = findMapLikeSerializer.findMapLikeSerializer(config, mapLikeType2, beanDescription, _findKeySerializer, createTypeSerializer, _findContentSerializer);
                    if (jsonSerializer2 != null) {
                        break;
                    }
                }
                if (jsonSerializer2 == null) {
                    jsonSerializer2 = findSerializerByAnnotations(serializerProvider, javaType, beanDescription);
                }
                if (jsonSerializer2 != null && this._factoryConfig.hasSerializerModifiers()) {
                    Iterator it = this._factoryConfig.serializerModifiers().iterator();
                    while (true) {
                        jsonSerializer = jsonSerializer2;
                        if (!it.hasNext()) {
                            break;
                        }
                        jsonSerializer2 = ((BeanSerializerModifier) it.next()).modifyMapLikeSerializer(config, mapLikeType2, beanDescription, jsonSerializer);
                    }
                } else {
                    return jsonSerializer2;
                }
            } else {
                return buildMapSerializer(serializerProvider, (MapType) mapLikeType, beanDescription, z2, _findKeySerializer, createTypeSerializer, _findContentSerializer);
            }
        } else if (javaType.isCollectionLikeType()) {
            CollectionLikeType collectionLikeType = (CollectionLikeType) javaType;
            if (!collectionLikeType.isTrueCollectionType()) {
                JsonSerializer<?> jsonSerializer3 = null;
                CollectionLikeType collectionLikeType2 = (CollectionLikeType) javaType;
                for (Serializers findCollectionLikeSerializer : customSerializers()) {
                    jsonSerializer3 = findCollectionLikeSerializer.findCollectionLikeSerializer(config, collectionLikeType2, beanDescription, createTypeSerializer, _findContentSerializer);
                    if (jsonSerializer3 != null) {
                        break;
                    }
                }
                if (jsonSerializer3 == null) {
                    jsonSerializer3 = findSerializerByAnnotations(serializerProvider, javaType, beanDescription);
                }
                if (jsonSerializer3 != null && this._factoryConfig.hasSerializerModifiers()) {
                    Iterator it2 = this._factoryConfig.serializerModifiers().iterator();
                    while (true) {
                        jsonSerializer = jsonSerializer3;
                        if (!it2.hasNext()) {
                            break;
                        }
                        jsonSerializer3 = ((BeanSerializerModifier) it2.next()).modifyCollectionLikeSerializer(config, collectionLikeType2, beanDescription, jsonSerializer);
                    }
                } else {
                    return jsonSerializer3;
                }
            } else {
                return buildCollectionSerializer(serializerProvider, (CollectionType) collectionLikeType, beanDescription, z2, createTypeSerializer, _findContentSerializer);
            }
        } else if (!javaType.isArrayType()) {
            return null;
        } else {
            return buildArraySerializer(serializerProvider, (ArrayType) javaType, beanDescription, z2, createTypeSerializer, _findContentSerializer);
        }
        return jsonSerializer;
    }

    /* access modifiers changed from: protected */
    public JsonSerializer<?> buildCollectionSerializer(SerializerProvider serializerProvider, CollectionType collectionType, BeanDescription beanDescription, boolean z, TypeSerializer typeSerializer, JsonSerializer<Object> jsonSerializer) throws JsonMappingException {
        JsonSerializer jsonSerializer2;
        JavaType javaType = null;
        SerializationConfig config = serializerProvider.getConfig();
        JsonSerializer jsonSerializer3 = null;
        for (Serializers findCollectionSerializer : customSerializers()) {
            jsonSerializer3 = findCollectionSerializer.findCollectionSerializer(config, collectionType, beanDescription, typeSerializer, jsonSerializer);
            if (jsonSerializer3 != null) {
                break;
            }
        }
        if (jsonSerializer3 == null) {
            jsonSerializer3 = findSerializerByAnnotations(serializerProvider, collectionType, beanDescription);
            if (jsonSerializer3 == null) {
                Value findExpectedFormat = beanDescription.findExpectedFormat(null);
                if (findExpectedFormat != null && findExpectedFormat.getShape() == Shape.OBJECT) {
                    return null;
                }
                Class rawClass = collectionType.getRawClass();
                if (EnumSet.class.isAssignableFrom(rawClass)) {
                    JavaType contentType = collectionType.getContentType();
                    if (contentType.isEnumType()) {
                        javaType = contentType;
                    }
                    jsonSerializer3 = buildEnumSetSerializer(javaType);
                } else {
                    Class<String> rawClass2 = collectionType.getContentType().getRawClass();
                    if (isIndexedList(rawClass)) {
                        if (rawClass2 != String.class) {
                            jsonSerializer3 = buildIndexedListSerializer(collectionType.getContentType(), z, typeSerializer, jsonSerializer);
                        } else if (jsonSerializer == null || ClassUtil.isJacksonStdImpl((Object) jsonSerializer)) {
                            jsonSerializer3 = IndexedStringListSerializer.instance;
                        }
                    } else if (rawClass2 == String.class && (jsonSerializer == null || ClassUtil.isJacksonStdImpl((Object) jsonSerializer))) {
                        jsonSerializer3 = StringCollectionSerializer.instance;
                    }
                    if (jsonSerializer3 == null) {
                        jsonSerializer3 = buildCollectionSerializer(collectionType.getContentType(), z, typeSerializer, jsonSerializer);
                    }
                }
            }
        }
        if (this._factoryConfig.hasSerializerModifiers()) {
            Iterator it = this._factoryConfig.serializerModifiers().iterator();
            while (true) {
                jsonSerializer2 = jsonSerializer3;
                if (!it.hasNext()) {
                    break;
                }
                jsonSerializer3 = ((BeanSerializerModifier) it.next()).modifyCollectionSerializer(config, collectionType, beanDescription, jsonSerializer2);
            }
        } else {
            jsonSerializer2 = jsonSerializer3;
        }
        return jsonSerializer2;
    }

    /* access modifiers changed from: protected */
    public boolean isIndexedList(Class<?> cls) {
        return RandomAccess.class.isAssignableFrom(cls);
    }

    public ContainerSerializer<?> buildIndexedListSerializer(JavaType javaType, boolean z, TypeSerializer typeSerializer, JsonSerializer<Object> jsonSerializer) {
        return new IndexedListSerializer(javaType, z, typeSerializer, jsonSerializer);
    }

    public ContainerSerializer<?> buildCollectionSerializer(JavaType javaType, boolean z, TypeSerializer typeSerializer, JsonSerializer<Object> jsonSerializer) {
        return new CollectionSerializer(javaType, z, typeSerializer, jsonSerializer);
    }

    public JsonSerializer<?> buildEnumSetSerializer(JavaType javaType) {
        return new EnumSetSerializer(javaType);
    }

    /* JADX WARNING: type inference failed for: r0v0 */
    /* JADX WARNING: type inference failed for: r0v1 */
    /* JADX WARNING: type inference failed for: r0v2 */
    /* JADX WARNING: type inference failed for: r0v3 */
    /* JADX WARNING: type inference failed for: r2v4, types: [com.fasterxml.jackson.databind.JsonSerializer<?>] */
    /* JADX WARNING: type inference failed for: r0v4 */
    /* JADX WARNING: type inference failed for: r2v7, types: [com.fasterxml.jackson.databind.JsonSerializer, com.fasterxml.jackson.databind.JsonSerializer<?>] */
    /* JADX WARNING: type inference failed for: r0v8, types: [com.fasterxml.jackson.databind.JsonSerializer] */
    /* JADX WARNING: type inference failed for: r0v9, types: [com.fasterxml.jackson.databind.JsonSerializer] */
    /* JADX WARNING: type inference failed for: r0v11, types: [com.fasterxml.jackson.databind.ser.std.MapSerializer] */
    /* JADX WARNING: type inference failed for: r0v12, types: [com.fasterxml.jackson.databind.ser.std.MapSerializer] */
    /* JADX WARNING: type inference failed for: r0v15, types: [com.fasterxml.jackson.databind.JsonSerializer] */
    /* JADX WARNING: type inference failed for: r0v16 */
    /* JADX WARNING: type inference failed for: r0v17 */
    /* JADX WARNING: type inference failed for: r0v18 */
    /* JADX WARNING: type inference failed for: r0v19 */
    /* JADX WARNING: type inference failed for: r0v20 */
    /* access modifiers changed from: protected */
    /* JADX WARNING: Multi-variable type inference failed. Error: jadx.core.utils.exceptions.JadxRuntimeException: No candidate types for var: r0v1
      assigns: []
      uses: []
      mth insns count: 52
    	at jadx.core.dex.visitors.typeinference.TypeSearch.fillTypeCandidates(TypeSearch.java:237)
    	at java.base/java.util.ArrayList.forEach(ArrayList.java:1540)
    	at jadx.core.dex.visitors.typeinference.TypeSearch.run(TypeSearch.java:53)
    	at jadx.core.dex.visitors.typeinference.TypeInferenceVisitor.runMultiVariableSearch(TypeInferenceVisitor.java:99)
    	at jadx.core.dex.visitors.typeinference.TypeInferenceVisitor.visit(TypeInferenceVisitor.java:92)
    	at jadx.core.dex.visitors.DepthTraversal.visit(DepthTraversal.java:27)
    	at jadx.core.dex.visitors.DepthTraversal.lambda$visit$1(DepthTraversal.java:14)
    	at java.base/java.util.ArrayList.forEach(ArrayList.java:1540)
    	at jadx.core.dex.visitors.DepthTraversal.visit(DepthTraversal.java:14)
    	at jadx.core.ProcessClass.process(ProcessClass.java:30)
    	at jadx.api.JadxDecompiler.processClass(JadxDecompiler.java:311)
    	at jadx.api.JavaClass.decompile(JavaClass.java:62)
    	at jadx.api.JadxDecompiler.lambda$appendSourcesSave$0(JadxDecompiler.java:217)
     */
    /* JADX WARNING: Unknown variable types count: 7 */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    public com.fasterxml.jackson.databind.JsonSerializer<?> buildMapSerializer(com.fasterxml.jackson.databind.SerializerProvider r10, com.fasterxml.jackson.databind.type.MapType r11, com.fasterxml.jackson.databind.BeanDescription r12, boolean r13, com.fasterxml.jackson.databind.JsonSerializer<java.lang.Object> r14, com.fasterxml.jackson.databind.jsontype.TypeSerializer r15, com.fasterxml.jackson.databind.JsonSerializer<java.lang.Object> r16) throws com.fasterxml.jackson.databind.JsonMappingException {
        /*
            r9 = this;
            com.fasterxml.jackson.databind.SerializationConfig r1 = r10.getConfig()
            r0 = 0
            java.lang.Iterable r2 = r9.customSerializers()
            java.util.Iterator r7 = r2.iterator()
        L_0x000d:
            boolean r2 = r7.hasNext()
            if (r2 == 0) goto L_0x0025
            java.lang.Object r0 = r7.next()
            com.fasterxml.jackson.databind.ser.Serializers r0 = (com.fasterxml.jackson.databind.ser.Serializers) r0
            r2 = r11
            r3 = r12
            r4 = r14
            r5 = r15
            r6 = r16
            com.fasterxml.jackson.databind.JsonSerializer r0 = r0.findMapSerializer(r1, r2, r3, r4, r5, r6)
            if (r0 == 0) goto L_0x000d
        L_0x0025:
            if (r0 != 0) goto L_0x0056
            com.fasterxml.jackson.databind.JsonSerializer r0 = r9.findSerializerByAnnotations(r10, r11, r12)
            if (r0 != 0) goto L_0x0056
            java.lang.Object r8 = r9.findFilterId(r1, r12)
            com.fasterxml.jackson.databind.AnnotationIntrospector r0 = r1.getAnnotationIntrospector()
            com.fasterxml.jackson.databind.introspect.AnnotatedClass r2 = r12.getClassInfo()
            r3 = 1
            java.lang.String[] r2 = r0.findPropertiesToIgnore(r2, r3)
            r3 = r11
            r4 = r13
            r5 = r15
            r6 = r14
            r7 = r16
            com.fasterxml.jackson.databind.ser.std.MapSerializer r0 = com.fasterxml.jackson.databind.ser.std.MapSerializer.construct(r2, r3, r4, r5, r6, r7, r8)
            com.fasterxml.jackson.databind.JavaType r2 = r11.getContentType()
            java.lang.Object r2 = r9.findSuppressableContentValue(r1, r2, r12)
            if (r2 == 0) goto L_0x0056
            com.fasterxml.jackson.databind.ser.std.MapSerializer r0 = r0.withContentInclusion(r2)
        L_0x0056:
            com.fasterxml.jackson.databind.cfg.SerializerFactoryConfig r2 = r9._factoryConfig
            boolean r2 = r2.hasSerializerModifiers()
            if (r2 == 0) goto L_0x007b
            com.fasterxml.jackson.databind.cfg.SerializerFactoryConfig r2 = r9._factoryConfig
            java.lang.Iterable r2 = r2.serializerModifiers()
            java.util.Iterator r3 = r2.iterator()
            r2 = r0
        L_0x0069:
            boolean r0 = r3.hasNext()
            if (r0 == 0) goto L_0x007c
            java.lang.Object r0 = r3.next()
            com.fasterxml.jackson.databind.ser.BeanSerializerModifier r0 = (com.fasterxml.jackson.databind.ser.BeanSerializerModifier) r0
            com.fasterxml.jackson.databind.JsonSerializer r0 = r0.modifyMapSerializer(r1, r11, r12, r2)
            r2 = r0
            goto L_0x0069
        L_0x007b:
            r2 = r0
        L_0x007c:
            return r2
        */
        throw new UnsupportedOperationException("Method not decompiled: com.fasterxml.jackson.databind.ser.BasicSerializerFactory.buildMapSerializer(com.fasterxml.jackson.databind.SerializerProvider, com.fasterxml.jackson.databind.type.MapType, com.fasterxml.jackson.databind.BeanDescription, boolean, com.fasterxml.jackson.databind.JsonSerializer, com.fasterxml.jackson.databind.jsontype.TypeSerializer, com.fasterxml.jackson.databind.JsonSerializer):com.fasterxml.jackson.databind.JsonSerializer");
    }

    /* access modifiers changed from: protected */
    public Object findSuppressableContentValue(SerializationConfig serializationConfig, JavaType javaType, BeanDescription beanDescription) throws JsonMappingException {
        JsonInclude.Value findPropertyInclusion = beanDescription.findPropertyInclusion(serializationConfig.getDefaultPropertyInclusion());
        if (findPropertyInclusion == null) {
            return null;
        }
        Include contentInclusion = findPropertyInclusion.getContentInclusion();
        switch (contentInclusion) {
            case USE_DEFAULTS:
                return null;
            default:
                return contentInclusion;
        }
    }

    /* access modifiers changed from: protected */
    public JsonSerializer<?> buildArraySerializer(SerializerProvider serializerProvider, ArrayType arrayType, BeanDescription beanDescription, boolean z, TypeSerializer typeSerializer, JsonSerializer<Object> jsonSerializer) throws JsonMappingException {
        SerializationConfig config = serializerProvider.getConfig();
        JsonSerializer jsonSerializer2 = null;
        for (Serializers findArraySerializer : customSerializers()) {
            jsonSerializer2 = findArraySerializer.findArraySerializer(config, arrayType, beanDescription, typeSerializer, jsonSerializer);
            if (jsonSerializer2 != null) {
                break;
            }
        }
        if (jsonSerializer2 == null) {
            Class<String[]> rawClass = arrayType.getRawClass();
            if (jsonSerializer == null || ClassUtil.isJacksonStdImpl((Object) jsonSerializer)) {
                if (String[].class == rawClass) {
                    jsonSerializer2 = StringArraySerializer.instance;
                } else {
                    jsonSerializer2 = StdArraySerializers.findStandardImpl(rawClass);
                }
            }
            if (jsonSerializer2 == null) {
                jsonSerializer2 = new ObjectArraySerializer(arrayType.getContentType(), z, typeSerializer, jsonSerializer);
            }
        }
        if (!this._factoryConfig.hasSerializerModifiers()) {
            return jsonSerializer2;
        }
        Iterator it = this._factoryConfig.serializerModifiers().iterator();
        while (true) {
            JsonSerializer jsonSerializer3 = jsonSerializer2;
            if (!it.hasNext()) {
                return jsonSerializer3;
            }
            jsonSerializer2 = ((BeanSerializerModifier) it.next()).modifyArraySerializer(config, arrayType, beanDescription, jsonSerializer3);
        }
    }

    /* access modifiers changed from: protected */
    public JsonSerializer<?> buildIteratorSerializer(SerializationConfig serializationConfig, JavaType javaType, BeanDescription beanDescription, boolean z, JavaType javaType2) throws JsonMappingException {
        return new IteratorSerializer(javaType2, z, createTypeSerializer(serializationConfig, javaType2));
    }

    /* access modifiers changed from: protected */
    @Deprecated
    public JsonSerializer<?> buildIteratorSerializer(SerializationConfig serializationConfig, JavaType javaType, BeanDescription beanDescription, boolean z) throws JsonMappingException {
        JavaType[] findTypeParameters = serializationConfig.getTypeFactory().findTypeParameters(javaType, Iterator.class);
        return buildIteratorSerializer(serializationConfig, javaType, beanDescription, z, (findTypeParameters == null || findTypeParameters.length != 1) ? TypeFactory.unknownType() : findTypeParameters[0]);
    }

    /* access modifiers changed from: protected */
    public JsonSerializer<?> buildIterableSerializer(SerializationConfig serializationConfig, JavaType javaType, BeanDescription beanDescription, boolean z, JavaType javaType2) throws JsonMappingException {
        return new IterableSerializer(javaType2, z, createTypeSerializer(serializationConfig, javaType2));
    }

    /* access modifiers changed from: protected */
    @Deprecated
    public JsonSerializer<?> buildIterableSerializer(SerializationConfig serializationConfig, JavaType javaType, BeanDescription beanDescription, boolean z) throws JsonMappingException {
        JavaType[] findTypeParameters = serializationConfig.getTypeFactory().findTypeParameters(javaType, Iterable.class);
        return buildIterableSerializer(serializationConfig, javaType, beanDescription, z, (findTypeParameters == null || findTypeParameters.length != 1) ? TypeFactory.unknownType() : findTypeParameters[0]);
    }

    /* access modifiers changed from: protected */
    public JsonSerializer<?> buildMapEntrySerializer(SerializationConfig serializationConfig, JavaType javaType, BeanDescription beanDescription, boolean z, JavaType javaType2, JavaType javaType3) throws JsonMappingException {
        return new MapEntrySerializer(javaType3, javaType2, javaType3, z, createTypeSerializer(serializationConfig, javaType3), null);
    }

    /* access modifiers changed from: protected */
    public JsonSerializer<?> buildEnumSerializer(SerializationConfig serializationConfig, JavaType javaType, BeanDescription beanDescription) throws JsonMappingException {
        Value findExpectedFormat = beanDescription.findExpectedFormat(null);
        if (findExpectedFormat == null || findExpectedFormat.getShape() != Shape.OBJECT) {
            JsonSerializer<?> construct = EnumSerializer.construct(javaType.getRawClass(), serializationConfig, beanDescription, findExpectedFormat);
            if (!this._factoryConfig.hasSerializerModifiers()) {
                return construct;
            }
            Iterator it = this._factoryConfig.serializerModifiers().iterator();
            while (true) {
                JsonSerializer<?> jsonSerializer = construct;
                if (!it.hasNext()) {
                    return jsonSerializer;
                }
                construct = ((BeanSerializerModifier) it.next()).modifyEnumSerializer(serializationConfig, javaType, beanDescription, jsonSerializer);
            }
        } else {
            ((BasicBeanDescription) beanDescription).removeProperty("declaringClass");
            return null;
        }
    }

    /* access modifiers changed from: protected */
    public JsonSerializer<Object> _findKeySerializer(SerializerProvider serializerProvider, Annotated annotated) throws JsonMappingException {
        Object findKeySerializer = serializerProvider.getAnnotationIntrospector().findKeySerializer(annotated);
        if (findKeySerializer != null) {
            return serializerProvider.serializerInstance(annotated, findKeySerializer);
        }
        return null;
    }

    /* access modifiers changed from: protected */
    public JsonSerializer<Object> _findContentSerializer(SerializerProvider serializerProvider, Annotated annotated) throws JsonMappingException {
        Object findContentSerializer = serializerProvider.getAnnotationIntrospector().findContentSerializer(annotated);
        if (findContentSerializer != null) {
            return serializerProvider.serializerInstance(annotated, findContentSerializer);
        }
        return null;
    }

    /* access modifiers changed from: protected */
    public Object findFilterId(SerializationConfig serializationConfig, BeanDescription beanDescription) {
        return serializationConfig.getAnnotationIntrospector().findFilterId(beanDescription.getClassInfo());
    }

    /* access modifiers changed from: protected */
    public boolean usesStaticTyping(SerializationConfig serializationConfig, BeanDescription beanDescription, TypeSerializer typeSerializer) {
        if (typeSerializer != null) {
            return false;
        }
        Typing findSerializationTyping = serializationConfig.getAnnotationIntrospector().findSerializationTyping(beanDescription.getClassInfo());
        if (findSerializationTyping == null || findSerializationTyping == Typing.DEFAULT_TYPING) {
            return serializationConfig.isEnabled(MapperFeature.USE_STATIC_TYPING);
        }
        if (findSerializationTyping == Typing.STATIC) {
            return true;
        }
        return false;
    }

    /* access modifiers changed from: protected */
    public Class<?> _verifyAsClass(Object obj, String str, Class<?> cls) {
        if (obj == null) {
            return null;
        }
        if (!(obj instanceof Class)) {
            throw new IllegalStateException("AnnotationIntrospector." + str + "() returned value of type " + obj.getClass().getName() + ": expected type JsonSerializer or Class<JsonSerializer> instead");
        }
        Class<?> cls2 = (Class) obj;
        if (cls2 == cls || ClassUtil.isBogusClass(cls2)) {
            return null;
        }
        return cls2;
    }
}
