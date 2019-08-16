package android.support.p000v4.graphics;

import android.content.Context;
import android.content.res.Resources;
import android.graphics.Typeface;
import android.net.Uri;
import android.os.CancellationSignal;
import android.support.annotation.NonNull;
import android.support.annotation.Nullable;
import android.support.annotation.RequiresApi;
import android.support.annotation.RestrictTo;
import android.support.annotation.RestrictTo.Scope;
import android.support.p000v4.content.res.FontResourcesParserCompat.FontFamilyFilesResourceEntry;
import android.support.p000v4.content.res.FontResourcesParserCompat.FontFileResourceEntry;
import android.support.p000v4.provider.FontsContractCompat.FontInfo;
import android.support.p000v4.util.SimpleArrayMap;
import android.util.Log;
import java.lang.reflect.Array;
import java.lang.reflect.Constructor;
import java.lang.reflect.InvocationTargetException;
import java.lang.reflect.Method;
import java.nio.ByteBuffer;

@RequiresApi(24)
@RestrictTo({Scope.LIBRARY_GROUP})
/* renamed from: android.support.v4.graphics.TypefaceCompatApi24Impl */
class TypefaceCompatApi24Impl extends TypefaceCompatBaseImpl {
    private static final String ADD_FONT_WEIGHT_STYLE_METHOD = "addFontWeightStyle";
    private static final String CREATE_FROM_FAMILIES_WITH_DEFAULT_METHOD = "createFromFamiliesWithDefault";
    private static final String FONT_FAMILY_CLASS = "android.graphics.FontFamily";
    private static final String TAG = "TypefaceCompatApi24Impl";
    private static final Method sAddFontWeightStyle;
    private static final Method sCreateFromFamiliesWithDefault;
    private static final Class sFontFamily;
    private static final Constructor sFontFamilyCtor;

    /* JADX WARNING: type inference failed for: r1v0 */
    /* JADX WARNING: Multi-variable type inference failed */
    /* JADX WARNING: Unknown variable types count: 1 */
    static {
        /*
            r1 = 0
            java.lang.String r0 = "android.graphics.FontFamily"
            java.lang.Class r0 = java.lang.Class.forName(r0)     // Catch:{ ClassNotFoundException -> 0x0064, NoSuchMethodException -> 0x0051 }
            r2 = 0
            java.lang.Class[] r2 = new java.lang.Class[r2]     // Catch:{ ClassNotFoundException -> 0x0064, NoSuchMethodException -> 0x0051 }
            java.lang.reflect.Constructor r4 = r0.getConstructor(r2)     // Catch:{ ClassNotFoundException -> 0x0064, NoSuchMethodException -> 0x0051 }
            java.lang.String r2 = "addFontWeightStyle"
            r3 = 5
            java.lang.Class[] r3 = new java.lang.Class[r3]     // Catch:{ ClassNotFoundException -> 0x0064, NoSuchMethodException -> 0x0051 }
            r5 = 0
            java.lang.Class<java.nio.ByteBuffer> r6 = java.nio.ByteBuffer.class
            r3[r5] = r6     // Catch:{ ClassNotFoundException -> 0x0064, NoSuchMethodException -> 0x0051 }
            r5 = 1
            java.lang.Class r6 = java.lang.Integer.TYPE     // Catch:{ ClassNotFoundException -> 0x0064, NoSuchMethodException -> 0x0051 }
            r3[r5] = r6     // Catch:{ ClassNotFoundException -> 0x0064, NoSuchMethodException -> 0x0051 }
            r5 = 2
            java.lang.Class<java.util.List> r6 = java.util.List.class
            r3[r5] = r6     // Catch:{ ClassNotFoundException -> 0x0064, NoSuchMethodException -> 0x0051 }
            r5 = 3
            java.lang.Class r6 = java.lang.Integer.TYPE     // Catch:{ ClassNotFoundException -> 0x0064, NoSuchMethodException -> 0x0051 }
            r3[r5] = r6     // Catch:{ ClassNotFoundException -> 0x0064, NoSuchMethodException -> 0x0051 }
            r5 = 4
            java.lang.Class r6 = java.lang.Boolean.TYPE     // Catch:{ ClassNotFoundException -> 0x0064, NoSuchMethodException -> 0x0051 }
            r3[r5] = r6     // Catch:{ ClassNotFoundException -> 0x0064, NoSuchMethodException -> 0x0051 }
            java.lang.reflect.Method r3 = r0.getMethod(r2, r3)     // Catch:{ ClassNotFoundException -> 0x0064, NoSuchMethodException -> 0x0051 }
            java.lang.Class<android.graphics.Typeface> r2 = android.graphics.Typeface.class
            java.lang.String r5 = "createFromFamiliesWithDefault"
            r6 = 1
            java.lang.Class[] r6 = new java.lang.Class[r6]     // Catch:{ ClassNotFoundException -> 0x0064, NoSuchMethodException -> 0x0051 }
            r7 = 0
            r8 = 1
            java.lang.Object r8 = java.lang.reflect.Array.newInstance(r0, r8)     // Catch:{ ClassNotFoundException -> 0x0064, NoSuchMethodException -> 0x0051 }
            java.lang.Class r8 = r8.getClass()     // Catch:{ ClassNotFoundException -> 0x0064, NoSuchMethodException -> 0x0051 }
            r6[r7] = r8     // Catch:{ ClassNotFoundException -> 0x0064, NoSuchMethodException -> 0x0051 }
            java.lang.reflect.Method r1 = r2.getMethod(r5, r6)     // Catch:{ ClassNotFoundException -> 0x0064, NoSuchMethodException -> 0x0051 }
            r2 = r1
        L_0x0048:
            sFontFamilyCtor = r4
            sFontFamily = r0
            sAddFontWeightStyle = r3
            sCreateFromFamiliesWithDefault = r2
            return
        L_0x0051:
            r0 = move-exception
        L_0x0052:
            java.lang.String r2 = "TypefaceCompatApi24Impl"
            java.lang.Class r3 = r0.getClass()
            java.lang.String r3 = r3.getName()
            android.util.Log.e(r2, r3, r0)
            r0 = r1
            r2 = r1
            r3 = r1
            r4 = r1
            goto L_0x0048
        L_0x0064:
            r0 = move-exception
            goto L_0x0052
        */
        throw new UnsupportedOperationException("Method not decompiled: android.support.p000v4.graphics.TypefaceCompatApi24Impl.<clinit>():void");
    }

