package com.amazon.device.iap.internal.util;

import android.content.SharedPreferences.Editor;
import com.amazon.device.iap.internal.C0236d;
import org.onepf.oms.appstore.AmazonAppstoreBillingService;

/* renamed from: com.amazon.device.iap.internal.util.b */
public class C0241b {
    /* renamed from: a */
    private static final String f105a = (C0241b.class.getName() + "_PREFS");

    /* renamed from: a */
    public static String m166a(String str) {
        C0243d.m169a((Object) str, AmazonAppstoreBillingService.JSON_KEY_USER_ID);
        Object b = C0236d.m142d().m151b();
        C0243d.m169a(b, "context");
        return b.getSharedPreferences(f105a, 0).getString(str, null);
    }

    /* renamed from: a */
    public static void m167a(String str, String str2) {
        C0243d.m169a((Object) str, AmazonAppstoreBillingService.JSON_KEY_USER_ID);
        Object b = C0236d.m142d().m151b();
        C0243d.m169a(b, "context");
        Editor edit = b.getSharedPreferences(f105a, 0).edit();
        edit.putString(str, str2);
        edit.commit();
    }
}
