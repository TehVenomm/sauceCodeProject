package com.github.droidfu.support;

import android.content.Context;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.graphics.drawable.BitmapDrawable;
import android.graphics.drawable.Drawable;
import android.util.DisplayMetrics;

public class DisplaySupport {
    public static final int SCREEN_DENSITY_HIGH = 240;
    public static final int SCREEN_DENSITY_LOW = 120;
    public static final int SCREEN_DENSITY_MEDIUM = 160;
    private static int screenDensity = -1;

    public static int dipToPx(Context context, int i) {
        return (int) ((context.getResources().getDisplayMetrics().density * ((float) i)) + 0.5f);
    }

    public static int getScreenDensity(Context context) {
        if (screenDensity == -1) {
            try {
                screenDensity = DisplayMetrics.class.getField("densityDpi").getInt(context.getResources().getDisplayMetrics());
            } catch (Exception e) {
                screenDensity = SCREEN_DENSITY_MEDIUM;
            }
        }
        return screenDensity;
    }

    public static Drawable scaleDrawable(Context context, int i, int i2, int i3) {
        return new BitmapDrawable(Bitmap.createScaledBitmap(BitmapFactory.decodeResource(context.getResources(), i), i2, i3, true));
    }
}
