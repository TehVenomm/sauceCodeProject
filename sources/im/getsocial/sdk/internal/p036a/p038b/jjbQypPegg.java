package im.getsocial.sdk.internal.p036a.p038b;

import java.io.Serializable;
import java.util.HashMap;
import java.util.Map;

/* renamed from: im.getsocial.sdk.internal.a.b.jjbQypPegg */
public class jjbQypPegg implements Serializable {
    /* renamed from: a */
    private final String f1180a;
    /* renamed from: b */
    private final Map<String, String> f1181b;
    /* renamed from: c */
    private final long f1182c;
    /* renamed from: d */
    private final String f1183d;
    /* renamed from: e */
    private long f1184e;

    private jjbQypPegg(String str, Map<String, String> map, long j, String str2, long j2) {
        Map hashMap;
        im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.jjbQypPegg.m1512a(im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.m1514a((Object) str), "Can not create AnalyticsEvent with null name");
        this.f1180a = str;
        if (map == null) {
            hashMap = new HashMap();
        }
        this.f1181b = hashMap;
        this.f1182c = j;
        this.f1183d = str2;
        this.f1184e = j2;
    }

    /* renamed from: a */
    public static jjbQypPegg m1015a(String str, Map<String, String> map, long j, String str2) {
        return new jjbQypPegg(str, map, j, str2, 0);
    }

    /* renamed from: a */
    public static jjbQypPegg m1016a(String str, Map<String, String> map, long j, String str2, long j2) {
        return new jjbQypPegg(str, map, j, str2, j2);
    }

    /* renamed from: a */
    public final String m1017a() {
        return this.f1180a;
    }

    /* renamed from: b */
    public final Map<String, String> m1018b() {
        return this.f1181b;
    }

    /* renamed from: c */
    public final long m1019c() {
        return this.f1182c;
    }

    /* renamed from: d */
    public final String m1020d() {
        return this.f1183d;
    }

    /* renamed from: e */
    public final long m1021e() {
        return this.f1184e;
    }

    /* renamed from: f */
    public final void m1022f() {
        this.f1184e++;
    }

    public String toString() {
        return "AnalyticsEvent{_name='" + this.f1180a + '\'' + ", _properties=" + this.f1181b + ", _timestamp=" + this.f1182c + ", _uniqueId=" + this.f1183d + ", _retryCount=" + this.f1184e + '}';
    }
}
