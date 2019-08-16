package com.fasterxml.jackson.core.json;

import com.facebook.internal.ServerProtocol;
import com.fasterxml.jackson.core.Base64Variant;
import com.fasterxml.jackson.core.JsonLocation;
import com.fasterxml.jackson.core.JsonParseException;
import com.fasterxml.jackson.core.JsonParser.Feature;
import com.fasterxml.jackson.core.JsonToken;
import com.fasterxml.jackson.core.ObjectCodec;
import com.fasterxml.jackson.core.SerializableString;
import com.fasterxml.jackson.core.base.ParserBase;
import com.fasterxml.jackson.core.p015io.CharTypes;
import com.fasterxml.jackson.core.p015io.IOContext;
import com.fasterxml.jackson.core.sym.ByteQuadsCanonicalizer;
import com.fasterxml.jackson.core.util.ByteArrayBuilder;
import com.github.droidfu.support.DisplaySupport;
import com.google.firebase.messaging.cpp.SerializedEventUnion;
import java.io.IOException;
import java.io.InputStream;
import java.io.OutputStream;
import java.util.Arrays;
import org.apache.commons.lang3.CharUtils;

public class UTF8StreamJsonParser extends ParserBase {
    static final byte BYTE_LF = 10;
    protected static final int[] _icLatin1 = CharTypes.getInputCodeLatin1();
    private static final int[] _icUTF8 = CharTypes.getInputCodeUtf8();
    protected boolean _bufferRecyclable;
    protected byte[] _inputBuffer;
    protected InputStream _inputStream;
    protected int _nameStartCol;
    protected int _nameStartOffset;
    protected int _nameStartRow;
    protected ObjectCodec _objectCodec;
    private int _quad1;
    protected int[] _quadBuffer = new int[16];
    protected final ByteQuadsCanonicalizer _symbols;
    protected boolean _tokenIncomplete;

    public UTF8StreamJsonParser(IOContext iOContext, int i, InputStream inputStream, ObjectCodec objectCodec, ByteQuadsCanonicalizer byteQuadsCanonicalizer, byte[] bArr, int i2, int i3, boolean z) {
        super(iOContext, i);
        this._inputStream = inputStream;
        this._objectCodec = objectCodec;
        this._symbols = byteQuadsCanonicalizer;
        this._inputBuffer = bArr;
        this._inputPtr = i2;
        this._inputEnd = i3;
        this._currInputRowStart = i2;
        this._currInputProcessed = (long) (-i2);
        this._bufferRecyclable = z;
    }

    public ObjectCodec getCodec() {
        return this._objectCodec;
    }

    public void setCodec(ObjectCodec objectCodec) {
        this._objectCodec = objectCodec;
    }

    public int releaseBuffered(OutputStream outputStream) throws IOException {
        int i = this._inputEnd - this._inputPtr;
        if (i < 1) {
            return 0;
        }
        outputStream.write(this._inputBuffer, this._inputPtr, i);
        return i;
    }

    public Object getInputSource() {
        return this._inputStream;
    }

    /* access modifiers changed from: protected */
    public final boolean loadMore() throws IOException {
        int i = this._inputEnd;
        this._currInputProcessed += (long) this._inputEnd;
        this._currInputRowStart -= this._inputEnd;
        this._nameStartOffset -= i;
        if (this._inputStream == null) {
            return false;
        }
        int length = this._inputBuffer.length;
        if (length == 0) {
            return false;
        }
        int read = this._inputStream.read(this._inputBuffer, 0, length);
        if (read > 0) {
            this._inputPtr = 0;
            this._inputEnd = read;
            return true;
        }
        _closeInput();
        if (read != 0) {
            return false;
        }
        throw new IOException("InputStream.read() returned 0 characters when trying to read " + this._inputBuffer.length + " bytes");
    }

    /* access modifiers changed from: protected */
    public final boolean _loadToHaveAtLeast(int i) throws IOException {
        if (this._inputStream == null) {
            return false;
        }
        int i2 = this._inputEnd - this._inputPtr;
        if (i2 <= 0 || this._inputPtr <= 0) {
            this._inputEnd = 0;
        } else {
            int i3 = this._inputPtr;
            this._currInputProcessed += (long) i3;
            this._currInputRowStart -= i3;
            this._nameStartOffset -= i3;
            System.arraycopy(this._inputBuffer, i3, this._inputBuffer, 0, i2);
            this._inputEnd = i2;
        }
        this._inputPtr = 0;
        while (this._inputEnd < i) {
            int read = this._inputStream.read(this._inputBuffer, this._inputEnd, this._inputBuffer.length - this._inputEnd);
            if (read < 1) {
                _closeInput();
                if (read != 0) {
                    return false;
                }
                throw new IOException("InputStream.read() returned 0 characters when trying to read " + i2 + " bytes");
            }
            this._inputEnd = read + this._inputEnd;
        }
        return true;
    }

    /* access modifiers changed from: protected */
    public void _closeInput() throws IOException {
        if (this._inputStream != null) {
            if (this._ioContext.isResourceManaged() || isEnabled(Feature.AUTO_CLOSE_SOURCE)) {
                this._inputStream.close();
            }
            this._inputStream = null;
        }
    }

    /* access modifiers changed from: protected */
    public void _releaseBuffers() throws IOException {
        super._releaseBuffers();
        this._symbols.release();
        if (this._bufferRecyclable) {
            byte[] bArr = this._inputBuffer;
            if (bArr != null) {
                this._inputBuffer = ByteArrayBuilder.NO_BYTES;
                this._ioContext.releaseReadIOBuffer(bArr);
            }
        }
    }

    public String getText() throws IOException {
        if (this._currToken != JsonToken.VALUE_STRING) {
            return _getText2(this._currToken);
        }
        if (!this._tokenIncomplete) {
            return this._textBuffer.contentsAsString();
        }
        this._tokenIncomplete = false;
        return _finishAndReturnString();
    }

    public String getValueAsString() throws IOException {
        if (this._currToken == JsonToken.VALUE_STRING) {
            if (!this._tokenIncomplete) {
                return this._textBuffer.contentsAsString();
            }
            this._tokenIncomplete = false;
            return _finishAndReturnString();
        } else if (this._currToken == JsonToken.FIELD_NAME) {
            return getCurrentName();
        } else {
            return super.getValueAsString(null);
        }
    }

    public String getValueAsString(String str) throws IOException {
        if (this._currToken == JsonToken.VALUE_STRING) {
            if (!this._tokenIncomplete) {
                return this._textBuffer.contentsAsString();
            }
            this._tokenIncomplete = false;
            return _finishAndReturnString();
        } else if (this._currToken == JsonToken.FIELD_NAME) {
            return getCurrentName();
        } else {
            return super.getValueAsString(str);
        }
    }

    public int getValueAsInt() throws IOException {
        JsonToken jsonToken = this._currToken;
        if (jsonToken != JsonToken.VALUE_NUMBER_INT && jsonToken != JsonToken.VALUE_NUMBER_FLOAT) {
            return super.getValueAsInt(0);
        }
        if ((this._numTypesValid & 1) == 0) {
            if (this._numTypesValid == 0) {
                return _parseIntValue();
            }
            if ((this._numTypesValid & 1) == 0) {
                convertNumberToInt();
            }
        }
        return this._numberInt;
    }

    public int getValueAsInt(int i) throws IOException {
        JsonToken jsonToken = this._currToken;
        if (jsonToken != JsonToken.VALUE_NUMBER_INT && jsonToken != JsonToken.VALUE_NUMBER_FLOAT) {
            return super.getValueAsInt(i);
        }
        if ((this._numTypesValid & 1) == 0) {
            if (this._numTypesValid == 0) {
                return _parseIntValue();
            }
            if ((this._numTypesValid & 1) == 0) {
                convertNumberToInt();
            }
        }
        return this._numberInt;
    }

    /* access modifiers changed from: protected */
    public final String _getText2(JsonToken jsonToken) {
        if (jsonToken == null) {
            return null;
        }
        switch (jsonToken.mo9113id()) {
            case 5:
                return this._parsingContext.getCurrentName();
            case 6:
            case 7:
            case 8:
                return this._textBuffer.contentsAsString();
            default:
                return jsonToken.asString();
        }
    }

    public char[] getTextCharacters() throws IOException {
        if (this._currToken == null) {
            return null;
        }
        switch (this._currToken.mo9113id()) {
            case 5:
                if (!this._nameCopied) {
                    String currentName = this._parsingContext.getCurrentName();
                    int length = currentName.length();
                    if (this._nameCopyBuffer == null) {
                        this._nameCopyBuffer = this._ioContext.allocNameCopyBuffer(length);
                    } else if (this._nameCopyBuffer.length < length) {
                        this._nameCopyBuffer = new char[length];
                    }
                    currentName.getChars(0, length, this._nameCopyBuffer, 0);
                    this._nameCopied = true;
                }
                return this._nameCopyBuffer;
            case 6:
                if (this._tokenIncomplete) {
                    this._tokenIncomplete = false;
                    _finishString();
                    break;
                }
                break;
            case 7:
            case 8:
                break;
            default:
                return this._currToken.asCharArray();
        }
        return this._textBuffer.getTextBuffer();
    }

    public int getTextLength() throws IOException {
        if (this._currToken == null) {
            return 0;
        }
        switch (this._currToken.mo9113id()) {
            case 5:
                return this._parsingContext.getCurrentName().length();
            case 6:
                if (this._tokenIncomplete) {
                    this._tokenIncomplete = false;
                    _finishString();
                    break;
                }
                break;
            case 7:
            case 8:
                break;
            default:
                return this._currToken.asCharArray().length;
        }
        return this._textBuffer.size();
    }

    public int getTextOffset() throws IOException {
        if (this._currToken == null) {
            return 0;
        }
        switch (this._currToken.mo9113id()) {
            case 6:
                if (this._tokenIncomplete) {
                    this._tokenIncomplete = false;
                    _finishString();
                    break;
                }
                break;
            case 7:
            case 8:
                break;
            default:
                return 0;
        }
        return this._textBuffer.getTextOffset();
    }

    public byte[] getBinaryValue(Base64Variant base64Variant) throws IOException {
        if (this._currToken != JsonToken.VALUE_STRING && (this._currToken != JsonToken.VALUE_EMBEDDED_OBJECT || this._binaryValue == null)) {
            _reportError("Current token (" + this._currToken + ") not VALUE_STRING or VALUE_EMBEDDED_OBJECT, can not access as binary");
        }
        if (this._tokenIncomplete) {
            try {
                this._binaryValue = _decodeBase64(base64Variant);
                this._tokenIncomplete = false;
            } catch (IllegalArgumentException e) {
                throw _constructError("Failed to decode VALUE_STRING as base64 (" + base64Variant + "): " + e.getMessage());
            }
        } else if (this._binaryValue == null) {
            ByteArrayBuilder _getByteArrayBuilder = _getByteArrayBuilder();
            _decodeBase64(getText(), _getByteArrayBuilder, base64Variant);
            this._binaryValue = _getByteArrayBuilder.toByteArray();
        }
        return this._binaryValue;
    }

    public int readBinaryValue(Base64Variant base64Variant, OutputStream outputStream) throws IOException {
        if (!this._tokenIncomplete || this._currToken != JsonToken.VALUE_STRING) {
            byte[] binaryValue = getBinaryValue(base64Variant);
            outputStream.write(binaryValue);
            return binaryValue.length;
        }
        byte[] allocBase64Buffer = this._ioContext.allocBase64Buffer();
        try {
            return _readBinary(base64Variant, outputStream, allocBase64Buffer);
        } finally {
            this._ioContext.releaseBase64Buffer(allocBase64Buffer);
        }
    }

    /* access modifiers changed from: protected */
    public int _readBinary(Base64Variant base64Variant, OutputStream outputStream, byte[] bArr) throws IOException {
        int i;
        int length = bArr.length - 3;
        int i2 = 0;
        int i3 = 0;
        while (true) {
            if (this._inputPtr >= this._inputEnd) {
                loadMoreGuaranteed();
            }
            byte[] bArr2 = this._inputBuffer;
            int i4 = this._inputPtr;
            this._inputPtr = i4 + 1;
            byte b = bArr2[i4] & 255;
            if (b > 32) {
                int decodeBase64Char = base64Variant.decodeBase64Char((int) b);
                if (decodeBase64Char < 0) {
                    if (b == 34) {
                        break;
                    }
                    decodeBase64Char = _decodeBase64Escape(base64Variant, (int) b, 0);
                    if (decodeBase64Char < 0) {
                        continue;
                    }
                }
                int i5 = decodeBase64Char;
                if (i3 > length) {
                    i2 += i3;
                    outputStream.write(bArr, 0, i3);
                    i = 0;
                } else {
                    i = i3;
                }
                if (this._inputPtr >= this._inputEnd) {
                    loadMoreGuaranteed();
                }
                byte[] bArr3 = this._inputBuffer;
                int i6 = this._inputPtr;
                this._inputPtr = i6 + 1;
                byte b2 = bArr3[i6] & 255;
                int decodeBase64Char2 = base64Variant.decodeBase64Char((int) b2);
                if (decodeBase64Char2 < 0) {
                    decodeBase64Char2 = _decodeBase64Escape(base64Variant, (int) b2, 1);
                }
                int i7 = (i5 << 6) | decodeBase64Char2;
                if (this._inputPtr >= this._inputEnd) {
                    loadMoreGuaranteed();
                }
                byte[] bArr4 = this._inputBuffer;
                int i8 = this._inputPtr;
                this._inputPtr = i8 + 1;
                byte b3 = bArr4[i8] & 255;
                int decodeBase64Char3 = base64Variant.decodeBase64Char((int) b3);
                if (decodeBase64Char3 < 0) {
                    if (decodeBase64Char3 != -2) {
                        if (b3 == 34 && !base64Variant.usesPadding()) {
                            i3 = i + 1;
                            bArr[i] = (byte) (i7 >> 4);
                            break;
                        }
                        decodeBase64Char3 = _decodeBase64Escape(base64Variant, (int) b3, 2);
                    }
                    if (decodeBase64Char3 == -2) {
                        if (this._inputPtr >= this._inputEnd) {
                            loadMoreGuaranteed();
                        }
                        byte[] bArr5 = this._inputBuffer;
                        int i9 = this._inputPtr;
                        this._inputPtr = i9 + 1;
                        byte b4 = bArr5[i9] & 255;
                        if (!base64Variant.usesPaddingChar((int) b4)) {
                            throw reportInvalidBase64Char(base64Variant, b4, 3, "expected padding character '" + base64Variant.getPaddingChar() + "'");
                        }
                        i3 = i + 1;
                        bArr[i] = (byte) (i7 >> 4);
                    }
                }
                int i10 = (i7 << 6) | decodeBase64Char3;
                if (this._inputPtr >= this._inputEnd) {
                    loadMoreGuaranteed();
                }
                byte[] bArr6 = this._inputBuffer;
                int i11 = this._inputPtr;
                this._inputPtr = i11 + 1;
                byte b5 = bArr6[i11] & 255;
                int decodeBase64Char4 = base64Variant.decodeBase64Char((int) b5);
                if (decodeBase64Char4 < 0) {
                    if (decodeBase64Char4 != -2) {
                        if (b5 == 34 && !base64Variant.usesPadding()) {
                            int i12 = i10 >> 2;
                            int i13 = i + 1;
                            bArr[i] = (byte) (i12 >> 8);
                            i3 = i13 + 1;
                            bArr[i13] = (byte) i12;
                            break;
                        }
                        decodeBase64Char4 = _decodeBase64Escape(base64Variant, (int) b5, 3);
                    }
                    if (decodeBase64Char4 == -2) {
                        int i14 = i10 >> 2;
                        int i15 = i + 1;
                        bArr[i] = (byte) (i14 >> 8);
                        i3 = i15 + 1;
                        bArr[i15] = (byte) i14;
                    }
                }
                int i16 = (i10 << 6) | decodeBase64Char4;
                int i17 = i + 1;
                bArr[i] = (byte) (i16 >> 16);
                int i18 = i17 + 1;
                bArr[i17] = (byte) (i16 >> 8);
                i3 = i18 + 1;
                bArr[i18] = (byte) i16;
            }
        }
        this._tokenIncomplete = false;
        if (i3 <= 0) {
            return i2;
        }
        int i19 = i2 + i3;
        outputStream.write(bArr, 0, i3);
        return i19;
    }

