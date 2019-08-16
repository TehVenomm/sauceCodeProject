package com.fasterxml.jackson.databind.ser.impl;

import com.fasterxml.jackson.core.JsonGenerator;
import com.fasterxml.jackson.databind.BeanProperty;
import com.fasterxml.jackson.databind.JavaType;
import com.fasterxml.jackson.databind.JsonMappingException;
import com.fasterxml.jackson.databind.JsonNode;
import com.fasterxml.jackson.databind.JsonSerializer;
import com.fasterxml.jackson.databind.SerializationFeature;
import com.fasterxml.jackson.databind.SerializerProvider;
import com.fasterxml.jackson.databind.annotation.JacksonStdImpl;
import com.fasterxml.jackson.databind.jsonFormatVisitors.JsonFormatTypes;
import com.fasterxml.jackson.databind.jsonFormatVisitors.JsonFormatVisitorWrapper;
import com.fasterxml.jackson.databind.jsontype.TypeSerializer;
import com.fasterxml.jackson.databind.ser.ContainerSerializer;
import com.fasterxml.jackson.databind.ser.ContextualSerializer;
import com.fasterxml.jackson.databind.ser.std.ArraySerializerBase;
import com.fasterxml.jackson.databind.type.TypeFactory;
import java.io.IOException;
import java.lang.reflect.Type;

@JacksonStdImpl
public class StringArraySerializer extends ArraySerializerBase<String[]> implements ContextualSerializer {
    private static final JavaType VALUE_TYPE = TypeFactory.defaultInstance().uncheckedSimpleType(String.class);
    public static final StringArraySerializer instance = new StringArraySerializer();
    protected final JsonSerializer<Object> _elementSerializer;

    protected StringArraySerializer() {
        super(String[].class);
        this._elementSerializer = null;
    }

    public StringArraySerializer(StringArraySerializer stringArraySerializer, BeanProperty beanProperty, JsonSerializer<?> jsonSerializer, Boolean bool) {
        super(stringArraySerializer, beanProperty, bool);
        this._elementSerializer = jsonSerializer;
    }

    public JsonSerializer<?> _withResolved(BeanProperty beanProperty, Boolean bool) {
        return new StringArraySerializer(this, beanProperty, this._elementSerializer, bool);
    }

    public ContainerSerializer<?> _withValueTypeSerializer(TypeSerializer typeSerializer) {
        return this;
    }

    /* JADX WARNING: Removed duplicated region for block: B:12:0x0029  */
    /* JADX WARNING: Removed duplicated region for block: B:19:0x003e  */
    /* JADX WARNING: Removed duplicated region for block: B:21:0x004a  */
    /* JADX WARNING: Removed duplicated region for block: B:9:0x0021  */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    public com.fasterxml.jackson.databind.JsonSerializer<?> createContextual(com.fasterxml.jackson.databind.SerializerProvider r5, com.fasterxml.jackson.databind.BeanProperty r6) throws com.fasterxml.jackson.databind.JsonMappingException {
        /*
            r4 = this;
            r1 = 0
            if (r6 == 0) goto L_0x004c
            com.fasterxml.jackson.databind.AnnotationIntrospector r0 = r5.getAnnotationIntrospector()
            com.fasterxml.jackson.databind.introspect.AnnotatedMember r2 = r6.getMember()
            if (r2 == 0) goto L_0x004c
            java.lang.Object r0 = r0.findContentSerializer(r2)
            if (r0 == 0) goto L_0x004c
            com.fasterxml.jackson.databind.JsonSerializer r0 = r5.serializerInstance(r2, r0)
        L_0x0017:
            java.lang.Class<java.lang.String[]> r2 = java.lang.String[].class
            com.fasterxml.jackson.annotation.JsonFormat$Feature r3 = com.fasterxml.jackson.annotation.JsonFormat.Feature.WRITE_SINGLE_ELEM_ARRAYS_UNWRAPPED
            java.lang.Boolean r2 = r4.findFormatFeature(r5, r6, r2, r3)
            if (r0 != 0) goto L_0x0023
            com.fasterxml.jackson.databind.JsonSerializer<java.lang.Object> r0 = r4._elementSerializer
        L_0x0023:
            com.fasterxml.jackson.databind.JsonSerializer r0 = r4.findConvertingContentSerializer(r5, r6, r0)
            if (r0 != 0) goto L_0x003e
            java.lang.Class<java.lang.String> r0 = java.lang.String.class
            com.fasterxml.jackson.databind.JsonSerializer r0 = r5.findValueSerializer(r0, r6)
        L_0x002f:
            boolean r3 = r4.isDefaultSerializer(r0)
            if (r3 == 0) goto L_0x004a
        L_0x0035:
            com.fasterxml.jackson.databind.JsonSerializer<java.lang.Object> r0 = r4._elementSerializer
            if (r1 != r0) goto L_0x0043
            java.lang.Boolean r0 = r4._unwrapSingle
            if (r2 != r0) goto L_0x0043
        L_0x003d:
            return r4
        L_0x003e:
            com.fasterxml.jackson.databind.JsonSerializer r0 = r5.handleSecondaryContextualization(r0, r6)
            goto L_0x002f
        L_0x0043:
            com.fasterxml.jackson.databind.ser.impl.StringArraySerializer r0 = new com.fasterxml.jackson.databind.ser.impl.StringArraySerializer
            r0.<init>(r4, r6, r1, r2)
            r4 = r0
            goto L_0x003d
        L_0x004a:
            r1 = r0
            goto L_0x0035
        L_0x004c:
            r0 = r1
            goto L_0x0017
        */
        throw new UnsupportedOperationException("Method not decompiled: com.fasterxml.jackson.databind.ser.impl.StringArraySerializer.createContextual(com.fasterxml.jackson.databind.SerializerProvider, com.fasterxml.jackson.databind.BeanProperty):com.fasterxml.jackson.databind.JsonSerializer");
    }

