package net.gogame.gopay.sdk.iab;

final class aw implements Runnable {
    /* renamed from: a */
    final /* synthetic */ String f1079a;
    /* renamed from: b */
    final /* synthetic */ at f1080b;

    aw(at atVar, String str) {
        this.f1080b = atVar;
        this.f1079a = str;
    }

    public final void run() {
        this.f1080b.f1074b.f1046y.loadData(this.f1079a, "text/html; charset=UTF-8", null);
    }
}