    public JsonToken nextToken() throws IOException {
        JsonToken jsonToken;
        if (this._currToken == JsonToken.FIELD_NAME) {
            return _nextAfterName();
        }
        this._numTypesValid = 0;
        if (this._tokenIncomplete) {
            _skipString();
        }
        int _skipWSOrEnd = _skipWSOrEnd();
        if (_skipWSOrEnd < 0) {
            close();
            this._currToken = null;
            return null;
        }
        this._binaryValue = null;
        if (_skipWSOrEnd == 93) {
            _updateLocation();
            if (!this._parsingContext.inArray()) {
                _reportMismatchedEndMarker(_skipWSOrEnd, '}');
            }
            this._parsingContext = this._parsingContext.clearAndGetParent();
            JsonToken jsonToken2 = JsonToken.END_ARRAY;
            this._currToken = jsonToken2;
            return jsonToken2;
        } else if (_skipWSOrEnd == 125) {
            _updateLocation();
            if (!this._parsingContext.inObject()) {
                _reportMismatchedEndMarker(_skipWSOrEnd, ']');
            }
            this._parsingContext = this._parsingContext.clearAndGetParent();
            JsonToken jsonToken3 = JsonToken.END_OBJECT;
            this._currToken = jsonToken3;
            return jsonToken3;
        } else {
            if (this._parsingContext.expectComma()) {
                if (_skipWSOrEnd != 44) {
                    _reportUnexpectedChar(_skipWSOrEnd, "was expecting comma to separate " + this._parsingContext.getTypeDesc() + " entries");
                }
                _skipWSOrEnd = _skipWS();
            }
            if (!this._parsingContext.inObject()) {
                _updateLocation();
                return _nextTokenNotInObject(_skipWSOrEnd);
            }
            _updateNameLocation();
            this._parsingContext.setCurrentName(_parseName(_skipWSOrEnd));
            this._currToken = JsonToken.FIELD_NAME;
            int _skipColon = _skipColon();
            _updateLocation();
            if (_skipColon == 34) {
                this._tokenIncomplete = true;
                this._nextToken = JsonToken.VALUE_STRING;
                return this._currToken;
            }
            switch (_skipColon) {
                case 45:
                    jsonToken = _parseNegNumber();
                    break;
                case 48:
                case 49:
                case 50:
                case 51:
                case 52:
                case 53:
                case 54:
                case 55:
                case 56:
                case 57:
                    jsonToken = _parsePosNumber(_skipColon);
                    break;
                case 91:
                    jsonToken = JsonToken.START_ARRAY;
                    break;
                case 102:
                    _matchToken("false", 1);
                    jsonToken = JsonToken.VALUE_FALSE;
                    break;
                case 110:
                    _matchToken("null", 1);
                    jsonToken = JsonToken.VALUE_NULL;
                    break;
                case 116:
                    _matchToken(ServerProtocol.DIALOG_RETURN_SCOPES_TRUE, 1);
                    jsonToken = JsonToken.VALUE_TRUE;
                    break;
                case 123:
                    jsonToken = JsonToken.START_OBJECT;
                    break;
                default:
                    jsonToken = _handleUnexpectedValue(_skipColon);
                    break;
            }
            this._nextToken = jsonToken;
            return this._currToken;
        }
    }

    private final JsonToken _nextTokenNotInObject(int i) throws IOException {
        if (i == 34) {
            this._tokenIncomplete = true;
            JsonToken jsonToken = JsonToken.VALUE_STRING;
            this._currToken = jsonToken;
            return jsonToken;
        }
        switch (i) {
            case 45:
                JsonToken _parseNegNumber = _parseNegNumber();
                this._currToken = _parseNegNumber;
                return _parseNegNumber;
            case 48:
            case 49:
            case 50:
            case 51:
            case 52:
            case 53:
            case 54:
            case 55:
            case 56:
            case 57:
                JsonToken _parsePosNumber = _parsePosNumber(i);
                this._currToken = _parsePosNumber;
                return _parsePosNumber;
            case 91:
                this._parsingContext = this._parsingContext.createChildArrayContext(this._tokenInputRow, this._tokenInputCol);
                JsonToken jsonToken2 = JsonToken.START_ARRAY;
                this._currToken = jsonToken2;
                return jsonToken2;
            case 102:
                _matchToken("false", 1);
                JsonToken jsonToken3 = JsonToken.VALUE_FALSE;
                this._currToken = jsonToken3;
                return jsonToken3;
            case 110:
                _matchToken("null", 1);
                JsonToken jsonToken4 = JsonToken.VALUE_NULL;
                this._currToken = jsonToken4;
                return jsonToken4;
            case 116:
                _matchToken(ServerProtocol.DIALOG_RETURN_SCOPES_TRUE, 1);
                JsonToken jsonToken5 = JsonToken.VALUE_TRUE;
                this._currToken = jsonToken5;
                return jsonToken5;
            case 123:
                this._parsingContext = this._parsingContext.createChildObjectContext(this._tokenInputRow, this._tokenInputCol);
                JsonToken jsonToken6 = JsonToken.START_OBJECT;
                this._currToken = jsonToken6;
                return jsonToken6;
            default:
                JsonToken _handleUnexpectedValue = _handleUnexpectedValue(i);
                this._currToken = _handleUnexpectedValue;
                return _handleUnexpectedValue;
        }
    }

    private final JsonToken _nextAfterName() {
        this._nameCopied = false;
        JsonToken jsonToken = this._nextToken;
        this._nextToken = null;
        if (jsonToken == JsonToken.START_ARRAY) {
            this._parsingContext = this._parsingContext.createChildArrayContext(this._tokenInputRow, this._tokenInputCol);
        } else if (jsonToken == JsonToken.START_OBJECT) {
            this._parsingContext = this._parsingContext.createChildObjectContext(this._tokenInputRow, this._tokenInputCol);
        }
        this._currToken = jsonToken;
        return jsonToken;
    }

    public boolean nextFieldName(SerializableString serializableString) throws IOException {
        this._numTypesValid = 0;
        if (this._currToken == JsonToken.FIELD_NAME) {
            _nextAfterName();
            return false;
        }
        if (this._tokenIncomplete) {
            _skipString();
        }
        int _skipWSOrEnd = _skipWSOrEnd();
        if (_skipWSOrEnd < 0) {
            close();
            this._currToken = null;
            return false;
        }
        this._binaryValue = null;
        if (_skipWSOrEnd == 93) {
            _updateLocation();
            if (!this._parsingContext.inArray()) {
                _reportMismatchedEndMarker(_skipWSOrEnd, '}');
            }
            this._parsingContext = this._parsingContext.clearAndGetParent();
            this._currToken = JsonToken.END_ARRAY;
            return false;
        } else if (_skipWSOrEnd == 125) {
            _updateLocation();
            if (!this._parsingContext.inObject()) {
                _reportMismatchedEndMarker(_skipWSOrEnd, ']');
            }
            this._parsingContext = this._parsingContext.clearAndGetParent();
            this._currToken = JsonToken.END_OBJECT;
            return false;
        } else {
            if (this._parsingContext.expectComma()) {
                if (_skipWSOrEnd != 44) {
                    _reportUnexpectedChar(_skipWSOrEnd, "was expecting comma to separate " + this._parsingContext.getTypeDesc() + " entries");
                }
                _skipWSOrEnd = _skipWS();
            }
            if (!this._parsingContext.inObject()) {
                _updateLocation();
                _nextTokenNotInObject(_skipWSOrEnd);
                return false;
            }
            _updateNameLocation();
            if (_skipWSOrEnd == 34) {
                byte[] asQuotedUTF8 = serializableString.asQuotedUTF8();
                int length = asQuotedUTF8.length;
                if (this._inputPtr + length + 4 < this._inputEnd) {
                    int i = this._inputPtr + length;
                    if (this._inputBuffer[i] == 34) {
                        int i2 = this._inputPtr;
                        int i3 = 0;
                        while (i2 != i) {
                            if (asQuotedUTF8[i3] == this._inputBuffer[i2]) {
                                i3++;
                                i2++;
                            }
                        }
                        this._parsingContext.setCurrentName(serializableString.getValue());
                        _isNextTokenNameYes(_skipColonFast(i2 + 1));
                        return true;
                    }
                }
            }
            return _isNextTokenNameMaybe(_skipWSOrEnd, serializableString);
        }
    }

    public String nextFieldName() throws IOException {
        JsonToken jsonToken;
        this._numTypesValid = 0;
        if (this._currToken == JsonToken.FIELD_NAME) {
            _nextAfterName();
            return null;
        }
        if (this._tokenIncomplete) {
            _skipString();
        }
        int _skipWSOrEnd = _skipWSOrEnd();
        if (_skipWSOrEnd < 0) {
            close();
            this._currToken = null;
            return null;
        }
        this._binaryValue = null;
        if (_skipWSOrEnd == 93) {
            _updateLocation();
            if (!this._parsingContext.inArray()) {
                _reportMismatchedEndMarker(_skipWSOrEnd, '}');
            }
            this._parsingContext = this._parsingContext.clearAndGetParent();
            this._currToken = JsonToken.END_ARRAY;
            return null;
        } else if (_skipWSOrEnd == 125) {
            _updateLocation();
            if (!this._parsingContext.inObject()) {
                _reportMismatchedEndMarker(_skipWSOrEnd, ']');
            }
            this._parsingContext = this._parsingContext.clearAndGetParent();
            this._currToken = JsonToken.END_OBJECT;
            return null;
        } else {
            if (this._parsingContext.expectComma()) {
                if (_skipWSOrEnd != 44) {
                    _reportUnexpectedChar(_skipWSOrEnd, "was expecting comma to separate " + this._parsingContext.getTypeDesc() + " entries");
                }
                _skipWSOrEnd = _skipWS();
            }
            if (!this._parsingContext.inObject()) {
                _updateLocation();
                _nextTokenNotInObject(_skipWSOrEnd);
                return null;
            }
            _updateNameLocation();
            String _parseName = _parseName(_skipWSOrEnd);
            this._parsingContext.setCurrentName(_parseName);
            this._currToken = JsonToken.FIELD_NAME;
            int _skipColon = _skipColon();
            _updateLocation();
            if (_skipColon == 34) {
                this._tokenIncomplete = true;
                this._nextToken = JsonToken.VALUE_STRING;
                return _parseName;
            }
            switch (_skipColon) {
                case 45:
                    jsonToken = _parseNegNumber();
                    break;
                case 48:
                case 49:
                case 50:
                case 51:
                case 52:
                case 53:
                case 54:
                case 55:
                case 56:
                case 57:
                    jsonToken = _parsePosNumber(_skipColon);
                    break;
                case 91:
                    jsonToken = JsonToken.START_ARRAY;
                    break;
                case 102:
                    _matchToken("false", 1);
                    jsonToken = JsonToken.VALUE_FALSE;
                    break;
                case 110:
                    _matchToken("null", 1);
                    jsonToken = JsonToken.VALUE_NULL;
                    break;
                case 116:
                    _matchToken(ServerProtocol.DIALOG_RETURN_SCOPES_TRUE, 1);
                    jsonToken = JsonToken.VALUE_TRUE;
                    break;
                case 123:
                    jsonToken = JsonToken.START_OBJECT;
                    break;
                default:
                    jsonToken = _handleUnexpectedValue(_skipColon);
                    break;
            }
            this._nextToken = jsonToken;
            return _parseName;
        }
    }

    private final int _skipColonFast(int i) throws IOException {
        int i2 = i + 1;
        byte b = this._inputBuffer[i];
        if (b == 58) {
            int i3 = i2 + 1;
            byte b2 = this._inputBuffer[i2];
            if (b2 > 32) {
                if (!(b2 == 47 || b2 == 35)) {
                    this._inputPtr = i3;
                    return b2;
                }
            } else if (b2 == 32 || b2 == 9) {
                int i4 = i3 + 1;
                byte b3 = this._inputBuffer[i3];
                if (b3 <= 32 || b3 == 47 || b3 == 35) {
                    i3 = i4;
                } else {
                    this._inputPtr = i4;
                    return b3;
                }
            }
            this._inputPtr = i3 - 1;
            return _skipColon2(true);
        }
        if (b == 32 || b == 9) {
            int i5 = i2 + 1;
            b = this._inputBuffer[i2];
            i2 = i5;
        }
        if (b == 58) {
            int i6 = i2 + 1;
            byte b4 = this._inputBuffer[i2];
            if (b4 > 32) {
                if (!(b4 == 47 || b4 == 35)) {
                    this._inputPtr = i6;
                    return b4;
                }
            } else if (b4 == 32 || b4 == 9) {
                int i7 = i6 + 1;
                byte b5 = this._inputBuffer[i6];
                if (b5 <= 32 || b5 == 47 || b5 == 35) {
                    i6 = i7;
                } else {
                    this._inputPtr = i7;
                    return b5;
                }
            }
            this._inputPtr = i6 - 1;
            return _skipColon2(true);
        }
        this._inputPtr = i2 - 1;
        return _skipColon2(false);
    }

    private final void _isNextTokenNameYes(int i) throws IOException {
        this._currToken = JsonToken.FIELD_NAME;
        _updateLocation();
        switch (i) {
            case 34:
                this._tokenIncomplete = true;
                this._nextToken = JsonToken.VALUE_STRING;
                return;
            case 45:
                this._nextToken = _parseNegNumber();
                return;
            case 48:
            case 49:
            case 50:
            case 51:
            case 52:
            case 53:
            case 54:
            case 55:
            case 56:
            case 57:
                this._nextToken = _parsePosNumber(i);
                return;
            case 91:
                this._nextToken = JsonToken.START_ARRAY;
                return;
            case 102:
                _matchToken("false", 1);
                this._nextToken = JsonToken.VALUE_FALSE;
                return;
            case 110:
                _matchToken("null", 1);
                this._nextToken = JsonToken.VALUE_NULL;
                return;
            case 116:
                _matchToken(ServerProtocol.DIALOG_RETURN_SCOPES_TRUE, 1);
                this._nextToken = JsonToken.VALUE_TRUE;
                return;
            case 123:
                this._nextToken = JsonToken.START_OBJECT;
                return;
            default:
                this._nextToken = _handleUnexpectedValue(i);
                return;
        }
    }

