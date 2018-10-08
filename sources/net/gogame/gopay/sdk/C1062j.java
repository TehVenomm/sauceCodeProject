package net.gogame.gopay.sdk;

import com.appsflyer.AppsFlyerProperties;
import com.facebook.appevents.AppEventsConstants;
import com.facebook.internal.ServerProtocol;
import com.facebook.share.internal.ShareConstants;
import com.google.firebase.analytics.FirebaseAnalytics.Param;
import io.fabric.sdk.android.services.network.HttpRequest;
import java.io.InputStream;
import java.net.HttpURLConnection;
import java.net.URL;
import java.net.URLEncoder;
import java.security.Key;
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
import net.gogame.gopay.sdk.iab.bu;
import net.gogame.gopay.sdk.support.IOUtils;
import org.jetbrains.annotations.NotNull;
import org.json.JSONArray;
import org.json.JSONObject;
import org.onepf.oms.appstore.googleUtils.IabException;
import org.onepf.oms.appstore.googleUtils.SkuDetails;

/* renamed from: net.gogame.gopay.sdk.j */
public final class C1062j {
    /* renamed from: a */
    private static String f1171a = null;
    /* renamed from: b */
    private static String f1172b;
    /* renamed from: c */
    private static String f1173c;
    /* renamed from: d */
    private static String f1174d;
    /* renamed from: e */
    private static String f1175e;
    /* renamed from: f */
    private static String f1176f = "";
    /* renamed from: g */
    private static String f1177g = "";
    /* renamed from: h */
    private static String f1178h = "";

    /* renamed from: a */
    public static String m858a() {
        return f1171a;
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
        map.put("game_version", f1178h);
        if (f1176f != null) {
            map.put("device_id", f1176f);
        }
        if (f1177g != null) {
            map.put("bundle_id", f1177g);
        }
        if (f1175e != null && f1175e.length() > 0) {
            map.put("glang", f1175e);
        }
        String str2 = "";
        if (str.indexOf("http") >= 0) {
            str2 = "http";
        }
        if (str.indexOf("https") >= 0) {
            str2 = "https";
        }
        str2 = HttpRequest.METHOD_GET + str2 + str.replace("http://", "").replace("https://", "");
        if (map != null) {
            str2 = str2 + C1062j.m860a(map);
        }
        f1172b = C1062j.m874c(str2, f1173c, "HmacSHA256");
        return str + "?" + C1062j.m860a(map);
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
        StringBuilder stringBuilder = new StringBuilder(bArr.length * 2);
        int length = bArr.length;
        for (int i = 0; i < length; i++) {
            stringBuilder.append(String.format("%02x", new Object[]{Integer.valueOf(bArr[i] & 255)}));
        }
        return stringBuilder.toString();
    }

    /* renamed from: a */
    private static List m862a(JSONArray jSONArray) {
        int length = jSONArray.length();
        if (length <= 0) {
            return null;
        }
        List arrayList = new ArrayList();
        for (int i = 0; i < length; i++) {
            JSONObject jSONObject = jSONArray.getJSONObject(i);
            arrayList.add(new Country(jSONObject.getString("code"), jSONObject.getString("name"), jSONObject.getString("displayIcon")));
        }
        return Collections.unmodifiableList(arrayList);
    }

    /* renamed from: a */
    private static C1032f m863a(@NotNull JSONObject jSONObject) {
        return new C1032f(jSONObject.getInt("statusCode"), jSONObject.getBoolean("status"), jSONObject.getString("statusMsg"));
    }

    /* renamed from: a */
    public static C1033g m864a(@NotNull String str, String str2, @NotNull String str3, String str4, boolean z) {
        try {
            Map hashMap = new HashMap();
            hashMap.put("appId", str);
            if (str2 != null) {
                hashMap.put("guid", str2);
            }
            hashMap.put("product_id", str3);
            if (str4 != null) {
                hashMap.put("country", str4);
            }
            if (z) {
                hashMap.put("requestCountries", AppEventsConstants.EVENT_PARAM_VALUE_YES);
            }
            return C1062j.m881f(C1062j.m888j(C1062j.m859a("https://gp-api.gogame.net/billing/v2/get_product_details/", hashMap)));
        } catch (Exception e) {
            throw new IabException(-1008, e.getLocalizedMessage(), e);
        } catch (Exception e2) {
            throw new IabException(-1002, "Bad response", e2);
        }
    }

