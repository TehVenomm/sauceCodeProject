package net.gogame.gopay.sdk.iab;

final class am implements Runnable {
    /* renamed from: a */
    final /* synthetic */ PurchaseActivity f1066a;

    am(PurchaseActivity purchaseActivity) {
        this.f1066a = purchaseActivity;
    }

    public final void run() {
        new bd(this.f1066a).execute(new Void[0]);
    }
}
