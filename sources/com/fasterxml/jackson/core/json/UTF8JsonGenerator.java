package com.fasterxml.jackson.core.json;

import com.fasterxml.jackson.core.Base64Variant;
import com.fasterxml.jackson.core.JsonGenerationException;
import com.fasterxml.jackson.core.JsonGenerator.Feature;
import com.fasterxml.jackson.core.ObjectCodec;
import com.fasterxml.jackson.core.SerializableString;
import com.fasterxml.jackson.core.p015io.CharTypes;
import com.fasterxml.jackson.core.p015io.CharacterEscapes;
import com.fasterxml.jackson.core.p015io.IOContext;
import com.fasterxml.jackson.core.p015io.NumberOutput;
import com.github.droidfu.support.DisplaySupport;
import com.google.android.gms.games.Notifications;
import java.io.IOException;
import java.io.InputStream;
import java.io.OutputStream;
import java.math.BigDecimal;
import java.math.BigInteger;

public class UTF8JsonGenerator extends JsonGeneratorImpl {
    private static final byte BYTE_0 = 48;
    private static final byte BYTE_BACKSLASH = 92;
    private static final byte BYTE_COLON = 58;
    private static final byte BYTE_COMMA = 44;
    private static final byte BYTE_LBRACKET = 91;
    private static final byte BYTE_LCURLY = 123;
    private static final byte BYTE_QUOTE = 34;
    private static final byte BYTE_RBRACKET = 93;
    private static final byte BYTE_RCURLY = 125;
    private static final byte BYTE_u = 117;
    private static final byte[] FALSE_BYTES = {102, 97, 108, 115, 101};
    private static final byte[] HEX_CHARS = CharTypes.copyHexBytes();
    private static final int MAX_BYTES_TO_BUFFER = 512;
    private static final byte[] NULL_BYTES = {110, BYTE_u, 108, 108};
    private static final byte[] TRUE_BYTES = {116, 114, BYTE_u, 101};
    protected boolean _bufferRecyclable;
    protected char[] _charBuffer;
    protected final int _charBufferLength;
    protected byte[] _entityBuffer;
    protected byte[] _outputBuffer;
    protected final int _outputEnd;
    protected final int _outputMaxContiguous;
    protected final OutputStream _outputStream;
    protected int _outputTail;

    public UTF8JsonGenerator(IOContext iOContext, int i, ObjectCodec objectCodec, OutputStream outputStream) {
        super(iOContext, i, objectCodec);
        this._outputStream = outputStream;
        this._bufferRecyclable = true;
        this._outputBuffer = iOContext.allocWriteEncodingBuffer();
        this._outputEnd = this._outputBuffer.length;
        this._outputMaxContiguous = this._outputEnd >> 3;
        this._charBuffer = iOContext.allocConcatBuffer();
        this._charBufferLength = this._charBuffer.length;
        if (isEnabled(Feature.ESCAPE_NON_ASCII)) {
            setHighestNonEscapedChar(Notifications.NOTIFICATION_TYPES_ALL);
        }
    }

    public UTF8JsonGenerator(IOContext iOContext, int i, ObjectCodec objectCodec, OutputStream outputStream, byte[] bArr, int i2, boolean z) {
        super(iOContext, i, objectCodec);
        this._outputStream = outputStream;
        this._bufferRecyclable = z;
        this._outputTail = i2;
        this._outputBuffer = bArr;
        this._outputEnd = this._outputBuffer.length;
        this._outputMaxContiguous = this._outputEnd >> 3;
        this._charBuffer = iOContext.allocConcatBuffer();
        this._charBufferLength = this._charBuffer.length;
    }

    public Object getOutputTarget() {
        return this._outputStream;
    }

    public int getOutputBuffered() {
        return this._outputTail;
    }

    public void writeFieldName(String str) throws IOException {
        if (this._cfgPrettyPrinter != null) {
            _writePPFieldName(str);
            return;
        }
        int writeFieldName = this._writeContext.writeFieldName(str);
        if (writeFieldName == 4) {
            _reportError("Can not write a field name, expecting a value");
        }
        if (writeFieldName == 1) {
            if (this._outputTail >= this._outputEnd) {
                _flushBuffer();
            }
            byte[] bArr = this._outputBuffer;
            int i = this._outputTail;
            this._outputTail = i + 1;
            bArr[i] = BYTE_COMMA;
        }
        if (this._cfgUnqNames) {
            _writeStringSegments(str, false);
            return;
        }
        int length = str.length();
        if (length > this._charBufferLength) {
            _writeStringSegments(str, true);
            return;
        }
        if (this._outputTail >= this._outputEnd) {
            _flushBuffer();
        }
        byte[] bArr2 = this._outputBuffer;
        int i2 = this._outputTail;
        this._outputTail = i2 + 1;
        bArr2[i2] = BYTE_QUOTE;
        if (length <= this._outputMaxContiguous) {
            if (this._outputTail + length > this._outputEnd) {
                _flushBuffer();
            }
            _writeStringSegment(str, 0, length);
        } else {
            _writeStringSegments(str, 0, length);
        }
        if (this._outputTail >= this._outputEnd) {
            _flushBuffer();
        }
        byte[] bArr3 = this._outputBuffer;
        int i3 = this._outputTail;
        this._outputTail = i3 + 1;
        bArr3[i3] = BYTE_QUOTE;
    }

    public void writeFieldName(SerializableString serializableString) throws IOException {
        if (this._cfgPrettyPrinter != null) {
            _writePPFieldName(serializableString);
            return;
        }
        int writeFieldName = this._writeContext.writeFieldName(serializableString.getValue());
        if (writeFieldName == 4) {
            _reportError("Can not write a field name, expecting a value");
        }
        if (writeFieldName == 1) {
            if (this._outputTail >= this._outputEnd) {
                _flushBuffer();
            }
            byte[] bArr = this._outputBuffer;
            int i = this._outputTail;
            this._outputTail = i + 1;
            bArr[i] = BYTE_COMMA;
        }
        if (this._cfgUnqNames) {
            _writeUnq(serializableString);
            return;
        }
        if (this._outputTail >= this._outputEnd) {
            _flushBuffer();
        }
        byte[] bArr2 = this._outputBuffer;
        int i2 = this._outputTail;
        this._outputTail = i2 + 1;
        bArr2[i2] = BYTE_QUOTE;
        int appendQuotedUTF8 = serializableString.appendQuotedUTF8(this._outputBuffer, this._outputTail);
        if (appendQuotedUTF8 < 0) {
            _writeBytes(serializableString.asQuotedUTF8());
        } else {
            this._outputTail = appendQuotedUTF8 + this._outputTail;
        }
        if (this._outputTail >= this._outputEnd) {
            _flushBuffer();
        }
        byte[] bArr3 = this._outputBuffer;
        int i3 = this._outputTail;
        this._outputTail = i3 + 1;
        bArr3[i3] = BYTE_QUOTE;
    }

    private final void _writeUnq(SerializableString serializableString) throws IOException {
        int appendQuotedUTF8 = serializableString.appendQuotedUTF8(this._outputBuffer, this._outputTail);
        if (appendQuotedUTF8 < 0) {
            _writeBytes(serializableString.asQuotedUTF8());
        } else {
            this._outputTail = appendQuotedUTF8 + this._outputTail;
        }
    }

    public final void writeStartArray() throws IOException {
        _verifyValueWrite("start an array");
        this._writeContext = this._writeContext.createChildArrayContext();
        if (this._cfgPrettyPrinter != null) {
            this._cfgPrettyPrinter.writeStartArray(this);
            return;
        }
        if (this._outputTail >= this._outputEnd) {
            _flushBuffer();
        }
        byte[] bArr = this._outputBuffer;
        int i = this._outputTail;
        this._outputTail = i + 1;
        bArr[i] = BYTE_LBRACKET;
    }

    public final void writeEndArray() throws IOException {
        if (!this._writeContext.inArray()) {
            _reportError("Current context not an ARRAY but " + this._writeContext.getTypeDesc());
        }
        if (this._cfgPrettyPrinter != null) {
            this._cfgPrettyPrinter.writeEndArray(this, this._writeContext.getEntryCount());
        } else {
            if (this._outputTail >= this._outputEnd) {
                _flushBuffer();
            }
            byte[] bArr = this._outputBuffer;
            int i = this._outputTail;
            this._outputTail = i + 1;
            bArr[i] = BYTE_RBRACKET;
        }
        this._writeContext = this._writeContext.clearAndGetParent();
    }

