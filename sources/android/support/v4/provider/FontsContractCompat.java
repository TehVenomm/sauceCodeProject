package android.support.p000v4.provider;

import android.content.Context;
import android.content.pm.PackageManager;
import android.content.pm.PackageManager.NameNotFoundException;
import android.content.pm.ProviderInfo;
import android.content.pm.Signature;
import android.content.res.Resources;
import android.graphics.Typeface;
import android.net.Uri;
import android.os.CancellationSignal;
import android.os.Handler;
import android.provider.BaseColumns;
import android.support.annotation.GuardedBy;
import android.support.annotation.IntRange;
import android.support.annotation.NonNull;
import android.support.annotation.Nullable;
import android.support.annotation.RequiresApi;
import android.support.annotation.RestrictTo;
import android.support.annotation.RestrictTo.Scope;
import android.support.annotation.VisibleForTesting;
import android.support.p000v4.content.res.FontResourcesParserCompat;
import android.support.p000v4.graphics.TypefaceCompat;
import android.support.p000v4.graphics.TypefaceCompatUtil;
import android.support.p000v4.provider.SelfDestructiveThread.ReplyCallback;
import android.support.p000v4.util.LruCache;
import android.support.p000v4.util.Preconditions;
import android.support.p000v4.util.SimpleArrayMap;
import android.widget.TextView;
import java.lang.annotation.Retention;
import java.lang.annotation.RetentionPolicy;
import java.lang.ref.WeakReference;
import java.nio.ByteBuffer;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.Collection;
import java.util.Collections;
import java.util.Comparator;
import java.util.HashMap;
import java.util.List;
import java.util.Map;
import java.util.concurrent.Callable;

/* renamed from: android.support.v4.provider.FontsContractCompat */
public class FontsContractCompat {
    private static final int BACKGROUND_THREAD_KEEP_ALIVE_DURATION_MS = 10000;
    @RestrictTo({Scope.LIBRARY_GROUP})
    public static final String PARCEL_FONT_RESULTS = "font_results";
    @RestrictTo({Scope.LIBRARY_GROUP})
    public static final int RESULT_CODE_PROVIDER_NOT_FOUND = -1;
    @RestrictTo({Scope.LIBRARY_GROUP})
    public static final int RESULT_CODE_WRONG_CERTIFICATES = -2;
    private static final String TAG = "FontsContractCompat";
    private static final SelfDestructiveThread sBackgroundThread = new SelfDestructiveThread("fonts", 10, 10000);
    private static final Comparator<byte[]> sByteArrayComparator = new Comparator<byte[]>() {
        public int compare(byte[] bArr, byte[] bArr2) {
            if (bArr.length != bArr2.length) {
                return bArr.length - bArr2.length;
            }
            for (int i = 0; i < bArr.length; i++) {
                if (bArr[i] != bArr2[i]) {
                    return bArr[i] - bArr2[i];
                }
            }
            return 0;
        }
    };
    /* access modifiers changed from: private */
    public static final Object sLock = new Object();
    /* access modifiers changed from: private */
    @GuardedBy("sLock")
    public static final SimpleArrayMap<String, ArrayList<ReplyCallback<Typeface>>> sPendingReplies = new SimpleArrayMap<>();
    /* access modifiers changed from: private */
    public static final LruCache<String, Typeface> sTypefaceCache = new LruCache<>(16);

    /* renamed from: android.support.v4.provider.FontsContractCompat$Columns */
    public static final class Columns implements BaseColumns {
        public static final String FILE_ID = "file_id";
        public static final String ITALIC = "font_italic";
        public static final String RESULT_CODE = "result_code";
        public static final int RESULT_CODE_FONT_NOT_FOUND = 1;
        public static final int RESULT_CODE_FONT_UNAVAILABLE = 2;
        public static final int RESULT_CODE_MALFORMED_QUERY = 3;
        public static final int RESULT_CODE_OK = 0;
        public static final String TTC_INDEX = "font_ttc_index";
        public static final String VARIATION_SETTINGS = "font_variation_settings";
        public static final String WEIGHT = "font_weight";
    }

    /* renamed from: android.support.v4.provider.FontsContractCompat$FontFamilyResult */
    public static class FontFamilyResult {
        public static final int STATUS_OK = 0;
        public static final int STATUS_UNEXPECTED_DATA_PROVIDED = 2;
        public static final int STATUS_WRONG_CERTIFICATES = 1;
        private final FontInfo[] mFonts;
        private final int mStatusCode;

