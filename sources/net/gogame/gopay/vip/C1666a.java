package net.gogame.gopay.vip;

import android.net.Uri;
import android.util.Log;
import java.io.ByteArrayOutputStream;
import java.io.IOException;
import java.io.InputStream;
import java.net.HttpURLConnection;
import java.net.URL;
import java.net.URLEncoder;
import java.util.Locale;
import java.util.Map;
import java.util.Map.Entry;
import javax.crypto.Mac;
import javax.crypto.spec.SecretKeySpec;
import org.json.JSONException;
import org.json.JSONObject;
import p017io.fabric.sdk.android.services.network.HttpRequest;

/* renamed from: net.gogame.gopay.vip.a */
final class C1666a {
    /* renamed from: a */
    static JSONObject m987a(String str, Map<String, String> map, String str2, Map<String, String> map2) throws IOException, JSONException, UnauthorizedException, HttpException {
        return new JSONObject(m988b(str, map, str2, map2));
    }

    /* renamed from: b */
    private static String m988b(String str, Map<String, String> map, String str2, Map<String, String> map2) throws IOException, UnauthorizedException, HttpException {
        if (str == null) {
            return null;
        }
        return m983a(str + "?" + m985a(map), m984a(HttpRequest.METHOD_GET, str, map, null, str2), map2);
    }

    /* renamed from: a */
    private static String m983a(String str, String str2, Map<String, String> map) throws IOException, UnauthorizedException, HttpException {
        HttpURLConnection httpURLConnection = (HttpURLConnection) new URL(str).openConnection();
        if (map != null) {
            for (Entry entry : map.entrySet()) {
                if (!(entry.getKey() == null || entry.getValue() == null)) {
                    httpURLConnection.addRequestProperty((String) entry.getKey(), (String) entry.getValue());
                }
            }
        }
        httpURLConnection.addRequestProperty("Go-Token", str2);
        try {
            httpURLConnection.connect();
            String a = m981a(httpURLConnection.getInputStream());
            int responseCode = httpURLConnection.getResponseCode();
            if (responseCode == 401 || responseCode == 403 || responseCode == 406) {
                throw new UnauthorizedException(String.format(Locale.US, "HTTP response %d %s", new Object[]{Integer.valueOf(httpURLConnection.getResponseCode()), httpURLConnection.getResponseMessage()}), httpURLConnection.getResponseCode(), httpURLConnection.getResponseMessage());
            } else if (responseCode >= 200 && responseCode < 300) {
                return a;
            } else {
                throw new HttpException(String.format(Locale.US, "HTTP response %d %s", new Object[]{Integer.valueOf(httpURLConnection.getResponseCode()), httpURLConnection.getResponseMessage()}), httpURLConnection.getResponseCode(), httpURLConnection.getResponseMessage());
            }
        } finally {
            httpURLConnection.disconnect();
        }
    }

    /* renamed from: a */
    private static String m984a(String str, String str2, Map<String, String> map, Map<String, String> map2, String str3) {
        Uri parse = Uri.parse(str2);
        String scheme = parse.getScheme();
        String host = parse.getHost();
        String path = parse.getPath();
        StringBuilder sb = new StringBuilder();
        sb.append(str);
        sb.append(scheme);
        sb.append(host);
        sb.append(path);
        if (map != null) {
            sb.append(m985a(map));
        }
        if (map2 != null) {
            sb.append(m985a(map2));
        }
        return m982a(sb.toString(), str3, "HmacSHA256");
    }

    /* renamed from: a */
    private static String m985a(Map<String, String> map) {
        StringBuilder sb = new StringBuilder();
        if (map != null) {
            for (Entry entry : map.entrySet()) {
                if (!(entry.getKey() == null || entry.getValue() == null)) {
                    if (sb.length() > 0) {
                        sb.append("&");
                    }
                    try {
                        sb.append(URLEncoder.encode((String) entry.getKey(), "UTF-8"));
                        sb.append("=");
                        sb.append(URLEncoder.encode((String) entry.getValue(), "UTF-8"));
                    } catch (Exception e) {
                        Log.e("goPay", "Exception", e);
                        return "";
                    }
                }
            }
        }
        return sb.toString();
    }

    /* renamed from: a */
    private static String m982a(String str, String str2, String str3) {
        try {
            SecretKeySpec secretKeySpec = new SecretKeySpec(str2.getBytes(), str3);
            Mac instance = Mac.getInstance(str3);
            instance.init(secretKeySpec);
            return m986a(instance.doFinal(str.getBytes()));
        } catch (Exception e) {
            Log.e("goPay", "Exception", e);
            return "";
        }
    }

    /* renamed from: a */
    private static String m986a(byte[] bArr) {
        StringBuilder sb = new StringBuilder(bArr.length * 2);
        for (byte b : bArr) {
            sb.append(String.format("%02x", new Object[]{Integer.valueOf(b & 255)}));
        }
        return sb.toString();
    }

    /* renamed from: a */
    private static String m981a(InputStream inputStream) throws IOException {
        ByteArrayOutputStream byteArrayOutputStream = new ByteArrayOutputStream();
        byte[] bArr = new byte[4096];
        while (true) {
            int read = inputStream.read(bArr);
            if (read <= 0) {
                return byteArrayOutputStream.toString("UTF-8");
            }
            byteArrayOutputStream.write(bArr, 0, read);
        }
    }
}