    public final void writeStartObject() throws IOException {
        _verifyValueWrite("start an object");
        this._writeContext = this._writeContext.createChildObjectContext();
        if (this._cfgPrettyPrinter != null) {
            this._cfgPrettyPrinter.writeStartObject(this);
            return;
        }
        if (this._outputTail >= this._outputEnd) {
            _flushBuffer();
        }
        byte[] bArr = this._outputBuffer;
        int i = this._outputTail;
        this._outputTail = i + 1;
        bArr[i] = BYTE_LCURLY;
    }

    public final void writeEndObject() throws IOException {
        if (!this._writeContext.inObject()) {
            _reportError("Current context not an object but " + this._writeContext.getTypeDesc());
        }
        if (this._cfgPrettyPrinter != null) {
            this._cfgPrettyPrinter.writeEndObject(this, this._writeContext.getEntryCount());
        } else {
            if (this._outputTail >= this._outputEnd) {
                _flushBuffer();
            }
            byte[] bArr = this._outputBuffer;
            int i = this._outputTail;
            this._outputTail = i + 1;
            bArr[i] = BYTE_RCURLY;
        }
        this._writeContext = this._writeContext.clearAndGetParent();
    }

    /* access modifiers changed from: protected */
    public final void _writePPFieldName(String str) throws IOException {
        int writeFieldName = this._writeContext.writeFieldName(str);
        if (writeFieldName == 4) {
            _reportError("Can not write a field name, expecting a value");
        }
        if (writeFieldName == 1) {
            this._cfgPrettyPrinter.writeObjectEntrySeparator(this);
        } else {
            this._cfgPrettyPrinter.beforeObjectEntries(this);
        }
        if (this._cfgUnqNames) {
            _writeStringSegments(str, false);
            return;
        }
        int length = str.length();
        if (length > this._charBufferLength) {
            _writeStringSegments(str, true);
            return;
        }
        if (this._outputTail >= this._outputEnd) {
            _flushBuffer();
        }
        byte[] bArr = this._outputBuffer;
        int i = this._outputTail;
        this._outputTail = i + 1;
        bArr[i] = BYTE_QUOTE;
        str.getChars(0, length, this._charBuffer, 0);
        if (length <= this._outputMaxContiguous) {
            if (this._outputTail + length > this._outputEnd) {
                _flushBuffer();
            }
            _writeStringSegment(this._charBuffer, 0, length);
        } else {
            _writeStringSegments(this._charBuffer, 0, length);
        }
        if (this._outputTail >= this._outputEnd) {
            _flushBuffer();
        }
        byte[] bArr2 = this._outputBuffer;
        int i2 = this._outputTail;
        this._outputTail = i2 + 1;
        bArr2[i2] = BYTE_QUOTE;
    }

    /* access modifiers changed from: protected */
    public final void _writePPFieldName(SerializableString serializableString) throws IOException {
        boolean z = true;
        int writeFieldName = this._writeContext.writeFieldName(serializableString.getValue());
        if (writeFieldName == 4) {
            _reportError("Can not write a field name, expecting a value");
        }
        if (writeFieldName == 1) {
            this._cfgPrettyPrinter.writeObjectEntrySeparator(this);
        } else {
            this._cfgPrettyPrinter.beforeObjectEntries(this);
        }
        if (this._cfgUnqNames) {
            z = false;
        }
        if (z) {
            if (this._outputTail >= this._outputEnd) {
                _flushBuffer();
            }
            byte[] bArr = this._outputBuffer;
            int i = this._outputTail;
            this._outputTail = i + 1;
            bArr[i] = BYTE_QUOTE;
        }
        _writeBytes(serializableString.asQuotedUTF8());
        if (z) {
            if (this._outputTail >= this._outputEnd) {
                _flushBuffer();
            }
            byte[] bArr2 = this._outputBuffer;
            int i2 = this._outputTail;
            this._outputTail = i2 + 1;
            bArr2[i2] = BYTE_QUOTE;
        }
    }

    public void writeString(String str) throws IOException {
        _verifyValueWrite("write a string");
        if (str == null) {
            _writeNull();
            return;
        }
        int length = str.length();
        if (length > this._outputMaxContiguous) {
            _writeStringSegments(str, true);
            return;
        }
        if (this._outputTail + length >= this._outputEnd) {
            _flushBuffer();
        }
        byte[] bArr = this._outputBuffer;
        int i = this._outputTail;
        this._outputTail = i + 1;
        bArr[i] = BYTE_QUOTE;
        _writeStringSegment(str, 0, length);
        if (this._outputTail >= this._outputEnd) {
            _flushBuffer();
        }
        byte[] bArr2 = this._outputBuffer;
        int i2 = this._outputTail;
        this._outputTail = i2 + 1;
        bArr2[i2] = BYTE_QUOTE;
    }

    public void writeString(char[] cArr, int i, int i2) throws IOException {
        _verifyValueWrite("write a string");
        if (this._outputTail >= this._outputEnd) {
            _flushBuffer();
        }
        byte[] bArr = this._outputBuffer;
        int i3 = this._outputTail;
        this._outputTail = i3 + 1;
        bArr[i3] = BYTE_QUOTE;
        if (i2 <= this._outputMaxContiguous) {
            if (this._outputTail + i2 > this._outputEnd) {
                _flushBuffer();
            }
            _writeStringSegment(cArr, i, i2);
        } else {
            _writeStringSegments(cArr, i, i2);
        }
        if (this._outputTail >= this._outputEnd) {
            _flushBuffer();
        }
        byte[] bArr2 = this._outputBuffer;
        int i4 = this._outputTail;
        this._outputTail = i4 + 1;
        bArr2[i4] = BYTE_QUOTE;
    }

    public final void writeString(SerializableString serializableString) throws IOException {
        _verifyValueWrite("write a string");
        if (this._outputTail >= this._outputEnd) {
            _flushBuffer();
        }
        byte[] bArr = this._outputBuffer;
        int i = this._outputTail;
        this._outputTail = i + 1;
        bArr[i] = BYTE_QUOTE;
        int appendQuotedUTF8 = serializableString.appendQuotedUTF8(this._outputBuffer, this._outputTail);
        if (appendQuotedUTF8 < 0) {
            _writeBytes(serializableString.asQuotedUTF8());
        } else {
            this._outputTail = appendQuotedUTF8 + this._outputTail;
        }
        if (this._outputTail >= this._outputEnd) {
            _flushBuffer();
        }
        byte[] bArr2 = this._outputBuffer;
        int i2 = this._outputTail;
        this._outputTail = i2 + 1;
        bArr2[i2] = BYTE_QUOTE;
    }

    public void writeRawUTF8String(byte[] bArr, int i, int i2) throws IOException {
        _verifyValueWrite("write a string");
        if (this._outputTail >= this._outputEnd) {
            _flushBuffer();
        }
        byte[] bArr2 = this._outputBuffer;
        int i3 = this._outputTail;
        this._outputTail = i3 + 1;
        bArr2[i3] = BYTE_QUOTE;
        _writeBytes(bArr, i, i2);
        if (this._outputTail >= this._outputEnd) {
            _flushBuffer();
        }
        byte[] bArr3 = this._outputBuffer;
        int i4 = this._outputTail;
        this._outputTail = i4 + 1;
        bArr3[i4] = BYTE_QUOTE;
    }

    public void writeUTF8String(byte[] bArr, int i, int i2) throws IOException {
        _verifyValueWrite("write a string");
        if (this._outputTail >= this._outputEnd) {
            _flushBuffer();
        }
        byte[] bArr2 = this._outputBuffer;
        int i3 = this._outputTail;
        this._outputTail = i3 + 1;
        bArr2[i3] = BYTE_QUOTE;
        if (i2 <= this._outputMaxContiguous) {
            _writeUTF8Segment(bArr, i, i2);
        } else {
            _writeUTF8Segments(bArr, i, i2);
        }
        if (this._outputTail >= this._outputEnd) {
            _flushBuffer();
        }
        byte[] bArr3 = this._outputBuffer;
        int i4 = this._outputTail;
        this._outputTail = i4 + 1;
        bArr3[i4] = BYTE_QUOTE;
    }

