package im.getsocial.sdk.internal.p033c;

import android.content.Context;
import android.content.SharedPreferences;
import im.getsocial.sdk.internal.p033c.p041b.XdbacJlTDQ;

/* renamed from: im.getsocial.sdk.internal.c.JbBdMtJmlU */
public class JbBdMtJmlU implements bpiSwUyLit {
    /* renamed from: a */
    private final SharedPreferences f1220a;

    @XdbacJlTDQ
    JbBdMtJmlU(Context context) {
        this.f1220a = context.getSharedPreferences("getsocial", 0);
    }

    /* renamed from: a */
    public final void mo4359a(String str, long j) {
        this.f1220a.edit().putLong(str, j).apply();
    }

    /* renamed from: a */
    public final void mo4360a(String str, String str2) {
        this.f1220a.edit().putString(str, str2).apply();
    }

    /* renamed from: a */
    public final boolean mo4361a(String str) {
        return this.f1220a.contains(str);
    }

    /* renamed from: b */
    public final String mo4362b(String str) {
        return this.f1220a.getString(str, null);
    }

    /* renamed from: c */
    public final long mo4363c(String str) {
        return this.f1220a.getLong(str, 0);
    }

    /* renamed from: d */
    public final int mo4364d(String str) {
        return this.f1220a.getInt(str, 0);
    }

    /* renamed from: e */
    public final void mo4365e(String str) {
        this.f1220a.edit().remove(str).apply();
    }
}