    public JavaType getContentType() {
        return VALUE_TYPE;
    }

    public JsonSerializer<?> getContentSerializer() {
        return this._elementSerializer;
    }

    public boolean isEmpty(SerializerProvider serializerProvider, String[] strArr) {
        return strArr == null || strArr.length == 0;
    }

    public boolean hasSingleElement(String[] strArr) {
        return strArr.length == 1;
    }

    public final void serialize(String[] strArr, JsonGenerator jsonGenerator, SerializerProvider serializerProvider) throws IOException {
        int length = strArr.length;
        if (length != 1 || ((this._unwrapSingle != null || !serializerProvider.isEnabled(SerializationFeature.WRITE_SINGLE_ELEM_ARRAYS_UNWRAPPED)) && this._unwrapSingle != Boolean.TRUE)) {
            jsonGenerator.writeStartArray(length);
            serializeContents(strArr, jsonGenerator, serializerProvider);
            jsonGenerator.writeEndArray();
            return;
        }
        serializeContents(strArr, jsonGenerator, serializerProvider);
    }

    public void serializeContents(String[] strArr, JsonGenerator jsonGenerator, SerializerProvider serializerProvider) throws IOException {
        int length = strArr.length;
        if (length != 0) {
            if (this._elementSerializer != null) {
                serializeContentsSlow(strArr, jsonGenerator, serializerProvider, this._elementSerializer);
                return;
            }
            for (int i = 0; i < length; i++) {
                if (strArr[i] == null) {
                    jsonGenerator.writeNull();
                } else {
                    jsonGenerator.writeString(strArr[i]);
                }
            }
        }
    }

    private void serializeContentsSlow(String[] strArr, JsonGenerator jsonGenerator, SerializerProvider serializerProvider, JsonSerializer<Object> jsonSerializer) throws IOException {
        int length = strArr.length;
        for (int i = 0; i < length; i++) {
            if (strArr[i] == null) {
                serializerProvider.defaultSerializeNull(jsonGenerator);
            } else {
                jsonSerializer.serialize(strArr[i], jsonGenerator, serializerProvider);
            }
        }
    }

    public JsonNode getSchema(SerializerProvider serializerProvider, Type type) {
        return createSchemaNode("array", true).set("items", createSchemaNode("string"));
    }

    public void acceptJsonFormatVisitor(JsonFormatVisitorWrapper jsonFormatVisitorWrapper, JavaType javaType) throws JsonMappingException {
        visitArrayFormat(jsonFormatVisitorWrapper, javaType, JsonFormatTypes.STRING);
    }
}
