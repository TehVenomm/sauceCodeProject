package im.getsocial.sdk.activities;

import android.graphics.Bitmap;
import java.util.Arrays;

public final class ActivityPostContent {
    /* renamed from: a */
    private String f1130a;
    /* renamed from: b */
    private Bitmap f1131b;
    /* renamed from: c */
    private byte[] f1132c;
    /* renamed from: d */
    private String f1133d;
    /* renamed from: e */
    private String f1134e;

    public static final class Builder {
        /* renamed from: a */
        private final ActivityPostContent f1129a = new ActivityPostContent();

        public final ActivityPostContent build() {
            return this.f1129a;
        }

        public final Builder withButton(String str, String str2) {
            this.f1129a.f1133d = str;
            this.f1129a.f1134e = str2;
            return this;
        }

        public final Builder withImage(Bitmap bitmap) {
            this.f1129a.f1132c = null;
            this.f1129a.f1131b = bitmap;
            return this;
        }

        public final Builder withText(String str) {
            this.f1129a.f1130a = str;
            return this;
        }

        public final Builder withVideo(byte[] bArr) {
            this.f1129a.f1131b = null;
            this.f1129a.f1132c = bArr;
            return this;
        }
    }

    ActivityPostContent() {
    }

    public static Builder createBuilderWithButton(String str, String str2) {
        return new Builder().withButton(str, str2);
    }

    public static Builder createBuilderWithImage(Bitmap bitmap) {
        return new Builder().withImage(bitmap);
    }

    public static Builder createBuilderWithText(String str) {
        return new Builder().withText(str);
    }

    public static Builder createBuilderWithVideo(byte[] bArr) {
        return new Builder().withVideo(bArr);
    }

    public final boolean equals(Object obj) {
        if (this != obj) {
            if (obj == null || getClass() != obj.getClass()) {
                return false;
            }
            ActivityPostContent activityPostContent = (ActivityPostContent) obj;
            if (this.f1130a != null) {
                if (!this.f1130a.equals(activityPostContent.f1130a)) {
                    return false;
                }
            } else if (activityPostContent.f1130a != null) {
                return false;
            }
            if (this.f1131b != null) {
                if (!this.f1131b.sameAs(activityPostContent.f1131b)) {
                    return false;
                }
            } else if (activityPostContent.f1131b != null) {
                return false;
            }
            if (!Arrays.equals(this.f1132c, activityPostContent.f1132c)) {
                return false;
            }
            if (this.f1133d != null) {
                if (!this.f1133d.equals(activityPostContent.f1133d)) {
                    return false;
                }
            } else if (activityPostContent.f1133d != null) {
                return false;
            }
            if (this.f1134e != null) {
                return this.f1134e.equals(activityPostContent.f1134e);
            }
            if (activityPostContent.f1134e != null) {
                return false;
            }
        }
        return true;
    }

    public final String getButtonAction() {
        return this.f1134e;
    }

    public final String getButtonTitle() {
        return this.f1133d;
    }

    public final Bitmap getImage() {
        return this.f1131b;
    }

    public final String getText() {
        return this.f1130a;
    }

    public final byte[] getVideo() {
        return this.f1132c;
    }

    public final int hashCode() {
        int i = 0;
        int hashCode = this.f1130a != null ? this.f1130a.hashCode() : 0;
        int hashCode2 = this.f1131b != null ? this.f1131b.hashCode() : 0;
        int hashCode3 = Arrays.hashCode(this.f1132c);
        int hashCode4 = this.f1133d != null ? this.f1133d.hashCode() : 0;
        if (this.f1134e != null) {
            i = this.f1134e.hashCode();
        }
        return (((((((hashCode * 31) + hashCode2) * 31) + hashCode3) * 31) + hashCode4) * 31) + i;
    }

    public final String toString() {
        return "ActivityPostContent{_text='" + this.f1130a + '\'' + ", _image=" + (this.f1131b == null ? "No image" : "Has image") + ", _video=" + (this.f1132c == null ? "No video" : "Has video") + ", _buttonTitle='" + this.f1133d + '\'' + ", _buttonAction='" + this.f1134e + '\'' + '}';
    }
}