        @RestrictTo({Scope.LIBRARY_GROUP})
        @Retention(RetentionPolicy.SOURCE)
        /* renamed from: android.support.v4.provider.FontsContractCompat$FontFamilyResult$FontResultStatus */
        @interface FontResultStatus {
        }

        @RestrictTo({Scope.LIBRARY_GROUP})
        public FontFamilyResult(int i, @Nullable FontInfo[] fontInfoArr) {
            this.mStatusCode = i;
            this.mFonts = fontInfoArr;
        }

        public FontInfo[] getFonts() {
            return this.mFonts;
        }

        public int getStatusCode() {
            return this.mStatusCode;
        }
    }

    /* renamed from: android.support.v4.provider.FontsContractCompat$FontInfo */
    public static class FontInfo {
        private final boolean mItalic;
        private final int mResultCode;
        private final int mTtcIndex;
        private final Uri mUri;
        private final int mWeight;

        @RestrictTo({Scope.LIBRARY_GROUP})
        public FontInfo(@NonNull Uri uri, @IntRange(from = 0) int i, @IntRange(from = 1, mo60to = 1000) int i2, boolean z, int i3) {
            this.mUri = (Uri) Preconditions.checkNotNull(uri);
            this.mTtcIndex = i;
            this.mWeight = i2;
            this.mItalic = z;
            this.mResultCode = i3;
        }

        public int getResultCode() {
            return this.mResultCode;
        }

        @IntRange(from = 0)
        public int getTtcIndex() {
            return this.mTtcIndex;
        }

        @NonNull
        public Uri getUri() {
            return this.mUri;
        }

        @IntRange(from = 1, mo60to = 1000)
        public int getWeight() {
            return this.mWeight;
        }

        public boolean isItalic() {
            return this.mItalic;
        }
    }

    /* renamed from: android.support.v4.provider.FontsContractCompat$FontRequestCallback */
    public static class FontRequestCallback {
        public static final int FAIL_REASON_FONT_LOAD_ERROR = -3;
        public static final int FAIL_REASON_FONT_NOT_FOUND = 1;
        public static final int FAIL_REASON_FONT_UNAVAILABLE = 2;
        public static final int FAIL_REASON_MALFORMED_QUERY = 3;
        public static final int FAIL_REASON_PROVIDER_NOT_FOUND = -1;
        public static final int FAIL_REASON_WRONG_CERTIFICATES = -2;

        @RestrictTo({Scope.LIBRARY_GROUP})
        @Retention(RetentionPolicy.SOURCE)
        /* renamed from: android.support.v4.provider.FontsContractCompat$FontRequestCallback$FontRequestFailReason */
        @interface FontRequestFailReason {
        }

        public void onTypefaceRequestFailed(int i) {
        }

        public void onTypefaceRetrieved(Typeface typeface) {
        }
    }

    private FontsContractCompat() {
    }

    public static Typeface buildTypeface(@NonNull Context context, @Nullable CancellationSignal cancellationSignal, @NonNull FontInfo[] fontInfoArr) {
        return TypefaceCompat.createFromFontInfo(context, cancellationSignal, fontInfoArr, 0);
    }

    private static List<byte[]> convertToByteArrayList(Signature[] signatureArr) {
        ArrayList arrayList = new ArrayList();
        for (Signature byteArray : signatureArr) {
            arrayList.add(byteArray.toByteArray());
        }
        return arrayList;
    }

    private static boolean equalsByteArrayList(List<byte[]> list, List<byte[]> list2) {
        if (list.size() != list2.size()) {
            return false;
        }
        for (int i = 0; i < list.size(); i++) {
            if (!Arrays.equals((byte[]) list.get(i), (byte[]) list2.get(i))) {
                return false;
            }
        }
        return true;
    }

    @NonNull
    public static FontFamilyResult fetchFonts(@NonNull Context context, @Nullable CancellationSignal cancellationSignal, @NonNull FontRequest fontRequest) throws NameNotFoundException {
        ProviderInfo provider = getProvider(context.getPackageManager(), fontRequest, context.getResources());
        return provider == null ? new FontFamilyResult(1, null) : new FontFamilyResult(0, getFontFromProvider(context, fontRequest, provider.authority, cancellationSignal));
    }

    private static List<List<byte[]>> getCertificates(FontRequest fontRequest, Resources resources) {
        return fontRequest.getCertificates() != null ? fontRequest.getCertificates() : FontResourcesParserCompat.readCerts(resources, fontRequest.getCertificatesArrayResId());
    }

