package org.apache.commons.lang3.math;

import com.facebook.appevents.AppEventsConstants;
import java.lang.reflect.Array;
import java.math.BigDecimal;
import java.math.BigInteger;
import net.gogame.gowrap.integrations.AbstractIntegrationSupport;
import org.apache.commons.lang3.StringUtils;
import org.apache.commons.lang3.Validate;

public class NumberUtils {
    public static final Byte BYTE_MINUS_ONE = Byte.valueOf(-1);
    public static final Byte BYTE_ONE = Byte.valueOf(1);
    public static final Byte BYTE_ZERO = Byte.valueOf(0);
    public static final Double DOUBLE_MINUS_ONE = Double.valueOf(-1.0d);
    public static final Double DOUBLE_ONE = Double.valueOf(1.0d);
    public static final Double DOUBLE_ZERO = Double.valueOf(0.0d);
    public static final Float FLOAT_MINUS_ONE = Float.valueOf(-1.0f);
    public static final Float FLOAT_ONE = Float.valueOf(1.0f);
    public static final Float FLOAT_ZERO = Float.valueOf(0.0f);
    public static final Integer INTEGER_MINUS_ONE = Integer.valueOf(-1);
    public static final Integer INTEGER_ONE = Integer.valueOf(1);
    public static final Integer INTEGER_ZERO = Integer.valueOf(0);
    public static final Long LONG_MINUS_ONE = Long.valueOf(-1);
    public static final Long LONG_ONE = Long.valueOf(1);
    public static final Long LONG_ZERO = Long.valueOf(0);
    public static final Short SHORT_MINUS_ONE = Short.valueOf(-1);
    public static final Short SHORT_ONE = Short.valueOf(1);
    public static final Short SHORT_ZERO = Short.valueOf(0);

    public static int toInt(String str) {
        return toInt(str, 0);
    }

    public static int toInt(String str, int i) {
        if (str == null) {
            return i;
        }
        try {
            return Integer.parseInt(str);
        } catch (NumberFormatException e) {
            return i;
        }
    }

    public static long toLong(String str) {
        return toLong(str, 0);
    }

    public static long toLong(String str, long j) {
        if (str == null) {
            return j;
        }
        try {
            return Long.parseLong(str);
        } catch (NumberFormatException e) {
            return j;
        }
    }

    public static float toFloat(String str) {
        return toFloat(str, 0.0f);
    }

    public static float toFloat(String str, float f) {
        if (str == null) {
            return f;
        }
        try {
            return Float.parseFloat(str);
        } catch (NumberFormatException e) {
            return f;
        }
    }

    public static double toDouble(String str) {
        return toDouble(str, 0.0d);
    }

    public static double toDouble(String str, double d) {
        if (str == null) {
            return d;
        }
        try {
            return Double.parseDouble(str);
        } catch (NumberFormatException e) {
            return d;
        }
    }

    public static byte toByte(String str) {
        return toByte(str, 0);
    }

    public static byte toByte(String str, byte b) {
        if (str == null) {
            return b;
        }
        try {
            return Byte.parseByte(str);
        } catch (NumberFormatException e) {
            return b;
        }
    }

    public static short toShort(String str) {
        return toShort(str, 0);
    }

    public static short toShort(String str, short s) {
        if (str == null) {
            return s;
        }
        try {
            return Short.parseShort(str);
        } catch (NumberFormatException e) {
            return s;
        }
    }

