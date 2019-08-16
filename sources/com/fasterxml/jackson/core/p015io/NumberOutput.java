package com.fasterxml.jackson.core.p015io;

import com.facebook.appevents.AppEventsConstants;
import com.google.android.gms.games.GamesStatusCodes;
import org.onepf.oms.appstore.SamsungAppsBillingService;

/* renamed from: com.fasterxml.jackson.core.io.NumberOutput */
public final class NumberOutput {
    private static int BILLION = 1000000000;
    private static final char[] FULL_3 = new char[GamesStatusCodes.STATUS_SNAPSHOT_NOT_FOUND];
    private static final byte[] FULL_TRIPLETS_B = new byte[GamesStatusCodes.STATUS_SNAPSHOT_NOT_FOUND];
    private static final char[] LEAD_3 = new char[GamesStatusCodes.STATUS_SNAPSHOT_NOT_FOUND];
    private static long MAX_INT_AS_LONG = 2147483647L;
    private static int MILLION = 1000000;
    private static long MIN_INT_AS_LONG = -2147483648L;

    /* renamed from: NC */
    private static final char f411NC = 0;
    static final String SMALLEST_LONG = String.valueOf(Long.MIN_VALUE);
    private static long TEN_BILLION_L = 10000000000L;
    private static long THOUSAND_L = 1000;
    private static final String[] sSmallIntStrs = {AppEventsConstants.EVENT_PARAM_VALUE_NO, AppEventsConstants.EVENT_PARAM_VALUE_YES, "2", "3", "4", "5", "6", "7", "8", "9", SamsungAppsBillingService.ITEM_TYPE_ALL};
    private static final String[] sSmallIntStrs2 = {"-1", "-2", "-3", "-4", "-5", "-6", "-7", "-8", "-9", "-10"};

    static {
        char c;
        char c2;
        int i = 0;
        for (int i2 = 0; i2 < 10; i2++) {
            char c3 = (char) (i2 + 48);
            if (i2 == 0) {
                c = 0;
            } else {
                c = c3;
            }
            for (int i3 = 0; i3 < 10; i3++) {
                char c4 = (char) (i3 + 48);
                if (i2 == 0 && i3 == 0) {
                    c2 = 0;
                } else {
                    c2 = c4;
                }
                for (int i4 = 0; i4 < 10; i4++) {
                    char c5 = (char) (i4 + 48);
                    LEAD_3[i] = c;
                    LEAD_3[i + 1] = c2;
                    LEAD_3[i + 2] = c5;
                    FULL_3[i] = c3;
                    FULL_3[i + 1] = c4;
                    FULL_3[i + 2] = c5;
                    i += 4;
                }
            }
        }
        for (int i5 = 0; i5 < 4000; i5++) {
            FULL_TRIPLETS_B[i5] = (byte) FULL_3[i5];
        }
    }

    public static int outputInt(int i, char[] cArr, int i2) {
        int leading3;
        if (i < 0) {
            if (i == Integer.MIN_VALUE) {
                return outputLong((long) i, cArr, i2);
            }
            int i3 = i2 + 1;
            cArr[i2] = '-';
            i = -i;
            i2 = i3;
        }
        if (i >= MILLION) {
            boolean z = i >= BILLION;
            if (z) {
                i -= BILLION;
                if (i >= BILLION) {
                    i -= BILLION;
                    int i4 = i2 + 1;
                    cArr[i2] = '2';
                    i2 = i4;
                } else {
                    int i5 = i2 + 1;
                    cArr[i2] = '1';
                    i2 = i5;
                }
            }
            int i6 = i / 1000;
            int i7 = i - (i6 * 1000);
            int i8 = i6 / 1000;
            int i9 = i6 - (i8 * 1000);
            if (z) {
                leading3 = full3(i8, cArr, i2);
            } else {
                leading3 = leading3(i8, cArr, i2);
            }
            return full3(i7, cArr, full3(i9, cArr, leading3));
        } else if (i >= 1000) {
            int i10 = i / 1000;
            return full3(i - (i10 * 1000), cArr, leading3(i10, cArr, i2));
        } else if (i >= 10) {
            return leading3(i, cArr, i2);
        } else {
            int i11 = i2 + 1;
            cArr[i2] = (char) (i + 48);
            return i11;
        }
    }

    public static int outputInt(int i, byte[] bArr, int i2) {
        int leading3;
        if (i < 0) {
            if (i == Integer.MIN_VALUE) {
                return outputLong((long) i, bArr, i2);
            }
            int i3 = i2 + 1;
            bArr[i2] = 45;
            i = -i;
            i2 = i3;
        }
        if (i >= MILLION) {
            boolean z = i >= BILLION;
            if (z) {
                i -= BILLION;
                if (i >= BILLION) {
                    i -= BILLION;
                    int i4 = i2 + 1;
                    bArr[i2] = 50;
                    i2 = i4;
                } else {
                    int i5 = i2 + 1;
                    bArr[i2] = 49;
                    i2 = i5;
                }
            }
            int i6 = i / 1000;
            int i7 = i - (i6 * 1000);
            int i8 = i6 / 1000;
            int i9 = i6 - (i8 * 1000);
            if (z) {
                leading3 = full3(i8, bArr, i2);
            } else {
                leading3 = leading3(i8, bArr, i2);
            }
            return full3(i7, bArr, full3(i9, bArr, leading3));
        } else if (i >= 1000) {
            int i10 = i / 1000;
            return full3(i - (i10 * 1000), bArr, leading3(i10, bArr, i2));
        } else if (i >= 10) {
            return leading3(i, bArr, i2);
        } else {
            int i11 = i2 + 1;
            bArr[i2] = (byte) (i + 48);
            return i11;
        }
    }

