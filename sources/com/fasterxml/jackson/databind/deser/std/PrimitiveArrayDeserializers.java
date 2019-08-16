package com.fasterxml.jackson.databind.deser.std;

import com.fasterxml.jackson.annotation.JsonFormat.Feature;
import com.fasterxml.jackson.core.Base64Variants;
import com.fasterxml.jackson.core.JsonParser;
import com.fasterxml.jackson.core.JsonProcessingException;
import com.fasterxml.jackson.core.JsonToken;
import com.fasterxml.jackson.databind.BeanProperty;
import com.fasterxml.jackson.databind.DeserializationContext;
import com.fasterxml.jackson.databind.DeserializationFeature;
import com.fasterxml.jackson.databind.JsonDeserializer;
import com.fasterxml.jackson.databind.JsonMappingException;
import com.fasterxml.jackson.databind.annotation.JacksonStdImpl;
import com.fasterxml.jackson.databind.deser.ContextualDeserializer;
import com.fasterxml.jackson.databind.jsontype.TypeDeserializer;
import com.fasterxml.jackson.databind.util.ArrayBuilders.BooleanBuilder;
import com.fasterxml.jackson.databind.util.ArrayBuilders.ByteBuilder;
import com.fasterxml.jackson.databind.util.ArrayBuilders.DoubleBuilder;
import com.fasterxml.jackson.databind.util.ArrayBuilders.FloatBuilder;
import com.fasterxml.jackson.databind.util.ArrayBuilders.IntBuilder;
import com.fasterxml.jackson.databind.util.ArrayBuilders.LongBuilder;
import com.fasterxml.jackson.databind.util.ArrayBuilders.ShortBuilder;
import java.io.IOException;

public abstract class PrimitiveArrayDeserializers<T> extends StdDeserializer<T> implements ContextualDeserializer {
    protected final Boolean _unwrapSingle;

    @JacksonStdImpl
    static final class BooleanDeser extends PrimitiveArrayDeserializers<boolean[]> {
        private static final long serialVersionUID = 1;

        public BooleanDeser() {
            super(boolean[].class);
        }

        protected BooleanDeser(BooleanDeser booleanDeser, Boolean bool) {
            super(booleanDeser, bool);
        }

        /* access modifiers changed from: protected */
        public PrimitiveArrayDeserializers<?> withResolved(Boolean bool) {
            return new BooleanDeser(this, bool);
        }

        public boolean[] deserialize(JsonParser jsonParser, DeserializationContext deserializationContext) throws IOException, JsonProcessingException {
            int i;
            if (!jsonParser.isExpectedStartArrayToken()) {
                return (boolean[]) handleNonArray(jsonParser, deserializationContext);
            }
            BooleanBuilder booleanBuilder = deserializationContext.getArrayBuilders().getBooleanBuilder();
            int i2 = 0;
            boolean[] zArr = (boolean[]) booleanBuilder.resetAndStart();
            while (jsonParser.nextToken() != JsonToken.END_ARRAY) {
                try {
                    boolean _parseBooleanPrimitive = _parseBooleanPrimitive(jsonParser, deserializationContext);
                    if (i2 >= zArr.length) {
                        i = 0;
                        zArr = (boolean[]) booleanBuilder.appendCompletedChunk(zArr, i2);
                    } else {
                        i = i2;
                    }
                    i2 = i + 1;
                    zArr[i] = _parseBooleanPrimitive;
                } catch (Exception e) {
                    throw JsonMappingException.wrapWithPath((Throwable) e, (Object) zArr, i2 + booleanBuilder.bufferedSize());
                }
            }
            return (boolean[]) booleanBuilder.completeAndClearBuffer(zArr, i2);
        }

        /* access modifiers changed from: protected */
        public boolean[] handleSingleElementUnwrapped(JsonParser jsonParser, DeserializationContext deserializationContext) throws IOException {
            return new boolean[]{_parseBooleanPrimitive(jsonParser, deserializationContext)};
        }
    }

    @JacksonStdImpl
    static final class ByteDeser extends PrimitiveArrayDeserializers<byte[]> {
        private static final long serialVersionUID = 1;

        public ByteDeser() {
            super(byte[].class);
        }

        protected ByteDeser(ByteDeser byteDeser, Boolean bool) {
            super(byteDeser, bool);
        }

        /* access modifiers changed from: protected */
        public PrimitiveArrayDeserializers<?> withResolved(Boolean bool) {
            return new ByteDeser(this, bool);
        }

