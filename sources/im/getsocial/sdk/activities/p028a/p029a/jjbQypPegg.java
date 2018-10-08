package im.getsocial.sdk.activities.p028a.p029a;

import im.getsocial.sdk.activities.ActivityPost.Type;

/* renamed from: im.getsocial.sdk.activities.a.a.jjbQypPegg */
public final class jjbQypPegg {
    /* renamed from: a */
    private final Type f1152a;
    /* renamed from: b */
    private final String f1153b;
    /* renamed from: c */
    private final String f1154c;
    /* renamed from: d */
    private String f1155d;
    /* renamed from: e */
    private String f1156e;
    /* renamed from: f */
    private String f1157f;
    /* renamed from: g */
    private String f1158g;
    /* renamed from: h */
    private String f1159h;
    /* renamed from: i */
    private String f1160i;
    /* renamed from: j */
    private im.getsocial.sdk.internal.p086j.p088b.jjbQypPegg f1161j;

    private jjbQypPegg(Type type, String str, String str2) {
        this.f1152a = type;
        this.f1154c = str;
        this.f1153b = str2;
    }

    /* renamed from: a */
    public static jjbQypPegg m952a(String str) {
        return new jjbQypPegg(Type.POST, str, null);
    }

    /* renamed from: b */
    public static jjbQypPegg m953b(String str) {
        return new jjbQypPegg(Type.COMMENT, null, str);
    }

    /* renamed from: a */
    public final Type m954a() {
        return this.f1152a;
    }

    /* renamed from: a */
    public final jjbQypPegg m955a(im.getsocial.sdk.internal.p086j.p088b.jjbQypPegg jjbqyppegg) {
        this.f1161j = jjbqyppegg;
        return this;
    }

    /* renamed from: a */
    public final jjbQypPegg m956a(String str, String str2) {
        this.f1158g = str;
        this.f1159h = str2;
        return this;
    }

    /* renamed from: b */
    public final String m957b() {
        return this.f1153b;
    }

    /* renamed from: c */
    public final jjbQypPegg m958c(String str) {
        this.f1155d = str;
        return this;
    }

    /* renamed from: c */
    public final String m959c() {
        return this.f1154c;
    }

    /* renamed from: d */
    public final jjbQypPegg m960d(String str) {
        this.f1156e = str;
        return this;
    }

    /* renamed from: d */
    public final String m961d() {
        return this.f1155d;
    }

    /* renamed from: e */
    public final jjbQypPegg m962e(String str) {
        this.f1157f = str;
        return this;
    }

    /* renamed from: e */
    public final String m963e() {
        return this.f1156e;
    }

    /* renamed from: f */
    public final jjbQypPegg m964f(String str) {
        this.f1160i = str;
        return this;
    }

    /* renamed from: f */
    public final String m965f() {
        return this.f1157f;
    }

    /* renamed from: g */
    public final String m966g() {
        return this.f1160i;
    }

    /* renamed from: h */
    public final im.getsocial.sdk.internal.p086j.p088b.jjbQypPegg m967h() {
        return this.f1161j;
    }

    /* renamed from: i */
    public final String m968i() {
        return this.f1158g;
    }

    /* renamed from: j */
    public final String m969j() {
        return this.f1159h;
    }
}
