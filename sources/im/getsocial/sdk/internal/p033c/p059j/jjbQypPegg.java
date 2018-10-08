package im.getsocial.sdk.internal.p033c.p059j;

import im.getsocial.sdk.internal.p033c.QWVUXapsSm;
import im.getsocial.sdk.internal.p033c.p041b.KluUZYuxme;
import im.getsocial.sdk.internal.p033c.p041b.jMsobIMeui;
import im.getsocial.sdk.usermanagement.OnUserChangedListener;
import java.util.HashMap;
import java.util.Map;

/* renamed from: im.getsocial.sdk.internal.c.j.jjbQypPegg */
public class jjbQypPegg implements KluUZYuxme {
    /* renamed from: a */
    private final Object f1309a = new Object();
    /* renamed from: b */
    private final Map<jMsobIMeui, QWVUXapsSm> f1310b = new HashMap();
    /* renamed from: c */
    private Runnable f1311c;
    /* renamed from: d */
    private OnUserChangedListener f1312d;

    public jjbQypPegg() {
        m1306d();
    }

    /* renamed from: a */
    private void m1305a(jMsobIMeui jmsobimeui, QWVUXapsSm qWVUXapsSm) {
        synchronized (this.f1309a) {
            this.f1310b.put(jmsobimeui, qWVUXapsSm);
        }
    }

    /* renamed from: d */
    private void m1306d() {
        synchronized (this.f1309a) {
            for (Object put : jMsobIMeui.values()) {
                this.f1310b.put(put, QWVUXapsSm.UNINITIALIZED);
            }
        }
    }

    /* renamed from: a */
    public final QWVUXapsSm m1307a(jMsobIMeui jmsobimeui) {
        QWVUXapsSm qWVUXapsSm;
        synchronized (this.f1309a) {
            qWVUXapsSm = (QWVUXapsSm) this.f1310b.get(jmsobimeui);
        }
        return qWVUXapsSm;
    }

    /* renamed from: a */
    public final jMsobIMeui mo4351a() {
        jMsobIMeui jmsobimeui;
        synchronized (this.f1309a) {
            jmsobimeui = jMsobIMeui.APP;
        }
        return jmsobimeui;
    }

    /* renamed from: a */
    public final void m1309a(OnUserChangedListener onUserChangedListener) {
        this.f1312d = onUserChangedListener;
    }

    /* renamed from: a */
    public final void m1310a(Runnable runnable) {
        this.f1311c = runnable;
    }

    /* renamed from: b */
    public final Runnable m1311b() {
        return this.f1311c;
    }

    /* renamed from: b */
    public final void m1312b(jMsobIMeui jmsobimeui) {
        m1305a(jmsobimeui, QWVUXapsSm.INITIALIZING);
    }

    /* renamed from: c */
    public final OnUserChangedListener m1313c() {
        return this.f1312d;
    }

    /* renamed from: c */
    public final void m1314c(jMsobIMeui jmsobimeui) {
        m1305a(jmsobimeui, QWVUXapsSm.INITIALIZED);
    }

    /* renamed from: d */
    public final void m1315d(jMsobIMeui jmsobimeui) {
        m1305a(jmsobimeui, QWVUXapsSm.UNINITIALIZED);
    }
}
