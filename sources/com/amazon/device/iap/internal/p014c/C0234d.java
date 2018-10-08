package com.amazon.device.iap.internal.p014c;

import org.json.JSONException;
import org.json.JSONObject;

/* renamed from: com.amazon.device.iap.internal.c.d */
class C0234d {
    /* renamed from: a */
    private final String f84a;
    /* renamed from: b */
    private final String f85b;
    /* renamed from: c */
    private final long f86c;
    /* renamed from: d */
    private final String f87d;

    public C0234d(String str, String str2, String str3, long j) {
        this.f84a = str;
        this.f85b = str2;
        this.f87d = str3;
        this.f86c = j;
    }

    /* renamed from: a */
    public static C0234d m137a(String str) throws C0235e {
        try {
            JSONObject jSONObject = new JSONObject(str);
            return new C0234d(jSONObject.getString("KEY_USER_ID"), jSONObject.getString("KEY_RECEIPT_STRING"), jSONObject.getString("KEY_REQUEST_ID"), jSONObject.getLong("KEY_TIMESTAMP"));
        } catch (Throwable th) {
            C0235e c0235e = new C0235e("Input invalid for PendingReceipt Object:" + str, th);
        }
    }

    /* renamed from: a */
    public String m138a() {
        return this.f87d;
    }

    /* renamed from: b */
    public String m139b() {
        return this.f85b;
    }

    /* renamed from: c */
    public long m140c() {
        return this.f86c;
    }

    /* renamed from: d */
    public String m141d() throws JSONException {
        JSONObject jSONObject = new JSONObject();
        jSONObject.put("KEY_USER_ID", this.f84a);
        jSONObject.put("KEY_RECEIPT_STRING", this.f85b);
        jSONObject.put("KEY_REQUEST_ID", this.f87d);
        jSONObject.put("KEY_TIMESTAMP", this.f86c);
        return jSONObject.toString();
    }
}
