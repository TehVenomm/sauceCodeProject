package com.amazon.device.iap.internal.p014c;

import org.json.JSONException;
import org.json.JSONObject;

/* renamed from: com.amazon.device.iap.internal.c.d */
class C0399d {

    /* renamed from: a */
    private final String f103a;

    /* renamed from: b */
    private final String f104b;

    /* renamed from: c */
    private final long f105c;

    /* renamed from: d */
    private final String f106d;

    public C0399d(String str, String str2, String str3, long j) {
        this.f103a = str;
        this.f104b = str2;
        this.f106d = str3;
        this.f105c = j;
    }

    /* renamed from: a */
    public static C0399d m132a(String str) throws C0400e {
        try {
            JSONObject jSONObject = new JSONObject(str);
            return new C0399d(jSONObject.getString("KEY_USER_ID"), jSONObject.getString("KEY_RECEIPT_STRING"), jSONObject.getString("KEY_REQUEST_ID"), jSONObject.getLong("KEY_TIMESTAMP"));
        } catch (Throwable th) {
            throw new C0400e("Input invalid for PendingReceipt Object:" + str, th);
        }
    }

    /* renamed from: a */
    public String mo6253a() {
        return this.f106d;
    }

    /* renamed from: b */
    public String mo6254b() {
        return this.f104b;
    }

    /* renamed from: c */
    public long mo6255c() {
        return this.f105c;
    }

    /* renamed from: d */
    public String mo6256d() throws JSONException {
        JSONObject jSONObject = new JSONObject();
        jSONObject.put("KEY_USER_ID", this.f103a);
        jSONObject.put("KEY_RECEIPT_STRING", this.f104b);
        jSONObject.put("KEY_REQUEST_ID", this.f106d);
        jSONObject.put("KEY_TIMESTAMP", this.f105c);
        return jSONObject.toString();
    }
}
