package com.amazon.device.iap.internal.p014c;

import android.content.SharedPreferences.Editor;
import com.amazon.device.iap.internal.C0236d;
import com.amazon.device.iap.internal.util.C0243d;
import com.amazon.device.iap.internal.util.C0244e;
import com.appsflyer.share.Constants;
import org.onepf.oms.appstore.AmazonAppstoreBillingService;

/* renamed from: com.amazon.device.iap.internal.c.c */
public class C0233c {
    /* renamed from: a */
    private static C0233c f81a = new C0233c();
    /* renamed from: b */
    private static final String f82b = C0233c.class.getSimpleName();
    /* renamed from: c */
    private static final String f83c = (C0233c.class.getName() + "_PREFS_");

    /* renamed from: a */
    public static C0233c m134a() {
        return f81a;
    }

    /* renamed from: a */
    public String m135a(String str, String str2) {
        String str3 = null;
        C0244e.m173a(f82b, "enter getReceiptIdFromSku for sku [" + str2 + "], user [" + str + "]");
        try {
            C0243d.m170a(str, AmazonAppstoreBillingService.JSON_KEY_USER_ID);
            C0243d.m170a(str2, "sku");
            Object b = C0236d.m142d().m151b();
            C0243d.m169a(b, "context");
            str3 = b.getSharedPreferences(f83c + str, 0).getString(str2, null);
        } catch (Throwable th) {
            C0244e.m173a(f82b, "error in saving v1 Entitlement:" + str2 + ":" + th.getMessage());
        }
        C0244e.m173a(f82b, "leaving saveEntitlementRecord for sku [" + str2 + "], user [" + str + "]");
        return str3;
    }

    /* renamed from: a */
    public void m136a(String str, String str2, String str3) {
        C0244e.m173a(f82b, "enter saveEntitlementRecord for v1 Entitlement [" + str2 + Constants.URL_PATH_DELIMITER + str3 + "], user [" + str + "]");
        try {
            C0243d.m170a(str, AmazonAppstoreBillingService.JSON_KEY_USER_ID);
            C0243d.m170a(str2, "receiptId");
            C0243d.m170a(str3, "sku");
            Object b = C0236d.m142d().m151b();
            C0243d.m169a(b, "context");
            Editor edit = b.getSharedPreferences(f83c + str, 0).edit();
            edit.putString(str3, str2);
            edit.commit();
        } catch (Throwable th) {
            C0244e.m173a(f82b, "error in saving v1 Entitlement:" + str2 + Constants.URL_PATH_DELIMITER + str3 + ":" + th.getMessage());
        }
        C0244e.m173a(f82b, "leaving saveEntitlementRecord for v1 Entitlement [" + str2 + Constants.URL_PATH_DELIMITER + str3 + "], user [" + str + "]");
    }
}
