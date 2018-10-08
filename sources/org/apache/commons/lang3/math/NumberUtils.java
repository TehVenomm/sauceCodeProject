package org.apache.commons.lang3.math;

import com.facebook.appevents.AppEventsConstants;
import java.lang.reflect.Array;
import java.math.BigDecimal;
import java.math.BigInteger;
import net.gogame.gowrap.integrations.AbstractIntegrationSupport;
import org.apache.commons.lang3.ClassUtils;
import org.apache.commons.lang3.StringUtils;
import org.apache.commons.lang3.Validate;

public class NumberUtils {
    public static final Byte BYTE_MINUS_ONE = Byte.valueOf((byte) -1);
    public static final Byte BYTE_ONE = Byte.valueOf((byte) 1);
    public static final Byte BYTE_ZERO = Byte.valueOf((byte) 0);
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
    public static final Short SHORT_MINUS_ONE = Short.valueOf((short) -1);
    public static final Short SHORT_ONE = Short.valueOf((short) 1);
    public static final Short SHORT_ZERO = Short.valueOf((short) 0);

    public static int toInt(String str) {
        return toInt(str, 0);
    }

    public static int toInt(String str, int i) {
        if (str != null) {
            try {
                i = Integer.parseInt(str);
            } catch (NumberFormatException e) {
            }
        }
        return i;
    }

    public static long toLong(String str) {
        return toLong(str, 0);
    }

    public static long toLong(String str, long j) {
        if (str != null) {
            try {
                j = Long.parseLong(str);
            } catch (NumberFormatException e) {
            }
        }
        return j;
    }

    public static float toFloat(String str) {
        return toFloat(str, 0.0f);
    }

    public static float toFloat(String str, float f) {
        if (str != null) {
            try {
                f = Float.parseFloat(str);
            } catch (NumberFormatException e) {
            }
        }
        return f;
    }

    public static double toDouble(String str) {
        return toDouble(str, 0.0d);
    }

    public static double toDouble(String str, double d) {
        if (str != null) {
            try {
                d = Double.parseDouble(str);
            } catch (NumberFormatException e) {
            }
        }
        return d;
    }

    public static byte toByte(String str) {
        return toByte(str, (byte) 0);
    }

    public static byte toByte(String str, byte b) {
        if (str != null) {
            try {
                b = Byte.parseByte(str);
            } catch (NumberFormatException e) {
            }
        }
        return b;
    }

    public static short toShort(String str) {
        return toShort(str, (short) 0);
    }

    public static short toShort(String str, short s) {
        if (str != null) {
            try {
                s = Short.parseShort(str);
            } catch (NumberFormatException e) {
            }
        }
        return s;
    }

    public static Number createNumber(String str) throws NumberFormatException {
        String str2 = null;
        int i = 0;
        if (str == null) {
            return null;
        }
        if (StringUtils.isBlank(str)) {
            throw new NumberFormatException("A blank string is not a valid number");
        }
        int length;
        for (String str3 : new String[]{"0x", "0X", "-0x", "-0X", "#", "-#"}) {
            if (str.startsWith(str3)) {
                length = str3.length() + 0;
                break;
            }
        }
        length = 0;
        int i2;
        if (length > 0) {
            i2 = length;
            while (length < str.length()) {
                char charAt = str.charAt(length);
                if (charAt != '0') {
                    break;
                }
                i2++;
                length++;
            }
            length = str.length() - i2;
            if (length > 16 || (length == 16 && r1 > '7')) {
                return createBigInteger(str);
            }
            if (length > 8 || (length == 8 && r1 > '7')) {
                return createLong(str);
            }
            return createInteger(str);
        }
        String mantissa;
        String str4;
        char charAt2 = str.charAt(str.length() - 1);
        i2 = str.indexOf(46);
        int indexOf = (str.indexOf(101) + str.indexOf(69)) + 1;
        String substring;
        if (i2 > -1) {
            if (indexOf <= -1) {
                substring = str.substring(i2 + 1);
            } else if (indexOf < i2 || indexOf > str.length()) {
                throw new NumberFormatException(str + " is not a valid number.");
            } else {
                substring = str.substring(i2 + 1, indexOf);
            }
            mantissa = getMantissa(str, i2);
            str4 = substring;
            length = substring.length();
        } else {
            if (indexOf <= -1) {
                substring = getMantissa(str);
            } else if (indexOf > str.length()) {
                throw new NumberFormatException(str + " is not a valid number.");
            } else {
                substring = getMantissa(str, indexOf);
            }
            str4 = null;
            mantissa = substring;
            length = 0;
        }
        Number createFloat;
        if (Character.isDigit(charAt2) || charAt2 == ClassUtils.PACKAGE_SEPARATOR_CHAR) {
            if (indexOf > -1 && indexOf < str.length() - 1) {
                str2 = str.substring(indexOf + 1, str.length());
            }
            if (str4 == null && r3 == null) {
                try {
                    return createInteger(str);
                } catch (NumberFormatException e) {
                    try {
                        return createLong(str);
                    } catch (NumberFormatException e2) {
                        return createBigInteger(str);
                    }
                }
            }
            if (isAllZeros(mantissa) && isAllZeros(r3)) {
                i = 1;
            }
            if (length <= 7) {
                try {
                    createFloat = createFloat(str);
                    if (!(createFloat.isInfinite() || (createFloat.floatValue() == 0.0f && r1 == 0))) {
                        return createFloat;
                    }
                } catch (NumberFormatException e3) {
                }
            }
            if (length <= 16) {
                try {
                    createFloat = createDouble(str);
                    if (!(createFloat.isInfinite() || (createFloat.doubleValue() == 0.0d && r1 == 0))) {
                        return createFloat;
                    }
                } catch (NumberFormatException e4) {
                }
            }
            return createBigDecimal(str);
        }
        if (indexOf > -1 && indexOf < str.length() - 1) {
            str2 = str.substring(indexOf + 1, str.length() - 1);
        }
        String substring2 = str.substring(0, str.length() - 1);
        length = (isAllZeros(mantissa) && isAllZeros(str2)) ? 1 : 0;
        switch (charAt2) {
            case 'D':
            case 'd':
                break;
            case 'F':
            case 'f':
                try {
                    createFloat = createFloat(substring2);
                    if (!(createFloat.isInfinite() || (createFloat.floatValue() == 0.0f && length == 0))) {
                        return createFloat;
                    }
                } catch (NumberFormatException e5) {
                    break;
                }
            case 'L':
            case 'l':
                if (str4 == null && str2 == null && ((substring2.charAt(0) == '-' && isDigits(substring2.substring(1))) || isDigits(substring2))) {
                    try {
                        return createLong(substring2);
                    } catch (NumberFormatException e6) {
                        return createBigInteger(substring2);
                    }
                }
                throw new NumberFormatException(str + " is not a valid number.");
        }
        try {
            createFloat = createDouble(substring2);
            if (!(createFloat.isInfinite() || (((double) createFloat.floatValue()) == 0.0d && length == 0))) {
                return createFloat;
            }
        } catch (NumberFormatException e7) {
        }
        try {
            return createBigDecimal(substring2);
        } catch (NumberFormatException e8) {
            throw new NumberFormatException(str + " is not a valid number.");
        }
    }

