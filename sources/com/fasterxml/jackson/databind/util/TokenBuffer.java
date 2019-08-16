package com.fasterxml.jackson.databind.util;

import com.fasterxml.jackson.core.Base64Variant;
import com.fasterxml.jackson.core.JsonGenerationException;
import com.fasterxml.jackson.core.JsonGenerator;
import com.fasterxml.jackson.core.JsonGenerator.Feature;
import com.fasterxml.jackson.core.JsonLocation;
import com.fasterxml.jackson.core.JsonParseException;
import com.fasterxml.jackson.core.JsonParser;
import com.fasterxml.jackson.core.JsonParser.NumberType;
import com.fasterxml.jackson.core.JsonStreamContext;
import com.fasterxml.jackson.core.JsonToken;
import com.fasterxml.jackson.core.ObjectCodec;
import com.fasterxml.jackson.core.SerializableString;
import com.fasterxml.jackson.core.TreeNode;
import com.fasterxml.jackson.core.Version;
import com.fasterxml.jackson.core.base.ParserMinimalBase;
import com.fasterxml.jackson.core.json.JsonReadContext;
import com.fasterxml.jackson.core.json.JsonWriteContext;
import com.fasterxml.jackson.core.util.ByteArrayBuilder;
import com.fasterxml.jackson.databind.DeserializationContext;
import com.fasterxml.jackson.databind.DeserializationFeature;
import com.fasterxml.jackson.databind.cfg.PackageVersion;
import java.io.IOException;
import java.io.InputStream;
import java.io.OutputStream;
import java.math.BigDecimal;
import java.math.BigInteger;
import java.util.TreeMap;

public class TokenBuffer extends JsonGenerator {
    protected static final int DEFAULT_GENERATOR_FEATURES = Feature.collectDefaults();
    protected int _appendAt;
    protected boolean _closed;
    protected Segment _first;
    protected boolean _forceBigDecimal;
    protected int _generatorFeatures;
    protected boolean _hasNativeId;
    protected boolean _hasNativeObjectIds;
    protected boolean _hasNativeTypeIds;
    protected Segment _last;
    protected boolean _mayHaveNativeIds;
    protected ObjectCodec _objectCodec;
    protected Object _objectId;
    protected Object _typeId;
    protected JsonWriteContext _writeContext;

    protected static final class Parser extends ParserMinimalBase {
        protected transient ByteArrayBuilder _byteBuilder;
        protected boolean _closed;
        protected ObjectCodec _codec;
        protected final boolean _hasNativeIds;
        protected final boolean _hasNativeObjectIds;
        protected final boolean _hasNativeTypeIds;
        protected JsonLocation _location = null;
        protected JsonReadContext _parsingContext;
        protected Segment _segment;
        protected int _segmentPtr;

        public Parser(Segment segment, ObjectCodec objectCodec, boolean z, boolean z2) {
            super(0);
            this._segment = segment;
            this._segmentPtr = -1;
            this._codec = objectCodec;
            this._parsingContext = JsonReadContext.createRootContext(null);
            this._hasNativeTypeIds = z;
            this._hasNativeObjectIds = z2;
            this._hasNativeIds = z | z2;
        }

        public void setLocation(JsonLocation jsonLocation) {
            this._location = jsonLocation;
        }

        public ObjectCodec getCodec() {
            return this._codec;
        }

        public void setCodec(ObjectCodec objectCodec) {
            this._codec = objectCodec;
        }

        public Version version() {
            return PackageVersion.VERSION;
        }

        public JsonToken peekNextToken() throws IOException {
            int i;
            if (this._closed) {
                return null;
            }
            Segment segment = this._segment;
            int i2 = this._segmentPtr + 1;
            if (i2 >= 16) {
                i = 0;
                segment = segment == null ? null : segment.next();
            } else {
                i = i2;
            }
            if (segment != null) {
                return segment.type(i);
            }
            return null;
        }

        public void close() throws IOException {
            if (!this._closed) {
                this._closed = true;
            }
        }

        public JsonToken nextToken() throws IOException {
            if (this._closed || this._segment == null) {
                return null;
            }
            int i = this._segmentPtr + 1;
            this._segmentPtr = i;
            if (i >= 16) {
                this._segmentPtr = 0;
                this._segment = this._segment.next();
                if (this._segment == null) {
                    return null;
                }
            }
            this._currToken = this._segment.type(this._segmentPtr);
            if (this._currToken == JsonToken.FIELD_NAME) {
                Object _currentObject = _currentObject();
                this._parsingContext.setCurrentName(_currentObject instanceof String ? (String) _currentObject : _currentObject.toString());
            } else if (this._currToken == JsonToken.START_OBJECT) {
                this._parsingContext = this._parsingContext.createChildObjectContext(-1, -1);
            } else if (this._currToken == JsonToken.START_ARRAY) {
                this._parsingContext = this._parsingContext.createChildArrayContext(-1, -1);
            } else if (this._currToken == JsonToken.END_OBJECT || this._currToken == JsonToken.END_ARRAY) {
                this._parsingContext = this._parsingContext.getParent();
                if (this._parsingContext == null) {
                    this._parsingContext = JsonReadContext.createRootContext(null);
                }
            }
            return this._currToken;
        }

