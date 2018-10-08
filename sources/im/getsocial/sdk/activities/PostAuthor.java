package im.getsocial.sdk.activities;

import im.getsocial.sdk.usermanagement.PublicUser;
import java.util.HashMap;
import java.util.Map;

public class PostAuthor extends PublicUser {
    /* renamed from: a */
    protected boolean f1148a;

    public static class Builder {
        /* renamed from: a */
        PostAuthor f1140a = new PostAuthor();

        public Builder(String str) {
            this.f1140a.b = str;
        }

        public PostAuthor build() {
            PostAuthor postAuthor = new PostAuthor();
            this.f1140a.m950a(postAuthor);
            return postAuthor;
        }

        public Builder setAvatarUrl(String str) {
            this.f1140a.d = str;
            return this;
        }

        public Builder setDisplayName(String str) {
            this.f1140a.c = str;
            return this;
        }

        public Builder setIdentities(Map<String, String> map) {
            this.f1140a.e = map;
            return this;
        }

        public Builder setPublicProperties(HashMap<String, String> hashMap) {
            this.f1140a.f = hashMap;
            return this;
        }

        public Builder setVerified(boolean z) {
            this.f1140a.f1148a = z;
            return this;
        }
    }

    protected PostAuthor() {
    }

    /* renamed from: a */
    protected final void m950a(PostAuthor postAuthor) {
        m944a(postAuthor);
        postAuthor.f1148a = this.f1148a;
    }

    public boolean equals(Object obj) {
        if (this == obj) {
            return true;
        }
        if (obj == null || getClass() != obj.getClass()) {
            return false;
        }
        return this.f1148a == ((PostAuthor) obj).f1148a && super.equals(obj);
    }

    public int hashCode() {
        return (this.f1148a ? 1 : 0) + super.hashCode();
    }

    public boolean isVerified() {
        return this.f1148a;
    }
}
