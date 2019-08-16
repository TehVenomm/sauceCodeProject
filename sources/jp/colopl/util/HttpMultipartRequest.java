package p018jp.colopl.util;

import java.io.File;
import java.util.List;
import org.apache.http.NameValuePair;

/* renamed from: jp.colopl.util.HttpMultipartRequest */
public class HttpMultipartRequest {
    private static final String BOUNDARY = "----------V2ymHFg03ehbqgZCaKO6jy";
    private static final int COUNT_UNIT_BYTES = 4096;
    private static final String TAG = "HttpMultipartRequest";
    private int connectionTimeout;
    private List<String> cookies;
    private File file;
    private ProgressCallback mCallback;
    private List<NameValuePair> postData;
    private int readingTimeout;
    private String url;
    private String userAgent;

    /* renamed from: jp.colopl.util.HttpMultipartRequest$ProgressCallback */
    public interface ProgressCallback {
        void progress(int i, int i2);
    }

    public HttpMultipartRequest(String str, List<NameValuePair> list, List<String> list2, File file2, String str2, ProgressCallback progressCallback) {
        this.connectionTimeout = 900000;
        this.readingTimeout = 900000;
        if (str == null || file2 == null) {
            throw new IllegalArgumentException();
        }
        this.url = str;
        this.postData = list;
        this.cookies = list2;
        this.file = file2;
        this.userAgent = str2;
        this.mCallback = progressCallback;
    }

    public HttpMultipartRequest(String str, List<NameValuePair> list, List<String> list2, String str2, String str3, ProgressCallback progressCallback) {
        this(str, list, list2, new File(str2), str3, progressCallback);
    }

    private String getBoundaryMessage(List<NameValuePair> list, String str) {
        StringBuffer append = new StringBuffer("--").append(BOUNDARY).append("\r\n");
        if (list != null) {
            for (NameValuePair nameValuePair : list) {
                append.append("Content-Disposition: form-data; name=\"").append(nameValuePair.getName()).append("\"\r\n").append("\r\n").append(nameValuePair.getValue()).append("\r\n").append("--").append(BOUNDARY).append("\r\n");
            }
        }
        String[] split = str.split("\\.");
        append.append("Content-Disposition: form-data; name=\"file\"").append("; filename=\"").append(str).append("\"\r\n").append("Content-Type: ").append("image/" + split[split.length - 1]).append("\r\n\r\n");
        return append.toString();
    }

