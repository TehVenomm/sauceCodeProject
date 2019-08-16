package com.fasterxml.jackson.databind.ser.std;

import com.fasterxml.jackson.annotation.JsonInclude.Include;
import com.fasterxml.jackson.core.JsonGenerator;
import com.fasterxml.jackson.databind.BeanProperty;
import com.fasterxml.jackson.databind.JavaType;
import com.fasterxml.jackson.databind.JsonMappingException;
import com.fasterxml.jackson.databind.JsonNode;
import com.fasterxml.jackson.databind.JsonSerializer;
import com.fasterxml.jackson.databind.SerializationFeature;
import com.fasterxml.jackson.databind.SerializerProvider;
import com.fasterxml.jackson.databind.annotation.JacksonStdImpl;
import com.fasterxml.jackson.databind.jsonFormatVisitors.JsonFormatVisitorWrapper;
import com.fasterxml.jackson.databind.jsonFormatVisitors.JsonMapFormatVisitor;
import com.fasterxml.jackson.databind.jsontype.TypeSerializer;
import com.fasterxml.jackson.databind.ser.ContainerSerializer;
import com.fasterxml.jackson.databind.ser.ContextualSerializer;
import com.fasterxml.jackson.databind.ser.PropertyFilter;
import com.fasterxml.jackson.databind.ser.impl.PropertySerializerMap;
import com.fasterxml.jackson.databind.ser.impl.PropertySerializerMap.SerializerAndMapResult;
import com.fasterxml.jackson.databind.type.TypeFactory;
import com.fasterxml.jackson.databind.util.ArrayBuilders;
import java.io.IOException;
import java.lang.reflect.Type;
import java.util.HashSet;
import java.util.Map;
import java.util.Map.Entry;
import java.util.SortedMap;
import java.util.TreeMap;

@JacksonStdImpl
public class MapSerializer extends ContainerSerializer<Map<?, ?>> implements ContextualSerializer {
    protected static final JavaType UNSPECIFIED_TYPE = TypeFactory.unknownType();
    private static final long serialVersionUID = 1;
    protected PropertySerializerMap _dynamicValueSerializers;
    protected final Object _filterId;
    protected final HashSet<String> _ignoredEntries;
    protected JsonSerializer<Object> _keySerializer;
    protected final JavaType _keyType;
    protected final BeanProperty _property;
    protected final boolean _sortKeys;
    protected final Object _suppressableValue;
    protected JsonSerializer<Object> _valueSerializer;
    protected final JavaType _valueType;
    protected final boolean _valueTypeIsStatic;
    protected final TypeSerializer _valueTypeSerializer;

    protected MapSerializer(HashSet<String> hashSet, JavaType javaType, JavaType javaType2, boolean z, TypeSerializer typeSerializer, JsonSerializer<?> jsonSerializer, JsonSerializer<?> jsonSerializer2) {
        super(Map.class, false);
        this._ignoredEntries = hashSet;
        this._keyType = javaType;
        this._valueType = javaType2;
        this._valueTypeIsStatic = z;
        this._valueTypeSerializer = typeSerializer;
        this._keySerializer = jsonSerializer;
        this._valueSerializer = jsonSerializer2;
        this._dynamicValueSerializers = PropertySerializerMap.emptyForProperties();
        this._property = null;
        this._filterId = null;
        this._sortKeys = false;
        this._suppressableValue = null;
    }

    /* access modifiers changed from: protected */
    public void _ensureOverride() {
        if (getClass() != MapSerializer.class) {
            throw new IllegalStateException("Missing override in class " + getClass().getName());
        }
    }

    protected MapSerializer(MapSerializer mapSerializer, BeanProperty beanProperty, JsonSerializer<?> jsonSerializer, JsonSerializer<?> jsonSerializer2, HashSet<String> hashSet) {
        super(Map.class, false);
        this._ignoredEntries = hashSet;
        this._keyType = mapSerializer._keyType;
        this._valueType = mapSerializer._valueType;
        this._valueTypeIsStatic = mapSerializer._valueTypeIsStatic;
        this._valueTypeSerializer = mapSerializer._valueTypeSerializer;
        this._keySerializer = jsonSerializer;
        this._valueSerializer = jsonSerializer2;
        this._dynamicValueSerializers = mapSerializer._dynamicValueSerializers;
        this._property = beanProperty;
        this._filterId = mapSerializer._filterId;
        this._sortKeys = mapSerializer._sortKeys;
        this._suppressableValue = mapSerializer._suppressableValue;
    }

