package com.fasterxml.jackson.databind.ser;

import com.fasterxml.jackson.annotation.ObjectIdGenerator;
import com.fasterxml.jackson.core.JsonGenerator;
import com.fasterxml.jackson.databind.BeanProperty;
import com.fasterxml.jackson.databind.JavaType;
import com.fasterxml.jackson.databind.JsonMappingException;
import com.fasterxml.jackson.databind.JsonNode;
import com.fasterxml.jackson.databind.JsonSerializer;
import com.fasterxml.jackson.databind.JsonSerializer.None;
import com.fasterxml.jackson.databind.PropertyName;
import com.fasterxml.jackson.databind.SerializationConfig;
import com.fasterxml.jackson.databind.SerializationFeature;
import com.fasterxml.jackson.databind.SerializerProvider;
import com.fasterxml.jackson.databind.cfg.HandlerInstantiator;
import com.fasterxml.jackson.databind.introspect.Annotated;
import com.fasterxml.jackson.databind.jsonFormatVisitors.JsonFormatVisitorWrapper;
import com.fasterxml.jackson.databind.jsonschema.JsonSchema;
import com.fasterxml.jackson.databind.jsonschema.SchemaAware;
import com.fasterxml.jackson.databind.jsontype.TypeSerializer;
import com.fasterxml.jackson.databind.node.ObjectNode;
import com.fasterxml.jackson.databind.ser.impl.WritableObjectId;
import com.fasterxml.jackson.databind.util.ClassUtil;
import java.io.Closeable;
import java.io.IOException;
import java.io.Serializable;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.IdentityHashMap;
import java.util.Map;

public abstract class DefaultSerializerProvider extends SerializerProvider implements Serializable {
    private static final long serialVersionUID = 1;
    protected transient ArrayList<ObjectIdGenerator<?>> _objectIdGenerators;
    protected transient Map<Object, WritableObjectId> _seenObjectIds;

    public static final class Impl extends DefaultSerializerProvider {
        private static final long serialVersionUID = 1;

        public Impl() {
        }

        public Impl(Impl impl) {
            super(impl);
        }

        protected Impl(SerializerProvider serializerProvider, SerializationConfig serializationConfig, SerializerFactory serializerFactory) {
            super(serializerProvider, serializationConfig, serializerFactory);
        }

        public DefaultSerializerProvider copy() {
            if (getClass() != Impl.class) {
                return DefaultSerializerProvider.super.copy();
            }
            return new Impl(this);
        }

        public Impl createInstance(SerializationConfig serializationConfig, SerializerFactory serializerFactory) {
            return new Impl(this, serializationConfig, serializerFactory);
        }
    }

    public abstract DefaultSerializerProvider createInstance(SerializationConfig serializationConfig, SerializerFactory serializerFactory);

    protected DefaultSerializerProvider() {
    }

    protected DefaultSerializerProvider(SerializerProvider serializerProvider, SerializationConfig serializationConfig, SerializerFactory serializerFactory) {
        super(serializerProvider, serializationConfig, serializerFactory);
    }

    protected DefaultSerializerProvider(DefaultSerializerProvider defaultSerializerProvider) {
        super(defaultSerializerProvider);
    }

    public DefaultSerializerProvider copy() {
        throw new IllegalStateException("DefaultSerializerProvider sub-class not overriding copy()");
    }

    public void serializeValue(JsonGenerator jsonGenerator, Object obj) throws IOException {
        boolean z = true;
        if (obj == null) {
            _serializeNull(jsonGenerator);
            return;
        }
        JsonSerializer findTypedValueSerializer = findTypedValueSerializer(obj.getClass(), true, (BeanProperty) null);
        PropertyName fullRootName = this._config.getFullRootName();
        if (fullRootName == null) {
            z = this._config.isEnabled(SerializationFeature.WRAP_ROOT_VALUE);
            if (z) {
                jsonGenerator.writeStartObject();
                jsonGenerator.writeFieldName(this._config.findRootName(obj.getClass()).simpleAsEncoded(this._config));
            }
        } else if (fullRootName.isEmpty()) {
            z = false;
        } else {
            jsonGenerator.writeStartObject();
            jsonGenerator.writeFieldName(fullRootName.getSimpleName());
        }
        try {
            findTypedValueSerializer.serialize(obj, jsonGenerator, this);
            if (z) {
                jsonGenerator.writeEndObject();
            }
        } catch (IOException e) {
            throw e;
        } catch (Exception e2) {
            Exception exc = e2;
            String message = exc.getMessage();
            if (message == null) {
                message = "[no message for " + exc.getClass().getName() + "]";
            }
            throw new JsonMappingException((Closeable) jsonGenerator, message, (Throwable) exc);
        }
    }