        public String nextFieldName() throws IOException {
            if (this._closed || this._segment == null) {
                return null;
            }
            int i = this._segmentPtr + 1;
            if (i < 16 && this._segment.type(i) == JsonToken.FIELD_NAME) {
                this._segmentPtr = i;
                Object obj = this._segment.get(i);
                String obj2 = obj instanceof String ? (String) obj : obj.toString();
                this._parsingContext.setCurrentName(obj2);
                return obj2;
            } else if (nextToken() == JsonToken.FIELD_NAME) {
                return getCurrentName();
            } else {
                return null;
            }
        }

        public boolean isClosed() {
            return this._closed;
        }

        public JsonStreamContext getParsingContext() {
            return this._parsingContext;
        }

        public JsonLocation getTokenLocation() {
            return getCurrentLocation();
        }

        public JsonLocation getCurrentLocation() {
            return this._location == null ? JsonLocation.f405NA : this._location;
        }

        public String getCurrentName() {
            if (this._currToken == JsonToken.START_OBJECT || this._currToken == JsonToken.START_ARRAY) {
                return this._parsingContext.getParent().getCurrentName();
            }
            return this._parsingContext.getCurrentName();
        }

        public void overrideCurrentName(String str) {
            JsonReadContext jsonReadContext = this._parsingContext;
            if (this._currToken == JsonToken.START_OBJECT || this._currToken == JsonToken.START_ARRAY) {
                jsonReadContext = jsonReadContext.getParent();
            }
            try {
                jsonReadContext.setCurrentName(str);
            } catch (IOException e) {
                throw new RuntimeException(e);
            }
        }

        public String getText() {
            if (this._currToken == JsonToken.VALUE_STRING || this._currToken == JsonToken.FIELD_NAME) {
                Object _currentObject = _currentObject();
                if (_currentObject instanceof String) {
                    return (String) _currentObject;
                }
                return _currentObject == null ? null : _currentObject.toString();
            } else if (this._currToken == null) {
                return null;
            } else {
                switch (this._currToken) {
                    case VALUE_NUMBER_INT:
                    case VALUE_NUMBER_FLOAT:
                        Object _currentObject2 = _currentObject();
                        if (_currentObject2 != null) {
                            return _currentObject2.toString();
                        }
                        return null;
                    default:
                        return this._currToken.asString();
                }
            }
        }

        public char[] getTextCharacters() {
            String text = getText();
            if (text == null) {
                return null;
            }
            return text.toCharArray();
        }

        public int getTextLength() {
            String text = getText();
            if (text == null) {
                return 0;
            }
            return text.length();
        }

        public int getTextOffset() {
            return 0;
        }

        public boolean hasTextCharacters() {
            return false;
        }

        public BigInteger getBigIntegerValue() throws IOException {
            Number numberValue = getNumberValue();
            if (numberValue instanceof BigInteger) {
                return (BigInteger) numberValue;
            }
            if (getNumberType() == NumberType.BIG_DECIMAL) {
                return ((BigDecimal) numberValue).toBigInteger();
            }
            return BigInteger.valueOf(numberValue.longValue());
        }

        public BigDecimal getDecimalValue() throws IOException {
            Number numberValue = getNumberValue();
            if (numberValue instanceof BigDecimal) {
                return (BigDecimal) numberValue;
            }
            switch (getNumberType()) {
                case INT:
                case LONG:
                    return BigDecimal.valueOf(numberValue.longValue());
                case BIG_INTEGER:
                    return new BigDecimal((BigInteger) numberValue);
                default:
                    return BigDecimal.valueOf(numberValue.doubleValue());
            }
        }

        public double getDoubleValue() throws IOException {
            return getNumberValue().doubleValue();
        }

        public float getFloatValue() throws IOException {
            return getNumberValue().floatValue();
        }

        public int getIntValue() throws IOException {
            if (this._currToken == JsonToken.VALUE_NUMBER_INT) {
                return ((Number) _currentObject()).intValue();
            }
            return getNumberValue().intValue();
        }

        public long getLongValue() throws IOException {
            return getNumberValue().longValue();
        }

        public NumberType getNumberType() throws IOException {
            Number numberValue = getNumberValue();
            if (numberValue instanceof Integer) {
                return NumberType.INT;
            }
            if (numberValue instanceof Long) {
                return NumberType.LONG;
            }
            if (numberValue instanceof Double) {
                return NumberType.DOUBLE;
            }
            if (numberValue instanceof BigDecimal) {
                return NumberType.BIG_DECIMAL;
            }
            if (numberValue instanceof BigInteger) {
                return NumberType.BIG_INTEGER;
            }
            if (numberValue instanceof Float) {
                return NumberType.FLOAT;
            }
            if (numberValue instanceof Short) {
                return NumberType.INT;
            }
            return null;
        }

        public final Number getNumberValue() throws IOException {
            _checkIsNumber();
            Object _currentObject = _currentObject();
            if (_currentObject instanceof Number) {
                return (Number) _currentObject;
            }
            if (_currentObject instanceof String) {
                String str = (String) _currentObject;
                if (str.indexOf(46) >= 0) {
                    return Double.valueOf(Double.parseDouble(str));
                }
                return Long.valueOf(Long.parseLong(str));
            } else if (_currentObject == null) {
                return null;
            } else {
                throw new IllegalStateException("Internal error: entry should be a Number, but is of type " + _currentObject.getClass().getName());
            }
        }

