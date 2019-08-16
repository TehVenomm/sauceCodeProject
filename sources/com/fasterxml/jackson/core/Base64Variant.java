package com.fasterxml.jackson.core;

import com.fasterxml.jackson.core.util.ByteArrayBuilder;
import java.io.Serializable;
import java.util.Arrays;

public final class Base64Variant implements Serializable {
    public static final int BASE64_VALUE_INVALID = -1;
    public static final int BASE64_VALUE_PADDING = -2;
    private static final int INT_SPACE = 32;
    static final char PADDING_CHAR_NONE = '\u0000';
    private static final long serialVersionUID = 1;
    private final transient int[] _asciiToBase64;
    private final transient byte[] _base64ToAsciiB;
    private final transient char[] _base64ToAsciiC;
    protected final transient int _maxLineLength;
    protected final String _name;
    protected final transient char _paddingChar;
    protected final transient boolean _usesPadding;

    public Base64Variant(String str, String str2, boolean z, char c, int i) {
        this._asciiToBase64 = new int[128];
        this._base64ToAsciiC = new char[64];
        this._base64ToAsciiB = new byte[64];
        this._name = str;
        this._usesPadding = z;
        this._paddingChar = c;
        this._maxLineLength = i;
        int length = str2.length();
        if (length != 64) {
            throw new IllegalArgumentException("Base64Alphabet length must be exactly 64 (was " + length + ")");
        }
        str2.getChars(0, length, this._base64ToAsciiC, 0);
        Arrays.fill(this._asciiToBase64, -1);
        for (int i2 = 0; i2 < length; i2++) {
            char c2 = this._base64ToAsciiC[i2];
            this._base64ToAsciiB[i2] = (byte) c2;
            this._asciiToBase64[c2] = i2;
        }
        if (z) {
            this._asciiToBase64[c] = -2;
        }
    }

    public Base64Variant(Base64Variant base64Variant, String str, int i) {
        this(base64Variant, str, base64Variant._usesPadding, base64Variant._paddingChar, i);
    }

    public Base64Variant(Base64Variant base64Variant, String str, boolean z, char c, int i) {
        this._asciiToBase64 = new int[128];
        this._base64ToAsciiC = new char[64];
        this._base64ToAsciiB = new byte[64];
        this._name = str;
        byte[] bArr = base64Variant._base64ToAsciiB;
        System.arraycopy(bArr, 0, this._base64ToAsciiB, 0, bArr.length);
        char[] cArr = base64Variant._base64ToAsciiC;
        System.arraycopy(cArr, 0, this._base64ToAsciiC, 0, cArr.length);
        int[] iArr = base64Variant._asciiToBase64;
        System.arraycopy(iArr, 0, this._asciiToBase64, 0, iArr.length);
        this._usesPadding = z;
        this._paddingChar = c;
        this._maxLineLength = i;
    }

    /* access modifiers changed from: protected */
    public Object readResolve() {
        return Base64Variants.valueOf(this._name);
    }

    public String getName() {
        return this._name;
    }

    public boolean usesPadding() {
        return this._usesPadding;
    }

    public boolean usesPaddingChar(char c) {
        return c == this._paddingChar;
    }

    public boolean usesPaddingChar(int i) {
        return i == this._paddingChar;
    }

    public char getPaddingChar() {
        return this._paddingChar;
    }

    public byte getPaddingByte() {
        return (byte) this._paddingChar;
    }

    public int getMaxLineLength() {
        return this._maxLineLength;
    }

    public int decodeBase64Char(char c) {
        if (c <= 127) {
            return this._asciiToBase64[c];
        }
        return -1;
    }

    public int decodeBase64Char(int i) {
        if (i <= 127) {
            return this._asciiToBase64[i];
        }
        return -1;
    }

    public int decodeBase64Byte(byte b) {
        if (b <= Byte.MAX_VALUE) {
            return this._asciiToBase64[b];
        }
        return -1;
    }

    public char encodeBase64BitsAsChar(int i) {
        return this._base64ToAsciiC[i];
    }