    public void serializeValue(JsonGenerator jsonGenerator, Object obj, JavaType javaType) throws IOException {
        boolean z = true;
        if (obj == null) {
            _serializeNull(jsonGenerator);
            return;
        }
        if (!javaType.getRawClass().isAssignableFrom(obj.getClass())) {
            _reportIncompatibleRootType(obj, javaType);
        }
        JsonSerializer findTypedValueSerializer = findTypedValueSerializer(javaType, true, (BeanProperty) null);
        PropertyName fullRootName = this._config.getFullRootName();
        if (fullRootName == null) {
            z = this._config.isEnabled(SerializationFeature.WRAP_ROOT_VALUE);
            if (z) {
                jsonGenerator.writeStartObject();
                jsonGenerator.writeFieldName(this._config.findRootName(obj.getClass()).simpleAsEncoded(this._config));
            }
        } else if (fullRootName.isEmpty()) {
            z = false;
        } else {
            jsonGenerator.writeStartObject();
            jsonGenerator.writeFieldName(fullRootName.getSimpleName());
        }
        try {
            findTypedValueSerializer.serialize(obj, jsonGenerator, this);
            if (z) {
                jsonGenerator.writeEndObject();
            }
        } catch (IOException e) {
            throw e;
        } catch (Exception e2) {
            Exception exc = e2;
            String message = exc.getMessage();
            if (message == null) {
                message = "[no message for " + exc.getClass().getName() + "]";
            }
            throw JsonMappingException.from(jsonGenerator, message, (Throwable) exc);
        }
    }

    public void serializeValue(JsonGenerator jsonGenerator, Object obj, JavaType javaType, JsonSerializer<Object> jsonSerializer) throws IOException {
        boolean z = true;
        if (obj == null) {
            _serializeNull(jsonGenerator);
            return;
        }
        if (javaType != null && !javaType.getRawClass().isAssignableFrom(obj.getClass())) {
            _reportIncompatibleRootType(obj, javaType);
        }
        if (jsonSerializer == null) {
            jsonSerializer = findTypedValueSerializer(javaType, true, (BeanProperty) null);
        }
        PropertyName fullRootName = this._config.getFullRootName();
        if (fullRootName == null) {
            boolean isEnabled = this._config.isEnabled(SerializationFeature.WRAP_ROOT_VALUE);
            if (isEnabled) {
                jsonGenerator.writeStartObject();
                jsonGenerator.writeFieldName((javaType == null ? this._config.findRootName(obj.getClass()) : this._config.findRootName(javaType)).simpleAsEncoded(this._config));
                z = isEnabled;
            } else {
                z = isEnabled;
            }
        } else if (fullRootName.isEmpty()) {
            z = false;
        } else {
            jsonGenerator.writeStartObject();
            jsonGenerator.writeFieldName(fullRootName.getSimpleName());
        }
        try {
            jsonSerializer.serialize(obj, jsonGenerator, this);
            if (z) {
                jsonGenerator.writeEndObject();
            }
        } catch (IOException e) {
            throw e;
        } catch (Exception e2) {
            Exception exc = e2;
            String message = exc.getMessage();
            if (message == null) {
                message = "[no message for " + exc.getClass().getName() + "]";
            }
            throw JsonMappingException.from(jsonGenerator, message, (Throwable) exc);
        }
    }

