package org.apache.commons.lang3;

import java.util.UUID;

public class Conversion {
    static final /* synthetic */ boolean $assertionsDisabled = (!Conversion.class.desiredAssertionStatus());
    private static final boolean[] FFFF = {false, false, false, false};
    private static final boolean[] FFFT = {false, false, false, true};
    private static final boolean[] FFTF = {false, false, true, false};
    private static final boolean[] FFTT = {false, false, true, true};
    private static final boolean[] FTFF = {false, true, false, false};
    private static final boolean[] FTFT = {false, true, false, true};
    private static final boolean[] FTTF = {false, true, true, false};
    private static final boolean[] FTTT = {false, true, true, true};
    private static final boolean[] TFFF = {true, false, false, false};
    private static final boolean[] TFFT = {true, false, false, true};
    private static final boolean[] TFTF = {true, false, true, false};
    private static final boolean[] TFTT = {true, false, true, true};
    private static final boolean[] TTFF = {true, true, false, false};
    private static final boolean[] TTFT = {true, true, false, true};
    private static final boolean[] TTTF = {true, true, true, false};
    private static final boolean[] TTTT = {true, true, true, true};

    public static int hexDigitToInt(char c) {
        int digit = Character.digit(c, 16);
        if (digit >= 0) {
            return digit;
        }
        throw new IllegalArgumentException("Cannot interpret '" + c + "' as a hexadecimal digit");
    }

    public static int hexDigitMsb0ToInt(char c) {
        switch (c) {
            case '0':
                return 0;
            case '1':
                return 8;
            case '2':
                return 4;
            case '3':
                return 12;
            case '4':
                return 2;
            case '5':
                return 10;
            case '6':
                return 6;
            case '7':
                return 14;
            case '8':
                return 1;
            case '9':
                return 9;
            case 'A':
            case 'a':
                return 5;
            case 'B':
            case 'b':
                return 13;
            case 'C':
            case 'c':
                return 3;
            case 'D':
            case 'd':
                return 11;
            case 'E':
            case 'e':
                return 7;
            case 'F':
            case 'f':
                return 15;
            default:
                throw new IllegalArgumentException("Cannot interpret '" + c + "' as a hexadecimal digit");
        }
    }

    public static boolean[] hexDigitToBinary(char c) {
        switch (c) {
            case '0':
                return (boolean[]) FFFF.clone();
            case '1':
                return (boolean[]) TFFF.clone();
            case '2':
                return (boolean[]) FTFF.clone();
            case '3':
                return (boolean[]) TTFF.clone();
            case '4':
                return (boolean[]) FFTF.clone();
            case '5':
                return (boolean[]) TFTF.clone();
            case '6':
                return (boolean[]) FTTF.clone();
            case '7':
                return (boolean[]) TTTF.clone();
            case '8':
                return (boolean[]) FFFT.clone();
            case '9':
                return (boolean[]) TFFT.clone();
            case 'A':
            case 'a':
                return (boolean[]) FTFT.clone();
            case 'B':
            case 'b':
                return (boolean[]) TTFT.clone();
            case 'C':
            case 'c':
                return (boolean[]) FFTT.clone();
            case 'D':
            case 'd':
                return (boolean[]) TFTT.clone();
            case 'E':
            case 'e':
                return (boolean[]) FTTT.clone();
            case 'F':
            case 'f':
                return (boolean[]) TTTT.clone();
            default:
                throw new IllegalArgumentException("Cannot interpret '" + c + "' as a hexadecimal digit");
        }
    }

