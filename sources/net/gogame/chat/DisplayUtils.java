package net.gogame.chat;

import android.content.Context;
import android.os.IBinder;
import android.view.LayoutInflater;
import android.view.inputmethod.InputMethodManager;

public final class DisplayUtils {
    private DisplayUtils() {
    }

    public static float dpFromPx(Context context, float f) {
        return f / context.getResources().getDisplayMetrics().density;
    }

    public static int pxFromDp(Context context, float f) {
        return Math.round(context.getResources().getDisplayMetrics().density * f);
    }

    public static void hideKeyboard(Context context, IBinder iBinder) {
        ((InputMethodManager) context.getSystemService("input_method")).hideSoftInputFromWindow(iBinder, 0);
    }

    public static LayoutInflater getLayoutInflater(Context context) {
        return (LayoutInflater) context.getSystemService("layout_inflater");
    }
}