    /* JADX WARNING: Removed duplicated region for block: B:13:0x0022 A[SYNTHETIC, Splitter:B:13:0x0022] */
    /* JADX WARNING: Removed duplicated region for block: B:30:0x003f A[SYNTHETIC, Splitter:B:30:0x003f] */
    /* JADX WARNING: Removed duplicated region for block: B:38:0x004c A[SYNTHETIC, Splitter:B:38:0x004c] */
    /* JADX WARNING: Unknown top exception splitter block from list: {B:25:0x0037=Splitter:B:25:0x0037, B:8:0x001a=Splitter:B:8:0x001a} */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    private byte[] getImageBytes(java.io.File r5) {
        /*
            r4 = this;
            r2 = 0
            r0 = 10
            byte[] r0 = new byte[r0]
            java.io.ByteArrayOutputStream r3 = new java.io.ByteArrayOutputStream
            r3.<init>()
            java.io.FileInputStream r1 = new java.io.FileInputStream     // Catch:{ FileNotFoundException -> 0x0050, IOException -> 0x0035, all -> 0x0045 }
            r1.<init>(r5)     // Catch:{ FileNotFoundException -> 0x0050, IOException -> 0x0035, all -> 0x0045 }
        L_0x000f:
            int r2 = r1.read(r0)     // Catch:{ FileNotFoundException -> 0x0019, IOException -> 0x005f, all -> 0x0061 }
            if (r2 <= 0) goto L_0x002a
            r3.write(r0)     // Catch:{ FileNotFoundException -> 0x0019, IOException -> 0x005f, all -> 0x0061 }
            goto L_0x000f
        L_0x0019:
            r0 = move-exception
        L_0x001a:
            r0.printStackTrace()     // Catch:{ all -> 0x0063 }
            r3.close()     // Catch:{ IOException -> 0x0053 }
        L_0x0020:
            if (r1 == 0) goto L_0x0025
            r1.close()     // Catch:{ IOException -> 0x0055 }
        L_0x0025:
            byte[] r0 = r3.toByteArray()
            return r0
        L_0x002a:
            r3.close()     // Catch:{ IOException -> 0x0057 }
        L_0x002d:
            if (r1 == 0) goto L_0x0025
            r1.close()     // Catch:{ IOException -> 0x0033 }
            goto L_0x0025
        L_0x0033:
            r0 = move-exception
            goto L_0x0025
        L_0x0035:
            r0 = move-exception
            r1 = r2
        L_0x0037:
            r0.printStackTrace()     // Catch:{ all -> 0x0063 }
            r3.close()     // Catch:{ IOException -> 0x0059 }
        L_0x003d:
            if (r1 == 0) goto L_0x0025
            r1.close()     // Catch:{ IOException -> 0x0043 }
            goto L_0x0025
        L_0x0043:
            r0 = move-exception
            goto L_0x0025
        L_0x0045:
            r0 = move-exception
        L_0x0046:
            r1 = r2
        L_0x0047:
            r3.close()     // Catch:{ IOException -> 0x005b }
        L_0x004a:
            if (r1 == 0) goto L_0x004f
            r1.close()     // Catch:{ IOException -> 0x005d }
        L_0x004f:
            throw r0
        L_0x0050:
            r0 = move-exception
            r1 = r2
            goto L_0x001a
        L_0x0053:
            r0 = move-exception
            goto L_0x0020
        L_0x0055:
            r0 = move-exception
            goto L_0x0025
        L_0x0057:
            r0 = move-exception
            goto L_0x002d
        L_0x0059:
            r0 = move-exception
            goto L_0x003d
        L_0x005b:
            r2 = move-exception
            goto L_0x004a
        L_0x005d:
            r1 = move-exception
            goto L_0x004f
        L_0x005f:
            r0 = move-exception
            goto L_0x0037
        L_0x0061:
            r0 = move-exception
            goto L_0x0047
        L_0x0063:
            r0 = move-exception
            r2 = r1
            goto L_0x0046
        */
        throw new UnsupportedOperationException("Method not decompiled: p018jp.colopl.util.HttpMultipartRequest.getImageBytes(java.io.File):byte[]");
    }

