package com.fasterxml.jackson.databind.ser.std;

import com.fasterxml.jackson.core.JsonGenerator;
import com.fasterxml.jackson.databind.BeanProperty;
import com.fasterxml.jackson.databind.JavaType;
import com.fasterxml.jackson.databind.JsonMappingException;
import com.fasterxml.jackson.databind.JsonSerializer;
import com.fasterxml.jackson.databind.SerializationFeature;
import com.fasterxml.jackson.databind.SerializerProvider;
import com.fasterxml.jackson.databind.jsonFormatVisitors.JsonFormatVisitorWrapper;
import com.fasterxml.jackson.databind.jsontype.TypeSerializer;
import com.fasterxml.jackson.databind.ser.ContainerSerializer;
import com.fasterxml.jackson.databind.ser.ContextualSerializer;
import com.fasterxml.jackson.databind.ser.impl.PropertySerializerMap;
import com.fasterxml.jackson.databind.ser.impl.PropertySerializerMap.SerializerAndMapResult;
import java.io.IOException;

public abstract class AsArraySerializerBase<T> extends ContainerSerializer<T> implements ContextualSerializer {
    protected PropertySerializerMap _dynamicSerializers;
    protected final JsonSerializer<Object> _elementSerializer;
    protected final JavaType _elementType;
    protected final BeanProperty _property;
    protected final boolean _staticTyping;
    protected final Boolean _unwrapSingle;
    protected final TypeSerializer _valueTypeSerializer;

    /* access modifiers changed from: protected */
    public abstract void serializeContents(T t, JsonGenerator jsonGenerator, SerializerProvider serializerProvider) throws IOException;

    public abstract AsArraySerializerBase<T> withResolved(BeanProperty beanProperty, TypeSerializer typeSerializer, JsonSerializer<?> jsonSerializer, Boolean bool);

    protected AsArraySerializerBase(Class<?> cls, JavaType javaType, boolean z, TypeSerializer typeSerializer, JsonSerializer<Object> jsonSerializer) {
        boolean z2 = false;
        super(cls, false);
        this._elementType = javaType;
        if (z || (javaType != null && javaType.isFinal())) {
            z2 = true;
        }
        this._staticTyping = z2;
        this._valueTypeSerializer = typeSerializer;
        this._property = null;
        this._elementSerializer = jsonSerializer;
        this._dynamicSerializers = PropertySerializerMap.emptyForProperties();
        this._unwrapSingle = null;
    }

    @Deprecated
    protected AsArraySerializerBase(Class<?> cls, JavaType javaType, boolean z, TypeSerializer typeSerializer, BeanProperty beanProperty, JsonSerializer<Object> jsonSerializer) {
        boolean z2 = false;
        super(cls, false);
        this._elementType = javaType;
        if (z || (javaType != null && javaType.isFinal())) {
            z2 = true;
        }
        this._staticTyping = z2;
        this._valueTypeSerializer = typeSerializer;
        this._property = beanProperty;
        this._elementSerializer = jsonSerializer;
        this._dynamicSerializers = PropertySerializerMap.emptyForProperties();
        this._unwrapSingle = null;
    }

    protected AsArraySerializerBase(AsArraySerializerBase<?> asArraySerializerBase, BeanProperty beanProperty, TypeSerializer typeSerializer, JsonSerializer<?> jsonSerializer, Boolean bool) {
        super((ContainerSerializer<?>) asArraySerializerBase);
        this._elementType = asArraySerializerBase._elementType;
        this._staticTyping = asArraySerializerBase._staticTyping;
        this._valueTypeSerializer = typeSerializer;
        this._property = beanProperty;
        this._elementSerializer = jsonSerializer;
        this._dynamicSerializers = asArraySerializerBase._dynamicSerializers;
        this._unwrapSingle = bool;
    }

    @Deprecated
    protected AsArraySerializerBase(AsArraySerializerBase<?> asArraySerializerBase, BeanProperty beanProperty, TypeSerializer typeSerializer, JsonSerializer<?> jsonSerializer) {
        this(asArraySerializerBase, beanProperty, typeSerializer, jsonSerializer, asArraySerializerBase._unwrapSingle);
    }

    @Deprecated
    public final AsArraySerializerBase<T> withResolved(BeanProperty beanProperty, TypeSerializer typeSerializer, JsonSerializer<?> jsonSerializer) {
        return withResolved(beanProperty, typeSerializer, jsonSerializer, this._unwrapSingle);
    }