    public int encodeBase64Chunk(int i, char[] cArr, int i2) {
        int i3 = i2 + 1;
        cArr[i2] = this._base64ToAsciiC[(i >> 18) & 63];
        int i4 = i3 + 1;
        cArr[i3] = this._base64ToAsciiC[(i >> 12) & 63];
        int i5 = i4 + 1;
        cArr[i4] = this._base64ToAsciiC[(i >> 6) & 63];
        int i6 = i5 + 1;
        cArr[i5] = this._base64ToAsciiC[i & 63];
        return i6;
    }

    public void encodeBase64Chunk(StringBuilder sb, int i) {
        sb.append(this._base64ToAsciiC[(i >> 18) & 63]);
        sb.append(this._base64ToAsciiC[(i >> 12) & 63]);
        sb.append(this._base64ToAsciiC[(i >> 6) & 63]);
        sb.append(this._base64ToAsciiC[i & 63]);
    }

    public int encodeBase64Partial(int i, int i2, char[] cArr, int i3) {
        int i4 = i3 + 1;
        cArr[i3] = this._base64ToAsciiC[(i >> 18) & 63];
        int i5 = i4 + 1;
        cArr[i4] = this._base64ToAsciiC[(i >> 12) & 63];
        if (this._usesPadding) {
            int i6 = i5 + 1;
            cArr[i5] = i2 == 2 ? this._base64ToAsciiC[(i >> 6) & 63] : this._paddingChar;
            int i7 = i6 + 1;
            cArr[i6] = this._paddingChar;
            return i7;
        } else if (i2 != 2) {
            return i5;
        } else {
            int i8 = i5 + 1;
            cArr[i5] = this._base64ToAsciiC[(i >> 6) & 63];
            return i8;
        }
    }

    public void encodeBase64Partial(StringBuilder sb, int i, int i2) {
        sb.append(this._base64ToAsciiC[(i >> 18) & 63]);
        sb.append(this._base64ToAsciiC[(i >> 12) & 63]);
        if (this._usesPadding) {
            sb.append(i2 == 2 ? this._base64ToAsciiC[(i >> 6) & 63] : this._paddingChar);
            sb.append(this._paddingChar);
        } else if (i2 == 2) {
            sb.append(this._base64ToAsciiC[(i >> 6) & 63]);
        }
    }

    public byte encodeBase64BitsAsByte(int i) {
        return this._base64ToAsciiB[i];
    }

    public int encodeBase64Chunk(int i, byte[] bArr, int i2) {
        int i3 = i2 + 1;
        bArr[i2] = this._base64ToAsciiB[(i >> 18) & 63];
        int i4 = i3 + 1;
        bArr[i3] = this._base64ToAsciiB[(i >> 12) & 63];
        int i5 = i4 + 1;
        bArr[i4] = this._base64ToAsciiB[(i >> 6) & 63];
        int i6 = i5 + 1;
        bArr[i5] = this._base64ToAsciiB[i & 63];
        return i6;
    }

    public int encodeBase64Partial(int i, int i2, byte[] bArr, int i3) {
        int i4 = i3 + 1;
        bArr[i3] = this._base64ToAsciiB[(i >> 18) & 63];
        int i5 = i4 + 1;
        bArr[i4] = this._base64ToAsciiB[(i >> 12) & 63];
        if (this._usesPadding) {
            byte b = (byte) this._paddingChar;
            int i6 = i5 + 1;
            bArr[i5] = i2 == 2 ? this._base64ToAsciiB[(i >> 6) & 63] : b;
            int i7 = i6 + 1;
            bArr[i6] = b;
            return i7;
        } else if (i2 != 2) {
            return i5;
        } else {
            int i8 = i5 + 1;
            bArr[i5] = this._base64ToAsciiB[(i >> 6) & 63];
            return i8;
        }
    }

    public String encode(byte[] bArr) {
        return encode(bArr, false);
    }

