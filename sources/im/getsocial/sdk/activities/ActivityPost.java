package im.getsocial.sdk.activities;

import im.getsocial.sdk.internal.p033c.p066m.upgqDBbsrL;
import java.util.Date;
import java.util.List;

public final class ActivityPost {
    /* renamed from: a */
    private String f1118a;
    /* renamed from: b */
    private jjbQypPegg f1119b;
    /* renamed from: c */
    private PostAuthor f1120c;
    /* renamed from: d */
    private long f1121d;
    /* renamed from: e */
    private long f1122e;
    /* renamed from: f */
    private long f1123f;
    /* renamed from: g */
    private int f1124g;
    /* renamed from: h */
    private boolean f1125h;
    /* renamed from: i */
    private int f1126i;
    /* renamed from: j */
    private String f1127j;
    /* renamed from: k */
    private List<Mention> f1128k;

    public static class Builder {
        /* renamed from: a */
        private final ActivityPost f1111a = new ActivityPost();

        Builder() {
        }

        public Builder author(PostAuthor postAuthor) {
            this.f1111a.f1120c = postAuthor;
            return this;
        }

        public ActivityPost build() {
            return this.f1111a;
        }

        public Builder commentsCount(int i) {
            this.f1111a.f1124g = i;
            return this;
        }

        @Deprecated
        public Builder content(String str, String str2, String str3, String str4) {
            return content(str, str2, null, str3, str4);
        }

        public Builder content(String str, String str2, String str3, String str4, String str5) {
            this.f1111a.f1119b = new jjbQypPegg();
            this.f1111a.f1119b.f1114a = str;
            this.f1111a.f1119b.f1115b = str2;
            this.f1111a.f1119b.f1117d = str3;
            if (!(str4 == null || str5 == null)) {
                this.f1111a.f1119b.f1116c = new jjbQypPegg();
                this.f1111a.f1119b.f1116c.f1112a = str4;
                this.f1111a.f1119b.f1116c.f1113b = str5;
            }
            return this;
        }

        public Builder createdAt(long j) {
            this.f1111a.f1121d = j;
            return this;
        }

        public Builder feedId(String str) {
            this.f1111a.f1127j = str;
            return this;
        }

        public Builder id(String str) {
            this.f1111a.f1118a = str;
            return this;
        }

        public Builder likedByMe(boolean z) {
            this.f1111a.f1125h = z;
            return this;
        }

        public Builder likesCount(int i) {
            this.f1111a.f1126i = i;
            return this;
        }

        public Builder mentions(List<Mention> list) {
            this.f1111a.f1128k = upgqDBbsrL.m1518a((List) list);
            return this;
        }

        public Builder stickyEnd(long j) {
            this.f1111a.f1123f = j;
            return this;
        }

        public Builder stickyStart(long j) {
            this.f1111a.f1122e = j;
            return this;
        }
    }

    public enum Type {
        POST,
        COMMENT
    }

    static class jjbQypPegg {
        /* renamed from: a */
        String f1114a;
        /* renamed from: b */
        String f1115b;
        /* renamed from: c */
        jjbQypPegg f1116c;
        /* renamed from: d */
        String f1117d;

        static class jjbQypPegg {
            /* renamed from: a */
            String f1112a;
            /* renamed from: b */
            String f1113b;

            jjbQypPegg() {
            }

            public boolean equals(Object obj) {
                if (this != obj) {
                    if (obj == null || getClass() != obj.getClass()) {
                        return false;
                    }
                    jjbQypPegg jjbqyppegg = (jjbQypPegg) obj;
                    if (this.f1112a != null) {
                        if (!this.f1112a.equals(jjbqyppegg.f1112a)) {
                            return false;
                        }
                    } else if (jjbqyppegg.f1112a != null) {
                        return false;
                    }
                    if (this.f1113b != null) {
                        return this.f1113b.equals(jjbqyppegg.f1113b);
                    }
                    if (jjbqyppegg.f1113b != null) {
                        return false;
                    }
                }
                return true;
            }

            public int hashCode() {
                int i = 0;
                int hashCode = this.f1112a != null ? this.f1112a.hashCode() : 0;
                if (this.f1113b != null) {
                    i = this.f1113b.hashCode();
                }
                return (hashCode * 31) + i;
            }
        }

        jjbQypPegg() {
        }

        public boolean equals(Object obj) {
            if (this != obj) {
                if (obj == null || getClass() != obj.getClass()) {
                    return false;
                }
                jjbQypPegg jjbqyppegg = (jjbQypPegg) obj;
                if (this.f1114a != null) {
                    if (!this.f1114a.equals(jjbqyppegg.f1114a)) {
                        return false;
                    }
                } else if (jjbqyppegg.f1114a != null) {
                    return false;
                }
                if (this.f1115b != null) {
                    if (!this.f1115b.equals(jjbqyppegg.f1115b)) {
                        return false;
                    }
                } else if (jjbqyppegg.f1115b != null) {
                    return false;
                }
                if (this.f1116c != null) {
                    return this.f1116c.equals(jjbqyppegg.f1116c);
                }
                if (jjbqyppegg.f1116c != null) {
                    return false;
                }
            }
            return true;
        }