    public void writeRaw(String str) throws IOException, JsonGenerationException {
        int length = str.length();
        int i = 0;
        while (length > 0) {
            char[] cArr = this._charBuffer;
            int length2 = cArr.length;
            if (length < length2) {
                length2 = length;
            }
            str.getChars(i, i + length2, cArr, 0);
            writeRaw(cArr, 0, length2);
            i += length2;
            length -= length2;
        }
    }

    public void writeRaw(String str, int i, int i2) throws IOException, JsonGenerationException {
        int i3 = i2;
        while (i3 > 0) {
            char[] cArr = this._charBuffer;
            int length = cArr.length;
            if (i3 < length) {
                length = i3;
            }
            str.getChars(i, i + length, cArr, 0);
            writeRaw(cArr, 0, length);
            i += length;
            i3 -= length;
        }
    }

    public void writeRaw(SerializableString serializableString) throws IOException, JsonGenerationException {
        byte[] asUnquotedUTF8 = serializableString.asUnquotedUTF8();
        if (asUnquotedUTF8.length > 0) {
            _writeBytes(asUnquotedUTF8);
        }
    }

    public void writeRawValue(SerializableString serializableString) throws IOException {
        _verifyValueWrite("write a raw (unencoded) value");
        byte[] asUnquotedUTF8 = serializableString.asUnquotedUTF8();
        if (asUnquotedUTF8.length > 0) {
            _writeBytes(asUnquotedUTF8);
        }
    }

    /* JADX WARNING: Code restructure failed: missing block: B:10:0x0020, code lost:
        r1 = r0 + 1;
        r0 = r7[r0];
     */
    /* JADX WARNING: Code restructure failed: missing block: B:11:0x0026, code lost:
        if (r0 >= 2048) goto L_0x0058;
     */
    /* JADX WARNING: Code restructure failed: missing block: B:12:0x0028, code lost:
        r3 = r6._outputBuffer;
        r4 = r6._outputTail;
        r6._outputTail = r4 + 1;
        r3[r4] = (byte) ((r0 >> 6) | 192);
        r3 = r6._outputBuffer;
        r4 = r6._outputTail;
        r6._outputTail = r4 + 1;
        r3[r4] = (byte) ((r0 & '?') | 128);
        r0 = r1;
     */
    /* JADX WARNING: Code restructure failed: missing block: B:15:0x0058, code lost:
        r0 = _outputRawMultiByteChar(r0, r7, r1, r2);
     */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    public final void writeRaw(char[] r7, int r8, int r9) throws java.io.IOException, com.fasterxml.jackson.core.JsonGenerationException {
        /*
            r6 = this;
            int r0 = r9 + r9
            int r0 = r0 + r9
            int r1 = r6._outputTail
            int r1 = r1 + r0
            int r2 = r6._outputEnd
            if (r1 <= r2) goto L_0x0015
            int r1 = r6._outputEnd
            if (r1 >= r0) goto L_0x0012
            r6._writeSegmentedRaw(r7, r8, r9)
        L_0x0011:
            return
        L_0x0012:
            r6._flushBuffer()
        L_0x0015:
            int r2 = r9 + r8
            r0 = r8
        L_0x0018:
            if (r0 >= r2) goto L_0x0011
        L_0x001a:
            char r1 = r7[r0]
            r3 = 127(0x7f, float:1.78E-43)
            if (r1 <= r3) goto L_0x0048
            int r1 = r0 + 1
            char r0 = r7[r0]
            r3 = 2048(0x800, float:2.87E-42)
            if (r0 >= r3) goto L_0x0058
            byte[] r3 = r6._outputBuffer
            int r4 = r6._outputTail
            int r5 = r4 + 1
            r6._outputTail = r5
            int r5 = r0 >> 6
            r5 = r5 | 192(0xc0, float:2.69E-43)
            byte r5 = (byte) r5
            r3[r4] = r5
            byte[] r3 = r6._outputBuffer
            int r4 = r6._outputTail
            int r5 = r4 + 1
            r6._outputTail = r5
            r0 = r0 & 63
            r0 = r0 | 128(0x80, float:1.794E-43)
            byte r0 = (byte) r0
            r3[r4] = r0
            r0 = r1
            goto L_0x0018
        L_0x0048:
            byte[] r3 = r6._outputBuffer
            int r4 = r6._outputTail
            int r5 = r4 + 1
            r6._outputTail = r5
            byte r1 = (byte) r1
            r3[r4] = r1
            int r0 = r0 + 1
            if (r0 < r2) goto L_0x001a
            goto L_0x0011
        L_0x0058:
            int r0 = r6._outputRawMultiByteChar(r0, r7, r1, r2)
            goto L_0x0018
        */
        throw new UnsupportedOperationException("Method not decompiled: com.fasterxml.jackson.core.json.UTF8JsonGenerator.writeRaw(char[], int, int):void");
    }

    public void writeRaw(char c) throws IOException, JsonGenerationException {
        if (this._outputTail + 3 >= this._outputEnd) {
            _flushBuffer();
        }
        byte[] bArr = this._outputBuffer;
        if (c <= 127) {
            int i = this._outputTail;
            this._outputTail = i + 1;
            bArr[i] = (byte) c;
        } else if (c < 2048) {
            int i2 = this._outputTail;
            this._outputTail = i2 + 1;
            bArr[i2] = (byte) ((c >> 6) | 192);
            int i3 = this._outputTail;
            this._outputTail = i3 + 1;
            bArr[i3] = (byte) ((c & '?') | 128);
        } else {
            _outputRawMultiByteChar(c, null, 0, 0);
        }
    }

    /* JADX WARNING: Code restructure failed: missing block: B:15:0x0051, code lost:
        r0 = _outputRawMultiByteChar(r0, r7, r1, r9);
     */
    /* JADX WARNING: Code restructure failed: missing block: B:5:0x0013, code lost:
        if ((r6._outputTail + 3) < r6._outputEnd) goto L_0x0018;
     */
    /* JADX WARNING: Code restructure failed: missing block: B:6:0x0015, code lost:
        _flushBuffer();
     */
    /* JADX WARNING: Code restructure failed: missing block: B:7:0x0018, code lost:
        r1 = r0 + 1;
        r0 = r7[r0];
     */
    /* JADX WARNING: Code restructure failed: missing block: B:8:0x001e, code lost:
        if (r0 >= 2048) goto L_0x0051;
     */
    /* JADX WARNING: Code restructure failed: missing block: B:9:0x0020, code lost:
        r4 = r6._outputTail;
        r6._outputTail = r4 + 1;
        r3[r4] = (byte) ((r0 >> 6) | 192);
        r4 = r6._outputTail;
        r6._outputTail = r4 + 1;
        r3[r4] = (byte) ((r0 & '?') | 128);
        r0 = r1;
     */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    private final void _writeSegmentedRaw(char[] r7, int r8, int r9) throws java.io.IOException, com.fasterxml.jackson.core.JsonGenerationException {
        /*
            r6 = this;
            int r2 = r6._outputEnd
            byte[] r3 = r6._outputBuffer
            r0 = r8
        L_0x0005:
            if (r0 >= r9) goto L_0x0050
        L_0x0007:
            char r1 = r7[r0]
            r4 = 128(0x80, float:1.794E-43)
            if (r1 < r4) goto L_0x003c
            int r1 = r6._outputTail
            int r1 = r1 + 3
            int r4 = r6._outputEnd
            if (r1 < r4) goto L_0x0018
            r6._flushBuffer()
        L_0x0018:
            int r1 = r0 + 1
            char r0 = r7[r0]
            r4 = 2048(0x800, float:2.87E-42)
            if (r0 >= r4) goto L_0x0051
            int r4 = r6._outputTail
            int r5 = r4 + 1
            r6._outputTail = r5
            int r5 = r0 >> 6
            r5 = r5 | 192(0xc0, float:2.69E-43)
            byte r5 = (byte) r5
            r3[r4] = r5
            int r4 = r6._outputTail
            int r5 = r4 + 1
            r6._outputTail = r5
            r0 = r0 & 63
            r0 = r0 | 128(0x80, float:1.794E-43)
            byte r0 = (byte) r0
            r3[r4] = r0
            r0 = r1
            goto L_0x0005
        L_0x003c:
            int r4 = r6._outputTail
            if (r4 < r2) goto L_0x0043
            r6._flushBuffer()
        L_0x0043:
            int r4 = r6._outputTail
            int r5 = r4 + 1
            r6._outputTail = r5
            byte r1 = (byte) r1
            r3[r4] = r1
            int r0 = r0 + 1
            if (r0 < r9) goto L_0x0007
        L_0x0050:
            return
        L_0x0051:
            int r0 = r6._outputRawMultiByteChar(r0, r7, r1, r9)
            goto L_0x0005
        */
        throw new UnsupportedOperationException("Method not decompiled: com.fasterxml.jackson.core.json.UTF8JsonGenerator._writeSegmentedRaw(char[], int, int):void");
    }