    /* JADX WARNING: Removed duplicated region for block: B:11:0x002c  */
    /* JADX WARNING: Removed duplicated region for block: B:33:0x006d  */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    public com.fasterxml.jackson.databind.JsonSerializer<?> createContextual(com.fasterxml.jackson.databind.SerializerProvider r6, com.fasterxml.jackson.databind.BeanProperty r7) throws com.fasterxml.jackson.databind.JsonMappingException {
        /*
            r5 = this;
            r1 = 0
            com.fasterxml.jackson.databind.jsontype.TypeSerializer r0 = r5._valueTypeSerializer
            if (r0 == 0) goto L_0x0074
            com.fasterxml.jackson.databind.jsontype.TypeSerializer r0 = r0.forProperty(r7)
            r3 = r0
        L_0x000a:
            if (r7 == 0) goto L_0x0071
            com.fasterxml.jackson.databind.AnnotationIntrospector r0 = r6.getAnnotationIntrospector()
            com.fasterxml.jackson.databind.introspect.AnnotatedMember r2 = r7.getMember()
            if (r2 == 0) goto L_0x006f
            java.lang.Object r0 = r0.findContentSerializer(r2)
            if (r0 == 0) goto L_0x006f
            com.fasterxml.jackson.databind.JsonSerializer r0 = r6.serializerInstance(r2, r0)
        L_0x0020:
            com.fasterxml.jackson.databind.SerializationConfig r2 = r6.getConfig()
            java.lang.Class r4 = r5._handledType
            com.fasterxml.jackson.annotation.JsonFormat$Value r2 = r7.findPropertyFormat(r2, r4)
            if (r2 == 0) goto L_0x006d
            com.fasterxml.jackson.annotation.JsonFormat$Feature r1 = com.fasterxml.jackson.annotation.JsonFormat.Feature.WRITE_SINGLE_ELEM_ARRAYS_UNWRAPPED
            java.lang.Boolean r1 = r2.getFeature(r1)
            r2 = r1
        L_0x0033:
            if (r0 != 0) goto L_0x0037
            com.fasterxml.jackson.databind.JsonSerializer<java.lang.Object> r0 = r5._elementSerializer
        L_0x0037:
            com.fasterxml.jackson.databind.JsonSerializer r0 = r5.findConvertingContentSerializer(r6, r7, r0)
            if (r0 != 0) goto L_0x0068
            com.fasterxml.jackson.databind.JavaType r1 = r5._elementType
            if (r1 == 0) goto L_0x0053
            boolean r1 = r5._staticTyping
            if (r1 == 0) goto L_0x0053
            com.fasterxml.jackson.databind.JavaType r1 = r5._elementType
            boolean r1 = r1.isJavaLangObject()
            if (r1 != 0) goto L_0x0053
            com.fasterxml.jackson.databind.JavaType r0 = r5._elementType
            com.fasterxml.jackson.databind.JsonSerializer r0 = r6.findValueSerializer(r0, r7)
        L_0x0053:
            com.fasterxml.jackson.databind.JsonSerializer<java.lang.Object> r1 = r5._elementSerializer
            if (r0 != r1) goto L_0x0063
            com.fasterxml.jackson.databind.BeanProperty r1 = r5._property
            if (r7 != r1) goto L_0x0063
            com.fasterxml.jackson.databind.jsontype.TypeSerializer r1 = r5._valueTypeSerializer
            if (r1 != r3) goto L_0x0063
            java.lang.Boolean r1 = r5._unwrapSingle
            if (r1 == r2) goto L_0x0067
        L_0x0063:
            com.fasterxml.jackson.databind.ser.std.AsArraySerializerBase r5 = r5.withResolved(r7, r3, r0, r2)
        L_0x0067:
            return r5
        L_0x0068:
            com.fasterxml.jackson.databind.JsonSerializer r0 = r6.handleSecondaryContextualization(r0, r7)
            goto L_0x0053
        L_0x006d:
            r2 = r1
            goto L_0x0033
        L_0x006f:
            r0 = r1
            goto L_0x0020
        L_0x0071:
            r2 = r1
            r0 = r1
            goto L_0x0033
        L_0x0074:
            r3 = r0
            goto L_0x000a
        */
        throw new UnsupportedOperationException("Method not decompiled: com.fasterxml.jackson.databind.ser.std.AsArraySerializerBase.createContextual(com.fasterxml.jackson.databind.SerializerProvider, com.fasterxml.jackson.databind.BeanProperty):com.fasterxml.jackson.databind.JsonSerializer");
    }

    public JavaType getContentType() {
        return this._elementType;
    }

    public JsonSerializer<?> getContentSerializer() {
        return this._elementSerializer;
    }

