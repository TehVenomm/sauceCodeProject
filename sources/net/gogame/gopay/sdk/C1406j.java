package net.gogame.gopay.sdk;

import com.appsflyer.AppsFlyerProperties;
import com.facebook.appevents.AppEventsConstants;
import com.facebook.appevents.UserDataStore;
import com.facebook.internal.ServerProtocol;
import com.facebook.share.internal.MessengerShareContentUtility;
import com.google.firebase.analytics.FirebaseAnalytics.Param;
import java.io.IOException;
import java.io.InputStream;
import java.net.ConnectException;
import java.net.HttpURLConnection;
import java.net.SocketTimeoutException;
import java.net.URL;
import java.net.URLEncoder;
import java.util.ArrayList;
import java.util.Collections;
import java.util.HashMap;
import java.util.Iterator;
import java.util.List;
import java.util.Locale;
import java.util.Map;
import java.util.Map.Entry;
import java.util.TimeZone;
import javax.crypto.Mac;
import javax.crypto.spec.SecretKeySpec;
import net.gogame.gopay.sdk.iab.C1622bu;
import net.gogame.gopay.sdk.support.IOUtils;
import org.jetbrains.annotations.NotNull;
import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;
import org.onepf.oms.appstore.googleUtils.IabException;
import org.onepf.oms.appstore.googleUtils.SkuDetails;
import p017io.fabric.sdk.android.services.network.HttpRequest;

/* renamed from: net.gogame.gopay.sdk.j */
public final class C1406j {

    /* renamed from: a */
    private static String f1115a = null;

    /* renamed from: b */
    private static String f1116b;

    /* renamed from: c */
    private static String f1117c;

    /* renamed from: d */
    private static String f1118d;

    /* renamed from: e */
    private static String f1119e;

    /* renamed from: f */
    private static String f1120f = "";

    /* renamed from: g */
    private static String f1121g = "";

    /* renamed from: h */
    private static String f1122h = "";

    /* renamed from: a */
    public static String m858a() {
        return f1115a;
    }

    /* renamed from: a */
    private static String m859a(String str, Map map) {
        if (map == null) {
            map = new HashMap();
        }
        map.put("sdk", "goPaySDK-android");
        map.put("sdk_version", "1.1.7");
        map.put("lang", Locale.getDefault().getISO3Language());
        map.put("tz", TimeZone.getDefault().getID());
        map.put("game_version", f1122h);
        if (f1120f != null) {
            map.put("device_id", f1120f);
        }
        if (f1121g != null) {
            map.put("bundle_id", f1121g);
        }
        if (f1119e != null && f1119e.length() > 0) {
            map.put("glang", f1119e);
        }
        String str2 = "";
        if (str.indexOf("http") >= 0) {
            str2 = "http";
        }
        if (str.indexOf("https") >= 0) {
            str2 = "https";
        }
        String str3 = HttpRequest.METHOD_GET + str2 + str.replace("http://", "").replace("https://", "");
        if (map != null) {
            str3 = str3 + m860a(map);
        }
        f1116b = m874c(str3, f1117c, "HmacSHA256");
        return str + "?" + m860a(map);
    }

    /* renamed from: a */
    private static String m860a(Map map) {
        String str = "";
        for (Entry entry : map.entrySet()) {
            if (!str.isEmpty()) {
                str = str + "&";
            }
            try {
                str = str + URLEncoder.encode((String) entry.getKey(), "UTF-8") + "=" + URLEncoder.encode((String) entry.getValue(), "UTF-8");
            } catch (Exception e) {
            }
        }
        return str;
    }

    /* renamed from: a */
    private static String m861a(byte[] bArr) {
        StringBuilder sb = new StringBuilder(bArr.length * 2);
        for (byte b : bArr) {
            sb.append(String.format("%02x", new Object[]{Integer.valueOf(b & 255)}));
        }
        return sb.toString();
    }

    /* renamed from: a */
    private static List m862a(JSONArray jSONArray) {
        int length = jSONArray.length();
        if (length <= 0) {
            return null;
        }
        ArrayList arrayList = new ArrayList();
        for (int i = 0; i < length; i++) {
            JSONObject jSONObject = jSONArray.getJSONObject(i);
            arrayList.add(new Country(jSONObject.getString("code"), jSONObject.getString("name"), jSONObject.getString("displayIcon")));
        }
        return Collections.unmodifiableList(arrayList);
    }

    /* renamed from: a */
    private static C1361f m863a(@NotNull JSONObject jSONObject) {
        return new C1361f(jSONObject.getInt("statusCode"), jSONObject.getBoolean("status"), jSONObject.getString("statusMsg"));
    }