    private final boolean _isNextTokenNameMaybe(int i, SerializableString serializableString) throws IOException {
        JsonToken _parsePosNumber;
        String _parseName = _parseName(i);
        this._parsingContext.setCurrentName(_parseName);
        boolean equals = _parseName.equals(serializableString.getValue());
        this._currToken = JsonToken.FIELD_NAME;
        int _skipColon = _skipColon();
        _updateLocation();
        if (_skipColon == 34) {
            this._tokenIncomplete = true;
            this._nextToken = JsonToken.VALUE_STRING;
            return equals;
        }
        switch (_skipColon) {
            case 45:
                _parsePosNumber = _parseNegNumber();
                break;
            case 48:
            case 49:
            case 50:
            case 51:
            case 52:
            case 53:
            case 54:
            case 55:
            case 56:
            case 57:
                _parsePosNumber = _parsePosNumber(_skipColon);
                break;
            case 91:
                _parsePosNumber = JsonToken.START_ARRAY;
                break;
            case 102:
                _matchToken("false", 1);
                _parsePosNumber = JsonToken.VALUE_FALSE;
                break;
            case 110:
                _matchToken("null", 1);
                _parsePosNumber = JsonToken.VALUE_NULL;
                break;
            case 116:
                _matchToken(ServerProtocol.DIALOG_RETURN_SCOPES_TRUE, 1);
                _parsePosNumber = JsonToken.VALUE_TRUE;
                break;
            case 123:
                _parsePosNumber = JsonToken.START_OBJECT;
                break;
            default:
                _parsePosNumber = _handleUnexpectedValue(_skipColon);
                break;
        }
        this._nextToken = _parsePosNumber;
        return equals;
    }

    public String nextTextValue() throws IOException {
        if (this._currToken == JsonToken.FIELD_NAME) {
            this._nameCopied = false;
            JsonToken jsonToken = this._nextToken;
            this._nextToken = null;
            this._currToken = jsonToken;
            if (jsonToken == JsonToken.VALUE_STRING) {
                if (!this._tokenIncomplete) {
                    return this._textBuffer.contentsAsString();
                }
                this._tokenIncomplete = false;
                return _finishAndReturnString();
            } else if (jsonToken == JsonToken.START_ARRAY) {
                this._parsingContext = this._parsingContext.createChildArrayContext(this._tokenInputRow, this._tokenInputCol);
                return null;
            } else if (jsonToken != JsonToken.START_OBJECT) {
                return null;
            } else {
                this._parsingContext = this._parsingContext.createChildObjectContext(this._tokenInputRow, this._tokenInputCol);
                return null;
            }
        } else if (nextToken() == JsonToken.VALUE_STRING) {
            return getText();
        } else {
            return null;
        }
    }

    public int nextIntValue(int i) throws IOException {
        if (this._currToken != JsonToken.FIELD_NAME) {
            return nextToken() == JsonToken.VALUE_NUMBER_INT ? getIntValue() : i;
        }
        this._nameCopied = false;
        JsonToken jsonToken = this._nextToken;
        this._nextToken = null;
        this._currToken = jsonToken;
        if (jsonToken == JsonToken.VALUE_NUMBER_INT) {
            return getIntValue();
        }
        if (jsonToken == JsonToken.START_ARRAY) {
            this._parsingContext = this._parsingContext.createChildArrayContext(this._tokenInputRow, this._tokenInputCol);
            return i;
        } else if (jsonToken != JsonToken.START_OBJECT) {
            return i;
        } else {
            this._parsingContext = this._parsingContext.createChildObjectContext(this._tokenInputRow, this._tokenInputCol);
            return i;
        }
    }

    public long nextLongValue(long j) throws IOException {
        if (this._currToken != JsonToken.FIELD_NAME) {
            return nextToken() == JsonToken.VALUE_NUMBER_INT ? getLongValue() : j;
        }
        this._nameCopied = false;
        JsonToken jsonToken = this._nextToken;
        this._nextToken = null;
        this._currToken = jsonToken;
        if (jsonToken == JsonToken.VALUE_NUMBER_INT) {
            return getLongValue();
        }
        if (jsonToken == JsonToken.START_ARRAY) {
            this._parsingContext = this._parsingContext.createChildArrayContext(this._tokenInputRow, this._tokenInputCol);
            return j;
        } else if (jsonToken != JsonToken.START_OBJECT) {
            return j;
        } else {
            this._parsingContext = this._parsingContext.createChildObjectContext(this._tokenInputRow, this._tokenInputCol);
            return j;
        }
    }

    public Boolean nextBooleanValue() throws IOException {
        if (this._currToken == JsonToken.FIELD_NAME) {
            this._nameCopied = false;
            JsonToken jsonToken = this._nextToken;
            this._nextToken = null;
            this._currToken = jsonToken;
            if (jsonToken == JsonToken.VALUE_TRUE) {
                return Boolean.TRUE;
            }
            if (jsonToken == JsonToken.VALUE_FALSE) {
                return Boolean.FALSE;
            }
            if (jsonToken == JsonToken.START_ARRAY) {
                this._parsingContext = this._parsingContext.createChildArrayContext(this._tokenInputRow, this._tokenInputCol);
                return null;
            } else if (jsonToken != JsonToken.START_OBJECT) {
                return null;
            } else {
                this._parsingContext = this._parsingContext.createChildObjectContext(this._tokenInputRow, this._tokenInputCol);
                return null;
            }
        } else {
            JsonToken nextToken = nextToken();
            if (nextToken == JsonToken.VALUE_TRUE) {
                return Boolean.TRUE;
            }
            if (nextToken == JsonToken.VALUE_FALSE) {
                return Boolean.FALSE;
            }
            return null;
        }
    }

    /* access modifiers changed from: protected */
    public JsonToken _parsePosNumber(int i) throws IOException {
        int i2;
        int i3;
        char[] emptyAndGetCurrentSegment = this._textBuffer.emptyAndGetCurrentSegment();
        if (i == 48) {
            i = _verifyNoLeadingZeroes();
        }
        emptyAndGetCurrentSegment[0] = (char) i;
        int length = (this._inputPtr + emptyAndGetCurrentSegment.length) - 1;
        if (length > this._inputEnd) {
            length = this._inputEnd;
            i3 = 1;
            i2 = 1;
        } else {
            i3 = 1;
            i2 = 1;
        }
        while (this._inputPtr < length) {
            byte[] bArr = this._inputBuffer;
            int i4 = this._inputPtr;
            this._inputPtr = i4 + 1;
            byte b = bArr[i4] & 255;
            if (b >= 48 && b <= 57) {
                i2++;
                int i5 = i3 + 1;
                emptyAndGetCurrentSegment[i3] = (char) b;
                i3 = i5;
            } else if (b == 46 || b == 101 || b == 69) {
                return _parseFloat(emptyAndGetCurrentSegment, i3, b, false, i2);
            } else {
                this._inputPtr--;
                this._textBuffer.setCurrentLength(i3);
                if (this._parsingContext.inRoot()) {
                    _verifyRootSpace(b);
                }
                return resetInt(false, i2);
            }
        }
        return _parseNumber2(emptyAndGetCurrentSegment, i3, false, i2);
    }

    /* access modifiers changed from: protected */
    public JsonToken _parseNegNumber() throws IOException {
        int i;
        char[] emptyAndGetCurrentSegment = this._textBuffer.emptyAndGetCurrentSegment();
        emptyAndGetCurrentSegment[0] = '-';
        if (this._inputPtr >= this._inputEnd) {
            loadMoreGuaranteed();
        }
        byte[] bArr = this._inputBuffer;
        int i2 = this._inputPtr;
        this._inputPtr = i2 + 1;
        int i3 = bArr[i2] & 255;
        if (i3 < 48 || i3 > 57) {
            return _handleInvalidNumberStart(i3, true);
        }
        if (i3 == 48) {
            i3 = _verifyNoLeadingZeroes();
        }
        int i4 = 2;
        emptyAndGetCurrentSegment[1] = (char) i3;
        int length = (this._inputPtr + emptyAndGetCurrentSegment.length) - 2;
        if (length > this._inputEnd) {
            length = this._inputEnd;
            i = 1;
        } else {
            i = 1;
        }
        while (this._inputPtr < length) {
            byte[] bArr2 = this._inputBuffer;
            int i5 = this._inputPtr;
            this._inputPtr = i5 + 1;
            byte b = bArr2[i5] & 255;
            if (b >= 48 && b <= 57) {
                i++;
                int i6 = i4 + 1;
                emptyAndGetCurrentSegment[i4] = (char) b;
                i4 = i6;
            } else if (b == 46 || b == 101 || b == 69) {
                return _parseFloat(emptyAndGetCurrentSegment, i4, b, true, i);
            } else {
                this._inputPtr--;
                this._textBuffer.setCurrentLength(i4);
                if (this._parsingContext.inRoot()) {
                    _verifyRootSpace(b);
                }
                return resetInt(true, i);
            }
        }
        return _parseNumber2(emptyAndGetCurrentSegment, i4, true, i);
    }

    private final JsonToken _parseNumber2(char[] cArr, int i, boolean z, int i2) throws IOException {
        byte b;
        int i3 = i2;
        int i4 = i;
        char[] cArr2 = cArr;
        while (true) {
            if (this._inputPtr < this._inputEnd || loadMore()) {
                byte[] bArr = this._inputBuffer;
                int i5 = this._inputPtr;
                this._inputPtr = i5 + 1;
                b = bArr[i5] & 255;
                if (b <= 57 && b >= 48) {
                    if (i4 >= cArr2.length) {
                        cArr2 = this._textBuffer.finishCurrentSegment();
                        i4 = 0;
                    }
                    int i6 = i4;
                    i4 = i6 + 1;
                    cArr2[i6] = (char) b;
                    i3++;
                }
            } else {
                this._textBuffer.setCurrentLength(i4);
                return resetInt(z, i3);
            }
        }
        if (b == 46 || b == 101 || b == 69) {
            return _parseFloat(cArr2, i4, b, z, i3);
        }
        this._inputPtr--;
        this._textBuffer.setCurrentLength(i4);
        if (this._parsingContext.inRoot()) {
            byte[] bArr2 = this._inputBuffer;
            int i7 = this._inputPtr;
            this._inputPtr = i7 + 1;
            _verifyRootSpace(bArr2[i7] & 255);
        }
        return resetInt(z, i3);
    }

    private final int _verifyNoLeadingZeroes() throws IOException {
        if (this._inputPtr >= this._inputEnd && !loadMore()) {
            return 48;
        }
        byte b = this._inputBuffer[this._inputPtr] & 255;
        if (b < 48 || b > 57) {
            return 48;
        }
        if (!isEnabled(Feature.ALLOW_NUMERIC_LEADING_ZEROS)) {
            reportInvalidNumber("Leading zeroes not allowed");
        }
        this._inputPtr++;
        if (b != 48) {
            return b;
        }
        do {
            if (this._inputPtr >= this._inputEnd && !loadMore()) {
                return b;
            }
            b = this._inputBuffer[this._inputPtr] & 255;
            if (b < 48 || b > 57) {
                return 48;
            }
            this._inputPtr++;
        } while (b == 48);
        return b;
    }

    private final JsonToken _parseFloat(char[] cArr, int i, int i2, boolean z, int i3) throws IOException {
        int i4;
        byte b;
        int i5;
        char[] cArr2;
        int i6;
        boolean z2;
        int i7;
        int i8;
        int i9;
        int i10;
        int i11;
        int i12;
        char[] cArr3;
        int i13;
        int i14 = 0;
        boolean z3 = false;
        if (i2 == 46) {
            i5 = i + 1;
            cArr[i] = (char) i2;
            while (true) {
                if (this._inputPtr >= this._inputEnd && !loadMore()) {
                    z3 = true;
                    b = i2;
                    break;
                }
                byte[] bArr = this._inputBuffer;
                int i15 = this._inputPtr;
                this._inputPtr = i15 + 1;
                i2 = bArr[i15] & 255;
                if (i2 < 48) {
                    b = i2;
                    break;
                } else if (i2 > 57) {
                    b = i2;
                    break;
                } else {
                    i14++;
                    if (i5 >= cArr.length) {
                        cArr = this._textBuffer.finishCurrentSegment();
                        i5 = 0;
                    }
                    int i16 = i5;
                    i5 = i16 + 1;
                    cArr[i16] = (char) i2;
                }
            }
            if (i14 == 0) {
                reportUnexpectedNumberChar(b, "Decimal point not followed by a digit");
            }
            i4 = i14;
            cArr2 = cArr;
        } else {
            i4 = 0;
            b = i2;
            i5 = i;
            cArr2 = cArr;
        }
        if (b == 101 || b == 69) {
            if (i5 >= cArr2.length) {
                i8 = 0;
                cArr2 = this._textBuffer.finishCurrentSegment();
            } else {
                i8 = i5;
            }
            int i17 = i8 + 1;
            cArr2[i8] = (char) b;
            if (this._inputPtr >= this._inputEnd) {
                loadMoreGuaranteed();
            }
            byte[] bArr2 = this._inputBuffer;
            int i18 = this._inputPtr;
            this._inputPtr = i18 + 1;
            byte b2 = bArr2[i18] & 255;
            if (b2 == 45 || b2 == 43) {
                if (i17 >= cArr2.length) {
                    cArr2 = this._textBuffer.finishCurrentSegment();
                    i13 = 0;
                } else {
                    i13 = i17;
                }
                int i19 = i13 + 1;
                cArr2[i13] = (char) b2;
                if (this._inputPtr >= this._inputEnd) {
                    loadMoreGuaranteed();
                }
                byte[] bArr3 = this._inputBuffer;
                int i20 = this._inputPtr;
                this._inputPtr = i20 + 1;
                b2 = bArr3[i20] & 255;
                i9 = 0;
                i10 = i19;
            } else {
                i9 = 0;
                i10 = i17;
            }
            while (true) {
                if (b <= 57 && b >= 48) {
                    i9++;
                    if (i10 >= cArr2.length) {
                        cArr3 = this._textBuffer.finishCurrentSegment();
                        i10 = 0;
                    } else {
                        cArr3 = cArr2;
                    }
                    i12 = i10 + 1;
                    cArr3[i10] = (char) b;
                    if (this._inputPtr >= this._inputEnd && !loadMore()) {
                        i11 = i9;
                        z2 = true;
                        break;
                    }
                    byte[] bArr4 = this._inputBuffer;
                    int i21 = this._inputPtr;
                    this._inputPtr = i21 + 1;
                    b2 = bArr4[i21] & 255;
                    i10 = i12;
                    cArr2 = cArr3;
                } else {
                    i11 = i9;
                    z2 = z3;
                    i12 = i10;
                }
            }
            i11 = i9;
            z2 = z3;
            i12 = i10;
            if (i11 == 0) {
                reportUnexpectedNumberChar(b, "Exponent indicator not followed by a digit");
            }
            i7 = i11;
            i6 = i12;
        } else {
            i7 = 0;
            z2 = z3;
            i6 = i5;
        }
        if (!z2) {
            this._inputPtr--;
            if (this._parsingContext.inRoot()) {
                _verifyRootSpace(b);
            }
        }
        this._textBuffer.setCurrentLength(i6);
        return resetFloat(z, i3, i4, i7);
    }

