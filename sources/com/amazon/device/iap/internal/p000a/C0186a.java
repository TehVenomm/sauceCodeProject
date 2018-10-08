package com.amazon.device.iap.internal.p000a;

import android.util.Log;
import com.amazon.device.iap.internal.C0185a;

/* renamed from: com.amazon.device.iap.internal.a.a */
public class C0186a implements C0185a {
    /* renamed from: a */
    private static String m20a(String str) {
        return "In App Purchasing SDK - Sandbox Mode: " + str;
    }

    /* renamed from: a */
    public void mo1176a(String str, String str2) {
        Log.d(str, C0186a.m20a(str2));
    }

    /* renamed from: a */
    public boolean mo1177a() {
        return true;
    }

    /* renamed from: b */
    public void mo1178b(String str, String str2) {
        Log.e(str, C0186a.m20a(str2));
    }

    /* renamed from: b */
    public boolean mo1179b() {
        return true;
    }
}
