package com.fasterxml.jackson.core.json;

import com.fasterxml.jackson.core.JsonEncoding;
import com.fasterxml.jackson.core.JsonFactory.Feature;
import com.fasterxml.jackson.core.JsonParser;
import com.fasterxml.jackson.core.ObjectCodec;
import com.fasterxml.jackson.core.format.InputAccessor;
import com.fasterxml.jackson.core.format.MatchStrength;
import com.fasterxml.jackson.core.p015io.IOContext;
import com.fasterxml.jackson.core.p015io.MergedStream;
import com.fasterxml.jackson.core.p015io.UTF32Reader;
import com.fasterxml.jackson.core.sym.ByteQuadsCanonicalizer;
import com.fasterxml.jackson.core.sym.CharsToNameCanonicalizer;
import java.io.ByteArrayInputStream;
import java.io.CharConversionException;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.io.Reader;

public final class ByteSourceJsonBootstrapper {
    static final byte UTF8_BOM_1 = -17;
    static final byte UTF8_BOM_2 = -69;
    static final byte UTF8_BOM_3 = -65;
    protected boolean _bigEndian = true;
    private final boolean _bufferRecyclable;
    protected int _bytesPerChar;
    protected final IOContext _context;
    protected final InputStream _in;
    protected final byte[] _inputBuffer;
    private int _inputEnd;
    protected int _inputProcessed;
    private int _inputPtr;

    public ByteSourceJsonBootstrapper(IOContext iOContext, InputStream inputStream) {
        this._context = iOContext;
        this._in = inputStream;
        this._inputBuffer = iOContext.allocReadIOBuffer();
        this._inputPtr = 0;
        this._inputEnd = 0;
        this._inputProcessed = 0;
        this._bufferRecyclable = true;
    }

    public ByteSourceJsonBootstrapper(IOContext iOContext, byte[] bArr, int i, int i2) {
        this._context = iOContext;
        this._in = null;
        this._inputBuffer = bArr;
        this._inputPtr = i;
        this._inputEnd = i + i2;
        this._inputProcessed = -i;
        this._bufferRecyclable = false;
    }

    /* JADX WARNING: Code restructure failed: missing block: B:11:0x0052, code lost:
        if (checkUTF16(r2 >>> 16) != false) goto L_0x003c;
     */
    /* JADX WARNING: Code restructure failed: missing block: B:16:0x0076, code lost:
        if (checkUTF16(((r5._inputBuffer[r5._inputPtr] & 255) << 8) | (r5._inputBuffer[r5._inputPtr + 1] & 255)) != false) goto L_0x003c;
     */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    public com.fasterxml.jackson.core.JsonEncoding detectEncoding() throws java.io.IOException {
        /*
            r5 = this;
            r0 = 1
            r1 = 0
            r2 = 4
            boolean r2 = r5.ensureLoaded(r2)
            if (r2 == 0) goto L_0x0056
            byte[] r2 = r5._inputBuffer
            int r3 = r5._inputPtr
            byte r2 = r2[r3]
            int r2 = r2 << 24
            byte[] r3 = r5._inputBuffer
            int r4 = r5._inputPtr
            int r4 = r4 + 1
            byte r3 = r3[r4]
            r3 = r3 & 255(0xff, float:3.57E-43)
            int r3 = r3 << 16
            r2 = r2 | r3
            byte[] r3 = r5._inputBuffer
            int r4 = r5._inputPtr
            int r4 = r4 + 2
            byte r3 = r3[r4]
            r3 = r3 & 255(0xff, float:3.57E-43)
            int r3 = r3 << 8
            r2 = r2 | r3
            byte[] r3 = r5._inputBuffer
            int r4 = r5._inputPtr
            int r4 = r4 + 3
            byte r3 = r3[r4]
            r3 = r3 & 255(0xff, float:3.57E-43)
            r2 = r2 | r3
            boolean r3 = r5.handleBOM(r2)
            if (r3 == 0) goto L_0x0046
        L_0x003c:
            if (r0 != 0) goto L_0x0079
            com.fasterxml.jackson.core.JsonEncoding r0 = com.fasterxml.jackson.core.JsonEncoding.UTF8
        L_0x0040:
            com.fasterxml.jackson.core.io.IOContext r1 = r5._context
            r1.setEncoding(r0)
            return r0
        L_0x0046:
            boolean r3 = r5.checkUTF32(r2)
            if (r3 != 0) goto L_0x003c
            int r2 = r2 >>> 16
            boolean r2 = r5.checkUTF16(r2)
            if (r2 != 0) goto L_0x003c
        L_0x0054:
            r0 = r1
            goto L_0x003c
        L_0x0056:
            r2 = 2
            boolean r2 = r5.ensureLoaded(r2)
            if (r2 == 0) goto L_0x0054
            byte[] r2 = r5._inputBuffer
            int r3 = r5._inputPtr
            byte r2 = r2[r3]
            r2 = r2 & 255(0xff, float:3.57E-43)
            int r2 = r2 << 8
            byte[] r3 = r5._inputBuffer
            int r4 = r5._inputPtr
            int r4 = r4 + 1
            byte r3 = r3[r4]
            r3 = r3 & 255(0xff, float:3.57E-43)
            r2 = r2 | r3
            boolean r2 = r5.checkUTF16(r2)
            if (r2 == 0) goto L_0x0054
            goto L_0x003c
        L_0x0079:
            int r0 = r5._bytesPerChar
            switch(r0) {
                case 1: goto L_0x0086;
                case 2: goto L_0x0089;
                case 3: goto L_0x007e;
                case 4: goto L_0x0093;
                default: goto L_0x007e;
            }
        L_0x007e:
            java.lang.RuntimeException r0 = new java.lang.RuntimeException
            java.lang.String r1 = "Internal error"
            r0.<init>(r1)
            throw r0
        L_0x0086:
            com.fasterxml.jackson.core.JsonEncoding r0 = com.fasterxml.jackson.core.JsonEncoding.UTF8
            goto L_0x0040
        L_0x0089:
            boolean r0 = r5._bigEndian
            if (r0 == 0) goto L_0x0090
            com.fasterxml.jackson.core.JsonEncoding r0 = com.fasterxml.jackson.core.JsonEncoding.UTF16_BE
            goto L_0x0040
        L_0x0090:
            com.fasterxml.jackson.core.JsonEncoding r0 = com.fasterxml.jackson.core.JsonEncoding.UTF16_LE
            goto L_0x0040
        L_0x0093:
            boolean r0 = r5._bigEndian
            if (r0 == 0) goto L_0x009a
            com.fasterxml.jackson.core.JsonEncoding r0 = com.fasterxml.jackson.core.JsonEncoding.UTF32_BE
            goto L_0x0040
        L_0x009a:
            com.fasterxml.jackson.core.JsonEncoding r0 = com.fasterxml.jackson.core.JsonEncoding.UTF32_LE
            goto L_0x0040
        */
        throw new UnsupportedOperationException("Method not decompiled: com.fasterxml.jackson.core.json.ByteSourceJsonBootstrapper.detectEncoding():com.fasterxml.jackson.core.JsonEncoding");
    }

