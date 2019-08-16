package com.facebook.internal;

import android.net.Uri;
import com.facebook.LoggingBehavior;
import com.facebook.internal.FileLruCache.Limits;
import java.io.IOException;
import java.io.OutputStream;

class UrlRedirectCache {
    private static final String REDIRECT_CONTENT_TAG = (TAG + "_Redirect");
    static final String TAG = UrlRedirectCache.class.getSimpleName();
    private static FileLruCache urlRedirectCache;

    UrlRedirectCache() {
    }

    static void cacheUriRedirect(Uri uri, Uri uri2) {
        OutputStream outputStream = null;
        if (uri != null && uri2 != null) {
            try {
                outputStream = getCache().openPutStream(uri.toString(), REDIRECT_CONTENT_TAG);
                outputStream.write(uri2.toString().getBytes());
            } catch (IOException e) {
            } finally {
                Utility.closeQuietly(outputStream);
            }
        }
    }

    static void clearCache() {
        try {
            getCache().clearCache();
        } catch (IOException e) {
            Logger.log(LoggingBehavior.CACHE, 5, TAG, "clearCache failed " + e.getMessage());
        }
    }

    static FileLruCache getCache() throws IOException {
        FileLruCache fileLruCache;
        synchronized (UrlRedirectCache.class) {
            try {
                if (urlRedirectCache == null) {
                    urlRedirectCache = new FileLruCache(TAG, new Limits());
                }
                fileLruCache = urlRedirectCache;
            } finally {
                Class<UrlRedirectCache> cls = UrlRedirectCache.class;
            }
        }
        return fileLruCache;
    }

