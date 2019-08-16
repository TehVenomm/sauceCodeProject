package com.fasterxml.jackson.core.p015io;

import com.fasterxml.jackson.core.util.BufferRecycler;
import com.fasterxml.jackson.core.util.ByteArrayBuilder;
import com.fasterxml.jackson.core.util.TextBuffer;
import com.github.droidfu.support.DisplaySupport;
import java.lang.ref.SoftReference;

/* renamed from: com.fasterxml.jackson.core.io.JsonStringEncoder */
public final class JsonStringEncoder {

    /* renamed from: HB */
    private static final byte[] f408HB = CharTypes.copyHexBytes();

    /* renamed from: HC */
    private static final char[] f409HC = CharTypes.copyHexChars();
    private static final int SURR1_FIRST = 55296;
    private static final int SURR1_LAST = 56319;
    private static final int SURR2_FIRST = 56320;
    private static final int SURR2_LAST = 57343;
    protected static final ThreadLocal<SoftReference<JsonStringEncoder>> _threadEncoder = new ThreadLocal<>();
    protected ByteArrayBuilder _bytes;
    protected final char[] _qbuf = new char[6];
    protected TextBuffer _text;

    public JsonStringEncoder() {
        this._qbuf[0] = '\\';
        this._qbuf[2] = '0';
        this._qbuf[3] = '0';
    }

    public static JsonStringEncoder getInstance() {
        SoftReference softReference = (SoftReference) _threadEncoder.get();
        JsonStringEncoder jsonStringEncoder = softReference == null ? null : (JsonStringEncoder) softReference.get();
        if (jsonStringEncoder != null) {
            return jsonStringEncoder;
        }
        JsonStringEncoder jsonStringEncoder2 = new JsonStringEncoder();
        _threadEncoder.set(new SoftReference(jsonStringEncoder2));
        return jsonStringEncoder2;
    }