    public void serializePolymorphic(JsonGenerator jsonGenerator, Object obj, JavaType javaType, JsonSerializer<Object> jsonSerializer, TypeSerializer typeSerializer) throws IOException {
        boolean z;
        if (obj == null) {
            _serializeNull(jsonGenerator);
            return;
        }
        if (javaType != null && !javaType.getRawClass().isAssignableFrom(obj.getClass())) {
            _reportIncompatibleRootType(obj, javaType);
        }
        if (jsonSerializer == null) {
            if (javaType == null || !javaType.isContainerType()) {
                jsonSerializer = findValueSerializer(obj.getClass(), (BeanProperty) null);
            } else {
                jsonSerializer = findValueSerializer(javaType, (BeanProperty) null);
            }
        }
        PropertyName fullRootName = this._config.getFullRootName();
        if (fullRootName == null) {
            z = this._config.isEnabled(SerializationFeature.WRAP_ROOT_VALUE);
            if (z) {
                jsonGenerator.writeStartObject();
                jsonGenerator.writeFieldName(this._config.findRootName(obj.getClass()).simpleAsEncoded(this._config));
            }
        } else if (fullRootName.isEmpty()) {
            z = false;
        } else {
            z = true;
            jsonGenerator.writeStartObject();
            jsonGenerator.writeFieldName(fullRootName.getSimpleName());
        }
        try {
            jsonSerializer.serializeWithType(obj, jsonGenerator, this, typeSerializer);
            if (z) {
                jsonGenerator.writeEndObject();
            }
        } catch (IOException e) {
            throw e;
        } catch (Exception e2) {
            Exception exc = e2;
            String message = exc.getMessage();
            if (message == null) {
                message = "[no message for " + exc.getClass().getName() + "]";
            }
            throw JsonMappingException.from(jsonGenerator, message, (Throwable) exc);
        }
    }

    @Deprecated
    public void serializePolymorphic(JsonGenerator jsonGenerator, Object obj, TypeSerializer typeSerializer) throws IOException {
        serializePolymorphic(jsonGenerator, obj, obj == null ? null : this._config.constructType(obj.getClass()), null, typeSerializer);
    }

    /* access modifiers changed from: protected */
    public void _serializeNull(JsonGenerator jsonGenerator) throws IOException {
        try {
            getDefaultNullValueSerializer().serialize(null, jsonGenerator, this);
        } catch (IOException e) {
            throw e;
        } catch (Exception e2) {
            String message = e2.getMessage();
            if (message == null) {
                message = "[no message for " + e2.getClass().getName() + "]";
            }
            throw JsonMappingException.from(jsonGenerator, message, (Throwable) e2);
        }
    }

    @Deprecated
    public JsonSchema generateJsonSchema(Class<?> cls) throws JsonMappingException {
        if (cls == null) {
            throw new IllegalArgumentException("A class must be provided");
        }
        JsonSerializer findValueSerializer = findValueSerializer(cls, (BeanProperty) null);
        JsonNode defaultSchemaNode = findValueSerializer instanceof SchemaAware ? ((SchemaAware) findValueSerializer).getSchema(this, null) : JsonSchema.getDefaultSchemaNode();
        if (defaultSchemaNode instanceof ObjectNode) {
            return new JsonSchema((ObjectNode) defaultSchemaNode);
        }
        throw new IllegalArgumentException("Class " + cls.getName() + " would not be serialized as a JSON object and therefore has no schema");
    }

    public void acceptJsonFormatVisitor(JavaType javaType, JsonFormatVisitorWrapper jsonFormatVisitorWrapper) throws JsonMappingException {
        if (javaType == null) {
            throw new IllegalArgumentException("A class must be provided");
        }
        jsonFormatVisitorWrapper.setProvider(this);
        findValueSerializer(javaType, (BeanProperty) null).acceptJsonFormatVisitor(jsonFormatVisitorWrapper, javaType);
    }