        public Object getEmbeddedObject() {
            if (this._currToken == JsonToken.VALUE_EMBEDDED_OBJECT) {
                return _currentObject();
            }
            return null;
        }

        public byte[] getBinaryValue(Base64Variant base64Variant) throws IOException, JsonParseException {
            if (this._currToken == JsonToken.VALUE_EMBEDDED_OBJECT) {
                Object _currentObject = _currentObject();
                if (_currentObject instanceof byte[]) {
                    return (byte[]) _currentObject;
                }
            }
            if (this._currToken != JsonToken.VALUE_STRING) {
                throw _constructError("Current token (" + this._currToken + ") not VALUE_STRING (or VALUE_EMBEDDED_OBJECT with byte[]), can not access as binary");
            }
            String text = getText();
            if (text == null) {
                return null;
            }
            ByteArrayBuilder byteArrayBuilder = this._byteBuilder;
            if (byteArrayBuilder == null) {
                byteArrayBuilder = new ByteArrayBuilder(100);
                this._byteBuilder = byteArrayBuilder;
            } else {
                this._byteBuilder.reset();
            }
            _decodeBase64(text, byteArrayBuilder, base64Variant);
            return byteArrayBuilder.toByteArray();
        }

        public int readBinaryValue(Base64Variant base64Variant, OutputStream outputStream) throws IOException {
            byte[] binaryValue = getBinaryValue(base64Variant);
            if (binaryValue == null) {
                return 0;
            }
            outputStream.write(binaryValue, 0, binaryValue.length);
            return binaryValue.length;
        }

        public boolean canReadObjectId() {
            return this._hasNativeObjectIds;
        }

        public boolean canReadTypeId() {
            return this._hasNativeTypeIds;
        }

        public Object getTypeId() {
            return this._segment.findTypeId(this._segmentPtr);
        }

        public Object getObjectId() {
            return this._segment.findObjectId(this._segmentPtr);
        }

        /* access modifiers changed from: protected */
        public final Object _currentObject() {
            return this._segment.get(this._segmentPtr);
        }

        /* access modifiers changed from: protected */
        public final void _checkIsNumber() throws JsonParseException {
            if (this._currToken == null || !this._currToken.isNumeric()) {
                throw _constructError("Current token (" + this._currToken + ") not numeric, can not use numeric value accessors");
            }
        }

        /* access modifiers changed from: protected */
        public void _handleEOF() throws JsonParseException {
            _throwInternal();
        }
    }

    protected static final class Segment {
        public static final int TOKENS_PER_SEGMENT = 16;
        private static final JsonToken[] TOKEN_TYPES_BY_INDEX = new JsonToken[16];
        protected TreeMap<Integer, Object> _nativeIds;
        protected Segment _next;
        protected long _tokenTypes;
        protected final Object[] _tokens = new Object[16];

        static {
            JsonToken[] values = JsonToken.values();
            System.arraycopy(values, 1, TOKEN_TYPES_BY_INDEX, 1, Math.min(15, values.length - 1));
        }

        public JsonToken type(int i) {
            long j = this._tokenTypes;
            if (i > 0) {
                j >>= i << 2;
            }
            return TOKEN_TYPES_BY_INDEX[((int) j) & 15];
        }

        public int rawType(int i) {
            long j = this._tokenTypes;
            if (i > 0) {
                j >>= i << 2;
            }
            return ((int) j) & 15;
        }

        public Object get(int i) {
            return this._tokens[i];
        }

        public Segment next() {
            return this._next;
        }

        public boolean hasIds() {
            return this._nativeIds != null;
        }

        public Segment append(int i, JsonToken jsonToken) {
            if (i < 16) {
                set(i, jsonToken);
                return null;
            }
            this._next = new Segment();
            this._next.set(0, jsonToken);
            return this._next;
        }

        public Segment append(int i, JsonToken jsonToken, Object obj, Object obj2) {
            if (i < 16) {
                set(i, jsonToken, obj, obj2);
                return null;
            }
            this._next = new Segment();
            this._next.set(0, jsonToken, obj, obj2);
            return this._next;
        }

        public Segment append(int i, JsonToken jsonToken, Object obj) {
            if (i < 16) {
                set(i, jsonToken, obj);
                return null;
            }
            this._next = new Segment();
            this._next.set(0, jsonToken, obj);
            return this._next;
        }

        public Segment append(int i, JsonToken jsonToken, Object obj, Object obj2, Object obj3) {
            if (i < 16) {
                set(i, jsonToken, obj, obj2, obj3);
                return null;
            }
            this._next = new Segment();
            this._next.set(0, jsonToken, obj, obj2, obj3);
            return this._next;
        }

        public Segment appendRaw(int i, int i2, Object obj) {
            if (i < 16) {
                set(i, i2, obj);
                return null;
            }
            this._next = new Segment();
            this._next.set(0, i2, obj);
            return this._next;
        }

        public Segment appendRaw(int i, int i2, Object obj, Object obj2, Object obj3) {
            if (i < 16) {
                set(i, i2, obj, obj2, obj3);
                return null;
            }
            this._next = new Segment();
            this._next.set(0, i2, obj, obj2, obj3);
            return this._next;
        }

