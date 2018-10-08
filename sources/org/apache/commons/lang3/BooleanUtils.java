package org.apache.commons.lang3;

import com.facebook.internal.ServerProtocol;
import org.apache.commons.lang3.math.NumberUtils;

public class BooleanUtils {
    public static Boolean negate(Boolean bool) {
        if (bool == null) {
            return null;
        }
        return bool.booleanValue() ? Boolean.FALSE : Boolean.TRUE;
    }

    public static boolean isTrue(Boolean bool) {
        return Boolean.TRUE.equals(bool);
    }

    public static boolean isNotTrue(Boolean bool) {
        return !isTrue(bool);
    }

    public static boolean isFalse(Boolean bool) {
        return Boolean.FALSE.equals(bool);
    }

    public static boolean isNotFalse(Boolean bool) {
        return !isFalse(bool);
    }

    public static boolean toBoolean(Boolean bool) {
        return bool != null && bool.booleanValue();
    }

    public static boolean toBooleanDefaultIfNull(Boolean bool, boolean z) {
        return bool == null ? z : bool.booleanValue();
    }

    public static boolean toBoolean(int i) {
        return i != 0;
    }

    public static Boolean toBooleanObject(int i) {
        return i == 0 ? Boolean.FALSE : Boolean.TRUE;
    }

    public static Boolean toBooleanObject(Integer num) {
        if (num == null) {
            return null;
        }
        return num.intValue() == 0 ? Boolean.FALSE : Boolean.TRUE;
    }

    public static boolean toBoolean(int i, int i2, int i3) {
        if (i == i2) {
            return true;
        }
        if (i == i3) {
            return false;
        }
        throw new IllegalArgumentException("The Integer did not match either specified value");
    }

    public static boolean toBoolean(Integer num, Integer num2, Integer num3) {
        if (num == null) {
            if (num2 == null) {
                return true;
            }
            if (num3 == null) {
                return false;
            }
        } else if (num.equals(num2)) {
            return true;
        } else {
            if (num.equals(num3)) {
                return false;
            }
        }
        throw new IllegalArgumentException("The Integer did not match either specified value");
    }

    public static Boolean toBooleanObject(int i, int i2, int i3, int i4) {
        if (i == i2) {
            return Boolean.TRUE;
        }
        if (i == i3) {
            return Boolean.FALSE;
        }
        if (i == i4) {
            return null;
        }
        throw new IllegalArgumentException("The Integer did not match any specified value");
    }

    public static Boolean toBooleanObject(Integer num, Integer num2, Integer num3, Integer num4) {
        if (num == null) {
            if (num2 == null) {
                return Boolean.TRUE;
            }
            if (num3 == null) {
                return Boolean.FALSE;
            }
            if (num4 == null) {
                return null;
            }
        } else if (num.equals(num2)) {
            return Boolean.TRUE;
        } else {
            if (num.equals(num3)) {
                return Boolean.FALSE;
            }
            if (num.equals(num4)) {
                return null;
            }
        }
        throw new IllegalArgumentException("The Integer did not match any specified value");
    }

    public static int toInteger(boolean z) {
        return z ? 1 : 0;
    }

    public static Integer toIntegerObject(boolean z) {
        return z ? NumberUtils.INTEGER_ONE : NumberUtils.INTEGER_ZERO;
    }

    public static Integer toIntegerObject(Boolean bool) {
        if (bool == null) {
            return null;
        }
        return bool.booleanValue() ? NumberUtils.INTEGER_ONE : NumberUtils.INTEGER_ZERO;
    }

    public static int toInteger(boolean z, int i, int i2) {
        return z ? i : i2;
    }

    public static int toInteger(Boolean bool, int i, int i2, int i3) {
        if (bool == null) {
            return i3;
        }
        if (!bool.booleanValue()) {
            i = i2;
        }
        return i;
    }

    public static Integer toIntegerObject(boolean z, Integer num, Integer num2) {
        return z ? num : num2;
    }

