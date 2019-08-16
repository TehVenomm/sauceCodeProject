package com.fasterxml.jackson.databind.ser.impl;

import com.fasterxml.jackson.core.JsonGenerationException;
import com.fasterxml.jackson.core.JsonGenerator;
import com.fasterxml.jackson.databind.AnnotationIntrospector;
import com.fasterxml.jackson.databind.BeanProperty;
import com.fasterxml.jackson.databind.JavaType;
import com.fasterxml.jackson.databind.JsonMappingException;
import com.fasterxml.jackson.databind.JsonSerializer;
import com.fasterxml.jackson.databind.SerializationFeature;
import com.fasterxml.jackson.databind.SerializerProvider;
import com.fasterxml.jackson.databind.annotation.JacksonStdImpl;
import com.fasterxml.jackson.databind.introspect.AnnotatedMember;
import com.fasterxml.jackson.databind.jsontype.TypeSerializer;
import com.fasterxml.jackson.databind.ser.ContainerSerializer;
import com.fasterxml.jackson.databind.ser.ContextualSerializer;
import com.fasterxml.jackson.databind.ser.impl.PropertySerializerMap.SerializerAndMapResult;
import java.io.IOException;
import java.util.Map;
import java.util.Map.Entry;

@JacksonStdImpl
public class MapEntrySerializer extends ContainerSerializer<Entry<?, ?>> implements ContextualSerializer {
    protected PropertySerializerMap _dynamicValueSerializers;
    protected final JavaType _entryType;
    protected JsonSerializer<Object> _keySerializer;
    protected final JavaType _keyType;
    protected final BeanProperty _property;
    protected JsonSerializer<Object> _valueSerializer;
    protected final JavaType _valueType;
    protected final boolean _valueTypeIsStatic;
    protected final TypeSerializer _valueTypeSerializer;

    public MapEntrySerializer(JavaType javaType, JavaType javaType2, JavaType javaType3, boolean z, TypeSerializer typeSerializer, BeanProperty beanProperty) {
        super(javaType);
        this._entryType = javaType;
        this._keyType = javaType2;
        this._valueType = javaType3;
        this._valueTypeIsStatic = z;
        this._valueTypeSerializer = typeSerializer;
        this._property = beanProperty;
        this._dynamicValueSerializers = PropertySerializerMap.emptyForProperties();
    }

    protected MapEntrySerializer(MapEntrySerializer mapEntrySerializer, BeanProperty beanProperty, TypeSerializer typeSerializer, JsonSerializer<?> jsonSerializer, JsonSerializer<?> jsonSerializer2) {
        super(Map.class, false);
        this._entryType = mapEntrySerializer._entryType;
        this._keyType = mapEntrySerializer._keyType;
        this._valueType = mapEntrySerializer._valueType;
        this._valueTypeIsStatic = mapEntrySerializer._valueTypeIsStatic;
        this._valueTypeSerializer = mapEntrySerializer._valueTypeSerializer;
        this._keySerializer = jsonSerializer;
        this._valueSerializer = jsonSerializer2;
        this._dynamicValueSerializers = mapEntrySerializer._dynamicValueSerializers;
        this._property = mapEntrySerializer._property;
    }

    public ContainerSerializer<?> _withValueTypeSerializer(TypeSerializer typeSerializer) {
        return new MapEntrySerializer(this, this._property, typeSerializer, this._keySerializer, this._valueSerializer);
    }

    public MapEntrySerializer withResolved(BeanProperty beanProperty, JsonSerializer<?> jsonSerializer, JsonSerializer<?> jsonSerializer2) {
        return new MapEntrySerializer(this, beanProperty, this._valueTypeSerializer, jsonSerializer, jsonSerializer2);
    }

    public JsonSerializer<?> createContextual(SerializerProvider serializerProvider, BeanProperty beanProperty) throws JsonMappingException {
        JsonSerializer<Object> jsonSerializer;
        JsonSerializer<Object> jsonSerializer2;
        JsonSerializer<Object> jsonSerializer3;
        JsonSerializer handleSecondaryContextualization;
        JsonSerializer<Object> jsonSerializer4;
        JsonSerializer handleSecondaryContextualization2;
        JsonSerializer<Object> jsonSerializer5;
        AnnotationIntrospector annotationIntrospector = serializerProvider.getAnnotationIntrospector();
        AnnotatedMember member = beanProperty == null ? null : beanProperty.getMember();
        if (member == null || annotationIntrospector == null) {
            jsonSerializer = null;
            jsonSerializer2 = null;
        } else {
            Object findKeySerializer = annotationIntrospector.findKeySerializer(member);
            if (findKeySerializer != null) {
                jsonSerializer5 = serializerProvider.serializerInstance(member, findKeySerializer);
            } else {
                jsonSerializer5 = null;
            }
            Object findContentSerializer = annotationIntrospector.findContentSerializer(member);
            if (findContentSerializer != null) {
                JsonSerializer<Object> serializerInstance = serializerProvider.serializerInstance(member, findContentSerializer);
                jsonSerializer = jsonSerializer5;
                jsonSerializer2 = serializerInstance;
            } else {
                jsonSerializer = jsonSerializer5;
                jsonSerializer2 = null;
            }
        }
        if (jsonSerializer2 == null) {
            jsonSerializer3 = this._valueSerializer;
        } else {
            jsonSerializer3 = jsonSerializer2;
        }
        JsonSerializer findConvertingContentSerializer = findConvertingContentSerializer(serializerProvider, beanProperty, jsonSerializer3);
        if (findConvertingContentSerializer != null) {
            handleSecondaryContextualization = serializerProvider.handleSecondaryContextualization(findConvertingContentSerializer, beanProperty);
        } else if (!this._valueTypeIsStatic || this._valueType.isJavaLangObject()) {
            handleSecondaryContextualization = findConvertingContentSerializer;
        } else {
            handleSecondaryContextualization = serializerProvider.findValueSerializer(this._valueType, beanProperty);
        }
        if (jsonSerializer == null) {
            jsonSerializer4 = this._keySerializer;
        } else {
            jsonSerializer4 = jsonSerializer;
        }
        if (jsonSerializer4 == null) {
            handleSecondaryContextualization2 = serializerProvider.findKeySerializer(this._keyType, beanProperty);
        } else {
            handleSecondaryContextualization2 = serializerProvider.handleSecondaryContextualization(jsonSerializer4, beanProperty);
        }
        return withResolved(beanProperty, handleSecondaryContextualization2, handleSecondaryContextualization);
    }