    public void serialize(T t, JsonGenerator jsonGenerator, SerializerProvider serializerProvider) throws IOException {
        if (!serializerProvider.isEnabled(SerializationFeature.WRITE_SINGLE_ELEM_ARRAYS_UNWRAPPED) || !hasSingleElement(t)) {
            jsonGenerator.writeStartArray();
            jsonGenerator.setCurrentValue(t);
            serializeContents(t, jsonGenerator, serializerProvider);
            jsonGenerator.writeEndArray();
            return;
        }
        serializeContents(t, jsonGenerator, serializerProvider);
    }

    public void serializeWithType(T t, JsonGenerator jsonGenerator, SerializerProvider serializerProvider, TypeSerializer typeSerializer) throws IOException {
        typeSerializer.writeTypePrefixForArray(t, jsonGenerator);
        jsonGenerator.setCurrentValue(t);
        serializeContents(t, jsonGenerator, serializerProvider);
        typeSerializer.writeTypeSuffixForArray(t, jsonGenerator);
    }

    /* JADX WARNING: Removed duplicated region for block: B:8:0x0026  */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    public com.fasterxml.jackson.databind.JsonNode getSchema(com.fasterxml.jackson.databind.SerializerProvider r6, java.lang.reflect.Type r7) throws com.fasterxml.jackson.databind.JsonMappingException {
        /*
            r5 = this;
            r1 = 0
            java.lang.String r0 = "array"
            r2 = 1
            com.fasterxml.jackson.databind.node.ObjectNode r2 = r5.createSchemaNode(r0, r2)
            com.fasterxml.jackson.databind.JavaType r0 = r5._elementType
            if (r0 == 0) goto L_0x002f
            java.lang.Class r3 = r0.getRawClass()
            java.lang.Class<java.lang.Object> r4 = java.lang.Object.class
            if (r3 == r4) goto L_0x0030
            com.fasterxml.jackson.databind.BeanProperty r3 = r5._property
            com.fasterxml.jackson.databind.JsonSerializer r0 = r6.findValueSerializer(r0, r3)
            boolean r3 = r0 instanceof com.fasterxml.jackson.databind.jsonschema.SchemaAware
            if (r3 == 0) goto L_0x0030
            com.fasterxml.jackson.databind.jsonschema.SchemaAware r0 = (com.fasterxml.jackson.databind.jsonschema.SchemaAware) r0
            com.fasterxml.jackson.databind.JsonNode r0 = r0.getSchema(r6, r1)
        L_0x0024:
            if (r0 != 0) goto L_0x002a
            com.fasterxml.jackson.databind.JsonNode r0 = com.fasterxml.jackson.databind.jsonschema.JsonSchema.getDefaultSchemaNode()
        L_0x002a:
            java.lang.String r1 = "items"
            r2.set(r1, r0)
        L_0x002f:
            return r2
        L_0x0030:
            r0 = r1
            goto L_0x0024
        */
        throw new UnsupportedOperationException("Method not decompiled: com.fasterxml.jackson.databind.ser.std.AsArraySerializerBase.getSchema(com.fasterxml.jackson.databind.SerializerProvider, java.lang.reflect.Type):com.fasterxml.jackson.databind.JsonNode");
    }

    public void acceptJsonFormatVisitor(JsonFormatVisitorWrapper jsonFormatVisitorWrapper, JavaType javaType) throws JsonMappingException {
        JsonSerializer<Object> jsonSerializer = this._elementSerializer;
        if (jsonSerializer == null) {
            jsonSerializer = jsonFormatVisitorWrapper.getProvider().findValueSerializer(this._elementType, this._property);
        }
        visitArrayFormat(jsonFormatVisitorWrapper, javaType, jsonSerializer, this._elementType);
    }

    /* access modifiers changed from: protected */
    public final JsonSerializer<Object> _findAndAddDynamic(PropertySerializerMap propertySerializerMap, Class<?> cls, SerializerProvider serializerProvider) throws JsonMappingException {
        SerializerAndMapResult findAndAddSecondarySerializer = propertySerializerMap.findAndAddSecondarySerializer(cls, serializerProvider, this._property);
        if (propertySerializerMap != findAndAddSecondarySerializer.map) {
            this._dynamicSerializers = findAndAddSecondarySerializer.map;
        }
        return findAndAddSecondarySerializer.serializer;
    }

    /* access modifiers changed from: protected */
    public final JsonSerializer<Object> _findAndAddDynamic(PropertySerializerMap propertySerializerMap, JavaType javaType, SerializerProvider serializerProvider) throws JsonMappingException {
        SerializerAndMapResult findAndAddSecondarySerializer = propertySerializerMap.findAndAddSecondarySerializer(javaType, serializerProvider, this._property);
        if (propertySerializerMap != findAndAddSecondarySerializer.map) {
            this._dynamicSerializers = findAndAddSecondarySerializer.map;
        }
        return findAndAddSecondarySerializer.serializer;
    }
}
