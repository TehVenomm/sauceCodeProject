package com.zopim.android.sdk.chatlog;

class aa<T extends aa> implements Comparable<aa> {
    /* renamed from: g */
    public String f743g;
    /* renamed from: h */
    public C0834a f744h = C0834a.UNKNOWN;
    /* renamed from: i */
    public String f745i;
    /* renamed from: j */
    public String f746j;
    /* renamed from: k */
    public String f747k;
    /* renamed from: l */
    public Long f748l;

    /* renamed from: com.zopim.android.sdk.chatlog.aa$a */
    enum C0834a {
        UNKNOWN(-1),
        VISITOR(0),
        AGENT(1),
        AGENT_TYPING(2),
        CHAT_EVENT(3),
        MEMBER_EVENT(4),
        ATTACHMENT_UPLOAD(5),
        CHAT_RATING(7);
        
        /* renamed from: i */
        final int f764i;

        private C0834a(int i) {
            this.f764i = i;
        }

        /* renamed from: a */
        static C0834a m671a(int i) {
            for (C0834a c0834a : C0834a.values()) {
                if (c0834a.m672a() == i) {
                    return c0834a;
                }
            }
            return UNKNOWN;
        }

        /* renamed from: a */
        int m672a() {
            return this.f764i;
        }
    }

    aa() {
    }

    aa(aa aaVar) {
        this.f743g = aaVar.f743g;
        this.f744h = aaVar.f744h;
        this.f745i = aaVar.f745i;
        this.f746j = aaVar.f746j;
        this.f747k = aaVar.f747k;
        this.f748l = aaVar.f748l;
    }

    /* renamed from: a */
    public void mo4242a(T t) {
        this.f743g = t.f743g;
        this.f744h = t.f744h;
        this.f745i = t.f745i;
        this.f746j = t.f746j;
        this.f747k = t.f747k;
        this.f748l = t.f748l;
    }

    /* renamed from: b */
    public int m668b(aa aaVar) {
        return this.f748l.compareTo(aaVar.f748l);
    }

    public /* synthetic */ int compareTo(Object obj) {
        return m668b((aa) obj);
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
        r5 = (com.zopim.android.sdk.chatlog.aa) r5;
        r2 = r4.f743g;
        if (r2 == 0) goto L_0x0055;
    L_0x0018:
        r2 = r4.f743g;
        r3 = r5.f743g;
        r2 = r2.equals(r3);
        if (r2 == 0) goto L_0x0005;
    L_0x0022:
        r2 = r4.f744h;
        r3 = r5.f744h;
        if (r2 != r3) goto L_0x0005;
    L_0x0028:
        r2 = r4.f745i;
        if (r2 == 0) goto L_0x005a;
    L_0x002c:
        r2 = r4.f745i;
        r3 = r5.f745i;
        r2 = r2.equals(r3);
        if (r2 == 0) goto L_0x0005;
    L_0x0036:
        r2 = r4.f746j;
        if (r2 == 0) goto L_0x005f;
    L_0x003a:
        r2 = r4.f746j;
        r3 = r5.f746j;
        r2 = r2.equals(r3);
        if (r2 == 0) goto L_0x0005;
    L_0x0044:
        r2 = r4.f747k;
        if (r2 == 0) goto L_0x0064;
    L_0x0048:
        r2 = r4.f747k;
        r3 = r5.f747k;
        r2 = r2.equals(r3);
        if (r2 != 0) goto L_0x0053;
    L_0x0052:
        r0 = r1;
    L_0x0053:
        r1 = r0;
        goto L_0x0005;
    L_0x0055:
        r2 = r5.f743g;
        if (r2 == 0) goto L_0x0022;
    L_0x0059:
        goto L_0x0005;
    L_0x005a:
        r2 = r5.f745i;
        if (r2 == 0) goto L_0x0036;
    L_0x005e:
        goto L_0x0005;
    L_0x005f:
        r2 = r5.f746j;
        if (r2 == 0) goto L_0x0044;
    L_0x0063:
        goto L_0x0005;
    L_0x0064:
        r2 = r5.f747k;
        if (r2 != 0) goto L_0x0052;
    L_0x0068:
        goto L_0x0053;
        */
        throw new UnsupportedOperationException("Method not decompiled: com.zopim.android.sdk.chatlog.aa.equals(java.lang.Object):boolean");
    }

    public int hashCode() {
        int i = 0;
        int hashCode = ((this.f746j != null ? this.f746j.hashCode() : 0) + (((this.f745i != null ? this.f745i.hashCode() : 0) + (((this.f744h != null ? this.f744h.hashCode() : 0) + ((this.f743g != null ? this.f743g.hashCode() : 0) * 31)) * 31)) * 31)) * 31;
        if (this.f747k != null) {
            i = this.f747k.hashCode();
        }
        return hashCode + i;
    }

    public String toString() {
        return " type:" + this.f744h + " dispName:" + this.f746j + " nick:" + this.f747k + " id:" + this.f743g + " ts:" + this.f748l;
    }
}
