package im.getsocial.sdk.internal.p033c.p041b;

import im.getsocial.sdk.internal.p033c.p041b.p050a.jjbQypPegg;
import im.getsocial.sdk.internal.p033c.p057g.cjrhisSQCL;
import im.getsocial.sdk.internal.p033c.p057g.upgqDBbsrL;
import java.io.Serializable;
import java.util.Comparator;
import java.util.EnumSet;
import java.util.HashMap;
import java.util.Map;
import java.util.Set;
import java.util.TreeSet;

/* renamed from: im.getsocial.sdk.internal.c.b.pdwpUtZXDT */
public class pdwpUtZXDT {
    /* renamed from: a */
    private static final cjrhisSQCL f1244a = upgqDBbsrL.m1274a(pdwpUtZXDT.class);
    /* renamed from: b */
    private static final Set<jjbQypPegg> f1245b;
    /* renamed from: c */
    private final Object f1246c = new Object();
    /* renamed from: d */
    private final Map<String, cjrhisSQCL> f1247d = new HashMap();
    /* renamed from: e */
    private final Map<String, Object> f1248e = new HashMap();
    /* renamed from: f */
    private final ruWsnwUPKh f1249f = new ruWsnwUPKh(EnumSet.allOf(jMsobIMeui.class));

    /* renamed from: im.getsocial.sdk.internal.c.b.pdwpUtZXDT$4 */
    class C09364 implements cjrhisSQCL {
        /* renamed from: a */
        final /* synthetic */ Class f1242a;
        /* renamed from: b */
        final /* synthetic */ pdwpUtZXDT f1243b;

        /* renamed from: a */
        public final Object mo4357a(pdwpUtZXDT pdwputzxdt) {
            return this.f1243b.m1203d(this.f1242a);
        }
    }

    /* renamed from: im.getsocial.sdk.internal.c.b.pdwpUtZXDT$jjbQypPegg */
    private static final class jjbQypPegg implements Serializable, Comparator<im.getsocial.sdk.internal.p033c.p041b.p050a.jjbQypPegg> {
        private jjbQypPegg() {
        }

        public final /* bridge */ /* synthetic */ int compare(Object obj, Object obj2) {
            boolean z = ((im.getsocial.sdk.internal.p033c.p041b.p050a.jjbQypPegg) obj) instanceof im.getsocial.sdk.internal.p033c.p041b.p050a.upgqDBbsrL;
            return z == (((im.getsocial.sdk.internal.p033c.p041b.p050a.jjbQypPegg) obj2) instanceof im.getsocial.sdk.internal.p033c.p041b.p050a.upgqDBbsrL) ? 0 : z ? -1 : 1;
        }
    }

    static {
        Set treeSet = new TreeSet(new jjbQypPegg());
        f1245b = treeSet;
        treeSet.add(new im.getsocial.sdk.internal.p033c.p041b.p050a.upgqDBbsrL());
    }

    public pdwpUtZXDT() {
        this.f1248e.put(pdwpUtZXDT.class.getName(), this);
    }

    /* renamed from: a */
    private <T> T m1198a(String str) {
        T t;
        synchronized (this.f1246c) {
            if (m1204d(str)) {
                t = this.f1248e.get(str);
            } else if (m1202c(str)) {
                t = ((cjrhisSQCL) this.f1247d.get(str)).mo4357a(this);
                this.f1248e.put(str, t);
            } else {
                throw new RuntimeException("Component is not registered for '" + str + "'");
            }
        }
        return t;
    }

    /* renamed from: a */
    private <T> void m1199a(String str, cjrhisSQCL<T> cjrhissqcl) {
        boolean z = true;
        synchronized (this.f1246c) {
            im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.cjrhisSQCL.m1511a(!m1202c(str), "Component already registered for '" + str + "'");
            if (m1204d(str)) {
                z = false;
            }
            im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.cjrhisSQCL.m1511a(z, "Component can't be registered after injecting the test component");
            this.f1247d.put(str, cjrhissqcl);
        }
    }

