package com.fasterxml.jackson.databind.deser.std;

import com.facebook.appevents.AppEventsConstants;
import com.facebook.internal.ServerProtocol;
import com.fasterxml.jackson.annotation.JsonFormat.Feature;
import com.fasterxml.jackson.annotation.JsonFormat.Value;
import com.fasterxml.jackson.core.JsonParser;
import com.fasterxml.jackson.core.JsonParser.NumberType;
import com.fasterxml.jackson.core.JsonToken;
import com.fasterxml.jackson.core.p015io.NumberInput;
import com.fasterxml.jackson.databind.AnnotationIntrospector;
import com.fasterxml.jackson.databind.BeanProperty;
import com.fasterxml.jackson.databind.DeserializationContext;
import com.fasterxml.jackson.databind.DeserializationFeature;
import com.fasterxml.jackson.databind.JavaType;
import com.fasterxml.jackson.databind.JsonDeserializer;
import com.fasterxml.jackson.databind.JsonMappingException;
import com.fasterxml.jackson.databind.KeyDeserializer;
import com.fasterxml.jackson.databind.introspect.AnnotatedMember;
import com.fasterxml.jackson.databind.jsontype.TypeDeserializer;
import com.fasterxml.jackson.databind.util.ClassUtil;
import com.fasterxml.jackson.databind.util.Converter;
import java.io.IOException;
import java.io.Serializable;
import java.util.Date;
import p017io.fabric.sdk.android.services.common.IdManager;

public abstract class StdDeserializer<T> extends JsonDeserializer<T> implements Serializable {
    protected static final int F_MASK_INT_COERCIONS = (DeserializationFeature.USE_BIG_INTEGER_FOR_INTS.getMask() | DeserializationFeature.USE_LONG_FOR_INTS.getMask());
    private static final long serialVersionUID = 1;
    protected final Class<?> _valueClass;

    protected StdDeserializer(Class<?> cls) {
        this._valueClass = cls;
    }

    protected StdDeserializer(JavaType javaType) {
        this._valueClass = javaType == null ? null : javaType.getRawClass();
    }

    protected StdDeserializer(StdDeserializer<?> stdDeserializer) {
        this._valueClass = stdDeserializer._valueClass;
    }

    public Class<?> handledType() {
        return this._valueClass;
    }

    @Deprecated
    public final Class<?> getValueClass() {
        return this._valueClass;
    }

    public JavaType getValueType() {
        return null;
    }

    /* access modifiers changed from: protected */
    public boolean isDefaultDeserializer(JsonDeserializer<?> jsonDeserializer) {
        return ClassUtil.isJacksonStdImpl((Object) jsonDeserializer);
    }

    /* access modifiers changed from: protected */
    public boolean isDefaultKeyDeserializer(KeyDeserializer keyDeserializer) {
        return ClassUtil.isJacksonStdImpl((Object) keyDeserializer);
    }

    public Object deserializeWithType(JsonParser jsonParser, DeserializationContext deserializationContext, TypeDeserializer typeDeserializer) throws IOException {
        return typeDeserializer.deserializeTypedFromAny(jsonParser, deserializationContext);
    }

    /* access modifiers changed from: protected */
    public final boolean _parseBooleanPrimitive(JsonParser jsonParser, DeserializationContext deserializationContext) throws IOException {
        boolean z = true;
        JsonToken currentToken = jsonParser.getCurrentToken();
        if (currentToken == JsonToken.VALUE_TRUE) {
            return true;
        }
        if (currentToken == JsonToken.VALUE_FALSE || currentToken == JsonToken.VALUE_NULL) {
            return false;
        }
        if (currentToken == JsonToken.VALUE_NUMBER_INT) {
            if (jsonParser.getNumberType() != NumberType.INT) {
                return _parseBooleanFromOther(jsonParser, deserializationContext);
            }
            if (jsonParser.getIntValue() == 0) {
                z = false;
            }
            return z;
        } else if (currentToken == JsonToken.VALUE_STRING) {
            String trim = jsonParser.getText().trim();
            if (ServerProtocol.DIALOG_RETURN_SCOPES_TRUE.equals(trim) || "True".equals(trim)) {
                return true;
            }
            if ("false".equals(trim) || "False".equals(trim) || trim.length() == 0 || _hasTextualNull(trim)) {
                return false;
            }
            throw deserializationContext.weirdStringException(trim, this._valueClass, "only \"true\" or \"false\" recognized");
        } else if (currentToken != JsonToken.START_ARRAY || !deserializationContext.isEnabled(DeserializationFeature.UNWRAP_SINGLE_VALUE_ARRAYS)) {
            throw deserializationContext.mappingException(this._valueClass, currentToken);
        } else {
            jsonParser.nextToken();
            boolean _parseBooleanPrimitive = _parseBooleanPrimitive(jsonParser, deserializationContext);
            if (jsonParser.nextToken() == JsonToken.END_ARRAY) {
                return _parseBooleanPrimitive;
            }
            throw deserializationContext.wrongTokenException(jsonParser, JsonToken.END_ARRAY, "Attempted to unwrap single value array for single 'boolean' value but there was more than a single value in the array");
        }
    }