    private final void _verifyRootSpace(int i) throws IOException {
        this._inputPtr++;
        switch (i) {
            case 9:
            case 32:
                return;
            case 10:
                this._currInputRow++;
                this._currInputRowStart = this._inputPtr;
                return;
            case 13:
                _skipCR();
                return;
            default:
                _reportMissingRootWS(i);
                return;
        }
    }

    /* access modifiers changed from: protected */
    public final String _parseName(int i) throws IOException {
        if (i != 34) {
            return _handleOddName(i);
        }
        if (this._inputPtr + 13 > this._inputEnd) {
            return slowParseName();
        }
        byte[] bArr = this._inputBuffer;
        int[] iArr = _icLatin1;
        int i2 = this._inputPtr;
        this._inputPtr = i2 + 1;
        byte b = bArr[i2] & 255;
        if (iArr[b] == 0) {
            int i3 = this._inputPtr;
            this._inputPtr = i3 + 1;
            byte b2 = bArr[i3] & 255;
            if (iArr[b2] == 0) {
                byte b3 = (b << 8) | b2;
                int i4 = this._inputPtr;
                this._inputPtr = i4 + 1;
                byte b4 = bArr[i4] & 255;
                if (iArr[b4] == 0) {
                    byte b5 = (b3 << 8) | b4;
                    int i5 = this._inputPtr;
                    this._inputPtr = i5 + 1;
                    byte b6 = bArr[i5] & 255;
                    if (iArr[b6] == 0) {
                        byte b7 = (b5 << 8) | b6;
                        int i6 = this._inputPtr;
                        this._inputPtr = i6 + 1;
                        byte b8 = bArr[i6] & 255;
                        if (iArr[b8] == 0) {
                            this._quad1 = b7;
                            return parseMediumName(b8);
                        } else if (b8 == 34) {
                            return findName(b7, 4);
                        } else {
                            return parseName(b7, b8, 4);
                        }
                    } else if (b6 == 34) {
                        return findName(b5, 3);
                    } else {
                        return parseName(b5, b6, 3);
                    }
                } else if (b4 == 34) {
                    return findName(b3, 2);
                } else {
                    return parseName(b3, b4, 2);
                }
            } else if (b2 == 34) {
                return findName(b, 1);
            } else {
                return parseName(b, b2, 1);
            }
        } else if (b == 34) {
            return "";
        } else {
            return parseName(0, b, 0);
        }
    }

    /* access modifiers changed from: protected */
    public final String parseMediumName(int i) throws IOException {
        byte[] bArr = this._inputBuffer;
        int[] iArr = _icLatin1;
        int i2 = this._inputPtr;
        this._inputPtr = i2 + 1;
        byte b = bArr[i2] & 255;
        if (iArr[b] == 0) {
            byte b2 = b | (i << 8);
            int i3 = this._inputPtr;
            this._inputPtr = i3 + 1;
            byte b3 = bArr[i3] & 255;
            if (iArr[b3] == 0) {
                byte b4 = (b2 << 8) | b3;
                int i4 = this._inputPtr;
                this._inputPtr = i4 + 1;
                byte b5 = bArr[i4] & 255;
                if (iArr[b5] == 0) {
                    byte b6 = (b4 << 8) | b5;
                    int i5 = this._inputPtr;
                    this._inputPtr = i5 + 1;
                    byte b7 = bArr[i5] & 255;
                    if (iArr[b7] == 0) {
                        return parseMediumName2(b7, b6);
                    }
                    if (b7 == 34) {
                        return findName(this._quad1, b6, 4);
                    }
                    return parseName(this._quad1, b6, b7, 4);
                } else if (b5 == 34) {
                    return findName(this._quad1, b4, 3);
                } else {
                    return parseName(this._quad1, b4, b5, 3);
                }
            } else if (b3 == 34) {
                return findName(this._quad1, b2, 2);
            } else {
                return parseName(this._quad1, b2, b3, 2);
            }
        } else if (b == 34) {
            return findName(this._quad1, i, 1);
        } else {
            return parseName(this._quad1, i, b, 1);
        }
    }

    /* access modifiers changed from: protected */
    public final String parseMediumName2(int i, int i2) throws IOException {
        byte[] bArr = this._inputBuffer;
        int[] iArr = _icLatin1;
        int i3 = this._inputPtr;
        this._inputPtr = i3 + 1;
        byte b = bArr[i3] & 255;
        if (iArr[b] == 0) {
            byte b2 = (i << 8) | b;
            int i4 = this._inputPtr;
            this._inputPtr = i4 + 1;
            byte b3 = bArr[i4] & 255;
            if (iArr[b3] == 0) {
                byte b4 = (b2 << 8) | b3;
                int i5 = this._inputPtr;
                this._inputPtr = i5 + 1;
                byte b5 = bArr[i5] & 255;
                if (iArr[b5] == 0) {
                    byte b6 = (b4 << 8) | b5;
                    int i6 = this._inputPtr;
                    this._inputPtr = i6 + 1;
                    byte b7 = bArr[i6] & 255;
                    if (iArr[b7] == 0) {
                        return parseLongName(b7, i2, b6);
                    }
                    if (b7 == 34) {
                        return findName(this._quad1, i2, (int) b6, 4);
                    }
                    return parseName(this._quad1, i2, b6, b7, 4);
                } else if (b5 == 34) {
                    return findName(this._quad1, i2, (int) b4, 3);
                } else {
                    return parseName(this._quad1, i2, b4, b5, 3);
                }
            } else if (b3 == 34) {
                return findName(this._quad1, i2, (int) b2, 2);
            } else {
                return parseName(this._quad1, i2, b2, b3, 2);
            }
        } else if (b == 34) {
            return findName(this._quad1, i2, i, 1);
        } else {
            return parseName(this._quad1, i2, i, b, 1);
        }
    }

    /* access modifiers changed from: protected */
    public final String parseLongName(int i, int i2, int i3) throws IOException {
        this._quadBuffer[0] = this._quad1;
        this._quadBuffer[1] = i2;
        this._quadBuffer[2] = i3;
        byte[] bArr = this._inputBuffer;
        int[] iArr = _icLatin1;
        int i4 = 3;
        int i5 = i;
        while (this._inputPtr + 4 <= this._inputEnd) {
            int i6 = this._inputPtr;
            this._inputPtr = i6 + 1;
            byte b = bArr[i6] & 255;
            if (iArr[b] == 0) {
                byte b2 = (i5 << 8) | b;
                int i7 = this._inputPtr;
                this._inputPtr = i7 + 1;
                byte b3 = bArr[i7] & 255;
                if (iArr[b3] == 0) {
                    byte b4 = (b2 << 8) | b3;
                    int i8 = this._inputPtr;
                    this._inputPtr = i8 + 1;
                    byte b5 = bArr[i8] & 255;
                    if (iArr[b5] == 0) {
                        int i9 = (b4 << 8) | b5;
                        int i10 = this._inputPtr;
                        this._inputPtr = i10 + 1;
                        byte b6 = bArr[i10] & 255;
                        if (iArr[b6] == 0) {
                            if (i4 >= this._quadBuffer.length) {
                                this._quadBuffer = growArrayBy(this._quadBuffer, i4);
                            }
                            int i11 = i4 + 1;
                            this._quadBuffer[i4] = i9;
                            i4 = i11;
                            i5 = b6;
                        } else if (b6 == 34) {
                            return findName(this._quadBuffer, i4, i9, 4);
                        } else {
                            return parseEscapedName(this._quadBuffer, i4, i9, b6, 4);
                        }
                    } else if (b5 == 34) {
                        return findName(this._quadBuffer, i4, (int) b4, 3);
                    } else {
                        return parseEscapedName(this._quadBuffer, i4, b4, b5, 3);
                    }
                } else if (b3 == 34) {
                    return findName(this._quadBuffer, i4, (int) b2, 2);
                } else {
                    return parseEscapedName(this._quadBuffer, i4, b2, b3, 2);
                }
            } else if (b == 34) {
                return findName(this._quadBuffer, i4, i5, 1);
            } else {
                return parseEscapedName(this._quadBuffer, i4, i5, b, 1);
            }
        }
        return parseEscapedName(this._quadBuffer, i4, 0, i5, 0);
    }

    /* access modifiers changed from: protected */
    public String slowParseName() throws IOException {
        if (this._inputPtr >= this._inputEnd && !loadMore()) {
            _reportInvalidEOF(": was expecting closing '\"' for name");
        }
        byte[] bArr = this._inputBuffer;
        int i = this._inputPtr;
        this._inputPtr = i + 1;
        byte b = bArr[i] & 255;
        if (b == 34) {
            return "";
        }
        return parseEscapedName(this._quadBuffer, 0, 0, b, 0);
    }

    private final String parseName(int i, int i2, int i3) throws IOException {
        return parseEscapedName(this._quadBuffer, 0, i, i2, i3);
    }

    private final String parseName(int i, int i2, int i3, int i4) throws IOException {
        this._quadBuffer[0] = i;
        return parseEscapedName(this._quadBuffer, 1, i2, i3, i4);
    }

    private final String parseName(int i, int i2, int i3, int i4, int i5) throws IOException {
        this._quadBuffer[0] = i;
        this._quadBuffer[1] = i2;
        return parseEscapedName(this._quadBuffer, 2, i3, i4, i5);
    }

    /* access modifiers changed from: protected */
    /* JADX WARNING: Removed duplicated region for block: B:28:0x0064  */
    /* JADX WARNING: Removed duplicated region for block: B:43:0x00b7  */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    public final java.lang.String parseEscapedName(int[] r9, int r10, int r11, int r12, int r13) throws java.io.IOException {
        /*
            r8 = this;
            r7 = 4
            r2 = 0
            int[] r6 = _icLatin1
        L_0x0004:
            r0 = r6[r12]
            if (r0 == 0) goto L_0x00cf
            r0 = 34
            if (r12 != r0) goto L_0x002e
            if (r13 <= 0) goto L_0x0021
            int r0 = r9.length
            if (r10 < r0) goto L_0x0018
            int r0 = r9.length
            int[] r9 = growArrayBy(r9, r0)
            r8._quadBuffer = r9
        L_0x0018:
            int r0 = r10 + 1
            int r1 = pad(r11, r13)
            r9[r10] = r1
            r10 = r0
        L_0x0021:
            com.fasterxml.jackson.core.sym.ByteQuadsCanonicalizer r0 = r8._symbols
            java.lang.String r0 = r0.findName(r9, r10)
            if (r0 != 0) goto L_0x002d
            java.lang.String r0 = r8.addName(r9, r10, r13)
        L_0x002d:
            return r0
        L_0x002e:
            r0 = 92
            if (r12 == r0) goto L_0x008a
            java.lang.String r0 = "name"
            r8._throwUnquotedSpace(r12, r0)
        L_0x0037:
            r0 = 127(0x7f, float:1.78E-43)
            if (r12 <= r0) goto L_0x00cf
            if (r13 < r7) goto L_0x00cc
            int r0 = r9.length
            if (r10 < r0) goto L_0x0047
            int r0 = r9.length
            int[] r9 = growArrayBy(r9, r0)
            r8._quadBuffer = r9
        L_0x0047:
            int r5 = r10 + 1
            r9[r10] = r11
            r13 = r2
            r11 = r2
            r0 = r9
        L_0x004e:
            r1 = 2048(0x800, float:2.87E-42)
            if (r12 >= r1) goto L_0x008f
            int r1 = r11 << 8
            int r3 = r12 >> 6
            r3 = r3 | 192(0xc0, float:2.69E-43)
            r3 = r3 | r1
            int r1 = r13 + 1
        L_0x005b:
            r4 = r12 & 63
            r4 = r4 | 128(0x80, float:1.794E-43)
            r13 = r1
            r11 = r3
            r10 = r5
        L_0x0062:
            if (r13 >= r7) goto L_0x00b7
            int r13 = r13 + 1
            int r1 = r11 << 8
            r11 = r1 | r4
            r9 = r0
        L_0x006b:
            int r0 = r8._inputPtr
            int r1 = r8._inputEnd
            if (r0 < r1) goto L_0x007c
            boolean r0 = r8.loadMore()
            if (r0 != 0) goto L_0x007c
            java.lang.String r0 = " in field name"
            r8._reportInvalidEOF(r0)
        L_0x007c:
            byte[] r0 = r8._inputBuffer
            int r1 = r8._inputPtr
            int r3 = r1 + 1
            r8._inputPtr = r3
            byte r0 = r0[r1]
            r12 = r0 & 255(0xff, float:3.57E-43)
            goto L_0x0004
        L_0x008a:
            char r12 = r8._decodeEscaped()
            goto L_0x0037
        L_0x008f:
            int r1 = r11 << 8
            int r3 = r12 >> 12
            r3 = r3 | 224(0xe0, float:3.14E-43)
            r3 = r3 | r1
            int r1 = r13 + 1
            if (r1 < r7) goto L_0x00ca
            int r1 = r0.length
            if (r5 < r1) goto L_0x00a4
            int r1 = r0.length
            int[] r0 = growArrayBy(r0, r1)
            r8._quadBuffer = r0
        L_0x00a4:
            int r4 = r5 + 1
            r0[r5] = r3
            r1 = r2
            r3 = r2
        L_0x00aa:
            int r3 = r3 << 8
            int r5 = r12 >> 6
            r5 = r5 & 63
            r5 = r5 | 128(0x80, float:1.794E-43)
            r3 = r3 | r5
            int r1 = r1 + 1
            r5 = r4
            goto L_0x005b
        L_0x00b7:
            int r1 = r0.length
            if (r10 < r1) goto L_0x00c1
            int r1 = r0.length
            int[] r0 = growArrayBy(r0, r1)
            r8._quadBuffer = r0
        L_0x00c1:
            int r1 = r10 + 1
            r0[r10] = r11
            r13 = 1
            r11 = r4
            r10 = r1
            r9 = r0
            goto L_0x006b
        L_0x00ca:
            r4 = r5
            goto L_0x00aa
        L_0x00cc:
            r5 = r10
            r0 = r9
            goto L_0x004e
        L_0x00cf:
            r4 = r12
            r0 = r9
            goto L_0x0062
        */
        throw new UnsupportedOperationException("Method not decompiled: com.fasterxml.jackson.core.json.UTF8StreamJsonParser.parseEscapedName(int[], int, int, int, int):java.lang.String");
    }

