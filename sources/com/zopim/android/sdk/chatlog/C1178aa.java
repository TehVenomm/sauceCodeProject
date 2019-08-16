package com.zopim.android.sdk.chatlog;

import com.zopim.android.sdk.chatlog.C1178aa;

/* renamed from: com.zopim.android.sdk.chatlog.aa */
class C1178aa<T extends C1178aa> implements Comparable<C1178aa> {

    /* renamed from: g */
    public String f793g;

    /* renamed from: h */
    public C1179a f794h = C1179a.UNKNOWN;

    /* renamed from: i */
    public String f795i;

    /* renamed from: j */
    public String f796j;

    /* renamed from: k */
    public String f797k;

    /* renamed from: l */
    public Long f798l;

    /* renamed from: com.zopim.android.sdk.chatlog.aa$a */
    enum C1179a {
        UNKNOWN(-1),
        VISITOR(0),
        AGENT(1),
        AGENT_TYPING(2),
        CHAT_EVENT(3),
        MEMBER_EVENT(4),
        ATTACHMENT_UPLOAD(5),
        CHAT_RATING(7);
        

        /* renamed from: i */
        final int f808i;

        private C1179a(int i) {
            this.f808i = i;
        }

        /* renamed from: a */
        static C1179a m684a(int i) {
            C1179a[] values;
            for (C1179a aVar : values()) {
                if (aVar.mo20726a() == i) {
                    return aVar;
                }
            }
            return UNKNOWN;
        }

        /* access modifiers changed from: 0000 */
        /* renamed from: a */
        public int mo20726a() {
            return this.f808i;
        }
    }

    C1178aa() {
    }

    C1178aa(C1178aa aaVar) {
        this.f793g = aaVar.f793g;
        this.f794h = aaVar.f794h;
        this.f795i = aaVar.f795i;
        this.f796j = aaVar.f796j;
        this.f797k = aaVar.f797k;
        this.f798l = aaVar.f798l;
    }

    /* renamed from: a */
    public void mo20720a(T t) {
        this.f793g = t.f793g;
        this.f794h = t.f794h;
        this.f795i = t.f795i;
        this.f796j = t.f796j;
        this.f797k = t.f797k;
        this.f798l = t.f798l;
    }

    /* renamed from: b */
    public int compareTo(C1178aa aaVar) {
        return this.f798l.compareTo(aaVar.f798l);
    }

    public boolean equals(Object obj) {
        boolean z = true;
        if (this == obj) {
            return true;
        }
        if (obj == null || getClass() != obj.getClass()) {
            return false;
        }
        C1178aa aaVar = (C1178aa) obj;
        if (this.f793g != null) {
            if (!this.f793g.equals(aaVar.f793g)) {
                return false;
            }
        } else if (aaVar.f793g != null) {
            return false;
        }
        if (this.f794h != aaVar.f794h) {
            return false;
        }
        if (this.f795i != null) {
            if (!this.f795i.equals(aaVar.f795i)) {
                return false;
            }
        } else if (aaVar.f795i != null) {
            return false;
        }
        if (this.f796j != null) {
            if (!this.f796j.equals(aaVar.f796j)) {
                return false;
            }
        } else if (aaVar.f796j != null) {
            return false;
        }
        if (this.f797k == null ? aaVar.f797k != null : !this.f797k.equals(aaVar.f797k)) {
            z = false;
        }
        return z;
    }

    public int hashCode() {
        int i = 0;
        int hashCode = ((this.f796j != null ? this.f796j.hashCode() : 0) + (((this.f795i != null ? this.f795i.hashCode() : 0) + (((this.f794h != null ? this.f794h.hashCode() : 0) + ((this.f793g != null ? this.f793g.hashCode() : 0) * 31)) * 31)) * 31)) * 31;
        if (this.f797k != null) {
            i = this.f797k.hashCode();
        }
        return hashCode + i;
    }

    public String toString() {
        return " type:" + this.f794h + " dispName:" + this.f796j + " nick:" + this.f797k + " id:" + this.f793g + " ts:" + this.f798l;
    }
}
