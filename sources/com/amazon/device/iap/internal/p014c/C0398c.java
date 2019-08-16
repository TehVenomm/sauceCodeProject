package com.amazon.device.iap.internal.p014c;

import android.content.Context;
import android.content.SharedPreferences.Editor;
import com.amazon.device.iap.internal.C0401d;
import com.amazon.device.iap.internal.util.C0408d;
import com.amazon.device.iap.internal.util.C0409e;
import com.appsflyer.share.Constants;
import org.onepf.oms.appstore.AmazonAppstoreBillingService;

/* renamed from: com.amazon.device.iap.internal.c.c */
public class C0398c {

    /* renamed from: a */
    private static C0398c f100a = new C0398c();

    /* renamed from: b */
    private static final String f101b = C0398c.class.getSimpleName();

    /* renamed from: c */
    private static final String f102c = (C0398c.class.getName() + "_PREFS_");

    /* renamed from: a */
    public static C0398c m129a() {
        return f100a;
    }

    /* renamed from: a */
    public String mo6251a(String str, String str2) {
        String str3 = null;
        C0409e.m168a(f101b, "enter getReceiptIdFromSku for sku [" + str2 + "], user [" + str + "]");
        try {
            C0408d.m165a(str, AmazonAppstoreBillingService.JSON_KEY_USER_ID);
            C0408d.m165a(str2, "sku");
            Context b = C0401d.m137d().mo6264b();
            C0408d.m164a((Object) b, "context");
            str3 = b.getSharedPreferences(f102c + str, 0).getString(str2, null);
        } catch (Throwable th) {
            C0409e.m168a(f101b, "error in saving v1 Entitlement:" + str2 + ":" + th.getMessage());
        }
        C0409e.m168a(f101b, "leaving saveEntitlementRecord for sku [" + str2 + "], user [" + str + "]");
        return str3;
    }

    /* renamed from: a */
    public void mo6252a(String str, String str2, String str3) {
        C0409e.m168a(f101b, "enter saveEntitlementRecord for v1 Entitlement [" + str2 + Constants.URL_PATH_DELIMITER + str3 + "], user [" + str + "]");
        try {
            C0408d.m165a(str, AmazonAppstoreBillingService.JSON_KEY_USER_ID);
            C0408d.m165a(str2, "receiptId");
            C0408d.m165a(str3, "sku");
            Context b = C0401d.m137d().mo6264b();
            C0408d.m164a((Object) b, "context");
            Editor edit = b.getSharedPreferences(f102c + str, 0).edit();
            edit.putString(str3, str2);
            edit.commit();
        } catch (Throwable th) {
            C0409e.m168a(f101b, "error in saving v1 Entitlement:" + str2 + Constants.URL_PATH_DELIMITER + str3 + ":" + th.getMessage());
        }
        C0409e.m168a(f101b, "leaving saveEntitlementRecord for v1 Entitlement [" + str2 + Constants.URL_PATH_DELIMITER + str3 + "], user [" + str + "]");
    }
}