    public static Integer toIntegerObject(Boolean bool, Integer num, Integer num2, Integer num3) {
        if (bool == null) {
            return num3;
        }
        if (!bool.booleanValue()) {
            num = num2;
        }
        return num;
    }

    public static Boolean toBooleanObject(String str) {
        if (str == ServerProtocol.DIALOG_RETURN_SCOPES_TRUE) {
            return Boolean.TRUE;
        }
        if (str == null) {
            return null;
        }
        char charAt;
        char charAt2;
        char charAt3;
        char charAt4;
        switch (str.length()) {
            case 1:
                charAt = str.charAt(0);
                if (charAt == 'y' || charAt == 'Y' || charAt == 't' || charAt == 'T') {
                    return Boolean.TRUE;
                }
                if (charAt == 'n' || charAt == 'N' || charAt == 'f' || charAt == 'F') {
                    return Boolean.FALSE;
                }
                break;
            case 2:
                charAt = str.charAt(0);
                charAt2 = str.charAt(1);
                if ((charAt == 'o' || charAt == 'O') && (charAt2 == 'n' || charAt2 == 'N')) {
                    return Boolean.TRUE;
                }
                if ((charAt == 'n' || charAt == 'N') && (charAt2 == 'o' || charAt2 == 'O')) {
                    return Boolean.FALSE;
                }
                break;
            case 3:
                charAt = str.charAt(0);
                charAt2 = str.charAt(1);
                charAt3 = str.charAt(2);
                if ((charAt == 'y' || charAt == 'Y') && ((charAt2 == 'e' || charAt2 == 'E') && (charAt3 == 's' || charAt3 == 'S'))) {
                    return Boolean.TRUE;
                }
                if ((charAt == 'o' || charAt == 'O') && ((charAt2 == 'f' || charAt2 == 'F') && (charAt3 == 'f' || charAt3 == 'F'))) {
                    return Boolean.FALSE;
                }
                break;
            case 4:
                charAt = str.charAt(0);
                charAt2 = str.charAt(1);
                charAt3 = str.charAt(2);
                charAt4 = str.charAt(3);
                if ((charAt == 't' || charAt == 'T') && ((charAt2 == 'r' || charAt2 == 'R') && ((charAt3 == 'u' || charAt3 == 'U') && (charAt4 == 'e' || charAt4 == 'E')))) {
                    return Boolean.TRUE;
                }
            case 5:
                charAt = str.charAt(0);
                charAt2 = str.charAt(1);
                charAt3 = str.charAt(2);
                charAt4 = str.charAt(3);
                char charAt5 = str.charAt(4);
                if ((charAt == 'f' || charAt == 'F') && ((charAt2 == 'a' || charAt2 == 'A') && ((charAt3 == 'l' || charAt3 == 'L') && ((charAt4 == 's' || charAt4 == 'S') && (charAt5 == 'e' || charAt5 == 'E'))))) {
                    return Boolean.FALSE;
                }
        }
        return null;
    }

    public static Boolean toBooleanObject(String str, String str2, String str3, String str4) {
        if (str == null) {
            if (str2 == null) {
                return Boolean.TRUE;
            }
            if (str3 == null) {
                return Boolean.FALSE;
            }
            if (str4 == null) {
                return null;
            }
        } else if (str.equals(str2)) {
            return Boolean.TRUE;
        } else {
            if (str.equals(str3)) {
                return Boolean.FALSE;
            }
            if (str.equals(str4)) {
                return null;
            }
        }
        throw new IllegalArgumentException("The String did not match any specified value");
    }

    public static boolean toBoolean(String str) {
        return toBooleanObject(str) == Boolean.TRUE;
    }

    public static boolean toBoolean(String str, String str2, String str3) {
        if (str == str2) {
            return true;
        }
        if (str == str3) {
            return false;
        }
        if (str != null) {
            if (str.equals(str2)) {
                return true;
            }
            if (str.equals(str3)) {
                return false;
            }
        }
        throw new IllegalArgumentException("The String did not match either specified value");
    }

