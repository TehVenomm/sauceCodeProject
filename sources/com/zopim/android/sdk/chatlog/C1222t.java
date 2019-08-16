package com.zopim.android.sdk.chatlog;

import com.zopim.android.sdk.model.ChatLog.Rating;

/* renamed from: com.zopim.android.sdk.chatlog.t */
final class C1222t extends C1178aa<C1222t> {

    /* renamed from: a */
    public Rating f875a = Rating.UNKNOWN;

    /* renamed from: b */
    public String f876b;

    C1222t(C1178aa aaVar) {
        super(aaVar);
    }

    /* renamed from: a */
    public void mo20720a(C1222t tVar) {
        super.mo20720a(tVar);
        this.f875a = tVar.f875a;
        this.f876b = tVar.f876b;
    }

    public boolean equals(Object obj) {
        boolean z = true;
        if (this == obj) {
            return true;
        }
        if (obj == null || getClass() != obj.getClass() || !super.equals(obj)) {
            return false;
        }
        C1222t tVar = (C1222t) obj;
        if (this.f875a != tVar.f875a) {
            return false;
        }
        if (this.f876b == null ? tVar.f876b != null : !this.f876b.equals(tVar.f876b)) {
            z = false;
        }
        return z;
    }

    public int hashCode() {
        int i = 0;
        int hashCode = ((this.f875a != null ? this.f875a.hashCode() : 0) + (super.hashCode() * 31)) * 31;
        if (this.f876b != null) {
            i = this.f876b.hashCode();
        }
        return hashCode + i;
    }

    public String toString() {
        return "rating:" + this.f875a + " comment:" + this.f876b + super.toString();
    }
}
