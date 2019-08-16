package com.amazon.device.iap.internal.p014c;

import android.content.Context;
import android.content.SharedPreferences;
import android.content.SharedPreferences.Editor;
import android.os.Handler;
import com.amazon.device.iap.internal.C0401d;
import com.amazon.device.iap.internal.p005b.C0373d;
import com.amazon.device.iap.internal.util.C0404a;
import com.amazon.device.iap.internal.util.C0408d;
import com.amazon.device.iap.internal.util.C0409e;
import com.amazon.device.iap.model.Receipt;
import com.appsflyer.share.Constants;
import java.util.HashSet;
import java.util.Map;
import java.util.Set;
import org.json.JSONException;
import org.json.JSONObject;
import org.onepf.oms.appstore.AmazonAppstoreBillingService;

/* renamed from: com.amazon.device.iap.internal.c.a */
public class C0395a {
    /* access modifiers changed from: private */

    /* renamed from: a */
    public static final String f91a = C0395a.class.getSimpleName();
    /* access modifiers changed from: private */

    /* renamed from: b */
    public static final String f92b = (C0395a.class.getName() + "_PREFS");

    /* renamed from: c */
    private static final String f93c = (C0395a.class.getName() + "_CLEANER_PREFS");
    /* access modifiers changed from: private */

    /* renamed from: d */
    public static int f94d = 604800000;

    /* renamed from: e */
    private static final C0395a f95e = new C0395a();

    /* renamed from: a */
    public static C0395a m115a() {
        return f95e;
    }

    /* renamed from: a */
    private void m116a(long j) {
        Context b = C0401d.m137d().mo6264b();
        C0408d.m164a((Object) b, "context");
        Editor edit = b.getSharedPreferences(f93c, 0).edit();
        edit.putLong("LAST_CLEANING_TIME", j);
        edit.commit();
    }

    /* renamed from: e */
    private void m120e() {
        C0409e.m168a(f91a, "enter old receipts cleanup! ");
        final Context b = C0401d.m137d().mo6264b();
        C0408d.m164a((Object) b, "context");
        m116a(System.currentTimeMillis());
        new Handler().post(new Runnable() {
            public void run() {
                try {
                    C0409e.m168a(C0395a.f91a, "perform house keeping! ");
                    SharedPreferences sharedPreferences = b.getSharedPreferences(C0395a.f92b, 0);
                    for (String str : sharedPreferences.getAll().keySet()) {
                        if (System.currentTimeMillis() - C0399d.m132a(sharedPreferences.getString(str, null)).mo6255c() > ((long) C0395a.f94d)) {
                            C0409e.m168a(C0395a.f91a, "house keeping - try remove Receipt:" + str + " since it's too old");
                            C0395a.this.mo6244a(str);
                        }
                    }
                } catch (C0400e e) {
                    C0409e.m168a(C0395a.f91a, "house keeping - try remove Receipt:" + str + " since it's invalid ");
                    C0395a.this.mo6244a(str);
                } catch (Throwable th) {
                    C0409e.m168a(C0395a.f91a, "Error in running cleaning job:" + th);
                }
            }
        });
    }

    /* renamed from: f */
    private long m121f() {
        Context b = C0401d.m137d().mo6264b();
        C0408d.m164a((Object) b, "context");
        long currentTimeMillis = System.currentTimeMillis();
        long j = b.getSharedPreferences(f93c, 0).getLong("LAST_CLEANING_TIME", 0);
        if (j != 0) {
            return j;
        }
        m116a(currentTimeMillis);
        return currentTimeMillis;
    }

    /* renamed from: a */
    public void mo6244a(String str) {
        C0409e.m168a(f91a, "enter removeReceipt for receipt[" + str + "]");
        Context b = C0401d.m137d().mo6264b();
        C0408d.m164a((Object) b, "context");
        Editor edit = b.getSharedPreferences(f92b, 0).edit();
        edit.remove(str);
        edit.commit();
        C0409e.m168a(f91a, "leave removeReceipt for receipt[" + str + "]");
    }

    /* renamed from: a */
    public void mo6245a(String str, String str2, String str3, String str4) {
        C0409e.m168a(f91a, "enter saveReceipt for receipt [" + str4 + "]");
        try {
            C0408d.m165a(str2, AmazonAppstoreBillingService.JSON_KEY_USER_ID);
            C0408d.m165a(str3, "receiptId");
            C0408d.m165a(str4, "receiptString");
            Context b = C0401d.m137d().mo6264b();
            C0408d.m164a((Object) b, "context");
            C0399d dVar = new C0399d(str2, str4, str, System.currentTimeMillis());
            Editor edit = b.getSharedPreferences(f92b, 0).edit();
            edit.putString(str3, dVar.mo6256d());
            edit.commit();
        } catch (Throwable th) {
            C0409e.m168a(f91a, "error in saving pending receipt:" + str + Constants.URL_PATH_DELIMITER + str4 + ":" + th.getMessage());
        }
        C0409e.m168a(f91a, "leaving saveReceipt for receipt id [" + str3 + "]");
    }

    /* renamed from: b */
    public Set<Receipt> mo6246b(String str) {
        Context b = C0401d.m137d().mo6264b();
        C0408d.m164a((Object) b, "context");
        C0409e.m168a(f91a, "enter getLocalReceipts for user[" + str + "]");
        HashSet hashSet = new HashSet();
        if (C0408d.m167a(str)) {
            C0409e.m170b(f91a, "empty UserId: " + str);
            throw new RuntimeException("Invalid UserId:" + str);
        }
        Map all = b.getSharedPreferences(f92b, 0).getAll();
        for (String str2 : all.keySet()) {
            String str3 = (String) all.get(str2);
            try {
                C0399d a = C0399d.m132a(str3);
                hashSet.add(C0404a.m154a(new JSONObject(a.mo6254b()), str, a.mo6253a()));
            } catch (C0373d e) {
                mo6244a(str2);
                C0409e.m170b(f91a, "failed to verify signature:[" + str3 + "]");
            } catch (JSONException e2) {
                mo6244a(str2);
                C0409e.m170b(f91a, "failed to convert string to JSON object:[" + str3 + "]");
            } catch (Throwable th) {
                C0409e.m170b(f91a, "failed to load the receipt from SharedPreference:[" + str3 + "]");
            }
        }
        C0409e.m168a(f91a, "leaving getLocalReceipts for user[" + str + "], " + hashSet.size() + " local receipts found.");
        if (System.currentTimeMillis() - m121f() > ((long) f94d)) {
            m120e();
        }
        return hashSet;
    }

    /* renamed from: c */
    public String mo6247c(String str) {
        String str2 = null;
        Context b = C0401d.m137d().mo6264b();
        C0408d.m164a((Object) b, "context");
        if (C0408d.m167a(str)) {
            C0409e.m170b(f91a, "empty receiptId: " + str);
            throw new RuntimeException("Invalid ReceiptId:" + str);
        }
        String string = b.getSharedPreferences(f92b, 0).getString(str, str2);
        if (string == null) {
            return str2;
        }
        try {
            return C0399d.m132a(string).mo6253a();
        } catch (C0400e e) {
            return str2;
        }
    }
}
