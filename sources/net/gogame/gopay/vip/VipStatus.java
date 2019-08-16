package net.gogame.gopay.vip;

public class VipStatus {

    /* renamed from: a */
    private final String f1369a;

    /* renamed from: b */
    private final boolean f1370b;

    /* renamed from: c */
    private final boolean f1371c;

    /* renamed from: d */
    private final String f1372d;

    VipStatus(String str, boolean z, boolean z2, String str2) {
        this.f1369a = str;
        this.f1370b = z;
        this.f1371c = z2;
        this.f1372d = str2;
    }

    public String getGuid() {
        return this.f1369a;
    }

    public boolean isVip() {
        return this.f1370b;
    }

    public boolean isSuspended() {
        return this.f1371c;
    }

    public String getSuspensionMessage() {
        return this.f1372d;
    }
}