    public static boolean[] hexDigitMsb0ToBinary(char c) {
        switch (c) {
            case '0':
                return (boolean[]) FFFF.clone();
            case '1':
                return (boolean[]) FFFT.clone();
            case '2':
                return (boolean[]) FFTF.clone();
            case '3':
                return (boolean[]) FFTT.clone();
            case '4':
                return (boolean[]) FTFF.clone();
            case '5':
                return (boolean[]) FTFT.clone();
            case '6':
                return (boolean[]) FTTF.clone();
            case '7':
                return (boolean[]) FTTT.clone();
            case '8':
                return (boolean[]) TFFF.clone();
            case '9':
                return (boolean[]) TFFT.clone();
            case 'A':
            case 'a':
                return (boolean[]) TFTF.clone();
            case 'B':
            case 'b':
                return (boolean[]) TFTT.clone();
            case 'C':
            case 'c':
                return (boolean[]) TTFF.clone();
            case 'D':
            case 'd':
                return (boolean[]) TTFT.clone();
            case 'E':
            case 'e':
                return (boolean[]) TTTF.clone();
            case 'F':
            case 'f':
                return (boolean[]) TTTT.clone();
            default:
                throw new IllegalArgumentException("Cannot interpret '" + c + "' as a hexadecimal digit");
        }
    }

    public static char binaryToHexDigit(boolean[] zArr) {
        return binaryToHexDigit(zArr, 0);
    }

    public static char binaryToHexDigit(boolean[] zArr, int i) {
        if (zArr.length == 0) {
            throw new IllegalArgumentException("Cannot convert an empty array.");
        } else if (zArr.length <= i + 3 || !zArr[i + 3]) {
            return (zArr.length <= i + 2 || !zArr[i + 2]) ? (zArr.length <= i + 1 || !zArr[i + 1]) ? zArr[i] ? '1' : '0' : zArr[i] ? '3' : '2' : (zArr.length <= i + 1 || !zArr[i + 1]) ? zArr[i] ? '5' : '4' : zArr[i] ? '7' : '6';
        } else {
            if (zArr.length <= i + 2 || !zArr[i + 2]) {
                return (zArr.length <= i + 1 || !zArr[i + 1]) ? zArr[i] ? '9' : '8' : zArr[i] ? 'b' : 'a';
            }
            if (zArr.length <= i + 1 || !zArr[i + 1]) {
                return zArr[i] ? 'd' : 'c';
            }
            if (zArr[i]) {
                return 'f';
            }
            return 'e';
        }
    }

    public static char binaryToHexDigitMsb0_4bits(boolean[] zArr) {
        return binaryToHexDigitMsb0_4bits(zArr, 0);
    }

    public static char binaryToHexDigitMsb0_4bits(boolean[] zArr, int i) {
        if (zArr.length > 8) {
            throw new IllegalArgumentException("src.length>8: src.length=" + zArr.length);
        } else if (zArr.length - i < 4) {
            throw new IllegalArgumentException("src.length-srcPos<4: src.length=" + zArr.length + ", srcPos=" + i);
        } else if (!zArr[i + 3]) {
            return zArr[i + 2] ? zArr[i + 1] ? zArr[i] ? 'e' : '6' : zArr[i] ? 'a' : '2' : zArr[i + 1] ? zArr[i] ? 'c' : '4' : zArr[i] ? '8' : '0';
        } else {
            if (!zArr[i + 2]) {
                return zArr[i + 1] ? zArr[i] ? 'd' : '5' : zArr[i] ? '9' : '1';
            }
            if (!zArr[i + 1]) {
                return zArr[i] ? 'b' : '3';
            }
            if (zArr[i]) {
                return 'f';
            }
            return '7';
        }
    }

    public static char binaryBeMsb0ToHexDigit(boolean[] zArr) {
        return binaryBeMsb0ToHexDigit(zArr, 0);
    }