    public String encode(byte[] bArr, boolean z) {
        int length = bArr.length;
        StringBuilder sb = new StringBuilder((length >> 2) + length + (length >> 3));
        if (z) {
            sb.append('\"');
        }
        int maxLineLength = getMaxLineLength() >> 2;
        int i = length - 3;
        int i2 = 0;
        while (i2 <= i) {
            int i3 = i2 + 1;
            int i4 = bArr[i2] << 8;
            int i5 = i3 + 1;
            i2 = i5 + 1;
            encodeBase64Chunk(sb, (((bArr[i3] & 255) | i4) << 8) | (bArr[i5] & 255));
            int i6 = maxLineLength - 1;
            if (i6 <= 0) {
                sb.append('\\');
                sb.append('n');
                i6 = getMaxLineLength() >> 2;
            }
            maxLineLength = i6;
        }
        int i7 = length - i2;
        if (i7 > 0) {
            int i8 = i2 + 1;
            int i9 = bArr[i2] << 16;
            if (i7 == 2) {
                int i10 = i8 + 1;
                i9 |= (bArr[i8] & 255) << 8;
            }
            encodeBase64Partial(sb, i9, i7);
        }
        if (z) {
            sb.append('\"');
        }
        return sb.toString();
    }

    public byte[] decode(String str) throws IllegalArgumentException {
        ByteArrayBuilder byteArrayBuilder = new ByteArrayBuilder();
        decode(str, byteArrayBuilder);
        return byteArrayBuilder.toByteArray();
    }

