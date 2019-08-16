package android.support.p000v4.graphics;

import android.content.Context;
import android.content.res.Resources;
import android.graphics.Typeface;
import android.support.annotation.Nullable;
import android.support.annotation.RequiresApi;
import android.support.annotation.RestrictTo;
import android.support.annotation.RestrictTo.Scope;
import android.support.p000v4.content.res.FontResourcesParserCompat.FontFamilyFilesResourceEntry;
import android.support.p000v4.content.res.FontResourcesParserCompat.FontFileResourceEntry;
import android.support.p000v4.provider.FontsContractCompat.FontInfo;
import java.io.File;
import java.io.InputStream;

@RequiresApi(14)
@RestrictTo({Scope.LIBRARY_GROUP})
/* renamed from: android.support.v4.graphics.TypefaceCompatBaseImpl */
class TypefaceCompatBaseImpl implements TypefaceCompatImpl {
    private static final String CACHE_FILE_PREFIX = "cached_font_";
    private static final String TAG = "TypefaceCompatBaseImpl";

    /* renamed from: android.support.v4.graphics.TypefaceCompatBaseImpl$StyleExtractor */
    private interface StyleExtractor<T> {
        int getWeight(T t);

        boolean isItalic(T t);
    }

    TypefaceCompatBaseImpl() {
    }

    private FontFileResourceEntry findBestEntry(FontFamilyFilesResourceEntry fontFamilyFilesResourceEntry, int i) {
        return (FontFileResourceEntry) findBestFont(fontFamilyFilesResourceEntry.getEntries(), i, new StyleExtractor<FontFileResourceEntry>() {
            public int getWeight(FontFileResourceEntry fontFileResourceEntry) {
                return fontFileResourceEntry.getWeight();
            }

            public boolean isItalic(FontFileResourceEntry fontFileResourceEntry) {
                return fontFileResourceEntry.isItalic();
            }
        });
    }

    private static <T> T findBestFont(T[] tArr, int i, StyleExtractor<T> styleExtractor) {
        int i2 = (i & 1) == 0 ? 400 : 700;
        boolean z = (i & 2) != 0;
        T t = null;
        int i3 = Integer.MAX_VALUE;
        for (T t2 : tArr) {
            int abs = (styleExtractor.isItalic(t2) == z ? 0 : 1) + (Math.abs(styleExtractor.getWeight(t2) - i2) * 2);
            if (t == null || i3 > abs) {
                t = t2;
                i3 = abs;
            }
        }
        return t;
    }

    @Nullable
    public Typeface createFromFontFamilyFilesResourceEntry(Context context, FontFamilyFilesResourceEntry fontFamilyFilesResourceEntry, Resources resources, int i) {
        FontFileResourceEntry findBestEntry = findBestEntry(fontFamilyFilesResourceEntry, i);
        if (findBestEntry == null) {
            return null;
        }
        return TypefaceCompat.createFromResourcesFontFile(context, resources, findBestEntry.getResourceId(), findBestEntry.getFileName(), i);
    }

    /* JADX WARNING: type inference failed for: r3v0, types: [java.io.Closeable] */
    /* JADX WARNING: type inference failed for: r3v1 */
    /* JADX WARNING: type inference failed for: r1v3, types: [java.io.Closeable] */
    /* JADX WARNING: type inference failed for: r1v5 */
    /* JADX WARNING: type inference failed for: r3v2 */
    /* JADX WARNING: type inference failed for: r1v8 */
    /* JADX WARNING: Multi-variable type inference failed */
    /* JADX WARNING: Unknown variable types count: 2 */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    public android.graphics.Typeface createFromFontInfo(android.content.Context r5, @android.support.annotation.Nullable android.os.CancellationSignal r6, @android.support.annotation.NonNull android.support.p000v4.provider.FontsContractCompat.FontInfo[] r7, int r8) {
        /*
            r4 = this;
            r0 = 0
            int r1 = r7.length
            r2 = 1
            if (r1 >= r2) goto L_0x0006
        L_0x0005:
            return r0
        L_0x0006:
            android.support.v4.provider.FontsContractCompat$FontInfo r1 = r4.findBestInfo(r7, r8)
            android.content.ContentResolver r2 = r5.getContentResolver()     // Catch:{ IOException -> 0x001e, all -> 0x0024 }
            android.net.Uri r1 = r1.getUri()     // Catch:{ IOException -> 0x001e, all -> 0x0024 }
            java.io.InputStream r1 = r2.openInputStream(r1)     // Catch:{ IOException -> 0x001e, all -> 0x0024 }
            android.graphics.Typeface r0 = r4.createFromInputStream(r5, r1)     // Catch:{ IOException -> 0x002f, all -> 0x002b }
            android.support.p000v4.graphics.TypefaceCompatUtil.closeQuietly(r1)
            goto L_0x0005
        L_0x001e:
            r1 = move-exception
            r1 = r0
        L_0x0020:
            android.support.p000v4.graphics.TypefaceCompatUtil.closeQuietly(r1)
            goto L_0x0005
        L_0x0024:
            r1 = move-exception
            r2 = r1
            r3 = r0
        L_0x0027:
            android.support.p000v4.graphics.TypefaceCompatUtil.closeQuietly(r3)
            throw r2
        L_0x002b:
            r0 = move-exception
            r2 = r0
            r3 = r1
            goto L_0x0027
        L_0x002f:
            r2 = move-exception
            goto L_0x0020
        */
        throw new UnsupportedOperationException("Method not decompiled: android.support.p000v4.graphics.TypefaceCompatBaseImpl.createFromFontInfo(android.content.Context, android.os.CancellationSignal, android.support.v4.provider.FontsContractCompat$FontInfo[], int):android.graphics.Typeface");
    }

    /* access modifiers changed from: protected */
    public Typeface createFromInputStream(Context context, InputStream inputStream) {
        Typeface typeface = null;
        File tempFile = TypefaceCompatUtil.getTempFile(context);
        if (tempFile != null) {
            try {
                if (TypefaceCompatUtil.copyToFile(tempFile, inputStream)) {
                    typeface = Typeface.createFromFile(tempFile.getPath());
                    tempFile.delete();
                }
            } catch (RuntimeException e) {
            } finally {
                tempFile.delete();
            }
        }
        return typeface;
    }

    @Nullable
    public Typeface createFromResourcesFontFile(Context context, Resources resources, int i, String str, int i2) {
        Typeface typeface = null;
        File tempFile = TypefaceCompatUtil.getTempFile(context);
        if (tempFile != null) {
            try {
                if (TypefaceCompatUtil.copyToFile(tempFile, resources, i)) {
                    typeface = Typeface.createFromFile(tempFile.getPath());
                    tempFile.delete();
                }
            } catch (RuntimeException e) {
            } finally {
                tempFile.delete();
            }
        }
        return typeface;
    }

    /* access modifiers changed from: protected */
    public FontInfo findBestInfo(FontInfo[] fontInfoArr, int i) {
        return (FontInfo) findBestFont(fontInfoArr, i, new StyleExtractor<FontInfo>() {
            public int getWeight(FontInfo fontInfo) {
                return fontInfo.getWeight();
            }

            public boolean isItalic(FontInfo fontInfo) {
                return fontInfo.isItalic();
            }
        });
    }
}
