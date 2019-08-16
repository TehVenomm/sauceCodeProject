package com.amazon.device.iap.internal.p004a;

import android.util.Log;
import com.amazon.device.iap.internal.C0350a;

/* renamed from: com.amazon.device.iap.internal.a.a */
public class C0351a implements C0350a {
    /* renamed from: a */
    private static String m12a(String str) {
        return "In App Purchasing SDK - Sandbox Mode: " + str;
    }

    /* renamed from: a */
    public void mo6192a(String str, String str2) {
        Log.d(str, m12a(str2));
    }

    /* renamed from: a */
    public boolean mo6193a() {
        return true;
    }

    /* renamed from: b */
    public void mo6194b(String str, String str2) {
        Log.e(str, m12a(str2));
    }

    /* renamed from: b */
    public boolean mo6195b() {
        return true;
    }
}
