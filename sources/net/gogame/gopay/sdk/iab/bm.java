package net.gogame.gopay.sdk.iab;

final class bm implements Runnable {
    /* renamed from: a */
    final /* synthetic */ PurchaseActivity f3497a;

    bm(PurchaseActivity purchaseActivity) {
        this.f3497a = purchaseActivity;
    }

    public final void run() {
        this.f3497a.f3428s.setVisibility(0);
        this.f3497a.f3428s.bringToFront();
    }
}