    @Deprecated
    protected MapSerializer(MapSerializer mapSerializer, TypeSerializer typeSerializer) {
        this(mapSerializer, typeSerializer, mapSerializer._suppressableValue);
    }

    protected MapSerializer(MapSerializer mapSerializer, TypeSerializer typeSerializer, Object obj) {
        super(Map.class, false);
        this._ignoredEntries = mapSerializer._ignoredEntries;
        this._keyType = mapSerializer._keyType;
        this._valueType = mapSerializer._valueType;
        this._valueTypeIsStatic = mapSerializer._valueTypeIsStatic;
        this._valueTypeSerializer = typeSerializer;
        this._keySerializer = mapSerializer._keySerializer;
        this._valueSerializer = mapSerializer._valueSerializer;
        this._dynamicValueSerializers = mapSerializer._dynamicValueSerializers;
        this._property = mapSerializer._property;
        this._filterId = mapSerializer._filterId;
        this._sortKeys = mapSerializer._sortKeys;
        Object obj2 = obj == Include.NON_ABSENT ? this._valueType.isReferenceType() ? Include.NON_EMPTY : Include.NON_NULL : obj;
        this._suppressableValue = obj2;
    }

    protected MapSerializer(MapSerializer mapSerializer, Object obj, boolean z) {
        super(Map.class, false);
        this._ignoredEntries = mapSerializer._ignoredEntries;
        this._keyType = mapSerializer._keyType;
        this._valueType = mapSerializer._valueType;
        this._valueTypeIsStatic = mapSerializer._valueTypeIsStatic;
        this._valueTypeSerializer = mapSerializer._valueTypeSerializer;
        this._keySerializer = mapSerializer._keySerializer;
        this._valueSerializer = mapSerializer._valueSerializer;
        this._dynamicValueSerializers = mapSerializer._dynamicValueSerializers;
        this._property = mapSerializer._property;
        this._filterId = obj;
        this._sortKeys = z;
        this._suppressableValue = mapSerializer._suppressableValue;
    }

    public MapSerializer _withValueTypeSerializer(TypeSerializer typeSerializer) {
        if (this._valueTypeSerializer == typeSerializer) {
            return this;
        }
        _ensureOverride();
        return new MapSerializer(this, typeSerializer, (Object) null);
    }

    public MapSerializer withResolved(BeanProperty beanProperty, JsonSerializer<?> jsonSerializer, JsonSerializer<?> jsonSerializer2, HashSet<String> hashSet, boolean z) {
        _ensureOverride();
        MapSerializer mapSerializer = new MapSerializer(this, beanProperty, jsonSerializer, jsonSerializer2, hashSet);
        if (z != mapSerializer._sortKeys) {
            return new MapSerializer(mapSerializer, this._filterId, z);
        }
        return mapSerializer;
    }

    public MapSerializer withFilterId(Object obj) {
        if (this._filterId == obj) {
            return this;
        }
        _ensureOverride();
        return new MapSerializer(this, obj, this._sortKeys);
    }

    public MapSerializer withContentInclusion(Object obj) {
        if (obj == this._suppressableValue) {
            return this;
        }
        _ensureOverride();
        return new MapSerializer(this, this._valueTypeSerializer, obj);
    }

    public static MapSerializer construct(String[] strArr, JavaType javaType, boolean z, TypeSerializer typeSerializer, JsonSerializer<Object> jsonSerializer, JsonSerializer<Object> jsonSerializer2, Object obj) {
        JavaType keyType;
        JavaType contentType;
        boolean z2;
        boolean z3 = false;
        HashSet arrayToSet = (strArr == null || strArr.length == 0) ? null : ArrayBuilders.arrayToSet(strArr);
        if (javaType == null) {
            JavaType javaType2 = UNSPECIFIED_TYPE;
            contentType = javaType2;
            keyType = javaType2;
        } else {
            keyType = javaType.getKeyType();
            contentType = javaType.getContentType();
        }
        if (!z) {
            if (contentType != null && contentType.isFinal()) {
                z3 = true;
            }
            z2 = z3;
        } else if (contentType.getRawClass() == Object.class) {
            z2 = false;
        } else {
            z2 = z;
        }
        MapSerializer mapSerializer = new MapSerializer(arrayToSet, keyType, contentType, z2, typeSerializer, jsonSerializer, jsonSerializer2);
        if (obj != null) {
            return mapSerializer.withFilterId(obj);
        }
        return mapSerializer;
    }

