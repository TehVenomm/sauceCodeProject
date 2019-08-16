package p018jp.colopl.util;

import android.net.Uri;
import android.text.TextUtils;
import android.util.Log;
import java.net.URI;
import java.net.URISyntaxException;
import java.util.List;
import org.apache.http.NameValuePair;
import org.apache.http.client.utils.URLEncodedUtils;

/* renamed from: jp.colopl.util.StringUtil */
public class StringUtil {
    private static final String TAG = "StringUtil";

    /* JADX WARNING: type inference failed for: r0v0 */
    /* JADX WARNING: type inference failed for: r2v0 */
    /* JADX WARNING: type inference failed for: r2v1 */
    /* JADX WARNING: type inference failed for: r2v2 */
    /* JADX WARNING: type inference failed for: r2v3, types: [java.io.BufferedReader] */
    /* JADX WARNING: type inference failed for: r2v5, types: [java.io.BufferedReader] */
    /* JADX WARNING: type inference failed for: r2v7 */
    /* JADX WARNING: type inference failed for: r0v6, types: [java.lang.String] */
    /* JADX WARNING: type inference failed for: r2v8, types: [java.io.BufferedReader] */
    /* JADX WARNING: type inference failed for: r2v11 */
    /* JADX WARNING: type inference failed for: r2v12 */
    /* JADX WARNING: type inference failed for: r2v13 */
    /* JADX WARNING: type inference failed for: r2v14, types: [java.io.BufferedReader] */
    /* JADX WARNING: type inference failed for: r0v11, types: [java.lang.String] */
    /* JADX WARNING: type inference failed for: r2v16 */
    /* JADX WARNING: type inference failed for: r2v17 */
    /* JADX WARNING: type inference failed for: r2v18 */
    /* JADX WARNING: type inference failed for: r2v19 */
    /* JADX WARNING: type inference failed for: r2v20 */
    /* JADX WARNING: type inference failed for: r2v21 */
    /* JADX WARNING: type inference failed for: r2v22 */
    /* JADX WARNING: type inference failed for: r2v23 */
    /* JADX WARNING: Multi-variable type inference failed. Error: jadx.core.utils.exceptions.JadxRuntimeException: No candidate types for var: r2v0
      assigns: []
      uses: []
      mth insns count: 77
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
    /* JADX WARNING: Removed duplicated region for block: B:17:0x0031 A[SYNTHETIC, Splitter:B:17:0x0031] */
    /* JADX WARNING: Removed duplicated region for block: B:39:0x0068 A[SYNTHETIC, Splitter:B:39:0x0068] */
    /* JADX WARNING: Removed duplicated region for block: B:48:0x007e A[SYNTHETIC, Splitter:B:48:0x007e] */
    /* JADX WARNING: Unknown variable types count: 6 */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    public static java.lang.String convertToString(java.io.InputStream r5) {
        /*
            r0 = 0
            java.io.InputStreamReader r1 = new java.io.InputStreamReader     // Catch:{ UnsupportedEncodingException -> 0x0093, IOException -> 0x0058, all -> 0x00a3 }
            java.lang.String r2 = "UTF-8"
            r1.<init>(r5, r2)     // Catch:{ UnsupportedEncodingException -> 0x0093, IOException -> 0x0058, all -> 0x00a3 }
            java.io.BufferedReader r2 = new java.io.BufferedReader     // Catch:{ UnsupportedEncodingException -> 0x0096, IOException -> 0x0090, all -> 0x0077 }
            r2.<init>(r1)     // Catch:{ UnsupportedEncodingException -> 0x0096, IOException -> 0x0090, all -> 0x0077 }
            java.lang.StringBuilder r1 = new java.lang.StringBuilder     // Catch:{ UnsupportedEncodingException -> 0x0022, IOException -> 0x00a1, all -> 0x008d }
            r1.<init>()     // Catch:{ UnsupportedEncodingException -> 0x0022, IOException -> 0x00a1, all -> 0x008d }
        L_0x0012:
            java.lang.String r3 = r2.readLine()     // Catch:{ UnsupportedEncodingException -> 0x0022, IOException -> 0x00a1, all -> 0x008d }
            if (r3 == 0) goto L_0x0035
            java.lang.StringBuilder r3 = r1.append(r3)     // Catch:{ UnsupportedEncodingException -> 0x0022, IOException -> 0x00a1, all -> 0x008d }
            java.lang.String r4 = "\n"
            r3.append(r4)     // Catch:{ UnsupportedEncodingException -> 0x0022, IOException -> 0x00a1, all -> 0x008d }
            goto L_0x0012
        L_0x0022:
            r1 = move-exception
        L_0x0023:
            java.lang.String r3 = "StringUtil Error:"
            java.lang.String r1 = r1.getMessage()     // Catch:{ all -> 0x00a6 }
            android.util.Log.e(r3, r1)     // Catch:{ all -> 0x00a6 }
            r5.close()     // Catch:{ IOException -> 0x0099 }
        L_0x002f:
            if (r2 == 0) goto L_0x0034
            r2.close()     // Catch:{ IOException -> 0x004d }
        L_0x0034:
            return r0
        L_0x0035:
            java.lang.String r0 = r1.toString()     // Catch:{ UnsupportedEncodingException -> 0x0022, IOException -> 0x00a1, all -> 0x008d }
            r5.close()     // Catch:{ IOException -> 0x009b }
        L_0x003c:
            if (r2 == 0) goto L_0x0034
            r2.close()     // Catch:{ IOException -> 0x0042 }
            goto L_0x0034
        L_0x0042:
            r1 = move-exception
            java.lang.String r2 = "StringUtil:"
            java.lang.String r1 = r1.toString()
            android.util.Log.e(r2, r1)
            goto L_0x0034
        L_0x004d:
            r1 = move-exception
            java.lang.String r2 = "StringUtil:"
            java.lang.String r1 = r1.toString()
            android.util.Log.e(r2, r1)
            goto L_0x0034
        L_0x0058:
            r1 = move-exception
            r2 = r0
        L_0x005a:
            java.lang.String r3 = "StringUtil Error:"
            java.lang.String r1 = r1.toString()     // Catch:{ all -> 0x00a6 }
            android.util.Log.e(r3, r1)     // Catch:{ all -> 0x00a6 }
            r5.close()     // Catch:{ IOException -> 0x009d }
        L_0x0066:
            if (r2 == 0) goto L_0x0034
            r2.close()     // Catch:{ IOException -> 0x006c }
            goto L_0x0034
        L_0x006c:
            r1 = move-exception
            java.lang.String r2 = "StringUtil:"
            java.lang.String r1 = r1.toString()
            android.util.Log.e(r2, r1)
            goto L_0x0034
        L_0x0077:
            r1 = move-exception
            r2 = r0
        L_0x0079:
            r5.close()     // Catch:{ IOException -> 0x009f }
        L_0x007c:
            if (r2 == 0) goto L_0x0081
            r2.close()     // Catch:{ IOException -> 0x0082 }
        L_0x0081:
            throw r1
        L_0x0082:
            r0 = move-exception
            java.lang.String r2 = "StringUtil:"
            java.lang.String r0 = r0.toString()
            android.util.Log.e(r2, r0)
            goto L_0x0081
        L_0x008d:
            r0 = move-exception
            r1 = r0
            goto L_0x0079
        L_0x0090:
            r1 = move-exception
            r2 = r0
            goto L_0x005a
        L_0x0093:
            r1 = move-exception
            r2 = r0
            goto L_0x0023
        L_0x0096:
            r1 = move-exception
            r2 = r0
            goto L_0x0023
        L_0x0099:
            r1 = move-exception
            goto L_0x002f
        L_0x009b:
            r1 = move-exception
            goto L_0x003c
        L_0x009d:
            r1 = move-exception
            goto L_0x0066
        L_0x009f:
            r0 = move-exception
            goto L_0x007c
        L_0x00a1:
            r1 = move-exception
            goto L_0x005a
        L_0x00a3:
            r1 = move-exception
            r2 = r0
            goto L_0x0079
        L_0x00a6:
            r0 = move-exception
            r1 = r0
            goto L_0x0079
        */
        throw new UnsupportedOperationException("Method not decompiled: p018jp.colopl.util.StringUtil.convertToString(java.io.InputStream):java.lang.String");
    }

