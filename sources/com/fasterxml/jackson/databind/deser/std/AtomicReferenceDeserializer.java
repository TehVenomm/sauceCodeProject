package com.fasterxml.jackson.databind.deser.std;

import com.fasterxml.jackson.core.JsonParser;
import com.fasterxml.jackson.core.JsonToken;
import com.fasterxml.jackson.databind.BeanProperty;
import com.fasterxml.jackson.databind.DeserializationContext;
import com.fasterxml.jackson.databind.JavaType;
import com.fasterxml.jackson.databind.JsonDeserializer;
import com.fasterxml.jackson.databind.JsonMappingException;
import com.fasterxml.jackson.databind.deser.ContextualDeserializer;
import com.fasterxml.jackson.databind.jsontype.TypeDeserializer;
import java.io.IOException;
import java.util.concurrent.atomic.AtomicReference;

public class AtomicReferenceDeserializer extends StdDeserializer<AtomicReference<?>> implements ContextualDeserializer {
    private static final long serialVersionUID = 1;
    protected final JavaType _referencedType;
    protected final JsonDeserializer<?> _valueDeserializer;
    protected final TypeDeserializer _valueTypeDeserializer;

    public AtomicReferenceDeserializer(JavaType javaType) {
        this(javaType, null, null);
    }

    public AtomicReferenceDeserializer(JavaType javaType, TypeDeserializer typeDeserializer, JsonDeserializer<?> jsonDeserializer) {
        super(AtomicReference.class);
        this._referencedType = javaType;
        this._valueDeserializer = jsonDeserializer;
        this._valueTypeDeserializer = typeDeserializer;
    }

    public AtomicReferenceDeserializer withResolved(TypeDeserializer typeDeserializer, JsonDeserializer<?> jsonDeserializer) {
        return (jsonDeserializer == this._valueDeserializer && typeDeserializer == this._valueTypeDeserializer) ? this : new AtomicReferenceDeserializer(this._referencedType, typeDeserializer, jsonDeserializer);
    }

    public AtomicReference<?> getNullValue(DeserializationContext deserializationContext) {
        return new AtomicReference<>();
    }

    @Deprecated
    public AtomicReference<?> getNullValue() {
        return new AtomicReference<>();
    }

    public JsonDeserializer<?> createContextual(DeserializationContext deserializationContext, BeanProperty beanProperty) throws JsonMappingException {
        JsonDeserializer handleSecondaryContextualization;
        JsonDeserializer<?> jsonDeserializer = this._valueDeserializer;
        TypeDeserializer typeDeserializer = this._valueTypeDeserializer;
        if (jsonDeserializer == null) {
            handleSecondaryContextualization = deserializationContext.findContextualValueDeserializer(this._referencedType, beanProperty);
        } else {
            handleSecondaryContextualization = deserializationContext.handleSecondaryContextualization(jsonDeserializer, beanProperty, this._referencedType);
        }
        if (typeDeserializer != null) {
            typeDeserializer = typeDeserializer.forProperty(beanProperty);
        }
        return withResolved(typeDeserializer, handleSecondaryContextualization);
    }

    public AtomicReference<?> deserialize(JsonParser jsonParser, DeserializationContext deserializationContext) throws IOException {
        return new AtomicReference<>(this._valueTypeDeserializer == null ? this._valueDeserializer.deserialize(jsonParser, deserializationContext) : this._valueDeserializer.deserializeWithType(jsonParser, deserializationContext, this._valueTypeDeserializer));
    }

    public Object deserializeWithType(JsonParser jsonParser, DeserializationContext deserializationContext, TypeDeserializer typeDeserializer) throws IOException {
        JsonToken currentToken = jsonParser.getCurrentToken();
        if (currentToken == JsonToken.VALUE_NULL) {
            return getNullValue(deserializationContext);
        }
        if (currentToken != null && currentToken.isScalarValue()) {
            return deserialize(jsonParser, deserializationContext);
        }
        if (this._valueTypeDeserializer == null) {
            return deserialize(jsonParser, deserializationContext);
        }
        return new AtomicReference(this._valueTypeDeserializer.deserializeTypedFromAny(jsonParser, deserializationContext));
    }
}