    /* access modifiers changed from: protected */
    public final Boolean _parseBoolean(JsonParser jsonParser, DeserializationContext deserializationContext) throws IOException {
        JsonToken currentToken = jsonParser.getCurrentToken();
        if (currentToken == JsonToken.VALUE_TRUE) {
            return Boolean.TRUE;
        }
        if (currentToken == JsonToken.VALUE_FALSE) {
            return Boolean.FALSE;
        }
        if (currentToken == JsonToken.VALUE_NUMBER_INT) {
            if (jsonParser.getNumberType() == NumberType.INT) {
                return jsonParser.getIntValue() == 0 ? Boolean.FALSE : Boolean.TRUE;
            }
            return Boolean.valueOf(_parseBooleanFromOther(jsonParser, deserializationContext));
        } else if (currentToken == JsonToken.VALUE_NULL) {
            return (Boolean) getNullValue(deserializationContext);
        } else {
            if (currentToken == JsonToken.VALUE_STRING) {
                String trim = jsonParser.getText().trim();
                if (ServerProtocol.DIALOG_RETURN_SCOPES_TRUE.equals(trim) || "True".equals(trim)) {
                    return Boolean.TRUE;
                }
                if ("false".equals(trim) || "False".equals(trim)) {
                    return Boolean.FALSE;
                }
                if (trim.length() == 0) {
                    return (Boolean) getEmptyValue(deserializationContext);
                }
                if (_hasTextualNull(trim)) {
                    return (Boolean) getNullValue(deserializationContext);
                }
                throw deserializationContext.weirdStringException(trim, this._valueClass, "only \"true\" or \"false\" recognized");
            } else if (currentToken != JsonToken.START_ARRAY || !deserializationContext.isEnabled(DeserializationFeature.UNWRAP_SINGLE_VALUE_ARRAYS)) {
                throw deserializationContext.mappingException(this._valueClass, currentToken);
            } else {
                jsonParser.nextToken();
                Boolean _parseBoolean = _parseBoolean(jsonParser, deserializationContext);
                if (jsonParser.nextToken() == JsonToken.END_ARRAY) {
                    return _parseBoolean;
                }
                throw deserializationContext.wrongTokenException(jsonParser, JsonToken.END_ARRAY, "Attempted to unwrap single value array for single 'Boolean' value but there was more than a single value in the array");
            }
        }
    }

    /* access modifiers changed from: protected */
    public final boolean _parseBooleanFromOther(JsonParser jsonParser, DeserializationContext deserializationContext) throws IOException {
        if (jsonParser.getNumberType() == NumberType.LONG) {
            return (jsonParser.getLongValue() == 0 ? Boolean.FALSE : Boolean.TRUE).booleanValue();
        }
        String text = jsonParser.getText();
        if (IdManager.DEFAULT_VERSION_NAME.equals(text) || AppEventsConstants.EVENT_PARAM_VALUE_NO.equals(text)) {
            return Boolean.FALSE.booleanValue();
        }
        return Boolean.TRUE.booleanValue();
    }

    /* access modifiers changed from: protected */
    public Byte _parseByte(JsonParser jsonParser, DeserializationContext deserializationContext) throws IOException {
        JsonToken currentToken = jsonParser.getCurrentToken();
        if (currentToken == JsonToken.VALUE_NUMBER_INT) {
            return Byte.valueOf(jsonParser.getByteValue());
        }
        if (currentToken == JsonToken.VALUE_STRING) {
            String trim = jsonParser.getText().trim();
            if (_hasTextualNull(trim)) {
                return (Byte) getNullValue(deserializationContext);
            }
            try {
                if (trim.length() == 0) {
                    return (Byte) getEmptyValue(deserializationContext);
                }
                int parseInt = NumberInput.parseInt(trim);
                if (parseInt >= -128 && parseInt <= 255) {
                    return Byte.valueOf((byte) parseInt);
                }
                throw deserializationContext.weirdStringException(trim, this._valueClass, "overflow, value can not be represented as 8-bit value");
            } catch (IllegalArgumentException e) {
                throw deserializationContext.weirdStringException(trim, this._valueClass, "not a valid Byte value");
            }
        } else if (currentToken == JsonToken.VALUE_NUMBER_FLOAT) {
            if (!deserializationContext.isEnabled(DeserializationFeature.ACCEPT_FLOAT_AS_INT)) {
                _failDoubleToIntCoercion(jsonParser, deserializationContext, "Byte");
            }
            return Byte.valueOf(jsonParser.getByteValue());
        } else if (currentToken == JsonToken.VALUE_NULL) {
            return (Byte) getNullValue(deserializationContext);
        } else {
            if (currentToken != JsonToken.START_ARRAY || !deserializationContext.isEnabled(DeserializationFeature.UNWRAP_SINGLE_VALUE_ARRAYS)) {
                throw deserializationContext.mappingException(this._valueClass, currentToken);
            }
            jsonParser.nextToken();
            Byte _parseByte = _parseByte(jsonParser, deserializationContext);
            if (jsonParser.nextToken() == JsonToken.END_ARRAY) {
                return _parseByte;
            }
            throw deserializationContext.wrongTokenException(jsonParser, JsonToken.END_ARRAY, "Attempted to unwrap single value array for single 'Byte' value but there was more than a single value in the array");
        }
    }

