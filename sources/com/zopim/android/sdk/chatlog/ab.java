package com.zopim.android.sdk.chatlog;

import java.io.File;
import java.net.URL;

final class ab extends aa<ab> {
    /* renamed from: a */
    public File f765a;
    /* renamed from: b */
    public URL f766b;
    /* renamed from: c */
    public int f767c;
    /* renamed from: d */
    public String f768d;
    /* renamed from: e */
    public boolean f769e;

    ab(aa aaVar) {
        super(aaVar);
    }

    /* renamed from: a */
    public void m674a(ab abVar) {
        super.mo4244a(abVar);
        this.f765a = abVar.f765a;
        this.f766b = abVar.f766b;
        this.f767c = abVar.f767c;
        this.f768d = abVar.f768d;
        this.f769e = abVar.f769e;
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
        r2 = r5 instanceof com.zopim.android.sdk.chatlog.ab;
        if (r2 == 0) goto L_0x0005;
    L_0x000a:
        r2 = super.equals(r5);
        if (r2 == 0) goto L_0x0005;
    L_0x0010:
        r5 = (com.zopim.android.sdk.chatlog.ab) r5;
        r2 = r4.f767c;
        r3 = r5.f767c;
        if (r2 != r3) goto L_0x0005;
    L_0x0018:
        r2 = r4.f769e;
        r3 = r5.f769e;
        if (r2 != r3) goto L_0x0005;
    L_0x001e:
        r2 = r4.f765a;
        if (r2 == 0) goto L_0x004b;
    L_0x0022:
        r2 = r4.f765a;
        r3 = r5.f765a;
        r2 = r2.equals(r3);
        if (r2 == 0) goto L_0x0005;
    L_0x002c:
        r2 = r4.f766b;
        if (r2 == 0) goto L_0x0050;
    L_0x0030:
        r2 = r4.f766b;
        r3 = r5.f766b;
        r2 = r2.equals(r3);
        if (r2 == 0) goto L_0x0005;
    L_0x003a:
        r2 = r4.f768d;
        if (r2 == 0) goto L_0x0055;
    L_0x003e:
        r2 = r4.f768d;
        r3 = r5.f768d;
        r2 = r2.equals(r3);
        if (r2 != 0) goto L_0x0049;
    L_0x0048:
        r0 = r1;
    L_0x0049:
        r1 = r0;
        goto L_0x0005;
    L_0x004b:
        r2 = r5.f765a;
        if (r2 == 0) goto L_0x002c;
    L_0x004f:
        goto L_0x0005;
    L_0x0050:
        r2 = r5.f766b;
        if (r2 == 0) goto L_0x003a;
    L_0x0054:
        goto L_0x0005;
    L_0x0055:
        r2 = r5.f768d;
        if (r2 != 0) goto L_0x0048;
    L_0x0059:
        goto L_0x0049;
        */
        throw new UnsupportedOperationException("Method not decompiled: com.zopim.android.sdk.chatlog.ab.equals(java.lang.Object):boolean");
    }

    public int hashCode() {
        int i = 0;
        int hashCode = ((this.f768d != null ? this.f768d.hashCode() : 0) + (((((this.f766b != null ? this.f766b.hashCode() : 0) + (((this.f765a != null ? this.f765a.hashCode() : 0) + (super.hashCode() * 31)) * 31)) * 31) + this.f767c) * 31)) * 31;
        if (this.f769e) {
            i = 1;
        }
        return hashCode + i;
    }

    public String toString() {
        return "file:" + this.f765a + " uploadUrl:" + this.f766b + " progress:" + this.f767c + " failed:" + this.f769e + super.toString();
    }
}
