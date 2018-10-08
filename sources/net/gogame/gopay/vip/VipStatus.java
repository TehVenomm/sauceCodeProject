package net.gogame.gopay.vip;

public class VipStatus {
    /* renamed from: a */
    private final String f1300a;
    /* renamed from: b */
    private final boolean f1301b;
    /* renamed from: c */
    private final boolean f1302c;
    /* renamed from: d */
    private final String f1303d;

    VipStatus(String str, boolean z, boolean z2, String str2) {
        this.f1300a = str;
        this.f1301b = z;
        this.f1302c = z2;
        this.f1303d = str2;
    }

    public String getGuid() {
        return this.f1300a;
    }

    public boolean isVip() {
        return this.f1301b;
    }

    public boolean isSuspended() {
        return this.f1302c;
    }

    public String getSuspensionMessage() {
        return this.f1303d;
    }
}
