package net.gogame.gopay.sdk.iab;

/* renamed from: net.gogame.gopay.sdk.iab.bm */
final class C1388bm implements Runnable {

    /* renamed from: a */
    final /* synthetic */ PurchaseActivity f1088a;

    C1388bm(PurchaseActivity purchaseActivity) {
        this.f1088a = purchaseActivity;
    }

    public final void run() {
        this.f1088a.f1045s.setVisibility(0);
        this.f1088a.f1045s.bringToFront();
    }
}
