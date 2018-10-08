package net.gogame.gopay.vip;

import android.net.Uri;
import android.util.Log;
import com.google.android.gms.nearby.messages.Strategy;
import io.fabric.sdk.android.services.network.HttpRequest;
import java.io.ByteArrayOutputStream;
import java.io.IOException;
import java.io.InputStream;
import java.net.HttpURLConnection;
import java.net.URL;
import java.net.URLEncoder;
import java.security.Key;
import java.util.Locale;
import java.util.Map;
import java.util.Map.Entry;
import javax.crypto.Mac;
import javax.crypto.spec.SecretKeySpec;
import org.json.JSONException;
import org.json.JSONObject;

/* renamed from: net.gogame.gopay.vip.a */
final class C1414a {
    /* renamed from: a */
    static JSONObject m3998a(String str, Map<String, String> map, String str2, Map<String, String> map2) throws IOException, JSONException, UnauthorizedException, HttpException {
        return new JSONObject(C1414a.m3999b(str, map, str2, map2));
    }

    /* renamed from: b */
    private static String m3999b(String str, Map<String, String> map, String str2, Map<String, String> map2) throws IOException, UnauthorizedException, HttpException {
        if (str == null) {
            return null;
        }
        return C1414a.m3994a(str + "?" + C1414a.m3996a((Map) map), C1414a.m3995a(HttpRequest.METHOD_GET, str, map, null, str2), (Map) map2);
    }

    /* renamed from: a */
    private static String m3994a(String str, String str2, Map<String, String> map) throws IOException, UnauthorizedException, HttpException {
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
            String a = C1414a.m3992a(httpURLConnection.getInputStream());
            int responseCode = httpURLConnection.getResponseCode();
            if (responseCode == 401 || responseCode == 403 || responseCode == 406) {
                throw new UnauthorizedException(String.format(Locale.US, "HTTP response %d %s", new Object[]{Integer.valueOf(httpURLConnection.getResponseCode()), httpURLConnection.getResponseMessage()}), httpURLConnection.getResponseCode(), httpURLConnection.getResponseMessage());
            } else if (responseCode >= 200 && responseCode < Strategy.TTL_SECONDS_DEFAULT) {
                return a;
            } else {
                throw new HttpException(String.format(Locale.US, "HTTP response %d %s", new Object[]{Integer.valueOf(httpURLConnection.getResponseCode()), httpURLConnection.getResponseMessage()}), httpURLConnection.getResponseCode(), httpURLConnection.getResponseMessage());
            }
        } finally {
            httpURLConnection.disconnect();
        }
    }

    /* renamed from: a */
    private static String m3995a(String str, String str2, Map<String, String> map, Map<String, String> map2, String str3) {
        Uri parse = Uri.parse(str2);
        String scheme = parse.getScheme();
        String host = parse.getHost();
        String path = parse.getPath();
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.append(str);
        stringBuilder.append(scheme);
        stringBuilder.append(host);
        stringBuilder.append(path);
        if (map != null) {
            stringBuilder.append(C1414a.m3996a((Map) map));
        }
        if (map2 != null) {
            stringBuilder.append(C1414a.m3996a((Map) map2));
        }
        return C1414a.m3993a(stringBuilder.toString(), str3, "HmacSHA256");
    }

    /* renamed from: a */
    private static String m3996a(Map<String, String> map) {
        StringBuilder stringBuilder = new StringBuilder();
        if (map != null) {
            for (Entry entry : map.entrySet()) {
                if (!(entry.getKey() == null || entry.getValue() == null)) {
                    if (stringBuilder.length() > 0) {
                        stringBuilder.append("&");
                    }
                    try {
                        stringBuilder.append(URLEncoder.encode((String) entry.getKey(), "UTF-8"));
                        stringBuilder.append("=");
                        stringBuilder.append(URLEncoder.encode((String) entry.getValue(), "UTF-8"));
                    } catch (Throwable e) {
                        Log.e("goPay", "Exception", e);
                        return "";
                    }
                }
            }
        }
        return stringBuilder.toString();
    }

    /* renamed from: a */
    private static String m3993a(String str, String str2, String str3) {
        try {
            Key secretKeySpec = new SecretKeySpec(str2.getBytes(), str3);
            Mac instance = Mac.getInstance(str3);
            instance.init(secretKeySpec);
            return C1414a.m3997a(instance.doFinal(str.getBytes()));
        } catch (Throwable e) {
            Log.e("goPay", "Exception", e);
            return "";
        }
    }

    /* renamed from: a */
    private static String m3997a(byte[] bArr) {
        StringBuilder stringBuilder = new StringBuilder(bArr.length * 2);
        int length = bArr.length;
        for (int i = 0; i < length; i++) {
            stringBuilder.append(String.format("%02x", new Object[]{Integer.valueOf(bArr[i] & 255)}));
        }
        return stringBuilder.toString();
    }

    /* renamed from: a */
    private static String m3992a(InputStream inputStream) throws IOException {
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