    /* JADX WARNING: Removed duplicated region for block: B:19:0x003e  */
    /* JADX WARNING: Removed duplicated region for block: B:22:0x0046  */
    /* JADX WARNING: Removed duplicated region for block: B:28:0x005a  */
    /* JADX WARNING: Removed duplicated region for block: B:30:0x005e  */
    /* JADX WARNING: Removed duplicated region for block: B:42:0x0088  */
    /* JADX WARNING: Removed duplicated region for block: B:43:0x008d  */
    /* JADX WARNING: Removed duplicated region for block: B:54:0x00b2  */
    /* JADX WARNING: Removed duplicated region for block: B:56:0x00b8  */
    /* JADX WARNING: Removed duplicated region for block: B:63:0x00cd  */
    /* JADX WARNING: Removed duplicated region for block: B:64:0x00cf  */
    /* JADX WARNING: Removed duplicated region for block: B:71:? A[RETURN, SYNTHETIC] */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    public com.fasterxml.jackson.databind.JsonSerializer<?> createContextual(com.fasterxml.jackson.databind.SerializerProvider r13, com.fasterxml.jackson.databind.BeanProperty r14) throws com.fasterxml.jackson.databind.JsonMappingException {
        /*
            r12 = this;
            r5 = 1
            r1 = 0
            r6 = 0
            com.fasterxml.jackson.databind.AnnotationIntrospector r8 = r13.getAnnotationIntrospector()
            if (r14 != 0) goto L_0x0083
            r0 = r1
        L_0x000a:
            java.lang.Object r4 = r12._suppressableValue
            if (r0 == 0) goto L_0x00db
            if (r8 == 0) goto L_0x00db
            java.lang.Object r2 = r8.findKeySerializer(r0)
            if (r2 == 0) goto L_0x00d8
            com.fasterxml.jackson.databind.JsonSerializer r2 = r13.serializerInstance(r0, r2)
        L_0x001a:
            java.lang.Object r3 = r8.findContentSerializer(r0)
            if (r3 == 0) goto L_0x00d5
            com.fasterxml.jackson.databind.JsonSerializer r1 = r13.serializerInstance(r0, r3)
            r3 = r1
        L_0x0025:
            if (r14 == 0) goto L_0x00d2
            com.fasterxml.jackson.databind.SerializationConfig r1 = r13.getConfig()
            java.lang.Class<java.util.Map> r7 = java.util.Map.class
            com.fasterxml.jackson.annotation.JsonInclude$Value r1 = r14.findPropertyInclusion(r1, r7)
            com.fasterxml.jackson.annotation.JsonInclude$Include r1 = r1.getContentInclusion()
            if (r1 == 0) goto L_0x00d2
            com.fasterxml.jackson.annotation.JsonInclude$Include r7 = com.fasterxml.jackson.annotation.JsonInclude.Include.USE_DEFAULTS
            if (r1 == r7) goto L_0x00d2
            r7 = r1
        L_0x003c:
            if (r3 != 0) goto L_0x00cf
            com.fasterxml.jackson.databind.JsonSerializer<java.lang.Object> r1 = r12._valueSerializer
        L_0x0040:
            com.fasterxml.jackson.databind.JsonSerializer r3 = r12.findConvertingContentSerializer(r13, r14, r1)
            if (r3 != 0) goto L_0x0088
            boolean r1 = r12._valueTypeIsStatic
            if (r1 == 0) goto L_0x0058
            com.fasterxml.jackson.databind.JavaType r1 = r12._valueType
            boolean r1 = r1.isJavaLangObject()
            if (r1 != 0) goto L_0x0058
            com.fasterxml.jackson.databind.JavaType r1 = r12._valueType
            com.fasterxml.jackson.databind.JsonSerializer r3 = r13.findValueSerializer(r1, r14)
        L_0x0058:
            if (r2 != 0) goto L_0x00cd
            com.fasterxml.jackson.databind.JsonSerializer<java.lang.Object> r1 = r12._keySerializer
        L_0x005c:
            if (r1 != 0) goto L_0x008d
            com.fasterxml.jackson.databind.JavaType r1 = r12._keyType
            com.fasterxml.jackson.databind.JsonSerializer r2 = r13.findKeySerializer(r1, r14)
        L_0x0064:
            java.util.HashSet<java.lang.String> r4 = r12._ignoredEntries
            if (r8 == 0) goto L_0x00cb
            if (r0 == 0) goto L_0x00cb
            java.lang.String[] r9 = r8.findPropertiesToIgnore(r0, r5)
            if (r9 == 0) goto L_0x0098
            if (r4 != 0) goto L_0x0092
            java.util.HashSet r1 = new java.util.HashSet
            r1.<init>()
        L_0x0077:
            int r10 = r9.length
            r4 = r6
        L_0x0079:
            if (r4 >= r10) goto L_0x0099
            r11 = r9[r4]
            r1.add(r11)
            int r4 = r4 + 1
            goto L_0x0079
        L_0x0083:
            com.fasterxml.jackson.databind.introspect.AnnotatedMember r0 = r14.getMember()
            goto L_0x000a
        L_0x0088:
            com.fasterxml.jackson.databind.JsonSerializer r3 = r13.handleSecondaryContextualization(r3, r14)
            goto L_0x0058
        L_0x008d:
            com.fasterxml.jackson.databind.JsonSerializer r2 = r13.handleSecondaryContextualization(r1, r14)
            goto L_0x0064
        L_0x0092:
            java.util.HashSet r1 = new java.util.HashSet
            r1.<init>(r4)
            goto L_0x0077
        L_0x0098:
            r1 = r4
        L_0x0099:
            java.lang.Boolean r0 = r8.findSerializationSortAlphabetically(r0)
            if (r0 == 0) goto L_0x00c9
            boolean r0 = r0.booleanValue()
            if (r0 == 0) goto L_0x00c9
            r0 = r5
        L_0x00a6:
            r5 = r0
            r4 = r1
        L_0x00a8:
            r0 = r12
            r1 = r14
            com.fasterxml.jackson.databind.ser.std.MapSerializer r0 = r0.withResolved(r1, r2, r3, r4, r5)
            java.lang.Object r1 = r12._suppressableValue
            if (r7 == r1) goto L_0x00b6
            com.fasterxml.jackson.databind.ser.std.MapSerializer r0 = r0.withContentInclusion(r7)
        L_0x00b6:
            if (r14 == 0) goto L_0x00c8
            com.fasterxml.jackson.databind.introspect.AnnotatedMember r1 = r14.getMember()
            if (r1 == 0) goto L_0x00c8
            java.lang.Object r1 = r8.findFilterId(r1)
            if (r1 == 0) goto L_0x00c8
            com.fasterxml.jackson.databind.ser.std.MapSerializer r0 = r0.withFilterId(r1)
        L_0x00c8:
            return r0
        L_0x00c9:
            r0 = r6
            goto L_0x00a6
        L_0x00cb:
            r5 = r6
            goto L_0x00a8
        L_0x00cd:
            r1 = r2
            goto L_0x005c
        L_0x00cf:
            r1 = r3
            goto L_0x0040
        L_0x00d2:
            r7 = r4
            goto L_0x003c
        L_0x00d5:
            r3 = r1
            goto L_0x0025
        L_0x00d8:
            r2 = r1
            goto L_0x001a
        L_0x00db:
            r2 = r1
            r3 = r1
            goto L_0x0025
        */
        throw new UnsupportedOperationException("Method not decompiled: com.fasterxml.jackson.databind.ser.std.MapSerializer.createContextual(com.fasterxml.jackson.databind.SerializerProvider, com.fasterxml.jackson.databind.BeanProperty):com.fasterxml.jackson.databind.JsonSerializer");
    }