    /* access modifiers changed from: protected */
    public Short _parseShort(JsonParser jsonParser, DeserializationContext deserializationContext) throws IOException {
        JsonToken currentToken = jsonParser.getCurrentToken();
        if (currentToken == JsonToken.VALUE_NUMBER_INT) {
            return Short.valueOf(jsonParser.getShortValue());
        }
        if (currentToken == JsonToken.VALUE_STRING) {
            String trim = jsonParser.getText().trim();
            try {
                if (trim.length() == 0) {
                    return (Short) getEmptyValue(deserializationContext);
                }
                if (_hasTextualNull(trim)) {
                    return (Short) getNullValue(deserializationContext);
                }
                int parseInt = NumberInput.parseInt(trim);
                if (parseInt >= -32768 && parseInt <= 32767) {
                    return Short.valueOf((short) parseInt);
                }
                throw deserializationContext.weirdStringException(trim, this._valueClass, "overflow, value can not be represented as 16-bit value");
            } catch (IllegalArgumentException e) {
                throw deserializationContext.weirdStringException(trim, this._valueClass, "not a valid Short value");
            }
        } else if (currentToken == JsonToken.VALUE_NUMBER_FLOAT) {
            if (!deserializationContext.isEnabled(DeserializationFeature.ACCEPT_FLOAT_AS_INT)) {
                _failDoubleToIntCoercion(jsonParser, deserializationContext, "Short");
            }
            return Short.valueOf(jsonParser.getShortValue());
        } else if (currentToken == JsonToken.VALUE_NULL) {
            return (Short) getNullValue(deserializationContext);
        } else {
            if (currentToken != JsonToken.START_ARRAY || !deserializationContext.isEnabled(DeserializationFeature.UNWRAP_SINGLE_VALUE_ARRAYS)) {
                throw deserializationContext.mappingException(this._valueClass, currentToken);
            }
            jsonParser.nextToken();
            Short _parseShort = _parseShort(jsonParser, deserializationContext);
            if (jsonParser.nextToken() == JsonToken.END_ARRAY) {
                return _parseShort;
            }
            throw deserializationContext.wrongTokenException(jsonParser, JsonToken.END_ARRAY, "Attempted to unwrap single value array for single 'Short' value but there was more than a single value in the array");
        }
    }

    /* access modifiers changed from: protected */
    public final short _parseShortPrimitive(JsonParser jsonParser, DeserializationContext deserializationContext) throws IOException {
        int _parseIntPrimitive = _parseIntPrimitive(jsonParser, deserializationContext);
        if (_parseIntPrimitive >= -32768 && _parseIntPrimitive <= 32767) {
            return (short) _parseIntPrimitive;
        }
        throw deserializationContext.weirdStringException(String.valueOf(_parseIntPrimitive), this._valueClass, "overflow, value can not be represented as 16-bit value");
    }

    /* access modifiers changed from: protected */
    public final int _parseIntPrimitive(JsonParser jsonParser, DeserializationContext deserializationContext) throws IOException {
        if (jsonParser.hasToken(JsonToken.VALUE_NUMBER_INT)) {
            return jsonParser.getIntValue();
        }
        JsonToken currentToken = jsonParser.getCurrentToken();
        if (currentToken == JsonToken.VALUE_STRING) {
            String trim = jsonParser.getText().trim();
            if (_hasTextualNull(trim)) {
                return 0;
            }
            try {
                int length = trim.length();
                if (length > 9) {
                    long parseLong = Long.parseLong(trim);
                    if (parseLong >= -2147483648L && parseLong <= 2147483647L) {
                        return (int) parseLong;
                    }
                    throw deserializationContext.weirdStringException(trim, this._valueClass, "Overflow: numeric value (" + trim + ") out of range of int (" + Integer.MIN_VALUE + " - " + Integer.MAX_VALUE + ")");
                } else if (length != 0) {
                    return NumberInput.parseInt(trim);
                } else {
                    return 0;
                }
            } catch (IllegalArgumentException e) {
                throw deserializationContext.weirdStringException(trim, this._valueClass, "not a valid int value");
            }
        } else if (currentToken == JsonToken.VALUE_NUMBER_FLOAT) {
            if (!deserializationContext.isEnabled(DeserializationFeature.ACCEPT_FLOAT_AS_INT)) {
                _failDoubleToIntCoercion(jsonParser, deserializationContext, "int");
            }
            return jsonParser.getValueAsInt();
        } else if (currentToken == JsonToken.VALUE_NULL) {
            return 0;
        } else {
            if (currentToken != JsonToken.START_ARRAY || !deserializationContext.isEnabled(DeserializationFeature.UNWRAP_SINGLE_VALUE_ARRAYS)) {
                throw deserializationContext.mappingException(this._valueClass, currentToken);
            }
            jsonParser.nextToken();
            int _parseIntPrimitive = _parseIntPrimitive(jsonParser, deserializationContext);
            if (jsonParser.nextToken() == JsonToken.END_ARRAY) {
                return _parseIntPrimitive;
            }
            throw deserializationContext.wrongTokenException(jsonParser, JsonToken.END_ARRAY, "Attempted to unwrap single value array for single 'int' value but there was more than a single value in the array");
        }
    }

