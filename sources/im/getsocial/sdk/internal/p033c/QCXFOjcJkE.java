package im.getsocial.sdk.internal.p033c;

import java.util.HashMap;
import java.util.Map;

/* renamed from: im.getsocial.sdk.internal.c.QCXFOjcJkE */
public class QCXFOjcJkE {
    /* renamed from: a */
    private final String f1224a;
    /* renamed from: b */
    private final long f1225b;
    /* renamed from: c */
    private final Map<String, Long> f1226c;

    public QCXFOjcJkE(String str, Long l) {
        this.f1224a = str;
        this.f1225b = l == null ? 10485760 : l.longValue();
        this.f1226c = new HashMap();
    }

    /* renamed from: a */
    public final int m1104a(String str) {
        Long l = (Long) this.f1226c.get(str);
        return l == null ? 512000 : l.intValue();
    }

    /* renamed from: a */
    public final String m1105a() {
        return this.f1224a;
    }

    /* renamed from: a */
    public final void m1106a(String str, Long l) {
        this.f1226c.put(str, l);
    }

    /* renamed from: b */
    public final long m1107b() {
        return this.f1225b;
    }
}
