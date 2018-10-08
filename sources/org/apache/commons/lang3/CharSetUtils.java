package org.apache.commons.lang3;

public class CharSetUtils {
    public static String squeeze(String str, String... strArr) {
        if (StringUtils.isEmpty(str) || deepEmpty(strArr)) {
            return str;
        }
        CharSet instance = CharSet.getInstance(strArr);
        StringBuilder stringBuilder = new StringBuilder(str.length());
        char[] toCharArray = str.toCharArray();
        int length = toCharArray.length;
        int i = 0;
        char c = ' ';
        while (i < length) {
            char c2 = toCharArray[i];
            if (c2 != c || i == 0 || !instance.contains(c2)) {
                stringBuilder.append(c2);
                c = c2;
            }
            i++;
        }
        return stringBuilder.toString();
    }

    public static boolean containsAny(String str, String... strArr) {
        if (StringUtils.isEmpty(str) || deepEmpty(strArr)) {
            return false;
        }
        CharSet instance = CharSet.getInstance(strArr);
        for (char contains : str.toCharArray()) {
            if (instance.contains(contains)) {
                return true;
            }
        }
        return false;
    }

    public static int count(String str, String... strArr) {
        int i = 0;
        if (!(StringUtils.isEmpty(str) || deepEmpty(strArr))) {
            CharSet instance = CharSet.getInstance(strArr);
            for (char contains : str.toCharArray()) {
                if (instance.contains(contains)) {
                    i++;
                }
            }
        }
        return i;
    }

    public static String keep(String str, String... strArr) {
        if (str == null) {
            return null;
        }
        if (str.isEmpty() || deepEmpty(strArr)) {
            return "";
        }
        return modify(str, strArr, true);
    }

    public static String delete(String str, String... strArr) {
        return (StringUtils.isEmpty(str) || deepEmpty(strArr)) ? str : modify(str, strArr, false);
    }

    private static String modify(String str, String[] strArr, boolean z) {
        CharSet instance = CharSet.getInstance(strArr);
        StringBuilder stringBuilder = new StringBuilder(str.length());
        char[] toCharArray = str.toCharArray();
        int length = toCharArray.length;
        for (int i = 0; i < length; i++) {
            if (instance.contains(toCharArray[i]) == z) {
                stringBuilder.append(toCharArray[i]);
            }
        }
        return stringBuilder.toString();
    }

    private static boolean deepEmpty(String[] strArr) {
        if (strArr != null) {
            for (CharSequence isNotEmpty : strArr) {
                if (StringUtils.isNotEmpty(isNotEmpty)) {
                    return false;
                }
            }
        }
        return true;
    }
}
