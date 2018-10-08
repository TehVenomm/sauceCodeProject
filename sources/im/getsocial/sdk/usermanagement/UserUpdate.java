package im.getsocial.sdk.usermanagement;

import android.graphics.Bitmap;
import java.util.HashMap;
import java.util.Map;

public class UserUpdate {
    /* renamed from: a */
    String f3262a;
    /* renamed from: b */
    String f3263b;
    /* renamed from: c */
    Bitmap f3264c;
    /* renamed from: d */
    final Map<String, String> f3265d = new HashMap();
    /* renamed from: e */
    final Map<String, String> f3266e = new HashMap();
    /* renamed from: f */
    final Map<String, String> f3267f = new HashMap();
    /* renamed from: g */
    final Map<String, String> f3268g = new HashMap();

    public static class Builder {
        /* renamed from: a */
        UserUpdate f3261a;

        private Builder() {
            this.f3261a = new UserUpdate();
        }

        public UserUpdate build() {
            UserUpdate userUpdate = new UserUpdate();
            userUpdate.f3262a = this.f3261a.f3262a;
            userUpdate.f3263b = this.f3261a.f3263b;
            userUpdate.f3264c = this.f3261a.f3264c;
            userUpdate.f3265d.putAll(this.f3261a.f3265d);
            userUpdate.f3266e.putAll(this.f3261a.f3266e);
            userUpdate.f3267f.putAll(this.f3261a.f3267f);
            userUpdate.f3268g.putAll(this.f3261a.f3268g);
            return userUpdate;
        }

        public Builder removePrivateProperty(String str) {
            this.f3261a.f3266e.put(str, "");
            return this;
        }

        public Builder removePublicProperty(String str) {
            this.f3261a.f3265d.put(str, "");
            return this;
        }

        public Builder setPrivateProperty(String str, String str2) {
            this.f3261a.f3266e.put(str, str2);
            return this;
        }

        public Builder setPublicProperty(String str, String str2) {
            this.f3261a.f3265d.put(str, str2);
            return this;
        }

        public Builder updateAvatar(Bitmap bitmap) {
            this.f3261a.f3264c = bitmap;
            this.f3261a.f3263b = null;
            return this;
        }

        public Builder updateAvatarUrl(String str) {
            this.f3261a.f3263b = str;
            this.f3261a.f3264c = null;
            return this;
        }

        public Builder updateDisplayName(String str) {
            this.f3261a.f3262a = str;
            return this;
        }
    }

    UserUpdate() {
    }

    public static Builder createBuilder() {
        return new Builder();
    }
}