    TypefaceCompatApi24Impl() {
    }

    private static boolean addFontWeightStyle(Object obj, ByteBuffer byteBuffer, int i, int i2, boolean z) {
        try {
            return ((Boolean) sAddFontWeightStyle.invoke(obj, new Object[]{byteBuffer, Integer.valueOf(i), null, Integer.valueOf(i2), Boolean.valueOf(z)})).booleanValue();
        } catch (IllegalAccessException | InvocationTargetException e) {
            throw new RuntimeException(e);
        }
    }

    private static Typeface createFromFamiliesWithDefault(Object obj) {
        try {
            Object newInstance = Array.newInstance(sFontFamily, 1);
            Array.set(newInstance, 0, obj);
            return (Typeface) sCreateFromFamiliesWithDefault.invoke(null, new Object[]{newInstance});
        } catch (IllegalAccessException | InvocationTargetException e) {
            throw new RuntimeException(e);
        }
    }

    public static boolean isUsable() {
        if (sAddFontWeightStyle == null) {
            Log.w(TAG, "Unable to collect necessary private methods.Fallback to legacy implementation.");
        }
        return sAddFontWeightStyle != null;
    }

    private static Object newFamily() {
        try {
            return sFontFamilyCtor.newInstance(new Object[0]);
        } catch (IllegalAccessException | InstantiationException | InvocationTargetException e) {
            throw new RuntimeException(e);
        }
    }

    public Typeface createFromFontFamilyFilesResourceEntry(Context context, FontFamilyFilesResourceEntry fontFamilyFilesResourceEntry, Resources resources, int i) {
        FontFileResourceEntry[] entries;
        Object newFamily = newFamily();
        for (FontFileResourceEntry fontFileResourceEntry : fontFamilyFilesResourceEntry.getEntries()) {
            if (!addFontWeightStyle(newFamily, TypefaceCompatUtil.copyToDirectBuffer(context, resources, fontFileResourceEntry.getResourceId()), 0, fontFileResourceEntry.getWeight(), fontFileResourceEntry.isItalic())) {
                return null;
            }
        }
        return createFromFamiliesWithDefault(newFamily);
    }

    public Typeface createFromFontInfo(Context context, @Nullable CancellationSignal cancellationSignal, @NonNull FontInfo[] fontInfoArr, int i) {
        Object newFamily = newFamily();
        SimpleArrayMap simpleArrayMap = new SimpleArrayMap();
        for (FontInfo fontInfo : fontInfoArr) {
            Uri uri = fontInfo.getUri();
            ByteBuffer byteBuffer = (ByteBuffer) simpleArrayMap.get(uri);
            if (byteBuffer == null) {
                byteBuffer = TypefaceCompatUtil.mmap(context, cancellationSignal, uri);
                simpleArrayMap.put(uri, byteBuffer);
            }
            if (!addFontWeightStyle(newFamily, byteBuffer, fontInfo.getTtcIndex(), fontInfo.getWeight(), fontInfo.isItalic())) {
                return null;
            }
        }
        return createFromFamiliesWithDefault(newFamily);
    }
}