    public void writeBinary(Base64Variant base64Variant, byte[] bArr, int i, int i2) throws IOException, JsonGenerationException {
        _verifyValueWrite("write a binary value");
        if (this._outputTail >= this._outputEnd) {
            _flushBuffer();
        }
        byte[] bArr2 = this._outputBuffer;
        int i3 = this._outputTail;
        this._outputTail = i3 + 1;
        bArr2[i3] = BYTE_QUOTE;
        _writeBinary(base64Variant, bArr, i, i + i2);
        if (this._outputTail >= this._outputEnd) {
            _flushBuffer();
        }
        byte[] bArr3 = this._outputBuffer;
        int i4 = this._outputTail;
        this._outputTail = i4 + 1;
        bArr3[i4] = BYTE_QUOTE;
    }

    public int writeBinary(Base64Variant base64Variant, InputStream inputStream, int i) throws IOException, JsonGenerationException {
        _verifyValueWrite("write a binary value");
        if (this._outputTail >= this._outputEnd) {
            _flushBuffer();
        }
        byte[] bArr = this._outputBuffer;
        int i2 = this._outputTail;
        this._outputTail = i2 + 1;
        bArr[i2] = BYTE_QUOTE;
        byte[] allocBase64Buffer = this._ioContext.allocBase64Buffer();
        if (i < 0) {
            try {
                i = _writeBinary(base64Variant, inputStream, allocBase64Buffer);
            } catch (Throwable th) {
                this._ioContext.releaseBase64Buffer(allocBase64Buffer);
                throw th;
            }
        } else {
            int _writeBinary = _writeBinary(base64Variant, inputStream, allocBase64Buffer, i);
            if (_writeBinary > 0) {
                _reportError("Too few bytes available: missing " + _writeBinary + " bytes (out of " + i + ")");
            }
        }
        this._ioContext.releaseBase64Buffer(allocBase64Buffer);
        if (this._outputTail >= this._outputEnd) {
            _flushBuffer();
        }
        byte[] bArr2 = this._outputBuffer;
        int i3 = this._outputTail;
        this._outputTail = i3 + 1;
        bArr2[i3] = BYTE_QUOTE;
        return i;
    }

    public void writeNumber(short s) throws IOException {
        _verifyValueWrite("write a number");
        if (this._outputTail + 6 >= this._outputEnd) {
            _flushBuffer();
        }
        if (this._cfgNumbersAsStrings) {
            _writeQuotedShort(s);
        } else {
            this._outputTail = NumberOutput.outputInt((int) s, this._outputBuffer, this._outputTail);
        }
    }

    private final void _writeQuotedShort(short s) throws IOException {
        if (this._outputTail + 8 >= this._outputEnd) {
            _flushBuffer();
        }
        byte[] bArr = this._outputBuffer;
        int i = this._outputTail;
        this._outputTail = i + 1;
        bArr[i] = BYTE_QUOTE;
        this._outputTail = NumberOutput.outputInt((int) s, this._outputBuffer, this._outputTail);
        byte[] bArr2 = this._outputBuffer;
        int i2 = this._outputTail;
        this._outputTail = i2 + 1;
        bArr2[i2] = BYTE_QUOTE;
    }

    public void writeNumber(int i) throws IOException {
        _verifyValueWrite("write a number");
        if (this._outputTail + 11 >= this._outputEnd) {
            _flushBuffer();
        }
        if (this._cfgNumbersAsStrings) {
            _writeQuotedInt(i);
        } else {
            this._outputTail = NumberOutput.outputInt(i, this._outputBuffer, this._outputTail);
        }
    }

    private final void _writeQuotedInt(int i) throws IOException {
        if (this._outputTail + 13 >= this._outputEnd) {
            _flushBuffer();
        }
        byte[] bArr = this._outputBuffer;
        int i2 = this._outputTail;
        this._outputTail = i2 + 1;
        bArr[i2] = BYTE_QUOTE;
        this._outputTail = NumberOutput.outputInt(i, this._outputBuffer, this._outputTail);
        byte[] bArr2 = this._outputBuffer;
        int i3 = this._outputTail;
        this._outputTail = i3 + 1;
        bArr2[i3] = BYTE_QUOTE;
    }

    public void writeNumber(long j) throws IOException {
        _verifyValueWrite("write a number");
        if (this._cfgNumbersAsStrings) {
            _writeQuotedLong(j);
            return;
        }
        if (this._outputTail + 21 >= this._outputEnd) {
            _flushBuffer();
        }
        this._outputTail = NumberOutput.outputLong(j, this._outputBuffer, this._outputTail);
    }

    private final void _writeQuotedLong(long j) throws IOException {
        if (this._outputTail + 23 >= this._outputEnd) {
            _flushBuffer();
        }
        byte[] bArr = this._outputBuffer;
        int i = this._outputTail;
        this._outputTail = i + 1;
        bArr[i] = BYTE_QUOTE;
        this._outputTail = NumberOutput.outputLong(j, this._outputBuffer, this._outputTail);
        byte[] bArr2 = this._outputBuffer;
        int i2 = this._outputTail;
        this._outputTail = i2 + 1;
        bArr2[i2] = BYTE_QUOTE;
    }

    public void writeNumber(BigInteger bigInteger) throws IOException {
        _verifyValueWrite("write a number");
        if (bigInteger == null) {
            _writeNull();
        } else if (this._cfgNumbersAsStrings) {
            _writeQuotedRaw(bigInteger.toString());
        } else {
            writeRaw(bigInteger.toString());
        }
    }

    public void writeNumber(double d) throws IOException {
        if (this._cfgNumbersAsStrings || ((Double.isNaN(d) || Double.isInfinite(d)) && Feature.QUOTE_NON_NUMERIC_NUMBERS.enabledIn(this._features))) {
            writeString(String.valueOf(d));
            return;
        }
        _verifyValueWrite("write a number");
        writeRaw(String.valueOf(d));
    }

    public void writeNumber(float f) throws IOException {
        if (this._cfgNumbersAsStrings || ((Float.isNaN(f) || Float.isInfinite(f)) && Feature.QUOTE_NON_NUMERIC_NUMBERS.enabledIn(this._features))) {
            writeString(String.valueOf(f));
            return;
        }
        _verifyValueWrite("write a number");
        writeRaw(String.valueOf(f));
    }

    public void writeNumber(BigDecimal bigDecimal) throws IOException {
        _verifyValueWrite("write a number");
        if (bigDecimal == null) {
            _writeNull();
        } else if (this._cfgNumbersAsStrings) {
            _writeQuotedRaw(Feature.WRITE_BIGDECIMAL_AS_PLAIN.enabledIn(this._features) ? bigDecimal.toPlainString() : bigDecimal.toString());
        } else if (Feature.WRITE_BIGDECIMAL_AS_PLAIN.enabledIn(this._features)) {
            writeRaw(bigDecimal.toPlainString());
        } else {
            writeRaw(bigDecimal.toString());
        }
    }

    public void writeNumber(String str) throws IOException {
        _verifyValueWrite("write a number");
        if (this._cfgNumbersAsStrings) {
            _writeQuotedRaw(str);
        } else {
            writeRaw(str);
        }
    }

    private final void _writeQuotedRaw(String str) throws IOException {
        if (this._outputTail >= this._outputEnd) {
            _flushBuffer();
        }
        byte[] bArr = this._outputBuffer;
        int i = this._outputTail;
        this._outputTail = i + 1;
        bArr[i] = BYTE_QUOTE;
        writeRaw(str);
        if (this._outputTail >= this._outputEnd) {
            _flushBuffer();
        }
        byte[] bArr2 = this._outputBuffer;
        int i2 = this._outputTail;
        this._outputTail = i2 + 1;
        bArr2[i2] = BYTE_QUOTE;
    }

