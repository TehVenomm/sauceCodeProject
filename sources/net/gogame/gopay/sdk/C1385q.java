package net.gogame.gopay.sdk;

/* renamed from: net.gogame.gopay.sdk.q */
final class C1385q implements Runnable {
    /* renamed from: a */
    final /* synthetic */ C1350h f3577a;
    /* renamed from: b */
    final /* synthetic */ StoreActivity f3578b;

    C1385q(StoreActivity storeActivity, C1350h c1350h) {
        this.f3578b = storeActivity;
        this.f3577a = c1350h;
    }

    public final void run() {
        this.f3578b.f3348d.m3782a((String) this.f3577a.f3374d.get("country"), this.f3577a.f3373c);
        this.f3578b.f3349e.setData(this.f3577a);
    }
}