    /* renamed from: a */
    public static C1362g m864a(@NotNull String str, String str2, @NotNull String str3, String str4, boolean z) {
        try {
            HashMap hashMap = new HashMap();
            hashMap.put("appId", str);
            if (str2 != null) {
                hashMap.put("guid", str2);
            }
            hashMap.put("product_id", str3);
            if (str4 != null) {
                hashMap.put(UserDataStore.COUNTRY, str4);
            }
            if (z) {
                hashMap.put("requestCountries", AppEventsConstants.EVENT_PARAM_VALUE_YES);
            }
            return m881f(m888j(m859a("https://gp-api.gogame.net/billing/v2/get_product_details/", (Map) hashMap)));
        } catch (IOException e) {
            throw new IabException(-1008, e.getLocalizedMessage(), e);
        } catch (JSONException e2) {
            throw new IabException(-1002, "Bad response", e2);
        }
    }

    /* renamed from: a */
    public static C1363h m865a(@NotNull String str, String str2, String str3) {
        try {
            HashMap hashMap = new HashMap();
            hashMap.put("appId", str);
            if (str2 != null) {
                hashMap.put("guid", str2);
            }
            if (str3 != null) {
                hashMap.put(UserDataStore.COUNTRY, str3);
            }
            hashMap.put("requestCountries", AppEventsConstants.EVENT_PARAM_VALUE_YES);
            return m879e(m888j(m859a("https://gp-api.gogame.net/billing/v2/get_sku_details/", (Map) hashMap)));
        } catch (IOException e) {
            throw new IabException(-1008, e.getLocalizedMessage(), e);
        } catch (JSONException e2) {
            throw new IabException(-1002, "Bad response", e2);
        }
    }

    /* renamed from: a */
    public static C1408n m866a(@NotNull String str, @NotNull String str2, @NotNull String str3, @NotNull String str4, String str5, String str6) {
        try {
            HashMap hashMap = new HashMap();
            hashMap.put("appId", str);
            hashMap.put("guid", str2);
            hashMap.put("product_id", str3);
            hashMap.put("payment_method", str4);
            if (str5 != null) {
                hashMap.put(UserDataStore.COUNTRY, str5);
            }
            if (str6 != null) {
                hashMap.put(MessengerShareContentUtility.ATTACHMENT_PAYLOAD, str6);
            }
            return m883g(m888j(m859a("https://gp-api.gogame.net/billing/v2/start_purchase/", (Map) hashMap)));
        } catch (IOException e) {
            throw new IabException(-1008, e.getLocalizedMessage(), e);
        } catch (JSONException e2) {
            throw new IabException(-1002, "Bad response", e2);
        } catch (Exception e3) {
            throw new IabException(-1002, "Bad response", e3);
        }
    }

    /* renamed from: a */
    public static C1408n m867a(@NotNull String str, @NotNull String str2, @NotNull SkuDetails skuDetails, @NotNull String str3, @NotNull String str4, @NotNull String str5, String str6, String str7) {
        try {
            HashMap hashMap = new HashMap();
            hashMap.put("appId", str);
            hashMap.put("guid", str2);
            hashMap.put("product_id", skuDetails.getSku());
            hashMap.put("payment_method", str5);
            if (str6 != null) {
                hashMap.put(UserDataStore.COUNTRY, str6);
            }
            if (str7 != null) {
                hashMap.put(MessengerShareContentUtility.ATTACHMENT_PAYLOAD, str7);
            }
            if (skuDetails != null) {
                if (skuDetails.getTitle() != null) {
                    hashMap.put("title", skuDetails.getTitle());
                }
                if (skuDetails.getDescription() != null) {
                    hashMap.put("description", skuDetails.getDescription());
                }
                if (skuDetails.getPrice() != null) {
                    hashMap.put(Param.PRICE, skuDetails.getPrice());
                }
            }
            if (str3 != null) {
                hashMap.put("price_amount_micros", str3);
            }
            if (str4 != null) {
                hashMap.put("price_currency_code", str4);
            }
            return m883g(m888j(m859a("https://gp-api.gogame.net/billing/v2/start_purchase/", (Map) hashMap)));
        } catch (IOException e) {
            throw new IabException(-1008, e.getLocalizedMessage(), e);
        } catch (JSONException e2) {
            throw new IabException(-1002, "Bad response", e2);
        } catch (Exception e3) {
            throw new IabException(-1002, "Bad response", e3);
        }
    }

    /* renamed from: a */
    public static void m868a(String str) {
        f1115a = str;
    }

