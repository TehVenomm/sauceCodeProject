package com.amazon.device.iap.internal.util;

import android.content.Context;
import android.content.SharedPreferences.Editor;
import com.amazon.device.iap.internal.C0401d;
import org.onepf.oms.appstore.AmazonAppstoreBillingService;

/* renamed from: com.amazon.device.iap.internal.util.b */
public class C0406b {

    /* renamed from: a */
    private static final String f124a = (C0406b.class.getName() + "_PREFS");

    /* renamed from: a */
    public static String m161a(String str) {
        C0408d.m164a((Object) str, AmazonAppstoreBillingService.JSON_KEY_USER_ID);
        Context b = C0401d.m137d().mo6264b();
        C0408d.m164a((Object) b, "context");
        return b.getSharedPreferences(f124a, 0).getString(str, null);
    }

    /* renamed from: a */
    public static void m162a(String str, String str2) {
        C0408d.m164a((Object) str, AmazonAppstoreBillingService.JSON_KEY_USER_ID);
        Context b = C0401d.m137d().mo6264b();
        C0408d.m164a((Object) b, "context");
        Editor edit = b.getSharedPreferences(f124a, 0).edit();
        edit.putString(str, str2);
        edit.commit();
    }
}