    /* JADX WARNING: Code restructure failed: missing block: B:10:0x0023, code lost:
        _reportBase64EOF();
     */
    /* JADX WARNING: Code restructure failed: missing block: B:11:0x0026, code lost:
        r0 = r1 + 1;
        r1 = r11.charAt(r1);
        r5 = decodeBase64Char(r1);
     */
    /* JADX WARNING: Code restructure failed: missing block: B:12:0x0030, code lost:
        if (r5 >= 0) goto L_0x0036;
     */
    /* JADX WARNING: Code restructure failed: missing block: B:13:0x0032, code lost:
        _reportInvalidBase64(r1, 1, null);
     */
    /* JADX WARNING: Code restructure failed: missing block: B:14:0x0036, code lost:
        r1 = (r4 << 6) | r5;
     */
    /* JADX WARNING: Code restructure failed: missing block: B:15:0x0039, code lost:
        if (r0 < r3) goto L_0x004a;
     */
    /* JADX WARNING: Code restructure failed: missing block: B:17:0x003f, code lost:
        if (usesPadding() != false) goto L_0x0047;
     */
    /* JADX WARNING: Code restructure failed: missing block: B:18:0x0041, code lost:
        r12.append(r1 >> 4);
     */
    /* JADX WARNING: Code restructure failed: missing block: B:19:0x0047, code lost:
        _reportBase64EOF();
     */
    /* JADX WARNING: Code restructure failed: missing block: B:20:0x004a, code lost:
        r4 = r0 + 1;
        r0 = r11.charAt(r0);
        r5 = decodeBase64Char(r0);
     */
    /* JADX WARNING: Code restructure failed: missing block: B:21:0x0054, code lost:
        if (r5 >= 0) goto L_0x0094;
     */
    /* JADX WARNING: Code restructure failed: missing block: B:22:0x0056, code lost:
        if (r5 == -2) goto L_0x005c;
     */
    /* JADX WARNING: Code restructure failed: missing block: B:23:0x0058, code lost:
        _reportInvalidBase64(r0, 2, null);
     */
    /* JADX WARNING: Code restructure failed: missing block: B:24:0x005c, code lost:
        if (r4 < r3) goto L_0x0061;
     */
    /* JADX WARNING: Code restructure failed: missing block: B:25:0x005e, code lost:
        _reportBase64EOF();
     */
    /* JADX WARNING: Code restructure failed: missing block: B:26:0x0061, code lost:
        r0 = r4 + 1;
        r4 = r11.charAt(r4);
     */
    /* JADX WARNING: Code restructure failed: missing block: B:27:0x006b, code lost:
        if (usesPaddingChar(r4) != false) goto L_0x008d;
     */
    /* JADX WARNING: Code restructure failed: missing block: B:28:0x006d, code lost:
        _reportInvalidBase64(r4, 3, "expected padding character '" + getPaddingChar() + "'");
     */
    /* JADX WARNING: Code restructure failed: missing block: B:29:0x008d, code lost:
        r12.append(r1 >> 4);
     */
    /* JADX WARNING: Code restructure failed: missing block: B:30:0x0094, code lost:
        r1 = (r1 << 6) | r5;
     */
    /* JADX WARNING: Code restructure failed: missing block: B:31:0x0098, code lost:
        if (r4 < r3) goto L_0x00aa;
     */
    /* JADX WARNING: Code restructure failed: missing block: B:33:0x009e, code lost:
        if (usesPadding() != false) goto L_0x00a7;
     */
    /* JADX WARNING: Code restructure failed: missing block: B:34:0x00a0, code lost:
        r12.appendTwoBytes(r1 >> 2);
     */
    /* JADX WARNING: Code restructure failed: missing block: B:35:0x00a7, code lost:
        _reportBase64EOF();
     */
    /* JADX WARNING: Code restructure failed: missing block: B:36:0x00aa, code lost:
        r0 = r4 + 1;
        r4 = r11.charAt(r4);
        r5 = decodeBase64Char(r4);
     */
    /* JADX WARNING: Code restructure failed: missing block: B:37:0x00b4, code lost:
        if (r5 >= 0) goto L_0x00c2;
     */
    /* JADX WARNING: Code restructure failed: missing block: B:38:0x00b6, code lost:
        if (r5 == -2) goto L_0x00bb;
     */
    /* JADX WARNING: Code restructure failed: missing block: B:39:0x00b8, code lost:
        _reportInvalidBase64(r4, 3, null);
     */
    /* JADX WARNING: Code restructure failed: missing block: B:40:0x00bb, code lost:
        r12.appendTwoBytes(r1 >> 2);
     */
    /* JADX WARNING: Code restructure failed: missing block: B:41:0x00c2, code lost:
        r12.appendThreeBytes((r1 << 6) | r5);
     */
    /* JADX WARNING: Code restructure failed: missing block: B:52:?, code lost:
        return;
     */
    /* JADX WARNING: Code restructure failed: missing block: B:53:?, code lost:
        return;
     */
    /* JADX WARNING: Code restructure failed: missing block: B:6:0x0018, code lost:
        r4 = decodeBase64Char(r0);
     */
    /* JADX WARNING: Code restructure failed: missing block: B:7:0x001c, code lost:
        if (r4 >= 0) goto L_0x0021;
     */
    /* JADX WARNING: Code restructure failed: missing block: B:8:0x001e, code lost:
        _reportInvalidBase64(r0, 0, null);
     */
    /* JADX WARNING: Code restructure failed: missing block: B:9:0x0021, code lost:
        if (r1 < r3) goto L_0x0026;
     */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    public void decode(java.lang.String r11, com.fasterxml.jackson.core.util.ByteArrayBuilder r12) throws java.lang.IllegalArgumentException {
        /*
            r10 = this;
            r9 = 3
            r2 = 0
            r8 = -2
            r7 = 0
            int r3 = r11.length()
            r0 = r2
        L_0x0009:
            if (r0 >= r3) goto L_0x0013
        L_0x000b:
            int r1 = r0 + 1
            char r0 = r11.charAt(r0)
            if (r1 < r3) goto L_0x0014
        L_0x0013:
            return
        L_0x0014:
            r4 = 32
            if (r0 <= r4) goto L_0x00ca
            int r4 = r10.decodeBase64Char(r0)
            if (r4 >= 0) goto L_0x0021
            r10._reportInvalidBase64(r0, r2, r7)
        L_0x0021:
            if (r1 < r3) goto L_0x0026
            r10._reportBase64EOF()
        L_0x0026:
            int r0 = r1 + 1
            char r1 = r11.charAt(r1)
            int r5 = r10.decodeBase64Char(r1)
            if (r5 >= 0) goto L_0x0036
            r6 = 1
            r10._reportInvalidBase64(r1, r6, r7)
        L_0x0036:
            int r1 = r4 << 6
            r1 = r1 | r5
            if (r0 < r3) goto L_0x004a
            boolean r4 = r10.usesPadding()
            if (r4 != 0) goto L_0x0047
            int r0 = r1 >> 4
            r12.append(r0)
            goto L_0x0013
        L_0x0047:
            r10._reportBase64EOF()
        L_0x004a:
            int r4 = r0 + 1
            char r0 = r11.charAt(r0)
            int r5 = r10.decodeBase64Char(r0)
            if (r5 >= 0) goto L_0x0094
            if (r5 == r8) goto L_0x005c
            r5 = 2
            r10._reportInvalidBase64(r0, r5, r7)
        L_0x005c:
            if (r4 < r3) goto L_0x0061
            r10._reportBase64EOF()
        L_0x0061:
            int r0 = r4 + 1
            char r4 = r11.charAt(r4)
            boolean r5 = r10.usesPaddingChar(r4)
            if (r5 != 0) goto L_0x008d
            java.lang.StringBuilder r5 = new java.lang.StringBuilder
            r5.<init>()
            java.lang.String r6 = "expected padding character '"
            java.lang.StringBuilder r5 = r5.append(r6)
            char r6 = r10.getPaddingChar()
            java.lang.StringBuilder r5 = r5.append(r6)
            java.lang.String r6 = "'"
            java.lang.StringBuilder r5 = r5.append(r6)
            java.lang.String r5 = r5.toString()
            r10._reportInvalidBase64(r4, r9, r5)
        L_0x008d:
            int r1 = r1 >> 4
            r12.append(r1)
            goto L_0x0009
        L_0x0094:
            int r0 = r1 << 6
            r1 = r0 | r5
            if (r4 < r3) goto L_0x00aa
            boolean r0 = r10.usesPadding()
            if (r0 != 0) goto L_0x00a7
            int r0 = r1 >> 2
            r12.appendTwoBytes(r0)
            goto L_0x0013
        L_0x00a7:
            r10._reportBase64EOF()
        L_0x00aa:
            int r0 = r4 + 1
            char r4 = r11.charAt(r4)
            int r5 = r10.decodeBase64Char(r4)
            if (r5 >= 0) goto L_0x00c2
            if (r5 == r8) goto L_0x00bb
            r10._reportInvalidBase64(r4, r9, r7)
        L_0x00bb:
            int r1 = r1 >> 2
            r12.appendTwoBytes(r1)
            goto L_0x0009
        L_0x00c2:
            int r1 = r1 << 6
            r1 = r1 | r5
            r12.appendThreeBytes(r1)
            goto L_0x0009
        L_0x00ca:
            r0 = r1
            goto L_0x000b
        */
        throw new UnsupportedOperationException("Method not decompiled: com.fasterxml.jackson.core.Base64Variant.decode(java.lang.String, com.fasterxml.jackson.core.util.ByteArrayBuilder):void");
    }