    /* JADX WARNING: type inference failed for: r0v0 */
    /* JADX WARNING: type inference failed for: r1v1 */
    /* JADX WARNING: type inference failed for: r2v1, types: [java.io.Closeable] */
    /* JADX WARNING: type inference failed for: r2v2 */
    /* JADX WARNING: type inference failed for: r2v3 */
    /* JADX WARNING: type inference failed for: r1v4, types: [java.io.Closeable] */
    /* JADX WARNING: type inference failed for: r1v5 */
    /* JADX WARNING: type inference failed for: r1v6, types: [java.io.Closeable] */
    /* JADX WARNING: type inference failed for: r2v5 */
    /* JADX WARNING: type inference failed for: r0v6, types: [android.net.Uri] */
    /* JADX WARNING: type inference failed for: r2v7, types: [java.io.Closeable, java.io.InputStreamReader] */
    /* JADX WARNING: type inference failed for: r1v10 */
    /* JADX WARNING: type inference failed for: r1v12 */
    /* JADX WARNING: type inference failed for: r0v7, types: [android.net.Uri] */
    /* JADX WARNING: type inference failed for: r2v8 */
    /* JADX WARNING: type inference failed for: r1v13 */
    /* JADX WARNING: type inference failed for: r0v8 */
    /* JADX WARNING: type inference failed for: r2v9 */
    /* JADX WARNING: Multi-variable type inference failed. Error: jadx.core.utils.exceptions.JadxRuntimeException: No candidate types for var: r2v2
      assigns: []
      uses: []
      mth insns count: 44
    	at jadx.core.dex.visitors.typeinference.TypeSearch.fillTypeCandidates(TypeSearch.java:237)
    	at java.base/java.util.ArrayList.forEach(ArrayList.java:1540)
    	at jadx.core.dex.visitors.typeinference.TypeSearch.run(TypeSearch.java:53)
    	at jadx.core.dex.visitors.typeinference.TypeInferenceVisitor.runMultiVariableSearch(TypeInferenceVisitor.java:99)
    	at jadx.core.dex.visitors.typeinference.TypeInferenceVisitor.visit(TypeInferenceVisitor.java:92)
    	at jadx.core.dex.visitors.DepthTraversal.visit(DepthTraversal.java:27)
    	at jadx.core.dex.visitors.DepthTraversal.lambda$visit$1(DepthTraversal.java:14)
    	at java.base/java.util.ArrayList.forEach(ArrayList.java:1540)
    	at jadx.core.dex.visitors.DepthTraversal.visit(DepthTraversal.java:14)
    	at jadx.core.ProcessClass.process(ProcessClass.java:30)
    	at jadx.core.ProcessClass.lambda$processDependencies$0(ProcessClass.java:49)
    	at java.base/java.util.ArrayList.forEach(ArrayList.java:1540)
    	at jadx.core.ProcessClass.processDependencies(ProcessClass.java:49)
    	at jadx.core.ProcessClass.process(ProcessClass.java:35)
    	at jadx.api.JadxDecompiler.processClass(JadxDecompiler.java:311)
    	at jadx.api.JavaClass.decompile(JavaClass.java:62)
    	at jadx.api.JadxDecompiler.lambda$appendSourcesSave$0(JadxDecompiler.java:217)
     */
    /* JADX WARNING: Unknown variable types count: 7 */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    static android.net.Uri getRedirectedUri(android.net.Uri r8) {
        /*
            r4 = 0
            r0 = 0
            if (r8 != 0) goto L_0x0005
        L_0x0004:
            return r0
        L_0x0005:
            java.lang.String r2 = r8.toString()
            com.facebook.internal.FileLruCache r5 = getCache()     // Catch:{ IOException -> 0x005e, all -> 0x0050 }
            r3 = r2
            r1 = r0
        L_0x000f:
            java.lang.String r2 = REDIRECT_CONTENT_TAG     // Catch:{ IOException -> 0x005a, all -> 0x0057 }
            java.io.InputStream r6 = r5.get(r3, r2)     // Catch:{ IOException -> 0x005a, all -> 0x0057 }
            if (r6 == 0) goto L_0x0042
            r4 = 1
            java.io.InputStreamReader r2 = new java.io.InputStreamReader     // Catch:{ IOException -> 0x005a, all -> 0x0057 }
            r2.<init>(r6)     // Catch:{ IOException -> 0x005a, all -> 0x0057 }
            r1 = 128(0x80, float:1.794E-43)
            char[] r1 = new char[r1]     // Catch:{ IOException -> 0x0033, all -> 0x005c }
            java.lang.StringBuilder r3 = new java.lang.StringBuilder     // Catch:{ IOException -> 0x0033, all -> 0x005c }
            r3.<init>()     // Catch:{ IOException -> 0x0033, all -> 0x005c }
        L_0x0026:
            r6 = 0
            int r7 = r1.length     // Catch:{ IOException -> 0x0033, all -> 0x005c }
            int r6 = r2.read(r1, r6, r7)     // Catch:{ IOException -> 0x0033, all -> 0x005c }
            if (r6 <= 0) goto L_0x0039
            r7 = 0
            r3.append(r1, r7, r6)     // Catch:{ IOException -> 0x0033, all -> 0x005c }
            goto L_0x0026
        L_0x0033:
            r1 = move-exception
            r1 = r2
        L_0x0035:
            com.facebook.internal.Utility.closeQuietly(r1)
            goto L_0x0004
        L_0x0039:
            com.facebook.internal.Utility.closeQuietly(r2)     // Catch:{ IOException -> 0x0033, all -> 0x005c }
            java.lang.String r3 = r3.toString()     // Catch:{ IOException -> 0x0033, all -> 0x005c }
            r1 = r2
            goto L_0x000f
        L_0x0042:
            if (r4 == 0) goto L_0x004c
            android.net.Uri r0 = android.net.Uri.parse(r3)     // Catch:{ IOException -> 0x005a, all -> 0x0057 }
            com.facebook.internal.Utility.closeQuietly(r1)
            goto L_0x0004
        L_0x004c:
            com.facebook.internal.Utility.closeQuietly(r1)
            goto L_0x0004
        L_0x0050:
            r1 = move-exception
            r2 = r0
        L_0x0052:
            r0 = r1
        L_0x0053:
            com.facebook.internal.Utility.closeQuietly(r2)
            throw r0
        L_0x0057:
            r0 = move-exception
            r2 = r1
            goto L_0x0053
        L_0x005a:
            r2 = move-exception
            goto L_0x0035
        L_0x005c:
            r1 = move-exception
            goto L_0x0052
        L_0x005e:
            r1 = move-exception
            r1 = r0
            goto L_0x0035
        */
        throw new UnsupportedOperationException("Method not decompiled: com.facebook.internal.UrlRedirectCache.getRedirectedUri(android.net.Uri):android.net.Uri");
    }
}
