package com.zopim.android.sdk.chatlog;

/* renamed from: com.zopim.android.sdk.chatlog.g */
final class C1209g extends C1178aa<C1209g> {

    /* renamed from: a */
    public String f854a;

    /* renamed from: b */
    public boolean f855b;

    /* renamed from: a */
    public void mo20720a(C1209g gVar) {
        super.mo20720a(gVar);
        this.f854a = gVar.f854a;
        this.f855b = gVar.f855b;
    }

    public boolean equals(Object obj) {
        boolean z = true;
        if (this == obj) {
            return true;
        }
        if (obj == null || getClass() != obj.getClass() || !super.equals(obj)) {
            return false;
        }
        C1209g gVar = (C1209g) obj;
        if (this.f855b != gVar.f855b) {
            return false;
        }
        if (this.f854a == null ? gVar.f854a != null : !this.f854a.equals(gVar.f854a)) {
            z = false;
        }
        return z;
    }

    public int hashCode() {
        int i = 0;
        int hashCode = ((this.f854a != null ? this.f854a.hashCode() : 0) + (super.hashCode() * 31)) * 31;
        if (this.f855b) {
            i = 1;
        }
        return hashCode + i;
    }

    public String toString() {
        return " avatarUri:" + this.f854a + " typing:" + this.f855b + super.toString();
    }
}