    public JavaType getContentType() {
        return this._valueType;
    }

    public JsonSerializer<?> getContentSerializer() {
        return this._valueSerializer;
    }

    public boolean isEmpty(SerializerProvider serializerProvider, Map<?, ?> map) {
        if (map == null || map.isEmpty()) {
            return true;
        }
        Object obj = this._suppressableValue;
        if (obj == null || obj == Include.ALWAYS) {
            return false;
        }
        JsonSerializer<Object> jsonSerializer = this._valueSerializer;
        if (jsonSerializer != null) {
            for (Object next : map.values()) {
                if (next != null && !jsonSerializer.isEmpty(serializerProvider, next)) {
                    return false;
                }
            }
            return true;
        }
        PropertySerializerMap propertySerializerMap = this._dynamicValueSerializers;
        for (Object next2 : map.values()) {
            if (next2 != null) {
                Class cls = next2.getClass();
                JsonSerializer serializerFor = propertySerializerMap.serializerFor(cls);
                if (serializerFor == null) {
                    try {
                        serializerFor = _findAndAddDynamic(propertySerializerMap, cls, serializerProvider);
                        propertySerializerMap = this._dynamicValueSerializers;
                    } catch (JsonMappingException e) {
                        return false;
                    }
                }
                if (!serializerFor.isEmpty(serializerProvider, next2)) {
                    return false;
                }
            }
        }
        return true;
    }

