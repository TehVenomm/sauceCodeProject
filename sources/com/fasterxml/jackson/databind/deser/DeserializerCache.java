package com.fasterxml.jackson.databind.deser;

import com.fasterxml.jackson.annotation.JsonFormat.Shape;
import com.fasterxml.jackson.annotation.JsonFormat.Value;
import com.fasterxml.jackson.databind.AnnotationIntrospector;
import com.fasterxml.jackson.databind.BeanDescription;
import com.fasterxml.jackson.databind.DeserializationConfig;
import com.fasterxml.jackson.databind.DeserializationContext;
import com.fasterxml.jackson.databind.JavaType;
import com.fasterxml.jackson.databind.JsonDeserializer;
import com.fasterxml.jackson.databind.JsonDeserializer.None;
import com.fasterxml.jackson.databind.JsonMappingException;
import com.fasterxml.jackson.databind.JsonNode;
import com.fasterxml.jackson.databind.KeyDeserializer;
import com.fasterxml.jackson.databind.deser.std.StdDelegatingDeserializer;
import com.fasterxml.jackson.databind.introspect.Annotated;
import com.fasterxml.jackson.databind.type.ArrayType;
import com.fasterxml.jackson.databind.type.CollectionLikeType;
import com.fasterxml.jackson.databind.type.CollectionType;
import com.fasterxml.jackson.databind.type.MapLikeType;
import com.fasterxml.jackson.databind.type.MapType;
import com.fasterxml.jackson.databind.type.ReferenceType;
import com.fasterxml.jackson.databind.util.ClassUtil;
import com.fasterxml.jackson.databind.util.Converter;
import java.io.Serializable;
import java.util.HashMap;
import java.util.concurrent.ConcurrentHashMap;

public final class DeserializerCache implements Serializable {
    private static final long serialVersionUID = 1;
    protected final ConcurrentHashMap<JavaType, JsonDeserializer<Object>> _cachedDeserializers = new ConcurrentHashMap<>(64, 0.75f, 4);
    protected final HashMap<JavaType, JsonDeserializer<Object>> _incompleteDeserializers = new HashMap<>(8);

    /* access modifiers changed from: 0000 */
    public Object writeReplace() {
        this._incompleteDeserializers.clear();
        return this;
    }

    public int cachedDeserializersCount() {
        return this._cachedDeserializers.size();
    }

    public void flushCachedDeserializers() {
        this._cachedDeserializers.clear();
    }

    public JsonDeserializer<Object> findValueDeserializer(DeserializationContext deserializationContext, DeserializerFactory deserializerFactory, JavaType javaType) throws JsonMappingException {
        JsonDeserializer<Object> _findCachedDeserializer = _findCachedDeserializer(javaType);
        if (_findCachedDeserializer != null) {
            return _findCachedDeserializer;
        }
        JsonDeserializer<Object> _createAndCacheValueDeserializer = _createAndCacheValueDeserializer(deserializationContext, deserializerFactory, javaType);
        if (_createAndCacheValueDeserializer == null) {
            return _handleUnknownValueDeserializer(deserializationContext, javaType);
        }
        return _createAndCacheValueDeserializer;
    }

    public KeyDeserializer findKeyDeserializer(DeserializationContext deserializationContext, DeserializerFactory deserializerFactory, JavaType javaType) throws JsonMappingException {
        KeyDeserializer createKeyDeserializer = deserializerFactory.createKeyDeserializer(deserializationContext, javaType);
        if (createKeyDeserializer == null) {
            return _handleUnknownKeyDeserializer(deserializationContext, javaType);
        }
        if (!(createKeyDeserializer instanceof ResolvableDeserializer)) {
            return createKeyDeserializer;
        }
        ((ResolvableDeserializer) createKeyDeserializer).resolve(deserializationContext);
        return createKeyDeserializer;
    }