    /* renamed from: a */
    public static void m869a(@NotNull String str, @NotNull String str2) {
        try {
            HashMap hashMap = new HashMap();
            hashMap.put("appId", str);
            hashMap.put("guid", str2);
            if (!(f1118d == null || f1117c == null)) {
                try {
                    hashMap.put("uid", IOUtils.encrypt(f1117c, f1118d));
                } catch (Exception e) {
                    e.printStackTrace();
                }
            }
            m888j(m859a("https://gp-api.gogame.net/billing/v2/status/", (Map) hashMap));
        } catch (Exception e2) {
            e2.printStackTrace();
        }
    }

    /* renamed from: b */
    public static GetCountryDetailsResponse m870b(@NotNull String str, String str2, String str3) {
        try {
            HashMap hashMap = new HashMap();
            hashMap.put("appId", str);
            if (str2 != null) {
                hashMap.put("guid", str2);
            }
            if (str3 != null) {
                hashMap.put("product_id", str3);
            }
            JSONObject j = m888j(m859a("https://gp-api.gogame.net/billing/v2/get_country_details/", (Map) hashMap));
            return new GetCountryDetailsResponse(j.getString(UserDataStore.COUNTRY), m862a(j.getJSONArray("countries")), m886h(j.getJSONObject("baseUrls")));
        } catch (IOException e) {
            throw new IabException(-1008, e.getLocalizedMessage(), e);
        } catch (JSONException e2) {
            throw new IabException(-1002, "Bad response", e2);
        }
    }

    /* renamed from: b */
    public static C1622bu m871b() {
        try {
            JSONObject j = m888j(m859a("https://gp-api.gogame.net/billing/v2/get_assets_path/", (Map) null));
            return new C1622bu((float) j.getDouble(ServerProtocol.FALLBACK_DIALOG_PARAM_VERSION), j.getString("file"), j.getBoolean("skip"));
        } catch (Exception e) {
            e.printStackTrace();
            return null;
        }
    }

    @NotNull
    /* renamed from: b */
    private static SkuDetails m872b(@NotNull JSONObject jSONObject) {
        String string = jSONObject.getString("product_id");
        String string2 = jSONObject.getString(Param.PRICE);
        return new SkuDetails(jSONObject.getString("type"), string, jSONObject.getString("title"), string2, jSONObject.getString("description"));
    }

    /* renamed from: b */
    public static void m873b(String str) {
        f1117c = str;
    }

    /* renamed from: c */
    private static String m874c(String str, String str2, String str3) {
        String str4 = "";
        try {
            SecretKeySpec secretKeySpec = new SecretKeySpec(str2.getBytes(), str3);
            Mac instance = Mac.getInstance(str3);
            instance.init(secretKeySpec);
            return m861a(instance.doFinal(str.getBytes()));
        } catch (Exception e) {
            return str4;
        }
    }

    @NotNull
    /* renamed from: c */
    private static C1636k m875c(@NotNull JSONObject jSONObject) {
        String string = jSONObject.getString("id");
        String string2 = jSONObject.getString("name");
        String string3 = jSONObject.getString("displayName");
        String string4 = jSONObject.getString("displayIcon");
        ArrayList arrayList = new ArrayList();
        JSONArray jSONArray = jSONObject.getJSONArray("paymentMethods");
        int length = jSONArray.length();
        for (int i = 0; i < length; i++) {
            JSONObject jSONObject2 = jSONArray.getJSONObject(i);
            arrayList.add(new C1637l(jSONObject2.getString("id"), jSONObject2.getString("name"), jSONObject2.getString("displayName"), jSONObject2.getString("displayIcon")));
        }
        return new C1636k(string, string2, string3, string4, arrayList);
    }

    /* renamed from: c */
    public static void m876c(String str) {
        f1118d = str;
    }

    @NotNull
    /* renamed from: d */
    private static C1602c m877d(JSONObject jSONObject) {
        boolean z = false;
        if (jSONObject.has("disableCountrySelection")) {
            z = jSONObject.getBoolean("disableCountrySelection");
        }
        return new C1602c(z);
    }

    /* renamed from: d */
    public static void m878d(String str) {
        f1120f = str;
    }

    @NotNull
    /* renamed from: e */
    private static C1363h m879e(@NotNull JSONObject jSONObject) {
        String str = null;
        String string = jSONObject.getString(UserDataStore.COUNTRY);
        ArrayList arrayList = new ArrayList();
        JSONArray jSONArray = jSONObject.getJSONArray("items");
        for (int i = 0; i < jSONArray.length(); i++) {
            arrayList.add(m872b(jSONArray.getJSONObject(i)));
        }
        List list = jSONObject.has("countries") ? m862a(jSONObject.getJSONArray("countries")) : null;
        Map map = jSONObject.has("baseUrls") ? m886h(jSONObject.getJSONObject("baseUrls")) : null;
        C1602c cVar = jSONObject.has("config") ? m877d(jSONObject.getJSONObject("config")) : null;
        if (jSONObject.has("redirectUrl")) {
            str = jSONObject.getString("redirectUrl");
        }
        return new C1363h(string, arrayList, list, map, cVar, str);
    }