    /* JADX WARNING: Removed duplicated region for block: B:47:0x0145  */
    @android.support.annotation.VisibleForTesting
    @android.support.annotation.NonNull
    /* Code decompiled incorrectly, please refer to instructions dump. */
    static android.support.p000v4.provider.FontsContractCompat.FontInfo[] getFontFromProvider(android.content.Context r18, android.support.p000v4.provider.FontRequest r19, java.lang.String r20, android.os.CancellationSignal r21) {
        /*
            java.util.ArrayList r11 = new java.util.ArrayList
            r11.<init>()
            android.net.Uri$Builder r2 = new android.net.Uri$Builder
            r2.<init>()
            java.lang.String r3 = "content"
            android.net.Uri$Builder r2 = r2.scheme(r3)
            r0 = r20
            android.net.Uri$Builder r2 = r2.authority(r0)
            android.net.Uri r3 = r2.build()
            android.net.Uri$Builder r2 = new android.net.Uri$Builder
            r2.<init>()
            java.lang.String r4 = "content"
            android.net.Uri$Builder r2 = r2.scheme(r4)
            r0 = r20
            android.net.Uri$Builder r2 = r2.authority(r0)
            java.lang.String r4 = "file"
            android.net.Uri$Builder r2 = r2.appendPath(r4)
            android.net.Uri r12 = r2.build()
            r9 = 0
            int r2 = android.os.Build.VERSION.SDK_INT     // Catch:{ all -> 0x0152 }
            r4 = 16
            if (r2 <= r4) goto L_0x00f3
            android.content.ContentResolver r2 = r18.getContentResolver()     // Catch:{ all -> 0x0152 }
            java.lang.String r7 = r19.getQuery()     // Catch:{ all -> 0x0152 }
            r4 = 7
            java.lang.String[] r4 = new java.lang.String[r4]     // Catch:{ all -> 0x0152 }
            r5 = 0
            java.lang.String r6 = "_id"
            r4[r5] = r6     // Catch:{ all -> 0x0152 }
            r5 = 1
            java.lang.String r6 = "file_id"
            r4[r5] = r6     // Catch:{ all -> 0x0152 }
            r5 = 2
            java.lang.String r6 = "font_ttc_index"
            r4[r5] = r6     // Catch:{ all -> 0x0152 }
            r5 = 3
            java.lang.String r6 = "font_variation_settings"
            r4[r5] = r6     // Catch:{ all -> 0x0152 }
            r5 = 4
            java.lang.String r6 = "font_weight"
            r4[r5] = r6     // Catch:{ all -> 0x0152 }
            r5 = 5
            java.lang.String r6 = "font_italic"
            r4[r5] = r6     // Catch:{ all -> 0x0152 }
            r5 = 6
            java.lang.String r6 = "result_code"
            r4[r5] = r6     // Catch:{ all -> 0x0152 }
            java.lang.String r5 = "query = ?"
            r6 = 1
            java.lang.String[] r6 = new java.lang.String[r6]     // Catch:{ all -> 0x0152 }
            r8 = 0
            r6[r8] = r7     // Catch:{ all -> 0x0152 }
            r7 = 0
            r8 = r21
            android.database.Cursor r10 = r2.query(r3, r4, r5, r6, r7, r8)     // Catch:{ all -> 0x0152 }
        L_0x0079:
            if (r10 == 0) goto L_0x0142
            int r2 = r10.getCount()     // Catch:{ all -> 0x0156 }
            if (r2 <= 0) goto L_0x0142
            java.lang.String r2 = "result_code"
            int r11 = r10.getColumnIndex(r2)     // Catch:{ all -> 0x0156 }
            java.util.ArrayList r2 = new java.util.ArrayList     // Catch:{ all -> 0x0156 }
            r2.<init>()     // Catch:{ all -> 0x0156 }
            java.lang.String r4 = "_id"
            int r13 = r10.getColumnIndex(r4)     // Catch:{ all -> 0x00ec }
            java.lang.String r4 = "file_id"
            int r14 = r10.getColumnIndex(r4)     // Catch:{ all -> 0x00ec }
            java.lang.String r4 = "font_ttc_index"
            int r15 = r10.getColumnIndex(r4)     // Catch:{ all -> 0x00ec }
            java.lang.String r4 = "font_weight"
            int r16 = r10.getColumnIndex(r4)     // Catch:{ all -> 0x00ec }
            java.lang.String r4 = "font_italic"
            int r17 = r10.getColumnIndex(r4)     // Catch:{ all -> 0x00ec }
        L_0x00aa:
            boolean r4 = r10.moveToNext()     // Catch:{ all -> 0x00ec }
            if (r4 == 0) goto L_0x0143
            r4 = -1
            if (r11 == r4) goto L_0x0130
            int r9 = r10.getInt(r11)     // Catch:{ all -> 0x00ec }
        L_0x00b7:
            r4 = -1
            if (r15 == r4) goto L_0x0132
            int r6 = r10.getInt(r15)     // Catch:{ all -> 0x00ec }
        L_0x00be:
            r4 = -1
            if (r14 != r4) goto L_0x0134
            long r4 = r10.getLong(r13)     // Catch:{ all -> 0x00ec }
            android.net.Uri r5 = android.content.ContentUris.withAppendedId(r3, r4)     // Catch:{ all -> 0x00ec }
        L_0x00c9:
            r4 = -1
            r0 = r16
            if (r0 == r4) goto L_0x013d
            r0 = r16
            int r7 = r10.getInt(r0)     // Catch:{ all -> 0x00ec }
        L_0x00d4:
            r4 = -1
            r0 = r17
            if (r0 == r4) goto L_0x0140
            r0 = r17
            int r4 = r10.getInt(r0)     // Catch:{ all -> 0x00ec }
            r8 = 1
            if (r4 != r8) goto L_0x0140
            r8 = 1
        L_0x00e3:
            android.support.v4.provider.FontsContractCompat$FontInfo r4 = new android.support.v4.provider.FontsContractCompat$FontInfo     // Catch:{ all -> 0x00ec }
            r4.<init>(r5, r6, r7, r8, r9)     // Catch:{ all -> 0x00ec }
            r2.add(r4)     // Catch:{ all -> 0x00ec }
            goto L_0x00aa
        L_0x00ec:
            r2 = move-exception
        L_0x00ed:
            if (r10 == 0) goto L_0x00f2
            r10.close()
        L_0x00f2:
            throw r2
        L_0x00f3:
            android.content.ContentResolver r2 = r18.getContentResolver()     // Catch:{ all -> 0x0152 }
            java.lang.String r7 = r19.getQuery()     // Catch:{ all -> 0x0152 }
            r4 = 7
            java.lang.String[] r4 = new java.lang.String[r4]     // Catch:{ all -> 0x0152 }
            r5 = 0
            java.lang.String r6 = "_id"
            r4[r5] = r6     // Catch:{ all -> 0x0152 }
            r5 = 1
            java.lang.String r6 = "file_id"
            r4[r5] = r6     // Catch:{ all -> 0x0152 }
            r5 = 2
            java.lang.String r6 = "font_ttc_index"
            r4[r5] = r6     // Catch:{ all -> 0x0152 }
            r5 = 3
            java.lang.String r6 = "font_variation_settings"
            r4[r5] = r6     // Catch:{ all -> 0x0152 }
            r5 = 4
            java.lang.String r6 = "font_weight"
            r4[r5] = r6     // Catch:{ all -> 0x0152 }
            r5 = 5
            java.lang.String r6 = "font_italic"
            r4[r5] = r6     // Catch:{ all -> 0x0152 }
            r5 = 6
            java.lang.String r6 = "result_code"
            r4[r5] = r6     // Catch:{ all -> 0x0152 }
            java.lang.String r5 = "query = ?"
            r6 = 1
            java.lang.String[] r6 = new java.lang.String[r6]     // Catch:{ all -> 0x0152 }
            r8 = 0
            r6[r8] = r7     // Catch:{ all -> 0x0152 }
            r7 = 0
            android.database.Cursor r10 = r2.query(r3, r4, r5, r6, r7)     // Catch:{ all -> 0x0152 }
            goto L_0x0079
        L_0x0130:
            r9 = 0
            goto L_0x00b7
        L_0x0132:
            r6 = 0
            goto L_0x00be
        L_0x0134:
            long r4 = r10.getLong(r14)     // Catch:{ all -> 0x00ec }
            android.net.Uri r5 = android.content.ContentUris.withAppendedId(r12, r4)     // Catch:{ all -> 0x00ec }
            goto L_0x00c9
        L_0x013d:
            r7 = 400(0x190, float:5.6E-43)
            goto L_0x00d4
        L_0x0140:
            r8 = 0
            goto L_0x00e3
        L_0x0142:
            r2 = r11
        L_0x0143:
            if (r10 == 0) goto L_0x0148
            r10.close()
        L_0x0148:
            r3 = 0
            android.support.v4.provider.FontsContractCompat$FontInfo[] r3 = new android.support.p000v4.provider.FontsContractCompat.FontInfo[r3]
            java.lang.Object[] r2 = r2.toArray(r3)
            android.support.v4.provider.FontsContractCompat$FontInfo[] r2 = (android.support.p000v4.provider.FontsContractCompat.FontInfo[]) r2
            return r2
        L_0x0152:
            r2 = move-exception
            r3 = r9
        L_0x0154:
            r10 = r3
            goto L_0x00ed
        L_0x0156:
            r2 = move-exception
            r3 = r10
            goto L_0x0154
        */
        throw new UnsupportedOperationException("Method not decompiled: android.support.p000v4.provider.FontsContractCompat.getFontFromProvider(android.content.Context, android.support.v4.provider.FontRequest, java.lang.String, android.os.CancellationSignal):android.support.v4.provider.FontsContractCompat$FontInfo[]");
    }

