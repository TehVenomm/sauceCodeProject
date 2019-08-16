package net.gogame.gowrap.p019ui.utils;

import android.view.View.MeasureSpec;
import java.util.Locale;

/* renamed from: net.gogame.gowrap.ui.utils.UIDebugUtils */
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
        return String.format(Locale.ENGLISH, "%s %d", new Object[]{getModeName(MeasureSpec.getMode(i)), Integer.valueOf(MeasureSpec.getSize(i))});
    }
}