    /* renamed from: b */
    private static String m1200b(Class cls, String str) {
        return cls.getName() + "#" + str;
    }

    /* renamed from: b */
    private boolean m1201b(String str) {
        return (m1202c(str) || m1204d(str)) ? false : true;
    }

    /* renamed from: c */
    private boolean m1202c(String str) {
        return this.f1247d.containsKey(str);
    }

    /* renamed from: d */
    private <T> T m1203d(Class<? extends T> cls) {
        for (jjbQypPegg a : f1245b) {
            try {
                return a.mo4378a(cls, this);
            } catch (Throwable th) {
                if (!th.getClass().getSimpleName().equals("ReflectionStrippedError")) {
                    f1244a.mo4392b(th);
                    break;
                }
            }
        }
        throw new RuntimeException("Failed to instantiate instance of class " + cls);
    }

    /* renamed from: d */
    private boolean m1204d(String str) {
        return this.f1248e.containsKey(str);
    }

    /* renamed from: a */
    public final <T extends KluUZYuxme> T m1205a(Class<T> cls) {
        return this.f1249f.m1218a((Class) cls);
    }

    /* renamed from: a */
    public final Object m1206a(Class cls, KSZKMmRWhZ kSZKMmRWhZ) {
        return kSZKMmRWhZ == null ? KluUZYuxme.class.isAssignableFrom(cls) ? m1205a(cls) : m1213b(cls) : m1198a(pdwpUtZXDT.m1200b(cls, kSZKMmRWhZ.m1182a()));
    }

    /* renamed from: a */
    public final void m1207a(ruWsnwUPKh ruwsnwupkh) {
        this.f1249f.m1219a(ruwsnwupkh);
    }

    /* renamed from: a */
    public final <T> void m1208a(Class<T> cls, cjrhisSQCL<T> cjrhissqcl) {
        m1199a(cls.getName(), (cjrhisSQCL) cjrhissqcl);
    }

    /* renamed from: a */
    public final <T> void m1209a(Class<T> cls, final Class<? extends T> cls2) {
        m1208a((Class) cls, new cjrhisSQCL<T>(this) {
            /* renamed from: b */
            final /* synthetic */ pdwpUtZXDT f1237b;

            /* renamed from: a */
            public final T mo4357a(pdwpUtZXDT pdwputzxdt) {
                return pdwputzxdt.m1203d(cls2);
            }
        });
    }

    /* renamed from: a */
    public final <T> void m1210a(Class<T> cls, final T t) {
        m1208a((Class) cls, new cjrhisSQCL<T>(this) {
            /* renamed from: b */
            final /* synthetic */ pdwpUtZXDT f1239b;

            /* renamed from: a */
            public final T mo4357a(pdwpUtZXDT pdwputzxdt) {
                return t;
            }
        });
    }

    /* renamed from: a */
    public final <T> void m1211a(Class<T> cls, String str, cjrhisSQCL<? super T> cjrhissqcl) {
        m1199a(pdwpUtZXDT.m1200b((Class) cls, str), (cjrhisSQCL) cjrhissqcl);
    }

    /* renamed from: a */
    public final boolean m1212a(Class cls, String str) {
        return m1201b(pdwpUtZXDT.m1200b(cls, str));
    }

    /* renamed from: b */
    public final <T> T m1213b(Class<T> cls) {
        return m1198a(cls.getName());
    }

    /* renamed from: b */
    public final <T> void m1214b(Class<T> cls, final T t) {
        this.f1247d.put(cls.getName(), new cjrhisSQCL(this) {
            /* renamed from: b */
            final /* synthetic */ pdwpUtZXDT f1241b;

            /* renamed from: a */
            public final Object mo4357a(pdwpUtZXDT pdwputzxdt) {
                return t;
            }
        });
    }

    /* renamed from: c */
    public final boolean m1215c(Class cls) {
        return m1201b(cls.getName());
    }
}