    public boolean hasSingleElement(Map<?, ?> map) {
        return map.size() == 1;
    }

    public JsonSerializer<?> getKeySerializer() {
        return this._keySerializer;
    }

    public void serialize(Map<?, ?> map, JsonGenerator jsonGenerator, SerializerProvider serializerProvider) throws IOException {
        Map<?, ?> map2;
        jsonGenerator.writeStartObject();
        jsonGenerator.setCurrentValue(map);
        if (!map.isEmpty()) {
            Object obj = this._suppressableValue;
            if (obj == Include.ALWAYS) {
                obj = null;
            } else if (obj == null && !serializerProvider.isEnabled(SerializationFeature.WRITE_NULL_MAP_VALUES)) {
                obj = Include.NON_NULL;
            }
            if (this._sortKeys || serializerProvider.isEnabled(SerializationFeature.ORDER_MAP_ENTRIES_BY_KEYS)) {
                map2 = _orderEntries(map);
            } else {
                map2 = map;
            }
            if (this._filterId != null) {
                serializeFilteredFields(map2, jsonGenerator, serializerProvider, findPropertyFilter(serializerProvider, this._filterId, map2), obj);
            } else if (obj != null) {
                serializeOptionalFields(map2, jsonGenerator, serializerProvider, obj);
            } else if (this._valueSerializer != null) {
                serializeFieldsUsing(map2, jsonGenerator, serializerProvider, this._valueSerializer);
            } else {
                serializeFields(map2, jsonGenerator, serializerProvider);
            }
        }
        jsonGenerator.writeEndObject();
    }

    public void serializeWithType(Map<?, ?> map, JsonGenerator jsonGenerator, SerializerProvider serializerProvider, TypeSerializer typeSerializer) throws IOException {
        Map<?, ?> map2;
        typeSerializer.writeTypePrefixForObject(map, jsonGenerator);
        jsonGenerator.setCurrentValue(map);
        if (!map.isEmpty()) {
            Object obj = this._suppressableValue;
            if (obj == Include.ALWAYS) {
                obj = null;
            } else if (obj == null && !serializerProvider.isEnabled(SerializationFeature.WRITE_NULL_MAP_VALUES)) {
                obj = Include.NON_NULL;
            }
            if (this._sortKeys || serializerProvider.isEnabled(SerializationFeature.ORDER_MAP_ENTRIES_BY_KEYS)) {
                map2 = _orderEntries(map);
            } else {
                map2 = map;
            }
            if (this._filterId != null) {
                serializeFilteredFields(map2, jsonGenerator, serializerProvider, findPropertyFilter(serializerProvider, this._filterId, map2), obj);
            } else if (obj != null) {
                serializeOptionalFields(map2, jsonGenerator, serializerProvider, obj);
            } else if (this._valueSerializer != null) {
                serializeFieldsUsing(map2, jsonGenerator, serializerProvider, this._valueSerializer);
            } else {
                serializeFields(map2, jsonGenerator, serializerProvider);
            }
        } else {
            map2 = map;
        }
        typeSerializer.writeTypeSuffixForObject(map2, jsonGenerator);
    }

    public void serializeFields(Map<?, ?> map, JsonGenerator jsonGenerator, SerializerProvider serializerProvider) throws IOException {
        PropertySerializerMap propertySerializerMap;
        JsonSerializer<Object> _findAndAddDynamic;
        if (this._valueTypeSerializer != null) {
            serializeTypedFields(map, jsonGenerator, serializerProvider, null);
            return;
        }
        JsonSerializer<Object> jsonSerializer = this._keySerializer;
        HashSet<String> hashSet = this._ignoredEntries;
        PropertySerializerMap propertySerializerMap2 = this._dynamicValueSerializers;
        PropertySerializerMap propertySerializerMap3 = propertySerializerMap2;
        for (Entry entry : map.entrySet()) {
            Object value = entry.getValue();
            Object key = entry.getKey();
            if (key == null) {
                serializerProvider.findNullKeySerializer(this._keyType, this._property).serialize(null, jsonGenerator, serializerProvider);
            } else if (hashSet == null || !hashSet.contains(key)) {
                jsonSerializer.serialize(key, jsonGenerator, serializerProvider);
            }
            if (value == null) {
                serializerProvider.defaultSerializeNull(jsonGenerator);
            } else {
                JsonSerializer<Object> jsonSerializer2 = this._valueSerializer;
                if (jsonSerializer2 == null) {
                    Class cls = value.getClass();
                    jsonSerializer2 = propertySerializerMap3.serializerFor(cls);
                    if (jsonSerializer2 == null) {
                        if (this._valueType.hasGenericTypes()) {
                            _findAndAddDynamic = _findAndAddDynamic(propertySerializerMap3, serializerProvider.constructSpecializedType(this._valueType, cls), serializerProvider);
                        } else {
                            _findAndAddDynamic = _findAndAddDynamic(propertySerializerMap3, cls, serializerProvider);
                        }
                        propertySerializerMap = this._dynamicValueSerializers;
                        jsonSerializer2 = _findAndAddDynamic;
                        jsonSerializer2.serialize(value, jsonGenerator, serializerProvider);
                        propertySerializerMap3 = propertySerializerMap;
                    }
                }
                propertySerializerMap = propertySerializerMap3;
                try {
                    jsonSerializer2.serialize(value, jsonGenerator, serializerProvider);
                } catch (Exception e) {
                    wrapAndThrow(serializerProvider, (Throwable) e, (Object) map, "" + key);
                }
                propertySerializerMap3 = propertySerializerMap;
            }
        }
    }

