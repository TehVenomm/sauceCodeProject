package com.fasterxml.jackson.databind.deser;

import com.fasterxml.jackson.core.JsonParser;
import com.fasterxml.jackson.core.JsonToken;
import com.fasterxml.jackson.databind.BeanProperty;
import com.fasterxml.jackson.databind.DeserializationContext;
import com.fasterxml.jackson.databind.JavaType;
import com.fasterxml.jackson.databind.JsonDeserializer;
import com.fasterxml.jackson.databind.JsonMappingException;
import com.fasterxml.jackson.databind.deser.impl.ReadableObjectId.Referring;
import com.fasterxml.jackson.databind.introspect.AnnotatedMethod;
import com.fasterxml.jackson.databind.jsontype.TypeDeserializer;
import java.io.IOException;
import java.io.Serializable;

public class SettableAnyProperty implements Serializable {
    private static final long serialVersionUID = 1;
    protected final BeanProperty _property;
    protected final AnnotatedMethod _setter;
    protected final JavaType _type;
    protected JsonDeserializer<Object> _valueDeserializer;
    protected final TypeDeserializer _valueTypeDeserializer;

    private static class AnySetterReferring extends Referring {
        private final SettableAnyProperty _parent;
        private final Object _pojo;
        private final String _propName;

        public AnySetterReferring(SettableAnyProperty settableAnyProperty, UnresolvedForwardReference unresolvedForwardReference, Class<?> cls, Object obj, String str) {
            super(unresolvedForwardReference, cls);
            this._parent = settableAnyProperty;
            this._pojo = obj;
            this._propName = str;
        }

        public void handleResolvedForwardReference(Object obj, Object obj2) throws IOException {
            if (!hasId(obj)) {
                throw new IllegalArgumentException("Trying to resolve a forward reference with id [" + obj.toString() + "] that wasn't previously registered.");
            }
            this._parent.set(this._pojo, this._propName, obj2);
        }
    }

    public SettableAnyProperty(BeanProperty beanProperty, AnnotatedMethod annotatedMethod, JavaType javaType, JsonDeserializer<Object> jsonDeserializer, TypeDeserializer typeDeserializer) {
        this._property = beanProperty;
        this._setter = annotatedMethod;
        this._type = javaType;
        this._valueDeserializer = jsonDeserializer;
        this._valueTypeDeserializer = typeDeserializer;
    }

    public SettableAnyProperty withValueDeserializer(JsonDeserializer<Object> jsonDeserializer) {
        return new SettableAnyProperty(this._property, this._setter, this._type, jsonDeserializer, this._valueTypeDeserializer);
    }

    protected SettableAnyProperty(SettableAnyProperty settableAnyProperty) {
        this._property = settableAnyProperty._property;
        this._setter = settableAnyProperty._setter;
        this._type = settableAnyProperty._type;
        this._valueDeserializer = settableAnyProperty._valueDeserializer;
        this._valueTypeDeserializer = settableAnyProperty._valueTypeDeserializer;
    }

    /* access modifiers changed from: 0000 */
    public Object readResolve() {
        if (this._setter != null && this._setter.getAnnotated() != null) {
            return this;
        }
        throw new IllegalArgumentException("Missing method (broken JDK (de)serialization?)");
    }

    public BeanProperty getProperty() {
        return this._property;
    }

    public boolean hasValueDeserializer() {
        return this._valueDeserializer != null;
    }

    public JavaType getType() {
        return this._type;
    }

    public final void deserializeAndSet(JsonParser jsonParser, DeserializationContext deserializationContext, Object obj, String str) throws IOException {
        try {
            set(obj, str, deserialize(jsonParser, deserializationContext));
        } catch (UnresolvedForwardReference e) {
            if (this._valueDeserializer.getObjectIdReader() == null) {
                throw JsonMappingException.from(jsonParser, "Unresolved forward reference but no identity info.", (Throwable) e);
            }
            e.getRoid().appendReferring(new AnySetterReferring(this, e, this._type.getRawClass(), obj, str));
        }
    }

    public Object deserialize(JsonParser jsonParser, DeserializationContext deserializationContext) throws IOException {
        if (jsonParser.getCurrentToken() == JsonToken.VALUE_NULL) {
            return null;
        }
        if (this._valueTypeDeserializer != null) {
            return this._valueDeserializer.deserializeWithType(jsonParser, deserializationContext, this._valueTypeDeserializer);
        }
        return this._valueDeserializer.deserialize(jsonParser, deserializationContext);
    }

