package com.zopim.android.sdk.chatlog;

import com.zopim.android.sdk.model.ChatLog.Rating;

/* renamed from: com.zopim.android.sdk.chatlog.t */
final class C0853t extends aa<C0853t> {
    /* renamed from: a */
    public Rating f831a = Rating.UNKNOWN;
    /* renamed from: b */
    public String f832b;

    C0853t(aa aaVar) {
        super(aaVar);
    }

    /* renamed from: a */
    public void m695a(C0853t c0853t) {
        super.mo4242a(c0853t);
        this.f831a = c0853t.f831a;
        this.f832b = c0853t.f832b;
    }

    /* JADX WARNING: inconsistent code. */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    public boolean equals(java.lang.Object r5) {
        /*
        r4 = this;
        r0 = 1;
        r1 = 0;
        if (r4 != r5) goto L_0x0006;
    L_0x0004:
        r1 = r0;
    L_0x0005:
        return r1;
    L_0x0006:
        if (r5 == 0) goto L_0x0005;
    L_0x0008:
        r2 = r4.getClass();
        r3 = r5.getClass();
        if (r2 != r3) goto L_0x0005;
    L_0x0012:
        r2 = super.equals(r5);
        if (r2 == 0) goto L_0x0005;
    L_0x0018:
        r5 = (com.zopim.android.sdk.chatlog.C0853t) r5;
        r2 = r4.f831a;
        r3 = r5.f831a;
        if (r2 != r3) goto L_0x0005;
    L_0x0020:
        r2 = r4.f832b;
        if (r2 == 0) goto L_0x0031;
    L_0x0024:
        r2 = r4.f832b;
        r3 = r5.f832b;
        r2 = r2.equals(r3);
        if (r2 != 0) goto L_0x002f;
    L_0x002e:
        r0 = r1;
    L_0x002f:
        r1 = r0;
        goto L_0x0005;
    L_0x0031:
        r2 = r5.f832b;
        if (r2 != 0) goto L_0x002e;
    L_0x0035:
        goto L_0x002f;
        */
        throw new UnsupportedOperationException("Method not decompiled: com.zopim.android.sdk.chatlog.t.equals(java.lang.Object):boolean");
    }

    public int hashCode() {
        int i = 0;
        int hashCode = ((this.f831a != null ? this.f831a.hashCode() : 0) + (super.hashCode() * 31)) * 31;
        if (this.f832b != null) {
            i = this.f832b.hashCode();
        }
        return hashCode + i;
    }

    public String toString() {
        return "rating:" + this.f831a + " comment:" + this.f832b + super.toString();
    }
}
