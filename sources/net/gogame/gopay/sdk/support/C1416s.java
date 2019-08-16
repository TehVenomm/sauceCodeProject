package net.gogame.gopay.sdk.support;

import java.util.HashMap;
import java.util.Locale;
import java.util.Map;

/* renamed from: net.gogame.gopay.sdk.support.s */
public final class C1416s {

    /* renamed from: a */
    private static final Map f1180a = new HashMap();

    static {
        m944a("store_title", "Store");
        m944a("select_payment_header", "Please select the payment method that you prefer");
        m944a("select_payment_subheader", "We only show the payment methods that are available in your country.");
        m944a("back_button_caption", "Back");
    }

    /* renamed from: a */
    public static String m943a(String str) {
        String locale = Locale.getDefault().toString();
        while (true) {
            String b = m945b(locale, str);
            if (b != null) {
                return b;
            }
            int lastIndexOf = locale.lastIndexOf(95);
            if (lastIndexOf < 0) {
                return m945b(null, str);
            }
            locale = locale.substring(0, lastIndexOf);
        }
    }

    /* renamed from: a */
    private static void m944a(String str, String str2) {
        Map map = (Map) f1180a.get(str);
        if (map == null) {
            map = new HashMap();
            f1180a.put(str, map);
        }
        map.put(null, str2);
    }

    /* renamed from: b */
    private static String m945b(String str, String str2) {
        Map map = (Map) f1180a.get(str2);
        if (map == null) {
            return null;
        }
        return (String) map.get(str);
    }
}
