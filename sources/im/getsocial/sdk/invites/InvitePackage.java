package im.getsocial.sdk.invites;

import android.graphics.Bitmap;

public class InvitePackage {
    /* renamed from: a */
    private String f2274a;
    /* renamed from: b */
    private String f2275b;
    /* renamed from: c */
    private String f2276c;
    /* renamed from: d */
    private Bitmap f2277d;
    /* renamed from: e */
    private String f2278e;
    /* renamed from: f */
    private String f2279f;
    /* renamed from: g */
    private String f2280g;
    /* renamed from: h */
    private String f2281h;

    public static class Builder {
        /* renamed from: a */
        private final InvitePackage f2273a = new InvitePackage();

        public InvitePackage build() {
            InvitePackage invitePackage = new InvitePackage();
            invitePackage.f2276c = this.f2273a.f2276c;
            invitePackage.f2279f = this.f2273a.f2279f;
            invitePackage.f2274a = this.f2273a.f2274a;
            invitePackage.f2277d = this.f2273a.f2277d;
            invitePackage.f2275b = this.f2273a.f2275b;
            invitePackage.f2278e = this.f2273a.f2278e;
            invitePackage.f2281h = this.f2273a.f2281h;
            invitePackage.f2280g = this.f2273a.f2280g;
            return invitePackage;
        }

        public Builder withGifUrl(String str) {
            this.f2273a.f2281h = str;
            return this;
        }

        public Builder withImage(Bitmap bitmap) {
            this.f2273a.f2277d = bitmap;
            return this;
        }

        public Builder withImageUrl(String str) {
            this.f2273a.f2278e = str;
            return this;
        }

        public Builder withReferralUrl(String str) {
            this.f2273a.f2279f = str;
            return this;
        }

        public Builder withSubject(String str) {
            this.f2273a.f2274a = str;
            return this;
        }

        public Builder withText(String str) {
            this.f2273a.f2275b = str;
            return this;
        }

        public Builder withUsername(String str) {
            this.f2273a.f2276c = str;
            return this;
        }

        public Builder withVideoUrl(String str) {
            this.f2273a.f2280g = str;
            return this;
        }
    }

    public String getGifUrl() {
        return this.f2281h;
    }

    public Bitmap getImage() {
        return this.f2277d;
    }

    public String getImageUrl() {
        return this.f2278e;
    }

    public String getReferralUrl() {
        return this.f2279f;
    }

    public String getSubject() {
        return this.f2274a;
    }

    public String getText() {
        return this.f2275b;
    }

    public String getUserName() {
        return this.f2276c;
    }

    public String getVideoUrl() {
        return this.f2280g;
    }
}
