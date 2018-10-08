package net.gogame.gopay.sdk;

/* renamed from: net.gogame.gopay.sdk.q */
final class C1069q implements Runnable {
    /* renamed from: a */
    final /* synthetic */ C1034h f1189a;
    /* renamed from: b */
    final /* synthetic */ StoreActivity f1190b;

    C1069q(StoreActivity storeActivity, C1034h c1034h) {
        this.f1190b = storeActivity;
        this.f1189a = c1034h;
    }

    public final void run() {
        this.f1190b.f960d.m757a((String) this.f1189a.f986d.get("country"), this.f1189a.f985c);
        this.f1190b.f961e.setData(this.f1189a);
    }
}