    /* access modifiers changed from: protected */
    public final Integer _parseInteger(JsonParser jsonParser, DeserializationContext deserializationContext) throws IOException {
        switch (jsonParser.getCurrentTokenId()) {
            case 3:
                if (deserializationContext.isEnabled(DeserializationFeature.UNWRAP_SINGLE_VALUE_ARRAYS)) {
                    jsonParser.nextToken();
                    Integer _parseInteger = _parseInteger(jsonParser, deserializationContext);
                    if (jsonParser.nextToken() == JsonToken.END_ARRAY) {
                        return _parseInteger;
                    }
                    throw deserializationContext.wrongTokenException(jsonParser, JsonToken.END_ARRAY, "Attempted to unwrap single value array for single 'Integer' value but there was more than a single value in the array");
                }
                break;
            case 6:
                String trim = jsonParser.getText().trim();
                try {
                    int length = trim.length();
                    if (_hasTextualNull(trim)) {
                        return (Integer) getNullValue(deserializationContext);
                    }
                    if (length > 9) {
                        long parseLong = Long.parseLong(trim);
                        if (parseLong >= -2147483648L && parseLong <= 2147483647L) {
                            return Integer.valueOf((int) parseLong);
                        }
                        throw deserializationContext.weirdStringException(trim, this._valueClass, "Overflow: numeric value (" + trim + ") out of range of Integer (" + Integer.MIN_VALUE + " - " + Integer.MAX_VALUE + ")");
                    } else if (length == 0) {
                        return (Integer) getEmptyValue(deserializationContext);
                    } else {
                        return Integer.valueOf(NumberInput.parseInt(trim));
                    }
                } catch (IllegalArgumentException e) {
                    throw deserializationContext.weirdStringException(trim, this._valueClass, "not a valid Integer value");
                }
            case 7:
                return Integer.valueOf(jsonParser.getIntValue());
            case 8:
                if (!deserializationContext.isEnabled(DeserializationFeature.ACCEPT_FLOAT_AS_INT)) {
                    _failDoubleToIntCoercion(jsonParser, deserializationContext, "Integer");
                }
                return Integer.valueOf(jsonParser.getValueAsInt());
            case 11:
                return (Integer) getNullValue(deserializationContext);
        }
        throw deserializationContext.mappingException(this._valueClass, jsonParser.getCurrentToken());
    }

    /* access modifiers changed from: protected */
    public final Long _parseLong(JsonParser jsonParser, DeserializationContext deserializationContext) throws IOException {
        switch (jsonParser.getCurrentTokenId()) {
            case 3:
                if (deserializationContext.isEnabled(DeserializationFeature.UNWRAP_SINGLE_VALUE_ARRAYS)) {
                    jsonParser.nextToken();
                    Long _parseLong = _parseLong(jsonParser, deserializationContext);
                    if (jsonParser.nextToken() == JsonToken.END_ARRAY) {
                        return _parseLong;
                    }
                    throw deserializationContext.wrongTokenException(jsonParser, JsonToken.END_ARRAY, "Attempted to unwrap single value array for single 'Long' value but there was more than a single value in the array");
                }
                break;
            case 6:
                String trim = jsonParser.getText().trim();
                if (trim.length() == 0) {
                    return (Long) getEmptyValue(deserializationContext);
                }
                if (_hasTextualNull(trim)) {
                    return (Long) getNullValue(deserializationContext);
                }
                try {
                    return Long.valueOf(NumberInput.parseLong(trim));
                } catch (IllegalArgumentException e) {
                    throw deserializationContext.weirdStringException(trim, this._valueClass, "not a valid Long value");
                }
            case 7:
                return Long.valueOf(jsonParser.getLongValue());
            case 8:
                if (!deserializationContext.isEnabled(DeserializationFeature.ACCEPT_FLOAT_AS_INT)) {
                    _failDoubleToIntCoercion(jsonParser, deserializationContext, "Long");
                }
                return Long.valueOf(jsonParser.getValueAsLong());
            case 11:
                return (Long) getNullValue(deserializationContext);
        }
        throw deserializationContext.mappingException(this._valueClass, jsonParser.getCurrentToken());
    }

