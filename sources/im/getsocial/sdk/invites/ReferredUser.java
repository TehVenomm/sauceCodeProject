package im.getsocial.sdk.invites;

import im.getsocial.sdk.usermanagement.PublicUser;
import java.util.Map;

public class ReferredUser extends PublicUser {
    /* renamed from: a */
    private long f2297a;
    /* renamed from: i */
    private String f2298i;

    public static class Builder {
        /* renamed from: a */
        private final ReferredUser f2296a = new ReferredUser();

        public Builder(String str) {
            this.f2296a.b = str;
        }

        public ReferredUser build() {
            ReferredUser referredUser = new ReferredUser();
            this.f2296a.m2263a(referredUser);
            return referredUser;
        }

        public Builder setAvatarUrl(String str) {
            this.f2296a.d = str;
            return this;
        }

        public Builder setDisplayName(String str) {
            this.f2296a.c = str;
            return this;
        }

        public Builder setIdentities(Map<String, String> map) {
            this.f2296a.e = map;
            return this;
        }

        public Builder setInstallationChannel(String str) {
            this.f2296a.f2298i = str;
            return this;
        }

        public Builder setInstallationDate(long j) {
            this.f2296a.f2297a = j;
            return this;
        }

        public Builder setPublicProperties(Map<String, String> map) {
            this.f2296a.f = map;
            return this;
        }
    }

    protected ReferredUser() {
    }

    /* renamed from: a */
    protected final void m2263a(ReferredUser referredUser) {
        m944a(referredUser);
        referredUser.f2297a = this.f2297a;
        referredUser.f2298i = this.f2298i;
    }

    public boolean equals(Object obj) {
        if (this == obj) {
            return true;
        }
        if (obj == null || getClass() != obj.getClass()) {
            return false;
        }
        return this.f2297a == ((ReferredUser) obj).f2297a && super.equals(obj);
    }

    public String getInstallationChannel() {
        return this.f2298i;
    }

    public long getInstallationDate() {
        return this.f2297a;
    }

    public int hashCode() {
        return (super.hashCode() * 31) + ((int) (this.f2297a ^ (this.f2297a >>> 32)));
    }
}
