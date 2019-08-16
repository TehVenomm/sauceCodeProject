package p018jp.colopl.util;

import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import java.io.File;
import java.io.IOException;
import java.io.InputStream;
import java.net.MalformedURLException;
import java.net.URL;
import java.util.ArrayList;
import java.util.List;
import org.apache.http.Header;
import org.apache.http.HttpEntity;
import org.apache.http.HttpResponse;
import org.apache.http.HttpVersion;
import org.apache.http.NameValuePair;
import org.apache.http.client.entity.UrlEncodedFormEntity;
import org.apache.http.client.methods.HttpPost;
import org.apache.http.entity.StringEntity;
import org.apache.http.impl.client.DefaultHttpClient;
import org.apache.http.params.HttpConnectionParams;
import org.apache.http.params.HttpParams;
import org.apache.http.params.HttpProtocolParams;
import p017io.fabric.sdk.android.services.network.HttpRequest;
import p018jp.colopl.config.Config;
import p018jp.colopl.drapro.AppConsts;
import p018jp.colopl.util.HttpMultipartRequest.ProgressCallback;

/* renamed from: jp.colopl.util.HTTP */
public class HTTP {
    private static int CONNECT_TIMEOUT = 10000;
    private static int CONNECT_TIMEOUT_FOR_UPLOAD = 300000;
    private static int READ_TIMEOUT = 10000;
    private static int READ_TIMEOUT_FOR_UPLOAD = 300000;
    private static String userAgent;

    public static List<String> createCookies(Config config) {
        ArrayList arrayList = new ArrayList();
        arrayList.add(config.getAndroidTokenCookieName() + "=" + config.generateToken());
        arrayList.add(config.getSession().getName() + "=" + config.getSession().getSid());
        arrayList.add(config.getAppVersionCookieName() + "=" + config.getVersionCode());
        arrayList.add(config.getOSVersionCookieName() + "=" + config.getOSVersion());
        arrayList.add(config.getAppTypeCookieName() + "=" + config.getAppTypeCookieValue());
        if (Config.debuggable) {
            long currentTimeMillis = System.currentTimeMillis();
            try {
                arrayList.add("_dyhsiuj=" + Crypto.encryptMD5("lhmovdkjedd," + String.valueOf(currentTimeMillis)));
            } catch (Exception e) {
                e.printStackTrace();
            }
            arrayList.add("_yomr=" + String.valueOf(currentTimeMillis));
        }
        return arrayList;
    }