    public void serializeOptionalFields(Map<?, ?> map, JsonGenerator jsonGenerator, SerializerProvider serializerProvider, Object obj) throws IOException {
        JsonSerializer<Object> jsonSerializer;
        JsonSerializer<Object> jsonSerializer2;
        PropertySerializerMap propertySerializerMap;
        JsonSerializer<Object> _findAndAddDynamic;
        if (this._valueTypeSerializer != null) {
            serializeTypedFields(map, jsonGenerator, serializerProvider, obj);
            return;
        }
        HashSet<String> hashSet = this._ignoredEntries;
        PropertySerializerMap propertySerializerMap2 = this._dynamicValueSerializers;
        PropertySerializerMap propertySerializerMap3 = propertySerializerMap2;
        for (Entry entry : map.entrySet()) {
            Object key = entry.getKey();
            if (key == null) {
                jsonSerializer = serializerProvider.findNullKeySerializer(this._keyType, this._property);
            } else if (hashSet == null || !hashSet.contains(key)) {
                jsonSerializer = this._keySerializer;
            }
            Object value = entry.getValue();
            if (value != null) {
                jsonSerializer2 = this._valueSerializer;
                if (jsonSerializer2 == null) {
                    Class cls = value.getClass();
                    jsonSerializer2 = propertySerializerMap3.serializerFor(cls);
                    if (jsonSerializer2 == null) {
                        if (this._valueType.hasGenericTypes()) {
                            _findAndAddDynamic = _findAndAddDynamic(propertySerializerMap3, serializerProvider.constructSpecializedType(this._valueType, cls), serializerProvider);
                        } else {
                            _findAndAddDynamic = _findAndAddDynamic(propertySerializerMap3, cls, serializerProvider);
                        }
                        propertySerializerMap = this._dynamicValueSerializers;
                        jsonSerializer2 = _findAndAddDynamic;
                        if (obj == Include.NON_EMPTY && jsonSerializer2.isEmpty(serializerProvider, value)) {
                            propertySerializerMap3 = propertySerializerMap;
                        }
                    }
                }
                propertySerializerMap = propertySerializerMap3;
                propertySerializerMap3 = propertySerializerMap;
            } else if (obj == null) {
                jsonSerializer2 = serializerProvider.getDefaultNullValueSerializer();
                propertySerializerMap = propertySerializerMap3;
            }
            try {
                jsonSerializer.serialize(key, jsonGenerator, serializerProvider);
                jsonSerializer2.serialize(value, jsonGenerator, serializerProvider);
            } catch (Exception e) {
                wrapAndThrow(serializerProvider, (Throwable) e, (Object) map, "" + key);
            }
            propertySerializerMap3 = propertySerializerMap;
        }
    }

    public void serializeFieldsUsing(Map<?, ?> map, JsonGenerator jsonGenerator, SerializerProvider serializerProvider, JsonSerializer<Object> jsonSerializer) throws IOException {
        JsonSerializer<Object> jsonSerializer2 = this._keySerializer;
        HashSet<String> hashSet = this._ignoredEntries;
        TypeSerializer typeSerializer = this._valueTypeSerializer;
        for (Entry entry : map.entrySet()) {
            Object key = entry.getKey();
            if (hashSet == null || !hashSet.contains(key)) {
                if (key == null) {
                    serializerProvider.findNullKeySerializer(this._keyType, this._property).serialize(null, jsonGenerator, serializerProvider);
                } else {
                    jsonSerializer2.serialize(key, jsonGenerator, serializerProvider);
                }
                Object value = entry.getValue();
                if (value == null) {
                    serializerProvider.defaultSerializeNull(jsonGenerator);
                } else if (typeSerializer == null) {
                    try {
                        jsonSerializer.serialize(value, jsonGenerator, serializerProvider);
                    } catch (Exception e) {
                        wrapAndThrow(serializerProvider, (Throwable) e, (Object) map, "" + key);
                    }
                } else {
                    jsonSerializer.serializeWithType(value, jsonGenerator, serializerProvider, typeSerializer);
                }
            }
        }
    }

