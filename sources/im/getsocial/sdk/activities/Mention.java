package im.getsocial.sdk.activities;

public class Mention {
    /* renamed from: a */
    private String f1136a;
    /* renamed from: b */
    private int f1137b;
    /* renamed from: c */
    private int f1138c;
    /* renamed from: d */
    private String f1139d;

    public static class Builder {
        /* renamed from: a */
        private final Mention f1135a = new Mention();

        Builder() {
        }

        public Mention build() {
            return this.f1135a;
        }

        public Builder withEndIndex(int i) {
            this.f1135a.f1138c = i;
            return this;
        }

        public Builder withStartIndex(int i) {
            this.f1135a.f1137b = i;
            return this;
        }

        public Builder withType(String str) {
            this.f1135a.f1139d = str;
            return this;
        }

        public Builder withUserId(String str) {
            this.f1135a.f1136a = str;
            return this;
        }
    }

    public static Builder builder() {
        return new Builder();
    }

    public int getEndIndex() {
        return this.f1138c;
    }

    public int getStartIndex() {
        return this.f1137b;
    }

    public String getType() {
        return this.f1139d;
    }

    public String getUserId() {
        return this.f1136a;
    }
}