        private void set(int i, JsonToken jsonToken) {
            long ordinal = (long) jsonToken.ordinal();
            if (i > 0) {
                ordinal <<= i << 2;
            }
            this._tokenTypes = ordinal | this._tokenTypes;
        }

        private void set(int i, JsonToken jsonToken, Object obj, Object obj2) {
            long ordinal = (long) jsonToken.ordinal();
            if (i > 0) {
                ordinal <<= i << 2;
            }
            this._tokenTypes = ordinal | this._tokenTypes;
            assignNativeIds(i, obj, obj2);
        }

        private void set(int i, JsonToken jsonToken, Object obj) {
            this._tokens[i] = obj;
            long ordinal = (long) jsonToken.ordinal();
            if (i > 0) {
                ordinal <<= i << 2;
            }
            this._tokenTypes = ordinal | this._tokenTypes;
        }

        private void set(int i, JsonToken jsonToken, Object obj, Object obj2, Object obj3) {
            this._tokens[i] = obj;
            long ordinal = (long) jsonToken.ordinal();
            if (i > 0) {
                ordinal <<= i << 2;
            }
            this._tokenTypes = ordinal | this._tokenTypes;
            assignNativeIds(i, obj2, obj3);
        }

        private void set(int i, int i2, Object obj) {
            this._tokens[i] = obj;
            long j = (long) i2;
            if (i > 0) {
                j <<= i << 2;
            }
            this._tokenTypes = j | this._tokenTypes;
        }

        private void set(int i, int i2, Object obj, Object obj2, Object obj3) {
            this._tokens[i] = obj;
            long j = (long) i2;
            if (i > 0) {
                j <<= i << 2;
            }
            this._tokenTypes = j | this._tokenTypes;
            assignNativeIds(i, obj2, obj3);
        }

        private final void assignNativeIds(int i, Object obj, Object obj2) {
            if (this._nativeIds == null) {
                this._nativeIds = new TreeMap<>();
            }
            if (obj != null) {
                this._nativeIds.put(Integer.valueOf(_objectIdIndex(i)), obj);
            }
            if (obj2 != null) {
                this._nativeIds.put(Integer.valueOf(_typeIdIndex(i)), obj2);
            }
        }

        public Object findObjectId(int i) {
            if (this._nativeIds == null) {
                return null;
            }
            return this._nativeIds.get(Integer.valueOf(_objectIdIndex(i)));
        }

        public Object findTypeId(int i) {
            if (this._nativeIds == null) {
                return null;
            }
            return this._nativeIds.get(Integer.valueOf(_typeIdIndex(i)));
        }

        private final int _typeIdIndex(int i) {
            return i + i;
        }

        private final int _objectIdIndex(int i) {
            return i + i + 1;
        }
    }

    @Deprecated
    public TokenBuffer(ObjectCodec objectCodec) {
        this(objectCodec, false);
    }

    public TokenBuffer(ObjectCodec objectCodec, boolean z) {
        this._hasNativeId = false;
        this._objectCodec = objectCodec;
        this._generatorFeatures = DEFAULT_GENERATOR_FEATURES;
        this._writeContext = JsonWriteContext.createRootContext(null);
        Segment segment = new Segment();
        this._last = segment;
        this._first = segment;
        this._appendAt = 0;
        this._hasNativeTypeIds = z;
        this._hasNativeObjectIds = z;
        this._mayHaveNativeIds = this._hasNativeTypeIds | this._hasNativeObjectIds;
    }

    public TokenBuffer(JsonParser jsonParser) {
        this(jsonParser, (DeserializationContext) null);
    }

    public TokenBuffer(JsonParser jsonParser, DeserializationContext deserializationContext) {
        boolean z = false;
        this._hasNativeId = false;
        this._objectCodec = jsonParser.getCodec();
        this._generatorFeatures = DEFAULT_GENERATOR_FEATURES;
        this._writeContext = JsonWriteContext.createRootContext(null);
        Segment segment = new Segment();
        this._last = segment;
        this._first = segment;
        this._appendAt = 0;
        this._hasNativeTypeIds = jsonParser.canReadTypeId();
        this._hasNativeObjectIds = jsonParser.canReadObjectId();
        this._mayHaveNativeIds = this._hasNativeTypeIds | this._hasNativeObjectIds;
        if (deserializationContext != null) {
            z = deserializationContext.isEnabled(DeserializationFeature.USE_BIG_DECIMAL_FOR_FLOATS);
        }
        this._forceBigDecimal = z;
    }

    public TokenBuffer forceUseOfBigDecimal(boolean z) {
        this._forceBigDecimal = z;
        return this;
    }

    public Version version() {
        return PackageVersion.VERSION;
    }

    public JsonParser asParser() {
        return asParser(this._objectCodec);
    }

    public JsonParser asParser(ObjectCodec objectCodec) {
        return new Parser(this._first, objectCodec, this._hasNativeTypeIds, this._hasNativeObjectIds);
    }

    public JsonParser asParser(JsonParser jsonParser) {
        Parser parser = new Parser(this._first, jsonParser.getCodec(), this._hasNativeTypeIds, this._hasNativeObjectIds);
        parser.setLocation(jsonParser.getTokenLocation());
        return parser;
    }

