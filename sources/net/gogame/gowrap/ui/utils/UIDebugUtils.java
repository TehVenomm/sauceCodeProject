package net.gogame.gowrap.ui.utils;

import android.view.View.MeasureSpec;
import java.util.Locale;

public class UIDebugUtils {
    public static String getModeName(int i) {
        switch (i) {
            case Integer.MIN_VALUE:
                return "AT_MOST";
            case 0:
                return "UNSPECIFIED";
            case 1073741824:
                return "EXACTLY";
            default:
                return String.valueOf(i);
        }
    }

    public static String measureSpecToString(int i) {
        int mode = MeasureSpec.getMode(i);
        int size = MeasureSpec.getSize(i);
        return String.format(Locale.ENGLISH, "%s %d", new Object[]{getModeName(mode), Integer.valueOf(size)});
    }
}