    /* access modifiers changed from: protected */
    public String _handleOddName(int i) throws IOException {
        int i2;
        if (i == 39 && isEnabled(Feature.ALLOW_SINGLE_QUOTES)) {
            return _parseAposName();
        }
        if (!isEnabled(Feature.ALLOW_UNQUOTED_FIELD_NAMES)) {
            _reportUnexpectedChar((char) _decodeCharForError(i), "was expecting double-quote to start field name");
        }
        int[] inputCodeUtf8JsNames = CharTypes.getInputCodeUtf8JsNames();
        if (inputCodeUtf8JsNames[i] != 0) {
            _reportUnexpectedChar(i, "was expecting either valid name character (for unquoted name) or double-quote (for quoted) to start field name");
        }
        int[] iArr = this._quadBuffer;
        int i3 = 0;
        int i4 = 0;
        int i5 = 0;
        byte b = i;
        while (true) {
            if (i3 < 4) {
                i3++;
                i2 = (i4 << 8) | b;
            } else {
                if (i5 >= iArr.length) {
                    iArr = growArrayBy(iArr, iArr.length);
                    this._quadBuffer = iArr;
                }
                int i6 = i5 + 1;
                iArr[i5] = i4;
                i3 = 1;
                i2 = b;
                i5 = i6;
            }
            if (this._inputPtr >= this._inputEnd && !loadMore()) {
                _reportInvalidEOF(" in field name");
            }
            b = this._inputBuffer[this._inputPtr] & 255;
            if (inputCodeUtf8JsNames[b] != 0) {
                break;
            }
            this._inputPtr++;
            i4 = i2;
        }
        if (i3 > 0) {
            if (i5 >= iArr.length) {
                iArr = growArrayBy(iArr, iArr.length);
                this._quadBuffer = iArr;
            }
            int i7 = i5 + 1;
            iArr[i5] = i2;
            i5 = i7;
        }
        String findName = this._symbols.findName(iArr, i5);
        if (findName == null) {
            return addName(iArr, i5, i3);
        }
        return findName;
    }

    /* JADX WARNING: type inference failed for: r3v5 */
    /* JADX WARNING: type inference failed for: r3v21 */
    /* access modifiers changed from: protected */
    /* JADX WARNING: Multi-variable type inference failed. Error: jadx.core.utils.exceptions.JadxRuntimeException: No candidate types for var: r3v5
      assigns: []
      uses: []
      mth insns count: 122
    	at jadx.core.dex.visitors.typeinference.TypeSearch.fillTypeCandidates(TypeSearch.java:237)
    	at java.base/java.util.ArrayList.forEach(ArrayList.java:1540)
    	at jadx.core.dex.visitors.typeinference.TypeSearch.run(TypeSearch.java:53)
    	at jadx.core.dex.visitors.typeinference.TypeInferenceVisitor.runMultiVariableSearch(TypeInferenceVisitor.java:99)
    	at jadx.core.dex.visitors.typeinference.TypeInferenceVisitor.visit(TypeInferenceVisitor.java:92)
    	at jadx.core.dex.visitors.DepthTraversal.visit(DepthTraversal.java:27)
    	at jadx.core.dex.visitors.DepthTraversal.lambda$visit$1(DepthTraversal.java:14)
    	at java.base/java.util.ArrayList.forEach(ArrayList.java:1540)
    	at jadx.core.dex.visitors.DepthTraversal.visit(DepthTraversal.java:14)
    	at jadx.core.ProcessClass.process(ProcessClass.java:30)
    	at jadx.core.ProcessClass.lambda$processDependencies$0(ProcessClass.java:49)
    	at java.base/java.util.ArrayList.forEach(ArrayList.java:1540)
    	at jadx.core.ProcessClass.processDependencies(ProcessClass.java:49)
    	at jadx.core.ProcessClass.process(ProcessClass.java:35)
    	at jadx.api.JadxDecompiler.processClass(JadxDecompiler.java:311)
    	at jadx.api.JavaClass.decompile(JavaClass.java:62)
    	at jadx.api.JadxDecompiler.lambda$appendSourcesSave$0(JadxDecompiler.java:217)
     */
    /* JADX WARNING: Unknown variable types count: 1 */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    public java.lang.String _parseAposName() throws java.io.IOException {
        /*
            r11 = this;
            r10 = 39
            r9 = 4
            r2 = 0
            int r0 = r11._inputPtr
            int r1 = r11._inputEnd
            if (r0 < r1) goto L_0x0015
            boolean r0 = r11.loadMore()
            if (r0 != 0) goto L_0x0015
            java.lang.String r0 = ": was expecting closing ''' for name"
            r11._reportInvalidEOF(r0)
        L_0x0015:
            byte[] r0 = r11._inputBuffer
            int r1 = r11._inputPtr
            int r3 = r1 + 1
            r11._inputPtr = r3
            byte r0 = r0[r1]
            r4 = r0 & 255(0xff, float:3.57E-43)
            if (r4 != r10) goto L_0x0026
            java.lang.String r0 = ""
        L_0x0025:
            return r0
        L_0x0026:
            int[] r0 = r11._quadBuffer
            int[] r8 = _icLatin1
            r6 = r2
            r3 = r2
            r5 = r2
        L_0x002d:
            if (r4 != r10) goto L_0x0050
            if (r6 <= 0) goto L_0x00f3
            int r1 = r0.length
            if (r5 < r1) goto L_0x003b
            int r1 = r0.length
            int[] r0 = growArrayBy(r0, r1)
            r11._quadBuffer = r0
        L_0x003b:
            int r1 = r5 + 1
            int r2 = pad(r3, r6)
            r0[r5] = r2
        L_0x0043:
            com.fasterxml.jackson.core.sym.ByteQuadsCanonicalizer r2 = r11._symbols
            java.lang.String r2 = r2.findName(r0, r1)
            if (r2 != 0) goto L_0x00f0
            java.lang.String r0 = r11.addName(r0, r1, r6)
            goto L_0x0025
        L_0x0050:
            r1 = 34
            if (r4 == r1) goto L_0x00fb
            r1 = r8[r4]
            if (r1 == 0) goto L_0x00fb
            r1 = 92
            if (r4 == r1) goto L_0x00b1
            java.lang.String r1 = "name"
            r11._throwUnquotedSpace(r4, r1)
            r7 = r4
        L_0x0062:
            r1 = 127(0x7f, float:1.78E-43)
            if (r7 <= r1) goto L_0x00f8
            if (r6 < r9) goto L_0x0079
            int r1 = r0.length
            if (r5 < r1) goto L_0x0072
            int r1 = r0.length
            int[] r0 = growArrayBy(r0, r1)
            r11._quadBuffer = r0
        L_0x0072:
            int r1 = r5 + 1
            r0[r5] = r3
            r6 = r2
            r3 = r2
            r5 = r1
        L_0x0079:
            r1 = 2048(0x800, float:2.87E-42)
            if (r7 >= r1) goto L_0x00b6
            int r1 = r3 << 8
            int r3 = r7 >> 6
            r3 = r3 | 192(0xc0, float:2.69E-43)
            r3 = r3 | r1
            int r1 = r6 + 1
        L_0x0086:
            r4 = r7 & 63
            r4 = r4 | 128(0x80, float:1.794E-43)
        L_0x008a:
            if (r1 >= r9) goto L_0x00de
            int r1 = r1 + 1
            int r3 = r3 << 8
            r3 = r3 | r4
        L_0x0091:
            int r4 = r11._inputPtr
            int r6 = r11._inputEnd
            if (r4 < r6) goto L_0x00a2
            boolean r4 = r11.loadMore()
            if (r4 != 0) goto L_0x00a2
            java.lang.String r4 = " in field name"
            r11._reportInvalidEOF(r4)
        L_0x00a2:
            byte[] r4 = r11._inputBuffer
            int r6 = r11._inputPtr
            int r7 = r6 + 1
            r11._inputPtr = r7
            byte r4 = r4[r6]
            r4 = r4 & 255(0xff, float:3.57E-43)
            r6 = r1
            goto L_0x002d
        L_0x00b1:
            char r7 = r11._decodeEscaped()
            goto L_0x0062
        L_0x00b6:
            int r1 = r3 << 8
            int r3 = r7 >> 12
            r3 = r3 | 224(0xe0, float:3.14E-43)
            r3 = r3 | r1
            int r1 = r6 + 1
            if (r1 < r9) goto L_0x00f6
            int r1 = r0.length
            if (r5 < r1) goto L_0x00cb
            int r1 = r0.length
            int[] r0 = growArrayBy(r0, r1)
            r11._quadBuffer = r0
        L_0x00cb:
            int r4 = r5 + 1
            r0[r5] = r3
            r1 = r2
            r3 = r2
        L_0x00d1:
            int r3 = r3 << 8
            int r5 = r7 >> 6
            r5 = r5 & 63
            r5 = r5 | 128(0x80, float:1.794E-43)
            r3 = r3 | r5
            int r1 = r1 + 1
            r5 = r4
            goto L_0x0086
        L_0x00de:
            int r1 = r0.length
            if (r5 < r1) goto L_0x00e8
            int r1 = r0.length
            int[] r0 = growArrayBy(r0, r1)
            r11._quadBuffer = r0
        L_0x00e8:
            int r6 = r5 + 1
            r0[r5] = r3
            r1 = 1
            r3 = r4
            r5 = r6
            goto L_0x0091
        L_0x00f0:
            r0 = r2
            goto L_0x0025
        L_0x00f3:
            r1 = r5
            goto L_0x0043
        L_0x00f6:
            r4 = r5
            goto L_0x00d1
        L_0x00f8:
            r1 = r6
            r4 = r7
            goto L_0x008a
        L_0x00fb:
            r1 = r6
            goto L_0x008a
        */
        throw new UnsupportedOperationException("Method not decompiled: com.fasterxml.jackson.core.json.UTF8StreamJsonParser._parseAposName():java.lang.String");
    }

    private final String findName(int i, int i2) throws JsonParseException {
        int pad = pad(i, i2);
        String findName = this._symbols.findName(pad);
        if (findName != null) {
            return findName;
        }
        this._quadBuffer[0] = pad;
        return addName(this._quadBuffer, 1, i2);
    }

    private final String findName(int i, int i2, int i3) throws JsonParseException {
        int pad = pad(i2, i3);
        String findName = this._symbols.findName(i, pad);
        if (findName != null) {
            return findName;
        }
        this._quadBuffer[0] = i;
        this._quadBuffer[1] = pad;
        return addName(this._quadBuffer, 2, i3);
    }

    private final String findName(int i, int i2, int i3, int i4) throws JsonParseException {
        int pad = pad(i3, i4);
        String findName = this._symbols.findName(i, i2, pad);
        if (findName != null) {
            return findName;
        }
        int[] iArr = this._quadBuffer;
        iArr[0] = i;
        iArr[1] = i2;
        iArr[2] = pad(pad, i4);
        return addName(iArr, 3, i4);
    }

    private final String findName(int[] iArr, int i, int i2, int i3) throws JsonParseException {
        if (i >= iArr.length) {
            iArr = growArrayBy(iArr, iArr.length);
            this._quadBuffer = iArr;
        }
        int i4 = i + 1;
        iArr[i] = pad(i2, i3);
        String findName = this._symbols.findName(iArr, i4);
        if (findName == null) {
            return addName(iArr, i4, i3);
        }
        return findName;
    }

    /* JADX WARNING: Removed duplicated region for block: B:35:0x00c3  */
    /* JADX WARNING: Removed duplicated region for block: B:53:0x00c9 A[SYNTHETIC] */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    private final java.lang.String addName(int[] r11, int r12, int r13) throws com.fasterxml.jackson.core.JsonParseException {
        /*
            r10 = this;
            int r0 = r12 << 2
            int r0 = r0 + -4
            int r7 = r0 + r13
            r0 = 4
            if (r13 >= r0) goto L_0x00d0
            int r0 = r12 + -1
            r0 = r11[r0]
            int r1 = r12 + -1
            int r2 = 4 - r13
            int r2 = r2 << 3
            int r2 = r0 << r2
            r11[r1] = r2
        L_0x0017:
            com.fasterxml.jackson.core.util.TextBuffer r1 = r10._textBuffer
            char[] r1 = r1.emptyAndGetCurrentSegment()
            r5 = 0
            r3 = 0
        L_0x001f:
            if (r3 >= r7) goto L_0x00f3
            int r2 = r3 >> 2
            r2 = r11[r2]
            r4 = r3 & 3
            int r4 = 3 - r4
            int r4 = r4 << 3
            int r2 = r2 >> r4
            r2 = r2 & 255(0xff, float:3.57E-43)
            int r3 = r3 + 1
            r4 = 127(0x7f, float:1.78E-43)
            if (r2 <= r4) goto L_0x0107
            r4 = r2 & 224(0xe0, float:3.14E-43)
            r6 = 192(0xc0, float:2.69E-43)
            if (r4 != r6) goto L_0x00d3
            r6 = r2 & 31
            r2 = 1
            r4 = r2
        L_0x003e:
            int r2 = r3 + r4
            if (r2 <= r7) goto L_0x0047
            java.lang.String r2 = " in field name"
            r10._reportInvalidEOF(r2)
        L_0x0047:
            int r2 = r3 >> 2
            r2 = r11[r2]
            r8 = r3 & 3
            int r8 = 3 - r8
            int r8 = r8 << 3
            int r2 = r2 >> r8
            int r3 = r3 + 1
            r8 = r2 & 192(0xc0, float:2.69E-43)
            r9 = 128(0x80, float:1.794E-43)
            if (r8 == r9) goto L_0x005d
            r10._reportInvalidOther(r2)
        L_0x005d:
            int r6 = r6 << 6
            r2 = r2 & 63
            r2 = r2 | r6
            r6 = 1
            if (r4 <= r6) goto L_0x00a0
            int r6 = r3 >> 2
            r6 = r11[r6]
            r8 = r3 & 3
            int r8 = 3 - r8
            int r8 = r8 << 3
            int r6 = r6 >> r8
            int r3 = r3 + 1
            r8 = r6 & 192(0xc0, float:2.69E-43)
            r9 = 128(0x80, float:1.794E-43)
            if (r8 == r9) goto L_0x007b
            r10._reportInvalidOther(r6)
        L_0x007b:
            int r2 = r2 << 6
            r6 = r6 & 63
            r2 = r2 | r6
            r6 = 2
            if (r4 <= r6) goto L_0x00a0
            int r6 = r3 >> 2
            r6 = r11[r6]
            r8 = r3 & 3
            int r8 = 3 - r8
            int r8 = r8 << 3
            int r6 = r6 >> r8
            int r3 = r3 + 1
            r8 = r6 & 192(0xc0, float:2.69E-43)
            r9 = 128(0x80, float:1.794E-43)
            if (r8 == r9) goto L_0x009b
            r8 = r6 & 255(0xff, float:3.57E-43)
            r10._reportInvalidOther(r8)
        L_0x009b:
            int r2 = r2 << 6
            r6 = r6 & 63
            r2 = r2 | r6
        L_0x00a0:
            r6 = 2
            if (r4 <= r6) goto L_0x0107
            r4 = 65536(0x10000, float:9.18355E-41)
            int r2 = r2 - r4
            int r4 = r1.length
            if (r5 < r4) goto L_0x00af
            com.fasterxml.jackson.core.util.TextBuffer r1 = r10._textBuffer
            char[] r1 = r1.expandCurrentSegment()
        L_0x00af:
            int r4 = r5 + 1
            r6 = 55296(0xd800, float:7.7486E-41)
            int r8 = r2 >> 10
            int r6 = r6 + r8
            char r6 = (char) r6
            r1[r5] = r6
            r5 = 56320(0xdc00, float:7.8921E-41)
            r2 = r2 & 1023(0x3ff, float:1.434E-42)
            r2 = r2 | r5
        L_0x00c0:
            int r5 = r1.length
            if (r4 < r5) goto L_0x00c9
            com.fasterxml.jackson.core.util.TextBuffer r1 = r10._textBuffer
            char[] r1 = r1.expandCurrentSegment()
        L_0x00c9:
            int r5 = r4 + 1
            char r2 = (char) r2
            r1[r4] = r2
            goto L_0x001f
        L_0x00d0:
            r0 = 0
            goto L_0x0017
        L_0x00d3:
            r4 = r2 & 240(0xf0, float:3.36E-43)
            r6 = 224(0xe0, float:3.14E-43)
            if (r4 != r6) goto L_0x00df
            r6 = r2 & 15
            r2 = 2
            r4 = r2
            goto L_0x003e
        L_0x00df:
            r4 = r2 & 248(0xf8, float:3.48E-43)
            r6 = 240(0xf0, float:3.36E-43)
            if (r4 != r6) goto L_0x00eb
            r6 = r2 & 7
            r2 = 3
            r4 = r2
            goto L_0x003e
        L_0x00eb:
            r10._reportInvalidInitial(r2)
            r2 = 1
            r4 = r2
            r6 = r2
            goto L_0x003e
        L_0x00f3:
            java.lang.String r2 = new java.lang.String
            r3 = 0
            r2.<init>(r1, r3, r5)
            r1 = 4
            if (r13 >= r1) goto L_0x0100
            int r1 = r12 + -1
            r11[r1] = r0
        L_0x0100:
            com.fasterxml.jackson.core.sym.ByteQuadsCanonicalizer r0 = r10._symbols
            java.lang.String r0 = r0.addName(r2, r11, r12)
            return r0
        L_0x0107:
            r4 = r5
            goto L_0x00c0
        */
        throw new UnsupportedOperationException("Method not decompiled: com.fasterxml.jackson.core.json.UTF8StreamJsonParser.addName(int[], int, int):java.lang.String");
    }