    public static Number createNumber(String str) throws NumberFormatException {
        int i;
        String mantissa;
        int i2;
        String str2;
        String str3;
        String substring;
        char c;
        String str4 = null;
        char c2 = 0;
        if (str == null) {
            return null;
        }
        if (StringUtils.isBlank(str)) {
            throw new NumberFormatException("A blank string is not a valid number");
        }
        String[] strArr = {"0x", "0X", "-0x", "-0X", "#", "-#"};
        int length = strArr.length;
        int i3 = 0;
        while (true) {
            if (i3 >= length) {
                i = 0;
                break;
            }
            String str5 = strArr[i3];
            if (str.startsWith(str5)) {
                i = 0 + str5.length();
                break;
            }
            i3++;
        }
        if (i > 0) {
            int i4 = i;
            int i5 = i;
            while (true) {
                if (i4 >= str.length()) {
                    c = c2;
                    break;
                }
                c = str.charAt(i4);
                if (c != '0') {
                    break;
                }
                i5++;
                i4++;
                c2 = c;
            }
            int length2 = str.length() - i5;
            if (length2 > 16 || (length2 == 16 && c > '7')) {
                return createBigInteger(str);
            }
            if (length2 > 8 || (length2 == 8 && c > '7')) {
                return createLong(str);
            }
            return createInteger(str);
        }
        char charAt = str.charAt(str.length() - 1);
        int indexOf = str.indexOf(46);
        int indexOf2 = str.indexOf(101) + str.indexOf(69) + 1;
        if (indexOf > -1) {
            if (indexOf2 <= -1) {
                substring = str.substring(indexOf + 1);
            } else if (indexOf2 < indexOf || indexOf2 > str.length()) {
                throw new NumberFormatException(str + " is not a valid number.");
            } else {
                substring = str.substring(indexOf + 1, indexOf2);
            }
            str3 = getMantissa(str, indexOf);
            i2 = substring.length();
            str2 = substring;
        } else {
            if (indexOf2 <= -1) {
                mantissa = getMantissa(str);
            } else if (indexOf2 > str.length()) {
                throw new NumberFormatException(str + " is not a valid number.");
            } else {
                mantissa = getMantissa(str, indexOf2);
            }
            i2 = 0;
            str2 = null;
            str3 = mantissa;
        }
        if (Character.isDigit(charAt) || charAt == '.') {
            if (indexOf2 > -1 && indexOf2 < str.length() - 1) {
                str4 = str.substring(indexOf2 + 1, str.length());
            }
            if (str2 == null && str4 == null) {
                try {
                    return createInteger(str);
                } catch (NumberFormatException e) {
                    try {
                        return createLong(str);
                    } catch (NumberFormatException e2) {
                        return createBigInteger(str);
                    }
                }
            } else {
                if (isAllZeros(str3) && isAllZeros(str4)) {
                    c2 = 1;
                }
                if (i2 <= 7) {
                    try {
                        Float createFloat = createFloat(str);
                        if (!createFloat.isInfinite() && !(createFloat.floatValue() == 0.0f && c2 == 0)) {
                            return createFloat;
                        }
                    } catch (NumberFormatException e3) {
                    }
                }
                if (i2 <= 16) {
                    try {
                        Double createDouble = createDouble(str);
                        if (!createDouble.isInfinite() && !(createDouble.doubleValue() == 0.0d && c2 == 0)) {
                            return createDouble;
                        }
                    } catch (NumberFormatException e4) {
                    }
                }
                return createBigDecimal(str);
            }
        } else {
            if (indexOf2 > -1 && indexOf2 < str.length() - 1) {
                str4 = str.substring(indexOf2 + 1, str.length() - 1);
            }
            String substring2 = str.substring(0, str.length() - 1);
            boolean z = isAllZeros(str3) && isAllZeros(str4);
            switch (charAt) {
                case 'D':
                case 'd':
                    break;
                case 'F':
                case 'f':
                    try {
                        Float createFloat2 = createFloat(substring2);
                        if (!createFloat2.isInfinite() && (createFloat2.floatValue() != 0.0f || z)) {
                            return createFloat2;
                        }
                    } catch (NumberFormatException e5) {
                        break;
                    }
                case 'L':
                case 'l':
                    if (str2 == null && str4 == null && ((substring2.charAt(0) == '-' && isDigits(substring2.substring(1))) || isDigits(substring2))) {
                        try {
                            return createLong(substring2);
                        } catch (NumberFormatException e6) {
                            return createBigInteger(substring2);
                        }
                    } else {
                        throw new NumberFormatException(str + " is not a valid number.");
                    }
            }
            try {
                Double createDouble2 = createDouble(substring2);
                if (!createDouble2.isInfinite() && (((double) createDouble2.floatValue()) != 0.0d || z)) {
                    return createDouble2;
                }
            } catch (NumberFormatException e7) {
            }
            try {
                return createBigDecimal(substring2);
            } catch (NumberFormatException e8) {
            }
        }
        throw new NumberFormatException(str + " is not a valid number.");
    }

