package android.support.p000v4.graphics;

import android.graphics.Paint;
import android.os.Build.VERSION;
import android.support.annotation.NonNull;

/* renamed from: android.support.v4.graphics.PaintCompat */
public final class PaintCompat {
    private PaintCompat() {
    }

    public static boolean hasGlyph(@NonNull Paint paint, @NonNull String str) {
        return VERSION.SDK_INT >= 23 ? paint.hasGlyph(str) : PaintCompatApi14.hasGlyph(paint, str);
    }
}
