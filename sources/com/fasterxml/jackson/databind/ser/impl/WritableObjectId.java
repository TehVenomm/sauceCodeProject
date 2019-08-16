package com.fasterxml.jackson.databind.ser.impl;

import com.fasterxml.jackson.annotation.ObjectIdGenerator;
import com.fasterxml.jackson.core.JsonGenerator;
import com.fasterxml.jackson.core.SerializableString;
import com.fasterxml.jackson.databind.SerializerProvider;
import java.io.IOException;

public final class WritableObjectId {
    public final ObjectIdGenerator<?> generator;

    /* renamed from: id */
    public Object f428id;
    protected boolean idWritten = false;

    public WritableObjectId(ObjectIdGenerator<?> objectIdGenerator) {
        this.generator = objectIdGenerator;
    }

    public boolean writeAsId(JsonGenerator jsonGenerator, SerializerProvider serializerProvider, ObjectIdWriter objectIdWriter) throws IOException {
        if (this.f428id == null || (!this.idWritten && !objectIdWriter.alwaysAsId)) {
            return false;
        }
        if (jsonGenerator.canWriteObjectId()) {
            jsonGenerator.writeObjectRef(String.valueOf(this.f428id));
        } else {
            objectIdWriter.serializer.serialize(this.f428id, jsonGenerator, serializerProvider);
        }
        return true;
    }

    public Object generateId(Object obj) {
        Object generateId = this.generator.generateId(obj);
        this.f428id = generateId;
        return generateId;
    }

    public void writeAsField(JsonGenerator jsonGenerator, SerializerProvider serializerProvider, ObjectIdWriter objectIdWriter) throws IOException {
        this.idWritten = true;
        if (jsonGenerator.canWriteObjectId()) {
            jsonGenerator.writeObjectId(String.valueOf(this.f428id));
            return;
        }
        SerializableString serializableString = objectIdWriter.propertyName;
        if (serializableString != null) {
            jsonGenerator.writeFieldName(serializableString);
            objectIdWriter.serializer.serialize(this.f428id, jsonGenerator, serializerProvider);
        }
    }
}