    private static String getMantissa(String str) {
        return getMantissa(str, str.length());
    }

    private static String getMantissa(String str, int i) {
        boolean z;
        char charAt = str.charAt(0);
        if (charAt == '-' || charAt == '+') {
            z = true;
        } else {
            z = false;
        }
        return z ? str.substring(1, i) : str.substring(0, i);
    }

    private static boolean isAllZeros(String str) {
        if (str == null) {
            return true;
        }
        for (int length = str.length() - 1; length >= 0; length--) {
            if (str.charAt(length) != '0') {
                return false;
            }
        }
        if (str.length() <= 0) {
            return false;
        }
        return true;
    }

    public static Float createFloat(String str) {
        if (str == null) {
            return null;
        }
        return Float.valueOf(str);
    }

    public static Double createDouble(String str) {
        if (str == null) {
            return null;
        }
        return Double.valueOf(str);
    }

    public static Integer createInteger(String str) {
        if (str == null) {
            return null;
        }
        return Integer.decode(str);
    }

    public static Long createLong(String str) {
        if (str == null) {
            return null;
        }
        return Long.decode(str);
    }

    public static BigInteger createBigInteger(String str) {
        boolean z;
        int i;
        int i2;
        if (str == null) {
            return null;
        }
        if (str.startsWith("-")) {
            z = true;
            i = 1;
        } else {
            z = false;
            i = 0;
        }
        if (str.startsWith("0x", i) || str.startsWith("0X", i)) {
            i += 2;
            i2 = 16;
        } else if (str.startsWith("#", i)) {
            i++;
            i2 = 16;
        } else if (!str.startsWith(AppEventsConstants.EVENT_PARAM_VALUE_NO, i) || str.length() <= i + 1) {
            i2 = 10;
        } else {
            i2 = 8;
            i++;
        }
        BigInteger bigInteger = new BigInteger(str.substring(i), i2);
        if (z) {
            return bigInteger.negate();
        }
        return bigInteger;
    }

    public static BigDecimal createBigDecimal(String str) {
        if (str == null) {
            return null;
        }
        if (StringUtils.isBlank(str)) {
            throw new NumberFormatException("A blank string is not a valid number");
        } else if (!str.trim().startsWith("--")) {
            return new BigDecimal(str);
        } else {
            throw new NumberFormatException(str + " is not a valid number.");
        }
    }

    public static long min(long... jArr) {
        validateArray(jArr);
        long j = jArr[0];
        for (int i = 1; i < jArr.length; i++) {
            if (jArr[i] < j) {
                j = jArr[i];
            }
        }
        return j;
    }

    public static int min(int... iArr) {
        validateArray(iArr);
        int i = iArr[0];
        for (int i2 = 1; i2 < iArr.length; i2++) {
            if (iArr[i2] < i) {
                i = iArr[i2];
            }
        }
        return i;
    }

    public static short min(short... sArr) {
        validateArray(sArr);
        short s = sArr[0];
        for (int i = 1; i < sArr.length; i++) {
            if (sArr[i] < s) {
                s = sArr[i];
            }
        }
        return s;
    }

    public static byte min(byte... bArr) {
        validateArray(bArr);
        byte b = bArr[0];
        for (int i = 1; i < bArr.length; i++) {
            if (bArr[i] < b) {
                b = bArr[i];
            }
        }
        return b;
    }

    public static double min(double... dArr) {
        validateArray(dArr);
        double d = dArr[0];
        for (int i = 1; i < dArr.length; i++) {
            if (Double.isNaN(dArr[i])) {
                return Double.NaN;
            }
            if (dArr[i] < d) {
                d = dArr[i];
            }
        }
        return d;
    }

    public static float min(float... fArr) {
        validateArray(fArr);
        float f = fArr[0];
        for (int i = 1; i < fArr.length; i++) {
            if (Float.isNaN(fArr[i])) {
                return Float.NaN;
            }
            if (fArr[i] < f) {
                f = fArr[i];
            }
        }
        return f;
    }

