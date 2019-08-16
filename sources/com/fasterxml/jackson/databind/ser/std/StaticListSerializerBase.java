package com.fasterxml.jackson.databind.ser.std;

import com.fasterxml.jackson.databind.BeanProperty;
import com.fasterxml.jackson.databind.JavaType;
import com.fasterxml.jackson.databind.JsonMappingException;
import com.fasterxml.jackson.databind.JsonNode;
import com.fasterxml.jackson.databind.JsonSerializer;
import com.fasterxml.jackson.databind.SerializerProvider;
import com.fasterxml.jackson.databind.jsonFormatVisitors.JsonArrayFormatVisitor;
import com.fasterxml.jackson.databind.jsonFormatVisitors.JsonFormatVisitorWrapper;
import com.fasterxml.jackson.databind.ser.ContextualSerializer;
import java.lang.reflect.Type;
import java.util.Collection;

public abstract class StaticListSerializerBase<T extends Collection<?>> extends StdSerializer<T> implements ContextualSerializer {
    protected final JsonSerializer<String> _serializer;
    protected final Boolean _unwrapSingle;

    public abstract JsonSerializer<?> _withResolved(BeanProperty beanProperty, JsonSerializer<?> jsonSerializer, Boolean bool);

    /* access modifiers changed from: protected */
    public abstract void acceptContentVisitor(JsonArrayFormatVisitor jsonArrayFormatVisitor) throws JsonMappingException;

    /* access modifiers changed from: protected */
    public abstract JsonNode contentSchema();

    protected StaticListSerializerBase(Class<?> cls) {
        super(cls, false);
        this._serializer = null;
        this._unwrapSingle = null;
    }

    protected StaticListSerializerBase(StaticListSerializerBase<?> staticListSerializerBase, JsonSerializer<?> jsonSerializer, Boolean bool) {
        super((StdSerializer<?>) staticListSerializerBase);
        this._serializer = jsonSerializer;
        this._unwrapSingle = bool;
    }

    /* JADX WARNING: Removed duplicated region for block: B:24:0x0054  */
    /* JADX WARNING: Removed duplicated region for block: B:9:0x0023  */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    public com.fasterxml.jackson.databind.JsonSerializer<?> createContextual(com.fasterxml.jackson.databind.SerializerProvider r5, com.fasterxml.jackson.databind.BeanProperty r6) throws com.fasterxml.jackson.databind.JsonMappingException {
        /*
            r4 = this;
            r1 = 0
            if (r6 == 0) goto L_0x0058
            com.fasterxml.jackson.databind.AnnotationIntrospector r0 = r5.getAnnotationIntrospector()
            com.fasterxml.jackson.databind.introspect.AnnotatedMember r2 = r6.getMember()
            if (r2 == 0) goto L_0x0056
            java.lang.Object r0 = r0.findContentSerializer(r2)
            if (r0 == 0) goto L_0x0056
            com.fasterxml.jackson.databind.JsonSerializer r0 = r5.serializerInstance(r2, r0)
        L_0x0017:
            com.fasterxml.jackson.databind.SerializationConfig r2 = r5.getConfig()
            java.lang.Class r3 = r4._handledType
            com.fasterxml.jackson.annotation.JsonFormat$Value r2 = r6.findPropertyFormat(r2, r3)
            if (r2 == 0) goto L_0x0054
            com.fasterxml.jackson.annotation.JsonFormat$Feature r3 = com.fasterxml.jackson.annotation.JsonFormat.Feature.WRITE_SINGLE_ELEM_ARRAYS_UNWRAPPED
            java.lang.Boolean r2 = r2.getFeature(r3)
        L_0x0029:
            if (r0 != 0) goto L_0x002d
            com.fasterxml.jackson.databind.JsonSerializer<java.lang.String> r0 = r4._serializer
        L_0x002d:
            com.fasterxml.jackson.databind.JsonSerializer r0 = r4.findConvertingContentSerializer(r5, r6, r0)
            if (r0 != 0) goto L_0x0048
            java.lang.Class<java.lang.String> r0 = java.lang.String.class
            com.fasterxml.jackson.databind.JsonSerializer r0 = r5.findValueSerializer(r0, r6)
        L_0x0039:
            boolean r3 = r4.isDefaultSerializer(r0)
            if (r3 == 0) goto L_0x0052
        L_0x003f:
            com.fasterxml.jackson.databind.JsonSerializer<java.lang.String> r0 = r4._serializer
            if (r1 != r0) goto L_0x004d
            java.lang.Boolean r0 = r4._unwrapSingle
            if (r2 != r0) goto L_0x004d
        L_0x0047:
            return r4
        L_0x0048:
            com.fasterxml.jackson.databind.JsonSerializer r0 = r5.handleSecondaryContextualization(r0, r6)
            goto L_0x0039
        L_0x004d:
            com.fasterxml.jackson.databind.JsonSerializer r4 = r4._withResolved(r6, r1, r2)
            goto L_0x0047
        L_0x0052:
            r1 = r0
            goto L_0x003f
        L_0x0054:
            r2 = r1
            goto L_0x0029
        L_0x0056:
            r0 = r1
            goto L_0x0017
        L_0x0058:
            r2 = r1
            r0 = r1
            goto L_0x0029
        */
        throw new UnsupportedOperationException("Method not decompiled: com.fasterxml.jackson.databind.ser.std.StaticListSerializerBase.createContextual(com.fasterxml.jackson.databind.SerializerProvider, com.fasterxml.jackson.databind.BeanProperty):com.fasterxml.jackson.databind.JsonSerializer");
    }

    @Deprecated
    public boolean isEmpty(T t) {
        return isEmpty((SerializerProvider) null, t);
    }

    public boolean isEmpty(SerializerProvider serializerProvider, T t) {
        return t == null || t.size() == 0;
    }

    public JsonNode getSchema(SerializerProvider serializerProvider, Type type) {
        return createSchemaNode("array", true).set("items", contentSchema());
    }

    public void acceptJsonFormatVisitor(JsonFormatVisitorWrapper jsonFormatVisitorWrapper, JavaType javaType) throws JsonMappingException {
        acceptContentVisitor(jsonFormatVisitorWrapper.expectArrayFormat(javaType));
    }
}
