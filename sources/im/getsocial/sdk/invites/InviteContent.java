package im.getsocial.sdk.invites;

import android.graphics.Bitmap;

public class InviteContent {
    /* renamed from: a */
    private String f2266a;
    /* renamed from: b */
    private Bitmap f2267b;
    /* renamed from: c */
    private String f2268c;
    /* renamed from: d */
    private String f2269d;
    /* renamed from: e */
    private String f2270e;
    /* renamed from: f */
    private String f2271f;
    /* renamed from: g */
    private byte[] f2272g;

    public static class Builder {
        /* renamed from: a */
        InviteContent f2265a = new InviteContent();

        Builder() {
        }

        public InviteContent build() {
            InviteContent inviteContent = new InviteContent();
            inviteContent.f2266a = this.f2265a.f2266a;
            inviteContent.f2268c = this.f2265a.f2268c;
            inviteContent.f2269d = this.f2265a.f2269d;
            inviteContent.f2267b = this.f2265a.f2267b;
            inviteContent.f2271f = this.f2265a.f2271f;
            inviteContent.f2270e = this.f2265a.f2270e;
            inviteContent.f2272g = this.f2265a.f2272g;
            return inviteContent;
        }

        public Builder withImage(Bitmap bitmap) {
            this.f2265a.f2267b = bitmap;
            return this;
        }

        public Builder withImageUrl(String str) {
            this.f2265a.f2266a = str;
            return this;
        }

        public Builder withSubject(String str) {
            this.f2265a.f2268c = str;
            return this;
        }

        public Builder withText(String str) {
            this.f2265a.f2269d = str;
            return this;
        }

        public Builder withVideo(byte[] bArr) {
            this.f2265a.f2272g = bArr;
            return this;
        }
    }

    InviteContent() {
    }

    public static Builder createBuilder() {
        return new Builder();
    }

    public boolean equals(Object obj) {
        if (this != obj) {
            if (obj == null || getClass() != obj.getClass()) {
                return false;
            }
            InviteContent inviteContent = (InviteContent) obj;
            if (this.f2271f != null) {
                if (!this.f2271f.equals(inviteContent.f2271f)) {
                    return false;
                }
            } else if (inviteContent.f2271f != null) {
                return false;
            }
            if (this.f2270e != null) {
                if (!this.f2270e.equals(inviteContent.f2270e)) {
                    return false;
                }
            } else if (inviteContent.f2270e != null) {
                return false;
            }
            if (this.f2266a != null) {
                if (!this.f2266a.equals(inviteContent.f2266a)) {
                    return false;
                }
            } else if (inviteContent.f2266a != null) {
                return false;
            }
            if (this.f2267b != null) {
                if (!this.f2267b.sameAs(inviteContent.f2267b)) {
                    return false;
                }
            } else if (inviteContent.f2267b != null) {
                return false;
            }
            if (this.f2268c != null) {
                if (!this.f2268c.equals(inviteContent.f2268c)) {
                    return false;
                }
            } else if (inviteContent.f2268c != null) {
                return false;
            }
            if (this.f2269d != null) {
                return this.f2269d.equals(inviteContent.f2269d);
            }
            if (inviteContent.f2269d != null) {
                return false;
            }
        }
        return true;
    }

    public String getGifUrl() {
        return this.f2270e;
    }

    public Bitmap getImage() {
        return this.f2267b;
    }

    public String getImageUrl() {
        return this.f2266a;
    }

    public String getSubject() {
        return this.f2268c;
    }

    public String getText() {
        return this.f2269d;
    }

    public byte[] getVideo() {
        return this.f2272g;
    }

    public String getVideoUrl() {
        return this.f2271f;
    }

    public int hashCode() {
        int i = 0;
        int hashCode = this.f2266a != null ? this.f2266a.hashCode() : 0;
        int hashCode2 = this.f2270e != null ? this.f2270e.hashCode() : 0;
        int hashCode3 = this.f2271f != null ? this.f2271f.hashCode() : 0;
        int hashCode4 = this.f2268c != null ? this.f2268c.hashCode() : 0;
        if (this.f2269d != null) {
            i = this.f2269d.hashCode();
        }
        return (((((((hashCode * 31) + hashCode2) * 31) + hashCode3) * 31) + hashCode4) * 31) + i;
    }

    public String toString() {
        return "InviteContent{_imageUrl='" + this.f2266a + '\'' + ", _image=" + (this.f2267b == null ? "No image" : "Has image") + ", _subject='" + this.f2268c + '\'' + ", _text='" + this.f2269d + '\'' + ", _gifUrl='" + this.f2270e + '\'' + ", _videoUrl='" + this.f2271f + '\'' + ", _video=" + (this.f2272g == null ? "No video" : "Has video") + '}';
    }
}