    public static char binaryBeMsb0ToHexDigit(boolean[] zArr, int i) {
        if (zArr.length == 0) {
            throw new IllegalArgumentException("Cannot convert an empty array.");
        }
        int length = (zArr.length - 1) - i;
        int min = Math.min(4, length + 1);
        boolean[] zArr2 = new boolean[4];
        System.arraycopy(zArr, (length + 1) - min, zArr2, 4 - min, min);
        if (!zArr2[0]) {
            return (zArr2.length <= 1 || !zArr2[1]) ? (zArr2.length <= 2 || !zArr2[2]) ? (zArr2.length <= 3 || !zArr2[3]) ? '0' : '1' : (zArr2.length <= 3 || !zArr2[3]) ? '2' : '3' : (zArr2.length <= 2 || !zArr2[2]) ? (zArr2.length <= 3 || !zArr2[3]) ? '4' : '5' : (zArr2.length <= 3 || !zArr2[3]) ? '6' : '7';
        }
        if (zArr2.length <= 1 || !zArr2[1]) {
            return (zArr2.length <= 2 || !zArr2[2]) ? (zArr2.length <= 3 || !zArr2[3]) ? '8' : '9' : (zArr2.length <= 3 || !zArr2[3]) ? 'a' : 'b';
        }
        if (zArr2.length <= 2 || !zArr2[2]) {
            return (zArr2.length <= 3 || !zArr2[3]) ? 'c' : 'd';
        }
        if (zArr2.length <= 3 || !zArr2[3]) {
            return 'e';
        }
        return 'f';
    }

    public static char intToHexDigit(int i) {
        char forDigit = Character.forDigit(i, 16);
        if (forDigit != 0) {
            return forDigit;
        }
        throw new IllegalArgumentException("nibble value not between 0 and 15: " + i);
    }

    public static char intToHexDigitMsb0(int i) {
        switch (i) {
            case 0:
                return '0';
            case 1:
                return '8';
            case 2:
                return '4';
            case 3:
                return 'c';
            case 4:
                return '2';
            case 5:
                return 'a';
            case 6:
                return '6';
            case 7:
                return 'e';
            case 8:
                return '1';
            case 9:
                return '9';
            case 10:
                return '5';
            case 11:
                return 'd';
            case 12:
                return '3';
            case 13:
                return 'b';
            case 14:
                return '7';
            case 15:
                return 'f';
            default:
                throw new IllegalArgumentException("nibble value not between 0 and 15: " + i);
        }
    }

    public static long intArrayToLong(int[] iArr, int i, long j, int i2, int i3) {
        if (!((iArr.length == 0 && i == 0) || i3 == 0)) {
            if (((i3 - 1) * 32) + i2 >= 64) {
                throw new IllegalArgumentException("(nInts-1)*32+dstPos is greather or equal to than 64");
            }
            for (int i4 = 0; i4 < i3; i4++) {
                int i5 = (i4 * 32) + i2;
                j = (((4294967295 << i5) ^ -1) & j) | ((((long) iArr[i4 + i]) & 4294967295L) << i5);
            }
        }
        return j;
    }

    public static long shortArrayToLong(short[] sArr, int i, long j, int i2, int i3) {
        if (!((sArr.length == 0 && i == 0) || i3 == 0)) {
            if (((i3 - 1) * 16) + i2 >= 64) {
                throw new IllegalArgumentException("(nShorts-1)*16+dstPos is greather or equal to than 64");
            }
            for (int i4 = 0; i4 < i3; i4++) {
                int i5 = (i4 * 16) + i2;
                j = (((65535 << i5) ^ -1) & j) | ((((long) sArr[i4 + i]) & 65535) << i5);
            }
        }
        return j;
    }

    public static int shortArrayToInt(short[] sArr, int i, int i2, int i3, int i4) {
        if (!((sArr.length == 0 && i == 0) || i4 == 0)) {
            if (((i4 - 1) * 16) + i3 >= 32) {
                throw new IllegalArgumentException("(nShorts-1)*16+dstPos is greather or equal to than 32");
            }
            for (int i5 = 0; i5 < i4; i5++) {
                int i6 = (i5 * 16) + i3;
                i2 = (((65535 << i6) ^ -1) & i2) | ((sArr[i5 + i] & 65535) << i6);
            }
        }
        return i2;
    }

