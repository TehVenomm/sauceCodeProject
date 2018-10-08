package im.getsocial.sdk.usermanagement;

public class UserReference {
    /* renamed from: a */
    protected String f3258a = null;
    /* renamed from: b */
    protected String f3259b = null;
    /* renamed from: c */
    protected String f3260c = null;

    public static class Builder {
        /* renamed from: a */
        UserReference f3257a = new UserReference();

        public Builder(String str) {
            this.f3257a.f3258a = str;
        }

        public UserReference build() {
            return this.f3257a;
        }

        public Builder setAvatarUrl(String str) {
            this.f3257a.f3260c = str;
            return this;
        }

        public Builder setDisplayName(String str) {
            this.f3257a.f3259b = str;
            return this;
        }
    }

    public String getAvatarUrl() {
        return this.f3260c;
    }

    public String getDisplayName() {
        return this.f3259b;
    }

    public String getId() {
        return this.f3258a;
    }
}
