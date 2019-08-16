package org.onepf.oms.appstore.googleUtils;

import org.jetbrains.annotations.NotNull;

public class Base64 {
    private static final byte[] ALPHABET = {65, 66, 67, 68, 69, 70, 71, 72, 73, 74, 75, 76, 77, 78, 79, 80, 81, 82, 83, 84, 85, 86, 87, 88, 89, 90, 97, 98, 99, 100, 101, 102, 103, 104, 105, 106, 107, 108, 109, 110, 111, 112, 113, 114, 115, 116, 117, 118, 119, 120, 121, 122, 48, 49, 50, 51, 52, 53, 54, 55, 56, 57, 43, 47};
    private static final byte[] DECODABET = {-9, -9, -9, -9, -9, -9, -9, -9, -9, WHITE_SPACE_ENC, WHITE_SPACE_ENC, -9, -9, WHITE_SPACE_ENC, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, WHITE_SPACE_ENC, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, 62, -9, -9, -9, 63, 52, 53, 54, 55, 56, 57, 58, 59, 60, EQUALS_SIGN, -9, -9, -9, EQUALS_SIGN_ENC, -9, -9, -9, 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, NEW_LINE, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, -9, -9, -9, -9, -9, -9, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 40, 41, 42, 43, 44, 45, 46, 47, 48, 49, 50, 51, -9, -9, -9, -9, -9};
    public static final boolean DECODE = false;
    public static final boolean ENCODE = true;
    private static final byte EQUALS_SIGN = 61;
    private static final byte EQUALS_SIGN_ENC = -1;
    private static final byte NEW_LINE = 10;
    private static final byte[] WEBSAFE_ALPHABET = {65, 66, 67, 68, 69, 70, 71, 72, 73, 74, 75, 76, 77, 78, 79, 80, 81, 82, 83, 84, 85, 86, 87, 88, 89, 90, 97, 98, 99, 100, 101, 102, 103, 104, 105, 106, 107, 108, 109, 110, 111, 112, 113, 114, 115, 116, 117, 118, 119, 120, 121, 122, 48, 49, 50, 51, 52, 53, 54, 55, 56, 57, 45, 95};
    private static final byte[] WEBSAFE_DECODABET = {-9, -9, -9, -9, -9, -9, -9, -9, -9, WHITE_SPACE_ENC, WHITE_SPACE_ENC, -9, -9, WHITE_SPACE_ENC, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, WHITE_SPACE_ENC, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, 62, -9, -9, 52, 53, 54, 55, 56, 57, 58, 59, 60, EQUALS_SIGN, -9, -9, -9, EQUALS_SIGN_ENC, -9, -9, -9, 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, NEW_LINE, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, -9, -9, -9, -9, 63, -9, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 40, 41, 42, 43, 44, 45, 46, 47, 48, 49, 50, 51, -9, -9, -9, -9, -9};
    private static final byte WHITE_SPACE_ENC = -5;

    private Base64() {
    }

    @NotNull
    public static byte[] decode(@NotNull String str) throws Base64DecoderException {
        byte[] bytes = str.getBytes();
        return decode(bytes, 0, bytes.length);
    }

    @NotNull
    public static byte[] decode(@NotNull byte[] bArr) throws Base64DecoderException {
        return decode(bArr, 0, bArr.length);
    }

    @NotNull
    public static byte[] decode(byte[] bArr, int i, int i2) throws Base64DecoderException {
        return decode(bArr, i, i2, DECODABET);
    }

    @NotNull
    public static byte[] decode(byte[] bArr, int i, int i2, byte[] bArr2) throws Base64DecoderException {
        int i3;
        byte[] bArr3 = new byte[(((i2 * 3) / 4) + 2)];
        byte[] bArr4 = new byte[4];
        int i4 = 0;
        int i5 = 0;
        int i6 = 0;
        while (true) {
            if (i4 >= i2) {
                break;
            }
            byte b = (byte) (bArr[i4 + i] & Byte.MAX_VALUE);
            byte b2 = bArr2[b];
            if (b2 >= -5) {
                if (b2 < -1) {
                    i3 = i6;
                } else if (b == 61) {
                    int i7 = i2 - i4;
                    byte b3 = (byte) (bArr[(i2 - 1) + i] & Byte.MAX_VALUE);
                    if (i6 == 0 || i6 == 1) {
                        throw new Base64DecoderException("invalid padding byte '=' at byte offset " + i4);
                    } else if ((i6 == 3 && i7 > 2) || (i6 == 4 && i7 > 1)) {
                        throw new Base64DecoderException("padding byte '=' falsely signals end of encoded value at offset " + i4);
                    } else if (b3 != 61 && b3 != 10) {
                        throw new Base64DecoderException("encoded value has invalid trailing byte");
                    }
                } else {
                    i3 = i6 + 1;
                    bArr4[i6] = (byte) b;
                    if (i3 == 4) {
                        i5 += decode4to3(bArr4, 0, bArr3, i5, bArr2);
                        i3 = 0;
                    }
                }
                i4++;
                i6 = i3;
            } else {
                throw new Base64DecoderException("Bad Base64 input character at " + i4 + ": " + bArr[i4 + i] + "(decimal)");
            }
        }
        if (i6 != 0) {
            if (i6 == 1) {
                throw new Base64DecoderException("single trailing character at offset " + (i2 - 1));
            }
            bArr4[i6] = (byte) 61;
            i5 += decode4to3(bArr4, 0, bArr3, i5, bArr2);
        }
        byte[] bArr5 = new byte[i5];
        System.arraycopy(bArr3, 0, bArr5, 0, i5);
        return bArr5;
    }