        public int hashCode() {
            int i = 0;
            int hashCode = this.f1114a != null ? this.f1114a.hashCode() : 0;
            int hashCode2 = this.f1115b != null ? this.f1115b.hashCode() : 0;
            if (this.f1116c != null) {
                i = this.f1116c.hashCode();
            }
            return (((hashCode * 31) + hashCode2) * 31) + i;
        }
    }

    ActivityPost() {
    }

    public static Builder builder() {
        return new Builder();
    }

    public final boolean equals(Object obj) {
        if (this == obj) {
            return true;
        }
        if (obj == null || getClass() != obj.getClass()) {
            return false;
        }
        ActivityPost activityPost = (ActivityPost) obj;
        if (!this.f1118a.equals(activityPost.f1118a) || this.f1121d != activityPost.f1121d || this.f1122e != activityPost.f1122e || this.f1123f != activityPost.f1123f || this.f1124g != activityPost.f1124g || this.f1125h != activityPost.f1125h || this.f1126i != activityPost.f1126i) {
            return false;
        }
        if (this.f1119b != null) {
            if (!this.f1119b.equals(activityPost.f1119b)) {
                return false;
            }
        } else if (activityPost.f1119b != null) {
            return false;
        }
        if (!this.f1120c.equals(activityPost.f1120c)) {
            return false;
        }
        if (this.f1127j != null) {
            if (!this.f1127j.equals(activityPost.f1127j)) {
                return false;
            }
        } else if (activityPost.f1127j != null) {
            return false;
        }
        return upgqDBbsrL.m1520a(this.f1128k, activityPost.f1128k);
    }

    public final PostAuthor getAuthor() {
        return this.f1120c;
    }

    public final String getButtonAction() {
        return hasButton() ? this.f1119b.f1116c.f1113b : null;
    }

    public final String getButtonTitle() {
        return (this.f1119b != null && hasButton()) ? this.f1119b.f1116c.f1112a : null;
    }

    public final int getCommentsCount() {
        return this.f1124g;
    }

    public final long getCreatedAt() {
        return this.f1121d;
    }

    public final String getFeedId() {
        return this.f1127j;
    }

    public final String getId() {
        return this.f1118a;
    }

    public final String getImageUrl() {
        return this.f1119b == null ? null : this.f1119b.f1115b;
    }

    public final int getLikesCount() {
        return this.f1126i;
    }

    public final List<Mention> getMentions() {
        return this.f1128k;
    }

    public final long getStickyEnd() {
        return this.f1123f;
    }

    public final long getStickyStart() {
        return this.f1122e;
    }

    public final String getText() {
        return this.f1119b == null ? null : this.f1119b.f1114a;
    }

    public final String getVideoUrl() {
        return this.f1119b == null ? null : this.f1119b.f1117d;
    }

    public final boolean hasButton() {
        return (this.f1119b == null || this.f1119b.f1116c == null) ? false : true;
    }

    public final boolean hasImage() {
        return (this.f1119b == null || this.f1119b.f1115b == null || this.f1119b.f1115b.isEmpty()) ? false : true;
    }

    public final boolean hasText() {
        return (this.f1119b == null || this.f1119b.f1114a == null || this.f1119b.f1114a.isEmpty()) ? false : true;
    }

    public final boolean hasVideo() {
        return (this.f1119b == null || this.f1119b.f1117d == null || this.f1119b.f1117d.isEmpty()) ? false : true;
    }

    public final int hashCode() {
        int i = 0;
        int hashCode = this.f1118a.hashCode();
        int hashCode2 = this.f1119b != null ? this.f1119b.hashCode() : 0;
        int hashCode3 = this.f1120c.hashCode();
        int i2 = (int) (this.f1121d ^ (this.f1121d >>> 32));
        int i3 = (int) (this.f1122e ^ (this.f1122e >>> 32));
        int i4 = (int) (this.f1123f ^ (this.f1123f >>> 32));
        int i5 = this.f1124g;
        int i6 = this.f1125h ? 1 : 0;
        int i7 = this.f1126i;
        if (this.f1127j != null) {
            i = this.f1127j.hashCode();
        }
        return ((((((((((((((((hashCode2 + (hashCode * 31)) * 31) + hashCode3) * 31) + i2) * 31) + i3) * 31) + i4) * 31) + i5) * 31) + i6) * 31) + i7) * 31) + i;
    }

    public final boolean isLikedByMe() {
        return this.f1125h;
    }

    public final boolean isStickyAt(Date date) {
        return date.after(new Date(this.f1122e)) && date.before(new Date(this.f1123f));
    }

    public final String toString() {
        return "ActivityPost{_id='" + getId() + '\'' + ", _text='" + getText() + '\'' + ", _imageProvider='" + getImageUrl() + ", _videoUrl='" + getVideoUrl() + '\'' + ", _buttonTitle='" + getButtonTitle() + '\'' + ", _buttonAction='" + getButtonAction() + '\'' + '}';
    }
}
