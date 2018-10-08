package net.gogame.gopay.sdk.support;

import android.app.Activity;
import android.content.Context;
import android.graphics.Point;

public class DisplayUtils {
    public static float dpFromPx(Context context, float f) {
        return f / context.getResources().getDisplayMetrics().density;
    }

    public static Point getScreenSize(Activity activity) {
        Point point = new Point();
        activity.getWindowManager().getDefaultDisplay().getSize(point);
        return point;
    }

    public static void lockOrientation(Activity activity) {
        if (activity.getResources().getConfiguration().orientation == 2) {
            activity.setRequestedOrientation(6);
        } else {
            activity.setRequestedOrientation(7);
        }
    }

    public static int pxFromDp(Context context, float f) {
        return Math.round(context.getResources().getDisplayMetrics().density * f);
    }
}