    public static String toStringTrueFalse(Boolean bool) {
        return toString(bool, ServerProtocol.DIALOG_RETURN_SCOPES_TRUE, "false", null);
    }

    public static String toStringOnOff(Boolean bool) {
        return toString(bool, "on", "off", null);
    }

    public static String toStringYesNo(Boolean bool) {
        return toString(bool, "yes", "no", null);
    }

    public static String toString(Boolean bool, String str, String str2, String str3) {
        if (bool == null) {
            return str3;
        }
        if (!bool.booleanValue()) {
            str = str2;
        }
        return str;
    }

    public static String toStringTrueFalse(boolean z) {
        return toString(z, ServerProtocol.DIALOG_RETURN_SCOPES_TRUE, "false");
    }

    public static String toStringOnOff(boolean z) {
        return toString(z, "on", "off");
    }

    public static String toStringYesNo(boolean z) {
        return toString(z, "yes", "no");
    }

    public static String toString(boolean z, String str, String str2) {
        return z ? str : str2;
    }

    public static boolean and(boolean... zArr) {
        if (zArr == null) {
            throw new IllegalArgumentException("The Array must not be null");
        } else if (zArr.length == 0) {
            throw new IllegalArgumentException("Array is empty");
        } else {
            for (boolean z : zArr) {
                if (!z) {
                    return false;
                }
            }
            return true;
        }
    }

    public static Boolean and(Boolean... boolArr) {
        if (boolArr == null) {
            throw new IllegalArgumentException("The Array must not be null");
        } else if (boolArr.length == 0) {
            throw new IllegalArgumentException("Array is empty");
        } else {
            try {
                return and(ArrayUtils.toPrimitive(boolArr)) ? Boolean.TRUE : Boolean.FALSE;
            } catch (NullPointerException e) {
                throw new IllegalArgumentException("The array must not contain any null elements");
            }
        }
    }

    public static boolean or(boolean... zArr) {
        if (zArr == null) {
            throw new IllegalArgumentException("The Array must not be null");
        } else if (zArr.length == 0) {
            throw new IllegalArgumentException("Array is empty");
        } else {
            for (boolean z : zArr) {
                if (z) {
                    return true;
                }
            }
            return false;
        }
    }

    public static Boolean or(Boolean... boolArr) {
        if (boolArr == null) {
            throw new IllegalArgumentException("The Array must not be null");
        } else if (boolArr.length == 0) {
            throw new IllegalArgumentException("Array is empty");
        } else {
            try {
                return or(ArrayUtils.toPrimitive(boolArr)) ? Boolean.TRUE : Boolean.FALSE;
            } catch (NullPointerException e) {
                throw new IllegalArgumentException("The array must not contain any null elements");
            }
        }
    }

    public static boolean xor(boolean... zArr) {
        int i = 0;
        if (zArr == null) {
            throw new IllegalArgumentException("The Array must not be null");
        } else if (zArr.length == 0) {
            throw new IllegalArgumentException("Array is empty");
        } else {
            boolean z = false;
            while (i < zArr.length) {
                z ^= zArr[i];
                i++;
            }
            return z;
        }
    }

    public static Boolean xor(Boolean... boolArr) {
        if (boolArr == null) {
            throw new IllegalArgumentException("The Array must not be null");
        } else if (boolArr.length == 0) {
            throw new IllegalArgumentException("Array is empty");
        } else {
            try {
                return xor(ArrayUtils.toPrimitive(boolArr)) ? Boolean.TRUE : Boolean.FALSE;
            } catch (NullPointerException e) {
                throw new IllegalArgumentException("The array must not contain any null elements");
            }
        }
    }

    public static int compare(boolean z, boolean z2) {
        if (z == z2) {
            return 0;
        }
        if (z) {
            return 1;
        }
        return -1;
    }
}