        public byte[] deserialize(JsonParser jsonParser, DeserializationContext deserializationContext) throws IOException {
            byte b;
            int i;
            JsonToken currentToken = jsonParser.getCurrentToken();
            if (currentToken == JsonToken.VALUE_STRING) {
                return jsonParser.getBinaryValue(deserializationContext.getBase64Variant());
            }
            if (currentToken == JsonToken.VALUE_EMBEDDED_OBJECT) {
                Object embeddedObject = jsonParser.getEmbeddedObject();
                if (embeddedObject == null) {
                    return null;
                }
                if (embeddedObject instanceof byte[]) {
                    return (byte[]) embeddedObject;
                }
            }
            if (!jsonParser.isExpectedStartArrayToken()) {
                return (byte[]) handleNonArray(jsonParser, deserializationContext);
            }
            ByteBuilder byteBuilder = deserializationContext.getArrayBuilders().getByteBuilder();
            int i2 = 0;
            byte[] bArr = (byte[]) byteBuilder.resetAndStart();
            while (true) {
                try {
                    JsonToken nextToken = jsonParser.nextToken();
                    if (nextToken == JsonToken.END_ARRAY) {
                        return (byte[]) byteBuilder.completeAndClearBuffer(bArr, i2);
                    }
                    if (nextToken == JsonToken.VALUE_NUMBER_INT || nextToken == JsonToken.VALUE_NUMBER_FLOAT) {
                        b = jsonParser.getByteValue();
                    } else if (nextToken != JsonToken.VALUE_NULL) {
                        throw deserializationContext.mappingException(this._valueClass.getComponentType());
                    } else {
                        b = 0;
                    }
                    if (i2 >= bArr.length) {
                        i = 0;
                        bArr = (byte[]) byteBuilder.appendCompletedChunk(bArr, i2);
                    } else {
                        i = i2;
                    }
                    i2 = i + 1;
                    bArr[i] = b;
                } catch (Exception e) {
                    throw JsonMappingException.wrapWithPath((Throwable) e, (Object) bArr, i2 + byteBuilder.bufferedSize());
                }
            }
        }

        /* access modifiers changed from: protected */
        public byte[] handleSingleElementUnwrapped(JsonParser jsonParser, DeserializationContext deserializationContext) throws IOException {
            byte b;
            JsonToken currentToken = jsonParser.getCurrentToken();
            if (currentToken == JsonToken.VALUE_NUMBER_INT || currentToken == JsonToken.VALUE_NUMBER_FLOAT) {
                b = jsonParser.getByteValue();
            } else if (currentToken != JsonToken.VALUE_NULL) {
                throw deserializationContext.mappingException(this._valueClass.getComponentType());
            } else {
                b = 0;
            }
            return new byte[]{b};
        }
    }

    @JacksonStdImpl
    static final class CharDeser extends PrimitiveArrayDeserializers<char[]> {
        private static final long serialVersionUID = 1;

        public CharDeser() {
            super(char[].class);
        }

        protected CharDeser(CharDeser charDeser, Boolean bool) {
            super(charDeser, bool);
        }

        /* access modifiers changed from: protected */
        public PrimitiveArrayDeserializers<?> withResolved(Boolean bool) {
            return this;
        }

        public char[] deserialize(JsonParser jsonParser, DeserializationContext deserializationContext) throws IOException {
            JsonToken currentToken = jsonParser.getCurrentToken();
            if (currentToken == JsonToken.VALUE_STRING) {
                char[] textCharacters = jsonParser.getTextCharacters();
                int textOffset = jsonParser.getTextOffset();
                int textLength = jsonParser.getTextLength();
                char[] cArr = new char[textLength];
                System.arraycopy(textCharacters, textOffset, cArr, 0, textLength);
                return cArr;
            } else if (jsonParser.isExpectedStartArrayToken()) {
                StringBuilder sb = new StringBuilder(64);
                while (true) {
                    JsonToken nextToken = jsonParser.nextToken();
                    if (nextToken == JsonToken.END_ARRAY) {
                        return sb.toString().toCharArray();
                    }
                    if (nextToken != JsonToken.VALUE_STRING) {
                        throw deserializationContext.mappingException(Character.TYPE);
                    }
                    String text = jsonParser.getText();
                    if (text.length() != 1) {
                        throw JsonMappingException.from(jsonParser, "Can not convert a JSON String of length " + text.length() + " into a char element of char array");
                    }
                    sb.append(text.charAt(0));
                }
            } else {
                if (currentToken == JsonToken.VALUE_EMBEDDED_OBJECT) {
                    Object embeddedObject = jsonParser.getEmbeddedObject();
                    if (embeddedObject == null) {
                        return null;
                    }
                    if (embeddedObject instanceof char[]) {
                        return (char[]) embeddedObject;
                    }
                    if (embeddedObject instanceof String) {
                        return ((String) embeddedObject).toCharArray();
                    }
                    if (embeddedObject instanceof byte[]) {
                        return Base64Variants.getDefaultVariant().encode((byte[]) embeddedObject, false).toCharArray();
                    }
                }
                throw deserializationContext.mappingException(this._valueClass);
            }
        }