    /* access modifiers changed from: protected */
    public final long _parseLongPrimitive(JsonParser jsonParser, DeserializationContext deserializationContext) throws IOException {
        switch (jsonParser.getCurrentTokenId()) {
            case 3:
                if (deserializationContext.isEnabled(DeserializationFeature.UNWRAP_SINGLE_VALUE_ARRAYS)) {
                    jsonParser.nextToken();
                    long _parseLongPrimitive = _parseLongPrimitive(jsonParser, deserializationContext);
                    if (jsonParser.nextToken() == JsonToken.END_ARRAY) {
                        return _parseLongPrimitive;
                    }
                    throw deserializationContext.wrongTokenException(jsonParser, JsonToken.END_ARRAY, "Attempted to unwrap single value array for single 'long' value but there was more than a single value in the array");
                }
                break;
            case 6:
                String trim = jsonParser.getText().trim();
                if (trim.length() == 0 || _hasTextualNull(trim)) {
                    return 0;
                }
                try {
                    return NumberInput.parseLong(trim);
                } catch (IllegalArgumentException e) {
                    throw deserializationContext.weirdStringException(trim, this._valueClass, "not a valid long value");
                }
            case 7:
                return jsonParser.getLongValue();
            case 8:
                if (!deserializationContext.isEnabled(DeserializationFeature.ACCEPT_FLOAT_AS_INT)) {
                    _failDoubleToIntCoercion(jsonParser, deserializationContext, "long");
                }
                return jsonParser.getValueAsLong();
            case 11:
                return 0;
        }
        throw deserializationContext.mappingException(this._valueClass, jsonParser.getCurrentToken());
    }

    /* access modifiers changed from: protected */
    public final Float _parseFloat(JsonParser jsonParser, DeserializationContext deserializationContext) throws IOException {
        JsonToken currentToken = jsonParser.getCurrentToken();
        if (currentToken == JsonToken.VALUE_NUMBER_INT || currentToken == JsonToken.VALUE_NUMBER_FLOAT) {
            return Float.valueOf(jsonParser.getFloatValue());
        }
        if (currentToken == JsonToken.VALUE_STRING) {
            String trim = jsonParser.getText().trim();
            if (trim.length() == 0) {
                return (Float) getEmptyValue(deserializationContext);
            }
            if (_hasTextualNull(trim)) {
                return (Float) getNullValue(deserializationContext);
            }
            switch (trim.charAt(0)) {
                case '-':
                    if (_isNegInf(trim)) {
                        return Float.valueOf(Float.NEGATIVE_INFINITY);
                    }
                    break;
                case 'I':
                    if (_isPosInf(trim)) {
                        return Float.valueOf(Float.POSITIVE_INFINITY);
                    }
                    break;
                case 'N':
                    if (_isNaN(trim)) {
                        return Float.valueOf(Float.NaN);
                    }
                    break;
            }
            try {
                return Float.valueOf(Float.parseFloat(trim));
            } catch (IllegalArgumentException e) {
                throw deserializationContext.weirdStringException(trim, this._valueClass, "not a valid Float value");
            }
        } else if (currentToken == JsonToken.VALUE_NULL) {
            return (Float) getNullValue(deserializationContext);
        } else {
            if (currentToken != JsonToken.START_ARRAY || !deserializationContext.isEnabled(DeserializationFeature.UNWRAP_SINGLE_VALUE_ARRAYS)) {
                throw deserializationContext.mappingException(this._valueClass, currentToken);
            }
            jsonParser.nextToken();
            Float _parseFloat = _parseFloat(jsonParser, deserializationContext);
            if (jsonParser.nextToken() == JsonToken.END_ARRAY) {
                return _parseFloat;
            }
            throw deserializationContext.wrongTokenException(jsonParser, JsonToken.END_ARRAY, "Attempted to unwrap single value array for single 'Byte' value but there was more than a single value in the array");
        }
    }

    /* access modifiers changed from: protected */
    public final float _parseFloatPrimitive(JsonParser jsonParser, DeserializationContext deserializationContext) throws IOException {
        JsonToken currentToken = jsonParser.getCurrentToken();
        if (currentToken == JsonToken.VALUE_NUMBER_INT || currentToken == JsonToken.VALUE_NUMBER_FLOAT) {
            return jsonParser.getFloatValue();
        }
        if (currentToken == JsonToken.VALUE_STRING) {
            String trim = jsonParser.getText().trim();
            if (trim.length() == 0 || _hasTextualNull(trim)) {
                return 0.0f;
            }
            switch (trim.charAt(0)) {
                case '-':
                    if (_isNegInf(trim)) {
                        return Float.NEGATIVE_INFINITY;
                    }
                    break;
                case 'I':
                    if (_isPosInf(trim)) {
                        return Float.POSITIVE_INFINITY;
                    }
                    break;
                case 'N':
                    if (_isNaN(trim)) {
                        return Float.NaN;
                    }
                    break;
            }
            try {
                return Float.parseFloat(trim);
            } catch (IllegalArgumentException e) {
                throw deserializationContext.weirdStringException(trim, this._valueClass, "not a valid float value");
            }
        } else if (currentToken == JsonToken.VALUE_NULL) {
            return 0.0f;
        } else {
            if (currentToken != JsonToken.START_ARRAY || !deserializationContext.isEnabled(DeserializationFeature.UNWRAP_SINGLE_VALUE_ARRAYS)) {
                throw deserializationContext.mappingException(this._valueClass, currentToken);
            }
            jsonParser.nextToken();
            float _parseFloatPrimitive = _parseFloatPrimitive(jsonParser, deserializationContext);
            if (jsonParser.nextToken() == JsonToken.END_ARRAY) {
                return _parseFloatPrimitive;
            }
            throw deserializationContext.wrongTokenException(jsonParser, JsonToken.END_ARRAY, "Attempted to unwrap single value array for single 'float' value but there was more than a single value in the array");
        }
    }

