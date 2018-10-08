package net.gogame.gopay.sdk.support;

import java.util.HashMap;
import java.util.Locale;
import java.util.Map;

/* renamed from: net.gogame.gopay.sdk.support.s */
public final class C1404s {
    /* renamed from: a */
    private static final Map f3637a = new HashMap();

    static {
        C1404s.m3973a("store_title", "Store");
        C1404s.m3973a("select_payment_header", "Please select the payment method that you prefer");
        C1404s.m3973a("select_payment_subheader", "We only show the payment methods that are available in your country.");
        C1404s.m3973a("back_button_caption", "Back");
    }

    /* renamed from: a */
    public static String m3972a(String str) {
        String locale = Locale.getDefault().toString();
        while (true) {
            String b = C1404s.m3974b(locale, str);
            if (b != null) {
                return b;
            }
            int lastIndexOf = locale.lastIndexOf(95);
            if (lastIndexOf < 0) {
                return C1404s.m3974b(null, str);
            }
            locale = locale.substring(0, lastIndexOf);
        }
    }

    /* renamed from: a */
    private static void m3973a(String str, String str2) {
        Map map = (Map) f3637a.get(str);
        if (map == null) {
            map = new HashMap();
            f3637a.put(str, map);
        }
        map.put(null, str2);
    }

    /* renamed from: b */
    private static String m3974b(String str, String str2) {
        Map map = (Map) f3637a.get(str2);
        return map == null ? null : (String) map.get(str);
    }
}
