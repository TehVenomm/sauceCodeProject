package android.support.p000v4.graphics;

import android.graphics.Rect;
import android.support.p000v4.util.Pair;

/* renamed from: android.support.v4.graphics.PaintCompatApi14 */
class PaintCompatApi14 {
    private static final String EM_STRING = "m";
    private static final String TOFU_STRING = "󟿽";
    private static final ThreadLocal<Pair<Rect, Rect>> sRectThreadLocal = new ThreadLocal<>();

    PaintCompatApi14() {
    }

    /* JADX WARNING: Code restructure failed: missing block: B:15:0x0050, code lost:
        if (r6 < r3) goto L_0x0052;
     */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    static boolean hasGlyph(@android.support.annotation.NonNull android.graphics.Paint r9, @android.support.annotation.NonNull java.lang.String r10) {
        /*
            r3 = 0
            r1 = 1
            r2 = 0
            int r4 = r10.length()
            if (r4 != r1) goto L_0x0015
            char r0 = r10.charAt(r2)
            boolean r0 = java.lang.Character.isWhitespace(r0)
            if (r0 == 0) goto L_0x0015
            r0 = r1
        L_0x0014:
            return r0
        L_0x0015:
            java.lang.String r0 = "󟿽"
            float r5 = r9.measureText(r0)
            java.lang.String r0 = "m"
            float r0 = r9.measureText(r0)
            float r6 = r9.measureText(r10)
            int r7 = (r6 > r3 ? 1 : (r6 == r3 ? 0 : -1))
            if (r7 == 0) goto L_0x0082
            int r7 = r10.length()
            int r7 = r10.codePointCount(r2, r7)
            if (r7 <= r1) goto L_0x0052
            r7 = 1073741824(0x40000000, float:2.0)
            float r0 = r0 * r7
            int r0 = (r6 > r0 ? 1 : (r6 == r0 ? 0 : -1))
            if (r0 > 0) goto L_0x0082
            r0 = r2
        L_0x003b:
            if (r0 >= r4) goto L_0x004e
            int r7 = r10.codePointAt(r0)
            int r7 = java.lang.Character.charCount(r7)
            int r8 = r0 + r7
            float r8 = r9.measureText(r10, r0, r8)
            float r3 = r3 + r8
            int r0 = r0 + r7
            goto L_0x003b
        L_0x004e:
            int r0 = (r6 > r3 ? 1 : (r6 == r3 ? 0 : -1))
            if (r0 >= 0) goto L_0x0082
        L_0x0052:
            int r0 = (r6 > r5 ? 1 : (r6 == r5 ? 0 : -1))
            if (r0 == 0) goto L_0x0058
            r0 = r1
            goto L_0x0014
        L_0x0058:
            android.support.v4.util.Pair r3 = obtainEmptyRects()
            java.lang.String r5 = "󟿽"
            java.lang.String r0 = "󟿽"
            int r6 = r0.length()
            F r0 = r3.first
            android.graphics.Rect r0 = (android.graphics.Rect) r0
            r9.getTextBounds(r5, r2, r6, r0)
            S r0 = r3.second
            android.graphics.Rect r0 = (android.graphics.Rect) r0
            r9.getTextBounds(r10, r2, r4, r0)
            F r0 = r3.first
            android.graphics.Rect r0 = (android.graphics.Rect) r0
            S r3 = r3.second
            boolean r0 = r0.equals(r3)
            if (r0 != 0) goto L_0x0080
            r0 = r1
            goto L_0x0014
        L_0x0080:
            r0 = r2
            goto L_0x0014
        L_0x0082:
            r0 = r2
            goto L_0x0014
        */
        throw new UnsupportedOperationException("Method not decompiled: android.support.p000v4.graphics.PaintCompatApi14.hasGlyph(android.graphics.Paint, java.lang.String):boolean");
    }

    private static Pair<Rect, Rect> obtainEmptyRects() {
        Pair<Rect, Rect> pair = (Pair) sRectThreadLocal.get();
        if (pair == null) {
            Pair<Rect, Rect> pair2 = new Pair<>(new Rect(), new Rect());
            sRectThreadLocal.set(pair2);
            return pair2;
        }
        ((Rect) pair.first).setEmpty();
        ((Rect) pair.second).setEmpty();
        return pair;
    }
}