    /* JADX WARNING: Code restructure failed: missing block: B:10:0x0030, code lost:
        if (r9 >= 0) goto L_0x006b;
     */
    /* JADX WARNING: Code restructure failed: missing block: B:11:0x0032, code lost:
        r2 = _appendNumeric(r2, r11._qbuf);
     */
    /* JADX WARNING: Code restructure failed: missing block: B:13:0x003b, code lost:
        if ((r1 + r2) <= r3.length) goto L_0x0072;
     */
    /* JADX WARNING: Code restructure failed: missing block: B:14:0x003d, code lost:
        r9 = r3.length - r1;
     */
    /* JADX WARNING: Code restructure failed: missing block: B:15:0x003f, code lost:
        if (r9 <= 0) goto L_0x0046;
     */
    /* JADX WARNING: Code restructure failed: missing block: B:16:0x0041, code lost:
        java.lang.System.arraycopy(r11._qbuf, 0, r3, r1, r9);
     */
    /* JADX WARNING: Code restructure failed: missing block: B:17:0x0046, code lost:
        r3 = r0.finishCurrentSegment();
        r1 = r2 - r9;
        java.lang.System.arraycopy(r11._qbuf, r9, r3, 0, r1);
     */
    /* JADX WARNING: Code restructure failed: missing block: B:18:0x0051, code lost:
        r2 = r4;
     */
    /* JADX WARNING: Code restructure failed: missing block: B:26:0x006b, code lost:
        r2 = _appendNamed(r9, r11._qbuf);
     */
    /* JADX WARNING: Code restructure failed: missing block: B:27:0x0072, code lost:
        java.lang.System.arraycopy(r11._qbuf, 0, r3, r1, r2);
        r1 = r1 + r2;
     */
    /* JADX WARNING: Code restructure failed: missing block: B:9:0x0028, code lost:
        r4 = r2 + 1;
        r2 = r12.charAt(r2);
        r9 = r6[r2];
     */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    public char[] quoteAsString(java.lang.String r12) {
        /*
            r11 = this;
            r5 = 0
            com.fasterxml.jackson.core.util.TextBuffer r0 = r11._text
            if (r0 != 0) goto L_0x000d
            com.fasterxml.jackson.core.util.TextBuffer r0 = new com.fasterxml.jackson.core.util.TextBuffer
            r1 = 0
            r0.<init>(r1)
            r11._text = r0
        L_0x000d:
            char[] r3 = r0.emptyAndGetCurrentSegment()
            int[] r6 = com.fasterxml.jackson.core.p015io.CharTypes.get7BitOutputEscapes()
            int r7 = r6.length
            int r8 = r12.length()
            r1 = r5
            r2 = r5
        L_0x001c:
            if (r2 >= r8) goto L_0x0063
        L_0x001e:
            char r9 = r12.charAt(r2)
            if (r9 >= r7) goto L_0x0053
            r4 = r6[r9]
            if (r4 == 0) goto L_0x0053
            int r4 = r2 + 1
            char r2 = r12.charAt(r2)
            r9 = r6[r2]
            if (r9 >= 0) goto L_0x006b
            char[] r9 = r11._qbuf
            int r2 = r11._appendNumeric(r2, r9)
        L_0x0038:
            int r9 = r1 + r2
            int r10 = r3.length
            if (r9 <= r10) goto L_0x0072
            int r9 = r3.length
            int r9 = r9 - r1
            if (r9 <= 0) goto L_0x0046
            char[] r10 = r11._qbuf
            java.lang.System.arraycopy(r10, r5, r3, r1, r9)
        L_0x0046:
            char[] r3 = r0.finishCurrentSegment()
            int r1 = r2 - r9
            char[] r2 = r11._qbuf
            java.lang.System.arraycopy(r2, r9, r3, r5, r1)
        L_0x0051:
            r2 = r4
            goto L_0x001c
        L_0x0053:
            int r4 = r3.length
            if (r1 < r4) goto L_0x0079
            char[] r3 = r0.finishCurrentSegment()
            r4 = r5
        L_0x005b:
            int r1 = r4 + 1
            r3[r4] = r9
            int r2 = r2 + 1
            if (r2 < r8) goto L_0x001e
        L_0x0063:
            r0.setCurrentLength(r1)
            char[] r0 = r0.contentsAsArray()
            return r0
        L_0x006b:
            char[] r2 = r11._qbuf
            int r2 = r11._appendNamed(r9, r2)
            goto L_0x0038
        L_0x0072:
            char[] r9 = r11._qbuf
            java.lang.System.arraycopy(r9, r5, r3, r1, r2)
            int r1 = r1 + r2
            goto L_0x0051
        L_0x0079:
            r4 = r1
            goto L_0x005b
        */
        throw new UnsupportedOperationException("Method not decompiled: com.fasterxml.jackson.core.p015io.JsonStringEncoder.quoteAsString(java.lang.String):char[]");
    }

