package com.zopim.android.sdk.chatlog;

import java.io.File;
import java.net.URL;

/* renamed from: com.zopim.android.sdk.chatlog.ab */
final class C1180ab extends C1178aa<C1180ab> {

    /* renamed from: a */
    public File f809a;

    /* renamed from: b */
    public URL f810b;

    /* renamed from: c */
    public int f811c;

    /* renamed from: d */
    public String f812d;

    /* renamed from: e */
    public boolean f813e;

    C1180ab(C1178aa aaVar) {
        super(aaVar);
    }

    /* renamed from: a */
    public void mo20720a(C1180ab abVar) {
        super.mo20720a(abVar);
        this.f809a = abVar.f809a;
        this.f810b = abVar.f810b;
        this.f811c = abVar.f811c;
        this.f812d = abVar.f812d;
        this.f813e = abVar.f813e;
    }

    public boolean equals(Object obj) {
        boolean z = true;
        if (this == obj) {
            return true;
        }
        if (!(obj instanceof C1180ab) || !super.equals(obj)) {
            return false;
        }
        C1180ab abVar = (C1180ab) obj;
        if (this.f811c != abVar.f811c || this.f813e != abVar.f813e) {
            return false;
        }
        if (this.f809a != null) {
            if (!this.f809a.equals(abVar.f809a)) {
                return false;
            }
        } else if (abVar.f809a != null) {
            return false;
        }
        if (this.f810b != null) {
            if (!this.f810b.equals(abVar.f810b)) {
                return false;
            }
        } else if (abVar.f810b != null) {
            return false;
        }
        if (this.f812d == null ? abVar.f812d != null : !this.f812d.equals(abVar.f812d)) {
            z = false;
        }
        return z;
    }

    public int hashCode() {
        int i = 0;
        int hashCode = ((this.f812d != null ? this.f812d.hashCode() : 0) + (((((this.f810b != null ? this.f810b.hashCode() : 0) + (((this.f809a != null ? this.f809a.hashCode() : 0) + (super.hashCode() * 31)) * 31)) * 31) + this.f811c) * 31)) * 31;
        if (this.f813e) {
            i = 1;
        }
        return hashCode + i;
    }

    public String toString() {
        return "file:" + this.f809a + " uploadUrl:" + this.f810b + " progress:" + this.f811c + " failed:" + this.f813e + super.toString();
    }
}
