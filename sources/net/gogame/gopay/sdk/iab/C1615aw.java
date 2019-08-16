package net.gogame.gopay.sdk.iab;

/* renamed from: net.gogame.gopay.sdk.iab.aw */
final class C1615aw implements Runnable {

    /* renamed from: a */
    final /* synthetic */ String f1243a;

    /* renamed from: b */
    final /* synthetic */ C1376at f1244b;

    C1615aw(C1376at atVar, String str) {
        this.f1244b = atVar;
        this.f1243a = str;
    }

    public final void run() {
        this.f1244b.f1067b.f1051y.loadData(this.f1243a, "text/html; charset=UTF-8", null);
    }
}
