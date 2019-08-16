package com.fasterxml.jackson.core.p015io;

import com.fasterxml.jackson.core.base.GeneratorBase;
import com.google.firebase.messaging.cpp.SerializedEventUnion;
import java.io.CharConversionException;
import java.io.IOException;
import java.io.InputStream;
import java.io.Reader;

/* renamed from: com.fasterxml.jackson.core.io.UTF32Reader */
public class UTF32Reader extends Reader {
    protected static final int LAST_VALID_UNICODE_CHAR = 1114111;

    /* renamed from: NC */
    protected static final char f412NC = 0;
    protected final boolean _bigEndian;
    protected byte[] _buffer;
    protected int _byteCount;
    protected int _charCount;
    protected final IOContext _context;
    protected InputStream _in;
    protected int _length;
    protected final boolean _managedBuffers;
    protected int _ptr;
    protected char _surrogate = 0;
    protected char[] _tmpBuf;

    public UTF32Reader(IOContext iOContext, InputStream inputStream, byte[] bArr, int i, int i2, boolean z) {
        boolean z2 = false;
        this._context = iOContext;
        this._in = inputStream;
        this._buffer = bArr;
        this._ptr = i;
        this._length = i2;
        this._bigEndian = z;
        if (inputStream != null) {
            z2 = true;
        }
        this._managedBuffers = z2;
    }

    public void close() throws IOException {
        InputStream inputStream = this._in;
        if (inputStream != null) {
            this._in = null;
            freeBuffers();
            inputStream.close();
        }
    }

    public int read() throws IOException {
        if (this._tmpBuf == null) {
            this._tmpBuf = new char[1];
        }
        if (read(this._tmpBuf, 0, 1) < 1) {
            return -1;
        }
        return this._tmpBuf[0];
    }

    public int read(char[] cArr, int i, int i2) throws IOException {
        int i3;
        int i4;
        byte b;
        if (this._buffer == null) {
            return -1;
        }
        if (i2 < 1) {
            return i2;
        }
        if (i < 0 || i + i2 > cArr.length) {
            reportBounds(cArr, i, i2);
        }
        int i5 = i2 + i;
        if (this._surrogate != 0) {
            i3 = i + 1;
            cArr[i] = this._surrogate;
            this._surrogate = 0;
        } else {
            int i6 = this._length - this._ptr;
            if (i6 < 4 && !loadMore(i6)) {
                return -1;
            }
            i3 = i;
        }
        while (true) {
            if (i3 >= i5) {
                i4 = i3;
                break;
            }
            int i7 = this._ptr;
            if (this._bigEndian) {
                b = (this._buffer[i7 + 3] & 255) | (this._buffer[i7] << 24) | ((this._buffer[i7 + 1] & 255) << 16) | ((this._buffer[i7 + 2] & 255) << 8);
            } else {
                b = (this._buffer[i7 + 3] << 24) | (this._buffer[i7] & 255) | ((this._buffer[i7 + 1] & 255) << 8) | ((this._buffer[i7 + 2] & 255) << 16);
            }
            this._ptr += 4;
            if (b > 65535) {
                if (b > LAST_VALID_UNICODE_CHAR) {
                    reportInvalid(b, i3 - i, "(above " + Integer.toHexString(LAST_VALID_UNICODE_CHAR) + ") ");
                }
                int i8 = b - SerializedEventUnion.NONE;
                i4 = i3 + 1;
                cArr[i3] = (char) (GeneratorBase.SURR1_FIRST + (i8 >> 10));
                b = (i8 & 1023) | SerializedEventUnion.NONE;
                if (i4 >= i5) {
                    this._surrogate = (char) b;
                    break;
                }
            } else {
                i4 = i3;
            }
            i3 = i4 + 1;
            cArr[i4] = (char) b;
            if (this._ptr >= this._length) {
                i4 = i3;
                break;
            }
        }
        int i9 = i4 - i;
        this._charCount += i9;
        return i9;
    }

    private void reportUnexpectedEOF(int i, int i2) throws IOException {
        throw new CharConversionException("Unexpected EOF in the middle of a 4-byte UTF-32 char: got " + i + ", needed " + i2 + ", at char #" + this._charCount + ", byte #" + (this._byteCount + i) + ")");
    }

    private void reportInvalid(int i, int i2, String str) throws IOException {
        throw new CharConversionException("Invalid UTF-32 character 0x" + Integer.toHexString(i) + str + " at char #" + (this._charCount + i2) + ", byte #" + ((this._byteCount + this._ptr) - 1) + ")");
    }

    private boolean loadMore(int i) throws IOException {
        int read;
        this._byteCount += this._length - i;
        if (i > 0) {
            if (this._ptr > 0) {
                System.arraycopy(this._buffer, this._ptr, this._buffer, 0, i);
                this._ptr = 0;
            }
            this._length = i;
        } else {
            this._ptr = 0;
            int read2 = this._in == null ? -1 : this._in.read(this._buffer);
            if (read2 < 1) {
                this._length = 0;
                if (read2 >= 0) {
                    reportStrangeStream();
                } else if (!this._managedBuffers) {
                    return false;
                } else {
                    freeBuffers();
                    return false;
                }
            }
            this._length = read2;
        }
        while (this._length < 4) {
            if (this._in == null) {
                read = -1;
            } else {
                read = this._in.read(this._buffer, this._length, this._buffer.length - this._length);
            }
            if (read < 1) {
                if (read < 0) {
                    if (this._managedBuffers) {
                        freeBuffers();
                    }
                    reportUnexpectedEOF(this._length, 4);
                }
                reportStrangeStream();
            }
            this._length = read + this._length;
        }
        return true;
    }

    private void freeBuffers() {
        byte[] bArr = this._buffer;
        if (bArr != null) {
            this._buffer = null;
            this._context.releaseReadIOBuffer(bArr);
        }
    }

    private void reportBounds(char[] cArr, int i, int i2) throws IOException {
        throw new ArrayIndexOutOfBoundsException("read(buf," + i + "," + i2 + "), cbuf[" + cArr.length + "]");
    }

    private void reportStrangeStream() throws IOException {
        throw new IOException("Strange I/O stream, returned 0 bytes on read");
    }
}