    public void writeBoolean(boolean z) throws IOException {
        _verifyValueWrite("write a boolean value");
        if (this._outputTail + 5 >= this._outputEnd) {
            _flushBuffer();
        }
        byte[] bArr = z ? TRUE_BYTES : FALSE_BYTES;
        int length = bArr.length;
        System.arraycopy(bArr, 0, this._outputBuffer, this._outputTail, length);
        this._outputTail += length;
    }

    public void writeNull() throws IOException {
        _verifyValueWrite("write a null");
        _writeNull();
    }

    /* access modifiers changed from: protected */
    public final void _verifyValueWrite(String str) throws IOException {
        byte b;
        int writeValue = this._writeContext.writeValue();
        if (writeValue == 5) {
            _reportError("Can not " + str + ", expecting field name");
        }
        if (this._cfgPrettyPrinter == null) {
            switch (writeValue) {
                case 1:
                    b = BYTE_COMMA;
                    break;
                case 2:
                    b = BYTE_COLON;
                    break;
                case 3:
                    if (this._rootValueSeparator != null) {
                        byte[] asUnquotedUTF8 = this._rootValueSeparator.asUnquotedUTF8();
                        if (asUnquotedUTF8.length > 0) {
                            _writeBytes(asUnquotedUTF8);
                            return;
                        }
                        return;
                    }
                    return;
                default:
                    return;
            }
            if (this._outputTail >= this._outputEnd) {
                _flushBuffer();
            }
            this._outputBuffer[this._outputTail] = b;
            this._outputTail++;
            return;
        }
        _verifyPrettyValueWrite(str, writeValue);
    }

    /* access modifiers changed from: protected */
    public final void _verifyPrettyValueWrite(String str, int i) throws IOException {
        switch (i) {
            case 0:
                if (this._writeContext.inArray()) {
                    this._cfgPrettyPrinter.beforeArrayValues(this);
                    return;
                } else if (this._writeContext.inObject()) {
                    this._cfgPrettyPrinter.beforeObjectEntries(this);
                    return;
                } else {
                    return;
                }
            case 1:
                this._cfgPrettyPrinter.writeArrayValueSeparator(this);
                return;
            case 2:
                this._cfgPrettyPrinter.writeObjectFieldValueSeparator(this);
                return;
            case 3:
                this._cfgPrettyPrinter.writeRootValueSeparator(this);
                return;
            default:
                _throwInternal();
                return;
        }
    }

    public void flush() throws IOException {
        _flushBuffer();
        if (this._outputStream != null && isEnabled(Feature.FLUSH_PASSED_TO_STREAM)) {
            this._outputStream.flush();
        }
    }

    public void close() throws IOException {
        super.close();
        if (this._outputBuffer != null && isEnabled(Feature.AUTO_CLOSE_JSON_CONTENT)) {
            while (true) {
                JsonWriteContext outputContext = getOutputContext();
                if (!outputContext.inArray()) {
                    if (!outputContext.inObject()) {
                        break;
                    }
                    writeEndObject();
                } else {
                    writeEndArray();
                }
            }
        }
        _flushBuffer();
        this._outputTail = 0;
        if (this._outputStream != null) {
            if (this._ioContext.isResourceManaged() || isEnabled(Feature.AUTO_CLOSE_TARGET)) {
                this._outputStream.close();
            } else if (isEnabled(Feature.FLUSH_PASSED_TO_STREAM)) {
                this._outputStream.flush();
            }
        }
        _releaseBuffers();
    }

    /* access modifiers changed from: protected */
    public void _releaseBuffers() {
        byte[] bArr = this._outputBuffer;
        if (bArr != null && this._bufferRecyclable) {
            this._outputBuffer = null;
            this._ioContext.releaseWriteEncodingBuffer(bArr);
        }
        char[] cArr = this._charBuffer;
        if (cArr != null) {
            this._charBuffer = null;
            this._ioContext.releaseConcatBuffer(cArr);
        }
    }

    private final void _writeBytes(byte[] bArr) throws IOException {
        int length = bArr.length;
        if (this._outputTail + length > this._outputEnd) {
            _flushBuffer();
            if (length > 512) {
                this._outputStream.write(bArr, 0, length);
                return;
            }
        }
        System.arraycopy(bArr, 0, this._outputBuffer, this._outputTail, length);
        this._outputTail = length + this._outputTail;
    }

    private final void _writeBytes(byte[] bArr, int i, int i2) throws IOException {
        if (this._outputTail + i2 > this._outputEnd) {
            _flushBuffer();
            if (i2 > 512) {
                this._outputStream.write(bArr, i, i2);
                return;
            }
        }
        System.arraycopy(bArr, i, this._outputBuffer, this._outputTail, i2);
        this._outputTail += i2;
    }

    private final void _writeStringSegments(String str, boolean z) throws IOException {
        if (z) {
            if (this._outputTail >= this._outputEnd) {
                _flushBuffer();
            }
            byte[] bArr = this._outputBuffer;
            int i = this._outputTail;
            this._outputTail = i + 1;
            bArr[i] = BYTE_QUOTE;
        }
        int length = str.length();
        int i2 = 0;
        while (length > 0) {
            int min = Math.min(this._outputMaxContiguous, length);
            if (this._outputTail + min > this._outputEnd) {
                _flushBuffer();
            }
            _writeStringSegment(str, i2, min);
            i2 += min;
            length -= min;
        }
        if (z) {
            if (this._outputTail >= this._outputEnd) {
                _flushBuffer();
            }
            byte[] bArr2 = this._outputBuffer;
            int i3 = this._outputTail;
            this._outputTail = i3 + 1;
            bArr2[i3] = BYTE_QUOTE;
        }
    }

    private final void _writeStringSegments(char[] cArr, int i, int i2) throws IOException {
        do {
            int min = Math.min(this._outputMaxContiguous, i2);
            if (this._outputTail + min > this._outputEnd) {
                _flushBuffer();
            }
            _writeStringSegment(cArr, i, min);
            i += min;
            i2 -= min;
        } while (i2 > 0);
    }

    private final void _writeStringSegments(String str, int i, int i2) throws IOException {
        do {
            int min = Math.min(this._outputMaxContiguous, i2);
            if (this._outputTail + min > this._outputEnd) {
                _flushBuffer();
            }
            _writeStringSegment(str, i, min);
            i += min;
            i2 -= min;
        } while (i2 > 0);
    }

    private final void _writeStringSegment(char[] cArr, int i, int i2) throws IOException {
        int i3 = i2 + i;
        int i4 = this._outputTail;
        byte[] bArr = this._outputBuffer;
        int[] iArr = this._outputEscapes;
        while (i < i3) {
            char c = cArr[i];
            if (c > 127 || iArr[c] != 0) {
                break;
            }
            int i5 = i4 + 1;
            bArr[i4] = (byte) c;
            i++;
            i4 = i5;
        }
        this._outputTail = i4;
        if (i >= i3) {
            return;
        }
        if (this._characterEscapes != null) {
            _writeCustomStringSegment2(cArr, i, i3);
        } else if (this._maximumNonEscapedChar == 0) {
            _writeStringSegment2(cArr, i, i3);
        } else {
            _writeStringSegmentASCII2(cArr, i, i3);
        }
    }

    private final void _writeStringSegment(String str, int i, int i2) throws IOException {
        int i3 = i2 + i;
        int i4 = this._outputTail;
        byte[] bArr = this._outputBuffer;
        int[] iArr = this._outputEscapes;
        while (i < i3) {
            char charAt = str.charAt(i);
            if (charAt > 127 || iArr[charAt] != 0) {
                break;
            }
            int i5 = i4 + 1;
            bArr[i4] = (byte) charAt;
            i++;
            i4 = i5;
        }
        this._outputTail = i4;
        if (i >= i3) {
            return;
        }
        if (this._characterEscapes != null) {
            _writeCustomStringSegment2(str, i, i3);
        } else if (this._maximumNonEscapedChar == 0) {
            _writeStringSegment2(str, i, i3);
        } else {
            _writeStringSegmentASCII2(str, i, i3);
        }
    }

