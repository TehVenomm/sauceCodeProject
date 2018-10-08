package im.getsocial.sdk.internal.p086j.p088b;

import java.util.Map;

/* renamed from: im.getsocial.sdk.internal.j.b.cjrhisSQCL */
public class cjrhisSQCL {
    /* renamed from: b */
    private static String f1991b = "jpg";
    /* renamed from: c */
    private static String f1992c = "gif";
    /* renamed from: d */
    private static String f1993d = "mp4";
    /* renamed from: a */
    private final Map<String, String> f1994a;

    public cjrhisSQCL(Map<String, String> map) {
        this.f1994a = map;
    }

    /* renamed from: a */
    public final String m2004a() {
        return this.f1994a.size() == 1 ? (String) this.f1994a.values().toArray()[0] : (String) this.f1994a.get(f1991b);
    }

    /* renamed from: b */
    public final String m2005b() {
        return (String) this.f1994a.get(f1992c);
    }

    /* renamed from: c */
    public final String m2006c() {
        return (String) this.f1994a.get(f1993d);
    }
}
