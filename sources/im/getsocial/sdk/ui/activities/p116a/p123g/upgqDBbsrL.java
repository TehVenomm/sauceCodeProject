package im.getsocial.sdk.ui.activities.p116a.p123g;

import im.getsocial.sdk.ui.internal.p125h.qZypgoeblR;
import java.util.ArrayList;
import java.util.Collection;
import java.util.HashSet;
import java.util.List;
import java.util.Set;

/* renamed from: im.getsocial.sdk.ui.activities.a.g.upgqDBbsrL */
public abstract class upgqDBbsrL<T> {
    /* renamed from: a */
    private final Set<T> f2733a = new HashSet();
    /* renamed from: b */
    private String f2734b;
    /* renamed from: c */
    private qZypgoeblR f2735c;

    protected upgqDBbsrL() {
    }

    /* renamed from: a */
    protected abstract qZypgoeblR mo4711a(String str);

    /* renamed from: a */
    protected final void m3042a(T t) {
        if (!this.f2733a.add(t)) {
            this.f2733a.remove(t);
            this.f2733a.add(t);
        }
    }

    /* renamed from: a */
    protected final void m3043a(Collection<T> collection) {
        this.f2733a.addAll(collection);
    }

    /* renamed from: a */
    protected abstract void mo4712a(List<T> list);

    /* renamed from: a */
    protected abstract boolean mo4713a(T t, String str);

    /* renamed from: b */
    protected final void m3046b(String str) {
        this.f2734b = str;
    }

    /* renamed from: c */
    public final void m3047c(String str) {
        if (this.f2735c != null) {
            this.f2735c.mo4710a();
            this.f2735c = null;
        }
        List d = m3048d(str);
        if (d.isEmpty()) {
            Object obj = (this.f2734b == null || !str.startsWith(this.f2734b)) ? null : 1;
            if (obj == null) {
                this.f2735c = mo4711a(str);
                this.f2735c.m3040a(500);
                return;
            }
        }
        mo4712a(d);
    }

    /* renamed from: d */
    protected final List<T> m3048d(String str) {
        if ("".equals(str)) {
            return new ArrayList(this.f2733a);
        }
        List<T> arrayList = new ArrayList();
        for (Object next : this.f2733a) {
            if (mo4713a(next, str)) {
                arrayList.add(next);
            }
        }
        return arrayList;
    }
}