    public static long max(long... jArr) {
        validateArray(jArr);
        long j = jArr[0];
        for (int i = 1; i < jArr.length; i++) {
            if (jArr[i] > j) {
                j = jArr[i];
            }
        }
        return j;
    }

    public static int max(int... iArr) {
        validateArray(iArr);
        int i = iArr[0];
        for (int i2 = 1; i2 < iArr.length; i2++) {
            if (iArr[i2] > i) {
                i = iArr[i2];
            }
        }
        return i;
    }

    public static short max(short... sArr) {
        validateArray(sArr);
        short s = sArr[0];
        for (int i = 1; i < sArr.length; i++) {
            if (sArr[i] > s) {
                s = sArr[i];
            }
        }
        return s;
    }

    public static byte max(byte... bArr) {
        validateArray(bArr);
        byte b = bArr[0];
        for (int i = 1; i < bArr.length; i++) {
            if (bArr[i] > b) {
                b = bArr[i];
            }
        }
        return b;
    }

    public static double max(double... dArr) {
        validateArray(dArr);
        double d = dArr[0];
        for (int i = 1; i < dArr.length; i++) {
            if (Double.isNaN(dArr[i])) {
                return Double.NaN;
            }
            if (dArr[i] > d) {
                d = dArr[i];
            }
        }
        return d;
    }

    public static float max(float... fArr) {
        validateArray(fArr);
        float f = fArr[0];
        for (int i = 1; i < fArr.length; i++) {
            if (Float.isNaN(fArr[i])) {
                return Float.NaN;
            }
            if (fArr[i] > f) {
                f = fArr[i];
            }
        }
        return f;
    }

    private static void validateArray(Object obj) {
        boolean z;
        if (obj == null) {
            throw new IllegalArgumentException("The Array must not be null");
        }
        if (Array.getLength(obj) != 0) {
            z = true;
        } else {
            z = false;
        }
        Validate.isTrue(z, "Array cannot be empty.", new Object[0]);
    }

    public static long min(long j, long j2, long j3) {
        long j4;
        if (j2 < j) {
            j4 = j2;
        } else {
            j4 = j;
        }
        return j3 < j4 ? j3 : j4;
    }

    public static int min(int i, int i2, int i3) {
        int i4;
        if (i2 < i) {
            i4 = i2;
        } else {
            i4 = i;
        }
        return i3 < i4 ? i3 : i4;
    }

    public static short min(short s, short s2, short s3) {
        short s4;
        if (s2 < s) {
            s4 = s2;
        } else {
            s4 = s;
        }
        return s3 < s4 ? s3 : s4;
    }

    public static byte min(byte b, byte b2, byte b3) {
        byte b4;
        if (b2 < b) {
            b4 = b2;
        } else {
            b4 = b;
        }
        return b3 < b4 ? b3 : b4;
    }

    public static double min(double d, double d2, double d3) {
        return Math.min(Math.min(d, d2), d3);
    }

    public static float min(float f, float f2, float f3) {
        return Math.min(Math.min(f, f2), f3);
    }

    public static long max(long j, long j2, long j3) {
        long j4;
        if (j2 > j) {
            j4 = j2;
        } else {
            j4 = j;
        }
        return j3 > j4 ? j3 : j4;
    }

    public static int max(int i, int i2, int i3) {
        int i4;
        if (i2 > i) {
            i4 = i2;
        } else {
            i4 = i;
        }
        return i3 > i4 ? i3 : i4;
    }

    public static short max(short s, short s2, short s3) {
        short s4;
        if (s2 > s) {
            s4 = s2;
        } else {
            s4 = s;
        }
        return s3 > s4 ? s3 : s4;
    }

    public static byte max(byte b, byte b2, byte b3) {
        byte b4;
        if (b2 > b) {
            b4 = b2;
        } else {
            b4 = b;
        }
        return b3 > b4 ? b3 : b4;
    }

    public static double max(double d, double d2, double d3) {
        return Math.max(Math.max(d, d2), d3);
    }

    public static float max(float f, float f2, float f3) {
        return Math.max(Math.max(f, f2), f3);
    }