    public byte[] quoteAsUTF8(String str) {
        int i;
        int i2;
        char c;
        int i3;
        int i4;
        int i5;
        ByteArrayBuilder byteArrayBuilder = this._bytes;
        if (byteArrayBuilder == null) {
            byteArrayBuilder = new ByteArrayBuilder((BufferRecycler) null);
            this._bytes = byteArrayBuilder;
        }
        int length = str.length();
        byte[] resetAndGetFirstSegment = byteArrayBuilder.resetAndGetFirstSegment();
        int i6 = 0;
        int i7 = 0;
        loop0:
        while (i7 < length) {
            int[] iArr = CharTypes.get7BitOutputEscapes();
            while (true) {
                char charAt = str.charAt(i7);
                if (charAt <= 127 && iArr[charAt] == 0) {
                    if (i6 >= resetAndGetFirstSegment.length) {
                        resetAndGetFirstSegment = byteArrayBuilder.finishCurrentSegment();
                        i5 = 0;
                    } else {
                        i5 = i6;
                    }
                    i6 = i5 + 1;
                    resetAndGetFirstSegment[i5] = (byte) charAt;
                    i7++;
                    if (i7 >= length) {
                        break loop0;
                    }
                }
            }
            if (i6 >= resetAndGetFirstSegment.length) {
                resetAndGetFirstSegment = byteArrayBuilder.finishCurrentSegment();
                i6 = 0;
            }
            int i8 = i7 + 1;
            char charAt2 = str.charAt(i7);
            if (charAt2 <= 127) {
                i6 = _appendByte(charAt2, iArr[charAt2], byteArrayBuilder, i6);
                resetAndGetFirstSegment = byteArrayBuilder.getCurrentSegment();
                i7 = i8;
            } else {
                if (charAt2 <= 2047) {
                    i2 = i6 + 1;
                    resetAndGetFirstSegment[i6] = (byte) ((charAt2 >> 6) | 192);
                    c = (charAt2 & '?') | 128;
                } else if (charAt2 < 55296 || charAt2 > 57343) {
                    int i9 = i6 + 1;
                    resetAndGetFirstSegment[i6] = (byte) ((charAt2 >> 12) | 224);
                    if (i9 >= resetAndGetFirstSegment.length) {
                        resetAndGetFirstSegment = byteArrayBuilder.finishCurrentSegment();
                        i = 0;
                    } else {
                        i = i9;
                    }
                    i2 = i + 1;
                    resetAndGetFirstSegment[i] = (byte) (((charAt2 >> 6) & 63) | 128);
                    c = (charAt2 & '?') | 128;
                } else {
                    if (charAt2 > 56319) {
                        _illegal(charAt2);
                    }
                    if (i8 >= length) {
                        _illegal(charAt2);
                    }
                    int i10 = i8 + 1;
                    int _convert = _convert(charAt2, str.charAt(i8));
                    if (_convert > 1114111) {
                        _illegal(_convert);
                    }
                    int i11 = i6 + 1;
                    resetAndGetFirstSegment[i6] = (byte) ((_convert >> 18) | DisplaySupport.SCREEN_DENSITY_HIGH);
                    if (i11 >= resetAndGetFirstSegment.length) {
                        resetAndGetFirstSegment = byteArrayBuilder.finishCurrentSegment();
                        i3 = 0;
                    } else {
                        i3 = i11;
                    }
                    int i12 = i3 + 1;
                    resetAndGetFirstSegment[i3] = (byte) (((_convert >> 12) & 63) | 128);
                    if (i12 >= resetAndGetFirstSegment.length) {
                        resetAndGetFirstSegment = byteArrayBuilder.finishCurrentSegment();
                        i4 = 0;
                    } else {
                        i4 = i12;
                    }
                    i2 = i4 + 1;
                    resetAndGetFirstSegment[i4] = (byte) (((_convert >> 6) & 63) | 128);
                    c = (_convert & '?') | 128;
                    i8 = i10;
                }
                if (i2 >= resetAndGetFirstSegment.length) {
                    resetAndGetFirstSegment = byteArrayBuilder.finishCurrentSegment();
                    i2 = 0;
                }
                int i13 = i2 + 1;
                resetAndGetFirstSegment[i2] = (byte) c;
                i6 = i13;
                i7 = i8;
            }
        }
        return this._bytes.completeAndCoalesce(i6);
    }

