package com.zopim.android.sdk.chatlog;

/* renamed from: com.zopim.android.sdk.chatlog.g */
final class C0840g extends aa<C0840g> {
    /* renamed from: a */
    public String f810a;
    /* renamed from: b */
    public boolean f811b;

    /* renamed from: a */
    public void m684a(C0840g c0840g) {
        super.mo4242a(c0840g);
        this.f810a = c0840g.f810a;
        this.f811b = c0840g.f811b;
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
        r5 = (com.zopim.android.sdk.chatlog.C0840g) r5;
        r2 = r4.f811b;
        r3 = r5.f811b;
        if (r2 != r3) goto L_0x0005;
    L_0x0020:
        r2 = r4.f810a;
        if (r2 == 0) goto L_0x0031;
    L_0x0024:
        r2 = r4.f810a;
        r3 = r5.f810a;
        r2 = r2.equals(r3);
        if (r2 != 0) goto L_0x002f;
    L_0x002e:
        r0 = r1;
    L_0x002f:
        r1 = r0;
        goto L_0x0005;
    L_0x0031:
        r2 = r5.f810a;
        if (r2 != 0) goto L_0x002e;
    L_0x0035:
        goto L_0x002f;
        */
        throw new UnsupportedOperationException("Method not decompiled: com.zopim.android.sdk.chatlog.g.equals(java.lang.Object):boolean");
    }

    public int hashCode() {
        int i = 0;
        int hashCode = ((this.f810a != null ? this.f810a.hashCode() : 0) + (super.hashCode() * 31)) * 31;
        if (this.f811b) {
            i = 1;
        }
        return hashCode + i;
    }

    public String toString() {
        return " avatarUri:" + this.f810a + " typing:" + this.f811b + super.toString();
    }
}
