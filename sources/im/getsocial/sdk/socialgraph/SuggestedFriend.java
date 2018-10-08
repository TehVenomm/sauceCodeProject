package im.getsocial.sdk.socialgraph;

import im.getsocial.sdk.usermanagement.PublicUser;
import java.util.Map;

public class SuggestedFriend extends PublicUser {
    /* renamed from: a */
    private int f2520a;

    public static class Builder {
        /* renamed from: a */
        SuggestedFriend f2519a = new SuggestedFriend();

        public Builder(String str) {
            this.f2519a.b = str;
        }

        public SuggestedFriend build() {
            SuggestedFriend suggestedFriend = new SuggestedFriend();
            this.f2519a.m2480a(suggestedFriend);
            return suggestedFriend;
        }

        public Builder setAvatarUrl(String str) {
            this.f2519a.d = str;
            return this;
        }

        public Builder setDisplayName(String str) {
            this.f2519a.c = str;
            return this;
        }

        public Builder setIdentities(Map<String, String> map) {
            this.f2519a.e = map;
            return this;
        }

        public Builder setMutualFriendsCount(int i) {
            this.f2519a.f2520a = i;
            return this;
        }

        public Builder setPublicProperties(Map<String, String> map) {
            this.f2519a.f = map;
            return this;
        }
    }

    /* renamed from: a */
    protected final void m2480a(SuggestedFriend suggestedFriend) {
        m944a(suggestedFriend);
        suggestedFriend.f2520a = this.f2520a;
    }

    public boolean equals(Object obj) {
        if (this == obj) {
            return true;
        }
        if (obj == null || getClass() != obj.getClass()) {
            return false;
        }
        return super.equals(obj) && this.f2520a == ((SuggestedFriend) obj).f2520a;
    }

    public int getMutualFriendsCount() {
        return this.f2520a;
    }

    public int hashCode() {
        return super.hashCode() + this.f2520a;
    }
}
