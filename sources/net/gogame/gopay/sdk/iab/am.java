package net.gogame.gopay.sdk.iab;

final class am implements Runnable {
    /* renamed from: a */
    final /* synthetic */ PurchaseActivity f3454a;

    am(PurchaseActivity purchaseActivity) {
        this.f3454a = purchaseActivity;
    }

    public final void run() {
        new bd(this.f3454a).execute(new Void[0]);
    }
}
