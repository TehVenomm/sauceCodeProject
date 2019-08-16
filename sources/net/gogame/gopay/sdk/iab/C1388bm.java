package net.gogame.gopay.sdk.iab;

/* renamed from: net.gogame.gopay.sdk.iab.bm */
final class C1388bm implements Runnable {

    /* renamed from: a */
    final /* synthetic */ PurchaseActivity f1094a;

    C1388bm(PurchaseActivity purchaseActivity) {
        this.f1094a = purchaseActivity;
    }

    public final void run() {
        this.f1094a.f1051s.setVisibility(0);
        this.f1094a.f1051s.bringToFront();
    }
}
