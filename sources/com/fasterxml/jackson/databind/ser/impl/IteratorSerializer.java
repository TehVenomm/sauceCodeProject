package com.fasterxml.jackson.databind.ser.impl;

import com.fasterxml.jackson.core.JsonGenerator;
import com.fasterxml.jackson.databind.BeanProperty;
import com.fasterxml.jackson.databind.JavaType;
import com.fasterxml.jackson.databind.JsonSerializer;
import com.fasterxml.jackson.databind.SerializationFeature;
import com.fasterxml.jackson.databind.SerializerProvider;
import com.fasterxml.jackson.databind.annotation.JacksonStdImpl;
import com.fasterxml.jackson.databind.jsontype.TypeSerializer;
import com.fasterxml.jackson.databind.ser.ContainerSerializer;
import com.fasterxml.jackson.databind.ser.std.AsArraySerializerBase;
import java.io.IOException;
import java.util.Iterator;

@JacksonStdImpl
public class IteratorSerializer extends AsArraySerializerBase<Iterator<?>> {
    public IteratorSerializer(JavaType javaType, boolean z, TypeSerializer typeSerializer) {
        super(Iterator.class, javaType, z, typeSerializer, null);
    }

    public IteratorSerializer(IteratorSerializer iteratorSerializer, BeanProperty beanProperty, TypeSerializer typeSerializer, JsonSerializer<?> jsonSerializer, Boolean bool) {
        super((AsArraySerializerBase<?>) iteratorSerializer, beanProperty, typeSerializer, jsonSerializer, bool);
    }

    public boolean isEmpty(SerializerProvider serializerProvider, Iterator<?> it) {
        return it == null || !it.hasNext();
    }

    public boolean hasSingleElement(Iterator<?> it) {
        return false;
    }

    public ContainerSerializer<?> _withValueTypeSerializer(TypeSerializer typeSerializer) {
        return new IteratorSerializer(this, this._property, typeSerializer, this._elementSerializer, this._unwrapSingle);
    }

    public IteratorSerializer withResolved(BeanProperty beanProperty, TypeSerializer typeSerializer, JsonSerializer<?> jsonSerializer, Boolean bool) {
        return new IteratorSerializer(this, beanProperty, typeSerializer, jsonSerializer, bool);
    }

    public final void serialize(Iterator<?> it, JsonGenerator jsonGenerator, SerializerProvider serializerProvider) throws IOException {
        if (((this._unwrapSingle != null || !serializerProvider.isEnabled(SerializationFeature.WRITE_SINGLE_ELEM_ARRAYS_UNWRAPPED)) && this._unwrapSingle != Boolean.TRUE) || !hasSingleElement(it)) {
            jsonGenerator.writeStartArray();
            serializeContents(it, jsonGenerator, serializerProvider);
            jsonGenerator.writeEndArray();
            return;
        }
        serializeContents(it, jsonGenerator, serializerProvider);
    }

    public void serializeContents(Iterator<?> it, JsonGenerator jsonGenerator, SerializerProvider serializerProvider) throws IOException {
        JsonSerializer jsonSerializer;
        if (it.hasNext()) {
            TypeSerializer typeSerializer = this._valueTypeSerializer;
            Class cls = null;
            JsonSerializer jsonSerializer2 = null;
            do {
                Object next = it.next();
                if (next == null) {
                    serializerProvider.defaultSerializeNull(jsonGenerator);
                } else {
                    JsonSerializer jsonSerializer3 = this._elementSerializer;
                    if (jsonSerializer3 == null) {
                        Class cls2 = next.getClass();
                        if (cls2 == cls) {
                            jsonSerializer3 = jsonSerializer2;
                            jsonSerializer = jsonSerializer2;
                        } else {
                            JsonSerializer findValueSerializer = serializerProvider.findValueSerializer(cls2, this._property);
                            jsonSerializer3 = findValueSerializer;
                            cls = cls2;
                            jsonSerializer = findValueSerializer;
                        }
                    } else {
                        jsonSerializer = jsonSerializer2;
                    }
                    if (typeSerializer == null) {
                        jsonSerializer3.serialize(next, jsonGenerator, serializerProvider);
                        jsonSerializer2 = jsonSerializer;
                    } else {
                        jsonSerializer3.serializeWithType(next, jsonGenerator, serializerProvider, typeSerializer);
                        jsonSerializer2 = jsonSerializer;
                    }
                }
            } while (it.hasNext());
        }
    }
}