    public JsonToken firstToken() {
        if (this._first != null) {
            return this._first.type(0);
        }
        return null;
    }

    public TokenBuffer append(TokenBuffer tokenBuffer) throws IOException {
        if (!this._hasNativeTypeIds) {
            this._hasNativeTypeIds = tokenBuffer.canWriteTypeId();
        }
        if (!this._hasNativeObjectIds) {
            this._hasNativeObjectIds = tokenBuffer.canWriteObjectId();
        }
        this._mayHaveNativeIds = this._hasNativeTypeIds | this._hasNativeObjectIds;
        JsonParser asParser = tokenBuffer.asParser();
        while (asParser.nextToken() != null) {
            copyCurrentStructure(asParser);
        }
        return this;
    }

    public void serialize(JsonGenerator jsonGenerator) throws IOException {
        boolean z;
        Segment segment = this._first;
        int i = -1;
        boolean z2 = this._mayHaveNativeIds;
        boolean z3 = z2 && segment.hasIds();
        while (true) {
            i++;
            if (i >= 16) {
                segment = segment.next();
                if (segment != null) {
                    z = z2 && segment.hasIds();
                    i = 0;
                } else {
                    return;
                }
            } else {
                z = z3;
            }
            JsonToken type = segment.type(i);
            if (type != null) {
                if (z) {
                    Object findObjectId = segment.findObjectId(i);
                    if (findObjectId != null) {
                        jsonGenerator.writeObjectId(findObjectId);
                    }
                    Object findTypeId = segment.findTypeId(i);
                    if (findTypeId != null) {
                        jsonGenerator.writeTypeId(findTypeId);
                    }
                }
                switch (type) {
                    case START_OBJECT:
                        jsonGenerator.writeStartObject();
                        break;
                    case END_OBJECT:
                        jsonGenerator.writeEndObject();
                        break;
                    case START_ARRAY:
                        jsonGenerator.writeStartArray();
                        break;
                    case END_ARRAY:
                        jsonGenerator.writeEndArray();
                        break;
                    case FIELD_NAME:
                        Object obj = segment.get(i);
                        if (!(obj instanceof SerializableString)) {
                            jsonGenerator.writeFieldName((String) obj);
                            break;
                        } else {
                            jsonGenerator.writeFieldName((SerializableString) obj);
                            break;
                        }
                    case VALUE_STRING:
                        Object obj2 = segment.get(i);
                        if (!(obj2 instanceof SerializableString)) {
                            jsonGenerator.writeString((String) obj2);
                            break;
                        } else {
                            jsonGenerator.writeString((SerializableString) obj2);
                            break;
                        }
                    case VALUE_NUMBER_INT:
                        Object obj3 = segment.get(i);
                        if (!(obj3 instanceof Integer)) {
                            if (!(obj3 instanceof BigInteger)) {
                                if (!(obj3 instanceof Long)) {
                                    if (!(obj3 instanceof Short)) {
                                        jsonGenerator.writeNumber(((Number) obj3).intValue());
                                        break;
                                    } else {
                                        jsonGenerator.writeNumber(((Short) obj3).shortValue());
                                        break;
                                    }
                                } else {
                                    jsonGenerator.writeNumber(((Long) obj3).longValue());
                                    break;
                                }
                            } else {
                                jsonGenerator.writeNumber((BigInteger) obj3);
                                break;
                            }
                        } else {
                            jsonGenerator.writeNumber(((Integer) obj3).intValue());
                            break;
                        }
                    case VALUE_NUMBER_FLOAT:
                        Object obj4 = segment.get(i);
                        if (obj4 instanceof Double) {
                            jsonGenerator.writeNumber(((Double) obj4).doubleValue());
                            break;
                        } else if (obj4 instanceof BigDecimal) {
                            jsonGenerator.writeNumber((BigDecimal) obj4);
                            break;
                        } else if (obj4 instanceof Float) {
                            jsonGenerator.writeNumber(((Float) obj4).floatValue());
                            break;
                        } else if (obj4 == null) {
                            jsonGenerator.writeNull();
                            break;
                        } else if (obj4 instanceof String) {
                            jsonGenerator.writeNumber((String) obj4);
                            break;
                        } else {
                            throw new JsonGenerationException(String.format("Unrecognized value type for VALUE_NUMBER_FLOAT: %s, can not serialize", new Object[]{obj4.getClass().getName()}), jsonGenerator);
                        }
                    case VALUE_TRUE:
                        jsonGenerator.writeBoolean(true);
                        break;
                    case VALUE_FALSE:
                        jsonGenerator.writeBoolean(false);
                        break;
                    case VALUE_NULL:
                        jsonGenerator.writeNull();
                        break;
                    case VALUE_EMBEDDED_OBJECT:
                        Object obj5 = segment.get(i);
                        if (!(obj5 instanceof RawValue)) {
                            jsonGenerator.writeObject(obj5);
                            break;
                        } else {
                            ((RawValue) obj5).serialize(jsonGenerator);
                            break;
                        }
                    default:
                        throw new RuntimeException("Internal error: should never end up through this code path");
                }
                z3 = z;
            } else {
                return;
            }
        }
    }