    public boolean hasValueDeserializerFor(DeserializationContext deserializationContext, DeserializerFactory deserializerFactory, JavaType javaType) throws JsonMappingException {
        JsonDeserializer _findCachedDeserializer = _findCachedDeserializer(javaType);
        if (_findCachedDeserializer == null) {
            _findCachedDeserializer = _createAndCacheValueDeserializer(deserializationContext, deserializerFactory, javaType);
        }
        return _findCachedDeserializer != null;
    }

    /* access modifiers changed from: protected */
    public JsonDeserializer<Object> _findCachedDeserializer(JavaType javaType) {
        if (javaType == null) {
            throw new IllegalArgumentException("Null JavaType passed");
        } else if (_hasCustomValueHandler(javaType)) {
            return null;
        } else {
            return (JsonDeserializer) this._cachedDeserializers.get(javaType);
        }
    }

    /* access modifiers changed from: protected */
    public JsonDeserializer<Object> _createAndCacheValueDeserializer(DeserializationContext deserializationContext, DeserializerFactory deserializerFactory, JavaType javaType) throws JsonMappingException {
        JsonDeserializer<Object> _findCachedDeserializer;
        synchronized (this._incompleteDeserializers) {
            _findCachedDeserializer = _findCachedDeserializer(javaType);
            if (_findCachedDeserializer == null) {
                int size = this._incompleteDeserializers.size();
                if (size > 0) {
                    _findCachedDeserializer = (JsonDeserializer) this._incompleteDeserializers.get(javaType);
                    if (_findCachedDeserializer != null) {
                    }
                }
                try {
                    _findCachedDeserializer = _createAndCache2(deserializationContext, deserializerFactory, javaType);
                    if (size == 0) {
                        if (this._incompleteDeserializers.size() > 0) {
                            this._incompleteDeserializers.clear();
                        }
                    }
                } catch (Throwable th) {
                    if (size == 0 && this._incompleteDeserializers.size() > 0) {
                        this._incompleteDeserializers.clear();
                    }
                    throw th;
                }
            }
        }
        return _findCachedDeserializer;
    }

    /* access modifiers changed from: protected */
    public JsonDeserializer<Object> _createAndCache2(DeserializationContext deserializationContext, DeserializerFactory deserializerFactory, JavaType javaType) throws JsonMappingException {
        boolean z;
        try {
            JsonDeserializer<Object> _createDeserializer = _createDeserializer(deserializationContext, deserializerFactory, javaType);
            if (_createDeserializer == null) {
                return null;
            }
            boolean z2 = _createDeserializer instanceof ResolvableDeserializer;
            if (_hasCustomValueHandler(javaType) || !_createDeserializer.isCachable()) {
                z = false;
            } else {
                z = true;
            }
            if (z2) {
                this._incompleteDeserializers.put(javaType, _createDeserializer);
                ((ResolvableDeserializer) _createDeserializer).resolve(deserializationContext);
                this._incompleteDeserializers.remove(javaType);
            }
            if (!z) {
                return _createDeserializer;
            }
            this._cachedDeserializers.put(javaType, _createDeserializer);
            return _createDeserializer;
        } catch (IllegalArgumentException e) {
            throw JsonMappingException.from(deserializationContext, e.getMessage(), (Throwable) e);
        }
    }