    /* access modifiers changed from: protected */
    public final Double _parseDouble(JsonParser jsonParser, DeserializationContext deserializationContext) throws IOException {
        JsonToken currentToken = jsonParser.getCurrentToken();
        if (currentToken == JsonToken.VALUE_NUMBER_INT || currentToken == JsonToken.VALUE_NUMBER_FLOAT) {
            return Double.valueOf(jsonParser.getDoubleValue());
        }
        if (currentToken == JsonToken.VALUE_STRING) {
            String trim = jsonParser.getText().trim();
            if (trim.length() == 0) {
                return (Double) getEmptyValue(deserializationContext);
            }
            if (_hasTextualNull(trim)) {
                return (Double) getNullValue(deserializationContext);
            }
            switch (trim.charAt(0)) {
                case '-':
                    if (_isNegInf(trim)) {
                        return Double.valueOf(Double.NEGATIVE_INFINITY);
                    }
                    break;
                case 'I':
                    if (_isPosInf(trim)) {
                        return Double.valueOf(Double.POSITIVE_INFINITY);
                    }
                    break;
                case 'N':
                    if (_isNaN(trim)) {
                        return Double.valueOf(Double.NaN);
                    }
                    break;
            }
            try {
                return Double.valueOf(parseDouble(trim));
            } catch (IllegalArgumentException e) {
                throw deserializationContext.weirdStringException(trim, this._valueClass, "not a valid Double value");
            }
        } else if (currentToken == JsonToken.VALUE_NULL) {
            return (Double) getNullValue(deserializationContext);
        } else {
            if (currentToken != JsonToken.START_ARRAY || !deserializationContext.isEnabled(DeserializationFeature.UNWRAP_SINGLE_VALUE_ARRAYS)) {
                throw deserializationContext.mappingException(this._valueClass, currentToken);
            }
            jsonParser.nextToken();
            Double _parseDouble = _parseDouble(jsonParser, deserializationContext);
            if (jsonParser.nextToken() == JsonToken.END_ARRAY) {
                return _parseDouble;
            }
            throw deserializationContext.wrongTokenException(jsonParser, JsonToken.END_ARRAY, "Attempted to unwrap single value array for single 'Double' value but there was more than a single value in the array");
        }
    }

    /* access modifiers changed from: protected */
    public final double _parseDoublePrimitive(JsonParser jsonParser, DeserializationContext deserializationContext) throws IOException {
        JsonToken currentToken = jsonParser.getCurrentToken();
        if (currentToken == JsonToken.VALUE_NUMBER_INT || currentToken == JsonToken.VALUE_NUMBER_FLOAT) {
            return jsonParser.getDoubleValue();
        }
        if (currentToken == JsonToken.VALUE_STRING) {
            String trim = jsonParser.getText().trim();
            if (trim.length() == 0 || _hasTextualNull(trim)) {
                return 0.0d;
            }
            switch (trim.charAt(0)) {
                case '-':
                    if (_isNegInf(trim)) {
                        return Double.NEGATIVE_INFINITY;
                    }
                    break;
                case 'I':
                    if (_isPosInf(trim)) {
                        return Double.POSITIVE_INFINITY;
                    }
                    break;
                case 'N':
                    if (_isNaN(trim)) {
                        return Double.NaN;
                    }
                    break;
            }
            try {
                return parseDouble(trim);
            } catch (IllegalArgumentException e) {
                throw deserializationContext.weirdStringException(trim, this._valueClass, "not a valid double value");
            }
        } else if (currentToken == JsonToken.VALUE_NULL) {
            return 0.0d;
        } else {
            if (currentToken != JsonToken.START_ARRAY || !deserializationContext.isEnabled(DeserializationFeature.UNWRAP_SINGLE_VALUE_ARRAYS)) {
                throw deserializationContext.mappingException(this._valueClass, currentToken);
            }
            jsonParser.nextToken();
            double _parseDoublePrimitive = _parseDoublePrimitive(jsonParser, deserializationContext);
            if (jsonParser.nextToken() == JsonToken.END_ARRAY) {
                return _parseDoublePrimitive;
            }
            throw deserializationContext.wrongTokenException(jsonParser, JsonToken.END_ARRAY, "Attempted to unwrap single value array for single 'Byte' value but there was more than a single value in the array");
        }
    }