    public static long byteArrayToLong(byte[] bArr, int i, long j, int i2, int i3) {
        if (!((bArr.length == 0 && i == 0) || i3 == 0)) {
            if (((i3 - 1) * 8) + i2 >= 64) {
                throw new IllegalArgumentException("(nBytes-1)*8+dstPos is greather or equal to than 64");
            }
            for (int i4 = 0; i4 < i3; i4++) {
                int i5 = (i4 * 8) + i2;
                j = (((255 << i5) ^ -1) & j) | ((((long) bArr[i4 + i]) & 255) << i5);
            }
        }
        return j;
    }

    public static int byteArrayToInt(byte[] bArr, int i, int i2, int i3, int i4) {
        if (!((bArr.length == 0 && i == 0) || i4 == 0)) {
            if (((i4 - 1) * 8) + i3 >= 32) {
                throw new IllegalArgumentException("(nBytes-1)*8+dstPos is greather or equal to than 32");
            }
            for (int i5 = 0; i5 < i4; i5++) {
                int i6 = (i5 * 8) + i3;
                i2 = (((255 << i6) ^ -1) & i2) | ((bArr[i5 + i] & 255) << i6);
            }
        }
        return i2;
    }

    public static short byteArrayToShort(byte[] bArr, int i, short s, int i2, int i3) {
        if (!((bArr.length == 0 && i == 0) || i3 == 0)) {
            if (((i3 - 1) * 8) + i2 >= 16) {
                throw new IllegalArgumentException("(nBytes-1)*8+dstPos is greather or equal to than 16");
            }
            for (int i4 = 0; i4 < i3; i4++) {
                int i5 = (i4 * 8) + i2;
                s = (short) ((((255 << i5) ^ -1) & s) | ((bArr[i4 + i] & 255) << i5));
            }
        }
        return s;
    }

    public static long hexToLong(String str, int i, long j, int i2, int i3) {
        if (i3 != 0) {
            if (((i3 - 1) * 4) + i2 >= 64) {
                throw new IllegalArgumentException("(nHexs-1)*4+dstPos is greather or equal to than 64");
            }
            for (int i4 = 0; i4 < i3; i4++) {
                int i5 = (i4 * 4) + i2;
                j = (((15 << i5) ^ -1) & j) | ((((long) hexDigitToInt(str.charAt(i4 + i))) & 15) << i5);
            }
        }
        return j;
    }

    public static int hexToInt(String str, int i, int i2, int i3, int i4) {
        if (i4 != 0) {
            if (((i4 - 1) * 4) + i3 >= 32) {
                throw new IllegalArgumentException("(nHexs-1)*4+dstPos is greather or equal to than 32");
            }
            for (int i5 = 0; i5 < i4; i5++) {
                int i6 = (i5 * 4) + i3;
                i2 = (((15 << i6) ^ -1) & i2) | ((hexDigitToInt(str.charAt(i5 + i)) & 15) << i6);
            }
        }
        return i2;
    }

    public static short hexToShort(String str, int i, short s, int i2, int i3) {
        if (i3 != 0) {
            if (((i3 - 1) * 4) + i2 >= 16) {
                throw new IllegalArgumentException("(nHexs-1)*4+dstPos is greather or equal to than 16");
            }
            for (int i4 = 0; i4 < i3; i4++) {
                int i5 = (i4 * 4) + i2;
                s = (short) ((((15 << i5) ^ -1) & s) | ((hexDigitToInt(str.charAt(i4 + i)) & 15) << i5));
            }
        }
        return s;
    }

    public static byte hexToByte(String str, int i, byte b, int i2, int i3) {
        if (i3 != 0) {
            if (((i3 - 1) * 4) + i2 >= 8) {
                throw new IllegalArgumentException("(nHexs-1)*4+dstPos is greather or equal to than 8");
            }
            for (int i4 = 0; i4 < i3; i4++) {
                int i5 = (i4 * 4) + i2;
                b = (byte) ((((15 << i5) ^ -1) & b) | ((hexDigitToInt(str.charAt(i4 + i)) & 15) << i5));
            }
        }
        return b;
    }