    /* access modifiers changed from: protected */
    public JsonDeserializer<Object> _createDeserializer(DeserializationContext deserializationContext, DeserializerFactory deserializerFactory, JavaType javaType) throws JsonMappingException {
        DeserializationConfig config = deserializationContext.getConfig();
        if (javaType.isAbstract() || javaType.isMapLikeType() || javaType.isCollectionLikeType()) {
            javaType = deserializerFactory.mapAbstractType(config, javaType);
        }
        BeanDescription introspect = config.introspect(javaType);
        JsonDeserializer findDeserializerFromAnnotation = findDeserializerFromAnnotation(deserializationContext, introspect.getClassInfo());
        if (findDeserializerFromAnnotation != null) {
            return findDeserializerFromAnnotation;
        }
        JavaType modifyTypeByAnnotation = modifyTypeByAnnotation(deserializationContext, introspect.getClassInfo(), javaType);
        if (modifyTypeByAnnotation != javaType) {
            introspect = config.introspect(modifyTypeByAnnotation);
            javaType = modifyTypeByAnnotation;
        }
        Class findPOJOBuilder = introspect.findPOJOBuilder();
        if (findPOJOBuilder != null) {
            return deserializerFactory.createBuilderBasedDeserializer(deserializationContext, javaType, introspect, findPOJOBuilder);
        }
        Converter findDeserializationConverter = introspect.findDeserializationConverter();
        if (findDeserializationConverter == null) {
            return _createDeserializer2(deserializationContext, deserializerFactory, javaType, introspect);
        }
        JavaType inputType = findDeserializationConverter.getInputType(deserializationContext.getTypeFactory());
        if (!inputType.hasRawClass(javaType.getRawClass())) {
            introspect = config.introspect(inputType);
        }
        return new StdDelegatingDeserializer(findDeserializationConverter, inputType, _createDeserializer2(deserializationContext, deserializerFactory, inputType, introspect));
    }

    /* access modifiers changed from: protected */
    public JsonDeserializer<?> _createDeserializer2(DeserializationContext deserializationContext, DeserializerFactory deserializerFactory, JavaType javaType, BeanDescription beanDescription) throws JsonMappingException {
        DeserializationConfig config = deserializationContext.getConfig();
        if (javaType.isEnumType()) {
            return deserializerFactory.createEnumDeserializer(deserializationContext, javaType, beanDescription);
        }
        if (javaType.isContainerType()) {
            if (javaType.isArrayType()) {
                return deserializerFactory.createArrayDeserializer(deserializationContext, (ArrayType) javaType, beanDescription);
            }
            if (javaType.isMapLikeType()) {
                MapLikeType mapLikeType = (MapLikeType) javaType;
                if (mapLikeType.isTrueMapType()) {
                    return deserializerFactory.createMapDeserializer(deserializationContext, (MapType) mapLikeType, beanDescription);
                }
                return deserializerFactory.createMapLikeDeserializer(deserializationContext, mapLikeType, beanDescription);
            } else if (javaType.isCollectionLikeType()) {
                Value findExpectedFormat = beanDescription.findExpectedFormat(null);
                if (findExpectedFormat == null || findExpectedFormat.getShape() != Shape.OBJECT) {
                    CollectionLikeType collectionLikeType = (CollectionLikeType) javaType;
                    if (collectionLikeType.isTrueCollectionType()) {
                        return deserializerFactory.createCollectionDeserializer(deserializationContext, (CollectionType) collectionLikeType, beanDescription);
                    }
                    return deserializerFactory.createCollectionLikeDeserializer(deserializationContext, collectionLikeType, beanDescription);
                }
            }
        }
        if (javaType.isReferenceType()) {
            return deserializerFactory.createReferenceDeserializer(deserializationContext, (ReferenceType) javaType, beanDescription);
        }
        if (JsonNode.class.isAssignableFrom(javaType.getRawClass())) {
            return deserializerFactory.createTreeDeserializer(config, javaType, beanDescription);
        }
        return deserializerFactory.createBeanDeserializer(deserializationContext, javaType, beanDescription);
    }

    /* access modifiers changed from: protected */
    public JsonDeserializer<Object> findDeserializerFromAnnotation(DeserializationContext deserializationContext, Annotated annotated) throws JsonMappingException {
        Object findDeserializer = deserializationContext.getAnnotationIntrospector().findDeserializer(annotated);
        if (findDeserializer == null) {
            return null;
        }
        return findConvertingDeserializer(deserializationContext, annotated, deserializationContext.deserializerInstance(annotated, findDeserializer));
    }