    /* renamed from: a */
    public static C1034h m865a(@NotNull String str, String str2, String str3) {
        try {
            Map hashMap = new HashMap();
            hashMap.put("appId", str);
            if (str2 != null) {
                hashMap.put("guid", str2);
            }
            if (str3 != null) {
                hashMap.put("country", str3);
            }
            hashMap.put("requestCountries", AppEventsConstants.EVENT_PARAM_VALUE_YES);
            return C1062j.m879e(C1062j.m888j(C1062j.m859a("https://gp-api.gogame.net/billing/v2/get_sku_details/", hashMap)));
        } catch (Exception e) {
            throw new IabException(-1008, e.getLocalizedMessage(), e);
        } catch (Exception e2) {
            throw new IabException(-1002, "Bad response", e2);
        }
    }

    /* renamed from: a */
    public static C1066n m866a(@NotNull String str, @NotNull String str2, @NotNull String str3, @NotNull String str4, String str5, String str6) {
        try {
            Map hashMap = new HashMap();
            hashMap.put("appId", str);
            hashMap.put("guid", str2);
            hashMap.put("product_id", str3);
            hashMap.put("payment_method", str4);
            if (str5 != null) {
                hashMap.put("country", str5);
            }
            if (str6 != null) {
                hashMap.put("payload", str6);
            }
            return C1062j.m883g(C1062j.m888j(C1062j.m859a("https://gp-api.gogame.net/billing/v2/start_purchase/", hashMap)));
        } catch (Exception e) {
            throw new IabException(-1008, e.getLocalizedMessage(), e);
        } catch (Exception e2) {
            throw new IabException(-1002, "Bad response", e2);
        } catch (Exception e22) {
            throw new IabException(-1002, "Bad response", e22);
        }
    }