    public static long binaryToLong(boolean[] zArr, int i, long j, int i2, int i3) {
        if (!((zArr.length == 0 && i == 0) || i3 == 0)) {
            if ((i3 - 1) + i2 >= 64) {
                throw new IllegalArgumentException("nBools-1+dstPos is greather or equal to than 64");
            }
            for (int i4 = 0; i4 < i3; i4++) {
                int i5 = i4 + i2;
                j = (((1 << i5) ^ -1) & j) | ((zArr[i4 + i] ? 1 : 0) << i5);
            }
        }
        return j;
    }

    public static int binaryToInt(boolean[] zArr, int i, int i2, int i3, int i4) {
        int i5;
        if (!((zArr.length == 0 && i == 0) || i4 == 0)) {
            if ((i4 - 1) + i3 >= 32) {
                throw new IllegalArgumentException("nBools-1+dstPos is greather or equal to than 32");
            }
            for (int i6 = 0; i6 < i4; i6++) {
                int i7 = i6 + i3;
                if (zArr[i6 + i]) {
                    i5 = 1;
                } else {
                    i5 = 0;
                }
                i2 = (((1 << i7) ^ -1) & i2) | (i5 << i7);
            }
        }
        return i2;
    }

    public static short binaryToShort(boolean[] zArr, int i, short s, int i2, int i3) {
        int i4;
        if (!((zArr.length == 0 && i == 0) || i3 == 0)) {
            if ((i3 - 1) + i2 >= 16) {
                throw new IllegalArgumentException("nBools-1+dstPos is greather or equal to than 16");
            }
            for (int i5 = 0; i5 < i3; i5++) {
                int i6 = i5 + i2;
                if (zArr[i5 + i]) {
                    i4 = 1;
                } else {
                    i4 = 0;
                }
                s = (short) ((i4 << i6) | (((1 << i6) ^ -1) & s));
            }
        }
        return s;
    }

    public static byte binaryToByte(boolean[] zArr, int i, byte b, int i2, int i3) {
        int i4;
        if (!((zArr.length == 0 && i == 0) || i3 == 0)) {
            if ((i3 - 1) + i2 >= 8) {
                throw new IllegalArgumentException("nBools-1+dstPos is greather or equal to than 8");
            }
            for (int i5 = 0; i5 < i3; i5++) {
                int i6 = i5 + i2;
                if (zArr[i5 + i]) {
                    i4 = 1;
                } else {
                    i4 = 0;
                }
                b = (byte) ((i4 << i6) | (((1 << i6) ^ -1) & b));
            }
        }
        return b;
    }

    public static int[] longToIntArray(long j, int i, int[] iArr, int i2, int i3) {
        if (i3 != 0) {
            if (((i3 - 1) * 32) + i >= 64) {
                throw new IllegalArgumentException("(nInts-1)*32+srcPos is greather or equal to than 64");
            }
            for (int i4 = 0; i4 < i3; i4++) {
                iArr[i2 + i4] = (int) (-1 & (j >> ((i4 * 32) + i)));
            }
        }
        return iArr;
    }

    public static short[] longToShortArray(long j, int i, short[] sArr, int i2, int i3) {
        if (i3 != 0) {
            if (((i3 - 1) * 16) + i >= 64) {
                throw new IllegalArgumentException("(nShorts-1)*16+srcPos is greather or equal to than 64");
            }
            for (int i4 = 0; i4 < i3; i4++) {
                sArr[i2 + i4] = (short) ((int) (65535 & (j >> ((i4 * 16) + i))));
            }
        }
        return sArr;
    }

    public static short[] intToShortArray(int i, int i2, short[] sArr, int i3, int i4) {
        if (i4 != 0) {
            if (((i4 - 1) * 16) + i2 >= 32) {
                throw new IllegalArgumentException("(nShorts-1)*16+srcPos is greather or equal to than 32");
            }
            for (int i5 = 0; i5 < i4; i5++) {
                sArr[i3 + i5] = (short) ((i >> ((i5 * 16) + i2)) & 65535);
            }
        }
        return sArr;
    }

