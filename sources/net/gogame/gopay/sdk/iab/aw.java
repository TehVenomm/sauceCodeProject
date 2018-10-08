package net.gogame.gopay.sdk.iab;

final class aw implements Runnable {
    /* renamed from: a */
    final /* synthetic */ String f3467a;
    /* renamed from: b */
    final /* synthetic */ at f3468b;

    aw(at atVar, String str) {
        this.f3468b = atVar;
        this.f3467a = str;
    }

    public final void run() {
        this.f3468b.f3462b.f3434y.loadData(this.f3467a, "text/html; charset=UTF-8", null);
    }
}
