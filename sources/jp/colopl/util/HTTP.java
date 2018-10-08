package jp.colopl.util;

import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import io.fabric.sdk.android.services.network.HttpRequest;
import java.io.File;
import java.io.IOException;
import java.io.InputStream;
import java.net.HttpURLConnection;
import java.net.MalformedURLException;
import java.net.URL;
import java.net.URLConnection;
import java.util.ArrayList;
import java.util.List;
import jp.colopl.config.Config;
import jp.colopl.drapro.AppConsts;
import jp.colopl.util.HttpMultipartRequest.ProgressCallback;
import org.apache.http.Header;
import org.apache.http.HttpEntity;
import org.apache.http.HttpResponse;
import org.apache.http.HttpVersion;
import org.apache.http.NameValuePair;
import org.apache.http.client.HttpClient;
import org.apache.http.client.entity.UrlEncodedFormEntity;
import org.apache.http.client.methods.HttpPost;
import org.apache.http.entity.StringEntity;
import org.apache.http.impl.client.DefaultHttpClient;
import org.apache.http.params.HttpConnectionParams;
import org.apache.http.params.HttpParams;
import org.apache.http.params.HttpProtocolParams;

public class HTTP {
    private static int CONNECT_TIMEOUT = 10000;
    private static int CONNECT_TIMEOUT_FOR_UPLOAD = 300000;
    private static int READ_TIMEOUT = 10000;
    private static int READ_TIMEOUT_FOR_UPLOAD = 300000;
    private static String userAgent;

    public static List<String> createCookies(Config config) {
        List<String> arrayList = new ArrayList();
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

    public static String get(String str, List<String> list) {
        URLConnection uRLConnection;
        String str2;
        try {
            String convertToString;
            URLConnection openConnection = new URL(str).openConnection();
            try {
                openConnection.setRequestProperty("User-Agent", getUserAgent());
                openConnection.setRequestProperty("apv", AppConsts.versionName);
                ((HttpURLConnection) openConnection).setRequestMethod(HttpRequest.METHOD_GET);
                openConnection.setUseCaches(false);
                openConnection.setConnectTimeout(CONNECT_TIMEOUT);
                openConnection.setReadTimeout(READ_TIMEOUT);
                if (list != null) {
                    StringBuilder stringBuilder = new StringBuilder();
                    for (String append : list) {
                        stringBuilder.append(append);
                        stringBuilder.append(";");
                    }
                    openConnection.setRequestProperty("Cookie", stringBuilder.toString());
                }
                openConnection.connect();
                convertToString = StringUtil.convertToString(new DoneHandlerInputStream(openConnection.getInputStream()));
            } catch (Exception e) {
                uRLConnection = openConnection;
                str2 = null;
                if (uRLConnection != null) {
                    ((HttpURLConnection) uRLConnection).disconnect();
                }
                return str2;
            }
            try {
                ((HttpURLConnection) openConnection).disconnect();
                return convertToString;
            } catch (Exception e2) {
                uRLConnection = openConnection;
                str2 = convertToString;
                if (uRLConnection != null) {
                    ((HttpURLConnection) uRLConnection).disconnect();
                }
                return str2;
            }
        } catch (Exception e3) {
            uRLConnection = null;
            str2 = null;
            if (uRLConnection != null) {
                ((HttpURLConnection) uRLConnection).disconnect();
            }
            return str2;
        }
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
        } catch (Throwable th) {
            if (inputStream != null) {
                try {
                    inputStream.close();
                } catch (IOException e2) {
                }
            }
        }
    }

    public static String post(String str, List<NameValuePair> list, List<String> list2) {
        HttpClient defaultHttpClient = new DefaultHttpClient();
        defaultHttpClient.getParams().setBooleanParameter("http.protocol.handle-redirects", false);
        defaultHttpClient.getParams().setParameter("http.protocol.expect-continue", Boolean.valueOf(false));
        defaultHttpClient.getParams().setParameter("http.protocol.version", HttpVersion.HTTP_1_1);
        HttpParams params = defaultHttpClient.getParams();
        HttpConnectionParams.setConnectionTimeout(params, CONNECT_TIMEOUT);
        HttpConnectionParams.setSoTimeout(params, READ_TIMEOUT);
        HttpProtocolParams.setUserAgent(params, getUserAgent());
        try {
            Object httpPost = new HttpPost(str);
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
        HttpMultipartRequest httpMultipartRequest = new HttpMultipartRequest(str, (List) list, (List) list2, file, getUserAgent(), progressCallback);
        httpMultipartRequest.setConnectionTimeout(CONNECT_TIMEOUT_FOR_UPLOAD);
        httpMultipartRequest.setReadingTimeout(READ_TIMEOUT_FOR_UPLOAD);
        return httpMultipartRequest.send();
    }

    public static String postMultipart(String str, List<NameValuePair> list, List<String> list2, String str2, ProgressCallback progressCallback) {
        HttpMultipartRequest httpMultipartRequest = new HttpMultipartRequest(str, (List) list, (List) list2, str2, getUserAgent(), progressCallback);
        httpMultipartRequest.setConnectionTimeout(CONNECT_TIMEOUT_FOR_UPLOAD);
        httpMultipartRequest.setReadingTimeout(READ_TIMEOUT_FOR_UPLOAD);
        return httpMultipartRequest.send();
    }

    public static InputStream postXML(String str, String str2) {
        return postXML(str, str2, null);
    }

    public static InputStream postXML(String str, String str2, List<String> list) {
        HttpClient defaultHttpClient = new DefaultHttpClient();
        defaultHttpClient.getParams().setBooleanParameter("http.protocol.handle-redirects", true);
        defaultHttpClient.getParams().setParameter("http.protocol.expect-continue", Boolean.valueOf(false));
        HttpParams params = defaultHttpClient.getParams();
        HttpConnectionParams.setConnectionTimeout(params, CONNECT_TIMEOUT);
        HttpConnectionParams.setSoTimeout(params, READ_TIMEOUT);
        HttpProtocolParams.setUserAgent(params, "Android");
        Object httpPost = new HttpPost(str);
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