    public Reader constructReader() throws IOException {
        InputStream inputStream;
        JsonEncoding encoding = this._context.getEncoding();
        switch (encoding.bits()) {
            case 8:
            case 16:
                InputStream inputStream2 = this._in;
                if (inputStream2 == null) {
                    inputStream = new ByteArrayInputStream(this._inputBuffer, this._inputPtr, this._inputEnd);
                } else if (this._inputPtr < this._inputEnd) {
                    inputStream = new MergedStream(this._context, inputStream2, this._inputBuffer, this._inputPtr, this._inputEnd);
                } else {
                    inputStream = inputStream2;
                }
                return new InputStreamReader(inputStream, encoding.getJavaName());
            case 32:
                return new UTF32Reader(this._context, this._in, this._inputBuffer, this._inputPtr, this._inputEnd, this._context.getEncoding().isBigEndian());
            default:
                throw new RuntimeException("Internal error");
        }
    }

    public JsonParser constructParser(int i, ObjectCodec objectCodec, ByteQuadsCanonicalizer byteQuadsCanonicalizer, CharsToNameCanonicalizer charsToNameCanonicalizer, int i2) throws IOException {
        if (detectEncoding() != JsonEncoding.UTF8 || !Feature.CANONICALIZE_FIELD_NAMES.enabledIn(i2)) {
            return new ReaderBasedJsonParser(this._context, i, constructReader(), objectCodec, charsToNameCanonicalizer.makeChild(i2));
        }
        return new UTF8StreamJsonParser(this._context, i, this._in, objectCodec, byteQuadsCanonicalizer.makeChild(i2), this._inputBuffer, this._inputPtr, this._inputEnd, this._bufferRecyclable);
    }