    public JavaType getContentType() {
        return this._valueType;
    }

    public JsonSerializer<?> getContentSerializer() {
        return this._valueSerializer;
    }

    public boolean hasSingleElement(Entry<?, ?> entry) {
        return true;
    }

    public boolean isEmpty(SerializerProvider serializerProvider, Entry<?, ?> entry) {
        return entry == null;
    }

    public void serialize(Entry<?, ?> entry, JsonGenerator jsonGenerator, SerializerProvider serializerProvider) throws IOException {
        jsonGenerator.writeStartObject();
        jsonGenerator.setCurrentValue(entry);
        if (this._valueSerializer != null) {
            serializeUsing(entry, jsonGenerator, serializerProvider, this._valueSerializer);
        } else {
            serializeDynamic(entry, jsonGenerator, serializerProvider);
        }
        jsonGenerator.writeEndObject();
    }

    public void serializeWithType(Entry<?, ?> entry, JsonGenerator jsonGenerator, SerializerProvider serializerProvider, TypeSerializer typeSerializer) throws IOException {
        typeSerializer.writeTypePrefixForObject(entry, jsonGenerator);
        jsonGenerator.setCurrentValue(entry);
        if (this._valueSerializer != null) {
            serializeUsing(entry, jsonGenerator, serializerProvider, this._valueSerializer);
        } else {
            serializeDynamic(entry, jsonGenerator, serializerProvider);
        }
        typeSerializer.writeTypeSuffixForObject(entry, jsonGenerator);
    }

    /* access modifiers changed from: protected */
    public void serializeDynamic(Entry<?, ?> entry, JsonGenerator jsonGenerator, SerializerProvider serializerProvider) throws IOException {
        JsonSerializer<Object> jsonSerializer = this._keySerializer;
        boolean z = !serializerProvider.isEnabled(SerializationFeature.WRITE_NULL_MAP_VALUES);
        TypeSerializer typeSerializer = this._valueTypeSerializer;
        PropertySerializerMap propertySerializerMap = this._dynamicValueSerializers;
        Object value = entry.getValue();
        Object key = entry.getKey();
        if (key == null) {
            serializerProvider.findNullKeySerializer(this._keyType, this._property).serialize(null, jsonGenerator, serializerProvider);
        } else if (!z || value != null) {
            jsonSerializer.serialize(key, jsonGenerator, serializerProvider);
        } else {
            return;
        }
        if (value == null) {
            serializerProvider.defaultSerializeNull(jsonGenerator);
            return;
        }
        Class cls = value.getClass();
        JsonSerializer serializerFor = propertySerializerMap.serializerFor(cls);
        if (serializerFor == null) {
            if (this._valueType.hasGenericTypes()) {
                serializerFor = _findAndAddDynamic(propertySerializerMap, serializerProvider.constructSpecializedType(this._valueType, cls), serializerProvider);
            } else {
                serializerFor = _findAndAddDynamic(propertySerializerMap, cls, serializerProvider);
            }
            PropertySerializerMap propertySerializerMap2 = this._dynamicValueSerializers;
        }
        if (typeSerializer == null) {
            try {
                serializerFor.serialize(value, jsonGenerator, serializerProvider);
            } catch (Exception e) {
                wrapAndThrow(serializerProvider, (Throwable) e, (Object) entry, "" + key);
            }
        } else {
            serializerFor.serializeWithType(value, jsonGenerator, serializerProvider, typeSerializer);
        }
    }

    /* access modifiers changed from: protected */
    public void serializeUsing(Entry<?, ?> entry, JsonGenerator jsonGenerator, SerializerProvider serializerProvider, JsonSerializer<Object> jsonSerializer) throws IOException, JsonGenerationException {
        JsonSerializer<Object> jsonSerializer2 = this._keySerializer;
        TypeSerializer typeSerializer = this._valueTypeSerializer;
        boolean z = !serializerProvider.isEnabled(SerializationFeature.WRITE_NULL_MAP_VALUES);
        Object value = entry.getValue();
        Object key = entry.getKey();
        if (key == null) {
            serializerProvider.findNullKeySerializer(this._keyType, this._property).serialize(null, jsonGenerator, serializerProvider);
        } else if (!z || value != null) {
            jsonSerializer2.serialize(key, jsonGenerator, serializerProvider);
        } else {
            return;
        }
        if (value == null) {
            serializerProvider.defaultSerializeNull(jsonGenerator);
        } else if (typeSerializer == null) {
            try {
                jsonSerializer.serialize(value, jsonGenerator, serializerProvider);
            } catch (Exception e) {
                wrapAndThrow(serializerProvider, (Throwable) e, (Object) entry, "" + key);
            }
        } else {
            jsonSerializer.serializeWithType(value, jsonGenerator, serializerProvider, typeSerializer);
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
}
