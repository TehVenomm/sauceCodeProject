package com.fasterxml.jackson.databind.deser.std;

import com.fasterxml.jackson.core.JsonParser;
import com.fasterxml.jackson.core.JsonToken;
import com.fasterxml.jackson.databind.BeanProperty;
import com.fasterxml.jackson.databind.DeserializationConfig;
import com.fasterxml.jackson.databind.DeserializationContext;
import com.fasterxml.jackson.databind.DeserializationFeature;
import com.fasterxml.jackson.databind.JsonDeserializer;
import com.fasterxml.jackson.databind.JsonMappingException;
import com.fasterxml.jackson.databind.MapperFeature;
import com.fasterxml.jackson.databind.annotation.JacksonStdImpl;
import com.fasterxml.jackson.databind.deser.ContextualDeserializer;
import com.fasterxml.jackson.databind.exc.InvalidFormatException;
import com.fasterxml.jackson.databind.introspect.AnnotatedMethod;
import com.fasterxml.jackson.databind.jsontype.TypeDeserializer;
import com.fasterxml.jackson.databind.util.ClassUtil;
import com.fasterxml.jackson.databind.util.CompactStringObjectMap;
import com.fasterxml.jackson.databind.util.EnumResolver;
import java.io.IOException;
import java.lang.reflect.Method;

@JacksonStdImpl
public class EnumDeserializer extends StdScalarDeserializer<Object> {
    private static final long serialVersionUID = 1;
    protected Object[] _enumsByIndex;
    protected final CompactStringObjectMap _lookupByName;
    protected CompactStringObjectMap _lookupByToString;

    protected static class FactoryBasedDeserializer extends StdDeserializer<Object> implements ContextualDeserializer {
        private static final long serialVersionUID = 1;
        protected final JsonDeserializer<?> _deser;
        protected final Method _factory;
        protected final Class<?> _inputType;

        public FactoryBasedDeserializer(Class<?> cls, AnnotatedMethod annotatedMethod, Class<?> cls2) {
            super(cls);
            this._factory = annotatedMethod.getAnnotated();
            this._inputType = cls2;
            this._deser = null;
        }

        protected FactoryBasedDeserializer(FactoryBasedDeserializer factoryBasedDeserializer, JsonDeserializer<?> jsonDeserializer) {
            super(factoryBasedDeserializer._valueClass);
            this._inputType = factoryBasedDeserializer._inputType;
            this._factory = factoryBasedDeserializer._factory;
            this._deser = jsonDeserializer;
        }

        public JsonDeserializer<?> createContextual(DeserializationContext deserializationContext, BeanProperty beanProperty) throws JsonMappingException {
            if (this._deser != null || this._inputType == String.class) {
                return this;
            }
            return new FactoryBasedDeserializer(this, deserializationContext.findContextualValueDeserializer(deserializationContext.constructType(this._inputType), beanProperty));
        }

        public Object deserialize(JsonParser jsonParser, DeserializationContext deserializationContext) throws IOException {
            Object text;
            if (this._deser != null) {
                text = this._deser.deserialize(jsonParser, deserializationContext);
            } else {
                JsonToken currentToken = jsonParser.getCurrentToken();
                if (currentToken == JsonToken.VALUE_STRING || currentToken == JsonToken.FIELD_NAME) {
                    text = jsonParser.getText();
                } else {
                    text = jsonParser.getValueAsString();
                }
            }
            try {
                return this._factory.invoke(this._valueClass, new Object[]{text});
            } catch (Exception e) {
                Throwable rootCause = ClassUtil.getRootCause(e);
                if (rootCause instanceof IOException) {
                    throw ((IOException) rootCause);
                }
                throw deserializationContext.instantiationException(this._valueClass, rootCause);
            }
        }

        public Object deserializeWithType(JsonParser jsonParser, DeserializationContext deserializationContext, TypeDeserializer typeDeserializer) throws IOException {
            if (this._deser == null) {
                return deserialize(jsonParser, deserializationContext);
            }
            return typeDeserializer.deserializeTypedFromAny(jsonParser, deserializationContext);
        }
    }

    public EnumDeserializer(EnumResolver enumResolver) {
        super(enumResolver.getEnumClass());
        this._lookupByName = enumResolver.constructLookup();
        this._enumsByIndex = enumResolver.getRawEnums();
    }

    public static JsonDeserializer<?> deserializerForCreator(DeserializationConfig deserializationConfig, Class<?> cls, AnnotatedMethod annotatedMethod) {
        Class rawParameterType = annotatedMethod.getRawParameterType(0);
        if (deserializationConfig.canOverrideAccessModifiers()) {
            ClassUtil.checkAndFixAccess(annotatedMethod.getMember(), deserializationConfig.isEnabled(MapperFeature.OVERRIDE_PUBLIC_ACCESS_MODIFIERS));
        }
        return new FactoryBasedDeserializer(cls, annotatedMethod, rawParameterType);
    }