    private final void _writeStringSegment2(char[] cArr, int i, int i2) throws IOException {
        if (this._outputTail + ((i2 - i) * 6) > this._outputEnd) {
            _flushBuffer();
        }
        int i3 = this._outputTail;
        byte[] bArr = this._outputBuffer;
        int[] iArr = this._outputEscapes;
        while (i < i2) {
            int i4 = i + 1;
            char c = cArr[i];
            if (c > 127) {
                if (c <= 2047) {
                    int i5 = i3 + 1;
                    bArr[i3] = (byte) ((c >> 6) | 192);
                    i3 = i5 + 1;
                    bArr[i5] = (byte) ((c & '?') | 128);
                } else {
                    i3 = _outputMultiByteChar(c, i3);
                }
                i = i4;
            } else if (iArr[c] == 0) {
                int i6 = i3 + 1;
                bArr[i3] = (byte) c;
                i3 = i6;
                i = i4;
            } else {
                int i7 = iArr[c];
                if (i7 > 0) {
                    int i8 = i3 + 1;
                    bArr[i3] = BYTE_BACKSLASH;
                    i3 = i8 + 1;
                    bArr[i8] = (byte) i7;
                    i = i4;
                } else {
                    i3 = _writeGenericEscape(c, i3);
                    i = i4;
                }
            }
        }
        this._outputTail = i3;
    }

    private final void _writeStringSegment2(String str, int i, int i2) throws IOException {
        if (this._outputTail + ((i2 - i) * 6) > this._outputEnd) {
            _flushBuffer();
        }
        int i3 = this._outputTail;
        byte[] bArr = this._outputBuffer;
        int[] iArr = this._outputEscapes;
        while (i < i2) {
            int i4 = i + 1;
            char charAt = str.charAt(i);
            if (charAt > 127) {
                if (charAt <= 2047) {
                    int i5 = i3 + 1;
                    bArr[i3] = (byte) ((charAt >> 6) | 192);
                    i3 = i5 + 1;
                    bArr[i5] = (byte) ((charAt & '?') | 128);
                } else {
                    i3 = _outputMultiByteChar(charAt, i3);
                }
                i = i4;
            } else if (iArr[charAt] == 0) {
                int i6 = i3 + 1;
                bArr[i3] = (byte) charAt;
                i3 = i6;
                i = i4;
            } else {
                int i7 = iArr[charAt];
                if (i7 > 0) {
                    int i8 = i3 + 1;
                    bArr[i3] = BYTE_BACKSLASH;
                    i3 = i8 + 1;
                    bArr[i8] = (byte) i7;
                    i = i4;
                } else {
                    i3 = _writeGenericEscape(charAt, i3);
                    i = i4;
                }
            }
        }
        this._outputTail = i3;
    }

    private final void _writeStringSegmentASCII2(char[] cArr, int i, int i2) throws IOException {
        if (this._outputTail + ((i2 - i) * 6) > this._outputEnd) {
            _flushBuffer();
        }
        int i3 = this._outputTail;
        byte[] bArr = this._outputBuffer;
        int[] iArr = this._outputEscapes;
        int i4 = this._maximumNonEscapedChar;
        while (i < i2) {
            int i5 = i + 1;
            char c = cArr[i];
            if (c <= 127) {
                if (iArr[c] == 0) {
                    int i6 = i3 + 1;
                    bArr[i3] = (byte) c;
                    i3 = i6;
                    i = i5;
                } else {
                    int i7 = iArr[c];
                    if (i7 > 0) {
                        int i8 = i3 + 1;
                        bArr[i3] = BYTE_BACKSLASH;
                        i3 = i8 + 1;
                        bArr[i8] = (byte) i7;
                        i = i5;
                    } else {
                        i3 = _writeGenericEscape(c, i3);
                        i = i5;
                    }
                }
            } else if (c > i4) {
                i3 = _writeGenericEscape(c, i3);
                i = i5;
            } else {
                if (c <= 2047) {
                    int i9 = i3 + 1;
                    bArr[i3] = (byte) ((c >> 6) | 192);
                    i3 = i9 + 1;
                    bArr[i9] = (byte) ((c & '?') | 128);
                } else {
                    i3 = _outputMultiByteChar(c, i3);
                }
                i = i5;
            }
        }
        this._outputTail = i3;
    }

    private final void _writeStringSegmentASCII2(String str, int i, int i2) throws IOException {
        if (this._outputTail + ((i2 - i) * 6) > this._outputEnd) {
            _flushBuffer();
        }
        int i3 = this._outputTail;
        byte[] bArr = this._outputBuffer;
        int[] iArr = this._outputEscapes;
        int i4 = this._maximumNonEscapedChar;
        while (i < i2) {
            int i5 = i + 1;
            char charAt = str.charAt(i);
            if (charAt <= 127) {
                if (iArr[charAt] == 0) {
                    int i6 = i3 + 1;
                    bArr[i3] = (byte) charAt;
                    i3 = i6;
                    i = i5;
                } else {
                    int i7 = iArr[charAt];
                    if (i7 > 0) {
                        int i8 = i3 + 1;
                        bArr[i3] = BYTE_BACKSLASH;
                        i3 = i8 + 1;
                        bArr[i8] = (byte) i7;
                        i = i5;
                    } else {
                        i3 = _writeGenericEscape(charAt, i3);
                        i = i5;
                    }
                }
            } else if (charAt > i4) {
                i3 = _writeGenericEscape(charAt, i3);
                i = i5;
            } else {
                if (charAt <= 2047) {
                    int i9 = i3 + 1;
                    bArr[i3] = (byte) ((charAt >> 6) | 192);
                    i3 = i9 + 1;
                    bArr[i9] = (byte) ((charAt & '?') | 128);
                } else {
                    i3 = _outputMultiByteChar(charAt, i3);
                }
                i = i5;
            }
        }
        this._outputTail = i3;
    }

    private final void _writeCustomStringSegment2(char[] cArr, int i, int i2) throws IOException {
        if (this._outputTail + ((i2 - i) * 6) > this._outputEnd) {
            _flushBuffer();
        }
        int i3 = this._outputTail;
        byte[] bArr = this._outputBuffer;
        int[] iArr = this._outputEscapes;
        int i4 = this._maximumNonEscapedChar <= 0 ? 65535 : this._maximumNonEscapedChar;
        CharacterEscapes characterEscapes = this._characterEscapes;
        while (i < i2) {
            int i5 = i + 1;
            char c = cArr[i];
            if (c <= 127) {
                if (iArr[c] == 0) {
                    int i6 = i3 + 1;
                    bArr[i3] = (byte) c;
                    i3 = i6;
                    i = i5;
                } else {
                    int i7 = iArr[c];
                    if (i7 > 0) {
                        int i8 = i3 + 1;
                        bArr[i3] = BYTE_BACKSLASH;
                        i3 = i8 + 1;
                        bArr[i8] = (byte) i7;
                        i = i5;
                    } else if (i7 == -2) {
                        SerializableString escapeSequence = characterEscapes.getEscapeSequence(c);
                        if (escapeSequence == null) {
                            _reportError("Invalid custom escape definitions; custom escape not found for character code 0x" + Integer.toHexString(c) + ", although was supposed to have one");
                        }
                        i3 = _writeCustomEscape(bArr, i3, escapeSequence, i2 - i5);
                        i = i5;
                    } else {
                        i3 = _writeGenericEscape(c, i3);
                        i = i5;
                    }
                }
            } else if (c > i4) {
                i3 = _writeGenericEscape(c, i3);
                i = i5;
            } else {
                SerializableString escapeSequence2 = characterEscapes.getEscapeSequence(c);
                if (escapeSequence2 != null) {
                    i3 = _writeCustomEscape(bArr, i3, escapeSequence2, i2 - i5);
                    i = i5;
                } else {
                    if (c <= 2047) {
                        int i9 = i3 + 1;
                        bArr[i3] = (byte) ((c >> 6) | 192);
                        i3 = i9 + 1;
                        bArr[i9] = (byte) ((c & '?') | 128);
                    } else {
                        i3 = _outputMultiByteChar(c, i3);
                    }
                    i = i5;
                }
            }
        }
        this._outputTail = i3;
    }

