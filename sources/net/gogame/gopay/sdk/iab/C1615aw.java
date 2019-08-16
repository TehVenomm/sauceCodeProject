package net.gogame.gopay.sdk.iab;

/* renamed from: net.gogame.gopay.sdk.iab.aw */
final class C1615aw implements Runnable {

    /* renamed from: a */
    final /* synthetic */ String f1255a;

    /* renamed from: b */
    final /* synthetic */ C1376at f1256b;

    C1615aw(C1376at atVar, String str) {
        this.f1256b = atVar;
        this.f1255a = str;
    }

    public final void run() {
        this.f1256b.f1073b.f1057y.loadData(this.f1255a, "text/html; charset=UTF-8", null);
    }
}