        /* access modifiers changed from: protected */
        public char[] handleSingleElementUnwrapped(JsonParser jsonParser, DeserializationContext deserializationContext) throws IOException {
            throw deserializationContext.mappingException(this._valueClass);
        }
    }

    @JacksonStdImpl
    static final class DoubleDeser extends PrimitiveArrayDeserializers<double[]> {
        private static final long serialVersionUID = 1;

        public DoubleDeser() {
            super(double[].class);
        }

        protected DoubleDeser(DoubleDeser doubleDeser, Boolean bool) {
            super(doubleDeser, bool);
        }

        /* access modifiers changed from: protected */
        public PrimitiveArrayDeserializers<?> withResolved(Boolean bool) {
            return new DoubleDeser(this, bool);
        }

        public double[] deserialize(JsonParser jsonParser, DeserializationContext deserializationContext) throws IOException {
            int i;
            if (!jsonParser.isExpectedStartArrayToken()) {
                return (double[]) handleNonArray(jsonParser, deserializationContext);
            }
            DoubleBuilder doubleBuilder = deserializationContext.getArrayBuilders().getDoubleBuilder();
            int i2 = 0;
            double[] dArr = (double[]) doubleBuilder.resetAndStart();
            while (jsonParser.nextToken() != JsonToken.END_ARRAY) {
                try {
                    double _parseDoublePrimitive = _parseDoublePrimitive(jsonParser, deserializationContext);
                    if (i2 >= dArr.length) {
                        i = 0;
                        dArr = (double[]) doubleBuilder.appendCompletedChunk(dArr, i2);
                    } else {
                        i = i2;
                    }
                    i2 = i + 1;
                    dArr[i] = _parseDoublePrimitive;
                } catch (Exception e) {
                    throw JsonMappingException.wrapWithPath((Throwable) e, (Object) dArr, i2 + doubleBuilder.bufferedSize());
                }
            }
            return (double[]) doubleBuilder.completeAndClearBuffer(dArr, i2);
        }

        /* access modifiers changed from: protected */
        public double[] handleSingleElementUnwrapped(JsonParser jsonParser, DeserializationContext deserializationContext) throws IOException {
            return new double[]{_parseDoublePrimitive(jsonParser, deserializationContext)};
        }
    }

    @JacksonStdImpl
    static final class FloatDeser extends PrimitiveArrayDeserializers<float[]> {
        private static final long serialVersionUID = 1;

        public FloatDeser() {
            super(float[].class);
        }

        protected FloatDeser(FloatDeser floatDeser, Boolean bool) {
            super(floatDeser, bool);
        }

        /* access modifiers changed from: protected */
        public PrimitiveArrayDeserializers<?> withResolved(Boolean bool) {
            return new FloatDeser(this, bool);
        }

        public float[] deserialize(JsonParser jsonParser, DeserializationContext deserializationContext) throws IOException, JsonProcessingException {
            int i;
            if (!jsonParser.isExpectedStartArrayToken()) {
                return (float[]) handleNonArray(jsonParser, deserializationContext);
            }
            FloatBuilder floatBuilder = deserializationContext.getArrayBuilders().getFloatBuilder();
            int i2 = 0;
            float[] fArr = (float[]) floatBuilder.resetAndStart();
            while (jsonParser.nextToken() != JsonToken.END_ARRAY) {
                try {
                    float _parseFloatPrimitive = _parseFloatPrimitive(jsonParser, deserializationContext);
                    if (i2 >= fArr.length) {
                        i = 0;
                        fArr = (float[]) floatBuilder.appendCompletedChunk(fArr, i2);
                    } else {
                        i = i2;
                    }
                    i2 = i + 1;
                    fArr[i] = _parseFloatPrimitive;
                } catch (Exception e) {
                    throw JsonMappingException.wrapWithPath((Throwable) e, (Object) fArr, i2 + floatBuilder.bufferedSize());
                }
            }
            return (float[]) floatBuilder.completeAndClearBuffer(fArr, i2);
        }