    public String toString() {
        return this._name;
    }

    public boolean equals(Object obj) {
        return obj == this;
    }

    public int hashCode() {
        return this._name.hashCode();
    }

    /* access modifiers changed from: protected */
    public void _reportInvalidBase64(char c, int i, String str) throws IllegalArgumentException {
        String str2;
        if (c <= ' ') {
            str2 = "Illegal white space character (code 0x" + Integer.toHexString(c) + ") as character #" + (i + 1) + " of 4-char base64 unit: can only used between units";
        } else if (usesPaddingChar(c)) {
            str2 = "Unexpected padding character ('" + getPaddingChar() + "') as character #" + (i + 1) + " of 4-char base64 unit: padding only legal as 3rd or 4th character";
        } else if (!Character.isDefined(c) || Character.isISOControl(c)) {
            str2 = "Illegal character (code 0x" + Integer.toHexString(c) + ") in base64 content";
        } else {
            str2 = "Illegal character '" + c + "' (code 0x" + Integer.toHexString(c) + ") in base64 content";
        }
        if (str != null) {
            str2 = str2 + ": " + str;
        }
        throw new IllegalArgumentException(str2);
    }

    /* access modifiers changed from: protected */
    public void _reportBase64EOF() throws IllegalArgumentException {
        throw new IllegalArgumentException("Unexpected end-of-String in base64 content");
    }
}
