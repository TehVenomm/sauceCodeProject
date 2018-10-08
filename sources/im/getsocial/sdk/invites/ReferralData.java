package im.getsocial.sdk.invites;

public class ReferralData {
    /* renamed from: a */
    private final String f2287a;
    /* renamed from: b */
    private final String f2288b;
    /* renamed from: c */
    private final String f2289c;
    /* renamed from: d */
    private final boolean f2290d;
    /* renamed from: e */
    private final boolean f2291e;
    /* renamed from: f */
    private final boolean f2292f;
    /* renamed from: g */
    private final boolean f2293g;
    /* renamed from: h */
    private final LinkParams f2294h;
    /* renamed from: i */
    private final LinkParams f2295i;

    ReferralData(String str, String str2, String str3, boolean z, boolean z2, boolean z3, boolean z4, LinkParams linkParams, LinkParams linkParams2) {
        this.f2287a = str;
        this.f2288b = str2;
        this.f2290d = z;
        this.f2291e = z2;
        this.f2292f = z3;
        this.f2293g = z4;
        this.f2294h = linkParams;
        this.f2289c = str3;
        this.f2295i = linkParams2;
    }

    public CustomReferralData getCustomReferralData() {
        return new CustomReferralData(this.f2294h.getStringValues());
    }

    public LinkParams getLinkParams() {
        return this.f2294h;
    }

    public CustomReferralData getOriginalCustomReferralData() {
        return new CustomReferralData(this.f2295i.getStringValues());
    }

    public LinkParams getOriginalLinkParams() {
        return this.f2295i;
    }

    public String getReferrerChannelId() {
        return this.f2289c;
    }

    public String getReferrerUserId() {
        return this.f2288b;
    }

    public String getToken() {
        return this.f2287a;
    }

    public boolean isFirstMatch() {
        return this.f2290d;
    }

    public boolean isFirstMatchLink() {
        return this.f2293g;
    }

    public boolean isGuaranteedMatch() {
        return this.f2291e;
    }

    public boolean isReinstall() {
        return this.f2292f;
    }

    public String toString() {
        StringBuilder stringBuilder = new StringBuilder("ReferralData{");
        stringBuilder.append("token='").append(this.f2287a).append('\'');
        stringBuilder.append(", referrerUserId='").append(this.f2288b).append('\'');
        stringBuilder.append(", referrerChannelId='").append(this.f2289c).append('\'');
        stringBuilder.append(", isFirstMatch=").append(this.f2290d);
        stringBuilder.append(", isGuaranteedMatch=").append(this.f2291e);
        stringBuilder.append(", isReinstall=").append(this.f2292f);
        stringBuilder.append(", isFirstMatchLink=").append(this.f2293g);
        stringBuilder.append(", linkParams=").append(this.f2294h);
        stringBuilder.append(", originalLinkParams=").append(this.f2295i);
        stringBuilder.append('}');
        return stringBuilder.toString();
    }
}