    /* renamed from: a */
    public static C1066n m867a(@NotNull String str, @NotNull String str2, @NotNull SkuDetails skuDetails, @NotNull String str3, @NotNull String str4, @NotNull String str5, String str6, String str7) {
        try {
            Map hashMap = new HashMap();
            hashMap.put("appId", str);
            hashMap.put("guid", str2);
            hashMap.put("product_id", skuDetails.getSku());
            hashMap.put("payment_method", str5);
            if (str6 != null) {
                hashMap.put("country", str6);
            }
            if (str7 != null) {
                hashMap.put("payload", str7);
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
            return C1062j.m883g(C1062j.m888j(C1062j.m859a("https://gp-api.gogame.net/billing/v2/start_purchase/", hashMap)));
        } catch (Exception e) {
            throw new IabException(-1008, e.getLocalizedMessage(), e);
        } catch (Exception e2) {
            throw new IabException(-1002, "Bad response", e2);
        } catch (Exception e22) {
            throw new IabException(-1002, "Bad response", e22);
        }
    }

    /* renamed from: a */
    public static void m868a(String str) {
        f1171a = str;
    }

    /* renamed from: a */
    public static void m869a(@NotNull String str, @NotNull String str2) {
        try {
            Map hashMap = new HashMap();
            hashMap.put("appId", str);
            hashMap.put("guid", str2);
            if (!(f1174d == null || f1173c == null)) {
                try {
                    hashMap.put("uid", IOUtils.encrypt(f1173c, f1174d));
                } catch (Exception e) {
                    e.printStackTrace();
                }
            }
            C1062j.m888j(C1062j.m859a("https://gp-api.gogame.net/billing/v2/status/", hashMap));
        } catch (Exception e2) {
            e2.printStackTrace();
        }
    }

    /* renamed from: b */
    public static GetCountryDetailsResponse m870b(@NotNull String str, String str2, String str3) {
        try {
            Map hashMap = new HashMap();
            hashMap.put("appId", str);
            if (str2 != null) {
                hashMap.put("guid", str2);
            }
            if (str3 != null) {
                hashMap.put("product_id", str3);
            }
            JSONObject j = C1062j.m888j(C1062j.m859a("https://gp-api.gogame.net/billing/v2/get_country_details/", hashMap));
            return new GetCountryDetailsResponse(j.getString("country"), C1062j.m862a(j.getJSONArray("countries")), C1062j.m886h(j.getJSONObject("baseUrls")));
        } catch (Exception e) {
            throw new IabException(-1008, e.getLocalizedMessage(), e);
        } catch (Exception e2) {
            throw new IabException(-1002, "Bad response", e2);
        }
    }

    /* renamed from: b */
    public static bu m871b() {
        try {
            JSONObject j = C1062j.m888j(C1062j.m859a("https://gp-api.gogame.net/billing/v2/get_assets_path/", null));
            return new bu((float) j.getDouble(ServerProtocol.FALLBACK_DIALOG_PARAM_VERSION), j.getString("file"), j.getBoolean("skip"));
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
        return new SkuDetails(jSONObject.getString(ShareConstants.MEDIA_TYPE), string, jSONObject.getString("title"), string2, jSONObject.getString("description"));
    }

    /* renamed from: b */
    public static void m873b(String str) {
        f1173c = str;
    }

    /* renamed from: c */
    private static String m874c(String str, String str2, String str3) {
        String str4 = "";
        try {
            Key secretKeySpec = new SecretKeySpec(str2.getBytes(), str3);
            Mac instance = Mac.getInstance(str3);
            instance.init(secretKeySpec);
            str4 = C1062j.m861a(instance.doFinal(str.getBytes()));
        } catch (Exception e) {
        }
        return str4;
    }

    @NotNull
    /* renamed from: c */
    private static C1063k m875c(@NotNull JSONObject jSONObject) {
        String string = jSONObject.getString("id");
        String string2 = jSONObject.getString("name");
        String string3 = jSONObject.getString("displayName");
        String string4 = jSONObject.getString("displayIcon");
        List arrayList = new ArrayList();
        JSONArray jSONArray = jSONObject.getJSONArray("paymentMethods");
        int length = jSONArray.length();
        for (int i = 0; i < length; i++) {
            JSONObject jSONObject2 = jSONArray.getJSONObject(i);
            arrayList.add(new C1064l(jSONObject2.getString("id"), jSONObject2.getString("name"), jSONObject2.getString("displayName"), jSONObject2.getString("displayIcon")));
        }
        return new C1063k(string, string2, string3, string4, arrayList);
    }

    /* renamed from: c */
    public static void m876c(String str) {
        f1174d = str;
    }

    @NotNull
    /* renamed from: d */
    private static C1028c m877d(JSONObject jSONObject) {
        boolean z = false;
        if (jSONObject.has("disableCountrySelection")) {
            z = jSONObject.getBoolean("disableCountrySelection");
        }
        return new C1028c(z);
    }

    /* renamed from: d */
    public static void m878d(String str) {
        f1176f = str;
    }

    @NotNull
    /* renamed from: e */
    private static C1034h m879e(@NotNull JSONObject jSONObject) {
        String str = null;
        String string = jSONObject.getString("country");
        List arrayList = new ArrayList();
        JSONArray jSONArray = jSONObject.getJSONArray("items");
        for (int i = 0; i < jSONArray.length(); i++) {
            arrayList.add(C1062j.m872b(jSONArray.getJSONObject(i)));
        }
        List a = jSONObject.has("countries") ? C1062j.m862a(jSONObject.getJSONArray("countries")) : null;
        Map h = jSONObject.has("baseUrls") ? C1062j.m886h(jSONObject.getJSONObject("baseUrls")) : null;
        C1028c d = jSONObject.has("config") ? C1062j.m877d(jSONObject.getJSONObject("config")) : null;
        if (jSONObject.has("redirectUrl")) {
            str = jSONObject.getString("redirectUrl");
        }
        return new C1034h(string, arrayList, a, h, d, str);
    }

    /* renamed from: e */
    public static void m880e(String str) {
        f1177g = str;
    }

    @NotNull
    /* renamed from: f */
    private static C1033g m881f(@NotNull JSONObject jSONObject) {
        if (!jSONObject.getBoolean("status")) {
            return new C1033g(C1062j.m863a(jSONObject));
        }
        String string = jSONObject.getString("country");
        String string2 = jSONObject.getString(AppsFlyerProperties.CURRENCY_CODE);
        SkuDetails b = C1062j.m872b(jSONObject.getJSONObject("item"));
        List arrayList = new ArrayList();
        JSONArray jSONArray = jSONObject.getJSONArray("paymentTypes");
        int length = jSONArray.length();
        for (int i = 0; i < length; i++) {
            arrayList.add(C1062j.m875c(jSONArray.getJSONObject(i)));
        }
        return new C1033g(string, string2, b, arrayList, C1062j.m886h(jSONObject.getJSONObject("baseUrls")), jSONObject.has("countries") ? C1062j.m862a(jSONObject.getJSONArray("countries")) : null, jSONObject.has("config") ? C1062j.m877d(jSONObject.getJSONObject("config")) : null, jSONObject.has("redirectUrl") ? jSONObject.getString("redirectUrl") : null);
    }

    /* renamed from: f */
    public static void m882f(String str) {
        f1178h = str;
    }

    @NotNull
    /* renamed from: g */
    private static C1066n m883g(@NotNull JSONObject jSONObject) {
        return !jSONObject.getBoolean("status") ? new C1066n(C1062j.m863a(jSONObject)) : new C1066n(jSONObject.getString("redirectUrl"), jSONObject);
    }

    /* renamed from: g */
    public static void m884g(String str) {
        f1175e = str;
    }

    /* renamed from: h */
    private static HttpURLConnection m885h(String str) {
        try {
            HttpURLConnection httpURLConnection = (HttpURLConnection) new URL(str).openConnection();
            httpURLConnection.addRequestProperty("Go-Token", f1172b);
            httpURLConnection.setRequestProperty("http.keepAlive", "false");
            httpURLConnection.setRequestMethod(HttpRequest.METHOD_GET);
            httpURLConnection.setConnectTimeout(5000);
            httpURLConnection.setReadTimeout(5000);
            httpURLConnection.setDoInput(true);
            httpURLConnection.connect();
            return httpURLConnection;
        } catch (Exception e) {
            throw new IabException(-1001, "Timeout", e);
        } catch (Exception e2) {
            throw new IabException(-1001, "Connection failed", e2);
        } catch (Exception e22) {
            throw new IabException(-1001, "I/O exception", e22);
        } catch (Exception e222) {
            throw new IabException(-1008, e222.getLocalizedMessage(), e222);
        }
    }

    @NotNull
    /* renamed from: h */
    private static Map m886h(@NotNull JSONObject jSONObject) {
        Map hashMap = new HashMap();
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
            HttpURLConnection h = C1062j.m885h(str);
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
        } catch (Exception e) {
            throw new IabException(-1001, "Timeout", e);
        } catch (Exception e2) {
            throw new IabException(-1001, "Connection failed", e2);
        } catch (Exception e22) {
            throw new IabException(-1001, "I/O exception", e22);
        } catch (Exception e222) {
            throw new IabException(-1008, e222.getLocalizedMessage(), e222);
        } catch (Throwable th) {
            if (inputStream != null) {
                inputStream.close();
            }
        }
    }

    /* renamed from: j */
    private static JSONObject m888j(String str) {
        try {
            return new JSONObject(C1062j.m887i(str));
        } catch (Exception e) {
            throw new IabException(-1002, "Bad response", e);
        }
    }
}