    /* access modifiers changed from: private */
    public static Typeface getFontInternal(Context context, FontRequest fontRequest, int i) {
        try {
            FontFamilyResult fetchFonts = fetchFonts(context, null, fontRequest);
            if (fetchFonts.getStatusCode() == 0) {
                return TypefaceCompat.createFromFontInfo(context, null, fetchFonts.getFonts(), i);
            }
            return null;
        } catch (NameNotFoundException e) {
            return null;
        }
    }

    @RestrictTo({Scope.LIBRARY_GROUP})
    public static Typeface getFontSync(final Context context, final FontRequest fontRequest, @Nullable final TextView textView, int i, int i2, final int i3) {
        final String str = fontRequest.getIdentifier() + "-" + i3;
        Typeface typeface = (Typeface) sTypefaceCache.get(str);
        if (typeface != null) {
            return typeface;
        }
        boolean z = i == 0;
        if (z && i2 == -1) {
            return getFontInternal(context, fontRequest, i3);
        }
        C01681 r3 = new Callable<Typeface>() {
            public Typeface call() throws Exception {
                Typeface access$000 = FontsContractCompat.getFontInternal(context, fontRequest, i3);
                if (access$000 != null) {
                    FontsContractCompat.sTypefaceCache.put(str, access$000);
                }
                return access$000;
            }
        };
        if (z) {
            try {
                return (Typeface) sBackgroundThread.postAndWait(r3, i2);
            } catch (InterruptedException e) {
                return null;
            }
        } else {
            final WeakReference weakReference = new WeakReference(textView);
            C01692 r4 = new ReplyCallback<Typeface>() {
                public void onReply(Typeface typeface) {
                    if (((TextView) weakReference.get()) != null) {
                        textView.setTypeface(typeface, i3);
                    }
                }
            };
            synchronized (sLock) {
                if (sPendingReplies.containsKey(str)) {
                    ((ArrayList) sPendingReplies.get(str)).add(r4);
                    return null;
                }
                ArrayList arrayList = new ArrayList();
                arrayList.add(r4);
                sPendingReplies.put(str, arrayList);
                sBackgroundThread.postAndReply(r3, new ReplyCallback<Typeface>() {
                    public void onReply(Typeface typeface) {
                        ArrayList arrayList;
                        synchronized (FontsContractCompat.sLock) {
                            arrayList = (ArrayList) FontsContractCompat.sPendingReplies.get(str);
                            FontsContractCompat.sPendingReplies.remove(str);
                        }
                        int i = 0;
                        while (true) {
                            int i2 = i;
                            if (i2 < arrayList.size()) {
                                ((ReplyCallback) arrayList.get(i2)).onReply(typeface);
                                i = i2 + 1;
                            } else {
                                return;
                            }
                        }
                    }
                });
                return null;
            }
        }
    }

