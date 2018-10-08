package com.zopim.android.sdk.util;

import android.content.Context;
import android.util.TypedValue;

public class Dimensions {
    public static int convertDpToPixel(Context context, float f) {
        return (int) TypedValue.applyDimension(1, f, context.getResources().getDisplayMetrics());
    }
}