    public void set(Object obj, String str, Object obj2) throws IOException {
        try {
            this._setter.getAnnotated().invoke(obj, new Object[]{str, obj2});
        } catch (Exception e) {
            _throwAsIOE(e, str, obj2);
        }
    }

    /* JADX WARNING: type inference failed for: r6v1, types: [java.lang.Throwable] */
    /* JADX WARNING: type inference failed for: r6v2, types: [java.lang.Throwable] */
    /* access modifiers changed from: protected */
    /* JADX WARNING: Multi-variable type inference failed */
    /* JADX WARNING: Unknown variable types count: 1 */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    public void _throwAsIOE(java.lang.Exception r6, java.lang.String r7, java.lang.Object r8) throws java.io.IOException {
        /*
            r5 = this;
            r4 = 0
            boolean r0 = r6 instanceof java.lang.IllegalArgumentException
            if (r0 == 0) goto L_0x0071
            if (r8 != 0) goto L_0x0062
            java.lang.String r0 = "[NULL]"
        L_0x0009:
            java.lang.StringBuilder r1 = new java.lang.StringBuilder
            java.lang.String r2 = "Problem deserializing \"any\" property '"
            r1.<init>(r2)
            java.lang.StringBuilder r1 = r1.append(r7)
            java.lang.StringBuilder r2 = new java.lang.StringBuilder
            r2.<init>()
            java.lang.String r3 = "' of class "
            java.lang.StringBuilder r2 = r2.append(r3)
            java.lang.String r3 = r5.getClassName()
            java.lang.StringBuilder r2 = r2.append(r3)
            java.lang.String r3 = " (expected type: "
            java.lang.StringBuilder r2 = r2.append(r3)
            java.lang.String r2 = r2.toString()
            java.lang.StringBuilder r2 = r1.append(r2)
            com.fasterxml.jackson.databind.JavaType r3 = r5._type
            r2.append(r3)
            java.lang.String r2 = "; actual type: "
            java.lang.StringBuilder r2 = r1.append(r2)
            java.lang.StringBuilder r0 = r2.append(r0)
            java.lang.String r2 = ")"
            r0.append(r2)
            java.lang.String r0 = r6.getMessage()
            if (r0 == 0) goto L_0x006b
            java.lang.String r2 = ", problem: "
            java.lang.StringBuilder r2 = r1.append(r2)
            r2.append(r0)
        L_0x0058:
            com.fasterxml.jackson.databind.JsonMappingException r0 = new com.fasterxml.jackson.databind.JsonMappingException
            java.lang.String r1 = r1.toString()
            r0.<init>(r4, r1, r6)
            throw r0
        L_0x0062:
            java.lang.Class r0 = r8.getClass()
            java.lang.String r0 = r0.getName()
            goto L_0x0009
        L_0x006b:
            java.lang.String r0 = " (no error message provided)"
            r1.append(r0)
            goto L_0x0058
        L_0x0071:
            boolean r0 = r6 instanceof java.io.IOException
            if (r0 == 0) goto L_0x0078
            java.io.IOException r6 = (java.io.IOException) r6
            throw r6
        L_0x0078:
            boolean r0 = r6 instanceof java.lang.RuntimeException
            if (r0 == 0) goto L_0x007f
            java.lang.RuntimeException r6 = (java.lang.RuntimeException) r6
            throw r6
        L_0x007f:
            java.lang.Throwable r0 = r6.getCause()
            if (r0 == 0) goto L_0x008a
            java.lang.Throwable r6 = r6.getCause()
            goto L_0x007f
        L_0x008a:
            com.fasterxml.jackson.databind.JsonMappingException r0 = new com.fasterxml.jackson.databind.JsonMappingException
            java.lang.String r1 = r6.getMessage()
            r0.<init>(r4, r1, r6)
            throw r0
        */
        throw new UnsupportedOperationException("Method not decompiled: com.fasterxml.jackson.databind.deser.SettableAnyProperty._throwAsIOE(java.lang.Exception, java.lang.String, java.lang.Object):void");
    }

    private String getClassName() {
        return this._setter.getDeclaringClass().getName();
    }

    public String toString() {
        return "[any property on class " + getClassName() + "]";
    }
}