    public void serializeFilteredFields(Map<?, ?> map, JsonGenerator jsonGenerator, SerializerProvider serializerProvider, PropertyFilter propertyFilter, Object obj) throws IOException {
        JsonSerializer<Object> jsonSerializer;
        JsonSerializer<Object> jsonSerializer2;
        PropertySerializerMap propertySerializerMap;
        JsonSerializer<Object> _findAndAddDynamic;
        HashSet<String> hashSet = this._ignoredEntries;
        PropertySerializerMap propertySerializerMap2 = this._dynamicValueSerializers;
        MapProperty mapProperty = new MapProperty(this._valueTypeSerializer, this._property);
        PropertySerializerMap propertySerializerMap3 = propertySerializerMap2;
        for (Entry entry : map.entrySet()) {
            Object key = entry.getKey();
            if (hashSet == null || !hashSet.contains(key)) {
                if (key == null) {
                    jsonSerializer = serializerProvider.findNullKeySerializer(this._keyType, this._property);
                } else {
                    jsonSerializer = this._keySerializer;
                }
                Object value = entry.getValue();
                if (value != null) {
                    jsonSerializer2 = this._valueSerializer;
                    if (jsonSerializer2 == null) {
                        Class cls = value.getClass();
                        jsonSerializer2 = propertySerializerMap3.serializerFor(cls);
                        if (jsonSerializer2 == null) {
                            if (this._valueType.hasGenericTypes()) {
                                _findAndAddDynamic = _findAndAddDynamic(propertySerializerMap3, serializerProvider.constructSpecializedType(this._valueType, cls), serializerProvider);
                            } else {
                                _findAndAddDynamic = _findAndAddDynamic(propertySerializerMap3, cls, serializerProvider);
                            }
                            propertySerializerMap = this._dynamicValueSerializers;
                            jsonSerializer2 = _findAndAddDynamic;
                            if (obj == Include.NON_EMPTY && jsonSerializer2.isEmpty(serializerProvider, value)) {
                                propertySerializerMap3 = propertySerializerMap;
                            }
                        }
                    }
                    propertySerializerMap = propertySerializerMap3;
                    propertySerializerMap3 = propertySerializerMap;
                } else if (obj == null) {
                    jsonSerializer2 = serializerProvider.getDefaultNullValueSerializer();
                    propertySerializerMap = propertySerializerMap3;
                }
                mapProperty.reset(key, jsonSerializer, jsonSerializer2);
                try {
                    propertyFilter.serializeAsField(value, jsonGenerator, serializerProvider, mapProperty);
                } catch (Exception e) {
                    wrapAndThrow(serializerProvider, (Throwable) e, (Object) map, "" + key);
                }
                propertySerializerMap3 = propertySerializerMap;
            }
        }
    }

    @Deprecated
    public void serializeFilteredFields(Map<?, ?> map, JsonGenerator jsonGenerator, SerializerProvider serializerProvider, PropertyFilter propertyFilter) throws IOException {
        serializeFilteredFields(map, jsonGenerator, serializerProvider, propertyFilter, serializerProvider.isEnabled(SerializationFeature.WRITE_NULL_MAP_VALUES) ? null : Include.NON_NULL);
    }

