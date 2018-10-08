package com.amazon.device.iap.internal.p010c;

import android.content.SharedPreferences;
import android.content.SharedPreferences.Editor;
import android.os.Handler;
import com.amazon.device.iap.internal.C0236d;
import com.amazon.device.iap.internal.p001b.C0215d;
import com.amazon.device.iap.internal.util.C0240a;
import com.amazon.device.iap.internal.util.C0243d;
import com.amazon.device.iap.internal.util.C0244e;
import com.amazon.device.iap.model.Receipt;
import com.appsflyer.share.Constants;
import java.util.HashSet;
import java.util.Map;
import java.util.Set;
import org.json.JSONException;
import org.json.JSONObject;
import org.onepf.oms.appstore.AmazonAppstoreBillingService;

/* renamed from: com.amazon.device.iap.internal.c.a */
public class C0231a {
    /* renamed from: a */
    private static final String f74a = C0231a.class.getSimpleName();
    /* renamed from: b */
    private static final String f75b = (C0231a.class.getName() + "_PREFS");
    /* renamed from: c */
    private static final String f76c = (C0231a.class.getName() + "_CLEANER_PREFS");
    /* renamed from: d */
    private static int f77d = 604800000;
    /* renamed from: e */
    private static final C0231a f78e = new C0231a();

    /* renamed from: a */
    public static C0231a m120a() {
        return f78e;
    }

    /* renamed from: a */
    private void m121a(long j) {
        Object b = C0236d.m142d().m151b();
        C0243d.m169a(b, "context");
        Editor edit = b.getSharedPreferences(f76c, 0).edit();
        edit.putLong("LAST_CLEANING_TIME", j);
        edit.commit();
    }

    /* renamed from: e */
    private void m125e() {
        C0244e.m173a(f74a, "enter old receipts cleanup! ");
        final Object b = C0236d.m142d().m151b();
        C0243d.m169a(b, "context");
        m121a(System.currentTimeMillis());
        new Handler().post(new Runnable(this) {
            /* renamed from: b */
            final /* synthetic */ C0231a f73b;

            public void run() {
                try {
                    C0244e.m173a(C0231a.f74a, "perform house keeping! ");
                    SharedPreferences sharedPreferences = b.getSharedPreferences(C0231a.f75b, 0);
                    for (String str : sharedPreferences.getAll().keySet()) {
                        if (System.currentTimeMillis() - C0234d.m137a(sharedPreferences.getString(str, null)).m140c() > ((long) C0231a.f77d)) {
                            C0244e.m173a(C0231a.f74a, "house keeping - try remove Receipt:" + str + " since it's too old");
                            this.f73b.m127a(str);
                        }
                    }
                } catch (C0235e e) {
                    C0244e.m173a(C0231a.f74a, "house keeping - try remove Receipt:" + str + " since it's invalid ");
                    this.f73b.m127a(str);
                } catch (Throwable th) {
                    C0244e.m173a(C0231a.f74a, "Error in running cleaning job:" + th);
                }
            }
        });
    }

    /* renamed from: f */
    private long m126f() {
        Object b = C0236d.m142d().m151b();
        C0243d.m169a(b, "context");
        long currentTimeMillis = System.currentTimeMillis();
        long j = b.getSharedPreferences(f76c, 0).getLong("LAST_CLEANING_TIME", 0);
        if (j != 0) {
            return j;
        }
        m121a(currentTimeMillis);
        return currentTimeMillis;
    }

    /* renamed from: a */
    public void m127a(String str) {
        C0244e.m173a(f74a, "enter removeReceipt for receipt[" + str + "]");
        Object b = C0236d.m142d().m151b();
        C0243d.m169a(b, "context");
        Editor edit = b.getSharedPreferences(f75b, 0).edit();
        edit.remove(str);
        edit.commit();
        C0244e.m173a(f74a, "leave removeReceipt for receipt[" + str + "]");
    }

    /* renamed from: a */
    public void m128a(String str, String str2, String str3, String str4) {
        C0244e.m173a(f74a, "enter saveReceipt for receipt [" + str4 + "]");
        try {
            C0243d.m170a(str2, AmazonAppstoreBillingService.JSON_KEY_USER_ID);
            C0243d.m170a(str3, "receiptId");
            C0243d.m170a(str4, "receiptString");
            Object b = C0236d.m142d().m151b();
            C0243d.m169a(b, "context");
            C0234d c0234d = new C0234d(str2, str4, str, System.currentTimeMillis());
            Editor edit = b.getSharedPreferences(f75b, 0).edit();
            edit.putString(str3, c0234d.m141d());
            edit.commit();
        } catch (Throwable th) {
            C0244e.m173a(f74a, "error in saving pending receipt:" + str + Constants.URL_PATH_DELIMITER + str4 + ":" + th.getMessage());
        }
        C0244e.m173a(f74a, "leaving saveReceipt for receipt id [" + str3 + "]");
    }

    /* renamed from: b */
    public Set<Receipt> m129b(String str) {
        Object b = C0236d.m142d().m151b();
        C0243d.m169a(b, "context");
        C0244e.m173a(f74a, "enter getLocalReceipts for user[" + str + "]");
        Set<Receipt> hashSet = new HashSet();
        if (C0243d.m172a(str)) {
            C0244e.m175b(f74a, "empty UserId: " + str);
            throw new RuntimeException("Invalid UserId:" + str);
        }
        Map all = b.getSharedPreferences(f75b, 0).getAll();
        for (String str2 : all.keySet()) {
            String str3 = (String) all.get(str2);
            try {
                C0234d a = C0234d.m137a(str3);
                hashSet.add(C0240a.m159a(new JSONObject(a.m139b()), str, a.m138a()));
            } catch (C0215d e) {
                m127a(str2);
                C0244e.m175b(f74a, "failed to verify signature:[" + str3 + "]");
            } catch (JSONException e2) {
                m127a(str2);
                C0244e.m175b(f74a, "failed to convert string to JSON object:[" + str3 + "]");
            } catch (Throwable th) {
                C0244e.m175b(f74a, "failed to load the receipt from SharedPreference:[" + str3 + "]");
            }
        }
        C0244e.m173a(f74a, "leaving getLocalReceipts for user[" + str + "], " + hashSet.size() + " local receipts found.");
        if (System.currentTimeMillis() - m126f() > ((long) f77d)) {
            m125e();
        }
        return hashSet;
    }

    /* renamed from: c */
    public String m130c(String str) {
        String str2 = null;
        Object b = C0236d.m142d().m151b();
        C0243d.m169a(b, "context");
        if (C0243d.m172a(str)) {
            C0244e.m175b(f74a, "empty receiptId: " + str);
            throw new RuntimeException("Invalid ReceiptId:" + str);
        }
        String string = b.getSharedPreferences(f75b, 0).getString(str, str2);
        if (string != null) {
            try {
                str2 = C0234d.m137a(string).m138a();
            } catch (C0235e e) {
            }
        }
        return str2;
    }
}