    public static int outputLong(long j, char[] cArr, int i) {
        if (j < 0) {
            if (j > MIN_INT_AS_LONG) {
                return outputInt((int) j, cArr, i);
            }
            if (j == Long.MIN_VALUE) {
                int length = SMALLEST_LONG.length();
                SMALLEST_LONG.getChars(0, length, cArr, i);
                return i + length;
            }
            int i2 = i + 1;
            cArr[i] = '-';
            j = -j;
            i = i2;
        } else if (j <= MAX_INT_AS_LONG) {
            return outputInt((int) j, cArr, i);
        }
        int calcLongStrLength = i + calcLongStrLength(j);
        int i3 = calcLongStrLength;
        while (j > MAX_INT_AS_LONG) {
            i3 -= 3;
            long j2 = j / THOUSAND_L;
            full3((int) (j - (THOUSAND_L * j2)), cArr, i3);
            j = j2;
        }
        int i4 = (int) j;
        int i5 = i3;
        while (i4 >= 1000) {
            i5 -= 3;
            int i6 = i4 / 1000;
            full3(i4 - (i6 * 1000), cArr, i5);
            i4 = i6;
        }
        leading3(i4, cArr, i);
        return calcLongStrLength;
    }

    public static int outputLong(long j, byte[] bArr, int i) {
        if (j < 0) {
            if (j > MIN_INT_AS_LONG) {
                return outputInt((int) j, bArr, i);
            }
            if (j == Long.MIN_VALUE) {
                int length = SMALLEST_LONG.length();
                int i2 = 0;
                int i3 = i;
                while (i2 < length) {
                    int i4 = i3 + 1;
                    bArr[i3] = (byte) SMALLEST_LONG.charAt(i2);
                    i2++;
                    i3 = i4;
                }
                return i3;
            }
            int i5 = i + 1;
            bArr[i] = 45;
            j = -j;
            i = i5;
        } else if (j <= MAX_INT_AS_LONG) {
            return outputInt((int) j, bArr, i);
        }
        int calcLongStrLength = i + calcLongStrLength(j);
        int i6 = calcLongStrLength;
        while (j > MAX_INT_AS_LONG) {
            i6 -= 3;
            long j2 = j / THOUSAND_L;
            full3((int) (j - (THOUSAND_L * j2)), bArr, i6);
            j = j2;
        }
        int i7 = (int) j;
        int i8 = i6;
        while (i7 >= 1000) {
            i8 -= 3;
            int i9 = i7 / 1000;
            full3(i7 - (i9 * 1000), bArr, i8);
            i7 = i9;
        }
        leading3(i7, bArr, i);
        return calcLongStrLength;
    }

    public static String toString(int i) {
        if (i < sSmallIntStrs.length) {
            if (i >= 0) {
                return sSmallIntStrs[i];
            }
            int i2 = (-i) - 1;
            if (i2 < sSmallIntStrs2.length) {
                return sSmallIntStrs2[i2];
            }
        }
        return Integer.toString(i);
    }

    public static String toString(long j) {
        if (j > 2147483647L || j < -2147483648L) {
            return Long.toString(j);
        }
        return toString((int) j);
    }

    public static String toString(double d) {
        return Double.toString(d);
    }

    public static String toString(float f) {
        return Float.toString(f);
    }

    private static int leading3(int i, char[] cArr, int i2) {
        int i3 = i << 2;
        int i4 = i3 + 1;
        char c = LEAD_3[i3];
        if (c != 0) {
            int i5 = i2 + 1;
            cArr[i2] = c;
            i2 = i5;
        }
        int i6 = i4 + 1;
        char c2 = LEAD_3[i4];
        if (c2 != 0) {
            int i7 = i2 + 1;
            cArr[i2] = c2;
            i2 = i7;
        }
        int i8 = i2 + 1;
        cArr[i2] = LEAD_3[i6];
        return i8;
    }

    private static int leading3(int i, byte[] bArr, int i2) {
        int i3 = i << 2;
        int i4 = i3 + 1;
        char c = LEAD_3[i3];
        if (c != 0) {
            int i5 = i2 + 1;
            bArr[i2] = (byte) c;
            i2 = i5;
        }
        int i6 = i4 + 1;
        char c2 = LEAD_3[i4];
        if (c2 != 0) {
            int i7 = i2 + 1;
            bArr[i2] = (byte) c2;
            i2 = i7;
        }
        int i8 = i2 + 1;
        bArr[i2] = (byte) LEAD_3[i6];
        return i8;
    }

    private static int full3(int i, char[] cArr, int i2) {
        int i3 = i << 2;
        int i4 = i2 + 1;
        int i5 = i3 + 1;
        cArr[i2] = FULL_3[i3];
        int i6 = i4 + 1;
        int i7 = i5 + 1;
        cArr[i4] = FULL_3[i5];
        int i8 = i6 + 1;
        cArr[i6] = FULL_3[i7];
        return i8;
    }

    private static int full3(int i, byte[] bArr, int i2) {
        int i3 = i << 2;
        int i4 = i2 + 1;
        int i5 = i3 + 1;
        bArr[i2] = FULL_TRIPLETS_B[i3];
        int i6 = i4 + 1;
        int i7 = i5 + 1;
        bArr[i4] = FULL_TRIPLETS_B[i5];
        int i8 = i6 + 1;
        bArr[i6] = FULL_TRIPLETS_B[i7];
        return i8;
    }

    private static int calcLongStrLength(long j) {
        int i = 10;
        for (long j2 = TEN_BILLION_L; j >= j2 && i != 19; j2 = (j2 << 1) + (j2 << 3)) {
            i++;
        }
        return i;
    }
}