    public static byte[] longToByteArray(long j, int i, byte[] bArr, int i2, int i3) {
        if (i3 != 0) {
            if (((i3 - 1) * 8) + i >= 64) {
                throw new IllegalArgumentException("(nBytes-1)*8+srcPos is greather or equal to than 64");
            }
            for (int i4 = 0; i4 < i3; i4++) {
                bArr[i2 + i4] = (byte) ((int) (255 & (j >> ((i4 * 8) + i))));
            }
        }
        return bArr;
    }

    public static byte[] intToByteArray(int i, int i2, byte[] bArr, int i3, int i4) {
        if (i4 != 0) {
            if (((i4 - 1) * 8) + i2 >= 32) {
                throw new IllegalArgumentException("(nBytes-1)*8+srcPos is greather or equal to than 32");
            }
            for (int i5 = 0; i5 < i4; i5++) {
                bArr[i3 + i5] = (byte) ((i >> ((i5 * 8) + i2)) & 255);
            }
        }
        return bArr;
    }

    public static byte[] shortToByteArray(short s, int i, byte[] bArr, int i2, int i3) {
        if (i3 != 0) {
            if (((i3 - 1) * 8) + i >= 16) {
                throw new IllegalArgumentException("(nBytes-1)*8+srcPos is greather or equal to than 16");
            }
            for (int i4 = 0; i4 < i3; i4++) {
                bArr[i2 + i4] = (byte) ((s >> ((i4 * 8) + i)) & 255);
            }
        }
        return bArr;
    }

    public static String longToHex(long j, int i, String str, int i2, int i3) {
        if (i3 == 0) {
            return str;
        }
        if (((i3 - 1) * 4) + i >= 64) {
            throw new IllegalArgumentException("(nHexs-1)*4+srcPos is greather or equal to than 64");
        }
        StringBuilder sb = new StringBuilder(str);
        int length = sb.length();
        for (int i4 = 0; i4 < i3; i4++) {
            int i5 = (int) (15 & (j >> ((i4 * 4) + i)));
            if (i2 + i4 == length) {
                length++;
                sb.append(intToHexDigit(i5));
            } else {
                sb.setCharAt(i2 + i4, intToHexDigit(i5));
            }
        }
        return sb.toString();
    }

    public static String intToHex(int i, int i2, String str, int i3, int i4) {
        if (i4 == 0) {
            return str;
        }
        if (((i4 - 1) * 4) + i2 >= 32) {
            throw new IllegalArgumentException("(nHexs-1)*4+srcPos is greather or equal to than 32");
        }
        StringBuilder sb = new StringBuilder(str);
        int length = sb.length();
        for (int i5 = 0; i5 < i4; i5++) {
            int i6 = (i >> ((i5 * 4) + i2)) & 15;
            if (i3 + i5 == length) {
                length++;
                sb.append(intToHexDigit(i6));
            } else {
                sb.setCharAt(i3 + i5, intToHexDigit(i6));
            }
        }
        return sb.toString();
    }

    public static String shortToHex(short s, int i, String str, int i2, int i3) {
        if (i3 == 0) {
            return str;
        }
        if (((i3 - 1) * 4) + i >= 16) {
            throw new IllegalArgumentException("(nHexs-1)*4+srcPos is greather or equal to than 16");
        }
        StringBuilder sb = new StringBuilder(str);
        int length = sb.length();
        for (int i4 = 0; i4 < i3; i4++) {
            int i5 = (s >> ((i4 * 4) + i)) & 15;
            if (i2 + i4 == length) {
                length++;
                sb.append(intToHexDigit(i5));
            } else {
                sb.setCharAt(i2 + i4, intToHexDigit(i5));
            }
        }
        return sb.toString();
    }