    private static String getMantissa(String str) {
        return getMantissa(str, str.length());
    }

    private static String getMantissa(String str, int i) {
        int i2;
        char charAt = str.charAt(0);
        if (charAt == '-' || charAt == '+') {
            i2 = 1;
        } else {
            i2 = 0;
        }
        return i2 != 0 ? str.substring(1, i) : str.substring(0, i);
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
        int i = 1;
        int i2 = 0;
        if (str == null) {
            return null;
        }
        int i3;
        if (str.startsWith("-")) {
            i2 = 1;
        } else {
            i = 0;
        }
        if (str.startsWith("0x", i2) || str.startsWith("0X", i2)) {
            i3 = i2 + 2;
            i2 = 16;
        } else if (str.startsWith("#", i2)) {
            i3 = i2 + 1;
            i2 = 16;
        } else if (!str.startsWith(AppEventsConstants.EVENT_PARAM_VALUE_NO, i2) || str.length() <= i2 + 1) {
            i3 = i2;
            i2 = 10;
        } else {
            i3 = i2 + 1;
            i2 = 8;
        }
        BigInteger bigInteger = new BigInteger(str.substring(i3), i2);
        if (i != 0) {
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
        if (obj == null) {
            throw new IllegalArgumentException("The Array must not be null");
        }
        boolean z;
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
        char[] toCharArray = str.toCharArray();
        int length = toCharArray.length;
        int i = toCharArray[0] == '-' ? 1 : 0;
        if (length > i + 1 && toCharArray[i] == '0') {
            if (toCharArray[i + 1] == 'x' || toCharArray[i + 1] == 'X') {
                i += 2;
                if (i == length) {
                    return false;
                }
                while (i < toCharArray.length) {
                    if ((toCharArray[i] < '0' || toCharArray[i] > '9') && ((toCharArray[i] < 'a' || toCharArray[i] > 'f') && (toCharArray[i] < 'A' || toCharArray[i] > 'F'))) {
                        return false;
                    }
                    i++;
                }
                return true;
            } else if (Character.isDigit(toCharArray[i + 1])) {
                i++;
                while (i < toCharArray.length) {
                    if (toCharArray[i] < '0' || toCharArray[i] > '7') {
                        return false;
                    }
                    i++;
                }
                return true;
            }
        }
        int i2 = length - 1;
        int i3 = i;
        boolean z2 = false;
        boolean z3 = false;
        boolean z4 = false;
        boolean z5 = false;
        while (true) {
            if (i3 < i2 || (i3 < i2 + 1 && z2 && !z5)) {
                if (toCharArray[i3] >= '0' && toCharArray[i3] <= '9') {
                    z5 = true;
                    z2 = false;
                } else if (toCharArray[i3] == ClassUtils.PACKAGE_SEPARATOR_CHAR) {
                    if (z3 || z4) {
                        return false;
                    }
                    z3 = true;
                } else if (toCharArray[i3] == 'e' || toCharArray[i3] == 'E') {
                    if (z4 || !z5) {
                        return false;
                    }
                    z2 = true;
                    z4 = true;
                } else if ((toCharArray[i3] != '+' && toCharArray[i3] != '-') || !z2) {
                    return false;
                } else {
                    z5 = false;
                    z2 = false;
                }
                i3++;
            }
        }
        if (i3 >= toCharArray.length) {
            if (z2 || !z5) {
                z = false;
            }
            return z;
        } else if (toCharArray[i3] >= '0' && toCharArray[i3] <= '9') {
            return true;
        } else {
            if (toCharArray[i3] == 'e' || toCharArray[i3] == 'E') {
                return false;
            }
            if (toCharArray[i3] == ClassUtils.PACKAGE_SEPARATOR_CHAR) {
                if (z3 || z4) {
                    return false;
                }
                return z5;
            } else if (!z2 && (toCharArray[i3] == 'd' || toCharArray[i3] == 'D' || toCharArray[i3] == 'f' || toCharArray[i3] == 'F')) {
                return z5;
            } else {
                if (toCharArray[i3] != 'l' && toCharArray[i3] != 'L') {
                    return false;
                }
                if (!z5 || z4 || z3) {
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