    private static int decode4to3(byte[] bArr, int i, byte[] bArr2, int i2, byte[] bArr3) {
        if (bArr[i + 2] == 61) {
            bArr2[i2] = (byte) ((byte) ((((bArr3[bArr[i]] << 24) >>> 6) | ((bArr3[bArr[i + 1]] << 24) >>> 12)) >>> 16));
            return 1;
        } else if (bArr[i + 3] == 61) {
            int i3 = ((bArr3[bArr[i]] << 24) >>> 6) | ((bArr3[bArr[i + 1]] << 24) >>> 12) | ((bArr3[bArr[i + 2]] << 24) >>> 18);
            bArr2[i2] = (byte) ((byte) (i3 >>> 16));
            bArr2[i2 + 1] = (byte) ((byte) (i3 >>> 8));
            return 2;
        } else {
            int i4 = ((bArr3[bArr[i]] << 24) >>> 6) | ((bArr3[bArr[i + 1]] << 24) >>> 12) | ((bArr3[bArr[i + 2]] << 24) >>> 18) | ((bArr3[bArr[i + 3]] << 24) >>> 24);
            bArr2[i2] = (byte) ((byte) (i4 >> 16));
            bArr2[i2 + 1] = (byte) ((byte) (i4 >> 8));
            bArr2[i2 + 2] = (byte) ((byte) i4);
            return 3;
        }
    }

    @NotNull
    public static byte[] decodeWebSafe(@NotNull String str) throws Base64DecoderException {
        byte[] bytes = str.getBytes();
        return decodeWebSafe(bytes, 0, bytes.length);
    }

    @NotNull
    public static byte[] decodeWebSafe(@NotNull byte[] bArr) throws Base64DecoderException {
        return decodeWebSafe(bArr, 0, bArr.length);
    }

    @NotNull
    public static byte[] decodeWebSafe(byte[] bArr, int i, int i2) throws Base64DecoderException {
        return decode(bArr, i, i2, WEBSAFE_DECODABET);
    }

    @NotNull
    public static String encode(@NotNull byte[] bArr) {
        return encode(bArr, 0, bArr.length, ALPHABET, true);
    }

    @NotNull
    public static String encode(byte[] bArr, int i, int i2, byte[] bArr2, boolean z) {
        byte[] encode = encode(bArr, i, i2, bArr2, Integer.MAX_VALUE);
        int length = encode.length;
        while (!z && length > 0 && encode[length - 1] == 61) {
            length--;
        }
        return new String(encode, 0, length);
    }

    @NotNull
    public static byte[] encode(byte[] bArr, int i, int i2, byte[] bArr2, int i3) {
        int i4 = ((i2 + 2) / 3) * 4;
        byte[] bArr3 = new byte[(i4 + (i4 / i3))];
        int i5 = 0;
        int i6 = 0;
        int i7 = 0;
        while (i5 < i2 - 2) {
            int i8 = ((bArr[i5 + i] << 24) >>> 8) | ((bArr[(i5 + 1) + i] << 24) >>> 16) | ((bArr[(i5 + 2) + i] << 24) >>> 24);
            bArr3[i7] = (byte) bArr2[i8 >>> 18];
            bArr3[i7 + 1] = (byte) bArr2[(i8 >>> 12) & 63];
            bArr3[i7 + 2] = (byte) bArr2[(i8 >>> 6) & 63];
            bArr3[i7 + 3] = (byte) bArr2[i8 & 63];
            int i9 = i6 + 4;
            if (i9 == i3) {
                bArr3[i7 + 4] = (byte) 10;
                i7++;
                i9 = 0;
            }
            i5 += 3;
            i7 += 4;
            i6 = i9;
        }
        if (i5 < i2) {
            encode3to4(bArr, i5 + i, i2 - i5, bArr3, i7, bArr2);
            if (i6 + 4 == i3) {
                bArr3[i7 + 4] = (byte) 10;
                int i10 = i7 + 1;
            }
        }
        return bArr3;
    }

    private static byte[] encode3to4(byte[] bArr, int i, int i2, byte[] bArr2, int i3, byte[] bArr3) {
        int i4 = 0;
        int i5 = i2 > 0 ? (bArr[i] << 24) >>> 8 : 0;
        int i6 = i2 > 1 ? (bArr[i + 1] << 24) >>> 16 : 0;
        if (i2 > 2) {
            i4 = (bArr[i + 2] << 24) >>> 24;
        }
        int i7 = i4 | i6 | i5;
        switch (i2) {
            case 1:
                bArr2[i3] = (byte) bArr3[i7 >>> 18];
                bArr2[i3 + 1] = (byte) bArr3[(i7 >>> 12) & 63];
                bArr2[i3 + 2] = (byte) 61;
                bArr2[i3 + 3] = (byte) 61;
                break;
            case 2:
                bArr2[i3] = (byte) bArr3[i7 >>> 18];
                bArr2[i3 + 1] = (byte) bArr3[(i7 >>> 12) & 63];
                bArr2[i3 + 2] = (byte) bArr3[(i7 >>> 6) & 63];
                bArr2[i3 + 3] = (byte) 61;
                break;
            case 3:
                bArr2[i3] = (byte) bArr3[i7 >>> 18];
                bArr2[i3 + 1] = (byte) bArr3[(i7 >>> 12) & 63];
                bArr2[i3 + 2] = (byte) bArr3[(i7 >>> 6) & 63];
                bArr2[i3 + 3] = (byte) bArr3[i7 & 63];
                break;
        }
        return bArr2;
    }

    @NotNull
    public static String encodeWebSafe(@NotNull byte[] bArr, boolean z) {
        return encode(bArr, 0, bArr.length, WEBSAFE_ALPHABET, z);
    }
}