    public static String byteToHex(byte b, int i, String str, int i2, int i3) {
        if (i3 == 0) {
            return str;
        }
        if (((i3 - 1) * 4) + i >= 8) {
            throw new IllegalArgumentException("(nHexs-1)*4+srcPos is greather or equal to than 8");
        }
        StringBuilder sb = new StringBuilder(str);
        int length = sb.length();
        for (int i4 = 0; i4 < i3; i4++) {
            int i5 = (b >> ((i4 * 4) + i)) & 15;
            if (i2 + i4 == length) {
                length++;
                sb.append(intToHexDigit(i5));
            } else {
                sb.setCharAt(i2 + i4, intToHexDigit(i5));
            }
        }
        return sb.toString();
    }

    public static boolean[] longToBinary(long j, int i, boolean[] zArr, int i2, int i3) {
        boolean z;
        if (i3 != 0) {
            if ((i3 - 1) + i >= 64) {
                throw new IllegalArgumentException("nBools-1+srcPos is greather or equal to than 64");
            }
            for (int i4 = 0; i4 < i3; i4++) {
                int i5 = i2 + i4;
                if ((1 & (j >> (i4 + i))) != 0) {
                    z = true;
                } else {
                    z = false;
                }
                zArr[i5] = z;
            }
        }
        return zArr;
    }

    public static boolean[] intToBinary(int i, int i2, boolean[] zArr, int i3, int i4) {
        boolean z;
        if (i4 != 0) {
            if ((i4 - 1) + i2 >= 32) {
                throw new IllegalArgumentException("nBools-1+srcPos is greather or equal to than 32");
            }
            for (int i5 = 0; i5 < i4; i5++) {
                int i6 = i3 + i5;
                if (((i >> (i5 + i2)) & 1) != 0) {
                    z = true;
                } else {
                    z = false;
                }
                zArr[i6] = z;
            }
        }
        return zArr;
    }

    public static boolean[] shortToBinary(short s, int i, boolean[] zArr, int i2, int i3) {
        boolean z;
        if (i3 != 0) {
            if ((i3 - 1) + i >= 16) {
                throw new IllegalArgumentException("nBools-1+srcPos is greather or equal to than 16");
            } else if ($assertionsDisabled || i3 - 1 < 16 - i) {
                for (int i4 = 0; i4 < i3; i4++) {
                    int i5 = i2 + i4;
                    if (((s >> (i4 + i)) & 1) != 0) {
                        z = true;
                    } else {
                        z = false;
                    }
                    zArr[i5] = z;
                }
            } else {
                throw new AssertionError();
            }
        }
        return zArr;
    }

    public static boolean[] byteToBinary(byte b, int i, boolean[] zArr, int i2, int i3) {
        boolean z;
        if (i3 != 0) {
            if ((i3 - 1) + i >= 8) {
                throw new IllegalArgumentException("nBools-1+srcPos is greather or equal to than 8");
            }
            for (int i4 = 0; i4 < i3; i4++) {
                int i5 = i2 + i4;
                if (((b >> (i4 + i)) & 1) != 0) {
                    z = true;
                } else {
                    z = false;
                }
                zArr[i5] = z;
            }
        }
        return zArr;
    }

    public static byte[] uuidToByteArray(UUID uuid, byte[] bArr, int i, int i2) {
        int i3;
        if (i2 != 0) {
            if (i2 > 16) {
                throw new IllegalArgumentException("nBytes is greather than 16");
            }
            long mostSignificantBits = uuid.getMostSignificantBits();
            if (i2 > 8) {
                i3 = 8;
            } else {
                i3 = i2;
            }
            longToByteArray(mostSignificantBits, 0, bArr, i, i3);
            if (i2 >= 8) {
                longToByteArray(uuid.getLeastSignificantBits(), 0, bArr, i + 8, i2 - 8);
            }
        }
        return bArr;
    }

    public static UUID byteArrayToUuid(byte[] bArr, int i) {
        if (bArr.length - i < 16) {
            throw new IllegalArgumentException("Need at least 16 bytes for UUID");
        }
        return new UUID(byteArrayToLong(bArr, i, 0, 0, 8), byteArrayToLong(bArr, i + 8, 0, 0, 8));
    }
}