    @VisibleForTesting
    @RestrictTo({Scope.LIBRARY_GROUP})
    @Nullable
    public static ProviderInfo getProvider(@NonNull PackageManager packageManager, @NonNull FontRequest fontRequest, @Nullable Resources resources) throws NameNotFoundException {
        int i = 0;
        String providerAuthority = fontRequest.getProviderAuthority();
        ProviderInfo resolveContentProvider = packageManager.resolveContentProvider(providerAuthority, 0);
        if (resolveContentProvider == null) {
            throw new NameNotFoundException("No package found for authority: " + providerAuthority);
        } else if (!resolveContentProvider.packageName.equals(fontRequest.getProviderPackage())) {
            throw new NameNotFoundException("Found content provider " + providerAuthority + ", but package was not " + fontRequest.getProviderPackage());
        } else {
            List convertToByteArrayList = convertToByteArrayList(packageManager.getPackageInfo(resolveContentProvider.packageName, 64).signatures);
            Collections.sort(convertToByteArrayList, sByteArrayComparator);
            List certificates = getCertificates(fontRequest, resources);
            while (true) {
                int i2 = i;
                if (i2 >= certificates.size()) {
                    return null;
                }
                ArrayList arrayList = new ArrayList((Collection) certificates.get(i2));
                Collections.sort(arrayList, sByteArrayComparator);
                if (equalsByteArrayList(convertToByteArrayList, arrayList)) {
                    return resolveContentProvider;
                }
                i = i2 + 1;
            }
        }
    }

