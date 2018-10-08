package im.getsocial.sdk.usermanagement;

import im.getsocial.sdk.internal.p033c.p066m.upgqDBbsrL;
import java.util.Collections;
import java.util.Map;

public class PublicUser {
    /* renamed from: b */
    protected String f1141b = null;
    /* renamed from: c */
    protected String f1142c = null;
    /* renamed from: d */
    protected String f1143d = null;
    /* renamed from: e */
    protected Map<String, String> f1144e = null;
    /* renamed from: f */
    protected Map<String, String> f1145f = null;
    /* renamed from: g */
    Map<String, String> f1146g = null;
    /* renamed from: h */
    boolean f1147h;

    public static class Builder {
        /* renamed from: a */
        PublicUser f3256a = new PublicUser();

        public Builder(String str) {
            this.f3256a.f1141b = str;
        }

        public PublicUser build() {
            PublicUser publicUser = new PublicUser();
            this.f3256a.m944a(publicUser);
            return publicUser;
        }

        public Builder setAvatarUrl(String str) {
            this.f3256a.f1143d = str;
            return this;
        }

        public Builder setDisplayName(String str) {
            this.f3256a.f1142c = str;
            return this;
        }

        public Builder setIdentities(Map<String, String> map) {
            this.f3256a.f1144e = map;
            return this;
        }

        public Builder setPublicProperties(Map<String, String> map) {
            this.f3256a.f1145f = map;
            return this;
        }
    }

    protected PublicUser() {
    }

    /* renamed from: a */
    protected final void m944a(PublicUser publicUser) {
        publicUser.f1141b = this.f1141b;
        publicUser.f1142c = this.f1142c;
        publicUser.f1143d = this.f1143d;
        publicUser.f1144e = upgqDBbsrL.m1519a(this.f1144e);
        publicUser.f1145f = upgqDBbsrL.m1519a(this.f1145f);
        publicUser.f1146g = upgqDBbsrL.m1519a(this.f1146g);
        publicUser.f1147h = this.f1147h;
    }

    public boolean equals(Object obj) {
        if (this == obj) {
            return true;
        }
        if (obj == null || getClass() != obj.getClass()) {
            return false;
        }
        PublicUser publicUser = (PublicUser) obj;
        if (!this.f1141b.equals(publicUser.f1141b) || this.f1147h != publicUser.f1147h) {
            return false;
        }
        if (this.f1142c != null) {
            if (!this.f1142c.equals(publicUser.f1142c)) {
                return false;
            }
        } else if (publicUser.f1142c != null) {
            return false;
        }
        if (this.f1143d != null) {
            if (!this.f1143d.equals(publicUser.f1143d)) {
                return false;
            }
        } else if (publicUser.f1143d != null) {
            return false;
        }
        return (this.f1144e.equals(publicUser.f1144e) && this.f1145f.equals(publicUser.f1145f)) ? this.f1146g.equals(publicUser.f1146g) : false;
    }

    public Map<String, String> getAllPublicProperties() {
        return Collections.unmodifiableMap(this.f1145f);
    }

    public Map<String, String> getAuthIdentities() {
        return this.f1144e;
    }

    public String getAvatarUrl() {
        return this.f1143d;
    }

    public String getDisplayName() {
        return this.f1142c;
    }

    public String getId() {
        return this.f1141b;
    }

    @Deprecated
    public Map<String, String> getIdentities() {
        return this.f1144e;
    }

    public String getPublicProperty(String str) {
        return (String) this.f1145f.get(str);
    }

    public boolean hasPublicProperty(String str) {
        return this.f1145f.containsKey(str);
    }

    public int hashCode() {
        int i = 0;
        int hashCode = this.f1141b.hashCode();
        int hashCode2 = this.f1142c != null ? this.f1142c.hashCode() : 0;
        int hashCode3 = this.f1143d != null ? this.f1143d.hashCode() : 0;
        int hashCode4 = this.f1144e.hashCode();
        int hashCode5 = this.f1145f.hashCode();
        int hashCode6 = this.f1146g.hashCode();
        if (this.f1147h) {
            i = 1;
        }
        return ((((((((((hashCode2 + (hashCode * 31)) * 31) + hashCode3) * 31) + hashCode4) * 31) + hashCode5) * 31) + hashCode6) * 31) + i;
    }
}
