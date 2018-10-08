package org.apache.commons.lang3;

import android.support.v4.media.TransportMediator;
import com.fasterxml.jackson.core.base.GeneratorBase;
import com.google.android.gms.nearby.messages.Strategy;
import java.util.Random;

public class RandomStringUtils {
    private static final Random RANDOM = new Random();

    public static String random(int i) {
        return random(i, false, false);
    }

    public static String randomAscii(int i) {
        return random(i, 32, TransportMediator.KEYCODE_MEDIA_PAUSE, false, false);
    }

    public static String randomAlphabetic(int i) {
        return random(i, true, false);
    }

    public static String randomAlphanumeric(int i) {
        return random(i, true, true);
    }

    public static String randomNumeric(int i) {
        return random(i, false, true);
    }

    public static String random(int i, boolean z, boolean z2) {
        return random(i, 0, 0, z, z2);
    }

    public static String random(int i, int i2, int i3, boolean z, boolean z2) {
        return random(i, i2, i3, z, z2, null, RANDOM);
    }

    public static String random(int i, int i2, int i3, boolean z, boolean z2, char... cArr) {
        return random(i, i2, i3, z, z2, cArr, RANDOM);
    }

    public static String random(int i, int i2, int i3, boolean z, boolean z2, char[] cArr, Random random) {
        if (i == 0) {
            return "";
        }
        if (i < 0) {
            throw new IllegalArgumentException("Requested random string length " + i + " is less than 0.");
        } else if (cArr == null || cArr.length != 0) {
            if (i2 == 0 && i3 == 0) {
                if (cArr != null) {
                    i3 = cArr.length;
                } else if (z || z2) {
                    i3 = 123;
                    i2 = 32;
                } else {
                    i3 = Strategy.TTL_SECONDS_INFINITE;
                }
            } else if (i3 <= i2) {
                throw new IllegalArgumentException("Parameter end (" + i3 + ") must be greater than start (" + i2 + ")");
            }
            char[] cArr2 = new char[i];
            int i4 = i3 - i2;
            while (true) {
                int i5 = i - 1;
                if (i == 0) {
                    return new String(cArr2);
                }
                char nextInt;
                if (cArr == null) {
                    nextInt = (char) (random.nextInt(i4) + i2);
                } else {
                    nextInt = cArr[random.nextInt(i4) + i2];
                }
                if (!(z && Character.isLetter(nextInt)) && (!(z2 && Character.isDigit(nextInt)) && (z || z2))) {
                    i5++;
                } else if (nextInt < '?' || nextInt > '?') {
                    if (nextInt < '?' || nextInt > '?') {
                        if (nextInt < '?' || nextInt > '?') {
                            cArr2[i5] = nextInt;
                        } else {
                            i5++;
                        }
                    } else if (i5 == 0) {
                        i5++;
                    } else {
                        cArr2[i5] = (char) (random.nextInt(128) + GeneratorBase.SURR2_FIRST);
                        i5--;
                        cArr2[i5] = nextInt;
                    }
                } else if (i5 == 0) {
                    i5++;
                } else {
                    cArr2[i5] = nextInt;
                    i5--;
                    cArr2[i5] = (char) (random.nextInt(128) + GeneratorBase.SURR1_FIRST);
                }
                i = i5;
            }
        } else {
            throw new IllegalArgumentException("The chars array must not be empty");
        }
    }

    public static String random(int i, String str) {
        if (str != null) {
            return random(i, str.toCharArray());
        }
        return random(i, 0, 0, false, false, null, RANDOM);
    }

    public static String random(int i, char... cArr) {
        if (cArr == null) {
            return random(i, 0, 0, false, false, null, RANDOM);
        }
        return random(i, 0, cArr.length, false, false, cArr, RANDOM);
    }
}
