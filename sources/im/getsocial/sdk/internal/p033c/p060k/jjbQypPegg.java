package im.getsocial.sdk.internal.p033c.p060k;

/* renamed from: im.getsocial.sdk.internal.c.k.jjbQypPegg */
public class jjbQypPegg {
    /* renamed from: a */
    private final String f1354a;
    /* renamed from: b */
    private final boolean f1355b;

    public jjbQypPegg(String str, boolean z) {
        this.f1354a = str;
        this.f1355b = z;
    }

    /* renamed from: a */
    public final String m1379a() {
        return this.f1354a;
    }

    /* renamed from: b */
    public final boolean m1380b() {
        return this.f1355b;
    }

    public String toString() {
        return (this.f1355b ? "https://" : "http://") + this.f1354a;
    }
}