    /* JADX WARNING: type inference failed for: r1v10 */
    /* JADX WARNING: type inference failed for: r1v11 */
    /* JADX WARNING: Multi-variable type inference failed */
    /* JADX WARNING: Removed duplicated region for block: B:17:0x0063  */
    /* JADX WARNING: Removed duplicated region for block: B:33:0x00b4  */
    /* JADX WARNING: Removed duplicated region for block: B:50:0x00f7  */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    public java.lang.String send() {
        /*
            r9 = this;
            r3 = 0
            java.net.URL r1 = new java.net.URL     // Catch:{ IOException -> 0x00ee, all -> 0x00af }
            java.lang.String r2 = r9.url     // Catch:{ IOException -> 0x00ee, all -> 0x00af }
            r1.<init>(r2)     // Catch:{ IOException -> 0x00ee, all -> 0x00af }
            java.net.URLConnection r2 = r1.openConnection()     // Catch:{ IOException -> 0x00ee, all -> 0x00af }
            java.lang.String r1 = "Content-Type"
            java.lang.String r4 = "multipart/form-data; boundary=----------V2ymHFg03ehbqgZCaKO6jy"
            r2.setRequestProperty(r1, r4)     // Catch:{ IOException -> 0x0056, all -> 0x00e7 }
            java.lang.String r1 = "User-Agent"
            java.lang.String r4 = r9.userAgent     // Catch:{ IOException -> 0x0056, all -> 0x00e7 }
            r2.setRequestProperty(r1, r4)     // Catch:{ IOException -> 0x0056, all -> 0x00e7 }
            int r1 = r9.connectionTimeout     // Catch:{ IOException -> 0x0056, all -> 0x00e7 }
            r2.setConnectTimeout(r1)     // Catch:{ IOException -> 0x0056, all -> 0x00e7 }
            int r1 = r9.readingTimeout     // Catch:{ IOException -> 0x0056, all -> 0x00e7 }
            r2.setReadTimeout(r1)     // Catch:{ IOException -> 0x0056, all -> 0x00e7 }
            r0 = r2
            java.net.HttpURLConnection r0 = (java.net.HttpURLConnection) r0     // Catch:{ IOException -> 0x0056, all -> 0x00e7 }
            r1 = r0
            java.lang.String r4 = "POST"
            r1.setRequestMethod(r4)     // Catch:{ IOException -> 0x0056, all -> 0x00e7 }
            r1 = 1
            r2.setDoOutput(r1)     // Catch:{ IOException -> 0x0056, all -> 0x00e7 }
            java.util.List<java.lang.String> r1 = r9.cookies     // Catch:{ IOException -> 0x0056, all -> 0x00e7 }
            if (r1 == 0) goto L_0x0073
            java.lang.StringBuffer r4 = new java.lang.StringBuffer     // Catch:{ IOException -> 0x0056, all -> 0x00e7 }
            r4.<init>()     // Catch:{ IOException -> 0x0056, all -> 0x00e7 }
            java.util.List<java.lang.String> r1 = r9.cookies     // Catch:{ IOException -> 0x0056, all -> 0x00e7 }
            java.util.Iterator r5 = r1.iterator()     // Catch:{ IOException -> 0x0056, all -> 0x00e7 }
        L_0x0040:
            boolean r1 = r5.hasNext()     // Catch:{ IOException -> 0x0056, all -> 0x00e7 }
            if (r1 == 0) goto L_0x006a
            java.lang.Object r1 = r5.next()     // Catch:{ IOException -> 0x0056, all -> 0x00e7 }
            java.lang.String r1 = (java.lang.String) r1     // Catch:{ IOException -> 0x0056, all -> 0x00e7 }
            java.lang.StringBuffer r1 = r4.append(r1)     // Catch:{ IOException -> 0x0056, all -> 0x00e7 }
            java.lang.String r6 = "; "
            r1.append(r6)     // Catch:{ IOException -> 0x0056, all -> 0x00e7 }
            goto L_0x0040
        L_0x0056:
            r4 = move-exception
            r1 = r2
        L_0x0058:
            java.lang.String r2 = ""
            java.lang.String r4 = r4.getMessage()     // Catch:{ all -> 0x00eb }
            p018jp.colopl.util.Util.dLog(r2, r4)     // Catch:{ all -> 0x00eb }
            if (r1 == 0) goto L_0x00f7
            java.net.HttpURLConnection r1 = (java.net.HttpURLConnection) r1
            r1.disconnect()
            r1 = r3
        L_0x0069:
            return r1
        L_0x006a:
            java.lang.String r1 = "Cookie"
            java.lang.String r4 = r4.toString()     // Catch:{ IOException -> 0x0056, all -> 0x00e7 }
            r2.setRequestProperty(r1, r4)     // Catch:{ IOException -> 0x0056, all -> 0x00e7 }
        L_0x0073:
            r2.connect()     // Catch:{ IOException -> 0x0056, all -> 0x00e7 }
            java.io.OutputStream r5 = r2.getOutputStream()     // Catch:{ IOException -> 0x0056, all -> 0x00e7 }
            java.util.List<org.apache.http.NameValuePair> r1 = r9.postData     // Catch:{ IOException -> 0x0056, all -> 0x00e7 }
            java.io.File r4 = r9.file     // Catch:{ IOException -> 0x0056, all -> 0x00e7 }
            java.lang.String r4 = r4.getName()     // Catch:{ IOException -> 0x0056, all -> 0x00e7 }
            java.lang.String r1 = r9.getBoundaryMessage(r1, r4)     // Catch:{ IOException -> 0x0056, all -> 0x00e7 }
            byte[] r1 = r1.getBytes()     // Catch:{ IOException -> 0x0056, all -> 0x00e7 }
            r5.write(r1)     // Catch:{ IOException -> 0x0056, all -> 0x00e7 }
            java.io.File r1 = r9.file     // Catch:{ IOException -> 0x0056, all -> 0x00e7 }
            byte[] r6 = r9.getImageBytes(r1)     // Catch:{ IOException -> 0x0056, all -> 0x00e7 }
            int r7 = r6.length     // Catch:{ IOException -> 0x0056, all -> 0x00e7 }
            r1 = 0
            r4 = r1
        L_0x0096:
            if (r4 >= r7) goto L_0x00ba
            r1 = 4096(0x1000, float:5.74E-42)
            int r8 = r4 + 4096
            if (r8 <= r7) goto L_0x00a0
            int r1 = r7 - r4
        L_0x00a0:
            r5.write(r6, r4, r1)     // Catch:{ IOException -> 0x0056, all -> 0x00e7 }
            int r1 = r1 + r4
            jp.colopl.util.HttpMultipartRequest$ProgressCallback r4 = r9.mCallback     // Catch:{ IOException -> 0x0056, all -> 0x00e7 }
            if (r4 == 0) goto L_0x00f5
            jp.colopl.util.HttpMultipartRequest$ProgressCallback r4 = r9.mCallback     // Catch:{ IOException -> 0x0056, all -> 0x00e7 }
            r4.progress(r1, r7)     // Catch:{ IOException -> 0x0056, all -> 0x00e7 }
            r4 = r1
            goto L_0x0096
        L_0x00af:
            r2 = move-exception
            r4 = r2
            r1 = r3
        L_0x00b2:
            if (r1 == 0) goto L_0x00b9
            java.net.HttpURLConnection r1 = (java.net.HttpURLConnection) r1
            r1.disconnect()
        L_0x00b9:
            throw r4
        L_0x00ba:
            java.lang.String r1 = "\r\n------------V2ymHFg03ehbqgZCaKO6jy--\r\n"
            byte[] r1 = r1.getBytes()     // Catch:{ IOException -> 0x0056, all -> 0x00e7 }
            r5.write(r1)     // Catch:{ IOException -> 0x0056, all -> 0x00e7 }
            r5.close()     // Catch:{ IOException -> 0x0056, all -> 0x00e7 }
            r0 = r2
            java.net.HttpURLConnection r0 = (java.net.HttpURLConnection) r0     // Catch:{ IOException -> 0x0056, all -> 0x00e7 }
            r1 = r0
            int r1 = r1.getResponseCode()     // Catch:{ IOException -> 0x0056, all -> 0x00e7 }
            r4 = 200(0xc8, float:2.8E-43)
            if (r1 != r4) goto L_0x00f3
            jp.colopl.util.DoneHandlerInputStream r1 = new jp.colopl.util.DoneHandlerInputStream     // Catch:{ IOException -> 0x0056, all -> 0x00e7 }
            java.io.InputStream r4 = r2.getInputStream()     // Catch:{ IOException -> 0x0056, all -> 0x00e7 }
            r1.<init>(r4)     // Catch:{ IOException -> 0x0056, all -> 0x00e7 }
            java.lang.String r1 = p018jp.colopl.util.StringUtil.convertToString(r1)     // Catch:{ IOException -> 0x0056, all -> 0x00e7 }
        L_0x00df:
            if (r2 == 0) goto L_0x0069
            java.net.HttpURLConnection r2 = (java.net.HttpURLConnection) r2
            r2.disconnect()
            goto L_0x0069
        L_0x00e7:
            r3 = move-exception
            r4 = r3
            r1 = r2
            goto L_0x00b2
        L_0x00eb:
            r2 = move-exception
            r4 = r2
            goto L_0x00b2
        L_0x00ee:
            r2 = move-exception
            r4 = r2
            r1 = r3
            goto L_0x0058
        L_0x00f3:
            r1 = r3
            goto L_0x00df
        L_0x00f5:
            r4 = r1
            goto L_0x0096
        L_0x00f7:
            r1 = r3
            goto L_0x0069
        */
        throw new UnsupportedOperationException("Method not decompiled: p018jp.colopl.util.HttpMultipartRequest.send():java.lang.String");
    }

    public void setConnectionTimeout(int i) {
        this.connectionTimeout = i;
    }

    public void setReadingTimeout(int i) {
        this.readingTimeout = i;
    }
}
