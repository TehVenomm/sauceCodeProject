package com.fasterxml.jackson.core.p015io;

import com.github.droidfu.support.DisplaySupport;
import java.io.IOException;
import java.io.OutputStream;
import java.io.Writer;

/* renamed from: com.fasterxml.jackson.core.io.UTF8Writer */
public final class UTF8Writer extends Writer {
    static final int SURR1_FIRST = 55296;
    static final int SURR1_LAST = 56319;
    static final int SURR2_FIRST = 56320;
    static final int SURR2_LAST = 57343;
    private final IOContext _context;
    private OutputStream _out;
    private byte[] _outBuffer;
    private final int _outBufferEnd = (this._outBuffer.length - 4);
    private int _outPtr = 0;
    private int _surrogate;

    public UTF8Writer(IOContext iOContext, OutputStream outputStream) {
        this._context = iOContext;
        this._out = outputStream;
        this._outBuffer = iOContext.allocWriteEncodingBuffer();
    }

    public Writer append(char c) throws IOException {
        write((int) c);
        return this;
    }

    public void close() throws IOException {
        if (this._out != null) {
            if (this._outPtr > 0) {
                this._out.write(this._outBuffer, 0, this._outPtr);
                this._outPtr = 0;
            }
            OutputStream outputStream = this._out;
            this._out = null;
            byte[] bArr = this._outBuffer;
            if (bArr != null) {
                this._outBuffer = null;
                this._context.releaseWriteEncodingBuffer(bArr);
            }
            outputStream.close();
            int i = this._surrogate;
            this._surrogate = 0;
            if (i > 0) {
                illegalSurrogate(i);
            }
        }
    }

    public void flush() throws IOException {
        if (this._out != null) {
            if (this._outPtr > 0) {
                this._out.write(this._outBuffer, 0, this._outPtr);
                this._outPtr = 0;
            }
            this._out.flush();
        }
    }

    public void write(char[] cArr) throws IOException {
        write(cArr, 0, cArr.length);
    }

    public void write(char[] cArr, int i, int i2) throws IOException {
        if (i2 >= 2) {
            if (this._surrogate > 0) {
                int i3 = i + 1;
                i2--;
                write(convertSurrogate(cArr[i]));
                i = i3;
            }
            int i4 = this._outPtr;
            byte[] bArr = this._outBuffer;
            int i5 = this._outBufferEnd;
            int i6 = i2 + i;
            int i7 = i;
            while (i7 < i6) {
                if (i4 >= i5) {
                    this._out.write(bArr, 0, i4);
                    i4 = 0;
                }
                int i8 = i7 + 1;
                char c = cArr[i7];
                if (c < 128) {
                    int i9 = i4 + 1;
                    bArr[i4] = (byte) c;
                    int i10 = i6 - i8;
                    int i11 = i5 - i9;
                    if (i10 <= i11) {
                        i11 = i10;
                    }
                    int i12 = i11 + i8;
                    i4 = i9;
                    int i13 = i8;
                    while (true) {
                        if (i13 >= i12) {
                            i7 = i13;
                            break;
                        }
                        i7 = i13 + 1;
                        c = cArr[i13];
                        if (c >= 128) {
                            break;
                        }
                        int i14 = i4 + 1;
                        bArr[i4] = (byte) c;
                        i4 = i14;
                        i13 = i7;
                    }
                } else {
                    i7 = i8;
                }
                if (c < 2048) {
                    int i15 = i4 + 1;
                    bArr[i4] = (byte) ((c >> 6) | 192);
                    i4 = i15 + 1;
                    bArr[i15] = (byte) ((c & '?') | 128);
                } else if (c < 55296 || c > 57343) {
                    int i16 = i4 + 1;
                    bArr[i4] = (byte) ((c >> 12) | 224);
                    int i17 = i16 + 1;
                    bArr[i16] = (byte) (((c >> 6) & 63) | 128);
                    i4 = i17 + 1;
                    bArr[i17] = (byte) ((c & '?') | 128);
                } else {
                    if (c > 56319) {
                        this._outPtr = i4;
                        illegalSurrogate(c);
                    }
                    this._surrogate = c;
                    if (i7 >= i6) {
                        break;
                    }
                    int i18 = i7 + 1;
                    int convertSurrogate = convertSurrogate(cArr[i7]);
                    if (convertSurrogate > 1114111) {
                        this._outPtr = i4;
                        illegalSurrogate(convertSurrogate);
                    }
                    int i19 = i4 + 1;
                    bArr[i4] = (byte) ((convertSurrogate >> 18) | DisplaySupport.SCREEN_DENSITY_HIGH);
                    int i20 = i19 + 1;
                    bArr[i19] = (byte) (((convertSurrogate >> 12) & 63) | 128);
                    int i21 = i20 + 1;
                    bArr[i20] = (byte) (((convertSurrogate >> 6) & 63) | 128);
                    i4 = i21 + 1;
                    bArr[i21] = (byte) ((convertSurrogate & 63) | 128);
                    i7 = i18;
                }
            }
            this._outPtr = i4;
        } else if (i2 == 1) {
            write((int) cArr[i]);
        }
    }