    public static MatchStrength hasJSONFormat(InputAccessor inputAccessor) throws IOException {
        if (!inputAccessor.hasMoreBytes()) {
            return MatchStrength.INCONCLUSIVE;
        }
        byte nextByte = inputAccessor.nextByte();
        if (nextByte == -17) {
            if (!inputAccessor.hasMoreBytes()) {
                return MatchStrength.INCONCLUSIVE;
            }
            if (inputAccessor.nextByte() != -69) {
                return MatchStrength.NO_MATCH;
            }
            if (!inputAccessor.hasMoreBytes()) {
                return MatchStrength.INCONCLUSIVE;
            }
            if (inputAccessor.nextByte() != -65) {
                return MatchStrength.NO_MATCH;
            }
            if (!inputAccessor.hasMoreBytes()) {
                return MatchStrength.INCONCLUSIVE;
            }
            nextByte = inputAccessor.nextByte();
        }
        int skipSpace = skipSpace(inputAccessor, nextByte);
        if (skipSpace < 0) {
            return MatchStrength.INCONCLUSIVE;
        }
        if (skipSpace == 123) {
            int skipSpace2 = skipSpace(inputAccessor);
            if (skipSpace2 < 0) {
                return MatchStrength.INCONCLUSIVE;
            }
            if (skipSpace2 == 34 || skipSpace2 == 125) {
                return MatchStrength.SOLID_MATCH;
            }
            return MatchStrength.NO_MATCH;
        } else if (skipSpace == 91) {
            int skipSpace3 = skipSpace(inputAccessor);
            if (skipSpace3 < 0) {
                return MatchStrength.INCONCLUSIVE;
            }
            if (skipSpace3 == 93 || skipSpace3 == 91) {
                return MatchStrength.SOLID_MATCH;
            }
            return MatchStrength.SOLID_MATCH;
        } else {
            MatchStrength matchStrength = MatchStrength.WEAK_MATCH;
            if (skipSpace == 34) {
                return matchStrength;
            }
            if (skipSpace <= 57 && skipSpace >= 48) {
                return matchStrength;
            }
            if (skipSpace == 45) {
                int skipSpace4 = skipSpace(inputAccessor);
                if (skipSpace4 < 0) {
                    return MatchStrength.INCONCLUSIVE;
                }
                if (skipSpace4 > 57 || skipSpace4 < 48) {
                    return MatchStrength.NO_MATCH;
                }
                return matchStrength;
            } else if (skipSpace == 110) {
                return tryMatch(inputAccessor, "ull", matchStrength);
            } else {
                if (skipSpace == 116) {
                    return tryMatch(inputAccessor, "rue", matchStrength);
                }
                if (skipSpace == 102) {
                    return tryMatch(inputAccessor, "alse", matchStrength);
                }
                return MatchStrength.NO_MATCH;
            }
        }
    }

    private static MatchStrength tryMatch(InputAccessor inputAccessor, String str, MatchStrength matchStrength) throws IOException {
        int length = str.length();
        for (int i = 0; i < length; i++) {
            if (!inputAccessor.hasMoreBytes()) {
                return MatchStrength.INCONCLUSIVE;
            }
            if (inputAccessor.nextByte() != str.charAt(i)) {
                return MatchStrength.NO_MATCH;
            }
        }
        return matchStrength;
    }

    private static int skipSpace(InputAccessor inputAccessor) throws IOException {
        if (!inputAccessor.hasMoreBytes()) {
            return -1;
        }
        return skipSpace(inputAccessor, inputAccessor.nextByte());
    }

    private static int skipSpace(InputAccessor inputAccessor, byte b) throws IOException {
        while (true) {
            byte b2 = b & 255;
            if (b2 != 32 && b2 != 13 && b2 != 10 && b2 != 9) {
                return b2;
            }
            if (!inputAccessor.hasMoreBytes()) {
                return -1;
            }
            b = inputAccessor.nextByte();
            byte b3 = b & 255;
        }
    }

    private boolean handleBOM(int i) throws IOException {
        switch (i) {
            case -16842752:
                break;
            case -131072:
                this._inputPtr += 4;
                this._bytesPerChar = 4;
                this._bigEndian = false;
                return true;
            case 65279:
                this._bigEndian = true;
                this._inputPtr += 4;
                this._bytesPerChar = 4;
                return true;
            case 65534:
                reportWeirdUCS4("2143");
                break;
        }
        reportWeirdUCS4("3412");
        int i2 = i >>> 16;
        if (i2 == 65279) {
            this._inputPtr += 2;
            this._bytesPerChar = 2;
            this._bigEndian = true;
            return true;
        } else if (i2 == 65534) {
            this._inputPtr += 2;
            this._bytesPerChar = 2;
            this._bigEndian = false;
            return true;
        } else if ((i >>> 8) != 15711167) {
            return false;
        } else {
            this._inputPtr += 3;
            this._bytesPerChar = 1;
            this._bigEndian = true;
            return true;
        }
    }

    private boolean checkUTF32(int i) throws IOException {
        if ((i >> 8) == 0) {
            this._bigEndian = true;
        } else if ((16777215 & i) == 0) {
            this._bigEndian = false;
        } else if ((-16711681 & i) == 0) {
            reportWeirdUCS4("3412");
        } else if ((-65281 & i) != 0) {
            return false;
        } else {
            reportWeirdUCS4("2143");
        }
        this._bytesPerChar = 4;
        return true;
    }

    private boolean checkUTF16(int i) {
        if ((65280 & i) == 0) {
            this._bigEndian = true;
        } else if ((i & 255) != 0) {
            return false;
        } else {
            this._bigEndian = false;
        }
        this._bytesPerChar = 2;
        return true;
    }

    private void reportWeirdUCS4(String str) throws IOException {
        throw new CharConversionException("Unsupported UCS-4 endianness (" + str + ") detected");
    }

    /* access modifiers changed from: protected */
    public boolean ensureLoaded(int i) throws IOException {
        int read;
        int i2 = this._inputEnd - this._inputPtr;
        while (i2 < i) {
            if (this._in == null) {
                read = -1;
            } else {
                read = this._in.read(this._inputBuffer, this._inputEnd, this._inputBuffer.length - this._inputEnd);
            }
            if (read < 1) {
                return false;
            }
            this._inputEnd += read;
            i2 = read + i2;
        }
        return true;
    }
}
