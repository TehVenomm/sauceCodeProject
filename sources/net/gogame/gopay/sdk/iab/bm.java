package net.gogame.gopay.sdk.iab;

final class bm implements Runnable {
    /* renamed from: a */
    final /* synthetic */ PurchaseActivity f1109a;

    bm(PurchaseActivity purchaseActivity) {
        this.f1109a = purchaseActivity;
    }

    public final void run() {
        this.f1109a.f1040s.setVisibility(0);
        this.f1109a.f1040s.bringToFront();
    }
}
