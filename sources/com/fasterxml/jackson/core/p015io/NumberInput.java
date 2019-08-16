package com.fasterxml.jackson.core.p015io;

import java.math.BigDecimal;

/* renamed from: com.fasterxml.jackson.core.io.NumberInput */
public final class NumberInput {
    static final long L_BILLION = 1000000000;
    static final String MAX_LONG_STR = String.valueOf(Long.MAX_VALUE);
    static final String MIN_LONG_STR_NO_SIGN = String.valueOf(Long.MIN_VALUE).substring(1);
    public static final String NASTY_SMALL_DOUBLE = "2.2250738585072012e-308";

    public static int parseInt(char[] cArr, int i, int i2) {
        int i3 = cArr[i] - '0';
        if (i2 > 4) {
            int i4 = i + 1;
            int i5 = i4 + 1;
            int i6 = i5 + 1;
            i = i6 + 1;
            i3 = (((((((i3 * 10) + (cArr[i4] - '0')) * 10) + (cArr[i5] - '0')) * 10) + (cArr[i6] - '0')) * 10) + (cArr[i] - '0');
            i2 -= 4;
            if (i2 > 4) {
                int i7 = i + 1;
                int i8 = i7 + 1;
                int i9 = i8 + 1;
                return (((((((i3 * 10) + (cArr[i7] - '0')) * 10) + (cArr[i8] - '0')) * 10) + (cArr[i9] - '0')) * 10) + (cArr[i9 + 1] - '0');
            }
        }
        if (i2 <= 1) {
            return i3;
        }
        int i10 = i + 1;
        int i11 = (i3 * 10) + (cArr[i10] - '0');
        if (i2 <= 2) {
            return i11;
        }
        int i12 = i10 + 1;
        int i13 = (i11 * 10) + (cArr[i12] - '0');
        if (i2 > 3) {
            return (i13 * 10) + (cArr[i12 + 1] - '0');
        }
        return i13;
    }

    /* JADX WARNING: Code restructure failed: missing block: B:48:?, code lost:
        return java.lang.Integer.parseInt(r7);
     */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    public static int parseInt(java.lang.String r7) {
        /*
            r2 = 0
            r1 = 1
            r6 = 57
            r5 = 48
            char r0 = r7.charAt(r2)
            int r4 = r7.length()
            r3 = 45
            if (r0 != r3) goto L_0x0020
            r3 = r1
        L_0x0013:
            if (r3 == 0) goto L_0x0031
            if (r4 == r1) goto L_0x001b
            r0 = 10
            if (r4 <= r0) goto L_0x0022
        L_0x001b:
            int r0 = java.lang.Integer.parseInt(r7)
        L_0x001f:
            return r0
        L_0x0020:
            r3 = r2
            goto L_0x0013
        L_0x0022:
            r0 = 2
            char r2 = r7.charAt(r1)
            r1 = r0
        L_0x0028:
            if (r2 > r6) goto L_0x002c
            if (r2 >= r5) goto L_0x003a
        L_0x002c:
            int r0 = java.lang.Integer.parseInt(r7)
            goto L_0x001f
        L_0x0031:
            r2 = 9
            if (r4 <= r2) goto L_0x0086
            int r0 = java.lang.Integer.parseInt(r7)
            goto L_0x001f
        L_0x003a:
            int r0 = r2 + -48
            if (r1 >= r4) goto L_0x0080
            int r2 = r1 + 1
            char r1 = r7.charAt(r1)
            if (r1 > r6) goto L_0x0048
            if (r1 >= r5) goto L_0x004d
        L_0x0048:
            int r0 = java.lang.Integer.parseInt(r7)
            goto L_0x001f
        L_0x004d:
            int r0 = r0 * 10
            int r1 = r1 + -48
            int r0 = r0 + r1
            if (r2 >= r4) goto L_0x0080
            int r1 = r2 + 1
            char r2 = r7.charAt(r2)
            if (r2 > r6) goto L_0x005e
            if (r2 >= r5) goto L_0x0063
        L_0x005e:
            int r0 = java.lang.Integer.parseInt(r7)
            goto L_0x001f
        L_0x0063:
            int r0 = r0 * 10
            int r2 = r2 + -48
            int r0 = r0 + r2
            if (r1 >= r4) goto L_0x0080
        L_0x006a:
            int r2 = r1 + 1
            char r1 = r7.charAt(r1)
            if (r1 > r6) goto L_0x0074
            if (r1 >= r5) goto L_0x0079
        L_0x0074:
            int r0 = java.lang.Integer.parseInt(r7)
            goto L_0x001f
        L_0x0079:
            int r0 = r0 * 10
            int r1 = r1 + -48
            int r0 = r0 + r1
            if (r2 < r4) goto L_0x0084
        L_0x0080:
            if (r3 == 0) goto L_0x001f
            int r0 = -r0
            goto L_0x001f
        L_0x0084:
            r1 = r2
            goto L_0x006a
        L_0x0086:
            r2 = r0
            goto L_0x0028
        */
        throw new UnsupportedOperationException("Method not decompiled: com.fasterxml.jackson.core.p015io.NumberInput.parseInt(java.lang.String):int");
    }