        /* access modifiers changed from: protected */
        public float[] handleSingleElementUnwrapped(JsonParser jsonParser, DeserializationContext deserializationContext) throws IOException {
            return new float[]{_parseFloatPrimitive(jsonParser, deserializationContext)};
        }
    }

    @JacksonStdImpl
    static final class IntDeser extends PrimitiveArrayDeserializers<int[]> {
        public static final IntDeser instance = new IntDeser();
        private static final long serialVersionUID = 1;

        public IntDeser() {
            super(int[].class);
        }

        protected IntDeser(IntDeser intDeser, Boolean bool) {
            super(intDeser, bool);
        }

        /* access modifiers changed from: protected */
        public PrimitiveArrayDeserializers<?> withResolved(Boolean bool) {
            return new IntDeser(this, bool);
        }

        public int[] deserialize(JsonParser jsonParser, DeserializationContext deserializationContext) throws IOException {
            int i;
            if (!jsonParser.isExpectedStartArrayToken()) {
                return (int[]) handleNonArray(jsonParser, deserializationContext);
            }
            IntBuilder intBuilder = deserializationContext.getArrayBuilders().getIntBuilder();
            int i2 = 0;
            int[] iArr = (int[]) intBuilder.resetAndStart();
            while (jsonParser.nextToken() != JsonToken.END_ARRAY) {
                try {
                    int _parseIntPrimitive = _parseIntPrimitive(jsonParser, deserializationContext);
                    if (i2 >= iArr.length) {
                        i = 0;
                        iArr = (int[]) intBuilder.appendCompletedChunk(iArr, i2);
                    } else {
                        i = i2;
                    }
                    i2 = i + 1;
                    iArr[i] = _parseIntPrimitive;
                } catch (Exception e) {
                    throw JsonMappingException.wrapWithPath((Throwable) e, (Object) iArr, i2 + intBuilder.bufferedSize());
                }
            }
            return (int[]) intBuilder.completeAndClearBuffer(iArr, i2);
        }

        /* access modifiers changed from: protected */
        public int[] handleSingleElementUnwrapped(JsonParser jsonParser, DeserializationContext deserializationContext) throws IOException {
            return new int[]{_parseIntPrimitive(jsonParser, deserializationContext)};
        }
    }

    @JacksonStdImpl
    static final class LongDeser extends PrimitiveArrayDeserializers<long[]> {
        public static final LongDeser instance = new LongDeser();
        private static final long serialVersionUID = 1;

        public LongDeser() {
            super(long[].class);
        }

        protected LongDeser(LongDeser longDeser, Boolean bool) {
            super(longDeser, bool);
        }

        /* access modifiers changed from: protected */
        public PrimitiveArrayDeserializers<?> withResolved(Boolean bool) {
            return new LongDeser(this, bool);
        }

        public long[] deserialize(JsonParser jsonParser, DeserializationContext deserializationContext) throws IOException {
            int i;
            if (!jsonParser.isExpectedStartArrayToken()) {
                return (long[]) handleNonArray(jsonParser, deserializationContext);
            }
            LongBuilder longBuilder = deserializationContext.getArrayBuilders().getLongBuilder();
            int i2 = 0;
            long[] jArr = (long[]) longBuilder.resetAndStart();
            while (jsonParser.nextToken() != JsonToken.END_ARRAY) {
                try {
                    long _parseLongPrimitive = _parseLongPrimitive(jsonParser, deserializationContext);
                    if (i2 >= jArr.length) {
                        i = 0;
                        jArr = (long[]) longBuilder.appendCompletedChunk(jArr, i2);
                    } else {
                        i = i2;
                    }
                    i2 = i + 1;
                    jArr[i] = _parseLongPrimitive;
                } catch (Exception e) {
                    throw JsonMappingException.wrapWithPath((Throwable) e, (Object) jArr, i2 + longBuilder.bufferedSize());
                }
            }
            return (long[]) longBuilder.completeAndClearBuffer(jArr, i2);
        }

        /* access modifiers changed from: protected */
        public long[] handleSingleElementUnwrapped(JsonParser jsonParser, DeserializationContext deserializationContext) throws IOException {
            return new long[]{_parseLongPrimitive(jsonParser, deserializationContext)};
        }
    }

    @JacksonStdImpl
    static final class ShortDeser extends PrimitiveArrayDeserializers<short[]> {
        private static final long serialVersionUID = 1;

        public ShortDeser() {
            super(short[].class);
        }

        protected ShortDeser(ShortDeser shortDeser, Boolean bool) {
            super(shortDeser, bool);
        }