    /* renamed from: e */
    public static void m880e(String str) {
        f1121g = str;
    }

    @NotNull
    /* renamed from: f */
    private static C1362g m881f(@NotNull JSONObject jSONObject) {
        if (!jSONObject.getBoolean("status")) {
            return new C1362g(m863a(jSONObject));
        }
        String string = jSONObject.getString(UserDataStore.COUNTRY);
        String string2 = jSONObject.getString(AppsFlyerProperties.CURRENCY_CODE);
        SkuDetails b = m872b(jSONObject.getJSONObject("item"));
        ArrayList arrayList = new ArrayList();
        JSONArray jSONArray = jSONObject.getJSONArray("paymentTypes");
        int length = jSONArray.length();
        for (int i = 0; i < length; i++) {
            arrayList.add(m875c(jSONArray.getJSONObject(i)));
        }
        return new C1362g(string, string2, b, arrayList, m886h(jSONObject.getJSONObject("baseUrls")), jSONObject.has("countries") ? m862a(jSONObject.getJSONArray("countries")) : null, jSONObject.has("config") ? m877d(jSONObject.getJSONObject("config")) : null, jSONObject.has("redirectUrl") ? jSONObject.getString("redirectUrl") : null);
    }

    /* renamed from: f */
    public static void m882f(String str) {
        f1122h = str;
    }

    @NotNull
    /* renamed from: g */
    private static C1408n m883g(@NotNull JSONObject jSONObject) {
        return !jSONObject.getBoolean("status") ? new C1408n(m863a(jSONObject)) : new C1408n(jSONObject.getString("redirectUrl"), jSONObject);
    }

    /* renamed from: g */
    public static void m884g(String str) {
        f1119e = str;
    }

    /* renamed from: h */
    private static HttpURLConnection m885h(String str) {
        try {
            HttpURLConnection httpURLConnection = (HttpURLConnection) new URL(str).openConnection();
            httpURLConnection.addRequestProperty("Go-Token", f1116b);
            httpURLConnection.setRequestProperty("http.keepAlive", "false");
            httpURLConnection.setRequestMethod(HttpRequest.METHOD_GET);
            httpURLConnection.setConnectTimeout(5000);
            httpURLConnection.setReadTimeout(5000);
            httpURLConnection.setDoInput(true);
            httpURLConnection.connect();
            return httpURLConnection;
        } catch (SocketTimeoutException e) {
            throw new IabException(-1001, "Timeout", e);
        } catch (ConnectException e2) {
            throw new IabException(-1001, "Connection failed", e2);
        } catch (IOException e3) {
            throw new IabException(-1001, "I/O exception", e3);
        } catch (Exception e4) {
            throw new IabException(-1008, e4.getLocalizedMessage(), e4);
        }
    }

    @NotNull
    /* renamed from: h */
    private static Map m886h(@NotNull JSONObject jSONObject) {
        HashMap hashMap = new HashMap();
        Iterator keys = jSONObject.keys();
        while (keys.hasNext()) {
            String str = (String) keys.next();
            hashMap.put(str, jSONObject.getString(str));
        }
        return hashMap;
    }

    /* renamed from: i */
    private static String m887i(String str) {
        InputStream inputStream;
        try {
            HttpURLConnection h = m885h(str);
            new StringBuilder("<< ").append(h.getResponseCode()).append(" ").append(h.getResponseMessage());
            if (h.getResponseCode() != 200) {
                throw new IabException(-1001, h.getResponseCode() + " " + h.getResponseMessage());
            }
            inputStream = null;
            inputStream = h.getInputStream();
            String readString = IOUtils.readString(inputStream, "UTF-8");
            if (inputStream != null) {
                inputStream.close();
            }
            return readString;
        } catch (SocketTimeoutException e) {
            throw new IabException(-1001, "Timeout", e);
        } catch (ConnectException e2) {
            throw new IabException(-1001, "Connection failed", e2);
        } catch (IOException e3) {
            throw new IabException(-1001, "I/O exception", e3);
        } catch (Exception e4) {
            throw new IabException(-1008, e4.getLocalizedMessage(), e4);
        } catch (Throwable th) {
            if (inputStream != null) {
                inputStream.close();
            }
            throw th;
        }
    }

    /* renamed from: j */
    private static JSONObject m888j(String str) {
        try {
            return new JSONObject(m887i(str));
        } catch (JSONException e) {
            throw new IabException(-1002, "Bad response", e);
        }
    }
}