    public TokenBuffer deserialize(JsonParser jsonParser, DeserializationContext deserializationContext) throws IOException {
        JsonToken nextToken;
        if (jsonParser.getCurrentTokenId() != JsonToken.FIELD_NAME.mo9113id()) {
            copyCurrentStructure(jsonParser);
        } else {
            writeStartObject();
            do {
                copyCurrentStructure(jsonParser);
                nextToken = jsonParser.nextToken();
            } while (nextToken == JsonToken.FIELD_NAME);
            if (nextToken != JsonToken.END_OBJECT) {
                throw deserializationContext.mappingException("Expected END_OBJECT after copying contents of a JsonParser into TokenBuffer, got " + nextToken);
            }
            writeEndObject();
        }
        return this;
    }

    public String toString() {
        int i = 0;
        StringBuilder sb = new StringBuilder();
        sb.append("[TokenBuffer: ");
        JsonParser asParser = asParser();
        boolean z = this._hasNativeTypeIds || this._hasNativeObjectIds;
        while (true) {
            try {
                JsonToken nextToken = asParser.nextToken();
                if (nextToken == null) {
                    break;
                }
                if (z) {
                    _appendNativeIds(sb);
                }
                if (i < 100) {
                    if (i > 0) {
                        sb.append(", ");
                    }
                    sb.append(nextToken.toString());
                    if (nextToken == JsonToken.FIELD_NAME) {
                        sb.append('(');
                        sb.append(asParser.getCurrentName());
                        sb.append(')');
                    }
                }
                i++;
            } catch (IOException e) {
                throw new IllegalStateException(e);
            }
        }
        if (i >= 100) {
            sb.append(" ... (truncated ").append(i - 100).append(" entries)");
        }
        sb.append(']');
        return sb.toString();
    }

    private final void _appendNativeIds(StringBuilder sb) {
        Object findObjectId = this._last.findObjectId(this._appendAt - 1);
        if (findObjectId != null) {
            sb.append("[objectId=").append(String.valueOf(findObjectId)).append(']');
        }
        Object findTypeId = this._last.findTypeId(this._appendAt - 1);
        if (findTypeId != null) {
            sb.append("[typeId=").append(String.valueOf(findTypeId)).append(']');
        }
    }

    public JsonGenerator enable(Feature feature) {
        this._generatorFeatures |= feature.getMask();
        return this;
    }

    public JsonGenerator disable(Feature feature) {
        this._generatorFeatures &= feature.getMask() ^ -1;
        return this;
    }

    public boolean isEnabled(Feature feature) {
        return (this._generatorFeatures & feature.getMask()) != 0;
    }

    public int getFeatureMask() {
        return this._generatorFeatures;
    }

    @Deprecated
    public JsonGenerator setFeatureMask(int i) {
        this._generatorFeatures = i;
        return this;
    }

    public JsonGenerator overrideStdFeatures(int i, int i2) {
        this._generatorFeatures = (getFeatureMask() & (i2 ^ -1)) | (i & i2);
        return this;
    }

    public JsonGenerator useDefaultPrettyPrinter() {
        return this;
    }

    public JsonGenerator setCodec(ObjectCodec objectCodec) {
        this._objectCodec = objectCodec;
        return this;
    }

    public ObjectCodec getCodec() {
        return this._objectCodec;
    }

    public final JsonWriteContext getOutputContext() {
        return this._writeContext;
    }

    public boolean canWriteBinaryNatively() {
        return true;
    }

    public void flush() throws IOException {
    }

    public void close() throws IOException {
        this._closed = true;
    }

    public boolean isClosed() {
        return this._closed;
    }

    public final void writeStartArray() throws IOException {
        _append(JsonToken.START_ARRAY);
        this._writeContext = this._writeContext.createChildArrayContext();
    }

    public final void writeEndArray() throws IOException {
        _append(JsonToken.END_ARRAY);
        JsonWriteContext parent = this._writeContext.getParent();
        if (parent != null) {
            this._writeContext = parent;
        }
    }

    public final void writeStartObject() throws IOException {
        _append(JsonToken.START_OBJECT);
        this._writeContext = this._writeContext.createChildObjectContext();
    }

    public final void writeEndObject() throws IOException {
        _append(JsonToken.END_OBJECT);
        JsonWriteContext parent = this._writeContext.getParent();
        if (parent != null) {
            this._writeContext = parent;
        }
    }

    public final void writeFieldName(String str) throws IOException {
        _append(JsonToken.FIELD_NAME, str);
        this._writeContext.writeFieldName(str);
    }

    public void writeFieldName(SerializableString serializableString) throws IOException {
        _append(JsonToken.FIELD_NAME, serializableString);
        this._writeContext.writeFieldName(serializableString.getValue());
    }

    public void writeString(String str) throws IOException {
        if (str == null) {
            writeNull();
        } else {
            _appendValue(JsonToken.VALUE_STRING, str);
        }
    }

    public void writeString(char[] cArr, int i, int i2) throws IOException {
        writeString(new String(cArr, i, i2));
    }

    public void writeString(SerializableString serializableString) throws IOException {
        if (serializableString == null) {
            writeNull();
        } else {
            _appendValue(JsonToken.VALUE_STRING, serializableString);
        }
    }

    public void writeRawUTF8String(byte[] bArr, int i, int i2) throws IOException {
        _reportUnsupportedOperation();
    }