    public static boolean isDigits(String str) {
        if (StringUtils.isEmpty(str)) {
            return false;
        }
        for (int i = 0; i < str.length(); i++) {
            if (!Character.isDigit(str.charAt(i))) {
                return false;
            }
        }
        return true;
    }

    public static boolean isNumber(String str) {
        boolean z = true;
        if (StringUtils.isEmpty(str)) {
            return false;
        }
        char[] charArray = str.toCharArray();
        int length = charArray.length;
        int i = charArray[0] == '-' ? 1 : 0;
        if (length > i + 1 && charArray[i] == '0') {
            if (charArray[i + 1] == 'x' || charArray[i + 1] == 'X') {
                int i2 = i + 2;
                if (i2 == length) {
                    return false;
                }
                while (i2 < charArray.length) {
                    if ((charArray[i2] < '0' || charArray[i2] > '9') && ((charArray[i2] < 'a' || charArray[i2] > 'f') && (charArray[i2] < 'A' || charArray[i2] > 'F'))) {
                        return false;
                    }
                    i2++;
                }
                return true;
            } else if (Character.isDigit(charArray[i + 1])) {
                for (int i3 = i + 1; i3 < charArray.length; i3++) {
                    if (charArray[i3] < '0' || charArray[i3] > '7') {
                        return false;
                    }
                }
                return true;
            }
        }
        int i4 = length - 1;
        boolean z2 = false;
        boolean z3 = false;
        boolean z4 = false;
        boolean z5 = false;
        while (true) {
            if (i < i4 || (i < i4 + 1 && z3 && !z2)) {
                if (charArray[i] >= '0' && charArray[i] <= '9') {
                    z2 = true;
                    z3 = false;
                } else if (charArray[i] == '.') {
                    if (z4 || z5) {
                        return false;
                    }
                    z4 = true;
                } else if (charArray[i] == 'e' || charArray[i] == 'E') {
                    if (z5 || !z2) {
                        return false;
                    }
                    z3 = true;
                    z5 = true;
                } else if ((charArray[i] != '+' && charArray[i] != '-') || !z3) {
                    return false;
                } else {
                    z2 = false;
                    z3 = false;
                }
                i++;
            }
        }
        if (i >= charArray.length) {
            if (z3 || !z2) {
                z = false;
            }
            return z;
        } else if (charArray[i] >= '0' && charArray[i] <= '9') {
            return true;
        } else {
            if (charArray[i] == 'e' || charArray[i] == 'E') {
                return false;
            }
            if (charArray[i] == '.') {
                if (z4 || z5) {
                    return false;
                }
                return z2;
            } else if (!z3 && (charArray[i] == 'd' || charArray[i] == 'D' || charArray[i] == 'f' || charArray[i] == 'F')) {
                return z2;
            } else {
                if (charArray[i] != 'l' && charArray[i] != 'L') {
                    return false;
                }
                if (!z2 || z5 || z4) {
                    z = false;
                }
                return z;
            }
        }
    }

    public static boolean isParsable(String str) {
        if (StringUtils.endsWith(str, AbstractIntegrationSupport.DEFAULT_EVENT_NAME_DELIMITER)) {
            return false;
        }
        if (StringUtils.startsWith(str, "-")) {
            return isDigits(StringUtils.replaceOnce(str.substring(1), AbstractIntegrationSupport.DEFAULT_EVENT_NAME_DELIMITER, ""));
        }
        return isDigits(StringUtils.replaceOnce(str, AbstractIntegrationSupport.DEFAULT_EVENT_NAME_DELIMITER, ""));
    }

    public static int compare(int i, int i2) {
        if (i == i2) {
            return 0;
        }
        if (i < i2) {
            return -1;
        }
        return 1;
    }

    public static int compare(long j, long j2) {
        if (j == j2) {
            return 0;
        }
        if (j < j2) {
            return -1;
        }
        return 1;
    }

    public static int compare(short s, short s2) {
        if (s == s2) {
            return 0;
        }
        if (s < s2) {
            return -1;
        }
        return 1;
    }

    public static int compare(byte b, byte b2) {
        return b - b2;
    }
}