    /* access modifiers changed from: protected */
    public Date _parseDate(JsonParser jsonParser, DeserializationContext deserializationContext) throws IOException {
        JsonToken currentToken = jsonParser.getCurrentToken();
        if (currentToken == JsonToken.VALUE_NUMBER_INT) {
            return new Date(jsonParser.getLongValue());
        }
        if (currentToken == JsonToken.VALUE_NULL) {
            return (Date) getNullValue(deserializationContext);
        }
        if (currentToken == JsonToken.VALUE_STRING) {
            try {
                String trim = jsonParser.getText().trim();
                if (trim.length() == 0) {
                    return (Date) getEmptyValue(deserializationContext);
                }
                if (_hasTextualNull(trim)) {
                    return (Date) getNullValue(deserializationContext);
                }
                return deserializationContext.parseDate(trim);
            } catch (IllegalArgumentException e) {
                throw deserializationContext.weirdStringException(null, this._valueClass, "not a valid representation (error: " + e.getMessage() + ")");
            }
        } else if (currentToken != JsonToken.START_ARRAY || !deserializationContext.isEnabled(DeserializationFeature.UNWRAP_SINGLE_VALUE_ARRAYS)) {
            throw deserializationContext.mappingException(this._valueClass, currentToken);
        } else {
            jsonParser.nextToken();
            Date _parseDate = _parseDate(jsonParser, deserializationContext);
            if (jsonParser.nextToken() == JsonToken.END_ARRAY) {
                return _parseDate;
            }
            throw deserializationContext.wrongTokenException(jsonParser, JsonToken.END_ARRAY, "Attempted to unwrap single value array for single 'java.util.Date' value but there was more than a single value in the array");
        }
    }

    protected static final double parseDouble(String str) throws NumberFormatException {
        if (NumberInput.NASTY_SMALL_DOUBLE.equals(str)) {
            return Double.MIN_NORMAL;
        }
        return Double.parseDouble(str);
    }

    /* access modifiers changed from: protected */
    public final String _parseString(JsonParser jsonParser, DeserializationContext deserializationContext) throws IOException {
        JsonToken currentToken = jsonParser.getCurrentToken();
        if (currentToken == JsonToken.VALUE_STRING) {
            return jsonParser.getText();
        }
        if (currentToken != JsonToken.START_ARRAY || !deserializationContext.isEnabled(DeserializationFeature.UNWRAP_SINGLE_VALUE_ARRAYS)) {
            String valueAsString = jsonParser.getValueAsString();
            if (valueAsString != null) {
                return valueAsString;
            }
            throw deserializationContext.mappingException(String.class, jsonParser.getCurrentToken());
        }
        jsonParser.nextToken();
        String _parseString = _parseString(jsonParser, deserializationContext);
        if (jsonParser.nextToken() == JsonToken.END_ARRAY) {
            return _parseString;
        }
        throw deserializationContext.wrongTokenException(jsonParser, JsonToken.END_ARRAY, "Attempted to unwrap single value array for single 'String' value but there was more than a single value in the array");
    }

    /* access modifiers changed from: protected */
    /* JADX WARNING: Code restructure failed: missing block: B:14:0x003d, code lost:
        if (r4.getText().trim().isEmpty() != false) goto L_0x0019;
     */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    public T _deserializeFromEmpty(com.fasterxml.jackson.core.JsonParser r4, com.fasterxml.jackson.databind.DeserializationContext r5) throws java.io.IOException {
        /*
            r3 = this;
            r2 = 0
            com.fasterxml.jackson.core.JsonToken r0 = r4.getCurrentToken()
            com.fasterxml.jackson.core.JsonToken r1 = com.fasterxml.jackson.core.JsonToken.START_ARRAY
            if (r0 != r1) goto L_0x0025
            com.fasterxml.jackson.databind.DeserializationFeature r0 = com.fasterxml.jackson.databind.DeserializationFeature.ACCEPT_EMPTY_ARRAY_AS_NULL_OBJECT
            boolean r0 = r5.isEnabled(r0)
            if (r0 == 0) goto L_0x003f
            com.fasterxml.jackson.core.JsonToken r0 = r4.nextToken()
            com.fasterxml.jackson.core.JsonToken r1 = com.fasterxml.jackson.core.JsonToken.END_ARRAY
            if (r0 != r1) goto L_0x001a
        L_0x0019:
            return r2
        L_0x001a:
            java.lang.Class r0 = r3.handledType()
            com.fasterxml.jackson.core.JsonToken r1 = com.fasterxml.jackson.core.JsonToken.START_ARRAY
            com.fasterxml.jackson.databind.JsonMappingException r0 = r5.mappingException(r0, r1)
            throw r0
        L_0x0025:
            com.fasterxml.jackson.core.JsonToken r1 = com.fasterxml.jackson.core.JsonToken.VALUE_STRING
            if (r0 != r1) goto L_0x003f
            com.fasterxml.jackson.databind.DeserializationFeature r0 = com.fasterxml.jackson.databind.DeserializationFeature.ACCEPT_EMPTY_STRING_AS_NULL_OBJECT
            boolean r0 = r5.isEnabled(r0)
            if (r0 == 0) goto L_0x003f
            java.lang.String r0 = r4.getText()
            java.lang.String r0 = r0.trim()
            boolean r0 = r0.isEmpty()
            if (r0 != 0) goto L_0x0019
        L_0x003f:
            java.lang.Class r0 = r3.handledType()
            com.fasterxml.jackson.databind.JsonMappingException r0 = r5.mappingException(r0)
            throw r0
        */
        throw new UnsupportedOperationException("Method not decompiled: com.fasterxml.jackson.databind.deser.std.StdDeserializer._deserializeFromEmpty(com.fasterxml.jackson.core.JsonParser, com.fasterxml.jackson.databind.DeserializationContext):java.lang.Object");
    }