    @RequiresApi(19)
    @RestrictTo({Scope.LIBRARY_GROUP})
    public static Map<Uri, ByteBuffer> prepareFontData(Context context, FontInfo[] fontInfoArr, CancellationSignal cancellationSignal) {
        HashMap hashMap = new HashMap();
        for (FontInfo fontInfo : fontInfoArr) {
            if (fontInfo.getResultCode() == 0) {
                Uri uri = fontInfo.getUri();
                if (!hashMap.containsKey(uri)) {
                    hashMap.put(uri, TypefaceCompatUtil.mmap(context, cancellationSignal, uri));
                }
            }
        }
        return Collections.unmodifiableMap(hashMap);
    }

    public static void requestFont(@NonNull final Context context, @NonNull final FontRequest fontRequest, @NonNull final FontRequestCallback fontRequestCallback, @NonNull Handler handler) {
        final Handler handler2 = new Handler();
        handler.post(new Runnable() {
            public void run() {
                try {
                    FontFamilyResult fetchFonts = FontsContractCompat.fetchFonts(context, null, fontRequest);
                    if (fetchFonts.getStatusCode() != 0) {
                        switch (fetchFonts.getStatusCode()) {
                            case 1:
                                handler2.post(new Runnable() {
                                    public void run() {
                                        fontRequestCallback.onTypefaceRequestFailed(-2);
                                    }
                                });
                                return;
                            case 2:
                                handler2.post(new Runnable() {
                                    public void run() {
                                        fontRequestCallback.onTypefaceRequestFailed(-3);
                                    }
                                });
                                return;
                            default:
                                handler2.post(new Runnable() {
                                    public void run() {
                                        fontRequestCallback.onTypefaceRequestFailed(-3);
                                    }
                                });
                                return;
                        }
                    } else {
                        FontInfo[] fonts = fetchFonts.getFonts();
                        if (fonts == null || fonts.length == 0) {
                            handler2.post(new Runnable() {
                                public void run() {
                                    fontRequestCallback.onTypefaceRequestFailed(1);
                                }
                            });
                            return;
                        }
                        int length = fonts.length;
                        int i = 0;
                        while (i < length) {
                            FontInfo fontInfo = fonts[i];
                            if (fontInfo.getResultCode() != 0) {
                                final int resultCode = fontInfo.getResultCode();
                                if (resultCode < 0) {
                                    handler2.post(new Runnable() {
                                        public void run() {
                                            fontRequestCallback.onTypefaceRequestFailed(-3);
                                        }
                                    });
                                    return;
                                } else {
                                    handler2.post(new Runnable() {
                                        public void run() {
                                            fontRequestCallback.onTypefaceRequestFailed(resultCode);
                                        }
                                    });
                                    return;
                                }
                            } else {
                                i++;
                            }
                        }
                        final Typeface buildTypeface = FontsContractCompat.buildTypeface(context, null, fonts);
                        if (buildTypeface == null) {
                            handler2.post(new Runnable() {
                                public void run() {
                                    fontRequestCallback.onTypefaceRequestFailed(-3);
                                }
                            });
                        } else {
                            handler2.post(new Runnable() {
                                public void run() {
                                    fontRequestCallback.onTypefaceRetrieved(buildTypeface);
                                }
                            });
                        }
                    }
                } catch (NameNotFoundException e) {
                    handler2.post(new Runnable() {
                        public void run() {
                            fontRequestCallback.onTypefaceRequestFailed(-1);
                        }
                    });
                }
            }
        });
    }
}