    private final void _writeCustomStringSegment2(String str, int i, int i2) throws IOException {
        if (this._outputTail + ((i2 - i) * 6) > this._outputEnd) {
            _flushBuffer();
        }
        int i3 = this._outputTail;
        byte[] bArr = this._outputBuffer;
        int[] iArr = this._outputEscapes;
        int i4 = this._maximumNonEscapedChar <= 0 ? 65535 : this._maximumNonEscapedChar;
        CharacterEscapes characterEscapes = this._characterEscapes;
        while (i < i2) {
            int i5 = i + 1;
            char charAt = str.charAt(i);
            if (charAt <= 127) {
                if (iArr[charAt] == 0) {
                    int i6 = i3 + 1;
                    bArr[i3] = (byte) charAt;
                    i3 = i6;
                    i = i5;
                } else {
                    int i7 = iArr[charAt];
                    if (i7 > 0) {
                        int i8 = i3 + 1;
                        bArr[i3] = BYTE_BACKSLASH;
                        i3 = i8 + 1;
                        bArr[i8] = (byte) i7;
                        i = i5;
                    } else if (i7 == -2) {
                        SerializableString escapeSequence = characterEscapes.getEscapeSequence(charAt);
                        if (escapeSequence == null) {
                            _reportError("Invalid custom escape definitions; custom escape not found for character code 0x" + Integer.toHexString(charAt) + ", although was supposed to have one");
                        }
                        i3 = _writeCustomEscape(bArr, i3, escapeSequence, i2 - i5);
                        i = i5;
                    } else {
                        i3 = _writeGenericEscape(charAt, i3);
                        i = i5;
                    }
                }
            } else if (charAt > i4) {
                i3 = _writeGenericEscape(charAt, i3);
                i = i5;
            } else {
                SerializableString escapeSequence2 = characterEscapes.getEscapeSequence(charAt);
                if (escapeSequence2 != null) {
                    i3 = _writeCustomEscape(bArr, i3, escapeSequence2, i2 - i5);
                    i = i5;
                } else {
                    if (charAt <= 2047) {
                        int i9 = i3 + 1;
                        bArr[i3] = (byte) ((charAt >> 6) | 192);
                        i3 = i9 + 1;
                        bArr[i9] = (byte) ((charAt & '?') | 128);
                    } else {
                        i3 = _outputMultiByteChar(charAt, i3);
                    }
                    i = i5;
                }
            }
        }
        this._outputTail = i3;
    }

    private final int _writeCustomEscape(byte[] bArr, int i, SerializableString serializableString, int i2) throws IOException, JsonGenerationException {
        byte[] asUnquotedUTF8 = serializableString.asUnquotedUTF8();
        int length = asUnquotedUTF8.length;
        if (length > 6) {
            return _handleLongCustomEscape(bArr, i, this._outputEnd, asUnquotedUTF8, i2);
        }
        System.arraycopy(asUnquotedUTF8, 0, bArr, i, length);
        return length + i;
    }

    private final int _handleLongCustomEscape(byte[] bArr, int i, int i2, byte[] bArr2, int i3) throws IOException, JsonGenerationException {
        int i4;
        int length = bArr2.length;
        if (i + length > i2) {
            this._outputTail = i;
            _flushBuffer();
            int i5 = this._outputTail;
            if (length > bArr.length) {
                this._outputStream.write(bArr2, 0, length);
                return i5;
            }
            System.arraycopy(bArr2, 0, bArr, i5, length);
            i4 = i5 + length;
        } else {
            i4 = i;
        }
        if ((i3 * 6) + i4 <= i2) {
            return i4;
        }
        _flushBuffer();
        return this._outputTail;
    }

    private final void _writeUTF8Segments(byte[] bArr, int i, int i2) throws IOException, JsonGenerationException {
        do {
            int min = Math.min(this._outputMaxContiguous, i2);
            _writeUTF8Segment(bArr, i, min);
            i += min;
            i2 -= min;
        } while (i2 > 0);
    }

    private final void _writeUTF8Segment(byte[] bArr, int i, int i2) throws IOException, JsonGenerationException {
        int[] iArr = this._outputEscapes;
        int i3 = i + i2;
        int i4 = i;
        while (i4 < i3) {
            int i5 = i4 + 1;
            byte b = bArr[i4];
            if (b < 0 || iArr[b] == 0) {
                i4 = i5;
            } else {
                _writeUTF8Segment2(bArr, i, i2);
                return;
            }
        }
        if (this._outputTail + i2 > this._outputEnd) {
            _flushBuffer();
        }
        System.arraycopy(bArr, i, this._outputBuffer, this._outputTail, i2);
        this._outputTail += i2;
    }

    private final void _writeUTF8Segment2(byte[] bArr, int i, int i2) throws IOException, JsonGenerationException {
        int i3;
        int i4 = this._outputTail;
        if ((i2 * 6) + i4 > this._outputEnd) {
            _flushBuffer();
            i4 = this._outputTail;
        }
        byte[] bArr2 = this._outputBuffer;
        int[] iArr = this._outputEscapes;
        int i5 = i2 + i;
        while (i < i5) {
            int i6 = i + 1;
            byte b = bArr[i];
            if (b < 0 || iArr[b] == 0) {
                int i7 = i3 + 1;
                bArr2[i3] = b;
                i3 = i7;
                i = i6;
            } else {
                int i8 = iArr[b];
                if (i8 > 0) {
                    int i9 = i3 + 1;
                    bArr2[i3] = BYTE_BACKSLASH;
                    i3 = i9 + 1;
                    bArr2[i9] = (byte) i8;
                } else {
                    i3 = _writeGenericEscape(b, i3);
                }
                i = i6;
            }
        }
        this._outputTail = i3;
    }

    /* access modifiers changed from: protected */
    public final void _writeBinary(Base64Variant base64Variant, byte[] bArr, int i, int i2) throws IOException, JsonGenerationException {
        int i3 = i2 - 3;
        int i4 = this._outputEnd - 6;
        int maxLineLength = base64Variant.getMaxLineLength() >> 2;
        while (i <= i3) {
            if (this._outputTail > i4) {
                _flushBuffer();
            }
            int i5 = i + 1;
            int i6 = bArr[i] << 8;
            int i7 = i5 + 1;
            i = i7 + 1;
            this._outputTail = base64Variant.encodeBase64Chunk((int) (((bArr[i5] & 255) | i6) << 8) | (bArr[i7] & 255), this._outputBuffer, this._outputTail);
            maxLineLength--;
            if (maxLineLength <= 0) {
                byte[] bArr2 = this._outputBuffer;
                int i8 = this._outputTail;
                this._outputTail = i8 + 1;
                bArr2[i8] = BYTE_BACKSLASH;
                byte[] bArr3 = this._outputBuffer;
                int i9 = this._outputTail;
                this._outputTail = i9 + 1;
                bArr3[i9] = 110;
                maxLineLength = base64Variant.getMaxLineLength() >> 2;
            }
        }
        int i10 = i2 - i;
        if (i10 > 0) {
            if (this._outputTail > i4) {
                _flushBuffer();
            }
            int i11 = i + 1;
            int i12 = bArr[i] << 16;
            if (i10 == 2) {
                int i13 = i11 + 1;
                i12 |= (bArr[i11] & 255) << 8;
            }
            this._outputTail = base64Variant.encodeBase64Partial(i12, i10, this._outputBuffer, this._outputTail);
        }
    }

