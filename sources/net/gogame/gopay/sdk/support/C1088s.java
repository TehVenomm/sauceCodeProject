package net.gogame.gopay.sdk.support;

import java.util.HashMap;
import java.util.Locale;
import java.util.Map;

/* renamed from: net.gogame.gopay.sdk.support.s */
public final class C1088s {
    /* renamed from: a */
    private static final Map f1249a = new HashMap();

    static {
        C1088s.m948a("store_title", "Store");
        C1088s.m948a("select_payment_header", "Please select the payment method that you prefer");
        C1088s.m948a("select_payment_subheader", "We only show the payment methods that are available in your country.");
        C1088s.m948a("back_button_caption", "Back");
    }

    /* renamed from: a */
    public static String m947a(String str) {
        String locale = Locale.getDefault().toString();
        while (true) {
            String b = C1088s.m949b(locale, str);
            if (b != null) {
                return b;
            }
            int lastIndexOf = locale.lastIndexOf(95);
            if (lastIndexOf < 0) {
                return C1088s.m949b(null, str);
            }
            locale = locale.substring(0, lastIndexOf);
        }
    }

    /* renamed from: a */
    private static void m948a(String str, String str2) {
        Map map = (Map) f1249a.get(str);
        if (map == null) {
            map = new HashMap();
            f1249a.put(str, map);
        }
        map.put(null, str2);
    }

    /* renamed from: b */
    private static String m949b(String str, String str2) {
        Map map = (Map) f1249a.get(str2);
        return map == null ? null : (String) map.get(str);
    }
}