    /* access modifiers changed from: protected */
    public boolean _hasTextualNull(String str) {
        return "null".equals(str);
    }

    /* access modifiers changed from: protected */
    public final boolean _isNegInf(String str) {
        return "-Infinity".equals(str) || "-INF".equals(str);
    }

    /* access modifiers changed from: protected */
    public final boolean _isPosInf(String str) {
        return "Infinity".equals(str) || "INF".equals(str);
    }

    /* access modifiers changed from: protected */
    public final boolean _isNaN(String str) {
        return "NaN".equals(str);
    }

    /* access modifiers changed from: protected */
    public Object _coerceIntegral(JsonParser jsonParser, DeserializationContext deserializationContext) throws IOException {
        int deserializationFeatures = deserializationContext.getDeserializationFeatures();
        if (DeserializationFeature.USE_BIG_INTEGER_FOR_INTS.enabledIn(deserializationFeatures)) {
            return jsonParser.getBigIntegerValue();
        }
        if (DeserializationFeature.USE_LONG_FOR_INTS.enabledIn(deserializationFeatures)) {
            return Long.valueOf(jsonParser.getLongValue());
        }
        return jsonParser.getBigIntegerValue();
    }

    /* access modifiers changed from: protected */
    public JsonDeserializer<Object> findDeserializer(DeserializationContext deserializationContext, JavaType javaType, BeanProperty beanProperty) throws JsonMappingException {
        return deserializationContext.findContextualValueDeserializer(javaType, beanProperty);
    }

    /* access modifiers changed from: protected */
    public final boolean _isIntNumber(String str) {
        int length = str.length();
        if (length <= 0) {
            return false;
        }
        char charAt = str.charAt(0);
        for (int i = (charAt == '-' || charAt == '+') ? 1 : 0; i < length; i++) {
            char charAt2 = str.charAt(i);
            if (charAt2 > '9' || charAt2 < '0') {
                return false;
            }
        }
        return true;
    }

    /* access modifiers changed from: protected */
    public JsonDeserializer<?> findConvertingContentDeserializer(DeserializationContext deserializationContext, BeanProperty beanProperty, JsonDeserializer<?> jsonDeserializer) throws JsonMappingException {
        AnnotationIntrospector annotationIntrospector = deserializationContext.getAnnotationIntrospector();
        if (annotationIntrospector == null || beanProperty == null) {
            return jsonDeserializer;
        }
        AnnotatedMember member = beanProperty.getMember();
        if (member == null) {
            return jsonDeserializer;
        }
        Object findDeserializationContentConverter = annotationIntrospector.findDeserializationContentConverter(member);
        if (findDeserializationContentConverter == null) {
            return jsonDeserializer;
        }
        Converter converterInstance = deserializationContext.converterInstance(beanProperty.getMember(), findDeserializationContentConverter);
        JavaType inputType = converterInstance.getInputType(deserializationContext.getTypeFactory());
        if (jsonDeserializer == null) {
            jsonDeserializer = deserializationContext.findContextualValueDeserializer(inputType, beanProperty);
        }
        return new StdDelegatingDeserializer(converterInstance, inputType, jsonDeserializer);
    }

    /* access modifiers changed from: protected */
    public Value findFormatOverrides(DeserializationContext deserializationContext, BeanProperty beanProperty, Class<?> cls) {
        if (beanProperty != null) {
            return beanProperty.findPropertyFormat(deserializationContext.getConfig(), cls);
        }
        return deserializationContext.getDefaultPropertyFormat(cls);
    }

    /* access modifiers changed from: protected */
    public Boolean findFormatFeature(DeserializationContext deserializationContext, BeanProperty beanProperty, Class<?> cls, Feature feature) {
        Value findFormatOverrides = findFormatOverrides(deserializationContext, beanProperty, cls);
        if (findFormatOverrides != null) {
            return findFormatOverrides.getFeature(feature);
        }
        return null;
    }

    /* access modifiers changed from: protected */
    public void handleUnknownProperty(JsonParser jsonParser, DeserializationContext deserializationContext, Object obj, String str) throws IOException {
        if (obj == null) {
            obj = handledType();
        }
        if (!deserializationContext.handleUnknownProperty(jsonParser, this, obj, str)) {
            deserializationContext.reportUnknownProperty(obj, str, this);
            jsonParser.skipChildren();
        }
    }

    /* access modifiers changed from: protected */
    public void _failDoubleToIntCoercion(JsonParser jsonParser, DeserializationContext deserializationContext, String str) throws IOException {
        throw deserializationContext.mappingException("Can not coerce a floating-point value ('%s') into %s; enable `DeserializationFeature.ACCEPT_FLOAT_AS_INT` to allow", jsonParser.getValueAsString(), str);
    }
}