    public void writeUTF8String(byte[] bArr, int i, int i2) throws IOException {
        _reportUnsupportedOperation();
    }

    public void writeRaw(String str) throws IOException {
        _reportUnsupportedOperation();
    }

    public void writeRaw(String str, int i, int i2) throws IOException {
        _reportUnsupportedOperation();
    }

    public void writeRaw(SerializableString serializableString) throws IOException {
        _reportUnsupportedOperation();
    }

    public void writeRaw(char[] cArr, int i, int i2) throws IOException {
        _reportUnsupportedOperation();
    }

    public void writeRaw(char c) throws IOException {
        _reportUnsupportedOperation();
    }

    public void writeRawValue(String str) throws IOException {
        _appendValue(JsonToken.VALUE_EMBEDDED_OBJECT, new RawValue(str));
    }

    public void writeRawValue(String str, int i, int i2) throws IOException {
        if (i > 0 || i2 != str.length()) {
            str = str.substring(i, i + i2);
        }
        _appendValue(JsonToken.VALUE_EMBEDDED_OBJECT, new RawValue(str));
    }

    public void writeRawValue(char[] cArr, int i, int i2) throws IOException {
        _appendValue(JsonToken.VALUE_EMBEDDED_OBJECT, new String(cArr, i, i2));
    }

    public void writeNumber(short s) throws IOException {
        _appendValue(JsonToken.VALUE_NUMBER_INT, Short.valueOf(s));
    }

    public void writeNumber(int i) throws IOException {
        _appendValue(JsonToken.VALUE_NUMBER_INT, Integer.valueOf(i));
    }

    public void writeNumber(long j) throws IOException {
        _appendValue(JsonToken.VALUE_NUMBER_INT, Long.valueOf(j));
    }

    public void writeNumber(double d) throws IOException {
        _appendValue(JsonToken.VALUE_NUMBER_FLOAT, Double.valueOf(d));
    }

    public void writeNumber(float f) throws IOException {
        _appendValue(JsonToken.VALUE_NUMBER_FLOAT, Float.valueOf(f));
    }

    public void writeNumber(BigDecimal bigDecimal) throws IOException {
        if (bigDecimal == null) {
            writeNull();
        } else {
            _appendValue(JsonToken.VALUE_NUMBER_FLOAT, bigDecimal);
        }
    }

    public void writeNumber(BigInteger bigInteger) throws IOException {
        if (bigInteger == null) {
            writeNull();
        } else {
            _appendValue(JsonToken.VALUE_NUMBER_INT, bigInteger);
        }
    }

    public void writeNumber(String str) throws IOException {
        _appendValue(JsonToken.VALUE_NUMBER_FLOAT, str);
    }

    public void writeBoolean(boolean z) throws IOException {
        _appendValue(z ? JsonToken.VALUE_TRUE : JsonToken.VALUE_FALSE);
    }

    public void writeNull() throws IOException {
        _appendValue(JsonToken.VALUE_NULL);
    }

    public void writeObject(Object obj) throws IOException {
        if (obj == null) {
            writeNull();
        } else if (obj.getClass() == byte[].class || (obj instanceof RawValue)) {
            _appendValue(JsonToken.VALUE_EMBEDDED_OBJECT, obj);
        } else if (this._objectCodec == null) {
            _appendValue(JsonToken.VALUE_EMBEDDED_OBJECT, obj);
        } else {
            this._objectCodec.writeValue(this, obj);
        }
    }

    public void writeTree(TreeNode treeNode) throws IOException {
        if (treeNode == null) {
            writeNull();
        } else if (this._objectCodec == null) {
            _appendValue(JsonToken.VALUE_EMBEDDED_OBJECT, treeNode);
        } else {
            this._objectCodec.writeTree(this, treeNode);
        }
    }

    public void writeBinary(Base64Variant base64Variant, byte[] bArr, int i, int i2) throws IOException {
        byte[] bArr2 = new byte[i2];
        System.arraycopy(bArr, i, bArr2, 0, i2);
        writeObject(bArr2);
    }

    public int writeBinary(Base64Variant base64Variant, InputStream inputStream, int i) {
        throw new UnsupportedOperationException();
    }

    public boolean canWriteTypeId() {
        return this._hasNativeTypeIds;
    }

    public boolean canWriteObjectId() {
        return this._hasNativeObjectIds;
    }

    public void writeTypeId(Object obj) {
        this._typeId = obj;
        this._hasNativeId = true;
    }

    public void writeObjectId(Object obj) {
        this._objectId = obj;
        this._hasNativeId = true;
    }