    /* access modifiers changed from: protected */
    public void _finishString() throws IOException {
        int i = this._inputPtr;
        if (i >= this._inputEnd) {
            loadMoreGuaranteed();
            i = this._inputPtr;
        }
        int i2 = 0;
        char[] emptyAndGetCurrentSegment = this._textBuffer.emptyAndGetCurrentSegment();
        int[] iArr = _icUTF8;
        int min = Math.min(this._inputEnd, emptyAndGetCurrentSegment.length + i);
        byte[] bArr = this._inputBuffer;
        int i3 = i;
        while (true) {
            if (i3 >= min) {
                break;
            }
            byte b = bArr[i3] & 255;
            if (iArr[b] == 0) {
                i3++;
                int i4 = i2 + 1;
                emptyAndGetCurrentSegment[i2] = (char) b;
                i2 = i4;
            } else if (b == 34) {
                this._inputPtr = i3 + 1;
                this._textBuffer.setCurrentLength(i2);
                return;
            }
        }
        this._inputPtr = i3;
        _finishString2(emptyAndGetCurrentSegment, i2);
    }

    /* access modifiers changed from: protected */
    public String _finishAndReturnString() throws IOException {
        int i = this._inputPtr;
        if (i >= this._inputEnd) {
            loadMoreGuaranteed();
            i = this._inputPtr;
        }
        int i2 = 0;
        char[] emptyAndGetCurrentSegment = this._textBuffer.emptyAndGetCurrentSegment();
        int[] iArr = _icUTF8;
        int min = Math.min(this._inputEnd, emptyAndGetCurrentSegment.length + i);
        byte[] bArr = this._inputBuffer;
        int i3 = i;
        while (true) {
            if (i3 >= min) {
                break;
            }
            byte b = bArr[i3] & 255;
            if (iArr[b] == 0) {
                i3++;
                int i4 = i2 + 1;
                emptyAndGetCurrentSegment[i2] = (char) b;
                i2 = i4;
            } else if (b == 34) {
                this._inputPtr = i3 + 1;
                return this._textBuffer.setCurrentAndReturn(i2);
            }
        }
        this._inputPtr = i3;
        _finishString2(emptyAndGetCurrentSegment, i2);
        return this._textBuffer.contentsAsString();
    }

    /* JADX WARNING: type inference failed for: r0v10, types: [int] */
    /* JADX WARNING: type inference failed for: r2v11 */
    /* JADX WARNING: type inference failed for: r0v11, types: [int] */
    /* JADX WARNING: type inference failed for: r2v12 */
    /* JADX WARNING: type inference failed for: r0v12, types: [int] */
    /* JADX WARNING: type inference failed for: r2v13 */
    /* JADX WARNING: Multi-variable type inference failed */
    /* JADX WARNING: Unknown variable types count: 3 */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    private final void _finishString2(char[] r8, int r9) throws java.io.IOException {
        /*
            r7 = this;
            r1 = 0
            int[] r4 = _icUTF8
            byte[] r5 = r7._inputBuffer
        L_0x0005:
            int r0 = r7._inputPtr
            int r2 = r7._inputEnd
            if (r0 < r2) goto L_0x0010
            r7.loadMoreGuaranteed()
            int r0 = r7._inputPtr
        L_0x0010:
            int r2 = r8.length
            if (r9 < r2) goto L_0x001a
            com.fasterxml.jackson.core.util.TextBuffer r2 = r7._textBuffer
            char[] r8 = r2.finishCurrentSegment()
            r9 = r1
        L_0x001a:
            int r2 = r7._inputEnd
            int r3 = r8.length
            int r3 = r3 - r9
            int r3 = r3 + r0
            int r6 = java.lang.Math.min(r2, r3)
        L_0x0023:
            if (r0 >= r6) goto L_0x0043
            int r2 = r0 + 1
            byte r0 = r5[r0]
            r0 = r0 & 255(0xff, float:3.57E-43)
            r3 = r4[r0]
            if (r3 == 0) goto L_0x003b
            r7._inputPtr = r2
            r2 = 34
            if (r0 != r2) goto L_0x0046
            com.fasterxml.jackson.core.util.TextBuffer r0 = r7._textBuffer
            r0.setCurrentLength(r9)
            return
        L_0x003b:
            int r3 = r9 + 1
            char r0 = (char) r0
            r8[r9] = r0
            r0 = r2
            r9 = r3
            goto L_0x0023
        L_0x0043:
            r7._inputPtr = r0
            goto L_0x0005
        L_0x0046:
            r2 = r4[r0]
            switch(r2) {
                case 1: goto L_0x0065;
                case 2: goto L_0x006b;
                case 3: goto L_0x0071;
                case 4: goto L_0x0085;
                default: goto L_0x004b;
            }
        L_0x004b:
            r2 = 32
            if (r0 >= r2) goto L_0x00a6
            java.lang.String r2 = "string value"
            r7._throwUnquotedSpace(r0, r2)
            r2 = r0
        L_0x0055:
            int r0 = r8.length
            if (r9 < r0) goto L_0x00ab
            com.fasterxml.jackson.core.util.TextBuffer r0 = r7._textBuffer
            char[] r8 = r0.finishCurrentSegment()
            r0 = r1
        L_0x005f:
            int r9 = r0 + 1
            char r2 = (char) r2
            r8[r0] = r2
            goto L_0x0005
        L_0x0065:
            char r0 = r7._decodeEscaped()
            r2 = r0
            goto L_0x0055
        L_0x006b:
            int r0 = r7._decodeUtf8_2(r0)
            r2 = r0
            goto L_0x0055
        L_0x0071:
            int r2 = r7._inputEnd
            int r3 = r7._inputPtr
            int r2 = r2 - r3
            r3 = 2
            if (r2 < r3) goto L_0x007f
            int r0 = r7._decodeUtf8_3fast(r0)
            r2 = r0
            goto L_0x0055
        L_0x007f:
            int r0 = r7._decodeUtf8_3(r0)
            r2 = r0
            goto L_0x0055
        L_0x0085:
            int r2 = r7._decodeUtf8_4(r0)
            int r0 = r9 + 1
            r3 = 55296(0xd800, float:7.7486E-41)
            int r6 = r2 >> 10
            r3 = r3 | r6
            char r3 = (char) r3
            r8[r9] = r3
            int r3 = r8.length
            if (r0 < r3) goto L_0x009e
            com.fasterxml.jackson.core.util.TextBuffer r0 = r7._textBuffer
            char[] r8 = r0.finishCurrentSegment()
            r0 = r1
        L_0x009e:
            r3 = 56320(0xdc00, float:7.8921E-41)
            r2 = r2 & 1023(0x3ff, float:1.434E-42)
            r2 = r2 | r3
            r9 = r0
            goto L_0x0055
        L_0x00a6:
            r7._reportInvalidChar(r0)
            r2 = r0
            goto L_0x0055
        L_0x00ab:
            r0 = r9
            goto L_0x005f
        */
        throw new UnsupportedOperationException("Method not decompiled: com.fasterxml.jackson.core.json.UTF8StreamJsonParser._finishString2(char[], int):void");
    }

    /* access modifiers changed from: protected */
    public void _skipString() throws IOException {
        this._tokenIncomplete = false;
        int[] iArr = _icUTF8;
        byte[] bArr = this._inputBuffer;
        while (true) {
            int i = this._inputPtr;
            int i2 = this._inputEnd;
            if (i >= i2) {
                loadMoreGuaranteed();
                i = this._inputPtr;
                i2 = this._inputEnd;
            }
            while (true) {
                if (i < i2) {
                    int i3 = i + 1;
                    byte b = bArr[i] & 255;
                    if (iArr[b] != 0) {
                        this._inputPtr = i3;
                        if (b != 34) {
                            switch (iArr[b]) {
                                case 1:
                                    _decodeEscaped();
                                    break;
                                case 2:
                                    _skipUtf8_2(b);
                                    break;
                                case 3:
                                    _skipUtf8_3(b);
                                    break;
                                case 4:
                                    _skipUtf8_4(b);
                                    break;
                                default:
                                    if (b >= 32) {
                                        _reportInvalidChar(b);
                                        break;
                                    } else {
                                        _throwUnquotedSpace(b, "string value");
                                        break;
                                    }
                            }
                        } else {
                            return;
                        }
                    } else {
                        i = i3;
                    }
                } else {
                    this._inputPtr = i;
                }
            }
        }
    }

    /* access modifiers changed from: protected */
    public JsonToken _handleUnexpectedValue(int i) throws IOException {
        switch (i) {
            case 39:
                break;
            case 43:
                if (this._inputPtr >= this._inputEnd && !loadMore()) {
                    _reportInvalidEOFInValue();
                }
                byte[] bArr = this._inputBuffer;
                int i2 = this._inputPtr;
                this._inputPtr = i2 + 1;
                return _handleInvalidNumberStart(bArr[i2] & 255, false);
            case 73:
                _matchToken("Infinity", 1);
                if (!isEnabled(Feature.ALLOW_NON_NUMERIC_NUMBERS)) {
                    _reportError("Non-standard token 'Infinity': enable JsonParser.Feature.ALLOW_NON_NUMERIC_NUMBERS to allow");
                    break;
                } else {
                    return resetAsNaN("Infinity", Double.POSITIVE_INFINITY);
                }
            case 78:
                _matchToken("NaN", 1);
                if (!isEnabled(Feature.ALLOW_NON_NUMERIC_NUMBERS)) {
                    _reportError("Non-standard token 'NaN': enable JsonParser.Feature.ALLOW_NON_NUMERIC_NUMBERS to allow");
                    break;
                } else {
                    return resetAsNaN("NaN", Double.NaN);
                }
            case 93:
            case 125:
                _reportUnexpectedChar(i, "expected a value");
                break;
        }
        if (isEnabled(Feature.ALLOW_SINGLE_QUOTES)) {
            return _handleApos();
        }
        if (Character.isJavaIdentifierStart(i)) {
            _reportInvalidToken("" + ((char) i), "('true', 'false' or 'null')");
        }
        _reportUnexpectedChar(i, "expected a valid value (number, String, array, object, 'true', 'false' or 'null')");
        return null;
    }

    /* access modifiers changed from: protected */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    public com.fasterxml.jackson.core.JsonToken _handleApos() throws java.io.IOException {
        /*
            r10 = this;
            r9 = 39
            r2 = 0
            com.fasterxml.jackson.core.util.TextBuffer r0 = r10._textBuffer
            char[] r0 = r0.emptyAndGetCurrentSegment()
            int[] r6 = _icUTF8
            byte[] r7 = r10._inputBuffer
            r1 = r2
        L_0x000e:
            int r3 = r10._inputPtr
            int r4 = r10._inputEnd
            if (r3 < r4) goto L_0x0017
            r10.loadMoreGuaranteed()
        L_0x0017:
            int r3 = r0.length
            if (r1 < r3) goto L_0x0021
            com.fasterxml.jackson.core.util.TextBuffer r0 = r10._textBuffer
            char[] r0 = r0.finishCurrentSegment()
            r1 = r2
        L_0x0021:
            int r4 = r10._inputEnd
            int r3 = r10._inputPtr
            int r5 = r0.length
            int r5 = r5 - r1
            int r3 = r3 + r5
            if (r3 >= r4) goto L_0x00b3
        L_0x002a:
            int r4 = r10._inputPtr
            if (r4 >= r3) goto L_0x000e
            int r4 = r10._inputPtr
            int r5 = r4 + 1
            r10._inputPtr = r5
            byte r4 = r7[r4]
            r5 = r4 & 255(0xff, float:3.57E-43)
            if (r5 == r9) goto L_0x003e
            r4 = r6[r5]
            if (r4 == 0) goto L_0x0048
        L_0x003e:
            if (r5 != r9) goto L_0x004f
            com.fasterxml.jackson.core.util.TextBuffer r0 = r10._textBuffer
            r0.setCurrentLength(r1)
            com.fasterxml.jackson.core.JsonToken r0 = com.fasterxml.jackson.core.JsonToken.VALUE_STRING
            return r0
        L_0x0048:
            int r4 = r1 + 1
            char r5 = (char) r5
            r0[r1] = r5
            r1 = r4
            goto L_0x002a
        L_0x004f:
            r3 = r6[r5]
            switch(r3) {
                case 1: goto L_0x0071;
                case 2: goto L_0x0078;
                case 3: goto L_0x007d;
                case 4: goto L_0x008f;
                default: goto L_0x0054;
            }
        L_0x0054:
            r3 = 32
            if (r5 >= r3) goto L_0x005d
            java.lang.String r3 = "string value"
            r10._throwUnquotedSpace(r5, r3)
        L_0x005d:
            r10._reportInvalidChar(r5)
        L_0x0060:
            r3 = r5
        L_0x0061:
            int r4 = r0.length
            if (r1 < r4) goto L_0x00af
            com.fasterxml.jackson.core.util.TextBuffer r0 = r10._textBuffer
            char[] r0 = r0.finishCurrentSegment()
            r4 = r2
        L_0x006b:
            int r1 = r4 + 1
            char r3 = (char) r3
            r0[r4] = r3
            goto L_0x000e
        L_0x0071:
            if (r5 == r9) goto L_0x0060
            char r3 = r10._decodeEscaped()
            goto L_0x0061
        L_0x0078:
            int r3 = r10._decodeUtf8_2(r5)
            goto L_0x0061
        L_0x007d:
            int r3 = r10._inputEnd
            int r4 = r10._inputPtr
            int r3 = r3 - r4
            r4 = 2
            if (r3 < r4) goto L_0x008a
            int r3 = r10._decodeUtf8_3fast(r5)
            goto L_0x0061
        L_0x008a:
            int r3 = r10._decodeUtf8_3(r5)
            goto L_0x0061
        L_0x008f:
            int r4 = r10._decodeUtf8_4(r5)
            int r3 = r1 + 1
            r5 = 55296(0xd800, float:7.7486E-41)
            int r8 = r4 >> 10
            r5 = r5 | r8
            char r5 = (char) r5
            r0[r1] = r5
            int r1 = r0.length
            if (r3 < r1) goto L_0x00b1
            com.fasterxml.jackson.core.util.TextBuffer r0 = r10._textBuffer
            char[] r0 = r0.finishCurrentSegment()
            r1 = r2
        L_0x00a8:
            r3 = 56320(0xdc00, float:7.8921E-41)
            r4 = r4 & 1023(0x3ff, float:1.434E-42)
            r3 = r3 | r4
            goto L_0x0061
        L_0x00af:
            r4 = r1
            goto L_0x006b
        L_0x00b1:
            r1 = r3
            goto L_0x00a8
        L_0x00b3:
            r3 = r4
            goto L_0x002a
        */
        throw new UnsupportedOperationException("Method not decompiled: com.fasterxml.jackson.core.json.UTF8StreamJsonParser._handleApos():com.fasterxml.jackson.core.JsonToken");
    }