    public void write(int i) throws IOException {
        int i2;
        if (this._surrogate > 0) {
            i = convertSurrogate(i);
        } else if (i >= 55296 && i <= 57343) {
            if (i > 56319) {
                illegalSurrogate(i);
            }
            this._surrogate = i;
            return;
        }
        if (this._outPtr >= this._outBufferEnd) {
            this._out.write(this._outBuffer, 0, this._outPtr);
            this._outPtr = 0;
        }
        if (i < 128) {
            byte[] bArr = this._outBuffer;
            int i3 = this._outPtr;
            this._outPtr = i3 + 1;
            bArr[i3] = (byte) i;
            return;
        }
        int i4 = this._outPtr;
        if (i < 2048) {
            int i5 = i4 + 1;
            this._outBuffer[i4] = (byte) ((i >> 6) | 192);
            i2 = i5 + 1;
            this._outBuffer[i5] = (byte) ((i & 63) | 128);
        } else if (i <= 65535) {
            int i6 = i4 + 1;
            this._outBuffer[i4] = (byte) ((i >> 12) | 224);
            int i7 = i6 + 1;
            this._outBuffer[i6] = (byte) (((i >> 6) & 63) | 128);
            i2 = i7 + 1;
            this._outBuffer[i7] = (byte) ((i & 63) | 128);
        } else {
            if (i > 1114111) {
                illegalSurrogate(i);
            }
            int i8 = i4 + 1;
            this._outBuffer[i4] = (byte) ((i >> 18) | DisplaySupport.SCREEN_DENSITY_HIGH);
            int i9 = i8 + 1;
            this._outBuffer[i8] = (byte) (((i >> 12) & 63) | 128);
            int i10 = i9 + 1;
            this._outBuffer[i9] = (byte) (((i >> 6) & 63) | 128);
            i2 = i10 + 1;
            this._outBuffer[i10] = (byte) ((i & 63) | 128);
        }
        this._outPtr = i2;
    }

    public void write(String str) throws IOException {
        write(str, 0, str.length());
    }