    public void copyCurrentEvent(JsonParser jsonParser) throws IOException {
        if (this._mayHaveNativeIds) {
            _checkNativeIds(jsonParser);
        }
        switch (jsonParser.getCurrentToken()) {
            case START_OBJECT:
                writeStartObject();
                return;
            case END_OBJECT:
                writeEndObject();
                return;
            case START_ARRAY:
                writeStartArray();
                return;
            case END_ARRAY:
                writeEndArray();
                return;
            case FIELD_NAME:
                writeFieldName(jsonParser.getCurrentName());
                return;
            case VALUE_STRING:
                if (jsonParser.hasTextCharacters()) {
                    writeString(jsonParser.getTextCharacters(), jsonParser.getTextOffset(), jsonParser.getTextLength());
                    return;
                } else {
                    writeString(jsonParser.getText());
                    return;
                }
            case VALUE_NUMBER_INT:
                switch (jsonParser.getNumberType()) {
                    case INT:
                        writeNumber(jsonParser.getIntValue());
                        return;
                    case BIG_INTEGER:
                        writeNumber(jsonParser.getBigIntegerValue());
                        return;
                    default:
                        writeNumber(jsonParser.getLongValue());
                        return;
                }
            case VALUE_NUMBER_FLOAT:
                if (this._forceBigDecimal) {
                    writeNumber(jsonParser.getDecimalValue());
                    return;
                }
                switch (jsonParser.getNumberType()) {
                    case BIG_DECIMAL:
                        writeNumber(jsonParser.getDecimalValue());
                        return;
                    case FLOAT:
                        writeNumber(jsonParser.getFloatValue());
                        return;
                    default:
                        writeNumber(jsonParser.getDoubleValue());
                        return;
                }
            case VALUE_TRUE:
                writeBoolean(true);
                return;
            case VALUE_FALSE:
                writeBoolean(false);
                return;
            case VALUE_NULL:
                writeNull();
                return;
            case VALUE_EMBEDDED_OBJECT:
                writeObject(jsonParser.getEmbeddedObject());
                return;
            default:
                throw new RuntimeException("Internal error: should never end up through this code path");
        }
    }

    public void copyCurrentStructure(JsonParser jsonParser) throws IOException {
        JsonToken currentToken = jsonParser.getCurrentToken();
        if (currentToken == JsonToken.FIELD_NAME) {
            if (this._mayHaveNativeIds) {
                _checkNativeIds(jsonParser);
            }
            writeFieldName(jsonParser.getCurrentName());
            currentToken = jsonParser.nextToken();
        }
        if (this._mayHaveNativeIds) {
            _checkNativeIds(jsonParser);
        }
        switch (currentToken) {
            case START_OBJECT:
                writeStartObject();
                while (jsonParser.nextToken() != JsonToken.END_OBJECT) {
                    copyCurrentStructure(jsonParser);
                }
                writeEndObject();
                return;
            case START_ARRAY:
                writeStartArray();
                while (jsonParser.nextToken() != JsonToken.END_ARRAY) {
                    copyCurrentStructure(jsonParser);
                }
                writeEndArray();
                return;
            default:
                copyCurrentEvent(jsonParser);
                return;
        }
    }

    private final void _checkNativeIds(JsonParser jsonParser) throws IOException {
        Object typeId = jsonParser.getTypeId();
        this._typeId = typeId;
        if (typeId != null) {
            this._hasNativeId = true;
        }
        Object objectId = jsonParser.getObjectId();
        this._objectId = objectId;
        if (objectId != null) {
            this._hasNativeId = true;
        }
    }

    /* access modifiers changed from: protected */
    public final void _append(JsonToken jsonToken) {
        Segment append = this._hasNativeId ? this._last.append(this._appendAt, jsonToken, this._objectId, this._typeId) : this._last.append(this._appendAt, jsonToken);
        if (append == null) {
            this._appendAt++;
            return;
        }
        this._last = append;
        this._appendAt = 1;
    }

    /* access modifiers changed from: protected */
    public final void _append(JsonToken jsonToken, Object obj) {
        Segment append;
        if (this._hasNativeId) {
            append = this._last.append(this._appendAt, jsonToken, obj, this._objectId, this._typeId);
        } else {
            append = this._last.append(this._appendAt, jsonToken, obj);
        }
        if (append == null) {
            this._appendAt++;
            return;
        }
        this._last = append;
        this._appendAt = 1;
    }

    /* access modifiers changed from: protected */
    public final void _appendValue(JsonToken jsonToken) {
        this._writeContext.writeValue();
        Segment append = this._hasNativeId ? this._last.append(this._appendAt, jsonToken, this._objectId, this._typeId) : this._last.append(this._appendAt, jsonToken);
        if (append == null) {
            this._appendAt++;
            return;
        }
        this._last = append;
        this._appendAt = 1;
    }

    /* access modifiers changed from: protected */
    public final void _appendValue(JsonToken jsonToken, Object obj) {
        Segment append;
        this._writeContext.writeValue();
        if (this._hasNativeId) {
            append = this._last.append(this._appendAt, jsonToken, obj, this._objectId, this._typeId);
        } else {
            append = this._last.append(this._appendAt, jsonToken, obj);
        }
        if (append == null) {
            this._appendAt++;
            return;
        }
        this._last = append;
        this._appendAt = 1;
    }

    /* access modifiers changed from: protected */
    public final void _appendRaw(int i, Object obj) {
        Segment appendRaw;
        if (this._hasNativeId) {
            appendRaw = this._last.appendRaw(this._appendAt, i, obj, this._objectId, this._typeId);
        } else {
            appendRaw = this._last.appendRaw(this._appendAt, i, obj);
        }
        if (appendRaw == null) {
            this._appendAt++;
            return;
        }
        this._last = appendRaw;
        this._appendAt = 1;
    }

    /* access modifiers changed from: protected */
    public void _reportUnsupportedOperation() {
        throw new UnsupportedOperationException("Called operation not supported for TokenBuffer");
    }
}
