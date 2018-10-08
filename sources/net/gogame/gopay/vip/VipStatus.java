package net.gogame.gopay.vip;

public class VipStatus {
    /* renamed from: a */
    private final String f3688a;
    /* renamed from: b */
    private final boolean f3689b;
    /* renamed from: c */
    private final boolean f3690c;
    /* renamed from: d */
    private final String f3691d;

    VipStatus(String str, boolean z, boolean z2, String str2) {
        this.f3688a = str;
        this.f3689b = z;
        this.f3690c = z2;
        this.f3691d = str2;
    }

    public String getGuid() {
        return this.f3688a;
    }

    public boolean isVip() {
        return this.f3689b;
    }

    public boolean isSuspended() {
        return this.f3690c;
    }

    public String getSuspensionMessage() {
        return this.f3691d;
    }
}
