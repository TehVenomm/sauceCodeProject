package net.gogame.gopay.vip;

public class VipStatus {

    /* renamed from: a */
    private final String f1355a;

    /* renamed from: b */
    private final boolean f1356b;

    /* renamed from: c */
    private final boolean f1357c;

    /* renamed from: d */
    private final String f1358d;

    VipStatus(String str, boolean z, boolean z2, String str2) {
        this.f1355a = str;
        this.f1356b = z;
        this.f1357c = z2;
        this.f1358d = str2;
    }

    public String getGuid() {
        return this.f1355a;
    }

    public boolean isVip() {
        return this.f1356b;
    }

    public boolean isSuspended() {
        return this.f1357c;
    }

    public String getSuspensionMessage() {
        return this.f1358d;
    }
}