    public void write(String str, int i, int i2) throws IOException {
        if (i2 >= 2) {
            if (this._surrogate > 0) {
                int i3 = i + 1;
                i2--;
                write(convertSurrogate(str.charAt(i)));
                i = i3;
            }
            int i4 = this._outPtr;
            byte[] bArr = this._outBuffer;
            int i5 = this._outBufferEnd;
            int i6 = i2 + i;
            int i7 = i;
            while (i7 < i6) {
                if (i4 >= i5) {
                    this._out.write(bArr, 0, i4);
                    i4 = 0;
                }
                int i8 = i7 + 1;
                char charAt = str.charAt(i7);
                if (charAt < 128) {
                    int i9 = i4 + 1;
                    bArr[i4] = (byte) charAt;
                    int i10 = i6 - i8;
                    int i11 = i5 - i9;
                    if (i10 <= i11) {
                        i11 = i10;
                    }
                    int i12 = i11 + i8;
                    i4 = i9;
                    int i13 = i8;
                    while (true) {
                        if (i13 >= i12) {
                            i7 = i13;
                            break;
                        }
                        i7 = i13 + 1;
                        charAt = str.charAt(i13);
                        if (charAt >= 128) {
                            break;
                        }
                        int i14 = i4 + 1;
                        bArr[i4] = (byte) charAt;
                        i4 = i14;
                        i13 = i7;
                    }
                } else {
                    i7 = i8;
                }
                if (charAt < 2048) {
                    int i15 = i4 + 1;
                    bArr[i4] = (byte) ((charAt >> 6) | 192);
                    i4 = i15 + 1;
                    bArr[i15] = (byte) ((charAt & '?') | 128);
                } else if (charAt < 55296 || charAt > 57343) {
                    int i16 = i4 + 1;
                    bArr[i4] = (byte) ((charAt >> 12) | 224);
                    int i17 = i16 + 1;
                    bArr[i16] = (byte) (((charAt >> 6) & 63) | 128);
                    i4 = i17 + 1;
                    bArr[i17] = (byte) ((charAt & '?') | 128);
                } else {
                    if (charAt > 56319) {
                        this._outPtr = i4;
                        illegalSurrogate(charAt);
                    }
                    this._surrogate = charAt;
                    if (i7 >= i6) {
                        break;
                    }
                    int i18 = i7 + 1;
                    int convertSurrogate = convertSurrogate(str.charAt(i7));
                    if (convertSurrogate > 1114111) {
                        this._outPtr = i4;
                        illegalSurrogate(convertSurrogate);
                    }
                    int i19 = i4 + 1;
                    bArr[i4] = (byte) ((convertSurrogate >> 18) | DisplaySupport.SCREEN_DENSITY_HIGH);
                    int i20 = i19 + 1;
                    bArr[i19] = (byte) (((convertSurrogate >> 12) & 63) | 128);
                    int i21 = i20 + 1;
                    bArr[i20] = (byte) (((convertSurrogate >> 6) & 63) | 128);
                    i4 = i21 + 1;
                    bArr[i21] = (byte) ((convertSurrogate & 63) | 128);
                    i7 = i18;
                }
            }
            this._outPtr = i4;
        } else if (i2 == 1) {
            write((int) str.charAt(i));
        }
    }

    /* access modifiers changed from: protected */
    public int convertSurrogate(int i) throws IOException {
        int i2 = this._surrogate;
        this._surrogate = 0;
        if (i >= 56320 && i <= 57343) {
            return ((i2 - 55296) << 10) + 65536 + (i - 56320);
        }
        throw new IOException("Broken surrogate pair: first char 0x" + Integer.toHexString(i2) + ", second 0x" + Integer.toHexString(i) + "; illegal combination");
    }

    protected static void illegalSurrogate(int i) throws IOException {
        throw new IOException(illegalSurrogateDesc(i));
    }

    protected static String illegalSurrogateDesc(int i) {
        if (i > 1114111) {
            return "Illegal character point (0x" + Integer.toHexString(i) + ") to output; max is 0x10FFFF as per RFC 4627";
        }
        if (i < 55296) {
            return "Illegal character point (0x" + Integer.toHexString(i) + ") to output";
        }
        if (i <= 56319) {
            return "Unmatched first part of surrogate pair (0x" + Integer.toHexString(i) + ")";
        }
        return "Unmatched second part of surrogate pair (0x" + Integer.toHexString(i) + ")";
    }
}
