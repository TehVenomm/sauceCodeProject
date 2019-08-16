package com.fasterxml.jackson.core.json;

import com.facebook.internal.ServerProtocol;
import com.fasterxml.jackson.core.Base64Variant;
import com.fasterxml.jackson.core.JsonLocation;
import com.fasterxml.jackson.core.JsonParser.Feature;
import com.fasterxml.jackson.core.JsonToken;
import com.fasterxml.jackson.core.ObjectCodec;
import com.fasterxml.jackson.core.SerializableString;
import com.fasterxml.jackson.core.base.ParserBase;
import com.fasterxml.jackson.core.p015io.CharTypes;
import com.fasterxml.jackson.core.p015io.IOContext;
import com.fasterxml.jackson.core.sym.CharsToNameCanonicalizer;
import com.fasterxml.jackson.core.util.ByteArrayBuilder;
import com.fasterxml.jackson.core.util.TextBuffer;
import java.io.IOException;
import java.io.OutputStream;
import java.io.Reader;
import java.io.Writer;
import org.apache.commons.lang3.CharUtils;

public class ReaderBasedJsonParser extends ParserBase {
    protected static final int[] _icLatin1 = CharTypes.getInputCodeLatin1();
    protected boolean _bufferRecyclable;
    protected final int _hashSeed;
    protected char[] _inputBuffer;
    protected int _nameStartCol;
    protected long _nameStartOffset;
    protected int _nameStartRow;
    protected ObjectCodec _objectCodec;
    protected Reader _reader;
    protected final CharsToNameCanonicalizer _symbols;
    protected boolean _tokenIncomplete;

    public ReaderBasedJsonParser(IOContext iOContext, int i, Reader reader, ObjectCodec objectCodec, CharsToNameCanonicalizer charsToNameCanonicalizer, char[] cArr, int i2, int i3, boolean z) {
        super(iOContext, i);
        this._reader = reader;
        this._inputBuffer = cArr;
        this._inputPtr = i2;
        this._inputEnd = i3;
        this._objectCodec = objectCodec;
        this._symbols = charsToNameCanonicalizer;
        this._hashSeed = charsToNameCanonicalizer.hashSeed();
        this._bufferRecyclable = z;
    }

    public ReaderBasedJsonParser(IOContext iOContext, int i, Reader reader, ObjectCodec objectCodec, CharsToNameCanonicalizer charsToNameCanonicalizer) {
        super(iOContext, i);
        this._reader = reader;
        this._inputBuffer = iOContext.allocTokenBuffer();
        this._inputPtr = 0;
        this._inputEnd = 0;
        this._objectCodec = objectCodec;
        this._symbols = charsToNameCanonicalizer;
        this._hashSeed = charsToNameCanonicalizer.hashSeed();
        this._bufferRecyclable = true;
    }

    public ObjectCodec getCodec() {
        return this._objectCodec;
    }

    public void setCodec(ObjectCodec objectCodec) {
        this._objectCodec = objectCodec;
    }

    public int releaseBuffered(Writer writer) throws IOException {
        int i = this._inputEnd - this._inputPtr;
        if (i < 1) {
            return 0;
        }
        writer.write(this._inputBuffer, this._inputPtr, i);
        return i;
    }

    public Object getInputSource() {
        return this._reader;
    }

    /* access modifiers changed from: protected */
    public boolean loadMore() throws IOException {
        int i = this._inputEnd;
        this._currInputProcessed += (long) i;
        this._currInputRowStart -= i;
        this._nameStartOffset -= (long) i;
        if (this._reader == null) {
            return false;
        }
        int read = this._reader.read(this._inputBuffer, 0, this._inputBuffer.length);
        if (read > 0) {
            this._inputPtr = 0;
            this._inputEnd = read;
            return true;
        }
        _closeInput();
        if (read != 0) {
            return false;
        }
        throw new IOException("Reader returned 0 characters when trying to read " + this._inputEnd);
    }

    /* access modifiers changed from: protected */
    public char getNextChar(String str) throws IOException {
        if (this._inputPtr >= this._inputEnd && !loadMore()) {
            _reportInvalidEOF(str);
        }
        char[] cArr = this._inputBuffer;
        int i = this._inputPtr;
        this._inputPtr = i + 1;
        return cArr[i];
    }

    /* access modifiers changed from: protected */
    public void _closeInput() throws IOException {
        if (this._reader != null) {
            if (this._ioContext.isResourceManaged() || isEnabled(Feature.AUTO_CLOSE_SOURCE)) {
                this._reader.close();
            }
            this._reader = null;
        }
    }

    /* access modifiers changed from: protected */
    public void _releaseBuffers() throws IOException {
        super._releaseBuffers();
        this._symbols.release();
        if (this._bufferRecyclable) {
            char[] cArr = this._inputBuffer;
            if (cArr != null) {
                this._inputBuffer = null;
                this._ioContext.releaseTokenBuffer(cArr);
            }
        }
    }

    public final String getText() throws IOException {
        JsonToken jsonToken = this._currToken;
        if (jsonToken != JsonToken.VALUE_STRING) {
            return _getText2(jsonToken);
        }
        if (this._tokenIncomplete) {
            this._tokenIncomplete = false;
            _finishString();
        }
        return this._textBuffer.contentsAsString();
    }

    public final String getValueAsString() throws IOException {
        if (this._currToken == JsonToken.VALUE_STRING) {
            if (this._tokenIncomplete) {
                this._tokenIncomplete = false;
                _finishString();
            }
            return this._textBuffer.contentsAsString();
        } else if (this._currToken == JsonToken.FIELD_NAME) {
            return getCurrentName();
        } else {
            return super.getValueAsString(null);
        }
    }

