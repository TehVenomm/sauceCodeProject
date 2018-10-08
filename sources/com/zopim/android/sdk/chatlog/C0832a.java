package com.zopim.android.sdk.chatlog;

import java.io.File;
import java.net.URL;
import java.util.Arrays;

/* renamed from: com.zopim.android.sdk.chatlog.a */
final class C0832a extends aa<C0832a> {
    /* renamed from: a */
    public URL f749a;
    /* renamed from: b */
    public Long f750b;
    /* renamed from: c */
    public String f751c;
    /* renamed from: d */
    public File f752d;
    /* renamed from: e */
    public String f753e;
    /* renamed from: f */
    public String[] f754f = new String[0];

    C0832a(aa aaVar) {
        super(aaVar);
    }

    /* renamed from: a */
    public void m669a(C0832a c0832a) {
        super.mo4244a(c0832a);
        this.f749a = c0832a.f749a;
        this.f750b = c0832a.f750b;
        this.f751c = c0832a.f751c;
        this.f753e = c0832a.f753e;
        this.f754f = c0832a.f754f;
    }

    public boolean equals(Object obj) {
        if (this == obj) {
            return true;
        }
        if (obj == null || getClass() != obj.getClass() || !super.equals(obj)) {
            return false;
        }
        C0832a c0832a = (C0832a) obj;
        if (this.f749a != null) {
            if (!this.f749a.equals(c0832a.f749a)) {
                return false;
            }
        } else if (c0832a.f749a != null) {
            return false;
        }
        if (this.f750b != null) {
            if (!this.f750b.equals(c0832a.f750b)) {
                return false;
            }
        } else if (c0832a.f750b != null) {
            return false;
        }
        if (this.f751c != null) {
            if (!this.f751c.equals(c0832a.f751c)) {
                return false;
            }
        } else if (c0832a.f751c != null) {
            return false;
        }
        if (this.f752d != null) {
            if (!this.f752d.equals(c0832a.f752d)) {
                return false;
            }
        } else if (c0832a.f752d != null) {
            return false;
        }
        if (this.f753e != null) {
            if (!this.f753e.equals(c0832a.f753e)) {
                return false;
            }
        } else if (c0832a.f753e != null) {
            return false;
        }
        return Arrays.equals(this.f754f, c0832a.f754f);
    }

    public int hashCode() {
        int i = 0;
        int hashCode = ((this.f753e != null ? this.f753e.hashCode() : 0) + (((this.f752d != null ? this.f752d.hashCode() : 0) + (((this.f751c != null ? this.f751c.hashCode() : 0) + (((this.f750b != null ? this.f750b.hashCode() : 0) + (((this.f749a != null ? this.f749a.hashCode() : 0) + (super.hashCode() * 31)) * 31)) * 31)) * 31)) * 31)) * 31;
        if (this.f754f != null) {
            i = Arrays.hashCode(this.f754f);
        }
        return hashCode + i;
    }

    public String toString() {
        return "options:" + this.f754f + " attachFile:" + this.f752d + " attachUrl:" + this.f749a + " attachName:" + this.f751c + " attachSize:" + this.f750b + super.toString();
    }
}