    /* access modifiers changed from: protected */
    public JsonToken _handleInvalidNumberStart(int i, boolean z) throws IOException {
        int i2;
        String str;
        int i3 = i;
        while (true) {
            if (i3 != 73) {
                i2 = i3;
                break;
            }
            if (this._inputPtr >= this._inputEnd && !loadMore()) {
                _reportInvalidEOFInValue();
            }
            byte[] bArr = this._inputBuffer;
            int i4 = this._inputPtr;
            this._inputPtr = i4 + 1;
            byte b = bArr[i4];
            if (b != 78) {
                if (b != 110) {
                    i2 = b;
                    break;
                }
                str = z ? "-Infinity" : "+Infinity";
            } else {
                str = z ? "-INF" : "+INF";
            }
            _matchToken(str, 3);
            if (isEnabled(Feature.ALLOW_NON_NUMERIC_NUMBERS)) {
                return resetAsNaN(str, z ? Double.NEGATIVE_INFINITY : Double.POSITIVE_INFINITY);
            }
            _reportError("Non-standard token '" + str + "': enable JsonParser.Feature.ALLOW_NON_NUMERIC_NUMBERS to allow");
            i3 = b;
        }
        reportUnexpectedNumberChar(i2, "expected digit (0-9) to follow minus sign, for valid numeric value");
        return null;
    }

    /* access modifiers changed from: protected */
    public final void _matchToken(String str, int i) throws IOException {
        int length = str.length();
        if (this._inputPtr + length >= this._inputEnd) {
            _matchToken2(str, i);
            return;
        }
        do {
            if (this._inputBuffer[this._inputPtr] != str.charAt(i)) {
                _reportInvalidToken(str.substring(0, i));
            }
            this._inputPtr++;
            i++;
        } while (i < length);
        byte b = this._inputBuffer[this._inputPtr] & 255;
        if (b >= 48 && b != 93 && b != 125) {
            _checkMatchEnd(str, i, b);
        }
    }

    private final void _matchToken2(String str, int i) throws IOException {
        int length = str.length();
        do {
            if ((this._inputPtr >= this._inputEnd && !loadMore()) || this._inputBuffer[this._inputPtr] != str.charAt(i)) {
                _reportInvalidToken(str.substring(0, i));
            }
            this._inputPtr++;
            i++;
        } while (i < length);
        if (this._inputPtr < this._inputEnd || loadMore()) {
            byte b = this._inputBuffer[this._inputPtr] & 255;
            if (b >= 48 && b != 93 && b != 125) {
                _checkMatchEnd(str, i, b);
            }
        }
    }

    private final void _checkMatchEnd(String str, int i, int i2) throws IOException {
        if (Character.isJavaIdentifierPart((char) _decodeCharForError(i2))) {
            _reportInvalidToken(str.substring(0, i));
        }
    }

    private final int _skipWS() throws IOException {
        while (this._inputPtr < this._inputEnd) {
            byte[] bArr = this._inputBuffer;
            int i = this._inputPtr;
            this._inputPtr = i + 1;
            byte b = bArr[i] & 255;
            if (b > 32) {
                if (b != 47 && b != 35) {
                    return b;
                }
                this._inputPtr--;
                return _skipWS2();
            } else if (b != 32) {
                if (b == 10) {
                    this._currInputRow++;
                    this._currInputRowStart = this._inputPtr;
                } else if (b == 13) {
                    _skipCR();
                } else if (b != 9) {
                    _throwInvalidSpace(b);
                }
            }
        }
        return _skipWS2();
    }

    private final int _skipWS2() throws IOException {
        byte b;
        while (true) {
            if (this._inputPtr < this._inputEnd || loadMore()) {
                byte[] bArr = this._inputBuffer;
                int i = this._inputPtr;
                this._inputPtr = i + 1;
                b = bArr[i] & 255;
                if (b > 32) {
                    if (b == 47) {
                        _skipComment();
                    } else if (b != 35 || !_skipYAMLComment()) {
                        return b;
                    }
                } else if (b != 32) {
                    if (b == 10) {
                        this._currInputRow++;
                        this._currInputRowStart = this._inputPtr;
                    } else if (b == 13) {
                        _skipCR();
                    } else if (b != 9) {
                        _throwInvalidSpace(b);
                    }
                }
            } else {
                throw _constructError("Unexpected end-of-input within/between " + this._parsingContext.getTypeDesc() + " entries");
            }
        }
        return b;
    }

    private final int _skipWSOrEnd() throws IOException {
        if (this._inputPtr >= this._inputEnd && !loadMore()) {
            return _eofAsNextChar();
        }
        byte[] bArr = this._inputBuffer;
        int i = this._inputPtr;
        this._inputPtr = i + 1;
        byte b = bArr[i] & 255;
        if (b <= 32) {
            if (b != 32) {
                if (b == 10) {
                    this._currInputRow++;
                    this._currInputRowStart = this._inputPtr;
                } else if (b == 13) {
                    _skipCR();
                } else if (b != 9) {
                    _throwInvalidSpace(b);
                }
            }
            while (this._inputPtr < this._inputEnd) {
                byte[] bArr2 = this._inputBuffer;
                int i2 = this._inputPtr;
                this._inputPtr = i2 + 1;
                byte b2 = bArr2[i2] & 255;
                if (b2 > 32) {
                    if (b2 != 47 && b2 != 35) {
                        return b2;
                    }
                    this._inputPtr--;
                    return _skipWSOrEnd2();
                } else if (b2 != 32) {
                    if (b2 == 10) {
                        this._currInputRow++;
                        this._currInputRowStart = this._inputPtr;
                    } else if (b2 == 13) {
                        _skipCR();
                    } else if (b2 != 9) {
                        _throwInvalidSpace(b2);
                    }
                }
            }
            return _skipWSOrEnd2();
        } else if (b != 47 && b != 35) {
            return b;
        } else {
            this._inputPtr--;
            return _skipWSOrEnd2();
        }
    }

    private final int _skipWSOrEnd2() throws IOException {
        while (true) {
            if (this._inputPtr >= this._inputEnd && !loadMore()) {
                return _eofAsNextChar();
            }
            byte[] bArr = this._inputBuffer;
            int i = this._inputPtr;
            this._inputPtr = i + 1;
            byte b = bArr[i] & 255;
            if (b > 32) {
                if (b == 47) {
                    _skipComment();
                } else if (b != 35 || !_skipYAMLComment()) {
                    return b;
                }
            } else if (b != 32) {
                if (b == 10) {
                    this._currInputRow++;
                    this._currInputRowStart = this._inputPtr;
                } else if (b == 13) {
                    _skipCR();
                } else if (b != 9) {
                    _throwInvalidSpace(b);
                }
            }
        }
    }

    private final int _skipColon() throws IOException {
        if (this._inputPtr + 4 >= this._inputEnd) {
            return _skipColon2(false);
        }
        byte b = this._inputBuffer[this._inputPtr];
        if (b == 58) {
            byte[] bArr = this._inputBuffer;
            int i = this._inputPtr + 1;
            this._inputPtr = i;
            byte b2 = bArr[i];
            if (b2 <= 32) {
                if (b2 == 32 || b2 == 9) {
                    byte[] bArr2 = this._inputBuffer;
                    int i2 = this._inputPtr + 1;
                    this._inputPtr = i2;
                    byte b3 = bArr2[i2];
                    if (b3 > 32) {
                        if (b3 == 47 || b3 == 35) {
                            return _skipColon2(true);
                        }
                        this._inputPtr++;
                        return b3;
                    }
                }
                return _skipColon2(true);
            } else if (b2 == 47 || b2 == 35) {
                return _skipColon2(true);
            } else {
                this._inputPtr++;
                return b2;
            }
        } else {
            if (b == 32 || b == 9) {
                byte[] bArr3 = this._inputBuffer;
                int i3 = this._inputPtr + 1;
                this._inputPtr = i3;
                b = bArr3[i3];
            }
            if (b != 58) {
                return _skipColon2(false);
            }
            byte[] bArr4 = this._inputBuffer;
            int i4 = this._inputPtr + 1;
            this._inputPtr = i4;
            byte b4 = bArr4[i4];
            if (b4 <= 32) {
                if (b4 == 32 || b4 == 9) {
                    byte[] bArr5 = this._inputBuffer;
                    int i5 = this._inputPtr + 1;
                    this._inputPtr = i5;
                    byte b5 = bArr5[i5];
                    if (b5 > 32) {
                        if (b5 == 47 || b5 == 35) {
                            return _skipColon2(true);
                        }
                        this._inputPtr++;
                        return b5;
                    }
                }
                return _skipColon2(true);
            } else if (b4 == 47 || b4 == 35) {
                return _skipColon2(true);
            } else {
                this._inputPtr++;
                return b4;
            }
        }
    }

    private final int _skipColon2(boolean z) throws IOException {
        while (true) {
            if (this._inputPtr < this._inputEnd || loadMore()) {
                byte[] bArr = this._inputBuffer;
                int i = this._inputPtr;
                this._inputPtr = i + 1;
                byte b = bArr[i] & 255;
                if (b > 32) {
                    if (b == 47) {
                        _skipComment();
                    } else if (b != 35 || !_skipYAMLComment()) {
                        if (z) {
                            return b;
                        }
                        if (b != 58) {
                            if (b < 32) {
                                _throwInvalidSpace(b);
                            }
                            _reportUnexpectedChar(b, "was expecting a colon to separate field name and value");
                        }
                        z = true;
                    }
                } else if (b != 32) {
                    if (b == 10) {
                        this._currInputRow++;
                        this._currInputRowStart = this._inputPtr;
                    } else if (b == 13) {
                        _skipCR();
                    } else if (b != 9) {
                        _throwInvalidSpace(b);
                    }
                }
            } else {
                throw _constructError("Unexpected end-of-input within/between " + this._parsingContext.getTypeDesc() + " entries");
            }
        }
    }

    private final void _skipComment() throws IOException {
        if (!isEnabled(Feature.ALLOW_COMMENTS)) {
            _reportUnexpectedChar(47, "maybe a (non-standard) comment? (not recognized as one since Feature 'ALLOW_COMMENTS' not enabled for parser)");
        }
        if (this._inputPtr >= this._inputEnd && !loadMore()) {
            _reportInvalidEOF(" in a comment");
        }
        byte[] bArr = this._inputBuffer;
        int i = this._inputPtr;
        this._inputPtr = i + 1;
        byte b = bArr[i] & 255;
        if (b == 47) {
            _skipLine();
        } else if (b == 42) {
            _skipCComment();
        } else {
            _reportUnexpectedChar(b, "was expecting either '*' or '/' for a comment");
        }
    }

    private final void _skipCComment() throws IOException {
        int[] inputCodeComment = CharTypes.getInputCodeComment();
        while (true) {
            if (this._inputPtr < this._inputEnd || loadMore()) {
                byte[] bArr = this._inputBuffer;
                int i = this._inputPtr;
                this._inputPtr = i + 1;
                byte b = bArr[i] & 255;
                int i2 = inputCodeComment[b];
                if (i2 != 0) {
                    switch (i2) {
                        case 2:
                            _skipUtf8_2(b);
                            continue;
                        case 3:
                            _skipUtf8_3(b);
                            continue;
                        case 4:
                            _skipUtf8_4(b);
                            continue;
                        case 10:
                            this._currInputRow++;
                            this._currInputRowStart = this._inputPtr;
                            continue;
                        case 13:
                            _skipCR();
                            continue;
                        case 42:
                            if (this._inputPtr >= this._inputEnd && !loadMore()) {
                                break;
                            } else if (this._inputBuffer[this._inputPtr] == 47) {
                                this._inputPtr++;
                                return;
                            } else {
                                continue;
                            }
                        default:
                            _reportInvalidChar(b);
                            continue;
                    }
                }
            }
        }
        _reportInvalidEOF(" in a comment");
    }

    private final boolean _skipYAMLComment() throws IOException {
        if (!isEnabled(Feature.ALLOW_YAML_COMMENTS)) {
            return false;
        }
        _skipLine();
        return true;
    }

    private final void _skipLine() throws IOException {
        int[] inputCodeComment = CharTypes.getInputCodeComment();
        while (true) {
            if (this._inputPtr < this._inputEnd || loadMore()) {
                byte[] bArr = this._inputBuffer;
                int i = this._inputPtr;
                this._inputPtr = i + 1;
                byte b = bArr[i] & 255;
                int i2 = inputCodeComment[b];
                if (i2 != 0) {
                    switch (i2) {
                        case 2:
                            _skipUtf8_2(b);
                            break;
                        case 3:
                            _skipUtf8_3(b);
                            break;
                        case 4:
                            _skipUtf8_4(b);
                            break;
                        case 10:
                            this._currInputRow++;
                            this._currInputRowStart = this._inputPtr;
                            return;
                        case 13:
                            _skipCR();
                            return;
                        case 42:
                            break;
                        default:
                            if (i2 >= 0) {
                                break;
                            } else {
                                _reportInvalidChar(b);
                                break;
                            }
                    }
                }
            } else {
                return;
            }
        }
    }

    /* access modifiers changed from: protected */
    public char _decodeEscaped() throws IOException {
        int i = 0;
        if (this._inputPtr >= this._inputEnd && !loadMore()) {
            _reportInvalidEOF(" in character escape sequence");
        }
        byte[] bArr = this._inputBuffer;
        int i2 = this._inputPtr;
        this._inputPtr = i2 + 1;
        byte b = bArr[i2];
        switch (b) {
            case 34:
            case 47:
            case 92:
                return (char) b;
            case 98:
                return 8;
            case 102:
                return 12;
            case 110:
                return 10;
            case 114:
                return CharUtils.f1421CR;
            case 116:
                return 9;
            case 117:
                int i3 = 0;
                while (true) {
                    int i4 = i;
                    if (i3 >= 4) {
                        return (char) i4;
                    }
                    if (this._inputPtr >= this._inputEnd && !loadMore()) {
                        _reportInvalidEOF(" in character escape sequence");
                    }
                    byte[] bArr2 = this._inputBuffer;
                    int i5 = this._inputPtr;
                    this._inputPtr = i5 + 1;
                    byte b2 = bArr2[i5];
                    int charToHex = CharTypes.charToHex(b2);
                    if (charToHex < 0) {
                        _reportUnexpectedChar(b2, "expected a hex-digit for character escape sequence");
                    }
                    i = (i4 << 4) | charToHex;
                    i3++;
                }
            default:
                return _handleUnrecognizedCharacterEscape((char) _decodeCharForError(b));
        }
    }