    public boolean isCachable() {
        return true;
    }

    public Object deserialize(JsonParser jsonParser, DeserializationContext deserializationContext) throws IOException {
        JsonToken currentToken = jsonParser.getCurrentToken();
        if (currentToken == JsonToken.VALUE_STRING || currentToken == JsonToken.FIELD_NAME) {
            CompactStringObjectMap compactStringObjectMap = deserializationContext.isEnabled(DeserializationFeature.READ_ENUMS_USING_TO_STRING) ? _getToStringLookup() : this._lookupByName;
            String text = jsonParser.getText();
            Object find = compactStringObjectMap.find(text);
            if (find == null) {
                return _deserializeAltString(jsonParser, deserializationContext, compactStringObjectMap, text);
            }
            return find;
        } else if (currentToken != JsonToken.VALUE_NUMBER_INT) {
            return _deserializeOther(jsonParser, deserializationContext);
        } else {
            int intValue = jsonParser.getIntValue();
            if (deserializationContext.isEnabled(DeserializationFeature.FAIL_ON_NUMBERS_FOR_ENUMS)) {
                _failOnNumber(deserializationContext, jsonParser, intValue);
            }
            if (intValue >= 0 && intValue <= this._enumsByIndex.length) {
                return this._enumsByIndex[intValue];
            }
            if (deserializationContext.isEnabled(DeserializationFeature.READ_UNKNOWN_ENUM_VALUES_AS_NULL)) {
                return null;
            }
            throw deserializationContext.weirdNumberException(Integer.valueOf(intValue), _enumClass(), "index value outside legal index range [0.." + (this._enumsByIndex.length - 1) + "]");
        }
    }

    private final Object _deserializeAltString(JsonParser jsonParser, DeserializationContext deserializationContext, CompactStringObjectMap compactStringObjectMap, String str) throws IOException {
        String trim = str.trim();
        if (trim.length() != 0) {
            char charAt = trim.charAt(0);
            if (charAt >= '0' && charAt <= '9') {
                try {
                    int parseInt = Integer.parseInt(trim);
                    if (deserializationContext.isEnabled(DeserializationFeature.FAIL_ON_NUMBERS_FOR_ENUMS)) {
                        _failOnNumber(deserializationContext, jsonParser, parseInt);
                    }
                    if (parseInt >= 0 && parseInt <= this._enumsByIndex.length) {
                        return this._enumsByIndex[parseInt];
                    }
                } catch (NumberFormatException e) {
                }
            }
        } else if (deserializationContext.isEnabled(DeserializationFeature.ACCEPT_EMPTY_STRING_AS_NULL_OBJECT)) {
            return null;
        }
        if (deserializationContext.isEnabled(DeserializationFeature.READ_UNKNOWN_ENUM_VALUES_AS_NULL)) {
            return null;
        }
        throw deserializationContext.weirdStringException(trim, _enumClass(), "value not one of declared Enum instance names: " + compactStringObjectMap.keys());
    }

    /* access modifiers changed from: protected */
    public Object _deserializeOther(JsonParser jsonParser, DeserializationContext deserializationContext) throws IOException {
        jsonParser.getCurrentToken();
        if (!deserializationContext.isEnabled(DeserializationFeature.UNWRAP_SINGLE_VALUE_ARRAYS) || !jsonParser.isExpectedStartArrayToken()) {
            throw deserializationContext.mappingException(_enumClass());
        }
        jsonParser.nextToken();
        Object deserialize = deserialize(jsonParser, deserializationContext);
        if (jsonParser.nextToken() == JsonToken.END_ARRAY) {
            return deserialize;
        }
        throw deserializationContext.wrongTokenException(jsonParser, JsonToken.END_ARRAY, "Attempted to unwrap single value array for single '" + _enumClass().getName() + "' value but there was more than a single value in the array");
    }

    /* access modifiers changed from: protected */
    public void _failOnNumber(DeserializationContext deserializationContext, JsonParser jsonParser, int i) throws IOException {
        throw InvalidFormatException.from(jsonParser, String.format("Not allowed to deserialize Enum value out of JSON number (%d): disable DeserializationConfig.DeserializationFeature.FAIL_ON_NUMBERS_FOR_ENUMS to allow", new Object[]{Integer.valueOf(i)}), Integer.valueOf(i), _enumClass());
    }

    /* access modifiers changed from: protected */
    public Class<?> _enumClass() {
        return handledType();
    }

    /* access modifiers changed from: protected */
    public CompactStringObjectMap _getToStringLookup() {
        CompactStringObjectMap compactStringObjectMap = this._lookupByToString;
        if (compactStringObjectMap == null) {
            synchronized (this) {
                compactStringObjectMap = EnumResolver.constructUnsafeUsingToString(_enumClass()).constructLookup();
            }
            this._lookupByToString = compactStringObjectMap;
        }
        return compactStringObjectMap;
    }
}