    public static long parseLong(char[] cArr, int i, int i2) {
        int i3 = i2 - 9;
        return ((long) parseInt(cArr, i3 + i, 9)) + (((long) parseInt(cArr, i, i3)) * L_BILLION);
    }

    public static long parseLong(String str) {
        if (str.length() <= 9) {
            return (long) parseInt(str);
        }
        return Long.parseLong(str);
    }

    public static boolean inLongRange(char[] cArr, int i, int i2, boolean z) {
        String str = z ? MIN_LONG_STR_NO_SIGN : MAX_LONG_STR;
        int length = str.length();
        if (i2 < length) {
            return true;
        }
        if (i2 > length) {
            return false;
        }
        for (int i3 = 0; i3 < length; i3++) {
            int charAt = cArr[i + i3] - str.charAt(i3);
            if (charAt != 0) {
                return charAt < 0;
            }
        }
        return true;
    }

    public static boolean inLongRange(String str, boolean z) {
        String str2 = z ? MIN_LONG_STR_NO_SIGN : MAX_LONG_STR;
        int length = str2.length();
        int length2 = str.length();
        if (length2 < length) {
            return true;
        }
        if (length2 > length) {
            return false;
        }
        for (int i = 0; i < length; i++) {
            int charAt = str.charAt(i) - str2.charAt(i);
            if (charAt != 0) {
                return charAt < 0;
            }
        }
        return true;
    }

    public static int parseAsInt(String str, int i) {
        int i2 = 0;
        if (str == null) {
            return i;
        }
        String trim = str.trim();
        int length = trim.length();
        if (length == 0) {
            return i;
        }
        if (0 < length) {
            char charAt = trim.charAt(0);
            if (charAt == '+') {
                trim = trim.substring(1);
                length = trim.length();
            } else if (charAt == '-') {
                i2 = 1;
            }
        }
        while (i2 < length) {
            char charAt2 = trim.charAt(i2);
            if (charAt2 > '9' || charAt2 < '0') {
                try {
                    return (int) parseDouble(trim);
                } catch (NumberFormatException e) {
                    return i;
                }
            } else {
                i2++;
            }
        }
        try {
            return Integer.parseInt(trim);
        } catch (NumberFormatException e2) {
            return i;
        }
    }

    public static long parseAsLong(String str, long j) {
        int i = 0;
        if (str == null) {
            return j;
        }
        String trim = str.trim();
        int length = trim.length();
        if (length == 0) {
            return j;
        }
        if (0 < length) {
            char charAt = trim.charAt(0);
            if (charAt == '+') {
                trim = trim.substring(1);
                length = trim.length();
            } else if (charAt == '-') {
                i = 1;
            }
        }
        while (i < length) {
            char charAt2 = trim.charAt(i);
            if (charAt2 > '9' || charAt2 < '0') {
                try {
                    return (long) parseDouble(trim);
                } catch (NumberFormatException e) {
                    return j;
                }
            } else {
                i++;
            }
        }
        try {
            return Long.parseLong(trim);
        } catch (NumberFormatException e2) {
            return j;
        }
    }

    public static double parseAsDouble(String str, double d) {
        if (str == null) {
            return d;
        }
        String trim = str.trim();
        if (trim.length() == 0) {
            return d;
        }
        try {
            return parseDouble(trim);
        } catch (NumberFormatException e) {
            return d;
        }
    }

    public static double parseDouble(String str) throws NumberFormatException {
        if (NASTY_SMALL_DOUBLE.equals(str)) {
            return Double.MIN_VALUE;
        }
        return Double.parseDouble(str);
    }

    public static BigDecimal parseBigDecimal(String str) throws NumberFormatException {
        try {
            return new BigDecimal(str);
        } catch (NumberFormatException e) {
            throw _badBD(str);
        }
    }

    public static BigDecimal parseBigDecimal(char[] cArr) throws NumberFormatException {
        return parseBigDecimal(cArr, 0, cArr.length);
    }

    public static BigDecimal parseBigDecimal(char[] cArr, int i, int i2) throws NumberFormatException {
        try {
            return new BigDecimal(cArr, i, i2);
        } catch (NumberFormatException e) {
            throw _badBD(new String(cArr, i, i2));
        }
    }

    private static NumberFormatException _badBD(String str) {
        return new NumberFormatException("Value \"" + str + "\" can not be represented as BigDecimal");
    }
}
