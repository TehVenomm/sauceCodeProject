package im.getsocial.sdk.ui.activities.p116a.p117f;

import im.getsocial.sdk.ui.activities.p116a.p117f.jjbQypPegg.jjbQypPegg;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.LinkedHashSet;
import java.util.List;
import java.util.Map;
import java.util.Set;

/* renamed from: im.getsocial.sdk.ui.activities.a.f.upgqDBbsrL */
public abstract class upgqDBbsrL<K, T> implements jjbQypPegg<K, T> {
    /* renamed from: a */
    private final Object f2559a = new Object();
    /* renamed from: b */
    private final Set<jjbQypPegg<T>> f2560b = new LinkedHashSet();
    /* renamed from: c */
    private final Map<K, im.getsocial.sdk.ui.activities.p116a.p117f.jjbQypPegg.upgqDBbsrL<T>> f2561c = new HashMap();
    /* renamed from: d */
    private final List<T> f2562d = new ArrayList();

    /* renamed from: a */
    private void m2556a(T t, T t2) {
        im.getsocial.sdk.ui.activities.p116a.p117f.jjbQypPegg.upgqDBbsrL upgqdbbsrl = (im.getsocial.sdk.ui.activities.p116a.p117f.jjbQypPegg.upgqDBbsrL) this.f2561c.get(mo4590a((Object) t));
        if (upgqdbbsrl != null) {
            upgqdbbsrl.mo4654a(t, t2);
        }
    }

    /* renamed from: b */
    private void m2557b() {
        for (jjbQypPegg c : this.f2560b) {
            c.mo4648c(mo4582a());
        }
    }

    /* renamed from: a */
    protected abstract K mo4590a(T t);

    /* renamed from: a */
    public final List<T> mo4582a() {
        List arrayList;
        synchronized (this.f2559a) {
            arrayList = new ArrayList(this.f2562d);
        }
        return arrayList;
    }

    /* renamed from: a */
    public final void mo4583a(jjbQypPegg<T> jjbqyppegg) {
        this.f2560b.add(jjbqyppegg);
    }

    /* renamed from: a */
    public final void mo4584a(T t, im.getsocial.sdk.ui.activities.p116a.p117f.jjbQypPegg.upgqDBbsrL<T> upgqdbbsrl) {
        this.f2561c.put(mo4590a((Object) t), upgqdbbsrl);
    }

    /* renamed from: a */
    public final void mo4585a(List<T> list) {
        synchronized (this.f2559a) {
            this.f2562d.addAll(list);
            m2557b();
        }
    }

    /* renamed from: b */
    public final void mo4586b(T t) {
        synchronized (this.f2559a) {
            Object a = mo4590a((Object) t);
            int size = this.f2562d.size();
            for (int i = 0; i < size; i++) {
                Object obj = this.f2562d.get(i);
                if (mo4590a(obj).equals(a)) {
                    this.f2562d.set(i, t);
                    m2556a(obj, (Object) t);
                    m2557b();
                    break;
                }
            }
        }
    }

    /* renamed from: b */
    public final void mo4587b(List<T> list) {
        synchronized (this.f2559a) {
            this.f2562d.addAll(0, list);
            m2557b();
        }
    }

    /* renamed from: c */
    public final void mo4588c(T t) {
        synchronized (this.f2559a) {
            this.f2562d.remove(t);
            m2557b();
            m2556a((Object) t, null);
        }
    }

    /* renamed from: d */
    public final T mo4589d(K k) {
        int size = this.f2562d.size();
        for (int i = 0; i < size; i++) {
            Object obj = this.f2562d.get(i);
            if (mo4590a(obj).equals(k)) {
                return obj;
            }
        }
        return null;
    }
}
