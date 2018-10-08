package org.apache.commons.lang3.text;

import java.util.Formattable;
import java.util.Formatter;
import org.apache.commons.lang3.ObjectUtils;
import org.apache.commons.lang3.Validate;

public class FormattableUtils {
    private static final String SIMPLEST_FORMAT = "%s";

    public static String toString(Formattable formattable) {
        return String.format(SIMPLEST_FORMAT, new Object[]{formattable});
    }

    public static Formatter append(CharSequence charSequence, Formatter formatter, int i, int i2, int i3) {
        return append(charSequence, formatter, i, i2, i3, ' ', null);
    }

    public static Formatter append(CharSequence charSequence, Formatter formatter, int i, int i2, int i3, char c) {
        return append(charSequence, formatter, i, i2, i3, c, null);
    }

    public static Formatter append(CharSequence charSequence, Formatter formatter, int i, int i2, int i3, CharSequence charSequence2) {
        return append(charSequence, formatter, i, i2, i3, ' ', charSequence2);
    }

    public static Formatter append(CharSequence charSequence, Formatter formatter, int i, int i2, int i3, char c, CharSequence charSequence2) {
        int i4 = 1;
        boolean z = charSequence2 == null || i3 < 0 || charSequence2.length() <= i3;
        Validate.isTrue(z, "Specified ellipsis '%1$s' exceeds precision of %2$s", charSequence2, Integer.valueOf(i3));
        StringBuilder stringBuilder = new StringBuilder(charSequence);
        if (i3 >= 0 && i3 < charSequence.length()) {
            CharSequence charSequence3 = (CharSequence) ObjectUtils.defaultIfNull(charSequence2, "");
            stringBuilder.replace(i3 - charSequence3.length(), charSequence.length(), charSequence3.toString());
        }
        if ((i & 1) != 1) {
            i4 = 0;
        }
        for (int length = stringBuilder.length(); length < i2; length++) {
            int i5;
            if (i4 != 0) {
                i5 = length;
            } else {
                i5 = 0;
            }
            stringBuilder.insert(i5, c);
        }
        formatter.format(stringBuilder.toString(), new Object[0]);
        return formatter;
    }
}
