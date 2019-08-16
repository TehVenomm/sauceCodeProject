package com.zopim.android.sdk.chatlog;

import java.io.File;
import java.net.URL;
import java.util.Arrays;

/* renamed from: com.zopim.android.sdk.chatlog.a */
final class C1177a extends C1178aa<C1177a> {

    /* renamed from: a */
    public URL f787a;

    /* renamed from: b */
    public Long f788b;

    /* renamed from: c */
    public String f789c;

    /* renamed from: d */
    public File f790d;

    /* renamed from: e */
    public String f791e;

    /* renamed from: f */
    public String[] f792f = new String[0];

    C1177a(C1178aa aaVar) {
        super(aaVar);
    }

    /* renamed from: a */
    public void mo20720a(C1177a aVar) {
        super.mo20720a(aVar);
        this.f787a = aVar.f787a;
        this.f788b = aVar.f788b;
        this.f789c = aVar.f789c;
        this.f791e = aVar.f791e;
        this.f792f = aVar.f792f;
    }

    public boolean equals(Object obj) {
        if (this == obj) {
            return true;
        }
        if (obj == null || getClass() != obj.getClass() || !super.equals(obj)) {
            return false;
        }
        C1177a aVar = (C1177a) obj;
        if (this.f787a != null) {
            if (!this.f787a.equals(aVar.f787a)) {
                return false;
            }
        } else if (aVar.f787a != null) {
            return false;
        }
        if (this.f788b != null) {
            if (!this.f788b.equals(aVar.f788b)) {
                return false;
            }
        } else if (aVar.f788b != null) {
            return false;
        }
        if (this.f789c != null) {
            if (!this.f789c.equals(aVar.f789c)) {
                return false;
            }
        } else if (aVar.f789c != null) {
            return false;
        }
        if (this.f790d != null) {
            if (!this.f790d.equals(aVar.f790d)) {
                return false;
            }
        } else if (aVar.f790d != null) {
            return false;
        }
        if (this.f791e != null) {
            if (!this.f791e.equals(aVar.f791e)) {
                return false;
            }
        } else if (aVar.f791e != null) {
            return false;
        }
        return Arrays.equals(this.f792f, aVar.f792f);
    }

    public int hashCode() {
        int i = 0;
        int hashCode = ((this.f791e != null ? this.f791e.hashCode() : 0) + (((this.f790d != null ? this.f790d.hashCode() : 0) + (((this.f789c != null ? this.f789c.hashCode() : 0) + (((this.f788b != null ? this.f788b.hashCode() : 0) + (((this.f787a != null ? this.f787a.hashCode() : 0) + (super.hashCode() * 31)) * 31)) * 31)) * 31)) * 31)) * 31;
        if (this.f792f != null) {
            i = Arrays.hashCode(this.f792f);
        }
        return hashCode + i;
    }

    public String toString() {
        return "options:" + this.f792f + " attachFile:" + this.f790d + " attachUrl:" + this.f787a + " attachName:" + this.f789c + " attachSize:" + this.f788b + super.toString();
    }
}