    /* access modifiers changed from: protected */
    public final int _writeBinary(Base64Variant base64Variant, InputStream inputStream, byte[] bArr, int i) throws IOException, JsonGenerationException {
        int i2;
        int i3 = 0;
        int i4 = 0;
        int i5 = -3;
        int i6 = this._outputEnd - 6;
        int maxLineLength = base64Variant.getMaxLineLength() >> 2;
        int i7 = i;
        while (i7 > 2) {
            if (i3 > i5) {
                i4 = _readMore(inputStream, bArr, i3, i4, i7);
                i3 = 0;
                if (i4 < 3) {
                    break;
                }
                i5 = i4 - 3;
            }
            if (this._outputTail > i6) {
                _flushBuffer();
            }
            int i8 = i3 + 1;
            int i9 = bArr[i3] << 8;
            int i10 = i8 + 1;
            i3 = i10 + 1;
            i7 -= 3;
            this._outputTail = base64Variant.encodeBase64Chunk((int) (((bArr[i8] & 255) | i9) << 8) | (bArr[i10] & 255), this._outputBuffer, this._outputTail);
            int i11 = maxLineLength - 1;
            if (i11 <= 0) {
                byte[] bArr2 = this._outputBuffer;
                int i12 = this._outputTail;
                this._outputTail = i12 + 1;
                bArr2[i12] = BYTE_BACKSLASH;
                byte[] bArr3 = this._outputBuffer;
                int i13 = this._outputTail;
                this._outputTail = i13 + 1;
                bArr3[i13] = 110;
                i11 = base64Variant.getMaxLineLength() >> 2;
            }
            maxLineLength = i11;
        }
        if (i7 <= 0) {
            return i7;
        }
        int _readMore = _readMore(inputStream, bArr, i3, i4, i7);
        if (_readMore <= 0) {
            return i7;
        }
        if (this._outputTail > i6) {
            _flushBuffer();
        }
        int i14 = bArr[0] << 16;
        if (1 < _readMore) {
            i14 |= (bArr[1] & 255) << 8;
            i2 = 2;
        } else {
            i2 = 1;
        }
        this._outputTail = base64Variant.encodeBase64Partial(i14, i2, this._outputBuffer, this._outputTail);
        return i7 - i2;
    }

    /* access modifiers changed from: protected */
    public final int _writeBinary(Base64Variant base64Variant, InputStream inputStream, byte[] bArr) throws IOException, JsonGenerationException {
        int i;
        int i2 = -3;
        int i3 = this._outputEnd - 6;
        int maxLineLength = base64Variant.getMaxLineLength() >> 2;
        int i4 = 0;
        int i5 = 0;
        int i6 = 0;
        while (true) {
            if (i6 > i2) {
                i5 = _readMore(inputStream, bArr, i6, i5, bArr.length);
                if (i5 < 3) {
                    break;
                }
                i2 = i5 - 3;
                i6 = 0;
            }
            if (this._outputTail > i3) {
                _flushBuffer();
            }
            int i7 = i6 + 1;
            int i8 = bArr[i6] << 8;
            int i9 = i7 + 1;
            i6 = i9 + 1;
            i4 += 3;
            this._outputTail = base64Variant.encodeBase64Chunk((int) (((bArr[i7] & 255) | i8) << 8) | (bArr[i9] & 255), this._outputBuffer, this._outputTail);
            int i10 = maxLineLength - 1;
            if (i10 <= 0) {
                byte[] bArr2 = this._outputBuffer;
                int i11 = this._outputTail;
                this._outputTail = i11 + 1;
                bArr2[i11] = BYTE_BACKSLASH;
                byte[] bArr3 = this._outputBuffer;
                int i12 = this._outputTail;
                this._outputTail = i12 + 1;
                bArr3[i12] = 110;
                i10 = base64Variant.getMaxLineLength() >> 2;
            }
            maxLineLength = i10;
        }
        if (0 >= i5) {
            return i4;
        }
        if (this._outputTail > i3) {
            _flushBuffer();
        }
        int i13 = bArr[0] << 16;
        if (1 < i5) {
            i13 |= (bArr[1] & 255) << 8;
            i = 2;
        } else {
            i = 1;
        }
        int i14 = i4 + i;
        this._outputTail = base64Variant.encodeBase64Partial(i13, i, this._outputBuffer, this._outputTail);
        return i14;
    }

    private final int _readMore(InputStream inputStream, byte[] bArr, int i, int i2, int i3) throws IOException {
        int i4 = 0;
        while (i < i2) {
            int i5 = i4 + 1;
            int i6 = i + 1;
            bArr[i4] = bArr[i];
            i4 = i5;
            i = i6;
        }
        int min = Math.min(i3, bArr.length);
        do {
            int i7 = min - i4;
            if (i7 != 0) {
                int read = inputStream.read(bArr, i4, i7);
                if (read < 0) {
                    break;
                }
                i4 += read;
            } else {
                break;
            }
        } while (i4 < 3);
        return i4;
    }

    private final int _outputRawMultiByteChar(int i, char[] cArr, int i2, int i3) throws IOException {
        if (i < 55296 || i > 57343) {
            byte[] bArr = this._outputBuffer;
            int i4 = this._outputTail;
            this._outputTail = i4 + 1;
            bArr[i4] = (byte) ((i >> 12) | 224);
            int i5 = this._outputTail;
            this._outputTail = i5 + 1;
            bArr[i5] = (byte) (((i >> 6) & 63) | 128);
            int i6 = this._outputTail;
            this._outputTail = i6 + 1;
            bArr[i6] = (byte) ((i & 63) | 128);
            return i2;
        }
        if (i2 >= i3 || cArr == null) {
            _reportError("Split surrogate on writeRaw() input (last character)");
        }
        _outputSurrogates(i, cArr[i2]);
        return i2 + 1;
    }

    /* access modifiers changed from: protected */
    public final void _outputSurrogates(int i, int i2) throws IOException {
        int _decodeSurrogate = _decodeSurrogate(i, i2);
        if (this._outputTail + 4 > this._outputEnd) {
            _flushBuffer();
        }
        byte[] bArr = this._outputBuffer;
        int i3 = this._outputTail;
        this._outputTail = i3 + 1;
        bArr[i3] = (byte) ((_decodeSurrogate >> 18) | DisplaySupport.SCREEN_DENSITY_HIGH);
        int i4 = this._outputTail;
        this._outputTail = i4 + 1;
        bArr[i4] = (byte) (((_decodeSurrogate >> 12) & 63) | 128);
        int i5 = this._outputTail;
        this._outputTail = i5 + 1;
        bArr[i5] = (byte) (((_decodeSurrogate >> 6) & 63) | 128);
        int i6 = this._outputTail;
        this._outputTail = i6 + 1;
        bArr[i6] = (byte) ((_decodeSurrogate & 63) | 128);
    }

    private final int _outputMultiByteChar(int i, int i2) throws IOException {
        byte[] bArr = this._outputBuffer;
        if (i < 55296 || i > 57343) {
            int i3 = i2 + 1;
            bArr[i2] = (byte) ((i >> 12) | 224);
            int i4 = i3 + 1;
            bArr[i3] = (byte) (((i >> 6) & 63) | 128);
            int i5 = i4 + 1;
            bArr[i4] = (byte) ((i & 63) | 128);
            return i5;
        }
        int i6 = i2 + 1;
        bArr[i2] = BYTE_BACKSLASH;
        int i7 = i6 + 1;
        bArr[i6] = BYTE_u;
        int i8 = i7 + 1;
        bArr[i7] = HEX_CHARS[(i >> 12) & 15];
        int i9 = i8 + 1;
        bArr[i8] = HEX_CHARS[(i >> 8) & 15];
        int i10 = i9 + 1;
        bArr[i9] = HEX_CHARS[(i >> 4) & 15];
        int i11 = i10 + 1;
        bArr[i10] = HEX_CHARS[i & 15];
        return i11;
    }

    private final void _writeNull() throws IOException {
        if (this._outputTail + 4 >= this._outputEnd) {
            _flushBuffer();
        }
        System.arraycopy(NULL_BYTES, 0, this._outputBuffer, this._outputTail, 4);
        this._outputTail += 4;
    }

    private int _writeGenericEscape(int i, int i2) throws IOException {
        int i3;
        byte[] bArr = this._outputBuffer;
        int i4 = i2 + 1;
        bArr[i2] = BYTE_BACKSLASH;
        int i5 = i4 + 1;
        bArr[i4] = BYTE_u;
        if (i > 255) {
            int i6 = (i >> 8) & 255;
            int i7 = i5 + 1;
            bArr[i5] = HEX_CHARS[i6 >> 4];
            i3 = i7 + 1;
            bArr[i7] = HEX_CHARS[i6 & 15];
            i &= 255;
        } else {
            int i8 = i5 + 1;
            bArr[i5] = BYTE_0;
            i3 = i8 + 1;
            bArr[i8] = BYTE_0;
        }
        int i9 = i3 + 1;
        bArr[i3] = HEX_CHARS[i >> 4];
        int i10 = i9 + 1;
        bArr[i9] = HEX_CHARS[i & 15];
        return i10;
    }

    /* access modifiers changed from: protected */
    public final void _flushBuffer() throws IOException {
        int i = this._outputTail;
        if (i > 0) {
            this._outputTail = 0;
            this._outputStream.write(this._outputBuffer, 0, i);
        }
    }
}
