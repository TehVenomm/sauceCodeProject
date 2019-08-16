package android.support.p000v4.view;

import android.os.Build.VERSION;
import android.view.ViewGroup.MarginLayoutParams;

/* renamed from: android.support.v4.view.MarginLayoutParamsCompat */
public final class MarginLayoutParamsCompat {
    private MarginLayoutParamsCompat() {
    }

    public static int getLayoutDirection(MarginLayoutParams marginLayoutParams) {
        int i = VERSION.SDK_INT >= 17 ? marginLayoutParams.getLayoutDirection() : 0;
        if (i == 0 || i == 1) {
            return i;
        }
        return 0;
    }

    public static int getMarginEnd(MarginLayoutParams marginLayoutParams) {
        return VERSION.SDK_INT >= 17 ? marginLayoutParams.getMarginEnd() : marginLayoutParams.rightMargin;
    }

    public static int getMarginStart(MarginLayoutParams marginLayoutParams) {
        return VERSION.SDK_INT >= 17 ? marginLayoutParams.getMarginStart() : marginLayoutParams.leftMargin;
    }

    public static boolean isMarginRelative(MarginLayoutParams marginLayoutParams) {
        if (VERSION.SDK_INT >= 17) {
            return marginLayoutParams.isMarginRelative();
        }
        return false;
    }

    public static void resolveLayoutDirection(MarginLayoutParams marginLayoutParams, int i) {
        if (VERSION.SDK_INT >= 17) {
            marginLayoutParams.resolveLayoutDirection(i);
        }
    }

    public static void setLayoutDirection(MarginLayoutParams marginLayoutParams, int i) {
        if (VERSION.SDK_INT >= 17) {
            marginLayoutParams.setLayoutDirection(i);
        }
    }

    public static void setMarginEnd(MarginLayoutParams marginLayoutParams, int i) {
        if (VERSION.SDK_INT >= 17) {
            marginLayoutParams.setMarginEnd(i);
        } else {
            marginLayoutParams.rightMargin = i;
        }
    }

    public static void setMarginStart(MarginLayoutParams marginLayoutParams, int i) {
        if (VERSION.SDK_INT >= 17) {
            marginLayoutParams.setMarginStart(i);
        } else {
            marginLayoutParams.leftMargin = i;
        }
    }
}
