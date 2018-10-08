package im.getsocial.sdk.internal.p033c;

/* renamed from: im.getsocial.sdk.internal.c.qZypgoeblR */
public class qZypgoeblR {
    /* renamed from: a */
    private final String f1497a;
    /* renamed from: b */
    private final String f1498b;

    public qZypgoeblR() {
        this("NO_NETWORK", null);
    }

    public qZypgoeblR(String str, String str2) {
        this.f1497a = str;
        this.f1498b = str2;
    }

    /* renamed from: a */
    public final String m1536a() {
        return this.f1497a;
    }

    /* renamed from: b */
    public final String m1537b() {
        return this.f1498b;
    }

    /* renamed from: c */
    public final String m1538c() {
        if (this.f1498b == null) {
            return this.f1497a;
        }
        return String.format("%s/%s", new Object[]{this.f1497a, this.f1498b});
    }

    /* renamed from: d */
    public final boolean m1539d() {
        return "WIFI".equalsIgnoreCase(this.f1497a);
    }

    /* renamed from: e */
    public final boolean m1540e() {
        return "LTE".equalsIgnoreCase(this.f1498b);
    }

    /* renamed from: f */
    public final boolean m1541f() {
        return "3G".equalsIgnoreCase(this.f1498b);
    }
}
