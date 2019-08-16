package com.fasterxml.jackson.databind.deser.std;

import com.fasterxml.jackson.annotation.JsonFormat.Feature;
import com.fasterxml.jackson.core.JsonParser;
import com.fasterxml.jackson.core.JsonProcessingException;
import com.fasterxml.jackson.core.JsonToken;
import com.fasterxml.jackson.databind.BeanProperty;
import com.fasterxml.jackson.databind.DeserializationContext;
import com.fasterxml.jackson.databind.DeserializationFeature;
import com.fasterxml.jackson.databind.JavaType;
import com.fasterxml.jackson.databind.JsonDeserializer;
import com.fasterxml.jackson.databind.JsonMappingException;
import com.fasterxml.jackson.databind.deser.ContextualDeserializer;
import com.fasterxml.jackson.databind.jsontype.TypeDeserializer;
import java.io.IOException;
import java.util.EnumSet;

public class EnumSetDeserializer extends StdDeserializer<EnumSet<?>> implements ContextualDeserializer {
    private static final long serialVersionUID = 1;
    protected final Class<Enum> _enumClass;
    protected JsonDeserializer<Enum<?>> _enumDeserializer;
    protected final JavaType _enumType;
    protected final Boolean _unwrapSingle;

    public EnumSetDeserializer(JavaType javaType, JsonDeserializer<?> jsonDeserializer) {
        super(EnumSet.class);
        this._enumType = javaType;
        this._enumClass = javaType.getRawClass();
        if (!this._enumClass.isEnum()) {
            throw new IllegalArgumentException("Type " + javaType + " not Java Enum type");
        }
        this._enumDeserializer = jsonDeserializer;
        this._unwrapSingle = null;
    }

    protected EnumSetDeserializer(EnumSetDeserializer enumSetDeserializer, JsonDeserializer<?> jsonDeserializer, Boolean bool) {
        super(EnumSet.class);
        this._enumType = enumSetDeserializer._enumType;
        this._enumClass = enumSetDeserializer._enumClass;
        this._enumDeserializer = jsonDeserializer;
        this._unwrapSingle = bool;
    }

    public EnumSetDeserializer withDeserializer(JsonDeserializer<?> jsonDeserializer) {
        return this._enumDeserializer == jsonDeserializer ? this : new EnumSetDeserializer(this, jsonDeserializer, this._unwrapSingle);
    }

    public EnumSetDeserializer withResolved(JsonDeserializer<?> jsonDeserializer, Boolean bool) {
        return (this._unwrapSingle == bool && this._enumDeserializer == jsonDeserializer) ? this : new EnumSetDeserializer(this, jsonDeserializer, bool);
    }

    public boolean isCachable() {
        if (this._enumType.getValueHandler() != null) {
            return false;
        }
        return true;
    }

    public JsonDeserializer<?> createContextual(DeserializationContext deserializationContext, BeanProperty beanProperty) throws JsonMappingException {
        JsonDeserializer handleSecondaryContextualization;
        Boolean findFormatFeature = findFormatFeature(deserializationContext, beanProperty, EnumSet.class, Feature.ACCEPT_SINGLE_VALUE_AS_ARRAY);
        JsonDeserializer<Enum<?>> jsonDeserializer = this._enumDeserializer;
        if (jsonDeserializer == null) {
            handleSecondaryContextualization = deserializationContext.findContextualValueDeserializer(this._enumType, beanProperty);
        } else {
            handleSecondaryContextualization = deserializationContext.handleSecondaryContextualization(jsonDeserializer, beanProperty, this._enumType);
        }
        return withResolved(handleSecondaryContextualization, findFormatFeature);
    }

    public EnumSet<?> deserialize(JsonParser jsonParser, DeserializationContext deserializationContext) throws IOException {
        if (!jsonParser.isExpectedStartArrayToken()) {
            return handleNonArray(jsonParser, deserializationContext);
        }
        EnumSet constructSet = constructSet();
        while (true) {
            try {
                JsonToken nextToken = jsonParser.nextToken();
                if (nextToken == JsonToken.END_ARRAY) {
                    return constructSet;
                }
                if (nextToken == JsonToken.VALUE_NULL) {
                    throw deserializationContext.mappingException(this._enumClass);
                }
                Enum enumR = (Enum) this._enumDeserializer.deserialize(jsonParser, deserializationContext);
                if (enumR != null) {
                    constructSet.add(enumR);
                }
            } catch (Exception e) {
                throw JsonMappingException.wrapWithPath((Throwable) e, (Object) constructSet, constructSet.size());
            }
        }
    }

    public Object deserializeWithType(JsonParser jsonParser, DeserializationContext deserializationContext, TypeDeserializer typeDeserializer) throws IOException, JsonProcessingException {
        return typeDeserializer.deserializeTypedFromArray(jsonParser, deserializationContext);
    }

    private EnumSet constructSet() {
        return EnumSet.noneOf(this._enumClass);
    }

    /* access modifiers changed from: protected */
    public EnumSet<?> handleNonArray(JsonParser jsonParser, DeserializationContext deserializationContext) throws IOException {
        if (!(this._unwrapSingle == Boolean.TRUE || (this._unwrapSingle == null && deserializationContext.isEnabled(DeserializationFeature.ACCEPT_SINGLE_VALUE_AS_ARRAY)))) {
            throw deserializationContext.mappingException(EnumSet.class);
        }
        EnumSet<?> constructSet = constructSet();
        if (jsonParser.hasToken(JsonToken.VALUE_NULL)) {
            throw deserializationContext.mappingException(this._enumClass);
        }
        try {
            Enum enumR = (Enum) this._enumDeserializer.deserialize(jsonParser, deserializationContext);
            if (enumR != null) {
                constructSet.add(enumR);
            }
            return constructSet;
        } catch (Exception e) {
            throw JsonMappingException.wrapWithPath((Throwable) e, (Object) constructSet, constructSet.size());
        }
    }
}