    /* JADX WARNING: Code restructure failed: missing block: B:19:?, code lost:
        return false;
     */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    public boolean hasSerializerFor(java.lang.Class<?> r5, java.util.concurrent.atomic.AtomicReference<java.lang.Throwable> r6) {
        /*
            r4 = this;
            r0 = 1
            r1 = 0
            java.lang.Class<java.lang.Object> r2 = java.lang.Object.class
            if (r5 != r2) goto L_0x0011
            com.fasterxml.jackson.databind.SerializationConfig r2 = r4._config
            com.fasterxml.jackson.databind.SerializationFeature r3 = com.fasterxml.jackson.databind.SerializationFeature.FAIL_ON_EMPTY_BEANS
            boolean r2 = r2.isEnabled(r3)
            if (r2 != 0) goto L_0x0011
        L_0x0010:
            return r0
        L_0x0011:
            com.fasterxml.jackson.databind.JsonSerializer r2 = r4._findExplicitUntypedSerializer(r5)     // Catch:{ JsonMappingException -> 0x0019, RuntimeException -> 0x0021 }
            if (r2 != 0) goto L_0x0010
            r0 = r1
            goto L_0x0010
        L_0x0019:
            r0 = move-exception
            if (r6 == 0) goto L_0x001f
            r6.set(r0)
        L_0x001f:
            r0 = r1
            goto L_0x0010
        L_0x0021:
            r0 = move-exception
            if (r6 != 0) goto L_0x0025
            throw r0
        L_0x0025:
            r6.set(r0)
            goto L_0x001f
        */
        throw new UnsupportedOperationException("Method not decompiled: com.fasterxml.jackson.databind.ser.DefaultSerializerProvider.hasSerializerFor(java.lang.Class, java.util.concurrent.atomic.AtomicReference):boolean");
    }

    public int cachedSerializersCount() {
        return this._serializerCache.size();
    }

    public void flushCachedSerializers() {
        this._serializerCache.flush();
    }

    public WritableObjectId findObjectId(Object obj, ObjectIdGenerator<?> objectIdGenerator) {
        ObjectIdGenerator objectIdGenerator2;
        if (this._seenObjectIds == null) {
            this._seenObjectIds = _createObjectIdMap();
        } else {
            WritableObjectId writableObjectId = (WritableObjectId) this._seenObjectIds.get(obj);
            if (writableObjectId != null) {
                return writableObjectId;
            }
        }
        if (this._objectIdGenerators != null) {
            int size = this._objectIdGenerators.size();
            int i = 0;
            while (true) {
                if (i >= size) {
                    objectIdGenerator2 = null;
                    break;
                }
                objectIdGenerator2 = (ObjectIdGenerator) this._objectIdGenerators.get(i);
                if (objectIdGenerator2.canUseFor(objectIdGenerator)) {
                    break;
                }
                i++;
            }
        } else {
            this._objectIdGenerators = new ArrayList<>(8);
            objectIdGenerator2 = null;
        }
        if (objectIdGenerator2 == null) {
            objectIdGenerator2 = objectIdGenerator.newForSerialization(this);
            this._objectIdGenerators.add(objectIdGenerator2);
        }
        WritableObjectId writableObjectId2 = new WritableObjectId(objectIdGenerator2);
        this._seenObjectIds.put(obj, writableObjectId2);
        return writableObjectId2;
    }

    /* access modifiers changed from: protected */
    public Map<Object, WritableObjectId> _createObjectIdMap() {
        if (isEnabled(SerializationFeature.USE_EQUALITY_FOR_OBJECT_ID)) {
            return new HashMap();
        }
        return new IdentityHashMap();
    }

    public JsonSerializer<Object> serializerInstance(Annotated annotated, Object obj) throws JsonMappingException {
        JsonSerializer jsonSerializer;
        JsonSerializer jsonSerializer2 = null;
        if (obj == null) {
            return null;
        }
        if (obj instanceof JsonSerializer) {
            jsonSerializer = (JsonSerializer) obj;
        } else if (!(obj instanceof Class)) {
            throw new IllegalStateException("AnnotationIntrospector returned serializer definition of type " + obj.getClass().getName() + "; expected type JsonSerializer or Class<JsonSerializer> instead");
        } else {
            Class<None> cls = (Class) obj;
            if (cls == None.class || ClassUtil.isBogusClass(cls)) {
                return null;
            }
            if (!JsonSerializer.class.isAssignableFrom(cls)) {
                throw new IllegalStateException("AnnotationIntrospector returned Class " + cls.getName() + "; expected Class<JsonSerializer>");
            }
            HandlerInstantiator handlerInstantiator = this._config.getHandlerInstantiator();
            if (handlerInstantiator != null) {
                jsonSerializer2 = handlerInstantiator.serializerInstance(this._config, annotated, cls);
            }
            if (jsonSerializer2 == null) {
                jsonSerializer = (JsonSerializer) ClassUtil.createInstance(cls, this._config.canOverrideAccessModifiers());
            } else {
                jsonSerializer = jsonSerializer2;
            }
        }
        return _handleResolvable(jsonSerializer);
    }
}
