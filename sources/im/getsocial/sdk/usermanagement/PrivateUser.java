package im.getsocial.sdk.usermanagement;

import im.getsocial.sdk.internal.p033c.p066m.upgqDBbsrL;
import java.util.Collections;
import java.util.Map;

public class PrivateUser extends PublicUser {
    /* renamed from: a */
    Map<String, String> f3252a = null;
    /* renamed from: i */
    private Map<String, String> f3253i = null;
    /* renamed from: j */
    private String f3254j = null;

    public static class Builder {
        /* renamed from: a */
        protected final PrivateUser f3255a = new PrivateUser();

        public Builder(String str) {
            this.f3255a.b = str;
        }

        public PrivateUser build() {
            PrivateUser privateUser = new PrivateUser();
            this.f3255a.m3635a(privateUser);
            return privateUser;
        }

        public Builder setAvatarUrl(String str) {
            this.f3255a.d = str;
            return this;
        }

        public Builder setDisplayName(String str) {
            this.f3255a.c = str;
            return this;
        }

        public Builder setIdentities(Map<String, String> map) {
            this.f3255a.e = map;
            return this;
        }

        public Builder setPassword(String str) {
            this.f3255a.f3254j = str;
            return this;
        }

        public Builder setPrivateProperties(Map<String, String> map) {
            this.f3255a.f3253i = map;
            return this;
        }

        public Builder setPublicProperties(Map<String, String> map) {
            this.f3255a.f = map;
            return this;
        }
    }

    PrivateUser() {
    }

    /* renamed from: a */
    protected final void m3635a(PrivateUser privateUser) {
        m944a(privateUser);
        privateUser.f3253i = upgqDBbsrL.m1519a(this.f3253i);
        privateUser.f3252a = upgqDBbsrL.m1519a(this.f3252a);
        privateUser.f3254j = this.f3254j;
    }

    public boolean equals(Object obj) {
        if (this == obj) {
            return true;
        }
        if (obj == null || getClass() != obj.getClass()) {
            return false;
        }
        PrivateUser privateUser = (PrivateUser) obj;
        return (super.equals(privateUser) && this.f3253i.equals(privateUser.f3253i)) ? this.f3254j.equals(privateUser.f3254j) : false;
    }

    public Map<String, String> getAllPrivateProperties() {
        return Collections.unmodifiableMap(this.f3253i);
    }

    public String getPassword() {
        return this.f3254j;
    }

    public String getPrivateProperty(String str) {
        return (String) this.f3253i.get(str);
    }

    public boolean hasPrivateProperty(String str) {
        return this.f3253i.containsKey(str);
    }

    public int hashCode() {
        return (this.f3253i.hashCode() * 31) + this.f3254j.hashCode();
    }
}
