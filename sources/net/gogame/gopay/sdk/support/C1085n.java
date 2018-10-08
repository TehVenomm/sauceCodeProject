package net.gogame.gopay.sdk.support;

import android.graphics.Bitmap;
import android.util.LruCache;

/* renamed from: net.gogame.gopay.sdk.support.n */
final class C1085n extends LruCache {
    C1085n(int i) {
        super(i);
    }

    protected final /* synthetic */ int sizeOf(Object obj, Object obj2) {
        return ((Bitmap) obj2).getByteCount() / 1024;
    }
}
