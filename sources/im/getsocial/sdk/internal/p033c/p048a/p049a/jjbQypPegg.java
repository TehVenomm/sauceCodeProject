package im.getsocial.sdk.internal.p033c.p048a.p049a;

import im.getsocial.sdk.GetSocialException;
import java.util.List;
import java.util.Map;

/* renamed from: im.getsocial.sdk.internal.c.a.a.jjbQypPegg */
public class jjbQypPegg extends GetSocialException {
    /* renamed from: a */
    private final List<jjbQypPegg> f1231a;

    /* renamed from: im.getsocial.sdk.internal.c.a.a.jjbQypPegg$jjbQypPegg */
    public static class jjbQypPegg {
        /* renamed from: a */
        private final int f1228a;
        /* renamed from: b */
        private final String f1229b;
        /* renamed from: c */
        private final Map<String, String> f1230c;

        public jjbQypPegg(int i, String str, Map<String, String> map) {
            this.f1228a = i;
            this.f1229b = str;
            this.f1230c = map;
        }

        /* renamed from: a */
        public final String m1133a() {
            return this.f1229b;
        }

        /* renamed from: b */
        public final int m1134b() {
            return this.f1228a;
        }

        public String toString() {
            return "ApiError{_errorCode=" + this.f1228a + ", _message='" + this.f1229b + '\'' + ", _context=" + this.f1230c + '}';
        }
    }

    public jjbQypPegg(int i, String str, List<jjbQypPegg> list) {
        super(i, str);
        this.f1231a = list;
    }

    /* renamed from: a */
    public final boolean m1135a(int i) {
        for (jjbQypPegg b : this.f1231a) {
            if (b.m1134b() == i) {
                return true;
            }
        }
        return false;
    }

    public boolean equals(Object obj) {
        if (this != obj) {
            if (obj == null || getClass() != obj.getClass() || !super.equals(obj)) {
                return false;
            }
            jjbQypPegg jjbqyppegg = (jjbQypPegg) obj;
            if (this.f1231a != null) {
                return this.f1231a.equals(jjbqyppegg.f1231a);
            }
            if (jjbqyppegg.f1231a != null) {
                return false;
            }
        }
        return true;
    }

    public int hashCode() {
        return (this.f1231a == null ? 0 : this.f1231a.hashCode()) + (super.hashCode() * 31);
    }

    public String toString() {
        StringBuilder append = new StringBuilder("GetSocialApiException{ _errorCode={ ").append(getErrorCode()).append(" },_errorMessage={ ").append(getMessage()).append(" },_apiErrors={ ");
        for (jjbQypPegg append2 : this.f1231a) {
            append.append(append2);
        }
        append.append(" }}");
        return append.toString();
    }
}
