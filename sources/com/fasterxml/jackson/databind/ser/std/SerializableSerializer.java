package com.fasterxml.jackson.databind.ser.std;

import com.fasterxml.jackson.core.JsonGenerator;
import com.fasterxml.jackson.databind.JavaType;
import com.fasterxml.jackson.databind.JsonMappingException;
import com.fasterxml.jackson.databind.JsonSerializable;
import com.fasterxml.jackson.databind.JsonSerializable.Base;
import com.fasterxml.jackson.databind.ObjectMapper;
import com.fasterxml.jackson.databind.SerializerProvider;
import com.fasterxml.jackson.databind.annotation.JacksonStdImpl;
import com.fasterxml.jackson.databind.jsonFormatVisitors.JsonFormatVisitorWrapper;
import com.fasterxml.jackson.databind.jsontype.TypeSerializer;
import java.io.IOException;
import java.util.concurrent.atomic.AtomicReference;

@JacksonStdImpl
public class SerializableSerializer extends StdSerializer<JsonSerializable> {
    private static final AtomicReference<ObjectMapper> _mapperReference = new AtomicReference<>();
    public static final SerializableSerializer instance = new SerializableSerializer();

    protected SerializableSerializer() {
        super(JsonSerializable.class);
    }

    public boolean isEmpty(SerializerProvider serializerProvider, JsonSerializable jsonSerializable) {
        if (jsonSerializable instanceof Base) {
            return ((Base) jsonSerializable).isEmpty(serializerProvider);
        }
        return false;
    }

    public void serialize(JsonSerializable jsonSerializable, JsonGenerator jsonGenerator, SerializerProvider serializerProvider) throws IOException {
        jsonSerializable.serialize(jsonGenerator, serializerProvider);
    }

    public final void serializeWithType(JsonSerializable jsonSerializable, JsonGenerator jsonGenerator, SerializerProvider serializerProvider, TypeSerializer typeSerializer) throws IOException {
        jsonSerializable.serializeWithType(jsonGenerator, serializerProvider, typeSerializer);
    }

    /* JADX WARNING: Removed duplicated region for block: B:12:0x0049  */
    /* JADX WARNING: Removed duplicated region for block: B:16:0x0058  */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    public com.fasterxml.jackson.databind.JsonNode getSchema(com.fasterxml.jackson.databind.SerializerProvider r8, java.lang.reflect.Type r9) throws com.fasterxml.jackson.databind.JsonMappingException {
        /*
            r7 = this;
            r2 = 0
            com.fasterxml.jackson.databind.node.ObjectNode r4 = r7.createObjectNode()
            java.lang.String r3 = "any"
            if (r9 == 0) goto L_0x007a
            java.lang.Class r0 = com.fasterxml.jackson.databind.type.TypeFactory.rawClass(r9)
            java.lang.Class<com.fasterxml.jackson.databind.jsonschema.JsonSerializableSchema> r1 = com.fasterxml.jackson.databind.jsonschema.JsonSerializableSchema.class
            boolean r1 = r0.isAnnotationPresent(r1)
            if (r1 == 0) goto L_0x007a
            java.lang.Class<com.fasterxml.jackson.databind.jsonschema.JsonSerializableSchema> r1 = com.fasterxml.jackson.databind.jsonschema.JsonSerializableSchema.class
            java.lang.annotation.Annotation r0 = r0.getAnnotation(r1)
            com.fasterxml.jackson.databind.jsonschema.JsonSerializableSchema r0 = (com.fasterxml.jackson.databind.jsonschema.JsonSerializableSchema) r0
            java.lang.String r3 = r0.schemaType()
            java.lang.String r1 = "##irrelevant"
            java.lang.String r5 = r0.schemaObjectPropertiesDefinition()
            boolean r1 = r1.equals(r5)
            if (r1 != 0) goto L_0x0078
            java.lang.String r1 = r0.schemaObjectPropertiesDefinition()
        L_0x0031:
            java.lang.String r5 = "##irrelevant"
            java.lang.String r6 = r0.schemaItemDefinition()
            boolean r5 = r5.equals(r6)
            if (r5 != 0) goto L_0x0076
            java.lang.String r2 = r0.schemaItemDefinition()
            r0 = r2
        L_0x0042:
            java.lang.String r2 = "type"
            r4.put(r2, r3)
            if (r1 == 0) goto L_0x0056
            java.lang.String r2 = "properties"
            com.fasterxml.jackson.databind.ObjectMapper r3 = _getObjectMapper()     // Catch:{ IOException -> 0x0066 }
            com.fasterxml.jackson.databind.JsonNode r1 = r3.readTree(r1)     // Catch:{ IOException -> 0x0066 }
            r4.set(r2, r1)     // Catch:{ IOException -> 0x0066 }
        L_0x0056:
            if (r0 == 0) goto L_0x0065
            java.lang.String r1 = "items"
            com.fasterxml.jackson.databind.ObjectMapper r2 = _getObjectMapper()     // Catch:{ IOException -> 0x006e }
            com.fasterxml.jackson.databind.JsonNode r0 = r2.readTree(r0)     // Catch:{ IOException -> 0x006e }
            r4.set(r1, r0)     // Catch:{ IOException -> 0x006e }
        L_0x0065:
            return r4
        L_0x0066:
            r0 = move-exception
            java.lang.String r0 = "Failed to parse @JsonSerializableSchema.schemaObjectPropertiesDefinition value"
            com.fasterxml.jackson.databind.JsonMappingException r0 = com.fasterxml.jackson.databind.JsonMappingException.from(r8, r0)
            throw r0
        L_0x006e:
            r0 = move-exception
            java.lang.String r0 = "Failed to parse @JsonSerializableSchema.schemaItemDefinition value"
            com.fasterxml.jackson.databind.JsonMappingException r0 = com.fasterxml.jackson.databind.JsonMappingException.from(r8, r0)
            throw r0
        L_0x0076:
            r0 = r2
            goto L_0x0042
        L_0x0078:
            r1 = r2
            goto L_0x0031
        L_0x007a:
            r0 = r2
            r1 = r2
            goto L_0x0042
        */
        throw new UnsupportedOperationException("Method not decompiled: com.fasterxml.jackson.databind.ser.std.SerializableSerializer.getSchema(com.fasterxml.jackson.databind.SerializerProvider, java.lang.reflect.Type):com.fasterxml.jackson.databind.JsonNode");
    }

    private static final synchronized ObjectMapper _getObjectMapper() {
        ObjectMapper objectMapper;
        synchronized (SerializableSerializer.class) {
            objectMapper = (ObjectMapper) _mapperReference.get();
            if (objectMapper == null) {
                objectMapper = new ObjectMapper();
                _mapperReference.set(objectMapper);
            }
        }
        return objectMapper;
    }

    public void acceptJsonFormatVisitor(JsonFormatVisitorWrapper jsonFormatVisitorWrapper, JavaType javaType) throws JsonMappingException {
        jsonFormatVisitorWrapper.expectAnyFormat(javaType);
    }
}
