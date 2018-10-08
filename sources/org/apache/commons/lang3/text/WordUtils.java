package org.apache.commons.lang3.text;

import org.apache.commons.lang3.StringUtils;
import org.apache.commons.lang3.SystemUtils;

public class WordUtils {
    public static String wrap(String str, int i) {
        return wrap(str, i, null, false);
    }

    public static String wrap(String str, int i, String str2, boolean z) {
        if (str == null) {
            return null;
        }
        if (str2 == null) {
            str2 = SystemUtils.LINE_SEPARATOR;
        }
        if (i < 1) {
            i = 1;
        }
        int length = str.length();
        int i2 = 0;
        StringBuilder stringBuilder = new StringBuilder(length + 32);
        while (i2 < length) {
            if (str.charAt(i2) == ' ') {
                i2++;
            } else if (length - i2 <= i) {
                break;
            } else {
                int lastIndexOf = str.lastIndexOf(32, i + i2);
                if (lastIndexOf >= i2) {
                    stringBuilder.append(str.substring(i2, lastIndexOf));
                    stringBuilder.append(str2);
                    i2 = lastIndexOf + 1;
                } else if (z) {
                    stringBuilder.append(str.substring(i2, i + i2));
                    stringBuilder.append(str2);
                    i2 += i;
                } else {
                    lastIndexOf = str.indexOf(32, i + i2);
                    if (lastIndexOf >= 0) {
                        stringBuilder.append(str.substring(i2, lastIndexOf));
                        stringBuilder.append(str2);
                        i2 = lastIndexOf + 1;
                    } else {
                        stringBuilder.append(str.substring(i2));
                        i2 = length;
                    }
                }
            }
        }
        stringBuilder.append(str.substring(i2));
        return stringBuilder.toString();
    }

    public static String capitalize(String str) {
        return capitalize(str, null);
    }

    public static String capitalize(String str, char... cArr) {
        int length = cArr == null ? -1 : cArr.length;
        if (StringUtils.isEmpty(str) || length == 0) {
            return str;
        }
        char[] toCharArray = str.toCharArray();
        Object obj = 1;
        for (length = 0; length < toCharArray.length; length++) {
            char c = toCharArray[length];
            if (isDelimiter(c, cArr)) {
                obj = 1;
            } else if (obj != null) {
                toCharArray[length] = Character.toTitleCase(c);
                obj = null;
            }
        }
        return new String(toCharArray);
    }

    public static String capitalizeFully(String str) {
        return capitalizeFully(str, null);
    }

    public static String capitalizeFully(String str, char... cArr) {
        return (StringUtils.isEmpty(str) || (cArr == null ? -1 : cArr.length) == 0) ? str : capitalize(str.toLowerCase(), cArr);
    }

    public static String uncapitalize(String str) {
        return uncapitalize(str, null);
    }

    public static String uncapitalize(String str, char... cArr) {
        int length = cArr == null ? -1 : cArr.length;
        if (StringUtils.isEmpty(str) || length == 0) {
            return str;
        }
        char[] toCharArray = str.toCharArray();
        Object obj = 1;
        for (length = 0; length < toCharArray.length; length++) {
            char c = toCharArray[length];
            if (isDelimiter(c, cArr)) {
                obj = 1;
            } else if (obj != null) {
                toCharArray[length] = Character.toLowerCase(c);
                obj = null;
            }
        }
        return new String(toCharArray);
    }

    public static String swapCase(String str) {
        if (StringUtils.isEmpty(str)) {
            return str;
        }
        char[] toCharArray = str.toCharArray();
        boolean z = true;
        for (int i = 0; i < toCharArray.length; i++) {
            char c = toCharArray[i];
            if (Character.isUpperCase(c)) {
                toCharArray[i] = Character.toLowerCase(c);
                z = false;
            } else if (Character.isTitleCase(c)) {
                toCharArray[i] = Character.toLowerCase(c);
                z = false;
            } else if (!Character.isLowerCase(c)) {
                z = Character.isWhitespace(c);
            } else if (z) {
                toCharArray[i] = Character.toTitleCase(c);
                z = false;
            } else {
                toCharArray[i] = Character.toUpperCase(c);
            }
        }
        return new String(toCharArray);
    }

    public static String initials(String str) {
        return initials(str, null);
    }

    public static String initials(String str, char... cArr) {
        if (StringUtils.isEmpty(str)) {
            return str;
        }
        if (cArr != null && cArr.length == 0) {
            return "";
        }
        int length = str.length();
        char[] cArr2 = new char[((length / 2) + 1)];
        int i = 1;
        int i2 = 0;
        for (int i3 = 0; i3 < length; i3++) {
            char charAt = str.charAt(i3);
            if (isDelimiter(charAt, cArr)) {
                i = 1;
            } else if (i != 0) {
                i = i2 + 1;
                cArr2[i2] = charAt;
                i2 = i;
                i = 0;
            }
        }
        return new String(cArr2, 0, i2);
    }

    private static boolean isDelimiter(char c, char[] cArr) {
        if (cArr == null) {
            return Character.isWhitespace(c);
        }
        for (char c2 : cArr) {
            if (c == c2) {
                return true;
            }
        }
        return false;
    }
}
