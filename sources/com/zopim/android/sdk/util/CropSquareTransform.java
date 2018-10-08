package com.zopim.android.sdk.util;

import android.graphics.Bitmap;
import com.squareup.picasso.Transformation;

public class CropSquareTransform implements Transformation {
    public String key() {
        return "square";
    }

    public Bitmap transform(Bitmap bitmap) {
        int min = Math.min(bitmap.getWidth(), bitmap.getHeight());
        Bitmap createBitmap = Bitmap.createBitmap(bitmap, (bitmap.getWidth() - min) / 2, (bitmap.getHeight() - min) / 2, min, min);
        if (createBitmap != bitmap) {
            bitmap.recycle();
        }
        return createBitmap;
    }
}