    /* JADX WARNING: type inference failed for: r1v8 */
    /* JADX WARNING: type inference failed for: r1v16 */
    /* JADX WARNING: Multi-variable type inference failed */
    /* JADX WARNING: Removed duplicated region for block: B:14:0x0056  */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    public static java.lang.String get(java.lang.String r6, java.util.List<java.lang.String> r7) {
        /*
            r4 = 0
            java.net.URL r1 = new java.net.URL     // Catch:{ Exception -> 0x007f }
            r1.<init>(r6)     // Catch:{ Exception -> 0x007f }
            java.net.URLConnection r2 = r1.openConnection()     // Catch:{ Exception -> 0x007f }
            java.lang.String r1 = "User-Agent"
            java.lang.String r3 = getUserAgent()     // Catch:{ Exception -> 0x0051 }
            r2.setRequestProperty(r1, r3)     // Catch:{ Exception -> 0x0051 }
            java.lang.String r1 = "apv"
            java.lang.String r3 = p018jp.colopl.drapro.AppConsts.versionName     // Catch:{ Exception -> 0x0051 }
            r2.setRequestProperty(r1, r3)     // Catch:{ Exception -> 0x0051 }
            r0 = r2
            java.net.HttpURLConnection r0 = (java.net.HttpURLConnection) r0     // Catch:{ Exception -> 0x0051 }
            r1 = r0
            java.lang.String r3 = "GET"
            r1.setRequestMethod(r3)     // Catch:{ Exception -> 0x0051 }
            r1 = 0
            r2.setUseCaches(r1)     // Catch:{ Exception -> 0x0051 }
            int r1 = CONNECT_TIMEOUT     // Catch:{ Exception -> 0x0051 }
            r2.setConnectTimeout(r1)     // Catch:{ Exception -> 0x0051 }
            int r1 = READ_TIMEOUT     // Catch:{ Exception -> 0x0051 }
            r2.setReadTimeout(r1)     // Catch:{ Exception -> 0x0051 }
            if (r7 == 0) goto L_0x0066
            java.lang.StringBuilder r3 = new java.lang.StringBuilder     // Catch:{ Exception -> 0x0051 }
            r3.<init>()     // Catch:{ Exception -> 0x0051 }
            java.util.Iterator r5 = r7.iterator()     // Catch:{ Exception -> 0x0051 }
        L_0x003c:
            boolean r1 = r5.hasNext()     // Catch:{ Exception -> 0x0051 }
            if (r1 == 0) goto L_0x005d
            java.lang.Object r1 = r5.next()     // Catch:{ Exception -> 0x0051 }
            java.lang.String r1 = (java.lang.String) r1     // Catch:{ Exception -> 0x0051 }
            r3.append(r1)     // Catch:{ Exception -> 0x0051 }
            java.lang.String r1 = ";"
            r3.append(r1)     // Catch:{ Exception -> 0x0051 }
            goto L_0x003c
        L_0x0051:
            r1 = move-exception
            r1 = r2
            r3 = r4
        L_0x0054:
            if (r1 == 0) goto L_0x005b
            java.net.HttpURLConnection r1 = (java.net.HttpURLConnection) r1
            r1.disconnect()
        L_0x005b:
            r1 = r3
        L_0x005c:
            return r1
        L_0x005d:
            java.lang.String r1 = "Cookie"
            java.lang.String r3 = r3.toString()     // Catch:{ Exception -> 0x0051 }
            r2.setRequestProperty(r1, r3)     // Catch:{ Exception -> 0x0051 }
        L_0x0066:
            r2.connect()     // Catch:{ Exception -> 0x0051 }
            jp.colopl.util.DoneHandlerInputStream r1 = new jp.colopl.util.DoneHandlerInputStream     // Catch:{ Exception -> 0x0051 }
            java.io.InputStream r3 = r2.getInputStream()     // Catch:{ Exception -> 0x0051 }
            r1.<init>(r3)     // Catch:{ Exception -> 0x0051 }
            java.lang.String r3 = p018jp.colopl.util.StringUtil.convertToString(r1)     // Catch:{ Exception -> 0x0051 }
            r0 = r2
            java.net.HttpURLConnection r0 = (java.net.HttpURLConnection) r0     // Catch:{ Exception -> 0x0083 }
            r1 = r0
            r1.disconnect()     // Catch:{ Exception -> 0x0083 }
            r1 = r3
            goto L_0x005c
        L_0x007f:
            r1 = move-exception
            r1 = r4
            r3 = r4
            goto L_0x0054
        L_0x0083:
            r1 = move-exception
            r1 = r2
            goto L_0x0054
        */
        throw new UnsupportedOperationException("Method not decompiled: p018jp.colopl.util.HTTP.get(java.lang.String, java.util.List):java.lang.String");
    }

    private static String getUserAgent() {
        if (userAgent == null) {
            userAgent = "Android";
        }
        return userAgent;
    }

    public static Bitmap image(String str) throws MalformedURLException, IOException {
        InputStream inputStream = null;
        try {
            inputStream = new URL(str).openStream();
            Bitmap decodeStream = BitmapFactory.decodeStream(inputStream);
            if (inputStream != null) {
                try {
                    inputStream.close();
                } catch (IOException e) {
                }
            }
            return decodeStream;
        } finally {
            if (inputStream != null) {
                try {
                    inputStream.close();
                } catch (IOException e2) {
                }
            }
        }
    }