    public byte[] encodeAsUTF8(String str) {
        int i;
        int i2;
        int i3;
        int i4;
        int i5;
        ByteArrayBuilder byteArrayBuilder = this._bytes;
        if (byteArrayBuilder == null) {
            byteArrayBuilder = new ByteArrayBuilder((BufferRecycler) null);
            this._bytes = byteArrayBuilder;
        }
        int length = str.length();
        byte[] resetAndGetFirstSegment = byteArrayBuilder.resetAndGetFirstSegment();
        int length2 = resetAndGetFirstSegment.length;
        int i6 = 0;
        int i7 = 0;
        loop0:
        while (true) {
            if (i7 >= length) {
                i = i6;
                break;
            }
            int i8 = i7 + 1;
            char charAt = str.charAt(i7);
            while (charAt <= 127) {
                if (i6 >= length2) {
                    resetAndGetFirstSegment = byteArrayBuilder.finishCurrentSegment();
                    length2 = resetAndGetFirstSegment.length;
                    i6 = 0;
                }
                int i9 = i6 + 1;
                resetAndGetFirstSegment[i6] = (byte) charAt;
                if (i8 >= length) {
                    i = i9;
                    break loop0;
                }
                int i10 = i8 + 1;
                charAt = str.charAt(i8);
                i6 = i9;
                i8 = i10;
            }
            if (i6 >= length2) {
                resetAndGetFirstSegment = byteArrayBuilder.finishCurrentSegment();
                length2 = resetAndGetFirstSegment.length;
                i2 = 0;
            } else {
                i2 = i6;
            }
            if (charAt < 2048) {
                i4 = i2 + 1;
                resetAndGetFirstSegment[i2] = (byte) ((charAt >> 6) | 192);
                i3 = charAt;
            } else if (charAt < 55296 || charAt > 57343) {
                int i11 = i2 + 1;
                resetAndGetFirstSegment[i2] = (byte) ((charAt >> 12) | 224);
                if (i11 >= length2) {
                    resetAndGetFirstSegment = byteArrayBuilder.finishCurrentSegment();
                    length2 = resetAndGetFirstSegment.length;
                    i11 = 0;
                }
                int i12 = i11 + 1;
                resetAndGetFirstSegment[i11] = (byte) (((charAt >> 6) & 63) | 128);
                i3 = charAt;
                i4 = i12;
            } else {
                if (charAt > 56319) {
                    _illegal(charAt);
                }
                if (i8 >= length) {
                    _illegal(charAt);
                }
                int i13 = i8 + 1;
                int _convert = _convert(charAt, str.charAt(i8));
                if (_convert > 1114111) {
                    _illegal(_convert);
                }
                int i14 = i2 + 1;
                resetAndGetFirstSegment[i2] = (byte) ((_convert >> 18) | DisplaySupport.SCREEN_DENSITY_HIGH);
                if (i14 >= length2) {
                    resetAndGetFirstSegment = byteArrayBuilder.finishCurrentSegment();
                    length2 = resetAndGetFirstSegment.length;
                    i14 = 0;
                }
                int i15 = i14 + 1;
                resetAndGetFirstSegment[i14] = (byte) (((_convert >> 12) & 63) | 128);
                if (i15 >= length2) {
                    resetAndGetFirstSegment = byteArrayBuilder.finishCurrentSegment();
                    length2 = resetAndGetFirstSegment.length;
                    i5 = 0;
                } else {
                    i5 = i15;
                }
                int i16 = i5 + 1;
                resetAndGetFirstSegment[i5] = (byte) (((_convert >> 6) & 63) | 128);
                i3 = _convert;
                i4 = i16;
                i8 = i13;
            }
            if (i4 >= length2) {
                resetAndGetFirstSegment = byteArrayBuilder.finishCurrentSegment();
                length2 = resetAndGetFirstSegment.length;
                i4 = 0;
            }
            int i17 = i4 + 1;
            resetAndGetFirstSegment[i4] = (byte) ((i3 & 63) | 128);
            i6 = i17;
            i7 = i8;
        }
        return this._bytes.completeAndCoalesce(i);
    }

    private int _appendNumeric(int i, char[] cArr) {
        cArr[1] = 'u';
        cArr[4] = f409HC[i >> 4];
        cArr[5] = f409HC[i & 15];
        return 6;
    }

    private int _appendNamed(int i, char[] cArr) {
        cArr[1] = (char) i;
        return 2;
    }

    private int _appendByte(int i, int i2, ByteArrayBuilder byteArrayBuilder, int i3) {
        byteArrayBuilder.setCurrentSegmentLength(i3);
        byteArrayBuilder.append(92);
        if (i2 < 0) {
            byteArrayBuilder.append(117);
            if (i > 255) {
                int i4 = i >> 8;
                byteArrayBuilder.append(f408HB[i4 >> 4]);
                byteArrayBuilder.append(f408HB[i4 & 15]);
                i &= 255;
            } else {
                byteArrayBuilder.append(48);
                byteArrayBuilder.append(48);
            }
            byteArrayBuilder.append(f408HB[i >> 4]);
            byteArrayBuilder.append(f408HB[i & 15]);
        } else {
            byteArrayBuilder.append((byte) i2);
        }
        return byteArrayBuilder.getCurrentSegmentLength();
    }

    private static int _convert(int i, int i2) {
        if (i2 >= 56320 && i2 <= 57343) {
            return 65536 + ((i - 55296) << 10) + (i2 - 56320);
        }
        throw new IllegalArgumentException("Broken surrogate pair: first char 0x" + Integer.toHexString(i) + ", second 0x" + Integer.toHexString(i2) + "; illegal combination");
    }

    private static void _illegal(int i) {
        throw new IllegalArgumentException(UTF8Writer.illegalSurrogateDesc(i));
    }
}
