package net.gogame.gowrap.support;

import android.os.Build.VERSION;
import android.text.Html;
import java.util.ArrayList;
import java.util.List;

public final class StringUtils {
    private StringUtils() {
    }

    public static String trimToNull(String str) {
        if (str == null) {
            return null;
        }
        String trim = str.trim();
        if (trim.length() != 0) {
            return trim;
        }
        return null;
    }

    public static String[] split(String str, String str2) {
        if (str == null) {
            return null;
        }
        int i = 0;
        List arrayList = new ArrayList();
        while (i < str.length()) {
            int indexOf = str.indexOf(str2, i);
            if (indexOf <= i) {
                if (indexOf != i) {
                    arrayList.add(str.substring(i));
                    break;
                }
                i = str2.length() + indexOf;
            } else {
                arrayList.add(str.substring(i, indexOf));
                i = str2.length() + indexOf;
            }
        }
        if (arrayList.isEmpty()) {
            return null;
        }
        return (String[]) arrayList.toArray(new String[arrayList.size()]);
    }

    public static boolean isEquals(String str, String str2) {
        if (str == null && str2 == null) {
            return true;
        }
        if (str != null && str2 == null) {
            return false;
        }
        if (str != null || str2 == null) {
            return str.equals(str2);
        }
        return false;
    }

    public static boolean startsWith(String str, String str2) {
        if (str == null || str2 == null) {
            return false;
        }
        return str.startsWith(str2);
    }

    public static boolean endsWith(String str, String str2) {
        if (str == null || str2 == null) {
            return false;
        }
        return str.endsWith(str2);
    }

    public static String escapeHtml(String str) {
        if (str != null && VERSION.SDK_INT >= 16) {
            return Html.escapeHtml(str);
        }
        return str;
    }
}