    public static String post(String str, List<NameValuePair> list, List<String> list2) {
        DefaultHttpClient defaultHttpClient = new DefaultHttpClient();
        defaultHttpClient.getParams().setBooleanParameter("http.protocol.handle-redirects", false);
        defaultHttpClient.getParams().setParameter("http.protocol.expect-continue", Boolean.valueOf(false));
        defaultHttpClient.getParams().setParameter("http.protocol.version", HttpVersion.HTTP_1_1);
        HttpParams params = defaultHttpClient.getParams();
        HttpConnectionParams.setConnectionTimeout(params, CONNECT_TIMEOUT);
        HttpConnectionParams.setSoTimeout(params, READ_TIMEOUT);
        HttpProtocolParams.setUserAgent(params, getUserAgent());
        try {
            HttpPost httpPost = new HttpPost(str);
            for (String addHeader : list2) {
                httpPost.addHeader("Cookie", addHeader);
            }
            httpPost.addHeader("apv", AppConsts.versionName);
            try {
                httpPost.setEntity(new UrlEncodedFormEntity(list, "UTF-8"));
                HttpResponse execute = defaultHttpClient.execute(httpPost);
                if (execute != null) {
                    HttpEntity entity = execute.getEntity();
                    Header firstHeader = execute.getFirstHeader("Location");
                    if (firstHeader != null) {
                        return firstHeader.getValue();
                    }
                    if (entity != null) {
                        return StringUtil.convertToString(entity.getContent());
                    }
                }
            } catch (Exception e) {
                httpPost.abort();
            }
            return null;
        } catch (NullPointerException e2) {
            throw new IllegalArgumentException(String.format("The following url is illegal %s", new Object[]{str}));
        }
    }

    public static String postMultipart(String str, List<NameValuePair> list, List<String> list2, File file, ProgressCallback progressCallback) {
        HttpMultipartRequest httpMultipartRequest = new HttpMultipartRequest(str, list, list2, file, getUserAgent(), progressCallback);
        httpMultipartRequest.setConnectionTimeout(CONNECT_TIMEOUT_FOR_UPLOAD);
        httpMultipartRequest.setReadingTimeout(READ_TIMEOUT_FOR_UPLOAD);
        return httpMultipartRequest.send();
    }

    public static String postMultipart(String str, List<NameValuePair> list, List<String> list2, String str2, ProgressCallback progressCallback) {
        HttpMultipartRequest httpMultipartRequest = new HttpMultipartRequest(str, list, list2, str2, getUserAgent(), progressCallback);
        httpMultipartRequest.setConnectionTimeout(CONNECT_TIMEOUT_FOR_UPLOAD);
        httpMultipartRequest.setReadingTimeout(READ_TIMEOUT_FOR_UPLOAD);
        return httpMultipartRequest.send();
    }

    public static InputStream postXML(String str, String str2) {
        return postXML(str, str2, null);
    }

    public static InputStream postXML(String str, String str2, List<String> list) {
        DefaultHttpClient defaultHttpClient = new DefaultHttpClient();
        defaultHttpClient.getParams().setBooleanParameter("http.protocol.handle-redirects", true);
        defaultHttpClient.getParams().setParameter("http.protocol.expect-continue", Boolean.valueOf(false));
        HttpParams params = defaultHttpClient.getParams();
        HttpConnectionParams.setConnectionTimeout(params, CONNECT_TIMEOUT);
        HttpConnectionParams.setSoTimeout(params, READ_TIMEOUT);
        HttpProtocolParams.setUserAgent(params, "Android");
        HttpPost httpPost = new HttpPost(str);
        if (list != null) {
            for (String addHeader : list) {
                httpPost.addHeader("Cookie", addHeader);
            }
        }
        try {
            httpPost.setEntity(new StringEntity(str2));
            httpPost.setHeader(HttpRequest.HEADER_CONTENT_TYPE, "application/xml; charset=UTF-8");
            HttpResponse execute = defaultHttpClient.execute(httpPost);
            if (execute != null) {
                HttpEntity entity = execute.getEntity();
                if (entity != null) {
                    return entity.getContent();
                }
            }
        } catch (Exception e) {
            httpPost.abort();
        }
        return null;
    }
}