    public final String getValueAsString(String str) throws IOException {
        if (this._currToken == JsonToken.VALUE_STRING) {
            if (this._tokenIncomplete) {
                this._tokenIncomplete = false;
                _finishString();
            }
            return this._textBuffer.contentsAsString();
        } else if (this._currToken == JsonToken.FIELD_NAME) {
            return getCurrentName();
        } else {
            return super.getValueAsString(str);
        }
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

    public final char[] getTextCharacters() throws IOException {
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

    public final int getTextLength() throws IOException {
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

    public final int getTextOffset() throws IOException {
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
            char[] cArr = this._inputBuffer;
            int i4 = this._inputPtr;
            this._inputPtr = i4 + 1;
            char c = cArr[i4];
            if (c > ' ') {
                int decodeBase64Char = base64Variant.decodeBase64Char(c);
                if (decodeBase64Char < 0) {
                    if (c == '\"') {
                        break;
                    }
                    decodeBase64Char = _decodeBase64Escape(base64Variant, c, 0);
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
                char[] cArr2 = this._inputBuffer;
                int i6 = this._inputPtr;
                this._inputPtr = i6 + 1;
                char c2 = cArr2[i6];
                int decodeBase64Char2 = base64Variant.decodeBase64Char(c2);
                if (decodeBase64Char2 < 0) {
                    decodeBase64Char2 = _decodeBase64Escape(base64Variant, c2, 1);
                }
                int i7 = (i5 << 6) | decodeBase64Char2;
                if (this._inputPtr >= this._inputEnd) {
                    loadMoreGuaranteed();
                }
                char[] cArr3 = this._inputBuffer;
                int i8 = this._inputPtr;
                this._inputPtr = i8 + 1;
                char c3 = cArr3[i8];
                int decodeBase64Char3 = base64Variant.decodeBase64Char(c3);
                if (decodeBase64Char3 < 0) {
                    if (decodeBase64Char3 != -2) {
                        if (c3 == '\"' && !base64Variant.usesPadding()) {
                            i3 = i + 1;
                            bArr[i] = (byte) (i7 >> 4);
                            break;
                        }
                        decodeBase64Char3 = _decodeBase64Escape(base64Variant, c3, 2);
                    }
                    if (decodeBase64Char3 == -2) {
                        if (this._inputPtr >= this._inputEnd) {
                            loadMoreGuaranteed();
                        }
                        char[] cArr4 = this._inputBuffer;
                        int i9 = this._inputPtr;
                        this._inputPtr = i9 + 1;
                        char c4 = cArr4[i9];
                        if (!base64Variant.usesPaddingChar(c4)) {
                            throw reportInvalidBase64Char(base64Variant, c4, 3, "expected padding character '" + base64Variant.getPaddingChar() + "'");
                        }
                        i3 = i + 1;
                        bArr[i] = (byte) (i7 >> 4);
                    }
                }
                int i10 = (i7 << 6) | decodeBase64Char3;
                if (this._inputPtr >= this._inputEnd) {
                    loadMoreGuaranteed();
                }
                char[] cArr5 = this._inputBuffer;
                int i11 = this._inputPtr;
                this._inputPtr = i11 + 1;
                char c5 = cArr5[i11];
                int decodeBase64Char4 = base64Variant.decodeBase64Char(c5);
                if (decodeBase64Char4 < 0) {
                    if (decodeBase64Char4 != -2) {
                        if (c5 == '\"' && !base64Variant.usesPadding()) {
                            int i12 = i10 >> 2;
                            int i13 = i + 1;
                            bArr[i] = (byte) (i12 >> 8);
                            i3 = i13 + 1;
                            bArr[i13] = (byte) i12;
                            break;
                        }
                        decodeBase64Char4 = _decodeBase64Escape(base64Variant, c5, 3);
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

    public final JsonToken nextToken() throws IOException {
        JsonToken _parsePosNumber;
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
            JsonToken jsonToken = JsonToken.END_ARRAY;
            this._currToken = jsonToken;
            return jsonToken;
        } else if (_skipWSOrEnd == 125) {
            _updateLocation();
            if (!this._parsingContext.inObject()) {
                _reportMismatchedEndMarker(_skipWSOrEnd, ']');
            }
            this._parsingContext = this._parsingContext.clearAndGetParent();
            JsonToken jsonToken2 = JsonToken.END_OBJECT;
            this._currToken = jsonToken2;
            return jsonToken2;
        } else {
            if (this._parsingContext.expectComma()) {
                _skipWSOrEnd = _skipComma(_skipWSOrEnd);
            }
            boolean inObject = this._parsingContext.inObject();
            if (inObject) {
                _updateNameLocation();
                this._parsingContext.setCurrentName(_skipWSOrEnd == 34 ? _parseName() : _handleOddName(_skipWSOrEnd));
                this._currToken = JsonToken.FIELD_NAME;
                _skipWSOrEnd = _skipColon();
            }
            _updateLocation();
            switch (_skipWSOrEnd) {
                case 34:
                    this._tokenIncomplete = true;
                    _parsePosNumber = JsonToken.VALUE_STRING;
                    break;
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
                    _parsePosNumber = _parsePosNumber(_skipWSOrEnd);
                    break;
                case 91:
                    if (!inObject) {
                        this._parsingContext = this._parsingContext.createChildArrayContext(this._tokenInputRow, this._tokenInputCol);
                    }
                    _parsePosNumber = JsonToken.START_ARRAY;
                    break;
                case 93:
                case 125:
                    _reportUnexpectedChar(_skipWSOrEnd, "expected a value");
                    break;
                case 102:
                    _matchFalse();
                    _parsePosNumber = JsonToken.VALUE_FALSE;
                    break;
                case 110:
                    _matchNull();
                    _parsePosNumber = JsonToken.VALUE_NULL;
                    break;
                case 116:
                    break;
                case 123:
                    if (!inObject) {
                        this._parsingContext = this._parsingContext.createChildObjectContext(this._tokenInputRow, this._tokenInputCol);
                    }
                    _parsePosNumber = JsonToken.START_OBJECT;
                    break;
                default:
                    _parsePosNumber = _handleOddValue(_skipWSOrEnd);
                    break;
            }
            _matchTrue();
            _parsePosNumber = JsonToken.VALUE_TRUE;
            if (inObject) {
                this._nextToken = _parsePosNumber;
                return this._currToken;
            }
            this._currToken = _parsePosNumber;
            return _parsePosNumber;
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
                _skipWSOrEnd = _skipComma(_skipWSOrEnd);
            }
            if (!this._parsingContext.inObject()) {
                _updateLocation();
                _nextTokenNotInObject(_skipWSOrEnd);
                return false;
            }
            _updateNameLocation();
            if (_skipWSOrEnd == 34) {
                char[] asQuotedChars = serializableString.asQuotedChars();
                int length = asQuotedChars.length;
                if (this._inputPtr + length + 4 < this._inputEnd) {
                    int i = this._inputPtr + length;
                    if (this._inputBuffer[i] == '\"') {
                        int i2 = this._inputPtr;
                        int i3 = 0;
                        while (i2 != i) {
                            if (asQuotedChars[i3] == this._inputBuffer[i2]) {
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
            return _isNextTokenNameMaybe(_skipWSOrEnd, serializableString.getValue());
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
                _skipWSOrEnd = _skipComma(_skipWSOrEnd);
            }
            if (!this._parsingContext.inObject()) {
                _updateLocation();
                _nextTokenNotInObject(_skipWSOrEnd);
                return null;
            }
            _updateNameLocation();
            String _handleOddName = _skipWSOrEnd == 34 ? _parseName() : _handleOddName(_skipWSOrEnd);
            this._parsingContext.setCurrentName(_handleOddName);
            this._currToken = JsonToken.FIELD_NAME;
            int _skipColon = _skipColon();
            _updateLocation();
            if (_skipColon == 34) {
                this._tokenIncomplete = true;
                this._nextToken = JsonToken.VALUE_STRING;
                return _handleOddName;
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
                    _matchFalse();
                    jsonToken = JsonToken.VALUE_FALSE;
                    break;
                case 110:
                    _matchNull();
                    jsonToken = JsonToken.VALUE_NULL;
                    break;
                case 116:
                    _matchTrue();
                    jsonToken = JsonToken.VALUE_TRUE;
                    break;
                case 123:
                    jsonToken = JsonToken.START_OBJECT;
                    break;
                default:
                    jsonToken = _handleOddValue(_skipColon);
                    break;
            }
            this._nextToken = jsonToken;
            return _handleOddName;
        }
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
                this._nextToken = _handleOddValue(i);
                return;
        }
    }

    /* access modifiers changed from: protected */
    public boolean _isNextTokenNameMaybe(int i, String str) throws IOException {
        JsonToken jsonToken;
        String _handleOddName = i == 34 ? _parseName() : _handleOddName(i);
        this._parsingContext.setCurrentName(_handleOddName);
        this._currToken = JsonToken.FIELD_NAME;
        int _skipColon = _skipColon();
        _updateLocation();
        if (_skipColon == 34) {
            this._tokenIncomplete = true;
            this._nextToken = JsonToken.VALUE_STRING;
            return str.equals(_handleOddName);
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
                _matchFalse();
                jsonToken = JsonToken.VALUE_FALSE;
                break;
            case 110:
                _matchNull();
                jsonToken = JsonToken.VALUE_NULL;
                break;
            case 116:
                _matchTrue();
                jsonToken = JsonToken.VALUE_TRUE;
                break;
            case 123:
                jsonToken = JsonToken.START_OBJECT;
                break;
            default:
                jsonToken = _handleOddValue(_skipColon);
                break;
        }
        this._nextToken = jsonToken;
        return str.equals(_handleOddName);
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
                JsonToken _handleOddValue = _handleOddValue(i);
                this._currToken = _handleOddValue;
                return _handleOddValue;
        }
    }

    public final String nextTextValue() throws IOException {
        if (this._currToken == JsonToken.FIELD_NAME) {
            this._nameCopied = false;
            JsonToken jsonToken = this._nextToken;
            this._nextToken = null;
            this._currToken = jsonToken;
            if (jsonToken == JsonToken.VALUE_STRING) {
                if (this._tokenIncomplete) {
                    this._tokenIncomplete = false;
                    _finishString();
                }
                return this._textBuffer.contentsAsString();
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

    public final int nextIntValue(int i) throws IOException {
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

    public final long nextLongValue(long j) throws IOException {
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

    public final Boolean nextBooleanValue() throws IOException {
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
            if (nextToken == null) {
                return null;
            }
            int id = nextToken.mo9113id();
            if (id == 9) {
                return Boolean.TRUE;
            }
            if (id == 10) {
                return Boolean.FALSE;
            }
            return null;
        }
    }

    /* access modifiers changed from: protected */
    public final JsonToken _parsePosNumber(int i) throws IOException {
        int i2 = this._inputPtr;
        int i3 = i2 - 1;
        int i4 = this._inputEnd;
        if (i == 48) {
            return _parseNumber2(false, i3);
        }
        int i5 = 1;
        int i6 = i2;
        while (i6 < i4) {
            int i7 = i6 + 1;
            char c = this._inputBuffer[i6];
            if (c >= '0' && c <= '9') {
                i5++;
                i6 = i7;
            } else if (c == '.' || c == 'e' || c == 'E') {
                this._inputPtr = i7;
                return _parseFloat(c, i3, i7, false, i5);
            } else {
                int i8 = i7 - 1;
                this._inputPtr = i8;
                if (this._parsingContext.inRoot()) {
                    _verifyRootSpace(c);
                }
                this._textBuffer.resetWithShared(this._inputBuffer, i3, i8 - i3);
                return resetInt(false, i5);
            }
        }
        this._inputPtr = i3;
        return _parseNumber2(false, i3);
    }

    /* JADX WARNING: type inference failed for: r9v0, types: [int] */
    /* JADX WARNING: type inference failed for: r2v0 */
    /* JADX WARNING: type inference failed for: r2v1 */
    /* JADX WARNING: type inference failed for: r2v2, types: [int] */
    /* JADX WARNING: type inference failed for: r2v4, types: [char[]] */
    /* JADX WARNING: type inference failed for: r2v5, types: [char] */
    /* JADX WARNING: type inference failed for: r2v6 */
    /* JADX WARNING: type inference failed for: r2v7, types: [int] */
    /* JADX WARNING: type inference failed for: r2v8, types: [char[]] */
    /* JADX WARNING: type inference failed for: r2v9, types: [char] */
    /* JADX WARNING: type inference failed for: r2v10, types: [char[]] */
    /* JADX WARNING: type inference failed for: r2v11, types: [char] */
    /* JADX WARNING: type inference failed for: r2v12, types: [char[]] */
    /* JADX WARNING: type inference failed for: r2v13, types: [int, char] */
    /* JADX WARNING: type inference failed for: r2v14 */
    /* JADX WARNING: type inference failed for: r2v15 */
    /* JADX WARNING: type inference failed for: r2v16 */
    /* JADX WARNING: type inference failed for: r2v17 */
    /* JADX WARNING: type inference failed for: r2v18 */
    /* JADX WARNING: Incorrect type for immutable var: ssa=char, code=null, for r2v11, types: [char] */
    /* JADX WARNING: Incorrect type for immutable var: ssa=char, code=null, for r2v13, types: [int, char] */
    /* JADX WARNING: Incorrect type for immutable var: ssa=char, code=null, for r2v5, types: [char] */
    /* JADX WARNING: Incorrect type for immutable var: ssa=char, code=null, for r2v9, types: [char] */
    /* JADX WARNING: Incorrect type for immutable var: ssa=char[], code=null, for r2v10, types: [char[]] */
    /* JADX WARNING: Incorrect type for immutable var: ssa=char[], code=null, for r2v12, types: [char[]] */
    /* JADX WARNING: Incorrect type for immutable var: ssa=char[], code=null, for r2v4, types: [char[]] */
    /* JADX WARNING: Incorrect type for immutable var: ssa=char[], code=null, for r2v8, types: [char[]] */
    /* JADX WARNING: Incorrect type for immutable var: ssa=int, code=null, for r9v0, types: [int] */
    /* JADX WARNING: Multi-variable type inference failed. Error: jadx.core.utils.exceptions.JadxRuntimeException: No candidate types for var: r2v6
      assigns: []
      uses: []
      mth insns count: 71
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
    /* JADX WARNING: Unknown variable types count: 12 */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    private final com.fasterxml.jackson.core.JsonToken _parseFloat(int r9, int r10, int r11, boolean r12, int r13) throws java.io.IOException {
        /*
            r8 = this;
            r7 = 57
            r6 = 48
            r0 = 0
            int r5 = r8._inputEnd
            r1 = 46
            if (r9 != r1) goto L_0x0093
            r3 = r0
        L_0x000c:
            if (r11 < r5) goto L_0x0013
            com.fasterxml.jackson.core.JsonToken r0 = r8._parseNumber2(r12, r10)
        L_0x0012:
            return r0
        L_0x0013:
            char[] r2 = r8._inputBuffer
            int r1 = r11 + 1
            char r2 = r2[r11]
            if (r2 < r6) goto L_0x001d
            if (r2 <= r7) goto L_0x0036
        L_0x001d:
            if (r3 != 0) goto L_0x0024
            java.lang.String r4 = "Decimal point not followed by a digit"
            r8.reportUnexpectedNumberChar(r2, r4)
        L_0x0024:
            r4 = r3
        L_0x0025:
            r3 = 101(0x65, float:1.42E-43)
            if (r2 == r3) goto L_0x002d
            r3 = 69
            if (r2 != r3) goto L_0x0075
        L_0x002d:
            if (r1 < r5) goto L_0x003a
            r8._inputPtr = r10
            com.fasterxml.jackson.core.JsonToken r0 = r8._parseNumber2(r12, r10)
            goto L_0x0012
        L_0x0036:
            int r3 = r3 + 1
            r11 = r1
            goto L_0x000c
        L_0x003a:
            char[] r2 = r8._inputBuffer
            int r3 = r1 + 1
            char r2 = r2[r1]
            r1 = 45
            if (r2 == r1) goto L_0x0048
            r1 = 43
            if (r2 != r1) goto L_0x0091
        L_0x0048:
            if (r3 < r5) goto L_0x0051
            r8._inputPtr = r10
            com.fasterxml.jackson.core.JsonToken r0 = r8._parseNumber2(r12, r10)
            goto L_0x0012
        L_0x0051:
            char[] r2 = r8._inputBuffer
            int r1 = r3 + 1
            char r2 = r2[r3]
        L_0x0057:
            if (r2 > r7) goto L_0x006e
            if (r2 < r6) goto L_0x006e
            int r0 = r0 + 1
            if (r1 < r5) goto L_0x0066
            r8._inputPtr = r10
            com.fasterxml.jackson.core.JsonToken r0 = r8._parseNumber2(r12, r10)
            goto L_0x0012
        L_0x0066:
            char[] r2 = r8._inputBuffer
            int r3 = r1 + 1
            char r2 = r2[r1]
            r1 = r3
            goto L_0x0057
        L_0x006e:
            if (r0 != 0) goto L_0x0075
            java.lang.String r3 = "Exponent indicator not followed by a digit"
            r8.reportUnexpectedNumberChar(r2, r3)
        L_0x0075:
            int r1 = r1 + -1
            r8._inputPtr = r1
            com.fasterxml.jackson.core.json.JsonReadContext r3 = r8._parsingContext
            boolean r3 = r3.inRoot()
            if (r3 == 0) goto L_0x0084
            r8._verifyRootSpace(r2)
        L_0x0084:
            int r1 = r1 - r10
            com.fasterxml.jackson.core.util.TextBuffer r2 = r8._textBuffer
            char[] r3 = r8._inputBuffer
            r2.resetWithShared(r3, r10, r1)
            com.fasterxml.jackson.core.JsonToken r0 = r8.resetFloat(r12, r13, r4, r0)
            goto L_0x0012
        L_0x0091:
            r1 = r3
            goto L_0x0057
        L_0x0093:
            r4 = r0
            r1 = r11
            r2 = r9
            goto L_0x0025
        */
        throw new UnsupportedOperationException("Method not decompiled: com.fasterxml.jackson.core.json.ReaderBasedJsonParser._parseFloat(int, int, int, boolean, int):com.fasterxml.jackson.core.JsonToken");
    }

    /* access modifiers changed from: protected */
    public final JsonToken _parseNegNumber() throws IOException {
        int i = this._inputPtr;
        int i2 = i - 1;
        int i3 = this._inputEnd;
        if (i >= i3) {
            return _parseNumber2(true, i2);
        }
        int i4 = i + 1;
        char c = this._inputBuffer[i];
        if (c > '9' || c < '0') {
            this._inputPtr = i4;
            return _handleInvalidNumberStart(c, true);
        } else if (c == '0') {
            return _parseNumber2(true, i2);
        } else {
            int i5 = 1;
            int i6 = i4;
            while (i6 < i3) {
                int i7 = i6 + 1;
                char c2 = this._inputBuffer[i6];
                if (c2 >= '0' && c2 <= '9') {
                    i5++;
                    i6 = i7;
                } else if (c2 == '.' || c2 == 'e' || c2 == 'E') {
                    this._inputPtr = i7;
                    return _parseFloat(c2, i2, i7, true, i5);
                } else {
                    int i8 = i7 - 1;
                    this._inputPtr = i8;
                    if (this._parsingContext.inRoot()) {
                        _verifyRootSpace(c2);
                    }
                    this._textBuffer.resetWithShared(this._inputBuffer, i2, i8 - i2);
                    return resetInt(true, i5);
                }
            }
            return _parseNumber2(true, i2);
        }
    }

    private final JsonToken _parseNumber2(boolean z, int i) throws IOException {
        int i2;
        char nextChar;
        int i3;
        int i4;
        char[] cArr;
        int i5;
        boolean z2;
        char c;
        int i6;
        int i7;
        char c2;
        boolean z3;
        int i8;
        int i9;
        char[] cArr2;
        int i10;
        int i11;
        char nextChar2;
        int i12;
        char[] cArr3;
        char[] cArr4;
        if (z) {
            i++;
        }
        this._inputPtr = i;
        char[] emptyAndGetCurrentSegment = this._textBuffer.emptyAndGetCurrentSegment();
        if (z) {
            i2 = 1;
            emptyAndGetCurrentSegment[0] = '-';
        } else {
            i2 = 0;
        }
        int i13 = 0;
        if (this._inputPtr < this._inputEnd) {
            char[] cArr5 = this._inputBuffer;
            int i14 = this._inputPtr;
            this._inputPtr = i14 + 1;
            nextChar = cArr5[i14];
        } else {
            nextChar = getNextChar("No digit following minus sign");
        }
        if (nextChar == '0') {
            nextChar = _verifyNoLeadingZeroes();
        }
        boolean z4 = false;
        char c3 = nextChar;
        while (true) {
            if (c3 >= '0' && c3 <= '9') {
                i13++;
                if (i2 >= emptyAndGetCurrentSegment.length) {
                    cArr4 = this._textBuffer.finishCurrentSegment();
                    i2 = 0;
                } else {
                    cArr4 = emptyAndGetCurrentSegment;
                }
                i4 = i2 + 1;
                cArr4[i2] = c3;
                if (this._inputPtr >= this._inputEnd && !loadMore()) {
                    c3 = 0;
                    z4 = true;
                    i3 = i13;
                    cArr = cArr4;
                    break;
                }
                char[] cArr6 = this._inputBuffer;
                int i15 = this._inputPtr;
                this._inputPtr = i15 + 1;
                c3 = cArr6[i15];
                i2 = i4;
                emptyAndGetCurrentSegment = cArr4;
            } else {
                i3 = i13;
                i4 = i2;
                cArr = emptyAndGetCurrentSegment;
            }
        }
        i3 = i13;
        i4 = i2;
        cArr = emptyAndGetCurrentSegment;
        if (i3 == 0) {
            return _handleInvalidNumberStart(c3, z);
        }
        int i16 = 0;
        if (c3 == '.') {
            int i17 = i4 + 1;
            cArr[i4] = c3;
            char c4 = c3;
            while (true) {
                if (this._inputPtr >= this._inputEnd && !loadMore()) {
                    z2 = true;
                    c = c4;
                    break;
                }
                char[] cArr7 = this._inputBuffer;
                int i18 = this._inputPtr;
                this._inputPtr = i18 + 1;
                c4 = cArr7[i18];
                if (c4 < '0') {
                    z2 = z4;
                    c = c4;
                    break;
                } else if (c4 > '9') {
                    z2 = z4;
                    c = c4;
                    break;
                } else {
                    i16++;
                    if (i17 >= cArr.length) {
                        cArr3 = this._textBuffer.finishCurrentSegment();
                        i12 = 0;
                    } else {
                        i12 = i17;
                        cArr3 = cArr;
                    }
                    i17 = i12 + 1;
                    cArr3[i12] = c4;
                    cArr = cArr3;
                }
            }
            if (i16 == 0) {
                reportUnexpectedNumberChar(c, "Decimal point not followed by a digit");
            }
            i5 = i16;
            i6 = i17;
        } else {
            i5 = 0;
            z2 = z4;
            c = c3;
            i6 = i4;
        }
        int i19 = 0;
        if (c == 'e' || c == 'E') {
            if (i7 >= cArr.length) {
                i9 = 0;
                cArr2 = this._textBuffer.finishCurrentSegment();
            } else {
                i9 = i7;
                cArr2 = cArr;
            }
            i7 = i9 + 1;
            cArr2[i9] = c;
            if (this._inputPtr < this._inputEnd) {
                char[] cArr8 = this._inputBuffer;
                int i20 = this._inputPtr;
                this._inputPtr = i20 + 1;
                c2 = cArr8[i20];
            } else {
                c2 = getNextChar("expected a digit for number exponent");
            }
            if (c2 == '-' || c2 == '+') {
                if (i7 >= cArr2.length) {
                    cArr2 = this._textBuffer.finishCurrentSegment();
                    i11 = 0;
                } else {
                    i11 = i7;
                }
                i7 = i11 + 1;
                cArr2[i11] = c2;
                if (this._inputPtr < this._inputEnd) {
                    char[] cArr9 = this._inputBuffer;
                    int i21 = this._inputPtr;
                    this._inputPtr = i21 + 1;
                    nextChar2 = cArr9[i21];
                } else {
                    nextChar2 = getNextChar("expected a digit for number exponent");
                }
                c2 = nextChar2;
            }
            while (true) {
                if (c2 <= '9' && c2 >= '0') {
                    i19++;
                    if (i7 >= cArr2.length) {
                        i10 = 0;
                        cArr2 = this._textBuffer.finishCurrentSegment();
                    } else {
                        i10 = i7;
                    }
                    i7 = i10 + 1;
                    cArr2[i10] = c2;
                    if (this._inputPtr >= this._inputEnd && !loadMore()) {
                        i8 = i19;
                        z3 = true;
                        break;
                    }
                    char[] cArr10 = this._inputBuffer;
                    int i22 = this._inputPtr;
                    this._inputPtr = i22 + 1;
                    c2 = cArr10[i22];
                } else {
                    i8 = i19;
                    z3 = z2;
                }
            }
            i8 = i19;
            z3 = z2;
            if (i8 == 0) {
                reportUnexpectedNumberChar(c2, "Exponent indicator not followed by a digit");
            }
        } else {
            i8 = 0;
            z3 = z2;
            c2 = c;
        }
        if (!z3) {
            this._inputPtr--;
            if (this._parsingContext.inRoot()) {
                _verifyRootSpace(c2);
            }
        }
        this._textBuffer.setCurrentLength(i7);
        return reset(z, i3, i5, i8);
    }

    private final char _verifyNoLeadingZeroes() throws IOException {
        if (this._inputPtr < this._inputEnd) {
            char c = this._inputBuffer[this._inputPtr];
            if (c < '0' || c > '9') {
                return '0';
            }
        }
        return _verifyNLZ2();
    }

    private char _verifyNLZ2() throws IOException {
        if (this._inputPtr >= this._inputEnd && !loadMore()) {
            return '0';
        }
        char c = this._inputBuffer[this._inputPtr];
        if (c < '0' || c > '9') {
            return '0';
        }
        if (!isEnabled(Feature.ALLOW_NUMERIC_LEADING_ZEROS)) {
            reportInvalidNumber("Leading zeroes not allowed");
        }
        this._inputPtr++;
        if (c != '0') {
            return c;
        }
        do {
            if (this._inputPtr >= this._inputEnd && !loadMore()) {
                return c;
            }
            c = this._inputBuffer[this._inputPtr];
            if (c < '0' || c > '9') {
                return '0';
            }
            this._inputPtr++;
        } while (c == '0');
        return c;
    }

    /* access modifiers changed from: protected */
    /* JADX WARNING: Incorrect type for immutable var: ssa=int, code=char, for r9v0, types: [int] */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    public com.fasterxml.jackson.core.JsonToken _handleInvalidNumberStart(char r9, boolean r10) throws java.io.IOException {
        /*
            r8 = this;
            r7 = 3
            r4 = 9218868437227405312(0x7ff0000000000000, double:Infinity)
            r2 = -4503599627370496(0xfff0000000000000, double:-Infinity)
            r0 = 73
            if (r9 != r0) goto L_0x005d
            int r0 = r8._inputPtr
            int r1 = r8._inputEnd
            if (r0 < r1) goto L_0x0018
            boolean r0 = r8.loadMore()
            if (r0 != 0) goto L_0x0018
            r8._reportInvalidEOFInValue()
        L_0x0018:
            char[] r0 = r8._inputBuffer
            int r1 = r8._inputPtr
            int r6 = r1 + 1
            r8._inputPtr = r6
            char r9 = r0[r1]
            r0 = 78
            if (r9 != r0) goto L_0x0064
            if (r10 == 0) goto L_0x003c
            java.lang.String r0 = "-INF"
        L_0x002a:
            r8._matchToken(r0, r7)
            com.fasterxml.jackson.core.JsonParser$Feature r1 = com.fasterxml.jackson.core.JsonParser.Feature.ALLOW_NON_NUMERIC_NUMBERS
            boolean r1 = r8.isEnabled(r1)
            if (r1 == 0) goto L_0x0041
            if (r10 == 0) goto L_0x003f
        L_0x0037:
            com.fasterxml.jackson.core.JsonToken r0 = r8.resetAsNaN(r0, r2)
        L_0x003b:
            return r0
        L_0x003c:
            java.lang.String r0 = "+INF"
            goto L_0x002a
        L_0x003f:
            r2 = r4
            goto L_0x0037
        L_0x0041:
            java.lang.StringBuilder r1 = new java.lang.StringBuilder
            r1.<init>()
            java.lang.String r2 = "Non-standard token '"
            java.lang.StringBuilder r1 = r1.append(r2)
            java.lang.StringBuilder r0 = r1.append(r0)
            java.lang.String r1 = "': enable JsonParser.Feature.ALLOW_NON_NUMERIC_NUMBERS to allow"
            java.lang.StringBuilder r0 = r0.append(r1)
            java.lang.String r0 = r0.toString()
            r8._reportError(r0)
        L_0x005d:
            java.lang.String r0 = "expected digit (0-9) to follow minus sign, for valid numeric value"
            r8.reportUnexpectedNumberChar(r9, r0)
            r0 = 0
            goto L_0x003b
        L_0x0064:
            r0 = 110(0x6e, float:1.54E-43)
            if (r9 != r0) goto L_0x005d
            if (r10 == 0) goto L_0x007e
            java.lang.String r0 = "-Infinity"
        L_0x006c:
            r8._matchToken(r0, r7)
            com.fasterxml.jackson.core.JsonParser$Feature r1 = com.fasterxml.jackson.core.JsonParser.Feature.ALLOW_NON_NUMERIC_NUMBERS
            boolean r1 = r8.isEnabled(r1)
            if (r1 == 0) goto L_0x0083
            if (r10 == 0) goto L_0x0081
        L_0x0079:
            com.fasterxml.jackson.core.JsonToken r0 = r8.resetAsNaN(r0, r2)
            goto L_0x003b
        L_0x007e:
            java.lang.String r0 = "+Infinity"
            goto L_0x006c
        L_0x0081:
            r2 = r4
            goto L_0x0079
        L_0x0083:
            java.lang.StringBuilder r1 = new java.lang.StringBuilder
            r1.<init>()
            java.lang.String r2 = "Non-standard token '"
            java.lang.StringBuilder r1 = r1.append(r2)
            java.lang.StringBuilder r0 = r1.append(r0)
            java.lang.String r1 = "': enable JsonParser.Feature.ALLOW_NON_NUMERIC_NUMBERS to allow"
            java.lang.StringBuilder r0 = r0.append(r1)
            java.lang.String r0 = r0.toString()
            r8._reportError(r0)
            goto L_0x005d
        */
        throw new UnsupportedOperationException("Method not decompiled: com.fasterxml.jackson.core.json.ReaderBasedJsonParser._handleInvalidNumberStart(int, boolean):com.fasterxml.jackson.core.JsonToken");
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
    public final String _parseName() throws IOException {
        int i = this._inputPtr;
        int i2 = this._hashSeed;
        int[] iArr = _icLatin1;
        while (true) {
            if (i >= this._inputEnd) {
                break;
            }
            char c = this._inputBuffer[i];
            if (c >= iArr.length || iArr[c] == 0) {
                i2 = (i2 * 33) + c;
                i++;
            } else if (c == '\"') {
                int i3 = this._inputPtr;
                this._inputPtr = i + 1;
                return this._symbols.findSymbol(this._inputBuffer, i3, i - i3, i2);
            }
        }
        int i4 = this._inputPtr;
        this._inputPtr = i;
        return _parseName2(i4, i2, 34);
    }

    private String _parseName2(int i, int i2, int i3) throws IOException {
        this._textBuffer.resetWithShared(this._inputBuffer, i, this._inputPtr - i);
        char[] currentSegment = this._textBuffer.getCurrentSegment();
        int currentSegmentSize = this._textBuffer.getCurrentSegmentSize();
        while (true) {
            if (this._inputPtr >= this._inputEnd && !loadMore()) {
                _reportInvalidEOF(": was expecting closing '" + ((char) i3) + "' for name");
            }
            char[] cArr = this._inputBuffer;
            int i4 = this._inputPtr;
            this._inputPtr = i4 + 1;
            char c = cArr[i4];
            if (c <= '\\') {
                if (c == '\\') {
                    c = _decodeEscaped();
                } else if (c <= i3) {
                    if (c == i3) {
                        this._textBuffer.setCurrentLength(currentSegmentSize);
                        TextBuffer textBuffer = this._textBuffer;
                        return this._symbols.findSymbol(textBuffer.getTextBuffer(), textBuffer.getTextOffset(), textBuffer.size(), i2);
                    } else if (c < ' ') {
                        _throwUnquotedSpace(c, "name");
                    }
                }
            }
            i2 = (i2 * 33) + c;
            int i5 = currentSegmentSize + 1;
            currentSegment[currentSegmentSize] = c;
            if (i5 >= currentSegment.length) {
                currentSegment = this._textBuffer.finishCurrentSegment();
                currentSegmentSize = 0;
            } else {
                currentSegmentSize = i5;
            }
        }
    }

    /* access modifiers changed from: protected */
    public String _handleOddName(int i) throws IOException {
        if (i == 39 && isEnabled(Feature.ALLOW_SINGLE_QUOTES)) {
            return _parseAposName();
        }
        if (!isEnabled(Feature.ALLOW_UNQUOTED_FIELD_NAMES)) {
            _reportUnexpectedChar(i, "was expecting double-quote to start field name");
        }
        int[] inputCodeLatin1JsNames = CharTypes.getInputCodeLatin1JsNames();
        int length = inputCodeLatin1JsNames.length;
        boolean isJavaIdentifierPart = i < length ? inputCodeLatin1JsNames[i] == 0 : Character.isJavaIdentifierPart((char) i);
        if (!isJavaIdentifierPart) {
            _reportUnexpectedChar(i, "was expecting either valid name character (for unquoted name) or double-quote (for quoted) to start field name");
        }
        int i2 = this._inputPtr;
        int i3 = this._hashSeed;
        int i4 = this._inputEnd;
        if (i2 < i4) {
            do {
                char c = this._inputBuffer[i2];
                if (c < length) {
                    if (inputCodeLatin1JsNames[c] != 0) {
                        int i5 = this._inputPtr - 1;
                        this._inputPtr = i2;
                        return this._symbols.findSymbol(this._inputBuffer, i5, i2 - i5, i3);
                    }
                } else if (!Character.isJavaIdentifierPart((char) c)) {
                    int i6 = this._inputPtr - 1;
                    this._inputPtr = i2;
                    return this._symbols.findSymbol(this._inputBuffer, i6, i2 - i6, i3);
                }
                i3 = (i3 * 33) + c;
                i2++;
            } while (i2 < i4);
        }
        int i7 = this._inputPtr - 1;
        this._inputPtr = i2;
        return _handleOddName2(i7, i3, inputCodeLatin1JsNames);
    }

    /* access modifiers changed from: protected */
    public String _parseAposName() throws IOException {
        int i = this._inputPtr;
        int i2 = this._hashSeed;
        int i3 = this._inputEnd;
        if (i < i3) {
            int[] iArr = _icLatin1;
            int length = iArr.length;
            do {
                char c = this._inputBuffer[i];
                if (c != '\'') {
                    if (c < length && iArr[c] != 0) {
                        break;
                    }
                    i2 = (i2 * 33) + c;
                    i++;
                } else {
                    int i4 = this._inputPtr;
                    this._inputPtr = i + 1;
                    return this._symbols.findSymbol(this._inputBuffer, i4, i - i4, i2);
                }
            } while (i < i3);
        }
        int i5 = this._inputPtr;
        this._inputPtr = i;
        return _parseName2(i5, i2, 39);
    }

    /* access modifiers changed from: protected */
    public JsonToken _handleOddValue(int i) throws IOException {
        switch (i) {
            case 39:
                if (isEnabled(Feature.ALLOW_SINGLE_QUOTES)) {
                    return _handleApos();
                }
                break;
            case 43:
                if (this._inputPtr >= this._inputEnd && !loadMore()) {
                    _reportInvalidEOFInValue();
                }
                char[] cArr = this._inputBuffer;
                int i2 = this._inputPtr;
                this._inputPtr = i2 + 1;
                return _handleInvalidNumberStart(cArr[i2], false);
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
        }
        if (Character.isJavaIdentifierStart(i)) {
            _reportInvalidToken("" + ((char) i), "('true', 'false' or 'null')");
        }
        _reportUnexpectedChar(i, "expected a valid value (number, String, array, object, 'true', 'false' or 'null')");
        return null;
    }

    /* access modifiers changed from: protected */
    public JsonToken _handleApos() throws IOException {
        char[] emptyAndGetCurrentSegment = this._textBuffer.emptyAndGetCurrentSegment();
        int currentSegmentSize = this._textBuffer.getCurrentSegmentSize();
        while (true) {
            if (this._inputPtr >= this._inputEnd && !loadMore()) {
                _reportInvalidEOF(": was expecting closing quote for a string value");
            }
            char[] cArr = this._inputBuffer;
            int i = this._inputPtr;
            this._inputPtr = i + 1;
            char c = cArr[i];
            if (c <= '\\') {
                if (c == '\\') {
                    c = _decodeEscaped();
                } else if (c <= '\'') {
                    if (c == '\'') {
                        this._textBuffer.setCurrentLength(currentSegmentSize);
                        return JsonToken.VALUE_STRING;
                    } else if (c < ' ') {
                        _throwUnquotedSpace(c, "string value");
                    }
                }
            }
            if (currentSegmentSize >= emptyAndGetCurrentSegment.length) {
                emptyAndGetCurrentSegment = this._textBuffer.finishCurrentSegment();
                currentSegmentSize = 0;
            }
            int i2 = currentSegmentSize;
            currentSegmentSize = i2 + 1;
            emptyAndGetCurrentSegment[i2] = c;
        }
    }

    private String _handleOddName2(int i, int i2, int[] iArr) throws IOException {
        this._textBuffer.resetWithShared(this._inputBuffer, i, this._inputPtr - i);
        char[] currentSegment = this._textBuffer.getCurrentSegment();
        int currentSegmentSize = this._textBuffer.getCurrentSegmentSize();
        int length = iArr.length;
        while (true) {
            if (this._inputPtr >= this._inputEnd && !loadMore()) {
                break;
            }
            char c = this._inputBuffer[this._inputPtr];
            if (c > length) {
                if (!Character.isJavaIdentifierPart(c)) {
                    break;
                }
            } else if (iArr[c] != 0) {
                break;
            }
            this._inputPtr++;
            i2 = (i2 * 33) + c;
            int i3 = currentSegmentSize + 1;
            currentSegment[currentSegmentSize] = c;
            if (i3 >= currentSegment.length) {
                currentSegment = this._textBuffer.finishCurrentSegment();
                currentSegmentSize = 0;
            } else {
                currentSegmentSize = i3;
            }
        }
        this._textBuffer.setCurrentLength(currentSegmentSize);
        TextBuffer textBuffer = this._textBuffer;
        return this._symbols.findSymbol(textBuffer.getTextBuffer(), textBuffer.getTextOffset(), textBuffer.size(), i2);
    }

    /* access modifiers changed from: protected */
    public final void _finishString() throws IOException {
        int i = this._inputPtr;
        int i2 = this._inputEnd;
        if (i < i2) {
            int[] iArr = _icLatin1;
            int length = iArr.length;
            while (true) {
                char c = this._inputBuffer[i];
                if (c >= length || iArr[c] == 0) {
                    i++;
                    if (i >= i2) {
                        break;
                    }
                } else if (c == '\"') {
                    this._textBuffer.resetWithShared(this._inputBuffer, this._inputPtr, i - this._inputPtr);
                    this._inputPtr = i + 1;
                    return;
                }
            }
        }
        this._textBuffer.resetWithCopy(this._inputBuffer, this._inputPtr, i - this._inputPtr);
        this._inputPtr = i;
        _finishString2();
    }

    /* access modifiers changed from: protected */
    public void _finishString2() throws IOException {
        char[] currentSegment = this._textBuffer.getCurrentSegment();
        int currentSegmentSize = this._textBuffer.getCurrentSegmentSize();
        int[] iArr = _icLatin1;
        int length = iArr.length;
        while (true) {
            if (this._inputPtr >= this._inputEnd && !loadMore()) {
                _reportInvalidEOF(": was expecting closing quote for a string value");
            }
            char[] cArr = this._inputBuffer;
            int i = this._inputPtr;
            this._inputPtr = i + 1;
            char c = cArr[i];
            if (c < length && iArr[c] != 0) {
                if (c == '\"') {
                    this._textBuffer.setCurrentLength(currentSegmentSize);
                    return;
                } else if (c == '\\') {
                    c = _decodeEscaped();
                } else if (c < ' ') {
                    _throwUnquotedSpace(c, "string value");
                }
            }
            if (currentSegmentSize >= currentSegment.length) {
                currentSegment = this._textBuffer.finishCurrentSegment();
                currentSegmentSize = 0;
            }
            int i2 = currentSegmentSize;
            currentSegmentSize = i2 + 1;
            currentSegment[i2] = c;
        }
    }

    /* access modifiers changed from: protected */
    public final void _skipString() throws IOException {
        this._tokenIncomplete = false;
        int i = this._inputPtr;
        int i2 = this._inputEnd;
        char[] cArr = this._inputBuffer;
        while (true) {
            if (i >= i2) {
                this._inputPtr = i;
                if (!loadMore()) {
                    _reportInvalidEOF(": was expecting closing quote for a string value");
                }
                i = this._inputPtr;
                i2 = this._inputEnd;
            }
            int i3 = i + 1;
            char c = cArr[i];
            if (c <= '\\') {
                if (c == '\\') {
                    this._inputPtr = i3;
                    _decodeEscaped();
                    i = this._inputPtr;
                    i2 = this._inputEnd;
                } else if (c <= '\"') {
                    if (c == '\"') {
                        this._inputPtr = i3;
                        return;
                    } else if (c < ' ') {
                        this._inputPtr = i3;
                        _throwUnquotedSpace(c, "string value");
                    }
                }
            }
            i = i3;
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

    private final int _skipColon() throws IOException {
        if (this._inputPtr + 4 >= this._inputEnd) {
            return _skipColon2(false);
        }
        char c = this._inputBuffer[this._inputPtr];
        if (c == ':') {
            char[] cArr = this._inputBuffer;
            int i = this._inputPtr + 1;
            this._inputPtr = i;
            char c2 = cArr[i];
            if (c2 <= ' ') {
                if (c2 == ' ' || c2 == 9) {
                    char[] cArr2 = this._inputBuffer;
                    int i2 = this._inputPtr + 1;
                    this._inputPtr = i2;
                    char c3 = cArr2[i2];
                    if (c3 > ' ') {
                        if (c3 == '/' || c3 == '#') {
                            return _skipColon2(true);
                        }
                        this._inputPtr++;
                        return c3;
                    }
                }
                return _skipColon2(true);
            } else if (c2 == '/' || c2 == '#') {
                return _skipColon2(true);
            } else {
                this._inputPtr++;
                return c2;
            }
        } else {
            if (c == ' ' || c == 9) {
                char[] cArr3 = this._inputBuffer;
                int i3 = this._inputPtr + 1;
                this._inputPtr = i3;
                c = cArr3[i3];
            }
            if (c != ':') {
                return _skipColon2(false);
            }
            char[] cArr4 = this._inputBuffer;
            int i4 = this._inputPtr + 1;
            this._inputPtr = i4;
            char c4 = cArr4[i4];
            if (c4 <= ' ') {
                if (c4 == ' ' || c4 == 9) {
                    char[] cArr5 = this._inputBuffer;
                    int i5 = this._inputPtr + 1;
                    this._inputPtr = i5;
                    char c5 = cArr5[i5];
                    if (c5 > ' ') {
                        if (c5 == '/' || c5 == '#') {
                            return _skipColon2(true);
                        }
                        this._inputPtr++;
                        return c5;
                    }
                }
                return _skipColon2(true);
            } else if (c4 == '/' || c4 == '#') {
                return _skipColon2(true);
            } else {
                this._inputPtr++;
                return c4;
            }
        }
    }

    private final int _skipColon2(boolean z) throws IOException {
        while (true) {
            if (this._inputPtr >= this._inputEnd) {
                loadMoreGuaranteed();
            }
            char[] cArr = this._inputBuffer;
            int i = this._inputPtr;
            this._inputPtr = i + 1;
            char c = cArr[i];
            if (c > ' ') {
                if (c == '/') {
                    _skipComment();
                } else if (c != '#' || !_skipYAMLComment()) {
                    if (z) {
                        return c;
                    }
                    if (c != ':') {
                        if (c < ' ') {
                            _throwInvalidSpace(c);
                        }
                        _reportUnexpectedChar(c, "was expecting a colon to separate field name and value");
                    }
                    z = true;
                }
            } else if (c < ' ') {
                if (c == 10) {
                    this._currInputRow++;
                    this._currInputRowStart = this._inputPtr;
                } else if (c == 13) {
                    _skipCR();
                } else if (c != 9) {
                    _throwInvalidSpace(c);
                }
            }
        }
    }

    private final int _skipColonFast(int i) throws IOException {
        int i2;
        boolean z = true;
        int i3 = i + 1;
        char c = this._inputBuffer[i];
        if (c == ':') {
            int i4 = i3 + 1;
            char c2 = this._inputBuffer[i3];
            if (c2 > ' ') {
                if (!(c2 == '/' || c2 == '#')) {
                    this._inputPtr = i4;
                    return c2;
                }
            } else if (c2 == ' ' || c2 == 9) {
                int i5 = i4 + 1;
                char c3 = this._inputBuffer[i4];
                if (c3 <= ' ' || c3 == '/' || c3 == '#') {
                    i4 = i5;
                } else {
                    this._inputPtr = i5;
                    return c3;
                }
            }
            this._inputPtr = i4 - 1;
            return _skipColon2(true);
        }
        if (c == ' ' || c == 9) {
            int i6 = i3 + 1;
            c = this._inputBuffer[i3];
            i3 = i6;
        }
        if (c != ':') {
            z = false;
        }
        if (z) {
            i2 = i3 + 1;
            char c4 = this._inputBuffer[i3];
            if (c4 > ' ') {
                if (!(c4 == '/' || c4 == '#')) {
                    this._inputPtr = i2;
                    return c4;
                }
            } else if (c4 == ' ' || c4 == 9) {
                i3 = i2 + 1;
                char c5 = this._inputBuffer[i2];
                if (!(c5 <= ' ' || c5 == '/' || c5 == '#')) {
                    this._inputPtr = i3;
                    return c5;
                }
            }
            this._inputPtr = i2 - 1;
            return _skipColon2(z);
        }
        i2 = i3;
        this._inputPtr = i2 - 1;
        return _skipColon2(z);
    }

    private final int _skipComma(int i) throws IOException {
        if (i != 44) {
            _reportUnexpectedChar(i, "was expecting comma to separate " + this._parsingContext.getTypeDesc() + " entries");
        }
        while (this._inputPtr < this._inputEnd) {
            char[] cArr = this._inputBuffer;
            int i2 = this._inputPtr;
            this._inputPtr = i2 + 1;
            char c = cArr[i2];
            if (c > ' ') {
                if (c != '/' && c != '#') {
                    return c;
                }
                this._inputPtr--;
                return _skipAfterComma2();
            } else if (c < ' ') {
                if (c == 10) {
                    this._currInputRow++;
                    this._currInputRowStart = this._inputPtr;
                } else if (c == 13) {
                    _skipCR();
                } else if (c != 9) {
                    _throwInvalidSpace(c);
                }
            }
        }
        return _skipAfterComma2();
    }

    private final int _skipAfterComma2() throws IOException {
        char c;
        while (true) {
            if (this._inputPtr < this._inputEnd || loadMore()) {
                char[] cArr = this._inputBuffer;
                int i = this._inputPtr;
                this._inputPtr = i + 1;
                c = cArr[i];
                if (c > ' ') {
                    if (c == '/') {
                        _skipComment();
                    } else if (c != '#' || !_skipYAMLComment()) {
                        return c;
                    }
                } else if (c < ' ') {
                    if (c == 10) {
                        this._currInputRow++;
                        this._currInputRowStart = this._inputPtr;
                    } else if (c == 13) {
                        _skipCR();
                    } else if (c != 9) {
                        _throwInvalidSpace(c);
                    }
                }
            } else {
                throw _constructError("Unexpected end-of-input within/between " + this._parsingContext.getTypeDesc() + " entries");
            }
        }
        return c;
    }

    private final int _skipWSOrEnd() throws IOException {
        if (this._inputPtr >= this._inputEnd && !loadMore()) {
            return _eofAsNextChar();
        }
        char[] cArr = this._inputBuffer;
        int i = this._inputPtr;
        this._inputPtr = i + 1;
        char c = cArr[i];
        if (c <= ' ') {
            if (c != ' ') {
                if (c == 10) {
                    this._currInputRow++;
                    this._currInputRowStart = this._inputPtr;
                } else if (c == 13) {
                    _skipCR();
                } else if (c != 9) {
                    _throwInvalidSpace(c);
                }
            }
            while (this._inputPtr < this._inputEnd) {
                char[] cArr2 = this._inputBuffer;
                int i2 = this._inputPtr;
                this._inputPtr = i2 + 1;
                char c2 = cArr2[i2];
                if (c2 > ' ') {
                    if (c2 != '/' && c2 != '#') {
                        return c2;
                    }
                    this._inputPtr--;
                    return _skipWSOrEnd2();
                } else if (c2 != ' ') {
                    if (c2 == 10) {
                        this._currInputRow++;
                        this._currInputRowStart = this._inputPtr;
                    } else if (c2 == 13) {
                        _skipCR();
                    } else if (c2 != 9) {
                        _throwInvalidSpace(c2);
                    }
                }
            }
            return _skipWSOrEnd2();
        } else if (c != '/' && c != '#') {
            return c;
        } else {
            this._inputPtr--;
            return _skipWSOrEnd2();
        }
    }

    private int _skipWSOrEnd2() throws IOException {
        while (true) {
            if (this._inputPtr >= this._inputEnd && !loadMore()) {
                return _eofAsNextChar();
            }
            char[] cArr = this._inputBuffer;
            int i = this._inputPtr;
            this._inputPtr = i + 1;
            char c = cArr[i];
            if (c > ' ') {
                if (c == '/') {
                    _skipComment();
                } else if (c != '#' || !_skipYAMLComment()) {
                    return c;
                }
            } else if (c != ' ') {
                if (c == 10) {
                    this._currInputRow++;
                    this._currInputRowStart = this._inputPtr;
                } else if (c == 13) {
                    _skipCR();
                } else if (c != 9) {
                    _throwInvalidSpace(c);
                }
            }
        }
    }

    private void _skipComment() throws IOException {
        if (!isEnabled(Feature.ALLOW_COMMENTS)) {
            _reportUnexpectedChar(47, "maybe a (non-standard) comment? (not recognized as one since Feature 'ALLOW_COMMENTS' not enabled for parser)");
        }
        if (this._inputPtr >= this._inputEnd && !loadMore()) {
            _reportInvalidEOF(" in a comment");
        }
        char[] cArr = this._inputBuffer;
        int i = this._inputPtr;
        this._inputPtr = i + 1;
        char c = cArr[i];
        if (c == '/') {
            _skipLine();
        } else if (c == '*') {
            _skipCComment();
        } else {
            _reportUnexpectedChar(c, "was expecting either '*' or '/' for a comment");
        }
    }

    private void _skipCComment() throws IOException {
        while (true) {
            if (this._inputPtr >= this._inputEnd && !loadMore()) {
                break;
            }
            char[] cArr = this._inputBuffer;
            int i = this._inputPtr;
            this._inputPtr = i + 1;
            char c = cArr[i];
            if (c <= '*') {
                if (c == '*') {
                    if (this._inputPtr >= this._inputEnd && !loadMore()) {
                        break;
                    } else if (this._inputBuffer[this._inputPtr] == '/') {
                        this._inputPtr++;
                        return;
                    }
                } else if (c < ' ') {
                    if (c == 10) {
                        this._currInputRow++;
                        this._currInputRowStart = this._inputPtr;
                    } else if (c == 13) {
                        _skipCR();
                    } else if (c != 9) {
                        _throwInvalidSpace(c);
                    }
                }
            }
        }
        _reportInvalidEOF(" in a comment");
    }

    private boolean _skipYAMLComment() throws IOException {
        if (!isEnabled(Feature.ALLOW_YAML_COMMENTS)) {
            return false;
        }
        _skipLine();
        return true;
    }

    private void _skipLine() throws IOException {
        while (true) {
            if (this._inputPtr < this._inputEnd || loadMore()) {
                char[] cArr = this._inputBuffer;
                int i = this._inputPtr;
                this._inputPtr = i + 1;
                char c = cArr[i];
                if (c < ' ') {
                    if (c == 10) {
                        this._currInputRow++;
                        this._currInputRowStart = this._inputPtr;
                        return;
                    } else if (c == 13) {
                        _skipCR();
                        return;
                    } else if (c != 9) {
                        _throwInvalidSpace(c);
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
        char[] cArr = this._inputBuffer;
        int i2 = this._inputPtr;
        this._inputPtr = i2 + 1;
        char c = cArr[i2];
        switch (c) {
            case '\"':
            case '/':
            case '\\':
                return c;
            case 'b':
                return 8;
            case 'f':
                return 12;
            case 'n':
                return 10;
            case 'r':
                return CharUtils.f1421CR;
            case 't':
                return 9;
            case 'u':
                int i3 = 0;
                while (true) {
                    int i4 = i;
                    if (i3 >= 4) {
                        return (char) i4;
                    }
                    if (this._inputPtr >= this._inputEnd && !loadMore()) {
                        _reportInvalidEOF(" in character escape sequence");
                    }
                    char[] cArr2 = this._inputBuffer;
                    int i5 = this._inputPtr;
                    this._inputPtr = i5 + 1;
                    char c2 = cArr2[i5];
                    int charToHex = CharTypes.charToHex(c2);
                    if (charToHex < 0) {
                        _reportUnexpectedChar(c2, "expected a hex-digit for character escape sequence");
                    }
                    i = (i4 << 4) | charToHex;
                    i3++;
                }
            default:
                return _handleUnrecognizedCharacterEscape(c);
        }
    }

    private final void _matchTrue() throws IOException {
        int i = this._inputPtr;
        if (i + 3 < this._inputEnd) {
            char[] cArr = this._inputBuffer;
            if (cArr[i] == 'r') {
                int i2 = i + 1;
                if (cArr[i2] == 'u') {
                    int i3 = i2 + 1;
                    if (cArr[i3] == 'e') {
                        int i4 = i3 + 1;
                        char c = cArr[i4];
                        if (c < '0' || c == ']' || c == '}') {
                            this._inputPtr = i4;
                            return;
                        }
                    }
                }
            }
        }
        _matchToken(ServerProtocol.DIALOG_RETURN_SCOPES_TRUE, 1);
    }

    private final void _matchFalse() throws IOException {
        int i = this._inputPtr;
        if (i + 4 < this._inputEnd) {
            char[] cArr = this._inputBuffer;
            if (cArr[i] == 'a') {
                int i2 = i + 1;
                if (cArr[i2] == 'l') {
                    int i3 = i2 + 1;
                    if (cArr[i3] == 's') {
                        int i4 = i3 + 1;
                        if (cArr[i4] == 'e') {
                            int i5 = i4 + 1;
                            char c = cArr[i5];
                            if (c < '0' || c == ']' || c == '}') {
                                this._inputPtr = i5;
                                return;
                            }
                        }
                    }
                }
            }
        }
        _matchToken("false", 1);
    }

    private final void _matchNull() throws IOException {
        int i = this._inputPtr;
        if (i + 3 < this._inputEnd) {
            char[] cArr = this._inputBuffer;
            if (cArr[i] == 'u') {
                int i2 = i + 1;
                if (cArr[i2] == 'l') {
                    int i3 = i2 + 1;
                    if (cArr[i3] == 'l') {
                        int i4 = i3 + 1;
                        char c = cArr[i4];
                        if (c < '0' || c == ']' || c == '}') {
                            this._inputPtr = i4;
                            return;
                        }
                    }
                }
            }
        }
        _matchToken("null", 1);
    }

    /* access modifiers changed from: protected */
    public final void _matchToken(String str, int i) throws IOException {
        int length = str.length();
        do {
            if (this._inputPtr >= this._inputEnd && !loadMore()) {
                _reportInvalidToken(str.substring(0, i));
            }
            if (this._inputBuffer[this._inputPtr] != str.charAt(i)) {
                _reportInvalidToken(str.substring(0, i));
            }
            this._inputPtr++;
            i++;
        } while (i < length);
        if (this._inputPtr < this._inputEnd || loadMore()) {
            char c = this._inputBuffer[this._inputPtr];
            if (c >= '0' && c != ']' && c != '}' && Character.isJavaIdentifierPart(c)) {
                _reportInvalidToken(str.substring(0, i));
            }
        }
    }

    /* access modifiers changed from: protected */
    public byte[] _decodeBase64(Base64Variant base64Variant) throws IOException {
        ByteArrayBuilder _getByteArrayBuilder = _getByteArrayBuilder();
        while (true) {
            if (this._inputPtr >= this._inputEnd) {
                loadMoreGuaranteed();
            }
            char[] cArr = this._inputBuffer;
            int i = this._inputPtr;
            this._inputPtr = i + 1;
            char c = cArr[i];
            if (c > ' ') {
                int decodeBase64Char = base64Variant.decodeBase64Char(c);
                if (decodeBase64Char < 0) {
                    if (c == '\"') {
                        return _getByteArrayBuilder.toByteArray();
                    }
                    decodeBase64Char = _decodeBase64Escape(base64Variant, c, 0);
                    if (decodeBase64Char < 0) {
                        continue;
                    }
                }
                if (this._inputPtr >= this._inputEnd) {
                    loadMoreGuaranteed();
                }
                char[] cArr2 = this._inputBuffer;
                int i2 = this._inputPtr;
                this._inputPtr = i2 + 1;
                char c2 = cArr2[i2];
                int decodeBase64Char2 = base64Variant.decodeBase64Char(c2);
                if (decodeBase64Char2 < 0) {
                    decodeBase64Char2 = _decodeBase64Escape(base64Variant, c2, 1);
                }
                int i3 = decodeBase64Char2 | (decodeBase64Char << 6);
                if (this._inputPtr >= this._inputEnd) {
                    loadMoreGuaranteed();
                }
                char[] cArr3 = this._inputBuffer;
                int i4 = this._inputPtr;
                this._inputPtr = i4 + 1;
                char c3 = cArr3[i4];
                int decodeBase64Char3 = base64Variant.decodeBase64Char(c3);
                if (decodeBase64Char3 < 0) {
                    if (decodeBase64Char3 != -2) {
                        if (c3 != '\"' || base64Variant.usesPadding()) {
                            decodeBase64Char3 = _decodeBase64Escape(base64Variant, c3, 2);
                        } else {
                            _getByteArrayBuilder.append(i3 >> 4);
                            return _getByteArrayBuilder.toByteArray();
                        }
                    }
                    if (decodeBase64Char3 == -2) {
                        if (this._inputPtr >= this._inputEnd) {
                            loadMoreGuaranteed();
                        }
                        char[] cArr4 = this._inputBuffer;
                        int i5 = this._inputPtr;
                        this._inputPtr = i5 + 1;
                        char c4 = cArr4[i5];
                        if (!base64Variant.usesPaddingChar(c4)) {
                            throw reportInvalidBase64Char(base64Variant, c4, 3, "expected padding character '" + base64Variant.getPaddingChar() + "'");
                        }
                        _getByteArrayBuilder.append(i3 >> 4);
                    }
                }
                int i6 = (i3 << 6) | decodeBase64Char3;
                if (this._inputPtr >= this._inputEnd) {
                    loadMoreGuaranteed();
                }
                char[] cArr5 = this._inputBuffer;
                int i7 = this._inputPtr;
                this._inputPtr = i7 + 1;
                char c5 = cArr5[i7];
                int decodeBase64Char4 = base64Variant.decodeBase64Char(c5);
                if (decodeBase64Char4 < 0) {
                    if (decodeBase64Char4 != -2) {
                        if (c5 != '\"' || base64Variant.usesPadding()) {
                            decodeBase64Char4 = _decodeBase64Escape(base64Variant, c5, 3);
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
            return new JsonLocation(sourceReference, -1, this._currInputProcessed + (this._nameStartOffset - 1), this._nameStartRow, this._nameStartCol);
        }
        return new JsonLocation(sourceReference, -1, this._tokenInputTotal - 1, this._tokenInputRow, this._tokenInputCol);
    }

    public JsonLocation getCurrentLocation() {
        return new JsonLocation(this._ioContext.getSourceReference(), -1, this._currInputProcessed + ((long) this._inputPtr), this._currInputRow, (this._inputPtr - this._currInputRowStart) + 1);
    }

    private final void _updateLocation() {
        int i = this._inputPtr;
        this._tokenInputTotal = this._currInputProcessed + ((long) i);
        this._tokenInputRow = this._currInputRow;
        this._tokenInputCol = i - this._currInputRowStart;
    }

    private final void _updateNameLocation() {
        int i = this._inputPtr;
        this._nameStartOffset = (long) i;
        this._nameStartRow = this._currInputRow;
        this._nameStartCol = i - this._currInputRowStart;
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
            char c = this._inputBuffer[this._inputPtr];
            if (!Character.isJavaIdentifierPart(c)) {
                break;
            }
            this._inputPtr++;
            sb.append(c);
        }
        _reportError("Unrecognized token '" + sb.toString() + "': was expecting " + str2);
    }
}