    public void serializeTypedFields(Map<?, ?> map, JsonGenerator jsonGenerator, SerializerProvider serializerProvider, Object obj) throws IOException {
        JsonSerializer<Object> jsonSerializer;
        JsonSerializer serializerFor;
        PropertySerializerMap propertySerializerMap;
        JsonSerializer _findAndAddDynamic;
        HashSet<String> hashSet = this._ignoredEntries;
        PropertySerializerMap propertySerializerMap2 = this._dynamicValueSerializers;
        PropertySerializerMap propertySerializerMap3 = propertySerializerMap2;
        for (Entry entry : map.entrySet()) {
            Object key = entry.getKey();
            if (key == null) {
                jsonSerializer = serializerProvider.findNullKeySerializer(this._keyType, this._property);
            } else if (hashSet == null || !hashSet.contains(key)) {
                jsonSerializer = this._keySerializer;
            }
            Object value = entry.getValue();
            if (value != null) {
                JsonSerializer<Object> jsonSerializer2 = this._valueSerializer;
                Class cls = value.getClass();
                serializerFor = propertySerializerMap3.serializerFor(cls);
                if (serializerFor == null) {
                    if (this._valueType.hasGenericTypes()) {
                        _findAndAddDynamic = _findAndAddDynamic(propertySerializerMap3, serializerProvider.constructSpecializedType(this._valueType, cls), serializerProvider);
                    } else {
                        _findAndAddDynamic = _findAndAddDynamic(propertySerializerMap3, cls, serializerProvider);
                    }
                    propertySerializerMap = this._dynamicValueSerializers;
                    serializerFor = _findAndAddDynamic;
                } else {
                    propertySerializerMap = propertySerializerMap3;
                }
                if (obj == Include.NON_EMPTY && serializerFor.isEmpty(serializerProvider, value)) {
                    propertySerializerMap3 = propertySerializerMap;
                }
            } else if (obj == null) {
                serializerFor = serializerProvider.getDefaultNullValueSerializer();
                propertySerializerMap = propertySerializerMap3;
            }
            jsonSerializer.serialize(key, jsonGenerator, serializerProvider);
            try {
                serializerFor.serializeWithType(value, jsonGenerator, serializerProvider, this._valueTypeSerializer);
            } catch (Exception e) {
                wrapAndThrow(serializerProvider, (Throwable) e, (Object) map, "" + key);
            }
            propertySerializerMap3 = propertySerializerMap;
        }
    }

    /* access modifiers changed from: protected */
    @Deprecated
    public void serializeTypedFields(Map<?, ?> map, JsonGenerator jsonGenerator, SerializerProvider serializerProvider) throws IOException {
        serializeTypedFields(map, jsonGenerator, serializerProvider, serializerProvider.isEnabled(SerializationFeature.WRITE_NULL_MAP_VALUES) ? null : Include.NON_NULL);
    }

    public JsonNode getSchema(SerializerProvider serializerProvider, Type type) {
        return createSchemaNode("object", true);
    }

    public void acceptJsonFormatVisitor(JsonFormatVisitorWrapper jsonFormatVisitorWrapper, JavaType javaType) throws JsonMappingException {
        JsonMapFormatVisitor expectMapFormat = jsonFormatVisitorWrapper == null ? null : jsonFormatVisitorWrapper.expectMapFormat(javaType);
        if (expectMapFormat != null) {
            expectMapFormat.keyFormat(this._keySerializer, this._keyType);
            JsonSerializer<Object> jsonSerializer = this._valueSerializer;
            if (jsonSerializer == null) {
                jsonSerializer = _findAndAddDynamic(this._dynamicValueSerializers, this._valueType, jsonFormatVisitorWrapper.getProvider());
            }
            expectMapFormat.valueFormat(jsonSerializer, this._valueType);
        }
    }

    /* access modifiers changed from: protected */
    public final JsonSerializer<Object> _findAndAddDynamic(PropertySerializerMap propertySerializerMap, Class<?> cls, SerializerProvider serializerProvider) throws JsonMappingException {
        SerializerAndMapResult findAndAddSecondarySerializer = propertySerializerMap.findAndAddSecondarySerializer(cls, serializerProvider, this._property);
        if (propertySerializerMap != findAndAddSecondarySerializer.map) {
            this._dynamicValueSerializers = findAndAddSecondarySerializer.map;
        }
        return findAndAddSecondarySerializer.serializer;
    }

    /* access modifiers changed from: protected */
    public final JsonSerializer<Object> _findAndAddDynamic(PropertySerializerMap propertySerializerMap, JavaType javaType, SerializerProvider serializerProvider) throws JsonMappingException {
        SerializerAndMapResult findAndAddSecondarySerializer = propertySerializerMap.findAndAddSecondarySerializer(javaType, serializerProvider, this._property);
        if (propertySerializerMap != findAndAddSecondarySerializer.map) {
            this._dynamicValueSerializers = findAndAddSecondarySerializer.map;
        }
        return findAndAddSecondarySerializer.serializer;
    }

    /* access modifiers changed from: protected */
    public Map<?, ?> _orderEntries(Map<?, ?> map) {
        return map instanceof SortedMap ? map : new TreeMap(map);
    }
}