    /* access modifiers changed from: protected */
    public JsonDeserializer<Object> findConvertingDeserializer(DeserializationContext deserializationContext, Annotated annotated, JsonDeserializer<Object> jsonDeserializer) throws JsonMappingException {
        Converter findConverter = findConverter(deserializationContext, annotated);
        return findConverter == null ? jsonDeserializer : new StdDelegatingDeserializer(findConverter, findConverter.getInputType(deserializationContext.getTypeFactory()), jsonDeserializer);
    }

    /* access modifiers changed from: protected */
    public Converter<Object, Object> findConverter(DeserializationContext deserializationContext, Annotated annotated) throws JsonMappingException {
        Object findDeserializationConverter = deserializationContext.getAnnotationIntrospector().findDeserializationConverter(annotated);
        if (findDeserializationConverter == null) {
            return null;
        }
        return deserializationContext.converterInstance(annotated, findDeserializationConverter);
    }

    private JavaType modifyTypeByAnnotation(DeserializationContext deserializationContext, Annotated annotated, JavaType javaType) throws JsonMappingException {
        JsonDeserializer jsonDeserializer;
        AnnotationIntrospector annotationIntrospector = deserializationContext.getAnnotationIntrospector();
        if (annotationIntrospector == null) {
            return javaType;
        }
        if (javaType.isMapLikeType()) {
            JavaType keyType = javaType.getKeyType();
            if (keyType != null && keyType.getValueHandler() == null) {
                Object findKeyDeserializer = annotationIntrospector.findKeyDeserializer(annotated);
                if (findKeyDeserializer != null) {
                    KeyDeserializer keyDeserializerInstance = deserializationContext.keyDeserializerInstance(annotated, findKeyDeserializer);
                    if (keyDeserializerInstance != null) {
                        javaType = ((MapLikeType) javaType).withKeyValueHandler(keyDeserializerInstance);
                        javaType.getKeyType();
                    }
                }
            }
        }
        JavaType contentType = javaType.getContentType();
        if (contentType != null && contentType.getValueHandler() == null) {
            Object findContentDeserializer = annotationIntrospector.findContentDeserializer(annotated);
            if (findContentDeserializer != null) {
                if (findContentDeserializer instanceof JsonDeserializer) {
                    JsonDeserializer jsonDeserializer2 = (JsonDeserializer) findContentDeserializer;
                    jsonDeserializer = null;
                } else {
                    Class _verifyAsClass = _verifyAsClass(findContentDeserializer, "findContentDeserializer", None.class);
                    jsonDeserializer = _verifyAsClass != null ? deserializationContext.deserializerInstance(annotated, _verifyAsClass) : null;
                }
                if (jsonDeserializer != null) {
                    javaType = javaType.withContentValueHandler(jsonDeserializer);
                }
            }
        }
        return annotationIntrospector.refineDeserializationType(deserializationContext.getConfig(), annotated, javaType);
    }

    private boolean _hasCustomValueHandler(JavaType javaType) {
        if (!javaType.isContainerType()) {
            return false;
        }
        JavaType contentType = javaType.getContentType();
        if (contentType == null) {
            return false;
        }
        if (contentType.getValueHandler() == null && contentType.getTypeHandler() == null) {
            return false;
        }
        return true;
    }

    private Class<?> _verifyAsClass(Object obj, String str, Class<?> cls) {
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

    /* access modifiers changed from: protected */
    public JsonDeserializer<Object> _handleUnknownValueDeserializer(DeserializationContext deserializationContext, JavaType javaType) throws JsonMappingException {
        if (!ClassUtil.isConcrete(javaType.getRawClass())) {
            throw JsonMappingException.from(deserializationContext, "Can not find a Value deserializer for abstract type " + javaType);
        }
        throw JsonMappingException.from(deserializationContext, "Can not find a Value deserializer for type " + javaType);
    }

    /* access modifiers changed from: protected */
    public KeyDeserializer _handleUnknownKeyDeserializer(DeserializationContext deserializationContext, JavaType javaType) throws JsonMappingException {
        throw JsonMappingException.from(deserializationContext, "Can not find a (Map) Key deserializer for type " + javaType);
    }
}