    /* access modifiers changed from: protected */
    public int _decodeCharForError(int i) throws IOException {
        char c;
        int i2 = i & 255;
        if (i2 <= 127) {
            return i2;
        }
        if ((i2 & 224) == 192) {
            i2 &= 31;
            c = 1;
        } else if ((i2 & DisplaySupport.SCREEN_DENSITY_HIGH) == 224) {
            i2 &= 15;
            c = 2;
        } else if ((i2 & 248) == 240) {
            i2 &= 7;
            c = 3;
        } else {
            _reportInvalidInitial(i2 & 255);
            c = 1;
        }
        int nextByte = nextByte();
        if ((nextByte & 192) != 128) {
            _reportInvalidOther(nextByte & 255);
        }
        int i3 = (i2 << 6) | (nextByte & 63);
        if (c <= 1) {
            return i3;
        }
        int nextByte2 = nextByte();
        if ((nextByte2 & 192) != 128) {
            _reportInvalidOther(nextByte2 & 255);
        }
        int i4 = (i3 << 6) | (nextByte2 & 63);
        if (c <= 2) {
            return i4;
        }
        int nextByte3 = nextByte();
        if ((nextByte3 & 192) != 128) {
            _reportInvalidOther(nextByte3 & 255);
        }
        return (i4 << 6) | (nextByte3 & 63);
    }

    private final int _decodeUtf8_2(int i) throws IOException {
        if (this._inputPtr >= this._inputEnd) {
            loadMoreGuaranteed();
        }
        byte[] bArr = this._inputBuffer;
        int i2 = this._inputPtr;
        this._inputPtr = i2 + 1;
        byte b = bArr[i2];
        if ((b & 192) != 128) {
            _reportInvalidOther(b & 255, this._inputPtr);
        }
        return (b & 63) | ((i & 31) << 6);
    }

    private final int _decodeUtf8_3(int i) throws IOException {
        if (this._inputPtr >= this._inputEnd) {
            loadMoreGuaranteed();
        }
        int i2 = i & 15;
        byte[] bArr = this._inputBuffer;
        int i3 = this._inputPtr;
        this._inputPtr = i3 + 1;
        byte b = bArr[i3];
        if ((b & 192) != 128) {
            _reportInvalidOther(b & 255, this._inputPtr);
        }
        byte b2 = (i2 << 6) | (b & 63);
        if (this._inputPtr >= this._inputEnd) {
            loadMoreGuaranteed();
        }
        byte[] bArr2 = this._inputBuffer;
        int i4 = this._inputPtr;
        this._inputPtr = i4 + 1;
        byte b3 = bArr2[i4];
        if ((b3 & 192) != 128) {
            _reportInvalidOther(b3 & 255, this._inputPtr);
        }
        return (b2 << 6) | (b3 & 63);
    }

    private final int _decodeUtf8_3fast(int i) throws IOException {
        int i2 = i & 15;
        byte[] bArr = this._inputBuffer;
        int i3 = this._inputPtr;
        this._inputPtr = i3 + 1;
        byte b = bArr[i3];
        if ((b & 192) != 128) {
            _reportInvalidOther(b & 255, this._inputPtr);
        }
        byte b2 = (i2 << 6) | (b & 63);
        byte[] bArr2 = this._inputBuffer;
        int i4 = this._inputPtr;
        this._inputPtr = i4 + 1;
        byte b3 = bArr2[i4];
        if ((b3 & 192) != 128) {
            _reportInvalidOther(b3 & 255, this._inputPtr);
        }
        return (b2 << 6) | (b3 & 63);
    }

    private final int _decodeUtf8_4(int i) throws IOException {
        if (this._inputPtr >= this._inputEnd) {
            loadMoreGuaranteed();
        }
        byte[] bArr = this._inputBuffer;
        int i2 = this._inputPtr;
        this._inputPtr = i2 + 1;
        byte b = bArr[i2];
        if ((b & 192) != 128) {
            _reportInvalidOther(b & 255, this._inputPtr);
        }
        byte b2 = (b & 63) | ((i & 7) << 6);
        if (this._inputPtr >= this._inputEnd) {
            loadMoreGuaranteed();
        }
        byte[] bArr2 = this._inputBuffer;
        int i3 = this._inputPtr;
        this._inputPtr = i3 + 1;
        byte b3 = bArr2[i3];
        if ((b3 & 192) != 128) {
            _reportInvalidOther(b3 & 255, this._inputPtr);
        }
        byte b4 = (b2 << 6) | (b3 & 63);
        if (this._inputPtr >= this._inputEnd) {
            loadMoreGuaranteed();
        }
        byte[] bArr3 = this._inputBuffer;
        int i4 = this._inputPtr;
        this._inputPtr = i4 + 1;
        byte b5 = bArr3[i4];
        if ((b5 & 192) != 128) {
            _reportInvalidOther(b5 & 255, this._inputPtr);
        }
        return ((b4 << 6) | (b5 & 63)) - SerializedEventUnion.NONE;
    }

    private final void _skipUtf8_2(int i) throws IOException {
        if (this._inputPtr >= this._inputEnd) {
            loadMoreGuaranteed();
        }
        byte[] bArr = this._inputBuffer;
        int i2 = this._inputPtr;
        this._inputPtr = i2 + 1;
        byte b = bArr[i2];
        if ((b & 192) != 128) {
            _reportInvalidOther(b & 255, this._inputPtr);
        }
    }

    private final void _skipUtf8_3(int i) throws IOException {
        if (this._inputPtr >= this._inputEnd) {
            loadMoreGuaranteed();
        }
        byte[] bArr = this._inputBuffer;
        int i2 = this._inputPtr;
        this._inputPtr = i2 + 1;
        byte b = bArr[i2];
        if ((b & 192) != 128) {
            _reportInvalidOther(b & 255, this._inputPtr);
        }
        if (this._inputPtr >= this._inputEnd) {
            loadMoreGuaranteed();
        }
        byte[] bArr2 = this._inputBuffer;
        int i3 = this._inputPtr;
        this._inputPtr = i3 + 1;
        byte b2 = bArr2[i3];
        if ((b2 & 192) != 128) {
            _reportInvalidOther(b2 & 255, this._inputPtr);
        }
    }

    private final void _skipUtf8_4(int i) throws IOException {
        if (this._inputPtr >= this._inputEnd) {
            loadMoreGuaranteed();
        }
        byte[] bArr = this._inputBuffer;
        int i2 = this._inputPtr;
        this._inputPtr = i2 + 1;
        byte b = bArr[i2];
        if ((b & 192) != 128) {
            _reportInvalidOther(b & 255, this._inputPtr);
        }
        if (this._inputPtr >= this._inputEnd) {
            loadMoreGuaranteed();
        }
        byte[] bArr2 = this._inputBuffer;
        int i3 = this._inputPtr;
        this._inputPtr = i3 + 1;
        byte b2 = bArr2[i3];
        if ((b2 & 192) != 128) {
            _reportInvalidOther(b2 & 255, this._inputPtr);
        }
        if (this._inputPtr >= this._inputEnd) {
            loadMoreGuaranteed();
        }
        byte[] bArr3 = this._inputBuffer;
        int i4 = this._inputPtr;
        this._inputPtr = i4 + 1;
        byte b3 = bArr3[i4];
        if ((b3 & 192) != 128) {
            _reportInvalidOther(b3 & 255, this._inputPtr);
        }
    }

    /* access modifiers changed from: protected */
    public final void _skipCR() throws IOException {
        if ((this._inputPtr < this._inputEnd || loadMore()) && this._inputBuffer[this._inputPtr] == 10) {
            this._inputPtr++;
        }
        this._currInputRow++;
        this._currInputRowStart = this._inputPtr;
    }

    private int nextByte() throws IOException {
        if (this._inputPtr >= this._inputEnd) {
            loadMoreGuaranteed();
        }
        byte[] bArr = this._inputBuffer;
        int i = this._inputPtr;
        this._inputPtr = i + 1;
        return bArr[i] & 255;
    }

    /* access modifiers changed from: protected */
    public void _reportInvalidToken(String str) throws IOException {
        _reportInvalidToken(str, "'null', 'true', 'false' or NaN");
    }

    /* access modifiers changed from: protected */
    public void _reportInvalidToken(String str, String str2) throws IOException {
        StringBuilder sb = new StringBuilder(str);
        while (true) {
            if (this._inputPtr >= this._inputEnd && !loadMore()) {
                break;
            }
            byte[] bArr = this._inputBuffer;
            int i = this._inputPtr;
            this._inputPtr = i + 1;
            char _decodeCharForError = (char) _decodeCharForError(bArr[i]);
            if (!Character.isJavaIdentifierPart(_decodeCharForError)) {
                break;
            }
            sb.append(_decodeCharForError);
        }
        _reportError("Unrecognized token '" + sb.toString() + "': was expecting " + str2);
    }

    /* access modifiers changed from: protected */
    public void _reportInvalidChar(int i) throws JsonParseException {
        if (i < 32) {
            _throwInvalidSpace(i);
        }
        _reportInvalidInitial(i);
    }

    /* access modifiers changed from: protected */
    public void _reportInvalidInitial(int i) throws JsonParseException {
        _reportError("Invalid UTF-8 start byte 0x" + Integer.toHexString(i));
    }

    /* access modifiers changed from: protected */
    public void _reportInvalidOther(int i) throws JsonParseException {
        _reportError("Invalid UTF-8 middle byte 0x" + Integer.toHexString(i));
    }

    /* access modifiers changed from: protected */
    public void _reportInvalidOther(int i, int i2) throws JsonParseException {
        this._inputPtr = i2;
        _reportInvalidOther(i);
    }

    public static int[] growArrayBy(int[] iArr, int i) {
        if (iArr == null) {
            return new int[i];
        }
        return Arrays.copyOf(iArr, iArr.length + i);
    }

    /* access modifiers changed from: protected */
    public final byte[] _decodeBase64(Base64Variant base64Variant) throws IOException {
        ByteArrayBuilder _getByteArrayBuilder = _getByteArrayBuilder();
        while (true) {
            if (this._inputPtr >= this._inputEnd) {
                loadMoreGuaranteed();
            }
            byte[] bArr = this._inputBuffer;
            int i = this._inputPtr;
            this._inputPtr = i + 1;
            byte b = bArr[i] & 255;
            if (b > 32) {
                int decodeBase64Char = base64Variant.decodeBase64Char((int) b);
                if (decodeBase64Char < 0) {
                    if (b == 34) {
                        return _getByteArrayBuilder.toByteArray();
                    }
                    decodeBase64Char = _decodeBase64Escape(base64Variant, (int) b, 0);
                    if (decodeBase64Char < 0) {
                        continue;
                    }
                }
                if (this._inputPtr >= this._inputEnd) {
                    loadMoreGuaranteed();
                }
                byte[] bArr2 = this._inputBuffer;
                int i2 = this._inputPtr;
                this._inputPtr = i2 + 1;
                byte b2 = bArr2[i2] & 255;
                int decodeBase64Char2 = base64Variant.decodeBase64Char((int) b2);
                if (decodeBase64Char2 < 0) {
                    decodeBase64Char2 = _decodeBase64Escape(base64Variant, (int) b2, 1);
                }
                int i3 = decodeBase64Char2 | (decodeBase64Char << 6);
                if (this._inputPtr >= this._inputEnd) {
                    loadMoreGuaranteed();
                }
                byte[] bArr3 = this._inputBuffer;
                int i4 = this._inputPtr;
                this._inputPtr = i4 + 1;
                byte b3 = bArr3[i4] & 255;
                int decodeBase64Char3 = base64Variant.decodeBase64Char((int) b3);
                if (decodeBase64Char3 < 0) {
                    if (decodeBase64Char3 != -2) {
                        if (b3 != 34 || base64Variant.usesPadding()) {
                            decodeBase64Char3 = _decodeBase64Escape(base64Variant, (int) b3, 2);
                        } else {
                            _getByteArrayBuilder.append(i3 >> 4);
                            return _getByteArrayBuilder.toByteArray();
                        }
                    }
                    if (decodeBase64Char3 == -2) {
                        if (this._inputPtr >= this._inputEnd) {
                            loadMoreGuaranteed();
                        }
                        byte[] bArr4 = this._inputBuffer;
                        int i5 = this._inputPtr;
                        this._inputPtr = i5 + 1;
                        byte b4 = bArr4[i5] & 255;
                        if (!base64Variant.usesPaddingChar((int) b4)) {
                            throw reportInvalidBase64Char(base64Variant, b4, 3, "expected padding character '" + base64Variant.getPaddingChar() + "'");
                        }
                        _getByteArrayBuilder.append(i3 >> 4);
                    }
                }
                int i6 = (i3 << 6) | decodeBase64Char3;
                if (this._inputPtr >= this._inputEnd) {
                    loadMoreGuaranteed();
                }
                byte[] bArr5 = this._inputBuffer;
                int i7 = this._inputPtr;
                this._inputPtr = i7 + 1;
                byte b5 = bArr5[i7] & 255;
                int decodeBase64Char4 = base64Variant.decodeBase64Char((int) b5);
                if (decodeBase64Char4 < 0) {
                    if (decodeBase64Char4 != -2) {
                        if (b5 != 34 || base64Variant.usesPadding()) {
                            decodeBase64Char4 = _decodeBase64Escape(base64Variant, (int) b5, 3);
                        } else {
                            _getByteArrayBuilder.appendTwoBytes(i6 >> 2);
                            return _getByteArrayBuilder.toByteArray();
                        }
                    }
                    if (decodeBase64Char4 == -2) {
                        _getByteArrayBuilder.appendTwoBytes(i6 >> 2);
                    }
                }
                _getByteArrayBuilder.appendThreeBytes(decodeBase64Char4 | (i6 << 6));
            }
        }
    }

    public JsonLocation getTokenLocation() {
        Object sourceReference = this._ioContext.getSourceReference();
        if (this._currToken == JsonToken.FIELD_NAME) {
            return new JsonLocation(sourceReference, this._currInputProcessed + ((long) (this._nameStartOffset - 1)), -1, this._nameStartRow, this._nameStartCol);
        }
        return new JsonLocation(sourceReference, this._tokenInputTotal - 1, -1, this._tokenInputRow, this._tokenInputCol);
    }

    public JsonLocation getCurrentLocation() {
        return new JsonLocation(this._ioContext.getSourceReference(), this._currInputProcessed + ((long) this._inputPtr), -1, this._currInputRow, (this._inputPtr - this._currInputRowStart) + 1);
    }

    private final void _updateLocation() {
        this._tokenInputRow = this._currInputRow;
        int i = this._inputPtr;
        this._tokenInputTotal = this._currInputProcessed + ((long) i);
        this._tokenInputCol = i - this._currInputRowStart;
    }

    private final void _updateNameLocation() {
        this._nameStartRow = this._currInputRow;
        int i = this._inputPtr;
        this._nameStartOffset = i;
        this._nameStartCol = i - this._currInputRowStart;
    }

    private static final int pad(int i, int i2) {
        return i2 == 4 ? i : i | (-1 << (i2 << 3));
    }
}