    public static String generateHashByJoiningColon(String str, String[] strArr) {
        boolean z = false;
        try {
            return Crypto.getMD5withSalt(TextUtils.join(":", strArr) + ":", str);
        } catch (Exception e) {
            e.printStackTrace();
            return z;
        }
    }

    public static String getQueryParamter(String str, String str2) {
        try {
            List<NameValuePair> parse = URLEncodedUtils.parse(new URI(str), "UTF-8");
            if (parse == null) {
                return null;
            }
            for (NameValuePair nameValuePair : parse) {
                String name = nameValuePair.getName();
                String value = nameValuePair.getValue();
                if (name.equals(str2)) {
                    return value;
                }
            }
            return null;
        } catch (URISyntaxException e) {
            Log.e(TAG, "failed to new URI object. url = " + str + ", Exception = " + e.toString());
            return null;
        }
    }

    public static boolean isMarketUrl(String str) {
        Uri parse = Uri.parse(str);
        String host = parse.getHost();
        String scheme = parse.getScheme();
        return scheme.equals("auonemkt") || scheme.equals("market") || host.endsWith("market.auone.jp") || host.endsWith("market.android.com");
    }

    public static boolean isSameHost(String str, String str2) {
        if (str == null || str2 == null) {
            return false;
        }
        Uri parse = Uri.parse(str);
        if (parse.getHost() == null) {
            return false;
        }
        Uri parse2 = Uri.parse(str2);
        if (parse2.getHost() != null) {
            return parse.getHost().equals(parse2.getHost());
        }
        return false;
    }
}
