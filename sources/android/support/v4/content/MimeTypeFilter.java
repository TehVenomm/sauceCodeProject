package android.support.p000v4.content;

import android.support.annotation.NonNull;
import android.support.annotation.Nullable;
import com.appsflyer.share.Constants;
import java.util.ArrayList;

/* renamed from: android.support.v4.content.MimeTypeFilter */
public final class MimeTypeFilter {
    private MimeTypeFilter() {
    }

    public static String matches(@Nullable String str, @NonNull String[] strArr) {
        if (str == null) {
            return null;
        }
        String[] split = str.split(Constants.URL_PATH_DELIMITER);
        for (String str2 : strArr) {
            if (mimeTypeAgainstFilter(split, str2.split(Constants.URL_PATH_DELIMITER))) {
                return str2;
            }
        }
        return null;
    }

    public static String matches(@Nullable String[] strArr, @NonNull String str) {
        if (strArr == null) {
            return null;
        }
        String[] split = str.split(Constants.URL_PATH_DELIMITER);
        for (String str2 : strArr) {
            if (mimeTypeAgainstFilter(str2.split(Constants.URL_PATH_DELIMITER), split)) {
                return str2;
            }
        }
        return null;
    }

    public static boolean matches(@Nullable String str, @NonNull String str2) {
        if (str == null) {
            return false;
        }
        return mimeTypeAgainstFilter(str.split(Constants.URL_PATH_DELIMITER), str2.split(Constants.URL_PATH_DELIMITER));
    }

    public static String[] matchesMany(@Nullable String[] strArr, @NonNull String str) {
        if (strArr == null) {
            return new String[0];
        }
        ArrayList arrayList = new ArrayList();
        String[] split = str.split(Constants.URL_PATH_DELIMITER);
        for (String str2 : strArr) {
            if (mimeTypeAgainstFilter(str2.split(Constants.URL_PATH_DELIMITER), split)) {
                arrayList.add(str2);
            }
        }
        return (String[]) arrayList.toArray(new String[arrayList.size()]);
    }

    private static boolean mimeTypeAgainstFilter(@NonNull String[] strArr, @NonNull String[] strArr2) {
        if (strArr2.length != 2) {
            throw new IllegalArgumentException("Ill-formatted MIME type filter. Must be type/subtype.");
        } else if (strArr2[0].isEmpty() || strArr2[1].isEmpty()) {
            throw new IllegalArgumentException("Ill-formatted MIME type filter. Type or subtype empty.");
        } else if (strArr.length != 2) {
            return false;
        } else {
            if ("*".equals(strArr2[0]) || strArr2[0].equals(strArr[0])) {
                return "*".equals(strArr2[1]) || strArr2[1].equals(strArr[1]);
            }
            return false;
        }
    }
}