        /* access modifiers changed from: protected */
        public PrimitiveArrayDeserializers<?> withResolved(Boolean bool) {
            return new ShortDeser(this, bool);
        }

        public short[] deserialize(JsonParser jsonParser, DeserializationContext deserializationContext) throws IOException {
            int i;
            if (!jsonParser.isExpectedStartArrayToken()) {
                return (short[]) handleNonArray(jsonParser, deserializationContext);
            }
            ShortBuilder shortBuilder = deserializationContext.getArrayBuilders().getShortBuilder();
            int i2 = 0;
            short[] sArr = (short[]) shortBuilder.resetAndStart();
            while (jsonParser.nextToken() != JsonToken.END_ARRAY) {
                try {
                    short _parseShortPrimitive = _parseShortPrimitive(jsonParser, deserializationContext);
                    if (i2 >= sArr.length) {
                        i = 0;
                        sArr = (short[]) shortBuilder.appendCompletedChunk(sArr, i2);
                    } else {
                        i = i2;
                    }
                    i2 = i + 1;
                    sArr[i] = _parseShortPrimitive;
                } catch (Exception e) {
                    throw JsonMappingException.wrapWithPath((Throwable) e, (Object) sArr, i2 + shortBuilder.bufferedSize());
                }
            }
            return (short[]) shortBuilder.completeAndClearBuffer(sArr, i2);
        }

        /* access modifiers changed from: protected */
        public short[] handleSingleElementUnwrapped(JsonParser jsonParser, DeserializationContext deserializationContext) throws IOException {
            return new short[]{_parseShortPrimitive(jsonParser, deserializationContext)};
        }
    }

    /* access modifiers changed from: protected */
    public abstract T handleSingleElementUnwrapped(JsonParser jsonParser, DeserializationContext deserializationContext) throws IOException;

    /* access modifiers changed from: protected */
    public abstract PrimitiveArrayDeserializers<?> withResolved(Boolean bool);

    protected PrimitiveArrayDeserializers(Class<T> cls) {
        super(cls);
        this._unwrapSingle = null;
    }

    protected PrimitiveArrayDeserializers(PrimitiveArrayDeserializers<?> primitiveArrayDeserializers, Boolean bool) {
        super(primitiveArrayDeserializers._valueClass);
        this._unwrapSingle = bool;
    }

    public static JsonDeserializer<?> forType(Class<?> cls) {
        if (cls == Integer.TYPE) {
            return IntDeser.instance;
        }
        if (cls == Long.TYPE) {
            return LongDeser.instance;
        }
        if (cls == Byte.TYPE) {
            return new ByteDeser();
        }
        if (cls == Short.TYPE) {
            return new ShortDeser();
        }
        if (cls == Float.TYPE) {
            return new FloatDeser();
        }
        if (cls == Double.TYPE) {
            return new DoubleDeser();
        }
        if (cls == Boolean.TYPE) {
            return new BooleanDeser();
        }
        if (cls == Character.TYPE) {
            return new CharDeser();
        }
        throw new IllegalStateException();
    }

    public JsonDeserializer<?> createContextual(DeserializationContext deserializationContext, BeanProperty beanProperty) throws JsonMappingException {
        Boolean findFormatFeature = findFormatFeature(deserializationContext, beanProperty, this._valueClass, Feature.ACCEPT_SINGLE_VALUE_AS_ARRAY);
        return findFormatFeature == this._unwrapSingle ? this : withResolved(findFormatFeature);
    }

    public Object deserializeWithType(JsonParser jsonParser, DeserializationContext deserializationContext, TypeDeserializer typeDeserializer) throws IOException {
        return typeDeserializer.deserializeTypedFromArray(jsonParser, deserializationContext);
    }

    /* access modifiers changed from: protected */
    public T handleNonArray(JsonParser jsonParser, DeserializationContext deserializationContext) throws IOException {
        if (jsonParser.hasToken(JsonToken.VALUE_STRING) && deserializationContext.isEnabled(DeserializationFeature.ACCEPT_EMPTY_STRING_AS_NULL_OBJECT) && jsonParser.getText().length() == 0) {
            return null;
        }
        if (this._unwrapSingle == Boolean.TRUE || (this._unwrapSingle == null && deserializationContext.isEnabled(DeserializationFeature.ACCEPT_SINGLE_VALUE_AS_ARRAY))) {
            return handleSingleElementUnwrapped(jsonParser, deserializationContext);
        }
        throw deserializationContext.mappingException(this._valueClass);
    }
}
