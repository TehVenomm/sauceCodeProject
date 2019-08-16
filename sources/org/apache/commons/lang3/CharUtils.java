package org.apache.commons.lang3;

public class CharUtils {
    private static final String[] CHAR_STRING_ARRAY = new String[128];

    /* renamed from: CR */
    public static final char f1421CR = '\r';
    private static final char[] HEX_DIGITS = {'0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f'};

    /* renamed from: LF */
    public static final char f1422LF = '\n';

    static {
        for (char c = 0; c < CHAR_STRING_ARRAY.length; c = (char) (c + 1)) {
            CHAR_STRING_ARRAY[c] = String.valueOf(c);
        }
    }

    @Deprecated
    public static Character toCharacterObject(char c) {
        return Character.valueOf(c);
    }

    public static Character toCharacterObject(String str) {
        if (StringUtils.isEmpty(str)) {
            return null;
        }
        return Character.valueOf(str.charAt(0));
    }

    public static char toChar(Character ch) {
        if (ch != null) {
            return ch.charValue();
        }
        throw new IllegalArgumentException("The Character must not be null");
    }

    public static char toChar(Character ch, char c) {
        return ch == null ? c : ch.charValue();
    }

    public static char toChar(String str) {
        if (!StringUtils.isEmpty(str)) {
            return str.charAt(0);
        }
        throw new IllegalArgumentException("The String must not be empty");
    }

    public static char toChar(String str, char c) {
        return StringUtils.isEmpty(str) ? c : str.charAt(0);
    }

    public static int toIntValue(char c) {
        if (isAsciiNumeric(c)) {
            return c - '0';
        }
        throw new IllegalArgumentException("The character " + c + " is not in the range '0' - '9'");
    }

    public static int toIntValue(char c, int i) {
        return !isAsciiNumeric(c) ? i : c - '0';
    }

    public static int toIntValue(Character ch) {
        if (ch != null) {
            return toIntValue(ch.charValue());
        }
        throw new IllegalArgumentException("The character must not be null");
    }

    public static int toIntValue(Character ch, int i) {
        return ch == null ? i : toIntValue(ch.charValue(), i);
    }

    public static String toString(char c) {
        if (c < 128) {
            return CHAR_STRING_ARRAY[c];
        }
        return new String(new char[]{c});
    }

    public static String toString(Character ch) {
        if (ch == null) {
            return null;
        }
        return toString(ch.charValue());
    }

    public static String unicodeEscaped(char c) {
        StringBuilder sb = new StringBuilder(6);
        sb.append("\\u");
        sb.append(HEX_DIGITS[(c >> 12) & 15]);
        sb.append(HEX_DIGITS[(c >> 8) & 15]);
        sb.append(HEX_DIGITS[(c >> 4) & 15]);
        sb.append(HEX_DIGITS[c & 15]);
        return sb.toString();
    }

    public static String unicodeEscaped(Character ch) {
        if (ch == null) {
            return null;
        }
        return unicodeEscaped(ch.charValue());
    }

    public static boolean isAscii(char c) {
        return c < 128;
    }

    public static boolean isAsciiPrintable(char c) {
        return c >= ' ' && c < 127;
    }

    public static boolean isAsciiControl(char c) {
        return c < ' ' || c == 127;
    }

    public static boolean isAsciiAlpha(char c) {
        return isAsciiAlphaUpper(c) || isAsciiAlphaLower(c);
    }

    public static boolean isAsciiAlphaUpper(char c) {
        return c >= 'A' && c <= 'Z';
    }

    public static boolean isAsciiAlphaLower(char c) {
        return c >= 'a' && c <= 'z';
    }

    public static boolean isAsciiNumeric(char c) {
        return c >= '0' && c <= '9';
    }

    public static boolean isAsciiAlphanumeric(char c) {
        return isAsciiAlpha(c) || isAsciiNumeric(c);
    }

    public static int compare(char c, char c2) {
        return c - c2;
    }
}
